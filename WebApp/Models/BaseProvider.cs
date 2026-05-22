using System.Data;
using Microsoft.Data.SqlClient;

namespace WebApp.Models;

public abstract class BaseProvider {
    string connectionString;
    public BaseProvider(IConfiguration configuration) {
        connectionString = configuration.GetConnectionString("EShop") ?? throw new EntryPointNotFoundException("Not found EShop");
    }
    IDbConnection? connection;
    protected IDbConnection Connection => connection ??= new SqlConnection(connectionString);
}