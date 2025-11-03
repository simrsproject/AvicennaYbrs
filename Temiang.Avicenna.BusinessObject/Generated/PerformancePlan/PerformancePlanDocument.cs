/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/18/2023 5:14:11 PM
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
	abstract public class esPerformancePlanDocumentCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanDocumentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanDocumentCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanDocumentQuery query)
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
			this.InitQuery(query as esPerformancePlanDocumentQuery);
		}
		#endregion

		virtual public PerformancePlanDocument DetachEntity(PerformancePlanDocument entity)
		{
			return base.DetachEntity(entity) as PerformancePlanDocument;
		}

		virtual public PerformancePlanDocument AttachEntity(PerformancePlanDocument entity)
		{
			return base.AttachEntity(entity) as PerformancePlanDocument;
		}

		virtual public void Combine(PerformancePlanDocumentCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanDocument this[int index]
		{
			get
			{
				return base[index] as PerformancePlanDocument;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanDocument);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanDocument : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanDocumentQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanDocument()
		{
		}

		public esPerformancePlanDocument(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 performancePlanDocumentID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performancePlanDocumentID);
			else
				return LoadByPrimaryKeyStoredProcedure(performancePlanDocumentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 performancePlanDocumentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performancePlanDocumentID);
			else
				return LoadByPrimaryKeyStoredProcedure(performancePlanDocumentID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 performancePlanDocumentID)
		{
			esPerformancePlanDocumentQuery query = this.GetDynamicQuery();
			query.Where(query.PerformancePlanDocumentID == performancePlanDocumentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 performancePlanDocumentID)
		{
			esParameters parms = new esParameters();
			parms.Add("PerformancePlanDocumentID", performancePlanDocumentID);
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
						case "PerformancePlanDocumentID": this.str.PerformancePlanDocumentID = (string)value; break;
						case "PerformancePlanType": this.str.PerformancePlanType = (string)value; break;
						case "PerformancePlanID": this.str.PerformancePlanID = (string)value; break;
						case "SRQuarterPeriod": this.str.SRQuarterPeriod = (string)value; break;
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
						case "PerformancePlanDocumentID":

							if (value == null || value is System.Int64)
								this.PerformancePlanDocumentID = (System.Int64?)value;
							break;
						case "PerformancePlanID":

							if (value == null || value is System.Int64)
								this.PerformancePlanID = (System.Int64?)value;
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
		/// Maps to PerformancePlanDocument.PerformancePlanDocumentID
		/// </summary>
		virtual public System.Int64? PerformancePlanDocumentID
		{
			get
			{
				return base.GetSystemInt64(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanDocumentID);
			}

			set
			{
				base.SetSystemInt64(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanDocumentID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.PerformancePlanType
		/// </summary>
		virtual public System.String PerformancePlanType
		{
			get
			{
				return base.GetSystemString(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanType);
			}

			set
			{
				base.SetSystemString(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanType, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.PerformancePlanID
		/// </summary>
		virtual public System.Int64? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt64(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt64(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.SRQuarterPeriod
		/// </summary>
		virtual public System.String SRQuarterPeriod
		{
			get
			{
				return base.GetSystemString(PerformancePlanDocumentMetadata.ColumnNames.SRQuarterPeriod);
			}

			set
			{
				base.SetSystemString(PerformancePlanDocumentMetadata.ColumnNames.SRQuarterPeriod, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.FileAttachName
		/// </summary>
		virtual public System.String FileAttachName
		{
			get
			{
				return base.GetSystemString(PerformancePlanDocumentMetadata.ColumnNames.FileAttachName);
			}

			set
			{
				base.SetSystemString(PerformancePlanDocumentMetadata.ColumnNames.FileAttachName, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.DocumentName
		/// </summary>
		virtual public System.String DocumentName
		{
			get
			{
				return base.GetSystemString(PerformancePlanDocumentMetadata.ColumnNames.DocumentName);
			}

			set
			{
				base.SetSystemString(PerformancePlanDocumentMetadata.ColumnNames.DocumentName, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.DocumentDate
		/// </summary>
		virtual public System.DateTime? DocumentDate
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanDocumentMetadata.ColumnNames.DocumentDate);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanDocumentMetadata.ColumnNames.DocumentDate, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PerformancePlanDocumentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PerformancePlanDocumentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.SmallImage
		/// </summary>
		virtual public System.Byte[] SmallImage
		{
			get
			{
				return base.GetSystemByteArray(PerformancePlanDocumentMetadata.ColumnNames.SmallImage);
			}

			set
			{
				base.SetSystemByteArray(PerformancePlanDocumentMetadata.ColumnNames.SmallImage, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.OriFileName
		/// </summary>
		virtual public System.String OriFileName
		{
			get
			{
				return base.GetSystemString(PerformancePlanDocumentMetadata.ColumnNames.OriFileName);
			}

			set
			{
				base.SetSystemString(PerformancePlanDocumentMetadata.ColumnNames.OriFileName, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.OriPath
		/// </summary>
		virtual public System.String OriPath
		{
			get
			{
				return base.GetSystemString(PerformancePlanDocumentMetadata.ColumnNames.OriPath);
			}

			set
			{
				base.SetSystemString(PerformancePlanDocumentMetadata.ColumnNames.OriPath, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.IsUpload
		/// </summary>
		virtual public System.Boolean? IsUpload
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanDocumentMetadata.ColumnNames.IsUpload);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanDocumentMetadata.ColumnNames.IsUpload, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanDocumentMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanDocumentMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanDocumentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanDocumentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanDocument.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanDocumentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanDocumentMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanDocument entity)
			{
				this.entity = entity;
			}
			public System.String PerformancePlanDocumentID
			{
				get
				{
					System.Int64? data = entity.PerformancePlanDocumentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanDocumentID = null;
					else entity.PerformancePlanDocumentID = Convert.ToInt64(value);
				}
			}
			public System.String PerformancePlanType
			{
				get
				{
					System.String data = entity.PerformancePlanType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanType = null;
					else entity.PerformancePlanType = Convert.ToString(value);
				}
			}
			public System.String PerformancePlanID
			{
				get
				{
					System.Int64? data = entity.PerformancePlanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanID = null;
					else entity.PerformancePlanID = Convert.ToInt64(value);
				}
			}
			public System.String SRQuarterPeriod
			{
				get
				{
					System.String data = entity.SRQuarterPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRQuarterPeriod = null;
					else entity.SRQuarterPeriod = Convert.ToString(value);
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
			private esPerformancePlanDocument entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanDocumentQuery query)
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
				throw new Exception("esPerformancePlanDocument can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanDocument : esPerformancePlanDocument
	{
	}

	[Serializable]
	abstract public class esPerformancePlanDocumentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanDocumentMetadata.Meta();
			}
		}

		public esQueryItem PerformancePlanDocumentID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanDocumentID, esSystemType.Int64);
			}
		}

		public esQueryItem PerformancePlanType
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanType, esSystemType.String);
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanID, esSystemType.Int64);
			}
		}

		public esQueryItem SRQuarterPeriod
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.SRQuarterPeriod, esSystemType.String);
			}
		}

		public esQueryItem FileAttachName
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.FileAttachName, esSystemType.String);
			}
		}

		public esQueryItem DocumentName
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.DocumentName, esSystemType.String);
			}
		}

		public esQueryItem DocumentDate
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.DocumentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem SmallImage
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.SmallImage, esSystemType.ByteArray);
			}
		}

		public esQueryItem OriFileName
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.OriFileName, esSystemType.String);
			}
		}

		public esQueryItem OriPath
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.OriPath, esSystemType.String);
			}
		}

		public esQueryItem IsUpload
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.IsUpload, esSystemType.Boolean);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanDocumentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanDocumentCollection")]
	public partial class PerformancePlanDocumentCollection : esPerformancePlanDocumentCollection, IEnumerable<PerformancePlanDocument>
	{
		public PerformancePlanDocumentCollection()
		{

		}

		public static implicit operator List<PerformancePlanDocument>(PerformancePlanDocumentCollection coll)
		{
			List<PerformancePlanDocument> list = new List<PerformancePlanDocument>();

			foreach (PerformancePlanDocument emp in coll)
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
				return PerformancePlanDocumentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanDocument(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanDocument();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanDocumentQuery();
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
		public bool Load(PerformancePlanDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanDocument AddNew()
		{
			PerformancePlanDocument entity = base.AddNewEntity() as PerformancePlanDocument;

			return entity;
		}
		public PerformancePlanDocument FindByPrimaryKey(Int64 performancePlanDocumentID)
		{
			return base.FindByPrimaryKey(performancePlanDocumentID) as PerformancePlanDocument;
		}

		#region IEnumerable< PerformancePlanDocument> Members

		IEnumerator<PerformancePlanDocument> IEnumerable<PerformancePlanDocument>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanDocument;
			}
		}

		#endregion

		private PerformancePlanDocumentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanDocument' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanDocument ({PerformancePlanDocumentID})")]
	[Serializable]
	public partial class PerformancePlanDocument : esPerformancePlanDocument
	{
		public PerformancePlanDocument()
		{
		}

		public PerformancePlanDocument(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanDocumentMetadata.Meta();
			}
		}

		override protected esPerformancePlanDocumentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanDocumentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanDocumentQuery();
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
		public bool Load(PerformancePlanDocumentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanDocumentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanDocumentQuery : esPerformancePlanDocumentQuery
	{
		public PerformancePlanDocumentQuery()
		{

		}

		public PerformancePlanDocumentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanDocumentQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanDocumentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanDocumentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanDocumentID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.PerformancePlanDocumentID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.PerformancePlanType;
			c.CharacterMaxLength = 1;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.PerformancePlanID, 2, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.PerformancePlanID;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.SRQuarterPeriod, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.SRQuarterPeriod;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.FileAttachName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.FileAttachName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.DocumentName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.DocumentName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.DocumentDate, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.DocumentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.SmallImage, 8, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.SmallImage;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.OriFileName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.OriFileName;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.OriPath, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.OriPath;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.IsUpload, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.IsUpload;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.IsDeleted, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanDocumentMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanDocumentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanDocumentMetadata Meta()
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
			public const string PerformancePlanDocumentID = "PerformancePlanDocumentID";
			public const string PerformancePlanType = "PerformancePlanType";
			public const string PerformancePlanID = "PerformancePlanID";
			public const string SRQuarterPeriod = "SRQuarterPeriod";
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
			public const string PerformancePlanDocumentID = "PerformancePlanDocumentID";
			public const string PerformancePlanType = "PerformancePlanType";
			public const string PerformancePlanID = "PerformancePlanID";
			public const string SRQuarterPeriod = "SRQuarterPeriod";
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
			lock (typeof(PerformancePlanDocumentMetadata))
			{
				if (PerformancePlanDocumentMetadata.mapDelegates == null)
				{
					PerformancePlanDocumentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanDocumentMetadata.meta == null)
				{
					PerformancePlanDocumentMetadata.meta = new PerformancePlanDocumentMetadata();
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

				meta.AddTypeMap("PerformancePlanDocumentID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PerformancePlanType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PerformancePlanID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("SRQuarterPeriod", new esTypeMap("varchar", "System.String"));
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


				meta.Source = "PerformancePlanDocument";
				meta.Destination = "PerformancePlanDocument";
				meta.spInsert = "proc_PerformancePlanDocumentInsert";
				meta.spUpdate = "proc_PerformancePlanDocumentUpdate";
				meta.spDelete = "proc_PerformancePlanDocumentDelete";
				meta.spLoadAll = "proc_PerformancePlanDocumentLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanDocumentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanDocumentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
