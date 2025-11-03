/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/4/2016 10:04:31 PM
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
    abstract public class esEpisodeBodyDiagramCollection : esEntityCollectionWAuditLog
    {
        public esEpisodeBodyDiagramCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "EpisodeBodyDiagramCollection";
        }

        #region Query Logic
        protected void InitQuery(esEpisodeBodyDiagramQuery query)
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
            this.InitQuery(query as esEpisodeBodyDiagramQuery);
        }
        #endregion

        virtual public EpisodeBodyDiagram DetachEntity(EpisodeBodyDiagram entity)
        {
            return base.DetachEntity(entity) as EpisodeBodyDiagram;
        }

        virtual public EpisodeBodyDiagram AttachEntity(EpisodeBodyDiagram entity)
        {
            return base.AttachEntity(entity) as EpisodeBodyDiagram;
        }

        virtual public void Combine(EpisodeBodyDiagramCollection collection)
        {
            base.Combine(collection);
        }

        new public EpisodeBodyDiagram this[int index]
        {
            get
            {
                return base[index] as EpisodeBodyDiagram;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(EpisodeBodyDiagram);
        }
    }

    [Serializable]
    abstract public class esEpisodeBodyDiagram : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esEpisodeBodyDiagramQuery GetDynamicQuery()
        {
            return null;
        }

        public esEpisodeBodyDiagram()
        {
        }

        public esEpisodeBodyDiagram(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo, Int32 sequenceNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 sequenceNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 sequenceNo)
        {
            esEpisodeBodyDiagramQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 sequenceNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
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
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "SequenceNo": this.str.SequenceNo = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "IsDeleted": this.str.IsDeleted = (string)value; break;
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
                        case "BodyImage":

                            if (value == null || value is System.Byte[])
                                this.BodyImage = (System.Byte[])value;
                            break;
                        case "IsDeleted":

                            if (value == null || value is System.Boolean)
                                this.IsDeleted = (System.Boolean?)value;
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
        /// Maps to EpisodeBodyDiagram.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.SequenceNo
        /// </summary>
        virtual public System.Int32? SequenceNo
        {
            get
            {
                return base.GetSystemInt32(EpisodeBodyDiagramMetadata.ColumnNames.SequenceNo);
            }

            set
            {
                base.SetSystemInt32(EpisodeBodyDiagramMetadata.ColumnNames.SequenceNo, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.ParamedicID
        /// </summary>
        virtual public System.String ParamedicID
        {
            get
            {
                return base.GetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.ParamedicID);
            }

            set
            {
                base.SetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.ParamedicID, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.BodyImage
        /// </summary>
        virtual public System.Byte[] BodyImage
        {
            get
            {
                return base.GetSystemByteArray(EpisodeBodyDiagramMetadata.ColumnNames.BodyImage);
            }

            set
            {
                base.SetSystemByteArray(EpisodeBodyDiagramMetadata.ColumnNames.BodyImage, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.IsDeleted
        /// </summary>
        virtual public System.Boolean? IsDeleted
        {
            get
            {
                return base.GetSystemBoolean(EpisodeBodyDiagramMetadata.ColumnNames.IsDeleted);
            }

            set
            {
                base.SetSystemBoolean(EpisodeBodyDiagramMetadata.ColumnNames.IsDeleted, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.CreatedDateTime
        /// </summary>
        virtual public System.DateTime? CreatedDateTime
        {
            get
            {
                return base.GetSystemDateTime(EpisodeBodyDiagramMetadata.ColumnNames.CreatedDateTime);
            }

            set
            {
                base.SetSystemDateTime(EpisodeBodyDiagramMetadata.ColumnNames.CreatedDateTime, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.CreatedByUserID
        /// </summary>
        virtual public System.String CreatedByUserID
        {
            get
            {
                return base.GetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.CreatedByUserID);
            }

            set
            {
                base.SetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.CreatedByUserID, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(EpisodeBodyDiagramMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(EpisodeBodyDiagramMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to EpisodeBodyDiagram.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(EpisodeBodyDiagramMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esEpisodeBodyDiagram entity)
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
            public System.String IsDeleted
            {
                get
                {
                    System.Boolean? data = entity.IsDeleted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDeleted = null;
                    else entity.IsDeleted = Convert.ToBoolean(value);
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
            private esEpisodeBodyDiagram entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esEpisodeBodyDiagramQuery query)
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
                throw new Exception("esEpisodeBodyDiagram can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class EpisodeBodyDiagram : esEpisodeBodyDiagram
    {
    }

    [Serializable]
    abstract public class esEpisodeBodyDiagramQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return EpisodeBodyDiagramMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem SequenceNo
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.SequenceNo, esSystemType.Int32);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ParamedicID
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.ParamedicID, esSystemType.String);
            }
        }

        public esQueryItem BodyImage
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.BodyImage, esSystemType.ByteArray);
            }
        }

        public esQueryItem IsDeleted
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
            }
        }

        public esQueryItem CreatedDateTime
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem CreatedByUserID
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, EpisodeBodyDiagramMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("EpisodeBodyDiagramCollection")]
    public partial class EpisodeBodyDiagramCollection : esEpisodeBodyDiagramCollection, IEnumerable<EpisodeBodyDiagram>
    {
        public EpisodeBodyDiagramCollection()
        {

        }

        public static implicit operator List<EpisodeBodyDiagram>(EpisodeBodyDiagramCollection coll)
        {
            List<EpisodeBodyDiagram> list = new List<EpisodeBodyDiagram>();

            foreach (EpisodeBodyDiagram emp in coll)
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
                return EpisodeBodyDiagramMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EpisodeBodyDiagramQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new EpisodeBodyDiagram(row);
        }

        override protected esEntity CreateEntity()
        {
            return new EpisodeBodyDiagram();
        }

        #endregion

        [BrowsableAttribute(false)]
        public EpisodeBodyDiagramQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EpisodeBodyDiagramQuery();
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
        public bool Load(EpisodeBodyDiagramQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public EpisodeBodyDiagram AddNew()
        {
            EpisodeBodyDiagram entity = base.AddNewEntity() as EpisodeBodyDiagram;

            return entity;
        }
        public EpisodeBodyDiagram FindByPrimaryKey(String registrationNo, Int32 sequenceNo)
        {
            return base.FindByPrimaryKey(registrationNo, sequenceNo) as EpisodeBodyDiagram;
        }

        #region IEnumerable< EpisodeBodyDiagram> Members

        IEnumerator<EpisodeBodyDiagram> IEnumerable<EpisodeBodyDiagram>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as EpisodeBodyDiagram;
            }
        }

        #endregion

        private EpisodeBodyDiagramQuery query;
    }


    /// <summary>
    /// Encapsulates the 'EpisodeBodyDiagram' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("EpisodeBodyDiagram ({RegistrationNo, SequenceNo})")]
    [Serializable]
    public partial class EpisodeBodyDiagram : esEpisodeBodyDiagram
    {
        public EpisodeBodyDiagram()
        {
        }

        public EpisodeBodyDiagram(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return EpisodeBodyDiagramMetadata.Meta();
            }
        }

        override protected esEpisodeBodyDiagramQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new EpisodeBodyDiagramQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public EpisodeBodyDiagramQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new EpisodeBodyDiagramQuery();
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
        public bool Load(EpisodeBodyDiagramQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private EpisodeBodyDiagramQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class EpisodeBodyDiagramQuery : esEpisodeBodyDiagramQuery
    {
        public EpisodeBodyDiagramQuery()
        {

        }

        public EpisodeBodyDiagramQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "EpisodeBodyDiagramQuery";
        }
    }

    [Serializable]
    public partial class EpisodeBodyDiagramMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected EpisodeBodyDiagramMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.SequenceNo, 1, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.SequenceNo;
            c.IsInPrimaryKey = true;
            c.NumericPrecision = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.ServiceUnitID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.ParamedicID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.ParamedicID;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.BodyImage, 4, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.BodyImage;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.IsDeleted, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.IsDeleted;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.CreatedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.CreatedDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.CreatedByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.CreatedByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(EpisodeBodyDiagramMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = EpisodeBodyDiagramMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 40;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public EpisodeBodyDiagramMetadata Meta()
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
            public const string SequenceNo = "SequenceNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ParamedicID = "ParamedicID";
            public const string BodyImage = "BodyImage";
            public const string IsDeleted = "IsDeleted";
            public const string CreatedDateTime = "CreatedDateTime";
            public const string CreatedByUserID = "CreatedByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string SequenceNo = "SequenceNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ParamedicID = "ParamedicID";
            public const string BodyImage = "BodyImage";
            public const string IsDeleted = "IsDeleted";
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
            lock (typeof(EpisodeBodyDiagramMetadata))
            {
                if (EpisodeBodyDiagramMetadata.mapDelegates == null)
                {
                    EpisodeBodyDiagramMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (EpisodeBodyDiagramMetadata.meta == null)
                {
                    EpisodeBodyDiagramMetadata.meta = new EpisodeBodyDiagramMetadata();
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
                meta.AddTypeMap("SequenceNo", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("BodyImage", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "EpisodeBodyDiagram";
                meta.Destination = "EpisodeBodyDiagram";
                meta.spInsert = "proc_EpisodeBodyDiagramInsert";
                meta.spUpdate = "proc_EpisodeBodyDiagramUpdate";
                meta.spDelete = "proc_EpisodeBodyDiagramDelete";
                meta.spLoadAll = "proc_EpisodeBodyDiagramLoadAll";
                meta.spLoadByPrimaryKey = "proc_EpisodeBodyDiagramLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private EpisodeBodyDiagramMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
