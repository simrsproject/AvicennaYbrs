/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/13/2019 2:53:31 PM
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
    abstract public class esItemTariffUpdateHistoryCollection : esEntityCollectionWAuditLog
    {
        public esItemTariffUpdateHistoryCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ItemTariffUpdateHistoryCollection";
        }

        #region Query Logic
        protected void InitQuery(esItemTariffUpdateHistoryQuery query)
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
            this.InitQuery(query as esItemTariffUpdateHistoryQuery);
        }
        #endregion

        virtual public ItemTariffUpdateHistory DetachEntity(ItemTariffUpdateHistory entity)
        {
            return base.DetachEntity(entity) as ItemTariffUpdateHistory;
        }

        virtual public ItemTariffUpdateHistory AttachEntity(ItemTariffUpdateHistory entity)
        {
            return base.AttachEntity(entity) as ItemTariffUpdateHistory;
        }

        virtual public void Combine(ItemTariffUpdateHistoryCollection collection)
        {
            base.Combine(collection);
        }

        new public ItemTariffUpdateHistory this[int index]
        {
            get
            {
                return base[index] as ItemTariffUpdateHistory;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ItemTariffUpdateHistory);
        }
    }

    [Serializable]
    abstract public class esItemTariffUpdateHistory : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esItemTariffUpdateHistoryQuery GetDynamicQuery()
        {
            return null;
        }

        public esItemTariffUpdateHistory()
        {
        }

        public esItemTariffUpdateHistory(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String requestNo, String sRTariffType, String itemID, String classID, DateTime startingDate)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(requestNo, sRTariffType, itemID, classID, startingDate);
            else
                return LoadByPrimaryKeyStoredProcedure(requestNo, sRTariffType, itemID, classID, startingDate);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String requestNo, String sRTariffType, String itemID, String classID, DateTime startingDate)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(requestNo, sRTariffType, itemID, classID, startingDate);
            else
                return LoadByPrimaryKeyStoredProcedure(requestNo, sRTariffType, itemID, classID, startingDate);
        }

        private bool LoadByPrimaryKeyDynamic(String requestNo, String sRTariffType, String itemID, String classID, DateTime startingDate)
        {
            esItemTariffUpdateHistoryQuery query = this.GetDynamicQuery();
            query.Where(query.RequestNo == requestNo, query.SRTariffType == sRTariffType, query.ItemID == itemID, query.ClassID == classID, query.StartingDate == startingDate);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String requestNo, String sRTariffType, String itemID, String classID, DateTime startingDate)
        {
            esParameters parms = new esParameters();
            parms.Add("RequestNo", requestNo);
            parms.Add("SRTariffType", sRTariffType);
            parms.Add("ItemID", itemID);
            parms.Add("ClassID", classID);
            parms.Add("StartingDate", startingDate);
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
                        case "RequestNo": this.str.RequestNo = (string)value; break;
                        case "SRTariffType": this.str.SRTariffType = (string)value; break;
                        case "ItemID": this.str.ItemID = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "StartingDate": this.str.StartingDate = (string)value; break;
                        case "Price": this.str.Price = (string)value; break;
                        case "ToPrice": this.str.ToPrice = (string)value; break;
                        case "DiscPercentage": this.str.DiscPercentage = (string)value; break;
                        case "ToDiscPercentage": this.str.ToDiscPercentage = (string)value; break;
                        case "IsAdminCalculation": this.str.IsAdminCalculation = (string)value; break;
                        case "ToIsAdminCalculation": this.str.ToIsAdminCalculation = (string)value; break;
                        case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;
                        case "ToIsAllowDiscount": this.str.ToIsAllowDiscount = (string)value; break;
                        case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;
                        case "ToIsAllowVariable": this.str.ToIsAllowVariable = (string)value; break;
                        case "IsAllowCito": this.str.IsAllowCito = (string)value; break;
                        case "ToIsAllowCito": this.str.ToIsAllowCito = (string)value; break;
                        case "IsCitoInPercent": this.str.IsCitoInPercent = (string)value; break;
                        case "ToIsCitoInPercent": this.str.ToIsCitoInPercent = (string)value; break;
                        case "CitoValue": this.str.CitoValue = (string)value; break;
                        case "ToCitoValue": this.str.ToCitoValue = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "IsCitoFromStandardReference": this.str.IsCitoFromStandardReference = (string)value; break;
                        case "ToIsCitoFromStandardReference": this.str.ToIsCitoFromStandardReference = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "StartingDate":

                            if (value == null || value is System.DateTime)
                                this.StartingDate = (System.DateTime?)value;
                            break;
                        case "Price":

                            if (value == null || value is System.Decimal)
                                this.Price = (System.Decimal?)value;
                            break;
                        case "ToPrice":

                            if (value == null || value is System.Decimal)
                                this.ToPrice = (System.Decimal?)value;
                            break;
                        case "DiscPercentage":

                            if (value == null || value is System.Decimal)
                                this.DiscPercentage = (System.Decimal?)value;
                            break;
                        case "ToDiscPercentage":

                            if (value == null || value is System.Decimal)
                                this.ToDiscPercentage = (System.Decimal?)value;
                            break;
                        case "IsAdminCalculation":

                            if (value == null || value is System.Boolean)
                                this.IsAdminCalculation = (System.Boolean?)value;
                            break;
                        case "ToIsAdminCalculation":

                            if (value == null || value is System.Boolean)
                                this.ToIsAdminCalculation = (System.Boolean?)value;
                            break;
                        case "IsAllowDiscount":

                            if (value == null || value is System.Boolean)
                                this.IsAllowDiscount = (System.Boolean?)value;
                            break;
                        case "ToIsAllowDiscount":

                            if (value == null || value is System.Boolean)
                                this.ToIsAllowDiscount = (System.Boolean?)value;
                            break;
                        case "IsAllowVariable":

                            if (value == null || value is System.Boolean)
                                this.IsAllowVariable = (System.Boolean?)value;
                            break;
                        case "ToIsAllowVariable":

                            if (value == null || value is System.Boolean)
                                this.ToIsAllowVariable = (System.Boolean?)value;
                            break;
                        case "IsAllowCito":

                            if (value == null || value is System.Boolean)
                                this.IsAllowCito = (System.Boolean?)value;
                            break;
                        case "ToIsAllowCito":

                            if (value == null || value is System.Boolean)
                                this.ToIsAllowCito = (System.Boolean?)value;
                            break;
                        case "IsCitoInPercent":

                            if (value == null || value is System.Boolean)
                                this.IsCitoInPercent = (System.Boolean?)value;
                            break;
                        case "ToIsCitoInPercent":

                            if (value == null || value is System.Boolean)
                                this.ToIsCitoInPercent = (System.Boolean?)value;
                            break;
                        case "CitoValue":

                            if (value == null || value is System.Decimal)
                                this.CitoValue = (System.Decimal?)value;
                            break;
                        case "ToCitoValue":

                            if (value == null || value is System.Decimal)
                                this.ToCitoValue = (System.Decimal?)value;
                            break;
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;
                        case "IsCitoFromStandardReference":

                            if (value == null || value is System.Boolean)
                                this.IsCitoFromStandardReference = (System.Boolean?)value;
                            break;
                        case "ToIsCitoFromStandardReference":

                            if (value == null || value is System.Boolean)
                                this.ToIsCitoFromStandardReference = (System.Boolean?)value;
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
        /// Maps to ItemTariffUpdateHistory.RequestNo
        /// </summary>
        virtual public System.String RequestNo
        {
            get
            {
                return base.GetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.RequestNo);
            }

            set
            {
                base.SetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.RequestNo, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.SRTariffType
        /// </summary>
        virtual public System.String SRTariffType
        {
            get
            {
                return base.GetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.SRTariffType);
            }

            set
            {
                base.SetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.SRTariffType, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ItemID
        /// </summary>
        virtual public System.String ItemID
        {
            get
            {
                return base.GetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.ItemID);
            }

            set
            {
                base.SetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.ItemID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.ClassID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.StartingDate
        /// </summary>
        virtual public System.DateTime? StartingDate
        {
            get
            {
                return base.GetSystemDateTime(ItemTariffUpdateHistoryMetadata.ColumnNames.StartingDate);
            }

            set
            {
                base.SetSystemDateTime(ItemTariffUpdateHistoryMetadata.ColumnNames.StartingDate, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.Price
        /// </summary>
        virtual public System.Decimal? Price
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.Price);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.Price, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToPrice
        /// </summary>
        virtual public System.Decimal? ToPrice
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.ToPrice);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.ToPrice, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.DiscPercentage
        /// </summary>
        virtual public System.Decimal? DiscPercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.DiscPercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.DiscPercentage, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToDiscPercentage
        /// </summary>
        virtual public System.Decimal? ToDiscPercentage
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.ToDiscPercentage);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.ToDiscPercentage, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.IsAdminCalculation
        /// </summary>
        virtual public System.Boolean? IsAdminCalculation
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAdminCalculation);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAdminCalculation, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToIsAdminCalculation
        /// </summary>
        virtual public System.Boolean? ToIsAdminCalculation
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAdminCalculation);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAdminCalculation, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.IsAllowDiscount
        /// </summary>
        virtual public System.Boolean? IsAllowDiscount
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowDiscount);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowDiscount, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToIsAllowDiscount
        /// </summary>
        virtual public System.Boolean? ToIsAllowDiscount
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowDiscount);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowDiscount, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.IsAllowVariable
        /// </summary>
        virtual public System.Boolean? IsAllowVariable
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowVariable);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowVariable, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToIsAllowVariable
        /// </summary>
        virtual public System.Boolean? ToIsAllowVariable
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowVariable);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowVariable, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.IsAllowCito
        /// </summary>
        virtual public System.Boolean? IsAllowCito
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowCito);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowCito, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToIsAllowCito
        /// </summary>
        virtual public System.Boolean? ToIsAllowCito
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowCito);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowCito, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.IsCitoInPercent
        /// </summary>
        virtual public System.Boolean? IsCitoInPercent
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsCitoInPercent);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsCitoInPercent, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToIsCitoInPercent
        /// </summary>
        virtual public System.Boolean? ToIsCitoInPercent
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsCitoInPercent);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsCitoInPercent, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.CitoValue
        /// </summary>
        virtual public System.Decimal? CitoValue
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.CitoValue);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.CitoValue, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToCitoValue
        /// </summary>
        virtual public System.Decimal? ToCitoValue
        {
            get
            {
                return base.GetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.ToCitoValue);
            }

            set
            {
                base.SetSystemDecimal(ItemTariffUpdateHistoryMetadata.ColumnNames.ToCitoValue, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ItemTariffUpdateHistoryMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ItemTariffUpdateHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ItemTariffUpdateHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.IsCitoFromStandardReference
        /// </summary>
        virtual public System.Boolean? IsCitoFromStandardReference
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsCitoFromStandardReference);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.IsCitoFromStandardReference, value);
            }
        }
        /// <summary>
        /// Maps to ItemTariffUpdateHistory.ToIsCitoFromStandardReference
        /// </summary>
        virtual public System.Boolean? ToIsCitoFromStandardReference
        {
            get
            {
                return base.GetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsCitoFromStandardReference);
            }

            set
            {
                base.SetSystemBoolean(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsCitoFromStandardReference, value);
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
            public esStrings(esItemTariffUpdateHistory entity)
            {
                this.entity = entity;
            }
            public System.String RequestNo
            {
                get
                {
                    System.String data = entity.RequestNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RequestNo = null;
                    else entity.RequestNo = Convert.ToString(value);
                }
            }
            public System.String SRTariffType
            {
                get
                {
                    System.String data = entity.SRTariffType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRTariffType = null;
                    else entity.SRTariffType = Convert.ToString(value);
                }
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
            public System.String StartingDate
            {
                get
                {
                    System.DateTime? data = entity.StartingDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.StartingDate = null;
                    else entity.StartingDate = Convert.ToDateTime(value);
                }
            }
            public System.String Price
            {
                get
                {
                    System.Decimal? data = entity.Price;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Price = null;
                    else entity.Price = Convert.ToDecimal(value);
                }
            }
            public System.String ToPrice
            {
                get
                {
                    System.Decimal? data = entity.ToPrice;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToPrice = null;
                    else entity.ToPrice = Convert.ToDecimal(value);
                }
            }
            public System.String DiscPercentage
            {
                get
                {
                    System.Decimal? data = entity.DiscPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiscPercentage = null;
                    else entity.DiscPercentage = Convert.ToDecimal(value);
                }
            }
            public System.String ToDiscPercentage
            {
                get
                {
                    System.Decimal? data = entity.ToDiscPercentage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToDiscPercentage = null;
                    else entity.ToDiscPercentage = Convert.ToDecimal(value);
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
            public System.String ToIsAdminCalculation
            {
                get
                {
                    System.Boolean? data = entity.ToIsAdminCalculation;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToIsAdminCalculation = null;
                    else entity.ToIsAdminCalculation = Convert.ToBoolean(value);
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
            public System.String ToIsAllowDiscount
            {
                get
                {
                    System.Boolean? data = entity.ToIsAllowDiscount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToIsAllowDiscount = null;
                    else entity.ToIsAllowDiscount = Convert.ToBoolean(value);
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
            public System.String ToIsAllowVariable
            {
                get
                {
                    System.Boolean? data = entity.ToIsAllowVariable;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToIsAllowVariable = null;
                    else entity.ToIsAllowVariable = Convert.ToBoolean(value);
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
            public System.String ToIsAllowCito
            {
                get
                {
                    System.Boolean? data = entity.ToIsAllowCito;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToIsAllowCito = null;
                    else entity.ToIsAllowCito = Convert.ToBoolean(value);
                }
            }
            public System.String IsCitoInPercent
            {
                get
                {
                    System.Boolean? data = entity.IsCitoInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCitoInPercent = null;
                    else entity.IsCitoInPercent = Convert.ToBoolean(value);
                }
            }
            public System.String ToIsCitoInPercent
            {
                get
                {
                    System.Boolean? data = entity.ToIsCitoInPercent;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToIsCitoInPercent = null;
                    else entity.ToIsCitoInPercent = Convert.ToBoolean(value);
                }
            }
            public System.String CitoValue
            {
                get
                {
                    System.Decimal? data = entity.CitoValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CitoValue = null;
                    else entity.CitoValue = Convert.ToDecimal(value);
                }
            }
            public System.String ToCitoValue
            {
                get
                {
                    System.Decimal? data = entity.ToCitoValue;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToCitoValue = null;
                    else entity.ToCitoValue = Convert.ToDecimal(value);
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
            public System.String IsCitoFromStandardReference
            {
                get
                {
                    System.Boolean? data = entity.IsCitoFromStandardReference;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCitoFromStandardReference = null;
                    else entity.IsCitoFromStandardReference = Convert.ToBoolean(value);
                }
            }
            public System.String ToIsCitoFromStandardReference
            {
                get
                {
                    System.Boolean? data = entity.ToIsCitoFromStandardReference;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ToIsCitoFromStandardReference = null;
                    else entity.ToIsCitoFromStandardReference = Convert.ToBoolean(value);
                }
            }
            private esItemTariffUpdateHistory entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esItemTariffUpdateHistoryQuery query)
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
                throw new Exception("esItemTariffUpdateHistory can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ItemTariffUpdateHistory : esItemTariffUpdateHistory
    {
    }

    [Serializable]
    abstract public class esItemTariffUpdateHistoryQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ItemTariffUpdateHistoryMetadata.Meta();
            }
        }

        public esQueryItem RequestNo
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.RequestNo, esSystemType.String);
            }
        }

        public esQueryItem SRTariffType
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.SRTariffType, esSystemType.String);
            }
        }

        public esQueryItem ItemID
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ItemID, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem StartingDate
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
            }
        }

        public esQueryItem Price
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.Price, esSystemType.Decimal);
            }
        }

        public esQueryItem ToPrice
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToPrice, esSystemType.Decimal);
            }
        }

        public esQueryItem DiscPercentage
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.DiscPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem ToDiscPercentage
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToDiscPercentage, esSystemType.Decimal);
            }
        }

        public esQueryItem IsAdminCalculation
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.IsAdminCalculation, esSystemType.Boolean);
            }
        }

        public esQueryItem ToIsAdminCalculation
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAdminCalculation, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowDiscount
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
            }
        }

        public esQueryItem ToIsAllowDiscount
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowDiscount, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowVariable
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
            }
        }

        public esQueryItem ToIsAllowVariable
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowVariable, esSystemType.Boolean);
            }
        }

        public esQueryItem IsAllowCito
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowCito, esSystemType.Boolean);
            }
        }

        public esQueryItem ToIsAllowCito
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowCito, esSystemType.Boolean);
            }
        }

        public esQueryItem IsCitoInPercent
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.IsCitoInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem ToIsCitoInPercent
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsCitoInPercent, esSystemType.Boolean);
            }
        }

        public esQueryItem CitoValue
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.CitoValue, esSystemType.Decimal);
            }
        }

        public esQueryItem ToCitoValue
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToCitoValue, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem IsCitoFromStandardReference
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.IsCitoFromStandardReference, esSystemType.Boolean);
            }
        }

        public esQueryItem ToIsCitoFromStandardReference
        {
            get
            {
                return new esQueryItem(this, ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsCitoFromStandardReference, esSystemType.Boolean);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ItemTariffUpdateHistoryCollection")]
    public partial class ItemTariffUpdateHistoryCollection : esItemTariffUpdateHistoryCollection, IEnumerable<ItemTariffUpdateHistory>
    {
        public ItemTariffUpdateHistoryCollection()
        {

        }

        public static implicit operator List<ItemTariffUpdateHistory>(ItemTariffUpdateHistoryCollection coll)
        {
            List<ItemTariffUpdateHistory> list = new List<ItemTariffUpdateHistory>();

            foreach (ItemTariffUpdateHistory emp in coll)
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
                return ItemTariffUpdateHistoryMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTariffUpdateHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ItemTariffUpdateHistory(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ItemTariffUpdateHistory();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ItemTariffUpdateHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTariffUpdateHistoryQuery();
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
        public bool Load(ItemTariffUpdateHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ItemTariffUpdateHistory AddNew()
        {
            ItemTariffUpdateHistory entity = base.AddNewEntity() as ItemTariffUpdateHistory;

            return entity;
        }
        public ItemTariffUpdateHistory FindByPrimaryKey(String requestNo, String sRTariffType, String itemID, String classID, DateTime startingDate)
        {
            return base.FindByPrimaryKey(requestNo, sRTariffType, itemID, classID, startingDate) as ItemTariffUpdateHistory;
        }

        #region IEnumerable< ItemTariffUpdateHistory> Members

        IEnumerator<ItemTariffUpdateHistory> IEnumerable<ItemTariffUpdateHistory>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ItemTariffUpdateHistory;
            }
        }

        #endregion

        private ItemTariffUpdateHistoryQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ItemTariffUpdateHistory' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ItemTariffUpdateHistory ({RequestNo, SRTariffType, ItemID, ClassID, StartingDate})")]
    [Serializable]
    public partial class ItemTariffUpdateHistory : esItemTariffUpdateHistory
    {
        public ItemTariffUpdateHistory()
        {
        }

        public ItemTariffUpdateHistory(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ItemTariffUpdateHistoryMetadata.Meta();
            }
        }

        override protected esItemTariffUpdateHistoryQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ItemTariffUpdateHistoryQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ItemTariffUpdateHistoryQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ItemTariffUpdateHistoryQuery();
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
        public bool Load(ItemTariffUpdateHistoryQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ItemTariffUpdateHistoryQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ItemTariffUpdateHistoryQuery : esItemTariffUpdateHistoryQuery
    {
        public ItemTariffUpdateHistoryQuery()
        {

        }

        public ItemTariffUpdateHistoryQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ItemTariffUpdateHistoryQuery";
        }
    }

    [Serializable]
    public partial class ItemTariffUpdateHistoryMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ItemTariffUpdateHistoryMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.RequestNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.RequestNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.SRTariffType, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.SRTariffType;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ItemID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ClassID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.StartingDate, 4, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.StartingDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.Price, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.Price;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToPrice, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToPrice;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.DiscPercentage, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.DiscPercentage;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToDiscPercentage, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToDiscPercentage;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAdminCalculation, 9, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.IsAdminCalculation;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAdminCalculation, 10, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToIsAdminCalculation;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowDiscount, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.IsAllowDiscount;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowDiscount, 12, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToIsAllowDiscount;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowVariable, 13, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.IsAllowVariable;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowVariable, 14, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToIsAllowVariable;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.IsAllowCito, 15, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.IsAllowCito;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsAllowCito, 16, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToIsAllowCito;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.IsCitoInPercent, 17, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.IsCitoInPercent;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsCitoInPercent, 18, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToIsCitoInPercent;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.CitoValue, 19, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.CitoValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToCitoValue, 20, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToCitoValue;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.IsCitoFromStandardReference, 23, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.IsCitoFromStandardReference;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ItemTariffUpdateHistoryMetadata.ColumnNames.ToIsCitoFromStandardReference, 24, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ItemTariffUpdateHistoryMetadata.PropertyNames.ToIsCitoFromStandardReference;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ItemTariffUpdateHistoryMetadata Meta()
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
            public const string RequestNo = "RequestNo";
            public const string SRTariffType = "SRTariffType";
            public const string ItemID = "ItemID";
            public const string ClassID = "ClassID";
            public const string StartingDate = "StartingDate";
            public const string Price = "Price";
            public const string ToPrice = "ToPrice";
            public const string DiscPercentage = "DiscPercentage";
            public const string ToDiscPercentage = "ToDiscPercentage";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string ToIsAdminCalculation = "ToIsAdminCalculation";
            public const string IsAllowDiscount = "IsAllowDiscount";
            public const string ToIsAllowDiscount = "ToIsAllowDiscount";
            public const string IsAllowVariable = "IsAllowVariable";
            public const string ToIsAllowVariable = "ToIsAllowVariable";
            public const string IsAllowCito = "IsAllowCito";
            public const string ToIsAllowCito = "ToIsAllowCito";
            public const string IsCitoInPercent = "IsCitoInPercent";
            public const string ToIsCitoInPercent = "ToIsCitoInPercent";
            public const string CitoValue = "CitoValue";
            public const string ToCitoValue = "ToCitoValue";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
            public const string ToIsCitoFromStandardReference = "ToIsCitoFromStandardReference";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RequestNo = "RequestNo";
            public const string SRTariffType = "SRTariffType";
            public const string ItemID = "ItemID";
            public const string ClassID = "ClassID";
            public const string StartingDate = "StartingDate";
            public const string Price = "Price";
            public const string ToPrice = "ToPrice";
            public const string DiscPercentage = "DiscPercentage";
            public const string ToDiscPercentage = "ToDiscPercentage";
            public const string IsAdminCalculation = "IsAdminCalculation";
            public const string ToIsAdminCalculation = "ToIsAdminCalculation";
            public const string IsAllowDiscount = "IsAllowDiscount";
            public const string ToIsAllowDiscount = "ToIsAllowDiscount";
            public const string IsAllowVariable = "IsAllowVariable";
            public const string ToIsAllowVariable = "ToIsAllowVariable";
            public const string IsAllowCito = "IsAllowCito";
            public const string ToIsAllowCito = "ToIsAllowCito";
            public const string IsCitoInPercent = "IsCitoInPercent";
            public const string ToIsCitoInPercent = "ToIsCitoInPercent";
            public const string CitoValue = "CitoValue";
            public const string ToCitoValue = "ToCitoValue";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string IsCitoFromStandardReference = "IsCitoFromStandardReference";
            public const string ToIsCitoFromStandardReference = "ToIsCitoFromStandardReference";
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
            lock (typeof(ItemTariffUpdateHistoryMetadata))
            {
                if (ItemTariffUpdateHistoryMetadata.mapDelegates == null)
                {
                    ItemTariffUpdateHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ItemTariffUpdateHistoryMetadata.meta == null)
                {
                    ItemTariffUpdateHistoryMetadata.meta = new ItemTariffUpdateHistoryMetadata();
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

                meta.AddTypeMap("RequestNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ToPrice", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("DiscPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ToDiscPercentage", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("IsAdminCalculation", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ToIsAdminCalculation", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ToIsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ToIsAllowVariable", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsAllowCito", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ToIsAllowCito", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsCitoInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ToIsCitoInPercent", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CitoValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ToCitoValue", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsCitoFromStandardReference", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ToIsCitoFromStandardReference", new esTypeMap("bit", "System.Boolean"));


                meta.Source = "ItemTariffUpdateHistory";
                meta.Destination = "ItemTariffUpdateHistory";
                meta.spInsert = "proc_ItemTariffUpdateHistoryInsert";
                meta.spUpdate = "proc_ItemTariffUpdateHistoryUpdate";
                meta.spDelete = "proc_ItemTariffUpdateHistoryDelete";
                meta.spLoadAll = "proc_ItemTariffUpdateHistoryLoadAll";
                meta.spLoadByPrimaryKey = "proc_ItemTariffUpdateHistoryLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ItemTariffUpdateHistoryMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
