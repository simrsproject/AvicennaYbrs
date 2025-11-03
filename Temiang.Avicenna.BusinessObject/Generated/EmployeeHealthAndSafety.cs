/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 9/21/2014 9:19:24 PM
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
	abstract public class esEmployeeHealthAndSafetyCollection : esEntityCollection
	{
		public esEmployeeHealthAndSafetyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeHealthAndSafetyCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeHealthAndSafetyQuery query)
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
			this.InitQuery(query as esEmployeeHealthAndSafetyQuery);
		}
		#endregion
		
		virtual public EmployeeHealthAndSafety DetachEntity(EmployeeHealthAndSafety entity)
		{
			return base.DetachEntity(entity) as EmployeeHealthAndSafety;
		}
		
		virtual public EmployeeHealthAndSafety AttachEntity(EmployeeHealthAndSafety entity)
		{
			return base.AttachEntity(entity) as EmployeeHealthAndSafety;
		}
		
		virtual public void Combine(EmployeeHealthAndSafetyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeHealthAndSafety this[int index]
		{
			get
			{
				return base[index] as EmployeeHealthAndSafety;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeHealthAndSafety);
		}
	}



	[Serializable]
	abstract public class esEmployeeHealthAndSafety : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeHealthAndSafetyQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeHealthAndSafety()
		{

		}

		public esEmployeeHealthAndSafety(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String employeeHealthAndSafetyNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeHealthAndSafetyNo);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeHealthAndSafetyNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String employeeHealthAndSafetyNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeHealthAndSafetyNo);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeHealthAndSafetyNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String employeeHealthAndSafetyNo)
		{
			esEmployeeHealthAndSafetyQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeHealthAndSafetyNo == employeeHealthAndSafetyNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String employeeHealthAndSafetyNo)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeHealthAndSafetyNo",employeeHealthAndSafetyNo);
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
						case "EmployeeHealthAndSafetyNo": this.str.EmployeeHealthAndSafetyNo = (string)value; break;							
						case "SREmployerStatus": this.str.SREmployerStatus = (string)value; break;							
						case "EmployeeID": this.str.EmployeeID = (string)value; break;							
						case "EmployeeName": this.str.EmployeeName = (string)value; break;							
						case "Birthdate": this.str.Birthdate = (string)value; break;							
						case "BirthPlace": this.str.BirthPlace = (string)value; break;							
						case "SRGenderType": this.str.SRGenderType = (string)value; break;							
						case "Address": this.str.Address = (string)value; break;							
						case "Telp": this.str.Telp = (string)value; break;							
						case "DepartmentID": this.str.DepartmentID = (string)value; break;							
						case "JobTitle": this.str.JobTitle = (string)value; break;							
						case "IncidentPlace": this.str.IncidentPlace = (string)value; break;							
						case "IncidentDateTime": this.str.IncidentDateTime = (string)value; break;							
						case "ArriveInHospitalDateTime": this.str.ArriveInHospitalDateTime = (string)value; break;							
						case "LeaveHospitalDateTime": this.str.LeaveHospitalDateTime = (string)value; break;							
						case "Chronology": this.str.Chronology = (string)value; break;							
						case "ChronologyEnvironmentInvolved": this.str.ChronologyEnvironmentInvolved = (string)value; break;							
						case "SRIncidentImpact": this.str.SRIncidentImpact = (string)value; break;							
						case "InjuryDetail": this.str.InjuryDetail = (string)value; break;							
						case "EnvironmentCost": this.str.EnvironmentCost = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "ConditionAfterFirstAid": this.str.ConditionAfterFirstAid = (string)value; break;							
						case "SREmployeeOutpatientTreatment": this.str.SREmployeeOutpatientTreatment = (string)value; break;							
						case "SREmployeeTreatment": this.str.SREmployeeTreatment = (string)value; break;							
						case "AdditionalInformation": this.str.AdditionalInformation = (string)value; break;							
						case "InsertByUserID": this.str.InsertByUserID = (string)value; break;							
						case "InsertDateTime": this.str.InsertDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Birthdate":
						
							if (value == null || value is System.DateTime)
								this.Birthdate = (System.DateTime?)value;
							break;
						
						case "IncidentDateTime":
						
							if (value == null || value is System.DateTime)
								this.IncidentDateTime = (System.DateTime?)value;
							break;
						
						case "ArriveInHospitalDateTime":
						
							if (value == null || value is System.DateTime)
								this.ArriveInHospitalDateTime = (System.DateTime?)value;
							break;
						
						case "LeaveHospitalDateTime":
						
							if (value == null || value is System.DateTime)
								this.LeaveHospitalDateTime = (System.DateTime?)value;
							break;
						
						case "InsertDateTime":
						
							if (value == null || value is System.DateTime)
								this.InsertDateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeHealthAndSafety.EmployeeHealthAndSafetyNo
		/// </summary>
		virtual public System.String EmployeeHealthAndSafetyNo
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeHealthAndSafetyNo);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeHealthAndSafetyNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.SREmployerStatus
		/// </summary>
		virtual public System.String SREmployerStatus
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployerStatus);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployerStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.EmployeeID
		/// </summary>
		virtual public System.String EmployeeID
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeID);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.EmployeeName
		/// </summary>
		virtual public System.String EmployeeName
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeName);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeName, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.Birthdate
		/// </summary>
		virtual public System.DateTime? Birthdate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.Birthdate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.Birthdate, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.BirthPlace
		/// </summary>
		virtual public System.String BirthPlace
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.BirthPlace);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.BirthPlace, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SRGenderType);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SRGenderType, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.Address);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.Address, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.Telp
		/// </summary>
		virtual public System.String Telp
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.Telp);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.Telp, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.DepartmentID);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.DepartmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.JobTitle
		/// </summary>
		virtual public System.String JobTitle
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.JobTitle);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.JobTitle, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.IncidentPlace
		/// </summary>
		virtual public System.String IncidentPlace
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.IncidentPlace);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.IncidentPlace, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.IncidentDateTime
		/// </summary>
		virtual public System.DateTime? IncidentDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.IncidentDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.IncidentDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.ArriveInHospitalDateTime
		/// </summary>
		virtual public System.DateTime? ArriveInHospitalDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.ArriveInHospitalDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.ArriveInHospitalDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.LeaveHospitalDateTime
		/// </summary>
		virtual public System.DateTime? LeaveHospitalDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.LeaveHospitalDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.LeaveHospitalDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.Chronology
		/// </summary>
		virtual public System.String Chronology
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.Chronology);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.Chronology, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.ChronologyEnvironmentInvolved
		/// </summary>
		virtual public System.String ChronologyEnvironmentInvolved
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.ChronologyEnvironmentInvolved);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.ChronologyEnvironmentInvolved, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.SRIncidentImpact
		/// </summary>
		virtual public System.String SRIncidentImpact
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SRIncidentImpact);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SRIncidentImpact, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.InjuryDetail
		/// </summary>
		virtual public System.String InjuryDetail
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.InjuryDetail);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.InjuryDetail, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.EnvironmentCost
		/// </summary>
		virtual public System.String EnvironmentCost
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.EnvironmentCost);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.EnvironmentCost, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.ConditionAfterFirstAid
		/// </summary>
		virtual public System.String ConditionAfterFirstAid
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.ConditionAfterFirstAid);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.ConditionAfterFirstAid, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.SREmployeeOutpatientTreatment
		/// </summary>
		virtual public System.String SREmployeeOutpatientTreatment
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployeeOutpatientTreatment);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployeeOutpatientTreatment, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.SREmployeeTreatment
		/// </summary>
		virtual public System.String SREmployeeTreatment
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployeeTreatment);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployeeTreatment, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.AdditionalInformation
		/// </summary>
		virtual public System.String AdditionalInformation
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.AdditionalInformation);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.AdditionalInformation, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.InsertByUserID
		/// </summary>
		virtual public System.String InsertByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.InsertByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.InsertByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.InsertDateTime
		/// </summary>
		virtual public System.DateTime? InsertDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.InsertDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.InsertDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeHealthAndSafetyMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeHealthAndSafety.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeHealthAndSafetyMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esEmployeeHealthAndSafety entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeHealthAndSafetyNo
			{
				get
				{
					System.String data = entity.EmployeeHealthAndSafetyNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeHealthAndSafetyNo = null;
					else entity.EmployeeHealthAndSafetyNo = Convert.ToString(value);
				}
			}
				
			public System.String SREmployerStatus
			{
				get
				{
					System.String data = entity.SREmployerStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployerStatus = null;
					else entity.SREmployerStatus = Convert.ToString(value);
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
				
			public System.String EmployeeName
			{
				get
				{
					System.String data = entity.EmployeeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeName = null;
					else entity.EmployeeName = Convert.ToString(value);
				}
			}
				
			public System.String Birthdate
			{
				get
				{
					System.DateTime? data = entity.Birthdate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Birthdate = null;
					else entity.Birthdate = Convert.ToDateTime(value);
				}
			}
				
			public System.String BirthPlace
			{
				get
				{
					System.String data = entity.BirthPlace;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthPlace = null;
					else entity.BirthPlace = Convert.ToString(value);
				}
			}
				
			public System.String SRGenderType
			{
				get
				{
					System.String data = entity.SRGenderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGenderType = null;
					else entity.SRGenderType = Convert.ToString(value);
				}
			}
				
			public System.String Address
			{
				get
				{
					System.String data = entity.Address;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address = null;
					else entity.Address = Convert.ToString(value);
				}
			}
				
			public System.String Telp
			{
				get
				{
					System.String data = entity.Telp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Telp = null;
					else entity.Telp = Convert.ToString(value);
				}
			}
				
			public System.String DepartmentID
			{
				get
				{
					System.String data = entity.DepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentID = null;
					else entity.DepartmentID = Convert.ToString(value);
				}
			}
				
			public System.String JobTitle
			{
				get
				{
					System.String data = entity.JobTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobTitle = null;
					else entity.JobTitle = Convert.ToString(value);
				}
			}
				
			public System.String IncidentPlace
			{
				get
				{
					System.String data = entity.IncidentPlace;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentPlace = null;
					else entity.IncidentPlace = Convert.ToString(value);
				}
			}
				
			public System.String IncidentDateTime
			{
				get
				{
					System.DateTime? data = entity.IncidentDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncidentDateTime = null;
					else entity.IncidentDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String ArriveInHospitalDateTime
			{
				get
				{
					System.DateTime? data = entity.ArriveInHospitalDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ArriveInHospitalDateTime = null;
					else entity.ArriveInHospitalDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LeaveHospitalDateTime
			{
				get
				{
					System.DateTime? data = entity.LeaveHospitalDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveHospitalDateTime = null;
					else entity.LeaveHospitalDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String Chronology
			{
				get
				{
					System.String data = entity.Chronology;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Chronology = null;
					else entity.Chronology = Convert.ToString(value);
				}
			}
				
			public System.String ChronologyEnvironmentInvolved
			{
				get
				{
					System.String data = entity.ChronologyEnvironmentInvolved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChronologyEnvironmentInvolved = null;
					else entity.ChronologyEnvironmentInvolved = Convert.ToString(value);
				}
			}
				
			public System.String SRIncidentImpact
			{
				get
				{
					System.String data = entity.SRIncidentImpact;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncidentImpact = null;
					else entity.SRIncidentImpact = Convert.ToString(value);
				}
			}
				
			public System.String InjuryDetail
			{
				get
				{
					System.String data = entity.InjuryDetail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InjuryDetail = null;
					else entity.InjuryDetail = Convert.ToString(value);
				}
			}
				
			public System.String EnvironmentCost
			{
				get
				{
					System.String data = entity.EnvironmentCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EnvironmentCost = null;
					else entity.EnvironmentCost = Convert.ToString(value);
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
				
			public System.String ConditionAfterFirstAid
			{
				get
				{
					System.String data = entity.ConditionAfterFirstAid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConditionAfterFirstAid = null;
					else entity.ConditionAfterFirstAid = Convert.ToString(value);
				}
			}
				
			public System.String SREmployeeOutpatientTreatment
			{
				get
				{
					System.String data = entity.SREmployeeOutpatientTreatment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeOutpatientTreatment = null;
					else entity.SREmployeeOutpatientTreatment = Convert.ToString(value);
				}
			}
				
			public System.String SREmployeeTreatment
			{
				get
				{
					System.String data = entity.SREmployeeTreatment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmployeeTreatment = null;
					else entity.SREmployeeTreatment = Convert.ToString(value);
				}
			}
				
			public System.String AdditionalInformation
			{
				get
				{
					System.String data = entity.AdditionalInformation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdditionalInformation = null;
					else entity.AdditionalInformation = Convert.ToString(value);
				}
			}
				
			public System.String InsertByUserID
			{
				get
				{
					System.String data = entity.InsertByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertByUserID = null;
					else entity.InsertByUserID = Convert.ToString(value);
				}
			}
				
			public System.String InsertDateTime
			{
				get
				{
					System.DateTime? data = entity.InsertDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InsertDateTime = null;
					else entity.InsertDateTime = Convert.ToDateTime(value);
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
			

			private esEmployeeHealthAndSafety entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeHealthAndSafetyQuery query)
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
				throw new Exception("esEmployeeHealthAndSafety can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esEmployeeHealthAndSafetyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeHealthAndSafetyMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeHealthAndSafetyNo
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeHealthAndSafetyNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmployerStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployerStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeID
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeID, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeName
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeName, esSystemType.String);
			}
		} 
		
		public esQueryItem Birthdate
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.Birthdate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem BirthPlace
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.BirthPlace, esSystemType.String);
			}
		} 
		
		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		} 
		
		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.Address, esSystemType.String);
			}
		} 
		
		public esQueryItem Telp
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.Telp, esSystemType.String);
			}
		} 
		
		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		} 
		
		public esQueryItem JobTitle
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.JobTitle, esSystemType.String);
			}
		} 
		
		public esQueryItem IncidentPlace
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.IncidentPlace, esSystemType.String);
			}
		} 
		
		public esQueryItem IncidentDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.IncidentDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ArriveInHospitalDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.ArriveInHospitalDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LeaveHospitalDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.LeaveHospitalDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Chronology
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.Chronology, esSystemType.String);
			}
		} 
		
		public esQueryItem ChronologyEnvironmentInvolved
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.ChronologyEnvironmentInvolved, esSystemType.String);
			}
		} 
		
		public esQueryItem SRIncidentImpact
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.SRIncidentImpact, esSystemType.String);
			}
		} 
		
		public esQueryItem InjuryDetail
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.InjuryDetail, esSystemType.String);
			}
		} 
		
		public esQueryItem EnvironmentCost
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.EnvironmentCost, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem ConditionAfterFirstAid
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.ConditionAfterFirstAid, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmployeeOutpatientTreatment
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployeeOutpatientTreatment, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmployeeTreatment
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployeeTreatment, esSystemType.String);
			}
		} 
		
		public esQueryItem AdditionalInformation
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.AdditionalInformation, esSystemType.String);
			}
		} 
		
		public esQueryItem InsertByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.InsertByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem InsertDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeHealthAndSafetyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeHealthAndSafetyCollection")]
	public partial class EmployeeHealthAndSafetyCollection : esEmployeeHealthAndSafetyCollection, IEnumerable<EmployeeHealthAndSafety>
	{
		public EmployeeHealthAndSafetyCollection()
		{

		}
		
		public static implicit operator List<EmployeeHealthAndSafety>(EmployeeHealthAndSafetyCollection coll)
		{
			List<EmployeeHealthAndSafety> list = new List<EmployeeHealthAndSafety>();
			
			foreach (EmployeeHealthAndSafety emp in coll)
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
				return  EmployeeHealthAndSafetyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeHealthAndSafetyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeHealthAndSafety(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeHealthAndSafety();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeHealthAndSafetyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeHealthAndSafetyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeHealthAndSafetyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeHealthAndSafety AddNew()
		{
			EmployeeHealthAndSafety entity = base.AddNewEntity() as EmployeeHealthAndSafety;
			
			return entity;
		}

		public EmployeeHealthAndSafety FindByPrimaryKey(System.String employeeHealthAndSafetyNo)
		{
			return base.FindByPrimaryKey(employeeHealthAndSafetyNo) as EmployeeHealthAndSafety;
		}


		#region IEnumerable<EmployeeHealthAndSafety> Members

		IEnumerator<EmployeeHealthAndSafety> IEnumerable<EmployeeHealthAndSafety>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeHealthAndSafety;
			}
		}

		#endregion
		
		private EmployeeHealthAndSafetyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeHealthAndSafety' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeHealthAndSafety ({EmployeeHealthAndSafetyNo})")]
	[Serializable]
	public partial class EmployeeHealthAndSafety : esEmployeeHealthAndSafety
	{
		public EmployeeHealthAndSafety()
		{

		}
	
		public EmployeeHealthAndSafety(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeHealthAndSafetyMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeHealthAndSafetyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeHealthAndSafetyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeHealthAndSafetyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeHealthAndSafetyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeHealthAndSafetyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeHealthAndSafetyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeHealthAndSafetyQuery : esEmployeeHealthAndSafetyQuery
	{
		public EmployeeHealthAndSafetyQuery()
		{

		}		
		
		public EmployeeHealthAndSafetyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeHealthAndSafetyQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeHealthAndSafetyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeHealthAndSafetyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeHealthAndSafetyNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.EmployeeHealthAndSafetyNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployerStatus, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.SREmployerStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.EmployeeID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.EmployeeName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.EmployeeName;
			c.CharacterMaxLength = 60;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.Birthdate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.Birthdate;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.BirthPlace, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.BirthPlace;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.SRGenderType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.Address, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.Telp, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.Telp;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.DepartmentID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.JobTitle, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.JobTitle;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.IncidentPlace, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.IncidentPlace;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.IncidentDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.IncidentDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.ArriveInHospitalDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.ArriveInHospitalDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.LeaveHospitalDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.LeaveHospitalDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.Chronology, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.Chronology;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.ChronologyEnvironmentInvolved, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.ChronologyEnvironmentInvolved;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.SRIncidentImpact, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.SRIncidentImpact;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.InjuryDetail, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.InjuryDetail;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.EnvironmentCost, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.EnvironmentCost;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.ParamedicID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.ConditionAfterFirstAid, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.ConditionAfterFirstAid;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployeeOutpatientTreatment, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.SREmployeeOutpatientTreatment;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.SREmployeeTreatment, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.SREmployeeTreatment;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.AdditionalInformation, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.AdditionalInformation;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.InsertByUserID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.InsertByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.InsertDateTime, 26, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.InsertDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.LastUpdateByUserID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeHealthAndSafetyMetadata.ColumnNames.LastUpdateDateTime, 28, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeHealthAndSafetyMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeHealthAndSafetyMetadata Meta()
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
			 public const string EmployeeHealthAndSafetyNo = "EmployeeHealthAndSafetyNo";
			 public const string SREmployerStatus = "SREmployerStatus";
			 public const string EmployeeID = "EmployeeID";
			 public const string EmployeeName = "EmployeeName";
			 public const string Birthdate = "Birthdate";
			 public const string BirthPlace = "BirthPlace";
			 public const string SRGenderType = "SRGenderType";
			 public const string Address = "Address";
			 public const string Telp = "Telp";
			 public const string DepartmentID = "DepartmentID";
			 public const string JobTitle = "JobTitle";
			 public const string IncidentPlace = "IncidentPlace";
			 public const string IncidentDateTime = "IncidentDateTime";
			 public const string ArriveInHospitalDateTime = "ArriveInHospitalDateTime";
			 public const string LeaveHospitalDateTime = "LeaveHospitalDateTime";
			 public const string Chronology = "Chronology";
			 public const string ChronologyEnvironmentInvolved = "ChronologyEnvironmentInvolved";
			 public const string SRIncidentImpact = "SRIncidentImpact";
			 public const string InjuryDetail = "InjuryDetail";
			 public const string EnvironmentCost = "EnvironmentCost";
			 public const string ParamedicID = "ParamedicID";
			 public const string ConditionAfterFirstAid = "ConditionAfterFirstAid";
			 public const string SREmployeeOutpatientTreatment = "SREmployeeOutpatientTreatment";
			 public const string SREmployeeTreatment = "SREmployeeTreatment";
			 public const string AdditionalInformation = "AdditionalInformation";
			 public const string InsertByUserID = "InsertByUserID";
			 public const string InsertDateTime = "InsertDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeHealthAndSafetyNo = "EmployeeHealthAndSafetyNo";
			 public const string SREmployerStatus = "SREmployerStatus";
			 public const string EmployeeID = "EmployeeID";
			 public const string EmployeeName = "EmployeeName";
			 public const string Birthdate = "Birthdate";
			 public const string BirthPlace = "BirthPlace";
			 public const string SRGenderType = "SRGenderType";
			 public const string Address = "Address";
			 public const string Telp = "Telp";
			 public const string DepartmentID = "DepartmentID";
			 public const string JobTitle = "JobTitle";
			 public const string IncidentPlace = "IncidentPlace";
			 public const string IncidentDateTime = "IncidentDateTime";
			 public const string ArriveInHospitalDateTime = "ArriveInHospitalDateTime";
			 public const string LeaveHospitalDateTime = "LeaveHospitalDateTime";
			 public const string Chronology = "Chronology";
			 public const string ChronologyEnvironmentInvolved = "ChronologyEnvironmentInvolved";
			 public const string SRIncidentImpact = "SRIncidentImpact";
			 public const string InjuryDetail = "InjuryDetail";
			 public const string EnvironmentCost = "EnvironmentCost";
			 public const string ParamedicID = "ParamedicID";
			 public const string ConditionAfterFirstAid = "ConditionAfterFirstAid";
			 public const string SREmployeeOutpatientTreatment = "SREmployeeOutpatientTreatment";
			 public const string SREmployeeTreatment = "SREmployeeTreatment";
			 public const string AdditionalInformation = "AdditionalInformation";
			 public const string InsertByUserID = "InsertByUserID";
			 public const string InsertDateTime = "InsertDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(EmployeeHealthAndSafetyMetadata))
			{
				if(EmployeeHealthAndSafetyMetadata.mapDelegates == null)
				{
					EmployeeHealthAndSafetyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeHealthAndSafetyMetadata.meta == null)
				{
					EmployeeHealthAndSafetyMetadata.meta = new EmployeeHealthAndSafetyMetadata();
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
				

				meta.AddTypeMap("EmployeeHealthAndSafetyNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployerStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Birthdate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("BirthPlace", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Telp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JobTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncidentPlace", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncidentDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ArriveInHospitalDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LeaveHospitalDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Chronology", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChronologyEnvironmentInvolved", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncidentImpact", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InjuryDetail", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EnvironmentCost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConditionAfterFirstAid", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeOutpatientTreatment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeTreatment", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdditionalInformation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "EmployeeHealthAndSafety";
				meta.Destination = "EmployeeHealthAndSafety";
				
				meta.spInsert = "proc_EmployeeHealthAndSafetyInsert";				
				meta.spUpdate = "proc_EmployeeHealthAndSafetyUpdate";		
				meta.spDelete = "proc_EmployeeHealthAndSafetyDelete";
				meta.spLoadAll = "proc_EmployeeHealthAndSafetyLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeHealthAndSafetyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeHealthAndSafetyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
