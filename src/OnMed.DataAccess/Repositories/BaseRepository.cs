using Dapper;
using Npgsql;
using OnMed.DataAccess.Handlers;

namespace OnMed.DataAccess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;
    public BaseRepository()
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        //this._connection = new NpgsqlConnection("Host=db-postgresql-sgp1-13928-do-user-14592202-0.b.db.ondigitalocean.com; Port=25060; Database=on-med-db; User Id=doadmin; Password=AVNS_WldvPCfwbFLcbGzkUxV;");
        this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=clone-onmed; User Id=postgres; Password=19969;");

    }
}
