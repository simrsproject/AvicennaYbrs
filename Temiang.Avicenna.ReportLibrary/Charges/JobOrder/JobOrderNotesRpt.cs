namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using Temiang.Avicenna.BusinessObject;
    using System;
    using Temiang.Dal.DynamicQuery;

    /// <summary>
    /// Summary description for JobOrderNotesRpt.
    /// </summary>
    public partial class JobOrderNotesRpt : Telerik.Reporting.Report
    {
        public JobOrderNotesRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);

            var hd = new TransChargesQuery("a");
            var dt = new TransChargesItemQuery("b");
            var reg = new RegistrationQuery("c");
            var unit = new ServiceUnitQuery("d");
            var room = new ServiceRoomQuery("e");
            var unit2 = new ServiceUnitQuery("f");
            var pasien = new PatientQuery("g");
            var item = new ItemQuery("h");
            var medic = new ParamedicQuery("i");
            var appstd = new AppStandardReferenceItemQuery("j");
            dt.InnerJoin(hd).On(hd.TransactionNo == dt.TransactionNo);
            dt.InnerJoin(reg).On(reg.RegistrationNo == hd.RegistrationNo);
            dt.InnerJoin(unit).On(unit.ServiceUnitID == reg.ServiceUnitID);
            dt.LeftJoin(room).On(room.RoomID == reg.RoomID);
            dt.InnerJoin(unit2).On(unit2.ServiceUnitID == hd.ToServiceUnitID);
            dt.InnerJoin(pasien).On(pasien.PatientID == reg.PatientID);
            dt.InnerJoin(item).On(item.ItemID == dt.ItemID);
            dt.InnerJoin(medic).On(reg.ParamedicID == medic.ParamedicID);
            dt.LeftJoin(appstd).On
                (
                    hd.SRTypeResult == appstd.ItemID &
                    appstd.StandardReferenceID == "TypeResult"
                );
            dt.Select
                (
                    hd.RegistrationNo,
                    pasien.MedicalNo,
                    pasien.DiagnosticNo,
                    pasien.PatientName,
                    @"<CASE WHEN g.Sex = 'M' THEN 'Lk' ELSE 'Pr' END AS Sex>",  
                    //pasien.Sex,
                    pasien.Address,
                    pasien.PhoneNo,
                    pasien.MobilePhoneNo,
                    @"<CONVERT(VARCHAR(10), g.DateOfBirth, 105) + ' (' + CAST(c.AgeInYear AS VARCHAR) + 'th ' + CAST(c.AgeInMonth AS VARCHAR) + 'bl ' + CAST(c.AgeInDay AS VARCHAR) + ' hr)' AS Age>",                   
                    //(reg.AgeInYear.Cast(esCastType.String) + "th " + reg.AgeInMonth.Cast(esCastType.String) + "bl " + 
                    //    reg.AgeInDay.Cast(esCastType.String) + "hr").As("Age"),
                    reg.InitialDiagnose,
                    unit.ServiceUnitName.As("ClusterName"),

                    "<CASE WHEN ISNULL(c.PhysicianSenders, '') = '' THEN i.ParamedicName ELSE c.PhysicianSenders END AS ParamedicName>",
                    //medic.ParamedicName,
                    medic.PhoneNo.As("DrPhone"),
                    medic.MobilePhoneNo.As("DrMobile"),

                    hd.TransactionNo,
                    hd.TransactionDate,
                    unit2.ServiceUnitName.ToUpper().As("ClusterOrderName").Trim(),

                    item.ItemName,
                    dt.ChargeQuantity,
                    dt.Notes,
                    hd.PhysicianSenders,

                    appstd.ItemName.As("Result"),
                    hd.Notes.As("HdNotes")
                );

            dt.Where
                (
                    hd.IsApproved == true,
                    hd.IsVoid == false,
                    dt.TransactionNo == printJobParameters[0].ValueString,
                    dt.IsVoid == false
                );


            dt.OrderBy(dt.SequenceNo.Ascending);

            this.DataSource = dt.LoadDataTable();
        }
    }
}