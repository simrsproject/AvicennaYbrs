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
    abstract public class esRlTxReport31Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport31Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport31Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport31Query query)
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
            this.InitQuery(query as esRlTxReport31Query);
        }
        #endregion

        virtual public RlTxReport31 DetachEntity(RlTxReport31 entity)
        {
            return base.DetachEntity(entity) as RlTxReport31;
        }

        virtual public RlTxReport31 AttachEntity(RlTxReport31 entity)
        {
            return base.AttachEntity(entity) as RlTxReport31;
        }

        virtual public void Combine(RlTxReport31Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport31 this[int index]
        {
            get
            {
                return base[index] as RlTxReport31;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport31);
        }
    }



    [Serializable]
    abstract public class esRlTxReport31 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport31Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport31()
        {

        }

        public esRlTxReport31(DataRow row)
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
            esRlTxReport31Query query = this.GetDynamicQuery();
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
                        case "PasienKeluarMatiK48": this.str.PasienKeluarMatiK48 = (string)value; break;
                        case "PasienKeluarMatiL48": this.str.PasienKeluarMatiL48 = (string)value; break;
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

                        case "PasienKeluarMatiK48":

                            if (value == null || value is System.Int32)
                                this.PasienKeluarMatiK48 = (System.Int32?)value;
                            break;

                        case "PasienKeluarMatiL48":

                            if (value == null || value is System.Int32)
                                this.PasienKeluarMatiL48 = (System.Int32?)value;
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
        /// Maps to RlTxReport3_1.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport31Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport31Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.PasienAwal
        /// </summary>
        virtual public System.Int32? PasienAwal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienAwal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienAwal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.PasienMasuk
        /// </summary>
        virtual public System.Int32? PasienMasuk
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienMasuk);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienMasuk, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.PasienKeluarHidup
        /// </summary>
        virtual public System.Int32? PasienKeluarHidup
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienKeluarHidup);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienKeluarHidup, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.PasienKeluarMatiK48
        /// </summary>
        virtual public System.Int32? PasienKeluarMatiK48
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienKeluarMatiK48);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienKeluarMatiK48, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.PasienKeluarMatiL48
        /// </summary>
        virtual public System.Int32? PasienKeluarMatiL48
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienKeluarMatiL48);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienKeluarMatiL48, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.LamaRawat
        /// </summary>
        virtual public System.Int32? LamaRawat
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.LamaRawat);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.LamaRawat, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.PasienAkhir
        /// </summary>
        virtual public System.Int32? PasienAkhir
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienAkhir);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienAkhir, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.HariRawat
        /// </summary>
        virtual public System.Int32? HariRawat
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.HariRawat);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.HariRawat, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.Vvip
        /// </summary>
        virtual public System.Int32? Vvip
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.Vvip);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.Vvip, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.Vip
        /// </summary>
        virtual public System.Int32? Vip
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.Vip);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.Vip, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.I
        /// </summary>
        virtual public System.Int32? I
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.I);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.I, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.Ii
        /// </summary>
        virtual public System.Int32? Ii
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.Ii);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.Ii, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.Iii
        /// </summary>
        virtual public System.Int32? Iii
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.Iii);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.Iii, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.KelasKhusus
        /// </summary>
        virtual public System.Int32? KelasKhusus
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.KelasKhusus);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.KelasKhusus, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport31Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport31Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport31Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport31Metadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1.PasienPindahan
        /// </summary>
        virtual public System.Int32? PasienPindahan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienPindahan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienPindahan, value);
            }
        }
        /// <summary>
        /// Maps to RlTxReport3_1.PasienDipindahkan
        /// </summary>
        virtual public System.Int32? PasienDipindahkan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienDipindahkan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31Metadata.ColumnNames.PasienDipindahkan, value);
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
            public esStrings(esRlTxReport31 entity)
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

            public System.String PasienKeluarMatiK48
            {
                get
                {
                    System.Int32? data = entity.PasienKeluarMatiK48;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienKeluarMatiK48 = null;
                    else entity.PasienKeluarMatiK48 = Convert.ToInt32(value);
                }
            }

            public System.String PasienKeluarMatiL48
            {
                get
                {
                    System.Int32? data = entity.PasienKeluarMatiL48;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PasienKeluarMatiL48 = null;
                    else entity.PasienKeluarMatiL48 = Convert.ToInt32(value);
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


            private esRlTxReport31 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport31Query query)
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
                throw new Exception("esRlTxReport31 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport31 : esRlTxReport31
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
    abstract public class esRlTxReport31Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport31Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem PasienAwal
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.PasienAwal, esSystemType.Int32);
            }
        }

        public esQueryItem PasienMasuk
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.PasienMasuk, esSystemType.Int32);
            }
        }

        public esQueryItem PasienKeluarHidup
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.PasienKeluarHidup, esSystemType.Int32);
            }
        }

        public esQueryItem PasienKeluarMatiK48
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.PasienKeluarMatiK48, esSystemType.Int32);
            }
        }

        public esQueryItem PasienKeluarMatiL48
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.PasienKeluarMatiL48, esSystemType.Int32);
            }
        }

        public esQueryItem LamaRawat
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.LamaRawat, esSystemType.Int32);
            }
        }

        public esQueryItem PasienAkhir
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.PasienAkhir, esSystemType.Int32);
            }
        }

        public esQueryItem HariRawat
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.HariRawat, esSystemType.Int32);
            }
        }

        public esQueryItem Vvip
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.Vvip, esSystemType.Int32);
            }
        }

        public esQueryItem Vip
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.Vip, esSystemType.Int32);
            }
        }

        public esQueryItem I
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.I, esSystemType.Int32);
            }
        }

        public esQueryItem Ii
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.Ii, esSystemType.Int32);
            }
        }

        public esQueryItem Iii
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.Iii, esSystemType.Int32);
            }
        }

        public esQueryItem KelasKhusus
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.KelasKhusus, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem PasienPindahan
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.PasienPindahan, esSystemType.Int32);
            }
        }

        public esQueryItem PasienDipindahkan
        {
            get
            {
                return new esQueryItem(this, RlTxReport31Metadata.ColumnNames.PasienDipindahkan, esSystemType.Int32);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport31Collection")]
    public partial class RlTxReport31Collection : esRlTxReport31Collection, IEnumerable<RlTxReport31>
    {
        public RlTxReport31Collection()
        {

        }

        public static implicit operator List<RlTxReport31>(RlTxReport31Collection coll)
        {
            List<RlTxReport31> list = new List<RlTxReport31>();

            foreach (RlTxReport31 emp in coll)
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
                return RlTxReport31Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport31Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport31(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport31();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport31Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport31Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport31Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport31 AddNew()
        {
            RlTxReport31 entity = base.AddNewEntity() as RlTxReport31;

            return entity;
        }

        public RlTxReport31 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport31;
        }


        #region IEnumerable<RlTxReport31> Members

        IEnumerator<RlTxReport31> IEnumerable<RlTxReport31>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport31;
            }
        }

        #endregion

        private RlTxReport31Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_1' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport31 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport31 : esRlTxReport31
    {
        public RlTxReport31()
        {

        }

        public RlTxReport31(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport31Metadata.Meta();
            }
        }



        override protected esRlTxReport31Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport31Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport31Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport31Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport31Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport31Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport31Query : esRlTxReport31Query
    {
        public RlTxReport31Query()
        {

        }

        public RlTxReport31Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport31Query";
        }


    }


    [Serializable]
    public partial class RlTxReport31Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport31Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.PasienAwal, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.PasienAwal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.PasienMasuk, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.PasienMasuk;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.PasienKeluarHidup, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.PasienKeluarHidup;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.PasienKeluarMatiK48, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.PasienKeluarMatiK48;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.PasienKeluarMatiL48, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.PasienKeluarMatiL48;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.LamaRawat, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.LamaRawat;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.PasienAkhir, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.PasienAkhir;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.HariRawat, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.HariRawat;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.Vvip, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.Vvip;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.Vip, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.Vip;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.I, 12, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.I;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.Ii, 13, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.Ii;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.Iii, 14, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.Iii;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.KelasKhusus, 15, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.KelasKhusus;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.PasienPindahan, 18, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.PasienPindahan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31Metadata.ColumnNames.PasienDipindahkan, 19, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31Metadata.PropertyNames.PasienDipindahkan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport31Metadata Meta()
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
            public const string PasienKeluarMatiK48 = "PasienKeluarMatiK48";
            public const string PasienKeluarMatiL48 = "PasienKeluarMatiL48";
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
            public const string PasienKeluarMatiK48 = "PasienKeluarMatiK48";
            public const string PasienKeluarMatiL48 = "PasienKeluarMatiL48";
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
            lock (typeof(RlTxReport31Metadata))
            {
                if (RlTxReport31Metadata.mapDelegates == null)
                {
                    RlTxReport31Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport31Metadata.meta == null)
                {
                    RlTxReport31Metadata.meta = new RlTxReport31Metadata();
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
                meta.AddTypeMap("PasienKeluarMatiK48", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PasienKeluarMatiL48", new esTypeMap("int", "System.Int32"));
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



                meta.Source = "RlTxReport3_1";
                meta.Destination = "RlTxReport3_1";

                meta.spInsert = "proc_RlTxReport3_1Insert";
                meta.spUpdate = "proc_RlTxReport3_1Update";
                meta.spDelete = "proc_RlTxReport3_1Delete";
                meta.spLoadAll = "proc_RlTxReport3_1LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_1LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport31Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
