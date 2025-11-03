/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/6/2016 1:08:57 PM
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
    abstract public class esPathologyAnatomyTissueCollection : esEntityCollectionWAuditLog
    {
        public esPathologyAnatomyTissueCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathologyAnatomyTissueCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathologyAnatomyTissueQuery query)
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
            this.InitQuery(query as esPathologyAnatomyTissueQuery);
        }
        #endregion

        virtual public PathologyAnatomyTissue DetachEntity(PathologyAnatomyTissue entity)
        {
            return base.DetachEntity(entity) as PathologyAnatomyTissue;
        }

        virtual public PathologyAnatomyTissue AttachEntity(PathologyAnatomyTissue entity)
        {
            return base.AttachEntity(entity) as PathologyAnatomyTissue;
        }

        virtual public void Combine(PathologyAnatomyTissueCollection collection)
        {
            base.Combine(collection);
        }

        new public PathologyAnatomyTissue this[int index]
        {
            get
            {
                return base[index] as PathologyAnatomyTissue;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PathologyAnatomyTissue);
        }
    }

    [Serializable]
    abstract public class esPathologyAnatomyTissue : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathologyAnatomyTissueQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathologyAnatomyTissue()
        {
        }

        public esPathologyAnatomyTissue(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String tissueID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tissueID);
            else
                return LoadByPrimaryKeyStoredProcedure(tissueID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String tissueID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(tissueID);
            else
                return LoadByPrimaryKeyStoredProcedure(tissueID);
        }

        private bool LoadByPrimaryKeyDynamic(String tissueID)
        {
            esPathologyAnatomyTissueQuery query = this.GetDynamicQuery();
            query.Where(query.TissueID == tissueID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String tissueID)
        {
            esParameters parms = new esParameters();
            parms.Add("TissueID", tissueID);
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
                        case "TissueID": this.str.TissueID = (string)value; break;
                        case "TissueName": this.str.TissueName = (string)value; break;
                        case "SourceOfTissueID": this.str.SourceOfTissueID = (string)value; break;
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
        /// Maps to PathologyAnatomyTissue.TissueID
        /// </summary>
        virtual public System.String TissueID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyTissueMetadata.ColumnNames.TissueID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyTissueMetadata.ColumnNames.TissueID, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyTissue.TissueName
        /// </summary>
        virtual public System.String TissueName
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyTissueMetadata.ColumnNames.TissueName);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyTissueMetadata.ColumnNames.TissueName, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyTissue.SourceOfTissueID
        /// </summary>
        virtual public System.String SourceOfTissueID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyTissueMetadata.ColumnNames.SourceOfTissueID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyTissueMetadata.ColumnNames.SourceOfTissueID, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyTissue.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathologyAnatomyTissueMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathologyAnatomyTissueMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PathologyAnatomyTissue.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathologyAnatomyTissueMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathologyAnatomyTissueMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPathologyAnatomyTissue entity)
            {
                this.entity = entity;
            }
            public System.String TissueID
            {
                get
                {
                    System.String data = entity.TissueID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TissueID = null;
                    else entity.TissueID = Convert.ToString(value);
                }
            }
            public System.String TissueName
            {
                get
                {
                    System.String data = entity.TissueName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TissueName = null;
                    else entity.TissueName = Convert.ToString(value);
                }
            }
            public System.String SourceOfTissueID
            {
                get
                {
                    System.String data = entity.SourceOfTissueID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SourceOfTissueID = null;
                    else entity.SourceOfTissueID = Convert.ToString(value);
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
            private esPathologyAnatomyTissue entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathologyAnatomyTissueQuery query)
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
                throw new Exception("esPathologyAnatomyTissue can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PathologyAnatomyTissue : esPathologyAnatomyTissue
    {
    }

    [Serializable]
    abstract public class esPathologyAnatomyTissueQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomyTissueMetadata.Meta();
            }
        }

        public esQueryItem TissueID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyTissueMetadata.ColumnNames.TissueID, esSystemType.String);
            }
        }

        public esQueryItem TissueName
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyTissueMetadata.ColumnNames.TissueName, esSystemType.String);
            }
        }

        public esQueryItem SourceOfTissueID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyTissueMetadata.ColumnNames.SourceOfTissueID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyTissueMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathologyAnatomyTissueMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathologyAnatomyTissueCollection")]
    public partial class PathologyAnatomyTissueCollection : esPathologyAnatomyTissueCollection, IEnumerable<PathologyAnatomyTissue>
    {
        public PathologyAnatomyTissueCollection()
        {

        }

        public static implicit operator List<PathologyAnatomyTissue>(PathologyAnatomyTissueCollection coll)
        {
            List<PathologyAnatomyTissue> list = new List<PathologyAnatomyTissue>();

            foreach (PathologyAnatomyTissue emp in coll)
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
                return PathologyAnatomyTissueMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomyTissueQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PathologyAnatomyTissue(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PathologyAnatomyTissue();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomyTissueQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomyTissueQuery();
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
        public bool Load(PathologyAnatomyTissueQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PathologyAnatomyTissue AddNew()
        {
            PathologyAnatomyTissue entity = base.AddNewEntity() as PathologyAnatomyTissue;

            return entity;
        }
        public PathologyAnatomyTissue FindByPrimaryKey(String tissueID)
        {
            return base.FindByPrimaryKey(tissueID) as PathologyAnatomyTissue;
        }

        #region IEnumerable< PathologyAnatomyTissue> Members

        IEnumerator<PathologyAnatomyTissue> IEnumerable<PathologyAnatomyTissue>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PathologyAnatomyTissue;
            }
        }

        #endregion

        private PathologyAnatomyTissueQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PathologyAnatomyTissue' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PathologyAnatomyTissue ({TissueID})")]
    [Serializable]
    public partial class PathologyAnatomyTissue : esPathologyAnatomyTissue
    {
        public PathologyAnatomyTissue()
        {
        }

        public PathologyAnatomyTissue(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathologyAnatomyTissueMetadata.Meta();
            }
        }

        override protected esPathologyAnatomyTissueQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathologyAnatomyTissueQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathologyAnatomyTissueQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathologyAnatomyTissueQuery();
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
        public bool Load(PathologyAnatomyTissueQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathologyAnatomyTissueQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathologyAnatomyTissueQuery : esPathologyAnatomyTissueQuery
    {
        public PathologyAnatomyTissueQuery()
        {

        }

        public PathologyAnatomyTissueQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathologyAnatomyTissueQuery";
        }
    }

    [Serializable]
    public partial class PathologyAnatomyTissueMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathologyAnatomyTissueMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathologyAnatomyTissueMetadata.ColumnNames.TissueID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyTissueMetadata.PropertyNames.TissueID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyTissueMetadata.ColumnNames.TissueName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyTissueMetadata.PropertyNames.TissueName;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyTissueMetadata.ColumnNames.SourceOfTissueID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyTissueMetadata.PropertyNames.SourceOfTissueID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyTissueMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathologyAnatomyTissueMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathologyAnatomyTissueMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PathologyAnatomyTissueMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PathologyAnatomyTissueMetadata Meta()
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
            public const string TissueID = "TissueID";
            public const string TissueName = "TissueName";
            public const string SourceOfTissueID = "SourceOfTissueID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TissueID = "TissueID";
            public const string TissueName = "TissueName";
            public const string SourceOfTissueID = "SourceOfTissueID";
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
            lock (typeof(PathologyAnatomyTissueMetadata))
            {
                if (PathologyAnatomyTissueMetadata.mapDelegates == null)
                {
                    PathologyAnatomyTissueMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathologyAnatomyTissueMetadata.meta == null)
                {
                    PathologyAnatomyTissueMetadata.meta = new PathologyAnatomyTissueMetadata();
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

                meta.AddTypeMap("TissueID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TissueName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SourceOfTissueID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PathologyAnatomyTissue";
                meta.Destination = "PathologyAnatomyTissue";
                meta.spInsert = "proc_PathologyAnatomyTissueInsert";
                meta.spUpdate = "proc_PathologyAnatomyTissueUpdate";
                meta.spDelete = "proc_PathologyAnatomyTissueDelete";
                meta.spLoadAll = "proc_PathologyAnatomyTissueLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathologyAnatomyTissueLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathologyAnatomyTissueMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
