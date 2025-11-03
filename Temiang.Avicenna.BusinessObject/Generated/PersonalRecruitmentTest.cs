/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/30/2022 7:53:02 PM
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
	abstract public class esPersonalRecruitmentTestCollection : esEntityCollectionWAuditLog
	{
		public esPersonalRecruitmentTestCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalRecruitmentTestCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalRecruitmentTestQuery query)
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
			this.InitQuery(query as esPersonalRecruitmentTestQuery);
		}
		#endregion

		virtual public PersonalRecruitmentTest DetachEntity(PersonalRecruitmentTest entity)
		{
			return base.DetachEntity(entity) as PersonalRecruitmentTest;
		}

		virtual public PersonalRecruitmentTest AttachEntity(PersonalRecruitmentTest entity)
		{
			return base.AttachEntity(entity) as PersonalRecruitmentTest;
		}

		virtual public void Combine(PersonalRecruitmentTestCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalRecruitmentTest this[int index]
		{
			get
			{
				return base[index] as PersonalRecruitmentTest;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalRecruitmentTest);
		}
	}

	[Serializable]
	abstract public class esPersonalRecruitmentTest : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalRecruitmentTestQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalRecruitmentTest()
		{
		}

		public esPersonalRecruitmentTest(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalRecruitmentTestID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalRecruitmentTestID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalRecruitmentTestID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalRecruitmentTestID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalRecruitmentTestID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalRecruitmentTestID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalRecruitmentTestID)
		{
			esPersonalRecruitmentTestQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalRecruitmentTestID == personalRecruitmentTestID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalRecruitmentTestID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalRecruitmentTestID", personalRecruitmentTestID);
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
						case "PersonalRecruitmentTestID": this.str.PersonalRecruitmentTestID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "TestDate": this.str.TestDate = (string)value; break;
						case "SRRecruitmentTest": this.str.SRRecruitmentTest = (string)value; break;
						case "TestResult": this.str.TestResult = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Advantages": this.str.Advantages = (string)value; break;
						case "Deficiency": this.str.Deficiency = (string)value; break;
						case "Suggestion": this.str.Suggestion = (string)value; break;
						case "SRRecruitmentTestConclusion": this.str.SRRecruitmentTestConclusion = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalRecruitmentTestID":

							if (value == null || value is System.Int32)
								this.PersonalRecruitmentTestID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "TestDate":

							if (value == null || value is System.DateTime)
								this.TestDate = (System.DateTime?)value;
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
		/// Maps to PersonalRecruitmentTest.PersonalRecruitmentTestID
		/// </summary>
		virtual public System.Int32? PersonalRecruitmentTestID
		{
			get
			{
				return base.GetSystemInt32(PersonalRecruitmentTestMetadata.ColumnNames.PersonalRecruitmentTestID);
			}

			set
			{
				base.SetSystemInt32(PersonalRecruitmentTestMetadata.ColumnNames.PersonalRecruitmentTestID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalRecruitmentTestMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalRecruitmentTestMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.TestDate
		/// </summary>
		virtual public System.DateTime? TestDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalRecruitmentTestMetadata.ColumnNames.TestDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalRecruitmentTestMetadata.ColumnNames.TestDate, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.SRRecruitmentTest
		/// </summary>
		virtual public System.String SRRecruitmentTest
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTest);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTest, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.TestResult
		/// </summary>
		virtual public System.String TestResult
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.TestResult);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.TestResult, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.Advantages
		/// </summary>
		virtual public System.String Advantages
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.Advantages);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.Advantages, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.Deficiency
		/// </summary>
		virtual public System.String Deficiency
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.Deficiency);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.Deficiency, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.Suggestion
		/// </summary>
		virtual public System.String Suggestion
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.Suggestion);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.Suggestion, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.SRRecruitmentTestConclusion
		/// </summary>
		virtual public System.String SRRecruitmentTestConclusion
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTestConclusion);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTestConclusion, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalRecruitmentTestMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalRecruitmentTestMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTest.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPersonalRecruitmentTest entity)
			{
				this.entity = entity;
			}
			public System.String PersonalRecruitmentTestID
			{
				get
				{
					System.Int32? data = entity.PersonalRecruitmentTestID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalRecruitmentTestID = null;
					else entity.PersonalRecruitmentTestID = Convert.ToInt32(value);
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
			public System.String TestDate
			{
				get
				{
					System.DateTime? data = entity.TestDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestDate = null;
					else entity.TestDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRRecruitmentTest
			{
				get
				{
					System.String data = entity.SRRecruitmentTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRecruitmentTest = null;
					else entity.SRRecruitmentTest = Convert.ToString(value);
				}
			}
			public System.String TestResult
			{
				get
				{
					System.String data = entity.TestResult;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TestResult = null;
					else entity.TestResult = Convert.ToString(value);
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
			public System.String Advantages
			{
				get
				{
					System.String data = entity.Advantages;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Advantages = null;
					else entity.Advantages = Convert.ToString(value);
				}
			}
			public System.String Deficiency
			{
				get
				{
					System.String data = entity.Deficiency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Deficiency = null;
					else entity.Deficiency = Convert.ToString(value);
				}
			}
			public System.String Suggestion
			{
				get
				{
					System.String data = entity.Suggestion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Suggestion = null;
					else entity.Suggestion = Convert.ToString(value);
				}
			}
			public System.String SRRecruitmentTestConclusion
			{
				get
				{
					System.String data = entity.SRRecruitmentTestConclusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRecruitmentTestConclusion = null;
					else entity.SRRecruitmentTestConclusion = Convert.ToString(value);
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
			private esPersonalRecruitmentTest entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalRecruitmentTestQuery query)
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
				throw new Exception("esPersonalRecruitmentTest can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalRecruitmentTest : esPersonalRecruitmentTest
	{
	}

	[Serializable]
	abstract public class esPersonalRecruitmentTestQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalRecruitmentTestMetadata.Meta();
			}
		}

		public esQueryItem PersonalRecruitmentTestID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.PersonalRecruitmentTestID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem TestDate
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.TestDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRRecruitmentTest
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTest, esSystemType.String);
			}
		}

		public esQueryItem TestResult
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.TestResult, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Advantages
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.Advantages, esSystemType.String);
			}
		}

		public esQueryItem Deficiency
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.Deficiency, esSystemType.String);
			}
		}

		public esQueryItem Suggestion
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.Suggestion, esSystemType.String);
			}
		}

		public esQueryItem SRRecruitmentTestConclusion
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTestConclusion, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalRecruitmentTestCollection")]
	public partial class PersonalRecruitmentTestCollection : esPersonalRecruitmentTestCollection, IEnumerable<PersonalRecruitmentTest>
	{
		public PersonalRecruitmentTestCollection()
		{

		}

		public static implicit operator List<PersonalRecruitmentTest>(PersonalRecruitmentTestCollection coll)
		{
			List<PersonalRecruitmentTest> list = new List<PersonalRecruitmentTest>();

			foreach (PersonalRecruitmentTest emp in coll)
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
				return PersonalRecruitmentTestMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalRecruitmentTestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalRecruitmentTest(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalRecruitmentTest();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalRecruitmentTestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalRecruitmentTestQuery();
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
		public bool Load(PersonalRecruitmentTestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalRecruitmentTest AddNew()
		{
			PersonalRecruitmentTest entity = base.AddNewEntity() as PersonalRecruitmentTest;

			return entity;
		}
		public PersonalRecruitmentTest FindByPrimaryKey(Int32 personalRecruitmentTestID)
		{
			return base.FindByPrimaryKey(personalRecruitmentTestID) as PersonalRecruitmentTest;
		}

		#region IEnumerable< PersonalRecruitmentTest> Members

		IEnumerator<PersonalRecruitmentTest> IEnumerable<PersonalRecruitmentTest>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalRecruitmentTest;
			}
		}

		#endregion

		private PersonalRecruitmentTestQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalRecruitmentTest' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalRecruitmentTest ({PersonalRecruitmentTestID})")]
	[Serializable]
	public partial class PersonalRecruitmentTest : esPersonalRecruitmentTest
	{
		public PersonalRecruitmentTest()
		{
		}

		public PersonalRecruitmentTest(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalRecruitmentTestMetadata.Meta();
			}
		}

		override protected esPersonalRecruitmentTestQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalRecruitmentTestQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalRecruitmentTestQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalRecruitmentTestQuery();
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
		public bool Load(PersonalRecruitmentTestQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalRecruitmentTestQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalRecruitmentTestQuery : esPersonalRecruitmentTestQuery
	{
		public PersonalRecruitmentTestQuery()
		{

		}

		public PersonalRecruitmentTestQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalRecruitmentTestQuery";
		}
	}

	[Serializable]
	public partial class PersonalRecruitmentTestMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalRecruitmentTestMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.PersonalRecruitmentTestID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.PersonalRecruitmentTestID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.TestDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.TestDate;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTest, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.SRRecruitmentTest;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.TestResult, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.TestResult;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 16;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.Advantages, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.Advantages;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.Deficiency, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.Deficiency;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.Suggestion, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.Suggestion;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.SRRecruitmentTestConclusion, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.SRRecruitmentTestConclusion;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalRecruitmentTestMetadata Meta()
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
			public const string PersonalRecruitmentTestID = "PersonalRecruitmentTestID";
			public const string PersonID = "PersonID";
			public const string TestDate = "TestDate";
			public const string SRRecruitmentTest = "SRRecruitmentTest";
			public const string TestResult = "TestResult";
			public const string Notes = "Notes";
			public const string Advantages = "Advantages";
			public const string Deficiency = "Deficiency";
			public const string Suggestion = "Suggestion";
			public const string SRRecruitmentTestConclusion = "SRRecruitmentTestConclusion";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalRecruitmentTestID = "PersonalRecruitmentTestID";
			public const string PersonID = "PersonID";
			public const string TestDate = "TestDate";
			public const string SRRecruitmentTest = "SRRecruitmentTest";
			public const string TestResult = "TestResult";
			public const string Notes = "Notes";
			public const string Advantages = "Advantages";
			public const string Deficiency = "Deficiency";
			public const string Suggestion = "Suggestion";
			public const string SRRecruitmentTestConclusion = "SRRecruitmentTestConclusion";
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
			lock (typeof(PersonalRecruitmentTestMetadata))
			{
				if (PersonalRecruitmentTestMetadata.mapDelegates == null)
				{
					PersonalRecruitmentTestMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalRecruitmentTestMetadata.meta == null)
				{
					PersonalRecruitmentTestMetadata.meta = new PersonalRecruitmentTestMetadata();
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

				meta.AddTypeMap("PersonalRecruitmentTestID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TestDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("SRRecruitmentTest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TestResult", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("Advantages", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Deficiency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Suggestion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRecruitmentTestConclusion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalRecruitmentTest";
				meta.Destination = "PersonalRecruitmentTest";
				meta.spInsert = "proc_PersonalRecruitmentTestInsert";
				meta.spUpdate = "proc_PersonalRecruitmentTestUpdate";
				meta.spDelete = "proc_PersonalRecruitmentTestDelete";
				meta.spLoadAll = "proc_PersonalRecruitmentTestLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalRecruitmentTestLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalRecruitmentTestMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
