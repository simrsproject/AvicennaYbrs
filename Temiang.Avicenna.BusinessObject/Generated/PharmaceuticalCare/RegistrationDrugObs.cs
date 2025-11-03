/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/13/2022 10:52:57 PM
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
	abstract public class esRegistrationDrugObsCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationDrugObsCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationDrugObsCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationDrugObsQuery query)
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
			this.InitQuery(query as esRegistrationDrugObsQuery);
		}
		#endregion

		virtual public RegistrationDrugObs DetachEntity(RegistrationDrugObs entity)
		{
			return base.DetachEntity(entity) as RegistrationDrugObs;
		}

		virtual public RegistrationDrugObs AttachEntity(RegistrationDrugObs entity)
		{
			return base.AttachEntity(entity) as RegistrationDrugObs;
		}

		virtual public void Combine(RegistrationDrugObsCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationDrugObs this[int index]
		{
			get
			{
				return base[index] as RegistrationDrugObs;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationDrugObs);
		}
	}

	[Serializable]
	abstract public class esRegistrationDrugObs : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationDrugObsQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationDrugObs()
		{
		}

		public esRegistrationDrugObs(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, Int32 drugObsNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, drugObsNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, drugObsNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, Int32 drugObsNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, drugObsNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, drugObsNo);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, Int32 drugObsNo)
		{
			esRegistrationDrugObsQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.DrugObsNo == drugObsNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, Int32 drugObsNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("DrugObsNo", drugObsNo);
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
						case "DrugObsNo": this.str.DrugObsNo = (string)value; break;
						case "DrugObsDateTime": this.str.DrugObsDateTime = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "IsNeedPto": this.str.IsNeedPto = (string)value; break;
						case "DrugInteractionRisk": this.str.DrugInteractionRisk = (string)value; break;
						case "Recommendation": this.str.Recommendation = (string)value; break;
						case "IsDeleted": this.str.IsDeleted = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "DrugObsNo":

							if (value == null || value is System.Int32)
								this.DrugObsNo = (System.Int32?)value;
							break;
						case "DrugObsDateTime":

							if (value == null || value is System.DateTime)
								this.DrugObsDateTime = (System.DateTime?)value;
							break;
						case "IsNeedPto":

							if (value == null || value is System.Boolean)
								this.IsNeedPto = (System.Boolean?)value;
							break;
						case "IsDeleted":

							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationDrugObs.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.DrugObsNo
		/// </summary>
		virtual public System.Int32? DrugObsNo
		{
			get
			{
				return base.GetSystemInt32(RegistrationDrugObsMetadata.ColumnNames.DrugObsNo);
			}

			set
			{
				base.SetSystemInt32(RegistrationDrugObsMetadata.ColumnNames.DrugObsNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.DrugObsDateTime
		/// </summary>
		virtual public System.DateTime? DrugObsDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationDrugObsMetadata.ColumnNames.DrugObsDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationDrugObsMetadata.ColumnNames.DrugObsDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.IsNeedPto
		/// </summary>
		virtual public System.Boolean? IsNeedPto
		{
			get
			{
				return base.GetSystemBoolean(RegistrationDrugObsMetadata.ColumnNames.IsNeedPto);
			}

			set
			{
				base.SetSystemBoolean(RegistrationDrugObsMetadata.ColumnNames.IsNeedPto, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.DrugInteractionRisk
		/// </summary>
		virtual public System.String DrugInteractionRisk
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsMetadata.ColumnNames.DrugInteractionRisk);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsMetadata.ColumnNames.DrugInteractionRisk, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.Recommendation
		/// </summary>
		virtual public System.String Recommendation
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsMetadata.ColumnNames.Recommendation);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsMetadata.ColumnNames.Recommendation, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.IsDeleted
		/// </summary>
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(RegistrationDrugObsMetadata.ColumnNames.IsDeleted);
			}

			set
			{
				base.SetSystemBoolean(RegistrationDrugObsMetadata.ColumnNames.IsDeleted, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationDrugObsMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationDrugObsMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationDrugObsMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationDrugObsMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationDrugObs.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationDrugObsMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationDrugObsMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esRegistrationDrugObs entity)
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
			public System.String DrugObsNo
			{
				get
				{
					System.Int32? data = entity.DrugObsNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DrugObsNo = null;
					else entity.DrugObsNo = Convert.ToInt32(value);
				}
			}
			public System.String DrugObsDateTime
			{
				get
				{
					System.DateTime? data = entity.DrugObsDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DrugObsDateTime = null;
					else entity.DrugObsDateTime = Convert.ToDateTime(value);
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
			public System.String IsNeedPto
			{
				get
				{
					System.Boolean? data = entity.IsNeedPto;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedPto = null;
					else entity.IsNeedPto = Convert.ToBoolean(value);
				}
			}
			public System.String DrugInteractionRisk
			{
				get
				{
					System.String data = entity.DrugInteractionRisk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DrugInteractionRisk = null;
					else entity.DrugInteractionRisk = Convert.ToString(value);
				}
			}
			public System.String Recommendation
			{
				get
				{
					System.String data = entity.Recommendation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Recommendation = null;
					else entity.Recommendation = Convert.ToString(value);
				}
			}
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
			private esRegistrationDrugObs entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationDrugObsQuery query)
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
				throw new Exception("esRegistrationDrugObs can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationDrugObs : esRegistrationDrugObs
	{
	}

	[Serializable]
	abstract public class esRegistrationDrugObsQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationDrugObsMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem DrugObsNo
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.DrugObsNo, esSystemType.Int32);
			}
		}

		public esQueryItem DrugObsDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.DrugObsDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem IsNeedPto
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.IsNeedPto, esSystemType.Boolean);
			}
		}

		public esQueryItem DrugInteractionRisk
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.DrugInteractionRisk, esSystemType.String);
			}
		}

		public esQueryItem Recommendation
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.Recommendation, esSystemType.String);
			}
		}

		public esQueryItem IsDeleted
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationDrugObsMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationDrugObsCollection")]
	public partial class RegistrationDrugObsCollection : esRegistrationDrugObsCollection, IEnumerable<RegistrationDrugObs>
	{
		public RegistrationDrugObsCollection()
		{

		}

		public static implicit operator List<RegistrationDrugObs>(RegistrationDrugObsCollection coll)
		{
			List<RegistrationDrugObs> list = new List<RegistrationDrugObs>();

			foreach (RegistrationDrugObs emp in coll)
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
				return RegistrationDrugObsMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationDrugObsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationDrugObs(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationDrugObs();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationDrugObsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationDrugObsQuery();
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
		public bool Load(RegistrationDrugObsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationDrugObs AddNew()
		{
			RegistrationDrugObs entity = base.AddNewEntity() as RegistrationDrugObs;

			return entity;
		}
		public RegistrationDrugObs FindByPrimaryKey(String registrationNo, Int32 drugObsNo)
		{
			return base.FindByPrimaryKey(registrationNo, drugObsNo) as RegistrationDrugObs;
		}

		#region IEnumerable< RegistrationDrugObs> Members

		IEnumerator<RegistrationDrugObs> IEnumerable<RegistrationDrugObs>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationDrugObs;
			}
		}

		#endregion

		private RegistrationDrugObsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationDrugObs' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationDrugObs ({RegistrationNo, DrugObsNo})")]
	[Serializable]
	public partial class RegistrationDrugObs : esRegistrationDrugObs
	{
		public RegistrationDrugObs()
		{
		}

		public RegistrationDrugObs(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationDrugObsMetadata.Meta();
			}
		}

		override protected esRegistrationDrugObsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationDrugObsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationDrugObsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationDrugObsQuery();
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
		public bool Load(RegistrationDrugObsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationDrugObsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationDrugObsQuery : esRegistrationDrugObsQuery
	{
		public RegistrationDrugObsQuery()
		{

		}

		public RegistrationDrugObsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationDrugObsQuery";
		}
	}

	[Serializable]
	public partial class RegistrationDrugObsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationDrugObsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.DrugObsNo, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.DrugObsNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.DrugObsDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.DrugObsDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.IsNeedPto, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.IsNeedPto;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.DrugInteractionRisk, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.DrugInteractionRisk;
			c.CharacterMaxLength = 800;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.Recommendation, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.Recommendation;
			c.CharacterMaxLength = 800;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.IsDeleted, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.IsDeleted;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.CreatedByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.CreatedDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationDrugObsMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationDrugObsMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationDrugObsMetadata Meta()
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
			public const string DrugObsNo = "DrugObsNo";
			public const string DrugObsDateTime = "DrugObsDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string IsNeedPto = "IsNeedPto";
			public const string DrugInteractionRisk = "DrugInteractionRisk";
			public const string Recommendation = "Recommendation";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string DrugObsNo = "DrugObsNo";
			public const string DrugObsDateTime = "DrugObsDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string IsNeedPto = "IsNeedPto";
			public const string DrugInteractionRisk = "DrugInteractionRisk";
			public const string Recommendation = "Recommendation";
			public const string IsDeleted = "IsDeleted";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(RegistrationDrugObsMetadata))
			{
				if (RegistrationDrugObsMetadata.mapDelegates == null)
				{
					RegistrationDrugObsMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationDrugObsMetadata.meta == null)
				{
					RegistrationDrugObsMetadata.meta = new RegistrationDrugObsMetadata();
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
				meta.AddTypeMap("DrugObsNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DrugObsDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNeedPto", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DrugInteractionRisk", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Recommendation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "RegistrationDrugObs";
				meta.Destination = "RegistrationDrugObs";
				meta.spInsert = "proc_RegistrationDrugObsInsert";
				meta.spUpdate = "proc_RegistrationDrugObsUpdate";
				meta.spDelete = "proc_RegistrationDrugObsDelete";
				meta.spLoadAll = "proc_RegistrationDrugObsLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationDrugObsLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationDrugObsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
