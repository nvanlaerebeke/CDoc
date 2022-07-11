using ApiBase.Error;
using Microsoft.AspNetCore.Mvc;

namespace Markdown.Api;

public class Startup : ApiBase.ApiBase
{
    public Startup(IConfiguration configuration) : base(configuration) { }

    protected override void ConfigureApi(IServiceCollection services) {
       _ = services.AddAutoMapper(typeof(Startup));
    }

    protected override bool ConfigureOpenAPI(IServiceCollection services) {
        return false;
    }

    protected override ApiVersion DefaultVersion() {
        return new ApiVersion(1, 0);
    }

    protected string GetName() {
        return "Sample API";
    }

    protected override IBadRequestErrorCodeProvider GetBadRequestErrorProvider(){
        return new BadRequestErrorCodeProvider();
    }
}