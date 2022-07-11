using ApiBase.Error;
using CDoc.Process;
using Microsoft.AspNetCore.Mvc;

namespace CDoc.Api;

public class Startup : ApiBase.ApiBase
{
    public Startup(IConfiguration configuration) : base(configuration) { }

    protected override void ConfigureApi(IServiceCollection services) {
       _ = services.AddAutoMapper(typeof(Startup));
       _ = services.AddSingleton<IDocumentProvider>(new ActionProvider().GetDocumentProvider());
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