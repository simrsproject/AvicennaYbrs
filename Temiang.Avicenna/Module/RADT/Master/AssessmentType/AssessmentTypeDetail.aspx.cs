//TODO: Add setting Account
using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class AssessmentTypeDetail : BasePageDetail
    {
        private string _standardReferenceID = "AssessmentType";
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "AssessmentTypeSearch.aspx";
            UrlPageList = "AssessmentTypeList.aspx";

            ProgramID = AppConstant.Program.AsessmentType;

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
            PopulateAssessmentTypeBodyDiagramGrid();
        }
        protected override void OnMenuNewClick()
        {

        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            AppStandardReferenceItem entity = new AppStandardReferenceItem();
            if (entity.LoadByPrimaryKey(_standardReferenceID, txtItemID.Text))
            {
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
            auditLogFilter.PrimaryKeyData = string.Format("StandardReferenceID='AssessmentType' AND ItemID='{0}'", txtItemID.Text.Trim());
            auditLogFilter.TableName = "AppStandardReferenceItem";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(Temiang.Avicenna.Common.AppEnum.DataMode oldVal, Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            RefreshCommandItemAssessmentTypeBodyDiagram(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AppStandardReferenceItem entity = new AppStandardReferenceItem();
            if (parameters.Length > 0)
            {
                String id = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(_standardReferenceID, id);
            }
            else
            {
                entity.LoadByPrimaryKey(_standardReferenceID, txtItemID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            AppStandardReferenceItem item = (AppStandardReferenceItem)entity;
            txtItemID.Text = item.ItemID;
            txtItemName.Text = item.ItemName;
            chkIsActive.Checked = item.IsActive ?? false;
        }

        #endregion

        #region Private Method Standard

        private void SaveEntity(AppStandardReferenceItem entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //AssessmentTypeBodyDiagram
                var coll = new AssessmentTypeBodyDiagramCollection();
                coll.Query.Where(coll.Query.SRAssessmentType == entity.ItemID);
                coll.LoadAll();
                foreach (GridDataItem dataItem in grdBodyDiagram.MasterTableView.Items)
                {
                    string bodyID = dataItem.GetDataKeyValue("BodyID").ToString();
                    bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;

                    bool isExist = false;
                    foreach (AssessmentTypeBodyDiagram row in coll)
                    {
                        if (row.BodyID.Equals(bodyID))
                        {
                            isExist = true;
                            if (!isSelect)
                                row.MarkAsDeleted();
                            break;
                        }
                    }
                    //Add
                    if (!isExist && isSelect)
                    {
                        AssessmentTypeBodyDiagram row = coll.AddNew();
                        row.SRAssessmentType = entity.ItemID;
                        row.BodyID = bodyID;
                        row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        row.LastUpdateDateTime = DateTime.Now;
                    }
                }
                coll.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            AppStandardReferenceItemQuery que = new AppStandardReferenceItemQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.StandardReferenceID == _standardReferenceID, que.ItemID > txtItemID.Text);
                que.OrderBy(que.ItemID.Ascending);
            }
            else
            {
                que.Where(que.StandardReferenceID == _standardReferenceID, que.ItemID < txtItemID.Text);
                que.OrderBy(que.ItemID.Descending);
            }
            var entity = new AppStandardReferenceItem();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region Record Detail Method Function BodyDiagram
        protected void grdBodyDiagram_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdBodyDiagram.DataSource = GetAssessmentTypeBodyDiagrams();
        }


        private DataTable GetAssessmentTypeBodyDiagrams()
        {
            AssessmentTypeBodyDiagramQuery query = new AssessmentTypeBodyDiagramQuery("a");
            BodyDiagramQuery qrRef = new BodyDiagramQuery("b");
            if (this.DataModeCurrent == Temiang.Avicenna.Common.AppEnum.DataMode.Read)
            {
                query.InnerJoin(qrRef).On(query.BodyID == qrRef.BodyID);
                query.Where(query.SRAssessmentType == txtItemID.Text);
            }
            else
            {
                query.RightJoin(qrRef).On(query.BodyID == qrRef.BodyID & query.SRAssessmentType == txtItemID.Text);
            }
            query.OrderBy(qrRef.BodyName.Ascending);
            query.Select
                (
                    "<CONVERT(Bit,CASE WHEN COALESCE(a.BodyID,'')='' THEN 0 ELSE 1 END) as IsSelect>",
                    qrRef.BodyID,
                    qrRef.BodyName,
                    qrRef.Description,
                    qrRef.BodyImage
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
        private void PopulateAssessmentTypeBodyDiagramGrid()
        {
            //Display Data Detail
            grdBodyDiagram.DataSource = GetAssessmentTypeBodyDiagrams();
            grdBodyDiagram.DataBind();
        }
        private void RefreshCommandItemAssessmentTypeBodyDiagram(Temiang.Avicenna.Common.AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != Temiang.Avicenna.Common.AppEnum.DataMode.Read);
            grdBodyDiagram.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdBodyDiagram.Rebind();
        }
        #endregion

    }
}