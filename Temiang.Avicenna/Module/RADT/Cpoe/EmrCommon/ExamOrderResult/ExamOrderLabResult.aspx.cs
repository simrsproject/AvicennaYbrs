using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Emr
{
    public partial class ExamOrderLabResult : BasePageDialog
    {
        private string _patientID;
        protected string PatientID
        {
            get
            {
                // Jangan ambil dari QueryString krn bisa jadi utk PatientID yg berbeda tetapi masih pasien yg sama (PatientRelated)
                //return Request.QueryString["patid"];
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    _patientID = reg.PatientID;
                }
                else
                    _patientID = Request.QueryString["patid"];

                return _patientID;
            }
        }
        protected string RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        private string TransactionNo
        {
            get { return Request.QueryString["trno"]; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;


            var pat = new Patient();
            if (pat.LoadByPrimaryKey(PatientID))
            {
                this.Title = string.Format("Lab Result Order No: {0} - {1} (MRN: {2})", TransactionNo, pat.PatientName, pat.MedicalNo);
            }

        }
        protected void btnQueryLabResult_Click(object sender, ImageClickEventArgs e)
        {
            Session["dtbLabResult"] = null;
            Session["dtbLabResult_pid"] = string.Empty;
            grdLabHist.Rebind();
        }
        protected void grdLabHist_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //if (!IsPostBack || Session["dtbLabResult_pid"] == null) return;
            DataTable dtb;
            if (Session["dtbLabResult"] == null || !TransactionNo.Equals(Session["dtbLabResult_trno"]))
            {
                dtb = LabHistOrderResult();
                Session["dtbLabResult"] = dtb;
                Session["dtbLabResult_trno"] = TransactionNo;
            }
            else
                dtb = (DataTable)Session["dtbLabResult"];

            grdLabHist.DataSource = dtb;


        }
        private DataTable LabHistOrderResult()
        {
            if (AppSession.Parameter.IsUsingHisInterop)
            {
                DataTable dtbResult;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "SYSMEX":
                    case "LINK_LIS":
                        dtbResult = LabHistOrderResultFromSysmex();
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry();
                        return dtbResult;
                    case "RSCH":
                        dtbResult = LabHistOrderResultFromRSCH();
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry();
                        return dtbResult;
                    default:
                        return LabHistOrderResultFromManualEntry();
                }
            }

            return LabHistOrderResultFromManualEntry();
        }

        private static DataTable LabHistOrderResultBlank()
        {
            var dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("OrderLabNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("LabOrderCode", typeof(string)));
            dtb.Columns.Add(new DataColumn("TEST_GROUP", typeof(string)));
            dtb.Columns.Add(new DataColumn("LabOrderSummary", typeof(string)));
            dtb.Columns.Add(new DataColumn("Result", typeof(string)));
            dtb.Columns.Add(new DataColumn("StandarValue", typeof(string)));
            dtb.Columns.Add(new DataColumn("OrderLabTglOrder", typeof(DateTime)));

            return dtb;
        }

        private DataTable LabHistOrderResultFromSysmex()
        {
            var sysmexHasil = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("hp");
            sysmexHasil.Where(sysmexHasil.OrderLabNo == TransactionNo);

            if (!string.IsNullOrEmpty(txtExamName.Text))
            {
                sysmexHasil.Where(sysmexHasil.LabOrderSummary.Like("%" + txtExamName.Text + "%"));
            }

            sysmexHasil.OrderBy(sysmexHasil.DispSeq.Ascending);
            return sysmexHasil.LoadDataTable();
        }
        private DataTable LabHistOrderResultFromRSCH()
        {
            var qr = new BusinessObject.Interop.RSCH.VwHasilPasienQuery("hp");

            qr.Select(qr.OrderLabNo,qr.OrderLabTglOrder,qr.CheckupResultGroupName.As("TEST_GROUP"), qr.CheckupResultFractionName.As("LabOrderSummary"), 
                qr.OutRange.As("Result"),qr.StandarValue, qr.CheckupResultFractionCode.As("LabOrderCode")
                );

            qr.Where(qr.OrderLabNo == TransactionNo);

            if (!string.IsNullOrEmpty(txtExamName.Text))
            {
                qr.Where(qr.OrderLabNama.Like("%" + txtExamName.Text + "%"));
            }

            qr.OrderBy(qr.Seq.Ascending);
            return qr.LoadDataTable();
        }
        private DataTable LabHistOrderResultFromManualEntry()
        {
// Ambil data dari ItemLaboratoryDetail
            var qr = new TransChargesItemQuery("dt");
            var order = new TransChargesQuery("hd");
            qr.InnerJoin(order).On(qr.TransactionNo == order.TransactionNo);

            var item = new ItemQuery("i");
            qr.InnerJoin(item).On(qr.ItemID == item.ItemID);

            var itemGroup = new ItemGroupQuery("g");
            qr.InnerJoin(itemGroup).On(item.ItemGroupID == itemGroup.ItemGroupID);

            qr.Select(qr.TransactionNo.As("OrderLabNo"), qr.ItemID.As("LabOrderCode"), item.ItemName.As("LabOrderSummary"),
                qr.ResultValue.As("Result"),
                order.TransactionDate.As("OrderLabTglOrder"), itemGroup.ItemGroupName.As("TEST_GROUP"));

            qr.Where(qr.TransactionNo == TransactionNo);

            var dtbTransChargesItem = qr.LoadDataTable();
            dtbTransChargesItem.Columns.Add(new DataColumn("StandarValue", typeof(string)));

            // Isi StandarValue
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);

            var ageInDays = (reg.RegistrationDate - patient.DateOfBirth).Value.TotalDays;

            foreach (DataRow row in dtbTransChargesItem.Rows)
            {
                var stdval = new ItemLaboratoryDetailQuery();
                stdval.Where(stdval.ItemID == row["LabOrderCode"].ToString());
                stdval.Where(stdval.Sex == patient.Sex);
                stdval.Where(stdval.TotalAgeMin <= ageInDays && stdval.TotalAgeMax >= ageInDays);
                var dtbStdVal = stdval.LoadDataTable();
                if (dtbStdVal.Rows.Count > 0)
                {
                    try
                    {
                        // Test is numeric value
                        var normalValueMin = Convert.ToDecimal(dtbStdVal.Rows[0]["NormalValueMin"]);
                        var normalValueMax = Convert.ToDecimal(dtbStdVal.Rows[0]["NormalValueMax"]);

                        // if no error
                        row["StandarValue"] = string.Format("{0} - {1}", dtbStdVal.Rows[0]["NormalValueMin"],
                            dtbStdVal.Rows[0]["NormalValueMax"]);
                    }
                    catch
                    {
                        row["StandarValue"] = dtbStdVal.Rows[0]["NormalValueMin"];
                    }
                }
            }

            return dtbTransChargesItem;

            var test = new TestResultQuery("a");
            var tci = new TransChargesItemQuery("b");
            test.InnerJoin(tci).On(test.TransactionNo == tci.TransactionNo && test.ItemID == tci.ItemID && tci.IsVoid == false);

            var med = new ParamedicQuery("c");
            test.InnerJoin(med).On(test.ParamedicID == med.ParamedicID);

            test.InnerJoin(item).On(test.ItemID == item.ItemID);

            var tc = new TransChargesQuery("e");
            test.InnerJoin(tc).On(test.TransactionNo == tc.TransactionNo && tc.IsVoid == false);

            test.Where(test.TransactionNo == TransactionNo);
            test.Select
                (
                    test.TransactionNo,
                    test.TestResultDateTime,
                    med.ParamedicName,
                    test.ItemID,
                    item.ItemName,
                    test.TestResult
                );
            test.OrderBy(test.TestResultDateTime.Descending);

            return test.LoadDataTable();

        }
    }
}
