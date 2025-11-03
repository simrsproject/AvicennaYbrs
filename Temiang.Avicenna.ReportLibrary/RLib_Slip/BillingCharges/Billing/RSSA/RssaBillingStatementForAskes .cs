using System.Linq;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System.Data;
    using System;

    /// <summary>
    /// Summary description for BillingSummary.
    /// </summary>
    public partial class RssaBillingStatementForAskes : Report
    {
        public RssaBillingStatementForAskes(string programID, PrintJobParameterCollection printJobParameters)
        {
            {
                /// <summary>
                /// Required for telerik Reporting designer support
                /// </summary>
                InitializeComponent();
                //Helper.InitializeNoLogoBigFont(this.pageHeader);

                DateTime? startDate = printJobParameters.FindByParameterName("StartDate").ValueDateTime;
                DateTime? endDate = printJobParameters.FindByParameterName("EndDate").ValueDateTime;
                string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;
                string seqNo = printJobParameters.FindByParameterName("SeqNo").ValueString;

                if (seqNo == string.Empty)
                {
                    #region AskesCoveredRoom

                    var asCo = new AskesCoveredQuery("a");
                    var reg = new RegistrationQuery("b");
                    var patient = new PatientQuery("c");
                    var medic1 = new ParamedicQuery("d");
                    var room = new ServiceRoomQuery("f");
                    var grr = new GuarantorQuery("h");
                    var cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'001' AS 'itemID'>",
                        @"<'Kamar/Hari' AS 'itemName'>",
                        @"<CASE WHEN a.RoomAmount > 0 THEN
	                a.RoomAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.RoomAmount > 0 THEN
	                a.RoomDays else 0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.RoomAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo));
                    DataTable table = asCo.LoadDataTable();


                    #endregion

                    #region AskesCoveredRoomICCU

                    asCo = new AskesCoveredQuery("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'002' AS 'itemID'>",
                        @"<'Kamar ICCU,ICU/Hari' AS 'itemName'>",
                        @"<CASE WHEN a.ICCUAmount > 0 THEN
	                a.ICCUAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.ICCUAmount > 0 THEN
	                a.ICCUDays else 0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.IccuDays.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo));
                    table.Merge(asCo.LoadDataTable());


                    #endregion

                    #region AskesCoveredRoomHCU

                    asCo = new AskesCoveredQuery("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'003' AS 'itemID'>",
                        @"<'Kamar HCU/Hari' AS 'itemName'>",
                        @"<CASE WHEN a.HCUAmount > 0 THEN
	                a.HCUAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.HCUAmount > 0 THEN
	                a.HCUDays else 0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.HcuDays.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo));
                    table.Merge(asCo.LoadDataTable());


                    #endregion

                    #region AskesCoveredMedicalSupportAmount

                    asCo = new AskesCoveredQuery("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                         @"<'005' AS 'itemID'>",
                        @"<'Penunjang Medis' AS 'itemName'>",
                        @"<CASE WHEN a.MedicalSupportAmount > 0 THEN
	                a.MedicalSupportAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.MedicalSupportAmount > 0 THEN
	                1
                    else
                    0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.MedicalSupportAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo));


                    table.Merge(asCo.LoadDataTable());

                    #endregion

                    #region AskesCoveredHD

                    asCo = new AskesCoveredQuery("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'006' AS 'itemID'>",
                        @"<'Haemodialisa(HD)' AS 'itemName'>",
                        @"<CASE WHEN a.HaemodialiseAmount > 0 THEN
	                a.HaemodialiseAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.HaemodialiseAmount > 0 THEN
	                1
                    else
                    0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.HaemodialiseAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo));

                    table.Merge(asCo.LoadDataTable());
                    #endregion

                    #region AskesCoveredCtScan

                    asCo = new AskesCoveredQuery("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'007' AS 'itemID'>",
                        @"<'Ct Scan' AS 'itemName'>",
                        @"<CASE WHEN a.CtScanAmount > 0 THEN
	                a.CtScanAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.CtScanAmount > 0 THEN
	                1
                    else
                    0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.CtScanAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo));

                    table.Merge(asCo.LoadDataTable());
                    #endregion

                    #region AskesCoveredSurgery

                    asCo = new AskesCoveredQuery("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                       @"<'004' AS 'itemID'>",
                       @"<'Tindakan Operasi' AS 'itemName'>",
                       @"<CASE WHEN a.SurgeryAmount > 0 THEN
	                a.SurgeryAmount else 0
	                END AS 'Price'>",
                      @"<CASE WHEN a.SurgeryAmount > 0 THEN
	                1
                    else
                    0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.SurgeryAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo));


                    table.Merge(asCo.LoadDataTable());
                    #endregion

                    this.DataSource = table;
                    table1.DataSource = table;
                }
                else
                {
                    #region AskesCoveredRoom

                    var asCo = new AskesCovered2Query("a");
                    var reg = new RegistrationQuery("b");
                    var patient = new PatientQuery("c");
                    var medic1 = new ParamedicQuery("d");
                    var room = new ServiceRoomQuery("f");
                    var grr = new GuarantorQuery("h");
                    var cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'001' AS 'itemID'>",
                        @"<'Kamar/Hari' AS 'itemName'>",
                        @"<CASE WHEN a.RoomAmount > 0 THEN
	                a.RoomAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.RoomAmount > 0 THEN
	                a.RoomDays else 0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.RoomAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo), asCo.SeqNo == seqNo);
                    DataTable table = asCo.LoadDataTable();


                    #endregion

                    #region AskesCoveredRoomICCU

                    asCo = new AskesCovered2Query("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'002' AS 'itemID'>",
                        @"<'Kamar ICCU,ICU/Hari' AS 'itemName'>",
                        @"<CASE WHEN a.ICCUAmount > 0 THEN
	                a.ICCUAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.ICCUAmount > 0 THEN
	                a.ICCUDays else 0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.IccuDays.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo), asCo.SeqNo == seqNo);
                    table.Merge(asCo.LoadDataTable());


                    #endregion

                    #region AskesCoveredRoomHCU

                    asCo = new AskesCovered2Query("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'003' AS 'itemID'>",
                        @"<'Kamar HCU/Hari' AS 'itemName'>",
                        @"<CASE WHEN a.HCUAmount > 0 THEN
	                a.HCUAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.HCUAmount > 0 THEN
	                a.HCUDays else 0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.HcuDays.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo), asCo.SeqNo == seqNo);
                    table.Merge(asCo.LoadDataTable());


                    #endregion

                    #region AskesCoveredMedicalSupportAmount

                    asCo = new AskesCovered2Query("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                         @"<'005' AS 'itemID'>",
                        @"<'Penunjang Medis' AS 'itemName'>",
                        @"<CASE WHEN a.MedicalSupportAmount > 0 THEN
	                a.MedicalSupportAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.MedicalSupportAmount > 0 THEN
	                1
                    else
                    0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.MedicalSupportAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo), asCo.SeqNo == seqNo);

                    table.Merge(asCo.LoadDataTable());

                    #endregion

                    #region AskesCoveredHD

                    asCo = new AskesCovered2Query("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'006' AS 'itemID'>",
                        @"<'Haemodialisa(HD)' AS 'itemName'>",
                        @"<CASE WHEN a.HaemodialiseAmount > 0 THEN
	                a.HaemodialiseAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.HaemodialiseAmount > 0 THEN
	                1
                    else
                    0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.HaemodialiseAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo), asCo.SeqNo == seqNo);

                    table.Merge(asCo.LoadDataTable());
                    #endregion

                    #region AskesCoveredCtScan

                    asCo = new AskesCovered2Query("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                        @"<'007' AS 'itemID'>",
                        @"<'Ct Scan' AS 'itemName'>",
                        @"<CASE WHEN a.CtScanAmount > 0 THEN
	                a.CtScanAmount else 0
	                END AS 'Price'>",
                        @"<CASE WHEN a.CtScanAmount > 0 THEN
	                1
                    else
                    0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.CtScanAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo), asCo.SeqNo == seqNo);

                    table.Merge(asCo.LoadDataTable());
                    #endregion

                    #region AskesCoveredSurgery

                    asCo = new AskesCovered2Query("a");
                    reg = new RegistrationQuery("b");
                    patient = new PatientQuery("c");
                    medic1 = new ParamedicQuery("d");
                    room = new ServiceRoomQuery("f");
                    grr = new GuarantorQuery("h");
                    cls = new ClassQuery("g");

                    asCo.Select(
                        reg.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.DateOfBirth,
                        patient.StreetName,
                        patient.City,
                        medic1.ParamedicName.As("ParamedicNameHeader"),
                        reg.RegistrationDateTime.As("DateRegistration"),
                        room.RoomName.Coalesce("''"),
                        cls.ClassName,
                        reg.BedID.Coalesce("''"),
                        grr.GuarantorName,
                        reg.DischargeDate,
                        reg.DischargeTime,
                        @"<CASE WHEN b.SRRegistrationType <> 'IPR' THEN	
            	        CAST((b.[RegistrationDate]+ ' ' +b.[RegistrationTime]) AS DATETIME)
                    ELSE             	 
            	        CASE 
                              WHEN b.DischargeDate Is Not NULL THEN 	
                      	        CAST((b.DischargeDate + ' ' + b.DischargeTime) AS DATETIME)
                              ELSE GETDATE()	
                        END	
                      END AS 'DischargeDates'>",
                       @"<'004' AS 'itemID'>",
                       @"<'Tindakan Operasi' AS 'itemName'>",
                       @"<CASE WHEN a.SurgeryAmount > 0 THEN
	                a.SurgeryAmount else 0
	                END AS 'Price'>",
                      @"<CASE WHEN a.SurgeryAmount > 0 THEN
	                1
                    else
                    0
	                END AS 'Qty'>"
                        );
                    asCo.InnerJoin(reg).On(asCo.RegistrationNo == reg.RegistrationNo);
                    asCo.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                    asCo.LeftJoin(medic1).On(reg.ParamedicID == medic1.ParamedicID);
                    asCo.LeftJoin(room).On(reg.RoomID == room.RoomID);
                    asCo.InnerJoin(cls).On(reg.ClassID == cls.ClassID);
                    asCo.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                    asCo.Where(asCo.SurgeryAmount.GreaterThan(0));
                    asCo.Where(asCo.RegistrationNo.In(regNo), asCo.SeqNo == seqNo);

                    table.Merge(asCo.LoadDataTable());
                    #endregion

                    this.DataSource = table;
                    table1.DataSource = table;
                }

                var healthcare = Healthcare.GetHealthcare();
                
                TxtNameRS.Value = healthcare.HealthcareName;
                TxtCityRS.Value = healthcare.AddressLine1 + ' ' + healthcare.City;
                TxtTelp.Value = "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;

                var user = new AppUser();
                user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
                TxtUser.Value = user.UserName;

                var oreg = new Registration();
                oreg.LoadByPrimaryKey(regNo);
                textBox21.Value = oreg.RegistrationNo;

                var clsp = new Class();
                clsp.LoadByPrimaryKey(oreg.ClassID);
                var serv = new ServiceUnit();
                serv.LoadByPrimaryKey(oreg.ServiceUnitID);

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(printJobParameters.FindByParameterName("GuarantorAskesID").ValueString);

                textBox49.Value = guar.GuarantorName;
                textBox26.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", oreg.RegistrationDate);

                if (oreg.SRRegistrationType != "IPR")
                {
                    txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, oreg.RegistrationDate);
                    textBox43.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", oreg.RegistrationDate);
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName;
                }
                else
                {
                    var ac2 = new AskesCovered2();
                    if (ac2.LoadByPrimaryKey(regNo, seqNo))
                    {
                        txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", ac2.StartDate, ac2.EndDate);

                        if (oreg.DischargeDate != null)
                            textBox43.Value = string.Format("{0:dd-MMM-yyyy}", oreg.DischargeDate) + " " + oreg.DischargeTime;
                        else
                            textBox43.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now);
                    }
                    else
                    {
                        if (oreg.DischargeDate != null)
                        {
                            txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, oreg.DischargeDate);
                            textBox43.Value = string.Format("{0:dd-MMM-yyyy}", oreg.DischargeDate) + " " + oreg.DischargeTime;
                        }
                        else
                        {
                            textBox43.Value = string.Format("{0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now);
                            if (startDate != null && endDate != null)
                                txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", startDate, endDate);

                            else
                                txtPeriode.Value = string.Format("Periode:  {0:dd - MMM - yyyy}  s/d  {1:dd - MMM - yyyy}", oreg.RegistrationDate, DateTime.Now);
                        }
                    }
                    textBox16.Value = serv.ServiceUnitName + " / " + clsp.ClassName + " / " + oreg.BedID;
                }
            }
        }
    }
}