using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class SurgeryMaleMcu : BaseJsonField
    {
        private QuestionGroupAnswerValue _examination;
        public QuestionGroupAnswerValue Examination
        {
            get
            {
                if (_examination == null)
                {
                    _examination = _examination = new QuestionGroupAnswerValue();
                    _examination.QuestionGroupID = "MCU.SURM";
                }
                return _examination;

            }
            set { _examination = value; }
        }
    }
}
