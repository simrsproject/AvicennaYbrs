using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class VwHasilPasienVanslab
    {
        public DateTime ValidateDateTime
        {
            get { return (DateTime)GetColumn("refTo_ValidateDateTime"); }
            set { SetColumn("refTo_ValidateDateTime", value); }
        }

        public string ValidateBy
        {
            get { return GetColumn("refTo_ValidateBy").ToString(); }
            set { SetColumn("refTo_ValidateBy", value); }
        }
    }
}
