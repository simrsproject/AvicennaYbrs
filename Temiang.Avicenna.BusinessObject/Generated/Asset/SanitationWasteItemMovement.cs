/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/6/2021 1:41:43 PM
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
	abstract public class esSanitationWasteItemMovementCollection : esEntityCollectionWAuditLog
	{
		public esSanitationWasteItemMovementCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationWasteItemMovementCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationWasteItemMovementQuery query)
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
			this.InitQuery(query as esSanitationWasteItemMovementQuery);
		}
		#endregion

		virtual public SanitationWasteItemMovement DetachEntity(SanitationWasteItemMovement entity)
		{
			return base.DetachEntity(entity) as SanitationWasteItemMovement;
		}

		virtual public SanitationWasteItemMovement AttachEntity(SanitationWasteItemMovement entity)
		{
			return base.AttachEntity(entity) as SanitationWasteItemMovement;
		}

		virtual public void Combine(SanitationWasteItemMovementCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationWasteItemMovement this[int index]
		{
			get
			{
				return base[index] as SanitationWasteItemMovement;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationWasteItemMovement);
		}
	}

	[Serializable]
	abstract public class esSanitationWasteItemMovement : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationWasteItemMovementQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationWasteItemMovement()
		{
		}

		public esSanitationWasteItemMovement(DataRow row)
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
			esSanitationWasteItemMovementQuery query = this.GetDynamicQuery();
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
						case "TransactionCode": this.str.TransactionCode = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SRWasteType": this.str.SRWasteType = (string)value; break;
						case "InitialQty": this.str.InitialQty = (string)value; break;
						case "QtyIn": this.str.QtyIn = (string)value; break;
						case "QtyOut": this.str.QtyOut = (string)value; break;
						case "Balance": this.str.Balance = (string)value; break;
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
						case "Balance":

							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
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
		/// Maps to SanitationWasteItemMovement.MovementID
		/// </summary>
		virtual public System.Int64? MovementID
		{
			get
			{
				return base.GetSystemInt64(SanitationWasteItemMovementMetadata.ColumnNames.MovementID);
			}

			set
			{
				base.SetSystemInt64(SanitationWasteItemMovementMetadata.ColumnNames.MovementID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.MovementDate
		/// </summary>
		virtual public System.DateTime? MovementDate
		{
			get
			{
				return base.GetSystemDateTime(SanitationWasteItemMovementMetadata.ColumnNames.MovementDate);
			}

			set
			{
				base.SetSystemDateTime(SanitationWasteItemMovementMetadata.ColumnNames.MovementDate, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.TransactionCode
		/// </summary>
		virtual public System.String TransactionCode
		{
			get
			{
				return base.GetSystemString(SanitationWasteItemMovementMetadata.ColumnNames.TransactionCode);
			}

			set
			{
				base.SetSystemString(SanitationWasteItemMovementMetadata.ColumnNames.TransactionCode, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(SanitationWasteItemMovementMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(SanitationWasteItemMovementMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.SRWasteType
		/// </summary>
		virtual public System.String SRWasteType
		{
			get
			{
				return base.GetSystemString(SanitationWasteItemMovementMetadata.ColumnNames.SRWasteType);
			}

			set
			{
				base.SetSystemString(SanitationWasteItemMovementMetadata.ColumnNames.SRWasteType, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.InitialQty
		/// </summary>
		virtual public System.Decimal? InitialQty
		{
			get
			{
				return base.GetSystemDecimal(SanitationWasteItemMovementMetadata.ColumnNames.InitialQty);
			}

			set
			{
				base.SetSystemDecimal(SanitationWasteItemMovementMetadata.ColumnNames.InitialQty, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.QtyIn
		/// </summary>
		virtual public System.Decimal? QtyIn
		{
			get
			{
				return base.GetSystemDecimal(SanitationWasteItemMovementMetadata.ColumnNames.QtyIn);
			}

			set
			{
				base.SetSystemDecimal(SanitationWasteItemMovementMetadata.ColumnNames.QtyIn, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.QtyOut
		/// </summary>
		virtual public System.Decimal? QtyOut
		{
			get
			{
				return base.GetSystemDecimal(SanitationWasteItemMovementMetadata.ColumnNames.QtyOut);
			}

			set
			{
				base.SetSystemDecimal(SanitationWasteItemMovementMetadata.ColumnNames.QtyOut, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(SanitationWasteItemMovementMetadata.ColumnNames.Balance);
			}

			set
			{
				base.SetSystemDecimal(SanitationWasteItemMovementMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationWasteItemMovementMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationWasteItemMovementMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemMovement.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationWasteItemMovementMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationWasteItemMovementMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationWasteItemMovement entity)
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
			public System.String SRWasteType
			{
				get
				{
					System.String data = entity.SRWasteType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWasteType = null;
					else entity.SRWasteType = Convert.ToString(value);
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
			public System.String Balance
			{
				get
				{
					System.Decimal? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToDecimal(value);
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
			private esSanitationWasteItemMovement entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationWasteItemMovementQuery query)
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
				throw new Exception("esSanitationWasteItemMovement can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationWasteItemMovement : esSanitationWasteItemMovement
	{
	}

	[Serializable]
	abstract public class esSanitationWasteItemMovementQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationWasteItemMovementMetadata.Meta();
			}
		}

		public esQueryItem MovementID
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.MovementID, esSystemType.Int64);
			}
		}

		public esQueryItem MovementDate
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.MovementDate, esSystemType.DateTime);
			}
		}

		public esQueryItem TransactionCode
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.TransactionCode, esSystemType.String);
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SRWasteType
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.SRWasteType, esSystemType.String);
			}
		}

		public esQueryItem InitialQty
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.InitialQty, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyIn
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.QtyIn, esSystemType.Decimal);
			}
		}

		public esQueryItem QtyOut
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.QtyOut, esSystemType.Decimal);
			}
		}

		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemMovementMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationWasteItemMovementCollection")]
	public partial class SanitationWasteItemMovementCollection : esSanitationWasteItemMovementCollection, IEnumerable<SanitationWasteItemMovement>
	{
		public SanitationWasteItemMovementCollection()
		{

		}

		public static implicit operator List<SanitationWasteItemMovement>(SanitationWasteItemMovementCollection coll)
		{
			List<SanitationWasteItemMovement> list = new List<SanitationWasteItemMovement>();

			foreach (SanitationWasteItemMovement emp in coll)
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
				return SanitationWasteItemMovementMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationWasteItemMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationWasteItemMovement(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationWasteItemMovement();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationWasteItemMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationWasteItemMovementQuery();
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
		public bool Load(SanitationWasteItemMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationWasteItemMovement AddNew()
		{
			SanitationWasteItemMovement entity = base.AddNewEntity() as SanitationWasteItemMovement;

			return entity;
		}
		public SanitationWasteItemMovement FindByPrimaryKey(Int64 movementID)
		{
			return base.FindByPrimaryKey(movementID) as SanitationWasteItemMovement;
		}

		#region IEnumerable< SanitationWasteItemMovement> Members

		IEnumerator<SanitationWasteItemMovement> IEnumerable<SanitationWasteItemMovement>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationWasteItemMovement;
			}
		}

		#endregion

		private SanitationWasteItemMovementQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationWasteItemMovement' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationWasteItemMovement ({MovementID})")]
	[Serializable]
	public partial class SanitationWasteItemMovement : esSanitationWasteItemMovement
	{
		public SanitationWasteItemMovement()
		{
		}

		public SanitationWasteItemMovement(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationWasteItemMovementMetadata.Meta();
			}
		}

		override protected esSanitationWasteItemMovementQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationWasteItemMovementQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationWasteItemMovementQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationWasteItemMovementQuery();
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
		public bool Load(SanitationWasteItemMovementQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationWasteItemMovementQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationWasteItemMovementQuery : esSanitationWasteItemMovementQuery
	{
		public SanitationWasteItemMovementQuery()
		{

		}

		public SanitationWasteItemMovementQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationWasteItemMovementQuery";
		}
	}

	[Serializable]
	public partial class SanitationWasteItemMovementMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationWasteItemMovementMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.MovementID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.MovementID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.MovementDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.MovementDate;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.TransactionCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.TransactionCode;
			c.CharacterMaxLength = 1;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.TransactionNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.SRWasteType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.SRWasteType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.InitialQty, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.InitialQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.QtyIn, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.QtyIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.QtyOut, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.QtyOut;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.Balance, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.Balance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemMovementMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteItemMovementMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationWasteItemMovementMetadata Meta()
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
			public const string TransactionCode = "TransactionCode";
			public const string TransactionNo = "TransactionNo";
			public const string SRWasteType = "SRWasteType";
			public const string InitialQty = "InitialQty";
			public const string QtyIn = "QtyIn";
			public const string QtyOut = "QtyOut";
			public const string Balance = "Balance";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MovementID = "MovementID";
			public const string MovementDate = "MovementDate";
			public const string TransactionCode = "TransactionCode";
			public const string TransactionNo = "TransactionNo";
			public const string SRWasteType = "SRWasteType";
			public const string InitialQty = "InitialQty";
			public const string QtyIn = "QtyIn";
			public const string QtyOut = "QtyOut";
			public const string Balance = "Balance";
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
			lock (typeof(SanitationWasteItemMovementMetadata))
			{
				if (SanitationWasteItemMovementMetadata.mapDelegates == null)
				{
					SanitationWasteItemMovementMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationWasteItemMovementMetadata.meta == null)
				{
					SanitationWasteItemMovementMetadata.meta = new SanitationWasteItemMovementMetadata();
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
				meta.AddTypeMap("TransactionCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRWasteType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InitialQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QtyOut", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationWasteItemMovement";
				meta.Destination = "SanitationWasteItemMovement";
				meta.spInsert = "proc_SanitationWasteItemMovementInsert";
				meta.spUpdate = "proc_SanitationWasteItemMovementUpdate";
				meta.spDelete = "proc_SanitationWasteItemMovementDelete";
				meta.spLoadAll = "proc_SanitationWasteItemMovementLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationWasteItemMovementLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationWasteItemMovementMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
