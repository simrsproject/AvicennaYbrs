/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/24/2021 5:46:14 PM
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
	abstract public class esResearchLetterDocumentCollection : esEntityCollectionWAuditLog
	{
		public esResearchLetterDocumentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ResearchLetterDocumentCollection";
		}

		#region Query Logic
		protected void InitQuery(esResearchLetterDocumentQuery query)
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
			this.InitQuery(query as esResearchLetterDocumentQuery);
		}
		#endregion

		virtual public ResearchLetterDocument DetachEntity(ResearchLetterDocument entity)
		{
			return base.DetachEntity(entity) as ResearchLetterDocument;
		}

		virtual public ResearchLetterDocument AttachEntity(ResearchLetterDocument entity)
		{
			return base.AttachEntity(entity) as ResearchLetterDocument;
		}

		virtual public void Combine(ResearchLetterDocumentCollection collection)
		{
			base.Combine(collection);
		}

		new public ResearchLetterDocument this[int index]
		{
			get
			{
				return base[index] as ResearchLetterDocument;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ResearchLetterDocument);
		}
	}

	[Serializable]
	abstract public class esResearchLetterDocument : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esResearchLetterDocumentQuery GetDynamicQuery()
		{
			return null;
		}

		public esResearchLetterDocument()
		{
		}

		public esResearchLetterDocument(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 documentID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 documentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 documentID)
		{
			esResearchLetterDocumentQuery query = this.GetDynamicQuery();
			query.Where(query.DocumentID == documentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 documentID)
		{
			esParameters parms = new esParameters();
			parms.Add("DocumentID", documentID);
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
						case "DocumentID": this.str.DocumentID = (string)value; break;
						case "LetterID": this.str.LetterID = (string)value; break;
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
					}
				}
				else
				{
					switch (name)
					{
						case "DocumentID":

							if (value == null || value is System.Int64)
								this.DocumentID = (System.Int64?)value;
							break;
						case "LetterID":

							if (value == null || value is System.Int64)
								this.LetterID = (System.Int64?)value;
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
		/// Maps to ResearchLetterDocument.DocumentID
		/// </summary>
		virtual public System.Int64? DocumentID
		{
			get
			{
				return base.GetSystemInt64(ResearchLetterDocumentMetadata.ColumnNames.DocumentID);
			}

			set
			{
				base.SetSystemInt64(ResearchLetterDocumentMetadata.ColumnNames.DocumentID, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.LetterID
		/// </summary>
		virtual public System.Int64? LetterID
		{
			get
			{
				return base.GetSystemInt64(ResearchLetterDocumentMetadata.ColumnNames.LetterID);
			}

			set
			{
				base.SetSystemInt64(ResearchLetterDocumentMetadata.ColumnNames.LetterID, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.FileAttachName
		/// </summary>
		virtual public System.String FileAttachName
		{
			get
			{
				return base.GetSystemString(ResearchLetterDocumentMetadata.ColumnNames.FileAttachName);
			}

			set
			{
				base.SetSystemString(ResearchLetterDocumentMetadata.ColumnNames.FileAttachName, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(ResearchLetterDocumentMetadata.ColumnNames.DocumentName);
			}

			set
			{
				base.SetSystemString(ResearchLetterDocumentMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.DocumentDate
		/// </summary>
		virtual public System.DateTime? DocumentDate
		{
			get
			{
				return base.GetSystemDateTime(ResearchLetterDocumentMetadata.ColumnNames.DocumentDate);
			}

			set
			{
				base.SetSystemDateTime(ResearchLetterDocumentMetadata.ColumnNames.DocumentDate, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ResearchLetterDocumentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ResearchLetterDocumentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.SmallImage
		/// </summary>
		virtual public System.Byte[] SmallImage
		{
			get
			{
				return base.GetSystemByteArray(ResearchLetterDocumentMetadata.ColumnNames.SmallImage);
			}

			set
			{
				base.SetSystemByteArray(ResearchLetterDocumentMetadata.ColumnNames.SmallImage, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.OriFileName
		/// </summary>
		virtual public System.String OriFileName
		{
			get
			{
				return base.GetSystemString(ResearchLetterDocumentMetadata.ColumnNames.OriFileName);
			}

			set
			{
				base.SetSystemString(ResearchLetterDocumentMetadata.ColumnNames.OriFileName, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.OriPath
		/// </summary>
		virtual public System.String OriPath
		{
			get
			{
				return base.GetSystemString(ResearchLetterDocumentMetadata.ColumnNames.OriPath);
			}

			set
			{
				base.SetSystemString(ResearchLetterDocumentMetadata.ColumnNames.OriPath, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.IsUpload
		/// </summary>
		virtual public System.Boolean? IsUpload
		{
			get
			{
				return base.GetSystemBoolean(ResearchLetterDocumentMetadata.ColumnNames.IsUpload);
			}

			set
			{
				base.SetSystemBoolean(ResearchLetterDocumentMetadata.ColumnNames.IsUpload, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(ResearchLetterDocumentMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(ResearchLetterDocumentMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ResearchLetterDocumentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ResearchLetterDocumentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ResearchLetterDocument.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ResearchLetterDocumentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ResearchLetterDocumentMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esResearchLetterDocument entity)
			{
				this.entity = entity;
			}
			public System.String DocumentID
			{
				get
				{
					System.Int64? data = entity.DocumentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentID = null;
					else entity.DocumentID = Convert.ToInt64(value);
				}
			}
			public System.String LetterID
			{
				get
				{
					System.Int64? data = entity.LetterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LetterID = null;
					else entity.LetterID = Convert.ToInt64(value);
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
			private esResearchLetterDocument entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esResearchLetterDocumentQuery query)
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
				throw new Exception("esResearchLetterDocument can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ResearchLetterDocument : esResearchLetterDocument
	{
	}

	[Serializable]
	abstract public class esResearchLetterDocumentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ResearchLetterDocumentMetadata.Meta();
			}
		}

		public esQueryItem DocumentID
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.DocumentID, esSystemType.Int64);
			}
		}

		public esQueryItem LetterID
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.LetterID, esSystemType.Int64);
			}
		}

		public esQueryItem FileAttachName
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.FileAttachName, esSystemType.String);
			}
		}

		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		}

		public esQueryItem DocumentDate
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.DocumentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem SmallImage
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.SmallImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem OriFileName
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.OriFileName, esSystemType.String);
			}
		}

		public esQueryItem OriPath
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.OriPath, esSystemType.String);
			}
		}

		public esQueryItem IsUpload
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.IsUpload, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ResearchLetterDocumentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ResearchLetterDocumentCollection")]
	public partial class ResearchLetterDocumentCollection : esResearchLetterDocumentCollection, IEnumerable<ResearchLetterDocument>
	{
		public ResearchLetterDocumentCollection()
		{

		}

		public static implicit operator List<ResearchLetterDocument>(ResearchLetterDocumentCollection coll)
		{
			List<ResearchLetterDocument> list = new List<ResearchLetterDocument>();

			foreach (ResearchLetterDocument emp in coll)
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
				return ResearchLetterDocumentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ResearchLetterDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ResearchLetterDocument(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ResearchLetterDocument();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ResearchLetterDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ResearchLetterDocumentQuery();
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
		public bool Load(ResearchLetterDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ResearchLetterDocument AddNew()
		{
			ResearchLetterDocument entity = base.AddNewEntity() as ResearchLetterDocument;

			return entity;
		}
		public ResearchLetterDocument FindByPrimaryKey(Int64 documentID)
		{
			return base.FindByPrimaryKey(documentID) as ResearchLetterDocument;
		}

		#region IEnumerable< ResearchLetterDocument> Members

		IEnumerator<ResearchLetterDocument> IEnumerable<ResearchLetterDocument>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ResearchLetterDocument;
			}
		}

		#endregion

		private ResearchLetterDocumentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ResearchLetterDocument' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ResearchLetterDocument ({DocumentID})")]
	[Serializable]
	public partial class ResearchLetterDocument : esResearchLetterDocument
	{
		public ResearchLetterDocument()
		{
		}

		public ResearchLetterDocument(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ResearchLetterDocumentMetadata.Meta();
			}
		}

		override protected esResearchLetterDocumentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ResearchLetterDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ResearchLetterDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ResearchLetterDocumentQuery();
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
		public bool Load(ResearchLetterDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ResearchLetterDocumentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ResearchLetterDocumentQuery : esResearchLetterDocumentQuery
	{
		public ResearchLetterDocumentQuery()
		{

		}

		public ResearchLetterDocumentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ResearchLetterDocumentQuery";
		}
	}

	[Serializable]
	public partial class ResearchLetterDocumentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ResearchLetterDocumentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.DocumentID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.DocumentID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.LetterID, 1, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.LetterID;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.FileAttachName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.FileAttachName;
			c.CharacterMaxLength = 150;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.DocumentName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.DocumentDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.DocumentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.SmallImage, 6, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.SmallImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.OriFileName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.OriFileName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.OriPath, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.OriPath;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.IsUpload, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.IsUpload;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.IsDeleted, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ResearchLetterDocumentMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ResearchLetterDocumentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ResearchLetterDocumentMetadata Meta()
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
			public const string DocumentID = "DocumentID";
			public const string LetterID = "LetterID";
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
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string DocumentID = "DocumentID";
			public const string LetterID = "LetterID";
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
			lock (typeof(ResearchLetterDocumentMetadata))
			{
				if (ResearchLetterDocumentMetadata.mapDelegates == null)
				{
					ResearchLetterDocumentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ResearchLetterDocumentMetadata.meta == null)
				{
					ResearchLetterDocumentMetadata.meta = new ResearchLetterDocumentMetadata();
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

				meta.AddTypeMap("DocumentID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("LetterID", new esTypeMap("bigint", "System.Int64"));
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


				meta.Source = "ResearchLetterDocument";
				meta.Destination = "ResearchLetterDocument";
				meta.spInsert = "proc_ResearchLetterDocumentInsert";
				meta.spUpdate = "proc_ResearchLetterDocumentUpdate";
				meta.spDelete = "proc_ResearchLetterDocumentDelete";
				meta.spLoadAll = "proc_ResearchLetterDocumentLoadAll";
				meta.spLoadByPrimaryKey = "proc_ResearchLetterDocumentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ResearchLetterDocumentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
