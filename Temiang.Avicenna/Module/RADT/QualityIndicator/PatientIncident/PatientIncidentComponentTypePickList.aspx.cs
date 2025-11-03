using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Globalization;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIncidentComponentTypePickList : BasePageDialog
    {
        protected string PatientIncidentNo
        {
            get
            {
                return Request.QueryString["piNo"];
            }
        }

        private void copy(PatientIncidentComponentTypeCollection source, PatientIncidentComponentTypeCollection dest)
        {
            foreach (var d in dest)
            {
                var s = source.Where(x => x.SRIncidentType == d.SRIncidentType && x.ComponentID == d.ComponentID && x.SubComponentID == d.SubComponentID);
                if (!s.Any())
                {
                    d.MarkAsDeleted();
                }
            }

            foreach (var s in source)
            {
                var entity = (dest.Where(i => i.SRIncidentType == s.SRIncidentType && i.ComponentID == s.ComponentID && i.SubComponentID == s.SubComponentID)).SingleOrDefault();

                if (entity == null)
                {
                    entity = dest.AddNew();
                    entity.PatientIncidentNo = string.Empty;
                }

                entity.SRIncidentType = s.SRIncidentType;
                entity.IncidentType = s.IncidentType;
                entity.ComponentID = s.ComponentID;
                entity.ComponentName = s.ComponentName;
                entity.SubComponentID = s.SubComponentID;
                entity.SubComponent = s.SubComponent;
                entity.SubComponentName = s.SubComponentName;
                entity.Modus = s.Modus;
                entity.IsAllowEdit = s.IsAllowEdit;
            }
        }

        private PatientIncidentComponentTypeCollection PatientIncidentComponentTypes
        {
            get
            {
                object obj = Session["collPatientIncidentComponentType"];
                if (obj == null)
                {
                    Session["collPatientIncidentComponentType"] = new PatientIncidentComponentTypeCollection();
                }
                return (PatientIncidentComponentTypeCollection)obj;
            }
            set { Session["collPatientIncidentComponentType"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PatientIncident;

            if (!IsPostBack)
            {
                //var grr = new Guarantor();
                //grr.LoadByPrimaryKey(Request.QueryString["grr"]);
                //Title = "Invoice List : " + grr.GuarantorName;

                var coll = (PatientIncidentComponentTypeCollection)Session["collPatientIncidentComponentType" + Request.UserHostName];
                PatientIncidentComponentTypes = new PatientIncidentComponentTypeCollection();
                var iq = new PatientIncidentComponentTypeQuery("a");
                var qdt = new IncidentTypeItemQuery("b");
                var qhd = new IncidentTypeQuery("c");
                var qstd = new AppStandardReferenceItemQuery("d");
                iq.LeftJoin(qdt).On(qdt.SRIncidentType == iq.SRIncidentType && qdt.ComponentID == iq.ComponentID && qdt.SubComponentID == iq.SubComponentID);
                iq.LeftJoin(qhd).On(qhd.ComponentID == qdt.ComponentID);
                iq.LeftJoin(qstd).On(qstd.StandardReferenceID == AppEnum.StandardReference.IncidentType && qstd.ItemID == iq.SRIncidentType);
                iq.Where(iq.PatientIncidentNo.IsNull())
                    .Select(@"<'' AS PatientIncidentNo>", qdt.SRIncidentType, qstd.ItemName.As("refToAppStandardReferenceItem_ItemName"),
                    qdt.ComponentID, qhd.ComponentName.As("refToIncidentType_ComponentName"),
                    qdt.SubComponentID, qdt.SubComponentName.As("refToIncidentTypeItem_SubComponentName"),
                    @"<'' AS SubComponentName>", @"<'' AS Modus>", qdt.IsAllowEdit.As("refToIncidentTypeItem_IsAllowEdit"));

                PatientIncidentComponentTypes.Load(iq);

                copy(coll, PatientIncidentComponentTypes);
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");

            query.Select(query.ItemID, query.ItemName);
            query.Where(
                    query.StandardReferenceID == AppEnum.StandardReference.IncidentType,
                    query.IsActive == true
                );

            var coll = new AppStandardReferenceItemCollection();
            coll.Load(query);

            grdList.DataSource = coll;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var coll = (PatientIncidentComponentTypeCollection)Session["collPatientIncidentComponentType" + Request.UserHostName];

                var item = ((GridDataItem)e.Item);

                var ii = (AppStandardReferenceItem)item.DataItem;

                CheckBox chk = item.FindControl("itemChkbox") as CheckBox;

                if (coll.Where(x => x.SRIncidentType == ii.ItemID).Any())
                {
                    chk.Checked = true;
                }
            }
        }

        protected void grdList_DataBound(object sender, EventArgs e)
        {
            PopulateListDetailGrid();
        }

        protected void grdListDetail_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdListDetail_ItemPreRender;
        }

        private void grdListDetail_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)source).ID)
            {
                case "grdListDetail":
                    PopulateListDetailGrid();
                    break;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            var selected = false;

            foreach (GridDataItem dataItem in grdListDetail.MasterTableView.Items)
            {
                selected = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                if (selected)
                    break;
            }

            if (selected)
            {
                foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                {
                    selected = ((CheckBox)dataItem.FindControl("itemChkbox")).Checked;
                    if (selected)
                        return string.Format("oWnd.argument.id = '{0}'", dataItem["ItemID"].Text);
                }
            }
            return string.Empty;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (PatientIncidentComponentTypeCollection)Session["collPatientIncidentComponentType" + Request.UserHostName];

            copy(PatientIncidentComponentTypes, coll);

            UpdateDataSourceDetail();

            return true;
        }

        private void UpdateDataSourceDetail()
        {
            var coll = (PatientIncidentComponentTypeCollection)Session["collPatientIncidentComponentType" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdListDetail.MasterTableView.Items)
            {
                bool isChecked = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                string srIncidentType = dataItem.GetDataKeyValue("SRIncidentType").ToString();
                string componentId = dataItem.GetDataKeyValue("ComponentID").ToString();
                string subComponentId = dataItem.GetDataKeyValue("SubComponentID").ToString();
                string subComponentName = ((RadTextBox)dataItem.FindControl("txtSubComponentName")).Text ?? string.Empty;
                string modus = ((RadTextBox)dataItem.FindControl("txtModus")).Text ?? string.Empty;

                if (isChecked)
                {
                    foreach (var c in coll)
                    {
                        if (c.SRIncidentType == srIncidentType && c.ComponentID == componentId && c.SubComponentID == subComponentId)
                        {
                            c.SubComponentName = subComponentName;
                            c.Modus = modus;

                            break;
                        }
                    }
                }
            }
        }

        protected void ToggleDiscSelectedState(object sender, EventArgs e)
        {
            var txt = (CheckBox)sender;
            var gdi = (GridDataItem)txt.NamingContainer;
            var chk = gdi.FindControl("detailChkbox") as CheckBox;
            if (chk != null)
            {
                ToggleSelectedState(chk, e);
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var chk = ((CheckBox)sender);

            if (chk.ID == "detailChkbox")
            {
                //var x = chk.Parent;

                Add(chk, (GridDataItem)chk.NamingContainer);
            }
            else if (chk.ID == "headerChkbox")
            {
                foreach (var dataItem in grdListDetail.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Enabled))
                {
                    var chkDetail = (CheckBox)dataItem.FindControl("detailChkbox");
                    chkDetail.Checked = chk.Checked;
                    ToggleSelectedState(chkDetail, new EventArgs());
                }
            }
        }

        private void Add(CheckBox chk, GridDataItem dataItem)
        {
            Add(chk.Checked, dataItem["SRIncidentType"].Text, dataItem["IncidentType"].Text, dataItem["ComponentID"].Text, dataItem["ComponentName"].Text,
                dataItem["SubComponentID"].Text, dataItem["SubComponent"].Text,
                (dataItem.FindControl("txtSubComponentName") as RadTextBox), (dataItem.FindControl("txtModus") as RadTextBox), (dataItem.FindControl("chkIsAllowEdit") as CheckBox)
            );
        }

        private void Add(bool Checked, string SRIncidentType, string IncidentType, string ComponentID, string ComponentName, string SubComponentID, string SubComponent,
            RadTextBox txtSubComponentName, RadTextBox txtModus, CheckBox chkIsAllowEdit)
        {
            var entity = (PatientIncidentComponentTypes.Where(i => i.SRIncidentType == SRIncidentType &&
                                                  i.ComponentID == ComponentID &&
                                                  i.SubComponentID == SubComponentID)).SingleOrDefault();
            if (!Checked)
            {
                // remove
                if (entity != null) entity.MarkAsDeleted();
                return;
            }
            else
            {
                if (entity == null)
                {
                    entity = PatientIncidentComponentTypes.AddNew();
                    entity.PatientIncidentNo = string.Empty;
                    entity.SRIncidentType = SRIncidentType;
                    entity.IncidentType = IncidentType;
                    entity.ComponentID = ComponentID;
                    entity.ComponentName = ComponentName;
                    entity.SubComponentID = SubComponentID;
                    entity.SubComponent = SubComponent;
                    entity.SubComponentName = txtSubComponentName.Text;
                    entity.Modus = txtModus.Text;
                    entity.IsAllowEdit = chkIsAllowEdit.Checked;
                }
                else
                {
                    entity.SubComponentName = txtSubComponentName.Text;
                    entity.Modus = txtModus.Text;
                }
            }
        }

        protected void grdListDetail_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                var ivi = PatientIncidentComponentTypes.Where(x => x.SRIncidentType == dataItem.GetDataKeyValue("SRIncidentType").ToString() &&
                                    x.ComponentID == dataItem.GetDataKeyValue("ComponentID").ToString() &&
                                    x.SubComponentID == dataItem.GetDataKeyValue("SubComponentID").ToString()).FirstOrDefault();
                if (ivi != null)
                {
                    (dataItem.FindControl("detailChkbox") as CheckBox).Checked = true;
                    (dataItem.FindControl("txtSubComponentName") as RadTextBox).Text = ivi.SubComponentName.ToString();
                    (dataItem.FindControl("txtModus") as RadTextBox).Text = ivi.Modus.ToString();
                }
            }
        }

        protected void grdListDetail_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            PopulateListDetailGrid();
        }

        protected void grdListDetail_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            PopulateListDetailGrid();
        }

        protected void grdListDetail_SortCommand(object source, GridSortCommandEventArgs e)
        {
            PopulateListDetailGrid();
        }


        protected void grdList_ItemChkBoxCheckedChanged(object sender, EventArgs e)
        {
            PopulateListDetailGrid();
        }

        protected void grdList_HeaderChkBoxCheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            foreach (var dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>())
            {
                ((CheckBox)dataItem.FindControl("itemChkbox")).Checked = chk.Checked;
            }
            PopulateListDetailGrid();
        }

        private void PopulateListDetailGrid()
        {
            var incidentTypes = new List<string>();
            foreach (var dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("itemChkbox")).Enabled))
            {
                if (((CheckBox)dataItem.FindControl("itemChkbox")).Checked == true)
                {
                    incidentTypes.Add(dataItem["ItemID"].Text);
                }
            }

            if (incidentTypes.Count > 0)
            {
                var query = new IncidentTypeItemQuery("a");
                var itQ = new IncidentTypeQuery("b");
                var pitQ = new PatientIncidentComponentTypeQuery("c");
                var stdQ = new AppStandardReferenceItemQuery("d");
                query.InnerJoin(itQ).On(itQ.SRIncidentType == query.SRIncidentType && itQ.ComponentID == query.ComponentID);
                query.LeftJoin(pitQ).On(pitQ.PatientIncidentNo == PatientIncidentNo && 
                    pitQ.SRIncidentType == query.SRIncidentType &&
                    pitQ.ComponentID == query.ComponentID &&
                    pitQ.SubComponentID == query.SubComponentID);
                query.InnerJoin(stdQ).On(stdQ.StandardReferenceID == AppEnum.StandardReference.IncidentType && stdQ.ItemID == query.SRIncidentType);

                query.Select(
                    query.SRIncidentType,
                    stdQ.ItemName.As("IncidentType"),
                    query.ComponentID,
                    itQ.ComponentName,
                    query.SubComponentID,
                    query.SubComponentName.As("SubComponent"),
                    pitQ.SubComponentName,
                    pitQ.Modus,
                    query.IsAllowEdit,
                    @"<CAST(0 AS BIT) AS 'IsChecked'>"
                    );
                query.Where(query.SRIncidentType.In(incidentTypes));
                query.OrderBy(query.SRIncidentType.Ascending, query.ComponentID.Ascending, query.SubComponentID.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var entity =
                        (PatientIncidentComponentTypes.Where(
                            i =>
                            i.SRIncidentType == row["SRIncidentType"].ToString() &&
                            i.ComponentID == row["ComponentID"].ToString() &&
                            i.SubComponentID == row["SubComponentID"].ToString())).SingleOrDefault();
                    if (entity == null)
                        row["IsChecked"] = false;
                    else row["IsChecked"] = true;
                }

                foreach (DataRow row in dtb.Rows)
                {
                    foreach (var item in PatientIncidentComponentTypes)
                    {
                        var srIncidentType = item.SRIncidentType;
                        var componentId = item.ComponentID;
                        var subComponentId = item.SubComponentID;

                        if (row["SRIncidentType"].ToString() == srIncidentType && row["ComponentID"].ToString() == componentId && row["SubComponentID"].ToString() == subComponentId)
                        {
                            row["SubComponentName"] = item.SubComponentName;
                            row["Modus"] = item.Modus;
                            break;
                        }
                    }
                }

                //string[] NoInPay = dtb.AsEnumerable().Select(x => x.Field<string>("InvoiceNo") + x.Field<string>("PaymentNo")).ToArray();

                //if (NoInPay.Length > 0)
                //{
                //    var xx = InvoicesItems.Where(x => !NoInPay.Contains(x.InvoiceReferenceNo + x.PaymentNo));
                //    foreach (var x in xx)
                //    {
                //        x.MarkAsDeleted();
                //    }
                //}

                dtb.AcceptChanges();
                grdListDetail.DataSource = null;
                grdListDetail.DataSource = dtb;
                grdListDetail.DataBind();
            }
            else
            {
                grdListDetail.DataSource = null;
                grdListDetail.DataBind();
            }
        }
    }
}