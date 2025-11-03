/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/14/2021 8:45:17 PM
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
	abstract public class esLaundryReturnedCollection : esEntityCollectionWAuditLog
	{
		public esLaundryReturnedCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryReturnedCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryReturnedQuery query)
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
			this.InitQuery(query as esLaundryReturnedQuery);
		}
		#endregion

		virtual public LaundryReturned DetachEntity(LaundryReturned entity)
		{
			return base.DetachEntity(entity) as LaundryReturned;
		}

		virtual public LaundryReturned AttachEntity(LaundryReturned entity)
		{
			return base.AttachEntity(entity) as LaundryReturned;
		}

		virtual public void Combine(LaundryReturnedCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryReturned this[int index]
		{
			get
			{
				return base[index] as LaundryReturned;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryReturned);
		}
	}

	[Serializable]
	abstract public class esLaundryReturned : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryReturnedQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryReturned()
		{
		}

		public esLaundryReturned(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String returnNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(returnNo);
			else
				return LoadByPrimaryKeyStoredProcedure(returnNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String returnNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(returnNo);
			else
				return LoadByPrimaryKeyStoredProcedure(returnNo);
		}

		private bool LoadByPrimaryKeyDynamic(String returnNo)
		{
			esLaundryReturnedQuery query = this.GetDynamicQuery();
			query.Where(query.ReturnNo == returnNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String returnNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ReturnNo", returnNo);
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
						case "ReturnNo": this.str.ReturnNo = (string)value; break;
						case "ReturnDate": this.str.ReturnDate = (string)value; break;
						case "ReturnTime": this.str.ReturnTime = (string)value; break;
						case "ToServiceUnitID": this.str.ToServiceUnitID = (string)value; break;
						case "HandedByUserID": this.str.HandedByUserID = (string)value; break;
						case "ReceivedBy": this.str.ReceivedBy = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReturnDate":

							if (value == null || value is System.DateTime)
								this.ReturnDate = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDateTime":

							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
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
		/// Maps to LaundryReturned.ReturnNo
		/// </summary>
		virtual public System.String ReturnNo
		{
			get
			{
				return base.GetSystemString(LaundryReturnedMetadata.ColumnNames.ReturnNo);
			}

			set
			{
				base.SetSystemString(LaundryReturnedMetadata.ColumnNames.ReturnNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.ReturnDate
		/// </summary>
		virtual public System.DateTime? ReturnDate
		{
			get
			{
				return base.GetSystemDateTime(LaundryReturnedMetadata.ColumnNames.ReturnDate);
			}

			set
			{
				base.SetSystemDateTime(LaundryReturnedMetadata.ColumnNames.ReturnDate, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.ReturnTime
		/// </summary>
		virtual public System.String ReturnTime
		{
			get
			{
				return base.GetSystemString(LaundryReturnedMetadata.ColumnNames.ReturnTime);
			}

			set
			{
				base.SetSystemString(LaundryReturnedMetadata.ColumnNames.ReturnTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(LaundryReturnedMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(LaundryReturnedMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.HandedByUserID
		/// </summary>
		virtual public System.String HandedByUserID
		{
			get
			{
				return base.GetSystemString(LaundryReturnedMetadata.ColumnNames.HandedByUserID);
			}

			set
			{
				base.SetSystemString(LaundryReturnedMetadata.ColumnNames.HandedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.ReceivedBy
		/// </summary>
		virtual public System.String ReceivedBy
		{
			get
			{
				return base.GetSystemString(LaundryReturnedMetadata.ColumnNames.ReceivedBy);
			}

			set
			{
				base.SetSystemString(LaundryReturnedMetadata.ColumnNames.ReceivedBy, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(LaundryReturnedMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(LaundryReturnedMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryReturnedMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryReturnedMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(LaundryReturnedMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(LaundryReturnedMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(LaundryReturnedMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(LaundryReturnedMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryReturnedMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryReturnedMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(LaundryReturnedMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(LaundryReturnedMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryReturnedMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryReturnedMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryReturned.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryReturnedMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryReturnedMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryReturned entity)
			{
				this.entity = entity;
			}
			public System.String ReturnNo
			{
				get
				{
					System.String data = entity.ReturnNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnNo = null;
					else entity.ReturnNo = Convert.ToString(value);
				}
			}
			public System.String ReturnDate
			{
				get
				{
					System.DateTime? data = entity.ReturnDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnDate = null;
					else entity.ReturnDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReturnTime
			{
				get
				{
					System.String data = entity.ReturnTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReturnTime = null;
					else entity.ReturnTime = Convert.ToString(value);
				}
			}
			public System.String ToServiceUnitID
			{
				get
				{
					System.String data = entity.ToServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToServiceUnitID = null;
					else entity.ToServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String HandedByUserID
			{
				get
				{
					System.String data = entity.HandedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HandedByUserID = null;
					else entity.HandedByUserID = Convert.ToString(value);
				}
			}
			public System.String ReceivedBy
			{
				get
				{
					System.String data = entity.ReceivedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedBy = null;
					else entity.ReceivedBy = Convert.ToString(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
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
			private esLaundryReturned entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryReturnedQuery query)
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
				throw new Exception("esLaundryReturned can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryReturned : esLaundryReturned
	{
	}

	[Serializable]
	abstract public class esLaundryReturnedQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryReturnedMetadata.Meta();
			}
		}

		public esQueryItem ReturnNo
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.ReturnNo, esSystemType.String);
			}
		}

		public esQueryItem ReturnDate
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.ReturnDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReturnTime
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.ReturnTime, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem HandedByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.HandedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReceivedBy
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.ReceivedBy, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryReturnedMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryReturnedCollection")]
	public partial class LaundryReturnedCollection : esLaundryReturnedCollection, IEnumerable<LaundryReturned>
	{
		public LaundryReturnedCollection()
		{

		}

		public static implicit operator List<LaundryReturned>(LaundryReturnedCollection coll)
		{
			List<LaundryReturned> list = new List<LaundryReturned>();

			foreach (LaundryReturned emp in coll)
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
				return LaundryReturnedMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryReturnedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryReturned(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryReturned();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryReturnedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryReturnedQuery();
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
		public bool Load(LaundryReturnedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryReturned AddNew()
		{
			LaundryReturned entity = base.AddNewEntity() as LaundryReturned;

			return entity;
		}
		public LaundryReturned FindByPrimaryKey(String returnNo)
		{
			return base.FindByPrimaryKey(returnNo) as LaundryReturned;
		}

		#region IEnumerable< LaundryReturned> Members

		IEnumerator<LaundryReturned> IEnumerable<LaundryReturned>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryReturned;
			}
		}

		#endregion

		private LaundryReturnedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryReturned' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryReturned ({ReturnNo})")]
	[Serializable]
	public partial class LaundryReturned : esLaundryReturned
	{
		public LaundryReturned()
		{
		}

		public LaundryReturned(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryReturnedMetadata.Meta();
			}
		}

		override protected esLaundryReturnedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryReturnedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryReturnedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryReturnedQuery();
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
		public bool Load(LaundryReturnedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryReturnedQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryReturnedQuery : esLaundryReturnedQuery
	{
		public LaundryReturnedQuery()
		{

		}

		public LaundryReturnedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryReturnedQuery";
		}
	}

	[Serializable]
	public partial class LaundryReturnedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryReturnedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.ReturnNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.ReturnNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.ReturnDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.ReturnDate;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.ReturnTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.ReturnTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.ToServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.HandedByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.HandedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.ReceivedBy, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.ReceivedBy;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.ApprovedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.ApprovedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.VoidDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryReturnedMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryReturnedMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryReturnedMetadata Meta()
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
			public const string ReturnNo = "ReturnNo";
			public const string ReturnDate = "ReturnDate";
			public const string ReturnTime = "ReturnTime";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string HandedByUserID = "HandedByUserID";
			public const string ReceivedBy = "ReceivedBy";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ReturnNo = "ReturnNo";
			public const string ReturnDate = "ReturnDate";
			public const string ReturnTime = "ReturnTime";
			public const string ToServiceUnitID = "ToServiceUnitID";
			public const string HandedByUserID = "HandedByUserID";
			public const string ReceivedBy = "ReceivedBy";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
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
			lock (typeof(LaundryReturnedMetadata))
			{
				if (LaundryReturnedMetadata.mapDelegates == null)
				{
					LaundryReturnedMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryReturnedMetadata.meta == null)
				{
					LaundryReturnedMetadata.meta = new LaundryReturnedMetadata();
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

				meta.AddTypeMap("ReturnNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReturnDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReturnTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HandedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundryReturned";
				meta.Destination = "LaundryReturned";
				meta.spInsert = "proc_LaundryReturnedInsert";
				meta.spUpdate = "proc_LaundryReturnedUpdate";
				meta.spDelete = "proc_LaundryReturnedDelete";
				meta.spLoadAll = "proc_LaundryReturnedLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryReturnedLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryReturnedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
