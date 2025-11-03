/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/25/2016 3:33:21 PM
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
    abstract public class esPCareKunjunganTindakanCollection : esEntityCollectionWAuditLog
    {
        public esPCareKunjunganTindakanCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PCareKunjunganTindakanCollection";
        }

        #region Query Logic
        protected void InitQuery(esPCareKunjunganTindakanQuery query)
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
            this.InitQuery(query as esPCareKunjunganTindakanQuery);
        }
        #endregion

        virtual public PCareKunjunganTindakan DetachEntity(PCareKunjunganTindakan entity)
        {
            return base.DetachEntity(entity) as PCareKunjunganTindakan;
        }

        virtual public PCareKunjunganTindakan AttachEntity(PCareKunjunganTindakan entity)
        {
            return base.AttachEntity(entity) as PCareKunjunganTindakan;
        }

        virtual public void Combine(PCareKunjunganTindakanCollection collection)
        {
            base.Combine(collection);
        }

        new public PCareKunjunganTindakan this[int index]
        {
            get
            {
                return base[index] as PCareKunjunganTindakan;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PCareKunjunganTindakan);
        }
    }

    [Serializable]
    abstract public class esPCareKunjunganTindakan : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPCareKunjunganTindakanQuery GetDynamicQuery()
        {
            return null;
        }

        public esPCareKunjunganTindakan()
        {
        }

        public esPCareKunjunganTindakan(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo)
        {
            esPCareKunjunganTindakanQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
            parms.Add("SequenceNo", sequenceNo);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "NoKunjungan": this.str.NoKunjungan = (string)value; break;
                        case "KdTindakanSK": this.str.KdTindakanSK = (string)value; break;
                        case "ErrorResponse": this.str.ErrorResponse = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "KdTindakanSK":

                            if (value == null || value is System.Int32)
                                this.KdTindakanSK = (System.Int32?)value;
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
        /// Maps to PCareKunjunganTindakan.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganTindakan.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganTindakan.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganTindakan.NoKunjungan
        /// </summary>
        virtual public System.String NoKunjungan
        {
            get
            {
                return base.GetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.NoKunjungan);
            }

            set
            {
                base.SetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.NoKunjungan, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganTindakan.KdTindakanSK
        /// </summary>
        virtual public System.Int32? KdTindakanSK
        {
            get
            {
                return base.GetSystemInt32(PCareKunjunganTindakanMetadata.ColumnNames.KdTindakanSK);
            }

            set
            {
                base.SetSystemInt32(PCareKunjunganTindakanMetadata.ColumnNames.KdTindakanSK, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganTindakan.ErrorResponse
        /// </summary>
        virtual public System.String ErrorResponse
        {
            get
            {
                return base.GetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.ErrorResponse);
            }

            set
            {
                base.SetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.ErrorResponse, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganTindakan.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PCareKunjunganTindakanMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PCareKunjunganTindakanMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganTindakan.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PCareKunjunganTindakanMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPCareKunjunganTindakan entity)
            {
                this.entity = entity;
            }
            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
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
            public System.String NoKunjungan
            {
                get
                {
                    System.String data = entity.NoKunjungan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoKunjungan = null;
                    else entity.NoKunjungan = Convert.ToString(value);
                }
            }
            public System.String KdTindakanSK
            {
                get
                {
                    System.Int32? data = entity.KdTindakanSK;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KdTindakanSK = null;
                    else entity.KdTindakanSK = Convert.ToInt32(value);
                }
            }
            public System.String ErrorResponse
            {
                get
                {
                    System.String data = entity.ErrorResponse;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ErrorResponse = null;
                    else entity.ErrorResponse = Convert.ToString(value);
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
            private esPCareKunjunganTindakan entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPCareKunjunganTindakanQuery query)
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
                throw new Exception("esPCareKunjunganTindakan can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PCareKunjunganTindakan : esPCareKunjunganTindakan
    {
    }

    [Serializable]
    abstract public class esPCareKunjunganTindakanQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PCareKunjunganTindakanMetadata.Meta();
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganTindakanMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganTindakanMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganTindakanMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem NoKunjungan
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganTindakanMetadata.ColumnNames.NoKunjungan, esSystemType.String);
            }
        }

        public esQueryItem KdTindakanSK
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganTindakanMetadata.ColumnNames.KdTindakanSK, esSystemType.Int32);
            }
        }

        public esQueryItem ErrorResponse
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganTindakanMetadata.ColumnNames.ErrorResponse, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganTindakanMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganTindakanMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PCareKunjunganTindakanCollection")]
    public partial class PCareKunjunganTindakanCollection : esPCareKunjunganTindakanCollection, IEnumerable<PCareKunjunganTindakan>
    {
        public PCareKunjunganTindakanCollection()
        {

        }

        public static implicit operator List<PCareKunjunganTindakan>(PCareKunjunganTindakanCollection coll)
        {
            List<PCareKunjunganTindakan> list = new List<PCareKunjunganTindakan>();

            foreach (PCareKunjunganTindakan emp in coll)
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
                return PCareKunjunganTindakanMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareKunjunganTindakanQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PCareKunjunganTindakan(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PCareKunjunganTindakan();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PCareKunjunganTindakanQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareKunjunganTindakanQuery();
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
        public bool Load(PCareKunjunganTindakanQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PCareKunjunganTindakan AddNew()
        {
            PCareKunjunganTindakan entity = base.AddNewEntity() as PCareKunjunganTindakan;

            return entity;
        }
        public PCareKunjunganTindakan FindByPrimaryKey(String transactionNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(transactionNo, sequenceNo) as PCareKunjunganTindakan;
        }

        #region IEnumerable< PCareKunjunganTindakan> Members

        IEnumerator<PCareKunjunganTindakan> IEnumerable<PCareKunjunganTindakan>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PCareKunjunganTindakan;
            }
        }

        #endregion

        private PCareKunjunganTindakanQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PCareKunjunganTindakan' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PCareKunjunganTindakan ({TransactionNo, SequenceNo})")]
    [Serializable]
    public partial class PCareKunjunganTindakan : esPCareKunjunganTindakan
    {
        public PCareKunjunganTindakan()
        {
        }

        public PCareKunjunganTindakan(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PCareKunjunganTindakanMetadata.Meta();
            }
        }

        override protected esPCareKunjunganTindakanQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareKunjunganTindakanQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PCareKunjunganTindakanQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareKunjunganTindakanQuery();
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
        public bool Load(PCareKunjunganTindakanQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PCareKunjunganTindakanQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PCareKunjunganTindakanQuery : esPCareKunjunganTindakanQuery
    {
        public PCareKunjunganTindakanQuery()
        {

        }

        public PCareKunjunganTindakanQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PCareKunjunganTindakanQuery";
        }
    }

    [Serializable]
    public partial class PCareKunjunganTindakanMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PCareKunjunganTindakanMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PCareKunjunganTindakanMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganTindakanMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganTindakanMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganTindakanMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganTindakanMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganTindakanMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganTindakanMetadata.ColumnNames.NoKunjungan, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganTindakanMetadata.PropertyNames.NoKunjungan;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganTindakanMetadata.ColumnNames.KdTindakanSK, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PCareKunjunganTindakanMetadata.PropertyNames.KdTindakanSK;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganTindakanMetadata.ColumnNames.ErrorResponse, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganTindakanMetadata.PropertyNames.ErrorResponse;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganTindakanMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PCareKunjunganTindakanMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganTindakanMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganTindakanMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PCareKunjunganTindakanMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string NoKunjungan = "NoKunjungan";
            public const string KdTindakanSK = "KdTindakanSK";
            public const string ErrorResponse = "ErrorResponse";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string SequenceNo = "SequenceNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string NoKunjungan = "NoKunjungan";
            public const string KdTindakanSK = "KdTindakanSK";
            public const string ErrorResponse = "ErrorResponse";
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
            lock (typeof(PCareKunjunganTindakanMetadata))
            {
                if (PCareKunjunganTindakanMetadata.mapDelegates == null)
                {
                    PCareKunjunganTindakanMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PCareKunjunganTindakanMetadata.meta == null)
                {
                    PCareKunjunganTindakanMetadata.meta = new PCareKunjunganTindakanMetadata();
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

                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoKunjungan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KdTindakanSK", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ErrorResponse", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PCareKunjunganTindakan";
                meta.Destination = "PCareKunjunganTindakan";
                meta.spInsert = "proc_PCareKunjunganTindakanInsert";
                meta.spUpdate = "proc_PCareKunjunganTindakanUpdate";
                meta.spDelete = "proc_PCareKunjunganTindakanDelete";
                meta.spLoadAll = "proc_PCareKunjunganTindakanLoadAll";
                meta.spLoadByPrimaryKey = "proc_PCareKunjunganTindakanLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PCareKunjunganTindakanMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
