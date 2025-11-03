/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/27/2023 7:23:16 PM
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
	abstract public class esClosingVisiteDownPaymentCollection : esEntityCollectionWAuditLog
	{
		public esClosingVisiteDownPaymentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ClosingVisiteDownPaymentCollection";
		}

		#region Query Logic
		protected void InitQuery(esClosingVisiteDownPaymentQuery query)
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
			this.InitQuery(query as esClosingVisiteDownPaymentQuery);
		}
		#endregion

		virtual public ClosingVisiteDownPayment DetachEntity(ClosingVisiteDownPayment entity)
		{
			return base.DetachEntity(entity) as ClosingVisiteDownPayment;
		}

		virtual public ClosingVisiteDownPayment AttachEntity(ClosingVisiteDownPayment entity)
		{
			return base.AttachEntity(entity) as ClosingVisiteDownPayment;
		}

		virtual public void Combine(ClosingVisiteDownPaymentCollection collection)
		{
			base.Combine(collection);
		}

		new public ClosingVisiteDownPayment this[int index]
		{
			get
			{
				return base[index] as ClosingVisiteDownPayment;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ClosingVisiteDownPayment);
		}
	}

	[Serializable]
	abstract public class esClosingVisiteDownPayment : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esClosingVisiteDownPaymentQuery GetDynamicQuery()
		{
			return null;
		}

		public esClosingVisiteDownPayment()
		{
		}

		public esClosingVisiteDownPayment(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String closingNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(closingNo);
			else
				return LoadByPrimaryKeyStoredProcedure(closingNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String closingNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(closingNo);
			else
				return LoadByPrimaryKeyStoredProcedure(closingNo);
		}

		private bool LoadByPrimaryKeyDynamic(String closingNo)
		{
			esClosingVisiteDownPaymentQuery query = this.GetDynamicQuery();
			query.Where(query.ClosingNo == closingNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String closingNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ClosingNo", closingNo);
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
						case "ClosingNo": this.str.ClosingNo = (string)value; break;
						case "ClosingDate": this.str.ClosingDate = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
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
						case "ClosingDate":

							if (value == null || value is System.DateTime)
								this.ClosingDate = (System.DateTime?)value;
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
		/// Maps to ClosingVisiteDownPayment.ClosingNo
		/// </summary>
		virtual public System.String ClosingNo
		{
			get
			{
				return base.GetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.ClosingNo);
			}

			set
			{
				base.SetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.ClosingNo, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.ClosingDate
		/// </summary>
		virtual public System.DateTime? ClosingDate
		{
			get
			{
				return base.GetSystemDateTime(ClosingVisiteDownPaymentMetadata.ColumnNames.ClosingDate);
			}

			set
			{
				base.SetSystemDateTime(ClosingVisiteDownPaymentMetadata.ColumnNames.ClosingDate, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ClosingVisiteDownPaymentMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(ClosingVisiteDownPaymentMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClosingVisiteDownPaymentMetadata.ColumnNames.ApprovedDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClosingVisiteDownPaymentMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ClosingVisiteDownPaymentMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(ClosingVisiteDownPaymentMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClosingVisiteDownPaymentMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClosingVisiteDownPaymentMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ClosingVisiteDownPaymentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ClosingVisiteDownPaymentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ClosingVisiteDownPayment.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ClosingVisiteDownPaymentMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esClosingVisiteDownPayment entity)
			{
				this.entity = entity;
			}
			public System.String ClosingNo
			{
				get
				{
					System.String data = entity.ClosingNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosingNo = null;
					else entity.ClosingNo = Convert.ToString(value);
				}
			}
			public System.String ClosingDate
			{
				get
				{
					System.DateTime? data = entity.ClosingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClosingDate = null;
					else entity.ClosingDate = Convert.ToDateTime(value);
				}
			}
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
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
			private esClosingVisiteDownPayment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esClosingVisiteDownPaymentQuery query)
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
				throw new Exception("esClosingVisiteDownPayment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ClosingVisiteDownPayment : esClosingVisiteDownPayment
	{
	}

	[Serializable]
	abstract public class esClosingVisiteDownPaymentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ClosingVisiteDownPaymentMetadata.Meta();
			}
		}

		public esQueryItem ClosingNo
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.ClosingNo, esSystemType.String);
			}
		}

		public esQueryItem ClosingDate
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.ClosingDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ClosingVisiteDownPaymentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ClosingVisiteDownPaymentCollection")]
	public partial class ClosingVisiteDownPaymentCollection : esClosingVisiteDownPaymentCollection, IEnumerable<ClosingVisiteDownPayment>
	{
		public ClosingVisiteDownPaymentCollection()
		{

		}

		public static implicit operator List<ClosingVisiteDownPayment>(ClosingVisiteDownPaymentCollection coll)
		{
			List<ClosingVisiteDownPayment> list = new List<ClosingVisiteDownPayment>();

			foreach (ClosingVisiteDownPayment emp in coll)
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
				return ClosingVisiteDownPaymentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClosingVisiteDownPaymentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ClosingVisiteDownPayment(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ClosingVisiteDownPayment();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ClosingVisiteDownPaymentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClosingVisiteDownPaymentQuery();
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
		public bool Load(ClosingVisiteDownPaymentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ClosingVisiteDownPayment AddNew()
		{
			ClosingVisiteDownPayment entity = base.AddNewEntity() as ClosingVisiteDownPayment;

			return entity;
		}
		public ClosingVisiteDownPayment FindByPrimaryKey(String closingNo)
		{
			return base.FindByPrimaryKey(closingNo) as ClosingVisiteDownPayment;
		}

		#region IEnumerable< ClosingVisiteDownPayment> Members

		IEnumerator<ClosingVisiteDownPayment> IEnumerable<ClosingVisiteDownPayment>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ClosingVisiteDownPayment;
			}
		}

		#endregion

		private ClosingVisiteDownPaymentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ClosingVisiteDownPayment' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ClosingVisiteDownPayment ({ClosingNo})")]
	[Serializable]
	public partial class ClosingVisiteDownPayment : esClosingVisiteDownPayment
	{
		public ClosingVisiteDownPayment()
		{
		}

		public ClosingVisiteDownPayment(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ClosingVisiteDownPaymentMetadata.Meta();
			}
		}

		override protected esClosingVisiteDownPaymentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ClosingVisiteDownPaymentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ClosingVisiteDownPaymentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ClosingVisiteDownPaymentQuery();
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
		public bool Load(ClosingVisiteDownPaymentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ClosingVisiteDownPaymentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ClosingVisiteDownPaymentQuery : esClosingVisiteDownPaymentQuery
	{
		public ClosingVisiteDownPaymentQuery()
		{

		}

		public ClosingVisiteDownPaymentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ClosingVisiteDownPaymentQuery";
		}
	}

	[Serializable]
	public partial class ClosingVisiteDownPaymentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ClosingVisiteDownPaymentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.ClosingNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.ClosingNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.ClosingDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.ClosingDate;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.PatientID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.IsApproved, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.ApprovedDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.ApprovedByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.VoidDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.VoidByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ClosingVisiteDownPaymentMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ClosingVisiteDownPaymentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ClosingVisiteDownPaymentMetadata Meta()
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
			public const string ClosingNo = "ClosingNo";
			public const string ClosingDate = "ClosingDate";
			public const string PatientID = "PatientID";
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
			public const string ClosingNo = "ClosingNo";
			public const string ClosingDate = "ClosingDate";
			public const string PatientID = "PatientID";
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
			lock (typeof(ClosingVisiteDownPaymentMetadata))
			{
				if (ClosingVisiteDownPaymentMetadata.mapDelegates == null)
				{
					ClosingVisiteDownPaymentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ClosingVisiteDownPaymentMetadata.meta == null)
				{
					ClosingVisiteDownPaymentMetadata.meta = new ClosingVisiteDownPaymentMetadata();
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

				meta.AddTypeMap("ClosingNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClosingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ClosingVisiteDownPayment";
				meta.Destination = "ClosingVisiteDownPayment";
				meta.spInsert = "proc_ClosingVisiteDownPaymentInsert";
				meta.spUpdate = "proc_ClosingVisiteDownPaymentUpdate";
				meta.spDelete = "proc_ClosingVisiteDownPaymentDelete";
				meta.spLoadAll = "proc_ClosingVisiteDownPaymentLoadAll";
				meta.spLoadByPrimaryKey = "proc_ClosingVisiteDownPaymentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ClosingVisiteDownPaymentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
