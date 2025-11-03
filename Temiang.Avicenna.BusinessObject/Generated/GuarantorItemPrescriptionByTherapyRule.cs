/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/20/2020 3:03:40 PM
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
    abstract public class esGuarantorItemPrescriptionByTherapyRuleCollection : esEntityCollectionWAuditLog
    {
        public esGuarantorItemPrescriptionByTherapyRuleCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "GuarantorItemPrescriptionByTherapyRuleCollection";
        }

        #region Query Logic
        protected void InitQuery(esGuarantorItemPrescriptionByTherapyRuleQuery query)
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
            this.InitQuery(query as esGuarantorItemPrescriptionByTherapyRuleQuery);
        }
        #endregion

        virtual public GuarantorItemPrescriptionByTherapyRule DetachEntity(GuarantorItemPrescriptionByTherapyRule entity)
        {
            return base.DetachEntity(entity) as GuarantorItemPrescriptionByTherapyRule;
        }

        virtual public GuarantorItemPrescriptionByTherapyRule AttachEntity(GuarantorItemPrescriptionByTherapyRule entity)
        {
            return base.AttachEntity(entity) as GuarantorItemPrescriptionByTherapyRule;
        }

        virtual public void Combine(GuarantorItemPrescriptionByTherapyRuleCollection collection)
        {
            base.Combine(collection);
        }

        new public GuarantorItemPrescriptionByTherapyRule this[int index]
        {
            get
            {
                return base[index] as GuarantorItemPrescriptionByTherapyRule;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(GuarantorItemPrescriptionByTherapyRule);
        }
    }

    [Serializable]
    abstract public class esGuarantorItemPrescriptionByTherapyRule : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esGuarantorItemPrescriptionByTherapyRuleQuery GetDynamicQuery()
        {
            return null;
        }

        public esGuarantorItemPrescriptionByTherapyRule()
        {
        }

        public esGuarantorItemPrescriptionByTherapyRule(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String guarantorID, String sRTherapyGroup)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(guarantorID, sRTherapyGroup);
            else
                return LoadByPrimaryKeyStoredProcedure(guarantorID, sRTherapyGroup);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String guarantorID, String sRTherapyGroup)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(guarantorID, sRTherapyGroup);
            else
                return LoadByPrimaryKeyStoredProcedure(guarantorID, sRTherapyGroup);
        }

        private bool LoadByPrimaryKeyDynamic(String guarantorID, String sRTherapyGroup)
        {
            esGuarantorItemPrescriptionByTherapyRuleQuery query = this.GetDynamicQuery();
            query.Where(query.GuarantorID == guarantorID, query.SRTherapyGroup == sRTherapyGroup);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String guarantorID, String sRTherapyGroup)
        {
            esParameters parms = new esParameters();
            parms.Add("GuarantorID", guarantorID);
            parms.Add("SRTherapyGroup", sRTherapyGroup);
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
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "SRTherapyGroup": this.str.SRTherapyGroup = (string)value; break;
                        case "SRGuarantorRuleType": this.str.SRGuarantorRuleType = (string)value; break;
                        case "AmountValue": this.str.AmountValue = (string)value; break;
                        case "OutpatientAmountValue": this.str.OutpatientAmountValue = (string)value; break;
                        case "EmergencyAmountValue": this.str.EmergencyAmountValue = (string)value; break;
                        case "IsValueInPercent": this.str.IsValueInPercent = (string)value; break;
                        case "IsInclude": this.str.IsInclude = (string)value; break;
                        case "IsToGuarantor": this.str.IsToGuarantor = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "AmountValue":

                            if (value == null || value is System.Decimal)
                                this.AmountValue = (System.Decimal?)value;
                            break;
                        case "OutpatientAmountValue":

                            if (value == null || value is System.Decimal)
                                this.OutpatientAmountValue = (System.Decimal?)value;
                            break;
                        case "EmergencyAmountValue":

                            if (value == null || value is System.Decimal)
                                this.EmergencyAmountValue = (System.Decimal?)value;
                            break;
                        case "IsValueInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsValueInPercent = (System.Boolean?)value;
                            break;
                        case "IsInclude":

                            if (value == null || value is System.Boolean)
                                this.IsInclude = (System.Boolean?)value;
                            break;
                        case "IsToGuarantor":

                            if (value == null || value is System.Boolean)
                                this.IsToGuarantor = (System.Boolean?)value;
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
        /// Maps to GuarantorItemPrescriptionByTherapyRule.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.GuarantorID, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.SRTherapyGroup
        /// </summary>
        virtual public System.String SRTherapyGroup
        {
            get
            {
                return base.GetSystemString(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRTherapyGroup);
            }

            set
            {
                base.SetSystemString(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRTherapyGroup, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.SRGuarantorRuleType
        /// </summary>
        virtual public System.String SRGuarantorRuleType
        {
            get
            {
                return base.GetSystemString(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRGuarantorRuleType);
            }

            set
            {
                base.SetSystemString(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRGuarantorRuleType, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.AmountValue
        /// </summary>
        virtual public System.Decimal? AmountValue
        {
            get
            {
                return base.GetSystemDecimal(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.AmountValue);
            }

            set
            {
                base.SetSystemDecimal(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.AmountValue, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.OutpatientAmountValue
        /// </summary>
        virtual public System.Decimal? OutpatientAmountValue
        {
            get
            {
                return base.GetSystemDecimal(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.OutpatientAmountValue);
            }

            set
            {
                base.SetSystemDecimal(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.OutpatientAmountValue, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.EmergencyAmountValue
        /// </summary>
        virtual public System.Decimal? EmergencyAmountValue
        {
            get
            {
                return base.GetSystemDecimal(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.EmergencyAmountValue);
            }

            set
            {
                base.SetSystemDecimal(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.EmergencyAmountValue, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.IsValueInPercent
        /// </summary>
        virtual public System.Boolean? IsValueInPercent
        {
            get
            {
                return base.GetSystemBoolean(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsValueInPercent);
            }

            set
            {
                base.SetSystemBoolean(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsValueInPercent, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.IsInclude
        /// </summary>
        virtual public System.Boolean? IsInclude
        {
            get
            {
                return base.GetSystemBoolean(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsInclude);
            }

            set
            {
                base.SetSystemBoolean(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsInclude, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.IsToGuarantor
        /// </summary>
        virtual public System.Boolean? IsToGuarantor
        {
            get
            {
                return base.GetSystemBoolean(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsToGuarantor);
            }

            set
            {
                base.SetSystemBoolean(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsToGuarantor, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to GuarantorItemPrescriptionByTherapyRule.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esGuarantorItemPrescriptionByTherapyRule entity)
            {
                this.entity = entity;
            }
            public System.String GuarantorID
            {
                get
                {
                    System.String data = entity.GuarantorID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GuarantorID = null;
                    else entity.GuarantorID = Convert.ToString(value);
                }
            }
            public System.String SRTherapyGroup
            {
                get
                {
                    System.String data = entity.SRTherapyGroup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTherapyGroup = null;
                    else entity.SRTherapyGroup = Convert.ToString(value);
                }
            }
            public System.String SRGuarantorRuleType
            {
                get
                {
                    System.String data = entity.SRGuarantorRuleType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRGuarantorRuleType = null;
                    else entity.SRGuarantorRuleType = Convert.ToString(value);
                }
            }
            public System.String AmountValue
            {
                get
                {
                    System.Decimal? data = entity.AmountValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AmountValue = null;
                    else entity.AmountValue = Convert.ToDecimal(value);
                }
            }
            public System.String OutpatientAmountValue
            {
                get
                {
                    System.Decimal? data = entity.OutpatientAmountValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OutpatientAmountValue = null;
                    else entity.OutpatientAmountValue = Convert.ToDecimal(value);
                }
            }
            public System.String EmergencyAmountValue
            {
                get
                {
                    System.Decimal? data = entity.EmergencyAmountValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EmergencyAmountValue = null;
                    else entity.EmergencyAmountValue = Convert.ToDecimal(value);
                }
            }
            public System.String IsValueInPercent
            {
                get
                {
                    System.Boolean? data = entity.IsValueInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsValueInPercent = null;
                    else entity.IsValueInPercent = Convert.ToBoolean(value);
                }
            }
            public System.String IsInclude
            {
                get
                {
                    System.Boolean? data = entity.IsInclude;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsInclude = null;
                    else entity.IsInclude = Convert.ToBoolean(value);
                }
            }
            public System.String IsToGuarantor
            {
                get
                {
                    System.Boolean? data = entity.IsToGuarantor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsToGuarantor = null;
                    else entity.IsToGuarantor = Convert.ToBoolean(value);
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
            private esGuarantorItemPrescriptionByTherapyRule entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esGuarantorItemPrescriptionByTherapyRuleQuery query)
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
                throw new Exception("esGuarantorItemPrescriptionByTherapyRule can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class GuarantorItemPrescriptionByTherapyRule : esGuarantorItemPrescriptionByTherapyRule
    {
    }

    [Serializable]
    abstract public class esGuarantorItemPrescriptionByTherapyRuleQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return GuarantorItemPrescriptionByTherapyRuleMetadata.Meta();
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem SRTherapyGroup
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRTherapyGroup, esSystemType.String);
            }
        }

        public esQueryItem SRGuarantorRuleType
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRGuarantorRuleType, esSystemType.String);
            }
        }

        public esQueryItem AmountValue
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.AmountValue, esSystemType.Decimal);
            }
        }

        public esQueryItem OutpatientAmountValue
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.OutpatientAmountValue, esSystemType.Decimal);
            }
        }

        public esQueryItem EmergencyAmountValue
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.EmergencyAmountValue, esSystemType.Decimal);
            }
        }

        public esQueryItem IsValueInPercent
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsValueInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem IsInclude
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsInclude, esSystemType.Boolean);
            }
        }

        public esQueryItem IsToGuarantor
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsToGuarantor, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("GuarantorItemPrescriptionByTherapyRuleCollection")]
    public partial class GuarantorItemPrescriptionByTherapyRuleCollection : esGuarantorItemPrescriptionByTherapyRuleCollection, IEnumerable<GuarantorItemPrescriptionByTherapyRule>
    {
        public GuarantorItemPrescriptionByTherapyRuleCollection()
        {

        }

        public static implicit operator List<GuarantorItemPrescriptionByTherapyRule>(GuarantorItemPrescriptionByTherapyRuleCollection coll)
        {
            List<GuarantorItemPrescriptionByTherapyRule> list = new List<GuarantorItemPrescriptionByTherapyRule>();

            foreach (GuarantorItemPrescriptionByTherapyRule emp in coll)
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
                return GuarantorItemPrescriptionByTherapyRuleMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new GuarantorItemPrescriptionByTherapyRuleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new GuarantorItemPrescriptionByTherapyRule(row);
        }

        override protected esEntity CreateEntity()
        {
            return new GuarantorItemPrescriptionByTherapyRule();
        }

        #endregion

        [BrowsableAttribute(false)]
        public GuarantorItemPrescriptionByTherapyRuleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new GuarantorItemPrescriptionByTherapyRuleQuery();
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
        public bool Load(GuarantorItemPrescriptionByTherapyRuleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public GuarantorItemPrescriptionByTherapyRule AddNew()
        {
            GuarantorItemPrescriptionByTherapyRule entity = base.AddNewEntity() as GuarantorItemPrescriptionByTherapyRule;

            return entity;
        }
        public GuarantorItemPrescriptionByTherapyRule FindByPrimaryKey(String guarantorID, String sRTherapyGroup)
        {
            return base.FindByPrimaryKey(guarantorID, sRTherapyGroup) as GuarantorItemPrescriptionByTherapyRule;
        }

        #region IEnumerable< GuarantorItemPrescriptionByTherapyRule> Members

        IEnumerator<GuarantorItemPrescriptionByTherapyRule> IEnumerable<GuarantorItemPrescriptionByTherapyRule>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as GuarantorItemPrescriptionByTherapyRule;
            }
        }

        #endregion

        private GuarantorItemPrescriptionByTherapyRuleQuery query;
    }


    /// <summary>
    /// Encapsulates the 'GuarantorItemPrescriptionByTherapyRule' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("GuarantorItemPrescriptionByTherapyRule ({GuarantorID, SRTherapyGroup})")]
    [Serializable]
    public partial class GuarantorItemPrescriptionByTherapyRule : esGuarantorItemPrescriptionByTherapyRule
    {
        public GuarantorItemPrescriptionByTherapyRule()
        {
        }

        public GuarantorItemPrescriptionByTherapyRule(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return GuarantorItemPrescriptionByTherapyRuleMetadata.Meta();
            }
        }

        override protected esGuarantorItemPrescriptionByTherapyRuleQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new GuarantorItemPrescriptionByTherapyRuleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public GuarantorItemPrescriptionByTherapyRuleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new GuarantorItemPrescriptionByTherapyRuleQuery();
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
        public bool Load(GuarantorItemPrescriptionByTherapyRuleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private GuarantorItemPrescriptionByTherapyRuleQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class GuarantorItemPrescriptionByTherapyRuleQuery : esGuarantorItemPrescriptionByTherapyRuleQuery
    {
        public GuarantorItemPrescriptionByTherapyRuleQuery()
        {

        }

        public GuarantorItemPrescriptionByTherapyRuleQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "GuarantorItemPrescriptionByTherapyRuleQuery";
        }
    }

    [Serializable]
    public partial class GuarantorItemPrescriptionByTherapyRuleMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected GuarantorItemPrescriptionByTherapyRuleMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.GuarantorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRTherapyGroup, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.SRTherapyGroup;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.SRGuarantorRuleType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.SRGuarantorRuleType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.AmountValue, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.AmountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.OutpatientAmountValue, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.OutpatientAmountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.EmergencyAmountValue, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.EmergencyAmountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsValueInPercent, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.IsValueInPercent;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsInclude, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.IsInclude;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.IsToGuarantor, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.IsToGuarantor;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(GuarantorItemPrescriptionByTherapyRuleMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = GuarantorItemPrescriptionByTherapyRuleMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public GuarantorItemPrescriptionByTherapyRuleMetadata Meta()
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
            public const string GuarantorID = "GuarantorID";
            public const string SRTherapyGroup = "SRTherapyGroup";
            public const string SRGuarantorRuleType = "SRGuarantorRuleType";
            public const string AmountValue = "AmountValue";
            public const string OutpatientAmountValue = "OutpatientAmountValue";
            public const string EmergencyAmountValue = "EmergencyAmountValue";
            public const string IsValueInPercent = "IsValueInPercent";
            public const string IsInclude = "IsInclude";
            public const string IsToGuarantor = "IsToGuarantor";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string GuarantorID = "GuarantorID";
            public const string SRTherapyGroup = "SRTherapyGroup";
            public const string SRGuarantorRuleType = "SRGuarantorRuleType";
            public const string AmountValue = "AmountValue";
            public const string OutpatientAmountValue = "OutpatientAmountValue";
            public const string EmergencyAmountValue = "EmergencyAmountValue";
            public const string IsValueInPercent = "IsValueInPercent";
            public const string IsInclude = "IsInclude";
            public const string IsToGuarantor = "IsToGuarantor";
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
            lock (typeof(GuarantorItemPrescriptionByTherapyRuleMetadata))
            {
                if (GuarantorItemPrescriptionByTherapyRuleMetadata.mapDelegates == null)
                {
                    GuarantorItemPrescriptionByTherapyRuleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (GuarantorItemPrescriptionByTherapyRuleMetadata.meta == null)
                {
                    GuarantorItemPrescriptionByTherapyRuleMetadata.meta = new GuarantorItemPrescriptionByTherapyRuleMetadata();
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

                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRTherapyGroup", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRGuarantorRuleType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AmountValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("OutpatientAmountValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("EmergencyAmountValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsValueInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsInclude", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsToGuarantor", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "GuarantorItemPrescriptionByTherapyRule";
                meta.Destination = "GuarantorItemPrescriptionByTherapyRule";
                meta.spInsert = "proc_GuarantorItemPrescriptionByTherapyRuleInsert";
                meta.spUpdate = "proc_GuarantorItemPrescriptionByTherapyRuleUpdate";
                meta.spDelete = "proc_GuarantorItemPrescriptionByTherapyRuleDelete";
                meta.spLoadAll = "proc_GuarantorItemPrescriptionByTherapyRuleLoadAll";
                meta.spLoadByPrimaryKey = "proc_GuarantorItemPrescriptionByTherapyRuleLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private GuarantorItemPrescriptionByTherapyRuleMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
