/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 2:16:32 PM
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
	abstract public class esCredentialProcessTeamCollection : esEntityCollectionWAuditLog
	{
		public esCredentialProcessTeamCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialProcessTeamCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialProcessTeamQuery query)
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
			this.InitQuery(query as esCredentialProcessTeamQuery);
		}
		#endregion

		virtual public CredentialProcessTeam DetachEntity(CredentialProcessTeam entity)
		{
			return base.DetachEntity(entity) as CredentialProcessTeam;
		}

		virtual public CredentialProcessTeam AttachEntity(CredentialProcessTeam entity)
		{
			return base.AttachEntity(entity) as CredentialProcessTeam;
		}

		virtual public void Combine(CredentialProcessTeamCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialProcessTeam this[int index]
		{
			get
			{
				return base[index] as CredentialProcessTeam;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialProcessTeam);
		}
	}

	[Serializable]
	abstract public class esCredentialProcessTeam : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialProcessTeamQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialProcessTeam()
		{
		}

		public esCredentialProcessTeam(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, Int32 personID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, personID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, personID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, Int32 personID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, personID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, personID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, Int32 personID)
		{
			esCredentialProcessTeamQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.PersonID == personID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, Int32 personID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("PersonID", personID);
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
						case "PersonID": this.str.PersonID = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "SRCredentialingTeamPosition": this.str.SRCredentialingTeamPosition = (string)value; break;
						case "AreasOfExpertise": this.str.AreasOfExpertise = (string)value; break;
						case "InvitationLetterNo": this.str.InvitationLetterNo = (string)value; break;
						case "MinutesOfMeeting": this.str.MinutesOfMeeting = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "PositionID":

							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
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
		/// Maps to CredentialProcessTeam.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessTeamMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessTeamMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessTeam.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessTeamMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessTeamMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessTeam.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(CredentialProcessTeamMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(CredentialProcessTeamMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessTeam.SRCredentialingTeamPosition
		/// </summary>
		virtual public System.String SRCredentialingTeamPosition
		{
			get
			{
				return base.GetSystemString(CredentialProcessTeamMetadata.ColumnNames.SRCredentialingTeamPosition);
			}

			set
			{
				base.SetSystemString(CredentialProcessTeamMetadata.ColumnNames.SRCredentialingTeamPosition, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessTeam.AreasOfExpertise
		/// </summary>
		virtual public System.String AreasOfExpertise
		{
			get
			{
				return base.GetSystemString(CredentialProcessTeamMetadata.ColumnNames.AreasOfExpertise);
			}

			set
			{
				base.SetSystemString(CredentialProcessTeamMetadata.ColumnNames.AreasOfExpertise, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessTeam.InvitationLetterNo
		/// </summary>
		virtual public System.String InvitationLetterNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessTeamMetadata.ColumnNames.InvitationLetterNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessTeamMetadata.ColumnNames.InvitationLetterNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessTeam.MinutesOfMeeting
		/// </summary>
		virtual public System.String MinutesOfMeeting
		{
			get
			{
				return base.GetSystemString(CredentialProcessTeamMetadata.ColumnNames.MinutesOfMeeting);
			}

			set
			{
				base.SetSystemString(CredentialProcessTeamMetadata.ColumnNames.MinutesOfMeeting, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessTeam.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessTeamMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessTeamMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessTeam.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessTeamMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessTeamMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialProcessTeam entity)
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
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
			public System.String SRCredentialingTeamPosition
			{
				get
				{
					System.String data = entity.SRCredentialingTeamPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCredentialingTeamPosition = null;
					else entity.SRCredentialingTeamPosition = Convert.ToString(value);
				}
			}
			public System.String AreasOfExpertise
			{
				get
				{
					System.String data = entity.AreasOfExpertise;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AreasOfExpertise = null;
					else entity.AreasOfExpertise = Convert.ToString(value);
				}
			}
			public System.String InvitationLetterNo
			{
				get
				{
					System.String data = entity.InvitationLetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvitationLetterNo = null;
					else entity.InvitationLetterNo = Convert.ToString(value);
				}
			}
			public System.String MinutesOfMeeting
			{
				get
				{
					System.String data = entity.MinutesOfMeeting;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinutesOfMeeting = null;
					else entity.MinutesOfMeeting = Convert.ToString(value);
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
			private esCredentialProcessTeam entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialProcessTeamQuery query)
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
				throw new Exception("esCredentialProcessTeam can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialProcessTeam : esCredentialProcessTeam
	{
	}

	[Serializable]
	abstract public class esCredentialProcessTeamQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessTeamMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem SRCredentialingTeamPosition
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.SRCredentialingTeamPosition, esSystemType.String);
			}
		}

		public esQueryItem AreasOfExpertise
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.AreasOfExpertise, esSystemType.String);
			}
		}

		public esQueryItem InvitationLetterNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.InvitationLetterNo, esSystemType.String);
			}
		}

		public esQueryItem MinutesOfMeeting
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.MinutesOfMeeting, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessTeamMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialProcessTeamCollection")]
	public partial class CredentialProcessTeamCollection : esCredentialProcessTeamCollection, IEnumerable<CredentialProcessTeam>
	{
		public CredentialProcessTeamCollection()
		{

		}

		public static implicit operator List<CredentialProcessTeam>(CredentialProcessTeamCollection coll)
		{
			List<CredentialProcessTeam> list = new List<CredentialProcessTeam>();

			foreach (CredentialProcessTeam emp in coll)
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
				return CredentialProcessTeamMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessTeamQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialProcessTeam(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialProcessTeam();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessTeamQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessTeamQuery();
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
		public bool Load(CredentialProcessTeamQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialProcessTeam AddNew()
		{
			CredentialProcessTeam entity = base.AddNewEntity() as CredentialProcessTeam;

			return entity;
		}
		public CredentialProcessTeam FindByPrimaryKey(String transactionNo, Int32 personID)
		{
			return base.FindByPrimaryKey(transactionNo, personID) as CredentialProcessTeam;
		}

		#region IEnumerable< CredentialProcessTeam> Members

		IEnumerator<CredentialProcessTeam> IEnumerable<CredentialProcessTeam>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialProcessTeam;
			}
		}

		#endregion

		private CredentialProcessTeamQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialProcessTeam' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialProcessTeam ({TransactionNo, PersonID})")]
	[Serializable]
	public partial class CredentialProcessTeam : esCredentialProcessTeam
	{
		public CredentialProcessTeam()
		{
		}

		public CredentialProcessTeam(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessTeamMetadata.Meta();
			}
		}

		override protected esCredentialProcessTeamQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessTeamQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessTeamQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessTeamQuery();
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
		public bool Load(CredentialProcessTeamQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialProcessTeamQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialProcessTeamQuery : esCredentialProcessTeamQuery
	{
		public CredentialProcessTeamQuery()
		{

		}

		public CredentialProcessTeamQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialProcessTeamQuery";
		}
	}

	[Serializable]
	public partial class CredentialProcessTeamMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialProcessTeamMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.PersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.PositionID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.SRCredentialingTeamPosition, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.SRCredentialingTeamPosition;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.AreasOfExpertise, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.AreasOfExpertise;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.InvitationLetterNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.InvitationLetterNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.MinutesOfMeeting, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.MinutesOfMeeting;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessTeamMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessTeamMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialProcessTeamMetadata Meta()
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
			public const string PersonID = "PersonID";
			public const string PositionID = "PositionID";
			public const string SRCredentialingTeamPosition = "SRCredentialingTeamPosition";
			public const string AreasOfExpertise = "AreasOfExpertise";
			public const string InvitationLetterNo = "InvitationLetterNo";
			public const string MinutesOfMeeting = "MinutesOfMeeting";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string PersonID = "PersonID";
			public const string PositionID = "PositionID";
			public const string SRCredentialingTeamPosition = "SRCredentialingTeamPosition";
			public const string AreasOfExpertise = "AreasOfExpertise";
			public const string InvitationLetterNo = "InvitationLetterNo";
			public const string MinutesOfMeeting = "MinutesOfMeeting";
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
			lock (typeof(CredentialProcessTeamMetadata))
			{
				if (CredentialProcessTeamMetadata.mapDelegates == null)
				{
					CredentialProcessTeamMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialProcessTeamMetadata.meta == null)
				{
					CredentialProcessTeamMetadata.meta = new CredentialProcessTeamMetadata();
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
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRCredentialingTeamPosition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AreasOfExpertise", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvitationLetterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinutesOfMeeting", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialProcessTeam";
				meta.Destination = "CredentialProcessTeam";
				meta.spInsert = "proc_CredentialProcessTeamInsert";
				meta.spUpdate = "proc_CredentialProcessTeamUpdate";
				meta.spDelete = "proc_CredentialProcessTeamDelete";
				meta.spLoadAll = "proc_CredentialProcessTeamLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialProcessTeamLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialProcessTeamMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
