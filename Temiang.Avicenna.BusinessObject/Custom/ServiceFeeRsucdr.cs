
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceFeeRsucdr
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

        public int Ctr { get; set; }
    }

    public partial class ServiceFeeRsucdrCollection : esServiceFeeRsucdrCollection
    {
        public ServiceFeeRsucdrQuery BaseQuery() {
            var sfrq = new ServiceFeeRsucdrQuery("sfr");
            var feeq = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var parq = new ParamedicQuery("par");

            sfrq.InnerJoin(feeq).On(sfrq.TransactionNo == feeq.TransactionNo && sfrq.SequenceNo == feeq.SequenceNo && sfrq.TariffComponentID == feeq.TariffComponentID)
                .InnerJoin(parq).On(feeq.ParamedicID == parq.ParamedicID)
                .Select(sfrq,
                    feeq.ParamedicID.As("refTo_ParamedicID"),
                    parq.ParamedicName.As("refTo_ParamedicName")
                );

            return sfrq;
        }
        public bool LoadByRemunID(int RemunID) {
            var query = BaseQuery();
            query.Where(query.RemunID == RemunID);
            return this.Load(query);
        }
    }
}
