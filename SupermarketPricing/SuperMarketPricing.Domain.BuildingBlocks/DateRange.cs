using Ardalis.GuardClauses;
using SuperMarketPricing.Domain.BuildingBlocks.Base;
using System;

namespace SuperMarketPricing.Domain.BuildingBlocks
{
    public static class GuardClauseExtensions
    {
        public static DateTime EndDateNotPrecedingStartDate(this IGuardClause guardClause, DateTime startDate, DateTime input, string parameterName)
        {
            if (DateTime.Compare(startDate, input) >= 0)
                throw new ArgumentOutOfRangeException(parameterName, $"{parameterName} must be later than StartDate");

            return input;
        }
    }

    public class DateRange : ValueObject<DateRange>
    {
        public readonly DateTime EndDate;
        public readonly DateTime StartDate;

        public DateRange(DateTime startDate, DateTime endDate)
        {
            Guard.Against.EndDateNotPrecedingStartDate(startDate, endDate, nameof(endDate));

            StartDate = startDate;
            EndDate = endDate;
        }

        public override int GetHashCode()
        {
            return EndDate.GetHashCode() ^ StartDate.GetHashCode();
        }

        public bool IsDateWithinRange(DateTime dateTime)
        {
            return StartDate <= dateTime && dateTime <= EndDate;
        }

        protected override bool InternalEquals(DateRange other)
        {
            return EndDate.Equals(other.EndDate) && StartDate.Equals(other.StartDate);
        }
    }
}