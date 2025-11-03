/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/19/2019 11:08:06 PM
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
    abstract public class esHL7MessageCollection : esEntityCollectionWAuditLog
    {
        public esHL7MessageCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "HL7MessageCollection";
        }

        #region Query Logic
        protected void InitQuery(esHL7MessageQuery query)
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
            this.InitQuery(query as esHL7MessageQuery);
        }
        #endregion

        virtual public HL7Message DetachEntity(HL7Message entity)
        {
            return base.DetachEntity(entity) as HL7Message;
        }

        virtual public HL7Message AttachEntity(HL7Message entity)
        {
            return base.AttachEntity(entity) as HL7Message;
        }

        virtual public void Combine(HL7MessageCollection collection)
        {
            base.Combine(collection);
        }

        new public HL7Message this[int index]
        {
            get
            {
                return base[index] as HL7Message;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(HL7Message);
        }
    }



    [Serializable]
    abstract public class esHL7Message : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esHL7MessageQuery GetDynamicQuery()
        {
            return null;
        }

        public esHL7Message()
        {

        }

        public esHL7Message(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Guid messageID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(messageID);
            else
                return LoadByPrimaryKeyStoredProcedure(messageID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Guid messageID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(messageID);
            else
                return LoadByPrimaryKeyStoredProcedure(messageID);
        }

        private bool LoadByPrimaryKeyDynamic(System.Guid messageID)
        {
            esHL7MessageQuery query = this.GetDynamicQuery();
            query.Where(query.MessageID == messageID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Guid messageID)
        {
            esParameters parms = new esParameters();
            parms.Add("MessageID", messageID);
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
                        case "MessageID": this.str.MessageID = (string)value; break;
                        case "Message": this.str.Message = (string)value; break;
                        case "MessageDateTime": this.str.MessageDateTime = (string)value; break;
                        case "MessageFileName": this.str.MessageFileName = (string)value; break;
                        case "SRItemType": this.str.SRItemType = (string)value; break;
                        case "RefferenceNo": this.str.RefferenceNo = (string)value; break;
                        case "Remarks1": this.str.Remarks1 = (string)value; break;
                        case "Remarks2": this.str.Remarks2 = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "MessageID":

                            if (value == null || value is System.Guid)
                                this.MessageID = (System.Guid?)value;
                            break;

                        case "MessageDateTime":

                            if (value == null || value is System.DateTime)
                                this.MessageDateTime = (System.DateTime?)value;
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
        /// Maps to HL7Message.MessageID
        /// </summary>
        virtual public System.Guid? MessageID
        {
            get
            {
                return base.GetSystemGuid(HL7MessageMetadata.ColumnNames.MessageID);
            }

            set
            {
                base.SetSystemGuid(HL7MessageMetadata.ColumnNames.MessageID, value);
            }
        }

        /// <summary>
        /// Maps to HL7Message.Message
        /// </summary>
        virtual public System.String Message
        {
            get
            {
                return base.GetSystemString(HL7MessageMetadata.ColumnNames.Message);
            }

            set
            {
                base.SetSystemString(HL7MessageMetadata.ColumnNames.Message, value);
            }
        }

        /// <summary>
        /// Maps to HL7Message.MessageDateTime
        /// </summary>
        virtual public System.DateTime? MessageDateTime
        {
            get
            {
                return base.GetSystemDateTime(HL7MessageMetadata.ColumnNames.MessageDateTime);
            }

            set
            {
                base.SetSystemDateTime(HL7MessageMetadata.ColumnNames.MessageDateTime, value);
            }
        }

        /// <summary>
        /// Maps to HL7Message.MessageFileName
        /// </summary>
        virtual public System.String MessageFileName
        {
            get
            {
                return base.GetSystemString(HL7MessageMetadata.ColumnNames.MessageFileName);
            }

            set
            {
                base.SetSystemString(HL7MessageMetadata.ColumnNames.MessageFileName, value);
            }
        }

        /// <summary>
        /// Maps to HL7Message.SRItemType
        /// </summary>
        virtual public System.String SRItemType
        {
            get
            {
                return base.GetSystemString(HL7MessageMetadata.ColumnNames.SRItemType);
            }

            set
            {
                base.SetSystemString(HL7MessageMetadata.ColumnNames.SRItemType, value);
            }
        }

        /// <summary>
        /// Maps to HL7Message.RefferenceNo
        /// </summary>
        virtual public System.String RefferenceNo
        {
            get
            {
                return base.GetSystemString(HL7MessageMetadata.ColumnNames.RefferenceNo);
            }

            set
            {
                base.SetSystemString(HL7MessageMetadata.ColumnNames.RefferenceNo, value);
            }
        }

        /// <summary>
        /// Maps to HL7Message.Remarks1
        /// </summary>
        virtual public System.String Remarks1
        {
            get
            {
                return base.GetSystemString(HL7MessageMetadata.ColumnNames.Remarks1);
            }

            set
            {
                base.SetSystemString(HL7MessageMetadata.ColumnNames.Remarks1, value);
            }
        }

        /// <summary>
        /// Maps to HL7Message.Remarks2
        /// </summary>
        virtual public System.String Remarks2
        {
            get
            {
                return base.GetSystemString(HL7MessageMetadata.ColumnNames.Remarks2);
            }

            set
            {
                base.SetSystemString(HL7MessageMetadata.ColumnNames.Remarks2, value);
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
            public esStrings(esHL7Message entity)
            {
                this.entity = entity;
            }


            public System.String MessageID
            {
                get
                {
                    System.Guid? data = entity.MessageID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MessageID = null;
                    else entity.MessageID = new Guid(value);
                }
            }

            public System.String Message
            {
                get
                {
                    System.String data = entity.Message;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Message = null;
                    else entity.Message = Convert.ToString(value);
                }
            }

            public System.String MessageDateTime
            {
                get
                {
                    System.DateTime? data = entity.MessageDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MessageDateTime = null;
                    else entity.MessageDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String MessageFileName
            {
                get
                {
                    System.String data = entity.MessageFileName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MessageFileName = null;
                    else entity.MessageFileName = Convert.ToString(value);
                }
            }

            public System.String SRItemType
            {
                get
                {
                    System.String data = entity.SRItemType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRItemType = null;
                    else entity.SRItemType = Convert.ToString(value);
                }
            }

            public System.String RefferenceNo
            {
                get
                {
                    System.String data = entity.RefferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RefferenceNo = null;
                    else entity.RefferenceNo = Convert.ToString(value);
                }
            }

            public System.String Remarks1
            {
                get
                {
                    System.String data = entity.Remarks1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Remarks1 = null;
                    else entity.Remarks1 = Convert.ToString(value);
                }
            }

            public System.String Remarks2
            {
                get
                {
                    System.String data = entity.Remarks2;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Remarks2 = null;
                    else entity.Remarks2 = Convert.ToString(value);
                }
            }


            private esHL7Message entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esHL7MessageQuery query)
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
                throw new Exception("esHL7Message can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esHL7MessageQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return HL7MessageMetadata.Meta();
            }
        }


        public esQueryItem MessageID
        {
            get
            {
                return new esQueryItem(this, HL7MessageMetadata.ColumnNames.MessageID, esSystemType.Guid);
            }
        }

        public esQueryItem Message
        {
            get
            {
                return new esQueryItem(this, HL7MessageMetadata.ColumnNames.Message, esSystemType.String);
            }
        }

        public esQueryItem MessageDateTime
        {
            get
            {
                return new esQueryItem(this, HL7MessageMetadata.ColumnNames.MessageDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem MessageFileName
        {
            get
            {
                return new esQueryItem(this, HL7MessageMetadata.ColumnNames.MessageFileName, esSystemType.String);
            }
        }

        public esQueryItem SRItemType
        {
            get
            {
                return new esQueryItem(this, HL7MessageMetadata.ColumnNames.SRItemType, esSystemType.String);
            }
        }

        public esQueryItem RefferenceNo
        {
            get
            {
                return new esQueryItem(this, HL7MessageMetadata.ColumnNames.RefferenceNo, esSystemType.String);
            }
        }

        public esQueryItem Remarks1
        {
            get
            {
                return new esQueryItem(this, HL7MessageMetadata.ColumnNames.Remarks1, esSystemType.String);
            }
        }

        public esQueryItem Remarks2
        {
            get
            {
                return new esQueryItem(this, HL7MessageMetadata.ColumnNames.Remarks2, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("HL7MessageCollection")]
    public partial class HL7MessageCollection : esHL7MessageCollection, IEnumerable<HL7Message>
    {
        public HL7MessageCollection()
        {

        }

        public static implicit operator List<HL7Message>(HL7MessageCollection coll)
        {
            List<HL7Message> list = new List<HL7Message>();

            foreach (HL7Message emp in coll)
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
                return HL7MessageMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new HL7MessageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new HL7Message(row);
        }

        override protected esEntity CreateEntity()
        {
            return new HL7Message();
        }


        #endregion


        [BrowsableAttribute(false)]
        public HL7MessageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new HL7MessageQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(HL7MessageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public HL7Message AddNew()
        {
            HL7Message entity = base.AddNewEntity() as HL7Message;

            return entity;
        }

        public HL7Message FindByPrimaryKey(System.Guid messageID)
        {
            return base.FindByPrimaryKey(messageID) as HL7Message;
        }


        #region IEnumerable<HL7Message> Members

        IEnumerator<HL7Message> IEnumerable<HL7Message>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as HL7Message;
            }
        }

        #endregion

        private HL7MessageQuery query;
    }


    /// <summary>
    /// Encapsulates the 'HL7Message' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("HL7Message ({MessageID})")]
    [Serializable]
    public partial class HL7Message : esHL7Message
    {
        public HL7Message()
        {

        }

        public HL7Message(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return HL7MessageMetadata.Meta();
            }
        }



        override protected esHL7MessageQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new HL7MessageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public HL7MessageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new HL7MessageQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(HL7MessageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private HL7MessageQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class HL7MessageQuery : esHL7MessageQuery
    {
        public HL7MessageQuery()
        {

        }

        public HL7MessageQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "HL7MessageQuery";
        }


    }


    [Serializable]
    public partial class HL7MessageMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected HL7MessageMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(HL7MessageMetadata.ColumnNames.MessageID, 0, typeof(System.Guid), esSystemType.Guid);
            c.PropertyName = HL7MessageMetadata.PropertyNames.MessageID;
            c.IsInPrimaryKey = true;
            c.HasDefault = true;
            c.Default = @"(newid())";
            _columns.Add(c);

            c = new esColumnMetadata(HL7MessageMetadata.ColumnNames.Message, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = HL7MessageMetadata.PropertyNames.Message;
            c.CharacterMaxLength = 1073741823;
            _columns.Add(c);

            c = new esColumnMetadata(HL7MessageMetadata.ColumnNames.MessageDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = HL7MessageMetadata.PropertyNames.MessageDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(HL7MessageMetadata.ColumnNames.MessageFileName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = HL7MessageMetadata.PropertyNames.MessageFileName;
            c.CharacterMaxLength = 1073741823;
            _columns.Add(c);

            c = new esColumnMetadata(HL7MessageMetadata.ColumnNames.SRItemType, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = HL7MessageMetadata.PropertyNames.SRItemType;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HL7MessageMetadata.ColumnNames.RefferenceNo, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = HL7MessageMetadata.PropertyNames.RefferenceNo;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HL7MessageMetadata.ColumnNames.Remarks1, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = HL7MessageMetadata.PropertyNames.Remarks1;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(HL7MessageMetadata.ColumnNames.Remarks2, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = HL7MessageMetadata.PropertyNames.Remarks2;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public HL7MessageMetadata Meta()
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
            public const string MessageID = "MessageID";
            public const string Message = "Message";
            public const string MessageDateTime = "MessageDateTime";
            public const string MessageFileName = "MessageFileName";
            public const string SRItemType = "SRItemType";
            public const string RefferenceNo = "RefferenceNo";
            public const string Remarks1 = "Remarks1";
            public const string Remarks2 = "Remarks2";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string MessageID = "MessageID";
            public const string Message = "Message";
            public const string MessageDateTime = "MessageDateTime";
            public const string MessageFileName = "MessageFileName";
            public const string SRItemType = "SRItemType";
            public const string RefferenceNo = "RefferenceNo";
            public const string Remarks1 = "Remarks1";
            public const string Remarks2 = "Remarks2";
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
            lock (typeof(HL7MessageMetadata))
            {
                if (HL7MessageMetadata.mapDelegates == null)
                {
                    HL7MessageMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (HL7MessageMetadata.meta == null)
                {
                    HL7MessageMetadata.meta = new HL7MessageMetadata();
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


                meta.AddTypeMap("MessageID", new esTypeMap("uniqueidentifier", "System.Guid"));
                meta.AddTypeMap("Message", new esTypeMap("nvarchar", "System.String"));
                meta.AddTypeMap("MessageDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("MessageFileName", new esTypeMap("nvarchar", "System.String"));
                meta.AddTypeMap("SRItemType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RefferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Remarks1", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Remarks2", new esTypeMap("varchar", "System.String"));



                meta.Source = "HL7Message";
                meta.Destination = "HL7Message";

                meta.spInsert = "proc_HL7MessageInsert";
                meta.spUpdate = "proc_HL7MessageUpdate";
                meta.spDelete = "proc_HL7MessageDelete";
                meta.spLoadAll = "proc_HL7MessageLoadAll";
                meta.spLoadByPrimaryKey = "proc_HL7MessageLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private HL7MessageMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
