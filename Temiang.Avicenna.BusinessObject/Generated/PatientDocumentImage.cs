/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/4/2016 10:55:39 AM
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
    abstract public class esPatientDocumentImageCollection : esEntityCollectionWAuditLog
    {
        public esPatientDocumentImageCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientDocumentImageCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientDocumentImageQuery query)
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
            this.InitQuery(query as esPatientDocumentImageQuery);
        }
        #endregion

        virtual public PatientDocumentImage DetachEntity(PatientDocumentImage entity)
        {
            return base.DetachEntity(entity) as PatientDocumentImage;
        }

        virtual public PatientDocumentImage AttachEntity(PatientDocumentImage entity)
        {
            return base.AttachEntity(entity) as PatientDocumentImage;
        }

        virtual public void Combine(PatientDocumentImageCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientDocumentImage this[int index]
        {
            get
            {
                return base[index] as PatientDocumentImage;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientDocumentImage);
        }
    }

    [Serializable]
    abstract public class esPatientDocumentImage : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientDocumentImageQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientDocumentImage()
        {
        }

        public esPatientDocumentImage(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientID, Int32 sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, Int32 sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String patientID, Int32 sequenceNo)
        {
            esPatientDocumentImageQuery query = this.GetDynamicQuery();
            query.Where(query.PatientID == patientID, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientID, Int32 sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientID", patientID);
            parms.Add("SequenceNo", sequenceNo);
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
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "SRImageTemplateType": this.str.SRImageTemplateType = (string)value; break;
                        case "ImageTemplateID": this.str.ImageTemplateID = (string)value; break;
                        case "Name": this.str.Name = (string)value; break;
                        case "Notes": this.str.Notes = (string)value; break;
                        case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
                        case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "SequenceNo":

                            if (value == null || value is System.Int32)
                                this.SequenceNo = (System.Int32?)value;
                            break;
                        case "Image":

                            if (value == null || value is System.Byte[])
                                this.Image = (System.Byte[])value;
                            break;
                        case "CreatedDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreatedDateTime = (System.DateTime?)value;
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
        /// Maps to PatientDocumentImage.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientDocumentImageMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientDocumentImageMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.SequenceNo
        /// </summary>
        virtual public System.Int32? SequenceNo
        {
            get
            {
                return base.GetSystemInt32(PatientDocumentImageMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemInt32(PatientDocumentImageMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PatientDocumentImageMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PatientDocumentImageMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.SRImageTemplateType
        /// </summary>
        virtual public System.String SRImageTemplateType
        {
            get
            {
                return base.GetSystemString(PatientDocumentImageMetadata.ColumnNames.SRImageTemplateType);
            }

            set
            {
                base.SetSystemString(PatientDocumentImageMetadata.ColumnNames.SRImageTemplateType, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.ImageTemplateID
        /// </summary>
        virtual public System.String ImageTemplateID
        {
            get
            {
                return base.GetSystemString(PatientDocumentImageMetadata.ColumnNames.ImageTemplateID);
            }

            set
            {
                base.SetSystemString(PatientDocumentImageMetadata.ColumnNames.ImageTemplateID, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.Name
        /// </summary>
        virtual public System.String Name
        {
            get
            {
                return base.GetSystemString(PatientDocumentImageMetadata.ColumnNames.Name);
            }

            set
            {
                base.SetSystemString(PatientDocumentImageMetadata.ColumnNames.Name, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.Notes
        /// </summary>
        virtual public System.String Notes
        {
            get
            {
                return base.GetSystemString(PatientDocumentImageMetadata.ColumnNames.Notes);
            }

            set
            {
                base.SetSystemString(PatientDocumentImageMetadata.ColumnNames.Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.Image
        /// </summary>
        virtual public System.Byte[] Image
        {
            get
            {
                return base.GetSystemByteArray(PatientDocumentImageMetadata.ColumnNames.Image);
            }

            set
            {
                base.SetSystemByteArray(PatientDocumentImageMetadata.ColumnNames.Image, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientDocumentImageMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientDocumentImageMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(PatientDocumentImageMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(PatientDocumentImageMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientDocumentImageMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientDocumentImageMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientDocumentImage.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientDocumentImageMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientDocumentImageMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPatientDocumentImage entity)
            {
                this.entity = entity;
            }
            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
                }
            }
            public System.String SequenceNo
            {
                get
                {
                    System.Int32? data = entity.SequenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SequenceNo = null;
                    else entity.SequenceNo = Convert.ToInt32(value);
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
            public System.String SRImageTemplateType
            {
                get
                {
                    System.String data = entity.SRImageTemplateType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRImageTemplateType = null;
                    else entity.SRImageTemplateType = Convert.ToString(value);
                }
            }
            public System.String ImageTemplateID
            {
                get
                {
                    System.String data = entity.ImageTemplateID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImageTemplateID = null;
                    else entity.ImageTemplateID = Convert.ToString(value);
                }
            }
            public System.String Name
            {
                get
                {
                    System.String data = entity.Name;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Name = null;
                    else entity.Name = Convert.ToString(value);
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
            public System.String CreatedDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreatedDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedDateTime = null;
                    else entity.CreatedDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String CreatedByUserID
            {
                get
                {
                    System.String data = entity.CreatedByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreatedByUserID = null;
                    else entity.CreatedByUserID = Convert.ToString(value);
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
            private esPatientDocumentImage entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientDocumentImageQuery query)
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
                throw new Exception("esPatientDocumentImage can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientDocumentImage : esPatientDocumentImage
    {
    }

    [Serializable]
    abstract public class esPatientDocumentImageQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientDocumentImageMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SRImageTemplateType
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.SRImageTemplateType, esSystemType.String);
            }
        }

        public esQueryItem ImageTemplateID
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.ImageTemplateID, esSystemType.String);
            }
        }

        public esQueryItem Name
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.Name, esSystemType.String);
            }
        }

        public esQueryItem Notes
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.Notes, esSystemType.String);
            }
        }

        public esQueryItem Image
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.Image, esSystemType.ByteArray);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientDocumentImageMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientDocumentImageCollection")]
    public partial class PatientDocumentImageCollection : esPatientDocumentImageCollection, IEnumerable<PatientDocumentImage>
    {
        public PatientDocumentImageCollection()
        {

        }

        public static implicit operator List<PatientDocumentImage>(PatientDocumentImageCollection coll)
        {
            List<PatientDocumentImage> list = new List<PatientDocumentImage>();

            foreach (PatientDocumentImage emp in coll)
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
                return PatientDocumentImageMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientDocumentImageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientDocumentImage(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientDocumentImage();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientDocumentImageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientDocumentImageQuery();
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
        public bool Load(PatientDocumentImageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientDocumentImage AddNew()
        {
            PatientDocumentImage entity = base.AddNewEntity() as PatientDocumentImage;

            return entity;
        }
        public PatientDocumentImage FindByPrimaryKey(String patientID, Int32 sequenceNo)
        {
            return base.FindByPrimaryKey(patientID, sequenceNo) as PatientDocumentImage;
        }

        #region IEnumerable< PatientDocumentImage> Members

        IEnumerator<PatientDocumentImage> IEnumerable<PatientDocumentImage>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientDocumentImage;
            }
        }

        #endregion

        private PatientDocumentImageQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientDocumentImage' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientDocumentImage ({PatientID, SequenceNo})")]
    [Serializable]
    public partial class PatientDocumentImage : esPatientDocumentImage
    {
        public PatientDocumentImage()
        {
        }

        public PatientDocumentImage(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientDocumentImageMetadata.Meta();
            }
        }

        override protected esPatientDocumentImageQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientDocumentImageQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientDocumentImageQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientDocumentImageQuery();
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
        public bool Load(PatientDocumentImageQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientDocumentImageQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientDocumentImageQuery : esPatientDocumentImageQuery
    {
        public PatientDocumentImageQuery()
        {

        }

        public PatientDocumentImageQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientDocumentImageQuery";
        }
    }

    [Serializable]
    public partial class PatientDocumentImageMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientDocumentImageMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.SequenceNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.SRImageTemplateType, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.SRImageTemplateType;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.ImageTemplateID, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.ImageTemplateID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.Name, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.Name;
            c.CharacterMaxLength = 500;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.Notes;
            c.CharacterMaxLength = -1;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.Image, 7, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.Image;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.CreatedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.CreatedByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientDocumentImageMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientDocumentImageMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientDocumentImageMetadata Meta()
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
            public const string PatientID = "PatientID";
            public const string SequenceNo = "SequenceNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string SRImageTemplateType = "SRImageTemplateType";
            public const string ImageTemplateID = "ImageTemplateID";
            public const string Name = "Name";
            public const string Notes = "Notes";
            public const string Image = "Image";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string SequenceNo = "SequenceNo";
            public const string RegistrationNo = "RegistrationNo";
            public const string SRImageTemplateType = "SRImageTemplateType";
            public const string ImageTemplateID = "ImageTemplateID";
            public const string Name = "Name";
            public const string Notes = "Notes";
            public const string Image = "Image";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
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
            lock (typeof(PatientDocumentImageMetadata))
            {
                if (PatientDocumentImageMetadata.mapDelegates == null)
                {
                    PatientDocumentImageMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientDocumentImageMetadata.meta == null)
                {
                    PatientDocumentImageMetadata.meta = new PatientDocumentImageMetadata();
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

                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRImageTemplateType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ImageTemplateID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Name", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Image", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientDocumentImage";
                meta.Destination = "PatientDocumentImage";
                meta.spInsert = "proc_PatientDocumentImageInsert";
                meta.spUpdate = "proc_PatientDocumentImageUpdate";
                meta.spDelete = "proc_PatientDocumentImageDelete";
                meta.spLoadAll = "proc_PatientDocumentImageLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientDocumentImageLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientDocumentImageMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
