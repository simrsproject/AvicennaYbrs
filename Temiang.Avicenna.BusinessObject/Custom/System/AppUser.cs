using System;
using System.Data.SqlClient;
using System.Reflection;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppUser
    {
        public class UserType
        {
            public const string Administration = "ADM";
            public const string Doctor = "DTR";
            public const string Nurse = "NRS";
            public const string Nutrition = "NUT";
            public const string Physiotherapy = "PHY";
            public const string Pharmacy = "FAR";
            public const string Rehabilitation = "RHB";
            public const string Registration = "REG";
        }

        public static string GetUserName(string userID)
        {
            if (String.IsNullOrWhiteSpace(userID)) return String.Empty;

            var qr = new AppUserQuery("usr");
            qr.Select(qr.UserName);
            qr.Where(qr.UserID == userID);

            var usr = new AppUser();
            if (usr.Load(qr))
                return usr.UserName;

            return string.Empty;
        }
        public static string GetParamedicName(string userID)
        {
            if (String.IsNullOrWhiteSpace(userID)) return String.Empty;

            var user = new AppUser();
            if (user.LoadByPrimaryKey(userID) && !string.IsNullOrWhiteSpace(user.ParamedicID))
            {
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(user.ParamedicID))
                    return par.ParamedicName;
            }
            return String.Empty;
        }
    }
}
