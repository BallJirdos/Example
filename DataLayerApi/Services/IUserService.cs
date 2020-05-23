using DataLayerApi.Models.Dtos.V10.UserManagement;
using DataLayerApi.Models.Entities.UserManagement;
using System.Collections.Generic;
using System.Linq;

namespace DataLayerApi.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Overeni uzivatele
        /// </summary>
        /// <param name="username">Uzivatelske jmeno</param>
        /// <param name="password">Heslo</param>
        /// <returns>Uzivatel</returns>
        AuthenticateResult Authenticate(string username, string password);

        /// <summary>
        /// Odhlásit stávajícího uživatele
        /// </summary>
        void SignOut();

        /// <summary>
        /// Aktualizovat token
        /// </summary>
        /// <returns></returns>
        AuthenticateResult RefreshToken();

        /// <summary>
        /// Prihlaseny uzivatel
        /// </summary>
        User User { get; }

        /// <summary>
        /// Získat ID uživatele
        /// </summary>
        /// <returns></returns>
        int GetUserId();

        /// <summary>
        /// Založit uživatele a automaticky přiřadit do skupiny guest
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>ID uživatele</returns>
        int CreateUser(UserRegistrationModel userModel);

        /// <summary>
        /// Zapomenuté heslo
        /// </summary>
        /// <param name="model">Model pro zapomenuté heslo</param>
        void ForgotPassword(ForgotPasswordModel model);

        /// <summary>
        /// Reset hesla
        /// </summary>
        /// <param name="model">Model pro reset hesla</param>
        void ResetPassword(ResetPasswordModel model);

        /// <summary>
        /// Získat nacachované role (pro aktuálního uživatele)
        /// </summary>
        /// <returns>Role</returns>
        IEnumerable<int> GetCachedRoles();

        /// <summary>
        /// Získat skupiny uživatele
        /// </summary>
        /// <param name="userId">ID uživatele, pro kterého chceme zobrazit práva (nepovinné, pokud je null, zobrazíme práva aktuálího uživatele)</param>
        /// <returns>Dostupné skupiny</returns>
        IQueryable<int> GetRoles(int? userId = null);

        /// <summary>
        /// Získat skupiny uživatele
        /// </summary>
        /// <param name="userName">Uživatelské jméno uživatele, pro kterého chceme zobrazit práva (nepovinné, pokud je null, zobrazíme práva aktuálího uživatele)</param>
        /// <returns>Dostupné skupiny</returns>
        IQueryable<int> GetRoles(string userName);
    }
}
