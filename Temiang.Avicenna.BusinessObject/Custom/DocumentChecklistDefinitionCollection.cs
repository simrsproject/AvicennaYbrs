using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class DocumentChecklistDefinitionCollection
    {
        public DataTable GetFullJoinWDocumentDefinition(string documentChecklistId)
        {
            esParameters par = new esParameters();
            par.Add("p_DocumentChecklistId", documentChecklistId);

            string commandText =
                @"SELECT a.DocumentFilesID, a.DocumentNumber, a.DocumentName, IsSelect=CONVERT(BIT,CASE WHEN COALESCE(b.DocumentFilesID,'-')='-' THEN 0 ELSE 1 END)
                FROM DocumentFiles a
                LEFT JOIN DocumentChecklistDefinition b
                      ON  a.DocumentFilesID = b.DocumentFilesID
                      AND b.SRDocumentChecklist = @p_DocumentChecklistId 
                WHERE a.IsUsedForGuarantorChecklist = 1
                ORDER BY a.DocumentNumber";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetInnerJoinWDocumentDefinition(string documentChecklistId)
        {
            esParameters par = new esParameters();
            par.Add("p_DocumentChecklistId", documentChecklistId);

            string commandText =
                @"SELECT a.DocumentFilesID, a.DocumentNumber, a.DocumentName, IsSelect = CONVERT(BIT, 1)
                FROM DocumentFiles a
                INNER JOIN DocumentChecklistDefinition b
                      ON  a.DocumentFilesID = b.DocumentFilesID
                      AND b.SRDocumentChecklist = @p_DocumentChecklistId 
                ORDER BY a.DocumentNumber";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
