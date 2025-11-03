using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class FunctionalProblem
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

    public class SpecialExamination
    {
        public string Fim { get; set; }
        public string FimDesc { get; set; }

        public string BarthelIndex { get; set; }
        public string BarthelIndexDesc { get; set; }

        public string Disphagya { get; set; }
        public string DisphagyaDesc { get; set; }

        public string Mmse { get; set; }
        public string MmseDesc { get; set; }

        public string Token { get; set; }
        public string TokenDesc { get; set; }

        public string Tadir { get; set; }
        public string TadirDesc { get; set; }

        public string BergBalance { get; set; }
        public string BergBalanceDesc { get; set; }

        public string Schober { get; set; }
        public string SchoberDesc { get; set; }

        public string Goniometer { get; set; }
        public string GoniometerDesc { get; set; }

        public string TimeUpGoTest { get; set; }
        public string WongBaker { get; set; }
        public string Vas { get; set; }
        public string Nrs { get; set; }
    }

    public class RehabilitationPe : BaseJsonField
    {

        public string GeneralCondition { get; set; }
        public string Neuromuskuloskeletal { get; set; }
        public string Cardiorespiratory { get; set; }
       

        private FunctionalProblem _functionalProblem;
        public FunctionalProblem FunctionalProblem
        {
            get { return _functionalProblem ?? (_functionalProblem = new FunctionalProblem()); }
            set { _functionalProblem = value; }
        }

        public string AncillaryExam { get; set; }

        private SpecialExamination _specialExamination;
        public SpecialExamination SpecialExamination
        {
            get { return _specialExamination ?? (_specialExamination = new SpecialExamination()); }
            set { _specialExamination = value; }
        }

        public string Summary { get; set; }
        public string Recomendation { get; set; }
        public string Evaluation { get; set; }
        public string EvaluationReason { get; set; }
    }
}
