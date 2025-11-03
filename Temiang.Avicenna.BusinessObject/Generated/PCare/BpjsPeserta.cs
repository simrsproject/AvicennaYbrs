/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/22/2016 9:36:13 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    abstract public class esBpjsPesertaCollection : esEntityCollectionWAuditLog
    {
        public esBpjsPesertaCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BpjsPesertaCollection";
        }

        #region Query Logic
        protected void InitQuery(esBpjsPesertaQuery query)
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
            this.InitQuery(query as esBpjsPesertaQuery);
        }
        #endregion

        virtual public BpjsPeserta DetachEntity(BpjsPeserta entity)
        {
            return base.DetachEntity(entity) as BpjsPeserta;
        }

        virtual public BpjsPeserta AttachEntity(BpjsPeserta entity)
        {
            return base.AttachEntity(entity) as BpjsPeserta;
        }

        virtual public void Combine(BpjsPesertaCollection collection)
        {
            base.Combine(collection);
        }

        new public BpjsPeserta this[int index]
        {
            get
            {
                return base[index] as BpjsPeserta;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BpjsPeserta);
        }
    }

    [Serializable]
    abstract public class esBpjsPeserta : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBpjsPesertaQuery GetDynamicQuery()
        {
            return null;
        }

        public esBpjsPeserta()
        {
        }

        public esBpjsPeserta(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String noKartu)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(noKartu);
            else
                return LoadByPrimaryKeyStoredProcedure(noKartu);
        }

        /// <summary>
        /// Loads an entity by primary key
        /// </summary>
        /// <remarks>
        /// Requires primary keys be defined on all tables.
        /// If a table does not have a primary key set,
        /// this method will not compile.
        /// </remarks>
        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String noKartu)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(noKartu);
            else
                return LoadByPrimaryKeyStoredProcedure(noKartu);
        }

        private bool LoadByPrimaryKeyDynamic(String noKartu)
        {
            esBpjsPesertaQuery query = this.GetDynamicQuery();
            query.Where(query.NoKartu == noKartu);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String noKartu)
        {
            esParameters parms = new esParameters();
            parms.Add("NoKartu", noKartu);
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
                        case "NoKartu": this.str.NoKartu = (string)value; break;
                        case "Nama": this.str.Nama = (string)value; break;
                        case "HubunganKeluarga": this.str.HubunganKeluarga = (string)value; break;
                        case "Sex": this.str.Sex = (string)value; break;
                        case "TglLahir": this.str.TglLahir = (string)value; break;
                        case "TglMulaiAktif": this.str.TglMulaiAktif = (string)value; break;
                        case "TglAkhirBerlaku": this.str.TglAkhirBerlaku = (string)value; break;
                        case "KdProviderPst_kdProvider": this.str.KdProviderPst_kdProvider = (string)value; break;
                        case "KdProviderPst_nmProvider": this.str.KdProviderPst_nmProvider = (string)value; break;
                        case "KdProviderGigi_kdProvider": this.str.KdProviderGigi_kdProvider = (string)value; break;
                        case "KdProviderGigi_nmProvider": this.str.KdProviderGigi_nmProvider = (string)value; break;
                        case "JnsKelas_kode": this.str.JnsKelas_kode = (string)value; break;
                        case "JnsKelas_nama": this.str.JnsKelas_nama = (string)value; break;
                        case "JnsPeserta_kode": this.str.JnsPeserta_kode = (string)value; break;
                        case "JnsPeserta_nama": this.str.JnsPeserta_nama = (string)value; break;
                        case "GolDarah": this.str.GolDarah = (string)value; break;
                        case "NoHP": this.str.NoHP = (string)value; break;
                        case "NoKTP": this.str.NoKTP = (string)value; break;
                        case "Aktif": this.str.Aktif = (string)value; break;
                        case "KetAktif": this.str.KetAktif = (string)value; break;
                        case "Asuransi_kdAsuransi": this.str.Asuransi_kdAsuransi = (string)value; break;
                        case "Asuransi_nmAsuransi": this.str.Asuransi_nmAsuransi = (string)value; break;
                        case "Asuransi_noAsuransi": this.str.Asuransi_noAsuransi = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TglLahir":

                            if (value == null || value is System.DateTime)
                                this.TglLahir = (System.DateTime?)value;
                            break;
                        case "TglMulaiAktif":

                            if (value == null || value is System.DateTime)
                                this.TglMulaiAktif = (System.DateTime?)value;
                            break;
                        case "TglAkhirBerlaku":

                            if (value == null || value is System.DateTime)
                                this.TglAkhirBerlaku = (System.DateTime?)value;
                            break;
                        case "Aktif":

                            if (value == null || value is System.Boolean)
                                this.Aktif = (System.Boolean?)value;
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
        /// Maps to BpjsPeserta.NoKartu
        /// </summary>
        virtual public System.String NoKartu
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.NoKartu);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.NoKartu, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.Nama
        /// </summary>
        virtual public System.String Nama
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.Nama);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.Nama, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.HubunganKeluarga
        /// </summary>
        virtual public System.String HubunganKeluarga
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.HubunganKeluarga);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.HubunganKeluarga, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.Sex
        /// </summary>
        virtual public System.String Sex
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.Sex);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.Sex, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.TglLahir
        /// </summary>
        virtual public System.DateTime? TglLahir
        {
            get
            {
                return base.GetSystemDateTime(BpjsPesertaMetadata.ColumnNames.TglLahir);
            }

            set
            {
                base.SetSystemDateTime(BpjsPesertaMetadata.ColumnNames.TglLahir, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.TglMulaiAktif
        /// </summary>
        virtual public System.DateTime? TglMulaiAktif
        {
            get
            {
                return base.GetSystemDateTime(BpjsPesertaMetadata.ColumnNames.TglMulaiAktif);
            }

            set
            {
                base.SetSystemDateTime(BpjsPesertaMetadata.ColumnNames.TglMulaiAktif, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.TglAkhirBerlaku
        /// </summary>
        virtual public System.DateTime? TglAkhirBerlaku
        {
            get
            {
                return base.GetSystemDateTime(BpjsPesertaMetadata.ColumnNames.TglAkhirBerlaku);
            }

            set
            {
                base.SetSystemDateTime(BpjsPesertaMetadata.ColumnNames.TglAkhirBerlaku, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.KdProviderPst_kdProvider
        /// </summary>
        virtual public System.String KdProviderPst_kdProvider
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.KdProviderPst_kdProvider);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.KdProviderPst_kdProvider, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.KdProviderPst_nmProvider
        /// </summary>
        virtual public System.String KdProviderPst_nmProvider
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.KdProviderPst_nmProvider);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.KdProviderPst_nmProvider, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.KdProviderGigi_kdProvider
        /// </summary>
        virtual public System.String KdProviderGigi_kdProvider
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.KdProviderGigi_kdProvider);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.KdProviderGigi_kdProvider, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.KdProviderGigi_nmProvider
        /// </summary>
        virtual public System.String KdProviderGigi_nmProvider
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.KdProviderGigi_nmProvider);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.KdProviderGigi_nmProvider, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.JnsKelas_kode
        /// </summary>
        virtual public System.String JnsKelas_kode
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.JnsKelas_kode);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.JnsKelas_kode, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.JnsKelas_nama
        /// </summary>
        virtual public System.String JnsKelas_nama
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.JnsKelas_nama);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.JnsKelas_nama, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.JnsPeserta_kode
        /// </summary>
        virtual public System.String JnsPeserta_kode
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.JnsPeserta_kode);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.JnsPeserta_kode, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.JnsPeserta_nama
        /// </summary>
        virtual public System.String JnsPeserta_nama
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.JnsPeserta_nama);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.JnsPeserta_nama, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.GolDarah
        /// </summary>
        virtual public System.String GolDarah
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.GolDarah);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.GolDarah, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.NoHP
        /// </summary>
        virtual public System.String NoHP
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.NoHP);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.NoHP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.NoKTP
        /// </summary>
        virtual public System.String NoKTP
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.NoKTP);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.NoKTP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.Aktif
        /// </summary>
        virtual public System.Boolean? Aktif
        {
            get
            {
                return base.GetSystemBoolean(BpjsPesertaMetadata.ColumnNames.Aktif);
            }

            set
            {
                base.SetSystemBoolean(BpjsPesertaMetadata.ColumnNames.Aktif, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.KetAktif
        /// </summary>
        virtual public System.String KetAktif
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.KetAktif);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.KetAktif, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.Asuransi_kdAsuransi
        /// </summary>
        virtual public System.String Asuransi_kdAsuransi
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.Asuransi_kdAsuransi);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.Asuransi_kdAsuransi, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.Asuransi_nmAsuransi
        /// </summary>
        virtual public System.String Asuransi_nmAsuransi
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.Asuransi_nmAsuransi);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.Asuransi_nmAsuransi, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.Asuransi_noAsuransi
        /// </summary>
        virtual public System.String Asuransi_noAsuransi
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.Asuransi_noAsuransi);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.Asuransi_noAsuransi, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BpjsPesertaMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BpjsPesertaMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BpjsPeserta.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BpjsPesertaMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BpjsPesertaMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        #endregion

        #region String Properties

        /// <summary>
        /// Converts an entity's properties to
        /// and from strings.
        /// </summary>
        /// <remarks>
        /// The str properties Get and Set provide easy conversion
        /// between a string and a property's data type. Not all
        /// data types will get a str property.
        /// </remarks>
        /// <example>
        /// Set a datetime from a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// entity.str.HireDate = "2007-01-01 00:00:00";
        /// entity.Save();
        /// </code>
        /// Get a datetime as a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// string theDate = entity.str.HireDate;
        /// </code>
        /// </example>
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
            public esStrings(esBpjsPeserta entity)
            {
                this.entity = entity;
            }
            public System.String NoKartu
            {
                get
                {
                    System.String data = entity.NoKartu;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoKartu = null;
                    else entity.NoKartu = Convert.ToString(value);
                }
            }
            public System.String Nama
            {
                get
                {
                    System.String data = entity.Nama;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Nama = null;
                    else entity.Nama = Convert.ToString(value);
                }
            }
            public System.String HubunganKeluarga
            {
                get
                {
                    System.String data = entity.HubunganKeluarga;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HubunganKeluarga = null;
                    else entity.HubunganKeluarga = Convert.ToString(value);
                }
            }
            public System.String Sex
            {
                get
                {
                    System.String data = entity.Sex;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Sex = null;
                    else entity.Sex = Convert.ToString(value);
                }
            }
            public System.String TglLahir
            {
                get
                {
                    System.DateTime? data = entity.TglLahir;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TglLahir = null;
                    else entity.TglLahir = Convert.ToDateTime(value);
                }
            }
            public System.String TglMulaiAktif
            {
                get
                {
                    System.DateTime? data = entity.TglMulaiAktif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TglMulaiAktif = null;
                    else entity.TglMulaiAktif = Convert.ToDateTime(value);
                }
            }
            public System.String TglAkhirBerlaku
            {
                get
                {
                    System.DateTime? data = entity.TglAkhirBerlaku;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TglAkhirBerlaku = null;
                    else entity.TglAkhirBerlaku = Convert.ToDateTime(value);
                }
            }
            public System.String KdProviderPst_kdProvider
            {
                get
                {
                    System.String data = entity.KdProviderPst_kdProvider;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KdProviderPst_kdProvider = null;
                    else entity.KdProviderPst_kdProvider = Convert.ToString(value);
                }
            }
            public System.String KdProviderPst_nmProvider
            {
                get
                {
                    System.String data = entity.KdProviderPst_nmProvider;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KdProviderPst_nmProvider = null;
                    else entity.KdProviderPst_nmProvider = Convert.ToString(value);
                }
            }
            public System.String KdProviderGigi_kdProvider
            {
                get
                {
                    System.String data = entity.KdProviderGigi_kdProvider;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KdProviderGigi_kdProvider = null;
                    else entity.KdProviderGigi_kdProvider = Convert.ToString(value);
                }
            }
            public System.String KdProviderGigi_nmProvider
            {
                get
                {
                    System.String data = entity.KdProviderGigi_nmProvider;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KdProviderGigi_nmProvider = null;
                    else entity.KdProviderGigi_nmProvider = Convert.ToString(value);
                }
            }
            public System.String JnsKelas_kode
            {
                get
                {
                    System.String data = entity.JnsKelas_kode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JnsKelas_kode = null;
                    else entity.JnsKelas_kode = Convert.ToString(value);
                }
            }
            public System.String JnsKelas_nama
            {
                get
                {
                    System.String data = entity.JnsKelas_nama;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JnsKelas_nama = null;
                    else entity.JnsKelas_nama = Convert.ToString(value);
                }
            }
            public System.String JnsPeserta_kode
            {
                get
                {
                    System.String data = entity.JnsPeserta_kode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JnsPeserta_kode = null;
                    else entity.JnsPeserta_kode = Convert.ToString(value);
                }
            }
            public System.String JnsPeserta_nama
            {
                get
                {
                    System.String data = entity.JnsPeserta_nama;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JnsPeserta_nama = null;
                    else entity.JnsPeserta_nama = Convert.ToString(value);
                }
            }
            public System.String GolDarah
            {
                get
                {
                    System.String data = entity.GolDarah;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GolDarah = null;
                    else entity.GolDarah = Convert.ToString(value);
                }
            }
            public System.String NoHP
            {
                get
                {
                    System.String data = entity.NoHP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoHP = null;
                    else entity.NoHP = Convert.ToString(value);
                }
            }
            public System.String NoKTP
            {
                get
                {
                    System.String data = entity.NoKTP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NoKTP = null;
                    else entity.NoKTP = Convert.ToString(value);
                }
            }
            public System.String Aktif
            {
                get
                {
                    System.Boolean? data = entity.Aktif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Aktif = null;
                    else entity.Aktif = Convert.ToBoolean(value);
                }
            }
            public System.String KetAktif
            {
                get
                {
                    System.String data = entity.KetAktif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KetAktif = null;
                    else entity.KetAktif = Convert.ToString(value);
                }
            }
            public System.String Asuransi_kdAsuransi
            {
                get
                {
                    System.String data = entity.Asuransi_kdAsuransi;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Asuransi_kdAsuransi = null;
                    else entity.Asuransi_kdAsuransi = Convert.ToString(value);
                }
            }
            public System.String Asuransi_nmAsuransi
            {
                get
                {
                    System.String data = entity.Asuransi_nmAsuransi;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Asuransi_nmAsuransi = null;
                    else entity.Asuransi_nmAsuransi = Convert.ToString(value);
                }
            }
            public System.String Asuransi_noAsuransi
            {
                get
                {
                    System.String data = entity.Asuransi_noAsuransi;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Asuransi_noAsuransi = null;
                    else entity.Asuransi_noAsuransi = Convert.ToString(value);
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
            private esBpjsPeserta entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBpjsPesertaQuery query)
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
                throw new Exception("esBpjsPeserta can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BpjsPeserta : esBpjsPeserta
    {
    }

    [Serializable]
    abstract public class esBpjsPesertaQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BpjsPesertaMetadata.Meta();
            }
        }

        public esQueryItem NoKartu
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.NoKartu, esSystemType.String);
            }
        }

        public esQueryItem Nama
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.Nama, esSystemType.String);
            }
        }

        public esQueryItem HubunganKeluarga
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.HubunganKeluarga, esSystemType.String);
            }
        }

        public esQueryItem Sex
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.Sex, esSystemType.String);
            }
        }

        public esQueryItem TglLahir
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.TglLahir, esSystemType.DateTime);
            }
        }

        public esQueryItem TglMulaiAktif
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.TglMulaiAktif, esSystemType.DateTime);
            }
        }

        public esQueryItem TglAkhirBerlaku
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.TglAkhirBerlaku, esSystemType.DateTime);
            }
        }

        public esQueryItem KdProviderPst_kdProvider
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.KdProviderPst_kdProvider, esSystemType.String);
            }
        }

        public esQueryItem KdProviderPst_nmProvider
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.KdProviderPst_nmProvider, esSystemType.String);
            }
        }

        public esQueryItem KdProviderGigi_kdProvider
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.KdProviderGigi_kdProvider, esSystemType.String);
            }
        }

        public esQueryItem KdProviderGigi_nmProvider
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.KdProviderGigi_nmProvider, esSystemType.String);
            }
        }

        public esQueryItem JnsKelas_kode
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.JnsKelas_kode, esSystemType.String);
            }
        }

        public esQueryItem JnsKelas_nama
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.JnsKelas_nama, esSystemType.String);
            }
        }

        public esQueryItem JnsPeserta_kode
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.JnsPeserta_kode, esSystemType.String);
            }
        }

        public esQueryItem JnsPeserta_nama
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.JnsPeserta_nama, esSystemType.String);
            }
        }

        public esQueryItem GolDarah
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.GolDarah, esSystemType.String);
            }
        }

        public esQueryItem NoHP
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.NoHP, esSystemType.String);
            }
        }

        public esQueryItem NoKTP
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.NoKTP, esSystemType.String);
            }
        }

        public esQueryItem Aktif
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.Aktif, esSystemType.Boolean);
            }
        }

        public esQueryItem KetAktif
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.KetAktif, esSystemType.String);
            }
        }

        public esQueryItem Asuransi_kdAsuransi
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.Asuransi_kdAsuransi, esSystemType.String);
            }
        }

        public esQueryItem Asuransi_nmAsuransi
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.Asuransi_nmAsuransi, esSystemType.String);
            }
        }

        public esQueryItem Asuransi_noAsuransi
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.Asuransi_noAsuransi, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BpjsPesertaMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BpjsPesertaCollection")]
    public partial class BpjsPesertaCollection : esBpjsPesertaCollection, IEnumerable<BpjsPeserta>
    {
        public BpjsPesertaCollection()
        {

        }

        public static implicit operator List<BpjsPeserta>(BpjsPesertaCollection coll)
        {
            List<BpjsPeserta> list = new List<BpjsPeserta>();

            foreach (BpjsPeserta emp in coll)
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
                return BpjsPesertaMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsPesertaQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BpjsPeserta(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BpjsPeserta();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BpjsPesertaQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsPesertaQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one record was loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(BpjsPesertaQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BpjsPeserta AddNew()
        {
            BpjsPeserta entity = base.AddNewEntity() as BpjsPeserta;

            return entity;
        }
        public BpjsPeserta FindByPrimaryKey(String noKartu)
        {
            return base.FindByPrimaryKey(noKartu) as BpjsPeserta;
        }

        #region IEnumerable< BpjsPeserta> Members

        IEnumerator<BpjsPeserta> IEnumerable<BpjsPeserta>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BpjsPeserta;
            }
        }

        #endregion

        private BpjsPesertaQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BpjsPeserta' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BpjsPeserta ({NoKartu})")]
    [Serializable]
    public partial class BpjsPeserta : esBpjsPeserta
    {
        public BpjsPeserta()
        {
        }

        public BpjsPeserta(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BpjsPesertaMetadata.Meta();
            }
        }

        override protected esBpjsPesertaQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsPesertaQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BpjsPesertaQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsPesertaQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one row is loaded.
        /// For an entity, an exception will be thrown
        /// if more than one row is loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(BpjsPesertaQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BpjsPesertaQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BpjsPesertaQuery : esBpjsPesertaQuery
    {
        public BpjsPesertaQuery()
        {

        }

        public BpjsPesertaQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BpjsPesertaQuery";
        }
    }

    [Serializable]
    public partial class BpjsPesertaMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BpjsPesertaMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.NoKartu, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.NoKartu;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.Nama, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.Nama;
            c.CharacterMaxLength = 200;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.HubunganKeluarga, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.HubunganKeluarga;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.Sex, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.Sex;
            c.CharacterMaxLength = 1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.TglLahir, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.TglLahir;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.TglMulaiAktif, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.TglMulaiAktif;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.TglAkhirBerlaku, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.TglAkhirBerlaku;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.KdProviderPst_kdProvider, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.KdProviderPst_kdProvider;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.KdProviderPst_nmProvider, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.KdProviderPst_nmProvider;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.KdProviderGigi_kdProvider, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.KdProviderGigi_kdProvider;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.KdProviderGigi_nmProvider, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.KdProviderGigi_nmProvider;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.JnsKelas_kode, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.JnsKelas_kode;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.JnsKelas_nama, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.JnsKelas_nama;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.JnsPeserta_kode, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.JnsPeserta_kode;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.JnsPeserta_nama, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.JnsPeserta_nama;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.GolDarah, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.GolDarah;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.NoHP, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.NoHP;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.NoKTP, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.NoKTP;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.Aktif, 18, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.Aktif;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.KetAktif, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.KetAktif;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.Asuransi_kdAsuransi, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.Asuransi_kdAsuransi;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.Asuransi_nmAsuransi, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.Asuransi_nmAsuransi;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.Asuransi_noAsuransi, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.Asuransi_noAsuransi;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.LastUpdateDateTime, 23, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsPesertaMetadata.ColumnNames.LastUpdateByUserID, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsPesertaMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BpjsPesertaMetadata Meta()
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
            public const string NoKartu = "NoKartu";
            public const string Nama = "Nama";
            public const string HubunganKeluarga = "HubunganKeluarga";
            public const string Sex = "Sex";
            public const string TglLahir = "TglLahir";
            public const string TglMulaiAktif = "TglMulaiAktif";
            public const string TglAkhirBerlaku = "TglAkhirBerlaku";
            public const string KdProviderPst_kdProvider = "KdProviderPst_kdProvider";
            public const string KdProviderPst_nmProvider = "KdProviderPst_nmProvider";
            public const string KdProviderGigi_kdProvider = "KdProviderGigi_kdProvider";
            public const string KdProviderGigi_nmProvider = "KdProviderGigi_nmProvider";
            public const string JnsKelas_kode = "JnsKelas_kode";
            public const string JnsKelas_nama = "JnsKelas_nama";
            public const string JnsPeserta_kode = "JnsPeserta_kode";
            public const string JnsPeserta_nama = "JnsPeserta_nama";
            public const string GolDarah = "GolDarah";
            public const string NoHP = "NoHP";
            public const string NoKTP = "NoKTP";
            public const string Aktif = "Aktif";
            public const string KetAktif = "KetAktif";
            public const string Asuransi_kdAsuransi = "Asuransi_kdAsuransi";
            public const string Asuransi_nmAsuransi = "Asuransi_nmAsuransi";
            public const string Asuransi_noAsuransi = "Asuransi_noAsuransi";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string NoKartu = "NoKartu";
            public const string Nama = "Nama";
            public const string HubunganKeluarga = "HubunganKeluarga";
            public const string Sex = "Sex";
            public const string TglLahir = "TglLahir";
            public const string TglMulaiAktif = "TglMulaiAktif";
            public const string TglAkhirBerlaku = "TglAkhirBerlaku";
            public const string KdProviderPst_kdProvider = "KdProviderPst_kdProvider";
            public const string KdProviderPst_nmProvider = "KdProviderPst_nmProvider";
            public const string KdProviderGigi_kdProvider = "KdProviderGigi_kdProvider";
            public const string KdProviderGigi_nmProvider = "KdProviderGigi_nmProvider";
            public const string JnsKelas_kode = "JnsKelas_kode";
            public const string JnsKelas_nama = "JnsKelas_nama";
            public const string JnsPeserta_kode = "JnsPeserta_kode";
            public const string JnsPeserta_nama = "JnsPeserta_nama";
            public const string GolDarah = "GolDarah";
            public const string NoHP = "NoHP";
            public const string NoKTP = "NoKTP";
            public const string Aktif = "Aktif";
            public const string KetAktif = "KetAktif";
            public const string Asuransi_kdAsuransi = "Asuransi_kdAsuransi";
            public const string Asuransi_nmAsuransi = "Asuransi_nmAsuransi";
            public const string Asuransi_noAsuransi = "Asuransi_noAsuransi";
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
            lock (typeof(BpjsPesertaMetadata))
            {
                if (BpjsPesertaMetadata.mapDelegates == null)
                {
                    BpjsPesertaMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BpjsPesertaMetadata.meta == null)
                {
                    BpjsPesertaMetadata.meta = new BpjsPesertaMetadata();
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

                meta.AddTypeMap("NoKartu", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Nama", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HubunganKeluarga", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Sex", new esTypeMap("char", "System.String"));
                meta.AddTypeMap("TglLahir", new esTypeMap("date", "System.DateTime"));
                meta.AddTypeMap("TglMulaiAktif", new esTypeMap("date", "System.DateTime"));
                meta.AddTypeMap("TglAkhirBerlaku", new esTypeMap("date", "System.DateTime"));
                meta.AddTypeMap("KdProviderPst_kdProvider", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KdProviderPst_nmProvider", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KdProviderGigi_kdProvider", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KdProviderGigi_nmProvider", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JnsKelas_kode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JnsKelas_nama", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JnsPeserta_kode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JnsPeserta_nama", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GolDarah", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoHP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NoKTP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Aktif", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("KetAktif", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Asuransi_kdAsuransi", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Asuransi_nmAsuransi", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Asuransi_noAsuransi", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "BpjsPeserta";
                meta.Destination = "BpjsPeserta";
                meta.spInsert = "proc_BpjsPesertaInsert";
                meta.spUpdate = "proc_BpjsPesertaUpdate";
                meta.spDelete = "proc_BpjsPesertaDelete";
                meta.spLoadAll = "proc_BpjsPesertaLoadAll";
                meta.spLoadByPrimaryKey = "proc_BpjsPesertaLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BpjsPesertaMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
