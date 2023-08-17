namespace OnMed.Application.Exceptions.Administrators;

public class AdminNotFoundException : NotFoundException
{
    public AdminNotFoundException()
    {
        this.TitleMessage = "Admin not found";
    }
}