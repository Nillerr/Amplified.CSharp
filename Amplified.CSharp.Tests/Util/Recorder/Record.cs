using System;
using Amplified.CSharp.Attributes;
using Xunit;

namespace Amplified.CSharp.Util
{
    public class Record
    {
        private static readonly Random Random = new Random();
        
        [Fact]
        [ExcludeFromCoverage]
        public void MustHaveExactly_WithZeroInvocations()
        {
            var rec = new Recorder();

            // ReSharper disable once UnusedVariable
            rec.Record((int arg) => { });
            
            rec.MustHaveExactly(0.Invocations());
        }
        
        [Fact]
        public void MustHaveExactly_WithOneInvocation()
        {
            var rec = new Recorder();
            
            var action = rec.Record((int arg) => { });
            action(int.MinValue);
            
            rec.MustHaveExactly(1.Invocations());
        }
        
        [Fact]
        public void MustHaveExactly_WithRandomInvocations()
        {
            var rec = new Recorder();
            
            var action = rec.Record((int arg) => { });

            var invocations = Random.Next(2, 100);
            for (var i = 0; i < invocations; i++) {
                action(int.MinValue);
            }

            rec.MustHaveExactly(invocations.Invocations());
        }
        
        [Fact]
        public void WithOneArgumentAction_IncrementsInvocationCounter()
        {
            var rec = new Recorder();
            var invocationsBefore = rec.Invocations;
            
            var action = rec.Record((int arg) => { });
            action(int.MaxValue);

            Assert.Equal(0, invocationsBefore);
            Assert.Equal(1, rec.Invocations);
        }
        
        [Fact]
        public void WithNoArgumentsFunc_IncrementsInvocationCounter()
        {
            var rec = new Recorder();
            var invocationsBefore = rec.Invocations;
            
            var func = rec.Record(() => true);
            var result = func();

            Assert.Equal(true, result);
            Assert.Equal(0, invocationsBefore);
            Assert.Equal(1, rec.Invocations);
        }
        
        [Fact]
        public void WithOneArgumentFunc_IncrementsInvocationCounter()
        {
            var rec = new Recorder();
            var invocationsBefore = rec.Invocations;
            
            var func = rec.Record((object arg1) => arg1);
            
            var param1 = new object();
            var result = func(param1);

            Assert.Same(param1, result);
            Assert.Equal(0, invocationsBefore);
            Assert.Equal(1, rec.Invocations);
        }
        
        [Fact]
        public void WithTwoArgumentFunc_IncrementsInvocationCounter()
        {
            var rec = new Recorder();
            var invocationsBefore = rec.Invocations;
            
            var func = rec.Record((object arg1, object arg2) => (arg1, arg2));
            
            var param1 = new object();
            var param2 = new object();
            
            var result = func(param1, param2);

            Assert.Same(param1, result.Item1);
            Assert.Same(param2, result.Item2);
            Assert.Equal(0, invocationsBefore);
            Assert.Equal(1, rec.Invocations);
        }
        
        [Fact]
        public void WithThreeArgumentFunc_IncrementsInvocationCounter()
        {
            var rec = new Recorder();
            var invocationsBefore = rec.Invocations;
            
            var func = rec.Record((object arg1, object arg2, object arg3) => (arg1, arg2, arg3));
            
            var param1 = new object();
            var param2 = new object();
            var param3 = new object();
            
            var result = func(param1, param2, param3);

            Assert.Same(param1, result.Item1);
            Assert.Same(param2, result.Item2);
            Assert.Same(param3, result.Item3);
            Assert.Equal(0, invocationsBefore);
            Assert.Equal(1, rec.Invocations);
        }
    }
}