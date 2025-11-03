/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/26/2022 10:43:15 PM
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
	abstract public class esAppProgramEsignCollection : esEntityCollectionWAuditLog
	{
		public esAppProgramEsignCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppProgramEsignCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppProgramEsignQuery query)
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
			this.InitQuery(query as esAppProgramEsignQuery);
		}
		#endregion

		virtual public AppProgramEsign DetachEntity(AppProgramEsign entity)
		{
			return base.DetachEntity(entity) as AppProgramEsign;
		}

		virtual public AppProgramEsign AttachEntity(AppProgramEsign entity)
		{
			return base.AttachEntity(entity) as AppProgramEsign;
		}

		virtual public void Combine(AppProgramEsignCollection collection)
		{
			base.Combine(collection);
		}

		new public AppProgramEsign this[int index]
		{
			get
			{
				return base[index] as AppProgramEsign;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppProgramEsign);
		}
	}

	[Serializable]
	abstract public class esAppProgramEsign : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppProgramEsignQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppProgramEsign()
		{
		}

		public esAppProgramEsign(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String programID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String programID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID);
		}

		private bool LoadByPrimaryKeyDynamic(String programID)
		{
			esAppProgramEsignQuery query = this.GetDynamicQuery();
			query.Where(query.ProgramID == programID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String programID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProgramID", programID);
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
						case "IsVisible": this.str.IsVisible = (string)value; break;
						case "TagCoordinate": this.str.TagCoordinate = (string)value; break;
						case "Page": this.str.Page = (string)value; break;
						case "XAxis": this.str.XAxis = (string)value; break;
						case "YAxis": this.str.YAxis = (string)value; break;
						case "Width": this.str.Width = (string)value; break;
						case "Height": this.str.Height = (string)value; break;
						case "UrlRootHist": this.str.UrlRootHist = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "IsVisible":

							if (value == null || value is System.Boolean)
								this.IsVisible = (System.Boolean?)value;
							break;
						case "XAxis":

							if (value == null || value is System.Int32)
								this.XAxis = (System.Int32?)value;
							break;
						case "YAxis":

							if (value == null || value is System.Int32)
								this.YAxis = (System.Int32?)value;
							break;
						case "Width":

							if (value == null || value is System.Int32)
								this.Width = (System.Int32?)value;
							break;
						case "Height":

							if (value == null || value is System.Int32)
								this.Height = (System.Int32?)value;
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
		/// Maps to AppProgramEsign.ProgramID
		/// </summary>
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(AppProgramEsignMetadata.ColumnNames.ProgramID);
			}

			set
			{
				base.SetSystemString(AppProgramEsignMetadata.ColumnNames.ProgramID, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramEsign.IsVisible
		/// </summary>
		virtual public System.Boolean? IsVisible
		{
			get
			{
				return base.GetSystemBoolean(AppProgramEsignMetadata.ColumnNames.IsVisible);
			}

			set
			{
				base.SetSystemBoolean(AppProgramEsignMetadata.ColumnNames.IsVisible, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramEsign.TagCoordinate
		/// </summary>
		virtual public System.String TagCoordinate
		{
			get
			{
				return base.GetSystemString(AppProgramEsignMetadata.ColumnNames.TagCoordinate);
			}

			set
			{
				base.SetSystemString(AppProgramEsignMetadata.ColumnNames.TagCoordinate, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramEsign.Page
		/// </summary>
		virtual public System.String Page
		{
			get
			{
				return base.GetSystemString(AppProgramEsignMetadata.ColumnNames.Page);
			}

			set
			{
				base.SetSystemString(AppProgramEsignMetadata.ColumnNames.Page, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramEsign.XAxis
		/// </summary>
		virtual public System.Int32? XAxis
		{
			get
			{
				return base.GetSystemInt32(AppProgramEsignMetadata.ColumnNames.XAxis);
			}

			set
			{
				base.SetSystemInt32(AppProgramEsignMetadata.ColumnNames.XAxis, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramEsign.YAxis
		/// </summary>
		virtual public System.Int32? YAxis
		{
			get
			{
				return base.GetSystemInt32(AppProgramEsignMetadata.ColumnNames.YAxis);
			}

			set
			{
				base.SetSystemInt32(AppProgramEsignMetadata.ColumnNames.YAxis, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramEsign.Width
		/// </summary>
		virtual public System.Int32? Width
		{
			get
			{
				return base.GetSystemInt32(AppProgramEsignMetadata.ColumnNames.Width);
			}

			set
			{
				base.SetSystemInt32(AppProgramEsignMetadata.ColumnNames.Width, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramEsign.Height
		/// </summary>
		virtual public System.Int32? Height
		{
			get
			{
				return base.GetSystemInt32(AppProgramEsignMetadata.ColumnNames.Height);
			}

			set
			{
				base.SetSystemInt32(AppProgramEsignMetadata.ColumnNames.Height, value);
			}
		}
		/// <summary>
		/// Maps to AppProgramEsign.UrlRootHist
		/// </summary>
		virtual public System.String UrlRootHist
		{
			get
			{
				return base.GetSystemString(AppProgramEsignMetadata.ColumnNames.UrlRootHist);
			}

			set
			{
				base.SetSystemString(AppProgramEsignMetadata.ColumnNames.UrlRootHist, value);
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
			public esStrings(esAppProgramEsign entity)
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
			public System.String TagCoordinate
			{
				get
				{
					System.String data = entity.TagCoordinate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TagCoordinate = null;
					else entity.TagCoordinate = Convert.ToString(value);
				}
			}
			public System.String Page
			{
				get
				{
					System.String data = entity.Page;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Page = null;
					else entity.Page = Convert.ToString(value);
				}
			}
			public System.String XAxis
			{
				get
				{
					System.Int32? data = entity.XAxis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.XAxis = null;
					else entity.XAxis = Convert.ToInt32(value);
				}
			}
			public System.String YAxis
			{
				get
				{
					System.Int32? data = entity.YAxis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YAxis = null;
					else entity.YAxis = Convert.ToInt32(value);
				}
			}
			public System.String Width
			{
				get
				{
					System.Int32? data = entity.Width;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Width = null;
					else entity.Width = Convert.ToInt32(value);
				}
			}
			public System.String Height
			{
				get
				{
					System.Int32? data = entity.Height;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Height = null;
					else entity.Height = Convert.ToInt32(value);
				}
			}
			public System.String UrlRootHist
			{
				get
				{
					System.String data = entity.UrlRootHist;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UrlRootHist = null;
					else entity.UrlRootHist = Convert.ToString(value);
				}
			}
			private esAppProgramEsign entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppProgramEsignQuery query)
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
				throw new Exception("esAppProgramEsign can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppProgramEsign : esAppProgramEsign
	{
	}

	[Serializable]
	abstract public class esAppProgramEsignQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppProgramEsignMetadata.Meta();
			}
		}

		public esQueryItem ProgramID
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.ProgramID, esSystemType.String);
			}
		}

		public esQueryItem IsVisible
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.IsVisible, esSystemType.Boolean);
			}
		}

		public esQueryItem TagCoordinate
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.TagCoordinate, esSystemType.String);
			}
		}

		public esQueryItem Page
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.Page, esSystemType.String);
			}
		}

		public esQueryItem XAxis
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.XAxis, esSystemType.Int32);
			}
		}

		public esQueryItem YAxis
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.YAxis, esSystemType.Int32);
			}
		}

		public esQueryItem Width
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.Width, esSystemType.Int32);
			}
		}

		public esQueryItem Height
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.Height, esSystemType.Int32);
			}
		}

		public esQueryItem UrlRootHist
		{
			get
			{
				return new esQueryItem(this, AppProgramEsignMetadata.ColumnNames.UrlRootHist, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppProgramEsignCollection")]
	public partial class AppProgramEsignCollection : esAppProgramEsignCollection, IEnumerable<AppProgramEsign>
	{
		public AppProgramEsignCollection()
		{

		}

		public static implicit operator List<AppProgramEsign>(AppProgramEsignCollection coll)
		{
			List<AppProgramEsign> list = new List<AppProgramEsign>();

			foreach (AppProgramEsign emp in coll)
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
				return AppProgramEsignMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppProgramEsignQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppProgramEsign(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppProgramEsign();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppProgramEsignQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppProgramEsignQuery();
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
		public bool Load(AppProgramEsignQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppProgramEsign AddNew()
		{
			AppProgramEsign entity = base.AddNewEntity() as AppProgramEsign;

			return entity;
		}
		public AppProgramEsign FindByPrimaryKey(String programID)
		{
			return base.FindByPrimaryKey(programID) as AppProgramEsign;
		}

		#region IEnumerable< AppProgramEsign> Members

		IEnumerator<AppProgramEsign> IEnumerable<AppProgramEsign>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppProgramEsign;
			}
		}

		#endregion

		private AppProgramEsignQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppProgramEsign' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppProgramEsign ({ProgramID})")]
	[Serializable]
	public partial class AppProgramEsign : esAppProgramEsign
	{
		public AppProgramEsign()
		{
		}

		public AppProgramEsign(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppProgramEsignMetadata.Meta();
			}
		}

		override protected esAppProgramEsignQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppProgramEsignQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppProgramEsignQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppProgramEsignQuery();
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
		public bool Load(AppProgramEsignQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppProgramEsignQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppProgramEsignQuery : esAppProgramEsignQuery
	{
		public AppProgramEsignQuery()
		{

		}

		public AppProgramEsignQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppProgramEsignQuery";
		}
	}

	[Serializable]
	public partial class AppProgramEsignMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppProgramEsignMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.ProgramID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			_columns.Add(c);

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.IsVisible, 1, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.IsVisible;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.TagCoordinate, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.TagCoordinate;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.Page, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.Page;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.XAxis, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.XAxis;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.YAxis, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.YAxis;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.Width, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.Width;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.Height, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.Height;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppProgramEsignMetadata.ColumnNames.UrlRootHist, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramEsignMetadata.PropertyNames.UrlRootHist;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppProgramEsignMetadata Meta()
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
			public const string IsVisible = "IsVisible";
			public const string TagCoordinate = "TagCoordinate";
			public const string Page = "Page";
			public const string XAxis = "XAxis";
			public const string YAxis = "YAxis";
			public const string Width = "Width";
			public const string Height = "Height";
			public const string UrlRootHist = "UrlRootHist";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ProgramID = "ProgramID";
			public const string IsVisible = "IsVisible";
			public const string TagCoordinate = "TagCoordinate";
			public const string Page = "Page";
			public const string XAxis = "XAxis";
			public const string YAxis = "YAxis";
			public const string Width = "Width";
			public const string Height = "Height";
			public const string UrlRootHist = "UrlRootHist";
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
			lock (typeof(AppProgramEsignMetadata))
			{
				if (AppProgramEsignMetadata.mapDelegates == null)
				{
					AppProgramEsignMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppProgramEsignMetadata.meta == null)
				{
					AppProgramEsignMetadata.meta = new AppProgramEsignMetadata();
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
				meta.AddTypeMap("IsVisible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TagCoordinate", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Page", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("XAxis", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("YAxis", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Width", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Height", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UrlRootHist", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppProgramEsign";
				meta.Destination = "AppProgramEsign";
				meta.spInsert = "proc_AppProgramEsignInsert";
				meta.spUpdate = "proc_AppProgramEsignUpdate";
				meta.spDelete = "proc_AppProgramEsignDelete";
				meta.spLoadAll = "proc_AppProgramEsignLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppProgramEsignLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppProgramEsignMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
