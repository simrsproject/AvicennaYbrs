using System;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class TestResultList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.TestResult;

            IsShowValueFromCookie = true;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, false);
                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboPhysician.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboPhysician.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                cboPhysician.SelectedValue = AppSession.UserLogin.ParamedicID;
                cboPhysician.Enabled = string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (AppSession.Parameter.IsTestResultListWithDefaultOutstanding)
                {
                    txtOrderDate1.SelectedDate = DateTime.Now.Date.AddDays(-7);
                    txtOrderDate2.SelectedDate = DateTime.Now.Date;
                    cboStatus.SelectedValue = "1";
                }
                else
                {
                    txtOrderDate1.SelectedDate = DateTime.Now.Date;
                    txtOrderDate2.SelectedDate = DateTime.Now.Date;
                }
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return;
            }
            
            var dataSource = TestResults;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        private DataTable TestResults
        {
            get 
            {
                var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(txtTransactionNo.Text) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && 
                    string.IsNullOrEmpty(txtFlmNo.Text) && string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboPhysician.SelectedValue) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue) && 
                    string.IsNullOrEmpty(cboItemID.SelectedValue) && string.IsNullOrEmpty(cboStatus.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Patient")) return null;

                var table = TestResultNonLaboratory;
                //table.Merge(TestResultsByPackage); // remark by teguh s 171115 muncul double di test result
                table.Merge(TestResultLaboratory);

                if (table.Rows.Count > 0)
                {
                    var tci = new TransChargesItemCollection();
                    var correction = tci.TransChargesItemWithCorrection(table.AsEnumerable().Select(t => t.Field<string>("TransactionNo")));

                    foreach (DataRow row in table.AsEnumerable()
                        .Where(t => (correction.AsEnumerable()
                            .Select(c => c.Field<string>("TransactionNo") + c.Field<string>("SequenceNo"))).Contains(t.Field<string>("TransactionNo") + t.Field<string>("SequenceNo"))))
                    {
                        row.Delete();
                    }
                    table.AcceptChanges();
                }

                var cl = table.Columns.Add("PaymentNo", typeof(string));

                var TransNos = table.AsEnumerable().Select(x => x.Field<string>("TransactionNo")).ToArray();
                if (TransNos.Length > 0)
                {
                    var TPNos = (new TransPaymentCollection()).GetPaymentNoByTransactionNo(TransNos);
                    foreach (DataRow row in table.Rows)
                    {
                        row["PaymentNo"] = TPNos.AsEnumerable().Where(x =>
                            x.Field<string>("TransactionNo") == row["TransactionNo"].ToString() &&
                            x.Field<string>("SequenceNo") == row["SequenceNo"].ToString())
                            .Select(x => x.Field<string>("PaymentNo")).FirstOrDefault();
                    }
                }

                table.AcceptChanges();

                return table;
            }
        }

        private DataTable TestResultNonLaboratory
        {
            get
            {
                var qChargesItem = new TransChargesItemQuery("a");
                var qCharges = new TransChargesQuery("t");
                var qTestResult = new TestResultQuery("res");
                var qItem = new ItemQuery("c");
                var qReg = new RegistrationQuery("r");
                var qPatient = new PatientQuery("p");
                var qToUnit = new ServiceUnitQuery("su");
                var sal = new AppStandardReferenceItemQuery("sal");
                var qGrr = new GuarantorQuery("grr");
                
                qChargesItem.InnerJoin(qCharges).On(qCharges.TransactionNo == qChargesItem.TransactionNo);
                qChargesItem.LeftJoin(qTestResult).On(
                    qChargesItem.TransactionNo == qTestResult.TransactionNo &&
                    qChargesItem.ItemID == qTestResult.ItemID
                    );
                qChargesItem.InnerJoin(qItem).On(
                    qChargesItem.ItemID == qItem.ItemID &&
                    qItem.IsHasTestResults == true
                    );
                qChargesItem.InnerJoin(qReg).On(
                    qCharges.RegistrationNo == qReg.RegistrationNo
                    );
                qChargesItem.InnerJoin(qPatient).On(qReg.PatientID == qPatient.PatientID);
                qChargesItem.InnerJoin(qToUnit).On(qCharges.ToServiceUnitID == qToUnit.ServiceUnitID);
                qChargesItem.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qPatient.SRSalutation == sal.ItemID);
                qChargesItem.LeftJoin(qGrr).On(qGrr.GuarantorID == qReg.GuarantorID);

                qChargesItem.Select(
                    qCharges.RegistrationNo,
                    qChargesItem.TransactionNo,
                    qChargesItem.SequenceNo,
                    qChargesItem.ItemID,
                    qTestResult.ParamedicID,
                    qTestResult.TestResultDateTime,
                    @"<ISNULL((SELECT TOP 1 p.ParamedicName FROM TransChargesItemComp AS tcic 
INNER JOIN Paramedic AS p ON p.ParamedicID = tcic.ParamedicID
WHERE tcic.TransactionNo = a.TransactionNo AND tcic.SequenceNo = a.SequenceNo AND tcic.ParamedicID <> ''
ORDER BY tcic.TariffComponentID), '') AS ParamedicName>",
                    //qChargesItem.ParamedicCollectionName.As("ParamedicName"),
                    qItem.ItemName,
                    qChargesItem.IsCito,
                    qPatient.PatientName,
                    qPatient.MedicalNo,
                    qCharges.ToServiceUnitID,
                    qToUnit.ServiceUnitName.As("ToServiceUnitName"),
                    qChargesItem.RealizationDateTime.As("RealizationDateTime"),
                    qTestResult.TestResult.Substring(100).As("TestResult"),
                    sal.ItemName.As("SalutationName"),
                    qChargesItem.FilmNo,
                    "<'' AS ResultUrl>",
                    qChargesItem.CommunicationID,
                    qItem.SRItemType,
                    "<'' AS ResultTitle>",
                    qGrr.GuarantorName
                );

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qChargesItem.Where(qCharges.TransactionDate >= txtOrderDate1.SelectedDate, qCharges.TransactionDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                if (cboToServiceUnitID.SelectedValue != string.Empty)
                    qChargesItem.Where(qCharges.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboPhysician.Text))
                {
                    if (!string.IsNullOrEmpty(AppSession.Parameter.RadiologyParamedicId))
                    {
                        qChargesItem.Where(qChargesItem.Or(qChargesItem.ParamedicID == cboPhysician.SelectedValue,
                        string.Format("< OR (ISNULL((SELECT TOP 1 p.ParamedicName FROM TransChargesItemComp AS tcic INNER JOIN Paramedic AS p ON p.ParamedicID = tcic.ParamedicID WHERE tcic.TransactionNo = a.TransactionNo AND tcic.SequenceNo = a.SequenceNo AND tcic.ParamedicID <> '' ORDER BY tcic.TariffComponentID), '')) = '{0}'>", cboPhysician.Text),
                        qChargesItem.ParamedicID == AppSession.Parameter.RadiologyParamedicId));
                    }
                    else
                    {
                        qChargesItem.Where(qChargesItem.Or(qChargesItem.ParamedicID == cboPhysician.SelectedValue,
                        string.Format("< OR (ISNULL((SELECT TOP 1 p.ParamedicName FROM TransChargesItemComp AS tcic INNER JOIN Paramedic AS p ON p.ParamedicID = tcic.ParamedicID WHERE tcic.TransactionNo = a.TransactionNo AND tcic.SequenceNo = a.SequenceNo AND tcic.ParamedicID <> '' ORDER BY tcic.TariffComponentID), '')) = '{0}'>", cboPhysician.Text)));
                    }
                }
                else
                {
                    var usr = new AppUserServiceUnitQuery("usr");
                    qChargesItem.InnerJoin(usr).On(usr.ServiceUnitID == qCharges.ToServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
                }
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    qChargesItem.Where(
                        qChargesItem.Or(
                            qReg.RegistrationNo == searchReg,
                            qPatient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            qPatient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    qChargesItem.Where(
                    //        qChargesItem.Or(
                    //            qCharges.RegistrationNo == searchReg,
                    //            qPatient.MedicalNo == searchReg,
                    //            qPatient.OldMedicalNo == searchReg,
                    //            string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    qChargesItem.Where(
                    //        qChargesItem.Or(
                    //            qCharges.RegistrationNo == searchReg,
                    //            qPatient.MedicalNo == searchReg,
                    //            qPatient.OldMedicalNo == searchReg,
                    //            string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }
                
                if (txtTransactionNo.Text != string.Empty)
                {
                    string searchTransactionNo = "%" + txtTransactionNo.Text + "%";
                    qChargesItem.Where(string.Format("<a.TransactionNo LIKE '{0}'>", searchTransactionNo));
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    qChargesItem.Where(qPatient.FullName.Like(searchPatient));

                    //qChargesItem.Where
                    //    (
                    //      string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                    //    );
                }
                if (txtFlmNo.Text != string.Empty) 
                {
                    string searchFilmNo = "%" + txtFlmNo.Text + "%";
                    qChargesItem.Where(string.Format("<a.FilmNo LIKE '{0}'>", searchFilmNo));
                }
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    qChargesItem.Where(qReg.GuarantorID == cboGuarantorID.SelectedValue);
                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    qChargesItem.Where(qChargesItem.ItemID == cboItemID.SelectedValue);
                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    if (cboStatus.SelectedValue == "1")
                        qChargesItem.Where(qTestResult.TestResult.IsNull());
                    else
                        qChargesItem.Where(qTestResult.TestResult.IsNotNull());
                }

                qChargesItem.Where(qChargesItem.IsBillProceed == true, qCharges.IsCorrection == false, qItem.SRItemType != ItemType.Laboratory);
                
                qChargesItem.es.Top = AppSession.Parameter.MaxResultRecord;

                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                    qChargesItem.OrderBy(qCharges.TransactionDate.Descending, qChargesItem.TransactionNo.Descending);
                else
                    qChargesItem.OrderBy(qCharges.TransactionDate.Ascending, qChargesItem.TransactionNo.Ascending);

                DataTable dtb = qChargesItem.LoadDataTable();

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM")
                {
                    var isUpdated = false;
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["SRItemType"].ToString() != ItemType.Radiology) continue;
                        var hl7 = new HL7Message();
                        hl7.Query.es.Top = 1;
                        hl7.Query.Where(hl7.Query.RefferenceNo == row["CommunicationID"].ToString());
                        hl7.Query.OrderBy(hl7.Query.MessageDateTime.Descending);
                        if (!hl7.Query.Load()) continue;
                        if (string.IsNullOrEmpty(hl7.Message)) continue;
                        row["ResultUrl"] = Common.HL7Helper.GetResultUrl(hl7.Message);
                        row["ResultTitle"] = row["ItemID"];
                        isUpdated = true;
                    }
                    if (isUpdated) dtb.AcceptChanges();
                }
                return dtb;
            }
        }

        private DataTable TestResultLaboratory
        {
            get
            {
                var qChargesItem = new TransChargesItemQuery("a");
                var qCharges = new TransChargesQuery("t");
                var qTestResult = new TestResultQuery("res");
                var qItem = new ItemQuery("c");
                var qReg = new RegistrationQuery("r");
                var qPatient = new PatientQuery("p");
                var qToUnit = new ServiceUnitQuery("su");
                var sal = new AppStandardReferenceItemQuery("sal");
                var qGrr = new GuarantorQuery("grr");
                
                qChargesItem.InnerJoin(qCharges).On(qCharges.TransactionNo == qChargesItem.TransactionNo);
                qChargesItem.LeftJoin(qTestResult).On(
                    qChargesItem.TransactionNo == qTestResult.TransactionNo &&
                    qChargesItem.ItemID == qTestResult.ItemID
                    );
                qChargesItem.InnerJoin(qItem).On(
                    qChargesItem.ItemID == qItem.ItemID &&
                    qItem.IsHasTestResults == true
                    );
                qChargesItem.InnerJoin(qReg).On(
                    qCharges.RegistrationNo == qReg.RegistrationNo
                    );
                qChargesItem.InnerJoin(qPatient).On(qReg.PatientID == qPatient.PatientID);
                qChargesItem.InnerJoin(qToUnit).On(qCharges.ToServiceUnitID == qToUnit.ServiceUnitID);
                qChargesItem.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qPatient.SRSalutation == sal.ItemID);
                qChargesItem.LeftJoin(qGrr).On(qGrr.GuarantorID == qReg.GuarantorID);
                
                qChargesItem.Select(
                    qCharges.RegistrationNo,
                    qChargesItem.TransactionNo,
                    qChargesItem.SequenceNo,
                    qChargesItem.ItemID,
                    qTestResult.ParamedicID,
                    qTestResult.TestResultDateTime,
                    @"<ISNULL((SELECT TOP 1 p.ParamedicName FROM TransChargesItemComp AS tcic 
INNER JOIN Paramedic AS p ON p.ParamedicID = tcic.ParamedicID
WHERE tcic.TransactionNo = a.TransactionNo AND tcic.SequenceNo = a.SequenceNo AND tcic.ParamedicID <> ''
ORDER BY tcic.TariffComponentID), '') AS ParamedicName>",
                    //qChargesItem.ParamedicCollectionName.As("ParamedicName"),
                    qItem.ItemName,
                    qChargesItem.IsCito,
                    qPatient.PatientName,
                    qPatient.MedicalNo,
                    qCharges.ToServiceUnitID,
                    qToUnit.ServiceUnitName.As("ToServiceUnitName"),
                    qChargesItem.RealizationDateTime.As("RealizationDateTime"),
                    qTestResult.TestResult.Substring(100).As("TestResult"), 
                    sal.ItemName.As("SalutationName"),
                    qChargesItem.FilmNo,
                    "<'' AS ResultUrl>",
                    qChargesItem.CommunicationID,
                    qItem.SRItemType,
                    qGrr.GuarantorName
                );

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    qChargesItem.Where(qCharges.TransactionDate >= txtOrderDate1.SelectedDate, qCharges.TransactionDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
                if (cboToServiceUnitID.SelectedValue != string.Empty)
                    qChargesItem.Where(qCharges.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboPhysician.Text))
                    qChargesItem.Where(qChargesItem.Or(qChargesItem.ParamedicID == cboPhysician.SelectedValue,
                        string.Format("< OR (ISNULL((SELECT TOP 1 p.ParamedicName FROM TransChargesItemComp AS tcic INNER JOIN Paramedic AS p ON p.ParamedicID = tcic.ParamedicID  WHERE tcic.TransactionNo = a.TransactionNo AND tcic.SequenceNo = a.SequenceNo AND tcic.ParamedicID <> '' ORDER BY tcic.TariffComponentID), '')) = '{0}'>", cboPhysician.Text)));
                else
                {
                    var usr = new AppUserServiceUnitQuery("usr");
                    qChargesItem.InnerJoin(usr).On(usr.ServiceUnitID == qCharges.ToServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
                }
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    qChargesItem.Where(
                        qChargesItem.Or(
                            qReg.RegistrationNo == searchReg,
                            qPatient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            qPatient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                    //    qChargesItem.Where(
                    //        qChargesItem.Or(
                    //            qCharges.RegistrationNo == searchReg,
                    //            qPatient.MedicalNo == searchReg,
                    //            qPatient.OldMedicalNo == searchReg,
                    //            string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                    //else
                    //    qChargesItem.Where(
                    //        qChargesItem.Or(
                    //            qCharges.RegistrationNo == searchReg,
                    //            qPatient.MedicalNo == searchReg,
                    //            qPatient.OldMedicalNo == searchReg,
                    //            string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                    //            string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
                    //            )
                    //        );
                }
                if (txtTransactionNo.Text != string.Empty)
                {
                    string searchTransactionNo = "%" + txtTransactionNo.Text + "%";
                    qChargesItem.Where(string.Format("<a.TransactionNo LIKE '{0}'>", searchTransactionNo));
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    qChargesItem.Where(qPatient.FullName.Like(searchPatient));

                    //qChargesItem.Where
                    //    (
                    //      string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                    //    );
                }
                if (txtFlmNo.Text != string.Empty)
                {
                    string searchFilmNo = "%" + txtFlmNo.Text + "%";
                    qChargesItem.Where(string.Format("<a.FilmNo LIKE '{0}'>", searchFilmNo));
                }
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    qChargesItem.Where(qReg.GuarantorID == cboGuarantorID.SelectedValue);
                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    qChargesItem.Where(qChargesItem.ItemID == cboItemID.SelectedValue);
                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    if (cboStatus.SelectedValue == "1")
                        qChargesItem.Where(qTestResult.TestResult.IsNull());
                    else
                        qChargesItem.Where(qTestResult.TestResult.IsNotNull());
                }

                qChargesItem.Where(qCharges.IsCorrection == false, qItem.SRItemType == ItemType.Laboratory, 
                    qChargesItem.Or(qChargesItem.IsDescriptionResult == true, qItem.IsHasTestResults == true));

                qChargesItem.es.Top = AppSession.Parameter.MaxResultRecord;
                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                    qChargesItem.OrderBy(qCharges.TransactionDate.Descending, qChargesItem.TransactionNo.Descending);
                else
                    qChargesItem.OrderBy(qCharges.TransactionDate.Ascending, qChargesItem.TransactionNo.Ascending);

                DataTable dtb = qChargesItem.LoadDataTable();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.CurrentPageIndex = 0;
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "rebind") grdList.Rebind();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var item = new ItemQuery();
            item.es.Top = 10;
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where
                (
                    item.SRItemType.In(ItemType.Service, ItemType.Laboratory, ItemType.Radiology),
                    item.Or
                     (
                         item.ItemID.Like(searchTextContain),
                         item.ItemName.Like(searchTextContain)
                     ),
                    item.IsActive == true
                );
            item.OrderBy(item.ItemID.Ascending);

            cboItemID.DataSource = item.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var grr = new GuarantorQuery();
            grr.es.Top = 10;
            grr.Select
                (
                    grr.GuarantorID,
                    grr.GuarantorName
                );
            grr.Where
                (
                    grr.Or
                     (
                         grr.GuarantorID.Like(searchTextContain),
                         grr.GuarantorName.Like(searchTextContain)
                     ),
                    grr.IsActive == true
                );
            grr.OrderBy(grr.GuarantorName.Ascending);

            cboGuarantorID.DataSource = grr.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }
    }
}
