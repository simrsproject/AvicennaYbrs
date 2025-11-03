/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/08/19 2:14:53 PM
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
    abstract public class esTransPrescriptionHighAlertCollection : esEntityCollectionWAuditLog
    {
        public esTransPrescriptionHighAlertCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "TransPrescriptionHighAlertCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPrescriptionHighAlertQuery query)
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
            this.InitQuery(query as esTransPrescriptionHighAlertQuery);
        }
        #endregion

        virtual public TransPrescriptionHighAlert DetachEntity(TransPrescriptionHighAlert entity)
        {
            return base.DetachEntity(entity) as TransPrescriptionHighAlert;
        }

        virtual public TransPrescriptionHighAlert AttachEntity(TransPrescriptionHighAlert entity)
        {
            return base.AttachEntity(entity) as TransPrescriptionHighAlert;
        }

        virtual public void Combine(TransPrescriptionHighAlertCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPrescriptionHighAlert this[int index]
        {
            get
            {
                return base[index] as TransPrescriptionHighAlert;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPrescriptionHighAlert);
        }
    }

    [Serializable]
    abstract public class esTransPrescriptionHighAlert : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPrescriptionHighAlertQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPrescriptionHighAlert()
        {
        }

        public esTransPrescriptionHighAlert(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String prescriptionNo, String sRPrescriptionHAlert)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sRPrescriptionHAlert);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sRPrescriptionHAlert);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String prescriptionNo, String sRPrescriptionHAlert)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sRPrescriptionHAlert);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sRPrescriptionHAlert);
        }

        private bool LoadByPrimaryKeyDynamic(String prescriptionNo, String sRPrescriptionHAlert)
        {
            esTransPrescriptionHighAlertQuery query = this.GetDynamicQuery();
            query.Where(query.PrescriptionNo == prescriptionNo, query.SRPrescriptionHAlert == sRPrescriptionHAlert);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String prescriptionNo, String sRPrescriptionHAlert)
        {
            esParameters parms = new esParameters();
            parms.Add("PrescriptionNo", prescriptionNo);
            parms.Add("SRPrescriptionHAlert", sRPrescriptionHAlert);
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
                        case "SRPrescriptionHAlert": this.str.SRPrescriptionHAlert = (string)value; break;
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
        /// Maps to TransPrescriptionHighAlert.PrescriptionNo
        /// </summary>
        virtual public System.String PrescriptionNo
        {
            get
            {
                return base.GetSystemString(TransPrescriptionHighAlertMetadata.ColumnNames.PrescriptionNo);
            }

            set
            {
                base.SetSystemString(TransPrescriptionHighAlertMetadata.ColumnNames.PrescriptionNo, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionHighAlert.SRPrescriptionHAlert
        /// </summary>
        virtual public System.String SRPrescriptionHAlert
        {
            get
            {
                return base.GetSystemString(TransPrescriptionHighAlertMetadata.ColumnNames.SRPrescriptionHAlert);
            }

            set
            {
                base.SetSystemString(TransPrescriptionHighAlertMetadata.ColumnNames.SRPrescriptionHAlert, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionHighAlert.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionHighAlertMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionHighAlertMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to TransPrescriptionHighAlert.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionHighAlertMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionHighAlertMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransPrescriptionHighAlert entity)
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
            public System.String SRPrescriptionHAlert
            {
                get
                {
                    System.String data = entity.SRPrescriptionHAlert;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRPrescriptionHAlert = null;
                    else entity.SRPrescriptionHAlert = Convert.ToString(value);
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
            private esTransPrescriptionHighAlert entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPrescriptionHighAlertQuery query)
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
                throw new Exception("esTransPrescriptionHighAlert can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class TransPrescriptionHighAlert : esTransPrescriptionHighAlert
    {
    }

    [Serializable]
    abstract public class esTransPrescriptionHighAlertQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionHighAlertMetadata.Meta();
            }
        }

        public esQueryItem PrescriptionNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionHighAlertMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
            }
        }

        public esQueryItem SRPrescriptionHAlert
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionHighAlertMetadata.ColumnNames.SRPrescriptionHAlert, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionHighAlertMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionHighAlertMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPrescriptionHighAlertCollection")]
    public partial class TransPrescriptionHighAlertCollection : esTransPrescriptionHighAlertCollection, IEnumerable<TransPrescriptionHighAlert>
    {
        public TransPrescriptionHighAlertCollection()
        {

        }

        public static implicit operator List<TransPrescriptionHighAlert>(TransPrescriptionHighAlertCollection coll)
        {
            List<TransPrescriptionHighAlert> list = new List<TransPrescriptionHighAlert>();

            foreach (TransPrescriptionHighAlert emp in coll)
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
                return TransPrescriptionHighAlertMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionHighAlertQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPrescriptionHighAlert(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPrescriptionHighAlert();
        }

        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionHighAlertQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionHighAlertQuery();
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
        public bool Load(TransPrescriptionHighAlertQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public TransPrescriptionHighAlert AddNew()
        {
            TransPrescriptionHighAlert entity = base.AddNewEntity() as TransPrescriptionHighAlert;

            return entity;
        }
        public TransPrescriptionHighAlert FindByPrimaryKey(String prescriptionNo, String sRPrescriptionHAlert)
        {
            return base.FindByPrimaryKey(prescriptionNo, sRPrescriptionHAlert) as TransPrescriptionHighAlert;
        }

        #region IEnumerable< TransPrescriptionHighAlert> Members

        IEnumerator<TransPrescriptionHighAlert> IEnumerable<TransPrescriptionHighAlert>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPrescriptionHighAlert;
            }
        }

        #endregion

        private TransPrescriptionHighAlertQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPrescriptionHighAlert' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TransPrescriptionHighAlert ({PrescriptionNo, SRPrescriptionHAlert})")]
    [Serializable]
    public partial class TransPrescriptionHighAlert : esTransPrescriptionHighAlert
    {
        public TransPrescriptionHighAlert()
        {
        }

        public TransPrescriptionHighAlert(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionHighAlertMetadata.Meta();
            }
        }

        override protected esTransPrescriptionHighAlertQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionHighAlertQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public TransPrescriptionHighAlertQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionHighAlertQuery();
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
        public bool Load(TransPrescriptionHighAlertQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPrescriptionHighAlertQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class TransPrescriptionHighAlertQuery : esTransPrescriptionHighAlertQuery
    {
        public TransPrescriptionHighAlertQuery()
        {

        }

        public TransPrescriptionHighAlertQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPrescriptionHighAlertQuery";
        }
    }

    [Serializable]
    public partial class TransPrescriptionHighAlertMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPrescriptionHighAlertMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPrescriptionHighAlertMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionHighAlertMetadata.PropertyNames.PrescriptionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionHighAlertMetadata.ColumnNames.SRPrescriptionHAlert, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionHighAlertMetadata.PropertyNames.SRPrescriptionHAlert;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionHighAlertMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionHighAlertMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionHighAlertMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionHighAlertMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public TransPrescriptionHighAlertMetadata Meta()
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
            public const string SRPrescriptionHAlert = "SRPrescriptionHAlert";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SRPrescriptionHAlert = "SRPrescriptionHAlert";
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
            lock (typeof(TransPrescriptionHighAlertMetadata))
            {
                if (TransPrescriptionHighAlertMetadata.mapDelegates == null)
                {
                    TransPrescriptionHighAlertMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPrescriptionHighAlertMetadata.meta == null)
                {
                    TransPrescriptionHighAlertMetadata.meta = new TransPrescriptionHighAlertMetadata();
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
                meta.AddTypeMap("SRPrescriptionHAlert", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "TransPrescriptionHighAlert";
                meta.Destination = "TransPrescriptionHighAlert";
                meta.spInsert = "proc_TransPrescriptionHighAlertInsert";
                meta.spUpdate = "proc_TransPrescriptionHighAlertUpdate";
                meta.spDelete = "proc_TransPrescriptionHighAlertDelete";
                meta.spLoadAll = "proc_TransPrescriptionHighAlertLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPrescriptionHighAlertLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPrescriptionHighAlertMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
