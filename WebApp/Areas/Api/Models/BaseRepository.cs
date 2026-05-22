using System.Data;

namespace WebApp.Areas.Api.Models;

public abstract class BaseRepository {
    protected IDbConnection connection;
    public BaseRepository(IDbConnection connection) => this.connection = connection;
}