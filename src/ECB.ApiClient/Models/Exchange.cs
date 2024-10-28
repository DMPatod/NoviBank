namespace ECB.ApiClient.Models;

public class Exchange
{
    public string Currency { get; set; }
    public decimal Rate { get; set; }
    public DateOnly Time { get; set; }

    public Exchange(string currency, string rate, string time)
    {
        Currency = currency;
        Rate = decimal.Parse(rate);
        Time = DateOnly.FromDateTime(DateTime.Parse(time));
    }
}