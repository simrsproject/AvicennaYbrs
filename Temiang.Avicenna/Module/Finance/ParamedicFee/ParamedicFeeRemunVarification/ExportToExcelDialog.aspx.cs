using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ExportToExcelDialog : BasePageDialog
    {
        private string RemunNo
        {
            get
            {
                return Request.QueryString["rno"];
            }
        }

        private bool IsSummary {
            get {
                return Request.QueryString["type"] == "sum";
            }
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";

            }

            if (IsSummary)
            {
                ExportSum();
            }
            else {
                ExportDetail();
            }
            
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        private void ExportSum() {
            var remd = new ParamedicFeeRemunByIdiDetailQuery("remd");
            var rem = new ParamedicFeeRemunByIdiQuery("rem");
            var par = new ParamedicQuery("par");
            var i = new ItemQuery("i");
            var su = new ServiceUnitQuery("su");
            var idi = new ItemIdiQuery("idi");

            remd.InnerJoin(rem).On(remd.RemunID == rem.RemunID)
                .InnerJoin(par).On(remd.ParamedicID == par.ParamedicID)
                .InnerJoin(i).On(remd.ItemID == i.ItemID)
                .InnerJoin(su).On(remd.ServiceUnitID == su.ServiceUnitID)
                .InnerJoin(idi).On(remd.IdiCode == idi.IdiCode)
                .Where(rem.RemunNo == RemunNo)
                .Select(
                    rem.RemunNo,
                    rem.DateStart,
                    rem.DateEnd,
                    remd.ParamedicID,
                    par.ParamedicName,
                    remd.ItemID,
                    i.ItemName,
                    remd.ServiceUnitID,
                    su.ServiceUnitName,
                    remd.IdiCode,
                    idi.IdiName,
                    remd.Qty,
                    remd.Score,
                    remd.Rvu,
                    remd.RvuConversion,
                    remd.Multiplier,
                    remd.Coefficient
                );

            var dtb = remd.LoadDataTable();

            Common.CreateExcelFile.CreateExcelDocument(dtb, "ParamedicFeeRemunSummary.xls", this.Response);
        }

        private void ExportDetail() {
            var rem = new ParamedicFeeRemunByIdiQuery("rem");
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var par = new ParamedicQuery("par");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var guar = new GuarantorQuery("guar");
            var tc = new TransChargesQuery("tc");
            var su = new ServiceUnitQuery("su");
            var i = new ItemQuery("i");
            var sureg = new ServiceUnitQuery("sureg");
            var ig = new ItemGroupQuery("ig");
            var ismf = new ItemIdiItemSmfQuery("ismf");
            var idi = new ItemIdiQuery("idi");

            rem.InnerJoin(fee).On(rem.RemunID == fee.RemunByIdiID)
                .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                .InnerJoin(reg).On(fee.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                .InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                .InnerJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                .InnerJoin(i).On(fee.ItemID == i.ItemID)
                .InnerJoin(sureg).On(reg.ServiceUnitID == sureg.ServiceUnitID)
                .LeftJoin(ig).On(i.ItemGroupID == ig.ItemGroupID && i.SRItemType == ig.SRItemType)
                .LeftJoin(ismf).On(fee.ItemID == ismf.ItemID && par.SRParamedicRL1 == ismf.SmfID)
                .LeftJoin(idi).On(ismf.IdiCode == idi.IdiCode)
                .Select(
                    fee.TariffComponentID.As("Jasa ID"),
                    "<FORMAT(rem.DateEnd, 'MMM yyyy') Bulan>",
                    "<'' No>",
                    par.ParamedicName.As("Nama Dokter"),
                    par.ParamedicID.As("ID Dokter"),
                    pat.MedicalNo.As("No RM"),
                    pat.PatientName.As("Nama Pasien"),
                    guar.GuarantorName.As("Jaminan"),
                    su.ServiceUnitName.As("Unit"),
                    fee.TransactionNo.As("Order ID"),
                    "<FORMAT(tc.ExecutionDate, 'yyyy-MM-dd hh:mm:ss') as [Tgl Order]>",
                    fee.ItemID.As("Kode Layanan"),
                    i.ItemName.As("Nama Layanan"),
                    fee.PriceItem.As("Tarif"),
                    sureg.ServiceUnitName.As("Unit Kunjungan"),
                    ig.ItemGroupName.As("Jenis Layanan"),
                    "<'' [ICD 10]>",
                    ismf.IdiCode.As("Acuan IDI"),
                    idi.IdiName.As("Nama Tindakan IDI"),
                    "<'' [ICD 9 CM]>",
                    (idi.F1 + idi.F21 + idi.F22 + idi.F23 + idi.F3 + idi.F4).Coalesce("0").As("Skor"),
                    idi.Rvu.Coalesce("0").As("Rvu"),

                    fee.Qty.As("Qty"),
                    fee.TransactionNoRef,
                    fee.TransactionNo,
                    fee.SequenceNo
                ).Where(rem.RemunNo == RemunNo);

            var dtb = rem.LoadDataTable();

            var corrections = dtb.AsEnumerable().Where(r => r.Field<decimal>("Qty") < 0);
            foreach (var correction in corrections) {
                var rsource = dtb.AsEnumerable().Where(r =>
                    r.Field<string>("TransactionNo") == correction["TransactionNoRef"].ToString() &&
                    r.Field<string>("SequenceNo") == correction["SequenceNo"].ToString() &&
                    r.Field<string>("Jasa ID") == correction["Jasa ID"].ToString()).FirstOrDefault();
                if (rsource != null) {
                    rsource["Qty"] = System.Convert.ToDecimal(rsource["Qty"]) + System.Convert.ToDecimal(correction["Qty"]);
                }
            }
            dtb.AcceptChanges();
            // remove <= 0
            var toRemove = dtb.AsEnumerable().Where(r => r.Field<decimal>("Qty") <= 0);
            foreach (var tr in toRemove) {
                tr.Delete();
            }
            dtb.AcceptChanges();

            dtb.Columns.Remove("SequenceNo");
            dtb.Columns.Remove("TransactionNo");
            dtb.Columns.Remove("TransactionNoRef");
            //dtb.Columns.Remove("Qty");
            dtb.AcceptChanges();

            Common.CreateExcelFile.CreateExcelDocument(dtb, "ParamedicFeeRemunDetail.xls", this.Response);
        }
    }
}
