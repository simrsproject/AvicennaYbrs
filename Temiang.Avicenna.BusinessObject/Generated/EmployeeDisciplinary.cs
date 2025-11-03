/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/24/2023 3:33:59 PM
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
	abstract public class esEmployeeDisciplinaryCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeDisciplinaryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeDisciplinaryCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeDisciplinaryQuery query)
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
			this.InitQuery(query as esEmployeeDisciplinaryQuery);
		}
		#endregion

		virtual public EmployeeDisciplinary DetachEntity(EmployeeDisciplinary entity)
		{
			return base.DetachEntity(entity) as EmployeeDisciplinary;
		}

		virtual public EmployeeDisciplinary AttachEntity(EmployeeDisciplinary entity)
		{
			return base.AttachEntity(entity) as EmployeeDisciplinary;
		}

		virtual public void Combine(EmployeeDisciplinaryCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeDisciplinary this[int index]
		{
			get
			{
				return base[index] as EmployeeDisciplinary;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeDisciplinary);
		}
	}

	[Serializable]
	abstract public class esEmployeeDisciplinary : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeDisciplinaryQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeDisciplinary()
		{
		}

		public esEmployeeDisciplinary(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 employeeDisciplinaryID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeDisciplinaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeDisciplinaryID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 employeeDisciplinaryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeDisciplinaryID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeDisciplinaryID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 employeeDisciplinaryID)
		{
			esEmployeeDisciplinaryQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeDisciplinaryID == employeeDisciplinaryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 employeeDisciplinaryID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeDisciplinaryID", employeeDisciplinaryID);
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
						case "EmployeeDisciplinaryID": this.str.EmployeeDisciplinaryID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRWarningLevel": this.str.SRWarningLevel = (string)value; break;
						case "IncidentDate": this.str.IncidentDate = (string)value; break;
						case "DateIssue": this.str.DateIssue = (string)value; break;
						case "Violation": this.str.Violation = (string)value; break;
						case "EffectViolation": this.str.EffectViolation = (string)value; break;
						case "AdviceGiven": this.str.AdviceGiven = (string)value; break;
						case "SanctionGiven": this.str.SanctionGiven = (string)value; break;
						case "SRViolationDegree": this.str.SRViolationDegree = (string)value; break;
						case "SRViolationType": this.str.SRViolationType = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "EffectiveDate": this.str.EffectiveDate = (string)value; break;
						case "ValidUntil": this.str.ValidUntil = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "EmployeeDisciplinaryID":

							if (value == null || value is System.Int32)
								this.EmployeeDisciplinaryID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "IncidentDate":

							if (value == null || value is System.DateTime)
								this.IncidentDate = (System.DateTime?)value;
							break;
						case "DateIssue":

							if (value == null || value is System.DateTime)
								this.DateIssue = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "EffectiveDate":

							if (value == null || value is System.DateTime)
								this.EffectiveDate = (System.DateTime?)value;
							break;
						case "ValidUntil":

							if (value == null || value is System.DateTime)
								this.ValidUntil = (System.DateTime?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeDisciplinary.EmployeeDisciplinaryID
		/// </summary>
		virtual public System.Int32? EmployeeDisciplinaryID
		{
			get
			{
				return base.GetSystemInt32(EmployeeDisciplinaryMetadata.ColumnNames.EmployeeDisciplinaryID);
			}

			set
			{
				base.SetSystemInt32(EmployeeDisciplinaryMetadata.ColumnNames.EmployeeDisciplinaryID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeDisciplinaryMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeDisciplinaryMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.SRWarningLevel
		/// </summary>
		virtual public System.String SRWarningLevel
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.SRWarningLevel);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.SRWarningLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.IncidentDate
		/// </summary>
		virtual public System.DateTime? IncidentDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.IncidentDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.IncidentDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.DateIssue
		/// </summary>
		virtual public System.DateTime? DateIssue
		{
			get
			{
				return base.GetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.DateIssue);
			}

			set
			{
				base.SetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.DateIssue, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.Violation
		/// </summary>
		virtual public System.String Violation
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.Violation);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.Violation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.EffectViolation
		/// </summary>
		virtual public System.String EffectViolation
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.EffectViolation);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.EffectViolation, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.AdviceGiven
		/// </summary>
		virtual public System.String AdviceGiven
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.AdviceGiven);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.AdviceGiven, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.SanctionGiven
		/// </summary>
		virtual public System.String SanctionGiven
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.SanctionGiven);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.SanctionGiven, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.SRViolationDegree
		/// </summary>
		virtual public System.String SRViolationDegree
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.SRViolationDegree);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.SRViolationDegree, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.SRViolationType
		/// </summary>
		virtual public System.String SRViolationType
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.SRViolationType);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.SRViolationType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.EffectiveDate
		/// </summary>
		virtual public System.DateTime? EffectiveDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.EffectiveDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.EffectiveDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.ValidUntil
		/// </summary>
		virtual public System.DateTime? ValidUntil
		{
			get
			{
				return base.GetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.ValidUntil);
			}

			set
			{
				base.SetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.ValidUntil, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeDisciplinaryMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeDisciplinary.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeDisciplinaryMetadata.ColumnNames.CreatedByUserID, value);
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
			public esStrings(esEmployeeDisciplinary entity)
			{
				this.entity = entity;
			}
			public System.String EmployeeDisciplinaryID
			{
				get
				{
					System.Int32? data = entity.EmployeeDisciplinaryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeDisciplinaryID = null;
					else entity.EmployeeDisciplinaryID = Convert.ToInt32(value);
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
			public System.String SRWarningLevel
			{
				get
				{
					System.String data = entity.SRWarningLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWarningLevel = null;
					else entity.SRWarningLevel = Convert.ToString(value);
				}
			}
			public System.String IncidentDate
			{
				get
				{
					System.DateTime? data = entity.IncidentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentDate = null;
					else entity.IncidentDate = Convert.ToDateTime(value);
				}
			}
			public System.String DateIssue
			{
				get
				{
					System.DateTime? data = entity.DateIssue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateIssue = null;
					else entity.DateIssue = Convert.ToDateTime(value);
				}
			}
			public System.String Violation
			{
				get
				{
					System.String data = entity.Violation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Violation = null;
					else entity.Violation = Convert.ToString(value);
				}
			}
			public System.String EffectViolation
			{
				get
				{
					System.String data = entity.EffectViolation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectViolation = null;
					else entity.EffectViolation = Convert.ToString(value);
				}
			}
			public System.String AdviceGiven
			{
				get
				{
					System.String data = entity.AdviceGiven;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdviceGiven = null;
					else entity.AdviceGiven = Convert.ToString(value);
				}
			}
			public System.String SanctionGiven
			{
				get
				{
					System.String data = entity.SanctionGiven;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SanctionGiven = null;
					else entity.SanctionGiven = Convert.ToString(value);
				}
			}
			public System.String SRViolationDegree
			{
				get
				{
					System.String data = entity.SRViolationDegree;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRViolationDegree = null;
					else entity.SRViolationDegree = Convert.ToString(value);
				}
			}
			public System.String SRViolationType
			{
				get
				{
					System.String data = entity.SRViolationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRViolationType = null;
					else entity.SRViolationType = Convert.ToString(value);
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
			public System.String EffectiveDate
			{
				get
				{
					System.DateTime? data = entity.EffectiveDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EffectiveDate = null;
					else entity.EffectiveDate = Convert.ToDateTime(value);
				}
			}
			public System.String ValidUntil
			{
				get
				{
					System.DateTime? data = entity.ValidUntil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidUntil = null;
					else entity.ValidUntil = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			private esEmployeeDisciplinary entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeDisciplinaryQuery query)
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
				throw new Exception("esEmployeeDisciplinary can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeDisciplinary : esEmployeeDisciplinary
	{
	}

	[Serializable]
	abstract public class esEmployeeDisciplinaryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeDisciplinaryMetadata.Meta();
			}
		}

		public esQueryItem EmployeeDisciplinaryID
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.EmployeeDisciplinaryID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRWarningLevel
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.SRWarningLevel, esSystemType.String);
			}
		}

		public esQueryItem IncidentDate
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.IncidentDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DateIssue
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.DateIssue, esSystemType.DateTime);
			}
		}

		public esQueryItem Violation
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.Violation, esSystemType.String);
			}
		}

		public esQueryItem EffectViolation
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.EffectViolation, esSystemType.String);
			}
		}

		public esQueryItem AdviceGiven
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.AdviceGiven, esSystemType.String);
			}
		}

		public esQueryItem SanctionGiven
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.SanctionGiven, esSystemType.String);
			}
		}

		public esQueryItem SRViolationDegree
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.SRViolationDegree, esSystemType.String);
			}
		}

		public esQueryItem SRViolationType
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.SRViolationType, esSystemType.String);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem EffectiveDate
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.EffectiveDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ValidUntil
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.ValidUntil, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeDisciplinaryMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeDisciplinaryCollection")]
	public partial class EmployeeDisciplinaryCollection : esEmployeeDisciplinaryCollection, IEnumerable<EmployeeDisciplinary>
	{
		public EmployeeDisciplinaryCollection()
		{

		}

		public static implicit operator List<EmployeeDisciplinary>(EmployeeDisciplinaryCollection coll)
		{
			List<EmployeeDisciplinary> list = new List<EmployeeDisciplinary>();

			foreach (EmployeeDisciplinary emp in coll)
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
				return EmployeeDisciplinaryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeDisciplinaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeDisciplinary(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeDisciplinary();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeDisciplinaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeDisciplinaryQuery();
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
		public bool Load(EmployeeDisciplinaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeDisciplinary AddNew()
		{
			EmployeeDisciplinary entity = base.AddNewEntity() as EmployeeDisciplinary;

			return entity;
		}
		public EmployeeDisciplinary FindByPrimaryKey(Int32 employeeDisciplinaryID)
		{
			return base.FindByPrimaryKey(employeeDisciplinaryID) as EmployeeDisciplinary;
		}

		#region IEnumerable< EmployeeDisciplinary> Members

		IEnumerator<EmployeeDisciplinary> IEnumerable<EmployeeDisciplinary>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeDisciplinary;
			}
		}

		#endregion

		private EmployeeDisciplinaryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeDisciplinary' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeDisciplinary ({EmployeeDisciplinaryID})")]
	[Serializable]
	public partial class EmployeeDisciplinary : esEmployeeDisciplinary
	{
		public EmployeeDisciplinary()
		{
		}

		public EmployeeDisciplinary(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeDisciplinaryMetadata.Meta();
			}
		}

		override protected esEmployeeDisciplinaryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeDisciplinaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeDisciplinaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeDisciplinaryQuery();
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
		public bool Load(EmployeeDisciplinaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeDisciplinaryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeDisciplinaryQuery : esEmployeeDisciplinaryQuery
	{
		public EmployeeDisciplinaryQuery()
		{

		}

		public EmployeeDisciplinaryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeDisciplinaryQuery";
		}
	}

	[Serializable]
	public partial class EmployeeDisciplinaryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeDisciplinaryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.EmployeeDisciplinaryID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.EmployeeDisciplinaryID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.SRWarningLevel, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.SRWarningLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.IncidentDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.IncidentDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.DateIssue, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.DateIssue;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.Violation, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.Violation;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.EffectViolation, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.EffectViolation;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.AdviceGiven, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.AdviceGiven;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.SanctionGiven, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.SanctionGiven;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.SRViolationDegree, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.SRViolationDegree;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.SRViolationType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.SRViolationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.Note, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.EffectiveDate, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.EffectiveDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.ValidUntil, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.ValidUntil;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.CreatedDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeDisciplinaryMetadata.ColumnNames.CreatedByUserID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeDisciplinaryMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeDisciplinaryMetadata Meta()
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
			public const string EmployeeDisciplinaryID = "EmployeeDisciplinaryID";
			public const string PersonID = "PersonID";
			public const string SRWarningLevel = "SRWarningLevel";
			public const string IncidentDate = "IncidentDate";
			public const string DateIssue = "DateIssue";
			public const string Violation = "Violation";
			public const string EffectViolation = "EffectViolation";
			public const string AdviceGiven = "AdviceGiven";
			public const string SanctionGiven = "SanctionGiven";
			public const string SRViolationDegree = "SRViolationDegree";
			public const string SRViolationType = "SRViolationType";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string EffectiveDate = "EffectiveDate";
			public const string ValidUntil = "ValidUntil";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string EmployeeDisciplinaryID = "EmployeeDisciplinaryID";
			public const string PersonID = "PersonID";
			public const string SRWarningLevel = "SRWarningLevel";
			public const string IncidentDate = "IncidentDate";
			public const string DateIssue = "DateIssue";
			public const string Violation = "Violation";
			public const string EffectViolation = "EffectViolation";
			public const string AdviceGiven = "AdviceGiven";
			public const string SanctionGiven = "SanctionGiven";
			public const string SRViolationDegree = "SRViolationDegree";
			public const string SRViolationType = "SRViolationType";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string EffectiveDate = "EffectiveDate";
			public const string ValidUntil = "ValidUntil";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(EmployeeDisciplinaryMetadata))
			{
				if (EmployeeDisciplinaryMetadata.mapDelegates == null)
				{
					EmployeeDisciplinaryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeDisciplinaryMetadata.meta == null)
				{
					EmployeeDisciplinaryMetadata.meta = new EmployeeDisciplinaryMetadata();
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

				meta.AddTypeMap("EmployeeDisciplinaryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRWarningLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncidentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DateIssue", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Violation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EffectViolation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdviceGiven", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SanctionGiven", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRViolationDegree", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRViolationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EffectiveDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidUntil", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeDisciplinary";
				meta.Destination = "EmployeeDisciplinary";
				meta.spInsert = "proc_EmployeeDisciplinaryInsert";
				meta.spUpdate = "proc_EmployeeDisciplinaryUpdate";
				meta.spDelete = "proc_EmployeeDisciplinaryDelete";
				meta.spLoadAll = "proc_EmployeeDisciplinaryLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeDisciplinaryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeDisciplinaryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
