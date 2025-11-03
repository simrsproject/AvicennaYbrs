/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 02/18/19 7:17:13 PM
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
    abstract public class esPCareKunjunganCollection : esEntityCollectionWAuditLog
    {
        public esPCareKunjunganCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PCareKunjunganCollection";
        }

        #region Query Logic
        protected void InitQuery(esPCareKunjunganQuery query)
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
            this.InitQuery(query as esPCareKunjunganQuery);
        }
        #endregion

        virtual public PCareKunjungan DetachEntity(PCareKunjungan entity)
        {
            return base.DetachEntity(entity) as PCareKunjungan;
        }

        virtual public PCareKunjungan AttachEntity(PCareKunjungan entity)
        {
            return base.AttachEntity(entity) as PCareKunjungan;
        }

        virtual public void Combine(PCareKunjunganCollection collection)
        {
            base.Combine(collection);
        }

        new public PCareKunjungan this[int index]
        {
            get
            {
                return base[index] as PCareKunjungan;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PCareKunjungan);
        }
    }

    [Serializable]
    abstract public class esPCareKunjungan : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPCareKunjunganQuery GetDynamicQuery()
        {
            return null;
        }

        public esPCareKunjungan()
        {
        }

        public esPCareKunjungan(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo)
        {
            esPCareKunjunganQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "NoKartu": this.str.NoKartu = (string)value; break;
                        case "NoUrutPendaftaran": this.str.NoUrutPendaftaran = (string)value; break;
                        case "NoKunjungan": this.str.NoKunjungan = (string)value; break;
                        case "PendaftaranPostData": this.str.PendaftaranPostData = (string)value; break;
                        case "KunjunganPostData": this.str.KunjunganPostData = (string)value; break;
                        case "ErrorResponse": this.str.ErrorResponse = (string)value; break;
                        case "IsAllTindakanPosted": this.str.IsAllTindakanPosted = (string)value; break;
                        case "IsAllObatPosted": this.str.IsAllObatPosted = (string)value; break;
                        case "IsClosed": this.str.IsClosed = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "KdPoli": this.str.KdPoli = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsAllTindakanPosted":

                            if (value == null || value is System.Boolean)
                                this.IsAllTindakanPosted = (System.Boolean?)value;
                            break;
                        case "IsAllObatPosted":

                            if (value == null || value is System.Boolean)
                                this.IsAllObatPosted = (System.Boolean?)value;
                            break;
                        case "IsClosed":

                            if (value == null || value is System.Boolean)
                                this.IsClosed = (System.Boolean?)value;
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
        /// Maps to PCareKunjungan.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.NoKartu
        /// </summary>
        virtual public System.String NoKartu
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.NoKartu);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.NoKartu, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.NoUrutPendaftaran
        /// </summary>
        virtual public System.String NoUrutPendaftaran
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.NoUrutPendaftaran);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.NoUrutPendaftaran, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.NoKunjungan
        /// </summary>
        virtual public System.String NoKunjungan
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.NoKunjungan);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.NoKunjungan, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.PendaftaranPostData
        /// </summary>
        virtual public System.String PendaftaranPostData
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.PendaftaranPostData);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.PendaftaranPostData, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.KunjunganPostData
        /// </summary>
        virtual public System.String KunjunganPostData
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.KunjunganPostData);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.KunjunganPostData, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.ErrorResponse
        /// </summary>
        virtual public System.String ErrorResponse
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.ErrorResponse);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.ErrorResponse, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.IsAllTindakanPosted
        /// </summary>
        virtual public System.Boolean? IsAllTindakanPosted
        {
            get
            {
                return base.GetSystemBoolean(PCareKunjunganMetadata.ColumnNames.IsAllTindakanPosted);
            }

            set
            {
                base.SetSystemBoolean(PCareKunjunganMetadata.ColumnNames.IsAllTindakanPosted, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.IsAllObatPosted
        /// </summary>
        virtual public System.Boolean? IsAllObatPosted
        {
            get
            {
                return base.GetSystemBoolean(PCareKunjunganMetadata.ColumnNames.IsAllObatPosted);
            }

            set
            {
                base.SetSystemBoolean(PCareKunjunganMetadata.ColumnNames.IsAllObatPosted, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.IsClosed
        /// </summary>
        virtual public System.Boolean? IsClosed
        {
            get
            {
                return base.GetSystemBoolean(PCareKunjunganMetadata.ColumnNames.IsClosed);
            }

            set
            {
                base.SetSystemBoolean(PCareKunjunganMetadata.ColumnNames.IsClosed, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PCareKunjunganMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PCareKunjunganMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PCareKunjungan.KdPoli
        /// </summary>
        virtual public System.String KdPoli
        {
            get
            {
                return base.GetSystemString(PCareKunjunganMetadata.ColumnNames.KdPoli);
            }

            set
            {
                base.SetSystemString(PCareKunjunganMetadata.ColumnNames.KdPoli, value);
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
            public esStrings(esPCareKunjungan entity)
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
            public System.String NoKartu
            {
                get
                {
                    System.String data = entity.NoKartu;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoKartu = null;
                    else entity.NoKartu = Convert.ToString(value);
                }
            }
            public System.String NoUrutPendaftaran
            {
                get
                {
                    System.String data = entity.NoUrutPendaftaran;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoUrutPendaftaran = null;
                    else entity.NoUrutPendaftaran = Convert.ToString(value);
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
            public System.String PendaftaranPostData
            {
                get
                {
                    System.String data = entity.PendaftaranPostData;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PendaftaranPostData = null;
                    else entity.PendaftaranPostData = Convert.ToString(value);
                }
            }
            public System.String KunjunganPostData
            {
                get
                {
                    System.String data = entity.KunjunganPostData;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KunjunganPostData = null;
                    else entity.KunjunganPostData = Convert.ToString(value);
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
            public System.String IsAllTindakanPosted
            {
                get
                {
                    System.Boolean? data = entity.IsAllTindakanPosted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllTindakanPosted = null;
                    else entity.IsAllTindakanPosted = Convert.ToBoolean(value);
                }
            }
            public System.String IsAllObatPosted
            {
                get
                {
                    System.Boolean? data = entity.IsAllObatPosted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllObatPosted = null;
                    else entity.IsAllObatPosted = Convert.ToBoolean(value);
                }
            }
            public System.String IsClosed
            {
                get
                {
                    System.Boolean? data = entity.IsClosed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsClosed = null;
                    else entity.IsClosed = Convert.ToBoolean(value);
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
            public System.String KdPoli
            {
                get
                {
                    System.String data = entity.KdPoli;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KdPoli = null;
                    else entity.KdPoli = Convert.ToString(value);
                }
            }
            private esPCareKunjungan entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPCareKunjunganQuery query)
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
                throw new Exception("esPCareKunjungan can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PCareKunjungan : esPCareKunjungan
    {
    }

    [Serializable]
    abstract public class esPCareKunjunganQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PCareKunjunganMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem NoKartu
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.NoKartu, esSystemType.String);
            }
        }

        public esQueryItem NoUrutPendaftaran
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.NoUrutPendaftaran, esSystemType.String);
            }
        }

        public esQueryItem NoKunjungan
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.NoKunjungan, esSystemType.String);
            }
        }

        public esQueryItem PendaftaranPostData
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.PendaftaranPostData, esSystemType.String);
            }
        }

        public esQueryItem KunjunganPostData
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.KunjunganPostData, esSystemType.String);
            }
        }

        public esQueryItem ErrorResponse
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.ErrorResponse, esSystemType.String);
            }
        }

        public esQueryItem IsAllTindakanPosted
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.IsAllTindakanPosted, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllObatPosted
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.IsAllObatPosted, esSystemType.Boolean);
            }
        }

        public esQueryItem IsClosed
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem KdPoli
        {
            get
            {
                return new esQueryItem(this, PCareKunjunganMetadata.ColumnNames.KdPoli, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PCareKunjunganCollection")]
    public partial class PCareKunjunganCollection : esPCareKunjunganCollection, IEnumerable<PCareKunjungan>
    {
        public PCareKunjunganCollection()
        {

        }

        public static implicit operator List<PCareKunjungan>(PCareKunjunganCollection coll)
        {
            List<PCareKunjungan> list = new List<PCareKunjungan>();

            foreach (PCareKunjungan emp in coll)
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
                return PCareKunjunganMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareKunjunganQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PCareKunjungan(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PCareKunjungan();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PCareKunjunganQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareKunjunganQuery();
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
        public bool Load(PCareKunjunganQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PCareKunjungan AddNew()
        {
            PCareKunjungan entity = base.AddNewEntity() as PCareKunjungan;

            return entity;
        }
        public PCareKunjungan FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as PCareKunjungan;
        }

        #region IEnumerable< PCareKunjungan> Members

        IEnumerator<PCareKunjungan> IEnumerable<PCareKunjungan>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PCareKunjungan;
            }
        }

        #endregion

        private PCareKunjunganQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PCareKunjungan' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PCareKunjungan ({RegistrationNo})")]
    [Serializable]
    public partial class PCareKunjungan : esPCareKunjungan
    {
        public PCareKunjungan()
        {
        }

        public PCareKunjungan(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PCareKunjunganMetadata.Meta();
            }
        }

        override protected esPCareKunjunganQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PCareKunjunganQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PCareKunjunganQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PCareKunjunganQuery();
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
        public bool Load(PCareKunjunganQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PCareKunjunganQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PCareKunjunganQuery : esPCareKunjunganQuery
    {
        public PCareKunjunganQuery()
        {

        }

        public PCareKunjunganQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PCareKunjunganQuery";
        }
    }

    [Serializable]
    public partial class PCareKunjunganMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PCareKunjunganMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.NoKartu, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.NoKartu;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.NoUrutPendaftaran, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.NoUrutPendaftaran;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.NoKunjungan, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.NoKunjungan;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.PendaftaranPostData, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.PendaftaranPostData;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.KunjunganPostData, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.KunjunganPostData;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.ErrorResponse, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.ErrorResponse;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.IsAllTindakanPosted, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.IsAllTindakanPosted;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.IsAllObatPosted, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.IsAllObatPosted;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.IsClosed, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.IsClosed;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PCareKunjunganMetadata.ColumnNames.KdPoli, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PCareKunjunganMetadata.PropertyNames.KdPoli;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PCareKunjunganMetadata Meta()
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
            public const string NoKartu = "NoKartu";
            public const string NoUrutPendaftaran = "NoUrutPendaftaran";
            public const string NoKunjungan = "NoKunjungan";
            public const string PendaftaranPostData = "PendaftaranPostData";
            public const string KunjunganPostData = "KunjunganPostData";
            public const string ErrorResponse = "ErrorResponse";
            public const string IsAllTindakanPosted = "IsAllTindakanPosted";
            public const string IsAllObatPosted = "IsAllObatPosted";
            public const string IsClosed = "IsClosed";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string KdPoli = "KdPoli";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string NoKartu = "NoKartu";
            public const string NoUrutPendaftaran = "NoUrutPendaftaran";
            public const string NoKunjungan = "NoKunjungan";
            public const string PendaftaranPostData = "PendaftaranPostData";
            public const string KunjunganPostData = "KunjunganPostData";
            public const string ErrorResponse = "ErrorResponse";
            public const string IsAllTindakanPosted = "IsAllTindakanPosted";
            public const string IsAllObatPosted = "IsAllObatPosted";
            public const string IsClosed = "IsClosed";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string KdPoli = "KdPoli";
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
            lock (typeof(PCareKunjunganMetadata))
            {
                if (PCareKunjunganMetadata.mapDelegates == null)
                {
                    PCareKunjunganMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PCareKunjunganMetadata.meta == null)
                {
                    PCareKunjunganMetadata.meta = new PCareKunjunganMetadata();
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
                meta.AddTypeMap("NoKartu", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoUrutPendaftaran", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoKunjungan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PendaftaranPostData", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KunjunganPostData", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ErrorResponse", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAllTindakanPosted", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllObatPosted", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KdPoli", new esTypeMap("varchar", "System.String"));


                meta.Source = "PCareKunjungan";
                meta.Destination = "PCareKunjungan";
                meta.spInsert = "proc_PCareKunjunganInsert";
                meta.spUpdate = "proc_PCareKunjunganUpdate";
                meta.spDelete = "proc_PCareKunjunganDelete";
                meta.spLoadAll = "proc_PCareKunjunganLoadAll";
                meta.spLoadByPrimaryKey = "proc_PCareKunjunganLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PCareKunjunganMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
