using System.Net;

namespace OnMed.Application.Exceptions;

public class AlreadyAppointmentException : ClientException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;
    public override string TitleMessage { get; protected set; } = String.Empty;
}
