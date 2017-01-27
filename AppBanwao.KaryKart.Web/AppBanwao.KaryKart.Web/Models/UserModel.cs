using AppBanwao.KarryKart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AppBanwao.KaryKart.Web.Models
{
    public class UserModel
    {
        public Guid UserID { get; set; }

        public string EmailAddress { get; set; }

        public string Mobile { get; set; }

        public DateTime Datecreated { get; set; }

        public DateTime LastUpdated { get; set; }

        public int RoleID { get; set; }

        public bool Active { get; set; }

        public bool ProfileComplete { get; set;}

        public int UserDetailsID { get; set; }
        
        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AddressID { get; set; }
        
        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }
        
        public Nullable<int> CityID { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string Pincode { get; set; }
        public string Landmark { get; set; }

        public List<Country> countries { get; set; }

        public List<refSaluation> salutations { get; set; }

        public List<refCity> cities { get; set; }

        public List<refState> states { get; set; }

    }
}