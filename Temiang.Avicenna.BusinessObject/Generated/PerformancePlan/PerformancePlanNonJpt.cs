/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/21/2023 10:40:13 AM
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
	abstract public class esPerformancePlanNonJptCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanNonJptCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanNonJptCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanNonJptQuery query)
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
			this.InitQuery(query as esPerformancePlanNonJptQuery);
		}
		#endregion

		virtual public PerformancePlanNonJpt DetachEntity(PerformancePlanNonJpt entity)
		{
			return base.DetachEntity(entity) as PerformancePlanNonJpt;
		}

		virtual public PerformancePlanNonJpt AttachEntity(PerformancePlanNonJpt entity)
		{
			return base.AttachEntity(entity) as PerformancePlanNonJpt;
		}

		virtual public void Combine(PerformancePlanNonJptCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanNonJpt this[int index]
		{
			get
			{
				return base[index] as PerformancePlanNonJpt;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanNonJpt);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanNonJpt : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanNonJptQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanNonJpt()
		{
		}

		public esPerformancePlanNonJpt(DataRow row)
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
			esPerformancePlanNonJptQuery query = this.GetDynamicQuery();
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
						case "StartMonth": this.str.StartMonth = (string)value; break;
						case "EndMonth": this.str.EndMonth = (string)value; break;
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
						case "SubOrganizationUnitID": this.str.SubOrganizationUnitID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
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
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to PerformancePlanNonJpt.PerformancePlanID
		/// </summary>
		virtual public System.Int32? PerformancePlanID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanNonJptMetadata.ColumnNames.PerformancePlanID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanNonJptMetadata.ColumnNames.PerformancePlanID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.StartMonth
		/// </summary>
		virtual public System.String StartMonth
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptMetadata.ColumnNames.StartMonth);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptMetadata.ColumnNames.StartMonth, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.EndMonth
		/// </summary>
		virtual public System.String EndMonth
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptMetadata.ColumnNames.EndMonth);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptMetadata.ColumnNames.EndMonth, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanNonJptMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanNonJptMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanNonJptMetadata.ColumnNames.SubOrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanNonJptMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanNonJptMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanNonJptMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(PerformancePlanNonJptMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(PerformancePlanNonJptMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanNonJptMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanNonJptMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanNonJpt.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanNonJptMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanNonJptMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanNonJpt entity)
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
			public System.String StartMonth
			{
				get
				{
					System.String data = entity.StartMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartMonth = null;
					else entity.StartMonth = Convert.ToString(value);
				}
			}
			public System.String EndMonth
			{
				get
				{
					System.String data = entity.EndMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndMonth = null;
					else entity.EndMonth = Convert.ToString(value);
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
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			private esPerformancePlanNonJpt entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanNonJptQuery query)
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
				throw new Exception("esPerformancePlanNonJpt can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanNonJpt : esPerformancePlanNonJpt
	{
	}

	[Serializable]
	abstract public class esPerformancePlanNonJptQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanNonJptMetadata.Meta();
			}
		}

		public esQueryItem PerformancePlanID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.PerformancePlanID, esSystemType.Int32);
			}
		}

		public esQueryItem StartMonth
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.StartMonth, esSystemType.String);
			}
		}

		public esQueryItem EndMonth
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.EndMonth, esSystemType.String);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanNonJptMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanNonJptCollection")]
	public partial class PerformancePlanNonJptCollection : esPerformancePlanNonJptCollection, IEnumerable<PerformancePlanNonJpt>
	{
		public PerformancePlanNonJptCollection()
		{

		}

		public static implicit operator List<PerformancePlanNonJpt>(PerformancePlanNonJptCollection coll)
		{
			List<PerformancePlanNonJpt> list = new List<PerformancePlanNonJpt>();

			foreach (PerformancePlanNonJpt emp in coll)
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
				return PerformancePlanNonJptMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanNonJptQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanNonJpt(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanNonJpt();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanNonJptQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanNonJptQuery();
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
		public bool Load(PerformancePlanNonJptQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanNonJpt AddNew()
		{
			PerformancePlanNonJpt entity = base.AddNewEntity() as PerformancePlanNonJpt;

			return entity;
		}
		public PerformancePlanNonJpt FindByPrimaryKey(Int32 performancePlanID)
		{
			return base.FindByPrimaryKey(performancePlanID) as PerformancePlanNonJpt;
		}

		#region IEnumerable< PerformancePlanNonJpt> Members

		IEnumerator<PerformancePlanNonJpt> IEnumerable<PerformancePlanNonJpt>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanNonJpt;
			}
		}

		#endregion

		private PerformancePlanNonJptQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanNonJpt' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanNonJpt ({PerformancePlanID})")]
	[Serializable]
	public partial class PerformancePlanNonJpt : esPerformancePlanNonJpt
	{
		public PerformancePlanNonJpt()
		{
		}

		public PerformancePlanNonJpt(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanNonJptMetadata.Meta();
			}
		}

		override protected esPerformancePlanNonJptQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanNonJptQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanNonJptQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanNonJptQuery();
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
		public bool Load(PerformancePlanNonJptQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanNonJptQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanNonJptQuery : esPerformancePlanNonJptQuery
	{
		public PerformancePlanNonJptQuery()
		{

		}

		public PerformancePlanNonJptQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanNonJptQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanNonJptMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanNonJptMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.PerformancePlanID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.PerformancePlanID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.StartMonth, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.StartMonth;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.EndMonth, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.EndMonth;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.OrganizationUnitID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.SubOrganizationUnitID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.PositionID, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.IsActive, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.CreatedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanNonJptMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanNonJptMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanNonJptMetadata Meta()
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
			public const string StartMonth = "StartMonth";
			public const string EndMonth = "EndMonth";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string PositionID = "PositionID";
			public const string IsActive = "IsActive";
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
			public const string StartMonth = "StartMonth";
			public const string EndMonth = "EndMonth";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string PositionID = "PositionID";
			public const string IsActive = "IsActive";
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
			lock (typeof(PerformancePlanNonJptMetadata))
			{
				if (PerformancePlanNonJptMetadata.mapDelegates == null)
				{
					PerformancePlanNonJptMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanNonJptMetadata.meta == null)
				{
					PerformancePlanNonJptMetadata.meta = new PerformancePlanNonJptMetadata();
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
				meta.AddTypeMap("StartMonth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EndMonth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubOrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanNonJpt";
				meta.Destination = "PerformancePlanNonJpt";
				meta.spInsert = "proc_PerformancePlanNonJptInsert";
				meta.spUpdate = "proc_PerformancePlanNonJptUpdate";
				meta.spDelete = "proc_PerformancePlanNonJptDelete";
				meta.spLoadAll = "proc_PerformancePlanNonJptLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanNonJptLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanNonJptMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
