/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/10/2022 4:27:52 PM
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
	abstract public class esEmployeeLeaveCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeLeaveCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeLeaveCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeLeaveQuery query)
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
			this.InitQuery(query as esEmployeeLeaveQuery);
		}
		#endregion

		virtual public EmployeeLeave DetachEntity(EmployeeLeave entity)
		{
			return base.DetachEntity(entity) as EmployeeLeave;
		}

		virtual public EmployeeLeave AttachEntity(EmployeeLeave entity)
		{
			return base.AttachEntity(entity) as EmployeeLeave;
		}

		virtual public void Combine(EmployeeLeaveCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeLeave this[int index]
		{
			get
			{
				return base[index] as EmployeeLeave;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeLeave);
		}
	}

	[Serializable]
	abstract public class esEmployeeLeave : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeLeaveQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeLeave()
		{
		}

		public esEmployeeLeave(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 employeeLeaveID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLeaveID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLeaveID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 employeeLeaveID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLeaveID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLeaveID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 employeeLeaveID)
		{
			esEmployeeLeaveQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeLeaveID == employeeLeaveID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 employeeLeaveID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeLeaveID", employeeLeaveID);
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
						case "EmployeeLeaveID": this.str.EmployeeLeaveID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SREmployeeLeaveType": this.str.SREmployeeLeaveType = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "LeaveEntitlementsQty": this.str.LeaveEntitlementsQty = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsPayCut": this.str.IsPayCut = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeLeaveID":

							if (value == null || value is System.Int64)
								this.EmployeeLeaveID = (System.Int64?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "StartDate":

							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":

							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						case "LeaveEntitlementsQty":

							if (value == null || value is System.Int32)
								this.LeaveEntitlementsQty = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsPayCut":

							if (value == null || value is System.Boolean)
								this.IsPayCut = (System.Boolean?)value;
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
		/// Maps to EmployeeLeave.EmployeeLeaveID
		/// </summary>
		virtual public System.Int64? EmployeeLeaveID
		{
			get
			{
				return base.GetSystemInt64(EmployeeLeaveMetadata.ColumnNames.EmployeeLeaveID);
			}

			set
			{
				base.SetSystemInt64(EmployeeLeaveMetadata.ColumnNames.EmployeeLeaveID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeLeaveMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.SREmployeeLeaveType
		/// </summary>
		virtual public System.String SREmployeeLeaveType
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveMetadata.ColumnNames.SREmployeeLeaveType);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveMetadata.ColumnNames.SREmployeeLeaveType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveMetadata.ColumnNames.StartDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveMetadata.ColumnNames.EndDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.LeaveEntitlementsQty
		/// </summary>
		virtual public System.Int32? LeaveEntitlementsQty
		{
			get
			{
				return base.GetSystemInt32(EmployeeLeaveMetadata.ColumnNames.LeaveEntitlementsQty);
			}

			set
			{
				base.SetSystemInt32(EmployeeLeaveMetadata.ColumnNames.LeaveEntitlementsQty, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLeaveMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeLeaveMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLeaveMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeLeaveMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeLeave.IsPayCut
		/// </summary>
		virtual public System.Boolean? IsPayCut
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLeaveMetadata.ColumnNames.IsPayCut);
			}

			set
			{
				base.SetSystemBoolean(EmployeeLeaveMetadata.ColumnNames.IsPayCut, value);
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
			public esStrings(esEmployeeLeave entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeLeaveID
			{
				get
				{
					System.Int64? data = entity.EmployeeLeaveID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeLeaveID = null;
					else entity.EmployeeLeaveID = Convert.ToInt64(value);
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
			public System.String SREmployeeLeaveType
			{
				get
				{
					System.String data = entity.SREmployeeLeaveType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeLeaveType = null;
					else entity.SREmployeeLeaveType = Convert.ToString(value);
				}
			}
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
			public System.String LeaveEntitlementsQty
			{
				get
				{
					System.Int32? data = entity.LeaveEntitlementsQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveEntitlementsQty = null;
					else entity.LeaveEntitlementsQty = Convert.ToInt32(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			public System.String IsPayCut
			{
				get
				{
					System.Boolean? data = entity.IsPayCut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPayCut = null;
					else entity.IsPayCut = Convert.ToBoolean(value);
				}
			}
			private esEmployeeLeave entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeLeaveQuery query)
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
				throw new Exception("esEmployeeLeave can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeLeave : esEmployeeLeave
	{
	}

	[Serializable]
	abstract public class esEmployeeLeaveQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLeaveMetadata.Meta();
			}
		}

		public esQueryItem EmployeeLeaveID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.EmployeeLeaveID, esSystemType.Int64);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmployeeLeaveType
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.SREmployeeLeaveType, esSystemType.String);
			}
		}

		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LeaveEntitlementsQty
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.LeaveEntitlementsQty, esSystemType.Int32);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsPayCut
		{
			get
			{
				return new esQueryItem(this, EmployeeLeaveMetadata.ColumnNames.IsPayCut, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeLeaveCollection")]
	public partial class EmployeeLeaveCollection : esEmployeeLeaveCollection, IEnumerable<EmployeeLeave>
	{
		public EmployeeLeaveCollection()
		{

		}

		public static implicit operator List<EmployeeLeave>(EmployeeLeaveCollection coll)
		{
			List<EmployeeLeave> list = new List<EmployeeLeave>();

			foreach (EmployeeLeave emp in coll)
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
				return EmployeeLeaveMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLeaveQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeLeave(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeLeave();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeLeaveQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLeaveQuery();
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
		public bool Load(EmployeeLeaveQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeLeave AddNew()
		{
			EmployeeLeave entity = base.AddNewEntity() as EmployeeLeave;

			return entity;
		}
		public EmployeeLeave FindByPrimaryKey(Int64 employeeLeaveID)
		{
			return base.FindByPrimaryKey(employeeLeaveID) as EmployeeLeave;
		}

		#region IEnumerable< EmployeeLeave> Members

		IEnumerator<EmployeeLeave> IEnumerable<EmployeeLeave>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeLeave;
			}
		}

		#endregion

		private EmployeeLeaveQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeLeave' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeLeave ({EmployeeLeaveID})")]
	[Serializable]
	public partial class EmployeeLeave : esEmployeeLeave
	{
		public EmployeeLeave()
		{
		}

		public EmployeeLeave(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLeaveMetadata.Meta();
			}
		}

		override protected esEmployeeLeaveQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLeaveQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeLeaveQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLeaveQuery();
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
		public bool Load(EmployeeLeaveQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeLeaveQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeLeaveQuery : esEmployeeLeaveQuery
	{
		public EmployeeLeaveQuery()
		{

		}

		public EmployeeLeaveQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeLeaveQuery";
		}
	}

	[Serializable]
	public partial class EmployeeLeaveMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeLeaveMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.EmployeeLeaveID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.EmployeeLeaveID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.SREmployeeLeaveType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.SREmployeeLeaveType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.StartDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.EndDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.LeaveEntitlementsQty, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.LeaveEntitlementsQty;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.Notes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeLeaveMetadata.ColumnNames.IsPayCut, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLeaveMetadata.PropertyNames.IsPayCut;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeLeaveMetadata Meta()
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
			public const string EmployeeLeaveID = "EmployeeLeaveID";
			public const string PersonID = "PersonID";
			public const string SREmployeeLeaveType = "SREmployeeLeaveType";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string LeaveEntitlementsQty = "LeaveEntitlementsQty";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPayCut = "IsPayCut";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeLeaveID = "EmployeeLeaveID";
			public const string PersonID = "PersonID";
			public const string SREmployeeLeaveType = "SREmployeeLeaveType";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string LeaveEntitlementsQty = "LeaveEntitlementsQty";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsPayCut = "IsPayCut";
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
			lock (typeof(EmployeeLeaveMetadata))
			{
				if (EmployeeLeaveMetadata.mapDelegates == null)
				{
					EmployeeLeaveMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeLeaveMetadata.meta == null)
				{
					EmployeeLeaveMetadata.meta = new EmployeeLeaveMetadata();
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

				meta.AddTypeMap("EmployeeLeaveID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmployeeLeaveType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LeaveEntitlementsQty", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPayCut", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "EmployeeLeave";
				meta.Destination = "EmployeeLeave";
				meta.spInsert = "proc_EmployeeLeaveInsert";
				meta.spUpdate = "proc_EmployeeLeaveUpdate";
				meta.spDelete = "proc_EmployeeLeaveDelete";
				meta.spLoadAll = "proc_EmployeeLeaveLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeLeaveLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeLeaveMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
