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

namespace Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.ClinicalPathway
{
    public partial class SelectTransactionItem : BasePageDialog
    {
        #region QueryString
        private string RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        private int DayNo
        {
            get { return Request.QueryString["dayno"].ToInt(); }
        }
        private int PathwayItemSeqNo
        {
            get { return Request.QueryString["seqno"].ToInt(); }
        }
        private string PathwayID
        {
            get { return Request.QueryString["pwid"]; }
        }
        private string PathwayItemID
        {
            get { return Request.QueryString["pwitid"]; }
        }
        private DateTime RegistrationDate
        {
            get { return Convert.ToDateTime(Request.QueryString["regdate"]); }
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Select Transaction";
            optDayNoType.Items[1].Text = string.Format("Day No {0}", DayNo);

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //    txtSearch.Text = Request.QueryString["pwitname"].Replace("_", "'");

        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //Populate
            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var item = new Item();
            item.LoadByPrimaryKey(PathwayItemID);
            switch (item.SRItemType)
            {
                case BusinessObject.Reference.ItemType.Medical:
                case BusinessObject.Reference.ItemType.NonMedical:
                    {
                        var dtb = PrescriptionItems;
                        dtb.Merge(ChargesItems(item.SRItemType));
                        grdList.DataSource = dtb.Select(null, "ItemName ASC");
                    }
                    break;
                case BusinessObject.Reference.ItemType.Service: //Charges, Job Order etc
                case BusinessObject.Reference.ItemType.Laboratory:
                case BusinessObject.Reference.ItemType.Radiology:
                case BusinessObject.Reference.ItemType.Package:
                case BusinessObject.Reference.ItemType.Diagnostic:
                    grdList.DataSource = ChargesItems(item.SRItemType);
                    break;
                default:
                    {
                        var dtb = PrescriptionItems;
                        dtb.Merge(ChargesItems(item.SRItemType));
                        grdList.DataSource = dtb.Select(null, "ItemName ASC");
                    }
                    break;
            }
        }


        private DataTable PrescriptionItems
        {
            get
            {
                var zats = new ItemProductMedicZatActiveCollection();
                zats.Query.Where(zats.Query.ZatActiveID == Request.QueryString["pwitid"]);
                var zat = zats.Query.Load();

                var query = new TransPrescriptionQuery("a");
                var prescItem = new TransPrescriptionItemQuery("b");
                query.InnerJoin(prescItem).On(query.PrescriptionNo == prescItem.PrescriptionNo);

                var item = new ItemQuery("i");
                query.LeftJoin(item).On(prescItem.ItemID == item.ItemID);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                (
                    query.PrescriptionNo.As("ReferenceNo"),
                    query.PrescriptionDate.As("TransactionDate"),
                    item.ItemID,
                    item.ItemName
                );

                query.Where
                (
                    query.IsApproval == true,
                    query.RegistrationNo == RegistrationNo
                );

                var items = new ClinicalPathwayCollection();
                items.Query.Where(items.Query.RegistrationNo == RegistrationNo);
                if (items.Query.Load())
                {
                    query.Where(
                        //(query.TransactionNo + "-" + transItem.ItemID).NotIn(items.Select(i => new { ReferenceNo = i.ReferenceNo + "-" + i.ItemID })),
                        $"<a.PrescriptionNo+'-'+i.ItemID NOT IN ('{string.Join("','", items.Select(i => i.ReferenceNo + "-" + i.ItemID))}')>"
                        );
                }

                if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["pwitid"]))
                {
                    if (!zat) query.Where(item.ItemID == Request.QueryString["pwitid"]);
                    else query.Where(query.Or(item.ItemID == Request.QueryString["pwitid"], item.ItemID.In(zats.Select(z => z.ItemID))));
                }
                else if (!txtSearch.Text.Trim().Equals(string.Empty))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    query.Where
                    (
                        query.Or
                        (
                            item.ItemID.Like(searchTextContain),
                            item.ItemName.Like(searchTextContain)
                        )
                    );
                }

