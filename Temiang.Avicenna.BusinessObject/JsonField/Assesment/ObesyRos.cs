using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class ObesyRos : BaseJsonField
    {
        private QuestionGroupAnswerValue _obeHist;
        public QuestionGroupAnswerValue ObeHist
        {
            get
            {
                if (_obeHist == null)
                {
                    _obeHist = _obeHist = new QuestionGroupAnswerValue();
                    _obeHist.QuestionGroupID = "AO.HIST";
                }
                return _obeHist;

            }
            set { _obeHist = value; }
        }

        private QuestionGroupAnswerValue _habit;
        public QuestionGroupAnswerValue Habit
        {
            get
            {
                if (_habit == null)
                {
                    _habit = _habit = new QuestionGroupAnswerValue();
                    _habit.QuestionGroupID = "AO.HABT";
                }
                return _habit;

            }
            set { _habit = value; }
        }

        private QuestionGroupAnswerValue _parqTest;
        public QuestionGroupAnswerValue ParqTest
        {
            get
            {
                if (_parqTest == null)
                {
                    _parqTest = _parqTest = new QuestionGroupAnswerValue();
                    _parqTest.QuestionGroupID = "AO.PARQ";
                }
                return _parqTest;

            }
            set { _parqTest = value; }
        }

        private QuestionGroupAnswerValue _nutritionAn;
        public QuestionGroupAnswerValue NutritionAnalisys
        {
            get
            {
                if (_nutritionAn == null)
                {
                    _nutritionAn = _nutritionAn = new QuestionGroupAnswerValue();
                    _nutritionAn.QuestionGroupID = "AO.NUTR";
                }
                return _nutritionAn;

            }
            set { _nutritionAn = value; }
        }

        private QuestionGroupAnswerValue _fitness;
        public QuestionGroupAnswerValue Fitness
        {
            get
            {
                if (_fitness == null)
                {
                    _fitness = _fitness = new QuestionGroupAnswerValue();
                    _fitness.QuestionGroupID = "AO.FITN";
                }
                return _fitness;

            }
            set { _fitness = value; }
        }

        private QuestionGroupAnswerValue _mentalist;
        public QuestionGroupAnswerValue Mentalist
        {
            get
            {
                if (_mentalist == null)
                {
                    _mentalist = _mentalist = new QuestionGroupAnswerValue();
                    _mentalist.QuestionGroupID = "AO.MENT";
                }
                return _mentalist;

            }
            set { _mentalist = value; }
        }
    }
}
