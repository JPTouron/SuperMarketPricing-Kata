using AutoFixture;
using AutoFixture.AutoMoq;
using SuperMarketPricing.Domain.BuildingBlocks.MoneyModel;
using SuperMarketPricing.Domain.BuildingBlocks.Tests.MoneyModel;
using System;
using Xunit;

namespace SuperMarketPricing.Domain.BuildingBlocks.Tests
{
    public class MaybeTests
    {
        private IFixture fixture;

        public MaybeTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public void Equals_WhenComparedWithAnotherMaybe_ThenReturnsFalseWhenDifferent()
        {
            const int value = 14;
            var m1 = new Maybe<int>(value);
            var m2 = new Maybe<int>(value + 1);

            Assert.False(m1.Equals(m2));

            var m3 = new Maybe<string>("hola");
            var m4 = new Maybe<string>("chau");

            Assert.False(m3.Equals(m4));

            CurrencyTypeRepository repo = new CurrencyTypeRepository();

            var value1 = Money.Create(98.003m, repo.Get("USD"));
            var value2 = Money.Create(98.002m, repo.Get("USD"));
            var m5 = new Maybe<Money>(value1);
            var m6 = new Maybe<Money>(value2);

            Assert.False(m5.Equals(m6));
        }

        [Fact]
        public void Equals_WhenComparedWithAnotherMaybe_ThenReturnsTrueWhenSame()
        {
            const int value = 14;
            var m1 = new Maybe<int>(value);
            var m2 = new Maybe<int>(value);

            Assert.True(m1.Equals(m2));

            const string value2 = "hola";
            var m3 = new Maybe<string>(value2);
            var m4 = new Maybe<string>(value2);

            Assert.True(m3.Equals(m4));

            CurrencyTypeRepository repo = new CurrencyTypeRepository();

            var value3 = Money.Create(98.003m, repo.Get("USD"));
            var m5 = new Maybe<Money>(value3);
            var m6 = new Maybe<Money>(value3);

            Assert.True(m5.Equals(m6));
        }

        [Fact]
        public void Selector_GetsTheMaybeItemWhenRequested()
        {
            CurrencyTypeRepository repo = new CurrencyTypeRepository();

            var value1 = Money.Create(98.003m, repo.Get("USD"));
            var m = new Maybe<Money>(value1);

            var selected = m.Select(x => x);

            Assert.Equal(m, selected);
        }

        [Fact]
        public void Selector_GetsTheMaybeOfBoolWhenQueried()
        {
            CurrencyTypeRepository repo = new CurrencyTypeRepository();

            var value1 = Money.Create(98.003m, repo.Get("USD"));
            var m = new Maybe<Money>(value1);

            var selected = m.Select(x => x.Amount >= 98m);

            var result = selected.GetValueOrFallback(false);
            Assert.True(result);
        }

        [Fact]
        public void WhenCreated_AndNoParams_GetsFallbackValueWhenRequested()
        {
            var m = new Maybe<int>();

            const int fallbackValue = 90;

            var result = m.GetValueOrFallback(fallbackValue);

            Assert.Equal(result, fallbackValue);
        }

        [Fact]
        public void WhenCreated_WithNullParams_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Maybe<object>(null));
        }

        [Fact]
        public void WhenCreated_WithParams_GetsValueInsteadOfFallback()
        {
            const int value = 20;
            var m = new Maybe<int>(value);

            const int fallbackValue = 90;

            var result = m.GetValueOrFallback(fallbackValue);

            Assert.Equal(result, value);
        }
    }
}