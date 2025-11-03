/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/21/2022 8:05:19 PM
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
	abstract public class esPersonalOrganizationCollection : esEntityCollectionWAuditLog
	{
		public esPersonalOrganizationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalOrganizationCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalOrganizationQuery query)
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
			this.InitQuery(query as esPersonalOrganizationQuery);
		}
		#endregion

		virtual public PersonalOrganization DetachEntity(PersonalOrganization entity)
		{
			return base.DetachEntity(entity) as PersonalOrganization;
		}

		virtual public PersonalOrganization AttachEntity(PersonalOrganization entity)
		{
			return base.AttachEntity(entity) as PersonalOrganization;
		}

		virtual public void Combine(PersonalOrganizationCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalOrganization this[int index]
		{
			get
			{
				return base[index] as PersonalOrganization;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalOrganization);
		}
	}

	[Serializable]
	abstract public class esPersonalOrganization : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalOrganizationQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalOrganization()
		{
		}

		public esPersonalOrganization(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalOrganizationID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalOrganizationID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalOrganizationID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalOrganizationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalOrganizationID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalOrganizationID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalOrganizationID)
		{
			esPersonalOrganizationQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalOrganizationID == personalOrganizationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalOrganizationID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalOrganizationID", personalOrganizationID);
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
						case "PersonalOrganizationID": this.str.PersonalOrganizationID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "OrganizationName": this.str.OrganizationName = (string)value; break;
						case "Location": this.str.Location = (string)value; break;
						case "SROrganizationType": this.str.SROrganizationType = (string)value; break;
						case "SROrganizationRole": this.str.SROrganizationRole = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalOrganizationID":

							if (value == null || value is System.Int32)
								this.PersonalOrganizationID = (System.Int32?)value;
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
		/// Maps to PersonalOrganization.PersonalOrganizationID
		/// </summary>
		virtual public System.Int32? PersonalOrganizationID
		{
			get
			{
				return base.GetSystemInt32(PersonalOrganizationMetadata.ColumnNames.PersonalOrganizationID);
			}

			set
			{
				base.SetSystemInt32(PersonalOrganizationMetadata.ColumnNames.PersonalOrganizationID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalOrganizationMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalOrganizationMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.OrganizationName
		/// </summary>
		virtual public System.String OrganizationName
		{
			get
			{
				return base.GetSystemString(PersonalOrganizationMetadata.ColumnNames.OrganizationName);
			}

			set
			{
				base.SetSystemString(PersonalOrganizationMetadata.ColumnNames.OrganizationName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.Location
		/// </summary>
		virtual public System.String Location
		{
			get
			{
				return base.GetSystemString(PersonalOrganizationMetadata.ColumnNames.Location);
			}

			set
			{
				base.SetSystemString(PersonalOrganizationMetadata.ColumnNames.Location, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.SROrganizationType
		/// </summary>
		virtual public System.String SROrganizationType
		{
			get
			{
				return base.GetSystemString(PersonalOrganizationMetadata.ColumnNames.SROrganizationType);
			}

			set
			{
				base.SetSystemString(PersonalOrganizationMetadata.ColumnNames.SROrganizationType, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.SROrganizationRole
		/// </summary>
		virtual public System.String SROrganizationRole
		{
			get
			{
				return base.GetSystemString(PersonalOrganizationMetadata.ColumnNames.SROrganizationRole);
			}

			set
			{
				base.SetSystemString(PersonalOrganizationMetadata.ColumnNames.SROrganizationRole, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(PersonalOrganizationMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(PersonalOrganizationMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(PersonalOrganizationMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(PersonalOrganizationMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(PersonalOrganizationMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(PersonalOrganizationMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalOrganizationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalOrganizationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalOrganization.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalOrganizationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalOrganizationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPersonalOrganization entity)
			{
				this.entity = entity;
			}
			public System.String PersonalOrganizationID
			{
				get
				{
					System.Int32? data = entity.PersonalOrganizationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalOrganizationID = null;
					else entity.PersonalOrganizationID = Convert.ToInt32(value);
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
			public System.String OrganizationName
			{
				get
				{
					System.String data = entity.OrganizationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationName = null;
					else entity.OrganizationName = Convert.ToString(value);
				}
			}
			public System.String Location
			{
				get
				{
					System.String data = entity.Location;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Location = null;
					else entity.Location = Convert.ToString(value);
				}
			}
			public System.String SROrganizationType
			{
				get
				{
					System.String data = entity.SROrganizationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROrganizationType = null;
					else entity.SROrganizationType = Convert.ToString(value);
				}
			}
			public System.String SROrganizationRole
			{
				get
				{
					System.String data = entity.SROrganizationRole;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROrganizationRole = null;
					else entity.SROrganizationRole = Convert.ToString(value);
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
			private esPersonalOrganization entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalOrganizationQuery query)
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
				throw new Exception("esPersonalOrganization can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalOrganization : esPersonalOrganization
	{
	}

	[Serializable]
	abstract public class esPersonalOrganizationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalOrganizationMetadata.Meta();
			}
		}

		public esQueryItem PersonalOrganizationID
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.PersonalOrganizationID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem OrganizationName
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.OrganizationName, esSystemType.String);
			}
		}

		public esQueryItem Location
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.Location, esSystemType.String);
			}
		}

		public esQueryItem SROrganizationType
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.SROrganizationType, esSystemType.String);
			}
		}

		public esQueryItem SROrganizationRole
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.SROrganizationRole, esSystemType.String);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalOrganizationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalOrganizationCollection")]
	public partial class PersonalOrganizationCollection : esPersonalOrganizationCollection, IEnumerable<PersonalOrganization>
	{
		public PersonalOrganizationCollection()
		{

		}

		public static implicit operator List<PersonalOrganization>(PersonalOrganizationCollection coll)
		{
			List<PersonalOrganization> list = new List<PersonalOrganization>();

			foreach (PersonalOrganization emp in coll)
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
				return PersonalOrganizationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalOrganizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalOrganization(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalOrganization();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalOrganizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalOrganizationQuery();
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
		public bool Load(PersonalOrganizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalOrganization AddNew()
		{
			PersonalOrganization entity = base.AddNewEntity() as PersonalOrganization;

			return entity;
		}
		public PersonalOrganization FindByPrimaryKey(Int32 personalOrganizationID)
		{
			return base.FindByPrimaryKey(personalOrganizationID) as PersonalOrganization;
		}

		#region IEnumerable< PersonalOrganization> Members

		IEnumerator<PersonalOrganization> IEnumerable<PersonalOrganization>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalOrganization;
			}
		}

		#endregion

		private PersonalOrganizationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalOrganization' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalOrganization ({PersonalOrganizationID})")]
	[Serializable]
	public partial class PersonalOrganization : esPersonalOrganization
	{
		public PersonalOrganization()
		{
		}

		public PersonalOrganization(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalOrganizationMetadata.Meta();
			}
		}

		override protected esPersonalOrganizationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalOrganizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalOrganizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalOrganizationQuery();
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
		public bool Load(PersonalOrganizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalOrganizationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalOrganizationQuery : esPersonalOrganizationQuery
	{
		public PersonalOrganizationQuery()
		{

		}

		public PersonalOrganizationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalOrganizationQuery";
		}
	}

	[Serializable]
	public partial class PersonalOrganizationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalOrganizationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.PersonalOrganizationID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.PersonalOrganizationID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.OrganizationName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.OrganizationName;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.Location, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.Location;
			c.CharacterMaxLength = 60;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.SROrganizationType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.SROrganizationType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.SROrganizationRole, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.SROrganizationRole;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.ValidFrom, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.ValidFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.ValidTo, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.Note, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalOrganizationMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalOrganizationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public PersonalOrganizationMetadata Meta()
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
			public const string PersonalOrganizationID = "PersonalOrganizationID";
			public const string PersonID = "PersonID";
			public const string OrganizationName = "OrganizationName";
			public const string Location = "Location";
			public const string SROrganizationType = "SROrganizationType";
			public const string SROrganizationRole = "SROrganizationRole";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalOrganizationID = "PersonalOrganizationID";
			public const string PersonID = "PersonID";
			public const string OrganizationName = "OrganizationName";
			public const string Location = "Location";
			public const string SROrganizationType = "SROrganizationType";
			public const string SROrganizationRole = "SROrganizationRole";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string Note = "Note";
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
			lock (typeof(PersonalOrganizationMetadata))
			{
				if (PersonalOrganizationMetadata.mapDelegates == null)
				{
					PersonalOrganizationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalOrganizationMetadata.meta == null)
				{
					PersonalOrganizationMetadata.meta = new PersonalOrganizationMetadata();
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

				meta.AddTypeMap("PersonalOrganizationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrganizationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Location", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROrganizationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROrganizationRole", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalOrganization";
				meta.Destination = "PersonalOrganization";
				meta.spInsert = "proc_PersonalOrganizationInsert";
				meta.spUpdate = "proc_PersonalOrganizationUpdate";
				meta.spDelete = "proc_PersonalOrganizationDelete";
				meta.spLoadAll = "proc_PersonalOrganizationLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalOrganizationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalOrganizationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
