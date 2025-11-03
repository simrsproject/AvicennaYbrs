/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/2/2019 2:29:40 PM
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
    abstract public class esImplantInstallationCollection : esEntityCollectionWAuditLog
    {
        public esImplantInstallationCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ImplantInstallationCollection";
        }

        #region Query Logic
        protected void InitQuery(esImplantInstallationQuery query)
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
            this.InitQuery(query as esImplantInstallationQuery);
        }
        #endregion

        virtual public ImplantInstallation DetachEntity(ImplantInstallation entity)
        {
            return base.DetachEntity(entity) as ImplantInstallation;
        }

        virtual public ImplantInstallation AttachEntity(ImplantInstallation entity)
        {
            return base.AttachEntity(entity) as ImplantInstallation;
        }

        virtual public void Combine(ImplantInstallationCollection collection)
        {
            base.Combine(collection);
        }

        new public ImplantInstallation this[int index]
        {
            get
            {
                return base[index] as ImplantInstallation;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ImplantInstallation);
        }
    }

    [Serializable]
    abstract public class esImplantInstallation : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esImplantInstallationQuery GetDynamicQuery()
        {
            return null;
        }

        public esImplantInstallation()
        {
        }

        public esImplantInstallation(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String bookingNo, String seqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bookingNo, seqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(bookingNo, seqNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String bookingNo, String seqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(bookingNo, seqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(bookingNo, seqNo);
        }

        private bool LoadByPrimaryKeyDynamic(String bookingNo, String seqNo)
        {
            esImplantInstallationQuery query = this.GetDynamicQuery();
            query.Where(query.BookingNo == bookingNo, query.SeqNo == seqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String bookingNo, String seqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("BookingNo", bookingNo);
            parms.Add("SeqNo", seqNo);
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
                        case "BookingNo": this.str.BookingNo = (string)value; break;
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "ImplantType": this.str.ImplantType = (string)value; break;
                        case "SerialNo": this.str.SerialNo = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "PlacementSite": this.str.PlacementSite = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Qty":

                            if (value == null || value is System.Int16)
                                this.Qty = (System.Int16?)value;
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
        /// Maps to ImplantInstallation.BookingNo
        /// </summary>
        virtual public System.String BookingNo
        {
            get
            {
                return base.GetSystemString(ImplantInstallationMetadata.ColumnNames.BookingNo);
            }

            set
            {
                base.SetSystemString(ImplantInstallationMetadata.ColumnNames.BookingNo, value);
            }
        }
        /// <summary>
        /// Maps to ImplantInstallation.SeqNo
        /// </summary>
        virtual public System.String SeqNo
        {
            get
            {
                return base.GetSystemString(ImplantInstallationMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemString(ImplantInstallationMetadata.ColumnNames.SeqNo, value);
            }
        }
        /// <summary>
        /// Maps to ImplantInstallation.ImplantType
        /// </summary>
        virtual public System.String ImplantType
        {
            get
            {
                return base.GetSystemString(ImplantInstallationMetadata.ColumnNames.ImplantType);
            }

            set
            {
                base.SetSystemString(ImplantInstallationMetadata.ColumnNames.ImplantType, value);
            }
        }
        /// <summary>
        /// Maps to ImplantInstallation.SerialNo
        /// </summary>
        virtual public System.String SerialNo
        {
            get
            {
                return base.GetSystemString(ImplantInstallationMetadata.ColumnNames.SerialNo);
            }

            set
            {
                base.SetSystemString(ImplantInstallationMetadata.ColumnNames.SerialNo, value);
            }
        }
        /// <summary>
        /// Maps to ImplantInstallation.Qty
        /// </summary>
        virtual public System.Int16? Qty
        {
            get
            {
                return base.GetSystemInt16(ImplantInstallationMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemInt16(ImplantInstallationMetadata.ColumnNames.Qty, value);
            }
        }
        /// <summary>
        /// Maps to ImplantInstallation.PlacementSite
        /// </summary>
        virtual public System.String PlacementSite
        {
            get
            {
                return base.GetSystemString(ImplantInstallationMetadata.ColumnNames.PlacementSite);
            }

            set
            {
                base.SetSystemString(ImplantInstallationMetadata.ColumnNames.PlacementSite, value);
            }
        }
        /// <summary>
        /// Maps to ImplantInstallation.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ImplantInstallationMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ImplantInstallationMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ImplantInstallation.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ImplantInstallationMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ImplantInstallationMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esImplantInstallation entity)
            {
                this.entity = entity;
            }
            public System.String BookingNo
            {
                get
                {
                    System.String data = entity.BookingNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BookingNo = null;
                    else entity.BookingNo = Convert.ToString(value);
                }
            }
            public System.String SeqNo
            {
                get
                {
                    System.String data = entity.SeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeqNo = null;
                    else entity.SeqNo = Convert.ToString(value);
                }
            }
            public System.String ImplantType
            {
                get
                {
                    System.String data = entity.ImplantType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImplantType = null;
                    else entity.ImplantType = Convert.ToString(value);
                }
            }
            public System.String SerialNo
            {
                get
                {
                    System.String data = entity.SerialNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SerialNo = null;
                    else entity.SerialNo = Convert.ToString(value);
                }
            }
            public System.String Qty
            {
                get
                {
                    System.Int16? data = entity.Qty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Qty = null;
                    else entity.Qty = Convert.ToInt16(value);
                }
            }
            public System.String PlacementSite
            {
                get
                {
                    System.String data = entity.PlacementSite;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PlacementSite = null;
                    else entity.PlacementSite = Convert.ToString(value);
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
            private esImplantInstallation entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esImplantInstallationQuery query)
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
                throw new Exception("esImplantInstallation can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ImplantInstallation : esImplantInstallation
    {
    }

    [Serializable]
    abstract public class esImplantInstallationQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ImplantInstallationMetadata.Meta();
            }
        }

        public esQueryItem BookingNo
        {
            get
            {
                return new esQueryItem(this, ImplantInstallationMetadata.ColumnNames.BookingNo, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, ImplantInstallationMetadata.ColumnNames.SeqNo, esSystemType.String);
            }
        }

        public esQueryItem ImplantType
        {
            get
            {
                return new esQueryItem(this, ImplantInstallationMetadata.ColumnNames.ImplantType, esSystemType.String);
            }
        }

        public esQueryItem SerialNo
        {
            get
            {
                return new esQueryItem(this, ImplantInstallationMetadata.ColumnNames.SerialNo, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, ImplantInstallationMetadata.ColumnNames.Qty, esSystemType.Int16);
            }
        }

        public esQueryItem PlacementSite
        {
            get
            {
                return new esQueryItem(this, ImplantInstallationMetadata.ColumnNames.PlacementSite, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ImplantInstallationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ImplantInstallationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ImplantInstallationCollection")]
    public partial class ImplantInstallationCollection : esImplantInstallationCollection, IEnumerable<ImplantInstallation>
    {
        public ImplantInstallationCollection()
        {

        }

        public static implicit operator List<ImplantInstallation>(ImplantInstallationCollection coll)
        {
            List<ImplantInstallation> list = new List<ImplantInstallation>();

            foreach (ImplantInstallation emp in coll)
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
                return ImplantInstallationMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ImplantInstallationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ImplantInstallation(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ImplantInstallation();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ImplantInstallationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ImplantInstallationQuery();
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
        public bool Load(ImplantInstallationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ImplantInstallation AddNew()
        {
            ImplantInstallation entity = base.AddNewEntity() as ImplantInstallation;

            return entity;
        }
        public ImplantInstallation FindByPrimaryKey(String bookingNo, String seqNo)
        {
            return base.FindByPrimaryKey(bookingNo, seqNo) as ImplantInstallation;
        }

        #region IEnumerable< ImplantInstallation> Members

        IEnumerator<ImplantInstallation> IEnumerable<ImplantInstallation>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ImplantInstallation;
            }
        }

        #endregion

        private ImplantInstallationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ImplantInstallation' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ImplantInstallation ({BookingNo, SeqNo})")]
    [Serializable]
    public partial class ImplantInstallation : esImplantInstallation
    {
        public ImplantInstallation()
        {
        }

        public ImplantInstallation(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ImplantInstallationMetadata.Meta();
            }
        }

        override protected esImplantInstallationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ImplantInstallationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ImplantInstallationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ImplantInstallationQuery();
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
        public bool Load(ImplantInstallationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ImplantInstallationQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ImplantInstallationQuery : esImplantInstallationQuery
    {
        public ImplantInstallationQuery()
        {

        }

        public ImplantInstallationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ImplantInstallationQuery";
        }
    }

    [Serializable]
    public partial class ImplantInstallationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ImplantInstallationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ImplantInstallationMetadata.ColumnNames.BookingNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ImplantInstallationMetadata.PropertyNames.BookingNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ImplantInstallationMetadata.ColumnNames.SeqNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ImplantInstallationMetadata.PropertyNames.SeqNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(ImplantInstallationMetadata.ColumnNames.ImplantType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ImplantInstallationMetadata.PropertyNames.ImplantType;
            c.CharacterMaxLength = 200;
            _columns.Add(c);

            c = new esColumnMetadata(ImplantInstallationMetadata.ColumnNames.SerialNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ImplantInstallationMetadata.PropertyNames.SerialNo;
            c.CharacterMaxLength = 100;
            _columns.Add(c);

            c = new esColumnMetadata(ImplantInstallationMetadata.ColumnNames.Qty, 4, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = ImplantInstallationMetadata.PropertyNames.Qty;
            c.NumericPrecision = 5;
            _columns.Add(c);

            c = new esColumnMetadata(ImplantInstallationMetadata.ColumnNames.PlacementSite, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ImplantInstallationMetadata.PropertyNames.PlacementSite;
            c.CharacterMaxLength = 200;
            _columns.Add(c);

            c = new esColumnMetadata(ImplantInstallationMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ImplantInstallationMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImplantInstallationMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ImplantInstallationMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ImplantInstallationMetadata Meta()
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
            public const string BookingNo = "BookingNo";
            public const string SeqNo = "SeqNo";
            public const string ImplantType = "ImplantType";
            public const string SerialNo = "SerialNo";
            public const string Qty = "Qty";
            public const string PlacementSite = "PlacementSite";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string BookingNo = "BookingNo";
            public const string SeqNo = "SeqNo";
            public const string ImplantType = "ImplantType";
            public const string SerialNo = "SerialNo";
            public const string Qty = "Qty";
            public const string PlacementSite = "PlacementSite";
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
            lock (typeof(ImplantInstallationMetadata))
            {
                if (ImplantInstallationMetadata.mapDelegates == null)
                {
                    ImplantInstallationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ImplantInstallationMetadata.meta == null)
                {
                    ImplantInstallationMetadata.meta = new ImplantInstallationMetadata();
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

                meta.AddTypeMap("BookingNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ImplantType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SerialNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("PlacementSite", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ImplantInstallation";
                meta.Destination = "ImplantInstallation";
                meta.spInsert = "proc_ImplantInstallationInsert";
                meta.spUpdate = "proc_ImplantInstallationUpdate";
                meta.spDelete = "proc_ImplantInstallationDelete";
                meta.spLoadAll = "proc_ImplantInstallationLoadAll";
                meta.spLoadByPrimaryKey = "proc_ImplantInstallationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ImplantInstallationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
