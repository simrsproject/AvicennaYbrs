/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/20/2022 10:43:45 PM
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
	abstract public class esHomePrescriptionCollection : esEntityCollectionWAuditLog
	{
		public esHomePrescriptionCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "HomePrescriptionCollection";
		}

		#region Query Logic
		protected void InitQuery(esHomePrescriptionQuery query)
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
			this.InitQuery(query as esHomePrescriptionQuery);
		}
		#endregion

		virtual public HomePrescription DetachEntity(HomePrescription entity)
		{
			return base.DetachEntity(entity) as HomePrescription;
		}

		virtual public HomePrescription AttachEntity(HomePrescription entity)
		{
			return base.AttachEntity(entity) as HomePrescription;
		}

		virtual public void Combine(HomePrescriptionCollection collection)
		{
			base.Combine(collection);
		}

		new public HomePrescription this[int index]
		{
			get
			{
				return base[index] as HomePrescription;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HomePrescription);
		}
	}

	[Serializable]
	abstract public class esHomePrescription : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHomePrescriptionQuery GetDynamicQuery()
		{
			return null;
		}

		public esHomePrescription()
		{
		}

		public esHomePrescription(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 medicationReceiveNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 medicationReceiveNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicationReceiveNo);
			else
				return LoadByPrimaryKeyStoredProcedure(medicationReceiveNo);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 medicationReceiveNo)
		{
			esHomePrescriptionQuery query = this.GetDynamicQuery();
			query.Where(query.MedicationReceiveNo == medicationReceiveNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 medicationReceiveNo)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicationReceiveNo", medicationReceiveNo);
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
						case "Morning": this.str.Morning = (string)value; break;
						case "Noon": this.str.Noon = (string)value; break;
						case "Afternoon": this.str.Afternoon = (string)value; break;
						case "Night": this.str.Night = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DrugName": this.str.DrugName = (string)value; break;
						case "Indication": this.str.Indication = (string)value; break;
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
		/// Maps to HomePrescription.MedicationReceiveNo
		/// </summary>
		virtual public System.Int64? MedicationReceiveNo
		{
			get
			{
				return base.GetSystemInt64(HomePrescriptionMetadata.ColumnNames.MedicationReceiveNo);
			}

			set
			{
				base.SetSystemInt64(HomePrescriptionMetadata.ColumnNames.MedicationReceiveNo, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.Morning
		/// </summary>
		virtual public System.String Morning
		{
			get
			{
				return base.GetSystemString(HomePrescriptionMetadata.ColumnNames.Morning);
			}

			set
			{
				base.SetSystemString(HomePrescriptionMetadata.ColumnNames.Morning, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.Noon
		/// </summary>
		virtual public System.String Noon
		{
			get
			{
				return base.GetSystemString(HomePrescriptionMetadata.ColumnNames.Noon);
			}

			set
			{
				base.SetSystemString(HomePrescriptionMetadata.ColumnNames.Noon, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.Afternoon
		/// </summary>
		virtual public System.String Afternoon
		{
			get
			{
				return base.GetSystemString(HomePrescriptionMetadata.ColumnNames.Afternoon);
			}

			set
			{
				base.SetSystemString(HomePrescriptionMetadata.ColumnNames.Afternoon, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.Night
		/// </summary>
		virtual public System.String Night
		{
			get
			{
				return base.GetSystemString(HomePrescriptionMetadata.ColumnNames.Night);
			}

			set
			{
				base.SetSystemString(HomePrescriptionMetadata.ColumnNames.Night, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(HomePrescriptionMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(HomePrescriptionMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(HomePrescriptionMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(HomePrescriptionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(HomePrescriptionMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(HomePrescriptionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.DrugName
		/// </summary>
		virtual public System.String DrugName
		{
			get
			{
				return base.GetSystemString(HomePrescriptionMetadata.ColumnNames.DrugName);
			}

			set
			{
				base.SetSystemString(HomePrescriptionMetadata.ColumnNames.DrugName, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescription.Indication
		/// </summary>
		virtual public System.String Indication
		{
			get
			{
				return base.GetSystemString(HomePrescriptionMetadata.ColumnNames.Indication);
			}

			set
			{
				base.SetSystemString(HomePrescriptionMetadata.ColumnNames.Indication, value);
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
			public esStrings(esHomePrescription entity)
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
			public System.String Morning
			{
				get
				{
					System.String data = entity.Morning;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Morning = null;
					else entity.Morning = Convert.ToString(value);
				}
			}
			public System.String Noon
			{
				get
				{
					System.String data = entity.Noon;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Noon = null;
					else entity.Noon = Convert.ToString(value);
				}
			}
			public System.String Afternoon
			{
				get
				{
					System.String data = entity.Afternoon;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Afternoon = null;
					else entity.Afternoon = Convert.ToString(value);
				}
			}
			public System.String Night
			{
				get
				{
					System.String data = entity.Night;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Night = null;
					else entity.Night = Convert.ToString(value);
				}
			}
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
			public System.String DrugName
			{
				get
				{
					System.String data = entity.DrugName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DrugName = null;
					else entity.DrugName = Convert.ToString(value);
				}
			}
			public System.String Indication
			{
				get
				{
					System.String data = entity.Indication;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Indication = null;
					else entity.Indication = Convert.ToString(value);
				}
			}
			private esHomePrescription entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHomePrescriptionQuery query)
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
				throw new Exception("esHomePrescription can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class HomePrescription : esHomePrescription
	{
	}

	[Serializable]
	abstract public class esHomePrescriptionQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return HomePrescriptionMetadata.Meta();
			}
		}

		public esQueryItem MedicationReceiveNo
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.MedicationReceiveNo, esSystemType.Int64);
			}
		}

		public esQueryItem Morning
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.Morning, esSystemType.String);
			}
		}

		public esQueryItem Noon
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.Noon, esSystemType.String);
			}
		}

		public esQueryItem Afternoon
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.Afternoon, esSystemType.String);
			}
		}

		public esQueryItem Night
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.Night, esSystemType.String);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem DrugName
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.DrugName, esSystemType.String);
			}
		}

		public esQueryItem Indication
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionMetadata.ColumnNames.Indication, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HomePrescriptionCollection")]
	public partial class HomePrescriptionCollection : esHomePrescriptionCollection, IEnumerable<HomePrescription>
	{
		public HomePrescriptionCollection()
		{

		}

		public static implicit operator List<HomePrescription>(HomePrescriptionCollection coll)
		{
			List<HomePrescription> list = new List<HomePrescription>();

			foreach (HomePrescription emp in coll)
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
				return HomePrescriptionMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HomePrescriptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HomePrescription(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HomePrescription();
		}

		#endregion

		[BrowsableAttribute(false)]
		public HomePrescriptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HomePrescriptionQuery();
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
		public bool Load(HomePrescriptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public HomePrescription AddNew()
		{
			HomePrescription entity = base.AddNewEntity() as HomePrescription;

			return entity;
		}
		public HomePrescription FindByPrimaryKey(Int64 medicationReceiveNo)
		{
			return base.FindByPrimaryKey(medicationReceiveNo) as HomePrescription;
		}

		#region IEnumerable< HomePrescription> Members

		IEnumerator<HomePrescription> IEnumerable<HomePrescription>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as HomePrescription;
			}
		}

		#endregion

		private HomePrescriptionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HomePrescription' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("HomePrescription ({MedicationReceiveNo})")]
	[Serializable]
	public partial class HomePrescription : esHomePrescription
	{
		public HomePrescription()
		{
		}

		public HomePrescription(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HomePrescriptionMetadata.Meta();
			}
		}

		override protected esHomePrescriptionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HomePrescriptionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public HomePrescriptionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HomePrescriptionQuery();
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
		public bool Load(HomePrescriptionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private HomePrescriptionQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class HomePrescriptionQuery : esHomePrescriptionQuery
	{
		public HomePrescriptionQuery()
		{

		}

		public HomePrescriptionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "HomePrescriptionQuery";
		}
	}

	[Serializable]
	public partial class HomePrescriptionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HomePrescriptionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.MedicationReceiveNo, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.MedicationReceiveNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.Morning, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.Morning;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.Noon, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.Noon;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.Afternoon, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.Afternoon;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.Night, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.Night;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.Note, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.DrugName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.DrugName;
			c.CharacterMaxLength = 600;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionMetadata.ColumnNames.Indication, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionMetadata.PropertyNames.Indication;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public HomePrescriptionMetadata Meta()
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
			public const string Morning = "Morning";
			public const string Noon = "Noon";
			public const string Afternoon = "Afternoon";
			public const string Night = "Night";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DrugName = "DrugName";
			public const string Indication = "Indication";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MedicationReceiveNo = "MedicationReceiveNo";
			public const string Morning = "Morning";
			public const string Noon = "Noon";
			public const string Afternoon = "Afternoon";
			public const string Night = "Night";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DrugName = "DrugName";
			public const string Indication = "Indication";
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
			lock (typeof(HomePrescriptionMetadata))
			{
				if (HomePrescriptionMetadata.mapDelegates == null)
				{
					HomePrescriptionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (HomePrescriptionMetadata.meta == null)
				{
					HomePrescriptionMetadata.meta = new HomePrescriptionMetadata();
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
				meta.AddTypeMap("Morning", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Noon", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Afternoon", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Night", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DrugName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Indication", new esTypeMap("varchar", "System.String"));


				meta.Source = "HomePrescription";
				meta.Destination = "HomePrescription";
				meta.spInsert = "proc_HomePrescriptionInsert";
				meta.spUpdate = "proc_HomePrescriptionUpdate";
				meta.spDelete = "proc_HomePrescriptionDelete";
				meta.spLoadAll = "proc_HomePrescriptionLoadAll";
				meta.spLoadByPrimaryKey = "proc_HomePrescriptionLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HomePrescriptionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
