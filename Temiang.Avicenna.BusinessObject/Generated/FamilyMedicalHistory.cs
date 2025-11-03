/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/1/2016 2:55:46 PM
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
    abstract public class esFamilyMedicalHistoryCollection : esEntityCollectionWAuditLog
    {
        public esFamilyMedicalHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "FamilyMedicalHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esFamilyMedicalHistoryQuery query)
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
            this.InitQuery(query as esFamilyMedicalHistoryQuery);
        }
        #endregion

        virtual public FamilyMedicalHistory DetachEntity(FamilyMedicalHistory entity)
        {
            return base.DetachEntity(entity) as FamilyMedicalHistory;
        }

        virtual public FamilyMedicalHistory AttachEntity(FamilyMedicalHistory entity)
        {
            return base.AttachEntity(entity) as FamilyMedicalHistory;
        }

        virtual public void Combine(FamilyMedicalHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public FamilyMedicalHistory this[int index]
        {
            get
            {
                return base[index] as FamilyMedicalHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(FamilyMedicalHistory);
        }
    }

    [Serializable]
    abstract public class esFamilyMedicalHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esFamilyMedicalHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esFamilyMedicalHistory()
        {
        }

        public esFamilyMedicalHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientID, String sRMedicalDisease)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, sRMedicalDisease);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, sRMedicalDisease);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, String sRMedicalDisease)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, sRMedicalDisease);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, sRMedicalDisease);
        }

        private bool LoadByPrimaryKeyDynamic(String patientID, String sRMedicalDisease)
        {
            esFamilyMedicalHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.PatientID == patientID, query.SRMedicalDisease == sRMedicalDisease);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientID, String sRMedicalDisease)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientID", patientID);
            parms.Add("SRMedicalDisease", sRMedicalDisease);
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
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "SRMedicalDisease": this.str.SRMedicalDisease = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
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
        /// Maps to FamilyMedicalHistory.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(FamilyMedicalHistoryMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(FamilyMedicalHistoryMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to FamilyMedicalHistory.SRMedicalDisease
        /// </summary>
        virtual public System.String SRMedicalDisease
        {
            get
            {
                return base.GetSystemString(FamilyMedicalHistoryMetadata.ColumnNames.SRMedicalDisease);
            }

            set
            {
                base.SetSystemString(FamilyMedicalHistoryMetadata.ColumnNames.SRMedicalDisease, value);
            }
        }
        /// <summary>
        /// Maps to FamilyMedicalHistory.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(FamilyMedicalHistoryMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(FamilyMedicalHistoryMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to FamilyMedicalHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(FamilyMedicalHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(FamilyMedicalHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to FamilyMedicalHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(FamilyMedicalHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(FamilyMedicalHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esFamilyMedicalHistory entity)
            {
                this.entity = entity;
            }
            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }
            public System.String SRMedicalDisease
            {
                get
                {
                    System.String data = entity.SRMedicalDisease;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicalDisease = null;
                    else entity.SRMedicalDisease = Convert.ToString(value);
                }
            }
            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
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
            private esFamilyMedicalHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esFamilyMedicalHistoryQuery query)
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
                throw new Exception("esFamilyMedicalHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class FamilyMedicalHistory : esFamilyMedicalHistory
    {
    }

    [Serializable]
    abstract public class esFamilyMedicalHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return FamilyMedicalHistoryMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, FamilyMedicalHistoryMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem SRMedicalDisease
        {
            get
            {
                return new esQueryItem(this, FamilyMedicalHistoryMetadata.ColumnNames.SRMedicalDisease, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, FamilyMedicalHistoryMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, FamilyMedicalHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, FamilyMedicalHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("FamilyMedicalHistoryCollection")]
    public partial class FamilyMedicalHistoryCollection : esFamilyMedicalHistoryCollection, IEnumerable<FamilyMedicalHistory>
    {
        public FamilyMedicalHistoryCollection()
        {

        }

        public static implicit operator List<FamilyMedicalHistory>(FamilyMedicalHistoryCollection coll)
        {
            List<FamilyMedicalHistory> list = new List<FamilyMedicalHistory>();

            foreach (FamilyMedicalHistory emp in coll)
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
                return FamilyMedicalHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new FamilyMedicalHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new FamilyMedicalHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new FamilyMedicalHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public FamilyMedicalHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new FamilyMedicalHistoryQuery();
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
        public bool Load(FamilyMedicalHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public FamilyMedicalHistory AddNew()
        {
            FamilyMedicalHistory entity = base.AddNewEntity() as FamilyMedicalHistory;

            return entity;
        }
        public FamilyMedicalHistory FindByPrimaryKey(String patientID, String sRMedicalDisease)
        {
            return base.FindByPrimaryKey(patientID, sRMedicalDisease) as FamilyMedicalHistory;
        }

        #region IEnumerable< FamilyMedicalHistory> Members

        IEnumerator<FamilyMedicalHistory> IEnumerable<FamilyMedicalHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as FamilyMedicalHistory;
            }
        }

        #endregion

        private FamilyMedicalHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'FamilyMedicalHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("FamilyMedicalHistory ({PatientID, SRMedicalDisease})")]
    [Serializable]
    public partial class FamilyMedicalHistory : esFamilyMedicalHistory
    {
        public FamilyMedicalHistory()
        {
        }

        public FamilyMedicalHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return FamilyMedicalHistoryMetadata.Meta();
            }
        }

        override protected esFamilyMedicalHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new FamilyMedicalHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public FamilyMedicalHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new FamilyMedicalHistoryQuery();
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
        public bool Load(FamilyMedicalHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private FamilyMedicalHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class FamilyMedicalHistoryQuery : esFamilyMedicalHistoryQuery
    {
        public FamilyMedicalHistoryQuery()
        {

        }

        public FamilyMedicalHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "FamilyMedicalHistoryQuery";
        }
    }

    [Serializable]
    public partial class FamilyMedicalHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected FamilyMedicalHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(FamilyMedicalHistoryMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = FamilyMedicalHistoryMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(FamilyMedicalHistoryMetadata.ColumnNames.SRMedicalDisease, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = FamilyMedicalHistoryMetadata.PropertyNames.SRMedicalDisease;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(FamilyMedicalHistoryMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = FamilyMedicalHistoryMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(FamilyMedicalHistoryMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = FamilyMedicalHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(FamilyMedicalHistoryMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = FamilyMedicalHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public FamilyMedicalHistoryMetadata Meta()
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
            public const string PatientID = "PatientID";
            public const string SRMedicalDisease = "SRMedicalDisease";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string SRMedicalDisease = "SRMedicalDisease";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
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
            lock (typeof(FamilyMedicalHistoryMetadata))
            {
                if (FamilyMedicalHistoryMetadata.mapDelegates == null)
                {
                    FamilyMedicalHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (FamilyMedicalHistoryMetadata.meta == null)
                {
                    FamilyMedicalHistoryMetadata.meta = new FamilyMedicalHistoryMetadata();
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

                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMedicalDisease", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "FamilyMedicalHistory";
                meta.Destination = "FamilyMedicalHistory";
                meta.spInsert = "proc_FamilyMedicalHistoryInsert";
                meta.spUpdate = "proc_FamilyMedicalHistoryUpdate";
                meta.spDelete = "proc_FamilyMedicalHistoryDelete";
                meta.spLoadAll = "proc_FamilyMedicalHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_FamilyMedicalHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private FamilyMedicalHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
