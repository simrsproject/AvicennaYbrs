/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/28/2014 10:51:10 AM
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
    abstract public class esItemDiagnosticCollection : esEntityCollectionWAuditLog
    {
        public esItemDiagnosticCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ItemDiagnosticCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemDiagnosticQuery query)
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
            this.InitQuery(query as esItemDiagnosticQuery);
        }
        #endregion

        virtual public ItemDiagnostic DetachEntity(ItemDiagnostic entity)
        {
            return base.DetachEntity(entity) as ItemDiagnostic;
        }

        virtual public ItemDiagnostic AttachEntity(ItemDiagnostic entity)
        {
            return base.AttachEntity(entity) as ItemDiagnostic;
        }

        virtual public void Combine(ItemDiagnosticCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemDiagnostic this[int index]
        {
            get
            {
                return base[index] as ItemDiagnostic;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemDiagnostic);
        }
    }



    [Serializable]
    abstract public class esItemDiagnostic : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemDiagnosticQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemDiagnostic()
        {

        }

        public esItemDiagnostic(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String itemID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(itemID);
            else
                return LoadByPrimaryKeyStoredProcedure(itemID);
        }

        private bool LoadByPrimaryKeyDynamic(System.String itemID)
        {
            esItemDiagnosticQuery query = this.GetDynamicQuery();
            query.Where(query.ItemID == itemID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String itemID)
        {
            esParameters parms = new esParameters();
            parms.Add("ItemID", itemID);
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
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ReportRLID": this.str.ReportRLID = (string)value; break;
                        case "IsAdminCalculation": this.str.IsAdminCalculation = (string)value; break;
                        case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;
                        case "IsAllowCito": this.str.IsAllowCito = (string)value; break;
                        case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;
                        case "IsPrintWithDoctorName": this.str.IsPrintWithDoctorName = (string)value; break;
                        case "IsAssetUtilization": this.str.IsAssetUtilization = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "IsAdminCalculation":

                            if (value == null || value is System.Boolean)
                                this.IsAdminCalculation = (System.Boolean?)value;
                            break;

                        case "IsAllowVariable":

                            if (value == null || value is System.Boolean)
                                this.IsAllowVariable = (System.Boolean?)value;
                            break;

                        case "IsAllowCito":

                            if (value == null || value is System.Boolean)
                                this.IsAllowCito = (System.Boolean?)value;
                            break;

                        case "IsAllowDiscount":

                            if (value == null || value is System.Boolean)
                                this.IsAllowDiscount = (System.Boolean?)value;
                            break;

                        case "IsPrintWithDoctorName":

                            if (value == null || value is System.Boolean)
                                this.IsPrintWithDoctorName = (System.Boolean?)value;
                            break;

                        case "IsAssetUtilization":

                            if (value == null || value is System.Boolean)
                                this.IsAssetUtilization = (System.Boolean?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "RlMasterReportItemID":

                            if (value == null || value is System.Int32)
                                this.RlMasterReportItemID = (System.Int32?)value;
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
        /// Maps to ItemDiagnostic.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemDiagnosticMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemDiagnosticMetadata.ColumnNames.ItemID, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.ReportRLID
        /// </summary>
        virtual public System.String ReportRLID
        {
            get
            {
                return base.GetSystemString(ItemDiagnosticMetadata.ColumnNames.ReportRLID);
            }

            set
            {
                base.SetSystemString(ItemDiagnosticMetadata.ColumnNames.ReportRLID, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.IsAdminCalculation
        /// </summary>
        virtual public System.Boolean? IsAdminCalculation
        {
            get
            {
                return base.GetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAdminCalculation);
            }

            set
            {
                base.SetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAdminCalculation, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.IsAllowVariable
        /// </summary>
        virtual public System.Boolean? IsAllowVariable
        {
            get
            {
                return base.GetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAllowVariable);
            }

            set
            {
                base.SetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAllowVariable, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.IsAllowCito
        /// </summary>
        virtual public System.Boolean? IsAllowCito
        {
            get
            {
                return base.GetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAllowCito);
            }

            set
            {
                base.SetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAllowCito, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.IsAllowDiscount
        /// </summary>
        virtual public System.Boolean? IsAllowDiscount
        {
            get
            {
                return base.GetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAllowDiscount);
            }

            set
            {
                base.SetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAllowDiscount, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.IsPrintWithDoctorName
        /// </summary>
        virtual public System.Boolean? IsPrintWithDoctorName
        {
            get
            {
                return base.GetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsPrintWithDoctorName);
            }

            set
            {
                base.SetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsPrintWithDoctorName, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.IsAssetUtilization
        /// </summary>
        virtual public System.Boolean? IsAssetUtilization
        {
            get
            {
                return base.GetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAssetUtilization);
            }

            set
            {
                base.SetSystemBoolean(ItemDiagnosticMetadata.ColumnNames.IsAssetUtilization, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemDiagnosticMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemDiagnosticMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemDiagnosticMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemDiagnosticMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to ItemDiagnostic.RlMasterReportItemID
        /// </summary>
        virtual public System.Int32? RlMasterReportItemID
        {
            get
            {
                return base.GetSystemInt32(ItemDiagnosticMetadata.ColumnNames.RlMasterReportItemID);
            }

            set
            {
                base.SetSystemInt32(ItemDiagnosticMetadata.ColumnNames.RlMasterReportItemID, value);
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
            public esStrings(esItemDiagnostic entity)
            {
                this.entity = entity;
            }


            public System.String ItemID
            {
                get
                {
                    System.String data = entity.ItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemID = null;
                    else entity.ItemID = Convert.ToString(value);
                }
            }

            public System.String ReportRLID
            {
                get
                {
                    System.String data = entity.ReportRLID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReportRLID = null;
                    else entity.ReportRLID = Convert.ToString(value);
                }
            }

            public System.String IsAdminCalculation
            {
                get
                {
                    System.Boolean? data = entity.IsAdminCalculation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAdminCalculation = null;
                    else entity.IsAdminCalculation = Convert.ToBoolean(value);
                }
            }

            public System.String IsAllowVariable
            {
                get
                {
                    System.Boolean? data = entity.IsAllowVariable;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllowVariable = null;
                    else entity.IsAllowVariable = Convert.ToBoolean(value);
                }
            }

            public System.String IsAllowCito
            {
                get
                {
                    System.Boolean? data = entity.IsAllowCito;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllowCito = null;
                    else entity.IsAllowCito = Convert.ToBoolean(value);
                }
            }

            public System.String IsAllowDiscount
            {
                get
                {
                    System.Boolean? data = entity.IsAllowDiscount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAllowDiscount = null;
                    else entity.IsAllowDiscount = Convert.ToBoolean(value);
                }
            }

            public System.String IsPrintWithDoctorName
            {
                get
                {
                    System.Boolean? data = entity.IsPrintWithDoctorName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPrintWithDoctorName = null;
                    else entity.IsPrintWithDoctorName = Convert.ToBoolean(value);
                }
            }

            public System.String IsAssetUtilization
            {
                get
                {
                    System.Boolean? data = entity.IsAssetUtilization;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsAssetUtilization = null;
                    else entity.IsAssetUtilization = Convert.ToBoolean(value);
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


            private esItemDiagnostic entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemDiagnosticQuery query)
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
                throw new Exception("esItemDiagnostic can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ItemDiagnostic : esItemDiagnostic
    {

        #region UpToItem - One To One
        /// <summary>
        /// One to One
        /// Foreign Key Name - RefItemToItemDiagnosticDt
        /// </summary>

        [XmlIgnore]
        public Item UpToItem
        {
            get
            {
                if (this._UpToItem == null
                    && ItemID != null)
                {
                    this._UpToItem = new Item();
                    this._UpToItem.es.Connection.Name = this.es.Connection.Name;
                    this.SetPreSave("UpToItem", this._UpToItem);
                    this._UpToItem.Query.Where(this._UpToItem.Query.ItemID == this.ItemID);
                    this._UpToItem.Query.Load();
                }

                return this._UpToItem;
            }

            set
            {
                this.RemovePreSave("UpToItem");

                if (value == null)
                {
                    this._UpToItem = null;
                }
                else
                {
                    this._UpToItem = value;
                    this.SetPreSave("UpToItem", this._UpToItem);
                }


            }
        }

        private Item _UpToItem;
        #endregion


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
    abstract public class esItemDiagnosticQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ItemDiagnosticMetadata.Meta();
            }
        }


        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ReportRLID
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.ReportRLID, esSystemType.String);
            }
        }

        public esQueryItem IsAdminCalculation
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowVariable
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowCito
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.IsAllowCito, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowDiscount
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
            }
        }

        public esQueryItem IsPrintWithDoctorName
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.IsPrintWithDoctorName, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAssetUtilization
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.IsAssetUtilization, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem RlMasterReportItemID
        {
            get
            {
                return new esQueryItem(this, ItemDiagnosticMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemDiagnosticCollection")]
    public partial class ItemDiagnosticCollection : esItemDiagnosticCollection, IEnumerable<ItemDiagnostic>
    {
        public ItemDiagnosticCollection()
        {

        }

        public static implicit operator List<ItemDiagnostic>(ItemDiagnosticCollection coll)
        {
            List<ItemDiagnostic> list = new List<ItemDiagnostic>();

            foreach (ItemDiagnostic emp in coll)
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
                return ItemDiagnosticMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemDiagnosticQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemDiagnostic(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemDiagnostic();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ItemDiagnosticQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemDiagnosticQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ItemDiagnosticQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ItemDiagnostic AddNew()
        {
            ItemDiagnostic entity = base.AddNewEntity() as ItemDiagnostic;

            return entity;
        }

        public ItemDiagnostic FindByPrimaryKey(System.String itemID)
        {
            return base.FindByPrimaryKey(itemID) as ItemDiagnostic;
        }


        #region IEnumerable<ItemDiagnostic> Members

        IEnumerator<ItemDiagnostic> IEnumerable<ItemDiagnostic>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemDiagnostic;
            }
        }

        #endregion

        private ItemDiagnosticQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemDiagnostic' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemDiagnostic ({ItemID})")]
    [Serializable]
    public partial class ItemDiagnostic : esItemDiagnostic
    {
        public ItemDiagnostic()
        {

        }

        public ItemDiagnostic(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemDiagnosticMetadata.Meta();
            }
        }



        override protected esItemDiagnosticQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemDiagnosticQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ItemDiagnosticQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemDiagnosticQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ItemDiagnosticQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemDiagnosticQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ItemDiagnosticQuery : esItemDiagnosticQuery
    {
        public ItemDiagnosticQuery()
        {

        }

        public ItemDiagnosticQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemDiagnosticQuery";
        }


    }


    [Serializable]
    public partial class ItemDiagnosticMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemDiagnosticMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.ReportRLID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.ReportRLID;
            c.CharacterMaxLength = 10;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.IsAdminCalculation, 2, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.IsAdminCalculation;
            c.HasDefault = true;
            c.Default = @"((1))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.IsAllowVariable, 3, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.IsAllowVariable;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.IsAllowCito, 4, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.IsAllowCito;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.IsAllowDiscount, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.IsAllowDiscount;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.IsPrintWithDoctorName, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.IsPrintWithDoctorName;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.IsAssetUtilization, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.IsAssetUtilization;
            c.HasDefault = true;
            c.Default = @"((0))";
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemDiagnosticMetadata.ColumnNames.RlMasterReportItemID, 10, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = ItemDiagnosticMetadata.PropertyNames.RlMasterReportItemID;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ItemDiagnosticMetadata Meta()
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
            public const string ItemID = "ItemID";
            public const string ReportRLID = "ReportRLID";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string IsAllowVariable = "IsAllowVariable";
            public const string IsAllowCito = "IsAllowCito";
            public const string IsAllowDiscount = "IsAllowDiscount";
            public const string IsPrintWithDoctorName = "IsPrintWithDoctorName";
            public const string IsAssetUtilization = "IsAssetUtilization";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ItemID = "ItemID";
            public const string ReportRLID = "ReportRLID";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string IsAllowVariable = "IsAllowVariable";
            public const string IsAllowCito = "IsAllowCito";
            public const string IsAllowDiscount = "IsAllowDiscount";
            public const string IsPrintWithDoctorName = "IsPrintWithDoctorName";
            public const string IsAssetUtilization = "IsAssetUtilization";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string RlMasterReportItemID = "RlMasterReportItemID";
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
            lock (typeof(ItemDiagnosticMetadata))
            {
                if (ItemDiagnosticMetadata.mapDelegates == null)
                {
                    ItemDiagnosticMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemDiagnosticMetadata.meta == null)
                {
                    ItemDiagnosticMetadata.meta = new ItemDiagnosticMetadata();
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


                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReportRLID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsAdminCalculation", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowCito", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsPrintWithDoctorName", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAssetUtilization", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));



                meta.Source = "ItemDiagnostic";
                meta.Destination = "ItemDiagnostic";

                meta.spInsert = "proc_ItemDiagnosticInsert";
                meta.spUpdate = "proc_ItemDiagnosticUpdate";
                meta.spDelete = "proc_ItemDiagnosticDelete";
                meta.spLoadAll = "proc_ItemDiagnosticLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemDiagnosticLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemDiagnosticMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
