namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSCH
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for SurgeyRegistrationReceipt.
    /// </summary>
    public partial class SurgeyRegistrationReceipt : Report
    {
        public SurgeyRegistrationReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            var book = new ServiceUnitBookingQuery("a");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var cls = new ClassQuery("f");
            var room = new ServiceRoomQuery("e");
            var medic1 = new ParamedicQuery("g");
            var medic2 = new ParamedicQuery("h");

            book.Select(
                string.Format("<(SELECT HealthcareName FROM Healthcare AS h WHERE h.HealthcareID = '{0}') AS HealthcareName>", printJobParameters[0].ValueString),
                string.Format("<(SELECT City FROM Healthcare AS h WHERE h.HealthcareID = '{0}') AS City>", printJobParameters[0].ValueString),
                pat.MedicalNo,
                "<ISNULL(b.RegistrationNo, '') AS RegistrationNo>",
                book.BookingNo,
                pat.PatientName,
                "<[dbo].[fnCalculateAge](c.DateOfBirth) AS Age>",
                "<CASE WHEN c.Sex = 'M' THEN 'Laki-laki' ELSE 'Perempuan' END AS Sex>",
                unit.ServiceUnitName.Coalesce("''"),
                cls.ClassName.Coalesce("''"),
                room.RoomName.Coalesce("''"),
                reg.BedID.Coalesce("''"),
                pat.Address,
                book.BookingDateTimeFrom,
                book.Diagnose,
                book.Notes,
                medic1.ParamedicName.As("ParamedicName1"),
                medic2.ParamedicName.As("ParamedicName2"),
                "<(SELECT UserName FROM AppUser WHERE UserID = a.LastCreateByUserID) AS UserName>",
                pat.PatientID
                );

            book.LeftJoin(reg).On(book.RegistrationNo == reg.RegistrationNo);
            book.InnerJoin(pat).On(book.PatientID == pat.PatientID);
            book.LeftJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            book.LeftJoin(cls).On(reg.ClassID == cls.ClassID);
            book.LeftJoin(room).On(reg.RoomID == room.RoomID);
            book.LeftJoin(medic1).On(book.ParamedicID == medic1.ParamedicID);
            book.LeftJoin(medic2).On(book.ParamedicIDAnestesi == medic2.ParamedicID);

            book.Where(book.BookingNo == printJobParameters[1].ValueString);

            var tbl = book.LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                if (string.IsNullOrEmpty(row["ServiceUnitName"].ToString()))
                {
                    reg = new RegistrationQuery("a");
                    unit = new ServiceUnitQuery("b");
                    room = new ServiceRoomQuery("c");
                    cls = new ClassQuery("d");

                    reg.es.Top = 1;
                    reg.Select(
                        unit.ServiceUnitName,
                        room.RoomName,
                        cls.ClassName,
                        reg.BedID
                        );
                    reg.LeftJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                    reg.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    reg.LeftJoin(cls).On(reg.ClassID == cls.ClassID);
                    reg.Where(
                        reg.PatientID == row["PatientID"].ToString(),
                        reg.IsConsul == false,
                        reg.IsVoid == false
                        );
                    reg.OrderBy(reg.RegistrationDate.Descending);

                    var dtb = reg.LoadDataTable();

                    if (dtb.Rows.Count > 0)
                    {
                        row["ServiceUnitName"] = dtb.Rows[0]["ServiceUnitName"].ToString();
                        row["RoomName"] = dtb.Rows[0]["RoomName"].ToString();
                        row["ClassName"] = dtb.Rows[0]["ClassName"].ToString();
                        row["BedID"] = dtb.Rows[0]["BedID"].ToString();
                    }
                }
            }

            DataSource = tbl;
        }
    }
}