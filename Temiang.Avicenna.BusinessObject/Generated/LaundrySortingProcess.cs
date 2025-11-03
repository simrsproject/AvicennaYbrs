/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/15/2022 7:52:36 PM
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
	abstract public class esLaundrySortingProcessCollection : esEntityCollectionWAuditLog
	{
		public esLaundrySortingProcessCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundrySortingProcessCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundrySortingProcessQuery query)
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
			this.InitQuery(query as esLaundrySortingProcessQuery);
		}
		#endregion

		virtual public LaundrySortingProcess DetachEntity(LaundrySortingProcess entity)
		{
			return base.DetachEntity(entity) as LaundrySortingProcess;
		}

		virtual public LaundrySortingProcess AttachEntity(LaundrySortingProcess entity)
		{
			return base.AttachEntity(entity) as LaundrySortingProcess;
		}

		virtual public void Combine(LaundrySortingProcessCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundrySortingProcess this[int index]
		{
			get
			{
				return base[index] as LaundrySortingProcess;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundrySortingProcess);
		}
	}

	[Serializable]
	abstract public class esLaundrySortingProcess : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundrySortingProcessQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundrySortingProcess()
		{
		}

		public esLaundrySortingProcess(DataRow row)
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
			esLaundrySortingProcessQuery query = this.GetDynamicQuery();
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
						case "ProcessNo": this.str.ProcessNo = (string)value; break;
						case "ProcessSeqNo": this.str.ProcessSeqNo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
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
		/// Maps to LaundrySortingProcess.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(LaundrySortingProcessMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(LaundrySortingProcessMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(LaundrySortingProcessMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(LaundrySortingProcessMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.ProcessNo
		/// </summary>
		virtual public System.String ProcessNo
		{
			get
			{
				return base.GetSystemString(LaundrySortingProcessMetadata.ColumnNames.ProcessNo);
			}

			set
			{
				base.SetSystemString(LaundrySortingProcessMetadata.ColumnNames.ProcessNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.ProcessSeqNo
		/// </summary>
		virtual public System.String ProcessSeqNo
		{
			get
			{
				return base.GetSystemString(LaundrySortingProcessMetadata.ColumnNames.ProcessSeqNo);
			}

			set
			{
				base.SetSystemString(LaundrySortingProcessMetadata.ColumnNames.ProcessSeqNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(LaundrySortingProcessMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(LaundrySortingProcessMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(LaundrySortingProcessMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(LaundrySortingProcessMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundrySortingProcessMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundrySortingProcessMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(LaundrySortingProcessMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(LaundrySortingProcessMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(LaundrySortingProcessMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(LaundrySortingProcessMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundrySortingProcessMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundrySortingProcessMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(LaundrySortingProcessMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(LaundrySortingProcessMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundrySortingProcessMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundrySortingProcessMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundrySortingProcess.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundrySortingProcessMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundrySortingProcessMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundrySortingProcess entity)
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
			public System.String ProcessNo
			{
				get
				{
					System.String data = entity.ProcessNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessNo = null;
					else entity.ProcessNo = Convert.ToString(value);
				}
			}
			public System.String ProcessSeqNo
			{
				get
				{
					System.String data = entity.ProcessSeqNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProcessSeqNo = null;
					else entity.ProcessSeqNo = Convert.ToString(value);
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
			private esLaundrySortingProcess entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundrySortingProcessQuery query)
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
				throw new Exception("esLaundrySortingProcess can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundrySortingProcess : esLaundrySortingProcess
	{
	}

	[Serializable]
	abstract public class esLaundrySortingProcessQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundrySortingProcessMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ProcessNo
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.ProcessNo, esSystemType.String);
			}
		}

		public esQueryItem ProcessSeqNo
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.ProcessSeqNo, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundrySortingProcessMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundrySortingProcessCollection")]
	public partial class LaundrySortingProcessCollection : esLaundrySortingProcessCollection, IEnumerable<LaundrySortingProcess>
	{
		public LaundrySortingProcessCollection()
		{

		}

		public static implicit operator List<LaundrySortingProcess>(LaundrySortingProcessCollection coll)
		{
			List<LaundrySortingProcess> list = new List<LaundrySortingProcess>();

			foreach (LaundrySortingProcess emp in coll)
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
				return LaundrySortingProcessMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundrySortingProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundrySortingProcess(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundrySortingProcess();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundrySortingProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundrySortingProcessQuery();
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
		public bool Load(LaundrySortingProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundrySortingProcess AddNew()
		{
			LaundrySortingProcess entity = base.AddNewEntity() as LaundrySortingProcess;

			return entity;
		}
		public LaundrySortingProcess FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as LaundrySortingProcess;
		}

		#region IEnumerable< LaundrySortingProcess> Members

		IEnumerator<LaundrySortingProcess> IEnumerable<LaundrySortingProcess>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundrySortingProcess;
			}
		}

		#endregion

		private LaundrySortingProcessQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundrySortingProcess' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundrySortingProcess ({TransactionNo})")]
	[Serializable]
	public partial class LaundrySortingProcess : esLaundrySortingProcess
	{
		public LaundrySortingProcess()
		{
		}

		public LaundrySortingProcess(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundrySortingProcessMetadata.Meta();
			}
		}

		override protected esLaundrySortingProcessQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundrySortingProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundrySortingProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundrySortingProcessQuery();
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
		public bool Load(LaundrySortingProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundrySortingProcessQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundrySortingProcessQuery : esLaundrySortingProcessQuery
	{
		public LaundrySortingProcessQuery()
		{

		}

		public LaundrySortingProcessQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundrySortingProcessQuery";
		}
	}

	[Serializable]
	public partial class LaundrySortingProcessMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundrySortingProcessMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.ProcessNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.ProcessNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.ProcessSeqNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.ProcessSeqNo;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.IsApproved, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.ApprovedDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.ApprovedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.IsVoid, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.VoidDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.VoidByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundrySortingProcessMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundrySortingProcessMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundrySortingProcessMetadata Meta()
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
			public const string ProcessNo = "ProcessNo";
			public const string ProcessSeqNo = "ProcessSeqNo";
			public const string Notes = "Notes";
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
			public const string ProcessNo = "ProcessNo";
			public const string ProcessSeqNo = "ProcessSeqNo";
			public const string Notes = "Notes";
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
			lock (typeof(LaundrySortingProcessMetadata))
			{
				if (LaundrySortingProcessMetadata.mapDelegates == null)
				{
					LaundrySortingProcessMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundrySortingProcessMetadata.meta == null)
				{
					LaundrySortingProcessMetadata.meta = new LaundrySortingProcessMetadata();
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
				meta.AddTypeMap("ProcessNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProcessSeqNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundrySortingProcess";
				meta.Destination = "LaundrySortingProcess";
				meta.spInsert = "proc_LaundrySortingProcessInsert";
				meta.spUpdate = "proc_LaundrySortingProcessUpdate";
				meta.spDelete = "proc_LaundrySortingProcessDelete";
				meta.spLoadAll = "proc_LaundrySortingProcessLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundrySortingProcessLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundrySortingProcessMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
