/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/23/2022 8:56:30 PM
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
	abstract public class esEmployeeSafetyCultureIncidentReportsDocumentCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeSafetyCultureIncidentReportsDocumentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeSafetyCultureIncidentReportsDocumentCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsDocumentQuery query)
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
			this.InitQuery(query as esEmployeeSafetyCultureIncidentReportsDocumentQuery);
		}
		#endregion

		virtual public EmployeeSafetyCultureIncidentReportsDocument DetachEntity(EmployeeSafetyCultureIncidentReportsDocument entity)
		{
			return base.DetachEntity(entity) as EmployeeSafetyCultureIncidentReportsDocument;
		}

		virtual public EmployeeSafetyCultureIncidentReportsDocument AttachEntity(EmployeeSafetyCultureIncidentReportsDocument entity)
		{
			return base.AttachEntity(entity) as EmployeeSafetyCultureIncidentReportsDocument;
		}

		virtual public void Combine(EmployeeSafetyCultureIncidentReportsDocumentCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeSafetyCultureIncidentReportsDocument this[int index]
		{
			get
			{
				return base[index] as EmployeeSafetyCultureIncidentReportsDocument;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeSafetyCultureIncidentReportsDocument);
		}
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsDocument : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeSafetyCultureIncidentReportsDocumentQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeSafetyCultureIncidentReportsDocument()
		{
		}

		public esEmployeeSafetyCultureIncidentReportsDocument(DataRow row)
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
			esEmployeeSafetyCultureIncidentReportsDocumentQuery query = this.GetDynamicQuery();
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
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.DocumentID
		/// </summary>
		virtual public System.Int64? DocumentID
		{
			get
			{
				return base.GetSystemInt64(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentID);
			}

			set
			{
				base.SetSystemInt64(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.FileAttachName
		/// </summary>
		virtual public System.String FileAttachName
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.FileAttachName);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.FileAttachName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentName);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.DocumentDate
		/// </summary>
		virtual public System.DateTime? DocumentDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.SmallImage
		/// </summary>
		virtual public System.Byte[] SmallImage
		{
			get
			{
				return base.GetSystemByteArray(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.SmallImage);
			}

			set
			{
				base.SetSystemByteArray(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.SmallImage, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.OriFileName
		/// </summary>
		virtual public System.String OriFileName
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.OriFileName);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.OriFileName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.OriPath
		/// </summary>
		virtual public System.String OriPath
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.OriPath);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.OriPath, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.IsUpload
		/// </summary>
		virtual public System.Boolean? IsUpload
		{
			get
			{
				return base.GetSystemBoolean(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.IsUpload);
			}

			set
			{
				base.SetSystemBoolean(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.IsUpload, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsDocument.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.IsDeleted, value);
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
			public esStrings(esEmployeeSafetyCultureIncidentReportsDocument entity)
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
			private esEmployeeSafetyCultureIncidentReportsDocument entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsDocumentQuery query)
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
				throw new Exception("esEmployeeSafetyCultureIncidentReportsDocument can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeSafetyCultureIncidentReportsDocument : esEmployeeSafetyCultureIncidentReportsDocument
	{
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsDocumentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsDocumentMetadata.Meta();
			}
		}

		public esQueryItem DocumentID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentID, esSystemType.Int64);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem FileAttachName
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.FileAttachName, esSystemType.String);
			}
		}

		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		}

		public esQueryItem DocumentDate
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem SmallImage
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.SmallImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem OriFileName
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.OriFileName, esSystemType.String);
			}
		}

		public esQueryItem OriPath
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.OriPath, esSystemType.String);
			}
		}

		public esQueryItem IsUpload
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.IsUpload, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeSafetyCultureIncidentReportsDocumentCollection")]
	public partial class EmployeeSafetyCultureIncidentReportsDocumentCollection : esEmployeeSafetyCultureIncidentReportsDocumentCollection, IEnumerable<EmployeeSafetyCultureIncidentReportsDocument>
	{
		public EmployeeSafetyCultureIncidentReportsDocumentCollection()
		{

		}

		public static implicit operator List<EmployeeSafetyCultureIncidentReportsDocument>(EmployeeSafetyCultureIncidentReportsDocumentCollection coll)
		{
			List<EmployeeSafetyCultureIncidentReportsDocument> list = new List<EmployeeSafetyCultureIncidentReportsDocument>();

			foreach (EmployeeSafetyCultureIncidentReportsDocument emp in coll)
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
				return EmployeeSafetyCultureIncidentReportsDocumentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeSafetyCultureIncidentReportsDocument(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeSafetyCultureIncidentReportsDocument();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsDocumentQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeSafetyCultureIncidentReportsDocument AddNew()
		{
			EmployeeSafetyCultureIncidentReportsDocument entity = base.AddNewEntity() as EmployeeSafetyCultureIncidentReportsDocument;

			return entity;
		}
		public EmployeeSafetyCultureIncidentReportsDocument FindByPrimaryKey(Int64 documentID)
		{
			return base.FindByPrimaryKey(documentID) as EmployeeSafetyCultureIncidentReportsDocument;
		}

		#region IEnumerable< EmployeeSafetyCultureIncidentReportsDocument> Members

		IEnumerator<EmployeeSafetyCultureIncidentReportsDocument> IEnumerable<EmployeeSafetyCultureIncidentReportsDocument>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeSafetyCultureIncidentReportsDocument;
			}
		}

		#endregion

		private EmployeeSafetyCultureIncidentReportsDocumentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeSafetyCultureIncidentReportsDocument' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeSafetyCultureIncidentReportsDocument ({DocumentID})")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsDocument : esEmployeeSafetyCultureIncidentReportsDocument
	{
		public EmployeeSafetyCultureIncidentReportsDocument()
		{
		}

		public EmployeeSafetyCultureIncidentReportsDocument(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsDocumentMetadata.Meta();
			}
		}

		override protected esEmployeeSafetyCultureIncidentReportsDocumentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsDocumentQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeSafetyCultureIncidentReportsDocumentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsDocumentQuery : esEmployeeSafetyCultureIncidentReportsDocumentQuery
	{
		public EmployeeSafetyCultureIncidentReportsDocumentQuery()
		{

		}

		public EmployeeSafetyCultureIncidentReportsDocumentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeSafetyCultureIncidentReportsDocumentQuery";
		}
	}

	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsDocumentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeSafetyCultureIncidentReportsDocumentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.DocumentID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.FileAttachName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.FileAttachName;
			c.CharacterMaxLength = 150;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.DocumentDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.DocumentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.SmallImage, 6, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.SmallImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.OriFileName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.OriFileName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.OriPath, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.OriPath;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.IsUpload, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.IsUpload;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsDocumentMetadata.ColumnNames.IsDeleted, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsDocumentMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeSafetyCultureIncidentReportsDocumentMetadata Meta()
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
			lock (typeof(EmployeeSafetyCultureIncidentReportsDocumentMetadata))
			{
				if (EmployeeSafetyCultureIncidentReportsDocumentMetadata.mapDelegates == null)
				{
					EmployeeSafetyCultureIncidentReportsDocumentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeSafetyCultureIncidentReportsDocumentMetadata.meta == null)
				{
					EmployeeSafetyCultureIncidentReportsDocumentMetadata.meta = new EmployeeSafetyCultureIncidentReportsDocumentMetadata();
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


				meta.Source = "EmployeeSafetyCultureIncidentReportsDocument";
				meta.Destination = "EmployeeSafetyCultureIncidentReportsDocument";
				meta.spInsert = "proc_EmployeeSafetyCultureIncidentReportsDocumentInsert";
				meta.spUpdate = "proc_EmployeeSafetyCultureIncidentReportsDocumentUpdate";
				meta.spDelete = "proc_EmployeeSafetyCultureIncidentReportsDocumentDelete";
				meta.spLoadAll = "proc_EmployeeSafetyCultureIncidentReportsDocumentLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeSafetyCultureIncidentReportsDocumentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeSafetyCultureIncidentReportsDocumentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
