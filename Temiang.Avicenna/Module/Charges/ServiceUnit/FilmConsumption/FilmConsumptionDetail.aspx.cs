using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class FilmConsumptionDetail : BasePageDialog
    {
        private string _serviceUnitRadiologyID, _serviceUnitRadiologyID2;
        private string _paramedicIdDokterLuar;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.FilmConsumptionEntry;

            _paramedicIdDokterLuar = AppSession.Parameter.ParamedicIdDokterLuar;

            if (!IsPostBack)
            {
                LoadTransactionHeader();
                TransChargesItems = null;
                Session["collTransChargesItemComp" + Request.UserHostName] = null;
                LoadTransChargesItemComp();
            }
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
            var sex = new AppStandardReferenceItem();
            if (sex.LoadByPrimaryKey("GenderType", pat.Sex))
                txtSex.Text = sex.ItemName;

            txtRadiologyNo.Text = pat.DiagnosticNo;

            PopulatePatientImage(pat.PatientID, pat.Sex);

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

            txtChiefComplaint.Text = reg.Complaint;
            txtAnamnesis.Text = reg.Anamnesis;
            txtInitialDiagnose.Text = reg.InitialDiagnose;
            txtPlanning.Text = reg.MedicationPlanning;

            var tx = new TransCharges();
            if (tx.LoadByPrimaryKey(Request.QueryString["joNo"]))
            {
                if (!string.IsNullOrEmpty(tx.PhysicianSenders))
                    txtPhysicianSenders.Text = tx.PhysicianSenders;
                else
                {
                    var parId = reg.ParamedicID ?? string.Empty;
                    if (parId == _paramedicIdDokterLuar)
                        txtPhysicianSenders.Text = reg.PhysicianSenders;
                    else
                    {
                        var par = new Paramedic();
                        par.LoadByPrimaryKey(parId);
                        txtPhysicianSenders.Text = par.ParamedicName;
                    }
                }

                grdTransChargesItem.Columns[1].Visible = (tx.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID 
                    || tx.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2 
                    || AppSession.Parameter.ServiceUnitRadiologyIdArray.Contains(tx.ToServiceUnitID)
                    || AppSession.Parameter.ServiceUnitRadiologyIDs.Contains(tx.ToServiceUnitID)); //ExposureFactor
            }
            else
                grdTransChargesItem.Columns[1].Visible = false;
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

            Session["collTransChargesItemComp" + Request.UserHostName] = collComp;
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
        }

        public override bool OnButtonOkClicked()
        {
            var collComp = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName];
            
            var transChargesItems = TransChargesItems;

            var charges = new TransCharges();
            charges.LoadByPrimaryKey(Request.QueryString["joNo"]);
            charges.PhysicianSenders = txtPhysicianSenders.Text;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(charges.ToServiceUnitID);

            var reg = new Registration();
            reg.LoadByPrimaryKey(charges.RegistrationNo);

            var grrID = reg.GuarantorID;
            if (grrID == AppSession.Parameter.SelfGuarantor)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                if (!string.IsNullOrEmpty(pat.MemberID))
                    grrID = pat.MemberID;
            }

            var guar = new Guarantor();
            guar.LoadByPrimaryKey(grrID);

            foreach (var a in TransChargesItems)
            {
                var collt = Helper.Tariff.GetItemTariffComponentCollection((new DateTime()).NowAtSqlServer().Date, guar.SRTariffType, reg.ChargeClassID, a.ItemID);
                if (!collt.Any())
                    collt = Helper.Tariff.GetItemTariffComponentCollection((new DateTime()).NowAtSqlServer().Date, AppSession.Parameter.DefaultTariffType,
                        AppSession.Parameter.DefaultTariffClass, a.ItemID);
                var colltx = collt.Where(c => TariffParamedic().Contains(c.TariffComponentID));

                if (colltx.Any())
                {
                    var collCompx =
                        collComp.Where(c => !string.IsNullOrEmpty(c.ParamedicID) && TariffParamedic().Contains(c.TariffComponentID) &&
                            c.TransactionNo == a.TransactionNo && c.SequenceNo == a.SequenceNo);
                    if (!collCompx.Any())
                    {
                        ShowInformationHeader("Physician for : " + a.ItemName + " have not been filled. Please correct the data.");
                        return false;
                    }
                }
            }

            //int? x;
            using (var trans = new esTransactionScope())
            {
                transChargesItems.Save();
                collComp.Save();
                charges.Save();

                if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment)
                {
                    var x = ParamedicFeeTransChargesItemCompSettled.UpdateSettled(charges, collComp, AppSession.UserLogin.UserID);

                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.SetFeeByTCIC(collComp, AppSession.UserLogin.UserID);
                    feeColl.Save();
                    //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                    //feeColl.Save();
                }

                #region Interop RIS/PACS
                if (AppSession.Parameter.IsUsingRisPacsInterop && (AppSession.Parameter.HealthcareInitialAppsVersion == "RSBK"))
                {
                    if (charges.ToServiceUnitID == _serviceUnitRadiologyID || charges.ToServiceUnitID == _serviceUnitRadiologyID2)
                    {
                        var patient = new Patient();
                        patient.LoadByPrimaryKey(reg.PatientID);

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
                                    order_control = "RO",
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
                                    operator_name = AppSession.UserLogin.UserName,
                                    exam_urgent = t.IsCito.HasValue ? (t.IsCito.Value ? "1" : "0") : "0",
                                    issuer = "H",
                                    if_flag = 0,
                                    result = 0,
                                    urllink = string.Empty
                                };
                            }).ToList();

                            var ris = new Common.Worklist.RSBK.Service();
                            if (!ris.UpdateExamOrder(list))
                            {
                                //args.MessageText = "Canceling Order Failed Because This Transaction Number Status Is Already Complete In PACS";
                                //args.IsCancel = true;
                                //return;
                            }
                            foreach (var tci in transChargesItems.Where(t => (t.IsOrderRealization ?? false) && !(t.IsVoid ?? false)))
                            {
                                tci.IsSendToLIS = true;
                            }
                        }
                    }
                }
                #endregion

                //Commit if success, Rollback if failed
                trans.Complete();
                
            }
            
            return true;
        }

        private TransChargesItemCollection TransChargesItems
        {
            get
            {
                if (Session["collTransChargesItem" + Request.UserHostName] != null)
                    return (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];

                var coll = new TransChargesItemCollection();

                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var header = new TransChargesQuery("c");
                var reference = new TransChargesItemQuery("d");

                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = query.ChargeQuantity * ((query.Price - query.DiscountAmount) + query.CitoAmount);

                query.Select
                    (
                        query,
                        total.As("refToTransChargesItem_Total"),
                        item.ItemName.As("refToItem_ItemName"),
                        header.ToServiceUnitID.As("refToTransCharges_ToServiceUnitID"),
                        item.SRItemType.As("refToItem_SRItemType"),
                        reference.TransactionNo.As("refTo_TransactionCorrectionNo")
                    );

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(reference).On(query.TransactionNo == reference.ReferenceNo &&
                                             query.SequenceNo == reference.ReferenceSequenceNo);

                query.Where
                    (
                        query.TransactionNo == Request.QueryString["joNo"],
                        query.IsVoid == false,
                        query.IsBillProceed == true
                    );
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);
                Session["collTransChargesItem" + Request.UserHostName] = coll;

                return coll;
            }
            set
            { Session["collTransChargesItem" + Request.UserHostName] = value; }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument == "rebind")
                grdTransChargesItem.Rebind();
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

        protected void cboPhysicianSendersID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
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
                    imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : (sex == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");
                }
            }
            else
                imgPatientPhoto.ImageUrl = sex == "M" ? "~/Images/Asset/Patient/ManVector.png" : (sex == "F" ? "~/Images/Asset/Patient/WomanVector.png" : "~/Images/Asset/Patient/HumanVector.png");

        }
        #endregion

        private string GetItemName(string itemId)
        {
            var item = new Item();
            return item.LoadByPrimaryKey(itemId) ? item.ItemName : string.Empty;
        }
    }
}
