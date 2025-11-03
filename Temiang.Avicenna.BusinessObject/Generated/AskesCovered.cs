/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/1/2012 11:37:20 AM
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
    abstract public class esAskesCoveredCollection : esEntityCollectionWAuditLog
    {
        public esAskesCoveredCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "AskesCoveredCollection";
        }

        #region Query Logic
        protected void InitQuery(esAskesCoveredQuery query)
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
            this.InitQuery(query as esAskesCoveredQuery);
        }
        #endregion

        virtual public AskesCovered DetachEntity(AskesCovered entity)
        {
            return base.DetachEntity(entity) as AskesCovered;
        }

        virtual public AskesCovered AttachEntity(AskesCovered entity)
        {
            return base.AttachEntity(entity) as AskesCovered;
        }

        virtual public void Combine(AskesCoveredCollection collection)
        {
            base.Combine(collection);
        }

        new public AskesCovered this[int index]
        {
            get
            {
                return base[index] as AskesCovered;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AskesCovered);
        }
    }



    [Serializable]
    abstract public class esAskesCovered : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAskesCoveredQuery GetDynamicQuery()
        {
            return null;
        }

        public esAskesCovered()
        {

        }

        public esAskesCovered(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String registrationNo)
        {
            esAskesCoveredQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "RoomAmount": this.str.RoomAmount = (string)value; break;
                        case "RoomDays": this.str.RoomDays = (string)value; break;
                        case "IccuAmount": this.str.IccuAmount = (string)value; break;
                        case "IccuDays": this.str.IccuDays = (string)value; break;
                        case "HcuAmount": this.str.HcuAmount = (string)value; break;
                        case "HcuDays": this.str.HcuDays = (string)value; break;
                        case "SurgeryAmount": this.str.SurgeryAmount = (string)value; break;
                        case "MedicalSupportAmount": this.str.MedicalSupportAmount = (string)value; break;
                        case "HaemodialiseAmount": this.str.HaemodialiseAmount = (string)value; break;
                        case "CtScanAmount": this.str.CtScanAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RoomAmount":

                            if (value == null || value is System.Decimal)
                                this.RoomAmount = (System.Decimal?)value;
                            break;

                        case "RoomDays":

                            if (value == null || value is System.Int32)
                                this.RoomDays = (System.Int32?)value;
                            break;

                        case "IccuAmount":

                            if (value == null || value is System.Decimal)
                                this.IccuAmount = (System.Decimal?)value;
                            break;

                        case "IccuDays":

                            if (value == null || value is System.Int32)
                                this.IccuDays = (System.Int32?)value;
                            break;

                        case "HcuAmount":

                            if (value == null || value is System.Decimal)
                                this.HcuAmount = (System.Decimal?)value;
                            break;

                        case "HcuDays":

                            if (value == null || value is System.Int32)
                                this.HcuDays = (System.Int32?)value;
                            break;

                        case "SurgeryAmount":

                            if (value == null || value is System.Decimal)
                                this.SurgeryAmount = (System.Decimal?)value;
                            break;

                        case "MedicalSupportAmount":

                            if (value == null || value is System.Decimal)
                                this.MedicalSupportAmount = (System.Decimal?)value;
                            break;

                        case "HaemodialiseAmount":

                            if (value == null || value is System.Decimal)
                                this.HaemodialiseAmount = (System.Decimal?)value;
                            break;

                        case "CtScanAmount":

                            if (value == null || value is System.Decimal)
                                this.CtScanAmount = (System.Decimal?)value;
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
        /// Maps to AskesCovered.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(AskesCoveredMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(AskesCoveredMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.RoomAmount
        /// </summary>
        virtual public System.Decimal? RoomAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCoveredMetadata.ColumnNames.RoomAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCoveredMetadata.ColumnNames.RoomAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.RoomDays
        /// </summary>
        virtual public System.Int32? RoomDays
        {
            get
            {
                return base.GetSystemInt32(AskesCoveredMetadata.ColumnNames.RoomDays);
            }

            set
            {
                base.SetSystemInt32(AskesCoveredMetadata.ColumnNames.RoomDays, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.IccuAmount
        /// </summary>
        virtual public System.Decimal? IccuAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCoveredMetadata.ColumnNames.IccuAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCoveredMetadata.ColumnNames.IccuAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.IccuDays
        /// </summary>
        virtual public System.Int32? IccuDays
        {
            get
            {
                return base.GetSystemInt32(AskesCoveredMetadata.ColumnNames.IccuDays);
            }

            set
            {
                base.SetSystemInt32(AskesCoveredMetadata.ColumnNames.IccuDays, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.HcuAmount
        /// </summary>
        virtual public System.Decimal? HcuAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCoveredMetadata.ColumnNames.HcuAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCoveredMetadata.ColumnNames.HcuAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.HcuDays
        /// </summary>
        virtual public System.Int32? HcuDays
        {
            get
            {
                return base.GetSystemInt32(AskesCoveredMetadata.ColumnNames.HcuDays);
            }

            set
            {
                base.SetSystemInt32(AskesCoveredMetadata.ColumnNames.HcuDays, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.SurgeryAmount
        /// </summary>
        virtual public System.Decimal? SurgeryAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCoveredMetadata.ColumnNames.SurgeryAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCoveredMetadata.ColumnNames.SurgeryAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.MedicalSupportAmount
        /// </summary>
        virtual public System.Decimal? MedicalSupportAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCoveredMetadata.ColumnNames.MedicalSupportAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCoveredMetadata.ColumnNames.MedicalSupportAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.HaemodialiseAmount
        /// </summary>
        virtual public System.Decimal? HaemodialiseAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCoveredMetadata.ColumnNames.HaemodialiseAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCoveredMetadata.ColumnNames.HaemodialiseAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.CtScanAmount
        /// </summary>
        virtual public System.Decimal? CtScanAmount
        {
            get
            {
                return base.GetSystemDecimal(AskesCoveredMetadata.ColumnNames.CtScanAmount);
            }

            set
            {
                base.SetSystemDecimal(AskesCoveredMetadata.ColumnNames.CtScanAmount, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AskesCoveredMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AskesCoveredMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AskesCovered.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AskesCoveredMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AskesCoveredMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAskesCovered entity)
            {
                this.entity = entity;
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

            public System.String RoomAmount
            {
                get
                {
                    System.Decimal? data = entity.RoomAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomAmount = null;
                    else entity.RoomAmount = Convert.ToDecimal(value);
                }
            }

            public System.String RoomDays
            {
                get
                {
                    System.Int32? data = entity.RoomDays;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomDays = null;
                    else entity.RoomDays = Convert.ToInt32(value);
                }
            }

            public System.String IccuAmount
            {
                get
                {
                    System.Decimal? data = entity.IccuAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IccuAmount = null;
                    else entity.IccuAmount = Convert.ToDecimal(value);
                }
            }

            public System.String IccuDays
            {
                get
                {
                    System.Int32? data = entity.IccuDays;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IccuDays = null;
                    else entity.IccuDays = Convert.ToInt32(value);
                }
            }

            public System.String HcuAmount
            {
                get
                {
                    System.Decimal? data = entity.HcuAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HcuAmount = null;
                    else entity.HcuAmount = Convert.ToDecimal(value);
                }
            }

            public System.String HcuDays
            {
                get
                {
                    System.Int32? data = entity.HcuDays;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HcuDays = null;
                    else entity.HcuDays = Convert.ToInt32(value);
                }
            }

            public System.String SurgeryAmount
            {
                get
                {
                    System.Decimal? data = entity.SurgeryAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SurgeryAmount = null;
                    else entity.SurgeryAmount = Convert.ToDecimal(value);
                }
            }

            public System.String MedicalSupportAmount
            {
                get
                {
                    System.Decimal? data = entity.MedicalSupportAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicalSupportAmount = null;
                    else entity.MedicalSupportAmount = Convert.ToDecimal(value);
                }
            }

            public System.String HaemodialiseAmount
            {
                get
                {
                    System.Decimal? data = entity.HaemodialiseAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.HaemodialiseAmount = null;
                    else entity.HaemodialiseAmount = Convert.ToDecimal(value);
                }
            }

            public System.String CtScanAmount
            {
                get
                {
                    System.Decimal? data = entity.CtScanAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CtScanAmount = null;
                    else entity.CtScanAmount = Convert.ToDecimal(value);
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


            private esAskesCovered entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAskesCoveredQuery query)
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
                throw new Exception("esAskesCovered can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class AskesCovered : esAskesCovered
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
    abstract public class esAskesCoveredQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return AskesCoveredMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem RoomAmount
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.RoomAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem RoomDays
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.RoomDays, esSystemType.Int32);
            }
        }

        public esQueryItem IccuAmount
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.IccuAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem IccuDays
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.IccuDays, esSystemType.Int32);
            }
        }

        public esQueryItem HcuAmount
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.HcuAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem HcuDays
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.HcuDays, esSystemType.Int32);
            }
        }

        public esQueryItem SurgeryAmount
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.SurgeryAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem MedicalSupportAmount
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.MedicalSupportAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem HaemodialiseAmount
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.HaemodialiseAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem CtScanAmount
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.CtScanAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AskesCoveredMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AskesCoveredCollection")]
    public partial class AskesCoveredCollection : esAskesCoveredCollection, IEnumerable<AskesCovered>
    {
        public AskesCoveredCollection()
        {

        }

        public static implicit operator List<AskesCovered>(AskesCoveredCollection coll)
        {
            List<AskesCovered> list = new List<AskesCovered>();

            foreach (AskesCovered emp in coll)
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
                return AskesCoveredMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AskesCoveredQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AskesCovered(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AskesCovered();
        }


        #endregion


        [BrowsableAttribute(false)]
        public AskesCoveredQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AskesCoveredQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(AskesCoveredQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public AskesCovered AddNew()
        {
            AskesCovered entity = base.AddNewEntity() as AskesCovered;

            return entity;
        }

        public AskesCovered FindByPrimaryKey(System.String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as AskesCovered;
        }


        #region IEnumerable<AskesCovered> Members

        IEnumerator<AskesCovered> IEnumerable<AskesCovered>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AskesCovered;
            }
        }

        #endregion

        private AskesCoveredQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AskesCovered' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("AskesCovered ({RegistrationNo})")]
    [Serializable]
    public partial class AskesCovered : esAskesCovered
    {
        public AskesCovered()
        {

        }

        public AskesCovered(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AskesCoveredMetadata.Meta();
            }
        }



        override protected esAskesCoveredQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AskesCoveredQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public AskesCoveredQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AskesCoveredQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(AskesCoveredQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AskesCoveredQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class AskesCoveredQuery : esAskesCoveredQuery
    {
        public AskesCoveredQuery()
        {

        }

        public AskesCoveredQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AskesCoveredQuery";
        }


    }


    [Serializable]
    public partial class AskesCoveredMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AskesCoveredMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.RoomAmount, 1, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.RoomAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.RoomDays, 2, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.RoomDays;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.IccuAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.IccuAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.IccuDays, 4, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.IccuDays;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.HcuAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.HcuAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.HcuDays, 6, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.HcuDays;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.SurgeryAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.SurgeryAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.MedicalSupportAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.MedicalSupportAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.HaemodialiseAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.HaemodialiseAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.CtScanAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.CtScanAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AskesCoveredMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = AskesCoveredMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public AskesCoveredMetadata Meta()
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
            public const string RegistrationNo = "RegistrationNo";
            public const string RoomAmount = "RoomAmount";
            public const string RoomDays = "RoomDays";
            public const string IccuAmount = "IccuAmount";
            public const string IccuDays = "IccuDays";
            public const string HcuAmount = "HcuAmount";
            public const string HcuDays = "HcuDays";
            public const string SurgeryAmount = "SurgeryAmount";
            public const string MedicalSupportAmount = "MedicalSupportAmount";
            public const string HaemodialiseAmount = "HaemodialiseAmount";
            public const string CtScanAmount = "CtScanAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string RoomAmount = "RoomAmount";
            public const string RoomDays = "RoomDays";
            public const string IccuAmount = "IccuAmount";
            public const string IccuDays = "IccuDays";
            public const string HcuAmount = "HcuAmount";
            public const string HcuDays = "HcuDays";
            public const string SurgeryAmount = "SurgeryAmount";
            public const string MedicalSupportAmount = "MedicalSupportAmount";
            public const string HaemodialiseAmount = "HaemodialiseAmount";
            public const string CtScanAmount = "CtScanAmount";
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
            lock (typeof(AskesCoveredMetadata))
            {
                if (AskesCoveredMetadata.mapDelegates == null)
                {
                    AskesCoveredMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AskesCoveredMetadata.meta == null)
                {
                    AskesCoveredMetadata.meta = new AskesCoveredMetadata();
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


                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RoomAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RoomDays", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("IccuAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IccuDays", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("HcuAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("HcuDays", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("SurgeryAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("MedicalSupportAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("HaemodialiseAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CtScanAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "AskesCovered";
                meta.Destination = "AskesCovered";

                meta.spInsert = "proc_AskesCoveredInsert";
                meta.spUpdate = "proc_AskesCoveredUpdate";
                meta.spDelete = "proc_AskesCoveredDelete";
                meta.spLoadAll = "proc_AskesCoveredLoadAll";
                meta.spLoadByPrimaryKey = "proc_AskesCoveredLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AskesCoveredMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
