using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class UserHostPrinterOther
    {
        public string PrinterName
        {
            get { return GetColumn("refToPrinter_PrinterName").ToString(); }
            set { SetColumn("refToPrinter_PrinterName", value); }
        }
        public string ProgramName
        {
            get { return GetColumn("refToProgram_ProgramName").ToString(); }
            set { SetColumn("refToProgram_ProgramName", value); }
        }
    }
}
