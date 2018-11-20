namespace Biob.Services.Web.PropertyMapping
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
