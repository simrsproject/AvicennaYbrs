/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/20/2022 12:17:39 PM
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
	abstract public class esCredentialProcessCpdCollection : esEntityCollectionWAuditLog
	{
		public esCredentialProcessCpdCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "CredentialProcessCpdCollection";
		}

		#region Query Logic
		protected void InitQuery(esCredentialProcessCpdQuery query)
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
			this.InitQuery(query as esCredentialProcessCpdQuery);
		}
		#endregion

		virtual public CredentialProcessCpd DetachEntity(CredentialProcessCpd entity)
		{
			return base.DetachEntity(entity) as CredentialProcessCpd;
		}

		virtual public CredentialProcessCpd AttachEntity(CredentialProcessCpd entity)
		{
			return base.AttachEntity(entity) as CredentialProcessCpd;
		}

		virtual public void Combine(CredentialProcessCpdCollection collection)
		{
			base.Combine(collection);
		}

		new public CredentialProcessCpd this[int index]
		{
			get
			{
				return base[index] as CredentialProcessCpd;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CredentialProcessCpd);
		}
	}

	[Serializable]
	abstract public class esCredentialProcessCpd : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCredentialProcessCpdQuery GetDynamicQuery()
		{
			return null;
		}

		public esCredentialProcessCpd()
		{
		}

		public esCredentialProcessCpd(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String cpdNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, cpdNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, cpdNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String cpdNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, cpdNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, cpdNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String cpdNo)
		{
			esCredentialProcessCpdQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.CpdNo == cpdNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String cpdNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("CpdNo", cpdNo);
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
						case "CpdNo": this.str.CpdNo = (string)value; break;
						case "CpdName": this.str.CpdName = (string)value; break;
						case "InstitutionName": this.str.InstitutionName = (string)value; break;
						case "TimeAndHours": this.str.TimeAndHours = (string)value; break;
						case "Skp": this.str.Skp = (string)value; break;
						case "AchievedCompetence": this.str.AchievedCompetence = (string)value; break;
						case "PhysicalEvidence": this.str.PhysicalEvidence = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Skp":

							if (value == null || value is System.Decimal)
								this.Skp = (System.Decimal?)value;
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
		/// Maps to CredentialProcessCpd.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessCpdMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessCpdMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.CpdNo
		/// </summary>
		virtual public System.String CpdNo
		{
			get
			{
				return base.GetSystemString(CredentialProcessCpdMetadata.ColumnNames.CpdNo);
			}

			set
			{
				base.SetSystemString(CredentialProcessCpdMetadata.ColumnNames.CpdNo, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.CpdName
		/// </summary>
		virtual public System.String CpdName
		{
			get
			{
				return base.GetSystemString(CredentialProcessCpdMetadata.ColumnNames.CpdName);
			}

			set
			{
				base.SetSystemString(CredentialProcessCpdMetadata.ColumnNames.CpdName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.InstitutionName
		/// </summary>
		virtual public System.String InstitutionName
		{
			get
			{
				return base.GetSystemString(CredentialProcessCpdMetadata.ColumnNames.InstitutionName);
			}

			set
			{
				base.SetSystemString(CredentialProcessCpdMetadata.ColumnNames.InstitutionName, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.TimeAndHours
		/// </summary>
		virtual public System.String TimeAndHours
		{
			get
			{
				return base.GetSystemString(CredentialProcessCpdMetadata.ColumnNames.TimeAndHours);
			}

			set
			{
				base.SetSystemString(CredentialProcessCpdMetadata.ColumnNames.TimeAndHours, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.Skp
		/// </summary>
		virtual public System.Decimal? Skp
		{
			get
			{
				return base.GetSystemDecimal(CredentialProcessCpdMetadata.ColumnNames.Skp);
			}

			set
			{
				base.SetSystemDecimal(CredentialProcessCpdMetadata.ColumnNames.Skp, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.AchievedCompetence
		/// </summary>
		virtual public System.String AchievedCompetence
		{
			get
			{
				return base.GetSystemString(CredentialProcessCpdMetadata.ColumnNames.AchievedCompetence);
			}

			set
			{
				base.SetSystemString(CredentialProcessCpdMetadata.ColumnNames.AchievedCompetence, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.PhysicalEvidence
		/// </summary>
		virtual public System.String PhysicalEvidence
		{
			get
			{
				return base.GetSystemString(CredentialProcessCpdMetadata.ColumnNames.PhysicalEvidence);
			}

			set
			{
				base.SetSystemString(CredentialProcessCpdMetadata.ColumnNames.PhysicalEvidence, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CredentialProcessCpdMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(CredentialProcessCpdMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CredentialProcessCpd.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CredentialProcessCpdMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(CredentialProcessCpdMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCredentialProcessCpd entity)
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
			public System.String CpdNo
			{
				get
				{
					System.String data = entity.CpdNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CpdNo = null;
					else entity.CpdNo = Convert.ToString(value);
				}
			}
			public System.String CpdName
			{
				get
				{
					System.String data = entity.CpdName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CpdName = null;
					else entity.CpdName = Convert.ToString(value);
				}
			}
			public System.String InstitutionName
			{
				get
				{
					System.String data = entity.InstitutionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstitutionName = null;
					else entity.InstitutionName = Convert.ToString(value);
				}
			}
			public System.String TimeAndHours
			{
				get
				{
					System.String data = entity.TimeAndHours;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TimeAndHours = null;
					else entity.TimeAndHours = Convert.ToString(value);
				}
			}
			public System.String Skp
			{
				get
				{
					System.Decimal? data = entity.Skp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Skp = null;
					else entity.Skp = Convert.ToDecimal(value);
				}
			}
			public System.String AchievedCompetence
			{
				get
				{
					System.String data = entity.AchievedCompetence;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AchievedCompetence = null;
					else entity.AchievedCompetence = Convert.ToString(value);
				}
			}
			public System.String PhysicalEvidence
			{
				get
				{
					System.String data = entity.PhysicalEvidence;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicalEvidence = null;
					else entity.PhysicalEvidence = Convert.ToString(value);
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
			private esCredentialProcessCpd entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCredentialProcessCpdQuery query)
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
				throw new Exception("esCredentialProcessCpd can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CredentialProcessCpd : esCredentialProcessCpd
	{
	}

	[Serializable]
	abstract public class esCredentialProcessCpdQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessCpdMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem CpdNo
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.CpdNo, esSystemType.String);
			}
		}

		public esQueryItem CpdName
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.CpdName, esSystemType.String);
			}
		}

		public esQueryItem InstitutionName
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.InstitutionName, esSystemType.String);
			}
		}

		public esQueryItem TimeAndHours
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.TimeAndHours, esSystemType.String);
			}
		}

		public esQueryItem Skp
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.Skp, esSystemType.Decimal);
			}
		}

		public esQueryItem AchievedCompetence
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.AchievedCompetence, esSystemType.String);
			}
		}

		public esQueryItem PhysicalEvidence
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.PhysicalEvidence, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CredentialProcessCpdMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CredentialProcessCpdCollection")]
	public partial class CredentialProcessCpdCollection : esCredentialProcessCpdCollection, IEnumerable<CredentialProcessCpd>
	{
		public CredentialProcessCpdCollection()
		{

		}

		public static implicit operator List<CredentialProcessCpd>(CredentialProcessCpdCollection coll)
		{
			List<CredentialProcessCpd> list = new List<CredentialProcessCpd>();

			foreach (CredentialProcessCpd emp in coll)
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
				return CredentialProcessCpdMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessCpdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CredentialProcessCpd(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CredentialProcessCpd();
		}

		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessCpdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessCpdQuery();
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
		public bool Load(CredentialProcessCpdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CredentialProcessCpd AddNew()
		{
			CredentialProcessCpd entity = base.AddNewEntity() as CredentialProcessCpd;

			return entity;
		}
		public CredentialProcessCpd FindByPrimaryKey(String transactionNo, String cpdNo)
		{
			return base.FindByPrimaryKey(transactionNo, cpdNo) as CredentialProcessCpd;
		}

		#region IEnumerable< CredentialProcessCpd> Members

		IEnumerator<CredentialProcessCpd> IEnumerable<CredentialProcessCpd>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as CredentialProcessCpd;
			}
		}

		#endregion

		private CredentialProcessCpdQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CredentialProcessCpd' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CredentialProcessCpd ({TransactionNo, CpdNo})")]
	[Serializable]
	public partial class CredentialProcessCpd : esCredentialProcessCpd
	{
		public CredentialProcessCpd()
		{
		}

		public CredentialProcessCpd(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CredentialProcessCpdMetadata.Meta();
			}
		}

		override protected esCredentialProcessCpdQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CredentialProcessCpdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public CredentialProcessCpdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CredentialProcessCpdQuery();
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
		public bool Load(CredentialProcessCpdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private CredentialProcessCpdQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CredentialProcessCpdQuery : esCredentialProcessCpdQuery
	{
		public CredentialProcessCpdQuery()
		{

		}

		public CredentialProcessCpdQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CredentialProcessCpdQuery";
		}
	}

	[Serializable]
	public partial class CredentialProcessCpdMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CredentialProcessCpdMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.CpdNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.CpdNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.CpdName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.CpdName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.InstitutionName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.InstitutionName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.TimeAndHours, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.TimeAndHours;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.Skp, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.Skp;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.AchievedCompetence, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.AchievedCompetence;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.PhysicalEvidence, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.PhysicalEvidence;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(CredentialProcessCpdMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CredentialProcessCpdMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public CredentialProcessCpdMetadata Meta()
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
			public const string CpdNo = "CpdNo";
			public const string CpdName = "CpdName";
			public const string InstitutionName = "InstitutionName";
			public const string TimeAndHours = "TimeAndHours";
			public const string Skp = "Skp";
			public const string AchievedCompetence = "AchievedCompetence";
			public const string PhysicalEvidence = "PhysicalEvidence";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string CpdNo = "CpdNo";
			public const string CpdName = "CpdName";
			public const string InstitutionName = "InstitutionName";
			public const string TimeAndHours = "TimeAndHours";
			public const string Skp = "Skp";
			public const string AchievedCompetence = "AchievedCompetence";
			public const string PhysicalEvidence = "PhysicalEvidence";
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
			lock (typeof(CredentialProcessCpdMetadata))
			{
				if (CredentialProcessCpdMetadata.mapDelegates == null)
				{
					CredentialProcessCpdMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (CredentialProcessCpdMetadata.meta == null)
				{
					CredentialProcessCpdMetadata.meta = new CredentialProcessCpdMetadata();
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
				meta.AddTypeMap("CpdNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CpdName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstitutionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TimeAndHours", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Skp", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AchievedCompetence", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicalEvidence", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "CredentialProcessCpd";
				meta.Destination = "CredentialProcessCpd";
				meta.spInsert = "proc_CredentialProcessCpdInsert";
				meta.spUpdate = "proc_CredentialProcessCpdUpdate";
				meta.spDelete = "proc_CredentialProcessCpdDelete";
				meta.spLoadAll = "proc_CredentialProcessCpdLoadAll";
				meta.spLoadByPrimaryKey = "proc_CredentialProcessCpdLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CredentialProcessCpdMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
