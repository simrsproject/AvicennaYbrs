/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/29/2016 4:44:16 AM
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
    abstract public class esServiceUnitVitalSignCollection : esEntityCollectionWAuditLog
    {
        public esServiceUnitVitalSignCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ServiceUnitVitalSignCollection";
        }

        #region Query Logic
        protected void InitQuery(esServiceUnitVitalSignQuery query)
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
            this.InitQuery(query as esServiceUnitVitalSignQuery);
        }
        #endregion

        virtual public ServiceUnitVitalSign DetachEntity(ServiceUnitVitalSign entity)
        {
            return base.DetachEntity(entity) as ServiceUnitVitalSign;
        }

        virtual public ServiceUnitVitalSign AttachEntity(ServiceUnitVitalSign entity)
        {
            return base.AttachEntity(entity) as ServiceUnitVitalSign;
        }

        virtual public void Combine(ServiceUnitVitalSignCollection collection)
        {
            base.Combine(collection);
        }

        new public ServiceUnitVitalSign this[int index]
        {
            get
            {
                return base[index] as ServiceUnitVitalSign;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ServiceUnitVitalSign);
        }
    }

    [Serializable]
    abstract public class esServiceUnitVitalSign : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esServiceUnitVitalSignQuery GetDynamicQuery()
        {
            return null;
        }

        public esServiceUnitVitalSign()
        {
        }

        public esServiceUnitVitalSign(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String serviceUnitID, String vitalSignID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, vitalSignID);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, vitalSignID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String vitalSignID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(serviceUnitID, vitalSignID);
            else
                return LoadByPrimaryKeyStoredProcedure(serviceUnitID, vitalSignID);
        }

        private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String vitalSignID)
        {
            esServiceUnitVitalSignQuery query = this.GetDynamicQuery();
            query.Where(query.ServiceUnitID == serviceUnitID, query.VitalSignID == vitalSignID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String vitalSignID)
        {
            esParameters parms = new esParameters();
            parms.Add("ServiceUnitID", serviceUnitID);
            parms.Add("VitalSignID", vitalSignID);
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
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "VitalSignID": this.str.VitalSignID = (string)value; break;
                        case "RowIndex": this.str.RowIndex = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RowIndex":

                            if (value == null || value is System.Int32)
                                this.RowIndex = (System.Int32?)value;
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
        /// Maps to ServiceUnitVitalSign.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(ServiceUnitVitalSignMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(ServiceUnitVitalSignMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitVitalSign.VitalSignID
        /// </summary>
        virtual public System.String VitalSignID
        {
            get
            {
                return base.GetSystemString(ServiceUnitVitalSignMetadata.ColumnNames.VitalSignID);
            }

            set
            {
                base.SetSystemString(ServiceUnitVitalSignMetadata.ColumnNames.VitalSignID, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitVitalSign.RowIndex
        /// </summary>
        virtual public System.Int32? RowIndex
        {
            get
            {
                return base.GetSystemInt32(ServiceUnitVitalSignMetadata.ColumnNames.RowIndex);
            }

            set
            {
                base.SetSystemInt32(ServiceUnitVitalSignMetadata.ColumnNames.RowIndex, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitVitalSign.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ServiceUnitVitalSignMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ServiceUnitVitalSignMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ServiceUnitVitalSign.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ServiceUnitVitalSignMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ServiceUnitVitalSignMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esServiceUnitVitalSign entity)
            {
                this.entity = entity;
            }
            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }
            public System.String VitalSignID
            {
                get
                {
                    System.String data = entity.VitalSignID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VitalSignID = null;
                    else entity.VitalSignID = Convert.ToString(value);
                }
            }
            public System.String RowIndex
            {
                get
                {
                    System.Int32? data = entity.RowIndex;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RowIndex = null;
                    else entity.RowIndex = Convert.ToInt32(value);
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
            private esServiceUnitVitalSign entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esServiceUnitVitalSignQuery query)
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
                throw new Exception("esServiceUnitVitalSign can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ServiceUnitVitalSign : esServiceUnitVitalSign
    {
    }

    [Serializable]
    abstract public class esServiceUnitVitalSignQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitVitalSignMetadata.Meta();
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitVitalSignMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem VitalSignID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitVitalSignMetadata.ColumnNames.VitalSignID, esSystemType.String);
            }
        }

        public esQueryItem RowIndex
        {
            get
            {
                return new esQueryItem(this, ServiceUnitVitalSignMetadata.ColumnNames.RowIndex, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ServiceUnitVitalSignMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ServiceUnitVitalSignMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ServiceUnitVitalSignCollection")]
    public partial class ServiceUnitVitalSignCollection : esServiceUnitVitalSignCollection, IEnumerable<ServiceUnitVitalSign>
    {
        public ServiceUnitVitalSignCollection()
        {

        }

        public static implicit operator List<ServiceUnitVitalSign>(ServiceUnitVitalSignCollection coll)
        {
            List<ServiceUnitVitalSign> list = new List<ServiceUnitVitalSign>();

            foreach (ServiceUnitVitalSign emp in coll)
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
                return ServiceUnitVitalSignMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitVitalSignQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ServiceUnitVitalSign(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ServiceUnitVitalSign();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ServiceUnitVitalSignQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitVitalSignQuery();
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
        public bool Load(ServiceUnitVitalSignQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ServiceUnitVitalSign AddNew()
        {
            ServiceUnitVitalSign entity = base.AddNewEntity() as ServiceUnitVitalSign;

            return entity;
        }
        public ServiceUnitVitalSign FindByPrimaryKey(String serviceUnitID, String vitalSignID)
        {
            return base.FindByPrimaryKey(serviceUnitID, vitalSignID) as ServiceUnitVitalSign;
        }

        #region IEnumerable< ServiceUnitVitalSign> Members

        IEnumerator<ServiceUnitVitalSign> IEnumerable<ServiceUnitVitalSign>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ServiceUnitVitalSign;
            }
        }

        #endregion

        private ServiceUnitVitalSignQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ServiceUnitVitalSign' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ServiceUnitVitalSign ({ServiceUnitID, VitalSignID})")]
    [Serializable]
    public partial class ServiceUnitVitalSign : esServiceUnitVitalSign
    {
        public ServiceUnitVitalSign()
        {
        }

        public ServiceUnitVitalSign(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ServiceUnitVitalSignMetadata.Meta();
            }
        }

        override protected esServiceUnitVitalSignQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ServiceUnitVitalSignQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ServiceUnitVitalSignQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ServiceUnitVitalSignQuery();
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
        public bool Load(ServiceUnitVitalSignQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ServiceUnitVitalSignQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ServiceUnitVitalSignQuery : esServiceUnitVitalSignQuery
    {
        public ServiceUnitVitalSignQuery()
        {

        }

        public ServiceUnitVitalSignQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ServiceUnitVitalSignQuery";
        }
    }

    [Serializable]
    public partial class ServiceUnitVitalSignMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ServiceUnitVitalSignMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ServiceUnitVitalSignMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitVitalSignMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitVitalSignMetadata.ColumnNames.VitalSignID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitVitalSignMetadata.PropertyNames.VitalSignID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitVitalSignMetadata.ColumnNames.RowIndex, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ServiceUnitVitalSignMetadata.PropertyNames.RowIndex;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitVitalSignMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ServiceUnitVitalSignMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ServiceUnitVitalSignMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ServiceUnitVitalSignMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ServiceUnitVitalSignMetadata Meta()
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
            public const string ServiceUnitID = "ServiceUnitID";
            public const string VitalSignID = "VitalSignID";
            public const string RowIndex = "RowIndex";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ServiceUnitID = "ServiceUnitID";
            public const string VitalSignID = "VitalSignID";
            public const string RowIndex = "RowIndex";
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
            lock (typeof(ServiceUnitVitalSignMetadata))
            {
                if (ServiceUnitVitalSignMetadata.mapDelegates == null)
                {
                    ServiceUnitVitalSignMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ServiceUnitVitalSignMetadata.meta == null)
                {
                    ServiceUnitVitalSignMetadata.meta = new ServiceUnitVitalSignMetadata();
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

                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VitalSignID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RowIndex", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ServiceUnitVitalSign";
                meta.Destination = "ServiceUnitVitalSign";
                meta.spInsert = "proc_ServiceUnitVitalSignInsert";
                meta.spUpdate = "proc_ServiceUnitVitalSignUpdate";
                meta.spDelete = "proc_ServiceUnitVitalSignDelete";
                meta.spLoadAll = "proc_ServiceUnitVitalSignLoadAll";
                meta.spLoadByPrimaryKey = "proc_ServiceUnitVitalSignLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ServiceUnitVitalSignMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
