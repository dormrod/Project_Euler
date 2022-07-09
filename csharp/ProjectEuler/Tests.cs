﻿using System.Linq;
using NUnit.Framework;
using ProjectEuler.Common;

namespace ProjectEuler
{
    [TestFixture]
    public class Tests
    {
        [TestCase(new long[] {100})]
        [TestCase(new long[] {2, 100})]
        [TestCase(new long[] {2, 5, 10, 97, 100})]
        [TestCase(new long[] {2, 5, 10, 32, 41, 42, 97, 100})]
        [TestCase(new long[] {2, 2, 5, 4, 10, 32, 41, 42, 17, 17, 97, 100})]
        public void PrimeCache_GeneratesPrimesUpTo100_Successfully(long[] maxValues)
        {
            var expected = new long[] {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};

            var sut = new PrimeCache();
            foreach (var maxValue in maxValues)
            {
                var _  = sut.GetValues(maxValue).ToArray();
            }

            var actual = sut.GetCache();
            
            Assert.That(actual, Is.EquivalentTo(expected));
        }
        
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        [TestCase(100_000_000, Explicit = true, Reason = "Relatively long running (<1m)")]
        public void PrimeCache_GeneratesLargeNumberOfPrimes_SuccessfullyWithoutOutOfMemoryException(long maxValue)
        {
            var sut = new PrimeCache();

            Assert.DoesNotThrow(() => sut.GetValues(maxValue).ToArray());
        }
        
        [TestCase(2)]
        [TestCase(41)]
        [TestCase(1000)]
        
        public void PrimeCache_GeneratesNextThreePrimesAfter100_Successfully(long startingValue)
        {
            var expected = new long[] {101, 103, 107};
            
            var sut = new PrimeCache(startingValue);

            var next1 = sut.NextValue(100);
            var next2 = sut.NextValue(next1);
            var next3 = sut.NextValue(next2);

            var actual = new[] {next1, next2, next3};
            
            Assert.That(actual, Is.EquivalentTo(expected));
        }
        
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        [TestCase(100_000_000, Explicit = true, Reason = "Relatively long running (<1m)")]
        public void PrimeCache_GeneratesNextPrimeAfterALargeNumberOfPrimes_SuccessfullyWithoutOutOfMemoryException(long maxValue)
        {
            var sut = new PrimeCache();

            Assert.DoesNotThrow(() => sut.NextValue(maxValue));
        }
        
        [TestCase(0, new [] {0}, new [] {0})]
        [TestCase(1, new [] {1}, new [] {1})]
        [TestCase(2, new [] {2}, new [] {0, 1})]
        [TestCase(10, new [] {0, 1}, new [] {0, 1, 0, 1})]
        [TestCase(123456789, new [] {9, 8, 7, 6, 5, 4, 3, 2, 1}, new [] {1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1})]
        [TestCase(12345678900000, new [] {0, 0, 0, 0, 0, 9, 8, 7, 6, 5, 4, 3, 2, 1}, new [] {0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 1})]
        public void NaturalNumber_CanGetDigits_InBaseTenOrTwo_Successfully(long value, int[] expectedBase10, int[] expectedBase2)
        {
            var sut = new NaturalNumber(value);
            
            Assert.That(sut.GetDigits().ToArray(), Is.EqualTo(expectedBase10));
            Assert.That(sut.GetDigits(2).ToArray(), Is.EqualTo(expectedBase2));
        }
        
        [TestCase(0, new int[0])]
        [TestCase(1, new int[0])]
        [TestCase(2, new [] {2})]
        [TestCase(10, new [] {2, 5})]
        [TestCase(43, new [] {43})]
        [TestCase(100, new [] {2, 2, 5, 5})]
        [TestCase(1_000_001, new [] {101, 9901})]
        public void NaturalNumber_CanGetPrimeFactors_Successfully(long value, int[] expected)
        {
            var primeCache = new PrimeCache();
            
            var sut = new NaturalNumber(value);
            
            Assert.That(sut.GetPrimeFactors(primeCache).ToArray(), Is.EqualTo(expected));
        }
    }
}