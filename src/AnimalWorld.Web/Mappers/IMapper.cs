namespace AnimalWorld.Web.Mappers
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);

        List<TDestination> MapList(List<TSource> source);
    }
}
