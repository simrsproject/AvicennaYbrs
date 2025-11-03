/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/28/2022 10:56:53 PM
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
	abstract public class esPatientDocumentCollection : esEntityCollectionWAuditLog
	{
		public esPatientDocumentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientDocumentCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientDocumentQuery query)
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
			this.InitQuery(query as esPatientDocumentQuery);
		}
		#endregion

		virtual public PatientDocument DetachEntity(PatientDocument entity)
		{
			return base.DetachEntity(entity) as PatientDocument;
		}

		virtual public PatientDocument AttachEntity(PatientDocument entity)
		{
			return base.AttachEntity(entity) as PatientDocument;
		}

		virtual public void Combine(PatientDocumentCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientDocument this[int index]
		{
			get
			{
				return base[index] as PatientDocument;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientDocument);
		}
	}

	[Serializable]
	abstract public class esPatientDocument : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientDocumentQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientDocument()
		{
		}

		public esPatientDocument(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 patientDocumentID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientDocumentID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientDocumentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 patientDocumentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientDocumentID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientDocumentID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 patientDocumentID)
		{
			esPatientDocumentQuery query = this.GetDynamicQuery();
			query.Where(query.PatientDocumentID == patientDocumentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 patientDocumentID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientDocumentID", patientDocumentID);
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
						case "PatientDocumentID": this.str.PatientDocumentID = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "FileAttachName": this.str.FileAttachName = (string)value; break;
						case "DocumentName": this.str.DocumentName = (string)value; break;
						case "DocumentDate": this.str.DocumentDate = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "OriFileName": this.str.OriFileName = (string)value; break;
						case "OriPath": this.str.OriPath = (string)value; break;
						case "IsUpload": this.str.IsUpload = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PatientDocumentID":

							if (value == null || value is System.Int64)
								this.PatientDocumentID = (System.Int64?)value;
							break;
						case "DocumentDate":

							if (value == null || value is System.DateTime)
								this.DocumentDate = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to PatientDocument.PatientDocumentID
		/// </summary>
		virtual public System.Int64? PatientDocumentID
		{
			get
			{
				return base.GetSystemInt64(PatientDocumentMetadata.ColumnNames.PatientDocumentID);
			}

			set
			{
				base.SetSystemInt64(PatientDocumentMetadata.ColumnNames.PatientDocumentID, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.FileAttachName
		/// </summary>
		virtual public System.String FileAttachName
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.FileAttachName);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.FileAttachName, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.DocumentName);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.DocumentDate
		/// </summary>
		virtual public System.DateTime? DocumentDate
		{
			get
			{
				return base.GetSystemDateTime(PatientDocumentMetadata.ColumnNames.DocumentDate);
			}

			set
			{
				base.SetSystemDateTime(PatientDocumentMetadata.ColumnNames.DocumentDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientDocumentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientDocumentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.SmallImage
		/// </summary>
		virtual public System.Byte[] SmallImage
		{
			get
			{
				return base.GetSystemByteArray(PatientDocumentMetadata.ColumnNames.SmallImage);
			}

			set
			{
				base.SetSystemByteArray(PatientDocumentMetadata.ColumnNames.SmallImage, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.OriFileName
		/// </summary>
		virtual public System.String OriFileName
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.OriFileName);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.OriFileName, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.OriPath
		/// </summary>
		virtual public System.String OriPath
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.OriPath);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.OriPath, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.IsUpload
		/// </summary>
		virtual public System.Boolean? IsUpload
		{
			get
			{
				return base.GetSystemBoolean(PatientDocumentMetadata.ColumnNames.IsUpload);
			}

			set
			{
				base.SetSystemBoolean(PatientDocumentMetadata.ColumnNames.IsUpload, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(PatientDocumentMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(PatientDocumentMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to PatientDocument.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(PatientDocumentMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(PatientDocumentMetadata.ColumnNames.ReferenceNo, value);
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
			public esStrings(esPatientDocument entity)
			{
				this.entity = entity;
			}
			public System.String PatientDocumentID
			{
				get
				{
					System.Int64? data = entity.PatientDocumentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientDocumentID = null;
					else entity.PatientDocumentID = Convert.ToInt64(value);
				}
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
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			private esPatientDocument entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientDocumentQuery query)
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
				throw new Exception("esPatientDocument can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientDocument : esPatientDocument
	{
	}

	[Serializable]
	abstract public class esPatientDocumentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientDocumentMetadata.Meta();
			}
		}

		public esQueryItem PatientDocumentID
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.PatientDocumentID, esSystemType.Int64);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem FileAttachName
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.FileAttachName, esSystemType.String);
			}
		}

		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		}

		public esQueryItem DocumentDate
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.DocumentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SmallImage
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.SmallImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem OriFileName
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.OriFileName, esSystemType.String);
			}
		}

		public esQueryItem OriPath
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.OriPath, esSystemType.String);
			}
		}

		public esQueryItem IsUpload
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.IsUpload, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, PatientDocumentMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientDocumentCollection")]
	public partial class PatientDocumentCollection : esPatientDocumentCollection, IEnumerable<PatientDocument>
	{
		public PatientDocumentCollection()
		{

		}

		public static implicit operator List<PatientDocument>(PatientDocumentCollection coll)
		{
			List<PatientDocument> list = new List<PatientDocument>();

			foreach (PatientDocument emp in coll)
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
				return PatientDocumentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientDocument(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientDocument();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientDocumentQuery();
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
		public bool Load(PatientDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientDocument AddNew()
		{
			PatientDocument entity = base.AddNewEntity() as PatientDocument;

			return entity;
		}
		public PatientDocument FindByPrimaryKey(Int64 patientDocumentID)
		{
			return base.FindByPrimaryKey(patientDocumentID) as PatientDocument;
		}

		#region IEnumerable< PatientDocument> Members

		IEnumerator<PatientDocument> IEnumerable<PatientDocument>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientDocument;
			}
		}

		#endregion

		private PatientDocumentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientDocument' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientDocument ({PatientDocumentID})")]
	[Serializable]
	public partial class PatientDocument : esPatientDocument
	{
		public PatientDocument()
		{
		}

		public PatientDocument(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientDocumentMetadata.Meta();
			}
		}

		override protected esPatientDocumentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientDocumentQuery();
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
		public bool Load(PatientDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientDocumentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientDocumentQuery : esPatientDocumentQuery
	{
		public PatientDocumentQuery()
		{

		}

		public PatientDocumentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientDocumentQuery";
		}
	}

	[Serializable]
	public partial class PatientDocumentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientDocumentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.PatientDocumentID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.PatientDocumentID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.PatientID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.FileAttachName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.FileAttachName;
			c.CharacterMaxLength = 150;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.DocumentName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 200;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.DocumentDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.DocumentDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.SmallImage, 9, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.SmallImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.OriFileName, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.OriFileName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.OriPath, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.OriPath;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.IsUpload, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.IsUpload;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.IsDeleted, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDocumentMetadata.ColumnNames.ReferenceNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDocumentMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PatientDocumentMetadata Meta()
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
			public const string PatientDocumentID = "PatientDocumentID";
			public const string PatientID = "PatientID";
			public const string RegistrationNo = "RegistrationNo";
			public const string FileAttachName = "FileAttachName";
			public const string DocumentName = "DocumentName";
			public const string DocumentDate = "DocumentDate";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SmallImage = "SmallImage";
			public const string OriFileName = "OriFileName";
			public const string OriPath = "OriPath";
			public const string IsUpload = "IsUpload";
			public const string IsDeleted = "IsDeleted";
			public const string ReferenceNo = "ReferenceNo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PatientDocumentID = "PatientDocumentID";
			public const string PatientID = "PatientID";
			public const string RegistrationNo = "RegistrationNo";
			public const string FileAttachName = "FileAttachName";
			public const string DocumentName = "DocumentName";
			public const string DocumentDate = "DocumentDate";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SmallImage = "SmallImage";
			public const string OriFileName = "OriFileName";
			public const string OriPath = "OriPath";
			public const string IsUpload = "IsUpload";
			public const string IsDeleted = "IsDeleted";
			public const string ReferenceNo = "ReferenceNo";
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
			lock (typeof(PatientDocumentMetadata))
			{
				if (PatientDocumentMetadata.mapDelegates == null)
				{
					PatientDocumentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientDocumentMetadata.meta == null)
				{
					PatientDocumentMetadata.meta = new PatientDocumentMetadata();
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

				meta.AddTypeMap("PatientDocumentID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FileAttachName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SmallImage", new esTypeMap("image", "System.Byte[]"));
				meta.AddTypeMap("OriFileName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OriPath", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUpload", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));


				meta.Source = "PatientDocument";
				meta.Destination = "PatientDocument";
				meta.spInsert = "proc_PatientDocumentInsert";
				meta.spUpdate = "proc_PatientDocumentUpdate";
				meta.spDelete = "proc_PatientDocumentDelete";
				meta.spLoadAll = "proc_PatientDocumentLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientDocumentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientDocumentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
