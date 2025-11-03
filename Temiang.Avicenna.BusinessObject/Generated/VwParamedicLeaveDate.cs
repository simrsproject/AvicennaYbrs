/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/28/2011 10:05:17 AM
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
    abstract public class esVwParamedicLeaveDateCollection : esEntityCollectionWAuditLog
    {
        public esVwParamedicLeaveDateCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "VwParamedicLeaveDateCollection";
        }

        #region Query Logic
        protected void InitQuery(esVwParamedicLeaveDateQuery query)
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
            this.InitQuery(query as esVwParamedicLeaveDateQuery);
        }
        #endregion

        virtual public VwParamedicLeaveDate DetachEntity(VwParamedicLeaveDate entity)
        {
            return base.DetachEntity(entity) as VwParamedicLeaveDate;
        }

        virtual public VwParamedicLeaveDate AttachEntity(VwParamedicLeaveDate entity)
        {
            return base.AttachEntity(entity) as VwParamedicLeaveDate;
        }

        virtual public void Combine(VwParamedicLeaveDateCollection collection)
        {
            base.Combine(collection);
        }

        new public VwParamedicLeaveDate this[int index]
        {
            get
            {
                return base[index] as VwParamedicLeaveDate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(VwParamedicLeaveDate);
        }
    }



    [Serializable]
    abstract public class esVwParamedicLeaveDate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esVwParamedicLeaveDateQuery GetDynamicQuery()
        {
            return null;
        }

        public esVwParamedicLeaveDate()
        {

        }

        public esVwParamedicLeaveDate(DataRow row)
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
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "LeaveDate": this.str.LeaveDate = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "LeaveDate":

                            if (value == null || value is System.DateTime)
                                this.LeaveDate = (System.DateTime?)value;
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
        /// Maps to vw_ParamedicLeaveDate.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(VwParamedicLeaveDateMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(VwParamedicLeaveDateMetadata.ColumnNames.ParamedicID, value);
            }
        }

        /// <summary>
        /// Maps to vw_ParamedicLeaveDate.LeaveDate
        /// </summary>
        virtual public System.DateTime? LeaveDate
        {
            get
            {
                return base.GetSystemDateTime(VwParamedicLeaveDateMetadata.ColumnNames.LeaveDate);
            }

            set
            {
                base.SetSystemDateTime(VwParamedicLeaveDateMetadata.ColumnNames.LeaveDate, value);
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
            public esStrings(esVwParamedicLeaveDate entity)
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

            public System.String LeaveDate
            {
                get
                {
                    System.DateTime? data = entity.LeaveDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LeaveDate = null;
                    else entity.LeaveDate = Convert.ToDateTime(value);
                }
            }


            private esVwParamedicLeaveDate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esVwParamedicLeaveDateQuery query)
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
                throw new Exception("esVwParamedicLeaveDate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esVwParamedicLeaveDateQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return VwParamedicLeaveDateMetadata.Meta();
            }
        }


        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, VwParamedicLeaveDateMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem LeaveDate
        {
            get
            {
                return new esQueryItem(this, VwParamedicLeaveDateMetadata.ColumnNames.LeaveDate, esSystemType.DateTime);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("VwParamedicLeaveDateCollection")]
    public partial class VwParamedicLeaveDateCollection : esVwParamedicLeaveDateCollection, IEnumerable<VwParamedicLeaveDate>
    {
        public VwParamedicLeaveDateCollection()
        {

        }

        public static implicit operator List<VwParamedicLeaveDate>(VwParamedicLeaveDateCollection coll)
        {
            List<VwParamedicLeaveDate> list = new List<VwParamedicLeaveDate>();

            foreach (VwParamedicLeaveDate emp in coll)
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
                return VwParamedicLeaveDateMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwParamedicLeaveDateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new VwParamedicLeaveDate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new VwParamedicLeaveDate();
        }


        override public bool LoadAll()
        {
            return base.LoadAll(esSqlAccessType.DynamicSQL);
        }

        #endregion


        [BrowsableAttribute(false)]
        public VwParamedicLeaveDateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwParamedicLeaveDateQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(VwParamedicLeaveDateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public VwParamedicLeaveDate AddNew()
        {
            VwParamedicLeaveDate entity = base.AddNewEntity() as VwParamedicLeaveDate;

            return entity;
        }


        #region IEnumerable<VwParamedicLeaveDate> Members

        IEnumerator<VwParamedicLeaveDate> IEnumerable<VwParamedicLeaveDate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as VwParamedicLeaveDate;
            }
        }

        #endregion

        private VwParamedicLeaveDateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'vw_ParamedicLeaveDate' view
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("VwParamedicLeaveDate ()")]
    [Serializable]
    public partial class VwParamedicLeaveDate : esVwParamedicLeaveDate
    {
        public VwParamedicLeaveDate()
        {

        }

        public VwParamedicLeaveDate(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return VwParamedicLeaveDateMetadata.Meta();
            }
        }



        override protected esVwParamedicLeaveDateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwParamedicLeaveDateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public VwParamedicLeaveDateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwParamedicLeaveDateQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(VwParamedicLeaveDateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private VwParamedicLeaveDateQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class VwParamedicLeaveDateQuery : esVwParamedicLeaveDateQuery
    {
        public VwParamedicLeaveDateQuery()
        {

        }

        public VwParamedicLeaveDateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "VwParamedicLeaveDateQuery";
        }


    }


    [Serializable]
    public partial class VwParamedicLeaveDateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected VwParamedicLeaveDateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(VwParamedicLeaveDateMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = VwParamedicLeaveDateMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(VwParamedicLeaveDateMetadata.ColumnNames.LeaveDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = VwParamedicLeaveDateMetadata.PropertyNames.LeaveDate;
            _columns.Add(c);

        }
        #endregion

        static public VwParamedicLeaveDateMetadata Meta()
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
            public const string LeaveDate = "LeaveDate";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ParamedicID = "ParamedicID";
            public const string LeaveDate = "LeaveDate";
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
            lock (typeof(VwParamedicLeaveDateMetadata))
            {
                if (VwParamedicLeaveDateMetadata.mapDelegates == null)
                {
                    VwParamedicLeaveDateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (VwParamedicLeaveDateMetadata.meta == null)
                {
                    VwParamedicLeaveDateMetadata.meta = new VwParamedicLeaveDateMetadata();
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
                meta.AddTypeMap("LeaveDate", new esTypeMap("datetime", "System.DateTime"));



                meta.Source = "vw_ParamedicLeaveDate";
                meta.Destination = "vw_ParamedicLeaveDate";

                meta.spInsert = "proc_vw_ParamedicLeaveDateInsert";
                meta.spUpdate = "proc_vw_ParamedicLeaveDateUpdate";
                meta.spDelete = "proc_vw_ParamedicLeaveDateDelete";
                meta.spLoadAll = "proc_vw_ParamedicLeaveDateLoadAll";
                meta.spLoadByPrimaryKey = "proc_vw_ParamedicLeaveDateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private VwParamedicLeaveDateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
