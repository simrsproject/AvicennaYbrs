using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.CRM.Master
{
    public partial class MembershipItemRedeemDetail : BasePageDetail
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "MembershipItemRedeemSearch.aspx";
            UrlPageList = "MembershipItemRedeemList.aspx";

            ProgramID = AppConstant.Program.MembershipItemRedeem;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRItemReedemGroup, AppEnum.StandardReference.ItemReedemGroup);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void SetEntityValue(MembershipItemRedeem entity)
        {
            entity.ItemReedemID = txtItemReedemID.Text;
            entity.ItemReedemName = txtItemReedemName.Text;
            entity.SRItemReedemGroup = cboSRItemReedemGroup.SelectedValue;
            entity.PointsUsed = Convert.ToDecimal(txtPointsUsed.Value);
            entity.IsActive = chkIsActive.Checked;
            
            //Last Update Status
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new MembershipItemRedeemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ItemReedemID > txtItemReedemID.Text);
                que.OrderBy(que.ItemReedemID.Ascending);
            }
            else
            {
                que.Where(que.ItemReedemID < txtItemReedemID.Text);
                que.OrderBy(que.ItemReedemID.Descending);
            }
            var entity = new MembershipItemRedeem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MembershipItemRedeem();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(id);
            }
            else
            {
                entity.LoadByPrimaryKey(txtItemReedemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var itemRedeem = (MembershipItemRedeem)entity;
            txtItemReedemID.Text = itemRedeem.ItemReedemID;
            txtItemReedemName.Text = itemRedeem.ItemReedemName;
            if (!string.IsNullOrEmpty(itemRedeem.SRItemReedemGroup))
                cboSRItemReedemGroup.SelectedValue = itemRedeem.SRItemReedemGroup;
            else {
                cboSRItemReedemGroup.SelectedValue = string.Empty;
                cboSRItemReedemGroup.Text = string.Empty;
            }
            txtPointsUsed.Value = Convert.ToDouble(itemRedeem.PointsUsed);
            chkIsActive.Checked = itemRedeem.IsActive ?? false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MembershipItemRedeem());
            txtItemReedemID.Text = GetItemId();
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
            auditLogFilter.PrimaryKeyData = string.Format("ItemReedemID='{0}'", txtItemReedemID.Text.Trim());
            auditLogFilter.TableName = "MembershipItemRedeem";
        }

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //txtItemReedemID.Enabled = (newVal == Temiang.Avicenna.Common.AppEnum.DataMode.New);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new MembershipItemRedeem();
            if (entity.LoadByPrimaryKey(txtItemReedemID.Text))
            {
                // cek apakah master item sudah ada transaksi
                var cek = new MembershipItemRedemptionItemCollection();
                cek.Query.Where(cek.Query.ItemReedemID == txtItemReedemID.Text);
                cek.LoadAll();
                if (cek.Count > 0)
                {
                    args.MessageText = "Item already used in transaction.";
                    args.IsCancel = true;
                    return;
                }

                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (txtPointsUsed.Value <= 0)
            {
                args.MessageText = "Points Used must greater than zero";
                args.IsCancel = true;
                return;
            }

            var entity = new MembershipItemRedeem();
            if (entity.LoadByPrimaryKey(txtItemReedemID.Text))
            {
                txtItemReedemID.Text = GetItemId();
            }
            entity = new MembershipItemRedeem();

            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(MembershipItemRedeem entity)
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
            if (txtPointsUsed.Value <= 0)
            {
                args.MessageText = "Points Used must greater than zero";
                args.IsCancel = true;
                return;
            }

            var entity = new MembershipItemRedeem();
            if (entity.LoadByPrimaryKey(txtItemReedemID.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
        }

        private static string GetItemId()
        {
            var query = new MembershipItemRedeemQuery("a");
            query.es.Top = 1;
            query.Select(query.ItemReedemID);
            query.OrderBy(query.ItemReedemID.Descending);

            var item = new MembershipItemRedeem();
            item.Load(query);

            string iId;
            if (item.ItemReedemID != null)
            {
                int x = (int.Parse(item.ItemReedemID) + 1);
                iId = string.Format("{0:0000}", x);
            }
            else
                iId = "0001";

            return iId;
        }
    }
}