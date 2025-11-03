/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 12/14/19 11:43:17 PM
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
	abstract public class esMedicalDischargeSummaryByNurseCollection : esEntityCollectionWAuditLog
	{
		public esMedicalDischargeSummaryByNurseCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "MedicalDischargeSummaryByNurseCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esMedicalDischargeSummaryByNurseQuery query)
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
			this.InitQuery(query as esMedicalDischargeSummaryByNurseQuery);
		}
		#endregion
			
		virtual public MedicalDischargeSummaryByNurse DetachEntity(MedicalDischargeSummaryByNurse entity)
		{
			return base.DetachEntity(entity) as MedicalDischargeSummaryByNurse;
		}
		
		virtual public MedicalDischargeSummaryByNurse AttachEntity(MedicalDischargeSummaryByNurse entity)
		{
			return base.AttachEntity(entity) as MedicalDischargeSummaryByNurse;
		}
		
		virtual public void Combine(MedicalDischargeSummaryByNurseCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalDischargeSummaryByNurse this[int index]
		{
			get
			{
				return base[index] as MedicalDischargeSummaryByNurse;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalDischargeSummaryByNurse);
		}
	}

	[Serializable]
	abstract public class esMedicalDischargeSummaryByNurse : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalDischargeSummaryByNurseQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esMedicalDischargeSummaryByNurse()
		{
		}
	
		public esMedicalDischargeSummaryByNurse(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo)
		{
			esMedicalDischargeSummaryByNurseQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Temp": this.str.Temp = (string)value; break;
						case "Pulse": this.str.Pulse = (string)value; break;
						case "Respiratory": this.str.Respiratory = (string)value; break;
						case "BloodPress": this.str.BloodPress = (string)value; break;
						case "DietType": this.str.DietType = (string)value; break;
						case "SpecialDietNote": this.str.SpecialDietNote = (string)value; break;
						case "DietLiquidLimitNote": this.str.DietLiquidLimitNote = (string)value; break;
						case "DefecateType": this.str.DefecateType = (string)value; break;
						case "UrinateType": this.str.UrinateType = (string)value; break;
						case "CatheterLastDate": this.str.CatheterLastDate = (string)value; break;
						case "UterineType": this.str.UterineType = (string)value; break;
						case "UterineHeight": this.str.UterineHeight = (string)value; break;
						case "VulvaType": this.str.VulvaType = (string)value; break;
						case "PerinealWoundType": this.str.PerinealWoundType = (string)value; break;
						case "LocheaType": this.str.LocheaType = (string)value; break;
						case "LocheaColor": this.str.LocheaColor = (string)value; break;
						case "LocheaSmell": this.str.LocheaSmell = (string)value; break;
						case "OperationWoundType": this.str.OperationWoundType = (string)value; break;
						case "TransferMobilizationType": this.str.TransferMobilizationType = (string)value; break;
						case "PartiallyAssisted": this.str.PartiallyAssisted = (string)value; break;
						case "AssistToolType": this.str.AssistToolType = (string)value; break;
						case "Education": this.str.Education = (string)value; break;
						case "EducationOthers": this.str.EducationOthers = (string)value; break;
						case "TreatmentDiag01": this.str.TreatmentDiag01 = (string)value; break;
						case "TreatmentDiag02": this.str.TreatmentDiag02 = (string)value; break;
						case "TreatmentDiag03": this.str.TreatmentDiag03 = (string)value; break;
						case "TreatmentDiag04": this.str.TreatmentDiag04 = (string)value; break;
						case "DischargeDiag01": this.str.DischargeDiag01 = (string)value; break;
						case "DischargeDiag02": this.str.DischargeDiag02 = (string)value; break;
						case "DischargeDiag03": this.str.DischargeDiag03 = (string)value; break;
						case "DischargeDiag04": this.str.DischargeDiag04 = (string)value; break;
						case "DrugsTaken": this.str.DrugsTaken = (string)value; break;
						case "PossibleEffect": this.str.PossibleEffect = (string)value; break;
						case "HospitalRefer": this.str.HospitalRefer = (string)value; break;
						case "LabResultSheet": this.str.LabResultSheet = (string)value; break;
						case "XRaysSheet": this.str.XRaysSheet = (string)value; break;
						case "CTScanSheet": this.str.CTScanSheet = (string)value; break;
						case "MriMraSheet": this.str.MriMraSheet = (string)value; break;
						case "UsgResultSheet": this.str.UsgResultSheet = (string)value; break;
						case "IsCertIllnes": this.str.IsCertIllnes = (string)value; break;
						case "IsInsLetter": this.str.IsInsLetter = (string)value; break;
						case "IsDischSummaryLetter": this.str.IsDischSummaryLetter = (string)value; break;
						case "IsBabyBook": this.str.IsBabyBook = (string)value; break;
						case "IsBabyBloodType": this.str.IsBabyBloodType = (string)value; break;
						case "IsCertBirth": this.str.IsCertBirth = (string)value; break;
						case "HandedBy": this.str.HandedBy = (string)value; break;
						case "OtherLetter": this.str.OtherLetter = (string)value; break;
						case "ControlPlan": this.str.ControlPlan = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "CatheterLastDate":
						
							if (value == null || value is System.DateTime)
								this.CatheterLastDate = (System.DateTime?)value;
							break;
						case "UterineHeight":
						
							if (value == null || value is System.Int32)
								this.UterineHeight = (System.Int32?)value;
							break;
						case "LabResultSheet":
						
							if (value == null || value is System.Int32)
								this.LabResultSheet = (System.Int32?)value;
							break;
						case "XRaysSheet":
						
							if (value == null || value is System.Int32)
								this.XRaysSheet = (System.Int32?)value;
							break;
						case "CTScanSheet":
						
							if (value == null || value is System.Int32)
								this.CTScanSheet = (System.Int32?)value;
							break;
						case "MriMraSheet":
						
							if (value == null || value is System.Int32)
								this.MriMraSheet = (System.Int32?)value;
							break;
						case "UsgResultSheet":
						
							if (value == null || value is System.Int32)
								this.UsgResultSheet = (System.Int32?)value;
							break;
						case "IsCertIllnes":
						
							if (value == null || value is System.Boolean)
								this.IsCertIllnes = (System.Boolean?)value;
							break;
						case "IsInsLetter":
						
							if (value == null || value is System.Boolean)
								this.IsInsLetter = (System.Boolean?)value;
							break;
						case "IsDischSummaryLetter":
						
							if (value == null || value is System.Boolean)
								this.IsDischSummaryLetter = (System.Boolean?)value;
							break;
						case "IsBabyBook":
						
							if (value == null || value is System.Boolean)
								this.IsBabyBook = (System.Boolean?)value;
							break;
						case "IsBabyBloodType":
						
							if (value == null || value is System.Boolean)
								this.IsBabyBloodType = (System.Boolean?)value;
							break;
						case "IsCertBirth":
						
							if (value == null || value is System.Boolean)
								this.IsCertBirth = (System.Boolean?)value;
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
		/// Maps to MedicalDischargeSummaryByNurse.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.Temp
		/// </summary>
		virtual public System.String Temp
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Temp);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Temp, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.Pulse
		/// </summary>
		virtual public System.String Pulse
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Pulse);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Pulse, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.Respiratory
		/// </summary>
		virtual public System.String Respiratory
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Respiratory);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Respiratory, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.BloodPress
		/// </summary>
		virtual public System.String BloodPress
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.BloodPress);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.BloodPress, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.DietType
		/// </summary>
		virtual public System.String DietType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DietType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DietType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.SpecialDietNote
		/// </summary>
		virtual public System.String SpecialDietNote
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.SpecialDietNote);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.SpecialDietNote, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.DietLiquidLimitNote
		/// </summary>
		virtual public System.String DietLiquidLimitNote
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DietLiquidLimitNote);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DietLiquidLimitNote, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.DefecateType
		/// </summary>
		virtual public System.String DefecateType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DefecateType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DefecateType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.UrinateType
		/// </summary>
		virtual public System.String UrinateType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UrinateType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UrinateType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.CatheterLastDate
		/// </summary>
		virtual public System.DateTime? CatheterLastDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalDischargeSummaryByNurseMetadata.ColumnNames.CatheterLastDate);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalDischargeSummaryByNurseMetadata.ColumnNames.CatheterLastDate, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.UterineType
		/// </summary>
		virtual public System.String UterineType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UterineType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UterineType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.UterineHeight
		/// </summary>
		virtual public System.Int32? UterineHeight
		{
			get
			{
				return base.GetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UterineHeight);
			}
			
			set
			{
				base.SetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UterineHeight, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.VulvaType
		/// </summary>
		virtual public System.String VulvaType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.VulvaType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.VulvaType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.PerinealWoundType
		/// </summary>
		virtual public System.String PerinealWoundType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PerinealWoundType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PerinealWoundType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.LocheaType
		/// </summary>
		virtual public System.String LocheaType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.LocheaColor
		/// </summary>
		virtual public System.String LocheaColor
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaColor);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaColor, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.LocheaSmell
		/// </summary>
		virtual public System.String LocheaSmell
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaSmell);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaSmell, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.OperationWoundType
		/// </summary>
		virtual public System.String OperationWoundType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.OperationWoundType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.OperationWoundType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.TransferMobilizationType
		/// </summary>
		virtual public System.String TransferMobilizationType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TransferMobilizationType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TransferMobilizationType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.PartiallyAssisted
		/// </summary>
		virtual public System.String PartiallyAssisted
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PartiallyAssisted);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PartiallyAssisted, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.AssistToolType
		/// </summary>
		virtual public System.String AssistToolType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.AssistToolType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.AssistToolType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.Education
		/// </summary>
		virtual public System.String Education
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Education);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Education, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.EducationOthers
		/// </summary>
		virtual public System.String EducationOthers
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.EducationOthers);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.EducationOthers, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.TreatmentDiag01
		/// </summary>
		virtual public System.String TreatmentDiag01
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag01);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag01, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.TreatmentDiag02
		/// </summary>
		virtual public System.String TreatmentDiag02
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag02);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag02, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.TreatmentDiag03
		/// </summary>
		virtual public System.String TreatmentDiag03
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag03);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag03, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.TreatmentDiag04
		/// </summary>
		virtual public System.String TreatmentDiag04
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag04);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag04, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.DischargeDiag01
		/// </summary>
		virtual public System.String DischargeDiag01
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag01);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag01, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.DischargeDiag02
		/// </summary>
		virtual public System.String DischargeDiag02
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag02);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag02, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.DischargeDiag03
		/// </summary>
		virtual public System.String DischargeDiag03
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag03);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag03, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.DischargeDiag04
		/// </summary>
		virtual public System.String DischargeDiag04
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag04);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag04, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.DrugsTaken
		/// </summary>
		virtual public System.String DrugsTaken
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DrugsTaken);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DrugsTaken, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.PossibleEffect
		/// </summary>
		virtual public System.String PossibleEffect
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PossibleEffect);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PossibleEffect, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.HospitalRefer
		/// </summary>
		virtual public System.String HospitalRefer
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.HospitalRefer);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.HospitalRefer, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.LabResultSheet
		/// </summary>
		virtual public System.Int32? LabResultSheet
		{
			get
			{
				return base.GetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LabResultSheet);
			}
			
			set
			{
				base.SetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LabResultSheet, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.XRaysSheet
		/// </summary>
		virtual public System.Int32? XRaysSheet
		{
			get
			{
				return base.GetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.XRaysSheet);
			}
			
			set
			{
				base.SetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.XRaysSheet, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.CTScanSheet
		/// </summary>
		virtual public System.Int32? CTScanSheet
		{
			get
			{
				return base.GetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.CTScanSheet);
			}
			
			set
			{
				base.SetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.CTScanSheet, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.MriMraSheet
		/// </summary>
		virtual public System.Int32? MriMraSheet
		{
			get
			{
				return base.GetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.MriMraSheet);
			}
			
			set
			{
				base.SetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.MriMraSheet, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.UsgResultSheet
		/// </summary>
		virtual public System.Int32? UsgResultSheet
		{
			get
			{
				return base.GetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UsgResultSheet);
			}
			
			set
			{
				base.SetSystemInt32(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UsgResultSheet, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.IsCertIllnes
		/// </summary>
		virtual public System.Boolean? IsCertIllnes
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsCertIllnes);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsCertIllnes, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.IsInsLetter
		/// </summary>
		virtual public System.Boolean? IsInsLetter
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsInsLetter);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsInsLetter, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.IsDischSummaryLetter
		/// </summary>
		virtual public System.Boolean? IsDischSummaryLetter
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsDischSummaryLetter);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsDischSummaryLetter, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.IsBabyBook
		/// </summary>
		virtual public System.Boolean? IsBabyBook
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsBabyBook);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsBabyBook, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.IsBabyBloodType
		/// </summary>
		virtual public System.Boolean? IsBabyBloodType
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsBabyBloodType);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsBabyBloodType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.IsCertBirth
		/// </summary>
		virtual public System.Boolean? IsCertBirth
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsCertBirth);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsCertBirth, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.HandedBy
		/// </summary>
		virtual public System.String HandedBy
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.HandedBy);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.HandedBy, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.OtherLetter
		/// </summary>
		virtual public System.String OtherLetter
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.OtherLetter);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.OtherLetter, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryByNurse.ControlPlan
		/// </summary>
		virtual public System.String ControlPlan
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.ControlPlan);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryByNurseMetadata.ColumnNames.ControlPlan, value);
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
			public esStrings(esMedicalDischargeSummaryByNurse entity)
			{
				this.entity = entity;
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
			public System.String Temp
			{
				get
				{
					System.String data = entity.Temp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temp = null;
					else entity.Temp = Convert.ToString(value);
				}
			}
			public System.String Pulse
			{
				get
				{
					System.String data = entity.Pulse;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse = null;
					else entity.Pulse = Convert.ToString(value);
				}
			}
			public System.String Respiratory
			{
				get
				{
					System.String data = entity.Respiratory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory = null;
					else entity.Respiratory = Convert.ToString(value);
				}
			}
			public System.String BloodPress
			{
				get
				{
					System.String data = entity.BloodPress;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodPress = null;
					else entity.BloodPress = Convert.ToString(value);
				}
			}
			public System.String DietType
			{
				get
				{
					System.String data = entity.DietType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DietType = null;
					else entity.DietType = Convert.ToString(value);
				}
			}
			public System.String SpecialDietNote
			{
				get
				{
					System.String data = entity.SpecialDietNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialDietNote = null;
					else entity.SpecialDietNote = Convert.ToString(value);
				}
			}
			public System.String DietLiquidLimitNote
			{
				get
				{
					System.String data = entity.DietLiquidLimitNote;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DietLiquidLimitNote = null;
					else entity.DietLiquidLimitNote = Convert.ToString(value);
				}
			}
			public System.String DefecateType
			{
				get
				{
					System.String data = entity.DefecateType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DefecateType = null;
					else entity.DefecateType = Convert.ToString(value);
				}
			}
			public System.String UrinateType
			{
				get
				{
					System.String data = entity.UrinateType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UrinateType = null;
					else entity.UrinateType = Convert.ToString(value);
				}
			}
			public System.String CatheterLastDate
			{
				get
				{
					System.DateTime? data = entity.CatheterLastDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CatheterLastDate = null;
					else entity.CatheterLastDate = Convert.ToDateTime(value);
				}
			}
			public System.String UterineType
			{
				get
				{
					System.String data = entity.UterineType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UterineType = null;
					else entity.UterineType = Convert.ToString(value);
				}
			}
			public System.String UterineHeight
			{
				get
				{
					System.Int32? data = entity.UterineHeight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UterineHeight = null;
					else entity.UterineHeight = Convert.ToInt32(value);
				}
			}
			public System.String VulvaType
			{
				get
				{
					System.String data = entity.VulvaType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VulvaType = null;
					else entity.VulvaType = Convert.ToString(value);
				}
			}
			public System.String PerinealWoundType
			{
				get
				{
					System.String data = entity.PerinealWoundType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PerinealWoundType = null;
					else entity.PerinealWoundType = Convert.ToString(value);
				}
			}
			public System.String LocheaType
			{
				get
				{
					System.String data = entity.LocheaType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocheaType = null;
					else entity.LocheaType = Convert.ToString(value);
				}
			}
			public System.String LocheaColor
			{
				get
				{
					System.String data = entity.LocheaColor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocheaColor = null;
					else entity.LocheaColor = Convert.ToString(value);
				}
			}
			public System.String LocheaSmell
			{
				get
				{
					System.String data = entity.LocheaSmell;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocheaSmell = null;
					else entity.LocheaSmell = Convert.ToString(value);
				}
			}
			public System.String OperationWoundType
			{
				get
				{
					System.String data = entity.OperationWoundType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationWoundType = null;
					else entity.OperationWoundType = Convert.ToString(value);
				}
			}
			public System.String TransferMobilizationType
			{
				get
				{
					System.String data = entity.TransferMobilizationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransferMobilizationType = null;
					else entity.TransferMobilizationType = Convert.ToString(value);
				}
			}
			public System.String PartiallyAssisted
			{
				get
				{
					System.String data = entity.PartiallyAssisted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PartiallyAssisted = null;
					else entity.PartiallyAssisted = Convert.ToString(value);
				}
			}
			public System.String AssistToolType
			{
				get
				{
					System.String data = entity.AssistToolType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssistToolType = null;
					else entity.AssistToolType = Convert.ToString(value);
				}
			}
			public System.String Education
			{
				get
				{
					System.String data = entity.Education;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Education = null;
					else entity.Education = Convert.ToString(value);
				}
			}
			public System.String EducationOthers
			{
				get
				{
					System.String data = entity.EducationOthers;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EducationOthers = null;
					else entity.EducationOthers = Convert.ToString(value);
				}
			}
			public System.String TreatmentDiag01
			{
				get
				{
					System.String data = entity.TreatmentDiag01;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TreatmentDiag01 = null;
					else entity.TreatmentDiag01 = Convert.ToString(value);
				}
			}
			public System.String TreatmentDiag02
			{
				get
				{
					System.String data = entity.TreatmentDiag02;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TreatmentDiag02 = null;
					else entity.TreatmentDiag02 = Convert.ToString(value);
				}
			}
			public System.String TreatmentDiag03
			{
				get
				{
					System.String data = entity.TreatmentDiag03;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TreatmentDiag03 = null;
					else entity.TreatmentDiag03 = Convert.ToString(value);
				}
			}
			public System.String TreatmentDiag04
			{
				get
				{
					System.String data = entity.TreatmentDiag04;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TreatmentDiag04 = null;
					else entity.TreatmentDiag04 = Convert.ToString(value);
				}
			}
			public System.String DischargeDiag01
			{
				get
				{
					System.String data = entity.DischargeDiag01;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDiag01 = null;
					else entity.DischargeDiag01 = Convert.ToString(value);
				}
			}
			public System.String DischargeDiag02
			{
				get
				{
					System.String data = entity.DischargeDiag02;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDiag02 = null;
					else entity.DischargeDiag02 = Convert.ToString(value);
				}
			}
			public System.String DischargeDiag03
			{
				get
				{
					System.String data = entity.DischargeDiag03;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDiag03 = null;
					else entity.DischargeDiag03 = Convert.ToString(value);
				}
			}
			public System.String DischargeDiag04
			{
				get
				{
					System.String data = entity.DischargeDiag04;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DischargeDiag04 = null;
					else entity.DischargeDiag04 = Convert.ToString(value);
				}
			}
			public System.String DrugsTaken
			{
				get
				{
					System.String data = entity.DrugsTaken;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DrugsTaken = null;
					else entity.DrugsTaken = Convert.ToString(value);
				}
			}
			public System.String PossibleEffect
			{
				get
				{
					System.String data = entity.PossibleEffect;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PossibleEffect = null;
					else entity.PossibleEffect = Convert.ToString(value);
				}
			}
			public System.String HospitalRefer
			{
				get
				{
					System.String data = entity.HospitalRefer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HospitalRefer = null;
					else entity.HospitalRefer = Convert.ToString(value);
				}
			}
			public System.String LabResultSheet
			{
				get
				{
					System.Int32? data = entity.LabResultSheet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LabResultSheet = null;
					else entity.LabResultSheet = Convert.ToInt32(value);
				}
			}
			public System.String XRaysSheet
			{
				get
				{
					System.Int32? data = entity.XRaysSheet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.XRaysSheet = null;
					else entity.XRaysSheet = Convert.ToInt32(value);
				}
			}
			public System.String CTScanSheet
			{
				get
				{
					System.Int32? data = entity.CTScanSheet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CTScanSheet = null;
					else entity.CTScanSheet = Convert.ToInt32(value);
				}
			}
			public System.String MriMraSheet
			{
				get
				{
					System.Int32? data = entity.MriMraSheet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MriMraSheet = null;
					else entity.MriMraSheet = Convert.ToInt32(value);
				}
			}
			public System.String UsgResultSheet
			{
				get
				{
					System.Int32? data = entity.UsgResultSheet;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UsgResultSheet = null;
					else entity.UsgResultSheet = Convert.ToInt32(value);
				}
			}
			public System.String IsCertIllnes
			{
				get
				{
					System.Boolean? data = entity.IsCertIllnes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCertIllnes = null;
					else entity.IsCertIllnes = Convert.ToBoolean(value);
				}
			}
			public System.String IsInsLetter
			{
				get
				{
					System.Boolean? data = entity.IsInsLetter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInsLetter = null;
					else entity.IsInsLetter = Convert.ToBoolean(value);
				}
			}
			public System.String IsDischSummaryLetter
			{
				get
				{
					System.Boolean? data = entity.IsDischSummaryLetter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDischSummaryLetter = null;
					else entity.IsDischSummaryLetter = Convert.ToBoolean(value);
				}
			}
			public System.String IsBabyBook
			{
				get
				{
					System.Boolean? data = entity.IsBabyBook;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBabyBook = null;
					else entity.IsBabyBook = Convert.ToBoolean(value);
				}
			}
			public System.String IsBabyBloodType
			{
				get
				{
					System.Boolean? data = entity.IsBabyBloodType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBabyBloodType = null;
					else entity.IsBabyBloodType = Convert.ToBoolean(value);
				}
			}
			public System.String IsCertBirth
			{
				get
				{
					System.Boolean? data = entity.IsCertBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCertBirth = null;
					else entity.IsCertBirth = Convert.ToBoolean(value);
				}
			}
			public System.String HandedBy
			{
				get
				{
					System.String data = entity.HandedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HandedBy = null;
					else entity.HandedBy = Convert.ToString(value);
				}
			}
			public System.String OtherLetter
			{
				get
				{
					System.String data = entity.OtherLetter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherLetter = null;
					else entity.OtherLetter = Convert.ToString(value);
				}
			}
			public System.String ControlPlan
			{
				get
				{
					System.String data = entity.ControlPlan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ControlPlan = null;
					else entity.ControlPlan = Convert.ToString(value);
				}
			}
			private esMedicalDischargeSummaryByNurse entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalDischargeSummaryByNurseQuery query)
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
				throw new Exception("esMedicalDischargeSummaryByNurse can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicalDischargeSummaryByNurse : esMedicalDischargeSummaryByNurse
	{	
	}

	[Serializable]
	abstract public class esMedicalDischargeSummaryByNurseQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return MedicalDischargeSummaryByNurseMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem Temp
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.Temp, esSystemType.String);
			}
		} 
			
		public esQueryItem Pulse
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.Pulse, esSystemType.String);
			}
		} 
			
		public esQueryItem Respiratory
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.Respiratory, esSystemType.String);
			}
		} 
			
		public esQueryItem BloodPress
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.BloodPress, esSystemType.String);
			}
		} 
			
		public esQueryItem DietType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.DietType, esSystemType.String);
			}
		} 
			
		public esQueryItem SpecialDietNote
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.SpecialDietNote, esSystemType.String);
			}
		} 
			
		public esQueryItem DietLiquidLimitNote
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.DietLiquidLimitNote, esSystemType.String);
			}
		} 
			
		public esQueryItem DefecateType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.DefecateType, esSystemType.String);
			}
		} 
			
		public esQueryItem UrinateType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.UrinateType, esSystemType.String);
			}
		} 
			
		public esQueryItem CatheterLastDate
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.CatheterLastDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem UterineType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.UterineType, esSystemType.String);
			}
		} 
			
		public esQueryItem UterineHeight
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.UterineHeight, esSystemType.Int32);
			}
		} 
			
		public esQueryItem VulvaType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.VulvaType, esSystemType.String);
			}
		} 
			
		public esQueryItem PerinealWoundType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.PerinealWoundType, esSystemType.String);
			}
		} 
			
		public esQueryItem LocheaType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaType, esSystemType.String);
			}
		} 
			
		public esQueryItem LocheaColor
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaColor, esSystemType.String);
			}
		} 
			
		public esQueryItem LocheaSmell
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaSmell, esSystemType.String);
			}
		} 
			
		public esQueryItem OperationWoundType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.OperationWoundType, esSystemType.String);
			}
		} 
			
		public esQueryItem TransferMobilizationType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.TransferMobilizationType, esSystemType.String);
			}
		} 
			
		public esQueryItem PartiallyAssisted
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.PartiallyAssisted, esSystemType.String);
			}
		} 
			
		public esQueryItem AssistToolType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.AssistToolType, esSystemType.String);
			}
		} 
			
		public esQueryItem Education
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.Education, esSystemType.String);
			}
		} 
			
		public esQueryItem EducationOthers
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.EducationOthers, esSystemType.String);
			}
		} 
			
		public esQueryItem TreatmentDiag01
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag01, esSystemType.String);
			}
		} 
			
		public esQueryItem TreatmentDiag02
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag02, esSystemType.String);
			}
		} 
			
		public esQueryItem TreatmentDiag03
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag03, esSystemType.String);
			}
		} 
			
		public esQueryItem TreatmentDiag04
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag04, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDiag01
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag01, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDiag02
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag02, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDiag03
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag03, esSystemType.String);
			}
		} 
			
		public esQueryItem DischargeDiag04
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag04, esSystemType.String);
			}
		} 
			
		public esQueryItem DrugsTaken
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.DrugsTaken, esSystemType.String);
			}
		} 
			
		public esQueryItem PossibleEffect
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.PossibleEffect, esSystemType.String);
			}
		} 
			
		public esQueryItem HospitalRefer
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.HospitalRefer, esSystemType.String);
			}
		} 
			
		public esQueryItem LabResultSheet
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.LabResultSheet, esSystemType.Int32);
			}
		} 
			
		public esQueryItem XRaysSheet
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.XRaysSheet, esSystemType.Int32);
			}
		} 
			
		public esQueryItem CTScanSheet
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.CTScanSheet, esSystemType.Int32);
			}
		} 
			
		public esQueryItem MriMraSheet
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.MriMraSheet, esSystemType.Int32);
			}
		} 
			
		public esQueryItem UsgResultSheet
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.UsgResultSheet, esSystemType.Int32);
			}
		} 
			
		public esQueryItem IsCertIllnes
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsCertIllnes, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsInsLetter
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsInsLetter, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsDischSummaryLetter
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsDischSummaryLetter, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsBabyBook
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsBabyBook, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsBabyBloodType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsBabyBloodType, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsCertBirth
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsCertBirth, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem HandedBy
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.HandedBy, esSystemType.String);
			}
		} 
			
		public esQueryItem OtherLetter
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.OtherLetter, esSystemType.String);
			}
		} 
			
		public esQueryItem ControlPlan
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryByNurseMetadata.ColumnNames.ControlPlan, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalDischargeSummaryByNurseCollection")]
	public partial class MedicalDischargeSummaryByNurseCollection : esMedicalDischargeSummaryByNurseCollection, IEnumerable< MedicalDischargeSummaryByNurse>
	{
		public MedicalDischargeSummaryByNurseCollection()
		{

		}	
		
		public static implicit operator List< MedicalDischargeSummaryByNurse>(MedicalDischargeSummaryByNurseCollection coll)
		{
			List< MedicalDischargeSummaryByNurse> list = new List< MedicalDischargeSummaryByNurse>();
			
			foreach (MedicalDischargeSummaryByNurse emp in coll)
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
				return  MedicalDischargeSummaryByNurseMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalDischargeSummaryByNurseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalDischargeSummaryByNurse(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalDischargeSummaryByNurse();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public MedicalDischargeSummaryByNurseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalDischargeSummaryByNurseQuery();
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
		public bool Load(MedicalDischargeSummaryByNurseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicalDischargeSummaryByNurse AddNew()
		{
			MedicalDischargeSummaryByNurse entity = base.AddNewEntity() as MedicalDischargeSummaryByNurse;
			
			return entity;		
		}
		public MedicalDischargeSummaryByNurse FindByPrimaryKey(String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as MedicalDischargeSummaryByNurse;
		}

		#region IEnumerable< MedicalDischargeSummaryByNurse> Members

		IEnumerator< MedicalDischargeSummaryByNurse> IEnumerable< MedicalDischargeSummaryByNurse>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalDischargeSummaryByNurse;
			}
		}

		#endregion
		
		private MedicalDischargeSummaryByNurseQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalDischargeSummaryByNurse' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicalDischargeSummaryByNurse ({RegistrationNo})")]
	[Serializable]
	public partial class MedicalDischargeSummaryByNurse : esMedicalDischargeSummaryByNurse
	{
		public MedicalDischargeSummaryByNurse()
		{
		}	
	
		public MedicalDischargeSummaryByNurse(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalDischargeSummaryByNurseMetadata.Meta();
			}
		}	
	
		override protected esMedicalDischargeSummaryByNurseQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalDischargeSummaryByNurseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public MedicalDischargeSummaryByNurseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalDischargeSummaryByNurseQuery();
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
		public bool Load(MedicalDischargeSummaryByNurseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private MedicalDischargeSummaryByNurseQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicalDischargeSummaryByNurseQuery : esMedicalDischargeSummaryByNurseQuery
	{
		public MedicalDischargeSummaryByNurseQuery()
		{

		}		
		
		public MedicalDischargeSummaryByNurseQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "MedicalDischargeSummaryByNurseQuery";
        }
	}

	[Serializable]
	public partial class MedicalDischargeSummaryByNurseMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalDischargeSummaryByNurseMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LastUpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LastUpdateByUserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Temp, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.Temp;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Pulse, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.Pulse;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Respiratory, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.Respiratory;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.BloodPress, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.BloodPress;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DietType, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.DietType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.SpecialDietNote, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.SpecialDietNote;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DietLiquidLimitNote, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.DietLiquidLimitNote;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DefecateType, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.DefecateType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UrinateType, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.UrinateType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.CatheterLastDate, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.CatheterLastDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UterineType, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.UterineType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UterineHeight, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.UterineHeight;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.VulvaType, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.VulvaType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PerinealWoundType, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.PerinealWoundType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaType, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.LocheaType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaColor, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.LocheaColor;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LocheaSmell, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.LocheaSmell;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.OperationWoundType, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.OperationWoundType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TransferMobilizationType, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.TransferMobilizationType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PartiallyAssisted, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.PartiallyAssisted;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.AssistToolType, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.AssistToolType;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.Education, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.Education;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.EducationOthers, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.EducationOthers;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag01, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.TreatmentDiag01;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag02, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.TreatmentDiag02;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag03, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.TreatmentDiag03;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.TreatmentDiag04, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.TreatmentDiag04;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag01, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.DischargeDiag01;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag02, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.DischargeDiag02;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag03, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.DischargeDiag03;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DischargeDiag04, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.DischargeDiag04;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.DrugsTaken, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.DrugsTaken;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.PossibleEffect, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.PossibleEffect;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.HospitalRefer, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.HospitalRefer;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.LabResultSheet, 37, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.LabResultSheet;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.XRaysSheet, 38, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.XRaysSheet;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.CTScanSheet, 39, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.CTScanSheet;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.MriMraSheet, 40, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.MriMraSheet;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.UsgResultSheet, 41, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.UsgResultSheet;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsCertIllnes, 42, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.IsCertIllnes;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsInsLetter, 43, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.IsInsLetter;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsDischSummaryLetter, 44, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.IsDischSummaryLetter;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsBabyBook, 45, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.IsBabyBook;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsBabyBloodType, 46, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.IsBabyBloodType;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.IsCertBirth, 47, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.IsCertBirth;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.HandedBy, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.HandedBy;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.OtherLetter, 49, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.OtherLetter;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryByNurseMetadata.ColumnNames.ControlPlan, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryByNurseMetadata.PropertyNames.ControlPlan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public MedicalDischargeSummaryByNurseMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Temp = "Temp";
			public const string Pulse = "Pulse";
			public const string Respiratory = "Respiratory";
			public const string BloodPress = "BloodPress";
			public const string DietType = "DietType";
			public const string SpecialDietNote = "SpecialDietNote";
			public const string DietLiquidLimitNote = "DietLiquidLimitNote";
			public const string DefecateType = "DefecateType";
			public const string UrinateType = "UrinateType";
			public const string CatheterLastDate = "CatheterLastDate";
			public const string UterineType = "UterineType";
			public const string UterineHeight = "UterineHeight";
			public const string VulvaType = "VulvaType";
			public const string PerinealWoundType = "PerinealWoundType";
			public const string LocheaType = "LocheaType";
			public const string LocheaColor = "LocheaColor";
			public const string LocheaSmell = "LocheaSmell";
			public const string OperationWoundType = "OperationWoundType";
			public const string TransferMobilizationType = "TransferMobilizationType";
			public const string PartiallyAssisted = "PartiallyAssisted";
			public const string AssistToolType = "AssistToolType";
			public const string Education = "Education";
			public const string EducationOthers = "EducationOthers";
			public const string TreatmentDiag01 = "TreatmentDiag01";
			public const string TreatmentDiag02 = "TreatmentDiag02";
			public const string TreatmentDiag03 = "TreatmentDiag03";
			public const string TreatmentDiag04 = "TreatmentDiag04";
			public const string DischargeDiag01 = "DischargeDiag01";
			public const string DischargeDiag02 = "DischargeDiag02";
			public const string DischargeDiag03 = "DischargeDiag03";
			public const string DischargeDiag04 = "DischargeDiag04";
			public const string DrugsTaken = "DrugsTaken";
			public const string PossibleEffect = "PossibleEffect";
			public const string HospitalRefer = "HospitalRefer";
			public const string LabResultSheet = "LabResultSheet";
			public const string XRaysSheet = "XRaysSheet";
			public const string CTScanSheet = "CTScanSheet";
			public const string MriMraSheet = "MriMraSheet";
			public const string UsgResultSheet = "UsgResultSheet";
			public const string IsCertIllnes = "IsCertIllnes";
			public const string IsInsLetter = "IsInsLetter";
			public const string IsDischSummaryLetter = "IsDischSummaryLetter";
			public const string IsBabyBook = "IsBabyBook";
			public const string IsBabyBloodType = "IsBabyBloodType";
			public const string IsCertBirth = "IsCertBirth";
			public const string HandedBy = "HandedBy";
			public const string OtherLetter = "OtherLetter";
			public const string ControlPlan = "ControlPlan";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Temp = "Temp";
			public const string Pulse = "Pulse";
			public const string Respiratory = "Respiratory";
			public const string BloodPress = "BloodPress";
			public const string DietType = "DietType";
			public const string SpecialDietNote = "SpecialDietNote";
			public const string DietLiquidLimitNote = "DietLiquidLimitNote";
			public const string DefecateType = "DefecateType";
			public const string UrinateType = "UrinateType";
			public const string CatheterLastDate = "CatheterLastDate";
			public const string UterineType = "UterineType";
			public const string UterineHeight = "UterineHeight";
			public const string VulvaType = "VulvaType";
			public const string PerinealWoundType = "PerinealWoundType";
			public const string LocheaType = "LocheaType";
			public const string LocheaColor = "LocheaColor";
			public const string LocheaSmell = "LocheaSmell";
			public const string OperationWoundType = "OperationWoundType";
			public const string TransferMobilizationType = "TransferMobilizationType";
			public const string PartiallyAssisted = "PartiallyAssisted";
			public const string AssistToolType = "AssistToolType";
			public const string Education = "Education";
			public const string EducationOthers = "EducationOthers";
			public const string TreatmentDiag01 = "TreatmentDiag01";
			public const string TreatmentDiag02 = "TreatmentDiag02";
			public const string TreatmentDiag03 = "TreatmentDiag03";
			public const string TreatmentDiag04 = "TreatmentDiag04";
			public const string DischargeDiag01 = "DischargeDiag01";
			public const string DischargeDiag02 = "DischargeDiag02";
			public const string DischargeDiag03 = "DischargeDiag03";
			public const string DischargeDiag04 = "DischargeDiag04";
			public const string DrugsTaken = "DrugsTaken";
			public const string PossibleEffect = "PossibleEffect";
			public const string HospitalRefer = "HospitalRefer";
			public const string LabResultSheet = "LabResultSheet";
			public const string XRaysSheet = "XRaysSheet";
			public const string CTScanSheet = "CTScanSheet";
			public const string MriMraSheet = "MriMraSheet";
			public const string UsgResultSheet = "UsgResultSheet";
			public const string IsCertIllnes = "IsCertIllnes";
			public const string IsInsLetter = "IsInsLetter";
			public const string IsDischSummaryLetter = "IsDischSummaryLetter";
			public const string IsBabyBook = "IsBabyBook";
			public const string IsBabyBloodType = "IsBabyBloodType";
			public const string IsCertBirth = "IsCertBirth";
			public const string HandedBy = "HandedBy";
			public const string OtherLetter = "OtherLetter";
			public const string ControlPlan = "ControlPlan";
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
			lock (typeof(MedicalDischargeSummaryByNurseMetadata))
			{
				if(MedicalDischargeSummaryByNurseMetadata.mapDelegates == null)
				{
					MedicalDischargeSummaryByNurseMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalDischargeSummaryByNurseMetadata.meta == null)
				{
					MedicalDischargeSummaryByNurseMetadata.meta = new MedicalDischargeSummaryByNurseMetadata();
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
				
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Temp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pulse", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Respiratory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BloodPress", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DietType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SpecialDietNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DietLiquidLimitNote", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DefecateType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UrinateType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CatheterLastDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("UterineType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UterineHeight", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("VulvaType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PerinealWoundType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocheaType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocheaColor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LocheaSmell", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OperationWoundType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransferMobilizationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PartiallyAssisted", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssistToolType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Education", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EducationOthers", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TreatmentDiag01", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TreatmentDiag02", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TreatmentDiag03", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TreatmentDiag04", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDiag01", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDiag02", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDiag03", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DischargeDiag04", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DrugsTaken", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PossibleEffect", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HospitalRefer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LabResultSheet", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("XRaysSheet", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CTScanSheet", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MriMraSheet", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UsgResultSheet", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsCertIllnes", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInsLetter", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDischSummaryLetter", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBabyBook", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBabyBloodType", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCertBirth", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("HandedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherLetter", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ControlPlan", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "MedicalDischargeSummaryByNurse";
				meta.Destination = "MedicalDischargeSummaryByNurse";
				meta.spInsert = "proc_MedicalDischargeSummaryByNurseInsert";				
				meta.spUpdate = "proc_MedicalDischargeSummaryByNurseUpdate";		
				meta.spDelete = "proc_MedicalDischargeSummaryByNurseDelete";
				meta.spLoadAll = "proc_MedicalDischargeSummaryByNurseLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalDischargeSummaryByNurseLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalDischargeSummaryByNurseMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
