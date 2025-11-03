/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/4/2016 2:13:18 PM
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
    abstract public class esParamedicFeeByNumberOfPatientsCollection : esEntityCollectionWAuditLog
    {
        public esParamedicFeeByNumberOfPatientsCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "ParamedicFeeByNumberOfPatientsCollection";
        }

        #region Query Logic
        protected void InitQuery(esParamedicFeeByNumberOfPatientsQuery query)
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
            this.InitQuery(query as esParamedicFeeByNumberOfPatientsQuery);
        }
        #endregion

        virtual public ParamedicFeeByNumberOfPatients DetachEntity(ParamedicFeeByNumberOfPatients entity)
        {
            return base.DetachEntity(entity) as ParamedicFeeByNumberOfPatients;
        }

        virtual public ParamedicFeeByNumberOfPatients AttachEntity(ParamedicFeeByNumberOfPatients entity)
        {
            return base.AttachEntity(entity) as ParamedicFeeByNumberOfPatients;
        }

        virtual public void Combine(ParamedicFeeByNumberOfPatientsCollection collection)
        {
            base.Combine(collection);
        }

        new public ParamedicFeeByNumberOfPatients this[int index]
        {
            get
            {
                return base[index] as ParamedicFeeByNumberOfPatients;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ParamedicFeeByNumberOfPatients);
        }
    }



    [Serializable]
    abstract public class esParamedicFeeByNumberOfPatients : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esParamedicFeeByNumberOfPatientsQuery GetDynamicQuery()
        {
            return null;
        }

        public esParamedicFeeByNumberOfPatients()
        {

        }

        public esParamedicFeeByNumberOfPatients(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(System.DateTime registrationDate, System.String paramedicID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationDate, paramedicID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationDate, paramedicID);
        }

        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime registrationDate, System.String paramedicID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationDate, paramedicID);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationDate, paramedicID);
        }

        private bool LoadByPrimaryKeyDynamic(System.DateTime registrationDate, System.String paramedicID)
        {
            esParamedicFeeByNumberOfPatientsQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationDate == registrationDate, query.ParamedicID == paramedicID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(System.DateTime registrationDate, System.String paramedicID)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationDate", registrationDate); parms.Add("ParamedicID", paramedicID);
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
                        case "RegistrationDate": this.str.RegistrationDate = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "NumberOfPatient": this.str.NumberOfPatient = (string)value; break;
                        case "FeeAmount": this.str.FeeAmount = (string)value; break;
                        case "VerificationNo": this.str.VerificationNo = (string)value; break;
                        case "LastCalculatedDateTime": this.str.LastCalculatedDateTime = (string)value; break;
                        case "LastCalculatedByUserID": this.str.LastCalculatedByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "RegistrationDate":

                            if (value == null || value is System.DateTime)
                                this.RegistrationDate = (System.DateTime?)value;
                            break;

                        case "NumberOfPatient":

                            if (value == null || value is System.Int16)
                                this.NumberOfPatient = (System.Int16?)value;
                            break;

                        case "FeeAmount":

                            if (value == null || value is System.Decimal)
                                this.FeeAmount = (System.Decimal?)value;
                            break;

                        case "LastCalculatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastCalculatedDateTime = (System.DateTime?)value;
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
        /// Maps to ParamedicFeeByNumberOfPatients.RegistrationDate
        /// </summary>
        virtual public System.DateTime? RegistrationDate
        {
            get
            {
                return base.GetSystemDateTime(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.RegistrationDate);
            }

            set
            {
                base.SetSystemDateTime(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.RegistrationDate, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeByNumberOfPatients.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.ParamedicID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeByNumberOfPatients.NumberOfPatient
        /// </summary>
        virtual public System.Int16? NumberOfPatient
        {
            get
            {
                return base.GetSystemInt16(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.NumberOfPatient);
            }

            set
            {
                base.SetSystemInt16(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.NumberOfPatient, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeByNumberOfPatients.FeeAmount
        /// </summary>
        virtual public System.Decimal? FeeAmount
        {
            get
            {
                return base.GetSystemDecimal(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.FeeAmount);
            }

            set
            {
                base.SetSystemDecimal(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.FeeAmount, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeByNumberOfPatients.VerificationNo
        /// </summary>
        virtual public System.String VerificationNo
        {
            get
            {
                return base.GetSystemString(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.VerificationNo);
            }

            set
            {
                base.SetSystemString(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.VerificationNo, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeByNumberOfPatients.LastCalculatedDateTime
        /// </summary>
        virtual public System.DateTime? LastCalculatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastCalculatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastCalculatedDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeByNumberOfPatients.LastCalculatedByUserID
        /// </summary>
        virtual public System.String LastCalculatedByUserID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastCalculatedByUserID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastCalculatedByUserID, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeByNumberOfPatients.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to ParamedicFeeByNumberOfPatients.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esParamedicFeeByNumberOfPatients entity)
            {
                this.entity = entity;
            }


            public System.String RegistrationDate
            {
                get
                {
                    System.DateTime? data = entity.RegistrationDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationDate = null;
                    else entity.RegistrationDate = Convert.ToDateTime(value);
                }
            }

            public System.String ParamedicID
            {
                get
                {
                    System.String data = entity.ParamedicID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParamedicID = null;
                    else entity.ParamedicID = Convert.ToString(value);
                }
            }

            public System.String NumberOfPatient
            {
                get
                {
                    System.Int16? data = entity.NumberOfPatient;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.NumberOfPatient = null;
                    else entity.NumberOfPatient = Convert.ToInt16(value);
                }
            }

            public System.String FeeAmount
            {
                get
                {
                    System.Decimal? data = entity.FeeAmount;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FeeAmount = null;
                    else entity.FeeAmount = Convert.ToDecimal(value);
                }
            }

            public System.String VerificationNo
            {
                get
                {
                    System.String data = entity.VerificationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.VerificationNo = null;
                    else entity.VerificationNo = Convert.ToString(value);
                }
            }

            public System.String LastCalculatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.LastCalculatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastCalculatedDateTime = null;
                    else entity.LastCalculatedDateTime = Convert.ToDateTime(value);
                }
            }

            public System.String LastCalculatedByUserID
            {
                get
                {
                    System.String data = entity.LastCalculatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastCalculatedByUserID = null;
                    else entity.LastCalculatedByUserID = Convert.ToString(value);
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


            private esParamedicFeeByNumberOfPatients entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esParamedicFeeByNumberOfPatientsQuery query)
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
                throw new Exception("esParamedicFeeByNumberOfPatients can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    public partial class ParamedicFeeByNumberOfPatients : esParamedicFeeByNumberOfPatients
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
    abstract public class esParamedicFeeByNumberOfPatientsQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicFeeByNumberOfPatientsMetadata.Meta();
            }
        }


        public esQueryItem RegistrationDate
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.RegistrationDate, esSystemType.DateTime);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem NumberOfPatient
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.NumberOfPatient, esSystemType.Int16);
            }
        }

        public esQueryItem FeeAmount
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.FeeAmount, esSystemType.Decimal);
            }
        }

        public esQueryItem VerificationNo
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.VerificationNo, esSystemType.String);
            }
        }

        public esQueryItem LastCalculatedDateTime
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastCalculatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastCalculatedByUserID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastCalculatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ParamedicFeeByNumberOfPatientsCollection")]
    public partial class ParamedicFeeByNumberOfPatientsCollection : esParamedicFeeByNumberOfPatientsCollection, IEnumerable<ParamedicFeeByNumberOfPatients>
    {
        public ParamedicFeeByNumberOfPatientsCollection()
        {

        }

        public static implicit operator List<ParamedicFeeByNumberOfPatients>(ParamedicFeeByNumberOfPatientsCollection coll)
        {
            List<ParamedicFeeByNumberOfPatients> list = new List<ParamedicFeeByNumberOfPatients>();

            foreach (ParamedicFeeByNumberOfPatients emp in coll)
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
                return ParamedicFeeByNumberOfPatientsMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicFeeByNumberOfPatientsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ParamedicFeeByNumberOfPatients(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ParamedicFeeByNumberOfPatients();
        }


        #endregion


        [BrowsableAttribute(false)]
        public ParamedicFeeByNumberOfPatientsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicFeeByNumberOfPatientsQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(ParamedicFeeByNumberOfPatientsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public ParamedicFeeByNumberOfPatients AddNew()
        {
            ParamedicFeeByNumberOfPatients entity = base.AddNewEntity() as ParamedicFeeByNumberOfPatients;

            return entity;
        }

        public ParamedicFeeByNumberOfPatients FindByPrimaryKey(System.DateTime registrationDate, System.String paramedicID)
        {
            return base.FindByPrimaryKey(registrationDate, paramedicID) as ParamedicFeeByNumberOfPatients;
        }


        #region IEnumerable<ParamedicFeeByNumberOfPatients> Members

        IEnumerator<ParamedicFeeByNumberOfPatients> IEnumerable<ParamedicFeeByNumberOfPatients>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ParamedicFeeByNumberOfPatients;
            }
        }

        #endregion

        private ParamedicFeeByNumberOfPatientsQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ParamedicFeeByNumberOfPatients' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeByNumberOfPatients ({RegistrationDate},{ParamedicID})")]
    [Serializable]
    public partial class ParamedicFeeByNumberOfPatients : esParamedicFeeByNumberOfPatients
    {
        public ParamedicFeeByNumberOfPatients()
        {

        }

        public ParamedicFeeByNumberOfPatients(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ParamedicFeeByNumberOfPatientsMetadata.Meta();
            }
        }



        override protected esParamedicFeeByNumberOfPatientsQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ParamedicFeeByNumberOfPatientsQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public ParamedicFeeByNumberOfPatientsQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ParamedicFeeByNumberOfPatientsQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(ParamedicFeeByNumberOfPatientsQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ParamedicFeeByNumberOfPatientsQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class ParamedicFeeByNumberOfPatientsQuery : esParamedicFeeByNumberOfPatientsQuery
    {
        public ParamedicFeeByNumberOfPatientsQuery()
        {

        }

        public ParamedicFeeByNumberOfPatientsQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ParamedicFeeByNumberOfPatientsQuery";
        }


    }


    [Serializable]
    public partial class ParamedicFeeByNumberOfPatientsMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ParamedicFeeByNumberOfPatientsMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.RegistrationDate, 0, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.RegistrationDate;
            c.IsInPrimaryKey = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.ParamedicID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.NumberOfPatient, 2, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.NumberOfPatient;
            c.NumericPrecision = 5;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.FeeAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.FeeAmount;
            c.NumericPrecision = 18;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.VerificationNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.VerificationNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastCalculatedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.LastCalculatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastCalculatedByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.LastCalculatedByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ParamedicFeeByNumberOfPatientsMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = ParamedicFeeByNumberOfPatientsMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public ParamedicFeeByNumberOfPatientsMetadata Meta()
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
            public const string RegistrationDate = "RegistrationDate";
            public const string ParamedicID = "ParamedicID";
            public const string NumberOfPatient = "NumberOfPatient";
            public const string FeeAmount = "FeeAmount";
            public const string VerificationNo = "VerificationNo";
            public const string LastCalculatedDateTime = "LastCalculatedDateTime";
            public const string LastCalculatedByUserID = "LastCalculatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationDate = "RegistrationDate";
            public const string ParamedicID = "ParamedicID";
            public const string NumberOfPatient = "NumberOfPatient";
            public const string FeeAmount = "FeeAmount";
            public const string VerificationNo = "VerificationNo";
            public const string LastCalculatedDateTime = "LastCalculatedDateTime";
            public const string LastCalculatedByUserID = "LastCalculatedByUserID";
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
            lock (typeof(ParamedicFeeByNumberOfPatientsMetadata))
            {
                if (ParamedicFeeByNumberOfPatientsMetadata.mapDelegates == null)
                {
                    ParamedicFeeByNumberOfPatientsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ParamedicFeeByNumberOfPatientsMetadata.meta == null)
                {
                    ParamedicFeeByNumberOfPatientsMetadata.meta = new ParamedicFeeByNumberOfPatientsMetadata();
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


                meta.AddTypeMap("RegistrationDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("NumberOfPatient", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("FeeAmount", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastCalculatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastCalculatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));



                meta.Source = "ParamedicFeeByNumberOfPatients";
                meta.Destination = "ParamedicFeeByNumberOfPatients";

                meta.spInsert = "proc_ParamedicFeeByNumberOfPatientsInsert";
                meta.spUpdate = "proc_ParamedicFeeByNumberOfPatientsUpdate";
                meta.spDelete = "proc_ParamedicFeeByNumberOfPatientsDelete";
                meta.spLoadAll = "proc_ParamedicFeeByNumberOfPatientsLoadAll";
                meta.spLoadByPrimaryKey = "proc_ParamedicFeeByNumberOfPatientsLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ParamedicFeeByNumberOfPatientsMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
