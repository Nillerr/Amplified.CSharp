using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Amplified.CSharp.ComponentModel
{
    public sealed class MaybeConverter : TypeConverter
    {
        public MaybeConverter(Type actualType)
        {
            var valueType = actualType.GenericTypeArguments[0];
            var actualConverterType = typeof(MaybeConverter<>).MakeGenericType(valueType);
            ActualConverter = (TypeConverter) Activator.CreateInstance(actualConverterType);
        }

        public TypeConverter ActualConverter { get; }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => ActualConverter.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            => ActualConverter.ConvertFrom(context, culture, value);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => ActualConverter.CanConvertTo(context, destinationType);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            => ActualConverter.ConvertTo(context, culture, value, destinationType);

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
            => ActualConverter.CreateInstance(context, propertyValues);

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            => ActualConverter.GetCreateInstanceSupported(context);

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            => ActualConverter.GetProperties(context, value, attributes);

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            => ActualConverter.GetPropertiesSupported(context);

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            => ActualConverter.GetStandardValues(context);

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            => ActualConverter.GetStandardValuesExclusive(context);

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            => ActualConverter.GetStandardValuesSupported(context);

        public override bool IsValid(ITypeDescriptorContext context, object value)
            => ActualConverter.IsValid(context, value);
    }

    public sealed class MaybeConverter<T> : TypeConverter
    {
        public MaybeConverter()
        {
            MaybeType = typeof(Maybe<T>);
            ValueType = typeof(T);
            _valueConverter = TypeDescriptor.GetConverter(ValueType);

            if (ValueType.GetTypeInfo().IsValueType && Nullable.GetUnderlyingType(ValueType) == null)
            {
                NullableType = typeof(Nullable<>).MakeGenericType(ValueType);
                NullableValueConverter = TypeDescriptor.GetConverter(NullableType);
            }
        }
        
        public Type MaybeType { get; }
        
        public Type ValueType { get; }

        public Type NullableType { get; }
        
        private readonly TypeConverter _valueConverter;

        public TypeConverter ValueConverter => NullableValueConverter ?? _valueConverter;
        
        public TypeConverter NullableValueConverter { get; } 
        
        /// <summary>
        ///     Converts an anonymous object to either a Maybe.Some when the object is non-null, or a Maybe.None when 
        ///     the object is null. 
        /// </summary>
        private static Maybe<T> NullableToMaybe(object value)
        {
            return value != null ? Maybe<T>.Some((T) value) : Maybe<T>.None();
        }

        private static bool IsNullableType(Type type)
        {
            return !type.GetTypeInfo().IsValueType || Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        ///    <para>Gets a value indicating whether this converter can convert an object in the
        ///       given source type to the underlying simple type or a Maybe.None.</para>
        /// </summary>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == ValueType)
                return true;

            if (NullableValueConverter != null)
            {
                var underlyingType = Nullable.GetUnderlyingType(sourceType);
                if (underlyingType != null)
                    return NullableValueConverter.CanConvertFrom(context, underlyingType);
            }
            
            if (ValueConverter != null)
                return ValueConverter.CanConvertFrom(context, sourceType);
            
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        ///    Converts the given value to the converter's underlying simple type and wraps it in a Maybe.Some or a Maybe.None.
        /// </summary>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return Maybe<T>.None();

            var valueType = value.GetType();
            if (valueType == ValueType)
                return Maybe<T>.Some((T) value);

            var stringValue = value as string;
            if (stringValue != null && string.IsNullOrEmpty(stringValue))
                return Maybe<T>.None();

            if (ValueConverter != null)
            {
                var convertedValue = (T) ValueConverter.ConvertFrom(context, culture, value);
                return Maybe<T>.Some(convertedValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Gets a value indicating whether this converter can convert a value object to the destination type.
        /// </summary>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == ValueType)
                return true;

            if (NullableValueConverter != null)
            {
                var underlyingType = Nullable.GetUnderlyingType(destinationType);
                if (underlyingType != null)
                    return NullableValueConverter.CanConvertTo(context, underlyingType);
            }

            if (ValueConverter != null)
                return ValueConverter.CanConvertTo(context, destinationType);

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given value object to the destination type.
        /// </summary>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException(nameof(destinationType));

            var maybeValue = (Maybe<T>) value;
            return maybeValue.Match(
                some => ConvertSomeTo(context, culture, some, destinationType),
                () => ConvertNoneTo(context, culture, destinationType)
            );
        }

        private object ConvertSomeTo(ITypeDescriptorContext context, CultureInfo culture, T value, Type destinationType)
        {
            if (value.GetType() == destinationType)
                return value;
            
            if (ValueConverter != null)
                return ValueConverter.ConvertTo(context, culture, value, destinationType);

            return base.ConvertTo(context, culture, value, destinationType);
        }

        private object ConvertNoneTo(ITypeDescriptorContext context, CultureInfo culture, Type destinationType)
        {
            if (destinationType == typeof(string))
                return string.Empty;
            
            if (IsNullableType(destinationType))
                return null;

            return base.ConvertTo(context, culture, null, destinationType);
        }

        /// <summary>
        /// </summary>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (ValueConverter != null)
            {
                var instance = ValueConverter.CreateInstance(context, propertyValues);
                var maybe = NullableToMaybe(instance);
                return maybe;
            }
            
            return base.CreateInstance(context, propertyValues);
        }

        /// <summary>
        ///    <para>
        ///        Gets a value indicating whether changing a value on this object requires a call to
        ///        <see cref='CreateInstance'/> to create a new value, using the specified context.
        ///    </para>
        /// </summary>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            if (ValueConverter != null)
            {
                return ValueConverter.GetCreateInstanceSupported(context);
            }

            return base.GetCreateInstanceSupported(context);
        }

        /// <summary>
        ///    <para>
        ///        Gets a collection of properties for the type of array specified by the value
        ///        parameter using the specified context and attributes.
        ///    </para>
        /// </summary>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (ValueConverter != null)
            {
                var unwrappedValue = ((Maybe<T>) value).Match(some => (object) some, none => null);
                return ValueConverter.GetProperties(context, unwrappedValue, attributes);
            }

            return base.GetProperties(context, value, attributes);
        }

        /// <summary>
        ///    <para>Gets a value indicating whether this object supports properties using the specified context.</para>
        /// </summary>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            if (ValueConverter != null)
            {
                return ValueConverter.GetPropertiesSupported(context);
            }

            return base.GetPropertiesSupported(context);
        }

        /// <summary>
        ///    <para>Gets a collection of standard values for the data type this type converter is designed for.</para>
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (ValueConverter != null)
            {
                var values = ValueConverter.GetStandardValues(context);
                if (GetStandardValuesSupported(context) && values != null)
                {
                    // Create set of standard values around Maybe instances.
                    var wrappedValues = new Maybe<T>[values.Count + 1];
                    var idx = 0;

                    wrappedValues[idx++] = Maybe<T>.None();
                    foreach (var value in values)
                        wrappedValues[idx++] = NullableToMaybe(value);

                    return new StandardValuesCollection(wrappedValues);
                }
            }
            
            return base.GetStandardValues(context);
        }
        
        /// <summary>
        ///    <para>
        ///        Gets a value indicating whether the collection of standard values returned from
        ///        <see cref='GetStandardValues'/> is an exclusive list of possible values, using the specified context.
        ///    </para>
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            if (ValueConverter != null)
                return ValueConverter.GetStandardValuesExclusive(context);

            return base.GetStandardValuesExclusive(context);
        }

        /// <summary>
        ///    <para>
        ///        Gets a value indicating whether this object supports a standard set of values that can
        ///        be picked from a list using the specified context.
        ///    </para>
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            if (ValueConverter != null)
                return ValueConverter.GetStandardValuesSupported(context);

            return base.GetStandardValuesSupported(context);
        }

        /// <summary>
        ///    <para>Gets a value indicating whether the given value object is valid for this type.</para>
        /// </summary>
        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            if (ValueConverter != null)
            {
                // System.ComponentModel.NullableConverter has a null check, so value probably can be null.
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable HeuristicUnreachableCode
                if (value == null)
                    return true; // null is transformed to Maybe<T>.None
                // ReSharper restore HeuristicUnreachableCode

                return ValueConverter.IsValid(context, value);
            }
            
            return base.IsValid(context, value);
        }
    }
}