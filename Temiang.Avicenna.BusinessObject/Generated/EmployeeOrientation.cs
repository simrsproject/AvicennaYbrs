/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/10/2021 1:31:29 PM
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
	abstract public class esEmployeeOrientationCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeOrientationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeOrientationCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeOrientationQuery query)
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
			this.InitQuery(query as esEmployeeOrientationQuery);
		}
		#endregion

		virtual public EmployeeOrientation DetachEntity(EmployeeOrientation entity)
		{
			return base.DetachEntity(entity) as EmployeeOrientation;
		}

		virtual public EmployeeOrientation AttachEntity(EmployeeOrientation entity)
		{
			return base.AttachEntity(entity) as EmployeeOrientation;
		}

		virtual public void Combine(EmployeeOrientationCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeOrientation this[int index]
		{
			get
			{
				return base[index] as EmployeeOrientation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeOrientation);
		}
	}

	[Serializable]
	abstract public class esEmployeeOrientation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeOrientationQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeOrientation()
		{
		}

		public esEmployeeOrientation(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeOrientationID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeOrientationID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeOrientationID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeOrientationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeOrientationID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeOrientationID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeOrientationID)
		{
			esEmployeeOrientationQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeOrientationID == employeeOrientationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeOrientationID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeOrientationID", employeeOrientationID);
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
						case "EmployeeOrientationID": this.str.EmployeeOrientationID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "IsGeneral": this.str.IsGeneral = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "DurationHour": this.str.DurationHour = (string)value; break;
						case "DurationMinutes": this.str.DurationMinutes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeOrientationID":

							if (value == null || value is System.Int32)
								this.EmployeeOrientationID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "IsGeneral":

							if (value == null || value is System.Boolean)
								this.IsGeneral = (System.Boolean?)value;
							break;
						case "StartDate":

							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":

							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						case "DurationHour":

							if (value == null || value is System.Decimal)
								this.DurationHour = (System.Decimal?)value;
							break;
						case "DurationMinutes":

							if (value == null || value is System.Decimal)
								this.DurationMinutes = (System.Decimal?)value;
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
		/// Maps to EmployeeOrientation.EmployeeOrientationID
		/// </summary>
		virtual public System.Int32? EmployeeOrientationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOrientationMetadata.ColumnNames.EmployeeOrientationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeOrientationMetadata.ColumnNames.EmployeeOrientationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOrientation.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeOrientationMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeOrientationMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOrientation.IsGeneral
		/// </summary>
		virtual public System.Boolean? IsGeneral
		{
			get
			{
				return base.GetSystemBoolean(EmployeeOrientationMetadata.ColumnNames.IsGeneral);
			}

			set
			{
				base.SetSystemBoolean(EmployeeOrientationMetadata.ColumnNames.IsGeneral, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOrientation.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOrientationMetadata.ColumnNames.StartDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOrientationMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOrientation.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOrientationMetadata.ColumnNames.EndDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOrientationMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOrientation.DurationHour
		/// </summary>
		virtual public System.Decimal? DurationHour
		{
			get
			{
				return base.GetSystemDecimal(EmployeeOrientationMetadata.ColumnNames.DurationHour);
			}

			set
			{
				base.SetSystemDecimal(EmployeeOrientationMetadata.ColumnNames.DurationHour, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOrientation.DurationMinutes
		/// </summary>
		virtual public System.Decimal? DurationMinutes
		{
			get
			{
				return base.GetSystemDecimal(EmployeeOrientationMetadata.ColumnNames.DurationMinutes);
			}

			set
			{
				base.SetSystemDecimal(EmployeeOrientationMetadata.ColumnNames.DurationMinutes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOrientation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeOrientationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeOrientationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeOrientation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeOrientationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeOrientationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeOrientation entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeOrientationID
			{
				get
				{
					System.Int32? data = entity.EmployeeOrientationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeOrientationID = null;
					else entity.EmployeeOrientationID = Convert.ToInt32(value);
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
			public System.String IsGeneral
			{
				get
				{
					System.Boolean? data = entity.IsGeneral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGeneral = null;
					else entity.IsGeneral = Convert.ToBoolean(value);
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
			public System.String DurationHour
			{
				get
				{
					System.Decimal? data = entity.DurationHour;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DurationHour = null;
					else entity.DurationHour = Convert.ToDecimal(value);
				}
			}
			public System.String DurationMinutes
			{
				get
				{
					System.Decimal? data = entity.DurationMinutes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DurationMinutes = null;
					else entity.DurationMinutes = Convert.ToDecimal(value);
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
			private esEmployeeOrientation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeOrientationQuery query)
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
				throw new Exception("esEmployeeOrientation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeOrientation : esEmployeeOrientation
	{
	}

	[Serializable]
	abstract public class esEmployeeOrientationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeOrientationMetadata.Meta();
			}
		}

		public esQueryItem EmployeeOrientationID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.EmployeeOrientationID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem IsGeneral
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.IsGeneral, esSystemType.Boolean);
			}
		}

		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DurationHour
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.DurationHour, esSystemType.Decimal);
			}
		}

		public esQueryItem DurationMinutes
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.DurationMinutes, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeOrientationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeOrientationCollection")]
	public partial class EmployeeOrientationCollection : esEmployeeOrientationCollection, IEnumerable<EmployeeOrientation>
	{
		public EmployeeOrientationCollection()
		{

		}

		public static implicit operator List<EmployeeOrientation>(EmployeeOrientationCollection coll)
		{
			List<EmployeeOrientation> list = new List<EmployeeOrientation>();

			foreach (EmployeeOrientation emp in coll)
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
				return EmployeeOrientationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeOrientationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeOrientation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeOrientation();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeOrientationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeOrientationQuery();
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
		public bool Load(EmployeeOrientationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeOrientation AddNew()
		{
			EmployeeOrientation entity = base.AddNewEntity() as EmployeeOrientation;

			return entity;
		}
		public EmployeeOrientation FindByPrimaryKey(Int32 employeeOrientationID)
		{
			return base.FindByPrimaryKey(employeeOrientationID) as EmployeeOrientation;
		}

		#region IEnumerable< EmployeeOrientation> Members

		IEnumerator<EmployeeOrientation> IEnumerable<EmployeeOrientation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeOrientation;
			}
		}

		#endregion

		private EmployeeOrientationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeOrientation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeOrientation ({EmployeeOrientationID})")]
	[Serializable]
	public partial class EmployeeOrientation : esEmployeeOrientation
	{
		public EmployeeOrientation()
		{
		}

		public EmployeeOrientation(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeOrientationMetadata.Meta();
			}
		}

		override protected esEmployeeOrientationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeOrientationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeOrientationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeOrientationQuery();
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
		public bool Load(EmployeeOrientationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeOrientationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeOrientationQuery : esEmployeeOrientationQuery
	{
		public EmployeeOrientationQuery()
		{

		}

		public EmployeeOrientationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeOrientationQuery";
		}
	}

	[Serializable]
	public partial class EmployeeOrientationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeOrientationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.EmployeeOrientationID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.EmployeeOrientationID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.IsGeneral, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.IsGeneral;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.StartDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.EndDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.DurationHour, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.DurationHour;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.DurationMinutes, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.DurationMinutes;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeOrientationMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeOrientationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeOrientationMetadata Meta()
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
			public const string EmployeeOrientationID = "EmployeeOrientationID";
			public const string PersonID = "PersonID";
			public const string IsGeneral = "IsGeneral";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string DurationHour = "DurationHour";
			public const string DurationMinutes = "DurationMinutes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeOrientationID = "EmployeeOrientationID";
			public const string PersonID = "PersonID";
			public const string IsGeneral = "IsGeneral";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string DurationHour = "DurationHour";
			public const string DurationMinutes = "DurationMinutes";
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
			lock (typeof(EmployeeOrientationMetadata))
			{
				if (EmployeeOrientationMetadata.mapDelegates == null)
				{
					EmployeeOrientationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeOrientationMetadata.meta == null)
				{
					EmployeeOrientationMetadata.meta = new EmployeeOrientationMetadata();
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

				meta.AddTypeMap("EmployeeOrientationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsGeneral", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DurationHour", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DurationMinutes", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeOrientation";
				meta.Destination = "EmployeeOrientation";
				meta.spInsert = "proc_EmployeeOrientationInsert";
				meta.spUpdate = "proc_EmployeeOrientationUpdate";
				meta.spDelete = "proc_EmployeeOrientationDelete";
				meta.spLoadAll = "proc_EmployeeOrientationLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeOrientationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeOrientationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
