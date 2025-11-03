/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/21/2017 1:27:06 PM
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
    abstract public class esRiskFactorsCollection : esEntityCollectionWAuditLog
    {
        public esRiskFactorsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RiskFactorsCollection";
        }

        #region Query Logic
        protected void InitQuery(esRiskFactorsQuery query)
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
            this.InitQuery(query as esRiskFactorsQuery);
        }
        #endregion

        virtual public RiskFactors DetachEntity(RiskFactors entity)
        {
            return base.DetachEntity(entity) as RiskFactors;
        }

        virtual public RiskFactors AttachEntity(RiskFactors entity)
        {
            return base.AttachEntity(entity) as RiskFactors;
        }

        virtual public void Combine(RiskFactorsCollection collection)
        {
            base.Combine(collection);
        }

        new public RiskFactors this[int index]
        {
            get
            {
                return base[index] as RiskFactors;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RiskFactors);
        }
    }

    [Serializable]
    abstract public class esRiskFactors : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRiskFactorsQuery GetDynamicQuery()
        {
            return null;
        }

        public esRiskFactors()
        {
        }

        public esRiskFactors(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRRiskFactorsType, String riskFactorsID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRRiskFactorsType, riskFactorsID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRRiskFactorsType, riskFactorsID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRRiskFactorsType, String riskFactorsID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRRiskFactorsType, riskFactorsID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRRiskFactorsType, riskFactorsID);
        }

        private bool LoadByPrimaryKeyDynamic(String sRRiskFactorsType, String riskFactorsID)
        {
            esRiskFactorsQuery query = this.GetDynamicQuery();
            query.Where(query.SRRiskFactorsType == sRRiskFactorsType, query.RiskFactorsID == riskFactorsID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRRiskFactorsType, String riskFactorsID)
        {
            esParameters parms = new esParameters();
            parms.Add("SRRiskFactorsType", sRRiskFactorsType);
            parms.Add("RiskFactorsID", riskFactorsID);
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
                        case "SRRiskFactorsType": this.str.SRRiskFactorsType = (string)value; break;
                        case "RiskFactorsID": this.str.RiskFactorsID = (string)value; break;
                        case "RiskFactorsName": this.str.RiskFactorsName = (string)value; break;
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
        /// Maps to RiskFactors.SRRiskFactorsType
        /// </summary>
        virtual public System.String SRRiskFactorsType
        {
            get
            {
                return base.GetSystemString(RiskFactorsMetadata.ColumnNames.SRRiskFactorsType);
            }

            set
            {
                base.SetSystemString(RiskFactorsMetadata.ColumnNames.SRRiskFactorsType, value);
            }
        }
        /// <summary>
        /// Maps to RiskFactors.RiskFactorsID
        /// </summary>
        virtual public System.String RiskFactorsID
        {
            get
            {
                return base.GetSystemString(RiskFactorsMetadata.ColumnNames.RiskFactorsID);
            }

            set
            {
                base.SetSystemString(RiskFactorsMetadata.ColumnNames.RiskFactorsID, value);
            }
        }
        /// <summary>
        /// Maps to RiskFactors.RiskFactorsName
        /// </summary>
        virtual public System.String RiskFactorsName
        {
            get
            {
                return base.GetSystemString(RiskFactorsMetadata.ColumnNames.RiskFactorsName);
            }

            set
            {
                base.SetSystemString(RiskFactorsMetadata.ColumnNames.RiskFactorsName, value);
            }
        }
        /// <summary>
        /// Maps to RiskFactors.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RiskFactorsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RiskFactorsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RiskFactors.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RiskFactorsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RiskFactorsMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRiskFactors entity)
            {
                this.entity = entity;
            }
            public System.String SRRiskFactorsType
            {
                get
                {
                    System.String data = entity.SRRiskFactorsType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRRiskFactorsType = null;
                    else entity.SRRiskFactorsType = Convert.ToString(value);
                }
            }
            public System.String RiskFactorsID
            {
                get
                {
                    System.String data = entity.RiskFactorsID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RiskFactorsID = null;
                    else entity.RiskFactorsID = Convert.ToString(value);
                }
            }
            public System.String RiskFactorsName
            {
                get
                {
                    System.String data = entity.RiskFactorsName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RiskFactorsName = null;
                    else entity.RiskFactorsName = Convert.ToString(value);
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
            private esRiskFactors entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRiskFactorsQuery query)
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
                throw new Exception("esRiskFactors can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RiskFactors : esRiskFactors
    {
    }

    [Serializable]
    abstract public class esRiskFactorsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RiskFactorsMetadata.Meta();
            }
        }

        public esQueryItem SRRiskFactorsType
        {
            get
            {
                return new esQueryItem(this, RiskFactorsMetadata.ColumnNames.SRRiskFactorsType, esSystemType.String);
            }
        }

        public esQueryItem RiskFactorsID
        {
            get
            {
                return new esQueryItem(this, RiskFactorsMetadata.ColumnNames.RiskFactorsID, esSystemType.String);
            }
        }

        public esQueryItem RiskFactorsName
        {
            get
            {
                return new esQueryItem(this, RiskFactorsMetadata.ColumnNames.RiskFactorsName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RiskFactorsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RiskFactorsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RiskFactorsCollection")]
    public partial class RiskFactorsCollection : esRiskFactorsCollection, IEnumerable<RiskFactors>
    {
        public RiskFactorsCollection()
        {

        }

        public static implicit operator List<RiskFactors>(RiskFactorsCollection coll)
        {
            List<RiskFactors> list = new List<RiskFactors>();

            foreach (RiskFactors emp in coll)
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
                return RiskFactorsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RiskFactorsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RiskFactors(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RiskFactors();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RiskFactorsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RiskFactorsQuery();
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
        public bool Load(RiskFactorsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RiskFactors AddNew()
        {
            RiskFactors entity = base.AddNewEntity() as RiskFactors;

            return entity;
        }
        public RiskFactors FindByPrimaryKey(String sRRiskFactorsType, String riskFactorsID)
        {
            return base.FindByPrimaryKey(sRRiskFactorsType, riskFactorsID) as RiskFactors;
        }

        #region IEnumerable< RiskFactors> Members

        IEnumerator<RiskFactors> IEnumerable<RiskFactors>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RiskFactors;
            }
        }

        #endregion

        private RiskFactorsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RiskFactors' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RiskFactors ({SRRiskFactorsType, RiskFactorsID})")]
    [Serializable]
    public partial class RiskFactors : esRiskFactors
    {
        public RiskFactors()
        {
        }

        public RiskFactors(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RiskFactorsMetadata.Meta();
            }
        }

        override protected esRiskFactorsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RiskFactorsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RiskFactorsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RiskFactorsQuery();
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
        public bool Load(RiskFactorsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RiskFactorsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RiskFactorsQuery : esRiskFactorsQuery
    {
        public RiskFactorsQuery()
        {

        }

        public RiskFactorsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RiskFactorsQuery";
        }
    }

    [Serializable]
    public partial class RiskFactorsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RiskFactorsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RiskFactorsMetadata.ColumnNames.SRRiskFactorsType, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RiskFactorsMetadata.PropertyNames.SRRiskFactorsType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RiskFactorsMetadata.ColumnNames.RiskFactorsID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RiskFactorsMetadata.PropertyNames.RiskFactorsID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RiskFactorsMetadata.ColumnNames.RiskFactorsName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RiskFactorsMetadata.PropertyNames.RiskFactorsName;
            c.CharacterMaxLength = 150;
            _columns.Add(c);

            c = new esColumnMetadata(RiskFactorsMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RiskFactorsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RiskFactorsMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RiskFactorsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public RiskFactorsMetadata Meta()
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
            public const string SRRiskFactorsType = "SRRiskFactorsType";
            public const string RiskFactorsID = "RiskFactorsID";
            public const string RiskFactorsName = "RiskFactorsName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRRiskFactorsType = "SRRiskFactorsType";
            public const string RiskFactorsID = "RiskFactorsID";
            public const string RiskFactorsName = "RiskFactorsName";
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
            lock (typeof(RiskFactorsMetadata))
            {
                if (RiskFactorsMetadata.mapDelegates == null)
                {
                    RiskFactorsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RiskFactorsMetadata.meta == null)
                {
                    RiskFactorsMetadata.meta = new RiskFactorsMetadata();
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

                meta.AddTypeMap("SRRiskFactorsType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RiskFactorsID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RiskFactorsName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "RiskFactors";
                meta.Destination = "RiskFactors";
                meta.spInsert = "proc_RiskFactorsInsert";
                meta.spUpdate = "proc_RiskFactorsUpdate";
                meta.spDelete = "proc_RiskFactorsDelete";
                meta.spLoadAll = "proc_RiskFactorsLoadAll";
                meta.spLoadByPrimaryKey = "proc_RiskFactorsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RiskFactorsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
