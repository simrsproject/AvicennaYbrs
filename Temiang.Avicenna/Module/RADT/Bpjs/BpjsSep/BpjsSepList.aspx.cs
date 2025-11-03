using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Configuration;
using System.Net;
using System.Web.UI;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class BpjsSepList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "BpjsSepSearch.aspx";
            UrlPageDetail = "VClaim/BpjsVClaimDetail.aspx";
            //UrlPageBPJSSearch = "BpjsSepDataDialog.aspx";

            //ToolBarMenuBPJSSearch.Visible = true;

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsSep;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!IsPostBack)
            {
                //Timer1_Tick(null, null);
                //Timer1.Enabled = true;

                //if (!this.IsUserDeleteAble)
                //{
                //    grdList.Columns[grdList.Columns.Count - 1].Visible = false;
                //}
            }
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(BpjsSEPMetadata.ColumnNames.SepID).ToString();
            Page.Response.Redirect("VClaim/BpjsVClaimDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = BpjsSeps;
        }

        private DataTable BpjsSeps
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null) return ((DataTable)(obj));

                BpjsSEPQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BpjsSEPQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BpjsSEPQuery("a");
                    var std = new AppStandardReferenceItemQuery("b");
                    var diag = new DiagnoseQuery("c");
                    var reg = new RegistrationQuery("e");
                    var brg = new ServiceUnitBridgingQuery("f");
                    var appt = new AppointmentQuery("g");
                    var unit = new ServiceUnitQuery("h");

                    query.es.Distinct = true;
                    query.LeftJoin(std).On(std.StandardReferenceID == AppEnum.StandardReference.BpjsTypeOfService.ToString() && std.ItemID == query.JenisPelayanan);
                    query.LeftJoin(diag).On(query.DiagnosaAwal == diag.DiagnoseID);
                    query.LeftJoin(brg).On(query.PoliTujuan == brg.BridgingID && brg.SRBridgingType == AppEnum.BridgingType.BPJS.ToString());
                    query.LeftJoin(reg).On(query.NoSEP == reg.BpjsSepNo && query.NomorKartu == reg.GuarantorCardNo && reg.IsVoid == false && reg.IsFromDispensary == false && query.TanggalSEP.Date() == reg.RegistrationDate.Date());
                    query.LeftJoin(appt).On(query.NoTransaksi == appt.AppointmentNo);
                    query.LeftJoin(unit).On(appt.ServiceUnitID == unit.ServiceUnitID);
                    query.Select(
                        query.SepID,
                        query.NoSEP,
                        query.TanggalSEP,
                        query.NoRujukan,
                        query.TanggalRujukan,
                        query.NomorKartu,
                        //query.NamaPasien,
                        query.TanggalLahir,
                        query.LastUpdateByUserID,
                        std.ItemName.As("TypeOfService"),
                        diag.DiagnoseName,
                        "<CAST(CASE WHEN a.LakaLantas = '1' THEN 1 ELSE 0 END AS BIT) AS IsLakaLantas>",
                        //query.PoliTujuan.As("BridgingID"),
                        //brg.BridgingID.Coalesce("''"),
                        "<ISNULL(h.ServiceUnitName, f.BridgingName) AS BridgingName>",
                        //brg.BridgingName.Coalesce("''"),
                        //"<CAST(ISNULL((SELECT CASE WHEN (SELECT COUNT(r.RegistrationNo) FROM Registration AS r WHERE r.BpjsSepNo = a.NoSEP AND r.IsVoid = 0 AND r.IsFromDispensary = 0) > 0 THEN 1 ELSE 0 END), 0) AS BIT) AS IsRegistration>",
                        "<CAST(CASE WHEN e.RegistrationNo IS NULL THEN 0 ELSE 1 END AS BIT) AS IsRegistration>",
                        "<a.NamaPasien + ' (' + a.JenisKelamin + ')' AS NamaPasienJK>",
                        reg.RegistrationNo.Coalesce("''")
                        );
                    query.Where(query.TanggalSEP.Date() == DateTime.Now.Date);
                    query.OrderBy(query.NoSEP.Descending);
                }

                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();

                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid)
            {
                if (eventArgument == "rebind") grdList.Rebind();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string url = ConfigurationManager.AppSettings["BPJSServiceUrlLocation"];
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)myRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    lblMessage.Text = string.Format("BPJS Service is online (url : {0})", url);
                    imgOk.Visible = true;
                    imgFailed.Visible = false;
                }
                else
                {
                    lblMessage.Text = string.Format("BPJS Service is offline (url : {0}), error : {1}", url, response.StatusDescription);
                    imgOk.Visible = false;
                    imgFailed.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = string.Format("BPJS Service is offline (url : {0}), error : {1}", url, ex.Message);
                imgOk.Visible = false;
                imgFailed.Visible = true;
            }
        }

        protected void grdList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var sepid = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SepID"]);
            var nosep = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["NoSEP"]);

            var reg = new Registration();
            reg.Query.es.Top = 1;
            reg.Query.Where(reg.Query.BpjsSepNo == nosep, reg.Query.IsVoid == false, reg.Query.IsFromDispensary == false);
            if (reg.Query.Load())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "delete", string.Format("alert('No SEP telah di mapping ke registrasi kunjungan pasien, data tidak bisa dihapus, ref : {0}');", reg.RegistrationNo), true);

                //var entity = new BpjsSEP();
                //entity.Query.es.Top = 1;
                //entity.Query.Where(entity.Query.NoSEP == nosep);
                //entity.Query.Load();

                //entity.LoadByPrimaryKey(nosep);
            }
            else
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var request = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep.DeleteRequest.TSep()
                {
                    NoSep = nosep,
                    User = string.IsNullOrEmpty(AppSession.UserLogin.LicenseNo) ? AppSession.UserLogin.UserID : AppSession.UserLogin.LicenseNo
                };
                var response = svc.Delete(request);

                var log = new WebServiceAPILog()
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = "DeleteSep",
                    Params = JsonConvert.SerializeObject(request),
                    Response = JsonConvert.SerializeObject(response),
                    Totalms = 0
                };
                log.Save();

                if (!response.MetaData.IsValid)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "delete", string.Format("alert('Code : {0}, Message : {1}');", response.MetaData.Code, response.MetaData.Message.Replace("'", string.Empty)), true);

                    //var entity = new BpjsSEP();
                    //if (entity.LoadByPrimaryKey(nosep))
                    //{
                    //    entity.MarkAsDeleted();
                    //    entity.Save();
                    //}

                    //ScriptManager.RegisterStartupScript(this, GetType(), "delete", string.Format("alert('Code : {0}, Message : {1}');", "000", "Hapus SEP berhasil"), true);

                    //this.Session[SessionNameForList] = null;

                    //grdList.Rebind();
                }
                else
                {
                    var entity = new BpjsSEP();
                    if (entity.LoadByPrimaryKey(nosep))
                    {
                        entity.MarkAsDeleted();
                        entity.Save();
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "delete", string.Format("alert('Code : {0}, Message : {1}');", "000", "Hapus SEP berhasil"), true);

                    this.Session[SessionNameForList] = null;

                    grdList.Rebind();
                }
            }
        }
    }
}