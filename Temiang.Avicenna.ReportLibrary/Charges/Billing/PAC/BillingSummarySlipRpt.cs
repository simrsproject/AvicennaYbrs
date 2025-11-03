using System;
using System.Linq;

namespace Temiang.Avicenna.ReportLibrary.Charges.PAC
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for BillingSummary.
    /// </summary>
    public partial class BillingSummarySlipRpt : Report
    {
        public BillingSummarySlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');

            decimal? downPayment = printJobParameters.FindByParameterName("DownPayment").ValueNumeric;
            decimal? paymentAmount = printJobParameters.FindByParameterName("PaymentAmount").ValueNumeric;
            decimal? paymentDiscount = printJobParameters.FindByParameterName("PaymentDiscount").ValueNumeric;
            if (paymentAmount > 0)
                paymentAmount -= downPayment;

            #region charges
            CostCalculationQuery cost = new CostCalculationQuery("a");
            RegistrationQuery reg = new RegistrationQuery("b");
            PatientQuery patient = new PatientQuery("c");
            ParamedicQuery medic1 = new ParamedicQuery("d");
            ServiceRoomQuery room = new ServiceRoomQuery("f");
            ClassQuery cls = new ClassQuery("g");
            GuarantorQuery grr = new GuarantorQuery("h");
            ItemQuery item = new ItemQuery("i");
            ItemGroupQuery group = new ItemGroupQuery("j");

            TransChargesQuery charges = new TransChargesQuery("k");
            TransChargesItemQuery chargesItem = new TransChargesItemQuery("l");
            ServiceUnitQuery unit2 = new ServiceUnitQuery("n");
            ItemServiceQuery svc = new ItemServiceQuery("o");

            cost.Select
                (
                // header
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.DateOfBirth,
                    medic1.ParamedicName.As("ParamedicNameHeader"),
                    reg.RegistrationDate,
                    reg.RegistrationTime,
                    room.RoomName.Coalesce("''"),
                    cls.ClassName,
                    reg.BedID.Coalesce("''"),
                    grr.GuarantorName,
                    reg.DischargeDate,

                    // footer
                    reg.AdministrationAmount,
                    reg.PlavonAmount,
                    "<" + downPayment + " AS DownPayment>",
                    "<" + paymentAmount + " AS 'PaymentAmount'>",
                    "<" + paymentDiscount + " AS 'PaymentDiscount'>",

                    // detail
                    group.ItemGroupName,
                    item.ItemName,
                //charges.TransactionDate,
                    charges.ExecutionDate.As("TransactionDate"),
                    unit2.ServiceUnitName,
                    chargesItem.Price,
                    chargesItem.ChargeQuantity,
                    chargesItem.DiscountAmount,
                    (chargesItem.Price - (chargesItem.DiscountAmount / chargesItem.ChargeQuantity)).As("Price2"),
                    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                    chargesItem.TransactionNo,
                    chargesItem.SequenceNo,
                    chargesItem.ReferenceNo,
                    chargesItem.ReferenceSequenceNo,
                    item.ItemGroupID,
                    svc.IsPrintWithDoctorName.Coalesce("0"),
                    chargesItem.ItemID
                );

            // header
            cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicName);
            cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
            cost.InnerJoin(cls).On(reg.ChargeClassID == cls.ClassID);
            cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

            // detail
            cost.InnerJoin(charges).On(cost.TransactionNo == charges.TransactionNo & charges.IsVoid == false);
            cost.InnerJoin(chargesItem).On
                (
                    cost.TransactionNo == chargesItem.TransactionNo &
                    cost.SequenceNo == chargesItem.SequenceNo
                );
            cost.InnerJoin(unit2).On(charges.ToServiceUnitID == unit2.ServiceUnitID);
            cost.InnerJoin(item).On(chargesItem.ItemID == item.ItemID);
            cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);
            cost.LeftJoin(svc).On(chargesItem.ItemID == svc.ItemID);

            if (registrationNo.Contains(","))
                cost.Where(cost.RegistrationNo.In(registrationNoList));
            else
                cost.Where(cost.RegistrationNo == registrationNo);

            cost.Where(
                cost.Or(
                    charges.PackageReferenceNo == string.Empty,
                    charges.PackageReferenceNo.IsNull()
                ),
                cost.Or(
                    cost.ParentNo == string.Empty,
                    cost.ParentNo.IsNull()
                    )
                );

            cost.OrderBy
                (
                    cost.RegistrationNo.Ascending,
                    charges.TransactionNo.Ascending,
                    chargesItem.SequenceNo.Ascending
                );

            DataTable table = cost.LoadDataTable(), temp = table.Copy();

            DataView view = temp.DefaultView;
            view.RowFilter = "ReferenceNo <> '' AND ReferenceSequenceNo <> ''";
            temp = view.ToTable();
            view.Dispose();

            foreach (DataRow row in table.Rows)
            {
                if (row["ReferenceNo"].ToString() == string.Empty && row["ReferenceSequenceNo"].ToString() == string.Empty)
                {
                    foreach (DataRow tmp in temp.Rows.Cast<DataRow>().Where(tmp => row["TransactionNo"].ToString() == tmp["ReferenceNo"].ToString() &&
                                                                                   row["SequenceNo"].ToString() == tmp["ReferenceSequenceNo"].ToString()))
                    {
                        row["Total"] = (decimal)row["Total"] + (decimal)tmp["Total"];
                        row["ChargeQuantity"] = (decimal)row["ChargeQuantity"] + (decimal)tmp["ChargeQuantity"];
                    }
                }
            }

            table.AcceptChanges();

            foreach (DataRow row in table.Rows.Cast<DataRow>().Where(row => ((decimal)row["Total"] <= 0) && ((decimal)row["ChargeQuantity"] <= 0)))
            {
                row.Delete();
            }

            table.AcceptChanges();

            foreach (DataRow row in table.Rows)
            {
                if (row["IsPrintWithDoctorName"].ToString() == "1")
                {
                    var comps = new TransChargesItemCompCollection();
                    comps.Query.Where
                        (
                            comps.Query.TransactionNo == row["TransactionNo"].ToString(),
                            comps.Query.SequenceNo == row["SequenceNo"].ToString()
                        );
                    comps.LoadAll();

                    var comp = comps.FindByPrimaryKey(row["TransactionNo"].ToString(), row["SequenceNo"].ToString(),
                        printJobParameters.FindByParameterName("ParamedicTariffComponentID").ValueString);

                    if (comp != null && !string.IsNullOrEmpty(comp.ParamedicID))
                    {
                        var doc = new Paramedic();
                        if (doc.LoadByPrimaryKey(comp.ParamedicID))
                            row["ItemName"] = row["ItemName"].ToString() + " (" + doc.ParamedicName + ")";
                    }
                }
            }

            #endregion

            #region prescription

            cost = new CostCalculationQuery("a");
            reg = new RegistrationQuery("b");
            patient = new PatientQuery("c");
            medic1 = new ParamedicQuery("d");
            room = new ServiceRoomQuery("f");
            cls = new ClassQuery("g");
            grr = new GuarantorQuery("h");
            item = new ItemQuery("i");
            group = new ItemGroupQuery("j");

            TransPrescriptionQuery presc = new TransPrescriptionQuery("k");
            TransPrescriptionItemQuery prescItem = new TransPrescriptionItemQuery("l");
            unit2 = new ServiceUnitQuery("n");

            cost.Select
                (
                // header
                    cost.TransactionNo,
                    reg.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    patient.DateOfBirth,
                    medic1.ParamedicName.As("ParamedicNameHeader"),
                    reg.RegistrationDate,
                    reg.RegistrationTime,
                    room.RoomName.Coalesce("''"),
                    cls.ClassName,
                    reg.BedID.Coalesce("''"),
                    grr.GuarantorName,
                    reg.DischargeDate,

                    // footer
                    reg.AdministrationAmount,
                    reg.PlavonAmount,
                    "<" + downPayment + " as DownPayment>",
                    "<" + paymentAmount + " AS 'PaymentAmount'>",
                    "<" + paymentDiscount + " AS 'PaymentDiscount'>",

                    // detail
                    presc.ReferenceNo,
                    presc.IsPrescriptionReturn,
                    group.ItemGroupName,
                    item.ItemID.As("ItemIDDetail"),
                    item.ItemName,
                    presc.PrescriptionDate.As("TransactionDate"),
                    unit2.ServiceUnitName,
                    prescItem.Price,
                    prescItem.ResultQty.As("ChargeQuantity"),
                    prescItem.DiscountAmount,
                    (prescItem.Price - (prescItem.DiscountAmount / prescItem.ResultQty)).As("Price2"),
                    (cost.PatientAmount + cost.GuarantorAmount).As("Total"),
                    item.ItemGroupID,
                    prescItem.ItemID
                );

            // header
            cost.InnerJoin(reg).On(cost.RegistrationNo == reg.RegistrationNo);
            cost.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            cost.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicName);
            cost.LeftJoin(room).On(reg.RoomID == room.RoomID);
            cost.InnerJoin(cls).On(reg.ChargeClassID == cls.ClassID);
            cost.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

            // detail
            cost.InnerJoin(presc).On(cost.TransactionNo == presc.PrescriptionNo);
            cost.InnerJoin(prescItem).On
                (
                    presc.PrescriptionNo == prescItem.PrescriptionNo &
                    cost.SequenceNo == prescItem.SequenceNo
                );

            cost.InnerJoin(unit2).On(presc.ServiceUnitID == unit2.ServiceUnitID);
            cost.InnerJoin(item).On(prescItem.ItemID == item.ItemID);
            cost.InnerJoin(group).On(item.ItemGroupID == group.ItemGroupID);

            if (registrationNo.Contains(","))
                cost.Where(cost.RegistrationNo.In(registrationNoList));
            else
                cost.Where(cost.RegistrationNo == registrationNo);

            cost.Where(
                cost.Or(
                    cost.ParentNo == string.Empty,
                    cost.ParentNo.IsNull()
                    )
                );

            cost.OrderBy
                (
                    cost.RegistrationNo.Ascending,
                    presc.PrescriptionNo.Ascending,
                    prescItem.SequenceNo.Ascending
                );

            DataTable tbl = cost.LoadDataTable(), temp2 = tbl.Copy();

            DataView vw = temp2.DefaultView;
            vw.RowFilter = "ReferenceNo <> '' AND IsPrescriptionReturn <> 0";
            temp2 = vw.ToTable();
            vw.Dispose();

            foreach (DataRow row in tbl.Rows)
            {
                if (row["ReferenceNo"].ToString() == string.Empty)
                {
                    foreach (DataRow tmp in temp2.Rows.Cast<DataRow>().Where(tmp => row["TransactionNo"].ToString() == tmp["ReferenceNo"].ToString() &&
                                                                                   row["ItemIDDetail"].ToString() == tmp["ItemIDDetail"].ToString()))
                    {
                        row["Total"] = (decimal)row["Total"] + (decimal)tmp["Total"];
                        row["ChargeQuantity"] = (decimal)row["ChargeQuantity"] + (decimal)tmp["ChargeQuantity"];
                    }
                }
            }

            tbl.AcceptChanges();

            foreach (DataRow row in tbl.Rows.Cast<DataRow>().Where(row => ((decimal)row["Total"] <= 0) && ((decimal)row["ChargeQuantity"] <= 0)))
            {
                row.Delete();
            }

            tbl.AcceptChanges();

            //table.Merge(cost.LoadDataTable());
            table.Merge(tbl);

            #endregion

            //package item
            var query = new RegistrationQuery();
            query.es.Distinct = true;
            query.Select(query.PatientID);
            if (registrationNo.Contains(","))
                query.Where(query.RegistrationNo.In(registrationNoList));
            else
                query.Where(query.RegistrationNo == registrationNo);

            //var visites = new TransPaymentItemVisiteCollection();
            //var header = new TransPaymentCollection();
            //visites.Query.Where(
            //    visites.Query.PatientID == query.LoadDataTable().Rows[0][0],
            //    visites.Query.IsClosed == false
            //    );
            //visites.LoadAll();

            var querys = new TransPaymentItemVisiteQuery("a");
            var head = new TransPaymentQuery("b");

            querys.Select(
                head.RegistrationNo, querys.PaymentNo, querys.PatientID, querys.ItemID, querys.VisiteQty, querys.RealizationQty, querys.Price,
                querys.Discount, querys.IsClosed, querys.LastUpdateDateTime, querys.LastUpdateByUserID);
            querys.InnerJoin(head).On(head.PaymentNo == querys.PaymentNo);

            if (registrationNo.Contains(","))
            {
                querys.Where(
                querys.PaymentNo == head.PaymentNo & head.RegistrationNo.In(registrationNoList),
                querys.PatientID == query.LoadDataTable().Rows[0][0],
                querys.IsClosed == false
                );
            }
            else
            {
                querys.Where(
                querys.PaymentNo == head.PaymentNo & head.RegistrationNo == registrationNo,
                querys.PatientID == query.LoadDataTable().Rows[0][0],
                querys.IsClosed == false
                );
            }

            DataTable visites = querys.LoadDataTable();



            foreach (var visite in visites.AsEnumerable())
            {
                var row = table.AsEnumerable().SingleOrDefault(t => t.Field<string>("ItemID") == visite.Field<string>("ItemID"));
                if (row != null)
                {
                    row["ChargeQuantity"] = Convert.ToDecimal(row["ChargeQuantity"]) + Convert.ToDecimal(visite["VisiteQty"]);
                    row["DiscountAmount"] = Convert.ToDecimal(row["DiscountAmount"]) + (Convert.ToDecimal(visite["VisiteQty"]) * (Convert.ToDecimal(visite["Price"]) * Convert.ToDecimal(visite["Discount"]) / 100));
                    row["Total"] = Convert.ToDecimal(row["Total"]) + ((Convert.ToDecimal(visite["VisiteQty"]) * Convert.ToDecimal(visite["Price"])) - (Convert.ToDecimal(visite["VisiteQty"]) * (Convert.ToDecimal(visite["Price"]) * Convert.ToDecimal(visite["Discount"]) / 100)));
                }
            }

            this.DataSource = table;
            table1.DataSource = table;


        }
    }

}