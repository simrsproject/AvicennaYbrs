/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 06/07/19 9:24:51 AM
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
    abstract public class esPathwayItemCollection : esEntityCollectionWAuditLog
    {
        public esPathwayItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathwayItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathwayItemQuery query)
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
            this.InitQuery(query as esPathwayItemQuery);
        }
        #endregion

        virtual public PathwayItem DetachEntity(PathwayItem entity)
        {
            return base.DetachEntity(entity) as PathwayItem;
        }

        virtual public PathwayItem AttachEntity(PathwayItem entity)
        {
            return base.AttachEntity(entity) as PathwayItem;
        }

        virtual public void Combine(PathwayItemCollection collection)
        {
            base.Combine(collection);
        }

        new public PathwayItem this[int index]
        {
            get
            {
                return base[index] as PathwayItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PathwayItem);
        }
    }

    [Serializable]
    abstract public class esPathwayItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathwayItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathwayItem()
        {
        }

        public esPathwayItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String pathwayID, Int32 pathwayItemSeqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, pathwayItemSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, pathwayItemSeqNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String pathwayID, Int32 pathwayItemSeqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, pathwayItemSeqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, pathwayItemSeqNo);
        }

        private bool LoadByPrimaryKeyDynamic(String pathwayID, Int32 pathwayItemSeqNo)
        {
            esPathwayItemQuery query = this.GetDynamicQuery();
            query.Where(query.PathwayID == pathwayID, query.PathwayItemSeqNo == pathwayItemSeqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String pathwayID, Int32 pathwayItemSeqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PathwayID", pathwayID);
            parms.Add("PathwayItemSeqNo", pathwayItemSeqNo);
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
                        case "PathwayID": this.str.PathwayID = (string)value; break;
                        case "PathwayItemSeqNo": this.str.PathwayItemSeqNo = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "AssesmentGroupName": this.str.AssesmentGroupName = (string)value; break;
                        case "AssesmentName": this.str.AssesmentName = (string)value; break;
                        case "CoverageValue1": this.str.CoverageValue1 = (string)value; break;
                        case "CoverageValue2": this.str.CoverageValue2 = (string)value; break;
                        case "CoverageValue3": this.str.CoverageValue3 = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "AssesmentHeaderName": this.str.AssesmentHeaderName = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PathwayItemSeqNo":

                            if (value == null || value is System.Int32)
                                this.PathwayItemSeqNo = (System.Int32?)value;
                            break;
                        case "CoverageValue1":

                            if (value == null || value is System.Decimal)
                                this.CoverageValue1 = (System.Decimal?)value;
                            break;
                        case "CoverageValue2":

                            if (value == null || value is System.Decimal)
                                this.CoverageValue2 = (System.Decimal?)value;
                            break;
                        case "CoverageValue3":

                            if (value == null || value is System.Decimal)
                                this.CoverageValue3 = (System.Decimal?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "AssesmentHeaderName":

                            if (value == null || value is System.String)
                                this.AssesmentHeaderName = (System.String)value;
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
        /// Maps to PathwayItem.PathwayID
        /// </summary>
        virtual public System.String PathwayID
        {
            get
            {
                return base.GetSystemString(PathwayItemMetadata.ColumnNames.PathwayID);
            }

            set
            {
                base.SetSystemString(PathwayItemMetadata.ColumnNames.PathwayID, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.PathwayItemSeqNo
        /// </summary>
        virtual public System.Int32? PathwayItemSeqNo
        {
            get
            {
                return base.GetSystemInt32(PathwayItemMetadata.ColumnNames.PathwayItemSeqNo);
            }

            set
            {
                base.SetSystemInt32(PathwayItemMetadata.ColumnNames.PathwayItemSeqNo, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(PathwayItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(PathwayItemMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.AssesmentGroupName
        /// </summary>
        virtual public System.String AssesmentGroupName
        {
            get
            {
                return base.GetSystemString(PathwayItemMetadata.ColumnNames.AssesmentGroupName);
            }

            set
            {
                base.SetSystemString(PathwayItemMetadata.ColumnNames.AssesmentGroupName, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.AssesmentName
        /// </summary>
        virtual public System.String AssesmentName
        {
            get
            {
                return base.GetSystemString(PathwayItemMetadata.ColumnNames.AssesmentName);
            }

            set
            {
                base.SetSystemString(PathwayItemMetadata.ColumnNames.AssesmentName, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.CoverageValue1
        /// </summary>
        virtual public System.Decimal? CoverageValue1
        {
            get
            {
                return base.GetSystemDecimal(PathwayItemMetadata.ColumnNames.CoverageValue1);
            }

            set
            {
                base.SetSystemDecimal(PathwayItemMetadata.ColumnNames.CoverageValue1, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.CoverageValue2
        /// </summary>
        virtual public System.Decimal? CoverageValue2
        {
            get
            {
                return base.GetSystemDecimal(PathwayItemMetadata.ColumnNames.CoverageValue2);
            }

            set
            {
                base.SetSystemDecimal(PathwayItemMetadata.ColumnNames.CoverageValue2, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.CoverageValue3
        /// </summary>
        virtual public System.Decimal? CoverageValue3
        {
            get
            {
                return base.GetSystemDecimal(PathwayItemMetadata.ColumnNames.CoverageValue3);
            }

            set
            {
                base.SetSystemDecimal(PathwayItemMetadata.ColumnNames.CoverageValue3, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PathwayItemMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PathwayItemMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(PathwayItemMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(PathwayItemMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathwayItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathwayItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathwayItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathwayItemMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PathwayItem.AssesmentHeaderName
        /// </summary>
        virtual public System.String AssesmentHeaderName
        {
            get
            {
                return base.GetSystemString(PathwayItemMetadata.ColumnNames.AssesmentHeaderName);
            }

            set
            {
                base.SetSystemString(PathwayItemMetadata.ColumnNames.AssesmentHeaderName, value);
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
            public esStrings(esPathwayItem entity)
            {
                this.entity = entity;
            }
            public System.String PathwayID
            {
                get
                {
                    System.String data = entity.PathwayID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PathwayID = null;
                    else entity.PathwayID = Convert.ToString(value);
                }
            }
            public System.String PathwayItemSeqNo
            {
                get
                {
                    System.Int32? data = entity.PathwayItemSeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PathwayItemSeqNo = null;
                    else entity.PathwayItemSeqNo = Convert.ToInt32(value);
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
            public System.String AssesmentGroupName
            {
                get
                {
                    System.String data = entity.AssesmentGroupName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssesmentGroupName = null;
                    else entity.AssesmentGroupName = Convert.ToString(value);
                }
            }
            public System.String AssesmentName
            {
                get
                {
                    System.String data = entity.AssesmentName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssesmentName = null;
                    else entity.AssesmentName = Convert.ToString(value);
                }
            }
            public System.String CoverageValue1
            {
                get
                {
                    System.Decimal? data = entity.CoverageValue1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CoverageValue1 = null;
                    else entity.CoverageValue1 = Convert.ToDecimal(value);
                }
            }
            public System.String CoverageValue2
            {
                get
                {
                    System.Decimal? data = entity.CoverageValue2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CoverageValue2 = null;
                    else entity.CoverageValue2 = Convert.ToDecimal(value);
                }
            }
            public System.String CoverageValue3
            {
                get
                {
                    System.Decimal? data = entity.CoverageValue3;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CoverageValue3 = null;
                    else entity.CoverageValue3 = Convert.ToDecimal(value);
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
            public System.String AssesmentHeaderName
            {
                get
                {
                    System.String data = entity.AssesmentHeaderName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssesmentHeaderName = null;
                    else entity.AssesmentHeaderName = Convert.ToString(value);
                }
            }
            private esPathwayItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathwayItemQuery query)
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
                throw new Exception("esPathwayItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PathwayItem : esPathwayItem
    {
    }

    [Serializable]
    abstract public class esPathwayItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathwayItemMetadata.Meta();
            }
        }

        public esQueryItem PathwayID
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.PathwayID, esSystemType.String);
            }
        }

        public esQueryItem PathwayItemSeqNo
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.PathwayItemSeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem AssesmentGroupName
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.AssesmentGroupName, esSystemType.String);
            }
        }

        public esQueryItem AssesmentName
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.AssesmentName, esSystemType.String);
            }
        }

        public esQueryItem CoverageValue1
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.CoverageValue1, esSystemType.Decimal);
            }
        }

        public esQueryItem CoverageValue2
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.CoverageValue2, esSystemType.Decimal);
            }
        }

        public esQueryItem CoverageValue3
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.CoverageValue3, esSystemType.Decimal);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem AssesmentHeaderName
        {
            get
            {
                return new esQueryItem(this, PathwayItemMetadata.ColumnNames.AssesmentHeaderName, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathwayItemCollection")]
    public partial class PathwayItemCollection : esPathwayItemCollection, IEnumerable<PathwayItem>
    {
        public PathwayItemCollection()
        {

        }

        public static implicit operator List<PathwayItem>(PathwayItemCollection coll)
        {
            List<PathwayItem> list = new List<PathwayItem>();

            foreach (PathwayItem emp in coll)
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
                return PathwayItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathwayItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PathwayItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PathwayItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathwayItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathwayItemQuery();
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
        public bool Load(PathwayItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PathwayItem AddNew()
        {
            PathwayItem entity = base.AddNewEntity() as PathwayItem;

            return entity;
        }
        public PathwayItem FindByPrimaryKey(String pathwayID, Int32 pathwayItemSeqNo)
        {
            return base.FindByPrimaryKey(pathwayID, pathwayItemSeqNo) as PathwayItem;
        }

        #region IEnumerable< PathwayItem> Members

        IEnumerator<PathwayItem> IEnumerable<PathwayItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PathwayItem;
            }
        }

        #endregion

        private PathwayItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PathwayItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PathwayItem ({PathwayID, PathwayItemSeqNo})")]
    [Serializable]
    public partial class PathwayItem : esPathwayItem
    {
        public PathwayItem()
        {
        }

        public PathwayItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathwayItemMetadata.Meta();
            }
        }

        override protected esPathwayItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathwayItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathwayItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathwayItemQuery();
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
        public bool Load(PathwayItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathwayItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathwayItemQuery : esPathwayItemQuery
    {
        public PathwayItemQuery()
        {

        }

        public PathwayItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathwayItemQuery";
        }
    }

    [Serializable]
    public partial class PathwayItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathwayItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.PathwayID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemMetadata.PropertyNames.PathwayID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.PathwayItemSeqNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PathwayItemMetadata.PropertyNames.PathwayItemSeqNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemMetadata.PropertyNames.ItemID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.AssesmentGroupName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemMetadata.PropertyNames.AssesmentGroupName;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.AssesmentName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemMetadata.PropertyNames.AssesmentName;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.CoverageValue1, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PathwayItemMetadata.PropertyNames.CoverageValue1;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.CoverageValue2, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PathwayItemMetadata.PropertyNames.CoverageValue2;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.CoverageValue3, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PathwayItemMetadata.PropertyNames.CoverageValue3;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.IsActive, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PathwayItemMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathwayItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayItemMetadata.ColumnNames.AssesmentHeaderName, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayItemMetadata.PropertyNames.AssesmentHeaderName;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

        }
        #endregion

        static public PathwayItemMetadata Meta()
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
            public const string PathwayID = "PathwayID";
            public const string PathwayItemSeqNo = "PathwayItemSeqNo";
            public const string ItemID = "ItemID";
            public const string AssesmentGroupName = "AssesmentGroupName";
            public const string AssesmentName = "AssesmentName";
            public const string CoverageValue1 = "CoverageValue1";
            public const string CoverageValue2 = "CoverageValue2";
            public const string CoverageValue3 = "CoverageValue3";
            public const string Notes = "Notes";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string AssesmentHeaderName = "AssesmentHeaderName";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PathwayID = "PathwayID";
            public const string PathwayItemSeqNo = "PathwayItemSeqNo";
            public const string ItemID = "ItemID";
            public const string AssesmentGroupName = "AssesmentGroupName";
            public const string AssesmentName = "AssesmentName";
            public const string CoverageValue1 = "CoverageValue1";
            public const string CoverageValue2 = "CoverageValue2";
            public const string CoverageValue3 = "CoverageValue3";
            public const string Notes = "Notes";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string AssesmentHeaderName = "AssesmentHeaderName";
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
            lock (typeof(PathwayItemMetadata))
            {
                if (PathwayItemMetadata.mapDelegates == null)
                {
                    PathwayItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathwayItemMetadata.meta == null)
                {
                    PathwayItemMetadata.meta = new PathwayItemMetadata();
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

                meta.AddTypeMap("PathwayID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PathwayItemSeqNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssesmentGroupName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssesmentName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CoverageValue1", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CoverageValue2", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CoverageValue3", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssesmentHeaderName", new esTypeMap("varchar", "System.String"));


                meta.Source = "PathwayItem";
                meta.Destination = "PathwayItem";
                meta.spInsert = "proc_PathwayItemInsert";
                meta.spUpdate = "proc_PathwayItemUpdate";
                meta.spDelete = "proc_PathwayItemDelete";
                meta.spLoadAll = "proc_PathwayItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathwayItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathwayItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
