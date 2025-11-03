/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/25/2021 3:45:04 PM
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
	abstract public class esLaundryRepairingProcessCollection : esEntityCollectionWAuditLog
	{
		public esLaundryRepairingProcessCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "LaundryRepairingProcessCollection";
		}

		#region Query Logic
		protected void InitQuery(esLaundryRepairingProcessQuery query)
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
			this.InitQuery(query as esLaundryRepairingProcessQuery);
		}
		#endregion

		virtual public LaundryRepairingProcess DetachEntity(LaundryRepairingProcess entity)
		{
			return base.DetachEntity(entity) as LaundryRepairingProcess;
		}

		virtual public LaundryRepairingProcess AttachEntity(LaundryRepairingProcess entity)
		{
			return base.AttachEntity(entity) as LaundryRepairingProcess;
		}

		virtual public void Combine(LaundryRepairingProcessCollection collection)
		{
			base.Combine(collection);
		}

		new public LaundryRepairingProcess this[int index]
		{
			get
			{
				return base[index] as LaundryRepairingProcess;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LaundryRepairingProcess);
		}
	}

	[Serializable]
	abstract public class esLaundryRepairingProcess : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLaundryRepairingProcessQuery GetDynamicQuery()
		{
			return null;
		}

		public esLaundryRepairingProcess()
		{
		}

		public esLaundryRepairingProcess(DataRow row)
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
			esLaundryRepairingProcessQuery query = this.GetDynamicQuery();
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
						case "Notes": this.str.Notes = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "IsClosed": this.str.IsClosed = (string)value; break;
						case "ClosedDateTime": this.str.ClosedDateTime = (string)value; break;
						case "ClosedByUserID": this.str.ClosedByUserID = (string)value; break;
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
						case "IsClosed":

							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						case "ClosedDateTime":

							if (value == null || value is System.DateTime)
								this.ClosedDateTime = (System.DateTime?)value;
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
		/// Maps to LaundryRepairingProcess.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(LaundryRepairingProcessMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(LaundryRepairingProcessMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.TransactionDate);
			}

			set
			{
				base.SetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(LaundryRepairingProcessMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(LaundryRepairingProcessMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(LaundryRepairingProcessMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(LaundryRepairingProcessMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(LaundryRepairingProcessMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(LaundryRepairingProcessMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(LaundryRepairingProcessMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(LaundryRepairingProcessMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(LaundryRepairingProcessMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(LaundryRepairingProcessMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(LaundryRepairingProcessMetadata.ColumnNames.IsClosed);
			}

			set
			{
				base.SetSystemBoolean(LaundryRepairingProcessMetadata.ColumnNames.IsClosed, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.ClosedDateTime
		/// </summary>
		virtual public System.DateTime? ClosedDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.ClosedDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.ClosedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.ClosedByUserID
		/// </summary>
		virtual public System.String ClosedByUserID
		{
			get
			{
				return base.GetSystemString(LaundryRepairingProcessMetadata.ColumnNames.ClosedByUserID);
			}

			set
			{
				base.SetSystemString(LaundryRepairingProcessMetadata.ColumnNames.ClosedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(LaundryRepairingProcessMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to LaundryRepairingProcess.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(LaundryRepairingProcessMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(LaundryRepairingProcessMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esLaundryRepairingProcess entity)
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
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
			public System.String ClosedDateTime
			{
				get
				{
					System.DateTime? data = entity.ClosedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedDateTime = null;
					else entity.ClosedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ClosedByUserID
			{
				get
				{
					System.String data = entity.ClosedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosedByUserID = null;
					else entity.ClosedByUserID = Convert.ToString(value);
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
			private esLaundryRepairingProcess entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLaundryRepairingProcessQuery query)
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
				throw new Exception("esLaundryRepairingProcess can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class LaundryRepairingProcess : esLaundryRepairingProcess
	{
	}

	[Serializable]
	abstract public class esLaundryRepairingProcessQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return LaundryRepairingProcessMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		}

		public esQueryItem ClosedDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.ClosedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ClosedByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.ClosedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, LaundryRepairingProcessMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LaundryRepairingProcessCollection")]
	public partial class LaundryRepairingProcessCollection : esLaundryRepairingProcessCollection, IEnumerable<LaundryRepairingProcess>
	{
		public LaundryRepairingProcessCollection()
		{

		}

		public static implicit operator List<LaundryRepairingProcess>(LaundryRepairingProcessCollection coll)
		{
			List<LaundryRepairingProcess> list = new List<LaundryRepairingProcess>();

			foreach (LaundryRepairingProcess emp in coll)
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
				return LaundryRepairingProcessMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryRepairingProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LaundryRepairingProcess(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LaundryRepairingProcess();
		}

		#endregion

		[BrowsableAttribute(false)]
		public LaundryRepairingProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryRepairingProcessQuery();
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
		public bool Load(LaundryRepairingProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public LaundryRepairingProcess AddNew()
		{
			LaundryRepairingProcess entity = base.AddNewEntity() as LaundryRepairingProcess;

			return entity;
		}
		public LaundryRepairingProcess FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as LaundryRepairingProcess;
		}

		#region IEnumerable< LaundryRepairingProcess> Members

		IEnumerator<LaundryRepairingProcess> IEnumerable<LaundryRepairingProcess>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as LaundryRepairingProcess;
			}
		}

		#endregion

		private LaundryRepairingProcessQuery query;
	}


	/// <summary>
	/// Encapsulates the 'LaundryRepairingProcess' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("LaundryRepairingProcess ({TransactionNo})")]
	[Serializable]
	public partial class LaundryRepairingProcess : esLaundryRepairingProcess
	{
		public LaundryRepairingProcess()
		{
		}

		public LaundryRepairingProcess(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LaundryRepairingProcessMetadata.Meta();
			}
		}

		override protected esLaundryRepairingProcessQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LaundryRepairingProcessQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public LaundryRepairingProcessQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LaundryRepairingProcessQuery();
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
		public bool Load(LaundryRepairingProcessQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private LaundryRepairingProcessQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class LaundryRepairingProcessQuery : esLaundryRepairingProcessQuery
	{
		public LaundryRepairingProcessQuery()
		{

		}

		public LaundryRepairingProcessQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "LaundryRepairingProcessQuery";
		}
	}

	[Serializable]
	public partial class LaundryRepairingProcessMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LaundryRepairingProcessMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.IsApproved, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.ApprovedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.ApprovedByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.VoidDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.VoidByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.IsClosed, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.IsClosed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.ClosedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.ClosedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.ClosedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.ClosedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(LaundryRepairingProcessMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = LaundryRepairingProcessMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public LaundryRepairingProcessMetadata Meta()
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
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string Notes = "Notes";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDateTime = "ApprovedDateTime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string IsClosed = "IsClosed";
			public const string ClosedDateTime = "ClosedDateTime";
			public const string ClosedByUserID = "ClosedByUserID";
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
			lock (typeof(LaundryRepairingProcessMetadata))
			{
				if (LaundryRepairingProcessMetadata.mapDelegates == null)
				{
					LaundryRepairingProcessMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (LaundryRepairingProcessMetadata.meta == null)
				{
					LaundryRepairingProcessMetadata.meta = new LaundryRepairingProcessMetadata();
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
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ClosedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ClosedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "LaundryRepairingProcess";
				meta.Destination = "LaundryRepairingProcess";
				meta.spInsert = "proc_LaundryRepairingProcessInsert";
				meta.spUpdate = "proc_LaundryRepairingProcessUpdate";
				meta.spDelete = "proc_LaundryRepairingProcessDelete";
				meta.spLoadAll = "proc_LaundryRepairingProcessLoadAll";
				meta.spLoadByPrimaryKey = "proc_LaundryRepairingProcessLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LaundryRepairingProcessMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
