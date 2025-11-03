using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitBridging
    {
        public string BridgingTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
    }

    public partial class ServiceUnitBridgingCollection {
        public bool LoadByBridgingID(string BridgingID) {
            //var subColl = new ServiceUnitBridgingCollection();
            this.QueryReset();
            this.Query.Where(this.Query.SRBridgingType == "BridgingType-001", this.Query.BridgingID == BridgingID);
            return this.LoadAll();
        }
        public bool LoadByServiceUnitID(string ServiceUnitID)
        {
            //var subColl = new ServiceUnitBridgingCollection();
            this.QueryReset();
            this.Query.Where(this.Query.SRBridgingType == "BridgingType-001", this.Query.ServiceUnitID == ServiceUnitID);
            return this.LoadAll();
        }
    }
}
