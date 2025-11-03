/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/9/2016 9:54:02 AM
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
	abstract public class esWageTransactionCollection : esEntityCollectionWAuditLog
	{
		public esWageTransactionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WageTransactionCollection";
		}

		#region Query Logic
		protected void InitQuery(esWageTransactionQuery query)
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
			this.InitQuery(query as esWageTransactionQuery);
		}
		#endregion
		
		virtual public WageTransaction DetachEntity(WageTransaction entity)
		{
			return base.DetachEntity(entity) as WageTransaction;
		}
		
		virtual public WageTransaction AttachEntity(WageTransaction entity)
		{
			return base.AttachEntity(entity) as WageTransaction;
		}
		
		virtual public void Combine(WageTransactionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WageTransaction this[int index]
		{
			get
			{
				return base[index] as WageTransaction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WageTransaction);
		}
	}



	[Serializable]
	abstract public class esWageTransaction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWageTransactionQuery GetDynamicQuery()
		{
			return null;
		}

		public esWageTransaction()
		{

		}

		public esWageTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 wageTransactionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageTransactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageTransactionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 wageTransactionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wageTransactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(wageTransactionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 wageTransactionID)
		{
			esWageTransactionQuery query = this.GetDynamicQuery();
			query.Where(query.WageTransactionID == wageTransactionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 wageTransactionID)
		{
			esParameters parms = new esParameters();
			parms.Add("WageTransactionID",wageTransactionID);
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
						case "WageTransactionID": this.str.WageTransactionID = (string)value; break;							
						case "WageProcessTypeID": this.str.WageProcessTypeID = (string)value; break;							
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
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
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;							
						case "ServiceYear": this.str.ServiceYear = (string)value; break;							
						case "SalaryTableNumber": this.str.SalaryTableNumber = (string)value; break;							
						case "EmployeeGradeID": this.str.EmployeeGradeID = (string)value; break;							
						case "NoOfDependent": this.str.NoOfDependent = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "IsKWI": this.str.IsKWI = (string)value; break;							
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "WageTransactionID":
						
							if (value == null || value is System.Int64)
								this.WageTransactionID = (System.Int64?)value;
							break;
						
						case "WageProcessTypeID":
						
							if (value == null || value is System.Int32)
								this.WageProcessTypeID = (System.Int32?)value;
							break;
						
						case "PayrollPeriodID":
						
							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
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
						
						case "ServiceYear":
						
							if (value == null || value is System.Int32)
								this.ServiceYear = (System.Int32?)value;
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
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "IsKWI":
						
							if (value == null || value is System.Boolean)
								this.IsKWI = (System.Boolean?)value;
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
		/// Maps to WageTransaction.WageTransactionID
		/// </summary>
		virtual public System.Int64? WageTransactionID
		{
			get
			{
				return base.GetSystemInt64(WageTransactionMetadata.ColumnNames.WageTransactionID);
			}
			
			set
			{
				base.SetSystemInt64(WageTransactionMetadata.ColumnNames.WageTransactionID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.WageProcessTypeID
		/// </summary>
		virtual public System.Int32? WageProcessTypeID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.WageProcessTypeID);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.WageProcessTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.PayrollPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(WageTransactionMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(WageTransactionMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SREmployeeType
		/// </summary>
		virtual public System.String SREmployeeType
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SREmployeeType);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SREmployeeType, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SRRemunerationType
		/// </summary>
		virtual public System.String SRRemunerationType
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SRRemunerationType);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SRRemunerationType, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.OrganizationUnitID
		/// </summary>
		virtual public System.Int32? OrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.OrganizationUnitID);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.OrganizationUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SubOrganizationUnitID
		/// </summary>
		virtual public System.Int32? SubOrganizationUnitID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.SubOrganizationUnitID);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.SubOrganizationUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SRPaymentFrequency
		/// </summary>
		virtual public System.String SRPaymentFrequency
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SRPaymentFrequency);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SRPaymentFrequency, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SRTaxStatus
		/// </summary>
		virtual public System.String SRTaxStatus
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SRTaxStatus);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SRTaxStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SREmployeeStatus
		/// </summary>
		virtual public System.String SREmployeeStatus
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SREmployeeStatus);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SREmployeeStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SRReligion
		/// </summary>
		virtual public System.String SRReligion
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SRReligion);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SRReligion, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SREmploymentType);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.PositionGradeID);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SRMaritalStatus);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.ServiceYear
		/// </summary>
		virtual public System.Int32? ServiceYear
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.ServiceYear);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.ServiceYear, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SalaryTableNumber
		/// </summary>
		virtual public System.Int32? SalaryTableNumber
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.SalaryTableNumber);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.SalaryTableNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.EmployeeGradeID
		/// </summary>
		virtual public System.Int32? EmployeeGradeID
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.EmployeeGradeID);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.EmployeeGradeID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.NoOfDependent
		/// </summary>
		virtual public System.Int32? NoOfDependent
		{
			get
			{
				return base.GetSystemInt32(WageTransactionMetadata.ColumnNames.NoOfDependent);
			}
			
			set
			{
				base.SetSystemInt32(WageTransactionMetadata.ColumnNames.NoOfDependent, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(WageTransactionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(WageTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.IsKWI
		/// </summary>
		virtual public System.Boolean? IsKWI
		{
			get
			{
				return base.GetSystemBoolean(WageTransactionMetadata.ColumnNames.IsKWI);
			}
			
			set
			{
				base.SetSystemBoolean(WageTransactionMetadata.ColumnNames.IsKWI, value);
			}
		}
		
		/// <summary>
		/// Maps to WageTransaction.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(WageTransactionMetadata.ColumnNames.SREducationLevel);
			}
			
			set
			{
				base.SetSystemString(WageTransactionMetadata.ColumnNames.SREducationLevel, value);
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
			public esStrings(esWageTransaction entity)
			{
				this.entity = entity;
			}
			
	
			public System.String WageTransactionID
			{
				get
				{
					System.Int64? data = entity.WageTransactionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageTransactionID = null;
					else entity.WageTransactionID = Convert.ToInt64(value);
				}
			}
				
			public System.String WageProcessTypeID
			{
				get
				{
					System.Int32? data = entity.WageProcessTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WageProcessTypeID = null;
					else entity.WageProcessTypeID = Convert.ToInt32(value);
				}
			}
				
			public System.String PayrollPeriodID
			{
				get
				{
					System.Int32? data = entity.PayrollPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
					else entity.PayrollPeriodID = Convert.ToInt32(value);
				}
			}
				
			public System.String TransactionDate
			{
				get
				{
					System.DateTime? data = entity.TransactionDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionDate = null;
					else entity.TransactionDate = Convert.ToDateTime(value);
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
				
			public System.String ServiceYear
			{
				get
				{
					System.Int32? data = entity.ServiceYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceYear = null;
					else entity.ServiceYear = Convert.ToInt32(value);
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
			

			private esWageTransaction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWageTransactionQuery query)
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
				throw new Exception("esWageTransaction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class WageTransaction : esWageTransaction
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
	abstract public class esWageTransactionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WageTransactionMetadata.Meta();
			}
		}	
		

		public esQueryItem WageTransactionID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.WageTransactionID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem WageProcessTypeID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.WageProcessTypeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SREmployeeType
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SREmployeeType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRRemunerationType
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SRRemunerationType, esSystemType.String);
			}
		} 
		
		public esQueryItem OrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.OrganizationUnitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SubOrganizationUnitID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SubOrganizationUnitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRPaymentFrequency
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SRPaymentFrequency, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTaxStatus
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SRTaxStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmployeeStatus
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SREmployeeStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRReligion
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SRReligion, esSystemType.String);
			}
		} 
		
		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		} 
		
		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceYear
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.ServiceYear, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryTableNumber
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SalaryTableNumber, esSystemType.Int32);
			}
		} 
		
		public esQueryItem EmployeeGradeID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.EmployeeGradeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem NoOfDependent
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.NoOfDependent, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsKWI
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.IsKWI, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, WageTransactionMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WageTransactionCollection")]
	public partial class WageTransactionCollection : esWageTransactionCollection, IEnumerable<WageTransaction>
	{
		public WageTransactionCollection()
		{

		}
		
		public static implicit operator List<WageTransaction>(WageTransactionCollection coll)
		{
			List<WageTransaction> list = new List<WageTransaction>();
			
			foreach (WageTransaction emp in coll)
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
				return  WageTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WageTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WageTransaction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WageTransaction();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WageTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WageTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WageTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WageTransaction AddNew()
		{
			WageTransaction entity = base.AddNewEntity() as WageTransaction;
			
			return entity;
		}

		public WageTransaction FindByPrimaryKey(System.Int64 wageTransactionID)
		{
			return base.FindByPrimaryKey(wageTransactionID) as WageTransaction;
		}


		#region IEnumerable<WageTransaction> Members

		IEnumerator<WageTransaction> IEnumerable<WageTransaction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WageTransaction;
			}
		}

		#endregion
		
		private WageTransactionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WageTransaction' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WageTransaction ({WageTransactionID})")]
	[Serializable]
	public partial class WageTransaction : esWageTransaction
	{
		public WageTransaction()
		{

		}
	
		public WageTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WageTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esWageTransactionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WageTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WageTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WageTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WageTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WageTransactionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WageTransactionQuery : esWageTransactionQuery
	{
		public WageTransactionQuery()
		{

		}		
		
		public WageTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WageTransactionQuery";
        }
		
			
	}


	[Serializable]
	public partial class WageTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WageTransactionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.WageTransactionID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = WageTransactionMetadata.PropertyNames.WageTransactionID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.WageProcessTypeID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.WageProcessTypeID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.PayrollPeriodID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.TransactionDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WageTransactionMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.PersonID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SREmployeeType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SREmployeeType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SRRemunerationType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SRRemunerationType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.OrganizationUnitID, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.OrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SubOrganizationUnitID, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SubOrganizationUnitID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SRPaymentFrequency, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SRPaymentFrequency;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SRTaxStatus, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SRTaxStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SREmployeeStatus, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SREmployeeStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.PositionID, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SRReligion, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SRReligion;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SREmploymentType, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.PositionGradeID, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SRMaritalStatus, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.ServiceYear, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.ServiceYear;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SalaryTableNumber, 18, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SalaryTableNumber;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.EmployeeGradeID, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.EmployeeGradeID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.NoOfDependent, 20, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = WageTransactionMetadata.PropertyNames.NoOfDependent;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WageTransactionMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.IsKWI, 23, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = WageTransactionMetadata.PropertyNames.IsKWI;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WageTransactionMetadata.ColumnNames.SREducationLevel, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = WageTransactionMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WageTransactionMetadata Meta()
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
			 public const string WageTransactionID = "WageTransactionID";
			 public const string WageProcessTypeID = "WageProcessTypeID";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string TransactionDate = "TransactionDate";
			 public const string PersonID = "PersonID";
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
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string ServiceYear = "ServiceYear";
			 public const string SalaryTableNumber = "SalaryTableNumber";
			 public const string EmployeeGradeID = "EmployeeGradeID";
			 public const string NoOfDependent = "NoOfDependent";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsKWI = "IsKWI";
			 public const string SREducationLevel = "SREducationLevel";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WageTransactionID = "WageTransactionID";
			 public const string WageProcessTypeID = "WageProcessTypeID";
			 public const string PayrollPeriodID = "PayrollPeriodID";
			 public const string TransactionDate = "TransactionDate";
			 public const string PersonID = "PersonID";
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
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string ServiceYear = "ServiceYear";
			 public const string SalaryTableNumber = "SalaryTableNumber";
			 public const string EmployeeGradeID = "EmployeeGradeID";
			 public const string NoOfDependent = "NoOfDependent";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsKWI = "IsKWI";
			 public const string SREducationLevel = "SREducationLevel";
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
			lock (typeof(WageTransactionMetadata))
			{
				if(WageTransactionMetadata.mapDelegates == null)
				{
					WageTransactionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WageTransactionMetadata.meta == null)
				{
					WageTransactionMetadata.meta = new WageTransactionMetadata();
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
				

				meta.AddTypeMap("WageTransactionID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("WageProcessTypeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
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
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryTableNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("EmployeeGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NoOfDependent", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsKWI", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "WageTransaction";
				meta.Destination = "WageTransaction";
				
				meta.spInsert = "proc_WageTransactionInsert";				
				meta.spUpdate = "proc_WageTransactionUpdate";		
				meta.spDelete = "proc_WageTransactionDelete";
				meta.spLoadAll = "proc_WageTransactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_WageTransactionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WageTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
