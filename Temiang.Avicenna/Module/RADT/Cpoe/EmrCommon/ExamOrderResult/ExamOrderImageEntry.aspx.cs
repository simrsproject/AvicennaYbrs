/// Tidak dipakai lagi hanya untuk copy code
/// 
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ExamOrderImageEntry : BasePageDialogHistEntry
    {
        public string TransactionNo
        {
            get
            {
                return Request.QueryString["trno"];
            }
        }

        public string SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"];
            }
        }
        public string ItemID
        {
            get
            {
                return Request.QueryString["itid"];
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            //IsSingleRecordMode = true; //Save then close
            //ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            //ToolBar.EditVisible = false;
            //ToolBar.AddVisible = false;
            // -------------------

            Splitter.Orientation = Orientation.Horizontal;
            Splitter.Items[0].Height = Unit.Pixel(600);

            if (!IsPostBack)
            {
                var item = new Item();
                item.LoadByPrimaryKey(ItemID);

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = string.Format("Document Image of : {2} for {0} (MRN: {1}) ",pat.PatientName, pat.MedicalNo,item.ItemName);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        public override void OnServerValidate(ValidateArgs args)
        {

        }
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = new TransChargesItemImage();
            if (ent.LoadByPrimaryKey(TransactionNo,SequenceNo, txtImageNo.Text.ToInt()))
            {
                txtImageNo.Text = string.Format("{0:00000}", ent.ImageNo);
                txtDocumentNotes.Text = ent.DocumentNotes;
                txtDocumentName.Text = ent.DocumentName;
                
                imgDocumentImage.DataValue = ent.DocumentImage;
                imgDocumentImage.Width = 500;
                imgDocumentImage.Height = 500;
                Context.Cache.Remove(Session.SessionID + "_DocumentImage");
                if (ent.DocumentImage != null)
                    Context.Cache.Insert(Session.SessionID + "_DocumentImage", ent.DocumentImage, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                else
                    imgDocumentImage.ImageUrl = ""; // Untuk menghilangkan image
            }
            else
            {
                ClearEntry();
            }

        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }
        protected override void OnMenuNewClick()
        {
            ClearEntry();
            PopulateImageNo(NewImageNo());
        }

        private void ClearEntry()
        {
            txtImageNo.Text = string.Empty;
            txtDocumentName.Text = string.Empty;
            txtDocumentNotes.Text = string.Empty;

            imgDocumentImage.ImageUrl = ""; // Untuk menghilangkan image

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, true);
            if (args.IsCancel==false)
                lvItemDocumentImage.Rebind();
        }

        private bool Save(ValidateArgs args, bool isNewRecord = false)
        {
            var ent = new TransChargesItemImage();
            if (isNewRecord || !ent.LoadByPrimaryKey(TransactionNo, SequenceNo, txtImageNo.Text.ToInt()))
            {
                ent.TransactionNo = TransactionNo;
                ent.SequenceNo = SequenceNo;
                ent.ImageNo = NewImageNo();
            }
            ent.DocumentName = txtDocumentName.Text;
            ent.DocumentNotes = txtDocumentNotes.Text;
            ent.DocumentImage = imgDocumentImage.DataValue;
            if (Context.Cache[Session.SessionID + "_DocumentImage"] != null)
                ent.DocumentImage = (byte[])Context.Cache[Session.SessionID + "_DocumentImage"];

            ent.Save();

            txtImageNo.Text = string.Format("{0:00000}", ent.ImageNo);
            return true;
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, false);
            if (args.IsCancel == false)
                lvItemDocumentImage.Rebind();
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }


        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var nmd = new TransChargesItemImage();
            nmd.LoadByPrimaryKey(TransactionNo, SequenceNo, txtImageNo.Text.ToInt());
            nmd.MarkAsDeleted();
            nmd.Save();

            ClearEntry();
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !string.IsNullOrEmpty(txtImageNo.Text);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(AjaxManager, uplDocumentImage);
            ajax.AddAjaxSetting(AjaxManager, imgDocumentImage);
            ajax.AddAjaxSetting(lvItemDocumentImage, txtImageNo);
            ajax.AddAjaxSetting(lvItemDocumentImage, txtDocumentName);
            ajax.AddAjaxSetting(lvItemDocumentImage, txtDocumentNotes);
            ajax.AddAjaxSetting(lvItemDocumentImage, uplDocumentImage);
            ajax.AddAjaxSetting(lvItemDocumentImage, imgDocumentImage);
        }
        #endregion

        #region Entry
        private int NewImageNo()
        {
            var qr = new TransChargesItemImageQuery("a");
            var fb = new TransChargesItemImage();
            qr.es.Top = 1;
            qr.Where(qr.TransactionNo == TransactionNo, qr.SequenceNo == SequenceNo);
            qr.OrderBy(qr.ImageNo.Descending);

            if (fb.Load(qr))
            {
                return fb.ImageNo.ToInt() + 1;
            }
            return 1;
        }

        protected void uplDocumentImage_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            using (Stream stream = e.File.InputStream)
            {
                byte[] imgData = new byte[stream.Length];
                stream.Read(imgData, 0, imgData.Length);

                Context.Cache.Remove(Session.SessionID + "_DocumentImage");
                Context.Cache.Insert(Session.SessionID + "_DocumentImage", imgData, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);

                imgDocumentImage.DataValue = imgData;
            }

        }
        protected void lbtnDocumentImageEdit_OnClick(object sender, EventArgs e)
        {
            PopulateImageNo(hdnImageNoForEdit.Value.ToInt());
            OnPopulateEntryControl(new ValidateArgs());
        }

        private void PopulateImageNo(int no)
        {
            txtImageNo.Text = string.Format("{0:00000}", no);
        }
        #endregion

        #region History
        protected void lvItemDocumentImage_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var qr = new TransChargesItemImageQuery();
            qr.Where(qr.TransactionNo == TransactionNo, qr.SequenceNo == SequenceNo);
            var dtb = qr.LoadDataTable();
            lvItemDocumentImage.DataSource = dtb;
        }
        #endregion


    }
}