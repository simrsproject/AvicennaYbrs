namespace Temiang.Avicenna.BusinessObject
{
    public partial class PrintSlipLog
    {
        public static bool InsertUpdate(string programId, string parameterName, string parameterValue, string userId)
        {
            var logs = new PrintSlipLogCollection();
            logs.Query.Where(logs.Query.ProgramID == programId, logs.Query.ParameterName == parameterName, logs.Query.ParameterValue == parameterValue);
            logs.LoadAll();

            if (logs.Count == 0)
            {
                var l = logs.AddNew();
                l.ProgramID = programId;
                l.ParameterName = parameterName;
                l.ParameterValue = parameterValue;
                l.PrintCount = 1;
                l.LastUpdateDateTime = System.DateTime.Now;
                l.LastUpdateByUserID = userId;
            }
            else
            {
                foreach (var l in logs)
                {
                    l.PrintCount += 1;
                    l.LastUpdateDateTime = System.DateTime.Now;
                    l.LastUpdateByUserID = userId;
                }
            }

            logs.Save();

            return true;
        }
    }
}
