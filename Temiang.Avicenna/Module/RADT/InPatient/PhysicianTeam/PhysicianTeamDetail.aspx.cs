using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class PhysicianTeamDetail : BasePageDetail
    {
        private bool _isSaveReg;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PhysicianTeamSearch.aspx?fp=dt";
            if (Request.QueryString["fr"] == "1")
                UrlPageList = "PhysicianTeamList.aspx";
            else
            {
                UrlPageList =
                    "../../../Charges/ServiceUnit/ServiceUnitTransaction/ServiceUnitRegistrationList.aspx?type=" +
                    Request.QueryString["type"] + "&resp=" + Request.QueryString["resp"] + "&disch=" +
                    Request.QueryString["disch"];
            }

            ProgramID = AppConstant.Program.PhysicianTeam;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                //(Helper.FindControlRecursive(Page, "fw_tbarData") as RadToolBar).Items[10].Enabled = false;

                ParamedicTeams = null;
            }
            WindowSearch.Height = 300;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdPhyscianTeam, grdPhyscianTeam);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            txtRegistrationNo.Text = Request.QueryString["regno"];

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);

            txtMedicalNo.Text = patient.MedicalNo;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = patient.PatientName;
            txtGender.Text = patient.Sex;
            txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);
            txtServiceUnit.Text = unit.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.RoomID);
            txtRoomBed.Text = room.RoomName + " - " + reg.BedID;

            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            txtPhysician.Text = par.ParamedicName;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (ParamedicTeams.Count == 0)
            {
                args.MessageText = "Detail team is required.";
                args.IsCancel = true;
                return;
            }

            var entity = new Registration();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Registration();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemParamedicTeam(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new Registration();
            if (parameters.Length > 0)
            {
                String regNo = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(regNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtRegistrationNo.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            using (new esTransactionScope())
            {
                var reg = (Registration) entity;
                txtRegistrationNo.Text = reg.RegistrationNo;
                var patient = new Patient();
                patient.LoadByPrimaryKey(reg.PatientID);

                txtMedicalNo.Text = patient.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = patient.PatientName;
                txtGender.Text = patient.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit.Text = unit.ServiceUnitName;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                txtRoomBed.Text = room.RoomName + " - " + reg.BedID;

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtPhysician.Text = par.ParamedicName;

                PopulateParamedicTeamGrid();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(Registration entity)
        {
            _isSaveReg = false;
            var parId = string.Empty;
            entity.RegistrationNo = txtRegistrationNo.Text;

            var lqParamedicTeams = from coll in ParamedicTeams
                                where coll.SRParamedicTeamStatus == AppSession.Parameter.ParamedicTeamStatusMain && coll.StartDate <= (new DateTime()).NowAtSqlServer()
                                orderby coll.StartDate descending 
                                select coll;

            foreach (var item in lqParamedicTeams)
            {
                if (item.EndDate == null || item.EndDate >= (new DateTime()).NowAtSqlServer())
                {
                    parId = item.ParamedicID;
                    _isSaveReg = true;
                    break;
                }
            }
            
            entity.ParamedicID = parId;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            
            foreach (ParamedicTeam item in ParamedicTeams)
            {
                item.RegistrationNo = entity.RegistrationNo;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(Registration entity)
        {
            using (var trans = new esTransactionScope())
            {
                if (_isSaveReg)
                    entity.Save();

                ParamedicTeams.Save();
                
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new RegistrationQuery("a");
            var usuQ = new AppUserServiceUnitQuery("b");
            que.InnerJoin(usuQ).On(que.ServiceUnitID == usuQ.ServiceUnitID);

            que.es.Top = 1; // SELECT TOP 1 ..
            que.Where(que.IsVoid == false, usuQ.UserID == AppSession.UserLogin.UserID,
                      que.Or(que.DischargeDate.IsNotNull(), que.SRDischargeMethod == string.Empty));
            if (isNextRecord)
            {
                que.Where
                    (
                    que.RegistrationNo > txtRegistrationNo.Text
                    );
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where
                    (
                    que.RegistrationNo < txtRegistrationNo.Text
                    );
                que.OrderBy(que.RegistrationNo.Descending);
            }

            var entity = new Registration();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }
        #endregion

        #region Record Detail Method Function ParamedicTeam

        private ParamedicTeamCollection ParamedicTeams
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collParamedicTeam"];
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
                query.OrderBy(query.SRParamedicTeamStatus.Ascending, query.StartDate.Ascending,
                              query.ParamedicID.Ascending);

                coll.Load(query);

                Session["collParamedicTeam"] = coll;
                return coll;
            }
            set { Session["collParamedicTeam"] = value; }
        }

        private void RefreshCommandItemParamedicTeam(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPhyscianTeam.Columns[0].Visible = isVisible;
            grdPhyscianTeam.Columns[grdPhyscianTeam.Columns.Count - 1].Visible = isVisible;

            grdPhyscianTeam.MasterTableView.CommandItemDisplay = isVisible
                                                                         ? GridCommandItemDisplay.Top
                                                                         : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdPhyscianTeam.Rebind();
        }

        private void PopulateParamedicTeamGrid()
        {
            //Display Data Detail
            ParamedicTeams = null; //Reset Record Detail
            grdPhyscianTeam.DataSource = ParamedicTeams; //Requery
            grdPhyscianTeam.MasterTableView.IsItemInserted = false;
            grdPhyscianTeam.MasterTableView.ClearEditItems();
            grdPhyscianTeam.DataBind();
        }

        protected void grdPhyscianTeam_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPhyscianTeam.DataSource = ParamedicTeams;
        }

        protected void grdPhyscianTeam_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String regNo =
                Convert.ToString((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ParamedicTeamMetadata.ColumnNames.RegistrationNo]);
            String parId =
                            Convert.ToString((string)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ParamedicTeamMetadata.ColumnNames.ParamedicID]);
            DateTime sDate = Convert.ToDateTime((DateTime)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                [ParamedicTeamMetadata.ColumnNames.StartDate]);
            ParamedicTeam entity = FindParamedicTeam(regNo, parId, sDate);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPhyscianTeam_DeleteCommand(object source, GridCommandEventArgs e)
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
        }

        protected void grdPhyscianTeam_InsertCommand(object source, GridCommandEventArgs e)
        {
            ParamedicTeam entity = ParamedicTeams.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPhyscianTeam.Rebind();
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
            var userControl = (PhysicianTeamDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
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
                entity.SourceType = string.IsNullOrEmpty(userControl.SourceType) ? null : userControl.SourceType;
            }
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            //ToolBarMenuEdit.Enabled = !(bool)ViewState["IsApproved"];
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler source, string argument)
        {
            base.RaisePostBackEvent(source, argument);

            if (string.IsNullOrEmpty(argument) || !(source is RadGrid))
                return;
        }
    }
}
