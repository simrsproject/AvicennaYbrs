/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/24/2022 10:55:55 AM
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
	abstract public class esEmployeeEmploymentPeriodCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeEmploymentPeriodCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeEmploymentPeriodCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeEmploymentPeriodQuery query)
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
			this.InitQuery(query as esEmployeeEmploymentPeriodQuery);
		}
		#endregion

		virtual public EmployeeEmploymentPeriod DetachEntity(EmployeeEmploymentPeriod entity)
		{
			return base.DetachEntity(entity) as EmployeeEmploymentPeriod;
		}

		virtual public EmployeeEmploymentPeriod AttachEntity(EmployeeEmploymentPeriod entity)
		{
			return base.AttachEntity(entity) as EmployeeEmploymentPeriod;
		}

		virtual public void Combine(EmployeeEmploymentPeriodCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeEmploymentPeriod this[int index]
		{
			get
			{
				return base[index] as EmployeeEmploymentPeriod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeEmploymentPeriod);
		}
	}

	[Serializable]
	abstract public class esEmployeeEmploymentPeriod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeEmploymentPeriodQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeEmploymentPeriod()
		{
		}

		public esEmployeeEmploymentPeriod(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeEmploymentPeriodID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeEmploymentPeriodID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeEmploymentPeriodID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeEmploymentPeriodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeEmploymentPeriodID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeEmploymentPeriodID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeEmploymentPeriodID)
		{
			esEmployeeEmploymentPeriodQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeEmploymentPeriodID == employeeEmploymentPeriodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeEmploymentPeriodID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeEmploymentPeriodID", employeeEmploymentPeriodID);
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
						case "EmployeeEmploymentPeriodID": this.str.EmployeeEmploymentPeriodID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "RecruitmentPlanID": this.str.RecruitmentPlanID = (string)value; break;
						case "SREmploymentCategory": this.str.SREmploymentCategory = (string)value; break;
						case "EmployeeNumber": this.str.EmployeeNumber = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeEmploymentPeriodID":

							if (value == null || value is System.Int32)
								this.EmployeeEmploymentPeriodID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "ValidFrom":

							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						case "ValidTo":

							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "RecruitmentPlanID":

							if (value == null || value is System.Int32)
								this.RecruitmentPlanID = (System.Int32?)value;
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
		/// Maps to EmployeeEmploymentPeriod.EmployeeEmploymentPeriodID
		/// </summary>
		virtual public System.Int32? EmployeeEmploymentPeriodID
		{
			get
			{
				return base.GetSystemInt32(EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeEmploymentPeriodID);
			}

			set
			{
				base.SetSystemInt32(EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeEmploymentPeriodID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeEmploymentPeriodMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeEmploymentPeriodMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentType);
			}

			set
			{
				base.SetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEmploymentPeriodMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEmploymentPeriodMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEmploymentPeriodMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEmploymentPeriodMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEmploymentPeriodMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEmploymentPeriodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.RecruitmentPlanID
		/// </summary>
		virtual public System.Int32? RecruitmentPlanID
		{
			get
			{
				return base.GetSystemInt32(EmployeeEmploymentPeriodMetadata.ColumnNames.RecruitmentPlanID);
			}

			set
			{
				base.SetSystemInt32(EmployeeEmploymentPeriodMetadata.ColumnNames.RecruitmentPlanID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.SREmploymentCategory
		/// </summary>
		virtual public System.String SREmploymentCategory
		{
			get
			{
				return base.GetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentCategory);
			}

			set
			{
				base.SetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentCategory, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEmploymentPeriod.EmployeeNumber
		/// </summary>
		virtual public System.String EmployeeNumber
		{
			get
			{
				return base.GetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeNumber);
			}

			set
			{
				base.SetSystemString(EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeNumber, value);
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
			public esStrings(esEmployeeEmploymentPeriod entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeEmploymentPeriodID
			{
				get
				{
					System.Int32? data = entity.EmployeeEmploymentPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeEmploymentPeriodID = null;
					else entity.EmployeeEmploymentPeriodID = Convert.ToInt32(value);
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
			public System.String SREmploymentType
			{
				get
				{
					System.String data = entity.SREmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentType = null;
					else entity.SREmploymentType = Convert.ToString(value);
				}
			}
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
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
			public System.String RecruitmentPlanID
			{
				get
				{
					System.Int32? data = entity.RecruitmentPlanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecruitmentPlanID = null;
					else entity.RecruitmentPlanID = Convert.ToInt32(value);
				}
			}
			public System.String SREmploymentCategory
			{
				get
				{
					System.String data = entity.SREmploymentCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentCategory = null;
					else entity.SREmploymentCategory = Convert.ToString(value);
				}
			}
			public System.String EmployeeNumber
			{
				get
				{
					System.String data = entity.EmployeeNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeNumber = null;
					else entity.EmployeeNumber = Convert.ToString(value);
				}
			}
			private esEmployeeEmploymentPeriod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeEmploymentPeriodQuery query)
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
				throw new Exception("esEmployeeEmploymentPeriod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeEmploymentPeriod : esEmployeeEmploymentPeriod
	{
	}

	[Serializable]
	abstract public class esEmployeeEmploymentPeriodQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeEmploymentPeriodMetadata.Meta();
			}
		}

		public esQueryItem EmployeeEmploymentPeriodID
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeEmploymentPeriodID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem RecruitmentPlanID
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.RecruitmentPlanID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmploymentCategory
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentCategory, esSystemType.String);
			}
		}

		public esQueryItem EmployeeNumber
		{
			get
			{
				return new esQueryItem(this, EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeNumber, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeEmploymentPeriodCollection")]
	public partial class EmployeeEmploymentPeriodCollection : esEmployeeEmploymentPeriodCollection, IEnumerable<EmployeeEmploymentPeriod>
	{
		public EmployeeEmploymentPeriodCollection()
		{

		}

		public static implicit operator List<EmployeeEmploymentPeriod>(EmployeeEmploymentPeriodCollection coll)
		{
			List<EmployeeEmploymentPeriod> list = new List<EmployeeEmploymentPeriod>();

			foreach (EmployeeEmploymentPeriod emp in coll)
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
				return EmployeeEmploymentPeriodMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeEmploymentPeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeEmploymentPeriod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeEmploymentPeriod();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeEmploymentPeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeEmploymentPeriodQuery();
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
		public bool Load(EmployeeEmploymentPeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeEmploymentPeriod AddNew()
		{
			EmployeeEmploymentPeriod entity = base.AddNewEntity() as EmployeeEmploymentPeriod;

			return entity;
		}
		public EmployeeEmploymentPeriod FindByPrimaryKey(Int32 employeeEmploymentPeriodID)
		{
			return base.FindByPrimaryKey(employeeEmploymentPeriodID) as EmployeeEmploymentPeriod;
		}

		#region IEnumerable< EmployeeEmploymentPeriod> Members

		IEnumerator<EmployeeEmploymentPeriod> IEnumerable<EmployeeEmploymentPeriod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeEmploymentPeriod;
			}
		}

		#endregion

		private EmployeeEmploymentPeriodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeEmploymentPeriod' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeEmploymentPeriod ({EmployeeEmploymentPeriodID})")]
	[Serializable]
	public partial class EmployeeEmploymentPeriod : esEmployeeEmploymentPeriod
	{
		public EmployeeEmploymentPeriod()
		{
		}

		public EmployeeEmploymentPeriod(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeEmploymentPeriodMetadata.Meta();
			}
		}

		override protected esEmployeeEmploymentPeriodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeEmploymentPeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeEmploymentPeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeEmploymentPeriodQuery();
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
		public bool Load(EmployeeEmploymentPeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeEmploymentPeriodQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeEmploymentPeriodQuery : esEmployeeEmploymentPeriodQuery
	{
		public EmployeeEmploymentPeriodQuery()
		{

		}

		public EmployeeEmploymentPeriodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeEmploymentPeriodQuery";
		}
	}

	[Serializable]
	public partial class EmployeeEmploymentPeriodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeEmploymentPeriodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeEmploymentPeriodID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.EmployeeEmploymentPeriodID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.ValidFrom, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.ValidTo, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.Note, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.RecruitmentPlanID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.RecruitmentPlanID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.SREmploymentCategory, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.SREmploymentCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEmploymentPeriodMetadata.ColumnNames.EmployeeNumber, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEmploymentPeriodMetadata.PropertyNames.EmployeeNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeEmploymentPeriodMetadata Meta()
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
			public const string EmployeeEmploymentPeriodID = "EmployeeEmploymentPeriodID";
			public const string PersonID = "PersonID";
			public const string SREmploymentType = "SREmploymentType";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string RecruitmentPlanID = "RecruitmentPlanID";
			public const string SREmploymentCategory = "SREmploymentCategory";
			public const string EmployeeNumber = "EmployeeNumber";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeEmploymentPeriodID = "EmployeeEmploymentPeriodID";
			public const string PersonID = "PersonID";
			public const string SREmploymentType = "SREmploymentType";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string RecruitmentPlanID = "RecruitmentPlanID";
			public const string SREmploymentCategory = "SREmploymentCategory";
			public const string EmployeeNumber = "EmployeeNumber";
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
			lock (typeof(EmployeeEmploymentPeriodMetadata))
			{
				if (EmployeeEmploymentPeriodMetadata.mapDelegates == null)
				{
					EmployeeEmploymentPeriodMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeEmploymentPeriodMetadata.meta == null)
				{
					EmployeeEmploymentPeriodMetadata.meta = new EmployeeEmploymentPeriodMetadata();
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

				meta.AddTypeMap("EmployeeEmploymentPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecruitmentPlanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmploymentCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeNumber", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeEmploymentPeriod";
				meta.Destination = "EmployeeEmploymentPeriod";
				meta.spInsert = "proc_EmployeeEmploymentPeriodInsert";
				meta.spUpdate = "proc_EmployeeEmploymentPeriodUpdate";
				meta.spDelete = "proc_EmployeeEmploymentPeriodDelete";
				meta.spLoadAll = "proc_EmployeeEmploymentPeriodLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeEmploymentPeriodLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeEmploymentPeriodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
