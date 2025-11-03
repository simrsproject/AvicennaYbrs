/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2023 11:17:39 AM
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
	abstract public class esPerformancePlanJptCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanJptCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanJptCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanJptQuery query)
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
			this.InitQuery(query as esPerformancePlanJptQuery);
		}
		#endregion

		virtual public PerformancePlanJpt DetachEntity(PerformancePlanJpt entity)
		{
			return base.DetachEntity(entity) as PerformancePlanJpt;
		}

		virtual public PerformancePlanJpt AttachEntity(PerformancePlanJpt entity)
		{
			return base.AttachEntity(entity) as PerformancePlanJpt;
		}

		virtual public void Combine(PerformancePlanJptCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanJpt this[int index]
		{
			get
			{
				return base[index] as PerformancePlanJpt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanJpt);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanJpt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanJptQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanJpt()
		{
		}

		public esPerformancePlanJpt(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 performancePlanID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performancePlanID);
			else
				return LoadByPrimaryKeyStoredProcedure(performancePlanID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 performancePlanID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performancePlanID);
			else
				return LoadByPrimaryKeyStoredProcedure(performancePlanID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 performancePlanID)
		{
			esPerformancePlanJptQuery query = this.GetDynamicQuery();
			query.Where(query.PerformancePlanID == performancePlanID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 performancePlanID)
		{
			esParameters parms = new esParameters();
			parms.Add("PerformancePlanID", performancePlanID);
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
						case "PerformancePlanID": this.str.PerformancePlanID = (string)value; break;
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
						case "SubOrganizationUnitID": this.str.SubOrganizationUnitID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PerformancePlanID":

							if (value == null || value is System.Int32)
								this.PerformancePlanID = (System.Int32?)value;
							break;
						case "OrganizationUnitID":

							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
							break;
						case "SubOrganizationUnitID":

							if (value == null || value is System.Int32)
								this.SubOrganizationUnitID = (System.Int32?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to PerformancePlanJpt.PerformancePlanID
		/// </summary>
		virtual public System.Int32? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanJptMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanJptMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJpt.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanJptMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanJptMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJpt.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanJptMetadata.ColumnNames.SubOrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanJptMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJpt.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanJptMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanJptMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJpt.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJpt.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJpt.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanJptMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanJptMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanJpt.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanJptMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanJptMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanJpt entity)
			{
				this.entity = entity;
			}
			public System.String PerformancePlanID
			{
				get
				{
					System.Int32? data = entity.PerformancePlanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformancePlanID = null;
					else entity.PerformancePlanID = Convert.ToInt32(value);
				}
			}
			public System.String OrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.OrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
					else entity.OrganizationUnitID = Convert.ToInt32(value);
				}
			}
			public System.String SubOrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.SubOrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubOrganizationUnitID = null;
					else entity.SubOrganizationUnitID = Convert.ToInt32(value);
				}
			}
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			private esPerformancePlanJpt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanJptQuery query)
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
				throw new Exception("esPerformancePlanJpt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanJpt : esPerformancePlanJpt
	{
	}

	[Serializable]
	abstract public class esPerformancePlanJptQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanJptMetadata.Meta();
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptMetadata.ColumnNames.PerformancePlanID, esSystemType.Int32);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanJptMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanJptCollection")]
	public partial class PerformancePlanJptCollection : esPerformancePlanJptCollection, IEnumerable<PerformancePlanJpt>
	{
		public PerformancePlanJptCollection()
		{

		}

		public static implicit operator List<PerformancePlanJpt>(PerformancePlanJptCollection coll)
		{
			List<PerformancePlanJpt> list = new List<PerformancePlanJpt>();

			foreach (PerformancePlanJpt emp in coll)
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
				return PerformancePlanJptMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanJptQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanJpt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanJpt();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanJptQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanJptQuery();
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
		public bool Load(PerformancePlanJptQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanJpt AddNew()
		{
			PerformancePlanJpt entity = base.AddNewEntity() as PerformancePlanJpt;

			return entity;
		}
		public PerformancePlanJpt FindByPrimaryKey(Int32 performancePlanID)
		{
			return base.FindByPrimaryKey(performancePlanID) as PerformancePlanJpt;
		}

		#region IEnumerable< PerformancePlanJpt> Members

		IEnumerator<PerformancePlanJpt> IEnumerable<PerformancePlanJpt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanJpt;
			}
		}

		#endregion

		private PerformancePlanJptQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanJpt' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanJpt ({PerformancePlanID})")]
	[Serializable]
	public partial class PerformancePlanJpt : esPerformancePlanJpt
	{
		public PerformancePlanJpt()
		{
		}

		public PerformancePlanJpt(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanJptMetadata.Meta();
			}
		}

		override protected esPerformancePlanJptQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanJptQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanJptQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanJptQuery();
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
		public bool Load(PerformancePlanJptQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanJptQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanJptQuery : esPerformancePlanJptQuery
	{
		public PerformancePlanJptQuery()
		{

		}

		public PerformancePlanJptQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanJptQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanJptMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanJptMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanJptMetadata.ColumnNames.PerformancePlanID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanJptMetadata.PropertyNames.PerformancePlanID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptMetadata.ColumnNames.OrganizationUnitID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanJptMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptMetadata.ColumnNames.SubOrganizationUnitID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanJptMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptMetadata.ColumnNames.PositionID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanJptMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanJptMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanJptMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanJptMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanJptMetadata Meta()
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
			public const string PerformancePlanID = "PerformancePlanID";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string PositionID = "PositionID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PerformancePlanID = "PerformancePlanID";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string PositionID = "PositionID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(PerformancePlanJptMetadata))
			{
				if (PerformancePlanJptMetadata.mapDelegates == null)
				{
					PerformancePlanJptMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanJptMetadata.meta == null)
				{
					PerformancePlanJptMetadata.meta = new PerformancePlanJptMetadata();
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

				meta.AddTypeMap("PerformancePlanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubOrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanJpt";
				meta.Destination = "PerformancePlanJpt";
				meta.spInsert = "proc_PerformancePlanJptInsert";
				meta.spUpdate = "proc_PerformancePlanJptUpdate";
				meta.spDelete = "proc_PerformancePlanJptDelete";
				meta.spLoadAll = "proc_PerformancePlanJptLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanJptLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanJptMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
