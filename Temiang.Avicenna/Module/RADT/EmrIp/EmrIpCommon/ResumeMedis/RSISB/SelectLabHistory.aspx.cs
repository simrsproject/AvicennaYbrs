using System;
using System.Collections.Generic;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.ResumeMedis.RSISB
{
    public partial class SelectLabHistory : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Title = "Select Laboratory Test";
                ButtonOk.OnClientClick = "CloseAndReturnValue();return false;";
            }
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = LaboratoryHist(MergeRegistrations);
        }
        private class SelectedItem
        {
            public int ID { get; set; }
            public string Description { get; set; }
            public string TransactionNo { get; set; }
            public DateTime TransactionDate { get; set; }
            public SelectedItem(int id, string description, string transactionNo, DateTime transactionDate)
            {
                ID = id;
                Description = description;
                TransactionNo = transactionNo;
                TransactionDate = transactionDate;
            }
        }

        private List<SelectedItem> LaboratoryHist(List<string> mergeRegistrations)
        {

            var dtbLab = ResumeMedisRichTextInPatientEntry.LaboratoryResult(mergeRegistrations);
            var orderNo = string.Empty;

            var listItems = new List<SelectedItem>();
            var no = 0;

            foreach (DataRow row in dtbLab.Rows)
            {
                no++;
                var description = string.Empty;
                if (row["Result"] != null && !string.IsNullOrWhiteSpace(row["Result"].ToString()))
                    description = string.Format("{0}: {1} {2}", row["LabOrderSummary"], row["Result"], row["Satuan"]);
                else
                    description = string.Format("{0}", row["LabOrderSummary"]);
                listItems.Add(new SelectedItem(no, description, row["OrderLabNo"].ToString(), Convert.ToDateTime(row["OrderLabTglOrder"])));

            }

            return listItems;
        }

    }
}