/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/4/2016 8:37:09 AM
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
    abstract public class esImageTemplateCollection : esEntityCollectionWAuditLog
    {
        public esImageTemplateCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "ImageTemplateCollection";
        }

        #region Query Logic
        protected void InitQuery(esImageTemplateQuery query)
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
            this.InitQuery(query as esImageTemplateQuery);
        }
        #endregion

        virtual public ImageTemplate DetachEntity(ImageTemplate entity)
        {
            return base.DetachEntity(entity) as ImageTemplate;
        }

        virtual public ImageTemplate AttachEntity(ImageTemplate entity)
        {
            return base.AttachEntity(entity) as ImageTemplate;
        }

        virtual public void Combine(ImageTemplateCollection collection)
        {
            base.Combine(collection);
        }

        new public ImageTemplate this[int index]
        {
            get
            {
                return base[index] as ImageTemplate;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(ImageTemplate);
        }
    }

    [Serializable]
    abstract public class esImageTemplate : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esImageTemplateQuery GetDynamicQuery()
        {
            return null;
        }

        public esImageTemplate()
        {
        }

        public esImageTemplate(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String imageTemplateID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(imageTemplateID);
            else
                return LoadByPrimaryKeyStoredProcedure(imageTemplateID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String imageTemplateID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(imageTemplateID);
            else
                return LoadByPrimaryKeyStoredProcedure(imageTemplateID);
        }

        private bool LoadByPrimaryKeyDynamic(String imageTemplateID)
        {
            esImageTemplateQuery query = this.GetDynamicQuery();
            query.Where(query.ImageTemplateID == imageTemplateID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String imageTemplateID)
        {
            esParameters parms = new esParameters();
            parms.Add("ImageTemplateID", imageTemplateID);
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
                        case "ImageTemplateID": this.str.ImageTemplateID = (string)value; break;
                        case "ImageTemplateName": this.str.ImageTemplateName = (string)value; break;
                        case "SRImageTemplateType": this.str.SRImageTemplateType = (string)value; break;
                        case "Description": this.str.Description = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Image":

                            if (value == null || value is System.Byte[])
                                this.Image = (System.Byte[])value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to ImageTemplate.ImageTemplateID
        /// </summary>
        virtual public System.String ImageTemplateID
        {
            get
            {
                return base.GetSystemString(ImageTemplateMetadata.ColumnNames.ImageTemplateID);
            }

            set
            {
                base.SetSystemString(ImageTemplateMetadata.ColumnNames.ImageTemplateID, value);
            }
        }
        /// <summary>
        /// Maps to ImageTemplate.ImageTemplateName
        /// </summary>
        virtual public System.String ImageTemplateName
        {
            get
            {
                return base.GetSystemString(ImageTemplateMetadata.ColumnNames.ImageTemplateName);
            }

            set
            {
                base.SetSystemString(ImageTemplateMetadata.ColumnNames.ImageTemplateName, value);
            }
        }
        /// <summary>
        /// Maps to ImageTemplate.SRImageTemplateType
        /// </summary>
        virtual public System.String SRImageTemplateType
        {
            get
            {
                return base.GetSystemString(ImageTemplateMetadata.ColumnNames.SRImageTemplateType);
            }

            set
            {
                base.SetSystemString(ImageTemplateMetadata.ColumnNames.SRImageTemplateType, value);
            }
        }
        /// <summary>
        /// Maps to ImageTemplate.Description
        /// </summary>
        virtual public System.String Description
        {
            get
            {
                return base.GetSystemString(ImageTemplateMetadata.ColumnNames.Description);
            }

            set
            {
                base.SetSystemString(ImageTemplateMetadata.ColumnNames.Description, value);
            }
        }
        /// <summary>
        /// Maps to ImageTemplate.Image
        /// </summary>
        virtual public System.Byte[] Image
        {
            get
            {
                return base.GetSystemByteArray(ImageTemplateMetadata.ColumnNames.Image);
            }

            set
            {
                base.SetSystemByteArray(ImageTemplateMetadata.ColumnNames.Image, value);
            }
        }
        /// <summary>
        /// Maps to ImageTemplate.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(ImageTemplateMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(ImageTemplateMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to ImageTemplate.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(ImageTemplateMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(ImageTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to ImageTemplate.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(ImageTemplateMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(ImageTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esImageTemplate entity)
            {
                this.entity = entity;
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
            public System.String ImageTemplateName
            {
                get
                {
                    System.String data = entity.ImageTemplateName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ImageTemplateName = null;
                    else entity.ImageTemplateName = Convert.ToString(value);
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
            public System.String Description
            {
                get
                {
                    System.String data = entity.Description;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Description = null;
                    else entity.Description = Convert.ToString(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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
            private esImageTemplate entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esImageTemplateQuery query)
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
                throw new Exception("esImageTemplate can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class ImageTemplate : esImageTemplate
    {
    }

    [Serializable]
    abstract public class esImageTemplateQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return ImageTemplateMetadata.Meta();
            }
        }

        public esQueryItem ImageTemplateID
        {
            get
            {
                return new esQueryItem(this, ImageTemplateMetadata.ColumnNames.ImageTemplateID, esSystemType.String);
            }
        }

        public esQueryItem ImageTemplateName
        {
            get
            {
                return new esQueryItem(this, ImageTemplateMetadata.ColumnNames.ImageTemplateName, esSystemType.String);
            }
        }

        public esQueryItem SRImageTemplateType
        {
            get
            {
                return new esQueryItem(this, ImageTemplateMetadata.ColumnNames.SRImageTemplateType, esSystemType.String);
            }
        }

        public esQueryItem Description
        {
            get
            {
                return new esQueryItem(this, ImageTemplateMetadata.ColumnNames.Description, esSystemType.String);
            }
        }

        public esQueryItem Image
        {
            get
            {
                return new esQueryItem(this, ImageTemplateMetadata.ColumnNames.Image, esSystemType.ByteArray);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, ImageTemplateMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, ImageTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, ImageTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("ImageTemplateCollection")]
    public partial class ImageTemplateCollection : esImageTemplateCollection, IEnumerable<ImageTemplate>
    {
        public ImageTemplateCollection()
        {

        }

        public static implicit operator List<ImageTemplate>(ImageTemplateCollection coll)
        {
            List<ImageTemplate> list = new List<ImageTemplate>();

            foreach (ImageTemplate emp in coll)
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
                return ImageTemplateMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ImageTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new ImageTemplate(row);
        }

        override protected esEntity CreateEntity()
        {
            return new ImageTemplate();
        }

        #endregion

        [BrowsableAttribute(false)]
        public ImageTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ImageTemplateQuery();
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
        public bool Load(ImageTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public ImageTemplate AddNew()
        {
            ImageTemplate entity = base.AddNewEntity() as ImageTemplate;

            return entity;
        }
        public ImageTemplate FindByPrimaryKey(String imageTemplateID)
        {
            return base.FindByPrimaryKey(imageTemplateID) as ImageTemplate;
        }

        #region IEnumerable< ImageTemplate> Members

        IEnumerator<ImageTemplate> IEnumerable<ImageTemplate>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as ImageTemplate;
            }
        }

        #endregion

        private ImageTemplateQuery query;
    }


    /// <summary>
    /// Encapsulates the 'ImageTemplate' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ImageTemplate ({ImageTemplateID})")]
    [Serializable]
    public partial class ImageTemplate : esImageTemplate
    {
        public ImageTemplate()
        {
        }

        public ImageTemplate(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return ImageTemplateMetadata.Meta();
            }
        }

        override protected esImageTemplateQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new ImageTemplateQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public ImageTemplateQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new ImageTemplateQuery();
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
        public bool Load(ImageTemplateQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private ImageTemplateQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class ImageTemplateQuery : esImageTemplateQuery
    {
        public ImageTemplateQuery()
        {

        }

        public ImageTemplateQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "ImageTemplateQuery";
        }
    }

    [Serializable]
    public partial class ImageTemplateMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected ImageTemplateMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(ImageTemplateMetadata.ColumnNames.ImageTemplateID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = ImageTemplateMetadata.PropertyNames.ImageTemplateID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 10;
            _columns.Add(c);

            c = new esColumnMetadata(ImageTemplateMetadata.ColumnNames.ImageTemplateName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = ImageTemplateMetadata.PropertyNames.ImageTemplateName;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImageTemplateMetadata.ColumnNames.SRImageTemplateType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = ImageTemplateMetadata.PropertyNames.SRImageTemplateType;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImageTemplateMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = ImageTemplateMetadata.PropertyNames.Description;
            c.CharacterMaxLength = 1000;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImageTemplateMetadata.ColumnNames.Image, 4, typeof(System.Byte[]), esSystemType.ByteArray);
            c.PropertyName = ImageTemplateMetadata.PropertyNames.Image;
            c.NumericPrecision = 0;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImageTemplateMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = ImageTemplateMetadata.PropertyNames.IsActive;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImageTemplateMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = ImageTemplateMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(ImageTemplateMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = ImageTemplateMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public ImageTemplateMetadata Meta()
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
            public const string ImageTemplateID = "ImageTemplateID";
            public const string ImageTemplateName = "ImageTemplateName";
            public const string SRImageTemplateType = "SRImageTemplateType";
            public const string Description = "Description";
            public const string Image = "Image";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ImageTemplateID = "ImageTemplateID";
            public const string ImageTemplateName = "ImageTemplateName";
            public const string SRImageTemplateType = "SRImageTemplateType";
            public const string Description = "Description";
            public const string Image = "Image";
            public const string IsActive = "IsActive";
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
            lock (typeof(ImageTemplateMetadata))
            {
                if (ImageTemplateMetadata.mapDelegates == null)
                {
                    ImageTemplateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (ImageTemplateMetadata.meta == null)
                {
                    ImageTemplateMetadata.meta = new ImageTemplateMetadata();
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

                meta.AddTypeMap("ImageTemplateID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ImageTemplateName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRImageTemplateType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Image", new esTypeMap("image", "System.Byte[]"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "ImageTemplate";
                meta.Destination = "ImageTemplate";
                meta.spInsert = "proc_ImageTemplateInsert";
                meta.spUpdate = "proc_ImageTemplateUpdate";
                meta.spDelete = "proc_ImageTemplateDelete";
                meta.spLoadAll = "proc_ImageTemplateLoadAll";
                meta.spLoadByPrimaryKey = "proc_ImageTemplateLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private ImageTemplateMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
