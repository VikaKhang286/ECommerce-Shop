using System.Data;
using Dapper;
using WebApp.Services;

namespace WebApp.Models;

public class MemberRepository : BaseRepository
{
    Uri uri = new Uri("http://localhost:5298");
    public MemberRepository(IDbConnection connection) : base(connection)
    {
    }
     public int Save(Member obj)
    {
        return connection.Execute("SaveMember", obj, commandType: CommandType.StoredProcedure);
    }
    public async Task<IEnumerable<Member>?> GetMembers()
    {
        using (HttpClient client = new HttpClient { BaseAddress = uri })
        {
            HttpResponseMessage message = await client.GetAsync("/api/member");
            if (message. IsSuccessStatusCode && message.StatusCode != System.Net.HttpStatusCode.NoContent){
                return await message.Content.ReadFromJsonAsync<IEnumerable<Member>>();
            }
            return null;
        }
    }
    public Member? Login(LoginModel model)
    {
        string sql = @"SELECT MemberId, Email, GivenName, Surname, LoginDate, RegisterDate, Role FROM Member WHERE Email = @Email AND Password = @Password";
        // Console.WriteLine(Helper.Hash(model.Password));
        return connection.QueryFirstOrDefault<Member>(sql, new { Email = model.Email, Password = Helper.Hash(model.Password) });
    }
    public void Register(RegisterModel model)
    {
        connection.Execute("SaveMember", new
            {
                MemberId = Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                Email = model.Email,
                Password = Helper.Hash(model.Password),
                GivenName = model.GivenName,
                Surname = model.Surname,
                Role = 0
            },
            commandType: CommandType.StoredProcedure
        );
    }
}