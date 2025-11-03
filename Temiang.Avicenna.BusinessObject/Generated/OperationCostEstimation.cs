/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/10/2015 4:56:50 PM
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
    abstract public class esOperationCostEstimationCollection : esEntityCollectionWAuditLog
    {
        public esOperationCostEstimationCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "OperationCostEstimationCollection";
        }

        #region Query Logic
        protected void InitQuery(esOperationCostEstimationQuery query)
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
            this.InitQuery(query as esOperationCostEstimationQuery);
        }
        #endregion

        virtual public OperationCostEstimation DetachEntity(OperationCostEstimation entity)
        {
            return base.DetachEntity(entity) as OperationCostEstimation;
        }

        virtual public OperationCostEstimation AttachEntity(OperationCostEstimation entity)
        {
            return base.AttachEntity(entity) as OperationCostEstimation;
        }

        virtual public void Combine(OperationCostEstimationCollection collection)
        {
            base.Combine(collection);
        }

        new public OperationCostEstimation this[int index]
        {
            get
            {
                return base[index] as OperationCostEstimation;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(OperationCostEstimation);
        }
    }



    [Serializable]
    abstract public class esOperationCostEstimation : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esOperationCostEstimationQuery GetDynamicQuery()
        {
            return null;
        }

        public esOperationCostEstimation()
        {

        }

        public esOperationCostEstimation(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo)
        {
            esOperationCostEstimationQuery query = this.GetDynamicQuery();
            query.Where(query.DiagnoseID == diagnoseID, query.ProcedureID == procedureID, query.SRProcedureCategory == sRProcedureCategory, query.ClassID == classID, query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("DiagnoseID", diagnoseID); parms.Add("ProcedureID", procedureID); parms.Add("SRProcedureCategory", sRProcedureCategory); parms.Add("ClassID", classID); parms.Add("RegistrationNo", registrationNo);
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
                        case "DiagnoseID": this.str.DiagnoseID = (string)value; break;
                        case "ProcedureID": this.str.ProcedureID = (string)value; break;
                        case "SRProcedureCategory": this.str.SRProcedureCategory = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "CostAmount": this.str.CostAmount = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "CostAmount":

                            if (value == null || value is System.Decimal)
                                this.CostAmount = (System.Decimal?)value;
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
        /// Maps to OperationCostEstimation.DiagnoseID
        /// </summary>
        virtual public System.String DiagnoseID
        {
            get
            {
                return base.GetSystemString(OperationCostEstimationMetadata.ColumnNames.DiagnoseID);
            }

            set
            {
                base.SetSystemString(OperationCostEstimationMetadata.ColumnNames.DiagnoseID, value);
            }
        }

        /// <summary>
        /// Maps to OperationCostEstimation.ProcedureID
        /// </summary>
        virtual public System.String ProcedureID
        {
            get
            {
                return base.GetSystemString(OperationCostEstimationMetadata.ColumnNames.ProcedureID);
            }

            set
            {
                base.SetSystemString(OperationCostEstimationMetadata.ColumnNames.ProcedureID, value);
            }
        }

        /// <summary>
        /// Maps to OperationCostEstimation.SRProcedureCategory
        /// </summary>
        virtual public System.String SRProcedureCategory
        {
            get
            {
                return base.GetSystemString(OperationCostEstimationMetadata.ColumnNames.SRProcedureCategory);
            }

            set
            {
                base.SetSystemString(OperationCostEstimationMetadata.ColumnNames.SRProcedureCategory, value);
            }
        }

        /// <summary>
        /// Maps to OperationCostEstimation.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(OperationCostEstimationMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(OperationCostEstimationMetadata.ColumnNames.ClassID, value);
            }
        }

        /// <summary>
        /// Maps to OperationCostEstimation.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(OperationCostEstimationMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(OperationCostEstimationMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to OperationCostEstimation.CostAmount
        /// </summary>
        virtual public System.Decimal? CostAmount
        {
            get
            {
                return base.GetSystemDecimal(OperationCostEstimationMetadata.ColumnNames.CostAmount);
            }

            set
            {
                base.SetSystemDecimal(OperationCostEstimationMetadata.ColumnNames.CostAmount, value);
            }
        }

        /// <summary>
        /// Maps to OperationCostEstimation.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(OperationCostEstimationMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(OperationCostEstimationMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to OperationCostEstimation.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(OperationCostEstimationMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(OperationCostEstimationMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esOperationCostEstimation entity)
            {
                this.entity = entity;
            }


            public System.String DiagnoseID
            {
                get
                {
                    System.String data = entity.DiagnoseID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseID = null;
                    else entity.DiagnoseID = Convert.ToString(value);
                }
            }

            public System.String ProcedureID
            {
                get
                {
                    System.String data = entity.ProcedureID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProcedureID = null;
                    else entity.ProcedureID = Convert.ToString(value);
                }
            }

            public System.String SRProcedureCategory
            {
                get
                {
                    System.String data = entity.SRProcedureCategory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRProcedureCategory = null;
                    else entity.SRProcedureCategory = Convert.ToString(value);
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

            public System.String CostAmount
            {
                get
                {
                    System.Decimal? data = entity.CostAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CostAmount = null;
                    else entity.CostAmount = Convert.ToDecimal(value);
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


            private esOperationCostEstimation entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esOperationCostEstimationQuery query)
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
                throw new Exception("esOperationCostEstimation can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class OperationCostEstimation : esOperationCostEstimation
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
    abstract public class esOperationCostEstimationQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return OperationCostEstimationMetadata.Meta();
            }
        }


        public esQueryItem DiagnoseID
        {
            get
            {
                return new esQueryItem(this, OperationCostEstimationMetadata.ColumnNames.DiagnoseID, esSystemType.String);
            }
        }

        public esQueryItem ProcedureID
        {
            get
            {
                return new esQueryItem(this, OperationCostEstimationMetadata.ColumnNames.ProcedureID, esSystemType.String);
            }
        }

        public esQueryItem SRProcedureCategory
        {
            get
            {
                return new esQueryItem(this, OperationCostEstimationMetadata.ColumnNames.SRProcedureCategory, esSystemType.String);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, OperationCostEstimationMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, OperationCostEstimationMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem CostAmount
        {
            get
            {
                return new esQueryItem(this, OperationCostEstimationMetadata.ColumnNames.CostAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, OperationCostEstimationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, OperationCostEstimationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("OperationCostEstimationCollection")]
    public partial class OperationCostEstimationCollection : esOperationCostEstimationCollection, IEnumerable<OperationCostEstimation>
    {
        public OperationCostEstimationCollection()
        {

        }

        public static implicit operator List<OperationCostEstimation>(OperationCostEstimationCollection coll)
        {
            List<OperationCostEstimation> list = new List<OperationCostEstimation>();

            foreach (OperationCostEstimation emp in coll)
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
                return OperationCostEstimationMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OperationCostEstimationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new OperationCostEstimation(row);
        }

        override protected esEntity CreateEntity()
        {
            return new OperationCostEstimation();
        }


        #endregion


        [BrowsableAttribute(false)]
        public OperationCostEstimationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OperationCostEstimationQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(OperationCostEstimationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public OperationCostEstimation AddNew()
        {
            OperationCostEstimation entity = base.AddNewEntity() as OperationCostEstimation;

            return entity;
        }

        public OperationCostEstimation FindByPrimaryKey(System.String diagnoseID, System.String procedureID, System.String sRProcedureCategory, System.String classID, System.String registrationNo)
        {
            return base.FindByPrimaryKey(diagnoseID, procedureID, sRProcedureCategory, classID, registrationNo) as OperationCostEstimation;
        }


        #region IEnumerable<OperationCostEstimation> Members

        IEnumerator<OperationCostEstimation> IEnumerable<OperationCostEstimation>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as OperationCostEstimation;
            }
        }

        #endregion

        private OperationCostEstimationQuery query;
    }


    /// <summary>
    /// Encapsulates the 'OperationCostEstimation' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("OperationCostEstimation ({DiagnoseID},{ProcedureID},{SRProcedureCategory},{ClassID},{RegistrationNo})")]
    [Serializable]
    public partial class OperationCostEstimation : esOperationCostEstimation
    {
        public OperationCostEstimation()
        {

        }

        public OperationCostEstimation(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return OperationCostEstimationMetadata.Meta();
            }
        }



        override protected esOperationCostEstimationQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new OperationCostEstimationQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public OperationCostEstimationQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new OperationCostEstimationQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(OperationCostEstimationQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private OperationCostEstimationQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class OperationCostEstimationQuery : esOperationCostEstimationQuery
    {
        public OperationCostEstimationQuery()
        {

        }

        public OperationCostEstimationQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "OperationCostEstimationQuery";
        }


    }


    [Serializable]
    public partial class OperationCostEstimationMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected OperationCostEstimationMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(OperationCostEstimationMetadata.ColumnNames.DiagnoseID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = OperationCostEstimationMetadata.PropertyNames.DiagnoseID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(OperationCostEstimationMetadata.ColumnNames.ProcedureID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = OperationCostEstimationMetadata.PropertyNames.ProcedureID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(OperationCostEstimationMetadata.ColumnNames.SRProcedureCategory, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = OperationCostEstimationMetadata.PropertyNames.SRProcedureCategory;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(OperationCostEstimationMetadata.ColumnNames.ClassID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = OperationCostEstimationMetadata.PropertyNames.ClassID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(OperationCostEstimationMetadata.ColumnNames.RegistrationNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = OperationCostEstimationMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(OperationCostEstimationMetadata.ColumnNames.CostAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = OperationCostEstimationMetadata.PropertyNames.CostAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OperationCostEstimationMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = OperationCostEstimationMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(OperationCostEstimationMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = OperationCostEstimationMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public OperationCostEstimationMetadata Meta()
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
            public const string DiagnoseID = "DiagnoseID";
            public const string ProcedureID = "ProcedureID";
            public const string SRProcedureCategory = "SRProcedureCategory";
            public const string ClassID = "ClassID";
            public const string RegistrationNo = "RegistrationNo";
            public const string CostAmount = "CostAmount";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string DiagnoseID = "DiagnoseID";
            public const string ProcedureID = "ProcedureID";
            public const string SRProcedureCategory = "SRProcedureCategory";
            public const string ClassID = "ClassID";
            public const string RegistrationNo = "RegistrationNo";
            public const string CostAmount = "CostAmount";
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
            lock (typeof(OperationCostEstimationMetadata))
            {
                if (OperationCostEstimationMetadata.mapDelegates == null)
                {
                    OperationCostEstimationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (OperationCostEstimationMetadata.meta == null)
                {
                    OperationCostEstimationMetadata.meta = new OperationCostEstimationMetadata();
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


                meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ProcedureID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRProcedureCategory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CostAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "OperationCostEstimation";
                meta.Destination = "OperationCostEstimation";

                meta.spInsert = "proc_OperationCostEstimationInsert";
                meta.spUpdate = "proc_OperationCostEstimationUpdate";
                meta.spDelete = "proc_OperationCostEstimationDelete";
                meta.spLoadAll = "proc_OperationCostEstimationLoadAll";
                meta.spLoadByPrimaryKey = "proc_OperationCostEstimationLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private OperationCostEstimationMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
