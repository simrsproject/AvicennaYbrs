using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientRelatedDetail : BasePageDetail
    {
        #region Page Event

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PatientRelatedSearch.aspx";
            UrlPageList = "PatientRelatedList.aspx";

            ProgramID = AppConstant.Program.PatientRelated;

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRSalutation, AppEnum.StandardReference.Salutation);
        }

        #endregion

        private void SetEntityValue(Patient entity)
        {
            entity.PatientID = txtPatientID.Text;
            entity.Ssn = txtSSN.Text;
            entity.ParentSpouseName = txtParentSpouseName.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (PatientRelated item in PatientRelateds)
            {
                item.PatientID = entity.PatientID;
                if (item.LastUpdateByUserID != "delete")
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
            cboSRSalutation.SelectedValue = patient.SRSalutation;
            txtFirstName.Text = patient.FirstName.TrimEnd();
            txtMiddleName.Text = patient.MiddleName.TrimEnd();
            txtLastName.Text = patient.LastName.TrimEnd();
            txtCityOfBirth.Text = patient.CityOfBirth.TrimEnd();
            txtDateOfBirth.SelectedDate = patient.DateOfBirth;
            rbtSex.SelectedValue = patient.Sex;
            txtParentSpouseName.Text = patient.ParentSpouseName.TrimEnd();

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
            auditLogFilter.PrimaryKeyData = string.Format("PatientID='{0}'", txtPatientID.Text.Trim());
            auditLogFilter.TableName = "Patient";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Patient entity = new Patient();
            entity.LoadByPrimaryKey(txtPatientID.Text);
            entity.MarkAsDeleted();

            PatientRelatedCollection collDt = new PatientRelatedCollection();
            collDt.Query.Where(collDt.Query.PatientID == txtPatientID.Text);
            collDt.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                collDt.Save();
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
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
                entity.Save();

                foreach (var p in PatientRelateds)
                {
                    if (p.LastUpdateByUserID == "delete")
                    {
                        var patient = new Patient();
                        patient.LoadByPrimaryKey(p.RelatedPatientID);
                        patient.IsActive = true;
                        patient.Notes = string.Empty;
                        patient.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        patient.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        patient.Save();
                        p.MarkAsDeleted();
                    }
                }

                PatientRelateds.Save();

                foreach (Patient p in from pr in PatientRelateds let p = new Patient() where p.LoadByPrimaryKey(pr.RelatedPatientID) select p)
                {
                    p.IsActive = false;
                    p.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    p.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    if (string.IsNullOrEmpty(p.Notes))
                        p.Notes = "This MRN has been merged into MRN: " + entity.MedicalNo;
                    else
                    {
                        var note = p.Notes;
                        p.Notes = "This MRN has been merged into MRN: " + entity.MedicalNo +"; " + note;
                    }
                    
                    p.Save();
                }

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
            grdPatientRelated.Columns[0].Visible = isVisible;
            grdPatientRelated.Columns[grdPatientRelated.Columns.Count - 1].Visible = isVisible;

            grdPatientRelated.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                PatientRelateds = null;

            //Perbaharui tampilan dan data
            grdPatientRelated.Rebind();
        }

        private PatientRelatedCollection PatientRelateds
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPatientRelated"];
                    if (obj != null)
                        return ((PatientRelatedCollection)(obj));
                }

                string patientID = txtPatientID.Text;

                var coll = new PatientRelatedCollection();
                var query = new PatientRelatedQuery("a");
                var qPat = new PatientQuery("b");
                query.InnerJoin(qPat).On(query.RelatedPatientID == qPat.PatientID);
                query.Select
                    (
                        query.PatientID,
                        query.RelatedPatientID,
                        qPat.MedicalNo.As("refToPatient_MedicalNo"),
                        qPat.PatientName.As("refToPatient_PatientName"),
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                     );
                query.Where(query.PatientID == patientID);
                query.OrderBy(query.RelatedPatientID.Ascending);
                coll.Load(query);

                Session["collPatientRelated"] = coll;
                return coll;
            }
            set
            {
                Session["collPatientRelated"] = value;
            }
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            PatientRelateds = null; //Reset Record Detail
            grdPatientRelated.DataSource = PatientRelateds;
            grdPatientRelated.DataBind();
        }

        protected void grdPatientRelated_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPatientRelated.DataSource = PatientRelateds;
        }

        protected void grdPatientRelated_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String relatedPatID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PatientRelatedMetadata.ColumnNames.RelatedPatientID]);
            var entity = FindPatientRelated(relatedPatID);
            if (entity != null)
                SetEntityValue(entity, e, relatedPatID);
        }

        protected void grdPatientRelated_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String relatedPatID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientRelatedMetadata.ColumnNames.RelatedPatientID]);
            var entity = FindPatientRelated(relatedPatID);
            if (entity != null)
                SetEntityValue(entity, e, relatedPatID);
                //entity.MarkAsDeleted();
        }

        protected void grdPatientRelated_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = PatientRelateds.AddNew();
            SetEntityValue(entity, e, "");
        }

        private PatientRelated FindPatientRelated(String relatedPatientID)
        {
            PatientRelatedCollection coll = PatientRelateds;
            return coll.FirstOrDefault(rec => rec.RelatedPatientID.Equals(relatedPatientID));
        }

        private void SetEntityValue(PatientRelated entity, GridCommandEventArgs e, string relatedPatID)
        {
            var userControl = (PatientRelatedItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PatientID = txtPatientID.Text;
                entity.RelatedPatientID = userControl.RelatedPatientID;

                var pat = new Patient();
                pat.LoadByPrimaryKey(entity.RelatedPatientID);

                entity.MedicalNo = pat.MedicalNo.TrimEnd();
                entity.PatientName = pat.PatientName.TrimEnd();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
            else
            {
                entity.PatientID = txtPatientID.Text;
                entity.RelatedPatientID = relatedPatID;

                var pat = new Patient();
                pat.LoadByPrimaryKey(entity.RelatedPatientID);

                entity.MedicalNo = pat.MedicalNo;
                entity.PatientName = pat.PatientName;
                entity.LastUpdateByUserID = "delete"; 
            }
        }

        #endregion
    }
}
