using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileAnalysisDetail : BasePageDetail
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "#";
            UrlPageList = "MedicalRecordFileAnalysisList.aspx";

            ProgramID = AppConstant.Program.AnalysisDocument; 

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRCompleteStatusRM, AppEnum.StandardReference.CompleteStatusRM);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuEditClick()
        {
            cboSRFilesAnalysis.Enabled = string.IsNullOrEmpty(cboSRFilesAnalysis.SelectedValue);
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MedicalRecordFileStatus());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AnalysisDocument();
            var analysisDocumentItem = new AnalysisDocumentItemCollection();

            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity, analysisDocumentItem);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            //var entity = new AnalysisDocument();
            //var analysisDocumentItem = new AnalysisDocumentItemCollection();

            //entity = new AnalysisDocument();
            //entity.AddNew();
            //SetEntityValue(entity, analysisDocumentItem);
            //SaveEntity(entity, analysisDocumentItem);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var ceklist = new MedicalRecordFileStatus();
            if (ceklist.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var entity = new AnalysisDocument();
                var analysisDocumentItem = new AnalysisDocumentItemCollection();

                if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
                {
                    SetEntityValue(entity, analysisDocumentItem);
                    SaveEntity(entity, analysisDocumentItem);
                }
                else
                {
                    entity = new AnalysisDocument();
                    entity.AddNew();
                    SetEntityValue(entity, analysisDocumentItem);
                    SaveEntity(entity, analysisDocumentItem);
                }
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

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("RegistrationNo='{0}'", txtRegistrationNo.Text.Trim());
            auditLogFilter.TableName = "MedicalRecordFileStatus";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_RegistrationNo", txtRegistrationNo.Text);
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            grdAnalysisDocumentItem.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            RefreshGridAnalysisDocumentItem();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MedicalRecordFileStatus();
            if (parameters.Length > 0)
            {
                String registrationNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(registrationNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtRegistrationNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var fs = (MedicalRecordFileStatus)entity;
            txtRegistrationNo.Text = fs.RegistrationNo;
            PopulatePatientInformation(txtRegistrationNo.Text);

            txtFilesReceiveDate.SelectedDate = fs.FileInDate;

            var analysisDocument = new AnalysisDocument();
            if (analysisDocument.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var files = new DocumentDefinitionQuery("a");
                var std = new AppStandardReferenceItemQuery("b");
                files.Select(files.SRFilesAnalysis, std.ItemName.As("FilesAnalysisName"));
                files.InnerJoin(std).On(files.SRFilesAnalysis == std.ItemID &&
                                        std.StandardReferenceID == AppEnum.StandardReference.FilesAnalysis.ToString());
                files.Where(files.DepartmentID == txtDepartmentID.Text,
                            files.SRFilesAnalysis == analysisDocument.SRFilesAnalysis);
                cboSRFilesAnalysis.DataSource = files.LoadDataTable();
                cboSRFilesAnalysis.DataBind();

                cboSRFilesAnalysis.SelectedValue = analysisDocument.SRFilesAnalysis;

                cboParamedicID.Items.Clear();
                var q = new ParamedicQuery();
                q.Where(q.ParamedicID == analysisDocument.ParamedicID);
                cboParamedicID.DataSource = q.LoadDataTable();
                cboParamedicID.DataBind();

                cboParamedicID.SelectedValue = analysisDocument.ParamedicID;
                cboSRCompleteStatusRM.SelectedValue = analysisDocument.SRCompleteStatusRM;
                txtFilesReceiveDate.SelectedDate = analysisDocument.FilesReceiveDate;
                txtFilesAcceptanceDate.SelectedDate = analysisDocument.FilesAcceptanceDate;
                txtNotes.Text = analysisDocument.Notes;
            }

            RefreshGridAnalysisDocumentItem();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(AnalysisDocument entity, AnalysisDocumentItemCollection analysisDocumentItem)
        {
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.SRFilesAnalysis = cboSRFilesAnalysis.SelectedValue;
            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.SRCompleteStatusRM = cboSRCompleteStatusRM.SelectedValue;

            entity.FilesReceiveDate = txtFilesReceiveDate.SelectedDate;
            entity.FilesAcceptanceDate = txtFilesAcceptanceDate.SelectedDate;
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //User ServiceUnit
            analysisDocumentItem.Query.Where(analysisDocumentItem.Query.RegistrationNo == txtRegistrationNo.Text);
            analysisDocumentItem.LoadAll();

            foreach (GridDataItem dataItem in grdAnalysisDocumentItem.MasterTableView.Items)
            {
                AnalysisDocumentItem item;
                string documentFilesID = dataItem.GetDataKeyValue("DocumentFilesID").ToString();

                item = analysisDocumentItem.FindByPrimaryKey(txtRegistrationNo.Text, Convert.ToInt32(documentFilesID));
                if (item == null)
                {
                    item = analysisDocumentItem.AddNew();
                    item.RegistrationNo = txtRegistrationNo.Text;
                    item.DocumentFilesID = Convert.ToInt32(documentFilesID);
                }
                item.RegistrationNo = txtRegistrationNo.Text;
                item.DocumentFilesID = Convert.ToInt32(documentFilesID);
                item.IsQuantity = ((CheckBox)dataItem.FindControl("chkIsQuantity")).Checked;
                item.IsQuality = ((CheckBox)dataItem.FindControl("chkIsQuality")).Checked;
                item.IsLegible = ((CheckBox)dataItem.FindControl("chkIsLegible")).Checked;
                item.IsSign = ((CheckBox)dataItem.FindControl("chkIsSign")).Checked;

                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }

        }

        private void SaveEntity(AnalysisDocument entity, AnalysisDocumentItemCollection analysisDocumentItem)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                analysisDocumentItem.Save();
                
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MedicalRecordFileStatusQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RegistrationNo > txtRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where(que.RegistrationNo < txtRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Descending);
            }
            var entity = new MedicalRecordFileStatus();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        protected void cboSRFilesAnalysis_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RefreshGridAnalysisDocumentItem();
        }
        #endregion

        #region ComboBox Function

        protected void cboParamedicID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.ParamedicID,
                    query.ParamedicName
                );
            query.Where(
                query.ParamedicName.Like(searchTextContain),
                query.IsAvailable == true,
                query.IsActive == true
                );

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboSRFilesAnalysis_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new DocumentDefinitionQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            query.Select
                (
                    query.SRFilesAnalysis,
                    std.ItemName.As("FilesAnalysisName")
                );
            query.InnerJoin(std).On(query.SRFilesAnalysis == std.ItemID &&
                                    std.StandardReferenceID == AppEnum.StandardReference.FilesAnalysis.ToString());
            query.Where(
                query.DepartmentID == txtDepartmentID.Text,
                std.ItemName.Like(searchTextContain),
                query.IsActive == true
                );

            cboSRFilesAnalysis.DataSource = query.LoadDataTable();
            cboSRFilesAnalysis.DataBind();
        }

        protected void cboSRFilesAnalysis_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FilesAnalysisName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SRFilesAnalysis"].ToString();
        }

        #endregion ComboBox Function

        #region UserServiceUnit

        protected void grdAnalysisDocumentItem_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdAnalysisDocumentItem.DataSource = DetailAnalysisDocumentItem;
        }

        private DataTable DetailAnalysisDocumentItem
        {
            get
            {
                object obj = this.Session["AnalysisDocumentItem"];
                if (obj != null)
                    return ((DataTable)(obj));

                var coll = new AnalysisDocumentItemCollection();
                DataTable dtb = coll.GetInnerJoinWDocument(txtRegistrationNo.Text);
                if (dtb.Rows.Count == 0)
                    dtb = coll.GetFullJoinWDocument("02", cboSRFilesAnalysis.SelectedValue);

                //DataTable dtb = DataModeCurrent != DataMode.New
                //                    ? coll.GetInnerJoinWDocument(txtRegistrationNo.Text)
                //                    : coll.GetFullJoinWDocument("02", cboSRFilesAnalysis.SelectedValue);

                Session["AnalysisDocumentItem"] = dtb;
                return dtb;
            }
        }

        private void RefreshGridAnalysisDocumentItem()
        {
            Session["AnalysisDocumentItem"] = null;
            grdAnalysisDocumentItem.Rebind();
        }

        #endregion

        private void PopulatePatientInformation(string regNo)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(regNo))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                txtAddress.Text = pat.StreetName + " " + pat.City.Trim() + " " + pat.County.Trim();

                string ageYear = Helper.GetAgeInYear(pat.DateOfBirth.Value).ToString();
                string ageMonth = Helper.GetAgeInMonth(pat.DateOfBirth.Value).ToString();
                string ageDay = Helper.GetAgeInDay(pat.DateOfBirth.Value).ToString();

                if (ageYear == "0")
                {
                    if (ageMonth == "0")
                        txtAge.Text = ageDay + " d";
                    else
                        txtAge.Text = ageMonth + " m";
                }
                else
                    txtAge.Text = ageYear + " y";

                txtGender.Text = pat.Sex;

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);

                var q = new ParamedicQuery();
                q.Where(q.ParamedicID == reg.ParamedicID);
                cboParamedicID.DataSource = q.LoadDataTable();
                cboParamedicID.DataBind();
                
                cboParamedicID.SelectedValue = reg.ParamedicID;

                txtDepartmentID.Text = reg.DepartmentID;
                var dep = new Department();
                txtDepartmentName.Text = dep.LoadByPrimaryKey(txtDepartmentID.Text) ? dep.DepartmentName : string.Empty;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtAge.Text = string.Empty;
                txtGender.Text = string.Empty;

                cboParamedicID.Items.Clear();
                cboParamedicID.Text = string.Empty;

                txtDepartmentID.Text = string.Empty;
                txtDepartmentName.Text = string.Empty;
            }
        }
    }
}
