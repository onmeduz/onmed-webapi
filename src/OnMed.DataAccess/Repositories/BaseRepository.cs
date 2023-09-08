using Aspose.Pdf;
using Dapper;
using Npgsql;
using OnMed.DataAccess.Handlers;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace OnMed.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository()
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=on-med-db; User Id=postgres; Password=root;");
    }
}
    