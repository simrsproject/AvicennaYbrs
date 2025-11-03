/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/13/2023 1:11:09 PM
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
	abstract public class esGuarantorAutoBillItemCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorAutoBillItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "GuarantorAutoBillItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorAutoBillItemQuery query)
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
			this.InitQuery(query as esGuarantorAutoBillItemQuery);
		}
		#endregion

		virtual public GuarantorAutoBillItem DetachEntity(GuarantorAutoBillItem entity)
		{
			return base.DetachEntity(entity) as GuarantorAutoBillItem;
		}

		virtual public GuarantorAutoBillItem AttachEntity(GuarantorAutoBillItem entity)
		{
			return base.AttachEntity(entity) as GuarantorAutoBillItem;
		}

		virtual public void Combine(GuarantorAutoBillItemCollection collection)
		{
			base.Combine(collection);
		}

		new public GuarantorAutoBillItem this[int index]
		{
			get
			{
				return base[index] as GuarantorAutoBillItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorAutoBillItem);
		}
	}

	[Serializable]
	abstract public class esGuarantorAutoBillItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorAutoBillItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorAutoBillItem()
		{
		}

		public esGuarantorAutoBillItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String guarantorID, String serviceUnitID, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, serviceUnitID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, serviceUnitID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String guarantorID, String serviceUnitID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, serviceUnitID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, serviceUnitID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String guarantorID, String serviceUnitID, String itemID)
		{
			esGuarantorAutoBillItemQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.ServiceUnitID == serviceUnitID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String guarantorID, String serviceUnitID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID", guarantorID);
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("ItemID", itemID);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "Quantity": this.str.Quantity = (string)value; break;
						case "IsGenerateOnRegistration": this.str.IsGenerateOnRegistration = (string)value; break;
						case "IsGenerateOnNewRegistration": this.str.IsGenerateOnNewRegistration = (string)value; break;
						case "IsGenerateOnReferral": this.str.IsGenerateOnReferral = (string)value; break;
						case "IsGenerateOnFirstRegistration": this.str.IsGenerateOnFirstRegistration = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Quantity":

							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						case "IsGenerateOnRegistration":

							if (value == null || value is System.Boolean)
								this.IsGenerateOnRegistration = (System.Boolean?)value;
							break;
						case "IsGenerateOnNewRegistration":

							if (value == null || value is System.Boolean)
								this.IsGenerateOnNewRegistration = (System.Boolean?)value;
							break;
						case "IsGenerateOnReferral":

							if (value == null || value is System.Boolean)
								this.IsGenerateOnReferral = (System.Boolean?)value;
							break;
						case "IsGenerateOnFirstRegistration":

							if (value == null || value is System.Boolean)
								this.IsGenerateOnFirstRegistration = (System.Boolean?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to GuarantorAutoBillItem.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorAutoBillItemMetadata.ColumnNames.GuarantorID);
			}

			set
			{
				base.SetSystemString(GuarantorAutoBillItemMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(GuarantorAutoBillItemMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(GuarantorAutoBillItemMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(GuarantorAutoBillItemMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(GuarantorAutoBillItemMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(GuarantorAutoBillItemMetadata.ColumnNames.Quantity);
			}

			set
			{
				base.SetSystemDecimal(GuarantorAutoBillItemMetadata.ColumnNames.Quantity, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.IsGenerateOnRegistration
		/// </summary>
		virtual public System.Boolean? IsGenerateOnRegistration
		{
			get
			{
				return base.GetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration);
			}

			set
			{
				base.SetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.IsGenerateOnNewRegistration
		/// </summary>
		virtual public System.Boolean? IsGenerateOnNewRegistration
		{
			get
			{
				return base.GetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration);
			}

			set
			{
				base.SetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.IsGenerateOnReferral
		/// </summary>
		virtual public System.Boolean? IsGenerateOnReferral
		{
			get
			{
				return base.GetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral);
			}

			set
			{
				base.SetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.IsGenerateOnFirstRegistration
		/// </summary>
		virtual public System.Boolean? IsGenerateOnFirstRegistration
		{
			get
			{
				return base.GetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration);
			}

			set
			{
				base.SetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(GuarantorAutoBillItemMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorAutoBillItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(GuarantorAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorAutoBillItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorAutoBillItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(GuarantorAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorAutoBillItem entity)
			{
				this.entity = entity;
			}
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
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
			public System.String Quantity
			{
				get
				{
					System.Decimal? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToDecimal(value);
				}
			}
			public System.String IsGenerateOnRegistration
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnRegistration = null;
					else entity.IsGenerateOnRegistration = Convert.ToBoolean(value);
				}
			}
			public System.String IsGenerateOnNewRegistration
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnNewRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnNewRegistration = null;
					else entity.IsGenerateOnNewRegistration = Convert.ToBoolean(value);
				}
			}
			public System.String IsGenerateOnReferral
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnReferral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnReferral = null;
					else entity.IsGenerateOnReferral = Convert.ToBoolean(value);
				}
			}
			public System.String IsGenerateOnFirstRegistration
			{
				get
				{
					System.Boolean? data = entity.IsGenerateOnFirstRegistration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateOnFirstRegistration = null;
					else entity.IsGenerateOnFirstRegistration = Convert.ToBoolean(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			private esGuarantorAutoBillItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorAutoBillItemQuery query)
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
				throw new Exception("esGuarantorAutoBillItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class GuarantorAutoBillItem : esGuarantorAutoBillItem
	{
	}

	[Serializable]
	abstract public class esGuarantorAutoBillItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return GuarantorAutoBillItemMetadata.Meta();
			}
		}

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		}

		public esQueryItem IsGenerateOnRegistration
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, esSystemType.Boolean);
			}
		}

		public esQueryItem IsGenerateOnNewRegistration
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration, esSystemType.Boolean);
			}
		}

		public esQueryItem IsGenerateOnReferral
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, esSystemType.Boolean);
			}
		}

		public esQueryItem IsGenerateOnFirstRegistration
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration, esSystemType.Boolean);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorAutoBillItemCollection")]
	public partial class GuarantorAutoBillItemCollection : esGuarantorAutoBillItemCollection, IEnumerable<GuarantorAutoBillItem>
	{
		public GuarantorAutoBillItemCollection()
		{

		}

		public static implicit operator List<GuarantorAutoBillItem>(GuarantorAutoBillItemCollection coll)
		{
			List<GuarantorAutoBillItem> list = new List<GuarantorAutoBillItem>();

			foreach (GuarantorAutoBillItem emp in coll)
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
				return GuarantorAutoBillItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorAutoBillItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorAutoBillItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorAutoBillItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public GuarantorAutoBillItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorAutoBillItemQuery();
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
		public bool Load(GuarantorAutoBillItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public GuarantorAutoBillItem AddNew()
		{
			GuarantorAutoBillItem entity = base.AddNewEntity() as GuarantorAutoBillItem;

			return entity;
		}
		public GuarantorAutoBillItem FindByPrimaryKey(String guarantorID, String serviceUnitID, String itemID)
		{
			return base.FindByPrimaryKey(guarantorID, serviceUnitID, itemID) as GuarantorAutoBillItem;
		}

		#region IEnumerable< GuarantorAutoBillItem> Members

		IEnumerator<GuarantorAutoBillItem> IEnumerable<GuarantorAutoBillItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorAutoBillItem;
			}
		}

		#endregion

		private GuarantorAutoBillItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorAutoBillItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("GuarantorAutoBillItem ({GuarantorID, ServiceUnitID, ItemID})")]
	[Serializable]
	public partial class GuarantorAutoBillItem : esGuarantorAutoBillItem
	{
		public GuarantorAutoBillItem()
		{
		}

		public GuarantorAutoBillItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorAutoBillItemMetadata.Meta();
			}
		}

		override protected esGuarantorAutoBillItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorAutoBillItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public GuarantorAutoBillItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorAutoBillItemQuery();
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
		public bool Load(GuarantorAutoBillItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private GuarantorAutoBillItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class GuarantorAutoBillItemQuery : esGuarantorAutoBillItemQuery
	{
		public GuarantorAutoBillItemQuery()
		{

		}

		public GuarantorAutoBillItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "GuarantorAutoBillItemQuery";
		}
	}

	[Serializable]
	public partial class GuarantorAutoBillItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorAutoBillItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.Quantity, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnRegistration, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.IsGenerateOnRegistration;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnNewRegistration, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.IsGenerateOnNewRegistration;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnReferral, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.IsGenerateOnReferral;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.IsGenerateOnFirstRegistration, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.IsGenerateOnFirstRegistration;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.IsActive, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorAutoBillItemMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorAutoBillItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public GuarantorAutoBillItemMetadata Meta()
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
			public const string GuarantorID = "GuarantorID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string Quantity = "Quantity";
			public const string IsGenerateOnRegistration = "IsGenerateOnRegistration";
			public const string IsGenerateOnNewRegistration = "IsGenerateOnNewRegistration";
			public const string IsGenerateOnReferral = "IsGenerateOnReferral";
			public const string IsGenerateOnFirstRegistration = "IsGenerateOnFirstRegistration";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string GuarantorID = "GuarantorID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string Quantity = "Quantity";
			public const string IsGenerateOnRegistration = "IsGenerateOnRegistration";
			public const string IsGenerateOnNewRegistration = "IsGenerateOnNewRegistration";
			public const string IsGenerateOnReferral = "IsGenerateOnReferral";
			public const string IsGenerateOnFirstRegistration = "IsGenerateOnFirstRegistration";
			public const string IsActive = "IsActive";
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
			lock (typeof(GuarantorAutoBillItemMetadata))
			{
				if (GuarantorAutoBillItemMetadata.mapDelegates == null)
				{
					GuarantorAutoBillItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (GuarantorAutoBillItemMetadata.meta == null)
				{
					GuarantorAutoBillItemMetadata.meta = new GuarantorAutoBillItemMetadata();
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

				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsGenerateOnRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnNewRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnReferral", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateOnFirstRegistration", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "GuarantorAutoBillItem";
				meta.Destination = "GuarantorAutoBillItem";
				meta.spInsert = "proc_GuarantorAutoBillItemInsert";
				meta.spUpdate = "proc_GuarantorAutoBillItemUpdate";
				meta.spDelete = "proc_GuarantorAutoBillItemDelete";
				meta.spLoadAll = "proc_GuarantorAutoBillItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorAutoBillItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorAutoBillItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
