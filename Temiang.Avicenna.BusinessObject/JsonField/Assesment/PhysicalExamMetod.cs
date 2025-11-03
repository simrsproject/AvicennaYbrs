using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class PhysicalExamMetod
    {
        private AbNormalAndNotes _abNormalAndNotes;
        public AbNormalAndNotes AbNormalAndNotes
        {
            get { return _abNormalAndNotes ?? (_abNormalAndNotes = new AbNormalAndNotes()); }
            set { _abNormalAndNotes = value; }
        }
        public string Inspeksi { get; set; }
        public string Palpasi { get; set; }
        public string Perkusi { get; set; }
        public string Auskultasi { get; set; }

    }
}
