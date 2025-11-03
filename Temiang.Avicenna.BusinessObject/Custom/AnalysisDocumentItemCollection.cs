using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AnalysisDocumentItemCollection
    {
        public DataTable GetFullJoinWDocument(string departmentID, string filesAnalysis)
        {
            esParameters par = new esParameters();
            par.Add("p_FilesAnalysis", filesAnalysis);

            string commandText =
                @"SELECT B.DocumentFilesID, C.DocumentName, C.DocumentNumber,
       CONVERT(BIT,0) AS IsQuantity, CONVERT(BIT,0) AS IsQuality, CONVERT(BIT,0) AS IsLegible, CONVERT(BIT,0) AS IsSign,
        C.IsQuality AS IsQualityCek, C.IsLegible AS IsLegibleCek, C.IsSign AS IsSignCek
FROM DocumentDefinition A
INNER JOIN DocumentDefinitionItem B ON A.DocumentDefinitionID = B.DocumentDefinitionID
INNER JOIN DocumentFiles C ON B.DocumentFilesID = C.DocumentFilesID
WHERE A.SRFilesAnalysis = @p_FilesAnalysis 
ORDER BY C.DocumentNumber";
            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable GetInnerJoinWDocument(string registrationNo)
        {
            esParameters par = new esParameters();
            par.Add("p_RegistrationNo", registrationNo);

            string commandText =
                @"SELECT A.RegistrationNo, B.DocumentFilesID, C.DocumentName, C.DocumentNumber,
                  C.IsQuality AS IsQualityCek, C.IsLegible AS IsLegibleCek, C.IsSign AS IsSignCek,
                  ISNULL(B.IsQuantity,0) AS IsQuantity, 
                  ISNULL(B.IsQuality,0) AS IsQuality, 
                  ISNULL(B.IsLegible,0) AS IsLegible,
                  ISNULL(B.IsSign,0) AS IsSign
                  FROM AnalysisDocument A
                  INNER JOIN AnalysisDocumentItem B ON A.RegistrationNo = B.RegistrationNo AND A.RegistrationNo = @p_RegistrationNo
                  INNER JOIN DocumentFiles C ON B.DocumentFilesID = C.DocumentFilesID
                  ORDER BY C.DocumentNumber";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
