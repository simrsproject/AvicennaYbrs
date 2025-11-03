using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientResearchDetail : BasePageDetail
    {
        #region Page Event

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PatientResearchSearch.aspx";
            UrlPageList = "PatientResearchList.aspx";

            ProgramID = AppConstant.Program.PatientResearch;

            WindowSearch.Height = 300;
        }

        #endregion

        private void SetEntityValue(Patient entity)
        {
            foreach (PatientResearch item in PatientResearchs)
            {
                item.PatientID = entity.PatientID;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            PatientQuery que = new PatientQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PatientID > txtPatientID.Text);
                que.OrderBy(que.PatientID.Ascending);
            }
            else
            {
                que.Where(que.PatientID < txtPatientID.Text);
                que.OrderBy(que.PatientID.Descending);
            }
            Patient entity = new Patient();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Patient entity = new Patient();
            if (parameters.Length > 0)
            {
                String patientID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(patientID);
            }
            else
                entity.LoadByPrimaryKey(txtPatientID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Patient patient = (Patient)entity;

            //Populate Control
            txtPatientID.Text = patient.PatientID.TrimEnd();
            txtMedicalNo.Text = patient.MedicalNo.TrimEnd();
            txtSSN.Text = patient.Ssn;

            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = patient.PatientName;

            txtCityOfBirth.Text = patient.CityOfBirth.TrimEnd();
            txtDateOfBirth.SelectedDate = patient.DateOfBirth;
            txtGender.Text = patient.Sex;
            txtAddress.Text = patient.Address;
            txtPhoneNo.Text = patient.PhoneNo;
            txtMobilePhoneNo.Text = patient.MobilePhoneNo;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Patient());
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
            //auditLogFilter.PrimaryKeyData = string.Format("PatientID='{0}'", txtPatientID.Text.Trim());
            //auditLogFilter.TableName = "Patient";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Patient entity = new Patient();
            if (entity.LoadByPrimaryKey(txtPatientID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new Patient();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Patient entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                
                PatientResearchs.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Patient entity = new Patient();
            if (entity.LoadByPrimaryKey(txtPatientID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region Record Detail Method Function

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdPatientResearch.Columns[0].Visible = isVisible;
            grdPatientResearch.Columns[grdPatientResearch.Columns.Count - 1].Visible = isVisible;

            grdPatientResearch.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                PatientResearchs = null;

            //Perbaharui tampilan dan data
            grdPatientResearch.Rebind();
        }

        private PatientResearchCollection PatientResearchs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientResearch"];
                    if (obj != null)
                        return ((PatientResearchCollection)(obj));
                }

                var coll = new PatientResearchCollection();
                var query = new PatientResearchQuery("a");
                var par = new ParamedicQuery("b");
                query.InnerJoin(par).On(par.ParamedicID == query.ParamedicID);
                query.Select
                    (
                        query,
                        par.ParamedicName.As("refToParamedic_ParamedicName")
                     );
                query.Where(query.PatientID == txtPatientID.Text);
                query.OrderBy(query.StartDate.Ascending);
                coll.Load(query);

                Session["collPatientResearch"] = coll;
                return coll;
            }
            set
            {
                Session["collPatientResearch"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            PatientResearchs = null; //Reset Record Detail
            grdPatientResearch.DataSource = PatientResearchs;
            grdPatientResearch.DataBind();
        }

        protected void grdPatientResearch_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPatientResearch.DataSource = PatientResearchs;
        }

        protected void grdPatientResearch_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 id = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientResearchMetadata.ColumnNames.ResearchID]);
            var entity = FindPatientResearch(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdPatientResearch_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientResearchMetadata.ColumnNames.ResearchID]);
            var entity = FindPatientResearch(id);
            if (entity != null)
                SetEntityValue(entity, e);
            //entity.MarkAsDeleted();
        }

        protected void grdPatientResearch_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = PatientResearchs.AddNew();
            SetEntityValue(entity, e);
        }

        private PatientResearch FindPatientResearch(Int32 id)
        {
            PatientResearchCollection coll = PatientResearchs;
            return coll.FirstOrDefault(rec => rec.ResearchID.Equals(id));
        }

        private void SetEntityValue(PatientResearch entity, GridCommandEventArgs e)
        {
            var userControl = (PatientResearchItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PatientID = txtPatientID.Text;
                entity.ResearchID = userControl.ResearchID;
                entity.ResearchTitle = userControl.ResearchTitle;
                entity.StartDate = userControl.StartDate;
                entity.EndDate = userControl.EndDate;
                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.Notes = userControl.Notes;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        #endregion
    }
}