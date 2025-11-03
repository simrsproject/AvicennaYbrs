/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/22/2017 1:20:05 PM
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
	abstract public class esVwEmployeeAllTableCollection : esEntityCollectionWAuditLog
	{
		public esVwEmployeeAllTableCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwEmployeeAllTableCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwEmployeeAllTableQuery query)
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
			this.InitQuery(query as esVwEmployeeAllTableQuery);
		}
		#endregion
		
		virtual public VwEmployeeAllTable DetachEntity(VwEmployeeAllTable entity)
		{
			return base.DetachEntity(entity) as VwEmployeeAllTable;
		}
		
		virtual public VwEmployeeAllTable AttachEntity(VwEmployeeAllTable entity)
		{
			return base.AttachEntity(entity) as VwEmployeeAllTable;
		}
		
		virtual public void Combine(VwEmployeeAllTableCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwEmployeeAllTable this[int index]
		{
			get
			{
				return base[index] as VwEmployeeAllTable;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwEmployeeAllTable);
		}
	}



	[Serializable]
	abstract public class esVwEmployeeAllTable : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwEmployeeAllTableQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwEmployeeAllTable()
		{

		}

		public esVwEmployeeAllTable(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		
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
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "EmployeeNumber": this.str.EmployeeNumber = (string)value; break;							
						case "EmployeeName": this.str.EmployeeName = (string)value; break;							
						case "SupervisorId": this.str.SupervisorId = (string)value; break;							
						case "SREmployeeType": this.str.SREmployeeType = (string)value; break;							
						case "SRRemunerationType": this.str.SRRemunerationType = (string)value; break;							
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;							
						case "SubOrganizationUnitID": this.str.SubOrganizationUnitID = (string)value; break;							
						case "SRPaymentFrequency": this.str.SRPaymentFrequency = (string)value; break;							
						case "SRTaxStatus": this.str.SRTaxStatus = (string)value; break;							
						case "SREmployeeStatus": this.str.SREmployeeStatus = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "SRReligion": this.str.SRReligion = (string)value; break;							
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;							
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;							
						case "PositionLevelID": this.str.PositionLevelID = (string)value; break;							
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;							
						case "JoinDate": this.str.JoinDate = (string)value; break;							
						case "BirthDate": this.str.BirthDate = (string)value; break;							
						case "ServiceYear": this.str.ServiceYear = (string)value; break;							
						case "ServiceYearText": this.str.ServiceYearText = (string)value; break;							
						case "EmployeeLevel": this.str.EmployeeLevel = (string)value; break;							
						case "SalaryTableNumber": this.str.SalaryTableNumber = (string)value; break;							
						case "EmployeeGradeID": this.str.EmployeeGradeID = (string)value; break;							
						case "NoOfDependent": this.str.NoOfDependent = (string)value; break;							
						case "IsKWI": this.str.IsKWI = (string)value; break;							
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;							
						case "GradeYear": this.str.GradeYear = (string)value; break;							
						case "CoverageClass": this.str.CoverageClass = (string)value; break;							
						case "CoverageClassBPJS": this.str.CoverageClassBPJS = (string)value; break;							
						case "SRGenderType": this.str.SRGenderType = (string)value; break;							
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "ResignDate": this.str.ResignDate = (string)value; break;							
						case "SRResignReason": this.str.SRResignReason = (string)value; break;							
						case "IsBPJS": this.str.IsBPJS = (string)value; break;							
						case "BPJSUncoveredNo": this.str.BPJSUncoveredNo = (string)value; break;							
						case "ServiceMonthThr": this.str.ServiceMonthThr = (string)value; break;							
						case "ServiceMonthPPH": this.str.ServiceMonthPPH = (string)value; break;							
						case "IsJP": this.str.IsJP = (string)value; break;							
						case "AbsenceCardNo": this.str.AbsenceCardNo = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "EmployeeTypePayroll": this.str.EmployeeTypePayroll = (string)value; break;
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
						
						case "SupervisorId":
						
							if (value == null || value is System.Int32)
								this.SupervisorId = (System.Int32?)value;
							break;
						
						case "OrganizationUnitID":
						
							if (value == null || value is System.Int32)
								this.OrganizationUnitID = (System.Int32?)value;
							break;
						
						case "SubOrganizationUnitID":
						
							if (value == null || value is System.Int32)
								this.SubOrganizationUnitID = (System.Int32?)value;
							break;
						
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
							break;
						
						case "PositionGradeID":
						
							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						
						case "PositionLevelID":
						
							if (value == null || value is System.Int32)
								this.PositionLevelID = (System.Int32?)value;
							break;
						
						case "JoinDate":
						
							if (value == null || value is System.DateTime)
								this.JoinDate = (System.DateTime?)value;
							break;
						
						case "BirthDate":
						
							if (value == null || value is System.DateTime)
								this.BirthDate = (System.DateTime?)value;
							break;
						
						case "ServiceYear":
						
							if (value == null || value is System.Decimal)
								this.ServiceYear = (System.Decimal?)value;
							break;
						
						case "SalaryTableNumber":
						
							if (value == null || value is System.Int32)
								this.SalaryTableNumber = (System.Int32?)value;
							break;
						
						case "EmployeeGradeID":
						
							if (value == null || value is System.Int32)
								this.EmployeeGradeID = (System.Int32?)value;
							break;
						
						case "NoOfDependent":
						
							if (value == null || value is System.Int32)
								this.NoOfDependent = (System.Int32?)value;
							break;
						
						case "IsKWI":
						
							if (value == null || value is System.Int32)
								this.IsKWI = (System.Int32?)value;
							break;
						
						case "GradeYear":
						
							if (value == null || value is System.Decimal)
								this.GradeYear = (System.Decimal?)value;
							break;
						
						case "ResignDate":
						
							if (value == null || value is System.DateTime)
								this.ResignDate = (System.DateTime?)value;
							break;
						
						case "IsBPJS":
						
							if (value == null || value is System.Int32)
								this.IsBPJS = (System.Int32?)value;
							break;
						
						case "BPJSUncoveredNo":
						
							if (value == null || value is System.Int32)
								this.BPJSUncoveredNo = (System.Int32?)value;
							break;
						
						case "ServiceMonthThr":
						
							if (value == null || value is System.Decimal)
								this.ServiceMonthThr = (System.Decimal?)value;
							break;
						
						case "ServiceMonthPPH":
						
							if (value == null || value is System.Int32)
								this.ServiceMonthPPH = (System.Int32?)value;
							break;
						
						case "IsJP":
						
							if (value == null || value is System.Int32)
								this.IsJP = (System.Int32?)value;
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
		/// Maps to Vw_EmployeeAllTable.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.EmployeeNumber
		/// </summary>
		virtual public System.String EmployeeNumber
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.EmployeeNumber);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.EmployeeNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.EmployeeName
		/// </summary>
		virtual public System.String EmployeeName
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.EmployeeName);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.EmployeeName, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SupervisorId
		/// </summary>
		virtual public System.Int32? SupervisorId
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.SupervisorId);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.SupervisorId, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SREmployeeType
		/// </summary>
		virtual public System.String SREmployeeType
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SREmployeeType);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SREmployeeType, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SRRemunerationType
		/// </summary>
		virtual public System.String SRRemunerationType
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRRemunerationType);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRRemunerationType, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.OrganizationUnitID);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.SubOrganizationUnitID);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SRPaymentFrequency
		/// </summary>
		virtual public System.String SRPaymentFrequency
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRPaymentFrequency);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRPaymentFrequency, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SRTaxStatus
		/// </summary>
		virtual public System.String SRTaxStatus
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRTaxStatus);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRTaxStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SREmployeeStatus
		/// </summary>
		virtual public System.String SREmployeeStatus
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SREmployeeStatus);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SREmployeeStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SRReligion
		/// </summary>
		virtual public System.String SRReligion
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRReligion);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRReligion, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SREmploymentType);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.PositionGradeID);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.PositionLevelID
		/// </summary>
		virtual public System.Int32? PositionLevelID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.PositionLevelID);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.PositionLevelID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRMaritalStatus);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.JoinDate
		/// </summary>
		virtual public System.DateTime? JoinDate
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeAllTableMetadata.ColumnNames.JoinDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwEmployeeAllTableMetadata.ColumnNames.JoinDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.BirthDate
		/// </summary>
		virtual public System.DateTime? BirthDate
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeAllTableMetadata.ColumnNames.BirthDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwEmployeeAllTableMetadata.ColumnNames.BirthDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.ServiceYear
		/// </summary>
		virtual public System.Decimal? ServiceYear
		{
			get
			{
				return base.GetSystemDecimal(VwEmployeeAllTableMetadata.ColumnNames.ServiceYear);
			}
			
			set
			{
				base.SetSystemDecimal(VwEmployeeAllTableMetadata.ColumnNames.ServiceYear, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.ServiceYearText
		/// </summary>
		virtual public System.String ServiceYearText
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.ServiceYearText);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.ServiceYearText, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.EmployeeLevel
		/// </summary>
		virtual public System.String EmployeeLevel
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.EmployeeLevel);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.EmployeeLevel, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SalaryTableNumber
		/// </summary>
		virtual public System.Int32? SalaryTableNumber
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.SalaryTableNumber);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.SalaryTableNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.EmployeeGradeID
		/// </summary>
		virtual public System.Int32? EmployeeGradeID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.EmployeeGradeID);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.EmployeeGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.NoOfDependent
		/// </summary>
		virtual public System.Int32? NoOfDependent
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.NoOfDependent);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.NoOfDependent, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.IsKWI
		/// </summary>
		virtual public System.Int32? IsKWI
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.IsKWI);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.IsKWI, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SREducationLevel);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.GradeYear
		/// </summary>
		virtual public System.Decimal? GradeYear
		{
			get
			{
				return base.GetSystemDecimal(VwEmployeeAllTableMetadata.ColumnNames.GradeYear);
			}
			
			set
			{
				base.SetSystemDecimal(VwEmployeeAllTableMetadata.ColumnNames.GradeYear, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.CoverageClass
		/// </summary>
		virtual public System.String CoverageClass
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.CoverageClass);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.CoverageClass, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.CoverageClassBPJS
		/// </summary>
		virtual public System.String CoverageClassBPJS
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.CoverageClassBPJS);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.CoverageClassBPJS, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRGenderType);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRGenderType, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.ResignDate
		/// </summary>
		virtual public System.DateTime? ResignDate
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeAllTableMetadata.ColumnNames.ResignDate);
			}
			
			set
			{
				base.SetSystemDateTime(VwEmployeeAllTableMetadata.ColumnNames.ResignDate, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.SRResignReason
		/// </summary>
		virtual public System.String SRResignReason
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRResignReason);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.SRResignReason, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.IsBPJS
		/// </summary>
		virtual public System.Int32? IsBPJS
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.IsBPJS);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.IsBPJS, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.BPJSUncoveredNo
		/// </summary>
		virtual public System.Int32? BPJSUncoveredNo
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.BPJSUncoveredNo);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.BPJSUncoveredNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.ServiceMonthThr
		/// </summary>
		virtual public System.Decimal? ServiceMonthThr
		{
			get
			{
				return base.GetSystemDecimal(VwEmployeeAllTableMetadata.ColumnNames.ServiceMonthThr);
			}
			
			set
			{
				base.SetSystemDecimal(VwEmployeeAllTableMetadata.ColumnNames.ServiceMonthThr, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.ServiceMonthPPH
		/// </summary>
		virtual public System.Int32? ServiceMonthPPH
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.ServiceMonthPPH);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.ServiceMonthPPH, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.IsJP
		/// </summary>
		virtual public System.Int32? IsJP
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.IsJP);
			}
			
			set
			{
				base.SetSystemInt32(VwEmployeeAllTableMetadata.ColumnNames.IsJP, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.AbsenceCardNo
		/// </summary>
		virtual public System.String AbsenceCardNo
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.AbsenceCardNo);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.AbsenceCardNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to Vw_EmployeeAllTable.EmployeeTypePayroll
		/// </summary>
		virtual public System.String EmployeeTypePayroll
		{
			get
			{
				return base.GetSystemString(VwEmployeeAllTableMetadata.ColumnNames.EmployeeTypePayroll);
			}
			
			set
			{
				base.SetSystemString(VwEmployeeAllTableMetadata.ColumnNames.EmployeeTypePayroll, value);
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
			public esStrings(esVwEmployeeAllTable entity)
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
				
			public System.String EmployeeNumber
			{
				get
				{
					System.String data = entity.EmployeeNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeNumber = null;
					else entity.EmployeeNumber = Convert.ToString(value);
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
				
			public System.String OrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.OrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationUnitID = null;
					else entity.OrganizationUnitID = Convert.ToInt32(value);
				}
			}
				
			public System.String SubOrganizationUnitID
			{
				get
				{
					System.Int32? data = entity.SubOrganizationUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubOrganizationUnitID = null;
					else entity.SubOrganizationUnitID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRPaymentFrequency
			{
				get
				{
					System.String data = entity.SRPaymentFrequency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentFrequency = null;
					else entity.SRPaymentFrequency = Convert.ToString(value);
				}
			}
				
			public System.String SRTaxStatus
			{
				get
				{
					System.String data = entity.SRTaxStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTaxStatus = null;
					else entity.SRTaxStatus = Convert.ToString(value);
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
				
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRReligion
			{
				get
				{
					System.String data = entity.SRReligion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReligion = null;
					else entity.SRReligion = Convert.ToString(value);
				}
			}
				
			public System.String SREmploymentType
			{
				get
				{
					System.String data = entity.SREmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentType = null;
					else entity.SREmploymentType = Convert.ToString(value);
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
				
			public System.String PositionLevelID
			{
				get
				{
					System.Int32? data = entity.PositionLevelID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionLevelID = null;
					else entity.PositionLevelID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRMaritalStatus
			{
				get
				{
					System.String data = entity.SRMaritalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMaritalStatus = null;
					else entity.SRMaritalStatus = Convert.ToString(value);
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
				
			public System.String BirthDate
			{
				get
				{
					System.DateTime? data = entity.BirthDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BirthDate = null;
					else entity.BirthDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ServiceYear
			{
				get
				{
					System.Decimal? data = entity.ServiceYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceYear = null;
					else entity.ServiceYear = Convert.ToDecimal(value);
				}
			}
				
			public System.String ServiceYearText
			{
				get
				{
					System.String data = entity.ServiceYearText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceYearText = null;
					else entity.ServiceYearText = Convert.ToString(value);
				}
			}
				
			public System.String EmployeeLevel
			{
				get
				{
					System.String data = entity.EmployeeLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeLevel = null;
					else entity.EmployeeLevel = Convert.ToString(value);
				}
			}
				
			public System.String SalaryTableNumber
			{
				get
				{
					System.Int32? data = entity.SalaryTableNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryTableNumber = null;
					else entity.SalaryTableNumber = Convert.ToInt32(value);
				}
			}
				
			public System.String EmployeeGradeID
			{
				get
				{
					System.Int32? data = entity.EmployeeGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeGradeID = null;
					else entity.EmployeeGradeID = Convert.ToInt32(value);
				}
			}
				
			public System.String NoOfDependent
			{
				get
				{
					System.Int32? data = entity.NoOfDependent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoOfDependent = null;
					else entity.NoOfDependent = Convert.ToInt32(value);
				}
			}
				
			public System.String IsKWI
			{
				get
				{
					System.Int32? data = entity.IsKWI;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsKWI = null;
					else entity.IsKWI = Convert.ToInt32(value);
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
				
			public System.String CoverageClass
			{
				get
				{
					System.String data = entity.CoverageClass;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageClass = null;
					else entity.CoverageClass = Convert.ToString(value);
				}
			}
				
			public System.String CoverageClassBPJS
			{
				get
				{
					System.String data = entity.CoverageClassBPJS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageClassBPJS = null;
					else entity.CoverageClassBPJS = Convert.ToString(value);
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
				
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
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
				
			public System.String IsBPJS
			{
				get
				{
					System.Int32? data = entity.IsBPJS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBPJS = null;
					else entity.IsBPJS = Convert.ToInt32(value);
				}
			}
				
			public System.String BPJSUncoveredNo
			{
				get
				{
					System.Int32? data = entity.BPJSUncoveredNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BPJSUncoveredNo = null;
					else entity.BPJSUncoveredNo = Convert.ToInt32(value);
				}
			}
				
			public System.String ServiceMonthThr
			{
				get
				{
					System.Decimal? data = entity.ServiceMonthThr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceMonthThr = null;
					else entity.ServiceMonthThr = Convert.ToDecimal(value);
				}
			}
				
			public System.String ServiceMonthPPH
			{
				get
				{
					System.Int32? data = entity.ServiceMonthPPH;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceMonthPPH = null;
					else entity.ServiceMonthPPH = Convert.ToInt32(value);
				}
			}
				
			public System.String IsJP
			{
				get
				{
					System.Int32? data = entity.IsJP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsJP = null;
					else entity.IsJP = Convert.ToInt32(value);
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
				
			public System.String EmployeeTypePayroll
			{
				get
				{
					System.String data = entity.EmployeeTypePayroll;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeTypePayroll = null;
					else entity.EmployeeTypePayroll = Convert.ToString(value);
				}
			}
			

			private esVwEmployeeAllTable entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwEmployeeAllTableQuery query)
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
				throw new Exception("esVwEmployeeAllTable can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwEmployeeAllTableQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwEmployeeAllTableMetadata.Meta();
			}
		}	
		

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EmployeeNumber
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.EmployeeNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeName
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.EmployeeName, esSystemType.String);
			}
		} 
		
		public esQueryItem SupervisorId
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SupervisorId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SREmployeeType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SREmployeeType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRRemunerationType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SRRemunerationType, esSystemType.String);
			}
		} 
		
		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRPaymentFrequency
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SRPaymentFrequency, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTaxStatus
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SRTaxStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmployeeStatus
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SREmployeeStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRReligion
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SRReligion, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionLevelID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.PositionLevelID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem JoinDate
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.JoinDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem BirthDate
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.BirthDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ServiceYear
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.ServiceYear, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ServiceYearText
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.ServiceYearText, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeLevel
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.EmployeeLevel, esSystemType.String);
			}
		} 
		
		public esQueryItem SalaryTableNumber
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SalaryTableNumber, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EmployeeGradeID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.EmployeeGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem NoOfDependent
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.NoOfDependent, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsKWI
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.IsKWI, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		} 
		
		public esQueryItem GradeYear
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.GradeYear, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CoverageClass
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.CoverageClass, esSystemType.String);
			}
		} 
		
		public esQueryItem CoverageClassBPJS
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.CoverageClassBPJS, esSystemType.String);
			}
		} 
		
		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem ResignDate
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.ResignDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRResignReason
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.SRResignReason, esSystemType.String);
			}
		} 
		
		public esQueryItem IsBPJS
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.IsBPJS, esSystemType.Int32);
			}
		} 
		
		public esQueryItem BPJSUncoveredNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.BPJSUncoveredNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ServiceMonthThr
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.ServiceMonthThr, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ServiceMonthPPH
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.ServiceMonthPPH, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsJP
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.IsJP, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AbsenceCardNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.AbsenceCardNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem EmployeeTypePayroll
		{
			get
			{
				return new esQueryItem(this, VwEmployeeAllTableMetadata.ColumnNames.EmployeeTypePayroll, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwEmployeeAllTableCollection")]
	public partial class VwEmployeeAllTableCollection : esVwEmployeeAllTableCollection, IEnumerable<VwEmployeeAllTable>
	{
		public VwEmployeeAllTableCollection()
		{

		}
		
		public static implicit operator List<VwEmployeeAllTable>(VwEmployeeAllTableCollection coll)
		{
			List<VwEmployeeAllTable> list = new List<VwEmployeeAllTable>();
			
			foreach (VwEmployeeAllTable emp in coll)
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
				return  VwEmployeeAllTableMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwEmployeeAllTableQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwEmployeeAllTable(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwEmployeeAllTable();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwEmployeeAllTableQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwEmployeeAllTableQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwEmployeeAllTableQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwEmployeeAllTable AddNew()
		{
			VwEmployeeAllTable entity = base.AddNewEntity() as VwEmployeeAllTable;
			
			return entity;
		}


		#region IEnumerable<VwEmployeeAllTable> Members

		IEnumerator<VwEmployeeAllTable> IEnumerable<VwEmployeeAllTable>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwEmployeeAllTable;
			}
		}

		#endregion
		
		private VwEmployeeAllTableQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Vw_EmployeeAllTable' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwEmployeeAllTable ()")]
	[Serializable]
	public partial class VwEmployeeAllTable : esVwEmployeeAllTable
	{
		public VwEmployeeAllTable()
		{

		}
	
		public VwEmployeeAllTable(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwEmployeeAllTableMetadata.Meta();
			}
		}
		
		
		
		override protected esVwEmployeeAllTableQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwEmployeeAllTableQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwEmployeeAllTableQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwEmployeeAllTableQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwEmployeeAllTableQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwEmployeeAllTableQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwEmployeeAllTableQuery : esVwEmployeeAllTableQuery
	{
		public VwEmployeeAllTableQuery()
		{

		}		
		
		public VwEmployeeAllTableQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwEmployeeAllTableQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwEmployeeAllTableMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwEmployeeAllTableMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.PersonID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.EmployeeNumber, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.EmployeeNumber;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.EmployeeName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.EmployeeName;
			c.CharacterMaxLength = 144;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SupervisorId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SupervisorId;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SREmployeeType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SREmployeeType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SRRemunerationType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SRRemunerationType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.OrganizationUnitID, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SubOrganizationUnitID, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SRPaymentFrequency, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SRPaymentFrequency;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SRTaxStatus, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SRTaxStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SREmployeeStatus, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SREmployeeStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.PositionID, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SRReligion, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SRReligion;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SREmploymentType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.PositionGradeID, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.PositionLevelID, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.PositionLevelID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SRMaritalStatus, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.JoinDate, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.JoinDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.BirthDate, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.BirthDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.ServiceYear, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.ServiceYear;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.ServiceYearText, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.ServiceYearText;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.EmployeeLevel, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.EmployeeLevel;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SalaryTableNumber, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SalaryTableNumber;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.EmployeeGradeID, 23, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.EmployeeGradeID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.NoOfDependent, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.NoOfDependent;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.IsKWI, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.IsKWI;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SREducationLevel, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.GradeYear, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.GradeYear;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.CoverageClass, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.CoverageClass;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.CoverageClassBPJS, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.CoverageClassBPJS;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SRGenderType, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.PatientID, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.ResignDate, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.ResignDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.SRResignReason, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.SRResignReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.IsBPJS, 34, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.IsBPJS;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.BPJSUncoveredNo, 35, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.BPJSUncoveredNo;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.ServiceMonthThr, 36, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.ServiceMonthThr;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.ServiceMonthPPH, 37, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.ServiceMonthPPH;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.IsJP, 38, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.IsJP;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.AbsenceCardNo, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.AbsenceCardNo;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.ServiceUnitID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwEmployeeAllTableMetadata.ColumnNames.EmployeeTypePayroll, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeAllTableMetadata.PropertyNames.EmployeeTypePayroll;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwEmployeeAllTableMetadata Meta()
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
			 public const string PersonID = "PersonID";
			 public const string EmployeeNumber = "EmployeeNumber";
			 public const string EmployeeName = "EmployeeName";
			 public const string SupervisorId = "SupervisorId";
			 public const string SREmployeeType = "SREmployeeType";
			 public const string SRRemunerationType = "SRRemunerationType";
			 public const string OrganizationUnitID = "OrganizationUnitID";
			 public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			 public const string SRPaymentFrequency = "SRPaymentFrequency";
			 public const string SRTaxStatus = "SRTaxStatus";
			 public const string SREmployeeStatus = "SREmployeeStatus";
			 public const string PositionID = "PositionID";
			 public const string SRReligion = "SRReligion";
			 public const string SREmploymentType = "SREmploymentType";
			 public const string PositionGradeID = "PositionGradeID";
			 public const string PositionLevelID = "PositionLevelID";
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string JoinDate = "JoinDate";
			 public const string BirthDate = "BirthDate";
			 public const string ServiceYear = "ServiceYear";
			 public const string ServiceYearText = "ServiceYearText";
			 public const string EmployeeLevel = "EmployeeLevel";
			 public const string SalaryTableNumber = "SalaryTableNumber";
			 public const string EmployeeGradeID = "EmployeeGradeID";
			 public const string NoOfDependent = "NoOfDependent";
			 public const string IsKWI = "IsKWI";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string GradeYear = "GradeYear";
			 public const string CoverageClass = "CoverageClass";
			 public const string CoverageClassBPJS = "CoverageClassBPJS";
			 public const string SRGenderType = "SRGenderType";
			 public const string PatientID = "PatientID";
			 public const string ResignDate = "ResignDate";
			 public const string SRResignReason = "SRResignReason";
			 public const string IsBPJS = "IsBPJS";
			 public const string BPJSUncoveredNo = "BPJSUncoveredNo";
			 public const string ServiceMonthThr = "ServiceMonthThr";
			 public const string ServiceMonthPPH = "ServiceMonthPPH";
			 public const string IsJP = "IsJP";
			 public const string AbsenceCardNo = "AbsenceCardNo";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string EmployeeTypePayroll = "EmployeeTypePayroll";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PersonID = "PersonID";
			 public const string EmployeeNumber = "EmployeeNumber";
			 public const string EmployeeName = "EmployeeName";
			 public const string SupervisorId = "SupervisorId";
			 public const string SREmployeeType = "SREmployeeType";
			 public const string SRRemunerationType = "SRRemunerationType";
			 public const string OrganizationUnitID = "OrganizationUnitID";
			 public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			 public const string SRPaymentFrequency = "SRPaymentFrequency";
			 public const string SRTaxStatus = "SRTaxStatus";
			 public const string SREmployeeStatus = "SREmployeeStatus";
			 public const string PositionID = "PositionID";
			 public const string SRReligion = "SRReligion";
			 public const string SREmploymentType = "SREmploymentType";
			 public const string PositionGradeID = "PositionGradeID";
			 public const string PositionLevelID = "PositionLevelID";
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string JoinDate = "JoinDate";
			 public const string BirthDate = "BirthDate";
			 public const string ServiceYear = "ServiceYear";
			 public const string ServiceYearText = "ServiceYearText";
			 public const string EmployeeLevel = "EmployeeLevel";
			 public const string SalaryTableNumber = "SalaryTableNumber";
			 public const string EmployeeGradeID = "EmployeeGradeID";
			 public const string NoOfDependent = "NoOfDependent";
			 public const string IsKWI = "IsKWI";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string GradeYear = "GradeYear";
			 public const string CoverageClass = "CoverageClass";
			 public const string CoverageClassBPJS = "CoverageClassBPJS";
			 public const string SRGenderType = "SRGenderType";
			 public const string PatientID = "PatientID";
			 public const string ResignDate = "ResignDate";
			 public const string SRResignReason = "SRResignReason";
			 public const string IsBPJS = "IsBPJS";
			 public const string BPJSUncoveredNo = "BPJSUncoveredNo";
			 public const string ServiceMonthThr = "ServiceMonthThr";
			 public const string ServiceMonthPPH = "ServiceMonthPPH";
			 public const string IsJP = "IsJP";
			 public const string AbsenceCardNo = "AbsenceCardNo";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string EmployeeTypePayroll = "EmployeeTypePayroll";
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
			lock (typeof(VwEmployeeAllTableMetadata))
			{
				if(VwEmployeeAllTableMetadata.mapDelegates == null)
				{
					VwEmployeeAllTableMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwEmployeeAllTableMetadata.meta == null)
				{
					VwEmployeeAllTableMetadata.meta = new VwEmployeeAllTableMetadata();
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
				

				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupervisorId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmployeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRemunerationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubOrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPaymentFrequency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTaxStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRReligion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionLevelID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JoinDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BirthDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ServiceYear", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ServiceYearText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalaryTableNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NoOfDependent", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsKWI", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GradeYear", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("CoverageClass", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageClassBPJS", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResignDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRResignReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsBPJS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BPJSUncoveredNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceMonthThr", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ServiceMonthPPH", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsJP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AbsenceCardNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeTypePayroll", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Vw_EmployeeAllTable";
				meta.Destination = "Vw_EmployeeAllTable";
				
				meta.spInsert = "proc_Vw_EmployeeAllTableInsert";				
				meta.spUpdate = "proc_Vw_EmployeeAllTableUpdate";		
				meta.spDelete = "proc_Vw_EmployeeAllTableDelete";
				meta.spLoadAll = "proc_Vw_EmployeeAllTableLoadAll";
				meta.spLoadByPrimaryKey = "proc_Vw_EmployeeAllTableLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwEmployeeAllTableMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
