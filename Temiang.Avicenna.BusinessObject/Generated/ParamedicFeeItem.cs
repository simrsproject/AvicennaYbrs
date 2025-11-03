/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/19/2014 1:44:18 PM
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
    abstract public class esParamedicFeeItemCollection : esEntityCollectionWAuditLog
    {
        public esParamedicFeeItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ParamedicFeeItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esParamedicFeeItemQuery query)
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
            this.InitQuery(query as esParamedicFeeItemQuery);
        }
        #endregion

        virtual public ParamedicFeeItem DetachEntity(ParamedicFeeItem entity)
        {
            return base.DetachEntity(entity) as ParamedicFeeItem;
        }

        virtual public ParamedicFeeItem AttachEntity(ParamedicFeeItem entity)
        {
            return base.AttachEntity(entity) as ParamedicFeeItem;
        }

        virtual public void Combine(ParamedicFeeItemCollection collection)
        {
            base.Combine(collection);
        }

        new public ParamedicFeeItem this[int index]
        {
            get
            {
                return base[index] as ParamedicFeeItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ParamedicFeeItem);
        }
    }



    [Serializable]
    abstract public class esParamedicFeeItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esParamedicFeeItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esParamedicFeeItem()
        {

        }

        public esParamedicFeeItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String paramedicID, System.String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paramedicID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(paramedicID, itemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paramedicID, System.String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paramedicID, itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(paramedicID, itemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String paramedicID, System.String itemID)
        {
            esParamedicFeeItemQuery query = this.GetDynamicQuery();
            query.Where(query.ParamedicID == paramedicID, query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String paramedicID, System.String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("ParamedicID", paramedicID); parms.Add("ItemID", itemID);
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
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "IsParamedicFeeUsePercentage": this.str.IsParamedicFeeUsePercentage = (string)value; break;
                        case "ParamedicFeeAmount": this.str.ParamedicFeeAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ParamedicFeeAmountReferral": this.str.ParamedicFeeAmountReferral = (string)value; break;
                        case "IsDeductionFeeUsePercentage": this.str.IsDeductionFeeUsePercentage = (string)value; break;
                        case "DeductionFeeAmount": this.str.DeductionFeeAmount = (string)value; break;
                        case "DeductionFeeAmountReferral": this.str.DeductionFeeAmountReferral = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsParamedicFeeUsePercentage":

                            if (value == null || value is System.Boolean)
                                this.IsParamedicFeeUsePercentage = (System.Boolean?)value;
                            break;

                        case "ParamedicFeeAmount":

                            if (value == null || value is System.Decimal)
                                this.ParamedicFeeAmount = (System.Decimal?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "ParamedicFeeAmountReferral":

                            if (value == null || value is System.Decimal)
                                this.ParamedicFeeAmountReferral = (System.Decimal?)value;
                            break;

                        case "IsDeductionFeeUsePercentage":

                            if (value == null || value is System.Boolean)
                                this.IsDeductionFeeUsePercentage = (System.Boolean?)value;
                            break;

                        case "DeductionFeeAmount":

                            if (value == null || value is System.Decimal)
                                this.DeductionFeeAmount = (System.Decimal?)value;
                            break;

                        case "DeductionFeeAmountReferral":

                            if (value == null || value is System.Decimal)
                                this.DeductionFeeAmountReferral = (System.Decimal?)value;
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
        /// Maps to ParamedicFeeItem.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeItemMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeItemMetadata.ColumnNames.ParamedicID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeItemMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeItemMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.IsParamedicFeeUsePercentage
        /// </summary>
        virtual public System.Boolean? IsParamedicFeeUsePercentage
        {
            get
            {
                return base.GetSystemBoolean(ParamedicFeeItemMetadata.ColumnNames.IsParamedicFeeUsePercentage);
            }

            set
            {
                base.SetSystemBoolean(ParamedicFeeItemMetadata.ColumnNames.IsParamedicFeeUsePercentage, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.ParamedicFeeAmount
        /// </summary>
        virtual public System.Decimal? ParamedicFeeAmount
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmount);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmount, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ParamedicFeeItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ParamedicFeeItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeItemMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.ParamedicFeeAmountReferral
        /// </summary>
        virtual public System.Decimal? ParamedicFeeAmountReferral
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmountReferral);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmountReferral, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.IsDeductionFeeUsePercentage
        /// </summary>
        virtual public System.Boolean? IsDeductionFeeUsePercentage
        {
            get
            {
                return base.GetSystemBoolean(ParamedicFeeItemMetadata.ColumnNames.IsDeductionFeeUsePercentage);
            }

            set
            {
                base.SetSystemBoolean(ParamedicFeeItemMetadata.ColumnNames.IsDeductionFeeUsePercentage, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.DeductionFeeAmount
        /// </summary>
        virtual public System.Decimal? DeductionFeeAmount
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmount);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmount, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItem.DeductionFeeAmountReferral
        /// </summary>
        virtual public System.Decimal? DeductionFeeAmountReferral
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmountReferral);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmountReferral, value);
            }
        }

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
            public esStrings(esParamedicFeeItem entity)
            {
                this.entity = entity;
            }


            public System.String ParamedicID
            {
                get
                {
                    System.String data = entity.ParamedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicID = null;
                    else entity.ParamedicID = Convert.ToString(value);
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

            public System.String IsParamedicFeeUsePercentage
            {
                get
                {
                    System.Boolean? data = entity.IsParamedicFeeUsePercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsParamedicFeeUsePercentage = null;
                    else entity.IsParamedicFeeUsePercentage = Convert.ToBoolean(value);
                }
            }

            public System.String ParamedicFeeAmount
            {
                get
                {
                    System.Decimal? data = entity.ParamedicFeeAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicFeeAmount = null;
                    else entity.ParamedicFeeAmount = Convert.ToDecimal(value);
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

            public System.String ParamedicFeeAmountReferral
            {
                get
                {
                    System.Decimal? data = entity.ParamedicFeeAmountReferral;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicFeeAmountReferral = null;
                    else entity.ParamedicFeeAmountReferral = Convert.ToDecimal(value);
                }
            }

            public System.String IsDeductionFeeUsePercentage
            {
                get
                {
                    System.Boolean? data = entity.IsDeductionFeeUsePercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDeductionFeeUsePercentage = null;
                    else entity.IsDeductionFeeUsePercentage = Convert.ToBoolean(value);
                }
            }

            public System.String DeductionFeeAmount
            {
                get
                {
                    System.Decimal? data = entity.DeductionFeeAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DeductionFeeAmount = null;
                    else entity.DeductionFeeAmount = Convert.ToDecimal(value);
                }
            }

            public System.String DeductionFeeAmountReferral
            {
                get
                {
                    System.Decimal? data = entity.DeductionFeeAmountReferral;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DeductionFeeAmountReferral = null;
                    else entity.DeductionFeeAmountReferral = Convert.ToDecimal(value);
                }
            }


            private esParamedicFeeItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esParamedicFeeItemQuery query)
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
                throw new Exception("esParamedicFeeItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ParamedicFeeItem : esParamedicFeeItem
    {


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
    abstract public class esParamedicFeeItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicFeeItemMetadata.Meta();
            }
        }


        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem IsParamedicFeeUsePercentage
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.IsParamedicFeeUsePercentage, esSystemType.Boolean);
            }
        }

        public esQueryItem ParamedicFeeAmount
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ParamedicFeeAmountReferral
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmountReferral, esSystemType.Decimal);
            }
        }

        public esQueryItem IsDeductionFeeUsePercentage
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.IsDeductionFeeUsePercentage, esSystemType.Boolean);
            }
        }

        public esQueryItem DeductionFeeAmount
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DeductionFeeAmountReferral
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmountReferral, esSystemType.Decimal);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ParamedicFeeItemCollection")]
    public partial class ParamedicFeeItemCollection : esParamedicFeeItemCollection, IEnumerable<ParamedicFeeItem>
    {
        public ParamedicFeeItemCollection()
        {

        }

        public static implicit operator List<ParamedicFeeItem>(ParamedicFeeItemCollection coll)
        {
            List<ParamedicFeeItem> list = new List<ParamedicFeeItem>();

            foreach (ParamedicFeeItem emp in coll)
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
                return ParamedicFeeItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicFeeItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ParamedicFeeItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ParamedicFeeItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ParamedicFeeItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicFeeItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ParamedicFeeItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ParamedicFeeItem AddNew()
        {
            ParamedicFeeItem entity = base.AddNewEntity() as ParamedicFeeItem;

            return entity;
        }

        public ParamedicFeeItem FindByPrimaryKey(System.String paramedicID, System.String itemID)
        {
            return base.FindByPrimaryKey(paramedicID, itemID) as ParamedicFeeItem;
        }


        #region IEnumerable<ParamedicFeeItem> Members

        IEnumerator<ParamedicFeeItem> IEnumerable<ParamedicFeeItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ParamedicFeeItem;
            }
        }

        #endregion

        private ParamedicFeeItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ParamedicFeeItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeItem ({ParamedicID},{ItemID})")]
    [Serializable]
    public partial class ParamedicFeeItem : esParamedicFeeItem
    {
        public ParamedicFeeItem()
        {

        }

        public ParamedicFeeItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicFeeItemMetadata.Meta();
            }
        }



        override protected esParamedicFeeItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicFeeItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ParamedicFeeItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicFeeItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ParamedicFeeItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ParamedicFeeItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ParamedicFeeItemQuery : esParamedicFeeItemQuery
    {
        public ParamedicFeeItemQuery()
        {

        }

        public ParamedicFeeItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ParamedicFeeItemQuery";
        }


    }


    [Serializable]
    public partial class ParamedicFeeItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ParamedicFeeItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.ParamedicID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.IsParamedicFeeUsePercentage, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.IsParamedicFeeUsePercentage;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.ParamedicFeeAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.ParamedicFeeAmountReferral, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.ParamedicFeeAmountReferral;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.IsDeductionFeeUsePercentage, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.IsDeductionFeeUsePercentage;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.DeductionFeeAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemMetadata.ColumnNames.DeductionFeeAmountReferral, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeItemMetadata.PropertyNames.DeductionFeeAmountReferral;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ParamedicFeeItemMetadata Meta()
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
            public const string ParamedicID = "ParamedicID";
            public const string ItemID = "ItemID";
            public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
            public const string ParamedicFeeAmount = "ParamedicFeeAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
            public const string IsDeductionFeeUsePercentage = "IsDeductionFeeUsePercentage";
            public const string DeductionFeeAmount = "DeductionFeeAmount";
            public const string DeductionFeeAmountReferral = "DeductionFeeAmountReferral";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ParamedicID = "ParamedicID";
            public const string ItemID = "ItemID";
            public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
            public const string ParamedicFeeAmount = "ParamedicFeeAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
            public const string IsDeductionFeeUsePercentage = "IsDeductionFeeUsePercentage";
            public const string DeductionFeeAmount = "DeductionFeeAmount";
            public const string DeductionFeeAmountReferral = "DeductionFeeAmountReferral";
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
            lock (typeof(ParamedicFeeItemMetadata))
            {
                if (ParamedicFeeItemMetadata.mapDelegates == null)
                {
                    ParamedicFeeItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ParamedicFeeItemMetadata.meta == null)
                {
                    ParamedicFeeItemMetadata.meta = new ParamedicFeeItemMetadata();
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


                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsParamedicFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsDeductionFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("DeductionFeeAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DeductionFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));



                meta.Source = "ParamedicFeeItem";
                meta.Destination = "ParamedicFeeItem";

                meta.spInsert = "proc_ParamedicFeeItemInsert";
                meta.spUpdate = "proc_ParamedicFeeItemUpdate";
                meta.spDelete = "proc_ParamedicFeeItemDelete";
                meta.spLoadAll = "proc_ParamedicFeeItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_ParamedicFeeItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ParamedicFeeItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
