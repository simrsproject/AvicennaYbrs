// Diganti menggunakan AuthenticationMiddleware

//using Microsoft.Owin.Security.OAuth;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Temiang.Avicenna.BusinessObject;
//using System;

//namespace Temiang.Avicenna.Bridging
//{
//    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
//    {
//        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
//        {
//            context.Validated();
//        }

//        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
//        {
//            using (UserAuthentication uat = new UserAuthentication())
//            {
//                if (!uat.ValidateUser(context.UserName, context.Password))
//                {
//                    context.SetError("invalid_grant", "Username or password is incorrect");
//                    return;
//                }
//                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
//                identity.AddClaim(new Claim(ClaimTypes.Role, "SuperAdmin"));
//                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
//                context.Validated(identity);
//            }
//        }

//        private class UserAuthentication : IDisposable
//        {
//            public bool ValidateUser(string username, string password)
//            {
//                var wsAk = new WebServiceAccessKey();
//                wsAk.Query.Where(wsAk.Query.AccessKey == password, wsAk.Query.ClientCode == username);
//                wsAk.Query.es.Top = 1;
//                if (wsAk.Query.Load() && wsAk.StartDate <= DateTime.Now && (wsAk.EndDate == null || wsAk.EndDate >= DateTime.Now))
//                {
//                    return true;
//                }
//                return false;
//            }
//            public void Dispose()
//            {
//                //Dispose();
//            }
//        }
//    }
//}