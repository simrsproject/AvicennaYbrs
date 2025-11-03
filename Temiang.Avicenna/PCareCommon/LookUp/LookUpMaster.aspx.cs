using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraPrinting.Native.LayoutAdjustment;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.Bridging.PCare;
using Temiang.Avicenna.Bridging.PCare.BusinessObject;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.PCareCommon
{
    public partial class LookUpMaster : BasePageDialog
    {
        private string ReferenceType
        {
            get { return this.Request.QueryString["rtype"]; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.Title = string.Format("PCare {0} Reference", ReferenceType);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (grdList.SelectedValues != null)
                return "oWnd.argument.code = '" + grdList.SelectedValue + "'";
            return "oWnd.argument.code = ''";
        }

        private void ShowMessage(string msg)
        {
            trMessage.Visible = true;
            lblMessage.Text = msg;
        }
        private void HideMessage()
        {
            trMessage.Visible = false;
        }
        private DataTable Datasource
        {
            get
            {
                HideMessage();
                var dtb = new DataTable();
                dtb.Columns.Add("Code", typeof(string));
                dtb.Columns.Add("Name", typeof(string));
                dtb.Columns.Add("ResponseData", typeof(string));

                if (!IsPostBack)
                {
                    Session["PCareReference"] = null;
                    return dtb;
                }

                if (Session["PCareReference"] != null)
                    return (DataTable)Session["PCareReference"];


                var bpjs = new Temiang.Avicenna.Bridging.PCare.Utils();


                switch (ReferenceType)
                {
                    case "Diagnosa": // 1
                        var resultDiagnosa = bpjs.DiagnosaList(txtSearch.Text, "0", "10");
                        if (resultDiagnosa.IsOk)
                        {
                            foreach (var item in resultDiagnosa.Response.List)
                            {
                                var newRow = dtb.NewRow();
                                newRow["Code"] = item.KdDiag;
                                newRow["Name"] = item.NmDiag;
                                newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                dtb.Rows.Add(newRow);
                            }
                        }
                        else
                        {
                            ShowMessage(resultDiagnosa.MetaData.MessageDescription);
                        }
                        break;
                    case "Dokter": // 2
                        var resultDokter = bpjs.DokterList("0", "9999");
                        if (resultDokter.IsOk)
                        {
                            foreach (var item in resultDokter.Response.List)
                            {
                                if (item.NmDokter.Contains(txtSearch.Text))
                                {
                                    var newRow = dtb.NewRow();
                                    newRow["Code"] = item.KdDokter;
                                    newRow["Name"] = item.NmDokter;
                                    newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                    dtb.Rows.Add(newRow);
                                }
                            }
                        }
                        else
                        {
                            var newRow = dtb.NewRow();
                            dtb.Rows.Add(newRow);
                            ShowMessage(resultDokter.MetaData.MessageDescription);
                        }

                        break;
                    case "Kesadaran": // 3
                        var resultKesadaran = bpjs.KesadaranList();
                        if (resultKesadaran.IsOk)
                        {
                            foreach (var item in resultKesadaran.Response.List)
                            {
                                if (item.NmSadar.Contains(txtSearch.Text))
                                {
                                    var newRow = dtb.NewRow();
                                    newRow["Code"] = item.KdSadar;
                                    newRow["Name"] = item.NmSadar;
                                    newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                    dtb.Rows.Add(newRow);
                                }
                            }
                        }
                        else
                        {
                            ShowMessage(resultKesadaran.MetaData.MessageDescription);
                        }
                        break;
                    case "Obat": // 4
                        var resultObat = bpjs.ObatList(txtSearch.Text, "0", "10");
                        if (resultObat.IsOk)
                        {
                            foreach (var item in resultObat.Response.List)
                            {
                                var newRow = dtb.NewRow();
                                newRow["Code"] = item.KdObat;
                                newRow["Name"] = item.NmObat;
                                newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                dtb.Rows.Add(newRow);
                            }
                        }
                        else
                        {
                            ShowMessage(resultObat.MetaData.MessageDescription);
                        }
                        break;
                    case "PoliFktp": // 5
                        var resultPoliFktp = bpjs.PoliFktpList("0", "9999");
                        if (resultPoliFktp.IsOk)
                        {
                            foreach (var item in resultPoliFktp.Response.List)
                            {
                                if (item.NmPoli.ToLower().Contains(txtSearch.Text.ToLower()))
                                {
                                    var newRow = dtb.NewRow();
                                    newRow["Code"] = item.KdPoli;
                                    newRow["Name"] = item.NmPoli;
                                    newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                    dtb.Rows.Add(newRow);
                                }
                            }
                        }
                        else
                        {
                            ShowMessage(resultPoliFktp.MetaData.MessageDescription);
                        }
                        break;
                    case "PoliFktl": // 6
                        var resultPoliFktl = bpjs.PoliFktlList("0", "9999");
                        if (resultPoliFktl.IsOk)
                        {
                            foreach (var item in resultPoliFktl.Response.List)
                            {
                                if (item.NmPoli.ToLower().Contains(txtSearch.Text.ToLower()))
                                {
                                    var newRow = dtb.NewRow();
                                    newRow["Code"] = item.KdPoli;
                                    newRow["Name"] = item.NmPoli;
                                    newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                    dtb.Rows.Add(newRow);
                                }
                            }
                        }
                        else
                        {
                            ShowMessage(resultPoliFktl.MetaData.MessageDescription);
                        }
                        break;
                    case "Provider": // 7
                        var resultProvider = bpjs.ProviderList("0", "9999");
                        if (resultProvider.IsOk)
                        {
                            foreach (var item in resultProvider.Response.List)
                            {
                                if (item.NmProvider.ToLower().Contains(txtSearch.Text.ToLower()))
                                {
                                    var newRow = dtb.NewRow();
                                    newRow["Code"] = item.KdProvider;
                                    newRow["Name"] = item.NmProvider;
                                    newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                    dtb.Rows.Add(newRow);
                                }
                            }
                        }
                        else
                        {
                            ShowMessage(resultProvider.MetaData.MessageDescription);
                        }
                        break;
                    case "StatusPulang": // 8 & 9
                        var resultStatusPulang = bpjs.StatusPulangList(false);
                        if (resultStatusPulang.IsOk)
                        {
                            foreach (var item in resultStatusPulang.Response.List)
                            {
                                if (item.NmStatusPulang.ToLower().Contains(txtSearch.Text.ToLower()))
                                {
                                    var newRow = dtb.NewRow();
                                    newRow["Code"] = item.KdStatusPulang;
                                    newRow["Name"] = item.NmStatusPulang;
                                    newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                    dtb.Rows.Add(newRow);
                                }
                            }
                        }
                        else
                        {
                            ShowMessage(resultStatusPulang.MetaData.MessageDescription);
                        }
                        break;
                    case "Tindakan":
                        var resultTindakan = bpjs.TindakanList("1", "9999");
                        if (resultTindakan.IsOk)
                        {
                            foreach (var item in resultTindakan.Response.List)
                            {
                                if (item.NmTindakan.ToLower().Contains(txtSearch.Text.ToLower()))
                                {
                                    var newRow = dtb.NewRow();
                                    newRow["Code"] = item.KdTindakan;
                                    newRow["Name"] = item.NmTindakan;
                                    newRow["ResponseData"] = JsonConvert.SerializeObject(item);
                                    dtb.Rows.Add(newRow);
                                }
                            }
                        }
                        else
                        {
                            ShowMessage(resultTindakan.MetaData.MessageDescription);
                        }
                        break;

                }

                Session["PCareReference"] = dtb;
                return dtb;
            }
            set { Session["PCareReference"] = value; }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Datasource;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
        public override bool OnButtonOkClicked()
        {
            if (grdList.SelectedValue == null) return false;
            var itemID = grdList.SelectedValue.ToString();
            var row = Datasource.Select("Code='" + itemID + "'")[0];

            var stdi = new PCareReferenceItem();
            if (!stdi.LoadByPrimaryKey(ReferenceType, itemID))
            {

                var std = new PCareReference();
                if (!std.LoadByPrimaryKey(ReferenceType))
                {
                    std.ReferenceID = ReferenceType;
                    std.ReferenceName = ReferenceType;
                    std.Save();
                }
                stdi = new PCareReferenceItem();
                stdi.ReferenceID = ReferenceType;
                stdi.ItemID = itemID;
                stdi.ItemName = row["Name"].ToString();
                stdi.ResponseData = row["ResponseData"].ToString();
                stdi.Save();
            }
            else
            {
                stdi.ReferenceID = ReferenceType;
                stdi.ItemName = row["Name"].ToString();
                stdi.ResponseData = row["ResponseData"].ToString();
                stdi.Save();
            }

            return true;
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Datasource = null;
            grdList.Rebind();
        }
    }
}
