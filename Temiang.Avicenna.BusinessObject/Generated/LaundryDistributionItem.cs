/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/14/2021 2:18:18 PM
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
	abstract public class esLaundryDistributionItemCollection : esEntityCollectionWAuditLog
	{
		public esLaundryDistributionItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryDistributionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryDistributionItemQuery query)
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
			this.InitQuery(query as esLaundryDistributionItemQuery);
		}
		#endregion

		virtual public LaundryDistributionItem DetachEntity(LaundryDistributionItem entity)
		{
			return base.DetachEntity(entity) as LaundryDistributionItem;
		}

		virtual public LaundryDistributionItem AttachEntity(LaundryDistributionItem entity)
		{
			return base.AttachEntity(entity) as LaundryDistributionItem;
		}

		virtual public void Combine(LaundryDistributionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryDistributionItem this[int index]
		{
			get
			{
				return base[index] as LaundryDistributionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryDistributionItem);
		}
	}

	[Serializable]
	abstract public class esLaundryDistributionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryDistributionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryDistributionItem()
		{
		}

		public esLaundryDistributionItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String distributionNo, String seqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(distributionNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(distributionNo, seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String distributionNo, String seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(distributionNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(distributionNo, seqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String distributionNo, String seqNo)
		{
			esLaundryDistributionItemQuery query = this.GetDynamicQuery();
			query.Where(query.DistributionNo == distributionNo, query.SeqNo == seqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String distributionNo, String seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("DistributionNo", distributionNo);
			parms.Add("SeqNo", seqNo);
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
						case "DistributionNo": this.str.DistributionNo = (string)value; break;
						case "SeqNo": this.str.SeqNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
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
		/// Maps to LaundryDistributionItem.DistributionNo
		/// </summary>
		virtual public System.String DistributionNo
		{
			get
			{
				return base.GetSystemString(LaundryDistributionItemMetadata.ColumnNames.DistributionNo);
			}

			set
			{
				base.SetSystemString(LaundryDistributionItemMetadata.ColumnNames.DistributionNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistributionItem.SeqNo
		/// </summary>
		virtual public System.String SeqNo
		{
			get
			{
				return base.GetSystemString(LaundryDistributionItemMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemString(LaundryDistributionItemMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistributionItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(LaundryDistributionItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(LaundryDistributionItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistributionItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(LaundryDistributionItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(LaundryDistributionItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistributionItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(LaundryDistributionItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(LaundryDistributionItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistributionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryDistributionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryDistributionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistributionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryDistributionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryDistributionItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryDistributionItem entity)
			{
				this.entity = entity;
			}
			public System.String DistributionNo
			{
				get
				{
					System.String data = entity.DistributionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DistributionNo = null;
					else entity.DistributionNo = Convert.ToString(value);
				}
			}
			public System.String SeqNo
			{
				get
				{
					System.String data = entity.SeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeqNo = null;
					else entity.SeqNo = Convert.ToString(value);
				}
			}
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
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
			private esLaundryDistributionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryDistributionItemQuery query)
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
				throw new Exception("esLaundryDistributionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryDistributionItem : esLaundryDistributionItem
	{
	}

	[Serializable]
	abstract public class esLaundryDistributionItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryDistributionItemMetadata.Meta();
			}
		}

		public esQueryItem DistributionNo
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionItemMetadata.ColumnNames.DistributionNo, esSystemType.String);
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionItemMetadata.ColumnNames.SeqNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryDistributionItemCollection")]
	public partial class LaundryDistributionItemCollection : esLaundryDistributionItemCollection, IEnumerable<LaundryDistributionItem>
	{
		public LaundryDistributionItemCollection()
		{

		}

		public static implicit operator List<LaundryDistributionItem>(LaundryDistributionItemCollection coll)
		{
			List<LaundryDistributionItem> list = new List<LaundryDistributionItem>();

			foreach (LaundryDistributionItem emp in coll)
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
				return LaundryDistributionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryDistributionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryDistributionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryDistributionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryDistributionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryDistributionItemQuery();
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
		public bool Load(LaundryDistributionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryDistributionItem AddNew()
		{
			LaundryDistributionItem entity = base.AddNewEntity() as LaundryDistributionItem;

			return entity;
		}
		public LaundryDistributionItem FindByPrimaryKey(String distributionNo, String seqNo)
		{
			return base.FindByPrimaryKey(distributionNo, seqNo) as LaundryDistributionItem;
		}

		#region IEnumerable< LaundryDistributionItem> Members

		IEnumerator<LaundryDistributionItem> IEnumerable<LaundryDistributionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryDistributionItem;
			}
		}

		#endregion

		private LaundryDistributionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryDistributionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryDistributionItem ({DistributionNo, SeqNo})")]
	[Serializable]
	public partial class LaundryDistributionItem : esLaundryDistributionItem
	{
		public LaundryDistributionItem()
		{
		}

		public LaundryDistributionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryDistributionItemMetadata.Meta();
			}
		}

		override protected esLaundryDistributionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryDistributionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryDistributionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryDistributionItemQuery();
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
		public bool Load(LaundryDistributionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryDistributionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryDistributionItemQuery : esLaundryDistributionItemQuery
	{
		public LaundryDistributionItemQuery()
		{

		}

		public LaundryDistributionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryDistributionItemQuery";
		}
	}

	[Serializable]
	public partial class LaundryDistributionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryDistributionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryDistributionItemMetadata.ColumnNames.DistributionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionItemMetadata.PropertyNames.DistributionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionItemMetadata.ColumnNames.SeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionItemMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionItemMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryDistributionItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionItemMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryDistributionItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryDistributionItemMetadata Meta()
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
			public const string DistributionNo = "DistributionNo";
			public const string SeqNo = "SeqNo";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string DistributionNo = "DistributionNo";
			public const string SeqNo = "SeqNo";
			public const string ItemID = "ItemID";
			public const string Qty = "Qty";
			public const string SRItemUnit = "SRItemUnit";
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
			lock (typeof(LaundryDistributionItemMetadata))
			{
				if (LaundryDistributionItemMetadata.mapDelegates == null)
				{
					LaundryDistributionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryDistributionItemMetadata.meta == null)
				{
					LaundryDistributionItemMetadata.meta = new LaundryDistributionItemMetadata();
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

				meta.AddTypeMap("DistributionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundryDistributionItem";
				meta.Destination = "LaundryDistributionItem";
				meta.spInsert = "proc_LaundryDistributionItemInsert";
				meta.spUpdate = "proc_LaundryDistributionItemUpdate";
				meta.spDelete = "proc_LaundryDistributionItemDelete";
				meta.spLoadAll = "proc_LaundryDistributionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryDistributionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryDistributionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
