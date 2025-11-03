using System.Data.SqlClient;
using System.Reflection;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class AppField
    {
        public enum FieldNameEnum
        {
            /// <summary>
            /// First day of last menstruation
            /// </summary>
            Fdolm = 1,
            IsBreastFeeding = 2,
            IsBreastFeedingL6M = 3,
            IsBreastFeedingM6M = 4,
            IsPregnant = 5
        }

    }
}
