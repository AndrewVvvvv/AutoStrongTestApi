using AutoMapper;
using AutoStrongTestApi.Dto;
using AutoStrongTestApi.Models;

namespace AutoStrongTestApi.MapperProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
        }
    }
}
