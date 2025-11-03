using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.RSCH
{
    public partial class PrevBuyDialog : BasePageDialog
    {
        public string ItemID
        {
            get
            {
                return Request.QueryString["itemid"];
            }
        }

        public string ItemInterventionID
        {
            get
            {
                return Request.QueryString["itemiid"];
            }
        }

        public string PatientID
        {
            get
            {
                var r = new Registration();
                if (r.LoadByPrimaryKey(Request.QueryString["rno"]))
                    return r.PatientID;
                return string.Empty;
            }
        }

        public string PrescriptionNo
        {
            get
            {
                return Request.QueryString["pno"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DateTime pdate;
            var prescHd = new TransPrescription();
            if (prescHd.LoadByPrimaryKey(PrescriptionNo))
                pdate = prescHd.PrescriptionDate ?? DateTime.Now.Date;
            else
                pdate = DateTime.Now.Date;

            var tp = new TransPrescriptionQuery("tp");
            var tpi = new TransPrescriptionItemQuery("tpi");
            var reg = new RegistrationQuery("r1");
            var i = new ItemQuery("i");

            tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);
            tp.InnerJoin(reg).On(tp.RegistrationNo == reg.RegistrationNo && reg.PatientID == PatientID);
            tp.InnerJoin(i).On(tpi.ItemID == i.ItemID);
            tp.Where(tpi.ItemID.In(new string[] { ItemID, ItemInterventionID }),
                    tp.IsApproval == true,
                    tp.IsPrescriptionReturn == false,
                    tp.IsVoid == false,
                    tp.PrescriptionNo != PrescriptionNo, 
                    tp.PrescriptionDate <= pdate);
            tp.OrderBy(tp.PrescriptionDate.Descending, tp.LastUpdateDateTime.Descending);
            tp.Select(tp.PrescriptionDate, tpi.PrescriptionNo, tpi.SequenceNo, i.ItemID, i.ItemName, tpi.TakenQty, tpi.SRItemUnit,
                    "<ISNULL(tpi.DaysOfUsage,0) DaysOfUsage>", tpi.SRConsumeMethod, tpi.ConsumeQty, tpi.SRConsumeUnit);

            tp.es.Top = 10;
            var dttbl = tp.LoadDataTable();

            grdList.DataSource = dttbl;
        }
    }
}