namespace AnimalWorld.Web.Mappers.Interfaces
{
    public interface IReverseMapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        TSource ReverseMap(TDestination destination);

        List<TSource> ReverseMapList(List<TDestination> destination)
        {
            return destination
                .Select(ReverseMap)
                .ToList();
        }
    }
}
