using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransChargesCollection
    {
//        public DataTable TransChargesHistory(string patientID, string registrationNo, string serviceUnit)
//        {
//            esParameters par = new esParameters();

//            string commandText = @"SELECT  FORMAT(f.[RegistrationDate], 'yyyy-MM-dd') + f.[RegistrationNo] RegDateRegNo, a.[RegistrationNo],f.[RegistrationDate],f.[PatientID],a.[TransactionNo],a.[TransactionDate],a.[IsApproved],a.[IsVoid],a.[IsOrder],
//                        c.[ServiceUnitName] AS 'ToServiceUnitName',g.[ItemName] AS 'RegistrationTypeName' 
//                        FROM [TransCharges] a 
//                        INNER JOIN [Registration] f ON f.[RegistrationNo] = a.[RegistrationNo] 
                         
//                        INNER JOIN [ServiceUnit] c ON c.[ServiceUnitID] = a.[ToServiceUnitID] 
//                        LEFT JOIN [AppStandardReferenceItem] g ON (g.[ItemID] = f.[SRRegistrationType] 
//                            AND g.[StandardReferenceID] = 'RegistrationType') 
//                        WHERE 
//                                ((f.[PatientID] = '" + patientID + "' OR f.PatientID IN (SELECT x.RelatedPatientID FROM PatientRelated x WHERE x.PatientID = '" + patientID + "'))) ";

////"AND a.[RegistrationNo] LIKE '" + registrationNo + "%' AND a.[ToServiceUnitID] LIKE '" + serviceUnit + "%' AND f.[IsVoid] = 0 " +
////"AND ISNULL(a.PackageReferenceNo,'') = '' " +
////"ORDER BY f.[RegistrationDate] ASC,f.[RegistrationTime] ASC, a.[ToServiceUnitID] ASC, a.[TransactionNo] ASC";

//            if (!string.IsNullOrEmpty(registrationNo))
//                commandText = string.Concat(commandText, " AND a.[RegistrationNo] LIKE '" + registrationNo + "%' ");

//            if (!string.IsNullOrEmpty(serviceUnit))
//                commandText = string.Concat(commandText, " AND a.[ToServiceUnitID] LIKE '" + serviceUnit + "%' ");

//            commandText = string.Concat(commandText, " AND f.[IsVoid] = 0 AND (a.PackageReferenceNo IS NULL or a.PackageReferenceNo ='') ");
//            commandText = string.Concat(commandText, " ORDER BY f.[RegistrationDate] ASC,f.[RegistrationTime] ASC, a.[ToServiceUnitID] ASC, a.[TransactionNo] ASC");

//            return FillDataTable(esQueryType.Text, commandText, par);
//        }
        
        public DataTable TransChargesHistory(string patientID, string registrationNo, string serviceUnit)
        {
            List<string> patientids = new List<string>();
            var prColl = new PatientRelatedCollection();
            prColl.Query.Where(prColl.Query.PatientID == patientID);
            if (prColl.LoadAll()) {
                patientids = prColl.Select(pr => pr.RelatedPatientID).ToList();
                patientids.Add(patientID);
            }

            esParameters par = new esParameters();

            string commandText = @"SELECT  FORMAT(f.[RegistrationDate], 'yyyy-MM-dd') + f.[RegistrationNo] RegDateRegNo, a.[RegistrationNo],f.[RegistrationDate],f.[PatientID],a.[TransactionNo],a.[TransactionDate],a.[IsApproved],a.[IsVoid],a.[IsOrder],
                        c.[ServiceUnitName] AS 'ToServiceUnitName',g.[ItemName] AS 'RegistrationTypeName' 
                        FROM [TransCharges] a 
                        INNER JOIN [Registration] f ON f.[RegistrationNo] = a.[RegistrationNo] 
                         
                        INNER JOIN [ServiceUnit] c ON c.[ServiceUnitID] = a.[ToServiceUnitID] 
                        LEFT JOIN [AppStandardReferenceItem] g ON (g.[ItemID] = f.[SRRegistrationType] 
                            AND g.[StandardReferenceID] = 'RegistrationType') ";

            if (patientids.Count > 0)
            {
                commandText += " WHERE f.PatientID IN (" + String.Join(",", patientids.Select(x => "'" + x + "'")) + ") ";
            }
            else {
                commandText += " WHERE f.PatientID = '" + patientID + "'";
            }

            if (!string.IsNullOrEmpty(registrationNo))
                commandText = string.Concat(commandText, " AND a.[RegistrationNo] LIKE '" + registrationNo + "%' ");

            if (!string.IsNullOrEmpty(serviceUnit))
                commandText = string.Concat(commandText, " AND a.[ToServiceUnitID] LIKE '" + serviceUnit + "%' ");

            commandText = string.Concat(commandText, " AND f.[IsVoid] = 0 AND (a.PackageReferenceNo IS NULL or a.PackageReferenceNo ='') ");
            commandText = string.Concat(commandText, " ORDER BY f.[RegistrationDate] ASC,f.[RegistrationTime] ASC, a.[ToServiceUnitID] ASC, a.[TransactionNo] ASC");

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable fGetPaymentMethod(string registrationNo)
        {
            if (registrationNo.Contains(","))
                registrationNo = registrationNo.Replace(",", "','");

            string commandText =
                @"BEGIN
                        DECLARE @PaymentMethod  VARCHAR(200),
                                @Desc           VARCHAR(2000),
                                @SumBalance     MONEY,
                                @Balance        MONEY,
                                @Total          MONEY,
                                @SumTotal       MONEY
                                        	
                        SET @Desc = ''
                        SET @SumBalance = 0
                        SET @SumTotal = 0

                        DECLARE crPaymentMethod CURSOR STATIC 
                        FOR
                            SELECT DISTINCT
                                   ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) + ' : ' + CONVERT(VARCHAR, CAST(B.Amount AS MONEY), 1) AS PayMethod,
                                   b.Balance,
                                   B.Amount
                            FROM   TransPayment a
                                   INNER JOIN TransPaymentItem b
                                        ON  B.PaymentNo = a.PaymentNo AND A.IsApproved = 1 
                                        AND A.IsVoid = 0 AND A.TransactionCode = '016'
                                   INNER JOIN PaymentType pt
                                        ON  b.SRPaymentType = pt.SRPaymentTypeID
                                   LEFT JOIN PaymentMethod pm
                                        ON  pm.SRPaymentMethodID = b.SRPaymentMethod
                                        AND pm.SRPaymentTypeID = b.SRPaymentType
                            WHERE  a.RegistrationNo IN ('" + registrationNo + @"')
                                   
                                        			 
                        OPEN crPaymentMethod
                        FETCH FIRST FROM crPaymentMethod INTO @PaymentMethod, @Balance, @Total
                                        	
                        WHILE @@Fetch_Status = 0
                        BEGIN
                            IF LEN(RTRIM(ISNULL(@PaymentMethod, ''))) > 0
                            BEGIN
                                SET @Desc = @Desc + @PaymentMethod + ', '
                                SET @SumBalance = @SumBalance + @Balance
                                SET @SumTotal = @SumTotal + @Total
                            END
                            
                            FETCH NEXT FROM crPaymentMethod INTO @PaymentMethod, @Balance, @Total
                        END
                                        	
                        CLOSE crPaymentMethod
                        DEALLOCATE crPaymentMethod
                                        	
                        IF LEN(RTRIM(ISNULL(@desc, ''))) > 0
                        BEGIN
                            SET @Desc = LEFT(@desc, LEN(@desc) - 1)
                        END
                                        	
                        SELECT @Desc AS PaymentMethod,
                               CONVERT(VARCHAR, CAST(@SumBalance AS MONEY), 1) AS SumBalance,
                               @SumTotal AS SumTotal
                 END";
            return FillDataTable(esQueryType.Text, commandText);
        }


        public DataTable fGetDownPaymentMethod(string registrationNo, string paymentNo)
        {
            if (registrationNo.Contains(","))
                registrationNo = registrationNo.Replace(",", "','");

            string commandText =
                @"BEGIN
                        DECLARE @PaymentMethod  VARCHAR(200),
                                @Desc           VARCHAR(2000),
                                @SumBalance     MONEY,
                                @Balance        MONEY,
                                @Total          MONEY,
                                @SumTotal       MONEY
                                        	
                        SET @Desc = ''
                        SET @SumBalance = 0
                        SET @SumTotal = 0

                        DECLARE crPaymentMethod CURSOR STATIC 
                        FOR
                            SELECT DISTINCT
                                   ISNULL(pm.PaymentMethodName, pt.PaymentTypeName) + ' : ' + CONVERT(VARCHAR, CAST(B.Amount AS MONEY), 1) AS PayMethod,
                                   b.Balance,
                                   B.Amount
                            FROM   TransPayment a
                                   INNER JOIN TransPaymentItem b
                                        ON  b.PaymentNo = a.PaymentNo
                                   INNER JOIN PaymentType pt
                                        ON  b.SRPaymentType = pt.SRPaymentTypeID
                                   LEFT JOIN PaymentMethod pm
                                        ON  pm.SRPaymentMethodID = b.SRPaymentMethod
                                        AND pm.SRPaymentTypeID = b.SRPaymentType
                            WHERE  a.RegistrationNo IN ('" + registrationNo + @"')
                                   AND a.PaymentNo = '" + paymentNo + @"'
                                   AND IsVoid = 0
                                   AND TransactionCode = '018'
                                        			 
                        OPEN crPaymentMethod
                        FETCH FIRST FROM crPaymentMethod INTO @PaymentMethod, @Balance, @Total
                                        	
                        WHILE @@Fetch_Status = 0
                        BEGIN
                            IF LEN(RTRIM(ISNULL(@PaymentMethod, ''))) > 0
                            BEGIN
                                SET @Desc = @Desc + @PaymentMethod + ', '
                                SET @SumBalance = @SumBalance + @Balance
                                SET @SumTotal = @SumTotal + @Total
                            END
                            
                            FETCH NEXT FROM crPaymentMethod INTO @PaymentMethod, @Balance, @Total
                        END
                                        	
                        CLOSE crPaymentMethod
                        DEALLOCATE crPaymentMethod
                                        	
                        IF LEN(RTRIM(ISNULL(@desc, ''))) > 0
                        BEGIN
                            SET @Desc = LEFT(@desc, LEN(@desc) - 1)
                        END
                                        	
                        SELECT @Desc AS PaymentMethod,
                               CONVERT(VARCHAR, CAST(@SumBalance AS MONEY), 1) AS SumBalance,
                               @SumTotal AS SumTotal
                 END";
            return FillDataTable(esQueryType.Text, commandText);
        }

        public DataTable GetMedicalSupportQueueByDate(DateTime TransactionDate, string ServiceUnitID)
        {
            string cmd = string.Empty;
            cmd = "sp_MedicalSupportQueueByDate";

            var pars = new esParameters();
            var pTD = new esParameter("TransactionDate", TransactionDate, esParameterDirection.Input, DbType.Date, 20);
            pars.Add(pTD);
            var pServiceUnitID = new esParameter("ServiceUnitID", ServiceUnitID, esParameterDirection.Input, DbType.String, 10);
            pars.Add(pServiceUnitID);

            es.Connection.CommandTimeout = 600;

            return FillDataTable(esQueryType.StoredProcedure, cmd, pars);
        }

    }
}

