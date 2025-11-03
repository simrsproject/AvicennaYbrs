using Temiang.Dal.Interfaces; using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class SupplierQuery
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
}