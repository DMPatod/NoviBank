using NoviBank.Application.Currencies.Commands;

namespace ECB.WebServer.Contracts.Currencies;

public record AddCurrencyRequest(IList<CurrencyItem> Currencies, string Date);