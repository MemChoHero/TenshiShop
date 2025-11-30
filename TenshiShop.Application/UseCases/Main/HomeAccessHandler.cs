using MediatR;
using TenshiShop.Application.ApiQueries.Main;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Abstractions;
using TenshiShop.Application.ApiResponses.Main;
using TenshiShop.Application.Permissions;

namespace TenshiShop.Application.UseCases.Main;

public class HomeAccessHandler : IRequestHandler<HomeAccessQuery, Result<HomeAccessResponse>>
{
    private readonly IActiveChecker _activeChecker;

    public HomeAccessHandler(IActiveChecker activeChecker)
    {
        _activeChecker = activeChecker; 
    }

    public async Task<Result<HomeAccessResponse>> Handle(HomeAccessQuery request, CancellationToken cancellationToken)
    {
        var access = await _activeChecker.CheckOrSendMail(request.Email, cancellationToken);
        if (!access)
        {
            return new Result<HomeAccessResponse>(new Error("Ваш аккаунт неактивен, направили ссылку активации на почту"));
        }

        return new Result<HomeAccessResponse>(new HomeAccessResponse()
        {
            Email = request.Email,
        });
    }
}