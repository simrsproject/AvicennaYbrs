using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class FunctionalProblem2
    {
        public bool PhysicalActivity { get; set; }
        public bool Swallowing { get; set; }
        public bool Gait { get; set; }
        public bool Cardiorespiratory { get; set; }
        public bool Defecation { get; set; }
        public bool Micturition { get; set; }
        public bool Noble { get; set; }
        public bool Execution { get; set; }
        public bool Sensory { get; set; }
        public bool Communication { get; set; }
        public bool Balance { get; set; }
        public bool Posture { get; set; }
        public bool Muscle { get; set; }
        public bool Joint { get; set; }
        public bool Locomotor { get; set; }
    }

    public class SpecialExamination2
    {
        public string Fim { get; set; }
        public string FimDesc { get; set; }

        public string BarthelIndex { get; set; }
        public string BarthelIndexDesc { get; set; }

        public string Disphagya { get; set; }
        public string DisphagyaDesc { get; set; }

        public string Mmse { get; set; }
        public string MmseDesc { get; set; }

        public string MocalnaDesc { get; set; }

        public string ReceptiveLanguage { get; set; }
        public string ReceptiveLanguageDesc { get; set; }

        public string ExpressiveLanguage { get; set; }
        public string ExpressiveLanguageDesc { get; set; }

        public string SpeakWordandSentence { get; set; }
        public string SpeakWordandSentenceDesc { get; set; }

        public string Articulation { get; set; }
        public string ArticulationDesc { get; set; }

        public string Orientation { get; set; }
        public string OrientationDesc { get; set; }

        public string Recall { get; set; }
        public string RecallDesc { get; set; }

        public string BergBalance { get; set; }
        public string BergBalanceDesc { get; set; }

        public string Schober { get; set; }
        public string SchoberDesc { get; set; }

        public string Goniometer { get; set; }
        public string GoniometerDesc { get; set; }

        public string TimeUpGoTest { get; set; }

        public string Nrs { get; set; }

        public string Gait { get; set; }
        public string GaitDesc { get; set; }

        public string MuscleStrengthDesc { get; set; }

        public string OthersDesc { get; set; }
    }

    public class RehabilitationPe2 : BaseJsonField
    {

        public string GeneralCondition { get; set; }
        public string Neuromuskuloskeletal { get; set; }
        public string Respiratory { get; set; }
       

        private FunctionalProblem2 _functionalProblem2;
        public FunctionalProblem2 FunctionalProblem2
        {
            get { return _functionalProblem2 ?? (_functionalProblem2 = new FunctionalProblem2()); }
            set { _functionalProblem2 = value; }
        }

        public string AncillaryExam { get; set; }

        private SpecialExamination2 _specialExamination2;
        public SpecialExamination2 SpecialExamination2
        {
            get { return _specialExamination2 ?? (_specialExamination2 = new SpecialExamination2()); }
            set { _specialExamination2 = value; }
        }

        public string Summary { get; set; }
        public string Evaluation { get; set; }
        public string EvaluationReason { get; set; }
    }
}
