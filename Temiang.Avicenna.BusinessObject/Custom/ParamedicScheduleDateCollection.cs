using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicScheduleDateCollection
    {
        public DataTable GetParamedicID(string serviceUnitId, string regType)
        {
            esParameters par = new esParameters();
            par.Add("p_ServiceUnitID", serviceUnitId);

            string commandText =
                @"SELECT DISTINCT psd.ParamedicID, p.ParamedicName FROM ParamedicScheduleDateItem AS psd
INNER JOIN Paramedic AS p ON p.ParamedicID = psd.ParamedicID AND p.IsActive = 1 
INNER JOIN OperationalTime AS ot ON ot.OperationalTimeID = psd.OperationalTimeID
INNER JOIN ServiceUnitParamedic AS sup ON sup.ServiceUnitID = psd.ServiceUnitID AND sup.ParamedicID = psd.ParamedicID
WHERE psd.ServiceUnitID = @p_ServiceUnitID AND psd.ScheduleDate = CAST(GETDATE() AS DATE)
AND (
(ot.StartTime1 <= CONVERT(VARCHAR(5), GETDATE(), 114) AND ot.EndTime1 >= CONVERT(VARCHAR(5), GETDATE(), 114))
OR
(ot.StartTime2 <= CONVERT(VARCHAR(5), GETDATE(), 114) AND ot.EndTime2 >= CONVERT(VARCHAR(5), GETDATE(), 114))
OR
(ot.StartTime3 <= CONVERT(VARCHAR(5), GETDATE(), 114) AND ot.EndTime3 >= CONVERT(VARCHAR(5), GETDATE(), 114))
OR
(ot.StartTime4 <= CONVERT(VARCHAR(5), GETDATE(), 114) AND ot.EndTime4 >= CONVERT(VARCHAR(5), GETDATE(), 114))
OR
(ot.StartTime5 <= CONVERT(VARCHAR(5), GETDATE(), 114) AND ot.EndTime5 >= CONVERT(VARCHAR(5), GETDATE(), 114))
) ";

            if (regType == "IPR")
                commandText += "AND psd.IsIpr = 1 ";
            else if (regType == "OPR" || regType == "MCU")
                commandText += "AND psd.IsOpr = 1 ";
            else if (regType == "EMR")
                commandText += "AND psd.IsEmr = 1 ";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
