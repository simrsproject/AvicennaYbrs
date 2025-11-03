/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 30/12/2024 10:18:07
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
    abstract public class esRlTxReportV2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReportV2025Collection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RlTxReportV2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReportV2025Query query)
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
            this.InitQuery(query as esRlTxReportV2025Query);
        }
        #endregion

        virtual public RlTxReportV2025 DetachEntity(RlTxReportV2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReportV2025;
        }

        virtual public RlTxReportV2025 AttachEntity(RlTxReportV2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReportV2025;
        }

        virtual public void Combine(RlTxReportV2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReportV2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReportV2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReportV2025);
        }
    }

    [Serializable]
    abstract public class esRlTxReportV2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReportV2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReportV2025()
        {
        }

        public esRlTxReportV2025(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String RlTxReportNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(RlTxReportNo);
            else
                return LoadByPrimaryKeyStoredProcedure(RlTxReportNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String RlTxReportNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(RlTxReportNo);
            else
                return LoadByPrimaryKeyStoredProcedure(RlTxReportNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String RlTxReportNo)
        {
            esRlTxReportV2025Query query = this.GetDynamicQuery();
            query.Where(query.RlTxReportNo == RlTxReportNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String RlTxReportNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RlTxReportNo", RlTxReportNo);
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
                        case "RlTxReportNo": this.str.RlTxReportNo = (string)value; break;
                        case "RlMasterReportID": this.str.RlMasterReportID = (string)value; break;
                        case "PeriodYear": this.str.PeriodYear = (string)value; break;
                        case "PeriodMonthStart": this.str.PeriodMonthStart = (string)value; break;
                        case "PeriodMonthEnd": this.str.PeriodMonthEnd = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
                        case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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

                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;

                        case "ApprovedDateTime":

                            if (value == null || value is System.DateTime)
                                this.ApprovedDateTime = (System.DateTime?)value;
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
        /// Maps to RlTxReportV2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReportV2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReportV2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.RlMasterReportID
        /// </summary>
        virtual public System.Int32? RlMasterReportID
        {
            get
            {
                return base.GetSystemInt32(RlTxReportV2025Metadata.ColumnNames.RlMasterReportID);
            }

            set
            {
                base.SetSystemInt32(RlTxReportV2025Metadata.ColumnNames.RlMasterReportID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.PeriodYear
        /// </summary>
        virtual public System.String PeriodYear
        {
            get
            {
                return base.GetSystemString(RlTxReportV2025Metadata.ColumnNames.PeriodYear);
            }

            set
            {
                base.SetSystemString(RlTxReportV2025Metadata.ColumnNames.PeriodYear, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.PeriodMonthStart
        /// </summary>
        virtual public System.String PeriodMonthStart
        {
            get
            {
                return base.GetSystemString(RlTxReportV2025Metadata.ColumnNames.PeriodMonthStart);
            }

            set
            {
                base.SetSystemString(RlTxReportV2025Metadata.ColumnNames.PeriodMonthStart, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.PeriodMonthEnd
        /// </summary>
        virtual public System.String PeriodMonthEnd
        {
            get
            {
                return base.GetSystemString(RlTxReportV2025Metadata.ColumnNames.PeriodMonthEnd);
            }

            set
            {
                base.SetSystemString(RlTxReportV2025Metadata.ColumnNames.PeriodMonthEnd, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(RlTxReportV2025Metadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(RlTxReportV2025Metadata.ColumnNames.IsApproved, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.ApprovedDateTime
        /// </summary>
        virtual public System.DateTime? ApprovedDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReportV2025Metadata.ColumnNames.ApprovedDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReportV2025Metadata.ColumnNames.ApprovedDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.ApprovedByUserID
        /// </summary>
        virtual public System.String ApprovedByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReportV2025Metadata.ColumnNames.ApprovedByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReportV2025Metadata.ColumnNames.ApprovedByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReportV2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReportV2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReportV2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReportV2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReportV2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReportV2025 entity)
            {
                this.entity = entity;
            }

            public System.String RlTxReportNo
            {
                get
                {
                    System.String data = entity.RlTxReportNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlTxReportNo = null;
                    else entity.RlTxReportNo = Convert.ToString(value);
                }
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

            public System.String PeriodYear
            {
                get
                {
                    System.String data = entity.PeriodYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PeriodYear = null;
                    else entity.PeriodYear = Convert.ToString(value);
                }
            }

            public System.String PeriodMonthStart
            {
                get
                {
                    System.String data = entity.PeriodMonthStart;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PeriodMonthStart = null;
                    else entity.PeriodMonthStart = Convert.ToString(value);
                }
            }

            public System.String PeriodMonthEnd
            {
                get
                {
                    System.String data = entity.PeriodMonthEnd;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PeriodMonthEnd = null;
                    else entity.PeriodMonthEnd = Convert.ToString(value);
                }
            }

            public System.String IsApproved
            {
                get
                {
                    System.Boolean? data = entity.IsApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproved = null;
                    else entity.IsApproved = Convert.ToBoolean(value);
                }
            }

            public System.String ApprovedDateTime
            {
                get
                {
                    System.DateTime? data = entity.ApprovedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
                    else entity.ApprovedDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String ApprovedByUserID
            {
                get
                {
                    System.String data = entity.ApprovedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
                    else entity.ApprovedByUserID = Convert.ToString(value);
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

            private esRlTxReportV2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReportV2025Query query)
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
                throw new Exception("esRlTxReportV2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RlTxReportV2025 : esRlTxReportV2025
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
    abstract public class esRlTxReportV2025Query : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RlTxReportV2025Metadata.Meta();
            }
        }

        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportID
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.RlMasterReportID, esSystemType.Int32);
            }
        }

        public esQueryItem PeriodYear
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.PeriodYear, esSystemType.String);
            }
        }

        public esQueryItem PeriodMonthStart
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.PeriodMonthStart, esSystemType.String);
            }
        }

        public esQueryItem PeriodMonthEnd
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.PeriodMonthEnd, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem ApprovedDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ApprovedByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.ApprovedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReportV2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReportV2025Collection")]
    public partial class RlTxReportV2025Collection : esRlTxReportV2025Collection, IEnumerable<RlTxReportV2025>
    {
        public RlTxReportV2025Collection()
        {

        }

        public static implicit operator List<RlTxReportV2025>(RlTxReportV2025Collection coll)
        {
            List<RlTxReportV2025> list = new List<RlTxReportV2025>();

            foreach (RlTxReportV2025 emp in coll)
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
                return RlTxReportV2025Metadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReportV2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReportV2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReportV2025();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RlTxReportV2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReportV2025Query();
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
        public bool Load(RlTxReportV2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RlTxReportV2025 AddNew()
        {
            RlTxReportV2025 entity = base.AddNewEntity() as RlTxReportV2025;

            return entity;
        }
        public RlTxReportV2025 FindByPrimaryKey(System.String RlTxReportNo)
        {
            return base.FindByPrimaryKey(RlTxReportNo) as RlTxReportV2025;
        }

        #region IEnumerable< RlTxReportV2025> Members

        IEnumerator<RlTxReportV2025> IEnumerable<RlTxReportV2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReportV2025;
            }
        }

        #endregion

        private RlTxReportV2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReportV2025' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RlTxReportV2025 ({RlTxReportNo})")]
    [Serializable]
    public partial class RlTxReportV2025 : esRlTxReportV2025
    {
        public RlTxReportV2025()
        {
        }

        public RlTxReportV2025(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReportV2025Metadata.Meta();
            }
        }

        override protected esRlTxReportV2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReportV2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RlTxReportV2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReportV2025Query();
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
        public bool Load(RlTxReportV2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReportV2025Query query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RlTxReportV2025Query : esRlTxReportV2025Query
    {
        public RlTxReportV2025Query()
        {

        }

        public RlTxReportV2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReportV2025Query";
        }
    }

    [Serializable]
    public partial class RlTxReportV2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReportV2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.RlMasterReportID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.RlMasterReportID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.PeriodYear, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.PeriodYear;
            c.CharacterMaxLength = 4;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.PeriodMonthStart, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.PeriodMonthStart;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.PeriodMonthEnd, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.PeriodMonthEnd;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.IsApproved, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.ApprovedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.ApprovedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.ApprovedByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.ApprovedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReportV2025Metadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReportV2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReportV2025Metadata Meta()
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
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportID = "RlMasterReportID";
            public const string PeriodYear = "PeriodYear";
            public const string PeriodMonthStart = "PeriodMonthStart";
            public const string PeriodMonthEnd = "PeriodMonthEnd";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportID = "RlMasterReportID";
            public const string PeriodYear = "PeriodYear";
            public const string PeriodMonthStart = "PeriodMonthStart";
            public const string PeriodMonthEnd = "PeriodMonthEnd";
            public const string IsApproved = "IsApproved";
            public const string ApprovedDateTime = "ApprovedDateTime";
            public const string ApprovedByUserID = "ApprovedByUserID";
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
            lock (typeof(RlTxReportV2025Metadata))
            {
                if (RlTxReportV2025Metadata.mapDelegates == null)
                {
                    RlTxReportV2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReportV2025Metadata.meta == null)
                {
                    RlTxReportV2025Metadata.meta = new RlTxReportV2025Metadata();
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

                meta.AddTypeMap("RlTxReportNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PeriodMonthStart", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PeriodMonthEnd", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "RlTxReportV2025";
                meta.Destination = "RlTxReportV2025";
                meta.spInsert = "proc_RlTxReportV2025Insert";
                meta.spUpdate = "proc_RlTxReportV2025Update";
                meta.spDelete = "proc_RlTxReportV2025Delete";
                meta.spLoadAll = "proc_RlTxReportV2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReportV2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReportV2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
