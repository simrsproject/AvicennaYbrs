/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/1/2021 2:12:23 PM
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
	abstract public class esParamedicScheduleCollection : esEntityCollectionWAuditLog
	{
		public esParamedicScheduleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ParamedicScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicScheduleQuery query)
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
			this.InitQuery(query as esParamedicScheduleQuery);
		}
		#endregion

		virtual public ParamedicSchedule DetachEntity(ParamedicSchedule entity)
		{
			return base.DetachEntity(entity) as ParamedicSchedule;
		}

		virtual public ParamedicSchedule AttachEntity(ParamedicSchedule entity)
		{
			return base.AttachEntity(entity) as ParamedicSchedule;
		}

		virtual public void Combine(ParamedicScheduleCollection collection)
		{
			base.Combine(collection);
		}

		new public ParamedicSchedule this[int index]
		{
			get
			{
				return base[index] as ParamedicSchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicSchedule);
		}
	}

	[Serializable]
	abstract public class esParamedicSchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicSchedule()
		{
		}

		public esParamedicSchedule(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitID, String paramedicID, String periodYear)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID, periodYear);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID, periodYear);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String paramedicID, String periodYear)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID, periodYear);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID, periodYear);
		}

		private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String paramedicID, String periodYear)
		{
			esParamedicScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ParamedicID == paramedicID, query.PeriodYear == periodYear);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String paramedicID, String periodYear)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("ParamedicID", paramedicID);
			parms.Add("PeriodYear", periodYear);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "PeriodYear": this.str.PeriodYear = (string)value; break;
						case "ExamDuration": this.str.ExamDuration = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "PeriodMonth": this.str.PeriodMonth = (string)value; break;
						case "Quota": this.str.Quota = (string)value; break;
						case "QuotaOnline": this.str.QuotaOnline = (string)value; break;
						case "QuotaBpjs": this.str.QuotaBpjs = (string)value; break;
						case "QuotaBpjsOnline": this.str.QuotaBpjsOnline = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ExamDuration":

							if (value == null || value is System.Int32)
								this.ExamDuration = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "Quota":

							if (value == null || value is System.Int16)
								this.Quota = (System.Int16?)value;
							break;
						case "QuotaOnline":

							if (value == null || value is System.Int16)
								this.QuotaOnline = (System.Int16?)value;
							break;
						case "QuotaBpjs":

							if (value == null || value is System.Int16)
								this.QuotaBpjs = (System.Int16?)value;
							break;
						case "QuotaBpjsOnline":

							if (value == null || value is System.Int16)
								this.QuotaBpjsOnline = (System.Int16?)value;
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
		/// Maps to ParamedicSchedule.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleMetadata.ColumnNames.PeriodYear);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.ExamDuration
		/// </summary>
		virtual public System.Int32? ExamDuration
		{
			get
			{
				return base.GetSystemInt32(ParamedicScheduleMetadata.ColumnNames.ExamDuration);
			}

			set
			{
				base.SetSystemInt32(ParamedicScheduleMetadata.ColumnNames.ExamDuration, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.PeriodMonth
		/// </summary>
		virtual public System.String PeriodMonth
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleMetadata.ColumnNames.PeriodMonth);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleMetadata.ColumnNames.PeriodMonth, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.Quota
		/// </summary>
		virtual public System.Int16? Quota
		{
			get
			{
				return base.GetSystemInt16(ParamedicScheduleMetadata.ColumnNames.Quota);
			}

			set
			{
				base.SetSystemInt16(ParamedicScheduleMetadata.ColumnNames.Quota, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.QuotaOnline
		/// </summary>
		virtual public System.Int16? QuotaOnline
		{
			get
			{
				return base.GetSystemInt16(ParamedicScheduleMetadata.ColumnNames.QuotaOnline);
			}

			set
			{
				base.SetSystemInt16(ParamedicScheduleMetadata.ColumnNames.QuotaOnline, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.QuotaBpjs
		/// </summary>
		virtual public System.Int16? QuotaBpjs
		{
			get
			{
				return base.GetSystemInt16(ParamedicScheduleMetadata.ColumnNames.QuotaBpjs);
			}

			set
			{
				base.SetSystemInt16(ParamedicScheduleMetadata.ColumnNames.QuotaBpjs, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicSchedule.QuotaBpjsOnline
		/// </summary>
		virtual public System.Int16? QuotaBpjsOnline
		{
			get
			{
				return base.GetSystemInt16(ParamedicScheduleMetadata.ColumnNames.QuotaBpjsOnline);
			}

			set
			{
				base.SetSystemInt16(ParamedicScheduleMetadata.ColumnNames.QuotaBpjsOnline, value);
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
			public esStrings(esParamedicSchedule entity)
			{
				this.entity = entity;
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
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
			public System.String PeriodYear
			{
				get
				{
					System.String data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToString(value);
				}
			}
			public System.String ExamDuration
			{
				get
				{
					System.Int32? data = entity.ExamDuration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExamDuration = null;
					else entity.ExamDuration = Convert.ToInt32(value);
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
			public System.String PeriodMonth
			{
				get
				{
					System.String data = entity.PeriodMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodMonth = null;
					else entity.PeriodMonth = Convert.ToString(value);
				}
			}
			public System.String Quota
			{
				get
				{
					System.Int16? data = entity.Quota;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quota = null;
					else entity.Quota = Convert.ToInt16(value);
				}
			}
			public System.String QuotaOnline
			{
				get
				{
					System.Int16? data = entity.QuotaOnline;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuotaOnline = null;
					else entity.QuotaOnline = Convert.ToInt16(value);
				}
			}
			public System.String QuotaBpjs
			{
				get
				{
					System.Int16? data = entity.QuotaBpjs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuotaBpjs = null;
					else entity.QuotaBpjs = Convert.ToInt16(value);
				}
			}
			public System.String QuotaBpjsOnline
			{
				get
				{
					System.Int16? data = entity.QuotaBpjsOnline;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuotaBpjsOnline = null;
					else entity.QuotaBpjsOnline = Convert.ToInt16(value);
				}
			}
			private esParamedicSchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicScheduleQuery query)
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
				throw new Exception("esParamedicSchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicSchedule : esParamedicSchedule
	{
	}

	[Serializable]
	abstract public class esParamedicScheduleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ParamedicScheduleMetadata.Meta();
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		}

		public esQueryItem ExamDuration
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.ExamDuration, esSystemType.Int32);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem PeriodMonth
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.PeriodMonth, esSystemType.String);
			}
		}

		public esQueryItem Quota
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.Quota, esSystemType.Int16);
			}
		}

		public esQueryItem QuotaOnline
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.QuotaOnline, esSystemType.Int16);
			}
		}

		public esQueryItem QuotaBpjs
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.QuotaBpjs, esSystemType.Int16);
			}
		}

		public esQueryItem QuotaBpjsOnline
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleMetadata.ColumnNames.QuotaBpjsOnline, esSystemType.Int16);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicScheduleCollection")]
	public partial class ParamedicScheduleCollection : esParamedicScheduleCollection, IEnumerable<ParamedicSchedule>
	{
		public ParamedicScheduleCollection()
		{

		}

		public static implicit operator List<ParamedicSchedule>(ParamedicScheduleCollection coll)
		{
			List<ParamedicSchedule> list = new List<ParamedicSchedule>();

			foreach (ParamedicSchedule emp in coll)
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
				return ParamedicScheduleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicSchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicSchedule();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ParamedicScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicScheduleQuery();
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
		public bool Load(ParamedicScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicSchedule AddNew()
		{
			ParamedicSchedule entity = base.AddNewEntity() as ParamedicSchedule;

			return entity;
		}
		public ParamedicSchedule FindByPrimaryKey(String serviceUnitID, String paramedicID, String periodYear)
		{
			return base.FindByPrimaryKey(serviceUnitID, paramedicID, periodYear) as ParamedicSchedule;
		}

		#region IEnumerable< ParamedicSchedule> Members

		IEnumerator<ParamedicSchedule> IEnumerable<ParamedicSchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicSchedule;
			}
		}

		#endregion

		private ParamedicScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicSchedule' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicSchedule ({ServiceUnitID, ParamedicID, PeriodYear})")]
	[Serializable]
	public partial class ParamedicSchedule : esParamedicSchedule
	{
		public ParamedicSchedule()
		{
		}

		public ParamedicSchedule(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicScheduleMetadata.Meta();
			}
		}

		override protected esParamedicScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ParamedicScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicScheduleQuery();
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
		public bool Load(ParamedicScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ParamedicScheduleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicScheduleQuery : esParamedicScheduleQuery
	{
		public ParamedicScheduleQuery()
		{

		}

		public ParamedicScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ParamedicScheduleQuery";
		}
	}

	[Serializable]
	public partial class ParamedicScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.PeriodYear, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.PeriodYear;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.ExamDuration, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.ExamDuration;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.PeriodMonth, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.PeriodMonth;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.Quota, 8, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.Quota;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.QuotaOnline, 9, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.QuotaOnline;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.QuotaBpjs, 10, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.QuotaBpjs;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleMetadata.ColumnNames.QuotaBpjsOnline, 11, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicScheduleMetadata.PropertyNames.QuotaBpjsOnline;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ParamedicScheduleMetadata Meta()
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
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string PeriodYear = "PeriodYear";
			public const string ExamDuration = "ExamDuration";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PeriodMonth = "PeriodMonth";
			public const string Quota = "Quota";
			public const string QuotaOnline = "QuotaOnline";
			public const string QuotaBpjs = "QuotaBpjs";
			public const string QuotaBpjsOnline = "QuotaBpjsOnline";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string PeriodYear = "PeriodYear";
			public const string ExamDuration = "ExamDuration";
			public const string Notes = "Notes";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string PeriodMonth = "PeriodMonth";
			public const string Quota = "Quota";
			public const string QuotaOnline = "QuotaOnline";
			public const string QuotaBpjs = "QuotaBpjs";
			public const string QuotaBpjsOnline = "QuotaBpjsOnline";
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
			lock (typeof(ParamedicScheduleMetadata))
			{
				if (ParamedicScheduleMetadata.mapDelegates == null)
				{
					ParamedicScheduleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ParamedicScheduleMetadata.meta == null)
				{
					ParamedicScheduleMetadata.meta = new ParamedicScheduleMetadata();
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

				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExamDuration", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodMonth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quota", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("QuotaOnline", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("QuotaBpjs", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("QuotaBpjsOnline", new esTypeMap("smallint", "System.Int16"));


				meta.Source = "ParamedicSchedule";
				meta.Destination = "ParamedicSchedule";
				meta.spInsert = "proc_ParamedicScheduleInsert";
				meta.spUpdate = "proc_ParamedicScheduleUpdate";
				meta.spDelete = "proc_ParamedicScheduleDelete";
				meta.spLoadAll = "proc_ParamedicScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicScheduleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
