/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/29/2023 8:04:48 PM
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
	abstract public class esCssdDistributionCollection : esEntityCollectionWAuditLog
	{
		public esCssdDistributionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CssdDistributionCollection";
		}

		#region Query Logic
		protected void InitQuery(esCssdDistributionQuery query)
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
			this.InitQuery(query as esCssdDistributionQuery);
		}
		#endregion

		virtual public CssdDistribution DetachEntity(CssdDistribution entity)
		{
			return base.DetachEntity(entity) as CssdDistribution;
		}

		virtual public CssdDistribution AttachEntity(CssdDistribution entity)
		{
			return base.AttachEntity(entity) as CssdDistribution;
		}

		virtual public void Combine(CssdDistributionCollection collection)
		{
			base.Combine(collection);
		}

		new public CssdDistribution this[int index]
		{
			get
			{
				return base[index] as CssdDistribution;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CssdDistribution);
		}
	}

	[Serializable]
	abstract public class esCssdDistribution : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCssdDistributionQuery GetDynamicQuery()
		{
			return null;
		}

		public esCssdDistribution()
		{
		}

		public esCssdDistribution(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esCssdDistributionQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "TransactionTime": this.str.TransactionTime = (string)value; break;
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
						case "TransactionDate":

							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
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
		/// Maps to CssdDistribution.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CssdDistributionMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CssdDistributionMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(CssdDistributionMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(CssdDistributionMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.TransactionTime
		/// </summary>
		virtual public System.String TransactionTime
		{
			get
			{
				return base.GetSystemString(CssdDistributionMetadata.ColumnNames.TransactionTime);
			}

			set
			{
				base.SetSystemString(CssdDistributionMetadata.ColumnNames.TransactionTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.ToServiceUnitID
		/// </summary>
		virtual public System.String ToServiceUnitID
		{
			get
			{
				return base.GetSystemString(CssdDistributionMetadata.ColumnNames.ToServiceUnitID);
			}

			set
			{
				base.SetSystemString(CssdDistributionMetadata.ColumnNames.ToServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.HandedByUserID
		/// </summary>
		virtual public System.String HandedByUserID
		{
			get
			{
				return base.GetSystemString(CssdDistributionMetadata.ColumnNames.HandedByUserID);
			}

			set
			{
				base.SetSystemString(CssdDistributionMetadata.ColumnNames.HandedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.ReceivedBy
		/// </summary>
		virtual public System.String ReceivedBy
		{
			get
			{
				return base.GetSystemString(CssdDistributionMetadata.ColumnNames.ReceivedBy);
			}

			set
			{
				base.SetSystemString(CssdDistributionMetadata.ColumnNames.ReceivedBy, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(CssdDistributionMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(CssdDistributionMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdDistributionMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdDistributionMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(CssdDistributionMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(CssdDistributionMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(CssdDistributionMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(CssdDistributionMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdDistributionMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdDistributionMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(CssdDistributionMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(CssdDistributionMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CssdDistributionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CssdDistributionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CssdDistribution.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CssdDistributionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CssdDistributionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCssdDistribution entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
				}
			}
			public System.String TransactionTime
			{
				get
				{
					System.String data = entity.TransactionTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionTime = null;
					else entity.TransactionTime = Convert.ToString(value);
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
			private esCssdDistribution entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCssdDistributionQuery query)
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
				throw new Exception("esCssdDistribution can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CssdDistribution : esCssdDistribution
	{
	}

	[Serializable]
	abstract public class esCssdDistributionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CssdDistributionMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem TransactionTime
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.TransactionTime, esSystemType.String);
			}
		}

		public esQueryItem ToServiceUnitID
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.ToServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem HandedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.HandedByUserID, esSystemType.String);
			}
		}

		public esQueryItem ReceivedBy
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.ReceivedBy, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CssdDistributionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CssdDistributionCollection")]
	public partial class CssdDistributionCollection : esCssdDistributionCollection, IEnumerable<CssdDistribution>
	{
		public CssdDistributionCollection()
		{

		}

		public static implicit operator List<CssdDistribution>(CssdDistributionCollection coll)
		{
			List<CssdDistribution> list = new List<CssdDistribution>();

			foreach (CssdDistribution emp in coll)
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
				return CssdDistributionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdDistributionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CssdDistribution(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CssdDistribution();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CssdDistributionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdDistributionQuery();
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
		public bool Load(CssdDistributionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CssdDistribution AddNew()
		{
			CssdDistribution entity = base.AddNewEntity() as CssdDistribution;

			return entity;
		}
		public CssdDistribution FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as CssdDistribution;
		}

		#region IEnumerable< CssdDistribution> Members

		IEnumerator<CssdDistribution> IEnumerable<CssdDistribution>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CssdDistribution;
			}
		}

		#endregion

		private CssdDistributionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CssdDistribution' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CssdDistribution ({TransactionNo})")]
	[Serializable]
	public partial class CssdDistribution : esCssdDistribution
	{
		public CssdDistribution()
		{
		}

		public CssdDistribution(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CssdDistributionMetadata.Meta();
			}
		}

		override protected esCssdDistributionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CssdDistributionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CssdDistributionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CssdDistributionQuery();
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
		public bool Load(CssdDistributionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CssdDistributionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CssdDistributionQuery : esCssdDistributionQuery
	{
		public CssdDistributionQuery()
		{

		}

		public CssdDistributionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CssdDistributionQuery";
		}
	}

	[Serializable]
	public partial class CssdDistributionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CssdDistributionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.TransactionTime, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.TransactionTime;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.ToServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.ToServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.HandedByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.HandedByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.ReceivedBy, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.ReceivedBy;
			c.CharacterMaxLength = 150;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.ApprovedDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.ApprovedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.VoidDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.VoidByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CssdDistributionMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = CssdDistributionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CssdDistributionMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string TransactionTime = "TransactionTime";
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
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string TransactionTime = "TransactionTime";
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
			lock (typeof(CssdDistributionMetadata))
			{
				if (CssdDistributionMetadata.mapDelegates == null)
				{
					CssdDistributionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CssdDistributionMetadata.meta == null)
				{
					CssdDistributionMetadata.meta = new CssdDistributionMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TransactionTime", new esTypeMap("varchar", "System.String"));
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


				meta.Source = "CssdDistribution";
				meta.Destination = "CssdDistribution";
				meta.spInsert = "proc_CssdDistributionInsert";
				meta.spUpdate = "proc_CssdDistributionUpdate";
				meta.spDelete = "proc_CssdDistributionDelete";
				meta.spLoadAll = "proc_CssdDistributionLoadAll";
				meta.spLoadByPrimaryKey = "proc_CssdDistributionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CssdDistributionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
