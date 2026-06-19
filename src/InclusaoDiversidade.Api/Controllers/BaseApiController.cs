using InclusaoDiversidade.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace InclusaoDiversidade.Api.Controllers;

/// <summary>
/// Base dos controllers: padroniza as respostas de sucesso no envelope
/// <see cref="RespostaApi{T}"/> (sucesso + mensagem + dados [+ paginação]).
/// </summary>
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult Sucesso<T>(T dados, string mensagem)
        => Ok(RespostaApi<T>.Ok(dados, mensagem));

    protected IActionResult SucessoPaginado<T>(PagedResult<T> resultado, string mensagem)
        => Ok(RespostaApi<IReadOnlyCollection<T>>.OkPaginado(
            resultado.Items, PaginacaoMetadados.De(resultado), mensagem));

    protected IActionResult Criado<T>(string actionName, object routeValues, T dados, string mensagem)
        => CreatedAtAction(actionName, routeValues, RespostaApi<T>.Ok(dados, mensagem));
}
