/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/22/2014 4:52:50 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

    [Serializable]
    abstract public class esRegistrationItemRuleCollection : esEntityCollectionWAuditLog
    {
        public esRegistrationItemRuleCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RegistrationItemRuleCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationItemRuleQuery query)
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
            this.InitQuery(query as esRegistrationItemRuleQuery);
        }
        #endregion

        virtual public RegistrationItemRule DetachEntity(RegistrationItemRule entity)
        {
            return base.DetachEntity(entity) as RegistrationItemRule;
        }

        virtual public RegistrationItemRule AttachEntity(RegistrationItemRule entity)
        {
            return base.AttachEntity(entity) as RegistrationItemRule;
        }

        virtual public void Combine(RegistrationItemRuleCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationItemRule this[int index]
        {
            get
            {
                return base[index] as RegistrationItemRule;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationItemRule);
        }
    }



    [Serializable]
    abstract public class esRegistrationItemRule : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationItemRuleQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationItemRule()
        {

        }

        public esRegistrationItemRule(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo, System.String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, itemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.String itemID)
        {
            esRegistrationItemRuleQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo); parms.Add("ItemID", itemID);
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
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "SRGuarantorRuleType": this.str.SRGuarantorRuleType = (string)value; break;
                        case "AmountValue": this.str.AmountValue = (string)value; break;
                        case "IsValueInPercent": this.str.IsValueInPercent = (string)value; break;
                        case "IsInclude": this.str.IsInclude = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsToGuarantor": this.str.IsToGuarantor = (string)value; break;
                        case "OutpatientAmountValue": this.str.OutpatientAmountValue = (string)value; break;
                        case "EmergencyAmountValue": this.str.EmergencyAmountValue = (string)value; break;
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

                        case "IsValueInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsValueInPercent = (System.Boolean?)value;
                            break;

                        case "IsInclude":

                            if (value == null || value is System.Boolean)
                                this.IsInclude = (System.Boolean?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "IsToGuarantor":

                            if (value == null || value is System.Boolean)
                                this.IsToGuarantor = (System.Boolean?)value;
                            break;

                        case "OutpatientAmountValue":

                            if (value == null || value is System.Decimal)
                                this.OutpatientAmountValue = (System.Decimal?)value;
                            break;

                        case "EmergencyAmountValue":

                            if (value == null || value is System.Decimal)
                                this.EmergencyAmountValue = (System.Decimal?)value;
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
        /// Maps to RegistrationItemRule.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationItemRuleMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                if (base.SetSystemString(RegistrationItemRuleMetadata.ColumnNames.RegistrationNo, value))
                {
                    this._UpToRegistrationByRegistrationNo = null;
                }
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(RegistrationItemRuleMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(RegistrationItemRuleMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.SRGuarantorRuleType
        /// </summary>
        virtual public System.String SRGuarantorRuleType
        {
            get
            {
                return base.GetSystemString(RegistrationItemRuleMetadata.ColumnNames.SRGuarantorRuleType);
            }

            set
            {
                base.SetSystemString(RegistrationItemRuleMetadata.ColumnNames.SRGuarantorRuleType, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.AmountValue
        /// </summary>
        virtual public System.Decimal? AmountValue
        {
            get
            {
                return base.GetSystemDecimal(RegistrationItemRuleMetadata.ColumnNames.AmountValue);
            }

            set
            {
                base.SetSystemDecimal(RegistrationItemRuleMetadata.ColumnNames.AmountValue, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.IsValueInPercent
        /// </summary>
        virtual public System.Boolean? IsValueInPercent
        {
            get
            {
                return base.GetSystemBoolean(RegistrationItemRuleMetadata.ColumnNames.IsValueInPercent);
            }

            set
            {
                base.SetSystemBoolean(RegistrationItemRuleMetadata.ColumnNames.IsValueInPercent, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.IsInclude
        /// </summary>
        virtual public System.Boolean? IsInclude
        {
            get
            {
                return base.GetSystemBoolean(RegistrationItemRuleMetadata.ColumnNames.IsInclude);
            }

            set
            {
                base.SetSystemBoolean(RegistrationItemRuleMetadata.ColumnNames.IsInclude, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationItemRuleMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationItemRuleMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationItemRuleMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationItemRuleMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.IsToGuarantor
        /// </summary>
        virtual public System.Boolean? IsToGuarantor
        {
            get
            {
                return base.GetSystemBoolean(RegistrationItemRuleMetadata.ColumnNames.IsToGuarantor);
            }

            set
            {
                base.SetSystemBoolean(RegistrationItemRuleMetadata.ColumnNames.IsToGuarantor, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.OutpatientAmountValue
        /// </summary>
        virtual public System.Decimal? OutpatientAmountValue
        {
            get
            {
                return base.GetSystemDecimal(RegistrationItemRuleMetadata.ColumnNames.OutpatientAmountValue);
            }

            set
            {
                base.SetSystemDecimal(RegistrationItemRuleMetadata.ColumnNames.OutpatientAmountValue, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationItemRule.EmergencyAmountValue
        /// </summary>
        virtual public System.Decimal? EmergencyAmountValue
        {
            get
            {
                return base.GetSystemDecimal(RegistrationItemRuleMetadata.ColumnNames.EmergencyAmountValue);
            }

            set
            {
                base.SetSystemDecimal(RegistrationItemRuleMetadata.ColumnNames.EmergencyAmountValue, value);
            }
        }

        [CLSCompliant(false)]
        internal protected Registration _UpToRegistrationByRegistrationNo;
        #endregion

        #region String Properties


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
            public esStrings(esRegistrationItemRule entity)
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


            private esRegistrationItemRule entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationItemRuleQuery query)
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
                throw new Exception("esRegistrationItemRule can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RegistrationItemRule : esRegistrationItemRule
    {


        #region UpToRegistrationByRegistrationNo - Many To One
        /// <summary>
        /// Many to One
        /// Foreign Key Name - RefRegistrationToRegistrationItemRule
        /// </summary>

        [XmlIgnore]
        public Registration UpToRegistrationByRegistrationNo
        {
            get
            {
                if (this._UpToRegistrationByRegistrationNo == null
                    && RegistrationNo != null)
                {
                    this._UpToRegistrationByRegistrationNo = new Registration();
                    this._UpToRegistrationByRegistrationNo.es.Connection.Name = this.es.Connection.Name;
                    this.SetPreSave("UpToRegistrationByRegistrationNo", this._UpToRegistrationByRegistrationNo);
                    this._UpToRegistrationByRegistrationNo.Query.Where(this._UpToRegistrationByRegistrationNo.Query.RegistrationNo == this.RegistrationNo);
                    this._UpToRegistrationByRegistrationNo.Query.Load();
                }

                return this._UpToRegistrationByRegistrationNo;
            }

            set
            {
                this.RemovePreSave("UpToRegistrationByRegistrationNo");


                if (value == null)
                {
                    this.RegistrationNo = null;
                    this._UpToRegistrationByRegistrationNo = null;
                }
                else
                {
                    this.RegistrationNo = value.RegistrationNo;
                    this._UpToRegistrationByRegistrationNo = value;
                    this.SetPreSave("UpToRegistrationByRegistrationNo", this._UpToRegistrationByRegistrationNo);
                }

            }
        }
        #endregion



        /// <summary>
        /// Used internally by the entity's hierarchical properties.
        /// </summary>
        protected override List<esPropertyDescriptor> GetHierarchicalProperties()
        {
            List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();


            return props;
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PreSave.
        /// </summary>
        protected override void ApplyPreSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostSave.
        /// </summary>
        protected override void ApplyPostSaveKeys()
        {
        }

        /// <summary>
        /// Used internally for retrieving AutoIncrementing keys
        /// during hierarchical PostOneToOneSave.
        /// </summary>
        protected override void ApplyPostOneSaveKeys()
        {
        }

    }



    [Serializable]
    abstract public class esRegistrationItemRuleQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationItemRuleMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem SRGuarantorRuleType
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.SRGuarantorRuleType, esSystemType.String);
            }
        }

        public esQueryItem AmountValue
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.AmountValue, esSystemType.Decimal);
            }
        }

        public esQueryItem IsValueInPercent
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.IsValueInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem IsInclude
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.IsInclude, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsToGuarantor
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.IsToGuarantor, esSystemType.Boolean);
            }
        }

        public esQueryItem OutpatientAmountValue
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.OutpatientAmountValue, esSystemType.Decimal);
            }
        }

        public esQueryItem EmergencyAmountValue
        {
            get
            {
                return new esQueryItem(this, RegistrationItemRuleMetadata.ColumnNames.EmergencyAmountValue, esSystemType.Decimal);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationItemRuleCollection")]
    public partial class RegistrationItemRuleCollection : esRegistrationItemRuleCollection, IEnumerable<RegistrationItemRule>
    {
        public RegistrationItemRuleCollection()
        {

        }

        public static implicit operator List<RegistrationItemRule>(RegistrationItemRuleCollection coll)
        {
            List<RegistrationItemRule> list = new List<RegistrationItemRule>();

            foreach (RegistrationItemRule emp in coll)
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
                return RegistrationItemRuleMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationItemRuleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationItemRule(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationItemRule();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RegistrationItemRuleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationItemRuleQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RegistrationItemRuleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RegistrationItemRule AddNew()
        {
            RegistrationItemRule entity = base.AddNewEntity() as RegistrationItemRule;

            return entity;
        }

        public RegistrationItemRule FindByPrimaryKey(System.String registrationNo, System.String itemID)
        {
            return base.FindByPrimaryKey(registrationNo, itemID) as RegistrationItemRule;
        }


        #region IEnumerable<RegistrationItemRule> Members

        IEnumerator<RegistrationItemRule> IEnumerable<RegistrationItemRule>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationItemRule;
            }
        }

        #endregion

        private RegistrationItemRuleQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationItemRule' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationItemRule ({RegistrationNo},{ItemID})")]
    [Serializable]
    public partial class RegistrationItemRule : esRegistrationItemRule
    {
        public RegistrationItemRule()
        {

        }

        public RegistrationItemRule(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationItemRuleMetadata.Meta();
            }
        }



        override protected esRegistrationItemRuleQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationItemRuleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RegistrationItemRuleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationItemRuleQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RegistrationItemRuleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationItemRuleQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RegistrationItemRuleQuery : esRegistrationItemRuleQuery
    {
        public RegistrationItemRuleQuery()
        {

        }

        public RegistrationItemRuleQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationItemRuleQuery";
        }


    }


    [Serializable]
    public partial class RegistrationItemRuleMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationItemRuleMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.SRGuarantorRuleType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.SRGuarantorRuleType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.AmountValue, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.AmountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.IsValueInPercent, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.IsValueInPercent;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.IsInclude, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.IsInclude;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.IsToGuarantor, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.IsToGuarantor;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.OutpatientAmountValue, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.OutpatientAmountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationItemRuleMetadata.ColumnNames.EmergencyAmountValue, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationItemRuleMetadata.PropertyNames.EmergencyAmountValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RegistrationItemRuleMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string SRGuarantorRuleType = "SRGuarantorRuleType";
            public const string AmountValue = "AmountValue";
            public const string IsValueInPercent = "IsValueInPercent";
            public const string IsInclude = "IsInclude";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsToGuarantor = "IsToGuarantor";
            public const string OutpatientAmountValue = "OutpatientAmountValue";
            public const string EmergencyAmountValue = "EmergencyAmountValue";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string ItemID = "ItemID";
            public const string SRGuarantorRuleType = "SRGuarantorRuleType";
            public const string AmountValue = "AmountValue";
            public const string IsValueInPercent = "IsValueInPercent";
            public const string IsInclude = "IsInclude";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsToGuarantor = "IsToGuarantor";
            public const string OutpatientAmountValue = "OutpatientAmountValue";
            public const string EmergencyAmountValue = "EmergencyAmountValue";
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
            lock (typeof(RegistrationItemRuleMetadata))
            {
                if (RegistrationItemRuleMetadata.mapDelegates == null)
                {
                    RegistrationItemRuleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationItemRuleMetadata.meta == null)
                {
                    RegistrationItemRuleMetadata.meta = new RegistrationItemRuleMetadata();
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
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRGuarantorRuleType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AmountValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsValueInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsInclude", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsToGuarantor", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("OutpatientAmountValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("EmergencyAmountValue", new esTypeMap("numeric", "System.Decimal"));



                meta.Source = "RegistrationItemRule";
                meta.Destination = "RegistrationItemRule";

                meta.spInsert = "proc_RegistrationItemRuleInsert";
                meta.spUpdate = "proc_RegistrationItemRuleUpdate";
                meta.spDelete = "proc_RegistrationItemRuleDelete";
                meta.spLoadAll = "proc_RegistrationItemRuleLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationItemRuleLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationItemRuleMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
