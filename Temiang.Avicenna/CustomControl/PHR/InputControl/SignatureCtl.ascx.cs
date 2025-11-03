using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    public partial class SignatureCtl : BasePhrCtl
    {
        public override object Value
        {
            get
            {
                var imgHelper = new ImageHelper();
                return imgHelper.ToByteArray(imgHelper.ToImage(hdnImage.Value), ImageFormat.Png); ;
            }
            set
            {
                if (value != null)
                {
                    var val = (byte[])value;
                    rbImage.DataValue = val;
                    var mstream = new MemoryStream(val);
                    Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                    var imgHelper = new ImageHelper();
                    hdnImage.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
                }
            }
        }

        //public string Notes
        //{
        //    get { return txtNotes.Text; }
        //    set { txtNotes.Text = value; }
        //}

        protected override void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            Value = phrLine.BodyImage;
        }

        protected override void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            phrLine.QuestionAnswerText = string.Empty;
            if (!string.IsNullOrWhiteSpace(hdnImage.Value))
            {
                var imgHelper = new ImageHelper();
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImage.Value), new Size(332, 185));
                phrLine.BodyImage = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }
        }
    }
}