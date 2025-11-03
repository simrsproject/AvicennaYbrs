using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class SurgeryFemaleMcu : BaseJsonField
    {
        private QuestionGroupAnswerValue _examination;
        public QuestionGroupAnswerValue Examination
        {
            get
            {
                if (_examination == null)
                {
                    _examination = _examination = new QuestionGroupAnswerValue();
                    _examination.QuestionGroupID = "MCU.SURF1";
                }
                return _examination;

            }
            set { _examination = value; }
        }

        private QuestionGroupAnswerValue _examination2;
        public QuestionGroupAnswerValue Examination2
        {
            get
            {
                if (_examination2 == null)
                {
                    _examination2 = _examination2 = new QuestionGroupAnswerValue();
                    _examination2.QuestionGroupID = "MCU.SURF2";
                }
                return _examination2;

            }
            set { _examination2 = value; }
        }
    }
}
