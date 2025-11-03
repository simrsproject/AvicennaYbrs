using System;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingDiagnosaTemplateDetaill : BasePageDetail
    {
        private NursingDiagnosaTemplateDetailCollection NursingDiagnosaTemplateDetails
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collNursingDiagnosaTemplateDetailsCollection"];
                    if (obj != null)
                        return ((NursingDiagnosaTemplateDetailCollection)(obj));
                }

                var ndtdColl = new NursingDiagnosaTemplateDetailCollection();
                var ndtd = new NursingDiagnosaTemplateDetailQuery("ndtd");
                var ndt = new NursingDiagnosaTemplateQuery("ndt");
                var q = new QuestionQuery("q");

                ndtd.Select(ndtd,
                    q.QuestionText.As("refToQuestion_QuestionText"))
                    .InnerJoin(ndt).On(ndtd.TemplateID == ndt.TemplateID)
                    .InnerJoin(q).On(ndtd.QuestionID == q.QuestionID)
                    .OrderBy(ndtd.RowIndex.Ascending);
                if(string.IsNullOrEmpty(hfTemplateID.Value)){
                    ndtd.Where(ndt.TemplateID.IsNull());
                }else{
                    ndtd.Where(ndt.TemplateID == hfTemplateID.Value);
                }
                ndtdColl.Load(ndtd);

                Session["collNursingDiagnosaTemplateDetailsCollection"] = ndtdColl;
                return ndtdColl;
            }
            set { Session["collNursingDiagnosaTemplateDetailsCollection"] = value; }
        }

        private void SetEntityValue(NursingDiagnosaTemplate entity, NursingDiagnosaTemplateServiceUnitCollection nst)
        {
            if (!string.IsNullOrEmpty(hfTemplateID.Value)) {
                entity.TemplateID = int.Parse(hfTemplateID.Value);
            }
            entity.TemplateName = txtTemplateName.Text;
            entity.TemplateText = txtTemplateText.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded)
            {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            foreach (var xt in NursingDiagnosaTemplateDetails)
            {
                if (xt.es.IsAdded)
                {
                    xt.CreateByUserID = AppSession.UserLogin.UserID;
                    xt.CreateDateTime = DateTime.Now;
                    xt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    xt.LastUpdateDateTime = DateTime.Now;
                }
                else if (xt.es.IsModified)
                {
                    xt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    xt.LastUpdateDateTime = DateTime.Now;
                }
            }

            var sus = gridServiceUnit.MasterTableView.Items.Cast<GridDataItem>()
                .Where(dataItem => ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                .Select(r => r.GetDataKeyValue("ServiceUnitID").ToString());
            // remove yang gak diselect
            foreach (var x in nst.Where(x => !sus.Contains(x.ServiceUnitID))) {
                x.MarkAsDeleted();
            }
            // add yang baru
            foreach (var ss in sus.Where(s => !nst.Select(t => t.ServiceUnitID).Contains(s))) {
                var x = nst.AddNew();
                x.ServiceUnitID = ss;
                x.TemplateID = (hfTemplateID.Value.Equals(string.Empty) ? 0 : int.Parse(hfTemplateID.Value));
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            NursingDiagnosaTemplateQuery que = new NursingDiagnosaTemplateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            var RId = (hfTemplateID.Value.Equals(string.Empty) ? 0 : int.Parse(hfTemplateID.Value));
            if (isNextRecord)
            {
                que.Where(que.TemplateID > RId);
                que.OrderBy(que.TemplateID.Ascending);
            }
            else
            {
                que.Where(que.TemplateID < RId);
                que.OrderBy(que.TemplateID.Descending);
            }
            NursingDiagnosaTemplate entity = new NursingDiagnosaTemplate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            NursingDiagnosaTemplate entity = new NursingDiagnosaTemplate();
            if (parameters.Length > 0)
            {
                String TemplateID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(int.Parse(TemplateID));
            }
            else
            {
                entity.LoadByPrimaryKey((hfTemplateID.Value.Equals(string.Empty) ? 0 : int.Parse(hfTemplateID.Value)));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            NursingDiagnosaTemplate classes = (NursingDiagnosaTemplate)entity;
            hfTemplateID.Value = classes.TemplateID.ToString();
            txtTemplateName.Text = classes.TemplateName;
            txtTemplateText.Text = classes.TemplateText;
            chkIsActive.Checked = classes.IsActive ?? false;

            NursingDiagnosaTemplateDetails = null;
            grdRespondTemplateDetail.Rebind();
            gridServiceUnit.Rebind();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new NursingDiagnosaTemplate());
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
            auditLogFilter.PrimaryKeyData = string.Format("TemplateID={0}", hfTemplateID.Value);
            auditLogFilter.TableName = "NursingDiagnosaTemplate";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGridDiagnosaTemplate(oldVal, newVal);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "NursingDiagnosaTemplateSearch.aspx";
            UrlPageList = "NursingDiagnosaTemplateList.aspx";

            ProgramID = AppConstant.Program.NursingDiagnosaTemplate;
                        
            //StandardReference Initialize
            if (!IsPostBack)
            {
            
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
               
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            NursingDiagnosaTemplate entity = new NursingDiagnosaTemplate();
            entity.LoadByPrimaryKey(int.Parse(hfTemplateID.Value));
            entity.MarkAsDeleted();

            var nst = new NursingDiagnosaTemplateServiceUnitCollection();
            nst.Query.Where(nst.Query.TemplateID == hfTemplateID.Value);
            nst.LoadAll();

            SaveEntity(entity, nst);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            NursingDiagnosaTemplate entity = new NursingDiagnosaTemplate();
            entity.AddNew();

            var nst = new NursingDiagnosaTemplateServiceUnitCollection();
            nst.Query.Where(nst.Query.TemplateID == (hfTemplateID.Value.Equals(string.Empty) ? 0 : int.Parse(hfTemplateID.Value)));
            nst.LoadAll();

            SetEntityValue(entity, nst);
            SaveEntity(entity, nst);
        }

        private void SaveEntity(NursingDiagnosaTemplate entity, NursingDiagnosaTemplateServiceUnitCollection nst)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var isDelete = entity.es.IsDeleted;
                var IsNew = entity.es.IsAdded;
                entity.Save();

                foreach (var xt in NursingDiagnosaTemplateDetails)
                {
                    if (xt.es.IsAdded)
                    {
                        xt.TemplateID = entity.TemplateID;
                    }
                }
                NursingDiagnosaTemplateDetails.Save();
                if (IsNew) {
                    foreach (var n in nst) {
                        n.TemplateID = entity.TemplateID;
                    }
                }
                if (isDelete) {
                    nst.MarkAllAsDeleted();
                }
                nst.Save();

                if (!isDelete)
                    hfTemplateID.Value = entity.TemplateID.ToString();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
            NursingDiagnosaTemplateDetails = null;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            NursingDiagnosaTemplate entity = new NursingDiagnosaTemplate();
            if (entity.LoadByPrimaryKey(int.Parse(hfTemplateID.Value)))
            {
                var nst = new NursingDiagnosaTemplateServiceUnitCollection();
                nst.Query.Where(nst.Query.TemplateID == hfTemplateID.Value);
                nst.LoadAll();

                SetEntityValue(entity, nst);
                SaveEntity(entity, nst);
            }
        }
        #endregion

        private void SetEntityValueNursingDiagnosaTemplateDetail(NursingDiagnosaTemplateDetail entity, GridCommandEventArgs e)
        {
            NursingDiagnosaTemplateDetailDetail userControl =
                (NursingDiagnosaTemplateDetailDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.QuestionID = userControl.QuestionID;
                entity.QuestionText = userControl.QuestionText;
                entity.RowIndex = userControl.RowIndex;
            }
        }

        protected void grdRespondTemplateDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRespondTemplateDetail.DataSource = NursingDiagnosaTemplateDetails;
        }
        protected void grdRespondTemplateDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            NursingDiagnosaTemplateDetail entity = NursingDiagnosaTemplateDetails.AddNew();
            SetEntityValueNursingDiagnosaTemplateDetail(entity, e);
        }
        protected void grdRespondTemplateDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String QuestionID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][NursingDiagnosaTemplateDetailMetadata.ColumnNames.QuestionID]);
            NursingDiagnosaTemplateDetail entity = NursingDiagnosaTemplateDetails.Where(x => x.QuestionID == QuestionID).FirstOrDefault();
            if (entity != null)
            {
                SetEntityValueNursingDiagnosaTemplateDetail(entity, e);
            }
        }
        protected void grdRespondTemplateDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;

            string QuestionID = (item.OwnerTableView.DataKeyValues[item.ItemIndex][NursingDiagnosaTemplateDetailMetadata.ColumnNames.QuestionID]).ToString();

            var entity = NursingDiagnosaTemplateDetails.Where(
                x => x.QuestionID == QuestionID).FirstOrDefault();

            if (entity != null)
                entity.MarkAsDeleted();
        }

        private void RefreshCommandItemGridDiagnosaTemplate(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRespondTemplateDetail.Columns[0].Visible = isVisible;
            grdRespondTemplateDetail.Columns[grdRespondTemplateDetail.Columns.Count - 1].Visible = isVisible;

            grdRespondTemplateDetail.MasterTableView.CommandItemDisplay = isVisible
                                                                       ? GridCommandItemDisplay.Top
                                                                       : GridCommandItemDisplay.None;
            //Reset Detail
            if (newVal != AppEnum.DataMode.Read)
            {
                NursingDiagnosaTemplateDetails = null;
                if (newVal == AppEnum.DataMode.New) {
                    hfTemplateID.Value = string.Empty;
                }
            }

            //Perbaharui tampilan dan data
            grdRespondTemplateDetail.Rebind();
            gridServiceUnit.Rebind();
        }

        protected void gridServiceUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var su = new ServiceUnitQuery("su");
            var ndtsu = new NursingDiagnosaTemplateServiceUnitQuery("ndtsu");
            
            if (DataModeCurrent == AppEnum.DataMode.Read) {
                su.InnerJoin(ndtsu).On(ndtsu.ServiceUnitID == su.ServiceUnitID
                    & ndtsu.TemplateID == (hfTemplateID.Value.Equals(string.Empty) ? 0 : int.Parse(hfTemplateID.Value)));
            }else{
                su.LeftJoin(ndtsu).On(ndtsu.ServiceUnitID == su.ServiceUnitID &
                    ndtsu.TemplateID == (hfTemplateID.Value.Equals(string.Empty) ? 0 : int.Parse(hfTemplateID.Value)));
            }
            su.Select(su.ServiceUnitID, su.ServiceUnitName,
                "<cast(case ISNULL(ndtsu.ServiceUnitID,'') when '' then 0 else 1 end as bit) IsSelected>")
                .Where(su.SRRegistrationType != string.Empty);
            var dtb = su.LoadDataTable();

            gridServiceUnit.DataSource = dtb;
        }

        protected void gridServiceUnit_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                var chkd = item.FindControl("detailChkbox") as CheckBox;
                if (chkd != null)
                {
                    chkd.Checked = ((DataRow)((DataRowView)e.Item.DataItem).Row).Field<bool>("IsSelected");
                }
            }
        }


        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in gridServiceUnit.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}