/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/27/2023 7:24:13 PM
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
	abstract public class esRegistrationPatientRiskColorHistoryCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationPatientRiskColorHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationPatientRiskColorHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationPatientRiskColorHistoryQuery query)
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
			this.InitQuery(query as esRegistrationPatientRiskColorHistoryQuery);
		}
		#endregion

		virtual public RegistrationPatientRiskColorHistory DetachEntity(RegistrationPatientRiskColorHistory entity)
		{
			return base.DetachEntity(entity) as RegistrationPatientRiskColorHistory;
		}

		virtual public RegistrationPatientRiskColorHistory AttachEntity(RegistrationPatientRiskColorHistory entity)
		{
			return base.AttachEntity(entity) as RegistrationPatientRiskColorHistory;
		}

		virtual public void Combine(RegistrationPatientRiskColorHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationPatientRiskColorHistory this[int index]
		{
			get
			{
				return base[index] as RegistrationPatientRiskColorHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationPatientRiskColorHistory);
		}
	}

	[Serializable]
	abstract public class esRegistrationPatientRiskColorHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationPatientRiskColorHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationPatientRiskColorHistory()
		{
		}

		public esRegistrationPatientRiskColorHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String fromSRPatientRiskColor, String toSRPatientRiskColor, DateTime lastUpdateDateTime)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, fromSRPatientRiskColor, toSRPatientRiskColor, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, fromSRPatientRiskColor, toSRPatientRiskColor, lastUpdateDateTime);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String fromSRPatientRiskColor, String toSRPatientRiskColor, DateTime lastUpdateDateTime)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, fromSRPatientRiskColor, toSRPatientRiskColor, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, fromSRPatientRiskColor, toSRPatientRiskColor, lastUpdateDateTime);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, String fromSRPatientRiskColor, String toSRPatientRiskColor, DateTime lastUpdateDateTime)
		{
			esRegistrationPatientRiskColorHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.FromSRPatientRiskColor == fromSRPatientRiskColor, query.ToSRPatientRiskColor == toSRPatientRiskColor, query.LastUpdateDateTime == lastUpdateDateTime);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String fromSRPatientRiskColor, String toSRPatientRiskColor, DateTime lastUpdateDateTime)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("FromSRPatientRiskColor", fromSRPatientRiskColor);
			parms.Add("ToSRPatientRiskColor", toSRPatientRiskColor);
			parms.Add("LastUpdateDateTime", lastUpdateDateTime);
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
						case "FromSRPatientRiskColor": this.str.FromSRPatientRiskColor = (string)value; break;
						case "ToSRPatientRiskColor": this.str.ToSRPatientRiskColor = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
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
		/// Maps to RegistrationPatientRiskColorHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
        /// <summary>
        /// Maps to RegistrationPatientRiskColorHistory.FromSRPatientRiskColor
        /// </summary>
        virtual public System.String FromSRPatientRiskColor
		{
			get
			{
				return base.GetSystemString(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.FromSRPatientRiskColor);
			}

			set
			{
				base.SetSystemString(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.FromSRPatientRiskColor, value);
			}
		}
        /// <summary>
        /// Maps to RegistrationPatientRiskColorHistory.ToSRPatientRiskColor
        /// </summary>
        virtual public System.String ToSRPatientRiskColor
		{
			get
			{
				return base.GetSystemString(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.ToSRPatientRiskColor);
			}

			set
			{
				base.SetSystemString(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.ToSRPatientRiskColor, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPatientRiskColorHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPatientRiskColorHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationPatientRiskColorHistory entity)
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
			public System.String FromSRPatientRiskColor
			{
				get
				{
					System.String data = entity.FromSRPatientRiskColor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromSRPatientRiskColor = null;
					else entity.FromSRPatientRiskColor = Convert.ToString(value);
				}
			}
			public System.String ToSRPatientRiskColor
			{
				get
				{
					System.String data = entity.ToSRPatientRiskColor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToSRPatientRiskColor = null;
					else entity.ToSRPatientRiskColor = Convert.ToString(value);
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
			private esRegistrationPatientRiskColorHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationPatientRiskColorHistoryQuery query)
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
				throw new Exception("esRegistrationPatientRiskColorHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationPatientRiskColorHistory : esRegistrationPatientRiskColorHistory
	{
	}

	[Serializable]
	abstract public class esRegistrationPatientRiskColorHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationPatientRiskColorHistoryMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskColorHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem FromSRPatientRiskColor
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskColorHistoryMetadata.ColumnNames.FromSRPatientRiskColor, esSystemType.String);
			}
		}

		public esQueryItem ToSRPatientRiskColor
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskColorHistoryMetadata.ColumnNames.ToSRPatientRiskColor, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskColorHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskColorHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationPatientRiskColorHistoryCollection")]
	public partial class RegistrationPatientRiskColorHistoryCollection : esRegistrationPatientRiskColorHistoryCollection, IEnumerable<RegistrationPatientRiskColorHistory>
	{
		public RegistrationPatientRiskColorHistoryCollection()
		{

		}

		public static implicit operator List<RegistrationPatientRiskColorHistory>(RegistrationPatientRiskColorHistoryCollection coll)
		{
			List<RegistrationPatientRiskColorHistory> list = new List<RegistrationPatientRiskColorHistory>();

			foreach (RegistrationPatientRiskColorHistory emp in coll)
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
				return RegistrationPatientRiskColorHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationPatientRiskColorHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationPatientRiskColorHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationPatientRiskColorHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationPatientRiskColorHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationPatientRiskColorHistoryQuery();
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
		public bool Load(RegistrationPatientRiskColorHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationPatientRiskColorHistory AddNew()
		{
			RegistrationPatientRiskColorHistory entity = base.AddNewEntity() as RegistrationPatientRiskColorHistory;

			return entity;
		}
		public RegistrationPatientRiskColorHistory FindByPrimaryKey(String registrationNo, String fromSRPatientRiskColor, String toSRPatientRiskColor, DateTime lastUpdateDateTime)
		{
			return base.FindByPrimaryKey(registrationNo, fromSRPatientRiskColor, toSRPatientRiskColor, lastUpdateDateTime) as RegistrationPatientRiskColorHistory;
		}

		#region IEnumerable< RegistrationPatientRiskColorHistory> Members

		IEnumerator<RegistrationPatientRiskColorHistory> IEnumerable<RegistrationPatientRiskColorHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationPatientRiskColorHistory;
			}
		}

		#endregion

		private RegistrationPatientRiskColorHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationPatientRiskColorHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationPatientRiskColorHistory ({RegistrationNo, fromSRPatientRiskColor, toSRPatientRiskColor, LastUpdateDateTime})")]
	[Serializable]
	public partial class RegistrationPatientRiskColorHistory : esRegistrationPatientRiskColorHistory
	{
		public RegistrationPatientRiskColorHistory()
		{
		}

		public RegistrationPatientRiskColorHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationPatientRiskColorHistoryMetadata.Meta();
			}
		}

		override protected esRegistrationPatientRiskColorHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationPatientRiskColorHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationPatientRiskColorHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationPatientRiskColorHistoryQuery();
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
		public bool Load(RegistrationPatientRiskColorHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationPatientRiskColorHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationPatientRiskColorHistoryQuery : esRegistrationPatientRiskColorHistoryQuery
	{
		public RegistrationPatientRiskColorHistoryQuery()
		{

		}

		public RegistrationPatientRiskColorHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationPatientRiskColorHistoryQuery";
		}
	}

	[Serializable]
	public partial class RegistrationPatientRiskColorHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationPatientRiskColorHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPatientRiskColorHistoryMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.FromSRPatientRiskColor, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPatientRiskColorHistoryMetadata.PropertyNames.FromSRPatientRiskColor;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.ToSRPatientRiskColor, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPatientRiskColorHistoryMetadata.PropertyNames.ToSRPatientRiskColor;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationPatientRiskColorHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationPatientRiskColorHistoryMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPatientRiskColorHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationPatientRiskColorHistoryMetadata Meta()
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
			public const string FromSRPatientRiskColor = "FromSRPatientRiskColor";
			public const string ToSRPatientRiskColor = "ToSRPatientRiskColor";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string FromSRPatientRiskColor = "FromSRPatientRiskColor";
			public const string ToSRPatientRiskColor = "ToSRPatientRiskColor";
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
			lock (typeof(RegistrationPatientRiskColorHistoryMetadata))
			{
				if (RegistrationPatientRiskColorHistoryMetadata.mapDelegates == null)
				{
					RegistrationPatientRiskColorHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationPatientRiskColorHistoryMetadata.meta == null)
				{
					RegistrationPatientRiskColorHistoryMetadata.meta = new RegistrationPatientRiskColorHistoryMetadata();
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
				meta.AddTypeMap("FromSRPatientRiskColor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToSRPatientRiskColor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "RegistrationPatientRiskColorHistory";
				meta.Destination = "RegistrationPatientRiskColorHistory";
				meta.spInsert = "proc_RegistrationPatientRiskColorHistoryInsert";
				meta.spUpdate = "proc_RegistrationPatientRiskColorHistoryUpdate";
				meta.spDelete = "proc_RegistrationPatientRiskColorHistoryDelete";
				meta.spLoadAll = "proc_RegistrationPatientRiskColorHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationPatientRiskColorHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationPatientRiskColorHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
