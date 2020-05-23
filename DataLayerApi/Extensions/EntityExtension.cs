using DataLayerApi.Models.Entities.Core;
using System;

namespace DataLayerApi.Models.Dtos.V10.Core
{
    public static class EntityExtension
    {
        [Obsolete("Neni optimalni", true)]
        public static E ToEntityItemEnum<E>(this EnumItem enumItem) where E : struct, Enum
        {
            return Enum.Parse<E>(enumItem.NormalizedName);
        }

        public static E ToEntityItemEnum<E>(this IDtoEnumItem enumItem) where E : struct, Enum
        {
            return Enum.Parse<E>(enumItem.NormalizedName);
        }

        public static E? ToEntityItemEnum<E>(this int? enumItemId) where E : struct, Enum
        {
            if (!enumItemId.HasValue) return null;

            return enumItemId.Value.ToEntityItemEnum<E>();
        }

        public static E ToEntityItemEnum<E>(this int enumItemId) where E : struct, Enum
        {
            if (!Enum.IsDefined(typeof(E), enumItemId))
                throw new ArgumentOutOfRangeException(nameof(enumItemId), $"For Enum {typeof(E).Name} not exist value for value {enumItemId}");

            return Enum.Parse<E>(enumItemId.ToString());
        }

        public static int? ToEntityNullableId(this Enum @enum)
        {
            if (@enum == null) return null;
            return Convert.ToInt32(@enum);
        }

        public static int ToEntityId(this Enum @enum)
        {
            return Convert.ToInt32(@enum);
        }
    }
}
