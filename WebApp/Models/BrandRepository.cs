using System.Data;
using Dapper;

namespace WebApp.Models;


public class BrandRepository : BaseRepository
{
    public BrandRepository(IDbConnection connection) : base(connection)
    {
    }
    public IEnumerable<Brand> GetBrands()
    {
        return connection.Query<Brand>("SELECT BrandId, CategoryId AS ParentId, BrandName, ItemCount FROM Brand");
    }
    public IEnumerable<Brand> GetShops(){
        return connection.Query<Brand>("SELECT * FROM Brand WHERE Logo IS NOT NULL");
    }
}