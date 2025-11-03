/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/6/2022 11:39:55 AM
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
	abstract public class esAppControlEntryMatrixCollection : esEntityCollectionWAuditLog
	{
		public esAppControlEntryMatrixCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppControlEntryMatrixCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppControlEntryMatrixQuery query)
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
			this.InitQuery(query as esAppControlEntryMatrixQuery);
		}
		#endregion

		virtual public AppControlEntryMatrix DetachEntity(AppControlEntryMatrix entity)
		{
			return base.DetachEntity(entity) as AppControlEntryMatrix;
		}

		virtual public AppControlEntryMatrix AttachEntity(AppControlEntryMatrix entity)
		{
			return base.AttachEntity(entity) as AppControlEntryMatrix;
		}

		virtual public void Combine(AppControlEntryMatrixCollection collection)
		{
			base.Combine(collection);
		}

		new public AppControlEntryMatrix this[int index]
		{
			get
			{
				return base[index] as AppControlEntryMatrix;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppControlEntryMatrix);
		}
	}

	[Serializable]
	abstract public class esAppControlEntryMatrix : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppControlEntryMatrixQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppControlEntryMatrix()
		{
		}

		public esAppControlEntryMatrix(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String healthcareInitialAppsVersion, String entryType, String controlID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(healthcareInitialAppsVersion, entryType, controlID);
			else
				return LoadByPrimaryKeyStoredProcedure(healthcareInitialAppsVersion, entryType, controlID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String healthcareInitialAppsVersion, String entryType, String controlID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(healthcareInitialAppsVersion, entryType, controlID);
			else
				return LoadByPrimaryKeyStoredProcedure(healthcareInitialAppsVersion, entryType, controlID);
		}

		private bool LoadByPrimaryKeyDynamic(String healthcareInitialAppsVersion, String entryType, String controlID)
		{
			esAppControlEntryMatrixQuery query = this.GetDynamicQuery();
			query.Where(query.HealthcareInitialAppsVersion == healthcareInitialAppsVersion, query.EntryType == entryType, query.ControlID == controlID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String healthcareInitialAppsVersion, String entryType, String controlID)
		{
			esParameters parms = new esParameters();
			parms.Add("HealthcareInitialAppsVersion", healthcareInitialAppsVersion);
			parms.Add("EntryType", entryType);
			parms.Add("ControlID", controlID);
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
						case "HealthcareInitialAppsVersion": this.str.HealthcareInitialAppsVersion = (string)value; break;
						case "EntryType": this.str.EntryType = (string)value; break;
						case "ControlID": this.str.ControlID = (string)value; break;
						case "IndexNo": this.str.IndexNo = (string)value; break;
						case "IsVisible": this.str.IsVisible = (string)value; break;
						case "ReferenceValue": this.str.ReferenceValue = (string)value; break;
						case "IsPanelCollapse": this.str.IsPanelCollapse = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IndexNo":

							if (value == null || value is System.Int32)
								this.IndexNo = (System.Int32?)value;
							break;
						case "IsVisible":

							if (value == null || value is System.Boolean)
								this.IsVisible = (System.Boolean?)value;
							break;
						case "IsPanelCollapse":

							if (value == null || value is System.Boolean)
								this.IsPanelCollapse = (System.Boolean?)value;
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
		/// Maps to AppControlEntryMatrix.HealthcareInitialAppsVersion
		/// </summary>
		virtual public System.String HealthcareInitialAppsVersion
		{
			get
			{
				return base.GetSystemString(AppControlEntryMatrixMetadata.ColumnNames.HealthcareInitialAppsVersion);
			}

			set
			{
				base.SetSystemString(AppControlEntryMatrixMetadata.ColumnNames.HealthcareInitialAppsVersion, value);
			}
		}
		/// <summary>
		/// Maps to AppControlEntryMatrix.EntryType
		/// </summary>
		virtual public System.String EntryType
		{
			get
			{
				return base.GetSystemString(AppControlEntryMatrixMetadata.ColumnNames.EntryType);
			}

			set
			{
				base.SetSystemString(AppControlEntryMatrixMetadata.ColumnNames.EntryType, value);
			}
		}
		/// <summary>
		/// Maps to AppControlEntryMatrix.ControlID
		/// </summary>
		virtual public System.String ControlID
		{
			get
			{
				return base.GetSystemString(AppControlEntryMatrixMetadata.ColumnNames.ControlID);
			}

			set
			{
				base.SetSystemString(AppControlEntryMatrixMetadata.ColumnNames.ControlID, value);
			}
		}
		/// <summary>
		/// Maps to AppControlEntryMatrix.IndexNo
		/// </summary>
		virtual public System.Int32? IndexNo
		{
			get
			{
				return base.GetSystemInt32(AppControlEntryMatrixMetadata.ColumnNames.IndexNo);
			}

			set
			{
				base.SetSystemInt32(AppControlEntryMatrixMetadata.ColumnNames.IndexNo, value);
			}
		}
		/// <summary>
		/// Maps to AppControlEntryMatrix.IsVisible
		/// </summary>
		virtual public System.Boolean? IsVisible
		{
			get
			{
				return base.GetSystemBoolean(AppControlEntryMatrixMetadata.ColumnNames.IsVisible);
			}

			set
			{
				base.SetSystemBoolean(AppControlEntryMatrixMetadata.ColumnNames.IsVisible, value);
			}
		}
		/// <summary>
		/// Maps to AppControlEntryMatrix.ReferenceValue
		/// </summary>
		virtual public System.String ReferenceValue
		{
			get
			{
				return base.GetSystemString(AppControlEntryMatrixMetadata.ColumnNames.ReferenceValue);
			}

			set
			{
				base.SetSystemString(AppControlEntryMatrixMetadata.ColumnNames.ReferenceValue, value);
			}
		}
		/// <summary>
		/// Maps to AppControlEntryMatrix.IsPanelCollapse
		/// </summary>
		virtual public System.Boolean? IsPanelCollapse
		{
			get
			{
				return base.GetSystemBoolean(AppControlEntryMatrixMetadata.ColumnNames.IsPanelCollapse);
			}

			set
			{
				base.SetSystemBoolean(AppControlEntryMatrixMetadata.ColumnNames.IsPanelCollapse, value);
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
			public esStrings(esAppControlEntryMatrix entity)
			{
				this.entity = entity;
			}
			public System.String HealthcareInitialAppsVersion
			{
				get
				{
					System.String data = entity.HealthcareInitialAppsVersion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HealthcareInitialAppsVersion = null;
					else entity.HealthcareInitialAppsVersion = Convert.ToString(value);
				}
			}
			public System.String EntryType
			{
				get
				{
					System.String data = entity.EntryType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EntryType = null;
					else entity.EntryType = Convert.ToString(value);
				}
			}
			public System.String ControlID
			{
				get
				{
					System.String data = entity.ControlID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ControlID = null;
					else entity.ControlID = Convert.ToString(value);
				}
			}
			public System.String IndexNo
			{
				get
				{
					System.Int32? data = entity.IndexNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IndexNo = null;
					else entity.IndexNo = Convert.ToInt32(value);
				}
			}
			public System.String IsVisible
			{
				get
				{
					System.Boolean? data = entity.IsVisible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVisible = null;
					else entity.IsVisible = Convert.ToBoolean(value);
				}
			}
			public System.String ReferenceValue
			{
				get
				{
					System.String data = entity.ReferenceValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceValue = null;
					else entity.ReferenceValue = Convert.ToString(value);
				}
			}
			public System.String IsPanelCollapse
			{
				get
				{
					System.Boolean? data = entity.IsPanelCollapse;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPanelCollapse = null;
					else entity.IsPanelCollapse = Convert.ToBoolean(value);
				}
			}
			private esAppControlEntryMatrix entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppControlEntryMatrixQuery query)
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
				throw new Exception("esAppControlEntryMatrix can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppControlEntryMatrix : esAppControlEntryMatrix
	{
	}

	[Serializable]
	abstract public class esAppControlEntryMatrixQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppControlEntryMatrixMetadata.Meta();
			}
		}

		public esQueryItem HealthcareInitialAppsVersion
		{
			get
			{
				return new esQueryItem(this, AppControlEntryMatrixMetadata.ColumnNames.HealthcareInitialAppsVersion, esSystemType.String);
			}
		}

		public esQueryItem EntryType
		{
			get
			{
				return new esQueryItem(this, AppControlEntryMatrixMetadata.ColumnNames.EntryType, esSystemType.String);
			}
		}

		public esQueryItem ControlID
		{
			get
			{
				return new esQueryItem(this, AppControlEntryMatrixMetadata.ColumnNames.ControlID, esSystemType.String);
			}
		}

		public esQueryItem IndexNo
		{
			get
			{
				return new esQueryItem(this, AppControlEntryMatrixMetadata.ColumnNames.IndexNo, esSystemType.Int32);
			}
		}

		public esQueryItem IsVisible
		{
			get
			{
				return new esQueryItem(this, AppControlEntryMatrixMetadata.ColumnNames.IsVisible, esSystemType.Boolean);
			}
		}

		public esQueryItem ReferenceValue
		{
			get
			{
				return new esQueryItem(this, AppControlEntryMatrixMetadata.ColumnNames.ReferenceValue, esSystemType.String);
			}
		}

		public esQueryItem IsPanelCollapse
		{
			get
			{
				return new esQueryItem(this, AppControlEntryMatrixMetadata.ColumnNames.IsPanelCollapse, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppControlEntryMatrixCollection")]
	public partial class AppControlEntryMatrixCollection : esAppControlEntryMatrixCollection, IEnumerable<AppControlEntryMatrix>
	{
		public AppControlEntryMatrixCollection()
		{

		}

		public static implicit operator List<AppControlEntryMatrix>(AppControlEntryMatrixCollection coll)
		{
			List<AppControlEntryMatrix> list = new List<AppControlEntryMatrix>();

			foreach (AppControlEntryMatrix emp in coll)
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
				return AppControlEntryMatrixMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppControlEntryMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppControlEntryMatrix(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppControlEntryMatrix();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppControlEntryMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppControlEntryMatrixQuery();
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
		public bool Load(AppControlEntryMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppControlEntryMatrix AddNew()
		{
			AppControlEntryMatrix entity = base.AddNewEntity() as AppControlEntryMatrix;

			return entity;
		}
		public AppControlEntryMatrix FindByPrimaryKey(String healthcareInitialAppsVersion, String entryType, String controlID)
		{
			return base.FindByPrimaryKey(healthcareInitialAppsVersion, entryType, controlID) as AppControlEntryMatrix;
		}

		#region IEnumerable< AppControlEntryMatrix> Members

		IEnumerator<AppControlEntryMatrix> IEnumerable<AppControlEntryMatrix>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppControlEntryMatrix;
			}
		}

		#endregion

		private AppControlEntryMatrixQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppControlEntryMatrix' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppControlEntryMatrix ({HealthcareInitialAppsVersion, EntryType, ControlID})")]
	[Serializable]
	public partial class AppControlEntryMatrix : esAppControlEntryMatrix
	{
		public AppControlEntryMatrix()
		{
		}

		public AppControlEntryMatrix(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppControlEntryMatrixMetadata.Meta();
			}
		}

		override protected esAppControlEntryMatrixQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppControlEntryMatrixQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppControlEntryMatrixQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppControlEntryMatrixQuery();
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
		public bool Load(AppControlEntryMatrixQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppControlEntryMatrixQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppControlEntryMatrixQuery : esAppControlEntryMatrixQuery
	{
		public AppControlEntryMatrixQuery()
		{

		}

		public AppControlEntryMatrixQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppControlEntryMatrixQuery";
		}
	}

	[Serializable]
	public partial class AppControlEntryMatrixMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppControlEntryMatrixMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppControlEntryMatrixMetadata.ColumnNames.HealthcareInitialAppsVersion, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppControlEntryMatrixMetadata.PropertyNames.HealthcareInitialAppsVersion;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppControlEntryMatrixMetadata.ColumnNames.EntryType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppControlEntryMatrixMetadata.PropertyNames.EntryType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AppControlEntryMatrixMetadata.ColumnNames.ControlID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppControlEntryMatrixMetadata.PropertyNames.ControlID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AppControlEntryMatrixMetadata.ColumnNames.IndexNo, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppControlEntryMatrixMetadata.PropertyNames.IndexNo;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppControlEntryMatrixMetadata.ColumnNames.IsVisible, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppControlEntryMatrixMetadata.PropertyNames.IsVisible;
			_columns.Add(c);

			c = new esColumnMetadata(AppControlEntryMatrixMetadata.ColumnNames.ReferenceValue, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppControlEntryMatrixMetadata.PropertyNames.ReferenceValue;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppControlEntryMatrixMetadata.ColumnNames.IsPanelCollapse, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppControlEntryMatrixMetadata.PropertyNames.IsPanelCollapse;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppControlEntryMatrixMetadata Meta()
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
			public const string HealthcareInitialAppsVersion = "HealthcareInitialAppsVersion";
			public const string EntryType = "EntryType";
			public const string ControlID = "ControlID";
			public const string IndexNo = "IndexNo";
			public const string IsVisible = "IsVisible";
			public const string ReferenceValue = "ReferenceValue";
			public const string IsPanelCollapse = "IsPanelCollapse";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string HealthcareInitialAppsVersion = "HealthcareInitialAppsVersion";
			public const string EntryType = "EntryType";
			public const string ControlID = "ControlID";
			public const string IndexNo = "IndexNo";
			public const string IsVisible = "IsVisible";
			public const string ReferenceValue = "ReferenceValue";
			public const string IsPanelCollapse = "IsPanelCollapse";
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
			lock (typeof(AppControlEntryMatrixMetadata))
			{
				if (AppControlEntryMatrixMetadata.mapDelegates == null)
				{
					AppControlEntryMatrixMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppControlEntryMatrixMetadata.meta == null)
				{
					AppControlEntryMatrixMetadata.meta = new AppControlEntryMatrixMetadata();
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

				meta.AddTypeMap("HealthcareInitialAppsVersion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EntryType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ControlID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IndexNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsVisible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReferenceValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPanelCollapse", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "AppControlEntryMatrix";
				meta.Destination = "AppControlEntryMatrix";
				meta.spInsert = "proc_AppControlEntryMatrixInsert";
				meta.spUpdate = "proc_AppControlEntryMatrixUpdate";
				meta.spDelete = "proc_AppControlEntryMatrixDelete";
				meta.spLoadAll = "proc_AppControlEntryMatrixLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppControlEntryMatrixLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppControlEntryMatrixMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
