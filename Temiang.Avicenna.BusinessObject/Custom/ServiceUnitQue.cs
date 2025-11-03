using System;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitQue
    {
        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }

        public string RoomName
        {
            get { return GetColumn("refToServiceRoom_RoomName").ToString(); }
            set { SetColumn("refToServiceRoom_RoomName", value); }
        }

        public static int GetNewQueNo(string serviceUnitID, string paramedicID, DateTime queDate)
        {
            var qsq = new ServiceUnitQueQuery();
            qsq.es.Top = 1;
            qsq.Where
                (
                    qsq.ServiceUnitID == serviceUnitID,
                    qsq.ParamedicID == paramedicID,
                    qsq.QueDate.Date() == queDate
                );
            qsq.OrderBy(qsq.QueNo, esOrderByDirection.Descending);
            qsq.Select(qsq.QueNo);

            DataTable dtb = qsq.LoadDataTable();
            if (dtb.Rows.Count > 0)
                return Convert.ToInt16(dtb.Rows[0][0]) + 1;

            return 1;
        }
    }
}