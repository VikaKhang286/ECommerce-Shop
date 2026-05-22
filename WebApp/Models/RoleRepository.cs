using System.Data;

namespace WebApp.Models;

public class RoleRepository : BaseRepository
{
    Uri uri = new Uri("http://localhost:5298");
    public RoleRepository(IDbConnection connection) : base(connection)
    {
    }
     public async Task<IEnumerable<RoleType>?> GetRolesAsync()
    {
        using (HttpClient client = new HttpClient { BaseAddress = uri })
        {
            return await client.GetFromJsonAsync<IEnumerable<RoleType>>("/api/role");
        }
    }
}