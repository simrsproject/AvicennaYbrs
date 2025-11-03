using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.CustomControl
{
    public partial class DentalControl : System.Web.UI.UserControl
    {
        public string LU1
        {
            get { return txtLU1.Text.Trim(); }
            set { txtLU1.Text = value; }
        }

        public string LD1
        {
            get { return txtLD1.Text.Trim(); }
            set { txtLD1.Text = value; }
        }

        public string RU1
        {
            get { return txtRU1.Text.Trim(); }
            set { txtRU1.Text = value; }
        }

        public string RD1
        {
            get { return txtRD1.Text.Trim(); }
            set { txtRD1.Text = value; }
        }


        public string LU2
        {
            get { return txtLU2.Text.Trim(); }
            set { txtLU2.Text = value; }
        }

        public string LD2
        {
            get { return txtLD2.Text.Trim(); }
            set { txtLD2.Text = value; }
        }

        public string RU2
        {
            get { return txtRU2.Text.Trim(); }
            set { txtRU2.Text = value; }
        }

        public string RD2
        {
            get { return txtRD2.Text.Trim(); }
            set { txtRD2.Text = value; }
        }


        public string LU3
        {
            get { return txtLU3.Text.Trim(); }
            set { txtLU3.Text = value; }
        }

        public string LD3
        {
            get { return txtLD3.Text.Trim(); }
            set { txtLD3.Text = value; }
        }

        public string RU3
        {
            get { return txtRU3.Text.Trim(); }
            set { txtRU3.Text = value; }
        }

        public string RD3
        {
            get { return txtRD3.Text.Trim(); }
            set { txtRD3.Text = value; }
        }


        public string LU4
        {
            get { return txtLU4.Text.Trim(); }
            set { txtLU4.Text = value; }
        }

        public string LD4
        {
            get { return txtLD4.Text.Trim(); }
            set { txtLD4.Text = value; }
        }

        public string RU4
        {
            get { return txtRU4.Text.Trim(); }
            set { txtRU4.Text = value; }
        }

        public string RD4
        {
            get { return txtRD4.Text.Trim(); }
            set { txtRD4.Text = value; }
        }


        public string LU5
        {
            get { return txtLU5.Text.Trim(); }
            set { txtLU5.Text = value; }
        }

        public string LD5
        {
            get { return txtLD5.Text.Trim(); }
            set { txtLD5.Text = value; }
        }

        public string RU5
        {
            get { return txtRU5.Text.Trim(); }
            set { txtRU5.Text = value; }
        }

        public string RD5
        {
            get { return txtRD5.Text.Trim(); }
            set { txtRD5.Text = value; }
        }


        public string LU6
        {
            get { return txtLU6.Text.Trim(); }
            set { txtLU6.Text = value; }
        }

        public string LD6
        {
            get { return txtLD6.Text.Trim(); }
            set { txtLD6.Text = value; }
        }

        public string RU6
        {
            get { return txtRU6.Text.Trim(); }
            set { txtRU6.Text = value; }
        }

        public string RD6
        {
            get { return txtRD6.Text.Trim(); }
            set { txtRD6.Text = value; }
        }


        public string LU7
        {
            get { return txtLU7.Text.Trim(); }
            set { txtLU7.Text = value; }
        }

        public string LD7
        {
            get { return txtLD7.Text.Trim(); }
            set { txtLD7.Text = value; }
        }

        public string RU7
        {
            get { return txtRU7.Text.Trim(); }
            set { txtRU7.Text = value; }
        }

        public string RD7
        {
            get { return txtRD7.Text.Trim(); }
            set { txtRD7.Text = value; }
        }


        public string LU8
        {
            get { return txtLU8.Text.Trim(); }
            set { txtLU8.Text = value; }
        }

        public string LD8
        {
            get { return txtLD8.Text.Trim(); }
            set { txtLD8.Text = value; }
        }

        public string RU8
        {
            get { return txtRU8.Text.Trim(); }
            set { txtRU8.Text = value; }
        }

        public string RD8
        {
            get { return txtRD8.Text.Trim(); }
            set { txtRD8.Text = value; }
        }

        private string ResultValue(int number, string value)
        {
            return string.IsNullOrEmpty(value) ? string.Format(" <span style='color: #ffffff'>{0}</span>", number.ToString()) : value;
        }

        public string MarkupResult
        {
            get
            {
                var result = string.Format(@"<p>{0}&nbsp;&nbsp;&nbsp;{1}&nbsp;&nbsp;&nbsp;{2}&nbsp;&nbsp;&nbsp;{3}&nbsp;&nbsp;&nbsp;{4}&nbsp;&nbsp;&nbsp;{5}&nbsp;&nbsp;&nbsp;{6}&nbsp;&nbsp;&nbsp;{7}&nbsp;&nbsp;&nbsp; <span style='color: #ffffff'>|</span>&nbsp;&nbsp;&nbsp;{8}&nbsp;&nbsp;&nbsp;{9}&nbsp;&nbsp;&nbsp;{10}&nbsp;&nbsp;&nbsp;{11}&nbsp;&nbsp;&nbsp;{12}&nbsp;&nbsp;&nbsp;{13}&nbsp;&nbsp;&nbsp;{14}&nbsp;&nbsp;&nbsp;{15}<br />8&nbsp;&nbsp;&nbsp; 7&nbsp;&nbsp;&nbsp; 6&nbsp;&nbsp;&nbsp; 5&nbsp;&nbsp;&nbsp; 4&nbsp;&nbsp;&nbsp; 3&nbsp;&nbsp;&nbsp; 2&nbsp;&nbsp;&nbsp; 1&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp; 1&nbsp;&nbsp;&nbsp; 2&nbsp;&nbsp;&nbsp; 3&nbsp;&nbsp;&nbsp; 4&nbsp;&nbsp;&nbsp; 5&nbsp;&nbsp;&nbsp; 6&nbsp;&nbsp;&nbsp; 7&nbsp;&nbsp;&nbsp; 8<br />---------------------------------------------------------------------------------<br />8&nbsp;&nbsp;&nbsp; 7&nbsp;&nbsp;&nbsp; 6&nbsp;&nbsp;&nbsp; 5&nbsp;&nbsp;&nbsp; 4&nbsp;&nbsp;&nbsp; 3&nbsp;&nbsp;&nbsp; 2&nbsp;&nbsp;&nbsp; 1&nbsp;&nbsp;&nbsp; |&nbsp;&nbsp;&nbsp; 1&nbsp;&nbsp;&nbsp; 2&nbsp;&nbsp;&nbsp; 3&nbsp;&nbsp;&nbsp; 4&nbsp;&nbsp;&nbsp; 5&nbsp;&nbsp;&nbsp; 6&nbsp;&nbsp;&nbsp; 7&nbsp;&nbsp;&nbsp; 8<br />{16}&nbsp;&nbsp;&nbsp;{17}&nbsp;&nbsp;&nbsp;{18}&nbsp;&nbsp;&nbsp;{19}&nbsp;&nbsp;&nbsp;{20}&nbsp;&nbsp;&nbsp;{21}&nbsp;&nbsp;&nbsp;{22}&nbsp;&nbsp;&nbsp;{23}&nbsp;&nbsp;&nbsp; <span style='color: #ffffff'>|</span>&nbsp;&nbsp;&nbsp;{24}&nbsp;&nbsp;&nbsp;{25}&nbsp;&nbsp;&nbsp;{26}&nbsp;&nbsp;&nbsp;{27}&nbsp;&nbsp;&nbsp;{28}&nbsp;&nbsp;&nbsp;{29}&nbsp;&nbsp;&nbsp;{30}&nbsp;&nbsp;&nbsp;{31}</p><p>G = Gangrene (Terinfeksi)&nbsp;&nbsp;&nbsp; R = Radix (Akar)&nbsp;&nbsp;&nbsp; F = Filling (Tumpatan)<br />C = Caries (Berlubang)&nbsp;&nbsp;&nbsp; M = Missing (Hilang)&nbsp;&nbsp;&nbsp; P = Prothese (Gigi Palsu)</p><p></p><p></p>",
                    ResultValue(8, LU8), ResultValue(7, LU7), ResultValue(6, LU6), ResultValue(5, LU5), ResultValue(4, LU4), ResultValue(3, LU3), ResultValue(2, LU2), ResultValue(1, LU1),
                    ResultValue(1, RU1), ResultValue(2, RU2), ResultValue(3, RU3), ResultValue(4, RU4), ResultValue(5, RU5), ResultValue(6, RU6), ResultValue(7, RU7), ResultValue(8, RU8),
                    ResultValue(8, LD8), ResultValue(7, LD7), ResultValue(6, LD6), ResultValue(5, LD5), ResultValue(4, LD4), ResultValue(3, LD3), ResultValue(2, LD2), ResultValue(1, LD1),
                    ResultValue(1, RD1), ResultValue(2, RD2), ResultValue(3, RD3), ResultValue(4, RD4), ResultValue(5, RD5), ResultValue(6, RD6), ResultValue(7, RD7), ResultValue(8, RD8));
                return result;
            }
        }

        public string CustomResult
        {
            get
            {
                var result = string.Format(@"{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25};{26};{27};{28};{29};{30};{31}", 
                    LU8, LU7, LU6, LU5, LU4, LU3, LU2, LU1, LD8, LD7, LD6, LD5, LD4, LD3, LD2, LD1, RU1, RU2, RU3, RU4, RU5, RU6, RU7, RU8, RD1, RD2, RD3, RD4, RD5, RD6, RD7, RD8);

                return result;
            }
        }
    }
}