/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/30/2022 10:33:52 PM
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
	abstract public class esReferExternalBakCollection : esEntityCollectionWAuditLog
	{
		public esReferExternalBakCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ReferExternalBakCollection";
		}

		#region Query Logic
		protected void InitQuery(esReferExternalBakQuery query)
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
			this.InitQuery(query as esReferExternalBakQuery);
		}
		#endregion

		virtual public ReferExternalBak DetachEntity(ReferExternalBak entity)
		{
			return base.DetachEntity(entity) as ReferExternalBak;
		}

		virtual public ReferExternalBak AttachEntity(ReferExternalBak entity)
		{
			return base.AttachEntity(entity) as ReferExternalBak;
		}

		virtual public void Combine(ReferExternalBakCollection collection)
		{
			base.Combine(collection);
		}

		new public ReferExternalBak this[int index]
		{
			get
			{
				return base[index] as ReferExternalBak;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ReferExternalBak);
		}
	}

	[Serializable]
	abstract public class esReferExternalBak : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esReferExternalBakQuery GetDynamicQuery()
		{
			return null;
		}

		public esReferExternalBak()
		{
		}

		public esReferExternalBak(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo)
		{
			esReferExternalBakQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
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
						case "ReferralID": this.str.ReferralID = (string)value; break;
						case "SRReferReason": this.str.SRReferReason = (string)value; break;
						case "ReferReasonOther": this.str.ReferReasonOther = (string)value; break;
						case "OtherInformation": this.str.OtherInformation = (string)value; break;
						case "ReferralAgreedBy": this.str.ReferralAgreedBy = (string)value; break;
						case "ReferralAgreedTime": this.str.ReferralAgreedTime = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReferralAgreedTime":

							if (value == null || value is System.DateTime)
								this.ReferralAgreedTime = (System.DateTime?)value;
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
		/// Maps to ReferExternalBak.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ReferExternalBakMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(ReferExternalBakMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ReferExternalBak.ReferralID
		/// </summary>
		virtual public System.String ReferralID
		{
			get
			{
				return base.GetSystemString(ReferExternalBakMetadata.ColumnNames.ReferralID);
			}

			set
			{
				base.SetSystemString(ReferExternalBakMetadata.ColumnNames.ReferralID, value);
			}
		}
		/// <summary>
		/// Maps to ReferExternalBak.SRReferReason
		/// </summary>
		virtual public System.String SRReferReason
		{
			get
			{
				return base.GetSystemString(ReferExternalBakMetadata.ColumnNames.SRReferReason);
			}

			set
			{
				base.SetSystemString(ReferExternalBakMetadata.ColumnNames.SRReferReason, value);
			}
		}
		/// <summary>
		/// Maps to ReferExternalBak.ReferReasonOther
		/// </summary>
		virtual public System.String ReferReasonOther
		{
			get
			{
				return base.GetSystemString(ReferExternalBakMetadata.ColumnNames.ReferReasonOther);
			}

			set
			{
				base.SetSystemString(ReferExternalBakMetadata.ColumnNames.ReferReasonOther, value);
			}
		}
		/// <summary>
		/// Maps to ReferExternalBak.OtherInformation
		/// </summary>
		virtual public System.String OtherInformation
		{
			get
			{
				return base.GetSystemString(ReferExternalBakMetadata.ColumnNames.OtherInformation);
			}

			set
			{
				base.SetSystemString(ReferExternalBakMetadata.ColumnNames.OtherInformation, value);
			}
		}
		/// <summary>
		/// Maps to ReferExternalBak.ReferralAgreedBy
		/// </summary>
		virtual public System.String ReferralAgreedBy
		{
			get
			{
				return base.GetSystemString(ReferExternalBakMetadata.ColumnNames.ReferralAgreedBy);
			}

			set
			{
				base.SetSystemString(ReferExternalBakMetadata.ColumnNames.ReferralAgreedBy, value);
			}
		}
		/// <summary>
		/// Maps to ReferExternalBak.ReferralAgreedTime
		/// </summary>
		virtual public System.DateTime? ReferralAgreedTime
		{
			get
			{
				return base.GetSystemDateTime(ReferExternalBakMetadata.ColumnNames.ReferralAgreedTime);
			}

			set
			{
				base.SetSystemDateTime(ReferExternalBakMetadata.ColumnNames.ReferralAgreedTime, value);
			}
		}
		/// <summary>
		/// Maps to ReferExternalBak.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ReferExternalBakMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ReferExternalBakMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ReferExternalBak.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ReferExternalBakMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ReferExternalBakMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esReferExternalBak entity)
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
			public System.String ReferralID
			{
				get
				{
					System.String data = entity.ReferralID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralID = null;
					else entity.ReferralID = Convert.ToString(value);
				}
			}
			public System.String SRReferReason
			{
				get
				{
					System.String data = entity.SRReferReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReferReason = null;
					else entity.SRReferReason = Convert.ToString(value);
				}
			}
			public System.String ReferReasonOther
			{
				get
				{
					System.String data = entity.ReferReasonOther;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferReasonOther = null;
					else entity.ReferReasonOther = Convert.ToString(value);
				}
			}
			public System.String OtherInformation
			{
				get
				{
					System.String data = entity.OtherInformation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherInformation = null;
					else entity.OtherInformation = Convert.ToString(value);
				}
			}
			public System.String ReferralAgreedBy
			{
				get
				{
					System.String data = entity.ReferralAgreedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralAgreedBy = null;
					else entity.ReferralAgreedBy = Convert.ToString(value);
				}
			}
			public System.String ReferralAgreedTime
			{
				get
				{
					System.DateTime? data = entity.ReferralAgreedTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferralAgreedTime = null;
					else entity.ReferralAgreedTime = Convert.ToDateTime(value);
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
			private esReferExternalBak entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esReferExternalBakQuery query)
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
				throw new Exception("esReferExternalBak can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ReferExternalBak : esReferExternalBak
	{
	}

	[Serializable]
	abstract public class esReferExternalBakQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ReferExternalBakMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ReferralID
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.ReferralID, esSystemType.String);
			}
		}

		public esQueryItem SRReferReason
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.SRReferReason, esSystemType.String);
			}
		}

		public esQueryItem ReferReasonOther
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.ReferReasonOther, esSystemType.String);
			}
		}

		public esQueryItem OtherInformation
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.OtherInformation, esSystemType.String);
			}
		}

		public esQueryItem ReferralAgreedBy
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.ReferralAgreedBy, esSystemType.String);
			}
		}

		public esQueryItem ReferralAgreedTime
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.ReferralAgreedTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ReferExternalBakMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ReferExternalBakCollection")]
	public partial class ReferExternalBakCollection : esReferExternalBakCollection, IEnumerable<ReferExternalBak>
	{
		public ReferExternalBakCollection()
		{

		}

		public static implicit operator List<ReferExternalBak>(ReferExternalBakCollection coll)
		{
			List<ReferExternalBak> list = new List<ReferExternalBak>();

			foreach (ReferExternalBak emp in coll)
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
				return ReferExternalBakMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ReferExternalBakQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ReferExternalBak(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ReferExternalBak();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ReferExternalBakQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ReferExternalBakQuery();
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
		public bool Load(ReferExternalBakQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ReferExternalBak AddNew()
		{
			ReferExternalBak entity = base.AddNewEntity() as ReferExternalBak;

			return entity;
		}
		public ReferExternalBak FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as ReferExternalBak;
		}

		#region IEnumerable< ReferExternalBak> Members

		IEnumerator<ReferExternalBak> IEnumerable<ReferExternalBak>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ReferExternalBak;
			}
		}

		#endregion

		private ReferExternalBakQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ReferExternalBak' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ReferExternalBak ({RegistrationNo})")]
	[Serializable]
	public partial class ReferExternalBak : esReferExternalBak
	{
		public ReferExternalBak()
		{
		}

		public ReferExternalBak(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ReferExternalBakMetadata.Meta();
			}
		}

		override protected esReferExternalBakQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ReferExternalBakQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ReferExternalBakQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ReferExternalBakQuery();
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
		public bool Load(ReferExternalBakQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ReferExternalBakQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ReferExternalBakQuery : esReferExternalBakQuery
	{
		public ReferExternalBakQuery()
		{

		}

		public ReferExternalBakQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ReferExternalBakQuery";
		}
	}

	[Serializable]
	public partial class ReferExternalBakMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ReferExternalBakMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.ReferralID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.ReferralID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.SRReferReason, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.SRReferReason;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.ReferReasonOther, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.ReferReasonOther;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.OtherInformation, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.OtherInformation;
			c.CharacterMaxLength = 1500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.ReferralAgreedBy, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.ReferralAgreedBy;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.ReferralAgreedTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.ReferralAgreedTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ReferExternalBakMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ReferExternalBakMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ReferExternalBakMetadata Meta()
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
			public const string ReferralID = "ReferralID";
			public const string SRReferReason = "SRReferReason";
			public const string ReferReasonOther = "ReferReasonOther";
			public const string OtherInformation = "OtherInformation";
			public const string ReferralAgreedBy = "ReferralAgreedBy";
			public const string ReferralAgreedTime = "ReferralAgreedTime";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string ReferralID = "ReferralID";
			public const string SRReferReason = "SRReferReason";
			public const string ReferReasonOther = "ReferReasonOther";
			public const string OtherInformation = "OtherInformation";
			public const string ReferralAgreedBy = "ReferralAgreedBy";
			public const string ReferralAgreedTime = "ReferralAgreedTime";
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
			lock (typeof(ReferExternalBakMetadata))
			{
				if (ReferExternalBakMetadata.mapDelegates == null)
				{
					ReferExternalBakMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ReferExternalBakMetadata.meta == null)
				{
					ReferExternalBakMetadata.meta = new ReferExternalBakMetadata();
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
				meta.AddTypeMap("ReferralID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRReferReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferReasonOther", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherInformation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralAgreedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferralAgreedTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ReferExternalBak";
				meta.Destination = "ReferExternalBak";
				meta.spInsert = "proc_ReferExternalBakInsert";
				meta.spUpdate = "proc_ReferExternalBakUpdate";
				meta.spDelete = "proc_ReferExternalBakDelete";
				meta.spLoadAll = "proc_ReferExternalBakLoadAll";
				meta.spLoadByPrimaryKey = "proc_ReferExternalBakLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ReferExternalBakMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
