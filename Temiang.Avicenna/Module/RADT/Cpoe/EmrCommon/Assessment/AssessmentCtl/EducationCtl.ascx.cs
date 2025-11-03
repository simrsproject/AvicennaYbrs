using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// Education Control untuk RSMP
    /// yg disave ke field PatientAssessment.Education
    public partial class EducationCtl : BaseAssessmentCtl
    {
        private Educations _educations = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Get Education
            if (!string.IsNullOrEmpty(assessment.Education))
            {
                // Convert to class w json
                try
                {
                    _educations = JsonConvert.DeserializeObject<Educations>(assessment.Education);
                    optIsEducationToPatient.SelectedValue = _educations.IsEducationToPatient ? "1" : "0";
                    txtEducationRecipient.Text = _educations.EducationRecipient;

                }
                catch (Exception)
                {
                }
            }


            grdEducation.Rebind();
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var edus = new Educations();
            edus.IsEducationToPatient = optIsEducationToPatient.SelectedValue == "1";
            edus.EducationRecipient = txtEducationRecipient.Text;
            edus.Items = new List<Education>();
            foreach (GridDataItem item in grdEducation.MasterTableView.Items)
            {
                if (item.Selected)
                {
                    var edu = new Education();
                    var txtNotes = ((RadTextBox)item.FindControl("txtNotes"));
                    edu.ID = item.GetDataKeyValue("ItemID").ToString();
                    edu.Name = item["ItemName"].Text;
                    edu.Notes = txtNotes.Text;
                    edus.Items.Add(edu);
                }
            }

            assessment.Education = JsonConvert.SerializeObject(edus);
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            grdEducation.Columns[0].Display = isEdited; // Selected
            grdEducation.Columns[1].Display = !isEdited; // IsSelected
            grdEducation.Columns[3].Display = !isEdited; // Notes
            grdEducation.Columns[4].Display = isEdited; // Notes Edit

            // Refresh
            grdEducation.Rebind();
        }

        #endregion



        #region Education

        protected void grdEducation_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //((RadGrid)sender).DataSource = IsEdited ? AssesmentEducationTableEditMode() : AssesmentEducationTable();

            // Tampilkan terus semuanya
            ((RadGrid)sender).DataSource = AssesmentEducationTableEditMode();
        }

        //private DataTable AssesmentEducationTable()
        //{
        //    var dtb = new DataTable();
        //    dtb.Columns.Add("ItemID", typeof(string));
        //    dtb.Columns.Add("ItemName", typeof(string));
        //    dtb.Columns.Add("IsSelected", typeof(bool));
        //    dtb.Columns.Add("Notes", typeof(string));

        //    var edus = new Educations();

        //    // Get Education
        //    var asses = CurrentPatientAssessmet;
        //    if (!string.IsNullOrEmpty(asses.Education))
        //    {
        //        // Convert to class w json
        //        try
        //        {
        //            edus = JsonConvert.DeserializeObject<Educations>(asses.Education);
        //            foreach (Education itemEducation in edus.Items)
        //            {
        //                var newRow = dtb.NewRow();
        //                newRow["ItemID"] = itemEducation.ID;
        //                newRow["ItemName"] = itemEducation.Name;
        //                newRow["IsSelected"] = true;
        //                newRow["Notes"] = itemEducation.Notes;
        //                dtb.Rows.Add(newRow);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //    return dtb;
        //}

        private DataTable AssesmentEducationTableEditMode()
        {


            // Retrieve template
            var que = new AppStandardReferenceItemQuery("sri");
            que.Where(que.StandardReferenceID == "AssesmentEducation");
            que.Select(que.ItemID, que.ItemName, "<CONVERT(BIT,0) as IsSelected>");

            // Populate
            var dtb = que.LoadDataTable();
            dtb.Columns.Add("Notes", typeof(string));

            if (_educations != null && _educations.Items != null)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    var edu = _educations.Items.FirstOrDefault(s => s.ID == row["ItemID"].ToString());
                    if (edu != null)
                    {
                        row["Notes"] = edu.Notes;
                        row["IsSelected"] = true;
                    }
                    else
                    {
                        row["Notes"] = string.Empty;
                        row["IsSelected"] = false;
                    }

                }
            }
            return dtb;
        }

        protected void grdEducation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!IsEdited)
                return;

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked)
                {
                    dataItem.Selected = true;
                }
            }
        }


        #endregion

    }
}