using TenshiShop.Application.Utils;
using TenshiShop.Domain.Gateways;

namespace TenshiShop.Application.Permissions.Impl;

public class ActiveCkeckerByEmail : IActiveChecker
{
    private readonly IFindUserByEmailGateway _gateway;
    private readonly IConfirmMailSender _confirmMailSender;

    public ActiveCkeckerByEmail(
        IFindUserByEmailGateway gateway, 
        IConfirmMailSender confirmMailSender)
    {
        _gateway = gateway; 
        _confirmMailSender = confirmMailSender;
    }

    public async Task<bool> CheckOrSendMail(string email, CancellationToken ct)
    {
        var user = await _gateway.FindUserByEmail(email, ct);
        if (user == null)
        {
            throw new ArgumentException("email not found");
        }

        if (!user.IsActive)
        {
            await _confirmMailSender.Send(user.Email, user.Name, ct);

            return false;
        }

        return true;
    }
}
