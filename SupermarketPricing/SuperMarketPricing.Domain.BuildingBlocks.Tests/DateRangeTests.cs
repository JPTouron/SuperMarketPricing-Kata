using AutoFixture;
using AutoFixture.AutoMoq;
using System;
using Xunit;

namespace SuperMarketPricing.Domain.BuildingBlocks.Tests
{
    public class DateRangeTests
    {
        private IFixture fixture;

        public DateRangeTests()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }

        [Fact]
        public void Create_EndDateMustBeGreaterThanStartDate()
        {
            var sd = DateTime.Now;
            var ed = sd;
            var ed2 = sd.AddDays(-1);

            Assert.Throws<ArgumentOutOfRangeException>(() => new DateRange(sd, ed));

            Assert.Throws<ArgumentOutOfRangeException>(() => new DateRange(sd, ed2));
        }

        [Fact]
        public void Create_MustHaveSetupDates()
        {
            var sd = DateTime.Now;
            var ed = DateTime.Now.AddDays(2);

            var dr = new DateRange(sd, ed);

            Assert.Equal(sd, dr.StartDate);
            Assert.Equal(ed, dr.EndDate);
        }
    }
}