/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/6/2021 2:41:35 PM
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
	abstract public class esParamedicGlobalScheduleCollection : esEntityCollectionWAuditLog
	{
		public esParamedicGlobalScheduleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ParamedicGlobalScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicGlobalScheduleQuery query)
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
			this.InitQuery(query as esParamedicGlobalScheduleQuery);
		}
		#endregion

		virtual public ParamedicGlobalSchedule DetachEntity(ParamedicGlobalSchedule entity)
		{
			return base.DetachEntity(entity) as ParamedicGlobalSchedule;
		}

		virtual public ParamedicGlobalSchedule AttachEntity(ParamedicGlobalSchedule entity)
		{
			return base.AttachEntity(entity) as ParamedicGlobalSchedule;
		}

		virtual public void Combine(ParamedicGlobalScheduleCollection collection)
		{
			base.Combine(collection);
		}

		new public ParamedicGlobalSchedule this[int index]
		{
			get
			{
				return base[index] as ParamedicGlobalSchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicGlobalSchedule);
		}
	}

	[Serializable]
	abstract public class esParamedicGlobalSchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicGlobalScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicGlobalSchedule()
		{
		}

		public esParamedicGlobalSchedule(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paramedicID, String serviceUnitID, Int32 dayOfWeek)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, serviceUnitID, dayOfWeek);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, serviceUnitID, dayOfWeek);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paramedicID, String serviceUnitID, Int32 dayOfWeek)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paramedicID, serviceUnitID, dayOfWeek);
			else
				return LoadByPrimaryKeyStoredProcedure(paramedicID, serviceUnitID, dayOfWeek);
		}

		private bool LoadByPrimaryKeyDynamic(String paramedicID, String serviceUnitID, Int32 dayOfWeek)
		{
			esParamedicGlobalScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.ParamedicID == paramedicID, query.ServiceUnitID == serviceUnitID, query.DayOfWeek == dayOfWeek);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String paramedicID, String serviceUnitID, Int32 dayOfWeek)
		{
			esParameters parms = new esParameters();
			parms.Add("ParamedicID", paramedicID);
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("DayOfWeek", dayOfWeek);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "DayOfWeek": this.str.DayOfWeek = (string)value; break;
						case "OperationalTimeID": this.str.OperationalTimeID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DayOfWeek":

							if (value == null || value is System.Int32)
								this.DayOfWeek = (System.Int32?)value;
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
		/// Maps to ParamedicGlobalSchedule.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicGlobalScheduleMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(ParamedicGlobalScheduleMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicGlobalSchedule.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicGlobalScheduleMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ParamedicGlobalScheduleMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicGlobalSchedule.DayOfWeek
		/// </summary>
		virtual public System.Int32? DayOfWeek
		{
			get
			{
				return base.GetSystemInt32(ParamedicGlobalScheduleMetadata.ColumnNames.DayOfWeek);
			}

			set
			{
				base.SetSystemInt32(ParamedicGlobalScheduleMetadata.ColumnNames.DayOfWeek, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicGlobalSchedule.OperationalTimeID
		/// </summary>
		virtual public System.String OperationalTimeID
		{
			get
			{
				return base.GetSystemString(ParamedicGlobalScheduleMetadata.ColumnNames.OperationalTimeID);
			}

			set
			{
				base.SetSystemString(ParamedicGlobalScheduleMetadata.ColumnNames.OperationalTimeID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicGlobalSchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicGlobalScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ParamedicGlobalScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicGlobalSchedule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicGlobalScheduleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicGlobalScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicGlobalSchedule entity)
			{
				this.entity = entity;
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
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
			public System.String DayOfWeek
			{
				get
				{
					System.Int32? data = entity.DayOfWeek;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DayOfWeek = null;
					else entity.DayOfWeek = Convert.ToInt32(value);
				}
			}
			public System.String OperationalTimeID
			{
				get
				{
					System.String data = entity.OperationalTimeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationalTimeID = null;
					else entity.OperationalTimeID = Convert.ToString(value);
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
			private esParamedicGlobalSchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicGlobalScheduleQuery query)
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
				throw new Exception("esParamedicGlobalSchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicGlobalSchedule : esParamedicGlobalSchedule
	{
	}

	[Serializable]
	abstract public class esParamedicGlobalScheduleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ParamedicGlobalScheduleMetadata.Meta();
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicGlobalScheduleMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicGlobalScheduleMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem DayOfWeek
		{
			get
			{
				return new esQueryItem(this, ParamedicGlobalScheduleMetadata.ColumnNames.DayOfWeek, esSystemType.Int32);
			}
		}

		public esQueryItem OperationalTimeID
		{
			get
			{
				return new esQueryItem(this, ParamedicGlobalScheduleMetadata.ColumnNames.OperationalTimeID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicGlobalScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicGlobalScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicGlobalScheduleCollection")]
	public partial class ParamedicGlobalScheduleCollection : esParamedicGlobalScheduleCollection, IEnumerable<ParamedicGlobalSchedule>
	{
		public ParamedicGlobalScheduleCollection()
		{

		}

		public static implicit operator List<ParamedicGlobalSchedule>(ParamedicGlobalScheduleCollection coll)
		{
			List<ParamedicGlobalSchedule> list = new List<ParamedicGlobalSchedule>();

			foreach (ParamedicGlobalSchedule emp in coll)
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
				return ParamedicGlobalScheduleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicGlobalScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicGlobalSchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicGlobalSchedule();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ParamedicGlobalScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicGlobalScheduleQuery();
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
		public bool Load(ParamedicGlobalScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicGlobalSchedule AddNew()
		{
			ParamedicGlobalSchedule entity = base.AddNewEntity() as ParamedicGlobalSchedule;

			return entity;
		}
		public ParamedicGlobalSchedule FindByPrimaryKey(String paramedicID, String serviceUnitID, Int32 dayOfWeek)
		{
			return base.FindByPrimaryKey(paramedicID, serviceUnitID, dayOfWeek) as ParamedicGlobalSchedule;
		}

		#region IEnumerable< ParamedicGlobalSchedule> Members

		IEnumerator<ParamedicGlobalSchedule> IEnumerable<ParamedicGlobalSchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicGlobalSchedule;
			}
		}

		#endregion

		private ParamedicGlobalScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicGlobalSchedule' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicGlobalSchedule ({ParamedicID, ServiceUnitID, DayOfWeek})")]
	[Serializable]
	public partial class ParamedicGlobalSchedule : esParamedicGlobalSchedule
	{
		public ParamedicGlobalSchedule()
		{
		}

		public ParamedicGlobalSchedule(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicGlobalScheduleMetadata.Meta();
			}
		}

		override protected esParamedicGlobalScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicGlobalScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ParamedicGlobalScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicGlobalScheduleQuery();
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
		public bool Load(ParamedicGlobalScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ParamedicGlobalScheduleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicGlobalScheduleQuery : esParamedicGlobalScheduleQuery
	{
		public ParamedicGlobalScheduleQuery()
		{

		}

		public ParamedicGlobalScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ParamedicGlobalScheduleQuery";
		}
	}

	[Serializable]
	public partial class ParamedicGlobalScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicGlobalScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicGlobalScheduleMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicGlobalScheduleMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicGlobalScheduleMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicGlobalScheduleMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicGlobalScheduleMetadata.ColumnNames.DayOfWeek, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicGlobalScheduleMetadata.PropertyNames.DayOfWeek;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicGlobalScheduleMetadata.ColumnNames.OperationalTimeID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicGlobalScheduleMetadata.PropertyNames.OperationalTimeID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicGlobalScheduleMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicGlobalScheduleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicGlobalScheduleMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicGlobalScheduleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ParamedicGlobalScheduleMetadata Meta()
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
			public const string ParamedicID = "ParamedicID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string DayOfWeek = "DayOfWeek";
			public const string OperationalTimeID = "OperationalTimeID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ParamedicID = "ParamedicID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string DayOfWeek = "DayOfWeek";
			public const string OperationalTimeID = "OperationalTimeID";
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
			lock (typeof(ParamedicGlobalScheduleMetadata))
			{
				if (ParamedicGlobalScheduleMetadata.mapDelegates == null)
				{
					ParamedicGlobalScheduleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ParamedicGlobalScheduleMetadata.meta == null)
				{
					ParamedicGlobalScheduleMetadata.meta = new ParamedicGlobalScheduleMetadata();
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

				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DayOfWeek", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OperationalTimeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ParamedicGlobalSchedule";
				meta.Destination = "ParamedicGlobalSchedule";
				meta.spInsert = "proc_ParamedicGlobalScheduleInsert";
				meta.spUpdate = "proc_ParamedicGlobalScheduleUpdate";
				meta.spDelete = "proc_ParamedicGlobalScheduleDelete";
				meta.spLoadAll = "proc_ParamedicGlobalScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicGlobalScheduleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicGlobalScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
