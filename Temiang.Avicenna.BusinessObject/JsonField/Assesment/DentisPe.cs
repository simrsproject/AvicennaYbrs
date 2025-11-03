using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class DentisPe : BaseJsonField
    {
        public string ExtraOral { get; set; }

        private IntraOral _intraOral ;
        public IntraOral IntraOral
        {
            get { return _intraOral ?? (_intraOral = new IntraOral()); }
            set { _intraOral = value; }
        }
        public string Notes { get; set; }
        public string NutritionSkrinning { get; set; }
        public string ActionAndTherapy { get; set; }
    }

    public class IntraOral
    {
        public string Bibir { get; set; }
        public string Palatum { get; set; }
        public string Lidah { get; set; }
        public string DasarMulut { get; set; }
        public string Vestibulum { get; set; }
        public string Ginggiva { get; set; }
        public string MukosaBukal { get; set; }
        public string MukosaLingual { get; set; }
        public string Tonsil { get; set; }
        public string Teeth { get; set; }
        public string Other { get; set; }

    }
}
