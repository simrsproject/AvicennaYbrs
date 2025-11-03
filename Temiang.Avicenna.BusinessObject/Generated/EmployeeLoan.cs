/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/24/2016 10:54:52 AM
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
	abstract public class esEmployeeLoanCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeLoanCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeLoanCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeLoanQuery query)
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
			this.InitQuery(query as esEmployeeLoanQuery);
		}
		#endregion
		
		virtual public EmployeeLoan DetachEntity(EmployeeLoan entity)
		{
			return base.DetachEntity(entity) as EmployeeLoan;
		}
		
		virtual public EmployeeLoan AttachEntity(EmployeeLoan entity)
		{
			return base.AttachEntity(entity) as EmployeeLoan;
		}
		
		virtual public void Combine(EmployeeLoanCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeLoan this[int index]
		{
			get
			{
				return base[index] as EmployeeLoan;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeLoan);
		}
	}



	[Serializable]
	abstract public class esEmployeeLoan : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeLoanQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeLoan()
		{

		}

		public esEmployeeLoan(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeLoanID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLoanID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLoanID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeLoanID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeLoanID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeLoanID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeLoanID)
		{
			esEmployeeLoanQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeLoanID == employeeLoanID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeLoanID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeLoanID",employeeLoanID);
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
						case "EmployeeLoanID": this.str.EmployeeLoanID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "LoanDate": this.str.LoanDate = (string)value; break;							
						case "Amount": this.str.Amount = (string)value; break;							
						case "LoanAmountWithInterest": this.str.LoanAmountWithInterest = (string)value; break;							
						case "PercentRateOfInterest": this.str.PercentRateOfInterest = (string)value; break;							
						case "FlatRate": this.str.FlatRate = (string)value; break;							
						case "SRPurposeOfLoan": this.str.SRPurposeOfLoan = (string)value; break;							
						case "NumberOfInstallment": this.str.NumberOfInstallment = (string)value; break;							
						case "AmountOfInstallment": this.str.AmountOfInstallment = (string)value; break;							
						case "SRHRPaymentMethod": this.str.SRHRPaymentMethod = (string)value; break;							
						case "StartPaymentPeriode": this.str.StartPaymentPeriode = (string)value; break;							
						case "SalaryCodeInstallment": this.str.SalaryCodeInstallment = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LoanNo": this.str.LoanNo = (string)value; break;							
						case "CoverageAmount": this.str.CoverageAmount = (string)value; break;							
						case "CompanyLaborProfileID": this.str.CompanyLaborProfileID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "ApprovedDateTime": this.str.ApprovedDateTime = (string)value; break;							
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;							
						case "InstallmentMethod": this.str.InstallmentMethod = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeLoanID":
						
							if (value == null || value is System.Int32)
								this.EmployeeLoanID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "LoanDate":
						
							if (value == null || value is System.DateTime)
								this.LoanDate = (System.DateTime?)value;
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						
						case "LoanAmountWithInterest":
						
							if (value == null || value is System.Decimal)
								this.LoanAmountWithInterest = (System.Decimal?)value;
							break;
						
						case "PercentRateOfInterest":
						
							if (value == null || value is System.Decimal)
								this.PercentRateOfInterest = (System.Decimal?)value;
							break;
						
						case "FlatRate":
						
							if (value == null || value is System.Boolean)
								this.FlatRate = (System.Boolean?)value;
							break;
						
						case "NumberOfInstallment":
						
							if (value == null || value is System.Int32)
								this.NumberOfInstallment = (System.Int32?)value;
							break;
						
						case "AmountOfInstallment":
						
							if (value == null || value is System.Decimal)
								this.AmountOfInstallment = (System.Decimal?)value;
							break;
						
						case "StartPaymentPeriode":
						
							if (value == null || value is System.Int32)
								this.StartPaymentPeriode = (System.Int32?)value;
							break;
						
						case "SalaryCodeInstallment":
						
							if (value == null || value is System.Int32)
								this.SalaryCodeInstallment = (System.Int32?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "LoanNo":
						
							if (value == null || value is System.Int32)
								this.LoanNo = (System.Int32?)value;
							break;
						
						case "CoverageAmount":
						
							if (value == null || value is System.Decimal)
								this.CoverageAmount = (System.Decimal?)value;
							break;
						
						case "CompanyLaborProfileID":
						
							if (value == null || value is System.Int32)
								this.CompanyLaborProfileID = (System.Int32?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "ApprovedDateTime":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDateTime = (System.DateTime?)value;
							break;
						
						case "InstallmentMethod":
						
							if (value == null || value is System.Boolean)
								this.InstallmentMethod = (System.Boolean?)value;
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
		/// Maps to EmployeeLoan.EmployeeLoanID
		/// </summary>
		virtual public System.Int32? EmployeeLoanID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanMetadata.ColumnNames.EmployeeLoanID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanMetadata.ColumnNames.EmployeeLoanID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.LoanDate
		/// </summary>
		virtual public System.DateTime? LoanDate
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLoanMetadata.ColumnNames.LoanDate);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLoanMetadata.ColumnNames.LoanDate, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.LoanAmountWithInterest
		/// </summary>
		virtual public System.Decimal? LoanAmountWithInterest
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanMetadata.ColumnNames.LoanAmountWithInterest);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanMetadata.ColumnNames.LoanAmountWithInterest, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.PercentRateOfInterest
		/// </summary>
		virtual public System.Decimal? PercentRateOfInterest
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanMetadata.ColumnNames.PercentRateOfInterest);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanMetadata.ColumnNames.PercentRateOfInterest, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.FlatRate
		/// </summary>
		virtual public System.Boolean? FlatRate
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLoanMetadata.ColumnNames.FlatRate);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeLoanMetadata.ColumnNames.FlatRate, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.SRPurposeOfLoan
		/// </summary>
		virtual public System.String SRPurposeOfLoan
		{
			get
			{
				return base.GetSystemString(EmployeeLoanMetadata.ColumnNames.SRPurposeOfLoan);
			}
			
			set
			{
				base.SetSystemString(EmployeeLoanMetadata.ColumnNames.SRPurposeOfLoan, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.NumberOfInstallment
		/// </summary>
		virtual public System.Int32? NumberOfInstallment
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanMetadata.ColumnNames.NumberOfInstallment);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanMetadata.ColumnNames.NumberOfInstallment, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.AmountOfInstallment
		/// </summary>
		virtual public System.Decimal? AmountOfInstallment
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanMetadata.ColumnNames.AmountOfInstallment);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanMetadata.ColumnNames.AmountOfInstallment, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.SRHRPaymentMethod
		/// </summary>
		virtual public System.String SRHRPaymentMethod
		{
			get
			{
				return base.GetSystemString(EmployeeLoanMetadata.ColumnNames.SRHRPaymentMethod);
			}
			
			set
			{
				base.SetSystemString(EmployeeLoanMetadata.ColumnNames.SRHRPaymentMethod, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.StartPaymentPeriode
		/// </summary>
		virtual public System.Int32? StartPaymentPeriode
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanMetadata.ColumnNames.StartPaymentPeriode);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanMetadata.ColumnNames.StartPaymentPeriode, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.SalaryCodeInstallment
		/// </summary>
		virtual public System.Int32? SalaryCodeInstallment
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanMetadata.ColumnNames.SalaryCodeInstallment);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanMetadata.ColumnNames.SalaryCodeInstallment, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLoanMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLoanMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLoanMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeLoanMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.LoanNo
		/// </summary>
		virtual public System.Int32? LoanNo
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanMetadata.ColumnNames.LoanNo);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanMetadata.ColumnNames.LoanNo, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.CoverageAmount
		/// </summary>
		virtual public System.Decimal? CoverageAmount
		{
			get
			{
				return base.GetSystemDecimal(EmployeeLoanMetadata.ColumnNames.CoverageAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmployeeLoanMetadata.ColumnNames.CoverageAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.CompanyLaborProfileID
		/// </summary>
		virtual public System.Int32? CompanyLaborProfileID
		{
			get
			{
				return base.GetSystemInt32(EmployeeLoanMetadata.ColumnNames.CompanyLaborProfileID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeLoanMetadata.ColumnNames.CompanyLaborProfileID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeLoanMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(EmployeeLoanMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLoanMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeLoanMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.ApprovedDateTime
		/// </summary>
		virtual public System.DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeLoanMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeLoanMetadata.ColumnNames.ApprovedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeLoanMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeLoanMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeLoan.InstallmentMethod
		/// </summary>
		virtual public System.Boolean? InstallmentMethod
		{
			get
			{
				return base.GetSystemBoolean(EmployeeLoanMetadata.ColumnNames.InstallmentMethod);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeLoanMetadata.ColumnNames.InstallmentMethod, value);
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
			public esStrings(esEmployeeLoan entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeLoanID
			{
				get
				{
					System.Int32? data = entity.EmployeeLoanID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeLoanID = null;
					else entity.EmployeeLoanID = Convert.ToInt32(value);
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
				
			public System.String LoanDate
			{
				get
				{
					System.DateTime? data = entity.LoanDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LoanDate = null;
					else entity.LoanDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
				}
			}
				
			public System.String LoanAmountWithInterest
			{
				get
				{
					System.Decimal? data = entity.LoanAmountWithInterest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LoanAmountWithInterest = null;
					else entity.LoanAmountWithInterest = Convert.ToDecimal(value);
				}
			}
				
			public System.String PercentRateOfInterest
			{
				get
				{
					System.Decimal? data = entity.PercentRateOfInterest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentRateOfInterest = null;
					else entity.PercentRateOfInterest = Convert.ToDecimal(value);
				}
			}
				
			public System.String FlatRate
			{
				get
				{
					System.Boolean? data = entity.FlatRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FlatRate = null;
					else entity.FlatRate = Convert.ToBoolean(value);
				}
			}
				
			public System.String SRPurposeOfLoan
			{
				get
				{
					System.String data = entity.SRPurposeOfLoan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPurposeOfLoan = null;
					else entity.SRPurposeOfLoan = Convert.ToString(value);
				}
			}
				
			public System.String NumberOfInstallment
			{
				get
				{
					System.Int32? data = entity.NumberOfInstallment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberOfInstallment = null;
					else entity.NumberOfInstallment = Convert.ToInt32(value);
				}
			}
				
			public System.String AmountOfInstallment
			{
				get
				{
					System.Decimal? data = entity.AmountOfInstallment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountOfInstallment = null;
					else entity.AmountOfInstallment = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRHRPaymentMethod
			{
				get
				{
					System.String data = entity.SRHRPaymentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRHRPaymentMethod = null;
					else entity.SRHRPaymentMethod = Convert.ToString(value);
				}
			}
				
			public System.String StartPaymentPeriode
			{
				get
				{
					System.Int32? data = entity.StartPaymentPeriode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartPaymentPeriode = null;
					else entity.StartPaymentPeriode = Convert.ToInt32(value);
				}
			}
				
			public System.String SalaryCodeInstallment
			{
				get
				{
					System.Int32? data = entity.SalaryCodeInstallment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryCodeInstallment = null;
					else entity.SalaryCodeInstallment = Convert.ToInt32(value);
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
				
			public System.String LoanNo
			{
				get
				{
					System.Int32? data = entity.LoanNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LoanNo = null;
					else entity.LoanNo = Convert.ToInt32(value);
				}
			}
				
			public System.String CoverageAmount
			{
				get
				{
					System.Decimal? data = entity.CoverageAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CoverageAmount = null;
					else entity.CoverageAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String CompanyLaborProfileID
			{
				get
				{
					System.Int32? data = entity.CompanyLaborProfileID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CompanyLaborProfileID = null;
					else entity.CompanyLaborProfileID = Convert.ToInt32(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
				
			public System.String ApprovedDateTime
			{
				get
				{
					System.DateTime? data = entity.ApprovedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDateTime = null;
					else entity.ApprovedDateTime = Convert.ToDateTime(value);
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
				
			public System.String InstallmentMethod
			{
				get
				{
					System.Boolean? data = entity.InstallmentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstallmentMethod = null;
					else entity.InstallmentMethod = Convert.ToBoolean(value);
				}
			}
			

			private esEmployeeLoan entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeLoanQuery query)
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
				throw new Exception("esEmployeeLoan can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeLoan : esEmployeeLoan
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
	abstract public class esEmployeeLoanQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLoanMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeLoanID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.EmployeeLoanID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LoanDate
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.LoanDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LoanAmountWithInterest
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.LoanAmountWithInterest, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PercentRateOfInterest
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.PercentRateOfInterest, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem FlatRate
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.FlatRate, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem SRPurposeOfLoan
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.SRPurposeOfLoan, esSystemType.String);
			}
		} 
		
		public esQueryItem NumberOfInstallment
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.NumberOfInstallment, esSystemType.Int32);
			}
		} 
		
		public esQueryItem AmountOfInstallment
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.AmountOfInstallment, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRHRPaymentMethod
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.SRHRPaymentMethod, esSystemType.String);
			}
		} 
		
		public esQueryItem StartPaymentPeriode
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.StartPaymentPeriode, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SalaryCodeInstallment
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.SalaryCodeInstallment, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LoanNo
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.LoanNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CoverageAmount
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.CoverageAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem CompanyLaborProfileID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.CompanyLaborProfileID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ApprovedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem InstallmentMethod
		{
			get
			{
				return new esQueryItem(this, EmployeeLoanMetadata.ColumnNames.InstallmentMethod, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeLoanCollection")]
	public partial class EmployeeLoanCollection : esEmployeeLoanCollection, IEnumerable<EmployeeLoan>
	{
		public EmployeeLoanCollection()
		{

		}
		
		public static implicit operator List<EmployeeLoan>(EmployeeLoanCollection coll)
		{
			List<EmployeeLoan> list = new List<EmployeeLoan>();
			
			foreach (EmployeeLoan emp in coll)
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
				return  EmployeeLoanMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLoanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeLoan(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeLoan();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeLoanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLoanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeLoanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeLoan AddNew()
		{
			EmployeeLoan entity = base.AddNewEntity() as EmployeeLoan;
			
			return entity;
		}

		public EmployeeLoan FindByPrimaryKey(System.Int32 employeeLoanID)
		{
			return base.FindByPrimaryKey(employeeLoanID) as EmployeeLoan;
		}


		#region IEnumerable<EmployeeLoan> Members

		IEnumerator<EmployeeLoan> IEnumerable<EmployeeLoan>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeLoan;
			}
		}

		#endregion
		
		private EmployeeLoanQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeLoan' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeLoan ({EmployeeLoanID})")]
	[Serializable]
	public partial class EmployeeLoan : esEmployeeLoan
	{
		public EmployeeLoan()
		{

		}
	
		public EmployeeLoan(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeLoanMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeLoanQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeLoanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeLoanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeLoanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeLoanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeLoanQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeLoanQuery : esEmployeeLoanQuery
	{
		public EmployeeLoanQuery()
		{

		}		
		
		public EmployeeLoanQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeLoanQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeLoanMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeLoanMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.EmployeeLoanID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.EmployeeLoanID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.LoanDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.LoanDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.Amount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.Amount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.LoanAmountWithInterest, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.LoanAmountWithInterest;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.PercentRateOfInterest, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.PercentRateOfInterest;
			c.NumericPrecision = 3;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.FlatRate, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.FlatRate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.SRPurposeOfLoan, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.SRPurposeOfLoan;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.NumberOfInstallment, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.NumberOfInstallment;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.AmountOfInstallment, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.AmountOfInstallment;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.SRHRPaymentMethod, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.SRHRPaymentMethod;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.StartPaymentPeriode, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.StartPaymentPeriode;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.SalaryCodeInstallment, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.SalaryCodeInstallment;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.LastUpdateByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.LoanNo, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.LoanNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.CoverageAmount, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.CoverageAmount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.CompanyLaborProfileID, 17, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.CompanyLaborProfileID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.Notes, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.IsApproved, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.ApprovedDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.ApprovedByUserID, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeLoanMetadata.ColumnNames.InstallmentMethod, 22, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeLoanMetadata.PropertyNames.InstallmentMethod;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeLoanMetadata Meta()
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
			 public const string EmployeeLoanID = "EmployeeLoanID";
			 public const string PersonID = "PersonID";
			 public const string LoanDate = "LoanDate";
			 public const string Amount = "Amount";
			 public const string LoanAmountWithInterest = "LoanAmountWithInterest";
			 public const string PercentRateOfInterest = "PercentRateOfInterest";
			 public const string FlatRate = "FlatRate";
			 public const string SRPurposeOfLoan = "SRPurposeOfLoan";
			 public const string NumberOfInstallment = "NumberOfInstallment";
			 public const string AmountOfInstallment = "AmountOfInstallment";
			 public const string SRHRPaymentMethod = "SRHRPaymentMethod";
			 public const string StartPaymentPeriode = "StartPaymentPeriode";
			 public const string SalaryCodeInstallment = "SalaryCodeInstallment";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LoanNo = "LoanNo";
			 public const string CoverageAmount = "CoverageAmount";
			 public const string CompanyLaborProfileID = "CompanyLaborProfileID";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDateTime = "ApprovedDateTime";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string InstallmentMethod = "InstallmentMethod";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeLoanID = "EmployeeLoanID";
			 public const string PersonID = "PersonID";
			 public const string LoanDate = "LoanDate";
			 public const string Amount = "Amount";
			 public const string LoanAmountWithInterest = "LoanAmountWithInterest";
			 public const string PercentRateOfInterest = "PercentRateOfInterest";
			 public const string FlatRate = "FlatRate";
			 public const string SRPurposeOfLoan = "SRPurposeOfLoan";
			 public const string NumberOfInstallment = "NumberOfInstallment";
			 public const string AmountOfInstallment = "AmountOfInstallment";
			 public const string SRHRPaymentMethod = "SRHRPaymentMethod";
			 public const string StartPaymentPeriode = "StartPaymentPeriode";
			 public const string SalaryCodeInstallment = "SalaryCodeInstallment";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LoanNo = "LoanNo";
			 public const string CoverageAmount = "CoverageAmount";
			 public const string CompanyLaborProfileID = "CompanyLaborProfileID";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string ApprovedDateTime = "ApprovedDateTime";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string InstallmentMethod = "InstallmentMethod";
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
			lock (typeof(EmployeeLoanMetadata))
			{
				if(EmployeeLoanMetadata.mapDelegates == null)
				{
					EmployeeLoanMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeLoanMetadata.meta == null)
				{
					EmployeeLoanMetadata.meta = new EmployeeLoanMetadata();
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
				

				meta.AddTypeMap("EmployeeLoanID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LoanDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Amount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("LoanAmountWithInterest", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("PercentRateOfInterest", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FlatRate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SRPurposeOfLoan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NumberOfInstallment", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("AmountOfInstallment", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("SRHRPaymentMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartPaymentPeriode", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryCodeInstallment", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LoanNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CoverageAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CompanyLaborProfileID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstallmentMethod", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "EmployeeLoan";
				meta.Destination = "EmployeeLoan";
				
				meta.spInsert = "proc_EmployeeLoanInsert";				
				meta.spUpdate = "proc_EmployeeLoanUpdate";		
				meta.spDelete = "proc_EmployeeLoanDelete";
				meta.spLoadAll = "proc_EmployeeLoanLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeLoanLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeLoanMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
