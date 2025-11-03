/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/27/2015 10:31:28 AM
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
    abstract public class esAddMealOrderCollection : esEntityCollectionWAuditLog
    {
        public esAddMealOrderCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "AddMealOrderCollection";
        }

        #region Query Logic
        protected void InitQuery(esAddMealOrderQuery query)
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
            this.InitQuery(query as esAddMealOrderQuery);
        }
        #endregion

        virtual public AddMealOrder DetachEntity(AddMealOrder entity)
        {
            return base.DetachEntity(entity) as AddMealOrder;
        }

        virtual public AddMealOrder AttachEntity(AddMealOrder entity)
        {
            return base.AttachEntity(entity) as AddMealOrder;
        }

        virtual public void Combine(AddMealOrderCollection collection)
        {
            base.Combine(collection);
        }

        new public AddMealOrder this[int index]
        {
            get
            {
                return base[index] as AddMealOrder;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(AddMealOrder);
        }
    }



    [Serializable]
    abstract public class esAddMealOrder : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esAddMealOrderQuery GetDynamicQuery()
        {
            return null;
        }

        public esAddMealOrder()
        {

        }

        public esAddMealOrder(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String orderNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(orderNo);
            else
                return LoadByPrimaryKeyStoredProcedure(orderNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String orderNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(orderNo);
            else
                return LoadByPrimaryKeyStoredProcedure(orderNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String orderNo)
        {
            esAddMealOrderQuery query = this.GetDynamicQuery();
            query.Where(query.OrderNo == orderNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String orderNo)
        {
            esParameters parms = new esParameters();
            parms.Add("OrderNo", orderNo);
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
                        case "OrderNo": this.str.OrderNo = (string)value; break;
                        case "OrderDate": this.str.OrderDate = (string)value; break;
                        case "EffectiveDate": this.str.EffectiveDate = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "MenuID": this.str.MenuID = (string)value; break;
                        case "MenuItemID": this.str.MenuItemID = (string)value; break;
                        case "VersionID": this.str.VersionID = (string)value; break;
                        case "SeqNo": this.str.SeqNo = (string)value; break;
                        case "SRMealSet": this.str.SRMealSet = (string)value; break;
                        case "Qty": this.str.Qty = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "IsApproved": this.str.IsApproved = (string)value; break;
                        case "IsVoid": this.str.IsVoid = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "OrderDate":

                            if (value == null || value is System.DateTime)
                                this.OrderDate = (System.DateTime?)value;
                            break;

                        case "EffectiveDate":

                            if (value == null || value is System.DateTime)
                                this.EffectiveDate = (System.DateTime?)value;
                            break;

                        case "Qty":

                            if (value == null || value is System.Int16)
                                this.Qty = (System.Int16?)value;
                            break;

                        case "IsApproved":

                            if (value == null || value is System.Boolean)
                                this.IsApproved = (System.Boolean?)value;
                            break;

                        case "IsVoid":

                            if (value == null || value is System.Boolean)
                                this.IsVoid = (System.Boolean?)value;
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
        /// Maps to AddMealOrder.OrderNo
        /// </summary>
        virtual public System.String OrderNo
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.OrderNo);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.OrderNo, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.OrderDate
        /// </summary>
        virtual public System.DateTime? OrderDate
        {
            get
            {
                return base.GetSystemDateTime(AddMealOrderMetadata.ColumnNames.OrderDate);
            }

            set
            {
                base.SetSystemDateTime(AddMealOrderMetadata.ColumnNames.OrderDate, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.EffectiveDate
        /// </summary>
        virtual public System.DateTime? EffectiveDate
        {
            get
            {
                return base.GetSystemDateTime(AddMealOrderMetadata.ColumnNames.EffectiveDate);
            }

            set
            {
                base.SetSystemDateTime(AddMealOrderMetadata.ColumnNames.EffectiveDate, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.MenuID
        /// </summary>
        virtual public System.String MenuID
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.MenuID);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.MenuID, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.MenuItemID
        /// </summary>
        virtual public System.String MenuItemID
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.MenuItemID);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.MenuItemID, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.VersionID
        /// </summary>
        virtual public System.String VersionID
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.VersionID);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.VersionID, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.SeqNo
        /// </summary>
        virtual public System.String SeqNo
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.SeqNo);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.SeqNo, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.SRMealSet
        /// </summary>
        virtual public System.String SRMealSet
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.SRMealSet);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.SRMealSet, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.Qty
        /// </summary>
        virtual public System.Int16? Qty
        {
            get
            {
                return base.GetSystemInt16(AddMealOrderMetadata.ColumnNames.Qty);
            }

            set
            {
                base.SetSystemInt16(AddMealOrderMetadata.ColumnNames.Qty, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.IsApproved
        /// </summary>
        virtual public System.Boolean? IsApproved
        {
            get
            {
                return base.GetSystemBoolean(AddMealOrderMetadata.ColumnNames.IsApproved);
            }

            set
            {
                base.SetSystemBoolean(AddMealOrderMetadata.ColumnNames.IsApproved, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.IsVoid
        /// </summary>
        virtual public System.Boolean? IsVoid
        {
            get
            {
                return base.GetSystemBoolean(AddMealOrderMetadata.ColumnNames.IsVoid);
            }

            set
            {
                base.SetSystemBoolean(AddMealOrderMetadata.ColumnNames.IsVoid, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(AddMealOrderMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(AddMealOrderMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to AddMealOrder.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(AddMealOrderMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(AddMealOrderMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esAddMealOrder entity)
            {
                this.entity = entity;
            }


            public System.String OrderNo
            {
                get
                {
                    System.String data = entity.OrderNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderNo = null;
                    else entity.OrderNo = Convert.ToString(value);
                }
            }

            public System.String OrderDate
            {
                get
                {
                    System.DateTime? data = entity.OrderDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderDate = null;
                    else entity.OrderDate = Convert.ToDateTime(value);
                }
            }

            public System.String EffectiveDate
            {
                get
                {
                    System.DateTime? data = entity.EffectiveDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.EffectiveDate = null;
                    else entity.EffectiveDate = Convert.ToDateTime(value);
                }
            }

            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }

            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
                }
            }

            public System.String MenuID
            {
                get
                {
                    System.String data = entity.MenuID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MenuID = null;
                    else entity.MenuID = Convert.ToString(value);
                }
            }

            public System.String MenuItemID
            {
                get
                {
                    System.String data = entity.MenuItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MenuItemID = null;
                    else entity.MenuItemID = Convert.ToString(value);
                }
            }

            public System.String VersionID
            {
                get
                {
                    System.String data = entity.VersionID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VersionID = null;
                    else entity.VersionID = Convert.ToString(value);
                }
            }

            public System.String SeqNo
            {
                get
                {
                    System.String data = entity.SeqNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SeqNo = null;
                    else entity.SeqNo = Convert.ToString(value);
                }
            }

            public System.String SRMealSet
            {
                get
                {
                    System.String data = entity.SRMealSet;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMealSet = null;
                    else entity.SRMealSet = Convert.ToString(value);
                }
            }

            public System.String Qty
            {
                get
                {
                    System.Int16? data = entity.Qty;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Qty = null;
                    else entity.Qty = Convert.ToInt16(value);
                }
            }

            public System.String Notes
            {
                get
                {
                    System.String data = entity.Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Notes = null;
                    else entity.Notes = Convert.ToString(value);
                }
            }

            public System.String IsApproved
            {
                get
                {
                    System.Boolean? data = entity.IsApproved;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsApproved = null;
                    else entity.IsApproved = Convert.ToBoolean(value);
                }
            }

            public System.String IsVoid
            {
                get
                {
                    System.Boolean? data = entity.IsVoid;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsVoid = null;
                    else entity.IsVoid = Convert.ToBoolean(value);
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


            private esAddMealOrder entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esAddMealOrderQuery query)
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
                throw new Exception("esAddMealOrder can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class AddMealOrder : esAddMealOrder
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
    abstract public class esAddMealOrderQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return AddMealOrderMetadata.Meta();
            }
        }


        public esQueryItem OrderNo
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.OrderNo, esSystemType.String);
            }
        }

        public esQueryItem OrderDate
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.OrderDate, esSystemType.DateTime);
            }
        }

        public esQueryItem EffectiveDate
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.EffectiveDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem MenuID
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.MenuID, esSystemType.String);
            }
        }

        public esQueryItem MenuItemID
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.MenuItemID, esSystemType.String);
            }
        }

        public esQueryItem VersionID
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.VersionID, esSystemType.String);
            }
        }

        public esQueryItem SeqNo
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.SeqNo, esSystemType.String);
            }
        }

        public esQueryItem SRMealSet
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.SRMealSet, esSystemType.String);
            }
        }

        public esQueryItem Qty
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.Qty, esSystemType.Int16);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem IsApproved
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
            }
        }

        public esQueryItem IsVoid
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, AddMealOrderMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("AddMealOrderCollection")]
    public partial class AddMealOrderCollection : esAddMealOrderCollection, IEnumerable<AddMealOrder>
    {
        public AddMealOrderCollection()
        {

        }

        public static implicit operator List<AddMealOrder>(AddMealOrderCollection coll)
        {
            List<AddMealOrder> list = new List<AddMealOrder>();

            foreach (AddMealOrder emp in coll)
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
                return AddMealOrderMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AddMealOrderQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new AddMealOrder(row);
        }

        override protected esEntity CreateEntity()
        {
            return new AddMealOrder();
        }


        #endregion


        [BrowsableAttribute(false)]
        public AddMealOrderQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AddMealOrderQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(AddMealOrderQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public AddMealOrder AddNew()
        {
            AddMealOrder entity = base.AddNewEntity() as AddMealOrder;

            return entity;
        }

        public AddMealOrder FindByPrimaryKey(System.String orderNo)
        {
            return base.FindByPrimaryKey(orderNo) as AddMealOrder;
        }


        #region IEnumerable<AddMealOrder> Members

        IEnumerator<AddMealOrder> IEnumerable<AddMealOrder>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as AddMealOrder;
            }
        }

        #endregion

        private AddMealOrderQuery query;
    }


    /// <summary>
    /// Encapsulates the 'AddMealOrder' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("AddMealOrder ({OrderNo})")]
    [Serializable]
    public partial class AddMealOrder : esAddMealOrder
    {
        public AddMealOrder()
        {

        }

        public AddMealOrder(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return AddMealOrderMetadata.Meta();
            }
        }



        override protected esAddMealOrderQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new AddMealOrderQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public AddMealOrderQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new AddMealOrderQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(AddMealOrderQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private AddMealOrderQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class AddMealOrderQuery : esAddMealOrderQuery
    {
        public AddMealOrderQuery()
        {

        }

        public AddMealOrderQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "AddMealOrderQuery";
        }


    }


    [Serializable]
    public partial class AddMealOrderMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected AddMealOrderMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.OrderNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.OrderNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.OrderDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.OrderDate;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.EffectiveDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.EffectiveDate;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.ClassID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.ClassID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.MenuID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.MenuID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.MenuItemID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.MenuItemID;
            c.CharacterMaxLength = 50;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.VersionID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.VersionID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.SeqNo, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.SeqNo;
            c.CharacterMaxLength = 3;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.SRMealSet, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.SRMealSet;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.Qty, 10, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.Qty;
            c.NumericPrecision = 5;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.Notes, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 500;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.IsApproved, 12, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.IsApproved;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.IsVoid, 13, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.IsVoid;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(AddMealOrderMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = AddMealOrderMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public AddMealOrderMetadata Meta()
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
            public const string OrderNo = "OrderNo";
            public const string OrderDate = "OrderDate";
            public const string EffectiveDate = "EffectiveDate";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string MenuID = "MenuID";
            public const string MenuItemID = "MenuItemID";
            public const string VersionID = "VersionID";
            public const string SeqNo = "SeqNo";
            public const string SRMealSet = "SRMealSet";
            public const string Qty = "Qty";
            public const string Notes = "Notes";
            public const string IsApproved = "IsApproved";
            public const string IsVoid = "IsVoid";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OrderNo = "OrderNo";
            public const string OrderDate = "OrderDate";
            public const string EffectiveDate = "EffectiveDate";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ClassID = "ClassID";
            public const string MenuID = "MenuID";
            public const string MenuItemID = "MenuItemID";
            public const string VersionID = "VersionID";
            public const string SeqNo = "SeqNo";
            public const string SRMealSet = "SRMealSet";
            public const string Qty = "Qty";
            public const string Notes = "Notes";
            public const string IsApproved = "IsApproved";
            public const string IsVoid = "IsVoid";
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
            lock (typeof(AddMealOrderMetadata))
            {
                if (AddMealOrderMetadata.mapDelegates == null)
                {
                    AddMealOrderMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (AddMealOrderMetadata.meta == null)
                {
                    AddMealOrderMetadata.meta = new AddMealOrderMetadata();
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


                meta.AddTypeMap("OrderNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("EffectiveDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MenuID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MenuItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("VersionID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMealSet", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Qty", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "AddMealOrder";
                meta.Destination = "AddMealOrder";

                meta.spInsert = "proc_AddMealOrderInsert";
                meta.spUpdate = "proc_AddMealOrderUpdate";
                meta.spDelete = "proc_AddMealOrderDelete";
                meta.spLoadAll = "proc_AddMealOrderLoadAll";
                meta.spLoadByPrimaryKey = "proc_AddMealOrderLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private AddMealOrderMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
