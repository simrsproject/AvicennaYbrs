/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 10:55:09 AM
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
    abstract public class esRlMasterReportItemCollection : esEntityCollectionWAuditLog
    {
        public esRlMasterReportItemCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RlMasterReportItemCollection";
        }

        #region Query Logic
        protected void InitQuery(esRlMasterReportItemQuery query)
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
            this.InitQuery(query as esRlMasterReportItemQuery);
        }
        #endregion

        virtual public RlMasterReportItem DetachEntity(RlMasterReportItem entity)
        {
            return base.DetachEntity(entity) as RlMasterReportItem;
        }

        virtual public RlMasterReportItem AttachEntity(RlMasterReportItem entity)
        {
            return base.AttachEntity(entity) as RlMasterReportItem;
        }

        virtual public void Combine(RlMasterReportItemCollection collection)
        {
            base.Combine(collection);
        }

        new public RlMasterReportItem this[int index]
        {
            get
            {
                return base[index] as RlMasterReportItem;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RlMasterReportItem);
        }
    }



    [Serializable]
    abstract public class esRlMasterReportItem : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRlMasterReportItemQuery GetDynamicQuery()
        {
            return null;
        }

        public esRlMasterReportItem()
        {

        }

        public esRlMasterReportItem(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.Int32 rlMasterReportItemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlMasterReportItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlMasterReportItemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 rlMasterReportItemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(rlMasterReportItemID);
            else
                return LoadByPrimaryKeyStoredProcedure(rlMasterReportItemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.Int32 rlMasterReportItemID)
        {
            esRlMasterReportItemQuery query = this.GetDynamicQuery();
            query.Where(query.RlMasterReportItemID == rlMasterReportItemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.Int32 rlMasterReportItemID)
        {
            esParameters parms = new esParameters();
            parms.Add("RlMasterReportItemID", rlMasterReportItemID);
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
                        case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
                        case "RlMasterReportID": this.str.RlMasterReportID = (string)value; break;
                        case "RlMasterReportItemNo": this.str.RlMasterReportItemNo = (string)value; break;
                        case "RlMasterReportItemCode": this.str.RlMasterReportItemCode = (string)value; break;
                        case "RlMasterReportItemName": this.str.RlMasterReportItemName = (string)value; break;
                        case "SRParamedicRL1": this.str.SRParamedicRL1 = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "ParameterValue": this.str.ParameterValue = (string)value; break;
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

                        case "RlMasterReportID":

                            if (value == null || value is System.Int32)
                                this.RlMasterReportID = (System.Int32?)value;
                            break;

                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to RlMasterReportItem.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemID, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.RlMasterReportID
        /// </summary>
        virtual public System.Int32? RlMasterReportID
        {
            get
            {
                return base.GetSystemInt32(RlMasterReportItemMetadata.ColumnNames.RlMasterReportID);
            }

            set
            {
                base.SetSystemInt32(RlMasterReportItemMetadata.ColumnNames.RlMasterReportID, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.RlMasterReportItemNo
        /// </summary>
        virtual public System.String RlMasterReportItemNo
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemNo);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemNo, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.RlMasterReportItemCode
        /// </summary>
        virtual public System.String RlMasterReportItemCode
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemCode);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemCode, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.RlMasterReportItemName
        /// </summary>
        virtual public System.String RlMasterReportItemName
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemName);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemName, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.SRParamedicRL1
        /// </summary>
        virtual public System.String SRParamedicRL1
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemMetadata.ColumnNames.SRParamedicRL1);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemMetadata.ColumnNames.SRParamedicRL1, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(RlMasterReportItemMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(RlMasterReportItemMetadata.ColumnNames.IsActive, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.ParameterValue
        /// </summary>
        virtual public System.String ParameterValue
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemMetadata.ColumnNames.ParameterValue);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemMetadata.ColumnNames.ParameterValue, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RlMasterReportItemMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RlMasterReportItemMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RlMasterReportItem.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RlMasterReportItemMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RlMasterReportItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esRlMasterReportItem entity)
            {
                this.entity = entity;
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

            public System.String RlMasterReportID
            {
                get
                {
                    System.Int32? data = entity.RlMasterReportID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportID = null;
                    else entity.RlMasterReportID = Convert.ToInt32(value);
                }
            }

            public System.String RlMasterReportItemNo
            {
                get
                {
                    System.String data = entity.RlMasterReportItemNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemNo = null;
                    else entity.RlMasterReportItemNo = Convert.ToString(value);
                }
            }

            public System.String RlMasterReportItemCode
            {
                get
                {
                    System.String data = entity.RlMasterReportItemCode;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemCode = null;
                    else entity.RlMasterReportItemCode = Convert.ToString(value);
                }
            }

            public System.String RlMasterReportItemName
            {
                get
                {
                    System.String data = entity.RlMasterReportItemName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RlMasterReportItemName = null;
                    else entity.RlMasterReportItemName = Convert.ToString(value);
                }
            }

            public System.String SRParamedicRL1
            {
                get
                {
                    System.String data = entity.SRParamedicRL1;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRParamedicRL1 = null;
                    else entity.SRParamedicRL1 = Convert.ToString(value);
                }
            }

            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
                }
            }

            public System.String ParameterValue
            {
                get
                {
                    System.String data = entity.ParameterValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParameterValue = null;
                    else entity.ParameterValue = Convert.ToString(value);
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


            private esRlMasterReportItem entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRlMasterReportItemQuery query)
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
                throw new Exception("esRlMasterReportItem can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class RlMasterReportItem : esRlMasterReportItem
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
    abstract public class esRlMasterReportItemQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RlMasterReportItemMetadata.Meta();
            }
        }


        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

        public esQueryItem RlMasterReportID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.RlMasterReportID, esSystemType.Int32);
            }
        }

        public esQueryItem RlMasterReportItemNo
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemNo, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemCode
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemCode, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemName
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemName, esSystemType.String);
            }
        }

        public esQueryItem SRParamedicRL1
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.SRParamedicRL1, esSystemType.String);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem ParameterValue
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.ParameterValue, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RlMasterReportItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RlMasterReportItemCollection")]
    public partial class RlMasterReportItemCollection : esRlMasterReportItemCollection, IEnumerable<RlMasterReportItem>
    {
        public RlMasterReportItemCollection()
        {

        }

        public static implicit operator List<RlMasterReportItem>(RlMasterReportItemCollection coll)
        {
            List<RlMasterReportItem> list = new List<RlMasterReportItem>();

            foreach (RlMasterReportItem emp in coll)
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
                return RlMasterReportItemMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlMasterReportItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RlMasterReportItem(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RlMasterReportItem();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RlMasterReportItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlMasterReportItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RlMasterReportItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RlMasterReportItem AddNew()
        {
            RlMasterReportItem entity = base.AddNewEntity() as RlMasterReportItem;

            return entity;
        }

        public RlMasterReportItem FindByPrimaryKey(System.Int32 rlMasterReportItemID)
        {
            return base.FindByPrimaryKey(rlMasterReportItemID) as RlMasterReportItem;
        }


        #region IEnumerable<RlMasterReportItem> Members

        IEnumerator<RlMasterReportItem> IEnumerable<RlMasterReportItem>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RlMasterReportItem;
            }
        }

        #endregion

        private RlMasterReportItemQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RlMasterReportItem' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RlMasterReportItem ({RlMasterReportItemID})")]
    [Serializable]
    public partial class RlMasterReportItem : esRlMasterReportItem
    {
        public RlMasterReportItem()
        {

        }

        public RlMasterReportItem(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RlMasterReportItemMetadata.Meta();
            }
        }



        override protected esRlMasterReportItemQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RlMasterReportItemQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RlMasterReportItemQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RlMasterReportItemQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RlMasterReportItemQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RlMasterReportItemQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RlMasterReportItemQuery : esRlMasterReportItemQuery
    {
        public RlMasterReportItemQuery()
        {

        }

        public RlMasterReportItemQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RlMasterReportItemQuery";
        }


    }


    [Serializable]
    public partial class RlMasterReportItemMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RlMasterReportItemMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemID, 0, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.RlMasterReportItemID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.RlMasterReportID, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.RlMasterReportID;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.RlMasterReportItemNo;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemCode, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.RlMasterReportItemCode;
            c.CharacterMaxLength = 50;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.RlMasterReportItemName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.RlMasterReportItemName;
            c.CharacterMaxLength = 300;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.SRParamedicRL1, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.SRParamedicRL1;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.ParameterValue, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.ParameterValue;
            c.CharacterMaxLength = 4000;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(RlMasterReportItemMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = RlMasterReportItemMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            _columns.Add(c);

        }
        #endregion

        static public RlMasterReportItemMetadata Meta()
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
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string RlMasterReportID = "RlMasterReportID";
            public const string RlMasterReportItemNo = "RlMasterReportItemNo";
            public const string RlMasterReportItemCode = "RlMasterReportItemCode";
            public const string RlMasterReportItemName = "RlMasterReportItemName";
            public const string SRParamedicRL1 = "SRParamedicRL1";
            public const string IsActive = "IsActive";
            public const string ParameterValue = "ParameterValue";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RlMasterReportItemID = "RlMasterReportItemID";
            public const string RlMasterReportID = "RlMasterReportID";
            public const string RlMasterReportItemNo = "RlMasterReportItemNo";
            public const string RlMasterReportItemCode = "RlMasterReportItemCode";
            public const string RlMasterReportItemName = "RlMasterReportItemName";
            public const string SRParamedicRL1 = "SRParamedicRL1";
            public const string IsActive = "IsActive";
            public const string ParameterValue = "ParameterValue";
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
            lock (typeof(RlMasterReportItemMetadata))
            {
                if (RlMasterReportItemMetadata.mapDelegates == null)
                {
                    RlMasterReportItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RlMasterReportItemMetadata.meta == null)
                {
                    RlMasterReportItemMetadata.meta = new RlMasterReportItemMetadata();
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


                meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RlMasterReportID", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RlMasterReportItemNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportItemCode", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRParamedicRL1", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ParameterValue", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "RlMasterReportItem";
                meta.Destination = "RlMasterReportItem";

                meta.spInsert = "proc_RlMasterReportItemInsert";
                meta.spUpdate = "proc_RlMasterReportItemUpdate";
                meta.spDelete = "proc_RlMasterReportItemDelete";
                meta.spLoadAll = "proc_RlMasterReportItemLoadAll";
                meta.spLoadByPrimaryKey = "proc_RlMasterReportItemLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RlMasterReportItemMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
