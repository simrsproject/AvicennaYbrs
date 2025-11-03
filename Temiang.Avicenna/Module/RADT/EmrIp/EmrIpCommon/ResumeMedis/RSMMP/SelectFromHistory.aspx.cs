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

namespace Temiang.Avicenna.Module.RADT.Emr.ResumeMedis.RSMMP
{
    public partial class SelectFromHistory : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Title = "Select Precription Item";
                ButtonOk.OnClientClick = "CloseAndReturnValue();return false;";
            }

        }
        #region Prescription

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //if (!IsPostBack || Session["Selected"] == null)
            //{
            //    Session["Selected"] = PrescriptionHist(MergeRegistrations);
            //}
            //grdList.DataSource = Session["Selected"];

            grdList.DataSource = PrescriptionHist(MergeRegistrations);
        }


        #endregion

        private class SelectedItem
        {
            public int ID { get; set; }
            public string Description { get; set; }
            public bool IsSelected { get; set; }
            public SelectedItem(int id, string description)
            {
                ID = id;
                Description = description;
            }
        }

        #region Prescription History
        private List<SelectedItem> PrescriptionHist(List<string> mergeRegistrations)
        {
            var listItems = new List<SelectedItem>();
            var no = 1;
            // Obat patent
            var query = PrescriptionItemNameList(mergeRegistrations, false);
            var dtbPresc = query.LoadDataTable();
            var strb = new StringBuilder();
            foreach (DataRow row in dtbPresc.Rows)
            {
                listItems.Add(new SelectedItem(no, string.Format("{0} ({1} {2} {3})",
                    row["ItemName"], row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"])));
                no++;
            }

            // Obat Racikan
            query = PrescriptionItemNameList(mergeRegistrations, true);
            dtbPresc = query.LoadDataTable();
            foreach (DataRow row in dtbPresc.Rows)
            {
                var consumeMethod = string.Format("{0} {1} {2}", row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"]);
                var itemDescription = PrescriptionItemCompound(row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), consumeMethod);
                listItems.Add(new SelectedItem(no, itemDescription));

                no++;
            }
            return listItems;
        }
        private TransPrescriptionItemQuery PrescriptionItemNameList(List<string> mergeRegistrations, bool isCompound)
        {
            //Prescription History, yg diambil hanya daftar obat dan consume methodnya
            var query = new TransPrescriptionItemQuery("a");
            var qrPresc = new TransPrescriptionQuery("b");
            query.InnerJoin(qrPresc).On(query.PrescriptionNo == qrPresc.PrescriptionNo);

            var qrItem = new ItemQuery("i");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

            var itemProduct = new ItemProductMedicQuery("ip");
            query.InnerJoin(itemProduct).On(query.ItemID == itemProduct.ItemID);


            var itemIntervention = new ItemQuery("int");
            query.LeftJoin(itemIntervention).On(query.ItemInterventionID == itemIntervention.ItemID);

            var itemProductInt = new ItemProductMedicQuery("ipi");
            query.LeftJoin(itemProductInt).On(query.ItemInterventionID == itemProductInt.ItemID);


            var consume = new ConsumeMethodQuery("e");
            query.LeftJoin(consume).On(query.SRConsumeMethod == consume.SRConsumeMethod);


            query.Select(
                "<COALESCE(int.ItemName, i.ItemName) as ItemName>",
                consume.SRConsumeMethodName,
                query.ConsumeQty,
                query.SRConsumeUnit,
                query.IsCompound

            );

            if (isCompound)
            {
                query.Select(query.ParentNo,
                    query.SequenceNo, query.PrescriptionNo);
                query.Where(query.Or(query.ParentNo.IsNull(), query.ParentNo == string.Empty));
            }
            else
            {
                query.Select("<'' as ParentNo>",
                    "<'' as SequenceNo>",
                    "<'' as PrescriptionNo>");
            }
            query.OrderBy("ItemName", esOrderByDirection.Ascending);
            query.es.Distinct = true;
            //if (!string.IsNullOrEmpty(fromRegistrationNo))
            //    query.Where(query.Or(qrPresc.RegistrationNo == registrationNo,
            //        qrPresc.RegistrationNo == fromRegistrationNo));
            //else
            //    query.Where(qrPresc.RegistrationNo == registrationNo);

            query.Where(qrPresc.RegistrationNo.In(mergeRegistrations));
            // Hanya tipe medication
            query.Where(query.Or(itemProductInt.IsMedication == true,query.And(itemProductInt.IsMedication.IsNull(), itemProduct.IsMedication == true)));
            query.Where(qrItem.ItemGroupID.In("FM.01", "FM.02", "FM.03", "FM.07", "FM.09")); 

            query.Where(query.IsCompound == isCompound);
            return query;
        }
        private string PrescriptionItemCompound(string prescriptionNo, string sequenceNo, string consumeMethod)
        {
            // Racikan
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemMedic = new ItemProductMedicQuery("im");
            var qItemIntervention = new ItemQuery("c");

            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID);
            query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);

            query.Select
            (
                query.ItemInterventionID, query.ParentNo, query.IsRFlag,
                qItem.ItemName, query.SRDosageUnit, query.DosageQty,
                qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention")
            );

            query.Where(query.PrescriptionNo == prescriptionNo, query.Or(query.SequenceNo == sequenceNo, query.ParentNo == sequenceNo));
            query.OrderBy(query.SequenceNo.Ascending);

            var dtb = query.LoadDataTable();
            var sbItem = new StringBuilder();
            foreach (DataRow row in dtb.Rows)
            {
                var itemName = row["ItemName"].ToString();

                if (row["ItemInterventionID"] != DBNull.Value &&
                    !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()))
                {
                    itemName = row["ItemNameIntervention"].ToString();
                }

                if (row["ParentNo"] != DBNull.Value && string.IsNullOrEmpty(row["ParentNo"].ToString()))
                {
                    //Header
                    sbItem = new StringBuilder();
                    sbItem.AppendFormat("{0} @{1} {2} ({3}){4}", itemName, row["DosageQty"], row["SRDosageUnit"], consumeMethod, Environment.NewLine);
                    sbItem.AppendLine("<ul>");
                }
                else
                {
                    sbItem.AppendFormat("<li>{0} @{1} {2}{3}</li>", itemName, row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                }
            }
            sbItem.AppendLine("</ul>");
            return sbItem.ToString();
        }
        #endregion

    }
}