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
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientBirthRecordDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PatientBirthRecord;

            UrlPageList = "PatientBirthRecordList.aspx";

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboBirth, AppEnum.StandardReference.SingleTwin);
                StandardReference.InitializeIncludeSpace(cboBirthMethod, AppEnum.StandardReference.BirthMethod);
                StandardReference.InitializeIncludeSpace(cboCaesarMethod, AppEnum.StandardReference.CaesarMethod);
                StandardReference.InitializeIncludeSpace(cboBornCondition, AppEnum.StandardReference.BornCondition);
                StandardReference.InitializeIncludeSpace(cboOccupation, AppEnum.StandardReference.Occupation);
                StandardReference.InitializeIncludeSpace(cboSRBornAt, AppEnum.StandardReference.BornAt);
                StandardReference.InitializeIncludeSpace(cboSRBirthComplication, AppEnum.StandardReference.BirthComplication);
                StandardReference.InitializeIncludeSpace(cboSRDeathCondition, AppEnum.StandardReference.DeathCondition);
                StandardReference.InitializeIncludeSpace(cboSRBornDeath, AppEnum.StandardReference.BornDeath);
                StandardReference.InitializeIncludeSpace(cboSRBirthIndication, AppEnum.StandardReference.BirthIndication);

                BirthAttendantsRecords = null;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(txtMotherRegistrationNo, txtMotherMedicalNo);
            ajax.AddAjaxSetting(txtMotherRegistrationNo, txtMotherName);
            ajax.AddAjaxSetting(txtMotherRegistrationNo, txtAgeYear);
            ajax.AddAjaxSetting(txtMotherRegistrationNo, txtAgeMonth);
            ajax.AddAjaxSetting(txtMotherRegistrationNo, txtAgeDay);
            ajax.AddAjaxSetting(txtMotherRegistrationNo, txtParamedicName);
            ajax.AddAjaxSetting(txtMotherRegistrationNo, txtServiceUnitName);
            ajax.AddAjaxSetting(rblEyesSmeared, txtEyesSmearedNotes);
            ajax.AddAjaxSetting(rblAnusExamined, txtAnusExaminedNotes);
            ajax.AddAjaxSetting(rblEyesSmeared, rblEyesSmeared);
            ajax.AddAjaxSetting(rblAnusExamined, rblAnusExamined);
            ajax.AddAjaxSetting(grdBirthAttendants, grdBirthAttendants);

            ajax.AddAjaxSetting(AjaxManager, txtMotherRegistrationNo);
            ajax.AddAjaxSetting(AjaxManager, grdBirthAttendants);
        }

        private void PopulateBabyInfo()
        {
            if (txtBabyRegistrationNo.Text == string.Empty)
                return;

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtBabyRegistrationNo.Text);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            txtBabyMedicalNo.Text = pat.MedicalNo;
            txtBabyName.Text = pat.PatientName;
            txtBirthDate.SelectedDate = pat.DateOfBirth;
            
            txtFatherName.Text = pat.FatherName;
            txtSSN.Text = pat.FatherSsn;
            cboOccupation.SelectedValue = pat.SRFatherOccupation;

            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            txtBabyPhysician.Text = par.ParamedicName;

            var sid = new ServiceUnit();
            sid.LoadByPrimaryKey(reg.ServiceUnitID);
            var sr = new ServiceRoom();
            sr.LoadByPrimaryKey(reg.RoomID);

            txtBabyServiceUnit.Text = sid.ServiceUnitName + " / " + sr.RoomName + " / " + reg.BedID;
        }

        private void PopulateMotherInfo()
        {
            if (txtMotherRegistrationNo.Text == string.Empty)
            {
                txtMotherMedicalNo.Text = string.Empty;
                txtMotherName.Text = string.Empty;
                txtAgeYear.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeDay.Value = 0;
                txtParamedicName.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;
                return;
            }


            var mreg = new Registration();
            if (mreg.LoadByPrimaryKey(txtMotherRegistrationNo.Text))
            {
                var mpat = new Patient();
                mpat.LoadByPrimaryKey(mreg.PatientID);
                txtMotherMedicalNo.Text = mpat.MedicalNo;
                txtMotherName.Text = mpat.PatientName;

                txtAgeYear.Text = mreg.AgeInYear.ToString();
                txtAgeMonth.Text = mreg.AgeInMonth.ToString();
                txtAgeDay.Text = mreg.AgeInDay.ToString();

                var par = new Paramedic();
                par.LoadByPrimaryKey(mreg.ParamedicID);
                txtParamedicName.Text = par.ParamedicName;

                var sid = new ServiceUnit();
                sid.LoadByPrimaryKey(mreg.ServiceUnitID);
                var sr = new ServiceRoom();
                sr.LoadByPrimaryKey(mreg.RoomID);

                txtServiceUnitName.Text = sid.ServiceUnitName + " / " + sr.RoomName + " / " + mreg.BedID;
            }
            else
            {
                txtMotherMedicalNo.Text = string.Empty;
                txtMotherName.Text = string.Empty;
                txtAgeYear.Value = 0;
                txtAgeMonth.Value = 0;
                txtAgeDay.Value = 0;
                txtParamedicName.Text = string.Empty;
                txtServiceUnitName.Text = string.Empty;
            }

        }

        protected override void OnMenuNewClick()
        {
            
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            
            printJobParameters.AddNew("RegistrationNo", txtBabyRegistrationNo.Text);
            printJobParameters.AddNew("HealthcareID", AppSession.Parameter.HealthcareID);
  

        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new BirthRecord();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(parameters[0]);
            }
            else
                entity.LoadByPrimaryKey(txtBabyRegistrationNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var birthRecord = (BirthRecord)entity;
            txtBabyRegistrationNo.Text = birthRecord.RegistrationNo;
            PopulateBabyInfo();

            txtMotherRegistrationNo.Text = birthRecord.MotherRegistrationNo;
            PopulateMotherInfo();

            txtBirthTime.Text = birthRecord.TimeOfBirth;
            cboSRBornAt.SelectedValue = birthRecord.SRBornAt;
            txtDescription.Text = birthRecord.BornAtDescription;
            cboBirth.SelectedValue = birthRecord.SRSingleTwin;
            if (!string.IsNullOrEmpty(birthRecord.TwinNo))
                txtTwinNumber.Text = birthRecord.TwinNo.Trim();
            else
                txtTwinNumber.Text = string.Empty;
            cboBirthMethod.SelectedValue = birthRecord.SRBirthMethod;
            cboCaesarMethod.SelectedValue = birthRecord.SRCaesarMethod;
            cboBornCondition.SelectedValue = birthRecord.SRBornCondition;
            cboSRBirthComplication.SelectedValue = birthRecord.SRBirthComplication;
            cboSRDeathCondition.SelectedValue = birthRecord.SRDeathCondition;
            cboSRBornDeath.SelectedValue = birthRecord.SRBornDeath;
            chkIsKangarooMethod.Checked = birthRecord.IsKangarooMethod ?? false;
            chkIsIMD.Checked = birthRecord.IsIMD ?? false;
            chkIsCongenitalHyperthyroidism.Checked = birthRecord.IsCongenitalHyperthyroidism ?? false ;
            cboSRBirthIndication.SelectedValue = birthRecord.SRBirthComplication;

            txtBirthPregnancyAge.Value = (double)(birthRecord.BirthPregnancyAge ?? 0);
            txtLength.Value = (double)(birthRecord.Length ?? 0);
            txtWeight.Value = (double)(birthRecord.Weight ?? 0);
            txtHeadCircumference.Value = (double)(birthRecord.HeadCircumference ?? 0);
            txtChestCircumference.Value = (double)(birthRecord.ChestCircumference ?? 0);
            txtAbdomenCircumference.Value = (double)(birthRecord.AbdomenCircumference ?? 0);
            txtAPGARScore1.Value = (double)(birthRecord.ApgarScore1 ?? 0);
            txtAPGARScore2.Value = (double)(birthRecord.ApgarScore2 ?? 0);
            txtAPGARScore3.Value = (double)(birthRecord.ApgarScore3 ?? 0);
            if (birthRecord.IsEyesSmeared ?? false)
                rblEyesSmeared.SelectedIndex = 1;
            else
                rblEyesSmeared.SelectedIndex = 0;
            txtEyesSmearedNotes.Text = birthRecord.EyesSmearedNotes;
            txtEyesSmearedNotes.Enabled = rblEyesSmeared.SelectedIndex == 1;
            if (birthRecord.IsAnusExamined ?? false)
                rblAnusExamined.SelectedIndex = 1;
            else
                rblAnusExamined.SelectedIndex = 0;
            txtAnusExaminedNotes.Text = birthRecord.AnusExaminedNotes;
            txtAnusExaminedNotes.Enabled = rblAnusExamined.SelectedIndex == 1;
            txtNotes.Text = birthRecord.Notes;
            txtCertNumber.Text = birthRecord.CertificateNo;

            txtFatherName.Text = birthRecord.FatherName;
            txtSSN.Text = birthRecord.FatherSsn;
            cboOccupation.SelectedValue = birthRecord.SROccupation;
            ctlAddress.StreetName = birthRecord.StreetName;
            txtFatherBirthDate.SelectedDate = birthRecord.FatherBirthOfDate;
            txtChildNo.Value = Convert.ToDouble(birthRecord.ChildNo);

            var zip = new ZipCodeQuery();
            zip.Where(zip.ZipCode == birthRecord.str.ZipCode);

            ctlAddress.ZipCodeCombo.DataSource = zip.LoadDataTable();
            ctlAddress.ZipCodeCombo.DataBind();

            bool exist = false;
            foreach (RadComboBoxItem item in ctlAddress.ZipCodeCombo.Items)
            {
                if (item.Value == birthRecord.str.ZipCode)
                {
                    exist = true;
                    break;
                }
            }

            if (exist)
                ctlAddress.ZipCodeCombo.SelectedValue = birthRecord.str.ZipCode;
            else
                ctlAddress.ZipCodeCombo.Text = birthRecord.str.ZipCode;

            ctlAddress.District = birthRecord.District;
            ctlAddress.County = birthRecord.County;
            ctlAddress.City = birthRecord.City;
            ctlAddress.State = birthRecord.State;
            ctlAddress.PhoneNo = birthRecord.PhoneNo;
            ctlAddress.FaxNo = birthRecord.FaxNo;
            ctlAddress.MobilePhoneNo = birthRecord.MobilePhoneNo;
            ctlAddress.Email = birthRecord.Email;

            PopulateGridDetail();
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("RegistrationNo='{0}'", txtBabyRegistrationNo.Text.Trim());
            auditLogFilter.TableName = "BirthRecord";
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            //dk:24-07-2024
            //kalo nambahin validasi, di appparameter u/ parametername jangan lupa ditambahin juga ya.. biar tau yg sudah bisa disetting apa aja
            var app = AppSession.Parameter.TablePatientBirthRecordFieldValidation;
            if (!string.IsNullOrEmpty(app))
            {
                if (app.Contains("SRBornAt"))
                {
                    rfvBornAt.Visible = true;
                }
                if (app.Contains("SRSingleTwin"))
                {
                    rfvBirth.Visible = true;
                }
                if (app.Contains("SRBirthMethod"))
                {
                    rfvBirthMethod.Visible = true;
                }
                if (app.Contains("SRBornCondition"))
                {
                    rfvBornCondition.Visible = true;
                }
            }

            if (txtWeight.Value <= 0)
            {
                args.MessageText = "Body Weight required.";
                args.IsCancel = true;
                return;
            }
            if (txtLength.Value <= 0)
            {
                args.MessageText = "Body Length required.";
                args.IsCancel = true;
                return;
            }
            
            if (cboBornCondition.SelectedValue == "05" && string.IsNullOrEmpty(cboSRDeathCondition.SelectedValue))
            {
                args.MessageText = "Death Condition required.";
                args.IsCancel = true;
                return;
            }

            if (!string.IsNullOrEmpty(txtTwinNumber.Text))
            {
                int twinNumberInt = 0;
                string twinNumberStr = txtTwinNumber.Text.Trim();
                if (!string.IsNullOrWhiteSpace(twinNumberStr) && !int.TryParse(twinNumberStr, out twinNumberInt))
                {
                    args.MessageText = "Invalid Twin Number (must be filled in with numbers).";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new BirthRecord();
            if (entity.LoadByPrimaryKey(txtBabyRegistrationNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        private void SetEntityValue(BirthRecord entity)
        {
            entity.RegistrationNo = txtBabyRegistrationNo.Text;
            entity.MotherMedicalNo = txtMotherMedicalNo.Text;
            entity.MotherRegistrationNo = txtMotherRegistrationNo.Text;
            entity.TimeOfBirth = txtBirthTime.TextWithLiterals;
            entity.SRBornAt = cboSRBornAt.SelectedValue;
            entity.BornAtDescription = txtDescription.Text;
            entity.SRSingleTwin = cboBirth.SelectedValue;
            entity.TwinNo = txtTwinNumber.Text;
            entity.SRBirthMethod = cboBirthMethod.SelectedValue;
            entity.SRCaesarMethod = cboCaesarMethod.SelectedValue;
            entity.SRBornCondition = cboBornCondition.SelectedValue;
            entity.SRBirthComplication = cboSRBirthComplication.SelectedValue;
            entity.SRDeathCondition = cboSRDeathCondition.SelectedValue;
            entity.SRBornDeath = cboSRBornDeath.SelectedValue;
            entity.SRBirthIndication = cboSRBirthIndication.SelectedValue;
            entity.BirthPregnancyAge = (decimal)(txtBirthPregnancyAge.Value ?? 0);
            entity.Length = (decimal)(txtLength.Value ?? 0);
            entity.Weight = (decimal)(txtWeight.Value ?? 0);
            entity.ApgarScore1 = (decimal)(txtAPGARScore1.Value ?? 0);
            entity.ApgarScore2 = (decimal)(txtAPGARScore2.Value ?? 0);
            entity.ApgarScore3 = (decimal)(txtAPGARScore3.Value ?? 0);
            entity.HeadCircumference = (decimal)(txtHeadCircumference.Value ?? 0);
            entity.ChestCircumference = (decimal)(txtChestCircumference.Value ?? 0);
            entity.AbdomenCircumference = (decimal)(txtAbdomenCircumference.Value ?? 0);
            entity.IsEyesSmeared = rblEyesSmeared.SelectedIndex == 1;
            entity.EyesSmearedNotes = txtEyesSmearedNotes.Text;
            entity.IsAnusExamined = rblEyesSmeared.SelectedIndex == 1;
            entity.AnusExaminedNotes = txtAnusExaminedNotes.Text;
            entity.Notes = txtNotes.Text;
            entity.CertificateNo = txtCertNumber.Text;
            entity.FatherName = txtFatherName.Text;
            entity.FatherSsn = txtSSN.Text;
            entity.FatherBirthOfDate = txtFatherBirthDate.SelectedDate;
            entity.StreetName = ctlAddress.StreetName;
            entity.District = ctlAddress.District;
            entity.City = ctlAddress.City;
            entity.County = ctlAddress.City;
            entity.State = ctlAddress.State;
            entity.str.ZipCode = ctlAddress.ZipCode;
            entity.SROccupation = cboOccupation.SelectedValue;
            entity.PhoneNo = ctlAddress.PhoneNo;
            entity.FaxNo = ctlAddress.FaxNo;
            entity.MobilePhoneNo = ctlAddress.MobilePhoneNo;
            entity.Email = ctlAddress.Email;
            entity.ChildNo = Convert.ToInt16(txtChildNo.Value);
            entity.IsKangarooMethod = chkIsKangarooMethod.Checked;
            entity.IsIMD = chkIsIMD.Checked;
            entity.IsCongenitalHyperthyroidism = chkIsCongenitalHyperthyroidism.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var bar in BirthAttendantsRecords)
            {
                bar.RegistrationNo = entity.RegistrationNo;
                bar.LastUpdateByUserID = AppSession.UserLogin.UserID;
                bar.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

        }

        private void SaveEntity(BirthRecord entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                BirthAttendantsRecords.Save();

                var reg = new Registration();
                if (reg.LoadByPrimaryKey(entity.RegistrationNo))
                {
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(reg.PatientID))
                    {
                        pat.DateOfBirth = txtBirthDate.SelectedDate;
                        pat.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;

            //dk:24-07-2024
            //kalo nambahin validasi, di appparameter u/ parametername jangan lupa ditambahin juga ya.. biar tau yg sudah bisa disetting apa aja
            var app = AppSession.Parameter.TablePatientBirthRecordFieldValidation;
            if (!string.IsNullOrEmpty(app))
            {
                if (app.Contains("SRBornAt"))
                {
                    rfvBornAt.Visible = true;
                }
                if (app.Contains("SRSingleTwin"))
                {
                    rfvBirth.Visible = true;
                }
                if (app.Contains("SRBirthMethod"))
                {
                    rfvBirthMethod.Visible = true;
                }
                if (app.Contains("SRBornCondition"))
                {
                    rfvBornCondition.Visible = true;
                }
                if (app.Contains("SRBirthComplication"))
                {
                    rfvBirthComplication.Visible = true;
                }
                if (app.Contains("FatherName"))
                {
                    rfvFatherName.Visible = true;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BirthRecordQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RegistrationNo > txtBabyRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where(que.RegistrationNo < txtBabyRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Descending);
            }
            var entity = new BirthRecord();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        protected void txtMotherRegistrationNo_TextChanged(object sender, EventArgs e)
        {
            PopulateMotherInfo();
        }

        protected void rblEyesSmeared_OnTextChanged(object sender, EventArgs e)
        {
            txtEyesSmearedNotes.Enabled = rblEyesSmeared.SelectedIndex == 1;
            txtEyesSmearedNotes.Text = string.Empty;
        }

        protected void rblAnusExamined_OnTextChanged(object sender, EventArgs e)
        {
            txtAnusExaminedNotes.Enabled = rblAnusExamined.SelectedIndex == 1;
            txtAnusExaminedNotes.Text = string.Empty;
        }

        #region Record Detail Method Function

        private BirthAttendantsRecordCollection BirthAttendantsRecords
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collBirthAttendantsRecord"];
                    if (obj != null)
                        return ((BirthAttendantsRecordCollection)(obj));
                }

                var coll = new BirthAttendantsRecordCollection();
                var query = new BirthAttendantsRecordQuery("a");
                var pq = new ParamedicQuery("b");
                var qSr = new AppStandardReferenceItemQuery("c");
                var qSr2 = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        pq.ParamedicName.As("refToParamedic_ParamedicName"),
                        qSr.ItemName.As("refToAppStandardReferenceItem_ItemNameMidwivesType"),
                        qSr2.ItemName.As("refToAppStandardReferenceItem_ItemNameParamedicType")
                    );

                query.InnerJoin(pq).On(query.ParamedicID == pq.ParamedicID);
                query.LeftJoin(qSr).On
                    (
                        query.SRMidwivesType == qSr.ItemID &
                        qSr.StandardReferenceID == AppEnum.StandardReference.MidwivesType
                    );
                query.InnerJoin(qSr2).On
                    (
                        pq.SRParamedicType == qSr2.ItemID &
                        qSr2.StandardReferenceID == AppEnum.StandardReference.ParamedicType
                    );

                query.Where(query.RegistrationNo == txtBabyRegistrationNo.Text);

                query.OrderBy(query.ParamedicID, esOrderByDirection.Ascending);

                coll.Load(query);

                Session["collBirthAttendantsRecord"] = coll;
                return coll;
            }
            set { Session["collBirthAttendantsRecord"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdBirthAttendants.Columns[0].Visible = isVisible;
            grdBirthAttendants.Columns[grdBirthAttendants.Columns.Count - 1].Visible = isVisible;

            grdBirthAttendants.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                BirthAttendantsRecords = null;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdBirthAttendants.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            BirthAttendantsRecords = null; //Reset Record Detail
            grdBirthAttendants.DataSource = BirthAttendantsRecords;
            grdBirthAttendants.MasterTableView.IsItemInserted = false;
            grdBirthAttendants.MasterTableView.ClearEditItems();
            grdBirthAttendants.DataBind();
        }

        protected void grdBirthAttendants_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdBirthAttendants.DataSource = BirthAttendantsRecords;
        }

        protected void grdBirthAttendants_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String parId =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        BirthAttendantsRecordMetadata.ColumnNames.ParamedicID]);
            BirthAttendantsRecord entity = FindItemGrid(parId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdBirthAttendants_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String parId =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][BirthAttendantsRecordMetadata.ColumnNames.ParamedicID]);
            BirthAttendantsRecord entity = FindItemGrid(parId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdBirthAttendants_InsertCommand(object source, GridCommandEventArgs e)
        {
            BirthAttendantsRecord entity = BirthAttendantsRecords.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdBirthAttendants.Rebind();
        }

        private void SetEntityValue(BirthAttendantsRecord entity, GridCommandEventArgs e)
        {
            var userControl = (BirthAttendantsDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ParamedicID = userControl.ParamedicId;
                entity.ParamedicName = userControl.ParamedicName;
                entity.SRMidwivesType = userControl.SrMidwivesType;
                entity.MidwivesType = userControl.MidwivesType;

                var par = new Paramedic();
                par.LoadByPrimaryKey(entity.ParamedicID);

                var std = new AppStandardReferenceItem();
                std.LoadByPrimaryKey(AppEnum.StandardReference.ParamedicType.ToString(), par.SRParamedicType);

                entity.ParamedicType = std.ItemName;
                entity.Notes = userControl.Notes;
            }
        }

        private BirthAttendantsRecord FindItemGrid(string parId)
        {
            BirthAttendantsRecordCollection coll = BirthAttendantsRecords;
            BirthAttendantsRecord retval = null;
            foreach (BirthAttendantsRecord rec in coll)
            {
                if (rec.ParamedicID.Equals(parId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion
    }
}
