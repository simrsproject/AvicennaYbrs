using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class OrganizationUnitDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "OrganizationUnitSearch.aspx";
            UrlPageList = "OrganizationUnitList.aspx";

            WindowSearch.Height = 300;

            ProgramID = AppConstant.Program.OrganizationUnit; //TODO: Isi ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpaceSortByLineNumber(cboSROrganizationLevel, AppEnum.StandardReference.OrganizationLevel);
                trSubledgerId.Visible = AppSession.Parameter.acc_IsAutoJournalPayroll;
                trDirectCost.Visible = trSubledgerId.Visible && AppSession.Parameter.acc_IsJournalPayrollWithDirectIndirectCost;
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
            OnPopulateEntryControl(new OrganizationUnit());
            chkIsActive.Checked = true;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            OrganizationUnit entity = new OrganizationUnit();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtOrganizationUnitID.Text)))
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
            OrganizationUnit entity = new OrganizationUnit();
            entity = new OrganizationUnit();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            OrganizationUnit entity = new OrganizationUnit();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtOrganizationUnitID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("OrganizationUnitID='{0}'", txtOrganizationUnitID.Text.Trim());
            auditLogFilter.TableName = "OrganizationUnit";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtOrganizationUnitID.Enabled = (newVal == AppEnum.DataMode.New);

        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            OrganizationUnit entity = new OrganizationUnit();
            if (parameters.Length > 0)
            {
                string organizationUnitID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(organizationUnitID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtOrganizationUnitID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            OrganizationUnit organizationUnit = (OrganizationUnit)entity;
            txtOrganizationUnitID.Value = Convert.ToDouble(organizationUnit.OrganizationUnitID);
            txtOrganizationUnitCode.Text = organizationUnit.OrganizationUnitCode;
            txtOrganizationUnitName.Text = organizationUnit.OrganizationUnitName;

            OrganizationUnitQuery pgQuery = new OrganizationUnitQuery();
            pgQuery.Where(pgQuery.OrganizationUnitID == Convert.ToInt32(organizationUnit.ParentOrganizationUnitID));
            var dtb = pgQuery.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                cboParentOrganizationUnitID.DataSource = dtb;
                cboParentOrganizationUnitID.DataBind();
                cboParentOrganizationUnitID.SelectedValue = organizationUnit.ParentOrganizationUnitID.ToString();
            }
            cboSROrganizationLevel.SelectedValue = organizationUnit.SROrganizationLevel;
            chkIsActive.Checked = organizationUnit.IsActive ?? false;

            if (organizationUnit.PersonID.HasValue && organizationUnit.PersonID > 0)
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(organizationUnit.PersonID));
                var dtb2 = personal.LoadDataTable();
                cboPersonID.DataSource = dtb2;
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = organizationUnit.PersonID.ToString();
                if (dtb2.Rows.Count > 0)
                    cboPersonID.Text = dtb2.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb2.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.SelectedValue = string.Empty;
                cboPersonID.Text = string.Empty;
            }

            int subLedgerId = (organizationUnit.SubLedgerId.HasValue ? organizationUnit.SubLedgerId.Value : 0);
            if (subLedgerId != 0)
            {
                SubLedgersQuery slQ = new SubLedgersQuery();
                slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
                slQ.Where(slQ.SubLedgerId == subLedgerId);
                DataTable dtbSl = slQ.LoadDataTable();
                cboSubledgerId.DataSource = dtbSl;
                cboSubledgerId.DataBind();
                cboSubledgerId.SelectedValue = subLedgerId.ToString();

                var sl = new SubLedgers();
                if (sl.LoadByPrimaryKey(subLedgerId))
                    chkIsDirectCost.Checked = sl.IsDirectCost ?? true;
            }
            else
            {
                cboSubledgerId.Items.Clear();
                cboSubledgerId.Text = string.Empty;
                chkIsDirectCost.Checked = true;
            }
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(OrganizationUnit entity)
        {
            entity.OrganizationUnitID = Convert.ToInt32(txtOrganizationUnitID.Value);
            entity.OrganizationUnitCode = txtOrganizationUnitCode.Text;
            entity.OrganizationUnitName = txtOrganizationUnitName.Text;
            entity.ParentOrganizationUnitID = string.IsNullOrEmpty(cboParentOrganizationUnitID.SelectedValue) ? 0 : Convert.ToInt32(cboParentOrganizationUnitID.SelectedValue);
            entity.SROrganizationLevel = cboSROrganizationLevel.SelectedValue;
            entity.IsActive = chkIsActive.Checked;

            int subLedgerId = 0;
            int.TryParse(cboSubledgerId.SelectedValue, out subLedgerId);
            entity.SubLedgerId = subLedgerId;

            if (string.IsNullOrEmpty(cboPersonID.SelectedValue))
                entity.PersonID = 0;
            else
                entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(OrganizationUnit entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                if (trDirectCost.Visible && entity.SubLedgerId.HasValue)
                {
                    var sl = new SubLedgers();
                    if (sl.LoadByPrimaryKey(entity.SubLedgerId ?? 0))
                    {
                        sl.IsDirectCost = chkIsDirectCost.Checked;
                        sl.Save();
                    }
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            OrganizationUnitQuery que = new OrganizationUnitQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.OrganizationUnitID > txtOrganizationUnitID.Text);
                que.OrderBy(que.OrganizationUnitID.Ascending);
            }
            else
            {
                que.Where(que.OrganizationUnitID < txtOrganizationUnitID.Text);
                que.OrderBy(que.OrganizationUnitID.Descending);
            }
            OrganizationUnit entity = new OrganizationUnit();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        protected void cboSubledgerId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cboSubledgerId.SelectedValue))
            {
                var sl = new SubLedgers();
                if (sl.LoadByPrimaryKey(e.Value.ToInt()))
                    chkIsDirectCost.Checked = sl.IsDirectCost ?? true;
            }
        }
        #endregion

        #region ComboBox Function

        protected void cboParentOrganizationUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            OrganizationUnitQuery query = new OrganizationUnitQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.OrganizationUnitID,
                    query.OrganizationUnitCode,
                    query.OrganizationUnitName,
                    query.ParentOrganizationUnitID
                );
            query.Where
                (
                    query.Or
                        (
                            query.OrganizationUnitID.Like(searchTextContain),
                            query.OrganizationUnitName.Like(searchTextContain)
                        )
                );

            cboParentOrganizationUnitID.DataSource = query.LoadDataTable();
            cboParentOrganizationUnitID.DataBind();
        }

        protected void cboParentOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
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
                    query.SREmployeeStatus == "1",
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
        #endregion ComboBox Function

        #region ComboBox SubledgerId

        protected void cboSubledgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == AppSession.Parameter.SubLedgerGroupIdServiceUnit);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerId.DataSource = dtb;
            cboSubledgerId.DataBind();
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }

        #endregion
    }
}
