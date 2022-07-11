using AutoMapper;
using CDoc.Api.Objects;

namespace CDoc.Api;

/// <summary>
/// Profile for AutoMapper
/// </summary>
public class MapperProfile : Profile
{
    /// <summary>
    /// Creation of the mapping rules
    /// </summary>
    public MapperProfile() {
        CreateMap<Process.Objects.Item, Item>();
        CreateMap<Process.Objects.Preview, Preview>();
        CreateMap<Config.Source, CDoc.Api.Objects.Source>();
    }
}