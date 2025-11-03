using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorInfoDialog : BasePageDialog
    {
        private string _guarId;
        private string _lblGuarantorInfo;
        private AppAutoNumberLast _autoNumber;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
            (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _guarId = Page.Request.QueryString["id"];
            _lblGuarantorInfo = Page.Request.QueryString["lblGuarantorInfo"];

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRGuarantorType, AppEnum.StandardReference.GuarantorType);
                StandardReference.InitializeIncludeSpace(cboSRBusinessMethod, AppEnum.StandardReference.BusinessMethod);
                StandardReference.InitializeIncludeSpace(cboSRTariffType, AppEnum.StandardReference.TariffType);

                StandardReference.InitializeIncludeSpace(cboSRGuarantorTypeHd, AppEnum.StandardReference.GuarantorType);
                StandardReference.InitializeIncludeSpace(cboSRBusinessMethodHd, AppEnum.StandardReference.BusinessMethod);
                StandardReference.InitializeIncludeSpace(cboSRTariffTypeHd, AppEnum.StandardReference.TariffType);

                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(Request.QueryString["id"]);

                //--------- Detail ---------
                txtGuarantorID.Text = guarantor.GuarantorID;
                txtGuarantorName.Text = guarantor.GuarantorName;
                cboSRGuarantorType.SelectedValue = guarantor.SRGuarantorType;
                txtContractStart.SelectedDate = guarantor.ContractStart;
                txtContractEnd.SelectedDate = guarantor.ContractEnd;
                txtContractSummary.Text = guarantor.ContractSummary;
                txtNoteRelatedCompanies.Text = guarantor.NoteCompanyList;
                txtContactPerson.Text = guarantor.ContactPerson;
                chkIsActive.Checked = guarantor.IsActive ?? false;
                cboSRBusinessMethod.SelectedValue = guarantor.SRBusinessMethod;
                cboSRTariffType.SelectedValue = guarantor.SRTariffType;
                txtAdminPercentage.Value = Convert.ToDouble(guarantor.AdminPercentage);
                txtAdminPercentageOp.Value = Convert.ToDouble(guarantor.AdminPercentageOp);

                txtAdminAmountMin.Value = Convert.ToDouble(guarantor.AdminValueMinimum);
                txtAdminAmountMax.Value = Convert.ToDouble(guarantor.AdminAmountLimit);
                txtAdminAmountMinOp.Value = Convert.ToDouble(guarantor.AdminValueMinimumOp);
                txtAdminAmountMaxOp.Value = Convert.ToDouble(guarantor.AdminAmountLimitOp);

                chkIsIncludeItemMedical.Checked = guarantor.IsIncludeItemMedical ?? false;
                rblToGuarantorMedical.SelectedIndex = (guarantor.IsIncludeItemMedicalToGuarantor ?? false) ? 0 : 1;

                chkIsIncludeItemNonMedical.Checked = guarantor.IsIncludeItemNonMedical ?? false;
                rblToGuarantorNonMedical.SelectedIndex = (guarantor.IsIncludeItemNonMedicalToGuarantor ?? false) ? 0 : 1;

                chkIsCoverInpatient.Checked = guarantor.IsCoverInpatient ?? false;
                chkIsCoverOutpatient.Checked = guarantor.IsCoverOutpatient ?? false;
                chkIsIncludeAdminValue.Checked = guarantor.IsIncludeAdminValue ?? false;

                //Address 
                ctlAddress.StreetName = guarantor.StreetName;
                ctlAddress.District = guarantor.District;
                ctlAddress.City = guarantor.City;
                ctlAddress.County = guarantor.County;
                ctlAddress.State = guarantor.State;
                ctlAddress.ZipCode = guarantor.ZipCode;
                ctlAddress.PhoneNo = guarantor.PhoneNo;
                ctlAddress.FaxNo = guarantor.FaxNo;
                ctlAddress.Email = guarantor.Email;
                ctlAddress.MobilePhoneNo = guarantor.MobilePhoneNo;

                //--------- Header ---------
                txtGuarantorIdHd.Text = guarantor.GuarantorHeaderID;
                
                guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(txtGuarantorIdHd.Text);

                txtGuarantorNameHd.Text = guarantor.GuarantorName;
                cboSRGuarantorTypeHd.SelectedValue = guarantor.SRGuarantorType;
                txtContractStartHd.SelectedDate = guarantor.ContractStart;
                txtContractEndHd.SelectedDate = guarantor.ContractEnd;
                txtContractSummaryHd.Text = guarantor.ContractSummary;
                txtNoteRelatedCompaniesHd.Text = guarantor.NoteCompanyList;
                txtContactPersonHd.Text = guarantor.ContactPerson;
                chkIsActiveHd.Checked = guarantor.IsActive ?? false;
                cboSRBusinessMethodHd.SelectedValue = guarantor.SRBusinessMethod;
                cboSRTariffTypeHd.SelectedValue = guarantor.SRTariffType;
                txtAdminPercentageHd.Value = Convert.ToDouble(guarantor.AdminPercentage);
                txtAdminPercentageOpHd.Value = Convert.ToDouble(guarantor.AdminPercentageOp);

                txtAdminAmountMinHd.Value = Convert.ToDouble(guarantor.AdminValueMinimum);
                txtAdminAmountMaxHd.Value = Convert.ToDouble(guarantor.AdminAmountLimit);
                txtAdminAmountMinOpHd.Value = Convert.ToDouble(guarantor.AdminValueMinimumOp);
                txtAdminAmountMaxOpHd.Value = Convert.ToDouble(guarantor.AdminAmountLimitOp);

                chkIsIncludeItemMedicalHd.Checked = guarantor.IsIncludeItemMedical ?? false;
                rblToGuarantorMedicalHd.SelectedIndex = (guarantor.IsIncludeItemMedicalToGuarantor ?? false) ? 0 : 1;

                chkIsIncludeItemNonMedicalHd.Checked = guarantor.IsIncludeItemNonMedical ?? false;
                rblToGuarantorNonMedicalHd.SelectedIndex = (guarantor.IsIncludeItemNonMedicalToGuarantor ?? false) ? 0 : 1;

                chkIsCoverInpatientHd.Checked = guarantor.IsCoverInpatient ?? false;
                chkIsCoverOutpatientHd.Checked = guarantor.IsCoverOutpatient ?? false;
                chkIsIncludeAdminValueHd.Checked = guarantor.IsIncludeAdminValue ?? false;

                //Address 
                ctlAddress2.StreetName = guarantor.StreetName;
                ctlAddress2.District = guarantor.District;
                ctlAddress2.City = guarantor.City;
                ctlAddress2.County = guarantor.County;
                ctlAddress2.State = guarantor.State;
                ctlAddress2.ZipCode = guarantor.ZipCode;
                ctlAddress2.PhoneNo = guarantor.PhoneNo;
                ctlAddress2.FaxNo = guarantor.FaxNo;
                ctlAddress2.Email = guarantor.Email;
                ctlAddress2.MobilePhoneNo = guarantor.MobilePhoneNo;

                if (AppSession.Parameter.HealthcareInitial == "RSCH")
                {
                    RadGridMaster.Columns[0].Visible = false;
                    RadGridMaster.Columns[RadGridMaster.Columns.Count - 1].Visible = false;
                }
            }
        }

        #region guarantor notes
        #region Private Method
        private void SaveEntity(GuarantorInfo entity, string mode)
        {
            var summ = new GuarantorInfoSummary();
            if (!summ.LoadByPrimaryKey(entity.GuarantorID))
            {
                summ.AddNew();
                summ.GuarantorID = entity.GuarantorID;
                summ.NoteCount = 0;
            }
            summ.LastUpdateByUserID = AppSession.UserLogin.UserID;
            summ.LastUpdateDateTime = DateTime.Now;

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (mode == "new")
                {
                    _autoNumber.Save();
                    summ.NoteCount += 1;
                    
                }
                else if (mode == "del")
                {
                    entity.MarkAsDeleted();
                    summ.NoteCount -= 1;
                }

                entity.Save();
                summ.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        #endregion

        private DataTable DtGuarantorInfo
        {
            get
            {

                GuarantorInfoQuery query;
                AppUserQuery qUser;

                query = new GuarantorInfoQuery("i");
                qUser = new AppUserQuery("u");
                query.LeftJoin(qUser).On(query.CreatedByUserID == qUser.UserID);
                query.Where(query.GuarantorID == _guarId);
                query.OrderBy(query.CreatedDateTime.Ascending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                return dtb;
            }

        }

        protected void RadGridMaster_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                SetMessage("Data cannot be inserted. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("New data is inserted!");
                RadGridMaster.Rebind();
            }
        }
        protected void RadGridMaster_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            String id = item.GetDataKeyValue("GuarantorInfoID").ToString();

            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                SetMessage("Data with ID: " + id + " cannot be updated. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("Data with ID: " + id + " is updated!");
                RadGridMaster.Rebind();
            }
        }
        protected void RadGridMaster_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            String id = dataItem.GetDataKeyValue("GuarantorInfoID").ToString();

            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                SetMessage("Data with ID: " + id + " cannot be deleted. Reason: " + e.Exception.Message);
            }
            else
            {
                SetMessage("Data with ID: " + id + " is deleted!");
                RadGridMaster.Rebind();
            }
        }

        protected void RadGridMaster_DataBound(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gridMessage))
            {
                DisplayMessage(gridMessage, (RadGrid)sender);
            }
        }
        protected void RadGridMaster_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGridMaster.DataSource = DtGuarantorInfo;
        }

        private void DisplayMessage(string text, RadGrid rg)
        {
            rg.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>", text)));
        }

        private void SetMessage(string message)
        {
            gridMessage = message;
        }

        private string gridMessage = null;

        private object GetColVal(GridColumn column, GridEditManager editMan)
        {
            if (column is IGridEditableColumn)
            {
                IGridEditableColumn editableCol = (column as IGridEditableColumn);
                if (editableCol.IsEditable)
                {
                    IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);

                    string editorText = "unknown";
                    object editorValue = null;

                    if (editor is GridTextColumnEditor)
                    {
                        editorText = (editor as GridTextColumnEditor).Text;
                        editorValue = (editor as GridTextColumnEditor).Text;
                    }

                    if (editor is GridBoolColumnEditor)
                    {
                        editorText = (editor as GridBoolColumnEditor).Value.ToString();
                        editorValue = (editor as GridBoolColumnEditor).Value;
                    }

                    if (editor is GridDropDownColumnEditor)
                    {
                        editorText = (editor as GridDropDownColumnEditor).SelectedText + "; " +
                         (editor as GridDropDownColumnEditor).SelectedValue;
                        editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
                    }

                    return editorValue;

                }
            }

            return null;
        }
        protected void RadGridMaster_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            GridColumn cInformation = e.Item.OwnerTableView.Columns[1];

            GuarantorInfo entity = new GuarantorInfo();
            entity.AddNew();

            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.GuarantorInfoNo);

            entity.GuarantorInfoID = _autoNumber.LastCompleteNumber;
            entity.GuarantorID = _guarId;
            entity.Information = GetColVal(cInformation, editMan).ToString();

            entity.CreatedByUserID = AppSession.UserLogin.UserID;
            entity.CreatedDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            SaveEntity(entity, "new");

            // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
            RadGridMaster.DataSource = null;
            RadGridMaster.Rebind();

            SetMessage("New data with ID " + entity.GuarantorInfoID + " is inserted!");

            UpdateParentOnStartup();
        }
        protected void RadGridMaster_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            GridEditManager editMan = editedItem.EditManager;

            GridColumn cInformation = e.Item.OwnerTableView.Columns[1];

            string ID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["GuarantorInfoID"].ToString();

            GuarantorInfo entity = new GuarantorInfo();
            entity.LoadByPrimaryKey(ID);
            entity.Information = GetColVal(cInformation, editMan).ToString();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;

            SaveEntity(entity, "update");

            // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
            RadGridMaster.DataSource = null;
            RadGridMaster.Rebind();

            SetMessage("Data with ID: " + ID + " is updated!");

            UpdateParentOnStartup();
        }
        protected void RadGridMaster_DeleteCommand(object source, GridCommandEventArgs e)
        {
            string ID = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["GuarantorInfoID"].ToString();

            // delete entity
            GuarantorInfo entity = new GuarantorInfo();
            entity.LoadByPrimaryKey(ID);

            SaveEntity(entity, "del");

            // set datasource to null will fire needdatasource when u call rebind. strange isn't it?
            RadGridMaster.DataSource = null;
            RadGridMaster.Rebind();

            SetMessage("Data with ID: " + ID + " is deleted!");

            UpdateParentOnStartup();
        }
        private void UpdateParentOnStartup()
        {
            var iCount = RadGridMaster.Items.Count;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UpdateGuarantorInfo", "UpdateInformationCount('" + _lblGuarantorInfo + "', '" + iCount.ToString() + "');", true);
        }
#endregion
    }
}
