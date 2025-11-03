/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/14/2021 2:17:38 PM
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
	abstract public class esLaundryDistributionCollection : esEntityCollectionWAuditLog
	{
		public esLaundryDistributionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryDistributionCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryDistributionQuery query)
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
			this.InitQuery(query as esLaundryDistributionQuery);
		}
		#endregion

		virtual public LaundryDistribution DetachEntity(LaundryDistribution entity)
		{
			return base.DetachEntity(entity) as LaundryDistribution;
		}

		virtual public LaundryDistribution AttachEntity(LaundryDistribution entity)
		{
			return base.AttachEntity(entity) as LaundryDistribution;
		}

		virtual public void Combine(LaundryDistributionCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryDistribution this[int index]
		{
			get
			{
				return base[index] as LaundryDistribution;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryDistribution);
		}
	}

	[Serializable]
	abstract public class esLaundryDistribution : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryDistributionQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryDistribution()
		{
		}

		public esLaundryDistribution(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String distributionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(distributionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(distributionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String distributionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(distributionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(distributionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String distributionNo)
		{
			esLaundryDistributionQuery query = this.GetDynamicQuery();
			query.Where(query.DistributionNo == distributionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String distributionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("DistributionNo", distributionNo);
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
						case "DistributionNo": this.str.DistributionNo = (string)value; break;
						case "DistributionDate": this.str.DistributionDate = (string)value; break;
						case "DistributionTime": this.str.DistributionTime = (string)value; break;
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
						case "DistributionDate":

							if (value == null || value is System.DateTime)
								this.DistributionDate = (System.DateTime?)value;
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
		/// Maps to LaundryDistribution.DistributionNo
		/// </summary>
		virtual public System.String DistributionNo
		{
			get
			{
				return base.GetSystemString(LaundryDistributionMetadata.ColumnNames.DistributionNo);
			}

			set
			{
				base.SetSystemString(LaundryDistributionMetadata.ColumnNames.DistributionNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.DistributionDate
		/// </summary>
		virtual public System.DateTime? DistributionDate
		{
			get
			{
				return base.GetSystemDateTime(LaundryDistributionMetadata.ColumnNames.DistributionDate);
			}

			set
			{
				base.SetSystemDateTime(LaundryDistributionMetadata.ColumnNames.DistributionDate, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.DistributionTime
		/// </summary>
		virtual public System.String DistributionTime
		{
			get
			{
				return base.GetSystemString(LaundryDistributionMetadata.ColumnNames.DistributionTime);
			}

			set
			{
				base.SetSystemString(LaundryDistributionMetadata.ColumnNames.DistributionTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(LaundryDistributionMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(LaundryDistributionMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.HandedByUserID
		/// </summary>
		virtual public System.String HandedByUserID
		{
			get
			{
				return base.GetSystemString(LaundryDistributionMetadata.ColumnNames.HandedByUserID);
			}

			set
			{
				base.SetSystemString(LaundryDistributionMetadata.ColumnNames.HandedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.ReceivedBy
		/// </summary>
		virtual public System.String ReceivedBy
		{
			get
			{
				return base.GetSystemString(LaundryDistributionMetadata.ColumnNames.ReceivedBy);
			}

			set
			{
				base.SetSystemString(LaundryDistributionMetadata.ColumnNames.ReceivedBy, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(LaundryDistributionMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(LaundryDistributionMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryDistributionMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryDistributionMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(LaundryDistributionMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(LaundryDistributionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(LaundryDistributionMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(LaundryDistributionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryDistributionMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryDistributionMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(LaundryDistributionMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(LaundryDistributionMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryDistributionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryDistributionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryDistribution.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryDistributionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryDistributionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryDistribution entity)
			{
				this.entity = entity;
			}
			public System.String DistributionNo
			{
				get
				{
					System.String data = entity.DistributionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DistributionNo = null;
					else entity.DistributionNo = Convert.ToString(value);
				}
			}
			public System.String DistributionDate
			{
				get
				{
					System.DateTime? data = entity.DistributionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DistributionDate = null;
					else entity.DistributionDate = Convert.ToDateTime(value);
				}
			}
			public System.String DistributionTime
			{
				get
				{
					System.String data = entity.DistributionTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DistributionTime = null;
					else entity.DistributionTime = Convert.ToString(value);
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
			private esLaundryDistribution entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryDistributionQuery query)
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
				throw new Exception("esLaundryDistribution can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryDistribution : esLaundryDistribution
	{
	}

	[Serializable]
	abstract public class esLaundryDistributionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryDistributionMetadata.Meta();
			}
		}

		public esQueryItem DistributionNo
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.DistributionNo, esSystemType.String);
			}
		}

		public esQueryItem DistributionDate
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.DistributionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DistributionTime
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.DistributionTime, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem HandedByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.HandedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReceivedBy
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.ReceivedBy, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryDistributionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryDistributionCollection")]
	public partial class LaundryDistributionCollection : esLaundryDistributionCollection, IEnumerable<LaundryDistribution>
	{
		public LaundryDistributionCollection()
		{

		}

		public static implicit operator List<LaundryDistribution>(LaundryDistributionCollection coll)
		{
			List<LaundryDistribution> list = new List<LaundryDistribution>();

			foreach (LaundryDistribution emp in coll)
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
				return LaundryDistributionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryDistributionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryDistribution(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryDistribution();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryDistributionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryDistributionQuery();
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
		public bool Load(LaundryDistributionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryDistribution AddNew()
		{
			LaundryDistribution entity = base.AddNewEntity() as LaundryDistribution;

			return entity;
		}
		public LaundryDistribution FindByPrimaryKey(String distributionNo)
		{
			return base.FindByPrimaryKey(distributionNo) as LaundryDistribution;
		}

		#region IEnumerable< LaundryDistribution> Members

		IEnumerator<LaundryDistribution> IEnumerable<LaundryDistribution>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryDistribution;
			}
		}

		#endregion

		private LaundryDistributionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryDistribution' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryDistribution ({DistributionNo})")]
	[Serializable]
	public partial class LaundryDistribution : esLaundryDistribution
	{
		public LaundryDistribution()
		{
		}

		public LaundryDistribution(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryDistributionMetadata.Meta();
			}
		}

		override protected esLaundryDistributionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryDistributionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryDistributionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryDistributionQuery();
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
		public bool Load(LaundryDistributionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryDistributionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryDistributionQuery : esLaundryDistributionQuery
	{
		public LaundryDistributionQuery()
		{

		}

		public LaundryDistributionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryDistributionQuery";
		}
	}

	[Serializable]
	public partial class LaundryDistributionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryDistributionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.DistributionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.DistributionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.DistributionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.DistributionDate;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.DistributionTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.DistributionTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.ToServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.HandedByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.HandedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.ReceivedBy, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.ReceivedBy;
			c.CharacterMaxLength = 250;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.ApprovedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.ApprovedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.VoidDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryDistributionMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryDistributionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryDistributionMetadata Meta()
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
			public const string DistributionNo = "DistributionNo";
			public const string DistributionDate = "DistributionDate";
			public const string DistributionTime = "DistributionTime";
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
			public const string DistributionNo = "DistributionNo";
			public const string DistributionDate = "DistributionDate";
			public const string DistributionTime = "DistributionTime";
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
			lock (typeof(LaundryDistributionMetadata))
			{
				if (LaundryDistributionMetadata.mapDelegates == null)
				{
					LaundryDistributionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryDistributionMetadata.meta == null)
				{
					LaundryDistributionMetadata.meta = new LaundryDistributionMetadata();
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

				meta.AddTypeMap("DistributionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DistributionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DistributionTime", new esTypeMap("varchar", "System.String"));
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


				meta.Source = "LaundryDistribution";
				meta.Destination = "LaundryDistribution";
				meta.spInsert = "proc_LaundryDistributionInsert";
				meta.spUpdate = "proc_LaundryDistributionUpdate";
				meta.spDelete = "proc_LaundryDistributionDelete";
				meta.spLoadAll = "proc_LaundryDistributionLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryDistributionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryDistributionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
