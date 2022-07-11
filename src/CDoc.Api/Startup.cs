using ApiBase.Error;
using CDoc.Error;
using CDoc.Process;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CDoc.Api;

/// <summary>
/// Api startup class
/// </summary>
public class Startup : ApiBase.ApiBase
{
    /// <summary>
    /// Startup constructor
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration) : base(configuration) { }
    
    /// <summary>
    /// Configures the Web Api services
    /// </summary>
    /// <param name="services"></param>
    protected override void ConfigureApi(IServiceCollection services) {
       _ = services.AddAutoMapper(typeof(Startup));

       var serviceProvider = new ActionProvider();
       _ = services.AddSingleton(serviceProvider.GetDocumentProvider());
       _ = services.AddSingleton(serviceProvider.GetSourceActions());

       CDoc.Process.Startup.Start();
    }

    /// <summary>
    /// Configures the OpenApi (swagger) documentation
    ///
    /// Returns true to enable the document generation
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    protected override bool ConfigureOpenAPI(IServiceCollection services) {
        return true;
    }

    /// <summary>
    /// Returns the default Api version to serve
    /// </summary>
    /// <returns></returns>
    protected override ApiVersion DefaultVersion() {
        return new ApiVersion(1, 0);
    }

    /// <summary>
    /// Returns the name of the Api that will be shown in the documentation
    /// </summary>
    /// <returns></returns>
    protected string GetName() {
        return "CDoc API";
    }

    /// <summary>
    /// Returns the class that will be used to provide error messages for invalid input properties
    /// </summary>
    /// <returns></returns>
    protected override IBadRequestErrorCodeProvider GetBadRequestErrorProvider(){
        return new BadRequestErrorCodeProvider();
    }
    
    /// <summary>
    /// Returns the name of the Xml file that contains the class and method documentation
    /// </summary>
    /// <returns></returns>
    protected override string GetAPIXmlDescriptionsFileName() {
        return "API.xml";
    }
}