using System.Data;
using Microsoft.Data.SqlClient;

namespace WebApp.Areas.Api.Models;

public abstract class BaseProvider {
    IDbConnection? connection;
    IConfiguration configuration;
    public BaseProvider(IConfiguration configuration) => this.configuration = configuration;
    protected IDbConnection Connection => connection ??= new SqlConnection(configuration.GetConnectionString("EShop"));
}