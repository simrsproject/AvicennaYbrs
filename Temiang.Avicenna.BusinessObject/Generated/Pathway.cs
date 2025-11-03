/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 06/07/19 9:24:14 AM
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
    abstract public class esPathwayCollection : esEntityCollectionWAuditLog
    {
        public esPathwayCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PathwayCollection";
        }

        #region Query Logic
        protected void InitQuery(esPathwayQuery query)
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
            this.InitQuery(query as esPathwayQuery);
        }
        #endregion

        virtual public Pathway DetachEntity(Pathway entity)
        {
            return base.DetachEntity(entity) as Pathway;
        }

        virtual public Pathway AttachEntity(Pathway entity)
        {
            return base.AttachEntity(entity) as Pathway;
        }

        virtual public void Combine(PathwayCollection collection)
        {
            base.Combine(collection);
        }

        new public Pathway this[int index]
        {
            get
            {
                return base[index] as Pathway;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Pathway);
        }
    }

    [Serializable]
    abstract public class esPathway : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPathwayQuery GetDynamicQuery()
        {
            return null;
        }

        public esPathway()
        {
        }

        public esPathway(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String pathwayID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String pathwayID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID);
        }

        private bool LoadByPrimaryKeyDynamic(String pathwayID)
        {
            esPathwayQuery query = this.GetDynamicQuery();
            query.Where(query.PathwayID == pathwayID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String pathwayID)
        {
            esParameters parms = new esParameters();
            parms.Add("PathwayID", pathwayID);
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
                        case "PathwayName": this.str.PathwayName = (string)value; break;
                        case "StartingDate": this.str.StartingDate = (string)value; break;
                        case "CoverageValue1": this.str.CoverageValue1 = (string)value; break;
                        case "CoverageValue2": this.str.CoverageValue2 = (string)value; break;
                        case "CoverageValue3": this.str.CoverageValue3 = (string)value; break;
                        case "ALOS": this.str.ALOS = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartingDate":

                            if (value == null || value is System.DateTime)
                                this.StartingDate = (System.DateTime?)value;
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
                        case "ALOS":

                            if (value == null || value is System.Int32)
                                this.ALOS = (System.Int32?)value;
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
        /// Maps to Pathway.PathwayID
        /// </summary>
        virtual public System.String PathwayID
        {
            get
            {
                return base.GetSystemString(PathwayMetadata.ColumnNames.PathwayID);
            }

            set
            {
                base.SetSystemString(PathwayMetadata.ColumnNames.PathwayID, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.PathwayName
        /// </summary>
        virtual public System.String PathwayName
        {
            get
            {
                return base.GetSystemString(PathwayMetadata.ColumnNames.PathwayName);
            }

            set
            {
                base.SetSystemString(PathwayMetadata.ColumnNames.PathwayName, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.StartingDate
        /// </summary>
        virtual public System.DateTime? StartingDate
        {
            get
            {
                return base.GetSystemDateTime(PathwayMetadata.ColumnNames.StartingDate);
            }

            set
            {
                base.SetSystemDateTime(PathwayMetadata.ColumnNames.StartingDate, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.CoverageValue1
        /// </summary>
        virtual public System.Decimal? CoverageValue1
        {
            get
            {
                return base.GetSystemDecimal(PathwayMetadata.ColumnNames.CoverageValue1);
            }

            set
            {
                base.SetSystemDecimal(PathwayMetadata.ColumnNames.CoverageValue1, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.CoverageValue2
        /// </summary>
        virtual public System.Decimal? CoverageValue2
        {
            get
            {
                return base.GetSystemDecimal(PathwayMetadata.ColumnNames.CoverageValue2);
            }

            set
            {
                base.SetSystemDecimal(PathwayMetadata.ColumnNames.CoverageValue2, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.CoverageValue3
        /// </summary>
        virtual public System.Decimal? CoverageValue3
        {
            get
            {
                return base.GetSystemDecimal(PathwayMetadata.ColumnNames.CoverageValue3);
            }

            set
            {
                base.SetSystemDecimal(PathwayMetadata.ColumnNames.CoverageValue3, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.ALOS
        /// </summary>
        virtual public System.Int32? ALOS
        {
            get
            {
                return base.GetSystemInt32(PathwayMetadata.ColumnNames.ALOS);
            }

            set
            {
                base.SetSystemInt32(PathwayMetadata.ColumnNames.ALOS, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PathwayMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PathwayMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(PathwayMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(PathwayMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PathwayMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PathwayMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Pathway.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PathwayMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PathwayMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPathway entity)
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
            public System.String PathwayName
            {
                get
                {
                    System.String data = entity.PathwayName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PathwayName = null;
                    else entity.PathwayName = Convert.ToString(value);
                }
            }
            public System.String StartingDate
            {
                get
                {
                    System.DateTime? data = entity.StartingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartingDate = null;
                    else entity.StartingDate = Convert.ToDateTime(value);
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
            public System.String ALOS
            {
                get
                {
                    System.Int32? data = entity.ALOS;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ALOS = null;
                    else entity.ALOS = Convert.ToInt32(value);
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
            private esPathway entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPathwayQuery query)
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
                throw new Exception("esPathway can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Pathway : esPathway
    {
    }

    [Serializable]
    abstract public class esPathwayQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PathwayMetadata.Meta();
            }
        }

        public esQueryItem PathwayID
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.PathwayID, esSystemType.String);
            }
        }

        public esQueryItem PathwayName
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.PathwayName, esSystemType.String);
            }
        }

        public esQueryItem StartingDate
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem CoverageValue1
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.CoverageValue1, esSystemType.Decimal);
            }
        }

        public esQueryItem CoverageValue2
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.CoverageValue2, esSystemType.Decimal);
            }
        }

        public esQueryItem CoverageValue3
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.CoverageValue3, esSystemType.Decimal);
            }
        }

        public esQueryItem ALOS
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.ALOS, esSystemType.Int32);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PathwayMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PathwayCollection")]
    public partial class PathwayCollection : esPathwayCollection, IEnumerable<Pathway>
    {
        public PathwayCollection()
        {

        }

        public static implicit operator List<Pathway>(PathwayCollection coll)
        {
            List<Pathway> list = new List<Pathway>();

            foreach (Pathway emp in coll)
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
                return PathwayMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathwayQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Pathway(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Pathway();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PathwayQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathwayQuery();
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
        public bool Load(PathwayQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Pathway AddNew()
        {
            Pathway entity = base.AddNewEntity() as Pathway;

            return entity;
        }
        public Pathway FindByPrimaryKey(String pathwayID)
        {
            return base.FindByPrimaryKey(pathwayID) as Pathway;
        }

        #region IEnumerable< Pathway> Members

        IEnumerator<Pathway> IEnumerable<Pathway>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Pathway;
            }
        }

        #endregion

        private PathwayQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Pathway' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Pathway ({PathwayID})")]
    [Serializable]
    public partial class Pathway : esPathway
    {
        public Pathway()
        {
        }

        public Pathway(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PathwayMetadata.Meta();
            }
        }

        override protected esPathwayQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PathwayQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PathwayQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PathwayQuery();
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
        public bool Load(PathwayQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PathwayQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PathwayQuery : esPathwayQuery
    {
        public PathwayQuery()
        {

        }

        public PathwayQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PathwayQuery";
        }
    }

    [Serializable]
    public partial class PathwayMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PathwayMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.PathwayID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayMetadata.PropertyNames.PathwayID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.PathwayName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayMetadata.PropertyNames.PathwayName;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.StartingDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathwayMetadata.PropertyNames.StartingDate;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.CoverageValue1, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PathwayMetadata.PropertyNames.CoverageValue1;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.CoverageValue2, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PathwayMetadata.PropertyNames.CoverageValue2;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.CoverageValue3, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = PathwayMetadata.PropertyNames.CoverageValue3;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.ALOS, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PathwayMetadata.PropertyNames.ALOS;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 2147483647;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.IsActive, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PathwayMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PathwayMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PathwayMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PathwayMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PathwayMetadata Meta()
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
            public const string PathwayName = "PathwayName";
            public const string StartingDate = "StartingDate";
            public const string CoverageValue1 = "CoverageValue1";
            public const string CoverageValue2 = "CoverageValue2";
            public const string CoverageValue3 = "CoverageValue3";
            public const string ALOS = "ALOS";
            public const string Notes = "Notes";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PathwayID = "PathwayID";
            public const string PathwayName = "PathwayName";
            public const string StartingDate = "StartingDate";
            public const string CoverageValue1 = "CoverageValue1";
            public const string CoverageValue2 = "CoverageValue2";
            public const string CoverageValue3 = "CoverageValue3";
            public const string ALOS = "ALOS";
            public const string Notes = "Notes";
            public const string IsActive = "IsActive";
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
            lock (typeof(PathwayMetadata))
            {
                if (PathwayMetadata.mapDelegates == null)
                {
                    PathwayMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PathwayMetadata.meta == null)
                {
                    PathwayMetadata.meta = new PathwayMetadata();
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
                meta.AddTypeMap("PathwayName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CoverageValue1", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CoverageValue2", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CoverageValue3", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ALOS", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "Pathway";
                meta.Destination = "Pathway";
                meta.spInsert = "proc_PathwayInsert";
                meta.spUpdate = "proc_PathwayUpdate";
                meta.spDelete = "proc_PathwayDelete";
                meta.spLoadAll = "proc_PathwayLoadAll";
                meta.spLoadByPrimaryKey = "proc_PathwayLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PathwayMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
