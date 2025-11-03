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
using Temiang.Avicenna.BusinessObject.Common;
using System.Text;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Microsoft.Owin;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.PrescriptionSalesCommon
{
    public partial class PrescriptionTemplate : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["rt"] == "opr"
                                                    ? AppConstant.Program.PrescriptionSalesOpr
                                                    : AppConstant.Program.PrescriptionSales;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Pharmacy Prescription Template";
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;

            if (!IsPostBack)
            {
            }
        }

        #region Prescription Template
        protected void grdPrescriptionTemplate_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            using (var trans = new esTransactionScope())
            {
                var templateNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransPrescriptionTemplateMetadata.ColumnNames.TemplateNo]);
                var entity = new TransPrescriptionTemplate();
                if (entity.LoadByPrimaryKey(templateNo))
                {
                    var tpit = new TransPrescriptionItemTemplateCollection();
                    tpit.Query.Where(tpit.Query.TemplateNo == templateNo);
                    tpit.LoadAll();
                    tpit.MarkAllAsDeleted();
                    tpit.Save();

                    entity.MarkAsDeleted();
                    entity.Save();

                    trans.Complete();
                }

            }
            Session["prescTemplate"] = null;
            grdPrescriptionTemplate.DataSource = null;
            grdPrescriptionTemplate.Rebind();
        }
        protected void grdPrescriptionTemplate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdPrescriptionTemplate.Columns[grdPrescriptionTemplate.Columns.Count - 1].Visible = this.IsPowerUser;
            grdPrescriptionTemplate.DataSource = PrescriptionTemplateDataTable();
        }

        private DataTable PrescriptionTemplateDataTable()
        {
            if (IsPostBack && Session["prescTemplate"] != null)
            {
                return (DataTable)Session["prescTemplate"];
            }
            var qr = new TransPrescriptionTemplateQuery("r");
            qr.Select(qr.TemplateNo, qr.TemplateName);
            qr.Where(qr.ParamedicID == "PHARMACY");
            qr.OrderBy(qr.TemplateName.Ascending);

            qr.Select(qr.TemplateNo, qr.TemplateName);
            var dtbTemplate = qr.LoadDataTable();
            dtbTemplate.Columns.Add(new DataColumn("PrescriptionTemplate", typeof(string)));

            foreach (DataRow rowHd in dtbTemplate.Rows)
            {
                var tno = rowHd["TemplateNo"].ToString();
                var tname = rowHd["TemplateName"].ToString();

                var template = PrescriptionTemplateDetailInHTML(tno, tname);
                if (string.IsNullOrEmpty(template))
                {
                    continue;
                }
                rowHd["PrescriptionTemplate"] = template;
            }

            //Jangan diganti nama sessionnya krn dipakai pada page new template
            Session["prescTemplate"] = dtbTemplate;
            return dtbTemplate;
        }

        private string PrescriptionTemplateDetailInHTML(string templateNo, string templateName)
        {
            var query = new TransPrescriptionItemTemplateQuery("a");
            var item = new ItemQuery("c");
            var consume = new ConsumeMethodQuery("e");
            var emb = new EmbalaceQuery("g");

            query.Select(query.TemplateNo, query.SequenceNo, query.ItemID, item.ItemName,
                query.ResultQty, query.SRItemUnit, consume.SRConsumeMethodName,
                query.IsRFlag.Coalesce("CAST(0 AS BIT)").As("IsRFlag"),
                query.IsCompound.Coalesce("CAST(0 AS BIT)").As("IsCompound"),
                emb.EmbalaceLabel, query.DosageQty, query.SRDosageUnit,
                query.EmbalaceQty, query.ConsumeQty, query.SRConsumeUnit,
                "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                query.Notes
                );


            query.LeftJoin(item).On(query.ItemID == item.ItemID);
            query.LeftJoin(consume).On(query.SRConsumeMethod == consume.SRConsumeMethod);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.Where(query.TemplateNo == templateNo);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            var dtbTemplateItem = query.LoadDataTable();

            var sbPresciption = new StringBuilder();
            var sbItem = new StringBuilder();
            sbPresciption.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
            foreach (DataRow row in dtbTemplateItem.Rows)
            {
                if (!Convert.ToBoolean(row["IsCompound"]))
                {
                    sbItem.AppendFormat("{0} {1} {2} {3} ({4} @ {5} {6} {7})<br />",
                        Convert.ToBoolean(row["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                        row["ItemName"],
                        row["ResultQty"],
                        StandardReference.GetItemName(AppEnum.StandardReference.ItemUnit, row["SRItemUnit"].ToString()),
                        row["SRConsumeMethodName"],
                        row["ConsumeQty"],
                        StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRConsumeUnit"].ToString()),
                        row["Notes"]);
                }
                else
                {
                    sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} ({6} @ {7} {8} {9})<br />",
                        Convert.ToBoolean(row["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                        row["ItemName"],
                        row["EmbalaceQty"],
                        row["EmbalaceLabel"],
                        row["DosageQty"],
                        StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRDosageUnit"].ToString()),
                        row["SRConsumeMethodName"],
                        row["ConsumeQty"],
                        StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRConsumeUnit"].ToString()),
                        row["Notes"]);
                }
            }

            sbPresciption.AppendLine("<tr><td align='left' style='width:20px;padding-top: 4px;vertical-align:top;'>");

            if (Request.QueryString["mode"] == "copy")
            {
                // Mode new bisa copy dari history prescription
                sbPresciption.AppendFormat("<div title='Copy to Current Entry'><a href='javascript:void(0);' onclick=\"javascript:showCopyPrescription('{0}')\"><img src='../../../../../Images/Toolbar/ordering16.png' alt='Copy' /></a></div></br>", templateNo);
            }

            sbPresciption.AppendLine("</td><td align='left'>");
            sbPresciption.AppendFormat("<fieldset><legend>{0}</legend>{1}</fieldset></br>", templateName, sbItem);
            sbPresciption.AppendLine("</td></tr>");

            sbPresciption.AppendLine("</table>");


            return sbPresciption.ToString();
        }
        #endregion

    }
}
