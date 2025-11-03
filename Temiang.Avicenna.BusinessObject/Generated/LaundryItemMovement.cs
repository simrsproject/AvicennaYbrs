/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/15/2022 9:31:31 PM
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
	abstract public class esLaundryItemMovementCollection : esEntityCollectionWAuditLog
	{
		public esLaundryItemMovementCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryItemMovementCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryItemMovementQuery query)
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
			this.InitQuery(query as esLaundryItemMovementQuery);
		}
		#endregion

		virtual public LaundryItemMovement DetachEntity(LaundryItemMovement entity)
		{
			return base.DetachEntity(entity) as LaundryItemMovement;
		}

		virtual public LaundryItemMovement AttachEntity(LaundryItemMovement entity)
		{
			return base.AttachEntity(entity) as LaundryItemMovement;
		}

		virtual public void Combine(LaundryItemMovementCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryItemMovement this[int index]
		{
			get
			{
				return base[index] as LaundryItemMovement;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryItemMovement);
		}
	}

	[Serializable]
	abstract public class esLaundryItemMovement : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryItemMovementQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryItemMovement()
		{
		}

		public esLaundryItemMovement(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 movementID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(movementID);
			else
				return LoadByPrimaryKeyStoredProcedure(movementID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 movementID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(movementID);
			else
				return LoadByPrimaryKeyStoredProcedure(movementID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 movementID)
		{
			esLaundryItemMovementQuery query = this.GetDynamicQuery();
			query.Where(query.MovementID == movementID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 movementID)
		{
			esParameters parms = new esParameters();
			parms.Add("MovementID", movementID);
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
						case "MovementID": this.str.MovementID = (string)value; break;
						case "MovementDate": this.str.MovementDate = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "TransactionCode": this.str.TransactionCode = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "IsCleanLaundry": this.str.IsCleanLaundry = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "InitialQty": this.str.InitialQty = (string)value; break;
						case "QtyIn": this.str.QtyIn = (string)value; break;
						case "QtyOut": this.str.QtyOut = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MovementID":

							if (value == null || value is System.Int64)
								this.MovementID = (System.Int64?)value;
							break;
						case "MovementDate":

							if (value == null || value is System.DateTime)
								this.MovementDate = (System.DateTime?)value;
							break;
						case "IsCleanLaundry":

							if (value == null || value is System.Boolean)
								this.IsCleanLaundry = (System.Boolean?)value;
							break;
						case "InitialQty":

							if (value == null || value is System.Decimal)
								this.InitialQty = (System.Decimal?)value;
							break;
						case "QtyIn":

							if (value == null || value is System.Decimal)
								this.QtyIn = (System.Decimal?)value;
							break;
						case "QtyOut":

							if (value == null || value is System.Decimal)
								this.QtyOut = (System.Decimal?)value;
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
		/// Maps to LaundryItemMovement.MovementID
		/// </summary>
		virtual public System.Int64? MovementID
		{
			get
			{
				return base.GetSystemInt64(LaundryItemMovementMetadata.ColumnNames.MovementID);
			}

			set
			{
				base.SetSystemInt64(LaundryItemMovementMetadata.ColumnNames.MovementID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.MovementDate
		/// </summary>
		virtual public System.DateTime? MovementDate
		{
			get
			{
				return base.GetSystemDateTime(LaundryItemMovementMetadata.ColumnNames.MovementDate);
			}

			set
			{
				base.SetSystemDateTime(LaundryItemMovementMetadata.ColumnNames.MovementDate, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(LaundryItemMovementMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(LaundryItemMovementMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.TransactionCode
		/// </summary>
		virtual public System.String TransactionCode
		{
			get
			{
				return base.GetSystemString(LaundryItemMovementMetadata.ColumnNames.TransactionCode);
			}

			set
			{
				base.SetSystemString(LaundryItemMovementMetadata.ColumnNames.TransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(LaundryItemMovementMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(LaundryItemMovementMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.IsCleanLaundry
		/// </summary>
		virtual public System.Boolean? IsCleanLaundry
		{
			get
			{
				return base.GetSystemBoolean(LaundryItemMovementMetadata.ColumnNames.IsCleanLaundry);
			}

			set
			{
				base.SetSystemBoolean(LaundryItemMovementMetadata.ColumnNames.IsCleanLaundry, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(LaundryItemMovementMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(LaundryItemMovementMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.InitialQty
		/// </summary>
		virtual public System.Decimal? InitialQty
		{
			get
			{
				return base.GetSystemDecimal(LaundryItemMovementMetadata.ColumnNames.InitialQty);
			}

			set
			{
				base.SetSystemDecimal(LaundryItemMovementMetadata.ColumnNames.InitialQty, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.QtyIn
		/// </summary>
		virtual public System.Decimal? QtyIn
		{
			get
			{
				return base.GetSystemDecimal(LaundryItemMovementMetadata.ColumnNames.QtyIn);
			}

			set
			{
				base.SetSystemDecimal(LaundryItemMovementMetadata.ColumnNames.QtyIn, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.QtyOut
		/// </summary>
		virtual public System.Decimal? QtyOut
		{
			get
			{
				return base.GetSystemDecimal(LaundryItemMovementMetadata.ColumnNames.QtyOut);
			}

			set
			{
				base.SetSystemDecimal(LaundryItemMovementMetadata.ColumnNames.QtyOut, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryItemMovementMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryItemMovementMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryItemMovement.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryItemMovementMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryItemMovementMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryItemMovement entity)
			{
				this.entity = entity;
			}
			public System.String MovementID
			{
				get
				{
					System.Int64? data = entity.MovementID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MovementID = null;
					else entity.MovementID = Convert.ToInt64(value);
				}
			}
			public System.String MovementDate
			{
				get
				{
					System.DateTime? data = entity.MovementDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MovementDate = null;
					else entity.MovementDate = Convert.ToDateTime(value);
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
			public System.String TransactionCode
			{
				get
				{
					System.String data = entity.TransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionCode = null;
					else entity.TransactionCode = Convert.ToString(value);
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
			public System.String IsCleanLaundry
			{
				get
				{
					System.Boolean? data = entity.IsCleanLaundry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCleanLaundry = null;
					else entity.IsCleanLaundry = Convert.ToBoolean(value);
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
			public System.String InitialQty
			{
				get
				{
					System.Decimal? data = entity.InitialQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialQty = null;
					else entity.InitialQty = Convert.ToDecimal(value);
				}
			}
			public System.String QtyIn
			{
				get
				{
					System.Decimal? data = entity.QtyIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyIn = null;
					else entity.QtyIn = Convert.ToDecimal(value);
				}
			}
			public System.String QtyOut
			{
				get
				{
					System.Decimal? data = entity.QtyOut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyOut = null;
					else entity.QtyOut = Convert.ToDecimal(value);
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
			private esLaundryItemMovement entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryItemMovementQuery query)
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
				throw new Exception("esLaundryItemMovement can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryItemMovement : esLaundryItemMovement
	{
	}

	[Serializable]
	abstract public class esLaundryItemMovementQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryItemMovementMetadata.Meta();
			}
		}

		public esQueryItem MovementID
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.MovementID, esSystemType.Int64);
			}
		}

		public esQueryItem MovementDate
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.MovementDate, esSystemType.DateTime);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionCode
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.TransactionCode, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem IsCleanLaundry
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.IsCleanLaundry, esSystemType.Boolean);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem InitialQty
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.InitialQty, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyIn
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.QtyIn, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyOut
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.QtyOut, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryItemMovementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryItemMovementCollection")]
	public partial class LaundryItemMovementCollection : esLaundryItemMovementCollection, IEnumerable<LaundryItemMovement>
	{
		public LaundryItemMovementCollection()
		{

		}

		public static implicit operator List<LaundryItemMovement>(LaundryItemMovementCollection coll)
		{
			List<LaundryItemMovement> list = new List<LaundryItemMovement>();

			foreach (LaundryItemMovement emp in coll)
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
				return LaundryItemMovementMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryItemMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryItemMovement(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryItemMovement();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryItemMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryItemMovementQuery();
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
		public bool Load(LaundryItemMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryItemMovement AddNew()
		{
			LaundryItemMovement entity = base.AddNewEntity() as LaundryItemMovement;

			return entity;
		}
		public LaundryItemMovement FindByPrimaryKey(Int64 movementID)
		{
			return base.FindByPrimaryKey(movementID) as LaundryItemMovement;
		}

		#region IEnumerable< LaundryItemMovement> Members

		IEnumerator<LaundryItemMovement> IEnumerable<LaundryItemMovement>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryItemMovement;
			}
		}

		#endregion

		private LaundryItemMovementQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryItemMovement' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryItemMovement ({MovementID})")]
	[Serializable]
	public partial class LaundryItemMovement : esLaundryItemMovement
	{
		public LaundryItemMovement()
		{
		}

		public LaundryItemMovement(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryItemMovementMetadata.Meta();
			}
		}

		override protected esLaundryItemMovementQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryItemMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryItemMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryItemMovementQuery();
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
		public bool Load(LaundryItemMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryItemMovementQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryItemMovementQuery : esLaundryItemMovementQuery
	{
		public LaundryItemMovementQuery()
		{

		}

		public LaundryItemMovementQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryItemMovementQuery";
		}
	}

	[Serializable]
	public partial class LaundryItemMovementMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryItemMovementMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.MovementID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.MovementID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.MovementDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.MovementDate;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.TransactionNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.TransactionCode, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.TransactionCode;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.IsCleanLaundry, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.IsCleanLaundry;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.ItemID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.InitialQty, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.InitialQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.QtyIn, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.QtyIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.QtyOut, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.QtyOut;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryItemMovementMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryItemMovementMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryItemMovementMetadata Meta()
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
			public const string MovementID = "MovementID";
			public const string MovementDate = "MovementDate";
			public const string TransactionNo = "TransactionNo";
			public const string TransactionCode = "TransactionCode";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string IsCleanLaundry = "IsCleanLaundry";
			public const string ItemID = "ItemID";
			public const string InitialQty = "InitialQty";
			public const string QtyIn = "QtyIn";
			public const string QtyOut = "QtyOut";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MovementID = "MovementID";
			public const string MovementDate = "MovementDate";
			public const string TransactionNo = "TransactionNo";
			public const string TransactionCode = "TransactionCode";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string IsCleanLaundry = "IsCleanLaundry";
			public const string ItemID = "ItemID";
			public const string InitialQty = "InitialQty";
			public const string QtyIn = "QtyIn";
			public const string QtyOut = "QtyOut";
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
			lock (typeof(LaundryItemMovementMetadata))
			{
				if (LaundryItemMovementMetadata.mapDelegates == null)
				{
					LaundryItemMovementMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryItemMovementMetadata.meta == null)
				{
					LaundryItemMovementMetadata.meta = new LaundryItemMovementMetadata();
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

				meta.AddTypeMap("MovementID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("MovementDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCleanLaundry", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InitialQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyOut", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundryItemMovement";
				meta.Destination = "LaundryItemMovement";
				meta.spInsert = "proc_LaundryItemMovementInsert";
				meta.spUpdate = "proc_LaundryItemMovementUpdate";
				meta.spDelete = "proc_LaundryItemMovementDelete";
				meta.spLoadAll = "proc_LaundryItemMovementLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryItemMovementLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryItemMovementMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
