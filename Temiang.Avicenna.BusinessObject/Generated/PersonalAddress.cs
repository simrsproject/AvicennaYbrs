/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/24/2020 1:15:01 PM
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
	abstract public class esPersonalAddressCollection : esEntityCollectionWAuditLog
	{
		public esPersonalAddressCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalAddressCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalAddressQuery query)
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
			this.InitQuery(query as esPersonalAddressQuery);
		}
		#endregion

		virtual public PersonalAddress DetachEntity(PersonalAddress entity)
		{
			return base.DetachEntity(entity) as PersonalAddress;
		}

		virtual public PersonalAddress AttachEntity(PersonalAddress entity)
		{
			return base.AttachEntity(entity) as PersonalAddress;
		}

		virtual public void Combine(PersonalAddressCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalAddress this[int index]
		{
			get
			{
				return base[index] as PersonalAddress;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalAddress);
		}
	}

	[Serializable]
	abstract public class esPersonalAddress : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalAddressQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalAddress()
		{
		}

		public esPersonalAddress(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalAddressID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalAddressID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalAddressID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalAddressID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalAddressID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalAddressID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalAddressID)
		{
			esPersonalAddressQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalAddressID == personalAddressID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalAddressID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalAddressID", personalAddressID);
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
						case "PersonalAddressID": this.str.PersonalAddressID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRAddressType": this.str.SRAddressType = (string)value; break;
						case "Address": this.str.Address = (string)value; break;
						case "SRState": this.str.SRState = (string)value; break;
						case "SRCity": this.str.SRCity = (string)value; break;
						case "ZipCode": this.str.ZipCode = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "District": this.str.District = (string)value; break;
						case "County": this.str.County = (string)value; break;
						case "City": this.str.City = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalAddressID":

							if (value == null || value is System.Int32)
								this.PersonalAddressID = (System.Int32?)value;
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
		/// Maps to PersonalAddress.PersonalAddressID
		/// </summary>
		virtual public System.Int32? PersonalAddressID
		{
			get
			{
				return base.GetSystemInt32(PersonalAddressMetadata.ColumnNames.PersonalAddressID);
			}

			set
			{
				base.SetSystemInt32(PersonalAddressMetadata.ColumnNames.PersonalAddressID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalAddressMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalAddressMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.SRAddressType
		/// </summary>
		virtual public System.String SRAddressType
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.SRAddressType);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.SRAddressType, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.Address);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.Address, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.SRState
		/// </summary>
		virtual public System.String SRState
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.SRState);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.SRState, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.SRCity
		/// </summary>
		virtual public System.String SRCity
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.SRCity);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.SRCity, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.ZipCode
		/// </summary>
		virtual public System.String ZipCode
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.ZipCode);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.ZipCode, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalAddressMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalAddressMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.District
		/// </summary>
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.District);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.District, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.County
		/// </summary>
		virtual public System.String County
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.County);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.County, value);
			}
		}
		/// <summary>
		/// Maps to PersonalAddress.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(PersonalAddressMetadata.ColumnNames.City);
			}

			set
			{
				base.SetSystemString(PersonalAddressMetadata.ColumnNames.City, value);
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
			public esStrings(esPersonalAddress entity)
			{
				this.entity = entity;
			}
			public System.String PersonalAddressID
			{
				get
				{
					System.Int32? data = entity.PersonalAddressID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalAddressID = null;
					else entity.PersonalAddressID = Convert.ToInt32(value);
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
			public System.String SRAddressType
			{
				get
				{
					System.String data = entity.SRAddressType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAddressType = null;
					else entity.SRAddressType = Convert.ToString(value);
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
			private esPersonalAddress entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalAddressQuery query)
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
				throw new Exception("esPersonalAddress can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalAddress : esPersonalAddress
	{
	}

	[Serializable]
	abstract public class esPersonalAddressQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalAddressMetadata.Meta();
			}
		}

		public esQueryItem PersonalAddressID
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.PersonalAddressID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRAddressType
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.SRAddressType, esSystemType.String);
			}
		}

		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.Address, esSystemType.String);
			}
		}

		public esQueryItem SRState
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.SRState, esSystemType.String);
			}
		}

		public esQueryItem SRCity
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.SRCity, esSystemType.String);
			}
		}

		public esQueryItem ZipCode
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.ZipCode, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem District
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.District, esSystemType.String);
			}
		}

		public esQueryItem County
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.County, esSystemType.String);
			}
		}

		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, PersonalAddressMetadata.ColumnNames.City, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalAddressCollection")]
	public partial class PersonalAddressCollection : esPersonalAddressCollection, IEnumerable<PersonalAddress>
	{
		public PersonalAddressCollection()
		{

		}

		public static implicit operator List<PersonalAddress>(PersonalAddressCollection coll)
		{
			List<PersonalAddress> list = new List<PersonalAddress>();

			foreach (PersonalAddress emp in coll)
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
				return PersonalAddressMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalAddressQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalAddress(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalAddress();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalAddressQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalAddressQuery();
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
		public bool Load(PersonalAddressQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalAddress AddNew()
		{
			PersonalAddress entity = base.AddNewEntity() as PersonalAddress;

			return entity;
		}
		public PersonalAddress FindByPrimaryKey(Int32 personalAddressID)
		{
			return base.FindByPrimaryKey(personalAddressID) as PersonalAddress;
		}

		#region IEnumerable< PersonalAddress> Members

		IEnumerator<PersonalAddress> IEnumerable<PersonalAddress>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalAddress;
			}
		}

		#endregion

		private PersonalAddressQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalAddress' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalAddress ({PersonalAddressID})")]
	[Serializable]
	public partial class PersonalAddress : esPersonalAddress
	{
		public PersonalAddress()
		{
		}

		public PersonalAddress(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalAddressMetadata.Meta();
			}
		}

		override protected esPersonalAddressQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalAddressQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalAddressQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalAddressQuery();
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
		public bool Load(PersonalAddressQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalAddressQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalAddressQuery : esPersonalAddressQuery
	{
		public PersonalAddressQuery()
		{

		}

		public PersonalAddressQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalAddressQuery";
		}
	}

	[Serializable]
	public partial class PersonalAddressMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalAddressMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.PersonalAddressID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.PersonalAddressID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.SRAddressType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.SRAddressType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.Address, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.SRState, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.SRState;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.SRCity, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.SRCity;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.ZipCode, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.ZipCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.District, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.District;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.County, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.County;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalAddressMetadata.ColumnNames.City, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalAddressMetadata.PropertyNames.City;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalAddressMetadata Meta()
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
			public const string PersonalAddressID = "PersonalAddressID";
			public const string PersonID = "PersonID";
			public const string SRAddressType = "SRAddressType";
			public const string Address = "Address";
			public const string SRState = "SRState";
			public const string SRCity = "SRCity";
			public const string ZipCode = "ZipCode";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string District = "District";
			public const string County = "County";
			public const string City = "City";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalAddressID = "PersonalAddressID";
			public const string PersonID = "PersonID";
			public const string SRAddressType = "SRAddressType";
			public const string Address = "Address";
			public const string SRState = "SRState";
			public const string SRCity = "SRCity";
			public const string ZipCode = "ZipCode";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string District = "District";
			public const string County = "County";
			public const string City = "City";
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
			lock (typeof(PersonalAddressMetadata))
			{
				if (PersonalAddressMetadata.mapDelegates == null)
				{
					PersonalAddressMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalAddressMetadata.meta == null)
				{
					PersonalAddressMetadata.meta = new PersonalAddressMetadata();
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

				meta.AddTypeMap("PersonalAddressID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRAddressType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRState", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCity", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ZipCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("County", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalAddress";
				meta.Destination = "PersonalAddress";
				meta.spInsert = "proc_PersonalAddressInsert";
				meta.spUpdate = "proc_PersonalAddressUpdate";
				meta.spDelete = "proc_PersonalAddressDelete";
				meta.spLoadAll = "proc_PersonalAddressLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalAddressLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalAddressMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
