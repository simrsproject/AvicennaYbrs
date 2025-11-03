/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/6/2017 3:54:52 PM
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
    abstract public class esPpiRiskFactorsItemCollection : esEntityCollectionWAuditLog
    {
        public esPpiRiskFactorsItemCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PpiRiskFactorsItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esPpiRiskFactorsItemQuery query)
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
            this.InitQuery(query as esPpiRiskFactorsItemQuery);
        }
        #endregion

        virtual public PpiRiskFactorsItem DetachEntity(PpiRiskFactorsItem entity)
        {
            return base.DetachEntity(entity) as PpiRiskFactorsItem;
        }

        virtual public PpiRiskFactorsItem AttachEntity(PpiRiskFactorsItem entity)
        {
            return base.AttachEntity(entity) as PpiRiskFactorsItem;
        }

        virtual public void Combine(PpiRiskFactorsItemCollection collection)
        {
            base.Combine(collection);
        }

        new public PpiRiskFactorsItem this[int index]
        {
            get
            {
                return base[index] as PpiRiskFactorsItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PpiRiskFactorsItem);
        }
    }

    [Serializable]
    abstract public class esPpiRiskFactorsItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPpiRiskFactorsItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esPpiRiskFactorsItem()
        {
        }

        public esPpiRiskFactorsItem(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, String sequenceNo, DateTime dateOfInfection, String sRSignsOfInfection)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo, dateOfInfection, sRSignsOfInfection);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo, dateOfInfection, sRSignsOfInfection);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String sequenceNo, DateTime dateOfInfection, String sRSignsOfInfection)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo, dateOfInfection, sRSignsOfInfection);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo, dateOfInfection, sRSignsOfInfection);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, String sequenceNo, DateTime dateOfInfection, String sRSignsOfInfection)
        {
            esPpiRiskFactorsItemQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo, query.DateOfInfection == dateOfInfection, query.SRSignsOfInfection == sRSignsOfInfection);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String sequenceNo, DateTime dateOfInfection, String sRSignsOfInfection)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            parms.Add("SequenceNo", sequenceNo);
            parms.Add("DateOfInfection", dateOfInfection);
            parms.Add("SRSignsOfInfection", sRSignsOfInfection);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "DateOfInfection": this.str.DateOfInfection = (string)value; break;
                        case "SRSignsOfInfection": this.str.SRSignsOfInfection = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "DateOfInfection":

                            if (value == null || value is System.DateTime)
                                this.DateOfInfection = (System.DateTime?)value;
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
        /// Maps to PpiRiskFactorsItem.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactorsItem.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactorsItem.DateOfInfection
        /// </summary>
        virtual public System.DateTime? DateOfInfection
        {
            get
            {
                return base.GetSystemDateTime(PpiRiskFactorsItemMetadata.ColumnNames.DateOfInfection);
            }

            set
            {
                base.SetSystemDateTime(PpiRiskFactorsItemMetadata.ColumnNames.DateOfInfection, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactorsItem.SRSignsOfInfection
        /// </summary>
        virtual public System.String SRSignsOfInfection
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.SRSignsOfInfection);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.SRSignsOfInfection, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactorsItem.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactorsItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PpiRiskFactorsItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PpiRiskFactorsItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PpiRiskFactorsItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PpiRiskFactorsItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPpiRiskFactorsItem entity)
            {
                this.entity = entity;
            }
            public System.String RegistrationNo
            {
                get
                {
                    System.String data = entity.RegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
                    else entity.RegistrationNo = Convert.ToString(value);
                }
            }
            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }
            public System.String DateOfInfection
            {
                get
                {
                    System.DateTime? data = entity.DateOfInfection;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DateOfInfection = null;
                    else entity.DateOfInfection = Convert.ToDateTime(value);
                }
            }
            public System.String SRSignsOfInfection
            {
                get
                {
                    System.String data = entity.SRSignsOfInfection;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRSignsOfInfection = null;
                    else entity.SRSignsOfInfection = Convert.ToString(value);
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
            private esPpiRiskFactorsItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPpiRiskFactorsItemQuery query)
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
                throw new Exception("esPpiRiskFactorsItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PpiRiskFactorsItem : esPpiRiskFactorsItem
    {
    }

    [Serializable]
    abstract public class esPpiRiskFactorsItemQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PpiRiskFactorsItemMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem DateOfInfection
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsItemMetadata.ColumnNames.DateOfInfection, esSystemType.DateTime);
            }
        }

        public esQueryItem SRSignsOfInfection
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsItemMetadata.ColumnNames.SRSignsOfInfection, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsItemMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PpiRiskFactorsItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PpiRiskFactorsItemCollection")]
    public partial class PpiRiskFactorsItemCollection : esPpiRiskFactorsItemCollection, IEnumerable<PpiRiskFactorsItem>
    {
        public PpiRiskFactorsItemCollection()
        {

        }

        public static implicit operator List<PpiRiskFactorsItem>(PpiRiskFactorsItemCollection coll)
        {
            List<PpiRiskFactorsItem> list = new List<PpiRiskFactorsItem>();

            foreach (PpiRiskFactorsItem emp in coll)
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
                return PpiRiskFactorsItemMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiRiskFactorsItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PpiRiskFactorsItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PpiRiskFactorsItem();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PpiRiskFactorsItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiRiskFactorsItemQuery();
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
        public bool Load(PpiRiskFactorsItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PpiRiskFactorsItem AddNew()
        {
            PpiRiskFactorsItem entity = base.AddNewEntity() as PpiRiskFactorsItem;

            return entity;
        }
        public PpiRiskFactorsItem FindByPrimaryKey(String registrationNo, String sequenceNo, DateTime dateOfInfection, String sRSignsOfInfection)
        {
            return base.FindByPrimaryKey(registrationNo, sequenceNo, dateOfInfection, sRSignsOfInfection) as PpiRiskFactorsItem;
        }

        #region IEnumerable< PpiRiskFactorsItem> Members

        IEnumerator<PpiRiskFactorsItem> IEnumerable<PpiRiskFactorsItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PpiRiskFactorsItem;
            }
        }

        #endregion

        private PpiRiskFactorsItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PpiRiskFactorsItem' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PpiRiskFactorsItem ({RegistrationNo, SequenceNo, DateOfInfection, SRSignsOfInfection})")]
    [Serializable]
    public partial class PpiRiskFactorsItem : esPpiRiskFactorsItem
    {
        public PpiRiskFactorsItem()
        {
        }

        public PpiRiskFactorsItem(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PpiRiskFactorsItemMetadata.Meta();
            }
        }

        override protected esPpiRiskFactorsItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PpiRiskFactorsItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PpiRiskFactorsItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PpiRiskFactorsItemQuery();
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
        public bool Load(PpiRiskFactorsItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PpiRiskFactorsItemQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PpiRiskFactorsItemQuery : esPpiRiskFactorsItemQuery
    {
        public PpiRiskFactorsItemQuery()
        {

        }

        public PpiRiskFactorsItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PpiRiskFactorsItemQuery";
        }
    }

    [Serializable]
    public partial class PpiRiskFactorsItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PpiRiskFactorsItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PpiRiskFactorsItemMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsItemMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsItemMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsItemMetadata.ColumnNames.DateOfInfection, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiRiskFactorsItemMetadata.PropertyNames.DateOfInfection;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsItemMetadata.ColumnNames.SRSignsOfInfection, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsItemMetadata.PropertyNames.SRSignsOfInfection;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsItemMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsItemMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PpiRiskFactorsItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PpiRiskFactorsItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PpiRiskFactorsItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PpiRiskFactorsItemMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string SequenceNo = "SequenceNo";
            public const string DateOfInfection = "DateOfInfection";
            public const string SRSignsOfInfection = "SRSignsOfInfection";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SequenceNo = "SequenceNo";
            public const string DateOfInfection = "DateOfInfection";
            public const string SRSignsOfInfection = "SRSignsOfInfection";
            public const string Notes = "Notes";
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
            lock (typeof(PpiRiskFactorsItemMetadata))
            {
                if (PpiRiskFactorsItemMetadata.mapDelegates == null)
                {
                    PpiRiskFactorsItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PpiRiskFactorsItemMetadata.meta == null)
                {
                    PpiRiskFactorsItemMetadata.meta = new PpiRiskFactorsItemMetadata();
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

                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DateOfInfection", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRSignsOfInfection", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PpiRiskFactorsItem";
                meta.Destination = "PpiRiskFactorsItem";
                meta.spInsert = "proc_PpiRiskFactorsItemInsert";
                meta.spUpdate = "proc_PpiRiskFactorsItemUpdate";
                meta.spDelete = "proc_PpiRiskFactorsItemDelete";
                meta.spLoadAll = "proc_PpiRiskFactorsItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_PpiRiskFactorsItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PpiRiskFactorsItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
