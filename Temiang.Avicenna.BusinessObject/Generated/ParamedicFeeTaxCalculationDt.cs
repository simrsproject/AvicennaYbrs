/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/13/2016 11:59:57 AM
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
    abstract public class esParamedicFeeTaxCalculationDtCollection : esEntityCollectionWAuditLog
    {
        public esParamedicFeeTaxCalculationDtCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ParamedicFeeTaxCalculationDtCollection";
        }

        #region Query Logic
        protected void InitQuery(esParamedicFeeTaxCalculationDtQuery query)
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
            this.InitQuery(query as esParamedicFeeTaxCalculationDtQuery);
        }
        #endregion

        virtual public ParamedicFeeTaxCalculationDt DetachEntity(ParamedicFeeTaxCalculationDt entity)
        {
            return base.DetachEntity(entity) as ParamedicFeeTaxCalculationDt;
        }

        virtual public ParamedicFeeTaxCalculationDt AttachEntity(ParamedicFeeTaxCalculationDt entity)
        {
            return base.AttachEntity(entity) as ParamedicFeeTaxCalculationDt;
        }

        virtual public void Combine(ParamedicFeeTaxCalculationDtCollection collection)
        {
            base.Combine(collection);
        }

        new public ParamedicFeeTaxCalculationDt this[int index]
        {
            get
            {
                return base[index] as ParamedicFeeTaxCalculationDt;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ParamedicFeeTaxCalculationDt);
        }
    }



    [Serializable]
    abstract public class esParamedicFeeTaxCalculationDt : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esParamedicFeeTaxCalculationDtQuery GetDynamicQuery()
        {
            return null;
        }

        public esParamedicFeeTaxCalculationDt()
        {

        }

        public esParamedicFeeTaxCalculationDt(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Decimal gross, System.Decimal percentage, System.String verificationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(gross, percentage, verificationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(gross, percentage, verificationNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Decimal gross, System.Decimal percentage, System.String verificationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(gross, percentage, verificationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(gross, percentage, verificationNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.Decimal gross, System.Decimal percentage, System.String verificationNo)
        {
            esParamedicFeeTaxCalculationDtQuery query = this.GetDynamicQuery();
            query.Where(query.Gross == gross, query.Percentage == percentage, query.VerificationNo == verificationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Decimal gross, System.Decimal percentage, System.String verificationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("Gross", gross); parms.Add("Percentage", percentage); parms.Add("VerificationNo", verificationNo);
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
                        case "VerificationNo": this.str.VerificationNo = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "Period": this.str.Period = (string)value; break;
                        case "Percentage": this.str.Percentage = (string)value; break;
                        case "Gross": this.str.Gross = (string)value; break;
                        case "TaxBaseGross": this.str.TaxBaseGross = (string)value; break;
                        case "AccumulationTax": this.str.AccumulationTax = (string)value; break;
                        case "TaxToBePaid": this.str.TaxToBePaid = (string)value; break;
                        case "CounterID": this.str.CounterID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Period":

                            if (value == null || value is System.Int16)
                                this.Period = (System.Int16?)value;
                            break;

                        case "Percentage":

                            if (value == null || value is System.Decimal)
                                this.Percentage = (System.Decimal?)value;
                            break;

                        case "Gross":

                            if (value == null || value is System.Decimal)
                                this.Gross = (System.Decimal?)value;
                            break;

                        case "TaxBaseGross":

                            if (value == null || value is System.Decimal)
                                this.TaxBaseGross = (System.Decimal?)value;
                            break;

                        case "AccumulationTax":

                            if (value == null || value is System.Decimal)
                                this.AccumulationTax = (System.Decimal?)value;
                            break;

                        case "TaxToBePaid":

                            if (value == null || value is System.Decimal)
                                this.TaxToBePaid = (System.Decimal?)value;
                            break;

                        case "CounterID":

                            if (value == null || value is System.Int32)
                                this.CounterID = (System.Int32?)value;
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
        /// Maps to ParamedicFeeTaxCalculationDt.VerificationNo
        /// </summary>
        virtual public System.String VerificationNo
        {
            get
            {
                return base.GetSystemString(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.VerificationNo);
            }

            set
            {
                base.SetSystemString(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.VerificationNo, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeTaxCalculationDt.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.ParamedicID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeTaxCalculationDt.Period
        /// </summary>
        virtual public System.Int16? Period
        {
            get
            {
                return base.GetSystemInt16(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Period);
            }

            set
            {
                base.SetSystemInt16(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Period, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeTaxCalculationDt.Percentage
        /// </summary>
        virtual public System.Decimal? Percentage
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Percentage);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Percentage, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeTaxCalculationDt.Gross
        /// </summary>
        virtual public System.Decimal? Gross
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Gross);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Gross, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeTaxCalculationDt.TaxBaseGross
        /// </summary>
        virtual public System.Decimal? TaxBaseGross
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.TaxBaseGross);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.TaxBaseGross, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeTaxCalculationDt.AccumulationTax
        /// </summary>
        virtual public System.Decimal? AccumulationTax
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.AccumulationTax);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.AccumulationTax, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeTaxCalculationDt.TaxToBePaid
        /// </summary>
        virtual public System.Decimal? TaxToBePaid
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.TaxToBePaid);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.TaxToBePaid, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeTaxCalculationDt.CounterID
        /// </summary>
        virtual public System.Int32? CounterID
        {
            get
            {
                return base.GetSystemInt32(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.CounterID);
            }

            set
            {
                base.SetSystemInt32(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.CounterID, value);
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
            public esStrings(esParamedicFeeTaxCalculationDt entity)
            {
                this.entity = entity;
            }


            public System.String VerificationNo
            {
                get
                {
                    System.String data = entity.VerificationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VerificationNo = null;
                    else entity.VerificationNo = Convert.ToString(value);
                }
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

            public System.String Period
            {
                get
                {
                    System.Int16? data = entity.Period;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Period = null;
                    else entity.Period = Convert.ToInt16(value);
                }
            }

            public System.String Percentage
            {
                get
                {
                    System.Decimal? data = entity.Percentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Percentage = null;
                    else entity.Percentage = Convert.ToDecimal(value);
                }
            }

            public System.String Gross
            {
                get
                {
                    System.Decimal? data = entity.Gross;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Gross = null;
                    else entity.Gross = Convert.ToDecimal(value);
                }
            }

            public System.String TaxBaseGross
            {
                get
                {
                    System.Decimal? data = entity.TaxBaseGross;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TaxBaseGross = null;
                    else entity.TaxBaseGross = Convert.ToDecimal(value);
                }
            }

            public System.String AccumulationTax
            {
                get
                {
                    System.Decimal? data = entity.AccumulationTax;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AccumulationTax = null;
                    else entity.AccumulationTax = Convert.ToDecimal(value);
                }
            }

            public System.String TaxToBePaid
            {
                get
                {
                    System.Decimal? data = entity.TaxToBePaid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TaxToBePaid = null;
                    else entity.TaxToBePaid = Convert.ToDecimal(value);
                }
            }

            public System.String CounterID
            {
                get
                {
                    System.Int32? data = entity.CounterID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CounterID = null;
                    else entity.CounterID = Convert.ToInt32(value);
                }
            }


            private esParamedicFeeTaxCalculationDt entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esParamedicFeeTaxCalculationDtQuery query)
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
                throw new Exception("esParamedicFeeTaxCalculationDt can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ParamedicFeeTaxCalculationDt : esParamedicFeeTaxCalculationDt
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
    abstract public class esParamedicFeeTaxCalculationDtQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicFeeTaxCalculationDtMetadata.Meta();
            }
        }


        public esQueryItem VerificationNo
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.VerificationNo, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem Period
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Period, esSystemType.Int16);
            }
        }

        public esQueryItem Percentage
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Percentage, esSystemType.Decimal);
            }
        }

        public esQueryItem Gross
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Gross, esSystemType.Decimal);
            }
        }

        public esQueryItem TaxBaseGross
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.TaxBaseGross, esSystemType.Decimal);
            }
        }

        public esQueryItem AccumulationTax
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.AccumulationTax, esSystemType.Decimal);
            }
        }

        public esQueryItem TaxToBePaid
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.TaxToBePaid, esSystemType.Decimal);
            }
        }

        public esQueryItem CounterID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeTaxCalculationDtMetadata.ColumnNames.CounterID, esSystemType.Int32);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ParamedicFeeTaxCalculationDtCollection")]
    public partial class ParamedicFeeTaxCalculationDtCollection : esParamedicFeeTaxCalculationDtCollection, IEnumerable<ParamedicFeeTaxCalculationDt>
    {
        public ParamedicFeeTaxCalculationDtCollection()
        {

        }

        public static implicit operator List<ParamedicFeeTaxCalculationDt>(ParamedicFeeTaxCalculationDtCollection coll)
        {
            List<ParamedicFeeTaxCalculationDt> list = new List<ParamedicFeeTaxCalculationDt>();

            foreach (ParamedicFeeTaxCalculationDt emp in coll)
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
                return ParamedicFeeTaxCalculationDtMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicFeeTaxCalculationDtQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ParamedicFeeTaxCalculationDt(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ParamedicFeeTaxCalculationDt();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ParamedicFeeTaxCalculationDtQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicFeeTaxCalculationDtQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ParamedicFeeTaxCalculationDtQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ParamedicFeeTaxCalculationDt AddNew()
        {
            ParamedicFeeTaxCalculationDt entity = base.AddNewEntity() as ParamedicFeeTaxCalculationDt;

            return entity;
        }

        public ParamedicFeeTaxCalculationDt FindByPrimaryKey(System.Decimal gross, System.Decimal percentage, System.String verificationNo)
        {
            return base.FindByPrimaryKey(gross, percentage, verificationNo) as ParamedicFeeTaxCalculationDt;
        }


        #region IEnumerable<ParamedicFeeTaxCalculationDt> Members

        IEnumerator<ParamedicFeeTaxCalculationDt> IEnumerable<ParamedicFeeTaxCalculationDt>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ParamedicFeeTaxCalculationDt;
            }
        }

        #endregion

        private ParamedicFeeTaxCalculationDtQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ParamedicFeeTaxCalculationDt' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeTaxCalculationDt ({VerificationNo},{Percentage},{Gross})")]
    [Serializable]
    public partial class ParamedicFeeTaxCalculationDt : esParamedicFeeTaxCalculationDt
    {
        public ParamedicFeeTaxCalculationDt()
        {

        }

        public ParamedicFeeTaxCalculationDt(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicFeeTaxCalculationDtMetadata.Meta();
            }
        }



        override protected esParamedicFeeTaxCalculationDtQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicFeeTaxCalculationDtQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ParamedicFeeTaxCalculationDtQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicFeeTaxCalculationDtQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ParamedicFeeTaxCalculationDtQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ParamedicFeeTaxCalculationDtQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ParamedicFeeTaxCalculationDtQuery : esParamedicFeeTaxCalculationDtQuery
    {
        public ParamedicFeeTaxCalculationDtQuery()
        {

        }

        public ParamedicFeeTaxCalculationDtQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ParamedicFeeTaxCalculationDtQuery";
        }


    }


    [Serializable]
    public partial class ParamedicFeeTaxCalculationDtMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ParamedicFeeTaxCalculationDtMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.VerificationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.VerificationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Period, 2, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.Period;
            c.NumericPrecision = 5;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Percentage, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.Percentage;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.Gross, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.Gross;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.TaxBaseGross, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.TaxBaseGross;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.AccumulationTax, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.AccumulationTax;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.TaxToBePaid, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.TaxToBePaid;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeTaxCalculationDtMetadata.ColumnNames.CounterID, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ParamedicFeeTaxCalculationDtMetadata.PropertyNames.CounterID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ParamedicFeeTaxCalculationDtMetadata Meta()
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
            public const string VerificationNo = "VerificationNo";
            public const string ParamedicID = "ParamedicID";
            public const string Period = "Period";
            public const string Percentage = "Percentage";
            public const string Gross = "Gross";
            public const string TaxBaseGross = "TaxBaseGross";
            public const string AccumulationTax = "AccumulationTax";
            public const string TaxToBePaid = "TaxToBePaid";
            public const string CounterID = "CounterID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string VerificationNo = "VerificationNo";
            public const string ParamedicID = "ParamedicID";
            public const string Period = "Period";
            public const string Percentage = "Percentage";
            public const string Gross = "Gross";
            public const string TaxBaseGross = "TaxBaseGross";
            public const string AccumulationTax = "AccumulationTax";
            public const string TaxToBePaid = "TaxToBePaid";
            public const string CounterID = "CounterID";
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
            lock (typeof(ParamedicFeeTaxCalculationDtMetadata))
            {
                if (ParamedicFeeTaxCalculationDtMetadata.mapDelegates == null)
                {
                    ParamedicFeeTaxCalculationDtMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ParamedicFeeTaxCalculationDtMetadata.meta == null)
                {
                    ParamedicFeeTaxCalculationDtMetadata.meta = new ParamedicFeeTaxCalculationDtMetadata();
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


                meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Period", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("Percentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Gross", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TaxBaseGross", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("AccumulationTax", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("TaxToBePaid", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CounterID", new esTypeMap("int", "System.Int32"));



                meta.Source = "ParamedicFeeTaxCalculationDt";
                meta.Destination = "ParamedicFeeTaxCalculationDt";

                meta.spInsert = "proc_ParamedicFeeTaxCalculationDtInsert";
                meta.spUpdate = "proc_ParamedicFeeTaxCalculationDtUpdate";
                meta.spDelete = "proc_ParamedicFeeTaxCalculationDtDelete";
                meta.spLoadAll = "proc_ParamedicFeeTaxCalculationDtLoadAll";
                meta.spLoadByPrimaryKey = "proc_ParamedicFeeTaxCalculationDtLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ParamedicFeeTaxCalculationDtMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
