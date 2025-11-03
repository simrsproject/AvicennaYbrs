using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class DocumentDefinitionItemCollection
    {
        public DataTable GetFullJoinWDocumentDefinition(int documentDefinitionId)
        {
            esParameters par = new esParameters();
            par.Add("p_DocumentDefinitionId", documentDefinitionId.ToString());

            string commandText =
                @"SELECT a.DocumentFilesID, a.DocumentNumber, a.DocumentName, IsSelect=CONVERT(BIT,CASE WHEN COALESCE(b.DocumentFilesID,'-')='-' THEN 0 ELSE 1 END)
                FROM DocumentFiles a
                LEFT JOIN DocumentDefinitionItem b
                      ON  a.DocumentFilesID = b.DocumentFilesID
                      AND b.DocumentDefinitionID = @p_DocumentDefinitionId 
                WHERE a.IsUsedForGuarantorChecklist = 0
                ORDER BY a.DocumentNumber";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
        public DataTable GetInnerJoinWDocumentDefinition(int documentDefinitionId)
        {
            esParameters par = new esParameters();
            par.Add("p_DocumentDefinitionId", documentDefinitionId.ToString());

            string commandText =
                @"SELECT a.DocumentFilesID, a.DocumentNumber, a.DocumentName, IsSelect = CONVERT(BIT, 1)
                FROM DocumentFiles a
                INNER JOIN DocumentDefinitionItem b
                      ON  a.DocumentFilesID = b.DocumentFilesID
                      AND b.DocumentDefinitionID = @p_DocumentDefinitionId 
                ORDER BY a.DocumentNumber";
            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
