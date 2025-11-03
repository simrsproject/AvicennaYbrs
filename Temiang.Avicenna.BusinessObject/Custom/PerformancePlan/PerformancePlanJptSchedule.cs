namespace Temiang.Avicenna.BusinessObject
{
    public partial class PerformancePlanJptSchedule
    {
        public static int GetIsOpenSchedule(string year, string phase, string quarter)
        {
            if (phase != "inp" && quarter == string.Empty)
                return 0;

            var ppsQ = new PerformancePlanJptScheduleQuery();
            switch (phase)
            {
                case "inp":
                    {
                        ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenInput == true);
                    }
                    break;
                case "rel":
                    {
                        switch (quarter.ToInt())
                        {
                            case 1:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenRealizationQuarter1 == true);
                                break;

                            case 2:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenRealizationQuarter2 == true);
                                break;

                            case 3:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenRealizationQuarter3 == true);
                                break;

                            case 4:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenRealizationQuarter4 == true);
                                break;

                            default:
                                ppsQ.Where(ppsQ.YearPeriod == "0000");
                                break;
                        }
                    }
                    break;
                case "ver":
                    {
                        switch (quarter.ToInt())
                        {
                            case 1:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenVerificationQuarter1 == true);
                                break;

                            case 2:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenVerificationQuarter2 == true);
                                break;

                            case 3:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenVerificationQuarter3 == true);
                                break;

                            case 4:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.YearPeriod == year, ppsQ.IsOpenVerificationQuarter4 == true);
                                break;

                            default:
                                ppsQ.Where(ppsQ.YearPeriod == "0000");
                                break;
                        }
                    }
                    break;
                case "val":
                    {
                        switch (quarter.ToInt())
                        {
                            case 1:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenValidationQuarter1 == true);
                                break;

                            case 2:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenValidationQuarter2 == true);
                                break;

                            case 3:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.IsOpenValidationQuarter3 == true);
                                break;

                            case 4:
                                ppsQ.Where(ppsQ.YearPeriod == year, ppsQ.YearPeriod == year, ppsQ.IsOpenValidationQuarter4 == true);
                                break;

                            default:
                                ppsQ.Where(ppsQ.YearPeriod == "0000");
                                break;
                        }
                    }
                    break;
                default:
                    ppsQ.Where(ppsQ.YearPeriod == "0000");
                    break;
            }

            var pps = new PerformancePlanJptSchedule();
            if (pps.Load(ppsQ))
                return 1;

            return 0;
        }
    }
}
