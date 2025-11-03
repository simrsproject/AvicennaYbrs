/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/5/2020 2:12:31 PM
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
    abstract public class esRlTxReport31ItemV2025Collection : esEntityCollectionWAuditLog
    {
        public esRlTxReport31ItemV2025Collection()
        {

        }


        protected override string GetCollectionName()
        {
            return "RlTxReport31ItemV2025Collection";
        }

        #region Query Logic
        protected void InitQuery(esRlTxReport31ItemV2025Query query)
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
            this.InitQuery(query as esRlTxReport31ItemV2025Query);
        }
        #endregion

        virtual public RlTxReport31ItemV2025 DetachEntity(RlTxReport31ItemV2025 entity)
        {
            return base.DetachEntity(entity) as RlTxReport31ItemV2025;
        }

        virtual public RlTxReport31ItemV2025 AttachEntity(RlTxReport31ItemV2025 entity)
        {
            return base.AttachEntity(entity) as RlTxReport31ItemV2025;
        }

        virtual public void Combine(RlTxReport31ItemV2025Collection collection)
        {
            base.Combine(collection);
        }

        new public RlTxReport31ItemV2025 this[int index]
        {
            get
            {
                return base[index] as RlTxReport31ItemV2025;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlTxReport31ItemV2025);
        }
    }

    [Serializable]
    abstract public class esRlTxReport31ItemV2025 : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlTxReport31ItemV2025Query GetDynamicQuery()
        {
            return null;
        }

        public esRlTxReport31ItemV2025()
        {
        }

        public esRlTxReport31ItemV2025(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String periodMonth, String periodEndMonth, String periodYear)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(periodMonth, periodEndMonth, periodYear);
            else
                return LoadByPrimaryKeyStoredProcedure(periodMonth, periodEndMonth, periodYear);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String periodMonth, string periodEndMonth, String periodYear)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(periodMonth, periodEndMonth, periodYear);
            else
                return LoadByPrimaryKeyStoredProcedure(periodMonth, periodEndMonth, periodYear);
        }

        private bool LoadByPrimaryKeyDynamic(String periodMonth, string periodEndMonth, String periodYear)
        {
            esRlTxReport31ItemV2025Query query = this.GetDynamicQuery();
            query.Where(query.PeriodMonth >= periodMonth, query.PeriodMonth <= periodEndMonth, query.PeriodYear == periodYear);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String periodMonth, string periodEndMonth, String periodYear)
        {
            esParameters parms = new esParameters();
            parms.Add("PeriodMonth", periodMonth);
            parms.Add("PeriodEndMonth", periodEndMonth);
            parms.Add("PeriodYear", periodYear);
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
                        case "PeriodMonth": this.str.PeriodMonth = (string)value; break;
                        case "PeriodYear": this.str.PeriodYear = (string)value; break;
                        case "HariPerawatanNonIntensif": this.str.HariPerawatanNonIntensif = (string)value; break;
                        case "HariPerawatanICU": this.str.HariPerawatanICU = (string)value; break;
                        case "HariPerawatanNICU": this.str.HariPerawatanNICU = (string)value; break;
                        case "HariPerawatanPICU": this.str.HariPerawatanPICU = (string)value; break;
                        case "HariPerawatanIntensifLainnya": this.str.HariPerawatanIntensifLainnya = (string)value; break;
                        case "LamaDirawatNonIntensif": this.str.LamaDirawatNonIntensif = (string)value; break;
                        case "LamaDirawatICU": this.str.LamaDirawatICU = (string)value; break;
                        case "LamaDirawatNICU": this.str.LamaDirawatNICU = (string)value; break;
                        case "LamaDirawatPICU": this.str.LamaDirawatPICU = (string)value; break;
                        case "LamaDirawatIntensifLainnya": this.str.LamaDirawatIntensifLainnya = (string)value; break;
                        case "KeluarNonIntensif": this.str.KeluarNonIntensif = (string)value; break;
                        case "KeluarICU": this.str.KeluarICU = (string)value; break;
                        case "KeluarNICU": this.str.KeluarNICU = (string)value; break;
                        case "KeluarPICU": this.str.KeluarPICU = (string)value; break;
                        case "KeluarIntensifLainnya": this.str.KeluarIntensifLainnya = (string)value; break;
                        case "KeluarMati48NonIntensif": this.str.KeluarMati48NonIntensif = (string)value; break;
                        case "KeluarMati48ICU": this.str.KeluarMati48ICU = (string)value; break;
                        case "KeluarMati48NICU": this.str.KeluarMati48NICU = (string)value; break;
                        case "KeluarMati48PICU": this.str.KeluarMati48PICU = (string)value; break;
                        case "KeluarMati48IntensifLainnya": this.str.KeluarMati48IntensifLainnya = (string)value; break;
                        case "KeluarMatiNonIntensif": this.str.KeluarMatiNonIntensif = (string)value; break;
                        case "KeluarMatiICU": this.str.KeluarMatiICU = (string)value; break;
                        case "KeluarMatiNICU": this.str.KeluarMatiNICU = (string)value; break;
                        case "KeluarMatiPICU": this.str.KeluarMatiPICU = (string)value; break;
                        case "KeluarMatiIntensifLainnya": this.str.KeluarMatiIntensifLainnya = (string)value; break;
                        case "TtNonIntensif": this.str.TtNonIntensif = (string)value; break;
                        case "TtICU": this.str.TtICU = (string)value; break;
                        case "TtNICU": this.str.TtNICU = (string)value; break;
                        case "TtPICU": this.str.TtPICU = (string)value; break;
                        case "TtIntensifLainnya": this.str.TtIntensifLainnya = (string)value; break;
                        case "HariDlmSatuPeriode": this.str.HariDlmSatuPeriode = (string)value; break;
                        case "Kunjungan": this.str.Kunjungan = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "JTtNonIntensif": this.str.JTtNonIntensif = (string)value; break;
                        case "JTtICU": this.str.JTtICU = (string)value; break;
                        case "JTtNICU": this.str.JTtNICU = (string)value; break;
                        case "JTtPICU": this.str.JTtPICU = (string)value; break;
                        case "JTtIntensifLainnya": this.str.JTtIntensifLainnya = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "HariPerawatanNonIntensif":
                            if (value == null || value is System.Int32)
                                this.HariPerawatanNonIntensif = (System.Int32?)value;
                            break;
                        case "HariPerawatanICU":
                            if (value == null || value is System.Int32)
                                this.HariPerawatanICU = (System.Int32?)value;
                            break;
                        case "HariPerawatanNICU":
                            if (value == null || value is System.Int32)
                                this.HariPerawatanNICU = (System.Int32?)value;
                            break;
                        case "HariPerawatanPICU":
                            if (value == null || value is System.Int32)
                                this.HariPerawatanPICU = (System.Int32?)value;
                            break;
                        case "HariPerawatanIntensifLainnya":
                            if (value == null || value is System.Int32)
                                this.HariPerawatanIntensifLainnya = (System.Int32?)value;
                            break;

                        case "LamaDirawatNonIntensif":
                            if (value == null || value is System.Int32)
                                this.LamaDirawatNonIntensif = (System.Int32?)value;
                            break;
                        case "LamaDirawatICU":
                            if (value == null || value is System.Int32)
                                this.LamaDirawatICU = (System.Int32?)value;
                            break;
                        case "LamaDirawatNICU":
                            if (value == null || value is System.Int32)
                                this.LamaDirawatNICU = (System.Int32?)value;
                            break;
                        case "LamaDirawatPICU":
                            if (value == null || value is System.Int32)
                                this.LamaDirawatPICU = (System.Int32?)value;
                            break;
                        case "LamaDirawatIntensifLainnya":
                            if (value == null || value is System.Int32)
                                this.LamaDirawatIntensifLainnya = (System.Int32?)value;
                            break;

                        case "KeluarNonIntensif":
                            if (value == null || value is System.Int32)
                                this.KeluarNonIntensif = (System.Int32?)value;
                            break;
                        case "KeluarICU":
                            if (value == null || value is System.Int32)
                                this.KeluarICU = (System.Int32?)value;
                            break;
                        case "KeluarNICU":
                            if (value == null || value is System.Int32)
                                this.KeluarNICU = (System.Int32?)value;
                            break;
                        case "KeluarPICU":
                            if (value == null || value is System.Int32)
                                this.KeluarPICU = (System.Int32?)value;
                            break;
                        case "KeluarIntensifLainnya":
                            if (value == null || value is System.Int32)
                                this.KeluarIntensifLainnya = (System.Int32?)value;
                            break;

                        case "KeluarMati48NonIntensif":
                            if (value == null || value is System.Int32)
                                this.KeluarMati48NonIntensif = (System.Int32?)value;
                            break;
                        case "KeluarMati48ICU":
                            if (value == null || value is System.Int32)
                                this.KeluarMati48ICU = (System.Int32?)value;
                            break;
                        case "KeluarMati48NICU":
                            if (value == null || value is System.Int32)
                                this.KeluarMati48NICU = (System.Int32?)value;
                            break;
                        case "KeluarMati48PICU":
                            if (value == null || value is System.Int32)
                                this.KeluarMati48PICU = (System.Int32?)value;
                            break;
                        case "KeluarMati48IntensifLainnya":
                            if (value == null || value is System.Int32)
                                this.KeluarMati48IntensifLainnya = (System.Int32?)value;
                            break;

                        case "KeluarMatiNonIntensif":
                            if (value == null || value is System.Int32)
                                this.KeluarMatiNonIntensif = (System.Int32?)value;
                            break;
                        case "KeluarMatiICU":
                            if (value == null || value is System.Int32)
                                this.KeluarMatiICU = (System.Int32?)value;
                            break;
                        case "KeluarMatiNICU":
                            if (value == null || value is System.Int32)
                                this.KeluarMatiNICU = (System.Int32?)value;
                            break;
                        case "KeluarMatiPICU":
                            if (value == null || value is System.Int32)
                                this.KeluarMatiPICU = (System.Int32?)value;
                            break;
                        case "KeluarMatiIntensifLainnya":
                            if (value == null || value is System.Int32)
                                this.KeluarMatiIntensifLainnya = (System.Int32?)value;
                            break;

                        case "TtNonIntensif":
                            if (value == null || value is System.Int32)
                                this.TtNonIntensif = (System.Int32?)value;
                            break;
                        case "TtICU":
                            if (value == null || value is System.Int32)
                                this.TtICU = (System.Int32?)value;
                            break;
                        case "TtNICU":
                            if (value == null || value is System.Int32)
                                this.TtNICU = (System.Int32?)value;
                            break;
                        case "TtPICU":
                            if (value == null || value is System.Int32)
                                this.TtPICU = (System.Int32?)value;
                            break;
                        case "TtIntensifLainnya":
                            if (value == null || value is System.Int32)
                                this.TtIntensifLainnya = (System.Int32?)value;
                            break;

                        case "HariDlmSatuPeriode":

                            if (value == null || value is System.Int32)
                                this.HariDlmSatuPeriode = (System.Int32?)value;
                            break;

                        case "Kunjungan":

                            if (value == null || value is System.Int32)
                                this.Kunjungan = (System.Int32?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "JTtNonIntensif":
                            if (value == null || value is System.Int32)
                                this.JTtNonIntensif = (System.Int32?)value;
                            break;
                        case "JTtICU":
                            if (value == null || value is System.Int32)
                                this.JTtICU = (System.Int32?)value;
                            break;
                        case "JTtNICU":
                            if (value == null || value is System.Int32)
                                this.JTtNICU = (System.Int32?)value;
                            break;
                        case "JTtPICU":
                            if (value == null || value is System.Int32)
                                this.JTtPICU = (System.Int32?)value;
                            break;
                        case "JTtIntensifLainnya":
                            if (value == null || value is System.Int32)
                                this.JTtIntensifLainnya = (System.Int32?)value;
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
        /// Maps to RlTxReport31ItemV2025.PeriodMonth
        /// </summary>
        virtual public System.String PeriodMonth
        {
            get
            {
                return base.GetSystemString(RlTxReport31ItemV2025Metadata.ColumnNames.PeriodMonth);
            }

            set
            {
                base.SetSystemString(RlTxReport31ItemV2025Metadata.ColumnNames.PeriodMonth, value);
            }
        }
        /// <summary>
        /// Maps to RlTxReport31ItemV2025.PeriodYear
        /// </summary>
        virtual public System.String PeriodYear
        {
            get
            {
                return base.GetSystemString(RlTxReport31ItemV2025Metadata.ColumnNames.PeriodYear);
            }

            set
            {
                base.SetSystemString(RlTxReport31ItemV2025Metadata.ColumnNames.PeriodYear, value);
            }
        }
        /// <summary>
        /// Maps to RlTxReport31ItemV2025.HariPerawatanNonIntensif
        /// </summary>
        virtual public System.Int32? HariPerawatanNonIntensif
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanNonIntensif);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanNonIntensif, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.HariPerawatanICU
        /// </summary>
        virtual public System.Int32? HariPerawatanICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.HariPerawatanNICU
        /// </summary>
        virtual public System.Int32? HariPerawatanNICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanNICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanNICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.HariPerawatanPICU
        /// </summary>
        virtual public System.Int32? HariPerawatanPICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanPICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanPICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.HariPerawatanIntensifLainnya
        /// </summary>
        virtual public System.Int32? HariPerawatanIntensifLainnya
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanIntensifLainnya);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanIntensifLainnya, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.LamaDirawatNonIntensif
        /// </summary>
        virtual public System.Int32? LamaDirawatNonIntensif
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatNonIntensif);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatNonIntensif, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.LamaDirawatICU
        /// </summary>
        virtual public System.Int32? LamaDirawatICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.LamaDirawatNICU
        /// </summary>
        virtual public System.Int32? LamaDirawatNICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatNICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatNICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.LamaDirawatPICU
        /// </summary>
        virtual public System.Int32? LamaDirawatPICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatPICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatPICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.LamaDirawatIntensifLainnya
        /// </summary>
        virtual public System.Int32? LamaDirawatIntensifLainnya
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatIntensifLainnya);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatIntensifLainnya, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarNonIntensif
        /// </summary>
        virtual public System.Int32? KeluarNonIntensif
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarNonIntensif);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarNonIntensif, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarICU
        /// </summary>
        virtual public System.Int32? KeluarICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarNICU
        /// </summary>
        virtual public System.Int32? KeluarNICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarNICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarNICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarPICU
        /// </summary>
        virtual public System.Int32? KeluarPICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarPICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarPICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarIntensifLainnya
        /// </summary>
        virtual public System.Int32? KeluarIntensifLainnya
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarIntensifLainnya);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarIntensifLainnya, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMati48NonIntensif
        /// </summary>
        virtual public System.Int32? KeluarMati48NonIntensif
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48NonIntensif);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48NonIntensif, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMati48ICU
        /// </summary>
        virtual public System.Int32? KeluarMati48ICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48ICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48ICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMati48NICU
        /// </summary>
        virtual public System.Int32? KeluarMati48NICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48NICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48NICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMati48PICU
        /// </summary>
        virtual public System.Int32? KeluarMati48PICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48PICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48PICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMati48IntensifLainnya
        /// </summary>
        virtual public System.Int32? KeluarMati48IntensifLainnya
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48IntensifLainnya);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48IntensifLainnya, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMatiNonIntensif
        /// </summary>
        virtual public System.Int32? KeluarMatiNonIntensif
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiNonIntensif);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiNonIntensif, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMatiICU
        /// </summary>
        virtual public System.Int32? KeluarMatiICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMatiNICU
        /// </summary>
        virtual public System.Int32? KeluarMatiNICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiNICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiNICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMatiPICU
        /// </summary>
        virtual public System.Int32? KeluarMatiPICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiPICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiPICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.KeluarMatiIntensifLainnya
        /// </summary>
        virtual public System.Int32? KeluarMatiIntensifLainnya
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiIntensifLainnya);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiIntensifLainnya, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.TtNonIntensif
        /// </summary>
        virtual public System.Int32? TtNonIntensif
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtNonIntensif);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtNonIntensif, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.TtICU
        /// </summary>
        virtual public System.Int32? TtICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.TtNICU
        /// </summary>
        virtual public System.Int32? TtNICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtNICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtNICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.TtPICU
        /// </summary>
        virtual public System.Int32? TtPICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtPICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtPICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.TtIntensifLainnya
        /// </summary>
        virtual public System.Int32? TtIntensifLainnya
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtIntensifLainnya);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.TtIntensifLainnya, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.HariDlmSatuPeriode
        /// </summary>
        virtual public System.Int32? HariDlmSatuPeriode
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariDlmSatuPeriode);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.HariDlmSatuPeriode, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.Kunjungan
        /// </summary>
        virtual public System.Int32? Kunjungan
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.Kunjungan);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.Kunjungan, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlTxReport31ItemV2025Metadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlTxReport31ItemV2025Metadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlTxReport31ItemV2025Metadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlTxReport31ItemV2025Metadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.JTtNonIntensif
        /// </summary>
        virtual public System.Int32? JTtNonIntensif
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtNonIntensif);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtNonIntensif, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.JTtICU
        /// </summary>
        virtual public System.Int32? JTtICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.JTtNICU
        /// </summary>
        virtual public System.Int32? JTtNICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtNICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtNICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.JTtPICU
        /// </summary>
        virtual public System.Int32? JTtPICU
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtPICU);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtPICU, value);
            }
        }

        /// <summary>
        /// Maps to RlTxReport31ItemV2025.JTtIntensifLainnya
        /// </summary>
        virtual public System.Int32? JTtIntensifLainnya
        {
            get
            {
                return base.GetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtIntensifLainnya);
            }

            set
            {
                base.SetSystemInt32(RlTxReport31ItemV2025Metadata.ColumnNames.JTtIntensifLainnya, value);
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
            public esStrings(esRlTxReport31ItemV2025 entity)
            {
                this.entity = entity;
            }
            public System.String PeriodMonth
            {
                get
                {
                    System.String data = entity.PeriodMonth;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PeriodMonth = null;
                    else entity.PeriodMonth = Convert.ToString(value);
                }
            }
            public System.String PeriodYear
            {
                get
                {
                    System.String data = entity.PeriodYear;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PeriodYear = null;
                    else entity.PeriodYear = Convert.ToString(value);
                }
            }
            public System.String HariPerawatanNonIntensif
            {
                get
                {
                    System.Int32? data = entity.HariPerawatanNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HariPerawatanNonIntensif = null;
                    else entity.HariPerawatanNonIntensif = Convert.ToInt32(value);
                }
            }

            public System.String HariPerawatanICU
            {
                get
                {
                    System.Int32? data = entity.HariPerawatanICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HariPerawatanICU = null;
                    else entity.HariPerawatanICU = Convert.ToInt32(value);
                }
            }

            public System.String HariPerawatanNICU
            {
                get
                {
                    System.Int32? data = entity.HariPerawatanNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HariPerawatanNICU = null;
                    else entity.HariPerawatanNICU = Convert.ToInt32(value);
                }
            }

            public System.String HariPerawatanPICU
            {
                get
                {
                    System.Int32? data = entity.HariPerawatanPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HariPerawatanPICU = null;
                    else entity.HariPerawatanPICU = Convert.ToInt32(value);
                }
            }

            public System.String HariPerawatanIntensifLainnya
            {
                get
                {
                    System.Int32? data = entity.HariPerawatanIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HariPerawatanIntensifLainnya = null;
                    else entity.HariPerawatanIntensifLainnya = Convert.ToInt32(value);
                }
            }

            public System.String LamaDirawatNonIntensif
            {
                get
                {
                    System.Int32? data = entity.LamaDirawatNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LamaDirawatNonIntensif = null;
                    else entity.LamaDirawatNonIntensif = Convert.ToInt32(value);
                }
            }

            public System.String LamaDirawatICU
            {
                get
                {
                    System.Int32? data = entity.LamaDirawatICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LamaDirawatICU = null;
                    else entity.LamaDirawatICU = Convert.ToInt32(value);
                }
            }

            public System.String LamaDirawatNICU
            {
                get
                {
                    System.Int32? data = entity.LamaDirawatNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LamaDirawatNICU = null;
                    else entity.LamaDirawatNICU = Convert.ToInt32(value);
                }
            }

            public System.String LamaDirawatPICU
            {
                get
                {
                    System.Int32? data = entity.LamaDirawatPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LamaDirawatPICU = null;
                    else entity.LamaDirawatPICU = Convert.ToInt32(value);
                }
            }

            public System.String LamaDirawatIntensifLainnya
            {
                get
                {
                    System.Int32? data = entity.LamaDirawatIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LamaDirawatIntensifLainnya = null;
                    else entity.LamaDirawatIntensifLainnya = Convert.ToInt32(value);
                }
            }

            public System.String KeluarNonIntensif
            {
                get
                {
                    System.Int32? data = entity.KeluarNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarNonIntensif = null;
                    else entity.KeluarNonIntensif = Convert.ToInt32(value);
                }
            }

            public System.String KeluarICU
            {
                get
                {
                    System.Int32? data = entity.KeluarICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarICU = null;
                    else entity.KeluarICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarNICU
            {
                get
                {
                    System.Int32? data = entity.KeluarNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarNICU = null;
                    else entity.KeluarNICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarPICU
            {
                get
                {
                    System.Int32? data = entity.KeluarPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarPICU = null;
                    else entity.KeluarPICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarIntensifLainnya
            {
                get
                {
                    System.Int32? data = entity.KeluarIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarIntensifLainnya = null;
                    else entity.KeluarIntensifLainnya = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMati48NonIntensif
            {
                get
                {
                    System.Int32? data = entity.KeluarMati48NonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMati48NonIntensif = null;
                    else entity.KeluarMati48NonIntensif = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMati48ICU
            {
                get
                {
                    System.Int32? data = entity.KeluarMati48ICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMati48ICU = null;
                    else entity.KeluarMati48ICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMati48NICU
            {
                get
                {
                    System.Int32? data = entity.KeluarMati48NICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMati48NICU = null;
                    else entity.KeluarMati48NICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMati48PICU
            {
                get
                {
                    System.Int32? data = entity.KeluarMati48PICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMati48PICU = null;
                    else entity.KeluarMati48PICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMati48IntensifLainnya
            {
                get
                {
                    System.Int32? data = entity.KeluarMati48IntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMati48IntensifLainnya = null;
                    else entity.KeluarMati48IntensifLainnya = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMatiNonIntensif
            {
                get
                {
                    System.Int32? data = entity.KeluarMatiNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMatiNonIntensif = null;
                    else entity.KeluarMatiNonIntensif = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMatiICU
            {
                get
                {
                    System.Int32? data = entity.KeluarMatiICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMatiICU = null;
                    else entity.KeluarMatiICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMatiNICU
            {
                get
                {
                    System.Int32? data = entity.KeluarMatiNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMatiNICU = null;
                    else entity.KeluarMatiNICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMatiPICU
            {
                get
                {
                    System.Int32? data = entity.KeluarMatiPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMatiPICU = null;
                    else entity.KeluarMatiPICU = Convert.ToInt32(value);
                }
            }

            public System.String KeluarMatiIntensifLainnya
            {
                get
                {
                    System.Int32? data = entity.KeluarMatiIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KeluarMatiIntensifLainnya = null;
                    else entity.KeluarMatiIntensifLainnya = Convert.ToInt32(value);
                }
            }

            public System.String TtNonIntensif
            {
                get
                {
                    System.Int32? data = entity.TtNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TtNonIntensif = null;
                    else entity.TtNonIntensif = Convert.ToInt32(value);
                }
            }

            public System.String TtICU
            {
                get
                {
                    System.Int32? data = entity.TtICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TtICU = null;
                    else entity.TtICU = Convert.ToInt32(value);
                }
            }

            public System.String TtNICU
            {
                get
                {
                    System.Int32? data = entity.TtNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TtNICU = null;
                    else entity.TtNICU = Convert.ToInt32(value);
                }
            }

            public System.String TtPICU
            {
                get
                {
                    System.Int32? data = entity.TtPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TtPICU = null;
                    else entity.TtPICU = Convert.ToInt32(value);
                }
            }

            public System.String TtIntensifLainnya
            {
                get
                {
                    System.Int32? data = entity.TtIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TtIntensifLainnya = null;
                    else entity.TtIntensifLainnya = Convert.ToInt32(value);
                }
            }

            public System.String HariDlmSatuPeriode
            {
                get
                {
                    System.Int32? data = entity.HariDlmSatuPeriode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HariDlmSatuPeriode = null;
                    else entity.HariDlmSatuPeriode = Convert.ToInt32(value);
                }
            }
            public System.String Kunjungan
            {
                get
                {
                    System.Int32? data = entity.Kunjungan;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Kunjungan = null;
                    else entity.Kunjungan = Convert.ToInt32(value);
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
            public System.String JTtNonIntensif
            {
                get
                {
                    System.Int32? data = entity.JTtNonIntensif;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JTtNonIntensif = null;
                    else entity.JTtNonIntensif = Convert.ToInt32(value);
                }
            }

            public System.String JTtICU
            {
                get
                {
                    System.Int32? data = entity.JTtICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JTtICU = null;
                    else entity.JTtICU = Convert.ToInt32(value);
                }
            }

            public System.String JTtNICU
            {
                get
                {
                    System.Int32? data = entity.JTtNICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JTtNICU = null;
                    else entity.JTtNICU = Convert.ToInt32(value);
                }
            }

            public System.String JTtPICU
            {
                get
                {
                    System.Int32? data = entity.JTtPICU;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JTtPICU = null;
                    else entity.JTtPICU = Convert.ToInt32(value);
                }
            }

            public System.String JTtIntensifLainnya
            {
                get
                {
                    System.Int32? data = entity.JTtIntensifLainnya;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JTtIntensifLainnya = null;
                    else entity.JTtIntensifLainnya = Convert.ToInt32(value);
                }
            }

            private esRlTxReport31ItemV2025 entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlTxReport31ItemV2025Query query)
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
                throw new Exception("esRlTxReport31ItemV2025 can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class RlTxReport31ItemV2025 : esRlTxReport31ItemV2025
    {
    }

    [Serializable]
    abstract public class esRlTxReport31ItemV2025Query : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport31ItemV2025Metadata.Meta();
            }
        }

        public esQueryItem PeriodMonth
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.PeriodMonth, esSystemType.String);
            }
        }

        public esQueryItem endmonth
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.PeriodMonth, esSystemType.String);
            }
        }

        public esQueryItem PeriodYear
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.PeriodYear, esSystemType.String);
            }
        }

        public esQueryItem HariPerawatanNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanNonIntensif, esSystemType.Int32);
            }
        }

        public esQueryItem HariPerawatanICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanICU, esSystemType.Int32);
            }
        }

        public esQueryItem HariPerawatanNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanNICU, esSystemType.Int32);
            }
        }

        public esQueryItem HariPerawatanPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanPICU, esSystemType.Int32);
            }
        }

        public esQueryItem HariPerawatanIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanIntensifLainnya, esSystemType.Int32);
            }
        }

        public esQueryItem LamaDirawatNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatNonIntensif, esSystemType.Int32);
            }
        }

        public esQueryItem LamaDirawatICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatICU, esSystemType.Int32);
            }
        }

        public esQueryItem LamaDirawatNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatNICU, esSystemType.Int32);
            }
        }

        public esQueryItem LamaDirawatPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatPICU, esSystemType.Int32);
            }
        }

        public esQueryItem LamaDirawatIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatIntensifLainnya, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarNonIntensif, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarNICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarPICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarIntensifLainnya, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMati48NonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48NonIntensif, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMati48ICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48ICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMati48NICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48NICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMati48PICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48PICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMati48IntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48IntensifLainnya, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMatiNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiNonIntensif, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMatiICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMatiNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiNICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMatiPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiPICU, esSystemType.Int32);
            }
        }

        public esQueryItem KeluarMatiIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiIntensifLainnya, esSystemType.Int32);
            }
        }

        public esQueryItem TtNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.TtNonIntensif, esSystemType.Int32);
            }
        }

        public esQueryItem TtICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.TtICU, esSystemType.Int32);
            }
        }

        public esQueryItem TtNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.TtNICU, esSystemType.Int32);
            }
        }

        public esQueryItem TtPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.TtPICU, esSystemType.Int32);
            }
        }

        public esQueryItem TtIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.TtIntensifLainnya, esSystemType.Int32);
            }
        }

        public esQueryItem HariDlmSatuPeriode
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.HariDlmSatuPeriode, esSystemType.Int32);
            }
        }

        public esQueryItem Kunjungan
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.Kunjungan, esSystemType.Int32);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem JTtNonIntensif
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.JTtNonIntensif, esSystemType.Int32);
            }
        }

        public esQueryItem JTtICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.JTtICU, esSystemType.Int32);
            }
        }

        public esQueryItem JTtNICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.JTtNICU, esSystemType.Int32);
            }
        }

        public esQueryItem JTtPICU
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.JTtPICU, esSystemType.Int32);
            }
        }

        public esQueryItem JTtIntensifLainnya
        {
            get
            {
                return new esQueryItem(this, RlTxReport31ItemV2025Metadata.ColumnNames.JTtIntensifLainnya, esSystemType.Int32);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlTxReport31ItemV2025Collection")]
    public partial class RlTxReport31ItemV2025Collection : esRlTxReport31ItemV2025Collection, IEnumerable<RlTxReport31ItemV2025>
    {
        public RlTxReport31ItemV2025Collection()
        {

        }

        public static implicit operator List<RlTxReport31ItemV2025>(RlTxReport31ItemV2025Collection coll)
        {
            List<RlTxReport31ItemV2025> list = new List<RlTxReport31ItemV2025>();

            foreach (RlTxReport31ItemV2025 emp in coll)
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
                return RlTxReport31ItemV2025Metadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport31ItemV2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlTxReport31ItemV2025(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlTxReport31ItemV2025();
        }

        #endregion

        [BrowsableAttribute(false)]
        public RlTxReport31ItemV2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport31ItemV2025Query();
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
        public bool Load(RlTxReport31ItemV2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public RlTxReport31ItemV2025 AddNew()
        {
            RlTxReport31ItemV2025 entity = base.AddNewEntity() as RlTxReport31ItemV2025;

            return entity;
        }
        public RlTxReport31ItemV2025 FindByPrimaryKey(String periodMonth, String periodYear)
        {
            return base.FindByPrimaryKey(periodMonth, periodYear) as RlTxReport31ItemV2025;
        }

        #region IEnumerable< RlTxReport31ItemV2025> Members

        IEnumerator<RlTxReport31ItemV2025> IEnumerable<RlTxReport31ItemV2025>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlTxReport31ItemV2025;
            }
        }

        #endregion

        private RlTxReport31ItemV2025Query query;
    }


    /// <summary>
    /// Encapsulates the 'RlTxReport31ItemV2025' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("RlTxReport31ItemV2025 ({PeriodMonth, PeriodYear})")]
    [Serializable]
    public partial class RlTxReport31ItemV2025 : esRlTxReport31ItemV2025
    {
        public RlTxReport31ItemV2025()
        {
        }

        public RlTxReport31ItemV2025(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlTxReport31ItemV2025Metadata.Meta();
            }
        }

        override protected esRlTxReport31ItemV2025Query GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlTxReport31ItemV2025Query();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public RlTxReport31ItemV2025Query Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlTxReport31ItemV2025Query();
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
        public bool Load(RlTxReport31ItemV2025Query query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlTxReport31ItemV2025Query query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class RlTxReport31ItemV2025Query : esRlTxReport31ItemV2025Query
    {
        public RlTxReport31ItemV2025Query()
        {

        }

        public RlTxReport31ItemV2025Query(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlTxReport31ItemV2025Query";
        }
    }

    [Serializable]
    public partial class RlTxReport31ItemV2025Metadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlTxReport31ItemV2025Metadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.PeriodMonth, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.PeriodMonth;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.PeriodYear, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.PeriodYear;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 4;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanNonIntensif, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.HariPerawatanNonIntensif;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanICU, 3, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.HariPerawatanICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanNICU, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.HariPerawatanNICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanPICU, 5, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.HariPerawatanPICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.HariPerawatanIntensifLainnya, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.HariPerawatanIntensifLainnya;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatNonIntensif, 7, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.LamaDirawatNonIntensif;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatICU, 8, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.LamaDirawatICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatNICU, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.LamaDirawatNICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatPICU, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.LamaDirawatPICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.LamaDirawatIntensifLainnya, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.LamaDirawatIntensifLainnya;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarNonIntensif, 12, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarNonIntensif;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarICU, 13, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarNICU, 14, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarNICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarPICU, 15, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarPICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarIntensifLainnya, 16, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarIntensifLainnya;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48NonIntensif, 17, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMati48NonIntensif;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48ICU, 18, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMati48ICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48NICU, 19, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMati48NICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48PICU, 20, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMati48PICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMati48IntensifLainnya, 21, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMati48IntensifLainnya;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiNonIntensif, 22, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMatiNonIntensif;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiICU, 23, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMatiICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiNICU, 24, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMatiNICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiPICU, 25, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMatiPICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.KeluarMatiIntensifLainnya, 26, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.KeluarMatiIntensifLainnya;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.TtNonIntensif, 27, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.TtNonIntensif;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.TtICU, 28, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.TtICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.TtNICU, 29, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.TtNICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.TtPICU, 30, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.TtPICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.TtIntensifLainnya, 31, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.TtIntensifLainnya;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.HariDlmSatuPeriode, 32, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.HariDlmSatuPeriode;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.Kunjungan, 33, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.Kunjungan;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.LastUpdateDateTime, 34, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.LastUpdateByUserID, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.JTtNonIntensif, 36, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.JTtNonIntensif;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.JTtICU, 37, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.JTtICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.JTtNICU, 38, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.JTtNICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.JTtPICU, 39, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.JTtPICU;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RlTxReport31ItemV2025Metadata.ColumnNames.JTtIntensifLainnya, 40, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlTxReport31ItemV2025Metadata.PropertyNames.JTtIntensifLainnya;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RlTxReport31ItemV2025Metadata Meta()
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
            public const string PeriodMonth = "PeriodMonth";
            public const string PeriodYear = "PeriodYear";
            public const string HariPerawatanNonIntensif = "HariPerawatanNonIntensif";
            public const string HariPerawatanICU = "HariPerawatanICU";
            public const string HariPerawatanNICU = "HariPerawatanNICU";
            public const string HariPerawatanPICU = "HariPerawatanPICU";
            public const string HariPerawatanIntensifLainnya = "HariPerawatanIntensifLainnya";
            public const string LamaDirawatNonIntensif = "LamaDirawatNonIntensif";
            public const string LamaDirawatICU = "LamaDirawatICU";
            public const string LamaDirawatNICU = "LamaDirawatNICU";
            public const string LamaDirawatPICU = "LamaDirawatPICU";
            public const string LamaDirawatIntensifLainnya = "LamaDirawatIntensifLainnya";
            public const string KeluarNonIntensif = "KeluarNonIntensif";
            public const string KeluarICU = "KeluarICU";
            public const string KeluarNICU = "KeluarNICU";
            public const string KeluarPICU = "KeluarPICU";
            public const string KeluarIntensifLainnya = "KeluarIntensifLainnya";
            public const string KeluarMati48NonIntensif = "KeluarMati48NonIntensif";
            public const string KeluarMati48ICU = "KeluarMati48ICU";
            public const string KeluarMati48NICU = "KeluarMati48NICU";
            public const string KeluarMati48PICU = "KeluarMati48PICU";
            public const string KeluarMati48IntensifLainnya = "KeluarMati48IntensifLainnya";
            public const string KeluarMatiNonIntensif = "KeluarMatiNonIntensif";
            public const string KeluarMatiICU = "KeluarMatiICU";
            public const string KeluarMatiNICU = "KeluarMatiNICU";
            public const string KeluarMatiPICU = "KeluarMatiPICU";
            public const string KeluarMatiIntensifLainnya = "KeluarMatiIntensifLainnya";
            public const string TtNonIntensif = "TtNonIntensif";
            public const string TtICU = "TtICU";
            public const string TtNICU = "TtNICU";
            public const string TtPICU = "TtPICU";
            public const string TtIntensifLainnya = "TtIntensifLainnya";
            public const string HariDlmSatuPeriode = "HariDlmSatuPeriode";
            public const string Kunjungan = "Kunjungan";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string JTtNonIntensif = "JTtNonIntensif";
            public const string JTtICU = "JTtICU";
            public const string JTtNICU = "JTtNICU";
            public const string JTtPICU = "JTtPICU";
            public const string JTtIntensifLainnya = "JTtIntensifLainnya";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PeriodMonth = "PeriodMonth";
            public const string PeriodYear = "PeriodYear";
            public const string HariPerawatanNonIntensif = "HariPerawatanNonIntensif";
            public const string HariPerawatanICU = "HariPerawatanICU";
            public const string HariPerawatanNICU = "HariPerawatanNICU";
            public const string HariPerawatanPICU = "HariPerawatanPICU";
            public const string HariPerawatanIntensifLainnya = "HariPerawatanIntensifLainnya";
            public const string LamaDirawatNonIntensif = "LamaDirawatNonIntensif";
            public const string LamaDirawatICU = "LamaDirawatICU";
            public const string LamaDirawatNICU = "LamaDirawatNICU";
            public const string LamaDirawatPICU = "LamaDirawatPICU";
            public const string LamaDirawatIntensifLainnya = "LamaDirawatIntensifLainnya";
            public const string KeluarNonIntensif = "KeluarNonIntensif";
            public const string KeluarICU = "KeluarICU";
            public const string KeluarNICU = "KeluarNICU";
            public const string KeluarPICU = "KeluarPICU";
            public const string KeluarIntensifLainnya = "KeluarIntensifLainnya";
            public const string KeluarMati48NonIntensif = "KeluarMati48NonIntensif";
            public const string KeluarMati48ICU = "KeluarMati48ICU";
            public const string KeluarMati48NICU = "KeluarMati48NICU";
            public const string KeluarMati48PICU = "KeluarMati48PICU";
            public const string KeluarMati48IntensifLainnya = "KeluarMati48IntensifLainnya";
            public const string KeluarMatiNonIntensif = "KeluarMatiNonIntensif";
            public const string KeluarMatiICU = "KeluarMatiICU";
            public const string KeluarMatiNICU = "KeluarMatiNICU";
            public const string KeluarMatiPICU = "KeluarMatiPICU";
            public const string KeluarMatiIntensifLainnya = "KeluarMatiIntensifLainnya";
            public const string TtNonIntensif = "TtNonIntensif";
            public const string TtICU = "TtICU";
            public const string TtNICU = "TtNICU";
            public const string TtPICU = "TtPICU";
            public const string TtIntensifLainnya = "TtIntensifLainnya";
            public const string HariDlmSatuPeriode = "HariDlmSatuPeriode";
            public const string Kunjungan = "Kunjungan";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string JTtNonIntensif = "JTtNonIntensif";
            public const string JTtICU = "JTtICU";
            public const string JTtNICU = "JTtNICU";
            public const string JTtPICU = "JTtPICU";
            public const string JTtIntensifLainnya = "JTtIntensifLainnya";
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
            lock (typeof(RlTxReport31ItemV2025Metadata))
            {
                if (RlTxReport31ItemV2025Metadata.mapDelegates == null)
                {
                    RlTxReport31ItemV2025Metadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlTxReport31ItemV2025Metadata.meta == null)
                {
                    RlTxReport31ItemV2025Metadata.meta = new RlTxReport31ItemV2025Metadata();
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

                meta.AddTypeMap("PeriodMonth", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("HariPerawatanNonIntensif", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HariPerawatanICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HariPerawatanNICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HariPerawatanPICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HariPerawatanIntensifLainnya", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LamaDirawatNonIntensif", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LamaDirawatICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LamaDirawatNICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LamaDirawatPICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LamaDirawatIntensifLainnya", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarNonIntensif", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarNICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarPICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarIntensifLainnya", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMati48NonIntensif", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMati48ICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMati48NICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMati48PICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMati48IntensifLainnya", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMatiNonIntensif", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMatiICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMatiNICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMatiPICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("KeluarMatiIntensifLainnya", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TtNonIntensif", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TtICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TtNICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TtPICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("TtIntensifLainnya", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HariDlmSatuPeriode", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("Kunjungan", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JTtNonIntensif", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JTtICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JTtNICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JTtPICU", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JTtIntensifLainnya", new esTypeMap("int", "System.Int32"));


                meta.Source = "RlTxReport31ItemV2025";
                meta.Destination = "RlTxReport31ItemV2025";
                meta.spInsert = "proc_RlTxReport31ItemV2025Insert";
                meta.spUpdate = "proc_RlTxReport31ItemV2025Update";
                meta.spDelete = "proc_RlTxReport31ItemV2025Delete";
                meta.spLoadAll = "proc_RlTxReport31ItemV2025LoadAll";
                meta.spLoadByPrimaryKey = "proc_RlTxReport31ItemV2025LoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlTxReport31ItemV2025Metadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
