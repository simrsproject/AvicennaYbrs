/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/11/2022 3:44:02 PM
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
	abstract public class esEmployeeWageStructureAndScaleCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeWageStructureAndScaleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeWageStructureAndScaleCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeWageStructureAndScaleQuery query)
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
			this.InitQuery(query as esEmployeeWageStructureAndScaleQuery);
		}
		#endregion

		virtual public EmployeeWageStructureAndScale DetachEntity(EmployeeWageStructureAndScale entity)
		{
			return base.DetachEntity(entity) as EmployeeWageStructureAndScale;
		}

		virtual public EmployeeWageStructureAndScale AttachEntity(EmployeeWageStructureAndScale entity)
		{
			return base.AttachEntity(entity) as EmployeeWageStructureAndScale;
		}

		virtual public void Combine(EmployeeWageStructureAndScaleCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeWageStructureAndScale this[int index]
		{
			get
			{
				return base[index] as EmployeeWageStructureAndScale;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeWageStructureAndScale);
		}
	}

	[Serializable]
	abstract public class esEmployeeWageStructureAndScale : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeWageStructureAndScaleQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeWageStructureAndScale()
		{
		}

		public esEmployeeWageStructureAndScale(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeWageStructureAndScaleID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeWageStructureAndScaleID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeWageStructureAndScaleID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeWageStructureAndScaleID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeWageStructureAndScaleID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeWageStructureAndScaleID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeWageStructureAndScaleID)
		{
			esEmployeeWageStructureAndScaleQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeWageStructureAndScaleID == employeeWageStructureAndScaleID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeWageStructureAndScaleID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeWageStructureAndScaleID", employeeWageStructureAndScaleID);
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
						case "EmployeeWageStructureAndScaleID": this.str.EmployeeWageStructureAndScaleID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "WageStructureAndScalePositionID": this.str.WageStructureAndScalePositionID = (string)value; break;
						case "Points": this.str.Points = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeWageStructureAndScaleID":

							if (value == null || value is System.Int32)
								this.EmployeeWageStructureAndScaleID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "WageStructureAndScalePositionID":

							if (value == null || value is System.Int32)
								this.WageStructureAndScalePositionID = (System.Int32?)value;
							break;
						case "Points":

							if (value == null || value is System.Decimal)
								this.Points = (System.Decimal?)value;
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
		/// Maps to EmployeeWageStructureAndScale.EmployeeWageStructureAndScaleID
		/// </summary>
		virtual public System.Int32? EmployeeWageStructureAndScaleID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScaleMetadata.ColumnNames.EmployeeWageStructureAndScaleID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScaleMetadata.ColumnNames.EmployeeWageStructureAndScaleID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScale.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScaleMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScaleMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScale.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWageStructureAndScaleMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWageStructureAndScaleMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScale.WageStructureAndScalePositionID
		/// </summary>
		virtual public System.Int32? WageStructureAndScalePositionID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWageStructureAndScaleMetadata.ColumnNames.WageStructureAndScalePositionID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWageStructureAndScaleMetadata.ColumnNames.WageStructureAndScalePositionID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScale.Points
		/// </summary>
		virtual public System.Decimal? Points
		{
			get
			{
				return base.GetSystemDecimal(EmployeeWageStructureAndScaleMetadata.ColumnNames.Points);
			}

			set
			{
				base.SetSystemDecimal(EmployeeWageStructureAndScaleMetadata.ColumnNames.Points, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScale.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWageStructureAndScaleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWageStructureAndScaleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWageStructureAndScale.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeWageStructureAndScaleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeWageStructureAndScaleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeWageStructureAndScale entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeWageStructureAndScaleID
			{
				get
				{
					System.Int32? data = entity.EmployeeWageStructureAndScaleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeWageStructureAndScaleID = null;
					else entity.EmployeeWageStructureAndScaleID = Convert.ToInt32(value);
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
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
			public System.String WageStructureAndScalePositionID
			{
				get
				{
					System.Int32? data = entity.WageStructureAndScalePositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageStructureAndScalePositionID = null;
					else entity.WageStructureAndScalePositionID = Convert.ToInt32(value);
				}
			}
			public System.String Points
			{
				get
				{
					System.Decimal? data = entity.Points;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Points = null;
					else entity.Points = Convert.ToDecimal(value);
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
			private esEmployeeWageStructureAndScale entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeWageStructureAndScaleQuery query)
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
				throw new Exception("esEmployeeWageStructureAndScale can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeWageStructureAndScale : esEmployeeWageStructureAndScale
	{
	}

	[Serializable]
	abstract public class esEmployeeWageStructureAndScaleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeWageStructureAndScaleMetadata.Meta();
			}
		}

		public esQueryItem EmployeeWageStructureAndScaleID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScaleMetadata.ColumnNames.EmployeeWageStructureAndScaleID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScaleMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScaleMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem WageStructureAndScalePositionID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScaleMetadata.ColumnNames.WageStructureAndScalePositionID, esSystemType.Int32);
			}
		}

		public esQueryItem Points
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScaleMetadata.ColumnNames.Points, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScaleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeWageStructureAndScaleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeWageStructureAndScaleCollection")]
	public partial class EmployeeWageStructureAndScaleCollection : esEmployeeWageStructureAndScaleCollection, IEnumerable<EmployeeWageStructureAndScale>
	{
		public EmployeeWageStructureAndScaleCollection()
		{

		}

		public static implicit operator List<EmployeeWageStructureAndScale>(EmployeeWageStructureAndScaleCollection coll)
		{
			List<EmployeeWageStructureAndScale> list = new List<EmployeeWageStructureAndScale>();

			foreach (EmployeeWageStructureAndScale emp in coll)
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
				return EmployeeWageStructureAndScaleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeWageStructureAndScaleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeWageStructureAndScale(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeWageStructureAndScale();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeWageStructureAndScaleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeWageStructureAndScaleQuery();
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
		public bool Load(EmployeeWageStructureAndScaleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeWageStructureAndScale AddNew()
		{
			EmployeeWageStructureAndScale entity = base.AddNewEntity() as EmployeeWageStructureAndScale;

			return entity;
		}
		public EmployeeWageStructureAndScale FindByPrimaryKey(Int32 employeeWageStructureAndScaleID)
		{
			return base.FindByPrimaryKey(employeeWageStructureAndScaleID) as EmployeeWageStructureAndScale;
		}

		#region IEnumerable< EmployeeWageStructureAndScale> Members

		IEnumerator<EmployeeWageStructureAndScale> IEnumerable<EmployeeWageStructureAndScale>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeWageStructureAndScale;
			}
		}

		#endregion

		private EmployeeWageStructureAndScaleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeWageStructureAndScale' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeWageStructureAndScale ({EmployeeWageStructureAndScaleID})")]
	[Serializable]
	public partial class EmployeeWageStructureAndScale : esEmployeeWageStructureAndScale
	{
		public EmployeeWageStructureAndScale()
		{
		}

		public EmployeeWageStructureAndScale(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeWageStructureAndScaleMetadata.Meta();
			}
		}

		override protected esEmployeeWageStructureAndScaleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeWageStructureAndScaleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeWageStructureAndScaleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeWageStructureAndScaleQuery();
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
		public bool Load(EmployeeWageStructureAndScaleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeWageStructureAndScaleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeWageStructureAndScaleQuery : esEmployeeWageStructureAndScaleQuery
	{
		public EmployeeWageStructureAndScaleQuery()
		{

		}

		public EmployeeWageStructureAndScaleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeWageStructureAndScaleQuery";
		}
	}

	[Serializable]
	public partial class EmployeeWageStructureAndScaleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeWageStructureAndScaleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeWageStructureAndScaleMetadata.ColumnNames.EmployeeWageStructureAndScaleID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScaleMetadata.PropertyNames.EmployeeWageStructureAndScaleID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScaleMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScaleMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScaleMetadata.ColumnNames.ValidFrom, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWageStructureAndScaleMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScaleMetadata.ColumnNames.WageStructureAndScalePositionID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWageStructureAndScaleMetadata.PropertyNames.WageStructureAndScalePositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScaleMetadata.ColumnNames.Points, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeWageStructureAndScaleMetadata.PropertyNames.Points;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScaleMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWageStructureAndScaleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWageStructureAndScaleMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWageStructureAndScaleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeWageStructureAndScaleMetadata Meta()
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
			public const string EmployeeWageStructureAndScaleID = "EmployeeWageStructureAndScaleID";
			public const string PersonID = "PersonID";
			public const string ValidFrom = "ValidFrom";
			public const string WageStructureAndScalePositionID = "WageStructureAndScalePositionID";
			public const string Points = "Points";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeWageStructureAndScaleID = "EmployeeWageStructureAndScaleID";
			public const string PersonID = "PersonID";
			public const string ValidFrom = "ValidFrom";
			public const string WageStructureAndScalePositionID = "WageStructureAndScalePositionID";
			public const string Points = "Points";
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
			lock (typeof(EmployeeWageStructureAndScaleMetadata))
			{
				if (EmployeeWageStructureAndScaleMetadata.mapDelegates == null)
				{
					EmployeeWageStructureAndScaleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeWageStructureAndScaleMetadata.meta == null)
				{
					EmployeeWageStructureAndScaleMetadata.meta = new EmployeeWageStructureAndScaleMetadata();
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

				meta.AddTypeMap("EmployeeWageStructureAndScaleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("WageStructureAndScalePositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Points", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeWageStructureAndScale";
				meta.Destination = "EmployeeWageStructureAndScale";
				meta.spInsert = "proc_EmployeeWageStructureAndScaleInsert";
				meta.spUpdate = "proc_EmployeeWageStructureAndScaleUpdate";
				meta.spDelete = "proc_EmployeeWageStructureAndScaleDelete";
				meta.spLoadAll = "proc_EmployeeWageStructureAndScaleLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeWageStructureAndScaleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeWageStructureAndScaleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
