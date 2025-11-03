/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2023 11:18:09 AM
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
	abstract public class esPerformancePlanPppkCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanPppkCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanPppkCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanPppkQuery query)
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
			this.InitQuery(query as esPerformancePlanPppkQuery);
		}
		#endregion

		virtual public PerformancePlanPppk DetachEntity(PerformancePlanPppk entity)
		{
			return base.DetachEntity(entity) as PerformancePlanPppk;
		}

		virtual public PerformancePlanPppk AttachEntity(PerformancePlanPppk entity)
		{
			return base.AttachEntity(entity) as PerformancePlanPppk;
		}

		virtual public void Combine(PerformancePlanPppkCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanPppk this[int index]
		{
			get
			{
				return base[index] as PerformancePlanPppk;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanPppk);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanPppk : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanPppkQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanPppk()
		{
		}

		public esPerformancePlanPppk(DataRow row)
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
			esPerformancePlanPppkQuery query = this.GetDynamicQuery();
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
		/// Maps to PerformancePlanPppk.PerformancePlanID
		/// </summary>
		virtual public System.Int32? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanPppkMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanPppkMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppk.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanPppkMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanPppkMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppk.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanPppkMetadata.ColumnNames.SubOrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanPppkMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppk.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanPppkMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanPppkMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppk.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanPppkMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanPppkMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppk.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppk.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanPppkMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanPppkMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanPppk.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanPppkMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanPppkMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanPppk entity)
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
			private esPerformancePlanPppk entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanPppkQuery query)
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
				throw new Exception("esPerformancePlanPppk can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanPppk : esPerformancePlanPppk
	{
	}

	[Serializable]
	abstract public class esPerformancePlanPppkQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanPppkMetadata.Meta();
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkMetadata.ColumnNames.PerformancePlanID, esSystemType.Int32);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanPppkMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanPppkCollection")]
	public partial class PerformancePlanPppkCollection : esPerformancePlanPppkCollection, IEnumerable<PerformancePlanPppk>
	{
		public PerformancePlanPppkCollection()
		{

		}

		public static implicit operator List<PerformancePlanPppk>(PerformancePlanPppkCollection coll)
		{
			List<PerformancePlanPppk> list = new List<PerformancePlanPppk>();

			foreach (PerformancePlanPppk emp in coll)
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
				return PerformancePlanPppkMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanPppkQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanPppk(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanPppk();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanPppkQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanPppkQuery();
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
		public bool Load(PerformancePlanPppkQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanPppk AddNew()
		{
			PerformancePlanPppk entity = base.AddNewEntity() as PerformancePlanPppk;

			return entity;
		}
		public PerformancePlanPppk FindByPrimaryKey(Int32 performancePlanID)
		{
			return base.FindByPrimaryKey(performancePlanID) as PerformancePlanPppk;
		}

		#region IEnumerable< PerformancePlanPppk> Members

		IEnumerator<PerformancePlanPppk> IEnumerable<PerformancePlanPppk>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanPppk;
			}
		}

		#endregion

		private PerformancePlanPppkQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanPppk' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanPppk ({PerformancePlanID})")]
	[Serializable]
	public partial class PerformancePlanPppk : esPerformancePlanPppk
	{
		public PerformancePlanPppk()
		{
		}

		public PerformancePlanPppk(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanPppkMetadata.Meta();
			}
		}

		override protected esPerformancePlanPppkQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanPppkQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanPppkQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanPppkQuery();
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
		public bool Load(PerformancePlanPppkQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanPppkQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanPppkQuery : esPerformancePlanPppkQuery
	{
		public PerformancePlanPppkQuery()
		{

		}

		public PerformancePlanPppkQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanPppkQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanPppkMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanPppkMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanPppkMetadata.ColumnNames.PerformancePlanID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanPppkMetadata.PropertyNames.PerformancePlanID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkMetadata.ColumnNames.OrganizationUnitID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanPppkMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkMetadata.ColumnNames.SubOrganizationUnitID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanPppkMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkMetadata.ColumnNames.PositionID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanPppkMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanPppkMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkMetadata.ColumnNames.CreatedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanPppkMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanPppkMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanPppkMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanPppkMetadata Meta()
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
			lock (typeof(PerformancePlanPppkMetadata))
			{
				if (PerformancePlanPppkMetadata.mapDelegates == null)
				{
					PerformancePlanPppkMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanPppkMetadata.meta == null)
				{
					PerformancePlanPppkMetadata.meta = new PerformancePlanPppkMetadata();
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


				meta.Source = "PerformancePlanPppk";
				meta.Destination = "PerformancePlanPppk";
				meta.spInsert = "proc_PerformancePlanPppkInsert";
				meta.spUpdate = "proc_PerformancePlanPppkUpdate";
				meta.spDelete = "proc_PerformancePlanPppkDelete";
				meta.spLoadAll = "proc_PerformancePlanPppkLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanPppkLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanPppkMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
