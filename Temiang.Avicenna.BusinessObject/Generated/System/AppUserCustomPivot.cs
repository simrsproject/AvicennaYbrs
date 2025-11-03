/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/31/2022 10:37:16 AM
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
	abstract public class esAppUserCustomPivotCollection : esEntityCollectionWAuditLog
	{
		public esAppUserCustomPivotCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppUserCustomPivotCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppUserCustomPivotQuery query)
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
			this.InitQuery(query as esAppUserCustomPivotQuery);
		}
		#endregion

		virtual public AppUserCustomPivot DetachEntity(AppUserCustomPivot entity)
		{
			return base.DetachEntity(entity) as AppUserCustomPivot;
		}

		virtual public AppUserCustomPivot AttachEntity(AppUserCustomPivot entity)
		{
			return base.AttachEntity(entity) as AppUserCustomPivot;
		}

		virtual public void Combine(AppUserCustomPivotCollection collection)
		{
			base.Combine(collection);
		}

		new public AppUserCustomPivot this[int index]
		{
			get
			{
				return base[index] as AppUserCustomPivot;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppUserCustomPivot);
		}
	}

	[Serializable]
	abstract public class esAppUserCustomPivot : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppUserCustomPivotQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppUserCustomPivot()
		{
		}

		public esAppUserCustomPivot(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String programID, Int32 customPivotID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID, customPivotID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID, customPivotID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String programID, Int32 customPivotID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID, customPivotID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID, customPivotID);
		}

		private bool LoadByPrimaryKeyDynamic(String programID, Int32 customPivotID)
		{
			esAppUserCustomPivotQuery query = this.GetDynamicQuery();
			query.Where(query.ProgramID == programID, query.CustomPivotID == customPivotID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String programID, Int32 customPivotID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProgramID", programID);
			parms.Add("CustomPivotID", customPivotID);
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
						case "ProgramID": this.str.ProgramID = (string)value; break;
						case "CustomPivotID": this.str.CustomPivotID = (string)value; break;
						case "UserID": this.str.UserID = (string)value; break;
						case "CustomPivotName": this.str.CustomPivotName = (string)value; break;
						case "IsShowColumnGrandTotals": this.str.IsShowColumnGrandTotals = (string)value; break;
						case "IsShowColumnTotals": this.str.IsShowColumnTotals = (string)value; break;
						case "IsShowRowGrandTotals": this.str.IsShowRowGrandTotals = (string)value; break;
						case "IsShowRowTotals": this.str.IsShowRowTotals = (string)value; break;
						case "IsShowGrandTotalsForSingleValues": this.str.IsShowGrandTotalsForSingleValues = (string)value; break;
						case "IsShowTotalsForSingleValues": this.str.IsShowTotalsForSingleValues = (string)value; break;
						case "SummaryType": this.str.SummaryType = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "CustomPivotID":

							if (value == null || value is System.Int32)
								this.CustomPivotID = (System.Int32?)value;
							break;
						case "IsShowColumnGrandTotals":

							if (value == null || value is System.Boolean)
								this.IsShowColumnGrandTotals = (System.Boolean?)value;
							break;
						case "IsShowColumnTotals":

							if (value == null || value is System.Boolean)
								this.IsShowColumnTotals = (System.Boolean?)value;
							break;
						case "IsShowRowGrandTotals":

							if (value == null || value is System.Boolean)
								this.IsShowRowGrandTotals = (System.Boolean?)value;
							break;
						case "IsShowRowTotals":

							if (value == null || value is System.Boolean)
								this.IsShowRowTotals = (System.Boolean?)value;
							break;
						case "IsShowGrandTotalsForSingleValues":

							if (value == null || value is System.Boolean)
								this.IsShowGrandTotalsForSingleValues = (System.Boolean?)value;
							break;
						case "IsShowTotalsForSingleValues":

							if (value == null || value is System.Boolean)
								this.IsShowTotalsForSingleValues = (System.Boolean?)value;
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
		/// Maps to AppUserCustomPivot.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(AppUserCustomPivotMetadata.ColumnNames.ProgramID);
			}

			set
			{
				base.SetSystemString(AppUserCustomPivotMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.CustomPivotID
		/// </summary>
		virtual public System.Int32? CustomPivotID
		{
			get
			{
				return base.GetSystemInt32(AppUserCustomPivotMetadata.ColumnNames.CustomPivotID);
			}

			set
			{
				base.SetSystemInt32(AppUserCustomPivotMetadata.ColumnNames.CustomPivotID, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.UserID
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return base.GetSystemString(AppUserCustomPivotMetadata.ColumnNames.UserID);
			}

			set
			{
				base.SetSystemString(AppUserCustomPivotMetadata.ColumnNames.UserID, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.CustomPivotName
		/// </summary>
		virtual public System.String CustomPivotName
		{
			get
			{
				return base.GetSystemString(AppUserCustomPivotMetadata.ColumnNames.CustomPivotName);
			}

			set
			{
				base.SetSystemString(AppUserCustomPivotMetadata.ColumnNames.CustomPivotName, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.IsShowColumnGrandTotals
		/// </summary>
		virtual public System.Boolean? IsShowColumnGrandTotals
		{
			get
			{
				return base.GetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowColumnGrandTotals);
			}

			set
			{
				base.SetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowColumnGrandTotals, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.IsShowColumnTotals
		/// </summary>
		virtual public System.Boolean? IsShowColumnTotals
		{
			get
			{
				return base.GetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowColumnTotals);
			}

			set
			{
				base.SetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowColumnTotals, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.IsShowRowGrandTotals
		/// </summary>
		virtual public System.Boolean? IsShowRowGrandTotals
		{
			get
			{
				return base.GetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowRowGrandTotals);
			}

			set
			{
				base.SetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowRowGrandTotals, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.IsShowRowTotals
		/// </summary>
		virtual public System.Boolean? IsShowRowTotals
		{
			get
			{
				return base.GetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowRowTotals);
			}

			set
			{
				base.SetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowRowTotals, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.IsShowGrandTotalsForSingleValues
		/// </summary>
		virtual public System.Boolean? IsShowGrandTotalsForSingleValues
		{
			get
			{
				return base.GetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowGrandTotalsForSingleValues);
			}

			set
			{
				base.SetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowGrandTotalsForSingleValues, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.IsShowTotalsForSingleValues
		/// </summary>
		virtual public System.Boolean? IsShowTotalsForSingleValues
		{
			get
			{
				return base.GetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowTotalsForSingleValues);
			}

			set
			{
				base.SetSystemBoolean(AppUserCustomPivotMetadata.ColumnNames.IsShowTotalsForSingleValues, value);
			}
		}
		/// <summary>
		/// Maps to AppUserCustomPivot.SummaryType
		/// </summary>
		virtual public System.String SummaryType
		{
			get
			{
				return base.GetSystemString(AppUserCustomPivotMetadata.ColumnNames.SummaryType);
			}

			set
			{
				base.SetSystemString(AppUserCustomPivotMetadata.ColumnNames.SummaryType, value);
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
			public esStrings(esAppUserCustomPivot entity)
			{
				this.entity = entity;
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
			public System.String CustomPivotID
			{
				get
				{
					System.Int32? data = entity.CustomPivotID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomPivotID = null;
					else entity.CustomPivotID = Convert.ToInt32(value);
				}
			}
			public System.String UserID
			{
				get
				{
					System.String data = entity.UserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserID = null;
					else entity.UserID = Convert.ToString(value);
				}
			}
			public System.String CustomPivotName
			{
				get
				{
					System.String data = entity.CustomPivotName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomPivotName = null;
					else entity.CustomPivotName = Convert.ToString(value);
				}
			}
			public System.String IsShowColumnGrandTotals
			{
				get
				{
					System.Boolean? data = entity.IsShowColumnGrandTotals;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShowColumnGrandTotals = null;
					else entity.IsShowColumnGrandTotals = Convert.ToBoolean(value);
				}
			}
			public System.String IsShowColumnTotals
			{
				get
				{
					System.Boolean? data = entity.IsShowColumnTotals;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShowColumnTotals = null;
					else entity.IsShowColumnTotals = Convert.ToBoolean(value);
				}
			}
			public System.String IsShowRowGrandTotals
			{
				get
				{
					System.Boolean? data = entity.IsShowRowGrandTotals;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShowRowGrandTotals = null;
					else entity.IsShowRowGrandTotals = Convert.ToBoolean(value);
				}
			}
			public System.String IsShowRowTotals
			{
				get
				{
					System.Boolean? data = entity.IsShowRowTotals;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShowRowTotals = null;
					else entity.IsShowRowTotals = Convert.ToBoolean(value);
				}
			}
			public System.String IsShowGrandTotalsForSingleValues
			{
				get
				{
					System.Boolean? data = entity.IsShowGrandTotalsForSingleValues;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShowGrandTotalsForSingleValues = null;
					else entity.IsShowGrandTotalsForSingleValues = Convert.ToBoolean(value);
				}
			}
			public System.String IsShowTotalsForSingleValues
			{
				get
				{
					System.Boolean? data = entity.IsShowTotalsForSingleValues;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShowTotalsForSingleValues = null;
					else entity.IsShowTotalsForSingleValues = Convert.ToBoolean(value);
				}
			}
			public System.String SummaryType
			{
				get
				{
					System.String data = entity.SummaryType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SummaryType = null;
					else entity.SummaryType = Convert.ToString(value);
				}
			}
			private esAppUserCustomPivot entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppUserCustomPivotQuery query)
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
				throw new Exception("esAppUserCustomPivot can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppUserCustomPivot : esAppUserCustomPivot
	{
	}

	[Serializable]
	abstract public class esAppUserCustomPivotQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppUserCustomPivotMetadata.Meta();
			}
		}

		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		}

		public esQueryItem CustomPivotID
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.CustomPivotID, esSystemType.Int32);
			}
		}

		public esQueryItem UserID
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.UserID, esSystemType.String);
			}
		}

		public esQueryItem CustomPivotName
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.CustomPivotName, esSystemType.String);
			}
		}

		public esQueryItem IsShowColumnGrandTotals
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.IsShowColumnGrandTotals, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShowColumnTotals
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.IsShowColumnTotals, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShowRowGrandTotals
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.IsShowRowGrandTotals, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShowRowTotals
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.IsShowRowTotals, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShowGrandTotalsForSingleValues
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.IsShowGrandTotalsForSingleValues, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShowTotalsForSingleValues
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.IsShowTotalsForSingleValues, esSystemType.Boolean);
			}
		}

		public esQueryItem SummaryType
		{
			get
			{
				return new esQueryItem(this, AppUserCustomPivotMetadata.ColumnNames.SummaryType, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppUserCustomPivotCollection")]
	public partial class AppUserCustomPivotCollection : esAppUserCustomPivotCollection, IEnumerable<AppUserCustomPivot>
	{
		public AppUserCustomPivotCollection()
		{

		}

		public static implicit operator List<AppUserCustomPivot>(AppUserCustomPivotCollection coll)
		{
			List<AppUserCustomPivot> list = new List<AppUserCustomPivot>();

			foreach (AppUserCustomPivot emp in coll)
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
				return AppUserCustomPivotMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppUserCustomPivotQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppUserCustomPivot(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppUserCustomPivot();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppUserCustomPivotQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppUserCustomPivotQuery();
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
		public bool Load(AppUserCustomPivotQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppUserCustomPivot AddNew()
		{
			AppUserCustomPivot entity = base.AddNewEntity() as AppUserCustomPivot;

			return entity;
		}
		public AppUserCustomPivot FindByPrimaryKey(String programID, Int32 customPivotID)
		{
			return base.FindByPrimaryKey(programID, customPivotID) as AppUserCustomPivot;
		}

		#region IEnumerable< AppUserCustomPivot> Members

		IEnumerator<AppUserCustomPivot> IEnumerable<AppUserCustomPivot>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppUserCustomPivot;
			}
		}

		#endregion

		private AppUserCustomPivotQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppUserCustomPivot' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppUserCustomPivot ({ProgramID, CustomPivotID})")]
	[Serializable]
	public partial class AppUserCustomPivot : esAppUserCustomPivot
	{
		public AppUserCustomPivot()
		{
		}

		public AppUserCustomPivot(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppUserCustomPivotMetadata.Meta();
			}
		}

		override protected esAppUserCustomPivotQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppUserCustomPivotQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppUserCustomPivotQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppUserCustomPivotQuery();
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
		public bool Load(AppUserCustomPivotQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppUserCustomPivotQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppUserCustomPivotQuery : esAppUserCustomPivotQuery
	{
		public AppUserCustomPivotQuery()
		{

		}

		public AppUserCustomPivotQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppUserCustomPivotQuery";
		}
	}

	[Serializable]
	public partial class AppUserCustomPivotMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppUserCustomPivotMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.ProgramID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.CustomPivotID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.CustomPivotID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.UserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.UserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.CustomPivotName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.CustomPivotName;
			c.CharacterMaxLength = 300;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.IsShowColumnGrandTotals, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.IsShowColumnGrandTotals;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.IsShowColumnTotals, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.IsShowColumnTotals;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.IsShowRowGrandTotals, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.IsShowRowGrandTotals;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.IsShowRowTotals, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.IsShowRowTotals;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.IsShowGrandTotalsForSingleValues, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.IsShowGrandTotalsForSingleValues;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.IsShowTotalsForSingleValues, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.IsShowTotalsForSingleValues;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppUserCustomPivotMetadata.ColumnNames.SummaryType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserCustomPivotMetadata.PropertyNames.SummaryType;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppUserCustomPivotMetadata Meta()
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
			public const string ProgramID = "ProgramID";
			public const string CustomPivotID = "CustomPivotID";
			public const string UserID = "UserID";
			public const string CustomPivotName = "CustomPivotName";
			public const string IsShowColumnGrandTotals = "IsShowColumnGrandTotals";
			public const string IsShowColumnTotals = "IsShowColumnTotals";
			public const string IsShowRowGrandTotals = "IsShowRowGrandTotals";
			public const string IsShowRowTotals = "IsShowRowTotals";
			public const string IsShowGrandTotalsForSingleValues = "IsShowGrandTotalsForSingleValues";
			public const string IsShowTotalsForSingleValues = "IsShowTotalsForSingleValues";
			public const string SummaryType = "SummaryType";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ProgramID = "ProgramID";
			public const string CustomPivotID = "CustomPivotID";
			public const string UserID = "UserID";
			public const string CustomPivotName = "CustomPivotName";
			public const string IsShowColumnGrandTotals = "IsShowColumnGrandTotals";
			public const string IsShowColumnTotals = "IsShowColumnTotals";
			public const string IsShowRowGrandTotals = "IsShowRowGrandTotals";
			public const string IsShowRowTotals = "IsShowRowTotals";
			public const string IsShowGrandTotalsForSingleValues = "IsShowGrandTotalsForSingleValues";
			public const string IsShowTotalsForSingleValues = "IsShowTotalsForSingleValues";
			public const string SummaryType = "SummaryType";
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
			lock (typeof(AppUserCustomPivotMetadata))
			{
				if (AppUserCustomPivotMetadata.mapDelegates == null)
				{
					AppUserCustomPivotMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppUserCustomPivotMetadata.meta == null)
				{
					AppUserCustomPivotMetadata.meta = new AppUserCustomPivotMetadata();
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

				meta.AddTypeMap("ProgramID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CustomPivotID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CustomPivotName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsShowColumnGrandTotals", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShowColumnTotals", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShowRowGrandTotals", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShowRowTotals", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShowGrandTotalsForSingleValues", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShowTotalsForSingleValues", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SummaryType", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppUserCustomPivot";
				meta.Destination = "AppUserCustomPivot";
				meta.spInsert = "proc_AppUserCustomPivotInsert";
				meta.spUpdate = "proc_AppUserCustomPivotUpdate";
				meta.spDelete = "proc_AppUserCustomPivotDelete";
				meta.spLoadAll = "proc_AppUserCustomPivotLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppUserCustomPivotLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppUserCustomPivotMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
