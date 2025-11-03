/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/24/2016 9:52:44 AM
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
    abstract public class esCssdMachineItemCollection : esEntityCollectionWAuditLog
    {
        public esCssdMachineItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "CssdMachineItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esCssdMachineItemQuery query)
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
            this.InitQuery(query as esCssdMachineItemQuery);
        }
        #endregion

        virtual public CssdMachineItem DetachEntity(CssdMachineItem entity)
        {
            return base.DetachEntity(entity) as CssdMachineItem;
        }

        virtual public CssdMachineItem AttachEntity(CssdMachineItem entity)
        {
            return base.AttachEntity(entity) as CssdMachineItem;
        }

        virtual public void Combine(CssdMachineItemCollection collection)
        {
            base.Combine(collection);
        }

        new public CssdMachineItem this[int index]
        {
            get
            {
                return base[index] as CssdMachineItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(CssdMachineItem);
        }
    }

    [Serializable]
    abstract public class esCssdMachineItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esCssdMachineItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esCssdMachineItem()
        {
        }

        public esCssdMachineItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String machineID, String sRCssdProcessType)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(machineID, sRCssdProcessType);
            else
                return LoadByPrimaryKeyStoredProcedure(machineID, sRCssdProcessType);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String machineID, String sRCssdProcessType)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(machineID, sRCssdProcessType);
            else
                return LoadByPrimaryKeyStoredProcedure(machineID, sRCssdProcessType);
        }

        private bool LoadByPrimaryKeyDynamic(String machineID, String sRCssdProcessType)
        {
            esCssdMachineItemQuery query = this.GetDynamicQuery();
            query.Where(query.MachineID == machineID, query.SRCssdProcessType == sRCssdProcessType);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String machineID, String sRCssdProcessType)
        {
            esParameters parms = new esParameters();
            parms.Add("MachineID", machineID);
            parms.Add("SRCssdProcessType", sRCssdProcessType);
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
                        case "MachineID": this.str.MachineID = (string)value; break;
                        case "SRCssdProcessType": this.str.SRCssdProcessType = (string)value; break;
                        case "Duration": this.str.Duration = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Duration":

                            if (value == null || value is System.Int16)
                                this.Duration = (System.Int16?)value;
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
        /// Maps to CssdMachineItem.MachineID
        /// </summary>
        virtual public System.String MachineID
        {
            get
            {
                return base.GetSystemString(CssdMachineItemMetadata.ColumnNames.MachineID);
            }

            set
            {
                base.SetSystemString(CssdMachineItemMetadata.ColumnNames.MachineID, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachineItem.SRCssdProcessType
        /// </summary>
        virtual public System.String SRCssdProcessType
        {
            get
            {
                return base.GetSystemString(CssdMachineItemMetadata.ColumnNames.SRCssdProcessType);
            }

            set
            {
                base.SetSystemString(CssdMachineItemMetadata.ColumnNames.SRCssdProcessType, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachineItem.Duration
        /// </summary>
        virtual public System.Int16? Duration
        {
            get
            {
                return base.GetSystemInt16(CssdMachineItemMetadata.ColumnNames.Duration);
            }

            set
            {
                base.SetSystemInt16(CssdMachineItemMetadata.ColumnNames.Duration, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachineItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(CssdMachineItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(CssdMachineItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to CssdMachineItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(CssdMachineItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(CssdMachineItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esCssdMachineItem entity)
            {
                this.entity = entity;
            }
            public System.String MachineID
            {
                get
                {
                    System.String data = entity.MachineID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MachineID = null;
                    else entity.MachineID = Convert.ToString(value);
                }
            }
            public System.String SRCssdProcessType
            {
                get
                {
                    System.String data = entity.SRCssdProcessType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRCssdProcessType = null;
                    else entity.SRCssdProcessType = Convert.ToString(value);
                }
            }
            public System.String Duration
            {
                get
                {
                    System.Int16? data = entity.Duration;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Duration = null;
                    else entity.Duration = Convert.ToInt16(value);
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
            private esCssdMachineItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esCssdMachineItemQuery query)
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
                throw new Exception("esCssdMachineItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class CssdMachineItem : esCssdMachineItem
    {
    }

    [Serializable]
    abstract public class esCssdMachineItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return CssdMachineItemMetadata.Meta();
            }
        }

        public esQueryItem MachineID
        {
            get
            {
                return new esQueryItem(this, CssdMachineItemMetadata.ColumnNames.MachineID, esSystemType.String);
            }
        }

        public esQueryItem SRCssdProcessType
        {
            get
            {
                return new esQueryItem(this, CssdMachineItemMetadata.ColumnNames.SRCssdProcessType, esSystemType.String);
            }
        }

        public esQueryItem Duration
        {
            get
            {
                return new esQueryItem(this, CssdMachineItemMetadata.ColumnNames.Duration, esSystemType.Int16);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, CssdMachineItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, CssdMachineItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("CssdMachineItemCollection")]
    public partial class CssdMachineItemCollection : esCssdMachineItemCollection, IEnumerable<CssdMachineItem>
    {
        public CssdMachineItemCollection()
        {

        }

        public static implicit operator List<CssdMachineItem>(CssdMachineItemCollection coll)
        {
            List<CssdMachineItem> list = new List<CssdMachineItem>();

            foreach (CssdMachineItem emp in coll)
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
                return CssdMachineItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CssdMachineItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new CssdMachineItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new CssdMachineItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public CssdMachineItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CssdMachineItemQuery();
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
        public bool Load(CssdMachineItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public CssdMachineItem AddNew()
        {
            CssdMachineItem entity = base.AddNewEntity() as CssdMachineItem;

            return entity;
        }
        public CssdMachineItem FindByPrimaryKey(String machineID, String sRCssdProcessType)
        {
            return base.FindByPrimaryKey(machineID, sRCssdProcessType) as CssdMachineItem;
        }

        #region IEnumerable< CssdMachineItem> Members

        IEnumerator<CssdMachineItem> IEnumerable<CssdMachineItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as CssdMachineItem;
            }
        }

        #endregion

        private CssdMachineItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'CssdMachineItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CssdMachineItem ({MachineID, SRCssdProcessType})")]
    [Serializable]
    public partial class CssdMachineItem : esCssdMachineItem
    {
        public CssdMachineItem()
        {
        }

        public CssdMachineItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return CssdMachineItemMetadata.Meta();
            }
        }

        override protected esCssdMachineItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new CssdMachineItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public CssdMachineItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new CssdMachineItemQuery();
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
        public bool Load(CssdMachineItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private CssdMachineItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class CssdMachineItemQuery : esCssdMachineItemQuery
    {
        public CssdMachineItemQuery()
        {

        }

        public CssdMachineItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "CssdMachineItemQuery";
        }
    }

    [Serializable]
    public partial class CssdMachineItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected CssdMachineItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(CssdMachineItemMetadata.ColumnNames.MachineID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdMachineItemMetadata.PropertyNames.MachineID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineItemMetadata.ColumnNames.SRCssdProcessType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdMachineItemMetadata.PropertyNames.SRCssdProcessType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineItemMetadata.ColumnNames.Duration, 2, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = CssdMachineItemMetadata.PropertyNames.Duration;
            c.NumericPrecision = 5;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = CssdMachineItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(CssdMachineItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = CssdMachineItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public CssdMachineItemMetadata Meta()
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
            public const string MachineID = "MachineID";
            public const string SRCssdProcessType = "SRCssdProcessType";
            public const string Duration = "Duration";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MachineID = "MachineID";
            public const string SRCssdProcessType = "SRCssdProcessType";
            public const string Duration = "Duration";
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
            lock (typeof(CssdMachineItemMetadata))
            {
                if (CssdMachineItemMetadata.mapDelegates == null)
                {
                    CssdMachineItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (CssdMachineItemMetadata.meta == null)
                {
                    CssdMachineItemMetadata.meta = new CssdMachineItemMetadata();
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

                meta.AddTypeMap("MachineID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRCssdProcessType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Duration", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "CssdMachineItem";
                meta.Destination = "CssdMachineItem";
                meta.spInsert = "proc_CssdMachineItemInsert";
                meta.spUpdate = "proc_CssdMachineItemUpdate";
                meta.spDelete = "proc_CssdMachineItemDelete";
                meta.spLoadAll = "proc_CssdMachineItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_CssdMachineItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private CssdMachineItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
