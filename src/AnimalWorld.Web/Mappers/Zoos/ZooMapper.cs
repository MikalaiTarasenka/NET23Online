using AnimalWorld.Core.Dtos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces.CustomMappers;
using AnimalWorld.Web.Models.Zoos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Mappers.Zoos
{
    public class ZooMapper : IZooMapper
    {
        public ZooViewModel Map(ZooData source)
        {
            return new ZooViewModel
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Address = source.Address
            };
        }

        public ZooData ReverseMap(ZooViewModel destination)
        {
            return new ZooData
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                Address = destination.Address
            };
        }

        public List<SelectListItem> ToSelectListItems(List<ZooData> source)
        {
            var selectZoosList = source.Select(zoo => new SelectListItem
            {
                Text = zoo.Name,
                Value = zoo.Id.ToString()
            }).ToList();
            return selectZoosList;
        }

        public ZooListViewModel ToPagedZoos(PagedResult<ZooDto> source)
        {
            return new ZooListViewModel
            {
                Zoos = source.Items.Select(zoo => new ZooViewModel
                {
                    Id = zoo.Id,
                    Name = zoo.Name,
                    Address = zoo.Address,
                    Description = zoo.Description,
                    AnimalFamilies = zoo.AnimalFamilies
                }).ToList(),
                HasNextPage = source.HasNextPage,
                CurrentPage = source.CurrentPage,
                HasPreviousPage = source.HasPreviousPage,
                PageNumbers = source.PageNumbers,
                TotalPages = source.TotalPages,
            };
        }
    }
}
