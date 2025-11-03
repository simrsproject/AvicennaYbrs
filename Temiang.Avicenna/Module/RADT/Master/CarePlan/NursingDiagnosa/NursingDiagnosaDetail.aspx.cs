using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingDiagnosaDetail : BasePageDetail
    {
        private void GetNewId() {
            txtNursingDiagnosaID.Text = NursingDiagnosa.GetNewID(getPageID, cboDiagType.SelectedValue);
        }
        private void GetNewSequenceNo()
        {
            if(!string.IsNullOrEmpty( txtNursingDiagnosaID.Text))
                txtSequenceNo.Text = NursingDiagnosa.GetNewSequenceNo(getPageID, cboDiagType.SelectedValue);
        }

        private DataTable NursingDiagnosaNsTypes
        {
            get
            {
                var tp = new AppStandardReferenceItemQuery("tp");
                var nds = new NursingDiagnosaNsTypeQuery("nds");

                if (DataModeCurrent == AppEnum.DataMode.Read)
                {
                    tp.InnerJoin(nds).On(tp.ItemID == nds.SRNsType &&
                    nds.NursingDiagnosaID == txtNursingDiagnosaID.Text);
                }
                else
                {
                    tp.LeftJoin(nds).On(tp.ItemID == nds.SRNsType &&
                    nds.NursingDiagnosaID == txtNursingDiagnosaID.Text);
                }
                tp.Select(tp.ItemID.As("SRNsTypeID"), tp.ItemName.As("SRNsTypeName"),
                    "<cast(case ISNULL(nds.SRNsType,'') when '' then 0 else 1 end as bit) IsSelected>")
                    .Where(tp.StandardReferenceID == AppEnum.StandardReference.NsType);

                var dtb = tp.LoadDataTable();

                return dtb;
            }
        }

        private void SetEntityValue(NursingDiagnosa entity, NursingDiagnosaNsTypeCollection ndt,
            NursingDiagnosaServiceUnitCollection nds)
        {
            entity.NursingDiagnosaID = txtNursingDiagnosaID.Text;
            entity.SequenceNo = txtSequenceNo.Text;
            entity.NursingDiagnosaCode = txtDiagCode.Text;
            entity.NursingDiagnosaName = txtNursingDiagnosaName.Text;
            entity.SRNursingDiagnosaLevel = getPageID;
            entity.NursingDiagnosaParentID = cboNursingParent.SelectedValue;
            entity.SRNsDiagnosaType = cboDiagType.SelectedValue;
            entity.SRNursingNocType = cboSRNursingNocType.SelectedValue;
            entity.SRNursingNicType = cboSRNursingNicType.SelectedValue;
            entity.RespondTemplate = txtRespondTemplate.Text;
            entity.IsActive = chkIsActive.Checked;
            int ? ii = null;
            entity.TemplateID = string.IsNullOrEmpty(cboNursingDiagnosaTemplateID.SelectedValue) ?
                ii : System.Convert.ToInt32(cboNursingDiagnosaTemplateID.SelectedValue);

            if (getPageID == "10")
            {
                entity.F1 = txtDefinition.Text;
            }
            if (getPageID == "11")
            {
                entity.SRNsEtiologyType = cboEtiologyType.SelectedValue;
            }
            if (getPageID == "20") {
                entity.F1 = txtDefinition.Text;
            }
            if (getPageID == "30")
            {
                entity.F1 = txtF1.Text;
                entity.F2 = txtF2.Text;
            }

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

            var typeids = grdNsType.MasterTableView.Items.Cast<GridDataItem>()
                .Where(dataItem => ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                .Select(r => r.GetDataKeyValue("SRNsTypeID").ToString());
            // remove yang gak diselect
            foreach (var x in ndt.Where(x => !typeids.Contains(x.SRNsType)))
            {
                x.MarkAsDeleted();
            }
            // add yang baru
            foreach (var ss in typeids.Where(s => !ndt.Select(t => t.SRNsType).Contains(s)))
            {
                var x = ndt.AddNew();
                x.SRNsType = ss;
                x.NursingDiagnosaID = txtNursingDiagnosaID.Text;
            }

            var sus = gridServiceUnit.MasterTableView.Items.Cast<GridDataItem>()
                .Where(dataItem => ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                .Select(r => r.GetDataKeyValue("ServiceUnitID").ToString());
            // remove yang gak diselect
            foreach (var x in nds.Where(x => !sus.Contains(x.ServiceUnitID)))
            {
                x.MarkAsDeleted();
            }
            // add yang baru
            foreach (var ss in sus.Where(s => !nds.Select(t => t.ServiceUnitID).Contains(s)))
            {
                var x = nds.AddNew();
                x.ServiceUnitID = ss;
                x.NursingDiagnosaID = txtNursingDiagnosaID.Text;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            NursingDiagnosaQuery que = new NursingDiagnosaQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.NursingDiagnosaID > txtNursingDiagnosaID.Text);
                que.OrderBy(que.NursingDiagnosaID.Ascending);
            }
            else
            {
                que.Where(que.NursingDiagnosaID < txtNursingDiagnosaID.Text);
                que.OrderBy(que.NursingDiagnosaID.Descending);
            }
            NursingDiagnosa entity = new NursingDiagnosa();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            NursingDiagnosa entity = new NursingDiagnosa();
            if (parameters.Length > 0)
            {
                String NursingDiagnosaID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(NursingDiagnosaID);
            }
            else
            {
                entity.LoadByPrimaryKey(txtNursingDiagnosaID.Text);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            NursingDiagnosa classes = (NursingDiagnosa)entity;
            txtNursingDiagnosaID.Text = classes.NursingDiagnosaID;
            txtSequenceNo.Text = classes.SequenceNo;
            txtDiagCode.Text = classes.NursingDiagnosaCode;
            txtNursingDiagnosaName.Text = classes.NursingDiagnosaName;
            cboSRNursingNocType.SelectedValue = classes.SRNursingNocType;
            cboSRNursingNicType.SelectedValue = classes.SRNursingNicType;
            txtRespondTemplate.Text = classes.RespondTemplate;
            chkIsActive.Checked = classes.IsActive ?? false;
            cboDiagType.SelectedValue = classes.SRNsDiagnosaType;
            cboEtiologyType.SelectedValue = classes.SRNsEtiologyType;

            if (getPageID == "10") {
                txtDefinition.Text = classes.F1;
            }
            if (getPageID == "20")
            {
                txtDefinition.Text = classes.F1;
            }
            if (getPageID == "30") {
                txtF1.Text = classes.F1;
                txtF2.Text = classes.F2;
            }

            var qDiag = new NursingDiagnosaQuery();
            qDiag.Where(qDiag.NursingDiagnosaID == (classes.NursingDiagnosaParentID ?? string.Empty));
            cboNursingParent.Text = string.Empty;
            cboNursingParent.DataSource = qDiag.LoadDataTable();
            cboNursingParent.DataBind();
            cboNursingParent.SelectedValue = classes.str.NursingDiagnosaParentID;

            cboNursingDiagnosaTemplateID.SelectedValue = string.Empty;
            if (classes.TemplateID.HasValue) { 
                var t = new NursingDiagnosaTemplate();
                if(t.LoadByPrimaryKey(classes.TemplateID.Value)){
                    cboNursingDiagnosaTemplateID_ItemsRequested(cboNursingDiagnosaTemplateID,
                        new RadComboBoxItemsRequestedEventArgs() { Text = t.TemplateName});
                    cboNursingDiagnosaTemplateID.SelectedValue = classes.TemplateID.Value.ToString();
                }
            }

            if (classes.es.IsAdded) {
                GetNewId();
                GetNewSequenceNo();
            }

            grdNsType.Rebind();
            gridServiceUnit.Rebind();

            DisableControl();
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new NursingDiagnosa());
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
            auditLogFilter.PrimaryKeyData = string.Format("NursingDiagnosaID='{0}'", txtNursingDiagnosaID.Text.Trim());
            auditLogFilter.TableName = "NursingDiagnosa";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtNursingDiagnosaID.Enabled = (newVal == AppEnum.DataMode.New);
            cboDiagType.Enabled = txtNursingDiagnosaID.Enabled;

            RefreshCommandItemGridNsType(oldVal, newVal);
        }

        private void RefreshCommandItemGridNsType(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            //grdNsType.Columns[0].Visible = isVisible;
            //grdNsType.Columns[grdNsType.Columns.Count - 1].Visible = isVisible;

            //grdNsType.MasterTableView.CommandItemDisplay = isVisible
            //                                                           ? GridCommandItemDisplay.Top
            //                                                           : GridCommandItemDisplay.None;
            //Reset Detail
            if (newVal != AppEnum.DataMode.Read)
            {
                if (newVal == AppEnum.DataMode.New)
                {
                    GetNewId();
                    GetNewSequenceNo();
                }
            }

            //Perbaharui tampilan dan data
            grdNsType.DataSource = null;
            grdNsType.Rebind();
            gridServiceUnit.DataSource = null;
            gridServiceUnit.Rebind();
        }

        protected void gridServiceUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var su = new ServiceUnitQuery("su");
            var ndsu = new NursingDiagnosaServiceUnitQuery("ndsu");

            if (DataModeCurrent == AppEnum.DataMode.Read)
            {
                su.InnerJoin(ndsu).On(ndsu.ServiceUnitID == su.ServiceUnitID
                    & ndsu.NursingDiagnosaID == txtNursingDiagnosaID.Text);
            }
            else
            {
                su.LeftJoin(ndsu).On(ndsu.ServiceUnitID == su.ServiceUnitID &
                    ndsu.NursingDiagnosaID == txtNursingDiagnosaID.Text);
            }
            su.Select(su.ServiceUnitID, su.ServiceUnitName,
                "<cast(case ISNULL(ndsu.ServiceUnitID,'') when '' then 0 else 1 end as bit) IsSelected>")
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

        private string getPageID
        {
            get
            {
                return Request.QueryString["ndl"];
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "NursingDiagnosaSearch.aspx";
            UrlPageList = "NursingDiagnosaList.aspx?" + "ndl=" + Request.QueryString["ndl"];

            switch (getPageID)
            {
                case "00":
                    {
                        ProgramID = AppConstant.Program.NursingDomain;
                        break;
                    }
                case "10":
                    {
                        ProgramID = AppConstant.Program.NursingDiag;
                        break;
                    }
                case "11":
                    {
                        ProgramID = AppConstant.Program.NursingProblem;
                        break;
                    }
                case "20":
                    {
                        ProgramID = AppConstant.Program.NursingNOC;
                        break;
                    }
                case "21":
                    {
                        ProgramID = AppConstant.Program.NursingNOCObjcetive;
                        break;
                    }
                case "30":
                    {
                        ProgramID = AppConstant.Program.NursingNIC;
                        break;
                    }
                case "31":
                    {
                        ProgramID = AppConstant.Program.NursingNICImplementation;
                        break;
                    }
                case "32":
                    {
                        ProgramID = AppConstant.Program.NursingNICImplementationCustom;
                        break;
                    }
                default:
                    {
                        ProgramID = AppConstant.Program.NursingDiagnosa;
                        break;
                    }
            }

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboDiagType, AppEnum.StandardReference.NsDiagnosaType);
                StandardReference.InitializeIncludeSpace(cboEtiologyType, AppEnum.StandardReference.NsEtiologyType);
                StandardReference.InitializeIncludeSpace(cboSRNursingNocType, AppEnum.StandardReference.NursingNocType);
                StandardReference.InitializeIncludeSpace(cboSRNursingNicType, AppEnum.StandardReference.NursingNicType);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey("NursingDiagnosaLevel", getPageID))
                {
                    lblNursingDiagnosaID.Text = std.ItemName + " ID";
                    lblNursingDiagnosaName.Text = std.ItemName + " Name";
                }

                DisableControl();
            }

            if (getPageID == "32")
            {
                rfvNursingParent.Enabled = false;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            NursingDiagnosa entity = new NursingDiagnosa();
            entity.LoadByPrimaryKey(txtNursingDiagnosaID.Text);
            entity.MarkAsDeleted();

            var ndt = new NursingDiagnosaNsTypeCollection();
            ndt.Query.Where(ndt.Query.NursingDiagnosaID == txtNursingDiagnosaID.Text);
            ndt.LoadAll();

            var nds = new NursingDiagnosaServiceUnitCollection();
            nds.Query.Where(nds.Query.NursingDiagnosaID == txtNursingDiagnosaID.Text);
            nds.LoadAll();

            SaveEntity(entity, ndt, nds);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            NursingDiagnosa entity = new NursingDiagnosa();
            entity.AddNew();
            GetNewId();
            GetNewSequenceNo();

            var ndt = new NursingDiagnosaNsTypeCollection();
            ndt.Query.Where(ndt.Query.NursingDiagnosaID == txtNursingDiagnosaID.Text);
            ndt.LoadAll();

            var nds = new NursingDiagnosaServiceUnitCollection();
            nds.Query.Where(nds.Query.NursingDiagnosaID == txtNursingDiagnosaID.Text);
            nds.LoadAll();

            SetEntityValue(entity, ndt, nds);
            SaveEntity(entity, ndt, nds);
        }

        private void SaveEntity(NursingDiagnosa entity, NursingDiagnosaNsTypeCollection ndt,
            NursingDiagnosaServiceUnitCollection nds)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var isDelete = entity.es.IsDeleted;
                var IsNew = entity.es.IsAdded;

                entity.Save();

                if (IsNew)
                {
                    foreach (var n in ndt)
                    {
                        n.NursingDiagnosaID = entity.NursingDiagnosaID;
                    }
                }
                if (isDelete)
                {
                    ndt.MarkAllAsDeleted();
                }
                ndt.Save();

                if (IsNew)
                {
                    foreach (var n in nds)
                    {
                        n.NursingDiagnosaID = entity.NursingDiagnosaID;
                    }
                }
                if (isDelete)
                {
                    nds.MarkAllAsDeleted();
                }
                nds.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            NursingDiagnosa entity = new NursingDiagnosa();
            if (entity.LoadByPrimaryKey(txtNursingDiagnosaID.Text))
            {
                var ndt = new NursingDiagnosaNsTypeCollection();
                ndt.Query.Where(ndt.Query.NursingDiagnosaID == txtNursingDiagnosaID.Text);
                ndt.LoadAll();

                var nds = new NursingDiagnosaServiceUnitCollection();
                nds.Query.Where(nds.Query.NursingDiagnosaID == txtNursingDiagnosaID.Text);
                nds.LoadAll();

                SetEntityValue(entity, ndt, nds);
                SaveEntity(entity, ndt, nds);
            }
        }

        #endregion

        protected void cboNursingParent_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            // cari level parentnya berdasarkan level yang dipilih
            if (getPageID == string.Empty) {
                cboNursingParent.DataSource = null;
                cboNursingParent.DataBind();
                return;
            }

            string lvlToDisplay = string.Empty;
            switch (getPageID) {
                case "-1": { lvlToDisplay = "00"; break; }
                case "00": { lvlToDisplay = "-"; break; }
                case "10": { lvlToDisplay = "00"; break; }
                case "11": { lvlToDisplay = "10"; break; }
                case "20": { lvlToDisplay = "10"; break; }
                case "21": { lvlToDisplay = "20"; break; }
                case "30": { lvlToDisplay = "10"; break; }
                case "31": { lvlToDisplay = "30"; break; }
                case "32": { lvlToDisplay = "31"; break; }
            }

            string searchTextContain = string.Format("%{0}%", e.Text);

            var obj = new NursingDiagnosaQuery("a");

            obj.es.Top = 20;
            obj.Where(
                obj.Or(
                    obj.NursingDiagnosaID.Like(searchTextContain),
                    obj.NursingDiagnosaName.Like(searchTextContain)
                ),
                obj.IsActive == true,
                obj.SRNursingDiagnosaLevel == lvlToDisplay
            );
            cboNursingParent.DataSource = obj.LoadDataTable();
            cboNursingParent.DataBind();
        }

        protected void cboNursingParent_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["NursingDiagnosaID"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["NursingDiagnosaName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["NursingDiagnosaID"].ToString();
        }

        protected void cboNursingParent_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (DataModeCurrent == AppEnum.DataMode.New) {
                //    GetNewId();
                if (!string.IsNullOrEmpty(cboNursingParent.SelectedValue)) {
                    var pDiag = new NursingDiagnosa();
                    if (pDiag.LoadByPrimaryKey(cboNursingParent.SelectedValue))
                    {
                        cboDiagType.SelectedValue = pDiag.SRNsDiagnosaType;
                        cboDiagType_SelectedIndexChanged(cboDiagType, new RadComboBoxSelectedIndexChangedEventArgs(cboDiagType.Text, "", cboDiagType.SelectedValue, ""));
                    }
                }
            }
        }

        protected void cboNursingDiagnosaTemplateID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var obj = new NursingDiagnosaTemplateQuery("a");

            obj.es.Top = 20;
            obj.Where(
                obj.TemplateName.Like(searchTextContain),
                obj.IsActive == true
            );
            cboNursingDiagnosaTemplateID.DataSource = obj.LoadDataTable();
            cboNursingDiagnosaTemplateID.DataBind();
        }

        protected void cboNursingDiagnosaTemplateID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TemplateID"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["TemplateName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TemplateID"].ToString();
        }

        protected void cboSRNursingDiagnosaLevel_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e) {
            DisableControl();
            GetNewSequenceNo();
            //if (DataModeCurrent == DataMode.New)
            //{
            //    if(e.Value == "00")
            //        GetNewId();
            //}
        }

        private void DisableControl()
        {
            //trNSDiag.Visible = false;
            rfDiagType.Enabled = false;

            trDefinition.Visible = false;

            trNsEtiology.Visible = false;
            rfEtiologyType.Enabled = false;

            trNocType.Visible = false;
            trNicType.Visible = false;

            trF1.Visible = false;
            trF2.Visible = false;

            if (getPageID == "10") {
                //trNSDiag.Visible = true;
                rfDiagType.Enabled = true;
                trDefinition.Visible = true;
            }
            if (getPageID == "11")
            {
                trNsEtiology.Visible = true;
                rfEtiologyType.Enabled = true;
            }

            if (getPageID != "00")
            {
                cboNursingParent.Enabled = true;
                rfvNursingParent.Enabled = true;
            }
            else
            {
                cboNursingParent.Enabled = false;
                rfvNursingParent.Enabled = false;
            }

            if(getPageID == "20"){
                trNocType.Visible = true;
                trDefinition.Visible = true;
            }

            if (getPageID == "30")
            {
                trNicType.Visible = true;
                trF1.Visible = true;
                trF2.Visible = true;
            }
            else
            {
                //cboSRNursingNicType.Enabled = false;
                //rfvRNursingNicType.Enabled = false;
            }

            if (getPageID == "31")
            {
                trRespondTemplate.Visible = true;
                trRespondTemplateID.Visible = true;
            }
            else
            {
                trRespondTemplate.Visible = false;
                trRespondTemplateID.Visible = false;
            }

            if (getPageID == "32")
            {
                trRespondTemplate.Visible = true;
                trRespondTemplateID.Visible = true;
            }
            else
            {
                trRespondTemplate.Visible = false;
                trRespondTemplateID.Visible = false;
            }

            // disable enable panel service unit
            if (getPageID == "11" || getPageID == "21" || getPageID == "30" || getPageID == "32")
            {
                pnlNsType.Visible = true;
            }
            else {
                pnlNsType.Visible = false;
            }
        }

        #region grid events
        protected void grdNsType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdNsType.DataSource = NursingDiagnosaNsTypes;
        }

        protected void grdNsType_ItemDataBound(object sender, GridItemEventArgs e)
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
            foreach (CheckBox chkBox in grdNsType.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }
        #endregion

        protected void cboDiagType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (DataModeCurrent == AppEnum.DataMode.New) {
                GetNewId();
                GetNewSequenceNo();
            }
        }
    }
}