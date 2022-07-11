using System.Net;
using ApiBase.Controller;
using ApiBase.Controller.Response;
using AutoMapper;
using CDoc.Api.Objects;
using CDoc.Error;
using CDoc.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CDoc.Api.Controllers;

/// <summary>
///     Endpoints related to documents
/// </summary>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiError))]
public class DocumentController : APIController
{
    private readonly IDocumentProvider _documentProvider;
    private readonly IMapper _mapper;

    /// <summary>
    ///     DocumentInfo controller constructor
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="documentProvider"></param>
    public DocumentController(IMapper mapper, IDocumentProvider documentProvider)
    {
        _mapper = mapper;
        _documentProvider = documentProvider;
    }

    /// <summary>
    ///     Gets a document by path
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    [HttpGet("{repository}/{path}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Item>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult Get(string repository, string path)
    {
        var result = _mapper.Map<Item>(_documentProvider.Get(WebUtility.UrlDecode(repository), WebUtility.UrlDecode(path)));
        if (result == null)
        {
            return NotFound();
        }

        return Ok(new Response<Item>(result));
    }

    /// <summary>
    ///     List the document tree for a specific path
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    [HttpGet("[action]/{repository}/{path}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<Item>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult List(string repository, string path)
    {
        var result = _mapper.Map<List<Item>>(_documentProvider.Ls(WebUtility.UrlDecode(repository), WebUtility.UrlDecode(path)));
        if (result == null)
        {
            return NotFound();
        }

        return Ok(new Response<List<Item>>(result));
    }

    /// <summary>
    ///     Returns the details for a specific supported document
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    [HttpGet("[action]/{repository}/{path}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Preview>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult Preview(string repository, string path)
    {
        var result = _mapper.Map<Preview>(_documentProvider.Preview(WebUtility.UrlDecode(repository), WebUtility.UrlDecode(path)));
        if (result == null)
        {
            return NotFound();
        }
        return Ok(new Response<Preview>(result));
    }

    /// <summary>
    ///     Returns the details for a specific supported document
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    [HttpGet("[action]/{repository}/{path}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Preview>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetRawData(string repository, string path)
    {
        var fileInfo = _documentProvider.GetFileInfo(WebUtility.UrlDecode(repository), WebUtility.UrlDecode(path));
        if (fileInfo == null)
        {
            return NotFound();
        }
        return File(fileInfo.OpenRead(), "application/binary");
    }
}