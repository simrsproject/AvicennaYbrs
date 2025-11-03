/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/25/2013 1:08:57 PM
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
    abstract public class esVwRegistrationActiveCollection : esEntityCollectionWAuditLog
    {
        public esVwRegistrationActiveCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "VwRegistrationActiveCollection";
        }

        #region Query Logic
        protected void InitQuery(esVwRegistrationActiveQuery query)
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
            this.InitQuery(query as esVwRegistrationActiveQuery);
        }
        #endregion

        virtual public VwRegistrationActive DetachEntity(VwRegistrationActive entity)
        {
            return base.DetachEntity(entity) as VwRegistrationActive;
        }

        virtual public VwRegistrationActive AttachEntity(VwRegistrationActive entity)
        {
            return base.AttachEntity(entity) as VwRegistrationActive;
        }

        virtual public void Combine(VwRegistrationActiveCollection collection)
        {
            base.Combine(collection);
        }

        new public VwRegistrationActive this[int index]
        {
            get
            {
                return base[index] as VwRegistrationActive;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(VwRegistrationActive);
        }
    }



    [Serializable]
    abstract public class esVwRegistrationActive : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esVwRegistrationActiveQuery GetDynamicQuery()
        {
            return null;
        }

        public esVwRegistrationActive()
        {

        }

        public esVwRegistrationActive(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey

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
                        case "RegistrationDate": this.str.RegistrationDate = (string)value; break;
                        case "RegistrationTime": this.str.RegistrationTime = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "RoomID": this.str.RoomID = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "GuarantorID": this.str.GuarantorID = (string)value; break;
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "BedID": this.str.BedID = (string)value; break;
                        case "IsTransferedToInpatient": this.str.IsTransferedToInpatient = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RegistrationDate":

                            if (value == null || value is System.DateTime)
                                this.RegistrationDate = (System.DateTime?)value;
                            break;

                        case "IsTransferedToInpatient":

                            if (value == null || value is System.Boolean)
                                this.IsTransferedToInpatient = (System.Boolean?)value;
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
        /// Maps to vw_RegistrationActive.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.RegistrationDate
        /// </summary>
        virtual public System.DateTime? RegistrationDate
        {
            get
            {
                return base.GetSystemDateTime(VwRegistrationActiveMetadata.ColumnNames.RegistrationDate);
            }

            set
            {
                base.SetSystemDateTime(VwRegistrationActiveMetadata.ColumnNames.RegistrationDate, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.RegistrationTime
        /// </summary>
        virtual public System.String RegistrationTime
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.RegistrationTime);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.RegistrationTime, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.RoomID
        /// </summary>
        virtual public System.String RoomID
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.RoomID);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.RoomID, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.ParamedicID, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.GuarantorID
        /// </summary>
        virtual public System.String GuarantorID
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.GuarantorID);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.GuarantorID, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.PatientID, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.BedID
        /// </summary>
        virtual public System.String BedID
        {
            get
            {
                return base.GetSystemString(VwRegistrationActiveMetadata.ColumnNames.BedID);
            }

            set
            {
                base.SetSystemString(VwRegistrationActiveMetadata.ColumnNames.BedID, value);
            }
        }

        /// <summary>
        /// Maps to vw_RegistrationActive.IsTransferedToInpatient
        /// </summary>
        virtual public System.Boolean? IsTransferedToInpatient
        {
            get
            {
                return base.GetSystemBoolean(VwRegistrationActiveMetadata.ColumnNames.IsTransferedToInpatient);
            }

            set
            {
                base.SetSystemBoolean(VwRegistrationActiveMetadata.ColumnNames.IsTransferedToInpatient, value);
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
            public esStrings(esVwRegistrationActive entity)
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

            public System.String RegistrationDate
            {
                get
                {
                    System.DateTime? data = entity.RegistrationDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationDate = null;
                    else entity.RegistrationDate = Convert.ToDateTime(value);
                }
            }

            public System.String RegistrationTime
            {
                get
                {
                    System.String data = entity.RegistrationTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationTime = null;
                    else entity.RegistrationTime = Convert.ToString(value);
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

            public System.String RoomID
            {
                get
                {
                    System.String data = entity.RoomID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomID = null;
                    else entity.RoomID = Convert.ToString(value);
                }
            }

            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
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

            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }

            public System.String BedID
            {
                get
                {
                    System.String data = entity.BedID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BedID = null;
                    else entity.BedID = Convert.ToString(value);
                }
            }

            public System.String IsTransferedToInpatient
            {
                get
                {
                    System.Boolean? data = entity.IsTransferedToInpatient;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsTransferedToInpatient = null;
                    else entity.IsTransferedToInpatient = Convert.ToBoolean(value);
                }
            }


            private esVwRegistrationActive entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esVwRegistrationActiveQuery query)
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
                throw new Exception("esVwRegistrationActive can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esVwRegistrationActiveQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return VwRegistrationActiveMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem RegistrationDate
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.RegistrationDate, esSystemType.DateTime);
            }
        }

        public esQueryItem RegistrationTime
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.RegistrationTime, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem RoomID
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.RoomID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem GuarantorID
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.GuarantorID, esSystemType.String);
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem BedID
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.BedID, esSystemType.String);
            }
        }

        public esQueryItem IsTransferedToInpatient
        {
            get
            {
                return new esQueryItem(this, VwRegistrationActiveMetadata.ColumnNames.IsTransferedToInpatient, esSystemType.Boolean);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("VwRegistrationActiveCollection")]
    public partial class VwRegistrationActiveCollection : esVwRegistrationActiveCollection, IEnumerable<VwRegistrationActive>
    {
        public VwRegistrationActiveCollection()
        {

        }

        public static implicit operator List<VwRegistrationActive>(VwRegistrationActiveCollection coll)
        {
            List<VwRegistrationActive> list = new List<VwRegistrationActive>();

            foreach (VwRegistrationActive emp in coll)
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
                return VwRegistrationActiveMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwRegistrationActiveQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new VwRegistrationActive(row);
        }

        override protected esEntity CreateEntity()
        {
            return new VwRegistrationActive();
        }


        override public bool LoadAll()
        {
            return base.LoadAll(esSqlAccessType.DynamicSQL);
        }

        #endregion


        [BrowsableAttribute(false)]
        public VwRegistrationActiveQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwRegistrationActiveQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(VwRegistrationActiveQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public VwRegistrationActive AddNew()
        {
            VwRegistrationActive entity = base.AddNewEntity() as VwRegistrationActive;

            return entity;
        }


        #region IEnumerable<VwRegistrationActive> Members

        IEnumerator<VwRegistrationActive> IEnumerable<VwRegistrationActive>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as VwRegistrationActive;
            }
        }

        #endregion

        private VwRegistrationActiveQuery query;
    }


    /// <summary>
    /// Encapsulates the 'vw_RegistrationActive' view
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("VwRegistrationActive ()")]
    [Serializable]
    public partial class VwRegistrationActive : esVwRegistrationActive
    {
        public VwRegistrationActive()
        {

        }

        public VwRegistrationActive(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return VwRegistrationActiveMetadata.Meta();
            }
        }



        override protected esVwRegistrationActiveQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwRegistrationActiveQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public VwRegistrationActiveQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwRegistrationActiveQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(VwRegistrationActiveQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private VwRegistrationActiveQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class VwRegistrationActiveQuery : esVwRegistrationActiveQuery
    {
        public VwRegistrationActiveQuery()
        {

        }

        public VwRegistrationActiveQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "VwRegistrationActiveQuery";
        }


    }


    [Serializable]
    public partial class VwRegistrationActiveMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected VwRegistrationActiveMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.RegistrationDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.RegistrationDate;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.RegistrationTime, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.RegistrationTime;
            c.CharacterMaxLength = 5;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.RoomID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.RoomID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.ClassID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.ClassID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.ParamedicID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.GuarantorID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.GuarantorID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.PatientID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.PatientID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.BedID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.BedID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(VwRegistrationActiveMetadata.ColumnNames.IsTransferedToInpatient, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = VwRegistrationActiveMetadata.PropertyNames.IsTransferedToInpatient;
            _columns.Add(c);

        }
        #endregion

        static public VwRegistrationActiveMetadata Meta()
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
            public const string RegistrationDate = "RegistrationDate";
            public const string RegistrationTime = "RegistrationTime";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string RoomID = "RoomID";
            public const string ClassID = "ClassID";
            public const string ParamedicID = "ParamedicID";
            public const string GuarantorID = "GuarantorID";
            public const string PatientID = "PatientID";
            public const string BedID = "BedID";
            public const string IsTransferedToInpatient = "IsTransferedToInpatient";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string RegistrationDate = "RegistrationDate";
            public const string RegistrationTime = "RegistrationTime";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string RoomID = "RoomID";
            public const string ClassID = "ClassID";
            public const string ParamedicID = "ParamedicID";
            public const string GuarantorID = "GuarantorID";
            public const string PatientID = "PatientID";
            public const string BedID = "BedID";
            public const string IsTransferedToInpatient = "IsTransferedToInpatient";
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
            lock (typeof(VwRegistrationActiveMetadata))
            {
                if (VwRegistrationActiveMetadata.mapDelegates == null)
                {
                    VwRegistrationActiveMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (VwRegistrationActiveMetadata.meta == null)
                {
                    VwRegistrationActiveMetadata.meta = new VwRegistrationActiveMetadata();
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
                meta.AddTypeMap("RegistrationDate", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("RegistrationTime", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BedID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsTransferedToInpatient", new esTypeMap("bit", "System.Boolean"));



                meta.Source = "vw_RegistrationActive";
                meta.Destination = "vw_RegistrationActive";

                meta.spInsert = "proc_vw_RegistrationActiveInsert";
                meta.spUpdate = "proc_vw_RegistrationActiveUpdate";
                meta.spDelete = "proc_vw_RegistrationActiveDelete";
                meta.spLoadAll = "proc_vw_RegistrationActiveLoadAll";
                meta.spLoadByPrimaryKey = "proc_vw_RegistrationActiveLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private VwRegistrationActiveMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
