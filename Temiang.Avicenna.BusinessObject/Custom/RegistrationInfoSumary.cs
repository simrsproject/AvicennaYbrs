namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationInfoSumary
    {
        public static string GetDocumentCheckListCountRemains(string registrationNo)
        {
            var ret = string.Empty;

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(registrationNo))
            {
                var gdc = new GuarantorDocumentChecklist();
                if (gdc.LoadByPrimaryKey(reg.GuarantorID, reg.SRRegistrationType))
                {
                    var dc = new AppStandardReferenceItem();
                    if (dc.LoadByPrimaryKey("DocumentChecklist", gdc.SRDocumentChecklist))
                    {
                        var ris = new RegistrationInfoSumary();
                        if (ris.LoadByPrimaryKey(registrationNo))
                            ret = (dc.LineNumber - ris.DocumentCheckListCount).ToString();
                    }
                }
            }

            return ret;
        }

    }
}
