/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/5/2013 9:19:54 AM
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
    abstract public class esRlTxReport31V2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport31V2025Collection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlTxReport31V2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport31V2025Query query)
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
            this.InitQuery(query as esRlTxReport31V2025Query);
        }
        #endregion

        virtual public RlTxReport31V2025 DetachEntity(RlTxReport31V2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport31V2025;
        }

        virtual public RlTxReport31V2025 AttachEntity(RlTxReport31V2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport31V2025;
        }

        virtual public void Combine(RlTxReport31V2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport31V2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport31V2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport31V2025);
        }
    }



    [Serializable]
    abstract public class esRlTxReport31V2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport31V2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport31V2025()
        {

        }

        public esRlTxReport31V2025(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String rlTxReportNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlTxReportNo);
            else
                return LoadByPrimaryKeyStoredProcedure(rlTxReportNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo)
        {
            esRlTxReport31V2025Query query = this.GetDynamicQuery();
            query.Where(query.RlTxReportNo == rlTxReportNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RlTxReportNo", rlTxReportNo);
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
                        case "BorNonIntensif": this.str.BorNonIntensif = (string)value; break;
                        case "BorICU": this.str.BorICU = (string)value; break;
                        case "BorNICU": this.str.BorNICU = (string)value; break;
                        case "BorPICU": this.str.BorPICU = (string)value; break;
                        case "BorIntensifLainnya": this.str.BorIntensifLainnya = (string)value; break;
                        case "LosNonIntensif": this.str.LosNonIntensif = (string)value; break;
                        case "LosICU": this.str.LosICU = (string)value; break;
                        case "LosNICU": this.str.LosNICU = (string)value; break;
                        case "LosPICU": this.str.LosPICU = (string)value; break;
                        case "LosIntensifLainnya": this.str.LosIntensifLainnya = (string)value; break;
                        case "BtoNonIntensif": this.str.BtoNonIntensif = (string)value; break;
                        case "BtoICU": this.str.BtoICU = (string)value; break;
                        case "BtoNICU": this.str.BtoNICU = (string)value; break;
                        case "BtoPICU": this.str.BtoPICU = (string)value; break;
                        case "BtoIntensifLainnya": this.str.BtoIntensifLainnya = (string)value; break;
                        case "ToiNonIntensif": this.str.ToiNonIntensif = (string)value; break;
                        case "ToiICU": this.str.ToiICU = (string)value; break;
                        case "ToiNICU": this.str.ToiNICU = (string)value; break;
                        case "ToiPICU": this.str.ToiPICU = (string)value; break;
                        case "ToiIntensifLainnya": this.str.ToiIntensifLainnya = (string)value; break;
                        case "NdrNonIntensif": this.str.NdrNonIntensif = (string)value; break;
                        case "NdrICU": this.str.NdrICU = (string)value; break;
                        case "NdrNICU": this.str.NdrNICU = (string)value; break;
                        case "NdrPICU": this.str.NdrPICU = (string)value; break;
                        case "NdrIntensifLainnya": this.str.NdrIntensifLainnya = (string)value; break;
                        case "GdrNonIntensif": this.str.GdrNonIntensif = (string)value; break;
                        case "GdrICU": this.str.GdrICU = (string)value; break;
                        case "GdrNICU": this.str.GdrNICU = (string)value; break;
                        case "GdrPICU": this.str.GdrPICU = (string)value; break;
                        case "GdrIntensifLainnya": this.str.GdrIntensifLainnya = (string)value; break;
                        case "RataKunjungan": this.str.RataKunjungan = (string)value; break;
                        case "RataRata": this.str.RataRata = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "BorNonIntensif":

                            if (value == null || value is System.Decimal)
                                this.BorNonIntensif = (System.Decimal?)value;
                            break;

                        case "BorICU":

                            if (value == null || value is System.Decimal)
                                this.BorICU = (System.Decimal?)value;
                            break;

                        case "BorNICU":

                            if (value == null || value is System.Decimal)
                                this.BorNICU = (System.Decimal?)value;
                            break;

                        case "BorPICU":

                            if (value == null || value is System.Decimal)
                                this.BorPICU = (System.Decimal?)value;
                            break;

                        case "BorIntensifLainnya":

                            if (value == null || value is System.Decimal)
                                this.BorIntensifLainnya = (System.Decimal?)value;
                            break;

                        case "LosNonIntensif":
                            if (value == null || value is System.Decimal)
                                this.LosNonIntensif = (System.Decimal?)value;
                            break;

                        case "LosICU":
                            if (value == null || value is System.Decimal)
                                this.LosICU = (System.Decimal?)value;
                            break;

                        case "LosNICU":
                            if (value == null || value is System.Decimal)
                                this.LosNICU = (System.Decimal?)value;
                            break;

                        case "LosPICU":
                            if (value == null || value is System.Decimal)
                                this.LosPICU = (System.Decimal?)value;
                            break;

                        case "LosIntensifLainnya":
                            if (value == null || value is System.Decimal)
                                this.LosIntensifLainnya = (System.Decimal?)value;
                            break;

                        case "BtoNonIntensif":
                            if (value == null || value is System.Decimal)
                                this.BtoNonIntensif = (System.Decimal?)value;
                            break;

                        case "BtoICU":
                            if (value == null || value is System.Decimal)
                                this.BtoICU = (System.Decimal?)value;
                            break;

                        case "BtoNICU":
                            if (value == null || value is System.Decimal)
                                this.BtoNICU = (System.Decimal?)value;
                            break;

                        case "BtoPICU":
                            if (value == null || value is System.Decimal)
                                this.BtoPICU = (System.Decimal?)value;
                            break;

                        case "BtoIntensifLainnya":
                            if (value == null || value is System.Decimal)
                                this.BtoIntensifLainnya = (System.Decimal?)value;
                            break;

                        case "ToiNonIntensif":
                            if (value == null || value is System.Decimal)
                                this.ToiNonIntensif = (System.Decimal?)value;
                            break;

                        case "ToiICU":
                            if (value == null || value is System.Decimal)
                                this.ToiICU = (System.Decimal?)value;
                            break;

                        case "ToiNICU":
                            if (value == null || value is System.Decimal)
                                this.ToiNICU = (System.Decimal?)value;
                            break;

                        case "ToiPICU":
                            if (value == null || value is System.Decimal)
                                this.ToiPICU = (System.Decimal?)value;
                            break;

                        case "ToiIntensifLainnya":
                            if (value == null || value is System.Decimal)
                                this.ToiIntensifLainnya = (System.Decimal?)value;
                            break;

                        case "NdrNonIntensif":
                            if (value == null || value is System.Decimal)
                                this.NdrNonIntensif = (System.Decimal?)value;
                            break;

                        case "NdrICU":
                            if (value == null || value is System.Decimal)
                                this.NdrICU = (System.Decimal?)value;
                            break;

                        case "NdrNICU":
                            if (value == null || value is System.Decimal)
                                this.NdrNICU = (System.Decimal?)value;
                            break;

                        case "NdrPICU":
                            if (value == null || value is System.Decimal)
                                this.NdrPICU = (System.Decimal?)value;
                            break;

                        case "NdrIntensifLainnya":
                            if (value == null || value is System.Decimal)
                                this.NdrIntensifLainnya = (System.Decimal?)value;
                            break;

                        case "GdrNonIntensif":
                            if (value == null || value is System.Decimal)
                                this.GdrNonIntensif = (System.Decimal?)value;
                            break;

                        case "GdrICU":
                            if (value == null || value is System.Decimal)
                                this.GdrICU = (System.Decimal?)value;
                            break;

                        case "GdrNICU":
                            if (value == null || value is System.Decimal)
                                this.GdrNICU = (System.Decimal?)value;
                            break;

                        case "GdrPICU":
                            if (value == null || value is System.Decimal)
                                this.GdrPICU = (System.Decimal?)value;
                            break;

                        case "GdrIntensifLainnya":
                            if (value == null || value is System.Decimal)
                                this.GdrIntensifLainnya = (System.Decimal?)value;
                            break;

                        case "RataKunjungan":

                            if (value == null || value is System.Decimal)
                                this.RataKunjungan = (System.Decimal?)value;
                            break;

                        case "RataRata":

                            if (value == null || value is System.Decimal)
                                this.RataRata = (System.Decimal?)value;
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
        /// Maps to RlTxReport3_1V2025.RlTxReportNo
        /// </summary>
        virtual public System.String RlTxReportNo
        {
            get
            {
                return base.GetSystemString(RlTxReport31V2025Metadata.ColumnNames.RlTxReportNo);
            }

            set
            {
                base.SetSystemString(RlTxReport31V2025Metadata.ColumnNames.RlTxReportNo, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BorNonIntensif
        /// </summary>
        virtual public System.Decimal? BorNonIntensif
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorNonIntensif);
            }
            set
            {
                base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorNonIntensif, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BorICU
        /// </summary>
        virtual public System.Decimal? BorICU
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorICU);
            }
            set
            {
                base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BorNICU
        /// </summary>
        virtual public System.Decimal? BorNICU
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorNICU);
            }
            set
            {
                base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorNICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BorPICU
        /// </summary>
        virtual public System.Decimal? BorPICU
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorPICU);
            }
            set
            {
                base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorPICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BorIntensifLainnya
        /// </summary>
        virtual public System.Decimal? BorIntensifLainnya
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorIntensifLainnya);
            }
            set
            {
                base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BorIntensifLainnya, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.LosNonIntensif
        /// </summary>
        virtual public System.Decimal? LosNonIntensif
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosNonIntensif); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosNonIntensif, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.LosICU
        /// </summary>
        virtual public System.Decimal? LosICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.LosNICU
        /// </summary>
        virtual public System.Decimal? LosNICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosNICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosNICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.LosPICU
        /// </summary>
        virtual public System.Decimal? LosPICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosPICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosPICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.LosIntensifLainnya
        /// </summary>
        virtual public System.Decimal? LosIntensifLainnya
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosIntensifLainnya); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.LosIntensifLainnya, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BtoNonIntensif
        /// </summary>
        virtual public System.Decimal? BtoNonIntensif
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoNonIntensif); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoNonIntensif, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BtoICU
        /// </summary>
        virtual public System.Decimal? BtoICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BtoNICU
        /// </summary>
        virtual public System.Decimal? BtoNICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoNICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoNICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BtoPICU
        /// </summary>
        virtual public System.Decimal? BtoPICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoPICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoPICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.BtoIntensifLainnya
        /// </summary>
        virtual public System.Decimal? BtoIntensifLainnya
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoIntensifLainnya); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.BtoIntensifLainnya, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.ToiNonIntensif
        /// </summary>
        virtual public System.Decimal? ToiNonIntensif
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiNonIntensif); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiNonIntensif, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.ToiICU
        /// </summary>
        virtual public System.Decimal? ToiICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.ToiNICU
        /// </summary>
        virtual public System.Decimal? ToiNICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiNICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiNICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.ToiPICU
        /// </summary>
        virtual public System.Decimal? ToiPICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiPICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiPICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.ToiIntensifLainnya
        /// </summary>
        virtual public System.Decimal? ToiIntensifLainnya
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiIntensifLainnya); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.ToiIntensifLainnya, value); }
        }


        /// <summary>
        /// Maps to RlTxReport3_1V2025.NdrNonIntensif
        /// </summary>
        virtual public System.Decimal? NdrNonIntensif
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrNonIntensif); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrNonIntensif, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.NdrICU
        /// </summary>
        virtual public System.Decimal? NdrICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.NdrNICU
        /// </summary>
        virtual public System.Decimal? NdrNICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrNICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrNICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.NdrPICU
        /// </summary>
        virtual public System.Decimal? NdrPICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrPICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrPICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.NdrIntensifLainnya
        /// </summary>
        virtual public System.Decimal? NdrIntensifLainnya
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrIntensifLainnya); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.NdrIntensifLainnya, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.GdrNonIntensif
        /// </summary>
        virtual public System.Decimal? GdrNonIntensif
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrNonIntensif); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrNonIntensif, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.GdrICU
        /// </summary>
        virtual public System.Decimal? GdrICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.GdrNICU
        /// </summary>
        virtual public System.Decimal? GdrNICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrNICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrNICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.GdrPICU
        /// </summary>
        virtual public System.Decimal? GdrPICU
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrPICU); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrPICU, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.GdrIntensifLainnya
        /// </summary>
        virtual public System.Decimal? GdrIntensifLainnya
        {
            get { return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrIntensifLainnya); }
            set { base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.GdrIntensifLainnya, value); }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.RataKunjungan
        /// </summary>
        virtual public System.Decimal? RataKunjungan
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.RataKunjungan);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.RataKunjungan, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.RataRata
        /// </summary>
        virtual public System.Decimal? RataRata
        {
            get
            {
                return base.GetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.RataRata);
            }

            set
            {
                base.SetSystemDecimal(RlTxReport31V2025Metadata.ColumnNames.RataRata, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport31V2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport31V2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport3_1V2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport31V2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport31V2025Metadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlTxReport31V2025 entity)
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

            public System.String BorNonIntensif
            {
                get
                {
                    System.Decimal? data = entity.BorNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BorNonIntensif = null;
                    else entity.BorNonIntensif = Convert.ToDecimal(value);
                }
            }

            public System.String BorICU
            {
                get
                {
                    System.Decimal? data = entity.BorICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BorICU = null;
                    else entity.BorICU = Convert.ToDecimal(value);
                }
            }

            public System.String BorNICU
            {
                get
                {
                    System.Decimal? data = entity.BorNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BorNICU = null;
                    else entity.BorNICU = Convert.ToDecimal(value);
                }
            }

            public System.String BorPICU
            {
                get
                {
                    System.Decimal? data = entity.BorPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BorPICU = null;
                    else entity.BorPICU = Convert.ToDecimal(value);
                }
            }

            public System.String BorIntensifLainnya
            {
                get
                {
                    System.Decimal? data = entity.BorIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BorIntensifLainnya = null;
                    else entity.BorIntensifLainnya = Convert.ToDecimal(value);
                }
            }

            public System.String LosNonIntensif
            {
                get
                {
                    System.Decimal? data = entity.LosNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LosNonIntensif = null;
                    else entity.LosNonIntensif = Convert.ToDecimal(value);
                }
            }

            public System.String LosICU
            {
                get
                {
                    System.Decimal? data = entity.LosICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LosICU = null;
                    else entity.LosICU = Convert.ToDecimal(value);
                }
            }

            public System.String LosNICU
            {
                get
                {
                    System.Decimal? data = entity.LosNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LosNICU = null;
                    else entity.LosNICU = Convert.ToDecimal(value);
                }
            }

            public System.String LosPICU
            {
                get
                {
                    System.Decimal? data = entity.LosPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LosPICU = null;
                    else entity.LosPICU = Convert.ToDecimal(value);
                }
            }

            public System.String LosIntensifLainnya
            {
                get
                {
                    System.Decimal? data = entity.LosIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LosIntensifLainnya = null;
                    else entity.LosIntensifLainnya = Convert.ToDecimal(value);
                }
            }

            public System.String BtoNonIntensif
            {
                get
                {
                    System.Decimal? data = entity.BtoNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BtoNonIntensif = null;
                    else entity.BtoNonIntensif = Convert.ToDecimal(value);
                }
            }

            public System.String BtoICU
            {
                get
                {
                    System.Decimal? data = entity.BtoICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BtoICU = null;
                    else entity.BtoICU = Convert.ToDecimal(value);
                }
            }

            public System.String BtoNICU
            {
                get
                {
                    System.Decimal? data = entity.BtoNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BtoNICU = null;
                    else entity.BtoNICU = Convert.ToDecimal(value);
                }
            }

            public System.String BtoPICU
            {
                get
                {
                    System.Decimal? data = entity.BtoPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BtoPICU = null;
                    else entity.BtoPICU = Convert.ToDecimal(value);
                }
            }

            public System.String BtoIntensifLainnya
            {
                get
                {
                    System.Decimal? data = entity.BtoIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BtoIntensifLainnya = null;
                    else entity.BtoIntensifLainnya = Convert.ToDecimal(value);
                }
            }

            public System.String ToiNonIntensif
            {
                get
                {
                    System.Decimal? data = entity.ToiNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToiNonIntensif = null;
                    else entity.ToiNonIntensif = Convert.ToDecimal(value);
                }
            }

            public System.String ToiICU
            {
                get
                {
                    System.Decimal? data = entity.ToiICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToiICU = null;
                    else entity.ToiICU = Convert.ToDecimal(value);
                }
            }

            public System.String ToiNICU
            {
                get
                {
                    System.Decimal? data = entity.ToiNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToiNICU = null;
                    else entity.ToiNICU = Convert.ToDecimal(value);
                }
            }

            public System.String ToiPICU
            {
                get
                {
                    System.Decimal? data = entity.ToiPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToiPICU = null;
                    else entity.ToiPICU = Convert.ToDecimal(value);
                }
            }

            public System.String ToiIntensifLainnya
            {
                get
                {
                    System.Decimal? data = entity.ToiIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToiIntensifLainnya = null;
                    else entity.ToiIntensifLainnya = Convert.ToDecimal(value);
                }
            }

            public System.String NdrNonIntensif
            {
                get
                {
                    System.Decimal? data = entity.NdrNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NdrNonIntensif = null;
                    else entity.NdrNonIntensif = Convert.ToDecimal(value);
                }
            }

            public System.String NdrICU
            {
                get
                {
                    System.Decimal? data = entity.NdrICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NdrICU = null;
                    else entity.NdrICU = Convert.ToDecimal(value);
                }
            }

            public System.String NdrNICU
            {
                get
                {
                    System.Decimal? data = entity.NdrNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NdrNICU = null;
                    else entity.NdrNICU = Convert.ToDecimal(value);
                }
            }

            public System.String NdrPICU
            {
                get
                {
                    System.Decimal? data = entity.NdrPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NdrPICU = null;
                    else entity.NdrPICU = Convert.ToDecimal(value);
                }
            }

            public System.String NdrIntensifLainnya
            {
                get
                {
                    System.Decimal? data = entity.NdrIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NdrIntensifLainnya = null;
                    else entity.NdrIntensifLainnya = Convert.ToDecimal(value);
                }
            }

            public System.String GdrNonIntensif
            {
                get
                {
                    System.Decimal? data = entity.GdrNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GdrNonIntensif = null;
                    else entity.GdrNonIntensif = Convert.ToDecimal(value);
                }
            }

            public System.String GdrICU
            {
                get
                {
                    System.Decimal? data = entity.GdrICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GdrICU = null;
                    else entity.GdrICU = Convert.ToDecimal(value);
                }
            }

            public System.String GdrNICU
            {
                get
                {
                    System.Decimal? data = entity.GdrNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GdrNICU = null;
                    else entity.GdrNICU = Convert.ToDecimal(value);
                }
            }

            public System.String GdrPICU
            {
                get
                {
                    System.Decimal? data = entity.GdrPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GdrPICU = null;
                    else entity.GdrPICU = Convert.ToDecimal(value);
                }
            }

            public System.String GdrIntensifLainnya
            {
                get
                {
                    System.Decimal? data = entity.GdrIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GdrIntensifLainnya = null;
                    else entity.GdrIntensifLainnya = Convert.ToDecimal(value);
                }
            }

            public System.String RataKunjungan
            {
                get
                {
                    System.Decimal? data = entity.RataKunjungan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RataKunjungan = null;
                    else entity.RataKunjungan = Convert.ToDecimal(value);
                }
            }

            public System.String RataRata
            {
                get
                {
                    System.Decimal? data = entity.RataRata;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RataRata = null;
                    else entity.RataRata = Convert.ToDecimal(value);
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


            private esRlTxReport31V2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport31V2025Query query)
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
                throw new Exception("esRlTxReport31V2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlTxReport31V2025 : esRlTxReport31V2025
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
    abstract public class esRlTxReport31V2025Query : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport31V2025Metadata.Meta();
            }
        }


        public esQueryItem RlTxReportNo
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
            }
        }

        public esQueryItem BorNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BorNonIntensif, esSystemType.Decimal);
            }
        }

        public esQueryItem BorICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BorICU, esSystemType.Decimal);
            }
        }

        public esQueryItem BorNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BorNICU, esSystemType.Decimal);
            }
        }

        public esQueryItem BorPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BorPICU, esSystemType.Decimal);
            }
        }

        public esQueryItem BorIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BorIntensifLainnya, esSystemType.Decimal);
            }
        }

        public esQueryItem LosNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.LosNonIntensif, esSystemType.Decimal);
            }
        }

        public esQueryItem LosICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.LosICU, esSystemType.Decimal);
            }
        }

        public esQueryItem LosNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.LosNICU, esSystemType.Decimal);
            }
        }

        public esQueryItem LosPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.LosPICU, esSystemType.Decimal);
            }
        }

        public esQueryItem LosIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.LosIntensifLainnya, esSystemType.Decimal);
            }
        }

        public esQueryItem BtoNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BtoNonIntensif, esSystemType.Decimal);
            }
        }

        public esQueryItem BtoICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BtoICU, esSystemType.Decimal);
            }
        }

        public esQueryItem BtoNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BtoNICU, esSystemType.Decimal);
            }
        }

        public esQueryItem BtoPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BtoPICU, esSystemType.Decimal);
            }
        }

        public esQueryItem BtoIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.BtoIntensifLainnya, esSystemType.Decimal);
            }
        }

        public esQueryItem ToiNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.ToiNonIntensif, esSystemType.Decimal);
            }
        }

        public esQueryItem ToiICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.ToiICU, esSystemType.Decimal);
            }
        }

        public esQueryItem ToiNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.ToiNICU, esSystemType.Decimal);
            }
        }

        public esQueryItem ToiPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.ToiPICU, esSystemType.Decimal);
            }
        }

        public esQueryItem ToiIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.ToiIntensifLainnya, esSystemType.Decimal);
            }
        }

        public esQueryItem NdrNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.NdrNonIntensif, esSystemType.Decimal);
            }
        }

        public esQueryItem NdrICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.NdrICU, esSystemType.Decimal);
            }
        }

        public esQueryItem NdrNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.NdrNICU, esSystemType.Decimal);
            }
        }

        public esQueryItem NdrPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.NdrPICU, esSystemType.Decimal);
            }
        }

        public esQueryItem NdrIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.NdrIntensifLainnya, esSystemType.Decimal);
            }
        }

        public esQueryItem GdrNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.GdrNonIntensif, esSystemType.Decimal);
            }
        }

        public esQueryItem GdrICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.GdrICU, esSystemType.Decimal);
            }
        }

        public esQueryItem GdrNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.GdrNICU, esSystemType.Decimal);
            }
        }

        public esQueryItem GdrPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.GdrPICU, esSystemType.Decimal);
            }
        }

        public esQueryItem GdrIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.GdrIntensifLainnya, esSystemType.Decimal);
            }
        }

        public esQueryItem RataKunjungan
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.RataKunjungan, esSystemType.Decimal);
            }
        }

        public esQueryItem RataRata
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.RataRata, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport31V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport31V2025Collection")]
    public partial class RlTxReport31V2025Collection : esRlTxReport31V2025Collection, IEnumerable<RlTxReport31V2025>
    {
        public RlTxReport31V2025Collection()
        {

        }

        public static implicit operator List<RlTxReport31V2025>(RlTxReport31V2025Collection coll)
        {
            List<RlTxReport31V2025> list = new List<RlTxReport31V2025>();

            foreach (RlTxReport31V2025 emp in coll)
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
                return RlTxReport31V2025Metadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport31V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport31V2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport31V2025();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlTxReport31V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport31V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlTxReport31V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlTxReport31V2025 AddNew()
        {
            RlTxReport31V2025 entity = base.AddNewEntity() as RlTxReport31V2025;

            return entity;
        }

        public RlTxReport31V2025 FindByPrimaryKey(System.String rlTxReportNo)
        {
            return base.FindByPrimaryKey(rlTxReportNo) as RlTxReport31V2025;
        }


        #region IEnumerable<RlTxReport31V2025> Members

        IEnumerator<RlTxReport31V2025> IEnumerable<RlTxReport31V2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport31V2025;
            }
        }

        #endregion

        private RlTxReport31V2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport3_1V2025' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport31V2025 ({RlTxReportNo})")]
    [Serializable]
    public partial class RlTxReport31V2025 : esRlTxReport31V2025
    {
        public RlTxReport31V2025()
        {

        }

        public RlTxReport31V2025(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport31V2025Metadata.Meta();
            }
        }



        override protected esRlTxReport31V2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport31V2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlTxReport31V2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport31V2025Query();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlTxReport31V2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport31V2025Query query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlTxReport31V2025Query : esRlTxReport31V2025Query
    {
        public RlTxReport31V2025Query()
        {

        }

        public RlTxReport31V2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport31V2025Query";
        }


    }


    [Serializable]
    public partial class RlTxReport31V2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport31V2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.RlTxReportNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BorNonIntensif, 1, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BorNonIntensif;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BorICU, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BorICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BorNICU, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BorNICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BorPICU, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BorPICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BorIntensifLainnya, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BorIntensifLainnya;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.LosNonIntensif, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.LosNonIntensif;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.LosICU, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.LosICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.LosNICU, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.LosNICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.LosPICU, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.LosPICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.LosIntensifLainnya, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.LosIntensifLainnya;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BtoNonIntensif, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BtoNonIntensif;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BtoICU, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BtoICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BtoNICU, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BtoNICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BtoPICU, 14, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BtoPICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.BtoIntensifLainnya, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.BtoIntensifLainnya;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.ToiNonIntensif, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.ToiNonIntensif;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.ToiICU, 17, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.ToiICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.ToiNICU, 18, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.ToiNICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.ToiPICU, 19, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.ToiPICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.ToiIntensifLainnya, 20, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.ToiIntensifLainnya;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.NdrNonIntensif, 21, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.NdrNonIntensif;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.NdrICU, 22, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.NdrICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.NdrNICU, 23, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.NdrNICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.NdrPICU, 24, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.NdrPICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.NdrIntensifLainnya, 25, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.NdrIntensifLainnya;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.GdrNonIntensif, 26, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.GdrNonIntensif;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.GdrICU, 27, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.GdrICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.GdrNICU, 28, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.GdrNICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.GdrPICU, 29, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.GdrPICU;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.GdrIntensifLainnya, 30, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.GdrIntensifLainnya;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.RataKunjungan, 31, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.RataKunjungan;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.RataRata, 32, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.RataRata;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.LastUpdateDateTime, 33, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31V2025Metadata.ColumnNames.LastUpdateByUserID, 34, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport31V2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport31V2025Metadata Meta()
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
            public const string BorNonIntensif = "BorNonIntensif";
            public const string BorICU = "BorICU";
            public const string BorNICU = "BorNICU";
            public const string BorPICU = "BorPICU";
            public const string BorIntensifLainnya = "BorIntensifLainnya";
            public const string LosNonIntensif = "LosNonIntensif";
            public const string LosICU = "LosICU";
            public const string LosNICU = "LosNICU";
            public const string LosPICU = "LosPICU";
            public const string LosIntensifLainnya = "LosIntensifLainnya";
            public const string BtoNonIntensif = "BtoNonIntensif";
            public const string BtoICU = "BtoICU";
            public const string BtoNICU = "BtoNICU";
            public const string BtoPICU = "BtoPICU";
            public const string BtoIntensifLainnya = "BtoIntensifLainnya";
            public const string ToiNonIntensif = "ToiNonIntensif";
            public const string ToiICU = "ToiICU";
            public const string ToiNICU = "ToiNICU";
            public const string ToiPICU = "ToiPICU";
            public const string ToiIntensifLainnya = "ToiIntensifLainnya";
            public const string NdrNonIntensif = "NdrNonIntensif";
            public const string NdrICU = "NdrICU";
            public const string NdrNICU = "NdrNICU";
            public const string NdrPICU = "NdrPICU";
            public const string NdrIntensifLainnya = "NdrIntensifLainnya";
            public const string GdrNonIntensif = "GdrNonIntensif";
            public const string GdrICU = "GdrICU";
            public const string GdrNICU = "GdrNICU";
            public const string GdrPICU = "GdrPICU";
            public const string GdrIntensifLainnya = "GdrIntensifLainnya";
            public const string RataKunjungan = "RataKunjungan";
            public const string RataRata = "RataRata";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlTxReportNo = "RlTxReportNo";
            public const string BorNonIntensif = "BorNonIntensif";
            public const string BorICU = "BorICU";
            public const string BorNICU = "BorNICU";
            public const string BorPICU = "BorPICU";
            public const string BorIntensifLainnya = "BorIntensifLainnya";
            public const string LosNonIntensif = "LosNonIntensif";
            public const string LosICU = "LosICU";
            public const string LosNICU = "LosNICU";
            public const string LosPICU = "LosPICU";
            public const string LosIntensifLainnya = "LosIntensifLainnya";
            public const string BtoNonIntensif = "BtoNonIntensif";
            public const string BtoICU = "BtoICU";
            public const string BtoNICU = "BtoNICU";
            public const string BtoPICU = "BtoPICU";
            public const string BtoIntensifLainnya = "BtoIntensifLainnya";
            public const string ToiNonIntensif = "ToiNonIntensif";
            public const string ToiICU = "ToiICU";
            public const string ToiNICU = "ToiNICU";
            public const string ToiPICU = "ToiPICU";
            public const string ToiIntensifLainnya = "ToiIntensifLainnya";
            public const string NdrNonIntensif = "NdrNonIntensif";
            public const string NdrICU = "NdrICU";
            public const string NdrNICU = "NdrNICU";
            public const string NdrPICU = "NdrPICU";
            public const string NdrIntensifLainnya = "NdrIntensifLainnya";
            public const string GdrNonIntensif = "GdrNonIntensif";
            public const string GdrICU = "GdrICU";
            public const string GdrNICU = "GdrNICU";
            public const string GdrPICU = "GdrPICU";
            public const string GdrIntensifLainnya = "GdrIntensifLainnya";
            public const string RataKunjungan = "RataKunjungan";
            public const string RataRata = "RataRata";
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
            lock (typeof(RlTxReport31V2025Metadata))
            {
                if (RlTxReport31V2025Metadata.mapDelegates == null)
                {
                    RlTxReport31V2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport31V2025Metadata.meta == null)
                {
                    RlTxReport31V2025Metadata.meta = new RlTxReport31V2025Metadata();
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
                meta.AddTypeMap("BorNonIntensif", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BorICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BorNICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BorPICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BorIntensifLainnya", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LosNonIntensif", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LosICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LosNICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LosPICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LosIntensifLainnya", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BtoNonIntensif", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BtoICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BtoNICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BtoPICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BtoIntensifLainnya", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ToiNonIntensif", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ToiICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ToiNICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ToiPICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ToiIntensifLainnya", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("NdrNonIntensif", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("NdrICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("NdrNICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("NdrPICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("NdrIntensifLainnya", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("GdrNonIntensif", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("GdrICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("GdrNICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("GdrPICU", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("GdrIntensifLainnya", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RataKunjungan", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RataRata", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlTxReport3_1V2025";
                meta.Destination = "RlTxReport3_1V2025";

                meta.spInsert = "proc_RlTxReport3_1V2025Insert";
                meta.spUpdate = "proc_RlTxReport3_1V2025Update";
                meta.spDelete = "proc_RlTxReport3_1V2025Delete";
                meta.spLoadAll = "proc_RlTxReport3_1V2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport3_1V2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport31V2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
