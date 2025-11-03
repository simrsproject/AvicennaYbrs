using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;
using System.Web.UI;
using System.Text;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PhysicianTeamDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["reg"];
                PopulatePatientInfo();
                this.Title = "Substitute Physician";
            }
        }

        private void PopulatePatientInfo()
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                txtRegistrationNo.Text = reg.RegistrationNo;
                txtMedicalNo.Text = patient.MedicalNo;
                txtPatientName.Text = patient.PatientName;

                optSexMale.Checked = (patient.Sex == "M");
                optSexMale.Enabled = (patient.Sex == "M");
                optSexFemale.Checked = (patient.Sex == "F");
                optSexFemale.Enabled = (patient.Sex == "F");

                txtFromServiceUnitID.Text = reg.ServiceUnitID;
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);

                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(reg.RoomID);

                txtServiceUnit.Text = su.ServiceUnitName;
                txtRoomName.Text = sr.RoomName;
                txtBedID.Text = reg.BedID;

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtPhysician.Text = par.ParamedicName;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ParamedicTeams;
        }

        protected void grdList_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicTeam entity = ParamedicTeams.AddNew();

            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                ParamedicTeams.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //grid not close first
            e.Canceled = true;
            grdList.Rebind();
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            String regNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicTeamMetadata.ColumnNames.RegistrationNo]);
            String parId =
                            Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicTeamMetadata.ColumnNames.ParamedicID]);
            DateTime sDate =
                Convert.ToDateTime(item.OwnerTableView.DataKeyValues[item.ItemIndex][ParamedicTeamMetadata.ColumnNames.StartDate]);
            ParamedicTeam entity = FindParamedicTeam(regNo, parId, sDate);

            if (entity != null)
            {
                entity.MarkAsDeleted();
            }

            using (var trans = new esTransactionScope())
            {
                ParamedicTeams.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private ParamedicTeam FindParamedicTeam(String regNo, String parId, DateTime sDate)
        {
            ParamedicTeamCollection coll = ParamedicTeams;
            ParamedicTeam retEntity = null;
            foreach (ParamedicTeam rec in coll)
            {
                if (rec.RegistrationNo.Equals(regNo) && rec.ParamedicID.Equals(parId) && rec.StartDate.Equals(sDate))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(ParamedicTeam entity, GridCommandEventArgs e)
        {
            var userControl = (PhysicianTeamDialogDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.ParamedicID = userControl.ParamedicID;

                entity.SRParamedicTeamStatus = userControl.SRParamedicTeamStatus;
                entity.StartDate = userControl.StartDate;
                if (userControl.EndDate == null)
                    entity.str.EndDate = string.Empty;
                else
                    entity.EndDate = userControl.EndDate;
                entity.Notes = userControl.Notes;

                var par = new Paramedic();
                par.LoadByPrimaryKey(entity.ParamedicID);
                entity.ParamedicName = par.ParamedicName;

                var asri = new AppStandardReferenceItem();
                asri.LoadByPrimaryKey("ParamedicTeamStatus", entity.SRParamedicTeamStatus);
                entity.ParamedicTeamStatus = asri.ItemName;
            }
        }

        private ParamedicTeamCollection ParamedicTeams
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicTeam" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((ParamedicTeamCollection)(obj));
                    }
                }

                var coll = new ParamedicTeamCollection();
                var query = new ParamedicTeamQuery("a");
                var srQuery = new AppStandardReferenceItemQuery("b");
                var parQuery = new ParamedicQuery("c");

                query.Select
                    (
                        query,
                        srQuery.ItemName.As("refToAppStandardReferenceItem_ParamedicTeamStatus"),
                        parQuery.ParamedicName.As("refToParamedic_ParamedicName")
                    );
                query.InnerJoin(srQuery).On(query.SRParamedicTeamStatus == srQuery.ItemID &&
                                            srQuery.StandardReferenceID == "ParamedicTeamStatus");
                query.InnerJoin(parQuery).On(query.ParamedicID == parQuery.ParamedicID);
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.StartDate.Ascending, query.SRParamedicTeamStatus.Ascending,
                              query.ParamedicID.Ascending);

                coll.Load(query);

                Session["collParamedicTeam" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collParamedicTeam" + Request.UserHostName] = value; }
        }


        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}
