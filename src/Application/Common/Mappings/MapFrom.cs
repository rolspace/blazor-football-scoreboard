using AutoMapper;

namespace Football.Application.Common.Mappings;

//TODO: change to something else, I do not like this interface with a default method. Looks dirty.
public abstract class MapFrom<T>
{
    public void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
