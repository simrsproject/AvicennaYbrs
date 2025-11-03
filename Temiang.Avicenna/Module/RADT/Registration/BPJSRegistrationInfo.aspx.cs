using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Text;
using System.Data;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class BPJSRegistrationInfo : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            switch (Request.QueryString["type"])
            {
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.Admitting;
                    break;
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientRegistration;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientRegistration;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientRegistration;
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    ProgramID = AppConstant.Program.HealthScreeningRegistration;
                    break;
                default:
                    if (Request.QueryString["type"] == "ruj") ProgramID = AppConstant.Program.BpjsRujukan;
                    break;
            }
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = BpjsSeps;
        }

        private DataTable BpjsSeps
        {
            get
            {
                var sep = new BpjsSEP();
                sep.Query.es.Top = 1;
                sep.Query.Where(sep.Query.NoSEP == Request.QueryString["sep"]);
                if (!sep.Query.Load())
                {
                    var svc = new Common.BPJS.VClaim.v11.Service();
                    var response = svc.GetSep(Request.QueryString["sep"]);
                    if (response.MetaData.IsValid)
                    {
                        sep = new BpjsSEP();
                        sep.NoSEP = response.Response.NoSep;
                        sep.NomorKartu = response.Response.Peserta.NoKartu;

                        sep.Save();
                    }
                    else
                    {

                    }
                }

                var query = new BpjsSEPQuery("a");
                var std = new AppStandardReferenceItemQuery("b");
                var diag = new DiagnoseQuery("c");
                var reg = new RegistrationQuery("e");
                var brg = new ServiceUnitBridgingQuery("f");

                query.LeftJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfService.ToString() && std.ItemID == query.JenisPelayanan);
                query.LeftJoin(diag).On(query.DiagnosaAwal == diag.DiagnoseID);
                query.LeftJoin(brg).On(query.PoliTujuan == brg.BridgingID && brg.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                query.LeftJoin(reg).On(query.NoSEP == reg.BpjsSepNo && reg.IsVoid == false);
                query.Select(
                    query,
                    std.ItemName.As("TypeOfService"),
                    diag.DiagnoseName,
                    "<CAST(CASE WHEN a.LakaLantas = '1' THEN 1 ELSE 0 END AS BIT) AS IsLakaLantas>",
                    brg.BridgingID.Coalesce("''"),
                    brg.BridgingName.Coalesce("''"),
                    "<CAST(ISNULL((SELECT CASE WHEN (SELECT COUNT(r.RegistrationNo) FROM Registration AS r WHERE r.BpjsSepNo = a.NoSEP AND r.IsVoid = 0) > 0 THEN 1 ELSE 0 END), 0) AS BIT) AS IsRegistration>",
                    "<a.NamaPasien + ' (' + a.JenisKelamin + ')' AS NamaPasienJK>",
                    reg.RegistrationNo.Coalesce("''")
                    );
                //query.Where(query.NoMR == Request.QueryString["medNo"], reg.RegistrationNo.IsNull());
                query.Where(query.NoSEP == Request.QueryString["sep"]);
                query.OrderBy(query.NoSEP.Descending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.sep = '" + grdList.SelectedValue + "'";
        }
    }
}
