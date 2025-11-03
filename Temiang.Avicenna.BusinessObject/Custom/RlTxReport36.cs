using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport36
    {
        public string RlMasterReportItemCode
        {
            get { return GetColumn("refToRlMasterReportItem_RlMasterReportItemCode").ToString(); }
            set { SetColumn("refToRlMasterReportItem_RlMasterReportItemCode", value); }
        }

        public string RlMasterReportItemName
        {
            get { return GetColumn("refToRlMasterReportItem_RlMasterReportItemName").ToString(); }
            set { SetColumn("refToRlMasterReportItem_RlMasterReportItemName", value); }
        }

        public static void Process(int fromMonth, int toMonth, int year, string surgerySpecialty, out int khusus, out int besar, out int sedang, out int kecil, out int lain)
        {
            khusus = 0;
            besar = 0;
            sedang = 0;
            kecil = 0;
            lain = 0;

            var parValueList = new string[1];
            if (surgerySpecialty.Contains(","))
                parValueList = surgerySpecialty.Split(',');

            var episodeq = new EpisodeProcedureQuery("a");
            var regq = new RegistrationQuery("b");
            var procq = new ProcedureQuery("c");
            var bookingq = new ServiceUnitBookingQuery("d");

            episodeq.InnerJoin(regq).On(episodeq.RegistrationNo == regq.RegistrationNo);
            episodeq.InnerJoin(procq).On(episodeq.ProcedureID == procq.ProcedureID);
            episodeq.InnerJoin(bookingq).On(episodeq.BookingNo == bookingq.BookingNo);
            episodeq.Where(string.Format("<MONTH(a.ProcedureDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            episodeq.Where(string.Format("<YEAR(a.ProcedureDate) = {0}>", year.ToString()));
            episodeq.Where(episodeq.IsVoid == false, episodeq.IsFromOperatingRoom == true,
                           regq.IsVoid == false, bookingq.SmfID.In(parValueList));

            var episodes = new EpisodeProcedureCollection();
            episodes.Load(episodeq);
            foreach (var episode in episodes)
            {
                switch (episode.SRProcedureCategory)
                {
                    case "01":
                        khusus++;
                        break;
                    case "02":
                        besar++;
                        break;
                    case "03":
                        sedang++;
                        break;
                    case "04":
                        kecil++;
                        break;
                    default:
                        lain++;
                        break;
                }
            }
        }
    }
}
