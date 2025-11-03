/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/6/2022 10:04:17 AM
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
	abstract public class esMedicationReceiveStatusCollection : esEntityCollectionWAuditLog
	{
		public esMedicationReceiveStatusCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicationReceiveStatusCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicationReceiveStatusQuery query)
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
			this.InitQuery(query as esMedicationReceiveStatusQuery);
		}
		#endregion

		virtual public MedicationReceiveStatus DetachEntity(MedicationReceiveStatus entity)
		{
			return base.DetachEntity(entity) as MedicationReceiveStatus;
		}

		virtual public MedicationReceiveStatus AttachEntity(MedicationReceiveStatus entity)
		{
			return base.AttachEntity(entity) as MedicationReceiveStatus;
		}

		virtual public void Combine(MedicationReceiveStatusCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicationReceiveStatus this[int index]
		{
			get
			{
				return base[index] as MedicationReceiveStatus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicationReceiveStatus);
		}
	}

	[Serializable]
	abstract public class esMedicationReceiveStatus : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicationReceiveStatusQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicationReceiveStatus()
		{
		}

		public esMedicationReceiveStatus(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 medicationReceiveNo, DateTime statusDateTime)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo, statusDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo, statusDateTime);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 medicationReceiveNo, DateTime statusDateTime)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo, statusDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo, statusDateTime);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 medicationReceiveNo, DateTime statusDateTime)
		{
			esMedicationReceiveStatusQuery query = this.GetDynamicQuery();
			query.Where(query.MedicationReceiveNo == medicationReceiveNo, query.StatusDateTime == statusDateTime);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 medicationReceiveNo, DateTime statusDateTime)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicationReceiveNo", medicationReceiveNo);
			parms.Add("StatusDateTime", statusDateTime);
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
						case "StatusDateTime": this.str.StatusDateTime = (string)value; break;
						case "IsMedicationStop": this.str.IsMedicationStop = (string)value; break;
						case "SRMedicationStopReason": this.str.SRMedicationStopReason = (string)value; break;
						case "MedicationReason": this.str.MedicationReason = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "SRMedicationStatusReason": this.str.SRMedicationStatusReason = (string)value; break;
						case "SRMedicationStatusType": this.str.SRMedicationStatusType = (string)value; break;
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
						case "StatusDateTime":

							if (value == null || value is System.DateTime)
								this.StatusDateTime = (System.DateTime?)value;
							break;
						case "IsMedicationStop":

							if (value == null || value is System.Boolean)
								this.IsMedicationStop = (System.Boolean?)value;
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
		/// Maps to MedicationReceiveStatus.MedicationReceiveNo
		/// </summary>
		virtual public System.Int64? MedicationReceiveNo
		{
			get
			{
				return base.GetSystemInt64(MedicationReceiveStatusMetadata.ColumnNames.MedicationReceiveNo);
			}

			set
			{
				base.SetSystemInt64(MedicationReceiveStatusMetadata.ColumnNames.MedicationReceiveNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveStatus.StatusDateTime
		/// </summary>
		virtual public System.DateTime? StatusDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveStatusMetadata.ColumnNames.StatusDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationReceiveStatusMetadata.ColumnNames.StatusDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveStatus.IsMedicationStop
		/// </summary>
		virtual public System.Boolean? IsMedicationStop
		{
			get
			{
				return base.GetSystemBoolean(MedicationReceiveStatusMetadata.ColumnNames.IsMedicationStop);
			}

			set
			{
				base.SetSystemBoolean(MedicationReceiveStatusMetadata.ColumnNames.IsMedicationStop, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveStatus.SRMedicationStopReason
		/// </summary>
		virtual public System.String SRMedicationStopReason
		{
			get
			{
				return base.GetSystemString(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStopReason);
			}

			set
			{
				base.SetSystemString(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStopReason, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveStatus.MedicationReason
		/// </summary>
		virtual public System.String MedicationReason
		{
			get
			{
				return base.GetSystemString(MedicationReceiveStatusMetadata.ColumnNames.MedicationReason);
			}

			set
			{
				base.SetSystemString(MedicationReceiveStatusMetadata.ColumnNames.MedicationReason, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveStatus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicationReceiveStatusMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicationReceiveStatusMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveStatus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicationReceiveStatusMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicationReceiveStatusMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveStatus.SRMedicationStatusReason
		/// </summary>
		virtual public System.String SRMedicationStatusReason
		{
			get
			{
				return base.GetSystemString(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStatusReason);
			}

			set
			{
				base.SetSystemString(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStatusReason, value);
			}
		}
		/// <summary>
		/// Maps to MedicationReceiveStatus.SRMedicationStatusType
		/// </summary>
		virtual public System.String SRMedicationStatusType
		{
			get
			{
				return base.GetSystemString(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStatusType);
			}

			set
			{
				base.SetSystemString(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStatusType, value);
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
			public esStrings(esMedicationReceiveStatus entity)
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
			public System.String StatusDateTime
			{
				get
				{
					System.DateTime? data = entity.StatusDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StatusDateTime = null;
					else entity.StatusDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsMedicationStop
			{
				get
				{
					System.Boolean? data = entity.IsMedicationStop;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMedicationStop = null;
					else entity.IsMedicationStop = Convert.ToBoolean(value);
				}
			}
			public System.String SRMedicationStopReason
			{
				get
				{
					System.String data = entity.SRMedicationStopReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicationStopReason = null;
					else entity.SRMedicationStopReason = Convert.ToString(value);
				}
			}
			public System.String MedicationReason
			{
				get
				{
					System.String data = entity.MedicationReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicationReason = null;
					else entity.MedicationReason = Convert.ToString(value);
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
			public System.String SRMedicationStatusReason
			{
				get
				{
					System.String data = entity.SRMedicationStatusReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicationStatusReason = null;
					else entity.SRMedicationStatusReason = Convert.ToString(value);
				}
			}
			public System.String SRMedicationStatusType
			{
				get
				{
					System.String data = entity.SRMedicationStatusType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicationStatusType = null;
					else entity.SRMedicationStatusType = Convert.ToString(value);
				}
			}
			private esMedicationReceiveStatus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicationReceiveStatusQuery query)
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
				throw new Exception("esMedicationReceiveStatus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicationReceiveStatus : esMedicationReceiveStatus
	{
	}

	[Serializable]
	abstract public class esMedicationReceiveStatusQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicationReceiveStatusMetadata.Meta();
			}
		}

		public esQueryItem MedicationReceiveNo
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
			}
		}

		public esQueryItem StatusDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.StatusDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsMedicationStop
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.IsMedicationStop, esSystemType.Boolean);
			}
		}

		public esQueryItem SRMedicationStopReason
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStopReason, esSystemType.String);
			}
		}

		public esQueryItem MedicationReason
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.MedicationReason, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem SRMedicationStatusReason
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStatusReason, esSystemType.String);
			}
		}

		public esQueryItem SRMedicationStatusType
		{
			get
			{
				return new esQueryItem(this, MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStatusType, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicationReceiveStatusCollection")]
	public partial class MedicationReceiveStatusCollection : esMedicationReceiveStatusCollection, IEnumerable<MedicationReceiveStatus>
	{
		public MedicationReceiveStatusCollection()
		{

		}

		public static implicit operator List<MedicationReceiveStatus>(MedicationReceiveStatusCollection coll)
		{
			List<MedicationReceiveStatus> list = new List<MedicationReceiveStatus>();

			foreach (MedicationReceiveStatus emp in coll)
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
				return MedicationReceiveStatusMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReceiveStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicationReceiveStatus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicationReceiveStatus();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicationReceiveStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReceiveStatusQuery();
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
		public bool Load(MedicationReceiveStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicationReceiveStatus AddNew()
		{
			MedicationReceiveStatus entity = base.AddNewEntity() as MedicationReceiveStatus;

			return entity;
		}
		public MedicationReceiveStatus FindByPrimaryKey(Int64 medicationReceiveNo, DateTime statusDateTime)
		{
			return base.FindByPrimaryKey(medicationReceiveNo, statusDateTime) as MedicationReceiveStatus;
		}

		#region IEnumerable< MedicationReceiveStatus> Members

		IEnumerator<MedicationReceiveStatus> IEnumerable<MedicationReceiveStatus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicationReceiveStatus;
			}
		}

		#endregion

		private MedicationReceiveStatusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicationReceiveStatus' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicationReceiveStatus ({MedicationReceiveNo, StatusDateTime})")]
	[Serializable]
	public partial class MedicationReceiveStatus : esMedicationReceiveStatus
	{
		public MedicationReceiveStatus()
		{
		}

		public MedicationReceiveStatus(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicationReceiveStatusMetadata.Meta();
			}
		}

		override protected esMedicationReceiveStatusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicationReceiveStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicationReceiveStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicationReceiveStatusQuery();
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
		public bool Load(MedicationReceiveStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicationReceiveStatusQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicationReceiveStatusQuery : esMedicationReceiveStatusQuery
	{
		public MedicationReceiveStatusQuery()
		{

		}

		public MedicationReceiveStatusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicationReceiveStatusQuery";
		}
	}

	[Serializable]
	public partial class MedicationReceiveStatusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicationReceiveStatusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.MedicationReceiveNo, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.MedicationReceiveNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.StatusDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.StatusDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.IsMedicationStop, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.IsMedicationStop;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStopReason, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.SRMedicationStopReason;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.MedicationReason, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.MedicationReason;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStatusReason, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.SRMedicationStatusReason;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicationReceiveStatusMetadata.ColumnNames.SRMedicationStatusType, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicationReceiveStatusMetadata.PropertyNames.SRMedicationStatusType;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicationReceiveStatusMetadata Meta()
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
			public const string StatusDateTime = "StatusDateTime";
			public const string IsMedicationStop = "IsMedicationStop";
			public const string SRMedicationStopReason = "SRMedicationStopReason";
			public const string MedicationReason = "MedicationReason";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRMedicationStatusReason = "SRMedicationStatusReason";
			public const string SRMedicationStatusType = "SRMedicationStatusType";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string StatusDateTime = "StatusDateTime";
			public const string IsMedicationStop = "IsMedicationStop";
			public const string SRMedicationStopReason = "SRMedicationStopReason";
			public const string MedicationReason = "MedicationReason";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string SRMedicationStatusReason = "SRMedicationStatusReason";
			public const string SRMedicationStatusType = "SRMedicationStatusType";
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
			lock (typeof(MedicationReceiveStatusMetadata))
			{
				if (MedicationReceiveStatusMetadata.mapDelegates == null)
				{
					MedicationReceiveStatusMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicationReceiveStatusMetadata.meta == null)
				{
					MedicationReceiveStatusMetadata.meta = new MedicationReceiveStatusMetadata();
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
				meta.AddTypeMap("StatusDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsMedicationStop", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRMedicationStopReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MedicationReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicationStatusReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicationStatusType", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicationReceiveStatus";
				meta.Destination = "MedicationReceiveStatus";
				meta.spInsert = "proc_MedicationReceiveStatusInsert";
				meta.spUpdate = "proc_MedicationReceiveStatusUpdate";
				meta.spDelete = "proc_MedicationReceiveStatusDelete";
				meta.spLoadAll = "proc_MedicationReceiveStatusLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicationReceiveStatusLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicationReceiveStatusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
