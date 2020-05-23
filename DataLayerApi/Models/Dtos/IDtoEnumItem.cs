using DataLayerApi.Models.Base;

namespace DataLayerApi.Models.Dtos
{
    public interface IDtoEnumItem : IEnumItem<int?, bool?>, IDtoIdentity
    {
    }
}
