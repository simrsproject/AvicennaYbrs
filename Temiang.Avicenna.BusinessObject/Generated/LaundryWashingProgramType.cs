/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/31/2021 9:02:16 PM
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
	abstract public class esLaundryWashingProgramTypeCollection : esEntityCollectionWAuditLog
	{
		public esLaundryWashingProgramTypeCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryWashingProgramTypeCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryWashingProgramTypeQuery query)
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
			this.InitQuery(query as esLaundryWashingProgramTypeQuery);
		}
		#endregion

		virtual public LaundryWashingProgramType DetachEntity(LaundryWashingProgramType entity)
		{
			return base.DetachEntity(entity) as LaundryWashingProgramType;
		}

		virtual public LaundryWashingProgramType AttachEntity(LaundryWashingProgramType entity)
		{
			return base.AttachEntity(entity) as LaundryWashingProgramType;
		}

		virtual public void Combine(LaundryWashingProgramTypeCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryWashingProgramType this[int index]
		{
			get
			{
				return base[index] as LaundryWashingProgramType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryWashingProgramType);
		}
	}

	[Serializable]
	abstract public class esLaundryWashingProgramType : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryWashingProgramTypeQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryWashingProgramType()
		{
		}

		public esLaundryWashingProgramType(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 laundryProgramTypeID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(laundryProgramTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(laundryProgramTypeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 laundryProgramTypeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(laundryProgramTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(laundryProgramTypeID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 laundryProgramTypeID)
		{
			esLaundryWashingProgramTypeQuery query = this.GetDynamicQuery();
			query.Where(query.LaundryProgramTypeID == laundryProgramTypeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 laundryProgramTypeID)
		{
			esParameters parms = new esParameters();
			parms.Add("LaundryProgramTypeID", laundryProgramTypeID);
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
						case "LaundryProgramTypeID": this.str.LaundryProgramTypeID = (string)value; break;
						case "SRLaundryProcessType": this.str.SRLaundryProcessType = (string)value; break;
						case "SRLaundryProgram": this.str.SRLaundryProgram = (string)value; break;
						case "SRLaundryType": this.str.SRLaundryType = (string)value; break;
						case "Weight": this.str.Weight = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LaundryProgramTypeID":

							if (value == null || value is System.Int32)
								this.LaundryProgramTypeID = (System.Int32?)value;
							break;
						case "Weight":

							if (value == null || value is System.Decimal)
								this.Weight = (System.Decimal?)value;
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
		/// Maps to LaundryWashingProgramType.LaundryProgramTypeID
		/// </summary>
		virtual public System.Int32? LaundryProgramTypeID
		{
			get
			{
				return base.GetSystemInt32(LaundryWashingProgramTypeMetadata.ColumnNames.LaundryProgramTypeID);
			}

			set
			{
				base.SetSystemInt32(LaundryWashingProgramTypeMetadata.ColumnNames.LaundryProgramTypeID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramType.SRLaundryProcessType
		/// </summary>
		virtual public System.String SRLaundryProcessType
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryProcessType);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryProcessType, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramType.SRLaundryProgram
		/// </summary>
		virtual public System.String SRLaundryProgram
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryProgram);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryProgram, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramType.SRLaundryType
		/// </summary>
		virtual public System.String SRLaundryType
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryType);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryType, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramType.Weight
		/// </summary>
		virtual public System.Decimal? Weight
		{
			get
			{
				return base.GetSystemDecimal(LaundryWashingProgramTypeMetadata.ColumnNames.Weight);
			}

			set
			{
				base.SetSystemDecimal(LaundryWashingProgramTypeMetadata.ColumnNames.Weight, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramType.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryWashingProgramTypeMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryWashingProgramTypeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingProgramType.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryWashingProgramTypeMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryWashingProgramTypeMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryWashingProgramType entity)
			{
				this.entity = entity;
			}
			public System.String LaundryProgramTypeID
			{
				get
				{
					System.Int32? data = entity.LaundryProgramTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LaundryProgramTypeID = null;
					else entity.LaundryProgramTypeID = Convert.ToInt32(value);
				}
			}
			public System.String SRLaundryProcessType
			{
				get
				{
					System.String data = entity.SRLaundryProcessType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryProcessType = null;
					else entity.SRLaundryProcessType = Convert.ToString(value);
				}
			}
			public System.String SRLaundryProgram
			{
				get
				{
					System.String data = entity.SRLaundryProgram;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryProgram = null;
					else entity.SRLaundryProgram = Convert.ToString(value);
				}
			}
			public System.String SRLaundryType
			{
				get
				{
					System.String data = entity.SRLaundryType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLaundryType = null;
					else entity.SRLaundryType = Convert.ToString(value);
				}
			}
			public System.String Weight
			{
				get
				{
					System.Decimal? data = entity.Weight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Weight = null;
					else entity.Weight = Convert.ToDecimal(value);
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
			private esLaundryWashingProgramType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryWashingProgramTypeQuery query)
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
				throw new Exception("esLaundryWashingProgramType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryWashingProgramType : esLaundryWashingProgramType
	{
	}

	[Serializable]
	abstract public class esLaundryWashingProgramTypeQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryWashingProgramTypeMetadata.Meta();
			}
		}

		public esQueryItem LaundryProgramTypeID
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeMetadata.ColumnNames.LaundryProgramTypeID, esSystemType.Int32);
			}
		}

		public esQueryItem SRLaundryProcessType
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryProcessType, esSystemType.String);
			}
		}

		public esQueryItem SRLaundryProgram
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryProgram, esSystemType.String);
			}
		}

		public esQueryItem SRLaundryType
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryType, esSystemType.String);
			}
		}

		public esQueryItem Weight
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeMetadata.ColumnNames.Weight, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryWashingProgramTypeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryWashingProgramTypeCollection")]
	public partial class LaundryWashingProgramTypeCollection : esLaundryWashingProgramTypeCollection, IEnumerable<LaundryWashingProgramType>
	{
		public LaundryWashingProgramTypeCollection()
		{

		}

		public static implicit operator List<LaundryWashingProgramType>(LaundryWashingProgramTypeCollection coll)
		{
			List<LaundryWashingProgramType> list = new List<LaundryWashingProgramType>();

			foreach (LaundryWashingProgramType emp in coll)
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
				return LaundryWashingProgramTypeMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryWashingProgramTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryWashingProgramType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryWashingProgramType();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryWashingProgramTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryWashingProgramTypeQuery();
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
		public bool Load(LaundryWashingProgramTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryWashingProgramType AddNew()
		{
			LaundryWashingProgramType entity = base.AddNewEntity() as LaundryWashingProgramType;

			return entity;
		}
		public LaundryWashingProgramType FindByPrimaryKey(Int32 laundryProgramTypeID)
		{
			return base.FindByPrimaryKey(laundryProgramTypeID) as LaundryWashingProgramType;
		}

		#region IEnumerable< LaundryWashingProgramType> Members

		IEnumerator<LaundryWashingProgramType> IEnumerable<LaundryWashingProgramType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryWashingProgramType;
			}
		}

		#endregion

		private LaundryWashingProgramTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryWashingProgramType' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryWashingProgramType ({LaundryProgramTypeID})")]
	[Serializable]
	public partial class LaundryWashingProgramType : esLaundryWashingProgramType
	{
		public LaundryWashingProgramType()
		{
		}

		public LaundryWashingProgramType(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryWashingProgramTypeMetadata.Meta();
			}
		}

		override protected esLaundryWashingProgramTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryWashingProgramTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryWashingProgramTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryWashingProgramTypeQuery();
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
		public bool Load(LaundryWashingProgramTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryWashingProgramTypeQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryWashingProgramTypeQuery : esLaundryWashingProgramTypeQuery
	{
		public LaundryWashingProgramTypeQuery()
		{

		}

		public LaundryWashingProgramTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryWashingProgramTypeQuery";
		}
	}

	[Serializable]
	public partial class LaundryWashingProgramTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryWashingProgramTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryWashingProgramTypeMetadata.ColumnNames.LaundryProgramTypeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = LaundryWashingProgramTypeMetadata.PropertyNames.LaundryProgramTypeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryProcessType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeMetadata.PropertyNames.SRLaundryProcessType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryProgram, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeMetadata.PropertyNames.SRLaundryProgram;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeMetadata.ColumnNames.SRLaundryType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeMetadata.PropertyNames.SRLaundryType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeMetadata.ColumnNames.Weight, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = LaundryWashingProgramTypeMetadata.PropertyNames.Weight;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryWashingProgramTypeMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingProgramTypeMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingProgramTypeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryWashingProgramTypeMetadata Meta()
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
			public const string LaundryProgramTypeID = "LaundryProgramTypeID";
			public const string SRLaundryProcessType = "SRLaundryProcessType";
			public const string SRLaundryProgram = "SRLaundryProgram";
			public const string SRLaundryType = "SRLaundryType";
			public const string Weight = "Weight";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LaundryProgramTypeID = "LaundryProgramTypeID";
			public const string SRLaundryProcessType = "SRLaundryProcessType";
			public const string SRLaundryProgram = "SRLaundryProgram";
			public const string SRLaundryType = "SRLaundryType";
			public const string Weight = "Weight";
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
			lock (typeof(LaundryWashingProgramTypeMetadata))
			{
				if (LaundryWashingProgramTypeMetadata.mapDelegates == null)
				{
					LaundryWashingProgramTypeMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryWashingProgramTypeMetadata.meta == null)
				{
					LaundryWashingProgramTypeMetadata.meta = new LaundryWashingProgramTypeMetadata();
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

				meta.AddTypeMap("LaundryProgramTypeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRLaundryProcessType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaundryProgram", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaundryType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Weight", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundryWashingProgramType";
				meta.Destination = "LaundryWashingProgramType";
				meta.spInsert = "proc_LaundryWashingProgramTypeInsert";
				meta.spUpdate = "proc_LaundryWashingProgramTypeUpdate";
				meta.spDelete = "proc_LaundryWashingProgramTypeDelete";
				meta.spLoadAll = "proc_LaundryWashingProgramTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryWashingProgramTypeLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryWashingProgramTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
