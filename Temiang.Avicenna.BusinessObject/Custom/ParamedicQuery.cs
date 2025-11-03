using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicQuery
    {

        public esQueryItem Address
        {
            get
            {
                esQueryItem address = StreetName + " " + City.RTrim() + " " + County.RTrim() + " " + ZipCode.RTrim();
                address = address.As("Address");
                return address;
            }
        }
    }

    public partial class Paramedic
    {
        public static string GetParamedicName(string paramedicID)
        {
            if (string.IsNullOrWhiteSpace(paramedicID))
                return string.Empty;

            var pa = new Paramedic();
            if (pa.LoadByPrimaryKey(paramedicID))
                return pa.ParamedicName;
            return string.Empty;
        }
    }
}