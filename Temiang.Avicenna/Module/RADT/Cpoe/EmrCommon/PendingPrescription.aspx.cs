using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe.EmrCommon
{
    /// <summary>
    /// Layar untuk keperluan perawat melihat status resep yg sudah complete tetapi belum diambil
    /// Dipanggil dari layar EMR List
    /// </summary>
    public partial class PendingPrescription : BasePageDialog
    {
        protected String RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        protected string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Title = "Complete Prescription has not been delivered";

            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
        }

        protected string PrescriptionDetailInHTML(string registrationNo)
        {
            var presc = new TransPrescriptionQuery("b");

            //Sub Query resep return
            var subQr = new TransPrescriptionQuery("tp");
            //subQr.Where(subQr.RegistrationNo == regNo, subQr.IsPrescriptionReturn == true, subQr.Or(subQr.IsVoid.IsNull(), subQr.IsVoid == false));
            subQr.Where(subQr.RegistrationNo == registrationNo,subQr.ReferenceNo==presc.PrescriptionNo, 
                subQr.IsPrescriptionReturn == true, subQr.Or(subQr.IsVoid.IsNull(), subQr.IsVoid == false));
            subQr.Select(subQr.ReferenceNo);

            var itemQuery = new TransPrescriptionItemQuery("a");
            var medic = new ParamedicQuery("d");
            var item = new ItemQuery("c");
            var consume = new ConsumeMethodQuery("e");
            var emb = new EmbalaceQuery("g");
            var oriconsume = new ConsumeMethodQuery("h");

            presc.Select(
                presc.PrescriptionNo,
                itemQuery.SequenceNo,
                presc.PrescriptionDate,
                medic.ParamedicName,
                presc.ParamedicID,
                presc.CreatedByUserID,
                item.ItemName,
                @"<ISNULL(a.OriResultQty, a.ResultQty) AS ResultQty>",
                @"<ISNULL(a.OriSRItemUnit, a.SRItemUnit) AS SRItemUnit>",
                @"<ISNULL(h.SRConsumeMethodName, e.SRConsumeMethodName) AS SRConsumeMethodName>",
                presc.IsUnitDosePrescription,
                itemQuery.IsRFlag.Coalesce("CAST(0 AS BIT)").As("IsRFlag"),
                itemQuery.IsCompound.Coalesce("CAST(0 AS BIT)").As("IsCompound"),
                emb.EmbalaceLabel,
                @"<ISNULL(a.OriDosageQty, a.DosageQty) AS DosageQty>",
                @"<ISNULL(a.OriSRDosageUnit, a.SRDosageUnit) AS SRDosageUnit>",
                itemQuery.EmbalaceQty,
                presc.Note,
                @"<ISNULL(a.OriConsumeQty, a.ConsumeQty) AS ConsumeQty>",
                @"<ISNULL(a.OriSRConsumeUnit, a.SRConsumeUnit) AS SRConsumeUnit>",
                "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                itemQuery.LineAmount,
                itemQuery.Notes, presc.CompleteDateTime
                );

            presc.LeftJoin(itemQuery).On(itemQuery.PrescriptionNo == presc.PrescriptionNo);
            presc.InnerJoin(medic).On(presc.ParamedicID == medic.ParamedicID);
            presc.LeftJoin(item).On(itemQuery.ItemID == item.ItemID);
            presc.LeftJoin(consume).On(itemQuery.SRConsumeMethod == consume.SRConsumeMethod);
            presc.LeftJoin(oriconsume).On(itemQuery.OriSRConsumeMethod == oriconsume.SRConsumeMethod);
            presc.LeftJoin(emb).On(itemQuery.EmbalaceID == emb.EmbalaceID);

            presc.Where(presc.RegistrationNo == registrationNo, presc.IsPrescriptionReturn == false, presc.Or(presc.IsVoid.IsNull(), presc.IsVoid == false));

            // Sudah komplit dan belum dideliver
            presc.Where(presc.CompleteDateTime.IsNotNull(), presc.DeliverDateTime.IsNull());

            presc.Where(presc.PrescriptionNo.NotIn(subQr));

            presc.OrderBy(presc.PrescriptionDate.Descending, presc.PrescriptionNo.Descending);
            presc.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            var table = presc.LoadDataTable();

            // Ambil list PrescriptionNo
            var prescs = from t in table.AsEnumerable()
                         group t by new
                         {
                             PrescriptionNo = t.Field<string>("PrescriptionNo")
                         }
                             into g
                             select new
                             {
                                 g.Key.PrescriptionNo
                             };


            var displayTotal = AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor;

            var prescriptionHeader = "";
            var sbPresciption = new StringBuilder();
            sbPresciption.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");

            var no = 1;
            foreach (var p in prescs)
            {
                int i = 0;
                double total = 0;
                var sbItem = new StringBuilder();
                foreach (DataRow r in table.AsEnumerable().Where(t => t.Field<string>("PrescriptionNo") == p.PrescriptionNo))
                {

                    if (i == 0)
                    {
                        prescriptionHeader = string.Format("Presc No: {0} Date:{1} Complete: {2}", r["PrescriptionNo"], Convert.ToDateTime(r["PrescriptionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth), Convert.ToDateTime(r["CompleteDateTime"]).ToString(AppConstant.DisplayFormat.DateHourMinute));
                    }
                    i++;

                    if (r["SequenceNo"] == DBNull.Value) continue;

                    if (!Convert.ToBoolean(r["IsCompound"]))
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} ({4} @ {5} {6} {7})<br />",
                            Convert.ToBoolean(r["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                            r["ItemName"], r["ResultQty"], r["SRItemUnit"], r["SRConsumeMethodName"], r["ConsumeQty"], r["SRConsumeUnit"], r["Notes"]);
                    }
                    else
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} ({6} @ {7} {8} {9})<br />",
                            Convert.ToBoolean(r["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                            r["ItemName"], r["EmbalaceQty"], r["EmbalaceLabel"], r["DosageQty"], r["SRDosageUnit"], r["SRConsumeMethodName"],
                            r["ConsumeQty"], r["SRConsumeUnit"], r["Notes"]);
                    }

                    total += Convert.ToDouble(r["LineAmount"]);
                }

                if (displayTotal)
                    sbItem.AppendFormat("<b>{0}</b>", " (Rp. " + string.Format("{0:n2}", (total)) + ")");

                sbPresciption.AppendLine("<tr><td align='left' style='width:20px;padding-top: 4px;vertical-align:top;'>"); ;

                sbPresciption.AppendFormat("<td align='left'><fieldset><legend>{0}. {1}</legend>{2}</fieldset></br></td>", no, prescriptionHeader, sbItem);
                sbPresciption.AppendLine("</tr>");
                no ++;
            }
            sbPresciption.AppendLine("</table>");
            return sbPresciption.ToString();
        }
    }
}