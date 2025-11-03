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
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class JobOrderConfirmedDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.JobOrderConfirmed;

            if (!IsPostBack)
            {
                LoadTransactionHeader();
                TransChargesItems = null;
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
            txtPatientName.Text = pat.PatientName;
            optSexFemale.Enabled = (pat.Sex == "F");
            optSexFemale.Checked = (pat.Sex == "F");
            optSexMale.Enabled = (pat.Sex == "M");
            optSexMale.Checked = (pat.Sex == "M");
            txtJobOrderNo.Text = Request.QueryString["joNo"];

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);
            txtServiceUnitID.Text = reg.ServiceUnitID;
            lblServiceUnitName.Text = unit.ServiceUnitName;

            var room = new ServiceRoom();
            room.LoadByPrimaryKey(reg.str.RoomID);
            txtRoomID.Text = reg.str.RoomID;
            lblRoomName.Text = room.str.RoomName;

            var c = new Class();
            c.LoadByPrimaryKey(reg.ClassID);
            txtClassID.Text = reg.ClassID;
            lblClassName.Text = c.ClassName;

            var med = new Paramedic();
            med.LoadByPrimaryKey(reg.str.ParamedicID);
            txtParamedicID.Text = reg.str.ParamedicID;
            lblParamedicName.Text = med.str.ParamedicName;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            txtGuarantorID.Text = reg.GuarantorID;
            lblGuarantorName.Text = grr.GuarantorName;

            // From table EpisodeSOAPE
            var soapColl = new EpisodeSOAPECollection();
            soapColl.Query.Where(
                soapColl.Query.RegistrationNo == reg.RegistrationNo &&
                soapColl.Query.IsVoid == false,
                soapColl.Query.Or(soapColl.Query.Imported.IsNull(), soapColl.Query.Imported == false)
                );
            soapColl.LoadAll();

            foreach (var soap in soapColl)
            {
                txtS.Text = string.IsNullOrEmpty(soap.Subjective.Trim()) ? txtS.Text : txtS.Text + soap.Subjective.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
                txtO.Text = string.IsNullOrEmpty(soap.Objective.Trim()) ? txtO.Text : txtO.Text + soap.Objective.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
                txtA.Text = string.IsNullOrEmpty(soap.Assesment.Trim()) ? txtA.Text : txtA.Text + soap.Assesment.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
                txtP.Text = string.IsNullOrEmpty(soap.Planning.Trim()) ? txtP.Text : txtP.Text + soap.Planning.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtS.Text) && string.IsNullOrEmpty(txtO.Text) && string.IsNullOrEmpty(txtA.Text) && string.IsNullOrEmpty(txtP.Text))
            {
                //From Table RegistrationInfoMedic
                var rimColl = new RegistrationInfoMedicCollection();
                rimColl.Query.Where(
                    rimColl.Query.RegistrationNo == reg.RegistrationNo,
                    rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
                    );
                rimColl.LoadAll();

                foreach (var rim in rimColl)
                {
                    txtS.Text = string.IsNullOrEmpty(rim.Info1.Trim()) ? txtS.Text : txtS.Text + rim.Info1.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
                    txtO.Text = string.IsNullOrEmpty(rim.Info2.Trim()) ? txtO.Text : txtO.Text + rim.Info2.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
                    txtA.Text = string.IsNullOrEmpty(rim.Info3.Trim()) ? txtA.Text : txtA.Text + rim.Info3.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
                    txtP.Text = string.IsNullOrEmpty(rim.Info4.Trim()) ? txtP.Text : txtP.Text + rim.Info4.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
                }
            }


            var tx = new TransCharges();
            if (tx.LoadByPrimaryKey(Request.QueryString["joNo"]))
                txtNotes.Text = tx.Notes;

            txtConfirmedDate.SelectedDate = (new DateTime()).NowAtSqlServer();



            PopulatePatientImage(pat.PatientID);
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = TransChargesItems;
        }

        public override bool OnButtonOkClicked()
        {
            if (string.IsNullOrEmpty(txtConfirmedBy.Text))
            {
                ShowInformationHeader("Confirmed By required.");
                return false;
            }

            var seqs = TransChargesItems.Where(item => !IsOrderConfirmed(item.TransactionNo, item.SequenceNo) &&
                                                       (item.IsBillProceed ?? false) &&
                                                       item.IsVoid == false);

            foreach (var detail in seqs)
            {
                detail.IsOrderConfirmed = true;
                detail.IsPaymentConfirmed = true;
                detail.PaymentConfirmedBy = txtConfirmedBy.Text;
                detail.PaymentConfirmedDateTime = txtConfirmedDate.SelectedDate;
                detail.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
                detail.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
            }

            using (var trans = new esTransactionScope())
            {
                TransChargesItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        private TransChargesItemCollection TransChargesItems
        {
            get
            {
                if (Session["collTransChargesItemConfirmed" + Request.UserHostName] != null)
                    return (TransChargesItemCollection)Session["collTransChargesItemConfirmed" + Request.UserHostName];

                var coll = new TransChargesItemCollection();

                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var header = new TransChargesQuery("d");
                var tci = new TransChargesItemQuery("e");

                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = query.ChargeQuantity * ((query.Price - query.DiscountAmount) + query.CitoAmount);

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo, tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select
                    (
                        query,
                        total.As("refToTransChargesItem_Total"),
                        item.ItemName.As("refToItem_ItemName"),
                        header.ToServiceUnitID.As("refToTransCharges_ToServiceUnitID"),
                        item.SRItemType.As("refToItem_SRItemType"),
                        @"<ISNULL(a.IsPaymentConfirmed, 0) AS 'refTo_IsPaymentConfirmed'>",
                        "<'' as refTo_PrevOrder>"
                    );

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);

                query.Where
                    (
                        query.TransactionNo == Request.QueryString["joNo"],
                        query.IsVoid == false,
                        query.IsBillProceed == false,
                        query.NotExists(tci)
                    );
                query.Where(query.ParentNo == string.Empty);
                query.Where(query.ChargeQuantity > 0);
                query.Where(query.Or(query.IsPaymentConfirmed.IsNull(), query.IsPaymentConfirmed == false));

                query.OrderBy(query.SequenceNo.Ascending);
                //DataTable dtb = query.LoadDataTable();

                coll.Load(query);
                coll.SetPrevOrder(RegistrationNo, AppSession.Parameter.IntervalOrderWarning);

                Session["collTransChargesItemConfirmed" + Request.UserHostName] = coll;

                return coll;
            }
            set
            { Session["collTransChargesItemConfirmed" + Request.UserHostName] = value; }
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
                            entity.IsOrderConfirmed = false;
                            entity.IsPaymentConfirmed = false;
                            entity.PaymentConfirmedBy = string.Empty;
                            entity.str.PaymentConfirmedDateTime = string.Empty;
                            entity.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
                            entity.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
                            entity.IsVoid = true;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                        break;
                    case "verify":
                        entity.IsOrderConfirmed = true;
                        entity.IsPaymentConfirmed = true;
                        entity.PaymentConfirmedBy = txtConfirmedBy.Text;
                        entity.PaymentConfirmedDateTime = txtConfirmedDate.SelectedDate;
                        entity.LastPaymentConfirmedByUserID = AppSession.UserLogin.UserID;
                        entity.LastPaymentConfirmedDateTime = (new DateTime()).NowAtSqlServer();
                        break;
                }
                grdTransChargesItem.Rebind();
            }
        }

        private static bool IsOrderConfirmed(string transactionNo, string sequenceNo)
        {
            var entity = new TransChargesItem();
            entity.LoadByPrimaryKey(transactionNo, sequenceNo);
            return (entity.IsPaymentConfirmed ?? false);
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

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
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
                    imgPatientPhoto.ImageUrl = optSexMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = optSexMale.Checked ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }
        #endregion
    }
}
