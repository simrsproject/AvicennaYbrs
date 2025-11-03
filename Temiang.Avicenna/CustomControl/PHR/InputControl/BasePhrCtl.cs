using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    public class BasePhrCtl : UserControl
    {
        public virtual object Value { get; set; }

        private Registration _cureg;
        protected Registration RegistrationCurrent
        {
            get
            {
                if (_cureg == null)
                {
                    // Control ini hnaya dipakai di page yg RegistrationNo nya dikirim dari page pemanggil
                    _cureg = new Registration();
                    _cureg.LoadByPrimaryKey(RegistrationNo);
                }
                return _cureg;
            }
        }

        #region override method
        protected string RegistrationNo
        {
            get
            {
                // Control ini hnaya dipakai di page yg RegistrationNo nya dikirim dari page pemanggil
                return Request.QueryString["regno"];
            }
        }

        private string _patientID;
        protected string PatientID
        {
            get
            {
                // Jika ada RegistrationNo maka ambil dari data Reg jangan dari query string
                // Untuk mencegah kesalahan 
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    _patientID = RegistrationCurrent.PatientID;
                }
                else if (string.IsNullOrEmpty(_patientID))
                    _patientID = Request.QueryString["patid"];

                return _patientID;
            }
        }
        protected virtual void OnPopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
        }

        protected virtual void OnSetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            throw new Exception("The method OnSetEntityValue is not implemented.");
        }


        protected virtual void OnBeforeMenuEditClick(ValidateArgs args, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
        }
        //protected virtual void OnDataModeChanged(AppEnum.DataMode oldDataMode, AppEnum.DataMode newDataMode)
        //{
        //}
        #endregion


        public void SetEntityValue(ValidateArgs args, Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            OnSetEntityValue(args, pat, reg, phr, phrLine, lastRegistrationNo);
        }

        public void PopulateEntryControl(Patient pat, Registration reg, PatientHealthRecord phr, PatientHealthRecordLine phrLine, string lastRegistrationNo)
        {
            OnPopulateEntryControl(pat, reg, phr, phrLine, lastRegistrationNo);
        }

        public void SetReadOnly(bool isReadOnly, Patient pat, Registration reg)
        {
            IsReadMode = isReadOnly;
            OnSetReadOnly(isReadOnly, pat, reg);
        }
        protected virtual void OnSetReadOnly(bool isReadOnly, Patient pat, Registration reg)
        {
        }

        /// <summary>
        /// Apakah akan diletakan pada kolom pertama pada kolom label
        /// </summary>
        public virtual bool IsLocateAtFirstColumn
        {
            get { return false; }
        }

        protected bool IsReadMode
        {
            get
            {
                if (ViewState["iro"] == null)
                    ViewState["iro"] = true;
                return (bool)ViewState["iro"];
            }
            set
            {
                ViewState["iro"] = value;
            }
        }

        /// <summary>
        /// Status New,Edit, atau Read tetapi datanya baru bisa dibaca pada event Load
        /// </summary>
        protected AppEnum.DataMode DataModeCurrent
        {
            get
            {
                var fw_hdnDataMode = (HiddenField)Helper.FindControlRecursive(Page.Master, "fw_hdnDataMode");
                if (string.IsNullOrWhiteSpace(fw_hdnDataMode.Value))
                    fw_hdnDataMode.Value = AppEnum.DataMode.Read.ToInt().ToString();
                return (AppEnum.DataMode)fw_hdnDataMode.Value.ToInt();
            }
        }

    }
}