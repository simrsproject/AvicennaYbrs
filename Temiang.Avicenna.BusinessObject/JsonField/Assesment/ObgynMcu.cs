using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class ObgynMcu : BaseJsonField
    {
        private QuestionGroupAnswerValue _anamnesa;
        public QuestionGroupAnswerValue Anamnesa
        {
            get
            {
                if (_anamnesa == null)
                {
                    _anamnesa = _anamnesa = new QuestionGroupAnswerValue();
                    _anamnesa.QuestionGroupID = "MCU.OBS1";
                }
                return _anamnesa;

            }
            set { _anamnesa = value; }
        }

        private QuestionGroupAnswerValue _riwayatObs;
        public QuestionGroupAnswerValue RiwayatObs
        {
            get
            {
                if (_riwayatObs == null)
                {
                    _riwayatObs = _riwayatObs = new QuestionGroupAnswerValue();
                    _riwayatObs.QuestionGroupID = "MCU.OBS2";
                }
                return _riwayatObs;

            }
            set { _riwayatObs = value; }
        }

        private QuestionGroupAnswerValue _pemeriksaan;
        public QuestionGroupAnswerValue Pemeriksaan
        {
            get
            {
                if (_pemeriksaan == null)
                {
                    _pemeriksaan = _pemeriksaan = new QuestionGroupAnswerValue();
                    _pemeriksaan.QuestionGroupID = "MCU.OBS3";
                }
                return _pemeriksaan;

            }
            set { _pemeriksaan = value; }
        }
    }
}
