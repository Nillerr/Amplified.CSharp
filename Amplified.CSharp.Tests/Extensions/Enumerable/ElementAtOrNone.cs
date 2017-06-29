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
    public class Enumerable__ElementAtOrNone
    {
        #region Maybe<TSource> ElementAtOrNone<TSource>(int index)

        [Fact]
        public void WhenSourceIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.ElementAtOrNone(154));
        }
        
        [Fact]
        public void WhenEmpty_ReturnsNone()
        {
            var source = new HashSet<int>().AsEnumerable();
            var result = source.ElementAtOrNone(0);
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenOneElement_ReturnsSomeOfSingle()
        {
            var source = Enumerable.Range(1, 1);
            var result = source.ElementAtOrNone(0);
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }
        
        [Fact]
        public void WhenTwoElements_ReturnsSomeOfLast()
        {
            var source = Enumerable.Range(1, 2);
            var result = source.ElementAtOrNone(1);
            var value = result.MustBeSome();
            Assert.Equal(2, value);
        }
        
        [Fact]
        public void WhenEmptyList_ReturnsNone()
        {
            var source = new List<int>().AsEnumerable();
            var result = source.ElementAtOrNone(0);
            result.MustBeNone();
        }
        
        [Fact]
        public void WhenOneElementList_ReturnsSomeOfSingle()
        {
            var source = new List<int> { 1 }.AsEnumerable();
            var result = source.ElementAtOrNone(0);
            var value = result.MustBeSome();
            Assert.Equal(1, value);
        }
        
        [Fact]
        public void WhenTwoElementsList_ReturnsSomeOfSingle()
        {
            var source = new List<int> {1, 2}.AsEnumerable();
            var result = source.ElementAtOrNone(1);
            var value = result.MustBeSome();
            Assert.Equal(2, value);
        }
        
        #endregion
    }
}