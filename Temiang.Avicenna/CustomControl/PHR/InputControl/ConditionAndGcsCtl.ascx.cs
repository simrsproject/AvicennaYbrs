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

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    public partial class ConditionAndGcsCtl : BasePhrCtl
    {
        protected override void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            var gcs = new ConditionAndGcs();
            if (!string.IsNullOrEmpty(phrLine.QuestionAnswerText) && phrLine.QuestionAnswerText.IsValidJson())
                gcs = JsonConvert.DeserializeObject<ConditionAndGcs>(phrLine.QuestionAnswerText);

            // Set Selectedvalue
            Eye = gcs.Eye.Code;
            Motor = gcs.Motor.Code;
            Verbal = gcs.Verbal.Code;

            // Populate Consciousness
            Consciousness = string.Format("{0} [{1}]", gcs.ConsciousnessDescription, gcs.ConsciousnessValue);

            optCondition.SelectedValue = gcs.Condition;
        }

        protected override void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            var gcs = new ConditionAndGcs();
            gcs.Eye.SetValue(Eye);
            gcs.Motor.SetValue(Motor);
            gcs.Verbal.SetValue(Verbal);
            gcs.Condition = optCondition.SelectedValue;
            phrLine.QuestionAnswerText = JsonConvert.SerializeObject(gcs);
        }

        #region Field

        private string Consciousness
        {
            get { return txtConsciousness.Text; }
            set { txtConsciousness.Text = value; }
        }
        private string Eye
        {
            get { return ddlGcsEye.SelectedValue; }
            set
            {
                SetSelectedGcsList(ddlGcsEye, value);
            }
        }
        public string Verbal
        {
            get { return ddlGcsVerbal.SelectedValue; }
            set
            {
                SetSelectedGcsList(ddlGcsVerbal, value);
            }
        }
        public string Motor
        {
            get { return ddlGcsMotor.SelectedValue; }
            set
            {
                SetSelectedGcsList(ddlGcsMotor, value);
            }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                PopulateGcsList(ddlGcsEye, "GcsEye");
                PopulateGcsList(ddlGcsVerbal, "GcsVerbal");
                PopulateGcsList(ddlGcsMotor, "GcsMotor");
            }
        }


        private void PopulateGcsList(RadDropDownList ddl, string standardReferenceID)
        {
            var dtb = GetStandardReference(standardReferenceID);
            ddl.Items.Clear();
            ddl.Items.Add(new DropDownListItem(string.Empty, "_99"));

            foreach (System.Data.DataRow row in dtb.Rows)
            {
                ddl.Items.Add(new DropDownListItem(string.Format("{0} - {1}",row["Note"], row["ItemName"]), string.Format("{0}_{1}", row["ItemID"], row["Note"])));
            }
        }
        private void SetSelectedGcsList(RadDropDownList ddl, string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                code = code + "_";
                foreach (DropDownListItem item in ddl.Items)
                {
                    if (item.Value.Contains(code))
                    {
                        ddl.SelectedValue = item.Value;
                        return;
                    }
                }
            }
            ddl.SelectedIndex = -1;
        }
        private DataTable GetStandardReference(string standardReferenceID)
        {
            // Note tempat menyimpan score untuk GCS
            var query = new AppStandardReferenceItemQuery();
            query.Select(query.ItemID, query.ItemName, query.Note);
            query.Where
                (
                    query.StandardReferenceID == standardReferenceID
                );
            query.OrderBy(query.ItemID.Ascending);
            var dtb = query.LoadDataTable();
            return dtb;
        }

        protected void ddlGcs_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            PopulateConsciousness();
        }

        private void PopulateConsciousness()
        {
            var gcs = new ConditionAndGcs();
            gcs.Eye.SetValue(Eye);
            gcs.Motor.SetValue(Motor);
            gcs.Verbal.SetValue(Verbal);
            txtConsciousness.Text = gcs.ConsciousnessValue > 20 ? gcs.ConsciousnessDescription : string.Format("{0} [{1}]", gcs.ConsciousnessDescription, gcs.ConsciousnessValue);
        }

        protected void optCondition_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (optCondition.SelectedValue == "DOA")
            {
                ddlGcsEye.SelectedIndex = 0;
                ddlGcsMotor.SelectedIndex = 0;
                ddlGcsVerbal.SelectedIndex = 0;
                PopulateConsciousness();
            }
        }
    }
}