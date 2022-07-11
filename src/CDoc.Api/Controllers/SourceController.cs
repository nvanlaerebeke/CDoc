using ApiBase.Controller;
using ApiBase.Controller.Response;
using AutoMapper;
using CDoc.Config;
using CDoc.Error;
using CDoc.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDoc.Api.Controllers;

/// <summary>
/// Endpoints related to sources 
/// </summary>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiError))]
public class SourceController: APIController
{
    private readonly IMapper _mapper;
    private readonly ISourceActions _sourceActions;

    /// <summary>
    /// DocumentInfo controller constructor
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="documentProvider"></param>
    public SourceController(IMapper mapper, ISourceActions documentProvider)
    {
        _mapper = mapper;
        _sourceActions = documentProvider;
    }

    /// <summary>
    /// Syncs all the sources
    /// </summary>
    /// <returns></returns>
    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult SyncAll()
    {
        _sourceActions.SyncAll();
        return Ok();
    } 
    
    /// <summary>
    /// Syncs all the sources
    /// </summary>
    /// <returns></returns>
    [HttpGet("[action]/{repository}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult Sync(string repository)
    {
        try
        {
            _sourceActions.Sync(repository);
            return Ok();
        }
        catch (CDocException ex)
        {
            if (ex.GetError().Code == ApiErrorCode.FileNotFound)
            {
                return NotFound();
            }
            throw;
        }
    } 
    
    /// <summary>
    /// Returns all the sources
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<Objects.Source>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult GetAll()
    {
        return Ok(
            new Response<List<Api.Objects.Source>>(
                _mapper.Map<List<Api.Objects.Source>>(new Settings().GetConfig().Sources) ?? new List<Objects.Source>()
            )
        );
    } 
}