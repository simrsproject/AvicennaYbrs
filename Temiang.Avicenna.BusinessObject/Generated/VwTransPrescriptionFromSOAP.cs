/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/31/2011 9:23:04 AM
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
    abstract public class esVwTransPrescriptionFromSOAPCollection : esEntityCollectionWAuditLog
    {
        public esVwTransPrescriptionFromSOAPCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "VwTransPrescriptionFromSOAPCollection";
        }

        #region Query Logic
        protected void InitQuery(esVwTransPrescriptionFromSOAPQuery query)
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
            this.InitQuery(query as esVwTransPrescriptionFromSOAPQuery);
        }
        #endregion

        virtual public VwTransPrescriptionFromSOAP DetachEntity(VwTransPrescriptionFromSOAP entity)
        {
            return base.DetachEntity(entity) as VwTransPrescriptionFromSOAP;
        }

        virtual public VwTransPrescriptionFromSOAP AttachEntity(VwTransPrescriptionFromSOAP entity)
        {
            return base.AttachEntity(entity) as VwTransPrescriptionFromSOAP;
        }

        virtual public void Combine(VwTransPrescriptionFromSOAPCollection collection)
        {
            base.Combine(collection);
        }

        new public VwTransPrescriptionFromSOAP this[int index]
        {
            get
            {
                return base[index] as VwTransPrescriptionFromSOAP;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(VwTransPrescriptionFromSOAP);
        }
    }



    [Serializable]
    abstract public class esVwTransPrescriptionFromSOAP : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esVwTransPrescriptionFromSOAPQuery GetDynamicQuery()
        {
            return null;
        }

        public esVwTransPrescriptionFromSOAP()
        {

        }

        public esVwTransPrescriptionFromSOAP(DataRow row)
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
                        case "PrescriptionDate": this.str.PrescriptionDate = (string)value; break;
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
        /// Maps to vw_TransPrescriptionFromSOAP.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(VwTransPrescriptionFromSOAPMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(VwTransPrescriptionFromSOAPMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_TransPrescriptionFromSOAP.PrescriptionDate
        /// </summary>
        virtual public System.DateTime? PrescriptionDate
        {
            get
            {
                return base.GetSystemDateTime(VwTransPrescriptionFromSOAPMetadata.ColumnNames.PrescriptionDate);
            }

            set
            {
                base.SetSystemDateTime(VwTransPrescriptionFromSOAPMetadata.ColumnNames.PrescriptionDate, value);
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
            public esStrings(esVwTransPrescriptionFromSOAP entity)
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


            private esVwTransPrescriptionFromSOAP entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esVwTransPrescriptionFromSOAPQuery query)
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
                throw new Exception("esVwTransPrescriptionFromSOAP can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esVwTransPrescriptionFromSOAPQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return VwTransPrescriptionFromSOAPMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, VwTransPrescriptionFromSOAPMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem PrescriptionDate
        {
            get
            {
                return new esQueryItem(this, VwTransPrescriptionFromSOAPMetadata.ColumnNames.PrescriptionDate, esSystemType.DateTime);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("VwTransPrescriptionFromSOAPCollection")]
    public partial class VwTransPrescriptionFromSOAPCollection : esVwTransPrescriptionFromSOAPCollection, IEnumerable<VwTransPrescriptionFromSOAP>
    {
        public VwTransPrescriptionFromSOAPCollection()
        {

        }

        public static implicit operator List<VwTransPrescriptionFromSOAP>(VwTransPrescriptionFromSOAPCollection coll)
        {
            List<VwTransPrescriptionFromSOAP> list = new List<VwTransPrescriptionFromSOAP>();

            foreach (VwTransPrescriptionFromSOAP emp in coll)
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
                return VwTransPrescriptionFromSOAPMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwTransPrescriptionFromSOAPQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new VwTransPrescriptionFromSOAP(row);
        }

        override protected esEntity CreateEntity()
        {
            return new VwTransPrescriptionFromSOAP();
        }


        override public bool LoadAll()
        {
            return base.LoadAll(esSqlAccessType.DynamicSQL);
        }

        #endregion


        [BrowsableAttribute(false)]
        public VwTransPrescriptionFromSOAPQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwTransPrescriptionFromSOAPQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(VwTransPrescriptionFromSOAPQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public VwTransPrescriptionFromSOAP AddNew()
        {
            VwTransPrescriptionFromSOAP entity = base.AddNewEntity() as VwTransPrescriptionFromSOAP;

            return entity;
        }


        #region IEnumerable<VwTransPrescriptionFromSOAP> Members

        IEnumerator<VwTransPrescriptionFromSOAP> IEnumerable<VwTransPrescriptionFromSOAP>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as VwTransPrescriptionFromSOAP;
            }
        }

        #endregion

        private VwTransPrescriptionFromSOAPQuery query;
    }


    /// <summary>
    /// Encapsulates the 'vw_TransPrescriptionFromSOAP' view
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("VwTransPrescriptionFromSOAP ()")]
    [Serializable]
    public partial class VwTransPrescriptionFromSOAP : esVwTransPrescriptionFromSOAP
    {
        public VwTransPrescriptionFromSOAP()
        {

        }

        public VwTransPrescriptionFromSOAP(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return VwTransPrescriptionFromSOAPMetadata.Meta();
            }
        }



        override protected esVwTransPrescriptionFromSOAPQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwTransPrescriptionFromSOAPQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public VwTransPrescriptionFromSOAPQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwTransPrescriptionFromSOAPQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(VwTransPrescriptionFromSOAPQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private VwTransPrescriptionFromSOAPQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class VwTransPrescriptionFromSOAPQuery : esVwTransPrescriptionFromSOAPQuery
    {
        public VwTransPrescriptionFromSOAPQuery()
        {

        }

        public VwTransPrescriptionFromSOAPQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "VwTransPrescriptionFromSOAPQuery";
        }


    }


    [Serializable]
    public partial class VwTransPrescriptionFromSOAPMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected VwTransPrescriptionFromSOAPMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(VwTransPrescriptionFromSOAPMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransPrescriptionFromSOAPMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransPrescriptionFromSOAPMetadata.ColumnNames.PrescriptionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = VwTransPrescriptionFromSOAPMetadata.PropertyNames.PrescriptionDate;
            _columns.Add(c);

        }
        #endregion

        static public VwTransPrescriptionFromSOAPMetadata Meta()
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
            public const string PrescriptionDate = "PrescriptionDate";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string PrescriptionDate = "PrescriptionDate";
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
            lock (typeof(VwTransPrescriptionFromSOAPMetadata))
            {
                if (VwTransPrescriptionFromSOAPMetadata.mapDelegates == null)
                {
                    VwTransPrescriptionFromSOAPMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (VwTransPrescriptionFromSOAPMetadata.meta == null)
                {
                    VwTransPrescriptionFromSOAPMetadata.meta = new VwTransPrescriptionFromSOAPMetadata();
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
                meta.AddTypeMap("PrescriptionDate", new esTypeMap("datetime", "System.DateTime"));



                meta.Source = "vw_TransPrescriptionFromSOAP";
                meta.Destination = "vw_TransPrescriptionFromSOAP";

                meta.spInsert = "proc_vw_TransPrescriptionFromSOAPInsert";
                meta.spUpdate = "proc_vw_TransPrescriptionFromSOAPUpdate";
                meta.spDelete = "proc_vw_TransPrescriptionFromSOAPDelete";
                meta.spLoadAll = "proc_vw_TransPrescriptionFromSOAPLoadAll";
                meta.spLoadByPrimaryKey = "proc_vw_TransPrescriptionFromSOAPLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private VwTransPrescriptionFromSOAPMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
