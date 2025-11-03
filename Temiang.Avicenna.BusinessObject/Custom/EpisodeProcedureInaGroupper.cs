namespace Temiang.Avicenna.BusinessObject
{
    public partial class EpisodeProcedureInaGroupper
    {
        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }

        public string CreatedByParamedicId
        {
            get { return GetColumn("refToAppUser_ParamedicID").ToString(); }
            set { SetColumn("refToAppUser_ParamedicID", value); }
        }

        public string CreateByUserName
        {
            get { return GetColumn("refToAppUser_CreateByUserID").ToString(); }
            set { SetColumn("refToAppUser_CreateByUserID", value); }
        }

        public string LastUpdateByUserName
        {
            get { return GetColumn("refToAppUser_LastUpdateByUserID").ToString(); }
            set { SetColumn("refToAppUser_LastUpdateByUserID", value); }
        }
    } 
}
