/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/24/2016 11:22:08 AM
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
    abstract public class esMedicalRecordFileStatusCollection : esEntityCollectionWAuditLog
    {
        public esMedicalRecordFileStatusCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "MedicalRecordFileStatusCollection";
        }

        #region Query Logic
        protected void InitQuery(esMedicalRecordFileStatusQuery query)
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
            this.InitQuery(query as esMedicalRecordFileStatusQuery);
        }
        #endregion

        virtual public MedicalRecordFileStatus DetachEntity(MedicalRecordFileStatus entity)
        {
            return base.DetachEntity(entity) as MedicalRecordFileStatus;
        }

        virtual public MedicalRecordFileStatus AttachEntity(MedicalRecordFileStatus entity)
        {
            return base.AttachEntity(entity) as MedicalRecordFileStatus;
        }

        virtual public void Combine(MedicalRecordFileStatusCollection collection)
        {
            base.Combine(collection);
        }

        new public MedicalRecordFileStatus this[int index]
        {
            get
            {
                return base[index] as MedicalRecordFileStatus;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(MedicalRecordFileStatus);
        }
    }



    [Serializable]
    abstract public class esMedicalRecordFileStatus : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esMedicalRecordFileStatusQuery GetDynamicQuery()
        {
            return null;
        }

        public esMedicalRecordFileStatus()
        {

        }

        public esMedicalRecordFileStatus(DataRow row)
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
            esMedicalRecordFileStatusQuery query = this.GetDynamicQuery();
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
                        case "FileOutDate": this.str.FileOutDate = (string)value; break;
                        case "FileInDate": this.str.FileInDate = (string)value; break;
                        case "SRMedicalFileCategory": this.str.SRMedicalFileCategory = (string)value; break;
                        case "SRMedicalFileStatus": this.str.SRMedicalFileStatus = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "RequestByUserID": this.str.RequestByUserID = (string)value; break;
                        case "ReceiptByUserID": this.str.ReceiptByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "FileOutDateConfirmed": this.str.FileOutDateConfirmed = (string)value; break;
                        case "FileOutUserIDComfirmed": this.str.FileOutUserIDComfirmed = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "FileOutDate":

                            if (value == null || value is System.DateTime)
                                this.FileOutDate = (System.DateTime?)value;
                            break;

                        case "FileInDate":

                            if (value == null || value is System.DateTime)
                                this.FileInDate = (System.DateTime?)value;
                            break;

                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        case "FileOutDateConfirmed":

                            if (value == null || value is System.DateTime)
                                this.FileOutDateConfirmed = (System.DateTime?)value;
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
        /// Maps to MedicalRecordFileStatus.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.FileOutDate
        /// </summary>
        virtual public System.DateTime? FileOutDate
        {
            get
            {
                return base.GetSystemDateTime(MedicalRecordFileStatusMetadata.ColumnNames.FileOutDate);
            }

            set
            {
                base.SetSystemDateTime(MedicalRecordFileStatusMetadata.ColumnNames.FileOutDate, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.FileInDate
        /// </summary>
        virtual public System.DateTime? FileInDate
        {
            get
            {
                return base.GetSystemDateTime(MedicalRecordFileStatusMetadata.ColumnNames.FileInDate);
            }

            set
            {
                base.SetSystemDateTime(MedicalRecordFileStatusMetadata.ColumnNames.FileInDate, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.SRMedicalFileCategory
        /// </summary>
        virtual public System.String SRMedicalFileCategory
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.SRMedicalFileCategory);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.SRMedicalFileCategory, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.SRMedicalFileStatus
        /// </summary>
        virtual public System.String SRMedicalFileStatus
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.SRMedicalFileStatus);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.SRMedicalFileStatus, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.Notes, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.RequestByUserID
        /// </summary>
        virtual public System.String RequestByUserID
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.RequestByUserID);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.RequestByUserID, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.ReceiptByUserID
        /// </summary>
        virtual public System.String ReceiptByUserID
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.ReceiptByUserID);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.ReceiptByUserID, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(MedicalRecordFileStatusMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(MedicalRecordFileStatusMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.FileOutDateConfirmed
        /// </summary>
        virtual public System.DateTime? FileOutDateConfirmed
        {
            get
            {
                return base.GetSystemDateTime(MedicalRecordFileStatusMetadata.ColumnNames.FileOutDateConfirmed);
            }

            set
            {
                base.SetSystemDateTime(MedicalRecordFileStatusMetadata.ColumnNames.FileOutDateConfirmed, value);
            }
        }

        /// <summary>
        /// Maps to MedicalRecordFileStatus.FileOutUserIDComfirmed
        /// </summary>
        virtual public System.String FileOutUserIDComfirmed
        {
            get
            {
                return base.GetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.FileOutUserIDComfirmed);
            }

            set
            {
                base.SetSystemString(MedicalRecordFileStatusMetadata.ColumnNames.FileOutUserIDComfirmed, value);
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
            public esStrings(esMedicalRecordFileStatus entity)
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

            public System.String FileOutDate
            {
                get
                {
                    System.DateTime? data = entity.FileOutDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FileOutDate = null;
                    else entity.FileOutDate = Convert.ToDateTime(value);
                }
            }

            public System.String FileInDate
            {
                get
                {
                    System.DateTime? data = entity.FileInDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FileInDate = null;
                    else entity.FileInDate = Convert.ToDateTime(value);
                }
            }

            public System.String SRMedicalFileCategory
            {
                get
                {
                    System.String data = entity.SRMedicalFileCategory;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicalFileCategory = null;
                    else entity.SRMedicalFileCategory = Convert.ToString(value);
                }
            }

            public System.String SRMedicalFileStatus
            {
                get
                {
                    System.String data = entity.SRMedicalFileStatus;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRMedicalFileStatus = null;
                    else entity.SRMedicalFileStatus = Convert.ToString(value);
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

            public System.String RequestByUserID
            {
                get
                {
                    System.String data = entity.RequestByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RequestByUserID = null;
                    else entity.RequestByUserID = Convert.ToString(value);
                }
            }

            public System.String ReceiptByUserID
            {
                get
                {
                    System.String data = entity.ReceiptByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReceiptByUserID = null;
                    else entity.ReceiptByUserID = Convert.ToString(value);
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

            public System.String FileOutDateConfirmed
            {
                get
                {
                    System.DateTime? data = entity.FileOutDateConfirmed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FileOutDateConfirmed = null;
                    else entity.FileOutDateConfirmed = Convert.ToDateTime(value);
                }
            }

            public System.String FileOutUserIDComfirmed
            {
                get
                {
                    System.String data = entity.FileOutUserIDComfirmed;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FileOutUserIDComfirmed = null;
                    else entity.FileOutUserIDComfirmed = Convert.ToString(value);
                }
            }


            private esMedicalRecordFileStatus entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esMedicalRecordFileStatusQuery query)
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
                throw new Exception("esMedicalRecordFileStatus can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esMedicalRecordFileStatusQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return MedicalRecordFileStatusMetadata.Meta();
            }
        }


        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem FileOutDate
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.FileOutDate, esSystemType.DateTime);
            }
        }

        public esQueryItem FileInDate
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.FileInDate, esSystemType.DateTime);
            }
        }

        public esQueryItem SRMedicalFileCategory
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.SRMedicalFileCategory, esSystemType.String);
            }
        }

        public esQueryItem SRMedicalFileStatus
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.SRMedicalFileStatus, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem RequestByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.RequestByUserID, esSystemType.String);
            }
        }

        public esQueryItem ReceiptByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.ReceiptByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem FileOutDateConfirmed
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.FileOutDateConfirmed, esSystemType.DateTime);
            }
        }

        public esQueryItem FileOutUserIDComfirmed
        {
            get
            {
                return new esQueryItem(this, MedicalRecordFileStatusMetadata.ColumnNames.FileOutUserIDComfirmed, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("MedicalRecordFileStatusCollection")]
    public partial class MedicalRecordFileStatusCollection : esMedicalRecordFileStatusCollection, IEnumerable<MedicalRecordFileStatus>
    {
        public MedicalRecordFileStatusCollection()
        {

        }

        public static implicit operator List<MedicalRecordFileStatus>(MedicalRecordFileStatusCollection coll)
        {
            List<MedicalRecordFileStatus> list = new List<MedicalRecordFileStatus>();

            foreach (MedicalRecordFileStatus emp in coll)
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
                return MedicalRecordFileStatusMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalRecordFileStatusQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new MedicalRecordFileStatus(row);
        }

        override protected esEntity CreateEntity()
        {
            return new MedicalRecordFileStatus();
        }


        #endregion


        [BrowsableAttribute(false)]
        public MedicalRecordFileStatusQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalRecordFileStatusQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(MedicalRecordFileStatusQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public MedicalRecordFileStatus AddNew()
        {
            MedicalRecordFileStatus entity = base.AddNewEntity() as MedicalRecordFileStatus;

            return entity;
        }

        public MedicalRecordFileStatus FindByPrimaryKey(System.String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as MedicalRecordFileStatus;
        }


        #region IEnumerable<MedicalRecordFileStatus> Members

        IEnumerator<MedicalRecordFileStatus> IEnumerable<MedicalRecordFileStatus>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as MedicalRecordFileStatus;
            }
        }

        #endregion

        private MedicalRecordFileStatusQuery query;
    }


    /// <summary>
    /// Encapsulates the 'MedicalRecordFileStatus' table
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("MedicalRecordFileStatus ({RegistrationNo})")]
    [Serializable]
    public partial class MedicalRecordFileStatus : esMedicalRecordFileStatus
    {
        public MedicalRecordFileStatus()
        {

        }

        public MedicalRecordFileStatus(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return MedicalRecordFileStatusMetadata.Meta();
            }
        }



        override protected esMedicalRecordFileStatusQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new MedicalRecordFileStatusQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public MedicalRecordFileStatusQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new MedicalRecordFileStatusQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(MedicalRecordFileStatusQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private MedicalRecordFileStatusQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class MedicalRecordFileStatusQuery : esMedicalRecordFileStatusQuery
    {
        public MedicalRecordFileStatusQuery()
        {

        }

        public MedicalRecordFileStatusQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "MedicalRecordFileStatusQuery";
        }


    }


    [Serializable]
    public partial class MedicalRecordFileStatusMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected MedicalRecordFileStatusMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.FileOutDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.FileOutDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.FileInDate, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.FileInDate;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.SRMedicalFileCategory, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.SRMedicalFileCategory;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.SRMedicalFileStatus, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.SRMedicalFileStatus;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.RequestByUserID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.RequestByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.ReceiptByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.ReceiptByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.FileOutDateConfirmed, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.FileOutDateConfirmed;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(MedicalRecordFileStatusMetadata.ColumnNames.FileOutUserIDComfirmed, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalRecordFileStatusMetadata.PropertyNames.FileOutUserIDComfirmed;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public MedicalRecordFileStatusMetadata Meta()
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
            public const string FileOutDate = "FileOutDate";
            public const string FileInDate = "FileInDate";
            public const string SRMedicalFileCategory = "SRMedicalFileCategory";
            public const string SRMedicalFileStatus = "SRMedicalFileStatus";
            public const string Notes = "Notes";
            public const string RequestByUserID = "RequestByUserID";
            public const string ReceiptByUserID = "ReceiptByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string FileOutDateConfirmed = "FileOutDateConfirmed";
            public const string FileOutUserIDComfirmed = "FileOutUserIDComfirmed";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string FileOutDate = "FileOutDate";
            public const string FileInDate = "FileInDate";
            public const string SRMedicalFileCategory = "SRMedicalFileCategory";
            public const string SRMedicalFileStatus = "SRMedicalFileStatus";
            public const string Notes = "Notes";
            public const string RequestByUserID = "RequestByUserID";
            public const string ReceiptByUserID = "ReceiptByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string FileOutDateConfirmed = "FileOutDateConfirmed";
            public const string FileOutUserIDComfirmed = "FileOutUserIDComfirmed";
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
            lock (typeof(MedicalRecordFileStatusMetadata))
            {
                if (MedicalRecordFileStatusMetadata.mapDelegates == null)
                {
                    MedicalRecordFileStatusMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (MedicalRecordFileStatusMetadata.meta == null)
                {
                    MedicalRecordFileStatusMetadata.meta = new MedicalRecordFileStatusMetadata();
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
                meta.AddTypeMap("FileOutDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("FileInDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("SRMedicalFileCategory", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRMedicalFileStatus", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RequestByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReceiptByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FileOutDateConfirmed", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("FileOutUserIDComfirmed", new esTypeMap("varchar", "System.String"));



                meta.Source = "MedicalRecordFileStatus";
                meta.Destination = "MedicalRecordFileStatus";

                meta.spInsert = "proc_MedicalRecordFileStatusInsert";
                meta.spUpdate = "proc_MedicalRecordFileStatusUpdate";
                meta.spDelete = "proc_MedicalRecordFileStatusDelete";
                meta.spLoadAll = "proc_MedicalRecordFileStatusLoadAll";
                meta.spLoadByPrimaryKey = "proc_MedicalRecordFileStatusLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private MedicalRecordFileStatusMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
