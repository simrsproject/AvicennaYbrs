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
using Telerik.Web.UI.Calendar;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class Census : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Census;

            if (!IsPostBack)
            {
                txtCensusDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;

                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }

                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                {
                    LoadClass(cboServiceUnitID.SelectedValue);
                }
                else
                {
                    var coll = new ClassCollection();
                    coll.Query.Where
                        (
                            coll.Query.IsActive == true,
                            coll.Query.IsInPatientClass == true
                        );
                    coll.Query.OrderBy(coll.Query.ClassID.Ascending);
                    coll.LoadAll();

                    cboClassID.Items.Add(new RadComboBoxItem("-All-", string.Empty));
                    foreach (Class c in coll)
                    {
                        cboClassID.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxManager.AjaxSettings.AddAjaxSetting(txtCensusDate, grdModel1, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(cboServiceUnitID, cboClassID, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(cboServiceUnitID, grdModel1, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(cboClassID, grdModel1, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(btnProcess, grdModel1, AjaxLoadingPanel);
            AjaxManager.AjaxSettings.AddAjaxSetting(grdModel1, grdModel1, AjaxLoadingPanel);
        }

        private void LoadClass(string serviceUnitId)
        {
            cboClassID.Items.Clear();

            var c = new ClassQuery("a");
            var b = new BedQuery("b");
            var r = new ServiceRoomQuery("c");

            c.es.Distinct = true;
            c.Select(
                c.ClassID,
                c.ClassName
                );
            c.InnerJoin(b).On(b.ClassID == c.ClassID);
            c.InnerJoin(r).On(r.RoomID == b.RoomID && r.ServiceUnitID == serviceUnitId);
            c.Where(c.IsActive == true, c.IsInPatientClass == true);

            var coll = c.LoadDataTable();

            cboClassID.Items.Add(new RadComboBoxItem("-All-", string.Empty));
            foreach (DataRow col in coll.Rows)
            {
                cboClassID.Items.Add(new RadComboBoxItem(col["ClassName"].ToString(), col["ClassID"].ToString()));
            }
        }

        private DataTable Census1
        {
            get
            {
                var smf = new SmfQuery("s");
                smf.Select(
                    string.Format("<'{0}' AS CensusDate>", txtCensusDate.SelectedDate.Value.ToShortDateString()),
                    string.Format("<'{0}' AS ServiceUnitID>", cboServiceUnitID.SelectedValue),
                    string.Format("<'{0}' AS ClassID>", cboClassID.SelectedValue),
                    smf.SmfID,
                    smf.SmfName,
                    string.Format(@"<CAST(ISNULL((SELECT cb.Balance FROM CensusBalance AS cb WHERE cb.CensusDate = '{0}' AND cb.ServiceUnitID = '{1}' AND cb.ClassID = '{2}' AND cb.SmfID = s.SmfID), 0) AS VARCHAR(MAX)) AS Sebelumnya>",
                                    txtCensusDate.SelectedDate.Value.AddDays(-1).ToShortDateString(), cboServiceUnitID.SelectedValue, cboClassID.SelectedValue),
                    string.Format(@"<CAST((SELECT COUNT(*) FROM PatientTransferHistory AS r WHERE r.DateOfEntry = '{0}' AND r.ServiceUnitID = '{1}' {2} AND r.SmfID = s.SmfID AND r.TransferNo = '') AS VARCHAR(MAX)) AS Masuk>",
                                    txtCensusDate.SelectedDate.Value.ToShortDateString(), cboServiceUnitID.SelectedValue,
                                    string.IsNullOrEmpty(cboClassID.SelectedValue) ? string.Empty : string.Format("AND r.ClassID = '{0}'", cboClassID.SelectedValue)),
                    "<'0' AS Pindahan>",
                    "<'0' AS Jumlah345>",
                    string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition NOT IN ('{1}', '{2}') AND r.ServiceUnitID = '{3}' {4} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Hidup>",
                                    txtCensusDate.SelectedDate.Value.ToShortDateString(), AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48,
                                    cboServiceUnitID.SelectedValue, string.IsNullOrEmpty(cboClassID.SelectedValue) ? string.Empty : string.Format("AND r.ClassID = '{0}'", cboClassID.SelectedValue)),
                    string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}', '{2}') AND r.ServiceUnitID = '{3}' {4} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Meninggal>",
                                    txtCensusDate.SelectedDate.Value.ToShortDateString(), AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48, cboServiceUnitID.SelectedValue,
                                    string.IsNullOrEmpty(cboClassID.SelectedValue) ? string.Empty : string.Format("AND r.ClassID = '{0}'", cboClassID.SelectedValue)),
                    string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}') AND r.ServiceUnitID = '{2}' {3} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Below48>",
                                    txtCensusDate.SelectedDate.Value.ToShortDateString(), AppSession.Parameter.DischargeConditionDieLessThen48, cboServiceUnitID.SelectedValue,
                                    string.IsNullOrEmpty(cboClassID.SelectedValue) ? string.Empty : string.Format("AND r.ClassID = '{0}'", cboClassID.SelectedValue)),
                    string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}') AND r.ServiceUnitID = '{2}' {3} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Over48>",
                                    txtCensusDate.SelectedDate.Value.ToShortDateString(), AppSession.Parameter.DischargeConditionDieMoreThen48, cboServiceUnitID.SelectedValue,
                                    string.IsNullOrEmpty(cboClassID.SelectedValue) ? string.Empty : string.Format("AND r.ClassID = '{0}'", cboClassID.SelectedValue)),
                    "<'0' AS Dipindahkan>",
                    "<'0' AS Jumlah7811>",
                    "<'0' AS Dirawat>"
                    );

                var tab1 = smf.LoadDataTable();

                foreach (DataRow row in tab1.Rows)
                {
                    row["Pindahan"] = SelectCountPindahan(row["CensusDate"].ToString(), row["ServiceUnitID"].ToString(), row["ClassID"].ToString(), row["SmfID"].ToString()).ToString();
                    row["Jumlah345"] = Convert.ToString(Convert.ToInt32(row["Sebelumnya"]) + Convert.ToInt32(row["Masuk"]) + Convert.ToInt32(row["Pindahan"]));
                    row["Dipindahkan"] = SelectCountDipindahkan(row["CensusDate"].ToString(), row["ServiceUnitID"].ToString(), row["ClassID"].ToString(), row["SmfID"].ToString()).ToString();
                    row["Jumlah7811"] = Convert.ToString(Convert.ToInt32(row["Hidup"]) + Convert.ToInt32(row["Meninggal"]) + Convert.ToInt32(row["Dipindahkan"]));
                    row["Dirawat"] = Convert.ToString(Convert.ToInt32(row["Jumlah345"]) - Convert.ToInt32(row["Jumlah7811"]));
                }

                tab1.AcceptChanges();

                foreach (DataRow row in tab1.Rows)
                {
                    for (int i = 2; i < tab1.Columns.Count; i++)
                    {
                        row[i] = row[i].ToString() == "0" ? string.Empty : row[i];
                    }
                }

                tab1.AcceptChanges();

                return tab1;
            }
        }

        public static DataTable SelectMasuk(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");
            var c = new ClassQuery("d");

            var t = new PatientTransferHistoryQuery("e");

            t.Select(
                r.RegistrationNo,
                p.MedicalNo,
                t.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                s.SmfName,
                c.ClassName
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

        public static int SelectCountPindahan(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var t = new PatientTransferQuery("t");
            var h = new PatientTransferHistoryQuery("h");
            var r = new RegistrationQuery("a");
            //var p = new PatientQuery("b");
            //var s = new SmfQuery("c");

            t.Select(
                //r.RegistrationNo,
                //p.MedicalNo,
                //r.BedID,
                //"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                //s.SmfName,

                t.FromServiceUnitID,
                t.ToServiceUnitID,
                t.FromClassID,
                t.ToClassID
                );
            t.InnerJoin(h).On(t.RegistrationNo == h.RegistrationNo && t.TransferNo == h.TransferNo);
            t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            //t.InnerJoin(p).On(r.PatientID == p.PatientID);
            //t.InnerJoin(s).On(r.SmfID == s.SmfID);
            t.Where(
                t.TransferDate == censusDate,
                t.ToServiceUnitID == serviceUnitID,
                //t.ToSpecialtyID == smfID,
                h.SmfID == smfID,
                t.IsApprove == true,
                r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.ToClassID == classID);

            var tab = t.LoadDataTable();

            return tab.Rows.Count;
        }

        public static DataTable SelectPindahan(string censusDate, string serviceUnitID, string classID, string smfID)
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
                r.RegistrationNo,
                p.MedicalNo,
                t.ToBedID,//r.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                t.FromServiceUnitID,
                u1.ServiceUnitName.As("FromServiceUnitName"),
                t.ToServiceUnitID,
                u2.ServiceUnitName.As("ToServiceUnitName"),
                t.FromClassID,
                c1.ClassName.As("FromClassName"),
                t.ToClassID,
                c2.ClassName.As("ToClassName"),
                t.FromBedID,
                t.ToBedID,
                s1.SmfName.As("FromSmfName"),
                s2.SmfName.As("ToSmfName")
                );
            t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            t.InnerJoin(th).On(t.RegistrationNo == th.RegistrationNo && t.TransferNo == th.TransferNo);
            t.InnerJoin(p).On(r.PatientID == p.PatientID);

            t.InnerJoin(c1).On(t.FromClassID == c1.ClassID);
            t.InnerJoin(c2).On(t.ToClassID == c2.ClassID);

            t.InnerJoin(u1).On(t.FromServiceUnitID == u1.ServiceUnitID);
            t.InnerJoin(u2).On(t.ToServiceUnitID == u2.ServiceUnitID);

            t.InnerJoin(s1).On(t.FromSpecialtyID == s1.SmfID);
            //t.InnerJoin(s2).On(t.ToSpecialtyID == s2.SmfID);
            t.InnerJoin(s2).On(th.SmfID == s2.SmfID);

            t.Where(
                t.TransferDate == censusDate,
                t.ToServiceUnitID == serviceUnitID,
                th.SmfID == smfID,
                //t.ToSpecialtyID == smfID,
                t.IsApprove == true,
                r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.ToClassID == classID);

            return t.LoadDataTable();
        }

        public static DataTable SelectHidup(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");
            var c = new ClassQuery("d");

            r.Select(
                r.RegistrationNo,
                p.MedicalNo,
                r.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                s.SmfName,
                c.ClassName
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
                r.RegistrationNo,
                p.MedicalNo,
                r.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                t.FromServiceUnitID,
                u1.ServiceUnitName.As("FromServiceUnitName"),
                t.ToServiceUnitID,
                u2.ServiceUnitName.As("ToServiceUnitName"),
                t.FromClassID,
                c1.ClassName.As("FromClassName"),
                t.ToClassID,
                c2.ClassName.As("ToClassName"),
                t.FromBedID,
                t.ToBedID,
                s1.SmfName.As("FromSmfName"),
                s2.SmfName.As("ToSmfName")
                );
            t.InnerJoin(th).On(t.RegistrationNo == th.RegistrationNo && t.TransferNo == th.TransferNo);
            t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            t.InnerJoin(p).On(r.PatientID == p.PatientID);

            t.InnerJoin(s1).On(t.FromSpecialtyID == s1.SmfID);
            //t.InnerJoin(s2).On(t.ToSpecialtyID == s2.SmfID);
            t.InnerJoin(s2).On(th.SmfID == s2.SmfID);

            t.InnerJoin(c1).On(t.FromClassID == c1.ClassID);
            t.InnerJoin(c2).On(t.ToClassID == c2.ClassID);

            t.InnerJoin(u1).On(t.FromServiceUnitID == u1.ServiceUnitID);
            t.InnerJoin(u2).On(t.ToServiceUnitID == u2.ServiceUnitID);

            t.Where(
                t.TransferDate == censusDate,
                t.FromServiceUnitID == serviceUnitID,
                t.FromSpecialtyID == smfID,
                t.IsApprove == true,
                r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.FromClassID == classID);

            return t.LoadDataTable();
        }

        public static int SelectCountDipindahkan(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var t = new PatientTransferQuery("t");
            var th = new PatientTransferHistoryQuery("th");
            var r = new RegistrationQuery("a");
            //var p = new PatientQuery("b");
            //var s = new SmfQuery("c");

            t.Select(
                //r.RegistrationNo,
                //p.MedicalNo,
                //r.BedID,
                //"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                //s.SmfName,

                t.FromServiceUnitID,
                t.ToServiceUnitID,
                t.FromClassID,
                t.ToClassID
                );
            t.InnerJoin(th).On(t.RegistrationNo == th.RegistrationNo && t.TransferNo == th.TransferNo);
            t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            //t.InnerJoin(p).On(r.PatientID == p.PatientID);
            //t.InnerJoin(s).On(r.SmfID == s.SmfID);
            t.Where(
                t.TransferDate == censusDate,
                t.FromServiceUnitID == serviceUnitID,
                t.FromSpecialtyID == smfID,
                t.IsApprove == true,
                r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.FromClassID == classID);

            var tab = t.LoadDataTable();

            //foreach (DataRow row in tab.AsEnumerable().Where(d => d.Field<string>("FromServiceUnitID") == d.Field<string>("ToServiceUnitID") &&
            //                                                      d.Field<string>("FromClassID") == d.Field<string>("ToClassID")))
            //{
            //    row.Delete();
            //}

            //tab.AcceptChanges();

            return tab.Rows.Count;
        }

        public static DataTable SelectMeninggal(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");
            var c = new ClassQuery("d");

            r.Select(
                r.RegistrationNo,
                p.MedicalNo,
                r.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                s.SmfName,
                string.Format("<(CASE WHEN a.SRDischargeCondition = '{0}' THEN '< 48 Jam' ELSE '> 48 Jam' END) AS Condition>", AppSession.Parameter.DischargeConditionDieLessThen48),
                c.ClassName
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

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            using (var trans = new esTransactionScope())
            {
                var cbs = new CensusBalanceCollection();
                cbs.Query.Where(
                    cbs.Query.CensusDate == txtCensusDate.SelectedDate.Value.Date,
                    cbs.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                    cbs.Query.ClassID == cboClassID.SelectedValue
                    );
                cbs.Query.Load();

                cbs.MarkAllAsDeleted();
                cbs.Save();

                foreach (GridDataItem dataItem in grdModel1.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => !string.IsNullOrEmpty((dataItem.FindControl("txtSebelumnya") as RadTextBox).Text) ||
                                                                                                                         dataItem["Dirawat"].Text != "&nbsp;"))
                {
                    //prev
                    var cb = new CensusBalance();
                    if (!cb.LoadByPrimaryKey(txtCensusDate.SelectedDate.Value.AddDays(-1).Date, cboServiceUnitID.SelectedValue, cboClassID.SelectedValue, dataItem["SmfID"].Text))
                    {
                        cb = new CensusBalance();
                        cb.CensusDate = txtCensusDate.SelectedDate.Value.AddDays(-1).Date;
                        cb.ServiceUnitID = cboServiceUnitID.SelectedValue;
                        cb.ClassID = cboClassID.SelectedValue;
                        cb.SmfID = dataItem["SmfID"].Text;
                    }
                    cb.Balance = int.Parse(string.IsNullOrEmpty((dataItem["Sebelumnya"].FindControl("txtSebelumnya") as RadTextBox).Text) ? "0" : (dataItem["Sebelumnya"].FindControl("txtSebelumnya") as RadTextBox).Text);
                    cb.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    cb.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    cb.Save();

                    //next
                    cb = new CensusBalance();
                    if (!cb.LoadByPrimaryKey(txtCensusDate.SelectedDate.Value.Date, cboServiceUnitID.SelectedValue, cboClassID.SelectedValue, dataItem["SmfID"].Text))
                    {
                        cb = new CensusBalance();
                        cb.CensusDate = txtCensusDate.SelectedDate.Value.Date;
                        cb.ServiceUnitID = cboServiceUnitID.SelectedValue;
                        cb.ClassID = cboClassID.SelectedValue;
                        cb.SmfID = dataItem["SmfID"].Text;
                    }
                    cb.Balance = (int.Parse(string.IsNullOrEmpty((dataItem["Sebelumnya"].FindControl("txtSebelumnya") as RadTextBox).Text) ? "0" : (dataItem["Sebelumnya"].FindControl("txtSebelumnya") as RadTextBox).Text) +
                        (int.Parse(Helper.TrimHTML(dataItem["Masuk"].Text) == string.Empty ? "0" : dataItem["Masuk"].Text) +
                        int.Parse(Helper.TrimHTML(dataItem["Pindahan"].Text) == string.Empty ? "0" : dataItem["Pindahan"].Text))) -
                        (int.Parse(Helper.TrimHTML(dataItem["Hidup"].Text) == string.Empty ? "0" : dataItem["Hidup"].Text) +
                        int.Parse(Helper.TrimHTML(dataItem["Meninggal"].Text) == string.Empty ? "0" : dataItem["Meninggal"].Text) +
                        int.Parse(Helper.TrimHTML(dataItem["Dipindahkan"].Text) == string.Empty ? "0" : dataItem["Dipindahkan"].Text));
                    cb.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    cb.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    if (!string.IsNullOrEmpty(cboClassID.SelectedValue))
                    {
                        var bed = new NumberOfBed();
                        bed.Query.es.Top = 1;
                        bed.Query.Where(
                            bed.Query.StartingDate <= txtCensusDate.SelectedDate,
                            bed.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                            bed.Query.ClassID == cboClassID.SelectedValue
                            );
                        bed.Query.OrderBy(bed.Query.StartingDate.Descending);

                        cb.NumberOfBed = bed.Query.Load() ? bed.NumberOfBed : 0;
                    }
                    else
                        cb.NumberOfBed = 0;
                    cb.Save();
                }

                trans.Complete();
            }

            grdModel1.Rebind();
        }

        int sebelumnya, masuk, pindahan, jumlah1, hidup, meninggal, below48, over48, dipindahkan, jumlah2, dirawat;

        protected void grdModel1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                sebelumnya += int.Parse(Helper.TrimHTML(dataItem["Sebelumnya"].Text) == string.Empty ? "0" : dataItem["Sebelumnya"].Text);
                masuk += int.Parse(Helper.TrimHTML(dataItem["Masuk"].Text) == string.Empty ? "0" : dataItem["Masuk"].Text);
                pindahan += int.Parse(Helper.TrimHTML(dataItem["Pindahan"].Text) == string.Empty ? "0" : dataItem["Pindahan"].Text);
                jumlah1 += int.Parse(Helper.TrimHTML(dataItem["Jumlah345"].Text) == string.Empty ? "0" : dataItem["Jumlah345"].Text);
                hidup += int.Parse(Helper.TrimHTML(dataItem["Hidup"].Text) == string.Empty ? "0" : dataItem["Hidup"].Text);
                meninggal += int.Parse(Helper.TrimHTML(dataItem["Meninggal"].Text) == string.Empty ? "0" : dataItem["Meninggal"].Text);
                below48 += int.Parse(Helper.TrimHTML(dataItem["Below48"].Text) == string.Empty ? "0" : dataItem["Below48"].Text);
                over48 += int.Parse(Helper.TrimHTML(dataItem["Over48"].Text) == string.Empty ? "0" : dataItem["Over48"].Text);
                dipindahkan += int.Parse(Helper.TrimHTML(dataItem["Dipindahkan"].Text) == string.Empty ? "0" : dataItem["Dipindahkan"].Text);
                jumlah2 += int.Parse(Helper.TrimHTML(dataItem["Jumlah7811"].Text) == string.Empty ? "0" : dataItem["Jumlah7811"].Text);
                dirawat += int.Parse(Helper.TrimHTML(dataItem["Dirawat"].Text) == string.Empty ? "0" : dataItem["Dirawat"].Text);
            }
            if (e.Item is GridFooterItem)
            {
                GridFooterItem footerItem = (GridFooterItem)e.Item;
                (footerItem["TemplateColumn1"].FindControl("txtSumSebelumnya") as RadTextBox).Text = sebelumnya == 0 ? string.Empty : sebelumnya.ToString();
                footerItem["Sebelumnya"].Text = sebelumnya == 0 ? string.Empty : sebelumnya.ToString();
                footerItem["Masuk"].Text = masuk == 0 ? string.Empty : masuk.ToString();
                footerItem["Pindahan"].Text = pindahan == 0 ? string.Empty : pindahan.ToString();
                footerItem["Jumlah345"].Text = jumlah1 == 0 ? string.Empty : jumlah1.ToString();
                footerItem["Hidup"].Text = hidup == 0 ? string.Empty : hidup.ToString();
                footerItem["Meninggal"].Text = meninggal == 0 ? string.Empty : meninggal.ToString();
                footerItem["Below48"].Text = below48 == 0 ? string.Empty : below48.ToString();
                footerItem["Over48"].Text = over48 == 0 ? string.Empty : over48.ToString();
                footerItem["Dipindahkan"].Text = dipindahkan == 0 ? string.Empty : dipindahkan.ToString();
                footerItem["Jumlah7811"].Text = jumlah2 == 0 ? string.Empty : jumlah2.ToString();
                footerItem["Dirawat"].Text = dirawat == 0 ? string.Empty : dirawat.ToString();
            }
        }

        protected void grdModel1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdModel1.DataSource = Census1;
        }

        protected void grdModel1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
                ((GridDataItem)e.Item).ChildItem.FindControl("InnerContainer").Visible = !e.Item.Expanded;
        }

        protected void txtCensusDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            //if (!txtCensusDate.IsEmpty) btnProcess_Click(null, null);

            grdModel1.Rebind();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //btnProcess_Click(null, null);

            if (e.OldValue == e.Value) return;


            LoadClass(e.Value);

            grdModel1.Rebind();

        }

        protected void cboClassID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //btnProcess_Click(null, null);

            grdModel1.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            switch (eventArgument)
            {
                case "form1":
                    btnProcess_Click(null, null);
                    Print(AppConstant.Report.ResumeSensusHarian);
                    break;
                case "form2":
                    btnProcess_Click(null, null);
                    Print(AppConstant.Report.SensusHarianPenderitaDirawat);
                    break;
                case "rebind":
                    grdModel1.Rebind();
                    break;
            }
        }

        private void Print(string reportName)
        {
            var jobParameters = new PrintJobParameterCollection();

            switch (reportName)
            {
                case AppConstant.Report.ResumeSensusHarian:
                case AppConstant.Report.SensusHarianPenderitaDirawat:
                    var jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "censusDate";
                    jobParameter.ValueString = txtCensusDate.SelectedDate.Value.ToShortDateString();

                    jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "serviceUnitID";
                    jobParameter.ValueString = cboServiceUnitID.SelectedValue;

                    jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "classID";
                    jobParameter.ValueString = cboClassID.SelectedValue;

                    jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "below48";
                    jobParameter.ValueString = AppSession.Parameter.DischargeConditionDieLessThen48;

                    jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "over48";
                    jobParameter.ValueString = AppSession.Parameter.DischargeConditionDieMoreThen48;
                    break;
            }

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = reportName;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }
    }
}
