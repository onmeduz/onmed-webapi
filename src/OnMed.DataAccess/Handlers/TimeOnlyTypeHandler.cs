using Dapper;
using System.Data;

namespace OnMed.DataAccess.Handlers;

public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override TimeOnly Parse(object value)
    {
        var stringVal = value.ToString();
        if (stringVal is null) return new TimeOnly();
        var dateTime = DateTime.Parse(stringVal);

        return TimeOnly.FromDateTime(dateTime);
    }

    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.Value = new TimeOnly(value.Hour, value.Minute);
    }
}
