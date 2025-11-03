using System;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicalRecordFileStatusMovement
    {
        public void MoveTo(string ServiceUnitId, string RegistrationNo, string UserID) {
            if (!this.LoadByPrimaryKey(ServiceUnitId, RegistrationNo))
                this.AddNew();

            this.RegistrationNo = RegistrationNo;
            this.LastPositionServiceUnitID = ServiceUnitId;
            this.LastPositionDateTime = DateTime.Now;
            this.LastPositionUserID = UserID;
        }
    }
}
