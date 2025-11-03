/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:14 PM
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
    abstract public class esRlTxReport32v2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport32v2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport32v2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport32v2025Query query)
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
            this.InitQuery(query as esRlTxReport32v2025Query);
        }
        #endregion

        virtual public RlTxReport32v2025 DetachEntity(RlTxReport32v2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport32v2025;
        }

        virtual public RlTxReport32v2025 AttachEntity(RlTxReport32v2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport32v2025;
        }

        virtual public void Combine(RlTxReport32v2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport32v2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport32v2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport32v2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport32v2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport32v2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport32v2025()
        {

        }

        public esRlTxReport32v2025(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            esRlTxReport32v2025Query query = this.GetDynamicQuery();
            query.Where(query.RlTxReportNo == rlTxReportNo, query.RlMasterReportItemID == rlMasterReportItemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            esParameters parms = new esParameters();
            parms.Add("RlTxReportNo", rlTxReportNo); parms.Add("RlMasterReportItemID", rlMasterReportItemID);
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
                        case "RlTxReportNo": this.str.RlTxReportNo = (string)value; break;
                        case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
                        case "PasienAwal": this.str.PasienAwal = (string)value; break;
                        case "PasienMasuk": this.str.PasienMasuk = (string)value; break;
                        case "PasienKeluarHidup": this.str.PasienKeluarHidup = (string)value; break;
                        case "PasienLKeluarMatiK48": this.str.PasienLKeluarMatiK48 = (string)value; break;
                        case "PasienPKeluarMatiK48": this.str.PasienPKeluarMatiK48 = (string)value; break;
                        case "PasienLKeluarMatiL48": this.str.PasienLKeluarMatiL48 = (string)value; break;
                        case "PasienPKeluarMatiL48": this.str.PasienPKeluarMatiL48 = (string)value; break;
                        case "LamaRawat": this.str.LamaRawat = (string)value; break;
                        case "PasienAkhir": this.str.PasienAkhir = (string)value; break;
                        case "HariRawat": this.str.HariRawat = (string)value; break;
                        case "Vvip": this.str.Vvip = (string)value; break;
                        case "Vip": this.str.Vip = (string)value; break;
                        case "I": this.str.I = (string)value; break;
                        case "Ii": this.str.Ii = (string)value; break;
                        case "Iii": this.str.Iii = (string)value; break;
                        case "KelasKhusus": this.str.KelasKhusus = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "PasienPindahan": this.str.PasienPindahan = (string)value; break;
                        case "PasienDipindahkan": this.str.PasienDipindahkan = (string)value; break;
                        case "AlokasiTT": this.str.AlokasiTT = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RlMasterReportItemID":

                            if (value == null || value is System.Int32)
                                this.RlMasterReportItemID = (System.Int32?)value;
                            break;

                        case "PasienAwal":

                            if (value == null || value is System.Int32)
                                this.PasienAwal = (System.Int32?)value;
                            break;

                        case "PasienMasuk":

                            if (value == null || value is System.Int32)
                                this.PasienMasuk = (System.Int32?)value;
                            break;

                        case "PasienKeluarHidup":

                            if (value == null || value is System.Int32)
                                this.PasienKeluarHidup = (System.Int32?)value;
                            break;

                        case "PasienLKeluarMatiK48":

                            if (value == null || value is System.Int32)
                                this.PasienLKeluarMatiK48 = (System.Int32?)value;
                            break;

                        case "PasienPKeluarMatiK48":

                            if (value == null || value is System.Int32)
                                this.PasienPKeluarMatiK48 = (System.Int32?)value;
                            break;

                        case "PasienLKeluarMatiL48":

                            if (value == null || value is System.Int32)
                                this.PasienLKeluarMatiL48 = (System.Int32?)value;
                            break;

                        case "PasienPKeluarMatiL48":

                            if (value == null || value is System.Int32)
                                this.PasienPKeluarMatiL48 = (System.Int32?)value;
                            break;

                        case "LamaRawat":

                            if (value == null || value is System.Int32)
                                this.LamaRawat = (System.Int32?)value;
                            break;

                        case "PasienAkhir":

                            if (value == null || value is System.Int32)
                                this.PasienAkhir = (System.Int32?)value;
                            break;

                        case "HariRawat":

                            if (value == null || value is System.Int32)
                                this.HariRawat = (System.Int32?)value;
                            break;

                        case "Vvip":

                            if (value == null || value is System.Int32)
                                this.Vvip = (System.Int32?)value;
                            break;

                        case "Vip":

                            if (value == null || value is System.Int32)
                                this.Vip = (System.Int32?)value;
                            break;

                        case "I":

                            if (value == null || value is System.Int32)
                                this.I = (System.Int32?)value;
                            break;

                        case "Ii":

                            if (value == null || value is System.Int32)
                                this.Ii = (System.Int32?)value;
                            break;

                        case "Iii":

                            if (value == null || value is System.Int32)
                                this.Iii = (System.Int32?)value;
                            break;

                        case "KelasKhusus":

                            if (value == null || value is System.Int32)
                                this.KelasKhusus = (System.Int32?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "PasienPindahan":

                            if (value == null || value is System.Int32)
                                this.PasienPindahan = (System.Int32?)value;
                            break;
                        case "PasienDipindahkan":

                            if (value == null || value is System.Int32)
                                this.PasienDipindahkan = (System.Int32?)value;
                            break;
                        case "AlokasiTT":

                            if (value == null || value is System.Int32)
                                this.AlokasiTT = (System.Int32?)value;
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
        /// Maps to RlTxReport3_2v2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport32v2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport32v2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienAwal
        /// </summary>
        virtual public System.Int32? PasienAwal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienAwal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienAwal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienMasuk
        /// </summary>
        virtual public System.Int32? PasienMasuk
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienMasuk);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienMasuk, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienKeluarHidup
        /// </summary>
        virtual public System.Int32? PasienKeluarHidup
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienKeluarHidup);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienKeluarHidup, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienLKeluarMatiK48
        /// </summary>
        virtual public System.Int32? PasienLKeluarMatiK48
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienLKeluarMatiK48);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienLKeluarMatiK48, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienPKeluarMatiK48
        /// </summary>
        virtual public System.Int32? PasienPKeluarMatiK48
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienPKeluarMatiK48);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienPKeluarMatiK48, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienLKeluarMatiL48
        /// </summary>
        virtual public System.Int32? PasienLKeluarMatiL48
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienLKeluarMatiL48);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienLKeluarMatiL48, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienPKeluarMatiL48
        /// </summary>
        virtual public System.Int32? PasienPKeluarMatiL48
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienPKeluarMatiL48);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienPKeluarMatiL48, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.LamaRawat
        /// </summary>
        virtual public System.Int32? LamaRawat
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.LamaRawat);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.LamaRawat, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienAkhir
        /// </summary>
        virtual public System.Int32? PasienAkhir
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienAkhir);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienAkhir, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.HariRawat
        /// </summary>
        virtual public System.Int32? HariRawat
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.HariRawat);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.HariRawat, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.Vvip
        /// </summary>
        virtual public System.Int32? Vvip
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.Vvip);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.Vvip, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.Vip
        /// </summary>
        virtual public System.Int32? Vip
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.Vip);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.Vip, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.I
        /// </summary>
        virtual public System.Int32? I
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.I);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.I, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.Ii
        /// </summary>
        virtual public System.Int32? Ii
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.Ii);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.Ii, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.Iii
        /// </summary>
        virtual public System.Int32? Iii
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.Iii);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.Iii, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.KelasKhusus
        /// </summary>
        virtual public System.Int32? KelasKhusus
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.KelasKhusus);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.KelasKhusus, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport32v2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport32v2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport32v2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport32v2025Metadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienPindahan
        /// </summary>
        virtual public System.Int32? PasienPindahan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienPindahan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienPindahan, value);
            }
        }
        /// <summary>
        /// Maps to RlTxReport3_2v2025.PasienDipindahkan
        /// </summary>
        virtual public System.Int32? PasienDipindahkan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienDipindahkan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.PasienDipindahkan, value);
            }
        }
        /// <summary>
        /// Maps to RlTxReport3_2v2025.AlokasiTT
        /// </summary>
        virtual public System.Int32? AlokasiTT
        {
            get
            {
                return base.GetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.AlokasiTT);
            }

            set
            {
                base.SetSystemInt32(RlTxReport32v2025Metadata.ColumnNames.AlokasiTT, value);
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
            public esStrings(esRlTxReport32v2025 entity)
            {
                this.entity = entity;
            }


            public System.String RlTxReportNo
            {
                get
                {
                    System.String data = entity.RlTxReportNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlTxReportNo = null;
                    else entity.RlTxReportNo = Convert.ToString(value);
                }
            }

            public System.String RlMasterReportItemID
            {
                get
                {
                    System.Int32? data = entity.RlMasterReportItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
                    else entity.RlMasterReportItemID = Convert.ToInt32(value);
                }
            }

            public System.String PasienAwal
            {
                get
                {
                    System.Int32? data = entity.PasienAwal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienAwal = null;
                    else entity.PasienAwal = Convert.ToInt32(value);
                }
            }

            public System.String PasienMasuk
            {
                get
                {
                    System.Int32? data = entity.PasienMasuk;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienMasuk = null;
                    else entity.PasienMasuk = Convert.ToInt32(value);
                }
            }

            public System.String PasienKeluarHidup
            {
                get
                {
                    System.Int32? data = entity.PasienKeluarHidup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienKeluarHidup = null;
                    else entity.PasienKeluarHidup = Convert.ToInt32(value);
                }
            }

            public System.String PasienLKeluarMatiK48
            {
                get
                {
                    System.Int32? data = entity.PasienLKeluarMatiK48;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienLKeluarMatiK48 = null;
                    else entity.PasienLKeluarMatiK48 = Convert.ToInt32(value);
                }
            }

            public System.String PasienPKeluarMatiK48
            {
                get
                {
                    System.Int32? data = entity.PasienPKeluarMatiK48;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienPKeluarMatiK48 = null;
                    else entity.PasienPKeluarMatiK48 = Convert.ToInt32(value);
                }
            }

            public System.String PasienLKeluarMatiL48
            {
                get
                {
                    System.Int32? data = entity.PasienLKeluarMatiL48;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienLKeluarMatiL48 = null;
                    else entity.PasienLKeluarMatiL48 = Convert.ToInt32(value);
                }
            }

            public System.String PasienPKeluarMatiL48
            {
                get
                {
                    System.Int32? data = entity.PasienPKeluarMatiL48;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienPKeluarMatiL48 = null;
                    else entity.PasienPKeluarMatiL48 = Convert.ToInt32(value);
                }
            }

            public System.String LamaRawat
            {
                get
                {
                    System.Int32? data = entity.LamaRawat;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LamaRawat = null;
                    else entity.LamaRawat = Convert.ToInt32(value);
                }
            }

            public System.String PasienAkhir
            {
                get
                {
                    System.Int32? data = entity.PasienAkhir;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienAkhir = null;
                    else entity.PasienAkhir = Convert.ToInt32(value);
                }
            }

            public System.String HariRawat
            {
                get
                {
                    System.Int32? data = entity.HariRawat;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HariRawat = null;
                    else entity.HariRawat = Convert.ToInt32(value);
                }
            }

            public System.String Vvip
            {
                get
                {
                    System.Int32? data = entity.Vvip;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Vvip = null;
                    else entity.Vvip = Convert.ToInt32(value);
                }
            }

            public System.String Vip
            {
                get
                {
                    System.Int32? data = entity.Vip;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Vip = null;
                    else entity.Vip = Convert.ToInt32(value);
                }
            }

            public System.String I
            {
                get
                {
                    System.Int32? data = entity.I;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.I = null;
                    else entity.I = Convert.ToInt32(value);
                }
            }

            public System.String Ii
            {
                get
                {
                    System.Int32? data = entity.Ii;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Ii = null;
                    else entity.Ii = Convert.ToInt32(value);
                }
            }

            public System.String Iii
            {
                get
                {
                    System.Int32? data = entity.Iii;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Iii = null;
                    else entity.Iii = Convert.ToInt32(value);
                }
            }

            public System.String KelasKhusus
            {
                get
                {
                    System.Int32? data = entity.KelasKhusus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KelasKhusus = null;
                    else entity.KelasKhusus = Convert.ToInt32(value);
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

            public System.String PasienPindahan
            {
                get
                {
                    System.Int32? data = entity.PasienPindahan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienPindahan = null;
                    else entity.PasienPindahan = Convert.ToInt32(value);
                }
            }
            public System.String PasienDipindahkan
            {
                get
                {
                    System.Int32? data = entity.PasienDipindahkan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienDipindahkan = null;
                    else entity.PasienDipindahkan = Convert.ToInt32(value);
                }
            }
            public System.String AlokasiTT
            {
                get
                {
                    System.Int32? data = entity.AlokasiTT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.AlokasiTT = null;
                    else entity.AlokasiTT = Convert.ToInt32(value);
                }
            }


            private esRlTxReport32v2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport32v2025Query query)
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
                throw new Exception("esRlTxReport32v2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport32v2025 : esRlTxReport32v2025
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
    abstract public class esRlTxReport32v2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport32v2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem PasienAwal
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienAwal, esSystemType.Int32);
            }
        }

        public esQueryItem PasienMasuk
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienMasuk, esSystemType.Int32);
            }
        }

        public esQueryItem PasienKeluarHidup
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienKeluarHidup, esSystemType.Int32);
            }
        }

        public esQueryItem PasienLKeluarMatiK48
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienLKeluarMatiK48, esSystemType.Int32);
            }
        }

        public esQueryItem PasienPKeluarMatiK48
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienPKeluarMatiK48, esSystemType.Int32);
            }
        }

        public esQueryItem PasienLKeluarMatiL48
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienLKeluarMatiL48, esSystemType.Int32);
            }
        }
        public esQueryItem PasienPKeluarMatiL48
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienPKeluarMatiL48, esSystemType.Int32);
            }
        }

        public esQueryItem LamaRawat
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.LamaRawat, esSystemType.Int32);
            }
        }

        public esQueryItem PasienAkhir
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienAkhir, esSystemType.Int32);
            }
        }

        public esQueryItem HariRawat
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.HariRawat, esSystemType.Int32);
            }
        }

        public esQueryItem Vvip
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.Vvip, esSystemType.Int32);
            }
        }

        public esQueryItem Vip
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.Vip, esSystemType.Int32);
            }
        }

        public esQueryItem I
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.I, esSystemType.Int32);
            }
        }

        public esQueryItem Ii
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.Ii, esSystemType.Int32);
            }
        }

        public esQueryItem Iii
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.Iii, esSystemType.Int32);
            }
        }

        public esQueryItem KelasKhusus
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.KelasKhusus, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PasienPindahan
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienPindahan, esSystemType.Int32);
            }
        }

        public esQueryItem PasienDipindahkan
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.PasienDipindahkan, esSystemType.Int32);
            }
        }
        public esQueryItem AlokasiTT
        {
            get
            {
                return new esQueryItem(this, RlTxReport32v2025Metadata.ColumnNames.AlokasiTT, esSystemType.Int32);
            }
        }


    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport32v2025Collection")]
    public partial class RlTxReport32v2025Collection : esRlTxReport32v2025Collection, IEnumerable<RlTxReport32v2025>
    {
        public RlTxReport32v2025Collection()
        {

        }

        public static implicit operator List<RlTxReport32v2025>(RlTxReport32v2025Collection coll)
        {
            List<RlTxReport32v2025> list = new List<RlTxReport32v2025>();

            foreach (RlTxReport32v2025 emp in coll)
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
                return RlTxReport32v2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport32v2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport32v2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport32v2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport32v2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport32v2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport32v2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport32v2025 AddNew()
        {
            RlTxReport32v2025 entity = base.AddNewEntity() as RlTxReport32v2025;

            return entity;
        }

        public RlTxReport32v2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport32v2025;
        }


        #region IEnumerable<RlTxReport32v2025> Members

        IEnumerator<RlTxReport32v2025> IEnumerable<RlTxReport32v2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport32v2025;
            }
        }

        #endregion

        private RlTxReport32v2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_2v2025' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport32v2025 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport32v2025 : esRlTxReport32v2025
    {
        public RlTxReport32v2025()
        {

        }

        public RlTxReport32v2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport32v2025Metadata.Meta();
            }
        }



        override protected esRlTxReport32v2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport32v2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport32v2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport32v2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport32v2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport32v2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport32v2025Query : esRlTxReport32v2025Query
    {
        public RlTxReport32v2025Query()
        {

        }

        public RlTxReport32v2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport32v2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport32v2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport32v2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienAwal, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienAwal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienMasuk, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienMasuk;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienKeluarHidup, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienKeluarHidup;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienLKeluarMatiK48, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienLKeluarMatiK48;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienPKeluarMatiK48, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienPKeluarMatiK48;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienLKeluarMatiL48, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienLKeluarMatiL48;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienPKeluarMatiL48, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienPKeluarMatiL48;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.LamaRawat, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.LamaRawat;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienAkhir, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienAkhir;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.HariRawat, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.HariRawat;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.Vvip, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.Vvip;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.Vip, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.Vip;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.I, 12, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.I;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.Ii, 13, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.Ii;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.Iii, 14, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.Iii;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.KelasKhusus, 15, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.KelasKhusus;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienPindahan, 18, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienPindahan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.PasienDipindahkan, 19, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.PasienDipindahkan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport32v2025Metadata.ColumnNames.AlokasiTT, 20, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport32v2025Metadata.PropertyNames.AlokasiTT;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport32v2025Metadata Meta()
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
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string PasienAwal = "PasienAwal";
            public const string PasienMasuk = "PasienMasuk";
            public const string PasienKeluarHidup = "PasienKeluarHidup";
            public const string PasienLKeluarMatiK48 = "PasienLKeluarMatiK48";
            public const string PasienPKeluarMatiK48 = "PasienPKeluarMatiK48";
            public const string PasienLKeluarMatiL48 = "PasienLKeluarMatiL48";
            public const string PasienPKeluarMatiL48 = "PasienPKeluarMatiL48";
            public const string LamaRawat = "LamaRawat";
            public const string PasienAkhir = "PasienAkhir";
            public const string HariRawat = "HariRawat";
            public const string Vvip = "Vvip";
            public const string Vip = "Vip";
            public const string I = "I";
            public const string Ii = "Ii";
            public const string Iii = "Iii";
            public const string KelasKhusus = "KelasKhusus";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PasienPindahan = "PasienPindahan";
            public const string PasienDipindahkan = "PasienDipindahkan";
            public const string AlokasiTT = "AlokasiTT";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string PasienAwal = "PasienAwal";
            public const string PasienMasuk = "PasienMasuk";
            public const string PasienKeluarHidup = "PasienKeluarHidup";
            public const string PasienLKeluarMatiK48 = "PasienLKeluarMatiK48";
            public const string PasienPKeluarMatiK48 = "PasienPKeluarMatiK48";
            public const string PasienLKeluarMatiL48 = "PasienLKeluarMatiL48";
            public const string PasienPKeluarMatiL48 = "PasienPKeluarMatiL48";
            public const string LamaRawat = "LamaRawat";
            public const string PasienAkhir = "PasienAkhir";
            public const string HariRawat = "HariRawat";
            public const string Vvip = "Vvip";
            public const string Vip = "Vip";
            public const string I = "I";
            public const string Ii = "Ii";
            public const string Iii = "Iii";
            public const string KelasKhusus = "KelasKhusus";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string PasienPindahan = "PasienPindahan";
            public const string PasienDipindahkan = "PasienDipindahkan";
            public const string AlokasiTT = "AlokasiTT";
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
            lock (typeof(RlTxReport32v2025Metadata))
            {
                if (RlTxReport32v2025Metadata.mapDelegates == null)
                {
                    RlTxReport32v2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport32v2025Metadata.meta == null)
                {
                    RlTxReport32v2025Metadata.meta = new RlTxReport32v2025Metadata();
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


                meta.AddTypeMap("RlTxReportNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienAwal", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienMasuk", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienKeluarHidup", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienLKeluarMatiK48", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienPKeluarMatiK48", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienLKeluarMatiL48", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienPKeluarMatiL48", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LamaRawat", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienAkhir", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HariRawat", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Vvip", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Vip", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("I", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Ii", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Iii", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KelasKhusus", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PasienPindahan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienDipindahkan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("AlokasiTT", new esTypeMap("int", "System.Int32"));



                meta.Source = "RlTxReport3_2V2025";
                meta.Destination = "RlTxReport3_2V2025";

                meta.spInsert = "proc_RlTxReport3_2V2025Insert";
                meta.spUpdate = "proc_RlTxReport3_2V2025Update";
                meta.spDelete = "proc_RlTxReport3_2V2025Delete";
                meta.spLoadAll = "proc_RlTxReport3_2V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_2V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport32v2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
