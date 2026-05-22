using System.Data;
using Dapper;
using WebApp.Areas.Api.Services;

namespace WebApp.Areas.Api.Models;

public class MemberRepository : BaseRepository
{
    public MemberRepository(IDbConnection connection) : base(connection) { }
    public int UpdateRole(MemberRole obj){
        string sql = "UPDATE Member SET Role = @Role WHERE MemberId = @Id";
        return connection.Execute(sql, obj);
    }
    public IEnumerable<Member> GetMembers()
    {
        string sql = "SELECT MemberId, Email, GivenName, Surname, RegisterDate, LoginDate, Role FROM Member";
        return connection.Query<Member>(sql);
    }
    public Member? GetMember(string id)
    {
        string sql = "SELECT MemberId, Email, GivenName, Surname, RegisterDate, LoginDate, Role FROM Member WHERE MemberId = @Id";
        return connection.QuerySingleOrDefault<Member>(sql, new {id});
    }
    public int Save(RegisterModel obj)
    {
        if (string.IsNullOrEmpty(obj.MemberId)) obj.MemberId = Guid.NewGuid().ToString().Replace("-", "");
        return connection.Execute("SaveMember", new
        {
            obj.MemberId,
            obj.Email,
            Password = Helper.Hash(obj.Password),
            obj.GivenName,
            obj.Surname,
            obj.Role
        }, commandType: CommandType.StoredProcedure);
    }
    public Member? GetMember(LoginModel obj){
        string sql = "SELECT MemberId, Email, GivenName, Surname, RegisterDate, LoginDate, Role FROM Member WHERE Email = @Email AND Password = @Password";
        return connection.QuerySingleOrDefault<Member>(sql, new { obj.Email, Password = Helper.Hash(obj.Password)});
    }
}