using AutoFixture;
using SupermarketPricing.BuildingBlocks.DateRangeModel;
using System;
using Xunit;

namespace SupermarketPricingTests.BuildingBlocks;

public class DateRangeTests
{
    private Fixture fixture;

    public DateRangeTests()
    {
        fixture = new Fixture();
    }

    [Fact]
    public void WhenCreated_HasStartAndEndDates()
    {
        var startDate = fixture.Create<DateTime>();
        var endDate = startDate.AddDays(1);

        DateRange dateRange = new DateRange(startDate, endDate);

        Assert.Equal(startDate, dateRange.StartDate);
        Assert.Equal(endDate, dateRange.EndDate);

        Assert.NotEqual(DateTime.MinValue, dateRange.StartDate);
        Assert.NotEqual(DateTime.MinValue, dateRange.EndDate);
    }

    [Fact]
    public void WhenCreated_EndDateMustBePosteriorToStartDate()
    {
        var startDate = fixture.Create<DateTime>();
        var endDate = startDate.AddDays(-3);

        Assert.Throws<ArgumentOutOfRangeException>(() => new DateRange(startDate, endDate));
    }
}