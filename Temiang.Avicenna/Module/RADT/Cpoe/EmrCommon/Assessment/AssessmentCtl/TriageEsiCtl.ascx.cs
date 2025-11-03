using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class TriageEsiCtl : System.Web.UI.UserControl
    {
        #region Field

        public Triage5Level Triage5Level
        {
            get
            {
                var triage = new Triage5Level();
                triage.TriageId.SetValue(TriageId);

                return triage;
            }
            set
            {
                if (value == null) value = new Triage5Level();

                // Set Selectedvalue
                TriageId = value.TriageId.Code;

                Airway = string.IsNullOrEmpty(value.AirwayDescription) ? string.Empty : string.Format("{0} [{1}]", value.AirwayDescription, value.TriageValue);
                Breathing = string.IsNullOrEmpty(value.BreathingDescription) ? string.Empty : string.Format("{0} [{1}]", value.BreathingDescription, value.TriageValue);
                Circulation = string.IsNullOrEmpty(value.CirculationDescription) ? string.Empty : string.Format("{0} [{1}]", value.CirculationDescription, value.TriageValue);
                Conscious = string.IsNullOrEmpty(value.ConsciousDescription) ? string.Empty : string.Format("{0} [{1}]", value.ConsciousDescription, value.TriageValue);
            }
        }
        public string Airway
        {
            get { return txtAirway.Text; }
            set { txtAirway.Text = value; }
        }
        public string Breathing
        {
            get { return txtBreathing.Text; }
            set { txtBreathing.Text = value; }
        }
        public string Circulation
        {
            get { return txtCirculation.Text; }
            set { txtCirculation.Text = value; }
        }
        public string Conscious
        {
            get { return txtConscious.Text; }
            set { txtConscious.Text = value; }
        }
        public string TriageId
        {
            get { return ddlTriage.SelectedValue; }
            set
            {
                SetSelectedTriageList(ddlTriage, value);
            }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                PopulateTriageList(ddlTriage, "Triage5Level");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void PopulateTriageList(RadDropDownList ddl, string standardReferenceID)
        {
            var dtb = GetStandardReference(standardReferenceID);
            ddl.Items.Clear();
            ddl.Items.Add(new DropDownListItem(string.Empty, string.Format("{0}_{1}", string.Empty, 0)));
            foreach (System.Data.DataRow row in dtb.Rows)
            {
                ddl.Items.Add(new DropDownListItem(string.Format("{0} - {1}", row["Note"], row["ItemName"]), string.Format("{0}_{1}", row["ItemID"], row["Note"])));
            }
        }
        private void SetSelectedTriageList(RadDropDownList ddl, string code)
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
            query.OrderBy(query.LineNumber.Ascending, query.ItemID.Ascending);
            var dtb = query.LoadDataTable();
            return dtb;
        }

        protected void ddlTriage_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            var triage = new Triage5Level();
            triage.TriageId.SetValue(TriageId);

            txtAirway.Text = string.IsNullOrEmpty(triage.AirwayDescription) ? string.Empty : string.Format("{0} [{1}]", triage.AirwayDescription, triage.TriageValue);
            txtBreathing.Text = string.IsNullOrEmpty(triage.BreathingDescription) ? string.Empty : string.Format("{0} [{1}]", triage.BreathingDescription, triage.TriageValue);
            txtCirculation.Text = string.IsNullOrEmpty(triage.CirculationDescription) ? string.Empty : string.Format("{0} [{1}]", triage.CirculationDescription, triage.TriageValue);
            txtConscious.Text = string.IsNullOrEmpty(triage.ConsciousDescription) ? string.Empty : string.Format("{0} [{1}]", triage.ConsciousDescription, triage.TriageValue);
        }
    }
}