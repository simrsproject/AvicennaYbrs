/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/19/2014 1:44:20 PM
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
    abstract public class esParamedicFeeItemGuarantorCollection : esEntityCollectionWAuditLog
    {
        public esParamedicFeeItemGuarantorCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ParamedicFeeItemGuarantorCollection";
        }

        #region Query Logic
        protected void InitQuery(esParamedicFeeItemGuarantorQuery query)
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
            this.InitQuery(query as esParamedicFeeItemGuarantorQuery);
        }
        #endregion

        virtual public ParamedicFeeItemGuarantor DetachEntity(ParamedicFeeItemGuarantor entity)
        {
            return base.DetachEntity(entity) as ParamedicFeeItemGuarantor;
        }

        virtual public ParamedicFeeItemGuarantor AttachEntity(ParamedicFeeItemGuarantor entity)
        {
            return base.AttachEntity(entity) as ParamedicFeeItemGuarantor;
        }

        virtual public void Combine(ParamedicFeeItemGuarantorCollection collection)
        {
            base.Combine(collection);
        }

        new public ParamedicFeeItemGuarantor this[int index]
        {
            get
            {
                return base[index] as ParamedicFeeItemGuarantor;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ParamedicFeeItemGuarantor);
        }
    }



    [Serializable]
    abstract public class esParamedicFeeItemGuarantor : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esParamedicFeeItemGuarantorQuery GetDynamicQuery()
        {
            return null;
        }

        public esParamedicFeeItemGuarantor()
        {

        }

        public esParamedicFeeItemGuarantor(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String paramedicID, System.String itemID, System.String guarantorID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paramedicID, itemID, guarantorID);
            else
                return LoadByPrimaryKeyStoredProcedure(paramedicID, itemID, guarantorID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paramedicID, System.String itemID, System.String guarantorID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(paramedicID, itemID, guarantorID);
            else
                return LoadByPrimaryKeyStoredProcedure(paramedicID, itemID, guarantorID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String paramedicID, System.String itemID, System.String guarantorID)
        {
            esParamedicFeeItemGuarantorQuery query = this.GetDynamicQuery();
            query.Where(query.ParamedicID == paramedicID, query.ItemID == itemID, query.GuarantorID == guarantorID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String paramedicID, System.String itemID, System.String guarantorID)
        {
            esParameters parms = new esParameters();
            parms.Add("ParamedicID", paramedicID); parms.Add("ItemID", itemID); parms.Add("GuarantorID", guarantorID);
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
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "IsParamedicFeeUsePercentage": this.str.IsParamedicFeeUsePercentage = (string)value; break;
                        case "ParamedicFeeAmount": this.str.ParamedicFeeAmount = (string)value; break;
                        case "ParamedicFeeAmountReferral": this.str.ParamedicFeeAmountReferral = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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

                        case "ParamedicFeeAmountReferral":

                            if (value == null || value is System.Decimal)
                                this.ParamedicFeeAmountReferral = (System.Decimal?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
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
        /// Maps to ParamedicFeeItemGuarantor.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeItemGuarantorMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeItemGuarantorMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeItemGuarantorMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeItemGuarantorMetadata.ColumnNames.GuarantorID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.IsParamedicFeeUsePercentage
        /// </summary>
        virtual public System.Boolean? IsParamedicFeeUsePercentage
        {
            get
            {
                return base.GetSystemBoolean(ParamedicFeeItemGuarantorMetadata.ColumnNames.IsParamedicFeeUsePercentage);
            }

            set
            {
                base.SetSystemBoolean(ParamedicFeeItemGuarantorMetadata.ColumnNames.IsParamedicFeeUsePercentage, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.ParamedicFeeAmount
        /// </summary>
        virtual public System.Decimal? ParamedicFeeAmount
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmount);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmount, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.ParamedicFeeAmountReferral
        /// </summary>
        virtual public System.Decimal? ParamedicFeeAmountReferral
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmountReferral);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmountReferral, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ParamedicFeeItemGuarantorMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ParamedicFeeItemGuarantorMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeItemGuarantorMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeItemGuarantorMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.IsDeductionFeeUsePercentage
        /// </summary>
        virtual public System.Boolean? IsDeductionFeeUsePercentage
        {
            get
            {
                return base.GetSystemBoolean(ParamedicFeeItemGuarantorMetadata.ColumnNames.IsDeductionFeeUsePercentage);
            }

            set
            {
                base.SetSystemBoolean(ParamedicFeeItemGuarantorMetadata.ColumnNames.IsDeductionFeeUsePercentage, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.DeductionFeeAmount
        /// </summary>
        virtual public System.Decimal? DeductionFeeAmount
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmount);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmount, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeItemGuarantor.DeductionFeeAmountReferral
        /// </summary>
        virtual public System.Decimal? DeductionFeeAmountReferral
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmountReferral);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmountReferral, value);
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
            public esStrings(esParamedicFeeItemGuarantor entity)
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


            private esParamedicFeeItemGuarantor entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esParamedicFeeItemGuarantorQuery query)
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
                throw new Exception("esParamedicFeeItemGuarantor can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ParamedicFeeItemGuarantor : esParamedicFeeItemGuarantor
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
    abstract public class esParamedicFeeItemGuarantorQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicFeeItemGuarantorMetadata.Meta();
            }
        }


        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem IsParamedicFeeUsePercentage
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.IsParamedicFeeUsePercentage, esSystemType.Boolean);
            }
        }

        public esQueryItem ParamedicFeeAmount
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem ParamedicFeeAmountReferral
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmountReferral, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsDeductionFeeUsePercentage
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.IsDeductionFeeUsePercentage, esSystemType.Boolean);
            }
        }

        public esQueryItem DeductionFeeAmount
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem DeductionFeeAmountReferral
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmountReferral, esSystemType.Decimal);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ParamedicFeeItemGuarantorCollection")]
    public partial class ParamedicFeeItemGuarantorCollection : esParamedicFeeItemGuarantorCollection, IEnumerable<ParamedicFeeItemGuarantor>
    {
        public ParamedicFeeItemGuarantorCollection()
        {

        }

        public static implicit operator List<ParamedicFeeItemGuarantor>(ParamedicFeeItemGuarantorCollection coll)
        {
            List<ParamedicFeeItemGuarantor> list = new List<ParamedicFeeItemGuarantor>();

            foreach (ParamedicFeeItemGuarantor emp in coll)
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
                return ParamedicFeeItemGuarantorMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicFeeItemGuarantorQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ParamedicFeeItemGuarantor(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ParamedicFeeItemGuarantor();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ParamedicFeeItemGuarantorQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicFeeItemGuarantorQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ParamedicFeeItemGuarantorQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ParamedicFeeItemGuarantor AddNew()
        {
            ParamedicFeeItemGuarantor entity = base.AddNewEntity() as ParamedicFeeItemGuarantor;

            return entity;
        }

        public ParamedicFeeItemGuarantor FindByPrimaryKey(System.String paramedicID, System.String itemID, System.String guarantorID)
        {
            return base.FindByPrimaryKey(paramedicID, itemID, guarantorID) as ParamedicFeeItemGuarantor;
        }


        #region IEnumerable<ParamedicFeeItemGuarantor> Members

        IEnumerator<ParamedicFeeItemGuarantor> IEnumerable<ParamedicFeeItemGuarantor>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ParamedicFeeItemGuarantor;
            }
        }

        #endregion

        private ParamedicFeeItemGuarantorQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ParamedicFeeItemGuarantor' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeItemGuarantor ({ParamedicID},{ItemID},{GuarantorID})")]
    [Serializable]
    public partial class ParamedicFeeItemGuarantor : esParamedicFeeItemGuarantor
    {
        public ParamedicFeeItemGuarantor()
        {

        }

        public ParamedicFeeItemGuarantor(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicFeeItemGuarantorMetadata.Meta();
            }
        }



        override protected esParamedicFeeItemGuarantorQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicFeeItemGuarantorQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ParamedicFeeItemGuarantorQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicFeeItemGuarantorQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ParamedicFeeItemGuarantorQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ParamedicFeeItemGuarantorQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ParamedicFeeItemGuarantorQuery : esParamedicFeeItemGuarantorQuery
    {
        public ParamedicFeeItemGuarantorQuery()
        {

        }

        public ParamedicFeeItemGuarantorQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ParamedicFeeItemGuarantorQuery";
        }


    }


    [Serializable]
    public partial class ParamedicFeeItemGuarantorMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ParamedicFeeItemGuarantorMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.ParamedicID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.GuarantorID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.GuarantorID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.IsParamedicFeeUsePercentage, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.IsParamedicFeeUsePercentage;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.ParamedicFeeAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmountReferral, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.ParamedicFeeAmountReferral;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.IsDeductionFeeUsePercentage, 8, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.IsDeductionFeeUsePercentage;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.DeductionFeeAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmountReferral, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeItemGuarantorMetadata.PropertyNames.DeductionFeeAmountReferral;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ParamedicFeeItemGuarantorMetadata Meta()
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
            public const string GuarantorID = "GuarantorID";
            public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
            public const string ParamedicFeeAmount = "ParamedicFeeAmount";
            public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
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
            public const string GuarantorID = "GuarantorID";
            public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
            public const string ParamedicFeeAmount = "ParamedicFeeAmount";
            public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
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
            lock (typeof(ParamedicFeeItemGuarantorMetadata))
            {
                if (ParamedicFeeItemGuarantorMetadata.mapDelegates == null)
                {
                    ParamedicFeeItemGuarantorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ParamedicFeeItemGuarantorMetadata.meta == null)
                {
                    ParamedicFeeItemGuarantorMetadata.meta = new ParamedicFeeItemGuarantorMetadata();
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
                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsParamedicFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ParamedicFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsDeductionFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("DeductionFeeAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DeductionFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));



                meta.Source = "ParamedicFeeItemGuarantor";
                meta.Destination = "ParamedicFeeItemGuarantor";

                meta.spInsert = "proc_ParamedicFeeItemGuarantorInsert";
                meta.spUpdate = "proc_ParamedicFeeItemGuarantorUpdate";
                meta.spDelete = "proc_ParamedicFeeItemGuarantorDelete";
                meta.spLoadAll = "proc_ParamedicFeeItemGuarantorLoadAll";
                meta.spLoadByPrimaryKey = "proc_ParamedicFeeItemGuarantorLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ParamedicFeeItemGuarantorMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
