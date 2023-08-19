namespace OnMed.Persistance.Validators;

public class DataOnlyValidator
{
    public static bool IsValid(string data)
    {
        if(data.Length < 10) return false;

        for (int i = 1; i < data.Length; i++)
        {
            if(i == 2 || i == 5)
            {
                if (data[i] == '-') continue;
                else return false;
            }
            else if (char.IsDigit(data[i])) continue;
            else return false;
        }

        return true;
    }
}
