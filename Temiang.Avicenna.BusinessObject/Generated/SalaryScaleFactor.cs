/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/19/2022 5:07:35 PM
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
	abstract public class esSalaryScaleFactorCollection : esEntityCollectionWAuditLog
	{
		public esSalaryScaleFactorCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SalaryScaleFactorCollection";
		}

		#region Query Logic
		protected void InitQuery(esSalaryScaleFactorQuery query)
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
			this.InitQuery(query as esSalaryScaleFactorQuery);
		}
		#endregion

		virtual public SalaryScaleFactor DetachEntity(SalaryScaleFactor entity)
		{
			return base.DetachEntity(entity) as SalaryScaleFactor;
		}

		virtual public SalaryScaleFactor AttachEntity(SalaryScaleFactor entity)
		{
			return base.AttachEntity(entity) as SalaryScaleFactor;
		}

		virtual public void Combine(SalaryScaleFactorCollection collection)
		{
			base.Combine(collection);
		}

		new public SalaryScaleFactor this[int index]
		{
			get
			{
				return base[index] as SalaryScaleFactor;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SalaryScaleFactor);
		}
	}

	[Serializable]
	abstract public class esSalaryScaleFactor : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSalaryScaleFactorQuery GetDynamicQuery()
		{
			return null;
		}

		public esSalaryScaleFactor()
		{
		}

		public esSalaryScaleFactor(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 salaryScaleFactorID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryScaleFactorID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryScaleFactorID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 salaryScaleFactorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryScaleFactorID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryScaleFactorID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 salaryScaleFactorID)
		{
			esSalaryScaleFactorQuery query = this.GetDynamicQuery();
			query.Where(query.SalaryScaleFactorID == salaryScaleFactorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 salaryScaleFactorID)
		{
			esParameters parms = new esParameters();
			parms.Add("SalaryScaleFactorID", salaryScaleFactorID);
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
						case "SalaryScaleFactorID": this.str.SalaryScaleFactorID = (string)value; break;
						case "SalaryScaleID": this.str.SalaryScaleID = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SalaryScaleFactorID":

							if (value == null || value is System.Int32)
								this.SalaryScaleFactorID = (System.Int32?)value;
							break;
						case "SalaryScaleID":

							if (value == null || value is System.Int32)
								this.SalaryScaleID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "Amount":

							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
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
		/// Maps to SalaryScaleFactor.SalaryScaleFactorID
		/// </summary>
		virtual public System.Int32? SalaryScaleFactorID
		{
			get
			{
				return base.GetSystemInt32(SalaryScaleFactorMetadata.ColumnNames.SalaryScaleFactorID);
			}

			set
			{
				base.SetSystemInt32(SalaryScaleFactorMetadata.ColumnNames.SalaryScaleFactorID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScaleFactor.SalaryScaleID
		/// </summary>
		virtual public System.Int32? SalaryScaleID
		{
			get
			{
				return base.GetSystemInt32(SalaryScaleFactorMetadata.ColumnNames.SalaryScaleID);
			}

			set
			{
				base.SetSystemInt32(SalaryScaleFactorMetadata.ColumnNames.SalaryScaleID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScaleFactor.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(SalaryScaleFactorMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(SalaryScaleFactorMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScaleFactor.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(SalaryScaleFactorMetadata.ColumnNames.Amount);
			}

			set
			{
				base.SetSystemDecimal(SalaryScaleFactorMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScaleFactor.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SalaryScaleFactorMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SalaryScaleFactorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScaleFactor.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SalaryScaleFactorMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SalaryScaleFactorMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSalaryScaleFactor entity)
			{
				this.entity = entity;
			}
			public System.String SalaryScaleFactorID
			{
				get
				{
					System.Int32? data = entity.SalaryScaleFactorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryScaleFactorID = null;
					else entity.SalaryScaleFactorID = Convert.ToInt32(value);
				}
			}
			public System.String SalaryScaleID
			{
				get
				{
					System.Int32? data = entity.SalaryScaleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryScaleID = null;
					else entity.SalaryScaleID = Convert.ToInt32(value);
				}
			}
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
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
			private esSalaryScaleFactor entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSalaryScaleFactorQuery query)
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
				throw new Exception("esSalaryScaleFactor can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SalaryScaleFactor : esSalaryScaleFactor
	{
	}

	[Serializable]
	abstract public class esSalaryScaleFactorQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SalaryScaleFactorMetadata.Meta();
			}
		}

		public esQueryItem SalaryScaleFactorID
		{
			get
			{
				return new esQueryItem(this, SalaryScaleFactorMetadata.ColumnNames.SalaryScaleFactorID, esSystemType.Int32);
			}
		}

		public esQueryItem SalaryScaleID
		{
			get
			{
				return new esQueryItem(this, SalaryScaleFactorMetadata.ColumnNames.SalaryScaleID, esSystemType.Int32);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, SalaryScaleFactorMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, SalaryScaleFactorMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SalaryScaleFactorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SalaryScaleFactorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SalaryScaleFactorCollection")]
	public partial class SalaryScaleFactorCollection : esSalaryScaleFactorCollection, IEnumerable<SalaryScaleFactor>
	{
		public SalaryScaleFactorCollection()
		{

		}

		public static implicit operator List<SalaryScaleFactor>(SalaryScaleFactorCollection coll)
		{
			List<SalaryScaleFactor> list = new List<SalaryScaleFactor>();

			foreach (SalaryScaleFactor emp in coll)
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
				return SalaryScaleFactorMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryScaleFactorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SalaryScaleFactor(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SalaryScaleFactor();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SalaryScaleFactorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryScaleFactorQuery();
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
		public bool Load(SalaryScaleFactorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SalaryScaleFactor AddNew()
		{
			SalaryScaleFactor entity = base.AddNewEntity() as SalaryScaleFactor;

			return entity;
		}
		public SalaryScaleFactor FindByPrimaryKey(Int32 salaryScaleFactorID)
		{
			return base.FindByPrimaryKey(salaryScaleFactorID) as SalaryScaleFactor;
		}

		#region IEnumerable< SalaryScaleFactor> Members

		IEnumerator<SalaryScaleFactor> IEnumerable<SalaryScaleFactor>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SalaryScaleFactor;
			}
		}

		#endregion

		private SalaryScaleFactorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SalaryScaleFactor' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SalaryScaleFactor ({SalaryScaleFactorID})")]
	[Serializable]
	public partial class SalaryScaleFactor : esSalaryScaleFactor
	{
		public SalaryScaleFactor()
		{
		}

		public SalaryScaleFactor(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SalaryScaleFactorMetadata.Meta();
			}
		}

		override protected esSalaryScaleFactorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryScaleFactorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SalaryScaleFactorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryScaleFactorQuery();
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
		public bool Load(SalaryScaleFactorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SalaryScaleFactorQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SalaryScaleFactorQuery : esSalaryScaleFactorQuery
	{
		public SalaryScaleFactorQuery()
		{

		}

		public SalaryScaleFactorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SalaryScaleFactorQuery";
		}
	}

	[Serializable]
	public partial class SalaryScaleFactorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SalaryScaleFactorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SalaryScaleFactorMetadata.ColumnNames.SalaryScaleFactorID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryScaleFactorMetadata.PropertyNames.SalaryScaleFactorID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleFactorMetadata.ColumnNames.SalaryScaleID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryScaleFactorMetadata.PropertyNames.SalaryScaleID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleFactorMetadata.ColumnNames.ValidFrom, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryScaleFactorMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleFactorMetadata.ColumnNames.Amount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SalaryScaleFactorMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleFactorMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryScaleFactorMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleFactorMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryScaleFactorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SalaryScaleFactorMetadata Meta()
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
			public const string SalaryScaleFactorID = "SalaryScaleFactorID";
			public const string SalaryScaleID = "SalaryScaleID";
			public const string ValidFrom = "ValidFrom";
			public const string Amount = "Amount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SalaryScaleFactorID = "SalaryScaleFactorID";
			public const string SalaryScaleID = "SalaryScaleID";
			public const string ValidFrom = "ValidFrom";
			public const string Amount = "Amount";
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
			lock (typeof(SalaryScaleFactorMetadata))
			{
				if (SalaryScaleFactorMetadata.mapDelegates == null)
				{
					SalaryScaleFactorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SalaryScaleFactorMetadata.meta == null)
				{
					SalaryScaleFactorMetadata.meta = new SalaryScaleFactorMetadata();
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

				meta.AddTypeMap("SalaryScaleFactorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryScaleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SalaryScaleFactor";
				meta.Destination = "SalaryScaleFactor";
				meta.spInsert = "proc_SalaryScaleFactorInsert";
				meta.spUpdate = "proc_SalaryScaleFactorUpdate";
				meta.spDelete = "proc_SalaryScaleFactorDelete";
				meta.spLoadAll = "proc_SalaryScaleFactorLoadAll";
				meta.spLoadByPrimaryKey = "proc_SalaryScaleFactorLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SalaryScaleFactorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
