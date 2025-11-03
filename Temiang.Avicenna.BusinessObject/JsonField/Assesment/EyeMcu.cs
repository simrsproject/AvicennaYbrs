using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class EyeMcu : BaseJsonField
    {
        public string Od { get; set; }
        public string Os { get; set; }
        public string Presbyopia { get; set; }
        
        private EyeMcuTest _rightEye;
        public EyeMcuTest RightEye
        {
            get { return _rightEye ?? (_rightEye = new EyeMcuTest()); }
            set { _rightEye = value; }
        }
        private EyeMcuTest _leftEye;
        public EyeMcuTest LeftEye
        {
            get { return _leftEye ?? (_leftEye = new EyeMcuTest()); }
            set { _leftEye = value; }
        }

        public string Ishihara { get; set; }
    }
    public class EyeMcuTest
    {
        public string Visus { get; set; }
        public string Correction { get; set; }
        public string Additions { get; set; }
        public string Position { get; set; }
        public string Palpebra { get; set; }
        public string Conjunctiva { get; set; }
        public string Cornea { get; set; }
        public string Coa { get; set; }
        public string Pupil { get; set; }
        public string Iris { get; set; }
        public string Lens { get; set; }
        public string Vitreous { get; set; }
        public string Fundus { get; set; }
        public string Tio { get; set; }
        public string Campus { get; set; }
    }
}
