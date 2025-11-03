/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/20/2021 1:09:37 PM
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
	abstract public class esOrganizationUnitCollection : esEntityCollectionWAuditLog
	{
		public esOrganizationUnitCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "OrganizationUnitCollection";
		}

		#region Query Logic
		protected void InitQuery(esOrganizationUnitQuery query)
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
			this.InitQuery(query as esOrganizationUnitQuery);
		}
		#endregion

		virtual public OrganizationUnit DetachEntity(OrganizationUnit entity)
		{
			return base.DetachEntity(entity) as OrganizationUnit;
		}

		virtual public OrganizationUnit AttachEntity(OrganizationUnit entity)
		{
			return base.AttachEntity(entity) as OrganizationUnit;
		}

		virtual public void Combine(OrganizationUnitCollection collection)
		{
			base.Combine(collection);
		}

		new public OrganizationUnit this[int index]
		{
			get
			{
				return base[index] as OrganizationUnit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OrganizationUnit);
		}
	}

	[Serializable]
	abstract public class esOrganizationUnit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOrganizationUnitQuery GetDynamicQuery()
		{
			return null;
		}

		public esOrganizationUnit()
		{
		}

		public esOrganizationUnit(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 organizationUnitID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(organizationUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(organizationUnitID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 organizationUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(organizationUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(organizationUnitID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 organizationUnitID)
		{
			esOrganizationUnitQuery query = this.GetDynamicQuery();
			query.Where(query.OrganizationUnitID == organizationUnitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 organizationUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("OrganizationUnitID", organizationUnitID);
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
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
						case "OrganizationUnitCode": this.str.OrganizationUnitCode = (string)value; break;
						case "OrganizationUnitName": this.str.OrganizationUnitName = (string)value; break;
						case "ParentOrganizationUnitID": this.str.ParentOrganizationUnitID = (string)value; break;
						case "SROrganizationLevel": this.str.SROrganizationLevel = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "OrganizationUnitID":

							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
							break;
						case "ParentOrganizationUnitID":

							if (value == null || value is System.Int32)
								this.ParentOrganizationUnitID = (System.Int32?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "SubLedgerId":

							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
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
		/// Maps to OrganizationUnit.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(OrganizationUnitMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(OrganizationUnitMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.OrganizationUnitCode
		/// </summary>
		virtual public System.String OrganizationUnitCode
		{
			get
			{
				return base.GetSystemString(OrganizationUnitMetadata.ColumnNames.OrganizationUnitCode);
			}

			set
			{
				base.SetSystemString(OrganizationUnitMetadata.ColumnNames.OrganizationUnitCode, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.OrganizationUnitName
		/// </summary>
		virtual public System.String OrganizationUnitName
		{
			get
			{
				return base.GetSystemString(OrganizationUnitMetadata.ColumnNames.OrganizationUnitName);
			}

			set
			{
				base.SetSystemString(OrganizationUnitMetadata.ColumnNames.OrganizationUnitName, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.ParentOrganizationUnitID
		/// </summary>
		virtual public System.Int32? ParentOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(OrganizationUnitMetadata.ColumnNames.ParentOrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(OrganizationUnitMetadata.ColumnNames.ParentOrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.SROrganizationLevel
		/// </summary>
		virtual public System.String SROrganizationLevel
		{
			get
			{
				return base.GetSystemString(OrganizationUnitMetadata.ColumnNames.SROrganizationLevel);
			}

			set
			{
				base.SetSystemString(OrganizationUnitMetadata.ColumnNames.SROrganizationLevel, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(OrganizationUnitMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(OrganizationUnitMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(OrganizationUnitMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(OrganizationUnitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(OrganizationUnitMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(OrganizationUnitMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(OrganizationUnitMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(OrganizationUnitMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to OrganizationUnit.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(OrganizationUnitMetadata.ColumnNames.SubLedgerId);
			}

			set
			{
				base.SetSystemInt32(OrganizationUnitMetadata.ColumnNames.SubLedgerId, value);
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
			public esStrings(esOrganizationUnit entity)
			{
				this.entity = entity;
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
			public System.String OrganizationUnitCode
			{
				get
				{
					System.String data = entity.OrganizationUnitCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitCode = null;
					else entity.OrganizationUnitCode = Convert.ToString(value);
				}
			}
			public System.String OrganizationUnitName
			{
				get
				{
					System.String data = entity.OrganizationUnitName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitName = null;
					else entity.OrganizationUnitName = Convert.ToString(value);
				}
			}
			public System.String ParentOrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.ParentOrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentOrganizationUnitID = null;
					else entity.ParentOrganizationUnitID = Convert.ToInt32(value);
				}
			}
			public System.String SROrganizationLevel
			{
				get
				{
					System.String data = entity.SROrganizationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROrganizationLevel = null;
					else entity.SROrganizationLevel = Convert.ToString(value);
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
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String SubLedgerId
			{
				get
				{
					System.Int32? data = entity.SubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerId = null;
					else entity.SubLedgerId = Convert.ToInt32(value);
				}
			}
			private esOrganizationUnit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOrganizationUnitQuery query)
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
				throw new Exception("esOrganizationUnit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class OrganizationUnit : esOrganizationUnit
	{
	}

	[Serializable]
	abstract public class esOrganizationUnitQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return OrganizationUnitMetadata.Meta();
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem OrganizationUnitCode
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.OrganizationUnitCode, esSystemType.String);
			}
		}

		public esQueryItem OrganizationUnitName
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.OrganizationUnitName, esSystemType.String);
			}
		}

		public esQueryItem ParentOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.ParentOrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SROrganizationLevel
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.SROrganizationLevel, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, OrganizationUnitMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("OrganizationUnitCollection")]
	public partial class OrganizationUnitCollection : esOrganizationUnitCollection, IEnumerable<OrganizationUnit>
	{
		public OrganizationUnitCollection()
		{

		}

		public static implicit operator List<OrganizationUnit>(OrganizationUnitCollection coll)
		{
			List<OrganizationUnit> list = new List<OrganizationUnit>();

			foreach (OrganizationUnit emp in coll)
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
				return OrganizationUnitMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrganizationUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OrganizationUnit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OrganizationUnit();
		}

		#endregion

		[BrowsableAttribute(false)]
		public OrganizationUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrganizationUnitQuery();
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
		public bool Load(OrganizationUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public OrganizationUnit AddNew()
		{
			OrganizationUnit entity = base.AddNewEntity() as OrganizationUnit;

			return entity;
		}
		public OrganizationUnit FindByPrimaryKey(Int32 organizationUnitID)
		{
			return base.FindByPrimaryKey(organizationUnitID) as OrganizationUnit;
		}

		#region IEnumerable< OrganizationUnit> Members

		IEnumerator<OrganizationUnit> IEnumerable<OrganizationUnit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as OrganizationUnit;
			}
		}

		#endregion

		private OrganizationUnitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'OrganizationUnit' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("OrganizationUnit ({OrganizationUnitID})")]
	[Serializable]
	public partial class OrganizationUnit : esOrganizationUnit
	{
		public OrganizationUnit()
		{
		}

		public OrganizationUnit(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OrganizationUnitMetadata.Meta();
			}
		}

		override protected esOrganizationUnitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrganizationUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public OrganizationUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrganizationUnitQuery();
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
		public bool Load(OrganizationUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private OrganizationUnitQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class OrganizationUnitQuery : esOrganizationUnitQuery
	{
		public OrganizationUnitQuery()
		{

		}

		public OrganizationUnitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "OrganizationUnitQuery";
		}
	}

	[Serializable]
	public partial class OrganizationUnitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OrganizationUnitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.OrganizationUnitID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.OrganizationUnitID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.OrganizationUnitCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.OrganizationUnitCode;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.OrganizationUnitName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.OrganizationUnitName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.ParentOrganizationUnitID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.ParentOrganizationUnitID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.SROrganizationLevel, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.SROrganizationLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.PersonID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(OrganizationUnitMetadata.ColumnNames.SubLedgerId, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrganizationUnitMetadata.PropertyNames.SubLedgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public OrganizationUnitMetadata Meta()
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
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string OrganizationUnitCode = "OrganizationUnitCode";
			public const string OrganizationUnitName = "OrganizationUnitName";
			public const string ParentOrganizationUnitID = "ParentOrganizationUnitID";
			public const string SROrganizationLevel = "SROrganizationLevel";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PersonID = "PersonID";
			public const string SubLedgerId = "SubLedgerId";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string OrganizationUnitCode = "OrganizationUnitCode";
			public const string OrganizationUnitName = "OrganizationUnitName";
			public const string ParentOrganizationUnitID = "ParentOrganizationUnitID";
			public const string SROrganizationLevel = "SROrganizationLevel";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PersonID = "PersonID";
			public const string SubLedgerId = "SubLedgerId";
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
			lock (typeof(OrganizationUnitMetadata))
			{
				if (OrganizationUnitMetadata.mapDelegates == null)
				{
					OrganizationUnitMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (OrganizationUnitMetadata.meta == null)
				{
					OrganizationUnitMetadata.meta = new OrganizationUnitMetadata();
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

				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrganizationUnitCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrganizationUnitName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentOrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SROrganizationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));


				meta.Source = "OrganizationUnit";
				meta.Destination = "OrganizationUnit";
				meta.spInsert = "proc_OrganizationUnitInsert";
				meta.spUpdate = "proc_OrganizationUnitUpdate";
				meta.spDelete = "proc_OrganizationUnitDelete";
				meta.spLoadAll = "proc_OrganizationUnitLoadAll";
				meta.spLoadByPrimaryKey = "proc_OrganizationUnitLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OrganizationUnitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
