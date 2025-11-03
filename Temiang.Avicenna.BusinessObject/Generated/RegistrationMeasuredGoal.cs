/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 01/29/19 11:58:57 AM
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
    abstract public class esRegistrationMeasuredGoalCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationMeasuredGoalCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RegistrationMeasuredGoalCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationMeasuredGoalQuery query)
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
            this.InitQuery(query as esRegistrationMeasuredGoalQuery);
        }
        #endregion

        virtual public RegistrationMeasuredGoal DetachEntity(RegistrationMeasuredGoal entity)
        {
            return base.DetachEntity(entity) as RegistrationMeasuredGoal;
        }

        virtual public RegistrationMeasuredGoal AttachEntity(RegistrationMeasuredGoal entity)
        {
            return base.AttachEntity(entity) as RegistrationMeasuredGoal;
        }

        virtual public void Combine(RegistrationMeasuredGoalCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationMeasuredGoal this[int index]
        {
            get
            {
                return base[index] as RegistrationMeasuredGoal;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationMeasuredGoal);
        }
    }

    [Serializable]
    abstract public class esRegistrationMeasuredGoal : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationMeasuredGoalQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationMeasuredGoal()
        {
        }

        public esRegistrationMeasuredGoal(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, Int32 seqNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 seqNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, seqNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, seqNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 seqNo)
        {
            esRegistrationMeasuredGoalQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SeqNo == seqNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 seqNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "SRUserType": this.str.SRUserType = (string)value; break;
                        case "Problem": this.str.Problem = (string)value; break;
                        case "Planning": this.str.Planning = (string)value; break;
                        case "Goal": this.str.Goal = (string)value; break;
                        case "IterationQty": this.str.IterationQty = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "SRTimeType": this.str.SRTimeType = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "SeqNo":

                            if (value == null || value is System.Int32)
                                this.SeqNo = (System.Int32?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
                            break;
                        case "IterationQty":

                            if (value == null || value is System.Int32)
                                this.IterationQty = (System.Int32?)value;
                            break;
                        case "Qty":

                            if (value == null || value is System.Int32)
                                this.Qty = (System.Int32?)value;
                            break;
                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
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
        /// Maps to RegistrationMeasuredGoal.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.SeqNo
        /// </summary>
        virtual public System.Int32? SeqNo
        {
            get
            {
                return base.GetSystemInt32(RegistrationMeasuredGoalMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemInt32(RegistrationMeasuredGoalMetadata.ColumnNames.SeqNo, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationMeasuredGoalMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationMeasuredGoalMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationMeasuredGoalMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationMeasuredGoalMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.SRUserType
        /// </summary>
        virtual public System.String SRUserType
        {
            get
            {
                return base.GetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.SRUserType);
            }

            set
            {
                base.SetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.SRUserType, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.Problem
        /// </summary>
        virtual public System.String Problem
        {
            get
            {
                return base.GetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.Problem);
            }

            set
            {
                base.SetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.Problem, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.Planning
        /// </summary>
        virtual public System.String Planning
        {
            get
            {
                return base.GetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.Planning);
            }

            set
            {
                base.SetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.Planning, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.Goal
        /// </summary>
        virtual public System.String Goal
        {
            get
            {
                return base.GetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.Goal);
            }

            set
            {
                base.SetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.Goal, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.IterationQty
        /// </summary>
        virtual public System.Int32? IterationQty
        {
            get
            {
                return base.GetSystemInt32(RegistrationMeasuredGoalMetadata.ColumnNames.IterationQty);
            }

            set
            {
                base.SetSystemInt32(RegistrationMeasuredGoalMetadata.ColumnNames.IterationQty, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.Qty
        /// </summary>
        virtual public System.Int32? Qty
        {
            get
            {
                return base.GetSystemInt32(RegistrationMeasuredGoalMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemInt32(RegistrationMeasuredGoalMetadata.ColumnNames.Qty, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.SRTimeType
        /// </summary>
        virtual public System.String SRTimeType
        {
            get
            {
                return base.GetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.SRTimeType);
            }

            set
            {
                base.SetSystemString(RegistrationMeasuredGoalMetadata.ColumnNames.SRTimeType, value);
            }
        }
        /// <summary>
        /// Maps to RegistrationMeasuredGoal.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(RegistrationMeasuredGoalMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(RegistrationMeasuredGoalMetadata.ColumnNames.IsVoid, value);
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
            public esStrings(esRegistrationMeasuredGoal entity)
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
            public System.String SeqNo
            {
                get
                {
                    System.Int32? data = entity.SeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeqNo = null;
                    else entity.SeqNo = Convert.ToInt32(value);
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
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
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
            public System.String SRUserType
            {
                get
                {
                    System.String data = entity.SRUserType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRUserType = null;
                    else entity.SRUserType = Convert.ToString(value);
                }
            }
            public System.String Problem
            {
                get
                {
                    System.String data = entity.Problem;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Problem = null;
                    else entity.Problem = Convert.ToString(value);
                }
            }
            public System.String Planning
            {
                get
                {
                    System.String data = entity.Planning;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Planning = null;
                    else entity.Planning = Convert.ToString(value);
                }
            }
            public System.String Goal
            {
                get
                {
                    System.String data = entity.Goal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Goal = null;
                    else entity.Goal = Convert.ToString(value);
                }
            }
            public System.String IterationQty
            {
                get
                {
                    System.Int32? data = entity.IterationQty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IterationQty = null;
                    else entity.IterationQty = Convert.ToInt32(value);
                }
            }
            public System.String Qty
            {
                get
                {
                    System.Int32? data = entity.Qty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Qty = null;
                    else entity.Qty = Convert.ToInt32(value);
                }
            }
            public System.String SRTimeType
            {
                get
                {
                    System.String data = entity.SRTimeType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTimeType = null;
                    else entity.SRTimeType = Convert.ToString(value);
                }
            }
            public System.String IsVoid
            {
                get
                {
                    System.Boolean? data = entity.IsVoid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoid = null;
                    else entity.IsVoid = Convert.ToBoolean(value);
                }
            }
            private esRegistrationMeasuredGoal entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationMeasuredGoalQuery query)
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
                throw new Exception("esRegistrationMeasuredGoal can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RegistrationMeasuredGoal : esRegistrationMeasuredGoal
    {
    }

    [Serializable]
    abstract public class esRegistrationMeasuredGoalQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RegistrationMeasuredGoalMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.SeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem SRUserType
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.SRUserType, esSystemType.String);
            }
        }

        public esQueryItem Problem
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.Problem, esSystemType.String);
            }
        }

        public esQueryItem Planning
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.Planning, esSystemType.String);
            }
        }

        public esQueryItem Goal
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.Goal, esSystemType.String);
            }
        }

        public esQueryItem IterationQty
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.IterationQty, esSystemType.Int32);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.Qty, esSystemType.Int32);
            }
        }

        public esQueryItem SRTimeType
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.SRTimeType, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, RegistrationMeasuredGoalMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationMeasuredGoalCollection")]
    public partial class RegistrationMeasuredGoalCollection : esRegistrationMeasuredGoalCollection, IEnumerable<RegistrationMeasuredGoal>
    {
        public RegistrationMeasuredGoalCollection()
        {

        }

        public static implicit operator List<RegistrationMeasuredGoal>(RegistrationMeasuredGoalCollection coll)
        {
            List<RegistrationMeasuredGoal> list = new List<RegistrationMeasuredGoal>();

            foreach (RegistrationMeasuredGoal emp in coll)
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
                return RegistrationMeasuredGoalMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationMeasuredGoalQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationMeasuredGoal(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationMeasuredGoal();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RegistrationMeasuredGoalQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationMeasuredGoalQuery();
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
        public bool Load(RegistrationMeasuredGoalQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RegistrationMeasuredGoal AddNew()
        {
            RegistrationMeasuredGoal entity = base.AddNewEntity() as RegistrationMeasuredGoal;

            return entity;
        }
        public RegistrationMeasuredGoal FindByPrimaryKey(String registrationNo, Int32 seqNo)
        {
            return base.FindByPrimaryKey(registrationNo, seqNo) as RegistrationMeasuredGoal;
        }

        #region IEnumerable< RegistrationMeasuredGoal> Members

        IEnumerator<RegistrationMeasuredGoal> IEnumerable<RegistrationMeasuredGoal>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationMeasuredGoal;
            }
        }

        #endregion

        private RegistrationMeasuredGoalQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationMeasuredGoal' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RegistrationMeasuredGoal ({RegistrationNo, SeqNo})")]
    [Serializable]
    public partial class RegistrationMeasuredGoal : esRegistrationMeasuredGoal
    {
        public RegistrationMeasuredGoal()
        {
        }

        public RegistrationMeasuredGoal(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationMeasuredGoalMetadata.Meta();
            }
        }

        override protected esRegistrationMeasuredGoalQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationMeasuredGoalQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RegistrationMeasuredGoalQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationMeasuredGoalQuery();
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
        public bool Load(RegistrationMeasuredGoalQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationMeasuredGoalQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RegistrationMeasuredGoalQuery : esRegistrationMeasuredGoalQuery
    {
        public RegistrationMeasuredGoalQuery()
        {

        }

        public RegistrationMeasuredGoalQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationMeasuredGoalQuery";
        }
    }

    [Serializable]
    public partial class RegistrationMeasuredGoalMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationMeasuredGoalMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.SeqNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.SeqNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.CreatedByUserID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.CreatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.SRUserType, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.SRUserType;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.Problem, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.Problem;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.Planning, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.Planning;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.Goal, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.Goal;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.IterationQty, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.IterationQty;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.Qty, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.Qty;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.SRTimeType, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.SRTimeType;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationMeasuredGoalMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RegistrationMeasuredGoalMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public RegistrationMeasuredGoalMetadata Meta()
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
            public const string SeqNo = "SeqNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string SRUserType = "SRUserType";
            public const string Problem = "Problem";
            public const string Planning = "Planning";
            public const string Goal = "Goal";
            public const string IterationQty = "IterationQty";
            public const string Qty = "Qty";
            public const string SRTimeType = "SRTimeType";
            public const string IsVoid = "IsVoid";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SeqNo = "SeqNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string SRUserType = "SRUserType";
            public const string Problem = "Problem";
            public const string Planning = "Planning";
            public const string Goal = "Goal";
            public const string IterationQty = "IterationQty";
            public const string Qty = "Qty";
            public const string SRTimeType = "SRTimeType";
            public const string IsVoid = "IsVoid";
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
            lock (typeof(RegistrationMeasuredGoalMetadata))
            {
                if (RegistrationMeasuredGoalMetadata.mapDelegates == null)
                {
                    RegistrationMeasuredGoalMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationMeasuredGoalMetadata.meta == null)
                {
                    RegistrationMeasuredGoalMetadata.meta = new RegistrationMeasuredGoalMetadata();
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
                meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRUserType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Problem", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Planning", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Goal", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IterationQty", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Qty", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SRTimeType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "RegistrationMeasuredGoal";
                meta.Destination = "RegistrationMeasuredGoal";
                meta.spInsert = "proc_RegistrationMeasuredGoalInsert";
                meta.spUpdate = "proc_RegistrationMeasuredGoalUpdate";
                meta.spDelete = "proc_RegistrationMeasuredGoalDelete";
                meta.spLoadAll = "proc_RegistrationMeasuredGoalLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationMeasuredGoalLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationMeasuredGoalMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
