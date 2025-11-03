using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class CardiologyMcu : BaseJsonField
    {
        private QuestionGroupAnswerValue _leher;
        public QuestionGroupAnswerValue Leher
        {
            get
            {
                if (_leher == null)
                {
                    _leher = _leher = new QuestionGroupAnswerValue();
                    _leher.QuestionGroupID = "MCU.CAR1";
                }
                return _leher;

            }
            set { _leher = value; }
        }

        private QuestionGroupAnswerValue _toraks;
        public QuestionGroupAnswerValue Toraks
        {
            get
            {
                if (_toraks == null)
                {
                    _toraks = _toraks = new QuestionGroupAnswerValue();
                    _toraks.QuestionGroupID = "MCU.CAR2";
                }
                return _toraks;

            }
            set { _toraks = value; }
        }

        private QuestionGroupAnswerValue _perut;
        public QuestionGroupAnswerValue Perut
        {
            get
            {
                if (_perut == null)
                {
                    _perut = _perut = new QuestionGroupAnswerValue();
                    _perut.QuestionGroupID = "MCU.CAR3";
                }
                return _perut;

            }
            set { _perut = value; }
        }

        private QuestionGroupAnswerValue _extremitas;
        public QuestionGroupAnswerValue Extremitas
        {
            get
            {
                if (_extremitas == null)
                {
                    _extremitas = _extremitas = new QuestionGroupAnswerValue();
                    _extremitas.QuestionGroupID = "MCU.CAR4";
                }
                return _extremitas;

            }
            set { _extremitas = value; }
        }

        private QuestionGroupAnswerValue _others;
        public QuestionGroupAnswerValue Others
        {
            get
            {
                if (_others == null)
                {
                    _others = _others = new QuestionGroupAnswerValue();
                    _others.QuestionGroupID = "MCU.CAR5";
                }
                return _others;

            }
            set { _others = value; }
        }
    }
}
