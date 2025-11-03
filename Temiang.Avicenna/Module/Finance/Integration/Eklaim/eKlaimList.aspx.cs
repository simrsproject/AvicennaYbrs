using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Integration.Eklaim
{
    public partial class eKlaimList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.InacbgProcess;

            if (!IsPostBack)
            {
                if (!AppSession.Parameter.IsDefaultEmptyDateOnEKlaimList)
                {
                    txtStart.SelectedDate = DateTime.Now;
                    txtEnd.SelectedDate = DateTime.Now;
                }

                StandardReference.Initialize(cboJenisPelayanan, AppEnum.StandardReference.BpjsTypeOfService);

                btnExportLog.Visible = AppSession.Parameter.HealthcareInitial == "RSI";
            }
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                var svc = new Common.Inacbg.v51.Service();
                var response = svc.Print(new Common.Inacbg.v51.Claim.Create.Data() { nomor_sep = e.CommandArgument.ToString() });
                if (response.Metadata.IsValid)
                {
                    var base64 = response.Data;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "print", "openPrint('" + base64 + "');", true);
                }
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            GetDataPatient = null;
            grdList.Rebind();
        }

        private DataTable GetDataPatient
        {
            get
            {
                if (ViewState["klaim"] == null)
                {
                    var pat = new PatientQuery("a");
                    var reg = new RegistrationQuery("b");
                    var unit = new ServiceUnitQuery("c");
                    var par = new ParamedicQuery("d");
                    var grr = new GuarantorQuery("e");
                    var ncc = new NccInacbgQuery("f");

                    pat.Select(
                        pat.PatientID,
                        pat.MedicalNo,
                        @"<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) + 
' [' + CASE WHEN a.Sex = 'F' THEN 'P' ELSE 'L' END + '] [' +
CAST(b.AgeInYear AS VARCHAR(MAX)) + 'th]' AS [PatientName]>",
                        pat.DateOfBirth,
                        reg.GuarantorCardNo,
                        reg.BpjsSepNo,
                        reg.RegistrationDate,
                        "<CASE WHEN b.SRRegistrationType = 'IPR' THEN b.DischargeDate ELSE ISNULL(b.DischargeDate, b.RegistrationDate) END AS DischargeDate>",
                        unit.ServiceUnitName,
                        par.ParamedicName,
                        grr.GuarantorName,
                        reg.RegistrationNo,
                        "<CASE WHEN f.RegistrationNo IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS [Status]>",
                        //"<CAST(1 AS BIT) AS [Status]>",
                        ncc.CbgID,
                        ncc.CbgName,
                        ncc.CbgSentStatus,
                        ncc.CbgStatus,
                        "<'' AS Base64Str>",
                        reg.IsNewPatient
                        );
                    pat.InnerJoin(reg).On(pat.PatientID == reg.PatientID);
                    pat.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                    pat.InnerJoin(par).On(reg.ParamedicID == par.ParamedicID);
                    pat.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    pat.LeftJoin(ncc).On(reg.RegistrationNo == ncc.RegistrationNo);

                    var isEmptyFilter = string.IsNullOrEmpty(txtNoMR.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(txtNoKartu.Text) &&
                        string.IsNullOrEmpty(txtNoNik.Text) && txtStart.IsEmpty && txtEnd.IsEmpty && string.IsNullOrEmpty(txtNoSep.Text);

                    if (isEmptyFilter)
                    {
                        pat.Where(pat.PatientID == "UnKnownXXXX9999");
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(txtNoMR.Text)) pat.Where(string.Format("<REPLACE(a.MedicalNo, '-', '') = '{0}'>", txtNoMR.Text.Replace("-", string.Empty)));
                        if (!string.IsNullOrEmpty(txtNoMR.Text))
                        {
                            if (AppSession.Parameter.IsMedicalNoContainStrip)
                                pat.Where(
                                    pat.Or(
                                        pat.MedicalNo == txtNoMR.Text,
                                        string.Format("< OR REPLACE(a.MedicalNo, '-', '') LIKE '%{0}%'>", txtNoMR.Text, pat.es.JoinAlias)
                                        )
                                    );
                            else
                                pat.Where(
                                    pat.Or(
                                        pat.MedicalNo == txtNoMR.Text,
                                        string.Format("< OR a.MedicalNo LIKE '%{0}%'>", txtNoMR.Text, pat.es.JoinAlias)
                                    )
                                );
                        }
                        if (!string.IsNullOrEmpty(txtPatientName.Text))
                        {
                            string searchPatient = "%" + txtPatientName.Text + "%";
                            pat.Where(string.Format("<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '{0}'>", searchPatient));
                        }
                        if (!string.IsNullOrEmpty(txtNoKartu.Text)) pat.Where(reg.GuarantorCardNo == txtNoKartu.Text);
                        if (!string.IsNullOrEmpty(txtNoNik.Text)) pat.Where(pat.Ssn == txtNoNik.Text);
                        if (!txtStart.IsEmpty && !txtEnd.IsEmpty)
                        {
                            if (rblTglPulang.SelectedValue == "01") pat.Where(reg.RegistrationDate.Between(txtStart.SelectedDate.Value.Date, txtEnd.SelectedDate.Value.Date));
                            else
                            {
                                if (cboJenisPelayanan.SelectedValue == "1") pat.Where(reg.DischargeDate.Between(txtStart.SelectedDate.Value.Date, txtEnd.SelectedDate.Value.Date));
                                else pat.Where(reg.RegistrationDate.Between(txtStart.SelectedDate.Value.Date, txtEnd.SelectedDate.Value.Date));
                            }
                        }
                        if (!string.IsNullOrEmpty(cboJenisPelayanan.SelectedValue))
                        {
                            string[] list = null;
                            if (cboJenisPelayanan.SelectedValue == "1") list = new string[1] { AppConstant.RegistrationType.InPatient };
                            else list = new string[2] { AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient };
                            pat.Where(reg.SRRegistrationType.In(list));
                        }
                        if (!string.IsNullOrEmpty(txtNoSep.Text)) pat.Where($"<b.BpjsSepNo LIKE '%{txtNoSep.Text}%'>");

                        pat.Where(reg.GuarantorID.In(GuarantorBPJS), reg.IsVoid == false, reg.IsFromDispensary == false, reg.IsConsul == false);
                    }

                    var dtb = pat.LoadDataTable();

                    //foreach (DataRow row in dtb.AsEnumerable().Where(d => !string.IsNullOrEmpty(d.Field<string>("BpjsSepNo"))))
                    //{
                    //    //if (string.IsNullOrEmpty(row["BpjsSepNo"].ToString())) continue;
                    //    //try
                    //    //{
                    //    //    var ncc = new NccInacbg();
                    //    //    if (!ncc.LoadByPrimaryKey(row["RegistrationNo"].ToString())) continue;
                    //    var service = new Common.Inacbg.v51.Service();
                    //    //    var response = service.GetDetail(new Common.Inacbg.v51.Claim.Get.GetDetail.Data() { nomor_sep = row["BpjsSepNo"].ToString() });
                    //    //    if (response.Metadata.IsValid)
                    //    //    {
                    //    //        row["Status"] = false;
                    //    //        if (response.DataResponse.Data.Grouper.Response != null) row["Cbg"] = response.DataResponse.Data.Grouper.Response.Cbg.Code;
                    //    //        row["CbgStatus"] = response.DataResponse.Data.KlaimStatusCd;
                    //    //        row["SentStatus"] = response.DataResponse.Data.KemenkesDcStatusCd;
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.Metadata.Code, response.Metadata.Message));
                    //    //    }
                    //    //}
                    //    //catch (Exception e) { }

                    //    //service = new Common.Inacbg.v51.Service();
                    //    var print = service.Print(new Common.Inacbg.v51.Claim.Create.Data() { nomor_sep = row["BpjsSepNo"].ToString() });
                    //    if (print.Metadata.IsValid) row["Base64Str"] = print.Data;
                    //}

                    ViewState["klaim"] = dtb;
                    return dtb;
                }
                else return ViewState["klaim"] as DataTable;
            }
            set { ViewState["klaim"] = value; }
        }

        private DataTable GetHistoryDataPatient(string patientID)
        {
            var reg = new RegistrationQuery("a");
            var unit = new ServiceUnitQuery("b");
            var medic = new ParamedicQuery("c");
            var ncc = new NccInacbgQuery("e");
            var bill = new CostCalculationQuery("f");
            var grr = new GuarantorQuery("g");

            reg.Select(
                reg.RegistrationNo,
                reg.RegistrationDate,
                "<CASE WHEN a.SRRegistrationType = 'IPR' THEN a.DischargeDate ELSE ISNULL(a.DischargeDate, a.RegistrationDate) END AS DischargeDate>",
                reg.BpjsSepNo,
                grr.GuarantorName,
                unit.ServiceUnitName,
                medic.ParamedicName,
                (ncc.CoverageAmount.Coalesce("0") + ncc.SpecialDrugAmount.Coalesce("0") + ncc.SpecialProcedureAmount.Coalesce("0")).As("Total"),
                (bill.PatientAmount.Coalesce("0") + bill.GuarantorAmount.Coalesce("0")).Sum().As("BillingAmount"),
                ncc.CbgID,
                ncc.CbgName,
                ncc.CbgSentStatus,
                ncc.CbgStatus
                );
            reg.LeftJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            reg.LeftJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.LeftJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            reg.LeftJoin(ncc).On(reg.RegistrationNo == ncc.RegistrationNo);
            reg.LeftJoin(bill).On(reg.RegistrationNo == bill.RegistrationNo);
            reg.Where(reg.PatientID == patientID, reg.IsVoid == false);
            reg.OrderBy(reg.RegistrationDate.Descending);
            reg.GroupBy(
                reg.RegistrationNo,
                reg.RegistrationDate,
                reg.SRRegistrationType,
                reg.DischargeDate,
                reg.BpjsSepNo,
                grr.GuarantorName,
                unit.ServiceUnitName,
                medic.ParamedicName,
                ncc.CoverageAmount,
                ncc.SpecialDrugAmount,
                ncc.SpecialProcedureAmount,
                ncc.CbgID,
                ncc.CbgName,
                ncc.CbgSentStatus,
                ncc.CbgStatus
                );

            return reg.LoadDataTable();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = GetDataPatient;
        }

        private string[] GuarantorBPJS
        {
            get
            {
                if (ViewState["bpjs"] != null) return (string[])ViewState["bpjs"];
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.INACBG.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) ViewState["bpjs"] = grr.Select(g => g.GuarantorID).ToArray();
                else ViewState["bpjs"] = new string[] { string.Empty };

                return (string[])ViewState["bpjs"];
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument.Split('|')[0] == "rebind")
            {
                var row = GetDataPatient.AsEnumerable().Where(d => d.Field<string>("RegistrationNo") == eventArgument.Split('|')[1]).SingleOrDefault();
                if (row != null)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(eventArgument.Split('|')[1]);

                    row["GuarantorCardNo"] = reg.GuarantorCardNo;
                    row["BpjsSepNo"] = reg.BpjsSepNo;

                    if (!string.IsNullOrEmpty(row["BpjsSepNo"].ToString()))
                    {
                        var service = new Common.Inacbg.v51.Service();
                        var response = service.GetDetail(new Common.Inacbg.v51.Claim.Get.GetDetail.Data() { nomor_sep = row["BpjsSepNo"].ToString() });
                        if (response.Metadata.IsValid)
                        {
                            row["Status"] = false;
                            if (response.DataResponse.Data.Grouper.Response != null) row["CbgID"] = response.DataResponse.Data.Grouper.Response.Cbg.Code;
                            row["CbgStatus"] = response.DataResponse.Data.KlaimStatusCd;
                            row["CbgSentStatus"] = response.DataResponse.Data.KemenkesDcStatusCd;
                        }
                    }

                    GetDataPatient.AcceptChanges();
                    grdList.Rebind();
                }
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = GetHistoryDataPatient(e.DetailTableView.ParentItem.GetDataKeyValue("PatientID").ToString());
        }

        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            if (txtStart.IsEmpty || txtEnd.IsEmpty) return;

            var reg = new RegistrationQuery("a");
            var ncc = new NccInacbgQuery("b");
            var pat = new PatientQuery("c");

            reg.Select(reg.SRRegistrationType.As("RegistrationType"), pat.MedicalNo, reg.RegistrationNo, pat.PatientName, reg.RegistrationDate.Date(), reg.DischargeDate.Date(), ncc.CoverageAmount.As("TotalCbg"), ncc.CbgID, ncc.CbgName, ncc.CbgStatus, ncc.CbgSentStatus, ncc.LastUpdateByUserID, ncc.LastUpdateDateTime);
            reg.InnerJoin(ncc).On(reg.RegistrationNo == ncc.RegistrationNo && ncc.CoverageAmount > 0 && ncc.CbgID.IsNotNull());
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(reg.RegistrationDate.Between(txtStart.SelectedDate?.Date, txtEnd.SelectedDate?.Date));
            reg.OrderBy(reg.SRRegistrationType.Ascending, reg.RegistrationDate.Ascending);

            var table = reg.LoadDataTable();

            CreateExcelFile.CreateExcelDocument(table, $"Eklaim-{txtStart.SelectedDate?.ToString("yyyyMMdd")}-{txtEnd.SelectedDate?.ToString("yyyyMMdd")}.xlsx", this.Response);
        }

        protected void btnExportLog_OnClick(object sender, ImageClickEventArgs e)
        {
            var log = new WebServiceAPILogQuery("a");
            var reg = new RegistrationQuery("b");
            log.Select(log);
            log.InnerJoin(reg).On(log.UrlAddress == reg.RegistrationNo);
            log.Where(log.IPAddress == "EklaimGroupper", reg.RegistrationDate.Between(txtStart.SelectedDate?.Date, txtEnd.SelectedDate?.Date));
            log.OrderBy(log.DateRequest.Descending);

            var table = log.LoadDataTable();

            CreateExcelFile.CreateExcelDocument(table, $"EklaimLogOtomatisasi-{txtStart.SelectedDate?.ToString("yyyyMMdd")}-{txtEnd.SelectedDate?.ToString("yyyyMMdd")}.xlsx", this.Response);
        }
    }
}