/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 26/02/2024 09:03:50
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
	abstract public class esParamedicFeeTaxCalculationCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeTaxCalculationCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeTaxCalculationCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeTaxCalculationQuery query)
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
			this.InitQuery(query as esParamedicFeeTaxCalculationQuery);
		}
		#endregion
			
		virtual public ParamedicFeeTaxCalculation DetachEntity(ParamedicFeeTaxCalculation entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeTaxCalculation;
		}
		
		virtual public ParamedicFeeTaxCalculation AttachEntity(ParamedicFeeTaxCalculation entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeTaxCalculation;
		}
		
		virtual public void Combine(ParamedicFeeTaxCalculationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeTaxCalculation this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeTaxCalculation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeTaxCalculation);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeTaxCalculation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeTaxCalculationQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeTaxCalculation()
		{
		}
	
		public esParamedicFeeTaxCalculation(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int64 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int64 id)
		{
			esParamedicFeeTaxCalculationQuery query = this.GetDynamicQuery();
			query.Where(query.id==id);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int64 id)
		{
			esParameters parms = new esParameters();
			parms.Add("id",id);
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
						case "id": this.str.id = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "SRPphType": this.str.SRPphType = (string)value; break;
						case "Period": this.str.Period = (string)value; break;
						case "IsNpwp": this.str.IsNpwp = (string)value; break;
						case "TaxInPercent": this.str.TaxInPercent = (string)value; break;
						case "FeeAmount": this.str.FeeAmount = (string)value; break;
						case "FeeAmountAccumulated": this.str.FeeAmountAccumulated = (string)value; break;
						case "TaxAmount": this.str.TaxAmount = (string)value; break;
						case "TaxAmountAccumulated": this.str.TaxAmountAccumulated = (string)value; break;
						case "VerificationNo": this.str.VerificationNo = (string)value; break;
						case "InsertByUserID": this.str.InsertByUserID = (string)value; break;
						case "InsertDateTime": this.str.InsertDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "JournalID": this.str.JournalID = (string)value; break;
						case "IsTaxFromPayment": this.str.IsTaxFromPayment = (string)value; break;
						case "IsPaymentApproved": this.str.IsPaymentApproved = (string)value; break;
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
						case "PeriodMonth": this.str.PeriodMonth = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "id":
						
							if (value == null || value is System.Int64)
								this.id = (System.Int64?)value;
							break;
						case "Period":
						
							if (value == null || value is System.Int16)
								this.Period = (System.Int16?)value;
							break;
						case "IsNpwp":
						
							if (value == null || value is System.Boolean)
								this.IsNpwp = (System.Boolean?)value;
							break;
						case "TaxInPercent":
						
							if (value == null || value is System.Decimal)
								this.TaxInPercent = (System.Decimal?)value;
							break;
						case "FeeAmount":
						
							if (value == null || value is System.Decimal)
								this.FeeAmount = (System.Decimal?)value;
							break;
						case "FeeAmountAccumulated":
						
							if (value == null || value is System.Decimal)
								this.FeeAmountAccumulated = (System.Decimal?)value;
							break;
						case "TaxAmount":
						
							if (value == null || value is System.Decimal)
								this.TaxAmount = (System.Decimal?)value;
							break;
						case "TaxAmountAccumulated":
						
							if (value == null || value is System.Decimal)
								this.TaxAmountAccumulated = (System.Decimal?)value;
							break;
						case "InsertDateTime":
						
							if (value == null || value is System.DateTime)
								this.InsertDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "JournalID":
						
							if (value == null || value is System.Int64)
								this.JournalID = (System.Int64?)value;
							break;
						case "IsTaxFromPayment":
						
							if (value == null || value is System.Boolean)
								this.IsTaxFromPayment = (System.Boolean?)value;
							break;
						case "IsPaymentApproved":
						
							if (value == null || value is System.Boolean)
								this.IsPaymentApproved = (System.Boolean?)value;
							break;
						case "PeriodMonth":
						
							if (value == null || value is System.Int16)
								this.PeriodMonth = (System.Int16?)value;
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
		/// Maps to ParamedicFeeTaxCalculation.id
		/// </summary>
		virtual public System.Int64? id
		{
			get
			{
				return base.GetSystemInt64(ParamedicFeeTaxCalculationMetadata.ColumnNames.id);
			}
			
			set
			{
				base.SetSystemInt64(ParamedicFeeTaxCalculationMetadata.ColumnNames.id, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.SRPphType
		/// </summary>
		virtual public System.String SRPphType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.SRPphType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.SRPphType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.Period
		/// </summary>
		virtual public System.Int16? Period
		{
			get
			{
				return base.GetSystemInt16(ParamedicFeeTaxCalculationMetadata.ColumnNames.Period);
			}
			
			set
			{
				base.SetSystemInt16(ParamedicFeeTaxCalculationMetadata.ColumnNames.Period, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.IsNpwp
		/// </summary>
		virtual public System.Boolean? IsNpwp
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsNpwp);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsNpwp, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.TaxInPercent
		/// </summary>
		virtual public System.Decimal? TaxInPercent
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxInPercent);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxInPercent, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.FeeAmount
		/// </summary>
		virtual public System.Decimal? FeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.FeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.FeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.FeeAmountAccumulated
		/// </summary>
		virtual public System.Decimal? FeeAmountAccumulated
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.FeeAmountAccumulated);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.FeeAmountAccumulated, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.TaxAmount
		/// </summary>
		virtual public System.Decimal? TaxAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.TaxAmountAccumulated
		/// </summary>
		virtual public System.Decimal? TaxAmountAccumulated
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxAmountAccumulated);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxAmountAccumulated, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.VerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.InsertByUserID
		/// </summary>
		virtual public System.String InsertByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.InsertByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.InsertByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.InsertDateTime
		/// </summary>
		virtual public System.DateTime? InsertDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTaxCalculationMetadata.ColumnNames.InsertDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTaxCalculationMetadata.ColumnNames.InsertDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTaxCalculationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTaxCalculationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.JournalID
		/// </summary>
		virtual public System.Int64? JournalID
		{
			get
			{
				return base.GetSystemInt64(ParamedicFeeTaxCalculationMetadata.ColumnNames.JournalID);
			}
			
			set
			{
				base.SetSystemInt64(ParamedicFeeTaxCalculationMetadata.ColumnNames.JournalID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.IsTaxFromPayment
		/// </summary>
		virtual public System.Boolean? IsTaxFromPayment
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsTaxFromPayment);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsTaxFromPayment, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.IsPaymentApproved
		/// </summary>
		virtual public System.Boolean? IsPaymentApproved
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsPaymentApproved);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsPaymentApproved, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationMetadata.ColumnNames.PaymentGroupNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculation.PeriodMonth
		/// </summary>
		virtual public System.Int16? PeriodMonth
		{
			get
			{
				return base.GetSystemInt16(ParamedicFeeTaxCalculationMetadata.ColumnNames.PeriodMonth);
			}
			
			set
			{
				base.SetSystemInt16(ParamedicFeeTaxCalculationMetadata.ColumnNames.PeriodMonth, value);
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
			public esStrings(esParamedicFeeTaxCalculation entity)
			{
				this.entity = entity;
			}
			public System.String id
			{
				get
				{
					System.Int64? data = entity.id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.id = null;
					else entity.id = Convert.ToInt64(value);
				}
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
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
			public System.String SRPphType
			{
				get
				{
					System.String data = entity.SRPphType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPphType = null;
					else entity.SRPphType = Convert.ToString(value);
				}
			}
			public System.String Period
			{
				get
				{
					System.Int16? data = entity.Period;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Period = null;
					else entity.Period = Convert.ToInt16(value);
				}
			}
			public System.String IsNpwp
			{
				get
				{
					System.Boolean? data = entity.IsNpwp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNpwp = null;
					else entity.IsNpwp = Convert.ToBoolean(value);
				}
			}
			public System.String TaxInPercent
			{
				get
				{
					System.Decimal? data = entity.TaxInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxInPercent = null;
					else entity.TaxInPercent = Convert.ToDecimal(value);
				}
			}
			public System.String FeeAmount
			{
				get
				{
					System.Decimal? data = entity.FeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmount = null;
					else entity.FeeAmount = Convert.ToDecimal(value);
				}
			}
			public System.String FeeAmountAccumulated
			{
				get
				{
					System.Decimal? data = entity.FeeAmountAccumulated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmountAccumulated = null;
					else entity.FeeAmountAccumulated = Convert.ToDecimal(value);
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
			public System.String TaxAmountAccumulated
			{
				get
				{
					System.Decimal? data = entity.TaxAmountAccumulated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxAmountAccumulated = null;
					else entity.TaxAmountAccumulated = Convert.ToDecimal(value);
				}
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
			public System.String JournalID
			{
				get
				{
					System.Int64? data = entity.JournalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalID = null;
					else entity.JournalID = Convert.ToInt64(value);
				}
			}
			public System.String IsTaxFromPayment
			{
				get
				{
					System.Boolean? data = entity.IsTaxFromPayment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTaxFromPayment = null;
					else entity.IsTaxFromPayment = Convert.ToBoolean(value);
				}
			}
			public System.String IsPaymentApproved
			{
				get
				{
					System.Boolean? data = entity.IsPaymentApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPaymentApproved = null;
					else entity.IsPaymentApproved = Convert.ToBoolean(value);
				}
			}
			public System.String PaymentGroupNo
			{
				get
				{
					System.String data = entity.PaymentGroupNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentGroupNo = null;
					else entity.PaymentGroupNo = Convert.ToString(value);
				}
			}
			public System.String PeriodMonth
			{
				get
				{
					System.Int16? data = entity.PeriodMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodMonth = null;
					else entity.PeriodMonth = Convert.ToInt16(value);
				}
			}
			private esParamedicFeeTaxCalculation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeTaxCalculationQuery query)
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
				throw new Exception("esParamedicFeeTaxCalculation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeTaxCalculation : esParamedicFeeTaxCalculation
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeTaxCalculationQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTaxCalculationMetadata.Meta();
			}
		}	
			
		public esQueryItem id
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.id, esSystemType.Int64);
			}
		} 
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRPphType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.SRPphType, esSystemType.String);
			}
		} 
			
		public esQueryItem Period
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.Period, esSystemType.Int16);
			}
		} 
			
		public esQueryItem IsNpwp
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.IsNpwp, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem TaxInPercent
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxInPercent, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.FeeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem FeeAmountAccumulated
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.FeeAmountAccumulated, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TaxAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TaxAmountAccumulated
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxAmountAccumulated, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem InsertByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.InsertByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem InsertDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.InsertDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem JournalID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.JournalID, esSystemType.Int64);
			}
		} 
			
		public esQueryItem IsTaxFromPayment
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.IsTaxFromPayment, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsPaymentApproved
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.IsPaymentApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PeriodMonth
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationMetadata.ColumnNames.PeriodMonth, esSystemType.Int16);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeTaxCalculationCollection")]
	public partial class ParamedicFeeTaxCalculationCollection : esParamedicFeeTaxCalculationCollection, IEnumerable< ParamedicFeeTaxCalculation>
	{
		public ParamedicFeeTaxCalculationCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeTaxCalculation>(ParamedicFeeTaxCalculationCollection coll)
		{
			List< ParamedicFeeTaxCalculation> list = new List< ParamedicFeeTaxCalculation>();
			
			foreach (ParamedicFeeTaxCalculation emp in coll)
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
				return  ParamedicFeeTaxCalculationMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTaxCalculationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeTaxCalculation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeTaxCalculation();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeTaxCalculationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTaxCalculationQuery();
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
		public bool Load(ParamedicFeeTaxCalculationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeTaxCalculation AddNew()
		{
			ParamedicFeeTaxCalculation entity = base.AddNewEntity() as ParamedicFeeTaxCalculation;
			
			return entity;		
		}
		public ParamedicFeeTaxCalculation FindByPrimaryKey(Int64 id)
		{
			return base.FindByPrimaryKey(id) as ParamedicFeeTaxCalculation;
		}

		#region IEnumerable< ParamedicFeeTaxCalculation> Members

		IEnumerator< ParamedicFeeTaxCalculation> IEnumerable< ParamedicFeeTaxCalculation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeTaxCalculation;
			}
		}

		#endregion
		
		private ParamedicFeeTaxCalculationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeTaxCalculation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeTaxCalculation ({id})")]
	[Serializable]
	public partial class ParamedicFeeTaxCalculation : esParamedicFeeTaxCalculation
	{
		public ParamedicFeeTaxCalculation()
		{
		}	
	
		public ParamedicFeeTaxCalculation(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTaxCalculationMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeTaxCalculationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTaxCalculationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeTaxCalculationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTaxCalculationQuery();
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
		public bool Load(ParamedicFeeTaxCalculationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeTaxCalculationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeTaxCalculationQuery : esParamedicFeeTaxCalculationQuery
	{
		public ParamedicFeeTaxCalculationQuery()
		{

		}		
		
		public ParamedicFeeTaxCalculationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeTaxCalculationQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeTaxCalculationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeTaxCalculationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.id, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 7;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.ParamedicID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.SRPphType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.SRPphType;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.Period, 6, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.Period;
			c.NumericPrecision = 5;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsNpwp, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.IsNpwp;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxInPercent, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.TaxInPercent;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.FeeAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.FeeAmount;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.FeeAmountAccumulated, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.FeeAmountAccumulated;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.TaxAmount;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.TaxAmountAccumulated, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.TaxAmountAccumulated;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.VerificationNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.InsertByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.InsertByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.InsertDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.InsertDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.LastUpdateDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.JournalID, 18, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.JournalID;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsTaxFromPayment, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.IsTaxFromPayment;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.IsPaymentApproved, 20, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.IsPaymentApproved;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.PaymentGroupNo, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.PaymentGroupNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationMetadata.ColumnNames.PeriodMonth, 22, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicFeeTaxCalculationMetadata.PropertyNames.PeriodMonth;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeTaxCalculationMetadata Meta()
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
			public const string id = "id";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string ParamedicID = "ParamedicID";
			public const string SRPphType = "SRPphType";
			public const string Period = "Period";
			public const string IsNpwp = "IsNpwp";
			public const string TaxInPercent = "TaxInPercent";
			public const string FeeAmount = "FeeAmount";
			public const string FeeAmountAccumulated = "FeeAmountAccumulated";
			public const string TaxAmount = "TaxAmount";
			public const string TaxAmountAccumulated = "TaxAmountAccumulated";
			public const string VerificationNo = "VerificationNo";
			public const string InsertByUserID = "InsertByUserID";
			public const string InsertDateTime = "InsertDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string JournalID = "JournalID";
			public const string IsTaxFromPayment = "IsTaxFromPayment";
			public const string IsPaymentApproved = "IsPaymentApproved";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string PeriodMonth = "PeriodMonth";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string id = "id";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string ParamedicID = "ParamedicID";
			public const string SRPphType = "SRPphType";
			public const string Period = "Period";
			public const string IsNpwp = "IsNpwp";
			public const string TaxInPercent = "TaxInPercent";
			public const string FeeAmount = "FeeAmount";
			public const string FeeAmountAccumulated = "FeeAmountAccumulated";
			public const string TaxAmount = "TaxAmount";
			public const string TaxAmountAccumulated = "TaxAmountAccumulated";
			public const string VerificationNo = "VerificationNo";
			public const string InsertByUserID = "InsertByUserID";
			public const string InsertDateTime = "InsertDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string JournalID = "JournalID";
			public const string IsTaxFromPayment = "IsTaxFromPayment";
			public const string IsPaymentApproved = "IsPaymentApproved";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string PeriodMonth = "PeriodMonth";
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
			lock (typeof(ParamedicFeeTaxCalculationMetadata))
			{
				if(ParamedicFeeTaxCalculationMetadata.mapDelegates == null)
				{
					ParamedicFeeTaxCalculationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeTaxCalculationMetadata.meta == null)
				{
					ParamedicFeeTaxCalculationMetadata.meta = new ParamedicFeeTaxCalculationMetadata();
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
				
				meta.AddTypeMap("id", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPphType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Period", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsNpwp", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TaxInPercent", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("FeeAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("FeeAmountAccumulated", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TaxAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TaxAmountAccumulated", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsertByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InsertDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("JournalID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("IsTaxFromPayment", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPaymentApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodMonth", new esTypeMap("smallint", "System.Int16"));
		

				meta.Source = "ParamedicFeeTaxCalculation";
				meta.Destination = "ParamedicFeeTaxCalculation";
				meta.spInsert = "proc_ParamedicFeeTaxCalculationInsert";				
				meta.spUpdate = "proc_ParamedicFeeTaxCalculationUpdate";		
				meta.spDelete = "proc_ParamedicFeeTaxCalculationDelete";
				meta.spLoadAll = "proc_ParamedicFeeTaxCalculationLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeTaxCalculationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeTaxCalculationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
