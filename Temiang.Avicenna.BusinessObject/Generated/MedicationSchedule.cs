/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/11/2022 8:50:17 AM
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
	abstract public class esMedicationScheduleCollection : esEntityCollectionWAuditLog
	{
		public esMedicationScheduleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicationScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicationScheduleQuery query)
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
			this.InitQuery(query as esMedicationScheduleQuery);
		}
		#endregion

		virtual public MedicationSchedule DetachEntity(MedicationSchedule entity)
		{
			return base.DetachEntity(entity) as MedicationSchedule;
		}

		virtual public MedicationSchedule AttachEntity(MedicationSchedule entity)
		{
			return base.AttachEntity(entity) as MedicationSchedule;
		}

		virtual public void Combine(MedicationScheduleCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicationSchedule this[int index]
		{
			get
			{
				return base[index] as MedicationSchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicationSchedule);
		}
	}

	[Serializable]
	abstract public class esMedicationSchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicationScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicationSchedule()
		{
		}

		public esMedicationSchedule(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 medicationReceiveNo, DateTime scheduleStartDate, Int32 scheduleNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo, scheduleStartDate, scheduleNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo, scheduleStartDate, scheduleNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 medicationReceiveNo, DateTime scheduleStartDate, Int32 scheduleNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo, scheduleStartDate, scheduleNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo, scheduleStartDate, scheduleNo);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 medicationReceiveNo, DateTime scheduleStartDate, Int32 scheduleNo)
		{
			esMedicationScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.MedicationReceiveNo == medicationReceiveNo, query.ScheduleStartDate == scheduleStartDate, query.ScheduleNo == scheduleNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 medicationReceiveNo, DateTime scheduleStartDate, Int32 scheduleNo)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicationReceiveNo", medicationReceiveNo);
			parms.Add("ScheduleStartDate", scheduleStartDate);
			parms.Add("ScheduleNo", scheduleNo);
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
						case "MedicationReceiveNo": this.str.MedicationReceiveNo = (string)value; break;
						case "ScheduleStartDate": this.str.ScheduleStartDate = (string)value; break;
						case "ScheduleNo": this.str.ScheduleNo = (string)value; break;
						case "ScheduleTime": this.str.ScheduleTime = (string)value; break;
						case "Qty": this.str.Qty = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MedicationReceiveNo":

							if (value == null || value is System.Int64)
								this.MedicationReceiveNo = (System.Int64?)value;
							break;
						case "ScheduleStartDate":

							if (value == null || value is System.DateTime)
								this.ScheduleStartDate = (System.DateTime?)value;
							break;
						case "ScheduleNo":

							if (value == null || value is System.Int32)
								this.ScheduleNo = (System.Int32?)value;
							break;
						case "ScheduleTime":

							if (value == null || value is System.DateTime)
								this.ScheduleTime = (System.DateTime?)value;
							break;
						case "Qty":

							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
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
		/// Maps to MedicationSchedule.MedicationReceiveNo
		/// </summary>
		virtual public System.Int64? MedicationReceiveNo
		{
			get
			{
				return base.GetSystemInt64(MedicationScheduleMetadata.ColumnNames.MedicationReceiveNo);
			}

			set
			{
				base.SetSystemInt64(MedicationScheduleMetadata.ColumnNames.MedicationReceiveNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationSchedule.ScheduleStartDate
		/// </summary>
		virtual public System.DateTime? ScheduleStartDate
		{
			get
			{
				return base.GetSystemDateTime(MedicationScheduleMetadata.ColumnNames.ScheduleStartDate);
			}

			set
			{
				base.SetSystemDateTime(MedicationScheduleMetadata.ColumnNames.ScheduleStartDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicationSchedule.ScheduleNo
		/// </summary>
		virtual public System.Int32? ScheduleNo
		{
			get
			{
				return base.GetSystemInt32(MedicationScheduleMetadata.ColumnNames.ScheduleNo);
			}

			set
			{
				base.SetSystemInt32(MedicationScheduleMetadata.ColumnNames.ScheduleNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationSchedule.ScheduleTime
		/// </summary>
		virtual public System.DateTime? ScheduleTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationScheduleMetadata.ColumnNames.ScheduleTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationScheduleMetadata.ColumnNames.ScheduleTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationSchedule.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(MedicationScheduleMetadata.ColumnNames.Qty);
			}

			set
			{
				base.SetSystemDecimal(MedicationScheduleMetadata.ColumnNames.Qty, value);
			}
		}
		/// <summary>
		/// Maps to MedicationSchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationSchedule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicationScheduleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicationScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicationSchedule entity)
			{
				this.entity = entity;
			}
			public System.String MedicationReceiveNo
			{
				get
				{
					System.Int64? data = entity.MedicationReceiveNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicationReceiveNo = null;
					else entity.MedicationReceiveNo = Convert.ToInt64(value);
				}
			}
			public System.String ScheduleStartDate
			{
				get
				{
					System.DateTime? data = entity.ScheduleStartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleStartDate = null;
					else entity.ScheduleStartDate = Convert.ToDateTime(value);
				}
			}
			public System.String ScheduleNo
			{
				get
				{
					System.Int32? data = entity.ScheduleNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleNo = null;
					else entity.ScheduleNo = Convert.ToInt32(value);
				}
			}
			public System.String ScheduleTime
			{
				get
				{
					System.DateTime? data = entity.ScheduleTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleTime = null;
					else entity.ScheduleTime = Convert.ToDateTime(value);
				}
			}
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
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
			private esMedicationSchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicationScheduleQuery query)
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
				throw new Exception("esMedicationSchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicationSchedule : esMedicationSchedule
	{
	}

	[Serializable]
	abstract public class esMedicationScheduleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicationScheduleMetadata.Meta();
			}
		}

		public esQueryItem MedicationReceiveNo
		{
			get
			{
				return new esQueryItem(this, MedicationScheduleMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
			}
		}

		public esQueryItem ScheduleStartDate
		{
			get
			{
				return new esQueryItem(this, MedicationScheduleMetadata.ColumnNames.ScheduleStartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ScheduleNo
		{
			get
			{
				return new esQueryItem(this, MedicationScheduleMetadata.ColumnNames.ScheduleNo, esSystemType.Int32);
			}
		}

		public esQueryItem ScheduleTime
		{
			get
			{
				return new esQueryItem(this, MedicationScheduleMetadata.ColumnNames.ScheduleTime, esSystemType.DateTime);
			}
		}

		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, MedicationScheduleMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicationScheduleCollection")]
	public partial class MedicationScheduleCollection : esMedicationScheduleCollection, IEnumerable<MedicationSchedule>
	{
		public MedicationScheduleCollection()
		{

		}

		public static implicit operator List<MedicationSchedule>(MedicationScheduleCollection coll)
		{
			List<MedicationSchedule> list = new List<MedicationSchedule>();

			foreach (MedicationSchedule emp in coll)
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
				return MedicationScheduleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicationSchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicationSchedule();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicationScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationScheduleQuery();
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
		public bool Load(MedicationScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicationSchedule AddNew()
		{
			MedicationSchedule entity = base.AddNewEntity() as MedicationSchedule;

			return entity;
		}
		public MedicationSchedule FindByPrimaryKey(Int64 medicationReceiveNo, DateTime scheduleStartDate, Int32 scheduleNo)
		{
			return base.FindByPrimaryKey(medicationReceiveNo, scheduleStartDate, scheduleNo) as MedicationSchedule;
		}

		#region IEnumerable< MedicationSchedule> Members

		IEnumerator<MedicationSchedule> IEnumerable<MedicationSchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicationSchedule;
			}
		}

		#endregion

		private MedicationScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicationSchedule' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicationSchedule ({MedicationReceiveNo, ScheduleStartDate, ScheduleNo})")]
	[Serializable]
	public partial class MedicationSchedule : esMedicationSchedule
	{
		public MedicationSchedule()
		{
		}

		public MedicationSchedule(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicationScheduleMetadata.Meta();
			}
		}

		override protected esMedicationScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicationScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationScheduleQuery();
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
		public bool Load(MedicationScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicationScheduleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicationScheduleQuery : esMedicationScheduleQuery
	{
		public MedicationScheduleQuery()
		{

		}

		public MedicationScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicationScheduleQuery";
		}
	}

	[Serializable]
	public partial class MedicationScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicationScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicationScheduleMetadata.ColumnNames.MedicationReceiveNo, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicationScheduleMetadata.PropertyNames.MedicationReceiveNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationScheduleMetadata.ColumnNames.ScheduleStartDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationScheduleMetadata.PropertyNames.ScheduleStartDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationScheduleMetadata.ColumnNames.ScheduleNo, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicationScheduleMetadata.PropertyNames.ScheduleNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationScheduleMetadata.ColumnNames.ScheduleTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationScheduleMetadata.PropertyNames.ScheduleTime;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationScheduleMetadata.ColumnNames.Qty, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicationScheduleMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationScheduleMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationScheduleMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationScheduleMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationScheduleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);


		}
		#endregion

		static public MedicationScheduleMetadata Meta()
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
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string ScheduleStartDate = "ScheduleStartDate";
			public const string ScheduleNo = "ScheduleNo";
			public const string ScheduleTime = "ScheduleTime";
			public const string Qty = "Qty";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string ScheduleStartDate = "ScheduleStartDate";
			public const string ScheduleNo = "ScheduleNo";
			public const string ScheduleTime = "ScheduleTime";
			public const string Qty = "Qty";
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
			lock (typeof(MedicationScheduleMetadata))
			{
				if (MedicationScheduleMetadata.mapDelegates == null)
				{
					MedicationScheduleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicationScheduleMetadata.meta == null)
				{
					MedicationScheduleMetadata.meta = new MedicationScheduleMetadata();
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

				meta.AddTypeMap("MedicationReceiveNo", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("ScheduleStartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ScheduleNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ScheduleTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicationSchedule";
				meta.Destination = "MedicationSchedule";
				meta.spInsert = "proc_MedicationScheduleInsert";
				meta.spUpdate = "proc_MedicationScheduleUpdate";
				meta.spDelete = "proc_MedicationScheduleDelete";
				meta.spLoadAll = "proc_MedicationScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicationScheduleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicationScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
