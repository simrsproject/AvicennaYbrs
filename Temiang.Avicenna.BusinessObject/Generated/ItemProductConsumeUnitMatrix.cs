/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 01/28/19 8:38:56 PM
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
    abstract public class esItemProductConsumeUnitMatrixCollection : esEntityCollectionWAuditLog
    {
        public esItemProductConsumeUnitMatrixCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemProductConsumeUnitMatrixCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemProductConsumeUnitMatrixQuery query)
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
            this.InitQuery(query as esItemProductConsumeUnitMatrixQuery);
        }
        #endregion

        virtual public ItemProductConsumeUnitMatrix DetachEntity(ItemProductConsumeUnitMatrix entity)
        {
            return base.DetachEntity(entity) as ItemProductConsumeUnitMatrix;
        }

        virtual public ItemProductConsumeUnitMatrix AttachEntity(ItemProductConsumeUnitMatrix entity)
        {
            return base.AttachEntity(entity) as ItemProductConsumeUnitMatrix;
        }

        virtual public void Combine(ItemProductConsumeUnitMatrixCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemProductConsumeUnitMatrix this[int index]
        {
            get
            {
                return base[index] as ItemProductConsumeUnitMatrix;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemProductConsumeUnitMatrix);
        }
    }

    [Serializable]
    abstract public class esItemProductConsumeUnitMatrix : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemProductConsumeUnitMatrixQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemProductConsumeUnitMatrix()
        {
        }

        public esItemProductConsumeUnitMatrix(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String itemID, String sRItemUnit, String sRConsumeUnit)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID, sRItemUnit, sRConsumeUnit);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID, sRItemUnit, sRConsumeUnit);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String itemID, String sRItemUnit, String sRConsumeUnit)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID, sRItemUnit, sRConsumeUnit);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID, sRItemUnit, sRConsumeUnit);
        }

        private bool LoadByPrimaryKeyDynamic(String itemID, String sRItemUnit, String sRConsumeUnit)
        {
            esItemProductConsumeUnitMatrixQuery query = this.GetDynamicQuery();
            query.Where(query.ItemID == itemID, query.SRItemUnit == sRItemUnit, query.SRConsumeUnit == sRConsumeUnit);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String itemID, String sRItemUnit, String sRConsumeUnit)
        {
            esParameters parms = new esParameters();
            parms.Add("ItemID", itemID);
            parms.Add("SRItemUnit", sRItemUnit);
            parms.Add("SRConsumeUnit", sRConsumeUnit);
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
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
                        case "SRConsumeUnit": this.str.SRConsumeUnit = (string)value; break;
                        case "ConversionFactor": this.str.ConversionFactor = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ConversionFactor":

                            if (value == null || value is System.Decimal)
                                this.ConversionFactor = (System.Decimal?)value;
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
        /// Maps to ItemProductConsumeUnitMatrix.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemProductConsumeUnitMatrixMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemProductConsumeUnitMatrixMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemProductConsumeUnitMatrix.SRItemUnit
        /// </summary>
        virtual public System.String SRItemUnit
        {
            get
            {
                return base.GetSystemString(ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRItemUnit);
            }

            set
            {
                base.SetSystemString(ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRItemUnit, value);
            }
        }
        /// <summary>
        /// Maps to ItemProductConsumeUnitMatrix.SRConsumeUnit
        /// </summary>
        virtual public System.String SRConsumeUnit
        {
            get
            {
                return base.GetSystemString(ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRConsumeUnit);
            }

            set
            {
                base.SetSystemString(ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRConsumeUnit, value);
            }
        }
        /// <summary>
        /// Maps to ItemProductConsumeUnitMatrix.ConversionFactor
        /// </summary>
        virtual public System.Decimal? ConversionFactor
        {
            get
            {
                return base.GetSystemDecimal(ItemProductConsumeUnitMatrixMetadata.ColumnNames.ConversionFactor);
            }

            set
            {
                base.SetSystemDecimal(ItemProductConsumeUnitMatrixMetadata.ColumnNames.ConversionFactor, value);
            }
        }
        /// <summary>
        /// Maps to ItemProductConsumeUnitMatrix.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemProductConsumeUnitMatrixMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemProductConsumeUnitMatrixMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemProductConsumeUnitMatrix.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemProductConsumeUnitMatrixMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemProductConsumeUnitMatrixMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esItemProductConsumeUnitMatrix entity)
            {
                this.entity = entity;
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
            public System.String SRItemUnit
            {
                get
                {
                    System.String data = entity.SRItemUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemUnit = null;
                    else entity.SRItemUnit = Convert.ToString(value);
                }
            }
            public System.String SRConsumeUnit
            {
                get
                {
                    System.String data = entity.SRConsumeUnit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRConsumeUnit = null;
                    else entity.SRConsumeUnit = Convert.ToString(value);
                }
            }
            public System.String ConversionFactor
            {
                get
                {
                    System.Decimal? data = entity.ConversionFactor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ConversionFactor = null;
                    else entity.ConversionFactor = Convert.ToDecimal(value);
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
            private esItemProductConsumeUnitMatrix entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemProductConsumeUnitMatrixQuery query)
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
                throw new Exception("esItemProductConsumeUnitMatrix can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemProductConsumeUnitMatrix : esItemProductConsumeUnitMatrix
    {
    }

    [Serializable]
    abstract public class esItemProductConsumeUnitMatrixQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemProductConsumeUnitMatrixMetadata.Meta();
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemProductConsumeUnitMatrixMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem SRItemUnit
        {
            get
            {
                return new esQueryItem(this, ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRItemUnit, esSystemType.String);
            }
        }

        public esQueryItem SRConsumeUnit
        {
            get
            {
                return new esQueryItem(this, ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRConsumeUnit, esSystemType.String);
            }
        }

        public esQueryItem ConversionFactor
        {
            get
            {
                return new esQueryItem(this, ItemProductConsumeUnitMatrixMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemProductConsumeUnitMatrixMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemProductConsumeUnitMatrixMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemProductConsumeUnitMatrixCollection")]
    public partial class ItemProductConsumeUnitMatrixCollection : esItemProductConsumeUnitMatrixCollection, IEnumerable<ItemProductConsumeUnitMatrix>
    {
        public ItemProductConsumeUnitMatrixCollection()
        {

        }

        public static implicit operator List<ItemProductConsumeUnitMatrix>(ItemProductConsumeUnitMatrixCollection coll)
        {
            List<ItemProductConsumeUnitMatrix> list = new List<ItemProductConsumeUnitMatrix>();

            foreach (ItemProductConsumeUnitMatrix emp in coll)
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
                return ItemProductConsumeUnitMatrixMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemProductConsumeUnitMatrixQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemProductConsumeUnitMatrix(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemProductConsumeUnitMatrix();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemProductConsumeUnitMatrixQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemProductConsumeUnitMatrixQuery();
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
        public bool Load(ItemProductConsumeUnitMatrixQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemProductConsumeUnitMatrix AddNew()
        {
            ItemProductConsumeUnitMatrix entity = base.AddNewEntity() as ItemProductConsumeUnitMatrix;

            return entity;
        }
        public ItemProductConsumeUnitMatrix FindByPrimaryKey(String itemID, String sRItemUnit, String sRConsumeUnit)
        {
            return base.FindByPrimaryKey(itemID, sRItemUnit, sRConsumeUnit) as ItemProductConsumeUnitMatrix;
        }

        #region IEnumerable< ItemProductConsumeUnitMatrix> Members

        IEnumerator<ItemProductConsumeUnitMatrix> IEnumerable<ItemProductConsumeUnitMatrix>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemProductConsumeUnitMatrix;
            }
        }

        #endregion

        private ItemProductConsumeUnitMatrixQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemProductConsumeUnitMatrix' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemProductConsumeUnitMatrix ({ItemID, SRItemUnit, SRConsumeUnit})")]
    [Serializable]
    public partial class ItemProductConsumeUnitMatrix : esItemProductConsumeUnitMatrix
    {
        public ItemProductConsumeUnitMatrix()
        {
        }

        public ItemProductConsumeUnitMatrix(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemProductConsumeUnitMatrixMetadata.Meta();
            }
        }

        override protected esItemProductConsumeUnitMatrixQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemProductConsumeUnitMatrixQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemProductConsumeUnitMatrixQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemProductConsumeUnitMatrixQuery();
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
        public bool Load(ItemProductConsumeUnitMatrixQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemProductConsumeUnitMatrixQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemProductConsumeUnitMatrixQuery : esItemProductConsumeUnitMatrixQuery
    {
        public ItemProductConsumeUnitMatrixQuery()
        {

        }

        public ItemProductConsumeUnitMatrixQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemProductConsumeUnitMatrixQuery";
        }
    }

    [Serializable]
    public partial class ItemProductConsumeUnitMatrixMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemProductConsumeUnitMatrixMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemProductConsumeUnitMatrixMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductConsumeUnitMatrixMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRItemUnit, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductConsumeUnitMatrixMetadata.PropertyNames.SRItemUnit;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductConsumeUnitMatrixMetadata.ColumnNames.SRConsumeUnit, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductConsumeUnitMatrixMetadata.PropertyNames.SRConsumeUnit;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductConsumeUnitMatrixMetadata.ColumnNames.ConversionFactor, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemProductConsumeUnitMatrixMetadata.PropertyNames.ConversionFactor;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductConsumeUnitMatrixMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemProductConsumeUnitMatrixMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemProductConsumeUnitMatrixMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemProductConsumeUnitMatrixMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemProductConsumeUnitMatrixMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string SRItemUnit = "SRItemUnit";
            public const string SRConsumeUnit = "SRConsumeUnit";
            public const string ConversionFactor = "ConversionFactor";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemID = "ItemID";
            public const string SRItemUnit = "SRItemUnit";
            public const string SRConsumeUnit = "SRConsumeUnit";
            public const string ConversionFactor = "ConversionFactor";
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
            lock (typeof(ItemProductConsumeUnitMatrixMetadata))
            {
                if (ItemProductConsumeUnitMatrixMetadata.mapDelegates == null)
                {
                    ItemProductConsumeUnitMatrixMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemProductConsumeUnitMatrixMetadata.meta == null)
                {
                    ItemProductConsumeUnitMatrixMetadata.meta = new ItemProductConsumeUnitMatrixMetadata();
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

                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRConsumeUnit", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ItemProductConsumeUnitMatrix";
                meta.Destination = "ItemProductConsumeUnitMatrix";
                meta.spInsert = "proc_ItemProductConsumeUnitMatrixInsert";
                meta.spUpdate = "proc_ItemProductConsumeUnitMatrixUpdate";
                meta.spDelete = "proc_ItemProductConsumeUnitMatrixDelete";
                meta.spLoadAll = "proc_ItemProductConsumeUnitMatrixLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemProductConsumeUnitMatrixLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemProductConsumeUnitMatrixMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
