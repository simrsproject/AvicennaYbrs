/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/17/2017 7:15:41 PM
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
	abstract public class esCostCalculationCollection : esEntityCollectionWAuditLog
	{
		public esCostCalculationCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "CostCalculationCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esCostCalculationQuery query)
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
			this.InitQuery(query as esCostCalculationQuery);
		}
		#endregion
			
		virtual public CostCalculation DetachEntity(CostCalculation entity)
		{
			return base.DetachEntity(entity) as CostCalculation;
		}
		
		virtual public CostCalculation AttachEntity(CostCalculation entity)
		{
			return base.AttachEntity(entity) as CostCalculation;
		}
		
		virtual public void Combine(CostCalculationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CostCalculation this[int index]
		{
			get
			{
				return base[index] as CostCalculation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CostCalculation);
		}
	}

	[Serializable]
	abstract public class esCostCalculation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCostCalculationQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esCostCalculation()
		{
		}
	
		public esCostCalculation(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String transactionNo, String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, transactionNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String transactionNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, transactionNo, sequenceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, String transactionNo, String sequenceNo)
		{
			esCostCalculationQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo==registrationNo, query.TransactionNo==transactionNo, query.SequenceNo==sequenceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String transactionNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("TransactionNo",transactionNo);
			parms.Add("SequenceNo",sequenceNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "ItemID": this.str.ItemID = (string)value; break;
						case "PatientAmount": this.str.PatientAmount = (string)value; break;
						case "GuarantorAmount": this.str.GuarantorAmount = (string)value; break;
						case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
						case "ParamedicAmount": this.str.ParamedicAmount = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "ParamedicFeeAmount": this.str.ParamedicFeeAmount = (string)value; break;
						case "ParamedicFeePaymentNo": this.str.ParamedicFeePaymentNo = (string)value; break;
						case "IsPackage": this.str.IsPackage = (string)value; break;
						case "ParentNo": this.str.ParentNo = (string)value; break;
						case "IntermBillNo": this.str.IntermBillNo = (string)value; break;
						case "IsChecked": this.str.IsChecked = (string)value; break;
						case "DiscountAmount2": this.str.DiscountAmount2 = (string)value; break;
						case "AmountAdjusted": this.str.AmountAdjusted = (string)value; break;
						case "AdjustedDiscAmount": this.str.AdjustedDiscAmount = (string)value; break;
						case "AdjustedDiscSelection": this.str.AdjustedDiscSelection = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PatientAmount":
						
							if (value == null || value is System.Decimal)
								this.PatientAmount = (System.Decimal?)value;
							break;
						case "GuarantorAmount":
						
							if (value == null || value is System.Decimal)
								this.GuarantorAmount = (System.Decimal?)value;
							break;
						case "DiscountAmount":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
							break;
						case "ParamedicAmount":
						
							if (value == null || value is System.Decimal)
								this.ParamedicAmount = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "ParamedicFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.ParamedicFeeAmount = (System.Decimal?)value;
							break;
						case "IsPackage":
						
							if (value == null || value is System.Boolean)
								this.IsPackage = (System.Boolean?)value;
							break;
						case "IsChecked":
						
							if (value == null || value is System.Boolean)
								this.IsChecked = (System.Boolean?)value;
							break;
						case "DiscountAmount2":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount2 = (System.Decimal?)value;
							break;
						case "AmountAdjusted":
						
							if (value == null || value is System.Decimal)
								this.AmountAdjusted = (System.Decimal?)value;
							break;
						case "AdjustedDiscAmount":
						
							if (value == null || value is System.Decimal)
								this.AdjustedDiscAmount = (System.Decimal?)value;
							break;
						case "AdjustedDiscSelection":
						
							if (value == null || value is System.Int32)
								this.AdjustedDiscSelection = (System.Int32?)value;
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
		/// Maps to CostCalculation.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(CostCalculationMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CostCalculationMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(CostCalculationMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CostCalculationMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(CostCalculationMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.PatientAmount
		/// </summary>
		virtual public System.Decimal? PatientAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationMetadata.ColumnNames.PatientAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationMetadata.ColumnNames.PatientAmount, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.GuarantorAmount
		/// </summary>
		virtual public System.Decimal? GuarantorAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationMetadata.ColumnNames.GuarantorAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationMetadata.ColumnNames.GuarantorAmount, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.ParamedicAmount
		/// </summary>
		virtual public System.Decimal? ParamedicAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationMetadata.ColumnNames.ParamedicAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationMetadata.ColumnNames.ParamedicAmount, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CostCalculationMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CostCalculationMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CostCalculationMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CostCalculationMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.ParamedicFeeAmount
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationMetadata.ColumnNames.ParamedicFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationMetadata.ColumnNames.ParamedicFeeAmount, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.ParamedicFeePaymentNo
		/// </summary>
		virtual public System.String ParamedicFeePaymentNo
		{
			get
			{
				return base.GetSystemString(CostCalculationMetadata.ColumnNames.ParamedicFeePaymentNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationMetadata.ColumnNames.ParamedicFeePaymentNo, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(CostCalculationMetadata.ColumnNames.IsPackage);
			}
			
			set
			{
				base.SetSystemBoolean(CostCalculationMetadata.ColumnNames.IsPackage, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.ParentNo
		/// </summary>
		virtual public System.String ParentNo
		{
			get
			{
				return base.GetSystemString(CostCalculationMetadata.ColumnNames.ParentNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationMetadata.ColumnNames.ParentNo, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.IntermBillNo
		/// </summary>
		virtual public System.String IntermBillNo
		{
			get
			{
				return base.GetSystemString(CostCalculationMetadata.ColumnNames.IntermBillNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationMetadata.ColumnNames.IntermBillNo, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.IsChecked
		/// </summary>
		virtual public System.Boolean? IsChecked
		{
			get
			{
				return base.GetSystemBoolean(CostCalculationMetadata.ColumnNames.IsChecked);
			}
			
			set
			{
				base.SetSystemBoolean(CostCalculationMetadata.ColumnNames.IsChecked, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.DiscountAmount2
		/// </summary>
		virtual public System.Decimal? DiscountAmount2
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationMetadata.ColumnNames.DiscountAmount2);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationMetadata.ColumnNames.DiscountAmount2, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.AmountAdjusted
		/// </summary>
		virtual public System.Decimal? AmountAdjusted
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationMetadata.ColumnNames.AmountAdjusted);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationMetadata.ColumnNames.AmountAdjusted, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.AdjustedDiscAmount
		/// </summary>
		virtual public System.Decimal? AdjustedDiscAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationMetadata.ColumnNames.AdjustedDiscAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationMetadata.ColumnNames.AdjustedDiscAmount, value);
			}
		}
		/// <summary>
		/// Maps to CostCalculation.AdjustedDiscSelection
		/// </summary>
		virtual public System.Int32? AdjustedDiscSelection
		{
			get
			{
				return base.GetSystemInt32(CostCalculationMetadata.ColumnNames.AdjustedDiscSelection);
			}
			
			set
			{
				base.SetSystemInt32(CostCalculationMetadata.ColumnNames.AdjustedDiscSelection, value);
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
			public esStrings(esCostCalculation entity)
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
			public System.String PatientAmount
			{
				get
				{
					System.Decimal? data = entity.PatientAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientAmount = null;
					else entity.PatientAmount = Convert.ToDecimal(value);
				}
			}
			public System.String GuarantorAmount
			{
				get
				{
					System.Decimal? data = entity.GuarantorAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorAmount = null;
					else entity.GuarantorAmount = Convert.ToDecimal(value);
				}
			}
			public System.String DiscountAmount
			{
				get
				{
					System.Decimal? data = entity.DiscountAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountAmount = null;
					else entity.DiscountAmount = Convert.ToDecimal(value);
				}
			}
			public System.String ParamedicAmount
			{
				get
				{
					System.Decimal? data = entity.ParamedicAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicAmount = null;
					else entity.ParamedicAmount = Convert.ToDecimal(value);
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
			public System.String ParamedicFeeAmount
			{
				get
				{
					System.Decimal? data = entity.ParamedicFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicFeeAmount = null;
					else entity.ParamedicFeeAmount = Convert.ToDecimal(value);
				}
			}
			public System.String ParamedicFeePaymentNo
			{
				get
				{
					System.String data = entity.ParamedicFeePaymentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicFeePaymentNo = null;
					else entity.ParamedicFeePaymentNo = Convert.ToString(value);
				}
			}
			public System.String IsPackage
			{
				get
				{
					System.Boolean? data = entity.IsPackage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackage = null;
					else entity.IsPackage = Convert.ToBoolean(value);
				}
			}
			public System.String ParentNo
			{
				get
				{
					System.String data = entity.ParentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentNo = null;
					else entity.ParentNo = Convert.ToString(value);
				}
			}
			public System.String IntermBillNo
			{
				get
				{
					System.String data = entity.IntermBillNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IntermBillNo = null;
					else entity.IntermBillNo = Convert.ToString(value);
				}
			}
			public System.String IsChecked
			{
				get
				{
					System.Boolean? data = entity.IsChecked;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsChecked = null;
					else entity.IsChecked = Convert.ToBoolean(value);
				}
			}
			public System.String DiscountAmount2
			{
				get
				{
					System.Decimal? data = entity.DiscountAmount2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountAmount2 = null;
					else entity.DiscountAmount2 = Convert.ToDecimal(value);
				}
			}
			public System.String AmountAdjusted
			{
				get
				{
					System.Decimal? data = entity.AmountAdjusted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountAdjusted = null;
					else entity.AmountAdjusted = Convert.ToDecimal(value);
				}
			}
			public System.String AdjustedDiscAmount
			{
				get
				{
					System.Decimal? data = entity.AdjustedDiscAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdjustedDiscAmount = null;
					else entity.AdjustedDiscAmount = Convert.ToDecimal(value);
				}
			}
			public System.String AdjustedDiscSelection
			{
				get
				{
					System.Int32? data = entity.AdjustedDiscSelection;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdjustedDiscSelection = null;
					else entity.AdjustedDiscSelection = Convert.ToInt32(value);
				}
			}
			private esCostCalculation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCostCalculationQuery query)
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
				throw new Exception("esCostCalculation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class CostCalculation : esCostCalculation
	{	
	}

	[Serializable]
	abstract public class esCostCalculationQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return CostCalculationMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
			
		public esQueryItem PatientAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.PatientAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem GuarantorAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.GuarantorAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ParamedicAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.ParamedicAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem ParamedicFeeAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem ParamedicFeePaymentNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.ParamedicFeePaymentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ParentNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.ParentNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IntermBillNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.IntermBillNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsChecked
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.IsChecked, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem DiscountAmount2
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.DiscountAmount2, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AmountAdjusted
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.AmountAdjusted, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AdjustedDiscAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.AdjustedDiscAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem AdjustedDiscSelection
		{
			get
			{
				return new esQueryItem(this, CostCalculationMetadata.ColumnNames.AdjustedDiscSelection, esSystemType.Int32);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CostCalculationCollection")]
	public partial class CostCalculationCollection : esCostCalculationCollection, IEnumerable< CostCalculation>
	{
		public CostCalculationCollection()
		{

		}	
		
		public static implicit operator List< CostCalculation>(CostCalculationCollection coll)
		{
			List< CostCalculation> list = new List< CostCalculation>();
			
			foreach (CostCalculation emp in coll)
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
				return  CostCalculationMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CostCalculationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CostCalculation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CostCalculation();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public CostCalculationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CostCalculationQuery();
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
		public bool Load(CostCalculationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public CostCalculation AddNew()
		{
			CostCalculation entity = base.AddNewEntity() as CostCalculation;
			
			return entity;		
		}
		public CostCalculation FindByPrimaryKey(String registrationNo, String transactionNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, transactionNo, sequenceNo) as CostCalculation;
		}

		#region IEnumerable< CostCalculation> Members

		IEnumerator< CostCalculation> IEnumerable< CostCalculation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CostCalculation;
			}
		}

		#endregion
		
		private CostCalculationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CostCalculation' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("CostCalculation ({RegistrationNo, TransactionNo, SequenceNo})")]
	[Serializable]
	public partial class CostCalculation : esCostCalculation
	{
		public CostCalculation()
		{
		}	
	
		public CostCalculation(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CostCalculationMetadata.Meta();
			}
		}	
	
		override protected esCostCalculationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CostCalculationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public CostCalculationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CostCalculationQuery();
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
		public bool Load(CostCalculationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private CostCalculationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class CostCalculationQuery : esCostCalculationQuery
	{
		public CostCalculationQuery()
		{

		}		
		
		public CostCalculationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "CostCalculationQuery";
        }
	}

	[Serializable]
	public partial class CostCalculationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CostCalculationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 6;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.PatientAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationMetadata.PropertyNames.PatientAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.GuarantorAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationMetadata.PropertyNames.GuarantorAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.DiscountAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.ParamedicAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationMetadata.PropertyNames.ParamedicAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CostCalculationMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.ParamedicFeeAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationMetadata.PropertyNames.ParamedicFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.ParamedicFeePaymentNo, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationMetadata.PropertyNames.ParamedicFeePaymentNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.IsPackage, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CostCalculationMetadata.PropertyNames.IsPackage;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.ParentNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationMetadata.PropertyNames.ParentNo;
			c.CharacterMaxLength = 7;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.IntermBillNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationMetadata.PropertyNames.IntermBillNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.IsChecked, 15, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CostCalculationMetadata.PropertyNames.IsChecked;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.DiscountAmount2, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationMetadata.PropertyNames.DiscountAmount2;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.AmountAdjusted, 17, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationMetadata.PropertyNames.AmountAdjusted;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.AdjustedDiscAmount, 18, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationMetadata.PropertyNames.AdjustedDiscAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(CostCalculationMetadata.ColumnNames.AdjustedDiscSelection, 19, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CostCalculationMetadata.PropertyNames.AdjustedDiscSelection;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public CostCalculationMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string PatientAmount = "PatientAmount";
			public const string GuarantorAmount = "GuarantorAmount";
			public const string DiscountAmount = "DiscountAmount";
			public const string ParamedicAmount = "ParamedicAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ParamedicFeeAmount = "ParamedicFeeAmount";
			public const string ParamedicFeePaymentNo = "ParamedicFeePaymentNo";
			public const string IsPackage = "IsPackage";
			public const string ParentNo = "ParentNo";
			public const string IntermBillNo = "IntermBillNo";
			public const string IsChecked = "IsChecked";
			public const string DiscountAmount2 = "DiscountAmount2";
			public const string AmountAdjusted = "AmountAdjusted";
			public const string AdjustedDiscAmount = "AdjustedDiscAmount";
			public const string AdjustedDiscSelection = "AdjustedDiscSelection";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string ItemID = "ItemID";
			public const string PatientAmount = "PatientAmount";
			public const string GuarantorAmount = "GuarantorAmount";
			public const string DiscountAmount = "DiscountAmount";
			public const string ParamedicAmount = "ParamedicAmount";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string ParamedicFeeAmount = "ParamedicFeeAmount";
			public const string ParamedicFeePaymentNo = "ParamedicFeePaymentNo";
			public const string IsPackage = "IsPackage";
			public const string ParentNo = "ParentNo";
			public const string IntermBillNo = "IntermBillNo";
			public const string IsChecked = "IsChecked";
			public const string DiscountAmount2 = "DiscountAmount2";
			public const string AmountAdjusted = "AmountAdjusted";
			public const string AdjustedDiscAmount = "AdjustedDiscAmount";
			public const string AdjustedDiscSelection = "AdjustedDiscSelection";
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
			lock (typeof(CostCalculationMetadata))
			{
				if(CostCalculationMetadata.mapDelegates == null)
				{
					CostCalculationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CostCalculationMetadata.meta == null)
				{
					CostCalculationMetadata.meta = new CostCalculationMetadata();
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
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("GuarantorAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicFeePaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IntermBillNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsChecked", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DiscountAmount2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AmountAdjusted", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AdjustedDiscAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AdjustedDiscSelection", new esTypeMap("int", "System.Int32"));
		

				meta.Source = "CostCalculation";
				meta.Destination = "CostCalculation";
				meta.spInsert = "proc_CostCalculationInsert";				
				meta.spUpdate = "proc_CostCalculationUpdate";		
				meta.spDelete = "proc_CostCalculationDelete";
				meta.spLoadAll = "proc_CostCalculationLoadAll";
				meta.spLoadByPrimaryKey = "proc_CostCalculationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CostCalculationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
