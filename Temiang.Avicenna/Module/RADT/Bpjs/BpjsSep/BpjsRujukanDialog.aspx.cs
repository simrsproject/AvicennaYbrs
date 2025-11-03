using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsRujukanDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.BpjsSep;

            if (!IsPostBack)
            {
                if (Request.QueryString["type"] == "0")
                {
                    lblNomor.Text = "No Peserta";
                    txtNoKartu.Text = Request.QueryString["bpjsNo"];
                }
                else
                {
                    lblNomor.Text = "No Rujukan";
                    txtNoKartu.Text = Request.QueryString["rujukanNo"];
                }

                if (!string.IsNullOrEmpty(txtNoKartu.Text)) btnCariData_Click(null, null);
            }
        }

        protected void btnCariData_Click(object sender, EventArgs e)
        {
            GetTableRujukan.Rows.Clear();

            var svc = new Common.BPJS.VClaim.Service();
            var response = svc.GetRujukan(txtNoKartu.Text, Request.QueryString["type"] == "0" ? Common.BPJS.VClaim.Enum.SearchRujukan.NoPeserta : Common.BPJS.VClaim.Enum.SearchRujukan.NoRujukan);
            if (response.MetaData.IsValid)
            {
                var rujukan = response.Response.Rujukan;

                DataRow row = GetTableRujukan.NewRow();
                row["NoKunjungan"] = rujukan.NoKunjungan;

                string format = "yyyy-MM-dd";
                DateTime parsed;
                DateTime.TryParseExact(rujukan.TglKunjungan, format, null, System.Globalization.DateTimeStyles.None, out parsed);

                row["TglKunjungan"] = parsed;

                row["NoKartu"] = rujukan.Peserta.NoKartu;
                row["NamaPeserta"] = rujukan.Peserta.Nama;
                row["NamaPPKPerujuk"] = rujukan.ProvPerujuk.Nama;
                GetTableRujukan.Rows.Add(row);
            }
            else ScriptManager.RegisterStartupScript(this, GetType(), "cari", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message), true);

            grdList.DataSource = GetTableRujukan;
            grdList.DataBind();

        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = '" + "value!rujukan!" + grdList.SelectedValue + "'";
        }

        private DataTable GetTableRujukan
        {
            get
            {
                if (ViewState["tblRujukan"] != null) return ViewState["tblRujukan"] as DataTable;
                else
                {
                    var tbl = new DataTable();
                    tbl.Columns.Add("NoKunjungan", typeof(string));
                    tbl.Columns.Add("TglKunjungan", typeof(DateTime));
                    tbl.Columns.Add("NoKartu", typeof(string));
                    tbl.Columns.Add("NamaPeserta", typeof(string));
                    tbl.Columns.Add("NamaPPKPerujuk", typeof(string));
                    return tbl;
                }
            }
        }
    }
}
