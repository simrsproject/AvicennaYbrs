using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class PrescriptionReviewList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            ProgramID = AppConstant.Program.PrecriptionReview;

            txtPrescriptionDate.DateInput.DisplayDateFormat = AppConstant.DisplayFormat.DateShortMonth;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery();
                query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.ClusterPatient,
                                                           AppConstant.RegistrationType.EmergencyPatient,
                                                           AppConstant.RegistrationType.OutPatient,
                                                           AppConstant.RegistrationType.InPatient,
                                                           AppConstant.RegistrationType.MedicalCheckUp));


                query.Where(query.IsActive == true);

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Yes", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("No", "2"));

                txtPrescriptionDate.SelectedDate = DateTime.Now;

                StandardReference.InitializeIncludeSpace(cboPrescriptionSRFloor, AppEnum.StandardReference.Floor);
                StandardReference.InitializeIncludeSpace(cboSRPrescriptionCategory, AppEnum.StandardReference.PrescriptionCategory);

                var utype = new AppStandardReferenceItemCollection();
                utype.Query.Where(utype.Query.StandardReferenceID == "UserType", utype.Query.ItemID.In("DTR", "NRS"));
                utype.LoadAll();
                cboPrescriptionCreatedBy.Items.Add(new RadComboBoxItem("", ""));
                foreach (var t in utype)
                {
                    cboPrescriptionCreatedBy.Items.Add(new RadComboBoxItem(t.ItemName, t.ItemID));
                }
                ComboBox.PopulateWithServiceUnitForTransaction(cboDispensaryID, TransactionCode.Prescription, true);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void grdPrescription_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "setStatus")
            {
                var cmd = e.CommandArgument.ToString().Split('|');
                var pres = new TransPrescription();
                if (pres.LoadByPrimaryKey(cmd[0]) && cmd.Length > 1)
                {
                    switch (cmd[1])
                    {
                        case "complete":
                            {
                                pres.CompleteDateTime = DateTime.Now;
                                pres.Save();
                                grdPrescription.Rebind();
                                break;
                            }
                        case "deliver":
                            {
                                pres.DeliverDateTime = DateTime.Now;
                                pres.Save();
                                grdPrescription.Rebind();
                                break;
                            }
                    }
                }
            }
        }

        protected void grdPrescription_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdPrescription.DataSource = TransPrescriptions;
                grdPrescription.MasterTableView.GroupsDefaultExpanded = true;
            }
        }

        private DataTable TransPrescriptions
        {
            get
            {
                var presc = new TransPrescriptionQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var medic = new ParamedicQuery("e");
                var grr = new GuarantorQuery("f");
                var unitfm = new ServiceUnitQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");
                var ctg = new AppStandardReferenceItemQuery("ctg");
                var revBy = new AppUserQuery("rb");

                //presc.es.Distinct = true;
                presc.Select
                    (
                        presc.PrescriptionDate,
                        presc.PrescriptionNo,
                        presc.RegistrationNo,
                        reg.PatientID,
                        patient.MedicalNo,
                        patient.PatientName,
                        unit.ServiceUnitName.As("FromServiceUnit"),
                        unitfm.ServiceUnitName,
                        medic.ParamedicName,
                        grr.GuarantorName,
                        presc.IsApproval,
                        presc.IsBillProceed,
                        presc.IsVoid,
                        "<CAST(0 AS BIT) AS IsPaid>",
                        presc.IsProceedByPharmacist,
                        presc.ApprovalDateTime,
                        presc.CompleteDateTime,
                        presc.DeliverDateTime,
                        "<0 AS Status>",
                        sal.ItemName.As("SalutationName"),
                        presc.SRPrescriptionCategory,
                        ctg.ItemName.As("PrescriptionCategoryName"),
                        presc.IsReviewed,
                        presc.ReviewedDateTime,
                        revBy.UserName.As("ReviewedByUserName"),
                        revBy.LicenseNo
                    );
                presc.GroupBy
                    (
                        presc.PrescriptionDate,
                        presc.PrescriptionNo,
                        presc.RegistrationNo,
                        reg.PatientID,
                        patient.MedicalNo,
                        patient.PatientName,
                        unit.ServiceUnitName,
                        unitfm.ServiceUnitName,
                        medic.ParamedicName,
                        grr.GuarantorName,
                        presc.IsApproval,
                        presc.IsBillProceed,
                        presc.IsVoid,
                        presc.IsProceedByPharmacist,
                        presc.ApprovalDateTime,
                        presc.CompleteDateTime,
                        presc.DeliverDateTime,
                        sal.ItemName,
                        presc.SRPrescriptionCategory,
                        ctg.ItemName,
                        presc.IsReviewed,
                        presc.ReviewedDateTime,
                        revBy.UserName,
                        revBy.LicenseNo
                    );

                presc.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo);
                presc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                presc.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                presc.LeftJoin(medic).On(presc.ParamedicID == medic.ParamedicID);
                presc.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                presc.InnerJoin(unitfm).On(presc.ServiceUnitID == unitfm.ServiceUnitID);
                presc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                presc.LeftJoin(ctg).On(ctg.StandardReferenceID == "PrescriptionCategory" & presc.SRPrescriptionCategory == ctg.ItemID);
                presc.LeftJoin(revBy).On(presc.ReviewedByUserID == revBy.UserID);

                presc.Where(presc.IsFromSOAP == true,
                            presc.IsPrescriptionReturn == false,
                            presc.Or(presc.IsUnitDosePrescription.IsNull(), presc.IsUnitDosePrescription == false));

                if (txtPrescriptionNo.Text != string.Empty)
                    presc.Where(presc.PrescriptionNo == txtPrescriptionNo.Text);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    presc.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                }

                if (cboParamedicID.SelectedValue != string.Empty)
                    presc.Where(presc.ParamedicID == cboParamedicID.SelectedValue);

                if (!string.IsNullOrEmpty(cboDispensaryID.SelectedValue))
                    presc.Where(presc.ServiceUnitID == cboDispensaryID.SelectedValue);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        presc.Where(
                            presc.Or(
                                string.Format("<a.RegistrationNo = '{0}' OR >", searchReg),
                                string.Format("<c.MedicalNo = '{0}' OR >", searchReg),
                                string.Format("<c.OldMedicalNo = '{0}'>", searchReg),
                                string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    else
                        presc.Where(
                            presc.Or(
                                string.Format("<a.RegistrationNo = '{0}' OR >", searchReg),
                                string.Format("<c.MedicalNo = '{0}' OR >", searchReg),
                                string.Format("<c.OldMedicalNo = '{0}'>", searchReg),
                                string.Format("< OR c.MedicalNo LIKE '%{0}%'>", searchReg),
                                string.Format("< OR c.OldMedicalNo LIKE '%{0}%'>", searchReg)
                                )
                            );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    presc.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                if (!string.IsNullOrEmpty(cboReviewed.SelectedValue))
                {
                    if (cboReviewed.SelectedValue == "1")
                        presc.Where(presc.IsReviewed == true);
                    else
                        presc.Where(presc.Or(presc.IsReviewed.IsNull(), presc.IsReviewed == false));
                }

                if (!string.IsNullOrEmpty(cboApproved.SelectedValue))
                {
                    if (cboApproved.SelectedValue == "1")
                        presc.Where(presc.IsApproval == true);
                    else
                        presc.Where(presc.Or(presc.IsApproval.IsNull(), presc.IsApproval == false));
                }

                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    if (cboStatus.SelectedValue == "1")
                        presc.Where(presc.IsProceedByPharmacist == true);
                    else
                        presc.Where(presc.Or(presc.IsProceedByPharmacist.IsNull(), presc.IsProceedByPharmacist == false));
                }

                if (!string.IsNullOrEmpty(cboDeliveryStatus.SelectedValue))
                {
                    switch (cboDeliveryStatus.SelectedValue)
                    {
                        case "1":
                            {
                                presc.Where(presc.CompleteDateTime.IsNull());
                                break;
                            }
                        case "2":
                            {
                                presc.Where(presc.CompleteDateTime.IsNotNull(), presc.DeliverDateTime.IsNull());
                                break;
                            }
                        case "3":
                            {
                                presc.Where(presc.DeliverDateTime.IsNotNull());
                                break;
                            }
                    }
                }

                if (!string.IsNullOrEmpty(cboPrescriptionSRFloor.SelectedValue))
                    presc.Where(presc.SRFloor == cboPrescriptionSRFloor.SelectedValue);

                if (!string.IsNullOrEmpty(cboSRPrescriptionCategory.SelectedValue))
                    presc.Where(presc.SRPrescriptionCategory == cboSRPrescriptionCategory.SelectedValue);

                if (!string.IsNullOrEmpty(txtBarcode.Text))
                {
                    if (!txtPrescriptionDate.IsEmpty)
                    {
                        presc.Where(presc.Or(
                        presc.PrescriptionNo == txtBarcode.Text,
                        presc.And(
                            presc.PrescriptionDate >= txtPrescriptionDate.SelectedDate,
                            presc.PrescriptionDate < txtPrescriptionDate.SelectedDate.Value.AddDays(1)
                            )
                        ));
                    }
                    else
                    {
                        presc.Where(presc.PrescriptionNo == txtBarcode.Text);
                    }
                }
                else
                {
                    if (!txtPrescriptionDate.IsEmpty)
                    {
                        presc.Where(presc.PrescriptionDate >= txtPrescriptionDate.SelectedDate, presc.PrescriptionDate < txtPrescriptionDate.SelectedDate.Value.AddDays(1));
                    }
                }

                presc.es.Top = AppSession.Parameter.MaxResultRecord;

                presc.OrderBy(presc.ApprovalDateTime.Descending, presc.PrescriptionNo.Descending);

                DataTable dtbl = presc.LoadDataTable();


                return dtbl;
            }
        }

        protected void grdPrescription_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var prescriptionNo = e.DetailTableView.ParentItem.GetDataKeyValue("PrescriptionNo").ToString();
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemI = new ItemQuery("c");

            var emb = new EmbalaceQuery("x");
            var cons = new ConsumeMethodQuery("y");

            var total = new esQueryItem(query, "Total", esSystemType.Decimal);
            total = query.ResultQty * (query.Price - query.DiscountAmount);

            query.Select
                (
                    query,
                    qItem.ItemName.As("ItemName"),
                    qItemI.ItemName.As("ItemInterventionName"),
                    total.As("Total"),
                    "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                    emb.EmbalaceLabel.As("EmbalaceLabel"),
                    cons.SRConsumeMethodName.As("SRConsumeMethodName")
                );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(qItemI).On(query.ItemInterventionID == qItemI.ItemID);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.Where(query.PrescriptionNo == prescriptionNo);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdPrescription.Rebind();
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            grdPrescription.Rebind();
            txtBarcode.Text = "";
            txtBarcode.Focus();
        }
        protected string GetColorCategory(object srPrescriptionCategory)
        {
            string color = "White";
            switch (srPrescriptionCategory.ToString())
            {
                case "LVSV":
                    {
                        color = "Red";
                        break;
                    }
                case "NEWP":
                    {
                        color = "Green";
                        break;
                    }
                case "NEWT":
                    {
                        color = "GreenYellow";
                        break;
                    }
                case "TUTB":
                    {
                        color = "BlueViolet";
                        break;
                    }
                case "DISC":
                    {
                        color = "Blue";
                        break;
                    }
            }

            return color;
        }

        protected void grdPrescription_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (true.Equals(item.GetDataKeyValue("IsVoid")))
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
                if (true.Equals(item.GetDataKeyValue("IsReviewed").ToBoolean()))
                {
                    item.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#92D050");
                }

            }
        }
    }
}
