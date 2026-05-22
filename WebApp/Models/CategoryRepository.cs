using System.Data;
using Dapper;

namespace WebApp.Models;

public class CategoryRepository : BaseRepository
{
    public CategoryRepository(IDbConnection connection) : base(connection) { }
    public IEnumerable<Category> GetCategories()
    {
        return connection.Query<Category>("SELECT CategoryId AS Id, CategoryName, Icon FROM Category");
    }
    public IEnumerable<Category> GetCategoriesWithIcon(){
        return connection. Query<Category>("SELECT CategoryId AS Id, CategoryName, Icon FROM Category WHERE Icon IS NOT NULL");
    }
}