/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/24/2021 11:21:54 AM
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
	abstract public class esEmployeePerformanceAppraisalCollection : esEntityCollectionWAuditLog
	{
		public esEmployeePerformanceAppraisalCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeePerformanceAppraisalCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeePerformanceAppraisalQuery query)
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
			this.InitQuery(query as esEmployeePerformanceAppraisalQuery);
		}
		#endregion

		virtual public EmployeePerformanceAppraisal DetachEntity(EmployeePerformanceAppraisal entity)
		{
			return base.DetachEntity(entity) as EmployeePerformanceAppraisal;
		}

		virtual public EmployeePerformanceAppraisal AttachEntity(EmployeePerformanceAppraisal entity)
		{
			return base.AttachEntity(entity) as EmployeePerformanceAppraisal;
		}

		virtual public void Combine(EmployeePerformanceAppraisalCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeePerformanceAppraisal this[int index]
		{
			get
			{
				return base[index] as EmployeePerformanceAppraisal;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeePerformanceAppraisal);
		}
	}

	[Serializable]
	abstract public class esEmployeePerformanceAppraisal : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeePerformanceAppraisalQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeePerformanceAppraisal()
		{
		}

		public esEmployeePerformanceAppraisal(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 performanceAppraisalID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performanceAppraisalID);
			else
				return LoadByPrimaryKeyStoredProcedure(performanceAppraisalID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 performanceAppraisalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(performanceAppraisalID);
			else
				return LoadByPrimaryKeyStoredProcedure(performanceAppraisalID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 performanceAppraisalID)
		{
			esEmployeePerformanceAppraisalQuery query = this.GetDynamicQuery();
			query.Where(query.PerformanceAppraisalID == performanceAppraisalID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 performanceAppraisalID)
		{
			esParameters parms = new esParameters();
			parms.Add("PerformanceAppraisalID", performanceAppraisalID);
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
						case "PerformanceAppraisalID": this.str.PerformanceAppraisalID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "ParticipantItemID": this.str.ParticipantItemID = (string)value; break;
						case "YearPeriod": this.str.YearPeriod = (string)value; break;
						case "SRQuarterPeriod": this.str.SRQuarterPeriod = (string)value; break;
						case "Score": this.str.Score = (string)value; break;
						case "ScoreText": this.str.ScoreText = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PerformanceAppraisalID":

							if (value == null || value is System.Int32)
								this.PerformanceAppraisalID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "ParticipantItemID":

							if (value == null || value is System.Int32)
								this.ParticipantItemID = (System.Int32?)value;
							break;
						case "Score":

							if (value == null || value is System.Decimal)
								this.Score = (System.Decimal?)value;
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
		/// Maps to EmployeePerformanceAppraisal.PerformanceAppraisalID
		/// </summary>
		virtual public System.Int32? PerformanceAppraisalID
		{
			get
			{
				return base.GetSystemInt32(EmployeePerformanceAppraisalMetadata.ColumnNames.PerformanceAppraisalID);
			}

			set
			{
				base.SetSystemInt32(EmployeePerformanceAppraisalMetadata.ColumnNames.PerformanceAppraisalID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeePerformanceAppraisalMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeePerformanceAppraisalMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.ParticipantItemID
		/// </summary>
		virtual public System.Int32? ParticipantItemID
		{
			get
			{
				return base.GetSystemInt32(EmployeePerformanceAppraisalMetadata.ColumnNames.ParticipantItemID);
			}

			set
			{
				base.SetSystemInt32(EmployeePerformanceAppraisalMetadata.ColumnNames.ParticipantItemID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.YearPeriod
		/// </summary>
		virtual public System.String YearPeriod
		{
			get
			{
				return base.GetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.YearPeriod);
			}

			set
			{
				base.SetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.YearPeriod, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.SRQuarterPeriod
		/// </summary>
		virtual public System.String SRQuarterPeriod
		{
			get
			{
				return base.GetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.SRQuarterPeriod);
			}

			set
			{
				base.SetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.SRQuarterPeriod, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.Score
		/// </summary>
		virtual public System.Decimal? Score
		{
			get
			{
				return base.GetSystemDecimal(EmployeePerformanceAppraisalMetadata.ColumnNames.Score);
			}

			set
			{
				base.SetSystemDecimal(EmployeePerformanceAppraisalMetadata.ColumnNames.Score, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.ScoreText
		/// </summary>
		virtual public System.String ScoreText
		{
			get
			{
				return base.GetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.ScoreText);
			}

			set
			{
				base.SetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.ScoreText, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeePerformanceAppraisalMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeePerformanceAppraisalMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeePerformanceAppraisal.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeePerformanceAppraisalMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeePerformanceAppraisal entity)
			{
				this.entity = entity;
			}
			public System.String PerformanceAppraisalID
			{
				get
				{
					System.Int32? data = entity.PerformanceAppraisalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerformanceAppraisalID = null;
					else entity.PerformanceAppraisalID = Convert.ToInt32(value);
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
			public System.String ParticipantItemID
			{
				get
				{
					System.Int32? data = entity.ParticipantItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParticipantItemID = null;
					else entity.ParticipantItemID = Convert.ToInt32(value);
				}
			}
			public System.String YearPeriod
			{
				get
				{
					System.String data = entity.YearPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearPeriod = null;
					else entity.YearPeriod = Convert.ToString(value);
				}
			}
			public System.String SRQuarterPeriod
			{
				get
				{
					System.String data = entity.SRQuarterPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRQuarterPeriod = null;
					else entity.SRQuarterPeriod = Convert.ToString(value);
				}
			}
			public System.String Score
			{
				get
				{
					System.Decimal? data = entity.Score;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Score = null;
					else entity.Score = Convert.ToDecimal(value);
				}
			}
			public System.String ScoreText
			{
				get
				{
					System.String data = entity.ScoreText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScoreText = null;
					else entity.ScoreText = Convert.ToString(value);
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
			private esEmployeePerformanceAppraisal entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeePerformanceAppraisalQuery query)
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
				throw new Exception("esEmployeePerformanceAppraisal can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeePerformanceAppraisal : esEmployeePerformanceAppraisal
	{
	}

	[Serializable]
	abstract public class esEmployeePerformanceAppraisalQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeePerformanceAppraisalMetadata.Meta();
			}
		}

		public esQueryItem PerformanceAppraisalID
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.PerformanceAppraisalID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem ParticipantItemID
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.ParticipantItemID, esSystemType.Int32);
			}
		}

		public esQueryItem YearPeriod
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.YearPeriod, esSystemType.String);
			}
		}

		public esQueryItem SRQuarterPeriod
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.SRQuarterPeriod, esSystemType.String);
			}
		}

		public esQueryItem Score
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.Score, esSystemType.Decimal);
			}
		}

		public esQueryItem ScoreText
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.ScoreText, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeePerformanceAppraisalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeePerformanceAppraisalCollection")]
	public partial class EmployeePerformanceAppraisalCollection : esEmployeePerformanceAppraisalCollection, IEnumerable<EmployeePerformanceAppraisal>
	{
		public EmployeePerformanceAppraisalCollection()
		{

		}

		public static implicit operator List<EmployeePerformanceAppraisal>(EmployeePerformanceAppraisalCollection coll)
		{
			List<EmployeePerformanceAppraisal> list = new List<EmployeePerformanceAppraisal>();

			foreach (EmployeePerformanceAppraisal emp in coll)
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
				return EmployeePerformanceAppraisalMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePerformanceAppraisalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeePerformanceAppraisal(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeePerformanceAppraisal();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeePerformanceAppraisalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePerformanceAppraisalQuery();
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
		public bool Load(EmployeePerformanceAppraisalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeePerformanceAppraisal AddNew()
		{
			EmployeePerformanceAppraisal entity = base.AddNewEntity() as EmployeePerformanceAppraisal;

			return entity;
		}
		public EmployeePerformanceAppraisal FindByPrimaryKey(Int32 performanceAppraisalID)
		{
			return base.FindByPrimaryKey(performanceAppraisalID) as EmployeePerformanceAppraisal;
		}

		#region IEnumerable< EmployeePerformanceAppraisal> Members

		IEnumerator<EmployeePerformanceAppraisal> IEnumerable<EmployeePerformanceAppraisal>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeePerformanceAppraisal;
			}
		}

		#endregion

		private EmployeePerformanceAppraisalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeePerformanceAppraisal' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeePerformanceAppraisal ({PerformanceAppraisalID})")]
	[Serializable]
	public partial class EmployeePerformanceAppraisal : esEmployeePerformanceAppraisal
	{
		public EmployeePerformanceAppraisal()
		{
		}

		public EmployeePerformanceAppraisal(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeePerformanceAppraisalMetadata.Meta();
			}
		}

		override protected esEmployeePerformanceAppraisalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeePerformanceAppraisalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeePerformanceAppraisalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeePerformanceAppraisalQuery();
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
		public bool Load(EmployeePerformanceAppraisalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeePerformanceAppraisalQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeePerformanceAppraisalQuery : esEmployeePerformanceAppraisalQuery
	{
		public EmployeePerformanceAppraisalQuery()
		{

		}

		public EmployeePerformanceAppraisalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeePerformanceAppraisalQuery";
		}
	}

	[Serializable]
	public partial class EmployeePerformanceAppraisalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeePerformanceAppraisalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.PerformanceAppraisalID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.PerformanceAppraisalID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.ParticipantItemID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.ParticipantItemID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.YearPeriod, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.YearPeriod;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.SRQuarterPeriod, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.SRQuarterPeriod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.Score, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.Score;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.ScoreText, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.ScoreText;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeePerformanceAppraisalMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeePerformanceAppraisalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeePerformanceAppraisalMetadata Meta()
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
			public const string PerformanceAppraisalID = "PerformanceAppraisalID";
			public const string PersonID = "PersonID";
			public const string ParticipantItemID = "ParticipantItemID";
			public const string YearPeriod = "YearPeriod";
			public const string SRQuarterPeriod = "SRQuarterPeriod";
			public const string Score = "Score";
			public const string ScoreText = "ScoreText";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PerformanceAppraisalID = "PerformanceAppraisalID";
			public const string PersonID = "PersonID";
			public const string ParticipantItemID = "ParticipantItemID";
			public const string YearPeriod = "YearPeriod";
			public const string SRQuarterPeriod = "SRQuarterPeriod";
			public const string Score = "Score";
			public const string ScoreText = "ScoreText";
			public const string Notes = "Notes";
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
			lock (typeof(EmployeePerformanceAppraisalMetadata))
			{
				if (EmployeePerformanceAppraisalMetadata.mapDelegates == null)
				{
					EmployeePerformanceAppraisalMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeePerformanceAppraisalMetadata.meta == null)
				{
					EmployeePerformanceAppraisalMetadata.meta = new EmployeePerformanceAppraisalMetadata();
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

				meta.AddTypeMap("PerformanceAppraisalID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParticipantItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("YearPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRQuarterPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Score", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ScoreText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeePerformanceAppraisal";
				meta.Destination = "EmployeePerformanceAppraisal";
				meta.spInsert = "proc_EmployeePerformanceAppraisalInsert";
				meta.spUpdate = "proc_EmployeePerformanceAppraisalUpdate";
				meta.spDelete = "proc_EmployeePerformanceAppraisalDelete";
				meta.spLoadAll = "proc_EmployeePerformanceAppraisalLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeePerformanceAppraisalLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeePerformanceAppraisalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
