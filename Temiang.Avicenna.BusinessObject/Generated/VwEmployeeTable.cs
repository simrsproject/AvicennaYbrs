/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/24/2023 10:11:09 PM
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
	abstract public class esVwEmployeeTableCollection : esEntityCollection
	{
		public esVwEmployeeTableCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "VwEmployeeTableCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwEmployeeTableQuery query)
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
			this.InitQuery(query as esVwEmployeeTableQuery);
		}
		#endregion

		virtual public VwEmployeeTable DetachEntity(VwEmployeeTable entity)
		{
			return base.DetachEntity(entity) as VwEmployeeTable;
		}

		virtual public VwEmployeeTable AttachEntity(VwEmployeeTable entity)
		{
			return base.AttachEntity(entity) as VwEmployeeTable;
		}

		virtual public void Combine(VwEmployeeTableCollection collection)
		{
			base.Combine(collection);
		}

		new public VwEmployeeTable this[int index]
		{
			get
			{
				return base[index] as VwEmployeeTable;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwEmployeeTable);
		}
	}

	[Serializable]
	abstract public class esVwEmployeeTable : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwEmployeeTableQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwEmployeeTable()
		{
		}

		public esVwEmployeeTable(DataRow row)
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
						case "EmployeeNumber": this.str.EmployeeNumber = (string)value; break;
						case "EmployeeName": this.str.EmployeeName = (string)value; break;
						case "EmployeeRegistrationNo": this.str.EmployeeRegistrationNo = (string)value; break;
						case "ManagerID": this.str.ManagerID = (string)value; break;
						case "SupervisorId": this.str.SupervisorId = (string)value; break;
						case "PreceptorId": this.str.PreceptorId = (string)value; break;
						case "SREmployeeType": this.str.SREmployeeType = (string)value; break;
						case "SRRemunerationType": this.str.SRRemunerationType = (string)value; break;
						case "SREmployeeShiftType": this.str.SREmployeeShiftType = (string)value; break;
						case "SREmployeeScheduleType": this.str.SREmployeeScheduleType = (string)value; break;
						case "SRProfessionType": this.str.SRProfessionType = (string)value; break;
						case "SRProfessionGroup": this.str.SRProfessionGroup = (string)value; break;
						case "SRClinicalWorkArea": this.str.SRClinicalWorkArea = (string)value; break;
						case "SRClinicalAuthorityLevel": this.str.SRClinicalAuthorityLevel = (string)value; break;
						case "OrganizationUnitID": this.str.OrganizationUnitID = (string)value; break;
						case "SubOrganizationUnitID": this.str.SubOrganizationUnitID = (string)value; break;
						case "SRPaymentFrequency": this.str.SRPaymentFrequency = (string)value; break;
						case "SRTaxStatus": this.str.SRTaxStatus = (string)value; break;
						case "SREmployeeStatus": this.str.SREmployeeStatus = (string)value; break;
						case "PositionID": this.str.PositionID = (string)value; break;
						case "SRReligion": this.str.SRReligion = (string)value; break;
						case "SRBloodType": this.str.SRBloodType = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "BankNo": this.str.BankNo = (string)value; break;
						case "BankAccountName": this.str.BankAccountName = (string)value; break;
						case "SRIncentiveServiceUnitGroup": this.str.SRIncentiveServiceUnitGroup = (string)value; break;
						case "SRIncentivePositionGroup": this.str.SRIncentivePositionGroup = (string)value; break;
						case "SRIncentivePosition": this.str.SRIncentivePosition = (string)value; break;
						case "IncentivePositionPoints": this.str.IncentivePositionPoints = (string)value; break;
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;
						case "SREmploymentCategory": this.str.SREmploymentCategory = (string)value; break;
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;
						case "PositionLevelID": this.str.PositionLevelID = (string)value; break;
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;
						case "JoinDate": this.str.JoinDate = (string)value; break;
						case "BirthDate": this.str.BirthDate = (string)value; break;
						case "PlaceBirth": this.str.PlaceBirth = (string)value; break;
						case "ServiceYear": this.str.ServiceYear = (string)value; break;
						case "ServiceYearText": this.str.ServiceYearText = (string)value; break;
						case "ServiceYearPermanent": this.str.ServiceYearPermanent = (string)value; break;
						case "ServiceYearPermanentText": this.str.ServiceYearPermanentText = (string)value; break;
						case "EmployeeLevel": this.str.EmployeeLevel = (string)value; break;
						case "SalaryTableNumber": this.str.SalaryTableNumber = (string)value; break;
						case "EmployeeGradeID": this.str.EmployeeGradeID = (string)value; break;
						case "NoOfDependent": this.str.NoOfDependent = (string)value; break;
						case "IsKWI": this.str.IsKWI = (string)value; break;
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
						case "SRFieldLabor": this.str.SRFieldLabor = (string)value; break;
						case "SREducationGroup": this.str.SREducationGroup = (string)value; break;
						case "GradeYear": this.str.GradeYear = (string)value; break;
						case "SalaryScaleID": this.str.SalaryScaleID = (string)value; break;
						case "CoverageClass": this.str.CoverageClass = (string)value; break;
						case "CoverageClassBPJS": this.str.CoverageClassBPJS = (string)value; break;
						case "SRGenderType": this.str.SRGenderType = (string)value; break;
						case "PatientID": this.str.PatientID = (string)value; break;
						case "ResignDate": this.str.ResignDate = (string)value; break;
						case "ResignDateReal": this.str.ResignDateReal = (string)value; break;
						case "SRResignReason": this.str.SRResignReason = (string)value; break;
						case "IsBPJS": this.str.IsBPJS = (string)value; break;
						case "BPJSUncoveredNo": this.str.BPJSUncoveredNo = (string)value; break;
						case "BPJSCoveredNo": this.str.BPJSCoveredNo = (string)value; break;
						case "ServiceMonthThr": this.str.ServiceMonthThr = (string)value; break;
						case "ServiceMonthPPH": this.str.ServiceMonthPPH = (string)value; break;
						case "IsJP": this.str.IsJP = (string)value; break;
						case "AbsenceCardNo": this.str.AbsenceCardNo = (string)value; break;
						case "SubDivisonID": this.str.SubDivisonID = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "EmployeeTypePayroll": this.str.EmployeeTypePayroll = (string)value; break;
						case "IsSalaryManaged": this.str.IsSalaryManaged = (string)value; break;
						case "TglDiangkat": this.str.TglDiangkat = (string)value; break;
						case "IjazahPengangkatan": this.str.IjazahPengangkatan = (string)value; break;
						case "IsNpwp": this.str.IsNpwp = (string)value; break;
						case "Npwp": this.str.Npwp = (string)value; break;
						case "PositionValidFromDate": this.str.PositionValidFromDate = (string)value; break;
						case "ProfessionType": this.str.ProfessionType = (string)value; break;
						case "BpjsKesNo": this.str.BpjsKesNo = (string)value; break;
						case "BpjsTkNo": this.str.BpjsTkNo = (string)value; break;
						case "Nik": this.str.Nik = (string)value; break;
						case "SupervisedCount": this.str.SupervisedCount = (string)value; break;
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
						case "ManagerID":

							if (value == null || value is System.Int32)
								this.ManagerID = (System.Int32?)value;
							break;
						case "SupervisorId":

							if (value == null || value is System.Int32)
								this.SupervisorId = (System.Int32?)value;
							break;
						case "PreceptorId":

							if (value == null || value is System.Int32)
								this.PreceptorId = (System.Int32?)value;
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
						case "IncentivePositionPoints":

							if (value == null || value is System.Decimal)
								this.IncentivePositionPoints = (System.Decimal?)value;
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
						case "ServiceYearPermanent":

							if (value == null || value is System.Decimal)
								this.ServiceYearPermanent = (System.Decimal?)value;
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

							if (value == null || value is System.Int32)
								this.GradeYear = (System.Int32?)value;
							break;
						case "SalaryScaleID":

							if (value == null || value is System.Int32)
								this.SalaryScaleID = (System.Int32?)value;
							break;
						case "ResignDate":

							if (value == null || value is System.DateTime)
								this.ResignDate = (System.DateTime?)value;
							break;
						case "ResignDateReal":

							if (value == null || value is System.DateTime)
								this.ResignDateReal = (System.DateTime?)value;
							break;
						case "IsBPJS":

							if (value == null || value is System.Int32)
								this.IsBPJS = (System.Int32?)value;
							break;
						case "BPJSUncoveredNo":

							if (value == null || value is System.Int32)
								this.BPJSUncoveredNo = (System.Int32?)value;
							break;
						case "BPJSCoveredNo":

							if (value == null || value is System.Int32)
								this.BPJSCoveredNo = (System.Int32?)value;
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
						case "SubDivisonID":

							if (value == null || value is System.Int32)
								this.SubDivisonID = (System.Int32?)value;
							break;
						case "IsSalaryManaged":

							if (value == null || value is System.Boolean)
								this.IsSalaryManaged = (System.Boolean?)value;
							break;
						case "TglDiangkat":

							if (value == null || value is System.DateTime)
								this.TglDiangkat = (System.DateTime?)value;
							break;
						case "IsNpwp":

							if (value == null || value is System.Int32)
								this.IsNpwp = (System.Int32?)value;
							break;
						case "PositionValidFromDate":

							if (value == null || value is System.DateTime)
								this.PositionValidFromDate = (System.DateTime?)value;
							break;
						case "SupervisedCount":

							if (value == null || value is System.Int32)
								this.SupervisedCount = (System.Int32?)value;
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
		/// Maps to VwEmployeeTable.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.EmployeeNumber
		/// </summary>
		virtual public System.String EmployeeNumber
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeNumber);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeNumber, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.EmployeeName
		/// </summary>
		virtual public System.String EmployeeName
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeName);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeName, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.EmployeeRegistrationNo
		/// </summary>
		virtual public System.String EmployeeRegistrationNo
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeRegistrationNo);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeRegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ManagerID
		/// </summary>
		virtual public System.Int32? ManagerID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.ManagerID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.ManagerID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SupervisorId
		/// </summary>
		virtual public System.Int32? SupervisorId
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SupervisorId);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SupervisorId, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.PreceptorId
		/// </summary>
		virtual public System.Int32? PreceptorId
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PreceptorId);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PreceptorId, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SREmployeeType
		/// </summary>
		virtual public System.String SREmployeeType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmployeeType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmployeeType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRRemunerationType
		/// </summary>
		virtual public System.String SRRemunerationType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRRemunerationType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRRemunerationType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SREmployeeShiftType
		/// </summary>
		virtual public System.String SREmployeeShiftType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmployeeShiftType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmployeeShiftType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SREmployeeScheduleType
		/// </summary>
		virtual public System.String SREmployeeScheduleType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmployeeScheduleType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmployeeScheduleType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRProfessionType
		/// </summary>
		virtual public System.String SRProfessionType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRProfessionType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRProfessionType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRProfessionGroup
		/// </summary>
		virtual public System.String SRProfessionGroup
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRProfessionGroup);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRProfessionGroup, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRClinicalWorkArea
		/// </summary>
		virtual public System.String SRClinicalWorkArea
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRClinicalWorkArea);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRClinicalWorkArea, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRClinicalAuthorityLevel
		/// </summary>
		virtual public System.String SRClinicalAuthorityLevel
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRClinicalAuthorityLevel);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRClinicalAuthorityLevel, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.OrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SubOrganizationUnitID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRPaymentFrequency
		/// </summary>
		virtual public System.String SRPaymentFrequency
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRPaymentFrequency);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRPaymentFrequency, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRTaxStatus
		/// </summary>
		virtual public System.String SRTaxStatus
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRTaxStatus);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRTaxStatus, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SREmployeeStatus
		/// </summary>
		virtual public System.String SREmployeeStatus
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmployeeStatus);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmployeeStatus, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PositionID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PositionID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRReligion
		/// </summary>
		virtual public System.String SRReligion
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRReligion);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRReligion, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRBloodType
		/// </summary>
		virtual public System.String SRBloodType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRBloodType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRBloodType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.BankID);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.BankNo
		/// </summary>
		virtual public System.String BankNo
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.BankNo);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.BankNo, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.BankAccountName
		/// </summary>
		virtual public System.String BankAccountName
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.BankAccountName);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.BankAccountName, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRIncentiveServiceUnitGroup
		/// </summary>
		virtual public System.String SRIncentiveServiceUnitGroup
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRIncentiveServiceUnitGroup);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRIncentiveServiceUnitGroup, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRIncentivePositionGroup
		/// </summary>
		virtual public System.String SRIncentivePositionGroup
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRIncentivePositionGroup);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRIncentivePositionGroup, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRIncentivePosition
		/// </summary>
		virtual public System.String SRIncentivePosition
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRIncentivePosition);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRIncentivePosition, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.IncentivePositionPoints
		/// </summary>
		virtual public System.Decimal? IncentivePositionPoints
		{
			get
			{
				return base.GetSystemDecimal(VwEmployeeTableMetadata.ColumnNames.IncentivePositionPoints);
			}

			set
			{
				base.SetSystemDecimal(VwEmployeeTableMetadata.ColumnNames.IncentivePositionPoints, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmploymentType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SREmploymentCategory
		/// </summary>
		virtual public System.String SREmploymentCategory
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmploymentCategory);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SREmploymentCategory, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PositionGradeID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.PositionLevelID
		/// </summary>
		virtual public System.Int32? PositionLevelID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PositionLevelID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.PositionLevelID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRMaritalStatus);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.JoinDate
		/// </summary>
		virtual public System.DateTime? JoinDate
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.JoinDate);
			}

			set
			{
				base.SetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.JoinDate, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.BirthDate
		/// </summary>
		virtual public System.DateTime? BirthDate
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.BirthDate);
			}

			set
			{
				base.SetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.BirthDate, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.PlaceBirth
		/// </summary>
		virtual public System.String PlaceBirth
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.PlaceBirth);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.PlaceBirth, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ServiceYear
		/// </summary>
		virtual public System.Decimal? ServiceYear
		{
			get
			{
				return base.GetSystemDecimal(VwEmployeeTableMetadata.ColumnNames.ServiceYear);
			}

			set
			{
				base.SetSystemDecimal(VwEmployeeTableMetadata.ColumnNames.ServiceYear, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ServiceYearText
		/// </summary>
		virtual public System.String ServiceYearText
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.ServiceYearText);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.ServiceYearText, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ServiceYearPermanent
		/// </summary>
		virtual public System.Decimal? ServiceYearPermanent
		{
			get
			{
				return base.GetSystemDecimal(VwEmployeeTableMetadata.ColumnNames.ServiceYearPermanent);
			}

			set
			{
				base.SetSystemDecimal(VwEmployeeTableMetadata.ColumnNames.ServiceYearPermanent, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ServiceYearPermanentText
		/// </summary>
		virtual public System.String ServiceYearPermanentText
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.ServiceYearPermanentText);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.ServiceYearPermanentText, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.EmployeeLevel
		/// </summary>
		virtual public System.String EmployeeLevel
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeLevel);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeLevel, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SalaryTableNumber
		/// </summary>
		virtual public System.Int32? SalaryTableNumber
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SalaryTableNumber);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SalaryTableNumber, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.EmployeeGradeID
		/// </summary>
		virtual public System.Int32? EmployeeGradeID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.EmployeeGradeID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.EmployeeGradeID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.NoOfDependent
		/// </summary>
		virtual public System.Int32? NoOfDependent
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.NoOfDependent);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.NoOfDependent, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.IsKWI
		/// </summary>
		virtual public System.Int32? IsKWI
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.IsKWI);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.IsKWI, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SREducationLevel);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRFieldLabor
		/// </summary>
		virtual public System.String SRFieldLabor
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRFieldLabor);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRFieldLabor, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SREducationGroup
		/// </summary>
		virtual public System.String SREducationGroup
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SREducationGroup);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SREducationGroup, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.GradeYear
		/// </summary>
		virtual public System.Int32? GradeYear
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.GradeYear);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.GradeYear, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SalaryScaleID
		/// </summary>
		virtual public System.Int32? SalaryScaleID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SalaryScaleID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SalaryScaleID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.CoverageClass
		/// </summary>
		virtual public System.String CoverageClass
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.CoverageClass);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.CoverageClass, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.CoverageClassBPJS
		/// </summary>
		virtual public System.String CoverageClassBPJS
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.CoverageClassBPJS);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.CoverageClassBPJS, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRGenderType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRGenderType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.PatientID);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.PatientID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ResignDate
		/// </summary>
		virtual public System.DateTime? ResignDate
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.ResignDate);
			}

			set
			{
				base.SetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.ResignDate, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ResignDateReal
		/// </summary>
		virtual public System.DateTime? ResignDateReal
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.ResignDateReal);
			}

			set
			{
				base.SetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.ResignDateReal, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SRResignReason
		/// </summary>
		virtual public System.String SRResignReason
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.SRResignReason);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.SRResignReason, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.IsBPJS
		/// </summary>
		virtual public System.Int32? IsBPJS
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.IsBPJS);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.IsBPJS, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.BPJSUncoveredNo
		/// </summary>
		virtual public System.Int32? BPJSUncoveredNo
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.BPJSUncoveredNo);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.BPJSUncoveredNo, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.BPJSCoveredNo
		/// </summary>
		virtual public System.Int32? BPJSCoveredNo
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.BPJSCoveredNo);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.BPJSCoveredNo, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ServiceMonthThr
		/// </summary>
		virtual public System.Decimal? ServiceMonthThr
		{
			get
			{
				return base.GetSystemDecimal(VwEmployeeTableMetadata.ColumnNames.ServiceMonthThr);
			}

			set
			{
				base.SetSystemDecimal(VwEmployeeTableMetadata.ColumnNames.ServiceMonthThr, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ServiceMonthPPH
		/// </summary>
		virtual public System.Int32? ServiceMonthPPH
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.ServiceMonthPPH);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.ServiceMonthPPH, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.IsJP
		/// </summary>
		virtual public System.Int32? IsJP
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.IsJP);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.IsJP, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.AbsenceCardNo
		/// </summary>
		virtual public System.String AbsenceCardNo
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.AbsenceCardNo);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.AbsenceCardNo, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SubDivisonID
		/// </summary>
		virtual public System.Int32? SubDivisonID
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SubDivisonID);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SubDivisonID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.EmployeeTypePayroll
		/// </summary>
		virtual public System.String EmployeeTypePayroll
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeTypePayroll);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.EmployeeTypePayroll, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.IsSalaryManaged
		/// </summary>
		virtual public System.Boolean? IsSalaryManaged
		{
			get
			{
				return base.GetSystemBoolean(VwEmployeeTableMetadata.ColumnNames.IsSalaryManaged);
			}

			set
			{
				base.SetSystemBoolean(VwEmployeeTableMetadata.ColumnNames.IsSalaryManaged, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.TglDiangkat
		/// </summary>
		virtual public System.DateTime? TglDiangkat
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.TglDiangkat);
			}

			set
			{
				base.SetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.TglDiangkat, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.IjazahPengangkatan
		/// </summary>
		virtual public System.String IjazahPengangkatan
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.IjazahPengangkatan);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.IjazahPengangkatan, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.IsNpwp
		/// </summary>
		virtual public System.Int32? IsNpwp
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.IsNpwp);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.IsNpwp, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.Npwp
		/// </summary>
		virtual public System.String Npwp
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.Npwp);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.Npwp, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.PositionValidFromDate
		/// </summary>
		virtual public System.DateTime? PositionValidFromDate
		{
			get
			{
				return base.GetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.PositionValidFromDate);
			}

			set
			{
				base.SetSystemDateTime(VwEmployeeTableMetadata.ColumnNames.PositionValidFromDate, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.ProfessionType
		/// </summary>
		virtual public System.String ProfessionType
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.ProfessionType);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.ProfessionType, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.BpjsKesNo
		/// </summary>
		virtual public System.String BpjsKesNo
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.BpjsKesNo);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.BpjsKesNo, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.BpjsTkNo
		/// </summary>
		virtual public System.String BpjsTkNo
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.BpjsTkNo);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.BpjsTkNo, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.Nik
		/// </summary>
		virtual public System.String Nik
		{
			get
			{
				return base.GetSystemString(VwEmployeeTableMetadata.ColumnNames.Nik);
			}

			set
			{
				base.SetSystemString(VwEmployeeTableMetadata.ColumnNames.Nik, value);
			}
		}
		/// <summary>
		/// Maps to VwEmployeeTable.SupervisedCount
		/// </summary>
		virtual public System.Int32? SupervisedCount
		{
			get
			{
				return base.GetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SupervisedCount);
			}

			set
			{
				base.SetSystemInt32(VwEmployeeTableMetadata.ColumnNames.SupervisedCount, value);
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
			public esStrings(esVwEmployeeTable entity)
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
			public System.String SRProfessionGroup
			{
				get
				{
					System.String data = entity.SRProfessionGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProfessionGroup = null;
					else entity.SRProfessionGroup = Convert.ToString(value);
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
			public System.String SRBloodType
			{
				get
				{
					System.String data = entity.SRBloodType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodType = null;
					else entity.SRBloodType = Convert.ToString(value);
				}
			}
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
			public System.String BankNo
			{
				get
				{
					System.String data = entity.BankNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankNo = null;
					else entity.BankNo = Convert.ToString(value);
				}
			}
			public System.String BankAccountName
			{
				get
				{
					System.String data = entity.BankAccountName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankAccountName = null;
					else entity.BankAccountName = Convert.ToString(value);
				}
			}
			public System.String SRIncentiveServiceUnitGroup
			{
				get
				{
					System.String data = entity.SRIncentiveServiceUnitGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncentiveServiceUnitGroup = null;
					else entity.SRIncentiveServiceUnitGroup = Convert.ToString(value);
				}
			}
			public System.String SRIncentivePositionGroup
			{
				get
				{
					System.String data = entity.SRIncentivePositionGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncentivePositionGroup = null;
					else entity.SRIncentivePositionGroup = Convert.ToString(value);
				}
			}
			public System.String SRIncentivePosition
			{
				get
				{
					System.String data = entity.SRIncentivePosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRIncentivePosition = null;
					else entity.SRIncentivePosition = Convert.ToString(value);
				}
			}
			public System.String IncentivePositionPoints
			{
				get
				{
					System.Decimal? data = entity.IncentivePositionPoints;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IncentivePositionPoints = null;
					else entity.IncentivePositionPoints = Convert.ToDecimal(value);
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
			public System.String SREmploymentCategory
			{
				get
				{
					System.String data = entity.SREmploymentCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentCategory = null;
					else entity.SREmploymentCategory = Convert.ToString(value);
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
			public System.String PlaceBirth
			{
				get
				{
					System.String data = entity.PlaceBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlaceBirth = null;
					else entity.PlaceBirth = Convert.ToString(value);
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
			public System.String ServiceYearPermanent
			{
				get
				{
					System.Decimal? data = entity.ServiceYearPermanent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceYearPermanent = null;
					else entity.ServiceYearPermanent = Convert.ToDecimal(value);
				}
			}
			public System.String ServiceYearPermanentText
			{
				get
				{
					System.String data = entity.ServiceYearPermanentText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceYearPermanentText = null;
					else entity.ServiceYearPermanentText = Convert.ToString(value);
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
			public System.String SRFieldLabor
			{
				get
				{
					System.String data = entity.SRFieldLabor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFieldLabor = null;
					else entity.SRFieldLabor = Convert.ToString(value);
				}
			}
			public System.String SREducationGroup
			{
				get
				{
					System.String data = entity.SREducationGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationGroup = null;
					else entity.SREducationGroup = Convert.ToString(value);
				}
			}
			public System.String GradeYear
			{
				get
				{
					System.Int32? data = entity.GradeYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GradeYear = null;
					else entity.GradeYear = Convert.ToInt32(value);
				}
			}
			public System.String SalaryScaleID
			{
				get
				{
					System.Int32? data = entity.SalaryScaleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryScaleID = null;
					else entity.SalaryScaleID = Convert.ToInt32(value);
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
			public System.String ResignDateReal
			{
				get
				{
					System.DateTime? data = entity.ResignDateReal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResignDateReal = null;
					else entity.ResignDateReal = Convert.ToDateTime(value);
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
			public System.String BPJSCoveredNo
			{
				get
				{
					System.Int32? data = entity.BPJSCoveredNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BPJSCoveredNo = null;
					else entity.BPJSCoveredNo = Convert.ToInt32(value);
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
			public System.String SubDivisonID
			{
				get
				{
					System.Int32? data = entity.SubDivisonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubDivisonID = null;
					else entity.SubDivisonID = Convert.ToInt32(value);
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
			public System.String IsSalaryManaged
			{
				get
				{
					System.Boolean? data = entity.IsSalaryManaged;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSalaryManaged = null;
					else entity.IsSalaryManaged = Convert.ToBoolean(value);
				}
			}
			public System.String TglDiangkat
			{
				get
				{
					System.DateTime? data = entity.TglDiangkat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglDiangkat = null;
					else entity.TglDiangkat = Convert.ToDateTime(value);
				}
			}
			public System.String IjazahPengangkatan
			{
				get
				{
					System.String data = entity.IjazahPengangkatan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IjazahPengangkatan = null;
					else entity.IjazahPengangkatan = Convert.ToString(value);
				}
			}
			public System.String IsNpwp
			{
				get
				{
					System.Int32? data = entity.IsNpwp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNpwp = null;
					else entity.IsNpwp = Convert.ToInt32(value);
				}
			}
			public System.String Npwp
			{
				get
				{
					System.String data = entity.Npwp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Npwp = null;
					else entity.Npwp = Convert.ToString(value);
				}
			}
			public System.String PositionValidFromDate
			{
				get
				{
					System.DateTime? data = entity.PositionValidFromDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionValidFromDate = null;
					else entity.PositionValidFromDate = Convert.ToDateTime(value);
				}
			}
			public System.String ProfessionType
			{
				get
				{
					System.String data = entity.ProfessionType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProfessionType = null;
					else entity.ProfessionType = Convert.ToString(value);
				}
			}
			public System.String BpjsKesNo
			{
				get
				{
					System.String data = entity.BpjsKesNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsKesNo = null;
					else entity.BpjsKesNo = Convert.ToString(value);
				}
			}
			public System.String BpjsTkNo
			{
				get
				{
					System.String data = entity.BpjsTkNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpjsTkNo = null;
					else entity.BpjsTkNo = Convert.ToString(value);
				}
			}
			public System.String Nik
			{
				get
				{
					System.String data = entity.Nik;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nik = null;
					else entity.Nik = Convert.ToString(value);
				}
			}
			public System.String SupervisedCount
			{
				get
				{
					System.Int32? data = entity.SupervisedCount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisedCount = null;
					else entity.SupervisedCount = Convert.ToInt32(value);
				}
			}
			private esVwEmployeeTable entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwEmployeeTableQuery query)
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
				throw new Exception("esVwEmployeeTable can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class VwEmployeeTable : esVwEmployeeTable
	{
	}

	[Serializable]
	abstract public class esVwEmployeeTableQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return VwEmployeeTableMetadata.Meta();
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeNumber
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.EmployeeNumber, esSystemType.String);
			}
		}

		public esQueryItem EmployeeName
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.EmployeeName, esSystemType.String);
			}
		}

		public esQueryItem EmployeeRegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.EmployeeRegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem ManagerID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ManagerID, esSystemType.Int32);
			}
		}

		public esQueryItem SupervisorId
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SupervisorId, esSystemType.Int32);
			}
		}

		public esQueryItem PreceptorId
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.PreceptorId, esSystemType.Int32);
			}
		}

		public esQueryItem SREmployeeType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SREmployeeType, esSystemType.String);
			}
		}

		public esQueryItem SRRemunerationType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRRemunerationType, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeShiftType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SREmployeeShiftType, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeScheduleType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SREmployeeScheduleType, esSystemType.String);
			}
		}

		public esQueryItem SRProfessionType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRProfessionType, esSystemType.String);
			}
		}

		public esQueryItem SRProfessionGroup
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRProfessionGroup, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalWorkArea
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRClinicalWorkArea, esSystemType.String);
			}
		}

		public esQueryItem SRClinicalAuthorityLevel
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRClinicalAuthorityLevel, esSystemType.String);
			}
		}

		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		}

		public esQueryItem SRPaymentFrequency
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRPaymentFrequency, esSystemType.String);
			}
		}

		public esQueryItem SRTaxStatus
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRTaxStatus, esSystemType.String);
			}
		}

		public esQueryItem SREmployeeStatus
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SREmployeeStatus, esSystemType.String);
			}
		}

		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		}

		public esQueryItem SRReligion
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRReligion, esSystemType.String);
			}
		}

		public esQueryItem SRBloodType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRBloodType, esSystemType.String);
			}
		}

		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.BankID, esSystemType.String);
			}
		}

		public esQueryItem BankNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.BankNo, esSystemType.String);
			}
		}

		public esQueryItem BankAccountName
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.BankAccountName, esSystemType.String);
			}
		}

		public esQueryItem SRIncentiveServiceUnitGroup
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRIncentiveServiceUnitGroup, esSystemType.String);
			}
		}

		public esQueryItem SRIncentivePositionGroup
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRIncentivePositionGroup, esSystemType.String);
			}
		}

		public esQueryItem SRIncentivePosition
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRIncentivePosition, esSystemType.String);
			}
		}

		public esQueryItem IncentivePositionPoints
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.IncentivePositionPoints, esSystemType.Decimal);
			}
		}

		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		}

		public esQueryItem SREmploymentCategory
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SREmploymentCategory, esSystemType.String);
			}
		}

		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem PositionLevelID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.PositionLevelID, esSystemType.Int32);
			}
		}

		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		}

		public esQueryItem JoinDate
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.JoinDate, esSystemType.DateTime);
			}
		}

		public esQueryItem BirthDate
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.BirthDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PlaceBirth
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.PlaceBirth, esSystemType.String);
			}
		}

		public esQueryItem ServiceYear
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ServiceYear, esSystemType.Decimal);
			}
		}

		public esQueryItem ServiceYearText
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ServiceYearText, esSystemType.String);
			}
		}

		public esQueryItem ServiceYearPermanent
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ServiceYearPermanent, esSystemType.Decimal);
			}
		}

		public esQueryItem ServiceYearPermanentText
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ServiceYearPermanentText, esSystemType.String);
			}
		}

		public esQueryItem EmployeeLevel
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.EmployeeLevel, esSystemType.String);
			}
		}

		public esQueryItem SalaryTableNumber
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SalaryTableNumber, esSystemType.Int32);
			}
		}

		public esQueryItem EmployeeGradeID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.EmployeeGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem NoOfDependent
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.NoOfDependent, esSystemType.Int32);
			}
		}

		public esQueryItem IsKWI
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.IsKWI, esSystemType.Int32);
			}
		}

		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		}

		public esQueryItem SRFieldLabor
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRFieldLabor, esSystemType.String);
			}
		}

		public esQueryItem SREducationGroup
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SREducationGroup, esSystemType.String);
			}
		}

		public esQueryItem GradeYear
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.GradeYear, esSystemType.Int32);
			}
		}

		public esQueryItem SalaryScaleID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SalaryScaleID, esSystemType.Int32);
			}
		}

		public esQueryItem CoverageClass
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.CoverageClass, esSystemType.String);
			}
		}

		public esQueryItem CoverageClassBPJS
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.CoverageClassBPJS, esSystemType.String);
			}
		}

		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		}

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		}

		public esQueryItem ResignDate
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ResignDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ResignDateReal
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ResignDateReal, esSystemType.DateTime);
			}
		}

		public esQueryItem SRResignReason
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SRResignReason, esSystemType.String);
			}
		}

		public esQueryItem IsBPJS
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.IsBPJS, esSystemType.Int32);
			}
		}

		public esQueryItem BPJSUncoveredNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.BPJSUncoveredNo, esSystemType.Int32);
			}
		}

		public esQueryItem BPJSCoveredNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.BPJSCoveredNo, esSystemType.Int32);
			}
		}

		public esQueryItem ServiceMonthThr
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ServiceMonthThr, esSystemType.Decimal);
			}
		}

		public esQueryItem ServiceMonthPPH
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ServiceMonthPPH, esSystemType.Int32);
			}
		}

		public esQueryItem IsJP
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.IsJP, esSystemType.Int32);
			}
		}

		public esQueryItem AbsenceCardNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.AbsenceCardNo, esSystemType.String);
			}
		}

		public esQueryItem SubDivisonID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SubDivisonID, esSystemType.Int32);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem EmployeeTypePayroll
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.EmployeeTypePayroll, esSystemType.String);
			}
		}

		public esQueryItem IsSalaryManaged
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.IsSalaryManaged, esSystemType.Boolean);
			}
		}

		public esQueryItem TglDiangkat
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.TglDiangkat, esSystemType.DateTime);
			}
		}

		public esQueryItem IjazahPengangkatan
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.IjazahPengangkatan, esSystemType.String);
			}
		}

		public esQueryItem IsNpwp
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.IsNpwp, esSystemType.Int32);
			}
		}

		public esQueryItem Npwp
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.Npwp, esSystemType.String);
			}
		}

		public esQueryItem PositionValidFromDate
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.PositionValidFromDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ProfessionType
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.ProfessionType, esSystemType.String);
			}
		}

		public esQueryItem BpjsKesNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.BpjsKesNo, esSystemType.String);
			}
		}

		public esQueryItem BpjsTkNo
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.BpjsTkNo, esSystemType.String);
			}
		}

		public esQueryItem Nik
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.Nik, esSystemType.String);
			}
		}

		public esQueryItem SupervisedCount
		{
			get
			{
				return new esQueryItem(this, VwEmployeeTableMetadata.ColumnNames.SupervisedCount, esSystemType.Int32);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwEmployeeTableCollection")]
	public partial class VwEmployeeTableCollection : esVwEmployeeTableCollection, IEnumerable<VwEmployeeTable>
	{
		public VwEmployeeTableCollection()
		{

		}

		public static implicit operator List<VwEmployeeTable>(VwEmployeeTableCollection coll)
		{
			List<VwEmployeeTable> list = new List<VwEmployeeTable>();

			foreach (VwEmployeeTable emp in coll)
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
				return VwEmployeeTableMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwEmployeeTableQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwEmployeeTable(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwEmployeeTable();
		}

		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}

		#endregion

		[BrowsableAttribute(false)]
		public VwEmployeeTableQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwEmployeeTableQuery();
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
		public bool Load(VwEmployeeTableQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public VwEmployeeTable AddNew()
		{
			VwEmployeeTable entity = base.AddNewEntity() as VwEmployeeTable;

			return entity;
		}

		#region IEnumerable< VwEmployeeTable> Members

		IEnumerator<VwEmployeeTable> IEnumerable<VwEmployeeTable>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as VwEmployeeTable;
			}
		}

		#endregion

		private VwEmployeeTableQuery query;
	}


	/// <summary>
	/// Encapsulates the 'VwEmployeeTable' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("VwEmployeeTable ()")]
	[Serializable]
	public partial class VwEmployeeTable : esVwEmployeeTable
	{
		public VwEmployeeTable()
		{
		}

		public VwEmployeeTable(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwEmployeeTableMetadata.Meta();
			}
		}

		override protected esVwEmployeeTableQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwEmployeeTableQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public VwEmployeeTableQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwEmployeeTableQuery();
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
		public bool Load(VwEmployeeTableQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private VwEmployeeTableQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class VwEmployeeTableQuery : esVwEmployeeTableQuery
	{
		public VwEmployeeTableQuery()
		{

		}

		public VwEmployeeTableQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "VwEmployeeTableQuery";
		}
	}

	[Serializable]
	public partial class VwEmployeeTableMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwEmployeeTableMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.PersonID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.EmployeeNumber, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.EmployeeNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.EmployeeName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.EmployeeName;
			c.CharacterMaxLength = 144;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.EmployeeRegistrationNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.EmployeeRegistrationNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ManagerID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ManagerID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SupervisorId, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SupervisorId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.PreceptorId, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.PreceptorId;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SREmployeeType, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SREmployeeType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRRemunerationType, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRRemunerationType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SREmployeeShiftType, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SREmployeeShiftType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SREmployeeScheduleType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SREmployeeScheduleType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRProfessionType, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRProfessionType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRProfessionGroup, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRProfessionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRClinicalWorkArea, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRClinicalWorkArea;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRClinicalAuthorityLevel, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRClinicalAuthorityLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.OrganizationUnitID, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SubOrganizationUnitID, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRPaymentFrequency, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRPaymentFrequency;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRTaxStatus, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRTaxStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SREmployeeStatus, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SREmployeeStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.PositionID, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRReligion, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRReligion;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRBloodType, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRBloodType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.BankID, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.BankNo, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.BankNo;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.BankAccountName, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.BankAccountName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRIncentiveServiceUnitGroup, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRIncentiveServiceUnitGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRIncentivePositionGroup, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRIncentivePositionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRIncentivePosition, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRIncentivePosition;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.IncentivePositionPoints, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.IncentivePositionPoints;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SREmploymentType, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SREmploymentCategory, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SREmploymentCategory;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.PositionGradeID, 32, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.PositionLevelID, 33, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.PositionLevelID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRMaritalStatus, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.JoinDate, 35, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.JoinDate;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.BirthDate, 36, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.BirthDate;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.PlaceBirth, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.PlaceBirth;
			c.CharacterMaxLength = 60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ServiceYear, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ServiceYear;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ServiceYearText, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ServiceYearText;
			c.CharacterMaxLength = -1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ServiceYearPermanent, 40, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ServiceYearPermanent;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ServiceYearPermanentText, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ServiceYearPermanentText;
			c.CharacterMaxLength = -1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.EmployeeLevel, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.EmployeeLevel;
			c.CharacterMaxLength = 1;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SalaryTableNumber, 43, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SalaryTableNumber;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.EmployeeGradeID, 44, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.EmployeeGradeID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.NoOfDependent, 45, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.NoOfDependent;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.IsKWI, 46, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.IsKWI;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SREducationLevel, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRFieldLabor, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRFieldLabor;
			c.CharacterMaxLength = 2000;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SREducationGroup, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SREducationGroup;
			c.CharacterMaxLength = 2000;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.GradeYear, 50, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.GradeYear;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SalaryScaleID, 51, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SalaryScaleID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.CoverageClass, 52, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.CoverageClass;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.CoverageClassBPJS, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.CoverageClassBPJS;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRGenderType, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.PatientID, 55, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ResignDate, 56, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ResignDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ResignDateReal, 57, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ResignDateReal;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SRResignReason, 58, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SRResignReason;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.IsBPJS, 59, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.IsBPJS;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.BPJSUncoveredNo, 60, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.BPJSUncoveredNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.BPJSCoveredNo, 61, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.BPJSCoveredNo;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ServiceMonthThr, 62, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ServiceMonthThr;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ServiceMonthPPH, 63, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ServiceMonthPPH;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.IsJP, 64, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.IsJP;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.AbsenceCardNo, 65, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.AbsenceCardNo;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SubDivisonID, 66, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SubDivisonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ServiceUnitID, 67, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.EmployeeTypePayroll, 68, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.EmployeeTypePayroll;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.IsSalaryManaged, 69, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.IsSalaryManaged;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.TglDiangkat, 70, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.TglDiangkat;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.IjazahPengangkatan, 71, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.IjazahPengangkatan;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.IsNpwp, 72, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.IsNpwp;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.Npwp, 73, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.Npwp;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.PositionValidFromDate, 74, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.PositionValidFromDate;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.ProfessionType, 75, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.ProfessionType;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.BpjsKesNo, 76, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.BpjsKesNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.BpjsTkNo, 77, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.BpjsTkNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.Nik, 78, typeof(System.String), esSystemType.String);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.Nik;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(VwEmployeeTableMetadata.ColumnNames.SupervisedCount, 79, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwEmployeeTableMetadata.PropertyNames.SupervisedCount;
			c.NumericPrecision = 10;
			_columns.Add(c);


		}
		#endregion

		static public VwEmployeeTableMetadata Meta()
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
			public const string EmployeeNumber = "EmployeeNumber";
			public const string EmployeeName = "EmployeeName";
			public const string EmployeeRegistrationNo = "EmployeeRegistrationNo";
			public const string ManagerID = "ManagerID";
			public const string SupervisorId = "SupervisorId";
			public const string PreceptorId = "PreceptorId";
			public const string SREmployeeType = "SREmployeeType";
			public const string SRRemunerationType = "SRRemunerationType";
			public const string SREmployeeShiftType = "SREmployeeShiftType";
			public const string SREmployeeScheduleType = "SREmployeeScheduleType";
			public const string SRProfessionType = "SRProfessionType";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string SRPaymentFrequency = "SRPaymentFrequency";
			public const string SRTaxStatus = "SRTaxStatus";
			public const string SREmployeeStatus = "SREmployeeStatus";
			public const string PositionID = "PositionID";
			public const string SRReligion = "SRReligion";
			public const string SRBloodType = "SRBloodType";
			public const string BankID = "BankID";
			public const string BankNo = "BankNo";
			public const string BankAccountName = "BankAccountName";
			public const string SRIncentiveServiceUnitGroup = "SRIncentiveServiceUnitGroup";
			public const string SRIncentivePositionGroup = "SRIncentivePositionGroup";
			public const string SRIncentivePosition = "SRIncentivePosition";
			public const string IncentivePositionPoints = "IncentivePositionPoints";
			public const string SREmploymentType = "SREmploymentType";
			public const string SREmploymentCategory = "SREmploymentCategory";
			public const string PositionGradeID = "PositionGradeID";
			public const string PositionLevelID = "PositionLevelID";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string JoinDate = "JoinDate";
			public const string BirthDate = "BirthDate";
			public const string PlaceBirth = "PlaceBirth";
			public const string ServiceYear = "ServiceYear";
			public const string ServiceYearText = "ServiceYearText";
			public const string ServiceYearPermanent = "ServiceYearPermanent";
			public const string ServiceYearPermanentText = "ServiceYearPermanentText";
			public const string EmployeeLevel = "EmployeeLevel";
			public const string SalaryTableNumber = "SalaryTableNumber";
			public const string EmployeeGradeID = "EmployeeGradeID";
			public const string NoOfDependent = "NoOfDependent";
			public const string IsKWI = "IsKWI";
			public const string SREducationLevel = "SREducationLevel";
			public const string SRFieldLabor = "SRFieldLabor";
			public const string SREducationGroup = "SREducationGroup";
			public const string GradeYear = "GradeYear";
			public const string SalaryScaleID = "SalaryScaleID";
			public const string CoverageClass = "CoverageClass";
			public const string CoverageClassBPJS = "CoverageClassBPJS";
			public const string SRGenderType = "SRGenderType";
			public const string PatientID = "PatientID";
			public const string ResignDate = "ResignDate";
			public const string ResignDateReal = "ResignDateReal";
			public const string SRResignReason = "SRResignReason";
			public const string IsBPJS = "IsBPJS";
			public const string BPJSUncoveredNo = "BPJSUncoveredNo";
			public const string BPJSCoveredNo = "BPJSCoveredNo";
			public const string ServiceMonthThr = "ServiceMonthThr";
			public const string ServiceMonthPPH = "ServiceMonthPPH";
			public const string IsJP = "IsJP";
			public const string AbsenceCardNo = "AbsenceCardNo";
			public const string SubDivisonID = "SubDivisonID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string EmployeeTypePayroll = "EmployeeTypePayroll";
			public const string IsSalaryManaged = "IsSalaryManaged";
			public const string TglDiangkat = "TglDiangkat";
			public const string IjazahPengangkatan = "IjazahPengangkatan";
			public const string IsNpwp = "IsNpwp";
			public const string Npwp = "Npwp";
			public const string PositionValidFromDate = "PositionValidFromDate";
			public const string ProfessionType = "ProfessionType";
			public const string BpjsKesNo = "BpjsKesNo";
			public const string BpjsTkNo = "BpjsTkNo";
			public const string Nik = "Nik";
			public const string SupervisedCount = "SupervisedCount";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonID = "PersonID";
			public const string EmployeeNumber = "EmployeeNumber";
			public const string EmployeeName = "EmployeeName";
			public const string EmployeeRegistrationNo = "EmployeeRegistrationNo";
			public const string ManagerID = "ManagerID";
			public const string SupervisorId = "SupervisorId";
			public const string PreceptorId = "PreceptorId";
			public const string SREmployeeType = "SREmployeeType";
			public const string SRRemunerationType = "SRRemunerationType";
			public const string SREmployeeShiftType = "SREmployeeShiftType";
			public const string SREmployeeScheduleType = "SREmployeeScheduleType";
			public const string SRProfessionType = "SRProfessionType";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SRClinicalWorkArea = "SRClinicalWorkArea";
			public const string SRClinicalAuthorityLevel = "SRClinicalAuthorityLevel";
			public const string OrganizationUnitID = "OrganizationUnitID";
			public const string SubOrganizationUnitID = "SubOrganizationUnitID";
			public const string SRPaymentFrequency = "SRPaymentFrequency";
			public const string SRTaxStatus = "SRTaxStatus";
			public const string SREmployeeStatus = "SREmployeeStatus";
			public const string PositionID = "PositionID";
			public const string SRReligion = "SRReligion";
			public const string SRBloodType = "SRBloodType";
			public const string BankID = "BankID";
			public const string BankNo = "BankNo";
			public const string BankAccountName = "BankAccountName";
			public const string SRIncentiveServiceUnitGroup = "SRIncentiveServiceUnitGroup";
			public const string SRIncentivePositionGroup = "SRIncentivePositionGroup";
			public const string SRIncentivePosition = "SRIncentivePosition";
			public const string IncentivePositionPoints = "IncentivePositionPoints";
			public const string SREmploymentType = "SREmploymentType";
			public const string SREmploymentCategory = "SREmploymentCategory";
			public const string PositionGradeID = "PositionGradeID";
			public const string PositionLevelID = "PositionLevelID";
			public const string SRMaritalStatus = "SRMaritalStatus";
			public const string JoinDate = "JoinDate";
			public const string BirthDate = "BirthDate";
			public const string PlaceBirth = "PlaceBirth";
			public const string ServiceYear = "ServiceYear";
			public const string ServiceYearText = "ServiceYearText";
			public const string ServiceYearPermanent = "ServiceYearPermanent";
			public const string ServiceYearPermanentText = "ServiceYearPermanentText";
			public const string EmployeeLevel = "EmployeeLevel";
			public const string SalaryTableNumber = "SalaryTableNumber";
			public const string EmployeeGradeID = "EmployeeGradeID";
			public const string NoOfDependent = "NoOfDependent";
			public const string IsKWI = "IsKWI";
			public const string SREducationLevel = "SREducationLevel";
			public const string SRFieldLabor = "SRFieldLabor";
			public const string SREducationGroup = "SREducationGroup";
			public const string GradeYear = "GradeYear";
			public const string SalaryScaleID = "SalaryScaleID";
			public const string CoverageClass = "CoverageClass";
			public const string CoverageClassBPJS = "CoverageClassBPJS";
			public const string SRGenderType = "SRGenderType";
			public const string PatientID = "PatientID";
			public const string ResignDate = "ResignDate";
			public const string ResignDateReal = "ResignDateReal";
			public const string SRResignReason = "SRResignReason";
			public const string IsBPJS = "IsBPJS";
			public const string BPJSUncoveredNo = "BPJSUncoveredNo";
			public const string BPJSCoveredNo = "BPJSCoveredNo";
			public const string ServiceMonthThr = "ServiceMonthThr";
			public const string ServiceMonthPPH = "ServiceMonthPPH";
			public const string IsJP = "IsJP";
			public const string AbsenceCardNo = "AbsenceCardNo";
			public const string SubDivisonID = "SubDivisonID";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string EmployeeTypePayroll = "EmployeeTypePayroll";
			public const string IsSalaryManaged = "IsSalaryManaged";
			public const string TglDiangkat = "TglDiangkat";
			public const string IjazahPengangkatan = "IjazahPengangkatan";
			public const string IsNpwp = "IsNpwp";
			public const string Npwp = "Npwp";
			public const string PositionValidFromDate = "PositionValidFromDate";
			public const string ProfessionType = "ProfessionType";
			public const string BpjsKesNo = "BpjsKesNo";
			public const string BpjsTkNo = "BpjsTkNo";
			public const string Nik = "Nik";
			public const string SupervisedCount = "SupervisedCount";
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
			lock (typeof(VwEmployeeTableMetadata))
			{
				if (VwEmployeeTableMetadata.mapDelegates == null)
				{
					VwEmployeeTableMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (VwEmployeeTableMetadata.meta == null)
				{
					VwEmployeeTableMetadata.meta = new VwEmployeeTableMetadata();
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
				meta.AddTypeMap("EmployeeNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ManagerID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SupervisorId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PreceptorId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmployeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRemunerationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeShiftType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeScheduleType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProfessionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProfessionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalWorkArea", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRClinicalAuthorityLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubOrganizationUnitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPaymentFrequency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTaxStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmployeeStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRReligion", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankAccountName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncentiveServiceUnitGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncentivePositionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRIncentivePosition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IncentivePositionPoints", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREmploymentCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionLevelID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JoinDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("BirthDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PlaceBirth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceYear", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ServiceYearText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceYearPermanent", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ServiceYearPermanentText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalaryTableNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NoOfDependent", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsKWI", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRFieldLabor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GradeYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryScaleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CoverageClass", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CoverageClassBPJS", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResignDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ResignDateReal", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRResignReason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsBPJS", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BPJSUncoveredNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("BPJSCoveredNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceMonthThr", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ServiceMonthPPH", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsJP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AbsenceCardNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubDivisonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmployeeTypePayroll", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsSalaryManaged", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TglDiangkat", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IjazahPengangkatan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNpwp", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Npwp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionValidFromDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ProfessionType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BpjsKesNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BpjsTkNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nik", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupervisedCount", new esTypeMap("int", "System.Int32"));


				meta.Source = "Vw_EmployeeTable";
				meta.Destination = "Vw_EmployeeTable";
				meta.spInsert = "proc_Vw_EmployeeTableInsert";
				meta.spUpdate = "proc_Vw_EmployeeTableUpdate";
				meta.spDelete = "proc_Vw_EmployeeTableDelete";
				meta.spLoadAll = "proc_Vw_EmployeeTableLoadAll";
				meta.spLoadByPrimaryKey = "proc_Vw_EmployeeTableLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwEmployeeTableMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
