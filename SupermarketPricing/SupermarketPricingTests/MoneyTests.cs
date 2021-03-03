using SupermarketPricing.Model1.MoneyModel;
using Xunit;

public class MoneyTests
{
    private CurrencyTypeRepository repo;

    public MoneyTests()
    {
        repo = new CurrencyTypeRepository();
    }

    [Fact]
    public void CurrencyIsEqual()
    {
        Currency cur1 = repo.Get("BTC");
        Currency cur2 = repo.Get("BTC");

        Assert.Equal<Currency>(cur1, cur2);
    }

    [Fact]
    public void WhenCreatedOffAnExistingMoney_RetainsAllPropertiesButTheAmount()
    {
        Currency cur1 = repo.Get("BTC");
        var newAmount = 4.065m;
        Money m1 = new Money(1.08m, repo.Get("BTC"));
        Money m2 = new Money(m1, newAmount);


        Assert.Equal<Currency>(m1.Currency, m2.Currency);
        Assert.Equal(newAmount, m2.Amount);
    }

    [Fact]
    public void CurrencyIsNotEqual()
    {
        Currency cur1 = repo.Get("HKD");
        Currency cur2 = repo.Get("BTC");
        Assert.NotEqual<Currency>(cur1, cur2);
    }

    [Fact]
    public void MoneyCurrencyIsNotEqual()
    {
        Money m1 = new Money(1m, repo.Get("HKD"));
        Money m2 = new Money(1m, repo.Get("MOP"));
        Assert.NotEqual<Money>(m1, m2);
    }

    [Fact]
    public void MoneyIsNotEqualAmount()
    {
        Money m1 = new Money(1m, repo.Get("HKD"));
        Money m2 = new Money(2m, repo.Get("HKD"));
        Assert.NotEqual<Money>(m1, m2);
    }

    [Fact]
    public void MoneyIsEqual()
    {
        Money m1 = new Money(1.00000001m, repo.Get("BTC"));
        Money m2 = new Money(1.00000001m, repo.Get("BTC"));
        Assert.Equal<Money>(m1, m2);
    }

    [Fact]
    public void MoneyAddMoney()
    {
        Money m1 = new Money(1.00000001m, repo.Get("BTC"));
        Money m2 = new Money(10.000000019m, repo.Get("BTC"));

        Money result = m1 + m2;
        Assert.Equal<Money>(result, new Money(11.000000029m, repo.Get("BTC")));
    }

    [Fact]
    public void MoneyAddDecimal()
    {
        Money m1 = new Money(1.00000001m, repo.Get("BTC"));

        Money result = m1 + 10.000000019m;
        Assert.Equal(11.000000029m, result.Amount);
    }

    [Fact]
    public void MoneySubtractMoney()
    {
        Money m1 = new Money(1.00000001m, repo.Get("MOP"));
        Money m2 = new Money(10.000000019m, repo.Get("MOP"));

        Money result = m2 - m1;
        Assert.Equal<Money>(result, new Money(9.000000009m, repo.Get("MOP")));
    }

    [Fact]
    public void MoneySubtractDecimal()
    {
        Money m1 = new Money(1.00000001m, repo.Get("BTC"));

        Money result = m1 - 0.000000019m;
        Assert.Equal(0.999999991m, result.Amount);
    }

    [Fact]
    public void MoneyMultiplyDecimal()
    {
        Money m1 = new Money(1.02m, repo.Get("MOP"));

        Money result = m1 * 2.5m;
        Assert.Equal<Money>(result, new Money(2.55m, repo.Get("MOP")));
    }

    [Fact]
    public void MoneyMultiplyInt()
    {
        Money m1 = new Money(1.000000014m, repo.Get("BTC"));

        Money result = m1 * 2;
        Assert.Equal(2.000000028m, result.Amount);
    }

    [Fact]
    public void MoneyMultiplySmallDecimal()
    {
        Money m1 = new Money(1.000000005m, repo.Get("BTC"));

        Money result = m1 * (5m / 1000m);
        Assert.Equal<Money>(result, new Money(0.005000000025m, repo.Get("BTC")));
    }

    [Fact]
    public void MoneyDividedByInt()
    {
        Money m1 = new Money(2.5m, repo.Get("BTC"));

        Money result = m1 / 2;
        Assert.Equal<Money>(result, new Money(1.25m, repo.Get("BTC")));
    }
}