/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/29/2023 3:52:35 PM
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
	abstract public class esGuarantorServiceUnitPlafondCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorServiceUnitPlafondCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "GuarantorServiceUnitPlafondCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorServiceUnitPlafondQuery query)
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
			this.InitQuery(query as esGuarantorServiceUnitPlafondQuery);
		}
		#endregion

		virtual public GuarantorServiceUnitPlafond DetachEntity(GuarantorServiceUnitPlafond entity)
		{
			return base.DetachEntity(entity) as GuarantorServiceUnitPlafond;
		}

		virtual public GuarantorServiceUnitPlafond AttachEntity(GuarantorServiceUnitPlafond entity)
		{
			return base.AttachEntity(entity) as GuarantorServiceUnitPlafond;
		}

		virtual public void Combine(GuarantorServiceUnitPlafondCollection collection)
		{
			base.Combine(collection);
		}

		new public GuarantorServiceUnitPlafond this[int index]
		{
			get
			{
				return base[index] as GuarantorServiceUnitPlafond;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorServiceUnitPlafond);
		}
	}

	[Serializable]
	abstract public class esGuarantorServiceUnitPlafond : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorServiceUnitPlafondQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorServiceUnitPlafond()
		{
		}

		public esGuarantorServiceUnitPlafond(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String guarantorID, String serviceUnitID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, serviceUnitID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String guarantorID, String serviceUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID, serviceUnitID);
		}

		private bool LoadByPrimaryKeyDynamic(String guarantorID, String serviceUnitID)
		{
			esGuarantorServiceUnitPlafondQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID, query.ServiceUnitID == serviceUnitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String guarantorID, String serviceUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID", guarantorID);
			parms.Add("ServiceUnitID", serviceUnitID);
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
						case "PlafondAmount": this.str.PlafondAmount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PlafondAmount":

							if (value == null || value is System.Decimal)
								this.PlafondAmount = (System.Decimal?)value;
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
		/// Maps to GuarantorServiceUnitPlafond.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorServiceUnitPlafondMetadata.ColumnNames.GuarantorID);
			}

			set
			{
				base.SetSystemString(GuarantorServiceUnitPlafondMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorServiceUnitPlafond.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(GuarantorServiceUnitPlafondMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(GuarantorServiceUnitPlafondMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorServiceUnitPlafond.PlafondAmount
		/// </summary>
		virtual public System.Decimal? PlafondAmount
		{
			get
			{
				return base.GetSystemDecimal(GuarantorServiceUnitPlafondMetadata.ColumnNames.PlafondAmount);
			}

			set
			{
				base.SetSystemDecimal(GuarantorServiceUnitPlafondMetadata.ColumnNames.PlafondAmount, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorServiceUnitPlafond.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorServiceUnitPlafondMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(GuarantorServiceUnitPlafondMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to GuarantorServiceUnitPlafond.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorServiceUnitPlafondMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(GuarantorServiceUnitPlafondMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorServiceUnitPlafond entity)
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
			public System.String PlafondAmount
			{
				get
				{
					System.Decimal? data = entity.PlafondAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlafondAmount = null;
					else entity.PlafondAmount = Convert.ToDecimal(value);
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
			private esGuarantorServiceUnitPlafond entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorServiceUnitPlafondQuery query)
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
				throw new Exception("esGuarantorServiceUnitPlafond can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class GuarantorServiceUnitPlafond : esGuarantorServiceUnitPlafond
	{
	}

	[Serializable]
	abstract public class esGuarantorServiceUnitPlafondQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return GuarantorServiceUnitPlafondMetadata.Meta();
			}
		}

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitPlafondMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitPlafondMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem PlafondAmount
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitPlafondMetadata.ColumnNames.PlafondAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitPlafondMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorServiceUnitPlafondMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorServiceUnitPlafondCollection")]
	public partial class GuarantorServiceUnitPlafondCollection : esGuarantorServiceUnitPlafondCollection, IEnumerable<GuarantorServiceUnitPlafond>
	{
		public GuarantorServiceUnitPlafondCollection()
		{

		}

		public static implicit operator List<GuarantorServiceUnitPlafond>(GuarantorServiceUnitPlafondCollection coll)
		{
			List<GuarantorServiceUnitPlafond> list = new List<GuarantorServiceUnitPlafond>();

			foreach (GuarantorServiceUnitPlafond emp in coll)
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
				return GuarantorServiceUnitPlafondMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorServiceUnitPlafondQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorServiceUnitPlafond(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorServiceUnitPlafond();
		}

		#endregion

		[BrowsableAttribute(false)]
		public GuarantorServiceUnitPlafondQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorServiceUnitPlafondQuery();
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
		public bool Load(GuarantorServiceUnitPlafondQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public GuarantorServiceUnitPlafond AddNew()
		{
			GuarantorServiceUnitPlafond entity = base.AddNewEntity() as GuarantorServiceUnitPlafond;

			return entity;
		}
		public GuarantorServiceUnitPlafond FindByPrimaryKey(String guarantorID, String serviceUnitID)
		{
			return base.FindByPrimaryKey(guarantorID, serviceUnitID) as GuarantorServiceUnitPlafond;
		}

		#region IEnumerable< GuarantorServiceUnitPlafond> Members

		IEnumerator<GuarantorServiceUnitPlafond> IEnumerable<GuarantorServiceUnitPlafond>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorServiceUnitPlafond;
			}
		}

		#endregion

		private GuarantorServiceUnitPlafondQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorServiceUnitPlafond' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("GuarantorServiceUnitPlafond ({GuarantorID, ServiceUnitID})")]
	[Serializable]
	public partial class GuarantorServiceUnitPlafond : esGuarantorServiceUnitPlafond
	{
		public GuarantorServiceUnitPlafond()
		{
		}

		public GuarantorServiceUnitPlafond(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorServiceUnitPlafondMetadata.Meta();
			}
		}

		override protected esGuarantorServiceUnitPlafondQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorServiceUnitPlafondQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public GuarantorServiceUnitPlafondQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorServiceUnitPlafondQuery();
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
		public bool Load(GuarantorServiceUnitPlafondQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private GuarantorServiceUnitPlafondQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class GuarantorServiceUnitPlafondQuery : esGuarantorServiceUnitPlafondQuery
	{
		public GuarantorServiceUnitPlafondQuery()
		{

		}

		public GuarantorServiceUnitPlafondQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "GuarantorServiceUnitPlafondQuery";
		}
	}

	[Serializable]
	public partial class GuarantorServiceUnitPlafondMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorServiceUnitPlafondMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorServiceUnitPlafondMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorServiceUnitPlafondMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorServiceUnitPlafondMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorServiceUnitPlafondMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorServiceUnitPlafondMetadata.ColumnNames.PlafondAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorServiceUnitPlafondMetadata.PropertyNames.PlafondAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorServiceUnitPlafondMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorServiceUnitPlafondMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(GuarantorServiceUnitPlafondMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorServiceUnitPlafondMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public GuarantorServiceUnitPlafondMetadata Meta()
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
			public const string PlafondAmount = "PlafondAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string GuarantorID = "GuarantorID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string PlafondAmount = "PlafondAmount";
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
			lock (typeof(GuarantorServiceUnitPlafondMetadata))
			{
				if (GuarantorServiceUnitPlafondMetadata.mapDelegates == null)
				{
					GuarantorServiceUnitPlafondMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (GuarantorServiceUnitPlafondMetadata.meta == null)
				{
					GuarantorServiceUnitPlafondMetadata.meta = new GuarantorServiceUnitPlafondMetadata();
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
				meta.AddTypeMap("PlafondAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "GuarantorServiceUnitPlafond";
				meta.Destination = "GuarantorServiceUnitPlafond";
				meta.spInsert = "proc_GuarantorServiceUnitPlafondInsert";
				meta.spUpdate = "proc_GuarantorServiceUnitPlafondUpdate";
				meta.spDelete = "proc_GuarantorServiceUnitPlafondDelete";
				meta.spLoadAll = "proc_GuarantorServiceUnitPlafondLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorServiceUnitPlafondLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorServiceUnitPlafondMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
