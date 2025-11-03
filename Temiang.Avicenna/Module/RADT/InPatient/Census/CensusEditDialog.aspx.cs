using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class CensusEditDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Census;

            if (IsPostBack) return;

            DateTime date = DateTime.Parse(Request.QueryString["date"]);

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(Request.QueryString["unit"]);

            var cls = new Class();
            if (!string.IsNullOrEmpty(Request.QueryString["cls"])) cls.LoadByPrimaryKey(Request.QueryString["cls"]);

            var smf = new Smf();
            smf.LoadByPrimaryKey(Request.QueryString["smf"]);

            Title = string.Format("Census Editor {0} : {1} - {2} - {3} - {4}", CensusType, date.ToShortDateString(), unit.ServiceUnitName,
                string.IsNullOrEmpty(Request.QueryString["cls"]) ? string.Empty : cls.ClassName, smf.SmfName);

            grdModel1.Columns[grdModel1.Columns.Count - 1].Visible = (Request.QueryString["id"] == "2" || Request.QueryString["id"] == "4");
        }

        private string CensusType
        {
            get
            {
                switch (Request.QueryString["id"])
                {
                    case "1":
                        return "Pasien Masuk";
                    case "2":
                        return "Pasien Pindahan";
                    case "3":
                        return "Pasien Keluar Hidup";
                    case "4":
                        return "Pasien Dipindahkan";
                    case "5":
                        return "Pasien Meninggal";
                    default:
                        return string.Empty;
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            using (var trans = new esTransactionScope())
            {
                foreach (GridDataItem item in grdModel1.MasterTableView.Items)
                {
                    switch (Request.QueryString["id"])
                    {
                        case "1": //pasien masuk
                            var msk = new PatientTransferHistory();
                            msk.LoadByPrimaryKey(item["RegistrationNo"].Text, string.Empty);
                            msk.SmfID = (item["TemplateColumn2"].FindControl("cboSmf") as RadComboBox).SelectedValue;
                            msk.Save();
                            break;
                        case "2": //pasien pindahan
                            var pindah = new PatientTransfer();
                            pindah.LoadByPrimaryKey(item["TransferNo"].Text);
                            pindah.ToSpecialtyID = (item["TemplateColumn2"].FindControl("cboSmf") as RadComboBox).SelectedValue;
                            pindah.Save();

                            var pindahhist = new PatientTransferHistory();
                            pindahhist.LoadByPrimaryKey(item["RegistrationNo"].Text, item["TransferNo"].Text);
                            pindahhist.SmfID = (item["TemplateColumn2"].FindControl("cboSmf") as RadComboBox).SelectedValue;
                            pindahhist.Save();
                            break;
                        case "3"://pasien keluar hidup
                        case "5"://pasien meninggal
                            var reg = new Registration();
                            reg.LoadByPrimaryKey(item["RegistrationNo"].Text);
                            reg.SmfID = (item["TemplateColumn2"].FindControl("cboSmf") as RadComboBox).SelectedValue;
                            reg.Save();
                            break;
                        case "4"://pasien dipindahkan
                            var dpindah = new PatientTransfer();
                            dpindah.LoadByPrimaryKey(item["TransferNo"].Text);
                            dpindah.FromSpecialtyID = (item["TemplateColumn2"].FindControl("cboSmf") as RadComboBox).SelectedValue;
                            dpindah.Save();
                            break;
                    }
                }

                trans.Complete();
            }

            return true;
        }

        protected void grdModel1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            switch (Request.QueryString["id"])
            {
                case "1":
                    grdModel1.DataSource = SelectMasuk(Request.QueryString["date"], Request.QueryString["unit"], Request.QueryString["cls"], Request.QueryString["smf"]);
                    break;
                case "2":
                    grdModel1.DataSource = SelectPindahan(Request.QueryString["date"], Request.QueryString["unit"], Request.QueryString["cls"], Request.QueryString["smf"]);
                    break;
                case "3":
                    grdModel1.DataSource = SelectHidup(Request.QueryString["date"], Request.QueryString["unit"], Request.QueryString["cls"], Request.QueryString["smf"]);
                    break;
                case "4":
                    grdModel1.DataSource = SelectDipindahkan(Request.QueryString["date"], Request.QueryString["unit"], Request.QueryString["cls"], Request.QueryString["smf"]);
                    break;
                case "5":
                    grdModel1.DataSource = SelectMeninggal(Request.QueryString["date"], Request.QueryString["unit"], Request.QueryString["cls"], Request.QueryString["smf"]);
                    break;
            }
        }

        protected void grdModel1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                RadComboBox radcombo = (RadComboBox)e.Item.FindControl("cboSmf");
                radcombo.DataSource = GetSmf;
                radcombo.DataTextField = "SmfName";
                radcombo.DataValueField = "SmfID";
                radcombo.DataBind();
                radcombo.SelectedValue = e.Item.Cells[5].Text;
            }
        }

        public DataTable GetSmf
        {
            get
            {
                if (ViewState["Smf"] == null)
                {
                    var smf = new SmfQuery();
                    ViewState["Smf"] = smf.LoadDataTable();
                }
                return (DataTable)ViewState["Smf"];
            }
        }

        private static DataTable SelectMasuk(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var t = new PatientTransferHistoryQuery("e");
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");
            var c = new ClassQuery("d");

            t.Select(
                r.RegistrationNo,
                "<a.RegistrationNo + '<br />' + b.MedicalNo + '<br />' + e.BedID AS Col0>",
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) + '<br />' + d.ClassName AS Col1>",
                s.SmfID
                );
            t.InnerJoin(r).On(r.RegistrationNo == t.RegistrationNo);
            t.InnerJoin(p).On(r.PatientID == p.PatientID);
            t.InnerJoin(s).On(t.SmfID == s.SmfID);
            t.InnerJoin(c).On(t.ClassID == c.ClassID);
            t.Where(
                t.TransferNo == string.Empty,
                t.DateOfEntry == censusDate,
                t.ServiceUnitID == serviceUnitID,
                t.SmfID == smfID,
                r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.ClassID == classID);
            return t.LoadDataTable();
        }

        private static DataTable SelectPindahan(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var t = new PatientTransferQuery("t");
            var th = new PatientTransferHistoryQuery("th");
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");

            var s1 = new SmfQuery("c");
            var s2 = new SmfQuery("h");

            var c1 = new ClassQuery("d");
            var c2 = new ClassQuery("e");

            var u1 = new ServiceUnitQuery("f");
            var u2 = new ServiceUnitQuery("g");

            t.Select(
                t.TransferNo,
                r.RegistrationNo,
                "<a.RegistrationNo + '<br />' + b.MedicalNo + '<br />' + a.BedID AS Col0>",
                @"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) + '<br />' + 
                   f.ServiceUnitName + ' -> ' + g.ServiceUnitName + '<br />' + 
                   d.ClassName + ' -> ' + e.ClassName + '<br />' + 
                   t.FromBedID + ' -> ' + t.ToBedID + '<br />' + 
                   c.SmfName + ' -> ' + h.SmfName AS Col1>",
                th.SmfID.As("SmfID")
                );
            t.InnerJoin(th).On(t.RegistrationNo == th.RegistrationNo && t.TransferNo == th.TransferNo);
            t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            t.InnerJoin(p).On(r.PatientID == p.PatientID);

            t.InnerJoin(c1).On(t.FromClassID == c1.ClassID);
            t.InnerJoin(c2).On(t.ToClassID == c2.ClassID);

            t.InnerJoin(u1).On(t.FromServiceUnitID == u1.ServiceUnitID);
            t.InnerJoin(u2).On(t.ToServiceUnitID == u2.ServiceUnitID);

            t.InnerJoin(s1).On(t.FromSpecialtyID == s1.SmfID);
            t.InnerJoin(s2).On(th.SmfID == s2.SmfID);

            t.Where(
                t.TransferDate == censusDate,
                t.ToServiceUnitID == serviceUnitID,
                th.SmfID == smfID,
                t.IsApprove == true
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.ToClassID == classID);

            return t.LoadDataTable();
        }

        private static DataTable SelectHidup(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");
            var c = new ClassQuery("d");

            r.Select(
                r.RegistrationNo,
                "<a.RegistrationNo + '<br />' + b.MedicalNo + '<br />' + a.BedID AS Col0>",
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) + '<br />' + d.ClassName AS Col1>",
                s.SmfID
                );
            r.InnerJoin(p).On(r.PatientID == p.PatientID);
            r.InnerJoin(s).On(r.SmfID == s.SmfID);
            r.InnerJoin(c).On(r.ClassID == c.ClassID);
            r.Where(
                r.DischargeDate == censusDate,
                r.SRDischargeCondition.NotIn(AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48),
                r.ServiceUnitID == serviceUnitID,
                r.SmfID == smfID
                );
            if (!string.IsNullOrEmpty(classID)) r.Where(r.ClassID == classID);
            return r.LoadDataTable();
        }

        public static DataTable SelectDipindahkan(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var t = new PatientTransferQuery("t");
            var th = new PatientTransferHistoryQuery("th");
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");

            var s1 = new SmfQuery("c");
            var s2 = new SmfQuery("h");

            var c1 = new ClassQuery("d");
            var c2 = new ClassQuery("e");

            var u1 = new ServiceUnitQuery("f");
            var u2 = new ServiceUnitQuery("g");

            t.Select(
                t.TransferNo,
                r.RegistrationNo,
                "<a.RegistrationNo + '<br />' + b.MedicalNo + '<br />' + a.BedID AS Col0>",
                @"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) + '<br />' + 
                   f.ServiceUnitName + ' -> ' + g.ServiceUnitName + '<br />' + 
                   d.ClassName + ' -> ' + e.ClassName + '<br />' + 
                   t.FromBedID + ' -> ' + t.ToBedID + '<br />' + 
                   c.SmfName + ' -> ' + h.SmfName AS Col1>",
                t.FromSpecialtyID.As("SmfID")
                );
            t.InnerJoin(th).On(t.RegistrationNo == th.RegistrationNo && t.TransferNo == th.TransferNo);
            t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            t.InnerJoin(p).On(r.PatientID == p.PatientID);

            t.InnerJoin(s1).On(t.FromSpecialtyID == s1.SmfID);
            t.InnerJoin(s2).On(th.SmfID == s2.SmfID);

            t.InnerJoin(c1).On(t.FromClassID == c1.ClassID);
            t.InnerJoin(c2).On(t.ToClassID == c2.ClassID);

            t.InnerJoin(u1).On(t.FromServiceUnitID == u1.ServiceUnitID);
            t.InnerJoin(u2).On(t.ToServiceUnitID == u2.ServiceUnitID);

            t.Where(
                t.TransferDate == censusDate,
                t.FromServiceUnitID == serviceUnitID,
                t.FromSpecialtyID == smfID,
                t.IsApprove == true
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.FromClassID == classID);

            return t.LoadDataTable();
        }

        public static DataTable SelectMeninggal(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");
            var c = new ClassQuery("d");

            r.Select(
                r.RegistrationNo,
                "<a.RegistrationNo + '<br />' + b.MedicalNo + '<br />' + a.BedID AS Col0>",
                @"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) + '<br />' + 
                   d.ClassName + '<br />' + 
                   (CASE WHEN a.SRDischargeCondition = '" + AppSession.Parameter.DischargeConditionDieLessThen48 + @"' THEN '< 48 Jam' ELSE '> 48 Jam' END)
                   AS Col1>",
                s.SmfID
                );
            r.InnerJoin(p).On(r.PatientID == p.PatientID);
            r.InnerJoin(s).On(r.SmfID == s.SmfID);
            r.InnerJoin(c).On(r.ClassID == c.ClassID);
            r.Where(
                r.DischargeDate == censusDate,
                r.SRDischargeCondition.In(AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48),
                r.ServiceUnitID == serviceUnitID,
                r.SmfID == smfID
                );
            if (!string.IsNullOrEmpty(classID)) r.Where(r.ClassID == classID);
            return r.LoadDataTable();
        }

        protected void grdModel1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            using (var trans = new esTransactionScope())
            {
                var pt = new PatientTransfer();
                pt.LoadByPrimaryKey(item["TransferNo"].Text);
                pt.IsApprove = false;
                pt.IsVoid = true;
                pt.Save();

                if (!string.IsNullOrEmpty(item["TransferNo"].Text))
                {
                    var transhist = new PatientTransferHistory();
                    transhist.LoadByPrimaryKey(item["RegistrationNo"].Text, item["TransferNo"].Text);
                    
                    var thbefore = new PatientTransferHistoryQuery();
                    thbefore.es.Top = 1;
                    thbefore.Where(thbefore.RegistrationNo == item["RegistrationNo"].Text,
                                         thbefore.TransferNo < item["TransferNo"].Text);
                    thbefore.OrderBy(thbefore.TransferNo.Descending);
                    DataTable dtbbefore = thbefore.LoadDataTable();
                    
                    var thafter = new PatientTransferHistoryQuery();
                    thafter.es.Top = 1;
                    thafter.Where(thafter.RegistrationNo == item["RegistrationNo"].Text,
                                         thafter.TransferNo > item["TransferNo"].Text);
                    thafter.OrderBy(thafter.TransferNo.Ascending);
                    DataTable dtbafter = thafter.LoadDataTable();

                    if (transhist.DateOfExit.HasValue)
                    {
                        // jika ada data selanjutnya, update date of entry sesuai dg data yg mo dihapus
                        if (dtbafter.Rows.Count > 0)
                        {
                            var th = new PatientTransferHistory();
                            th.LoadByPrimaryKey(item["RegistrationNo"].Text, dtbafter.Rows[0]["TransferNo"].ToString());
                            th.DateOfEntry = transhist.DateOfEntry;
                            th.TimeOfEntry = transhist.TimeOfEntry;
                            th.Save();

                            var t = new PatientTransfer();
                            t.LoadByPrimaryKey(dtbafter.Rows[0]["TransferNo"].ToString());
                            t.TransferDate = transhist.DateOfEntry;
                            t.TransferTime = transhist.TimeOfEntry;
                            t.Save();
                        }
                        else
                        {
                            if (dtbbefore.Rows.Count > 0)
                            {
                                var th = new PatientTransferHistory();
                                th.LoadByPrimaryKey(item["RegistrationNo"].Text, dtbbefore.Rows[0]["TransferNo"].ToString());
                                th.DateOfExit = transhist.DateOfExit;
                                th.TimeOfExit = transhist.TimeOfExit;
                                th.Save();

                                var r = new Registration();
                                r.LoadByPrimaryKey(item["RegistrationNo"].Text);
                                r.ServiceUnitID = dtbbefore.Rows[0]["ServiceUnitID"].ToString();
                                r.RoomID = dtbbefore.Rows[0]["RoomID"].ToString();
                                r.BedID = dtbbefore.Rows[0]["BedID"].ToString();
                                r.ClassID = dtbbefore.Rows[0]["ClassID"].ToString();
                                r.ChargeClassID = dtbbefore.Rows[0]["ChargeClassID"].ToString();
                                r.SmfID = dtbbefore.Rows[0]["SmfID"].ToString();
                                r.Save();
                            }
                        }
                    }
                    else
                    {
                        if (dtbbefore.Rows.Count > 0)
                        {
                            var th = new PatientTransferHistory();
                            th.LoadByPrimaryKey(item["RegistrationNo"].Text, dtbbefore.Rows[0]["TransferNo"].ToString());
                            th.DateOfExit = null;
                            th.TimeOfExit = null;
                            th.Save();

                            var r = new Registration();
                            r.LoadByPrimaryKey(item["RegistrationNo"].Text);
                            r.ServiceUnitID = dtbbefore.Rows[0]["ServiceUnitID"].ToString();
                            r.RoomID = dtbbefore.Rows[0]["RoomID"].ToString();
                            r.BedID = dtbbefore.Rows[0]["BedID"].ToString();
                            r.ClassID = dtbbefore.Rows[0]["ClassID"].ToString();
                            r.ChargeClassID = dtbbefore.Rows[0]["ChargeClassID"].ToString();
                            r.SmfID = dtbbefore.Rows[0]["SmfID"].ToString();
                            r.Save();
                        }
                    }

                    transhist.MarkAsDeleted();
                    transhist.Save();
                }

                trans.Complete();
            }

        }
    }
}
