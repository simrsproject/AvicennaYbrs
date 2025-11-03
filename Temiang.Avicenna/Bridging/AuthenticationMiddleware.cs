using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using Temiang.Avicenna.BusinessObject;
using System;
using Microsoft.Owin;
using System.Text;
using System.Security.Cryptography;

namespace Temiang.Avicenna.Bridging
{
    public class AuthenticationMiddleware : OwinMiddleware
    {
        public AuthenticationMiddleware(OwinMiddleware next) : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            var request = context.Request;
            var token = request.Headers["x-token"];
            if (String.IsNullOrWhiteSpace(token))
            {
                await Next.Invoke(context);
                return;
            }
            var response = context.Response;

            response.OnSendingHeaders(state =>
            {
                var resp = (OwinResponse)state;

                if (resp.StatusCode == 401)
                {
                    resp.Headers.Add("WWW - Authenticate", new[] { "Basic" });
                    resp.StatusCode = 403;
                    resp.ReasonPhrase = "Forbidden";
                }
            }, response);


            token = string.Format("basic {0}", token);

            var authHeader = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(token);

            //if ("Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
            //{

            var parameter = ":";
            try
            {
                parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter));
            }
            catch (Exception ex)
            {
            }


            var parts = parameter.Split(':');

            // Check token value
            if (parts.Length == 5)
            {
                var username = parts[1];
                var password = parts[2];

                // Check Signature
                if (GetXSignature(username, password).Equals(parts[4]))
                {
                    // Check expire
                    if (parts[3].ToInt() > GetUnixTimeStamp(0).ToInt())
                    {
                        // Check User Password
                        if (ValidateUser(username, password))
                        {
                            var claims = new[] { new Claim(ClaimTypes.Name, username) };
                            var identity = new ClaimsIdentity(claims, "Basic");
                            request.User = new ClaimsPrincipal(identity);
                        }
                    }
                }
            }
            //}

            await Next.Invoke(context);
        }

        internal static bool ValidateUser(string username, string password)
        {
            var wsAk = new WebServiceAccessKey();
            wsAk.Query.Where(wsAk.Query.AccessKey == password, wsAk.Query.ClientCode == username);
            wsAk.Query.es.Top = 1;
            if (wsAk.Query.Load() && wsAk.StartDate <= DateTime.Now && (wsAk.EndDate == null || wsAk.EndDate >= DateTime.Now))
            {
                return true;
            }
            return false;
        }

        internal static string GetXSignature(string userName, string password)
        {
            // Initialize the keyed hash object using the secretkey as the key
            HMACSHA256 hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(password));

            // Computes the signature by hashing the salt withthe secret key as the key
            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(userName));

            // Base 64 Encode
            var encodedSignature = System.Convert.ToBase64String(signature);

            return encodedSignature;
        }

        internal static string GetUnixTimeStamp(int addMinute)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.Now.AddMinutes(addMinute).ToUniversalTime() - dateStart;
            return Math.Floor(diff.TotalSeconds).ToString();
        }
    }
}