/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/3/2021 7:31:33 AM
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
    abstract public class esBpjsRujukanCollection : esEntityCollectionWAuditLog
    {
        public esBpjsRujukanCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "BpjsRujukanCollection";
        }

        #region Query Logic
        protected void InitQuery(esBpjsRujukanQuery query)
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
            this.InitQuery(query as esBpjsRujukanQuery);
        }
        #endregion

        virtual public BpjsRujukan DetachEntity(BpjsRujukan entity)
        {
            return base.DetachEntity(entity) as BpjsRujukan;
        }

        virtual public BpjsRujukan AttachEntity(BpjsRujukan entity)
        {
            return base.AttachEntity(entity) as BpjsRujukan;
        }

        virtual public void Combine(BpjsRujukanCollection collection)
        {
            base.Combine(collection);
        }

        new public BpjsRujukan this[int index]
        {
            get
            {
                return base[index] as BpjsRujukan;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BpjsRujukan);
        }
    }



    [Serializable]
    abstract public class esBpjsRujukan : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBpjsRujukanQuery GetDynamicQuery()
        {
            return null;
        }

        public esBpjsRujukan()
        {

        }

        public esBpjsRujukan(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String noSep, System.String noRujukan)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(noSep, noRujukan);
            else
                return LoadByPrimaryKeyStoredProcedure(noRujukan, noSep);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String noSep, System.String noRujukan)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(noSep, noRujukan);
            else
                return LoadByPrimaryKeyStoredProcedure(noSep, noRujukan);
        }

        private bool LoadByPrimaryKeyDynamic(System.String noSep, System.String noRujukan)
        {
            esBpjsRujukanQuery query = this.GetDynamicQuery();
            query.Where(query.NoSep == noSep, query.NoRujukan == noRujukan);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String noSep, System.String noRujukan)
        {
            esParameters parms = new esParameters();
            parms.Add("noSep", noSep); parms.Add("NoRujukan", noRujukan);
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
                        case "NoSep": this.str.NoSep = (string)value; break;
                        case "NoRujukan": this.str.NoRujukan = (string)value; break;
                        case "TglRujukan": this.str.TglRujukan = (string)value; break;
                        case "PpkDirujuk": this.str.PpkDirujuk = (string)value; break;
                        case "NamaPpkDirujuk": this.str.NamaPpkDirujuk = (string)value; break;
                        case "JnsPelayanan": this.str.JnsPelayanan = (string)value; break;
                        case "Catatan": this.str.Catatan = (string)value; break;
                        case "DiagRujukan": this.str.DiagRujukan = (string)value; break;
                        case "TipeRujukan": this.str.TipeRujukan = (string)value; break;
                        case "PoliRujukan": this.str.PoliRujukan = (string)value; break;
                        case "NamaPoliRujukan": this.str.NamaPoliRujukan = (string)value; break;
                        case "User": this.str.User = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "TglRencana": this.str.TglRencana = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TglRujukan":

                            if (value == null || value is System.DateTime)
                                this.TglRujukan = (System.DateTime?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "TglRencana":

                            if (value == null || value is System.DateTime)
                                this.TglRencana = (System.DateTime?)value;
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
        /// Maps to BpjsRujukan.noSep
        /// </summary>
        virtual public System.String NoSep
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.NoSep);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.NoSep, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.NoRujukan
        /// </summary>
        virtual public System.String NoRujukan
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.NoRujukan);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.NoRujukan, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.tglRujukan
        /// </summary>
        virtual public System.DateTime? TglRujukan
        {
            get
            {
                return base.GetSystemDateTime(BpjsRujukanMetadata.ColumnNames.TglRujukan);
            }

            set
            {
                base.SetSystemDateTime(BpjsRujukanMetadata.ColumnNames.TglRujukan, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.ppkDirujuk
        /// </summary>
        virtual public System.String PpkDirujuk
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.PpkDirujuk);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.PpkDirujuk, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.namaPpkDirujuk
        /// </summary>
        virtual public System.String NamaPpkDirujuk
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.NamaPpkDirujuk);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.NamaPpkDirujuk, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.jnsPelayanan
        /// </summary>
        virtual public System.String JnsPelayanan
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.JnsPelayanan);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.JnsPelayanan, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.catatan
        /// </summary>
        virtual public System.String Catatan
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.Catatan);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.Catatan, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.diagRujukan
        /// </summary>
        virtual public System.String DiagRujukan
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.DiagRujukan);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.DiagRujukan, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.tipeRujukan
        /// </summary>
        virtual public System.String TipeRujukan
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.TipeRujukan);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.TipeRujukan, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.poliRujukan
        /// </summary>
        virtual public System.String PoliRujukan
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.PoliRujukan);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.PoliRujukan, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.namaPoliRujukan
        /// </summary>
        virtual public System.String NamaPoliRujukan
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.NamaPoliRujukan);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.NamaPoliRujukan, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.user
        /// </summary>
        virtual public System.String User
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.User);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.User, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BpjsRujukanMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BpjsRujukanMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BpjsRujukanMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BpjsRujukanMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to BpjsRujukan.tglRencana
        /// </summary>
        virtual public System.DateTime? TglRencana
        {
            get
            {
                return base.GetSystemDateTime(BpjsRujukanMetadata.ColumnNames.TglRencana);
            }

            set
            {
                base.SetSystemDateTime(BpjsRujukanMetadata.ColumnNames.TglRencana, value);
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
            public esStrings(esBpjsRujukan entity)
            {
                this.entity = entity;
            }


            public System.String NoSep
            {
                get
                {
                    System.String data = entity.NoSep;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoSep = null;
                    else entity.NoSep = Convert.ToString(value);
                }
            }

            public System.String NoRujukan
            {
                get
                {
                    System.String data = entity.NoRujukan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoRujukan = null;
                    else entity.NoRujukan = Convert.ToString(value);
                }
            }

            public System.String TglRujukan
            {
                get
                {
                    System.DateTime? data = entity.TglRujukan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TglRujukan = null;
                    else entity.TglRujukan = Convert.ToDateTime(value);
                }
            }

            public System.String PpkDirujuk
            {
                get
                {
                    System.String data = entity.PpkDirujuk;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PpkDirujuk = null;
                    else entity.PpkDirujuk = Convert.ToString(value);
                }
            }

            public System.String NamaPpkDirujuk
            {
                get
                {
                    System.String data = entity.NamaPpkDirujuk;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NamaPpkDirujuk = null;
                    else entity.NamaPpkDirujuk = Convert.ToString(value);
                }
            }

            public System.String JnsPelayanan
            {
                get
                {
                    System.String data = entity.JnsPelayanan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JnsPelayanan = null;
                    else entity.JnsPelayanan = Convert.ToString(value);
                }
            }

            public System.String Catatan
            {
                get
                {
                    System.String data = entity.Catatan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Catatan = null;
                    else entity.Catatan = Convert.ToString(value);
                }
            }

            public System.String DiagRujukan
            {
                get
                {
                    System.String data = entity.DiagRujukan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagRujukan = null;
                    else entity.DiagRujukan = Convert.ToString(value);
                }
            }

            public System.String TipeRujukan
            {
                get
                {
                    System.String data = entity.TipeRujukan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TipeRujukan = null;
                    else entity.TipeRujukan = Convert.ToString(value);
                }
            }

            public System.String PoliRujukan
            {
                get
                {
                    System.String data = entity.PoliRujukan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PoliRujukan = null;
                    else entity.PoliRujukan = Convert.ToString(value);
                }
            }

            public System.String NamaPoliRujukan
            {
                get
                {
                    System.String data = entity.NamaPoliRujukan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NamaPoliRujukan = null;
                    else entity.NamaPoliRujukan = Convert.ToString(value);
                }
            }

            public System.String User
            {
                get
                {
                    System.String data = entity.User;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.User = null;
                    else entity.User = Convert.ToString(value);
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

            public System.String TglRencana
            {
                get
                {
                    System.DateTime? data = entity.TglRencana;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TglRencana = null;
                    else entity.TglRencana = Convert.ToDateTime(value);
                }
            }


            private esBpjsRujukan entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBpjsRujukanQuery query)
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
                throw new Exception("esBpjsRujukan can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esBpjsRujukanQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return BpjsRujukanMetadata.Meta();
            }
        }


        public esQueryItem NoSep
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.NoSep, esSystemType.String);
            }
        }

        public esQueryItem NoRujukan
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.NoRujukan, esSystemType.String);
            }
        }

        public esQueryItem TglRujukan
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.TglRujukan, esSystemType.DateTime);
            }
        }

        public esQueryItem PpkDirujuk
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.PpkDirujuk, esSystemType.String);
            }
        }

        public esQueryItem NamaPpkDirujuk
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.NamaPpkDirujuk, esSystemType.String);
            }
        }

        public esQueryItem JnsPelayanan
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.JnsPelayanan, esSystemType.String);
            }
        }

        public esQueryItem Catatan
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.Catatan, esSystemType.String);
            }
        }

        public esQueryItem DiagRujukan
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.DiagRujukan, esSystemType.String);
            }
        }

        public esQueryItem TipeRujukan
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.TipeRujukan, esSystemType.String);
            }
        }

        public esQueryItem PoliRujukan
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.PoliRujukan, esSystemType.String);
            }
        }

        public esQueryItem NamaPoliRujukan
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.NamaPoliRujukan, esSystemType.String);
            }
        }

        public esQueryItem User
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.User, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem TglRencana
        {
            get
            {
                return new esQueryItem(this, BpjsRujukanMetadata.ColumnNames.TglRencana, esSystemType.DateTime);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BpjsRujukanCollection")]
    public partial class BpjsRujukanCollection : esBpjsRujukanCollection, IEnumerable<BpjsRujukan>
    {
        public BpjsRujukanCollection()
        {

        }

        public static implicit operator List<BpjsRujukan>(BpjsRujukanCollection coll)
        {
            List<BpjsRujukan> list = new List<BpjsRujukan>();

            foreach (BpjsRujukan emp in coll)
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
                return BpjsRujukanMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsRujukanQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BpjsRujukan(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BpjsRujukan();
        }


        #endregion


        [BrowsableAttribute(false)]
        public BpjsRujukanQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsRujukanQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(BpjsRujukanQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public BpjsRujukan AddNew()
        {
            BpjsRujukan entity = base.AddNewEntity() as BpjsRujukan;

            return entity;
        }

        public BpjsRujukan FindByPrimaryKey(System.String noRujukan, System.String noSep)
        {
            return base.FindByPrimaryKey(noRujukan, noSep) as BpjsRujukan;
        }


        #region IEnumerable<BpjsRujukan> Members

        IEnumerator<BpjsRujukan> IEnumerable<BpjsRujukan>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BpjsRujukan;
            }
        }

        #endregion

        private BpjsRujukanQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BpjsRujukan' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("BpjsRujukan ({NoSep},{NoRujukan})")]
    [Serializable]
    public partial class BpjsRujukan : esBpjsRujukan
    {
        public BpjsRujukan()
        {

        }

        public BpjsRujukan(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BpjsRujukanMetadata.Meta();
            }
        }



        override protected esBpjsRujukanQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsRujukanQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public BpjsRujukanQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsRujukanQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(BpjsRujukanQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BpjsRujukanQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class BpjsRujukanQuery : esBpjsRujukanQuery
    {
        public BpjsRujukanQuery()
        {

        }

        public BpjsRujukanQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BpjsRujukanQuery";
        }


    }


    [Serializable]
    public partial class BpjsRujukanMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BpjsRujukanMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.NoSep, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.NoSep;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.NoRujukan, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.NoRujukan;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.TglRujukan, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.TglRujukan;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.PpkDirujuk, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.PpkDirujuk;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.NamaPpkDirujuk, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.NamaPpkDirujuk;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.JnsPelayanan, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.JnsPelayanan;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.Catatan, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.Catatan;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.DiagRujukan, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.DiagRujukan;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.TipeRujukan, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.TipeRujukan;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.PoliRujukan, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.PoliRujukan;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.NamaPoliRujukan, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.NamaPoliRujukan;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.User, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.User;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsRujukanMetadata.ColumnNames.TglRencana, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsRujukanMetadata.PropertyNames.TglRencana;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public BpjsRujukanMetadata Meta()
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
            public const string NoSep = "noSep";
            public const string NoRujukan = "NoRujukan";
            public const string TglRujukan = "tglRujukan";
            public const string PpkDirujuk = "ppkDirujuk";
            public const string NamaPpkDirujuk = "namaPpkDirujuk";
            public const string JnsPelayanan = "jnsPelayanan";
            public const string Catatan = "catatan";
            public const string DiagRujukan = "diagRujukan";
            public const string TipeRujukan = "tipeRujukan";
            public const string PoliRujukan = "poliRujukan";
            public const string NamaPoliRujukan = "namaPoliRujukan";
            public const string User = "user";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string TglRencana = "tglRencana";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string NoSep = "NoSep";
            public const string NoRujukan = "NoRujukan";
            public const string TglRujukan = "TglRujukan";
            public const string PpkDirujuk = "PpkDirujuk";
            public const string NamaPpkDirujuk = "NamaPpkDirujuk";
            public const string JnsPelayanan = "JnsPelayanan";
            public const string Catatan = "Catatan";
            public const string DiagRujukan = "DiagRujukan";
            public const string TipeRujukan = "TipeRujukan";
            public const string PoliRujukan = "PoliRujukan";
            public const string NamaPoliRujukan = "NamaPoliRujukan";
            public const string User = "User";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string TglRencana = "TglRencana";
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
            lock (typeof(BpjsRujukanMetadata))
            {
                if (BpjsRujukanMetadata.mapDelegates == null)
                {
                    BpjsRujukanMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BpjsRujukanMetadata.meta == null)
                {
                    BpjsRujukanMetadata.meta = new BpjsRujukanMetadata();
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


                meta.AddTypeMap("NoSep", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoRujukan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TglRujukan", new esTypeMap("smalldatetime", "System.DateTime"));
                meta.AddTypeMap("PpkDirujuk", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NamaPpkDirujuk", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JnsPelayanan", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("Catatan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiagRujukan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TipeRujukan", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("PoliRujukan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NamaPoliRujukan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("User", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TglRencana", new esTypeMap("smalldatetime", "System.DateTime"));



                meta.Source = "BpjsRujukan";
                meta.Destination = "BpjsRujukan";

                meta.spInsert = "proc_BpjsRujukanInsert";
                meta.spUpdate = "proc_BpjsRujukanUpdate";
                meta.spDelete = "proc_BpjsRujukanDelete";
                meta.spLoadAll = "proc_BpjsRujukanLoadAll";
                meta.spLoadByPrimaryKey = "proc_BpjsRujukanLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BpjsRujukanMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
