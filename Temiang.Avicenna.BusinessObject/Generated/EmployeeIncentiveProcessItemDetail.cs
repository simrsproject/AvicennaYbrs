/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/23/2022 5:57:37 PM
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
	abstract public class esEmployeeIncentiveProcessItemDetailCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeIncentiveProcessItemDetailCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeIncentiveProcessItemDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeIncentiveProcessItemDetailQuery query)
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
			this.InitQuery(query as esEmployeeIncentiveProcessItemDetailQuery);
		}
		#endregion

		virtual public EmployeeIncentiveProcessItemDetail DetachEntity(EmployeeIncentiveProcessItemDetail entity)
		{
			return base.DetachEntity(entity) as EmployeeIncentiveProcessItemDetail;
		}

		virtual public EmployeeIncentiveProcessItemDetail AttachEntity(EmployeeIncentiveProcessItemDetail entity)
		{
			return base.AttachEntity(entity) as EmployeeIncentiveProcessItemDetail;
		}

		virtual public void Combine(EmployeeIncentiveProcessItemDetailCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeIncentiveProcessItemDetail this[int index]
		{
			get
			{
				return base[index] as EmployeeIncentiveProcessItemDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeIncentiveProcessItemDetail);
		}
	}

	[Serializable]
	abstract public class esEmployeeIncentiveProcessItemDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeIncentiveProcessItemDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeIncentiveProcessItemDetail()
		{
		}

		public esEmployeeIncentiveProcessItemDetail(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup, Int32 personID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup, personID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup, personID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup, Int32 personID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup, personID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup, personID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup, Int32 personID)
		{
			esEmployeeIncentiveProcessItemDetailQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeIncentiveProcessID == employeeIncentiveProcessID, query.SRIncentiveServiceUnitGroup == sRIncentiveServiceUnitGroup, query.PersonID == personID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup, Int32 personID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeIncentiveProcessID", employeeIncentiveProcessID);
			parms.Add("SRIncentiveServiceUnitGroup", sRIncentiveServiceUnitGroup);
			parms.Add("PersonID", personID);
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
						case "EmployeeIncentiveProcessID": this.str.EmployeeIncentiveProcessID = (string)value; break;
						case "SRIncentiveServiceUnitGroup": this.str.SRIncentiveServiceUnitGroup = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "Points": this.str.Points = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeIncentiveProcessID":

							if (value == null || value is System.Int32)
								this.EmployeeIncentiveProcessID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
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
		/// Maps to EmployeeIncentiveProcessItemDetail.EmployeeIncentiveProcessID
		/// </summary>
		virtual public System.Int32? EmployeeIncentiveProcessID
		{
			get
			{
				return base.GetSystemInt32(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.EmployeeIncentiveProcessID);
			}

			set
			{
				base.SetSystemInt32(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.EmployeeIncentiveProcessID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItemDetail.SRIncentiveServiceUnitGroup
		/// </summary>
		virtual public System.String SRIncentiveServiceUnitGroup
		{
			get
			{
				return base.GetSystemString(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.SRIncentiveServiceUnitGroup);
			}

			set
			{
				base.SetSystemString(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.SRIncentiveServiceUnitGroup, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItemDetail.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItemDetail.Points
		/// </summary>
		virtual public System.Decimal? Points
		{
			get
			{
				return base.GetSystemDecimal(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.Points);
			}

			set
			{
				base.SetSystemDecimal(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.Points, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItemDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeIncentiveProcessItemDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeIncentiveProcessItemDetail entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeIncentiveProcessID
			{
				get
				{
					System.Int32? data = entity.EmployeeIncentiveProcessID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeIncentiveProcessID = null;
					else entity.EmployeeIncentiveProcessID = Convert.ToInt32(value);
				}
			}
			public System.String SRIncentiveServiceUnitGroup
			{
				get
				{
					System.String data = entity.SRIncentiveServiceUnitGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncentiveServiceUnitGroup = null;
					else entity.SRIncentiveServiceUnitGroup = Convert.ToString(value);
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
			private esEmployeeIncentiveProcessItemDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeIncentiveProcessItemDetailQuery query)
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
				throw new Exception("esEmployeeIncentiveProcessItemDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeIncentiveProcessItemDetail : esEmployeeIncentiveProcessItemDetail
	{
	}

	[Serializable]
	abstract public class esEmployeeIncentiveProcessItemDetailQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeIncentiveProcessItemDetailMetadata.Meta();
			}
		}

		public esQueryItem EmployeeIncentiveProcessID
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.EmployeeIncentiveProcessID, esSystemType.Int32);
			}
		}

		public esQueryItem SRIncentiveServiceUnitGroup
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.SRIncentiveServiceUnitGroup, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem Points
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.Points, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeIncentiveProcessItemDetailCollection")]
	public partial class EmployeeIncentiveProcessItemDetailCollection : esEmployeeIncentiveProcessItemDetailCollection, IEnumerable<EmployeeIncentiveProcessItemDetail>
	{
		public EmployeeIncentiveProcessItemDetailCollection()
		{

		}

		public static implicit operator List<EmployeeIncentiveProcessItemDetail>(EmployeeIncentiveProcessItemDetailCollection coll)
		{
			List<EmployeeIncentiveProcessItemDetail> list = new List<EmployeeIncentiveProcessItemDetail>();

			foreach (EmployeeIncentiveProcessItemDetail emp in coll)
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
				return EmployeeIncentiveProcessItemDetailMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeIncentiveProcessItemDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeIncentiveProcessItemDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeIncentiveProcessItemDetail();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeIncentiveProcessItemDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeIncentiveProcessItemDetailQuery();
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
		public bool Load(EmployeeIncentiveProcessItemDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeIncentiveProcessItemDetail AddNew()
		{
			EmployeeIncentiveProcessItemDetail entity = base.AddNewEntity() as EmployeeIncentiveProcessItemDetail;

			return entity;
		}
		public EmployeeIncentiveProcessItemDetail FindByPrimaryKey(Int32 employeeIncentiveProcessID, String sRIncentiveServiceUnitGroup, Int32 personID)
		{
			return base.FindByPrimaryKey(employeeIncentiveProcessID, sRIncentiveServiceUnitGroup, personID) as EmployeeIncentiveProcessItemDetail;
		}

		#region IEnumerable< EmployeeIncentiveProcessItemDetail> Members

		IEnumerator<EmployeeIncentiveProcessItemDetail> IEnumerable<EmployeeIncentiveProcessItemDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeIncentiveProcessItemDetail;
			}
		}

		#endregion

		private EmployeeIncentiveProcessItemDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeIncentiveProcessItemDetail' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeIncentiveProcessItemDetail ({EmployeeIncentiveProcessID, SRIncentiveServiceUnitGroup, PersonID})")]
	[Serializable]
	public partial class EmployeeIncentiveProcessItemDetail : esEmployeeIncentiveProcessItemDetail
	{
		public EmployeeIncentiveProcessItemDetail()
		{
		}

		public EmployeeIncentiveProcessItemDetail(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeIncentiveProcessItemDetailMetadata.Meta();
			}
		}

		override protected esEmployeeIncentiveProcessItemDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeIncentiveProcessItemDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeIncentiveProcessItemDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeIncentiveProcessItemDetailQuery();
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
		public bool Load(EmployeeIncentiveProcessItemDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeIncentiveProcessItemDetailQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeIncentiveProcessItemDetailQuery : esEmployeeIncentiveProcessItemDetailQuery
	{
		public EmployeeIncentiveProcessItemDetailQuery()
		{

		}

		public EmployeeIncentiveProcessItemDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeIncentiveProcessItemDetailQuery";
		}
	}

	[Serializable]
	public partial class EmployeeIncentiveProcessItemDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeIncentiveProcessItemDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.EmployeeIncentiveProcessID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeIncentiveProcessItemDetailMetadata.PropertyNames.EmployeeIncentiveProcessID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.SRIncentiveServiceUnitGroup, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeIncentiveProcessItemDetailMetadata.PropertyNames.SRIncentiveServiceUnitGroup;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeIncentiveProcessItemDetailMetadata.PropertyNames.PersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.Points, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeIncentiveProcessItemDetailMetadata.PropertyNames.Points;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeIncentiveProcessItemDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeIncentiveProcessItemDetailMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeIncentiveProcessItemDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeIncentiveProcessItemDetailMetadata Meta()
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
			public const string EmployeeIncentiveProcessID = "EmployeeIncentiveProcessID";
			public const string SRIncentiveServiceUnitGroup = "SRIncentiveServiceUnitGroup";
			public const string PersonID = "PersonID";
			public const string Points = "Points";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeIncentiveProcessID = "EmployeeIncentiveProcessID";
			public const string SRIncentiveServiceUnitGroup = "SRIncentiveServiceUnitGroup";
			public const string PersonID = "PersonID";
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
			lock (typeof(EmployeeIncentiveProcessItemDetailMetadata))
			{
				if (EmployeeIncentiveProcessItemDetailMetadata.mapDelegates == null)
				{
					EmployeeIncentiveProcessItemDetailMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeIncentiveProcessItemDetailMetadata.meta == null)
				{
					EmployeeIncentiveProcessItemDetailMetadata.meta = new EmployeeIncentiveProcessItemDetailMetadata();
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

				meta.AddTypeMap("EmployeeIncentiveProcessID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRIncentiveServiceUnitGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Points", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeIncentiveProcessItemDetail";
				meta.Destination = "EmployeeIncentiveProcessItemDetail";
				meta.spInsert = "proc_EmployeeIncentiveProcessItemDetailInsert";
				meta.spUpdate = "proc_EmployeeIncentiveProcessItemDetailUpdate";
				meta.spDelete = "proc_EmployeeIncentiveProcessItemDetailDelete";
				meta.spLoadAll = "proc_EmployeeIncentiveProcessItemDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeIncentiveProcessItemDetailLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeIncentiveProcessItemDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
