using System;
using System.Web;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.MasterPage
{
    public partial class MasterBootstrap : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var par = new BusinessObject.AppParameter();
            if (par.LoadByPrimaryKey("HealthcareInitial"))
            {
                lblHCI.Text = par.ParameterValue;
            }
            var hc = BusinessObject.Healthcare.GetHealthcare();

            lblHCInit.Text = hc.HealthcareName;
            lblAddress.Text = string.Format("{0} {1} {2} PhoneNo: {3} Fax: {4}",
                hc.AddressLine1, hc.AddressLine2, hc.City, hc.PhoneNo, hc.FaxNo);

            var brand = new Brand();
            linkBrand.NavigateUrl = brand.Website;
            linkBrand.Text = brand.VendorName;

            if (string.IsNullOrEmpty(Page.Title) || Page.Title.Contains("Untitled Page")) Page.Title = brand.Name.ToUpper();
        }

        public string GetBasePath()
        {
            return string.Format("{0}://{1}{2}",
                    HttpContext.Current.Request.Url.Scheme,
                    HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                    (HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath
                    );
        }
    }
}
