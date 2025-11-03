using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;
using DevExpress.Web.Data.Internal;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.MainContent
{
    public partial class ExamOrderHistCtl : BaseMainContentCtl
    {

        private bool? _isLaboratoryOfficer = null;
        public bool IsLaboratoryOfficer
        {
            get
            {
                if (_isLaboratoryOfficer == null)
                {
                    var ausColl = new AppUserServiceUnitCollection();
                    if (ausColl.LoadByUserID(AppSession.UserLogin.UserID))
                    {
                        _isLaboratoryOfficer = ausColl.Where(a => a.ServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID ||
                        AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(a.ServiceUnitID)).Any();
                    }
                    else
                        _isLaboratoryOfficer = false;
                }

                return _isLaboratoryOfficer ?? false;
            }
        }

        private static ItemLaboratoryCollection _iItemLaboratoryConfidentials = null;
        private static ItemLaboratoryCollection ItemLaboratoryConfidentials
        {
            get
            {
                if (_iItemLaboratoryConfidentials == null)
                {
                    var itemLab = new ItemLaboratoryCollection();
                    itemLab.Query.Select(itemLab.Query.ItemID);
                    itemLab.Query.Where(itemLab.Query.IsConfidential == true);
                    itemLab.LoadAll();
                    _iItemLaboratoryConfidentials = itemLab;
                }

                return _iItemLaboratoryConfidentials;
            }
        }

        public string GrdRadiologyClientID
        {
            get { return grdRadiology.ClientID; }
        }

        public string GrdExamOrderOtherClientID
        {
            get { return grdExamOrderOther.ClientID; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            grdRadiology.Columns.FindByUniqueName("INTIWID").Display = new string[] { "INTIWID", "GERSTJ", "EMPIRIS" }.Contains(AppSession.Parameter.GetParameterValueString(AppParameter.ParameterItem.RisPacsInteropVendor));
            grdRadiology.Columns.FindByUniqueName("ELVA").Display = new string[] { "ELVA" }.Contains(AppSession.Parameter.GetParameterValueString(AppParameter.ParameterItem.RisPacsInteropVendor));

            //mainRadTabStrip.Tabs[2].Visible = !AppSession.Parameter.IsPathologyAnatomyWithTestResult;
            //mainRadTabStrip.Tabs[3].Visible = AppSession.Parameter.IsPathologyAnatomyWithTestResult;
            // -db:20231028, ganti settingan, lihat dari aktif tidaknya modul PA
            // jika aktif, hasil ambil dari penginputan di modul PA
            // jika tidak, hasil ambil dari penginpuran di form test result
            mainRadTabStrip.Tabs[2].Visible = AppSession.Application.IsMenuPathologyAnatomyActive;
            mainRadTabStrip.Tabs[3].Visible = !AppSession.Application.IsMenuPathologyAnatomyActive;

            if (!IsPostBack)
            {
                //Reset supaya load ulang setiap buka page baru
                _iItemLaboratoryConfidentials = null;
            }
        }

        public String ReferFromRegistrationNo
        {
            set { ViewState["freg"] = value; }
            get { return Convert.ToString(ViewState["freg"]); }
        }

        public string GridLaboratoryClientID
        {
            get { return grdLaboratory.ClientID; }
        }
        public RadGrid GridLaboratory
        {
            get { return grdLaboratory; }
        }
        public RadGrid GridRadiology
        {
            get { return grdRadiology; }
        }
        public RadGrid GridPathology
        {
            get { return grdPathology; }
        }
        public RadGrid GridPathology2
        {
            get { return grdPathology2; }
        }
        public RadGrid GridExamOrderOther
        {
            get { return grdExamOrderOther; }
        }
        public void GridLaboratoryHistDatabind()
        {
            grdLaboratory.DataBind();
        }


        //protected bool IsUserCanNotAddExamOrder()
        //{
        //    if (this.IsUserAddAble.Equals(false)) return true;

        //    if (!AppSession.Parameter.IsExamOrderCanEntryByUserNonPhsycian && string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID)) return true;

        //    if (AppSession.Parameter.IsExamOrderCanEntryByUserNonPhsycian || AppSession.UserLogin.ParamedicID.Equals(ParamedicID)) return false;

        //    return !IsUserInParamedicTeam();
        //}
        protected bool IsUserAccessExamOrder(string registrationNo, bool isPostBack)
        {
            if (this.IsUserAddAble.Equals(false)) return false;
            return true;
        }

        #region Job Order



        protected void grdJobOrder_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "TransactionNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.JobOrderNotesDiagnostic;

                ShowPrintPreview();

            }
            else if (e.CommandName == "MarkAsRead")
            {
                if (AppSession.UserLogin.SRUserType == AppUser.UserType.Doctor)
                {
                    var tc = new TransCharges();
                    if (tc.LoadByPrimaryKey(e.CommandArgument.ToString()))
                    {
                        using (var trans = new esTransactionScope())
                        {
                            tc.ResultReadByPhysicianID = AppSession.UserLogin.UserID;
                            tc.ResultReadByPhysicianDateTime = (new DateTime()).NowAtSqlServer();
                            tc.Save();

                            var grd = (e.Item.FindControl("grdLaboratoryResult") as RadGrid);
                            foreach (GridDataItem gdi in grd.Items)
                            {
                                var isCritical = (bool)gdi.GetDataKeyValue("IsCritical");
                                if (isCritical)
                                {
                                    var tNo = gdi.GetDataKeyValue("OrderLabNo").ToString();
                                    tNo = tNo.Split('^')[0];
                                    var testId = gdi.GetDataKeyValue("LabOrderCode").ToString();

                                    var tclc = new TransChargesLaboratoryCritical();
                                    if (!tclc.LoadByPrimaryKey(tNo, testId))
                                    {
                                        tclc.AddNew();
                                        tclc.TransactionNo = tNo;
                                        tclc.LisTestID = testId;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(tclc.ReadByPhysicianID))
                                        {
                                            // sudah diset, tidak perlu diset lagi
                                            continue;
                                        }
                                    }
                                    tclc.ReadByPhysicianID = AppSession.UserLogin.UserID;
                                    tclc.ReadByPhysicianDateTime = (new DateTime()).NowAtSqlServer();
                                    tclc.Save();
                                }
                            }

                            trans.Complete();
                        }

                        grdLaboratory.Rebind();
                    }
                }
            }
            else if (e.CommandName == "MarkAsReadByNurse")
            {
                var tNo = e.CommandArgument.ToString().Split('|')[0];
                tNo = tNo.Split('^')[0];
                var testId = e.CommandArgument.ToString().Split('|')[1];
                var tclc = new TransChargesLaboratoryCritical();
                if (!tclc.LoadByPrimaryKey(tNo, testId))
                {
                    tclc.AddNew();
                    tclc.TransactionNo = tNo;
                    tclc.LisTestID = testId;
                }
                tclc.ReportedByNurseID = AppSession.UserLogin.UserID;
                tclc.ReportedByNurseDateTime = (new DateTime()).NowAtSqlServer();
                tclc.Save();

                grdLaboratory.Rebind();
            }
            else if (e.CommandName == "MarkAsReadByPhy")
            {
                var tNo = e.CommandArgument.ToString().Split('|')[0];
                tNo = tNo.Split('^')[0];
                var testId = e.CommandArgument.ToString().Split('|')[1];
                var tclc = new TransChargesLaboratoryCritical();
                if (!tclc.LoadByPrimaryKey(tNo, testId))
                {
                    tclc.AddNew();
                    tclc.TransactionNo = tNo;
                    tclc.LisTestID = testId;
                }
                tclc.ReadByPhysicianID = AppSession.UserLogin.UserID;
                tclc.ReadByPhysicianDateTime = (new DateTime()).NowAtSqlServer();
                tclc.Save();

                grdLaboratory.Rebind();
            }
            else if (e.CommandName == "MarkAsComplete")
            {
                var tNo = e.CommandArgument.ToString().Split('|')[0];
                tNo = tNo.Split('^')[0];
                var testId = e.CommandArgument.ToString().Split('|')[1];
                var tclc = new TransChargesLaboratoryCritical();
                if (!tclc.LoadByPrimaryKey(tNo, testId))
                {
                    tclc.AddNew();
                    tclc.TransactionNo = tNo;
                    tclc.LisTestID = testId;
                }
                tclc.CompletelyReportedByUserID = AppSession.UserLogin.UserID;
                tclc.CompletelyReportedDateTime = (new DateTime()).NowAtSqlServer();
                tclc.Save();

                grdLaboratory.Rebind();
            }
            else if (e.CommandName == "RebindLab")
            {
                grdLaboratory.Rebind();
            }
            else if (e.CommandName == "RebindRad")
            {
                grdRadiology.Rebind();
            }
            else if (e.CommandName == "RebindPat")
            {
                grdPathology.Rebind();
            }
            else if (e.CommandName == "RebindPat2")
            {
                grdPathology2.Rebind();
            }
            else if (e.CommandName == "RebindOth")
            {
                grdExamOrderOther.Rebind();
            }
        }
        [Obsolete("Lakukan di detil", true)]
        protected void grdJobOrder_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (!CheckAccess) return;

            var item = e.Item as GridDataItem;
            if (item == null) return;


            using (var trans = new esTransactionScope())
            {
                var entity = new TransCharges();
                if (!entity.LoadByPrimaryKey(Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["TransactionNo"]))) return;

                if (Helper.IsDeadlineEditedOver(entity.TransactionDate.Value))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Invalid void to system');", true);
                    return;
                }

                var coll = new TransChargesItemCollection();
                coll.Query.Where(coll.Query.TransactionNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["TransactionNo"]));
                coll.LoadAll();

                foreach (var c in coll)
                {
                    if (c.IsOrderRealization ?? false) continue;

                    c.IsApprove = false;
                    c.IsBillProceed = false;
                    c.IsVoid = true;
                }

                coll.Save();

                if (coll.Count() == coll.Count(c => c.IsVoid ?? false))
                {
                    if (!(entity.IsVoid ?? false))
                    {
                        entity.IsApproved = false;
                        entity.IsBillProceed = false;
                        entity.IsVoid = true;
                        entity.Save();
                    }
                }

                trans.Complete();
            }

            var grd = (RadGrid)source;
            grd.Rebind();
        }


        public static DataTable TransChargesDataTable(string transType, string patientID, List<string> patientRelateds, DateTime? toRegistrationDate, bool isAllTx = false)
        {
            // OutPatient maupun InPatient dimunculkan semua historynya (2019 09 13)
            var query = new TransChargesItemQuery("a");
            var tc = new TransChargesQuery("b");
            query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);

            var item = new ItemQuery("c");
            query.InnerJoin(item).On(query.ItemID == item.ItemID);

            var toUnit = new ServiceUnitQuery("tu");
            query.InnerJoin(toUnit).On(tc.ToServiceUnitID == toUnit.ServiceUnitID);

            var fromUnit = new ServiceUnitQuery("fu");
            query.InnerJoin(fromUnit).On(tc.FromServiceUnitID == fromUnit.ServiceUnitID);

            var reg = new RegistrationQuery("f");
            query.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);

            var patient = new PatientQuery("p");
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);

            query.Select(
                tc.RegistrationNo,
                query.TransactionNo,
                query.SequenceNo,
                tc.ExecutionDate.As("TransactionDate"),
                query.ItemID,
                item.ItemName,
                query.ChargeQuantity,
                query.Price,
                query.SRItemUnit,
                query.IsApprove,
                query.IsOrderRealization,
                query.IsVoid,
                query.IsBillProceed,
                toUnit.ServiceUnitName.As("ToServiceUnitName"),
                fromUnit.ServiceUnitName.As("fromServiceUnitName"),
                tc.ToServiceUnitID,
                tc.PhysicianSenders,
                tc.IsApproved.As("IsHdApproved"),
                tc.IsVoid.As("IsHdVoid"),
                query.CommunicationID,
                query.IsCasemixApproved,
                query.CasemixApprovedByUserID.Coalesce("'|'"),
                query.Notes,
                query.ResultValue.Coalesce("''"),
                query.CommunicationID.Coalesce("''"),
                item.SRItemType,
                patient.MedicalNo,
                query.IsCorrection,
                tc.ResultReadByPhysicianID,
                item.IsHasTestResults
                );

            //if (PatientRelateds.Count == 1)
            //    query.Where(reg.PatientID == PatientID);
            //else
            //    query.Where(reg.PatientID.In(PatientRelateds));

            //// Non Inpatient ambil beberapa (parameter) registrasi terakhir (Handono 2022-09) (info: Imel)
            //var regCount = AppParameter.GetParameterValue(AppParameter.ParameterItem.EmrHistoryRegistrationCount).ToInt();
            //var lastRegNos = Patient.Last.RegistrationNos(PatientID, regCount, RegistrationNo);
            //if (lastRegNos.Count == 1)
            //    query.Where(tc.RegistrationNo == lastRegNos[0]);
            //else
            //    query.Where(tc.RegistrationNo.In(lastRegNos));

            // Non Inpatient ambil beberapa (parameter) registrasi terakhir ternyata tidak cocok untuk pasien yg lama tidak melakukan exam order
            // sehingga dikembalikan lagi dimunculkan semua tetapi dibatasi jumlah exam ordernya (Handono 2023-03-05) (info: Imel)
            //if (PatientRelateds.Count == 1)
            //    query.Where(reg.PatientID == PatientID);
            //else
            //    query.Where(reg.PatientID.In(PatientRelateds));

            //if (_currentRegDate == null)
            //{
            //    var curReg = new Registration();
            //    curReg.LoadByPrimaryKey(RegistrationNo);
            //    _currentRegDate = curReg.RegistrationDate;
            //}

            //query.Where(reg.RegistrationDate <= _currentRegDate);

            if (patientRelateds.Count == 1)
                query.Where(reg.PatientID == patientID);
            else
                query.Where(reg.PatientID.In(patientRelateds));

            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsUseCurrentDateRegistration))
            {
                if (toRegistrationDate != null)
                    query.Where(reg.RegistrationDate <= toRegistrationDate);
            } else
            {
                var lastReg = Patient.Last.Registration(patientID);

                if (!string.IsNullOrEmpty(lastReg.RegistrationNo))
                {
                    query.Where(tc.RegistrationNo.In(AppCache.RelatedRegistrations(true, lastReg.RegistrationNo)));
                }
            }
            

            if (!isAllTx)
                query.es.Top = 50;

            query.Where
                (
                    tc.IsVoid == false,
                    tc.IsCorrection == false,
                    query.ParentNo == ""
                );

            switch (transType)
            {
                case "LAB":
                    {
                        //query.Where(tc.IsOrder == true, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID);

                        query.Where(tc.IsOrder == true,
                             query.Or(
                                        tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID,
                                        tc.ToServiceUnitID.In(AppSession.Parameter.ServiceUnitLaboratoryIdArray)
                                     )
                            );
                        break;
                    }
                case "RAD":
                    {
                        //rsmmp item cath-lab dianggap item service dan dientry bkn sbg order
                        //deby: ditambah or u/ ambil item yg memang diorder
                        if (AppSession.Parameter.HealthcareInitial != "RSMMP")
                            query.Where(tc.IsOrder == true);
                        else
                        {
                            query.Where(query.Or(tc.IsOrder == true, query.IsSendToLIS == true));
                        }

                        var others = AppSession.Parameter.ServiceUnitRadiologyIDs;
                        if (!string.IsNullOrWhiteSpace(others))
                        {
                            if (others.Contains(";"))
                                query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                    tc.ToServiceUnitID.In(others.Split(';')),
                                    tc.ToServiceUnitID.In(AppSession.Parameter.ServiceUnitRadiologyIdArray)));
                            else
                                query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                    tc.ToServiceUnitID == others,
                                    tc.ToServiceUnitID.In(AppSession.Parameter.ServiceUnitRadiologyIdArray)));
                        }
                        else
                            query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                    tc.ToServiceUnitID.In(AppSession.Parameter.ServiceUnitRadiologyIdArray)));
                        break;
                    }
                case "PAT":
                    {
                        query.Where(tc.IsOrder == true, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitPathologyAnatomyID);
                        var itemGroupPa = AppSession.Parameter.ItemGroupPathologyAnatomyID;
                        if (!string.IsNullOrWhiteSpace(itemGroupPa))
                        {
                            if (itemGroupPa.Contains(","))
                                query.Where(item.ItemGroupID.In(itemGroupPa.Split(',')));
                            else
                                query.Where(item.ItemGroupID == itemGroupPa);
                        }
                        break;
                    }
                default:
                    {
                        // List selain LAB,RAD, PAT ditambah dari ServiceUnit Transaction yg item nya memiliki result
                        //                    query.Where(
                        //tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitLaboratoryID,
                        //tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID,
                        //tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID2,
                        //tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitPathologyAnatomyID);

                        //                    var others = AppSession.Parameter.ServiceUnitRadiologyIDs;
                        //                    if (!string.IsNullOrWhiteSpace(others))
                        //                    {
                        //                        if (others.Contains(";"))
                        //                            query.Where(tc.ToServiceUnitID.NotIn(others.Split(';')));
                        //                        else
                        //                            query.Where(tc.ToServiceUnitID != others);
                        //                    }

                        var orderServiceUnits = new List<string>();
                        orderServiceUnits.Add(AppSession.Parameter.ServiceUnitLaboratoryID);
                        orderServiceUnits.Add(AppSession.Parameter.ServiceUnitRadiologyID);
                        orderServiceUnits.Add(AppSession.Parameter.ServiceUnitRadiologyID2);
                        orderServiceUnits.Add(AppSession.Parameter.ServiceUnitPathologyAnatomyID);
                        orderServiceUnits.AddRange(AppSession.Parameter.ServiceUnitLaboratoryIdArray);
                        orderServiceUnits.AddRange(AppSession.Parameter.ServiceUnitRadiologyIdArray);

                        var other = AppSession.Parameter.ServiceUnitRadiologyIDs;
                        if (!string.IsNullOrWhiteSpace(other))
                        {
                            if (other.Contains(";"))
                                orderServiceUnits.AddRange(other.Split(';'));
                            else
                                orderServiceUnits.Add(other);
                        }

                        //query.Where(
                        //    query.Or(
                        //        query.And(tc.IsOrder == true, tc.ToServiceUnitID.NotIn(orderServiceUnits)),
                        //        query.And(tc.IsOrder == false, item.IsHasTestResults == true)
                        //        )
                        //    );

                        //db:20250227 -- hanya dari Job Order atau IsHasTestResults saja yg muncul
                        query.Where(
                               tc.ToServiceUnitID.NotIn(orderServiceUnits),
                               query.Or(
                                   query.And(tc.IsOrder == true, tc.TransactionNo.Like("JO%")),
                                   item.IsHasTestResults == true
                                   )
                               );
                        break;
                    }
            }

            query.OrderBy(tc.ExecutionDate.Descending, tc.TransactionNo.Descending);
            var table = query.LoadDataTable();

            if (transType == "RAD" && AppSession.Parameter.HealthcareInitial == "RSMMP")
            {
                foreach (DataRow row in table.Rows)
                {
                    var svc = new Common.Worklist.RSMMP.Service();
                    var status = svc.GetImageStatus(row["TransactionNo"].ToString(), row["SequenceNo"].ToString(), row["ResultValue"].ToString());
                    if (!status.Status) continue;
                    var tci = new TransChargesItem();
                    if (!tci.LoadByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString())) continue;
                    if (tci.ResultValue != status.PacsStudyUid)
                    {
                        tci.ResultValue = status.PacsStudyUid;
                        tci.Save();
                    }
                    row["ResultValue"] = status.PacsStudyUid;
                }
                table.AcceptChanges();
            }

            var tcs = from t in table.AsEnumerable()
                      group t by new
                      {
                          TransactionDate = t.Field<DateTime>("TransactionDate"),
                          TransactionNo = t.Field<string>("TransactionNo")
                      }
                          into g
                      select g;


            var dtb = JobOrderTable.Clone();

            // For NovaWeb
            dtb.Columns.Add("NovaWebResultUrl", typeof(string));

            dtb.Columns.Add("ResultReadByPhysicianID", typeof(string));

            var displayTotal = AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor;
            var urlRoot = Helper.UrlRoot();
            var isRisPacsDataNotLoaded = true;
            var risPacsInteropVendor = AppSession.Parameter.GetParameterValueString(AppParameter.ParameterItem.RisPacsInteropVendor);

            var resultValueList = new List<string>();
            var dicomValueList = new List<string>();

            foreach (var p in tcs)
            {
                resultValueList = new List<string>();
                dicomValueList = new List<string>();

                int i = 0;
                double total = 0;
                var orderContent = new StringBuilder();
                //orderContent.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
                var row = dtb.NewRow();
                foreach (DataRow orderItem in table.AsEnumerable().Where(t => t.Field<string>("TransactionNo") == p.Key.TransactionNo))
                {
                    var toServiceUnitID = orderItem["ToServiceUnitID"].ToString();

                    if (i == 0)
                    {
                        //var menuView = string.Empty; // hasil sudah ditampilkan
                        //orderContent.AppendLine("<tr><td style='font-weight: bold'>");
                        //orderContent.AppendFormat("{2}: {0} - {1} {3}<br />", orderItem["TransactionNo"], Convert.ToDateTime(orderItem["TransactionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth), orderItem["ToServiceUnitName"], menuView);
                        //orderContent.AppendLine("</td></tr>");

                        //orderContent.AppendLine("<tr><td align='left'>");

                        row["IsVoid"] = orderItem["IsVoid"];
                        row["IsHdVoid"] = orderItem["IsHdVoid"];
                        row["IsHdApproved"] = orderItem["IsHdApproved"];
                        row["IsBillProceed"] = orderItem["IsBillProceed"];
                        row["ToServiceUnitName"] = orderItem["ToServiceUnitName"];
                        row["FromServiceUnitName"] = orderItem["FromServiceUnitName"];
                        row["ToServiceUnitID"] = orderItem["ToServiceUnitID"];
                        row["PhysicianSenders"] = orderItem["PhysicianSenders"];
                        row["SequenceNo"] = orderItem["SequenceNo"];
                        row["RegistrationNo"] = orderItem["RegistrationNo"];
                        row["ResultValue"] = orderItem["ResultValue"];
                        row["ResultReadByPhysicianID"] = orderItem["ResultReadByPhysicianID"];

                        if (ConfigurationManager.AppSettings["EpsUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EpsUrlLocation"])) row["EpsUrlLocation"] = ConfigurationManager.AppSettings["EpsUrlLocation"];
                        else row["EpsUrlLocation"] = string.Empty;

                        if (ConfigurationManager.AppSettings["DcomUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DcomUrlLocation"])) row["DcomUrlLocation"] = ConfigurationManager.AppSettings["DcomUrlLocation"];
                        else row["DcomUrlLocation"] = string.Empty;

                        row["IsResultAvailable"] = "0";
                    }
                    i++;

                    resultValueList.Add(ConfigurationManager.AppSettings["EpsUrlLocation"] + orderItem["ResultValue"].ToString());
                    dicomValueList.Add(ConfigurationManager.AppSettings["DcomUrlLocation"] + orderItem["ResultValue"].ToString());


                    if (transType == "RAD" && AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsUsingRisPacsInterop))
                    {
                        switch (risPacsInteropVendor)
                        {
                            case "INTIWID":
                                {
                                    if (!string.IsNullOrEmpty(orderItem["ResultValue"].ToString()))
                                    {
                                        //if (string.IsNullOrEmpty(orderItem["CommunicationID"].ToString()))
                                        {
                                            try
                                            {
                                                if (ConfigurationManager.AppSettings["EpsUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EpsUrlLocation"]))
                                                    row["EpsUrlLocation"] = string.Join(";", resultValueList.Where(r => !string.IsNullOrWhiteSpace(r)));
                                                else row["EpsUrlLocation"] = string.Empty;

                                                if (ConfigurationManager.AppSettings["DcomUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DcomUrlLocation"]))
                                                    row["DcomUrlLocation"] = string.Join(";", dicomValueList.Where(r => !string.IsNullOrWhiteSpace(r)));
                                                else row["DcomUrlLocation"] = string.Empty;

                                                //var service = new Common.Worklist.RSI.Service();
                                                //var json = service.GetJsonOrder(new Common.Worklist.RSI.Json.Order.Root()
                                                //{ uid = orderItem["ResultValue"].ToString() }, isRisPacsDataNotLoaded);

                                                isRisPacsDataNotLoaded = false;

                                                //row["IsResultAvailable"] = json.status == "APPROVED" ? "1" : "0";
                                                row["IsResultAvailable"] = "1";
                                                if (row["IsResultAvailable"].ToString() == "1")
                                                {
                                                    var tci = new TransChargesItem();
                                                    tci.LoadByPrimaryKey(orderItem["TransactionNo"].ToString(),
                                                        orderItem["SequenceNo"].ToString());
                                                    tci.CommunicationID = row["IsResultAvailable"].ToString();
                                                    tci.Save();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                        }
                                    }
                                    else row["IsResultAvailable"] = "1";
                                    break;
                                }
                            case "GERSTJ":
                                if (!string.IsNullOrEmpty(orderItem["ResultValue"].ToString()))
                                {
                                    row["IsResultAvailable"] = "1";

                                    foreach (var detail in table.AsEnumerable().Where(t => t.Field<string>("TransactionNo") == orderItem["TransactionNo"].ToString()))
                                    {
                                        var svc = new Common.Worklist.RSTJ.Service();
                                        var response = svc.GetJsonResult($"{detail["TransactionNo"].ToString()}-{detail["SequenceNo"].ToString()}");
                                        if (response == null) continue;
                                        resultValueList.Add(response.Filepdf);
                                    }

                                    if (ConfigurationManager.AppSettings["EpsUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EpsUrlLocation"]))
                                        row["EpsUrlLocation"] = string.Join(";", resultValueList.Where(r => !string.IsNullOrWhiteSpace(r)));
                                    else row["EpsUrlLocation"] = string.Empty;

                                    if (ConfigurationManager.AppSettings["DcomUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DcomUrlLocation"]))
                                        row["DcomUrlLocation"] = string.Format(ConfigurationManager.AppSettings["DcomUrlLocation"], orderItem["MedicalNo"].ToString());
                                    else row["DcomUrlLocation"] = string.Empty;

                                    row["ResultValue"] = " ";
                                    orderItem["ResultValue"] = " ";
                                }
                                else row["IsResultAvailable"] = "0";
                                break;
                            case "NOVAWEB":
                                {
                                    //novrad novaris bridging

                                    if (orderItem["SRItemType"].ToString() == ItemType.Radiology && orderItem["CommunicationID"] != DBNull.Value)
                                    {
                                        var hl7 = new HL7Message();
                                        hl7.Query.es.Top = 1;
                                        hl7.Query.Where(hl7.Query.RefferenceNo == orderItem["CommunicationID"].ToString());
                                        hl7.Query.OrderBy(hl7.Query.MessageDateTime.Descending);

                                        if (hl7.Query.Load() && !string.IsNullOrEmpty(hl7.Message))
                                        {
                                            row["NovaWebResultUrl"] = Common.HL7Helper.GetResultUrl(hl7.Message);
                                        }
                                    }
                                    break;
                                }
                            case "EMPIRIS":
                                if (!string.IsNullOrEmpty(orderItem["ResultValue"].ToString()))
                                {
                                    if (AppSession.Parameter.HealthcareInitial == "RSMMP")
                                    {
                                        var svc = new Common.Worklist.RSMMP.Service();
                                        var status = svc.GetImageStatus(orderItem["TransactionNo"].ToString(), orderItem["SequenceNo"].ToString(), orderItem["ResultValue"].ToString());
                                        row["IsResultAvailable"] = status.Status ? "1" : "0";
                                        row["ResultValue"] = orderItem["ResultValue"];
                                    }
                                    else
                                    {
                                        row["IsResultAvailable"] = "1";
                                        row["ResultValue"] = orderItem["ResultValue"];
                                    }

                                    if (ConfigurationManager.AppSettings["EpsUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EpsUrlLocation"]))
                                        row["EpsUrlLocation"] = string.Join(";", resultValueList.Where(r => !string.IsNullOrWhiteSpace(r)));
                                    else row["EpsUrlLocation"] = string.Empty;

                                    if (ConfigurationManager.AppSettings["DcomUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DcomUrlLocation"]))
                                        //row["DcomUrlLocation"] = string.Format("{0}{1}", ConfigurationManager.AppSettings["DcomUrlLocation"], orderItem["ResultValue"].ToString());
                                        row["DcomUrlLocation"] = string.Join(";", dicomValueList.Where(r => !string.IsNullOrWhiteSpace(r)));
                                    else row["DcomUrlLocation"] = string.Empty;

                                    //row["ResultValue"] = " ";
                                    orderItem["CommunicationID"] = orderItem["ResultValue"];
                                }
                                else row["IsResultAvailable"] = "1";
                                break;
                            case "INFINITT":
                                if (!string.IsNullOrEmpty(orderItem["ResultValue"].ToString()))
                                {
                                    row["IsResultAvailable"] = "1";

                                    if (ConfigurationManager.AppSettings["EpsUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EpsUrlLocation"]))
                                        row["EpsUrlLocation"] = string.Join(";", resultValueList.Where(r => !string.IsNullOrWhiteSpace(r)));
                                    else row["EpsUrlLocation"] = string.Empty;

                                    if (ConfigurationManager.AppSettings["DcomUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DcomUrlLocation"]))
                                        //row["DcomUrlLocation"] = string.Format("{0}{1}", ConfigurationManager.AppSettings["DcomUrlLocation"], orderItem["ResultValue"].ToString());
                                        row["DcomUrlLocation"] = string.Join(";", dicomValueList.Where(r => !string.IsNullOrWhiteSpace(r)));
                                    else row["DcomUrlLocation"] = string.Empty;

                                    //row["ResultValue"] = " ";
                                    orderItem["CommunicationID"] = orderItem["ResultValue"];
                                    row["ResultValue"] = orderItem["ResultValue"];
                                }
                                else row["IsResultAvailable"] = "1";
                                break;
                            case "ELVA":
                                {
                                    try
                                    {
                                        var allResultMarkup = new List<string>();
                                        var dicomValueLists = new List<string>();

                                        foreach (var detail in table.AsEnumerable().Where(t => t.Field<string>("TransactionNo") == orderItem["TransactionNo"].ToString() && t.Field<bool>("IsCorrection") == false || t.Field<bool>("IsVoid") == false && t.Field<bool>("IsOrderRealization") == true || t.Field<bool>("IsApprove") == true))
                                        {
                                            if (!string.IsNullOrEmpty(detail.Field<string>("TransactionNo")) && !string.IsNullOrEmpty(detail.Field<string>("SequenceNo")))
                                            {
                                                var svc = new Common.Worklist.RSBK.Service();
                                                string combinedString = $"{detail.Field<string>("TransactionNo")}{detail.Field<string>("SequenceNo").Substring(1)}"; //co:JO240424-00003 + 001 > JO240424-00003 01 > JO240424-0000301
                                                var dataOrder = new Common.Worklist.RSBK.DataExamOrder { accession_number = combinedString };
                                                string responseOrder = svc.GetUrllink(dataOrder);

                                                if (responseOrder == null) continue;

                                                var tci = new TransChargesItem();
                                                tci.LoadByPrimaryKey(detail["TransactionNo"].ToString(),
                                                    detail["SequenceNo"].ToString());
                                                tci.CommunicationID = responseOrder;
                                                tci.Save();

                                                dicomValueLists.Add(responseOrder);

                                                var Elvalink = ConfigurationManager.AppSettings["DcomUrlLocation"];
                                                var CombineValue = Elvalink + responseOrder;

                                                var resultMarkup = new StringBuilder();
                                                var dataReport = new Common.Worklist.RSBK.DataExamReport { accession_number = combinedString };
                                                var reportList = svc.GetReport(dataReport);

                                                foreach (var report in reportList)
                                                {
                                                    resultMarkup.AppendLine("<table>");

                                                    resultMarkup.AppendLine("<tr>");
                                                    resultMarkup.AppendLine("<td style='text-align: left; font-weight: bold; width: 100px;'>Reading Doctor</td>\t");
                                                    resultMarkup.AppendFormat("<td>: {0}</td>", report.reading_doctor1);
                                                    resultMarkup.AppendLine("</tr>");

                                                    resultMarkup.AppendLine("<tr>");
                                                    resultMarkup.AppendLine("<td style='text-align: left; font-weight: bold; width: 100px;'>Report Date</td>\t");
                                                    resultMarkup.AppendFormat("<td>: {0}</td>", report.report_date_time);
                                                    resultMarkup.AppendLine("</tr>");

                                                    resultMarkup.AppendLine("<tr>");
                                                    resultMarkup.AppendLine("<td style='text-align: left; font-weight: bold; width: 100px;'>Confirm Doctor</td>\t");
                                                    resultMarkup.AppendFormat("<td>: {0}</td>", report.confirm_doctor);
                                                    resultMarkup.AppendLine("</tr>");

                                                    string formattedReport = report.report.Replace("\r\n", "<br>");
                                                    resultMarkup.AppendLine("<tr>");
                                                    resultMarkup.AppendLine("<td style='vertical-align: top; text-align: left; font-weight: bold;'>Report Result</td>");
                                                    resultMarkup.AppendFormat("<td style='text-align: left; border: 1px solid; padding: 10px; width:500px; overflow-y: auto;'><div style='max-height: 300px; max-width:100%; white-space: break-space;'>{0}</div></td>", formattedReport);
                                                    resultMarkup.AppendLine("</tr>");

                                                    resultMarkup.AppendLine("</table> <br>");
                                                }
                                                allResultMarkup.Add(resultMarkup.ToString());
                                                row["DcomUrlLocation"] = CombineValue;
                                            }
                                            row["IsResultAvailable"] = "1";
                                        }

                                        var finalResultMarkup = string.Join(" ", allResultMarkup);

                                        row["ElvaResultValue"] = finalResultMarkup;
                                        row["ElvaResultUrl"] = string.Join(";", dicomValueLists);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error: {ex.Message}");
                                    }
                                    break;
                                }
                        }
                    }

                    var isVoid = orderItem["IsVoid"].ToBoolean();
                    var isCorrection = orderItem["IsCorrection"].ToBoolean();

                    if (AppSession.Parameter.CasemixValidationRegistrationType.Any())
                    {
                        var casemixApprovedByUserID = string.Empty;
                        if (orderItem["CasemixApprovedByUserID"] != null)
                            casemixApprovedByUserID = orderItem["CasemixApprovedByUserID"].ToString();

                        var isOrderRealization = orderItem["IsOrderRealization"].ToBoolean();
                        var isCasemixApproved = false;
                        if (orderItem["IsCasemixApproved"] != null)
                            isCasemixApproved = orderItem["IsCasemixApproved"].ToBoolean();

                        //orderContent.AppendFormat("{5} {6}{0} {1} {2}{7} <img src='{4}/Images/Toolbar/{3}' />&nbsp;<input type='image' id='image' onclick=\"alert('Notes : {9}')\" src='{4}/Images/{8}' alt='{9}' />&nbsp;&nbsp;", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                        //    (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), urlRoot, AppConstant.HtmlChar.Bullet,
                        //    !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "<strike>" : string.Empty,
                        //    !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "</strike>" : string.Empty,
                        //    !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "infoblue16.png" : string.Empty,
                        //    !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? orderItem["Notes"].ToString() : string.Empty);

                        orderContent.AppendFormat("{5} {6}{0} {1} {2}{7} <img src='{4}/Images/Toolbar/{3}' />&nbsp;<input type='image' id='image' onclick=\"alert('Notes : {9}')\" src='{4}/Images/{8}' alt='{9}' />&nbsp;&nbsp;", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                            (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), urlRoot, AppConstant.HtmlChar.Bullet,
                            (!isOrderRealization && isVoid) || (isOrderRealization && isCorrection) ? "<strike>" : string.Empty,
                            (!isOrderRealization && isVoid) || (isOrderRealization && isCorrection) ? "</strike>" : string.Empty,
                            !isOrderRealization ? (isVoid ? "infored16.png" : (!isCasemixApproved && casemixApprovedByUserID == "|" ? "infoblue16.png" : (isCasemixApproved && casemixApprovedByUserID != "|" ? "infogreen16.png" : string.Empty))) : string.Empty,
                            !isOrderRealization ? ((isVoid || (isCasemixApproved && casemixApprovedByUserID != "|")) ? orderItem["Notes"].ToString() : (!isCasemixApproved && casemixApprovedByUserID == "|" ? "Need validation by Casemix" : string.Empty)) : string.Empty);

                    }
                    else
                    {
                        //orderContent.AppendFormat("{5} {0} {1} {2} <img src='{4}/Images/Toolbar/{3}' />&nbsp;&nbsp;", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                        //    (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), urlRoot, AppConstant.HtmlChar.Bullet);

                        
                        orderContent.AppendFormat("{5} {6}{0} {1} {2}{7} <img src='{4}/Images/Toolbar/{3}' />&nbsp;&nbsp;", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                            (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), urlRoot, AppConstant.HtmlChar.Bullet,
                            (isVoid || isCorrection) ? "<strike>" : string.Empty,
                            (isVoid || isCorrection) ? "</strike>" : string.Empty);
                    }

                    // Radiology Result
                    if (toServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID ||
                        toServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2 || AppSession.Parameter.ServiceUnitRadiologyIDs.Contains(toServiceUnitID))
                    {
                        if (AppSession.UserLogin.IsRadilogyParamedic)
                            orderContent.AppendFormat(
                                "<a href=\"#\" onclick=\"javascript:examOrderRadilogyResultEdit('{0}','{1}','{2}'); return false;\"><img src=\"{3}/Images/Toolbar/edit16.png\"  alt=\"View\" /></a>",
                                orderItem["TransactionNo"], orderItem["SequenceNo"], AppSession.UserLogin.ParamedicID, urlRoot);
                        else
                            orderContent.AppendFormat("<img src=\"{0}/Images/Toolbar/edit16_d.png\"  alt=\"View\" />", urlRoot);

                        if (row["NovaWebResultUrl"] != DBNull.Value && !string.IsNullOrEmpty(row["NovaWebResultUrl"].ToString()))
                        {
                            orderContent.AppendFormat(
    "<a href=\"#\" onclick=\"javascript:openUrlInNewWindow('{0}'); return false;\"><img src=\"{1}/Images/Medical/radiology.png\"  alt=\"DICOM\" /></a>",
    row["NovaWebResultUrl"], urlRoot);
                        }
                        else if (orderItem["CommunicationID"] != DBNull.Value && !string.IsNullOrEmpty(orderItem["CommunicationID"].ToString()))
                        {
                            orderContent.AppendFormat(
                                "<a href=\"#\" onclick=\"javascript:openDicom('{0}'); return false;\"><img src=\"{1}/Images/Medical/radiology.png\"  alt=\"DICOM\" /></a>",
                                orderItem["CommunicationID"], urlRoot);
                        }

                        orderContent.Append("<br />");
                    }
                    else
                    {
                        //db:20241218 - add tombol link ke test result u/ tab other
                        if (transType == "OTH" && orderItem["IsHasTestResults"].ToBoolean() == true)
                        {
                            if (orderItem["IsBillProceed"].ToBoolean() == true)
                            {
                                var tcicQ = new TransChargesItemCompQuery();
                                tcicQ.Where(tcicQ.TransactionNo == orderItem["TransactionNo"].ToString(), tcicQ.SequenceNo == orderItem["SequenceNo"].ToString(), tcicQ.ParamedicID != "");
                                tcicQ.OrderBy(tcicQ.TariffComponentID.Ascending);
                                tcicQ.es.Top = 1;

                                var tcic = new TransChargesItemComp();
                                if (tcic.Load(tcicQ) && tcic.ParamedicID == AppSession.UserLogin.ParamedicID)
                                {
                                    orderContent.AppendFormat(
                                    "<a href=\"#\" onclick=\"javascript:examOrderOtherResultEdit('{0}','{1}','{2}'); return false;\"><img src=\"{3}/Images/Toolbar/edit16.png\" title=\"Test Result\" /></a>",
                                    orderItem["TransactionNo"], orderItem["SequenceNo"], AppSession.UserLogin.ParamedicID, urlRoot);
                                }
                                else
                                {
                                    orderContent.AppendFormat("<img src=\"{0}/Images/Toolbar/edit16_d.png\"  alt=\"View\" />", urlRoot);
                                }
                            }
                            else
                            {
                                orderContent.AppendFormat("<img src=\"{0}/Images/Toolbar/edit16_d.png\"  alt=\"View\" />", urlRoot);
                            }
                        }
                        
                        orderContent.Append("<br />");
                    }

                    total += Convert.ToDouble(orderItem["ChargeQuantity"]) * Convert.ToDouble(orderItem["Price"]);
                }

                if (displayTotal)
                    orderContent.AppendFormat("<b>{0}</b>", " (Rp. " + string.Format("{0:n2}", (total)) + ")");


                row["TransactionNo"] = p.Key.TransactionNo;
                row["JobOrderSummary"] = orderContent.ToString();
                row["TransactionDate"] = p.Key.TransactionDate;

                dtb.Rows.Add(row);
            }
            return dtb;
        }

        private static DataTable JobOrderTable
        {
            get
            {
                //if (ViewState["JobOrderTable" + Request.UserHostName] != null) return ViewState["JobOrderTable" + Request.UserHostName] as DataTable;

                var table = new DataTable();
                table.Columns.Add(new DataColumn("RegistrationNo", typeof(string)));
                table.Columns.Add(new DataColumn("TransactionNo", typeof(string)));
                table.Columns.Add(new DataColumn("SequenceNo", typeof(string)));
                table.Columns.Add(new DataColumn("JobOrderSummary", typeof(string)));
                table.Columns.Add(new DataColumn("IsVoid", typeof(bool)));
                table.Columns.Add(new DataColumn("IsHdVoid", typeof(bool)));
                table.Columns.Add(new DataColumn("IsHdApproved", typeof(bool)));
                table.Columns.Add(new DataColumn("IsBillProceed", typeof(bool)));
                table.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));
                table.Columns.Add(new DataColumn("ToServiceUnitName", typeof(string)));
                table.Columns.Add(new DataColumn("FromServiceUnitName", typeof(string)));
                table.Columns.Add(new DataColumn("ToServiceUnitID", typeof(string)));
                table.Columns.Add(new DataColumn("PhysicianSenders", typeof(string)));
                table.Columns.Add(new DataColumn("ResultValue", typeof(string)));
                table.Columns.Add(new DataColumn("EpsUrlLocation", typeof(string)));
                table.Columns.Add(new DataColumn("DcomUrlLocation", typeof(string)));
                table.Columns.Add(new DataColumn("IsResultAvailable", typeof(string)));

                //ViewState["JobOrderTable" + Request.UserHostName] = table;
                //return ViewState["JobOrderTable" + Request.UserHostName] as DataTable;
                return table;
            }
        }

        private DateTime? _currentRegDate = null;
        private DateTime CurrentRegDate
        {
            get
            {
                if (_currentRegDate == null)
                {
                    var curReg = new Registration();
                    curReg.LoadByPrimaryKey(RegistrationNo);
                    _currentRegDate = curReg.RegistrationDate;
                }

                return _currentRegDate ?? DateTime.Now;
            }
        }
        #endregion

        #region Laboratory
        protected void grdLaboratory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLaboratory.DataSource = TransChargesDataTable("LAB", PatientID, PatientRelateds, CurrentRegDate);
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
                        grdResult.DataSource = LaboratoryResult(RegistrationNo, transactionNo, labNo);
                        break;
                    default:
                        transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                        grdResult.DataSource = LaboratoryResult(RegistrationNo, transactionNo);
                        break;
                }

                if ((grdResult.DataSource as DataTable).Rows.Count == 0)
                {
                    ((grdResult.NamingContainer as GridDataItem).FindControl("lbMarkAsRead") as LinkButton).Visible = false;
                }

                grdResult.Rebind();
            }

        }

        public string GetTransactionNo(GridItem dataitem)
        {
            return (dataitem.OwnerTableView.OwnerGrid.NamingContainer as GridDataItem).GetDataKeyValue("TransactionNo").ToString();
        }
        #endregion

        #region LaboratoryResult
        private static TransChargesLaboratoryCriticalCollection GetCriticalMarkings(string transactionNo)
        {
            var tclcColl = new TransChargesLaboratoryCriticalCollection();
            tclcColl.Query.Where(tclcColl.Query.TransactionNo == transactionNo);
            tclcColl.LoadAll();
            return tclcColl;
        }
        public static DataTable LaboratoryResult(string regNo, string transactionNo)
        {
            if (AppSession.Parameter.IsUsingHisInterop)
            {
                DataTable dtbResult;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "SYSMEX":
                        dtbResult = LabHistOrderResultFromSysmex(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);
                        break;
                    case "RSCH":
                        dtbResult = LabHistOrderResultFromRSCH(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);
                        break;
                    case "VANSLAB":
                        dtbResult = LabHistOrderResultFromVanslab(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);
                        break;
                    case "VANSLITE":
                        dtbResult = LabHistOrderResultFromVanslite(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);
                        break;
                    case "WYNAKOM":
                        dtbResult = LabHistOrderResultFromWynakom(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);
                        break;
                    case "ELIMS":
                        dtbResult = LabHistOrderResultFromElims(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);
                        break;
                    default:
                        dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);
                        break;
                }
                // add critical markings
                AddCriticalMarking(dtbResult, transactionNo);

                return dtbResult;
            }
            else
            {
                DataTable dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);
                //dtbResult.Columns.Add("ReadByPhysicianID", typeof(string));
                //dtbResult.Columns.Add("ReportedByNurseID", typeof(string));
                //dtbResult.Columns.Add("CompletelyReportedByUserID", typeof(string));

                //dtbResult.AcceptChanges();
                AddCriticalMarking(dtbResult, transactionNo);

                return dtbResult;
            }
            //return LabHistOrderResultFromManualEntry(transactionNo, regNo);
        }

        public static DataTable LaboratoryResult(string regNo, string transactionNo, string labNo)
        {
            if (AppSession.Parameter.IsUsingHisInterop)
            {
                DataTable dtbResult;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "LINK_LIS":
                        dtbResult = LabHistOrderResultFromSysmex(transactionNo, labNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, regNo);

                        AddCriticalMarking(dtbResult, transactionNo);

                        return dtbResult;
                }
            }
            return LabHistOrderResultFromManualEntry(transactionNo, regNo);
        }

        private static void AddCriticalMarking(DataTable dtbResult, string transactionNo)
        {
            if (!dtbResult.Columns.Contains("IsCritical"))
            {
                dtbResult.Columns.Add("IsCritical", typeof(bool));
                foreach (System.Data.DataRow row in dtbResult.Rows)
                {
                    row["IsCritical"] = false;
                }
            }
            else
            {
                foreach (System.Data.DataRow row in dtbResult.Rows)
                {
                    if ((bool)row["IsCritical"])
                    {
                        var rc = row["ResultComment"].ToString();
                        var cn = row["CriticalNote"].ToString();
                        rc += (!string.IsNullOrEmpty(rc) && (!string.IsNullOrEmpty(cn))) ? "<br />" : "";
                        if (!string.IsNullOrEmpty(cn))
                        {
                            rc += "<strong>Critical</strong>: " + cn;
                        }
                        row["ResultComment"] = rc;
                    }
                }
            }

            dtbResult.Columns.Add("ReadByPhysicianID", typeof(string));
            dtbResult.Columns.Add("ReportedByNurseID", typeof(string));
            dtbResult.Columns.Add("CompletelyReportedByUserID", typeof(string));

            var tclcColl = GetCriticalMarkings(transactionNo);
            foreach (var tclc in tclcColl)
            {
                var rows = dtbResult.AsEnumerable().Where(r => r["OrderLabNo"].ToString().Split('^')[0] == tclc.TransactionNo && r["LabOrderCode"].ToString() == tclc.LisTestID);
                foreach (var row in rows)
                {
                    row["ReadByPhysicianID"] = tclc.ReadByPhysicianID;
                    row["ReportedByNurseID"] = tclc.ReportedByNurseID;
                    row["CompletelyReportedByUserID"] = tclc.CompletelyReportedByUserID;
                }
            }
            dtbResult.AcceptChanges();
        }


        public static DataTable LabHistOrderResultFromSysmex(string transactionNo)
        {
            var qr = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("a");
            qr.Where(qr.OrderLabNo == transactionNo);
            qr.Select(qr.OrderLabNo, qr.LabOrderSummary, qr.Flag,
            "<CASE WHEN a.flag='L' THEN '<div style=\"font-weight: bold; color: blue;\">'+a.Result + ' ' + a.Unit+'</div>' " +
            "WHEN a.flag='H' THEN '<div style=\"font-weight: bold; color: red;\">'+a.Result + ' ' + a.Unit+'</div>' " +
            "WHEN a.flag='LL' THEN '<div style=\"font-weight: bold; color: #660066;\">'+a.Result + ' ' + a.Unit+'</div>' " +
            "WHEN a.flag='HH' THEN '<div style=\"font-weight: bold; color: #800000;\">'+a.Result + ' ' + a.Unit+'</div>' " +
            "ELSE a.Result + ' ' + a.Unit END as Result>",
            qr.OrderLabTglOrder.As("ResultDatetime"), qr.StandarValue, qr.LabOrderCode, "<1 as IsFraction>", qr.OrderLabTglOrder, qr.TestGroup.As("TestGroup"), "<'' as ResultComment>",
            @"<'' AS HisTestId>",
            @"<'' AS HeaderFlag>");

            if (!string.IsNullOrEmpty(AppSession.Parameter.LisCriticalFieldName))
            {
                qr.Select("<CAST((CASE ISNULL(" + AppSession.Parameter.LisCriticalFieldName + ", '') WHEN '' THEN 0 ELSE 1 END) as BIT) as IsCritical>",
                    "<" + AppSession.Parameter.LisCriticalFieldName + " as CriticalNote>");
            }
            else
            {
                qr.Select("<CAST(0 AS BIT) AS IsCritical>",
                    "<'' AS CriticalNote>");
            }

            qr.OrderBy(qr.DispSeq.Ascending);
            return qr.LoadDataTable();
        }

        public static DataTable LabHistOrderResultFromSysmex(string transactionNo, string labNo)
        {
            var qr = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("a");
            qr.Where(string.Format("<a.TransactionNo = '{0}'>", transactionNo));
            qr.Where(qr.OrderLabNo == labNo);
            qr.Select(qr.OrderLabNo, qr.LabOrderSummary, qr.Flag,
            "<CASE WHEN a.flag='L' THEN '<div style=\"font-weight: bold; color: blue;\">'+a.Result + ' ' + a.Unit+'</div>' WHEN a.flag='H' THEN '<div style=\"font-weight: bold; color: red;\">'+a.Result + ' ' + a.Unit+'</div>' ELSE a.Result + ' ' + a.Unit END as Result>",
            qr.OrderLabTglOrder.As("ResultDatetime"), qr.StandarValue, qr.LabOrderCode, "<1 as IsFraction>", qr.OrderLabTglOrder, qr.TestGroup.As("TestGroup"), "<'' as ResultComment>",
            @"<'' AS HisTestId>",
            @"<'' AS HeaderFlag>");

            if (!string.IsNullOrEmpty(AppSession.Parameter.LisCriticalFieldName))
            {
                qr.Select("<CAST((CASE ISNULL(" + AppSession.Parameter.LisCriticalFieldName + ", '') WHEN '' THEN 0 ELSE 1 END) as BIT) as IsCritical>",
                    "<" + AppSession.Parameter.LisCriticalFieldName + " as CriticalNote>");
            }
            else
            {
                qr.Select("<CAST(0 AS BIT) AS IsCritical>",
                    "<'' AS CriticalNote>");
            }

            qr.OrderBy(qr.DispSeq.Ascending);
            return qr.LoadDataTable();
        }

        public static DataTable LabHistOrderResultFromRSCH(string transactionNo)
        {
            var qr = new BusinessObject.Interop.RSCH.VwHasilPasienQuery("a");

            qr.Select(qr.OrderLabNo, qr.CheckupResultFractionName.As("LabOrderSummary"), "<'' as Flag>",
                qr.OutRange.As("Result"), qr.OrderLabTglOrder.As("ResultDatetime"), qr.StandarValue, qr.CheckupResultFractionCode.As("LabOrderCode"),
                "<CASE WHEN a.CheckupResultFractionCode>'' THEN 1 ELSE 0 END as IsFraction>", qr.OrderLabTglOrder, qr.CheckupResultGroupName.As("TestGroup"), "<'' as ResultComment>",
                @"<'' AS HisTestId>",
                @"<'' AS HeaderFlag>"
                );

            if (!string.IsNullOrEmpty(AppSession.Parameter.LisCriticalFieldName))
            {
                qr.Select("<CAST((CASE ISNULL(" + AppSession.Parameter.LisCriticalFieldName + ", '') WHEN '' THEN 0 ELSE 1 END) as BIT) as IsCritical>",
                    "<" + AppSession.Parameter.LisCriticalFieldName + " as CriticalNote>");
            }
            else
            {
                qr.Select("<CAST(0 AS BIT) AS IsCritical>",
                    "<'' AS CriticalNote>");
            }

            qr.Where(qr.OrderLabNo == transactionNo);
            qr.OrderBy(qr.Seq.Ascending);
            return qr.LoadDataTable();
        }

        public static DataTable LabHistOrderResultFromElims(string transactionNo)
        {
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSISB")
            {
                //TODO: IsConfidential=1 jangan ditampilkan kode_sir=ItemID
                var qr = new BusinessObject.Interop.ELIMS.HasilLISQuery("a");
                qr.es2.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;

                qr.Where(qr.NolabRs == transactionNo);
                qr.Select(qr.NolabRs.As("OrderLabNo"),
                "<'' as KodeSir>",
                "<CASE WHEN a.NILAI_KRITIS IS NULL THEN a.FLAG_HL ELSE a.FLAG_HL +'**' END AS Flag>",
                qr.ParameterName.As("LabOrderSummary"),
                qr.Hasil.As("Result"),
                qr.TglHasilSelesai.As("ResultDatetime"),
                (qr.NilaiRujukan + " " + qr.Satuan).As("StandarValue"),
                qr.NolabRs.As("LabOrderCode"),
                "<'' as IsFraction>",
                //(qr.Catatan + " "+ (qr.USER_LAPOR_KRITIS)).As("ResultComment"),
                "<ISNULL(a.Catatan,'') +' '+ ISNULL(a.USER_LAPOR_KRITIS,'') AS ResultComment>",
                @"<'' AS HisTestId>",
                @"<'' AS HeaderFlag>"
                );

                if (!string.IsNullOrEmpty(AppSession.Parameter.LisCriticalFieldName))
                {
                    qr.Select("<CAST((CASE ISNULL(" + AppSession.Parameter.LisCriticalFieldName + ", '') WHEN '' THEN 0 ELSE 1 END) as BIT) as IsCritical>",
                    "<" + AppSession.Parameter.LisCriticalFieldName + " as CriticalNote>");
                }
                else
                {
                    qr.Select("<CAST(0 AS BIT) AS IsCritical>",
                        "<'' AS CriticalNote>");
                }

                qr.OrderBy(qr.UrutBound.Ascending);
                var dtb = qr.LoadDataTable();

                dtb.AcceptChanges();


                // Add Column
                dtb.Columns.Add("OrderLabTglOrder", typeof(DateTime));
                dtb.Columns.Add("TestGroup", typeof(string));

                return dtb;
            }
            else

            {

                //TODO: IsConfidential=1 jangan ditampilkan kode_sir=ItemID
                var qr = new BusinessObject.Interop.ELIMS.HasilLISQuery("a");
                qr.es2.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;

                qr.Where(qr.NolabRs == transactionNo);
                qr.Select(qr.NolabRs.As("OrderLabNo"),
                "<'' as KodeSir>",
                qr.FlagHl.As("Flag"),
                qr.ParameterName.As("LabOrderSummary"),
                qr.Hasil.As("Result"),
                qr.TglHasilSelesai.As("ResultDatetime"),
                (qr.NilaiRujukan + " " + qr.Satuan).As("StandarValue"),
                qr.NolabRs.As("LabOrderCode"),
                "<'' as IsFraction>",
                qr.USER_LAPOR_KRITIS.As("ResultComment"),
                @"<'' AS HisTestId>",
                @"<'' AS HeaderFlag>",
                @"<CAST(0 AS BIT) AS IsCritical>",
                @"<'' AS CriticalNote>"
                );

                qr.OrderBy(qr.UrutBound.Ascending);
                var dtb = qr.LoadDataTable();


                dtb.AcceptChanges();


                // Add Column
                dtb.Columns.Add("OrderLabTglOrder", typeof(DateTime));
                dtb.Columns.Add("TestGroup", typeof(string));

                return dtb;
            }
        }
        public static DataTable LabHistOrderResultFromVanslab(string transactionNo)
        {
            //TODO: IsConfidential=1 jangan ditampilkan kode_sir=ItemID
            var qr = new BusinessObject.Interop.VANSLAB.LabHasilQuery("a");
            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;

            qr.Where(qr.NoRegistrasi == transactionNo);
            qr.Select(qr.NoRegistrasi.As("OrderLabNo"), qr.KodeSir, qr.Flag,
                "<REPLACE(a.nama_pemeriksaan, '    ', '&emsp;') AS LabOrderSummary>",
                "<CASE WHEN a.flag='L' THEN '<div style=\"font-weight: bold; color: blue;\">'+a.Hasil + ' ' + a.Unit+'</div>' WHEN a.flag='H' THEN '<div style=\"font-weight: bold; color: red;\">'+a.Hasil + ' ' + a.Unit+'</div>' ELSE a.Hasil + ' ' + a.Unit END as Result>",
                "<CASE WHEN a.Type='U' THEN a.tgl_hasil ELSE NULL END as ResultDatetime>",
                 qr.Normal.As("StandarValue"), qr.KodePemeriksaan.As("LabOrderCode"),
                "<CASE WHEN a.Type='U' THEN 1 ELSE 0 END as IsFraction>", "<'' as ResultComment>",
                @"<'' AS HisTestId>",
                @"<'' AS HeaderFlag>");

            if (!string.IsNullOrEmpty(AppSession.Parameter.LisCriticalFieldName))
            {
                qr.Select("<CAST((CASE ISNULL(" + AppSession.Parameter.LisCriticalFieldName + ", '') WHEN '' THEN 0 ELSE 1 END) as BIT) as IsCritical>",
                    "<" + AppSession.Parameter.LisCriticalFieldName + " as CriticalNote>");
            }
            else
            {
                qr.Select("<CAST(0 AS BIT) AS IsCritical>",
                    "<'' AS CriticalNote>");
            }

            qr.OrderBy(qr.NoUrut.Ascending);
            var dtb = qr.LoadDataTable();

            // Hapus tipe lab yg Confidential
            foreach (DataRow row in dtb.Rows)
            {
                // Jika akses pakai nama field maka pakai nama aslinya kecuali diset aliasnya
                if (row["kode_sir"] == null || string.IsNullOrWhiteSpace(row["kode_sir"].ToString())) continue;
                //var ilab = new ItemLaboratory();
                //if (ilab.LoadByPrimaryKey(row["kode_sir"].ToString()))
                //{
                //    if (ilab.IsConfidential ?? false)
                //        row.Delete();
                //}
                if (ItemLaboratoryConfidentials.Any(x => x.ItemID.Equals(row["kode_sir"].ToString())))
                {
                    row.Delete();
                }
            }

            dtb.AcceptChanges();


            // Add Column
            dtb.Columns.Add("OrderLabTglOrder", typeof(DateTime));
            dtb.Columns.Add("TestGroup", typeof(string));

            return dtb;
        }
        public static DataTable LabHistOrderResultFromVanslite(string transactionNo)
        {
            //TODO: IsConfidential=1 jangan ditampilkan kode_sir=ItemID
            var qr = new BusinessObject.Interop.VANSLITE.LabHasilQuery("a");
            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME;

            qr.Where(qr.NoPesanan == transactionNo);
            qr.Select(qr.NoRegistrasi.As("OrderLabNo"), qr.KodeSir, qr.Flag,
                "<REPLACE(a.nama_pemeriksaan, '    ', '&emsp;') AS LabOrderSummary>",
                "<CASE WHEN a.flag='L' THEN '<div style=\"font-weight: bold; color: blue;\">'+a.hasil + ' ' + a.unit+'</div>' WHEN a.flag='H' THEN '<div style=\"font-weight: bold; color: red;\">'+a.hasil + ' ' + a.unit+'</div>' ELSE a.hasil + ' ' + a.unit END as Result>",
                "<CASE WHEN a.type='U' THEN a.tgl_hasil ELSE NULL END as ResultDatetime>",
                 qr.Normal.As("StandarValue"), qr.KodePemeriksaan.As("LabOrderCode"),
                "<CASE WHEN a.type='U' THEN 1 ELSE 0 END as IsFraction>", "<'' as ResultComment>",
                @"<'' AS HisTestId>",
                @"<'' AS HeaderFlag>");

            if (!string.IsNullOrEmpty(AppSession.Parameter.LisCriticalFieldName))
            {
                qr.Select("<CAST((CASE ISNULL(" + AppSession.Parameter.LisCriticalFieldName + ", '') WHEN '' THEN 0 ELSE 1 END) as BIT) as IsCritical>",
                    "<" + AppSession.Parameter.LisCriticalFieldName + " as CriticalNote>");
            }
            else
            {
                qr.Select("<CAST(0 AS BIT) AS IsCritical>",
                    "<'' AS CriticalNote>");
            }

            qr.OrderBy(qr.NoUrut.Ascending);
            var dtb = qr.LoadDataTable();

            // Hapus tipe lab yg Confidential
            foreach (DataRow row in dtb.Rows)
            {
                // Jika akses pakai nama field maka pakai nama aslinya kecuali diset aliasnya
                if (row["kode_sir"] == null || string.IsNullOrWhiteSpace(row["kode_sir"].ToString())) continue;
                //var ilab = new ItemLaboratory();
                //if (ilab.LoadByPrimaryKey(row["kode_sir"].ToString()))
                //{
                //    if (ilab.IsConfidential ?? false)
                //        row.Delete();
                //}
                if (ItemLaboratoryConfidentials.Any(x => x.ItemID.Equals(row["kode_sir"].ToString())))
                {
                    row.Delete();
                }
            }

            dtb.AcceptChanges();


            // Add Column
            dtb.Columns.Add("OrderLabTglOrder", typeof(DateTime));
            dtb.Columns.Add("TestGroup", typeof(string));

            return dtb;
        }
        public static DataTable LabHistOrderResultFromWynakom(string transactionNo)
        {
            //TODO: IsConfidential=1 jangan ditampilkan kode_sir=ItemID
            string toTransactionNo = string.Format("{0}^ZZZ", transactionNo);
            var qr = new BusinessObject.Interop.Wynakom.OrderedResultsQuery("a");
            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
            qr.es.WithNoLock = true;
            qr.Where(qr.HisRegNo >= transactionNo, qr.HisRegNo < toTransactionNo); // Resultnya bisa bertahap contoh JO220825-00181^005

            qr.Select(
                qr.HisRegNo.As("OrderLabNo"),
                @"<CASE WHEN ISNULL(a.antibiotic_level, '') = '' THEN a.test_name ELSE a.antibiotic_level + ' - ' + a.test_name END AS 'LabOrderSummary'>",
                //qr.TestName.As("LabOrderSummary"),
                "<CASE WHEN a.test_flag_sign='L' OR a.test_flag_sign='H' THEN '<div style=\"font-weight: bold; color: blue;\">'+a.result + ' ' + a.test_units_name+'</div>' WHEN a.test_flag_sign='HH' OR a.test_flag_sign='LL' THEN '<div style=\"font-weight: bold; color: red;\">'+a.result + ' ' + a.test_units_name+'</div>' ELSE a.result +' ' + a.test_units_name END as Result>",
                qr.AuthorizationDate.As("ResultDatetime"),
                qr.ReferenceValue.As("StandarValue"),
                qr.LisTestId.As("LabOrderCode"),
                "<CONVERT(bit,CASE WHEN a.header_flag='0' THEN 1 ELSE 0 END) as IsFraction>",
                "<CASE WHEN a.test_flag_sign='*' THEN '' ELSE a.test_flag_sign END as Flag>",
                qr.TestGroup.As("TestGroup"),
                qr.ResultComment.As("ResultComment"),
                qr.HisTestId,
                qr.HeaderFlag);

            if (!string.IsNullOrEmpty(AppSession.Parameter.LisCriticalFieldName))
            {
                qr.Select("<CAST((CASE ISNULL(" + AppSession.Parameter.LisCriticalFieldName + ", '') WHEN '' THEN 0 ELSE 1 END) as BIT) as IsCritical>",
                     "<" + AppSession.Parameter.LisCriticalFieldName + " as CriticalNote>");
            }
            else
            {
                qr.Select("<CAST(0 AS BIT) AS IsCritical>",
                    "<'' AS CriticalNote>");
            }

            //qr.OrderBy(qr.Sequence.Ascending);
            //qr.OrderBy(@"<CASE WHEN a.antibiotic_level ='Reserve' THEN 'z'+a.antibiotic_level ELSE a.antibiotic_level END, a.lis_test_id>");
            qr.OrderBy(qr.Sequence.Ascending);
            qr.OrderBy(@"<a.lis_test_id,CASE WHEN a.antibiotic_level ='Reserve' THEN 'z'+a.antibiotic_level ELSE a.antibiotic_level END>");
            var dtb = qr.LoadDataTable();

            // Add Column
            dtb.Columns.Add("OrderLabTglOrder", typeof(DateTime));

            // Hapus yg confidential
            if (AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsHideConfidentialLabResult))
            {
                foreach (DataRow row in dtb.Rows)
                {
                    //var ilab = new ItemLaboratory();
                    //if (ilab.LoadByPrimaryKey(row[BusinessObject.Interop.Wynakom.OrderedResultsMetadata.ColumnNames.HisTestId].ToString()) && (ilab.IsConfidential ?? false))
                    //{
                    //    row.Delete();
                    //}
                    if (ItemLaboratoryConfidentials.Any(x => x.ItemID.Equals(row[BusinessObject.Interop.Wynakom.OrderedResultsMetadata.ColumnNames.HisTestId].ToString())))
                    {
                        row.Delete();
                    }
                }

                dtb.AcceptChanges();
            }
            return dtb;
        }
        public static string LaboratoryResultNote(string transactionNo)
        {
            if (!AppSession.Parameter.IsUsingHisInterop || AppSession.Parameter.LisInterop != "WYNAKOM") return string.Empty;

            string toTransactionNo = string.Format("{0}^ZZZ", transactionNo);
            var qr = new BusinessObject.Interop.Wynakom.OrderedResultsQuery("a");
            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
            qr.es.WithNoLock = true;
            qr.Where(qr.HisRegNo >= transactionNo, qr.HisRegNo < toTransactionNo, qr.ResultNote > string.Empty);
            qr.Select(qr.ResultNote.As("ResultNote"));
            qr.es.Top = 1;
            qr.es.WithNoLock = true;
            var dtb = qr.LoadDataTable();
            if (dtb.Rows.Count > 0)
                return string.Format("<fieldset><legend><strong>Result Note</strong></legend>{0}</fieldset>", (dtb.Rows[0][0]));
            else
                return string.Empty;

        }
        public static DataTable LabHistOrderResultFromManualEntry(string transactionNo, string registrationNo)
        {
            // Ambil data dari ItemLaboratoryDetail
            var qr = new TransChargesItemQuery("dt");
            var order = new TransChargesQuery("hd");
            qr.InnerJoin(order).On(qr.TransactionNo == order.TransactionNo);

            var item = new ItemQuery("i");
            qr.InnerJoin(item).On(qr.ItemID == item.ItemID);

            var itemGroup = new ItemGroupQuery("g");
            qr.InnerJoin(itemGroup).On(item.ItemGroupID == itemGroup.ItemGroupID);

            qr.Select(
                qr.TransactionNo.As("OrderLabNo"),
                qr.ItemID.As("LabOrderCode"),
                item.ItemName.As("LabOrderSummary"), "<'' as Flag>",
                qr.ResultValue.As("Result"),
                order.ExecutionDate.As("ResultDatetime"),
                //qr.IsExtraItem.As("IsFraction"), // Untuk entrian lab manual IsFraction dibaca dari ada tidaknya ParentNo (Handono 231109)
                "<CASE WHEN dt.ParentNo='' THEN 0 ELSE 1 END AS IsFraction>",
                qr.SequenceNo, qr.ParentNo, // Untuk update TestGroup
                order.TransactionDate.As("OrderLabTglOrder"),
                itemGroup.ItemGroupName.As("TestGroup"), 
                qr.Notes.As("ResultComment"),
                //"<'' as ResultComment>",
                @"<'' AS HisTestId>",
                @"<'' AS HeaderFlag>",
                @"<CAST(0 AS BIT) AS IsCritical>",
                @"<'' AS CriticalNote>"
                );

            qr.Where(qr.TransactionNo == transactionNo, order.IsValidated == true);

            var dtbTransChargesItem = qr.LoadDataTable();
            dtbTransChargesItem.Columns.Add(new DataColumn("StandarValue", typeof(string)));

            // Isi StandarValue
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);

            var ageInDays = (reg.RegistrationDate - patient.DateOfBirth).Value.TotalDays;

            foreach (DataRow row in dtbTransChargesItem.Rows)
            {
                if (row["Result"] == DBNull.Value)
                    row["ResultDatetime"] = DBNull.Value;

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

            // Untuk entrian lab manual TestGroup fraction samakan dengan Parent nya (Handono 231109)
            foreach (DataRow row in dtbTransChargesItem.Rows)
            {
                if (string.Empty.Equals(row["ParentNo"]))
                {
                    var parentNo = row["SequenceNo"].ToString();
                    var parentTestGroup = row["TestGroup"].ToString();
                    foreach (DataRow subRow in dtbTransChargesItem.Rows)
                    {
                        if (parentNo.Equals(subRow["ParentNo"]))
                            subRow["TestGroup"] = parentTestGroup;
                    }
                }
            }

            return dtbTransChargesItem;
        }
        #endregion

        #region Radiology
        protected void grdRadiology_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRadiology.DataSource = TransChargesDataTable("RAD", PatientID, PatientRelateds, CurrentRegDate);
        }
        protected void grdRadiology_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //var dataItem = e.Item as GridDataItem;
                var grdResult = (RadGrid)e.Item.FindControl("grdRadiologyResult");
                var transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                PopulateWithTestResult(grdResult, transactionNo);
            }
        }

        protected void grdRadiologyResult_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //var dataItem = e.Item as GridDataItem;
                var lvItemDocumentImage = (RadListView)e.Item.FindControl("lvItemDocumentImage");

                var transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                var sequenceNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SequenceNo"]);

                var qr = new TransChargesItemImageQuery();
                qr.Where(qr.TransactionNo == transactionNo, qr.SequenceNo == sequenceNo);
                var dtb = qr.LoadDataTable();

                lvItemDocumentImage.DataSource = dtb;
                lvItemDocumentImage.Rebind();
            }
        }
        #endregion

        #region Pathology

        #region PathologyAnatomyResult
        protected string PathologyAnatomyResult(string transactionNo)
        {
            var pa = new PathologyAnatomy();
            pa.Query.es.Top = 1;
            pa.Query.Where(pa.Query.TransactionNo == transactionNo);
            if (pa.Query.Load())
            {
                var resultTypeName = string.Empty;
                var reportId = string.Empty;
                switch (pa.ResultType)
                {
                    case "01":
                        resultTypeName = "CYTOLOGY";
                        reportId = "XML.RSTJ.12.001";
                        break;
                    case "02":
                        resultTypeName = "HISTOPATHOLOGY";
                        reportId = "XML.RSTJ.12.005";
                        break;
                    case "03":
                        resultTypeName = "PAP SMEAR";
                        //reportId = "XML.RSTJ.12.005";
                        break;
                    case "04":
                        resultTypeName = "IMMUNOHISTOCHEMISTRY";
                        reportId = "XML.RSTJ.12.003";
                        break;
                    case "05":
                        resultTypeName = "Fine Needle Aspiration Biopsy (FNAB)";
                        reportId = "XML.RSTJ.12.004";
                        break;
                    case "06":
                        resultTypeName = "Vries Coupe (VC)";
                        reportId = "XML.RSTJ.12.002";
                        break;
                    case "07":
                        resultTypeName = "HISTOCHEMISTRY";
                        reportId = "XML.RSTJ.12.006";
                        break;
                }
                var medic = new Paramedic();
                medic.LoadByPrimaryKey(pa.ParamedicID);
                var strb = new StringBuilder();
                strb.AppendLine("<fieldset>");
                strb.AppendFormat("<legend><strong>{0} <a href=\"#\" onclick=\"javascript:printPreviewPaResult('{1}', '{2}'); return false;\"><img src=\"{3}/Images/Toolbar/print16.png\"  alt=\"New\" /></a></strong></legend>", resultTypeName, pa.ResultNo, reportId, Helper.UrlRoot());
                strb.AppendLine("<table width=\"100%\">");

                strb.AppendFormat("<tr><td class='label'>Examiner By</td><td >{0}</td></tr>", medic.ParamedicName);
                strb.AppendFormat("<tr><td class='label'>Date Of Completion</td><td >{0}</td></tr>", Convert.ToDateTime(pa.DateOfCompletion.Value).ToString(AppConstant.DisplayFormat.Date));

                var diag = new PathologyAnatomyDiagnosis();
                if (!string.IsNullOrWhiteSpace(pa.DiagnosisID))
                    diag.LoadByPrimaryKey(pa.ResultType, pa.DiagnosisID);
                strb.AppendFormat("<tr><td class='label'>Diagnosis</td><td >{0}</td></tr>", diag.DiagnosisName);

                switch (pa.ResultType)
                {
                    case "01": //Cytology
                        AddPaLocation(pa, strb);

                        strb.AppendFormat("<tr><td class='label'>Notes</td><td>{0}</td></tr>", pa.Notes);
                        strb.AppendFormat("<tr><td class='label'>Clinical Diagnosis</td><td>{0}</td></tr>", pa.DiagnosisName);
                        strb.AppendFormat("<tr><td class='label'>Clinical Data</td><td>{0}</td></tr>", pa.ClinicalData);
                        strb.AppendFormat("<tr><td class='label'>Examination Material</td><td>{0}</td></tr>", pa.ExaminationMaterial);
                        strb.AppendFormat("<tr><td class='label'>Macroscopic</td><td>{0}</td></tr>", pa.Macroscopic);
                        strb.AppendFormat("<tr><td class='label'>Microscopic</td><td>{0}</td></tr>", pa.Microscopic);
                        if (AppSession.Parameter.IsPathologyAnatomyWithImpressionResult)
                            strb.AppendFormat("<tr><td class='label'>Impression</td><td>{0}</td></tr>", pa.Impression);
                        else
                            strb.AppendFormat("<tr><td class='label'>Conclusion / PA Diagnosis</td><td>{0}</td></tr>", pa.PathologyAnatomyDiagnoses);
                        strb.AppendFormat("<tr><td class='label'>Additional Notes</td><td>{0}</td></tr>", pa.AdditionalNotes);

                        break;
                    case "02": //Histology
                        AddPaMorphology(pa, strb);
                        AddPaSourceOfTissue(pa, strb);

                        strb.AppendFormat("<tr><td class='label'>Notes</td><td>{0}</td></tr>", pa.Notes);
                        strb.AppendFormat("<tr><td class='label'>Clinical Diagnosis</td><td>{0}</td></tr>", pa.DiagnosisName);
                        strb.AppendFormat("<tr><td class='label'>Clinical Data</td><td>{0}</td></tr>", pa.ClinicalData);
                        strb.AppendFormat("<tr><td class='label'>Location</td><td>{0}</td></tr>", pa.LocationName);
                        strb.AppendFormat("<tr><td class='label'>Macroscopic</td><td>{0}</td></tr>", pa.Macroscopic);
                        strb.AppendFormat("<tr><td class='label'>Microscopic</td><td>{0}</td></tr>", pa.Microscopic);
                        if (AppSession.Parameter.IsPathologyAnatomyWithImpressionResult)
                            strb.AppendFormat("<tr><td class='label'>Impression</td><td>{0}</td></tr>", pa.Impression);
                        else
                            strb.AppendFormat("<tr><td class='label'>Conclusion / PA Diagnosis</td><td>{0}</td></tr>", pa.PathologyAnatomyDiagnoses);
                        strb.AppendFormat("<tr><td class='label'>Additional Notes</td><td>{0}</td></tr>", pa.AdditionalNotes);

                        break;
                    case "03": // Pap Smear
                        strb.AppendFormat("<tr><td class='label'>Notes</td><td>{0}</td></tr>", pa.Notes);
                        strb.AppendFormat("<tr><td class='label'>Interpretation Of Results</td><td>{0}</td></tr>", pa.InterpretationOfResults);
                        strb.AppendFormat("<tr><td class='label'>Suggestion</td><td>{0}</td></tr>", pa.Suggestion);

                        break;
                    case "04": //Immunohistochemistry
                    case "07": //Histochemistry
                        AddPaMorphology(pa, strb);
                        AddPaSourceOfTissue(pa, strb);
                        strb.AppendFormat("<tr><td class='label'>Notes</td><td>{0}</td></tr>", pa.Notes);
                        strb.AppendFormat("<tr><td class='label'>Pathology Anatomy Diagnoses</td><td>{0}</td></tr>", pa.PathologyAnatomyDiagnoses);

                        // Result
                        strb.AppendFormat("<tr><td class='label'>Result</td><td><strong>Mammae Result</strong><br/>ER: {0}<br />PR: {1}<br />Her2-Neu: {2}<br />Ki-67: {3}<br/><br/><strong>Other Result</strong><br/>{4}</td></tr>", pa.ER, pa.PR, pa.Her2Neu, pa.Ki67, pa.Result);
                        strb.AppendFormat("<tr><td class='label'>Impression</td><td>{0}</td></tr>", pa.Impression);

                        break;
                    case "05": //FNAB
                        AddPaLocation(pa, strb);

                        strb.AppendFormat("<tr><td class='label'>Notes</td><td>{0}</td></tr>", pa.Notes);
                        strb.AppendFormat("<tr><td class='label'>Clinical Diagnosis</td><td>{0}</td></tr>", pa.DiagnosisName);
                        strb.AppendFormat("<tr><td class='label'>Clinical Data</td><td>{0}</td></tr>", pa.ClinicalData);
                        strb.AppendFormat("<tr><td class='label'>Examination Material</td><td>{0}</td></tr>", pa.ExaminationMaterial);
                        strb.AppendFormat("<tr><td class='label'>Macroscopic</td><td>{0}</td></tr>", pa.Macroscopic);
                        strb.AppendFormat("<tr><td class='label'>Microscopic</td><td>{0}</td></tr>", pa.Microscopic);
                        if (AppSession.Parameter.IsPathologyAnatomyWithImpressionResult)
                            strb.AppendFormat("<tr><td class='label'>Impression</td><td>{0}</td></tr>", pa.Impression);
                        else
                            strb.AppendFormat("<tr><td class='label'>Conclusion / PA Diagnosis</td><td>{0}</td></tr>", pa.PathologyAnatomyDiagnoses);
                        strb.AppendFormat("<tr><td class='label'>Additional Notes</td><td>{0}</td></tr>", pa.AdditionalNotes);

                        break;
                    case "06":  //VC
                        AddPaLocation(pa, strb);

                        strb.AppendFormat("<tr><td class='label'>Notes</td><td>{0}</td></tr>", pa.Notes);
                        strb.AppendFormat("<tr><td class='label'>Clinical Diagnosis</td><td>{0}</td></tr>", pa.DiagnosisName);
                        strb.AppendFormat("<tr><td class='label'>Macroscopic</td><td>{0}</td></tr>", pa.Macroscopic);
                        strb.AppendFormat("<tr><td class='label'>Microscopic</td><td>{0}</td></tr>", pa.Microscopic);
                        if (AppSession.Parameter.IsPathologyAnatomyWithImpressionResult)
                            strb.AppendFormat("<tr><td class='label'>Impression</td><td>{0}</td></tr>", pa.Impression);
                        else
                            strb.AppendFormat("<tr><td class='label'>Conclusion / PA Diagnosis</td><td>{0}</td></tr>", pa.PathologyAnatomyDiagnoses);
                        strb.AppendFormat("<tr><td class='label'>Additional Notes</td><td>{0}</td></tr>", pa.AdditionalNotes);

                        break;
                    default:
                        break;
                }

                if (pa.ResultType == "03") //
                {
                    AddPaTissue(pa, strb);
                }

                strb.AppendLine("</table></fieldset>");
                return strb.ToString();
            }
            return string.Empty;
        }

        private static void AddPaTissue(PathologyAnatomy pa, StringBuilder strb)
        {
            var tis = new PathologyAnatomyTissue();
            if (!string.IsNullOrWhiteSpace(pa.TissueID))
                tis.LoadByPrimaryKey(pa.TissueID);
            strb.AppendFormat("<tr><td class='label'>Tissue</td><td>{0}</td></tr>", tis.TissueName);
        }

        private static void AddPaSourceOfTissue(PathologyAnatomy pa, StringBuilder strb)
        {
            var sot = new PathologyAnatomySourceOfTissue();
            if (!string.IsNullOrWhiteSpace(pa.SourceOfTissueID))
                sot.LoadByPrimaryKey(pa.SourceOfTissueID);
            strb.AppendFormat("<tr><td class='label'>Source Of Tissue</td><td>{0}</td></tr>", sot.SourceOfTissueName);
        }

        private static void AddPaMorphology(PathologyAnatomy pa, StringBuilder strb)
        {
            var morp = new Diagnose();
            if (!string.IsNullOrWhiteSpace(pa.MorphologyID))
                morp.LoadByPrimaryKey(pa.MorphologyID);
            strb.AppendFormat("<tr><td class='label'>Morphology (ICD X)</td><td>{0}</td></tr>", morp.DiagnoseName);
        }

        private static void AddPaLocation(PathologyAnatomy pa, StringBuilder strb)
        {
            var loc = new PathologyAnatomyLocationOfCytology();
            if (!string.IsNullOrWhiteSpace(pa.LocationID))
                loc.LoadByPrimaryKey(pa.LocationID);
            strb.AppendFormat("<tr><td class='label'>Location</td><td>{0}</td></tr>", loc.LocationName);
        }

        #endregion

        protected void grdPathology_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //if (!AppSession.Parameter.IsPathologyAnatomyWithTestResult)
            if (AppSession.Application.IsMenuPathologyAnatomyActive)
            {
                var dtb = TransChargesDataTable("PAT", PatientID, PatientRelateds, CurrentRegDate);
                dtb.Columns.Add("DocumentImage", typeof(System.Byte[]));

                // Lengkapi image 
                // Order PA hanya 1 item detil
                foreach (DataRow row in dtb.Rows)
                {
                    var tciImg = new TransChargesItemImage();
                    tciImg.Query.es.Top = 1;
                    tciImg.Query.Where(tciImg.Query.TransactionNo == row["TransactionNo"].ToString());
                    if (tciImg.Query.Load())
                    {
                        row["DocumentImage"] = tciImg.DocumentImage;
                    }
                }

                grdPathology.DataSource = dtb;
            }
            else
                grdPathology.DataSource = null;
        }
        protected void grdPathology_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var lvItemDocumentImage = (RadListView)e.Item.FindControl("lvItemDocumentImage");
                var transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);

                var pat = new PathologyAnatomy();
                pat.Query.es.Top = 1;
                pat.Query.Where(pat.Query.TransactionNo == transactionNo);
                if (pat.Query.Load())
                {
                    var qr = new PathologyAnatomyImageQuery();
                    qr.Where(qr.ResultNo == pat.ResultNo);
                    var dtb = qr.LoadDataTable();

                    lvItemDocumentImage.DataSource = dtb;
                    lvItemDocumentImage.Rebind();
                }
            }
        }


        #endregion

        #region Pathology2
        protected void grdPathology2_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //if (AppSession.Parameter.IsPathologyAnatomyWithTestResult)
            if (!AppSession.Application.IsMenuPathologyAnatomyActive)
                ((RadGrid)sender).DataSource = TransChargesDataTable("PAT", PatientID, PatientRelateds, CurrentRegDate);
            else
                ((RadGrid)sender).DataSource = null;
        }

        protected void grdPathology2_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var grdResult = (RadGrid)e.Item.FindControl("grdPathology2Result");
                var transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);

                PopulateWithTestResult(grdResult, transactionNo);
            }
        }

        #endregion


        #region Other Order
        protected void grdExamOrderOther_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)sender).DataSource = TransChargesDataTable("OTH", PatientID, PatientRelateds, CurrentRegDate);
        }

        protected void grdExamOrderOther_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var grdResult = (RadGrid)e.Item.FindControl("grdExamOrderOtherResult");
                var transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSISB")
                //{
                //    LabHistOrderResultFromElims(transactionNo);
                //}
                //else
                //{
                PopulateWithTestResult(grdResult, transactionNo);
                //}

            }
        }

        private static void PopulateWithTestResult(RadGrid grdResult, string transactionNo)
        {
            var result = new TestResultQuery("r");

            var item = new ItemQuery("i");
            result.InnerJoin(item).On(result.ItemID == item.ItemID);

            var tci = new TransChargesItemQuery("tci");
            result.InnerJoin(tci).On(result.ItemID == tci.ItemID & tci.TransactionNo == transactionNo);

            result.Where(result.TransactionNo == transactionNo, tci.TransactionNo == transactionNo);
            result.Select(result.TransactionNo, tci.SequenceNo, item.ItemName, result.ParamedicID, result.TestResultDateTime, result.TestResult);

            var dtb = result.LoadDataTable();

            if (dtb != null && dtb.Rows.Count > 0)
            {
                // Format               
                foreach (DataRow row in dtb.Rows)
                {
                    var medic = new Paramedic();
                    medic.LoadByPrimaryKey(row["ParamedicID"].ToString());
                    var strb = new StringBuilder();
                    strb.AppendLine("<fieldset>");
                    strb.AppendFormat("<legend>Result By: {0}<br />Date: ({1})</legend>", medic.ParamedicName, Convert.ToDateTime(row["TestResultDateTime"]).ToString(AppConstant.DisplayFormat.DateHourMinute));
                    strb.AppendLine(row["TestResult"].ToString());
                    strb.AppendLine("</fieldset>");

                    row["TestResult"] = strb.ToString();
                }

                grdResult.DataSource = dtb;
                grdResult.Rebind();
            }
            else
                grdResult.Visible = false;
        }

        #endregion

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //TODO: cari cara supaya hanya ketika diperlukan saja baru postback
            if (mainRadTabStrip.SelectedIndex == 0) grdLaboratory.Rebind();
            else if (mainRadTabStrip.SelectedIndex == 1) grdRadiology.Rebind();
            else return;
        }

        public bool IsUsingCasemix
        {
            get { return AppSession.Parameter.CasemixValidationRegistrationType.Contains(RegistrationType); }
        }

        private string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }
    }
}