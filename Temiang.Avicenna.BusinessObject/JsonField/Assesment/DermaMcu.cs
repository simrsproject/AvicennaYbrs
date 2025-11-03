using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class DermaMcu : BaseJsonField
    {
        private QuestionGroupAnswerValue _anamnesa;
        public QuestionGroupAnswerValue Anamnesa
        {
            get
            {
                if (_anamnesa == null)
                {
                    _anamnesa = _anamnesa = new QuestionGroupAnswerValue();
                    _anamnesa.QuestionGroupID = "MCU.DERM1";
                }
                return _anamnesa;

            }
            set { _anamnesa = value; }
        }

        private QuestionGroupAnswerValue _kawin;
        public QuestionGroupAnswerValue Kawin
        {
            get
            {
                if (_kawin == null)
                {
                    _kawin = _kawin = new QuestionGroupAnswerValue();
                    _kawin.QuestionGroupID = "MCU.DERM2";
                }
                return _kawin;

            }
            set { _kawin = value; }
        }

        private QuestionGroupAnswerValue _abortus;
        public QuestionGroupAnswerValue Abortus
        {
            get
            {
                if (_abortus == null)
                {
                    _abortus = _abortus = new QuestionGroupAnswerValue();
                    _abortus.QuestionGroupID = "MCU.DERM3";
                }
                return _abortus;

            }
            set { _abortus = value; }
        }

        private QuestionGroupAnswerValue _others;
        public QuestionGroupAnswerValue Others
        {
            get
            {
                if (_others == null)
                {
                    _others = _others = new QuestionGroupAnswerValue();
                    _others.QuestionGroupID = "MCU.DERM4";
                }
                return _others;

            }
            set { _abortus = value; }
        }

        private QuestionGroupAnswerValue _pemeriksaan;
        public QuestionGroupAnswerValue Pemeriksaan
        {
            get
            {
                if (_pemeriksaan == null)
                {
                    _pemeriksaan = _pemeriksaan = new QuestionGroupAnswerValue();
                    _pemeriksaan.QuestionGroupID = "MCU.DERM5";
                }
                return _pemeriksaan;

            }
            set { _pemeriksaan = value; }
        }
    }
}
