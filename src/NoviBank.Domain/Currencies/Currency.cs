using DDD.Core.DomainObjects;

namespace NoviBank.Domain.Currencies;

public class Currency : AggregateRoot<DefaultGuidId>
{
    public string Name { get; set; }
    
    public decimal Rate { get; set; }

    public DateTime Updated_At { get; set; }

    public Currency(DefaultGuidId id, string name, decimal rate, DateTime updated_At)
        : base(id)
    {
        Name = name;
        Rate = rate;
        Updated_At = updated_At;
    }

    public static Currency Create(string name, decimal rate)
    {
        return new Currency(DefaultGuidId.Create(), name, rate, DateTime.Now);
    }
}