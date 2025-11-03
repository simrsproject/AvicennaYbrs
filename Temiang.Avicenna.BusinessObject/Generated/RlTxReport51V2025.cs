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
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;



namespace Temiang.Avicenna.BusinessObject
{

    [Serializable]
    abstract public class esRlTxReport51V2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport51V2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport51V2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport51V2025Query query)
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
            this.InitQuery(query as esRlTxReport51V2025Query);
        }
        #endregion

        virtual public RlTxReport51V2025 DetachEntity(RlTxReport51V2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport51V2025;
        }

        virtual public RlTxReport51V2025 AttachEntity(RlTxReport51V2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport51V2025;
        }

        virtual public void Combine(RlTxReport51V2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport51V2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport51V2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport51V2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport51V2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport51V2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport51V2025()
        {

        }

        public esRlTxReport51V2025(DataRow row)
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
            esRlTxReport51V2025Query query = this.GetDynamicQuery();
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
                        case "L0001j": this.str.L0001j = (string)value; break;
                        case "P0001j": this.str.P0001j = (string)value; break;
                        case "L0001h": this.str.L0001h = (string)value; break;
                        case "P0001h": this.str.P0001h = (string)value; break;
                        case "L0007h": this.str.L0007h = (string)value; break;
                        case "P0007h": this.str.P0007h = (string)value; break;
                        case "L0828h": this.str.L0828h = (string)value; break;
                        case "P0828h": this.str.P0828h = (string)value; break;
                        case "L29h03b": this.str.L29h03b = (string)value; break;
                        case "P29h03b": this.str.P29h03b = (string)value; break;
                        case "L3b6b": this.str.L3b6b = (string)value; break;
                        case "P3b6b": this.str.P3b6b = (string)value; break;
                        case "L6b11b": this.str.L6b11b = (string)value; break;
                        case "P6b11b": this.str.P6b11b = (string)value; break;
                        case "L0104t": this.str.L0104t = (string)value; break;
                        case "P0104t": this.str.P0104t = (string)value; break;
                        case "L0509t": this.str.L0509t = (string)value; break;
                        case "P0509t": this.str.P0509t = (string)value; break;
                        case "L1014t": this.str.L1014t = (string)value; break;
                        case "P1014t": this.str.P1014t = (string)value; break;
                        case "L1519t": this.str.L1519t = (string)value; break;
                        case "P1519t": this.str.P1519t = (string)value; break;
                        case "L2024t": this.str.L2024t = (string)value; break;
                        case "P2024t": this.str.P2024t = (string)value; break;
                        case "L2529t": this.str.L2529t = (string)value; break;
                        case "P2529t": this.str.P2529t = (string)value; break;
                        case "L3034t": this.str.L3034t = (string)value; break;
                        case "P3034t": this.str.P3034t = (string)value; break;
                        case "L3539t": this.str.L3539t = (string)value; break;
                        case "P3539t": this.str.P3539t = (string)value; break;
                        case "L4044t": this.str.L4044t = (string)value; break;
                        case "P4044t": this.str.P4044t = (string)value; break;
                        case "L4549t": this.str.L4549t = (string)value; break;
                        case "P4549t": this.str.P4549t = (string)value; break;
                        case "L5054t": this.str.L5054t = (string)value; break;
                        case "P5054t": this.str.P5054t = (string)value; break;
                        case "L5559t": this.str.L5559t = (string)value; break;
                        case "P5559t": this.str.P5559t = (string)value; break;
                        case "L6064t": this.str.L6064t = (string)value; break;
                        case "P6064t": this.str.P6064t = (string)value; break;
                        case "L6569t": this.str.L6569t = (string)value; break;
                        case "P6569t": this.str.P6569t = (string)value; break;
                        case "L7074t": this.str.L7074t = (string)value; break;
                        case "P7074t": this.str.P7074t = (string)value; break;
                        case "L7579t": this.str.L7579t = (string)value; break;
                        case "P7579t": this.str.P7579t = (string)value; break;
                        case "L8084t": this.str.L8084t = (string)value; break;
                        case "P8084t": this.str.P8084t = (string)value; break;
                        case "L85t": this.str.L85t = (string)value; break;
                        case "P85t": this.str.P85t = (string)value; break;
                        case "KasusBaruL": this.str.KasusBaruL = (string)value; break;
                        case "KasusBaruP": this.str.KasusBaruP = (string)value; break;
                        case "TotalKasusBaru": this.str.TotalKasusBaru = (string)value; break;
                        case "TotalKunjungan": this.str.TotalKunjungan = (string)value; break;
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

                        case "L0001j":
                            if (value == null || value is System.Int32)
                                this.L0001j = (System.Int32?)value;
                            break;

                        case "P0001j":
                            if (value == null || value is System.Int32)
                                this.P0001j = (System.Int32?)value;
                            break;

                        case "L0001h":
                            if (value == null || value is System.Int32)
                                this.L0001h = (System.Int32?)value;
                            break;

                        case "P0001h":
                            if (value == null || value is System.Int32)
                                this.P0001h = (System.Int32?)value;
                            break;

                        case "L0007h":
                            if (value == null || value is System.Int32)
                                this.L0007h = (System.Int32?)value;
                            break;

                        case "P0007h":
                            if (value == null || value is System.Int32)
                                this.P0007h = (System.Int32?)value;
                            break;

                        case "L0828h":
                            if (value == null || value is System.Int32)
                                this.L0828h = (System.Int32?)value;
                            break;

                        case "P0828h":
                            if (value == null || value is System.Int32)
                                this.P0828h = (System.Int32?)value;
                            break;

                        case "L29h03b":
                            if (value == null || value is System.Int32)
                                this.L29h03b = (System.Int32?)value;
                            break;

                        case "P29h03b":
                            if (value == null || value is System.Int32)
                                this.P29h03b = (System.Int32?)value;
                            break;

                        case "L3b6b":
                            if (value == null || value is System.Int32)
                                this.L3b6b = (System.Int32?)value;
                            break;

                        case "P3b6b":
                            if (value == null || value is System.Int32)
                                this.P3b6b = (System.Int32?)value;
                            break;

                        case "L6b11b":
                            if (value == null || value is System.Int32)
                                this.L6b11b = (System.Int32?)value;
                            break;

                        case "P6b11b":
                            if (value == null || value is System.Int32)
                                this.P6b11b = (System.Int32?)value;
                            break;

                        case "L0104t":
                            if (value == null || value is System.Int32)
                                this.L0104t = (System.Int32?)value;
                            break;

                        case "P0104t":
                            if (value == null || value is System.Int32)
                                this.P0104t = (System.Int32?)value;
                            break;

                        case "L0509t":
                            if (value == null || value is System.Int32)
                                this.L0509t = (System.Int32?)value;
                            break;

                        case "P0509t":
                            if (value == null || value is System.Int32)
                                this.P0509t = (System.Int32?)value;
                            break;

                        case "L1014t":
                            if (value == null || value is System.Int32)
                                this.L1014t = (System.Int32?)value;
                            break;

                        case "P1014t":
                            if (value == null || value is System.Int32)
                                this.P1014t = (System.Int32?)value;
                            break;

                        case "L1519t":
                            if (value == null || value is System.Int32)
                                this.L1519t = (System.Int32?)value;
                            break;

                        case "P1519t":
                            if (value == null || value is System.Int32)
                                this.P1519t = (System.Int32?)value;
                            break;

                        case "L2024t":
                            if (value == null || value is System.Int32)
                                this.L2024t = (System.Int32?)value;
                            break;

                        case "P2024t":
                            if (value == null || value is System.Int32)
                                this.P2024t = (System.Int32?)value;
                            break;

                        case "L2529t":
                            if (value == null || value is System.Int32)
                                this.L2529t = (System.Int32?)value;
                            break;

                        case "P2529t":
                            if (value == null || value is System.Int32)
                                this.P2529t = (System.Int32?)value;
                            break;

                        case "L3034t":
                            if (value == null || value is System.Int32)
                                this.L3034t = (System.Int32?)value;
                            break;

                        case "P3034t":
                            if (value == null || value is System.Int32)
                                this.P3034t = (System.Int32?)value;
                            break;

                        case "L3539t":
                            if (value == null || value is System.Int32)
                                this.L3539t = (System.Int32?)value;
                            break;

                        case "P3539t":
                            if (value == null || value is System.Int32)
                                this.P3539t = (System.Int32?)value;
                            break;

                        case "L4044t":
                            if (value == null || value is System.Int32)
                                this.L4044t = (System.Int32?)value;
                            break;

                        case "P4044t":
                            if (value == null || value is System.Int32)
                                this.P4044t = (System.Int32?)value;
                            break;

                        case "L4549t":
                            if (value == null || value is System.Int32)
                                this.L4549t = (System.Int32?)value;
                            break;

                        case "P4549t":
                            if (value == null || value is System.Int32)
                                this.P4549t = (System.Int32?)value;
                            break;

                        case "L5054t":
                            if (value == null || value is System.Int32)
                                this.L5054t = (System.Int32?)value;
                            break;

                        case "P5054t":
                            if (value == null || value is System.Int32)
                                this.P5054t = (System.Int32?)value;
                            break;

                        case "L5559t":
                            if (value == null || value is System.Int32)
                                this.L5559t = (System.Int32?)value;
                            break;

                        case "P5559t":
                            if (value == null || value is System.Int32)
                                this.P5559t = (System.Int32?)value;
                            break;

                        case "L6064t":
                            if (value == null || value is System.Int32)
                                this.L6064t = (System.Int32?)value;
                            break;

                        case "P6064t":
                            if (value == null || value is System.Int32)
                                this.P6064t = (System.Int32?)value;
                            break;

                        case "L6569t":
                            if (value == null || value is System.Int32)
                                this.L6569t = (System.Int32?)value;
                            break;

                        case "P6569t":
                            if (value == null || value is System.Int32)
                                this.P6569t = (System.Int32?)value;
                            break;

                        case "L7074t":
                            if (value == null || value is System.Int32)
                                this.L7074t = (System.Int32?)value;
                            break;

                        case "P7074t":
                            if (value == null || value is System.Int32)
                                this.P7074t = (System.Int32?)value;
                            break;

                        case "L7579t":
                            if (value == null || value is System.Int32)
                                this.L7579t = (System.Int32?)value;
                            break;

                        case "P7579t":
                            if (value == null || value is System.Int32)
                                this.P7579t = (System.Int32?)value;
                            break;

                        case "L8084t":
                            if (value == null || value is System.Int32)
                                this.L8084t = (System.Int32?)value;
                            break;

                        case "P8084t":
                            if (value == null || value is System.Int32)
                                this.P8084t = (System.Int32?)value;
                            break;

                        case "L85t":
                            if (value == null || value is System.Int32)
                                this.L85t = (System.Int32?)value;
                            break;

                        case "P85t":
                            if (value == null || value is System.Int32)
                                this.P85t = (System.Int32?)value;
                            break;

                        case "KasusBaruL":

                            if (value == null || value is System.Int32)
                                this.KasusBaruL = (System.Int32?)value;
                            break;

                        case "KasusBaruP":

                            if (value == null || value is System.Int32)
                                this.KasusBaruP = (System.Int32?)value;
                            break;

                        case "TotalKasusBaru":

                            if (value == null || value is System.Int32)
                                this.TotalKasusBaru = (System.Int32?)value;
                            break;

                        case "TotalKunjungan":

                            if (value == null || value is System.Int32)
                                this.TotalKunjungan = (System.Int32?)value;
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
        /// Maps to RlTxReport51V2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport51V2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport51V2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L0006h
        /// </summary>
        virtual public System.Int32? L0001j
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0001j);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0001j, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P0006h
        /// </summary>
        virtual public System.Int32? P0001j
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0001j);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0001j, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L0628h
        /// </summary>
        virtual public System.Int32? L0001h
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0001h);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0001h, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P0628h
        /// </summary>
        virtual public System.Int32? P0001h
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0001h);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0001h, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L0007h
        /// </summary>
        virtual public System.Int32? L0007h
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0007h);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0007h, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P0007h
        /// </summary>
        virtual public System.Int32? P0007h
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0007h);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0007h, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L0828h
        /// </summary>
        virtual public System.Int32? L0828h
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0828h);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0828h, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P0828h
        /// </summary>
        virtual public System.Int32? P0828h
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0828h);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0828h, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L29h03b
        /// </summary>
        virtual public System.Int32? L29h03b
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L29h03b);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L29h03b, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P29h03b
        /// </summary>
        virtual public System.Int32? P29h03b
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P29h03b);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P29h03b, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L3b6b
        /// </summary>
        virtual public System.Int32? L3b6b
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L3b6b);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L3b6b, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P3b6b
        /// </summary>
        virtual public System.Int32? P3b6b
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P3b6b);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P3b6b, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L6b11b
        /// </summary>
        virtual public System.Int32? L6b11b
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L6b11b);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L6b11b, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P6b11b
        /// </summary>
        virtual public System.Int32? P6b11b
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P6b11b);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P6b11b, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L0104t
        /// </summary>
        virtual public System.Int32? L0104t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0104t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0104t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P0104t
        /// </summary>
        virtual public System.Int32? P0104t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0104t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0104t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L0509t
        /// </summary>
        virtual public System.Int32? L0509t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0509t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L0509t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P0509t
        /// </summary>
        virtual public System.Int32? P0509t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0509t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P0509t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L1014t
        /// </summary>
        virtual public System.Int32? L1014t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L1014t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L1014t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P1014t
        /// </summary>
        virtual public System.Int32? P1014t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P1014t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P1014t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L1519t
        /// </summary>
        virtual public System.Int32? L1519t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L1519t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L1519t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P1519t
        /// </summary>
        virtual public System.Int32? P1519t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P1519t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P1519t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L2024t
        /// </summary>
        virtual public System.Int32? L2024t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L2024t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L2024t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P2024t
        /// </summary>
        virtual public System.Int32? P2024t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P2024t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P2024t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L2529t
        /// </summary>
        virtual public System.Int32? L2529t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L2529t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L2529t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P2529t
        /// </summary>
        virtual public System.Int32? P2529t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P2529t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P2529t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L3034t
        /// </summary>
        virtual public System.Int32? L3034t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L3034t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L3034t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P3034t
        /// </summary>
        virtual public System.Int32? P3034t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P3034t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P3034t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L3539t
        /// </summary>
        virtual public System.Int32? L3539t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L3539t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L3539t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P3539t
        /// </summary>
        virtual public System.Int32? P3539t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P3539t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P3539t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L4044t
        /// </summary>
        virtual public System.Int32? L4044t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L4044t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L4044t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P4044t
        /// </summary>
        virtual public System.Int32? P4044t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P4044t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P4044t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L4549t
        /// </summary>
        virtual public System.Int32? L4549t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L4549t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L4549t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P4549t
        /// </summary>
        virtual public System.Int32? P4549t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P4549t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P4549t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L5054t
        /// </summary>
        virtual public System.Int32? L5054t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L5054t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L5054t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P5054t
        /// </summary>
        virtual public System.Int32? P5054t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P5054t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P5054t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L5559t
        /// </summary>
        virtual public System.Int32? L5559t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L5559t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L5559t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P5559t
        /// </summary>
        virtual public System.Int32? P5559t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P5559t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P5559t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L6064t
        /// </summary>
        virtual public System.Int32? L6064t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L6064t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L6064t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P6064t
        /// </summary>
        virtual public System.Int32? P6064t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P6064t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P6064t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L6569t
        /// </summary>
        virtual public System.Int32? L6569t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L6569t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L6569t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P6569t
        /// </summary>
        virtual public System.Int32? P6569t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P6569t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P6569t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L7074t
        /// </summary>
        virtual public System.Int32? L7074t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L7074t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L7074t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P7074t
        /// </summary>
        virtual public System.Int32? P7074t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P7074t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P7074t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L7579t
        /// </summary>
        virtual public System.Int32? L7579t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L7579t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L7579t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P7579t
        /// </summary>
        virtual public System.Int32? P7579t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P7579t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P7579t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L8084t
        /// </summary>
        virtual public System.Int32? L8084t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L8084t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L8084t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P8084t
        /// </summary>
        virtual public System.Int32? P8084t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P8084t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P8084t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.L85t
        /// </summary>
        virtual public System.Int32? L85t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L85t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.L85t, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.P85t
        /// </summary>
        virtual public System.Int32? P85t
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P85t);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.P85t, value);
            }
        }


        /// <summary>
        /// Maps to RlTxReport51V2025.KasusBaruL
        /// </summary>
        virtual public System.Int32? KasusBaruL
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.KasusBaruL);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.KasusBaruL, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.KasusBaruP
        /// </summary>
        virtual public System.Int32? KasusBaruP
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.KasusBaruP);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.KasusBaruP, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.TotalKasusBaru
        /// </summary>
        virtual public System.Int32? TotalKasusBaru
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.TotalKasusBaru);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.TotalKasusBaru, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.TotalKunjungan
        /// </summary>
        virtual public System.Int32? TotalKunjungan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.TotalKunjungan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport51V2025Metadata.ColumnNames.TotalKunjungan, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport51V2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport51V2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport51V2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport51V2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport51V2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport51V2025 entity)
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

            public System.String L0001j
            {
                get
                {
                    System.Int32? data = entity.L0001j;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L0001j = null;
                    else entity.L0001j = Convert.ToInt32(value);
                }
            }

            public System.String P0001j
            {
                get
                {
                    System.Int32? data = entity.P0001j;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P0001j = null;
                    else entity.P0001j = Convert.ToInt32(value);
                }
            }

            public System.String L0001h
            {
                get
                {
                    System.Int32? data = entity.L0001h;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L0001h = null;
                    else entity.L0001h = Convert.ToInt32(value);
                }
            }

            public System.String P0001h
            {
                get
                {
                    System.Int32? data = entity.P0001h;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P0001h = null;
                    else entity.P0001h = Convert.ToInt32(value);
                }
            }

            public System.String L0007h
            {
                get
                {
                    System.Int32? data = entity.L0007h;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L0007h = null;
                    else entity.L0007h = Convert.ToInt32(value);
                }
            }

            public System.String P0007h
            {
                get
                {
                    System.Int32? data = entity.P0007h;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P0007h = null;
                    else entity.P0007h = Convert.ToInt32(value);
                }
            }

            public System.String L0828h
            {
                get
                {
                    System.Int32? data = entity.L0828h;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L0828h = null;
                    else entity.L0828h = Convert.ToInt32(value);
                }
            }

            public System.String P0828h
            {
                get
                {
                    System.Int32? data = entity.P0828h;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P0828h = null;
                    else entity.P0828h = Convert.ToInt32(value);
                }
            }

            public System.String L29h03b
            {
                get
                {
                    System.Int32? data = entity.L29h03b;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L29h03b = null;
                    else entity.L29h03b = Convert.ToInt32(value);
                }
            }

            public System.String P29h03b
            {
                get
                {
                    System.Int32? data = entity.P29h03b;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P29h03b = null;
                    else entity.P29h03b = Convert.ToInt32(value);
                }
            }

            public System.String L3b6b
            {
                get
                {
                    System.Int32? data = entity.L3b6b;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L3b6b = null;
                    else entity.L3b6b = Convert.ToInt32(value);
                }
            }

            public System.String P3b6b
            {
                get
                {
                    System.Int32? data = entity.P3b6b;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P3b6b = null;
                    else entity.P3b6b = Convert.ToInt32(value);
                }
            }

            public System.String L6b11b
            {
                get
                {
                    System.Int32? data = entity.L6b11b;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L6b11b = null;
                    else entity.L6b11b = Convert.ToInt32(value);
                }
            }

            public System.String P6b11b
            {
                get
                {
                    System.Int32? data = entity.P6b11b;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P6b11b = null;
                    else entity.P6b11b = Convert.ToInt32(value);
                }
            }

            public System.String L0104t
            {
                get
                {
                    System.Int32? data = entity.L0104t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L0104t = null;
                    else entity.L0104t = Convert.ToInt32(value);
                }
            }

            public System.String P0104t
            {
                get
                {
                    System.Int32? data = entity.P0104t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P0104t = null;
                    else entity.P0104t = Convert.ToInt32(value);
                }
            }

            public System.String L0509t
            {
                get
                {
                    System.Int32? data = entity.L0509t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L0509t = null;
                    else entity.L0509t = Convert.ToInt32(value);
                }
            }

            public System.String P0509t
            {
                get
                {
                    System.Int32? data = entity.P0509t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P0509t = null;
                    else entity.P0509t = Convert.ToInt32(value);
                }
            }

            public System.String L1014t
            {
                get
                {
                    System.Int32? data = entity.L1014t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L1014t = null;
                    else entity.L1014t = Convert.ToInt32(value);
                }
            }

            public System.String P1014t
            {
                get
                {
                    System.Int32? data = entity.P1014t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P1014t = null;
                    else entity.P1014t = Convert.ToInt32(value);
                }
            }

            public System.String L1519t
            {
                get
                {
                    System.Int32? data = entity.L1519t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L1519t = null;
                    else entity.L1519t = Convert.ToInt32(value);
                }
            }

            public System.String P1519t
            {
                get
                {
                    System.Int32? data = entity.P1519t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P1519t = null;
                    else entity.P1519t = Convert.ToInt32(value);
                }
            }

            public System.String L2024t
            {
                get
                {
                    System.Int32? data = entity.L2024t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L2024t = null;
                    else entity.L2024t = Convert.ToInt32(value);
                }
            }

            public System.String P2024t
            {
                get
                {
                    System.Int32? data = entity.P2024t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P2024t = null;
                    else entity.P2024t = Convert.ToInt32(value);
                }
            }

            public System.String L2529t
            {
                get
                {
                    System.Int32? data = entity.L2529t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L2529t = null;
                    else entity.L2529t = Convert.ToInt32(value);
                }
            }

            public System.String P2529t
            {
                get
                {
                    System.Int32? data = entity.P2529t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P2529t = null;
                    else entity.P2529t = Convert.ToInt32(value);
                }
            }

            public System.String L3034t
            {
                get
                {
                    System.Int32? data = entity.L3034t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L3034t = null;
                    else entity.L3034t = Convert.ToInt32(value);
                }
            }

            public System.String P3034t
            {
                get
                {
                    System.Int32? data = entity.P3034t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P3034t = null;
                    else entity.P3034t = Convert.ToInt32(value);
                }
            }

            public System.String L3539t
            {
                get
                {
                    System.Int32? data = entity.L3539t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L3539t = null;
                    else entity.L3539t = Convert.ToInt32(value);
                }
            }

            public System.String P3539t
            {
                get
                {
                    System.Int32? data = entity.P3539t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P3539t = null;
                    else entity.P3539t = Convert.ToInt32(value);
                }
            }

            public System.String L4044t
            {
                get
                {
                    System.Int32? data = entity.L4044t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L4044t = null;
                    else entity.L4044t = Convert.ToInt32(value);
                }
            }

            public System.String P4044t
            {
                get
                {
                    System.Int32? data = entity.P4044t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P4044t = null;
                    else entity.P4044t = Convert.ToInt32(value);
                }
            }

            public System.String L4549t
            {
                get
                {
                    System.Int32? data = entity.L4549t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L4549t = null;
                    else entity.L4549t = Convert.ToInt32(value);
                }
            }

            public System.String P4549t
            {
                get
                {
                    System.Int32? data = entity.P4549t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P4549t = null;
                    else entity.P4549t = Convert.ToInt32(value);
                }
            }

            public System.String L5054t
            {
                get
                {
                    System.Int32? data = entity.L5054t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L5054t = null;
                    else entity.L5054t = Convert.ToInt32(value);
                }
            }

            public System.String P5054t
            {
                get
                {
                    System.Int32? data = entity.P5054t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P5054t = null;
                    else entity.P5054t = Convert.ToInt32(value);
                }
            }

            public System.String L5559t
            {
                get
                {
                    System.Int32? data = entity.L5559t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L5559t = null;
                    else entity.L5559t = Convert.ToInt32(value);
                }
            }

            public System.String P5559t
            {
                get
                {
                    System.Int32? data = entity.P5559t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P5559t = null;
                    else entity.P5559t = Convert.ToInt32(value);
                }
            }

            public System.String L6064t
            {
                get
                {
                    System.Int32? data = entity.L6064t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L6064t = null;
                    else entity.L6064t = Convert.ToInt32(value);
                }
            }

            public System.String P6064t
            {
                get
                {
                    System.Int32? data = entity.P6064t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P6064t = null;
                    else entity.P6064t = Convert.ToInt32(value);
                }
            }

            public System.String L6569t
            {
                get
                {
                    System.Int32? data = entity.L6569t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L6569t = null;
                    else entity.L6569t = Convert.ToInt32(value);
                }
            }

            public System.String P6569t
            {
                get
                {
                    System.Int32? data = entity.P6569t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P6569t = null;
                    else entity.P6569t = Convert.ToInt32(value);
                }
            }

            public System.String L7074t
            {
                get
                {
                    System.Int32? data = entity.L7074t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L7074t = null;
                    else entity.L7074t = Convert.ToInt32(value);
                }
            }

            public System.String P7074t
            {
                get
                {
                    System.Int32? data = entity.P7074t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P7074t = null;
                    else entity.P7074t = Convert.ToInt32(value);
                }
            }

            public System.String L7579t
            {
                get
                {
                    System.Int32? data = entity.L7579t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L7579t = null;
                    else entity.L7579t = Convert.ToInt32(value);
                }
            }

            public System.String P7579t
            {
                get
                {
                    System.Int32? data = entity.P7579t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P7579t = null;
                    else entity.P7579t = Convert.ToInt32(value);
                }
            }

            public System.String L8084t
            {
                get
                {
                    System.Int32? data = entity.L8084t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L8084t = null;
                    else entity.L8084t = Convert.ToInt32(value);
                }
            }

            public System.String P8084t
            {
                get
                {
                    System.Int32? data = entity.P8084t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P8084t = null;
                    else entity.P8084t = Convert.ToInt32(value);
                }
            }

            public System.String L85t
            {
                get
                {
                    System.Int32? data = entity.L85t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.L85t = null;
                    else entity.L85t = Convert.ToInt32(value);
                }
            }

            public System.String P85t
            {
                get
                {
                    System.Int32? data = entity.P85t;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.P85t = null;
                    else entity.P85t = Convert.ToInt32(value);
                }
            }

            public System.String KasusBaruL
            {
                get
                {
                    System.Int32? data = entity.KasusBaruL;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KasusBaruL = null;
                    else entity.KasusBaruL = Convert.ToInt32(value);
                }
            }

            public System.String KasusBaruP
            {
                get
                {
                    System.Int32? data = entity.KasusBaruP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KasusBaruP = null;
                    else entity.KasusBaruP = Convert.ToInt32(value);
                }
            }

            public System.String TotalKasusBaru
            {
                get
                {
                    System.Int32? data = entity.TotalKasusBaru;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TotalKasusBaru = null;
                    else entity.TotalKasusBaru = Convert.ToInt32(value);
                }
            }

            public System.String TotalKunjungan
            {
                get
                {
                    System.Int32? data = entity.TotalKunjungan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TotalKunjungan = null;
                    else entity.TotalKunjungan = Convert.ToInt32(value);
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


            private esRlTxReport51V2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport51V2025Query query)
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
                throw new Exception("esRlTxReport51V2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport51V2025 : esRlTxReport51V2025
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
    abstract public class esRlTxReport51V2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport51V2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem L0001j
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L0001j, esSystemType.Int32);
            }
        }

        public esQueryItem P0001j
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P0001j, esSystemType.Int32);
            }
        }

        public esQueryItem L0001h
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L0001h, esSystemType.Int32);
            }
        }

        public esQueryItem P0001h
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P0001h, esSystemType.Int32);
            }
        }

        public esQueryItem L0007h
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L0007h, esSystemType.Int32);
            }
        }

        public esQueryItem P0007h
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P0007h, esSystemType.Int32);
            }
        }

        public esQueryItem L0828h
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L0828h, esSystemType.Int32);
            }
        }

        public esQueryItem P0828h
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P0828h, esSystemType.Int32);
            }
        }

        public esQueryItem L29h03b
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L29h03b, esSystemType.Int32);
            }
        }

        public esQueryItem P29h03b
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P29h03b, esSystemType.Int32);
            }
        }

        public esQueryItem L3b6b
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L3b6b, esSystemType.Int32);
            }
        }

        public esQueryItem P3b6b
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P3b6b, esSystemType.Int32);
            }
        }

        public esQueryItem L6b11b
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L6b11b, esSystemType.Int32);
            }
        }

        public esQueryItem P6b11b
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P6b11b, esSystemType.Int32);
            }
        }

        public esQueryItem L0104t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L0104t, esSystemType.Int32);
            }
        }

        public esQueryItem P0104t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P0104t, esSystemType.Int32);
            }
        }

        public esQueryItem L0509t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L0509t, esSystemType.Int32);
            }
        }

        public esQueryItem P0509t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P0509t, esSystemType.Int32);
            }
        }

        public esQueryItem L1014t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L1014t, esSystemType.Int32);
            }
        }

        public esQueryItem P1014t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P1014t, esSystemType.Int32);
            }
        }

        public esQueryItem L1519t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L1519t, esSystemType.Int32);
            }
        }

        public esQueryItem P1519t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P1519t, esSystemType.Int32);
            }
        }

        public esQueryItem L2024t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L2024t, esSystemType.Int32);
            }
        }

        public esQueryItem P2024t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P2024t, esSystemType.Int32);
            }
        }

        public esQueryItem L2529t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L2529t, esSystemType.Int32);
            }
        }

        public esQueryItem P2529t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P2529t, esSystemType.Int32);
            }
        }

        public esQueryItem L3034t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L3034t, esSystemType.Int32);
            }
        }

        public esQueryItem P3034t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P3034t, esSystemType.Int32);
            }
        }

        public esQueryItem L3539t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L3539t, esSystemType.Int32);
            }
        }

        public esQueryItem P3539t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P3539t, esSystemType.Int32);
            }
        }

        public esQueryItem L4044t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L4044t, esSystemType.Int32);
            }
        }

        public esQueryItem P4044t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P4044t, esSystemType.Int32);
            }
        }

        public esQueryItem L4549t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L4549t, esSystemType.Int32);
            }
        }

        public esQueryItem P4549t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P4549t, esSystemType.Int32);
            }
        }

        public esQueryItem L5054t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L5054t, esSystemType.Int32);
            }
        }

        public esQueryItem P5054t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P5054t, esSystemType.Int32);
            }
        }

        public esQueryItem L5559t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L5559t, esSystemType.Int32);
            }
        }

        public esQueryItem P5559t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P5559t, esSystemType.Int32);
            }
        }

        public esQueryItem L6064t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L6064t, esSystemType.Int32);
            }
        }

        public esQueryItem P6064t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P6064t, esSystemType.Int32);
            }
        }

        public esQueryItem L6569t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L6569t, esSystemType.Int32);
            }
        }

        public esQueryItem P6569t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P6569t, esSystemType.Int32);
            }
        }

        public esQueryItem L7074t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L7074t, esSystemType.Int32);
            }
        }

        public esQueryItem P7074t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P7074t, esSystemType.Int32);
            }
        }

        public esQueryItem L7579t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L7579t, esSystemType.Int32);
            }
        }

        public esQueryItem P7579t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P7579t, esSystemType.Int32);
            }
        }

        public esQueryItem L8084t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L8084t, esSystemType.Int32);
            }
        }

        public esQueryItem P8084t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P8084t, esSystemType.Int32);
            }
        }

        public esQueryItem L85t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.L85t, esSystemType.Int32);
            }
        }

        public esQueryItem P85t
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.P85t, esSystemType.Int32);
            }
        }


        public esQueryItem KasusBaruL
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.KasusBaruL, esSystemType.Int32);
            }
        }

        public esQueryItem KasusBaruP
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.KasusBaruP, esSystemType.Int32);
            }
        }

        public esQueryItem TotalKasusBaru
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.TotalKasusBaru, esSystemType.Int32);
            }
        }

        public esQueryItem TotalKunjungan
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.TotalKunjungan, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport51V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport51V2025Collection")]
    public partial class RlTxReport51V2025Collection : esRlTxReport51V2025Collection, IEnumerable<RlTxReport51V2025>
    {
        public RlTxReport51V2025Collection()
        {

        }

        public static implicit operator List<RlTxReport51V2025>(RlTxReport51V2025Collection coll)
        {
            List<RlTxReport51V2025> list = new List<RlTxReport51V2025>();

            foreach (RlTxReport51V2025 emp in coll)
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
                return RlTxReport51V2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport51V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport51V2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport51V2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport51V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport51V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport51V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport51V2025 AddNew()
        {
            RlTxReport51V2025 entity = base.AddNewEntity() as RlTxReport51V2025;

            return entity;
        }

        public RlTxReport51V2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport51V2025;
        }


        #region IEnumerable<RlTxReport51V2025> Members

        IEnumerator<RlTxReport51V2025> IEnumerable<RlTxReport51V2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport51V2025;
            }
        }

        #endregion

        private RlTxReport51V2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport51V2025' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport51V2025 ({RlTxReportNo},{RlMasterReportItemID})")]
    [Serializable]
    public partial class RlTxReport51V2025 : esRlTxReport51V2025
    {
        public RlTxReport51V2025()
        {

        }

        public RlTxReport51V2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport51V2025Metadata.Meta();
            }
        }



        override protected esRlTxReport51V2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport51V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport51V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport51V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport51V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport51V2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport51V2025Query : esRlTxReport51V2025Query
    {
        public RlTxReport51V2025Query()
        {

        }

        public RlTxReport51V2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport51V2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport51V2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport51V2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L0001j, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L0001j;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P0001j, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P0001j;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L0001h, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L0001h;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P0001h, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P0001h;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L0007h, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L0007h;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P0007h, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P0007h;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L0828h, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L0828h;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P0828h, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P0828h;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L29h03b, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L29h03b;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P29h03b, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P29h03b;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L3b6b, 12, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L3b6b;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P3b6b, 13, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P3b6b;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L6b11b, 14, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L6b11b;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P6b11b, 15, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P6b11b;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L0104t, 16, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L0104t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P0104t, 17, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P0104t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L0509t, 18, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L0509t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P0509t, 19, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P0509t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L1014t, 20, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L1014t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P1014t, 21, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P1014t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L1519t, 22, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L1519t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P1519t, 23, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P1519t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L2024t, 24, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L2024t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P2024t, 25, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P2024t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L2529t, 26, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L2529t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P2529t, 27, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P2529t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L3034t, 28, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L3034t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P3034t, 29, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P3034t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L3539t, 30, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L3539t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P3539t, 31, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P3539t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L4044t, 32, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L4044t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P4044t, 33, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P4044t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L4549t, 34, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L4549t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P4549t, 35, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P4549t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L5054t, 36, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L5054t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P5054t, 37, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P5054t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L5559t, 38, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L5559t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P5559t, 39, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P5559t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L6064t, 40, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L6064t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P6064t, 41, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P6064t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L6569t, 42, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L6569t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P6569t, 43, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P6569t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L7074t, 44, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L7074t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P7074t, 45, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P7074t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L7579t, 46, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L7579t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P7579t, 47, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P7579t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L8084t, 48, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L8084t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P8084t, 49, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P8084t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.L85t, 50, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.L85t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.P85t, 51, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.P85t;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);


            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.KasusBaruL, 52, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.KasusBaruL;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.KasusBaruP, 53, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.KasusBaruP;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.TotalKasusBaru, 54, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.TotalKasusBaru;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.TotalKunjungan, 55, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.TotalKunjungan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.LastUpdateDateTime, 56, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport51V2025Metadata.ColumnNames.LastUpdateByUserID, 57, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport51V2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport51V2025Metadata Meta()
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
            public const string L0001j = "L0001j";
            public const string P0001j = "P0001j";
            public const string L0001h = "L0001h";
            public const string P0001h = "P0001h";
            public const string L0007h = "L0007h";
            public const string P0007h = "P0007h";
            public const string L0828h = "L0828h";
            public const string P0828h = "P0828h";
            public const string L29h03b = "L29h03b";
            public const string P29h03b = "P29h03b";
            public const string L3b6b = "L3b6b";
            public const string P3b6b = "P3b6b";
            public const string L6b11b = "L6b11b";
            public const string P6b11b = "P6b11b";
            public const string L0104t = "L0104t";
            public const string P0104t = "P0104t";
            public const string L0509t = "L0509t";
            public const string P0509t = "P0509t";
            public const string L1014t = "L1014t";
            public const string P1014t = "P1014t";
            public const string L1519t = "L1519t";
            public const string P1519t = "P1519t";
            public const string L2024t = "L2024t";
            public const string P2024t = "P2024t";
            public const string L2529t = "L2529t";
            public const string P2529t = "P2529t";
            public const string L3034t = "L3034t";
            public const string P3034t = "P3034t";
            public const string L3539t = "L3539t";
            public const string P3539t = "P3539t";
            public const string L4044t = "L4044t";
            public const string P4044t = "P4044t";
            public const string L4549t = "L4549t";
            public const string P4549t = "P4549t";
            public const string L5054t = "L5054t";
            public const string P5054t = "P5054t";
            public const string L5559t = "L5559t";
            public const string P5559t = "P5559t";
            public const string L6064t = "L6064t";
            public const string P6064t = "P6064t";
            public const string L6569t = "L6569t";
            public const string P6569t = "P6569t";
            public const string L7074t = "L7074t";
            public const string P7074t = "P7074t";
            public const string L7579t = "L7579t";
            public const string P7579t = "P7579t";
            public const string L8084t = "L8084t";
            public const string P8084t = "P8084t";
            public const string L85t = "L85t";
            public const string P85t = "P85t";
            public const string KasusBaruL = "KasusBaruL";
            public const string KasusBaruP = "KasusBaruP";
            public const string TotalKasusBaru = "TotalKasusBaru";
            public const string TotalKunjungan = "TotalKunjungan";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string L0001j = "L0001j";
            public const string P0001j = "P0001j";
            public const string L0001h = "L0001h";
            public const string P0001h = "P0001h";
            public const string L0007h = "L0007h";
            public const string P0007h = "P0007h";
            public const string L0828h = "L0828h";
            public const string P0828h = "P0828h";
            public const string L29h03b = "L29h03b";
            public const string P29h03b = "P29h03b";
            public const string L3b6b = "L3b6b";
            public const string P3b6b = "P3b6b";
            public const string L6b11b = "L6b11b";
            public const string P6b11b = "P6b11b";
            public const string L0104t = "L0104t";
            public const string P0104t = "P0104t";
            public const string L0509t = "L0509t";
            public const string P0509t = "P0509t";
            public const string L1014t = "L1014t";
            public const string P1014t = "P1014t";
            public const string L1519t = "L1519t";
            public const string P1519t = "P1519t";
            public const string L2024t = "L2024t";
            public const string P2024t = "P2024t";
            public const string L2529t = "L2529t";
            public const string P2529t = "P2529t";
            public const string L3034t = "L3034t";
            public const string P3034t = "P3034t";
            public const string L3539t = "L3539t";
            public const string P3539t = "P3539t";
            public const string L4044t = "L4044t";
            public const string P4044t = "P4044t";
            public const string L4549t = "L4549t";
            public const string P4549t = "P4549t";
            public const string L5054t = "L5054t";
            public const string P5054t = "P5054t";
            public const string L5559t = "L5559t";
            public const string P5559t = "P5559t";
            public const string L6064t = "L6064t";
            public const string P6064t = "P6064t";
            public const string L6569t = "L6569t";
            public const string P6569t = "P6569t";
            public const string L7074t = "L7074t";
            public const string P7074t = "P7074t";
            public const string L7579t = "L7579t";
            public const string P7579t = "P7579t";
            public const string L8084t = "L8084t";
            public const string P8084t = "P8084t";
            public const string L85t = "L85t";
            public const string P85t = "P85t";
            public const string KasusBaruL = "KasusBaruL";
            public const string KasusBaruP = "KasusBaruP";
            public const string TotalKasusBaru = "TotalKasusBaru";
            public const string TotalKunjungan = "TotalKunjungan";
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
            lock (typeof(RlTxReport51V2025Metadata))
            {
                if (RlTxReport51V2025Metadata.mapDelegates == null)
                {
                    RlTxReport51V2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport51V2025Metadata.meta == null)
                {
                    RlTxReport51V2025Metadata.meta = new RlTxReport51V2025Metadata();
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
                meta.AddTypeMap("L0001j", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P0001j", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L0001h", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P0001h", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L0007h", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P0007h", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L0828h", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P0828h", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L29h03b", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P29h03b", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L3b6b", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P3b6b", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L6b11b", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P6b11b", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L0104t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P0104t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L0509t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P0509t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L1014t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P1014t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L1519t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P1519t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L2024t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P2024t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L2529t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P2529t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L3034t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P3034t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L3539t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P3539t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L4044t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P4044t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L4549t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P4549t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L5054t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P5054t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L5559t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P5559t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L6064t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P6064t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L6569t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P6569t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L7074t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P7074t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L7579t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P7579t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L8084t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P8084t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("L85t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("P85t", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KasusBaruL", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KasusBaruP", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TotalKasusBaru", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TotalKunjungan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "RlTxReport51V2025";
                meta.Destination = "RlTxReport51V2025";

                meta.spInsert = "proc_RlTxReport51V2025Insert";
                meta.spUpdate = "proc_RlTxReport51V2025Update";
                meta.spDelete = "proc_RlTxReport51V2025Delete";
                meta.spLoadAll = "proc_RlTxReport51V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport51V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport51V2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
