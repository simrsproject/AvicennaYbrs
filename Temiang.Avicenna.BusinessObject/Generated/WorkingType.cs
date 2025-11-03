/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/15/2011 10:42:34 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esWorkingTypeCollection : esEntityCollectionWAuditLog
	{
		public esWorkingTypeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WorkingTypeCollection";
		}

		#region Query Logic
		protected void InitQuery(esWorkingTypeQuery query)
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
			this.InitQuery(query as esWorkingTypeQuery);
		}
		#endregion
		
		virtual public WorkingType DetachEntity(WorkingType entity)
		{
			return base.DetachEntity(entity) as WorkingType;
		}
		
		virtual public WorkingType AttachEntity(WorkingType entity)
		{
			return base.AttachEntity(entity) as WorkingType;
		}
		
		virtual public void Combine(WorkingTypeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WorkingType this[int index]
		{
			get
			{
				return base[index] as WorkingType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WorkingType);
		}
	}



	[Serializable]
	abstract public class esWorkingType : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWorkingTypeQuery GetDynamicQuery()
		{
			return null;
		}

		public esWorkingType()
		{

		}

		public esWorkingType(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 workingTypeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingTypeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 workingTypeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(workingTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(workingTypeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 workingTypeID)
		{
			esWorkingTypeQuery query = this.GetDynamicQuery();
			query.Where(query.WorkingTypeID == workingTypeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 workingTypeID)
		{
			esParameters parms = new esParameters();
			parms.Add("WorkingTypeID",workingTypeID);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{							
						case "WorkingTypeID": this.str.WorkingTypeID = (string)value; break;							
						case "WorkingTypeCode": this.str.WorkingTypeCode = (string)value; break;							
						case "WorkingTypeName": this.str.WorkingTypeName = (string)value; break;							
						case "WorkingTypeDescription": this.str.WorkingTypeDescription = (string)value; break;							
						case "SRShiftCompensation": this.str.SRShiftCompensation = (string)value; break;							
						case "ChangeTheDate": this.str.ChangeTheDate = (string)value; break;							
						case "EarlyAttendanceTime": this.str.EarlyAttendanceTime = (string)value; break;							
						case "AttendanceTime": this.str.AttendanceTime = (string)value; break;							
						case "AttendanceTimeRevision": this.str.AttendanceTimeRevision = (string)value; break;							
						case "LeaveTime": this.str.LeaveTime = (string)value; break;							
						case "LeaveTimeRevision": this.str.LeaveTimeRevision = (string)value; break;							
						case "Overtime1Start": this.str.Overtime1Start = (string)value; break;							
						case "Overtime1Finish": this.str.Overtime1Finish = (string)value; break;							
						case "Overtime2Start": this.str.Overtime2Start = (string)value; break;							
						case "Overtime2Finish": this.str.Overtime2Finish = (string)value; break;							
						case "Overtime3Start": this.str.Overtime3Start = (string)value; break;							
						case "Overtime3Finish": this.str.Overtime3Finish = (string)value; break;							
						case "Overtime4Start": this.str.Overtime4Start = (string)value; break;							
						case "Overtime4Finish": this.str.Overtime4Finish = (string)value; break;							
						case "Overtime5Start": this.str.Overtime5Start = (string)value; break;							
						case "Overtime5Finish": this.str.Overtime5Finish = (string)value; break;							
						case "Break1Start": this.str.Break1Start = (string)value; break;							
						case "Break1Finish": this.str.Break1Finish = (string)value; break;							
						case "Break2Start": this.str.Break2Start = (string)value; break;							
						case "Break2Finish": this.str.Break2Finish = (string)value; break;							
						case "Break3Start": this.str.Break3Start = (string)value; break;							
						case "Break3Finish": this.str.Break3Finish = (string)value; break;							
						case "Break4Start": this.str.Break4Start = (string)value; break;							
						case "Break4Finish": this.str.Break4Finish = (string)value; break;							
						case "Break5Start": this.str.Break5Start = (string)value; break;							
						case "Break5Finish": this.str.Break5Finish = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "WorkingTypeID":
						
							if (value == null || value is System.Int32)
								this.WorkingTypeID = (System.Int32?)value;
							break;
						
						case "ChangeTheDate":
						
							if (value == null || value is System.DateTime)
								this.ChangeTheDate = (System.DateTime?)value;
							break;
						
						case "EarlyAttendanceTime":
						
							if (value == null || value is System.DateTime)
								this.EarlyAttendanceTime = (System.DateTime?)value;
							break;
						
						case "AttendanceTime":
						
							if (value == null || value is System.DateTime)
								this.AttendanceTime = (System.DateTime?)value;
							break;
						
						case "AttendanceTimeRevision":
						
							if (value == null || value is System.DateTime)
								this.AttendanceTimeRevision = (System.DateTime?)value;
							break;
						
						case "LeaveTime":
						
							if (value == null || value is System.DateTime)
								this.LeaveTime = (System.DateTime?)value;
							break;
						
						case "LeaveTimeRevision":
						
							if (value == null || value is System.DateTime)
								this.LeaveTimeRevision = (System.DateTime?)value;
							break;
						
						case "Overtime1Start":
						
							if (value == null || value is System.DateTime)
								this.Overtime1Start = (System.DateTime?)value;
							break;
						
						case "Overtime1Finish":
						
							if (value == null || value is System.DateTime)
								this.Overtime1Finish = (System.DateTime?)value;
							break;
						
						case "Overtime2Start":
						
							if (value == null || value is System.DateTime)
								this.Overtime2Start = (System.DateTime?)value;
							break;
						
						case "Overtime2Finish":
						
							if (value == null || value is System.DateTime)
								this.Overtime2Finish = (System.DateTime?)value;
							break;
						
						case "Overtime3Start":
						
							if (value == null || value is System.DateTime)
								this.Overtime3Start = (System.DateTime?)value;
							break;
						
						case "Overtime3Finish":
						
							if (value == null || value is System.DateTime)
								this.Overtime3Finish = (System.DateTime?)value;
							break;
						
						case "Overtime4Start":
						
							if (value == null || value is System.DateTime)
								this.Overtime4Start = (System.DateTime?)value;
							break;
						
						case "Overtime4Finish":
						
							if (value == null || value is System.DateTime)
								this.Overtime4Finish = (System.DateTime?)value;
							break;
						
						case "Overtime5Start":
						
							if (value == null || value is System.DateTime)
								this.Overtime5Start = (System.DateTime?)value;
							break;
						
						case "Overtime5Finish":
						
							if (value == null || value is System.DateTime)
								this.Overtime5Finish = (System.DateTime?)value;
							break;
						
						case "Break1Start":
						
							if (value == null || value is System.DateTime)
								this.Break1Start = (System.DateTime?)value;
							break;
						
						case "Break1Finish":
						
							if (value == null || value is System.DateTime)
								this.Break1Finish = (System.DateTime?)value;
							break;
						
						case "Break2Start":
						
							if (value == null || value is System.DateTime)
								this.Break2Start = (System.DateTime?)value;
							break;
						
						case "Break2Finish":
						
							if (value == null || value is System.DateTime)
								this.Break2Finish = (System.DateTime?)value;
							break;
						
						case "Break3Start":
						
							if (value == null || value is System.DateTime)
								this.Break3Start = (System.DateTime?)value;
							break;
						
						case "Break3Finish":
						
							if (value == null || value is System.DateTime)
								this.Break3Finish = (System.DateTime?)value;
							break;
						
						case "Break4Start":
						
							if (value == null || value is System.DateTime)
								this.Break4Start = (System.DateTime?)value;
							break;
						
						case "Break4Finish":
						
							if (value == null || value is System.DateTime)
								this.Break4Finish = (System.DateTime?)value;
							break;
						
						case "Break5Start":
						
							if (value == null || value is System.DateTime)
								this.Break5Start = (System.DateTime?)value;
							break;
						
						case "Break5Finish":
						
							if (value == null || value is System.DateTime)
								this.Break5Finish = (System.DateTime?)value;
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
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}
		
		
		/// <summary>
		/// Maps to WorkingType.WorkingTypeID
		/// </summary>
		virtual public System.Int32? WorkingTypeID
		{
			get
			{
				return base.GetSystemInt32(WorkingTypeMetadata.ColumnNames.WorkingTypeID);
			}
			
			set
			{
				base.SetSystemInt32(WorkingTypeMetadata.ColumnNames.WorkingTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.WorkingTypeCode
		/// </summary>
		virtual public System.String WorkingTypeCode
		{
			get
			{
				return base.GetSystemString(WorkingTypeMetadata.ColumnNames.WorkingTypeCode);
			}
			
			set
			{
				base.SetSystemString(WorkingTypeMetadata.ColumnNames.WorkingTypeCode, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.WorkingTypeName
		/// </summary>
		virtual public System.String WorkingTypeName
		{
			get
			{
				return base.GetSystemString(WorkingTypeMetadata.ColumnNames.WorkingTypeName);
			}
			
			set
			{
				base.SetSystemString(WorkingTypeMetadata.ColumnNames.WorkingTypeName, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.WorkingTypeDescription
		/// </summary>
		virtual public System.String WorkingTypeDescription
		{
			get
			{
				return base.GetSystemString(WorkingTypeMetadata.ColumnNames.WorkingTypeDescription);
			}
			
			set
			{
				base.SetSystemString(WorkingTypeMetadata.ColumnNames.WorkingTypeDescription, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.SRShiftCompensation
		/// </summary>
		virtual public System.String SRShiftCompensation
		{
			get
			{
				return base.GetSystemString(WorkingTypeMetadata.ColumnNames.SRShiftCompensation);
			}
			
			set
			{
				base.SetSystemString(WorkingTypeMetadata.ColumnNames.SRShiftCompensation, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.ChangeTheDate
		/// </summary>
		virtual public System.DateTime? ChangeTheDate
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.ChangeTheDate);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.ChangeTheDate, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.EarlyAttendanceTime
		/// </summary>
		virtual public System.DateTime? EarlyAttendanceTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.EarlyAttendanceTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.EarlyAttendanceTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.AttendanceTime
		/// </summary>
		virtual public System.DateTime? AttendanceTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.AttendanceTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.AttendanceTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.AttendanceTimeRevision
		/// </summary>
		virtual public System.DateTime? AttendanceTimeRevision
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.AttendanceTimeRevision);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.AttendanceTimeRevision, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.LeaveTime
		/// </summary>
		virtual public System.DateTime? LeaveTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.LeaveTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.LeaveTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.LeaveTimeRevision
		/// </summary>
		virtual public System.DateTime? LeaveTimeRevision
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.LeaveTimeRevision);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.LeaveTimeRevision, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime1Start
		/// </summary>
		virtual public System.DateTime? Overtime1Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime1Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime1Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime1Finish
		/// </summary>
		virtual public System.DateTime? Overtime1Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime1Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime1Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime2Start
		/// </summary>
		virtual public System.DateTime? Overtime2Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime2Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime2Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime2Finish
		/// </summary>
		virtual public System.DateTime? Overtime2Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime2Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime2Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime3Start
		/// </summary>
		virtual public System.DateTime? Overtime3Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime3Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime3Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime3Finish
		/// </summary>
		virtual public System.DateTime? Overtime3Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime3Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime3Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime4Start
		/// </summary>
		virtual public System.DateTime? Overtime4Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime4Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime4Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime4Finish
		/// </summary>
		virtual public System.DateTime? Overtime4Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime4Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime4Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime5Start
		/// </summary>
		virtual public System.DateTime? Overtime5Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime5Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime5Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Overtime5Finish
		/// </summary>
		virtual public System.DateTime? Overtime5Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime5Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Overtime5Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break1Start
		/// </summary>
		virtual public System.DateTime? Break1Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break1Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break1Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break1Finish
		/// </summary>
		virtual public System.DateTime? Break1Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break1Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break1Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break2Start
		/// </summary>
		virtual public System.DateTime? Break2Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break2Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break2Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break2Finish
		/// </summary>
		virtual public System.DateTime? Break2Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break2Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break2Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break3Start
		/// </summary>
		virtual public System.DateTime? Break3Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break3Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break3Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break3Finish
		/// </summary>
		virtual public System.DateTime? Break3Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break3Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break3Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break4Start
		/// </summary>
		virtual public System.DateTime? Break4Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break4Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break4Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break4Finish
		/// </summary>
		virtual public System.DateTime? Break4Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break4Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break4Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break5Start
		/// </summary>
		virtual public System.DateTime? Break5Start
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break5Start);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break5Start, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.Break5Finish
		/// </summary>
		virtual public System.DateTime? Break5Finish
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break5Finish);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.Break5Finish, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WorkingTypeMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(WorkingTypeMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WorkingType.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(WorkingTypeMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(WorkingTypeMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


		[BrowsableAttribute( false )]
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
			public esStrings(esWorkingType entity)
			{
				this.entity = entity;
			}
			
	
			public System.String WorkingTypeID
			{
				get
				{
					System.Int32? data = entity.WorkingTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingTypeID = null;
					else entity.WorkingTypeID = Convert.ToInt32(value);
				}
			}
				
			public System.String WorkingTypeCode
			{
				get
				{
					System.String data = entity.WorkingTypeCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingTypeCode = null;
					else entity.WorkingTypeCode = Convert.ToString(value);
				}
			}
				
			public System.String WorkingTypeName
			{
				get
				{
					System.String data = entity.WorkingTypeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingTypeName = null;
					else entity.WorkingTypeName = Convert.ToString(value);
				}
			}
				
			public System.String WorkingTypeDescription
			{
				get
				{
					System.String data = entity.WorkingTypeDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WorkingTypeDescription = null;
					else entity.WorkingTypeDescription = Convert.ToString(value);
				}
			}
				
			public System.String SRShiftCompensation
			{
				get
				{
					System.String data = entity.SRShiftCompensation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRShiftCompensation = null;
					else entity.SRShiftCompensation = Convert.ToString(value);
				}
			}
				
			public System.String ChangeTheDate
			{
				get
				{
					System.DateTime? data = entity.ChangeTheDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChangeTheDate = null;
					else entity.ChangeTheDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EarlyAttendanceTime
			{
				get
				{
					System.DateTime? data = entity.EarlyAttendanceTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EarlyAttendanceTime = null;
					else entity.EarlyAttendanceTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String AttendanceTime
			{
				get
				{
					System.DateTime? data = entity.AttendanceTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AttendanceTime = null;
					else entity.AttendanceTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String AttendanceTimeRevision
			{
				get
				{
					System.DateTime? data = entity.AttendanceTimeRevision;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AttendanceTimeRevision = null;
					else entity.AttendanceTimeRevision = Convert.ToDateTime(value);
				}
			}
				
			public System.String LeaveTime
			{
				get
				{
					System.DateTime? data = entity.LeaveTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveTime = null;
					else entity.LeaveTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LeaveTimeRevision
			{
				get
				{
					System.DateTime? data = entity.LeaveTimeRevision;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveTimeRevision = null;
					else entity.LeaveTimeRevision = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime1Start
			{
				get
				{
					System.DateTime? data = entity.Overtime1Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime1Start = null;
					else entity.Overtime1Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime1Finish
			{
				get
				{
					System.DateTime? data = entity.Overtime1Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime1Finish = null;
					else entity.Overtime1Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime2Start
			{
				get
				{
					System.DateTime? data = entity.Overtime2Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime2Start = null;
					else entity.Overtime2Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime2Finish
			{
				get
				{
					System.DateTime? data = entity.Overtime2Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime2Finish = null;
					else entity.Overtime2Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime3Start
			{
				get
				{
					System.DateTime? data = entity.Overtime3Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime3Start = null;
					else entity.Overtime3Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime3Finish
			{
				get
				{
					System.DateTime? data = entity.Overtime3Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime3Finish = null;
					else entity.Overtime3Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime4Start
			{
				get
				{
					System.DateTime? data = entity.Overtime4Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime4Start = null;
					else entity.Overtime4Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime4Finish
			{
				get
				{
					System.DateTime? data = entity.Overtime4Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime4Finish = null;
					else entity.Overtime4Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime5Start
			{
				get
				{
					System.DateTime? data = entity.Overtime5Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime5Start = null;
					else entity.Overtime5Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Overtime5Finish
			{
				get
				{
					System.DateTime? data = entity.Overtime5Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Overtime5Finish = null;
					else entity.Overtime5Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break1Start
			{
				get
				{
					System.DateTime? data = entity.Break1Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break1Start = null;
					else entity.Break1Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break1Finish
			{
				get
				{
					System.DateTime? data = entity.Break1Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break1Finish = null;
					else entity.Break1Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break2Start
			{
				get
				{
					System.DateTime? data = entity.Break2Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break2Start = null;
					else entity.Break2Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break2Finish
			{
				get
				{
					System.DateTime? data = entity.Break2Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break2Finish = null;
					else entity.Break2Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break3Start
			{
				get
				{
					System.DateTime? data = entity.Break3Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break3Start = null;
					else entity.Break3Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break3Finish
			{
				get
				{
					System.DateTime? data = entity.Break3Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break3Finish = null;
					else entity.Break3Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break4Start
			{
				get
				{
					System.DateTime? data = entity.Break4Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break4Start = null;
					else entity.Break4Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break4Finish
			{
				get
				{
					System.DateTime? data = entity.Break4Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break4Finish = null;
					else entity.Break4Finish = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break5Start
			{
				get
				{
					System.DateTime? data = entity.Break5Start;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break5Start = null;
					else entity.Break5Start = Convert.ToDateTime(value);
				}
			}
				
			public System.String Break5Finish
			{
				get
				{
					System.DateTime? data = entity.Break5Finish;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Break5Finish = null;
					else entity.Break5Finish = Convert.ToDateTime(value);
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
			

			private esWorkingType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWorkingTypeQuery query)
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
				throw new Exception("esWorkingType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class WorkingType : esWorkingType
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esWorkingTypeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WorkingTypeMetadata.Meta();
			}
		}	
		

		public esQueryItem WorkingTypeID
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.WorkingTypeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem WorkingTypeCode
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.WorkingTypeCode, esSystemType.String);
			}
		} 
		
		public esQueryItem WorkingTypeName
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.WorkingTypeName, esSystemType.String);
			}
		} 
		
		public esQueryItem WorkingTypeDescription
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.WorkingTypeDescription, esSystemType.String);
			}
		} 
		
		public esQueryItem SRShiftCompensation
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.SRShiftCompensation, esSystemType.String);
			}
		} 
		
		public esQueryItem ChangeTheDate
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.ChangeTheDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EarlyAttendanceTime
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.EarlyAttendanceTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem AttendanceTime
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.AttendanceTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem AttendanceTimeRevision
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.AttendanceTimeRevision, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LeaveTime
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.LeaveTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LeaveTimeRevision
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.LeaveTimeRevision, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime1Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime1Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime1Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime1Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime2Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime2Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime2Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime2Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime3Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime3Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime3Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime3Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime4Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime4Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime4Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime4Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime5Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime5Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Overtime5Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Overtime5Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break1Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break1Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break1Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break1Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break2Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break2Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break2Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break2Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break3Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break3Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break3Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break3Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break4Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break4Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break4Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break4Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break5Start
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break5Start, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Break5Finish
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.Break5Finish, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, WorkingTypeMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WorkingTypeCollection")]
	public partial class WorkingTypeCollection : esWorkingTypeCollection, IEnumerable<WorkingType>
	{
		public WorkingTypeCollection()
		{

		}
		
		public static implicit operator List<WorkingType>(WorkingTypeCollection coll)
		{
			List<WorkingType> list = new List<WorkingType>();
			
			foreach (WorkingType emp in coll)
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
				return  WorkingTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WorkingType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WorkingType();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WorkingTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WorkingTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WorkingType AddNew()
		{
			WorkingType entity = base.AddNewEntity() as WorkingType;
			
			return entity;
		}

		public WorkingType FindByPrimaryKey(System.Int32 workingTypeID)
		{
			return base.FindByPrimaryKey(workingTypeID) as WorkingType;
		}


		#region IEnumerable<WorkingType> Members

		IEnumerator<WorkingType> IEnumerable<WorkingType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WorkingType;
			}
		}

		#endregion
		
		private WorkingTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WorkingType' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WorkingType ({WorkingTypeID})")]
	[Serializable]
	public partial class WorkingType : esWorkingType
	{
		public WorkingType()
		{

		}
	
		public WorkingType(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WorkingTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esWorkingTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WorkingTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WorkingTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WorkingTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WorkingTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WorkingTypeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WorkingTypeQuery : esWorkingTypeQuery
	{
		public WorkingTypeQuery()
		{

		}		
		
		public WorkingTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WorkingTypeQuery";
        }
		
			
	}


	[Serializable]
	public partial class WorkingTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WorkingTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.WorkingTypeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.WorkingTypeID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.WorkingTypeCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.WorkingTypeCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.WorkingTypeName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.WorkingTypeName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.WorkingTypeDescription, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.WorkingTypeDescription;
			c.CharacterMaxLength = 4000;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.SRShiftCompensation, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.SRShiftCompensation;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.ChangeTheDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.ChangeTheDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.EarlyAttendanceTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.EarlyAttendanceTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.AttendanceTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.AttendanceTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.AttendanceTimeRevision, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.AttendanceTimeRevision;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.LeaveTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.LeaveTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.LeaveTimeRevision, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.LeaveTimeRevision;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime1Start, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime1Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime1Finish, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime1Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime2Start, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime2Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime2Finish, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime2Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime3Start, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime3Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime3Finish, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime3Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime4Start, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime4Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime4Finish, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime4Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime5Start, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime5Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Overtime5Finish, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Overtime5Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break1Start, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break1Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break1Finish, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break1Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break2Start, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break2Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break2Finish, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break2Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break3Start, 25, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break3Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break3Finish, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break3Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break4Start, 27, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break4Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break4Finish, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break4Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break5Start, 29, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break5Start;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.Break5Finish, 30, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.Break5Finish;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.LastUpdateDateTime, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(WorkingTypeMetadata.ColumnNames.LastUpdateByUserID, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = WorkingTypeMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WorkingTypeMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string WorkingTypeID = "WorkingTypeID";
			 public const string WorkingTypeCode = "WorkingTypeCode";
			 public const string WorkingTypeName = "WorkingTypeName";
			 public const string WorkingTypeDescription = "WorkingTypeDescription";
			 public const string SRShiftCompensation = "SRShiftCompensation";
			 public const string ChangeTheDate = "ChangeTheDate";
			 public const string EarlyAttendanceTime = "EarlyAttendanceTime";
			 public const string AttendanceTime = "AttendanceTime";
			 public const string AttendanceTimeRevision = "AttendanceTimeRevision";
			 public const string LeaveTime = "LeaveTime";
			 public const string LeaveTimeRevision = "LeaveTimeRevision";
			 public const string Overtime1Start = "Overtime1Start";
			 public const string Overtime1Finish = "Overtime1Finish";
			 public const string Overtime2Start = "Overtime2Start";
			 public const string Overtime2Finish = "Overtime2Finish";
			 public const string Overtime3Start = "Overtime3Start";
			 public const string Overtime3Finish = "Overtime3Finish";
			 public const string Overtime4Start = "Overtime4Start";
			 public const string Overtime4Finish = "Overtime4Finish";
			 public const string Overtime5Start = "Overtime5Start";
			 public const string Overtime5Finish = "Overtime5Finish";
			 public const string Break1Start = "Break1Start";
			 public const string Break1Finish = "Break1Finish";
			 public const string Break2Start = "Break2Start";
			 public const string Break2Finish = "Break2Finish";
			 public const string Break3Start = "Break3Start";
			 public const string Break3Finish = "Break3Finish";
			 public const string Break4Start = "Break4Start";
			 public const string Break4Finish = "Break4Finish";
			 public const string Break5Start = "Break5Start";
			 public const string Break5Finish = "Break5Finish";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WorkingTypeID = "WorkingTypeID";
			 public const string WorkingTypeCode = "WorkingTypeCode";
			 public const string WorkingTypeName = "WorkingTypeName";
			 public const string WorkingTypeDescription = "WorkingTypeDescription";
			 public const string SRShiftCompensation = "SRShiftCompensation";
			 public const string ChangeTheDate = "ChangeTheDate";
			 public const string EarlyAttendanceTime = "EarlyAttendanceTime";
			 public const string AttendanceTime = "AttendanceTime";
			 public const string AttendanceTimeRevision = "AttendanceTimeRevision";
			 public const string LeaveTime = "LeaveTime";
			 public const string LeaveTimeRevision = "LeaveTimeRevision";
			 public const string Overtime1Start = "Overtime1Start";
			 public const string Overtime1Finish = "Overtime1Finish";
			 public const string Overtime2Start = "Overtime2Start";
			 public const string Overtime2Finish = "Overtime2Finish";
			 public const string Overtime3Start = "Overtime3Start";
			 public const string Overtime3Finish = "Overtime3Finish";
			 public const string Overtime4Start = "Overtime4Start";
			 public const string Overtime4Finish = "Overtime4Finish";
			 public const string Overtime5Start = "Overtime5Start";
			 public const string Overtime5Finish = "Overtime5Finish";
			 public const string Break1Start = "Break1Start";
			 public const string Break1Finish = "Break1Finish";
			 public const string Break2Start = "Break2Start";
			 public const string Break2Finish = "Break2Finish";
			 public const string Break3Start = "Break3Start";
			 public const string Break3Finish = "Break3Finish";
			 public const string Break4Start = "Break4Start";
			 public const string Break4Finish = "Break4Finish";
			 public const string Break5Start = "Break5Start";
			 public const string Break5Finish = "Break5Finish";
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
			lock (typeof(WorkingTypeMetadata))
			{
				if(WorkingTypeMetadata.mapDelegates == null)
				{
					WorkingTypeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WorkingTypeMetadata.meta == null)
				{
					WorkingTypeMetadata.meta = new WorkingTypeMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				

				meta.AddTypeMap("WorkingTypeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("WorkingTypeCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WorkingTypeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("WorkingTypeDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRShiftCompensation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChangeTheDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EarlyAttendanceTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AttendanceTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AttendanceTimeRevision", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LeaveTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LeaveTimeRevision", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime1Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime1Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime2Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime2Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime3Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime3Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime4Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime4Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime5Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Overtime5Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break1Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break1Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break2Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break2Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break3Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break3Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break4Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break4Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break5Start", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Break5Finish", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "WorkingType";
				meta.Destination = "WorkingType";
				
				meta.spInsert = "proc_WorkingTypeInsert";				
				meta.spUpdate = "proc_WorkingTypeUpdate";		
				meta.spDelete = "proc_WorkingTypeDelete";
				meta.spLoadAll = "proc_WorkingTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_WorkingTypeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WorkingTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
