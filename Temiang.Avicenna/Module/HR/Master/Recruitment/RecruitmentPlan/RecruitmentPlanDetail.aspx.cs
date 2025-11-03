using System;
using System.Data;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Recruitment.Master
{
    public partial class RecruitmentPlanDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "RecruitmentPlanSearch.aspx";
            UrlPageList = "RecruitmentPlanList.aspx";

            ProgramID = AppConstant.Program.RecruitmentPlan; //TODO: Isi ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
            {
                trRecruitmentPlanName.Visible = !AppSession.Parameter.IsAutoRecruitmentPlanName;
                rfvPositionID.Visible = AppSession.Parameter.IsAutoRecruitmentPlanName;
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
            ajax.AddAjaxSetting(cboOrganizationUnitID, cboOrganizationUnitID);
            ajax.AddAjaxSetting(cboOrganizationUnitID, cboDivisionID);
            ajax.AddAjaxSetting(cboOrganizationUnitID, cboSubDivisonID);
            ajax.AddAjaxSetting(cboOrganizationUnitID, cboSectionID);

            ajax.AddAjaxSetting(cboDivisionID, cboDivisionID);
            ajax.AddAjaxSetting(cboDivisionID, cboSubDivisonID);
            ajax.AddAjaxSetting(cboDivisionID, cboSectionID);

            ajax.AddAjaxSetting(cboSubDivisonID, cboSubDivisonID);
            ajax.AddAjaxSetting(cboSubDivisonID, cboSectionID);

        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new RecruitmentPlan());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            RecruitmentPlan entity = new RecruitmentPlan();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtRecruitmentPlanID.Text)))
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
            RecruitmentPlan entity = new RecruitmentPlan();
            entity = new RecruitmentPlan();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            RecruitmentPlan entity = new RecruitmentPlan();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtRecruitmentPlanID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("RecruitmentPlanID='{0}'", txtRecruitmentPlanID.Text.Trim());
            auditLogFilter.TableName = "RecruitmentPlan";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtRecruitmentPlanID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            RecruitmentPlan entity = new RecruitmentPlan();
            if (parameters.Length > 0)
            {
                string recruitmentPlanID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(recruitmentPlanID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtRecruitmentPlanID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            RecruitmentPlan recruitmentPlan = (RecruitmentPlan)entity;
            txtRecruitmentPlanID.Value = Convert.ToDouble(recruitmentPlan.RecruitmentPlanID);
          

            var plQuery = new OrganizationUnitQuery();
            plQuery.Where(plQuery.OrganizationUnitID == Convert.ToInt32(recruitmentPlan.OrganizationUnitID));
            var dtb = plQuery.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                cboOrganizationUnitID.DataSource = dtb;
                cboOrganizationUnitID.DataBind();
                cboOrganizationUnitID.SelectedValue = recruitmentPlan.OrganizationUnitID.ToString();
            }
            else
            {
                cboOrganizationUnitID.DataSource = null;
                cboOrganizationUnitID.DataBind();
                cboOrganizationUnitID.Items.Clear();
                cboOrganizationUnitID.SelectedValue = string.Empty;
                cboOrganizationUnitID.Text = string.Empty;
            }

            var DivQuery = new OrganizationUnitQuery();
            DivQuery.Where(DivQuery.OrganizationUnitID == Convert.ToInt32(recruitmentPlan.DivisionID));
            var dtb1 = DivQuery.LoadDataTable();
            if (dtb1.Rows.Count > 0)
            {
                cboDivisionID.DataSource = dtb1;
                cboDivisionID.DataBind();
                cboDivisionID.SelectedValue = recruitmentPlan.DivisionID.ToString();
            }
            else
            {
                cboDivisionID.Items.Clear();
                cboDivisionID.SelectedValue = string.Empty;
                cboDivisionID.Text = string.Empty;
            }

            var SubQuery = new OrganizationUnitQuery();
            SubQuery.Where(SubQuery.OrganizationUnitID == Convert.ToInt32(recruitmentPlan.SubDivisionID));
            var dtb2 = SubQuery.LoadDataTable();
            if (dtb2.Rows.Count > 0)
            {
                cboSubDivisonID.DataSource = dtb2;
                cboSubDivisonID.DataBind();
                cboSubDivisonID.SelectedValue = recruitmentPlan.SubDivisionID.ToString();
            }
            else
            {
                cboSubDivisonID.Items.Clear();
                cboSubDivisonID.SelectedValue = string.Empty;
                cboSubDivisonID.Text = string.Empty;
            }

            var SecQuery = new OrganizationUnitQuery();
            SecQuery.Where(SecQuery.OrganizationUnitID == Convert.ToInt32(recruitmentPlan.SectionID));
            var dtb3 = SecQuery.LoadDataTable();
            if (dtb3.Rows.Count > 0)
            {
                cboSectionID.DataSource = dtb3;
                cboSectionID.DataBind();
                cboSectionID.SelectedValue = recruitmentPlan.SectionID.ToString();
            }
            else
            {
                cboSectionID.Items.Clear();
                cboSectionID.SelectedValue = string.Empty;
                cboSectionID.Text = string.Empty;
            }


            var POQuery = new PositionQuery();
            POQuery.Where(POQuery.PositionID == Convert.ToInt32(recruitmentPlan.PositionID));
            var dtb4 = POQuery.LoadDataTable();
            if (dtb4.Rows.Count > 0)
            {
                cboPositionID.DataSource = dtb4;
                cboPositionID.DataBind();
                cboPositionID.SelectedValue = recruitmentPlan.PositionID.ToString();


            }
            else
            {
                cboPositionID.Items.Clear();
                cboPositionID.SelectedValue = string.Empty;
                cboPositionID.Text = string.Empty;
            }

            txtRecruitmentPlanName.Text = recruitmentPlan.RecruitmentPlanName;
            txtNotes.Text = recruitmentPlan.Notes;

            txtValidFrom.SelectedDate = recruitmentPlan.ValidFrom;
            txtValidTo.SelectedDate = recruitmentPlan.ValidTo;
            txtNumberOfRequestedEmployees.Value = Convert.ToDouble(recruitmentPlan.NumberOfRequestedEmployees);




            //Display Data Detail
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(RecruitmentPlan entity)
        {
            entity.RecruitmentPlanID = Convert.ToInt32(txtRecruitmentPlanID.Value);
            entity.OrganizationUnitID = string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue) ? -1 : Convert.ToInt32(cboOrganizationUnitID.SelectedValue);
            entity.DivisionID = string.IsNullOrEmpty(cboDivisionID.SelectedValue) ? -1 : Convert.ToInt32(cboDivisionID.SelectedValue);
            entity.SubDivisionID = string.IsNullOrEmpty(cboSubDivisonID.SelectedValue) ? -1 : Convert.ToInt32(cboSubDivisonID.SelectedValue);
            entity.SectionID = string.IsNullOrEmpty(cboSectionID.SelectedValue) ? -1 : Convert.ToInt32(cboSectionID.SelectedValue);
            entity.PositionID = string.IsNullOrEmpty(cboPositionID.SelectedValue) ? -1 : Convert.ToInt32(cboPositionID.SelectedValue);
            entity.ValidFrom = txtValidFrom.SelectedDate;
            entity.ValidTo = txtValidTo.SelectedDate;
            entity.NumberOfRequestedEmployees = Convert.ToInt32(txtNumberOfRequestedEmployees.Value);
            entity.Notes = txtNotes.Text;

            if (!AppSession.Parameter.IsAutoRecruitmentPlanName)
            {
                entity.RecruitmentPlanName = txtRecruitmentPlanName.Text;
            }
            else
            {
                var rname = cboPositionID.Text;

                if (!string.IsNullOrEmpty(cboSectionID.SelectedValue))
                    rname += " [" + cboSectionID.Text + "]";
                else if((!string.IsNullOrEmpty(cboSubDivisonID.SelectedValue)))
                    rname += " [" + cboSubDivisonID.Text + "]";
                else if ((!string.IsNullOrEmpty(cboDivisionID.SelectedValue)))
                    rname += " [" + cboDivisionID.Text + "]";
                else if ((!string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue)))
                    rname += " [" + cboOrganizationUnitID.Text + "]";
                entity.RecruitmentPlanName = rname;
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(RecruitmentPlan entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtRecruitmentPlanID.Value = Convert.ToDouble(entity.RecruitmentPlanID);

            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            RecruitmentPlanQuery que = new RecruitmentPlanQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RecruitmentPlanID > txtRecruitmentPlanID.Text);
                que.OrderBy(que.RecruitmentPlanID.Ascending);
            }
            else
            {
                que.Where(que.RecruitmentPlanID < txtRecruitmentPlanID.Text);
                que.OrderBy(que.RecruitmentPlanID.Descending);
            }
            RecruitmentPlan entity = new RecruitmentPlan();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox

        #region ComboBox Function OrganizationUnit
        protected void cboOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboOrganizationUnitID((RadComboBox)sender, e.Text);
        }
        private void PopulateCboOrganizationUnitID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();

            query.Where(
                query.OrganizationUnitName.Like(searchTextContain));

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "3");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        private void PopulateCboOrganizationUnitID(RadComboBox comboBox, int textSearch) // open
        {
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();

        }
        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        protected void cboOrganizationUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboDivisionID.Items.Clear();
            cboDivisionID.Text = string.Empty;
            cboSubDivisonID.Items.Clear();
            cboSubDivisonID.Text = string.Empty;
            cboSectionID.Items.Clear();
            cboSectionID.Text = string.Empty;
        }

        #endregion ComboBox Function

        #region ComboBox Function Division
        protected void cboDivisionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboDivisionID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboDivisionID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitName.Like(searchTextContain));
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "2", query.ParentOrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt());
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        private void PopulatecboDivisionID(RadComboBox comboBox, int textSearch) //open
        {
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID.As("DivisionID"),
                         query.OrganizationUnitName.As("DivisionName"));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboDivisionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        protected void cboDivisionID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSectionID.Items.Clear();
            cboSectionID.Text = string.Empty;
            cboSubDivisonID.Items.Clear();
            cboSubDivisonID.Text = string.Empty;
        }

        #endregion ComboBox Function

        #region ComboBox Function SubDivision
        protected void cboSubDivisonID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSubDivisonID((RadComboBox)sender, e.Text);
        }
        private void PopulateCboSubDivisonID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitName.Like(searchTextContain));
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "1",
                query.Or(query.ParentOrganizationUnitID == cboDivisionID.SelectedValue.ToInt(), query.ParentOrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt())
                );
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        private void PopulateCboSubDivisonID(RadComboBox comboBox, int textSearch) //open
        {
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID.As("SubDivisionID"),
                         query.OrganizationUnitName.As("SubDivisionName"));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSubDivisonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        protected void cboSubDivisonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSectionID.Items.Clear();
            cboSectionID.Text = string.Empty;
        }

        #endregion ComboBox Function

        #region ComboBox Function Section
        protected void cboSectionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboSectionID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboSectionID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            DataTable dtb;

            var query = new OrganizationUnitQuery("a");
            var sub = new OrganizationUnitQuery("b");
            query.LeftJoin(sub).On(sub.OrganizationUnitID == query.ParentOrganizationUnitID);
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            query.Where(query.OrganizationUnitName.Like(searchTextContain), query.SROrganizationLevel == "0",
                query.Or(
                    query.ParentOrganizationUnitID == cboSubDivisonID.SelectedValue.ToInt(),
                    query.ParentOrganizationUnitID == cboDivisionID.SelectedValue.ToInt(),
                    query.ParentOrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt()));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboSectionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        #endregion ComboBox Function

        #region ComboBox Function Position

        protected void cboPositionID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PositionQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.PositionID,
                    query.PositionCode,
                    query.PositionName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PositionID.Like(searchTextContain),
                            query.PositionName.Like(searchTextContain)
                        )
                );

            cboPositionID.DataSource = query.LoadDataTable();
            cboPositionID.DataBind();
        }

        protected void cboPositionID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PositionQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.PositionID,
                    query.PositionCode,
                    query.PositionName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PositionID.Like(searchTextContain),
                            query.PositionName.Like(searchTextContain)
                        )
                );

            cboPositionID.DataSource = query.LoadDataTable();
            cboPositionID.DataBind();
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }

        #endregion ComboBox Function


        #endregion ComboBox Function

    }
}
