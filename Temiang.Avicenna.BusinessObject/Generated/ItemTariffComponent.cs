/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/2/2021 2:19:12 PM
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
	abstract public class esItemTariffComponentCollection : esEntityCollectionWAuditLog
	{
		public esItemTariffComponentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ItemTariffComponentCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffComponentQuery query)
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
			this.InitQuery(query as esItemTariffComponentQuery);
		}
		#endregion

		virtual public ItemTariffComponent DetachEntity(ItemTariffComponent entity)
		{
			return base.DetachEntity(entity) as ItemTariffComponent;
		}

		virtual public ItemTariffComponent AttachEntity(ItemTariffComponent entity)
		{
			return base.AttachEntity(entity) as ItemTariffComponent;
		}

		virtual public void Combine(ItemTariffComponentCollection collection)
		{
			base.Combine(collection);
		}

		new public ItemTariffComponent this[int index]
		{
			get
			{
				return base[index] as ItemTariffComponent;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariffComponent);
		}
	}

	[Serializable]
	abstract public class esItemTariffComponent : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffComponentQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTariffComponent()
		{
		}

		public esItemTariffComponent(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRTariffType, String itemID, String classID, DateTime startingDate, String tariffComponentID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRTariffType, itemID, classID, startingDate, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRTariffType, itemID, classID, startingDate, tariffComponentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRTariffType, String itemID, String classID, DateTime startingDate, String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRTariffType, itemID, classID, startingDate, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRTariffType, itemID, classID, startingDate, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(String sRTariffType, String itemID, String classID, DateTime startingDate, String tariffComponentID)
		{
			esItemTariffComponentQuery query = this.GetDynamicQuery();
			query.Where(query.SRTariffType == sRTariffType, query.ItemID == itemID, query.ClassID == classID, query.StartingDate == startingDate, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String sRTariffType, String itemID, String classID, DateTime startingDate, String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("SRTariffType", sRTariffType);
			parms.Add("ItemID", itemID);
			parms.Add("ClassID", classID);
			parms.Add("StartingDate", startingDate);
			parms.Add("TariffComponentID", tariffComponentID);
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
						case "SRTariffType": this.str.SRTariffType = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ClassID": this.str.ClassID = (string)value; break;
						case "StartingDate": this.str.StartingDate = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
						case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;
						case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "StartingDate":

							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
							break;
						case "Price":

							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						case "IsAllowDiscount":

							if (value == null || value is System.Boolean)
								this.IsAllowDiscount = (System.Boolean?)value;
							break;
						case "IsAllowVariable":

							if (value == null || value is System.Boolean)
								this.IsAllowVariable = (System.Boolean?)value;
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
		/// Maps to ItemTariffComponent.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentMetadata.ColumnNames.SRTariffType);
			}

			set
			{
				base.SetSystemString(ItemTariffComponentMetadata.ColumnNames.SRTariffType, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ItemTariffComponentMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentMetadata.ColumnNames.ClassID);
			}

			set
			{
				base.SetSystemString(ItemTariffComponentMetadata.ColumnNames.ClassID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffComponentMetadata.ColumnNames.StartingDate);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffComponentMetadata.ColumnNames.StartingDate, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentMetadata.ColumnNames.TariffComponentID);
			}

			set
			{
				base.SetSystemString(ItemTariffComponentMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffComponentMetadata.ColumnNames.Price);
			}

			set
			{
				base.SetSystemDecimal(ItemTariffComponentMetadata.ColumnNames.Price, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.IsAllowDiscount
		/// </summary>
		virtual public System.Boolean? IsAllowDiscount
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffComponentMetadata.ColumnNames.IsAllowDiscount);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffComponentMetadata.ColumnNames.IsAllowDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.IsAllowVariable
		/// </summary>
		virtual public System.Boolean? IsAllowVariable
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffComponentMetadata.ColumnNames.IsAllowVariable);
			}

			set
			{
				base.SetSystemBoolean(ItemTariffComponentMetadata.ColumnNames.IsAllowVariable, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffComponentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ItemTariffComponentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ItemTariffComponentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ItemTariffComponent.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(ItemTariffComponentMetadata.ColumnNames.ReferenceNo, value);
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
			public esStrings(esItemTariffComponent entity)
			{
				this.entity = entity;
			}
			public System.String SRTariffType
			{
				get
				{
					System.String data = entity.SRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffType = null;
					else entity.SRTariffType = Convert.ToString(value);
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
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
			public System.String StartingDate
			{
				get
				{
					System.DateTime? data = entity.StartingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingDate = null;
					else entity.StartingDate = Convert.ToDateTime(value);
				}
			}
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
				}
			}
			public System.String IsAllowDiscount
			{
				get
				{
					System.Boolean? data = entity.IsAllowDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowDiscount = null;
					else entity.IsAllowDiscount = Convert.ToBoolean(value);
				}
			}
			public System.String IsAllowVariable
			{
				get
				{
					System.Boolean? data = entity.IsAllowVariable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowVariable = null;
					else entity.IsAllowVariable = Convert.ToBoolean(value);
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
			private esItemTariffComponent entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffComponentQuery query)
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
				throw new Exception("esItemTariffComponent can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ItemTariffComponent : esItemTariffComponent
	{
	}

	[Serializable]
	abstract public class esItemTariffComponentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffComponentMetadata.Meta();
			}
		}

		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		}

		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		}

		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		}

		public esQueryItem IsAllowDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAllowVariable
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffComponentCollection")]
	public partial class ItemTariffComponentCollection : esItemTariffComponentCollection, IEnumerable<ItemTariffComponent>
	{
		public ItemTariffComponentCollection()
		{

		}

		public static implicit operator List<ItemTariffComponent>(ItemTariffComponentCollection coll)
		{
			List<ItemTariffComponent> list = new List<ItemTariffComponent>();

			foreach (ItemTariffComponent emp in coll)
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
				return ItemTariffComponentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffComponentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariffComponent(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariffComponent();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffComponentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffComponentQuery();
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
		public bool Load(ItemTariffComponentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ItemTariffComponent AddNew()
		{
			ItemTariffComponent entity = base.AddNewEntity() as ItemTariffComponent;

			return entity;
		}
		public ItemTariffComponent FindByPrimaryKey(String sRTariffType, String itemID, String classID, DateTime startingDate, String tariffComponentID)
		{
			return base.FindByPrimaryKey(sRTariffType, itemID, classID, startingDate, tariffComponentID) as ItemTariffComponent;
		}

		#region IEnumerable< ItemTariffComponent> Members

		IEnumerator<ItemTariffComponent> IEnumerable<ItemTariffComponent>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariffComponent;
			}
		}

		#endregion

		private ItemTariffComponentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariffComponent' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ItemTariffComponent ({SRTariffType, ItemID, ClassID, StartingDate, TariffComponentID})")]
	[Serializable]
	public partial class ItemTariffComponent : esItemTariffComponent
	{
		public ItemTariffComponent()
		{
		}

		public ItemTariffComponent(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffComponentMetadata.Meta();
			}
		}

		override protected esItemTariffComponentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffComponentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ItemTariffComponentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffComponentQuery();
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
		public bool Load(ItemTariffComponentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ItemTariffComponentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ItemTariffComponentQuery : esItemTariffComponentQuery
	{
		public ItemTariffComponentQuery()
		{

		}

		public ItemTariffComponentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ItemTariffComponentQuery";
		}
	}

	[Serializable]
	public partial class ItemTariffComponentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffComponentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.SRTariffType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.SRTariffType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.StartingDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.StartingDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.TariffComponentID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.Price, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.IsAllowDiscount, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.IsAllowDiscount;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.IsAllowVariable, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.IsAllowVariable;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(ItemTariffComponentMetadata.ColumnNames.ReferenceNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ItemTariffComponentMetadata Meta()
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
			public const string SRTariffType = "SRTariffType";
			public const string ItemID = "ItemID";
			public const string ClassID = "ClassID";
			public const string StartingDate = "StartingDate";
			public const string TariffComponentID = "TariffComponentID";
			public const string Price = "Price";
			public const string IsAllowDiscount = "IsAllowDiscount";
			public const string IsAllowVariable = "IsAllowVariable";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ReferenceNo = "ReferenceNo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SRTariffType = "SRTariffType";
			public const string ItemID = "ItemID";
			public const string ClassID = "ClassID";
			public const string StartingDate = "StartingDate";
			public const string TariffComponentID = "TariffComponentID";
			public const string Price = "Price";
			public const string IsAllowDiscount = "IsAllowDiscount";
			public const string IsAllowVariable = "IsAllowVariable";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(ItemTariffComponentMetadata))
			{
				if (ItemTariffComponentMetadata.mapDelegates == null)
				{
					ItemTariffComponentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ItemTariffComponentMetadata.meta == null)
				{
					ItemTariffComponentMetadata.meta = new ItemTariffComponentMetadata();
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

				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));


				meta.Source = "ItemTariffComponent";
				meta.Destination = "ItemTariffComponent";
				meta.spInsert = "proc_ItemTariffComponentInsert";
				meta.spUpdate = "proc_ItemTariffComponentUpdate";
				meta.spDelete = "proc_ItemTariffComponentDelete";
				meta.spLoadAll = "proc_ItemTariffComponentLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffComponentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffComponentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
