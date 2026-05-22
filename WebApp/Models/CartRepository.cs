using System.Data;
using Dapper;

namespace WebApp.Models;

public class CartRepository : BaseRepository {

    public CartRepository(IDbConnection connection) : base(connection) {
    }
    public IEnumerable<Cart> GetCarts(string memberId)
    {
        string sql = @"SELECT Cart.*, Title, Price, SaleOff, ImageUrl FROM Cart JOIN Product ON Cart.ProductId = Product.ProductId WHERE Cart.MemberId = @memberId";
        return connection.Query<Cart>(sql, new { memberId });
    }

    public int Save(string memberId, int productId, short quantity){
        return connection.Execute("SaveCart", new {MemberId = memberId, ProductId = productId, Quantity = quantity}, commandType: CommandType.StoredProcedure);
    }
    public int Count(string memberId){
        return connection.QuerySingleOrDefault<int>("SELECT ISNULL(SUM(Quantity), 0) FROM Cart WHERE MemberId = @MemberId", new {memberId});
    }
    public void Delete(Guid cartId)
    {
        connection.Execute("DELETE FROM Cart WHERE CartId = @cartId", new { cartId });
    }
    public void UpdateQuantity(Guid cartId, short quantity)
    {
        connection.Execute(@"UPDATE Cart SET Quantity = @quantity, UpdatedDate = GETDATE() WHERE CartId = @cartId", new { cartId, quantity });
    }
}