namespace WebApp.Areas.Api.Models;

public class SiteProvider : BaseProvider
{
    public SiteProvider(IConfiguration configuration) : base(configuration) { }
    MemberRepository? member;
    public MemberRepository Member => member ??= new MemberRepository(Connection);
}