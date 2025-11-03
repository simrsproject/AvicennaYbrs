using System;
using System.Drawing;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Text;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class PlafondInformationList : BasePage
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

            ProgramID = AppConstant.Program.BpjsPlafondInformation;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithParamedic(cboParamedicID);
                StandardReference.Initialize(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                cboSRRegistrationType.SelectedValue = AppConstant.RegistrationType.InPatient;
                trGuarantorID.Visible = AppSession.Parameter.IsVisibleGuarantorFilterOnPlafondInformationList;
                trPrescription23Days.Visible = AppSession.Parameter.IsVisible23PrescFilterOnPlafondInformationList;

                txtOverPlafondRange1.Value = 0;
                txtOverPlafondRange2.Value = 0;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack) RestoreValueFromCookie();
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry searching criteria {0}');", searchingLabel), true);
            }
            return false;
        }

        protected void grdRegistrationList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;            

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };                
                return;
            }

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;                
            }
        }

        private DataTable Registrations
        {
            get
            {
                //var isEmptyFilter = txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) &&
                //    string.IsNullOrEmpty(cboGuarantorID.SelectedValue) && string.IsNullOrEmpty(txtInitialDiagnosis.Text) && string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue) &&
                //    string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue);
                var isEmptyFilter = (chkIncludeClosedPatients.Checked || chkIncludeDischargedPatients.Checked) && 
                    txtRegistrationDate1.IsEmpty && txtRegistrationDate2.IsEmpty &&
                    txtDischargeDate1.IsEmpty && txtDischargeDate2.IsEmpty &&
                    string.IsNullOrEmpty(txtRegistrationNo.Text);

                if (!ValidateSearch(isEmptyFilter, "(Registration Date | Discharge Date | Registration No / Medical No)")) 
                    return null;

                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var guar = new GuarantorQuery("g");
                var infoSum = new RegistrationInfoSumaryQuery("h");
                var bpjsp = new BpjsPackageQuery("bp");
                var bd = new BedQuery("bd");
                var sal = new AppStandardReferenceItemQuery("sal");
                var cls = new ClassQuery("cls");
                var cls2 = new ClassQuery("cls2");

                qr.Select
                    (
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        qr.IsHoldTransactionEntry,
                        guar.GuarantorName,
                        qr.DischargePlanDate,
                        qr.IsConsul,
                        qr.SRRegistrationType,
                        infoSum.NoteCount,
                        qr.DischargeDate,
                        "<CASE WHEN r.DischargeDate IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarge>",
                        qr.ChargeClassID, qr.CoverageClassID, qr.ClassID,
                        @"<ISNULL(bd.DefaultChargeClassID, r.ChargeClassID) AS DefaultClassID>",
                        @"<CASE WHEN ISNULL(r.PlavonAmount, 0) = 0 THEN ISNULL(r.ApproximatePlafondAmount, 0) ELSE r.PlavonAmount END AS ApproximatePlafondAmount2>",
                        qr.PlavonAmount,
                        qr.ApproximatePlafondAmount,
                        qr.InitialDiagnose,
                        bpjsp.PackageName,
                        sal.ItemName.As("SalutationName"),
                        cls.ClassName.As("ChargeClassName"),
                        @"<CASE WHEN r.SRRegistrationType = 'IPR' THEN DATEDIFF(DAY, r.RegistrationDate, ISNULL(r.DischargeDate, GETDATE())) + 1 ELSE 0 END AS LoS>",
                        qr.IsClosed,
                        @"<CAST(cls.ClassSeq AS VARCHAR) AS ClassSeq1>",
                        @"<CAST(cls2.ClassSeq AS VARCHAR) AS ClassSeq2>"
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);
                qr.LeftJoin(infoSum).On(qr.RegistrationNo == infoSum.RegistrationNo & infoSum.NoteCount > 0);
                qr.LeftJoin(bpjsp).On(qr.BpjsPackageID == bpjsp.PackageID);
                qr.LeftJoin(bd).On(bd.BedID == qr.BedID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                qr.InnerJoin(cls).On(cls.ClassID == qr.ChargeClassID);
                qr.InnerJoin(cls2).On(cls2.ClassID == qr.CoverageClassID);

                qr.Where(
                    qr.SRRegistrationType == cboSRRegistrationType.SelectedValue,
                    qr.IsVoid == false,
                    //qr.GuarantorID.In(GuarantorBPJS),
                    qr.IsNonPatient == false
                    );

                var isFilterMax = true;

                if (!txtRegistrationDate1.IsEmpty && !txtRegistrationDate2.IsEmpty)
                    qr.Where(qr.RegistrationDate.Between(txtRegistrationDate1.SelectedDate, txtRegistrationDate2.SelectedDate));

                if (!txtDischargeDate1.IsEmpty && !txtDischargeDate2.IsEmpty)
                    qr.Where(qr.DischargeDate.Between(txtDischargeDate1.SelectedDate, txtDischargeDate2.SelectedDate));

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (cboParamedicID.SelectedValue != string.Empty)
                    qr.Where(qr.ParamedicID == cboParamedicID.SelectedValue);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where(
                    //    qr.Or(
                    //        qr.RegistrationNo == searchReg,
                    //        qp.MedicalNo == searchReg,
                    //        qp.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    qr.Where(qp.FullName.Like(searchPatient));
                    //qr.Where
                    //    (
                    //      string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                    //    );
                }

                if (txtInitialDiagnosis.Text != string.Empty)
                {
                    string searchPatient = "%" + txtInitialDiagnosis.Text + "%";
                    qr.Where
                        (
                          string.Format("<r.InitialDiagnose LIKE '{0}'>", searchPatient)
                        );
                }

                if (!chkIncludeDischargedPatients.Checked)
                    qr.Where(qr.Or(qr.DischargeDate.IsNull(), qr.SRBussinesMethod == string.Empty));

                if (!chkIncludeClosedPatients.Checked)
                    qr.Where(qr.IsClosed == false);

                if (!chkPrescription23Days.Checked)
                    qr.Where(qr.IsFromDispensary == false);
                else
                {
                    var presc = new TransPrescriptionQuery("presc");
                    qr.InnerJoin(presc).On(qr.RegistrationNo == presc.RegistrationNo &&
                        presc.IsApproval == true && presc.IsVoid == false && presc.IsPrescriptionReturn == false && presc.Is23Days == true);
                }

                if (chkPlafondNotSet.Checked)
                    qr.Where(qr.PlavonAmount == 0, qr.ApproximatePlafondAmount == 0);

                if (string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    qr.Where(qr.GuarantorID.In(GuarantorBPJS));
                else
                    qr.Where(qr.GuarantorID == cboGuarantorID.SelectedValue);

                if (AppSession.Parameter.IsSeparatePaymentForOpConsul)
                    qr.Where(qr.IsConsul == false);

                qr.OrderBy(qm.ParamedicName.Ascending, qr.RegistrationNo.Descending);

                if (cboSRRegistrationType.SelectedValue == AppConstant.RegistrationType.InPatient && !chkIncludeClosedPatients.Checked && !chkIncludeDischargedPatients.Checked)
                    isFilterMax = false;

                if (rblPatientClass.SelectedValue != "")
                {
                    if (rblPatientClass.SelectedValue == "-")
                    {
                        qr.Where(cls.ClassSeq > cls2.ClassSeq);
                    }
                    else
                    {
                        qr.Where(cls.ClassSeq < cls2.ClassSeq);
                    }
                }

                if (isFilterMax)
                    qr.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable tbl = qr.LoadDataTable();

                if (txtOverPlafondRange1.Value > 0 || txtOverPlafondRange2.Value > 0)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        string regNo = row["RegistrationNo"].ToString();
                        decimal tpatient = 0;
                        decimal tguarantor = 0;
                        decimal totalPlafond = 0;
                        var usedInPercent = PlafondValueUsedInPercent(regNo, ref tguarantor, ref tpatient, ref totalPlafond);

                        decimal range1 = Convert.ToDecimal(txtOverPlafondRange1.Value);
                        decimal range2 = Convert.ToDecimal(txtOverPlafondRange2.Value);

                        if (range1 > 0 && range2 > 0)
                        {
                            if (usedInPercent <= range1 || usedInPercent > range2)
                                row.Delete();
                        }
                        else if (range1 == 0 && range2 > 0)
                        {
                            if (usedInPercent > range2)
                                row.Delete();
                        }
                        else if (range1 > 0 && range2 == 0)
                        {
                            if (usedInPercent <= range1)
                                row.Delete();
                        }
                    }
                    tbl.AcceptChanges();
                    //return tbl;
                }

                return tbl;
            }
        }

        private string[] GuarantorBPJS
        {
            get
            {
                if (ViewState["bpjs"] != null) return (string[])ViewState["bpjs"];
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                            AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                            AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                if (grr.Query.Load()) ViewState["bpjs"] = grr.Select(g => g.GuarantorID).ToArray();
                else ViewState["bpjs"] = new string[] { string.Empty };

                return (string[])ViewState["bpjs"];
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdRegistrationList.DataSource = Registrations;
            grdRegistrationList.DataBind();
        }

        protected void grdRegistrationList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem["ChargeClassID"].Text != dataItem["CoverageClassID"].Text)
                {
                    // Beri warna merah jika CoverageClassID berbeda dg ChargeClassID Up, 
                    // Beri warna biru jika CoverageClassID berbeda dg ChargeClassID Down, 
                    var classSeq1 = dataItem["ClassSeq1"].Text.ToInt();
                    var classSeq2 = dataItem["ClassSeq2"].Text.ToInt();

                    dataItem.ForeColor = classSeq1 < classSeq2 ? Color.Red : Color.Blue;
                    dataItem.Font.Bold = true;
                    tooltip = "Charge class is different from coverage class.";
                }
                if (dataItem["ChargeClassID"].Text != dataItem["DefaultClassID"].Text)
                {
                    dataItem.Font.Bold = true;
                    dataItem.Font.Italic = true;
                    tooltip = tooltip == string.Empty ? "Charge class is different from bed class." : "Charge class is different from coverage and bed class.";
                }
                dataItem.ToolTip = tooltip;               
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdRegistrationList.Rebind();
        }

        protected string PlafondProgress(string regNo)
        {
            decimal tpatient = 0;
            decimal tguarantor = 0;
            decimal totalPlafond = 0;

            var usedInPercent = PlafondValueUsedInPercent(regNo, ref tguarantor, ref tpatient, ref totalPlafond);
            if (usedInPercent == 0) return string.Empty;
            return string.Format(@"<div title='G: [{3:N2}] P: [{4:N2}] V: [{5:N2}]' style='background:white;width: 100%; padding: 1px'>
                            <div style='background:{0};color:Black;width: {1}%'>{2:n2}%</div>
                        </div>", usedInPercent > 100 ? "red" : usedInPercent > 75 ? "yellow" : "green", usedInPercent > 100 ? 100 : usedInPercent, usedInPercent, tguarantor, tpatient, totalPlafond);

        }

        protected string TransctionProgress(string regNo)
        {
            decimal tpatient = 0;
            decimal tguarantor = 0;
            decimal totalPlafond = 0;

            var total = TotalTransaction(regNo, ref tguarantor, ref tpatient, ref totalPlafond);
            return string.Format("{0:n2}", total);
        }

        protected string Plafond(string regNo)
        {
            var reg = Registration(regNo);
            decimal cobPlafond = AdditionalPlafond(regNo);
            decimal totalPlafond = TotalPlafond(reg, cobPlafond);

            return string.Format("{0:n2}", totalPlafond);
        }

        private decimal PlafondValueUsedInPercent(string regno, ref decimal tguarantor, ref decimal tpatient, ref decimal totalPlafond)
        {
            var reg = Registration(regno);
            decimal cobPlafond = AdditionalPlafond(regno);
            totalPlafond = TotalPlafond(reg, cobPlafond);
            if (totalPlafond == 0) 
                totalPlafond = 1;

            var regnos = Helper.MergeBilling.GetMergeRegistration(regno);

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);

            //Helper.CostCalculation.GetBillingTotal2(regnos, reg.SRBussinesMethod, (reg.PlavonAmount ?? 0) + cobPlafond, out tpatient, out tguarantor,
            //                                       guarantor, reg.IsGlobalPlafond ?? false);

            Helper.CostCalculation.GetBillingTotal2(regnos, reg.SRBussinesMethod, 0, out tpatient, out tguarantor,
                                                   guarantor, reg.IsGlobalPlafond ?? false);

            var totalRemain = tguarantor + tpatient;

            var plafonUsedPercent = (totalRemain / totalPlafond) * (decimal)100;
            return plafonUsedPercent;
        }

        private decimal TotalTransaction(string regno, ref decimal tguarantor, ref decimal tpatient, ref decimal totalPlafond)
        {
            var reg = Registration(regno);
            decimal cobPlafond = AdditionalPlafond(regno);
            totalPlafond = TotalPlafond(reg, cobPlafond);

            var regnos = Helper.MergeBilling.GetMergeRegistration(regno);

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);

            //Helper.CostCalculation.GetBillingTotal2(regnos, reg.SRBussinesMethod, (reg.PlavonAmount ?? 0) + cobPlafond, out tpatient, out tguarantor,
            //                                       guarantor, reg.IsGlobalPlafond ?? false);

            Helper.CostCalculation.GetBillingTotal2(regnos, reg.SRBussinesMethod, 0, out tpatient, out tguarantor,
                                                   guarantor, reg.IsGlobalPlafond ?? false);

            var totalRemain = tguarantor + tpatient;

            return totalRemain;
        }

        private Registration Registration(string regNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(regNo);
            return reg;
        }

        private decimal AdditionalPlafond(string regno)
        {
            decimal cobPlafond = 0;
            var cob = new RegistrationGuarantorCollection();
            cob.Query.Where(cob.Query.RegistrationNo == regno);
            cob.LoadAll();
            cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
            return cobPlafond;
        }

        private decimal TotalPlafond(Registration reg, decimal additionalPlafond)
        {
            decimal plafondAmt = reg.PlavonAmount ?? 0;
            //if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID) && plafondAmt == 0)
            if (GuarantorBPJS.Contains(reg.GuarantorID) && plafondAmt == 0)
                plafondAmt = (decimal)(reg.ApproximatePlafondAmount == null ? 0 : reg.ApproximatePlafondAmount);

            return plafondAmt + additionalPlafond;
        }

        protected void cboSRRegistrationType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.SelectedValue = string.Empty;
            cboServiceUnitID.Text = string.Empty;
        }

        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            query.Where(
                query.Or(query.ServiceUnitID == e.Text,
                query.ServiceUnitName.Like(searchTextContain)),
                query.SRRegistrationType == cboSRRegistrationType.SelectedValue,
                query.IsActive == true
                );
            query.Select(query.ServiceUnitID, query.ServiceUnitName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboServiceUnitID.DataSource = dtb;
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }

        protected void cboGuarantorID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery("a");
            query.Where(
                query.Or(query.GuarantorID == e.Text,
                query.GuarantorName.Like(searchTextContain)),
                query.IsActive == true
                );
            query.Select(query.GuarantorID, query.GuarantorName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboGuarantorID.DataSource = dtb;
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.GuarantorItemDataBound(e);
        }

        private StringBuilder StrbResponseScripts { get; } = new StringBuilder();
        protected string UpdateStatScript(string statType, object regNo, object fregNo, object regType, object patId, object dob, object parId)
        {
            if (statType == "ews" && !regType.Equals(AppConstant.RegistrationType.InPatient))
                return string.Empty;

            if (statType == "plafond" && !AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsEmrListShowPlafondProgress))
                return String.Empty;


            var script = string.Format("UpdateState(\"{6}\",\"{6}{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{7}\");", regNo.ToString().Replace("/", "_"),
                regNo, fregNo, regType, patId, dob, statType, parId);

            // Tampung script untuk diregistrasi pada AjaxManager.ResponseScripts karena AJAX defaultnya tidak akan menjalankan script yg ada di page
            StrbResponseScripts.AppendLine(script);
            return script;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (IsPostBack)
                AjaxManager.ResponseScripts.Add(StrbResponseScripts.ToString());
        }
    }
}