/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/13/2015 11:14:55 AM
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
    abstract public class esDietLiquidPatientTimeCollection : esEntityCollectionWAuditLog
    {
        public esDietLiquidPatientTimeCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "DietLiquidPatientTimeCollection";
        }

        #region Query Logic
        protected void InitQuery(esDietLiquidPatientTimeQuery query)
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
            this.InitQuery(query as esDietLiquidPatientTimeQuery);
        }
        #endregion

        virtual public DietLiquidPatientTime DetachEntity(DietLiquidPatientTime entity)
        {
            return base.DetachEntity(entity) as DietLiquidPatientTime;
        }

        virtual public DietLiquidPatientTime AttachEntity(DietLiquidPatientTime entity)
        {
            return base.AttachEntity(entity) as DietLiquidPatientTime;
        }

        virtual public void Combine(DietLiquidPatientTimeCollection collection)
        {
            base.Combine(collection);
        }

        new public DietLiquidPatientTime this[int index]
        {
            get
            {
                return base[index] as DietLiquidPatientTime;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(DietLiquidPatientTime);
        }
    }



    [Serializable]
    abstract public class esDietLiquidPatientTime : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDietLiquidPatientTimeQuery GetDynamicQuery()
        {
            return null;
        }

        public esDietLiquidPatientTime()
        {

        }

        public esDietLiquidPatientTime(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String dietTime)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, dietTime);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, dietTime);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String dietTime)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo, dietTime);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo, dietTime);
        }

        private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String dietTime)
        {
            esDietLiquidPatientTimeQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo, query.DietTime == dietTime);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String dietTime)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo); parms.Add("DietTime", dietTime);
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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "DietTime": this.str.DietTime = (string)value; break;
                        case "FoodID": this.str.FoodID = (string)value; break;
                        case "Measure": this.str.Measure = (string)value; break;
                        case "AmountOfWater": this.str.AmountOfWater = (string)value; break;
                        case "Etc": this.str.Etc = (string)value; break;
                        case "Total": this.str.Total = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "AmountOfWater":

                            if (value == null || value is System.Decimal)
                                this.AmountOfWater = (System.Decimal?)value;
                            break;

                        case "Total":

                            if (value == null || value is System.Decimal)
                                this.Total = (System.Decimal?)value;
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
        /// Maps to DietLiquidPatientTime.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.DietTime
        /// </summary>
        virtual public System.String DietTime
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.DietTime);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.DietTime, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.FoodID
        /// </summary>
        virtual public System.String FoodID
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.FoodID);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.FoodID, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.Measure
        /// </summary>
        virtual public System.String Measure
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.Measure);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.Measure, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.AmountOfWater
        /// </summary>
        virtual public System.Decimal? AmountOfWater
        {
            get
            {
                return base.GetSystemDecimal(DietLiquidPatientTimeMetadata.ColumnNames.AmountOfWater);
            }

            set
            {
                base.SetSystemDecimal(DietLiquidPatientTimeMetadata.ColumnNames.AmountOfWater, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.Etc
        /// </summary>
        virtual public System.String Etc
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.Etc);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.Etc, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.Total
        /// </summary>
        virtual public System.Decimal? Total
        {
            get
            {
                return base.GetSystemDecimal(DietLiquidPatientTimeMetadata.ColumnNames.Total);
            }

            set
            {
                base.SetSystemDecimal(DietLiquidPatientTimeMetadata.ColumnNames.Total, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DietLiquidPatientTimeMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DietLiquidPatientTimeMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatientTime.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientTimeMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esDietLiquidPatientTime entity)
            {
                this.entity = entity;
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

            public System.String DietTime
            {
                get
                {
                    System.String data = entity.DietTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietTime = null;
                    else entity.DietTime = Convert.ToString(value);
                }
            }

            public System.String FoodID
            {
                get
                {
                    System.String data = entity.FoodID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FoodID = null;
                    else entity.FoodID = Convert.ToString(value);
                }
            }

            public System.String Measure
            {
                get
                {
                    System.String data = entity.Measure;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Measure = null;
                    else entity.Measure = Convert.ToString(value);
                }
            }

            public System.String AmountOfWater
            {
                get
                {
                    System.Decimal? data = entity.AmountOfWater;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AmountOfWater = null;
                    else entity.AmountOfWater = Convert.ToDecimal(value);
                }
            }

            public System.String Etc
            {
                get
                {
                    System.String data = entity.Etc;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Etc = null;
                    else entity.Etc = Convert.ToString(value);
                }
            }

            public System.String Total
            {
                get
                {
                    System.Decimal? data = entity.Total;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Total = null;
                    else entity.Total = Convert.ToDecimal(value);
                }
            }

            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
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


            private esDietLiquidPatientTime entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDietLiquidPatientTimeQuery query)
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
                throw new Exception("esDietLiquidPatientTime can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class DietLiquidPatientTime : esDietLiquidPatientTime
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
    abstract public class esDietLiquidPatientTimeQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return DietLiquidPatientTimeMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem DietTime
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.DietTime, esSystemType.String);
            }
        }

        public esQueryItem FoodID
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.FoodID, esSystemType.String);
            }
        }

        public esQueryItem Measure
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.Measure, esSystemType.String);
            }
        }

        public esQueryItem AmountOfWater
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.AmountOfWater, esSystemType.Decimal);
            }
        }

        public esQueryItem Etc
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.Etc, esSystemType.String);
            }
        }

        public esQueryItem Total
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.Total, esSystemType.Decimal);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientTimeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DietLiquidPatientTimeCollection")]
    public partial class DietLiquidPatientTimeCollection : esDietLiquidPatientTimeCollection, IEnumerable<DietLiquidPatientTime>
    {
        public DietLiquidPatientTimeCollection()
        {

        }

        public static implicit operator List<DietLiquidPatientTime>(DietLiquidPatientTimeCollection coll)
        {
            List<DietLiquidPatientTime> list = new List<DietLiquidPatientTime>();

            foreach (DietLiquidPatientTime emp in coll)
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
                return DietLiquidPatientTimeMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietLiquidPatientTimeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new DietLiquidPatientTime(row);
        }

        override protected esEntity CreateEntity()
        {
            return new DietLiquidPatientTime();
        }


        #endregion


        [BrowsableAttribute(false)]
        public DietLiquidPatientTimeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietLiquidPatientTimeQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(DietLiquidPatientTimeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public DietLiquidPatientTime AddNew()
        {
            DietLiquidPatientTime entity = base.AddNewEntity() as DietLiquidPatientTime;

            return entity;
        }

        public DietLiquidPatientTime FindByPrimaryKey(System.String transactionNo, System.String dietTime)
        {
            return base.FindByPrimaryKey(transactionNo, dietTime) as DietLiquidPatientTime;
        }


        #region IEnumerable<DietLiquidPatientTime> Members

        IEnumerator<DietLiquidPatientTime> IEnumerable<DietLiquidPatientTime>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as DietLiquidPatientTime;
            }
        }

        #endregion

        private DietLiquidPatientTimeQuery query;
    }


    /// <summary>
    /// Encapsulates the 'DietLiquidPatientTime' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("DietLiquidPatientTime ({TransactionNo},{DietTime})")]
    [Serializable]
    public partial class DietLiquidPatientTime : esDietLiquidPatientTime
    {
        public DietLiquidPatientTime()
        {

        }

        public DietLiquidPatientTime(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DietLiquidPatientTimeMetadata.Meta();
            }
        }



        override protected esDietLiquidPatientTimeQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietLiquidPatientTimeQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public DietLiquidPatientTimeQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietLiquidPatientTimeQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(DietLiquidPatientTimeQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DietLiquidPatientTimeQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class DietLiquidPatientTimeQuery : esDietLiquidPatientTimeQuery
    {
        public DietLiquidPatientTimeQuery()
        {

        }

        public DietLiquidPatientTimeQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DietLiquidPatientTimeQuery";
        }


    }


    [Serializable]
    public partial class DietLiquidPatientTimeMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DietLiquidPatientTimeMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.DietTime, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.DietTime;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.FoodID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.FoodID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.Measure, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.Measure;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.AmountOfWater, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.AmountOfWater;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.Etc, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.Etc;
            c.CharacterMaxLength = 150;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.Total, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.Total;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientTimeMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientTimeMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public DietLiquidPatientTimeMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string DietTime = "DietTime";
            public const string FoodID = "FoodID";
            public const string Measure = "Measure";
            public const string AmountOfWater = "AmountOfWater";
            public const string Etc = "Etc";
            public const string Total = "Total";
            public const string Notes = "Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string DietTime = "DietTime";
            public const string FoodID = "FoodID";
            public const string Measure = "Measure";
            public const string AmountOfWater = "AmountOfWater";
            public const string Etc = "Etc";
            public const string Total = "Total";
            public const string Notes = "Notes";
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
            lock (typeof(DietLiquidPatientTimeMetadata))
            {
                if (DietLiquidPatientTimeMetadata.mapDelegates == null)
                {
                    DietLiquidPatientTimeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DietLiquidPatientTimeMetadata.meta == null)
                {
                    DietLiquidPatientTimeMetadata.meta = new DietLiquidPatientTimeMetadata();
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


                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DietTime", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FoodID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Measure", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("AmountOfWater", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Etc", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Total", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "DietLiquidPatientTime";
                meta.Destination = "DietLiquidPatientTime";

                meta.spInsert = "proc_DietLiquidPatientTimeInsert";
                meta.spUpdate = "proc_DietLiquidPatientTimeUpdate";
                meta.spDelete = "proc_DietLiquidPatientTimeDelete";
                meta.spLoadAll = "proc_DietLiquidPatientTimeLoadAll";
                meta.spLoadByPrimaryKey = "proc_DietLiquidPatientTimeLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DietLiquidPatientTimeMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
