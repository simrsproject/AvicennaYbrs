/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2022 12:27:41 AM
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
	abstract public class esSatuSehatResultCollection : esEntityCollectionWAuditLog
	{
		public esSatuSehatResultCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SatuSehatResultCollection";
		}

		#region Query Logic
		protected void InitQuery(esSatuSehatResultQuery query)
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
			this.InitQuery(query as esSatuSehatResultQuery);
		}
		#endregion

		virtual public SatuSehatResult DetachEntity(SatuSehatResult entity)
		{
			return base.DetachEntity(entity) as SatuSehatResult;
		}

		virtual public SatuSehatResult AttachEntity(SatuSehatResult entity)
		{
			return base.AttachEntity(entity) as SatuSehatResult;
		}

		virtual public void Combine(SatuSehatResultCollection collection)
		{
			base.Combine(collection);
		}

		new public SatuSehatResult this[int index]
		{
			get
			{
				return base[index] as SatuSehatResult;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SatuSehatResult);
		}
	}

	[Serializable]
	abstract public class esSatuSehatResult : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSatuSehatResultQuery GetDynamicQuery()
		{
			return null;
		}

		public esSatuSehatResult()
		{
		}

		public esSatuSehatResult(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Guid encounterID, String resourceType, Int32 indexNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(encounterID, resourceType, indexNo);
			else
				return LoadByPrimaryKeyStoredProcedure(encounterID, resourceType, indexNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Guid encounterID, String resourceType, Int32 indexNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(encounterID, resourceType, indexNo);
			else
				return LoadByPrimaryKeyStoredProcedure(encounterID, resourceType, indexNo);
		}

		private bool LoadByPrimaryKeyDynamic(Guid encounterID, String resourceType, Int32 indexNo)
		{
			esSatuSehatResultQuery query = this.GetDynamicQuery();
			query.Where(query.EncounterID == encounterID, query.ResourceType == resourceType, query.IndexNo == indexNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Guid encounterID, String resourceType, Int32 indexNo)
		{
			esParameters parms = new esParameters();
			parms.Add("EncounterID", encounterID);
			parms.Add("ResourceType", resourceType);
			parms.Add("IndexNo", indexNo);
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
						case "EncounterID": this.str.EncounterID = (string)value; break;
						case "ResourceType": this.str.ResourceType = (string)value; break;
						case "IndexNo": this.str.IndexNo = (string)value; break;
						case "Category": this.str.Category = (string)value; break;
						case "Code": this.str.Code = (string)value; break;
						case "ResultID": this.str.ResultID = (string)value; break;
						case "PostData": this.str.PostData = (string)value; break;
						case "ErrorResponse": this.str.ErrorResponse = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EncounterID":

							if (value == null || value is System.Guid)
								this.EncounterID = (System.Guid?)value;
							break;
						case "IndexNo":

							if (value == null || value is System.Int32)
								this.IndexNo = (System.Int32?)value;
							break;
						case "ResultID":

							if (value == null || value is System.Guid)
								this.ResultID = (System.Guid?)value;
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
		/// Maps to SatuSehatResult.EncounterID
		/// </summary>
		virtual public System.Guid? EncounterID
		{
			get
			{
				return base.GetSystemGuid(SatuSehatResultMetadata.ColumnNames.EncounterID);
			}

			set
			{
				base.SetSystemGuid(SatuSehatResultMetadata.ColumnNames.EncounterID, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.ResourceType
		/// </summary>
		virtual public System.String ResourceType
		{
			get
			{
				return base.GetSystemString(SatuSehatResultMetadata.ColumnNames.ResourceType);
			}

			set
			{
				base.SetSystemString(SatuSehatResultMetadata.ColumnNames.ResourceType, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.IndexNo
		/// </summary>
		virtual public System.Int32? IndexNo
		{
			get
			{
				return base.GetSystemInt32(SatuSehatResultMetadata.ColumnNames.IndexNo);
			}

			set
			{
				base.SetSystemInt32(SatuSehatResultMetadata.ColumnNames.IndexNo, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.Category
		/// </summary>
		virtual public System.String Category
		{
			get
			{
				return base.GetSystemString(SatuSehatResultMetadata.ColumnNames.Category);
			}

			set
			{
				base.SetSystemString(SatuSehatResultMetadata.ColumnNames.Category, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.Code
		/// </summary>
		virtual public System.String Code
		{
			get
			{
				return base.GetSystemString(SatuSehatResultMetadata.ColumnNames.Code);
			}

			set
			{
				base.SetSystemString(SatuSehatResultMetadata.ColumnNames.Code, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.ResultID
		/// </summary>
		virtual public System.Guid? ResultID
		{
			get
			{
				return base.GetSystemGuid(SatuSehatResultMetadata.ColumnNames.ResultID);
			}

			set
			{
				base.SetSystemGuid(SatuSehatResultMetadata.ColumnNames.ResultID, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.PostData
		/// </summary>
		virtual public System.String PostData
		{
			get
			{
				return base.GetSystemString(SatuSehatResultMetadata.ColumnNames.PostData);
			}

			set
			{
				base.SetSystemString(SatuSehatResultMetadata.ColumnNames.PostData, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.ErrorResponse
		/// </summary>
		virtual public System.String ErrorResponse
		{
			get
			{
				return base.GetSystemString(SatuSehatResultMetadata.ColumnNames.ErrorResponse);
			}

			set
			{
				base.SetSystemString(SatuSehatResultMetadata.ColumnNames.ErrorResponse, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SatuSehatResultMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SatuSehatResultMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SatuSehatResult.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SatuSehatResultMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SatuSehatResultMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSatuSehatResult entity)
			{
				this.entity = entity;
			}
			public System.String EncounterID
			{
				get
				{
					System.Guid? data = entity.EncounterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EncounterID = null;
					else entity.EncounterID = new Guid(value);
				}
			}
			public System.String ResourceType
			{
				get
				{
					System.String data = entity.ResourceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResourceType = null;
					else entity.ResourceType = Convert.ToString(value);
				}
			}
			public System.String IndexNo
			{
				get
				{
					System.Int32? data = entity.IndexNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndexNo = null;
					else entity.IndexNo = Convert.ToInt32(value);
				}
			}
			public System.String Category
			{
				get
				{
					System.String data = entity.Category;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Category = null;
					else entity.Category = Convert.ToString(value);
				}
			}
			public System.String Code
			{
				get
				{
					System.String data = entity.Code;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Code = null;
					else entity.Code = Convert.ToString(value);
				}
			}
			public System.String ResultID
			{
				get
				{
					System.Guid? data = entity.ResultID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResultID = null;
					else entity.ResultID = new Guid(value);
				}
			}
			public System.String PostData
			{
				get
				{
					System.String data = entity.PostData;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PostData = null;
					else entity.PostData = Convert.ToString(value);
				}
			}
			public System.String ErrorResponse
			{
				get
				{
					System.String data = entity.ErrorResponse;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ErrorResponse = null;
					else entity.ErrorResponse = Convert.ToString(value);
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
			private esSatuSehatResult entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSatuSehatResultQuery query)
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
				throw new Exception("esSatuSehatResult can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SatuSehatResult : esSatuSehatResult
	{
	}

	[Serializable]
	abstract public class esSatuSehatResultQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SatuSehatResultMetadata.Meta();
			}
		}

		public esQueryItem EncounterID
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.EncounterID, esSystemType.Guid);
			}
		}

		public esQueryItem ResourceType
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.ResourceType, esSystemType.String);
			}
		}

		public esQueryItem IndexNo
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.IndexNo, esSystemType.Int32);
			}
		}

		public esQueryItem Category
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.Category, esSystemType.String);
			}
		}

		public esQueryItem Code
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.Code, esSystemType.String);
			}
		}

		public esQueryItem ResultID
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.ResultID, esSystemType.Guid);
			}
		}

		public esQueryItem PostData
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.PostData, esSystemType.String);
			}
		}

		public esQueryItem ErrorResponse
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.ErrorResponse, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SatuSehatResultMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SatuSehatResultCollection")]
	public partial class SatuSehatResultCollection : esSatuSehatResultCollection, IEnumerable<SatuSehatResult>
	{
		public SatuSehatResultCollection()
		{

		}

		public static implicit operator List<SatuSehatResult>(SatuSehatResultCollection coll)
		{
			List<SatuSehatResult> list = new List<SatuSehatResult>();

			foreach (SatuSehatResult emp in coll)
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
				return SatuSehatResultMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SatuSehatResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SatuSehatResult(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SatuSehatResult();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SatuSehatResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SatuSehatResultQuery();
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
		public bool Load(SatuSehatResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SatuSehatResult AddNew()
		{
			SatuSehatResult entity = base.AddNewEntity() as SatuSehatResult;

			return entity;
		}
		public SatuSehatResult FindByPrimaryKey(Guid encounterID, String resourceType, Int32 indexNo)
		{
			return base.FindByPrimaryKey(encounterID, resourceType, indexNo) as SatuSehatResult;
		}

		#region IEnumerable< SatuSehatResult> Members

		IEnumerator<SatuSehatResult> IEnumerable<SatuSehatResult>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SatuSehatResult;
			}
		}

		#endregion

		private SatuSehatResultQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SatuSehatResult' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SatuSehatResult ({EncounterID, ResourceType, IndexNo})")]
	[Serializable]
	public partial class SatuSehatResult : esSatuSehatResult
	{
		public SatuSehatResult()
		{
		}

		public SatuSehatResult(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SatuSehatResultMetadata.Meta();
			}
		}

		override protected esSatuSehatResultQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SatuSehatResultQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SatuSehatResultQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SatuSehatResultQuery();
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
		public bool Load(SatuSehatResultQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SatuSehatResultQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SatuSehatResultQuery : esSatuSehatResultQuery
	{
		public SatuSehatResultQuery()
		{

		}

		public SatuSehatResultQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SatuSehatResultQuery";
		}
	}

	[Serializable]
	public partial class SatuSehatResultMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SatuSehatResultMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.EncounterID, 0, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.EncounterID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 0;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.ResourceType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.ResourceType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.IndexNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.IndexNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.Category, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.Category;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.Code, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.Code;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.ResultID, 5, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.ResultID;
			c.NumericPrecision = 0;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.PostData, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.PostData;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.ErrorResponse, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.ErrorResponse;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(SatuSehatResultMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = SatuSehatResultMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public SatuSehatResultMetadata Meta()
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
			public const string EncounterID = "EncounterID";
			public const string ResourceType = "ResourceType";
			public const string IndexNo = "IndexNo";
			public const string Category = "Category";
			public const string Code = "Code";
			public const string ResultID = "ResultID";
			public const string PostData = "PostData";
			public const string ErrorResponse = "ErrorResponse";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EncounterID = "EncounterID";
			public const string ResourceType = "ResourceType";
			public const string IndexNo = "IndexNo";
			public const string Category = "Category";
			public const string Code = "Code";
			public const string ResultID = "ResultID";
			public const string PostData = "PostData";
			public const string ErrorResponse = "ErrorResponse";
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
			lock (typeof(SatuSehatResultMetadata))
			{
				if (SatuSehatResultMetadata.mapDelegates == null)
				{
					SatuSehatResultMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SatuSehatResultMetadata.meta == null)
				{
					SatuSehatResultMetadata.meta = new SatuSehatResultMetadata();
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

				meta.AddTypeMap("EncounterID", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("ResourceType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Category", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Code", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResultID", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("PostData", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ErrorResponse", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SatuSehatResult";
				meta.Destination = "SatuSehatResult";
				meta.spInsert = "proc_SatuSehatResultInsert";
				meta.spUpdate = "proc_SatuSehatResultUpdate";
				meta.spDelete = "proc_SatuSehatResultDelete";
				meta.spLoadAll = "proc_SatuSehatResultLoadAll";
				meta.spLoadByPrimaryKey = "proc_SatuSehatResultLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SatuSehatResultMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
