using System;
using System.Collections;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class AssessmentImageCtl : BaseAssessmentCtl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            Save(rim.RegistrationInfoMedicID);
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            Rebind(isEdited);
        }

        #endregion

        #region grdImageGallery
        private void Save(string rimid)
        {
            System.Data.DataTable dtbLast = null;
            var lastNo = 0;
            var diags = PatientAssessmentImages;
            foreach (var ed in diags)
            {
                if (ed.es.IsDeleted) continue;
                if (ed.es.IsAdded && ed.DocumentImage == null)
                {
                    ed.MarkAsDeleted();
                }
                else if (ed.es.IsAdded)
                {
                    // Set Header Value
                    ed.RegistrationInfoMedicID = rimid;

                    // Cek ulang ImageNo terakhir di DB utk mencegah error jika ada user lain yg nyalip insert Diagnosa nya
                    if (dtbLast == null && lastNo == 0)
                    {
                        var qr = new PatientAssessmentImageQuery("ed");
                        qr.Select(qr.ImageNo);
                        qr.Where(qr.RegistrationInfoMedicID == rimid);
                        qr.OrderBy(qr.ImageNo.Descending);
                        qr.es.Top = 1;

                        dtbLast = qr.LoadDataTable();
                        if (dtbLast.Rows.Count > 0)
                            lastNo = dtbLast.Rows[0][0].ToInt();
                        else
                            lastNo = 0;
                    }
                    var newNo = lastNo == 0 ? 1 : lastNo + 1;
                    ed.ImageNo = newNo;
                    lastNo = newNo;
                }
            }
            diags.Save();
        }

        private string RegistrationInfoMedicID
        {
            get
            {
                return Request.QueryString["rimid"];
            }
        }
        public void Rebind(bool isEdited)
        {
            // Load Data
            PatientAssessmentImages = null;
            LoadPatientAssessmentImages(RegistrationInfoMedicID);

            //Toogle grid command
            RefreshMenu(isEdited);
        }

        public void RefreshMenu(bool isEdited)
        {
            //Toogle grid command
            grdImageGallery.Columns[0].Visible = isEdited;
            grdImageGallery.Columns[grdImageGallery.Columns.Count - 1].Visible = isEdited;

            grdImageGallery.MasterTableView.CommandItemDisplay = isEdited
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;

            if (!isEdited)
            {
                grdImageGallery.MasterTableView.ClearEditItems();
                grdImageGallery.MasterTableView.IsItemInserted = false;
            }
            else
            {
                // Insert Mode
                grdImageGallery.MasterTableView.IsItemInserted = true;
            }

            //Perbaharui tampilan dan data
            grdImageGallery.Rebind();
        }

        protected void grdImageGallery_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            // Load Data
            if (!IsPostBack || PatientAssessmentImages == null)
            {
                LoadPatientAssessmentImages(RegistrationInfoMedicID);
            }

            grdImageGallery.DataSource = PatientAssessmentImages;
        }

        private void LoadPatientAssessmentImages(string rimid)
        {
            var qr = new PatientAssessmentImageQuery("ed");
            qr.Where(qr.RegistrationInfoMedicID == rimid);
            qr.OrderBy(qr.ImageNo.Ascending);

            var coll = new PatientAssessmentImageCollection();
            coll.Load(qr);
            PatientAssessmentImages = coll;
        }

        private PatientAssessmentImageCollection PatientAssessmentImages
        {
            get
            {
                if (Session["paimg"] == null)
                    return null;

                return (PatientAssessmentImageCollection)Session["paimg"];
            }
            set { Session["paimg"] = value; }
        }

        protected void grdImageGallery_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var uc = (AssessmentImageItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (uc != null)
            {

                var ent = PatientAssessmentImages.AddNew();
                var lastNo = 0;
                foreach (var item in PatientAssessmentImages)
                {
                    if (item.ImageNo > 0)
                        lastNo = item.ImageNo ?? 0;
                }

                var newNo = (lastNo == 0) ? 1 : lastNo + 1;
                ent.ImageNo = newNo;
                SetEntityValue(uc, ent);
                e.Canceled = true;

                // Show hasil insert
                grdImageGallery.Rebind();
            }

        }

        private static void SetEntityValue(AssessmentImageItem uc, PatientAssessmentImage ent)
        {
            ent.DocumentName = uc.DocumentName;

            if (!string.IsNullOrWhiteSpace(uc.ImageCaptureInString))
            {
                // Contoh data 
                //  - dari JCrop  -> data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD...
                //  - dari CropIt -> data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA...

                var imgHelper = new ImageHelper();
                var dataImage = imgHelper.ConvertBase64StringToImage(uc.ImageCaptureInString.Split(',')[1]);
                // Save As Is
                //ent.DocumentImage = dataImage.ToByteArray(System.Drawing.Imaging.ImageFormat.Jpeg);

                var resizedImg = imgHelper.ResizeImage(dataImage, new System.Drawing.Size(320, 320), true, System.Drawing.Drawing2D.InterpolationMode.Default);
                ent.DocumentImage = imgHelper.ToByteArray(resizedImg,System.Drawing.Imaging.ImageFormat.Jpeg);

                //var compressedImg = resizedImg.CompressImageToArray(100);
                //if (compressedImg != null)
                //{
                //    ent.DocumentImage = compressedImg;
                //}


            }
        }

        protected void grdImageGallery_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var imgNo = editedItem.GetDataKeyValue("ImageNo").ToInt();
            var ent = PatientAssessmentImages.FirstOrDefault(n => n.ImageNo == imgNo);
            if (ent != null)
            {
                var uc = (AssessmentImageItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                SetEntityValue(uc, ent);
            }
            grdImageGallery.Rebind();

        }
        protected void grdImageGallery_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var imgNo = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ImageNo"].ToInt();
            var ep = PatientAssessmentImages.FirstOrDefault(n => n.ImageNo == imgNo);
            if (ep != null)
            {
                ep.MarkAsDeleted();
            }
        }

        protected void grdImageGallery_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            RadGrid grid = (sender as RadGrid);
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                grid.MasterTableView.ClearEditItems(); // Close Edit Template
            }
            else if (e.CommandName == RadGrid.EditCommandName)
            {
                e.Item.OwnerTableView.IsItemInserted = false;  // Close Insert Template
            }

        }

        #endregion
    }
}