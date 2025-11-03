/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/13/2020 4:44:08 PM
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
	abstract public class esPatientHealthRecordCollection : esEntityCollectionWAuditLog
	{
		public esPatientHealthRecordCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PatientHealthRecordCollection";
		}

		#region Query Logic
		protected void InitQuery(esPatientHealthRecordQuery query)
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
			this.InitQuery(query as esPatientHealthRecordQuery);
		}
		#endregion

		virtual public PatientHealthRecord DetachEntity(PatientHealthRecord entity)
		{
			return base.DetachEntity(entity) as PatientHealthRecord;
		}

		virtual public PatientHealthRecord AttachEntity(PatientHealthRecord entity)
		{
			return base.AttachEntity(entity) as PatientHealthRecord;
		}

		virtual public void Combine(PatientHealthRecordCollection collection)
		{
			base.Combine(collection);
		}

		new public PatientHealthRecord this[int index]
		{
			get
			{
				return base[index] as PatientHealthRecord;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PatientHealthRecord);
		}
	}

	[Serializable]
	abstract public class esPatientHealthRecord : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPatientHealthRecordQuery GetDynamicQuery()
		{
			return null;
		}

		public esPatientHealthRecord()
		{
		}

		public esPatientHealthRecord(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String registrationNo, String questionFormID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, registrationNo, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, registrationNo, questionFormID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String registrationNo, String questionFormID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, registrationNo, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, registrationNo, questionFormID);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String registrationNo, String questionFormID)
		{
			esPatientHealthRecordQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.RegistrationNo == registrationNo, query.QuestionFormID == questionFormID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String registrationNo, String questionFormID)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("QuestionFormID", questionFormID);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;
						case "RecordDate": this.str.RecordDate = (string)value; break;
						case "RecordTime": this.str.RecordTime = (string)value; break;
						case "EmployeeID": this.str.EmployeeID = (string)value; break;
						case "IsComplete": this.str.IsComplete = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ExaminerID": this.str.ExaminerID = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "ApprovedDatetime": this.str.ApprovedDatetime = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "LetterNo": this.str.LetterNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "RecordDate":

							if (value == null || value is System.DateTime)
								this.RecordDate = (System.DateTime?)value;
							break;
						case "IsComplete":

							if (value == null || value is System.Boolean)
								this.IsComplete = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "CreateDateTime":

							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "IsApproved":

							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "ApprovedDatetime":

							if (value == null || value is System.DateTime)
								this.ApprovedDatetime = (System.DateTime?)value;
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
		/// Maps to PatientHealthRecord.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.QuestionFormID);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.RecordDate
		/// </summary>
		virtual public System.DateTime? RecordDate
		{
			get
			{
				return base.GetSystemDateTime(PatientHealthRecordMetadata.ColumnNames.RecordDate);
			}

			set
			{
				base.SetSystemDateTime(PatientHealthRecordMetadata.ColumnNames.RecordDate, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.RecordTime
		/// </summary>
		virtual public System.String RecordTime
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.RecordTime);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.RecordTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.EmployeeID
		/// </summary>
		virtual public System.String EmployeeID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.EmployeeID);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.EmployeeID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.IsComplete
		/// </summary>
		virtual public System.Boolean? IsComplete
		{
			get
			{
				return base.GetSystemBoolean(PatientHealthRecordMetadata.ColumnNames.IsComplete);
			}

			set
			{
				base.SetSystemBoolean(PatientHealthRecordMetadata.ColumnNames.IsComplete, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientHealthRecordMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientHealthRecordMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.ExaminerID
		/// </summary>
		virtual public System.String ExaminerID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.ExaminerID);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.ExaminerID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.CreateByUserID);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PatientHealthRecordMetadata.ColumnNames.CreateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PatientHealthRecordMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.ReferenceNo);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(PatientHealthRecordMetadata.ColumnNames.IsApproved);
			}

			set
			{
				base.SetSystemBoolean(PatientHealthRecordMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.ApprovedDatetime
		/// </summary>
		virtual public System.DateTime? ApprovedDatetime
		{
			get
			{
				return base.GetSystemDateTime(PatientHealthRecordMetadata.ColumnNames.ApprovedDatetime);
			}

			set
			{
				base.SetSystemDateTime(PatientHealthRecordMetadata.ColumnNames.ApprovedDatetime, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.ApprovedByUserID);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PatientHealthRecord.LetterNo
		/// </summary>
		virtual public System.String LetterNo
		{
			get
			{
				return base.GetSystemString(PatientHealthRecordMetadata.ColumnNames.LetterNo);
			}

			set
			{
				base.SetSystemString(PatientHealthRecordMetadata.ColumnNames.LetterNo, value);
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
			public esStrings(esPatientHealthRecord entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
				}
			}
			public System.String RecordDate
			{
				get
				{
					System.DateTime? data = entity.RecordDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecordDate = null;
					else entity.RecordDate = Convert.ToDateTime(value);
				}
			}
			public System.String RecordTime
			{
				get
				{
					System.String data = entity.RecordTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecordTime = null;
					else entity.RecordTime = Convert.ToString(value);
				}
			}
			public System.String EmployeeID
			{
				get
				{
					System.String data = entity.EmployeeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeID = null;
					else entity.EmployeeID = Convert.ToString(value);
				}
			}
			public System.String IsComplete
			{
				get
				{
					System.Boolean? data = entity.IsComplete;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsComplete = null;
					else entity.IsComplete = Convert.ToBoolean(value);
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
			public System.String ExaminerID
			{
				get
				{
					System.String data = entity.ExaminerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExaminerID = null;
					else entity.ExaminerID = Convert.ToString(value);
				}
			}
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
				}
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
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
				}
			}
			public System.String ApprovedDatetime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDatetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDatetime = null;
					else entity.ApprovedDatetime = Convert.ToDateTime(value);
				}
			}
			public System.String ApprovedByUserID
			{
				get
				{
					System.String data = entity.ApprovedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedByUserID = null;
					else entity.ApprovedByUserID = Convert.ToString(value);
				}
			}
			public System.String LetterNo
			{
				get
				{
					System.String data = entity.LetterNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LetterNo = null;
					else entity.LetterNo = Convert.ToString(value);
				}
			}
			private esPatientHealthRecord entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPatientHealthRecordQuery query)
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
				throw new Exception("esPatientHealthRecord can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PatientHealthRecord : esPatientHealthRecord
	{
	}

	[Serializable]
	abstract public class esPatientHealthRecordQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PatientHealthRecordMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		}

		public esQueryItem RecordDate
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.RecordDate, esSystemType.DateTime);
			}
		}

		public esQueryItem RecordTime
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.RecordTime, esSystemType.String);
			}
		}

		public esQueryItem EmployeeID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.EmployeeID, esSystemType.String);
			}
		}

		public esQueryItem IsComplete
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.IsComplete, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem ExaminerID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.ExaminerID, esSystemType.String);
			}
		}

		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		}

		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		}

		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		}

		public esQueryItem ApprovedDatetime
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.ApprovedDatetime, esSystemType.DateTime);
			}
		}

		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LetterNo
		{
			get
			{
				return new esQueryItem(this, PatientHealthRecordMetadata.ColumnNames.LetterNo, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PatientHealthRecordCollection")]
	public partial class PatientHealthRecordCollection : esPatientHealthRecordCollection, IEnumerable<PatientHealthRecord>
	{
		public PatientHealthRecordCollection()
		{

		}

		public static implicit operator List<PatientHealthRecord>(PatientHealthRecordCollection coll)
		{
			List<PatientHealthRecord> list = new List<PatientHealthRecord>();

			foreach (PatientHealthRecord emp in coll)
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
				return PatientHealthRecordMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientHealthRecordQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PatientHealthRecord(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PatientHealthRecord();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PatientHealthRecordQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientHealthRecordQuery();
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
		public bool Load(PatientHealthRecordQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PatientHealthRecord AddNew()
		{
			PatientHealthRecord entity = base.AddNewEntity() as PatientHealthRecord;

			return entity;
		}
		public PatientHealthRecord FindByPrimaryKey(String transactionNo, String registrationNo, String questionFormID)
		{
			return base.FindByPrimaryKey(transactionNo, registrationNo, questionFormID) as PatientHealthRecord;
		}

		#region IEnumerable< PatientHealthRecord> Members

		IEnumerator<PatientHealthRecord> IEnumerable<PatientHealthRecord>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PatientHealthRecord;
			}
		}

		#endregion

		private PatientHealthRecordQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PatientHealthRecord' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PatientHealthRecord ({TransactionNo, RegistrationNo, QuestionFormID})")]
	[Serializable]
	public partial class PatientHealthRecord : esPatientHealthRecord
	{
		public PatientHealthRecord()
		{
		}

		public PatientHealthRecord(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PatientHealthRecordMetadata.Meta();
			}
		}

		override protected esPatientHealthRecordQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PatientHealthRecordQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PatientHealthRecordQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PatientHealthRecordQuery();
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
		public bool Load(PatientHealthRecordQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PatientHealthRecordQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PatientHealthRecordQuery : esPatientHealthRecordQuery
	{
		public PatientHealthRecordQuery()
		{

		}

		public PatientHealthRecordQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PatientHealthRecordQuery";
		}
	}

	[Serializable]
	public partial class PatientHealthRecordMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PatientHealthRecordMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.QuestionFormID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.RecordDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.RecordDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.RecordTime, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.RecordTime;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.EmployeeID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.EmployeeID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.IsComplete, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.IsComplete;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.ExaminerID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.ExaminerID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.CreateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.CreateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.ServiceUnitID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.ReferenceNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.IsApproved, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.ApprovedDatetime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.ApprovedDatetime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.ApprovedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PatientHealthRecordMetadata.ColumnNames.LetterNo, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = PatientHealthRecordMetadata.PropertyNames.LetterNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PatientHealthRecordMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string RecordDate = "RecordDate";
			public const string RecordTime = "RecordTime";
			public const string EmployeeID = "EmployeeID";
			public const string IsComplete = "IsComplete";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ExaminerID = "ExaminerID";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ReferenceNo = "ReferenceNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDatetime = "ApprovedDatetime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string LetterNo = "LetterNo";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string RegistrationNo = "RegistrationNo";
			public const string QuestionFormID = "QuestionFormID";
			public const string RecordDate = "RecordDate";
			public const string RecordTime = "RecordTime";
			public const string EmployeeID = "EmployeeID";
			public const string IsComplete = "IsComplete";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ExaminerID = "ExaminerID";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ReferenceNo = "ReferenceNo";
			public const string IsApproved = "IsApproved";
			public const string ApprovedDatetime = "ApprovedDatetime";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string LetterNo = "LetterNo";
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
			lock (typeof(PatientHealthRecordMetadata))
			{
				if (PatientHealthRecordMetadata.mapDelegates == null)
				{
					PatientHealthRecordMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PatientHealthRecordMetadata.meta == null)
				{
					PatientHealthRecordMetadata.meta = new PatientHealthRecordMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RecordDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("RecordTime", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EmployeeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsComplete", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExaminerID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDatetime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LetterNo", new esTypeMap("varchar", "System.String"));


				meta.Source = "PatientHealthRecord";
				meta.Destination = "PatientHealthRecord";
				meta.spInsert = "proc_PatientHealthRecordInsert";
				meta.spUpdate = "proc_PatientHealthRecordUpdate";
				meta.spDelete = "proc_PatientHealthRecordDelete";
				meta.spLoadAll = "proc_PatientHealthRecordLoadAll";
				meta.spLoadByPrimaryKey = "proc_PatientHealthRecordLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PatientHealthRecordMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
