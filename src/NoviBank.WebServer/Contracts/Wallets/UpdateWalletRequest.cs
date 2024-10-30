namespace ECB.WebServer.Contracts.Wallets;

public record UpdateWalletRequest(decimal Amount, string? CurrencyId, string Strategy);