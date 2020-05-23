

namespace DataLayerApi.Models.Dtos.V10.UserManagement
{
    /// <summary>
    /// Položky tabulky Roles.
    /// </summary>
    public enum ERoles
    {
        /// <summary>
        /// Administrator - Oprávnění admina.
        /// </summary>
        Admin = 2
,

        /// <summary>
        /// Všichni uživatelé - Skupina všech uživatelů.
        /// </summary>
        AllUsers = 1
,

        /// <summary>
        /// Zákazník - Zákazník.
        /// </summary>
        Customer = 4
,

        /// <summary>
        /// Host - Host.
        /// </summary>
        Guest = 6
,

        /// <summary>
        /// Prodejce - Prodejce zboží.
        /// </summary>
        Seller = 3
,

        /// <summary>
        /// Dodavatel - Dodavatel zboží.
        /// </summary>
        Supplier = 7
,

        /// <summary>
        /// OvěřenýZákazník - Ověřený zákazník.
        /// </summary>
        VerifiedCustomer = 5

    }

}
