/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 16/04/2025 11:48:13
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

namespace Temiang.Avicenna.BusinessObject.Interop.Wynakom
{
    [Serializable]
    abstract public class esSatusehatOrderedItemsCollection : esEntityCollectionWAuditLog
    {
        public esSatusehatOrderedItemsCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "SatusehatOrderedItemsCollection";
        }

        #region Query Logic
        protected void InitQuery(esSatusehatOrderedItemsQuery query)
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
            this.InitQuery(query as esSatusehatOrderedItemsQuery);
        }
        #endregion

        virtual public SatusehatOrderedItems DetachEntity(SatusehatOrderedItems entity)
        {
            return base.DetachEntity(entity) as SatusehatOrderedItems;
        }

        virtual public SatusehatOrderedItems AttachEntity(SatusehatOrderedItems entity)
        {
            return base.AttachEntity(entity) as SatusehatOrderedItems;
        }

        virtual public void Combine(SatusehatOrderedItemsCollection collection)
        {
            base.Combine(collection);
        }

        new public SatusehatOrderedItems this[int index]
        {
            get
            {
                return base[index] as SatusehatOrderedItems;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(SatusehatOrderedItems);
        }
    }

    [Serializable]
    abstract public class esSatusehatOrderedItems : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esSatusehatOrderedItemsQuery GetDynamicQuery()
        {
            return null;
        }

        public esSatusehatOrderedItems()
        {
        }

        public esSatusehatOrderedItems(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String orderNumber, String orderSequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(orderNumber, orderSequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(orderNumber, orderSequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String orderNumber, String orderSequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(orderNumber, orderSequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(orderNumber, orderSequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String orderNumber, String orderSequenceNo)
        {
            esSatusehatOrderedItemsQuery query = this.GetDynamicQuery();
            query.Where(query.OrderNumber == orderNumber, query.OrderSequenceNo == orderSequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String orderNumber, String orderSequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("Order_Number", orderNumber);
            parms.Add("Order_Sequence_No", orderSequenceNo);
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
                        case "OrderNumber": this.str.OrderNumber = (string)value; break;
                        case "OrderSequenceNo": this.str.OrderSequenceNo = (string)value; break;
                        case "OrderItemID": this.str.OrderItemID = (string)value; break;
                        case "OrderItemName": this.str.OrderItemName = (string)value; break;
                        case "SSLoincName": this.str.SSLoincName = (string)value; break;
                        case "SSLoincID": this.str.SSLoincID = (string)value; break;
                        case "SSPatientID": this.str.SSPatientID = (string)value; break;
                        case "SSPatientName": this.str.SSPatientName = (string)value; break;
                        case "SSRequesterPractionerID": this.str.SSRequesterPractionerID = (string)value; break;
                        case "SSRequesterPractionerName": this.str.SSRequesterPractionerName = (string)value; break;
                        case "SSEncounterID": this.str.SSEncounterID = (string)value; break;
                        case "SSServiceRequestID": this.str.SSServiceRequestID = (string)value; break;
                        case "SSSpecimenID": this.str.SSSpecimenID = (string)value; break;
                        case "SSObservationID": this.str.SSObservationID = (string)value; break;
                        case "SSDiagnosticReportID": this.str.SSDiagnosticReportID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {

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
        /// Maps to Satusehat_Ordered_Items.Order_Number
        /// </summary>
        virtual public System.String OrderNumber
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.OrderNumber);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.OrderNumber, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.Order_Sequence_No
        /// </summary>
        virtual public System.String OrderSequenceNo
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.OrderSequenceNo);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.OrderSequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.Order_Item_ID
        /// </summary>
        virtual public System.String OrderItemID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.OrderItemID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.OrderItemID, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.Order_Item_Name
        /// </summary>
        virtual public System.String OrderItemName
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.OrderItemName);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.OrderItemName, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Loinc_Name
        /// </summary>
        virtual public System.String SSLoincName
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSLoincName);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSLoincName, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Loinc_ID
        /// </summary>
        virtual public System.String SSLoincID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSLoincID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSLoincID, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Patient_ID
        /// </summary>
        virtual public System.String SSPatientID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSPatientID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSPatientID, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Patient_Name
        /// </summary>
        virtual public System.String SSPatientName
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSPatientName);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSPatientName, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Requester_Practioner_ID
        /// </summary>
        virtual public System.String SSRequesterPractionerID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSRequesterPractionerID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSRequesterPractionerID, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Requester_Practioner_Name
        /// </summary>
        virtual public System.String SSRequesterPractionerName
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSRequesterPractionerName);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSRequesterPractionerName, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Encounter_ID
        /// </summary>
        virtual public System.String SSEncounterID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSEncounterID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSEncounterID, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Service_Request_ID
        /// </summary>
        virtual public System.String SSServiceRequestID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSServiceRequestID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSServiceRequestID, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Specimen_ID
        /// </summary>
        virtual public System.String SSSpecimenID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSSpecimenID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSSpecimenID, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Observation_ID
        /// </summary>
        virtual public System.String SSObservationID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSObservationID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSObservationID, value);
            }
        }
        /// <summary>
        /// Maps to Satusehat_Ordered_Items.SS_Diagnostic_Report_ID
        /// </summary>
        virtual public System.String SSDiagnosticReportID
        {
            get
            {
                return base.GetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSDiagnosticReportID);
            }

            set
            {
                base.SetSystemString(SatusehatOrderedItemsMetadata.ColumnNames.SSDiagnosticReportID, value);
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
            public esStrings(esSatusehatOrderedItems entity)
            {
                this.entity = entity;
            }
            public System.String OrderNumber
            {
                get
                {
                    System.String data = entity.OrderNumber;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderNumber = null;
                    else entity.OrderNumber = Convert.ToString(value);
                }
            }
            public System.String OrderSequenceNo
            {
                get
                {
                    System.String data = entity.OrderSequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderSequenceNo = null;
                    else entity.OrderSequenceNo = Convert.ToString(value);
                }
            }
            public System.String OrderItemID
            {
                get
                {
                    System.String data = entity.OrderItemID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderItemID = null;
                    else entity.OrderItemID = Convert.ToString(value);
                }
            }
            public System.String OrderItemName
            {
                get
                {
                    System.String data = entity.OrderItemName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderItemName = null;
                    else entity.OrderItemName = Convert.ToString(value);
                }
            }
            public System.String SSLoincName
            {
                get
                {
                    System.String data = entity.SSLoincName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSLoincName = null;
                    else entity.SSLoincName = Convert.ToString(value);
                }
            }
            public System.String SSLoincID
            {
                get
                {
                    System.String data = entity.SSLoincID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSLoincID = null;
                    else entity.SSLoincID = Convert.ToString(value);
                }
            }
            public System.String SSPatientID
            {
                get
                {
                    System.String data = entity.SSPatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSPatientID = null;
                    else entity.SSPatientID = Convert.ToString(value);
                }
            }
            public System.String SSPatientName
            {
                get
                {
                    System.String data = entity.SSPatientName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSPatientName = null;
                    else entity.SSPatientName = Convert.ToString(value);
                }
            }
            public System.String SSRequesterPractionerID
            {
                get
                {
                    System.String data = entity.SSRequesterPractionerID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSRequesterPractionerID = null;
                    else entity.SSRequesterPractionerID = Convert.ToString(value);
                }
            }
            public System.String SSRequesterPractionerName
            {
                get
                {
                    System.String data = entity.SSRequesterPractionerName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSRequesterPractionerName = null;
                    else entity.SSRequesterPractionerName = Convert.ToString(value);
                }
            }
            public System.String SSEncounterID
            {
                get
                {
                    System.String data = entity.SSEncounterID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSEncounterID = null;
                    else entity.SSEncounterID = Convert.ToString(value);
                }
            }
            public System.String SSServiceRequestID
            {
                get
                {
                    System.String data = entity.SSServiceRequestID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSServiceRequestID = null;
                    else entity.SSServiceRequestID = Convert.ToString(value);
                }
            }
            public System.String SSSpecimenID
            {
                get
                {
                    System.String data = entity.SSSpecimenID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSSpecimenID = null;
                    else entity.SSSpecimenID = Convert.ToString(value);
                }
            }
            public System.String SSObservationID
            {
                get
                {
                    System.String data = entity.SSObservationID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSObservationID = null;
                    else entity.SSObservationID = Convert.ToString(value);
                }
            }
            public System.String SSDiagnosticReportID
            {
                get
                {
                    System.String data = entity.SSDiagnosticReportID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SSDiagnosticReportID = null;
                    else entity.SSDiagnosticReportID = Convert.ToString(value);
                }
            }
            private esSatusehatOrderedItems entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esSatusehatOrderedItemsQuery query)
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
                throw new Exception("esSatusehatOrderedItems can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class SatusehatOrderedItems : esSatusehatOrderedItems
    {
    }

    [Serializable]
    abstract public class esSatusehatOrderedItemsQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return SatusehatOrderedItemsMetadata.Meta();
            }
        }

        public esQueryItem OrderNumber
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.OrderNumber, esSystemType.String);
            }
        }

        public esQueryItem OrderSequenceNo
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.OrderSequenceNo, esSystemType.String);
            }
        }

        public esQueryItem OrderItemID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.OrderItemID, esSystemType.String);
            }
        }

        public esQueryItem OrderItemName
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.OrderItemName, esSystemType.String);
            }
        }

        public esQueryItem SSLoincName
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSLoincName, esSystemType.String);
            }
        }

        public esQueryItem SSLoincID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSLoincID, esSystemType.String);
            }
        }

        public esQueryItem SSPatientID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSPatientID, esSystemType.String);
            }
        }

        public esQueryItem SSPatientName
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSPatientName, esSystemType.String);
            }
        }

        public esQueryItem SSRequesterPractionerID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSRequesterPractionerID, esSystemType.String);
            }
        }

        public esQueryItem SSRequesterPractionerName
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSRequesterPractionerName, esSystemType.String);
            }
        }

        public esQueryItem SSEncounterID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSEncounterID, esSystemType.String);
            }
        }

        public esQueryItem SSServiceRequestID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSServiceRequestID, esSystemType.String);
            }
        }

        public esQueryItem SSSpecimenID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSSpecimenID, esSystemType.String);
            }
        }

        public esQueryItem SSObservationID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSObservationID, esSystemType.String);
            }
        }

        public esQueryItem SSDiagnosticReportID
        {
            get
            {
                return new esQueryItem(this, SatusehatOrderedItemsMetadata.ColumnNames.SSDiagnosticReportID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("SatusehatOrderedItemsCollection")]
    public partial class SatusehatOrderedItemsCollection : esSatusehatOrderedItemsCollection, IEnumerable<SatusehatOrderedItems>
    {
        public SatusehatOrderedItemsCollection()
        {

        }

        public static implicit operator List<SatusehatOrderedItems>(SatusehatOrderedItemsCollection coll)
        {
            List<SatusehatOrderedItems> list = new List<SatusehatOrderedItems>();

            foreach (SatusehatOrderedItems emp in coll)
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
                return SatusehatOrderedItemsMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SatusehatOrderedItemsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new SatusehatOrderedItems(row);
        }

        override protected esEntity CreateEntity()
        {
            return new SatusehatOrderedItems();
        }

        #endregion

        [BrowsableAttribute(false)]
        public SatusehatOrderedItemsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SatusehatOrderedItemsQuery();
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
        public bool Load(SatusehatOrderedItemsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public SatusehatOrderedItems AddNew()
        {
            SatusehatOrderedItems entity = base.AddNewEntity() as SatusehatOrderedItems;

            return entity;
        }
        public SatusehatOrderedItems FindByPrimaryKey(String orderNumber, String orderSequenceNo)
        {
            return base.FindByPrimaryKey(orderNumber, orderSequenceNo) as SatusehatOrderedItems;
        }

        #region IEnumerable< SatusehatOrderedItems> Members

        IEnumerator<SatusehatOrderedItems> IEnumerable<SatusehatOrderedItems>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as SatusehatOrderedItems;
            }
        }

        #endregion

        private SatusehatOrderedItemsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'SatusehatOrderedItems' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SatusehatOrderedItems ({Order_Number, Order_Sequence_No})")]
    [Serializable]
    public partial class SatusehatOrderedItems : esSatusehatOrderedItems
    {
        public SatusehatOrderedItems()
        {
        }

        public SatusehatOrderedItems(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return SatusehatOrderedItemsMetadata.Meta();
            }
        }

        override protected esSatusehatOrderedItemsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new SatusehatOrderedItemsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public SatusehatOrderedItemsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new SatusehatOrderedItemsQuery();
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
        public bool Load(SatusehatOrderedItemsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private SatusehatOrderedItemsQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class SatusehatOrderedItemsQuery : esSatusehatOrderedItemsQuery
    {
        public SatusehatOrderedItemsQuery()
        {

        }

        public SatusehatOrderedItemsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "SatusehatOrderedItemsQuery";
        }
    }

    [Serializable]
    public partial class SatusehatOrderedItemsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected SatusehatOrderedItemsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.OrderNumber, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.OrderNumber;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.OrderSequenceNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.OrderSequenceNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 3;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.OrderItemID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.OrderItemID;
            c.CharacterMaxLength = 2000;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.OrderItemName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.OrderItemName;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSLoincName, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSLoincName;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSLoincID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSLoincID;
            c.CharacterMaxLength = 2000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSPatientID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSPatientID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSPatientName, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSPatientName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSRequesterPractionerID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSRequesterPractionerID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSRequesterPractionerName, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSRequesterPractionerName;
            c.CharacterMaxLength = 200;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSEncounterID, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSEncounterID;
            c.CharacterMaxLength = 36;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSServiceRequestID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSServiceRequestID;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSSpecimenID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSSpecimenID;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSObservationID, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSObservationID;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(SatusehatOrderedItemsMetadata.ColumnNames.SSDiagnosticReportID, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = SatusehatOrderedItemsMetadata.PropertyNames.SSDiagnosticReportID;
            c.CharacterMaxLength = 4000;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public SatusehatOrderedItemsMetadata Meta()
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
            public const string OrderNumber = "Order_Number";
            public const string OrderSequenceNo = "Order_Sequence_No";
            public const string OrderItemID = "Order_Item_ID";
            public const string OrderItemName = "Order_Item_Name";
            public const string SSLoincName = "SS_Loinc_Name";
            public const string SSLoincID = "SS_Loinc_ID";
            public const string SSPatientID = "SS_Patient_ID";
            public const string SSPatientName = "SS_Patient_Name";
            public const string SSRequesterPractionerID = "SS_Requester_Practioner_ID";
            public const string SSRequesterPractionerName = "SS_Requester_Practioner_Name";
            public const string SSEncounterID = "SS_Encounter_ID";
            public const string SSServiceRequestID = "SS_Service_Request_ID";
            public const string SSSpecimenID = "SS_Specimen_ID";
            public const string SSObservationID = "SS_Observation_ID";
            public const string SSDiagnosticReportID = "SS_Diagnostic_Report_ID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string OrderNumber = "OrderNumber";
            public const string OrderSequenceNo = "OrderSequenceNo";
            public const string OrderItemID = "OrderItemID";
            public const string OrderItemName = "OrderItemName";
            public const string SSLoincName = "SSLoincName";
            public const string SSLoincID = "SSLoincID";
            public const string SSPatientID = "SSPatientID";
            public const string SSPatientName = "SSPatientName";
            public const string SSRequesterPractionerID = "SSRequesterPractionerID";
            public const string SSRequesterPractionerName = "SSRequesterPractionerName";
            public const string SSEncounterID = "SSEncounterID";
            public const string SSServiceRequestID = "SSServiceRequestID";
            public const string SSSpecimenID = "SSSpecimenID";
            public const string SSObservationID = "SSObservationID";
            public const string SSDiagnosticReportID = "SSDiagnosticReportID";
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
            lock (typeof(SatusehatOrderedItemsMetadata))
            {
                if (SatusehatOrderedItemsMetadata.mapDelegates == null)
                {
                    SatusehatOrderedItemsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (SatusehatOrderedItemsMetadata.meta == null)
                {
                    SatusehatOrderedItemsMetadata.meta = new SatusehatOrderedItemsMetadata();
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

                meta.AddTypeMap("OrderNumber", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderSequenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderItemID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderItemName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSLoincName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSLoincID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSPatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSPatientName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSRequesterPractionerID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSRequesterPractionerName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSEncounterID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSServiceRequestID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSSpecimenID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSObservationID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SSDiagnosticReportID", new esTypeMap("varchar", "System.String"));


                meta.Source = "Satusehat_Ordered_Items";
                meta.Destination = "Satusehat_Ordered_Items";
                meta.spInsert = "proc_SatusehatOrderedItemsInsert";
                meta.spUpdate = "proc_SatusehatOrderedItemsUpdate";
                meta.spDelete = "proc_SatusehatOrderedItemsDelete";
                meta.spLoadAll = "proc_SatusehatOrderedItemsLoadAll";
                meta.spLoadByPrimaryKey = "proc_SatusehatOrderedItemsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private SatusehatOrderedItemsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
