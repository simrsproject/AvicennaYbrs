/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/3/2021 2:15:21 PM
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
	abstract public class esTransChargesExtramuralItemsCollection : esEntityCollectionWAuditLog
	{
		public esTransChargesExtramuralItemsCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "TransChargesExtramuralItemsCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransChargesExtramuralItemsQuery query)
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
			this.InitQuery(query as esTransChargesExtramuralItemsQuery);
		}
		#endregion

		virtual public TransChargesExtramuralItems DetachEntity(TransChargesExtramuralItems entity)
		{
			return base.DetachEntity(entity) as TransChargesExtramuralItems;
		}

		virtual public TransChargesExtramuralItems AttachEntity(TransChargesExtramuralItems entity)
		{
			return base.AttachEntity(entity) as TransChargesExtramuralItems;
		}

		virtual public void Combine(TransChargesExtramuralItemsCollection collection)
		{
			base.Combine(collection);
		}

		new public TransChargesExtramuralItems this[int index]
		{
			get
			{
				return base[index] as TransChargesExtramuralItems;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransChargesExtramuralItems);
		}
	}

	[Serializable]
	abstract public class esTransChargesExtramuralItems : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransChargesExtramuralItemsQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransChargesExtramuralItems()
		{
		}

		public esTransChargesExtramuralItems(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 iD)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 iD)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(iD);
			else
				return LoadByPrimaryKeyStoredProcedure(iD);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 iD)
		{
			esTransChargesExtramuralItemsQuery query = this.GetDynamicQuery();
			query.Where(query.ID == iD);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 iD)
		{
			esParameters parms = new esParameters();
			parms.Add("ID", iD);
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
						case "ID": this.str.ID = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SRExtramuralItem": this.str.SRExtramuralItem = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "LeasingPeriodInDays": this.str.LeasingPeriodInDays = (string)value; break;
						case "IsReturned": this.str.IsReturned = (string)value; break;
						case "ReturnDate": this.str.ReturnDate = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "GuarantyAmount": this.str.GuarantyAmount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ID":

							if (value == null || value is System.Int64)
								this.ID = (System.Int64?)value;
							break;
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						case "LeasingPeriodInDays":

							if (value == null || value is System.Int32)
								this.LeasingPeriodInDays = (System.Int32?)value;
							break;
						case "IsReturned":

							if (value == null || value is System.Boolean)
								this.IsReturned = (System.Boolean?)value;
							break;
						case "ReturnDate":

							if (value == null || value is System.DateTime)
								this.ReturnDate = (System.DateTime?)value;
							break;
						case "CreateDateTime":

							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "GuarantyAmount":

							if (value == null || value is System.Decimal)
								this.GuarantyAmount = (System.Decimal?)value;
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
		/// Maps to TransChargesExtramuralItems.ID
		/// </summary>
		virtual public System.Int64? ID
		{
			get
			{
				return base.GetSystemInt64(TransChargesExtramuralItemsMetadata.ColumnNames.ID);
			}

			set
			{
				base.SetSystemInt64(TransChargesExtramuralItemsMetadata.ColumnNames.ID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(TransChargesExtramuralItemsMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(TransChargesExtramuralItemsMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.SRExtramuralItem
		/// </summary>
		virtual public System.String SRExtramuralItem
		{
			get
			{
				return base.GetSystemString(TransChargesExtramuralItemsMetadata.ColumnNames.SRExtramuralItem);
			}

			set
			{
				base.SetSystemString(TransChargesExtramuralItemsMetadata.ColumnNames.SRExtramuralItem, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(TransChargesExtramuralItemsMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(TransChargesExtramuralItemsMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.LeasingPeriodInDays
		/// </summary>
		virtual public System.Int32? LeasingPeriodInDays
		{
			get
			{
				return base.GetSystemInt32(TransChargesExtramuralItemsMetadata.ColumnNames.LeasingPeriodInDays);
			}

			set
			{
				base.SetSystemInt32(TransChargesExtramuralItemsMetadata.ColumnNames.LeasingPeriodInDays, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.IsReturned
		/// </summary>
		virtual public System.Boolean? IsReturned
		{
			get
			{
				return base.GetSystemBoolean(TransChargesExtramuralItemsMetadata.ColumnNames.IsReturned);
			}

			set
			{
				base.SetSystemBoolean(TransChargesExtramuralItemsMetadata.ColumnNames.IsReturned, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.ReturnDate
		/// </summary>
		virtual public System.DateTime? ReturnDate
		{
			get
			{
				return base.GetSystemDateTime(TransChargesExtramuralItemsMetadata.ColumnNames.ReturnDate);
			}

			set
			{
				base.SetSystemDateTime(TransChargesExtramuralItemsMetadata.ColumnNames.ReturnDate, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesExtramuralItemsMetadata.ColumnNames.CreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesExtramuralItemsMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesExtramuralItemsMetadata.ColumnNames.CreateByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesExtramuralItemsMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransChargesExtramuralItemsMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(TransChargesExtramuralItemsMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransChargesExtramuralItemsMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(TransChargesExtramuralItemsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to TransChargesExtramuralItems.GuarantyAmount
		/// </summary>
		virtual public System.Decimal? GuarantyAmount
		{
			get
			{
				return base.GetSystemDecimal(TransChargesExtramuralItemsMetadata.ColumnNames.GuarantyAmount);
			}

			set
			{
				base.SetSystemDecimal(TransChargesExtramuralItemsMetadata.ColumnNames.GuarantyAmount, value);
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
			public esStrings(esTransChargesExtramuralItems entity)
			{
				this.entity = entity;
			}
			public System.String ID
			{
				get
				{
					System.Int64? data = entity.ID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ID = null;
					else entity.ID = Convert.ToInt64(value);
				}
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
			public System.String SRExtramuralItem
			{
				get
				{
					System.String data = entity.SRExtramuralItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRExtramuralItem = null;
					else entity.SRExtramuralItem = Convert.ToString(value);
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
			public System.String LeasingPeriodInDays
			{
				get
				{
					System.Int32? data = entity.LeasingPeriodInDays;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeasingPeriodInDays = null;
					else entity.LeasingPeriodInDays = Convert.ToInt32(value);
				}
			}
			public System.String IsReturned
			{
				get
				{
					System.Boolean? data = entity.IsReturned;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReturned = null;
					else entity.IsReturned = Convert.ToBoolean(value);
				}
			}
			public System.String ReturnDate
			{
				get
				{
					System.DateTime? data = entity.ReturnDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnDate = null;
					else entity.ReturnDate = Convert.ToDateTime(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
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
			public System.String GuarantyAmount
			{
				get
				{
					System.Decimal? data = entity.GuarantyAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantyAmount = null;
					else entity.GuarantyAmount = Convert.ToDecimal(value);
				}
			}
			private esTransChargesExtramuralItems entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransChargesExtramuralItemsQuery query)
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
				throw new Exception("esTransChargesExtramuralItems can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class TransChargesExtramuralItems : esTransChargesExtramuralItems
	{
	}

	[Serializable]
	abstract public class esTransChargesExtramuralItemsQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return TransChargesExtramuralItemsMetadata.Meta();
			}
		}

		public esQueryItem ID
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.ID, esSystemType.Int64);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRExtramuralItem
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.SRExtramuralItem, esSystemType.String);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem LeasingPeriodInDays
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.LeasingPeriodInDays, esSystemType.Int32);
			}
		}

		public esQueryItem IsReturned
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.IsReturned, esSystemType.Boolean);
			}
		}

		public esQueryItem ReturnDate
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.ReturnDate, esSystemType.DateTime);
			}
		}

		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem GuarantyAmount
		{
			get
			{
				return new esQueryItem(this, TransChargesExtramuralItemsMetadata.ColumnNames.GuarantyAmount, esSystemType.Decimal);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransChargesExtramuralItemsCollection")]
	public partial class TransChargesExtramuralItemsCollection : esTransChargesExtramuralItemsCollection, IEnumerable<TransChargesExtramuralItems>
	{
		public TransChargesExtramuralItemsCollection()
		{

		}

		public static implicit operator List<TransChargesExtramuralItems>(TransChargesExtramuralItemsCollection coll)
		{
			List<TransChargesExtramuralItems> list = new List<TransChargesExtramuralItems>();

			foreach (TransChargesExtramuralItems emp in coll)
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
				return TransChargesExtramuralItemsMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesExtramuralItemsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransChargesExtramuralItems(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransChargesExtramuralItems();
		}

		#endregion

		[BrowsableAttribute(false)]
		public TransChargesExtramuralItemsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesExtramuralItemsQuery();
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
		public bool Load(TransChargesExtramuralItemsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public TransChargesExtramuralItems AddNew()
		{
			TransChargesExtramuralItems entity = base.AddNewEntity() as TransChargesExtramuralItems;

			return entity;
		}
		public TransChargesExtramuralItems FindByPrimaryKey(Int64 iD)
		{
			return base.FindByPrimaryKey(iD) as TransChargesExtramuralItems;
		}

		#region IEnumerable< TransChargesExtramuralItems> Members

		IEnumerator<TransChargesExtramuralItems> IEnumerable<TransChargesExtramuralItems>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as TransChargesExtramuralItems;
			}
		}

		#endregion

		private TransChargesExtramuralItemsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransChargesExtramuralItems' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("TransChargesExtramuralItems ({ID})")]
	[Serializable]
	public partial class TransChargesExtramuralItems : esTransChargesExtramuralItems
	{
		public TransChargesExtramuralItems()
		{
		}

		public TransChargesExtramuralItems(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransChargesExtramuralItemsMetadata.Meta();
			}
		}

		override protected esTransChargesExtramuralItemsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransChargesExtramuralItemsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public TransChargesExtramuralItemsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransChargesExtramuralItemsQuery();
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
		public bool Load(TransChargesExtramuralItemsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private TransChargesExtramuralItemsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class TransChargesExtramuralItemsQuery : esTransChargesExtramuralItemsQuery
	{
		public TransChargesExtramuralItemsQuery()
		{

		}

		public TransChargesExtramuralItemsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransChargesExtramuralItemsQuery";
		}
	}

	[Serializable]
	public partial class TransChargesExtramuralItemsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransChargesExtramuralItemsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.ID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.ID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.SRExtramuralItem, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.SRExtramuralItem;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.Qty, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.LeasingPeriodInDays, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.LeasingPeriodInDays;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.IsReturned, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.IsReturned;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.ReturnDate, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.ReturnDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.CreateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.CreateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(TransChargesExtramuralItemsMetadata.ColumnNames.GuarantyAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransChargesExtramuralItemsMetadata.PropertyNames.GuarantyAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public TransChargesExtramuralItemsMetadata Meta()
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
			public const string ID = "ID";
			public const string TransactionNo = "TransactionNo";
			public const string SRExtramuralItem = "SRExtramuralItem";
			public const string Qty = "Qty";
			public const string LeasingPeriodInDays = "LeasingPeriodInDays";
			public const string IsReturned = "IsReturned";
			public const string ReturnDate = "ReturnDate";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string GuarantyAmount = "GuarantyAmount";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ID = "ID";
			public const string TransactionNo = "TransactionNo";
			public const string SRExtramuralItem = "SRExtramuralItem";
			public const string Qty = "Qty";
			public const string LeasingPeriodInDays = "LeasingPeriodInDays";
			public const string IsReturned = "IsReturned";
			public const string ReturnDate = "ReturnDate";
			public const string CreateDateTime = "CreateDateTime";
			public const string CreateByUserID = "CreateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string GuarantyAmount = "GuarantyAmount";
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
			lock (typeof(TransChargesExtramuralItemsMetadata))
			{
				if (TransChargesExtramuralItemsMetadata.mapDelegates == null)
				{
					TransChargesExtramuralItemsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (TransChargesExtramuralItemsMetadata.meta == null)
				{
					TransChargesExtramuralItemsMetadata.meta = new TransChargesExtramuralItemsMetadata();
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

				meta.AddTypeMap("ID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRExtramuralItem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("LeasingPeriodInDays", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsReturned", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReturnDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantyAmount", new esTypeMap("numeric", "System.Decimal"));


				meta.Source = "TransChargesExtramuralItems";
				meta.Destination = "TransChargesExtramuralItems";
				meta.spInsert = "proc_TransChargesExtramuralItemsInsert";
				meta.spUpdate = "proc_TransChargesExtramuralItemsUpdate";
				meta.spDelete = "proc_TransChargesExtramuralItemsDelete";
				meta.spLoadAll = "proc_TransChargesExtramuralItemsLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransChargesExtramuralItemsLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransChargesExtramuralItemsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
