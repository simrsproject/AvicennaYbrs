/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:23 PM
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
    abstract public class esProcedureCollection : esEntityCollectionWAuditLog
    {
        public esProcedureCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ProcedureCollection";
        }

        #region Query Logic
        protected void InitQuery(esProcedureQuery query)
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
            this.InitQuery(query as esProcedureQuery);
        }
        #endregion

        virtual public Procedure DetachEntity(Procedure entity)
        {
            return base.DetachEntity(entity) as Procedure;
        }

        virtual public Procedure AttachEntity(Procedure entity)
        {
            return base.AttachEntity(entity) as Procedure;
        }

        virtual public void Combine(ProcedureCollection collection)
        {
            base.Combine(collection);
        }

        new public Procedure this[int index]
        {
            get
            {
                return base[index] as Procedure;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Procedure);
        }
    }



    [Serializable]
    abstract public class esProcedure : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esProcedureQuery GetDynamicQuery()
        {
            return null;
        }

        public esProcedure()
        {

        }

        public esProcedure(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String procedureID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(procedureID);
            else
                return LoadByPrimaryKeyStoredProcedure(procedureID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String procedureID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(procedureID);
            else
                return LoadByPrimaryKeyStoredProcedure(procedureID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String procedureID)
        {
            esProcedureQuery query = this.GetDynamicQuery();
            query.Where(query.ProcedureID == procedureID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String procedureID)
        {
            esParameters parms = new esParameters();
            parms.Add("ProcedureID", procedureID);
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
                        case "ProcedureID": this.str.ProcedureID = (string)value; break;
                        case "ProcedureName": this.str.ProcedureName = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IM": this.str.IM = (string)value; break;
                        case "ValidCode": this.str.ValidCode = (string)value; break;
                        case "Asterisk": this.str.Asterisk = (string)value; break;
                        case "Accpdx": this.str.Asterisk = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IM":

                            if (value == null || value is System.Boolean)
                                this.IM = (System.Boolean?)value;
                            break;
                        case "ValidCode":

                            if (value == null || value is System.Boolean)
                                this.ValidCode = (System.Boolean?)value;
                            break;
                        case "Asterisk":

                            if (value == null || value is System.Boolean)
                                this.Asterisk = (System.Boolean?)value;
                            break;
                        case "Accpdx":

                            if (value == null || value is System.Boolean)
                                this.Asterisk = (System.Boolean?)value;
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
        /// Maps to Procedure.ProcedureID
        /// </summary>
        virtual public System.String ProcedureID
        {
            get
            {
                return base.GetSystemString(ProcedureMetadata.ColumnNames.ProcedureID);
            }

            set
            {
                base.SetSystemString(ProcedureMetadata.ColumnNames.ProcedureID, value);
            }
        }

        /// <summary>
        /// Maps to Procedure.ProcedureName
        /// </summary>
        virtual public System.String ProcedureName
        {
            get
            {
                return base.GetSystemString(ProcedureMetadata.ColumnNames.ProcedureName);
            }

            set
            {
                base.SetSystemString(ProcedureMetadata.ColumnNames.ProcedureName, value);
            }
        }

        /// <summary>
        /// Maps to Procedure.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ProcedureMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ProcedureMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to Procedure.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ProcedureMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ProcedureMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.IM
        /// </summary>
        virtual public System.Boolean? IM
        {
            get
            {
                return base.GetSystemBoolean(ProcedureMetadata.ColumnNames.IM);
            }

            set
            {
                base.SetSystemBoolean(ProcedureMetadata.ColumnNames.IM, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.ValidCode
        /// </summary>
        virtual public System.Boolean? ValidCode
        {
            get
            {
                return base.GetSystemBoolean(ProcedureMetadata.ColumnNames.ValidCode);
            }

            set
            {
                base.SetSystemBoolean(ProcedureMetadata.ColumnNames.ValidCode, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.Asterisk
        /// </summary>
        virtual public System.Boolean? Asterisk
        {
            get
            {
                return base.GetSystemBoolean(ProcedureMetadata.ColumnNames.Asterisk);
            }

            set
            {
                base.SetSystemBoolean(ProcedureMetadata.ColumnNames.Asterisk, value);
            }
        }
        /// <summary>
        /// Maps to Diagnose.Accpdx
        /// </summary>
        virtual public System.Boolean? Accpdx
        {
            get
            {
                return base.GetSystemBoolean(ProcedureMetadata.ColumnNames.Accpdx);
            }

            set
            {
                base.SetSystemBoolean(ProcedureMetadata.ColumnNames.Accpdx, value);
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
            public esStrings(esProcedure entity)
            {
                this.entity = entity;
            }


            public System.String ProcedureID
            {
                get
                {
                    System.String data = entity.ProcedureID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureID = null;
                    else entity.ProcedureID = Convert.ToString(value);
                }
            }

            public System.String ProcedureName
            {
                get
                {
                    System.String data = entity.ProcedureName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureName = null;
                    else entity.ProcedureName = Convert.ToString(value);
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

            public System.String IM
            {
                get
                {
                    System.Boolean? data = entity.IM;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IM = null;
                    else entity.IM = Convert.ToBoolean(value);
                }
            }
            public System.String ValidCode
            {
                get
                {
                    System.Boolean? data = entity.ValidCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ValidCode = null;
                    else entity.ValidCode = Convert.ToBoolean(value);
                }
            }
            public System.String Asterisk
            {
                get
                {
                    System.Boolean? data = entity.Asterisk;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Asterisk = null;
                    else entity.Asterisk = Convert.ToBoolean(value);
                }
            }
            public System.String Accpdx
            {
                get
                {
                    System.Boolean? data = entity.Accpdx;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Accpdx = null;
                    else entity.Asterisk = Convert.ToBoolean(value);
                }
            }


            private esProcedure entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esProcedureQuery query)
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
                throw new Exception("esProcedure can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class Procedure : esProcedure
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
    abstract public class esProcedureQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ProcedureMetadata.Meta();
            }
        }


        public esQueryItem ProcedureID
        {
            get
            {
                return new esQueryItem(this, ProcedureMetadata.ColumnNames.ProcedureID, esSystemType.String);
            }
        }

        public esQueryItem ProcedureName
        {
            get
            {
                return new esQueryItem(this, ProcedureMetadata.ColumnNames.ProcedureName, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ProcedureMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ProcedureMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IM
        {
            get
            {
                return new esQueryItem(this, ProcedureMetadata.ColumnNames.IM, esSystemType.Boolean);
            }
        }
        public esQueryItem ValidCode
        {
            get
            {
                return new esQueryItem(this, ProcedureMetadata.ColumnNames.ValidCode, esSystemType.Boolean);
            }
        }
        public esQueryItem Asterisk
        {
            get
            {
                return new esQueryItem(this, ProcedureMetadata.ColumnNames.Asterisk, esSystemType.Boolean);
            }
        }
        public esQueryItem Accpdx
        {
            get
            {
                return new esQueryItem(this, ProcedureMetadata.ColumnNames.Accpdx, esSystemType.Boolean);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ProcedureCollection")]
    public partial class ProcedureCollection : esProcedureCollection, IEnumerable<Procedure>
    {
        public ProcedureCollection()
        {

        }

        public static implicit operator List<Procedure>(ProcedureCollection coll)
        {
            List<Procedure> list = new List<Procedure>();

            foreach (Procedure emp in coll)
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
                return ProcedureMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ProcedureQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Procedure(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Procedure();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ProcedureQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ProcedureQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ProcedureQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public Procedure AddNew()
        {
            Procedure entity = base.AddNewEntity() as Procedure;

            return entity;
        }

        public Procedure FindByPrimaryKey(System.String procedureID)
        {
            return base.FindByPrimaryKey(procedureID) as Procedure;
        }


        #region IEnumerable<Procedure> Members

        IEnumerator<Procedure> IEnumerable<Procedure>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Procedure;
            }
        }

        #endregion

        private ProcedureQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Procedure' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("Procedure ({ProcedureID})")]
    [Serializable]
    public partial class Procedure : esProcedure
    {
        public Procedure()
        {

        }

        public Procedure(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ProcedureMetadata.Meta();
            }
        }



        override protected esProcedureQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ProcedureQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ProcedureQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ProcedureQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ProcedureQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ProcedureQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ProcedureQuery : esProcedureQuery
    {
        public ProcedureQuery()
        {

        }

        public ProcedureQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ProcedureQuery";
        }


    }


    [Serializable]
    public partial class ProcedureMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ProcedureMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ProcedureMetadata.ColumnNames.ProcedureID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ProcedureMetadata.PropertyNames.ProcedureID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureMetadata.ColumnNames.ProcedureName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ProcedureMetadata.PropertyNames.ProcedureName;
            c.CharacterMaxLength = 250;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ProcedureMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ProcedureMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureMetadata.ColumnNames.IM, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ProcedureMetadata.PropertyNames.IM;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureMetadata.ColumnNames.ValidCode, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ProcedureMetadata.PropertyNames.ValidCode;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureMetadata.ColumnNames.Asterisk, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ProcedureMetadata.PropertyNames.Asterisk;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ProcedureMetadata.ColumnNames.Accpdx, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ProcedureMetadata.PropertyNames.Accpdx;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ProcedureMetadata Meta()
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
            public const string ProcedureID = "ProcedureID";
            public const string ProcedureName = "ProcedureName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IM = "IM";
            public const string ValidCode = "ValidCode";
            public const string Asterisk = "Asterisk";
            public const string Accpdx = "Accpdx";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ProcedureID = "ProcedureID";
            public const string ProcedureName = "ProcedureName";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IM = "IM";
            public const string ValidCode = "ValidCode";
            public const string Asterisk = "Asterisk";
            public const string Accpdx = "Accpdx";
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
            lock (typeof(ProcedureMetadata))
            {
                if (ProcedureMetadata.mapDelegates == null)
                {
                    ProcedureMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ProcedureMetadata.meta == null)
                {
                    ProcedureMetadata.meta = new ProcedureMetadata();
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


                meta.AddTypeMap("ProcedureID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IM", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ValidCode", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Asterisk", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("Accpdx", new esTypeMap("bit", "System.Boolean"));



                meta.Source = "Procedure";
                meta.Destination = "Procedure";

                meta.spInsert = "proc_ProcedureInsert";
                meta.spUpdate = "proc_ProcedureUpdate";
                meta.spDelete = "proc_ProcedureDelete";
                meta.spLoadAll = "proc_ProcedureLoadAll";
                meta.spLoadByPrimaryKey = "proc_ProcedureLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ProcedureMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
