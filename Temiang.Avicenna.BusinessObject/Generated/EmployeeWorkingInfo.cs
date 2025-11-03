/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/15/2022 10:20:54 AM
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
	abstract public class esEmployeeWorkingInfoCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeWorkingInfoCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeWorkingInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeWorkingInfoQuery query)
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
			this.InitQuery(query as esEmployeeWorkingInfoQuery);
		}
		#endregion

		virtual public EmployeeWorkingInfo DetachEntity(EmployeeWorkingInfo entity)
		{
			return base.DetachEntity(entity) as EmployeeWorkingInfo;
		}

		virtual public EmployeeWorkingInfo AttachEntity(EmployeeWorkingInfo entity)
		{
			return base.AttachEntity(entity) as EmployeeWorkingInfo;
		}

		virtual public void Combine(EmployeeWorkingInfoCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeWorkingInfo this[int index]
		{
			get
			{
				return base[index] as EmployeeWorkingInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeWorkingInfo);
		}
	}

	[Serializable]
	abstract public class esEmployeeWorkingInfo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeWorkingInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeWorkingInfo()
		{
		}

		public esEmployeeWorkingInfo(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personID)
		{
			esEmployeeWorkingInfoQuery query = this.GetDynamicQuery();
			query.Where(query.PersonID == personID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonID", personID);
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
						case "PersonID": this.str.PersonID = (string)value; break;
						case "JoinDate": this.str.JoinDate = (string)value; break;
						case "SupervisorId": this.str.SupervisorId = (string)value; break;
						case "SREmployeeStatus": this.str.SREmployeeStatus = (string)value; break;
						case "SREmployeeType": this.str.SREmployeeType = (string)value; break;
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;
						case "SRRemunerationType": this.str.SRRemunerationType = (string)value; break;
						case "AbsenceCardNo": this.str.AbsenceCardNo = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsKWI": this.str.IsKWI = (string)value; break;
						case "GradeYear": this.str.GradeYear = (string)value; break;
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
						case "ResignDate": this.str.ResignDate = (string)value; break;
						case "SRResignReason": this.str.SRResignReason = (string)value; break;
						case "EmployeeRegistrationNo": this.str.EmployeeRegistrationNo = (string)value; break;
						case "PreceptorId": this.str.PreceptorId = (string)value; break;
						case "SREmployeeShiftType": this.str.SREmployeeShiftType = (string)value; break;
						case "SREmployeeScheduleType": this.str.SREmployeeScheduleType = (string)value; break;
						case "SRProfessionType": this.str.SRProfessionType = (string)value; break;
						case "SRClinicalWorkArea": this.str.SRClinicalWorkArea = (string)value; break;
						case "SRClinicalAuthorityLevel": this.str.SRClinicalAuthorityLevel = (string)value; break;
						case "ManagerID": this.str.ManagerID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "JoinDate":

							if (value == null || value is System.DateTime)
								this.JoinDate = (System.DateTime?)value;
							break;
						case "SupervisorId":

							if (value == null || value is System.Int32)
								this.SupervisorId = (System.Int32?)value;
							break;
						case "PositionGradeID":

							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsKWI":

							if (value == null || value is System.Boolean)
								this.IsKWI = (System.Boolean?)value;
							break;
						case "GradeYear":

							if (value == null || value is System.Decimal)
								this.GradeYear = (System.Decimal?)value;
							break;
						case "ResignDate":

							if (value == null || value is System.DateTime)
								this.ResignDate = (System.DateTime?)value;
							break;
						case "PreceptorId":

							if (value == null || value is System.Int32)
								this.PreceptorId = (System.Int32?)value;
							break;
						case "ManagerID":

							if (value == null || value is System.Int32)
								this.ManagerID = (System.Int32?)value;
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
		/// Maps to EmployeeWorkingInfo.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.JoinDate
		/// </summary>
		virtual public System.DateTime? JoinDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWorkingInfoMetadata.ColumnNames.JoinDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWorkingInfoMetadata.ColumnNames.JoinDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SupervisorId
		/// </summary>
		virtual public System.Int32? SupervisorId
		{
			get
			{
				return base.GetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.SupervisorId);
			}

			set
			{
				base.SetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.SupervisorId, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SREmployeeStatus
		/// </summary>
		virtual public System.String SREmployeeStatus
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeStatus);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeStatus, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SREmployeeType
		/// </summary>
		virtual public System.String SREmployeeType
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeType);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.PositionGradeID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SRRemunerationType
		/// </summary>
		virtual public System.String SRRemunerationType
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRRemunerationType);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRRemunerationType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.AbsenceCardNo
		/// </summary>
		virtual public System.String AbsenceCardNo
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.AbsenceCardNo);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.AbsenceCardNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWorkingInfoMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWorkingInfoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.IsKWI
		/// </summary>
		virtual public System.Boolean? IsKWI
		{
			get
			{
				return base.GetSystemBoolean(EmployeeWorkingInfoMetadata.ColumnNames.IsKWI);
			}

			set
			{
				base.SetSystemBoolean(EmployeeWorkingInfoMetadata.ColumnNames.IsKWI, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.GradeYear
		/// </summary>
		virtual public System.Decimal? GradeYear
		{
			get
			{
				return base.GetSystemDecimal(EmployeeWorkingInfoMetadata.ColumnNames.GradeYear);
			}

			set
			{
				base.SetSystemDecimal(EmployeeWorkingInfoMetadata.ColumnNames.GradeYear, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREducationLevel);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.ResignDate
		/// </summary>
		virtual public System.DateTime? ResignDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeWorkingInfoMetadata.ColumnNames.ResignDate);
			}

			set
			{
				base.SetSystemDateTime(EmployeeWorkingInfoMetadata.ColumnNames.ResignDate, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SRResignReason
		/// </summary>
		virtual public System.String SRResignReason
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRResignReason);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRResignReason, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.EmployeeRegistrationNo
		/// </summary>
		virtual public System.String EmployeeRegistrationNo
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.EmployeeRegistrationNo);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.EmployeeRegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.PreceptorId
		/// </summary>
		virtual public System.Int32? PreceptorId
		{
			get
			{
				return base.GetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.PreceptorId);
			}

			set
			{
				base.SetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.PreceptorId, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SREmployeeShiftType
		/// </summary>
		virtual public System.String SREmployeeShiftType
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeShiftType);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeShiftType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SREmployeeScheduleType
		/// </summary>
		virtual public System.String SREmployeeScheduleType
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeScheduleType);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeScheduleType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SRProfessionType
		/// </summary>
		virtual public System.String SRProfessionType
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRProfessionType);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRProfessionType, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SRClinicalWorkArea
		/// </summary>
		virtual public System.String SRClinicalWorkArea
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRClinicalWorkArea);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRClinicalWorkArea, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.SRClinicalAuthorityLevel
		/// </summary>
		virtual public System.String SRClinicalAuthorityLevel
		{
			get
			{
				return base.GetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRClinicalAuthorityLevel);
			}

			set
			{
				base.SetSystemString(EmployeeWorkingInfoMetadata.ColumnNames.SRClinicalAuthorityLevel, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeWorkingInfo.ManagerID
		/// </summary>
		virtual public System.Int32? ManagerID
		{
			get
			{
				return base.GetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.ManagerID);
			}

			set
			{
				base.SetSystemInt32(EmployeeWorkingInfoMetadata.ColumnNames.ManagerID, value);
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
			public esStrings(esEmployeeWorkingInfo entity)
			{
				this.entity = entity;
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
			public System.String JoinDate
			{
				get
				{
					System.DateTime? data = entity.JoinDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JoinDate = null;
					else entity.JoinDate = Convert.ToDateTime(value);
				}
			}
			public System.String SupervisorId
			{
				get
				{
					System.Int32? data = entity.SupervisorId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorId = null;
					else entity.SupervisorId = Convert.ToInt32(value);
				}
			}
			public System.String SREmployeeStatus
			{
				get
				{
					System.String data = entity.SREmployeeStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeStatus = null;
					else entity.SREmployeeStatus = Convert.ToString(value);
				}
			}
			public System.String SREmployeeType
			{
				get
				{
					System.String data = entity.SREmployeeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeType = null;
					else entity.SREmployeeType = Convert.ToString(value);
				}
			}
			public System.String PositionGradeID
			{
				get
				{
					System.Int32? data = entity.PositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionGradeID = null;
					else entity.PositionGradeID = Convert.ToInt32(value);
				}
			}
			public System.String SRRemunerationType
			{
				get
				{
					System.String data = entity.SRRemunerationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRemunerationType = null;
					else entity.SRRemunerationType = Convert.ToString(value);
				}
			}
			public System.String AbsenceCardNo
			{
				get
				{
					System.String data = entity.AbsenceCardNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AbsenceCardNo = null;
					else entity.AbsenceCardNo = Convert.ToString(value);
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
			public System.String IsKWI
			{
				get
				{
					System.Boolean? data = entity.IsKWI;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsKWI = null;
					else entity.IsKWI = Convert.ToBoolean(value);
				}
			}
			public System.String GradeYear
			{
				get
				{
					System.Decimal? data = entity.GradeYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GradeYear = null;
					else entity.GradeYear = Convert.ToDecimal(value);
				}
			}
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
				}
			}
			public System.String ResignDate
			{
				get
				{
					System.DateTime? data = entity.ResignDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResignDate = null;
					else entity.ResignDate = Convert.ToDateTime(value);
				}
			}
			public System.String SRResignReason
			{
				get
				{
					System.String data = entity.SRResignReason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRResignReason = null;
					else entity.SRResignReason = Convert.ToString(value);
				}
			}
			public System.String EmployeeRegistrationNo
			{
				get
				{
					System.String data = entity.EmployeeRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeRegistrationNo = null;
					else entity.EmployeeRegistrationNo = Convert.ToString(value);
				}
			}
			public System.String PreceptorId
			{
				get
				{
					System.Int32? data = entity.PreceptorId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PreceptorId = null;
					else entity.PreceptorId = Convert.ToInt32(value);
				}
			}
			public System.String SREmployeeShiftType
			{
				get
				{
					System.String data = entity.SREmployeeShiftType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeShiftType = null;
					else entity.SREmployeeShiftType = Convert.ToString(value);
				}
			}
			public System.String SREmployeeScheduleType
			{
				get
				{
					System.String data = entity.SREmployeeScheduleType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeScheduleType = null;
					else entity.SREmployeeScheduleType = Convert.ToString(value);
				}
			}
			public System.String SRProfessionType
			{
				get
				{
					System.String data = entity.SRProfessionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProfessionType = null;
					else entity.SRProfessionType = Convert.ToString(value);
				}
			}
			public System.String SRClinicalWorkArea
			{
				get
				{
					System.String data = entity.SRClinicalWorkArea;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalWorkArea = null;
					else entity.SRClinicalWorkArea = Convert.ToString(value);
				}
			}
			public System.String SRClinicalAuthorityLevel
			{
				get
				{
					System.String data = entity.SRClinicalAuthorityLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRClinicalAuthorityLevel = null;
					else entity.SRClinicalAuthorityLevel = Convert.ToString(value);
				}
			}
			public System.String ManagerID
			{
				get
				{
					System.Int32? data = entity.ManagerID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ManagerID = null;
					else entity.ManagerID = Convert.ToInt32(value);
				}
			}
			private esEmployeeWorkingInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeWorkingInfoQuery query)
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
				throw new Exception("esEmployeeWorkingInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeWorkingInfo : esEmployeeWorkingInfo
	{
	}

	[Serializable]
	abstract public class esEmployeeWorkingInfoQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeWorkingInfoMetadata.Meta();
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem JoinDate
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.JoinDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SupervisorId
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SupervisorId, esSystemType.Int32);
			}
		}

		public esQueryItem SREmployeeStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeStatus, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeType
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeType, esSystemType.String);
			}
		}

		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem SRRemunerationType
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SRRemunerationType, esSystemType.String);
			}
		}

		public esQueryItem AbsenceCardNo
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.AbsenceCardNo, esSystemType.String);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsKWI
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.IsKWI, esSystemType.Boolean);
			}
		}

		public esQueryItem GradeYear
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.GradeYear, esSystemType.Decimal);
			}
		}

		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		}

		public esQueryItem ResignDate
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.ResignDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SRResignReason
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SRResignReason, esSystemType.String);
			}
		}

		public esQueryItem EmployeeRegistrationNo
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.EmployeeRegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem PreceptorId
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.PreceptorId, esSystemType.Int32);
			}
		}

		public esQueryItem SREmployeeShiftType
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeShiftType, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeScheduleType
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeScheduleType, esSystemType.String);
			}
		}

		public esQueryItem SRProfessionType
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SRProfessionType, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalWorkArea
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SRClinicalWorkArea, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalAuthorityLevel
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.SRClinicalAuthorityLevel, esSystemType.String);
			}
		}

		public esQueryItem ManagerID
		{
			get
			{
				return new esQueryItem(this, EmployeeWorkingInfoMetadata.ColumnNames.ManagerID, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeWorkingInfoCollection")]
	public partial class EmployeeWorkingInfoCollection : esEmployeeWorkingInfoCollection, IEnumerable<EmployeeWorkingInfo>
	{
		public EmployeeWorkingInfoCollection()
		{

		}

		public static implicit operator List<EmployeeWorkingInfo>(EmployeeWorkingInfoCollection coll)
		{
			List<EmployeeWorkingInfo> list = new List<EmployeeWorkingInfo>();

			foreach (EmployeeWorkingInfo emp in coll)
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
				return EmployeeWorkingInfoMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeWorkingInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeWorkingInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeWorkingInfo();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeWorkingInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeWorkingInfoQuery();
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
		public bool Load(EmployeeWorkingInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeWorkingInfo AddNew()
		{
			EmployeeWorkingInfo entity = base.AddNewEntity() as EmployeeWorkingInfo;

			return entity;
		}
		public EmployeeWorkingInfo FindByPrimaryKey(Int32 personID)
		{
			return base.FindByPrimaryKey(personID) as EmployeeWorkingInfo;
		}

		#region IEnumerable< EmployeeWorkingInfo> Members

		IEnumerator<EmployeeWorkingInfo> IEnumerable<EmployeeWorkingInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeWorkingInfo;
			}
		}

		#endregion

		private EmployeeWorkingInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeWorkingInfo' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeWorkingInfo ({PersonID})")]
	[Serializable]
	public partial class EmployeeWorkingInfo : esEmployeeWorkingInfo
	{
		public EmployeeWorkingInfo()
		{
		}

		public EmployeeWorkingInfo(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeWorkingInfoMetadata.Meta();
			}
		}

		override protected esEmployeeWorkingInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeWorkingInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeWorkingInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeWorkingInfoQuery();
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
		public bool Load(EmployeeWorkingInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeWorkingInfoQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeWorkingInfoQuery : esEmployeeWorkingInfoQuery
	{
		public EmployeeWorkingInfoQuery()
		{

		}

		public EmployeeWorkingInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeWorkingInfoQuery";
		}
	}

	[Serializable]
	public partial class EmployeeWorkingInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeWorkingInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.PersonID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.PersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.JoinDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.JoinDate;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SupervisorId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SupervisorId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SREmployeeStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SREmployeeType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.PositionGradeID, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SRRemunerationType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SRRemunerationType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.AbsenceCardNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.AbsenceCardNo;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.Note, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.IsKWI, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.IsKWI;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.GradeYear, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.GradeYear;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SREducationLevel, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.ResignDate, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.ResignDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SRResignReason, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SRResignReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.EmployeeRegistrationNo, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.EmployeeRegistrationNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.PreceptorId, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.PreceptorId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeShiftType, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SREmployeeShiftType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SREmployeeScheduleType, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SREmployeeScheduleType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SRProfessionType, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SRProfessionType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SRClinicalWorkArea, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SRClinicalWorkArea;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.SRClinicalAuthorityLevel, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.SRClinicalAuthorityLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeWorkingInfoMetadata.ColumnNames.ManagerID, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeWorkingInfoMetadata.PropertyNames.ManagerID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeWorkingInfoMetadata Meta()
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
			public const string PersonID = "PersonID";
			public const string JoinDate = "JoinDate";
			public const string SupervisorId = "SupervisorId";
			public const string SREmployeeStatus = "SREmployeeStatus";
			public const string SREmployeeType = "SREmployeeType";
			public const string PositionGradeID = "PositionGradeID";
			public const string SRRemunerationType = "SRRemunerationType";
			public const string AbsenceCardNo = "AbsenceCardNo";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsKWI = "IsKWI";
			public const string GradeYear = "GradeYear";
			public const string SREducationLevel = "SREducationLevel";
			public const string ResignDate = "ResignDate";
			public const string SRResignReason = "SRResignReason";
			public const string EmployeeRegistrationNo = "EmployeeRegistrationNo";
			public const string PreceptorId = "PreceptorId";
			public const string SREmployeeShiftType = "SREmployeeShiftType";
			public const string SREmployeeScheduleType = "SREmployeeScheduleType";
			public const string SRProfessionType = "SRProfessionType";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string ManagerID = "ManagerID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonID = "PersonID";
			public const string JoinDate = "JoinDate";
			public const string SupervisorId = "SupervisorId";
			public const string SREmployeeStatus = "SREmployeeStatus";
			public const string SREmployeeType = "SREmployeeType";
			public const string PositionGradeID = "PositionGradeID";
			public const string SRRemunerationType = "SRRemunerationType";
			public const string AbsenceCardNo = "AbsenceCardNo";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsKWI = "IsKWI";
			public const string GradeYear = "GradeYear";
			public const string SREducationLevel = "SREducationLevel";
			public const string ResignDate = "ResignDate";
			public const string SRResignReason = "SRResignReason";
			public const string EmployeeRegistrationNo = "EmployeeRegistrationNo";
			public const string PreceptorId = "PreceptorId";
			public const string SREmployeeShiftType = "SREmployeeShiftType";
			public const string SREmployeeScheduleType = "SREmployeeScheduleType";
			public const string SRProfessionType = "SRProfessionType";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string ManagerID = "ManagerID";
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
			lock (typeof(EmployeeWorkingInfoMetadata))
			{
				if (EmployeeWorkingInfoMetadata.mapDelegates == null)
				{
					EmployeeWorkingInfoMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeWorkingInfoMetadata.meta == null)
				{
					EmployeeWorkingInfoMetadata.meta = new EmployeeWorkingInfoMetadata();
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

				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JoinDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SupervisorId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmployeeStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRemunerationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AbsenceCardNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsKWI", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("GradeYear", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResignDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRResignReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PreceptorId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmployeeShiftType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeScheduleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProfessionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalWorkArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalAuthorityLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ManagerID", new esTypeMap("int", "System.Int32"));


				meta.Source = "EmployeeWorkingInfo";
				meta.Destination = "EmployeeWorkingInfo";
				meta.spInsert = "proc_EmployeeWorkingInfoInsert";
				meta.spUpdate = "proc_EmployeeWorkingInfoUpdate";
				meta.spDelete = "proc_EmployeeWorkingInfoDelete";
				meta.spLoadAll = "proc_EmployeeWorkingInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeWorkingInfoLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeWorkingInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
