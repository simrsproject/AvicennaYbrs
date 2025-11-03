using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService.AvicennaMobile
{
    /// <summary>
    /// Summary description for LoginWsv
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LoginWsv : V0.BaseDataService
    {

        [WebMethod]
        public void Login(string AccessKey, string UserID, string Password)
        {
            var log = LogAdd();
            try
            {
                ValidateAccessKey(AccessKey);

                InspectStringRequired(AppUserMetadata.ColumnNames.UserID, UserID);
                InspectStringRequired(AppUserMetadata.ColumnNames.Password, Password);

                var user = new AppUser();
                if (user.LoadByPrimaryKey(UserID))
                {
                    if (!user.Password.Equals(Encryptor.Encrypt(Password)))
                    {
                        throw new Exception("Password not accepted");
                    }
                    else if (user.ExpireDate.Value < DateTime.Now)
                    {
                        throw new Exception("User has expired");
                    }
                }
                else
                {
                    throw new Exception("User ID not accepted");
                }


                var query = new AppUserQuery();
                query.Where(query.UserID == UserID);

                var tbl = query.LoadDataTable();

                InspectOneResult(tbl);
                WriteResponseAndLog(log, JSonRetFormatted(ConvertDataRowtoObject(tbl.Rows[0])));

            }

            catch (Exception ex)
            {
                WriteResponseAndLog(log, JSonRetFormatted(GetErrorMessage(ex.Message), false, GetErrorCode(ex.Message)));
            }
        }
    }
}
