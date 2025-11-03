using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class ThtMcu : BaseJsonField
    {
        public string DaunTelingaKanan { get; set; }
        public string DaunTelingaKiri { get; set; }
        public string LiangTelingaKanan { get; set; }
        public string LiangTelingaKiri { get; set; }
        public string MembranTympaniKanan { get; set; }
        public string MembranTympaniKiri { get; set; }
        public string AudiogramKanan { get; set; }
        public string AudiogramKiri { get; set; }

        private QuestionGroupAnswerValue _tht;
        public QuestionGroupAnswerValue Tht
        {
            get
            {
                if (_tht == null)
                {
                    _tht = _tht = new QuestionGroupAnswerValue();
                    _tht.QuestionGroupID = "MCU.THT1";
                }
                return _tht;

            }
            set { _tht = value; }
        }

    }
}
