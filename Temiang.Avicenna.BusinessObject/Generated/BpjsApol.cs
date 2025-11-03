/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/31/2025 10:00:57 AM
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
    abstract public class esBpjsApolCollection : esEntityCollectionWAuditLog
    {
        public esBpjsApolCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BpjsApolCollection";
        }

        #region Query Logic
        protected void InitQuery(esBpjsApolQuery query)
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
            this.InitQuery(query as esBpjsApolQuery);
        }
        #endregion

        virtual public BpjsApol DetachEntity(BpjsApol entity)
        {
            return base.DetachEntity(entity) as BpjsApol;
        }

        virtual public BpjsApol AttachEntity(BpjsApol entity)
        {
            return base.AttachEntity(entity) as BpjsApol;
        }

        virtual public void Combine(BpjsApolCollection collection)
        {
            base.Combine(collection);
        }

        new public BpjsApol this[int index]
        {
            get
            {
                return base[index] as BpjsApol;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BpjsApol);
        }
    }

    [Serializable]
    abstract public class esBpjsApol : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBpjsApolQuery GetDynamicQuery()
        {
            return null;
        }

        public esBpjsApol()
        {
        }

        public esBpjsApol(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int32 iD)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 iD)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
        }

        private bool LoadByPrimaryKeyDynamic(Int32 iD)
        {
            esBpjsApolQuery query = this.GetDynamicQuery();
            query.Where(query.ID == iD);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int32 iD)
        {
            esParameters parms = new esParameters();
            parms.Add("ID", iD);
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
                        case "ID": this.str.ID = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
                        case "TGLSJP": this.str.TGLSJP = (string)value; break;
                        case "REFASALSJP": this.str.REFASALSJP = (string)value; break;
                        case "POLIRSP": this.str.POLIRSP = (string)value; break;
                        case "KDJNSOBAT": this.str.KDJNSOBAT = (string)value; break;
                        case "NORESEP": this.str.NORESEP = (string)value; break;
                        case "IDUSERSJP": this.str.IDUSERSJP = (string)value; break;
                        case "TGLRSP": this.str.TGLRSP = (string)value; break;
                        case "TGLPELRSP": this.str.TGLPELRSP = (string)value; break;
                        case "KDDOKTER": this.str.KDDOKTER = (string)value; break;
                        case "ITERASI": this.str.ITERASI = (string)value; break;
                        case "NosepKunjungan": this.str.NosepKunjungan = (string)value; break;
                        case "NOKARTU": this.str.NOKARTU = (string)value; break;
                        case "NAMA": this.str.NAMA = (string)value; break;
                        case "FASKESASAL": this.str.FASKESASAL = (string)value; break;
                        case "NOAPOTIK": this.str.NOAPOTIK = (string)value; break;
                        case "TGLRESEP": this.str.TGLRESEP = (string)value; break;
                        case "BYTAGRSP": this.str.BYTAGRSP = (string)value; break;
                        case "BYVERRSP": this.str.BYVERRSP = (string)value; break;
                        case "TGLENTRY": this.str.TGLENTRY = (string)value; break;
                        case "MetadataCode": this.str.MetadataCode = (string)value; break;
                        case "MetadataMessage": this.str.MetadataMessage = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ID":

                            if (value == null || value is System.Int32)
                                this.ID = (System.Int32?)value;
                            break;
                        case "TGLSJP":

                            if (value == null || value is System.DateTime)
                                this.TGLSJP = (System.DateTime?)value;
                            break;
                        case "KDJNSOBAT":

                            if (value == null || value is System.Byte)
                                this.KDJNSOBAT = (System.Byte?)value;
                            break;
                        case "TGLRSP":

                            if (value == null || value is System.DateTime)
                                this.TGLRSP = (System.DateTime?)value;
                            break;
                        case "TGLPELRSP":

                            if (value == null || value is System.DateTime)
                                this.TGLPELRSP = (System.DateTime?)value;
                            break;
                        case "ITERASI":

                            if (value == null || value is System.Byte)
                                this.ITERASI = (System.Byte?)value;
                            break;
                        case "TGLRESEP":

                            if (value == null || value is System.DateTime)
                                this.TGLRESEP = (System.DateTime?)value;
                            break;
                        case "BYTAGRSP":

                            if (value == null || value is System.Byte)
                                this.BYTAGRSP = (System.Byte?)value;
                            break;
                        case "BYVERRSP":

                            if (value == null || value is System.Byte)
                                this.BYVERRSP = (System.Byte?)value;
                            break;
                        case "TGLENTRY":

                            if (value == null || value is System.DateTime)
                                this.TGLENTRY = (System.DateTime?)value;
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
        /// Maps to BpjsApol.ID
        /// </summary>
        virtual public System.Int32? ID
        {
            get
            {
                return base.GetSystemInt32(BpjsApolMetadata.ColumnNames.ID);
            }

            set
            {
                base.SetSystemInt32(BpjsApolMetadata.ColumnNames.ID, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.PrescriptionNo
        /// </summary>
        virtual public System.String PrescriptionNo
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.PrescriptionNo);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.PrescriptionNo, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.TGLSJP
        /// </summary>
        virtual public System.DateTime? TGLSJP
        {
            get
            {
                return base.GetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLSJP);
            }

            set
            {
                base.SetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLSJP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.REFASALSJP
        /// </summary>
        virtual public System.String REFASALSJP
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.REFASALSJP);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.REFASALSJP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.POLIRSP
        /// </summary>
        virtual public System.String POLIRSP
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.POLIRSP);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.POLIRSP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.KDJNSOBAT
        /// </summary>
        virtual public System.Byte? KDJNSOBAT
        {
            get
            {
                return base.GetSystemByte(BpjsApolMetadata.ColumnNames.KDJNSOBAT);
            }

            set
            {
                base.SetSystemByte(BpjsApolMetadata.ColumnNames.KDJNSOBAT, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.NORESEP
        /// </summary>
        virtual public System.String NORESEP
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.NORESEP);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.NORESEP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.IDUSERSJP
        /// </summary>
        virtual public System.String IDUSERSJP
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.IDUSERSJP);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.IDUSERSJP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.TGLRSP
        /// </summary>
        virtual public System.DateTime? TGLRSP
        {
            get
            {
                return base.GetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLRSP);
            }

            set
            {
                base.SetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLRSP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.TGLPELRSP
        /// </summary>
        virtual public System.DateTime? TGLPELRSP
        {
            get
            {
                return base.GetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLPELRSP);
            }

            set
            {
                base.SetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLPELRSP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.KDDOKTER
        /// </summary>
        virtual public System.String KDDOKTER
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.KDDOKTER);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.KDDOKTER, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.ITERASI
        /// </summary>
        virtual public System.Byte? ITERASI
        {
            get
            {
                return base.GetSystemByte(BpjsApolMetadata.ColumnNames.ITERASI);
            }

            set
            {
                base.SetSystemByte(BpjsApolMetadata.ColumnNames.ITERASI, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.NOSEP_KUNJUNGAN
        /// </summary>
        virtual public System.String NosepKunjungan
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.NosepKunjungan);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.NosepKunjungan, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.NOKARTU
        /// </summary>
        virtual public System.String NOKARTU
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.NOKARTU);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.NOKARTU, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.NAMA
        /// </summary>
        virtual public System.String NAMA
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.NAMA);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.NAMA, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.FASKESASAL
        /// </summary>
        virtual public System.String FASKESASAL
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.FASKESASAL);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.FASKESASAL, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.NOAPOTIK
        /// </summary>
        virtual public System.String NOAPOTIK
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.NOAPOTIK);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.NOAPOTIK, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.TGLRESEP
        /// </summary>
        virtual public System.DateTime? TGLRESEP
        {
            get
            {
                return base.GetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLRESEP);
            }

            set
            {
                base.SetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLRESEP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.BYTAGRSP
        /// </summary>
        virtual public System.Byte? BYTAGRSP
        {
            get
            {
                return base.GetSystemByte(BpjsApolMetadata.ColumnNames.BYTAGRSP);
            }

            set
            {
                base.SetSystemByte(BpjsApolMetadata.ColumnNames.BYTAGRSP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.BYVERRSP
        /// </summary>
        virtual public System.Byte? BYVERRSP
        {
            get
            {
                return base.GetSystemByte(BpjsApolMetadata.ColumnNames.BYVERRSP);
            }

            set
            {
                base.SetSystemByte(BpjsApolMetadata.ColumnNames.BYVERRSP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.TGLENTRY
        /// </summary>
        virtual public System.DateTime? TGLENTRY
        {
            get
            {
                return base.GetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLENTRY);
            }

            set
            {
                base.SetSystemDateTime(BpjsApolMetadata.ColumnNames.TGLENTRY, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.METADATA_CODE
        /// </summary>
        virtual public System.String MetadataCode
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.MetadataCode);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.MetadataCode, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.METADATA_MESSAGE
        /// </summary>
        virtual public System.String MetadataMessage
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.MetadataMessage);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.MetadataMessage, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BpjsApolMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BpjsApolMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApol.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BpjsApolMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BpjsApolMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esBpjsApol entity)
            {
                this.entity = entity;
            }
            public System.String ID
            {
                get
                {
                    System.Int32? data = entity.ID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ID = null;
                    else entity.ID = Convert.ToInt32(value);
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
            public System.String PrescriptionNo
            {
                get
                {
                    System.String data = entity.PrescriptionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PrescriptionNo = null;
                    else entity.PrescriptionNo = Convert.ToString(value);
                }
            }
            public System.String TGLSJP
            {
                get
                {
                    System.DateTime? data = entity.TGLSJP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TGLSJP = null;
                    else entity.TGLSJP = Convert.ToDateTime(value);
                }
            }
            public System.String REFASALSJP
            {
                get
                {
                    System.String data = entity.REFASALSJP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.REFASALSJP = null;
                    else entity.REFASALSJP = Convert.ToString(value);
                }
            }
            public System.String POLIRSP
            {
                get
                {
                    System.String data = entity.POLIRSP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.POLIRSP = null;
                    else entity.POLIRSP = Convert.ToString(value);
                }
            }
            public System.String KDJNSOBAT
            {
                get
                {
                    System.Byte? data = entity.KDJNSOBAT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KDJNSOBAT = null;
                    else entity.KDJNSOBAT = Convert.ToByte(value);
                }
            }
            public System.String NORESEP
            {
                get
                {
                    System.String data = entity.NORESEP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NORESEP = null;
                    else entity.NORESEP = Convert.ToString(value);
                }
            }
            public System.String IDUSERSJP
            {
                get
                {
                    System.String data = entity.IDUSERSJP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IDUSERSJP = null;
                    else entity.IDUSERSJP = Convert.ToString(value);
                }
            }
            public System.String TGLRSP
            {
                get
                {
                    System.DateTime? data = entity.TGLRSP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TGLRSP = null;
                    else entity.TGLRSP = Convert.ToDateTime(value);
                }
            }
            public System.String TGLPELRSP
            {
                get
                {
                    System.DateTime? data = entity.TGLPELRSP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TGLPELRSP = null;
                    else entity.TGLPELRSP = Convert.ToDateTime(value);
                }
            }
            public System.String KDDOKTER
            {
                get
                {
                    System.String data = entity.KDDOKTER;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KDDOKTER = null;
                    else entity.KDDOKTER = Convert.ToString(value);
                }
            }
            public System.String ITERASI
            {
                get
                {
                    System.Byte? data = entity.ITERASI;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ITERASI = null;
                    else entity.ITERASI = Convert.ToByte(value);
                }
            }
            public System.String NosepKunjungan
            {
                get
                {
                    System.String data = entity.NosepKunjungan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NosepKunjungan = null;
                    else entity.NosepKunjungan = Convert.ToString(value);
                }
            }
            public System.String NOKARTU
            {
                get
                {
                    System.String data = entity.NOKARTU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NOKARTU = null;
                    else entity.NOKARTU = Convert.ToString(value);
                }
            }
            public System.String NAMA
            {
                get
                {
                    System.String data = entity.NAMA;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NAMA = null;
                    else entity.NAMA = Convert.ToString(value);
                }
            }
            public System.String FASKESASAL
            {
                get
                {
                    System.String data = entity.FASKESASAL;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FASKESASAL = null;
                    else entity.FASKESASAL = Convert.ToString(value);
                }
            }
            public System.String NOAPOTIK
            {
                get
                {
                    System.String data = entity.NOAPOTIK;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NOAPOTIK = null;
                    else entity.NOAPOTIK = Convert.ToString(value);
                }
            }
            public System.String TGLRESEP
            {
                get
                {
                    System.DateTime? data = entity.TGLRESEP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TGLRESEP = null;
                    else entity.TGLRESEP = Convert.ToDateTime(value);
                }
            }
            public System.String BYTAGRSP
            {
                get
                {
                    System.Byte? data = entity.BYTAGRSP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BYTAGRSP = null;
                    else entity.BYTAGRSP = Convert.ToByte(value);
                }
            }
            public System.String BYVERRSP
            {
                get
                {
                    System.Byte? data = entity.BYVERRSP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BYVERRSP = null;
                    else entity.BYVERRSP = Convert.ToByte(value);
                }
            }
            public System.String TGLENTRY
            {
                get
                {
                    System.DateTime? data = entity.TGLENTRY;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TGLENTRY = null;
                    else entity.TGLENTRY = Convert.ToDateTime(value);
                }
            }
            public System.String MetadataCode
            {
                get
                {
                    System.String data = entity.MetadataCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MetadataCode = null;
                    else entity.MetadataCode = Convert.ToString(value);
                }
            }
            public System.String MetadataMessage
            {
                get
                {
                    System.String data = entity.MetadataMessage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MetadataMessage = null;
                    else entity.MetadataMessage = Convert.ToString(value);
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
            private esBpjsApol entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBpjsApolQuery query)
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
                throw new Exception("esBpjsApol can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BpjsApol : esBpjsApol
    {
    }

    [Serializable]
    abstract public class esBpjsApolQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BpjsApolMetadata.Meta();
            }
        }

        public esQueryItem ID
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.ID, esSystemType.Int32);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem PrescriptionNo
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
            }
        }

        public esQueryItem TGLSJP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.TGLSJP, esSystemType.DateTime);
            }
        }

        public esQueryItem REFASALSJP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.REFASALSJP, esSystemType.String);
            }
        }

        public esQueryItem POLIRSP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.POLIRSP, esSystemType.String);
            }
        }

        public esQueryItem KDJNSOBAT
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.KDJNSOBAT, esSystemType.Byte);
            }
        }

        public esQueryItem NORESEP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.NORESEP, esSystemType.String);
            }
        }

        public esQueryItem IDUSERSJP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.IDUSERSJP, esSystemType.String);
            }
        }

        public esQueryItem TGLRSP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.TGLRSP, esSystemType.DateTime);
            }
        }

        public esQueryItem TGLPELRSP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.TGLPELRSP, esSystemType.DateTime);
            }
        }

        public esQueryItem KDDOKTER
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.KDDOKTER, esSystemType.String);
            }
        }

        public esQueryItem ITERASI
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.ITERASI, esSystemType.Byte);
            }
        }

        public esQueryItem NosepKunjungan
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.NosepKunjungan, esSystemType.String);
            }
        }

        public esQueryItem NOKARTU
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.NOKARTU, esSystemType.String);
            }
        }

        public esQueryItem NAMA
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.NAMA, esSystemType.String);
            }
        }

        public esQueryItem FASKESASAL
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.FASKESASAL, esSystemType.String);
            }
        }

        public esQueryItem NOAPOTIK
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.NOAPOTIK, esSystemType.String);
            }
        }

        public esQueryItem TGLRESEP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.TGLRESEP, esSystemType.DateTime);
            }
        }

        public esQueryItem BYTAGRSP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.BYTAGRSP, esSystemType.Byte);
            }
        }

        public esQueryItem BYVERRSP
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.BYVERRSP, esSystemType.Byte);
            }
        }

        public esQueryItem TGLENTRY
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.TGLENTRY, esSystemType.DateTime);
            }
        }

        public esQueryItem MetadataCode
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.MetadataCode, esSystemType.String);
            }
        }

        public esQueryItem MetadataMessage
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.MetadataMessage, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BpjsApolMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BpjsApolCollection")]
    public partial class BpjsApolCollection : esBpjsApolCollection, IEnumerable<BpjsApol>
    {
        public BpjsApolCollection()
        {

        }

        public static implicit operator List<BpjsApol>(BpjsApolCollection coll)
        {
            List<BpjsApol> list = new List<BpjsApol>();

            foreach (BpjsApol emp in coll)
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
                return BpjsApolMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsApolQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BpjsApol(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BpjsApol();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BpjsApolQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsApolQuery();
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
        public bool Load(BpjsApolQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BpjsApol AddNew()
        {
            BpjsApol entity = base.AddNewEntity() as BpjsApol;

            return entity;
        }
        public BpjsApol FindByPrimaryKey(Int32 iD)
        {
            return base.FindByPrimaryKey(iD) as BpjsApol;
        }

        #region IEnumerable< BpjsApol> Members

        IEnumerator<BpjsApol> IEnumerable<BpjsApol>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BpjsApol;
            }
        }

        #endregion

        private BpjsApolQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BpjsApol' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BpjsApol ({ID})")]
    [Serializable]
    public partial class BpjsApol : esBpjsApol
    {
        public BpjsApol()
        {
        }

        public BpjsApol(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BpjsApolMetadata.Meta();
            }
        }

        override protected esBpjsApolQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsApolQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BpjsApolQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsApolQuery();
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
        public bool Load(BpjsApolQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BpjsApolQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BpjsApolQuery : esBpjsApolQuery
    {
        public BpjsApolQuery()
        {

        }

        public BpjsApolQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BpjsApolQuery";
        }
    }

    [Serializable]
    public partial class BpjsApolMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BpjsApolMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.ID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = BpjsApolMetadata.PropertyNames.ID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 30;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.PrescriptionNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.PrescriptionNo;
            c.CharacterMaxLength = 30;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.TGLSJP, 3, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsApolMetadata.PropertyNames.TGLSJP;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.REFASALSJP, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.REFASALSJP;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.POLIRSP, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.POLIRSP;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.KDJNSOBAT, 6, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = BpjsApolMetadata.PropertyNames.KDJNSOBAT;
            c.NumericPrecision = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.NORESEP, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.NORESEP;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.IDUSERSJP, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.IDUSERSJP;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.TGLRSP, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsApolMetadata.PropertyNames.TGLRSP;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.TGLPELRSP, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsApolMetadata.PropertyNames.TGLPELRSP;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.KDDOKTER, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.KDDOKTER;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.ITERASI, 12, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = BpjsApolMetadata.PropertyNames.ITERASI;
            c.NumericPrecision = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.NosepKunjungan, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.NosepKunjungan;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.NOKARTU, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.NOKARTU;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.NAMA, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.NAMA;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.FASKESASAL, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.FASKESASAL;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.NOAPOTIK, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.NOAPOTIK;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.TGLRESEP, 18, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsApolMetadata.PropertyNames.TGLRESEP;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.BYTAGRSP, 19, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = BpjsApolMetadata.PropertyNames.BYTAGRSP;
            c.NumericPrecision = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.BYVERRSP, 20, typeof(System.Byte), esSystemType.Byte);
            c.PropertyName = BpjsApolMetadata.PropertyNames.BYVERRSP;
            c.NumericPrecision = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.TGLENTRY, 21, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsApolMetadata.PropertyNames.TGLENTRY;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.MetadataCode, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.MetadataCode;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.MetadataMessage, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.MetadataMessage;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.LastUpdateDateTime, 24, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsApolMetadata.PropertyNames.LastUpdateDateTime;
            c.HasDefault = true;
            c.Default = @"(getdate())";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolMetadata.ColumnNames.LastUpdateByUserID, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BpjsApolMetadata Meta()
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
            public const string ID = "ID";
            public const string RegistrationNo = "RegistrationNo";
            public const string PrescriptionNo = "PrescriptionNo";
            public const string TGLSJP = "TGLSJP";
            public const string REFASALSJP = "REFASALSJP";
            public const string POLIRSP = "POLIRSP";
            public const string KDJNSOBAT = "KDJNSOBAT";
            public const string NORESEP = "NORESEP";
            public const string IDUSERSJP = "IDUSERSJP";
            public const string TGLRSP = "TGLRSP";
            public const string TGLPELRSP = "TGLPELRSP";
            public const string KDDOKTER = "KDDOKTER";
            public const string ITERASI = "ITERASI";
            public const string NosepKunjungan = "NOSEP_KUNJUNGAN";
            public const string NOKARTU = "NOKARTU";
            public const string NAMA = "NAMA";
            public const string FASKESASAL = "FASKESASAL";
            public const string NOAPOTIK = "NOAPOTIK";
            public const string TGLRESEP = "TGLRESEP";
            public const string BYTAGRSP = "BYTAGRSP";
            public const string BYVERRSP = "BYVERRSP";
            public const string TGLENTRY = "TGLENTRY";
            public const string MetadataCode = "METADATA_CODE";
            public const string MetadataMessage = "METADATA_MESSAGE";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ID = "ID";
            public const string RegistrationNo = "RegistrationNo";
            public const string PrescriptionNo = "PrescriptionNo";
            public const string TGLSJP = "TGLSJP";
            public const string REFASALSJP = "REFASALSJP";
            public const string POLIRSP = "POLIRSP";
            public const string KDJNSOBAT = "KDJNSOBAT";
            public const string NORESEP = "NORESEP";
            public const string IDUSERSJP = "IDUSERSJP";
            public const string TGLRSP = "TGLRSP";
            public const string TGLPELRSP = "TGLPELRSP";
            public const string KDDOKTER = "KDDOKTER";
            public const string ITERASI = "ITERASI";
            public const string NosepKunjungan = "NosepKunjungan";
            public const string NOKARTU = "NOKARTU";
            public const string NAMA = "NAMA";
            public const string FASKESASAL = "FASKESASAL";
            public const string NOAPOTIK = "NOAPOTIK";
            public const string TGLRESEP = "TGLRESEP";
            public const string BYTAGRSP = "BYTAGRSP";
            public const string BYVERRSP = "BYVERRSP";
            public const string TGLENTRY = "TGLENTRY";
            public const string MetadataCode = "MetadataCode";
            public const string MetadataMessage = "MetadataMessage";
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
            lock (typeof(BpjsApolMetadata))
            {
                if (BpjsApolMetadata.mapDelegates == null)
                {
                    BpjsApolMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BpjsApolMetadata.meta == null)
                {
                    BpjsApolMetadata.meta = new BpjsApolMetadata();
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

                meta.AddTypeMap("ID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TGLSJP", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("REFASALSJP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("POLIRSP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KDJNSOBAT", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("NORESEP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IDUSERSJP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TGLRSP", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("TGLPELRSP", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("KDDOKTER", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ITERASI", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("NosepKunjungan", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NOKARTU", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NAMA", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FASKESASAL", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NOAPOTIK", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TGLRESEP", new esTypeMap("date", "System.DateTime"));
                meta.AddTypeMap("BYTAGRSP", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("BYVERRSP", new esTypeMap("tinyint", "System.Byte"));
                meta.AddTypeMap("TGLENTRY", new esTypeMap("date", "System.DateTime"));
                meta.AddTypeMap("MetadataCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MetadataMessage", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "BpjsApol";
                meta.Destination = "BpjsApol";
                meta.spInsert = "proc_BpjsApolInsert";
                meta.spUpdate = "proc_BpjsApolUpdate";
                meta.spDelete = "proc_BpjsApolDelete";
                meta.spLoadAll = "proc_BpjsApolLoadAll";
                meta.spLoadByPrimaryKey = "proc_BpjsApolLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BpjsApolMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
