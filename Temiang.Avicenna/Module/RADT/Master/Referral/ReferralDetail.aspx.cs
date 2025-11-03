using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ReferralDetail : BasePageDetail
    {
        private void SetEntityValue(Referral entity)
        {
            entity.ReferralID = txtReferralID.Text;
            entity.ReferralName = txtReferralName.Text;
            entity.ShortName = txtShortName.Text;
            entity.DepartmentName = txtDepartmentName.Text;
            entity.SRReferralGroup = cboSRReferralGroup.SelectedValue;
            entity.TaxRegistrationNo = txtTaxRegistrationNo.Text;
            entity.IsPKP = chkIsPKP.Checked;
            entity.StreetName = AddressCtl1.StreetName;
            entity.District = AddressCtl1.District;
            entity.City = AddressCtl1.City;
            entity.County = AddressCtl1.County;
            entity.State = AddressCtl1.State;
            entity.ZipCode = AddressCtl1.ZipCode;
            entity.PhoneNo = AddressCtl1.PhoneNo;
            entity.FaxNo = AddressCtl1.FaxNo;
            entity.Email = AddressCtl1.Email;
            entity.MobilePhoneNo = AddressCtl1.MobilePhoneNo;
            entity.IsRefferalFrom = chkIsRefferalFrom.Checked;
            entity.IsRefferalTo = chkIsRefferalTo.Checked;
            entity.IsActive = chkIsActive.Checked;
            entity.ParamedicID = cboParamedicID.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ReferralQuery que = new ReferralQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ReferralID > txtReferralID.Text);
                que.OrderBy(que.ReferralID.Ascending);
            }
            else
            {
                que.Where(que.ReferralID < txtReferralID.Text);
                que.OrderBy(que.ReferralID.Descending);
            }
            Referral entity = new Referral();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Referral entity = new Referral();
            if (parameters.Length > 0)
            {
                String referralID = (String)parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(referralID);
            }
            else
                entity.LoadByPrimaryKey(txtReferralID.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Referral referral = (Referral)entity;
            txtReferralID.Text = referral.ReferralID;
            txtReferralName.Text = referral.ReferralName;
            txtShortName.Text = referral.ShortName;
            txtDepartmentName.Text = referral.DepartmentName;
            //cboSRReferralGroup.SelectedValue = referral.SRReferralGroup;
            if (!string.IsNullOrEmpty(referral.SRReferralGroup))
            {
                var rg = new AppStandardReferenceItemQuery();
                rg.Where(rg.StandardReferenceID == AppEnum.StandardReference.ReferralGroup.ToString(), rg.ItemID == referral.SRReferralGroup);
                cboSRReferralGroup.DataSource = rg.LoadDataTable();
                cboSRReferralGroup.DataBind();
                cboSRReferralGroup.SelectedValue = referral.SRReferralGroup;
            }
            else
            {
                cboSRReferralGroup.Items.Clear();
                cboSRReferralGroup.Text = string.Empty;
                cboSRReferralGroup.SelectedValue = string.Empty;
            }
            txtTaxRegistrationNo.Text = referral.TaxRegistrationNo;
            chkIsPKP.Checked = referral.IsPKP ?? false;
            AddressCtl1.StreetName = referral.StreetName;
            AddressCtl1.District = referral.District;
            AddressCtl1.City = referral.City;
            AddressCtl1.County = referral.County;
            AddressCtl1.State = referral.State;
            AddressCtl1.ZipCode = referral.ZipCode;
            AddressCtl1.PhoneNo = referral.PhoneNo;
            AddressCtl1.FaxNo = referral.FaxNo;
            AddressCtl1.Email = referral.Email;
            AddressCtl1.MobilePhoneNo = referral.MobilePhoneNo;
            chkIsRefferalFrom.Checked = referral.IsRefferalFrom ?? false;
            chkIsRefferalTo.Checked = referral.IsRefferalTo ?? false;
            chkIsActive.Checked = referral.IsActive ?? false;
            cboParamedicID.SelectedValue = referral.ParamedicID;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Referral());
            cboParamedicID.Text = string.Empty;
            cboParamedicID.SelectedValue = string.Empty;
            chkIsRefferalFrom.Checked = true;
            chkIsRefferalTo.Checked = true;
            chkIsActive.Checked = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("ReferralID='{0}'", txtReferralID.Text.Trim());
            auditLogFilter.TableName = "Referral";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtReferralID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ReferralSearch.aspx";
            UrlPageList = "ReferralList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.Referral;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                var parColl = new ParamedicCollection();
                parColl.Query.Where(parColl.Query.IsActive == true);
                parColl.Query.OrderBy(parColl.Query.ParamedicID.Ascending);
                parColl.LoadAll();
                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic par in parColl)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(par.ParamedicName, par.ParamedicID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Referral entity = new Referral();
            entity.LoadByPrimaryKey(txtReferralID.Text);
            entity.MarkAsDeleted();
            SaveEntity(entity);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRReferralGroup.SelectedValue))
            {
                args.MessageText = "Referral Group required.";
                args.IsCancel = true;
                return;
            }

            Referral entity = new Referral();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(Referral entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRReferralGroup.SelectedValue))
            {
                args.MessageText = "Referral Group required.";
                args.IsCancel = true;
                return;
            }

            Referral entity = new Referral();
            if (entity.LoadByPrimaryKey(txtReferralID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion
        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ParamedicQuery query = new ParamedicQuery("a");
            query.Where
                (
                    query.Or
                        (
                             query.ParamedicName.Like(searchTextContain),
                             query.ParamedicID.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.ParamedicName.Ascending);
            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();

            cboParamedicID.DataSource = dtb;
            cboParamedicID.DataBind();

        }

        protected void cboSRReferralGroup_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        protected void cboSRReferralGroup_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.StandardReferenceItemsRequested((RadComboBox)o, "ReferralGroup", e.Text);
        }
    }
}