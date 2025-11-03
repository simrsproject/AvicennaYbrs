using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class NeurologyMcu : BaseJsonField
    {
        private QuestionGroupAnswerValue _neurology;
        public QuestionGroupAnswerValue Neurology
        {
            get
            {
                if (_neurology == null)
                {
                    _neurology = _neurology = new QuestionGroupAnswerValue();
                    _neurology.QuestionGroupID = "MCU.NEU1";
                }
                return _neurology;

            }
            set { _neurology = value; }
        }
    }
}
