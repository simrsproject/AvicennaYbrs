using System;
using System.Data;
using System.IO;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Admin
{
    public partial class UserDetail : BasePageDetail
    {
        private void SetEntityValue(esAppUser entity, AppUserUserGroupCollection userGroups, AppUserServiceUnitCollection userServiceUnits)
        {
            entity.UserID = txtUserID.Text;
            entity.UserName = txtUserName.Text;

            if (entity.es.IsAdded)
            {
                entity.Password = Encryptor.Encrypt(txtPassword.Text);
            }
            entity.SRLanguage = cboSRLanguage.SelectedValue;
            entity.SRUserType = cboSRUserType.SelectedValue;

            entity.ActiveDate = txtActiveDate.SelectedDate;
            entity.ExpireDate = txtExpireDate.SelectedDate;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.LicenseNo = txtLicenseNo.Text;
            entity.ESignNik = txtESignNik.Text;
            entity.Email = txtEmail.Text;
            entity.IsLocked = chkIsLocked.Checked;

            if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);
            else
                entity.PersonID = -1;

            if (Context.Cache[Session.SessionID + "_SignatureImage"] != null)
                entity.SignatureImage = (byte[])Context.Cache[Session.SessionID + "_SignatureImage"];


            //User Group
            userGroups.Query.Where(userGroups.Query.UserID == txtUserID.Text);
            userGroups.LoadAll();

            foreach (GridDataItem dataItem in grdUserUserGroup.MasterTableView.Items)
            {
                AppUserUserGroup item;
                string userGroupID = dataItem.GetDataKeyValue("UserGroupID").ToString();
                item = userGroups.FindByPrimaryKey(txtUserID.Text, userGroupID);
                if (dataItem.Selected)
                {
                    if (item == null)
                    {
                        item = userGroups.AddNew();
                        item.UserID = txtUserID.Text;
                        item.UserGroupID = userGroupID;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                    if (item != null)
                    item.MarkAsDeleted();
            }

            //User ServiceUnit
            userServiceUnits.Query.Where(userServiceUnits.Query.UserID == txtUserID.Text);
            userServiceUnits.LoadAll();

            foreach (GridDataItem dataItem in grdUserServiceUnit.MasterTableView.Items)
            {
                AppUserServiceUnit item;
                string serviceUnitID = dataItem.GetDataKeyValue("ServiceUnitID").ToString();
                item = userServiceUnits.FindByPrimaryKey(txtUserID.Text, serviceUnitID);
                if (dataItem.Selected)
                {
                    if (item == null)
                    {
                        item = userServiceUnits.AddNew();
                        item.UserID = txtUserID.Text;
                        item.ServiceUnitID = serviceUnitID;
                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        item.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                    if (item != null)
                    item.MarkAsDeleted();
            }

        }

        private void MoveRecord(bool isNextRecord)
        {
            AppUserQuery que = new AppUserQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.UserID > txtUserID.Text);
                que.OrderBy(que.UserID.Ascending);
            }
            else
            {
                que.Where(que.UserID < txtUserID.Text);
                que.OrderBy(que.UserID.Descending);
            }
            AppUser entity = new AppUser();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AppUser entity = new AppUser();
            if (parameters.Length > 0)
            {
                String userID = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(userID);
            }
            else
                entity.LoadByPrimaryKey(txtUserID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var appUser = (AppUser)entity;
            txtUserID.Text = appUser.UserID;
            txtUserName.Text = appUser.UserName;
            txtPassword.Text = "123"; // Waktu save edit password tidak akan di update
            txtPasswordConfirm.Text = "123";
            if (!string.IsNullOrEmpty(appUser.SRLanguage))
                cboSRLanguage.SelectedValue = appUser.SRLanguage;
            else
            {
                cboSRLanguage.SelectedValue = string.Empty;
                cboSRLanguage.Text = string.Empty;
            }

            ComboBox.SelectedValue(cboSRUserType, appUser.SRUserType);
            txtActiveDate.SelectedDate = appUser.ActiveDate;
            txtExpireDate.SelectedDate = appUser.ExpireDate;
            if (!string.IsNullOrEmpty(appUser.ParamedicID))
                cboParamedicID.SelectedValue = appUser.ParamedicID;
            else
            {
                cboParamedicID.SelectedValue = string.Empty;
                cboParamedicID.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(appUser.ServiceUnitID))
                cboServiceUnitID.SelectedValue = appUser.ServiceUnitID;
            else
            {
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
            }

            txtLicenseNo.Text = appUser.LicenseNo;
            txtESignNik.Text = appUser.ESignNik;
            txtEmail.Text = appUser.Email;
            chkIsLocked.Checked = appUser.IsLocked ?? false;

            imgSignatureImage.DataValue = appUser.SignatureImage;

            Context.Cache.Remove(Session.SessionID + "_SignatureImage");
            if (appUser.SignatureImage != null)
                Context.Cache.Insert(Session.SessionID + "_SignatureyImage", appUser.SignatureImage, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            else
                imgSignatureImage.ImageUrl = ""; // Untuk menghilangkan image

            if (!string.IsNullOrEmpty(appUser.PersonID.ToString()) && appUser.PersonID != -1)
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(appUser.PersonID));
                var dtb = personal.LoadDataTable();
                cboPersonID.DataSource = dtb;
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = appUser.PersonID.ToString();
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.Text = string.Empty;
            }

            //Refresh Detail
            if (IsPostBack)
            {
                RefreshGridUserGroup();
                RefreshGridServiceUnit();
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(AjaxManager, uplSignatureImage);
            ajax.AddAjaxSetting(AjaxManager, imgSignatureImage);
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
            auditLogFilter.PrimaryKeyData = "UserID='" + txtUserID.Text.Trim() + "'";
            auditLogFilter.TableName = "AppUser";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtUserID.Enabled = (newVal == AppEnum.DataMode.New);

            if (newVal == AppEnum.DataMode.New)
                OnPopulateEntryControl(new AppUser());

            txtPassword.Enabled = newVal == AppEnum.DataMode.New;
            txtPasswordConfirm.Enabled = newVal == AppEnum.DataMode.New;
            uplSignatureImage.Enabled = (newVal != AppEnum.DataMode.Read);


            //Refresh Grid Detail
            grdUserUserGroup.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            RefreshGridUserGroup();
            grdUserServiceUnit.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            RefreshGridServiceUnit();

            //Refresh Selection Check
            switch (newVal)
            {
                case AppEnum.DataMode.New:
                    foreach (GridDataItem dataItem in grdUserUserGroup.MasterTableView.Items)
                        dataItem.Selected = false;

                    foreach (GridDataItem dataItem in grdUserServiceUnit.MasterTableView.Items)
                        dataItem.Selected = false;

                    break;
                case AppEnum.DataMode.Edit:
                    foreach (GridDataItem dataItem in grdUserUserGroup.MasterTableView.Items)
                        dataItem.Selected = (bool)dataItem.GetDataKeyValue("IsSelect");

                    foreach (GridDataItem dataItem in grdUserServiceUnit.MasterTableView.Items)
                        dataItem.Selected = (bool)dataItem.GetDataKeyValue("IsSelect");

                    break;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "UserSearch.aspx";
            UrlPageList = "UserList.aspx";

            ProgramID = AppConstant.Program.User;

            //Window Search
            WindowSearch.Height = 120;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var meds = new ParamedicCollection();
                meds.Query.Where(
                    meds.Query.IsActive == true,
                    meds.Query.IsAvailable == true
                    );
                meds.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                foreach (var med in meds)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(med.ParamedicName, med.ParamedicID));
                }

                var sus = new ServiceUnitCollection();
                sus.Query.Where(sus.Query.IsActive == true);
                sus.LoadAll();

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var su in sus)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(su.ServiceUnitName, su.ServiceUnitID));
                }

                StandardReference.InitializeIncludeSpace(cboSRLanguage, AppEnum.StandardReference.Language);
                StandardReference.InitializeIncludeSpace(cboSRUserType, AppEnum.StandardReference.UserType);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            if (txtUserID.Text == "sci" && AppSession.UserLogin.UserID != "sci")
            {
                args.MessageText = "You don't have authorization to delete this data";
                args.IsCancel = true;
                return;
            }

            AppUser entity = new AppUser();
            entity.LoadByPrimaryKey(txtUserID.Text);
            entity.MarkAsDeleted();

            AppUserUserGroupCollection collDetail = new AppUserUserGroupCollection();
            collDetail.Query.Where(collDetail.Query.UserID == txtUserID.Text);
            collDetail.LoadAll();
            collDetail.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                // Save Detail
                collDetail.Save();

                //Save Header
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            //Cek Password
            if (txtPassword.Text == string.Empty)
            {
                args.MessageText = AppConstant.Message.PasswordCantEmpty;
                args.IsCancel = true;
                return;
            }
            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                args.MessageText = AppConstant.Message.PasswordConfirmNotValid;
                args.IsCancel = true;
                return;
            }

            AppUser entity = new AppUser();
            if (entity.LoadByPrimaryKey(txtUserID.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            AppUserUserGroupCollection userGroups = new AppUserUserGroupCollection();
            AppUserServiceUnitCollection userServiceUnits = new AppUserServiceUnitCollection();
            entity = new AppUser();
            entity.AddNew();
            SetEntityValue(entity, userGroups, userServiceUnits);
            SaveEntity(entity, userGroups, userServiceUnits);
        }

        private void SaveEntity(AppUser entity, AppUserUserGroupCollection userGroups, AppUserServiceUnitCollection userServiceUnits)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //Save Header
                entity.Save();

                // Save Detail
                userGroups.Save();
                userServiceUnits.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (txtUserID.Text == "sci" && AppSession.UserLogin.UserID != "sci")
            {
                args.MessageText = "You don't have authorization to edit this data";
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AppUser entity = new AppUser();
            if (entity.LoadByPrimaryKey(txtUserID.Text))
            {
                AppUserUserGroupCollection userGroups = new AppUserUserGroupCollection();
                AppUserServiceUnitCollection userServiceUnits = new AppUserServiceUnitCollection();
                SetEntityValue(entity, userGroups, userServiceUnits);
                SaveEntity(entity, userGroups, userServiceUnits);
            }
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }


        #endregion

        #region UserUserGroup

        protected void grdUserUserGroup_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdUserUserGroup.DataSource = DetailUserGroup;
        }

        private DataTable DetailUserGroup
        {
            get
            {
                object obj = this.Session["UserGroup"];
                if (obj != null)
                    return ((DataTable)(obj));

                AppUserUserGroupCollection coll = new AppUserUserGroupCollection();
                DataTable dtb = DataModeCurrent == AppEnum.DataMode.Read
                                    ? coll.GetInnerJoinWUserGroup(txtUserID.Text)
                                    : coll.GetFullJoinWUserGroup(txtUserID.Text);

                Session["UserGroup"] = dtb;
                return dtb;
            }
        }

        private void RefreshGridUserGroup()
        {
            Session["UserGroup"] = null;
            grdUserUserGroup.Rebind();
        }

        #endregion

        #region UserServiceUnit

        protected void grdUserServiceUnit_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdUserServiceUnit.DataSource = DetailServiceUnit;
        }

        private DataTable DetailServiceUnit
        {
            get
            {
                object obj = this.Session["ServiceUnit"];
                if (obj != null)
                    return ((DataTable)(obj));

                AppUserServiceUnitCollection coll = new AppUserServiceUnitCollection();
                DataTable dtb = DataModeCurrent == AppEnum.DataMode.Read
                                    ? coll.GetInnerJoinWUser(txtUserID.Text)
                                    : coll.GetFullJoinWUser(txtUserID.Text);

                Session["ServiceUnit"] = dtb;
                return dtb;
            }
        }

        private void RefreshGridServiceUnit()
        {
            Session["ServiceUnit"] = null;
            grdUserServiceUnit.Rebind();
        }

        #endregion

        protected void uplSignatureImage_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            using (Stream stream = e.File.InputStream)
            {
                byte[] imgData = new byte[stream.Length];
                stream.Read(imgData, 0, imgData.Length);
                imgSignatureImage.DataValue = imgData;

                Context.Cache.Remove(Session.SessionID + "_SignatureImage");
                Context.Cache.Insert(Session.SessionID + "_SignatureImage", imgData, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            }
        }
    }
}
