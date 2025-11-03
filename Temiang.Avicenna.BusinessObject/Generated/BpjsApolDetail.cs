/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/31/2025 10:01:18 AM
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
    abstract public class esBpjsApolDetailCollection : esEntityCollectionWAuditLog
    {
        public esBpjsApolDetailCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "BpjsApolDetailCollection";
        }

        #region Query Logic
        protected void InitQuery(esBpjsApolDetailQuery query)
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
            this.InitQuery(query as esBpjsApolDetailQuery);
        }
        #endregion

        virtual public BpjsApolDetail DetachEntity(BpjsApolDetail entity)
        {
            return base.DetachEntity(entity) as BpjsApolDetail;
        }

        virtual public BpjsApolDetail AttachEntity(BpjsApolDetail entity)
        {
            return base.AttachEntity(entity) as BpjsApolDetail;
        }

        virtual public void Combine(BpjsApolDetailCollection collection)
        {
            base.Combine(collection);
        }

        new public BpjsApolDetail this[int index]
        {
            get
            {
                return base[index] as BpjsApolDetail;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(BpjsApolDetail);
        }
    }

    [Serializable]
    abstract public class esBpjsApolDetail : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esBpjsApolDetailQuery GetDynamicQuery()
        {
            return null;
        }

        public esBpjsApolDetail()
        {
        }

        public esBpjsApolDetail(DataRow row)
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
            esBpjsApolDetailQuery query = this.GetDynamicQuery();
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
                        case "PrescriptionNo": this.str.PrescriptionNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ParentNo": this.str.ParentNo = (string)value; break;
                        case "BpjsApolID": this.str.BpjsApolID = (string)value; break;
                        case "NOSJP": this.str.NOSJP = (string)value; break;
                        case "NORESEP": this.str.NORESEP = (string)value; break;
                        case "JNSROBT": this.str.JNSROBT = (string)value; break;
                        case "KDOBT": this.str.KDOBT = (string)value; break;
                        case "NMOBAT": this.str.NMOBAT = (string)value; break;
                        case "SIGNA1OBT": this.str.SIGNA1OBT = (string)value; break;
                        case "SIGNA2OBT": this.str.SIGNA2OBT = (string)value; break;
                        case "PERMINTAAN": this.str.PERMINTAAN = (string)value; break;
                        case "JMLOBT": this.str.JMLOBT = (string)value; break;
                        case "JHO": this.str.JHO = (string)value; break;
                        case "CATKHSOBT": this.str.CATKHSOBT = (string)value; break;
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
                        case "BpjsApolID":

                            if (value == null || value is System.Int32)
                                this.BpjsApolID = (System.Int32?)value;
                            break;
                        case "SIGNA1OBT":

                            if (value == null || value is System.Int32)
                                this.SIGNA1OBT = (System.Int32?)value;
                            break;
                        case "SIGNA2OBT":

                            if (value == null || value is System.Int32)
                                this.SIGNA2OBT = (System.Int32?)value;
                            break;
                        case "PERMINTAAN":

                            if (value == null || value is System.Int32)
                                this.PERMINTAAN = (System.Int32?)value;
                            break;
                        case "JMLOBT":

                            if (value == null || value is System.Int32)
                                this.JMLOBT = (System.Int32?)value;
                            break;
                        case "JHO":

                            if (value == null || value is System.Int32)
                                this.JHO = (System.Int32?)value;
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
        /// Maps to BpjsApolDetail.ID
        /// </summary>
        virtual public System.Int32? ID
        {
            get
            {
                return base.GetSystemInt32(BpjsApolDetailMetadata.ColumnNames.ID);
            }

            set
            {
                base.SetSystemInt32(BpjsApolDetailMetadata.ColumnNames.ID, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.PrescriptionNo
        /// </summary>
        virtual public System.String PrescriptionNo
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.PrescriptionNo);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.PrescriptionNo, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.SequenceNo
        /// </summary>
        virtual public System.String SequenceNo
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.ParentNo
        /// </summary>
        virtual public System.String ParentNo
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.ParentNo);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.ParentNo, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.BpjsApolID
        /// </summary>
        virtual public System.Int32? BpjsApolID
        {
            get
            {
                return base.GetSystemInt32(BpjsApolDetailMetadata.ColumnNames.BpjsApolID);
            }

            set
            {
                base.SetSystemInt32(BpjsApolDetailMetadata.ColumnNames.BpjsApolID, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.NOSJP
        /// </summary>
        virtual public System.String NOSJP
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.NOSJP);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.NOSJP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.NORESEP
        /// </summary>
        virtual public System.String NORESEP
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.NORESEP);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.NORESEP, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.JNSROBT
        /// </summary>
        virtual public System.String JNSROBT
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.JNSROBT);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.JNSROBT, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.KDOBT
        /// </summary>
        virtual public System.String KDOBT
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.KDOBT);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.KDOBT, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.NMOBAT
        /// </summary>
        virtual public System.String NMOBAT
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.NMOBAT);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.NMOBAT, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.SIGNA1OBT
        /// </summary>
        virtual public System.Int32? SIGNA1OBT
        {
            get
            {
                return base.GetSystemInt32(BpjsApolDetailMetadata.ColumnNames.SIGNA1OBT);
            }

            set
            {
                base.SetSystemInt32(BpjsApolDetailMetadata.ColumnNames.SIGNA1OBT, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.SIGNA2OBT
        /// </summary>
        virtual public System.Int32? SIGNA2OBT
        {
            get
            {
                return base.GetSystemInt32(BpjsApolDetailMetadata.ColumnNames.SIGNA2OBT);
            }

            set
            {
                base.SetSystemInt32(BpjsApolDetailMetadata.ColumnNames.SIGNA2OBT, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.PERMINTAAN
        /// </summary>
        virtual public System.Int32? PERMINTAAN
        {
            get
            {
                return base.GetSystemInt32(BpjsApolDetailMetadata.ColumnNames.PERMINTAAN);
            }

            set
            {
                base.SetSystemInt32(BpjsApolDetailMetadata.ColumnNames.PERMINTAAN, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.JMLOBT
        /// </summary>
        virtual public System.Int32? JMLOBT
        {
            get
            {
                return base.GetSystemInt32(BpjsApolDetailMetadata.ColumnNames.JMLOBT);
            }

            set
            {
                base.SetSystemInt32(BpjsApolDetailMetadata.ColumnNames.JMLOBT, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.JHO
        /// </summary>
        virtual public System.Int32? JHO
        {
            get
            {
                return base.GetSystemInt32(BpjsApolDetailMetadata.ColumnNames.JHO);
            }

            set
            {
                base.SetSystemInt32(BpjsApolDetailMetadata.ColumnNames.JHO, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.CATKHSOBT
        /// </summary>
        virtual public System.String CATKHSOBT
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.CATKHSOBT);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.CATKHSOBT, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.METADATA_CODE
        /// </summary>
        virtual public System.String MetadataCode
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.MetadataCode);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.MetadataCode, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.METADATA_MESSAGE
        /// </summary>
        virtual public System.String MetadataMessage
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.MetadataMessage);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.MetadataMessage, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(BpjsApolDetailMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(BpjsApolDetailMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to BpjsApolDetail.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(BpjsApolDetailMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(BpjsApolDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esBpjsApolDetail entity)
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
            public System.String SequenceNo
            {
                get
                {
                    System.String data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToString(value);
                }
            }
            public System.String ParentNo
            {
                get
                {
                    System.String data = entity.ParentNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParentNo = null;
                    else entity.ParentNo = Convert.ToString(value);
                }
            }
            public System.String BpjsApolID
            {
                get
                {
                    System.Int32? data = entity.BpjsApolID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BpjsApolID = null;
                    else entity.BpjsApolID = Convert.ToInt32(value);
                }
            }
            public System.String NOSJP
            {
                get
                {
                    System.String data = entity.NOSJP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NOSJP = null;
                    else entity.NOSJP = Convert.ToString(value);
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
            public System.String JNSROBT
            {
                get
                {
                    System.String data = entity.JNSROBT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JNSROBT = null;
                    else entity.JNSROBT = Convert.ToString(value);
                }
            }
            public System.String KDOBT
            {
                get
                {
                    System.String data = entity.KDOBT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.KDOBT = null;
                    else entity.KDOBT = Convert.ToString(value);
                }
            }
            public System.String NMOBAT
            {
                get
                {
                    System.String data = entity.NMOBAT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NMOBAT = null;
                    else entity.NMOBAT = Convert.ToString(value);
                }
            }
            public System.String SIGNA1OBT
            {
                get
                {
                    System.Int32? data = entity.SIGNA1OBT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SIGNA1OBT = null;
                    else entity.SIGNA1OBT = Convert.ToInt32(value);
                }
            }
            public System.String SIGNA2OBT
            {
                get
                {
                    System.Int32? data = entity.SIGNA2OBT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SIGNA2OBT = null;
                    else entity.SIGNA2OBT = Convert.ToInt32(value);
                }
            }
            public System.String PERMINTAAN
            {
                get
                {
                    System.Int32? data = entity.PERMINTAAN;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PERMINTAAN = null;
                    else entity.PERMINTAAN = Convert.ToInt32(value);
                }
            }
            public System.String JMLOBT
            {
                get
                {
                    System.Int32? data = entity.JMLOBT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JMLOBT = null;
                    else entity.JMLOBT = Convert.ToInt32(value);
                }
            }
            public System.String JHO
            {
                get
                {
                    System.Int32? data = entity.JHO;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.JHO = null;
                    else entity.JHO = Convert.ToInt32(value);
                }
            }
            public System.String CATKHSOBT
            {
                get
                {
                    System.String data = entity.CATKHSOBT;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CATKHSOBT = null;
                    else entity.CATKHSOBT = Convert.ToString(value);
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
            private esBpjsApolDetail entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esBpjsApolDetailQuery query)
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
                throw new Exception("esBpjsApolDetail can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class BpjsApolDetail : esBpjsApolDetail
    {
    }

    [Serializable]
    abstract public class esBpjsApolDetailQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return BpjsApolDetailMetadata.Meta();
            }
        }

        public esQueryItem ID
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.ID, esSystemType.Int32);
            }
        }

        public esQueryItem PrescriptionNo
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.PrescriptionNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.SequenceNo, esSystemType.String);
            }
        }

        public esQueryItem ParentNo
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.ParentNo, esSystemType.String);
            }
        }

        public esQueryItem BpjsApolID
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.BpjsApolID, esSystemType.Int32);
            }
        }

        public esQueryItem NOSJP
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.NOSJP, esSystemType.String);
            }
        }

        public esQueryItem NORESEP
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.NORESEP, esSystemType.String);
            }
        }

        public esQueryItem JNSROBT
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.JNSROBT, esSystemType.String);
            }
        }

        public esQueryItem KDOBT
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.KDOBT, esSystemType.String);
            }
        }

        public esQueryItem NMOBAT
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.NMOBAT, esSystemType.String);
            }
        }

        public esQueryItem SIGNA1OBT
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.SIGNA1OBT, esSystemType.Int32);
            }
        }

        public esQueryItem SIGNA2OBT
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.SIGNA2OBT, esSystemType.Int32);
            }
        }

        public esQueryItem PERMINTAAN
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.PERMINTAAN, esSystemType.Int32);
            }
        }

        public esQueryItem JMLOBT
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.JMLOBT, esSystemType.Int32);
            }
        }

        public esQueryItem JHO
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.JHO, esSystemType.Int32);
            }
        }

        public esQueryItem CATKHSOBT
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.CATKHSOBT, esSystemType.String);
            }
        }

        public esQueryItem MetadataCode
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.MetadataCode, esSystemType.String);
            }
        }

        public esQueryItem MetadataMessage
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.MetadataMessage, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, BpjsApolDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("BpjsApolDetailCollection")]
    public partial class BpjsApolDetailCollection : esBpjsApolDetailCollection, IEnumerable<BpjsApolDetail>
    {
        public BpjsApolDetailCollection()
        {

        }

        public static implicit operator List<BpjsApolDetail>(BpjsApolDetailCollection coll)
        {
            List<BpjsApolDetail> list = new List<BpjsApolDetail>();

            foreach (BpjsApolDetail emp in coll)
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
                return BpjsApolDetailMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsApolDetailQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new BpjsApolDetail(row);
        }

        override protected esEntity CreateEntity()
        {
            return new BpjsApolDetail();
        }

        #endregion

        [BrowsableAttribute(false)]
        public BpjsApolDetailQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsApolDetailQuery();
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
        public bool Load(BpjsApolDetailQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public BpjsApolDetail AddNew()
        {
            BpjsApolDetail entity = base.AddNewEntity() as BpjsApolDetail;

            return entity;
        }
        public BpjsApolDetail FindByPrimaryKey(Int32 iD)
        {
            return base.FindByPrimaryKey(iD) as BpjsApolDetail;
        }

        #region IEnumerable< BpjsApolDetail> Members

        IEnumerator<BpjsApolDetail> IEnumerable<BpjsApolDetail>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as BpjsApolDetail;
            }
        }

        #endregion

        private BpjsApolDetailQuery query;
    }


    /// <summary>
    /// Encapsulates the 'BpjsApolDetail' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BpjsApolDetail ({ID})")]
    [Serializable]
    public partial class BpjsApolDetail : esBpjsApolDetail
    {
        public BpjsApolDetail()
        {
        }

        public BpjsApolDetail(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return BpjsApolDetailMetadata.Meta();
            }
        }

        override protected esBpjsApolDetailQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new BpjsApolDetailQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public BpjsApolDetailQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new BpjsApolDetailQuery();
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
        public bool Load(BpjsApolDetailQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private BpjsApolDetailQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class BpjsApolDetailQuery : esBpjsApolDetailQuery
    {
        public BpjsApolDetailQuery()
        {

        }

        public BpjsApolDetailQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "BpjsApolDetailQuery";
        }
    }

    [Serializable]
    public partial class BpjsApolDetailMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected BpjsApolDetailMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.ID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.ID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.PrescriptionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.PrescriptionNo;
            c.CharacterMaxLength = 30;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.SequenceNo;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.ParentNo, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.ParentNo;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.BpjsApolID, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.BpjsApolID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.NOSJP, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.NOSJP;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.NORESEP, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.NORESEP;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.JNSROBT, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.JNSROBT;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.KDOBT, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.KDOBT;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.NMOBAT, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.NMOBAT;
            c.CharacterMaxLength = 100;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.SIGNA1OBT, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.SIGNA1OBT;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.SIGNA2OBT, 11, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.SIGNA2OBT;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.PERMINTAAN, 12, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.PERMINTAAN;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.JMLOBT, 13, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.JMLOBT;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.JHO, 14, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.JHO;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.CATKHSOBT, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.CATKHSOBT;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.MetadataCode, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.MetadataCode;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.MetadataMessage, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.MetadataMessage;
            c.CharacterMaxLength = 255;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.LastUpdateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.LastUpdateDateTime;
            c.HasDefault = true;
            c.Default = @"(getdate())";
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(BpjsApolDetailMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = BpjsApolDetailMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public BpjsApolDetailMetadata Meta()
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
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ParentNo = "ParentNo";
            public const string BpjsApolID = "BpjsApolID";
            public const string NOSJP = "NOSJP";
            public const string NORESEP = "NORESEP";
            public const string JNSROBT = "JNSROBT";
            public const string KDOBT = "KDOBT";
            public const string NMOBAT = "NMOBAT";
            public const string SIGNA1OBT = "SIGNA1OBT";
            public const string SIGNA2OBT = "SIGNA2OBT";
            public const string PERMINTAAN = "PERMINTAAN";
            public const string JMLOBT = "JMLOBT";
            public const string JHO = "JHO";
            public const string CATKHSOBT = "CATKHSOBT";
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
            public const string PrescriptionNo = "PrescriptionNo";
            public const string SequenceNo = "SequenceNo";
            public const string ParentNo = "ParentNo";
            public const string BpjsApolID = "BpjsApolID";
            public const string NOSJP = "NOSJP";
            public const string NORESEP = "NORESEP";
            public const string JNSROBT = "JNSROBT";
            public const string KDOBT = "KDOBT";
            public const string NMOBAT = "NMOBAT";
            public const string SIGNA1OBT = "SIGNA1OBT";
            public const string SIGNA2OBT = "SIGNA2OBT";
            public const string PERMINTAAN = "PERMINTAAN";
            public const string JMLOBT = "JMLOBT";
            public const string JHO = "JHO";
            public const string CATKHSOBT = "CATKHSOBT";
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
            lock (typeof(BpjsApolDetailMetadata))
            {
                if (BpjsApolDetailMetadata.mapDelegates == null)
                {
                    BpjsApolDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (BpjsApolDetailMetadata.meta == null)
                {
                    BpjsApolDetailMetadata.meta = new BpjsApolDetailMetadata();
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
                meta.AddTypeMap("PrescriptionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParentNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BpjsApolID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("NOSJP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NORESEP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("JNSROBT", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("KDOBT", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NMOBAT", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SIGNA1OBT", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SIGNA2OBT", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PERMINTAAN", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JMLOBT", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("JHO", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("CATKHSOBT", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MetadataCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MetadataMessage", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "BpjsApolDetail";
                meta.Destination = "BpjsApolDetail";
                meta.spInsert = "proc_BpjsApolDetailInsert";
                meta.spUpdate = "proc_BpjsApolDetailUpdate";
                meta.spDelete = "proc_BpjsApolDetailDelete";
                meta.spLoadAll = "proc_BpjsApolDetailLoadAll";
                meta.spLoadByPrimaryKey = "proc_BpjsApolDetailLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private BpjsApolDetailMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
