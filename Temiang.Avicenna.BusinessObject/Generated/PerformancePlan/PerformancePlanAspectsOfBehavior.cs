/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/7/2023 11:16:18 AM
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
	abstract public class esPerformancePlanAspectsOfBehaviorCollection : esEntityCollectionWAuditLog
	{
		public esPerformancePlanAspectsOfBehaviorCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PerformancePlanAspectsOfBehaviorCollection";
		}

		#region Query Logic
		protected void InitQuery(esPerformancePlanAspectsOfBehaviorQuery query)
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
			this.InitQuery(query as esPerformancePlanAspectsOfBehaviorQuery);
		}
		#endregion

		virtual public PerformancePlanAspectsOfBehavior DetachEntity(PerformancePlanAspectsOfBehavior entity)
		{
			return base.DetachEntity(entity) as PerformancePlanAspectsOfBehavior;
		}

		virtual public PerformancePlanAspectsOfBehavior AttachEntity(PerformancePlanAspectsOfBehavior entity)
		{
			return base.AttachEntity(entity) as PerformancePlanAspectsOfBehavior;
		}

		virtual public void Combine(PerformancePlanAspectsOfBehaviorCollection collection)
		{
			base.Combine(collection);
		}

		new public PerformancePlanAspectsOfBehavior this[int index]
		{
			get
			{
				return base[index] as PerformancePlanAspectsOfBehavior;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PerformancePlanAspectsOfBehavior);
		}
	}

	[Serializable]
	abstract public class esPerformancePlanAspectsOfBehavior : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPerformancePlanAspectsOfBehaviorQuery GetDynamicQuery()
		{
			return null;
		}

		public esPerformancePlanAspectsOfBehavior()
		{
		}

		public esPerformancePlanAspectsOfBehavior(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 aspectsOfBehaviorID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(aspectsOfBehaviorID);
			else
				return LoadByPrimaryKeyStoredProcedure(aspectsOfBehaviorID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 aspectsOfBehaviorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(aspectsOfBehaviorID);
			else
				return LoadByPrimaryKeyStoredProcedure(aspectsOfBehaviorID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 aspectsOfBehaviorID)
		{
			esPerformancePlanAspectsOfBehaviorQuery query = this.GetDynamicQuery();
			query.Where(query.AspectsOfBehaviorID == aspectsOfBehaviorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 aspectsOfBehaviorID)
		{
			esParameters parms = new esParameters();
			parms.Add("AspectsOfBehaviorID", aspectsOfBehaviorID);
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
						case "AspectsOfBehaviorID": this.str.AspectsOfBehaviorID = (string)value; break;
						case "AspectsOfBehaviorCode": this.str.AspectsOfBehaviorCode = (string)value; break;
						case "AspectsOfBehaviorName": this.str.AspectsOfBehaviorName = (string)value; break;
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
						case "AspectsOfBehaviorID":

							if (value == null || value is System.Int32)
								this.AspectsOfBehaviorID = (System.Int32?)value;
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
		/// Maps to PerformancePlanAspectsOfBehavior.AspectsOfBehaviorID
		/// </summary>
		virtual public System.Int32? AspectsOfBehaviorID
		{
			get
			{
				return base.GetSystemInt32(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorID);
			}

			set
			{
				base.SetSystemInt32(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehavior.AspectsOfBehaviorCode
		/// </summary>
		virtual public System.String AspectsOfBehaviorCode
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorCode);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorCode, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehavior.AspectsOfBehaviorName
		/// </summary>
		virtual public System.String AspectsOfBehaviorName
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorName);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorName, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehavior.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehavior.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehavior.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PerformancePlanAspectsOfBehavior.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPerformancePlanAspectsOfBehavior entity)
			{
				this.entity = entity;
			}
			public System.String AspectsOfBehaviorID
			{
				get
				{
					System.Int32? data = entity.AspectsOfBehaviorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AspectsOfBehaviorID = null;
					else entity.AspectsOfBehaviorID = Convert.ToInt32(value);
				}
			}
			public System.String AspectsOfBehaviorCode
			{
				get
				{
					System.String data = entity.AspectsOfBehaviorCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AspectsOfBehaviorCode = null;
					else entity.AspectsOfBehaviorCode = Convert.ToString(value);
				}
			}
			public System.String AspectsOfBehaviorName
			{
				get
				{
					System.String data = entity.AspectsOfBehaviorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AspectsOfBehaviorName = null;
					else entity.AspectsOfBehaviorName = Convert.ToString(value);
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
			private esPerformancePlanAspectsOfBehavior entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPerformancePlanAspectsOfBehaviorQuery query)
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
				throw new Exception("esPerformancePlanAspectsOfBehavior can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PerformancePlanAspectsOfBehavior : esPerformancePlanAspectsOfBehavior
	{
	}

	[Serializable]
	abstract public class esPerformancePlanAspectsOfBehaviorQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanAspectsOfBehaviorMetadata.Meta();
			}
		}

		public esQueryItem AspectsOfBehaviorID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorID, esSystemType.Int32);
			}
		}

		public esQueryItem AspectsOfBehaviorCode
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorCode, esSystemType.String);
			}
		}

		public esQueryItem AspectsOfBehaviorName
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorName, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PerformancePlanAspectsOfBehaviorCollection")]
	public partial class PerformancePlanAspectsOfBehaviorCollection : esPerformancePlanAspectsOfBehaviorCollection, IEnumerable<PerformancePlanAspectsOfBehavior>
	{
		public PerformancePlanAspectsOfBehaviorCollection()
		{

		}

		public static implicit operator List<PerformancePlanAspectsOfBehavior>(PerformancePlanAspectsOfBehaviorCollection coll)
		{
			List<PerformancePlanAspectsOfBehavior> list = new List<PerformancePlanAspectsOfBehavior>();

			foreach (PerformancePlanAspectsOfBehavior emp in coll)
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
				return PerformancePlanAspectsOfBehaviorMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanAspectsOfBehaviorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PerformancePlanAspectsOfBehavior(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PerformancePlanAspectsOfBehavior();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanAspectsOfBehaviorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanAspectsOfBehaviorQuery();
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
		public bool Load(PerformancePlanAspectsOfBehaviorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PerformancePlanAspectsOfBehavior AddNew()
		{
			PerformancePlanAspectsOfBehavior entity = base.AddNewEntity() as PerformancePlanAspectsOfBehavior;

			return entity;
		}
		public PerformancePlanAspectsOfBehavior FindByPrimaryKey(Int32 aspectsOfBehaviorID)
		{
			return base.FindByPrimaryKey(aspectsOfBehaviorID) as PerformancePlanAspectsOfBehavior;
		}

		#region IEnumerable< PerformancePlanAspectsOfBehavior> Members

		IEnumerator<PerformancePlanAspectsOfBehavior> IEnumerable<PerformancePlanAspectsOfBehavior>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PerformancePlanAspectsOfBehavior;
			}
		}

		#endregion

		private PerformancePlanAspectsOfBehaviorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PerformancePlanAspectsOfBehavior' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PerformancePlanAspectsOfBehavior ({AspectsOfBehaviorID})")]
	[Serializable]
	public partial class PerformancePlanAspectsOfBehavior : esPerformancePlanAspectsOfBehavior
	{
		public PerformancePlanAspectsOfBehavior()
		{
		}

		public PerformancePlanAspectsOfBehavior(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PerformancePlanAspectsOfBehaviorMetadata.Meta();
			}
		}

		override protected esPerformancePlanAspectsOfBehaviorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PerformancePlanAspectsOfBehaviorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PerformancePlanAspectsOfBehaviorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PerformancePlanAspectsOfBehaviorQuery();
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
		public bool Load(PerformancePlanAspectsOfBehaviorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PerformancePlanAspectsOfBehaviorQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PerformancePlanAspectsOfBehaviorQuery : esPerformancePlanAspectsOfBehaviorQuery
	{
		public PerformancePlanAspectsOfBehaviorQuery()
		{

		}

		public PerformancePlanAspectsOfBehaviorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PerformancePlanAspectsOfBehaviorQuery";
		}
	}

	[Serializable]
	public partial class PerformancePlanAspectsOfBehaviorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PerformancePlanAspectsOfBehaviorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PerformancePlanAspectsOfBehaviorMetadata.PropertyNames.AspectsOfBehaviorID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorMetadata.PropertyNames.AspectsOfBehaviorCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.AspectsOfBehaviorName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorMetadata.PropertyNames.AspectsOfBehaviorName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.CreatedDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanAspectsOfBehaviorMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.CreatedByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PerformancePlanAspectsOfBehaviorMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PerformancePlanAspectsOfBehaviorMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PerformancePlanAspectsOfBehaviorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PerformancePlanAspectsOfBehaviorMetadata Meta()
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
			public const string AspectsOfBehaviorID = "AspectsOfBehaviorID";
			public const string AspectsOfBehaviorCode = "AspectsOfBehaviorCode";
			public const string AspectsOfBehaviorName = "AspectsOfBehaviorName";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AspectsOfBehaviorID = "AspectsOfBehaviorID";
			public const string AspectsOfBehaviorCode = "AspectsOfBehaviorCode";
			public const string AspectsOfBehaviorName = "AspectsOfBehaviorName";
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
			lock (typeof(PerformancePlanAspectsOfBehaviorMetadata))
			{
				if (PerformancePlanAspectsOfBehaviorMetadata.mapDelegates == null)
				{
					PerformancePlanAspectsOfBehaviorMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PerformancePlanAspectsOfBehaviorMetadata.meta == null)
				{
					PerformancePlanAspectsOfBehaviorMetadata.meta = new PerformancePlanAspectsOfBehaviorMetadata();
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

				meta.AddTypeMap("AspectsOfBehaviorID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AspectsOfBehaviorCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AspectsOfBehaviorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PerformancePlanAspectsOfBehavior";
				meta.Destination = "PerformancePlanAspectsOfBehavior";
				meta.spInsert = "proc_PerformancePlanAspectsOfBehaviorInsert";
				meta.spUpdate = "proc_PerformancePlanAspectsOfBehaviorUpdate";
				meta.spDelete = "proc_PerformancePlanAspectsOfBehaviorDelete";
				meta.spLoadAll = "proc_PerformancePlanAspectsOfBehaviorLoadAll";
				meta.spLoadByPrimaryKey = "proc_PerformancePlanAspectsOfBehaviorLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PerformancePlanAspectsOfBehaviorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
