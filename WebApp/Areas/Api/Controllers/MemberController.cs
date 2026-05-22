using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Api.Models;

namespace WebApp.Areas.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MemberController : BaseController
{
    public IEnumerable<Member> GetMembers()
    {
        return Provider.Member.GetMembers();
    }
    [HttpGet("{id}")]
    public Member? GetMember(string id)
    {
        return Provider.Member.GetMember(id);
    }
    [HttpPut("updaterole")]
        public int UpdateRole(MemberRole obj){
        return Provider.Member.UpdateRole(obj);
    }
}