using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class InternaMcu : BaseJsonField
    {
        private QuestionGroupAnswerValue _interna;
        public QuestionGroupAnswerValue Interna
        {
            get
            {
                if (_interna == null)
                {
                    _interna = _interna = new QuestionGroupAnswerValue();
                    _interna.QuestionGroupID = "MCU.INT1";
                }
                return _interna;

            }
            set { _interna = value; }
        }

        private QuestionGroupAnswerValue _interna2;
        public QuestionGroupAnswerValue Interna2
        {
            get
            {
                if (_interna2 == null)
                {
                    _interna2 = _interna2 = new QuestionGroupAnswerValue();
                    _interna2.QuestionGroupID = "MCU.INT2";
                }
                return _interna2;

            }
            set { _interna2 = value; }
        }
    }
}
