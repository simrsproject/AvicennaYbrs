/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/29/18 11:23:32 AM
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
    abstract public class esAssessmentTypeBodyDiagramCollection : esEntityCollectionWAuditLog
    {
        public esAssessmentTypeBodyDiagramCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AssessmentTypeBodyDiagramCollection";
        }

        #region Query Logic
        protected void InitQuery(esAssessmentTypeBodyDiagramQuery query)
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
            this.InitQuery(query as esAssessmentTypeBodyDiagramQuery);
        }
        #endregion

        virtual public AssessmentTypeBodyDiagram DetachEntity(AssessmentTypeBodyDiagram entity)
        {
            return base.DetachEntity(entity) as AssessmentTypeBodyDiagram;
        }

        virtual public AssessmentTypeBodyDiagram AttachEntity(AssessmentTypeBodyDiagram entity)
        {
            return base.AttachEntity(entity) as AssessmentTypeBodyDiagram;
        }

        virtual public void Combine(AssessmentTypeBodyDiagramCollection collection)
        {
            base.Combine(collection);
        }

        new public AssessmentTypeBodyDiagram this[int index]
        {
            get
            {
                return base[index] as AssessmentTypeBodyDiagram;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AssessmentTypeBodyDiagram);
        }
    }

    [Serializable]
    abstract public class esAssessmentTypeBodyDiagram : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAssessmentTypeBodyDiagramQuery GetDynamicQuery()
        {
            return null;
        }

        public esAssessmentTypeBodyDiagram()
        {
        }

        public esAssessmentTypeBodyDiagram(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRAssessmentType, String bodyID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRAssessmentType, bodyID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRAssessmentType, bodyID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRAssessmentType, String bodyID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRAssessmentType, bodyID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRAssessmentType, bodyID);
        }

        private bool LoadByPrimaryKeyDynamic(String sRAssessmentType, String bodyID)
        {
            esAssessmentTypeBodyDiagramQuery query = this.GetDynamicQuery();
            query.Where(query.SRAssessmentType == sRAssessmentType, query.BodyID == bodyID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRAssessmentType, String bodyID)
        {
            esParameters parms = new esParameters();
            parms.Add("SRAssessmentType", sRAssessmentType);
            parms.Add("BodyID", bodyID);
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
                        case "SRAssessmentType": this.str.SRAssessmentType = (string)value; break;
                        case "BodyID": this.str.BodyID = (string)value; break;
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
        /// Maps to AssessmentTypeBodyDiagram.SRAssessmentType
        /// </summary>
        virtual public System.String SRAssessmentType
        {
            get
            {
                return base.GetSystemString(AssessmentTypeBodyDiagramMetadata.ColumnNames.SRAssessmentType);
            }

            set
            {
                base.SetSystemString(AssessmentTypeBodyDiagramMetadata.ColumnNames.SRAssessmentType, value);
            }
        }
        /// <summary>
        /// Maps to AssessmentTypeBodyDiagram.BodyID
        /// </summary>
        virtual public System.String BodyID
        {
            get
            {
                return base.GetSystemString(AssessmentTypeBodyDiagramMetadata.ColumnNames.BodyID);
            }

            set
            {
                base.SetSystemString(AssessmentTypeBodyDiagramMetadata.ColumnNames.BodyID, value);
            }
        }
        /// <summary>
        /// Maps to AssessmentTypeBodyDiagram.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AssessmentTypeBodyDiagramMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AssessmentTypeBodyDiagramMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to AssessmentTypeBodyDiagram.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AssessmentTypeBodyDiagramMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AssessmentTypeBodyDiagramMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAssessmentTypeBodyDiagram entity)
            {
                this.entity = entity;
            }
            public System.String SRAssessmentType
            {
                get
                {
                    System.String data = entity.SRAssessmentType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRAssessmentType = null;
                    else entity.SRAssessmentType = Convert.ToString(value);
                }
            }
            public System.String BodyID
            {
                get
                {
                    System.String data = entity.BodyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BodyID = null;
                    else entity.BodyID = Convert.ToString(value);
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
            private esAssessmentTypeBodyDiagram entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAssessmentTypeBodyDiagramQuery query)
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
                throw new Exception("esAssessmentTypeBodyDiagram can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AssessmentTypeBodyDiagram : esAssessmentTypeBodyDiagram
    {
    }

    [Serializable]
    abstract public class esAssessmentTypeBodyDiagramQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AssessmentTypeBodyDiagramMetadata.Meta();
            }
        }

        public esQueryItem SRAssessmentType
        {
            get
            {
                return new esQueryItem(this, AssessmentTypeBodyDiagramMetadata.ColumnNames.SRAssessmentType, esSystemType.String);
            }
        }

        public esQueryItem BodyID
        {
            get
            {
                return new esQueryItem(this, AssessmentTypeBodyDiagramMetadata.ColumnNames.BodyID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AssessmentTypeBodyDiagramMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AssessmentTypeBodyDiagramMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AssessmentTypeBodyDiagramCollection")]
    public partial class AssessmentTypeBodyDiagramCollection : esAssessmentTypeBodyDiagramCollection, IEnumerable<AssessmentTypeBodyDiagram>
    {
        public AssessmentTypeBodyDiagramCollection()
        {

        }

        public static implicit operator List<AssessmentTypeBodyDiagram>(AssessmentTypeBodyDiagramCollection coll)
        {
            List<AssessmentTypeBodyDiagram> list = new List<AssessmentTypeBodyDiagram>();

            foreach (AssessmentTypeBodyDiagram emp in coll)
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
                return AssessmentTypeBodyDiagramMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AssessmentTypeBodyDiagramQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AssessmentTypeBodyDiagram(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AssessmentTypeBodyDiagram();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AssessmentTypeBodyDiagramQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AssessmentTypeBodyDiagramQuery();
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
        public bool Load(AssessmentTypeBodyDiagramQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AssessmentTypeBodyDiagram AddNew()
        {
            AssessmentTypeBodyDiagram entity = base.AddNewEntity() as AssessmentTypeBodyDiagram;

            return entity;
        }
        public AssessmentTypeBodyDiagram FindByPrimaryKey(String sRAssessmentType, String bodyID)
        {
            return base.FindByPrimaryKey(sRAssessmentType, bodyID) as AssessmentTypeBodyDiagram;
        }

        #region IEnumerable< AssessmentTypeBodyDiagram> Members

        IEnumerator<AssessmentTypeBodyDiagram> IEnumerable<AssessmentTypeBodyDiagram>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AssessmentTypeBodyDiagram;
            }
        }

        #endregion

        private AssessmentTypeBodyDiagramQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AssessmentTypeBodyDiagram' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AssessmentTypeBodyDiagram ({SRAssessmentType, BodyID})")]
    [Serializable]
    public partial class AssessmentTypeBodyDiagram : esAssessmentTypeBodyDiagram
    {
        public AssessmentTypeBodyDiagram()
        {
        }

        public AssessmentTypeBodyDiagram(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AssessmentTypeBodyDiagramMetadata.Meta();
            }
        }

        override protected esAssessmentTypeBodyDiagramQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AssessmentTypeBodyDiagramQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AssessmentTypeBodyDiagramQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AssessmentTypeBodyDiagramQuery();
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
        public bool Load(AssessmentTypeBodyDiagramQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AssessmentTypeBodyDiagramQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AssessmentTypeBodyDiagramQuery : esAssessmentTypeBodyDiagramQuery
    {
        public AssessmentTypeBodyDiagramQuery()
        {

        }

        public AssessmentTypeBodyDiagramQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AssessmentTypeBodyDiagramQuery";
        }
    }

    [Serializable]
    public partial class AssessmentTypeBodyDiagramMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AssessmentTypeBodyDiagramMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AssessmentTypeBodyDiagramMetadata.ColumnNames.SRAssessmentType, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AssessmentTypeBodyDiagramMetadata.PropertyNames.SRAssessmentType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AssessmentTypeBodyDiagramMetadata.ColumnNames.BodyID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AssessmentTypeBodyDiagramMetadata.PropertyNames.BodyID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AssessmentTypeBodyDiagramMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AssessmentTypeBodyDiagramMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AssessmentTypeBodyDiagramMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AssessmentTypeBodyDiagramMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AssessmentTypeBodyDiagramMetadata Meta()
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
            public const string SRAssessmentType = "SRAssessmentType";
            public const string BodyID = "BodyID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRAssessmentType = "SRAssessmentType";
            public const string BodyID = "BodyID";
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
            lock (typeof(AssessmentTypeBodyDiagramMetadata))
            {
                if (AssessmentTypeBodyDiagramMetadata.mapDelegates == null)
                {
                    AssessmentTypeBodyDiagramMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AssessmentTypeBodyDiagramMetadata.meta == null)
                {
                    AssessmentTypeBodyDiagramMetadata.meta = new AssessmentTypeBodyDiagramMetadata();
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

                meta.AddTypeMap("SRAssessmentType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BodyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "AssessmentTypeBodyDiagram";
                meta.Destination = "AssessmentTypeBodyDiagram";
                meta.spInsert = "proc_AssessmentTypeBodyDiagramInsert";
                meta.spUpdate = "proc_AssessmentTypeBodyDiagramUpdate";
                meta.spDelete = "proc_AssessmentTypeBodyDiagramDelete";
                meta.spLoadAll = "proc_AssessmentTypeBodyDiagramLoadAll";
                meta.spLoadByPrimaryKey = "proc_AssessmentTypeBodyDiagramLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AssessmentTypeBodyDiagramMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
