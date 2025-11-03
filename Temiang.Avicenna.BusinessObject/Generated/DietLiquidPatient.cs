/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/13/2015 11:23:12 AM
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
    abstract public class esDietLiquidPatientCollection : esEntityCollectionWAuditLog
    {
        public esDietLiquidPatientCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "DietLiquidPatientCollection";
        }

        #region Query Logic
        protected void InitQuery(esDietLiquidPatientQuery query)
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
            this.InitQuery(query as esDietLiquidPatientQuery);
        }
        #endregion

        virtual public DietLiquidPatient DetachEntity(DietLiquidPatient entity)
        {
            return base.DetachEntity(entity) as DietLiquidPatient;
        }

        virtual public DietLiquidPatient AttachEntity(DietLiquidPatient entity)
        {
            return base.AttachEntity(entity) as DietLiquidPatient;
        }

        virtual public void Combine(DietLiquidPatientCollection collection)
        {
            base.Combine(collection);
        }

        new public DietLiquidPatient this[int index]
        {
            get
            {
                return base[index] as DietLiquidPatient;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(DietLiquidPatient);
        }
    }



    [Serializable]
    abstract public class esDietLiquidPatient : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDietLiquidPatientQuery GetDynamicQuery()
        {
            return null;
        }

        public esDietLiquidPatient()
        {

        }

        public esDietLiquidPatient(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String transactionNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(transactionNo);
            else
                return LoadByPrimaryKeyStoredProcedure(transactionNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
        {
            esDietLiquidPatientQuery query = this.GetDynamicQuery();
            query.Where(query.TransactionNo == transactionNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
        {
            esParameters parms = new esParameters();
            parms.Add("TransactionNo", transactionNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "EffectiveStartDate": this.str.EffectiveStartDate = (string)value; break;
                        case "EffectiveStartTime": this.str.EffectiveStartTime = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
                        case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "EffectiveStartDate":

                            if (value == null || value is System.DateTime)
                                this.EffectiveStartDate = (System.DateTime?)value;
                            break;

                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
                            break;

                        case "VoidDateTime":

                            if (value == null || value is System.DateTime)
                                this.VoidDateTime = (System.DateTime?)value;
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
        /// Maps to DietLiquidPatient.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.EffectiveStartDate
        /// </summary>
        virtual public System.DateTime? EffectiveStartDate
        {
            get
            {
                return base.GetSystemDateTime(DietLiquidPatientMetadata.ColumnNames.EffectiveStartDate);
            }

            set
            {
                base.SetSystemDateTime(DietLiquidPatientMetadata.ColumnNames.EffectiveStartDate, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.EffectiveStartTime
        /// </summary>
        virtual public System.String EffectiveStartTime
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientMetadata.ColumnNames.EffectiveStartTime);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientMetadata.ColumnNames.EffectiveStartTime, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(DietLiquidPatientMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(DietLiquidPatientMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.VoidDateTime
        /// </summary>
        virtual public System.DateTime? VoidDateTime
        {
            get
            {
                return base.GetSystemDateTime(DietLiquidPatientMetadata.ColumnNames.VoidDateTime);
            }

            set
            {
                base.SetSystemDateTime(DietLiquidPatientMetadata.ColumnNames.VoidDateTime, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.VoidByUserID
        /// </summary>
        virtual public System.String VoidByUserID
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientMetadata.ColumnNames.VoidByUserID);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientMetadata.ColumnNames.VoidByUserID, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DietLiquidPatientMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DietLiquidPatientMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to DietLiquidPatient.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DietLiquidPatientMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DietLiquidPatientMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esDietLiquidPatient entity)
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

            public System.String EffectiveStartDate
            {
                get
                {
                    System.DateTime? data = entity.EffectiveStartDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EffectiveStartDate = null;
                    else entity.EffectiveStartDate = Convert.ToDateTime(value);
                }
            }

            public System.String EffectiveStartTime
            {
                get
                {
                    System.String data = entity.EffectiveStartTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EffectiveStartTime = null;
                    else entity.EffectiveStartTime = Convert.ToString(value);
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

            public System.String VoidDateTime
            {
                get
                {
                    System.DateTime? data = entity.VoidDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidDateTime = null;
                    else entity.VoidDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String VoidByUserID
            {
                get
                {
                    System.String data = entity.VoidByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VoidByUserID = null;
                    else entity.VoidByUserID = Convert.ToString(value);
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


            private esDietLiquidPatient entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDietLiquidPatientQuery query)
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
                throw new Exception("esDietLiquidPatient can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class DietLiquidPatient : esDietLiquidPatient
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
    abstract public class esDietLiquidPatientQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return DietLiquidPatientMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem EffectiveStartDate
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.EffectiveStartDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EffectiveStartTime
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.EffectiveStartTime, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem VoidDateTime
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem VoidByUserID
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.VoidByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DietLiquidPatientMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DietLiquidPatientCollection")]
    public partial class DietLiquidPatientCollection : esDietLiquidPatientCollection, IEnumerable<DietLiquidPatient>
    {
        public DietLiquidPatientCollection()
        {

        }

        public static implicit operator List<DietLiquidPatient>(DietLiquidPatientCollection coll)
        {
            List<DietLiquidPatient> list = new List<DietLiquidPatient>();

            foreach (DietLiquidPatient emp in coll)
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
                return DietLiquidPatientMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietLiquidPatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new DietLiquidPatient(row);
        }

        override protected esEntity CreateEntity()
        {
            return new DietLiquidPatient();
        }


        #endregion


        [BrowsableAttribute(false)]
        public DietLiquidPatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietLiquidPatientQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(DietLiquidPatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public DietLiquidPatient AddNew()
        {
            DietLiquidPatient entity = base.AddNewEntity() as DietLiquidPatient;

            return entity;
        }

        public DietLiquidPatient FindByPrimaryKey(System.String transactionNo)
        {
            return base.FindByPrimaryKey(transactionNo) as DietLiquidPatient;
        }


        #region IEnumerable<DietLiquidPatient> Members

        IEnumerator<DietLiquidPatient> IEnumerable<DietLiquidPatient>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as DietLiquidPatient;
            }
        }

        #endregion

        private DietLiquidPatientQuery query;
    }


    /// <summary>
    /// Encapsulates the 'DietLiquidPatient' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("DietLiquidPatient ({TransactionNo})")]
    [Serializable]
    public partial class DietLiquidPatient : esDietLiquidPatient
    {
        public DietLiquidPatient()
        {

        }

        public DietLiquidPatient(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DietLiquidPatientMetadata.Meta();
            }
        }



        override protected esDietLiquidPatientQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietLiquidPatientQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public DietLiquidPatientQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietLiquidPatientQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(DietLiquidPatientQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DietLiquidPatientQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class DietLiquidPatientQuery : esDietLiquidPatientQuery
    {
        public DietLiquidPatientQuery()
        {

        }

        public DietLiquidPatientQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DietLiquidPatientQuery";
        }


    }


    [Serializable]
    public partial class DietLiquidPatientMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DietLiquidPatientMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.TransactionNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.EffectiveStartDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.EffectiveStartDate;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.EffectiveStartTime, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.EffectiveStartTime;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.IsVoid, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.VoidDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.VoidDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.VoidByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.VoidByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietLiquidPatientMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = DietLiquidPatientMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public DietLiquidPatientMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string EffectiveStartDate = "EffectiveStartDate";
            public const string EffectiveStartTime = "EffectiveStartTime";
            public const string Notes = "Notes";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string EffectiveStartDate = "EffectiveStartDate";
            public const string EffectiveStartTime = "EffectiveStartTime";
            public const string Notes = "Notes";
            public const string IsVoid = "IsVoid";
            public const string VoidDateTime = "VoidDateTime";
            public const string VoidByUserID = "VoidByUserID";
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
            lock (typeof(DietLiquidPatientMetadata))
            {
                if (DietLiquidPatientMetadata.mapDelegates == null)
                {
                    DietLiquidPatientMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DietLiquidPatientMetadata.meta == null)
                {
                    DietLiquidPatientMetadata.meta = new DietLiquidPatientMetadata();
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
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("EffectiveStartDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("EffectiveStartTime", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "DietLiquidPatient";
                meta.Destination = "DietLiquidPatient";

                meta.spInsert = "proc_DietLiquidPatientInsert";
                meta.spUpdate = "proc_DietLiquidPatientUpdate";
                meta.spDelete = "proc_DietLiquidPatientDelete";
                meta.spLoadAll = "proc_DietLiquidPatientLoadAll";
                meta.spLoadByPrimaryKey = "proc_DietLiquidPatientLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DietLiquidPatientMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
