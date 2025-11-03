/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/27/2020 1:38:49 PM
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
	abstract public class esPatientDischargeAppointmentCollection : esEntityCollectionWAuditLog
	{
		public esPatientDischargeAppointmentCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientDischargeAppointmentCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientDischargeAppointmentQuery query)
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
			this.InitQuery(query as esPatientDischargeAppointmentQuery);
		}
		#endregion

		virtual public PatientDischargeAppointment DetachEntity(PatientDischargeAppointment entity)
		{
			return base.DetachEntity(entity) as PatientDischargeAppointment;
		}

		virtual public PatientDischargeAppointment AttachEntity(PatientDischargeAppointment entity)
		{
			return base.AttachEntity(entity) as PatientDischargeAppointment;
		}

		virtual public void Combine(PatientDischargeAppointmentCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientDischargeAppointment this[int index]
		{
			get
			{
				return base[index] as PatientDischargeAppointment;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientDischargeAppointment);
		}
	}

	[Serializable]
	abstract public class esPatientDischargeAppointment : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientDischargeAppointmentQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientDischargeAppointment()
		{
		}

		public esPatientDischargeAppointment(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String paramedicID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, paramedicID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String paramedicID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, paramedicID);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, String paramedicID)
		{
			esPatientDischargeAppointmentQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.ParamedicID == paramedicID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String paramedicID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("ParamedicID", paramedicID);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "AppointmentDate": this.str.AppointmentDate = (string)value; break;
						case "AppointmentTime": this.str.AppointmentTime = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "RoomID": this.str.RoomID = (string)value; break;
						case "QueNo": this.str.QueNo = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsProcessed": this.str.IsProcessed = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "AppointmentDate":

							if (value == null || value is System.DateTime)
								this.AppointmentDate = (System.DateTime?)value;
							break;
						case "IsProcessed":

							if (value == null || value is System.Boolean)
								this.IsProcessed = (System.Boolean?)value;
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
		/// Maps to PatientDischargeAppointment.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.AppointmentDate
		/// </summary>
		virtual public System.DateTime? AppointmentDate
		{
			get
			{
				return base.GetSystemDateTime(PatientDischargeAppointmentMetadata.ColumnNames.AppointmentDate);
			}

			set
			{
				base.SetSystemDateTime(PatientDischargeAppointmentMetadata.ColumnNames.AppointmentDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.AppointmentTime
		/// </summary>
		virtual public System.String AppointmentTime
		{
			get
			{
				return base.GetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.AppointmentTime);
			}

			set
			{
				base.SetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.AppointmentTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.RoomID
		/// </summary>
		virtual public System.String RoomID
		{
			get
			{
				return base.GetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.RoomID);
			}

			set
			{
				base.SetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.RoomID, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.QueNo
		/// </summary>
		virtual public System.String QueNo
		{
			get
			{
				return base.GetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.QueNo);
			}

			set
			{
				base.SetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.QueNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.IsProcessed
		/// </summary>
		virtual public System.Boolean? IsProcessed
		{
			get
			{
				return base.GetSystemBoolean(PatientDischargeAppointmentMetadata.ColumnNames.IsProcessed);
			}

			set
			{
				base.SetSystemBoolean(PatientDischargeAppointmentMetadata.ColumnNames.IsProcessed, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientDischargeAppointmentMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientDischargeAppointmentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientDischargeAppointment.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PatientDischargeAppointmentMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPatientDischargeAppointment entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String AppointmentDate
			{
				get
				{
					System.DateTime? data = entity.AppointmentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentDate = null;
					else entity.AppointmentDate = Convert.ToDateTime(value);
				}
			}
			public System.String AppointmentTime
			{
				get
				{
					System.String data = entity.AppointmentTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentTime = null;
					else entity.AppointmentTime = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String RoomID
			{
				get
				{
					System.String data = entity.RoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RoomID = null;
					else entity.RoomID = Convert.ToString(value);
				}
			}
			public System.String QueNo
			{
				get
				{
					System.String data = entity.QueNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QueNo = null;
					else entity.QueNo = Convert.ToString(value);
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
			public System.String IsProcessed
			{
				get
				{
					System.Boolean? data = entity.IsProcessed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProcessed = null;
					else entity.IsProcessed = Convert.ToBoolean(value);
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
			private esPatientDischargeAppointment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientDischargeAppointmentQuery query)
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
				throw new Exception("esPatientDischargeAppointment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientDischargeAppointment : esPatientDischargeAppointment
	{
	}

	[Serializable]
	abstract public class esPatientDischargeAppointmentQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientDischargeAppointmentMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem AppointmentDate
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.AppointmentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem AppointmentTime
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.AppointmentTime, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem RoomID
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.RoomID, esSystemType.String);
			}
		}

		public esQueryItem QueNo
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.QueNo, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsProcessed
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.IsProcessed, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientDischargeAppointmentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientDischargeAppointmentCollection")]
	public partial class PatientDischargeAppointmentCollection : esPatientDischargeAppointmentCollection, IEnumerable<PatientDischargeAppointment>
	{
		public PatientDischargeAppointmentCollection()
		{

		}

		public static implicit operator List<PatientDischargeAppointment>(PatientDischargeAppointmentCollection coll)
		{
			List<PatientDischargeAppointment> list = new List<PatientDischargeAppointment>();

			foreach (PatientDischargeAppointment emp in coll)
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
				return PatientDischargeAppointmentMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientDischargeAppointmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientDischargeAppointment(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientDischargeAppointment();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientDischargeAppointmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientDischargeAppointmentQuery();
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
		public bool Load(PatientDischargeAppointmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientDischargeAppointment AddNew()
		{
			PatientDischargeAppointment entity = base.AddNewEntity() as PatientDischargeAppointment;

			return entity;
		}
		public PatientDischargeAppointment FindByPrimaryKey(String registrationNo, String paramedicID)
		{
			return base.FindByPrimaryKey(registrationNo, paramedicID) as PatientDischargeAppointment;
		}

		#region IEnumerable< PatientDischargeAppointment> Members

		IEnumerator<PatientDischargeAppointment> IEnumerable<PatientDischargeAppointment>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientDischargeAppointment;
			}
		}

		#endregion

		private PatientDischargeAppointmentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientDischargeAppointment' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientDischargeAppointment ({RegistrationNo, ParamedicID})")]
	[Serializable]
	public partial class PatientDischargeAppointment : esPatientDischargeAppointment
	{
		public PatientDischargeAppointment()
		{
		}

		public PatientDischargeAppointment(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientDischargeAppointmentMetadata.Meta();
			}
		}

		override protected esPatientDischargeAppointmentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientDischargeAppointmentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientDischargeAppointmentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientDischargeAppointmentQuery();
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
		public bool Load(PatientDischargeAppointmentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientDischargeAppointmentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientDischargeAppointmentQuery : esPatientDischargeAppointmentQuery
	{
		public PatientDischargeAppointmentQuery()
		{

		}

		public PatientDischargeAppointmentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientDischargeAppointmentQuery";
		}
	}

	[Serializable]
	public partial class PatientDischargeAppointmentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientDischargeAppointmentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.AppointmentDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.AppointmentDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.AppointmentTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.AppointmentTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.ServiceUnitID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.RoomID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.RoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.QueNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.QueNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.IsProcessed, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.IsProcessed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientDischargeAppointmentMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientDischargeAppointmentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PatientDischargeAppointmentMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string ParamedicID = "ParamedicID";
			public const string AppointmentDate = "AppointmentDate";
			public const string AppointmentTime = "AppointmentTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string QueNo = "QueNo";
			public const string Notes = "Notes";
			public const string IsProcessed = "IsProcessed";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string ParamedicID = "ParamedicID";
			public const string AppointmentDate = "AppointmentDate";
			public const string AppointmentTime = "AppointmentTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string RoomID = "RoomID";
			public const string QueNo = "QueNo";
			public const string Notes = "Notes";
			public const string IsProcessed = "IsProcessed";
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
			lock (typeof(PatientDischargeAppointmentMetadata))
			{
				if (PatientDischargeAppointmentMetadata.mapDelegates == null)
				{
					PatientDischargeAppointmentMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientDischargeAppointmentMetadata.meta == null)
				{
					PatientDischargeAppointmentMetadata.meta = new PatientDischargeAppointmentMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AppointmentDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("AppointmentTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QueNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsProcessed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PatientDischargeAppointment";
				meta.Destination = "PatientDischargeAppointment";
				meta.spInsert = "proc_PatientDischargeAppointmentInsert";
				meta.spUpdate = "proc_PatientDischargeAppointmentUpdate";
				meta.spDelete = "proc_PatientDischargeAppointmentDelete";
				meta.spLoadAll = "proc_PatientDischargeAppointmentLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientDischargeAppointmentLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientDischargeAppointmentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
