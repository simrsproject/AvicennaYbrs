using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationInfoMedical
    {
        private int _tmpID = 0;
        public string UserName
        {
            get { return GetColumn("refToAppUser_UserName").ToString(); }
            set { SetColumn("refToAppUser_UserName", value); }
        }
        public string FormattedInfo
        {
            get { return GetColumn("FormattedInfo").ToString(); }
            set { SetColumn("FormattedInfo", value); }
        }
        public int TmpID
        {
            get { return _tmpID; }
            set { _tmpID = value; }
        }

        public int GenerateTmpID(RegistrationInfoMedicalCollection coll) {
            if (coll.Count == 0) return 1;
            var imax = (from c in coll select c.TmpID).Max();
            return imax++;
        }
    }
}
