using System;
using System.Collections.Generic;
using System.Linq;
using Amplified.CSharp.Extensions;
using Amplified.CSharp.Util;
using Xunit;

namespace Amplified.CSharp
{
    // ReSharper disable once InconsistentNaming
    public class Enumerable_To_Maybe
    {
        #region Guards

        [Fact]
        public void SingleOrNone_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.SingleOrNone());
        }

        [Fact]
        public void SingleOrNone_WithPredicate_WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.SingleOrNone(it => true));
        }

        [Fact]
        public void SingleOrNone_WithPredicate_WhenPredicateIsNull_ThrowsArgumentNullException()
        {
            var source = Enumerable.Empty<int>();
            Assert.Throws<ArgumentNullException>(() => source.SingleOrNone(null));
        }
        
        #endregion
        
        #region Maybe<TSource> SingleOrNone<TSource>()
        
        [Fact]
        public void SingleOrNone_WhenEmpty_ReturnsNone()
        {
            var source = Enumerable.Empty<int>();
            var result = source.SingleOrNone();
            result.MustBeNone();
        }
        
        [Fact]
        public void SingleOrNone_WhenOneElement_ReturnsSome()
        {
            var source = Enumerable.Range(1, 1);
            var result = source.SingleOrNone();
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }
        
        [Fact]
        public void SingleOrNone_WhenTwoElements_ThrowsException()
        {
            var source = Enumerable.Range(1, 2);
            Assert.Throws<InvalidOperationException>(() => source.SingleOrNone());
        }
        
        [Fact]
        public void SingleOrNone_WhenThreeElements_ThrowsException()
        {
            var source = Enumerable.Range(1, 3);
            Assert.Throws<InvalidOperationException>(() => source.SingleOrNone());
        }
        
        [Fact]
        public void SingleOrNone_WhenEmptyList_ReturnsNone()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var source = new List<int>();
            var result = source.SingleOrNone();
            result.MustBeNone();
        }
        
        [Fact]
        public void SingleOrNone_WhenOneElementList_ReturnsSome()
        {
            var source = new List<int> { 1 };
            var result = source.SingleOrNone();
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }
        
        [Fact]
        public void SingleOrNone_WhenTwoElementsList_ThrowsException()
        {
            var source = new List<int> {1, 2};
            Assert.Throws<InvalidOperationException>(() => source.SingleOrNone());
        }
        
        #endregion
        
        #region Maybe<TSource> SingleOrNone<TSource>(Func<TSource, bool> predicate)
        
        #region Empty
        
        [Fact]
        public void SingleOrNone_WithTruePredicate_WhenEmpty_ReturnsNone()
        {
            var source = Enumerable.Empty<int>();
            var result = source.SingleOrNone(it => true);
            result.MustBeNone();
        }
        
        [Fact]
        public void SingleOrNone_WithFalsePredicate_WhenEmpty_ReturnsNone()
        {
            var source = Enumerable.Empty<int>();
            var result = source.SingleOrNone(it => false);
            result.MustBeNone();
        }
        
        [Fact]
        public void SingleOrNone_WithPredicate_WhenEmpty_DoesNotInvokePredicate()
        {
            var rec = new Recorder();
            var source = Enumerable.Empty<int>();
            var result = source.SingleOrNone(rec.Record((int it) => false));
            result.MustBeNone();
            rec.MustHaveExactly(0.Invocations());
        }
        
        #endregion
        
        #region One Element
        
        [Fact]
        public void SingleOrNone_WithTruePredicate_WhenOneElement_ReturnsSome()
        {
            var source = Enumerable.Range(0, 1);
            var result = source.SingleOrNone(it => true);
            result.MustBeSome();
        }
        
        [Fact]
        public void SingleOrNone_WithFalsePredicate_WhenOneElement_ReturnsNone()
        {
            var source = Enumerable.Range(0, 1);
            var result = source.SingleOrNone(it => false);
            result.MustBeNone();
        }
        
        [Fact]
        public void SingleOrNone_WithPredicate_WhenOneElement_DoesNotInvokePredicate()
        {
            var rec = new Recorder();
            var source = Enumerable.Range(0, 1);
            var result = source.SingleOrNone(rec.Record((int it) => true));
            result.MustBeSome();
            rec.MustHaveExactly(1.Invocations());
        }
        
        #endregion
        
        #region Two Elements
        
        [Fact]
        public void SingleOrNone_WithTruePredicate_WhenTwoElements_ThrowsException()
        {
            var source = Enumerable.Range(0, 2);
            Assert.Throws<InvalidOperationException>(() => source.SingleOrNone(it => true));
        }
        
        [Fact]
        public void SingleOrNone_WithFalsePredicate_WhenTwoElements_ReturnsNone()
        {
            var source = Enumerable.Range(0, 2);
            var result = source.SingleOrNone(it => false);
            result.MustBeNone();
        }
        
        #endregion
        
        #region Three Elements

        [Fact]
        public void SingleOrNone_WithPredicateMatchingFirst_WhenThreeElements_ReturnsSome()
        {
            var source = Enumerable.Range(0, 3);
            var result = source.SingleOrNone(it => it == 0);
            var value = result.MustBeSome();
            Assert.Equal(0, value);
        }

        [Fact]
        public void SingleOrNone_WithPredicateMatchingMiddle_WhenThreeElements_ReturnsSome()
        {
            var source = Enumerable.Range(0, 3);
            var result = source.SingleOrNone(it => it == 1);
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }

        [Fact]
        public void SingleOrNone_WithPredicateMatchingLast_WhenThreeElements_ReturnsSome()
        {
            var source = Enumerable.Range(0, 3);
            var result = source.SingleOrNone(it => it == 2);
            var value = result.MustBeSome();
            Assert.Equal(2, value);
        }

        [Fact]
        public void SingleOrNone_WithPredicate_WhenThreeElements_InvokesPredicateTwoTimes()
        {
            var rec = new Recorder();
            var source = Enumerable.Range(0, 3);
            Assert.Throws<InvalidOperationException>(() => source.SingleOrNone(rec.Record((int it) => true)));
            rec.MustHaveExactly(2.Invocations());
        }
        
        #endregion
        
        #endregion
    }
}