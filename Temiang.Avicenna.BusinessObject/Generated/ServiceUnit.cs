/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/24/2023 9:56:26 AM
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
	abstract public class esServiceUnitCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ServiceUnitCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esServiceUnitQuery query)
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
			this.InitQuery(query as esServiceUnitQuery);
		}
		#endregion
			
		virtual public ServiceUnit DetachEntity(ServiceUnit entity)
		{
			return base.DetachEntity(entity) as ServiceUnit;
		}
		
		virtual public ServiceUnit AttachEntity(ServiceUnit entity)
		{
			return base.AttachEntity(entity) as ServiceUnit;
		}
		
		virtual public void Combine(ServiceUnitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ServiceUnit this[int index]
		{
			get
			{
				return base[index] as ServiceUnit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnit);
		}
	}

	[Serializable]
	abstract public class esServiceUnit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esServiceUnit()
		{
		}
	
		public esServiceUnit(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String serviceUnitID)
		{
			esServiceUnitQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID",serviceUnitID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "DepartmentID": this.str.DepartmentID = (string)value; break;
						case "ServiceUnitName": this.str.ServiceUnitName = (string)value; break;
						case "ShortName": this.str.ShortName = (string)value; break;
						case "ServiceUnitOfficer": this.str.ServiceUnitOfficer = (string)value; break;
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
						case "IsUsingJobOrder": this.str.IsUsingJobOrder = (string)value; break;
						case "IsPatientTransaction": this.str.IsPatientTransaction = (string)value; break;
						case "IsTransactionEntry": this.str.IsTransactionEntry = (string)value; break;
						case "IsDispensaryUnit": this.str.IsDispensaryUnit = (string)value; break;
						case "IsPurchasingUnit": this.str.IsPurchasingUnit = (string)value; break;
						case "IsGenerateMedicalNo": this.str.IsGenerateMedicalNo = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "ChartOfAccountIdIncome": this.str.ChartOfAccountIdIncome = (string)value; break;
						case "SubledgerIdIncome": this.str.SubledgerIdIncome = (string)value; break;
						case "ChartOfAccountIdAcrual": this.str.ChartOfAccountIdAcrual = (string)value; break;
						case "SubledgerIdAcrual": this.str.SubledgerIdAcrual = (string)value; break;
						case "ChartOfAccountIdDiscount": this.str.ChartOfAccountIdDiscount = (string)value; break;
						case "SubledgerIdDiscount": this.str.SubledgerIdDiscount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ChartOfAccountIdCost": this.str.ChartOfAccountIdCost = (string)value; break;
						case "SubledgerIdCost": this.str.SubledgerIdCost = (string)value; break;
						case "IsDirectPayment": this.str.IsDirectPayment = (string)value; break;
						case "ChartOfAccountIdCostNonMedic": this.str.ChartOfAccountIdCostNonMedic = (string)value; break;
						case "SubledgerIdCostNonMedic": this.str.SubledgerIdCostNonMedic = (string)value; break;
						case "SRGenderType": this.str.SRGenderType = (string)value; break;
						case "ServiceUnitPharmacyID": this.str.ServiceUnitPharmacyID = (string)value; break;
						case "ChartOfAccountIdCostParamedicFee": this.str.ChartOfAccountIdCostParamedicFee = (string)value; break;
						case "SubledgerIdCostParamedicFee": this.str.SubledgerIdCostParamedicFee = (string)value; break;
						case "SRAssessmentType": this.str.SRAssessmentType = (string)value; break;
						case "IsNeedConfirmationOfAttendance": this.str.IsNeedConfirmationOfAttendance = (string)value; break;
						case "SRServiceUnitGroup": this.str.SRServiceUnitGroup = (string)value; break;
						case "LocationPharmacyID": this.str.LocationPharmacyID = (string)value; break;
						case "Email": this.str.Email = (string)value; break;
						case "ServiceUnitPorID": this.str.ServiceUnitPorID = (string)value; break;
						case "LocationPorID": this.str.LocationPorID = (string)value; break;
						case "ChartOfAccountIdPpnIn": this.str.ChartOfAccountIdPpnIn = (string)value; break;
						case "SubledgerIdPpnIn": this.str.SubledgerIdPpnIn = (string)value; break;
						case "IsShowOnKiosk": this.str.IsShowOnKiosk = (string)value; break;
						case "DefaultChargeClassID": this.str.DefaultChargeClassID = (string)value; break;
						case "CustomDisplay": this.str.CustomDisplay = (string)value; break;
						case "IsAllowAccessPatientWithServiceUnitParamedic": this.str.IsAllowAccessPatientWithServiceUnitParamedic = (string)value; break;
						case "MedicalFileFolderColor": this.str.MedicalFileFolderColor = (string)value; break;
						case "SRInpatientClassificationUnit": this.str.SRInpatientClassificationUnit = (string)value; break;
						case "SRBuilding": this.str.SRBuilding = (string)value; break;
						case "SoundFilePath": this.str.SoundFilePath = (string)value; break;
						case "QueueCode": this.str.QueueCode = (string)value; break;
						case "SrqueueinglocationReg": this.str.SrqueueinglocationReg = (string)value; break;
						case "SrqueueinglocationPoli": this.str.SrqueueinglocationPoli = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsUsingJobOrder":
						
							if (value == null || value is System.Boolean)
								this.IsUsingJobOrder = (System.Boolean?)value;
							break;
						case "IsPatientTransaction":
						
							if (value == null || value is System.Boolean)
								this.IsPatientTransaction = (System.Boolean?)value;
							break;
						case "IsTransactionEntry":
						
							if (value == null || value is System.Boolean)
								this.IsTransactionEntry = (System.Boolean?)value;
							break;
						case "IsDispensaryUnit":
						
							if (value == null || value is System.Boolean)
								this.IsDispensaryUnit = (System.Boolean?)value;
							break;
						case "IsPurchasingUnit":
						
							if (value == null || value is System.Boolean)
								this.IsPurchasingUnit = (System.Boolean?)value;
							break;
						case "IsGenerateMedicalNo":
						
							if (value == null || value is System.Boolean)
								this.IsGenerateMedicalNo = (System.Boolean?)value;
							break;
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
							break;
						case "ChartOfAccountIdIncome":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdIncome = (System.Int32?)value;
							break;
						case "SubledgerIdIncome":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdIncome = (System.Int32?)value;
							break;
						case "ChartOfAccountIdAcrual":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdAcrual = (System.Int32?)value;
							break;
						case "SubledgerIdAcrual":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdAcrual = (System.Int32?)value;
							break;
						case "ChartOfAccountIdDiscount":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdDiscount = (System.Int32?)value;
							break;
						case "SubledgerIdDiscount":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdDiscount = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ChartOfAccountIdCost":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCost = (System.Int32?)value;
							break;
						case "SubledgerIdCost":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCost = (System.Int32?)value;
							break;
						case "IsDirectPayment":
						
							if (value == null || value is System.Boolean)
								this.IsDirectPayment = (System.Boolean?)value;
							break;
						case "ChartOfAccountIdCostNonMedic":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCostNonMedic = (System.Int32?)value;
							break;
						case "SubledgerIdCostNonMedic":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCostNonMedic = (System.Int32?)value;
							break;
						case "ChartOfAccountIdCostParamedicFee":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdCostParamedicFee = (System.Int32?)value;
							break;
						case "SubledgerIdCostParamedicFee":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdCostParamedicFee = (System.Int32?)value;
							break;
						case "IsNeedConfirmationOfAttendance":
						
							if (value == null || value is System.Boolean)
								this.IsNeedConfirmationOfAttendance = (System.Boolean?)value;
							break;
						case "ChartOfAccountIdPpnIn":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountIdPpnIn = (System.Int32?)value;
							break;
						case "SubledgerIdPpnIn":
						
							if (value == null || value is System.Int32)
								this.SubledgerIdPpnIn = (System.Int32?)value;
							break;
						case "IsShowOnKiosk":
						
							if (value == null || value is System.Boolean)
								this.IsShowOnKiosk = (System.Boolean?)value;
							break;
						case "IsAllowAccessPatientWithServiceUnitParamedic":
						
							if (value == null || value is System.Boolean)
								this.IsAllowAccessPatientWithServiceUnitParamedic = (System.Boolean?)value;
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
		/// Maps to ServiceUnit.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.DepartmentID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.DepartmentID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ServiceUnitName
		/// </summary>
		virtual public System.String ServiceUnitName
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitName);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitName, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.ShortName);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.ShortName, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ServiceUnitOfficer
		/// </summary>
		virtual public System.String ServiceUnitOfficer
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitOfficer);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitOfficer, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsUsingJobOrder
		/// </summary>
		virtual public System.Boolean? IsUsingJobOrder
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsUsingJobOrder);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsUsingJobOrder, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsPatientTransaction
		/// </summary>
		virtual public System.Boolean? IsPatientTransaction
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsPatientTransaction);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsPatientTransaction, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsTransactionEntry
		/// </summary>
		virtual public System.Boolean? IsTransactionEntry
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsTransactionEntry);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsTransactionEntry, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsDispensaryUnit
		/// </summary>
		virtual public System.Boolean? IsDispensaryUnit
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsDispensaryUnit);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsDispensaryUnit, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsPurchasingUnit
		/// </summary>
		virtual public System.Boolean? IsPurchasingUnit
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsPurchasingUnit);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsPurchasingUnit, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsGenerateMedicalNo
		/// </summary>
		virtual public System.Boolean? IsGenerateMedicalNo
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsGenerateMedicalNo);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsGenerateMedicalNo, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ChartOfAccountIdIncome
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdIncome
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SubledgerIdIncome
		/// </summary>
		virtual public System.Int32? SubledgerIdIncome
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdIncome);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdIncome, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ChartOfAccountIdAcrual
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdAcrual
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdAcrual);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdAcrual, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SubledgerIdAcrual
		/// </summary>
		virtual public System.Int32? SubledgerIdAcrual
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdAcrual);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdAcrual, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ChartOfAccountIdDiscount
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SubledgerIdDiscount
		/// </summary>
		virtual public System.Int32? SubledgerIdDiscount
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdDiscount);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdDiscount, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ServiceUnitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ChartOfAccountIdCost
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCost
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCost);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCost, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SubledgerIdCost
		/// </summary>
		virtual public System.Int32? SubledgerIdCost
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdCost);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdCost, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsDirectPayment
		/// </summary>
		virtual public System.Boolean? IsDirectPayment
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsDirectPayment);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsDirectPayment, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ChartOfAccountIdCostNonMedic
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCostNonMedic
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCostNonMedic);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCostNonMedic, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SubledgerIdCostNonMedic
		/// </summary>
		virtual public System.Int32? SubledgerIdCostNonMedic
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdCostNonMedic);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdCostNonMedic, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SRGenderType);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SRGenderType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ServiceUnitPharmacyID
		/// </summary>
		virtual public System.String ServiceUnitPharmacyID
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitPharmacyID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitPharmacyID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ChartOfAccountIdCostParamedicFee
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdCostParamedicFee
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCostParamedicFee);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCostParamedicFee, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SubledgerIdCostParamedicFee
		/// </summary>
		virtual public System.Int32? SubledgerIdCostParamedicFee
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdCostParamedicFee);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdCostParamedicFee, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SRAssessmentType
		/// </summary>
		virtual public System.String SRAssessmentType
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SRAssessmentType);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SRAssessmentType, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsNeedConfirmationOfAttendance
		/// </summary>
		virtual public System.Boolean? IsNeedConfirmationOfAttendance
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsNeedConfirmationOfAttendance);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsNeedConfirmationOfAttendance, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SRServiceUnitGroup
		/// </summary>
		virtual public System.String SRServiceUnitGroup
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SRServiceUnitGroup);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SRServiceUnitGroup, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.LocationPharmacyID
		/// </summary>
		virtual public System.String LocationPharmacyID
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.LocationPharmacyID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.LocationPharmacyID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.Email, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ServiceUnitPorID
		/// </summary>
		virtual public System.String ServiceUnitPorID
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitPorID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.ServiceUnitPorID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.LocationPorID
		/// </summary>
		virtual public System.String LocationPorID
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.LocationPorID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.LocationPorID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.ChartOfAccountIdPpnIn
		/// </summary>
		virtual public System.Int32? ChartOfAccountIdPpnIn
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdPpnIn);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdPpnIn, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SubledgerIdPpnIn
		/// </summary>
		virtual public System.Int32? SubledgerIdPpnIn
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdPpnIn);
			}
			
			set
			{
				base.SetSystemInt32(ServiceUnitMetadata.ColumnNames.SubledgerIdPpnIn, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsShowOnKiosk
		/// </summary>
		virtual public System.Boolean? IsShowOnKiosk
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsShowOnKiosk);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsShowOnKiosk, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.DefaultChargeClassID
		/// </summary>
		virtual public System.String DefaultChargeClassID
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.DefaultChargeClassID);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.DefaultChargeClassID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.CustomDisplay
		/// </summary>
		virtual public System.String CustomDisplay
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.CustomDisplay);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.CustomDisplay, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.IsAllowAccessPatientWithServiceUnitParamedic
		/// </summary>
		virtual public System.Boolean? IsAllowAccessPatientWithServiceUnitParamedic
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsAllowAccessPatientWithServiceUnitParamedic);
			}
			
			set
			{
				base.SetSystemBoolean(ServiceUnitMetadata.ColumnNames.IsAllowAccessPatientWithServiceUnitParamedic, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.MedicalFileFolderColor
		/// </summary>
		virtual public System.String MedicalFileFolderColor
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.MedicalFileFolderColor);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.MedicalFileFolderColor, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SRInpatientClassificationUnit
		/// </summary>
		virtual public System.String SRInpatientClassificationUnit
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SRInpatientClassificationUnit);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SRInpatientClassificationUnit, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SRBuilding
		/// </summary>
		virtual public System.String SRBuilding
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SRBuilding);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SRBuilding, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SoundFilePath
		/// </summary>
		virtual public System.String SoundFilePath
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SoundFilePath);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SoundFilePath, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.QueueCode
		/// </summary>
		virtual public System.String QueueCode
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.QueueCode);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.QueueCode, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SRQueueingLocation_Reg
		/// </summary>
		virtual public System.String SrqueueinglocationReg
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SrqueueinglocationReg);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SrqueueinglocationReg, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnit.SRQueueingLocation_Poli
		/// </summary>
		virtual public System.String SrqueueinglocationPoli
		{
			get
			{
				return base.GetSystemString(ServiceUnitMetadata.ColumnNames.SrqueueinglocationPoli);
			}
			
			set
			{
				base.SetSystemString(ServiceUnitMetadata.ColumnNames.SrqueueinglocationPoli, value);
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
			public esStrings(esServiceUnit entity)
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
			public System.String ServiceUnitName
			{
				get
				{
					System.String data = entity.ServiceUnitName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitName = null;
					else entity.ServiceUnitName = Convert.ToString(value);
				}
			}
			public System.String ShortName
			{
				get
				{
					System.String data = entity.ShortName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShortName = null;
					else entity.ShortName = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitOfficer
			{
				get
				{
					System.String data = entity.ServiceUnitOfficer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitOfficer = null;
					else entity.ServiceUnitOfficer = Convert.ToString(value);
				}
			}
			public System.String SRRegistrationType
			{
				get
				{
					System.String data = entity.SRRegistrationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRegistrationType = null;
					else entity.SRRegistrationType = Convert.ToString(value);
				}
			}
			public System.String IsUsingJobOrder
			{
				get
				{
					System.Boolean? data = entity.IsUsingJobOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingJobOrder = null;
					else entity.IsUsingJobOrder = Convert.ToBoolean(value);
				}
			}
			public System.String IsPatientTransaction
			{
				get
				{
					System.Boolean? data = entity.IsPatientTransaction;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPatientTransaction = null;
					else entity.IsPatientTransaction = Convert.ToBoolean(value);
				}
			}
			public System.String IsTransactionEntry
			{
				get
				{
					System.Boolean? data = entity.IsTransactionEntry;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTransactionEntry = null;
					else entity.IsTransactionEntry = Convert.ToBoolean(value);
				}
			}
			public System.String IsDispensaryUnit
			{
				get
				{
					System.Boolean? data = entity.IsDispensaryUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDispensaryUnit = null;
					else entity.IsDispensaryUnit = Convert.ToBoolean(value);
				}
			}
			public System.String IsPurchasingUnit
			{
				get
				{
					System.Boolean? data = entity.IsPurchasingUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPurchasingUnit = null;
					else entity.IsPurchasingUnit = Convert.ToBoolean(value);
				}
			}
			public System.String IsGenerateMedicalNo
			{
				get
				{
					System.Boolean? data = entity.IsGenerateMedicalNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsGenerateMedicalNo = null;
					else entity.IsGenerateMedicalNo = Convert.ToBoolean(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountIdIncome
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdIncome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdIncome = null;
					else entity.ChartOfAccountIdIncome = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdIncome
			{
				get
				{
					System.Int32? data = entity.SubledgerIdIncome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdIncome = null;
					else entity.SubledgerIdIncome = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdAcrual
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdAcrual;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdAcrual = null;
					else entity.ChartOfAccountIdAcrual = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdAcrual
			{
				get
				{
					System.Int32? data = entity.SubledgerIdAcrual;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdAcrual = null;
					else entity.SubledgerIdAcrual = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountIdDiscount
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdDiscount = null;
					else entity.ChartOfAccountIdDiscount = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdDiscount
			{
				get
				{
					System.Int32? data = entity.SubledgerIdDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdDiscount = null;
					else entity.SubledgerIdDiscount = Convert.ToInt32(value);
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
			public System.String ChartOfAccountIdCost
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCost = null;
					else entity.ChartOfAccountIdCost = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCost
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCost = null;
					else entity.SubledgerIdCost = Convert.ToInt32(value);
				}
			}
			public System.String IsDirectPayment
			{
				get
				{
					System.Boolean? data = entity.IsDirectPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDirectPayment = null;
					else entity.IsDirectPayment = Convert.ToBoolean(value);
				}
			}
			public System.String ChartOfAccountIdCostNonMedic
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCostNonMedic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCostNonMedic = null;
					else entity.ChartOfAccountIdCostNonMedic = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCostNonMedic
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCostNonMedic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCostNonMedic = null;
					else entity.SubledgerIdCostNonMedic = Convert.ToInt32(value);
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
			public System.String ServiceUnitPharmacyID
			{
				get
				{
					System.String data = entity.ServiceUnitPharmacyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitPharmacyID = null;
					else entity.ServiceUnitPharmacyID = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountIdCostParamedicFee
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdCostParamedicFee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdCostParamedicFee = null;
					else entity.ChartOfAccountIdCostParamedicFee = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdCostParamedicFee
			{
				get
				{
					System.Int32? data = entity.SubledgerIdCostParamedicFee;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdCostParamedicFee = null;
					else entity.SubledgerIdCostParamedicFee = Convert.ToInt32(value);
				}
			}
			public System.String SRAssessmentType
			{
				get
				{
					System.String data = entity.SRAssessmentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAssessmentType = null;
					else entity.SRAssessmentType = Convert.ToString(value);
				}
			}
			public System.String IsNeedConfirmationOfAttendance
			{
				get
				{
					System.Boolean? data = entity.IsNeedConfirmationOfAttendance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNeedConfirmationOfAttendance = null;
					else entity.IsNeedConfirmationOfAttendance = Convert.ToBoolean(value);
				}
			}
			public System.String SRServiceUnitGroup
			{
				get
				{
					System.String data = entity.SRServiceUnitGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRServiceUnitGroup = null;
					else entity.SRServiceUnitGroup = Convert.ToString(value);
				}
			}
			public System.String LocationPharmacyID
			{
				get
				{
					System.String data = entity.LocationPharmacyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationPharmacyID = null;
					else entity.LocationPharmacyID = Convert.ToString(value);
				}
			}
			public System.String Email
			{
				get
				{
					System.String data = entity.Email;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Email = null;
					else entity.Email = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitPorID
			{
				get
				{
					System.String data = entity.ServiceUnitPorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitPorID = null;
					else entity.ServiceUnitPorID = Convert.ToString(value);
				}
			}
			public System.String LocationPorID
			{
				get
				{
					System.String data = entity.LocationPorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationPorID = null;
					else entity.LocationPorID = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountIdPpnIn
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountIdPpnIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountIdPpnIn = null;
					else entity.ChartOfAccountIdPpnIn = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerIdPpnIn
			{
				get
				{
					System.Int32? data = entity.SubledgerIdPpnIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerIdPpnIn = null;
					else entity.SubledgerIdPpnIn = Convert.ToInt32(value);
				}
			}
			public System.String IsShowOnKiosk
			{
				get
				{
					System.Boolean? data = entity.IsShowOnKiosk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShowOnKiosk = null;
					else entity.IsShowOnKiosk = Convert.ToBoolean(value);
				}
			}
			public System.String DefaultChargeClassID
			{
				get
				{
					System.String data = entity.DefaultChargeClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DefaultChargeClassID = null;
					else entity.DefaultChargeClassID = Convert.ToString(value);
				}
			}
			public System.String CustomDisplay
			{
				get
				{
					System.String data = entity.CustomDisplay;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomDisplay = null;
					else entity.CustomDisplay = Convert.ToString(value);
				}
			}
			public System.String IsAllowAccessPatientWithServiceUnitParamedic
			{
				get
				{
					System.Boolean? data = entity.IsAllowAccessPatientWithServiceUnitParamedic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowAccessPatientWithServiceUnitParamedic = null;
					else entity.IsAllowAccessPatientWithServiceUnitParamedic = Convert.ToBoolean(value);
				}
			}
			public System.String MedicalFileFolderColor
			{
				get
				{
					System.String data = entity.MedicalFileFolderColor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalFileFolderColor = null;
					else entity.MedicalFileFolderColor = Convert.ToString(value);
				}
			}
			public System.String SRInpatientClassificationUnit
			{
				get
				{
					System.String data = entity.SRInpatientClassificationUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRInpatientClassificationUnit = null;
					else entity.SRInpatientClassificationUnit = Convert.ToString(value);
				}
			}
			public System.String SRBuilding
			{
				get
				{
					System.String data = entity.SRBuilding;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBuilding = null;
					else entity.SRBuilding = Convert.ToString(value);
				}
			}
			public System.String SoundFilePath
			{
				get
				{
					System.String data = entity.SoundFilePath;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SoundFilePath = null;
					else entity.SoundFilePath = Convert.ToString(value);
				}
			}
			public System.String QueueCode
			{
				get
				{
					System.String data = entity.QueueCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QueueCode = null;
					else entity.QueueCode = Convert.ToString(value);
				}
			}
			public System.String SrqueueinglocationReg
			{
				get
				{
					System.String data = entity.SrqueueinglocationReg;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SrqueueinglocationReg = null;
					else entity.SrqueueinglocationReg = Convert.ToString(value);
				}
			}
			public System.String SrqueueinglocationPoli
			{
				get
				{
					System.String data = entity.SrqueueinglocationPoli;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SrqueueinglocationPoli = null;
					else entity.SrqueueinglocationPoli = Convert.ToString(value);
				}
			}
			private esServiceUnit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitQuery query)
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
				throw new Exception("esServiceUnit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceUnit : esServiceUnit
	{	
	}

	[Serializable]
	abstract public class esServiceUnitQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitMetadata.Meta();
			}
		}	
			
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
			
		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitName
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ServiceUnitName, esSystemType.String);
			}
		} 
			
		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitOfficer
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ServiceUnitOfficer, esSystemType.String);
			}
		} 
			
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsUsingJobOrder
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsUsingJobOrder, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPatientTransaction
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsPatientTransaction, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsTransactionEntry
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsTransactionEntry, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDispensaryUnit
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsDispensaryUnit, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPurchasingUnit
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsPurchasingUnit, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsGenerateMedicalNo
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsGenerateMedicalNo, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ChartOfAccountIdIncome
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ChartOfAccountIdIncome, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdIncome
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SubledgerIdIncome, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdAcrual
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ChartOfAccountIdAcrual, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdAcrual
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SubledgerIdAcrual, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountIdDiscount
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ChartOfAccountIdDiscount, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdDiscount
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SubledgerIdDiscount, esSystemType.Int32);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCost
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCost, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCost
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SubledgerIdCost, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsDirectPayment
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsDirectPayment, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCostNonMedic
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCostNonMedic, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCostNonMedic
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SubledgerIdCostNonMedic, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitPharmacyID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ServiceUnitPharmacyID, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountIdCostParamedicFee
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCostParamedicFee, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdCostParamedicFee
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SubledgerIdCostParamedicFee, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SRAssessmentType
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SRAssessmentType, esSystemType.String);
			}
		} 
			
		public esQueryItem IsNeedConfirmationOfAttendance
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsNeedConfirmationOfAttendance, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem SRServiceUnitGroup
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SRServiceUnitGroup, esSystemType.String);
			}
		} 
			
		public esQueryItem LocationPharmacyID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.LocationPharmacyID, esSystemType.String);
			}
		} 
			
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
			
		public esQueryItem ServiceUnitPorID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ServiceUnitPorID, esSystemType.String);
			}
		} 
			
		public esQueryItem LocationPorID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.LocationPorID, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountIdPpnIn
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.ChartOfAccountIdPpnIn, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerIdPpnIn
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SubledgerIdPpnIn, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsShowOnKiosk
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsShowOnKiosk, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem DefaultChargeClassID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.DefaultChargeClassID, esSystemType.String);
			}
		} 
			
		public esQueryItem CustomDisplay
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.CustomDisplay, esSystemType.String);
			}
		} 
			
		public esQueryItem IsAllowAccessPatientWithServiceUnitParamedic
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.IsAllowAccessPatientWithServiceUnitParamedic, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem MedicalFileFolderColor
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.MedicalFileFolderColor, esSystemType.String);
			}
		} 
			
		public esQueryItem SRInpatientClassificationUnit
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SRInpatientClassificationUnit, esSystemType.String);
			}
		} 
			
		public esQueryItem SRBuilding
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SRBuilding, esSystemType.String);
			}
		} 
			
		public esQueryItem SoundFilePath
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SoundFilePath, esSystemType.String);
			}
		} 
			
		public esQueryItem QueueCode
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.QueueCode, esSystemType.String);
			}
		} 
			
		public esQueryItem SrqueueinglocationReg
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SrqueueinglocationReg, esSystemType.String);
			}
		} 
			
		public esQueryItem SrqueueinglocationPoli
		{
			get
			{
				return new esQueryItem(this, ServiceUnitMetadata.ColumnNames.SrqueueinglocationPoli, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitCollection")]
	public partial class ServiceUnitCollection : esServiceUnitCollection, IEnumerable< ServiceUnit>
	{
		public ServiceUnitCollection()
		{

		}	
		
		public static implicit operator List< ServiceUnit>(ServiceUnitCollection coll)
		{
			List< ServiceUnit> list = new List< ServiceUnit>();
			
			foreach (ServiceUnit emp in coll)
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
				return  ServiceUnitMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnit();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitQuery();
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
		public bool Load(ServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceUnit AddNew()
		{
			ServiceUnit entity = base.AddNewEntity() as ServiceUnit;
			
			return entity;		
		}
		public ServiceUnit FindByPrimaryKey(String serviceUnitID)
		{
			return base.FindByPrimaryKey(serviceUnitID) as ServiceUnit;
		}

		#region IEnumerable< ServiceUnit> Members

		IEnumerator< ServiceUnit> IEnumerable< ServiceUnit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnit;
			}
		}

		#endregion
		
		private ServiceUnitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnit' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceUnit ({ServiceUnitID})")]
	[Serializable]
	public partial class ServiceUnit : esServiceUnit
	{
		public ServiceUnit()
		{
		}	
	
		public ServiceUnit(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitMetadata.Meta();
			}
		}	
	
		override protected esServiceUnitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitQuery();
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
		public bool Load(ServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ServiceUnitQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceUnitQuery : esServiceUnitQuery
	{
		public ServiceUnitQuery()
		{

		}		
		
		public ServiceUnitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ServiceUnitQuery";
        }
	}

	[Serializable]
	public partial class ServiceUnitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.DepartmentID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ServiceUnitName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ServiceUnitName;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ShortName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 35;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ServiceUnitOfficer, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ServiceUnitOfficer;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SRRegistrationType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsUsingJobOrder, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsUsingJobOrder;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsPatientTransaction, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsPatientTransaction;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsTransactionEntry, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsTransactionEntry;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsDispensaryUnit, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsDispensaryUnit;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsPurchasingUnit, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsPurchasingUnit;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsGenerateMedicalNo, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsGenerateMedicalNo;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsActive, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdIncome, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ChartOfAccountIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SubledgerIdIncome, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SubledgerIdIncome;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdAcrual, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ChartOfAccountIdAcrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SubledgerIdAcrual, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SubledgerIdAcrual;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdDiscount, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ChartOfAccountIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SubledgerIdDiscount, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SubledgerIdDiscount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.LastUpdateDateTime, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.LastUpdateByUserID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCost, 21, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ChartOfAccountIdCost;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SubledgerIdCost, 22, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SubledgerIdCost;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsDirectPayment, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsDirectPayment;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCostNonMedic, 24, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ChartOfAccountIdCostNonMedic;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SubledgerIdCostNonMedic, 25, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SubledgerIdCostNonMedic;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SRGenderType, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ServiceUnitPharmacyID, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ServiceUnitPharmacyID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdCostParamedicFee, 28, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ChartOfAccountIdCostParamedicFee;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SubledgerIdCostParamedicFee, 29, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SubledgerIdCostParamedicFee;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SRAssessmentType, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SRAssessmentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsNeedConfirmationOfAttendance, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsNeedConfirmationOfAttendance;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SRServiceUnitGroup, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SRServiceUnitGroup;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.LocationPharmacyID, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.LocationPharmacyID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.Email, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ServiceUnitPorID, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ServiceUnitPorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.LocationPorID, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.LocationPorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.ChartOfAccountIdPpnIn, 37, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.ChartOfAccountIdPpnIn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SubledgerIdPpnIn, 38, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SubledgerIdPpnIn;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsShowOnKiosk, 39, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsShowOnKiosk;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.DefaultChargeClassID, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.DefaultChargeClassID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.CustomDisplay, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.CustomDisplay;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.IsAllowAccessPatientWithServiceUnitParamedic, 42, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.IsAllowAccessPatientWithServiceUnitParamedic;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.MedicalFileFolderColor, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.MedicalFileFolderColor;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SRInpatientClassificationUnit, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SRInpatientClassificationUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SRBuilding, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SRBuilding;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SoundFilePath, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SoundFilePath;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.QueueCode, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.QueueCode;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SrqueueinglocationReg, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SrqueueinglocationReg;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ServiceUnitMetadata.ColumnNames.SrqueueinglocationPoli, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitMetadata.PropertyNames.SrqueueinglocationPoli;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ServiceUnitMetadata Meta()
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
			public const string ServiceUnitID = "ServiceUnitID";
			public const string DepartmentID = "DepartmentID";
			public const string ServiceUnitName = "ServiceUnitName";
			public const string ShortName = "ShortName";
			public const string ServiceUnitOfficer = "ServiceUnitOfficer";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string IsUsingJobOrder = "IsUsingJobOrder";
			public const string IsPatientTransaction = "IsPatientTransaction";
			public const string IsTransactionEntry = "IsTransactionEntry";
			public const string IsDispensaryUnit = "IsDispensaryUnit";
			public const string IsPurchasingUnit = "IsPurchasingUnit";
			public const string IsGenerateMedicalNo = "IsGenerateMedicalNo";
			public const string IsActive = "IsActive";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdAcrual = "ChartOfAccountIdAcrual";
			public const string SubledgerIdAcrual = "SubledgerIdAcrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
			public const string SubledgerIdCost = "SubledgerIdCost";
			public const string IsDirectPayment = "IsDirectPayment";
			public const string ChartOfAccountIdCostNonMedic = "ChartOfAccountIdCostNonMedic";
			public const string SubledgerIdCostNonMedic = "SubledgerIdCostNonMedic";
			public const string SRGenderType = "SRGenderType";
			public const string ServiceUnitPharmacyID = "ServiceUnitPharmacyID";
			public const string ChartOfAccountIdCostParamedicFee = "ChartOfAccountIdCostParamedicFee";
			public const string SubledgerIdCostParamedicFee = "SubledgerIdCostParamedicFee";
			public const string SRAssessmentType = "SRAssessmentType";
			public const string IsNeedConfirmationOfAttendance = "IsNeedConfirmationOfAttendance";
			public const string SRServiceUnitGroup = "SRServiceUnitGroup";
			public const string LocationPharmacyID = "LocationPharmacyID";
			public const string Email = "Email";
			public const string ServiceUnitPorID = "ServiceUnitPorID";
			public const string LocationPorID = "LocationPorID";
			public const string ChartOfAccountIdPpnIn = "ChartOfAccountIdPpnIn";
			public const string SubledgerIdPpnIn = "SubledgerIdPpnIn";
			public const string IsShowOnKiosk = "IsShowOnKiosk";
			public const string DefaultChargeClassID = "DefaultChargeClassID";
			public const string CustomDisplay = "CustomDisplay";
			public const string IsAllowAccessPatientWithServiceUnitParamedic = "IsAllowAccessPatientWithServiceUnitParamedic";
			public const string MedicalFileFolderColor = "MedicalFileFolderColor";
			public const string SRInpatientClassificationUnit = "SRInpatientClassificationUnit";
			public const string SRBuilding = "SRBuilding";
			public const string SoundFilePath = "SoundFilePath";
			public const string QueueCode = "QueueCode";
			public const string SrqueueinglocationReg = "SRQueueingLocation_Reg";
			public const string SrqueueinglocationPoli = "SRQueueingLocation_Poli";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string ServiceUnitID = "ServiceUnitID";
			public const string DepartmentID = "DepartmentID";
			public const string ServiceUnitName = "ServiceUnitName";
			public const string ShortName = "ShortName";
			public const string ServiceUnitOfficer = "ServiceUnitOfficer";
			public const string SRRegistrationType = "SRRegistrationType";
			public const string IsUsingJobOrder = "IsUsingJobOrder";
			public const string IsPatientTransaction = "IsPatientTransaction";
			public const string IsTransactionEntry = "IsTransactionEntry";
			public const string IsDispensaryUnit = "IsDispensaryUnit";
			public const string IsPurchasingUnit = "IsPurchasingUnit";
			public const string IsGenerateMedicalNo = "IsGenerateMedicalNo";
			public const string IsActive = "IsActive";
			public const string ChartOfAccountIdIncome = "ChartOfAccountIdIncome";
			public const string SubledgerIdIncome = "SubledgerIdIncome";
			public const string ChartOfAccountIdAcrual = "ChartOfAccountIdAcrual";
			public const string SubledgerIdAcrual = "SubledgerIdAcrual";
			public const string ChartOfAccountIdDiscount = "ChartOfAccountIdDiscount";
			public const string SubledgerIdDiscount = "SubledgerIdDiscount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ChartOfAccountIdCost = "ChartOfAccountIdCost";
			public const string SubledgerIdCost = "SubledgerIdCost";
			public const string IsDirectPayment = "IsDirectPayment";
			public const string ChartOfAccountIdCostNonMedic = "ChartOfAccountIdCostNonMedic";
			public const string SubledgerIdCostNonMedic = "SubledgerIdCostNonMedic";
			public const string SRGenderType = "SRGenderType";
			public const string ServiceUnitPharmacyID = "ServiceUnitPharmacyID";
			public const string ChartOfAccountIdCostParamedicFee = "ChartOfAccountIdCostParamedicFee";
			public const string SubledgerIdCostParamedicFee = "SubledgerIdCostParamedicFee";
			public const string SRAssessmentType = "SRAssessmentType";
			public const string IsNeedConfirmationOfAttendance = "IsNeedConfirmationOfAttendance";
			public const string SRServiceUnitGroup = "SRServiceUnitGroup";
			public const string LocationPharmacyID = "LocationPharmacyID";
			public const string Email = "Email";
			public const string ServiceUnitPorID = "ServiceUnitPorID";
			public const string LocationPorID = "LocationPorID";
			public const string ChartOfAccountIdPpnIn = "ChartOfAccountIdPpnIn";
			public const string SubledgerIdPpnIn = "SubledgerIdPpnIn";
			public const string IsShowOnKiosk = "IsShowOnKiosk";
			public const string DefaultChargeClassID = "DefaultChargeClassID";
			public const string CustomDisplay = "CustomDisplay";
			public const string IsAllowAccessPatientWithServiceUnitParamedic = "IsAllowAccessPatientWithServiceUnitParamedic";
			public const string MedicalFileFolderColor = "MedicalFileFolderColor";
			public const string SRInpatientClassificationUnit = "SRInpatientClassificationUnit";
			public const string SRBuilding = "SRBuilding";
			public const string SoundFilePath = "SoundFilePath";
			public const string QueueCode = "QueueCode";
			public const string SrqueueinglocationReg = "SrqueueinglocationReg";
			public const string SrqueueinglocationPoli = "SrqueueinglocationPoli";
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
			lock (typeof(ServiceUnitMetadata))
			{
				if(ServiceUnitMetadata.mapDelegates == null)
				{
					ServiceUnitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ServiceUnitMetadata.meta == null)
				{
					ServiceUnitMetadata.meta = new ServiceUnitMetadata();
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
				
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitOfficer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsingJobOrder", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPatientTransaction", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTransactionEntry", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDispensaryUnit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPurchasingUnit", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsGenerateMedicalNo", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdIncome", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdAcrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdAcrual", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdDiscount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdCost", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCost", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsDirectPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ChartOfAccountIdCostNonMedic", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCostNonMedic", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitPharmacyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdCostParamedicFee", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdCostParamedicFee", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRAssessmentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsNeedConfirmationOfAttendance", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRServiceUnitGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationPharmacyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitPorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocationPorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountIdPpnIn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerIdPpnIn", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsShowOnKiosk", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DefaultChargeClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CustomDisplay", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAllowAccessPatientWithServiceUnitParamedic", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("MedicalFileFolderColor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRInpatientClassificationUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBuilding", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SoundFilePath", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QueueCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SrqueueinglocationReg", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SrqueueinglocationPoli", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "ServiceUnit";
				meta.Destination = "ServiceUnit";
				meta.spInsert = "proc_ServiceUnitInsert";				
				meta.spUpdate = "proc_ServiceUnitUpdate";		
				meta.spDelete = "proc_ServiceUnitDelete";
				meta.spLoadAll = "proc_ServiceUnitLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
