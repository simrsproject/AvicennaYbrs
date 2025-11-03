/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/11/2019 1:28:51 PM
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
    abstract public class esDocumentChecklistDefinitionCollection : esEntityCollectionWAuditLog
    {
        public esDocumentChecklistDefinitionCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "DocumentChecklistDefinitionCollection";
        }

        #region Query Logic
        protected void InitQuery(esDocumentChecklistDefinitionQuery query)
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
            this.InitQuery(query as esDocumentChecklistDefinitionQuery);
        }
        #endregion

        virtual public DocumentChecklistDefinition DetachEntity(DocumentChecklistDefinition entity)
        {
            return base.DetachEntity(entity) as DocumentChecklistDefinition;
        }

        virtual public DocumentChecklistDefinition AttachEntity(DocumentChecklistDefinition entity)
        {
            return base.AttachEntity(entity) as DocumentChecklistDefinition;
        }

        virtual public void Combine(DocumentChecklistDefinitionCollection collection)
        {
            base.Combine(collection);
        }

        new public DocumentChecklistDefinition this[int index]
        {
            get
            {
                return base[index] as DocumentChecklistDefinition;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(DocumentChecklistDefinition);
        }
    }

    [Serializable]
    abstract public class esDocumentChecklistDefinition : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDocumentChecklistDefinitionQuery GetDynamicQuery()
        {
            return null;
        }

        public esDocumentChecklistDefinition()
        {
        }

        public esDocumentChecklistDefinition(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String sRDocumentChecklist, Int32 documentFilesID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRDocumentChecklist, documentFilesID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRDocumentChecklist, documentFilesID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRDocumentChecklist, Int32 documentFilesID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(sRDocumentChecklist, documentFilesID);
            else
                return LoadByPrimaryKeyStoredProcedure(sRDocumentChecklist, documentFilesID);
        }

        private bool LoadByPrimaryKeyDynamic(String sRDocumentChecklist, Int32 documentFilesID)
        {
            esDocumentChecklistDefinitionQuery query = this.GetDynamicQuery();
            query.Where(query.SRDocumentChecklist == sRDocumentChecklist, query.DocumentFilesID == documentFilesID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String sRDocumentChecklist, Int32 documentFilesID)
        {
            esParameters parms = new esParameters();
            parms.Add("SRDocumentChecklist", sRDocumentChecklist);
            parms.Add("DocumentFilesID", documentFilesID);
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
                        case "SRDocumentChecklist": this.str.SRDocumentChecklist = (string)value; break;
                        case "DocumentFilesID": this.str.DocumentFilesID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DocumentFilesID":

                            if (value == null || value is System.Int32)
                                this.DocumentFilesID = (System.Int32?)value;
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
        /// Maps to DocumentChecklistDefinition.SRDocumentChecklist
        /// </summary>
        virtual public System.String SRDocumentChecklist
        {
            get
            {
                return base.GetSystemString(DocumentChecklistDefinitionMetadata.ColumnNames.SRDocumentChecklist);
            }

            set
            {
                base.SetSystemString(DocumentChecklistDefinitionMetadata.ColumnNames.SRDocumentChecklist, value);
            }
        }
        /// <summary>
        /// Maps to DocumentChecklistDefinition.DocumentFilesID
        /// </summary>
        virtual public System.Int32? DocumentFilesID
        {
            get
            {
                return base.GetSystemInt32(DocumentChecklistDefinitionMetadata.ColumnNames.DocumentFilesID);
            }

            set
            {
                base.SetSystemInt32(DocumentChecklistDefinitionMetadata.ColumnNames.DocumentFilesID, value);
            }
        }
        /// <summary>
        /// Maps to DocumentChecklistDefinition.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DocumentChecklistDefinitionMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DocumentChecklistDefinitionMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to DocumentChecklistDefinition.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DocumentChecklistDefinitionMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DocumentChecklistDefinitionMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esDocumentChecklistDefinition entity)
            {
                this.entity = entity;
            }
            public System.String SRDocumentChecklist
            {
                get
                {
                    System.String data = entity.SRDocumentChecklist;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDocumentChecklist = null;
                    else entity.SRDocumentChecklist = Convert.ToString(value);
                }
            }
            public System.String DocumentFilesID
            {
                get
                {
                    System.Int32? data = entity.DocumentFilesID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DocumentFilesID = null;
                    else entity.DocumentFilesID = Convert.ToInt32(value);
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
            private esDocumentChecklistDefinition entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDocumentChecklistDefinitionQuery query)
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
                throw new Exception("esDocumentChecklistDefinition can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class DocumentChecklistDefinition : esDocumentChecklistDefinition
    {
    }

    [Serializable]
    abstract public class esDocumentChecklistDefinitionQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return DocumentChecklistDefinitionMetadata.Meta();
            }
        }

        public esQueryItem SRDocumentChecklist
        {
            get
            {
                return new esQueryItem(this, DocumentChecklistDefinitionMetadata.ColumnNames.SRDocumentChecklist, esSystemType.String);
            }
        }

        public esQueryItem DocumentFilesID
        {
            get
            {
                return new esQueryItem(this, DocumentChecklistDefinitionMetadata.ColumnNames.DocumentFilesID, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DocumentChecklistDefinitionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DocumentChecklistDefinitionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DocumentChecklistDefinitionCollection")]
    public partial class DocumentChecklistDefinitionCollection : esDocumentChecklistDefinitionCollection, IEnumerable<DocumentChecklistDefinition>
    {
        public DocumentChecklistDefinitionCollection()
        {

        }

        public static implicit operator List<DocumentChecklistDefinition>(DocumentChecklistDefinitionCollection coll)
        {
            List<DocumentChecklistDefinition> list = new List<DocumentChecklistDefinition>();

            foreach (DocumentChecklistDefinition emp in coll)
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
                return DocumentChecklistDefinitionMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DocumentChecklistDefinitionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new DocumentChecklistDefinition(row);
        }

        override protected esEntity CreateEntity()
        {
            return new DocumentChecklistDefinition();
        }

        #endregion

        [BrowsableAttribute(false)]
        public DocumentChecklistDefinitionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DocumentChecklistDefinitionQuery();
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
        public bool Load(DocumentChecklistDefinitionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public DocumentChecklistDefinition AddNew()
        {
            DocumentChecklistDefinition entity = base.AddNewEntity() as DocumentChecklistDefinition;

            return entity;
        }
        public DocumentChecklistDefinition FindByPrimaryKey(String sRDocumentChecklist, Int32 documentFilesID)
        {
            return base.FindByPrimaryKey(sRDocumentChecklist, documentFilesID) as DocumentChecklistDefinition;
        }

        #region IEnumerable< DocumentChecklistDefinition> Members

        IEnumerator<DocumentChecklistDefinition> IEnumerable<DocumentChecklistDefinition>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as DocumentChecklistDefinition;
            }
        }

        #endregion

        private DocumentChecklistDefinitionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'DocumentChecklistDefinition' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DocumentChecklistDefinition ({SRDocumentChecklist, DocumentFilesID})")]
    [Serializable]
    public partial class DocumentChecklistDefinition : esDocumentChecklistDefinition
    {
        public DocumentChecklistDefinition()
        {
        }

        public DocumentChecklistDefinition(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DocumentChecklistDefinitionMetadata.Meta();
            }
        }

        override protected esDocumentChecklistDefinitionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DocumentChecklistDefinitionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public DocumentChecklistDefinitionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DocumentChecklistDefinitionQuery();
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
        public bool Load(DocumentChecklistDefinitionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DocumentChecklistDefinitionQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class DocumentChecklistDefinitionQuery : esDocumentChecklistDefinitionQuery
    {
        public DocumentChecklistDefinitionQuery()
        {

        }

        public DocumentChecklistDefinitionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DocumentChecklistDefinitionQuery";
        }
    }

    [Serializable]
    public partial class DocumentChecklistDefinitionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DocumentChecklistDefinitionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DocumentChecklistDefinitionMetadata.ColumnNames.SRDocumentChecklist, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DocumentChecklistDefinitionMetadata.PropertyNames.SRDocumentChecklist;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DocumentChecklistDefinitionMetadata.ColumnNames.DocumentFilesID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = DocumentChecklistDefinitionMetadata.PropertyNames.DocumentFilesID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(DocumentChecklistDefinitionMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DocumentChecklistDefinitionMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DocumentChecklistDefinitionMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = DocumentChecklistDefinitionMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public DocumentChecklistDefinitionMetadata Meta()
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
            public const string SRDocumentChecklist = "SRDocumentChecklist";
            public const string DocumentFilesID = "DocumentFilesID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string SRDocumentChecklist = "SRDocumentChecklist";
            public const string DocumentFilesID = "DocumentFilesID";
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
            lock (typeof(DocumentChecklistDefinitionMetadata))
            {
                if (DocumentChecklistDefinitionMetadata.mapDelegates == null)
                {
                    DocumentChecklistDefinitionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DocumentChecklistDefinitionMetadata.meta == null)
                {
                    DocumentChecklistDefinitionMetadata.meta = new DocumentChecklistDefinitionMetadata();
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

                meta.AddTypeMap("SRDocumentChecklist", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DocumentFilesID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "DocumentChecklistDefinition";
                meta.Destination = "DocumentChecklistDefinition";
                meta.spInsert = "proc_DocumentChecklistDefinitionInsert";
                meta.spUpdate = "proc_DocumentChecklistDefinitionUpdate";
                meta.spDelete = "proc_DocumentChecklistDefinitionDelete";
                meta.spLoadAll = "proc_DocumentChecklistDefinitionLoadAll";
                meta.spLoadByPrimaryKey = "proc_DocumentChecklistDefinitionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DocumentChecklistDefinitionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
