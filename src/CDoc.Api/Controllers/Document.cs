using System.Net;
using ApiBase.Controller;
using ApiBase.Controller.Response;
using AutoMapper;
using CDoc.Api.Objects;
using CDoc.Process;
using Microsoft.AspNetCore.Mvc;

namespace CDoc.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiError))]
public class DocumentInfo : APIController
{
    private readonly IMapper _mapper;
    private readonly IDocumentProvider _documentProvider;

    public DocumentInfo(IMapper mapper, IDocumentProvider documentProvider)
    {
        _mapper = mapper;
        _documentProvider = documentProvider;
    }

    [HttpGet("List/{path}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<Item>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult List(string path)
    {
        List<Item> result = null;
        if (result == null)
        {
            return NotFound();
        }
        return Ok(new Response<List<Item>>());
    } 
}