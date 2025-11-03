/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/20/2022 11:59:40 PM
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
	abstract public class esHomePrescriptionHdCollection : esEntityCollectionWAuditLog
	{
		public esHomePrescriptionHdCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "HomePrescriptionHdCollection";
		}

		#region Query Logic
		protected void InitQuery(esHomePrescriptionHdQuery query)
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
			this.InitQuery(query as esHomePrescriptionHdQuery);
		}
		#endregion

		virtual public HomePrescriptionHd DetachEntity(HomePrescriptionHd entity)
		{
			return base.DetachEntity(entity) as HomePrescriptionHd;
		}

		virtual public HomePrescriptionHd AttachEntity(HomePrescriptionHd entity)
		{
			return base.AttachEntity(entity) as HomePrescriptionHd;
		}

		virtual public void Combine(HomePrescriptionHdCollection collection)
		{
			base.Combine(collection);
		}

		new public HomePrescriptionHd this[int index]
		{
			get
			{
				return base[index] as HomePrescriptionHd;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HomePrescriptionHd);
		}
	}

	[Serializable]
	abstract public class esHomePrescriptionHd : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHomePrescriptionHdQuery GetDynamicQuery()
		{
			return null;
		}

		public esHomePrescriptionHd()
		{
		}

		public esHomePrescriptionHd(DataRow row)
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
			esHomePrescriptionHdQuery query = this.GetDynamicQuery();
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
						case "EduDateTime": this.str.EduDateTime = (string)value; break;
						case "StartDateTime": this.str.StartDateTime = (string)value; break;
						case "FinishDateTime": this.str.FinishDateTime = (string)value; break;
						case "EduByUserID": this.str.EduByUserID = (string)value; break;
						case "IsRecipientAsPatient": this.str.IsRecipientAsPatient = (string)value; break;
						case "RecipientName": this.str.RecipientName = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "IsNgt": this.str.IsNgt = (string)value; break;
						case "IsOralHygiene": this.str.IsOralHygiene = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EduDateTime":

							if (value == null || value is System.DateTime)
								this.EduDateTime = (System.DateTime?)value;
							break;
						case "StartDateTime":

							if (value == null || value is System.DateTime)
								this.StartDateTime = (System.DateTime?)value;
							break;
						case "FinishDateTime":

							if (value == null || value is System.DateTime)
								this.FinishDateTime = (System.DateTime?)value;
							break;
						case "IsRecipientAsPatient":

							if (value == null || value is System.Boolean)
								this.IsRecipientAsPatient = (System.Boolean?)value;
							break;
						case "IsNgt":

							if (value == null || value is System.Boolean)
								this.IsNgt = (System.Boolean?)value;
							break;
						case "IsOralHygiene":

							if (value == null || value is System.Boolean)
								this.IsOralHygiene = (System.Boolean?)value;
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
		/// Maps to HomePrescriptionHd.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(HomePrescriptionHdMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(HomePrescriptionHdMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.EduDateTime
		/// </summary>
		virtual public System.DateTime? EduDateTime
		{
			get
			{
				return base.GetSystemDateTime(HomePrescriptionHdMetadata.ColumnNames.EduDateTime);
			}

			set
			{
				base.SetSystemDateTime(HomePrescriptionHdMetadata.ColumnNames.EduDateTime, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.StartDateTime
		/// </summary>
		virtual public System.DateTime? StartDateTime
		{
			get
			{
				return base.GetSystemDateTime(HomePrescriptionHdMetadata.ColumnNames.StartDateTime);
			}

			set
			{
				base.SetSystemDateTime(HomePrescriptionHdMetadata.ColumnNames.StartDateTime, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.FinishDateTime
		/// </summary>
		virtual public System.DateTime? FinishDateTime
		{
			get
			{
				return base.GetSystemDateTime(HomePrescriptionHdMetadata.ColumnNames.FinishDateTime);
			}

			set
			{
				base.SetSystemDateTime(HomePrescriptionHdMetadata.ColumnNames.FinishDateTime, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.EduByUserID
		/// </summary>
		virtual public System.String EduByUserID
		{
			get
			{
				return base.GetSystemString(HomePrescriptionHdMetadata.ColumnNames.EduByUserID);
			}

			set
			{
				base.SetSystemString(HomePrescriptionHdMetadata.ColumnNames.EduByUserID, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.IsRecipientAsPatient
		/// </summary>
		virtual public System.Boolean? IsRecipientAsPatient
		{
			get
			{
				return base.GetSystemBoolean(HomePrescriptionHdMetadata.ColumnNames.IsRecipientAsPatient);
			}

			set
			{
				base.SetSystemBoolean(HomePrescriptionHdMetadata.ColumnNames.IsRecipientAsPatient, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.RecipientName
		/// </summary>
		virtual public System.String RecipientName
		{
			get
			{
				return base.GetSystemString(HomePrescriptionHdMetadata.ColumnNames.RecipientName);
			}

			set
			{
				base.SetSystemString(HomePrescriptionHdMetadata.ColumnNames.RecipientName, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(HomePrescriptionHdMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(HomePrescriptionHdMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.IsNgt
		/// </summary>
		virtual public System.Boolean? IsNgt
		{
			get
			{
				return base.GetSystemBoolean(HomePrescriptionHdMetadata.ColumnNames.IsNgt);
			}

			set
			{
				base.SetSystemBoolean(HomePrescriptionHdMetadata.ColumnNames.IsNgt, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.IsOralHygiene
		/// </summary>
		virtual public System.Boolean? IsOralHygiene
		{
			get
			{
				return base.GetSystemBoolean(HomePrescriptionHdMetadata.ColumnNames.IsOralHygiene);
			}

			set
			{
				base.SetSystemBoolean(HomePrescriptionHdMetadata.ColumnNames.IsOralHygiene, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(HomePrescriptionHdMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(HomePrescriptionHdMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to HomePrescriptionHd.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(HomePrescriptionHdMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(HomePrescriptionHdMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esHomePrescriptionHd entity)
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
			public System.String EduDateTime
			{
				get
				{
					System.DateTime? data = entity.EduDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EduDateTime = null;
					else entity.EduDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String StartDateTime
			{
				get
				{
					System.DateTime? data = entity.StartDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDateTime = null;
					else entity.StartDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String FinishDateTime
			{
				get
				{
					System.DateTime? data = entity.FinishDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FinishDateTime = null;
					else entity.FinishDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String EduByUserID
			{
				get
				{
					System.String data = entity.EduByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EduByUserID = null;
					else entity.EduByUserID = Convert.ToString(value);
				}
			}
			public System.String IsRecipientAsPatient
			{
				get
				{
					System.Boolean? data = entity.IsRecipientAsPatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRecipientAsPatient = null;
					else entity.IsRecipientAsPatient = Convert.ToBoolean(value);
				}
			}
			public System.String RecipientName
			{
				get
				{
					System.String data = entity.RecipientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecipientName = null;
					else entity.RecipientName = Convert.ToString(value);
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
			public System.String IsNgt
			{
				get
				{
					System.Boolean? data = entity.IsNgt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNgt = null;
					else entity.IsNgt = Convert.ToBoolean(value);
				}
			}
			public System.String IsOralHygiene
			{
				get
				{
					System.Boolean? data = entity.IsOralHygiene;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOralHygiene = null;
					else entity.IsOralHygiene = Convert.ToBoolean(value);
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
			private esHomePrescriptionHd entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHomePrescriptionHdQuery query)
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
				throw new Exception("esHomePrescriptionHd can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class HomePrescriptionHd : esHomePrescriptionHd
	{
	}

	[Serializable]
	abstract public class esHomePrescriptionHdQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return HomePrescriptionHdMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem EduDateTime
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.EduDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem StartDateTime
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.StartDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem FinishDateTime
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.FinishDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem EduByUserID
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.EduByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsRecipientAsPatient
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.IsRecipientAsPatient, esSystemType.Boolean);
			}
		}

		public esQueryItem RecipientName
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.RecipientName, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem IsNgt
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.IsNgt, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOralHygiene
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.IsOralHygiene, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, HomePrescriptionHdMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HomePrescriptionHdCollection")]
	public partial class HomePrescriptionHdCollection : esHomePrescriptionHdCollection, IEnumerable<HomePrescriptionHd>
	{
		public HomePrescriptionHdCollection()
		{

		}

		public static implicit operator List<HomePrescriptionHd>(HomePrescriptionHdCollection coll)
		{
			List<HomePrescriptionHd> list = new List<HomePrescriptionHd>();

			foreach (HomePrescriptionHd emp in coll)
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
				return HomePrescriptionHdMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HomePrescriptionHdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HomePrescriptionHd(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HomePrescriptionHd();
		}

		#endregion

		[BrowsableAttribute(false)]
		public HomePrescriptionHdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HomePrescriptionHdQuery();
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
		public bool Load(HomePrescriptionHdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public HomePrescriptionHd AddNew()
		{
			HomePrescriptionHd entity = base.AddNewEntity() as HomePrescriptionHd;

			return entity;
		}
		public HomePrescriptionHd FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as HomePrescriptionHd;
		}

		#region IEnumerable< HomePrescriptionHd> Members

		IEnumerator<HomePrescriptionHd> IEnumerable<HomePrescriptionHd>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as HomePrescriptionHd;
			}
		}

		#endregion

		private HomePrescriptionHdQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HomePrescriptionHd' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("HomePrescriptionHd ({RegistrationNo})")]
	[Serializable]
	public partial class HomePrescriptionHd : esHomePrescriptionHd
	{
		public HomePrescriptionHd()
		{
		}

		public HomePrescriptionHd(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HomePrescriptionHdMetadata.Meta();
			}
		}

		override protected esHomePrescriptionHdQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HomePrescriptionHdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public HomePrescriptionHdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HomePrescriptionHdQuery();
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
		public bool Load(HomePrescriptionHdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private HomePrescriptionHdQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class HomePrescriptionHdQuery : esHomePrescriptionHdQuery
	{
		public HomePrescriptionHdQuery()
		{

		}

		public HomePrescriptionHdQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "HomePrescriptionHdQuery";
		}
	}

	[Serializable]
	public partial class HomePrescriptionHdMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HomePrescriptionHdMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.EduDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.EduDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.StartDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.StartDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.FinishDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.FinishDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.EduByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.EduByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.IsRecipientAsPatient, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.IsRecipientAsPatient;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.RecipientName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.RecipientName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.ServiceUnitID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.IsNgt, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.IsNgt;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.IsOralHygiene, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.IsOralHygiene;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(HomePrescriptionHdMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = HomePrescriptionHdMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public HomePrescriptionHdMetadata Meta()
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
			public const string EduDateTime = "EduDateTime";
			public const string StartDateTime = "StartDateTime";
			public const string FinishDateTime = "FinishDateTime";
			public const string EduByUserID = "EduByUserID";
			public const string IsRecipientAsPatient = "IsRecipientAsPatient";
			public const string RecipientName = "RecipientName";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string IsNgt = "IsNgt";
			public const string IsOralHygiene = "IsOralHygiene";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string EduDateTime = "EduDateTime";
			public const string StartDateTime = "StartDateTime";
			public const string FinishDateTime = "FinishDateTime";
			public const string EduByUserID = "EduByUserID";
			public const string IsRecipientAsPatient = "IsRecipientAsPatient";
			public const string RecipientName = "RecipientName";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string IsNgt = "IsNgt";
			public const string IsOralHygiene = "IsOralHygiene";
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
			lock (typeof(HomePrescriptionHdMetadata))
			{
				if (HomePrescriptionHdMetadata.mapDelegates == null)
				{
					HomePrescriptionHdMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (HomePrescriptionHdMetadata.meta == null)
				{
					HomePrescriptionHdMetadata.meta = new HomePrescriptionHdMetadata();
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
				meta.AddTypeMap("EduDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("StartDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("FinishDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EduByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsRecipientAsPatient", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RecipientName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNgt", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOralHygiene", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "HomePrescriptionHd";
				meta.Destination = "HomePrescriptionHd";
				meta.spInsert = "proc_HomePrescriptionHdInsert";
				meta.spUpdate = "proc_HomePrescriptionHdUpdate";
				meta.spDelete = "proc_HomePrescriptionHdDelete";
				meta.spLoadAll = "proc_HomePrescriptionHdLoadAll";
				meta.spLoadByPrimaryKey = "proc_HomePrescriptionHdLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HomePrescriptionHdMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
