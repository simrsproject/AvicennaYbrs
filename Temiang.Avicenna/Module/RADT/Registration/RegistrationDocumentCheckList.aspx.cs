using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class RegistrationDocumentCheckList : BasePageDialog
    {
        private string _lblRegistrationInfo;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _lblRegistrationInfo = Page.Request.QueryString["lblRegistrationInfo"];

            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            //if (grdList.SelectedValue != null)
            //{
            //    return "oWnd.argument.print = '" + RegistrationNo + "|" + grdList.SelectedValue +
            //           "'";
            //}
            return string.Empty;
        }

        //public override bool OnButtonOkClicked()
        //{
        //    // 
        //    var regno = RegistrationNo;
        //    var iCount = 0;
        //    var dcdCount = 0;

        //    var dcd = new AppStandardReferenceItem();
        //    if (dcd.LoadByPrimaryKey(AppEnum.StandardReference.DocumentChecklist.ToString(), hdDocumentChecklist.Value))
        //        dcdCount = dcd.LineNumber ?? 0;

        //    var odt = new RegistrationDocumentCheckListCollection();
        //    odt.Query.Where(odt.Query.RegistrationNo == regno);
        //    odt.LoadAll();

        //    // find checked
        //    List<int?> docSelected = new List<int?>();
        //    List<int?> docRemoved = new List<int?>();
        //    foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
        //    {
        //        if (((CheckBox)dataItem.FindControl("IsAttached")).Checked)
        //        {
        //            docSelected.Add(System.Convert.ToInt32(dataItem["DocumentFilesID"].Text));
        //        }
        //        else
        //        {
        //            docRemoved.Add(System.Convert.ToInt32(dataItem["DocumentFilesID"].Text));
        //        }
        //    }

        //    // tambahkan yang selected
        //    foreach (var s in docSelected)
        //    {
        //        if ((from o in odt where o.DocumentFilesID == s select o).Count() > 0)
        //        {
        //            // jika sudah ada maka biarkan saja
        //        }
        //        else
        //        {
        //            // jika belum ada tambahkan yang baru
        //            var newo = odt.AddNew();
        //            newo.RegistrationNo = regno;
        //            newo.DocumentFilesID = s;
        //            newo.LastUpdateDateTime = DateTime.Now;
        //            newo.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //        }
        //    }

        //    // buang yang gak dipilih
        //    foreach (var s in docRemoved)
        //    {
        //        if ((from o in odt where o.DocumentFilesID == s select o).Count() > 0)
        //        {
        //            // remove yang ini
        //            var odel = (from o in odt where o.DocumentFilesID == s select o).First();
        //            odel.MarkAsDeleted();
        //        }
        //        else
        //        {
        //            // 
        //        }
        //    }

        //    var odtCount = odt.Count();

        //    var regInfoCount = new RegistrationInfoSumary();
        //    if (!regInfoCount.LoadByPrimaryKey(regno))
        //    {
        //        regInfoCount.AddNew();
        //        regInfoCount.RegistrationNo = regno;
        //        regInfoCount.NoteCount = 0;
        //        regInfoCount.NoteMedicalCount = 0;
        //    }
        //    regInfoCount.DocumentCheckListCount = odtCount;
        //    regInfoCount.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //    regInfoCount.LastUpdateDateTime = DateTime.Now;

        //    iCount = dcdCount - odtCount;

        //    using (esTransactionScope trans = new esTransactionScope())
        //    {
        //        odt.Save();
        //        regInfoCount.Save();
        //        //Commit if success, Rollback if failed
        //        trans.Complete();
        //    }

        //    UpdateParentOnStartup(iCount);

        //    return true;
        //}

        //public bool OnButtonCancelClicked()
        //{
        //    var regno = RegistrationNo;
        //    var iCount = 0;
        //    var dcdCount = 0;

        //    var dcd = new AppStandardReferenceItem();
        //    if (dcd.LoadByPrimaryKey(AppEnum.StandardReference.DocumentChecklist.ToString(), hdDocumentChecklist.Value))
        //        dcdCount = dcd.LineNumber ?? 0;

        //    var odt = new RegistrationDocumentCheckListCollection();
        //    odt.Query.Where(odt.Query.RegistrationNo == regno);
        //    odt.LoadAll();
        //    var odtCount = odt.Count();

        //    var regInfoCount = new RegistrationInfoSumary();
        //    if (!regInfoCount.LoadByPrimaryKey(regno))
        //    {
        //        regInfoCount.AddNew();
        //        regInfoCount.RegistrationNo = regno;
        //        regInfoCount.NoteCount = 0;
        //        regInfoCount.NoteMedicalCount = 0;
        //    }
        //    regInfoCount.DocumentCheckListCount = odtCount;
        //    regInfoCount.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //    regInfoCount.LastUpdateDateTime = DateTime.Now;
        //    regInfoCount.Save();

        //    iCount = dcdCount - odtCount;

        //    UpdateParentOnStartup(iCount);

        //    return true;
        //}

        //protected void grdList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{
        //    if (e.Item is Telerik.Web.UI.GridDataItem)
        //    {
        //        Telerik.Web.UI.GridDataItem item = e.Item as Telerik.Web.UI.GridDataItem;
        //        System.Data.DataRowView dr = item.DataItem as System.Data.DataRowView; // Convert DataItem into Your Assigned Object
        //        (item.FindControl("IsAttached") as CheckBox).Checked = GetBoolValueFromString(Convert.ToString(dr["IsAttached"]));
        //    }
        //}

        //protected bool GetBoolValueFromString(string strFlag)
        //{
        //    return strFlag.Trim() == "1";
        //}

        private void UpdateParentOnStartup(int iCount)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UpdateRegInfo2", "UpdateInformationCount2('" + _lblRegistrationInfo + "', '" + iCount.ToString() + "');", true);
        }

        protected void grdList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);

                var gfa = new GuarantorDocumentChecklist();
                switch (reg.SRRegistrationType)
                {
                    case "IPR":
                    case "MCU":
                    case "OPR":
                    case "EMR":
                        if (gfa.LoadByPrimaryKey(reg.GuarantorID, reg.SRRegistrationType))
                            hdDocumentChecklist.Value = gfa.SRDocumentChecklist;
                        break;
                    case "ANC":
                        if (gfa.LoadByPrimaryKey(reg.GuarantorID, "OPR"))
                            hdDocumentChecklist.Value = gfa.SRDocumentChecklist;
                        break;
                    default:
                        throw new Exception("Unknown Registration Type!!");
                }
            }

            var dcd = new DocumentChecklistDefinitionQuery("a");
            var df = new DocumentFilesQuery("b");
            var rd = new RegistrationDocumentCheckListQuery("c");

            dcd.InnerJoin(df).On(dcd.DocumentFilesID == df.DocumentFilesID)
                .LeftJoin(rd).
                    On(rd.RegistrationNo == RegistrationNo & dcd.DocumentFilesID == rd.DocumentFilesID)
                .Where(
                    dcd.SRDocumentChecklist == hdDocumentChecklist.Value,
                    df.IsActive == true)
                .Select(
                    dcd.SRDocumentChecklist,
                    dcd.DocumentFilesID,
                    df.DocumentNumber,
                    df.DocumentName,
                    rd.CreatedByUserID,
                    rd.CreatedDateTime,
                    rd.FileName,
                    @"<CASE WHEN c.RegistrationNo IS NULL THEN 0 ELSE 1 END IsAttached>");

            var dtb = dcd.LoadDataTable();
            dtb.Columns.Add("ThumbNail", typeof(System.Byte[]));

            // Replace empty smallimage
            var imgHelper = new ImageHelper();
            var pdfImage = imgHelper.LoadImageToArray(string.Format("{0}\\Images\\pdf100.png", Server.MapPath("~")));
            foreach (DataRow row in dtb.Rows)
            {
                var fileName = row["FileName"].ToString();

                // Update thumbnail
                if (File.Exists(fileName))
                {
                    try
                    {
                        if (fileName.ToLower().Contains(".pdf"))
                        {
                            row["ThumbNail"] = pdfImage;
                        }
                        else
                        {
                            
                            var smallImg = imgHelper.ResizeImage(imgHelper.LoadImageToArray(fileName), new Size(100, 100), true, InterpolationMode.Low);
                            row["ThumbNail"] = imgHelper.ToByteArray(smallImg, ImageFormat.Jpeg);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            grdList.DataSource = dtb;
        }

        protected void grdList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "IsAttached")
            {
                ToggleDocumentStatus(e.CommandArgument.ToString());
                grdList.Rebind();

            }
        }

        private void ToggleDocumentStatus(string dfid)
        {
            var rdc = new BusinessObject.RegistrationDocumentCheckList();
            if (rdc.LoadByPrimaryKey(RegistrationNo, dfid.ToInt()))
            {
                rdc.MarkAsDeleted();
            }
            else
            {
                // jika belum ada tambahkan yang baru
                rdc = new BusinessObject.RegistrationDocumentCheckList();
                rdc.RegistrationNo = RegistrationNo;
                rdc.DocumentFilesID = dfid.ToInt();
            }

            rdc.Save();

            // Update Info remain in Parent
            // Jumlah document yg dianggap sudah ada
            var rdclColl = new RegistrationDocumentCheckListCollection();
            rdclColl.Query.Where(rdclColl.Query.RegistrationNo == RegistrationNo);
            rdclColl.LoadAll();

            // Info Document Count
            var regInfoCount = new RegistrationInfoSumary();
            if (!regInfoCount.LoadByPrimaryKey(RegistrationNo))
            {
                regInfoCount.AddNew();
                regInfoCount.RegistrationNo = RegistrationNo;
                regInfoCount.NoteCount = 0;
                regInfoCount.NoteMedicalCount = 0;
            }
            regInfoCount.DocumentCheckListCount = rdclColl.Count;
            regInfoCount.Save();

            // Jumlah kebutuhan Document

            // Tampilkan jml yg belum ada
            var iCount = (DocumentChecklistCount ?? 0) - regInfoCount.DocumentCheckListCount;
            AjaxManager.ResponseScripts.Add("UpdateInformationCount2('" + _lblRegistrationInfo + "', '" + iCount.ToString() + "');");
        }

        private int? _documentChecklistCount;
        protected int? DocumentChecklistCount
        {
            get
            {
                if (_documentChecklistCount == null)
                {
                    var dcd = new AppStandardReferenceItem();
                    if (dcd.LoadByPrimaryKey(AppEnum.StandardReference.DocumentChecklist.ToString(), hdDocumentChecklist.Value))
                        _documentChecklistCount = dcd.LineNumber ?? 0; // LineNumber berisi jumlah document yg diperlukan yg diupdate dari entrian Guarantor Document Checklist Definition
                }
                return _documentChecklistCount;
            }
        }
    }
}
