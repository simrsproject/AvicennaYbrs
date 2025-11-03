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
using Temiang.Avicenna.Module.RADT.Emr.MainContent;

namespace Temiang.Avicenna.Module.Emr
{
    public partial class ExamOrderLabHist : BasePageDialog
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
                this.Title = string.Format("Exam Order History Of : {0} (MRN: {1})", pat.PatientName, pat.MedicalNo);
            }

        }
        protected void btnQueryLabResult_Click(object sender, ImageClickEventArgs e)
        {
            Session["dtbLabResult"] = null;
            Session["dtbLabResult_pid"] = string.Empty;
            //grdLabHist.Rebind();
        }
        private List<string> _patientRelateds;
        protected List<string> PatientRelateds
        {
            get
            {
                if (_patientRelateds == null)
                {
                    _patientRelateds = Patient.PatientRelateds(PatientID);
                }

                return _patientRelateds;
            }
        }
        protected void grdLaboratory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //if (!IsPostBack)
            //    grdLaboratory.DataSource = string.Empty;
            //else
                grdLaboratory.DataSource = ExamOrderHistCtl.TransChargesDataTable("LAB", PatientID, PatientRelateds, null, true);
        }
        protected void grdLaboratory_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //var dataItem = e.Item as GridDataItem;
                var grdResult = (RadGrid)e.Item.FindControl("grdLaboratoryResult");

                // InitializeCultureGrid manual krn tidak terjangkau oleh fungsi di basepage
                grdResult.InitializeCultureGrid();

                // Set Datasource
                string transactionNo;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "LINK_LIS":
                        transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                        var labNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ResultValue"]);
                        grdResult.DataSource = ExamOrderHistCtl.LaboratoryResult(RegistrationNo, transactionNo, labNo);
                        break;
                    default:
                        transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                        grdResult.DataSource = ExamOrderHistCtl.LaboratoryResult(RegistrationNo, transactionNo);
                        break;
                }

                grdResult.Rebind();
            }

        }

    }
}
