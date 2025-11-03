/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/9/2025 5:20:15 PM
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
	abstract public class esCredentialProcessDocumentUploadCollection : esEntityCollectionWAuditLog
	{
		public esCredentialProcessDocumentUploadCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialProcessDocumentUploadCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialProcessDocumentUploadQuery query)
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
			this.InitQuery(query as esCredentialProcessDocumentUploadQuery);
		}
		#endregion

		virtual public CredentialProcessDocumentUpload DetachEntity(CredentialProcessDocumentUpload entity)
		{
			return base.DetachEntity(entity) as CredentialProcessDocumentUpload;
		}

		virtual public CredentialProcessDocumentUpload AttachEntity(CredentialProcessDocumentUpload entity)
		{
			return base.AttachEntity(entity) as CredentialProcessDocumentUpload;
		}

		virtual public void Combine(CredentialProcessDocumentUploadCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialProcessDocumentUpload this[int index]
		{
			get
			{
				return base[index] as CredentialProcessDocumentUpload;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialProcessDocumentUpload);
		}
	}

	[Serializable]
	abstract public class esCredentialProcessDocumentUpload : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialProcessDocumentUploadQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialProcessDocumentUpload()
		{
		}

		public esCredentialProcessDocumentUpload(DataRow row)
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
			esCredentialProcessDocumentUploadQuery query = this.GetDynamicQuery();
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "FileAttachName": this.str.FileAttachName = (string)value; break;
						case "DocumentName": this.str.DocumentName = (string)value; break;
						case "DocumentDate": this.str.DocumentDate = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "OriFileName": this.str.OriFileName = (string)value; break;
						case "OriPath": this.str.OriPath = (string)value; break;
						case "IsUpload": this.str.IsUpload = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "DocumentCode": this.str.DocumentCode = (string)value; break;
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
		/// Maps to CredentialProcessDocumentUpload.DocumentID
		/// </summary>
		virtual public System.Int64? DocumentID
		{
			get
			{
				return base.GetSystemInt64(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentID);
			}

			set
			{
				base.SetSystemInt64(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.FileAttachName
		/// </summary>
		virtual public System.String FileAttachName
		{
			get
			{
				return base.GetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.FileAttachName);
			}

			set
			{
				base.SetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.FileAttachName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentName);
			}

			set
			{
				base.SetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.DocumentDate
		/// </summary>
		virtual public System.DateTime? DocumentDate
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentDate);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentDate, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.SmallImage
		/// </summary>
		virtual public System.Byte[] SmallImage
		{
			get
			{
				return base.GetSystemByteArray(CredentialProcessDocumentUploadMetadata.ColumnNames.SmallImage);
			}

			set
			{
				base.SetSystemByteArray(CredentialProcessDocumentUploadMetadata.ColumnNames.SmallImage, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.OriFileName
		/// </summary>
		virtual public System.String OriFileName
		{
			get
			{
				return base.GetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.OriFileName);
			}

			set
			{
				base.SetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.OriFileName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.OriPath
		/// </summary>
		virtual public System.String OriPath
		{
			get
			{
				return base.GetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.OriPath);
			}

			set
			{
				base.SetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.OriPath, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.IsUpload
		/// </summary>
		virtual public System.Boolean? IsUpload
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessDocumentUploadMetadata.ColumnNames.IsUpload);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessDocumentUploadMetadata.ColumnNames.IsUpload, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(CredentialProcessDocumentUploadMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(CredentialProcessDocumentUploadMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessDocumentUpload.DocumentCode
		/// </summary>
		virtual public System.String DocumentCode
		{
			get
			{
				return base.GetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentCode);
			}

			set
			{
				base.SetSystemString(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentCode, value);
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
			public esStrings(esCredentialProcessDocumentUpload entity)
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
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
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
			private esCredentialProcessDocumentUpload entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialProcessDocumentUploadQuery query)
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
				throw new Exception("esCredentialProcessDocumentUpload can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialProcessDocumentUpload : esCredentialProcessDocumentUpload
	{
	}

	[Serializable]
	abstract public class esCredentialProcessDocumentUploadQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessDocumentUploadMetadata.Meta();
			}
		}

		public esQueryItem DocumentID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentID, esSystemType.Int64);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem FileAttachName
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.FileAttachName, esSystemType.String);
			}
		}

		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		}

		public esQueryItem DocumentDate
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem SmallImage
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.SmallImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem OriFileName
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.OriFileName, esSystemType.String);
			}
		}

		public esQueryItem OriPath
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.OriPath, esSystemType.String);
			}
		}

		public esQueryItem IsUpload
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.IsUpload, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem DocumentCode
		{
			get
			{
				return new esQueryItem(this, CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentCode, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialProcessDocumentUploadCollection")]
	public partial class CredentialProcessDocumentUploadCollection : esCredentialProcessDocumentUploadCollection, IEnumerable<CredentialProcessDocumentUpload>
	{
		public CredentialProcessDocumentUploadCollection()
		{

		}

		public static implicit operator List<CredentialProcessDocumentUpload>(CredentialProcessDocumentUploadCollection coll)
		{
			List<CredentialProcessDocumentUpload> list = new List<CredentialProcessDocumentUpload>();

			foreach (CredentialProcessDocumentUpload emp in coll)
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
				return CredentialProcessDocumentUploadMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessDocumentUploadQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialProcessDocumentUpload(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialProcessDocumentUpload();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessDocumentUploadQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessDocumentUploadQuery();
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
		public bool Load(CredentialProcessDocumentUploadQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialProcessDocumentUpload AddNew()
		{
			CredentialProcessDocumentUpload entity = base.AddNewEntity() as CredentialProcessDocumentUpload;

			return entity;
		}
		public CredentialProcessDocumentUpload FindByPrimaryKey(Int64 documentID)
		{
			return base.FindByPrimaryKey(documentID) as CredentialProcessDocumentUpload;
		}

		#region IEnumerable< CredentialProcessDocumentUpload> Members

		IEnumerator<CredentialProcessDocumentUpload> IEnumerable<CredentialProcessDocumentUpload>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialProcessDocumentUpload;
			}
		}

		#endregion

		private CredentialProcessDocumentUploadQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialProcessDocumentUpload' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialProcessDocumentUpload ({DocumentID})")]
	[Serializable]
	public partial class CredentialProcessDocumentUpload : esCredentialProcessDocumentUpload
	{
		public CredentialProcessDocumentUpload()
		{
		}

		public CredentialProcessDocumentUpload(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessDocumentUploadMetadata.Meta();
			}
		}

		override protected esCredentialProcessDocumentUploadQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessDocumentUploadQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessDocumentUploadQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessDocumentUploadQuery();
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
		public bool Load(CredentialProcessDocumentUploadQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialProcessDocumentUploadQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialProcessDocumentUploadQuery : esCredentialProcessDocumentUploadQuery
	{
		public CredentialProcessDocumentUploadQuery()
		{

		}

		public CredentialProcessDocumentUploadQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialProcessDocumentUploadQuery";
		}
	}

	[Serializable]
	public partial class CredentialProcessDocumentUploadMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialProcessDocumentUploadMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.DocumentID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.FileAttachName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.FileAttachName;
			c.CharacterMaxLength = 150;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.DocumentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.SmallImage, 6, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.SmallImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.OriFileName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.OriFileName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.OriPath, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.OriPath;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.IsUpload, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.IsUpload;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.IsDeleted, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessDocumentUploadMetadata.ColumnNames.DocumentCode, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessDocumentUploadMetadata.PropertyNames.DocumentCode;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialProcessDocumentUploadMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string FileAttachName = "FileAttachName";
			public const string DocumentName = "DocumentName";
			public const string DocumentDate = "DocumentDate";
			public const string Notes = "Notes";
			public const string SmallImage = "SmallImage";
			public const string OriFileName = "OriFileName";
			public const string OriPath = "OriPath";
			public const string IsUpload = "IsUpload";
			public const string IsDeleted = "IsDeleted";
			public const string DocumentCode = "DocumentCode";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string DocumentID = "DocumentID";
			public const string TransactionNo = "TransactionNo";
			public const string FileAttachName = "FileAttachName";
			public const string DocumentName = "DocumentName";
			public const string DocumentDate = "DocumentDate";
			public const string Notes = "Notes";
			public const string SmallImage = "SmallImage";
			public const string OriFileName = "OriFileName";
			public const string OriPath = "OriPath";
			public const string IsUpload = "IsUpload";
			public const string IsDeleted = "IsDeleted";
			public const string DocumentCode = "DocumentCode";
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
			lock (typeof(CredentialProcessDocumentUploadMetadata))
			{
				if (CredentialProcessDocumentUploadMetadata.mapDelegates == null)
				{
					CredentialProcessDocumentUploadMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialProcessDocumentUploadMetadata.meta == null)
				{
					CredentialProcessDocumentUploadMetadata.meta = new CredentialProcessDocumentUploadMetadata();
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
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FileAttachName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmallImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("OriFileName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriPath", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUpload", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DocumentCode", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialProcessDocumentUpload";
				meta.Destination = "CredentialProcessDocumentUpload";
				meta.spInsert = "proc_CredentialProcessDocumentUploadInsert";
				meta.spUpdate = "proc_CredentialProcessDocumentUploadUpdate";
				meta.spDelete = "proc_CredentialProcessDocumentUploadDelete";
				meta.spLoadAll = "proc_CredentialProcessDocumentUploadLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialProcessDocumentUploadLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialProcessDocumentUploadMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
