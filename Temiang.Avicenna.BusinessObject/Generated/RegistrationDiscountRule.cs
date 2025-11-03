/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 8/3/2015 11:03:35 AM
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
    abstract public class esRegistrationDiscountRuleCollection : esEntityCollection
    {
        public esRegistrationDiscountRuleCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "RegistrationDiscountRuleCollection";
        }

        #region Query Logic
        protected void InitQuery(esRegistrationDiscountRuleQuery query)
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
            this.InitQuery(query as esRegistrationDiscountRuleQuery);
        }
        #endregion

        virtual public RegistrationDiscountRule DetachEntity(RegistrationDiscountRule entity)
        {
            return base.DetachEntity(entity) as RegistrationDiscountRule;
        }

        virtual public RegistrationDiscountRule AttachEntity(RegistrationDiscountRule entity)
        {
            return base.AttachEntity(entity) as RegistrationDiscountRule;
        }

        virtual public void Combine(RegistrationDiscountRuleCollection collection)
        {
            base.Combine(collection);
        }

        new public RegistrationDiscountRule this[int index]
        {
            get
            {
                return base[index] as RegistrationDiscountRule;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(RegistrationDiscountRule);
        }
    }



    [Serializable]
    abstract public class esRegistrationDiscountRule : esEntity
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esRegistrationDiscountRuleQuery GetDynamicQuery()
        {
            return null;
        }

        public esRegistrationDiscountRule()
        {

        }

        public esRegistrationDiscountRule(DataRow row)
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
            esRegistrationDiscountRuleQuery query = this.GetDynamicQuery();
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
                        case "RoomPercentage": this.str.RoomPercentage = (string)value; break;
                        case "RsPercentage": this.str.RsPercentage = (string)value; break;
                        case "DrPercentage": this.str.DrPercentage = (string)value; break;
                        case "BhpPercentage": this.str.BhpPercentage = (string)value; break;
                        case "ResepPercentage": this.str.ResepPercentage = (string)value; break;
                        case "IsDiscountGlobal": this.str.IsDiscountGlobal = (string)value; break;
                        case "IsDiscountGlobalInPercent": this.str.IsDiscountGlobalInPercent = (string)value; break;
                        case "DiscountGlobalAmount": this.str.DiscountGlobalAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "ItemMedicalPercentage": this.str.ItemMedicalPercentage = (string)value; break;
                        case "ItemNonMedicalPercentage": this.str.ItemNonMedicalPercentage = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RoomPercentage":

                            if (value == null || value is System.Decimal)
                                this.RoomPercentage = (System.Decimal?)value;
                            break;

                        case "RsPercentage":

                            if (value == null || value is System.Decimal)
                                this.RsPercentage = (System.Decimal?)value;
                            break;

                        case "DrPercentage":

                            if (value == null || value is System.Decimal)
                                this.DrPercentage = (System.Decimal?)value;
                            break;

                        case "BhpPercentage":

                            if (value == null || value is System.Decimal)
                                this.BhpPercentage = (System.Decimal?)value;
                            break;

                        case "ResepPercentage":

                            if (value == null || value is System.Decimal)
                                this.ResepPercentage = (System.Decimal?)value;
                            break;

                        case "IsDiscountGlobal":

                            if (value == null || value is System.Boolean)
                                this.IsDiscountGlobal = (System.Boolean?)value;
                            break;

                        case "IsDiscountGlobalInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsDiscountGlobalInPercent = (System.Boolean?)value;
                            break;

                        case "DiscountGlobalAmount":

                            if (value == null || value is System.Decimal)
                                this.DiscountGlobalAmount = (System.Decimal?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "ItemMedicalPercentage":

                            if (value == null || value is System.Decimal)
                                this.ItemMedicalPercentage = (System.Decimal?)value;
                            break;

                        case "ItemNonMedicalPercentage":

                            if (value == null || value is System.Decimal)
                                this.ItemNonMedicalPercentage = (System.Decimal?)value;
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
        /// Maps to RegistrationDiscountRule.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(RegistrationDiscountRuleMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(RegistrationDiscountRuleMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.RoomPercentage
        /// </summary>
        virtual public System.Decimal? RoomPercentage
        {
            get
            {
                return base.GetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.RoomPercentage);
            }

            set
            {
                base.SetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.RoomPercentage, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.RsPercentage
        /// </summary>
        virtual public System.Decimal? RsPercentage
        {
            get
            {
                return base.GetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.RsPercentage);
            }

            set
            {
                base.SetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.RsPercentage, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.DrPercentage
        /// </summary>
        virtual public System.Decimal? DrPercentage
        {
            get
            {
                return base.GetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.DrPercentage);
            }

            set
            {
                base.SetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.DrPercentage, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.BhpPercentage
        /// </summary>
        virtual public System.Decimal? BhpPercentage
        {
            get
            {
                return base.GetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.BhpPercentage);
            }

            set
            {
                base.SetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.BhpPercentage, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.ResepPercentage
        /// </summary>
        virtual public System.Decimal? ResepPercentage
        {
            get
            {
                return base.GetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.ResepPercentage);
            }

            set
            {
                base.SetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.ResepPercentage, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.IsDiscountGlobal
        /// </summary>
        virtual public System.Boolean? IsDiscountGlobal
        {
            get
            {
                return base.GetSystemBoolean(RegistrationDiscountRuleMetadata.ColumnNames.IsDiscountGlobal);
            }

            set
            {
                base.SetSystemBoolean(RegistrationDiscountRuleMetadata.ColumnNames.IsDiscountGlobal, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.IsDiscountGlobalInPercent
        /// </summary>
        virtual public System.Boolean? IsDiscountGlobalInPercent
        {
            get
            {
                return base.GetSystemBoolean(RegistrationDiscountRuleMetadata.ColumnNames.IsDiscountGlobalInPercent);
            }

            set
            {
                base.SetSystemBoolean(RegistrationDiscountRuleMetadata.ColumnNames.IsDiscountGlobalInPercent, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.DiscountGlobalAmount
        /// </summary>
        virtual public System.Decimal? DiscountGlobalAmount
        {
            get
            {
                return base.GetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.DiscountGlobalAmount);
            }

            set
            {
                base.SetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.DiscountGlobalAmount, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(RegistrationDiscountRuleMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(RegistrationDiscountRuleMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(RegistrationDiscountRuleMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(RegistrationDiscountRuleMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.ItemMedicalPercentage
        /// </summary>
        virtual public System.Decimal? ItemMedicalPercentage
        {
            get
            {
                return base.GetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.ItemMedicalPercentage);
            }

            set
            {
                base.SetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.ItemMedicalPercentage, value);
            }
        }

        /// <summary>
        /// Maps to RegistrationDiscountRule.ItemNonMedicalPercentage
        /// </summary>
        virtual public System.Decimal? ItemNonMedicalPercentage
        {
            get
            {
                return base.GetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.ItemNonMedicalPercentage);
            }

            set
            {
                base.SetSystemDecimal(RegistrationDiscountRuleMetadata.ColumnNames.ItemNonMedicalPercentage, value);
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
            public esStrings(esRegistrationDiscountRule entity)
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

            public System.String RoomPercentage
            {
                get
                {
                    System.Decimal? data = entity.RoomPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RoomPercentage = null;
                    else entity.RoomPercentage = Convert.ToDecimal(value);
                }
            }

            public System.String RsPercentage
            {
                get
                {
                    System.Decimal? data = entity.RsPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RsPercentage = null;
                    else entity.RsPercentage = Convert.ToDecimal(value);
                }
            }

            public System.String DrPercentage
            {
                get
                {
                    System.Decimal? data = entity.DrPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DrPercentage = null;
                    else entity.DrPercentage = Convert.ToDecimal(value);
                }
            }

            public System.String BhpPercentage
            {
                get
                {
                    System.Decimal? data = entity.BhpPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.BhpPercentage = null;
                    else entity.BhpPercentage = Convert.ToDecimal(value);
                }
            }

            public System.String ResepPercentage
            {
                get
                {
                    System.Decimal? data = entity.ResepPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ResepPercentage = null;
                    else entity.ResepPercentage = Convert.ToDecimal(value);
                }
            }

            public System.String IsDiscountGlobal
            {
                get
                {
                    System.Boolean? data = entity.IsDiscountGlobal;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDiscountGlobal = null;
                    else entity.IsDiscountGlobal = Convert.ToBoolean(value);
                }
            }

            public System.String IsDiscountGlobalInPercent
            {
                get
                {
                    System.Boolean? data = entity.IsDiscountGlobalInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDiscountGlobalInPercent = null;
                    else entity.IsDiscountGlobalInPercent = Convert.ToBoolean(value);
                }
            }

            public System.String DiscountGlobalAmount
            {
                get
                {
                    System.Decimal? data = entity.DiscountGlobalAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscountGlobalAmount = null;
                    else entity.DiscountGlobalAmount = Convert.ToDecimal(value);
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

            public System.String ItemMedicalPercentage
            {
                get
                {
                    System.Decimal? data = entity.ItemMedicalPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemMedicalPercentage = null;
                    else entity.ItemMedicalPercentage = Convert.ToDecimal(value);
                }
            }

            public System.String ItemNonMedicalPercentage
            {
                get
                {
                    System.Decimal? data = entity.ItemNonMedicalPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ItemNonMedicalPercentage = null;
                    else entity.ItemNonMedicalPercentage = Convert.ToDecimal(value);
                }
            }


            private esRegistrationDiscountRule entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esRegistrationDiscountRuleQuery query)
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
                throw new Exception("esRegistrationDiscountRule can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esRegistrationDiscountRuleQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationDiscountRuleMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem RoomPercentage
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.RoomPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem RsPercentage
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.RsPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem DrPercentage
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.DrPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem BhpPercentage
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.BhpPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem ResepPercentage
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.ResepPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem IsDiscountGlobal
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.IsDiscountGlobal, esSystemType.Boolean);
            }
        }

        public esQueryItem IsDiscountGlobalInPercent
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.IsDiscountGlobalInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem DiscountGlobalAmount
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.DiscountGlobalAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem ItemMedicalPercentage
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.ItemMedicalPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem ItemNonMedicalPercentage
        {
            get
            {
                return new esQueryItem(this, RegistrationDiscountRuleMetadata.ColumnNames.ItemNonMedicalPercentage, esSystemType.Decimal);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("RegistrationDiscountRuleCollection")]
    public partial class RegistrationDiscountRuleCollection : esRegistrationDiscountRuleCollection, IEnumerable<RegistrationDiscountRule>
    {
        public RegistrationDiscountRuleCollection()
        {

        }

        public static implicit operator List<RegistrationDiscountRule>(RegistrationDiscountRuleCollection coll)
        {
            List<RegistrationDiscountRule> list = new List<RegistrationDiscountRule>();

            foreach (RegistrationDiscountRule emp in coll)
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
                return RegistrationDiscountRuleMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationDiscountRuleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new RegistrationDiscountRule(row);
        }

        override protected esEntity CreateEntity()
        {
            return new RegistrationDiscountRule();
        }


        #endregion


        [BrowsableAttribute(false)]
        public RegistrationDiscountRuleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationDiscountRuleQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(RegistrationDiscountRuleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public RegistrationDiscountRule AddNew()
        {
            RegistrationDiscountRule entity = base.AddNewEntity() as RegistrationDiscountRule;

            return entity;
        }

        public RegistrationDiscountRule FindByPrimaryKey(System.String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as RegistrationDiscountRule;
        }


        #region IEnumerable<RegistrationDiscountRule> Members

        IEnumerator<RegistrationDiscountRule> IEnumerable<RegistrationDiscountRule>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as RegistrationDiscountRule;
            }
        }

        #endregion

        private RegistrationDiscountRuleQuery query;
    }


    /// <summary>
    /// Encapsulates the 'RegistrationDiscountRule' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationDiscountRule ({RegistrationNo})")]
    [Serializable]
    public partial class RegistrationDiscountRule : esRegistrationDiscountRule
    {
        public RegistrationDiscountRule()
        {

        }

        public RegistrationDiscountRule(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return RegistrationDiscountRuleMetadata.Meta();
            }
        }



        override protected esRegistrationDiscountRuleQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new RegistrationDiscountRuleQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public RegistrationDiscountRuleQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new RegistrationDiscountRuleQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(RegistrationDiscountRuleQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private RegistrationDiscountRuleQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class RegistrationDiscountRuleQuery : esRegistrationDiscountRuleQuery
    {
        public RegistrationDiscountRuleQuery()
        {

        }

        public RegistrationDiscountRuleQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "RegistrationDiscountRuleQuery";
        }


    }


    [Serializable]
    public partial class RegistrationDiscountRuleMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected RegistrationDiscountRuleMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.RoomPercentage, 1, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.RoomPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.RsPercentage, 2, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.RsPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.DrPercentage, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.DrPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.BhpPercentage, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.BhpPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.ResepPercentage, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.ResepPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.IsDiscountGlobal, 6, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.IsDiscountGlobal;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.IsDiscountGlobalInPercent, 7, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.IsDiscountGlobalInPercent;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.DiscountGlobalAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.DiscountGlobalAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.ItemMedicalPercentage, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.ItemMedicalPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(RegistrationDiscountRuleMetadata.ColumnNames.ItemNonMedicalPercentage, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = RegistrationDiscountRuleMetadata.PropertyNames.ItemNonMedicalPercentage;
            c.NumericPrecision = 6;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public RegistrationDiscountRuleMetadata Meta()
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
            public const string RoomPercentage = "RoomPercentage";
            public const string RsPercentage = "RsPercentage";
            public const string DrPercentage = "DrPercentage";
            public const string BhpPercentage = "BhpPercentage";
            public const string ResepPercentage = "ResepPercentage";
            public const string IsDiscountGlobal = "IsDiscountGlobal";
            public const string IsDiscountGlobalInPercent = "IsDiscountGlobalInPercent";
            public const string DiscountGlobalAmount = "DiscountGlobalAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ItemMedicalPercentage = "ItemMedicalPercentage";
            public const string ItemNonMedicalPercentage = "ItemNonMedicalPercentage";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string RoomPercentage = "RoomPercentage";
            public const string RsPercentage = "RsPercentage";
            public const string DrPercentage = "DrPercentage";
            public const string BhpPercentage = "BhpPercentage";
            public const string ResepPercentage = "ResepPercentage";
            public const string IsDiscountGlobal = "IsDiscountGlobal";
            public const string IsDiscountGlobalInPercent = "IsDiscountGlobalInPercent";
            public const string DiscountGlobalAmount = "DiscountGlobalAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string ItemMedicalPercentage = "ItemMedicalPercentage";
            public const string ItemNonMedicalPercentage = "ItemNonMedicalPercentage";
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
            lock (typeof(RegistrationDiscountRuleMetadata))
            {
                if (RegistrationDiscountRuleMetadata.mapDelegates == null)
                {
                    RegistrationDiscountRuleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (RegistrationDiscountRuleMetadata.meta == null)
                {
                    RegistrationDiscountRuleMetadata.meta = new RegistrationDiscountRuleMetadata();
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
                meta.AddTypeMap("RoomPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("RsPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DrPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("BhpPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ResepPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsDiscountGlobal", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsDiscountGlobalInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("DiscountGlobalAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemMedicalPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ItemNonMedicalPercentage", new esTypeMap("numeric", "System.Decimal"));



                meta.Source = "RegistrationDiscountRule";
                meta.Destination = "RegistrationDiscountRule";

                meta.spInsert = "proc_RegistrationDiscountRuleInsert";
                meta.spUpdate = "proc_RegistrationDiscountRuleUpdate";
                meta.spDelete = "proc_RegistrationDiscountRuleDelete";
                meta.spLoadAll = "proc_RegistrationDiscountRuleLoadAll";
                meta.spLoadByPrimaryKey = "proc_RegistrationDiscountRuleLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private RegistrationDiscountRuleMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
