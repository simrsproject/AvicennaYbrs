using System;
using System.Data;
using System.Linq;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnit
    {
        ParamedicCollection _schPars;
        private int _TodayAvailableParamedics = 0;
        private bool _ApptHasScheduleWeekly = false;
        private AdjustedDisc _adjustedDisc = new AdjustedDisc();

        public string RegistrationTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RegistrationType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RegistrationType", value); }
        }

        public ParamedicCollection ScheduledParamedics
        {
            get { return _schPars; }
            set { _schPars = value; }
        }
        public int TodayAvailableParamedics
        {
            get { return _TodayAvailableParamedics; }
            set { _TodayAvailableParamedics = value; }
        }

        public bool ApptHasScheduleWeekly
        {
            get { return _ApptHasScheduleWeekly; }
            set { _ApptHasScheduleWeekly = value; }
        }

        public AdjustedDisc AdjustedDisc
        {
            get { return _adjustedDisc; }
            set { _adjustedDisc = value; }
        }

        #region Get Function
        public static string MainLocationID(string serviceUnitId)
        {
            if (string.IsNullOrEmpty(serviceUnitId))
                return string.Empty;

            string locationId;
            var sulq = new ServiceUnitLocationQuery();
            sulq.Where(sulq.ServiceUnitID == serviceUnitId, sulq.IsLocationMain == true);
            sulq.es.Top = 1;
            var sul = new ServiceUnitLocation();
            try
            {
                sul.Load(sulq);
                locationId = sul.LocationID;
            }
            catch (Exception)
            {
                locationId = string.Empty;
            }

            if (string.IsNullOrEmpty(locationId))
                return string.Empty;

            return locationId;
        }
        public string GetMainLocationId(string serviceUnitId)
        {
            if (string.IsNullOrEmpty(serviceUnitId))
                return string.Empty;

            string locationId;
            var sulq = new ServiceUnitLocationQuery();
            sulq.Where(sulq.ServiceUnitID == serviceUnitId, sulq.IsLocationMain == true);
            sulq.es.Top = 1;
            var sul = new ServiceUnitLocation();
            try
            {
                sul.Load(sulq);
                locationId = sul.LocationID;
            }
            catch (Exception)
            {
                locationId = string.Empty;
            }

            if (string.IsNullOrEmpty(locationId))
                return string.Empty;

            return locationId;
        }
        public string GetMainLocationId()
        {
            return GetMainLocationId(ServiceUnitID);
        }
        public static string GetServiceUnitName(string serviceUnitID)
        {
            var qr = new ServiceUnitQuery("su");
            qr.Where(qr.ServiceUnitID==serviceUnitID);
            qr.Select(qr.ServiceUnitName); // Just select what you need

            var su = new ServiceUnit();
            if (su.Load(qr)) return su.ServiceUnitName;

            return string.Empty;
        }

        public bool GetByBpjsCode(string BridgingID) {
            var subColl = new ServiceUnitBridgingCollection();
            subColl.Query.Where(subColl.Query.BridgingID == BridgingID);
            if (!subColl.LoadAll()) return false;
            if (this.LoadByPrimaryKey(subColl.First().ServiceUnitID)) return true;
            return false;
        }
        #endregion
    }

    public partial class ServiceUnitCollection
    {
        public DataTable InpatientBedAvailability()
        {
            var par = new esParameters();

            var commandText = @"SELECT a.*, ISNULL(b.Available, 0) AS Available
FROM
(
SELECT c.BpjsClassID, sr.RoomID, sr.RoomName AS ServiceUnitName,  COUNT(b.BedID) Capacity
FROM ServiceUnit AS su
INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.IsVisibleTo3rdParty = 1 AND b.IsActive = 1 AND b.SRBedStatus != 'BedStatus-07' and ISNULL(b.IsTemporary, 0) = 0
INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
WHERE su.SRRegistrationType = 'IPR'
GROUP BY c.BpjsClassID, sr.RoomID, sr.RoomName
) a
LEFT JOIN 
(
SELECT c.BpjsClassID, sr.RoomID, sr.RoomName AS ServiceUnitName, COUNT(b.BedID) Available
FROM ServiceUnit AS su
INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.RegistrationNo = '' AND b.IsVisibleTo3rdParty = 1 AND b.IsActive = 1 AND b.SRBedStatus != 'BedStatus-07' and ISNULL(b.IsTemporary, 0) = 0
INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
WHERE su.SRRegistrationType = 'IPR'
GROUP BY c.BpjsClassID, sr.RoomID, sr.RoomName
) b ON b.BpjsClassID = a.BpjsClassID AND b.RoomID = a.RoomID AND b.ServiceUnitName = a.ServiceUnitName";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable InpatientBedAvailabilityV2()
        {
            var par = new esParameters();

            var commandText = @"SELECT a.*, ISNULL(b.Available, 0) AS Available
FROM
(
SELECT c.BpjsClassID, sr.RoomID, sr.RoomName AS ServiceUnitName,  COUNT(b.BedID) Capacity
FROM ServiceUnit AS su
INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.IsVisibleTo3rdParty = 1 AND b.IsActive = 1 AND b.SRBedStatus != 'BedStatus-07' and ISNULL(b.IsTemporary, 0) = 0
INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
WHERE su.SRRegistrationType = 'IPR'
GROUP BY c.BpjsClassID, sr.RoomID, sr.RoomName
) a
LEFT JOIN 
(
SELECT c.BpjsClassID, sr.RoomID, sr.RoomName AS ServiceUnitName, COUNT(b.BedID) Available
FROM ServiceUnit AS su
INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.RegistrationNo = '' AND b.IsVisibleTo3rdParty = 1 AND b.IsActive = 1 AND b.SRBedStatus != 'BedStatus-07' and ISNULL(b.IsTemporary, 0) = 0
INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
WHERE su.SRRegistrationType = 'IPR'
GROUP BY c.BpjsClassID, sr.RoomID, sr.RoomName
) b ON b.BpjsClassID = a.BpjsClassID AND b.RoomID = a.RoomID AND b.ServiceUnitName = a.ServiceUnitName";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable SiranapV21()
        {
            var par = new esParameters();

            var commandText = @"SELECT a.id_tt,
	a.tt,
	a.ruang,
	a.kode_siranap,
	a.jumlah_ruang,
	a.jumlah,
	isnull(b.jumlah, 0) as terpakai,
	isnull(c.jumlah, 0) as terpakai_suspek,
	isnull(d.jumlah, 0) as terpakai_konfirmasi,
	0 as antrian,
	0 as 'prepare',
	0 as prepare_plan,
	a.jumlah - isnull(b.jumlah, 0) as kosong,
	cast(a.covid as int) as covid
FROM (
	SELECT x.id_tt, x.tt, x.ruang, x.kode_siranap, SUM(x.jumlah_ruang) as jumlah_ruang, sum(x.jumlah) as jumlah, x.covid
	FROM (
		SELECT cast(cb.BridgingID as int) as id_tt,
			cb.bridgingname as tt,
			sr.RoomName as ruang,
			cb.bridgingid as kode_siranap,
			COUNT(sr.RoomID) as jumlah_ruang,
			COUNT(b.BedID) as jumlah,
			isnull(sr.IsPandemicRoom, 0) as covid
		FROM ServiceUnit AS su
		INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
		INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.IsActive = 1 AND b.IsVisibleTo3rdParty = 1 AND b.SRBedStatus != 'BedStatus-07' and ISNULL(b.IsTemporary, 0) = 0
		INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
		INNER JOIN ClassBridging cb on cb.SRBridgingType = 'BridgingType-014' and cb.ClassID = c.ClassID
		WHERE su.SRRegistrationType = 'IPR' AND su.IsActive = 1
		GROUP BY cb.bridgingname, sr.RoomName, cb.bridgingid, sr.IsPandemicRoom
	) x
	GROUP BY x.id_tt, x.tt, x.ruang, x.covid, x.kode_siranap
) a
LEFT JOIN (
		SELECT cast(cb.BridgingID as int) as id_tt,
			cb.bridgingname as tt,
			sr.RoomName as ruang,
			cb.bridgingid as kode_siranap,
			COUNT(sr.RoomID) as jumlah_ruang,
			COUNT(b.BedID) as jumlah,
			isnull(sr.IsPandemicRoom, 0) as covid
		FROM ServiceUnit AS su
		INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
		INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.IsActive = 1 AND b.IsVisibleTo3rdParty = 1 AND b.SRBedStatus != 'BedStatus-07' and ISNULL(b.IsTemporary, 0) = 0
		INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
		INNER JOIN ClassBridging cb on cb.SRBridgingType = 'BridgingType-014' and cb.ClassID = c.ClassID
		inner join Registration r on b.RegistrationNo = r.RegistrationNo
		WHERE su.SRRegistrationType = 'IPR' AND su.IsActive = 1
		GROUP BY cb.bridgingname, sr.RoomName, cb.bridgingid, sr.IsPandemicRoom
) b on a.id_tt = b.id_tt AND a.tt = b.tt and a.ruang = b.ruang and a.kode_siranap = b.kode_siranap
LEFT JOIN (
		SELECT cast(cb.BridgingID as int) as id_tt,
			cb.bridgingname as tt,
			sr.RoomName as ruang,
			cb.bridgingid as kode_siranap,
			COUNT(sr.RoomID) as jumlah_ruang,
			COUNT(b.BedID) as jumlah,
			isnull(sr.IsPandemicRoom, 0) as covid
		FROM ServiceUnit AS su
		INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
		INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.IsActive = 1 AND b.IsVisibleTo3rdParty = 1 AND b.SRBedStatus != 'BedStatus-07' and ISNULL(b.IsTemporary, 0) = 0
		INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
		INNER JOIN ClassBridging cb on cb.SRBridgingType = 'BridgingType-014' and cb.ClassID = c.ClassID
		inner join Registration r on b.RegistrationNo = r.RegistrationNo and r.SRCovidStatus = '1'
		WHERE su.SRRegistrationType = 'IPR' AND su.IsActive = 1
		GROUP BY cb.bridgingname, sr.RoomName, cb.bridgingid, sr.IsPandemicRoom
) c on a.id_tt = c.id_tt AND a.tt = c.tt and a.ruang = c.ruang and a.kode_siranap = c.kode_siranap
LEFT JOIN (
		SELECT cast(cb.BridgingID as int) as id_tt,
			cb.bridgingname as tt,
			sr.RoomName as ruang,
			cb.bridgingid as kode_siranap,
			COUNT(sr.RoomID) as jumlah_ruang,
			COUNT(b.BedID) as jumlah,
			isnull(sr.IsPandemicRoom, 0) as covid
		FROM ServiceUnit AS su
		INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
		INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.IsActive = 1 AND b.IsVisibleTo3rdParty = 1 AND b.SRBedStatus != 'BedStatus-07' and ISNULL(b.IsTemporary, 0) = 0
		INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
		INNER JOIN ClassBridging cb on cb.SRBridgingType = 'BridgingType-014' and cb.ClassID = c.ClassID
		inner join Registration r on b.RegistrationNo = r.RegistrationNo and r.SRCovidStatus = '2'
		WHERE su.SRRegistrationType = 'IPR' AND su.IsActive = 1
		GROUP BY cb.bridgingname, sr.RoomName, cb.bridgingid, sr.IsPandemicRoom
) d on a.id_tt = d.id_tt AND a.tt = d.tt and a.ruang = d.ruang and a.kode_siranap = d.kode_siranap";

            return FillDataTable(esQueryType.Text, commandText, par);
        }

        public DataTable InpatientBedAvailabilityNonApplicares()
        {
            var par = new esParameters();

            var commandText = @"SELECT a.*, b.Available 
FROM
(
SELECT CASE WHEN ISNULL(c.BpjsClassID, '') = '' THEN c.ClassID ELSE c.BpjsClassID END AS BpjsClassID, 
	c.ClassName, su.ServiceUnitID, su.ServiceUnitName, COUNT(b.BedID) Capacity
FROM ServiceUnit AS su
INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.IsActive = 1
INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
WHERE su.SRRegistrationType = 'IPR'
GROUP BY c.BpjsClassID, c.ClassID, c.ClassName, su.ServiceUnitID, su.ServiceUnitName
) a
INNER JOIN 
(
SELECT CASE WHEN ISNULL(c.BpjsClassID, '') = '' THEN c.ClassID ELSE c.BpjsClassID END AS BpjsClassID,
	c.ClassName, su.ServiceUnitID, su.ServiceUnitName, COUNT(b.BedID) Available
FROM ServiceUnit AS su
INNER JOIN ServiceRoom AS sr ON sr.ServiceUnitID = su.ServiceUnitID AND sr.IsActive = 1
INNER JOIN Bed AS b ON b.RoomID = sr.RoomID AND b.RegistrationNo = '' AND b.IsVisibleTo3rdParty = 1 AND b.IsActive = 1
INNER JOIN Class AS c ON c.ClassID = b.ClassID AND c.IsActive = 1
WHERE su.SRRegistrationType = 'IPR'
GROUP BY c.BpjsClassID, c.ClassID, c.ClassName, su.ServiceUnitID, su.ServiceUnitName
) b ON b.BpjsClassID = a.BpjsClassID AND b.ServiceUnitID = a.ServiceUnitID AND b.ServiceUnitName = a.ServiceUnitName";

            return FillDataTable(esQueryType.Text, commandText, par);
        }
    }
}
