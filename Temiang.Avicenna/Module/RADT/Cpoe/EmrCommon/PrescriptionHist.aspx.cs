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

namespace Temiang.Avicenna.Module.Emr.EmrCommon
{
    /// <summary>
    /// Layar untuk keperluan perawat melihat status resep yg sudah complete tetapi belum diambil
    /// Dipanggil dari layar EMR List
    /// </summary>
    public partial class PrescriptionHist : BasePageDialog
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
            Title = "Prescription History";

           Footer.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                timelinePrescription.DataSource = Prescriptions();
                timelinePrescription.DataBind();
            }
        }
        private List<Prescription> Prescriptions()
        {
            var retVal = new List<Prescription>();
            var presc = new TransPrescriptionQuery("b");

            //Sub Query resep return
            var subQr = new TransPrescriptionQuery("tp");
            //subQr.Where(subQr.RegistrationNo == regNo, subQr.IsPrescriptionReturn == true, subQr.Or(subQr.IsVoid.IsNull(), subQr.IsVoid == false));
            subQr.Where(subQr.ReferenceNo == presc.PrescriptionNo,
                subQr.IsPrescriptionReturn == true, subQr.Or(subQr.IsVoid.IsNull(), subQr.IsVoid == false));
            subQr.Select(subQr.ReferenceNo);

            var itemQuery = new TransPrescriptionItemQuery("a");
            presc.LeftJoin(itemQuery).On(itemQuery.PrescriptionNo == presc.PrescriptionNo);

            var medic = new ParamedicQuery("d");
            presc.InnerJoin(medic).On(presc.ParamedicID == medic.ParamedicID);

            var item = new ItemQuery("c");
            presc.LeftJoin(item).On(itemQuery.ItemID == item.ItemID);

            var consume = new ConsumeMethodQuery("e");
            presc.LeftJoin(consume).On(itemQuery.SRConsumeMethod == consume.SRConsumeMethod);

            var emb = new EmbalaceQuery("g");
            presc.LeftJoin(emb).On(itemQuery.EmbalaceID == emb.EmbalaceID);

            var oriconsume = new ConsumeMethodQuery("h");
            presc.LeftJoin(oriconsume).On(itemQuery.OriSRConsumeMethod == oriconsume.SRConsumeMethod);

            var reg = new RegistrationQuery("r");
            presc.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo);

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
                itemQuery.Notes, presc.CompleteDateTime,
                reg.Complaint
                );


            if (RegistrationType == AppConstant.RegistrationType.InPatient)
            {
                presc.Where(presc.RegistrationNo.In(AppCache.RelatedRegistrations(IsPostBack, RegistrationNo)));
            }
            else
            {
                var patRels = Patient.PatientRelateds(PatientID);
                if (patRels.Count == 1)
                    presc.Where(reg.PatientID == PatientID);
                else
                    presc.Where(reg.PatientID.In(patRels));
            }

            presc.Where(presc.IsPrescriptionReturn == false, presc.Or(presc.IsVoid.IsNull(), presc.IsVoid == false));

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

            var no = 1;
            foreach (var p in prescs)
            {
                int i = 0;
                double total = 0;
                var sbItem = new StringBuilder();

                var prescItem = new Prescription();

                foreach (DataRow r in table.AsEnumerable().Where(t => t.Field<string>("PrescriptionNo") == p.PrescriptionNo))
                {

                    if (i == 0)
                    {
                        prescItem.Title = r["PrescriptionNo"].ToString();
                        prescItem.PrescriptionDate = Convert.ToDateTime(r["PrescriptionDate"]);
                        prescItem.PrescriptionDateLabel = Convert.ToDateTime(r["PrescriptionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth);
                        prescItem.ParamedicName = r["ParamedicName"].ToString();
                        prescItem.ChiefComplaint = r["Complaint"].ToString();
                        //prescriptionHeader = string.Format("Presc No: {0} Date:{1} Complete: {2}", r["PrescriptionNo"], Convert.ToDateTime(r["PrescriptionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth), Convert.ToDateTime(r["CompleteDateTime"]).ToString(AppConstant.DisplayFormat.DateHourMinute));
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


                prescItem.PrescriptionItem = sbItem.ToString();

                retVal.Add(prescItem);
                no++;
            }
            //sbPresciption.AppendLine("</table>");


            //return sbPresciption.ToString();

            return retVal;
        }

        private class Prescription
        {
            public string Title { get; set; }
            public string ParamedicName { get; set; }
            public string PrescriptionItem { get; set; }
            public string Url { get; set; }
            public DateTime PrescriptionDate { get; set; }
            public string PrescriptionDateLabel { get; set; }
            public string ChiefComplaint { get; set; }
            
        }
    }
}