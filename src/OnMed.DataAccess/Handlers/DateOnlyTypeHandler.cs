using Dapper;
using System.Data;

namespace OnMed.DataAccess.Handlers;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value)
    {
        var stringVal = value.ToString();
        if (stringVal is null) return new DateOnly();
        var dateTime = DateTime.Parse(stringVal);

        return DateOnly.FromDateTime(dateTime);
    }

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = new DateTime(value.Year, value.Month, value.Day);
    }

}
