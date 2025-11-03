/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/20/2023 2:28:23 PM
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
	abstract public class esMedicalRecordFileCompletenessItemCollection : esEntityCollectionWAuditLog
	{
		public esMedicalRecordFileCompletenessItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicalRecordFileCompletenessItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileCompletenessItemQuery query)
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
			this.InitQuery(query as esMedicalRecordFileCompletenessItemQuery);
		}
		#endregion

		virtual public MedicalRecordFileCompletenessItem DetachEntity(MedicalRecordFileCompletenessItem entity)
		{
			return base.DetachEntity(entity) as MedicalRecordFileCompletenessItem;
		}

		virtual public MedicalRecordFileCompletenessItem AttachEntity(MedicalRecordFileCompletenessItem entity)
		{
			return base.AttachEntity(entity) as MedicalRecordFileCompletenessItem;
		}

		virtual public void Combine(MedicalRecordFileCompletenessItemCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicalRecordFileCompletenessItem this[int index]
		{
			get
			{
				return base[index] as MedicalRecordFileCompletenessItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalRecordFileCompletenessItem);
		}
	}

	[Serializable]
	abstract public class esMedicalRecordFileCompletenessItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalRecordFileCompletenessItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalRecordFileCompletenessItem()
		{
		}

		public esMedicalRecordFileCompletenessItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 documentFilesID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, documentFilesID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 documentFilesID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, documentFilesID);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 documentFilesID)
		{
			esMedicalRecordFileCompletenessItemQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.DocumentFilesID == documentFilesID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 documentFilesID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("DocumentFilesID", documentFilesID);
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
						case "DocumentFilesID": this.str.DocumentFilesID = (string)value; break;
						case "IsComplete": this.str.IsComplete = (string)value; break;
						case "IsNotApplicable": this.str.IsNotApplicable = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DocumentFilesID":

							if (value == null || value is System.Int32)
								this.DocumentFilesID = (System.Int32?)value;
							break;
						case "IsComplete":

							if (value == null || value is System.Boolean)
								this.IsComplete = (System.Boolean?)value;
							break;
						case "IsNotApplicable":

							if (value == null || value is System.Boolean)
								this.IsNotApplicable = (System.Boolean?)value;
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
		/// Maps to MedicalRecordFileCompletenessItem.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessItemMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessItemMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessItem.DocumentFilesID
		/// </summary>
		virtual public System.Int32? DocumentFilesID
		{
			get
			{
				return base.GetSystemInt32(MedicalRecordFileCompletenessItemMetadata.ColumnNames.DocumentFilesID);
			}

			set
			{
				base.SetSystemInt32(MedicalRecordFileCompletenessItemMetadata.ColumnNames.DocumentFilesID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessItem.IsComplete
		/// </summary>
		virtual public System.Boolean? IsComplete
		{
			get
			{
				return base.GetSystemBoolean(MedicalRecordFileCompletenessItemMetadata.ColumnNames.IsComplete);
			}

			set
			{
				base.SetSystemBoolean(MedicalRecordFileCompletenessItemMetadata.ColumnNames.IsComplete, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessItem.IsNotApplicable
		/// </summary>
		virtual public System.Boolean? IsNotApplicable
		{
			get
			{
				return base.GetSystemBoolean(MedicalRecordFileCompletenessItemMetadata.ColumnNames.IsNotApplicable);
			}

			set
			{
				base.SetSystemBoolean(MedicalRecordFileCompletenessItemMetadata.ColumnNames.IsNotApplicable, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileCompletenessItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileCompletenessItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileCompletenessItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileCompletenessItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileCompletenessItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicalRecordFileCompletenessItem entity)
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
			public System.String DocumentFilesID
			{
				get
				{
					System.Int32? data = entity.DocumentFilesID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentFilesID = null;
					else entity.DocumentFilesID = Convert.ToInt32(value);
				}
			}
			public System.String IsComplete
			{
				get
				{
					System.Boolean? data = entity.IsComplete;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsComplete = null;
					else entity.IsComplete = Convert.ToBoolean(value);
				}
			}
			public System.String IsNotApplicable
			{
				get
				{
					System.Boolean? data = entity.IsNotApplicable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNotApplicable = null;
					else entity.IsNotApplicable = Convert.ToBoolean(value);
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
			private esMedicalRecordFileCompletenessItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileCompletenessItemQuery query)
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
				throw new Exception("esMedicalRecordFileCompletenessItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicalRecordFileCompletenessItem : esMedicalRecordFileCompletenessItem
	{
	}

	[Serializable]
	abstract public class esMedicalRecordFileCompletenessItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileCompletenessItemMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem DocumentFilesID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessItemMetadata.ColumnNames.DocumentFilesID, esSystemType.Int32);
			}
		}

		public esQueryItem IsComplete
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessItemMetadata.ColumnNames.IsComplete, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNotApplicable
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessItemMetadata.ColumnNames.IsNotApplicable, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileCompletenessItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalRecordFileCompletenessItemCollection")]
	public partial class MedicalRecordFileCompletenessItemCollection : esMedicalRecordFileCompletenessItemCollection, IEnumerable<MedicalRecordFileCompletenessItem>
	{
		public MedicalRecordFileCompletenessItemCollection()
		{

		}

		public static implicit operator List<MedicalRecordFileCompletenessItem>(MedicalRecordFileCompletenessItemCollection coll)
		{
			List<MedicalRecordFileCompletenessItem> list = new List<MedicalRecordFileCompletenessItem>();

			foreach (MedicalRecordFileCompletenessItem emp in coll)
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
				return MedicalRecordFileCompletenessItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileCompletenessItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalRecordFileCompletenessItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalRecordFileCompletenessItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileCompletenessItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileCompletenessItemQuery();
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
		public bool Load(MedicalRecordFileCompletenessItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicalRecordFileCompletenessItem AddNew()
		{
			MedicalRecordFileCompletenessItem entity = base.AddNewEntity() as MedicalRecordFileCompletenessItem;

			return entity;
		}
		public MedicalRecordFileCompletenessItem FindByPrimaryKey(String registrationNo, Int32 documentFilesID)
		{
			return base.FindByPrimaryKey(registrationNo, documentFilesID) as MedicalRecordFileCompletenessItem;
		}

		#region IEnumerable< MedicalRecordFileCompletenessItem> Members

		IEnumerator<MedicalRecordFileCompletenessItem> IEnumerable<MedicalRecordFileCompletenessItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicalRecordFileCompletenessItem;
			}
		}

		#endregion

		private MedicalRecordFileCompletenessItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalRecordFileCompletenessItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicalRecordFileCompletenessItem ({RegistrationNo, DocumentFilesID})")]
	[Serializable]
	public partial class MedicalRecordFileCompletenessItem : esMedicalRecordFileCompletenessItem
	{
		public MedicalRecordFileCompletenessItem()
		{
		}

		public MedicalRecordFileCompletenessItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileCompletenessItemMetadata.Meta();
			}
		}

		override protected esMedicalRecordFileCompletenessItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileCompletenessItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileCompletenessItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileCompletenessItemQuery();
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
		public bool Load(MedicalRecordFileCompletenessItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicalRecordFileCompletenessItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicalRecordFileCompletenessItemQuery : esMedicalRecordFileCompletenessItemQuery
	{
		public MedicalRecordFileCompletenessItemQuery()
		{

		}

		public MedicalRecordFileCompletenessItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicalRecordFileCompletenessItemQuery";
		}
	}

	[Serializable]
	public partial class MedicalRecordFileCompletenessItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalRecordFileCompletenessItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalRecordFileCompletenessItemMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessItemMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessItemMetadata.ColumnNames.DocumentFilesID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalRecordFileCompletenessItemMetadata.PropertyNames.DocumentFilesID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessItemMetadata.ColumnNames.IsComplete, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalRecordFileCompletenessItemMetadata.PropertyNames.IsComplete;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessItemMetadata.ColumnNames.IsNotApplicable, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalRecordFileCompletenessItemMetadata.PropertyNames.IsNotApplicable;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessItemMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessItemMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileCompletenessItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileCompletenessItemMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileCompletenessItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MedicalRecordFileCompletenessItemMetadata Meta()
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
			public const string DocumentFilesID = "DocumentFilesID";
			public const string IsComplete = "IsComplete";
			public const string IsNotApplicable = "IsNotApplicable";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string DocumentFilesID = "DocumentFilesID";
			public const string IsComplete = "IsComplete";
			public const string IsNotApplicable = "IsNotApplicable";
			public const string Notes = "Notes";
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
			lock (typeof(MedicalRecordFileCompletenessItemMetadata))
			{
				if (MedicalRecordFileCompletenessItemMetadata.mapDelegates == null)
				{
					MedicalRecordFileCompletenessItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicalRecordFileCompletenessItemMetadata.meta == null)
				{
					MedicalRecordFileCompletenessItemMetadata.meta = new MedicalRecordFileCompletenessItemMetadata();
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
				meta.AddTypeMap("DocumentFilesID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsComplete", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNotApplicable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "MedicalRecordFileCompletenessItem";
				meta.Destination = "MedicalRecordFileCompletenessItem";
				meta.spInsert = "proc_MedicalRecordFileCompletenessItemInsert";
				meta.spUpdate = "proc_MedicalRecordFileCompletenessItemUpdate";
				meta.spDelete = "proc_MedicalRecordFileCompletenessItemDelete";
				meta.spLoadAll = "proc_MedicalRecordFileCompletenessItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalRecordFileCompletenessItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalRecordFileCompletenessItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
