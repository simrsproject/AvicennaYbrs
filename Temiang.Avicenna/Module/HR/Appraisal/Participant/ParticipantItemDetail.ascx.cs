using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class ParticipantItemDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        private CheckBox chkIsScoringRecapitulation
        {
            get
            { return (CheckBox)Helper.FindControlRecursive(Page, "chkIsScoringRecapitulation"); }
        }

        private RadComboBox cboSREmployeeType
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSREmployeeType"); }
        }

        private RadComboBox cboServiceUnitID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboServiceUnitID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboSREmployeeType.Enabled = false;
            cboServiceUnitID.Enabled = false;

            var cps = new AppStandardReferenceItemCollection();
            cps.Query.Where(cps.Query.StandardReferenceID == AppEnum.StandardReference.EvaluatorType, cps.Query.ReferenceID == "CP");
            cps.LoadAll();

            if (cps.Count > 0)
            {
                var excludeList = new List<string>();

                foreach (var cp in cps)
                {
                    excludeList.Add(cp.ItemID);
                }

                var exclude = excludeList.ToArray();
                StandardReference.InitializeIncludeSpace(cboSREvaluatorType, AppEnum.StandardReference.EvaluatorType, exclude);
            }
            else
                StandardReference.InitializeIncludeSpace(cboSREvaluatorType, AppEnum.StandardReference.EvaluatorType);
            
            chkIsClosed.Enabled = false;

            cboEvaluatorQuestionerID.Enabled = chkIsScoringRecapitulation.Checked;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (AppraisalParticipantItemCollection)Session["collAppraisalParticipantItem" + Request.UserHostName];
                if (coll.Count == 0)
                    ViewState["id"] = "1";
                else
                {
                    var participantItemId = (coll.OrderByDescending(c => c.ParticipantItemID).Select(c => c.ParticipantItemID)).Take(1);
                    int id = Convert.ToInt32(participantItemId.Single()) + 1;

                    ViewState["id"] = id.ToString();
                }
                chkIsClosed.Checked = false;

                grdParticipantEvaluator.Rebind();
                grdQuestionerItem.Rebind();

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["id"] = DataBinder.Eval(DataItem, AppraisalParticipantItemMetadata.ColumnNames.ParticipantItemID).ToInt();

            var emp = DataBinder.Eval(DataItem, AppraisalParticipantItemMetadata.ColumnNames.EmployeeID).ToString();
            var vw = new VwEmployeeTableQuery();
            vw.Where(vw.PersonID == emp.ToInt());
            cboEmployeeID.DataSource = vw.LoadDataTable();
            cboEmployeeID.DataBind();
            cboEmployeeID.SelectedValue = emp;
            cboEmployeeID.Enabled = false;

            chkIsClosed.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, AppraisalParticipantItemMetadata.ColumnNames.IsClosed));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AppraisalParticipantItemCollection)Session["collAppraisalParticipantItem" + Request.UserHostName];
                var isExist = coll.Any(entity => entity.EmployeeID.Equals(cboEmployeeID.SelectedValue));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Employee ID: {0} has exist", cboEmployeeID.SelectedValue);
                }
            }
        }

        public int ParticipantItemID
        {
            get { return ViewState["id"].ToInt(); }
        }

        public int EmployeeID
        {
            get { return string.IsNullOrEmpty(cboEmployeeID.SelectedValue) ? 0 : cboEmployeeID.SelectedValue.ToInt(); }
        }

        public string EmployeeName
        {
            get { return cboEmployeeID.Text; }
        }

        public bool IsClosed
        {
            get { return chkIsClosed.Checked; }
        }

        public string Evaluators
        {
            get
            {
                var str = string.Empty;

                var ds = ParticipantEvaluators.Where(p => p.ParticipantItemID == ViewState["id"].ToInt());

                foreach (var e in ds)
                {
                    str += string.Format("{0} - ({1})<br />", e.EmployeeName, e.ItemName);
                }
                if (!string.IsNullOrEmpty(str)) str = str.Remove(str.Length - 6);
                return str;
            }
        }

        public string Questioners
        {
            get
            {
                var str = string.Empty;

                var ds = ParticipantQuestioners.Where(p => p.ParticipantItemID == ViewState["id"].ToInt());

                foreach (var e in ds)
                {
                    str += string.Format("{0}<br />", e.QuestionerName + " - " + e.QuestionerEvaluatorName);
                }
                if (!string.IsNullOrEmpty(str)) str = str.Remove(str.Length - 6);
                return str;
            }
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboEmployeeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vw = new VwEmployeeTableQuery();
            vw.es.Top = 20;
            vw.Where(vw.EmployeeName.Like(searchTextContain));
            
            if (!string.IsNullOrEmpty(cboSREmployeeType.SelectedValue))
                vw.Where(vw.SREmployeeType == cboSREmployeeType.SelectedValue);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                vw.Where(vw.ServiceUnitID == cboServiceUnitID.SelectedValue);

            if ((o as RadComboBox).ID == cboEvaluatorID.ID && ParticipantEvaluators.Any())
            {
                var ds = ParticipantEvaluators.Where(p => p.ParticipantItemID == ViewState["id"].ToInt());
                if (ds.Any())
                    vw.Where(vw.PersonID.NotIn(ds.Select(p => p.EvaluatorID)));
            }
                
            (o as RadComboBox).DataSource = vw.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void cboEmployeeQuestionerID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vw = new VwEmployeeTableQuery();
            vw.es.Top = 20;
            vw.Where(vw.EmployeeName.Like(searchTextContain));

            var ds = ParticipantEvaluators.Where(p => p.ParticipantItemID == ViewState["id"].ToInt());
            if (ds.Any())
                vw.Where(vw.PersonID.In(ds.Select(p => p.EvaluatorID)));
            
            (o as RadComboBox).DataSource = vw.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        protected void cboQuestionerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["QuestionerNo"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["QuestionerName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["QuestionerID"].ToString();
        }

        protected void cboQuestionerID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var vw = new AppraisalQuestionQuery("a");
            var mtx = new EmployeeAppraisalQuestionQuery("b");
            vw.InnerJoin(mtx).On(mtx.PersonID == cboEmployeeID.SelectedValue.ToInt() && mtx.QuestionerID == vw.QuestionerID);
            vw.es.Top = 20;
            vw.Select(vw.QuestionerID, vw.QuestionerNo, vw.QuestionerName);
            vw.Where(vw.QuestionerName.Like(searchTextContain));
            if (AppSession.Parameter.AppraisalVersionNo == "3")
                vw.Where(vw.IsScoringRecapitulation == chkIsScoringRecapitulation.Checked);
            cboQuestionerID.DataSource = vw.LoadDataTable();
            cboQuestionerID.DataBind();
        }

        protected void btnAddQuestioner_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboQuestionerID.SelectedValue)) return;

            AppraisalParticipantQuestioner entity;
            if (chkIsScoringRecapitulation.Checked)
                entity = ParticipantQuestioners.Where(rec => rec.ParticipantItemID == ViewState["id"].ToInt() && rec.QuestionerID.Equals(cboQuestionerID.SelectedValue.ToInt()) && rec.EvaluatorID.Equals(cboEvaluatorQuestionerID.SelectedValue.ToInt())).SingleOrDefault();
            else
                entity = ParticipantQuestioners.Where(rec => rec.ParticipantItemID == ViewState["id"].ToInt()).SingleOrDefault();

            if (entity != null) return;
            entity = ParticipantQuestioners.AddNew();
            entity.ParticipantItemID = ViewState["id"].ToInt();
            entity.QuestionerID = cboQuestionerID.SelectedValue.ToInt();
            entity.QuestionerName = cboQuestionerID.Text;
            entity.EvaluatorID = !string.IsNullOrEmpty(cboEvaluatorQuestionerID.SelectedValue) ? cboEvaluatorQuestionerID.SelectedValue.ToInt() : -1;
            entity.QuestionerEvaluatorName = cboEvaluatorQuestionerID.Text;

            grdQuestionerItem.Rebind();

            cboQuestionerID.Text = string.Empty;
            cboQuestionerID.SelectedValue = string.Empty;
            cboQuestionerID.DataSource = null;
            cboQuestionerID.DataBind();

            cboEvaluatorQuestionerID.Text = string.Empty;
            cboEvaluatorQuestionerID.SelectedValue = string.Empty;
            cboEvaluatorQuestionerID.DataSource = null;
            cboEvaluatorQuestionerID.DataBind();
        }

        private AppraisalParticipantQuestionerCollection ParticipantQuestioners
        {
            get
            {
                var obj = Session["collAppraisalParticipantQuestioner" + Request.UserHostName];
                var coll = ((AppraisalParticipantQuestionerCollection)(obj));
                coll.Where(c => c.ParticipantItemID == ViewState["id"].ToInt());
                return coll;
            }
        }

        protected void grdQuestionerItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ds = from d in ParticipantQuestioners
                     where d.ParticipantItemID == ViewState["id"].ToInt()
                     select d;
            grdQuestionerItem.DataSource = ds;

            //grdQuestionerItem.DataSource = ParticipantQuestioners;
        }

        protected void grdQuestionerItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var as1 = new AppraisalScoresheetQuery("as1");
            as1.Where(as1.ParticipantItemID == ViewState["id"].ToInt(), as1.IsVoid == false);
            DataTable as1dtb = as1.LoadDataTable();
            if (as1dtb.Rows.Count > 0) return;

            var questionerId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppraisalParticipantQuestionerMetadata.ColumnNames.QuestionerID]);
            var evaluatorId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppraisalParticipantQuestionerMetadata.ColumnNames.EvaluatorID]);
            if (evaluatorId == string.Empty)
                evaluatorId = "-1";

            var entity = ParticipantQuestioners.FirstOrDefault(rec => rec.ParticipantItemID == ViewState["id"].ToInt() && rec.QuestionerID.Equals(questionerId.ToInt()) && rec.EvaluatorID.Equals(evaluatorId.ToInt()));
            if (entity != null)
            {
                entity.MarkAsDeleted();
                ParticipantQuestioners.Save();
            }
        }

        private AppraisalParticipantEvaluatorCollection ParticipantEvaluators
        {
            get
            {
                var obj = Session["collAppraisalParticipantEvaluator" + Request.UserHostName];
                var coll = ((AppraisalParticipantEvaluatorCollection)(obj));
                coll.Where(c => c.ParticipantItemID == ViewState["id"].ToInt());
                return coll;
            }
        }

        protected void grdParticipantEvaluator_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ds = from d in ParticipantEvaluators
                     where d.ParticipantItemID == ViewState["id"].ToInt()
                     select d;
            grdParticipantEvaluator.DataSource = ds;

            //grdParticipantEvaluator.DataSource = ParticipantEvaluators;
        }

        protected void grdParticipantEvaluator_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var as1 = new AppraisalScoresheetQuery("as1");
            as1.Where(as1.ParticipantItemID == ViewState["id"].ToInt(), as1.IsVoid == false);
            DataTable as1dtb = as1.LoadDataTable();
            if (as1dtb.Rows.Count > 0) return;

            var empId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][AppraisalParticipantEvaluatorMetadata.ColumnNames.EvaluatorID]);
            var entity = ParticipantEvaluators.FirstOrDefault(rec => rec.ParticipantItemID == ViewState["id"].ToInt() && rec.EvaluatorID.Equals(empId.ToInt()));
            if (entity != null)
            {
                entity.MarkAsDeleted();
                ParticipantEvaluators.Save();
            }
        }

        protected void btnAddEvaluator_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboEmployeeID.SelectedValue)) return;
            if (string.IsNullOrEmpty(cboEvaluatorID.SelectedValue)) return;

            var entity = ParticipantEvaluators.Where(rec => rec.ParticipantItemID == ViewState["id"].ToInt() && rec.EvaluatorID.Equals(cboEvaluatorID.SelectedValue.ToInt())).SingleOrDefault();
            if (entity != null) return;
            entity = ParticipantEvaluators.AddNew();
            entity.ParticipantItemID = ViewState["id"].ToInt();
            entity.EvaluatorID = cboEvaluatorID.SelectedValue.ToInt();
            entity.EmployeeName = cboEvaluatorID.Text;
            entity.SREvaluatorType = cboSREvaluatorType.SelectedValue;
            entity.ItemName = cboSREvaluatorType.Text;

            var emps = new VwEmployeeTable();
            emps.Query.Where(emps.Query.PersonID == entity.EvaluatorID);
            if (emps.Query.Load())
            {
                entity.PositionID = emps.PositionID;
                entity.PositionValidFromDate = emps.PositionValidFromDate;
                entity.OrganizationUnitID = emps.OrganizationUnitID;
                entity.SubOrganizationUnitID = emps.SubOrganizationUnitID;
                entity.ServiceUnitID = emps.ServiceUnitID;
            }

            grdParticipantEvaluator.Rebind();

            cboEvaluatorID.Text = string.Empty;
            cboEvaluatorID.SelectedValue = string.Empty;
            cboEvaluatorID.DataSource = null;
            cboEvaluatorID.DataBind();

            cboSREvaluatorType.Text = string.Empty;
            cboSREvaluatorType.SelectedValue = string.Empty;
            cboSREvaluatorType.DataSource = null;
            cboSREvaluatorType.DataBind();
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            var collitem = (AppraisalParticipantItemCollection)Session["collAppraisalParticipantItem" + Request.UserHostName];
            if (collitem.Count == 0)
            {
                cboSREmployeeType.Enabled = true;
                cboServiceUnitID.Enabled = true;
            }
        }
    }
}