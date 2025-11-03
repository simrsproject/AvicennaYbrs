using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class CompanyLaborProfileDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "CompanyLaborProfileSearch.aspx";
            UrlPageList = "CompanyLaborProfileList.aspx";
			
			ProgramID = AppConstant.Program.Profile ; //TODO: Isi ProgramID
            txtCompanyLaborProfileID.Text = "1";
			//StandardReference Initialize
			if (!IsPostBack)
            {
            }
			
			//PopUp Search
			if (!IsCallback)
			{
				
			}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CompanyLaborProfile());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            CompanyLaborProfile entity = new CompanyLaborProfile();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtCompanyLaborProfileID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            CompanyLaborProfile entity = new CompanyLaborProfile();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtCompanyLaborProfileID.Text)))            
            entity = new CompanyLaborProfile();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            CompanyLaborProfile entity = new CompanyLaborProfile();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtCompanyLaborProfileID.Text)))
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

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("CompanyLaborProfileID='{0}'", txtCompanyLaborProfileID.Text.Trim());
            auditLogFilter.TableName = "CompanyLaborProfile";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtCompanyLaborProfileID.Enabled = (newVal == AppEnum.DataMode.New);
            RefreshCommandItemCompanyEducationProfile(newVal);
            RefreshCommandItemCompanyFieldOfWorkProfile(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            CompanyLaborProfile entity = new CompanyLaborProfile();
            if (parameters.Length > 0)
            {
                string companyLaborProfileID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(companyLaborProfileID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtCompanyLaborProfileID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            CompanyLaborProfile companyLaborProfile = (CompanyLaborProfile)entity;
            txtCompanyLaborProfileID.Value = Convert.ToDouble(companyLaborProfile.CompanyLaborProfileID);
            txtCompanyLaborProfileCode.Text = companyLaborProfile.CompanyLaborProfileCode;
            txtCompanyLaborProfileName.Text = companyLaborProfile.CompanyLaborProfileName;

            //Display Data Detail
            PopulateCompanyEducationProfileGrid();
            PopulateCompanyFieldOfWorkProfileGrid();
            
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(CompanyLaborProfile entity)
        {
            entity.CompanyLaborProfileID = Convert.ToInt32(txtCompanyLaborProfileID.Value);
            entity.CompanyLaborProfileCode = txtCompanyLaborProfileCode.Text;
            entity.CompanyLaborProfileName = txtCompanyLaborProfileName.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //--> Education
            foreach (CompanyEducationProfile education in CompanyEducationProfiles)
            {
                education.CompanyLaborProfileID = Convert.ToInt32(txtCompanyLaborProfileID.Text);
                //Last Update Status
                if (education.es.IsAdded || education.es.IsModified)
                {
                    education.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    education.LastUpdateDateTime = DateTime.Now;
                }
            }

            //--> Education
            foreach (CompanyFieldOfWorkProfile fieldOfWork in CompanyFieldOfWorkProfiles)
            {
                fieldOfWork.CompanyLaborProfileID = Convert.ToInt32(txtCompanyLaborProfileID.Text);
                //Last Update Status
                if (fieldOfWork.es.IsAdded || fieldOfWork.es.IsModified)
                {
                    fieldOfWork.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    fieldOfWork.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(CompanyLaborProfile entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                CompanyEducationProfiles.Save();
                CompanyFieldOfWorkProfiles.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            CompanyLaborProfileQuery que = new CompanyLaborProfileQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.CompanyLaborProfileID > txtCompanyLaborProfileID.Text);
                que.OrderBy(que.CompanyLaborProfileID.Ascending);
            }
            else
            {
                que.Where(que.CompanyLaborProfileID < txtCompanyLaborProfileID.Text);
                que.OrderBy(que.CompanyLaborProfileID.Descending);
            }
            CompanyLaborProfile entity = new CompanyLaborProfile();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region Record Detail Method Function CompanyEducationProfile
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah CompanyEducationProfiles.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemCompanyEducationProfile(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdCompanyEducationProfile.Columns[0].Visible = isVisible;
            grdCompanyEducationProfile.Columns[grdCompanyEducationProfile.Columns.Count - 1].Visible = isVisible;

            grdCompanyEducationProfile.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdCompanyEducationProfile.Rebind();
        }

        private CompanyEducationProfileCollection CompanyEducationProfiles
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCompanyEducationProfile"];
                    if (obj != null)
                    {
                        return ((CompanyEducationProfileCollection)(obj));
                    }
                }

                CompanyEducationProfileCollection coll = new CompanyEducationProfileCollection();
                CompanyEducationProfileQuery query = new CompanyEducationProfileQuery("a");

                CompanyLaborProfileQuery profile = new CompanyLaborProfileQuery("b");

                query.InnerJoin(profile).On(query.CompanyLaborProfileID == profile.CompanyLaborProfileID);

                query.Where(query.CompanyLaborProfileID == txtCompanyLaborProfileID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.CompanyLaborProfileID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collCompanyEducationProfile"] = coll;
                return coll;
            }
            set { Session["collCompanyEducationProfile"] = value; }
        }

        private void PopulateCompanyEducationProfileGrid()
        {
            //Display Data Detail
            CompanyEducationProfiles = null; //Reset Record Detail
            grdCompanyEducationProfile.DataSource = CompanyEducationProfiles; //Requery
            grdCompanyEducationProfile.MasterTableView.IsItemInserted = false;
            grdCompanyEducationProfile.MasterTableView.ClearEditItems();
            grdCompanyEducationProfile.DataBind();
        }

        protected void grdCompanyEducationProfile_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCompanyEducationProfile.DataSource = CompanyEducationProfiles;
        }

        protected void grdCompanyEducationProfile_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 companyEducationProfileID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileID]);
            CompanyEducationProfile entity = FindCompanyEducationProfile(companyEducationProfileID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdCompanyEducationProfile_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 companyEducationProfileID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][CompanyEducationProfileMetadata.ColumnNames.CompanyEducationProfileID]);
            CompanyEducationProfile entity = FindCompanyEducationProfile(companyEducationProfileID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdCompanyEducationProfile_InsertCommand(object source, GridCommandEventArgs e)
        {
            CompanyEducationProfile entity = CompanyEducationProfiles.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdCompanyEducationProfile.Rebind();
        }
        private CompanyEducationProfile FindCompanyEducationProfile(Int32 companyEducationProfileID)
        {
            CompanyEducationProfileCollection coll = CompanyEducationProfiles;
            CompanyEducationProfile retEntity = null;
            foreach (CompanyEducationProfile rec in coll)
            {
                if (rec.CompanyEducationProfileID.Equals(companyEducationProfileID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(CompanyEducationProfile entity, GridCommandEventArgs e)
        {
            CompanyEducationProfileDetail userControl = (CompanyEducationProfileDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.CompanyEducationProfileID = userControl.CompanyEducationProfileID;
                entity.CompanyEducationProfileCode = userControl.CompanyEducationProfileCode;
                entity.CompanyEducationProfileName = userControl.CompanyEducationProfileName;

            }
        }

        #endregion

        #region Record Detail Method Function CompanyFieldOfWorkProfile
        //TODO: Isi ulang field untuk relasi child ke parent nya di method SetEntity
        //TODO: Tambahkan perintah CompanyFieldOfWorkProfiles.Save(); di method SaveEntity
        //TODO: Panggil method RefreshCommandItemGrid dari OnDataModeChanged
        private void RefreshCommandItemCompanyFieldOfWorkProfile(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdCompanyFieldOfWorkProfile.Columns[0].Visible = isVisible;
            grdCompanyFieldOfWorkProfile.Columns[grdCompanyFieldOfWorkProfile.Columns.Count - 1].Visible = isVisible;

            grdCompanyFieldOfWorkProfile.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdCompanyFieldOfWorkProfile.Rebind();
        }

        private CompanyFieldOfWorkProfileCollection CompanyFieldOfWorkProfiles
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCompanyFieldOfWorkProfile"];
                    if (obj != null)
                    {
                        return ((CompanyFieldOfWorkProfileCollection)(obj));
                    }
                }

                CompanyFieldOfWorkProfileCollection coll = new CompanyFieldOfWorkProfileCollection();
                CompanyFieldOfWorkProfileQuery query = new CompanyFieldOfWorkProfileQuery("a");
                CompanyLaborProfileQuery profile = new CompanyLaborProfileQuery("b");

                query.InnerJoin(profile).On(query.CompanyLaborProfileID == profile.CompanyLaborProfileID);

                query.Where(query.CompanyLaborProfileID == txtCompanyLaborProfileID.Text); //TODO: Betulkan parameternya
                query.OrderBy(query.CompanyLaborProfileID.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collCompanyFieldOfWorkProfile"] = coll;
                return coll;
            }
            set { Session["collCompanyFieldOfWorkProfile"] = value; }
        }

        private void PopulateCompanyFieldOfWorkProfileGrid()
        {
            //Display Data Detail
            CompanyFieldOfWorkProfiles = null; //Reset Record Detail
            grdCompanyFieldOfWorkProfile.DataSource = CompanyFieldOfWorkProfiles; //Requery
            grdCompanyFieldOfWorkProfile.MasterTableView.IsItemInserted = false;
            grdCompanyFieldOfWorkProfile.MasterTableView.ClearEditItems();
            grdCompanyFieldOfWorkProfile.DataBind();
        }

        protected void grdCompanyFieldOfWorkProfile_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCompanyFieldOfWorkProfile.DataSource = CompanyFieldOfWorkProfiles;
        }

        protected void grdCompanyFieldOfWorkProfile_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 companyFieldOfWorkProfileID = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileID]);
            CompanyFieldOfWorkProfile entity = FindCompanyFieldOfWorkProfile(companyFieldOfWorkProfileID);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdCompanyFieldOfWorkProfile_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 companyFieldOfWorkProfileID = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][CompanyFieldOfWorkProfileMetadata.ColumnNames.CompanyFieldOfWorkProfileID]);
            CompanyFieldOfWorkProfile entity = FindCompanyFieldOfWorkProfile(companyFieldOfWorkProfileID);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdCompanyFieldOfWorkProfile_InsertCommand(object source, GridCommandEventArgs e)
        {
            CompanyFieldOfWorkProfile entity = CompanyFieldOfWorkProfiles.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdCompanyFieldOfWorkProfile.Rebind();
        }
        private CompanyFieldOfWorkProfile FindCompanyFieldOfWorkProfile(Int32 companyFieldOfWorkProfileID)
        {
            CompanyFieldOfWorkProfileCollection coll = CompanyFieldOfWorkProfiles;
            CompanyFieldOfWorkProfile retEntity = null;
            foreach (CompanyFieldOfWorkProfile rec in coll)
            {
                if (rec.CompanyFieldOfWorkProfileID.Equals(companyFieldOfWorkProfileID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(CompanyFieldOfWorkProfile entity, GridCommandEventArgs e)
        {
            CompanyFieldOfWorkProfileDetail userControl = (CompanyFieldOfWorkProfileDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.CompanyFieldOfWorkProfileID = userControl.CompanyFieldOfWorkProfileID;
                entity.CompanyFieldOfWorkProfileCode = userControl.CompanyFieldOfWorkProfileCode;
                entity.CompanyFieldOfWorkProfileName = userControl.CompanyFieldOfWorkProfileName;

            }
        }

        #endregion


    }
}
