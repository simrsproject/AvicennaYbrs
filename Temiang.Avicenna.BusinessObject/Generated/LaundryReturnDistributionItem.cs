/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/15/2022 9:32:28 PM
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
	abstract public class esLaundryReturnDistributionItemCollection : esEntityCollectionWAuditLog
	{
		public esLaundryReturnDistributionItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryReturnDistributionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryReturnDistributionItemQuery query)
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
			this.InitQuery(query as esLaundryReturnDistributionItemQuery);
		}
		#endregion

		virtual public LaundryReturnDistributionItem DetachEntity(LaundryReturnDistributionItem entity)
		{
			return base.DetachEntity(entity) as LaundryReturnDistributionItem;
		}

		virtual public LaundryReturnDistributionItem AttachEntity(LaundryReturnDistributionItem entity)
		{
			return base.AttachEntity(entity) as LaundryReturnDistributionItem;
		}

		virtual public void Combine(LaundryReturnDistributionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryReturnDistributionItem this[int index]
		{
			get
			{
				return base[index] as LaundryReturnDistributionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryReturnDistributionItem);
		}
	}

	[Serializable]
	abstract public class esLaundryReturnDistributionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryReturnDistributionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryReturnDistributionItem()
		{
		}

		public esLaundryReturnDistributionItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String returnNo, String seqNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(returnNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(returnNo, seqNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String returnNo, String seqNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(returnNo, seqNo);
			else
				return LoadByPrimaryKeyStoredProcedure(returnNo, seqNo);
		}

		private bool LoadByPrimaryKeyDynamic(String returnNo, String seqNo)
		{
			esLaundryReturnDistributionItemQuery query = this.GetDynamicQuery();
			query.Where(query.ReturnNo == returnNo, query.SeqNo == seqNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String returnNo, String seqNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ReturnNo", returnNo);
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
						case "ReturnNo": this.str.ReturnNo = (string)value; break;
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
		/// Maps to LaundryReturnDistributionItem.ReturnNo
		/// </summary>
		virtual public System.String ReturnNo
		{
			get
			{
				return base.GetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.ReturnNo);
			}

			set
			{
				base.SetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.ReturnNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturnDistributionItem.SeqNo
		/// </summary>
		virtual public System.String SeqNo
		{
			get
			{
				return base.GetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.SeqNo);
			}

			set
			{
				base.SetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.SeqNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturnDistributionItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturnDistributionItem.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(LaundryReturnDistributionItemMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(LaundryReturnDistributionItemMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturnDistributionItem.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.SRItemUnit);
			}

			set
			{
				base.SetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturnDistributionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryReturnDistributionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryReturnDistributionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturnDistributionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryReturnDistributionItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryReturnDistributionItem entity)
			{
				this.entity = entity;
			}
			public System.String ReturnNo
			{
				get
				{
					System.String data = entity.ReturnNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnNo = null;
					else entity.ReturnNo = Convert.ToString(value);
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
			private esLaundryReturnDistributionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryReturnDistributionItemQuery query)
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
				throw new Exception("esLaundryReturnDistributionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryReturnDistributionItem : esLaundryReturnDistributionItem
	{
	}

	[Serializable]
	abstract public class esLaundryReturnDistributionItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryReturnDistributionItemMetadata.Meta();
			}
		}

		public esQueryItem ReturnNo
		{
			get
			{
				return new esQueryItem(this, LaundryReturnDistributionItemMetadata.ColumnNames.ReturnNo, esSystemType.String);
			}
		}

		public esQueryItem SeqNo
		{
			get
			{
				return new esQueryItem(this, LaundryReturnDistributionItemMetadata.ColumnNames.SeqNo, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, LaundryReturnDistributionItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, LaundryReturnDistributionItemMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, LaundryReturnDistributionItemMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryReturnDistributionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryReturnDistributionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryReturnDistributionItemCollection")]
	public partial class LaundryReturnDistributionItemCollection : esLaundryReturnDistributionItemCollection, IEnumerable<LaundryReturnDistributionItem>
	{
		public LaundryReturnDistributionItemCollection()
		{

		}

		public static implicit operator List<LaundryReturnDistributionItem>(LaundryReturnDistributionItemCollection coll)
		{
			List<LaundryReturnDistributionItem> list = new List<LaundryReturnDistributionItem>();

			foreach (LaundryReturnDistributionItem emp in coll)
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
				return LaundryReturnDistributionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryReturnDistributionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryReturnDistributionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryReturnDistributionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryReturnDistributionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryReturnDistributionItemQuery();
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
		public bool Load(LaundryReturnDistributionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryReturnDistributionItem AddNew()
		{
			LaundryReturnDistributionItem entity = base.AddNewEntity() as LaundryReturnDistributionItem;

			return entity;
		}
		public LaundryReturnDistributionItem FindByPrimaryKey(String returnNo, String seqNo)
		{
			return base.FindByPrimaryKey(returnNo, seqNo) as LaundryReturnDistributionItem;
		}

		#region IEnumerable< LaundryReturnDistributionItem> Members

		IEnumerator<LaundryReturnDistributionItem> IEnumerable<LaundryReturnDistributionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryReturnDistributionItem;
			}
		}

		#endregion

		private LaundryReturnDistributionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryReturnDistributionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryReturnDistributionItem ({ReturnNo, SeqNo})")]
	[Serializable]
	public partial class LaundryReturnDistributionItem : esLaundryReturnDistributionItem
	{
		public LaundryReturnDistributionItem()
		{
		}

		public LaundryReturnDistributionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryReturnDistributionItemMetadata.Meta();
			}
		}

		override protected esLaundryReturnDistributionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryReturnDistributionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryReturnDistributionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryReturnDistributionItemQuery();
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
		public bool Load(LaundryReturnDistributionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryReturnDistributionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryReturnDistributionItemQuery : esLaundryReturnDistributionItemQuery
	{
		public LaundryReturnDistributionItemQuery()
		{

		}

		public LaundryReturnDistributionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryReturnDistributionItemQuery";
		}
	}

	[Serializable]
	public partial class LaundryReturnDistributionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryReturnDistributionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryReturnDistributionItemMetadata.ColumnNames.ReturnNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnDistributionItemMetadata.PropertyNames.ReturnNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnDistributionItemMetadata.ColumnNames.SeqNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnDistributionItemMetadata.PropertyNames.SeqNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnDistributionItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnDistributionItemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnDistributionItemMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryReturnDistributionItemMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnDistributionItemMetadata.ColumnNames.SRItemUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnDistributionItemMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnDistributionItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryReturnDistributionItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnDistributionItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnDistributionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryReturnDistributionItemMetadata Meta()
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
			public const string ReturnNo = "ReturnNo";
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
			public const string ReturnNo = "ReturnNo";
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
			lock (typeof(LaundryReturnDistributionItemMetadata))
			{
				if (LaundryReturnDistributionItemMetadata.mapDelegates == null)
				{
					LaundryReturnDistributionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryReturnDistributionItemMetadata.meta == null)
				{
					LaundryReturnDistributionItemMetadata.meta = new LaundryReturnDistributionItemMetadata();
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

				meta.AddTypeMap("ReturnNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundryReturnDistributionItem";
				meta.Destination = "LaundryReturnDistributionItem";
				meta.spInsert = "proc_LaundryReturnDistributionItemInsert";
				meta.spUpdate = "proc_LaundryReturnDistributionItemUpdate";
				meta.spDelete = "proc_LaundryReturnDistributionItemDelete";
				meta.spLoadAll = "proc_LaundryReturnDistributionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryReturnDistributionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryReturnDistributionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
