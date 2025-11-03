/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 30/12/2024 11:04:02
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
    abstract public class esRlMasterReportV2025Collection : esEntityCollectionWAuditLog
    {
        public esRlMasterReportV2025Collection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RlMasterReportV2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlMasterReportV2025Query query)
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
            this.InitQuery(query as esRlMasterReportV2025Query);
        }
        #endregion

        virtual public RlMasterReportV2025 DetachEntity(RlMasterReportV2025 entity)
        {
            return base.DetachEntity(entity) as RlMasterReportV2025;
        }

        virtual public RlMasterReportV2025 AttachEntity(RlMasterReportV2025 entity)
        {
            return base.AttachEntity(entity) as RlMasterReportV2025;
        }

        virtual public void Combine(RlMasterReportV2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlMasterReportV2025 this[int index]
        {
            get
            {
                return base[index] as RlMasterReportV2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlMasterReportV2025);
        }
    }

    [Serializable]
    abstract public class esRlMasterReportV2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlMasterReportV2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlMasterReportV2025()
        {
        }

        public esRlMasterReportV2025(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Int32 rlMasterReportID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlMasterReportID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlMasterReportID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 rlMasterReportID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlMasterReportID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlMasterReportID);
        }

        private bool LoadByPrimaryKeyDynamic(System.Int32 rlMasterReportID)
        {
            esRlMasterReportV2025Query query = this.GetDynamicQuery();
            query.Where(query.RlMasterReportID == rlMasterReportID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Int32 rlMasterReportID)
        {
            esParameters parms = new esParameters();
            parms.Add("RlMasterReportID", rlMasterReportID);
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
                        case "RlMasterReportID": this.str.RlMasterReportID = (string)value; break;
                        case "RlMasterReportNo": this.str.RlMasterReportNo = (string)value; break;
                        case "RlMasterReportName": this.str.RlMasterReportName = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RlMasterReportID":

                            if (value == null || value is System.Int32)
                                this.RlMasterReportID = (System.Int32?)value;
                            break;

                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to RlMasterReport.RlMasterReportID
        /// </summary>
        virtual public System.Int32? RlMasterReportID
        {
            get
            {
                return base.GetSystemInt32(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportID);
            }

            set
            {
                base.SetSystemInt32(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportID, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.RlMasterReportNo
        /// </summary>
        virtual public System.String RlMasterReportNo
        {
            get
            {
                return base.GetSystemString(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportNo);
            }

            set
            {
                base.SetSystemString(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.RlMasterReportName
        /// </summary>
        virtual public System.String RlMasterReportName
        {
            get
            {
                return base.GetSystemString(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportName);
            }

            set
            {
                base.SetSystemString(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportName, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(RlMasterReportV2025Metadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(RlMasterReportV2025Metadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlMasterReportV2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlMasterReportV2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlMasterReportV2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlMasterReportV2025Metadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReport.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(RlMasterReportV2025Metadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(RlMasterReportV2025Metadata.ColumnNames.Notes, value);
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
            public esStrings(esRlMasterReportV2025 entity)
            {
                this.entity = entity;
            }
            public System.String RlMasterReportID
            {
                get
                {
                    System.Int32? data = entity.RlMasterReportID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportID = null;
                    else entity.RlMasterReportID = Convert.ToInt32(value);
                }
            }

            public System.String RlMasterReportNo
            {
                get
                {
                    System.String data = entity.RlMasterReportNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportNo = null;
                    else entity.RlMasterReportNo = Convert.ToString(value);
                }
            }

            public System.String RlMasterReportName
            {
                get
                {
                    System.String data = entity.RlMasterReportName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportName = null;
                    else entity.RlMasterReportName = Convert.ToString(value);
                }
            }

            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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

            private esRlMasterReportV2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlMasterReportV2025Query query)
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
                throw new Exception("esRlMasterReportV2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RlMasterReportV2025 : esRlMasterReportV2025
    {
        /// <summary>
        /// Used internally by the entity's hierarchical properties.
        /// </summary>
        protected override List<esPropertyDescriptor> GetHierarchicalProperties()
        {
            List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();


            return props;
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PreSave.
        /// </summary>
        protected override void ApplyPreSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostSave.
        /// </summary>
        protected override void ApplyPostSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostOneToOneSave.
        /// </summary>
        protected override void ApplyPostOneSaveKeys()
        {
        }
    }

    [Serializable]
    abstract public class esRlMasterReportV2025Query : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RlMasterReportV2025Metadata.Meta();
            }
        }

        public esQueryItem RlMasterReportID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportV2025Metadata.ColumnNames.RlMasterReportID, esSystemType.Int32);
            }
        }

        public esQueryItem RlMasterReportNo
        {
            get
            {
                return new esQueryItem(this, RlMasterReportV2025Metadata.ColumnNames.RlMasterReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportName
        {
            get
            {
                return new esQueryItem(this, RlMasterReportV2025Metadata.ColumnNames.RlMasterReportName, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, RlMasterReportV2025Metadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlMasterReportV2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportV2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, RlMasterReportV2025Metadata.ColumnNames.Notes, esSystemType.String);
            }
        }
    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlMasterReportV2025Collection")]
    public partial class RlMasterReportV2025Collection : esRlMasterReportV2025Collection, IEnumerable<RlMasterReportV2025>
    {
        public RlMasterReportV2025Collection()
        {

        }

        public static implicit operator List<RlMasterReportV2025>(RlMasterReportV2025Collection coll)
        {
            List<RlMasterReportV2025> list = new List<RlMasterReportV2025>();

            foreach (RlMasterReportV2025 emp in coll)
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
                return RlMasterReportV2025Metadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlMasterReportV2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlMasterReportV2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlMasterReportV2025();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RlMasterReportV2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlMasterReportV2025Query();
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
        public bool Load(RlMasterReportV2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RlMasterReportV2025 AddNew()
        {
            RlMasterReportV2025 entity = base.AddNewEntity() as RlMasterReportV2025;

            return entity;
        }
        public RlMasterReportV2025 FindByPrimaryKey(System.Int32 rlMasterReportID)
        {
            return base.FindByPrimaryKey(rlMasterReportID) as RlMasterReportV2025;
        }

        #region IEnumerable< RlMasterReportV2025> Members

        IEnumerator<RlMasterReportV2025> IEnumerable<RlMasterReportV2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlMasterReportV2025;
            }
        }

        #endregion

        private RlMasterReportV2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlMasterReportV2025' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RlMasterReportV2025 ({RlMasterReportID})")]
    [Serializable]
    public partial class RlMasterReportV2025 : esRlMasterReportV2025
    {
        public RlMasterReportV2025()
        {
        }

        public RlMasterReportV2025(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlMasterReportV2025Metadata.Meta();
            }
        }

        override protected esRlMasterReportV2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlMasterReportV2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RlMasterReportV2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlMasterReportV2025Query();
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
        public bool Load(RlMasterReportV2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlMasterReportV2025Query query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RlMasterReportV2025Query : esRlMasterReportV2025Query
    {
        public RlMasterReportV2025Query()
        {

        }

        public RlMasterReportV2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlMasterReportV2025Query";
        }
    }

    [Serializable]
    public partial class RlMasterReportV2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlMasterReportV2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlMasterReportV2025Metadata.PropertyNames.RlMasterReportID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportV2025Metadata.PropertyNames.RlMasterReportNo;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportV2025Metadata.ColumnNames.RlMasterReportName, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportV2025Metadata.PropertyNames.RlMasterReportName;
            c.CharacterMaxLength = 300;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportV2025Metadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RlMasterReportV2025Metadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportV2025Metadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlMasterReportV2025Metadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportV2025Metadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportV2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportV2025Metadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportV2025Metadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlMasterReportV2025Metadata Meta()
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
            public const string RlMasterReportID = "RlMasterReportID";
            public const string RlMasterReportNo = "RlMasterReportNo";
            public const string RlMasterReportName = "RlMasterReportName";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Notes = "Notes";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlMasterReportID = "RlMasterReportID";
            public const string RlMasterReportNo = "RlMasterReportNo";
            public const string RlMasterReportName = "RlMasterReportName";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string Notes = "Notes";
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
            lock (typeof(RlMasterReportV2025Metadata))
            {
                if (RlMasterReportV2025Metadata.mapDelegates == null)
                {
                    RlMasterReportV2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlMasterReportV2025Metadata.meta == null)
                {
                    RlMasterReportV2025Metadata.meta = new RlMasterReportV2025Metadata();
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

                meta.AddTypeMap("RlMasterReportID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RlMasterReportNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlMasterReportV2025";
                meta.Destination = "RlMasterReportV2025";
                meta.spInsert = "proc_RlMasterReportV2025Insert";
                meta.spUpdate = "proc_RlMasterReportV2025Update";
                meta.spDelete = "proc_RlMasterReportV2025Delete";
                meta.spLoadAll = "proc_RlMasterReportV2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlMasterReportV2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlMasterReportV2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}