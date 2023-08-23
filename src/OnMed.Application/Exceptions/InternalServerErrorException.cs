using System.Net;

namespace OnMed.Application.Exceptions;

public class InternalServerErrorException : ClientException
{

    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;
    public override string TitleMessage { get; protected set; } = String.Empty;
}
