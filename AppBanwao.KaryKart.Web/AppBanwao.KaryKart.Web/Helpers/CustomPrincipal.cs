using AppBanwao.KarryKart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AppBanwao.KaryKart.Web.Helpers
{
    public interface ICustomPrincipal : System.Security.Principal.IPrincipal
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string EmailAddress { get; set; }

        bool ProfileComplete { get; set; }

       // string MarketID { get; set; }

        //string MarketName { get; set; }

//        List<string> Permisssions { get; set; }

  //      bool IsDefaultUser { get; set; }
    }
    public class CustomPrincipal :IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            var dbContext = new karrykartEntities();
           
            var isRoleExists = Identity != null && Identity.IsAuthenticated &&
               !string.IsNullOrWhiteSpace(role) && (dbContext.Users.Where(x=>x.EmailAddress==Identity.Name||x.Mobile==Identity.Name).FirstOrDefault().Role.RoleName.Contains(role));
            dbContext = null;
 
            return isRoleExists;

        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        
        public bool ProfileComplete { get; set; }
        
        public string FullName { get { return FirstName + " " + LastName; } }

    }

    public class CustomPrincipalSerialize
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public bool ProfileComplete { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }
       
    }
}