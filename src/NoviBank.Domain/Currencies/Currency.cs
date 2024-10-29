using DDD.Core.DomainObjects;

namespace NoviBank.Domain.Currencies;

public class Currency : AggregateRoot<DefaultGuidId>
{
    public string Name { get; set; }
    public decimal Value { get; set; }
    public DateTime Updated_At { get; set; }
}