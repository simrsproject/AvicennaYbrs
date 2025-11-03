using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeWorkingInfoCollection
    {
        public DataTable EmployeeWorkingInfoView(int PersonID)
        {
            esParameters par = new esParameters();
            par.Add("p_PersonID", PersonID);
            string commandText = @"SELECT *
                            FROM Vw_EmployeeTable
                            WHERE PersonID = @p_PersonID";
            return FillDataTable(esQueryType.Text, commandText, par);
        }

		public DataTable GetActiveLicense(string pId)
		{
			esParameters par = new esParameters();
			par.Add("p_PersonID", pId);

			string commandText =
			@"SELECT x.PersonID, x.SRLicenceType, y.ItemName AS LicenseTypeName, x.ValidFrom, x.ValidTo, x.Note
			FROM 
			(
				SELECT pl.PersonID, pl.SRLicenceType, pl.ValidFrom, pl.ValidTo, pl.Note, 
						ROW_NUMBER() OVER(PARTITION BY pl.PersonID, pl.SRLicenceType ORDER BY pl.ValidTo DESC) AS rn 
				FROM PersonalLicence AS pl WITH (NOLOCK)
				WHERE pl.PersonID = @p_PersonID AND pl.ValidFrom < GETDATE() AND DATEADD(DAY, 1, pl.ValidTo) > GETDATE()  
			) x
			INNER JOIN (SELECT asri.ItemID, asri.ItemName FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'LicenseType') y ON y.ItemID = x.SRLicenceType
			WHERE x.rn = 1
			ORDER BY x.SRLicenceType";

			return FillDataTable(esQueryType.Text, commandText, par);
		}

		public DataTable GetEmployeeByEmployment()
		{
			esParameters par = new esParameters();

			string commandText =
			@"SELECT x.SREmploymentType, a.ItemName, COUNT(x.PersonID) AS ChartCount
			FROM 
			(
				SELECT eep.PersonID, eep.SREmploymentType,  
						ROW_NUMBER() OVER(PARTITION BY eep.PersonID ORDER BY eep.ValidFrom DESC) AS rn 
				FROM EmployeeEmploymentPeriod AS eep WITH (NOLOCK)
					INNER JOIN (SELECT asri.ItemID, asri.ItemName FROM AppStandardReferenceItem AS asri WITH(NOLOCK) WHERE asri.StandardReferenceID = 'EmploymentType') a ON a.ItemID = eep.SREmploymentType
			) x
			INNER JOIN EmployeeWorkingInfo AS ewi ON ewi.PersonID = x.PersonID
			INNER JOIN (SELECT asri.ItemID, asri.ItemName FROM AppStandardReferenceItem AS asri WITH(NOLOCK) WHERE asri.StandardReferenceID = 'EmploymentType') a ON a.ItemID = x.SREmploymentType
			WHERE x.rn = 1 AND ewi.SREmployeeStatus = '1'
			GROUP BY x.SREmploymentType, a.ItemName
			ORDER BY x.SREmploymentType";

			return FillDataTable(esQueryType.Text, commandText, par);
		}

		public DataTable GetEmployeeByType()
		{
			esParameters par = new esParameters();

			string commandText =
			@"SELECT x.SREmployeeType, a.ItemName, COUNT(x.PersonID) AS ChartCount
			FROM EmployeeWorkingInfo AS x 
			INNER JOIN (SELECT asri.ItemID, asri.ItemName FROM AppStandardReferenceItem AS asri WITH(NOLOCK) WHERE asri.StandardReferenceID = 'EmployeeType') a ON a.ItemID = x.SREmployeeType
			WHERE x.SREmployeeStatus = '1'
			GROUP BY x.SREmployeeType, a.ItemName
			ORDER BY x.SREmployeeType";

			return FillDataTable(esQueryType.Text, commandText, par);
		}

		public DataTable GetEmployeeByAge()
		{
			esParameters par = new esParameters();

			string commandText =
			@"SELECT y.ItemID, y.ItemName, y.SRGenderType, z.ItemName AS 'GenderType', COUNT(y.PersonID) AS ChartCount
			FROM (
				SELECT x.SRGenderType, CASE WHEN x.Age < 31 THEN '01' 
						WHEN x.Age >= 31 AND x.Age < 41 THEN '02' 
						WHEN x.Age >= 41 AND x.Age < 51 THEN '03' ELSE '04' END AS ItemID, 
						CASE WHEN x.Age < 31 THEN '< 31 Th' 
						WHEN x.Age >= 31 AND x.Age < 41 THEN '31 Th s/d 40.9 Th' 
						WHEN x.Age >= 41 AND x.Age < 51 THEN '41 Th s/d 50.9 Th' ELSE '>= 51 Th' END AS ItemName, 
						x.PersonID	
				FROM
				(
					SELECT p.PersonID, CAST(CASE WHEN p.BirthDate IS NULL THEN 0 ELSE DATEDIFF(DAY, p.BirthDate, GETDATE()) / 365.0 END AS NUMERIC(18, 2)) AS Age, p.SRGenderType
					FROM PersonalInfo AS p WITH(NOLOCK)
					INNER JOIN EmployeeWorkingInfo AS ewi WITH(NOLOCK) ON ewi.PersonID = p.PersonID
					WHERE ewi.SREmployeeStatus  = '1'
				) x 
			) y 
			INNER JOIN (SELECT asri.ItemID, asri.ItemName FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'GenderType') z ON z.ItemID = y.SRGenderType
			GROUP BY y.ItemID, y.ItemName, y.SRGenderType, z.ItemName
			ORDER BY y.ItemID, y.SRGenderType";

			return FillDataTable(esQueryType.Text, commandText, par);
		}

		public DataTable GetEmployeeByLicense()
		{
			esParameters par = new esParameters();

			string commandText =
			@"SELECT a.ItemId, a.ItemName, a.StActive, COUNT(a.PersonID) AS ChartCount 
			FROM
			(
				SELECT x.SRLicenceType AS ItemId, l.ItemName AS ItemName, x.PersonID, CASE WHEN x.ValidTo >= GETDATE() THEN 'Active' ELSE 'Expired' END AS StActive
				FROM 
				(
					SELECT pl.PersonID, pl.SRLicenceType, pl.ValidTo, 
							ROW_NUMBER() OVER(PARTITION BY pl.PersonID, pl.SRLicenceType ORDER BY pl.ValidTo DESC) AS rn 
					FROM PersonalLicence AS pl WITH (NOLOCK)
					INNER JOIN EmployeeWorkingInfo AS ewi ON ewi.PersonID = pl.PersonID AND ewi.SREmployeeStatus = '1'
				) x
				INNER JOIN (SELECT * FROM AppStandardReferenceItem AS asri WHERE asri.StandardReferenceID = 'LicenseType') l ON l.ItemID = x.SRLicenceType
				WHERE x.rn = 1
			) a
			GROUP BY a.ItemId, a.ItemName, a.StActive
			ORDER BY a.ItemId, a.ItemName, a.StActive ";

			return FillDataTable(esQueryType.Text, commandText, par);
		}

		public DataTable GetEmployeeByFieldLabor()
		{
			esParameters par = new esParameters();

			string commandText =
			@"SELECT a.ItemID, a.ItemName, COUNT(a.PersonID) AS ChartCount 
			FROM
			(
				SELECT x.ItemID, x.ItemName, x.PersonID
				FROM 
				(
					SELECT pl.PersonID, pl.SREducationLevel, fl.ItemID, fl.ItemName, pl.ValidFrom, 
							ROW_NUMBER() OVER(PARTITION BY pl.PersonID, pl.SREducationLevel ORDER BY pl.ValidFrom DESC) AS rn 
					FROM EmployeeEducationLevel AS pl WITH (NOLOCK)
					INNER JOIN (SELECT asri.ItemID, asri.ItemName, asri.Note FROM AppStandardReferenceItem AS asri WITH (NOLOCK) WHERE asri.StandardReferenceID = 'EducationLevel') el  ON el.ItemID = pl.SREducationLevel
					INNER JOIN (SELECT asri.ItemID, asri.ItemName FROM AppStandardReferenceItem AS asri WITH (NOLOCK) WHERE asri.StandardReferenceID = 'FieldLabor') fl ON fl.ItemID = el.Note
					INNER JOIN EmployeeWorkingInfo AS ewi WITH (NOLOCK) ON ewi.PersonID = pl.PersonID AND ewi.SREmployeeStatus = '1'
				) x
				WHERE x.rn = 1
			) a
			GROUP BY a.ItemID, a.ItemName
			ORDER BY a.ItemID";

			return FillDataTable(esQueryType.Text, commandText, par);
		}

		public DataTable GetEmployeeByUnit()
		{
			esParameters par = new esParameters();

			string commandText =
			@"SELECT a.SubOrganizationID, ou.OrganizationUnitName, COUNT(a.PersonID) AS ChartCount 
			FROM
			(
				SELECT x.SubOrganizationID, x.PersonID
				FROM 
				(
					SELECT pl.PersonID, pl.SubOrganizationID, pl.ValidFrom, 
							ROW_NUMBER() OVER(PARTITION BY pl.PersonID, pl.SubOrganizationID ORDER BY pl.ValidFrom DESC) AS rn 
					FROM EmployeeOrganization AS pl WITH (NOLOCK)
					INNER JOIN EmployeeWorkingInfo AS ewi WITH (NOLOCK) ON ewi.PersonID = pl.PersonID AND ewi.SREmployeeStatus = '1'
				) x
				WHERE x.rn = 1
			) a
			INNER JOIN OrganizationUnit AS ou WITH (NOLOCK) ON ou.OrganizationUnitID = a.SubOrganizationID
			GROUP BY a.SubOrganizationID, ou.OrganizationUnitCode, ou.OrganizationUnitName
			ORDER BY ou.OrganizationUnitCode ";

			return FillDataTable(esQueryType.Text, commandText, par);
		}
	}
}