                if (optDayNoType.SelectedIndex == 1)
                {
                    var date = RegistrationDate.AddDays(DayNo - 1);
                    query.Where(query.PrescriptionDate == date);
                }


                DataTable dtb = query.LoadDataTable();
                dtb.Columns.Add("DayNo", typeof(int));
                foreach (DataRow row in dtb.Rows)
                {
                    row["DayNo"] =
                        (Convert.ToDateTime(row["TransactionDate"]).Date - RegistrationDate).TotalDays.ToInt() + 1;
                }
                return dtb;
            }
        }
        private DataTable ChargesItems(string itemType)
        {
            var query = new TransChargesQuery("a");
            var transItem = new TransChargesItemQuery("b");
            query.InnerJoin(transItem).On(query.TransactionNo == transItem.TransactionNo);

            var item = new ItemQuery("i");
            query.InnerJoin(item).On(transItem.ItemID == item.ItemID);


            query.es.Top = AppSession.Parameter.MaxResultRecord;

            query.Select
            (
                query.TransactionNo.As("ReferenceNo"),
                query.TransactionDate,
                item.ItemID,
                item.ItemName
            );

            query.Where
            (
                query.IsApproved == true,
                query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(RegistrationNo))
            );

            var items = new ClinicalPathwayCollection();
            items.Query.Where(items.Query.RegistrationNo == RegistrationNo);
            if (items.Query.Load())
            {
                query.Where(
                    //(query.TransactionNo + "-" + transItem.ItemID).NotIn(items.Select(i => new { ReferenceNo = i.ReferenceNo + "-" + i.ItemID })),
                    $"<a.TransactionNo+'-'+i.ItemID NOT IN ('{string.Join("','", items.Select(i => i.ReferenceNo + "-" + i.ItemID))}')>"
                    );
            }

            if (!string.IsNullOrEmpty(itemType))
                query.Where(item.SRItemType == itemType);

            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["pwitid"]))
                query.Where(item.ItemID == Request.QueryString["pwitid"]);
            else if (!txtSearch.Text.Trim().Equals(string.Empty))
            {
                string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                query.Where
                (
                    query.Or
                    (
                        item.ItemID.Like(searchTextContain),
                        item.ItemName.Like(searchTextContain)
                    )
                );
            }
            if (optDayNoType.SelectedIndex == 1)
            {
                var date = RegistrationDate.AddDays(DayNo - 1);
                query.Where(query.TransactionDate >= date, query.TransactionDate < date.AddDays(1));
            }

            DataTable dtb = query.LoadDataTable();
            dtb.Columns.Add("DayNo", typeof(int));
            foreach (DataRow row in dtb.Rows)
            {
                row["DayNo"] =
                    (Convert.ToDateTime(row["TransactionDate"]).Date - RegistrationDate).TotalDays.ToInt() + 1;
            }
            return dtb;
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            var selectedItems = grdList.MasterTableView.GetSelectedItems();
            if (selectedItems.Length > 0)
            {
                var itemID = Convert.ToString(selectedItems[0].GetDataKeyValue("ItemID"));
                var referenceNo = Convert.ToString(selectedItems[0].GetDataKeyValue("ReferenceNo"));
                var cp = new BusinessObject.ClinicalPathway();
                if (!cp.LoadByPrimaryKey(RegistrationNo, PathwayID, PathwayItemSeqNo, DayNo))
                {
                    cp.RegistrationNo = RegistrationNo;
                    cp.PathwayID = PathwayID;
                    cp.PathwayItemSeqNo = PathwayItemSeqNo;
                    cp.DayNo = DayNo;
                }

                cp.IsRealized = true;
                cp.RealizedDateTime = DateTime.Now;
                cp.ReferenceNo = referenceNo;
                cp.ItemID = PathwayItemID;
                if (itemID != PathwayItemID)
                    cp.InterventionItemID = itemID;
                cp.Save();

            }
            else
            {
                args.IsCancel = true;
                args.MessageText = "Select item first";
            }
        }


    }


}