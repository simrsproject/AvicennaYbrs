/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/5/2013 2:02:43 PM
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
    abstract public class esRlTxReport37v2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport37v2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport37v2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport37v2025Query query)
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
            this.InitQuery(query as esRlTxReport37v2025Query);
        }
        #endregion

        virtual public RlTxReport37v2025 DetachEntity(RlTxReport37v2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport37v2025;
        }

        virtual public RlTxReport37v2025 AttachEntity(RlTxReport37v2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport37v2025;
        }

        virtual public void Combine(RlTxReport37v2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport37v2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport37v2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport37v2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport37v2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport37v2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport37v2025()
        {

        }

        public esRlTxReport37v2025(DataRow row)
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
            esRlTxReport37v2025Query query = this.GetDynamicQuery();
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
                        case "RmRumahSakit": this.str.RmRumahSakit = (string)value; break;
                        case "RmBidan": this.str.RmBidan = (string)value; break;
                        case "RmPuskesmas": this.str.RmPuskesmas = (string)value; break;
                        case "RmFasKesLain": this.str.RmFasKesLain = (string)value; break;
                        case "RmHidup": this.str.RmHidup = (string)value; break;
                        case "RmMati": this.str.RmMati = (string)value; break;
                        case "RmTotal": this.str.RmTotal = (string)value; break;
                        case "RnmHidup": this.str.RnmHidup = (string)value; break;
                        case "RnmMati": this.str.RnmMati = (string)value; break;
                        case "RnmTotal": this.str.RnmTotal = (string)value; break;
                        case "NrHidup": this.str.NrHidup = (string)value; break;
                        case "NrMati": this.str.NrMati = (string)value; break;
                        case "NrTotal": this.str.NrTotal = (string)value; break;
                        case "DiRujuk": this.str.DiRujuk = (string)value; break;
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

                        case "RmRumahSakit":

                            if (value == null || value is System.Int32)
                                this.RmRumahSakit = (System.Int32?)value;
                            break;

                        case "RmBidan":

                            if (value == null || value is System.Int32)
                                this.RmBidan = (System.Int32?)value;
                            break;

                        case "RmPuskesmas":

                            if (value == null || value is System.Int32)
                                this.RmPuskesmas = (System.Int32?)value;
                            break;

                        case "RmFasKesLain":

                            if (value == null || value is System.Int32)
                                this.RmFasKesLain = (System.Int32?)value;
                            break;

                        case "RmHidup":

                            if (value == null || value is System.Int32)
                                this.RmHidup = (System.Int32?)value;
                            break;

                        case "RmMati":

                            if (value == null || value is System.Int32)
                                this.RmMati = (System.Int32?)value;
                            break;

                        case "RmTotal":

                            if (value == null || value is System.Int32)
                                this.RmTotal = (System.Int32?)value;
                            break;

                        case "RnmHidup":

                            if (value == null || value is System.Int32)
                                this.RnmHidup = (System.Int32?)value;
                            break;

                        case "RnmMati":

                            if (value == null || value is System.Int32)
                                this.RnmMati = (System.Int32?)value;
                            break;

                        case "RnmTotal":

                            if (value == null || value is System.Int32)
                                this.RnmTotal = (System.Int32?)value;
                            break;

                        case "NrHidup":

                            if (value == null || value is System.Int32)
                                this.NrHidup = (System.Int32?)value;
                            break;

                        case "NrMati":

                            if (value == null || value is System.Int32)
                                this.NrMati = (System.Int32?)value;
                            break;

                        case "NrTotal":

                            if (value == null || value is System.Int32)
                                this.NrTotal = (System.Int32?)value;
                            break;

                        case "DiRujuk":

                            if (value == null || value is System.Int32)
                                this.DiRujuk = (System.Int32?)value;
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
        /// Maps to RlTxReport3_7v2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport37v2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport37v2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RmRumahSakit
        /// </summary>
        virtual public System.Int32? RmRumahSakit
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmRumahSakit);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmRumahSakit, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RmBidan
        /// </summary>
        virtual public System.Int32? RmBidan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmBidan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmBidan, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RmPuskesmas
        /// </summary>
        virtual public System.Int32? RmPuskesmas
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmPuskesmas);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmPuskesmas, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RmFasKesLain
        /// </summary>
        virtual public System.Int32? RmFasKesLain
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmFasKesLain);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmFasKesLain, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RmHidup
        /// </summary>
        virtual public System.Int32? RmHidup
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmHidup);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmHidup, value);
            }
        }


        /// <summary>
        /// Maps to RlTxReport3_7v2025.RmMati
        /// </summary>
        virtual public System.Int32? RmMati
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmMati);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmMati, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RmTotal
        /// </summary>
        virtual public System.Int32? RmTotal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmTotal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RmTotal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RnmHidup
        /// </summary>
        virtual public System.Int32? RnmHidup
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RnmHidup);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RnmHidup, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RnmMati
        /// </summary>
        virtual public System.Int32? RnmMati
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RnmMati);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RnmMati, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.RnmTotal
        /// </summary>
        virtual public System.Int32? RnmTotal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RnmTotal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.RnmTotal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.NrHidup
        /// </summary>
        virtual public System.Int32? NrHidup
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.NrHidup);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.NrHidup, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.NrMati
        /// </summary>
        virtual public System.Int32? NrMati
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.NrMati);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.NrMati, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.NrTotal
        /// </summary>
        virtual public System.Int32? NrTotal
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.NrTotal);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.NrTotal, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.DiRujuk
        /// </summary>
        virtual public System.Int32? DiRujuk
        {
            get
            {
                return base.GetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.DiRujuk);
            }

            set
            {
                base.SetSystemInt32(RlTxReport37v2025Metadata.ColumnNames.DiRujuk, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport37v2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport37v2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_7v2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport37v2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport37v2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport37v2025 entity)
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

            public System.String RmRumahSakit
            {
                get
                {
                    System.Int32? data = entity.RmRumahSakit;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RmRumahSakit = null;
                    else entity.RmRumahSakit = Convert.ToInt32(value);
                }
            }

            public System.String RmBidan
            {
                get
                {
                    System.Int32? data = entity.RmBidan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RmBidan = null;
                    else entity.RmBidan = Convert.ToInt32(value);
                }
            }

            public System.String RmPuskesmas
            {
                get
                {
                    System.Int32? data = entity.RmPuskesmas;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RmPuskesmas = null;
                    else entity.RmPuskesmas = Convert.ToInt32(value);
                }
            }

            public System.String RmFasKesLain
            {
                get
                {
                    System.Int32? data = entity.RmFasKesLain;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RmFasKesLain = null;
                    else entity.RmFasKesLain = Convert.ToInt32(value);
                }
            }

            public System.String RmHidup
            {
                get
                {
                    System.Int32? data = entity.RmHidup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RmHidup = null;
                    else entity.RmHidup = Convert.ToInt32(value);
                }
            }

            public System.String RmMati
            {
                get
                {
                    System.Int32? data = entity.RmMati;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RmMati = null;
                    else entity.RmMati = Convert.ToInt32(value);
                }
            }

            public System.String RmTotal
            {
                get
                {
                    System.Int32? data = entity.RmTotal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RmTotal = null;
                    else entity.RmTotal = Convert.ToInt32(value);
                }
            }

            public System.String RnmHidup
            {
                get
                {
                    System.Int32? data = entity.RnmHidup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RnmHidup = null;
                    else entity.RnmHidup = Convert.ToInt32(value);
                }
            }

            public System.String RnmMati
            {
                get
                {
                    System.Int32? data = entity.RnmMati;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RnmMati = null;
                    else entity.RnmMati = Convert.ToInt32(value);
                }
            }

            public System.String RnmTotal
            {
                get
                {
                    System.Int32? data = entity.RnmTotal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RnmTotal = null;
                    else entity.RnmTotal = Convert.ToInt32(value);
                }
            }

            public System.String NrHidup
            {
                get
                {
                    System.Int32? data = entity.NrHidup;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NrHidup = null;
                    else entity.NrHidup = Convert.ToInt32(value);
                }
            }

            public System.String NrMati
            {
                get
                {
                    System.Int32? data = entity.NrMati;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NrMati = null;
                    else entity.NrMati = Convert.ToInt32(value);
                }
            }

            public System.String NrTotal
            {
                get
                {
                    System.Int32? data = entity.NrTotal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NrTotal = null;
                    else entity.NrTotal = Convert.ToInt32(value);
                }
            }

            public System.String DiRujuk
            {
                get
                {
                    System.Int32? data = entity.DiRujuk;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiRujuk = null;
                    else entity.DiRujuk = Convert.ToInt32(value);
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


            private esRlTxReport37v2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport37v2025Query query)
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
                throw new Exception("esRlTxReport37v2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport37v2025 : esRlTxReport37v2025
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
    abstract public class esRlTxReport37v2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport37v2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem RmRumahSakit
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RmRumahSakit, esSystemType.Int32);
            }
        }

        public esQueryItem RmBidan
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RmBidan, esSystemType.Int32);
            }
        }

        public esQueryItem RmPuskesmas
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RmPuskesmas, esSystemType.Int32);
            }
        }

        public esQueryItem RmFasKesLain
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RmFasKesLain, esSystemType.Int32);
            }
        }

        public esQueryItem RmHidup
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RmHidup, esSystemType.Int32);
            }
        }

        public esQueryItem RmMati
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RmMati, esSystemType.Int32);
            }
        }

        public esQueryItem RmTotal
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RmTotal, esSystemType.Int32);
            }
        }

        public esQueryItem RnmHidup
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RnmHidup, esSystemType.Int32);
            }
        }

        public esQueryItem RnmMati
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RnmMati, esSystemType.Int32);
            }
        }

        public esQueryItem RnmTotal
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.RnmTotal, esSystemType.Int32);
            }
        }

        public esQueryItem NrHidup
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.NrHidup, esSystemType.Int32);
            }
        }

        public esQueryItem NrTotal
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.NrTotal, esSystemType.Int32);
            }
        }

        public esQueryItem DiRujuk
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.DiRujuk, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport37v2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport37v2025Collection")]
    public partial class RlTxReport37v2025Collection : esRlTxReport37v2025Collection, IEnumerable<RlTxReport37v2025>
    {
        public RlTxReport37v2025Collection()
        {

        }

        public static implicit operator List<RlTxReport37v2025>(RlTxReport37v2025Collection coll)
        {
            List<RlTxReport37v2025> list = new List<RlTxReport37v2025>();

            foreach (RlTxReport37v2025 emp in coll)
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
                return RlTxReport37v2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport37v2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport37v2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport37v2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport37v2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport37v2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport37v2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport37v2025 AddNew()
        {
            RlTxReport37v2025 entity = base.AddNewEntity() as RlTxReport37v2025;

            return entity;
        }

        public RlTxReport37v2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport37v2025;
        }


        #region IEnumerable<RlTxReport37v2025> Members

        IEnumerator<RlTxReport37v2025> IEnumerable<RlTxReport37v2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport37v2025;
            }
        }

        #endregion

        private RlTxReport37v2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_7v2025' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport37v2025 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport37v2025 : esRlTxReport37v2025
    {
        public RlTxReport37v2025()
        {

        }

        public RlTxReport37v2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport37v2025Metadata.Meta();
            }
        }



        override protected esRlTxReport37v2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport37v2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport37v2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport37v2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport37v2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport37v2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport37v2025Query : esRlTxReport37v2025Query
    {
        public RlTxReport37v2025Query()
        {

        }

        public RlTxReport37v2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport37v2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport37v2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport37v2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RmRumahSakit, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RmRumahSakit;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RmBidan, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RmBidan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RmPuskesmas, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RmPuskesmas;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RmFasKesLain, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RmFasKesLain;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RmHidup, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RmHidup;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RmMati, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RmMati;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RmTotal, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RmTotal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RnmHidup, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RnmHidup;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RnmMati, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RnmMati;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.RnmTotal, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.RnmTotal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.NrHidup, 12, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.NrHidup;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.NrMati, 13, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.NrMati;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.NrTotal, 14, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.NrTotal;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.DiRujuk, 15, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.DiRujuk;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.LastUpdateDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport37v2025Metadata.ColumnNames.LastUpdateByUserID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport37v2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport37v2025Metadata Meta()
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
            public const string RmRumahSakit = "RmRumahSakit";
            public const string RmBidan = "RmBidan";
            public const string RmPuskesmas = "RmPuskesmas";
            public const string RmFasKesLain = "RmFasKesLain";
            public const string RmHidup = "RmHidup";
            public const string RmMati = "RmMati";
            public const string RmTotal = "RmTotal";
            public const string RnmHidup = "RnmHidup";
            public const string RnmMati = "RnmMati";
            public const string RnmTotal = "RnmTotal";
            public const string NrHidup = "NrHidup";
            public const string NrMati = "NrMati";
            public const string NrTotal = "NrTotal";
            public const string DiRujuk = "DiRujuk";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string RmRumahSakit = "RmRumahSakit";
            public const string RmBidan = "RmBidan";
            public const string RmPuskesmas = "RmPuskesmas";
            public const string RmFasKesLain = "RmFasKesLain";
            public const string RmHidup = "RmHidup";
            public const string RmMati = "RmMati";
            public const string RmTotal = "RmTotal";
            public const string RnmHidup = "RnmHidup";
            public const string RnmMati = "RnmMati";
            public const string RnmTotal = "RnmTotal";
            public const string NrHidup = "NrHidup";
            public const string NrMati = "NrMati";
            public const string NrTotal = "NrTotal";
            public const string DiRujuk = "DiRujuk";
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
            lock (typeof(RlTxReport37v2025Metadata))
            {
                if (RlTxReport37v2025Metadata.mapDelegates == null)
                {
                    RlTxReport37v2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport37v2025Metadata.meta == null)
                {
                    RlTxReport37v2025Metadata.meta = new RlTxReport37v2025Metadata();
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
                meta.AddTypeMap("RmRumahSakit", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RmBidan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RmPuskesmas", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RmFasKesLain", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RmHidup", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RmMati", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RmTotal", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RnmHidup", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RnmMati", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RnmTotal", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("NrHidup", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("NrMati", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("NrTotal", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("DiRujuk", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlTxReport3_7v2025";
                meta.Destination = "RlTxReport3_7v2025";

                meta.spInsert = "proc_RlTxReport3_7v2025Insert";
                meta.spUpdate = "proc_RlTxReport3_7v2025Update";
                meta.spDelete = "proc_RlTxReport3_7v2025Delete";
                meta.spLoadAll = "proc_RlTxReport3_7v2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_7v2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport37v2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
