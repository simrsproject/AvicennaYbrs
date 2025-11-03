/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/2/2022 10:30:33 AM
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
	abstract public class esPersonalDocumentCollection : esEntityCollectionWAuditLog
	{
		public esPersonalDocumentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalDocumentCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalDocumentQuery query)
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
			this.InitQuery(query as esPersonalDocumentQuery);
		}
		#endregion

		virtual public PersonalDocument DetachEntity(PersonalDocument entity)
		{
			return base.DetachEntity(entity) as PersonalDocument;
		}

		virtual public PersonalDocument AttachEntity(PersonalDocument entity)
		{
			return base.AttachEntity(entity) as PersonalDocument;
		}

		virtual public void Combine(PersonalDocumentCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalDocument this[int index]
		{
			get
			{
				return base[index] as PersonalDocument;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalDocument);
		}
	}

	[Serializable]
	abstract public class esPersonalDocument : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalDocumentQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalDocument()
		{
		}

		public esPersonalDocument(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 personalDocumentID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalDocumentID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalDocumentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 personalDocumentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalDocumentID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalDocumentID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 personalDocumentID)
		{
			esPersonalDocumentQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalDocumentID == personalDocumentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 personalDocumentID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalDocumentID", personalDocumentID);
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
						case "PersonalDocumentID": this.str.PersonalDocumentID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "FileAttachName": this.str.FileAttachName = (string)value; break;
						case "DocumentName": this.str.DocumentName = (string)value; break;
						case "DocumentDate": this.str.DocumentDate = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "OriFileName": this.str.OriFileName = (string)value; break;
						case "OriPath": this.str.OriPath = (string)value; break;
						case "IsUpload": this.str.IsUpload = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DocumentCode": this.str.DocumentCode = (string)value; break;
						case "RefferenceID": this.str.RefferenceID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalDocumentID":

							if (value == null || value is System.Int64)
								this.PersonalDocumentID = (System.Int64?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "DocumentDate":

							if (value == null || value is System.DateTime)
								this.DocumentDate = (System.DateTime?)value;
							break;
						case "SmallImage":

							if (value == null || value is System.Byte[])
								this.SmallImage = (System.Byte[])value;
							break;
						case "IsUpload":

							if (value == null || value is System.Boolean)
								this.IsUpload = (System.Boolean?)value;
							break;
						case "IsDeleted":

							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "RefferenceID":

							if (value == null || value is System.Int32)
								this.RefferenceID = (System.Int32?)value;
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
		/// Maps to PersonalDocument.PersonalDocumentID
		/// </summary>
		virtual public System.Int64? PersonalDocumentID
		{
			get
			{
				return base.GetSystemInt64(PersonalDocumentMetadata.ColumnNames.PersonalDocumentID);
			}

			set
			{
				base.SetSystemInt64(PersonalDocumentMetadata.ColumnNames.PersonalDocumentID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalDocumentMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalDocumentMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.FileAttachName
		/// </summary>
		virtual public System.String FileAttachName
		{
			get
			{
				return base.GetSystemString(PersonalDocumentMetadata.ColumnNames.FileAttachName);
			}

			set
			{
				base.SetSystemString(PersonalDocumentMetadata.ColumnNames.FileAttachName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(PersonalDocumentMetadata.ColumnNames.DocumentName);
			}

			set
			{
				base.SetSystemString(PersonalDocumentMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.DocumentDate
		/// </summary>
		virtual public System.DateTime? DocumentDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalDocumentMetadata.ColumnNames.DocumentDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalDocumentMetadata.ColumnNames.DocumentDate, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PersonalDocumentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PersonalDocumentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.SmallImage
		/// </summary>
		virtual public System.Byte[] SmallImage
		{
			get
			{
				return base.GetSystemByteArray(PersonalDocumentMetadata.ColumnNames.SmallImage);
			}

			set
			{
				base.SetSystemByteArray(PersonalDocumentMetadata.ColumnNames.SmallImage, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.OriFileName
		/// </summary>
		virtual public System.String OriFileName
		{
			get
			{
				return base.GetSystemString(PersonalDocumentMetadata.ColumnNames.OriFileName);
			}

			set
			{
				base.SetSystemString(PersonalDocumentMetadata.ColumnNames.OriFileName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.OriPath
		/// </summary>
		virtual public System.String OriPath
		{
			get
			{
				return base.GetSystemString(PersonalDocumentMetadata.ColumnNames.OriPath);
			}

			set
			{
				base.SetSystemString(PersonalDocumentMetadata.ColumnNames.OriPath, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.IsUpload
		/// </summary>
		virtual public System.Boolean? IsUpload
		{
			get
			{
				return base.GetSystemBoolean(PersonalDocumentMetadata.ColumnNames.IsUpload);
			}

			set
			{
				base.SetSystemBoolean(PersonalDocumentMetadata.ColumnNames.IsUpload, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(PersonalDocumentMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(PersonalDocumentMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalDocumentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalDocumentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalDocumentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalDocumentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.DocumentCode
		/// </summary>
		virtual public System.String DocumentCode
		{
			get
			{
				return base.GetSystemString(PersonalDocumentMetadata.ColumnNames.DocumentCode);
			}

			set
			{
				base.SetSystemString(PersonalDocumentMetadata.ColumnNames.DocumentCode, value);
			}
		}
		/// <summary>
		/// Maps to PersonalDocument.RefferenceID
		/// </summary>
		virtual public System.Int32? RefferenceID
		{
			get
			{
				return base.GetSystemInt32(PersonalDocumentMetadata.ColumnNames.RefferenceID);
			}

			set
			{
				base.SetSystemInt32(PersonalDocumentMetadata.ColumnNames.RefferenceID, value);
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
			public esStrings(esPersonalDocument entity)
			{
				this.entity = entity;
			}
			public System.String PersonalDocumentID
			{
				get
				{
					System.Int64? data = entity.PersonalDocumentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalDocumentID = null;
					else entity.PersonalDocumentID = Convert.ToInt64(value);
				}
			}
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String FileAttachName
			{
				get
				{
					System.String data = entity.FileAttachName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FileAttachName = null;
					else entity.FileAttachName = Convert.ToString(value);
				}
			}
			public System.String DocumentName
			{
				get
				{
					System.String data = entity.DocumentName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentName = null;
					else entity.DocumentName = Convert.ToString(value);
				}
			}
			public System.String DocumentDate
			{
				get
				{
					System.DateTime? data = entity.DocumentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentDate = null;
					else entity.DocumentDate = Convert.ToDateTime(value);
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
			public System.String OriFileName
			{
				get
				{
					System.String data = entity.OriFileName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriFileName = null;
					else entity.OriFileName = Convert.ToString(value);
				}
			}
			public System.String OriPath
			{
				get
				{
					System.String data = entity.OriPath;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OriPath = null;
					else entity.OriPath = Convert.ToString(value);
				}
			}
			public System.String IsUpload
			{
				get
				{
					System.Boolean? data = entity.IsUpload;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUpload = null;
					else entity.IsUpload = Convert.ToBoolean(value);
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
			public System.String DocumentCode
			{
				get
				{
					System.String data = entity.DocumentCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentCode = null;
					else entity.DocumentCode = Convert.ToString(value);
				}
			}
			public System.String RefferenceID
			{
				get
				{
					System.Int32? data = entity.RefferenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RefferenceID = null;
					else entity.RefferenceID = Convert.ToInt32(value);
				}
			}
			private esPersonalDocument entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalDocumentQuery query)
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
				throw new Exception("esPersonalDocument can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalDocument : esPersonalDocument
	{
	}

	[Serializable]
	abstract public class esPersonalDocumentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalDocumentMetadata.Meta();
			}
		}

		public esQueryItem PersonalDocumentID
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.PersonalDocumentID, esSystemType.Int64);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem FileAttachName
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.FileAttachName, esSystemType.String);
			}
		}

		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		}

		public esQueryItem DocumentDate
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.DocumentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem SmallImage
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.SmallImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem OriFileName
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.OriFileName, esSystemType.String);
			}
		}

		public esQueryItem OriPath
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.OriPath, esSystemType.String);
			}
		}

		public esQueryItem IsUpload
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.IsUpload, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem DocumentCode
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.DocumentCode, esSystemType.String);
			}
		}

		public esQueryItem RefferenceID
		{
			get
			{
				return new esQueryItem(this, PersonalDocumentMetadata.ColumnNames.RefferenceID, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalDocumentCollection")]
	public partial class PersonalDocumentCollection : esPersonalDocumentCollection, IEnumerable<PersonalDocument>
	{
		public PersonalDocumentCollection()
		{

		}

		public static implicit operator List<PersonalDocument>(PersonalDocumentCollection coll)
		{
			List<PersonalDocument> list = new List<PersonalDocument>();

			foreach (PersonalDocument emp in coll)
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
				return PersonalDocumentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalDocument(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalDocument();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalDocumentQuery();
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
		public bool Load(PersonalDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalDocument AddNew()
		{
			PersonalDocument entity = base.AddNewEntity() as PersonalDocument;

			return entity;
		}
		public PersonalDocument FindByPrimaryKey(Int64 personalDocumentID)
		{
			return base.FindByPrimaryKey(personalDocumentID) as PersonalDocument;
		}

		#region IEnumerable< PersonalDocument> Members

		IEnumerator<PersonalDocument> IEnumerable<PersonalDocument>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalDocument;
			}
		}

		#endregion

		private PersonalDocumentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalDocument' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalDocument ({PersonalDocumentID})")]
	[Serializable]
	public partial class PersonalDocument : esPersonalDocument
	{
		public PersonalDocument()
		{
		}

		public PersonalDocument(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalDocumentMetadata.Meta();
			}
		}

		override protected esPersonalDocumentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalDocumentQuery();
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
		public bool Load(PersonalDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalDocumentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalDocumentQuery : esPersonalDocumentQuery
	{
		public PersonalDocumentQuery()
		{

		}

		public PersonalDocumentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalDocumentQuery";
		}
	}

	[Serializable]
	public partial class PersonalDocumentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalDocumentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.PersonalDocumentID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.PersonalDocumentID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.FileAttachName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.FileAttachName;
			c.CharacterMaxLength = 150;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.DocumentName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.DocumentDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.DocumentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.SmallImage, 6, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.SmallImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.OriFileName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.OriFileName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.OriPath, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.OriPath;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.IsUpload, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.IsUpload;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.IsDeleted, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.DocumentCode, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.DocumentCode;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalDocumentMetadata.ColumnNames.RefferenceID, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalDocumentMetadata.PropertyNames.RefferenceID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalDocumentMetadata Meta()
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
			public const string PersonalDocumentID = "PersonalDocumentID";
			public const string PersonID = "PersonID";
			public const string FileAttachName = "FileAttachName";
			public const string DocumentName = "DocumentName";
			public const string DocumentDate = "DocumentDate";
			public const string Notes = "Notes";
			public const string SmallImage = "SmallImage";
			public const string OriFileName = "OriFileName";
			public const string OriPath = "OriPath";
			public const string IsUpload = "IsUpload";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DocumentCode = "DocumentCode";
			public const string RefferenceID = "RefferenceID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalDocumentID = "PersonalDocumentID";
			public const string PersonID = "PersonID";
			public const string FileAttachName = "FileAttachName";
			public const string DocumentName = "DocumentName";
			public const string DocumentDate = "DocumentDate";
			public const string Notes = "Notes";
			public const string SmallImage = "SmallImage";
			public const string OriFileName = "OriFileName";
			public const string OriPath = "OriPath";
			public const string IsUpload = "IsUpload";
			public const string IsDeleted = "IsDeleted";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DocumentCode = "DocumentCode";
			public const string RefferenceID = "RefferenceID";
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
			lock (typeof(PersonalDocumentMetadata))
			{
				if (PersonalDocumentMetadata.mapDelegates == null)
				{
					PersonalDocumentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalDocumentMetadata.meta == null)
				{
					PersonalDocumentMetadata.meta = new PersonalDocumentMetadata();
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

				meta.AddTypeMap("PersonalDocumentID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FileAttachName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmallImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("OriFileName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriPath", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUpload", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("RefferenceID", new esTypeMap("int", "System.Int32"));


				meta.Source = "PersonalDocument";
				meta.Destination = "PersonalDocument";
				meta.spInsert = "proc_PersonalDocumentInsert";
				meta.spUpdate = "proc_PersonalDocumentUpdate";
				meta.spDelete = "proc_PersonalDocumentDelete";
				meta.spLoadAll = "proc_PersonalDocumentLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalDocumentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalDocumentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
