/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/30/2022 7:08:48 PM
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
	abstract public class esPersonalRecruitmentTestInterviewCollection : esEntityCollectionWAuditLog
	{
		public esPersonalRecruitmentTestInterviewCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalRecruitmentTestInterviewCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalRecruitmentTestInterviewQuery query)
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
			this.InitQuery(query as esPersonalRecruitmentTestInterviewQuery);
		}
		#endregion

		virtual public PersonalRecruitmentTestInterview DetachEntity(PersonalRecruitmentTestInterview entity)
		{
			return base.DetachEntity(entity) as PersonalRecruitmentTestInterview;
		}

		virtual public PersonalRecruitmentTestInterview AttachEntity(PersonalRecruitmentTestInterview entity)
		{
			return base.AttachEntity(entity) as PersonalRecruitmentTestInterview;
		}

		virtual public void Combine(PersonalRecruitmentTestInterviewCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalRecruitmentTestInterview this[int index]
		{
			get
			{
				return base[index] as PersonalRecruitmentTestInterview;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalRecruitmentTestInterview);
		}
	}

	[Serializable]
	abstract public class esPersonalRecruitmentTestInterview : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalRecruitmentTestInterviewQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalRecruitmentTestInterview()
		{
		}

		public esPersonalRecruitmentTestInterview(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalRecruitmentTestID, String sRRecruitmentTestQuestion)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalRecruitmentTestID, sRRecruitmentTestQuestion);
			else
				return LoadByPrimaryKeyStoredProcedure(personalRecruitmentTestID, sRRecruitmentTestQuestion);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalRecruitmentTestID, String sRRecruitmentTestQuestion)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalRecruitmentTestID, sRRecruitmentTestQuestion);
			else
				return LoadByPrimaryKeyStoredProcedure(personalRecruitmentTestID, sRRecruitmentTestQuestion);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalRecruitmentTestID, String sRRecruitmentTestQuestion)
		{
			esPersonalRecruitmentTestInterviewQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalRecruitmentTestID == personalRecruitmentTestID, query.SRRecruitmentTestQuestion == sRRecruitmentTestQuestion);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalRecruitmentTestID, String sRRecruitmentTestQuestion)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalRecruitmentTestID", personalRecruitmentTestID);
			parms.Add("SRRecruitmentTestQuestion", sRRecruitmentTestQuestion);
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
						case "SRRecruitmentTestQuestion": this.str.SRRecruitmentTestQuestion = (string)value; break;
						case "Score1": this.str.Score1 = (string)value; break;
						case "Score2": this.str.Score2 = (string)value; break;
						case "Score3": this.str.Score3 = (string)value; break;
						case "Score4": this.str.Score4 = (string)value; break;
						case "Score5": this.str.Score5 = (string)value; break;
						case "Score6": this.str.Score6 = (string)value; break;
						case "AverageScore": this.str.AverageScore = (string)value; break;
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
						case "Score1":

							if (value == null || value is System.Decimal)
								this.Score1 = (System.Decimal?)value;
							break;
						case "Score2":

							if (value == null || value is System.Decimal)
								this.Score2 = (System.Decimal?)value;
							break;
						case "Score3":

							if (value == null || value is System.Decimal)
								this.Score3 = (System.Decimal?)value;
							break;
						case "Score4":

							if (value == null || value is System.Decimal)
								this.Score4 = (System.Decimal?)value;
							break;
						case "Score5":

							if (value == null || value is System.Decimal)
								this.Score5 = (System.Decimal?)value;
							break;
						case "Score6":

							if (value == null || value is System.Decimal)
								this.Score6 = (System.Decimal?)value;
							break;
						case "AverageScore":

							if (value == null || value is System.Decimal)
								this.AverageScore = (System.Decimal?)value;
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
		/// Maps to PersonalRecruitmentTestInterview.PersonalRecruitmentTestID
		/// </summary>
		virtual public System.Int32? PersonalRecruitmentTestID
		{
			get
			{
				return base.GetSystemInt32(PersonalRecruitmentTestInterviewMetadata.ColumnNames.PersonalRecruitmentTestID);
			}

			set
			{
				base.SetSystemInt32(PersonalRecruitmentTestInterviewMetadata.ColumnNames.PersonalRecruitmentTestID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.SRRecruitmentTestQuestion
		/// </summary>
		virtual public System.String SRRecruitmentTestQuestion
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestInterviewMetadata.ColumnNames.SRRecruitmentTestQuestion);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestInterviewMetadata.ColumnNames.SRRecruitmentTestQuestion, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.Score1
		/// </summary>
		virtual public System.Decimal? Score1
		{
			get
			{
				return base.GetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score1);
			}

			set
			{
				base.SetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score1, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.Score2
		/// </summary>
		virtual public System.Decimal? Score2
		{
			get
			{
				return base.GetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score2);
			}

			set
			{
				base.SetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score2, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.Score3
		/// </summary>
		virtual public System.Decimal? Score3
		{
			get
			{
				return base.GetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score3);
			}

			set
			{
				base.SetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score3, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.Score4
		/// </summary>
		virtual public System.Decimal? Score4
		{
			get
			{
				return base.GetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score4);
			}

			set
			{
				base.SetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score4, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.Score5
		/// </summary>
		virtual public System.Decimal? Score5
		{
			get
			{
				return base.GetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score5);
			}

			set
			{
				base.SetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score5, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.Score6
		/// </summary>
		virtual public System.Decimal? Score6
		{
			get
			{
				return base.GetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score6);
			}

			set
			{
				base.SetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score6, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.AverageScore
		/// </summary>
		virtual public System.Decimal? AverageScore
		{
			get
			{
				return base.GetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.AverageScore);
			}

			set
			{
				base.SetSystemDecimal(PersonalRecruitmentTestInterviewMetadata.ColumnNames.AverageScore, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalRecruitmentTestInterviewMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalRecruitmentTestInterviewMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalRecruitmentTestInterview.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalRecruitmentTestInterviewMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalRecruitmentTestInterviewMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPersonalRecruitmentTestInterview entity)
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
			public System.String SRRecruitmentTestQuestion
			{
				get
				{
					System.String data = entity.SRRecruitmentTestQuestion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRecruitmentTestQuestion = null;
					else entity.SRRecruitmentTestQuestion = Convert.ToString(value);
				}
			}
			public System.String Score1
			{
				get
				{
					System.Decimal? data = entity.Score1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score1 = null;
					else entity.Score1 = Convert.ToDecimal(value);
				}
			}
			public System.String Score2
			{
				get
				{
					System.Decimal? data = entity.Score2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score2 = null;
					else entity.Score2 = Convert.ToDecimal(value);
				}
			}
			public System.String Score3
			{
				get
				{
					System.Decimal? data = entity.Score3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score3 = null;
					else entity.Score3 = Convert.ToDecimal(value);
				}
			}
			public System.String Score4
			{
				get
				{
					System.Decimal? data = entity.Score4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score4 = null;
					else entity.Score4 = Convert.ToDecimal(value);
				}
			}
			public System.String Score5
			{
				get
				{
					System.Decimal? data = entity.Score5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score5 = null;
					else entity.Score5 = Convert.ToDecimal(value);
				}
			}
			public System.String Score6
			{
				get
				{
					System.Decimal? data = entity.Score6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score6 = null;
					else entity.Score6 = Convert.ToDecimal(value);
				}
			}
			public System.String AverageScore
			{
				get
				{
					System.Decimal? data = entity.AverageScore;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AverageScore = null;
					else entity.AverageScore = Convert.ToDecimal(value);
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
			private esPersonalRecruitmentTestInterview entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalRecruitmentTestInterviewQuery query)
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
				throw new Exception("esPersonalRecruitmentTestInterview can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalRecruitmentTestInterview : esPersonalRecruitmentTestInterview
	{
	}

	[Serializable]
	abstract public class esPersonalRecruitmentTestInterviewQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalRecruitmentTestInterviewMetadata.Meta();
			}
		}

		public esQueryItem PersonalRecruitmentTestID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.PersonalRecruitmentTestID, esSystemType.Int32);
			}
		}

		public esQueryItem SRRecruitmentTestQuestion
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.SRRecruitmentTestQuestion, esSystemType.String);
			}
		}

		public esQueryItem Score1
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score1, esSystemType.Decimal);
			}
		}

		public esQueryItem Score2
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score2, esSystemType.Decimal);
			}
		}

		public esQueryItem Score3
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score3, esSystemType.Decimal);
			}
		}

		public esQueryItem Score4
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score4, esSystemType.Decimal);
			}
		}

		public esQueryItem Score5
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score5, esSystemType.Decimal);
			}
		}

		public esQueryItem Score6
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score6, esSystemType.Decimal);
			}
		}

		public esQueryItem AverageScore
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.AverageScore, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalRecruitmentTestInterviewMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalRecruitmentTestInterviewCollection")]
	public partial class PersonalRecruitmentTestInterviewCollection : esPersonalRecruitmentTestInterviewCollection, IEnumerable<PersonalRecruitmentTestInterview>
	{
		public PersonalRecruitmentTestInterviewCollection()
		{

		}

		public static implicit operator List<PersonalRecruitmentTestInterview>(PersonalRecruitmentTestInterviewCollection coll)
		{
			List<PersonalRecruitmentTestInterview> list = new List<PersonalRecruitmentTestInterview>();

			foreach (PersonalRecruitmentTestInterview emp in coll)
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
				return PersonalRecruitmentTestInterviewMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalRecruitmentTestInterviewQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalRecruitmentTestInterview(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalRecruitmentTestInterview();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalRecruitmentTestInterviewQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalRecruitmentTestInterviewQuery();
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
		public bool Load(PersonalRecruitmentTestInterviewQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalRecruitmentTestInterview AddNew()
		{
			PersonalRecruitmentTestInterview entity = base.AddNewEntity() as PersonalRecruitmentTestInterview;

			return entity;
		}
		public PersonalRecruitmentTestInterview FindByPrimaryKey(Int32 personalRecruitmentTestID, String sRRecruitmentTestQuestion)
		{
			return base.FindByPrimaryKey(personalRecruitmentTestID, sRRecruitmentTestQuestion) as PersonalRecruitmentTestInterview;
		}

		#region IEnumerable< PersonalRecruitmentTestInterview> Members

		IEnumerator<PersonalRecruitmentTestInterview> IEnumerable<PersonalRecruitmentTestInterview>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalRecruitmentTestInterview;
			}
		}

		#endregion

		private PersonalRecruitmentTestInterviewQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalRecruitmentTestInterview' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalRecruitmentTestInterview ({PersonalRecruitmentTestID, SRRecruitmentTestQuestion})")]
	[Serializable]
	public partial class PersonalRecruitmentTestInterview : esPersonalRecruitmentTestInterview
	{
		public PersonalRecruitmentTestInterview()
		{
		}

		public PersonalRecruitmentTestInterview(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalRecruitmentTestInterviewMetadata.Meta();
			}
		}

		override protected esPersonalRecruitmentTestInterviewQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalRecruitmentTestInterviewQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalRecruitmentTestInterviewQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalRecruitmentTestInterviewQuery();
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
		public bool Load(PersonalRecruitmentTestInterviewQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalRecruitmentTestInterviewQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalRecruitmentTestInterviewQuery : esPersonalRecruitmentTestInterviewQuery
	{
		public PersonalRecruitmentTestInterviewQuery()
		{

		}

		public PersonalRecruitmentTestInterviewQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalRecruitmentTestInterviewQuery";
		}
	}

	[Serializable]
	public partial class PersonalRecruitmentTestInterviewMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalRecruitmentTestInterviewMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.PersonalRecruitmentTestID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.PersonalRecruitmentTestID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.SRRecruitmentTestQuestion, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.SRRecruitmentTestQuestion;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score1, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.Score1;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score2, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.Score2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score3, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.Score3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score4, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.Score4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score5, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.Score5;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.Score6, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.Score6;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.AverageScore, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.AverageScore;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalRecruitmentTestInterviewMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalRecruitmentTestInterviewMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalRecruitmentTestInterviewMetadata Meta()
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
			public const string SRRecruitmentTestQuestion = "SRRecruitmentTestQuestion";
			public const string Score1 = "Score1";
			public const string Score2 = "Score2";
			public const string Score3 = "Score3";
			public const string Score4 = "Score4";
			public const string Score5 = "Score5";
			public const string Score6 = "Score6";
			public const string AverageScore = "AverageScore";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalRecruitmentTestID = "PersonalRecruitmentTestID";
			public const string SRRecruitmentTestQuestion = "SRRecruitmentTestQuestion";
			public const string Score1 = "Score1";
			public const string Score2 = "Score2";
			public const string Score3 = "Score3";
			public const string Score4 = "Score4";
			public const string Score5 = "Score5";
			public const string Score6 = "Score6";
			public const string AverageScore = "AverageScore";
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
			lock (typeof(PersonalRecruitmentTestInterviewMetadata))
			{
				if (PersonalRecruitmentTestInterviewMetadata.mapDelegates == null)
				{
					PersonalRecruitmentTestInterviewMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalRecruitmentTestInterviewMetadata.meta == null)
				{
					PersonalRecruitmentTestInterviewMetadata.meta = new PersonalRecruitmentTestInterviewMetadata();
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
				meta.AddTypeMap("SRRecruitmentTestQuestion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Score1", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Score2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Score3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Score4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Score5", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Score6", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AverageScore", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalRecruitmentTestInterview";
				meta.Destination = "PersonalRecruitmentTestInterview";
				meta.spInsert = "proc_PersonalRecruitmentTestInterviewInsert";
				meta.spUpdate = "proc_PersonalRecruitmentTestInterviewUpdate";
				meta.spDelete = "proc_PersonalRecruitmentTestInterviewDelete";
				meta.spLoadAll = "proc_PersonalRecruitmentTestInterviewLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalRecruitmentTestInterviewLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalRecruitmentTestInterviewMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
