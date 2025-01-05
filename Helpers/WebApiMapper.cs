using AutoMapper;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class WebApiMapper : Profile
    {
        public WebApiMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
