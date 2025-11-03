/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/22/2021 7:46:51 PM
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
	abstract public class esPersonalLicenceCollection : esEntityCollectionWAuditLog
	{
		public esPersonalLicenceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalLicenceCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalLicenceQuery query)
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
			this.InitQuery(query as esPersonalLicenceQuery);
		}
		#endregion

		virtual public PersonalLicence DetachEntity(PersonalLicence entity)
		{
			return base.DetachEntity(entity) as PersonalLicence;
		}

		virtual public PersonalLicence AttachEntity(PersonalLicence entity)
		{
			return base.AttachEntity(entity) as PersonalLicence;
		}

		virtual public void Combine(PersonalLicenceCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalLicence this[int index]
		{
			get
			{
				return base[index] as PersonalLicence;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalLicence);
		}
	}

	[Serializable]
	abstract public class esPersonalLicence : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalLicenceQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalLicence()
		{
		}

		public esPersonalLicence(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalLicenceID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalLicenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalLicenceID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalLicenceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalLicenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalLicenceID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalLicenceID)
		{
			esPersonalLicenceQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalLicenceID == personalLicenceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalLicenceID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalLicenceID", personalLicenceID);
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
						case "PersonalLicenceID": this.str.PersonalLicenceID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRLicenceType": this.str.SRLicenceType = (string)value; break;
						case "ValidFrom": this.str.ValidFrom = (string)value; break;
						case "ValidTo": this.str.ValidTo = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "VerificationDate": this.str.VerificationDate = (string)value; break;
						case "VerificationLetterNo": this.str.VerificationLetterNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalLicenceID":

							if (value == null || value is System.Int32)
								this.PersonalLicenceID = (System.Int32?)value;
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
						case "VerificationDate":

							if (value == null || value is System.DateTime)
								this.VerificationDate = (System.DateTime?)value;
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
		/// Maps to PersonalLicence.PersonalLicenceID
		/// </summary>
		virtual public System.Int32? PersonalLicenceID
		{
			get
			{
				return base.GetSystemInt32(PersonalLicenceMetadata.ColumnNames.PersonalLicenceID);
			}

			set
			{
				base.SetSystemInt32(PersonalLicenceMetadata.ColumnNames.PersonalLicenceID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalLicenceMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalLicenceMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.SRLicenceType
		/// </summary>
		virtual public System.String SRLicenceType
		{
			get
			{
				return base.GetSystemString(PersonalLicenceMetadata.ColumnNames.SRLicenceType);
			}

			set
			{
				base.SetSystemString(PersonalLicenceMetadata.ColumnNames.SRLicenceType, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(PersonalLicenceMetadata.ColumnNames.ValidFrom);
			}

			set
			{
				base.SetSystemDateTime(PersonalLicenceMetadata.ColumnNames.ValidFrom, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(PersonalLicenceMetadata.ColumnNames.ValidTo);
			}

			set
			{
				base.SetSystemDateTime(PersonalLicenceMetadata.ColumnNames.ValidTo, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(PersonalLicenceMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(PersonalLicenceMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalLicenceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalLicenceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalLicenceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalLicenceMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.VerificationDate
		/// </summary>
		virtual public System.DateTime? VerificationDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalLicenceMetadata.ColumnNames.VerificationDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalLicenceMetadata.ColumnNames.VerificationDate, value);
			}
		}
		/// <summary>
		/// Maps to PersonalLicence.VerificationLetterNo
		/// </summary>
		virtual public System.String VerificationLetterNo
		{
			get
			{
				return base.GetSystemString(PersonalLicenceMetadata.ColumnNames.VerificationLetterNo);
			}

			set
			{
				base.SetSystemString(PersonalLicenceMetadata.ColumnNames.VerificationLetterNo, value);
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
			public esStrings(esPersonalLicence entity)
			{
				this.entity = entity;
			}
			public System.String PersonalLicenceID
			{
				get
				{
					System.Int32? data = entity.PersonalLicenceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalLicenceID = null;
					else entity.PersonalLicenceID = Convert.ToInt32(value);
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
			public System.String SRLicenceType
			{
				get
				{
					System.String data = entity.SRLicenceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLicenceType = null;
					else entity.SRLicenceType = Convert.ToString(value);
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
			public System.String VerificationDate
			{
				get
				{
					System.DateTime? data = entity.VerificationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationDate = null;
					else entity.VerificationDate = Convert.ToDateTime(value);
				}
			}
			public System.String VerificationLetterNo
			{
				get
				{
					System.String data = entity.VerificationLetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationLetterNo = null;
					else entity.VerificationLetterNo = Convert.ToString(value);
				}
			}
			private esPersonalLicence entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalLicenceQuery query)
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
				throw new Exception("esPersonalLicence can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalLicence : esPersonalLicence
	{
	}

	[Serializable]
	abstract public class esPersonalLicenceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalLicenceMetadata.Meta();
			}
		}

		public esQueryItem PersonalLicenceID
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.PersonalLicenceID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRLicenceType
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.SRLicenceType, esSystemType.String);
			}
		}

		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem VerificationDate
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.VerificationDate, esSystemType.DateTime);
			}
		}

		public esQueryItem VerificationLetterNo
		{
			get
			{
				return new esQueryItem(this, PersonalLicenceMetadata.ColumnNames.VerificationLetterNo, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalLicenceCollection")]
	public partial class PersonalLicenceCollection : esPersonalLicenceCollection, IEnumerable<PersonalLicence>
	{
		public PersonalLicenceCollection()
		{

		}

		public static implicit operator List<PersonalLicence>(PersonalLicenceCollection coll)
		{
			List<PersonalLicence> list = new List<PersonalLicence>();

			foreach (PersonalLicence emp in coll)
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
				return PersonalLicenceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalLicenceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalLicence(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalLicence();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalLicenceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalLicenceQuery();
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
		public bool Load(PersonalLicenceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalLicence AddNew()
		{
			PersonalLicence entity = base.AddNewEntity() as PersonalLicence;

			return entity;
		}
		public PersonalLicence FindByPrimaryKey(Int32 personalLicenceID)
		{
			return base.FindByPrimaryKey(personalLicenceID) as PersonalLicence;
		}

		#region IEnumerable< PersonalLicence> Members

		IEnumerator<PersonalLicence> IEnumerable<PersonalLicence>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalLicence;
			}
		}

		#endregion

		private PersonalLicenceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalLicence' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalLicence ({PersonalLicenceID})")]
	[Serializable]
	public partial class PersonalLicence : esPersonalLicence
	{
		public PersonalLicence()
		{
		}

		public PersonalLicence(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalLicenceMetadata.Meta();
			}
		}

		override protected esPersonalLicenceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalLicenceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalLicenceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalLicenceQuery();
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
		public bool Load(PersonalLicenceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalLicenceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalLicenceQuery : esPersonalLicenceQuery
	{
		public PersonalLicenceQuery()
		{

		}

		public PersonalLicenceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalLicenceQuery";
		}
	}

	[Serializable]
	public partial class PersonalLicenceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalLicenceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.PersonalLicenceID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.PersonalLicenceID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.SRLicenceType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.SRLicenceType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.ValidFrom, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.ValidTo, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.ValidTo;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.Note, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.VerificationDate, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.VerificationDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalLicenceMetadata.ColumnNames.VerificationLetterNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalLicenceMetadata.PropertyNames.VerificationLetterNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalLicenceMetadata Meta()
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
			public const string PersonalLicenceID = "PersonalLicenceID";
			public const string PersonID = "PersonID";
			public const string SRLicenceType = "SRLicenceType";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VerificationDate = "VerificationDate";
			public const string VerificationLetterNo = "VerificationLetterNo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalLicenceID = "PersonalLicenceID";
			public const string PersonID = "PersonID";
			public const string SRLicenceType = "SRLicenceType";
			public const string ValidFrom = "ValidFrom";
			public const string ValidTo = "ValidTo";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string VerificationDate = "VerificationDate";
			public const string VerificationLetterNo = "VerificationLetterNo";
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
			lock (typeof(PersonalLicenceMetadata))
			{
				if (PersonalLicenceMetadata.mapDelegates == null)
				{
					PersonalLicenceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalLicenceMetadata.meta == null)
				{
					PersonalLicenceMetadata.meta = new PersonalLicenceMetadata();
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

				meta.AddTypeMap("PersonalLicenceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRLicenceType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VerificationLetterNo", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalLicence";
				meta.Destination = "PersonalLicence";
				meta.spInsert = "proc_PersonalLicenceInsert";
				meta.spUpdate = "proc_PersonalLicenceUpdate";
				meta.spDelete = "proc_PersonalLicenceDelete";
				meta.spLoadAll = "proc_PersonalLicenceLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalLicenceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalLicenceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
