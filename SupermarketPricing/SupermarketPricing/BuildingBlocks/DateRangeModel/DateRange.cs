using System;

namespace SupermarketPricing.BuildingBlocks.DateRangeModel;

public class DateRange
{
    public readonly DateTime StartDate;
    public readonly DateTime EndDate;

    public DateRange(DateTime startDate, DateTime endDate)
    {
        AssertDateRange(startDate, endDate);

        StartDate = startDate;
        EndDate = endDate;
    }

    private static void AssertDateRange(DateTime startDate, DateTime endDate)
    {
        if (DateTime.Compare(startDate, endDate) > 0)
            throw new ArgumentOutOfRangeException("endDate", "EndDate must be later than StartDate");
    }
}