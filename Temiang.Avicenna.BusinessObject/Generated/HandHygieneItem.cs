/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/23/2022 11:16:34 AM
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
	abstract public class esHandHygieneItemCollection : esEntityCollectionWAuditLog
	{
		public esHandHygieneItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "HandHygieneItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esHandHygieneItemQuery query)
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
			this.InitQuery(query as esHandHygieneItemQuery);
		}
		#endregion

		virtual public HandHygieneItem DetachEntity(HandHygieneItem entity)
		{
			return base.DetachEntity(entity) as HandHygieneItem;
		}

		virtual public HandHygieneItem AttachEntity(HandHygieneItem entity)
		{
			return base.AttachEntity(entity) as HandHygieneItem;
		}

		virtual public void Combine(HandHygieneItemCollection collection)
		{
			base.Combine(collection);
		}

		new public HandHygieneItem this[int index]
		{
			get
			{
				return base[index] as HandHygieneItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HandHygieneItem);
		}
	}

	[Serializable]
	abstract public class esHandHygieneItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHandHygieneItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esHandHygieneItem()
		{
		}

		public esHandHygieneItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sROpportunity)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sROpportunity);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sROpportunity);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sROpportunity)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sROpportunity);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sROpportunity);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sROpportunity)
		{
			esHandHygieneItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SROpportunity == sROpportunity);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sROpportunity)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SROpportunity", sROpportunity);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SROpportunity": this.str.SROpportunity = (string)value; break;
						case "SRHandWashType": this.str.SRHandWashType = (string)value; break;
						case "IsWearGloves": this.str.IsWearGloves = (string)value; break;
						case "SRHandHygieneNote": this.str.SRHandHygieneNote = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsApply6Steps": this.str.IsApply6Steps = (string)value; break;
						case "SRApply6StepsResult": this.str.SRApply6StepsResult = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsWearGloves":

							if (value == null || value is System.Boolean)
								this.IsWearGloves = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsApply6Steps":

							if (value == null || value is System.Boolean)
								this.IsApply6Steps = (System.Boolean?)value;
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
		/// Maps to HandHygieneItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(HandHygieneItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(HandHygieneItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to HandHygieneItem.SROpportunity
		/// </summary>
		virtual public System.String SROpportunity
		{
			get
			{
				return base.GetSystemString(HandHygieneItemMetadata.ColumnNames.SROpportunity);
			}

			set
			{
				base.SetSystemString(HandHygieneItemMetadata.ColumnNames.SROpportunity, value);
			}
		}
		/// <summary>
		/// Maps to HandHygieneItem.SRHandWashType
		/// </summary>
		virtual public System.String SRHandWashType
		{
			get
			{
				return base.GetSystemString(HandHygieneItemMetadata.ColumnNames.SRHandWashType);
			}

			set
			{
				base.SetSystemString(HandHygieneItemMetadata.ColumnNames.SRHandWashType, value);
			}
		}
		/// <summary>
		/// Maps to HandHygieneItem.IsWearGloves
		/// </summary>
		virtual public System.Boolean? IsWearGloves
		{
			get
			{
				return base.GetSystemBoolean(HandHygieneItemMetadata.ColumnNames.IsWearGloves);
			}

			set
			{
				base.SetSystemBoolean(HandHygieneItemMetadata.ColumnNames.IsWearGloves, value);
			}
		}
		/// <summary>
		/// Maps to HandHygieneItem.SRHandHygieneNote
		/// </summary>
		virtual public System.String SRHandHygieneNote
		{
			get
			{
				return base.GetSystemString(HandHygieneItemMetadata.ColumnNames.SRHandHygieneNote);
			}

			set
			{
				base.SetSystemString(HandHygieneItemMetadata.ColumnNames.SRHandHygieneNote, value);
			}
		}
		/// <summary>
		/// Maps to HandHygieneItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(HandHygieneItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(HandHygieneItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to HandHygieneItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(HandHygieneItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(HandHygieneItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to HandHygieneItem.IsApply6Steps
		/// </summary>
		virtual public System.Boolean? IsApply6Steps
		{
			get
			{
				return base.GetSystemBoolean(HandHygieneItemMetadata.ColumnNames.IsApply6Steps);
			}

			set
			{
				base.SetSystemBoolean(HandHygieneItemMetadata.ColumnNames.IsApply6Steps, value);
			}
		}
		/// <summary>
		/// Maps to HandHygieneItem.SRApply6StepsResult
		/// </summary>
		virtual public System.String SRApply6StepsResult
		{
			get
			{
				return base.GetSystemString(HandHygieneItemMetadata.ColumnNames.SRApply6StepsResult);
			}

			set
			{
				base.SetSystemString(HandHygieneItemMetadata.ColumnNames.SRApply6StepsResult, value);
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
			public esStrings(esHandHygieneItem entity)
			{
				this.entity = entity;
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
			public System.String SROpportunity
			{
				get
				{
					System.String data = entity.SROpportunity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROpportunity = null;
					else entity.SROpportunity = Convert.ToString(value);
				}
			}
			public System.String SRHandWashType
			{
				get
				{
					System.String data = entity.SRHandWashType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRHandWashType = null;
					else entity.SRHandWashType = Convert.ToString(value);
				}
			}
			public System.String IsWearGloves
			{
				get
				{
					System.Boolean? data = entity.IsWearGloves;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWearGloves = null;
					else entity.IsWearGloves = Convert.ToBoolean(value);
				}
			}
			public System.String SRHandHygieneNote
			{
				get
				{
					System.String data = entity.SRHandHygieneNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRHandHygieneNote = null;
					else entity.SRHandHygieneNote = Convert.ToString(value);
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
			public System.String IsApply6Steps
			{
				get
				{
					System.Boolean? data = entity.IsApply6Steps;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApply6Steps = null;
					else entity.IsApply6Steps = Convert.ToBoolean(value);
				}
			}
			public System.String SRApply6StepsResult
			{
				get
				{
					System.String data = entity.SRApply6StepsResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRApply6StepsResult = null;
					else entity.SRApply6StepsResult = Convert.ToString(value);
				}
			}
			private esHandHygieneItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHandHygieneItemQuery query)
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
				throw new Exception("esHandHygieneItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class HandHygieneItem : esHandHygieneItem
	{
	}

	[Serializable]
	abstract public class esHandHygieneItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return HandHygieneItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SROpportunity
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.SROpportunity, esSystemType.String);
			}
		}

		public esQueryItem SRHandWashType
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.SRHandWashType, esSystemType.String);
			}
		}

		public esQueryItem IsWearGloves
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.IsWearGloves, esSystemType.Boolean);
			}
		}

		public esQueryItem SRHandHygieneNote
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.SRHandHygieneNote, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsApply6Steps
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.IsApply6Steps, esSystemType.Boolean);
			}
		}

		public esQueryItem SRApply6StepsResult
		{
			get
			{
				return new esQueryItem(this, HandHygieneItemMetadata.ColumnNames.SRApply6StepsResult, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HandHygieneItemCollection")]
	public partial class HandHygieneItemCollection : esHandHygieneItemCollection, IEnumerable<HandHygieneItem>
	{
		public HandHygieneItemCollection()
		{

		}

		public static implicit operator List<HandHygieneItem>(HandHygieneItemCollection coll)
		{
			List<HandHygieneItem> list = new List<HandHygieneItem>();

			foreach (HandHygieneItem emp in coll)
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
				return HandHygieneItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HandHygieneItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HandHygieneItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HandHygieneItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public HandHygieneItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HandHygieneItemQuery();
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
		public bool Load(HandHygieneItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public HandHygieneItem AddNew()
		{
			HandHygieneItem entity = base.AddNewEntity() as HandHygieneItem;

			return entity;
		}
		public HandHygieneItem FindByPrimaryKey(String transactionNo, String sROpportunity)
		{
			return base.FindByPrimaryKey(transactionNo, sROpportunity) as HandHygieneItem;
		}

		#region IEnumerable< HandHygieneItem> Members

		IEnumerator<HandHygieneItem> IEnumerable<HandHygieneItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as HandHygieneItem;
			}
		}

		#endregion

		private HandHygieneItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HandHygieneItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("HandHygieneItem ({TransactionNo, SROpportunity})")]
	[Serializable]
	public partial class HandHygieneItem : esHandHygieneItem
	{
		public HandHygieneItem()
		{
		}

		public HandHygieneItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HandHygieneItemMetadata.Meta();
			}
		}

		override protected esHandHygieneItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HandHygieneItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public HandHygieneItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HandHygieneItemQuery();
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
		public bool Load(HandHygieneItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private HandHygieneItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class HandHygieneItemQuery : esHandHygieneItemQuery
	{
		public HandHygieneItemQuery()
		{

		}

		public HandHygieneItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "HandHygieneItemQuery";
		}
	}

	[Serializable]
	public partial class HandHygieneItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HandHygieneItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.SROpportunity, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.SROpportunity;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.SRHandWashType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.SRHandWashType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.IsWearGloves, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.IsWearGloves;
			_columns.Add(c);

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.SRHandHygieneNote, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.SRHandHygieneNote;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.IsApply6Steps, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.IsApply6Steps;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HandHygieneItemMetadata.ColumnNames.SRApply6StepsResult, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = HandHygieneItemMetadata.PropertyNames.SRApply6StepsResult;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public HandHygieneItemMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SROpportunity = "SROpportunity";
			public const string SRHandWashType = "SRHandWashType";
			public const string IsWearGloves = "IsWearGloves";
			public const string SRHandHygieneNote = "SRHandHygieneNote";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsApply6Steps = "IsApply6Steps";
			public const string SRApply6StepsResult = "SRApply6StepsResult";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SROpportunity = "SROpportunity";
			public const string SRHandWashType = "SRHandWashType";
			public const string IsWearGloves = "IsWearGloves";
			public const string SRHandHygieneNote = "SRHandHygieneNote";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsApply6Steps = "IsApply6Steps";
			public const string SRApply6StepsResult = "SRApply6StepsResult";
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
			lock (typeof(HandHygieneItemMetadata))
			{
				if (HandHygieneItemMetadata.mapDelegates == null)
				{
					HandHygieneItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (HandHygieneItemMetadata.meta == null)
				{
					HandHygieneItemMetadata.meta = new HandHygieneItemMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROpportunity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRHandWashType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsWearGloves", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRHandHygieneNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApply6Steps", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRApply6StepsResult", new esTypeMap("varchar", "System.String"));


				meta.Source = "HandHygieneItem";
				meta.Destination = "HandHygieneItem";
				meta.spInsert = "proc_HandHygieneItemInsert";
				meta.spUpdate = "proc_HandHygieneItemUpdate";
				meta.spDelete = "proc_HandHygieneItemDelete";
				meta.spLoadAll = "proc_HandHygieneItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_HandHygieneItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HandHygieneItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
