using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalLicenceCollection
    {
		public DataTable GetPersonalLicenseRecap(int personId)
		{
			esParameters par = new esParameters();
			par.Add("p_PersonID", personId);

			string commandText =
				@"SELECT x.SRLicenceType, asri.ItemName AS LicenseTypeName, asri.NumericValue AS DayLimit, [dbo].[fn_PersonalLicenseNotes](
					(SELECT TOP 1 pl.PersonalLicenceID FROM PersonalLicence AS pl WHERE pl.PersonID = @p_PersonID AND pl.SRLicenceType = x.SRLicenceType ORDER BY pl.ValidTo DESC)) AS Notes, 
					[dbo].[fn_PersonalLicenseRemaining](
					(SELECT TOP 1 pl.PersonalLicenceID FROM PersonalLicence AS pl WHERE pl.PersonID = @p_PersonID AND pl.SRLicenceType = x.SRLicenceType ORDER BY pl.ValidTo DESC)) AS Remaining
				FROM 
				(SELECT DISTINCT pl.SRLicenceType FROM PersonalLicence AS pl WHERE pl.PersonID = @p_PersonID) x
				INNER JOIN AppStandardReferenceItem AS asri ON asri.StandardReferenceID = 'LicenseType' AND asri.ItemID = x.SRLicenceType 
				ORDER BY x.SRLicenceType ";
			return FillDataTable(esQueryType.Text, commandText, par);
		}
	}
}
