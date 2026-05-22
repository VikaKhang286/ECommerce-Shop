namespace WebApp.Models;

public class SiteProvider : BaseProvider
{
    public SiteProvider(IConfiguration configuration) : base(configuration) { }
    CategoryRepository? category;
    public CategoryRepository Category => category ??= new CategoryRepository(Connection);
    BrandRepository? brand;
    public BrandRepository Brand => brand ??= new BrandRepository(Connection);
    ProductRepository? product;
    public ProductRepository Product => product ??= new ProductRepository(Connection);
    MemberRepository? member;
    public MemberRepository Member => member ??= new MemberRepository(Connection);
    CartRepository? cart;
    public CartRepository Cart => cart ??= new CartRepository(Connection);
    RoleRepository? role;
    public RoleRepository Role => role ??= new RoleRepository(Connection);
}