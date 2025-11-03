/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/23/2015 11:07:15 AM
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
    abstract public class esTransPrescriptionFloorSeqNoCollection : esEntityCollectionWAuditLog
    {
        public esTransPrescriptionFloorSeqNoCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "TransPrescriptionFloorSeqNoCollection";
        }

        #region Query Logic
        protected void InitQuery(esTransPrescriptionFloorSeqNoQuery query)
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
            this.InitQuery(query as esTransPrescriptionFloorSeqNoQuery);
        }
        #endregion

        virtual public TransPrescriptionFloorSeqNo DetachEntity(TransPrescriptionFloorSeqNo entity)
        {
            return base.DetachEntity(entity) as TransPrescriptionFloorSeqNo;
        }

        virtual public TransPrescriptionFloorSeqNo AttachEntity(TransPrescriptionFloorSeqNo entity)
        {
            return base.AttachEntity(entity) as TransPrescriptionFloorSeqNo;
        }

        virtual public void Combine(TransPrescriptionFloorSeqNoCollection collection)
        {
            base.Combine(collection);
        }

        new public TransPrescriptionFloorSeqNo this[int index]
        {
            get
            {
                return base[index] as TransPrescriptionFloorSeqNo;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(TransPrescriptionFloorSeqNo);
        }
    }



    [Serializable]
    abstract public class esTransPrescriptionFloorSeqNo : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esTransPrescriptionFloorSeqNoQuery GetDynamicQuery()
        {
            return null;
        }

        public esTransPrescriptionFloorSeqNo()
        {

        }

        public esTransPrescriptionFloorSeqNo(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.DateTime prescriptionDate, System.String sRFloor, System.String serviceUnitID, System.String shiftID, System.String rtype)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionDate, sRFloor, serviceUnitID, shiftID, rtype);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionDate, sRFloor, serviceUnitID, shiftID, rtype);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime prescriptionDate, System.String sRFloor, System.String serviceUnitID, System.String shiftID, System.String rtype)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(prescriptionDate, sRFloor, serviceUnitID, shiftID, rtype);
            else
                return LoadByPrimaryKeyStoredProcedure(prescriptionDate, sRFloor, serviceUnitID, shiftID, rtype);
        }

        private bool LoadByPrimaryKeyDynamic(System.DateTime prescriptionDate, System.String sRFloor, System.String serviceUnitID, System.String shiftID, System.String rtype)
        {
            esTransPrescriptionFloorSeqNoQuery query = this.GetDynamicQuery();
            query.Where(query.PrescriptionDate == prescriptionDate, query.SRFloor == sRFloor, query.ServiceUnitID == serviceUnitID, query.ShiftID == shiftID, query.Rtype == rtype);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.DateTime prescriptionDate, System.String sRFloor, System.String serviceUnitID, System.String shiftID, System.String rtype)
        {
            esParameters parms = new esParameters();
            parms.Add("PrescriptionDate", prescriptionDate); parms.Add("SRFloor", sRFloor); parms.Add("ServiceUnitID", serviceUnitID); parms.Add("ShiftID", shiftID); parms.Add("Rtype", rtype);
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
                        case "PrescriptionDate": this.str.PrescriptionDate = (string)value; break;
                        case "SRFloor": this.str.SRFloor = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ShiftID": this.str.ShiftID = (string)value; break;
                        case "Rtype": this.str.Rtype = (string)value; break;
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "PrescriptionDate":

                            if (value == null || value is System.DateTime)
                                this.PrescriptionDate = (System.DateTime?)value;
                            break;

                        case "SeqNo":

                            if (value == null || value is System.Int32)
                                this.SeqNo = (System.Int32?)value;
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
        /// Maps to TransPrescriptionFloorSeqNo.PrescriptionDate
        /// </summary>
        virtual public System.DateTime? PrescriptionDate
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionFloorSeqNoMetadata.ColumnNames.PrescriptionDate);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionFloorSeqNoMetadata.ColumnNames.PrescriptionDate, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionFloorSeqNo.SRFloor
        /// </summary>
        virtual public System.String SRFloor
        {
            get
            {
                return base.GetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.SRFloor);
            }

            set
            {
                base.SetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.SRFloor, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionFloorSeqNo.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionFloorSeqNo.ShiftID
        /// </summary>
        virtual public System.String ShiftID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.ShiftID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.ShiftID, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionFloorSeqNo.Rtype
        /// </summary>
        virtual public System.String Rtype
        {
            get
            {
                return base.GetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.Rtype);
            }

            set
            {
                base.SetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.Rtype, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionFloorSeqNo.SeqNo
        /// </summary>
        virtual public System.Int32? SeqNo
        {
            get
            {
                return base.GetSystemInt32(TransPrescriptionFloorSeqNoMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemInt32(TransPrescriptionFloorSeqNoMetadata.ColumnNames.SeqNo, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionFloorSeqNo.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(TransPrescriptionFloorSeqNoMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(TransPrescriptionFloorSeqNoMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to TransPrescriptionFloorSeqNo.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(TransPrescriptionFloorSeqNoMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esTransPrescriptionFloorSeqNo entity)
            {
                this.entity = entity;
            }


            public System.String PrescriptionDate
            {
                get
                {
                    System.DateTime? data = entity.PrescriptionDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionDate = null;
                    else entity.PrescriptionDate = Convert.ToDateTime(value);
                }
            }

            public System.String SRFloor
            {
                get
                {
                    System.String data = entity.SRFloor;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRFloor = null;
                    else entity.SRFloor = Convert.ToString(value);
                }
            }

            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }

            public System.String ShiftID
            {
                get
                {
                    System.String data = entity.ShiftID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ShiftID = null;
                    else entity.ShiftID = Convert.ToString(value);
                }
            }

            public System.String Rtype
            {
                get
                {
                    System.String data = entity.Rtype;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Rtype = null;
                    else entity.Rtype = Convert.ToString(value);
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


            private esTransPrescriptionFloorSeqNo entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esTransPrescriptionFloorSeqNoQuery query)
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
                throw new Exception("esTransPrescriptionFloorSeqNo can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class TransPrescriptionFloorSeqNo : esTransPrescriptionFloorSeqNo
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
    abstract public class esTransPrescriptionFloorSeqNoQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionFloorSeqNoMetadata.Meta();
            }
        }


        public esQueryItem PrescriptionDate
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionFloorSeqNoMetadata.ColumnNames.PrescriptionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRFloor
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionFloorSeqNoMetadata.ColumnNames.SRFloor, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionFloorSeqNoMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ShiftID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionFloorSeqNoMetadata.ColumnNames.ShiftID, esSystemType.String);
            }
        }

        public esQueryItem Rtype
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionFloorSeqNoMetadata.ColumnNames.Rtype, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionFloorSeqNoMetadata.ColumnNames.SeqNo, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionFloorSeqNoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, TransPrescriptionFloorSeqNoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("TransPrescriptionFloorSeqNoCollection")]
    public partial class TransPrescriptionFloorSeqNoCollection : esTransPrescriptionFloorSeqNoCollection, IEnumerable<TransPrescriptionFloorSeqNo>
    {
        public TransPrescriptionFloorSeqNoCollection()
        {

        }

        public static implicit operator List<TransPrescriptionFloorSeqNo>(TransPrescriptionFloorSeqNoCollection coll)
        {
            List<TransPrescriptionFloorSeqNo> list = new List<TransPrescriptionFloorSeqNo>();

            foreach (TransPrescriptionFloorSeqNo emp in coll)
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
                return TransPrescriptionFloorSeqNoMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionFloorSeqNoQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new TransPrescriptionFloorSeqNo(row);
        }

        override protected esEntity CreateEntity()
        {
            return new TransPrescriptionFloorSeqNo();
        }


        #endregion


        [BrowsableAttribute(false)]
        public TransPrescriptionFloorSeqNoQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionFloorSeqNoQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(TransPrescriptionFloorSeqNoQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public TransPrescriptionFloorSeqNo AddNew()
        {
            TransPrescriptionFloorSeqNo entity = base.AddNewEntity() as TransPrescriptionFloorSeqNo;

            return entity;
        }

        public TransPrescriptionFloorSeqNo FindByPrimaryKey(System.DateTime prescriptionDate, System.String sRFloor, System.String serviceUnitID, System.String shiftID, System.String rtype)
        {
            return base.FindByPrimaryKey(prescriptionDate, sRFloor, serviceUnitID, shiftID, rtype) as TransPrescriptionFloorSeqNo;
        }


        #region IEnumerable<TransPrescriptionFloorSeqNo> Members

        IEnumerator<TransPrescriptionFloorSeqNo> IEnumerable<TransPrescriptionFloorSeqNo>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as TransPrescriptionFloorSeqNo;
            }
        }

        #endregion

        private TransPrescriptionFloorSeqNoQuery query;
    }


    /// <summary>
    /// Encapsulates the 'TransPrescriptionFloorSeqNo' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPrescriptionFloorSeqNo ({PrescriptionDate},{SRFloor},{ServiceUnitID},{ShiftID},{Rtype})")]
    [Serializable]
    public partial class TransPrescriptionFloorSeqNo : esTransPrescriptionFloorSeqNo
    {
        public TransPrescriptionFloorSeqNo()
        {

        }

        public TransPrescriptionFloorSeqNo(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return TransPrescriptionFloorSeqNoMetadata.Meta();
            }
        }



        override protected esTransPrescriptionFloorSeqNoQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new TransPrescriptionFloorSeqNoQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public TransPrescriptionFloorSeqNoQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new TransPrescriptionFloorSeqNoQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(TransPrescriptionFloorSeqNoQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private TransPrescriptionFloorSeqNoQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class TransPrescriptionFloorSeqNoQuery : esTransPrescriptionFloorSeqNoQuery
    {
        public TransPrescriptionFloorSeqNoQuery()
        {

        }

        public TransPrescriptionFloorSeqNoQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "TransPrescriptionFloorSeqNoQuery";
        }


    }


    [Serializable]
    public partial class TransPrescriptionFloorSeqNoMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected TransPrescriptionFloorSeqNoMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(TransPrescriptionFloorSeqNoMetadata.ColumnNames.PrescriptionDate, 0, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionFloorSeqNoMetadata.PropertyNames.PrescriptionDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionFloorSeqNoMetadata.ColumnNames.SRFloor, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionFloorSeqNoMetadata.PropertyNames.SRFloor;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionFloorSeqNoMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionFloorSeqNoMetadata.PropertyNames.ServiceUnitID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionFloorSeqNoMetadata.ColumnNames.ShiftID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionFloorSeqNoMetadata.PropertyNames.ShiftID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionFloorSeqNoMetadata.ColumnNames.Rtype, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionFloorSeqNoMetadata.PropertyNames.Rtype;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionFloorSeqNoMetadata.ColumnNames.SeqNo, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = TransPrescriptionFloorSeqNoMetadata.PropertyNames.SeqNo;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionFloorSeqNoMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = TransPrescriptionFloorSeqNoMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(TransPrescriptionFloorSeqNoMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = TransPrescriptionFloorSeqNoMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public TransPrescriptionFloorSeqNoMetadata Meta()
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
            public const string PrescriptionDate = "PrescriptionDate";
            public const string SRFloor = "SRFloor";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ShiftID = "ShiftID";
            public const string Rtype = "Rtype";
            public const string SeqNo = "SeqNo";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PrescriptionDate = "PrescriptionDate";
            public const string SRFloor = "SRFloor";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ShiftID = "ShiftID";
            public const string Rtype = "Rtype";
            public const string SeqNo = "SeqNo";
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
            lock (typeof(TransPrescriptionFloorSeqNoMetadata))
            {
                if (TransPrescriptionFloorSeqNoMetadata.mapDelegates == null)
                {
                    TransPrescriptionFloorSeqNoMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (TransPrescriptionFloorSeqNoMetadata.meta == null)
                {
                    TransPrescriptionFloorSeqNoMetadata.meta = new TransPrescriptionFloorSeqNoMetadata();
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


                meta.AddTypeMap("PrescriptionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRFloor", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ShiftID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Rtype", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeqNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "TransPrescriptionFloorSeqNo";
                meta.Destination = "TransPrescriptionFloorSeqNo";

                meta.spInsert = "proc_TransPrescriptionFloorSeqNoInsert";
                meta.spUpdate = "proc_TransPrescriptionFloorSeqNoUpdate";
                meta.spDelete = "proc_TransPrescriptionFloorSeqNoDelete";
                meta.spLoadAll = "proc_TransPrescriptionFloorSeqNoLoadAll";
                meta.spLoadByPrimaryKey = "proc_TransPrescriptionFloorSeqNoLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private TransPrescriptionFloorSeqNoMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
