using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class NutritionCareAssessmentQuestion
    {
        #region Private

        #endregion

        #region Public

        #endregion

        #region Public Static
        public static string GetNewID()
        {
            var prefixID = "As";
            return NewIdFormatted(prefixID, GetNewID(prefixID, string.Empty));
        }

        private static int GetNewID(string prefix, string lates)
        {

            var query = new NutritionCareAssessmentQuestionQuery();
            query.Select("<max(QuestionID) LastID>");
            query.Where("<LEFT(QuestionID, " + prefix.Length + ") = '" + prefix + "'>");
            query.Where("<QuestionID > '" + lates + "'>");
            var dttbl = query.LoadDataTable();

            var iLastID = 1;
            if (dttbl.Rows.Count > 0)
            {
                if (dttbl.Rows[0][0] == null)
                {

                }
                else if (dttbl.Rows[0][0].ToString() == string.Empty)
                {

                }
                else
                {
                    var sLast = dttbl.Rows[0][0].ToString();
                    var sLast1 = sLast.Substring(prefix.Length);
                    if (IsNumeric(sLast1))
                    {
                        iLastID = System.Convert.ToInt32(sLast1);
                        iLastID++;
                    }
                    else
                    {
                        iLastID = GetNewID(prefix, sLast);
                    }
                }
            }
            else
            {
                // nothing
            }
            return iLastID;
        }

        private static string NewIdFormatted(string prefix, int id)
        {
            return prefix + id.ToString().PadLeft(3, '0');
        }

        private static bool IsNumeric(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            var chArray = expression.Trim().ToCharArray();
            return chArray.All(char.IsNumber);
        }
        #endregion
    }
}
