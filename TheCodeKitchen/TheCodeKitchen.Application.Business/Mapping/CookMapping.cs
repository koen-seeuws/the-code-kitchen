using TheCodeKitchen.Application.Contracts.Response.Cook;

namespace TheCodeKitchen.Application.Business.Mapping;

public class CookMapping : Profile
{
    public CookMapping()
    {
        CreateMap<Cook, CreateCookResponse>();
        CreateMap<Cook, GetCookResponse>();
    }
}