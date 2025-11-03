/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/16/2022 1:31:05 PM
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
	abstract public class esPersonalEmergencyContactCollection : esEntityCollectionWAuditLog
	{
		public esPersonalEmergencyContactCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalEmergencyContactCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalEmergencyContactQuery query)
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
			this.InitQuery(query as esPersonalEmergencyContactQuery);
		}
		#endregion

		virtual public PersonalEmergencyContact DetachEntity(PersonalEmergencyContact entity)
		{
			return base.DetachEntity(entity) as PersonalEmergencyContact;
		}

		virtual public PersonalEmergencyContact AttachEntity(PersonalEmergencyContact entity)
		{
			return base.AttachEntity(entity) as PersonalEmergencyContact;
		}

		virtual public void Combine(PersonalEmergencyContactCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalEmergencyContact this[int index]
		{
			get
			{
				return base[index] as PersonalEmergencyContact;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalEmergencyContact);
		}
	}

	[Serializable]
	abstract public class esPersonalEmergencyContact : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalEmergencyContactQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalEmergencyContact()
		{
		}

		public esPersonalEmergencyContact(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalEmergencyContactID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalEmergencyContactID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalEmergencyContactID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalEmergencyContactID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalEmergencyContactID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalEmergencyContactID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalEmergencyContactID)
		{
			esPersonalEmergencyContactQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalEmergencyContactID == personalEmergencyContactID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalEmergencyContactID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalEmergencyContactID", personalEmergencyContactID);
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
						case "PersonalEmergencyContactID": this.str.PersonalEmergencyContactID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "ContactName": this.str.ContactName = (string)value; break;
						case "Address": this.str.Address = (string)value; break;
						case "SRState": this.str.SRState = (string)value; break;
						case "SRCity": this.str.SRCity = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "Phone": this.str.Phone = (string)value; break;
						case "Mobile": this.str.Mobile = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "City": this.str.City = (string)value; break;
						case "SRFamilyRelation": this.str.SRFamilyRelation = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalEmergencyContactID":

							if (value == null || value is System.Int32)
								this.PersonalEmergencyContactID = (System.Int32?)value;
							break;
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
		/// Maps to PersonalEmergencyContact.PersonalEmergencyContactID
		/// </summary>
		virtual public System.Int32? PersonalEmergencyContactID
		{
			get
			{
				return base.GetSystemInt32(PersonalEmergencyContactMetadata.ColumnNames.PersonalEmergencyContactID);
			}

			set
			{
				base.SetSystemInt32(PersonalEmergencyContactMetadata.ColumnNames.PersonalEmergencyContactID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalEmergencyContactMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalEmergencyContactMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.ContactName
		/// </summary>
		virtual public System.String ContactName
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.ContactName);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.ContactName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.Address);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.Address, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.SRState
		/// </summary>
		virtual public System.String SRState
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.SRState);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.SRState, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.SRCity
		/// </summary>
		virtual public System.String SRCity
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.SRCity);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.SRCity, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.Phone
		/// </summary>
		virtual public System.String Phone
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.Phone);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.Phone, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.Mobile
		/// </summary>
		virtual public System.String Mobile
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.Mobile);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.Mobile, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalEmergencyContactMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalEmergencyContactMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.District);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.County);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.City, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEmergencyContact.SRFamilyRelation
		/// </summary>
		virtual public System.String SRFamilyRelation
		{
			get
			{
				return base.GetSystemString(PersonalEmergencyContactMetadata.ColumnNames.SRFamilyRelation);
			}

			set
			{
				base.SetSystemString(PersonalEmergencyContactMetadata.ColumnNames.SRFamilyRelation, value);
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
			public esStrings(esPersonalEmergencyContact entity)
			{
				this.entity = entity;
			}
			public System.String PersonalEmergencyContactID
			{
				get
				{
					System.Int32? data = entity.PersonalEmergencyContactID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalEmergencyContactID = null;
					else entity.PersonalEmergencyContactID = Convert.ToInt32(value);
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
			public System.String ContactName
			{
				get
				{
					System.String data = entity.ContactName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactName = null;
					else entity.ContactName = Convert.ToString(value);
				}
			}
			public System.String Address
			{
				get
				{
					System.String data = entity.Address;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address = null;
					else entity.Address = Convert.ToString(value);
				}
			}
			public System.String SRState
			{
				get
				{
					System.String data = entity.SRState;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRState = null;
					else entity.SRState = Convert.ToString(value);
				}
			}
			public System.String SRCity
			{
				get
				{
					System.String data = entity.SRCity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCity = null;
					else entity.SRCity = Convert.ToString(value);
				}
			}
			public System.String ZipCode
			{
				get
				{
					System.String data = entity.ZipCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ZipCode = null;
					else entity.ZipCode = Convert.ToString(value);
				}
			}
			public System.String Phone
			{
				get
				{
					System.String data = entity.Phone;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Phone = null;
					else entity.Phone = Convert.ToString(value);
				}
			}
			public System.String Mobile
			{
				get
				{
					System.String data = entity.Mobile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Mobile = null;
					else entity.Mobile = Convert.ToString(value);
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
			public System.String District
			{
				get
				{
					System.String data = entity.District;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.District = null;
					else entity.District = Convert.ToString(value);
				}
			}
			public System.String County
			{
				get
				{
					System.String data = entity.County;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.County = null;
					else entity.County = Convert.ToString(value);
				}
			}
			public System.String City
			{
				get
				{
					System.String data = entity.City;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.City = null;
					else entity.City = Convert.ToString(value);
				}
			}
			public System.String SRFamilyRelation
			{
				get
				{
					System.String data = entity.SRFamilyRelation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFamilyRelation = null;
					else entity.SRFamilyRelation = Convert.ToString(value);
				}
			}
			private esPersonalEmergencyContact entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalEmergencyContactQuery query)
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
				throw new Exception("esPersonalEmergencyContact can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalEmergencyContact : esPersonalEmergencyContact
	{
	}

	[Serializable]
	abstract public class esPersonalEmergencyContactQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalEmergencyContactMetadata.Meta();
			}
		}

		public esQueryItem PersonalEmergencyContactID
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.PersonalEmergencyContactID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem ContactName
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.ContactName, esSystemType.String);
			}
		}

		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.Address, esSystemType.String);
			}
		}

		public esQueryItem SRState
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.SRState, esSystemType.String);
			}
		}

		public esQueryItem SRCity
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.SRCity, esSystemType.String);
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem Phone
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.Phone, esSystemType.String);
			}
		}

		public esQueryItem Mobile
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.Mobile, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.District, esSystemType.String);
			}
		}

		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.County, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.City, esSystemType.String);
			}
		}

		public esQueryItem SRFamilyRelation
		{
			get
			{
				return new esQueryItem(this, PersonalEmergencyContactMetadata.ColumnNames.SRFamilyRelation, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalEmergencyContactCollection")]
	public partial class PersonalEmergencyContactCollection : esPersonalEmergencyContactCollection, IEnumerable<PersonalEmergencyContact>
	{
		public PersonalEmergencyContactCollection()
		{

		}

		public static implicit operator List<PersonalEmergencyContact>(PersonalEmergencyContactCollection coll)
		{
			List<PersonalEmergencyContact> list = new List<PersonalEmergencyContact>();

			foreach (PersonalEmergencyContact emp in coll)
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
				return PersonalEmergencyContactMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalEmergencyContactQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalEmergencyContact(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalEmergencyContact();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalEmergencyContactQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalEmergencyContactQuery();
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
		public bool Load(PersonalEmergencyContactQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalEmergencyContact AddNew()
		{
			PersonalEmergencyContact entity = base.AddNewEntity() as PersonalEmergencyContact;

			return entity;
		}
		public PersonalEmergencyContact FindByPrimaryKey(Int32 personalEmergencyContactID)
		{
			return base.FindByPrimaryKey(personalEmergencyContactID) as PersonalEmergencyContact;
		}

		#region IEnumerable< PersonalEmergencyContact> Members

		IEnumerator<PersonalEmergencyContact> IEnumerable<PersonalEmergencyContact>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalEmergencyContact;
			}
		}

		#endregion

		private PersonalEmergencyContactQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalEmergencyContact' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalEmergencyContact ({PersonalEmergencyContactID})")]
	[Serializable]
	public partial class PersonalEmergencyContact : esPersonalEmergencyContact
	{
		public PersonalEmergencyContact()
		{
		}

		public PersonalEmergencyContact(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalEmergencyContactMetadata.Meta();
			}
		}

		override protected esPersonalEmergencyContactQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalEmergencyContactQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalEmergencyContactQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalEmergencyContactQuery();
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
		public bool Load(PersonalEmergencyContactQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalEmergencyContactQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalEmergencyContactQuery : esPersonalEmergencyContactQuery
	{
		public PersonalEmergencyContactQuery()
		{

		}

		public PersonalEmergencyContactQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalEmergencyContactQuery";
		}
	}

	[Serializable]
	public partial class PersonalEmergencyContactMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalEmergencyContactMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.PersonalEmergencyContactID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.PersonalEmergencyContactID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.ContactName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.ContactName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.Address, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.SRState, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.SRState;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.SRCity, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.SRCity;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.ZipCode, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.Phone, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.Phone;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.Mobile, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.Mobile;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.District, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.County, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.City, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEmergencyContactMetadata.ColumnNames.SRFamilyRelation, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEmergencyContactMetadata.PropertyNames.SRFamilyRelation;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalEmergencyContactMetadata Meta()
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
			public const string PersonalEmergencyContactID = "PersonalEmergencyContactID";
			public const string PersonID = "PersonID";
			public const string ContactName = "ContactName";
			public const string Address = "Address";
			public const string SRState = "SRState";
			public const string SRCity = "SRCity";
			public const string ZipCode = "ZipCode";
			public const string Phone = "Phone";
			public const string Mobile = "Mobile";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string District = "District";
			public const string County = "County";
			public const string City = "City";
			public const string SRFamilyRelation = "SRFamilyRelation";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalEmergencyContactID = "PersonalEmergencyContactID";
			public const string PersonID = "PersonID";
			public const string ContactName = "ContactName";
			public const string Address = "Address";
			public const string SRState = "SRState";
			public const string SRCity = "SRCity";
			public const string ZipCode = "ZipCode";
			public const string Phone = "Phone";
			public const string Mobile = "Mobile";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string District = "District";
			public const string County = "County";
			public const string City = "City";
			public const string SRFamilyRelation = "SRFamilyRelation";
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
			lock (typeof(PersonalEmergencyContactMetadata))
			{
				if (PersonalEmergencyContactMetadata.mapDelegates == null)
				{
					PersonalEmergencyContactMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalEmergencyContactMetadata.meta == null)
				{
					PersonalEmergencyContactMetadata.meta = new PersonalEmergencyContactMetadata();
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

				meta.AddTypeMap("PersonalEmergencyContactID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ContactName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRState", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Phone", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Mobile", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFamilyRelation", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalEmergencyContact";
				meta.Destination = "PersonalEmergencyContact";
				meta.spInsert = "proc_PersonalEmergencyContactInsert";
				meta.spUpdate = "proc_PersonalEmergencyContactUpdate";
				meta.spDelete = "proc_PersonalEmergencyContactDelete";
				meta.spLoadAll = "proc_PersonalEmergencyContactLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalEmergencyContactLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalEmergencyContactMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
