/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 05/02/2024 18:57:01
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    abstract public class esDiagnoseCollection : esEntityCollectionWAuditLog
    {
        public esDiagnoseCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "DiagnoseCollection";
        }

        #region Query Logic
        protected void InitQuery(esDiagnoseQuery query)
        {
            query.OnLoadDelegate = this.OnQueryLoaded;
            query.es2.Connection = ((IEntityCollection)this).Connection;
        }

        protected bool OnQueryLoaded(DataTable table)
        {
            this.PopulateCollection(table);
            return (this.RowCount > 0) ? true : false;
        }

        protected override void HookupQuery(esDynamicQuery query)
        {
            this.InitQuery(query as esDiagnoseQuery);
        }
        #endregion

        virtual public Diagnose DetachEntity(Diagnose entity)
        {
            return base.DetachEntity(entity) as Diagnose;
        }

        virtual public Diagnose AttachEntity(Diagnose entity)
        {
            return base.AttachEntity(entity) as Diagnose;
        }

        virtual public void Combine(DiagnoseCollection collection)
        {
            base.Combine(collection);
        }

        new public Diagnose this[int index]
        {
            get
            {
                return base[index] as Diagnose;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Diagnose);
        }
    }

    [Serializable]
    abstract public class esDiagnose : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDiagnoseQuery GetDynamicQuery()
        {
            return null;
        }

        public esDiagnose()
        {
        }

        public esDiagnose(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String diagnoseID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(diagnoseID);
            else
                return LoadByPrimaryKeyStoredProcedure(diagnoseID);
        }

        /// <summary>
        /// Loads an entity by primary key
        /// </summary>
        /// <remarks>
        /// Requires primary keys be defined on all tables.
        /// If a table does not have a primary key set,
        /// this method will not compile.
        /// </remarks>
        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String diagnoseID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(diagnoseID);
            else
                return LoadByPrimaryKeyStoredProcedure(diagnoseID);
        }

        private bool LoadByPrimaryKeyDynamic(String diagnoseID)
        {
            esDiagnoseQuery query = this.GetDynamicQuery();
            query.Where(query.DiagnoseID == diagnoseID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String diagnoseID)
        {
            esParameters parms = new esParameters();
            parms.Add("DiagnoseID", diagnoseID);
            return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
        }
        #endregion

        #region Properties

        public override void SetProperties(IDictionary values)
        {
            foreach (string propertyName in values.Keys)
            {
                this.SetProperty(propertyName, values[propertyName]);
            }
        }

        public override void SetProperty(string name, object value)
        {
            if (this.Row == null) this.AddNew();

            esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
            if (col != null)
            {
                if (value == null || value is System.String)
                {
                    // Use the strongly typed property
                    switch (name)
                    {
                        case "DiagnoseID": this.str.DiagnoseID = (string)value; break;
                        case "DtdNo": this.str.DtdNo = (string)value; break;
                        case "DiagnoseName": this.str.DiagnoseName = (string)value; break;
                        case "IsChronicDisease": this.str.IsChronicDisease = (string)value; break;
                        case "IsDisease": this.str.IsDisease = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "Synonym": this.str.Synonym = (string)value; break;
                        case "IsSatuSehat": this.str.IsSatuSehat = (string)value; break;
                        case "IM": this.str.IM = (string)value; break;
                        case "ValidCode": this.str.ValidCode = (string)value; break;
                        case "Asterisk": this.str.Asterisk = (string)value; break;
                        case "Accpdx": this.str.Asterisk = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsChronicDisease":

                            if (value == null || value is System.Boolean)
                                this.IsChronicDisease = (System.Boolean?)value;
                            break;
                        case "IsDisease":

                            if (value == null || value is System.Boolean)
                                this.IsDisease = (System.Boolean?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsSatuSehat":

                            if (value == null || value is System.Boolean)
                                this.IsSatuSehat = (System.Boolean?)value;
                            break;
                        case "IM":

                            if (value == null || value is System.Boolean)
                                this.IM = (System.Boolean?)value;
                            break;
                        case "ValidCode":

                            if (value == null || value is System.Boolean)
                                this.ValidCode = (System.Boolean?)value;
                            break;
                        case "Asterisk":

                            if (value == null || value is System.Boolean)
                                this.Asterisk = (System.Boolean?)value;
                            break;
                        case "Accpdx":

                            if (value == null || value is System.Boolean)
                                this.Accpdx = (System.Boolean?)value;
                            break;

                        default:
                            break;
                    }
                }
            }
            else if (this.Row.Table.Columns.Contains(name))
            {
                this.Row[name] = value;
            }
            else
            {
                throw new Exception("SetProperty Error: '" + name + "' not found");
            }
        }

        /// <summary>
        /// Maps to Diagnose.DiagnoseID
        /// </summary>
        virtual public System.String DiagnoseID
        {
            get
            {
                return base.GetSystemString(DiagnoseMetadata.ColumnNames.DiagnoseID);
            }

            set
            {
                base.SetSystemString(DiagnoseMetadata.ColumnNames.DiagnoseID, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.DtdNo
        /// </summary>
        virtual public System.String DtdNo
        {
            get
            {
                return base.GetSystemString(DiagnoseMetadata.ColumnNames.DtdNo);
            }

            set
            {
                base.SetSystemString(DiagnoseMetadata.ColumnNames.DtdNo, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.DiagnoseName
        /// </summary>
        virtual public System.String DiagnoseName
        {
            get
            {
                return base.GetSystemString(DiagnoseMetadata.ColumnNames.DiagnoseName);
            }

            set
            {
                base.SetSystemString(DiagnoseMetadata.ColumnNames.DiagnoseName, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.IsChronicDisease
        /// </summary>
        virtual public System.Boolean? IsChronicDisease
        {
            get
            {
                return base.GetSystemBoolean(DiagnoseMetadata.ColumnNames.IsChronicDisease);
            }

            set
            {
                base.SetSystemBoolean(DiagnoseMetadata.ColumnNames.IsChronicDisease, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.IsDisease
        /// </summary>
        virtual public System.Boolean? IsDisease
        {
            get
            {
                return base.GetSystemBoolean(DiagnoseMetadata.ColumnNames.IsDisease);
            }

            set
            {
                base.SetSystemBoolean(DiagnoseMetadata.ColumnNames.IsDisease, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(DiagnoseMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(DiagnoseMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DiagnoseMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DiagnoseMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DiagnoseMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DiagnoseMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.Synonym
        /// </summary>
        virtual public System.String Synonym
        {
            get
            {
                return base.GetSystemString(DiagnoseMetadata.ColumnNames.Synonym);
            }

            set
            {
                base.SetSystemString(DiagnoseMetadata.ColumnNames.Synonym, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.IsSatuSehat
        /// </summary>
        virtual public System.Boolean? IsSatuSehat
        {
            get
            {
                return base.GetSystemBoolean(DiagnoseMetadata.ColumnNames.IsSatuSehat);
            }

            set
            {
                base.SetSystemBoolean(DiagnoseMetadata.ColumnNames.IsSatuSehat, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.IM
        /// </summary>
        virtual public System.Boolean? IM
        {
            get
            {
                return base.GetSystemBoolean(DiagnoseMetadata.ColumnNames.IM);
            }

            set
            {
                base.SetSystemBoolean(DiagnoseMetadata.ColumnNames.IM, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.ValidCode
        /// </summary>
        virtual public System.Boolean? ValidCode
        {
            get
            {
                return base.GetSystemBoolean(DiagnoseMetadata.ColumnNames.ValidCode);
            }

            set
            {
                base.SetSystemBoolean(DiagnoseMetadata.ColumnNames.ValidCode, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.Asterisk
        /// </summary>
        virtual public System.Boolean? Asterisk
        {
            get
            {
                return base.GetSystemBoolean(DiagnoseMetadata.ColumnNames.Asterisk);
            }

            set
            {
                base.SetSystemBoolean(DiagnoseMetadata.ColumnNames.Asterisk, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.Accpdx
        /// </summary>
        virtual public System.Boolean? Accpdx
        {
            get
            {
                return base.GetSystemBoolean(DiagnoseMetadata.ColumnNames.Accpdx);
            }

            set
            {
                base.SetSystemBoolean(DiagnoseMetadata.ColumnNames.Accpdx, value);
            }
        }

        #endregion

        #region String Properties

        /// <summary>
        /// Converts an entity's properties to
        /// and from strings.
        /// </summary>
        /// <remarks>
        /// The str properties Get and Set provide easy conversion
        /// between a string and a property's data type. Not all
        /// data types will get a str property.
        /// </remarks>
        /// <example>
        /// Set a datetime from a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// entity.str.HireDate = "2007-01-01 00:00:00";
        /// entity.Save();
        /// </code>
        /// Get a datetime as a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// string theDate = entity.str.HireDate;
        /// </code>
        /// </example>
        [BrowsableAttribute(false)]
        public esStrings str
        {
            get
            {
                if (esstrings == null)
                {
                    esstrings = new esStrings(this);
                }
                return esstrings;
            }
        }

        [Serializable]
        sealed public class esStrings
        {
            public esStrings(esDiagnose entity)
            {
                this.entity = entity;
            }
            public System.String DiagnoseID
            {
                get
                {
                    System.String data = entity.DiagnoseID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseID = null;
                    else entity.DiagnoseID = Convert.ToString(value);
                }
            }
            public System.String DtdNo
            {
                get
                {
                    System.String data = entity.DtdNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DtdNo = null;
                    else entity.DtdNo = Convert.ToString(value);
                }
            }
            public System.String DiagnoseName
            {
                get
                {
                    System.String data = entity.DiagnoseName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseName = null;
                    else entity.DiagnoseName = Convert.ToString(value);
                }
            }
            public System.String IsChronicDisease
            {
                get
                {
                    System.Boolean? data = entity.IsChronicDisease;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsChronicDisease = null;
                    else entity.IsChronicDisease = Convert.ToBoolean(value);
                }
            }
            public System.String IsDisease
            {
                get
                {
                    System.Boolean? data = entity.IsDisease;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDisease = null;
                    else entity.IsDisease = Convert.ToBoolean(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
                }
            }
            public System.String LastUpdateDateTime
            {
                get
                {
                    System.DateTime? data = entity.LastUpdateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
                    else entity.LastUpdateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String LastUpdateByUserID
            {
                get
                {
                    System.String data = entity.LastUpdateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
                    else entity.LastUpdateByUserID = Convert.ToString(value);
                }
            }
            public System.String Synonym
            {
                get
                {
                    System.String data = entity.Synonym;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Synonym = null;
                    else entity.Synonym = Convert.ToString(value);
                }
            }
            public System.String IsSatuSehat
            {
                get
                {
                    System.Boolean? data = entity.IsSatuSehat;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsSatuSehat = null;
                    else entity.IsSatuSehat = Convert.ToBoolean(value);
                }
            }
            public System.String IM
            {
                get
                {
                    System.Boolean? data = entity.IM;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IM = null;
                    else entity.IM = Convert.ToBoolean(value);
                }
            }
            public System.String ValidCode
            {
                get
                {
                    System.Boolean? data = entity.ValidCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ValidCode = null;
                    else entity.ValidCode = Convert.ToBoolean(value);
                }
            }
            public System.String Asterisk
            {
                get
                {
                    System.Boolean? data = entity.Asterisk;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Asterisk = null;
                    else entity.Asterisk = Convert.ToBoolean(value);
                }
            }
            public System.String Accpdx
            {
                get
                {
                    System.Boolean? data = entity.Accpdx;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Accpdx = null;
                    else entity.Accpdx = Convert.ToBoolean(value);
                }
            }
            private esDiagnose entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDiagnoseQuery query)
        {
            query.OnLoadDelegate = this.OnQueryLoaded;
            query.es2.Connection = ((IEntity)this).Connection;
        }

        [System.Diagnostics.DebuggerNonUserCode]
        protected bool OnQueryLoaded(DataTable table)
        {
            bool dataFound = this.PopulateEntity(table);

            if (this.RowCount > 1)
            {
                throw new Exception("esDiagnose can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Diagnose : esDiagnose
    {
    }

    [Serializable]
    abstract public class esDiagnoseQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return DiagnoseMetadata.Meta();
            }
        }

        public esQueryItem DiagnoseID
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.DiagnoseID, esSystemType.String);
            }
        }

        public esQueryItem DtdNo
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.DtdNo, esSystemType.String);
            }
        }

        public esQueryItem DiagnoseName
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.DiagnoseName, esSystemType.String);
            }
        }

        public esQueryItem IsChronicDisease
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.IsChronicDisease, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDisease
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.IsDisease, esSystemType.Boolean);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem Synonym
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.Synonym, esSystemType.String);
            }
        }

        public esQueryItem IsSatuSehat
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.IsSatuSehat, esSystemType.Boolean);
            }
        }
        public esQueryItem IM
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.IM, esSystemType.Boolean);
            }
        }
        public esQueryItem ValidCode
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.ValidCode, esSystemType.Boolean);
            }
        }
        public esQueryItem Asterisk
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.Asterisk, esSystemType.Boolean);
            }
        }
        public esQueryItem Accpdx
        {
            get
            {
                return new esQueryItem(this, DiagnoseMetadata.ColumnNames.Accpdx, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DiagnoseCollection")]
    public partial class DiagnoseCollection : esDiagnoseCollection, IEnumerable<Diagnose>
    {
        public DiagnoseCollection()
        {

        }

        public static implicit operator List<Diagnose>(DiagnoseCollection coll)
        {
            List<Diagnose> list = new List<Diagnose>();

            foreach (Diagnose emp in coll)
            {
                list.Add(emp);
            }

            return list;
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DiagnoseMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DiagnoseQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Diagnose(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Diagnose();
        }

        #endregion

        [BrowsableAttribute(false)]
        public DiagnoseQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DiagnoseQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one record was loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(DiagnoseQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Diagnose AddNew()
        {
            Diagnose entity = base.AddNewEntity() as Diagnose;

            return entity;
        }
        public Diagnose FindByPrimaryKey(String diagnoseID)
        {
            return base.FindByPrimaryKey(diagnoseID) as Diagnose;
        }

        #region IEnumerable< Diagnose> Members

        IEnumerator<Diagnose> IEnumerable<Diagnose>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Diagnose;
            }
        }

        #endregion

        private DiagnoseQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Diagnose' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Diagnose ({DiagnoseID})")]
    [Serializable]
    public partial class Diagnose : esDiagnose
    {
        public Diagnose()
        {
        }

        public Diagnose(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DiagnoseMetadata.Meta();
            }
        }

        override protected esDiagnoseQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DiagnoseQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public DiagnoseQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DiagnoseQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one row is loaded.
        /// For an entity, an exception will be thrown
        /// if more than one row is loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(DiagnoseQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DiagnoseQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class DiagnoseQuery : esDiagnoseQuery
    {
        public DiagnoseQuery()
        {

        }

        public DiagnoseQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DiagnoseQuery";
        }
    }

    [Serializable]
    public partial class DiagnoseMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DiagnoseMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.DiagnoseID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DiagnoseMetadata.PropertyNames.DiagnoseID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.DtdNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = DiagnoseMetadata.PropertyNames.DtdNo;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.DiagnoseName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = DiagnoseMetadata.PropertyNames.DiagnoseName;
            c.CharacterMaxLength = 500;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.IsChronicDisease, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DiagnoseMetadata.PropertyNames.IsChronicDisease;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.IsDisease, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DiagnoseMetadata.PropertyNames.IsDisease;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DiagnoseMetadata.PropertyNames.IsActive;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DiagnoseMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = DiagnoseMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.Synonym, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = DiagnoseMetadata.PropertyNames.Synonym;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.IsSatuSehat, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DiagnoseMetadata.PropertyNames.IsSatuSehat;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.IM, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DiagnoseMetadata.PropertyNames.IM;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.ValidCode, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DiagnoseMetadata.PropertyNames.ValidCode;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.Asterisk, 12, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DiagnoseMetadata.PropertyNames.Asterisk;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DiagnoseMetadata.ColumnNames.Accpdx, 13, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DiagnoseMetadata.PropertyNames.Accpdx;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public DiagnoseMetadata Meta()
        {
            return meta;
        }

        public Guid DataID
        {
            get { return base._dataID; }
        }

        public bool MultiProviderMode
        {
            get { return false; }
        }

        public esColumnMetadataCollection Columns
        {
            get { return base._columns; }
        }

        #region ColumnNames
        public class ColumnNames
        {
            public const string DiagnoseID = "DiagnoseID";
            public const string DtdNo = "DtdNo";
            public const string DiagnoseName = "DiagnoseName";
            public const string IsChronicDisease = "IsChronicDisease";
            public const string IsDisease = "IsDisease";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Synonym = "Synonym";
            public const string IsSatuSehat = "IsSatuSehat";
            public const string IM = "IM";
            public const string ValidCode = "ValidCode";
            public const string Asterisk = "Asterisk";
            public const string Accpdx = "Accpdx";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string DiagnoseID = "DiagnoseID";
            public const string DtdNo = "DtdNo";
            public const string DiagnoseName = "DiagnoseName";
            public const string IsChronicDisease = "IsChronicDisease";
            public const string IsDisease = "IsDisease";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Synonym = "Synonym";
            public const string IsSatuSehat = "IsSatuSehat";
            public const string IM = "IM";
            public const string ValidCode = "ValidCode";
            public const string Asterisk = "Asterisk";
            public const string Accpdx = "Accpdx";
        }
        #endregion

        public esProviderSpecificMetadata GetProviderMetadata(string mapName)
        {
            MapToMeta mapMethod = mapDelegates[mapName];

            if (mapMethod != null)
                return mapMethod(mapName);
            else
                return null;
        }

        #region MAP esDefault

        static private int RegisterDelegateesDefault()
        {
            // This is only executed once per the life of the application
            lock (typeof(DiagnoseMetadata))
            {
                if (DiagnoseMetadata.mapDelegates == null)
                {
                    DiagnoseMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DiagnoseMetadata.meta == null)
                {
                    DiagnoseMetadata.meta = new DiagnoseMetadata();
                }

                MapToMeta mapMethod = new MapToMeta(meta.esDefault);
                mapDelegates.Add("esDefault", mapMethod);
                mapMethod("esDefault");
            }
            return 0;
        }

        private esProviderSpecificMetadata esDefault(string mapName)
        {
            if (!_providerMetadataMaps.ContainsKey(mapName))
            {
                esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

                meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DtdNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiagnoseName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsChronicDisease", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDisease", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Synonym", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsSatuSehat", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IM", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ValidCode", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Asterisk", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Accpdx", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "Diagnose";
                meta.Destination = "Diagnose";
                meta.spInsert = "proc_DiagnoseInsert";
                meta.spUpdate = "proc_DiagnoseUpdate";
                meta.spDelete = "proc_DiagnoseDelete";
                meta.spLoadAll = "proc_DiagnoseLoadAll";
                meta.spLoadByPrimaryKey = "proc_DiagnoseLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DiagnoseMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
