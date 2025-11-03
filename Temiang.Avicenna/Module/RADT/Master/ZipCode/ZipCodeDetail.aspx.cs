using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ZipCodeDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ZipCodeSearch.aspx";
            UrlPageList = "ZipCodeList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.ZipCode;

			//StandardReference Initialize
			if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRProvince, AppEnum.StandardReference.Province);

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateZipCodeIdAutomatic) == "Yes")
                    rfvZipCode.Visible = false;
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
            OnPopulateEntryControl(new ZipCode());

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsCreateZipCodeIdAutomatic))
            {
                txtZipCode.Text = GetZipCodeId();
                txtZipCode.ReadOnly = true;
            }

            cboSRProvince.Text = string.Empty;
            cboSRProvince.SelectedValue = string.Empty;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ZipCode entity = new ZipCode();
            if (entity.LoadByPrimaryKey(txtZipCode.Text))
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
            if (string.IsNullOrEmpty(cboSRProvince.SelectedValue))
            {
                args.MessageText = "Province required.";
                args.IsCancel = true;
                return;
            }
            
            ZipCode entity = new ZipCode();
            if (entity.LoadByPrimaryKey(txtZipCode.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ZipCode();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRProvince.SelectedValue))
            {
                args.MessageText = "Province required.";
                args.IsCancel = true;
                return;
            }
            ZipCode entity = new ZipCode();
            if (entity.LoadByPrimaryKey(txtZipCode.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("ZipCode='{0}'", txtZipCode.Text.Trim());
            auditLogFilter.TableName = "ZipCode";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtZipCode.ReadOnly = (newVal != AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ZipCode entity = new ZipCode();
            if (parameters.Length > 0)
            {
                String zipCode = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(zipCode);
            }
            else
            {
                entity.LoadByPrimaryKey(txtZipCode.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ZipCode zipCode = (ZipCode)entity;
            txtZipCode.Text = zipCode.ZipCode;
            txtZipPostalCode.Text = zipCode.ZipPostalCode;
            txtStreetName.Text = zipCode.StreetName;
            txtDistrict.Text = zipCode.District;
            txtCounty.Text = zipCode.County;
            txtCity.Text = zipCode.City;
            cboSRProvince.SelectedValue = zipCode.SRProvince;
            txtLatitude.Value = Convert.ToDouble(zipCode.Latitude);
            txtLongitude.Value = Convert.ToDouble(zipCode.Longitude);
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(ZipCode entity)
        {
            entity.ZipCode = txtZipCode.Text;
            entity.ZipPostalCode = txtZipPostalCode.Text;
            entity.StreetName = txtStreetName.Text;
            entity.District = txtDistrict.Text;
            entity.County = txtCounty.Text;
            entity.City = txtCity.Text;
            entity.SRProvince = cboSRProvince.SelectedValue;
            entity.Latitude = Convert.ToDecimal(txtLatitude.Value);
            entity.Longitude = Convert.ToDecimal(txtLongitude.Value);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ZipCode entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ZipCodeQuery que = new ZipCodeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ZipCode > txtZipCode.Text);
                que.OrderBy(que.ZipCode.Ascending);
            }
            else
            {
                que.Where(que.ZipCode < txtZipCode.Text);
                que.OrderBy(que.ZipCode.Descending);
            }
            ZipCode entity = new ZipCode();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        private string GetZipCodeId()
        {
            var query = new ZipCodeQuery("a");
            query.es.Top = 1;
            query.Select(query.ZipCode);
            query.OrderBy(query.ZipCode.Descending);

            var zc = new ZipCode();
            zc.Load(query);

            string iId;
            if (zc.ZipCode != null)
            {
                int x = (int.Parse(zc.ZipCode) + 1);
                iId = string.Format("{0:0000000000}", x);
            }
            else
                iId = "0000000001";

            return iId;
        }
    }
}

