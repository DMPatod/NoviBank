using Moq;
using NoviBank.Application.Wallets.Commands;
using NoviBank.Domain;

namespace NoviBank.Application.Tests.Wallets.Commands;

public class AddWalletCommandTests
{
    [Fact]
    public async Task T()
    {
        var unitOfWork = new Mock<UnitOfWork>();

        var handler = new AddWalletCommandHandler(unitOfWork.Object);
        var result = await handler.Handle(new AddWalletCommand(""), default);
        
    }
}