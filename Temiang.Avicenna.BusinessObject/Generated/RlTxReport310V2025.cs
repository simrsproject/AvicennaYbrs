/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:15 PM
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
    abstract public class esRlTxReport310V2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport310V2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport310V2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport310V2025Query query)
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
            this.InitQuery(query as esRlTxReport310V2025Query);
        }
        #endregion

        virtual public RlTxReport310V2025 DetachEntity(RlTxReport310V2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport310V2025;
        }

        virtual public RlTxReport310V2025 AttachEntity(RlTxReport310V2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport310V2025;
        }

        virtual public void Combine(RlTxReport310V2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport310V2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport310V2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport310V2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport310V2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport310V2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport310V2025()
        {

        }

        public esRlTxReport310V2025(DataRow row)
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
            esRlTxReport310V2025Query query = this.GetDynamicQuery();
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
                        case "RujukanPuskesmas": this.str.RujukanPuskesmas = (string)value; break;
                        case "RujukanRsLain": this.str.RujukanRsLain = (string)value; break;
                        case "RujukanFasKesLain": this.str.RujukanFasKesLain = (string)value; break;
                        //case "TotalRujukanMasuk": this.str.TotalRujukanMasuk = (string)value; break;
                        case "DirujukKePuskesmasAsal": this.str.DirujukKePuskesmasAsal = (string)value; break;
                        case "DirujukKeRsAsal": this.str.DirujukKeRsAsal = (string)value; break;
                        case "DirujukKeFasKesAsal": this.str.DirujukKeFasKesAsal = (string)value; break;
                        //case "TotalRujukanDikembalikan": this.str.TotalRujukanDikembalikan = (string)value; break;
                        case "DirujukPasienRujukan": this.str.DirujukPasienRujukan = (string)value; break;
                        case "DirujukPasienDtgSendiri": this.str.DirujukPasienDtgSendiri = (string)value; break;
                        //case "TotalDirujukKeluar": this.str.TotalDirujukKeluar = (string)value; break;
                        case "DirujukDiterimaKembali": this.str.DirujukDiterimaKembali = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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

                        case "RujukanPuskesmas":

                            if (value == null || value is System.Int32)
                                this.RujukanPuskesmas = (System.Int32?)value;
                            break;

                        case "RujukanRsLain":

                            if (value == null || value is System.Int32)
                                this.RujukanRsLain = (System.Int32?)value;

                            break;

                        case "RujukanFasKesLain":

                            if (value == null || value is System.Int32)
                                this.RujukanFasKesLain = (System.Int32?)value;
                            break;

                        //case "TotalRujukanMasuk":

                        //    if (value == null || value is System.Int32)
                        //        this.TotalRujukanMasuk = (System.Int32?)value;
                        //    break;

                        case "DirujukKePuskesmasAsal":

                            if (value == null || value is System.Int32)
                                this.DirujukKePuskesmasAsal = (System.Int32?)value;
                            break;

                        case "DirujukKeRsAsal":

                            if (value == null || value is System.Int32)
                                this.DirujukKeRsAsal = (System.Int32?)value;
                            break;

                        case "DirujukKeFasKesAsal":

                            if (value == null || value is System.Int32)
                                this.DirujukKeFasKesAsal = (System.Int32?)value;
                            break;

                        //case "TotalRujukanDikembalikan":

                        //    if (value == null || value is System.Int32)
                        //        this.TotalRujukanDikembalikan = (System.Int32?)value;
                        //    break;

                        case "DirujukPasienRujukan":

                            if (value == null || value is System.Int32)
                                this.DirujukPasienRujukan = (System.Int32?)value;
                            break;

                        case "DirujukPasienDtgSendiri":

                            if (value == null || value is System.Int32)
                                this.DirujukPasienDtgSendiri = (System.Int32?)value;
                            break;

                        //case "TotalDirujukKeluar":

                        //    if (value == null || value is System.Int32)
                        //        this.TotalDirujukKeluar = (System.Int32?)value;
                        //    break;

                        case "DirujukDiterimaKembali":

                            if (value == null || value is System.Int32)
                                this.DirujukDiterimaKembali = (System.Int32?)value;
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
        /// Maps to RlTxReport3_10V2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport310V2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport310V2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.RujukanPuskesmas
        /// </summary>
        virtual public System.Int32? RujukanPuskesmas
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.RujukanPuskesmas);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.RujukanPuskesmas, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.RujukanRSLain
        /// </summary>
        virtual public System.Int32? RujukanRsLain
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.RujukanRsLain);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.RujukanRsLain, value);
            }
        }


        /// <summary>
        /// Maps to RlTxReport3_10V2025.RujukanFasKesLain
        /// </summary>
        virtual public System.Int32? RujukanFasKesLain
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.RujukanFasKesLain);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.RujukanFasKesLain, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.TotalRujukanMasuk
        /// </summary>
        //virtual public System.Int32? TotalRujukanMasuk
        //{
        //    get
        //    {
        //        return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.TotalRujukanMasuk);
        //    }

        //    set
        //    {
        //        base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.TotalRujukanMasuk, value);
        //    }
        //}

        /// <summary>
        /// Maps to RlTxReport3_10V2025.TotalRujukanDikembalikan
        /// </summary>
        //virtual public System.Int32? TotalRujukanDikembalikan
        //{
        //    get
        //    {
        //        return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.TotalRujukanDiKembalikan);
        //    }

        //    set
        //    {
        //        base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.TotalRujukanDiKembalikan, value);
        //    }
        //}

        /// <summary>
        /// Maps to RlTxReport3_10V2025.DirujukKePuskesmasAsal
        /// </summary>
        virtual public System.Int32? DirujukKePuskesmasAsal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukKePuskesmasAsal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukKePuskesmasAsal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.DirujukKeRsAsal
        /// </summary>
        virtual public System.Int32? DirujukKeRsAsal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukKeRsAsal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukKeRsAsal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.DirujukKeFasKesAsal
        /// </summary>
        virtual public System.Int32? DirujukKeFasKesAsal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukKeFasKesAsal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukKeFasKesAsal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.TotalDirujukKeluar
        /// </summary>
        //virtual public System.Int32? TotalDirujukKeluar
        //{
        //    get
        //    {
        //        return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.TotalDirujukKeluar);
        //    }

        //    set
        //    {
        //        base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.TotalDirujukKeluar, value);
        //    }
        //}

        /// <summary>
        /// Maps to RlTxReport3_10V2025.DirujukPasienRujukan
        /// </summary>
        virtual public System.Int32? DirujukPasienRujukan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukPasienRujukan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukPasienRujukan, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.DirujukPasienDtgSendiri
        /// </summary>
        virtual public System.Int32? DirujukPasienDtgSendiri
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukPasienDtgSendiri);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukPasienDtgSendiri, value);
            }
        }


        /// <summary>
        /// Maps to RlTxReport3_10V2025.DirujukPasienDtgSendiri
        /// </summary>
        virtual public System.Int32? DirujukDiterimaKembali
        {
            get
            {
                return base.GetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukDiterimaKembali);
            }

            set
            {
                base.SetSystemInt32(RlTxReport310V2025Metadata.ColumnNames.DirujukDiterimaKembali, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport310V2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport310V2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_10V2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport310V2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport310V2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport310V2025 entity)
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

            public System.String RujukanPuskesmas
            {
                get
                {
                    System.Int32? data = entity.RujukanPuskesmas;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RujukanPuskesmas = null;
                    else entity.RujukanPuskesmas = Convert.ToInt32(value);
                }
            }

            public System.String RujukanRsLain
            {
                get
                {
                    System.Int32? data = entity.RujukanRsLain;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RujukanRsLain = null;
                    else entity.RujukanRsLain = Convert.ToInt32(value);
                }
            }

            public System.String RujukanFasKesLain
            {
                get
                {
                    System.Int32? data = entity.RujukanFasKesLain;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RujukanFasKesLain = null;
                    else entity.RujukanFasKesLain = Convert.ToInt32(value);
                }
            }

            //public System.String TotalRujukanMasuk
            //{
            //    get
            //    {
            //        System.Int32? data = entity.TotalRujukanMasuk;
            //        return (data == null) ? String.Empty : Convert.ToString(data);
            //    }

            //    set
            //    {
            //        if (value == null || value.Length == 0) entity.TotalRujukanMasuk = null;
            //        else entity.TotalRujukanMasuk = Convert.ToInt32(value);
            //    }
            //}

            public System.String DirujukKePuskesmasAsal
            {
                get
                {
                    System.Int32? data = entity.DirujukKePuskesmasAsal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DirujukKePuskesmasAsal = null;
                    else entity.DirujukKePuskesmasAsal = Convert.ToInt32(value);
                }
            }

            public System.String DirujukKeRsAsal
            {
                get
                {
                    System.Int32? data = entity.DirujukKeRsAsal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DirujukKeRsAsal = null;
                    else entity.DirujukKeRsAsal = Convert.ToInt32(value);
                }
            }

            public System.String DirujukKeFasKesAsal
            {
                get
                {
                    System.Int32? data = entity.DirujukKeFasKesAsal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DirujukKeFasKesAsal = null;
                    else entity.DirujukKeFasKesAsal = Convert.ToInt32(value);
                }
            }

            //public System.String TotalRujukanDikembalikan
            //{
            //    get
            //    {
            //        System.Int32? data = entity.TotalRujukanDikembalikan;
            //        return (data == null) ? String.Empty : Convert.ToString(data);
            //    }

            //    set
            //    {
            //        if (value == null || value.Length == 0) entity.TotalRujukanDikembalikan = null;
            //        else entity.TotalRujukanDikembalikan = Convert.ToInt32(value);
            //    }
            //}

            public System.String DirujukPasienRujukan
            {
                get
                {
                    System.Int32? data = entity.DirujukPasienRujukan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DirujukPasienRujukan = null;
                    else entity.DirujukPasienRujukan = Convert.ToInt32(value);
                }
            }

            public System.String DirujukPasienDtgSendiri
            {
                get
                {
                    System.Int32? data = entity.DirujukPasienDtgSendiri;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DirujukPasienDtgSendiri = null;
                    else entity.DirujukPasienDtgSendiri = Convert.ToInt32(value);
                }
            }

            //public System.String TotalDirujukKeluar
            //{
            //    get
            //    {
            //        System.Int32? data = entity.TotalDirujukKeluar;
            //        return (data == null) ? String.Empty : Convert.ToString(data);
            //    }

            //    set
            //    {
            //        if (value == null || value.Length == 0) entity.TotalDirujukKeluar = null;
            //        else entity.TotalDirujukKeluar = Convert.ToInt32(value);            
            //    }
            //}

            public System.String DirujukDiterimaKembali
            {
                get
                {
                    System.Int32? data = entity.DirujukDiterimaKembali;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DirujukDiterimaKembali = null;
                    else entity.DirujukDiterimaKembali = Convert.ToInt32(value);
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


            private esRlTxReport310V2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport310V2025Query query)
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
                throw new Exception("esRlTxReport310V2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport310V2025 : esRlTxReport310V2025
    {
        //public int TotalRujukanDikembalikan;


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
    abstract public class esRlTxReport310V2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport310V2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem RujukanPuskesmas
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.RujukanPuskesmas, esSystemType.Int32);
            }
        }

        public esQueryItem RujukanRsLain
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.RujukanRsLain, esSystemType.Int32);
            }
        }

        public esQueryItem RujukanFasKesLain
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.RujukanFasKesLain, esSystemType.Int32);
            }
        }

        //public esQueryItem TotalRujukanMasuk
        //{
        //    get
        //    {
        //        return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.TotalRujukanMasuk, esSystemType.Int32);
        //    }
        //}

        public esQueryItem DirujukKePuskesmasAsal
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.DirujukKePuskesmasAsal, esSystemType.Int32);
            }
        }

        public esQueryItem DirujukKeRsAsal
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.DirujukKeRsAsal, esSystemType.Int32);
            }
        }

        public esQueryItem DirujukKeFasKesAsal
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.DirujukKeFasKesAsal, esSystemType.Int32);
            }
        }

        //public esQueryItem TotalRujukanDiKembalikan
        //{
        //    get
        //    {
        //        return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.TotalRujukanDiKembalikan, esSystemType.Int32);
        //    }
        //}

        public esQueryItem DirujukPasienRujukan
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.DirujukPasienRujukan, esSystemType.Int32);
            }
        }

        public esQueryItem DirujukPasienDtgSendiri
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.DirujukPasienDtgSendiri, esSystemType.Int32);
            }
        }

        //public esQueryItem TotalDirujukKeluar
        //{
        //    get
        //    {
        //        return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.TotalDirujukKeluar, esSystemType.Int32);
        //    }
        //}

        public esQueryItem DirujukDiterimaKembali
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.DirujukDiterimaKembali, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport310V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport310V2025Collection")]
    public partial class RlTxReport310V2025Collection : esRlTxReport310V2025Collection, IEnumerable<RlTxReport310V2025>
    {
        public RlTxReport310V2025Collection()
        {

        }

        public static implicit operator List<RlTxReport310V2025>(RlTxReport310V2025Collection coll)
        {
            List<RlTxReport310V2025> list = new List<RlTxReport310V2025>();

            foreach (RlTxReport310V2025 emp in coll)
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
                return RlTxReport310V2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport310V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport310V2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport310V2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport310V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport310V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport310V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport310V2025 AddNew()
        {
            RlTxReport310V2025 entity = base.AddNewEntity() as RlTxReport310V2025;

            return entity;
        }

        public RlTxReport310V2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport310V2025;
        }


        #region IEnumerable<RlTxReport310V2025> Members

        IEnumerator<RlTxReport310V2025> IEnumerable<RlTxReport310V2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport310V2025;
            }
        }

        #endregion

        private RlTxReport310V2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_14' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport310V2025 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport310V2025 : esRlTxReport310V2025
    {
        public RlTxReport310V2025()
        {

        }

        public RlTxReport310V2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport310V2025Metadata.Meta();
            }
        }



        override protected esRlTxReport310V2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport310V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport310V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport310V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport310V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport310V2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport310V2025Query : esRlTxReport310V2025Query
    {
        public RlTxReport310V2025Query()
        {

        }

        public RlTxReport310V2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport310V2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport310V2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport310V2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.RujukanPuskesmas, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.RujukanPuskesmas;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.RujukanRsLain, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.RujukanRsLain;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.RujukanFasKesLain, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.RujukanFasKesLain;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            //c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.TotalRujukanMasuk, 5, typeof(System.Int32), esSystemType.Int32);
            //c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.TotalRujukanMasuk;
            //c.NumericPrecision = 10;
            //c.IsNullable = true;
            //_columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.DirujukKePuskesmasAsal, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.DirujukKePuskesmasAsal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.DirujukKeRsAsal, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.DirujukKeRsAsal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.DirujukKeFasKesAsal, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.DirujukKeFasKesAsal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            //c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.TotalRujukanDiKembalikan, 9, typeof(System.Int32), esSystemType.Int32);
            //c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.TotalRujukanDiKembalikan;
            //c.NumericPrecision = 10;
            //c.IsNullable = true;
            //_columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.DirujukPasienRujukan, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.DirujukPasienRujukan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.DirujukPasienDtgSendiri, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.DirujukPasienDtgSendiri;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            //c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.TotalDirujukKeluar, 12, typeof(System.Int32), esSystemType.Int32);
            //c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.TotalDirujukKeluar;
            //c.NumericPrecision = 10;
            //c.IsNullable = true;
            //_columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.DirujukDiterimaKembali, 13, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.DirujukDiterimaKembali;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport310V2025Metadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport310V2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport310V2025Metadata Meta()
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
            public const string RujukanPuskesmas = "RujukanPuskesmas";
            public const string RujukanRsLain = "RujukanRsLain";
            public const string RujukanFasKesLain = "RujukanFasKesLain";
            //public const string TotalRujukanMasuk = "TotalRujukanMasuk";
            public const string DirujukKePuskesmasAsal = "DirujukKePuskesmasAsal";
            public const string DirujukKeRsAsal = "DirujukKeRsAsal";
            public const string DirujukKeFasKesAsal = "DirujukKeFasKesAsal";
            //public const string TotalRujukanDiKembalikan = "TotalRujukanDiKembalikan";
            public const string DirujukPasienRujukan = "DirujukPasienRujukan";
            public const string DirujukPasienDtgSendiri = "DirujukPasienDtgSendiri";
            //public const string TotalDirujukKeluar = "TotalDirujukKeluar";
            public const string DirujukDiterimaKembali = "DirujukDiterimaKembali";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string RujukanPuskesmas = "RujukanPuskesmas";
            public const string RujukanRsLain = "RujukanRsLain";
            public const string RujukanFasKesLain = "RujukanFasKesLain";
            //public const string TotalRujukanMasuk = "TotalRujukanMasuk";
            public const string DirujukKePuskesmasAsal = "DirujukKePuskesmasAsal";
            public const string DirujukKeRsAsal = "DirujukKeRsAsal";
            public const string DirujukKeFasKesAsal = "DirujukKeFasKesAsal";
            //public const string TotalRujukanDiKembalikan = "TotalRujukanDiKembalikan";
            public const string DirujukPasienRujukan = "DirujukPasienRujukan";
            public const string DirujukPasienDtgSendiri = "DirujukPasienDtgSendiri";
            //public const string TotalDirujukKeluar = "TotalDirujukKeluar";
            public const string DirujukDiterimaKembali = "DirujukDiterimaKembali";
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
            lock (typeof(RlTxReport310V2025Metadata))
            {
                if (RlTxReport310V2025Metadata.mapDelegates == null)
                {
                    RlTxReport310V2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport310V2025Metadata.meta == null)
                {
                    RlTxReport310V2025Metadata.meta = new RlTxReport310V2025Metadata();
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
                meta.AddTypeMap("RujukanPuskesmas", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RujukanRsLain", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RujukanFasKesLain", new esTypeMap("int", "System.Int32"));
                //meta.AddTypeMap("TotalRujukanMasuk", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DirujukKePuskesmasAsal", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DirujukKeRsAsal", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DirujukKeFasKesAsal", new esTypeMap("int", "System.Int32"));
                //meta.AddTypeMap("TotalRujukanDiKembalikan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DirujukPasienRujukan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DirujukPasienDtgSendiri", new esTypeMap("int", "System.Int32"));
                //meta.AddTypeMap("TotalDirujukKeluar", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DirujukDiterimaKembali", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlTxReport3_10V2025";
                meta.Destination = "RlTxReport3_10V2025";

                meta.spInsert = "proc_RlTxReport3_10V2025Insert";
                meta.spUpdate = "proc_RlTxReport3_10V2025Update";
                meta.spDelete = "proc_RlTxReport3_10V2025Delete";
                meta.spLoadAll = "proc_RlTxReport3_10V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_10V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport310V2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
