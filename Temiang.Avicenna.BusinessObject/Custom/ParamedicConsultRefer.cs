using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicConsultRefer
    {
        public override void Save()
        {
            // Trigger Update Paramedic Team Status jika tipe consul
            if (es.IsModified && ConsultReferType == "C" && !string.IsNullOrWhiteSpace(SRConsultAnswerType))
            {
                var stdi = new AppStandardReferenceItem();
                stdi.Query.Where(stdi.Query.StandardReferenceID == "ConsultAnswerType", stdi.Query.ItemID == SRConsultAnswerType);
                stdi.Query.es.Top = 1;
                if (stdi.Query.Load())
                {
                    var pt = new ParamedicTeam();
                    if (pt.LoadByPrimaryKey(RegistrationNo, ToParamedicID, ConsultDateTime.Value.Date) && pt.SourceType == "C")
                    {
                        if (stdi.ReferenceID.ToLower() == "stop")
                            pt.EndDate = DateTime.Now;
                        else
                        {
                            pt.str.EndDate = string.Empty;
                            pt.SRParamedicTeamStatus =
                                AppParameter.GetParameterValue(AppParameter.ParameterItem
                                    .ParamedicTeamStatusSharingID); // Set hak aksesnya sama dg DPJP
                        }
                        pt.Save();
                    }
                }
            }

            base.Save();
        }

        public static ParamedicConsultRefer LastConsultReferTo(List<string> mergeRegistrations, string toParamedicID)
        {
            var consult = new ParamedicConsultRefer();
            var consulQr = new ParamedicConsultReferQuery();

            // Jawab consul harus oleh dokter bersangkutan jadi amannya pakai AppSession.UserLogin.ParamedicID
            consulQr.Where(consulQr.Or(consulQr.ToRegistrationNo.In(mergeRegistrations), consulQr.RegistrationNo.In(mergeRegistrations)), consulQr.ToParamedicID == toParamedicID);
            consulQr.es.Top = 1;
            consulQr.OrderBy(consulQr.ConsultReferNo.Descending);
            if (consult.Load(consulQr))
                return consult;
            return null;
        }
    }
}
