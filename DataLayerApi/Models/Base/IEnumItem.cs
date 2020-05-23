namespace DataLayerApi.Models.Base
{
    /// <summary>
    /// Výčtový typ
    /// </summary>
    public interface IEnumItem<E, B> : IIdentity<E>
    {
        string NormalizedName { get; set; }

        string Name { get; set; }

        string Title { get; set; }

        B IsEnabled { get; set; }

        E Order { get; set; }
    }
}
