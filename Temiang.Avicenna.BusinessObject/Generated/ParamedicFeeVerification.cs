/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/2/2021 10:06:10 AM
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
	abstract public class esParamedicFeeVerificationCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeVerificationCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeVerificationCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeVerificationQuery query)
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
			this.InitQuery(query as esParamedicFeeVerificationQuery);
		}
		#endregion
			
		virtual public ParamedicFeeVerification DetachEntity(ParamedicFeeVerification entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeVerification;
		}
		
		virtual public ParamedicFeeVerification AttachEntity(ParamedicFeeVerification entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeVerification;
		}
		
		virtual public void Combine(ParamedicFeeVerificationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeVerification this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeVerification;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeVerification);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeVerification : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeVerificationQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeVerification()
		{
		}
	
		public esParamedicFeeVerification(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String verificationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(verificationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(verificationNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String verificationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(verificationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(verificationNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String verificationNo)
		{
			esParamedicFeeVerificationQuery query = this.GetDynamicQuery();
			query.Where(query.VerificationNo==verificationNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String verificationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("VerificationNo",verificationNo);
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
						case "VerificationNo": this.str.VerificationNo = (string)value; break;
						case "VerificationDate": this.str.VerificationDate = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "TaxPeriod": this.str.TaxPeriod = (string)value; break;
						case "VerificationAmount": this.str.VerificationAmount = (string)value; break;
						case "TaxAmount": this.str.TaxAmount = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "PaymentNo": this.str.PaymentNo = (string)value; break;
						case "VerificationTaxAmount": this.str.VerificationTaxAmount = (string)value; break;
						case "ApprovedDate": this.str.ApprovedDate = (string)value; break;
						case "ApprovedByUserID": this.str.ApprovedByUserID = (string)value; break;
						case "SumDeductionAmountAfterTax": this.str.SumDeductionAmountAfterTax = (string)value; break;
						case "PlanningPaymentDate": this.str.PlanningPaymentDate = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "VerificationDate":
						
							if (value == null || value is System.DateTime)
								this.VerificationDate = (System.DateTime?)value;
							break;
						case "StartDate":
						
							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":
						
							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						case "TaxPeriod":
						
							if (value == null || value is System.Int16)
								this.TaxPeriod = (System.Int16?)value;
							break;
						case "VerificationAmount":
						
							if (value == null || value is System.Decimal)
								this.VerificationAmount = (System.Decimal?)value;
							break;
						case "TaxAmount":
						
							if (value == null || value is System.Decimal)
								this.TaxAmount = (System.Decimal?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "VerificationTaxAmount":
						
							if (value == null || value is System.Decimal)
								this.VerificationTaxAmount = (System.Decimal?)value;
							break;
						case "ApprovedDate":
						
							if (value == null || value is System.DateTime)
								this.ApprovedDate = (System.DateTime?)value;
							break;
						case "SumDeductionAmountAfterTax":
						
							if (value == null || value is System.Decimal)
								this.SumDeductionAmountAfterTax = (System.Decimal?)value;
							break;
						case "PlanningPaymentDate":
						
							if (value == null || value is System.DateTime)
								this.PlanningPaymentDate = (System.DateTime?)value;
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
		/// Maps to ParamedicFeeVerification.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.VerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.VerificationDate
		/// </summary>
		virtual public System.DateTime? VerificationDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.VerificationDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.VerificationDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.StartDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.EndDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.TaxPeriod
		/// </summary>
		virtual public System.Int16? TaxPeriod
		{
			get
			{
				return base.GetSystemInt16(ParamedicFeeVerificationMetadata.ColumnNames.TaxPeriod);
			}
			
			set
			{
				base.SetSystemInt16(ParamedicFeeVerificationMetadata.ColumnNames.TaxPeriod, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.VerificationAmount
		/// </summary>
		virtual public System.Decimal? VerificationAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeVerificationMetadata.ColumnNames.VerificationAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeVerificationMetadata.ColumnNames.VerificationAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.TaxAmount
		/// </summary>
		virtual public System.Decimal? TaxAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeVerificationMetadata.ColumnNames.TaxAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeVerificationMetadata.ColumnNames.TaxAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeVerificationMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeVerificationMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeVerificationMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeVerificationMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.PaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.VerificationTaxAmount
		/// </summary>
		virtual public System.Decimal? VerificationTaxAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeVerificationMetadata.ColumnNames.VerificationTaxAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeVerificationMetadata.ColumnNames.VerificationTaxAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.ApprovedDate
		/// </summary>
		virtual public System.DateTime? ApprovedDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.ApprovedDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.ApprovedDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.ApprovedByUserID
		/// </summary>
		virtual public System.String ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeVerificationMetadata.ColumnNames.ApprovedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.SumDeductionAmountAfterTax
		/// </summary>
		virtual public System.Decimal? SumDeductionAmountAfterTax
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeVerificationMetadata.ColumnNames.SumDeductionAmountAfterTax);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeVerificationMetadata.ColumnNames.SumDeductionAmountAfterTax, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeVerification.PlanningPaymentDate
		/// </summary>
		virtual public System.DateTime? PlanningPaymentDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.PlanningPaymentDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeVerificationMetadata.ColumnNames.PlanningPaymentDate, value);
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
			public esStrings(esParamedicFeeVerification entity)
			{
				this.entity = entity;
			}
			public System.String VerificationNo
			{
				get
				{
					System.String data = entity.VerificationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNo = null;
					else entity.VerificationNo = Convert.ToString(value);
				}
			}
			public System.String VerificationDate
			{
				get
				{
					System.DateTime? data = entity.VerificationDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationDate = null;
					else entity.VerificationDate = Convert.ToDateTime(value);
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
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
			public System.String TaxPeriod
			{
				get
				{
					System.Int16? data = entity.TaxPeriod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxPeriod = null;
					else entity.TaxPeriod = Convert.ToInt16(value);
				}
			}
			public System.String VerificationAmount
			{
				get
				{
					System.Decimal? data = entity.VerificationAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationAmount = null;
					else entity.VerificationAmount = Convert.ToDecimal(value);
				}
			}
			public System.String TaxAmount
			{
				get
				{
					System.Decimal? data = entity.TaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxAmount = null;
					else entity.TaxAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
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
			public System.String PaymentNo
			{
				get
				{
					System.String data = entity.PaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentNo = null;
					else entity.PaymentNo = Convert.ToString(value);
				}
			}
			public System.String VerificationTaxAmount
			{
				get
				{
					System.Decimal? data = entity.VerificationTaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationTaxAmount = null;
					else entity.VerificationTaxAmount = Convert.ToDecimal(value);
				}
			}
			public System.String ApprovedDate
			{
				get
				{
					System.DateTime? data = entity.ApprovedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApprovedDate = null;
					else entity.ApprovedDate = Convert.ToDateTime(value);
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
			public System.String SumDeductionAmountAfterTax
			{
				get
				{
					System.Decimal? data = entity.SumDeductionAmountAfterTax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SumDeductionAmountAfterTax = null;
					else entity.SumDeductionAmountAfterTax = Convert.ToDecimal(value);
				}
			}
			public System.String PlanningPaymentDate
			{
				get
				{
					System.DateTime? data = entity.PlanningPaymentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlanningPaymentDate = null;
					else entity.PlanningPaymentDate = Convert.ToDateTime(value);
				}
			}
			private esParamedicFeeVerification entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeVerificationQuery query)
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
				throw new Exception("esParamedicFeeVerification can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeVerification : esParamedicFeeVerification
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeVerificationQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeVerificationMetadata.Meta();
			}
		}	
			
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem VerificationDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.VerificationDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem TaxPeriod
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.TaxPeriod, esSystemType.Int16);
			}
		} 
			
		public esQueryItem VerificationAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.VerificationAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TaxAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.TaxAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem VerificationTaxAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.VerificationTaxAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ApprovedDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.ApprovedDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ApprovedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.ApprovedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem SumDeductionAmountAfterTax
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.SumDeductionAmountAfterTax, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PlanningPaymentDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeVerificationMetadata.ColumnNames.PlanningPaymentDate, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeVerificationCollection")]
	public partial class ParamedicFeeVerificationCollection : esParamedicFeeVerificationCollection, IEnumerable< ParamedicFeeVerification>
	{
		public ParamedicFeeVerificationCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeVerification>(ParamedicFeeVerificationCollection coll)
		{
			List< ParamedicFeeVerification> list = new List< ParamedicFeeVerification>();
			
			foreach (ParamedicFeeVerification emp in coll)
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
				return  ParamedicFeeVerificationMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeVerificationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeVerification(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeVerification();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeVerificationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeVerificationQuery();
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
		public bool Load(ParamedicFeeVerificationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeVerification AddNew()
		{
			ParamedicFeeVerification entity = base.AddNewEntity() as ParamedicFeeVerification;
			
			return entity;		
		}
		public ParamedicFeeVerification FindByPrimaryKey(String verificationNo)
		{
			return base.FindByPrimaryKey(verificationNo) as ParamedicFeeVerification;
		}

		#region IEnumerable< ParamedicFeeVerification> Members

		IEnumerator< ParamedicFeeVerification> IEnumerable< ParamedicFeeVerification>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeVerification;
			}
		}

		#endregion
		
		private ParamedicFeeVerificationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeVerification' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeVerification ({VerificationNo})")]
	[Serializable]
	public partial class ParamedicFeeVerification : esParamedicFeeVerification
	{
		public ParamedicFeeVerification()
		{
		}	
	
		public ParamedicFeeVerification(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeVerificationMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeVerificationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeVerificationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeVerificationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeVerificationQuery();
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
		public bool Load(ParamedicFeeVerificationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeVerificationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeVerificationQuery : esParamedicFeeVerificationQuery
	{
		public ParamedicFeeVerificationQuery()
		{

		}		
		
		public ParamedicFeeVerificationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeVerificationQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeVerificationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeVerificationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.VerificationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.VerificationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.VerificationDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.VerificationDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.StartDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.EndDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.TaxPeriod, 5, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.TaxPeriod;
			c.NumericPrecision = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.VerificationAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.VerificationAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.TaxAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.TaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.IsVoid, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.IsApproved, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.IsApproved;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.RegistrationNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.PaymentNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.PaymentNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.VerificationTaxAmount, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.VerificationTaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.ApprovedDate, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.ApprovedDate;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.ApprovedByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.SumDeductionAmountAfterTax, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.SumDeductionAmountAfterTax;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeVerificationMetadata.ColumnNames.PlanningPaymentDate, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeVerificationMetadata.PropertyNames.PlanningPaymentDate;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeVerificationMetadata Meta()
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
			public const string VerificationNo = "VerificationNo";
			public const string VerificationDate = "VerificationDate";
			public const string ParamedicID = "ParamedicID";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string TaxPeriod = "TaxPeriod";
			public const string VerificationAmount = "VerificationAmount";
			public const string TaxAmount = "TaxAmount";
			public const string IsVoid = "IsVoid";
			public const string IsApproved = "IsApproved";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string RegistrationNo = "RegistrationNo";
			public const string PaymentNo = "PaymentNo";
			public const string VerificationTaxAmount = "VerificationTaxAmount";
			public const string ApprovedDate = "ApprovedDate";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string SumDeductionAmountAfterTax = "SumDeductionAmountAfterTax";
			public const string PlanningPaymentDate = "PlanningPaymentDate";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string VerificationNo = "VerificationNo";
			public const string VerificationDate = "VerificationDate";
			public const string ParamedicID = "ParamedicID";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string TaxPeriod = "TaxPeriod";
			public const string VerificationAmount = "VerificationAmount";
			public const string TaxAmount = "TaxAmount";
			public const string IsVoid = "IsVoid";
			public const string IsApproved = "IsApproved";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string RegistrationNo = "RegistrationNo";
			public const string PaymentNo = "PaymentNo";
			public const string VerificationTaxAmount = "VerificationTaxAmount";
			public const string ApprovedDate = "ApprovedDate";
			public const string ApprovedByUserID = "ApprovedByUserID";
			public const string SumDeductionAmountAfterTax = "SumDeductionAmountAfterTax";
			public const string PlanningPaymentDate = "PlanningPaymentDate";
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
			lock (typeof(ParamedicFeeVerificationMetadata))
			{
				if(ParamedicFeeVerificationMetadata.mapDelegates == null)
				{
					ParamedicFeeVerificationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeVerificationMetadata.meta == null)
				{
					ParamedicFeeVerificationMetadata.meta = new ParamedicFeeVerificationMetadata();
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
				
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("TaxPeriod", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("VerificationAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationTaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ApprovedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SumDeductionAmountAfterTax", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PlanningPaymentDate", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ParamedicFeeVerification";
				meta.Destination = "ParamedicFeeVerification";
				meta.spInsert = "proc_ParamedicFeeVerificationInsert";				
				meta.spUpdate = "proc_ParamedicFeeVerificationUpdate";		
				meta.spDelete = "proc_ParamedicFeeVerificationDelete";
				meta.spLoadAll = "proc_ParamedicFeeVerificationLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeVerificationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeVerificationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
