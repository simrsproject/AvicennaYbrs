/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/20/2022 2:28:44 PM
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
	abstract public class esSubLedgersCollection : esEntityCollectionWAuditLog
	{
		public esSubLedgersCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SubLedgersCollection";
		}

		#region Query Logic
		protected void InitQuery(esSubLedgersQuery query)
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
			this.InitQuery(query as esSubLedgersQuery);
		}
		#endregion

		virtual public SubLedgers DetachEntity(SubLedgers entity)
		{
			return base.DetachEntity(entity) as SubLedgers;
		}

		virtual public SubLedgers AttachEntity(SubLedgers entity)
		{
			return base.AttachEntity(entity) as SubLedgers;
		}

		virtual public void Combine(SubLedgersCollection collection)
		{
			base.Combine(collection);
		}

		new public SubLedgers this[int index]
		{
			get
			{
				return base[index] as SubLedgers;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SubLedgers);
		}
	}

	[Serializable]
	abstract public class esSubLedgers : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSubLedgersQuery GetDynamicQuery()
		{
			return null;
		}

		public esSubLedgers()
		{
		}

		public esSubLedgers(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 subLedgerId)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subLedgerId);
			else
				return LoadByPrimaryKeyStoredProcedure(subLedgerId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 subLedgerId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subLedgerId);
			else
				return LoadByPrimaryKeyStoredProcedure(subLedgerId);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 subLedgerId)
		{
			esSubLedgersQuery query = this.GetDynamicQuery();
			query.Where(query.SubLedgerId == subLedgerId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 subLedgerId)
		{
			esParameters parms = new esParameters();
			parms.Add("SubLedgerId", subLedgerId);
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
						case "SubLedgerId": this.str.SubLedgerId = (string)value; break;
						case "GroupId": this.str.GroupId = (string)value; break;
						case "SubLedgerName": this.str.SubLedgerName = (string)value; break;
						case "Description": this.str.Description = (string)value; break;
						case "DateCreated": this.str.DateCreated = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "CreatedBy": this.str.CreatedBy = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "TempID": this.str.TempID = (string)value; break;
						case "HoSubLedgerId": this.str.HoSubLedgerId = (string)value; break;
						case "HoDescription": this.str.HoDescription = (string)value; break;
						case "IsDirectCost": this.str.IsDirectCost = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SubLedgerId":

							if (value == null || value is System.Int32)
								this.SubLedgerId = (System.Int32?)value;
							break;
						case "GroupId":

							if (value == null || value is System.Int32)
								this.GroupId = (System.Int32?)value;
							break;
						case "DateCreated":

							if (value == null || value is System.DateTime)
								this.DateCreated = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "tempID":

							if (value == null || value is System.Int32)
								this.TempID = (System.Int32?)value;
							break;
						case "HoSubLedgerId":

							if (value == null || value is System.Int32)
								this.HoSubLedgerId = (System.Int32?)value;
							break;
						case "IsDirectCost":

							if (value == null || value is System.Boolean)
								this.IsDirectCost = (System.Boolean?)value;
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
		/// Maps to SubLedgers.SubLedgerId
		/// </summary>
		virtual public System.Int32? SubLedgerId
		{
			get
			{
				return base.GetSystemInt32(SubLedgersMetadata.ColumnNames.SubLedgerId);
			}

			set
			{
				base.SetSystemInt32(SubLedgersMetadata.ColumnNames.SubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.GroupId
		/// </summary>
		virtual public System.Int32? GroupId
		{
			get
			{
				return base.GetSystemInt32(SubLedgersMetadata.ColumnNames.GroupId);
			}

			set
			{
				base.SetSystemInt32(SubLedgersMetadata.ColumnNames.GroupId, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.SubLedgerName
		/// </summary>
		virtual public System.String SubLedgerName
		{
			get
			{
				return base.GetSystemString(SubLedgersMetadata.ColumnNames.SubLedgerName);
			}

			set
			{
				base.SetSystemString(SubLedgersMetadata.ColumnNames.SubLedgerName, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(SubLedgersMetadata.ColumnNames.Description);
			}

			set
			{
				base.SetSystemString(SubLedgersMetadata.ColumnNames.Description, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.DateCreated
		/// </summary>
		virtual public System.DateTime? DateCreated
		{
			get
			{
				return base.GetSystemDateTime(SubLedgersMetadata.ColumnNames.DateCreated);
			}

			set
			{
				base.SetSystemDateTime(SubLedgersMetadata.ColumnNames.DateCreated, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SubLedgersMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SubLedgersMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(SubLedgersMetadata.ColumnNames.CreatedBy);
			}

			set
			{
				base.SetSystemString(SubLedgersMetadata.ColumnNames.CreatedBy, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SubLedgersMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SubLedgersMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.tempID
		/// </summary>
		virtual public System.Int32? TempID
		{
			get
			{
				return base.GetSystemInt32(SubLedgersMetadata.ColumnNames.TempID);
			}

			set
			{
				base.SetSystemInt32(SubLedgersMetadata.ColumnNames.TempID, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.HoSubLedgerId
		/// </summary>
		virtual public System.Int32? HoSubLedgerId
		{
			get
			{
				return base.GetSystemInt32(SubLedgersMetadata.ColumnNames.HoSubLedgerId);
			}

			set
			{
				base.SetSystemInt32(SubLedgersMetadata.ColumnNames.HoSubLedgerId, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.HoDescription
		/// </summary>
		virtual public System.String HoDescription
		{
			get
			{
				return base.GetSystemString(SubLedgersMetadata.ColumnNames.HoDescription);
			}

			set
			{
				base.SetSystemString(SubLedgersMetadata.ColumnNames.HoDescription, value);
			}
		}
		/// <summary>
		/// Maps to SubLedgers.IsDirectCost
		/// </summary>
		virtual public System.Boolean? IsDirectCost
		{
			get
			{
				return base.GetSystemBoolean(SubLedgersMetadata.ColumnNames.IsDirectCost);
			}

			set
			{
				base.SetSystemBoolean(SubLedgersMetadata.ColumnNames.IsDirectCost, value);
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
			public esStrings(esSubLedgers entity)
			{
				this.entity = entity;
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
			public System.String GroupId
			{
				get
				{
					System.Int32? data = entity.GroupId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GroupId = null;
					else entity.GroupId = Convert.ToInt32(value);
				}
			}
			public System.String SubLedgerName
			{
				get
				{
					System.String data = entity.SubLedgerName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubLedgerName = null;
					else entity.SubLedgerName = Convert.ToString(value);
				}
			}
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
			public System.String DateCreated
			{
				get
				{
					System.DateTime? data = entity.DateCreated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateCreated = null;
					else entity.DateCreated = Convert.ToDateTime(value);
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
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
			public System.String TempID
			{
				get
				{
					System.Int32? data = entity.TempID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TempID = null;
					else entity.TempID = Convert.ToInt32(value);
				}
			}
			public System.String HoSubLedgerId
			{
				get
				{
					System.Int32? data = entity.HoSubLedgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HoSubLedgerId = null;
					else entity.HoSubLedgerId = Convert.ToInt32(value);
				}
			}
			public System.String HoDescription
			{
				get
				{
					System.String data = entity.HoDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HoDescription = null;
					else entity.HoDescription = Convert.ToString(value);
				}
			}
			public System.String IsDirectCost
			{
				get
				{
					System.Boolean? data = entity.IsDirectCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDirectCost = null;
					else entity.IsDirectCost = Convert.ToBoolean(value);
				}
			}
			private esSubLedgers entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSubLedgersQuery query)
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
				throw new Exception("esSubLedgers can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SubLedgers : esSubLedgers
	{
	}

	[Serializable]
	abstract public class esSubLedgersQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SubLedgersMetadata.Meta();
			}
		}

		public esQueryItem SubLedgerId
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.SubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem GroupId
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.GroupId, esSystemType.Int32);
			}
		}

		public esQueryItem SubLedgerName
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.SubLedgerName, esSystemType.String);
			}
		}

		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.Description, esSystemType.String);
			}
		}

		public esQueryItem DateCreated
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.DateCreated, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem TempID
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.TempID, esSystemType.Int32);
			}
		}

		public esQueryItem HoSubLedgerId
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.HoSubLedgerId, esSystemType.Int32);
			}
		}

		public esQueryItem HoDescription
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.HoDescription, esSystemType.String);
			}
		}

		public esQueryItem IsDirectCost
		{
			get
			{
				return new esQueryItem(this, SubLedgersMetadata.ColumnNames.IsDirectCost, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SubLedgersCollection")]
	public partial class SubLedgersCollection : esSubLedgersCollection, IEnumerable<SubLedgers>
	{
		public SubLedgersCollection()
		{

		}

		public static implicit operator List<SubLedgers>(SubLedgersCollection coll)
		{
			List<SubLedgers> list = new List<SubLedgers>();

			foreach (SubLedgers emp in coll)
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
				return SubLedgersMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubLedgersQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SubLedgers(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SubLedgers();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SubLedgersQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubLedgersQuery();
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
		public bool Load(SubLedgersQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SubLedgers AddNew()
		{
			SubLedgers entity = base.AddNewEntity() as SubLedgers;

			return entity;
		}
		public SubLedgers FindByPrimaryKey(Int32 subLedgerId)
		{
			return base.FindByPrimaryKey(subLedgerId) as SubLedgers;
		}

		#region IEnumerable< SubLedgers> Members

		IEnumerator<SubLedgers> IEnumerable<SubLedgers>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SubLedgers;
			}
		}

		#endregion

		private SubLedgersQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SubLedgers' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SubLedgers ({SubLedgerId})")]
	[Serializable]
	public partial class SubLedgers : esSubLedgers
	{
		public SubLedgers()
		{
		}

		public SubLedgers(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SubLedgersMetadata.Meta();
			}
		}

		override protected esSubLedgersQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubLedgersQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SubLedgersQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubLedgersQuery();
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
		public bool Load(SubLedgersQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SubLedgersQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SubLedgersQuery : esSubLedgersQuery
	{
		public SubLedgersQuery()
		{

		}

		public SubLedgersQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SubLedgersQuery";
		}
	}

	[Serializable]
	public partial class SubLedgersMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SubLedgersMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.SubLedgerId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgersMetadata.PropertyNames.SubLedgerId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.GroupId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgersMetadata.PropertyNames.GroupId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.SubLedgerName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgersMetadata.PropertyNames.SubLedgerName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgersMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.DateCreated, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SubLedgersMetadata.PropertyNames.DateCreated;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SubLedgersMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.CreatedBy, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgersMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 25;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgersMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 25;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.TempID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgersMetadata.PropertyNames.TempID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.HoSubLedgerId, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubLedgersMetadata.PropertyNames.HoSubLedgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.HoDescription, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = SubLedgersMetadata.PropertyNames.HoDescription;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SubLedgersMetadata.ColumnNames.IsDirectCost, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SubLedgersMetadata.PropertyNames.IsDirectCost;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SubLedgersMetadata Meta()
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
			public const string SubLedgerId = "SubLedgerId";
			public const string GroupId = "GroupId";
			public const string SubLedgerName = "SubLedgerName";
			public const string Description = "Description";
			public const string DateCreated = "DateCreated";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string CreatedBy = "CreatedBy";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string TempID = "tempID";
			public const string HoSubLedgerId = "HoSubLedgerId";
			public const string HoDescription = "HoDescription";
			public const string IsDirectCost = "IsDirectCost";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SubLedgerId = "SubLedgerId";
			public const string GroupId = "GroupId";
			public const string SubLedgerName = "SubLedgerName";
			public const string Description = "Description";
			public const string DateCreated = "DateCreated";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string CreatedBy = "CreatedBy";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string TempID = "TempID";
			public const string HoSubLedgerId = "HoSubLedgerId";
			public const string HoDescription = "HoDescription";
			public const string IsDirectCost = "IsDirectCost";
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
			lock (typeof(SubLedgersMetadata))
			{
				if (SubLedgersMetadata.mapDelegates == null)
				{
					SubLedgersMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SubLedgersMetadata.meta == null)
				{
					SubLedgersMetadata.meta = new SubLedgersMetadata();
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

				meta.AddTypeMap("SubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("GroupId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubLedgerName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DateCreated", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TempID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("HoSubLedgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("HoDescription", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IsDirectCost", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "SubLedgers";
				meta.Destination = "SubLedgers";
				meta.spInsert = "proc_SubLedgersInsert";
				meta.spUpdate = "proc_SubLedgersUpdate";
				meta.spDelete = "proc_SubLedgersDelete";
				meta.spLoadAll = "proc_SubLedgersLoadAll";
				meta.spLoadByPrimaryKey = "proc_SubLedgersLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SubLedgersMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
