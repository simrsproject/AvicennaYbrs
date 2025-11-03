/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 26/07/2024 10:31:36
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
    abstract public class esAppStandardReferenceItemBridgingCollection : esEntityCollectionWAuditLog
    {
        public esAppStandardReferenceItemBridgingCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "AppStandardReferenceItemBridgingCollection";
        }

        #region Query Logic
        protected void InitQuery(esAppStandardReferenceItemBridgingQuery query)
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
            this.InitQuery(query as esAppStandardReferenceItemBridgingQuery);
        }
        #endregion

        virtual public AppStandardReferenceItemBridging DetachEntity(AppStandardReferenceItemBridging entity)
        {
            return base.DetachEntity(entity) as AppStandardReferenceItemBridging;
        }

        virtual public AppStandardReferenceItemBridging AttachEntity(AppStandardReferenceItemBridging entity)
        {
            return base.AttachEntity(entity) as AppStandardReferenceItemBridging;
        }

        virtual public void Combine(AppStandardReferenceItemBridgingCollection collection)
        {
            base.Combine(collection);
        }

        new public AppStandardReferenceItemBridging this[int index]
        {
            get
            {
                return base[index] as AppStandardReferenceItemBridging;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AppStandardReferenceItemBridging);
        }
    }

    [Serializable]
    abstract public class esAppStandardReferenceItemBridging : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAppStandardReferenceItemBridgingQuery GetDynamicQuery()
        {
            return null;
        }

        public esAppStandardReferenceItemBridging()
        {
        }

        public esAppStandardReferenceItemBridging(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String standardReferenceID, String itemID, String sRBridgingType)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(standardReferenceID, itemID, sRBridgingType);
            else
                return LoadByPrimaryKeyStoredProcedure(standardReferenceID, itemID, sRBridgingType);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String standardReferenceID, String itemID, String sRBridgingType)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(standardReferenceID, itemID, sRBridgingType);
            else
                return LoadByPrimaryKeyStoredProcedure(standardReferenceID, itemID, sRBridgingType);
        }

        private bool LoadByPrimaryKeyDynamic(String standardReferenceID, String itemID, String sRBridgingType)
        {
            esAppStandardReferenceItemBridgingQuery query = this.GetDynamicQuery();
            query.Where(query.StandardReferenceID == standardReferenceID, query.ItemID == itemID, query.SRBridgingType == sRBridgingType);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String standardReferenceID, String itemID, String sRBridgingType)
        {
            esParameters parms = new esParameters();
            parms.Add("StandardReferenceID", standardReferenceID);
            parms.Add("ItemID", itemID);
            parms.Add("SRBridgingType", sRBridgingType);
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
                        case "StandardReferenceID": this.str.StandardReferenceID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "SRBridgingType": this.str.SRBridgingType = (string)value; break;
                        case "BridgingID": this.str.BridgingID = (string)value; break;
                        case "BridgingName": this.str.BridgingName = (string)value; break;
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
        /// Maps to AppStandardReferenceItemBridging.StandardReferenceID
        /// </summary>
        virtual public System.String StandardReferenceID
        {
            get
            {
                return base.GetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.StandardReferenceID);
            }

            set
            {
                base.SetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.StandardReferenceID, value);
            }
        }
        /// <summary>
        /// Maps to AppStandardReferenceItemBridging.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to AppStandardReferenceItemBridging.SRBridgingType
        /// </summary>
        virtual public System.String SRBridgingType
        {
            get
            {
                return base.GetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.SRBridgingType);
            }

            set
            {
                base.SetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.SRBridgingType, value);
            }
        }
        /// <summary>
        /// Maps to AppStandardReferenceItemBridging.BridgingID
        /// </summary>
        virtual public System.String BridgingID
        {
            get
            {
                return base.GetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.BridgingID);
            }

            set
            {
                base.SetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.BridgingID, value);
            }
        }
        /// <summary>
        /// Maps to AppStandardReferenceItemBridging.BridgingName
        /// </summary>
        virtual public System.String BridgingName
        {
            get
            {
                return base.GetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.BridgingName);
            }

            set
            {
                base.SetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.BridgingName, value);
            }
        }
        /// <summary>
        /// Maps to AppStandardReferenceItemBridging.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AppStandardReferenceItemBridgingMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AppStandardReferenceItemBridgingMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to AppStandardReferenceItemBridging.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AppStandardReferenceItemBridgingMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAppStandardReferenceItemBridging entity)
            {
                this.entity = entity;
            }
            public System.String StandardReferenceID
            {
                get
                {
                    System.String data = entity.StandardReferenceID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StandardReferenceID = null;
                    else entity.StandardReferenceID = Convert.ToString(value);
                }
            }
            public System.String ItemID
            {
                get
                {
                    System.String data = entity.ItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemID = null;
                    else entity.ItemID = Convert.ToString(value);
                }
            }
            public System.String SRBridgingType
            {
                get
                {
                    System.String data = entity.SRBridgingType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRBridgingType = null;
                    else entity.SRBridgingType = Convert.ToString(value);
                }
            }
            public System.String BridgingID
            {
                get
                {
                    System.String data = entity.BridgingID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BridgingID = null;
                    else entity.BridgingID = Convert.ToString(value);
                }
            }
            public System.String BridgingName
            {
                get
                {
                    System.String data = entity.BridgingName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BridgingName = null;
                    else entity.BridgingName = Convert.ToString(value);
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
            private esAppStandardReferenceItemBridging entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAppStandardReferenceItemBridgingQuery query)
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
                throw new Exception("esAppStandardReferenceItemBridging can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class AppStandardReferenceItemBridging : esAppStandardReferenceItemBridging
    {
    }

    [Serializable]
    abstract public class esAppStandardReferenceItemBridgingQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return AppStandardReferenceItemBridgingMetadata.Meta();
            }
        }

        public esQueryItem StandardReferenceID
        {
            get
            {
                return new esQueryItem(this, AppStandardReferenceItemBridgingMetadata.ColumnNames.StandardReferenceID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, AppStandardReferenceItemBridgingMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem SRBridgingType
        {
            get
            {
                return new esQueryItem(this, AppStandardReferenceItemBridgingMetadata.ColumnNames.SRBridgingType, esSystemType.String);
            }
        }

        public esQueryItem BridgingID
        {
            get
            {
                return new esQueryItem(this, AppStandardReferenceItemBridgingMetadata.ColumnNames.BridgingID, esSystemType.String);
            }
        }

        public esQueryItem BridgingName
        {
            get
            {
                return new esQueryItem(this, AppStandardReferenceItemBridgingMetadata.ColumnNames.BridgingName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AppStandardReferenceItemBridgingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AppStandardReferenceItemBridgingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AppStandardReferenceItemBridgingCollection")]
    public partial class AppStandardReferenceItemBridgingCollection : esAppStandardReferenceItemBridgingCollection, IEnumerable<AppStandardReferenceItemBridging>
    {
        public AppStandardReferenceItemBridgingCollection()
        {

        }

        public static implicit operator List<AppStandardReferenceItemBridging>(AppStandardReferenceItemBridgingCollection coll)
        {
            List<AppStandardReferenceItemBridging> list = new List<AppStandardReferenceItemBridging>();

            foreach (AppStandardReferenceItemBridging emp in coll)
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
                return AppStandardReferenceItemBridgingMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppStandardReferenceItemBridgingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AppStandardReferenceItemBridging(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AppStandardReferenceItemBridging();
        }

        #endregion

        [BrowsableAttribute(false)]
        public AppStandardReferenceItemBridgingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppStandardReferenceItemBridgingQuery();
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
        public bool Load(AppStandardReferenceItemBridgingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public AppStandardReferenceItemBridging AddNew()
        {
            AppStandardReferenceItemBridging entity = base.AddNewEntity() as AppStandardReferenceItemBridging;

            return entity;
        }
        public AppStandardReferenceItemBridging FindByPrimaryKey(String standardReferenceID, String itemID, String sRBridgingType)
        {
            return base.FindByPrimaryKey(standardReferenceID, itemID, sRBridgingType) as AppStandardReferenceItemBridging;
        }

        #region IEnumerable< AppStandardReferenceItemBridging> Members

        IEnumerator<AppStandardReferenceItemBridging> IEnumerable<AppStandardReferenceItemBridging>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AppStandardReferenceItemBridging;
            }
        }

        #endregion

        private AppStandardReferenceItemBridgingQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AppStandardReferenceItemBridging' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AppStandardReferenceItemBridging ({StandardReferenceID, ItemID, SRBridgingType})")]
    [Serializable]
    public partial class AppStandardReferenceItemBridging : esAppStandardReferenceItemBridging
    {
        public AppStandardReferenceItemBridging()
        {
        }

        public AppStandardReferenceItemBridging(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AppStandardReferenceItemBridgingMetadata.Meta();
            }
        }

        override protected esAppStandardReferenceItemBridgingQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AppStandardReferenceItemBridgingQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public AppStandardReferenceItemBridgingQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AppStandardReferenceItemBridgingQuery();
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
        public bool Load(AppStandardReferenceItemBridgingQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AppStandardReferenceItemBridgingQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class AppStandardReferenceItemBridgingQuery : esAppStandardReferenceItemBridgingQuery
    {
        public AppStandardReferenceItemBridgingQuery()
        {

        }

        public AppStandardReferenceItemBridgingQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AppStandardReferenceItemBridgingQuery";
        }
    }

    [Serializable]
    public partial class AppStandardReferenceItemBridgingMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AppStandardReferenceItemBridgingMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AppStandardReferenceItemBridgingMetadata.ColumnNames.StandardReferenceID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AppStandardReferenceItemBridgingMetadata.PropertyNames.StandardReferenceID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 30;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(AppStandardReferenceItemBridgingMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = AppStandardReferenceItemBridgingMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(AppStandardReferenceItemBridgingMetadata.ColumnNames.SRBridgingType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = AppStandardReferenceItemBridgingMetadata.PropertyNames.SRBridgingType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AppStandardReferenceItemBridgingMetadata.ColumnNames.BridgingID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AppStandardReferenceItemBridgingMetadata.PropertyNames.BridgingID;
            c.CharacterMaxLength = 255;
            _columns.Add(c);

            c = new esColumnMetadata(AppStandardReferenceItemBridgingMetadata.ColumnNames.BridgingName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = AppStandardReferenceItemBridgingMetadata.PropertyNames.BridgingName;
            c.CharacterMaxLength = 255;
            _columns.Add(c);

            c = new esColumnMetadata(AppStandardReferenceItemBridgingMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AppStandardReferenceItemBridgingMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AppStandardReferenceItemBridgingMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = AppStandardReferenceItemBridgingMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public AppStandardReferenceItemBridgingMetadata Meta()
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
            public const string StandardReferenceID = "StandardReferenceID";
            public const string ItemID = "ItemID";
            public const string SRBridgingType = "SRBridgingType";
            public const string BridgingID = "BridgingID";
            public const string BridgingName = "BridgingName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string StandardReferenceID = "StandardReferenceID";
            public const string ItemID = "ItemID";
            public const string SRBridgingType = "SRBridgingType";
            public const string BridgingID = "BridgingID";
            public const string BridgingName = "BridgingName";
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
            lock (typeof(AppStandardReferenceItemBridgingMetadata))
            {
                if (AppStandardReferenceItemBridgingMetadata.mapDelegates == null)
                {
                    AppStandardReferenceItemBridgingMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AppStandardReferenceItemBridgingMetadata.meta == null)
                {
                    AppStandardReferenceItemBridgingMetadata.meta = new AppStandardReferenceItemBridgingMetadata();
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

                meta.AddTypeMap("StandardReferenceID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRBridgingType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BridgingID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BridgingName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "AppStandardReferenceItemBridging";
                meta.Destination = "AppStandardReferenceItemBridging";
                meta.spInsert = "proc_AppStandardReferenceItemBridgingInsert";
                meta.spUpdate = "proc_AppStandardReferenceItemBridgingUpdate";
                meta.spDelete = "proc_AppStandardReferenceItemBridgingDelete";
                meta.spLoadAll = "proc_AppStandardReferenceItemBridgingLoadAll";
                meta.spLoadByPrimaryKey = "proc_AppStandardReferenceItemBridgingLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AppStandardReferenceItemBridgingMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
