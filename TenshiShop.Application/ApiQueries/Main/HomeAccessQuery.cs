using MediatR;
using TenshiShop.Application.ApiResponses;
using TenshiShop.Application.ApiResponses.Main;

namespace TenshiShop.Application.ApiQueries.Main;

public class HomeAccessQuery : IRequest<Result<HomeAccessResponse>>
{
    public string Email { get; set; } = string.Empty;
}