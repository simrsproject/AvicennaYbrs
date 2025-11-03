/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/10/2021 1:30:52 PM
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
	abstract public class esEmployeeEducationCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeEducationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeEducationCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeEducationQuery query)
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
			this.InitQuery(query as esEmployeeEducationQuery);
		}
		#endregion

		virtual public EmployeeEducation DetachEntity(EmployeeEducation entity)
		{
			return base.DetachEntity(entity) as EmployeeEducation;
		}

		virtual public EmployeeEducation AttachEntity(EmployeeEducation entity)
		{
			return base.AttachEntity(entity) as EmployeeEducation;
		}

		virtual public void Combine(EmployeeEducationCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeEducation this[int index]
		{
			get
			{
				return base[index] as EmployeeEducation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeEducation);
		}
	}

	[Serializable]
	abstract public class esEmployeeEducation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeEducationQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeEducation()
		{
		}

		public esEmployeeEducation(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeEducationID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeEducationID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeEducationID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeEducationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeEducationID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeEducationID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeEducationID)
		{
			esEmployeeEducationQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeEducationID == employeeEducationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeEducationID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeEducationID", employeeEducationID);
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
						case "EmployeeEducationID": this.str.EmployeeEducationID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SREducationStatus": this.str.SREducationStatus = (string)value; break;
						case "SREducationFinancingSources": this.str.SREducationFinancingSources = (string)value; break;
						case "IsTuitionAssistance": this.str.IsTuitionAssistance = (string)value; break;
						case "AssistanceAmount": this.str.AssistanceAmount = (string)value; break;
						case "InstitutionName": this.str.InstitutionName = (string)value; break;
						case "StudyProgram": this.str.StudyProgram = (string)value; break;
						case "StartYearPeriod": this.str.StartYearPeriod = (string)value; break;
						case "EndYearPeriod": this.str.EndYearPeriod = (string)value; break;
						case "SRStudyPeriodStatus": this.str.SRStudyPeriodStatus = (string)value; break;
						case "IsCommitmentToWork": this.str.IsCommitmentToWork = (string)value; break;
						case "DurationOfService": this.str.DurationOfService = (string)value; break;
						case "StartServiceDate": this.str.StartServiceDate = (string)value; break;
						case "EndServiceDate": this.str.EndServiceDate = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeEducationID":

							if (value == null || value is System.Int32)
								this.EmployeeEducationID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "IsTuitionAssistance":

							if (value == null || value is System.Boolean)
								this.IsTuitionAssistance = (System.Boolean?)value;
							break;
						case "AssistanceAmount":

							if (value == null || value is System.Decimal)
								this.AssistanceAmount = (System.Decimal?)value;
							break;
						case "IsCommitmentToWork":

							if (value == null || value is System.Boolean)
								this.IsCommitmentToWork = (System.Boolean?)value;
							break;
						case "StartServiceDate":

							if (value == null || value is System.DateTime)
								this.StartServiceDate = (System.DateTime?)value;
							break;
						case "EndServiceDate":

							if (value == null || value is System.DateTime)
								this.EndServiceDate = (System.DateTime?)value;
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
		/// Maps to EmployeeEducation.EmployeeEducationID
		/// </summary>
		virtual public System.Int32? EmployeeEducationID
		{
			get
			{
				return base.GetSystemInt32(EmployeeEducationMetadata.ColumnNames.EmployeeEducationID);
			}

			set
			{
				base.SetSystemInt32(EmployeeEducationMetadata.ColumnNames.EmployeeEducationID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeEducationMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeEducationMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.SREducationStatus
		/// </summary>
		virtual public System.String SREducationStatus
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.SREducationStatus);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.SREducationStatus, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.SREducationFinancingSources
		/// </summary>
		virtual public System.String SREducationFinancingSources
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.SREducationFinancingSources);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.SREducationFinancingSources, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.IsTuitionAssistance
		/// </summary>
		virtual public System.Boolean? IsTuitionAssistance
		{
			get
			{
				return base.GetSystemBoolean(EmployeeEducationMetadata.ColumnNames.IsTuitionAssistance);
			}

			set
			{
				base.SetSystemBoolean(EmployeeEducationMetadata.ColumnNames.IsTuitionAssistance, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.AssistanceAmount
		/// </summary>
		virtual public System.Decimal? AssistanceAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeEducationMetadata.ColumnNames.AssistanceAmount);
			}

			set
			{
				base.SetSystemDecimal(EmployeeEducationMetadata.ColumnNames.AssistanceAmount, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.InstitutionName
		/// </summary>
		virtual public System.String InstitutionName
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.InstitutionName);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.InstitutionName, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.StudyProgram
		/// </summary>
		virtual public System.String StudyProgram
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.StudyProgram);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.StudyProgram, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.StartYearPeriod
		/// </summary>
		virtual public System.String StartYearPeriod
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.StartYearPeriod);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.StartYearPeriod, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.EndYearPeriod
		/// </summary>
		virtual public System.String EndYearPeriod
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.EndYearPeriod);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.EndYearPeriod, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.SRStudyPeriodStatus
		/// </summary>
		virtual public System.String SRStudyPeriodStatus
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.SRStudyPeriodStatus);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.SRStudyPeriodStatus, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.IsCommitmentToWork
		/// </summary>
		virtual public System.Boolean? IsCommitmentToWork
		{
			get
			{
				return base.GetSystemBoolean(EmployeeEducationMetadata.ColumnNames.IsCommitmentToWork);
			}

			set
			{
				base.SetSystemBoolean(EmployeeEducationMetadata.ColumnNames.IsCommitmentToWork, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.DurationOfService
		/// </summary>
		virtual public System.String DurationOfService
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.DurationOfService);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.DurationOfService, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.StartServiceDate
		/// </summary>
		virtual public System.DateTime? StartServiceDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEducationMetadata.ColumnNames.StartServiceDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEducationMetadata.ColumnNames.StartServiceDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.EndServiceDate
		/// </summary>
		virtual public System.DateTime? EndServiceDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEducationMetadata.ColumnNames.EndServiceDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEducationMetadata.ColumnNames.EndServiceDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeEducationMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeEducationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeEducation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeEducationMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeEducationMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeEducation entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeEducationID
			{
				get
				{
					System.Int32? data = entity.EmployeeEducationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeEducationID = null;
					else entity.EmployeeEducationID = Convert.ToInt32(value);
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
			public System.String SREducationStatus
			{
				get
				{
					System.String data = entity.SREducationStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationStatus = null;
					else entity.SREducationStatus = Convert.ToString(value);
				}
			}
			public System.String SREducationFinancingSources
			{
				get
				{
					System.String data = entity.SREducationFinancingSources;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationFinancingSources = null;
					else entity.SREducationFinancingSources = Convert.ToString(value);
				}
			}
			public System.String IsTuitionAssistance
			{
				get
				{
					System.Boolean? data = entity.IsTuitionAssistance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTuitionAssistance = null;
					else entity.IsTuitionAssistance = Convert.ToBoolean(value);
				}
			}
			public System.String AssistanceAmount
			{
				get
				{
					System.Decimal? data = entity.AssistanceAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistanceAmount = null;
					else entity.AssistanceAmount = Convert.ToDecimal(value);
				}
			}
			public System.String InstitutionName
			{
				get
				{
					System.String data = entity.InstitutionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstitutionName = null;
					else entity.InstitutionName = Convert.ToString(value);
				}
			}
			public System.String StudyProgram
			{
				get
				{
					System.String data = entity.StudyProgram;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StudyProgram = null;
					else entity.StudyProgram = Convert.ToString(value);
				}
			}
			public System.String StartYearPeriod
			{
				get
				{
					System.String data = entity.StartYearPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartYearPeriod = null;
					else entity.StartYearPeriod = Convert.ToString(value);
				}
			}
			public System.String EndYearPeriod
			{
				get
				{
					System.String data = entity.EndYearPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndYearPeriod = null;
					else entity.EndYearPeriod = Convert.ToString(value);
				}
			}
			public System.String SRStudyPeriodStatus
			{
				get
				{
					System.String data = entity.SRStudyPeriodStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRStudyPeriodStatus = null;
					else entity.SRStudyPeriodStatus = Convert.ToString(value);
				}
			}
			public System.String IsCommitmentToWork
			{
				get
				{
					System.Boolean? data = entity.IsCommitmentToWork;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCommitmentToWork = null;
					else entity.IsCommitmentToWork = Convert.ToBoolean(value);
				}
			}
			public System.String DurationOfService
			{
				get
				{
					System.String data = entity.DurationOfService;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DurationOfService = null;
					else entity.DurationOfService = Convert.ToString(value);
				}
			}
			public System.String StartServiceDate
			{
				get
				{
					System.DateTime? data = entity.StartServiceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartServiceDate = null;
					else entity.StartServiceDate = Convert.ToDateTime(value);
				}
			}
			public System.String EndServiceDate
			{
				get
				{
					System.DateTime? data = entity.EndServiceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndServiceDate = null;
					else entity.EndServiceDate = Convert.ToDateTime(value);
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
			private esEmployeeEducation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeEducationQuery query)
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
				throw new Exception("esEmployeeEducation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeEducation : esEmployeeEducation
	{
	}

	[Serializable]
	abstract public class esEmployeeEducationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeEducationMetadata.Meta();
			}
		}

		public esQueryItem EmployeeEducationID
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.EmployeeEducationID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SREducationStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.SREducationStatus, esSystemType.String);
			}
		}

		public esQueryItem SREducationFinancingSources
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.SREducationFinancingSources, esSystemType.String);
			}
		}

		public esQueryItem IsTuitionAssistance
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.IsTuitionAssistance, esSystemType.Boolean);
			}
		}

		public esQueryItem AssistanceAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.AssistanceAmount, esSystemType.Decimal);
			}
		}

		public esQueryItem InstitutionName
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.InstitutionName, esSystemType.String);
			}
		}

		public esQueryItem StudyProgram
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.StudyProgram, esSystemType.String);
			}
		}

		public esQueryItem StartYearPeriod
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.StartYearPeriod, esSystemType.String);
			}
		}

		public esQueryItem EndYearPeriod
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.EndYearPeriod, esSystemType.String);
			}
		}

		public esQueryItem SRStudyPeriodStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.SRStudyPeriodStatus, esSystemType.String);
			}
		}

		public esQueryItem IsCommitmentToWork
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.IsCommitmentToWork, esSystemType.Boolean);
			}
		}

		public esQueryItem DurationOfService
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.DurationOfService, esSystemType.String);
			}
		}

		public esQueryItem StartServiceDate
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.StartServiceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndServiceDate
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.EndServiceDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeEducationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeEducationCollection")]
	public partial class EmployeeEducationCollection : esEmployeeEducationCollection, IEnumerable<EmployeeEducation>
	{
		public EmployeeEducationCollection()
		{

		}

		public static implicit operator List<EmployeeEducation>(EmployeeEducationCollection coll)
		{
			List<EmployeeEducation> list = new List<EmployeeEducation>();

			foreach (EmployeeEducation emp in coll)
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
				return EmployeeEducationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeEducationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeEducation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeEducation();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeEducationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeEducationQuery();
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
		public bool Load(EmployeeEducationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeEducation AddNew()
		{
			EmployeeEducation entity = base.AddNewEntity() as EmployeeEducation;

			return entity;
		}
		public EmployeeEducation FindByPrimaryKey(Int32 employeeEducationID)
		{
			return base.FindByPrimaryKey(employeeEducationID) as EmployeeEducation;
		}

		#region IEnumerable< EmployeeEducation> Members

		IEnumerator<EmployeeEducation> IEnumerable<EmployeeEducation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeEducation;
			}
		}

		#endregion

		private EmployeeEducationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeEducation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeEducation ({EmployeeEducationID})")]
	[Serializable]
	public partial class EmployeeEducation : esEmployeeEducation
	{
		public EmployeeEducation()
		{
		}

		public EmployeeEducation(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeEducationMetadata.Meta();
			}
		}

		override protected esEmployeeEducationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeEducationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeEducationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeEducationQuery();
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
		public bool Load(EmployeeEducationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeEducationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeEducationQuery : esEmployeeEducationQuery
	{
		public EmployeeEducationQuery()
		{

		}

		public EmployeeEducationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeEducationQuery";
		}
	}

	[Serializable]
	public partial class EmployeeEducationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeEducationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.EmployeeEducationID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.EmployeeEducationID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.SREducationStatus, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.SREducationStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.SREducationFinancingSources, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.SREducationFinancingSources;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.IsTuitionAssistance, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.IsTuitionAssistance;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.AssistanceAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.AssistanceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.InstitutionName, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.InstitutionName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.StudyProgram, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.StudyProgram;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.StartYearPeriod, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.StartYearPeriod;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.EndYearPeriod, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.EndYearPeriod;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.SRStudyPeriodStatus, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.SRStudyPeriodStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.IsCommitmentToWork, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.IsCommitmentToWork;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.DurationOfService, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.DurationOfService;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.StartServiceDate, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.StartServiceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.EndServiceDate, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.EndServiceDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeEducationMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeEducationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeEducationMetadata Meta()
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
			public const string EmployeeEducationID = "EmployeeEducationID";
			public const string PersonID = "PersonID";
			public const string SREducationStatus = "SREducationStatus";
			public const string SREducationFinancingSources = "SREducationFinancingSources";
			public const string IsTuitionAssistance = "IsTuitionAssistance";
			public const string AssistanceAmount = "AssistanceAmount";
			public const string InstitutionName = "InstitutionName";
			public const string StudyProgram = "StudyProgram";
			public const string StartYearPeriod = "StartYearPeriod";
			public const string EndYearPeriod = "EndYearPeriod";
			public const string SRStudyPeriodStatus = "SRStudyPeriodStatus";
			public const string IsCommitmentToWork = "IsCommitmentToWork";
			public const string DurationOfService = "DurationOfService";
			public const string StartServiceDate = "StartServiceDate";
			public const string EndServiceDate = "EndServiceDate";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeEducationID = "EmployeeEducationID";
			public const string PersonID = "PersonID";
			public const string SREducationStatus = "SREducationStatus";
			public const string SREducationFinancingSources = "SREducationFinancingSources";
			public const string IsTuitionAssistance = "IsTuitionAssistance";
			public const string AssistanceAmount = "AssistanceAmount";
			public const string InstitutionName = "InstitutionName";
			public const string StudyProgram = "StudyProgram";
			public const string StartYearPeriod = "StartYearPeriod";
			public const string EndYearPeriod = "EndYearPeriod";
			public const string SRStudyPeriodStatus = "SRStudyPeriodStatus";
			public const string IsCommitmentToWork = "IsCommitmentToWork";
			public const string DurationOfService = "DurationOfService";
			public const string StartServiceDate = "StartServiceDate";
			public const string EndServiceDate = "EndServiceDate";
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
			lock (typeof(EmployeeEducationMetadata))
			{
				if (EmployeeEducationMetadata.mapDelegates == null)
				{
					EmployeeEducationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeEducationMetadata.meta == null)
				{
					EmployeeEducationMetadata.meta = new EmployeeEducationMetadata();
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

				meta.AddTypeMap("EmployeeEducationID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationFinancingSources", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsTuitionAssistance", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AssistanceAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("InstitutionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StudyProgram", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartYearPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EndYearPeriod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRStudyPeriodStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCommitmentToWork", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DurationOfService", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartServiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndServiceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeEducation";
				meta.Destination = "EmployeeEducation";
				meta.spInsert = "proc_EmployeeEducationInsert";
				meta.spUpdate = "proc_EmployeeEducationUpdate";
				meta.spDelete = "proc_EmployeeEducationDelete";
				meta.spLoadAll = "proc_EmployeeEducationLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeEducationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeEducationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
