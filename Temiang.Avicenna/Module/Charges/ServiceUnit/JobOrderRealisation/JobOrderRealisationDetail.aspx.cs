using DevExpress.XtraRichEdit.Layout.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Diagram;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class JobOrderRealisationDetail : BasePageDialog
    {
        private string _paramedicIdDokterLuar, _serviceUnitRadiologyID, _serviceUnitRadiologyID2, _serviceUnitLaboratoryID;
        private string _defaultTariffType, _defaultTariffClass, _guarantorRuleTypeDiscount, _guarantorRuleTypeMargin;
        private AppAutoNumberLast _amplopFilmAutoNumber, _filmNo;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["disch"] == "0" ? AppConstant.Program.JobOrderRealisation : AppConstant.Program.JobOrderRealisationVerification;

            _paramedicIdDokterLuar = AppSession.Parameter.ParamedicIdDokterLuar;
            _serviceUnitRadiologyID = AppSession.Parameter.ServiceUnitRadiologyID;
            _serviceUnitRadiologyID2 = AppSession.Parameter.ServiceUnitRadiologyID2;
            _defaultTariffType = AppSession.Parameter.DefaultTariffType;
            _defaultTariffClass = AppSession.Parameter.DefaultTariffClass;
            _guarantorRuleTypeDiscount = AppSession.Parameter.GuarantorRuleTypeDiscount;
            _guarantorRuleTypeMargin = AppSession.Parameter.GuarantorRuleTypeMargin;
            _serviceUnitLaboratoryID = AppSession.Parameter.ServiceUnitLaboratoryID;

            if (!IsPostBack)
            {
                hdnPageId.Value = PageID;

                LoadTransactionHeader();
                TransChargesItems = null;
                Session["collTransChargesItemComp" + Request.UserHostName] = null;
                LoadTransChargesItemComp();

                TransChargesItemConsumptions = null;
                var cons = TransChargesItemConsumptions;

                var tc = new TransCharges();
                tc.LoadByPrimaryKey(Request.QueryString["joNo"]);
                if (tc.ToServiceUnitID == _serviceUnitLaboratoryID && AppSession.Parameter.IsUsingHisInterop && AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME) pnlLinkLis.Visible = true;
                if (pnlLinkLis.Visible)
                {
                    var pb = new ParamedicBridgingQuery("a");
                    var p = new ParamedicQuery("b");

                    pb.Select(pb.BridgingID, pb.BridgingName, p.SRParamedicType);
                    pb.InnerJoin(p).On(pb.ParamedicID == p.ParamedicID &&
                        p.SRParamedicType.In(AppEnum.ParamedicType.ClinicalPathologist.ToString(), AppEnum.ParamedicType.LaboratoryAnalyst.ToString()) &&
                        p.IsActive == true);
                    pb.Where(pb.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                    var dtb = pb.LoadDataTable();

                    cboPhysicianIDPathology.Items.Clear();
                    cboPhysicianIDPathology.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (DataRow item in dtb.AsEnumerable().Where(d => d.Field<string>("SRParamedicType") == AppEnum.ParamedicType.ClinicalPathologist.ToString()))
                    {
                        cboPhysicianIDPathology.Items.Add(new RadComboBoxItem(item["BridgingName"].ToString(), item["BridgingID"].ToString()));
                    }

                    cboAnalystID.Items.Clear();
                    cboAnalystID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (DataRow item in dtb.AsEnumerable().Where(d => d.Field<string>("SRParamedicType") == AppEnum.ParamedicType.LaboratoryAnalyst.ToString()))
                    {
                        cboAnalystID.Items.Add(new RadComboBoxItem(item["BridgingName"].ToString(), item["BridgingID"].ToString()));
                    }
                }
                trClinicalDiagnosis.Visible = AppSession.Parameter.IsVisibleClinicalDiagnosisOnJobOrderRealization;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (trRadiologyNo.Visible)
                RadAjaxManagerProxy1.AjaxSettings.AddAjaxSetting(grdTransChargesItem, txtRadiologyNo);

        }

        private void LoadTransactionHeader()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
            txtRegistrationNo.Text = reg.RegistrationNo;
            txtAgeInYear.Text = reg.AgeInYear.ToString();
            txtAgeInMonth.Text = reg.AgeInMonth.ToString();
            txtAgeInDay.Text = reg.AgeInDay.ToString();
            txtBedID.Text = reg.BedID;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            txtMedicalNo.Text = pat.MedicalNo;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = pat.PatientName;
            txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
            //optSexFemale.Enabled = (pat.Sex == "F");
            //optSexFemale.Checked = (pat.Sex == "F");
            //optSexMale.Enabled = (pat.Sex == "M");
            //optSexMale.Checked = (pat.Sex == "M");
            txtRadiologyNo.Text = pat.DiagnosticNo;
            PopulatePatientImage(pat.PatientID, pat.Sex);

            txtJobOrderNo.Text = Request.QueryString["joNo"];

            var sex = new AppStandardReferenceItem();
            sex.LoadByPrimaryKey("GenderType", pat.Sex);
            txtSex.Text = sex.ItemName;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);
            txtServiceUnitID.Text = reg.ServiceUnitID;
            lblServiceUnitName.Text = unit.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.str.RoomID);
            txtRoomID.Text = reg.str.RoomID;
            lblRoomName.Text = room.str.RoomName;

            var c = new Class();
            c.LoadByPrimaryKey(reg.ChargeClassID);
            txtClassID.Text = c.ClassID;
            lblClassName.Text = c.ClassName;

            var med = new Paramedic();
            med.LoadByPrimaryKey(reg.str.ParamedicID);
            txtParamedicID.Text = reg.str.ParamedicID;
            lblParamedicName.Text = med.str.ParamedicName;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            txtGuarantorID.Text = reg.GuarantorID;
            lblGuarantorName.Text = grr.GuarantorName;
            trBpjsSepNo.Visible = grr.SRGuarantorType == AppSession.Parameter.GuarantorTypeBPJS;
            txtBpjsSepNo.Text = reg.BpjsSepNo;

            //// Remark by Handono 231104
            ///  Change to just display SOAP history current Patient Episode order by DateTimeInfo.Descending (Handono 231104)

            //// From table EpisodeSOAPE
            //var soapColl = new EpisodeSOAPECollection();
            //soapColl.Query.Where(
            //    soapColl.Query.RegistrationNo == reg.RegistrationNo &&
            //    soapColl.Query.IsVoid == false,
            //    soapColl.Query.Or(soapColl.Query.Imported.IsNull(), soapColl.Query.Imported == false)
            //    );
            //soapColl.LoadAll();

            //foreach (var soap in soapColl)
            //{
            //    litSoapS.Text = string.IsNullOrEmpty(soap.Subjective.Trim()) ? litSoapS.Text : litSoapS.Text + soap.Subjective + "<br />";
            //    litSoapO.Text = string.IsNullOrEmpty(soap.Objective.Trim()) ? litSoapO.Text : litSoapO.Text + soap.Objective + "<br />";
            //    litSoapA.Text = string.IsNullOrEmpty(soap.Assesment.Trim()) ? litSoapA.Text : litSoapA.Text + soap.Assesment + "<br />";
            //    litSoapP.Text = string.IsNullOrEmpty(soap.Planning.Trim()) ? litSoapP.Text : litSoapP.Text + soap.Planning + "<br />";
            //}

            //if (string.IsNullOrEmpty(litSoapS.Text) && string.IsNullOrEmpty(litSoapO.Text) && string.IsNullOrEmpty(litSoapA.Text) && string.IsNullOrEmpty(litSoapP.Text))
            //{
            //    //From Table RegistrationInfoMedic
            //    var rimColl = new RegistrationInfoMedicCollection();
            //    rimColl.Query.Where(
            //        rimColl.Query.RegistrationNo == reg.RegistrationNo,
            //        rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
            //        );
            //    rimColl.LoadAll();

            //    foreach (var rim in rimColl)
            //    {
            //        litSoapS.Text = string.IsNullOrEmpty(rim.Info1.Trim()) ? litSoapS.Text : litSoapS.Text + rim.Info1 + "<br />";
            //        litSoapO.Text = string.IsNullOrEmpty(rim.Info2.Trim()) ? litSoapO.Text : litSoapO.Text + rim.Info2 + "<br />";
            //        litSoapA.Text = string.IsNullOrEmpty(rim.Info3.Trim()) ? litSoapA.Text : litSoapA.Text + rim.Info3 + "<br />";
            //        litSoapP.Text = string.IsNullOrEmpty(rim.Info4.Trim()) ? litSoapP.Text : litSoapP.Text + rim.Info4 + "<br />";
            //    }
            //}

            //litSoapS.Text = litSoapS.Text.Replace(System.Environment.NewLine, "<br />");
            //litSoapO.Text = litSoapO.Text.Replace(System.Environment.NewLine, "<br />");
            //litSoapA.Text = litSoapA.Text.Replace(System.Environment.NewLine, "<br />");
            //litSoapP.Text = litSoapP.Text.Replace(System.Environment.NewLine, "<br />");

            ///  Display SOAP history current Patient Episode order by DateTimeInfo.Descending (Handono 231104)
            PopulateSoap();

            var tx = new TransCharges();
            if (tx.LoadByPrimaryKey(Request.QueryString["joNo"]))
            {
                txtNotes.Text = tx.Notes;
                txtClinicalDiagnosis.Text = tx.ClinicalDiagnosis;

                if (!string.IsNullOrEmpty(tx.PhysicianSenders))
                    txtPhysicianSenders.Text = tx.PhysicianSenders;
                else
                {
                    var parId = reg.ParamedicID;
                    if (parId == _paramedicIdDokterLuar)
                        txtPhysicianSenders.Text = reg.PhysicianSenders;
                    else
                    {
                        var par = new Paramedic();
                        par.LoadByPrimaryKey(parId);
                        txtPhysicianSenders.Text = par.ParamedicName;
                    }
                }

                trRadiologyNo.Visible = (tx.ToServiceUnitID == _serviceUnitRadiologyID ||
                                         tx.ToServiceUnitID == _serviceUnitRadiologyID2 ||
                                         AppSession.Parameter.ServiceUnitRadiologyIDs.Contains(tx.ToServiceUnitID) ||
                                         AppSession.Parameter.ServiceUnitRadiologyIdArray.Contains(tx.ToServiceUnitID));

                grdTransChargesItem.Columns.FindByUniqueName("ExposureFactor").Visible = (tx.ToServiceUnitID == _serviceUnitRadiologyID || tx.ToServiceUnitID == _serviceUnitRadiologyID2 || AppSession.Parameter.ServiceUnitRadiologyIDs.Contains(tx.ToServiceUnitID) || AppSession.Parameter.ServiceUnitRadiologyIdArray.Contains(tx.ToServiceUnitID)); //ExposureFactor

                btnUpdateRadNo.Visible = !AppSession.Parameter.IsRadiologyNoAutoCreate;

                if (tx.ToServiceUnitID == _serviceUnitLaboratoryID &&
                    AppSession.Parameter.IsUsingHisInterop &&
                    AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.PRODIA_LIS_INTEROP_CONNECTION_NAME)
                {
                    pnlProdia.Visible = true;
                }
                else
                {
                    pnlProdia.Visible = false;
                }

                StandardReference.InitializeIncludeSpace(cboSRProdiaContractID, AppEnum.StandardReference.ProdiaContractID);
                cboSRProdiaContractID.SelectedValue = tx.SRProdiaContractID;

                var param = new ParamedicQuery("a");
                var paramunit = new ServiceUnitParamedicQuery("b");

                param.Select
                    (
                        param.ParamedicID,
                        param.ParamedicName
                    );
                param.InnerJoin(paramunit).On(param.ParamedicID == paramunit.ParamedicID);
                param.Where
                    (
                        paramunit.ServiceUnitID == tx.ToServiceUnitID,
                        param.IsActive == true
                    );

                var tbl = param.LoadDataTable();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow row in tbl.Rows)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem((string)row["ParamedicName"], (string)row["ParamedicID"]));
                }

                //if (tx.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                //{
                //    var psdColl = new ParamedicScheduleDateCollection();
                //    DataTable dtb = psdColl.GetParamedicID(AppSession.Parameter.ServiceUnitLaboratoryID, reg.SRRegistrationType);

                //    if (dtb.Rows.Count == 1)
                //    {
                //        cboParamedicID.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                //        cboParamedicID.Text = dtb.Rows[0]["ParamedicName"].ToString();
                //    }
                //}

                var psdColl = new ParamedicScheduleDateCollection();
                DataTable dtb = psdColl.GetParamedicID(tx.ToServiceUnitID, reg.SRRegistrationType);

                if (dtb.Rows.Count == 1)
                {
                    cboParamedicID.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
                    cboParamedicID.Text = dtb.Rows[0]["ParamedicName"].ToString();
                }

            }
            else
            {
                trRadiologyNo.Visible = false;
                btnUpdateRadNo.Visible = false;
                grdTransChargesItem.Columns.FindByUniqueName("ExposureFactor").Visible = false;
                pnlProdia.Visible = false;
            }
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
        }

        public override bool OnButtonOkClicked()
        {
            var collComp = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName];

            var charges = new TransCharges();
            charges.LoadByPrimaryKey(Request.QueryString["joNo"]);

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(charges.ToServiceUnitID);
            if (string.IsNullOrEmpty(charges.LocationID)) charges.LocationID = unit.GetMainLocationId(charges.ToServiceUnitID);

            var reg = new Registration();
            reg.LoadByPrimaryKey(charges.RegistrationNo);

            if (reg.IsHoldTransactionEntry ?? false)
            {
                ShowInformationHeader("Registration locked.");
                return false;
            }

            if (reg.IsClosed ?? false)
            {
                ShowInformationHeader("Registration closed.");
                return false;
            }

            if ((reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient || reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) &&
                charges.FromServiceUnitID != charges.ToServiceUnitID &&
                AppSession.Parameter.IsValidateDiagnosisOnRealizationOrderOp &&
                string.IsNullOrEmpty(litSoapA.Text))
            {
                ShowInformationHeader("Diagnosis required.");
                return false;
            }

            if (pnlProdia.Visible && string.IsNullOrEmpty(cboSRProdiaContractID.SelectedValue))
            {
                ShowInformationHeader("Prodia Contract ID required.");
                return false;
            }

            if (AppSession.Parameter.IsUsingHisInterop && AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME)
            {
                if (charges.ToServiceUnitID == _serviceUnitLaboratoryID)
                {
                    if (string.IsNullOrEmpty(cboPhysicianIDPathology.SelectedValue))
                    {
                        ShowInformationHeader(string.Format("Clinical Pathologist is required."));
                        return false;
                    }

                    if (string.IsNullOrEmpty(cboAnalystID.SelectedValue))
                    {
                        ShowInformationHeader(string.Format("Laboratory Analyst is required."));
                        return false;
                    }
                }
            }

            var grrId = reg.GuarantorID;
            if (grrId == AppSession.Parameter.SelfGuarantor)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                if (!string.IsNullOrEmpty(pat.MemberID)) grrId = pat.MemberID;
            }

            var guar = new Guarantor();
            guar.LoadByPrimaryKey(grrId);

            var tariffDate = guar.TariffCalculationMethod == 1 ? reg.RegistrationDate.Value.Date : charges.ExecutionDate.Value.Date;

            var isProceed = false;
            foreach (var a in TransChargesItems)
            {
                var py = new TransPaymentItemOrderQuery();
                py.Where(py.TransactionNo == a.TransactionNo, py.SequenceNo == a.SequenceNo, py.IsPaymentProceed == true, py.IsPaymentReturned == false);
                bool isPaid = py.LoadDataTable().Rows.Count > 0;

                if (isPaid && (a.IsVoid ?? false))
                {
                    var i = new Item();
                    i.LoadByPrimaryKey(a.ItemID);

                    ShowInformationHeader("Existing payment for : " + i.ItemName + ". This transaction can not be canceled.");
                    return false;
                }

                if (a.IsOrderRealization ?? false)
                {
                    var collt = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, guar.SRTariffType, reg.ChargeClassID, a.ItemID);
                    if (!collt.Any()) collt = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, guar.SRTariffType, _defaultTariffClass, a.ItemID);
                    if (!collt.Any()) collt = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, reg.ChargeClassID, a.ItemID);
                    if (!collt.Any()) collt = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, _defaultTariffClass, a.ItemID);
                    var colltx = collt.Where(c => TariffParamedic().Contains(c.TariffComponentID));

                    if (colltx.Any())
                    {
                        //var collCompx =
                        //    collComp.Where(c => !string.IsNullOrEmpty(c.ParamedicID) && TariffParamedic().Contains(c.TariffComponentID) &&
                        //        c.TransactionNo == a.TransactionNo && c.SequenceNo == a.SequenceNo);
                        //if (!collCompx.Any())
                        //{
                        //    ShowInformationHeader("Physician for : " + a.ItemName + " have not been filled. Please correct the data.");
                        //    return false;
                        //}

                        var collCompx = collComp.Where(c => string.IsNullOrEmpty(c.ParamedicID) && TariffParamedic().Contains(c.TariffComponentID) && c.TransactionNo == a.TransactionNo && c.SequenceNo == a.SequenceNo);
                        if (collCompx.Any())
                        {
                            ShowInformationHeader("Physician for : " + a.ItemName + " have not been filled. Please correct the data.");
                            return false;
                        }
                    }
                    isProceed = true;
                }
            }

            #region TransCharges
            charges.PhysicianSenders = txtPhysicianSenders.Text;
            charges.SRProdiaContractID = cboSRProdiaContractID.SelectedValue;
            charges.ClinicalDiagnosis = txtClinicalDiagnosis.Text;
            //if (isProceed)
            //{
            if (charges.IsBillProceed == false) charges.ExecutionDate = (new DateTime()).NowAtSqlServer();
            charges.IsBillProceed = true;
            //}
            if (pnlLinkLis.Visible)
            {
                charges.LaboratoryParamedicID = cboPhysicianIDPathology.SelectedValue;
                charges.AnalystID = cboAnalystID.SelectedValue;
            }
            if (isProceed) charges.SROrderStatus = "2";
            #endregion

            var collCost = new CostCalculationCollection();

            var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrId, charges.ClassID, reg.CoverageClassID, (TransChargesItems.Select(t => t.ItemID)).ToArray(), tariffDate, false);

            var seqs = TransChargesItems.Where(item => !IsOrderRealization(item.TransactionNo, item.SequenceNo) && (item.IsBillProceed ?? false) && item.IsVoid == false);

            foreach (var detail in seqs)
            {
                //--- untuk item detail paket, covered diambil dari header item paket
                string itemId = detail.ItemID;
                if (!string.IsNullOrEmpty(charges.PackageReferenceNo))
                {
                    var tciPackageRef = new TransChargesItem();
                    if (tciPackageRef.LoadByPrimaryKey(charges.PackageReferenceNo, detail.SequenceNo.Substring(0, 3))) itemId = tciPackageRef.ItemID;

                    tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrId, charges.ClassID, itemId, tariffDate, false);
                }

                //var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == detail.ItemID &&
                //  

                var rowCovered = tblCovered.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == itemId && t.Field<bool>("IsInclude"));

                #region TransChargesItemComps
                if (rowCovered != null)
                {
                    decimal? discount = 0;
                    bool isDiscount = false, isMargin = false;
                    foreach (var comp in collComp.Where(t => t.TransactionNo == detail.TransactionNo && t.SequenceNo == detail.SequenceNo)
                                                 .OrderBy(t => t.TariffComponentID))
                    {
                        decimal? amountValue = 0;
                        decimal? basicPrice = 0;
                        decimal? coveragePrice = 0;

                        if (Convert.ToBoolean(rowCovered["IsByTariffComponent"]))
                        {
                            var array = rowCovered["TariffComponentValue"].ToString().Split(';').Where(l => l.Split('/')[2] == comp.TariffComponentID).SingleOrDefault();
                            if (array == null)
                            {
                                amountValue = (decimal?)rowCovered["AmountValue"];
                                basicPrice = (decimal?)rowCovered["BasicPrice"];
                                coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                            }
                            else
                            {
                                var list = array.Split('/');
                                if (list == null || list.Count() == 0)
                                {
                                    amountValue = (decimal?)rowCovered["AmountValue"];
                                    basicPrice = (decimal?)rowCovered["BasicPrice"];
                                    coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                                }
                                else
                                {
                                    amountValue = Convert.ToDecimal(list[3]);
                                    basicPrice = Convert.ToDecimal(list[0]);
                                    coveragePrice = Convert.ToDecimal(list[1]);
                                }
                            }
                        }
                        else
                        {
                            amountValue = (decimal?)rowCovered["AmountValue"];
                            basicPrice = (decimal?)rowCovered["BasicPrice"];
                            coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                        }

                        basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, tariffDate);
                        coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, tariffDate);

                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeDiscount))
                        {
                            if ((comp.Price - comp.DiscountAmount) <= 0) continue;

                            var compPrice = comp.Price ?? 0;
                            if (basicPrice > coveragePrice)
                            {
                                var tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, guar.SRTariffType, reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                if (!tcomp.AsEnumerable().Any()) tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, guar.SRTariffType, _defaultTariffClass, comp.TariffComponentID, detail.ItemID);
                                if (!tcomp.AsEnumerable().Any()) tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, _defaultTariffType, reg.CoverageClassID, comp.TariffComponentID, detail.ItemID);
                                if (!tcomp.AsEnumerable().Any()) tcomp = Helper.Tariff.GetItemTariffComponent(tariffDate, _defaultTariffType, _defaultTariffClass, comp.TariffComponentID, detail.ItemID);

                                if (!tcomp.AsEnumerable().Any()) continue;

                                compPrice = tcomp.AsEnumerable().Select(c => c.Field<decimal>("Price")).Single();
                                compPrice = Helper.Tariff.GetItemConditionRuleTariff(compPrice, detail.ItemConditionRuleID, charges.TransactionDate.Value);
                            }

                            decimal basicCitoAmount = detail.BasicCitoAmount ?? 0;
                            decimal compCitoAmt = compPrice * basicCitoAmount / 100;

                            if ((bool)rowCovered["IsValueInPercent"])
                            {
                                var discountRule = (amountValue / 100) * (compPrice + compCitoAmt);
                                var fee = comp.CalculateParamedicPercentDiscount(
                                    AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                    reg.RegistrationNo, detail.ItemID, discountRule,
                                    AppSession.UserLogin.UserID, charges.ClassID, charges.ToServiceUnitID);
                                comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                //comp.DiscountAmount = (amountValue / 100) * compPrice;
                                //comp.AutoProcessCalculation = 0 - (amountValue / 100) * compPrice;
                            }
                            else
                            {
                                //if (!isDiscount)
                                //{
                                //    if (discount == 0)
                                //    {
                                if (detail.Price > compPrice)
                                    amountValue = ((compPrice + compCitoAmt) / (detail.Price + (detail.CitoAmount / Math.Abs(detail.ChargeQuantity ?? 0)))) * amountValue;

                                if (compPrice + compCitoAmt >= amountValue)
                                {
                                    var discountRule = amountValue;
                                    var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                        reg.RegistrationNo, detail.ItemID, discountRule, AppSession.UserLogin.UserID,
                                        charges.ClassID, charges.ToServiceUnitID);
                                    comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                    //comp.DiscountAmount = amountValue;
                                    //comp.AutoProcessCalculation = 0 - amountValue;
                                    //isDiscount = true;
                                }
                                else
                                {
                                    var discountRule = compPrice + compCitoAmt;
                                    var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                        reg.RegistrationNo, detail.ItemID, discountRule, AppSession.UserLogin.UserID,
                                        charges.ClassID, charges.ToServiceUnitID);
                                    comp.AutoProcessCalculation = 0 - comp.DiscountAmount;

                                    //comp.DiscountAmount = compPrice;
                                    //comp.AutoProcessCalculation = 0 - compPrice;
                                    //discount = amountValue - compPrice;
                                }
                                //    }
                                //    else
                                //    {
                                //        if (compPrice >= discount)
                                //        {
                                //            comp.DiscountAmount = discount;
                                //            comp.AutoProcessCalculation = 0 - discount;
                                //            isDiscount = true;
                                //        }
                                //        else
                                //        {
                                //            comp.DiscountAmount = compPrice;
                                //            comp.AutoProcessCalculation = 0 - compPrice;
                                //            discount -= compPrice;
                                //        }
                                //    }
                                //}
                            }
                        }
                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeMargin))
                        {
                            decimal basicCitoAmount = detail.BasicCitoAmount ?? 0;

                            if ((bool)rowCovered["IsValueInPercent"])
                            {
                                comp.AutoProcessCalculation = (amountValue / 100) * (comp.Price + comp.CitoAmount);
                                comp.Price += (amountValue / 100) * comp.Price;
                                comp.CitoAmount += (amountValue / 100) * comp.CitoAmount;

                                var discountRule = 0;
                                var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                    reg.RegistrationNo, detail.ItemID, discountRule, AppSession.UserLogin.UserID,
                                    charges.ClassID, charges.ToServiceUnitID);
                                comp.AutoProcessCalculation = comp.AutoProcessCalculation - comp.DiscountAmount;
                            }
                            else
                            {
                                if (!isMargin)
                                {
                                    comp.Price += amountValue;
                                    comp.CitoAmount = comp.Price * basicCitoAmount / 100;
                                    comp.AutoProcessCalculation = amountValue + comp.CitoAmount;
                                    isMargin = true;

                                    var discountRule = 0;
                                    var fee = comp.CalculateParamedicPercentDiscount(AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                        reg.RegistrationNo, detail.ItemID, discountRule, AppSession.UserLogin.UserID,
                                        charges.ClassID, charges.ToServiceUnitID);
                                    comp.AutoProcessCalculation = amountValue - comp.DiscountAmount;
                                }
                            }
                        }
                        //comp.CitoAmount = 0;
                        comp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        comp.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    }
                }
                #endregion

                #region TransChargesItems

                if (collComp.Count > 0)
                {
                    detail.AutoProcessCalculation = collComp.Where(t => t.TransactionNo == detail.TransactionNo &&
                                                                        t.SequenceNo == detail.SequenceNo)
                                                            .Sum(t => t.AutoProcessCalculation);
                    if (detail.AutoProcessCalculation < 0)
                    {
                        detail.DiscountAmount += detail.ChargeQuantity * Math.Abs(detail.AutoProcessCalculation ?? 0);

                        if (detail.DiscountAmount > (detail.Price * detail.ChargeQuantity) + detail.CitoAmount)
                        {
                            detail.DiscountAmount = (detail.Price * detail.ChargeQuantity) + detail.CitoAmount;
                            detail.AutoProcessCalculation = 0 - (detail.Price + (detail.CitoAmount / detail.ChargeQuantity ?? 0));
                        }
                    }
                    else if (detail.AutoProcessCalculation > 0) detail.Price += detail.AutoProcessCalculation;
                }
                else
                {
                    if (rowCovered != null)
                    {
                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeDiscount))
                        {
                            var basicPrice = (decimal?)rowCovered["BasicPrice"];
                            var coveragePrice = (decimal?)rowCovered["CoveragePrice"];
                            basicPrice = Helper.Tariff.GetItemConditionRuleTariff(basicPrice ?? 0, detail.ItemConditionRuleID, tariffDate);
                            coveragePrice = Helper.Tariff.GetItemConditionRuleTariff(coveragePrice ?? 0, detail.ItemConditionRuleID, tariffDate);

                            var detailPrice = detail.Price ?? 0;
                            if (basicPrice > coveragePrice)
                            {
                                ItemTariff tariff = (Helper.Tariff.GetItemTariff(tariffDate, guar.SRTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                     Helper.Tariff.GetItemTariff(tariffDate, guar.SRTariffType, _defaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                                    (Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, reg.CoverageClassID, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                                     Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, _defaultTariffClass, reg.CoverageClassID, detail.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));
                                if (tariff != null)
                                {
                                    //detailPrice = tariff.Price ?? 0;
                                    detailPrice = Helper.Tariff.GetItemConditionRuleTariff(tariff.Price ?? 0, detail.ItemConditionRuleID, tariffDate);
                                }
                            }

                            if ((bool)rowCovered["IsValueInPercent"])
                            {
                                detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                                detail.AutoProcessCalculation = 0 - (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                            }
                            else
                            {
                                detail.DiscountAmount = (detail.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];
                                detail.AutoProcessCalculation = 0 - (((decimal)rowCovered["AmountValue"] / 100) * detailPrice);
                            }

                            if (detail.DiscountAmount > detailPrice * detail.ChargeQuantity)
                                detail.DiscountAmount = detailPrice * detail.ChargeQuantity;
                        }
                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(_guarantorRuleTypeMargin))
                        {
                            if ((bool)rowCovered["IsValueInPercent"])
                            {
                                detail.Price += ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                                detail.AutoProcessCalculation = ((decimal)rowCovered["AmountValue"] / 100) * detail.Price;
                            }
                            else
                            {
                                detail.Price += (decimal)rowCovered["AmountValue"];
                                detail.AutoProcessCalculation = (decimal)rowCovered["AmountValue"];
                            }
                        }
                    }
                }
                #endregion

                detail.TariffDate = tariffDate;
                detail.IsBillProceed = true;
                detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                detail.RealizationDateTime = (new DateTime()).NowAtSqlServer();
                detail.RealizationUserID = AppSession.UserLogin.UserID;
                detail.IsSendToLIS = charges.ToServiceUnitID == _serviceUnitLaboratoryID;

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH" && string.IsNullOrEmpty(detail.FilmNo))
                {
                    var item = new Item();
                    item.LoadByPrimaryKey(detail.ItemID);
                    if (item.Notes.Length > 0 && item.SRItemType != ItemType.Medical && item.SRItemType != ItemType.NonMedical && item.SRItemType != ItemType.Kitchen)
                    {
                        _amplopFilmAutoNumber = Helper.GetNewAutoNumber(charges.TransactionDate.Value.Date, AppEnum.AutoNumber.AmplopFilmNo,
                            item.Notes.Length >= 3 ? item.Notes.Substring(0, 3).ToUpper() : item.Notes.ToUpper(), AppSession.UserLogin.UserID);

                        var filmNo = _amplopFilmAutoNumber.LastCompleteNumber;
                        _amplopFilmAutoNumber.Save();

                        detail.FilmNo = filmNo;
                    }
                }
                else if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP" && AppSession.Parameter.HealthcareInitial == "RSI")
                {
                    //if (string.IsNullOrEmpty(detail.FilmNo))
                    {
                        var i = new Item();
                        i.LoadByPrimaryKey(detail.ItemID);
                        if (i.SRItemType == ItemType.Radiology)
                        {
                            if (i.ItemIDExternal == "CR")
                            {
                                _amplopFilmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CRRadiologyFilmNo);
                                detail.FilmNo = _amplopFilmAutoNumber.LastCompleteNumber;
                                _amplopFilmAutoNumber.Save();
                            }
                            else if (i.ItemIDExternal == "CT")
                            {
                                _amplopFilmAutoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.CTRadiologyFilmNo);
                                detail.FilmNo = _amplopFilmAutoNumber.LastCompleteNumber;
                                _amplopFilmAutoNumber.Save();
                            }
                        }
                    }
                }

                if (rowCovered != null)
                {
                    var total = ((detail.ChargeQuantity * detail.Price) - detail.DiscountAmount) + detail.CitoAmount;
                    //var calc = new Helper.CostCalculation(grrId, detail.ItemID, total ?? 0, tblCovered, detail.ChargeQuantity ?? 0,
                    //                                                  detail.IsCito ?? false,
                    //                                                  detail.IsCitoInPercent ?? false,
                    //                                                  detail.BasicCitoAmount ?? 0, detail.Price ?? 0,
                    //                                                  charges.IsRoomIn ?? false, detail.IsItemRoom ?? false,
                    //                                                  charges.TariffDiscountForRoomIn ?? 0, detail.DiscountAmount ?? 0, false);

                    var calc = new Helper.CostCalculation(grrId, itemId, total ?? 0, tblCovered, detail.ChargeQuantity ?? 0, detail.IsCito ?? false, detail.IsCitoInPercent ?? false,
                        detail.BasicCitoAmount ?? 0, detail.Price ?? 0, charges.IsRoomIn ?? false, detail.IsItemRoom ?? false, charges.TariffDiscountForRoomIn ?? 0,
                        detail.DiscountAmount ?? 0, false, detail.ItemConditionRuleID, tariffDate, detail.IsVariable ?? false);

                    #region CostCalculation
                    var cost = new CostCalculation();
                    cost.Query.Where(
                        cost.Query.RegistrationNo.Equal(reg.RegistrationNo),
                        cost.Query.TransactionNo.Equal(detail.TransactionNo),
                        cost.Query.SequenceNo.Equal(detail.SequenceNo)
                        );
                    if (!cost.Load(cost.Query)) cost = collCost.AddNew();

                    cost.RegistrationNo = reg.RegistrationNo;
                    cost.TransactionNo = detail.TransactionNo;
                    cost.SequenceNo = detail.SequenceNo;
                    cost.ItemID = detail.ItemID;

                    var py = new TransPaymentItemOrderQuery();
                    py.Where(py.TransactionNo == detail.TransactionNo, py.SequenceNo == detail.SequenceNo, py.IsPaymentProceed == true, py.IsPaymentReturned == false);
                    bool isPaid = py.LoadDataTable().Rows.Count > 0;
                    if (isPaid)
                    {
                        cost.PatientAmount = calc.PatientAmount + calc.GuarantorAmount;
                        cost.GuarantorAmount = 0;
                        cost.DiscountAmount = 0;
                    }
                    else
                    {
                        decimal? totaltrans = calc.GuarantorAmount + calc.PatientAmount + (detail.DiscountAmount ?? 0);
                        decimal? totaldisc = detail.DiscountAmount ?? 0;

                        if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                        {
                            if (totaldisc >= totaltrans)
                            {
                                cost.GuarantorAmount = 0;
                                cost.PatientAmount = 0;
                            }
                            else
                            {
                                cost.GuarantorAmount = totaltrans - totaldisc;
                                cost.PatientAmount = 0;
                            }
                            cost.DiscountAmount = totaldisc;
                        }
                        else
                        {
                            if (calc.GuarantorAmount > 0)
                            {
                                cost.DiscountAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                           ? (calc.GuarantorAmount + detail.DiscountAmount)
                                                           : totaldisc;

                                cost.GuarantorAmount = totaldisc > (calc.GuarantorAmount + detail.DiscountAmount)
                                                           ? 0
                                                           : (calc.GuarantorAmount + detail.DiscountAmount) - totaldisc;
                                cost.PatientAmount = calc.PatientAmount;
                            }
                            else
                            {
                                cost.DiscountAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                          ? calc.PatientAmount + detail.DiscountAmount
                                                          : totaldisc;

                                cost.PatientAmount = totaldisc > calc.PatientAmount + detail.DiscountAmount
                                                         ? 0
                                                         : calc.PatientAmount + detail.DiscountAmount - totaldisc;
                                cost.GuarantorAmount = calc.GuarantorAmount;
                            }

                            if (totaldisc > cost.DiscountAmount)
                            {
                                //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                                var compColl = collComp.Where(
                                        t =>
                                        t.TransactionNo == detail.TransactionNo &&
                                        t.SequenceNo == detail.SequenceNo)
                                        .OrderBy(t => t.TariffComponentID);
                                var i = compColl.Count();

                                foreach (var compEntity in compColl)
                                {
                                    compEntity.DiscountAmount = i == 1
                                                               ? (cost.DiscountAmount / Math.Abs(detail.ChargeQuantity ?? 0))
                                                               : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / detail.DiscountAmount);

                                    var fee = compEntity.CalculateParamedicPercentDiscount(
                                        AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                        cost.RegistrationNo, detail.ItemID, (compEntity.DiscountAmount ?? 0),
                                        AppSession.UserLogin.UserID, charges.ClassID, charges.ToServiceUnitID);

                                }

                                collComp.Save();

                                detail.DiscountAmount = cost.DiscountAmount;
                                detail.Save();
                            }
                        }
                    }

                    cost.DiscountAmount = detail.DiscountAmount;
                    cost.IsPackage = detail.IsPackage;
                    cost.ParentNo = detail.ParentNo;
                    cost.ParamedicAmount = detail.ChargeQuantity * collComp.Where(comp => comp.TransactionNo == detail.TransactionNo &&
                                                                                          comp.SequenceNo == detail.SequenceNo &&
                                                                                          !string.IsNullOrEmpty(comp.ParamedicID))
                                                                           .Sum(comp => comp.Price - comp.DiscountAmount + comp.CitoAmount);
                    cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    #endregion
                }
            }

            var detil = seqs.Select(item => item.SequenceNo).ToArray();

            var consumption = new TransChargesItemConsumptionCollection();

            if (seqs.Select(item => item.SequenceNo).Any())
            {
                var conss = TransChargesItemConsumptions.Where(c =>
                    c.TransactionNo == Request.QueryString["joNo"] &&
                    detil.Contains(c.SequenceNo));
                foreach (var cons in conss)
                {
                    // set default loc
                    if (string.IsNullOrEmpty(cons.LocationID))
                    {
                        cons.LocationID = charges.LocationID;
                    }
                    consumption.AttachEntity(cons);
                }
            }

            using (var trans = new esTransactionScope())
            {
                // stock calculation
                var chargesBalances = new ItemBalanceCollection();
                var chargesDetailBalances = new ItemBalanceDetailCollection();
                var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var chargesMovements = new ItemMovementCollection();

                string itemNoStock;
                var transChargesItems = TransChargesItems;

                ItemBalance.PrepareItemBalances(transChargesItems, charges.ToServiceUnitID, charges.LocationID, AppSession.UserLogin.UserID, true, ref chargesBalances, ref chargesDetailBalances,
                    ref chargesMovements, ref chargesDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                if (!string.IsNullOrEmpty(itemNoStock))
                {
                    string msg;
                    if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|") msg = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                    else msg = "Insufficient balance of item : " + itemNoStock;

                    ShowInformationHeader(msg);
                    return false;
                }

                // charges
                charges.Save();
                transChargesItems.Save();
                collComp.Save();
                collCost.Save();
                consumption.Save();

                // consumption
                var consumptionBalances = new ItemBalanceCollection();
                var consumptionDetailBalances = new ItemBalanceDetailCollection();
                var consumptionDetailBalanceEds = new ItemBalanceDetailEdCollection();
                var consumptionMovements = new ItemMovementCollection();

                // consumption grouping by loc
                var consLocs = consumption.Select(c => c.LocationID).ToArray().Distinct();

                foreach (var consLoc in consLocs)
                {
                    ItemBalance.PrepareItemBalances(consumption, charges.ToServiceUnitID, consLoc, AppSession.UserLogin.UserID,
                    ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements, ref consumptionDetailBalanceEds, AppSession.Parameter.IsEnabledStockWithEdControl, out itemNoStock);

                    if (!string.IsNullOrEmpty(itemNoStock))
                    {
                        string msg;
                        if (itemNoStock.Length >= 5 && itemNoStock.Substring(0, 5) == "Zero|") msg = "Zero cost price of item : " + itemNoStock.Replace("Zero|", "");
                        else msg = "Insufficient balance of item : " + itemNoStock;

                        ShowInformationHeader(msg);
                        return false;
                    }

                    if ((consumptionBalances != null && consumptionBalances.Count > 0) || (chargesBalances != null && chargesBalances.Count > 0))
                    {
                        var loc = new Location();
                        if (loc.LoadByPrimaryKey(consLoc) && loc.IsHoldForTransaction == true)
                        {
                            ShowInformationHeader("Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.");
                            return false;
                        }
                    }

                    if (consumptionBalances != null) consumptionBalances.Save();
                    if (consumptionDetailBalances != null) consumptionDetailBalances.Save();
                    if (consumptionDetailBalanceEds != null) consumptionDetailBalanceEds.Save();
                    if (consumptionMovements != null) consumptionMovements.Save();

                    consumptionBalances.QueryReset();
                    if (consumptionDetailBalances != null)
                        consumptionDetailBalances.QueryReset();
                    if (consumptionDetailBalanceEds != null)
                        consumptionDetailBalanceEds.QueryReset();
                }

                if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                {
                    // extract fee
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.SetFeeByTCIC(collComp, AppSession.UserLogin.UserID);
                    feeColl.Save();
                    //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                    //feeColl.Save();
                }

                if (chargesBalances != null) chargesBalances.Save();
                if (chargesDetailBalances != null) chargesDetailBalances.Save();
                if (chargesDetailBalanceEds != null) chargesDetailBalanceEds.Save();
                if (chargesMovements != null) chargesMovements.Save();

                /* Automatic Journal Testing Start */
                if (detil.Any())
                {
                    var finalCost = new CostCalculationCollection();
                    finalCost.Query.Where(
                        finalCost.Query.RegistrationNo == charges.RegistrationNo,
                        finalCost.Query.TransactionNo == Request.QueryString["joNo"],
                        finalCost.Query.SequenceNo.In(detil)
                        );
                    if (finalCost.LoadAll())
                    {
                        //NOTE: this function will throw an exception!! make sure to do proper error handling
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                            {
                                JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, charges.TransactionNo, AppSession.UserLogin.UserID, 0);
                            }
                            else
                            {
                                var isClosingPeriod = PostingStatus.IsPeriodeClosed(charges.TransactionDate.Value.Date);
                                if (isClosingPeriod)
                                {
                                    ShowInformationHeader("Financial statements for period: " +
                                                          string.Format("{0:MMMM-yyyy}", charges.TransactionDate.Value.Date) +
                                                          " have been closed. Please contact the authorities.");
                                    return false;
                                }

                                var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                                if (type.Contains(reg.SRRegistrationType))
                                {
                                    int? journalId = JournalTransactions.AddNewIncomeJournalTemporary(charges, collComp, reg, unit, finalCost, "JO", AppSession.UserLogin.UserID, 0);
                                }
                            }
                        }
                        /* Automatic Journal Testing End */
                    }
                }

                #region Interop
                if (AppSession.Parameter.IsUsingHisInterop)
                {
                    var patient = new Patient();
                    patient.LoadByPrimaryKey(reg.PatientID);

                    var salutation = string.Empty;
                    var apstd = new AppStandardReferenceItem();
                    if (apstd.LoadByPrimaryKey("Salutation", patient.SRSalutation))
                        salutation = apstd.ItemName;

                    var paramedic = new Paramedic();
                    paramedic.LoadByPrimaryKey(reg.ParamedicID);

                    //interop lab
                    if (charges.ToServiceUnitID == _serviceUnitLaboratoryID || AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(charges.ToServiceUnitID))
                    {
                        if (!AppSession.Parameter.IsUsingHisInteropWithMultipleConnection)
                        {
                            #region with single connection (default)
                            switch (AppSession.Parameter.HisInteropConfigName)
                            {
                                #region PAC_HIS_INTEROP_CONNECTION_NAME
                                case "PAC_HIS_INTEROP_CONNECTION_NAME": //jangan ubah-ubah punya PAC, jadi error !
                                    if (charges.ToServiceUnitID == _serviceUnitLaboratoryID)
                                    {
                                        var lto = new BusinessObject.Interop.PAC.LabTestOrder();
                                        lto.es.Connection.Name = AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME;

                                        if (lto.LoadByPrimaryKey(charges.TransactionNo))
                                        {
                                            foreach (var entity in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                            {
                                                var item = new Item();
                                                item.LoadByPrimaryKey(entity.ItemID);

                                                lto.TestOrderID += item.ItemIDExternal + "^";
                                                lto.TestOrderName += item.ItemName + "^";
                                            }
                                        }
                                        else
                                        {
                                            lto = new BusinessObject.Interop.PAC.LabTestOrder
                                            {
                                                TransactionNo = charges.TransactionNo,
                                                TransactionDate = charges.TransactionDate,
                                                RegistrationNo = charges.RegistrationNo
                                            };

                                            lto.MedicalNo = patient.MedicalNo;
                                            lto.FirstName = patient.FirstName;
                                            lto.MiddleName = patient.MiddleName;
                                            lto.LastName = patient.LastName;
                                            lto.Sex = patient.Sex;
                                            lto.FromServiceUnitID = reg.ServiceUnitID;
                                            lto.FromServiceUnitName = unit.ServiceUnitName;
                                            lto.ClassID = charges.ClassID;

                                            var cls = new Class();
                                            cls.LoadByPrimaryKey(charges.ClassID);
                                            lto.ClassName = cls.ClassName;

                                            lto.CityOfBirth = patient.CityOfBirth;
                                            lto.DateOfBirth = patient.DateOfBirth;
                                            lto.ParamedicID = reg.ParamedicID;

                                            var param = new Paramedic();
                                            param.LoadByPrimaryKey(reg.ParamedicID);
                                            lto.ParamedicName = param.ParamedicName;

                                            lto.StreetName = patient.StreetName;
                                            lto.District = patient.District;
                                            lto.City = patient.City;
                                            lto.County = patient.County;
                                            lto.State = patient.State;
                                            lto.ZipCode = patient.ZipCode;
                                            lto.PhoneNo = patient.PhoneNo;
                                            lto.FaxNo = patient.FaxNo;
                                            lto.Email = patient.Email;
                                            lto.MobilePhoneNo = patient.MobilePhoneNo;
                                            lto.Company = patient.Company;

                                            var grr = new Guarantor();
                                            grr.LoadByPrimaryKey(reg.GuarantorID);
                                            lto.GuarantorName = grr.GuarantorName;

                                            foreach (var entity in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                            {
                                                var item = new Item();
                                                item.LoadByPrimaryKey(entity.ItemID);

                                                lto.TestOrderID += item.ItemIDExternal + "^";
                                                lto.TestOrderName += item.ItemName + "^";
                                            }

                                            lto.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                            lto.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            lto.IsConfirm = false;
                                        }

                                        lto.es.Connection.Name = AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME;
                                        lto.Save();
                                    }
                                    break;
                                #endregion
                                #region RSCH_LIS_INTEROP_CONNECTION_NAME
                                case AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME:
                                    if (charges.ToServiceUnitID == _serviceUnitLaboratoryID)
                                    {
                                        var olh = new BusinessObject.Interop.RSCH.OrderLabHeader();
                                        olh.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;
                                        olh.OrderLabNo = charges.TransactionNo;
                                        olh.OrderLabTglOrder = (new DateTime()).NowAtSqlServer().Date;
                                        olh.OrderLabNoMR = patient.MedicalNo;
                                        olh.OrderLabNama = ((patient.FirstName.Trim() + " " + patient.MiddleName.Trim()).Trim() + " " + patient.LastName.Trim()).Trim();

                                        if (!string.IsNullOrEmpty(reg.PhysicianSenders)) olh.OrderLabNamaPengirim = reg.PhysicianSenders;
                                        else
                                        {
                                            var medic = new Paramedic();
                                            medic.LoadByPrimaryKey(reg.ParamedicID);
                                            olh.OrderLabNamaPengirim = medic.ParamedicName.Trim();
                                        }

                                        olh.OrderLabKdPoli = charges.FromServiceUnitID;
                                        olh.OrderLabBirthdate = patient.DateOfBirth;
                                        olh.OrderLabAgeYear = reg.AgeInYear;
                                        olh.OrderLabAgeMonth = reg.AgeInMonth;
                                        olh.OrderLabAgeDay = reg.AgeInDay;
                                        olh.OrderLabSex = patient.Sex;
                                        olh.OrderLabKdPengirim = string.Empty;

                                        unit = new ServiceUnit();
                                        unit.LoadByPrimaryKey(charges.FromServiceUnitID);
                                        olh.OrderlabNamaPoli = unit.ServiceUnitName.Trim();

                                        olh.OrderLabJamOrder = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                                        olh.OrderLabStatus = string.Empty;
                                        olh.OrderLabNoBed = reg.BedID;
                                        olh.GuarantorName = guar.GuarantorName;

                                        if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                                        {
                                            var soap = new RegistrationInfoMedic();
                                            soap.Query.es.Top = 1;
                                            soap.Query.Where(soap.Query.RegistrationNo == reg.RegistrationNo, soap.Query.SRMedicalNotesInputType == "SOAP");
                                            soap.Query.OrderBy(soap.Query.RegistrationInfoMedicID.Descending);
                                            if (soap.Query.Load()) olh.DiagnoseText = soap.Info3;
                                            else olh.DiagnoseText = string.Empty;
                                        }
                                        else if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                                        {
                                            var nhd = new NursingTransHD();
                                            nhd.Query.es.Top = 1;
                                            nhd.Query.Where(nhd.Query.RegistrationNo == reg.RegistrationNo);
                                            if (nhd.Query.Load())
                                            {
                                                var ndt = new NursingDiagnosaTransDT();
                                                ndt.Query.es.Top = 1;
                                                ndt.Query.Where(ndt.Query.TransactionNo == nhd.TransactionNo);
                                                ndt.Query.OrderBy(ndt.Query.Priority.Ascending);
                                                if (ndt.Query.Load()) olh.DiagnoseText = ndt.NursingDiagnosaName;
                                                else olh.DiagnoseText = string.Empty;
                                            }
                                            else olh.DiagnoseText = string.Empty;
                                        }

                                        olh.Save();

                                        var details = new BusinessObject.Interop.RSCH.OrderLabDetailCollection();
                                        details.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;

                                        foreach (var entity in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                        {
                                            var old = details.AddNew();

                                            var item = new Item();
                                            item.LoadByPrimaryKey(entity.ItemID);

                                            old.OrderLabNo = charges.TransactionNo;
                                            old.OrderLabTglOrder = olh.OrderLabTglOrder;
                                            old.CheckupResultTestCode = item.ItemIDExternal;
                                            old.OrderLabJamOrder = olh.OrderLabJamOrder;
                                            old.OrderLabStatus = string.Empty;
                                            old.OrderLabCito = (entity.IsCito ?? false) ? "C" : string.Empty;
                                        }

                                        if (details.Any()) details.Save();
                                    }
                                    break;
                                #endregion
                                #region SYSMEX_LIS_INTEROP_CONNECTION_NAME
                                case AppConstant.HIS_INTEROP.MEDICLAB_LIS_INTEROP_CONNECTION_NAME:
                                case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                                case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                                    if (charges.ToServiceUnitID == _serviceUnitLaboratoryID)
                                    {
                                        if (SysmexLisInteropUpdateSingleConnection(charges, unit, reg, guar, transChargesItems, patient, salutation))
                                        {
                                            // Update info satusehat
                                            SatusehatServiceRequestPostAndLogToLis(reg, charges.TransactionNo, AppSession.Parameter.HisInteropConfigName);
                                        }
                                    }
                                    break;
                                #endregion
                                #region PRODIA_LIS_INTEROP_CONNECTION_NAME
                                case AppConstant.HIS_INTEROP.PRODIA_LIS_INTEROP_CONNECTION_NAME:
                                    break;
                                #endregion
                                #region ELIMS_LIS_INTEROP_CONNECTION_NAME
                                case AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME:
                                    if (charges.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID || AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(charges.ToServiceUnitID))
                                    {

                                        //var kl = new BusinessObject.Interop.ELIMS.KirimLIS();
                                        //kl.es.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;

                                        var kls = new BusinessObject.Interop.ELIMS.KirimLISCollection();

                                        switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                        {
                                            case "RSISB":
                                                {
                                                    foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                    {
                                                        var kl = kls.AddNew();
                                                        kl.ModifiedDate = charges.ExecutionDate;
                                                        kl.NoPasien = patient.MedicalNo;
                                                        kl.KodeKunjungan = charges.TransactionNo;
                                                        kl.Nama = patient.PatientName + " " + apstd.ItemName;
                                                        kl.Email = patient.Email;
                                                        kl.DateOfBirth = patient.DateOfBirth;
                                                        kl.UmurTahun = reg.AgeInYear;
                                                        kl.UmurBulan = reg.AgeInMonth;
                                                        kl.UmurHari = reg.AgeInDay;
                                                        kl.Gender = patient.Sex == "M" ? "L" : "P";
                                                        kl.Alamat = patient.Address;
                                                        kl.Diagnosa = reg.Anamnesis;
                                                        kl.TglPeriksa = charges.ExecutionDate;
                                                        kl.Pengirim = reg.ParamedicID;

                                                        if (!string.IsNullOrEmpty(reg.PhysicianSenders)) kl.PengirimName = reg.PhysicianSenders;
                                                        else
                                                        {
                                                            var medic = new Paramedic();
                                                            medic.LoadByPrimaryKey(reg.ParamedicID);
                                                            kl.PengirimName = medic.ParamedicName;
                                                        }

                                                        var cls = new Class();
                                                        cls.LoadByPrimaryKey(charges.ClassID);
                                                        kl.Kelas = cls.ClassID;
                                                        kl.KelasName = cls.ClassID;

                                                        var room = new ServiceRoom();
                                                        room.LoadByPrimaryKey(charges.RoomID);
                                                        kl.Ruang = room.RoomID;
                                                        kl.RuangName = room.RoomName;

                                                        kl.CaraBayar = guar.GuarantorID;
                                                        kl.CaraBayarName = guar.GuarantorName;



                                                        kl.KodeTarif = detail.ItemID;
                                                        kl.Update = detail.IsCorrection == false ? "N" : "D";


                                                        kl.ISInap = reg.SRRegistrationType == "IPR" ? "1" : "0";
                                                        kl.Status = "1";

                                                        kl.NIK = patient.Ssn;



                                                        kl.IS_CITO = detail.IsCito == false ? "0" : "1";



                                                    }

                                                }
                                                break;
                                            default:
                                                foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                {
                                                    var kl = kls.AddNew();
                                                    kl.ModifiedDate = charges.ExecutionDate;
                                                    kl.NoPasien = patient.MedicalNo;
                                                    kl.KodeKunjungan = charges.TransactionNo;
                                                    kl.Nama = patient.PatientName + " " + apstd.ItemName;
                                                    kl.Email = patient.Email;
                                                    kl.DateOfBirth = patient.DateOfBirth;
                                                    kl.UmurTahun = reg.AgeInYear;
                                                    kl.UmurBulan = reg.AgeInMonth;
                                                    kl.UmurHari = reg.AgeInDay;
                                                    kl.Gender = patient.Sex == "M" ? "L" : "P";
                                                    kl.Alamat = patient.Address;
                                                    kl.Diagnosa = reg.Anamnesis;
                                                    kl.TglPeriksa = charges.ExecutionDate;
                                                    kl.Pengirim = reg.ParamedicID;

                                                    if (!string.IsNullOrEmpty(reg.PhysicianSenders)) kl.PengirimName = reg.PhysicianSenders;
                                                    else
                                                    {
                                                        var medic = new Paramedic();
                                                        medic.LoadByPrimaryKey(reg.ParamedicID);
                                                        kl.PengirimName = medic.ParamedicName;
                                                    }

                                                    var cls = new Class();
                                                    cls.LoadByPrimaryKey(charges.ClassID);
                                                    kl.Kelas = cls.ClassID;
                                                    kl.KelasName = cls.ClassID;

                                                    var room = new ServiceRoom();
                                                    room.LoadByPrimaryKey(charges.RoomID);
                                                    kl.Ruang = room.RoomID;
                                                    kl.RuangName = room.RoomName;

                                                    kl.CaraBayar = guar.GuarantorID;
                                                    kl.CaraBayarName = guar.GuarantorName;



                                                    kl.KodeTarif = detail.ItemID;
                                                    kl.Update = detail.IsCorrection == false ? "N" : "D";


                                                    kl.ISInap = reg.SRRegistrationType == "IPR" ? "1" : "0";
                                                    kl.Status = "1";


                                                    kl.NIK = patient.Ssn;



                                                    kl.IS_CITO = detail.IsCito == false ? "0" : "1";

                                                }


                                                break;
                                        }

                                        kls.es.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;
                                        kls.Save();

                                    }
                                    break;
                                #endregion
                                case AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME:
                                    var orderDateTime = WynakomLisInteropUpdateSingleConnection(charges, patient, reg, transChargesItems, unit, guar);

                                    if (orderDateTime != null)
                                    {
                                        // Update info satusehat
                                        SatusehatServiceRequestPostAndLogToLis(reg, charges.TransactionNo, AppSession.Parameter.HisInteropConfigName);
                                    }
                                    break;
                                case AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME:
                                    var id_ruangan = new ServiceUnitBridging();
                                    id_ruangan.Query.Where(id_ruangan.Query.ServiceUnitID == reg.ServiceUnitID, id_ruangan.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                    if (!id_ruangan.Query.Load())
                                    {
                                        var ruangan = new ServiceUnit();
                                        ruangan.LoadByPrimaryKey(reg.ServiceUnitID);
                                        ShowInformationHeader("Service Unit : " + ruangan.ServiceUnitName + " not mapped to Link Lis.");
                                        return false;
                                    }

                                    var id_dokter = new ParamedicBridging();
                                    id_dokter.Query.Where(id_dokter.Query.ParamedicID == reg.ParamedicID, id_dokter.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                    if (!id_dokter.Query.Load())
                                    {
                                        var ruangan = new Paramedic();
                                        ruangan.LoadByPrimaryKey(reg.ParamedicID);
                                        ShowInformationHeader("Physician : " + ruangan.ParamedicName + " not mapped to Link Lis.");
                                        return false;
                                    }

                                    var id_status = new GuarantorBridging();
                                    id_status.Query.Where(id_status.Query.GuarantorID == reg.GuarantorID, id_status.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                    if (!id_status.Query.Load())
                                    {
                                        var ruangan = new Guarantor();
                                        ruangan.LoadByPrimaryKey(reg.GuarantorID);
                                        ShowInformationHeader("Guarantor : " + ruangan.GuarantorName + " not mapped to Link Lis.");
                                        return false;
                                    }

                                    foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                    {
                                        var ib = new ItemBridging();
                                        ib.Query.Where(ib.Query.ItemID == detail.ItemID, ib.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                        if (!ib.Query.Load())
                                        {
                                            var ruangan = new Item();
                                            ruangan.LoadByPrimaryKey(detail.ItemID);
                                            ShowInformationHeader("Item : " + ruangan.ItemName + " not mapped to Link Lis.");
                                            return false;
                                        }
                                    }

                                    if (transChargesItems.Any(t => t.IsOrderRealization ?? false))
                                    {
                                        var svc = new Common.LinkLis.Service();
                                        var response = svc.InsertRegistrasiPasien(new Common.LinkLis.Object.RegistrasiPasien()
                                        {
                                            no_rm = patient.MedicalNo,
                                            nama = patient.PatientName,
                                            alamat = patient.Address,
                                            tgl_lahir = patient.DateOfBirth.Value.ToString("dd-MM-yyyy"),
                                            jenis_kelamin = patient.Sex == "F" ? "Perempuan" : "Laki-laki",
                                            status = id_status.BridgingID
                                        }, true);

                                        svc = new Common.LinkLis.Service();
                                        var kode = svc.GetKodePemeriksaan();

                                        svc = new Common.LinkLis.Service();
                                        response = svc.InsertRegistrasiPemeriksaan(new Common.LinkLis.Object.RegistrasiPemeriksaan()
                                        {
                                            kode_pemeriksaan = kode.KodePemeriksaan,
                                            no_rm = patient.MedicalNo,
                                            id_ruangan = id_ruangan.BridgingID,
                                            id_dokter = id_dokter.BridgingID.Split('-')[1],
                                            id_analis = charges.AnalystID.Split('-')[1],
                                            id_dokterpk = charges.LaboratoryParamedicID.Split('-')[1],
                                            id_status = id_status.BridgingID
                                        }, true);

                                        var list_pemeriksaan = new List<Common.LinkLis.Object.ListPemeriksaan>();
                                        var list_parameter = new List<Common.LinkLis.Object.ListParameter>();
                                        foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                        {
                                            var ib = new ItemBridging();
                                            ib.Query.Where(ib.Query.ItemID == detail.ItemID, ib.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                            ib.Query.Load();

                                            if (!list_pemeriksaan.Any(p => p.list_pemeriksaan == ib.BridgingGroupID)) list_pemeriksaan.Add(new Common.LinkLis.Object.ListPemeriksaan() { list_pemeriksaan = ib.BridgingGroupID });
                                            list_parameter.Add(new Common.LinkLis.Object.ListParameter() { list_pemeriksaan = ib.BridgingGroupID, list_parameter = ib.BridgingID });
                                            detail.ResultValue = kode.KodePemeriksaan;
                                        }

                                        svc = new Common.LinkLis.Service();
                                        response = svc.InsertRegistrasiParameterPemeriksaan(new Common.LinkLis.Object.ParameterPemeriksaan()
                                        {
                                            kode_pemeriksaan = kode.KodePemeriksaan,
                                            list_pemeriksaan = list_pemeriksaan,
                                            list_parameter = list_parameter,
                                            feedback_id = null,
                                            feedback_url = null
                                        });

                                        transChargesItems.Save();
                                    }
                                    break;
                                case AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME:
                                    {
                                        var pmedic = new Paramedic();
                                        pmedic.LoadByPrimaryKey(reg.ParamedicID);

                                        var tunit = new ServiceUnit();
                                        tunit.LoadByPrimaryKey(charges.FromServiceUnitID);

                                        var vlites = new BusinessObject.Interop.VANSLITE.OrderLabCollection();

                                        foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                        {
                                            var vlite = vlites.AddNew();
                                            vlite.AsalLab = tunit.ServiceUnitName;
                                            vlite.NoLab = detail.TransactionNo;
                                            vlite.NoLabDtl = detail.SequenceNo;
                                            vlite.NoRegistrasi = reg.RegistrationNo;
                                            vlite.NoRm = patient.MedicalNo;
                                            vlite.TglOrder = Convert.ToDateTime(charges.ExecutionDate.Value.ToString("MM/dd/yyyy"));
                                            vlite.NamaPas = patient.PatientName;
                                            vlite.JenisKel = patient.Sex == "M" ? "L" : "P";
                                            vlite.TglLahir = patient.DateOfBirth;
                                            vlite.Usia = Helper.GetAgeInYear(patient.DateOfBirth.Value, charges.ExecutionDate.Value).ToString();
                                            vlite.Alamat = patient.Address;
                                            vlite.KodeDokKirim = reg.ParamedicID;
                                            vlite.NamaDokKirim = pmedic.ParamedicName;
                                            vlite.KodeRuang = charges.FromServiceUnitID;
                                            vlite.NamaRuang = tunit.ServiceUnitName;
                                            vlite.KodeCaraBayar = reg.GuarantorID;
                                            vlite.CaraBayar = guar.GuarantorName;
                                            vlite.KetKlinis = reg.Complaint;
                                            vlite.KodeTest = detail.ItemID;
                                            var item = new Item();
                                            item.LoadByPrimaryKey(detail.ItemID);
                                            vlite.Test = item.ItemName;
                                            vlite.Harga = (detail.Price ?? 0).ToInt();
                                            vlite.WaktuKirim = Convert.ToDateTime(charges.ExecutionDate.Value.ToString("HH:mm:ss"));
                                            vlite.Prioritas = (detail.IsCito ?? false) ? "C" : "R";
                                            vlite.JnsRawat = reg.SRRegistrationType == "IPR" ? "Rawat Inap" : "Rawat Jalan";
                                            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                                            {
                                                vlite.DokJaga = cboParamedicID.Text;
                                            }
                                            else
                                            {
                                                if (string.IsNullOrWhiteSpace(detail.ParamedicID)) vlite.DokJaga = "";
                                                else
                                                {
                                                    var tmedic = new Paramedic();
                                                    tmedic.LoadByPrimaryKey(detail.ParamedicID);
                                                    vlite.DokJaga = tmedic.ParamedicName;
                                                }
                                            }
                                            vlite.Status = 0;
                                            vlite.Batal = 0;
                                            vlite.JumlahTest = (detail.ChargeQuantity ?? 0).ToInt();

                                            detail.IsSendToLIS = true;
                                        }

                                        transChargesItems.Save();

                                        vlites.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME;
                                        vlites.Save();
                                    }
                                    break;
                            }
                            #endregion
                        }
                        else
                        {
                            #region with multiple connection 
                            var listItem = transChargesItems.Where(t => t.IsOrderRealization ?? false && t.IsVoid == false).Select(q => new
                            {
                                ItemId = q.ItemID
                            });

                            if (listItem.Any())
                            {
                                var itemBridgings = new ItemBridgingCollection();
                                var ibq = new ItemBridgingQuery("a");
                                var connq = new AppStandardReferenceItemQuery("b");
                                ibq.Select(ibq, connq.ReferenceID.As("refToAppStandardReferenceItem_BridgingTypeReferenceID"));
                                ibq.InnerJoin(connq).On(connq.StandardReferenceID == AppEnum.StandardReference.BridgingType.ToString() && connq.ItemID == ibq.SRBridgingType);
                                ibq.Where(ibq.ItemID.In(listItem.Select(l => l.ItemId)), ibq.IsActive == true,
                                    connq.ReferenceID.IsNotNull(), connq.ReferenceID != string.Empty);
                                ibq.OrderBy(connq.ReferenceID.Ascending, ibq.ItemID.Ascending);
                                itemBridgings.Load(ibq);

                                foreach (var group in (from g in itemBridgings
                                                       group g by new
                                                       {
                                                           g.ConnectionName
                                                       }
                                                       into grp
                                                       orderby grp.Key.ConnectionName
                                                       select new
                                                       {
                                                           ConnectionName = grp.Key.ConnectionName
                                                       }))
                                {
                                    switch (group.ConnectionName)
                                    {
                                        #region PAC_HIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME: //jangan ubah-ubah punya PAC, jadi error !
                                            if (charges.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                            {
                                                var lto = new BusinessObject.Interop.PAC.LabTestOrder();
                                                lto.es.Connection.Name = AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME;

                                                if (lto.LoadByPrimaryKey(charges.TransactionNo))
                                                {
                                                    foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                    {
                                                        var item = new Item();
                                                        item.LoadByPrimaryKey(i.ItemID);

                                                        lto.TestOrderID += i.ItemIdExternal + "^";
                                                        lto.TestOrderName += item.ItemName + "^";
                                                    }
                                                }
                                                else
                                                {
                                                    lto = new BusinessObject.Interop.PAC.LabTestOrder
                                                    {
                                                        TransactionNo = charges.TransactionNo,
                                                        TransactionDate = charges.TransactionDate,
                                                        RegistrationNo = charges.RegistrationNo
                                                    };

                                                    lto.MedicalNo = patient.MedicalNo;
                                                    lto.FirstName = patient.FirstName;
                                                    lto.MiddleName = patient.MiddleName;
                                                    lto.LastName = patient.LastName;
                                                    lto.Sex = patient.Sex;
                                                    lto.FromServiceUnitID = reg.ServiceUnitID;
                                                    lto.FromServiceUnitName = unit.ServiceUnitName;
                                                    lto.ClassID = charges.ClassID;

                                                    var cls = new Class();
                                                    cls.LoadByPrimaryKey(charges.ClassID);
                                                    lto.ClassName = cls.ClassName;

                                                    lto.CityOfBirth = patient.CityOfBirth;
                                                    lto.DateOfBirth = patient.DateOfBirth;
                                                    lto.ParamedicID = reg.ParamedicID;
                                                    lto.ParamedicName = paramedic.ParamedicName;

                                                    lto.StreetName = patient.StreetName;
                                                    lto.District = patient.District;
                                                    lto.City = patient.City;
                                                    lto.County = patient.County;
                                                    lto.State = patient.State;
                                                    lto.ZipCode = patient.ZipCode;
                                                    lto.PhoneNo = patient.PhoneNo;
                                                    lto.FaxNo = patient.FaxNo;
                                                    lto.Email = patient.Email;
                                                    lto.MobilePhoneNo = patient.MobilePhoneNo;
                                                    lto.Company = patient.Company;

                                                    var grr = new Guarantor();
                                                    grr.LoadByPrimaryKey(reg.GuarantorID);
                                                    lto.GuarantorName = grr.GuarantorName;

                                                    foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                    {
                                                        var item = new Item();
                                                        item.LoadByPrimaryKey(i.ItemID);

                                                        lto.TestOrderID += i.ItemIdExternal + "^";
                                                        lto.TestOrderName += item.ItemName + "^";
                                                    }

                                                    lto.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                                    lto.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                    lto.IsConfirm = false;
                                                }

                                                lto.es.Connection.Name = AppConstant.HIS_INTEROP.PAC_HIS_INTEROP_CONNECTION_NAME;
                                                lto.Save();
                                            }
                                            break;
                                        #endregion
                                        #region RSCH_LIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME:
                                            if (charges.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                            {
                                                var olh = new BusinessObject.Interop.RSCH.OrderLabHeader();
                                                olh.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;
                                                olh.OrderLabNo = charges.TransactionNo;
                                                olh.OrderLabTglOrder = (new DateTime()).NowAtSqlServer().Date;
                                                olh.OrderLabNoMR = patient.MedicalNo;
                                                olh.OrderLabNama = ((patient.FirstName.Trim() + " " + patient.MiddleName.Trim()).Trim() + " " + patient.LastName.Trim()).Trim();

                                                if (!string.IsNullOrEmpty(reg.PhysicianSenders))
                                                    olh.OrderLabNamaPengirim = reg.PhysicianSenders;
                                                else
                                                    olh.OrderLabNamaPengirim = paramedic.ParamedicName.Trim();

                                                olh.OrderLabKdPoli = charges.FromServiceUnitID;
                                                olh.OrderLabBirthdate = patient.DateOfBirth;
                                                olh.OrderLabAgeYear = reg.AgeInYear;
                                                olh.OrderLabAgeMonth = reg.AgeInMonth;
                                                olh.OrderLabAgeDay = reg.AgeInDay;
                                                olh.OrderLabSex = patient.Sex;
                                                olh.OrderLabKdPengirim = string.Empty;

                                                unit = new ServiceUnit();
                                                unit.LoadByPrimaryKey(charges.FromServiceUnitID);
                                                olh.OrderlabNamaPoli = unit.ServiceUnitName.Trim();

                                                olh.OrderLabJamOrder = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
                                                olh.OrderLabStatus = string.Empty;
                                                olh.OrderLabNoBed = reg.BedID;
                                                olh.GuarantorName = guar.GuarantorName;

                                                if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                                                {
                                                    var soap = new RegistrationInfoMedic();
                                                    soap.Query.es.Top = 1;
                                                    soap.Query.Where(soap.Query.RegistrationNo == reg.RegistrationNo, soap.Query.SRMedicalNotesInputType == "SOAP");
                                                    soap.Query.OrderBy(soap.Query.RegistrationInfoMedicID.Descending);
                                                    if (soap.Query.Load())
                                                        olh.DiagnoseText = soap.Info3;
                                                    else
                                                        olh.DiagnoseText = string.Empty;
                                                }
                                                else if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                                                {
                                                    var nhd = new NursingTransHD();
                                                    nhd.Query.es.Top = 1;
                                                    nhd.Query.Where(nhd.Query.RegistrationNo == reg.RegistrationNo);
                                                    if (nhd.Query.Load())
                                                    {
                                                        var ndt = new NursingDiagnosaTransDT();
                                                        ndt.Query.es.Top = 1;
                                                        ndt.Query.Where(ndt.Query.TransactionNo == nhd.TransactionNo);
                                                        ndt.Query.OrderBy(ndt.Query.Priority.Ascending);
                                                        if (ndt.Query.Load())
                                                            olh.DiagnoseText = ndt.NursingDiagnosaName;
                                                        else
                                                            olh.DiagnoseText = string.Empty;
                                                    }
                                                    else olh.DiagnoseText = string.Empty;
                                                }

                                                olh.Save();

                                                var details = new BusinessObject.Interop.RSCH.OrderLabDetailCollection();
                                                details.es.Connection.Name = AppConstant.HIS_INTEROP.RSCH_LIS_INTEROP_CONNECTION_NAME;

                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                {
                                                    var old = details.AddNew();

                                                    var item = new Item();
                                                    item.LoadByPrimaryKey(i.ItemID);

                                                    old.OrderLabNo = charges.TransactionNo;
                                                    old.OrderLabTglOrder = olh.OrderLabTglOrder;
                                                    old.CheckupResultTestCode = i.ItemIdExternal;
                                                    old.OrderLabJamOrder = olh.OrderLabJamOrder;
                                                    old.OrderLabStatus = string.Empty;

                                                    var isCito = false;
                                                    foreach (var findCito in transChargesItems.Where(findCito => findCito.IsOrderRealization ?? false && findCito.ItemID.Equals(i.ItemID) && findCito.IsCito == true))
                                                    {
                                                        isCito = true;
                                                        break;
                                                    }

                                                    old.OrderLabCito = isCito ? "C" : string.Empty;
                                                }

                                                if (details.Any()) details.Save();
                                            }
                                            break;
                                        #endregion
                                        #region SYSMEX_LIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                                        case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                                            if (charges.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID)
                                            {
                                                var lo = new BusinessObject.Interop.SYSMEX.LisOrder();
                                                if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
                                                else lo.es.Connection.Name = AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME;

                                                lo.MessageDt = (charges.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                lo.OrderControl = "NW";
                                                if (AppSession.Parameter.HealthcareInitialAppsVersion == "GRHA") lo.Pid = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
                                                else lo.Pid = patient.MedicalNo;
                                                lo.Pname = patient.PatientName;
                                                lo.Address1 = patient.StreetName;

                                                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                {
                                                    case "RSTJ":
                                                        lo.Pname = (patient.PatientName + " " + salutation).Trim();
                                                        lo.Address2 = patient.District;
                                                        lo.Address3 = patient.County;
                                                        lo.Address4 = patient.State;
                                                        break;
                                                    case "RSUTAMA":
                                                    case "KLUTAMA":
                                                        lo.Address2 = patient.District;
                                                        lo.Address3 = patient.County + " " + patient.State;
                                                        lo.Address4 = patient.MobilePhoneNo;
                                                        break;

                                                    case "RSMP":
                                                    case "GRHA":
                                                        var refral = new Referral();
                                                        refral.LoadByPrimaryKey(reg.ReferralID);

                                                        lo.Address2 = patient.District;
                                                        lo.Address3 = string.IsNullOrEmpty(reg.ReferralID) ? reg.ReferralName : refral.ReferralName;
                                                        lo.Address4 = guar.GuarantorName;
                                                        break;
                                                    case "RSSMCB":
                                                        lo.Address2 = guar.GuarantorName;
                                                        lo.Address3 = patient.District + " " + patient.County;
                                                        if (AppSession.Parameter.IsUsingHisInteropToHcLab)
                                                        {
                                                            lo.Address4 = patient.MobilePhoneNo;
                                                        }
                                                        else
                                                        {
                                                            if (AppSession.Parameter.HealthcareInitial == "RSSMHB")
                                                            {
                                                                lo.Address3 += " " + patient.State;

                                                                var mb = new MergeBilling();
                                                                if (mb.LoadByPrimaryKey(reg.RegistrationNo))
                                                                {
                                                                    if (!string.IsNullOrEmpty(mb.FromRegistrationNo))
                                                                    {
                                                                        var freg = new Registration();
                                                                        freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                                                                        var funit = new ServiceUnit();
                                                                        funit.LoadByPrimaryKey(freg.ServiceUnitID);
                                                                        lo.Address4 = funit.ServiceUnitID + "^" + funit.ServiceUnitName;
                                                                    }
                                                                    else lo.Address4 = unit.ServiceUnitID + "^" + unit.ServiceUnitName; ;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                lo.Address4 = patient.State;
                                                            }
                                                        }

                                                        break;
                                                    case "YBRSGKP":
                                                        lo.Address2 = "";
                                                        switch (guar.SRGuarantorType)
                                                        {
                                                            case "09":
                                                                lo.Address2 = "BPJS";
                                                                break;
                                                            case "00":
                                                                lo.Address2 = "PRIBADI";
                                                                break;
                                                            default:
                                                                lo.Address2 = "MITRA";
                                                                break;

                                                        }

                                                        lo.Address3 = guar.GuarantorName;
                                                        lo.Address4 = charges.ClassID;

                                                        var cls = new Class();
                                                        cls.LoadByPrimaryKey(charges.ClassID);
                                                        lo.Address4 = reg.SRRegistrationType == "IPR" ? cls.ClassName : string.Empty;
                                                        break;
                                                    default:
                                                        lo.Address2 = patient.District;
                                                        lo.Address3 = patient.County;
                                                        lo.Address4 = patient.State;
                                                        break;
                                                }

                                                lo.Ptype = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "IN" : "OP";
                                                lo.BirthDt = (patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
                                                lo.Sex = patient.Sex == "M" ? "1" : "0";
                                                lo.Ono = charges.TransactionNo;
                                                lo.RequestDt = (charges.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");

                                                unit = new ServiceUnit();
                                                unit.LoadByPrimaryKey(charges.FromServiceUnitID);

                                                lo.Source = unit.ServiceUnitID + "^" + unit.ServiceUnitName;

                                                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                {
                                                    case "RSUTAMA":
                                                        if (!string.IsNullOrEmpty(reg.ReferralID))
                                                        {
                                                            var refer = new Referral();
                                                            refer.LoadByPrimaryKey(reg.ReferralID);

                                                            lo.Clinician = reg.ReferralID + "^" + refer.ReferralName;
                                                        }
                                                        else if (!string.IsNullOrEmpty(reg.PhysicianSenders))
                                                        {
                                                            lo.Clinician = reg.ParamedicID + "^" + reg.PhysicianSenders;
                                                        }
                                                        else
                                                        {
                                                            lo.Clinician = reg.ParamedicID + "^" + paramedic.ParamedicName;
                                                        }
                                                        break;
                                                    default:
                                                        lo.Clinician = reg.ParamedicID + "^" + paramedic.ParamedicName;
                                                        break;
                                                }

                                                lo.RoomNo = reg.RoomID;
                                                lo.Priority = transChargesItems.Any(t => (t.IsOrderRealization ?? false) && (t.IsCito ?? false)) ? "U" : "R";
                                                if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME)
                                                    lo.Cmt = guar.GuarantorName;
                                                else
                                                {
                                                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB" && AppSession.Parameter.IsUsingHisInteropToHcLab)
                                                        lo.Cmt = patient.Ssn;
                                                    else
                                                        lo.Cmt = charges.Notes; //string.Empty;
                                                }

                                                lo.Visitno = charges.RegistrationNo;

                                                var prefixCode = AppSession.Parameter.PrefixOnoSysmexInterop;
                                                if (!string.IsNullOrWhiteSpace(prefixCode))
                                                {
                                                    lo.HealthcareCode = prefixCode;
                                                    lo.Pid = prefixCode + lo.Pid;
                                                    lo.Ono = prefixCode + lo.Ono;
                                                    lo.Source = prefixCode + lo.Source;
                                                    lo.Clinician = prefixCode + lo.Clinician;
                                                }

                                                //switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                //{
                                                //    case "RSUI":
                                                //        lo.HealthcareCode = "TG";
                                                //        lo.Pid = "TG" + lo.Pid;
                                                //        lo.Ono = "TG" + lo.Ono;
                                                //        lo.Source = "TG" + lo.Source;
                                                //        lo.Clinician = "TG" + lo.Clinician;
                                                //        break;
                                                //    case "RSPM":
                                                //        lo.HealthcareCode = "ST";
                                                //        lo.Pid = "ST" + lo.Pid;
                                                //        lo.Ono = "ST" + lo.Ono;
                                                //        lo.Source = "ST" + lo.Source;
                                                //        lo.Clinician = "ST" + lo.Clinician;
                                                //        break;
                                                //}

                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                {
                                                    if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME)
                                                        lo.OrderTestid += i.ItemID + "~";
                                                    else lo.OrderTestid += i.ItemIdExternal + "~";
                                                }
                                                lo.Save();
                                            }
                                            break;
                                        #endregion
                                        #region PRODIA_LIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.PRODIA_LIS_INTEROP_CONNECTION_NAME:
                                            break;
                                        #endregion
                                        #region ELIMS_LIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME:
                                            if (charges.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID || AppSession.Parameter.ServiceUnitLaboratoryIdArray.Contains(charges.ToServiceUnitID))
                                            {

                                                //var kl = new BusinessObject.Interop.ELIMS.KirimLIS();
                                                //kl.es.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;

                                                var kls = new BusinessObject.Interop.ELIMS.KirimLISCollection();

                                                switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                                                {
                                                    case "RSISB":
                                                        {
                                                            foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                            {
                                                                var kl = kls.AddNew();
                                                                kl.ModifiedDate = charges.ExecutionDate;
                                                                kl.NoPasien = patient.MedicalNo;
                                                                kl.KodeKunjungan = charges.TransactionNo;
                                                                kl.Nama = patient.PatientName + " " + apstd.ItemName;
                                                                kl.Email = patient.Email;
                                                                kl.DateOfBirth = patient.DateOfBirth;
                                                                kl.UmurTahun = reg.AgeInYear;
                                                                kl.UmurBulan = reg.AgeInMonth;
                                                                kl.UmurHari = reg.AgeInDay;
                                                                kl.Gender = patient.Sex == "M" ? "L" : "P";
                                                                kl.Alamat = patient.Address;
                                                                kl.Diagnosa = reg.Anamnesis;
                                                                kl.TglPeriksa = charges.ExecutionDate;
                                                                kl.Pengirim = reg.ParamedicID;

                                                                if (!string.IsNullOrEmpty(reg.PhysicianSenders)) kl.PengirimName = reg.PhysicianSenders;
                                                                else
                                                                {
                                                                    var medic = new Paramedic();
                                                                    medic.LoadByPrimaryKey(reg.ParamedicID);
                                                                    kl.PengirimName = medic.ParamedicName;
                                                                }

                                                                var cls = new Class();
                                                                cls.LoadByPrimaryKey(charges.ClassID);
                                                                kl.Kelas = cls.ClassID;
                                                                kl.KelasName = cls.ClassID;

                                                                var room = new ServiceRoom();
                                                                room.LoadByPrimaryKey(charges.RoomID);
                                                                kl.Ruang = room.RoomID;
                                                                kl.RuangName = room.RoomName;

                                                                kl.CaraBayar = guar.GuarantorID;
                                                                kl.CaraBayarName = guar.GuarantorName;



                                                                kl.KodeTarif = detail.ItemID;
                                                                kl.Update = detail.IsCorrection == false ? "N" : "D";


                                                                kl.ISInap = reg.SRRegistrationType == "IPR" ? "1" : "0";
                                                                kl.Status = "1";

                                                                kl.NIK = patient.Ssn;



                                                                kl.IS_CITO = detail.IsCito == false ? "0" : "1";



                                                            }

                                                        }
                                                        break;
                                                    default:
                                                        foreach (var detail in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                                        {
                                                            var kl = kls.AddNew();
                                                            kl.ModifiedDate = charges.ExecutionDate;
                                                            kl.NoPasien = patient.MedicalNo;
                                                            kl.KodeKunjungan = charges.TransactionNo;
                                                            kl.Nama = patient.PatientName + " " + apstd.ItemName;
                                                            kl.Email = patient.Email;
                                                            kl.DateOfBirth = patient.DateOfBirth;
                                                            kl.UmurTahun = reg.AgeInYear;
                                                            kl.UmurBulan = reg.AgeInMonth;
                                                            kl.UmurHari = reg.AgeInDay;
                                                            kl.Gender = patient.Sex == "M" ? "L" : "P";
                                                            kl.Alamat = patient.Address;
                                                            kl.Diagnosa = reg.Anamnesis;
                                                            kl.TglPeriksa = charges.ExecutionDate;
                                                            kl.Pengirim = reg.ParamedicID;

                                                            if (!string.IsNullOrEmpty(reg.PhysicianSenders)) kl.PengirimName = reg.PhysicianSenders;
                                                            else
                                                            {
                                                                var medic = new Paramedic();
                                                                medic.LoadByPrimaryKey(reg.ParamedicID);
                                                                kl.PengirimName = medic.ParamedicName;
                                                            }

                                                            var cls = new Class();
                                                            cls.LoadByPrimaryKey(charges.ClassID);
                                                            kl.Kelas = cls.ClassID;
                                                            kl.KelasName = cls.ClassID;

                                                            var room = new ServiceRoom();
                                                            room.LoadByPrimaryKey(charges.RoomID);
                                                            kl.Ruang = room.RoomID;
                                                            kl.RuangName = room.RoomName;

                                                            kl.CaraBayar = guar.GuarantorID;
                                                            kl.CaraBayarName = guar.GuarantorName;



                                                            kl.KodeTarif = detail.ItemID;
                                                            kl.Update = detail.IsCorrection == false ? "N" : "D";


                                                            kl.ISInap = reg.SRRegistrationType == "IPR" ? "1" : "0";
                                                            kl.Status = "1";

                                                        }


                                                        break;
                                                }

                                                kls.es.Connection.Name = AppConstant.HIS_INTEROP.ELIMS_LIS_INTEROP_CONNECTION_NAME;
                                                kls.Save();

                                            }
                                            break;
                                        #endregion
                                        #region WYNAKOM_LIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME:
                                            var orderDateTime = WynakomLisInteropUpdateMultipleConnection(charges, patient, reg, transChargesItems, unit, guar, paramedic, itemBridgings, group.ConnectionName);

                                            if (orderDateTime != null)
                                            {
                                                // Update info satusehat
                                                SatusehatServiceRequestPostAndLogToLis(reg, charges.TransactionNo, AppSession.Parameter.HisInteropConfigName);
                                            }
                                            break;
                                        #endregion
                                        #region ROCHE_LIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME:
                                            {
                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                {
                                                    var pa = new BusinessObject.Interop.ROCHE.PocAdt();
                                                    pa.es.Connection.Name = AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME;
                                                    pa.Action = "ADD";
                                                    pa.Pid = patient.MedicalNo;
                                                    pa.Dob = patient.DateOfBirth;
                                                    pa.Sex = patient.Sex;
                                                    pa.VisitNum = charges.TransactionNo;

                                                    var pc = string.Empty;
                                                    if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) pc = "1";
                                                    else if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient) pc = "2";
                                                    else if (reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) pc = "3";
                                                    else pc = "2";
                                                    pa.PatientClass = pc;

                                                    var fUnit = new ServiceUnit();
                                                    fUnit.LoadByPrimaryKey(charges.FromServiceUnitID);

                                                    pa.LocationCode = fUnit.ServiceUnitID;
                                                    pa.LocationName = fUnit.ServiceUnitName;
                                                    pa.DoctorCode = reg.ParamedicID;
                                                    pa.DoctorName = paramedic.ParamedicName;
                                                    pa.AdmitDt = charges.ExecutionDate;
                                                    pa.PatientName = patient.PatientName;

                                                    pa.Save();
                                                }
                                            }
                                            break;
                                        #endregion
                                        #region LINK_LIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME:
                                            var id_ruangan = new ServiceUnitBridging();
                                            id_ruangan.Query.Where(id_ruangan.Query.ServiceUnitID == reg.ServiceUnitID, id_ruangan.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                            if (!id_ruangan.Query.Load())
                                            {
                                                var ruangan = new ServiceUnit();
                                                ruangan.LoadByPrimaryKey(reg.ServiceUnitID);
                                                ShowInformationHeader("Service Unit : " + ruangan.ServiceUnitName + " not mapped to Link Lis.");
                                                return false;
                                            }

                                            var id_dokter = new ParamedicBridging();
                                            id_dokter.Query.Where(id_dokter.Query.ParamedicID == reg.ParamedicID, id_dokter.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                            if (!id_dokter.Query.Load())
                                            {
                                                var ruangan = new Paramedic();
                                                ruangan.LoadByPrimaryKey(reg.ParamedicID);
                                                ShowInformationHeader("Physician : " + ruangan.ParamedicName + " not mapped to Link Lis.");
                                                return false;
                                            }

                                            var id_status = new GuarantorBridging();
                                            id_status.Query.Where(id_status.Query.GuarantorID == reg.GuarantorID, id_status.Query.SRBridgingType == AppEnum.BridgingType.LINK_LIS.ToString());
                                            if (!id_status.Query.Load())
                                            {
                                                var ruangan = new Guarantor();
                                                ruangan.LoadByPrimaryKey(reg.GuarantorID);
                                                ShowInformationHeader("Guarantor : " + ruangan.GuarantorName + " not mapped to Link Lis.");
                                                return false;
                                            }

                                            if (itemBridgings.Any(t => t.ConnectionName == group.ConnectionName))
                                            {
                                                var svc = new Common.LinkLis.Service();
                                                var response = svc.InsertRegistrasiPasien(new Common.LinkLis.Object.RegistrasiPasien()
                                                {
                                                    no_rm = patient.MedicalNo,
                                                    nama = patient.PatientName,
                                                    alamat = patient.Address,
                                                    tgl_lahir = patient.DateOfBirth.Value.ToString("dd-MM-yyyy"),
                                                    jenis_kelamin = patient.Sex == "F" ? "Perempuan" : "Laki-laki",
                                                    status = id_status.BridgingID
                                                }, true);

                                                svc = new Common.LinkLis.Service();
                                                var kode = svc.GetKodePemeriksaan();

                                                svc = new Common.LinkLis.Service();
                                                response = svc.InsertRegistrasiPemeriksaan(new Common.LinkLis.Object.RegistrasiPemeriksaan()
                                                {
                                                    kode_pemeriksaan = kode.KodePemeriksaan,
                                                    no_rm = patient.MedicalNo,
                                                    id_ruangan = id_ruangan.BridgingID,
                                                    id_dokter = id_dokter.BridgingID.Split('-')[1],
                                                    id_analis = charges.AnalystID.Split('-')[1],
                                                    id_dokterpk = charges.LaboratoryParamedicID.Split('-')[1],
                                                    id_status = id_status.BridgingID
                                                }, true);

                                                var list_pemeriksaan = new List<Common.LinkLis.Object.ListPemeriksaan>();
                                                var list_parameter = new List<Common.LinkLis.Object.ListParameter>();
                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                {
                                                    foreach (var detail in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && t.ItemID == i.ItemID))
                                                    {
                                                        detail.ResultValue = kode.KodePemeriksaan;
                                                    }

                                                    if (!list_pemeriksaan.Any(p => p.list_pemeriksaan == i.BridgingGroupID)) list_pemeriksaan.Add(new Common.LinkLis.Object.ListPemeriksaan() { list_pemeriksaan = i.BridgingGroupID });
                                                    list_parameter.Add(new Common.LinkLis.Object.ListParameter() { list_pemeriksaan = i.BridgingGroupID, list_parameter = i.BridgingID });
                                                }

                                                svc = new Common.LinkLis.Service();
                                                response = svc.InsertRegistrasiParameterPemeriksaan(new Common.LinkLis.Object.ParameterPemeriksaan()
                                                {
                                                    kode_pemeriksaan = kode.KodePemeriksaan,
                                                    list_pemeriksaan = list_pemeriksaan,
                                                    list_parameter = list_parameter,
                                                    feedback_id = null,
                                                    feedback_url = null
                                                });

                                                transChargesItems.Save();
                                            }
                                            break;
                                        #endregion
                                        #region VANSLITE_LIS_INTEROP_CONNECTION_NAME
                                        case AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME:
                                            {
                                                var tunit = new ServiceUnit();
                                                tunit.LoadByPrimaryKey(charges.ToServiceUnitID);

                                                var vlites = new BusinessObject.Interop.VANSLITE.OrderLabCollection();

                                                foreach (var i in itemBridgings.Where(i => i.ConnectionName == group.ConnectionName))
                                                {
                                                    var seqNo = string.Empty;
                                                    var parId = string.Empty;
                                                    decimal price = 0;
                                                    decimal qty = 0;
                                                    bool isCito = false;
                                                    foreach (var detail in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && t.ItemID == i.ItemID))
                                                    {
                                                        seqNo = detail.SequenceNo;
                                                        price = detail.Price ?? 0;
                                                        qty = detail.ChargeQuantity ?? 0;
                                                        isCito = detail.IsCito ?? false;
                                                        parId = string.IsNullOrEmpty(detail.ParamedicID) ? string.Empty : detail.ParamedicID;

                                                        detail.IsSendToLIS = true;

                                                        break;
                                                    }

                                                    var vlite = vlites.AddNew();
                                                    vlite.AsalLab = tunit.ServiceUnitName;
                                                    vlite.NoLab = charges.TransactionNo;
                                                    vlite.NoLabDtl = seqNo;
                                                    vlite.NoRegistrasi = reg.RegistrationNo;
                                                    vlite.NoRm = patient.MedicalNo;
                                                    vlite.TglOrder = Convert.ToDateTime(charges.ExecutionDate.Value.ToString("MM/dd/yyyy"));
                                                    vlite.NamaPas = patient.PatientName;
                                                    vlite.JenisKel = patient.Sex == "M" ? "L" : "P";
                                                    vlite.TglLahir = patient.DateOfBirth;
                                                    vlite.Usia = Helper.GetAgeInYear(patient.DateOfBirth.Value, charges.ExecutionDate.Value).ToString();
                                                    vlite.Alamat = patient.Address;
                                                    vlite.KodeDokKirim = reg.ParamedicID;
                                                    vlite.NamaDokKirim = paramedic.ParamedicName;
                                                    vlite.KodeRuang = charges.FromServiceUnitID;
                                                    vlite.NamaRuang = unit.ServiceUnitName;
                                                    vlite.KodeCaraBayar = reg.GuarantorID;
                                                    vlite.CaraBayar = guar.GuarantorName;
                                                    vlite.KetKlinis = reg.Complaint;
                                                    vlite.KodeTest = i.ItemID;

                                                    var item = new Item();
                                                    item.LoadByPrimaryKey(i.ItemID);
                                                    vlite.Test = item.ItemName;
                                                    vlite.Harga = price.ToInt();
                                                    vlite.WaktuKirim = Convert.ToDateTime(charges.ExecutionDate.Value.ToString("HH:mm:ss"));
                                                    vlite.Prioritas = isCito ? "C" : "R";
                                                    vlite.JnsRawat = reg.SRRegistrationType == "IPR" ? "Rawat Inap" : "Rawat Jalan";
                                                    if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                                                    {
                                                        vlite.DokJaga = cboParamedicID.Text;
                                                    }
                                                    else
                                                    {
                                                        if (string.IsNullOrWhiteSpace(parId)) vlite.DokJaga = "";
                                                        else
                                                        {
                                                            var tmedic = new Paramedic();
                                                            tmedic.LoadByPrimaryKey(parId);
                                                            vlite.DokJaga = tmedic.ParamedicName;
                                                        }
                                                    }
                                                    vlite.Status = 0;
                                                    vlite.Batal = 0;
                                                    vlite.JumlahTest = qty.ToInt();
                                                }

                                                transChargesItems.Save();

                                                vlites.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLITE_LIS_INTEROP_CONNECTION_NAME;
                                                vlites.Save();
                                            }
                                            break;
                                            #endregion
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion

                #region Interop RIS/PACS
                //interop radiologi
                if (AppSession.Parameter.IsUsingRisPacsInterop)
                {
                    if (charges.ToServiceUnitID == _serviceUnitRadiologyID || charges.ToServiceUnitID == _serviceUnitRadiologyID2)
                    {
                        var patient = new Patient();
                        patient.LoadByPrimaryKey(reg.PatientID);

                        switch (AppSession.Parameter.HealthcareInitialAppsVersion)
                        {
                            case "GRHA":
                                if (!HttpContext.Current.IsDebuggingEnabled)
                                {
                                    var svc = new Common.Worklist.MM2100.Service();
                                    svc.NewWorklist(Request.QueryString["joNo"], transChargesItems);
                                }
                                break;
                            case "YBRSGKP":
                                {
                                    if (AppSession.Parameter.HealthcareInitial == "RSI")
                                    {
                                        if (!HttpContext.Current.IsDebuggingEnabled)
                                        {
                                            foreach (var entity in transChargesItems.Where(t => t.IsOrderRealization ?? false))
                                            {
                                                var root = new Common.Worklist.RSI.Json.Order.Root();

                                                var orderno = string.Empty;
                                                foreach (var c in entity.TransactionNo.ToCharArray())
                                                {
                                                    if (!int.TryParse(c.ToString(), out int number)) continue;
                                                    if (number == 0) continue;
                                                    orderno += number.ToString();
                                                }

                                                root.acc = orderno;

                                                orderno += entity.SequenceNo.ToInt().ToString();
                                                foreach (var c in entity.ItemID.ToCharArray())
                                                {
                                                    if (!int.TryParse(c.ToString(), out int number)) continue;
                                                    if (number == 0) continue;
                                                    orderno += number.ToString();
                                                }

                                                var irad = new Item();
                                                irad.LoadByPrimaryKey(entity.ItemID);
                                                if (string.IsNullOrEmpty(irad.ItemIDExternal)) continue;

                                                root.uid = string.Format("{0}.{1}.{2}{3}.{4}", "1.2.40.0.13.1", patient.MedicalNo.ToInt().ToString(), charges.ExecutionDate.Value.ToString("yyMd"), charges.ExecutionDate.Value.ToString("Hms"), orderno);
                                                //if (string.IsNullOrEmpty(entity.FilmNo))
                                                //{
                                                //    _filmNo = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.RadiologyFilmNo, irad.ItemIDExternal, string.Empty, AppSession.UserLogin.UserID);
                                                //    var filmNo = _filmNo.LastCompleteNumber;
                                                //    root.patientid = filmNo;
                                                //    _filmNo.Save();
                                                //}
                                                //else 
                                                root.patientid = entity.FilmNo;
                                                root.mrn = patient.MedicalNo;
                                                root.name = patient.PatientName;
                                                root.address = patient.Address;
                                                root.sex = patient.Sex;
                                                root.birth_date = patient.DateOfBirth.Value.ToString("yyyyMMdd");
                                                root.weight = "null";

                                                var unitf = new ServiceUnit();
                                                unitf.LoadByPrimaryKey(charges.FromServiceUnitID);
                                                root.name_dep = unitf.ServiceUnitName;

                                                root.xray_type_code = irad.ItemIDExternal;
                                                root.typename = string.Empty;
                                                root.prosedur = irad.ItemName;

                                                var drp = new Paramedic();
                                                drp.LoadByPrimaryKey(reg.ParamedicID);
                                                root.dokterid = reg.ParamedicID;
                                                root.named = drp.ParamedicName;

                                                var drr = new Paramedic();
                                                drr.LoadByPrimaryKey(entity.ParamedicID);
                                                root.dokradid = entity.ParamedicID;
                                                root.dokrad_name = drr.ParamedicName;
                                                root.create_time = string.Format("{0}{1}00", charges.ExecutionDate.Value.ToString("yyyyMMdd"), charges.ExecutionDate.Value.ToString("HH:mm:ss").Replace(":", string.Empty));

                                                var date = charges.ExecutionDate.Value.AddMinutes(10);
                                                root.schedule_date = date.ToString("yyyyMMdd");
                                                root.schedule_time = date.ToString("HH:mm:ss").Replace(":", string.Empty);

                                                root.priority = (entity.IsCito ?? false) ? "CITO" : "NORMAL";
                                                root.pat_state = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "RAWAT INAP" :
                                                    reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient ? "RAWAT JALAN" : "IGD";

                                                var diag = new EpisodeDiagnose();
                                                diag.Query.es.Top = 1;
                                                diag.Query.Where(diag.Query.RegistrationNo == charges.RegistrationNo, diag.Query.DiagnosisText != string.Empty);
                                                diag.Query.OrderBy(diag.Query.LastUpdateDateTime.Descending);
                                                if (diag.Query.Load()) root.spc_needs = diag.DiagnosisText;
                                                else root.spc_needs = string.Empty;

                                                root.payment = reg.GuarantorID == AppParameter.GetParameterValue(AppParameter.ParameterItem.SelfGuarantorID) ? "TUNAI" : "ASURANSI/PERUSAHAAN";
                                                root.arrive_date = charges.ExecutionDate.Value.ToString("yyyyMMdd");
                                                root.arrive_time = charges.ExecutionDate.Value.ToString("HH:mm:ss").Replace(":", string.Empty);

                                                var service = new Common.Worklist.RSI.Service();
                                                var response = service.CreateJsonOrder(root);
                                                if (!response.data.status)
                                                {
                                                    ShowInformationHeader(response.data.hasil);
                                                    return false;
                                                }

                                                //xml
                                                service = new Common.Worklist.RSI.Service();
                                                var sps = service.GetSpsLastCode();

                                                var ds = new Common.Worklist.RSI.Xml.dataset();

                                                Common.Worklist.RSI.Xml.datasetAttr[] datasetAttr_array = new Common.Worklist.RSI.Xml.datasetAttr[24];

                                                ///
                                                var datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00080005",
                                                    Text = "ISO_IR 192"
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 0);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00400100"
                                                };

                                                var datasetAttrAttr_array = new Common.Worklist.RSI.Xml.datasetAttrAttr[12];

                                                var datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400001",
                                                    Text = "DCMPACS"
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 0);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400002",
                                                    Text = root.schedule_date
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 1);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400003",
                                                    Text = root.schedule_time
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 2);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00080060",
                                                    Text = root.xray_type_code
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 3);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400006",
                                                    Text = "NULL"
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 4);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400007",
                                                    Text = root.prosedur
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 5);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400011",
                                                    Text = "Scheduled Procedure Step Location"
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 6);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400008"
                                                };

                                                var datasetAttrAttrAttr_array = new Common.Worklist.RSI.Xml.datasetAttrAttrAttr[3];

                                                var datasetAttrAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttrAttr
                                                {
                                                    tag = "00080100",
                                                    Value = "PROT-1205"
                                                };
                                                datasetAttrAttrAttr_array.SetValue(datasetAttrAttrAttr, 0);

                                                datasetAttrAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttrAttr
                                                {
                                                    tag = "00080102",
                                                    Value = "DCM"
                                                };
                                                datasetAttrAttrAttr_array.SetValue(datasetAttrAttrAttr, 1);

                                                datasetAttrAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttrAttr
                                                {
                                                    tag = "00080104",
                                                    Value = "NA"
                                                };
                                                datasetAttrAttrAttr_array.SetValue(datasetAttrAttrAttr, 2);

                                                datasetAttrAttr.item = datasetAttrAttrAttr_array;

                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 7);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400012",
                                                    Text = "Pre-Medication"
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 8);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400009",
                                                    Text = sps[0]
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 9);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00321070",
                                                    Text = "NULL"
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 10);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00400020",
                                                    Text = "SCHEDULED"
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 11);

                                                datasetAttr.item = datasetAttrAttr_array;
                                                datasetAttr_array.SetValue(datasetAttr, 1);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00401001",
                                                    Text = sps[1]
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 2);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00321060",
                                                    Text = irad.ItemName
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 3);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00321064"
                                                };

                                                datasetAttrAttr_array = new Common.Worklist.RSI.Xml.datasetAttrAttr[3];

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00080100",
                                                    Text = "PROC-1205"
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 0);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00080102",
                                                    Text = "DCM"
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 1);

                                                datasetAttrAttr = new Common.Worklist.RSI.Xml.datasetAttrAttr
                                                {
                                                    tag = "00080104",
                                                    Text = irad.ItemName
                                                };
                                                datasetAttrAttr_array.SetValue(datasetAttrAttr, 2);

                                                datasetAttr.item = datasetAttrAttr_array;
                                                datasetAttr_array.SetValue(datasetAttr, 4);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "0020000D",
                                                    Text = root.uid
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 5);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00401003",
                                                    Text = root.priority
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 6);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00080050",
                                                    Text = root.acc
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 7);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00321032",
                                                    Text = root.dokrad_name
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 8);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00321033",
                                                    Text = root.name_dep
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 9);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00080090",
                                                    Text = root.named
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 10);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00380010",
                                                    Text = "ADM-1234"
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 11);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00380300",
                                                    Text = root.name_dep
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 12);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00100010",
                                                    Text = root.name
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 13);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00100020",
                                                    Text = root.patientid
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 14);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00100030",
                                                    Text = root.birth_date
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 15);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00100040",
                                                    Text = root.sex
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 16);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00101030",
                                                    Text = "NULL"
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 17);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00403001",
                                                    Text = "V"
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 18);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00380500",
                                                    Text = root.pat_state
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 19);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "001021C0",
                                                    Text = "000"
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 20);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00102000",
                                                    Text = "Medical Alerts"
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 21);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00102110",
                                                    Text = "NULL"
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 22);

                                                ///
                                                datasetAttr = new Common.Worklist.RSI.Xml.datasetAttr
                                                {
                                                    tag = "00380050"
                                                };
                                                datasetAttr_array.SetValue(datasetAttr, 23);

                                                ds.attr = datasetAttr_array;

                                                service = new Common.Worklist.RSI.Service();
                                                service.CreateXmlWorklist(ds);

                                                entity.ResultValue = root.uid;
                                            }
                                        }
                                    }
                                    else if (AppSession.Parameter.HealthcareInitial == "RSBK")
                                    {
                                        var pref = new Paramedic();
                                        pref.LoadByPrimaryKey(reg.ParamedicID);

                                        var uref = new ServiceUnit();
                                        uref.LoadByPrimaryKey(charges.FromServiceUnitID);

                                        var epsdiag = new EpisodeDiagnose();
                                        epsdiag.Query.es.Top = 1;
                                        epsdiag.Query.Where(epsdiag.Query.RegistrationNo == reg.RegistrationNo, epsdiag.Query.SRDiagnoseType.In("DiagnoseType-001", "DiagnoseType-006"), epsdiag.Query.IsVoid == false);
                                        epsdiag.Query.OrderBy(epsdiag.Query.CreateDateTime.Descending);
                                        var isEpsDiag = epsdiag.Query.Load();

                                        string diagId = string.Empty;
                                        string diagnoseName = string.Empty;
                                        string patasdiagnose = string.Empty;

                                        var patas = new PatientAssessment();
                                        patas.Query.es.Top = 1;
                                        patas.Query.Where(patas.Query.RegistrationNo == reg.RegistrationNo);
                                        patas.Query.OrderBy(patas.Query.CreatedDateTime.Descending);
                                        var patasdiag = patas.Query.Load();

                                        diagId = string.IsNullOrWhiteSpace(epsdiag.DiagnoseID) ? string.Empty : $"({epsdiag.DiagnoseID}) ";
                                        diagnoseName = epsdiag.DiagnosisText ?? string.Empty;
                                        patasdiagnose = patas.Diagnose ?? string.Empty;

                                        if (transChargesItems.Any(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                        {
                                            var list = transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)).Select(t =>
                                            {
                                                var it = new Item();
                                                it.LoadByPrimaryKey(t.ItemID);

                                                if (it.IsHasTestResults == false)
                                                {
                                                    return null;
                                                }

                                                var itg = new ItemGroup();
                                                itg.LoadByPrimaryKey(it.ItemGroupID);

                                                var refdoc = charges.PhysicianSenders ?? string.Empty;

                                                var tcic = new TransChargesItemComp();
                                                tcic.LoadByPrimaryKey(t.TransactionNo, t.SequenceNo, "05");

                                                var opername = string.Empty;
                                                opername = tcic.ParamedicID ?? string.Empty;

                                                var sero = new ServiceRoom();
                                                sero.LoadByPrimaryKey(reg.RoomID);

                                                var seru = new ServiceUnit();
                                                seru.LoadByPrimaryKey(charges.FromServiceUnitID);

                                                var sal = new AppStandardReferenceItem();
                                                sal.LoadByPrimaryKey("Salutation", patient.SRSalutation);

                                                return new Common.Worklist.RSBK.DataExamOrder()
                                                {
                                                    patient_id = patient.MedicalNo,
                                                    patient_name = $"{sal.ItemName} {patient.FirstName} {patient.MiddleName} {patient.LastName}",
                                                    patient_sex = patient.Sex == "M" ? "M" : (patient.Sex == "F" ? "F" : (patient.Sex == "O" ? "O" : "U")),
                                                    patient_birthday = patient.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                                                    patient_weight = string.Empty,
                                                    patient_class = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "I" : reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient ? "O" : "E",
                                                    ward = sero.RoomName,
                                                    attending_doctor = t.ParamedicCollectionName,
                                                    referring_doctor = refdoc,
                                                    order_control = "NW",
                                                    order_department = reg.DepartmentID,
                                                    accession_number = $"{t.TransactionNo}" + $"{t.SequenceNo.Substring(t.SequenceNo.Length - 2)}", //co:JO240424-00003 + 001 > JO240424-00003 + 01 > JO240424-0000301
                                                    study_code = t.ItemID,
                                                    study_name = GetItemName(t.ItemID),
                                                    order_datetime = charges.TransactionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                                                    scheduled_datetime = charges.ExecutionDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                                                    clinic_comments = $"{diagId}, {diagnoseName} ~~ {charges.Notes} ~~ {patasdiagnose}",
                                                    sickness_name = t.Notes,
                                                    reason_for_study = string.Empty,
                                                    body_part = string.Empty,
                                                    ordering_doctor = t.ParamedicCollectionName,
                                                    exam_room = seru.ServiceUnitName,
                                                    modality = itg.Initial.Substring(itg.Initial.Length - 2),
                                                    operator_name = Paramedic.GetParamedicName(opername),
                                                    exam_urgent = t.IsCito.HasValue ? (t.IsCito.Value ? "1" : "0") : "0",
                                                    issuer = "H",
                                                    if_flag = 0,
                                                    result = 0,
                                                    urllink = string.Empty
                                                };
                                            })
                                            .Where(order => order != null)
                                            .ToList();

                                            var ris = new Common.Worklist.RSBK.Service();
                                            if (!ris.InsertExamOrder(list))
                                            {
                                                ShowInformationHeader($"Send order failed, please try again.");
                                                return false;
                                            }
                                            foreach (var tci in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                            {
                                                tci.IsSendToLIS = true;
                                            }
                                        }
                                    }
                                    transChargesItems.Save();
                                }
                                break;
                            case "RSTJ":
                                //if (!HttpContext.Current.IsDebuggingEnabled)
                                {
                                    var funit = new ServiceUnit();
                                    funit.LoadByPrimaryKey(charges.FromServiceUnitID);

                                    var pmedic = new Paramedic();
                                    pmedic.LoadByPrimaryKey(reg.ParamedicID);

                                    foreach (var detail in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                    {
                                        var item = new Item();
                                        item.LoadByPrimaryKey(detail.ItemID);

                                        var tmedic = new Paramedic();
                                        //if (!tmedic.LoadByPrimaryKey(detail.ParamedicID))
                                        {
                                            var comps = Session["collTransChargesItemComp" + Request.UserHostName] as TransChargesItemCompCollection;
                                            var comp = comps.Where(c => c.TransactionNo == detail.TransactionNo && c.SequenceNo == detail.SequenceNo && c.TariffComponentID == "01" && !string.IsNullOrEmpty(c.ParamedicID)).SingleOrDefault();
                                            if (comp != null)
                                            {
                                                //tmedic = new Paramedic();
                                                tmedic.LoadByPrimaryKey(comp.ParamedicID);
                                            }
                                        }

                                        var svc = new Common.Worklist.RSTJ.Service();
                                        var request = new Common.Worklist.RSTJ.Json.Request.Root()
                                        {
                                            Norm = patient.MedicalNo,
                                            Nama = patient.PatientName,
                                            Alamat = patient.Address,
                                            Kota = patient.City,
                                            Tgllahir = patient.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                                            Nohap = patient.MobilePhoneNo,
                                            Kelamin = patient.Sex == "M" ? "L" : "P",
                                            Drpeminta = pmedic.ParamedicName,
                                            Asalpasien = funit.ServiceUnitName,
                                            Layanan = item.ItemName,
                                            Notagihan = "",
                                            Statusbayar = "",
                                            NoTransaksi = detail.TransactionNo + "-" + detail.SequenceNo,
                                            Asuransi = guar.GuarantorName,
                                            DokterRadiologi = tmedic.ParamedicName,
                                            DokterId = tmedic.ParamedicID
                                        };
                                        var response = svc.SendJsonOrder(request);

                                        if (!string.IsNullOrEmpty(response)) detail.IsSendToLIS = true;

                                        var log = new WebServiceAPILog()
                                        {
                                            DateRequest = DateTime.Now,
                                            IPAddress = "10.10.10.38",
                                            UrlAddress = "http://10.10.10.38/reqpasien",
                                            Params = Newtonsoft.Json.JsonConvert.SerializeObject(request),
                                            Response = response,
                                            Totalms = 0
                                        };
                                        log.Save();
                                    }

                                    transChargesItems.Save();
                                }
                                break;
                            case "RSMMP":
                                {
                                    var pref = new Paramedic();
                                    pref.LoadByPrimaryKey(reg.ParamedicID);

                                    var uref = new ServiceUnit();
                                    uref.LoadByPrimaryKey(charges.FromServiceUnitID);

                                    var diag = new EpisodeDiagnose();
                                    diag.Query.es.Top = 1;
                                    diag.Query.Where(diag.Query.RegistrationNo == reg.RegistrationNo, diag.Query.SRDiagnoseType.In(AppSession.Parameter.DiagnoseTypeMain), diag.Query.IsVoid == false);
                                    diag.Query.OrderBy(diag.Query.LastUpdateDateTime.Descending);
                                    var isDiag = diag.Query.Load();

                                    var diagId = !isDiag ? string.Empty : $"({diag.DiagnoseID}) ";

                                    var tc = new TariffComponentCollection();
                                    tc.Query.Where(tc.Query.IsTariffParamedic == true);
                                    tc.Query.Load();

                                    var tcics = collComp.Where(t => t.TransactionNo == charges.TransactionNo && tc.Select(c => c.TariffComponentID).Contains(t.TariffComponentID) && !string.IsNullOrWhiteSpace(t.ParamedicID))
                                        .Select(t => t.SequenceNo)
                                        .Distinct();

                                    if (tcics.Any())
                                    {
                                        if (transChargesItems.Any(t => tcics.Contains(t.SequenceNo) && (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                        {
                                            var root = new Common.Worklist.RSMMP.Json.Order.Request.Root
                                            {
                                                Patient = new Common.Worklist.RSMMP.Json.Order.Request.Patient()
                                                {
                                                    Address = patient.Address,
                                                    Alerts = string.Empty,
                                                    Allergies = string.Empty,
                                                    BirthDate = patient.DateOfBirth.Value.ToString("yyyyMMdd"),
                                                    BirthPlace = patient.CityOfBirth,
                                                    Diagnosis = isDiag ? $"{diagId}{diag.DiagnosisText}" : string.Empty,
                                                    Email = patient.Email,
                                                    FirstName = patient.FirstName,
                                                    Gender = patient.Sex,
                                                    Height = 0,
                                                    LastName = patient.LastName,
                                                    MedicalNo = patient.MedicalNo,
                                                    MiddleName = patient.MiddleName,
                                                    Phone = patient.MobilePhoneNo,
                                                    Pregnancy = null,
                                                    Ssn = patient.Ssn,
                                                    Weight = 0
                                                },
                                                Exams = transChargesItems.Where(t => tcics.Contains(t.SequenceNo) && (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false))
                                                                     .Select(tci => new Common.Worklist.RSMMP.Json.Order.Request.Exam()
                                                                     {
                                                                         Datetime = tci.RealizationDateTime.Value.ToString("yyyyMMdd HHmmss"),
                                                                         HisRefId = $"{tci.TransactionNo}#{tci.SequenceNo}",
                                                                         Payor = new Common.Worklist.RSMMP.Json.Order.Request.Payor()
                                                                         {
                                                                             Code = guar.GuarantorID,
                                                                             Name = guar.GuarantorName,
                                                                         },
                                                                         ParamedicPic = new Common.Worklist.RSMMP.Json.Order.Request.ParamedicPic()
                                                                         {
                                                                             Code = !string.IsNullOrWhiteSpace(cboParamedicID.SelectedValue) ? cboParamedicID.SelectedValue : tci.ParamedicID
                                                                         },
                                                                         ParamedicRef = new Common.Worklist.RSMMP.Json.Order.Request.ParamedicRef()
                                                                         {
                                                                             Code = reg.ParamedicID,
                                                                             Name = pref.ParamedicName
                                                                         },
                                                                         Study = new Common.Worklist.RSMMP.Json.Order.Request.Study()
                                                                         {
                                                                             Code = tci.ItemID
                                                                         },
                                                                         CitoStat = tci.IsCito ?? false,
                                                                         UnitPic = new Common.Worklist.RSMMP.Json.Order.Request.UnitPic()
                                                                         {
                                                                             Code = charges.ToServiceUnitID
                                                                         },
                                                                         UnitRef = new Common.Worklist.RSMMP.Json.Order.Request.UnitRef()
                                                                         {
                                                                             Code = charges.FromServiceUnitID,
                                                                             Name = uref.ServiceUnitName
                                                                         },
                                                                         UserId = tci.RealizationUserID
                                                                     }).ToList()
                                            };

                                            var svc = new Common.Worklist.RSMMP.Service();
                                            var response = svc.SendOrder(root);
                                            if (response.Metadata.Code == 200)
                                            {
                                                foreach (var tci in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                                {
                                                    var data = response.Exams.SingleOrDefault(r => r.HisRefId == $"{tci.TransactionNo}#{tci.SequenceNo}");
                                                    if (data == null) continue;
                                                    tci.IsSendToLIS = true;
                                                    tci.ResultValue = data.PacsStudyUid;
                                                    tci.FilmNo = data.FilmNo;
                                                }
                                                transChargesItems.Save();
                                            }
                                            else
                                            {
                                                ShowInformationHeader($"Send order failed, please try again. {response.Metadata.Message}");
                                                return false;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "RSCDR":
                                {
                                    var pref = new Paramedic();
                                    pref.LoadByPrimaryKey(reg.ParamedicID);

                                    var uref = new ServiceUnit();
                                    uref.LoadByPrimaryKey(charges.FromServiceUnitID);

                                    foreach (var detail in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                    {
                                        var item = new Item();
                                        item.LoadByPrimaryKey(detail.ItemID);

                                        var root = new Common.Worklist.RSCDR.Json.Order.Root()
                                        {
                                            Order = new Common.Worklist.RSCDR.Json.Order.JOrder()
                                            {
                                                Order2 = new Common.Worklist.RSCDR.Json.Order.JOrder2()
                                                {
                                                    Id = $"{detail.TransactionNo}-{detail.SequenceNo.ToInt()}",
                                                    ServiceCode = detail.ItemID,
                                                    ServiceName = item.ItemName,
                                                    Status = "NEW",
                                                    OrderDate = charges.ExecutionDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                                                    Doctor = pref.ParamedicName,
                                                    Modality = "CT",
                                                    ClinicalDiagnosis = string.Empty
                                                },
                                                Patient = new Common.Worklist.RSCDR.Json.Order.Patient()
                                                {
                                                    Id = patient.MedicalNo,
                                                    FirstName = string.Empty,
                                                    MiddleName = string.Empty,
                                                    LastName = patient.PatientName,
                                                    Sex = patient.Sex,
                                                    BirthDate = patient.DateOfBirth?.ToString("yyyy-MM-dd"),
                                                    Phone = patient.MobilePhoneNo,
                                                    Address = patient.Address,
                                                    Height = string.Empty,
                                                    Weight = string.Empty,
                                                    Priority = string.Empty,
                                                    Department = uref.ServiceUnitName
                                                }
                                            }
                                        };

                                        detail.IsSendToLIS = true;

                                        var svc = new Common.Worklist.RSCDR.Service();
                                        var response = svc.PostOrder(root);

                                        var log = new WebServiceAPILog()
                                        {
                                            DateRequest = DateTime.Now,
                                            IPAddress = "10.10.10.38",
                                            UrlAddress = "http://121.121.121.4:10110/pacs/putOrder/",
                                            Params = Newtonsoft.Json.JsonConvert.SerializeObject(root),
                                            Response = response,
                                            Totalms = 0
                                        };
                                        log.Save();
                                    }

                                    transChargesItems.Save();
                                }
                                break;
                            case "RSUTS":
                                {
                                    var pref = new Paramedic();
                                    pref.LoadByPrimaryKey(reg.ParamedicID);

                                    var uref = new ServiceUnit();
                                    uref.LoadByPrimaryKey(charges.FromServiceUnitID);

                                    var diag = new RegistrationInfoMedicDiagnose();
                                    diag.Query.es.Top = 1;
                                    diag.Query.Where(diag.Query.RegistrationNo == reg.RegistrationNo, diag.Query.SRDiagnoseType.In("DiagnoseType-001", "DiagnoseType-006"), diag.Query.IsVoid == false);
                                    diag.Query.OrderBy(diag.Query.DiagnoseDateTime.Descending);
                                    var isDiag = diag.Query.Load();

                                    var diagId = string.IsNullOrWhiteSpace(diag.DiagnoseID) ? string.Empty : $"({diag.DiagnoseID}) ";

                                    if (transChargesItems.Any(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                    {
                                        var list = transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)).Select(t => new Common.Worklist.RSTS.Data()
                                        {
                                            ordercode = $"{t.TransactionNo}_{t.SequenceNo}",
                                            trancode = $"{t.TransactionNo}_{t.SequenceNo}",
                                            patientid = patient.MedicalNo,
                                            patientname = patient.PatientName,
                                            patientbirth = patient.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                                            orderdate = charges.ExecutionDate.Value.ToString("yyyy-MM-dd"),
                                            ordertime = charges.ExecutionDate.Value.ToString("HH:mm:ss"),
                                            doctorid = reg.ParamedicID,
                                            doctorname = pref.ParamedicName,
                                            iostatus = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "I" : "O",
                                            examinationid = t.ItemID,
                                            examinationname = GetItemName(t.ItemID),
                                            clinicdiag = isDiag ? $"{diagId}, {diag.DiagnoseName}" : string.Empty,
                                            additional = string.Empty,
                                            flag = string.Empty,
                                            orderstatus = "1", // order
                                            unitcode = charges.FromServiceUnitID,
                                            unitname = uref.ServiceUnitName,
                                            patientsex = patient.Sex == "M" ? "L" : "P",
                                            patientaddress = patient.Address,
                                            no_hp = patient.MobilePhoneNo,
                                            branch = "RSUD Tamansari Jakarta"

                                        }).ToList();
                                        var ris = new Common.Worklist.RSTS.Service();
                                        if (!ris.Insert(list))
                                        {
                                            ShowInformationHeader($"Send order failed, please try again.");
                                            return false;
                                        }
                                        foreach (var tci in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                                        {
                                            tci.IsSendToLIS = true;
                                        }
                                    }
                                    transChargesItems.Save();
                                }
                                break;
                        }
                    }
                }
                #endregion

                if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment)
                {
                    int? x = ParamedicFeeTransChargesItemCompSettled.UpdateSettled(charges, collComp, AppSession.UserLogin.UserID);
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        private static bool SysmexLisInteropUpdateSingleConnection(TransCharges charges, ServiceUnit unit, Registration reg, Guarantor guar, TransChargesItemCollection transChargesItems, Patient patient, string salutation)
        {
            var lo = new BusinessObject.Interop.SYSMEX.LisOrder();
            if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.es.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
            else if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.MEDICLAB_LIS_INTEROP_CONNECTION_NAME) lo.es.Connection.Name = AppConstant.HIS_INTEROP.MEDICLAB_LIS_INTEROP_CONNECTION_NAME;
            else
                lo.es.Connection.Name = AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME;

            lo.MessageDt = (charges.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
            lo.OrderControl = "NW";
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "GRHA") lo.Pid = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
            else lo.Pid = patient.MedicalNo;
            lo.Pname = patient.PatientName;
            lo.Address1 = patient.StreetName;

            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            {
                case "RSTJ":
                    lo.Pname = (patient.PatientName + " " + salutation).Trim();
                    lo.Address2 = patient.District;
                    lo.Address3 = patient.County;
                    lo.Address4 = patient.State;
                    break;
                case "RSUTAMA":
                case "KLUTAMA":
                    lo.Address2 = patient.District;
                    lo.Address3 = patient.County + " " + patient.State;
                    lo.Address4 = patient.MobilePhoneNo;
                    break;

                case "RSMP":
                case "GRHA":
                    if (!string.IsNullOrEmpty(reg.ReferralID))
                    {
                        var refral = new Referral();
                        refral.LoadByPrimaryKey(reg.ReferralID);

                        lo.Address3 = "";
                    }

                    lo.Address2 = patient.District;
                    lo.Address3 = "";
                    lo.Address4 = guar.GuarantorName;
                    break;
                case "RSSMCB":
                    lo.Address2 = guar.GuarantorName;
                    lo.Address3 = patient.District + " " + patient.County;
                    if (AppSession.Parameter.IsUsingHisInteropToHcLab)
                    {
                        lo.Address4 = patient.MobilePhoneNo;
                    }
                    else
                    {
                        if (AppSession.Parameter.HealthcareInitial == "RSSMHB")
                        {
                            lo.Address3 += " " + patient.State;

                            var mb = new MergeBilling();
                            if (mb.LoadByPrimaryKey(reg.RegistrationNo))
                            {
                                if (!string.IsNullOrEmpty(mb.FromRegistrationNo))
                                {
                                    var freg = new Registration();
                                    freg.LoadByPrimaryKey(mb.FromRegistrationNo);
                                    var funit = new ServiceUnit();
                                    funit.LoadByPrimaryKey(freg.ServiceUnitID);
                                    lo.Address4 = funit.ServiceUnitID + "^" + funit.ServiceUnitName;
                                }
                                else lo.Address4 = unit.ServiceUnitID + "^" + unit.ServiceUnitName; ;
                            }
                        }
                        else
                        {
                            lo.Address4 = patient.State;
                        }
                    }

                    break;
                case "YBRSGKP":
                    lo.Address2 = "";
                    switch (guar.SRGuarantorType)
                    {
                        case "09":
                            lo.Address2 = "BPJS";
                            break;
                        case "00":
                            lo.Address2 = "PRIBADI";
                            break;
                        default:
                            lo.Address2 = "MITRA";
                            break;

                    }

                    lo.Address3 = guar.GuarantorName;
                    lo.Address4 = charges.ClassID;

                    var cls = new Class();
                    cls.LoadByPrimaryKey(charges.ClassID);
                    lo.Address4 = reg.SRRegistrationType == "IPR" ? cls.ClassName : string.Empty;
                    break;
                case "RSGPI":

                    var cl = new Class();
                    cl.LoadByPrimaryKey(charges.ClassID);
                    lo.Address2 = patient.District;
                    lo.Address3 = guar.GuarantorName;
                    lo.Address4 = cl.ClassName;
                    break;

                default:
                    lo.Pname = patient.PatientName;
                    lo.Address2 = patient.District;
                    lo.Address3 = patient.County;
                    lo.Address4 = patient.State;
                    break;
            }

            lo.Ptype = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? "IN" : "OP";
            lo.BirthDt = (patient.DateOfBirth ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
            lo.Sex = patient.Sex == "M" ? "1" : "0";
            lo.Ono = charges.TransactionNo;
            lo.RequestDt = (charges.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");

            //switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            //{
            //    case "RSTJ":
            //        lo.RequestDt = (charges.CreatedDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
            //        break;
            //    default:
            //        lo.RequestDt = (charges.LastUpdateDateTime ?? (new DateTime()).NowAtSqlServer()).ToString("yyyyMMddHHmmss");
            //        break;
            //}

            unit = new ServiceUnit();
            unit.LoadByPrimaryKey(charges.FromServiceUnitID);

            lo.Source = unit.ServiceUnitID + "^" + unit.ServiceUnitName;

            var param = new Paramedic();

            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            {
                case "RSUTAMA":
                    if (!string.IsNullOrEmpty(reg.ReferralID))
                    {
                        var refer = new Referral();
                        refer.LoadByPrimaryKey(reg.ReferralID);

                        lo.Clinician = reg.ReferralID + "^" + refer.ReferralName;
                    }
                    else if (!string.IsNullOrEmpty(reg.PhysicianSenders))
                    {
                        lo.Clinician = reg.ParamedicID + "^" + reg.PhysicianSenders;
                    }
                    else
                    {
                        param.LoadByPrimaryKey(reg.ParamedicID);

                        lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
                    }
                    break;
                default:
                    param.LoadByPrimaryKey(reg.ParamedicID);

                    lo.Clinician = reg.ParamedicID + "^" + param.ParamedicName;
                    break;
            }

            lo.RoomNo = reg.RoomID;
            lo.Priority = transChargesItems.Any(t => (t.IsCito ?? false)) ? "U" : "R";
            if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.Cmt = guar.GuarantorName;
            else
            {
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSMCB" && AppSession.Parameter.IsUsingHisInteropToHcLab)
                    lo.Cmt = patient.Ssn;
                else
                    lo.Cmt = charges.Notes; //string.Empty;
            }

            lo.Visitno = charges.RegistrationNo;

            var prefixCode = AppSession.Parameter.PrefixOnoSysmexInterop;
            if (!string.IsNullOrWhiteSpace(prefixCode))
            {
                lo.HealthcareCode = prefixCode;
                lo.Pid = prefixCode + lo.Pid;
                lo.Ono = prefixCode + lo.Ono;
                lo.Source = prefixCode + lo.Source;
                lo.Clinician = prefixCode + lo.Clinician;
            }

            //switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            //{
            //    case "RSUI":
            //        lo.HealthcareCode = "TG";
            //        lo.Pid = "TG" + lo.Pid;
            //        lo.Ono = "TG" + lo.Ono;
            //        lo.Source = "TG" + lo.Source;
            //        lo.Clinician = "TG" + lo.Clinician;
            //        break;
            //    case "RSPM":
            //        lo.HealthcareCode = "ST";
            //        lo.Pid = "ST" + lo.Pid;
            //        lo.Ono = "ST" + lo.Ono;
            //        lo.Source = "ST" + lo.Source;
            //        lo.Clinician = "ST" + lo.Clinician;
            //        break;
            //}

            if (transChargesItems.Any(t => (t.IsOrderRealization ?? false)))
            {
                var items = new ItemCollection();
                items.Query.Where(items.Query.ItemID.In(transChargesItems.Where(t => string.IsNullOrEmpty(t.ParentNo)).Select(t => t.ItemID)));
                items.Query.Load();

                foreach (var item in items)
                {
                    if (AppSession.Parameter.HisInteropConfigName == AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME) lo.OrderTestid += item.ItemID + "~";
                    else lo.OrderTestid += item.ItemIDExternal + "~";
                }

                lo.Save();
                return true;
            }

            return false;
        }

        private DateTime? WynakomLisInteropUpdateSingleConnection(TransCharges charges, Patient patient, Registration reg,
            TransChargesItemCollection transChargesItems, ServiceUnit unit, Guarantor guar)
        {
            if (charges.ToServiceUnitID != _serviceUnitLaboratoryID) return null;

            var demographics = new BusinessObject.Interop.Wynakom.Demographics();
            demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

            if (!demographics.LoadByPrimaryKey(string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo))
                demographics = new BusinessObject.Interop.Wynakom.Demographics();
            demographics.PatientId = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
            demographics.GenderId = patient.Sex;
            demographics.DateOfBirth = patient.DateOfBirth;

            var std = new AppStandardReferenceItem();
            if (std.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), patient.SRSalutation))
                demographics.PatientName = string.Format("{0} {1}", std.ItemName, patient.PatientName);
            else
                demographics.PatientName = string.Format("{0}", patient.PatientName); // string.Format("{0} {1}", string.Empty, patient.PatientName);

            demographics.PatientAddress = patient.Address;
            demographics.CityName = patient.City;
            demographics.PhoneNumber = patient.PhoneNo;
            demographics.FaxNumber = patient.FaxNo;
            demographics.MobileNumber = patient.MobilePhoneNo;
            demographics.Email = patient.Email;
            demographics.KtpNumber = patient.Ssn;

            var registration = new BusinessObject.Interop.Wynakom.Registration();
            registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

            registration.PatientId = demographics.PatientId;
            registration.VisitNumber = reg.RegistrationNo;
            registration.OrderNumber = charges.TransactionNo;
            registration.OrderDateTime = DateTime.Now;
            registration.DiagnoseName = string.Empty;
            registration.Cito = transChargesItems.Any(t => t.IsCito ?? false);
            registration.ServiceUnitID = unit.ServiceUnitID;
            registration.ServiceUnitName = unit.ServiceUnitName;
            registration.GuarantorID = guar.GuarantorID;
            registration.GuarantorName = guar.GuarantorName;

            var pmedic = new Paramedic();
            pmedic.LoadByPrimaryKey(reg.ParamedicID);
            registration.DoctorID = pmedic.ParamedicID;
            registration.DoctorName = pmedic.ParamedicName;

            var cls = new Class();
            cls.LoadByPrimaryKey(charges.ClassID);
            registration.ClassID = cls.ClassID;
            registration.ClassName = cls.ClassName;
            registration.AgreementID = cls.ClassID;
            registration.AgreementName = cls.ClassName;

            registration.WardID = unit.ServiceUnitID;
            registration.WardName = unit.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(charges.RoomID);
            registration.RoomID = room.RoomID;
            registration.RoomName = room.RoomName;

            registration.BedID = charges.BedID;
            registration.BedName = charges.BedID;
            registration.RegUserName = AppSession.UserLogin.UserName;
            registration.Notes = reg.Notes;

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSJKT")
            {
                //registration.PhysicianSenders = reg.PhysicianSenders;
            }

            var ordered_item = new BusinessObject.Interop.Wynakom.OrderedItems();
            ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
            ordered_item.OrderNumber = registration.OrderNumber;
            ordered_item.OrderItemDate = registration.OrderDateTime;

            bool isInsert = false;
            string maxSequence = string.Empty;
            foreach (var entity in transChargesItems.Where(t => t.IsOrderRealization ?? false && t.IsVoid == false))
            {
                var item = new Item();
                item.LoadByPrimaryKey(entity.ItemID);

                if (!string.IsNullOrEmpty(item.ItemIDExternal))
                {
                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                    {
                        if (AppSession.Parameter.HealthcareInitial == "RSI")
                        {
                            var pa = new BusinessObject.Interop.ROCHE.PocAdt();
                            pa.es.Connection.Name = AppConstant.HIS_INTEROP.ROCHE_LIS_INTEROP_CONNECTION_NAME;
                            pa.Action = "ADD";
                            pa.Pid = patient.MedicalNo;
                            pa.Dob = patient.DateOfBirth;
                            pa.Sex = patient.Sex;
                            pa.VisitNum = charges.TransactionNo + $"^{entity.SequenceNo}";

                            var pc = string.Empty;
                            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) pc = "1";
                            else if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient) pc = "2";
                            else if (reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient) pc = "3";
                            else pc = "2";
                            pa.PatientClass = pc;

                            var fUnit = new ServiceUnit();
                            fUnit.LoadByPrimaryKey(charges.FromServiceUnitID);

                            pa.LocationCode = fUnit.ServiceUnitID;
                            pa.LocationName = fUnit.ServiceUnitName;
                            pa.DoctorCode = pmedic.ParamedicID;
                            pa.DoctorName = pmedic.ParamedicName;
                            pa.AdmitDt = charges.ExecutionDate;
                            pa.PatientName = patient.PatientName;

                            pa.Save();
                        }
                    }
                }

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSPP")
                {
                    //ordered_item.OrderItemID += (string.IsNullOrEmpty(item.ItemIDExternal) ? item.ItemID : item.ItemIDExternal) + "~";
                    ordered_item.OrderItemID += item.ItemID + "~";
                    ordered_item.OrderItemName += item.ItemName + "~";
                    isInsert = true;
                }
                else
                {
                    ordered_item.OrderItemID += item.ItemID + "~";
                    ordered_item.OrderItemName += item.ItemName + "~";
                    isInsert = true;
                }

                maxSequence = entity.SequenceNo;
            }

            if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
            {
                if (AppSession.Parameter.HealthcareInitial == "RSI")
                {
                    registration.OrderNumber += $"^{maxSequence}";
                    ordered_item.OrderNumber += $"^{maxSequence}";
                }
            }


            if (isInsert)
            {
                if (!string.IsNullOrEmpty(ordered_item.OrderItemID) && !string.IsNullOrEmpty(ordered_item.OrderItemName))
                {
                    ordered_item.OrderItemID = ordered_item.OrderItemID.Remove(ordered_item.OrderItemID.Length - 1);
                    ordered_item.OrderItemName = ordered_item.OrderItemName.Remove(ordered_item.OrderItemName.Length - 1);
                }

                demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                demographics.Save();

                registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                registration.Save();

                ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                ordered_item.Save();
            }

            return registration.OrderDateTime;
        }


        private DateTime? WynakomLisInteropUpdateMultipleConnection(TransCharges charges, Patient patient, Registration reg,
            TransChargesItemCollection transChargesItems, ServiceUnit unit, Guarantor guar, Paramedic paramedic, ItemBridgingCollection itemBridgings, string groupConnectionName)
        {
            if (charges.ToServiceUnitID != _serviceUnitLaboratoryID) return null;


            var demographics = new BusinessObject.Interop.Wynakom.Demographics();
            demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

            if (!demographics.LoadByPrimaryKey(string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo))
                demographics = new BusinessObject.Interop.Wynakom.Demographics();
            demographics.PatientId = string.IsNullOrEmpty(patient.MedicalNo) ? patient.PatientID : patient.MedicalNo;
            demographics.GenderId = patient.Sex;
            demographics.DateOfBirth = patient.DateOfBirth;

            var std = new AppStandardReferenceItem();
            if (std.LoadByPrimaryKey(AppEnum.StandardReference.Salutation.ToString(), patient.SRSalutation))
                demographics.PatientName = string.Format("{0} {1}", std.ItemName, patient.PatientName);
            else
                demographics.PatientName = string.Format("{0}", patient.PatientName); // string.Format("{0} {1}", string.Empty, patient.PatientName);

            demographics.PatientAddress = patient.Address;
            demographics.CityName = patient.City;
            demographics.PhoneNumber = patient.PhoneNo;
            demographics.FaxNumber = patient.FaxNo;
            demographics.MobileNumber = patient.MobilePhoneNo;
            demographics.Email = patient.Email;
            demographics.KtpNumber = patient.Ssn;

            var registration = new BusinessObject.Interop.Wynakom.Registration();
            registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

            registration.PatientId = demographics.PatientId;
            registration.VisitNumber = reg.RegistrationNo;
            registration.OrderNumber = charges.TransactionNo;
            registration.OrderDateTime = DateTime.Now;
            registration.DiagnoseName = string.Empty;
            registration.Cito = transChargesItems.Any(t => t.IsCito ?? false);
            registration.ServiceUnitID = unit.ServiceUnitID;
            registration.ServiceUnitName = unit.ServiceUnitName;
            registration.GuarantorID = guar.GuarantorID;
            registration.GuarantorName = guar.GuarantorName;

            registration.DoctorID = reg.ParamedicID;
            registration.DoctorName = paramedic.ParamedicName;

            var cls = new Class();
            cls.LoadByPrimaryKey(charges.ClassID);
            registration.ClassID = cls.ClassID;
            registration.ClassName = cls.ClassName;
            registration.AgreementID = cls.ClassID;
            registration.AgreementName = cls.ClassName;

            registration.WardID = unit.ServiceUnitID;
            registration.WardName = unit.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(charges.RoomID);
            registration.RoomID = room.RoomID;
            registration.RoomName = room.RoomName;

            registration.BedID = charges.BedID;
            registration.BedName = charges.BedID;
            registration.RegUserName = AppSession.UserLogin.UserName;
            registration.Notes = reg.Notes;

            var ordered_item = new BusinessObject.Interop.Wynakom.OrderedItems();
            ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
            ordered_item.OrderNumber = registration.OrderNumber;
            ordered_item.OrderItemDate = registration.OrderDateTime;

            bool isInsert = false;
            foreach (var i in itemBridgings.Where(i => i.ConnectionName == groupConnectionName))
            {
                var item = new Item();
                item.LoadByPrimaryKey(i.ItemID);

                //ordered_item.OrderItemID += (string.IsNullOrEmpty(item.ItemIDExternal) ? item.ItemID : item.ItemIDExternal) + "~";
                ordered_item.OrderItemID += item.ItemID + "~";
                ordered_item.OrderItemName += item.ItemName + "~";
                isInsert = true;
            }

            if (isInsert)
            {
                if (!string.IsNullOrEmpty(ordered_item.OrderItemID) && !string.IsNullOrEmpty(ordered_item.OrderItemName))
                {
                    ordered_item.OrderItemID = ordered_item.OrderItemID.Remove(ordered_item.OrderItemID.Length - 1);
                    ordered_item.OrderItemName = ordered_item.OrderItemName.Remove(ordered_item.OrderItemName.Length - 1);
                }

                demographics.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                demographics.Save();

                registration.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                registration.Save();

                ordered_item.es.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                ordered_item.Save();
            }
            return registration.OrderDateTime;
        }
        private void SatusehatServiceRequestPostAndLogToLis(Registration reg, string transactionNo, string lisConnectionName)
        {
            var tc = new TransCharges();
            if (!tc.LoadByPrimaryKey(transactionNo)) return;

            // Stusehat post encounter & ServiceRequest
            var util = new Temiang.Avicenna.WebService.Satusehat();
            util.OrderLab(transactionNo);

            // Update Wynakom
            var satuSehatLog = new SatuSehatKunjungan();
            if (!satuSehatLog.LoadByPrimaryKey(reg.RegistrationNo)) return;

            if (!satuSehatLog.EncounterID.HasValue) return;

            // Load history Service Request
            var ssServiceReqs = new SatuSehatResultCollection();
            ssServiceReqs.Query.Where(ssServiceReqs.Query.EncounterID == satuSehatLog.EncounterID, ssServiceReqs.Query.ResourceType == "ServiceRequest", ssServiceReqs.Query.Category == transactionNo);
            ssServiceReqs.Query.Load();

            //var ssItems = new Temiang.Avicenna.BusinessObject.Interop.Wynakom.SatusehatOrderedItemsCollection();

            var satuSehatBridgingType = AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID);
            var patSs = new PatientBridging();
            patSs.LoadByPrimaryKey(reg.PatientID, satuSehatBridgingType);

            var parSs = new ParamedicBridging();
            parSs.Query.Where(parSs.Query.ParamedicID == reg.ParamedicID, parSs.Query.SRBridgingType == satuSehatBridgingType);
            parSs.Query.es.Top = 1;
            parSs.Query.Load();

            var itemIds = string.Empty;
            var itemNames = string.Empty;
            var loinscIds = string.Empty;
            var loinscNames = string.Empty;
            var servReqs = string.Empty;
            var specimens = string.Empty;
            var itemCount = 0;
            foreach (var ssResult in ssServiceReqs)
            {
                var tci = new TransChargesItem();
                if (!tci.LoadByPrimaryKey(transactionNo, ssResult.Code)) continue;

                if (tci.IsOrderRealization == null || tci.IsOrderRealization == false) continue;

                itemCount++;
                var item = new Item();
                item.LoadByPrimaryKey(tci.ItemID);

                switch (lisConnectionName)
                {
                    #region SYSMEX_LIS_INTEROP_CONNECTION_NAME
                    case AppConstant.HIS_INTEROP.MEDICLAB_LIS_INTEROP_CONNECTION_NAME:
                    case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                    case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                        itemIds = string.Concat(itemIds, item.ItemIDExternal, "~");
                        break;
                    #endregion SYSMEX_LIS_INTEROP_CONNECTION_NAME
                    default:
                        itemIds = string.Concat(itemIds, item.ItemID, "~"); // Pakai versi Wynakom dulu
                        break;
                }

                itemNames = string.Concat(itemNames, item.ItemName, "~");

                var itemBg = new ItemBridging();
                itemBg.Query.Where(itemBg.Query.ItemID == tci.ItemID, itemBg.Query.SRBridgingType == satuSehatBridgingType);
                itemBg.Query.es.Top = 1;
                itemBg.Query.Load();

                loinscIds = string.Concat(loinscIds, itemBg.BridgingID, "~");
                loinscNames = string.Concat(loinscNames, itemBg.BridgingName, "~");
                servReqs = string.Concat(servReqs, ssResult.ResultID.ToString(), "~");

                // Specimen
                var ssSpecimen = new SatuSehatResult();
                ssSpecimen.Query.Where(ssSpecimen.Query.EncounterID == satuSehatLog.EncounterID, ssSpecimen.Query.ResourceType == "Specimen", ssSpecimen.Query.Category == transactionNo, ssSpecimen.Query.Code == tci.SequenceNo);
                ssSpecimen.Query.es.Top = 1;
                if (ssSpecimen.Query.Load())
                {
                    specimens = string.Concat(specimens, ssSpecimen.ResultID.ToString(), "~");
                }
            }

            if (!string.IsNullOrWhiteSpace(itemIds))
            {
                var ssItem = new BusinessObject.Interop.Wynakom.SatusehatOrderedItems(); // Saat ini menggunakan table yang sama untuk beberapa macam LIS
                ssItem.es.Connection.Name = lisConnectionName;

                var orderNumber = string.Empty;
                switch (lisConnectionName)
                {
                    #region SYSMEX_LIS_INTEROP_CONNECTION_NAME
                    case AppConstant.HIS_INTEROP.MEDICLAB_LIS_INTEROP_CONNECTION_NAME:
                    case AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME:
                    case AppConstant.HIS_INTEROP.SYSMEX_LIS_INTEROP_CONNECTION_NAME:
                        orderNumber = transactionNo;
                        break;
                    #endregion SYSMEX_LIS_INTEROP_CONNECTION_NAME
                    default:
                        orderNumber = string.Format("{0}^{1:000}", transactionNo, itemCount); // Pakai versi Wynakom dulu
                        break;
                }

                if (!ssItem.LoadByPrimaryKey(orderNumber, ""))
                {
                    ssItem = new BusinessObject.Interop.Wynakom.SatusehatOrderedItems();
                    ssItem.es.Connection.Name = lisConnectionName;
                }

                ssItem.OrderNumber = orderNumber;
                ssItem.OrderSequenceNo = string.Empty;
                ssItem.SSEncounterID = satuSehatLog.EncounterID.ToString();

                ssItem.SSPatientID = patSs.BridgingID;
                ssItem.SSPatientName = patSs.BridgingName;

                ssItem.SSRequesterPractionerID = parSs.BridgingID;
                ssItem.SSRequesterPractionerName = parSs.BridgingName;

                ssItem.OrderItemID = itemIds.Remove(itemIds.Length - 1);
                ssItem.OrderItemName = itemNames.Remove(itemNames.Length - 1);

                if (!string.IsNullOrEmpty(loinscIds))
                    ssItem.SSLoincID = loinscIds.Remove(loinscIds.Length - 1);

                if (!string.IsNullOrEmpty(loinscNames))
                    ssItem.SSLoincName = loinscNames.Remove(loinscNames.Length - 1);

                if (!string.IsNullOrEmpty(servReqs))
                    ssItem.SSServiceRequestID = servReqs.Remove(servReqs.Length - 1);

                if (!string.IsNullOrEmpty(specimens))
                    ssItem.SSSpecimenID = specimens.Remove(specimens.Length - 1);

                ssItem.Save();
            }
        }

        private string GetItemName(string itemId)
        {
            var item = new Item();
            return item.LoadByPrimaryKey(itemId) ? item.ItemName : string.Empty;
        }

        private TransChargesItemCollection TransChargesItems
        {
            get
            {
                if (Session["collTransChargesItem" + Request.UserHostName] != null) return (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];

                var coll = new TransChargesItemCollection();

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
                        item.ItemName.As("refToItem_ItemName"),
                        header.ToServiceUnitID.As("refToTransCharges_ToServiceUnitID"),
                        item.SRItemType.As("refToItem_SRItemType"),
                        "<CAST((CASE WHEN b.SRItemType IN ('" + ItemType.Medical + "', '" + ItemType.NonMedical + "') THEN 0 ELSE 1 END) AS BIT) AS refTo_IsItemTypeService>",
                        "<'' as refTo_PrevOrder>",
                        reg.SRRegistrationType.As("refToRegistration_SRRegistrationType"),
                        @"<CASE WHEN st.ItemName IS NULL THEN '' ELSE 'Specimen Type: ' + st.ItemName END AS 'refTo_SpecimenTypeName'>",
                        @"<CASE WHEN ISNULL(d.SRBloodSampleTakenBy, '') = '' THEN '' ELSE (CASE WHEN a.SpecimenReceivedDateTime IS NULL THEN '0' ELSE '1' END) END AS 'refTo_SpecimenStatus'>",
                        @"<ISNULL(a.IsCasemixApproved, 0) AS 'refto_IsCasemixApprovedFlag'>",
                        @"<CASE WHEN ISNULL(a.CasemixNotes, '') = '' THEN '' ELSE ' (Casemix: ' + a.CasemixNotes +')' END AS 'refTo_CombinedNotes'>"
                    );
                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
                query.LeftJoin(itemlab).On(itemlab.ItemID == query.ItemID);
                query.LeftJoin(stype).On(stype.StandardReferenceID == "SpecimenType" & stype.ItemID == itemlab.SRSpecimenType);
                query.Where
                    (
                        query.TransactionNo == Request.QueryString["joNo"],
                        query.IsVoid == false,
                        query.IsBillProceed == false,
                        query.NotExists(tci)
                    );
                query.Where(query.ParentNo == string.Empty);
                query.Where(query.ChargeQuantity > 0);
                if (AppSession.Parameter.IsJobOrderRealizationNeedConfirm) query.Where(query.IsPaymentConfirmed.IsNotNull(), query.IsPaymentConfirmed == true);

                query.OrderBy(query.SequenceNo.Ascending);
                //DataTable dtb = query.LoadDataTable();

                coll.Load(query);
                coll.SetPrevOrder(RegistrationNo, AppSession.Parameter.IntervalOrderWarning);

                Session["collTransChargesItem" + Request.UserHostName] = coll;

                return coll;
            }
            set
            { Session["collTransChargesItem" + Request.UserHostName] = value; }
        }

        private void LoadTransChargesItemComp()
        {
            var collComp = new TransChargesItemCompCollection();
            collComp.Query.Where(collComp.Query.TransactionNo == Request.QueryString["joNo"]);
            collComp.Query.OrderBy
                (
                    collComp.Query.SequenceNo.Ascending,
                    collComp.Query.TariffComponentID.Ascending
                );
            collComp.LoadAll();

            var trans = new TransCharges();
            trans.LoadByPrimaryKey(Request.QueryString["joNo"]);

            var reg = new Registration();
            reg.LoadByPrimaryKey(trans.RegistrationNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(txtGuarantorID.Text);

            var tariffDate = grr.TariffCalculationMethod == 1 ? reg.RegistrationDate.Value.Date : trans.ExecutionDate.Value.Date;

            if (string.IsNullOrEmpty(trans.PackageReferenceNo))
            {
                foreach (var entity in TransChargesItems)
                {
                    var comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, trans.ClassID, entity.ItemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, _defaultTariffClass, entity.ItemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, trans.ClassID, entity.ItemID);
                    if (!comps.Any())
                        comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, _defaultTariffClass, entity.ItemID);

                    foreach (var comp in comps)
                    {
                        var component = collComp.FindByPrimaryKey(entity.TransactionNo, entity.SequenceNo, comp.TariffComponentID);
                        if (component == null)
                        {
                            component = collComp.AddNew();
                            component.TransactionNo = entity.TransactionNo;
                            component.SequenceNo = entity.SequenceNo;
                            component.TariffComponentID = comp.TariffComponentID;
                            component.Price = comp.Price;
                            component.DiscountAmount = 0;
                            component.CitoAmount = (!entity.IsCitoInPercent ?? false) ? entity.BasicCitoAmount : ((entity.BasicCitoAmount / 100) * comp.Price);
                            component.ParamedicID = string.Empty;
                            component.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            component.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            component.IsPackage = false;
                        }
                    }
                }
            }

            Session["collTransChargesItemComp" + Request.UserHostName] = collComp;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument == "rebind")
                grdTransChargesItem.Rebind();
            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                var entity = TransChargesItems.FindByPrimaryKey(param[1], param[2]);

                switch (param[0])
                {
                    case "delete":
                        if (entity != null)
                        {
                            entity.IsOrderRealization = false;
                            entity.IsApprove = false;
                            entity.IsVoid = true;
                            entity.VoidByUserID = AppSession.UserLogin.UserID;
                            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
                            entity.Notes = (entity.Notes + " " + string.Format("void reason: {0}", param[3])).Trim();
                        }
                        break;
                    case "verify":
                        var hd = new TransCharges();
                        hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(hd.RegistrationNo);

                        var grr = new Guarantor();
                        grr.LoadByPrimaryKey(txtGuarantorID.Text);

                        var tariffDate = grr.TariffCalculationMethod == 1 ? reg.RegistrationDate.Value.Date : hd.ExecutionDate.Value.Date;

                        if (entity.IsCito ?? false)
                        {
                            var tariff = Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, hd.ClassID, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, _defaultTariffClass, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType);
                            if (tariff == null)
                                tariff = Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, hd.ClassID, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, _defaultTariffClass, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType);

                            entity.CitoAmount = (tariff.IsCitoInPercent ?? false) ? (tariff.CitoValue / 100) * entity.Price : tariff.CitoValue;
                        }

                        entity.Total = entity.ChargeQuantity * ((entity.Price - entity.DiscountAmount) + entity.CitoAmount);
                        entity.IsOrderRealization = true;
                        entity.IsBillProceed = true;
                        entity.ParamedicID = string.Empty;
                        entity.SRDiscountReason = string.Empty;
                        entity.IsApprove = true;
                        entity.IsVoid = false;
                        entity.FilmNo = string.Empty;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        var p = string.Empty;
                        var pCode = string.Empty;

                        if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(entity.ParamedicCollectionName))
                        {
                            var collComp = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName];

                            var comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, hd.ClassID, entity.ItemID);
                            if (!comps.Any())
                                comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, _defaultTariffClass, entity.ItemID);
                            if (!comps.Any())
                                comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, hd.ClassID, entity.ItemID);
                            if (!comps.Any())
                                comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, _defaultTariffClass, entity.ItemID);

                            foreach (var comp in comps)
                            {
                                var component = collComp.FindByPrimaryKey(entity.TransactionNo, entity.SequenceNo, comp.TariffComponentID);
                                if (component != null && TariffParamedic().Contains(component.TariffComponentID))
                                {
                                    component.ParamedicID = cboParamedicID.SelectedValue;

                                    var par = new Paramedic();
                                    par.LoadByPrimaryKey(component.ParamedicID);
                                    if (par.IsPrintInSlip ?? true)
                                    {
                                        if (p.Length == 0)
                                        {
                                            p = par.ParamedicName;
                                            pCode = par.ParamedicID;
                                        }
                                        else if (!p.Contains(par.ParamedicName))
                                            p = p + "; " + par.ParamedicName;
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(p))
                            entity.ParamedicCollectionName = p;
                        if (!pCode.Equals(string.Empty))
                            entity.ParamedicID = pCode;

                        break;
                }

                grdTransChargesItem.Rebind();
            }
            else if (eventArgument == "verify_all")
            {
                foreach (GridDataItem r in grdTransChargesItem.MasterTableView.Items)
                {
                    string transactionNo = Request.QueryString["joNo"];
                    string sequenceNo = r.GetDataKeyValue("SequenceNo").ToString();
                    string itemName = r["ItemName"].Text;
                    string srRegistrationType = r["SRRegistrationType"].Text;
                    string specimenStatus = r["SpecimenStatus"].Text;
                    bool isCasemixApproved = ((CheckBox)r.FindControl("chkIsCasemixApproved")).Checked;
                    bool isOrderRealization = ((CheckBox)r.FindControl("chkIsOrderRealization")).Checked;

                    if (GetStatus(isCasemixApproved, isOrderRealization, transactionNo, sequenceNo, itemName, srRegistrationType, specimenStatus))
                    {
                        var entity = TransChargesItems.FindByPrimaryKey(transactionNo, sequenceNo);

                        var hd = new TransCharges();
                        hd.LoadByPrimaryKey(Request.QueryString["joNo"]);

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(hd.RegistrationNo);

                        var grr = new Guarantor();
                        grr.LoadByPrimaryKey(txtGuarantorID.Text);

                        var tariffDate = grr.TariffCalculationMethod == 1 ? reg.RegistrationDate.Value.Date : hd.ExecutionDate.Value.Date;

                        if (entity.IsCito ?? false)
                        {
                            var tariff = Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, hd.ClassID, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, _defaultTariffClass, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType);
                            if (tariff == null)
                                tariff = Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, hd.ClassID, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                         Helper.Tariff.GetItemTariff(tariffDate, _defaultTariffType, _defaultTariffClass, hd.ClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType);

                            entity.CitoAmount = (tariff.IsCitoInPercent ?? false) ? (tariff.CitoValue / 100) * entity.Price : tariff.CitoValue;
                        }

                        entity.Total = entity.ChargeQuantity * ((entity.Price - entity.DiscountAmount) + entity.CitoAmount);
                        entity.IsOrderRealization = true;
                        entity.IsBillProceed = true;
                        entity.ParamedicID = string.Empty;
                        entity.SRDiscountReason = string.Empty;
                        entity.IsApprove = true;
                        entity.IsVoid = false;
                        entity.FilmNo = string.Empty;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        var p = string.Empty;
                        var pCode = string.Empty;

                        if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(entity.ParamedicCollectionName))
                        {
                            var collComp = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName];

                            var comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, hd.ClassID, entity.ItemID);
                            if (!comps.Any())
                                comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, grr.SRTariffType, _defaultTariffClass, entity.ItemID);
                            if (!comps.Any())
                                comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, hd.ClassID, entity.ItemID);
                            if (!comps.Any())
                                comps = Helper.Tariff.GetItemTariffComponentCollection(tariffDate, _defaultTariffType, _defaultTariffClass, entity.ItemID);

                            foreach (var comp in comps)
                            {
                                var component = collComp.FindByPrimaryKey(entity.TransactionNo, entity.SequenceNo, comp.TariffComponentID);
                                if (component != null && TariffParamedic().Contains(component.TariffComponentID))
                                {
                                    component.ParamedicID = cboParamedicID.SelectedValue;

                                    var par = new Paramedic();
                                    par.LoadByPrimaryKey(component.ParamedicID);
                                    if (par.IsPrintInSlip ?? true)
                                    {
                                        if (p.Length == 0)
                                        {
                                            p = par.ParamedicName;
                                            pCode = par.ParamedicID;
                                        }
                                        else if (!p.Contains(par.ParamedicName))
                                            p = p + "; " + par.ParamedicName;
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(p))
                            entity.ParamedicCollectionName = p;
                        if (!pCode.Equals(string.Empty))
                            entity.ParamedicID = pCode;
                    }
                }
                grdTransChargesItem.Rebind();
            }
            else if (eventArgument.Contains("!"))
            {
                var param = eventArgument.Split('!');
                var regno = param[1];
                var diagno = param[2];
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(regno))
                {
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(reg.PatientID))
                    {
                        pat.DiagnosticNo = diagno;
                        pat.Save();
                        txtRadiologyNo.Text = diagno;
                    }
                }
                grdTransChargesItem.Rebind();
            }
        }

        private static bool IsOrderRealization(string transactionNo, string sequenceNo)
        {
            var entity = new TransChargesItem();
            entity.LoadByPrimaryKey(transactionNo, sequenceNo);
            return (entity.IsOrderRealization ?? false);
        }

        protected void grdTransChargesItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = TransChargesItems[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (var i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        private IEnumerable<string> TariffParamedic()
        {
            var coll = new TariffComponentCollection();
            coll.Query.Where(coll.Query.IsTariffParamedic == true);
            coll.LoadAll();

            var arr = new string[coll.Count];

            var idx = 0;
            foreach (var item in coll)
            {
                arr.SetValue(item.TariffComponentID, idx);
                idx++;
            }

            return arr;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        protected void btnLisSatusehat_Click(object sender, EventArgs e)
        {
            #region Reupdate JO No
            var txNo = Request.QueryString["joNo"];
            var charges = new TransCharges();
            charges.LoadByPrimaryKey(txNo);

            var reg = new Registration();
            reg.LoadByPrimaryKey(charges.RegistrationNo);

            SatusehatServiceRequestPostAndLogToLis(reg, charges.TransactionNo, AppSession.Parameter.HisInteropConfigName);

            #endregion


            // ReUpdate
            //var ssItem = new BusinessObject.Interop.Wynakom.SatusehatOrderedItemsQuery("a"); // Saat ini menggunakan table yang sama untuk beberapa macam LIS
            //ssItem.es2.Connection.Name = AppSession.Parameter.HisInteropConfigName;
            //ssItem.Where(ssItem.OrderNumber > "JO250819-00137");  //, ssItem.SSSpecimenID.IsNotNull());
            //ssItem.Select(ssItem.OrderNumber);

            #region Reupdate From JO No
            //var ssItem = new SatuSehatResultQuery("a");
            //ssItem.Where(ssItem.ResourceType== "ServiceRequest", ssItem.Category > "JO250819-00137");
            //ssItem.Select(ssItem.Category);
            //ssItem.es.Distinct = true;

            //var dtb = ssItem.LoadDataTable();
            //foreach (DataRow row in dtb.Rows)
            //{
            //    var charges = new TransCharges();
            //    //var trNo = row[0].ToString().Split('^')[0];
            //    var trNo = row[0].ToString();
            //    if (charges.LoadByPrimaryKey(trNo))
            //    {
            //        var reg = new Registration();
            //        reg.LoadByPrimaryKey(charges.RegistrationNo);

            //        SatusehatServiceRequestPostAndLogToLis(reg, charges.TransactionNo, AppSession.Parameter.HisInteropConfigName);
            //    }
            //}
            #endregion

        }
        private TransChargesItemConsumptionCollection TransChargesItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemConsumption" + Request.UserHostName + hdnPageId.Value];
                    if (obj != null)
                        return ((TransChargesItemConsumptionCollection)(obj));
                }

                var coll = new TransChargesItemConsumptionCollection();

                var query = new TransChargesItemConsumptionQuery("a");
                var item = new ItemQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName")
                    );
                if (AppSession.Parameter.IsValidateMaxQtyItemConsumptions)
                    query.Select(@"<CASE WHEN a.Qty = 0 THEN 10000 ELSE a.Qty END AS 'refTo_MaxQty'>");
                else
                    query.Select(@"<CAST(10000 AS NUMERIC(10,2)) AS 'refTo_MaxQty'>");

                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);
                query.Where(query.TransactionNo == txtJobOrderNo.Text);

                coll.Load(query);

                Session["collTransChargesItemConsumption" + Request.UserHostName + hdnPageId.Value] = coll;
                return coll;
            }
            set { Session["collTransChargesItemConsumption" + Request.UserHostName + hdnPageId.Value] = value; }
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID, string sex)
        {
            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    //imgPatientPhoto.ImageUrl = optSexMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                    imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : (sex == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                //imgPatientPhoto.ImageUrl = optSexMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : (sex == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");

        }

        #endregion

        protected string GetStatus(object IsCasemixApproved, object IsOrderRealization, object TransactionNo, object SequenceNo, object ItemName, object SRRegistrationType, object SpecimenStatus)
        {
            if (Helper.GuarantorBpjsCasemix.Contains(txtGuarantorID.Text))
            {
                if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(SRRegistrationType.ToString()))
                {
                    if (IsCasemixApproved == null || IsCasemixApproved == DBNull.Value || IsCasemixApproved.Equals(false))
                        return string.Format("<a href=\"#\" onclick=\"alert('Validation required by Casemix for item : {0}.'); return false;\"><img src=\"../../../../Images/Toolbar/lock16.png\" border=\"0\" /></a>", ItemName);
                    //else if (IsCasemixApproved.Equals(false))
                    //    return string.Format("<a href=\"#\" onclick=\"alert('Item : {0} is cancelled by Casemix.'); return false;\"><img src=\"../../../../Images/Toolbar/blacklist.png\" border=\"0\" /></a>", ItemName);
                    else
                    {

                        if (AppSession.Parameter.IsValidatedSpecimenOnOrderRealization & !string.IsNullOrEmpty(SpecimenStatus.ToString()) & SpecimenStatus.ToString() == "0")
                        {
                            return string.Format("<a href=\"#\" onclick=\"alert('Specimens for item : {0} have not been received.'); return false;\"><img src=\"../../../../Images/Toolbar/lock16_d.png\" border=\"0\" /></a>", ItemName);
                        }
                        return string.Format("<a href=\"#\" onclick=\"verifyOrder('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/{2}\" border=\"0\" /></a>", TransactionNo, SequenceNo, IsOrderRealization.Equals(true) ? "post16.png" : "post16_d.png");
                    }

                }
                else
                {
                    if (AppSession.Parameter.IsValidatedSpecimenOnOrderRealization & !string.IsNullOrEmpty(SpecimenStatus.ToString()) & SpecimenStatus.ToString() == "0")
                    {
                        return string.Format("<a href=\"#\" onclick=\"alert('Specimens for item : {0} have not been received.'); return false;\"><img src=\"../../../../Images/Toolbar/lock16_d.png\" border=\"0\" /></a>", ItemName);
                    }
                    return string.Format("<a href=\"#\" onclick=\"verifyOrder('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/{2}\" border=\"0\" /></a>", TransactionNo, SequenceNo, IsOrderRealization.Equals(true) ? "post16.png" : "post16_d.png");
                }

            }
            else
            {
                if (AppSession.Parameter.IsValidatedSpecimenOnOrderRealization & !string.IsNullOrEmpty(SpecimenStatus.ToString()) & SpecimenStatus.ToString() == "0")
                {
                    return string.Format("<a href=\"#\" onclick=\"alert('Specimens for item : {0} have not been received.'); return false;\"><img src=\"../../../../Images/Toolbar/lock16_d.png\" border=\"0\" /></a>", ItemName);
                }
                return string.Format("<a href=\"#\" onclick=\"verifyOrder('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/{2}\" border=\"0\" /></a>", TransactionNo, SequenceNo, IsOrderRealization.Equals(true) ? "post16.png" : "post16_d.png");
            }
        }

        protected bool GetStatus(bool IsCasemixApproved, bool IsOrderRealization, string TransactionNo, string SequenceNo, string ItemName, string SRRegistrationType, string SpecimenStatus)
        {
            if (Helper.GuarantorBpjsCasemix.Contains(txtGuarantorID.Text))
            {
                if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(SRRegistrationType.ToString()))
                {
                    if (IsCasemixApproved == null || IsCasemixApproved.Equals(false))
                        return false;
                    else
                    {
                        if (AppSession.Parameter.IsValidatedSpecimenOnOrderRealization & !string.IsNullOrEmpty(SpecimenStatus.ToString()) & SpecimenStatus.ToString() == "0")
                        {
                            return false;
                        }
                        return true;
                    }
                }
                else
                {
                    if (AppSession.Parameter.IsValidatedSpecimenOnOrderRealization & !string.IsNullOrEmpty(SpecimenStatus.ToString()) & SpecimenStatus.ToString() == "0")
                    {
                        return false;
                    }
                    return true;
                }
            }
            else
            {
                if (AppSession.Parameter.IsValidatedSpecimenOnOrderRealization & !string.IsNullOrEmpty(SpecimenStatus.ToString()) & SpecimenStatus.ToString() == "0")
                {
                    return false;
                }
                return true;
            }
        }

        private string[] GuarantorBPJS
        {
            get
            {
                if (ViewState["bpjs"] != null) return (string[])ViewState["bpjs"];
                var grr = new GuarantorBridgingCollection();
                grr.Query.es.Distinct = true;
                grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                if (grr.Query.Load()) ViewState["bpjs"] = grr.Select(g => g.GuarantorID).ToArray();
                else ViewState["bpjs"] = new string[] { string.Empty };

                return (string[])ViewState["bpjs"];
            }
        }

        protected string GetStatusTariffComponent(object IsCasemixApproved, object IsOrderRealization, object TransactionNo, object SequenceNo, object ItemName, object ItemID, object ToServiceUnitID, object SRItemType, object SRRegistrationType, object SpecimenStatus)
        {
            if (SRItemType.Equals(ItemType.Medical) || SRItemType.Equals(ItemType.NonMedical) || SRItemType.Equals(ItemType.Kitchen)) return string.Empty;
            else
            {
                if (Helper.GuarantorBpjsCasemix.Contains(txtGuarantorID.Text))
                {
                    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(SRRegistrationType.ToString()))
                    {
                        if (IsCasemixApproved == null || IsCasemixApproved == DBNull.Value || IsCasemixApproved.Equals(false))
                            return string.Format("<a href=\"#\" onclick=\"alert('Validation required by Casemix for item : {0}.'); return false;\"><img src=\"../../../../Images/Toolbar/lock16.png\" border=\"0\" /></a>", ItemName);
                        else
                            return string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}','{1}','{2}','{3}','{4}'); return false;\">Tariff Component</a>", TransactionNo, ItemID, SequenceNo, ToServiceUnitID, SRItemType);
                    }
                    else
                        return string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}','{1}','{2}','{3}','{4}'); return false;\">Tariff Component</a>", TransactionNo, ItemID, SequenceNo, ToServiceUnitID, SRItemType);
                }
                else
                    return string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}','{1}','{2}','{3}','{4}'); return false;\">Tariff Component</a>", TransactionNo, ItemID, SequenceNo, ToServiceUnitID, SRItemType);
            }
        }

        private void PopulateSoap()
        {
            // Display SOAP history current Patient Episode order by DateTimeInfo.Descending (Handono 231104)
            var regNos = Registration.RelatedRegistrations(RegistrationNo);
            var rimColl = new RegistrationInfoMedicCollection();
            rimColl.Query.Where(
                rimColl.Query.RegistrationNo.In(regNos),
                rimColl.Query.SRMedicalNotesInputType == "SOAP",
                rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
                );
            rimColl.Query.OrderBy(rimColl.Query.DateTimeInfo.Descending);
            rimColl.LoadAll();

            var strb = new StringBuilder();
            strb.AppendLine("<table width=\"100%\"><tr><td width=\"25%\" class=\"rgHeader\">Subjective</td><td width=\"25%\" class=\"rgHeader\">Objective</td><td width=\"25%\" class=\"rgHeader\">Assessmnet</td><td width=\"25%\" class=\"rgHeader\">Planning</td></tr></table>");
            strb.AppendLine("<div style=\"overflow: auto;width:100%; height: 140px;\"><table width=\"100%\">");

            var i = 0;
            foreach (var rim in rimColl)
            {
                if (!string.IsNullOrWhiteSpace(string.Concat(rim.Info1, rim.Info2, rim.Info3, rim.Info4)))
                {
                    i++;
                    var className = i % 2 == 0 ? "rgAltRow" : "rgRow";
                    strb.AppendLine("<tr>");
                    strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}<br /></td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info1.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info2.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info3.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendFormat("<td valign=\"top\" class=\"{2}\" width=\"25%\"><b>{0}</b><br />{1}</td>", rim.DateTimeInfo.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), rim.Info4.Trim().Replace(System.Environment.NewLine, "<br />"), className);
                    strb.AppendLine("</tr>");

                    litSoapA.Text = string.IsNullOrEmpty(rim.Info3.Trim()) ? litSoapA.Text : litSoapA.Text + rim.Info3; // Dipakai untuk pengecekan "Diagnosis required"
                }
            }
            strb.AppendLine("</table></div>");
            litSoap.Text = strb.ToString();
        }

    }
}
