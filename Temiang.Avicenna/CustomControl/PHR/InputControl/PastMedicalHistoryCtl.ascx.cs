using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Phr;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    public partial class PastMedicalHistoryCtl : BasePhrCtl
    {
        private const string PastMedHist = "PastMedHist";
        private bool _isReadOnly = false;
        protected override void OnSetReadOnly(bool isReadOnly, Patient pat, Registration reg)
        {
            _isReadOnly = isReadOnly;
            grdMedicalHist.Columns[0].Display = !isReadOnly; // Selected
            grdMedicalHist.Columns[1].Display = isReadOnly; // IsSelected
        }

        protected override void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            // Refresh
            grdMedicalHist.DataSource = null;
            grdMedicalHist.Rebind();
        }

        protected override void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            SavePastMedicalHist(pat.PatientID);
        }

        #region Past Medical History
        private void SavePastMedicalHist(string patientID)
        {
            using (var trans = new esTransactionScope())
            {
                // PastMedicalHistory
                var medColl = new PastMedicalHistoryCollection();

                medColl.Query.Where(medColl.Query.PatientID == patientID);
                medColl.LoadAll();
                medColl.MarkAllAsDeleted();
                medColl.Save();

                medColl = new PastMedicalHistoryCollection();

                foreach (GridDataItem item in grdMedicalHist.MasterTableView.Items)
                {
                    var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));
                    if (chkIsSelected.Checked)
                    {
                        var txtNotes = ((RadTextBox)item.FindControl("txtNotes"));
                        var med = medColl.AddNew();
                        med.PatientID = patientID;
                        med.SRMedicalDisease = item.GetDataKeyValue("ItemID").ToString();
                        med.Notes = txtNotes.Text;
                    }
                }

                medColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private DataTable MedicalHistDataTableEditMode(string patientID)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrFam = new PastMedicalHistoryQuery("a");
            que.LeftJoin(qrFam)
                .On(que.ItemID == qrFam.SRMedicalDisease && qrFam.PatientID == patientID);
            que.Where(que.StandardReferenceID == PastMedHist);

            //if (!string.IsNullOrEmpty(this.PastMedicalHistRefId))
            //    que.Where(que.ReferenceID.Like(string.Format("{0}%", PastMedicalHistRefId)));
            //else
            que.Where(que.Or(que.ReferenceID.IsNull(), que.ReferenceID == string.Empty));

            que.Select(que.ItemID, que.ItemName, qrFam.Notes, "<CONVERT(BIT,CASE WHEN a.SRMedicalDisease IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        protected void grdMedicalHist_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (_isReadOnly)
                return;

            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var chkIsSelected = ((CheckBox)dataItem.FindControl("chkIsSelected"));
                chkIsSelected.Checked = ((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked;

                var txtNotes = ((RadTextBox)dataItem.FindControl("txtNotes"));
                var notes = dataItem["Notes"].Text;
                if (notes == "&nbsp;")
                    notes = string.Empty;
                txtNotes.Text = notes;

            }
        }
        protected void grdMedicalHist_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdMedicalHist.DataSource = MedicalHistDataTableEditMode(PatientID);
        }
        #endregion

    }
}