using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Web.UI;
using Telerik.Reporting;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class JobOrderRealisationList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["disch"] == "0")
                ProgramID = AppConstant.Program.JobOrderRealisation;
            else
                ProgramID = AppConstant.Program.JobOrderRealisationVerification;

            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                query.Where(
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.ClusterPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                            ),
                        query.IsActive == true
                        );


                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                if (AppSession.Parameter.IsJobOrderRealizationListByOrderDate)
                    rblFilterDate.SelectedIndex = 0;
                else
                    rblFilterDate.SelectedIndex = 1;

                txtOrderDate1.SelectedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now;
                txtOrderDate2.SelectedDate = (new DateTime()).NowAtSqlServer(); //DateTime.Now;

                if (AppSession.Parameter.IsShowPrintLabel1InJobOrderRealizationList)
                {
                    grdList.Columns.FindByUniqueName("PrintLabel1").Visible = true;
                    grdList2.Columns.FindByUniqueName("PrintLabel1").Visible = true;
                }

                trCasemix.Visible = AppSession.Parameter.CasemixValidationRegistrationType.Any(a => !string.IsNullOrEmpty(a));
                grdList.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = trCasemix.Visible;

                ComboBox.PopulateWithServiceUnitForTransactionJO(cboToServiceUnitID, TransactionCode.JobOrder, true);

                tabStrip.Tabs[1].Visible = AppSession.Parameter.IsJobOrderRealizationListWith2Tabs;

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingItemConsAndExpFactorOnJORealizationList) == "Yes")
                {
                    grdList2.MasterTableView.DetailTables[0].Columns.FindByUniqueName("cons").Visible = true;
                    grdList2.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ExposureFactor").Visible = true;
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                var grd = (RadGrid)source;
                grd.DataSource = new String[] { };
                return;
            }

            grdList.DataSource = TransCharges;
        }

        private DataTable TransCharges
        {
            get
            {
                string[] patIdSearchs = null;
                if (txtRegistrationNo.Text != string.Empty && !txtRegistrationNo.Text.ToLower().Contains("reg"))
                {
                    patIdSearchs = Helper.PatientIds(txtRegistrationNo.Text, false);
                }
                else if (txtPatientName.Text != string.Empty)
                {
                    patIdSearchs = Helper.PatientIds(txtPatientName.Text, true);
                }

                if (patIdSearchs != null && patIdSearchs.Length == 0) return null; // Tidak ada patientnya

                var query = new TransChargesItemQuery("a");
                var tcq = new TransChargesQuery("aa");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var unit2 = new ServiceUnitQuery("e");
                var room = new ServiceRoomQuery("f");
                var item = new ItemQuery("g");
                var guar = new GuarantorQuery("h");
                var sumInfo = new RegistrationInfoSumaryQuery("sum");
                var sal = new AppStandardReferenceItemQuery("sal");
                var casemixguar = new CasemixCoveredGuarantorQuery("casemixg");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.TransactionNo,
                        query.ReferenceNo,
                        tcq.TransactionDate,
                        tcq.ExecutionDate,
                        unit.ServiceUnitName,
                        tcq.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        unit2.ServiceUnitName.As("ClusterName"),
                        room.RoomName,
                        tcq.BedID,
                        tcq.TransactionDate.Date().As("Group"),
                        @"<'' AS [PaymentNo]>",
                        @"<CONVERT(VARCHAR(5), aa.TransactionDate, 114) AS [TransactionTime]>",
                        tcq.ToServiceUnitID.As("ServiceUnitID"),
                        reg.PatientID,
                        guar.GuarantorName,
                        sumInfo.NoteCount,
                        @"<CAST(CONVERT(VARCHAR(10), aa.ExecutionDate, 101) AS DATETIME) AS [ExecDate]>",
                        "<case when (select count(*) from TransChargesItem x where x.TransactionNo = a.TransactionNo and x.IsOrderRealization = 0 and x.IsVoid = 0 and x.IsCito = 1) > 0 then 1 else 0 end as IsCitoAvailable>",
                        sal.ItemName.As("SalutationName"),
                        tcq.Notes,
                        @"< CASE WHEN ISNULL(aa.SRBloodSampleTakenBy, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS [IsBloodSampleVisible]>",
                        @"<'' AS SpecimenStatus>",
                        @"<CASE WHEN casemixg.GuarantorID IS NULL THEN CAST(0 AS BIT) ElSE CAST(1 AS BIT) END AS IsGuarantorBpjsCasemix>"
                    );

                if (grdList.Columns.FindByUniqueName("NeedValidationByCasemix").Visible)
                    query.Select(@"<ISNULL((SELECT TOP 1 CAST(1 AS BIT) AS x FROM TransChargesItem AS tci
                        WHERE tci.TransactionNo = aa.TransactionNo AND ISNULL(tci.IsCasemixApproved, 0) = 0 AND tci.CasemixApprovedDateTime IS NULL), CAST(0 AS BIT)) AS 'IsNeedValidationByCasemix'>");
                else
                    query.Select(@"<CAST(0 AS BIT) AS IsNeedValidationByCasemix>");

                query.InnerJoin(tcq).On(query.TransactionNo == tcq.TransactionNo);
                query.InnerJoin(reg).On(tcq.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(unit).On(tcq.ToServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(unit2).On(tcq.FromServiceUnitID == unit2.ServiceUnitID);
                query.LeftJoin(room).On(tcq.RoomID == room.RoomID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
                query.LeftJoin(sumInfo).On(tcq.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(casemixguar).On(casemixguar.GuarantorID == reg.GuarantorID);

                if (Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(
                        tcq.ToServiceUnitID == qusr.ServiceUnitID &&
                        qusr.UserID == AppSession.UserLogin.UserID
                        );
                }

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                {
                    if (rblFilterDate.SelectedValue == "OD")
                        query.Where(tcq.TransactionDate >= txtOrderDate1.SelectedDate, tcq.TransactionDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                    else
                        query.Where(tcq.ExecutionDate >= txtOrderDate1.SelectedDate, tcq.ExecutionDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(tcq.FromServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboToServiceUnitID.SelectedValue != string.Empty)
                    query.Where(tcq.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    if (txtRegistrationNo.Text.ToLower().Contains("reg"))
                    {
                        string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                        query.Where(reg.RegistrationNo == searchReg);
                    }
                    else if (patIdSearchs != null && patIdSearchs.Length > 0)
                        query.Where(reg.PatientID.In(patIdSearchs), patient.PatientID.In(patIdSearchs));

                }
                else if (txtPatientName.Text != string.Empty)
                {
                    if (patIdSearchs != null && patIdSearchs.Length > 0)
                        query.Where(reg.PatientID.In(patIdSearchs), patient.PatientID.In(patIdSearchs));
                }

                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(reg.GuarantorID == cboGuarantorID.SelectedValue);

                if (txtTransactionNo.Text != string.Empty)
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                }
                query.Where
                    (
                        query.IsOrderRealization == false,
                        query.IsVoid == false,
                        //tcq.TransactionNo.Substring(1, 2) == "JO",
                        tcq.TransactionNo.Like("JO%"),
                        tcq.IsApproved == true,
                        tcq.IsOrder == true,
                        //reg.IsHoldTransactionEntry == false,
                        query.Or(query.IsSelectedExtraItem == true, query.IsSelectedExtraItem.IsNull())
                    );
                query.Where(query.ParentNo == string.Empty);

                if (AppSession.Parameter.IsJobOrderRealizationNeedConfirm)
                {
                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "GRHA")
                        query.Where(query.Or(reg.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp,
                                             query.IsPaymentConfirmed == true));
                    else
                        query.Where(query.IsPaymentConfirmed.IsNotNull(), query.IsPaymentConfirmed == true);
                }

                query.es.Distinct = true;

                if (AppSession.Parameter.IsJobOrderRealizationListByOrderDate)
                    query.OrderBy(tcq.TransactionDate.Descending);
                else
                    query.OrderBy(tcq.ExecutionDate.Descending);

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var pay = new TransPaymentItemOrderQuery();
                    pay.Where(pay.TransactionNo == row["TransactionNo"], pay.IsPaymentProceed == true, pay.IsPaymentReturned == false);
                    pay.Select(pay.PaymentNo);
                    pay.es.Top = 1;
                    DataTable pTable = pay.LoadDataTable();
                    if (pTable.Rows.Count > 0)
                    {
                        row["PaymentNo"] = pTable.Rows[0]["PaymentNo"].ToString();
                    }

                    if (Convert.ToBoolean(row["IsBloodSampleVisible"]) == true & row["ServiceUnitID"].ToString() == AppSession.Parameter.ServiceUnitLaboratoryID & AppSession.Parameter.IsNeedBloodSample)
                    {
                        var jSpecimen = new TransChargesItemCollection();
                        jSpecimen.Query.Where(jSpecimen.Query.TransactionNo == row["TransactionNo"].ToString(), jSpecimen.Query.SpecimenReceivedDateTime.IsNotNull());
                        jSpecimen.LoadAll();

                        if (jSpecimen.Count == 0)
                            row["SpecimenStatus"] = "0";
                        else
                        {
                            var jItems = new TransChargesItemCollection();
                            jItems.Query.Where(jItems.Query.TransactionNo == row["TransactionNo"].ToString());
                            jItems.LoadAll();

                            if (jSpecimen.Count == jItems.Count)
                                row["SpecimenStatus"] = "2";
                            else row["SpecimenStatus"] = "1";
                        }
                    }
                }

                tbl.AcceptChanges();

                var listDelete = new List<DataRow>();
                foreach (DataRow row in tbl.Rows)
                {
                    var tci = new TransChargesItemCollection();
                    tci.Query.Where(tci.Query.TransactionNo == row["TransactionNo"].ToString(), tci.Query.IsVoid == false);
                    tci.Query.Load();
                    var transCount = tci.Count();
                    var rejectCount = tci.Count(t => t.IsCasemixApproved == false && !string.IsNullOrEmpty(t.CasemixApprovedByUserID));
                    //if (transCount == rejectCount)
                    {
                        if (trCasemix.Visible)
                        {
                            if (chkStatusRejected.Checked)
                            {
                                if (rejectCount == 0) listDelete.Add(row);
                            }
                            else
                            {
                                if (rejectCount > 0)
                                    if (transCount == rejectCount)
                                        listDelete.Add(row);
                            }
                        }
                    }
                }

                if (listDelete.Any())
                {
                    foreach (var row in listDelete)
                    {
                        tbl.Rows.Remove(row);
                    }
                }
                tbl.AcceptChanges();

                if (AppSession.Parameter.IsJobOrderRealizationListByCitoStatus)
                {
                    DataTable newDataTable;
                    if (AppSession.Parameter.IsJobOrderRealizationListByOrderDate)
                    {
                        newDataTable = tbl.AsEnumerable()
                       .OrderByDescending(r => r.Field<DateTime>("Group")).ThenByDescending(r => r.Field<int>("IsCitoAvailable"))
                       .CopyToDataTable();
                    }
                    else
                    {
                        newDataTable = tbl.AsEnumerable()
                       .OrderByDescending(r => r.Field<DateTime>("ExecutionDate")).ThenByDescending(r => r.Field<int>("IsCitoAvailable"))
                       .CopyToDataTable();
                    }

                    return newDataTable;
                }

                return tbl;
            }
        }


        protected void grdList2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                var grd = (RadGrid)source;
                grd.DataSource = new String[] { };
                return;
            }

            if (AppSession.Parameter.IsJobOrderRealizationListWith2Tabs)
                grdList2.DataSource = TransCharges2;
            else grdList2.DataSource = null;
        }

        private DataTable TransCharges2
        {
            get
            {
                string[] patIdSearchs = null;
                if (txtRegistrationNo.Text != string.Empty && !txtRegistrationNo.Text.ToLower().Contains("reg"))
                {
                    patIdSearchs = Helper.PatientIds(txtRegistrationNo.Text, false);
                }
                else if (txtPatientName.Text != string.Empty)
                {
                    patIdSearchs = Helper.PatientIds(txtPatientName.Text, true);
                }

                if (patIdSearchs != null && patIdSearchs.Length == 0) return null; // Tidak ada patientnya

                var query = new TransChargesItemQuery("a");
                var tcq = new TransChargesQuery("aa");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var unit2 = new ServiceUnitQuery("e");
                var room = new ServiceRoomQuery("f");
                var item = new ItemQuery("g");
                var guar = new GuarantorQuery("h");
                var sumInfo = new RegistrationInfoSumaryQuery("sum");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.TransactionNo,
                        query.ReferenceNo,
                        tcq.TransactionDate,
                        tcq.ExecutionDate,
                        unit.ServiceUnitName,
                        tcq.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        unit2.ServiceUnitName.As("ClusterName"),
                        room.RoomName,
                        tcq.BedID,
                        tcq.TransactionDate.Date().As("Group"),
                        @"<'' AS [PaymentNo]>",
                        @"<CONVERT(VARCHAR(5), aa.TransactionDate, 114) AS [TransactionTime]>",
                        tcq.ToServiceUnitID.As("ServiceUnitID"),
                        reg.PatientID,
                        guar.GuarantorName,
                        sumInfo.NoteCount,
                        @"<CAST(CONVERT(VARCHAR(10), aa.ExecutionDate, 101) AS DATETIME) AS [ExecDate]>",
                        "<case when (select count(*) from TransChargesItem x where x.TransactionNo = a.TransactionNo and x.IsOrderRealization = 0 and x.IsVoid = 0 and x.IsCito = 1) > 0 then 1 else 0 end as IsCitoAvailable>",
                        sal.ItemName.As("SalutationName"),
                        tcq.Notes,
                        @"< CASE WHEN ISNULL(aa.SRBloodSampleTakenBy, '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS [IsBloodSampleVisible]>",
                        @"<'' AS SpecimenStatus>"
                    );

                query.InnerJoin(tcq).On(query.TransactionNo == tcq.TransactionNo);
                query.InnerJoin(reg).On(tcq.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(unit).On(tcq.ToServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(unit2).On(tcq.FromServiceUnitID == unit2.ServiceUnitID);
                query.LeftJoin(room).On(tcq.RoomID == room.RoomID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
                query.LeftJoin(sumInfo).On(tcq.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (Request.QueryString["disch"] == "0")
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(
                        tcq.ToServiceUnitID == qusr.ServiceUnitID &&
                        qusr.UserID == AppSession.UserLogin.UserID
                        );
                }

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                {
                    if (rblFilterDate.SelectedValue == "OD")
                        query.Where(tcq.TransactionDate >= txtOrderDate1.SelectedDate, tcq.TransactionDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                    else
                        query.Where(tcq.ExecutionDate >= txtOrderDate1.SelectedDate, tcq.ExecutionDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(tcq.FromServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboToServiceUnitID.SelectedValue != string.Empty)
                    query.Where(tcq.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

                //if (txtRegistrationNo.Text != string.Empty)
                //{
                //    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                //    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                //    query.Where(
                //        query.Or(
                //            reg.RegistrationNo == searchReg,
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );
                //}

                if (txtRegistrationNo.Text != string.Empty)
                {
                    if (txtRegistrationNo.Text.ToLower().Contains("reg"))
                    {
                        string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                        query.Where(reg.RegistrationNo == searchReg);
                    }
                    else if (patIdSearchs != null && patIdSearchs.Length > 0)
                        query.Where(reg.PatientID.In(patIdSearchs), patient.PatientID.In(patIdSearchs));

                }
                else if (txtPatientName.Text != string.Empty)
                {
                    if (patIdSearchs != null && patIdSearchs.Length > 0)
                        query.Where(reg.PatientID.In(patIdSearchs), patient.PatientID.In(patIdSearchs));
                }

                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(reg.GuarantorID == cboGuarantorID.SelectedValue);

                if (txtTransactionNo.Text != string.Empty)
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                }
                //if (txtPatientName.Text != string.Empty)
                //{
                //    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                //    query.Where(patient.FullName.Like(searchPatient));
                //}

                query.Where
                    (
                        query.IsOrderRealization == true,
                        query.IsVoid == false,
                        tcq.TransactionNo.Substring(1, 2) == "JO",
                        tcq.IsApproved == true,
                        tcq.IsOrder == true,
                        //reg.IsHoldTransactionEntry == false,
                        query.Or(query.IsSelectedExtraItem == true, query.IsSelectedExtraItem.IsNull())
                    );
                query.Where(query.ParentNo == string.Empty);

                query.es.Distinct = true;

                if (AppSession.Parameter.IsJobOrderRealizationListByOrderDate)
                    query.OrderBy(tcq.TransactionDate.Descending);
                else
                    query.OrderBy(tcq.ExecutionDate.Descending);

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var pay = new TransPaymentItemOrderQuery();
                    pay.Where(pay.TransactionNo == row["TransactionNo"], pay.IsPaymentProceed == true, pay.IsPaymentReturned == false);
                    pay.Select(pay.PaymentNo);
                    pay.es.Top = 1;
                    DataTable pTable = pay.LoadDataTable();
                    if (pTable.Rows.Count > 0)
                    {
                        row["PaymentNo"] = pTable.Rows[0]["PaymentNo"].ToString();
                    }
                }

                tbl.AcceptChanges();

                return tbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
            if (AppSession.Parameter.IsJobOrderRealizationListWith2Tabs)
                grdList2.Rebind();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "rebind")
                grdList.Rebind();
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "TransactionNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobReportID = AppConstant.Report.TestReceipt;
                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winProcess.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintJoNotes")
            {
                var jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "TransactionNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobReportID = AppConstant.Report.JobOrderNotes;
                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winProcess.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }

            else if (e.CommandName == "PrintLabel1")
            {
                var jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "TransactionNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobReportID = AppConstant.Report.PrintLabel1;
                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winProcess.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }



            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            {
                ((GridDataItem)e.Item).ChildItem.FindControl("InnerContainer").Visible = !e.Item.Expanded;
                ((GridDataItem)e.Item).ChildItem.FindControl("Panel1").Visible = !e.Item.Expanded;
            }
        }

        protected void grdList_PreRender(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    grdList.MasterTableView.Items[0].Expanded = true;
            //    grdList.MasterTableView.Items[0].ChildItem.FindControl("InnerContainer").Visible = true;
            //}
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master")
                {
                    dataItem.ToolTip = dataItem["Notes"].Text;
                }
            }
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            var gridNestedViewItem = e.Item as GridNestedViewItem;
            if (gridNestedViewItem != null)
            {
                e.Item.FindControl("InnerContainer").Visible = (gridNestedViewItem).ParentItem.Expanded;
                e.Item.FindControl("Panel1").Visible = (gridNestedViewItem).ParentItem.Expanded;
            }
        }

        protected void grdList2_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "PrintTransactionReceipt")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "TransactionNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                var usr = new AppUser();
                usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                PrintJobParameter jobParameter2;

                jobParameter2 = jobParameters.AddNew();
                jobParameter2.Name = "UserName";
                jobParameter2.ValueString = usr.UserName;

                AppSession.PrintJobReportID = AppConstant.Report.TransactionReceipt;
                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winProcess.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "PrintLabel1")
            {
                var jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "TransactionNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobReportID = AppConstant.Report.PrintLabel1;
                AppSession.PrintJobParameters = jobParameters;

                string script = @"var oWnd = $find('" + winProcess.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        protected void grdList2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master")
                {
                    dataItem.ToolTip = dataItem["Notes"].Text;
                }

                //foreach (GridDataItem dataItem2 in grdList2.MasterTableView.Items)
                //{
                //    var tc = new TransCharges();
                //    if (tc.LoadByPrimaryKey(dataItem2.GetDataKeyValue("TransactionNo").ToString()))
                //    {
                //        grdList2.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ExposureFactor").Visible = (tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID || tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2 || AppSession.Parameter.ServiceUnitRadiologyIdArray.Contains(tc.ToServiceUnitID));
                //    }
                //}
            }
        }

        protected void grdList2_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            switch (e.DetailTableView.Name)
            {
                case "detail":
                    {
                        gridListLoadDetail(e);
                        break;
                    }
            }
        }

        private void gridListLoadDetail(GridDetailTableDataBindEventArgs e)
        {
            var query = new TransChargesItemQuery("a");
            var item = new ItemQuery("b");
            var header = new TransChargesQuery("d");
            var tci = new TransChargesItemQuery("e");
            var reg = new RegistrationQuery("f");
            var itemlab = new ItemLaboratoryQuery("il");
            var stype = new AppStandardReferenceItemQuery("st");

            var total = new esQueryItem(query, "Total", esSystemType.Decimal);
            total = query.ChargeQuantity * ((query.Price - query.DiscountAmount) + query.CitoAmount);

            tci.Select(tci.TransactionNo, tci.SequenceNo);
            tci.Where
                (
                    tci.TransactionNo == query.TransactionNo,
                    tci.SequenceNo == query.SequenceNo,
                    tci.IsExtraItem == true,
                    tci.IsSelectedExtraItem == false
                );
            query.Select
                (
                    query,
                    total.As("refToTransChargesItem_Total"),
                    item.ItemName.As("ItemName"),
                    header.RegistrationNo.As("RegistrationNo"),
                    header.ToServiceUnitID.As("refToTransCharges_ToServiceUnitID"),
                    item.SRItemType.As("refToItem_SRItemType"),
                    "<CAST((CASE WHEN b.SRItemType IN ('" + ItemType.Medical + "', '" + ItemType.NonMedical + "') THEN 0 ELSE 1 END) AS BIT) AS refTo_IsItemTypeService>",
                    "<'' as refTo_PrevOrder>",
                    reg.SRRegistrationType.As("refToRegistration_SRRegistrationType"),
                    @"<CASE WHEN st.ItemName IS NULL THEN '' ELSE 'Specimen Type: ' + st.ItemName END AS 'SpecimenTypeName'>",
                    @"<CASE WHEN ISNULL(d.SRBloodSampleTakenBy, '') = '' THEN '' ELSE (CASE WHEN a.SpecimenReceivedDateTime IS NULL THEN '0' ELSE '1' END) END AS 'refTo_SpecimenStatus'>"
                );
            query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(itemlab).On(itemlab.ItemID == query.ItemID);
            query.LeftJoin(stype).On(stype.StandardReferenceID == "SpecimenType" & stype.ItemID == itemlab.SRSpecimenType);
            query.Where
                (
                    query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString(),
                    query.IsVoid == false,
                    query.IsBillProceed == true,
                    query.IsOrderRealization == true,
                    query.NotExists(tci)
                );
            query.Where(query.ParentNo == string.Empty);
            query.Where(query.ChargeQuantity > 0);
            query.OrderBy(query.SequenceNo.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
            if (AppSession.Parameter.IsJobOrderRealizationListWith2Tabs)
                grdList2.Rebind();
        }

        public static DataTable SelectPhrForm(string serviceUnitID, string registrationNo)
        {
            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");
            var phrQr = new PatientHealthRecordQuery("b");
            var empQr = new EmployeeQuery("e");

            query.InnerJoin(suQr).On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);
            query.LeftJoin(phrQr).On(phrQr.QuestionFormID == query.QuestionFormID && phrQr.RegistrationNo == registrationNo);
            query.LeftJoin(empQr).On(phrQr.EmployeeID == empQr.EmployeeID);

            query.Where(query.IsActive == true);

            query.Select(string.Format("<'{0}' as RegistrationNo>", registrationNo),
                query.QuestionFormID,
                @"<CASE WHEN a.QuestionFormID = 'MEDHIS' then 1
                        WHEN a.QuestionFormID = 'PHYEXAM' THEN 2
                        WHEN a.QuestionFormID = 'SUMMARY' THEN 3
                        ELSE 4 END AS formID>",
                query.QuestionFormName,
                query.IsMCUForm,
                phrQr.RecordDate,
                phrQr.EmployeeID,
                empQr.EmployeeName,
                phrQr.IsComplete
                );

            return query.LoadDataTable();
        }

        protected void grdTransaction_Init(object sender, EventArgs e)
        {
            InitializeCultureGrid((RadGrid)sender);
        }

        protected void grdPHR_ItemCommand(object source, GridCommandEventArgs e)
        {
            var pars = e.CommandArgument.ToString().Split('_');
            var jobParameters = new PrintJobParameterCollection();
            jobParameters.AddNew("p_RegistrationNo", pars[0]);
            jobParameters.AddNew("p_QuestionFormID", pars[1]);

            AppSession.PrintJobParameters = jobParameters;

            switch (e.CommandName)
            {
                case "1":
                    AppSession.PrintJobReportID = AppConstant.Report.MedicalHistory;
                    break;
                case "2":
                    AppSession.PrintJobReportID = AppConstant.Report.PhysicalExam;
                    break;
                case "3":
                    AppSession.PrintJobReportID = AppConstant.Report.ExaminationSummary;
                    break;
                default:
                    AppSession.PrintJobReportID = AppConstant.Report.SystemicExam;
                    break;
            }


            string script = @"var oWnd = $find('" + winPrint.ClientID + @"');oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + @"');
                                oWnd.Show(); oWnd.Maximize();";

            radAjaxPanel.ResponseScripts.Add(script);

        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var guar = new GuarantorQuery("a");
            guar.es.Top = 20;
            guar.Where(
                guar.GuarantorName.Like(searchTextContain),
                guar.IsActive == true
                );

            cboGuarantorID.DataSource = guar.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void chkStatusRejected_CheckedChanged(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        protected void rblFilterDate_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        //protected override void InitializeControlFromCookie(Control ctl, object value)
        //{
        //    if (ctl.ID.ToLower().Equals(cboGuarantorID.ID.ToLower()) && value != null)
        //    {
        //        var query = new GuarantorQuery();
        //        query.es.Top = 1;
        //        query.Select
        //            (
        //                query.GuarantorID,
        //                query.GuarantorName
        //            );
        //        query.Where(query.GuarantorID == value);

        //        cboGuarantorID.DataSource = query.LoadDataTable();
        //        cboGuarantorID.DataBind();
        //    }
        //}
    }
}
