/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/27/2023 5:28:13 PM
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
	abstract public class esClosingVisiteDownPaymentItemCollection : esEntityCollectionWAuditLog
	{
		public esClosingVisiteDownPaymentItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ClosingVisiteDownPaymentItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esClosingVisiteDownPaymentItemQuery query)
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
			this.InitQuery(query as esClosingVisiteDownPaymentItemQuery);
		}
		#endregion

		virtual public ClosingVisiteDownPaymentItem DetachEntity(ClosingVisiteDownPaymentItem entity)
		{
			return base.DetachEntity(entity) as ClosingVisiteDownPaymentItem;
		}

		virtual public ClosingVisiteDownPaymentItem AttachEntity(ClosingVisiteDownPaymentItem entity)
		{
			return base.AttachEntity(entity) as ClosingVisiteDownPaymentItem;
		}

		virtual public void Combine(ClosingVisiteDownPaymentItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ClosingVisiteDownPaymentItem this[int index]
		{
			get
			{
				return base[index] as ClosingVisiteDownPaymentItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClosingVisiteDownPaymentItem);
		}
	}

	[Serializable]
	abstract public class esClosingVisiteDownPaymentItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClosingVisiteDownPaymentItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esClosingVisiteDownPaymentItem()
		{
		}

		public esClosingVisiteDownPaymentItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String closingNo, String paymentNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(closingNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(closingNo, paymentNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String closingNo, String paymentNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(closingNo, paymentNo);
			else
				return LoadByPrimaryKeyStoredProcedure(closingNo, paymentNo);
		}

		private bool LoadByPrimaryKeyDynamic(String closingNo, String paymentNo)
		{
			esClosingVisiteDownPaymentItemQuery query = this.GetDynamicQuery();
			query.Where(query.ClosingNo == closingNo, query.PaymentNo == paymentNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String closingNo, String paymentNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ClosingNo", closingNo);
			parms.Add("PaymentNo", paymentNo);
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
						case "ClosingNo": this.str.ClosingNo = (string)value; break;
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Amount":

							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
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
		/// Maps to ClosingVisiteDownPaymentItem.ClosingNo
		/// </summary>
		virtual public System.String ClosingNo
		{
			get
			{
				return base.GetSystemString(ClosingVisiteDownPaymentItemMetadata.ColumnNames.ClosingNo);
			}

			set
			{
				base.SetSystemString(ClosingVisiteDownPaymentItemMetadata.ColumnNames.ClosingNo, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPaymentItem.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(ClosingVisiteDownPaymentItemMetadata.ColumnNames.PaymentNo);
			}

			set
			{
				base.SetSystemString(ClosingVisiteDownPaymentItemMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPaymentItem.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(ClosingVisiteDownPaymentItemMetadata.ColumnNames.Amount);
			}

			set
			{
				base.SetSystemDecimal(ClosingVisiteDownPaymentItemMetadata.ColumnNames.Amount, value);
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
			public esStrings(esClosingVisiteDownPaymentItem entity)
			{
				this.entity = entity;
			}
			public System.String ClosingNo
			{
				get
				{
					System.String data = entity.ClosingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosingNo = null;
					else entity.ClosingNo = Convert.ToString(value);
				}
			}
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
				}
			}
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
				}
			}
			private esClosingVisiteDownPaymentItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClosingVisiteDownPaymentItemQuery query)
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
				throw new Exception("esClosingVisiteDownPaymentItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ClosingVisiteDownPaymentItem : esClosingVisiteDownPaymentItem
	{
	}

	[Serializable]
	abstract public class esClosingVisiteDownPaymentItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ClosingVisiteDownPaymentItemMetadata.Meta();
			}
		}

		public esQueryItem ClosingNo
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentItemMetadata.ColumnNames.ClosingNo, esSystemType.String);
			}
		}

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentItemMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		}

		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClosingVisiteDownPaymentItemCollection")]
	public partial class ClosingVisiteDownPaymentItemCollection : esClosingVisiteDownPaymentItemCollection, IEnumerable<ClosingVisiteDownPaymentItem>
	{
		public ClosingVisiteDownPaymentItemCollection()
		{

		}

		public static implicit operator List<ClosingVisiteDownPaymentItem>(ClosingVisiteDownPaymentItemCollection coll)
		{
			List<ClosingVisiteDownPaymentItem> list = new List<ClosingVisiteDownPaymentItem>();

			foreach (ClosingVisiteDownPaymentItem emp in coll)
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
				return ClosingVisiteDownPaymentItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClosingVisiteDownPaymentItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClosingVisiteDownPaymentItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClosingVisiteDownPaymentItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ClosingVisiteDownPaymentItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClosingVisiteDownPaymentItemQuery();
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
		public bool Load(ClosingVisiteDownPaymentItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ClosingVisiteDownPaymentItem AddNew()
		{
			ClosingVisiteDownPaymentItem entity = base.AddNewEntity() as ClosingVisiteDownPaymentItem;

			return entity;
		}
		public ClosingVisiteDownPaymentItem FindByPrimaryKey(String closingNo, String paymentNo)
		{
			return base.FindByPrimaryKey(closingNo, paymentNo) as ClosingVisiteDownPaymentItem;
		}

		#region IEnumerable< ClosingVisiteDownPaymentItem> Members

		IEnumerator<ClosingVisiteDownPaymentItem> IEnumerable<ClosingVisiteDownPaymentItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ClosingVisiteDownPaymentItem;
			}
		}

		#endregion

		private ClosingVisiteDownPaymentItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClosingVisiteDownPaymentItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ClosingVisiteDownPaymentItem ({ClosingNo, PaymentNo})")]
	[Serializable]
	public partial class ClosingVisiteDownPaymentItem : esClosingVisiteDownPaymentItem
	{
		public ClosingVisiteDownPaymentItem()
		{
		}

		public ClosingVisiteDownPaymentItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClosingVisiteDownPaymentItemMetadata.Meta();
			}
		}

		override protected esClosingVisiteDownPaymentItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClosingVisiteDownPaymentItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ClosingVisiteDownPaymentItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClosingVisiteDownPaymentItemQuery();
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
		public bool Load(ClosingVisiteDownPaymentItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ClosingVisiteDownPaymentItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ClosingVisiteDownPaymentItemQuery : esClosingVisiteDownPaymentItemQuery
	{
		public ClosingVisiteDownPaymentItemQuery()
		{

		}

		public ClosingVisiteDownPaymentItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ClosingVisiteDownPaymentItemQuery";
		}
	}

	[Serializable]
	public partial class ClosingVisiteDownPaymentItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClosingVisiteDownPaymentItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClosingVisiteDownPaymentItemMetadata.ColumnNames.ClosingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingVisiteDownPaymentItemMetadata.PropertyNames.ClosingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentItemMetadata.ColumnNames.PaymentNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingVisiteDownPaymentItemMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentItemMetadata.ColumnNames.Amount, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ClosingVisiteDownPaymentItemMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);


		}
		#endregion

		static public ClosingVisiteDownPaymentItemMetadata Meta()
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
			public const string ClosingNo = "ClosingNo";
			public const string PaymentNo = "PaymentNo";
			public const string Amount = "Amount";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ClosingNo = "ClosingNo";
			public const string PaymentNo = "PaymentNo";
			public const string Amount = "Amount";
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
			lock (typeof(ClosingVisiteDownPaymentItemMetadata))
			{
				if (ClosingVisiteDownPaymentItemMetadata.mapDelegates == null)
				{
					ClosingVisiteDownPaymentItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ClosingVisiteDownPaymentItemMetadata.meta == null)
				{
					ClosingVisiteDownPaymentItemMetadata.meta = new ClosingVisiteDownPaymentItemMetadata();
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

				meta.AddTypeMap("ClosingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "ClosingVisiteDownPaymentItem";
				meta.Destination = "ClosingVisiteDownPaymentItem";
				meta.spInsert = "proc_ClosingVisiteDownPaymentItemInsert";
				meta.spUpdate = "proc_ClosingVisiteDownPaymentItemUpdate";
				meta.spDelete = "proc_ClosingVisiteDownPaymentItemDelete";
				meta.spLoadAll = "proc_ClosingVisiteDownPaymentItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClosingVisiteDownPaymentItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClosingVisiteDownPaymentItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
