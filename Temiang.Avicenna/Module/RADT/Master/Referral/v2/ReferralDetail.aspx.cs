using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Linq;
using System.Configuration;
using Temiang.Avicenna.Module.Finance.Master;

namespace Temiang.Avicenna.Module.RADT.Master.Referralv2
{
    public partial class ReferralDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber, _autoNumberDetail;

        private void SetEntityValue(AppStandardReferenceItem entity)
        {
            if (entity.es.IsAdded && AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateReferralGroupIdAutomatic) == "Yes")
            {
                txtItemID.Text = GetNewId();
                _autoNumber.Save();
            }

            entity.StandardReferenceID = "ReferralGroup";
            entity.ItemID = txtItemID.Text;
            entity.ItemName = txtItemName.Text;
            entity.Note = txtNote.Text;
            entity.ReferenceID = cboReferenceID.SelectedValue;
            entity.IsUsedBySystem = false;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //Item
            var coll = Referrals;
            foreach (Referral item in coll)
            {
                item.SRReferralGroup = txtItemID.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new AppStandardReferenceItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            que.Where(que.StandardReferenceID == AppEnum.StandardReference.ReferralGroup);
            if (isNextRecord)
            {
                que.Where(que.ItemID > txtItemID.Text);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.ItemID < txtItemID.Text);
                que.OrderBy(que.ItemID.Descending);
            }
            var entity = new AppStandardReferenceItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                String id = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(AppEnum.StandardReference.ReferralGroup.ToString(), id);
            }
            else
                entity.LoadByPrimaryKey(AppEnum.StandardReference.ReferralGroup.ToString(), txtItemID.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var rg = (AppStandardReferenceItem)entity;
            txtItemID.Text = rg.ItemID;
            txtItemName.Text = rg.ItemName;
            txtNote.Text = rg.Note;
            cboReferenceID.SelectedValue = rg.ReferenceID;
            chkIsActive.Checked = rg.IsActive ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdReferral, grdReferral);
        }

        private string GetNewId()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.ReferralGroupId);

            return _autoNumber.LastCompleteNumber;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AppStandardReferenceItem());

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateReferralGroupIdAutomatic) == "Yes")
            {
                txtItemID.Text = GetNewId();
                txtItemName.Focus();
            }

            cboReferenceID.SelectedValue = string.Empty;
            cboReferenceID.Text = string.Empty;
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
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtItemID.ReadOnly = (newVal != AppEnum.DataMode.New) || AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateReferralGroupIdAutomatic) == "Yes";
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "ReferralSearch.aspx";
            UrlPageList = "ReferralList.aspx";

            ProgramID = AppConstant.Program.Referral;

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                cboReferenceID.Items.Add(new RadComboBoxItem("", ""));
                cboReferenceID.Items.Add(new RadComboBoxItem("RS", "RS"));
                cboReferenceID.Items.Add(new RadComboBoxItem("RS LAIN", "RSLAIN"));
                cboReferenceID.Items.Add(new RadComboBoxItem("BIDAN", "BIDAN"));
                cboReferenceID.Items.Add(new RadComboBoxItem("PUSKESMAS", "PUSKESMAS"));
                cboReferenceID.Items.Add(new RadComboBoxItem("FASKES", "FASKES"));
                cboReferenceID.Items.Add(new RadComboBoxItem("FASKES LAIN", "FASKESLAIN"));
                cboReferenceID.Items.Add(new RadComboBoxItem("DUKUN", "DUKUN"));
                cboReferenceID.Items.Add(new RadComboBoxItem("DATANG SENDIRI", "DATANGSENDIRI"));

                // Reset data grid
                Referrals = null;
                PCareReferenceItemMappings = null;

            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                Referrals.Save();

                if (PCareReferenceItemMappings != null)
                    PCareReferenceItemMappings.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.ReferralGroup.ToString(), txtItemID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(AppEnum.StandardReference.ReferralGroup.ToString(), txtItemID.Text))
            {
                entity.MarkAsDeleted();

                var itemDetail = new ReferralCollection();
                itemDetail.Query.Where(itemDetail.Query.SRReferralGroup == txtItemID.Text);
                itemDetail.LoadAll();
                itemDetail.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    itemDetail.Save();

                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
        }

        #endregion

        #region Record Detail Method Function

        private ReferralCollection Referrals
        {
            get
            {
                object obj = Session["collReferral"];
                if (obj != null)
                    return ((ReferralCollection)(obj));

                var coll = new ReferralCollection();
                var query = new ReferralQuery("a");

                query.Select(query);
                query.Where(query.SRReferralGroup == txtItemID.Text);
                query.OrderBy(query.ReferralID.Ascending);
                coll.Load(query);

                //Bila nama session dirubah, rubah jug yg di StandardReferenceItemDetail.ascx.cs
                Session["collReferral"] = coll;
                return coll;
            }
            set
            {
                Session["collReferral"] = value;
            }
        }

        private PCareReferenceItemMappingCollection PCareReferenceItemMappings
        {
            get
            {
                object obj = Session["collPcare"];
                if (obj != null)
                    return ((PCareReferenceItemMappingCollection)(obj));

                var coll = new PCareReferenceItemMappingCollection();
                var query = new PCareReferenceItemMappingQuery("a");

                query.Select(query);
                query.Where(query.MappingWithID == ""); // Select 0 record
                query.OrderBy(query.MappingWithID.Ascending);
                coll.Load(query);

                //Bila nama session dirubah, rubah jug yg di StandardReferenceItemDetail.ascx.cs
                Session["collPcare"] = coll;
                return coll;
            }
            set
            {
                Session["collPcare"] = value;
            }
        }


        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdReferral.Columns[0].Visible = isVisible;
            grdReferral.Columns[grdReferral.Columns.Count - 1].Visible = isVisible;

            grdReferral.MasterTableView.CommandItemDisplay = isVisible
                ? GridCommandItemDisplay.Top
                : GridCommandItemDisplay.None;

            grdReferral.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Reset Record Detail
            Referrals = null;

            //Requery Record Detail
            grdReferral.DataSource = Referrals;
            grdReferral.MasterTableView.IsItemInserted = false;
            grdReferral.MasterTableView.ClearEditItems();
            grdReferral.DataBind();
        }

        protected void grdReferral_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdReferral.DataSource = Referrals;
        }

        protected void grdReferral_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ReferralID"].ToString();

            Referral item = Referrals.FindByPrimaryKey(id);

            SetEntityValue(item, e);
        }

        private void SetEntityValue(Referral item, GridCommandEventArgs e)
        {
            var ctl = (ReferralDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl != null)
            {
                if (string.IsNullOrEmpty(ctl.ReferralID) && (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsCreateReferralIdAutomatic) == "Yes"))
                {
                    _autoNumberDetail = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.ReferralId);
                    item.ReferralID = _autoNumberDetail.LastCompleteNumber;
                    _autoNumberDetail.Save();
                }
                else
                    item.ReferralID = ctl.ReferralID;

                item.ReferralName = ctl.ReferralName;
                item.ShortName = ctl.ShortName;
                item.DepartmentName = ctl.DepartmentName;
                item.TaxRegistrationNo = ctl.TaxRegistrationNo;
                item.IsPKP = ctl.IsPKP;
                item.StreetName = ctl.StreetName;
                item.District = ctl.District;
                item.City = ctl.City;
                item.County = ctl.County;
                item.State = ctl.State;
                item.ZipCode = ctl.ZipCode;
                item.PhoneNo = ctl.PhoneNo;
                item.FaxNo = ctl.FaxNo;
                item.Email = ctl.Email;
                item.MobilePhoneNo = ctl.MobilePhoneNo;
                item.IsRefferalFrom = ctl.IsRefferalFrom;
                item.IsRefferalTo = ctl.IsRefferalTo;
                item.IsActive = ctl.IsActive;
                item.ParamedicID = ctl.ParamedicID;

                // PCareReferenceItemMapping Provider
                var pCareValidation = ConfigurationManager.AppSettings["PCareValidation"];
                if (!string.IsNullOrEmpty(pCareValidation) && pCareValidation.ToUpper().Equals("YES"))
                {
                    var isNewMap = false;
                    var pcareMap = PCareReferenceItemMappings.Where(r => r.MappingWithID == ctl.ReferralID).FirstOrDefault();
                    if (pcareMap == null)
                    {
                        pcareMap = new PCareReferenceItemMapping();
                        if (!pcareMap.LoadByPrimaryKey("Provider", ctl.ReferralID))
                        {
                            if (!string.IsNullOrEmpty(ctl.PCareItemID))
                            {
                                isNewMap = true;
                                pcareMap = new PCareReferenceItemMapping();
                            }
                            else
                                pcareMap = null;
                        }
                    }

                    if (pcareMap != null)
                    {
                        pcareMap.ReferenceID = "Provider";
                        pcareMap.MappingWithID = ctl.ReferralID;
                        pcareMap.ItemID = ctl.PCareItemID;

                        if (isNewMap)
                            PCareReferenceItemMappings.AttachEntity(pcareMap);
                    }
                }
            }
        }


        protected void grdReferral_DeleteCommand(object source, GridCommandEventArgs e)
        {
            string id = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ReferralID"].ToString();

            var coll = Referrals;
            foreach (Referral item in coll)
            {
                if (item.ReferralID.Equals(id))
                {
                    item.MarkAsDeleted();
                    break;
                }
            }
        }

        protected void grdReferral_InsertCommand(object source, GridCommandEventArgs e)
        {
            var item = Referrals.AddNew();
            SetEntityValue(item, e);
            //Stay in insert mode
            e.Canceled = true;
            grdReferral.Rebind();
        }

        #endregion
    }
}