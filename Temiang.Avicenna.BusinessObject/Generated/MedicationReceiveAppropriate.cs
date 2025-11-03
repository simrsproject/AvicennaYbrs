/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/28/18 6:49:14 AM
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
    abstract public class esMedicationReceiveAppropriateCollection : esEntityCollectionWAuditLog
    {
        public esMedicationReceiveAppropriateCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "MedicationReceiveAppropriateCollection";
        }

        #region Query Logic
        protected void InitQuery(esMedicationReceiveAppropriateQuery query)
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
            this.InitQuery(query as esMedicationReceiveAppropriateQuery);
        }
        #endregion

        virtual public MedicationReceiveAppropriate DetachEntity(MedicationReceiveAppropriate entity)
        {
            return base.DetachEntity(entity) as MedicationReceiveAppropriate;
        }

        virtual public MedicationReceiveAppropriate AttachEntity(MedicationReceiveAppropriate entity)
        {
            return base.AttachEntity(entity) as MedicationReceiveAppropriate;
        }

        virtual public void Combine(MedicationReceiveAppropriateCollection collection)
        {
            base.Combine(collection);
        }

        new public MedicationReceiveAppropriate this[int index]
        {
            get
            {
                return base[index] as MedicationReceiveAppropriate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MedicationReceiveAppropriate);
        }
    }

    [Serializable]
    abstract public class esMedicationReceiveAppropriate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMedicationReceiveAppropriateQuery GetDynamicQuery()
        {
            return null;
        }

        public esMedicationReceiveAppropriate()
        {
        }

        public esMedicationReceiveAppropriate(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 medicationReceiveNo, String appropriateType)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(medicationReceiveNo, appropriateType);
            else
                return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo, appropriateType);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 medicationReceiveNo, String appropriateType)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(medicationReceiveNo, appropriateType);
            else
                return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo, appropriateType);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 medicationReceiveNo, String appropriateType)
        {
            esMedicationReceiveAppropriateQuery query = this.GetDynamicQuery();
            query.Where(query.MedicationReceiveNo == medicationReceiveNo, query.AppropriateType == appropriateType);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 medicationReceiveNo, String appropriateType)
        {
            esParameters parms = new esParameters();
            parms.Add("MedicationReceiveNo", medicationReceiveNo);
            parms.Add("AppropriateType", appropriateType);
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
                        case "MedicationReceiveNo": this.str.MedicationReceiveNo = (string)value; break;
                        case "AppropriateType": this.str.AppropriateType = (string)value; break;
                        case "SRMedicationNotAppropriateReason": this.str.SRMedicationNotAppropriateReason = (string)value; break;
                        case "MedicationReason": this.str.MedicationReason = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CratedByUserID": this.str.CratedByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "MedicationReceiveNo":

                            if (value == null || value is System.Int64)
                                this.MedicationReceiveNo = (System.Int64?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to MedicationReceiveAppropriate.MedicationReceiveNo
        /// </summary>
        virtual public System.Int64? MedicationReceiveNo
        {
            get
            {
                return base.GetSystemInt64(MedicationReceiveAppropriateMetadata.ColumnNames.MedicationReceiveNo);
            }

            set
            {
                base.SetSystemInt64(MedicationReceiveAppropriateMetadata.ColumnNames.MedicationReceiveNo, value);
            }
        }
        /// <summary>
        /// Maps to MedicationReceiveAppropriate.AppropriateType
        /// </summary>
        virtual public System.String AppropriateType
        {
            get
            {
                return base.GetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.AppropriateType);
            }

            set
            {
                base.SetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.AppropriateType, value);
            }
        }
        /// <summary>
        /// Maps to MedicationReceiveAppropriate.SRMedicationNotAppropriateReason
        /// </summary>
        virtual public System.String SRMedicationNotAppropriateReason
        {
            get
            {
                return base.GetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.SRMedicationNotAppropriateReason);
            }

            set
            {
                base.SetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.SRMedicationNotAppropriateReason, value);
            }
        }
        /// <summary>
        /// Maps to MedicationReceiveAppropriate.MedicationReason
        /// </summary>
        virtual public System.String MedicationReason
        {
            get
            {
                return base.GetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.MedicationReason);
            }

            set
            {
                base.SetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.MedicationReason, value);
            }
        }
        /// <summary>
        /// Maps to MedicationReceiveAppropriate.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicationReceiveAppropriateMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicationReceiveAppropriateMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicationReceiveAppropriate.CratedByUserID
        /// </summary>
        virtual public System.String CratedByUserID
        {
            get
            {
                return base.GetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.CratedByUserID);
            }

            set
            {
                base.SetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.CratedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to MedicationReceiveAppropriate.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicationReceiveAppropriateMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicationReceiveAppropriateMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to MedicationReceiveAppropriate.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MedicationReceiveAppropriateMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esMedicationReceiveAppropriate entity)
            {
                this.entity = entity;
            }
            public System.String MedicationReceiveNo
            {
                get
                {
                    System.Int64? data = entity.MedicationReceiveNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicationReceiveNo = null;
                    else entity.MedicationReceiveNo = Convert.ToInt64(value);
                }
            }
            public System.String AppropriateType
            {
                get
                {
                    System.String data = entity.AppropriateType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AppropriateType = null;
                    else entity.AppropriateType = Convert.ToString(value);
                }
            }
            public System.String SRMedicationNotAppropriateReason
            {
                get
                {
                    System.String data = entity.SRMedicationNotAppropriateReason;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicationNotAppropriateReason = null;
                    else entity.SRMedicationNotAppropriateReason = Convert.ToString(value);
                }
            }
            public System.String MedicationReason
            {
                get
                {
                    System.String data = entity.MedicationReason;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicationReason = null;
                    else entity.MedicationReason = Convert.ToString(value);
                }
            }
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CratedByUserID
            {
                get
                {
                    System.String data = entity.CratedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CratedByUserID = null;
                    else entity.CratedByUserID = Convert.ToString(value);
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
            private esMedicationReceiveAppropriate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMedicationReceiveAppropriateQuery query)
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
                throw new Exception("esMedicationReceiveAppropriate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class MedicationReceiveAppropriate : esMedicationReceiveAppropriate
    {
    }

    [Serializable]
    abstract public class esMedicationReceiveAppropriateQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return MedicationReceiveAppropriateMetadata.Meta();
            }
        }

        public esQueryItem MedicationReceiveNo
        {
            get
            {
                return new esQueryItem(this, MedicationReceiveAppropriateMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
            }
        }

        public esQueryItem AppropriateType
        {
            get
            {
                return new esQueryItem(this, MedicationReceiveAppropriateMetadata.ColumnNames.AppropriateType, esSystemType.String);
            }
        }

        public esQueryItem SRMedicationNotAppropriateReason
        {
            get
            {
                return new esQueryItem(this, MedicationReceiveAppropriateMetadata.ColumnNames.SRMedicationNotAppropriateReason, esSystemType.String);
            }
        }

        public esQueryItem MedicationReason
        {
            get
            {
                return new esQueryItem(this, MedicationReceiveAppropriateMetadata.ColumnNames.MedicationReason, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, MedicationReceiveAppropriateMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CratedByUserID
        {
            get
            {
                return new esQueryItem(this, MedicationReceiveAppropriateMetadata.ColumnNames.CratedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicationReceiveAppropriateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MedicationReceiveAppropriateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MedicationReceiveAppropriateCollection")]
    public partial class MedicationReceiveAppropriateCollection : esMedicationReceiveAppropriateCollection, IEnumerable<MedicationReceiveAppropriate>
    {
        public MedicationReceiveAppropriateCollection()
        {

        }

        public static implicit operator List<MedicationReceiveAppropriate>(MedicationReceiveAppropriateCollection coll)
        {
            List<MedicationReceiveAppropriate> list = new List<MedicationReceiveAppropriate>();

            foreach (MedicationReceiveAppropriate emp in coll)
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
                return MedicationReceiveAppropriateMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicationReceiveAppropriateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MedicationReceiveAppropriate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MedicationReceiveAppropriate();
        }

        #endregion

        [BrowsableAttribute(false)]
        public MedicationReceiveAppropriateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicationReceiveAppropriateQuery();
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
        public bool Load(MedicationReceiveAppropriateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public MedicationReceiveAppropriate AddNew()
        {
            MedicationReceiveAppropriate entity = base.AddNewEntity() as MedicationReceiveAppropriate;

            return entity;
        }
        public MedicationReceiveAppropriate FindByPrimaryKey(Int64 medicationReceiveNo, String appropriateType)
        {
            return base.FindByPrimaryKey(medicationReceiveNo, appropriateType) as MedicationReceiveAppropriate;
        }

        #region IEnumerable< MedicationReceiveAppropriate> Members

        IEnumerator<MedicationReceiveAppropriate> IEnumerable<MedicationReceiveAppropriate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MedicationReceiveAppropriate;
            }
        }

        #endregion

        private MedicationReceiveAppropriateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MedicationReceiveAppropriate' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MedicationReceiveAppropriate ({MedicationReceiveNo, AppropriateType})")]
    [Serializable]
    public partial class MedicationReceiveAppropriate : esMedicationReceiveAppropriate
    {
        public MedicationReceiveAppropriate()
        {
        }

        public MedicationReceiveAppropriate(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MedicationReceiveAppropriateMetadata.Meta();
            }
        }

        override protected esMedicationReceiveAppropriateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicationReceiveAppropriateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public MedicationReceiveAppropriateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicationReceiveAppropriateQuery();
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
        public bool Load(MedicationReceiveAppropriateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MedicationReceiveAppropriateQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class MedicationReceiveAppropriateQuery : esMedicationReceiveAppropriateQuery
    {
        public MedicationReceiveAppropriateQuery()
        {

        }

        public MedicationReceiveAppropriateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MedicationReceiveAppropriateQuery";
        }
    }

    [Serializable]
    public partial class MedicationReceiveAppropriateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MedicationReceiveAppropriateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MedicationReceiveAppropriateMetadata.ColumnNames.MedicationReceiveNo, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = MedicationReceiveAppropriateMetadata.PropertyNames.MedicationReceiveNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(MedicationReceiveAppropriateMetadata.ColumnNames.AppropriateType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicationReceiveAppropriateMetadata.PropertyNames.AppropriateType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(MedicationReceiveAppropriateMetadata.ColumnNames.SRMedicationNotAppropriateReason, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicationReceiveAppropriateMetadata.PropertyNames.SRMedicationNotAppropriateReason;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicationReceiveAppropriateMetadata.ColumnNames.MedicationReason, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicationReceiveAppropriateMetadata.PropertyNames.MedicationReason;
            c.CharacterMaxLength = 400;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicationReceiveAppropriateMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicationReceiveAppropriateMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicationReceiveAppropriateMetadata.ColumnNames.CratedByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicationReceiveAppropriateMetadata.PropertyNames.CratedByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicationReceiveAppropriateMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicationReceiveAppropriateMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicationReceiveAppropriateMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicationReceiveAppropriateMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public MedicationReceiveAppropriateMetadata Meta()
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
            public const string MedicationReceiveNo = "MedicationReceiveNo";
            public const string AppropriateType = "AppropriateType";
            public const string SRMedicationNotAppropriateReason = "SRMedicationNotAppropriateReason";
            public const string MedicationReason = "MedicationReason";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CratedByUserID = "CratedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MedicationReceiveNo = "MedicationReceiveNo";
            public const string AppropriateType = "AppropriateType";
            public const string SRMedicationNotAppropriateReason = "SRMedicationNotAppropriateReason";
            public const string MedicationReason = "MedicationReason";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CratedByUserID = "CratedByUserID";
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
            lock (typeof(MedicationReceiveAppropriateMetadata))
            {
                if (MedicationReceiveAppropriateMetadata.mapDelegates == null)
                {
                    MedicationReceiveAppropriateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MedicationReceiveAppropriateMetadata.meta == null)
                {
                    MedicationReceiveAppropriateMetadata.meta = new MedicationReceiveAppropriateMetadata();
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

                meta.AddTypeMap("MedicationReceiveNo", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("AppropriateType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMedicationNotAppropriateReason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MedicationReason", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CratedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "MedicationReceiveAppropriate";
                meta.Destination = "MedicationReceiveAppropriate";
                meta.spInsert = "proc_MedicationReceiveAppropriateInsert";
                meta.spUpdate = "proc_MedicationReceiveAppropriateUpdate";
                meta.spDelete = "proc_MedicationReceiveAppropriateDelete";
                meta.spLoadAll = "proc_MedicationReceiveAppropriateLoadAll";
                meta.spLoadByPrimaryKey = "proc_MedicationReceiveAppropriateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MedicationReceiveAppropriateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
