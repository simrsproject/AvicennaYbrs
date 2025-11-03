using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class ImunizationHistCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            // Refresh
            grdImunizationHist.Rebind();
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            SaveImmunization();
        }


        protected override void OnDataModeChanged(bool isEdited)
        {
            // Refresh
            grdImunizationHist.DataSource = null;
            grdImunizationHist.Rebind();
        }
        #endregion

        #region Imunization History
        private void SaveImmunization()
        {
            // Hapus yg hanya dari entry manual
            var coll = new PatientImmunizationCollection();
            coll.Query.Where(coll.Query.PatientID == PatientID);
            coll.Query.Where(coll.Query.ReferenceNo.IsNull() || coll.Query.ReferenceNo != string.Empty); 
            coll.LoadAll();
            coll.MarkAllAsDeleted();
            coll.Save();

            // Add yg entry manual
            coll = new PatientImmunizationCollection();
            foreach (GridDataItem item in grdImunizationHist.MasterTableView.Items)
            {
                for (int i = 1; i < 11; i++)
                {
                    AddPatientManualImmunization(coll, item, i);
                }
            }

            coll.Save();

        }

        private void AddPatientManualImmunization(PatientImmunizationCollection coll, GridDataItem item, int seqNo)
        {
            if (!string.IsNullOrEmpty(item["ReferenceNo"].Text) && !item["ReferenceNo"].Text.Equals("&nbsp;")) return;
            if (item["MaxCount"].Text.ToInt() < seqNo) return;

            var date = ((RadMonthYearPicker)item.FindControl(string.Format("txtMonthYear_{0:00}", seqNo))).SelectedDate;
            var isChecked = ((CheckBox)item.FindControl(string.Format("chkDate_{0:00}", seqNo))).Checked;
            if (date != null || isChecked)
            {
                var ent = coll.AddNew();
                ent.PatientID = PatientID;
                ent.ImmunizationID = item["ImmunizationID"].Text;
                ent.ImmunizationNo = seqNo;
                ent.IsDateInMonthYear = true;

                if (date != null)
                    ent.ImmunizationDate = date;
            }
        }
        protected void grdImunizationHist_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)sender).DataSource = PatientImunization();
        }


        private DataTable PatientImunization()
        {
            var query = new ImmunizationQuery("a");
            query.Select(query.ImmunizationID, query.ImmunizationName, query.MaxCount);
            query.OrderBy(query.IndexNo.Ascending);
            var dtb = query.LoadDataTable();
            for (int i = 1; i < 11; i++)
            {
                dtb.Columns.Add(string.Format("Date_0{0}", i), typeof(DateTime));
                dtb.Columns.Add(string.Format("IsChecked_0{0}", i), typeof(bool));
            }

            dtb.PrimaryKey = new[] { dtb.Columns["ImmunizationID"] };

            if (dtb.Rows == null || dtb.Rows.Count == 0) return dtb;

            // Populate Imunization Date
            var qrImun = new PatientImmunizationQuery("b");
            qrImun.Where(qrImun.PatientID == PatientID);
            qrImun.Select(qrImun.ImmunizationID, qrImun.ImmunizationNo, qrImun.ImmunizationDate);
            qrImun.OrderBy(qrImun.ImmunizationID.Ascending, qrImun.ImmunizationNo.Ascending);

            var dtbPatientImun = qrImun.LoadDataTable();
            var rowHd = dtb.Rows[0];
            foreach (DataRow row in dtbPatientImun.Rows)
            {
                if (rowHd["ImmunizationID"].ToString() != row["ImmunizationID"].ToString())
                    rowHd = dtb.Rows.Find(row["ImmunizationID"].ToString());

                if (rowHd == null) continue;

                for (int i = 1; i < 11; i++)
                {
                    if (i.Equals(row["ImmunizationNo"]))
                    {
                        rowHd[string.Format("Date_0{0}", i)] = row["ImmunizationDate"];
                        rowHd[string.Format("IsChecked_0{0}", i)] = true;
                        break;
                    }
                }
            }


            return dtb;
        }

        protected void grdImunizationHist_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                for (var i = 1; i < 11; i++)
                {
                    PopulateCell(item, i);
                }
            }
        }

        private void PopulateCell(GridDataItem item, int seqNo)
        {
            var isExistRef = (!string.IsNullOrEmpty(item["ReferenceNo"].Text) &&
                !item["ReferenceNo"].Text.Equals("&nbsp;")) ;
            var txtDate = (RadMonthYearPicker)item.FindControl(string.Format("txtMonthYear_{0:00}", seqNo));
            var chkDate = (CheckBox)item.FindControl(string.Format("chkDate_{0:00}", seqNo));
            txtDate.Visible = item["MaxCount"].Text.ToInt() >= seqNo;
            chkDate.Visible = txtDate.Visible;

            // Date
            var date = item[string.Format("Date_{0:00}", seqNo)].Text;
            if (!string.IsNullOrEmpty(date) && !date.ToLower().Equals("&nbsp;"))
                txtDate.SelectedDate = Convert.ToDateTime(date);
            else
                txtDate.SelectedDate = null;
            txtDate.Enabled = IsEdited && !isExistRef;


            // Check
            var isChecked = item[string.Format("IsChecked_{0:00}", seqNo)].Text;
            if (!string.IsNullOrEmpty(isChecked) && !isChecked.ToLower().Equals("&nbsp;"))
                chkDate.Checked = Convert.ToBoolean(isChecked);
            chkDate.Enabled = IsEdited && !isExistRef;

            if (chkDate.Checked)
                item[string.Format("InputDate_{0:00}", seqNo)].BackColor = System.Drawing.Color.DodgerBlue;

            //if (!txtDate.Visible)
            //    item[string.Format("InputDate_{0:00}", seqNo)].BackColor = System.Drawing.Color.LightGray;

        }
        #endregion

    }
}