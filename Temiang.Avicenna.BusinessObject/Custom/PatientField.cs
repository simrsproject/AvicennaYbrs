using System;
using System.Data;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientField
    {
        public static string GetValueString(string patientID, AppField.FieldNameEnum fieldName)
        {
            var patField = new PatientField();
            if (patField.LoadByPrimaryKey(patientID, fieldName.ToInt()))
            {
                return patField.ValueInString;
            }

            return null;
        }

        public static DateTime? GetValueDateTime(string patientID, AppField.FieldNameEnum fieldName)
        {
            var patField = new PatientField();
            if (patField.LoadByPrimaryKey(patientID, fieldName.ToInt()))
            {
                return patField.ValueInDatetime;
            }

            return null;
        }
        public static bool? GetValueBool(string patientID, AppField.FieldNameEnum fieldName)
        {
            var patField = new PatientField();
            if (patField.LoadByPrimaryKey(patientID, fieldName.ToInt()))
            {
                return patField.ValueInBool;
            }

            return null;
        }

        public static decimal? GetValueNumeric(string patientID, AppField.FieldNameEnum fieldName)
        {
            var patField = new PatientField();
            if (patField.LoadByPrimaryKey(patientID, fieldName.ToInt()))
            {
                return patField.ValueInNumeric;
            }

            return null;
        }
        public static byte[] GetValueImage(string patientID, AppField.FieldNameEnum fieldName)
        {
            var patField = new PatientField();
            if (patField.LoadByPrimaryKey(patientID, fieldName.ToInt()))
            {
                return patField.ValueInImage;
            }

            return null;
        }

        #region Update
        public static void Update(string patientID, AppField.FieldNameEnum fieldName, DateTime? dataDateTime,
            DateTime? valueInDateTime)
        {
            Update(patientID, fieldName, dataDateTime, "DTM", null, null, valueInDateTime, null, null);
        }

        public static void Update(string patientID, AppField.FieldNameEnum fieldName, DateTime? dataDateTime,
            string valueInString)
        {
            Update(patientID, fieldName, dataDateTime, "STR", valueInString, null, null, null, null);
        }

        public static void Update(string patientID, AppField.FieldNameEnum fieldName, DateTime? dataDateTime,
            decimal valueInNumeric)
        {
            Update(patientID, fieldName, dataDateTime, "NUM", null, valueInNumeric, null, null, null);
        }

        public static void Update(string patientID, AppField.FieldNameEnum fieldName, DateTime? dataDateTime,
            bool? valueInBool)
        {
            Update(patientID, fieldName, dataDateTime, "BIT", null, null, null,valueInBool, null);
        }

        public static void Update(string patientID, AppField.FieldNameEnum fieldName, DateTime? dataDateTime,
            byte[] valueInImage)
        {
            Update(patientID, fieldName, dataDateTime, "IMG", null, null, null,null, valueInImage);
        }
        private static void Update(string patientID, AppField.FieldNameEnum fieldName, DateTime? dataDateTime, string valueType, string valueInString, decimal? valueInNumeric, DateTime? valueInDateTime, bool? valueInBool, byte[] valueInImage)
        {
            var patField = new PatientField();
            if (!patField.LoadByPrimaryKey(patientID, fieldName.ToInt()))
            {
                patField.PatientID = patientID;
                patField.FieldID = fieldName.ToInt();
            }

            // Update
            if (patField.DataDateTime == null || patField.DataDateTime <= dataDateTime)
            {
                patField.DataDateTime = dataDateTime;

                switch (valueType)
                {
                    case "DTM":
                        {
                            if (valueInDateTime == null)
                                patField.str.ValueInDatetime = string.Empty;
                            else
                                patField.ValueInDatetime = valueInDateTime;
                            break;
                        }
                    case "STR":
                        {
                            if (valueInString == null)
                                patField.str.ValueInString = string.Empty;
                            else
                                patField.ValueInString = valueInString;
                            break;
                        }
                    case "NUM":
                        {
                            if (valueInNumeric == null)
                                patField.str.ValueInString = string.Empty;
                            else
                                patField.ValueInNumeric = valueInNumeric;
                            break;
                        }
                    case "BIT":
                    {
                        if (valueInBool == null)
                            patField.str.ValueInBool = string.Empty;
                        else
                            patField.ValueInBool = valueInBool;
                        break;
                    }
                    case "IMG":
                        {
                            patField.ValueInImage = valueInImage;
                            break;
                        }
                }

                patField.Save();
            }

        }
        #endregion
    }
}
