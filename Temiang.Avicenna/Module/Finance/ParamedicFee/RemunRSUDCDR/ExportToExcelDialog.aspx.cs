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

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.RemunRSUDCDR
{
    public partial class ExportToExcelDialog : BasePageDialog
    {
        public bool IsBPJS
        {
            get
            {
                return Request.QueryString["type"] == "BPJS";
            }
        }

        private string RemunNo
        {
            get
            {
                return Request.QueryString["rno"];
            }
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";

            }

            ExportDetail();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        private void ExportDetail() {
            var dtb = new DataTable();

            var rem = new ServiceFeeRemunRsucdrCollection();
            rem.Query.Where(rem.Query.RemunNo == RemunNo);
            if (rem.LoadAll()) {
                if (IsBPJS)
                {
                    dtb = rem.GetForExportExcelBPJS(rem.First().RemunID.Value);
                }
                else {
                    dtb = rem.GetForExportExcelNonBPJS(rem.First().RemunID.Value);
                }
            }


            Common.CreateExcelFile.CreateExcelDocument(dtb, "ParamedicFeeRemunDetail.xls", this.Response);

            //var corrections = dtb.AsEnumerable().Where(r => r.Field<decimal>("Qty") < 0);
            //foreach (var correction in corrections) {
            //    var rsource = dtb.AsEnumerable().Where(r =>
            //        r.Field<string>("TransactionNo") == correction["TransactionNoRef"].ToString() &&
            //        r.Field<string>("SequenceNo") == correction["SequenceNo"].ToString() &&
            //        r.Field<string>("Jasa ID") == correction["Jasa ID"].ToString()).FirstOrDefault();
            //    if (rsource != null) {
            //        rsource["Qty"] = System.Convert.ToDecimal(rsource["Qty"]) + System.Convert.ToDecimal(correction["Qty"]);
            //    }
            //}
            //dtb.AcceptChanges();
            //// remove <= 0
            //var toRemove = dtb.AsEnumerable().Where(r => r.Field<decimal>("Qty") <= 0);
            //foreach (var tr in toRemove) {
            //    tr.Delete();
            //}
            //dtb.AcceptChanges();

            //dtb.Columns.Remove("SequenceNo");
            //dtb.Columns.Remove("TransactionNo");
            //dtb.Columns.Remove("TransactionNoRef");
            //dtb.Columns.Remove("Qty");
            //dtb.AcceptChanges();

            
        }
    }
}
