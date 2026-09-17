using AnimalWorld.Core.Dtos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Models.Zoos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Mappers.Interfaces.CustomMappers
{
    public interface IZooMapper : IReverseMapper<ZooData, ZooViewModel>
    {
        public List<SelectListItem> ToSelectListItems(List<ZooData> source);
        public ZooListViewModel ToPagedZoos(PagedResult<ZooDto> source);
    }
}
