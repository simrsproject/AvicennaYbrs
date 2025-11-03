/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 30/12/2024 11:17:27
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
    abstract public class esRlMasterReportItemV2025Collection : esEntityCollectionWAuditLog
    {
        public esRlMasterReportItemV2025Collection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RlMasterReportItemV2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlMasterReportItemV2025Query query)
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
            this.InitQuery(query as esRlMasterReportItemV2025Query);
        }
        #endregion

        virtual public RlMasterReportItemV2025 DetachEntity(RlMasterReportItemV2025 entity)
        {
            return base.DetachEntity(entity) as RlMasterReportItemV2025;
        }

        virtual public RlMasterReportItemV2025 AttachEntity(RlMasterReportItemV2025 entity)
        {
            return base.AttachEntity(entity) as RlMasterReportItemV2025;
        }

        virtual public void Combine(RlMasterReportItemV2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlMasterReportItemV2025 this[int index]
        {
            get
            {
                return base[index] as RlMasterReportItemV2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlMasterReportItemV2025);
        }
    }

    [Serializable]
    abstract public class esRlMasterReportItemV2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlMasterReportItemV2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlMasterReportItemV2025()
        {
        }

        public esRlMasterReportItemV2025(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Int32 rlMasterReportItemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlMasterReportItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlMasterReportItemID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 rlMasterReportItemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlMasterReportItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlMasterReportItemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.Int32 rlMasterReportItemID)
        {
            esRlMasterReportItemV2025Query query = this.GetDynamicQuery();
            query.Where(query.RlMasterReportItemID == rlMasterReportItemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Int32 rlMasterReportItemID)
        {
            esParameters parms = new esParameters();
            parms.Add("RlMasterReportItemID", rlMasterReportItemID);
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
                        case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
                        case "RlMasterReportID": this.str.RlMasterReportID = (string)value; break;
                        case "RlMasterReportItemNo": this.str.RlMasterReportItemNo = (string)value; break;
                        case "RlMasterReportItemCode": this.str.RlMasterReportItemCode = (string)value; break;
                        case "RlMasterReportItemName": this.str.RlMasterReportItemName = (string)value; break;
                        case "SRParamedicRL1": this.str.SRParamedicRL1 = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "ParameterValue": this.str.ParameterValue = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RlMasterReportItemID":

                            if (value == null || value is System.Int32)
                                this.RlMasterReportItemID = (System.Int32?)value;
                            break;

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
        /// Maps to RlMasterReportItem.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.RlMasterReportID
        /// </summary>
        virtual public System.Int32? RlMasterReportID
        {
            get
            {
                return base.GetSystemInt32(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportID);
            }

            set
            {
                base.SetSystemInt32(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportID, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.RlMasterReportItemNo
        /// </summary>
        virtual public System.String RlMasterReportItemNo
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemNo);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemNo, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.RlMasterReportItemCode
        /// </summary>
        virtual public System.String RlMasterReportItemCode
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemCode);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemCode, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.RlMasterReportItemName
        /// </summary>
        virtual public System.String RlMasterReportItemName
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemName);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemName, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.SRParamedicRL1
        /// </summary>
        virtual public System.String SRParamedicRL1
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.SRParamedicRL1);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.SRParamedicRL1, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(RlMasterReportItemV2025Metadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(RlMasterReportItemV2025Metadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.ParameterValue
        /// </summary>
        virtual public System.String ParameterValue
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.ParameterValue);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.ParameterValue, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlMasterReportItemV2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlMasterReportItemV2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemV2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlMasterReportItemV2025 entity)
            {
                this.entity = entity;
            }

            public System.String RlMasterReportItemID
            {
                get
                {
                    System.Int32? data = entity.RlMasterReportItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
                    else entity.RlMasterReportItemID = Convert.ToInt32(value);
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

            public System.String RlMasterReportItemNo
            {
                get
                {
                    System.String data = entity.RlMasterReportItemNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemNo = null;
                    else entity.RlMasterReportItemNo = Convert.ToString(value);
                }
            }

            public System.String RlMasterReportItemCode
            {
                get
                {
                    System.String data = entity.RlMasterReportItemCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemCode = null;
                    else entity.RlMasterReportItemCode = Convert.ToString(value);
                }
            }

            public System.String RlMasterReportItemName
            {
                get
                {
                    System.String data = entity.RlMasterReportItemName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemName = null;
                    else entity.RlMasterReportItemName = Convert.ToString(value);
                }
            }

            public System.String SRParamedicRL1
            {
                get
                {
                    System.String data = entity.SRParamedicRL1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRParamedicRL1 = null;
                    else entity.SRParamedicRL1 = Convert.ToString(value);
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

            public System.String ParameterValue
            {
                get
                {
                    System.String data = entity.ParameterValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParameterValue = null;
                    else entity.ParameterValue = Convert.ToString(value);
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

            private esRlMasterReportItemV2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlMasterReportItemV2025Query query)
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
                throw new Exception("esRlMasterReportItemV2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RlMasterReportItemV2025 : esRlMasterReportItemV2025
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
    abstract public class esRlMasterReportItemV2025Query : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RlMasterReportItemV2025Metadata.Meta();
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem RlMasterReportID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportID, esSystemType.Int32);
            }
        }

        public esQueryItem RlMasterReportItemNo
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemCode
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemCode, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemName
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemName, esSystemType.String);
            }
        }

        public esQueryItem SRParamedicRL1
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.SRParamedicRL1, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem ParameterValue
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.ParameterValue, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemV2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }
    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlMasterReportItemV2025Collection")]
    public partial class RlMasterReportItemV2025Collection : esRlMasterReportItemV2025Collection, IEnumerable<RlMasterReportItemV2025>
    {
        public RlMasterReportItemV2025Collection()
        {

        }

        public static implicit operator List<RlMasterReportItemV2025>(RlMasterReportItemV2025Collection coll)
        {
            List<RlMasterReportItemV2025> list = new List<RlMasterReportItemV2025>();

            foreach (RlMasterReportItemV2025 emp in coll)
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
                return RlMasterReportItemV2025Metadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlMasterReportItemV2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlMasterReportItemV2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlMasterReportItemV2025();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RlMasterReportItemV2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlMasterReportItemV2025Query();
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
        public bool Load(RlMasterReportItemV2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RlMasterReportItemV2025 AddNew()
        {
            RlMasterReportItemV2025 entity = base.AddNewEntity() as RlMasterReportItemV2025;

            return entity;
        }
        public RlMasterReportItemV2025 FindByPrimaryKey(System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlMasterReportItemID) as RlMasterReportItemV2025;
        }

        #region IEnumerable< RlMasterReportItemV2025> Members

        IEnumerator<RlMasterReportItemV2025> IEnumerable<RlMasterReportItemV2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlMasterReportItemV2025;
            }
        }

        #endregion

        private RlMasterReportItemV2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlMasterReportItemV2025' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RlMasterReportItemV2025 ({RlMasterReportItemID})")]
    [Serializable]
    public partial class RlMasterReportItemV2025 : esRlMasterReportItemV2025
    {
        public RlMasterReportItemV2025()
        {
        }

        public RlMasterReportItemV2025(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlMasterReportItemV2025Metadata.Meta();
            }
        }

        override protected esRlMasterReportItemV2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlMasterReportItemV2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RlMasterReportItemV2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlMasterReportItemV2025Query();
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
        public bool Load(RlMasterReportItemV2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlMasterReportItemV2025Query query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RlMasterReportItemV2025Query : esRlMasterReportItemV2025Query
    {
        public RlMasterReportItemV2025Query()
        {

        }

        public RlMasterReportItemV2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlMasterReportItemV2025Query";
        }
    }

    [Serializable]
    public partial class RlMasterReportItemV2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlMasterReportItemV2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.RlMasterReportID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.RlMasterReportItemNo;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemCode, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.RlMasterReportItemCode;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.RlMasterReportItemName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.RlMasterReportItemName;
            c.CharacterMaxLength = 300;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.SRParamedicRL1, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.SRParamedicRL1;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.ParameterValue, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.ParameterValue;
            c.CharacterMaxLength = 4000;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemV2025Metadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemV2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

        }
        #endregion

        static public RlMasterReportItemV2025Metadata Meta()
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
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string RlMasterReportID = "RlMasterReportID";
            public const string RlMasterReportItemNo = "RlMasterReportItemNo";
            public const string RlMasterReportItemCode = "RlMasterReportItemCode";
            public const string RlMasterReportItemName = "RlMasterReportItemName";
            public const string SRParamedicRL1 = "SRParamedicRL1";
            public const string IsActive = "IsActive";
            public const string ParameterValue = "ParameterValue";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string RlMasterReportID = "RlMasterReportID";
            public const string RlMasterReportItemNo = "RlMasterReportItemNo";
            public const string RlMasterReportItemCode = "RlMasterReportItemCode";
            public const string RlMasterReportItemName = "RlMasterReportItemName";
            public const string SRParamedicRL1 = "SRParamedicRL1";
            public const string IsActive = "IsActive";
            public const string ParameterValue = "ParameterValue";
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
            lock (typeof(RlMasterReportItemV2025Metadata))
            {
                if (RlMasterReportItemV2025Metadata.mapDelegates == null)
                {
                    RlMasterReportItemV2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlMasterReportItemV2025Metadata.meta == null)
                {
                    RlMasterReportItemV2025Metadata.meta = new RlMasterReportItemV2025Metadata();
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

                meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RlMasterReportID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RlMasterReportItemNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportItemCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRParamedicRL1", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ParameterValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));

                meta.Source = "RlMasterReportItemV2025";
                meta.Destination = "RlMasterReportItemV2025";
                meta.spInsert = "proc_RlMasterReportItemV2025Insert";
                meta.spUpdate = "proc_RlMasterReportItemV2025Update";
                meta.spDelete = "proc_RlMasterReportItemV2025Delete";
                meta.spLoadAll = "proc_RlMasterReportItemV2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlMasterReportItemV2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlMasterReportItemV2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}