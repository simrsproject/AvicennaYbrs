using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.RADT.Cpoe;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Assessment Control Localist / Body Diagram
    /// </summary>
    /// Create By: Handono
    /// Modif History:
    /// -------------------------------------------
    /// 2023-09-26 Handono (RSUD Cideres)
    /// - Display Body Diagram filter by ReferenceIDs
    /// 
    public partial class LocalistCtl : BaseAssessmentCtl
    {
        public string Width
        {
            get
            {
                if (ViewState["width"]==null)
                    return "style=\"width: 49%; border: 1px solid gray;\"";

                var width = ViewState["width"].ToString();
                return string.IsNullOrEmpty(width) ? "style=\"width: 49%; border: 1px solid gray;\"" : string.Format("style=\"width: {0}; border: 1px solid gray;\"", width);
            }
            set { ViewState["width"] = value; }
        }

        public string BodyImageReferenceID
        {
            get { return hdnReferenceID.Value; }
            set { hdnReferenceID.Value = value; }
        }
        public bool IsNoteVisible
        {
            get { return hdnNoteVisible.Value == "1" ? true : false; }
            set
            {
                hdnNoteVisible.Value = value ? "1" : "0";
            }
        }
        protected string SessionNameDtb
        {
            // Jika dirubah maka rubah juga yg di LocalistStatusEntry.aspx.cs
            get { return string.IsNullOrEmpty(BodyImageReferenceID) ? "rimBodyImage" : string.Format("rimBodyImage_{0}", BodyImageReferenceID); }
        }
        #region override method

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Save Localist / Body Image
            var dtbSession = (DataTable)Session[SessionNameDtb];

            var i = 0;
            foreach (DataRow row in dtbSession.Rows)
            {
                var txtNotes = (RadTextBox)lvLocalistStatus.Items[i].FindControl("txtNotes");
                txtNotes.Visible = IsNoteVisible;
                i++;
                if (true.Equals(row["IsModified"]) || !string.IsNullOrWhiteSpace(txtNotes.Text))
                {
                    SaveLocalistStatus(RegistrationInfoMedicID, row["BodyID"].ToString(),
                        (byte[])row["BodyImage"], txtNotes.Text);
                }
            }
        }

        #endregion

        private void SaveLocalistStatus(string regInfoMedicID, string bodyId, byte[] bodyImage, string notes)
        {
            var es = new RegistrationInfoMedicBodyDiagram();
            if (!es.LoadByPrimaryKey(regInfoMedicID, bodyId))
            {
                es = new RegistrationInfoMedicBodyDiagram
                {
                    RegistrationInfoMedicID = regInfoMedicID,
                    IsDeleted = false,
                    ServiceUnitID = Request.QueryString["unit"],
                    ParamedicID = ParamedicID,
                    BodyID = bodyId,
                    CreatedDateTime = DateTime.Now,
                    CreatedByUserID = AppSession.UserLogin.UserID
                };
            }

            es.BodyImage = bodyImage;
            es.Notes = notes;
            es.Save();
        }


        protected void lvLocalistStatus_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || !RegistrationInfoMedicID.Equals(Session["rimBodyImage_id"]))
            {
                DataTable dtb;
                if (!string.IsNullOrEmpty(BodyImageReferenceID))
                {
                    dtb = BodyDiagramByReferenceIDs();
                }
                else
                {
                    if (Request.QueryString["rt"] == "IPR")
                        dtb = BodyDiagramInPatient();
                    else
                    {
                        dtb = BodyDiagramOutPatient();
                        if (dtb.Rows.Count == 0)
                            dtb = BodyDiagramInPatient(); // Ambil dari AssessmentType Matrix
                    }
                }

                Session["rimBodyImage_id"] = RegistrationInfoMedicID;
                Session[SessionNameDtb] = dtb;
            }

            var dtbSession = (DataTable)Session[SessionNameDtb];
            lvLocalistStatus.DataSource = dtbSession;
        }

        private DataTable BodyDiagramOutPatient()
        {
            var qr = new RegistrationInfoMedicBodyDiagramQuery("rim");
            var qrSubd = new BodyDiagramServiceUnitQuery("bsu");
            qr.RightJoin(qrSubd)
                .On(qr.BodyID == qrSubd.BodyID && qr.RegistrationInfoMedicID == RegistrationInfoMedicID);

            var qrBody = new BodyDiagramQuery("bd");
            qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

            qr.Select(qr.RegistrationInfoMedicID,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime, qr.Notes,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

            qr.Where(qrSubd.ServiceUnitID == ServiceUnitID);

            var dtb = qr.LoadDataTable();
            return dtb;
        }
        private DataTable BodyDiagramInPatient()
        {
            var qr = new RegistrationInfoMedicBodyDiagramQuery("rim");
            var qrSubd = new AssessmentTypeBodyDiagramQuery("bsu");
            qr.RightJoin(qrSubd)
                .On(qr.BodyID == qrSubd.BodyID && qr.RegistrationInfoMedicID == RegistrationInfoMedicID);

            var qrBody = new BodyDiagramQuery("bd");
            qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

            qr.Select(qr.RegistrationInfoMedicID,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime, qr.Notes,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

            qr.Where(qrSubd.SRAssessmentType == AssessmentType);

            var dtb = qr.LoadDataTable();
            return dtb;
        }

        private DataTable BodyDiagramByReferenceIDs()
        {
            var qr = new RegistrationInfoMedicBodyDiagramQuery("rim");
            var qrBody = new BodyDiagramQuery("bd");
            qr.RightJoin(qrBody).On(qr.BodyID == qrBody.BodyID && qr.RegistrationInfoMedicID == RegistrationInfoMedicID);

            qr.Select(qr.RegistrationInfoMedicID,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime, qr.Notes,
                "<CASE WHEN rim.RegistrationInfoMedicID IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

            qr.Where(qrBody.ReferenceIDs.Like("%" + BodyImageReferenceID + "%"));

            var dtb = qr.LoadDataTable();
            return dtb;
        }

    }
}