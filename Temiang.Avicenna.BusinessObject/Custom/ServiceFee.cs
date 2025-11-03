
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceFee
    {
        public string ParamedicID
        {
            get { return GetColumn("refTo_ParamedicID").ToString(); }
            set { SetColumn("refTo_ParamedicID", value); }
        }
        public string ParamedicName
        {
            get { return GetColumn("refTo_ParamedicName").ToString(); }
            set { SetColumn("refTo_ParamedicName", value); }
        }
        public string ToServiceUnitID
        {
            get { return GetColumn("refTo_ToServiceUnitID").ToString(); }
            set { SetColumn("refTo_ToServiceUnitID", value); }
        }
        public decimal Qty
        {
            get { return System.Convert.ToDecimal(GetColumn("refTo_Qty")); }
            set { SetColumn("refTo_Qty", value); }
        }
        public bool IsIGD { get; set; }
    }

    public partial class ServiceFeeCollection : esServiceFeeCollection
    {
        public ServiceFeeQuery BaseQuery()
        {
            var sfq = new ServiceFeeQuery("sfr");
            var feeq = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var parq = new ParamedicQuery("par");

            sfq.InnerJoin(feeq).On(sfq.TransactionNo == feeq.TransactionNo && sfq.SequenceNo == feeq.SequenceNo && sfq.TariffComponentID == feeq.TariffComponentID)
                .InnerJoin(parq).On(feeq.ParamedicID == parq.ParamedicID)
                .Select(sfq,
                    feeq.ParamedicID.As("refTo_ParamedicID"),
                    parq.ParamedicName.As("refTo_ParamedicName")
                );

            return sfq;
        }
        public bool LoadByRemunID(int RemunID)
        {
            var query = BaseQuery();
            query.Where(query.RemunID == RemunID);
            return this.Load(query);
        }
    }
}
