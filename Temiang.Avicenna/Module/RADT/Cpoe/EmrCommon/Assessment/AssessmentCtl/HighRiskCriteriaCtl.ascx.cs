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
    public partial class HighRiskCriteriaCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            _highRiskCriteria = assessment.HighRiskCriteria;
            grdHighRiskCriteria.DataSource = null;
            grdHighRiskCriteria.Rebind();
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var risks = new HighRiskCriterias();
            risks.Items = new List<HighRiskCriteria>();
            foreach (GridDataItem item in grdHighRiskCriteria.MasterTableView.Items)
            {
                if (item.Selected)
                {
                    var edu = new HighRiskCriteria();
                    edu.ID = item.GetDataKeyValue("ItemID").ToString();
                    risks.Items.Add(edu);
                }
            }

            assessment.HighRiskCriteria = JsonConvert.SerializeObject(risks);
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            grdHighRiskCriteria.Columns[0].Display = isEdited; // Selected
            grdHighRiskCriteria.Columns[1].Display = !isEdited; // IsSelected

            // Refresh
            grdHighRiskCriteria.Rebind();
        }

        #endregion



        #region Education

        protected void grdHighRiskCriteria_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //((RadGrid)sender).DataSource = IsEdited ? HighRiskCriteriaTableEditMode() : HighRiskCriteriaTable();

            // Tampilkan terus semuanya
            ((RadGrid)sender).DataSource = HighRiskCriteriaTableEditMode();
        }


        private string _highRiskCriteria = null;
        private DataTable HighRiskCriteriaTableEditMode()
        {
            // Retrieve template
            var que = new AppStandardReferenceItemQuery("sri");
            que.Where(que.StandardReferenceID == "HighRiskCriteria");
            que.Select(que.ItemID, que.ItemName, "<CONVERT(BIT,0) as IsSelected>");

            // Load
            var dtb = que.LoadDataTable();

            // Risk selected
            if (!string.IsNullOrEmpty(_highRiskCriteria))
            {
                var risks = new HighRiskCriterias();
                risks = JsonConvert.DeserializeObject<HighRiskCriterias>(_highRiskCriteria); // Convert to class w json
                if (risks.Items.Count > 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        var edu = risks.Items.FirstOrDefault(s => s.ID == row["ItemID"].ToString());
                        if (edu != null)
                        {
                            row["IsSelected"] = true;
                        }
                        else
                        {
                            row["IsSelected"] = false;
                        }

                    }
                }
            }

            return dtb;
        }

        protected void grdHighRiskCriteria_ItemDataBound(object sender, GridItemEventArgs e)
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