/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/15/2022 2:08:31 PM
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
	abstract public class esEmployeeSafetyCultureIncidentReportsMeetingParticipantCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeSafetyCultureIncidentReportsMeetingParticipantCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsMeetingParticipantQuery query)
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
			this.InitQuery(query as esEmployeeSafetyCultureIncidentReportsMeetingParticipantQuery);
		}
		#endregion

		virtual public EmployeeSafetyCultureIncidentReportsMeetingParticipant DetachEntity(EmployeeSafetyCultureIncidentReportsMeetingParticipant entity)
		{
			return base.DetachEntity(entity) as EmployeeSafetyCultureIncidentReportsMeetingParticipant;
		}

		virtual public EmployeeSafetyCultureIncidentReportsMeetingParticipant AttachEntity(EmployeeSafetyCultureIncidentReportsMeetingParticipant entity)
		{
			return base.AttachEntity(entity) as EmployeeSafetyCultureIncidentReportsMeetingParticipant;
		}

		virtual public void Combine(EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeSafetyCultureIncidentReportsMeetingParticipant this[int index]
		{
			get
			{
				return base[index] as EmployeeSafetyCultureIncidentReportsMeetingParticipant;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeSafetyCultureIncidentReportsMeetingParticipant);
		}
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsMeetingParticipant : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeSafetyCultureIncidentReportsMeetingParticipantQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeSafetyCultureIncidentReportsMeetingParticipant()
		{
		}

		public esEmployeeSafetyCultureIncidentReportsMeetingParticipant(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String sequenceNo, Int32 participantPersonID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, participantPersonID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, participantPersonID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String sequenceNo, Int32 participantPersonID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo, participantPersonID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo, participantPersonID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String sequenceNo, Int32 participantPersonID)
		{
			esEmployeeSafetyCultureIncidentReportsMeetingParticipantQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo, query.ParticipantPersonID == participantPersonID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String sequenceNo, Int32 participantPersonID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("SequenceNo", sequenceNo);
			parms.Add("ParticipantPersonID", participantPersonID);
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
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ParticipantPersonID": this.str.ParticipantPersonID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ParticipantPersonID":

							if (value == null || value is System.Int32)
								this.ParticipantPersonID = (System.Int32?)value;
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
		/// Maps to EmployeeSafetyCultureIncidentReportsMeetingParticipant.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsMeetingParticipant.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.SequenceNo);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsMeetingParticipant.ParticipantPersonID
		/// </summary>
		virtual public System.Int32? ParticipantPersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.ParticipantPersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.ParticipantPersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsMeetingParticipant.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeSafetyCultureIncidentReportsMeetingParticipant.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeSafetyCultureIncidentReportsMeetingParticipant entity)
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
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String ParticipantPersonID
			{
				get
				{
					System.Int32? data = entity.ParticipantPersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantPersonID = null;
					else entity.ParticipantPersonID = Convert.ToInt32(value);
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
			private esEmployeeSafetyCultureIncidentReportsMeetingParticipant entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeSafetyCultureIncidentReportsMeetingParticipantQuery query)
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
				throw new Exception("esEmployeeSafetyCultureIncidentReportsMeetingParticipant can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeSafetyCultureIncidentReportsMeetingParticipant : esEmployeeSafetyCultureIncidentReportsMeetingParticipant
	{
	}

	[Serializable]
	abstract public class esEmployeeSafetyCultureIncidentReportsMeetingParticipantQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		}

		public esQueryItem ParticipantPersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.ParticipantPersonID, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection")]
	public partial class EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection : esEmployeeSafetyCultureIncidentReportsMeetingParticipantCollection, IEnumerable<EmployeeSafetyCultureIncidentReportsMeetingParticipant>
	{
		public EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection()
		{

		}

		public static implicit operator List<EmployeeSafetyCultureIncidentReportsMeetingParticipant>(EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection coll)
		{
			List<EmployeeSafetyCultureIncidentReportsMeetingParticipant> list = new List<EmployeeSafetyCultureIncidentReportsMeetingParticipant>();

			foreach (EmployeeSafetyCultureIncidentReportsMeetingParticipant emp in coll)
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
				return EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeSafetyCultureIncidentReportsMeetingParticipant(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeSafetyCultureIncidentReportsMeetingParticipant();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeSafetyCultureIncidentReportsMeetingParticipant AddNew()
		{
			EmployeeSafetyCultureIncidentReportsMeetingParticipant entity = base.AddNewEntity() as EmployeeSafetyCultureIncidentReportsMeetingParticipant;

			return entity;
		}
		public EmployeeSafetyCultureIncidentReportsMeetingParticipant FindByPrimaryKey(String transactionNo, String sequenceNo, Int32 participantPersonID)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo, participantPersonID) as EmployeeSafetyCultureIncidentReportsMeetingParticipant;
		}

		#region IEnumerable< EmployeeSafetyCultureIncidentReportsMeetingParticipant> Members

		IEnumerator<EmployeeSafetyCultureIncidentReportsMeetingParticipant> IEnumerable<EmployeeSafetyCultureIncidentReportsMeetingParticipant>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeSafetyCultureIncidentReportsMeetingParticipant;
			}
		}

		#endregion

		private EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeSafetyCultureIncidentReportsMeetingParticipant' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeSafetyCultureIncidentReportsMeetingParticipant ({TransactionNo, SequenceNo, ParticipantPersonID})")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsMeetingParticipant : esEmployeeSafetyCultureIncidentReportsMeetingParticipant
	{
		public EmployeeSafetyCultureIncidentReportsMeetingParticipant()
		{
		}

		public EmployeeSafetyCultureIncidentReportsMeetingParticipant(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.Meta();
			}
		}

		override protected esEmployeeSafetyCultureIncidentReportsMeetingParticipantQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery();
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
		public bool Load(EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery : esEmployeeSafetyCultureIncidentReportsMeetingParticipantQuery
	{
		public EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery()
		{

		}

		public EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery";
		}
	}

	[Serializable]
	public partial class EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.ParticipantPersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.PropertyNames.ParticipantPersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata Meta()
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
			public const string SequenceNo = "SequenceNo";
			public const string ParticipantPersonID = "ParticipantPersonID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ParticipantPersonID = "ParticipantPersonID";
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
			lock (typeof(EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata))
			{
				if (EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.mapDelegates == null)
				{
					EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.meta == null)
				{
					EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata.meta = new EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata();
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
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParticipantPersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeSafetyCultureIncidentReportsMeetingParticipant";
				meta.Destination = "EmployeeSafetyCultureIncidentReportsMeetingParticipant";
				meta.spInsert = "proc_EmployeeSafetyCultureIncidentReportsMeetingParticipantInsert";
				meta.spUpdate = "proc_EmployeeSafetyCultureIncidentReportsMeetingParticipantUpdate";
				meta.spDelete = "proc_EmployeeSafetyCultureIncidentReportsMeetingParticipantDelete";
				meta.spLoadAll = "proc_EmployeeSafetyCultureIncidentReportsMeetingParticipantLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeSafetyCultureIncidentReportsMeetingParticipantLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeSafetyCultureIncidentReportsMeetingParticipantMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
