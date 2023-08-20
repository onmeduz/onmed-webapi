namespace OnMed.Application.Exceptions;

public class HeadNotFoundException : NotFoundException
{
    public HeadNotFoundException()
    {
        this.TitleMessage = "Head not found!";
    }
}
