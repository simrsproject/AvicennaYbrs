/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/15/2022 5:27:28 PM
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
	abstract public class esPrintSlipLogCollection : esEntityCollectionWAuditLog
	{
		public esPrintSlipLogCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PrintSlipLogCollection";
		}

		#region Query Logic
		protected void InitQuery(esPrintSlipLogQuery query)
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
			this.InitQuery(query as esPrintSlipLogQuery);
		}
		#endregion

		virtual public PrintSlipLog DetachEntity(PrintSlipLog entity)
		{
			return base.DetachEntity(entity) as PrintSlipLog;
		}

		virtual public PrintSlipLog AttachEntity(PrintSlipLog entity)
		{
			return base.AttachEntity(entity) as PrintSlipLog;
		}

		virtual public void Combine(PrintSlipLogCollection collection)
		{
			base.Combine(collection);
		}

		new public PrintSlipLog this[int index]
		{
			get
			{
				return base[index] as PrintSlipLog;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PrintSlipLog);
		}
	}

	[Serializable]
	abstract public class esPrintSlipLog : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPrintSlipLogQuery GetDynamicQuery()
		{
			return null;
		}

		public esPrintSlipLog()
		{
		}

		public esPrintSlipLog(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 logID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(logID);
			else
				return LoadByPrimaryKeyStoredProcedure(logID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 logID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(logID);
			else
				return LoadByPrimaryKeyStoredProcedure(logID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 logID)
		{
			esPrintSlipLogQuery query = this.GetDynamicQuery();
			query.Where(query.LogID == logID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 logID)
		{
			esParameters parms = new esParameters();
			parms.Add("LogID", logID);
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
						case "LogID": this.str.LogID = (string)value; break;
						case "ProgramID": this.str.ProgramID = (string)value; break;
						case "ParameterName": this.str.ParameterName = (string)value; break;
						case "ParameterValue": this.str.ParameterValue = (string)value; break;
						case "PrintCount": this.str.PrintCount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LogID":

							if (value == null || value is System.Int64)
								this.LogID = (System.Int64?)value;
							break;
						case "PrintCount":

							if (value == null || value is System.Int16)
								this.PrintCount = (System.Int16?)value;
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
		/// Maps to PrintSlipLog.LogID
		/// </summary>
		virtual public System.Int64? LogID
		{
			get
			{
				return base.GetSystemInt64(PrintSlipLogMetadata.ColumnNames.LogID);
			}

			set
			{
				base.SetSystemInt64(PrintSlipLogMetadata.ColumnNames.LogID, value);
			}
		}
		/// <summary>
		/// Maps to PrintSlipLog.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(PrintSlipLogMetadata.ColumnNames.ProgramID);
			}

			set
			{
				base.SetSystemString(PrintSlipLogMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to PrintSlipLog.ParameterName
		/// </summary>
		virtual public System.String ParameterName
		{
			get
			{
				return base.GetSystemString(PrintSlipLogMetadata.ColumnNames.ParameterName);
			}

			set
			{
				base.SetSystemString(PrintSlipLogMetadata.ColumnNames.ParameterName, value);
			}
		}
		/// <summary>
		/// Maps to PrintSlipLog.ParameterValue
		/// </summary>
		virtual public System.String ParameterValue
		{
			get
			{
				return base.GetSystemString(PrintSlipLogMetadata.ColumnNames.ParameterValue);
			}

			set
			{
				base.SetSystemString(PrintSlipLogMetadata.ColumnNames.ParameterValue, value);
			}
		}
		/// <summary>
		/// Maps to PrintSlipLog.PrintCount
		/// </summary>
		virtual public System.Int16? PrintCount
		{
			get
			{
				return base.GetSystemInt16(PrintSlipLogMetadata.ColumnNames.PrintCount);
			}

			set
			{
				base.SetSystemInt16(PrintSlipLogMetadata.ColumnNames.PrintCount, value);
			}
		}
		/// <summary>
		/// Maps to PrintSlipLog.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PrintSlipLogMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PrintSlipLogMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PrintSlipLog.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PrintSlipLogMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PrintSlipLogMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPrintSlipLog entity)
			{
				this.entity = entity;
			}
			public System.String LogID
			{
				get
				{
					System.Int64? data = entity.LogID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LogID = null;
					else entity.LogID = Convert.ToInt64(value);
				}
			}
			public System.String ProgramID
			{
				get
				{
					System.String data = entity.ProgramID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProgramID = null;
					else entity.ProgramID = Convert.ToString(value);
				}
			}
			public System.String ParameterName
			{
				get
				{
					System.String data = entity.ParameterName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParameterName = null;
					else entity.ParameterName = Convert.ToString(value);
				}
			}
			public System.String ParameterValue
			{
				get
				{
					System.String data = entity.ParameterValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParameterValue = null;
					else entity.ParameterValue = Convert.ToString(value);
				}
			}
			public System.String PrintCount
			{
				get
				{
					System.Int16? data = entity.PrintCount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrintCount = null;
					else entity.PrintCount = Convert.ToInt16(value);
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
			private esPrintSlipLog entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPrintSlipLogQuery query)
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
				throw new Exception("esPrintSlipLog can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PrintSlipLog : esPrintSlipLog
	{
	}

	[Serializable]
	abstract public class esPrintSlipLogQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PrintSlipLogMetadata.Meta();
			}
		}

		public esQueryItem LogID
		{
			get
			{
				return new esQueryItem(this, PrintSlipLogMetadata.ColumnNames.LogID, esSystemType.Int64);
			}
		}

		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, PrintSlipLogMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		}

		public esQueryItem ParameterName
		{
			get
			{
				return new esQueryItem(this, PrintSlipLogMetadata.ColumnNames.ParameterName, esSystemType.String);
			}
		}

		public esQueryItem ParameterValue
		{
			get
			{
				return new esQueryItem(this, PrintSlipLogMetadata.ColumnNames.ParameterValue, esSystemType.String);
			}
		}

		public esQueryItem PrintCount
		{
			get
			{
				return new esQueryItem(this, PrintSlipLogMetadata.ColumnNames.PrintCount, esSystemType.Int16);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PrintSlipLogMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PrintSlipLogMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PrintSlipLogCollection")]
	public partial class PrintSlipLogCollection : esPrintSlipLogCollection, IEnumerable<PrintSlipLog>
	{
		public PrintSlipLogCollection()
		{

		}

		public static implicit operator List<PrintSlipLog>(PrintSlipLogCollection coll)
		{
			List<PrintSlipLog> list = new List<PrintSlipLog>();

			foreach (PrintSlipLog emp in coll)
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
				return PrintSlipLogMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrintSlipLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PrintSlipLog(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PrintSlipLog();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PrintSlipLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrintSlipLogQuery();
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
		public bool Load(PrintSlipLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PrintSlipLog AddNew()
		{
			PrintSlipLog entity = base.AddNewEntity() as PrintSlipLog;

			return entity;
		}
		public PrintSlipLog FindByPrimaryKey(Int64 logID)
		{
			return base.FindByPrimaryKey(logID) as PrintSlipLog;
		}

		#region IEnumerable< PrintSlipLog> Members

		IEnumerator<PrintSlipLog> IEnumerable<PrintSlipLog>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PrintSlipLog;
			}
		}

		#endregion

		private PrintSlipLogQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PrintSlipLog' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PrintSlipLog ({LogID})")]
	[Serializable]
	public partial class PrintSlipLog : esPrintSlipLog
	{
		public PrintSlipLog()
		{
		}

		public PrintSlipLog(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PrintSlipLogMetadata.Meta();
			}
		}

		override protected esPrintSlipLogQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrintSlipLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PrintSlipLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrintSlipLogQuery();
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
		public bool Load(PrintSlipLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PrintSlipLogQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PrintSlipLogQuery : esPrintSlipLogQuery
	{
		public PrintSlipLogQuery()
		{

		}

		public PrintSlipLogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PrintSlipLogQuery";
		}
	}

	[Serializable]
	public partial class PrintSlipLogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PrintSlipLogMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PrintSlipLogMetadata.ColumnNames.LogID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PrintSlipLogMetadata.PropertyNames.LogID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(PrintSlipLogMetadata.ColumnNames.ProgramID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PrintSlipLogMetadata.PropertyNames.ProgramID;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PrintSlipLogMetadata.ColumnNames.ParameterName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PrintSlipLogMetadata.PropertyNames.ParameterName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PrintSlipLogMetadata.ColumnNames.ParameterValue, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PrintSlipLogMetadata.PropertyNames.ParameterValue;
			c.CharacterMaxLength = 1000;
			_columns.Add(c);

			c = new esColumnMetadata(PrintSlipLogMetadata.ColumnNames.PrintCount, 4, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = PrintSlipLogMetadata.PropertyNames.PrintCount;
			c.NumericPrecision = 5;
			_columns.Add(c);

			c = new esColumnMetadata(PrintSlipLogMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PrintSlipLogMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PrintSlipLogMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PrintSlipLogMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PrintSlipLogMetadata Meta()
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
			public const string LogID = "LogID";
			public const string ProgramID = "ProgramID";
			public const string ParameterName = "ParameterName";
			public const string ParameterValue = "ParameterValue";
			public const string PrintCount = "PrintCount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string LogID = "LogID";
			public const string ProgramID = "ProgramID";
			public const string ParameterName = "ParameterName";
			public const string ParameterValue = "ParameterValue";
			public const string PrintCount = "PrintCount";
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
			lock (typeof(PrintSlipLogMetadata))
			{
				if (PrintSlipLogMetadata.mapDelegates == null)
				{
					PrintSlipLogMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PrintSlipLogMetadata.meta == null)
				{
					PrintSlipLogMetadata.meta = new PrintSlipLogMetadata();
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

				meta.AddTypeMap("LogID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParameterName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParameterValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrintCount", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PrintSlipLog";
				meta.Destination = "PrintSlipLog";
				meta.spInsert = "proc_PrintSlipLogInsert";
				meta.spUpdate = "proc_PrintSlipLogUpdate";
				meta.spDelete = "proc_PrintSlipLogDelete";
				meta.spLoadAll = "proc_PrintSlipLogLoadAll";
				meta.spLoadByPrimaryKey = "proc_PrintSlipLogLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PrintSlipLogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
