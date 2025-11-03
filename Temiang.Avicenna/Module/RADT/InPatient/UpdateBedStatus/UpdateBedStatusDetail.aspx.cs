using System;
using System.Text;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Linq;


namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class UpdateBedStatusDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "";
            UrlPageList = "UpdateBedStatusList.aspx";

            ProgramID = AppConstant.Program.UpdateBedStatusForPatientSurgery;

            if (!IsPostBack)
            {
                var srq = new AppStandardReferenceItemQuery();
                srq.Where(srq.StandardReferenceID == AppEnum.StandardReference.BedStatus, srq.IsActive == true,
                          srq.ReferenceID == "Empty");

                DataTable dtbsr = srq.LoadDataTable();

                foreach (DataRow row in dtbsr.Rows)
                {
                    cboBedStatus.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
                }

                PopulateEntryControl(Request.QueryString["id"]);

                trIsRoomIn.Visible = AppSession.Parameter.IsUsingRoomingIn;
            }

        }

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new Bed();
            var history = new BedStatusHistory();
            
            if (entity.LoadByPrimaryKey(txtBedID.Text))
            {
                var briColl = new BedRoomInCollection();
                briColl.Query.Where(briColl.Query.BedID == txtBedID.Text, briColl.Query.DateOfExit.IsNull(),
                                    briColl.Query.IsVoid == false);
                briColl.LoadAll();

                if (!string.IsNullOrEmpty(entity.RegistrationNo))
                {
                    if (!chkIsRoomIn.Checked)
                    {
                        if (briColl.Count == 0)
                        {
                            if (entity.SRBedStatus == AppSession.Parameter.BedStatusOccupied && cboBedStatus.SelectedValue == AppSession.Parameter.BedStatusGoToOperatingRoom)
                            {
                                SetEntityValue(entity, history);
                                SaveEntity(entity, history);
                            }
                            else if (entity.SRBedStatus == AppSession.Parameter.BedStatusGoToOperatingRoom && cboBedStatus.SelectedValue == AppSession.Parameter.BedStatusOccupied)
                            {
                                SetEntityValue(entity, history);
                                SaveEntity(entity, history);
                            }
                            else
                                args.MessageText = "Bed Status can't be changed because has registered to patient";
                        }
                        else
                            args.MessageText = "Bed Status can't be changed because has patient room in";
                    }
                    else
                    {
                        if (briColl.Count > 0)
                        {
                            if (entity.SRBedStatus == AppSession.Parameter.BedStatusOccupied && cboBedStatus.SelectedValue == AppSession.Parameter.BedStatusGoToOperatingRoom)
                            {
                                SetEntityValue(entity, history);
                                SaveEntity(entity, history);
                            }
                            else if (entity.SRBedStatus == AppSession.Parameter.BedStatusGoToOperatingRoom && cboBedStatus.SelectedValue == AppSession.Parameter.BedStatusOccupied)
                            {
                                SetEntityValue(entity, history);
                                SaveEntity(entity, history);
                            }
                            else
                                args.MessageText = "Bed Status can't be changed because has registered to patient";
                        }
                        else
                            args.MessageText = "Bed Status can't be changed because there is no patient room in";
                    }
                }
                else
                {
                    if (!chkIsRoomIn.Checked)
                    {
                        if (briColl.Count == 0)
                        {
                            if (cboBedStatus.SelectedValue == AppSession.Parameter.BedStatusOccupied || cboBedStatus.SelectedValue == AppSession.Parameter.BedStatusGoToOperatingRoom)
                            {
                                args.MessageText = "Bed Status can't be changed because no patient has registered";
                            }
                            else
                            {
                                SetEntityValue(entity, history);
                                SaveEntity(entity, history);
                            }
                        }
                        else
                            args.MessageText = "Bed Status can't be changed because has patient room in";
                    }
                    else
                    {
                        if (briColl.Count > 0)
                        {
                            if (cboBedStatus.SelectedValue == AppSession.Parameter.BedStatusOccupied || cboBedStatus.SelectedValue == AppSession.Parameter.BedStatusGoToOperatingRoom)
                            {
                                args.MessageText = "Bed Status can't be changed because no patient has registered";
                            }
                            else
                            {
                                SetEntityValue(entity, history);
                                SaveEntity(entity, history);
                            }
                        }
                        else
                            args.MessageText = "Bed Status can't be changed because there is no patient room in";
                    }

                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {

        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {

        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {

        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuEditClick()
        {
            //InitializedQuestion();
        }

        #endregion

        private void SetEntityValue(esBed bed, esBedStatusHistory history)
        {
            bed.LoadByPrimaryKey(txtBedID.Text);
            var srBedStatusBefore = bed.SRBedStatus;
            bed.SRBedStatus = cboBedStatus.SelectedValue;
            bed.IsRoomIn = chkIsRoomIn.Checked;

            if (bed.es.IsAdded || bed.es.IsModified)
            {
                bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                bed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            history.AddNew();
            history.BedID = bed.BedID;
            history.SRBedStatusFrom = srBedStatusBefore;
            history.SRBedStatusTo = bed.SRBedStatus;
            history.RegistrationNo = string.Empty;
            history.TransferNo = string.Empty;
            history.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            history.LastUpdateByUserID = AppSession.UserLogin.UserID;
        }

        private void PopulateEntryControl(String bedId)
        {
            string guarId, parId, suId, clId;
            txtBedID.Text = bedId;

            var bed = new Bed();
            bed.LoadByPrimaryKey(bedId);
            txtRegistrationNo.Text = bed.RegistrationNo;

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                txtPatientID.Text = reg.PatientID;
                guarId = reg.GuarantorID;
                parId = reg.ParamedicID;
                suId = reg.ServiceUnitID;
                clId = reg.ClassID;
            }
            else
            {
                txtPatientID.Text = string.Empty;
                guarId = string.Empty;
                parId = string.Empty;
                suId = string.Empty;
                clId = string.Empty;
            }

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(txtPatientID.Text))
            {
                txtMedicalNo.Text = patient.MedicalNo;
                txtPatientName.Text = patient.PatientName;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
            }

            var guarr = new Guarantor();
            guarr.LoadByPrimaryKey(guarId);
            txtGuarantor.Text = guarr.GuarantorName;

            var par = new Paramedic();
            par.LoadByPrimaryKey(parId);
            txtParamedic.Text = par.ParamedicName;

            var sup = new ServiceUnit();
            sup.LoadByPrimaryKey(suId);
            txtServiceUnitIDPatient.Text = sup.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(bed.RoomID);
            txtRoom.Text = room.RoomName;

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(room.ServiceUnitID);
            txtServiceUnit.Text = su.ServiceUnitName;

            var cl = new Class();
            cl.LoadByPrimaryKey(bed.ClassID);
            txtClass.Text = cl.ClassName;
            cboBedStatus.SelectedValue = bed.SRBedStatus;
            chkIsRoomIn.Checked = bed.IsRoomIn ?? false;

            var clp = new Class();
            clp.LoadByPrimaryKey(clId);
            txtClassIDPatient.Text = clp.ClassName;
        }

        private void SaveEntity(Bed bed, BedStatusHistory history)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                bed.Save();
                history.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
    }
}
