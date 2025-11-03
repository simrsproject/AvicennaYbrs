/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/10/2023 12:53:27 PM
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
	abstract public class esMedicalRecordFileCompletenessHistoryItemCollection : esEntityCollectionWAuditLog
	{
		public esMedicalRecordFileCompletenessHistoryItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicalRecordFileCompletenessHistoryItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileCompletenessHistoryItemQuery query)
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
			this.InitQuery(query as esMedicalRecordFileCompletenessHistoryItemQuery);
		}
		#endregion

		virtual public MedicalRecordFileCompletenessHistoryItem DetachEntity(MedicalRecordFileCompletenessHistoryItem entity)
		{
			return base.DetachEntity(entity) as MedicalRecordFileCompletenessHistoryItem;
		}

		virtual public MedicalRecordFileCompletenessHistoryItem AttachEntity(MedicalRecordFileCompletenessHistoryItem entity)
		{
			return base.AttachEntity(entity) as MedicalRecordFileCompletenessHistoryItem;
		}

		virtual public void Combine(MedicalRecordFileCompletenessHistoryItemCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicalRecordFileCompletenessHistoryItem this[int index]
		{
			get
			{
				return base[index] as MedicalRecordFileCompletenessHistoryItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalRecordFileCompletenessHistoryItem);
		}
	}

	[Serializable]
	abstract public class esMedicalRecordFileCompletenessHistoryItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalRecordFileCompletenessHistoryItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalRecordFileCompletenessHistoryItem()
		{
		}

		public esMedicalRecordFileCompletenessHistoryItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 txId, Int32 documentFilesID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txId, documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(txId, documentFilesID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 txId, Int32 documentFilesID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(txId, documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(txId, documentFilesID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 txId, Int32 documentFilesID)
		{
			esMedicalRecordFileCompletenessHistoryItemQuery query = this.GetDynamicQuery();
			query.Where(query.TxId == txId, query.DocumentFilesID == documentFilesID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 txId, Int32 documentFilesID)
		{
			esParameters parms = new esParameters();
			parms.Add("TxId", txId);
			parms.Add("DocumentFilesID", documentFilesID);
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
						case "TxId": this.str.TxId = (string)value; break;
						case "DocumentFilesID": this.str.DocumentFilesID = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "TxId":

							if (value == null || value is System.Int64)
								this.TxId = (System.Int64?)value;
							break;
						case "DocumentFilesID":

							if (value == null || value is System.Int32)
								this.DocumentFilesID = (System.Int32?)value;
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
		/// Maps to MedicalRecordFileCompletenessHistoryItem.TxId
		/// </summary>
		virtual public System.Int64? TxId
		{
			get
			{
				return base.GetSystemInt64(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.TxId);
			}

			set
			{
				base.SetSystemInt64(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.TxId, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistoryItem.DocumentFilesID
		/// </summary>
		virtual public System.Int32? DocumentFilesID
		{
			get
			{
				return base.GetSystemInt32(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.DocumentFilesID);
			}

			set
			{
				base.SetSystemInt32(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.DocumentFilesID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistoryItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistoryItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessHistoryItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicalRecordFileCompletenessHistoryItem entity)
			{
				this.entity = entity;
			}
			public System.String TxId
			{
				get
				{
					System.Int64? data = entity.TxId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TxId = null;
					else entity.TxId = Convert.ToInt64(value);
				}
			}
			public System.String DocumentFilesID
			{
				get
				{
					System.Int32? data = entity.DocumentFilesID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentFilesID = null;
					else entity.DocumentFilesID = Convert.ToInt32(value);
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
			private esMedicalRecordFileCompletenessHistoryItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileCompletenessHistoryItemQuery query)
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
				throw new Exception("esMedicalRecordFileCompletenessHistoryItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicalRecordFileCompletenessHistoryItem : esMedicalRecordFileCompletenessHistoryItem
	{
	}

	[Serializable]
	abstract public class esMedicalRecordFileCompletenessHistoryItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileCompletenessHistoryItemMetadata.Meta();
			}
		}

		public esQueryItem TxId
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.TxId, esSystemType.Int64);
			}
		}

		public esQueryItem DocumentFilesID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.DocumentFilesID, esSystemType.Int32);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalRecordFileCompletenessHistoryItemCollection")]
	public partial class MedicalRecordFileCompletenessHistoryItemCollection : esMedicalRecordFileCompletenessHistoryItemCollection, IEnumerable<MedicalRecordFileCompletenessHistoryItem>
	{
		public MedicalRecordFileCompletenessHistoryItemCollection()
		{

		}

		public static implicit operator List<MedicalRecordFileCompletenessHistoryItem>(MedicalRecordFileCompletenessHistoryItemCollection coll)
		{
			List<MedicalRecordFileCompletenessHistoryItem> list = new List<MedicalRecordFileCompletenessHistoryItem>();

			foreach (MedicalRecordFileCompletenessHistoryItem emp in coll)
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
				return MedicalRecordFileCompletenessHistoryItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileCompletenessHistoryItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalRecordFileCompletenessHistoryItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalRecordFileCompletenessHistoryItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileCompletenessHistoryItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileCompletenessHistoryItemQuery();
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
		public bool Load(MedicalRecordFileCompletenessHistoryItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicalRecordFileCompletenessHistoryItem AddNew()
		{
			MedicalRecordFileCompletenessHistoryItem entity = base.AddNewEntity() as MedicalRecordFileCompletenessHistoryItem;

			return entity;
		}
		public MedicalRecordFileCompletenessHistoryItem FindByPrimaryKey(Int64 txId, Int32 documentFilesID)
		{
			return base.FindByPrimaryKey(txId, documentFilesID) as MedicalRecordFileCompletenessHistoryItem;
		}

		#region IEnumerable< MedicalRecordFileCompletenessHistoryItem> Members

		IEnumerator<MedicalRecordFileCompletenessHistoryItem> IEnumerable<MedicalRecordFileCompletenessHistoryItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicalRecordFileCompletenessHistoryItem;
			}
		}

		#endregion

		private MedicalRecordFileCompletenessHistoryItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalRecordFileCompletenessHistoryItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicalRecordFileCompletenessHistoryItem ({TxId, DocumentFilesID})")]
	[Serializable]
	public partial class MedicalRecordFileCompletenessHistoryItem : esMedicalRecordFileCompletenessHistoryItem
	{
		public MedicalRecordFileCompletenessHistoryItem()
		{
		}

		public MedicalRecordFileCompletenessHistoryItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileCompletenessHistoryItemMetadata.Meta();
			}
		}

		override protected esMedicalRecordFileCompletenessHistoryItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileCompletenessHistoryItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileCompletenessHistoryItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileCompletenessHistoryItemQuery();
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
		public bool Load(MedicalRecordFileCompletenessHistoryItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicalRecordFileCompletenessHistoryItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicalRecordFileCompletenessHistoryItemQuery : esMedicalRecordFileCompletenessHistoryItemQuery
	{
		public MedicalRecordFileCompletenessHistoryItemQuery()
		{

		}

		public MedicalRecordFileCompletenessHistoryItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicalRecordFileCompletenessHistoryItemQuery";
		}
	}

	[Serializable]
	public partial class MedicalRecordFileCompletenessHistoryItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalRecordFileCompletenessHistoryItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.TxId, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicalRecordFileCompletenessHistoryItemMetadata.PropertyNames.TxId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.DocumentFilesID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalRecordFileCompletenessHistoryItemMetadata.PropertyNames.DocumentFilesID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessHistoryItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessHistoryItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessHistoryItemMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessHistoryItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicalRecordFileCompletenessHistoryItemMetadata Meta()
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
			public const string TxId = "TxId";
			public const string DocumentFilesID = "DocumentFilesID";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TxId = "TxId";
			public const string DocumentFilesID = "DocumentFilesID";
			public const string Notes = "Notes";
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
			lock (typeof(MedicalRecordFileCompletenessHistoryItemMetadata))
			{
				if (MedicalRecordFileCompletenessHistoryItemMetadata.mapDelegates == null)
				{
					MedicalRecordFileCompletenessHistoryItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicalRecordFileCompletenessHistoryItemMetadata.meta == null)
				{
					MedicalRecordFileCompletenessHistoryItemMetadata.meta = new MedicalRecordFileCompletenessHistoryItemMetadata();
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

				meta.AddTypeMap("TxId", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("DocumentFilesID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicalRecordFileCompletenessHistoryItem";
				meta.Destination = "MedicalRecordFileCompletenessHistoryItem";
				meta.spInsert = "proc_MedicalRecordFileCompletenessHistoryItemInsert";
				meta.spUpdate = "proc_MedicalRecordFileCompletenessHistoryItemUpdate";
				meta.spDelete = "proc_MedicalRecordFileCompletenessHistoryItemDelete";
				meta.spLoadAll = "proc_MedicalRecordFileCompletenessHistoryItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalRecordFileCompletenessHistoryItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalRecordFileCompletenessHistoryItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
