using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class QuestionForm
    {
        public class QuestionFormType
        {
            public const string ServiceUnitBooking = "SUB";
            public const string Physiotherapy = "PHY";
            public const string PatientTransfer = "PTF";
            public const string PatientLetter = "LTR";
            public const string General = "GEN";
            public const string PraRegistration = "PRA";
        }

        public static string GetQuestionFormName(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return String.Empty;

            var ent = new QuestionForm();
            if (ent.LoadByPrimaryKey(id))
                return ent.QuestionFormName;

            return String.Empty;
        }
    }

    public partial class QuestionGroupCollection
    {
        public bool LoadGroupByFormID(string QuestionFormID)
        {
            var qgif = new QuestionGroupInFormQuery("qgif");
            var qg = new QuestionGroupQuery("qg");
            qg.InnerJoin(qgif).On(qgif.QuestionGroupID == qg.QuestionGroupID)
                .Where(qgif.QuestionFormID == QuestionFormID, qg.IsActive == true)
                .Select(qg)
                .OrderBy(qgif.RowIndex.Ascending);
            return this.Load(qg);
        }
    }
}
