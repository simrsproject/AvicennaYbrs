/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/9/2023 1:30:19 PM
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
	abstract public class esCredentialInvitationTeamCollection : esEntityCollectionWAuditLog
	{
		public esCredentialInvitationTeamCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialInvitationTeamCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialInvitationTeamQuery query)
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
			this.InitQuery(query as esCredentialInvitationTeamQuery);
		}
		#endregion

		virtual public CredentialInvitationTeam DetachEntity(CredentialInvitationTeam entity)
		{
			return base.DetachEntity(entity) as CredentialInvitationTeam;
		}

		virtual public CredentialInvitationTeam AttachEntity(CredentialInvitationTeam entity)
		{
			return base.AttachEntity(entity) as CredentialInvitationTeam;
		}

		virtual public void Combine(CredentialInvitationTeamCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialInvitationTeam this[int index]
		{
			get
			{
				return base[index] as CredentialInvitationTeam;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialInvitationTeam);
		}
	}

	[Serializable]
	abstract public class esCredentialInvitationTeam : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialInvitationTeamQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialInvitationTeam()
		{
		}

		public esCredentialInvitationTeam(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String invitationNo, Int32 personID, String sRCredentialingTeamPosition)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invitationNo, personID, sRCredentialingTeamPosition);
			else
				return LoadByPrimaryKeyStoredProcedure(invitationNo, personID, sRCredentialingTeamPosition);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String invitationNo, Int32 personID, String sRCredentialingTeamPosition)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(invitationNo, personID, sRCredentialingTeamPosition);
			else
				return LoadByPrimaryKeyStoredProcedure(invitationNo, personID, sRCredentialingTeamPosition);
		}

		private bool LoadByPrimaryKeyDynamic(String invitationNo, Int32 personID, String sRCredentialingTeamPosition)
		{
			esCredentialInvitationTeamQuery query = this.GetDynamicQuery();
			query.Where(query.InvitationNo == invitationNo, query.PersonID == personID, query.SRCredentialingTeamPosition == sRCredentialingTeamPosition);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String invitationNo, Int32 personID, String sRCredentialingTeamPosition)
		{
			esParameters parms = new esParameters();
			parms.Add("InvitationNo", invitationNo);
			parms.Add("PersonID", personID);
			parms.Add("SRCredentialingTeamPosition", sRCredentialingTeamPosition);
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
						case "InvitationNo": this.str.InvitationNo = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRCredentialingTeamPosition": this.str.SRCredentialingTeamPosition = (string)value; break;
						case "InvitationLetterNo": this.str.InvitationLetterNo = (string)value; break;
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
		/// Maps to CredentialInvitationTeam.InvitationNo
		/// </summary>
		virtual public System.String InvitationNo
		{
			get
			{
				return base.GetSystemString(CredentialInvitationTeamMetadata.ColumnNames.InvitationNo);
			}

			set
			{
				base.SetSystemString(CredentialInvitationTeamMetadata.ColumnNames.InvitationNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitationTeam.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(CredentialInvitationTeamMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(CredentialInvitationTeamMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitationTeam.SRCredentialingTeamPosition
		/// </summary>
		virtual public System.String SRCredentialingTeamPosition
		{
			get
			{
				return base.GetSystemString(CredentialInvitationTeamMetadata.ColumnNames.SRCredentialingTeamPosition);
			}

			set
			{
				base.SetSystemString(CredentialInvitationTeamMetadata.ColumnNames.SRCredentialingTeamPosition, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitationTeam.InvitationLetterNo
		/// </summary>
		virtual public System.String InvitationLetterNo
		{
			get
			{
				return base.GetSystemString(CredentialInvitationTeamMetadata.ColumnNames.InvitationLetterNo);
			}

			set
			{
				base.SetSystemString(CredentialInvitationTeamMetadata.ColumnNames.InvitationLetterNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitationTeam.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialInvitationTeamMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialInvitationTeamMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialInvitationTeam.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialInvitationTeamMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialInvitationTeamMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialInvitationTeam entity)
			{
				this.entity = entity;
			}
			public System.String InvitationNo
			{
				get
				{
					System.String data = entity.InvitationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InvitationNo = null;
					else entity.InvitationNo = Convert.ToString(value);
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
			private esCredentialInvitationTeam entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialInvitationTeamQuery query)
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
				throw new Exception("esCredentialInvitationTeam can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialInvitationTeam : esCredentialInvitationTeam
	{
	}

	[Serializable]
	abstract public class esCredentialInvitationTeamQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialInvitationTeamMetadata.Meta();
			}
		}

		public esQueryItem InvitationNo
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationTeamMetadata.ColumnNames.InvitationNo, esSystemType.String);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationTeamMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRCredentialingTeamPosition
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationTeamMetadata.ColumnNames.SRCredentialingTeamPosition, esSystemType.String);
			}
		}

		public esQueryItem InvitationLetterNo
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationTeamMetadata.ColumnNames.InvitationLetterNo, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationTeamMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialInvitationTeamMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialInvitationTeamCollection")]
	public partial class CredentialInvitationTeamCollection : esCredentialInvitationTeamCollection, IEnumerable<CredentialInvitationTeam>
	{
		public CredentialInvitationTeamCollection()
		{

		}

		public static implicit operator List<CredentialInvitationTeam>(CredentialInvitationTeamCollection coll)
		{
			List<CredentialInvitationTeam> list = new List<CredentialInvitationTeam>();

			foreach (CredentialInvitationTeam emp in coll)
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
				return CredentialInvitationTeamMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialInvitationTeamQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialInvitationTeam(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialInvitationTeam();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialInvitationTeamQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialInvitationTeamQuery();
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
		public bool Load(CredentialInvitationTeamQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialInvitationTeam AddNew()
		{
			CredentialInvitationTeam entity = base.AddNewEntity() as CredentialInvitationTeam;

			return entity;
		}
		public CredentialInvitationTeam FindByPrimaryKey(String invitationNo, Int32 personID, String sRCredentialingTeamPosition)
		{
			return base.FindByPrimaryKey(invitationNo, personID, sRCredentialingTeamPosition) as CredentialInvitationTeam;
		}

		#region IEnumerable< CredentialInvitationTeam> Members

		IEnumerator<CredentialInvitationTeam> IEnumerable<CredentialInvitationTeam>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialInvitationTeam;
			}
		}

		#endregion

		private CredentialInvitationTeamQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialInvitationTeam' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialInvitationTeam ({InvitationNo, PersonID, SRCredentialingTeamPosition})")]
	[Serializable]
	public partial class CredentialInvitationTeam : esCredentialInvitationTeam
	{
		public CredentialInvitationTeam()
		{
		}

		public CredentialInvitationTeam(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialInvitationTeamMetadata.Meta();
			}
		}

		override protected esCredentialInvitationTeamQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialInvitationTeamQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialInvitationTeamQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialInvitationTeamQuery();
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
		public bool Load(CredentialInvitationTeamQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialInvitationTeamQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialInvitationTeamQuery : esCredentialInvitationTeamQuery
	{
		public CredentialInvitationTeamQuery()
		{

		}

		public CredentialInvitationTeamQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialInvitationTeamQuery";
		}
	}

	[Serializable]
	public partial class CredentialInvitationTeamMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialInvitationTeamMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialInvitationTeamMetadata.ColumnNames.InvitationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationTeamMetadata.PropertyNames.InvitationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationTeamMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CredentialInvitationTeamMetadata.PropertyNames.PersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationTeamMetadata.ColumnNames.SRCredentialingTeamPosition, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationTeamMetadata.PropertyNames.SRCredentialingTeamPosition;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationTeamMetadata.ColumnNames.InvitationLetterNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationTeamMetadata.PropertyNames.InvitationLetterNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationTeamMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialInvitationTeamMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialInvitationTeamMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialInvitationTeamMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialInvitationTeamMetadata Meta()
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
			public const string InvitationNo = "InvitationNo";
			public const string PersonID = "PersonID";
			public const string SRCredentialingTeamPosition = "SRCredentialingTeamPosition";
			public const string InvitationLetterNo = "InvitationLetterNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string InvitationNo = "InvitationNo";
			public const string PersonID = "PersonID";
			public const string SRCredentialingTeamPosition = "SRCredentialingTeamPosition";
			public const string InvitationLetterNo = "InvitationLetterNo";
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
			lock (typeof(CredentialInvitationTeamMetadata))
			{
				if (CredentialInvitationTeamMetadata.mapDelegates == null)
				{
					CredentialInvitationTeamMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialInvitationTeamMetadata.meta == null)
				{
					CredentialInvitationTeamMetadata.meta = new CredentialInvitationTeamMetadata();
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

				meta.AddTypeMap("InvitationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRCredentialingTeamPosition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InvitationLetterNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialInvitationTeam";
				meta.Destination = "CredentialInvitationTeam";
				meta.spInsert = "proc_CredentialInvitationTeamInsert";
				meta.spUpdate = "proc_CredentialInvitationTeamUpdate";
				meta.spDelete = "proc_CredentialInvitationTeamDelete";
				meta.spLoadAll = "proc_CredentialInvitationTeamLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialInvitationTeamLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialInvitationTeamMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
