/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/25/2022 4:25:31 PM
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
	abstract public class esMonthlyAttendanceDetailSummaryCollection : esEntityCollectionWAuditLog
	{
		public esMonthlyAttendanceDetailSummaryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MonthlyAttendanceDetailSummaryCollection";
		}

		#region Query Logic
		protected void InitQuery(esMonthlyAttendanceDetailSummaryQuery query)
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
			this.InitQuery(query as esMonthlyAttendanceDetailSummaryQuery);
		}
		#endregion

		virtual public MonthlyAttendanceDetailSummary DetachEntity(MonthlyAttendanceDetailSummary entity)
		{
			return base.DetachEntity(entity) as MonthlyAttendanceDetailSummary;
		}

		virtual public MonthlyAttendanceDetailSummary AttachEntity(MonthlyAttendanceDetailSummary entity)
		{
			return base.AttachEntity(entity) as MonthlyAttendanceDetailSummary;
		}

		virtual public void Combine(MonthlyAttendanceDetailSummaryCollection collection)
		{
			base.Combine(collection);
		}

		new public MonthlyAttendanceDetailSummary this[int index]
		{
			get
			{
				return base[index] as MonthlyAttendanceDetailSummary;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MonthlyAttendanceDetailSummary);
		}
	}

	[Serializable]
	abstract public class esMonthlyAttendanceDetailSummary : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMonthlyAttendanceDetailSummaryQuery GetDynamicQuery()
		{
			return null;
		}

		public esMonthlyAttendanceDetailSummary()
		{
		}

		public esMonthlyAttendanceDetailSummary(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 monthlyAttendanceDetailID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monthlyAttendanceDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(monthlyAttendanceDetailID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 monthlyAttendanceDetailID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(monthlyAttendanceDetailID);
			else
				return LoadByPrimaryKeyStoredProcedure(monthlyAttendanceDetailID);
		}

		private bool LoadByPrimaryKeyDynamic(Int64 monthlyAttendanceDetailID)
		{
			esMonthlyAttendanceDetailSummaryQuery query = this.GetDynamicQuery();
			query.Where(query.MonthlyAttendanceDetailID == monthlyAttendanceDetailID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int64 monthlyAttendanceDetailID)
		{
			esParameters parms = new esParameters();
			parms.Add("MonthlyAttendanceDetailID", monthlyAttendanceDetailID);
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
						case "MonthlyAttendanceDetailID": this.str.MonthlyAttendanceDetailID = (string)value; break;
						case "WeekdayOvertime5WD": this.str.WeekdayOvertime5WD = (string)value; break;
						case "HolidayOvertime5WD": this.str.HolidayOvertime5WD = (string)value; break;
						case "WeekdayOvertime6WD": this.str.WeekdayOvertime6WD = (string)value; break;
						case "HolidayOvertime6WD": this.str.HolidayOvertime6WD = (string)value; break;
						case "LeaderShift": this.str.LeaderShift = (string)value; break;
						case "NightShift": this.str.NightShift = (string)value; break;
						case "MealAllowance": this.str.MealAllowance = (string)value; break;
						case "PayCut5WD": this.str.PayCut5WD = (string)value; break;
						case "PayCut6WD": this.str.PayCut6WD = (string)value; break;
						case "WeekdayNightShift": this.str.WeekdayNightShift = (string)value; break;
						case "HolidayNightShift": this.str.HolidayNightShift = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "MonthlyAttendanceDetailID":

							if (value == null || value is System.Int64)
								this.MonthlyAttendanceDetailID = (System.Int64?)value;
							break;
						case "WeekdayOvertime5WD":

							if (value == null || value is System.Decimal)
								this.WeekdayOvertime5WD = (System.Decimal?)value;
							break;
						case "HolidayOvertime5WD":

							if (value == null || value is System.Decimal)
								this.HolidayOvertime5WD = (System.Decimal?)value;
							break;
						case "WeekdayOvertime6WD":

							if (value == null || value is System.Decimal)
								this.WeekdayOvertime6WD = (System.Decimal?)value;
							break;
						case "HolidayOvertime6WD":

							if (value == null || value is System.Decimal)
								this.HolidayOvertime6WD = (System.Decimal?)value;
							break;
						case "LeaderShift":

							if (value == null || value is System.Int32)
								this.LeaderShift = (System.Int32?)value;
							break;
						case "NightShift":

							if (value == null || value is System.Int32)
								this.NightShift = (System.Int32?)value;
							break;
						case "MealAllowance":

							if (value == null || value is System.Int32)
								this.MealAllowance = (System.Int32?)value;
							break;
						case "PayCut5WD":

							if (value == null || value is System.Int32)
								this.PayCut5WD = (System.Int32?)value;
							break;
						case "PayCut6WD":

							if (value == null || value is System.Int32)
								this.PayCut6WD = (System.Int32?)value;
							break;
						case "WeekdayNightShift":

							if (value == null || value is System.Int32)
								this.WeekdayNightShift = (System.Int32?)value;
							break;
						case "HolidayNightShift":

							if (value == null || value is System.Int32)
								this.HolidayNightShift = (System.Int32?)value;
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
		/// Maps to MonthlyAttendanceDetailSummary.MonthlyAttendanceDetailID
		/// </summary>
		virtual public System.Int64? MonthlyAttendanceDetailID
		{
			get
			{
				return base.GetSystemInt64(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.MonthlyAttendanceDetailID);
			}

			set
			{
				base.SetSystemInt64(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.MonthlyAttendanceDetailID, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.WeekdayOvertime5WD
		/// </summary>
		virtual public System.Decimal? WeekdayOvertime5WD
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayOvertime5WD);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayOvertime5WD, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.HolidayOvertime5WD
		/// </summary>
		virtual public System.Decimal? HolidayOvertime5WD
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayOvertime5WD);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayOvertime5WD, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.WeekdayOvertime6WD
		/// </summary>
		virtual public System.Decimal? WeekdayOvertime6WD
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayOvertime6WD);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayOvertime6WD, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.HolidayOvertime6WD
		/// </summary>
		virtual public System.Decimal? HolidayOvertime6WD
		{
			get
			{
				return base.GetSystemDecimal(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayOvertime6WD);
			}

			set
			{
				base.SetSystemDecimal(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayOvertime6WD, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.LeaderShift
		/// </summary>
		virtual public System.Int32? LeaderShift
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.LeaderShift);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.LeaderShift, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.NightShift
		/// </summary>
		virtual public System.Int32? NightShift
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.NightShift);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.NightShift, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.MealAllowance
		/// </summary>
		virtual public System.Int32? MealAllowance
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.MealAllowance);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.MealAllowance, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.PayCut5WD
		/// </summary>
		virtual public System.Int32? PayCut5WD
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.PayCut5WD);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.PayCut5WD, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.PayCut6WD
		/// </summary>
		virtual public System.Int32? PayCut6WD
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.PayCut6WD);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.PayCut6WD, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.WeekdayNightShift
		/// </summary>
		virtual public System.Int32? WeekdayNightShift
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayNightShift);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayNightShift, value);
			}
		}
		/// <summary>
		/// Maps to MonthlyAttendanceDetailSummary.HolidayNightShift
		/// </summary>
		virtual public System.Int32? HolidayNightShift
		{
			get
			{
				return base.GetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayNightShift);
			}

			set
			{
				base.SetSystemInt32(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayNightShift, value);
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
			public esStrings(esMonthlyAttendanceDetailSummary entity)
			{
				this.entity = entity;
			}
			public System.String MonthlyAttendanceDetailID
			{
				get
				{
					System.Int64? data = entity.MonthlyAttendanceDetailID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MonthlyAttendanceDetailID = null;
					else entity.MonthlyAttendanceDetailID = Convert.ToInt64(value);
				}
			}
			public System.String WeekdayOvertime5WD
			{
				get
				{
					System.Decimal? data = entity.WeekdayOvertime5WD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WeekdayOvertime5WD = null;
					else entity.WeekdayOvertime5WD = Convert.ToDecimal(value);
				}
			}
			public System.String HolidayOvertime5WD
			{
				get
				{
					System.Decimal? data = entity.HolidayOvertime5WD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HolidayOvertime5WD = null;
					else entity.HolidayOvertime5WD = Convert.ToDecimal(value);
				}
			}
			public System.String WeekdayOvertime6WD
			{
				get
				{
					System.Decimal? data = entity.WeekdayOvertime6WD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WeekdayOvertime6WD = null;
					else entity.WeekdayOvertime6WD = Convert.ToDecimal(value);
				}
			}
			public System.String HolidayOvertime6WD
			{
				get
				{
					System.Decimal? data = entity.HolidayOvertime6WD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HolidayOvertime6WD = null;
					else entity.HolidayOvertime6WD = Convert.ToDecimal(value);
				}
			}
			public System.String LeaderShift
			{
				get
				{
					System.Int32? data = entity.LeaderShift;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaderShift = null;
					else entity.LeaderShift = Convert.ToInt32(value);
				}
			}
			public System.String NightShift
			{
				get
				{
					System.Int32? data = entity.NightShift;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NightShift = null;
					else entity.NightShift = Convert.ToInt32(value);
				}
			}
			public System.String MealAllowance
			{
				get
				{
					System.Int32? data = entity.MealAllowance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MealAllowance = null;
					else entity.MealAllowance = Convert.ToInt32(value);
				}
			}
			public System.String PayCut5WD
			{
				get
				{
					System.Int32? data = entity.PayCut5WD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayCut5WD = null;
					else entity.PayCut5WD = Convert.ToInt32(value);
				}
			}
			public System.String PayCut6WD
			{
				get
				{
					System.Int32? data = entity.PayCut6WD;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayCut6WD = null;
					else entity.PayCut6WD = Convert.ToInt32(value);
				}
			}
			public System.String WeekdayNightShift
			{
				get
				{
					System.Int32? data = entity.WeekdayNightShift;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WeekdayNightShift = null;
					else entity.WeekdayNightShift = Convert.ToInt32(value);
				}
			}
			public System.String HolidayNightShift
			{
				get
				{
					System.Int32? data = entity.HolidayNightShift;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HolidayNightShift = null;
					else entity.HolidayNightShift = Convert.ToInt32(value);
				}
			}
			private esMonthlyAttendanceDetailSummary entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMonthlyAttendanceDetailSummaryQuery query)
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
				throw new Exception("esMonthlyAttendanceDetailSummary can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MonthlyAttendanceDetailSummary : esMonthlyAttendanceDetailSummary
	{
	}

	[Serializable]
	abstract public class esMonthlyAttendanceDetailSummaryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MonthlyAttendanceDetailSummaryMetadata.Meta();
			}
		}

		public esQueryItem MonthlyAttendanceDetailID
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.MonthlyAttendanceDetailID, esSystemType.Int64);
			}
		}

		public esQueryItem WeekdayOvertime5WD
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayOvertime5WD, esSystemType.Decimal);
			}
		}

		public esQueryItem HolidayOvertime5WD
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayOvertime5WD, esSystemType.Decimal);
			}
		}

		public esQueryItem WeekdayOvertime6WD
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayOvertime6WD, esSystemType.Decimal);
			}
		}

		public esQueryItem HolidayOvertime6WD
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayOvertime6WD, esSystemType.Decimal);
			}
		}

		public esQueryItem LeaderShift
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.LeaderShift, esSystemType.Int32);
			}
		}

		public esQueryItem NightShift
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.NightShift, esSystemType.Int32);
			}
		}

		public esQueryItem MealAllowance
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.MealAllowance, esSystemType.Int32);
			}
		}

		public esQueryItem PayCut5WD
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.PayCut5WD, esSystemType.Int32);
			}
		}

		public esQueryItem PayCut6WD
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.PayCut6WD, esSystemType.Int32);
			}
		}

		public esQueryItem WeekdayNightShift
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayNightShift, esSystemType.Int32);
			}
		}

		public esQueryItem HolidayNightShift
		{
			get
			{
				return new esQueryItem(this, MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayNightShift, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MonthlyAttendanceDetailSummaryCollection")]
	public partial class MonthlyAttendanceDetailSummaryCollection : esMonthlyAttendanceDetailSummaryCollection, IEnumerable<MonthlyAttendanceDetailSummary>
	{
		public MonthlyAttendanceDetailSummaryCollection()
		{

		}

		public static implicit operator List<MonthlyAttendanceDetailSummary>(MonthlyAttendanceDetailSummaryCollection coll)
		{
			List<MonthlyAttendanceDetailSummary> list = new List<MonthlyAttendanceDetailSummary>();

			foreach (MonthlyAttendanceDetailSummary emp in coll)
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
				return MonthlyAttendanceDetailSummaryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MonthlyAttendanceDetailSummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MonthlyAttendanceDetailSummary(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MonthlyAttendanceDetailSummary();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MonthlyAttendanceDetailSummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MonthlyAttendanceDetailSummaryQuery();
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
		public bool Load(MonthlyAttendanceDetailSummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MonthlyAttendanceDetailSummary AddNew()
		{
			MonthlyAttendanceDetailSummary entity = base.AddNewEntity() as MonthlyAttendanceDetailSummary;

			return entity;
		}
		public MonthlyAttendanceDetailSummary FindByPrimaryKey(Int64 monthlyAttendanceDetailID)
		{
			return base.FindByPrimaryKey(monthlyAttendanceDetailID) as MonthlyAttendanceDetailSummary;
		}

		#region IEnumerable< MonthlyAttendanceDetailSummary> Members

		IEnumerator<MonthlyAttendanceDetailSummary> IEnumerable<MonthlyAttendanceDetailSummary>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MonthlyAttendanceDetailSummary;
			}
		}

		#endregion

		private MonthlyAttendanceDetailSummaryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MonthlyAttendanceDetailSummary' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MonthlyAttendanceDetailSummary ({MonthlyAttendanceDetailID})")]
	[Serializable]
	public partial class MonthlyAttendanceDetailSummary : esMonthlyAttendanceDetailSummary
	{
		public MonthlyAttendanceDetailSummary()
		{
		}

		public MonthlyAttendanceDetailSummary(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MonthlyAttendanceDetailSummaryMetadata.Meta();
			}
		}

		override protected esMonthlyAttendanceDetailSummaryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MonthlyAttendanceDetailSummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MonthlyAttendanceDetailSummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MonthlyAttendanceDetailSummaryQuery();
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
		public bool Load(MonthlyAttendanceDetailSummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MonthlyAttendanceDetailSummaryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MonthlyAttendanceDetailSummaryQuery : esMonthlyAttendanceDetailSummaryQuery
	{
		public MonthlyAttendanceDetailSummaryQuery()
		{

		}

		public MonthlyAttendanceDetailSummaryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MonthlyAttendanceDetailSummaryQuery";
		}
	}

	[Serializable]
	public partial class MonthlyAttendanceDetailSummaryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MonthlyAttendanceDetailSummaryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.MonthlyAttendanceDetailID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.MonthlyAttendanceDetailID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayOvertime5WD, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.WeekdayOvertime5WD;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayOvertime5WD, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.HolidayOvertime5WD;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayOvertime6WD, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.WeekdayOvertime6WD;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayOvertime6WD, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.HolidayOvertime6WD;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.LeaderShift, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.LeaderShift;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.NightShift, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.NightShift;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.MealAllowance, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.MealAllowance;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.PayCut5WD, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.PayCut5WD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.PayCut6WD, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.PayCut6WD;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.WeekdayNightShift, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.WeekdayNightShift;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(MonthlyAttendanceDetailSummaryMetadata.ColumnNames.HolidayNightShift, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MonthlyAttendanceDetailSummaryMetadata.PropertyNames.HolidayNightShift;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public MonthlyAttendanceDetailSummaryMetadata Meta()
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
			public const string MonthlyAttendanceDetailID = "MonthlyAttendanceDetailID";
			public const string WeekdayOvertime5WD = "WeekdayOvertime5WD";
			public const string HolidayOvertime5WD = "HolidayOvertime5WD";
			public const string WeekdayOvertime6WD = "WeekdayOvertime6WD";
			public const string HolidayOvertime6WD = "HolidayOvertime6WD";
			public const string LeaderShift = "LeaderShift";
			public const string NightShift = "NightShift";
			public const string MealAllowance = "MealAllowance";
			public const string PayCut5WD = "PayCut5WD";
			public const string PayCut6WD = "PayCut6WD";
			public const string WeekdayNightShift = "WeekdayNightShift";
			public const string HolidayNightShift = "HolidayNightShift";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string MonthlyAttendanceDetailID = "MonthlyAttendanceDetailID";
			public const string WeekdayOvertime5WD = "WeekdayOvertime5WD";
			public const string HolidayOvertime5WD = "HolidayOvertime5WD";
			public const string WeekdayOvertime6WD = "WeekdayOvertime6WD";
			public const string HolidayOvertime6WD = "HolidayOvertime6WD";
			public const string LeaderShift = "LeaderShift";
			public const string NightShift = "NightShift";
			public const string MealAllowance = "MealAllowance";
			public const string PayCut5WD = "PayCut5WD";
			public const string PayCut6WD = "PayCut6WD";
			public const string WeekdayNightShift = "WeekdayNightShift";
			public const string HolidayNightShift = "HolidayNightShift";
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
			lock (typeof(MonthlyAttendanceDetailSummaryMetadata))
			{
				if (MonthlyAttendanceDetailSummaryMetadata.mapDelegates == null)
				{
					MonthlyAttendanceDetailSummaryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MonthlyAttendanceDetailSummaryMetadata.meta == null)
				{
					MonthlyAttendanceDetailSummaryMetadata.meta = new MonthlyAttendanceDetailSummaryMetadata();
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

				meta.AddTypeMap("MonthlyAttendanceDetailID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("WeekdayOvertime5WD", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HolidayOvertime5WD", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("WeekdayOvertime6WD", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HolidayOvertime6WD", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LeaderShift", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NightShift", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MealAllowance", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayCut5WD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayCut6WD", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WeekdayNightShift", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("HolidayNightShift", new esTypeMap("int", "System.Int32"));


				meta.Source = "MonthlyAttendanceDetailSummary";
				meta.Destination = "MonthlyAttendanceDetailSummary";
				meta.spInsert = "proc_MonthlyAttendanceDetailSummaryInsert";
				meta.spUpdate = "proc_MonthlyAttendanceDetailSummaryUpdate";
				meta.spDelete = "proc_MonthlyAttendanceDetailSummaryDelete";
				meta.spLoadAll = "proc_MonthlyAttendanceDetailSummaryLoadAll";
				meta.spLoadByPrimaryKey = "proc_MonthlyAttendanceDetailSummaryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MonthlyAttendanceDetailSummaryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
