/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.5.278
 * Time: 7/22/2016 9:28:01 AM
 ************************************************************/
 
-- sp_xml_slpTestResultNativeMultiRowsV2 'JO230901-00540', '001'
 
ALTER PROCEDURE sp_xml_slpTestResultNativeMultiRowsV2
 (@p_TransactionNo VARCHAR(20) /*='JO160522-00029'*/ ,
  @p_SequenceNo  VARCHAR(10) /*='001'*/)
AS
SET NOCOUNT ON
 
--DECLARE @p_TransactionNo VARCHAR(100) ='JO230901-00540',
--		@p_SequenceNo    VARCHAR(120) ='001'

DECLARE @xResult VARCHAR(MAX) = (
	SELECT REPLACE(tr.[TestResult],'<br/>','') Result 
	FROM TestResult AS tr 
		INNER JOIN TransChargesItem AS tci ON tr.TransactionNo = tci.TransactionNo 
			AND tr.ItemID = tci.ItemID AND tci.SequenceNo = @p_SequenceNo
	WHERE tr.TransactionNo = @p_TransactionNo)

SELECT a.[TransactionNo],
       b.[RegistrationNo],
       a.[ItemID],
       f.ItemName  AS         'itemname',
       a.[ParamedicID] ,
       b.PhysicianSenders ParamedicName,
       h.RealizationDateTime AS TglPeriksa,
       a.ClinicalInfo,
       b.Notes,
       a.[TestResultDateTime],
       --REPLACE(a.[TestResult],'<br/>','') AS 'Result',
       --REPLACE(a.TestResultOtherLang,'<br/>','') AS 'TestResultOther',
       RTRIM(
           LTRIM(
               (
                   (
                       RTRIM(LTRIM(((d.[FirstName] + ' ') + d.[MiddleName]))) + 
                       ' '
                   ) + d.[LastName]
               )
           )
       )                            AS 'PatientName',
       ISNULL(d.MedicalNo, ' -')  AS 'MedicalNo', 
       CONVERT(VARCHAR(MAX), d.DateOfBirth, 101) + ' - ' + dbo.[fnCalculateAge](d.DateOfBirth) AS 
       'DateOfBirth2',
       d.DateOfBirth,
        c.AgeInYear,
       CASE 
            WHEN d.sex = 'F' THEN 'Female'
            ELSE 'MALE'
       END                          AS 'Sex2',
       d.[Sex],
       CASE 
            WHEN d.StreetName = '' AND d.City = '' THEN ' -'
            WHEN d.StreetName <> '' AND d.City = '' THEN d.StreetName
            WHEN d.StreetName = '' AND d.City <> '' THEN d.City
            ELSE d.StreetName + ' - ' + d.City
       END                          AS 'ADDRESS',
       CASE 
            WHEN d.PhoneNo = '' AND d.MobilePhoneNo = '' THEN ' -'
            WHEN d.PhoneNo <> '' AND d.MobilePhoneNo = '' THEN d.PhoneNo
            WHEN d.PhoneNo = '' AND d.MobilePhoneNo <> '' THEN d.MobilePhoneNo
            ELSE d.PhoneNo + ' / ' + d.MobilePhoneNo
       END                          AS 'HP',
       g.[ServiceUnitName], 
       h.FilmNo,
       a.LastUpdateByUserID,
       i.GuarantorName,
       p.ParamedicName AS ParamedicCollectionName,
       su.ServiceUnitName ToServiceUnit,
       sr.RoomName,c.BedID,c2.ClassName,
       xRows.[VALUE] ResultRows,
       CHARINDEX('</table>',xRows.[VALUE]) TableIndex
FROM   [TestResult] a
       INNER JOIN [TransCharges] b
            ON  a.[TransactionNo] = b.[TransactionNo]
       INNER JOIN [Registration] c
            ON  c.[RegistrationNo] = b.[RegistrationNo]
       INNER JOIN [Patient] d
            ON  c.[PatientID] = d.[PatientID]
       LEFT JOIN [Paramedic] e
            ON  a.[ParamedicID] = e.[ParamedicID]
       INNER JOIN [Item] f
            ON  a.[ItemID] = f.[ItemID]
       INNER JOIN [ServiceUnit] g
            ON  b.[ToServiceUnitID] = g.[ServiceUnitID]
       INNER JOIN [TransChargesItem] h
            ON  (
                    b.[TransactionNo] = h.[TransactionNo]
                    AND a.[ItemID] = h.[ItemID]
            ) 
       INNER JOIN Guarantor AS i 
			ON i.GuarantorID = c.GuarantorID
		INNER JOIN Paramedic AS p
			ON p.ParamedicID = a.ParamedicID
		INNER JOIN ServiceUnit AS su
			ON su.ServiceUnitID = c.ServiceUnitID
		INNER JOIN ServiceRoom AS sr
			ON sr.RoomID = c.RoomID
		INNER JOIN Class AS c2
			ON c2.ClassID = b.ClassID
		CROSS JOIN (select * from TestResultLineToRowsV2(@xResult)) xRows
WHERE  a.[TransactionNo] = @p_TransactionNo
       AND h.[SequenceNo] = @p_SequenceNo
       
       
       
       --SELECT*FROM TransChargesItem AS tci WHERE tci.TransactionNo='JO180126-00001'