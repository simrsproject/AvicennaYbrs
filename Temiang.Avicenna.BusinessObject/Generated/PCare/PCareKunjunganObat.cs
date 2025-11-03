/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/28/2016 11:41:51 PM
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
    abstract public class esPCareKunjunganObatCollection : esEntityCollectionWAuditLog
    {
        public esPCareKunjunganObatCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PCareKunjunganObatCollection";
        }

        #region Query Logic
        protected void InitQuery(esPCareKunjunganObatQuery query)
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
            this.InitQuery(query as esPCareKunjunganObatQuery);
        }
        #endregion

        virtual public PCareKunjunganObat DetachEntity(PCareKunjunganObat entity)
        {
            return base.DetachEntity(entity) as PCareKunjunganObat;
        }

        virtual public PCareKunjunganObat AttachEntity(PCareKunjunganObat entity)
        {
            return base.AttachEntity(entity) as PCareKunjunganObat;
        }

        virtual public void Combine(PCareKunjunganObatCollection collection)
        {
            base.Combine(collection);
        }

        new public PCareKunjunganObat this[int index]
        {
            get
            {
                return base[index] as PCareKunjunganObat;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PCareKunjunganObat);
        }
    }

    [Serializable]
    abstract public class esPCareKunjunganObat : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPCareKunjunganObatQuery GetDynamicQuery()
        {
            return null;
        }

        public esPCareKunjunganObat()
        {
        }

        public esPCareKunjunganObat(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String prescriptionNo, String sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String prescriptionNo, String sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String prescriptionNo, String sequenceNo)
        {
            esPCareKunjunganObatQuery query = this.GetDynamicQuery();
            query.Where(query.PrescriptionNo == prescriptionNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String prescriptionNo, String sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PrescriptionNo", prescriptionNo);
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
                        case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "NoKunjungan": this.str.NoKunjungan = (string)value; break;
                        case "KdObatSK": this.str.KdObatSK = (string)value; break;
                        case "KdRacikan": this.str.KdRacikan = (string)value; break;
                        case "ErrorResponse": this.str.ErrorResponse = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "KdObatSK":

                            if (value == null || value is System.Int32)
                                this.KdObatSK = (System.Int32?)value;
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
        /// Maps to PCareKunjunganObat.PrescriptionNo
        /// </summary>
        virtual public System.String PrescriptionNo
        {
            get
            {
                return base.GetSystemString(PCareKunjunganObatMetadata.ColumnNames.PrescriptionNo);
            }

            set
            {
                base.SetSystemString(PCareKunjunganObatMetadata.ColumnNames.PrescriptionNo, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganObat.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(PCareKunjunganObatMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(PCareKunjunganObatMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganObat.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PCareKunjunganObatMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PCareKunjunganObatMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganObat.NoKunjungan
        /// </summary>
        virtual public System.String NoKunjungan
        {
            get
            {
                return base.GetSystemString(PCareKunjunganObatMetadata.ColumnNames.NoKunjungan);
            }

            set
            {
                base.SetSystemString(PCareKunjunganObatMetadata.ColumnNames.NoKunjungan, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganObat.KdObatSK
        /// </summary>
        virtual public System.Int32? KdObatSK
        {
            get
            {
                return base.GetSystemInt32(PCareKunjunganObatMetadata.ColumnNames.KdObatSK);
            }

            set
            {
                base.SetSystemInt32(PCareKunjunganObatMetadata.ColumnNames.KdObatSK, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganObat.KdRacikan
        /// </summary>
        virtual public System.String KdRacikan
        {
            get
            {
                return base.GetSystemString(PCareKunjunganObatMetadata.ColumnNames.KdRacikan);
            }

            set
            {
                base.SetSystemString(PCareKunjunganObatMetadata.ColumnNames.KdRacikan, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganObat.ErrorResponse
        /// </summary>
        virtual public System.String ErrorResponse
        {
            get
            {
                return base.GetSystemString(PCareKunjunganObatMetadata.ColumnNames.ErrorResponse);
            }

            set
            {
                base.SetSystemString(PCareKunjunganObatMetadata.ColumnNames.ErrorResponse, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganObat.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PCareKunjunganObatMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PCareKunjunganObatMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjunganObat.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PCareKunjunganObatMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PCareKunjunganObatMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPCareKunjunganObat entity)
            {
                this.entity = entity;
            }
            public System.String PrescriptionNo
            {
                get
                {
                    System.String data = entity.PrescriptionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionNo = null;
                    else entity.PrescriptionNo = Convert.ToString(value);
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
            public System.String KdObatSK
            {
                get
                {
                    System.Int32? data = entity.KdObatSK;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KdObatSK = null;
                    else entity.KdObatSK = Convert.ToInt32(value);
                }
            }
            public System.String KdRacikan
            {
                get
                {
                    System.String data = entity.KdRacikan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KdRacikan = null;
                    else entity.KdRacikan = Convert.ToString(value);
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
            private esPCareKunjunganObat entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPCareKunjunganObatQuery query)
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
                throw new Exception("esPCareKunjunganObat can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PCareKunjunganObat : esPCareKunjunganObat
    {
    }

    [Serializable]
    abstract public class esPCareKunjunganObatQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PCareKunjunganObatMetadata.Meta();
            }
        }

        public esQueryItem PrescriptionNo
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem NoKunjungan
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.NoKunjungan, esSystemType.String);
            }
        }

        public esQueryItem KdObatSK
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.KdObatSK, esSystemType.Int32);
            }
        }

        public esQueryItem KdRacikan
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.KdRacikan, esSystemType.String);
            }
        }

        public esQueryItem ErrorResponse
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.ErrorResponse, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganObatMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PCareKunjunganObatCollection")]
    public partial class PCareKunjunganObatCollection : esPCareKunjunganObatCollection, IEnumerable<PCareKunjunganObat>
    {
        public PCareKunjunganObatCollection()
        {

        }

        public static implicit operator List<PCareKunjunganObat>(PCareKunjunganObatCollection coll)
        {
            List<PCareKunjunganObat> list = new List<PCareKunjunganObat>();

            foreach (PCareKunjunganObat emp in coll)
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
                return PCareKunjunganObatMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareKunjunganObatQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PCareKunjunganObat(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PCareKunjunganObat();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PCareKunjunganObatQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareKunjunganObatQuery();
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
        public bool Load(PCareKunjunganObatQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PCareKunjunganObat AddNew()
        {
            PCareKunjunganObat entity = base.AddNewEntity() as PCareKunjunganObat;

            return entity;
        }
        public PCareKunjunganObat FindByPrimaryKey(String prescriptionNo, String sequenceNo)
        {
            return base.FindByPrimaryKey(prescriptionNo, sequenceNo) as PCareKunjunganObat;
        }

        #region IEnumerable< PCareKunjunganObat> Members

        IEnumerator<PCareKunjunganObat> IEnumerable<PCareKunjunganObat>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PCareKunjunganObat;
            }
        }

        #endregion

        private PCareKunjunganObatQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PCareKunjunganObat' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PCareKunjunganObat ({PrescriptionNo, SequenceNo})")]
    [Serializable]
    public partial class PCareKunjunganObat : esPCareKunjunganObat
    {
        public PCareKunjunganObat()
        {
        }

        public PCareKunjunganObat(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PCareKunjunganObatMetadata.Meta();
            }
        }

        override protected esPCareKunjunganObatQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareKunjunganObatQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PCareKunjunganObatQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareKunjunganObatQuery();
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
        public bool Load(PCareKunjunganObatQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PCareKunjunganObatQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PCareKunjunganObatQuery : esPCareKunjunganObatQuery
    {
        public PCareKunjunganObatQuery()
        {

        }

        public PCareKunjunganObatQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PCareKunjunganObatQuery";
        }
    }

    [Serializable]
    public partial class PCareKunjunganObatMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PCareKunjunganObatMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.PrescriptionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.PrescriptionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.NoKunjungan, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.NoKunjungan;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.KdObatSK, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.KdObatSK;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.KdRacikan, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.KdRacikan;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.ErrorResponse, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.ErrorResponse;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganObatMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganObatMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PCareKunjunganObatMetadata Meta()
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
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SequenceNo = "SequenceNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string NoKunjungan = "NoKunjungan";
            public const string KdObatSK = "KdObatSK";
            public const string KdRacikan = "KdRacikan";
            public const string ErrorResponse = "ErrorResponse";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SequenceNo = "SequenceNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string NoKunjungan = "NoKunjungan";
            public const string KdObatSK = "KdObatSK";
            public const string KdRacikan = "KdRacikan";
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
            lock (typeof(PCareKunjunganObatMetadata))
            {
                if (PCareKunjunganObatMetadata.mapDelegates == null)
                {
                    PCareKunjunganObatMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PCareKunjunganObatMetadata.meta == null)
                {
                    PCareKunjunganObatMetadata.meta = new PCareKunjunganObatMetadata();
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

                meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoKunjungan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KdObatSK", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KdRacikan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ErrorResponse", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PCareKunjunganObat";
                meta.Destination = "PCareKunjunganObat";
                meta.spInsert = "proc_PCareKunjunganObatInsert";
                meta.spUpdate = "proc_PCareKunjunganObatUpdate";
                meta.spDelete = "proc_PCareKunjunganObatDelete";
                meta.spLoadAll = "proc_PCareKunjunganObatLoadAll";
                meta.spLoadByPrimaryKey = "proc_PCareKunjunganObatLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PCareKunjunganObatMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
