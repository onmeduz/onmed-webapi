namespace OnMed.Persistance.Validators;

public class PasswordValidator
{

    public static (bool IsValid, string Message) IsStrongPassword(string password)
    {
        if (password.Length < 8) return (IsValid: false, Message: "Password can not be less than 8 characters!");

        return (IsValid: true, "");
    }
}
