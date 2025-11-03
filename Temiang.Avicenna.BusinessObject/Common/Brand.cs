using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.BusinessObject.Common
{
    public class Brand
    {
        private string _name;
        private string _vendorName;
        private string _website;
        private string _email;

        public string Name { get { return _name; } }
        public string VendorName { get { return _vendorName; } }
        public string Website { get { return _website; } }
        public string Email { get { return _email; } }
        public Brand()
        {
            SetProps(System.Configuration.ConfigurationManager.AppSettings["VendorIdentifier"]);
        }

        public Brand(string Identifier)
        {
            SetProps(Identifier);
        }

        private void SetProps(string Identifier) {
            switch (Identifier) {
                case "1": {
                        _name = "Aviat";
                        _vendorName = "PT. Karya Prima Putera Perkasa";
                        _website = "";
                        _email = "";
                        break;
                    }
                default: {
                        _name = "Avicenna";
                        _vendorName = "PT. Solusi Cipta Integrasi";
                        _website = "http://www.sciptaintegrasi.com/";
                        _email = "mail@sciptaintegrasi.com";
                        break;
                    }
            }
        }
    }
}
