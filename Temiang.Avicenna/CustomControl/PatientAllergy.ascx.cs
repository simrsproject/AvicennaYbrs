using System.Web.UI;
using Telerik.Web.UI;

namespace Temiang.Avicenna.CustomControl
{
    public partial class PatientAllergy : UserControl
    {
        public RadGrid PatientAllergyGrid
        {
            get { return grdPatientAllergy; }
        }
    }
}