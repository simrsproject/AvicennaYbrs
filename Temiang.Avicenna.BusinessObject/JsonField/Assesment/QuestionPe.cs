using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class QuestionPe : BaseJsonField
    {
        private QuestionGroupAnswerValue _answerValue;
        public QuestionGroupAnswerValue Value
        {
            get
            {
                if (_answerValue == null)
                {
                    _answerValue = _answerValue = new QuestionGroupAnswerValue();
                }
                return _answerValue;

            }
            set { _answerValue = value; }
        }

    }
}
