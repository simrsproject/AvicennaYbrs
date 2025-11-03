using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class SpiroMcu : BaseJsonField
    {
        public string KeteranganKlinis { get; set; }

        public string FcvMeas { get; set; }
        public string FcvPr { get; set; }
        public int FcvPrNum { get; set; }
        public string Fcv1Meas { get; set; }
        public string Fcv1Pr { get; set; }
        public int Fcv1PrNum { get; set; }
        public string Fcv2Meas { get; set; }
        public string Fcv2Pr { get; set; }
        public int Fcv2PrNum { get; set; }
        public string Fef2Meas { get; set; }
        public string Fef2Pr { get; set; }
        public int Fef2PrNum { get; set; }
        public string FefMeas { get; set; }
        public string FefPr { get; set; }
        public int FefPrNum { get; set; }
        public string Fef25Meas { get; set; }
        public string Fef25Pr { get; set; }
        public int Fef25PrNum { get; set; }
        public string Fef50Meas { get; set; }
        public string Fef50Pr { get; set; }
        public int Fef50PrNum { get; set; }
        public string Fef75Meas { get; set; }
        public string Fef75Pr { get; set; }
        public int Fef75PrNum { get; set; }

        private QuestionGroupAnswerValue _kesan;
        public QuestionGroupAnswerValue Kesan
        {
            get
            {
                if (_kesan == null)
                {
                    _kesan = _kesan = new QuestionGroupAnswerValue();
                    _kesan.QuestionGroupID = "MCU.SPI1";
                }
                return _kesan;

            }
            set { _kesan = value; }
        }
    }
}
