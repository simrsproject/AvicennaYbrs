using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Text;
using System.Web.UI.HtmlControls;
using Temiang.Avicenna.Module.RADT.PharmaceuticalCare.DrugObservation;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class EsoDrugItemPicker : BasePageDialog
    {
        private int EsoNo { get { return Request.QueryString["esono"].ToInt(); } }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        private IList<DrugItem> DrugItemSelections
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["diSelections"];
                    if (obj != null)
                        return (IList<DrugItem>)obj;
                }

                var qr = new MedicationReceiveQuery("a");
                qr.Select(qr.MedicationReceiveNo, qr.ItemDescription, qr.RefTransactionNo, qr.RefSequenceNo, qr.SRConsumeMethod, qr.ConsumeQty, qr.SRConsumeUnit);

                var di = new RegistrationEsoItemQuery("b");
                di.Select(di.MedicationReceiveNo);
                di.Where(di.RegistrationNo == RegistrationNo, di.EsoNo == EsoNo, di.MedicationReceiveNo == qr.MedicationReceiveNo);

                qr.Where(qr.RegistrationNo == RegistrationNo, qr.MedicationReceiveNo.NotIn(di));

                var dtb = qr.LoadDataTable();
                IList<DrugItem> drugItems = new List<DrugItem>();
                foreach (DataRow row in dtb.Rows)
                {
                    var itemDesc = row["ItemDescription"].ToString();
                    if (row["RefTransactionNo"] != DBNull.Value && row["RefSequenceNo"] != DBNull.Value)
                        itemDesc = MedicationReceive.PrescriptionItemDescription(row["RefTransactionNo"].ToString(), row["RefSequenceNo"].ToString(), row["ItemDescription"].ToString(), false, false);

                    var cm = ConsumeMethod(row["SRConsumeMethod"].ToString(), row["ConsumeQty"].ToString(), row["SRConsumeUnit"].ToString());

                    drugItems.Add(new DrugItem(row["MedicationReceiveNo"].ToInt(), itemDesc, cm));
                }
                Session["diSelections"] = drugItems;

                return drugItems;
            }

        }

        protected void grdSelection_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSelection.DataSource = DrugItemSelections;
        }

        private IList<DrugItem> DrugItemSelecteds
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["diSelecteds"];
                    if (obj != null)
                        return (IList<DrugItem>)obj;
                }

                var qr = new MedicationReceiveQuery("a");
                var di = new RegistrationEsoItemQuery("b");
                qr.InnerJoin(di).On(qr.MedicationReceiveNo == di.MedicationReceiveNo);
                qr.Select(qr.MedicationReceiveNo, qr.ItemDescription, qr.RefTransactionNo, qr.RefSequenceNo, qr.SRConsumeMethod, qr.ConsumeQty,qr.SRConsumeUnit);
                qr.Where(di.RegistrationNo == RegistrationNo, di.EsoNo == EsoNo);
                var dtb = qr.LoadDataTable();
                IList<DrugItem> drugItems = new List<DrugItem>();
                foreach (DataRow row in dtb.Rows)
                {
                    var itemDesc = row["ItemDescription"].ToString();
                    if (row["RefTransactionNo"] != DBNull.Value && row["RefSequenceNo"] != DBNull.Value)
                        itemDesc = MedicationReceive.PrescriptionItemDescription(row["RefTransactionNo"].ToString(), row["RefSequenceNo"].ToString(), row["ItemDescription"].ToString(), false, false);

                    var cm = ConsumeMethod(row["SRConsumeMethod"].ToString(), row["ConsumeQty"].ToString(), row["SRConsumeUnit"].ToString());

                    drugItems.Add(new DrugItem(row["MedicationReceiveNo"].ToInt(), itemDesc, cm));
                }
                Session["diSelecteds"] = drugItems;

                return drugItems;
            }

        }

        private string ConsumeMethod(string srConsumeMethod, string consumeQty, string srConsumeUnit)
        {
            var cons = new ConsumeMethod();
            cons.LoadByPrimaryKey(srConsumeMethod);
            return string.Format("{0} @{1} {2}", cons.SRConsumeMethodName, consumeQty, srConsumeUnit);
        }

        protected void grdSelected_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSelected.DataSource = DrugItemSelecteds;
        }

        protected void grdSelection_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == grdSelection.ClientID)
                {
                    // items are drag from selection to selected grid 
                    if ((e.DestDataItem == null && DrugItemSelecteds.Count == 0) ||
                        e.DestDataItem != null &&
                        e.DestDataItem.OwnerGridID == grdSelected.ClientID)
                        MoveDrugItem(e, DrugItemSelections, DrugItemSelecteds);
                }
            }
        }

        protected void grdSelected_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == grdSelected.ClientID)
                {
                    // items are drag from selectied to selection grid 
                    if ((e.DestDataItem == null && DrugItemSelections.Count == 0) ||
                        e.DestDataItem != null &&
                        e.DestDataItem.OwnerGridID == grdSelection.ClientID)
                        MoveDrugItem(e, DrugItemSelecteds, DrugItemSelections);
                }
            }
        }

        private void MoveDrugItem(GridDragDropEventArgs e, IList<DrugItem> sourceList, IList<DrugItem> destinationList)
        {
            int destinationIndex = -1;
            if (e.DestDataItem != null)
            {
                DrugItem order = GetDrugItem(destinationList, (int)e.DestDataItem.GetDataKeyValue("MedicationReceiveNo"));
                destinationIndex = (order != null) ? destinationList.IndexOf(order) : -1;
            }

            foreach (GridDataItem draggedItem in e.DraggedItems)
            {
                DrugItem tmpDrugItem = GetDrugItem(sourceList, (int)draggedItem.GetDataKeyValue("MedicationReceiveNo"));
                if (tmpDrugItem != null)
                {
                    if (destinationIndex > -1)
                        destinationList.Insert(destinationIndex, tmpDrugItem);
                    else
                    {
                        destinationList.Add(tmpDrugItem);
                    }

                    sourceList.Remove(tmpDrugItem);
                }
            }

            grdSelection.Rebind();
            grdSelected.Rebind();
        }

        private static DrugItem GetDrugItem(IEnumerable<DrugItem> drugItemToSearchIn, int medicationReceiveNo)
        {
            foreach (DrugItem item in drugItemToSearchIn)
            {
                if (item.MedicationReceiveNo == medicationReceiveNo)
                    return item;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}
