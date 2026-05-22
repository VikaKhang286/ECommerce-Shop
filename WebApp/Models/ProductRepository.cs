using System.Data;
using Dapper;
namespace WebApp.Models;

public class ProductRepository : BaseRepository
{

    public ProductRepository(IDbConnection connection) : base(connection) { }
    public IEnumerable<Product> GetProducts()
    {
        return connection.Query<Product>("SELECT * FROM Product JOIN Brand ON Product.BrandId = Brand.BrandId");
    }
    public IEnumerable<Product> GetProductsByBrand(short brandId)
    {
        string sql = @"SELECT * FROM Product JOIN Brand ON Product.BrandId = Brand.BrandId WHERE Product.BrandId = @brandId";
        return connection.Query<Product>(sql, new { brandId });
    }
    public Product? GetProductDetail(int id)
    {
        string sql = @"SELECT * FROM Product JOIN Brand ON Product.BrandId = Brand.BrandId WHERE ProductId = @id";
        return connection.QueryFirstOrDefault<Product>(sql, new { id });
    }
    public IEnumerable<Product> GetProducts(int page = 1, int pageSize = 8)
    {
        int skip = (page - 1) * pageSize;
        string sql = @"SELECT * FROM Product JOIN Brand ON Product.BrandId = Brand.BrandId ORDER BY Product.ProductId DESC OFFSET @skip ROWS FETCH NEXT @pageSize ROWS ONLY";
        return connection.Query<Product>(sql, new { skip, pageSize });
    }
    public IEnumerable<Product> GetProductsByBrand(short brandId, int page = 1, int pageSize = 8)
    {
        int skip = (page - 1) * pageSize;
        string sql = @"SELECT * FROM Product JOIN Brand ON Product.BrandId = Brand.BrandId WHERE Product.BrandId = @brandId ORDER BY Product.ProductId DESC OFFSET @skip ROWS FETCH NEXT @pageSize ROWS ONLY";
        return connection.Query<Product>(sql, new { brandId, skip, pageSize });
    }
    public int CountProducts()
    {
        return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Product");
    }
    public int CountProductsByBrand(short brandId)
    {
        return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Product WHERE BrandId=@brandId", new { brandId });
    }
    public IEnumerable<Product> SearchProducts(string? keyword, string? sort, int page, int pageSize, out int totalProducts)
    {
        int skip = (page - 1) * pageSize;
        string where = "";
        if (!string.IsNullOrEmpty(keyword))
        {
            where = "WHERE Title LIKE @keyword";
        }
        string orderBy = sort switch
        {
            "price_desc" => "ORDER BY Price DESC",
            "price_asc" => "ORDER BY Price ASC",
            "title_asc" => "ORDER BY Title ASC",
            "title_desc" => "ORDER BY Title DESC",
            _ => "ORDER BY Title ASC"
        };
        string countSql = $@"SELECT COUNT(*) FROM Product {where}";
        totalProducts = connection.ExecuteScalar<int>(countSql, new { keyword = $"%{keyword}%" });
        string sql = $@"SELECT * FROM Product JOIN Brand ON Product.BrandId = Brand.BrandId {where} {orderBy} OFFSET @skip ROWS FETCH NEXT @pageSize ROWS ONLY";
        return connection.Query<Product>(sql, new { keyword = $"%{keyword}%", skip, pageSize });
    }
    public void Insert(Product model)
    {
        string sql = @"INSERT INTO Product (BrandId, Title, ImageUrl, Description, Price, SaleOff) VALUES(@BrandId, @Title, @ImageUrl, @Description, @Price, @SaleOff)";
        connection.Execute(sql, model);
    }
    public void Update(Product model)
    {
        string sql = @"UPDATE Product SET BrandId = @BrandId, Title = @Title, ImageUrl = @ImageUrl, Description = @Description, Price = @Price, SaleOff = @SaleOff WHERE ProductId = @ProductId";
        connection.Execute(sql, model);
    }
    public void Delete(int id)
    {
        connection.Execute("DELETE FROM Product WHERE ProductId=@id",new { id });
    }
    public Product? GetById(int id)
    {
        return connection.QueryFirstOrDefault<Product>(@"SELECT * FROM Product JOIN Brand ON Product.BrandId = Brand.BrandId WHERE ProductId=@id", new { id });
    }
}