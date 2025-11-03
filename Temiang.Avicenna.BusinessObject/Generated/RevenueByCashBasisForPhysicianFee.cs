/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/3/2015 9:51:05 AM
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
	abstract public class esRevenueByCashBasisForPhysicianFeeCollection : esEntityCollectionWAuditLog
	{
		public esRevenueByCashBasisForPhysicianFeeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RevenueByCashBasisForPhysicianFeeCollection";
		}

		#region Query Logic
		protected void InitQuery(esRevenueByCashBasisForPhysicianFeeQuery query)
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
			this.InitQuery(query as esRevenueByCashBasisForPhysicianFeeQuery);
		}
		#endregion
		
		virtual public RevenueByCashBasisForPhysicianFee DetachEntity(RevenueByCashBasisForPhysicianFee entity)
		{
			return base.DetachEntity(entity) as RevenueByCashBasisForPhysicianFee;
		}
		
		virtual public RevenueByCashBasisForPhysicianFee AttachEntity(RevenueByCashBasisForPhysicianFee entity)
		{
			return base.AttachEntity(entity) as RevenueByCashBasisForPhysicianFee;
		}
		
		virtual public void Combine(RevenueByCashBasisForPhysicianFeeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RevenueByCashBasisForPhysicianFee this[int index]
		{
			get
			{
				return base[index] as RevenueByCashBasisForPhysicianFee;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RevenueByCashBasisForPhysicianFee);
		}
	}



	[Serializable]
	abstract public class esRevenueByCashBasisForPhysicianFee : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRevenueByCashBasisForPhysicianFeeQuery GetDynamicQuery()
		{
			return null;
		}

		public esRevenueByCashBasisForPhysicianFee()
		{

		}

		public esRevenueByCashBasisForPhysicianFee(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.String tariffComponentID, System.String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, paymentReferenceNo, sequenceNo, tariffComponentID, transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, paymentReferenceNo, sequenceNo, tariffComponentID, transactionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.String tariffComponentID, System.String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, paymentReferenceNo, sequenceNo, tariffComponentID, transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, paymentReferenceNo, sequenceNo, tariffComponentID, transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.String tariffComponentID, System.String transactionNo)
		{
			esRevenueByCashBasisForPhysicianFeeQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo, query.PaymentReferenceNo == paymentReferenceNo, query.SequenceNo == sequenceNo, query.TariffComponentID == tariffComponentID, query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.String tariffComponentID, System.String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentNo",paymentNo);			parms.Add("PaymentReferenceNo",paymentReferenceNo);			parms.Add("SequenceNo",sequenceNo);			parms.Add("TariffComponentID",tariffComponentID);			parms.Add("TransactionNo",transactionNo);
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
						case "PaymentDate": this.str.PaymentDate = (string)value; break;							
						case "PaymentNo": this.str.PaymentNo = (string)value; break;							
						case "PaymentReferenceNo": this.str.PaymentReferenceNo = (string)value; break;							
						case "PatientCategory": this.str.PatientCategory = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;							
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "ItemGroup": this.str.ItemGroup = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ItemName": this.str.ItemName = (string)value; break;							
						case "Qty": this.str.Qty = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "Discount": this.str.Discount = (string)value; break;							
						case "TotalIncome": this.str.TotalIncome = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PaymentDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentDate = (System.DateTime?)value;
							break;
						
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						
						case "Qty":
						
							if (value == null || value is System.Decimal)
								this.Qty = (System.Decimal?)value;
							break;
						
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "Discount":
						
							if (value == null || value is System.Decimal)
								this.Discount = (System.Decimal?)value;
							break;
						
						case "TotalIncome":
						
							if (value == null || value is System.Decimal)
								this.TotalIncome = (System.Decimal?)value;
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
		/// Maps to RevenueByCashBasisForPhysicianFee.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentDate);
			}
			
			set
			{
				base.SetSystemDateTime(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentDate, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.PaymentReferenceNo
		/// </summary>
		virtual public System.String PaymentReferenceNo
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentReferenceNo);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.PatientCategory
		/// </summary>
		virtual public System.String PatientCategory
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PatientCategory);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PatientCategory, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.SRRegistrationType, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.ItemGroup
		/// </summary>
		virtual public System.String ItemGroup
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemGroup);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemGroup, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.ItemName
		/// </summary>
		virtual public System.String ItemName
		{
			get
			{
				return base.GetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemName);
			}
			
			set
			{
				base.SetSystemString(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemName, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.Qty
		/// </summary>
		virtual public System.Decimal? Qty
		{
			get
			{
				return base.GetSystemDecimal(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Qty);
			}
			
			set
			{
				base.SetSystemDecimal(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Qty, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Discount, value);
			}
		}
		
		/// <summary>
		/// Maps to RevenueByCashBasisForPhysicianFee.TotalIncome
		/// </summary>
		virtual public System.Decimal? TotalIncome
		{
			get
			{
				return base.GetSystemDecimal(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TotalIncome);
			}
			
			set
			{
				base.SetSystemDecimal(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TotalIncome, value);
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
			public esStrings(esRevenueByCashBasisForPhysicianFee entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PaymentDate
			{
				get
				{
					System.DateTime? data = entity.PaymentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentDate = null;
					else entity.PaymentDate = Convert.ToDateTime(value);
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
				
			public System.String PaymentReferenceNo
			{
				get
				{
					System.String data = entity.PaymentReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentReferenceNo = null;
					else entity.PaymentReferenceNo = Convert.ToString(value);
				}
			}
				
			public System.String PatientCategory
			{
				get
				{
					System.String data = entity.PatientCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientCategory = null;
					else entity.PatientCategory = Convert.ToString(value);
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
				
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
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
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
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
				
			public System.String ItemGroup
			{
				get
				{
					System.String data = entity.ItemGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemGroup = null;
					else entity.ItemGroup = Convert.ToString(value);
				}
			}
				
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
				
			public System.String ItemName
			{
				get
				{
					System.String data = entity.ItemName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemName = null;
					else entity.ItemName = Convert.ToString(value);
				}
			}
				
			public System.String Qty
			{
				get
				{
					System.Decimal? data = entity.Qty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Qty = null;
					else entity.Qty = Convert.ToDecimal(value);
				}
			}
				
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
				}
			}
				
			public System.String Discount
			{
				get
				{
					System.Decimal? data = entity.Discount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount = null;
					else entity.Discount = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalIncome
			{
				get
				{
					System.Decimal? data = entity.TotalIncome;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalIncome = null;
					else entity.TotalIncome = Convert.ToDecimal(value);
				}
			}
			

			private esRevenueByCashBasisForPhysicianFee entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRevenueByCashBasisForPhysicianFeeQuery query)
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
				throw new Exception("esRevenueByCashBasisForPhysicianFee can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RevenueByCashBasisForPhysicianFee : esRevenueByCashBasisForPhysicianFee
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
	abstract public class esRevenueByCashBasisForPhysicianFeeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RevenueByCashBasisForPhysicianFeeMetadata.Meta();
			}
		}	
		

		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PaymentReferenceNo
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientCategory
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PatientCategory, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemGroup
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemGroup, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemName
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemName, esSystemType.String);
			}
		} 
		
		public esQueryItem Qty
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Qty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalIncome
		{
			get
			{
				return new esQueryItem(this, RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TotalIncome, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RevenueByCashBasisForPhysicianFeeCollection")]
	public partial class RevenueByCashBasisForPhysicianFeeCollection : esRevenueByCashBasisForPhysicianFeeCollection, IEnumerable<RevenueByCashBasisForPhysicianFee>
	{
		public RevenueByCashBasisForPhysicianFeeCollection()
		{

		}
		
		public static implicit operator List<RevenueByCashBasisForPhysicianFee>(RevenueByCashBasisForPhysicianFeeCollection coll)
		{
			List<RevenueByCashBasisForPhysicianFee> list = new List<RevenueByCashBasisForPhysicianFee>();
			
			foreach (RevenueByCashBasisForPhysicianFee emp in coll)
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
				return  RevenueByCashBasisForPhysicianFeeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RevenueByCashBasisForPhysicianFeeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RevenueByCashBasisForPhysicianFee(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RevenueByCashBasisForPhysicianFee();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RevenueByCashBasisForPhysicianFeeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RevenueByCashBasisForPhysicianFeeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RevenueByCashBasisForPhysicianFeeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RevenueByCashBasisForPhysicianFee AddNew()
		{
			RevenueByCashBasisForPhysicianFee entity = base.AddNewEntity() as RevenueByCashBasisForPhysicianFee;
			
			return entity;
		}

		public RevenueByCashBasisForPhysicianFee FindByPrimaryKey(System.String paymentNo, System.String paymentReferenceNo, System.String sequenceNo, System.String tariffComponentID, System.String transactionNo)
		{
			return base.FindByPrimaryKey(paymentNo, paymentReferenceNo, sequenceNo, tariffComponentID, transactionNo) as RevenueByCashBasisForPhysicianFee;
		}


		#region IEnumerable<RevenueByCashBasisForPhysicianFee> Members

		IEnumerator<RevenueByCashBasisForPhysicianFee> IEnumerable<RevenueByCashBasisForPhysicianFee>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RevenueByCashBasisForPhysicianFee;
			}
		}

		#endregion
		
		private RevenueByCashBasisForPhysicianFeeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RevenueByCashBasisForPhysicianFee' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RevenueByCashBasisForPhysicianFee ({PaymentNo},{PaymentReferenceNo},{TransactionNo},{SequenceNo},{TariffComponentID})")]
	[Serializable]
	public partial class RevenueByCashBasisForPhysicianFee : esRevenueByCashBasisForPhysicianFee
	{
		public RevenueByCashBasisForPhysicianFee()
		{

		}
	
		public RevenueByCashBasisForPhysicianFee(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RevenueByCashBasisForPhysicianFeeMetadata.Meta();
			}
		}
		
		
		
		override protected esRevenueByCashBasisForPhysicianFeeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RevenueByCashBasisForPhysicianFeeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RevenueByCashBasisForPhysicianFeeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RevenueByCashBasisForPhysicianFeeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RevenueByCashBasisForPhysicianFeeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RevenueByCashBasisForPhysicianFeeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RevenueByCashBasisForPhysicianFeeQuery : esRevenueByCashBasisForPhysicianFeeQuery
	{
		public RevenueByCashBasisForPhysicianFeeQuery()
		{

		}		
		
		public RevenueByCashBasisForPhysicianFeeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RevenueByCashBasisForPhysicianFeeQuery";
        }
		
			
	}


	[Serializable]
	public partial class RevenueByCashBasisForPhysicianFeeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RevenueByCashBasisForPhysicianFeeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentDate, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.PaymentDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PaymentReferenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.PaymentReferenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PatientCategory, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.PatientCategory;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.RegistrationNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.SRRegistrationType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.PatientID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.GuarantorID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ServiceUnitID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ClassID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.ClassID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TransactionNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TransactionDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.SequenceNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 7;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TariffComponentID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ParamedicID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemGroup, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.ItemGroup;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.ItemName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.ItemName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Qty, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.Qty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Price, 19, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.Discount, 20, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(RevenueByCashBasisForPhysicianFeeMetadata.ColumnNames.TotalIncome, 21, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RevenueByCashBasisForPhysicianFeeMetadata.PropertyNames.TotalIncome;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RevenueByCashBasisForPhysicianFeeMetadata Meta()
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
			 public const string PaymentDate = "PaymentDate";
			 public const string PaymentNo = "PaymentNo";
			 public const string PaymentReferenceNo = "PaymentReferenceNo";
			 public const string PatientCategory = "PatientCategory";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRRegistrationType = "SRRegistrationType";
			 public const string PatientID = "PatientID";
			 public const string GuarantorID = "GuarantorID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string ParamedicID = "ParamedicID";
			 public const string ItemGroup = "ItemGroup";
			 public const string ItemID = "ItemID";
			 public const string ItemName = "ItemName";
			 public const string Qty = "Qty";
			 public const string Price = "Price";
			 public const string Discount = "Discount";
			 public const string TotalIncome = "TotalIncome";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PaymentDate = "PaymentDate";
			 public const string PaymentNo = "PaymentNo";
			 public const string PaymentReferenceNo = "PaymentReferenceNo";
			 public const string PatientCategory = "PatientCategory";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRRegistrationType = "SRRegistrationType";
			 public const string PatientID = "PatientID";
			 public const string GuarantorID = "GuarantorID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ClassID = "ClassID";
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string SequenceNo = "SequenceNo";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string ParamedicID = "ParamedicID";
			 public const string ItemGroup = "ItemGroup";
			 public const string ItemID = "ItemID";
			 public const string ItemName = "ItemName";
			 public const string Qty = "Qty";
			 public const string Price = "Price";
			 public const string Discount = "Discount";
			 public const string TotalIncome = "TotalIncome";
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
			lock (typeof(RevenueByCashBasisForPhysicianFeeMetadata))
			{
				if(RevenueByCashBasisForPhysicianFeeMetadata.mapDelegates == null)
				{
					RevenueByCashBasisForPhysicianFeeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RevenueByCashBasisForPhysicianFeeMetadata.meta == null)
				{
					RevenueByCashBasisForPhysicianFeeMetadata.meta = new RevenueByCashBasisForPhysicianFeeMetadata();
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
				

				meta.AddTypeMap("PaymentDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Qty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TotalIncome", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "RevenueByCashBasisForPhysicianFee";
				meta.Destination = "RevenueByCashBasisForPhysicianFee";
				
				meta.spInsert = "proc_RevenueByCashBasisForPhysicianFeeInsert";				
				meta.spUpdate = "proc_RevenueByCashBasisForPhysicianFeeUpdate";		
				meta.spDelete = "proc_RevenueByCashBasisForPhysicianFeeDelete";
				meta.spLoadAll = "proc_RevenueByCashBasisForPhysicianFeeLoadAll";
				meta.spLoadByPrimaryKey = "proc_RevenueByCashBasisForPhysicianFeeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RevenueByCashBasisForPhysicianFeeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
