/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/06/19 8:37:28 PM
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
    abstract public class esTransPrescriptionProgressCollection : esEntityCollectionWAuditLog
    {
        public esTransPrescriptionProgressCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransPrescriptionProgressCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPrescriptionProgressQuery query)
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
            this.InitQuery(query as esTransPrescriptionProgressQuery);
        }
        #endregion

        virtual public TransPrescriptionProgress DetachEntity(TransPrescriptionProgress entity)
        {
            return base.DetachEntity(entity) as TransPrescriptionProgress;
        }

        virtual public TransPrescriptionProgress AttachEntity(TransPrescriptionProgress entity)
        {
            return base.AttachEntity(entity) as TransPrescriptionProgress;
        }

        virtual public void Combine(TransPrescriptionProgressCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPrescriptionProgress this[int index]
        {
            get
            {
                return base[index] as TransPrescriptionProgress;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPrescriptionProgress);
        }
    }

    [Serializable]
    abstract public class esTransPrescriptionProgress : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPrescriptionProgressQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPrescriptionProgress()
        {
        }

        public esTransPrescriptionProgress(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String prescriptionNo, String sRPrescriptionProgress)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sRPrescriptionProgress);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sRPrescriptionProgress);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String prescriptionNo, String sRPrescriptionProgress)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sRPrescriptionProgress);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sRPrescriptionProgress);
        }

        private bool LoadByPrimaryKeyDynamic(String prescriptionNo, String sRPrescriptionProgress)
        {
            esTransPrescriptionProgressQuery query = this.GetDynamicQuery();
            query.Where(query.PrescriptionNo == prescriptionNo, query.SRPrescriptionProgress == sRPrescriptionProgress);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String prescriptionNo, String sRPrescriptionProgress)
        {
            esParameters parms = new esParameters();
            parms.Add("PrescriptionNo", prescriptionNo);
            parms.Add("SRPrescriptionProgress", sRPrescriptionProgress);
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
                        case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
                        case "SRPrescriptionProgress": this.str.SRPrescriptionProgress = (string)value; break;
                        case "ProgressNo": this.str.ProgressNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ProgressNo":

                            if (value == null || value is System.Int32)
                                this.ProgressNo = (System.Int32?)value;
                            break;
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
        /// Maps to TransPrescriptionProgress.PrescriptionNo
        /// </summary>
        virtual public System.String PrescriptionNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionProgressMetadata.ColumnNames.PrescriptionNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionProgressMetadata.ColumnNames.PrescriptionNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionProgress.SRPrescriptionProgress
        /// </summary>
        virtual public System.String SRPrescriptionProgress
        {
            get
            {
                return base.GetSystemString(TransPrescriptionProgressMetadata.ColumnNames.SRPrescriptionProgress);
            }

            set
            {
                base.SetSystemString(TransPrescriptionProgressMetadata.ColumnNames.SRPrescriptionProgress, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionProgress.ProgressNo
        /// </summary>
        virtual public System.Int32? ProgressNo
        {
            get
            {
                return base.GetSystemInt32(TransPrescriptionProgressMetadata.ColumnNames.ProgressNo);
            }

            set
            {
                base.SetSystemInt32(TransPrescriptionProgressMetadata.ColumnNames.ProgressNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionProgress.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionProgressMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionProgressMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionProgress.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionProgressMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionProgressMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransPrescriptionProgress entity)
            {
                this.entity = entity;
            }
            public System.String PrescriptionNo
            {
                get
                {
                    System.String data = entity.PrescriptionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionNo = null;
                    else entity.PrescriptionNo = Convert.ToString(value);
                }
            }
            public System.String SRPrescriptionProgress
            {
                get
                {
                    System.String data = entity.SRPrescriptionProgress;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPrescriptionProgress = null;
                    else entity.SRPrescriptionProgress = Convert.ToString(value);
                }
            }
            public System.String ProgressNo
            {
                get
                {
                    System.Int32? data = entity.ProgressNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProgressNo = null;
                    else entity.ProgressNo = Convert.ToInt32(value);
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
            private esTransPrescriptionProgress entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPrescriptionProgressQuery query)
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
                throw new Exception("esTransPrescriptionProgress can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransPrescriptionProgress : esTransPrescriptionProgress
    {
    }

    [Serializable]
    abstract public class esTransPrescriptionProgressQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionProgressMetadata.Meta();
            }
        }

        public esQueryItem PrescriptionNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionProgressMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
            }
        }

        public esQueryItem SRPrescriptionProgress
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionProgressMetadata.ColumnNames.SRPrescriptionProgress, esSystemType.String);
            }
        }

        public esQueryItem ProgressNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionProgressMetadata.ColumnNames.ProgressNo, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionProgressMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionProgressMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPrescriptionProgressCollection")]
    public partial class TransPrescriptionProgressCollection : esTransPrescriptionProgressCollection, IEnumerable<TransPrescriptionProgress>
    {
        public TransPrescriptionProgressCollection()
        {

        }

        public static implicit operator List<TransPrescriptionProgress>(TransPrescriptionProgressCollection coll)
        {
            List<TransPrescriptionProgress> list = new List<TransPrescriptionProgress>();

            foreach (TransPrescriptionProgress emp in coll)
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
                return TransPrescriptionProgressMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionProgressQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPrescriptionProgress(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPrescriptionProgress();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionProgressQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionProgressQuery();
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
        public bool Load(TransPrescriptionProgressQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransPrescriptionProgress AddNew()
        {
            TransPrescriptionProgress entity = base.AddNewEntity() as TransPrescriptionProgress;

            return entity;
        }
        public TransPrescriptionProgress FindByPrimaryKey(String prescriptionNo, String sRPrescriptionProgress)
        {
            return base.FindByPrimaryKey(prescriptionNo, sRPrescriptionProgress) as TransPrescriptionProgress;
        }

        #region IEnumerable< TransPrescriptionProgress> Members

        IEnumerator<TransPrescriptionProgress> IEnumerable<TransPrescriptionProgress>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPrescriptionProgress;
            }
        }

        #endregion

        private TransPrescriptionProgressQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPrescriptionProgress' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransPrescriptionProgress ({PrescriptionNo, SRPrescriptionProgress})")]
    [Serializable]
    public partial class TransPrescriptionProgress : esTransPrescriptionProgress
    {
        public TransPrescriptionProgress()
        {
        }

        public TransPrescriptionProgress(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionProgressMetadata.Meta();
            }
        }

        override protected esTransPrescriptionProgressQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionProgressQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionProgressQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionProgressQuery();
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
        public bool Load(TransPrescriptionProgressQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPrescriptionProgressQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransPrescriptionProgressQuery : esTransPrescriptionProgressQuery
    {
        public TransPrescriptionProgressQuery()
        {

        }

        public TransPrescriptionProgressQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPrescriptionProgressQuery";
        }
    }

    [Serializable]
    public partial class TransPrescriptionProgressMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPrescriptionProgressMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPrescriptionProgressMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionProgressMetadata.PropertyNames.PrescriptionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionProgressMetadata.ColumnNames.SRPrescriptionProgress, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionProgressMetadata.PropertyNames.SRPrescriptionProgress;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionProgressMetadata.ColumnNames.ProgressNo, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TransPrescriptionProgressMetadata.PropertyNames.ProgressNo;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionProgressMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionProgressMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionProgressMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionProgressMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransPrescriptionProgressMetadata Meta()
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
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SRPrescriptionProgress = "SRPrescriptionProgress";
            public const string ProgressNo = "ProgressNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SRPrescriptionProgress = "SRPrescriptionProgress";
            public const string ProgressNo = "ProgressNo";
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
            lock (typeof(TransPrescriptionProgressMetadata))
            {
                if (TransPrescriptionProgressMetadata.mapDelegates == null)
                {
                    TransPrescriptionProgressMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPrescriptionProgressMetadata.meta == null)
                {
                    TransPrescriptionProgressMetadata.meta = new TransPrescriptionProgressMetadata();
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

                meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRPrescriptionProgress", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProgressNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransPrescriptionProgress";
                meta.Destination = "TransPrescriptionProgress";
                meta.spInsert = "proc_TransPrescriptionProgressInsert";
                meta.spUpdate = "proc_TransPrescriptionProgressUpdate";
                meta.spDelete = "proc_TransPrescriptionProgressDelete";
                meta.spLoadAll = "proc_TransPrescriptionProgressLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPrescriptionProgressLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPrescriptionProgressMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
