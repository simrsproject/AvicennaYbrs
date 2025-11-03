using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PrintJobParameterCollection 
    {
        public PrintJobParameter FindByParameterName(string parameterName)
        {
            PrintJobParameter result = null;
            parameterName = parameterName.ToLower();
            foreach (PrintJobParameter parameter in this)
            {
                if (parameter.Name.ToLower().Equals(parameterName))
                {
                    result = parameter;
                    break;
                }
            }
            return result;
        }
        public void AddNew(string name, string valueString)
        {
            AddNew(name, valueString, null, null);
        }
        public void AddNew(string name, int valueInt)
        {
            AddNew(name, null, valueInt, null);
        }
        public void AddNew(Int64? printNo, string name, decimal? valueNumeric)
        {
            AddNew(name, null, valueNumeric, null);
        }
        public void AddNew(string name, DateTime? valueDateTime)
        {
            AddNew(name, null, null, valueDateTime);
        }
        public void AddNew(string name, string valueString, decimal? valueNumeric, DateTime? valueDateTime)
        {
            PrintJobParameter entity = base.AddNewEntity() as PrintJobParameter;
            entity.Name = name;
            entity.ValueString = valueString;
            entity.ValueNumeric = valueNumeric;
            entity.ValueDateTime = valueDateTime;
        }

    }
}
