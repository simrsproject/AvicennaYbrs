using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ReservationCollection
    {
        public DataTable GetJadualBed()
        {
            string commandText =
                @"SET NOCOUNT ON
DECLARE @TanggalAwal DATETIME
DECLARE @n INT
DECLARE @nTotal INT
DECLARE @MYSQL VARCHAR(4000)
DECLARE @MYSQL2 VARCHAR(1000)
 
SET @TanggalAwal = DATEADD(DAY,-3,CONVERT(DATETIME,CONVERT(CHAR(10),GETDATE(),102)))
IF ISNULL(((SELECT OBJECT_ID('TEMPDB..#Temp_Jadual')   )), 0) > 0  
        DROP TABLE #Temp_Jadual

CREATE TABLE #Temp_Jadual
( 
  Tanggal DATETIME NOT NULL,
)
SET @n = 0
WHILE @n < 16
 BEGIN  
   SET @MYSQL = 'INSERT INTO #Temp_Jadual SELECT DATEADD(DAY,'+ CONVERT(VARCHAR(2),@n) + ', '''+ CONVERT(CHAR(10),@TanggalAwal,102) + ''')'
   EXEC (@MYSQL)  
   SET @n = @n + 1 
 END
 
 BEGIN
   SET @n = 1
   SET @nTotal = ( SELECT COUNT(A.BedID)
    	           FROM BED A
       	           INNER JOIN ServiceRoom B ON A.RoomID = B.RoomID AND A.IsActive = 1
	               INNER JOIN ServiceUnit C ON B.ServiceUnitID = C.ServiceUnitID AND C.SRRegistrationType = 'IPR' )
 END
 
 WHILE @n < ( @nTotal + 1)
 BEGIN  
   SET @MYSQL = 'ALTER TABLE #Temp_Jadual ADD [Kolom' + CONVERT(VARCHAR(2),@n) + '] varchar(60) DEFAULT '''' NOT NULL'
   EXEC (@MYSQL)  
   SET @n = @n + 1 
 END
 
 BEGIN
   SET @MYSQL = ''
   SET @n = 1
   DECLARE BedID_cursor CURSOR
   FOR
	 SELECT
	  A.BedID
	FROM BED A
	INNER JOIN ServiceRoom B ON A.RoomID = B.RoomID AND A.IsActive = 1
	INNER JOIN ServiceUnit C ON B.ServiceUnitID = C.ServiceUnitID AND C.SRRegistrationType = 'IPR'
	ORDER BY A.BedID
    OPEN BedID_cursor    
    DECLARE @BedID varchar(10)    
     
   FETCH NEXT FROM BedID_cursor INTO @BedID
         WHILE (@@FETCH_STATUS <> -1)
         BEGIN
                SET @MYSQL = 'UPDATE #Temp_Jadual SET [Kolom' + CONVERT(VARCHAR(2),@n) + ']= '''+ @BedID + ''' WHERE CONVERT(CHAR(10),Tanggal,102) ='''+ CONVERT(CHAR(10),@TanggalAwal,102) + ''''
                EXEC(@MYSQL)
                
                SET @MYSQL2 = 'UPDATE  #Temp_Jadual SET '
                               + '[Kolom' + CONVERT(VARCHAR(2),@n) + ']= ((((C.[FirstName]+'' '')+RTRIM(C.[MiddleName]))+'' '')+RTRIM(C.[LastName])) ' +
                               + 'FROM  #Temp_Jadual A ' +
                               + ' INNER JOIN Reservation B ON A.Tanggal = B.ReservationDate AND B.BedID = '''+ @BedID + ''''
                               + ' INNER JOIN dbo.Patient C ON B.PatientID = C.PatientID '
                EXEC(@MYSQL2)                
                FETCH NEXT FROM BedID_cursor INTO @BedID
                SET @n = @n + 1 
         END       
   
   CLOSE BedID_cursor    
   DEALLOCATE BedID_cursor  
 END
 
 SELECT * FROM #Temp_Jadual";

            return FillDataTable(esQueryType.Text, commandText);
        }
    }
}
