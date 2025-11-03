/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2019 9:16:49 AM
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
    abstract public class esNutritionCareAssessmentTransHDCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareAssessmentTransHDCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareAssessmentTransHDCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareAssessmentTransHDQuery query)
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
            this.InitQuery(query as esNutritionCareAssessmentTransHDQuery);
        }
        #endregion

        virtual public NutritionCareAssessmentTransHD DetachEntity(NutritionCareAssessmentTransHD entity)
        {
            return base.DetachEntity(entity) as NutritionCareAssessmentTransHD;
        }

        virtual public NutritionCareAssessmentTransHD AttachEntity(NutritionCareAssessmentTransHD entity)
        {
            return base.AttachEntity(entity) as NutritionCareAssessmentTransHD;
        }

        virtual public void Combine(NutritionCareAssessmentTransHDCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareAssessmentTransHD this[int index]
        {
            get
            {
                return base[index] as NutritionCareAssessmentTransHD;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareAssessmentTransHD);
        }
    }

    [Serializable]
    abstract public class esNutritionCareAssessmentTransHD : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareAssessmentTransHDQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareAssessmentTransHD()
        {
        }

        public esNutritionCareAssessmentTransHD(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 iD)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 iD)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 iD)
        {
            esNutritionCareAssessmentTransHDQuery query = this.GetDynamicQuery();
            query.Where(query.ID == iD);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 iD)
        {
            esParameters parms = new esParameters();
            parms.Add("ID", iD);
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
                        case "ID": this.str.ID = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "AssessmentDateTime": this.str.AssessmentDateTime = (string)value; break;
                        case "QuestionFormReference": this.str.QuestionFormReference = (string)value; break;
                        case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ID":

                            if (value == null || value is System.Int64)
                                this.ID = (System.Int64?)value;
                            break;
                        case "AssessmentDateTime":

                            if (value == null || value is System.DateTime)
                                this.AssessmentDateTime = (System.DateTime?)value;
                            break;
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
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
        /// Maps to NutritionCareAssessmentTransHD.ID
        /// </summary>
        virtual public System.Int64? ID
        {
            get
            {
                return base.GetSystemInt64(NutritionCareAssessmentTransHDMetadata.ColumnNames.ID);
            }

            set
            {
                base.SetSystemInt64(NutritionCareAssessmentTransHDMetadata.ColumnNames.ID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransHD.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransHDMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransHDMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransHD.AssessmentDateTime
        /// </summary>
        virtual public System.DateTime? AssessmentDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentTransHDMetadata.ColumnNames.AssessmentDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentTransHDMetadata.ColumnNames.AssessmentDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransHD.QuestionFormReference
        /// </summary>
        virtual public System.String QuestionFormReference
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransHDMetadata.ColumnNames.QuestionFormReference);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransHDMetadata.ColumnNames.QuestionFormReference, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransHD.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransHDMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransHDMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransHD.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentTransHDMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentTransHDMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransHD.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareAssessmentTransHDMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareAssessmentTransHDMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareAssessmentTransHD.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareAssessmentTransHDMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareAssessmentTransHDMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNutritionCareAssessmentTransHD entity)
            {
                this.entity = entity;
            }
            public System.String ID
            {
                get
                {
                    System.Int64? data = entity.ID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ID = null;
                    else entity.ID = Convert.ToInt64(value);
                }
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
            public System.String AssessmentDateTime
            {
                get
                {
                    System.DateTime? data = entity.AssessmentDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AssessmentDateTime = null;
                    else entity.AssessmentDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String QuestionFormReference
            {
                get
                {
                    System.String data = entity.QuestionFormReference;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.QuestionFormReference = null;
                    else entity.QuestionFormReference = Convert.ToString(value);
                }
            }
            public System.String CreateByUserID
            {
                get
                {
                    System.String data = entity.CreateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateByUserID = null;
                    else entity.CreateByUserID = Convert.ToString(value);
                }
            }
            public System.String CreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateDateTime = null;
                    else entity.CreateDateTime = Convert.ToDateTime(value);
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
            private esNutritionCareAssessmentTransHD entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareAssessmentTransHDQuery query)
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
                throw new Exception("esNutritionCareAssessmentTransHD can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareAssessmentTransHD : esNutritionCareAssessmentTransHD
    {
    }

    [Serializable]
    abstract public class esNutritionCareAssessmentTransHDQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareAssessmentTransHDMetadata.Meta();
            }
        }

        public esQueryItem ID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransHDMetadata.ColumnNames.ID, esSystemType.Int64);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransHDMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem AssessmentDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransHDMetadata.ColumnNames.AssessmentDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem QuestionFormReference
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransHDMetadata.ColumnNames.QuestionFormReference, esSystemType.String);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransHDMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransHDMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransHDMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareAssessmentTransHDMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareAssessmentTransHDCollection")]
    public partial class NutritionCareAssessmentTransHDCollection : esNutritionCareAssessmentTransHDCollection, IEnumerable<NutritionCareAssessmentTransHD>
    {
        public NutritionCareAssessmentTransHDCollection()
        {

        }

        public static implicit operator List<NutritionCareAssessmentTransHD>(NutritionCareAssessmentTransHDCollection coll)
        {
            List<NutritionCareAssessmentTransHD> list = new List<NutritionCareAssessmentTransHD>();

            foreach (NutritionCareAssessmentTransHD emp in coll)
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
                return NutritionCareAssessmentTransHDMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareAssessmentTransHDQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareAssessmentTransHD(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareAssessmentTransHD();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareAssessmentTransHDQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareAssessmentTransHDQuery();
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
        public bool Load(NutritionCareAssessmentTransHDQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareAssessmentTransHD AddNew()
        {
            NutritionCareAssessmentTransHD entity = base.AddNewEntity() as NutritionCareAssessmentTransHD;

            return entity;
        }
        public NutritionCareAssessmentTransHD FindByPrimaryKey(Int64 iD)
        {
            return base.FindByPrimaryKey(iD) as NutritionCareAssessmentTransHD;
        }

        #region IEnumerable< NutritionCareAssessmentTransHD> Members

        IEnumerator<NutritionCareAssessmentTransHD> IEnumerable<NutritionCareAssessmentTransHD>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareAssessmentTransHD;
            }
        }

        #endregion

        private NutritionCareAssessmentTransHDQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareAssessmentTransHD' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareAssessmentTransHD ({ID})")]
    [Serializable]
    public partial class NutritionCareAssessmentTransHD : esNutritionCareAssessmentTransHD
    {
        public NutritionCareAssessmentTransHD()
        {
        }

        public NutritionCareAssessmentTransHD(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareAssessmentTransHDMetadata.Meta();
            }
        }

        override protected esNutritionCareAssessmentTransHDQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareAssessmentTransHDQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareAssessmentTransHDQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareAssessmentTransHDQuery();
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
        public bool Load(NutritionCareAssessmentTransHDQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareAssessmentTransHDQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareAssessmentTransHDQuery : esNutritionCareAssessmentTransHDQuery
    {
        public NutritionCareAssessmentTransHDQuery()
        {

        }

        public NutritionCareAssessmentTransHDQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareAssessmentTransHDQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareAssessmentTransHDMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareAssessmentTransHDMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareAssessmentTransHDMetadata.ColumnNames.ID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = NutritionCareAssessmentTransHDMetadata.PropertyNames.ID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransHDMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransHDMetadata.PropertyNames.TransactionNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransHDMetadata.ColumnNames.AssessmentDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentTransHDMetadata.PropertyNames.AssessmentDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransHDMetadata.ColumnNames.QuestionFormReference, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransHDMetadata.PropertyNames.QuestionFormReference;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransHDMetadata.ColumnNames.CreateByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransHDMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransHDMetadata.ColumnNames.CreateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentTransHDMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransHDMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareAssessmentTransHDMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareAssessmentTransHDMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareAssessmentTransHDMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareAssessmentTransHDMetadata Meta()
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
            public const string ID = "ID";
            public const string TransactionNo = "TransactionNo";
            public const string AssessmentDateTime = "AssessmentDateTime";
            public const string QuestionFormReference = "QuestionFormReference";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ID = "ID";
            public const string TransactionNo = "TransactionNo";
            public const string AssessmentDateTime = "AssessmentDateTime";
            public const string QuestionFormReference = "QuestionFormReference";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
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
            lock (typeof(NutritionCareAssessmentTransHDMetadata))
            {
                if (NutritionCareAssessmentTransHDMetadata.mapDelegates == null)
                {
                    NutritionCareAssessmentTransHDMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareAssessmentTransHDMetadata.meta == null)
                {
                    NutritionCareAssessmentTransHDMetadata.meta = new NutritionCareAssessmentTransHDMetadata();
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

                meta.AddTypeMap("ID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AssessmentDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("QuestionFormReference", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareAssessmentTransHD";
                meta.Destination = "NutritionCareAssessmentTransHD";
                meta.spInsert = "proc_NutritionCareAssessmentTransHDInsert";
                meta.spUpdate = "proc_NutritionCareAssessmentTransHDUpdate";
                meta.spDelete = "proc_NutritionCareAssessmentTransHDDelete";
                meta.spLoadAll = "proc_NutritionCareAssessmentTransHDLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareAssessmentTransHDLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareAssessmentTransHDMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
