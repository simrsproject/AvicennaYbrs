/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/23/2022 5:32:32 PM
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
	abstract public class esApdSurveyItemCollection : esEntityCollectionWAuditLog
	{
		public esApdSurveyItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ApdSurveyItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esApdSurveyItemQuery query)
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
			this.InitQuery(query as esApdSurveyItemQuery);
		}
		#endregion

		virtual public ApdSurveyItem DetachEntity(ApdSurveyItem entity)
		{
			return base.DetachEntity(entity) as ApdSurveyItem;
		}

		virtual public ApdSurveyItem AttachEntity(ApdSurveyItem entity)
		{
			return base.AttachEntity(entity) as ApdSurveyItem;
		}

		virtual public void Combine(ApdSurveyItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ApdSurveyItem this[int index]
		{
			get
			{
				return base[index] as ApdSurveyItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApdSurveyItem);
		}
	}

	[Serializable]
	abstract public class esApdSurveyItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApdSurveyItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esApdSurveyItem()
		{
		}

		public esApdSurveyItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sRApdType)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRApdType);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRApdType);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sRApdType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sRApdType);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sRApdType);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sRApdType)
		{
			esApdSurveyItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SRApdType == sRApdType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sRApdType)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SRApdType", sRApdType);
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
						case "SRApdType": this.str.SRApdType = (string)value; break;
						case "IsCorrectIndication": this.str.IsCorrectIndication = (string)value; break;
						case "IsCorrectUsageTime": this.str.IsCorrectUsageTime = (string)value; break;
						case "IsCorrectUsageTechnique": this.str.IsCorrectUsageTechnique = (string)value; break;
						case "IsCorrectReleaseTechnique": this.str.IsCorrectReleaseTechnique = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsCorrectIndication":

							if (value == null || value is System.Boolean)
								this.IsCorrectIndication = (System.Boolean?)value;
							break;
						case "IsCorrectUsageTime":

							if (value == null || value is System.Boolean)
								this.IsCorrectUsageTime = (System.Boolean?)value;
							break;
						case "IsCorrectUsageTechnique":

							if (value == null || value is System.Boolean)
								this.IsCorrectUsageTechnique = (System.Boolean?)value;
							break;
						case "IsCorrectReleaseTechnique":

							if (value == null || value is System.Boolean)
								this.IsCorrectReleaseTechnique = (System.Boolean?)value;
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
		/// Maps to ApdSurveyItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ApdSurveyItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(ApdSurveyItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ApdSurveyItem.SRApdType
		/// </summary>
		virtual public System.String SRApdType
		{
			get
			{
				return base.GetSystemString(ApdSurveyItemMetadata.ColumnNames.SRApdType);
			}

			set
			{
				base.SetSystemString(ApdSurveyItemMetadata.ColumnNames.SRApdType, value);
			}
		}
		/// <summary>
		/// Maps to ApdSurveyItem.IsCorrectIndication
		/// </summary>
		virtual public System.Boolean? IsCorrectIndication
		{
			get
			{
				return base.GetSystemBoolean(ApdSurveyItemMetadata.ColumnNames.IsCorrectIndication);
			}

			set
			{
				base.SetSystemBoolean(ApdSurveyItemMetadata.ColumnNames.IsCorrectIndication, value);
			}
		}
		/// <summary>
		/// Maps to ApdSurveyItem.IsCorrectUsageTime
		/// </summary>
		virtual public System.Boolean? IsCorrectUsageTime
		{
			get
			{
				return base.GetSystemBoolean(ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTime);
			}

			set
			{
				base.SetSystemBoolean(ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTime, value);
			}
		}
		/// <summary>
		/// Maps to ApdSurveyItem.IsCorrectUsageTechnique
		/// </summary>
		virtual public System.Boolean? IsCorrectUsageTechnique
		{
			get
			{
				return base.GetSystemBoolean(ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTechnique);
			}

			set
			{
				base.SetSystemBoolean(ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTechnique, value);
			}
		}
		/// <summary>
		/// Maps to ApdSurveyItem.IsCorrectReleaseTechnique
		/// </summary>
		virtual public System.Boolean? IsCorrectReleaseTechnique
		{
			get
			{
				return base.GetSystemBoolean(ApdSurveyItemMetadata.ColumnNames.IsCorrectReleaseTechnique);
			}

			set
			{
				base.SetSystemBoolean(ApdSurveyItemMetadata.ColumnNames.IsCorrectReleaseTechnique, value);
			}
		}
		/// <summary>
		/// Maps to ApdSurveyItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApdSurveyItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ApdSurveyItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ApdSurveyItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApdSurveyItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ApdSurveyItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApdSurveyItem entity)
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
			public System.String SRApdType
			{
				get
				{
					System.String data = entity.SRApdType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRApdType = null;
					else entity.SRApdType = Convert.ToString(value);
				}
			}
			public System.String IsCorrectIndication
			{
				get
				{
					System.Boolean? data = entity.IsCorrectIndication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCorrectIndication = null;
					else entity.IsCorrectIndication = Convert.ToBoolean(value);
				}
			}
			public System.String IsCorrectUsageTime
			{
				get
				{
					System.Boolean? data = entity.IsCorrectUsageTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCorrectUsageTime = null;
					else entity.IsCorrectUsageTime = Convert.ToBoolean(value);
				}
			}
			public System.String IsCorrectUsageTechnique
			{
				get
				{
					System.Boolean? data = entity.IsCorrectUsageTechnique;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCorrectUsageTechnique = null;
					else entity.IsCorrectUsageTechnique = Convert.ToBoolean(value);
				}
			}
			public System.String IsCorrectReleaseTechnique
			{
				get
				{
					System.Boolean? data = entity.IsCorrectReleaseTechnique;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCorrectReleaseTechnique = null;
					else entity.IsCorrectReleaseTechnique = Convert.ToBoolean(value);
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
			private esApdSurveyItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApdSurveyItemQuery query)
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
				throw new Exception("esApdSurveyItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ApdSurveyItem : esApdSurveyItem
	{
	}

	[Serializable]
	abstract public class esApdSurveyItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ApdSurveyItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ApdSurveyItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRApdType
		{
			get
			{
				return new esQueryItem(this, ApdSurveyItemMetadata.ColumnNames.SRApdType, esSystemType.String);
			}
		}

		public esQueryItem IsCorrectIndication
		{
			get
			{
				return new esQueryItem(this, ApdSurveyItemMetadata.ColumnNames.IsCorrectIndication, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCorrectUsageTime
		{
			get
			{
				return new esQueryItem(this, ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTime, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCorrectUsageTechnique
		{
			get
			{
				return new esQueryItem(this, ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTechnique, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCorrectReleaseTechnique
		{
			get
			{
				return new esQueryItem(this, ApdSurveyItemMetadata.ColumnNames.IsCorrectReleaseTechnique, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApdSurveyItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApdSurveyItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApdSurveyItemCollection")]
	public partial class ApdSurveyItemCollection : esApdSurveyItemCollection, IEnumerable<ApdSurveyItem>
	{
		public ApdSurveyItemCollection()
		{

		}

		public static implicit operator List<ApdSurveyItem>(ApdSurveyItemCollection coll)
		{
			List<ApdSurveyItem> list = new List<ApdSurveyItem>();

			foreach (ApdSurveyItem emp in coll)
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
				return ApdSurveyItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApdSurveyItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApdSurveyItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApdSurveyItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ApdSurveyItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApdSurveyItemQuery();
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
		public bool Load(ApdSurveyItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ApdSurveyItem AddNew()
		{
			ApdSurveyItem entity = base.AddNewEntity() as ApdSurveyItem;

			return entity;
		}
		public ApdSurveyItem FindByPrimaryKey(String transactionNo, String sRApdType)
		{
			return base.FindByPrimaryKey(transactionNo, sRApdType) as ApdSurveyItem;
		}

		#region IEnumerable< ApdSurveyItem> Members

		IEnumerator<ApdSurveyItem> IEnumerable<ApdSurveyItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ApdSurveyItem;
			}
		}

		#endregion

		private ApdSurveyItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApdSurveyItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ApdSurveyItem ({TransactionNo, SRApdType})")]
	[Serializable]
	public partial class ApdSurveyItem : esApdSurveyItem
	{
		public ApdSurveyItem()
		{
		}

		public ApdSurveyItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApdSurveyItemMetadata.Meta();
			}
		}

		override protected esApdSurveyItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApdSurveyItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ApdSurveyItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApdSurveyItemQuery();
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
		public bool Load(ApdSurveyItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ApdSurveyItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ApdSurveyItemQuery : esApdSurveyItemQuery
	{
		public ApdSurveyItemQuery()
		{

		}

		public ApdSurveyItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ApdSurveyItemQuery";
		}
	}

	[Serializable]
	public partial class ApdSurveyItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApdSurveyItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApdSurveyItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ApdSurveyItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ApdSurveyItemMetadata.ColumnNames.SRApdType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ApdSurveyItemMetadata.PropertyNames.SRApdType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ApdSurveyItemMetadata.ColumnNames.IsCorrectIndication, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ApdSurveyItemMetadata.PropertyNames.IsCorrectIndication;
			_columns.Add(c);

			c = new esColumnMetadata(ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTime, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ApdSurveyItemMetadata.PropertyNames.IsCorrectUsageTime;
			_columns.Add(c);

			c = new esColumnMetadata(ApdSurveyItemMetadata.ColumnNames.IsCorrectUsageTechnique, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ApdSurveyItemMetadata.PropertyNames.IsCorrectUsageTechnique;
			_columns.Add(c);

			c = new esColumnMetadata(ApdSurveyItemMetadata.ColumnNames.IsCorrectReleaseTechnique, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ApdSurveyItemMetadata.PropertyNames.IsCorrectReleaseTechnique;
			_columns.Add(c);

			c = new esColumnMetadata(ApdSurveyItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApdSurveyItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ApdSurveyItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ApdSurveyItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ApdSurveyItemMetadata Meta()
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
			public const string SRApdType = "SRApdType";
			public const string IsCorrectIndication = "IsCorrectIndication";
			public const string IsCorrectUsageTime = "IsCorrectUsageTime";
			public const string IsCorrectUsageTechnique = "IsCorrectUsageTechnique";
			public const string IsCorrectReleaseTechnique = "IsCorrectReleaseTechnique";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SRApdType = "SRApdType";
			public const string IsCorrectIndication = "IsCorrectIndication";
			public const string IsCorrectUsageTime = "IsCorrectUsageTime";
			public const string IsCorrectUsageTechnique = "IsCorrectUsageTechnique";
			public const string IsCorrectReleaseTechnique = "IsCorrectReleaseTechnique";
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
			lock (typeof(ApdSurveyItemMetadata))
			{
				if (ApdSurveyItemMetadata.mapDelegates == null)
				{
					ApdSurveyItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ApdSurveyItemMetadata.meta == null)
				{
					ApdSurveyItemMetadata.meta = new ApdSurveyItemMetadata();
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
				meta.AddTypeMap("SRApdType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCorrectIndication", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCorrectUsageTime", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCorrectUsageTechnique", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCorrectReleaseTechnique", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ApdSurveyItem";
				meta.Destination = "ApdSurveyItem";
				meta.spInsert = "proc_ApdSurveyItemInsert";
				meta.spUpdate = "proc_ApdSurveyItemUpdate";
				meta.spDelete = "proc_ApdSurveyItemDelete";
				meta.spLoadAll = "proc_ApdSurveyItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApdSurveyItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApdSurveyItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
