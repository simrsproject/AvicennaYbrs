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
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.K3RS.MedicalHistory
{
    public partial class LaboratoryResultTest : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.K3RS_EmployeeMedicalHistory;

            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                Page.Title = "Laboratory Result for : " + pat.PatientName + " [MRN: " + pat.MedicalNo + " / Reg#: " + reg.RegistrationNo + "]";

                //(Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        private DataTable JobOrderTable
        {
            get
            {
                if (ViewState["JobOrderTable" + Request.UserHostName] != null) return ViewState["JobOrderTable" + Request.UserHostName] as DataTable;

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

                ViewState["JobOrderTable" + Request.UserHostName] = table;
                return ViewState["JobOrderTable" + Request.UserHostName] as DataTable;
            }
        }

        private DataTable TransChargesDataTable(string transType)
        {
            // OutPatient maupun InPatient dimunculkan semua historynya (2019 09 13)
            var query = new TransChargesItemQuery("a");
            var tc = new TransChargesQuery("b");
            var item = new ItemQuery("c");
            var reg = new RegistrationQuery("f");
            var toUnit = new ServiceUnitQuery("tu");
            var fromUnit = new ServiceUnitQuery("fu");

            query.Select(
                tc.RegistrationNo,
                query.TransactionNo,
                query.SequenceNo,
                tc.TransactionDate,
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
                query.CasemixApprovedByUserID,
                query.Notes,
                query.ResultValue.Coalesce("''"),
                query.CommunicationID.Coalesce("''"),
                item.SRItemType
                );

            query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(toUnit).On(tc.ToServiceUnitID == toUnit.ServiceUnitID);
            query.InnerJoin(fromUnit).On(tc.FromServiceUnitID == fromUnit.ServiceUnitID);

            query.Where(
                tc.RegistrationNo == Request.QueryString["regNo"],
                tc.IsVoid == false,
                query.ParentNo == ""
                );

            switch (transType)
            {
                case "LAB":
                    {
                        query.Where(tc.IsOrder == true, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID);
                        break;
                    }
                case "RAD":
                    {
                        query.Where(tc.IsOrder == true);

                        var others = AppSession.Parameter.ServiceUnitRadiologyIDs;
                        if (!string.IsNullOrWhiteSpace(others))
                        {
                            if (others.Contains(";"))
                                query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                    tc.ToServiceUnitID.In(others.Split(';'))));
                            else
                                query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                    tc.ToServiceUnitID == others));
                        }
                        else
                            query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2));
                        break;
                    }
                case "PAT":
                    {
                        query.Where(tc.IsOrder == true, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitPathologyAnatomyID);
                        break;
                    }
                default:
                    {
                        // List selain LAB,RAD, PAT ditambah dari ServiceUnit Transaction
                        query.Where(tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitLaboratoryID,
                            tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID,
                            tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID2, tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitPathologyAnatomyID);

                        var others = AppSession.Parameter.ServiceUnitRadiologyIDs;
                        if (!string.IsNullOrWhiteSpace(others))
                        {
                            if (others.Contains(";"))
                                query.Where(tc.ToServiceUnitID.NotIn(others.Split(';')));
                            else
                                query.Where(tc.ToServiceUnitID != others);
                        }
                        break;
                    }
            }

            query.OrderBy(tc.TransactionDate.Descending, tc.TransactionNo.Descending);
            var table = query.LoadDataTable();


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

            var urlRoot = Helper.UrlRoot();
            var isRisPacsDataNotLoaded = true;
            var risPacsInteropVendor = AppParameter.GetParameterValue(AppParameter.ParameterItem.RisPacsInteropVendor);

            foreach (var p in tcs)
            {
                int i = 0;
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

                        if (ConfigurationManager.AppSettings["EpsUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EpsUrlLocation"])) row["EpsUrlLocation"] = ConfigurationManager.AppSettings["EpsUrlLocation"];
                        else row["EpsUrlLocation"] = string.Empty;

                        if (ConfigurationManager.AppSettings["DcomUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DcomUrlLocation"])) row["DcomUrlLocation"] = ConfigurationManager.AppSettings["DcomUrlLocation"];
                        else row["DcomUrlLocation"] = string.Empty;

                        row["IsResultAvailable"] = "0";

                        if (transType == "RAD" && AppParameter.IsYes(AppParameter.ParameterItem.IsUsingRisPacsInterop))
                        {
                            switch (risPacsInteropVendor)
                            {
                                case "INTIWID":
                                    {
                                        if (!string.IsNullOrEmpty(orderItem["ResultValue"].ToString()))
                                        {
                                            if (string.IsNullOrEmpty(orderItem["CommunicationID"].ToString()))
                                            {
                                                try
                                                {

                                                    var service = new Common.Worklist.RSI.Service();
                                                    var json = service.GetJsonOrder(new Common.Worklist.RSI.Json.Order.Root()
                                                    { uid = orderItem["ResultValue"].ToString() }, isRisPacsDataNotLoaded);

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
                            }


                        }

                    }
                    i++;

                    if (AppSession.Parameter.CasemixValidationRegistrationType.Any())
                    {
                        var casemixApprovedByUserID = string.Empty;
                        if (orderItem["CasemixApprovedByUserID"] != null)
                            casemixApprovedByUserID = orderItem["CasemixApprovedByUserID"].ToString();

                        var isOrderRealization = orderItem["IsOrderRealization"].ToBoolean();

                        orderContent.AppendFormat("{5} {6}{0} {1} {2}{7} <img src='{4}/Images/Toolbar/{3}' />&nbsp;<input type='image' id='image' onclick=\"alert('Notes : {9}')\" src='{4}/Images/{8}' alt='{9}' />&nbsp;&nbsp;", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                            (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), urlRoot, AppConstant.HtmlChar.Bullet,
                            !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "<strike>" : string.Empty,
                            !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "</strike>" : string.Empty,
                            !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "infoblue16.png" : string.Empty,
                            !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? orderItem["Notes"].ToString() : string.Empty);
                    }
                    else
                    {
                        orderContent.AppendFormat("{5} {0} {1} {2} <img src='{4}/Images/Toolbar/{3}' />&nbsp;&nbsp;", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                            (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), urlRoot, AppConstant.HtmlChar.Bullet);
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
                        orderContent.Append("<br />");
                    }
                }

                row["TransactionNo"] = p.Key.TransactionNo;
                row["JobOrderSummary"] = orderContent.ToString();
                row["TransactionDate"] = p.Key.TransactionDate;

                dtb.Rows.Add(row);
            }
            return dtb;
        }

        #region Laboratory
        protected void grdLaboratory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLaboratory.DataSource = TransChargesDataTable("LAB");
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
                var transactionNo = string.Empty;
                var labNo = string.Empty;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "LINK_LIS":
                        transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                        labNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ResultValue"]);
                        grdResult.DataSource = LaboratoryResult(transactionNo, labNo);
                        break;
                    default:
                        transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                        grdResult.DataSource = LaboratoryResult(transactionNo);
                        break;
                }
                grdResult.Rebind();
            }

        }
        #endregion
        #region LaboratoryResult
        private DataTable LaboratoryResult(string transactionNo)
        {
            if (AppSession.Parameter.IsUsingHisInterop)
            {
                DataTable dtbResult;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "SYSMEX":
                        dtbResult = LabHistOrderResultFromSysmex(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, RegistrationNo);
                        return dtbResult;
                    case "RSCH":
                        dtbResult = LabHistOrderResultFromRSCH(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, RegistrationNo);
                        return dtbResult;
                    case "VANSLAB":
                        dtbResult = LabHistOrderResultFromVanslab(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, RegistrationNo);
                        return dtbResult;
                    case "WYNAKOM":
                        dtbResult = LabHistOrderResultFromWynakom(transactionNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, RegistrationNo);
                        return dtbResult;
                    default:
                        return LabHistOrderResultFromManualEntry(transactionNo, RegistrationNo);
                }
            }
            return LabHistOrderResultFromManualEntry(transactionNo, RegistrationNo);
        }

        private DataTable LaboratoryResult(string transactionNo, string labNo)
        {
            if (AppSession.Parameter.IsUsingHisInterop)
            {
                DataTable dtbResult;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "LINK_LIS":
                        dtbResult = LabHistOrderResultFromSysmex(transactionNo, labNo);
                        if (dtbResult == null || dtbResult.Rows.Count < 1)
                            dtbResult = LabHistOrderResultFromManualEntry(transactionNo, RegistrationNo);
                        return dtbResult;
                }
            }
            return LabHistOrderResultFromManualEntry(transactionNo, RegistrationNo);
        }


        public static DataTable LabHistOrderResultFromSysmex(string transactionNo)
        {
            var qr = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("a");
            qr.Where(qr.OrderLabNo == transactionNo);
            qr.Select(qr.OrderLabNo, qr.LabOrderSummary, qr.Flag,
            "<CASE WHEN a.flag='L' THEN '<div style=\"font-weight: bold; color: blue;\">'+a.Result + ' ' + a.Unit+'</div>' WHEN a.flag='H' THEN '<div style=\"font-weight: bold; color: red;\">'+a.Result + ' ' + a.Unit+'</div>' ELSE a.Result + ' ' + a.Unit END as Result>",
            qr.OrderLabTglOrder.As("ResultDatetime"), qr.StandarValue, qr.LabOrderCode, "<1 as IsFraction>", qr.OrderLabTglOrder, qr.TestGroup.As("TestGroup"), "<'' as ResultComment>");
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
            qr.OrderLabTglOrder.As("ResultDatetime"), qr.StandarValue, qr.LabOrderCode, "<1 as IsFraction>", qr.OrderLabTglOrder, qr.TestGroup.As("TestGroup"), "<'' as ResultComment>");
            qr.OrderBy(qr.DispSeq.Ascending);
            return qr.LoadDataTable();
        }

        public static DataTable LabHistOrderResultFromRSCH(string transactionNo)
        {
            var qr = new BusinessObject.Interop.RSCH.VwHasilPasienQuery("a");

            qr.Select(qr.OrderLabNo, qr.CheckupResultFractionName.As("LabOrderSummary"), "<'' as Flag>",
                qr.OutRange.As("Result"), qr.OrderLabTglOrder.As("ResultDatetime"), qr.StandarValue, qr.CheckupResultFractionCode.As("LabOrderCode"),
                "<CASE WHEN a.CheckupResultFractionCode>'' THEN 1 ELSE 0 END as IsFraction>", qr.OrderLabTglOrder, qr.CheckupResultGroupName.As("TestGroup"), "<'' as ResultComment>"
                );

            qr.Where(qr.OrderLabNo == transactionNo);
            qr.OrderBy(qr.Seq.Ascending);
            return qr.LoadDataTable();
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
                "<CASE WHEN a.Type='U' THEN 1 ELSE 0 END as IsFraction>", "<'' as ResultComment>");
            qr.OrderBy(qr.NoUrut.Ascending);
            var dtb = qr.LoadDataTable();

            // Hapus tipe lab yg Confidential
            foreach (DataRow row in dtb.Rows)
            {
                // Jika akses pakai nama field maka pakai nama aslinya kecuali diset aliasnya
                if (row["kode_sir"] == null || string.IsNullOrWhiteSpace(row["kode_sir"].ToString())) continue;
                var ilab = new ItemLaboratory();
                if (ilab.LoadByPrimaryKey(row["kode_sir"].ToString()))
                {
                    if (ilab.IsConfidential ?? false)
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
            string toTransactionNo = string.Format("{0}^ZZZ", transactionNo);
            //TODO: IsConfidential=1 jangan ditampilkan kode_sir=ItemID
            var qr = new BusinessObject.Interop.Wynakom.OrderedResultsQuery("a");
            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

            qr.Where(qr.HisRegNo >= transactionNo, qr.HisRegNo < toTransactionNo);
            qr.Select(qr.HisRegNo.As("OrderLabNo"), qr.TestName.As("LabOrderSummary"),
                "<CASE WHEN a.test_flag_sign='L' OR a.test_flag_sign='H' THEN '<div style=\"font-weight: bold; color: orange;\">'+a.result + ' ' + a.test_units_name+'</div>' WHEN a.test_flag_sign='HH' OR a.test_flag_sign='LL' THEN '<div style=\"font-weight: bold; color: red;\">'+a.result + ' ' + a.test_units_name+'</div>' ELSE a.result +' ' + a.test_units_name END as Result>",
                qr.AuthorizationDate.As("ResultDatetime"),
                 qr.ReferenceValue.As("StandarValue"), qr.LisTestId.As("LabOrderCode"),
                "<CONVERT(bit,CASE WHEN a.header_flag='0' THEN 1 ELSE 0 END) as IsFraction>", "<CASE WHEN a.test_flag_sign='*' THEN '' ELSE a.test_flag_sign END as Flag>",
                qr.TestGroup.As("TestGroup"), qr.ResultComment.As("ResultComment"), qr.HisTestId, qr.HeaderFlag);
            qr.OrderBy(qr.Sequence.Ascending);
            var dtb = qr.LoadDataTable();

            // Add Column
            dtb.Columns.Add("OrderLabTglOrder", typeof(DateTime));

            // Hapus yg confidential
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsHideConfidentialLabResult))
            {
                foreach (DataRow row in dtb.Rows)
                {
                    var ilab = new ItemLaboratory();
                    if (ilab.LoadByPrimaryKey(row[BusinessObject.Interop.Wynakom.OrderedResultsMetadata.ColumnNames.HisTestId].ToString()) && (ilab.IsConfidential ?? false))
                    {
                        row.Delete();
                    }
                }

                dtb.AcceptChanges();
            }
            return dtb;
        }
        protected string LaboratoryResultNote(string transactionNo)
        {
            if (!AppSession.Parameter.IsUsingHisInterop || AppSession.Parameter.LisInterop != "WYNAKOM") return string.Empty;

            string toTransactionNo = string.Format("{0}^ZZZ", transactionNo);
            var qr = new BusinessObject.Interop.Wynakom.OrderedResultsQuery("a");
            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
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

            qr.Select(qr.TransactionNo.As("OrderLabNo"), qr.ItemID.As("LabOrderCode"), item.ItemName.As("LabOrderSummary"), "<'' as Flag>",
                qr.ResultValue.As("Result"), order.ExecutionDate.As("ResultDatetime"), qr.IsExtraItem.As("IsFraction"), order.TransactionDate.As("OrderLabTglOrder"), itemGroup.ItemGroupName.As("TestGroup"), "<'' as ResultComment>");

            qr.Where(qr.TransactionNo == transactionNo);

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

            return dtbTransChargesItem;
        }
        #endregion

        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}