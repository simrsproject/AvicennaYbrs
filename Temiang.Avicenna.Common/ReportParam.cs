using System;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    [Serializable]
    public class ReportParam
    {
        private string _assemblyName;
        private string _title;
        private string _programID;
        private string _storeProcedureName;
        private string _customPivotUserID;
        private Int32 _customPivotID;

        private esParameters _parameters;

        public ReportParam(string assemblyName, string programID, string title,string storeProcedureName, esParameters parameters)
        {
            _assemblyName = assemblyName;
            _parameters = parameters;
            _title = title;
            _programID = programID;
            _storeProcedureName = storeProcedureName;
            
        }

        public ReportParam(string assemblyName, string programID, string title, string storeProcedureName, Int32 customPivotID, string customPivotUserID, esParameters parameters)
        {
            _assemblyName = assemblyName;
            _parameters = parameters;
            _title = title;
            _programID = programID;
            _storeProcedureName = storeProcedureName;
            _customPivotUserID = customPivotUserID;
            _customPivotID = customPivotID;
        }

        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }

        public esParameters Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        public string StoreProcedureName
        {
            get { return _storeProcedureName; }
            set { _storeProcedureName = value; }
        }

        public string CustomPivotUserID
        {
            get { return _customPivotUserID; }
            set { _customPivotUserID = value; }
        }

        public Int32 CustomPivotID
        {
            get { return _customPivotID; }
            set { _customPivotID = value; }
        }
    }
}
