using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class UpdateBedStatusList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.UpdateBedStatusForPatientSurgery;
            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.IsActive == true
                    );
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                var roomquery = new ServiceRoomQuery("b");
                var unitquery = new ServiceUnitQuery("a");
                var userquery = new AppUserServiceUnitQuery("c");
                roomquery.InnerJoin(unitquery).On(roomquery.ServiceUnitID == unitquery.ServiceUnitID);
                roomquery.InnerJoin(userquery).On(unitquery.ServiceUnitID == userquery.ServiceUnitID);
                roomquery.Where(
                    unitquery.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    userquery.UserID == AppSession.UserLogin.UserID,
                    roomquery.IsActive == true
                    );
                roomquery.Select(roomquery.RoomID, roomquery.RoomName);
                roomquery.es.Distinct = true;

                DataTable tbl = roomquery.LoadDataTable();
                cboRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow row in tbl.Rows)
                {
                    cboRoomID.Items.Add(new RadComboBoxItem(row["RoomName"].ToString(), row["RoomID"].ToString()));
                }

                var classcoll = new ClassCollection();
                classcoll.Query.Where(
                    classcoll.Query.IsInPatientClass == true,
                    classcoll.Query.IsActive == true
                    );
                classcoll.LoadAll();

                cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Class c in classcoll)
                {
                    cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }

                var srq = new AppStandardReferenceItemQuery();
                srq.Where(srq.StandardReferenceID == AppEnum.StandardReference.BedStatus, srq.IsActive == true,
                          srq.ReferenceID == "Empty");

                DataTable dtbsr = srq.LoadDataTable();
                cboBedStatus.Items.Add(new RadComboBoxItem("", ""));

                foreach (DataRow row in dtbsr.Rows)
                {
                    cboBedStatus.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
                }

                txtReady.ReadOnly = true;
                txtReady.BackColor = System.Drawing.Color.Green;
                txtCleaning.ReadOnly = true;
                txtCleaning.BackColor = System.Drawing.Color.Yellow;
                txtRepaired.ReadOnly = true;
                txtRepaired.BackColor = System.Drawing.Color.Purple;
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Beds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable Beds
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboRoomID.SelectedValue) && string.IsNullOrEmpty(cboClassID.SelectedValue) && string.IsNullOrEmpty(cboBedStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Update Bed Status")) return null;

                var query = new BedQuery("a");
                var srq = new ServiceRoomQuery("b");
                query.InnerJoin(srq).On
                    (
                        query.RoomID == srq.RoomID &
                        srq.IsActive == true
                    );

                var suq = new ServiceUnitQuery("c");
                query.InnerJoin(suq).On
                    (
                        srq.ServiceUnitID == suq.ServiceUnitID &
                        suq.IsActive == true
                    );

                var cq = new ClassQuery("e");
                query.InnerJoin(cq).On
                    (
                        query.ClassID == cq.ClassID &
                        cq.IsActive == true
                    );

                var rq = new RegistrationQuery("f");
                query.LeftJoin(rq).On
                    (
                        query.RegistrationNo == rq.RegistrationNo &
                        rq.IsVoid == false &
                        rq.DischargeDate.IsNull()
                    );

                var gq = new GuarantorQuery("d");
                query.LeftJoin(gq).On(rq.GuarantorID == gq.GuarantorID);

                var pq = new PatientQuery("g");
                query.LeftJoin(pq).On(rq.PatientID == pq.PatientID);

                var asri = new AppStandardReferenceItemQuery("h");
                query.InnerJoin(asri).On
                    (
                        query.SRBedStatus == asri.ItemID &
                        asri.StandardReferenceID == "BedStatus" &
                        asri.ReferenceID == "Empty"
                    );

                query.Select
                    (
                        srq.ServiceUnitID,
                        suq.ServiceUnitName,
                        query.RoomID,
                        srq.RoomName,
                        query.BedID,
                        cq.ClassName,
                        query.SRBedStatus,
                        query.RegistrationNo,
                        pq.MedicalNo.Coalesce("''"),
                        pq.PatientName.Coalesce("''"),
                        gq.GuarantorName,
                        asri.ItemName,
                        query.IsActive,
                        query.IsRoomIn
                    );

                if (cboRoomID.SelectedValue != string.Empty)
                    query.Where(srq.RoomID == cboRoomID.SelectedValue);
                if (cboClassID.SelectedValue != string.Empty)
                    query.Where(cq.ClassID == cboClassID.SelectedValue);
                if (cboBedStatus.SelectedValue != string.Empty)
                    query.Where(query.SRBedStatus == cboBedStatus.SelectedValue);
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(srq.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtSearchRegPatient.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtSearchRegPatient.Text) + "%";

                    query.Where
                        (
                          string.Format("<RTRIM(g.MedicalNo) LIKE '{0}' OR RTRIM(g.FirstName+' '+g.MiddleName)+' '+g.LastName LIKE '{0}'>", searchPatient)
                        );
                }

                query.Where(query.IsActive == true);

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        public System.Drawing.Color GetColor(object srBedStatus)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            switch (srBedStatus.ToString())
            {
                case "BedStatus-01":
                    color = System.Drawing.Color.Green;
                    break;

                case "BedStatus-02":
                    color = System.Drawing.Color.Red;
                    break;

                case "BedStatus-03":
                    color = System.Drawing.Color.Brown;
                    break;

                case "BedStatus-04":
                    color = System.Drawing.Color.Orange;
                    break;

                case "BedStatus-05":
                    color = System.Drawing.Color.Yellow;
                    break;

                case "BedStatus-06":
                    color = System.Drawing.Color.Blue;
                    break;

                case "BedStatus-07":
                    color = System.Drawing.Color.Purple;
                    break;
            }

            return color;
        }
    }
}
