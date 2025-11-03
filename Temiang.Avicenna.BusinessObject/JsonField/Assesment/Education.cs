using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class Educations : BaseJsonField
    {
        public bool IsEducationToPatient { get; set; }
        public string EducationRecipient { get; set; }
        public List<Education> Items { get; set; }
    }
    public class Education
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}
