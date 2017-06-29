using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
    public class Enumerable__LastOrNone
    {
        #region Maybe<TSource> LastOrNone<TSource>()

        [Fact]
        public void WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.LastOrNone());
        }
        
        [Fact]
        public void WhenEmpty_ReturnsNone()
        {
            var source = new HashSet<int>().AsEnumerable();
            var result = source.LastOrNone();
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenOneElement_ReturnsSomeOfSingle()
        {
            var source = Enumerable.Range(1, 1);
            var result = source.LastOrNone();
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }
        
        [Fact]
        public void WhenTwoElements_ReturnsSomeOfLast()
        {
            var source = Enumerable.Range(1, 2);
            var result = source.LastOrNone();
            var value = result.MustBeSome();
            Assert.Equal(2, value);
        }
        
        [Fact]
        public void WhenEmptyList_ReturnsNone()
        {
            var source = new List<int>().AsEnumerable();
            var result = source.LastOrNone();
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenOneElementList_ReturnsSomeOfSingle()
        {
            var source = new List<int> { 1 }.AsEnumerable();
            var result = source.LastOrNone();
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }
        
        [Fact]
        public void WhenTwoElementsList_ReturnsSomeOfSingle()
        {
            var source = new List<int> {1, 2}.AsEnumerable();
            var result = source.LastOrNone();
            var value = result.MustBeSome();
            Assert.Equal(2, value);
        }
        
        #endregion
        
        #region Maybe<TSource> LastOrNone<TSource>(Func<TSource, bool> predicate)

        [Fact]
        public void WithPredicate_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.LastOrNone(it => true));
        }

        [Fact]
        public void WithPredicate_WhenPredicateIsNull_ThrowsArgumentNullException()
        {
            var source = Enumerable.Empty<int>();
            Assert.Throws<ArgumentNullException>(() => source.LastOrNone(null));
        }
        
        #region Empty
        
        [Fact]
        public void WithTruePredicate_WhenEmpty_ReturnsNone()
        {
            var source = new HashSet<int>().AsEnumerable();
            var result = source.LastOrNone(it => true);
            result.MustBeNone();
        }
        
        [Fact]
        public void WithFalsePredicate_WhenEmpty_ReturnsNone()
        {
            var source = new HashSet<int>().AsEnumerable();
            var result = source.LastOrNone(it => false);
            result.MustBeNone();
        }
        
        [Fact]
        public void WithPredicate_WhenEmpty_DoesNotInvokePredicate()
        {
            var rec = new Recorder();
            var source = new HashSet<int>().AsEnumerable();
            var result = source.LastOrNone(rec.Record((int it) => false));
            result.MustBeNone();
            rec.MustHaveExactly(0.Invocations());
        }
        
        #endregion
        
        #region One Element
        
        [Fact]
        public void WithTruePredicate_WhenOneElement_ReturnsSome()
        {
            var source = Enumerable.Range(1, 1);
            var result = source.LastOrNone(it => true);
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }
        
        [Fact]
        public void WithFalsePredicate_WhenOneElement_ReturnsNone()
        {
            var source = Enumerable.Range(1, 1);
            var result = source.LastOrNone(it => false);
            result.MustBeNone();
        }
        
        [Fact]
        public void WithPredicate_WhenOneElement_DoesNotInvokePredicate()
        {
            var rec = new Recorder();
            var source = Enumerable.Range(1, 1);
            var result = source.LastOrNone(rec.Record((int it) => true));
            var value = result.MustBeSome();
            Assert.Equal(1, value);
            rec.MustHaveExactly(1.Invocations());
        }
        
        #endregion
        
        #region Two Elements
        
        [Fact]
        public void WithTruePredicate_WhenTwoElements_ReturnsLast()
        {
            var source = Enumerable.Range(1, 2);
            var result = source.LastOrNone(it => true);
            var value = result.MustBeSome();
            Assert.Equal(2, value);
        }
        
        [Fact]
        public void WithFalsePredicate_WhenTwoElements_ReturnsNone()
        {
            var source = Enumerable.Range(1, 2);
            var result = source.LastOrNone(it => false);
            result.MustBeNone();
        }
        
        #endregion
        
        #region Three Elements

        [Fact]
        public void WithPredicateMatchingFirst_WhenThreeElements_ReturnsSome()
        {
            var source = Enumerable.Range(1, 3);
            var result = source.LastOrNone(it => it == 1);
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }

        [Fact]
        public void WithPredicateMatchingMiddle_WhenThreeElements_ReturnsSome()
        {
            var source = Enumerable.Range(1, 3);
            var result = source.LastOrNone(it => it == 2);
            var value = result.MustBeSome();
            Assert.Equal(2, value);
        }

        [Fact]
        public void WithPredicateMatchingLast_WhenThreeElements_ReturnsSomeOfLast()
        {
            var source = Enumerable.Range(1, 3);
            var result = source.LastOrNone(it => it == 3);
            var value = result.MustBeSome();
            Assert.Equal(3, value);
        }

        [Fact]
        public void WithPredicateMatchingLastAndNextToLast_WhenThreeElements_ReturnsSomeOfLast()
        {
            var source = new []{1, 2, 3, 3}.AsEnumerable();
            var result = source.LastOrNone(it => it == 3);
            var value = result.MustBeSome();
            Assert.Equal(3, value);
        }

        [Fact]
        public void WithPredicate_WhenThreeElements_InvokesPredicateThreeTimes()
        {
            var rec = new Recorder();
            var source = Enumerable.Range(1, 3);
            var result = source.LastOrNone(rec.Record((int it) => true));
            var value = result.MustBeSome();
            Assert.Equal(3, value);
            rec.MustHaveExactly(3.Invocations());
        }
        
        #endregion
        
        #endregion
    }
}