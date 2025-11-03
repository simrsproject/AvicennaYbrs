/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/31/2021 7:37:44 PM
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
	abstract public class esLaundryWashingMachineProgramCollection : esEntityCollectionWAuditLog
	{
		public esLaundryWashingMachineProgramCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryWashingMachineProgramCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryWashingMachineProgramQuery query)
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
			this.InitQuery(query as esLaundryWashingMachineProgramQuery);
		}
		#endregion

		virtual public LaundryWashingMachineProgram DetachEntity(LaundryWashingMachineProgram entity)
		{
			return base.DetachEntity(entity) as LaundryWashingMachineProgram;
		}

		virtual public LaundryWashingMachineProgram AttachEntity(LaundryWashingMachineProgram entity)
		{
			return base.AttachEntity(entity) as LaundryWashingMachineProgram;
		}

		virtual public void Combine(LaundryWashingMachineProgramCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryWashingMachineProgram this[int index]
		{
			get
			{
				return base[index] as LaundryWashingMachineProgram;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryWashingMachineProgram);
		}
	}

	[Serializable]
	abstract public class esLaundryWashingMachineProgram : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryWashingMachineProgramQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryWashingMachineProgram()
		{
		}

		public esLaundryWashingMachineProgram(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String machineID, String sRLaundryProgram)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(machineID, sRLaundryProgram);
			else
				return LoadByPrimaryKeyStoredProcedure(machineID, sRLaundryProgram);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String machineID, String sRLaundryProgram)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(machineID, sRLaundryProgram);
			else
				return LoadByPrimaryKeyStoredProcedure(machineID, sRLaundryProgram);
		}

		private bool LoadByPrimaryKeyDynamic(String machineID, String sRLaundryProgram)
		{
			esLaundryWashingMachineProgramQuery query = this.GetDynamicQuery();
			query.Where(query.MachineID == machineID, query.SRLaundryProgram == sRLaundryProgram);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String machineID, String sRLaundryProgram)
		{
			esParameters parms = new esParameters();
			parms.Add("MachineID", machineID);
			parms.Add("SRLaundryProgram", sRLaundryProgram);
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
						case "MachineID": this.str.MachineID = (string)value; break;
						case "SRLaundryProgram": this.str.SRLaundryProgram = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
		/// Maps to LaundryWashingMachineProgram.MachineID
		/// </summary>
		virtual public System.String MachineID
		{
			get
			{
				return base.GetSystemString(LaundryWashingMachineProgramMetadata.ColumnNames.MachineID);
			}

			set
			{
				base.SetSystemString(LaundryWashingMachineProgramMetadata.ColumnNames.MachineID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingMachineProgram.SRLaundryProgram
		/// </summary>
		virtual public System.String SRLaundryProgram
		{
			get
			{
				return base.GetSystemString(LaundryWashingMachineProgramMetadata.ColumnNames.SRLaundryProgram);
			}

			set
			{
				base.SetSystemString(LaundryWashingMachineProgramMetadata.ColumnNames.SRLaundryProgram, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingMachineProgram.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryWashingMachineProgramMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryWashingMachineProgramMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryWashingMachineProgram.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryWashingMachineProgramMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryWashingMachineProgramMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryWashingMachineProgram entity)
			{
				this.entity = entity;
			}
			public System.String MachineID
			{
				get
				{
					System.String data = entity.MachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MachineID = null;
					else entity.MachineID = Convert.ToString(value);
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
			private esLaundryWashingMachineProgram entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryWashingMachineProgramQuery query)
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
				throw new Exception("esLaundryWashingMachineProgram can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryWashingMachineProgram : esLaundryWashingMachineProgram
	{
	}

	[Serializable]
	abstract public class esLaundryWashingMachineProgramQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryWashingMachineProgramMetadata.Meta();
			}
		}

		public esQueryItem MachineID
		{
			get
			{
				return new esQueryItem(this, LaundryWashingMachineProgramMetadata.ColumnNames.MachineID, esSystemType.String);
			}
		}

		public esQueryItem SRLaundryProgram
		{
			get
			{
				return new esQueryItem(this, LaundryWashingMachineProgramMetadata.ColumnNames.SRLaundryProgram, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryWashingMachineProgramMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryWashingMachineProgramMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryWashingMachineProgramCollection")]
	public partial class LaundryWashingMachineProgramCollection : esLaundryWashingMachineProgramCollection, IEnumerable<LaundryWashingMachineProgram>
	{
		public LaundryWashingMachineProgramCollection()
		{

		}

		public static implicit operator List<LaundryWashingMachineProgram>(LaundryWashingMachineProgramCollection coll)
		{
			List<LaundryWashingMachineProgram> list = new List<LaundryWashingMachineProgram>();

			foreach (LaundryWashingMachineProgram emp in coll)
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
				return LaundryWashingMachineProgramMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryWashingMachineProgramQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryWashingMachineProgram(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryWashingMachineProgram();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryWashingMachineProgramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryWashingMachineProgramQuery();
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
		public bool Load(LaundryWashingMachineProgramQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryWashingMachineProgram AddNew()
		{
			LaundryWashingMachineProgram entity = base.AddNewEntity() as LaundryWashingMachineProgram;

			return entity;
		}
		public LaundryWashingMachineProgram FindByPrimaryKey(String machineID, String sRLaundryProgram)
		{
			return base.FindByPrimaryKey(machineID, sRLaundryProgram) as LaundryWashingMachineProgram;
		}

		#region IEnumerable< LaundryWashingMachineProgram> Members

		IEnumerator<LaundryWashingMachineProgram> IEnumerable<LaundryWashingMachineProgram>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryWashingMachineProgram;
			}
		}

		#endregion

		private LaundryWashingMachineProgramQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryWashingMachineProgram' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryWashingMachineProgram ({MachineID, SRLaundryProgram})")]
	[Serializable]
	public partial class LaundryWashingMachineProgram : esLaundryWashingMachineProgram
	{
		public LaundryWashingMachineProgram()
		{
		}

		public LaundryWashingMachineProgram(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryWashingMachineProgramMetadata.Meta();
			}
		}

		override protected esLaundryWashingMachineProgramQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryWashingMachineProgramQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryWashingMachineProgramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryWashingMachineProgramQuery();
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
		public bool Load(LaundryWashingMachineProgramQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryWashingMachineProgramQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryWashingMachineProgramQuery : esLaundryWashingMachineProgramQuery
	{
		public LaundryWashingMachineProgramQuery()
		{

		}

		public LaundryWashingMachineProgramQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryWashingMachineProgramQuery";
		}
	}

	[Serializable]
	public partial class LaundryWashingMachineProgramMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryWashingMachineProgramMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryWashingMachineProgramMetadata.ColumnNames.MachineID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingMachineProgramMetadata.PropertyNames.MachineID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingMachineProgramMetadata.ColumnNames.SRLaundryProgram, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingMachineProgramMetadata.PropertyNames.SRLaundryProgram;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingMachineProgramMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryWashingMachineProgramMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryWashingMachineProgramMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryWashingMachineProgramMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryWashingMachineProgramMetadata Meta()
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
			public const string MachineID = "MachineID";
			public const string SRLaundryProgram = "SRLaundryProgram";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MachineID = "MachineID";
			public const string SRLaundryProgram = "SRLaundryProgram";
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
			lock (typeof(LaundryWashingMachineProgramMetadata))
			{
				if (LaundryWashingMachineProgramMetadata.mapDelegates == null)
				{
					LaundryWashingMachineProgramMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryWashingMachineProgramMetadata.meta == null)
				{
					LaundryWashingMachineProgramMetadata.meta = new LaundryWashingMachineProgramMetadata();
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

				meta.AddTypeMap("MachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRLaundryProgram", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundryWashingMachineProgram";
				meta.Destination = "LaundryWashingMachineProgram";
				meta.spInsert = "proc_LaundryWashingMachineProgramInsert";
				meta.spUpdate = "proc_LaundryWashingMachineProgramUpdate";
				meta.spDelete = "proc_LaundryWashingMachineProgramDelete";
				meta.spLoadAll = "proc_LaundryWashingMachineProgramLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryWashingMachineProgramLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryWashingMachineProgramMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
