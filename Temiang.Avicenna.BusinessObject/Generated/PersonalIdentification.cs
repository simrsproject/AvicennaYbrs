/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/22/2021 8:46:54 PM
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
	abstract public class esPersonalIdentificationCollection : esEntityCollectionWAuditLog
	{
		public esPersonalIdentificationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalIdentificationCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalIdentificationQuery query)
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
			this.InitQuery(query as esPersonalIdentificationQuery);
		}
		#endregion

		virtual public PersonalIdentification DetachEntity(PersonalIdentification entity)
		{
			return base.DetachEntity(entity) as PersonalIdentification;
		}

		virtual public PersonalIdentification AttachEntity(PersonalIdentification entity)
		{
			return base.AttachEntity(entity) as PersonalIdentification;
		}

		virtual public void Combine(PersonalIdentificationCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalIdentification this[int index]
		{
			get
			{
				return base[index] as PersonalIdentification;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalIdentification);
		}
	}

	[Serializable]
	abstract public class esPersonalIdentification : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalIdentificationQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalIdentification()
		{
		}

		public esPersonalIdentification(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalIdentificationID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalIdentificationID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalIdentificationID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalIdentificationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalIdentificationID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalIdentificationID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalIdentificationID)
		{
			esPersonalIdentificationQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalIdentificationID == personalIdentificationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalIdentificationID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalIdentificationID", personalIdentificationID);
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
						case "PersonalIdentificationID": this.str.PersonalIdentificationID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRIdentificationType": this.str.SRIdentificationType = (string)value; break;
						case "IdentificationValue": this.str.IdentificationValue = (string)value; break;
						case "PlaceOfIssue": this.str.PlaceOfIssue = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IdentificationName": this.str.IdentificationName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalIdentificationID":

							if (value == null || value is System.Int32)
								this.PersonalIdentificationID = (System.Int32?)value;
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
		/// Maps to PersonalIdentification.PersonalIdentificationID
		/// </summary>
		virtual public System.Int32? PersonalIdentificationID
		{
			get
			{
				return base.GetSystemInt32(PersonalIdentificationMetadata.ColumnNames.PersonalIdentificationID);
			}

			set
			{
				base.SetSystemInt32(PersonalIdentificationMetadata.ColumnNames.PersonalIdentificationID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalIdentificationMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalIdentificationMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.SRIdentificationType
		/// </summary>
		virtual public System.String SRIdentificationType
		{
			get
			{
				return base.GetSystemString(PersonalIdentificationMetadata.ColumnNames.SRIdentificationType);
			}

			set
			{
				base.SetSystemString(PersonalIdentificationMetadata.ColumnNames.SRIdentificationType, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.IdentificationValue
		/// </summary>
		virtual public System.String IdentificationValue
		{
			get
			{
				return base.GetSystemString(PersonalIdentificationMetadata.ColumnNames.IdentificationValue);
			}

			set
			{
				base.SetSystemString(PersonalIdentificationMetadata.ColumnNames.IdentificationValue, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.PlaceOfIssue
		/// </summary>
		virtual public System.String PlaceOfIssue
		{
			get
			{
				return base.GetSystemString(PersonalIdentificationMetadata.ColumnNames.PlaceOfIssue);
			}

			set
			{
				base.SetSystemString(PersonalIdentificationMetadata.ColumnNames.PlaceOfIssue, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(PersonalIdentificationMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(PersonalIdentificationMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(PersonalIdentificationMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(PersonalIdentificationMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalIdentificationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalIdentificationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalIdentificationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalIdentificationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalIdentification.IdentificationName
		/// </summary>
		virtual public System.String IdentificationName
		{
			get
			{
				return base.GetSystemString(PersonalIdentificationMetadata.ColumnNames.IdentificationName);
			}

			set
			{
				base.SetSystemString(PersonalIdentificationMetadata.ColumnNames.IdentificationName, value);
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
			public esStrings(esPersonalIdentification entity)
			{
				this.entity = entity;
			}
			public System.String PersonalIdentificationID
			{
				get
				{
					System.Int32? data = entity.PersonalIdentificationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalIdentificationID = null;
					else entity.PersonalIdentificationID = Convert.ToInt32(value);
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
			public System.String SRIdentificationType
			{
				get
				{
					System.String data = entity.SRIdentificationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIdentificationType = null;
					else entity.SRIdentificationType = Convert.ToString(value);
				}
			}
			public System.String IdentificationValue
			{
				get
				{
					System.String data = entity.IdentificationValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdentificationValue = null;
					else entity.IdentificationValue = Convert.ToString(value);
				}
			}
			public System.String PlaceOfIssue
			{
				get
				{
					System.String data = entity.PlaceOfIssue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlaceOfIssue = null;
					else entity.PlaceOfIssue = Convert.ToString(value);
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
			public System.String IdentificationName
			{
				get
				{
					System.String data = entity.IdentificationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdentificationName = null;
					else entity.IdentificationName = Convert.ToString(value);
				}
			}
			private esPersonalIdentification entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalIdentificationQuery query)
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
				throw new Exception("esPersonalIdentification can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalIdentification : esPersonalIdentification
	{
	}

	[Serializable]
	abstract public class esPersonalIdentificationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalIdentificationMetadata.Meta();
			}
		}

		public esQueryItem PersonalIdentificationID
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.PersonalIdentificationID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRIdentificationType
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.SRIdentificationType, esSystemType.String);
			}
		}

		public esQueryItem IdentificationValue
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.IdentificationValue, esSystemType.String);
			}
		}

		public esQueryItem PlaceOfIssue
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.PlaceOfIssue, esSystemType.String);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IdentificationName
		{
			get
			{
				return new esQueryItem(this, PersonalIdentificationMetadata.ColumnNames.IdentificationName, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalIdentificationCollection")]
	public partial class PersonalIdentificationCollection : esPersonalIdentificationCollection, IEnumerable<PersonalIdentification>
	{
		public PersonalIdentificationCollection()
		{

		}

		public static implicit operator List<PersonalIdentification>(PersonalIdentificationCollection coll)
		{
			List<PersonalIdentification> list = new List<PersonalIdentification>();

			foreach (PersonalIdentification emp in coll)
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
				return PersonalIdentificationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalIdentificationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalIdentification(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalIdentification();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalIdentificationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalIdentificationQuery();
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
		public bool Load(PersonalIdentificationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalIdentification AddNew()
		{
			PersonalIdentification entity = base.AddNewEntity() as PersonalIdentification;

			return entity;
		}
		public PersonalIdentification FindByPrimaryKey(Int32 personalIdentificationID)
		{
			return base.FindByPrimaryKey(personalIdentificationID) as PersonalIdentification;
		}

		#region IEnumerable< PersonalIdentification> Members

		IEnumerator<PersonalIdentification> IEnumerable<PersonalIdentification>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalIdentification;
			}
		}

		#endregion

		private PersonalIdentificationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalIdentification' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalIdentification ({PersonalIdentificationID})")]
	[Serializable]
	public partial class PersonalIdentification : esPersonalIdentification
	{
		public PersonalIdentification()
		{
		}

		public PersonalIdentification(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalIdentificationMetadata.Meta();
			}
		}

		override protected esPersonalIdentificationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalIdentificationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalIdentificationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalIdentificationQuery();
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
		public bool Load(PersonalIdentificationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalIdentificationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalIdentificationQuery : esPersonalIdentificationQuery
	{
		public PersonalIdentificationQuery()
		{

		}

		public PersonalIdentificationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalIdentificationQuery";
		}
	}

	[Serializable]
	public partial class PersonalIdentificationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalIdentificationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.PersonalIdentificationID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.PersonalIdentificationID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.SRIdentificationType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.SRIdentificationType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.IdentificationValue, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.IdentificationValue;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.PlaceOfIssue, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.PlaceOfIssue;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.ValidFrom, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.ValidFrom;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.ValidTo, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalIdentificationMetadata.ColumnNames.IdentificationName, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalIdentificationMetadata.PropertyNames.IdentificationName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalIdentificationMetadata Meta()
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
			public const string PersonalIdentificationID = "PersonalIdentificationID";
			public const string PersonID = "PersonID";
			public const string SRIdentificationType = "SRIdentificationType";
			public const string IdentificationValue = "IdentificationValue";
			public const string PlaceOfIssue = "PlaceOfIssue";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IdentificationName = "IdentificationName";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalIdentificationID = "PersonalIdentificationID";
			public const string PersonID = "PersonID";
			public const string SRIdentificationType = "SRIdentificationType";
			public const string IdentificationValue = "IdentificationValue";
			public const string PlaceOfIssue = "PlaceOfIssue";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IdentificationName = "IdentificationName";
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
			lock (typeof(PersonalIdentificationMetadata))
			{
				if (PersonalIdentificationMetadata.mapDelegates == null)
				{
					PersonalIdentificationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalIdentificationMetadata.meta == null)
				{
					PersonalIdentificationMetadata.meta = new PersonalIdentificationMetadata();
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

				meta.AddTypeMap("PersonalIdentificationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRIdentificationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IdentificationValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PlaceOfIssue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IdentificationName", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalIdentification";
				meta.Destination = "PersonalIdentification";
				meta.spInsert = "proc_PersonalIdentificationInsert";
				meta.spUpdate = "proc_PersonalIdentificationUpdate";
				meta.spDelete = "proc_PersonalIdentificationDelete";
				meta.spLoadAll = "proc_PersonalIdentificationLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalIdentificationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalIdentificationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
