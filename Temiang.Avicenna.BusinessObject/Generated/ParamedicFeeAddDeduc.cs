/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/3/2022 4:08:42 PM
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
	abstract public class esParamedicFeeAddDeducCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeAddDeducCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeAddDeducCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeAddDeducQuery query)
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
			this.InitQuery(query as esParamedicFeeAddDeducQuery);
		}
		#endregion
			
		virtual public ParamedicFeeAddDeduc DetachEntity(ParamedicFeeAddDeduc entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeAddDeduc;
		}
		
		virtual public ParamedicFeeAddDeduc AttachEntity(ParamedicFeeAddDeduc entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeAddDeduc;
		}
		
		virtual public void Combine(ParamedicFeeAddDeducCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeAddDeduc this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeAddDeduc;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeAddDeduc);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeAddDeduc : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeAddDeducQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeAddDeduc()
		{
		}
	
		public esParamedicFeeAddDeduc(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String transactionNo)
		{
			esParamedicFeeAddDeducQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo==transactionNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "TransactionDate": this.str.TransactionDate = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "SRParamedicFeeAdjustType": this.str.SRParamedicFeeAdjustType = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "IsApproved": this.str.IsApproved = (string)value; break;
						case "VerificationNo": this.str.VerificationNo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdatedByUserID": this.str.LastUpdatedByUserID = (string)value; break;
						case "IsIncludeInTaxCalc": this.str.IsIncludeInTaxCalc = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "SubledgerId": this.str.SubledgerId = (string)value; break;
						case "ChartOfAccountTemplateId": this.str.ChartOfAccountTemplateId = (string)value; break;
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
						case "Price": this.str.Price = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "TransactionDate":
						
							if (value == null || value is System.DateTime)
								this.TransactionDate = (System.DateTime?)value;
							break;
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsIncludeInTaxCalc":
						
							if (value == null || value is System.Boolean)
								this.IsIncludeInTaxCalc = (System.Boolean?)value;
							break;
						case "ChartOfAccountId":
						
							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "SubledgerId":
						
							if (value == null || value is System.Int32)
								this.SubledgerId = (System.Int32?)value;
							break;
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeAddDeduc.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeAddDeducMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeAddDeducMetadata.ColumnNames.TransactionDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.SRParamedicFeeAdjustType
		/// </summary>
		virtual public System.String SRParamedicFeeAdjustType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.SRParamedicFeeAdjustType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.SRParamedicFeeAdjustType, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeAddDeducMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeAddDeducMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeAddDeducMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeAddDeducMetadata.ColumnNames.IsApproved, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.VerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeAddDeducMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeAddDeducMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.LastUpdatedByUserID
		/// </summary>
		virtual public System.String LastUpdatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.LastUpdatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.LastUpdatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.IsIncludeInTaxCalc
		/// </summary>
		virtual public System.Boolean? IsIncludeInTaxCalc
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeAddDeducMetadata.ColumnNames.IsIncludeInTaxCalc);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeAddDeducMetadata.ColumnNames.IsIncludeInTaxCalc, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeAddDeducMetadata.ColumnNames.ChartOfAccountId);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeAddDeducMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.SubledgerId
		/// </summary>
		virtual public System.Int32? SubledgerId
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeAddDeducMetadata.ColumnNames.SubledgerId);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeAddDeducMetadata.ColumnNames.SubledgerId, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.ChartOfAccountTemplateId
		/// </summary>
		virtual public System.String ChartOfAccountTemplateId
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.ChartOfAccountTemplateId);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.ChartOfAccountTemplateId, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeAddDeducMetadata.ColumnNames.PaymentGroupNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeAddDeduc.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeAddDeducMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeAddDeducMetadata.ColumnNames.Price, value);
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
			public esStrings(esParamedicFeeAddDeduc entity)
			{
				this.entity = entity;
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
			public System.String SRParamedicFeeAdjustType
			{
				get
				{
					System.String data = entity.SRParamedicFeeAdjustType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRParamedicFeeAdjustType = null;
					else entity.SRParamedicFeeAdjustType = Convert.ToString(value);
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
			public System.String LastUpdatedByUserID
			{
				get
				{
					System.String data = entity.LastUpdatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdatedByUserID = null;
					else entity.LastUpdatedByUserID = Convert.ToString(value);
				}
			}
			public System.String IsIncludeInTaxCalc
			{
				get
				{
					System.Boolean? data = entity.IsIncludeInTaxCalc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIncludeInTaxCalc = null;
					else entity.IsIncludeInTaxCalc = Convert.ToBoolean(value);
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
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerId
			{
				get
				{
					System.Int32? data = entity.SubledgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerId = null;
					else entity.SubledgerId = Convert.ToInt32(value);
				}
			}
			public System.String ChartOfAccountTemplateId
			{
				get
				{
					System.String data = entity.ChartOfAccountTemplateId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountTemplateId = null;
					else entity.ChartOfAccountTemplateId = Convert.ToString(value);
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
			private esParamedicFeeAddDeduc entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeAddDeducQuery query)
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
				throw new Exception("esParamedicFeeAddDeduc can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeAddDeduc : esParamedicFeeAddDeduc
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeAddDeducQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeAddDeducMetadata.Meta();
			}
		}	
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRParamedicFeeAdjustType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.SRParamedicFeeAdjustType, esSystemType.String);
			}
		} 
			
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
			
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.LastUpdatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsIncludeInTaxCalc
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.IsIncludeInTaxCalc, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem SubledgerId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.SubledgerId, esSystemType.Int32);
			}
		} 
			
		public esQueryItem ChartOfAccountTemplateId
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.ChartOfAccountTemplateId, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
			
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeAddDeducMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeAddDeducCollection")]
	public partial class ParamedicFeeAddDeducCollection : esParamedicFeeAddDeducCollection, IEnumerable< ParamedicFeeAddDeduc>
	{
		public ParamedicFeeAddDeducCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeAddDeduc>(ParamedicFeeAddDeducCollection coll)
		{
			List< ParamedicFeeAddDeduc> list = new List< ParamedicFeeAddDeduc>();
			
			foreach (ParamedicFeeAddDeduc emp in coll)
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
				return  ParamedicFeeAddDeducMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeAddDeducQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeAddDeduc(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeAddDeduc();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeAddDeducQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeAddDeducQuery();
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
		public bool Load(ParamedicFeeAddDeducQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeAddDeduc AddNew()
		{
			ParamedicFeeAddDeduc entity = base.AddNewEntity() as ParamedicFeeAddDeduc;
			
			return entity;		
		}
		public ParamedicFeeAddDeduc FindByPrimaryKey(String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as ParamedicFeeAddDeduc;
		}

		#region IEnumerable< ParamedicFeeAddDeduc> Members

		IEnumerator< ParamedicFeeAddDeduc> IEnumerable< ParamedicFeeAddDeduc>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeAddDeduc;
			}
		}

		#endregion
		
		private ParamedicFeeAddDeducQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeAddDeduc' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeAddDeduc ({TransactionNo})")]
	[Serializable]
	public partial class ParamedicFeeAddDeduc : esParamedicFeeAddDeduc
	{
		public ParamedicFeeAddDeduc()
		{
		}	
	
		public ParamedicFeeAddDeduc(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeAddDeducMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeAddDeducQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeAddDeducQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeAddDeducQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeAddDeducQuery();
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
		public bool Load(ParamedicFeeAddDeducQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeAddDeducQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeAddDeducQuery : esParamedicFeeAddDeducQuery
	{
		public ParamedicFeeAddDeducQuery()
		{

		}		
		
		public ParamedicFeeAddDeducQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeAddDeducQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeAddDeducMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeAddDeducMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.TransactionDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.SRParamedicFeeAdjustType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.SRParamedicFeeAdjustType;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.Amount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.VerificationNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.LastUpdatedByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.LastUpdatedByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.IsIncludeInTaxCalc, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.IsIncludeInTaxCalc;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.TariffComponentID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.ChartOfAccountId, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.SubledgerId, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.SubledgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.ChartOfAccountTemplateId, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.ChartOfAccountTemplateId;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.PaymentGroupNo, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.PaymentGroupNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeAddDeducMetadata.ColumnNames.Price, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeAddDeducMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeAddDeducMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string ParamedicID = "ParamedicID";
			public const string SRParamedicFeeAdjustType = "SRParamedicFeeAdjustType";
			public const string Notes = "Notes";
			public const string Amount = "Amount";
			public const string IsApproved = "IsApproved";
			public const string VerificationNo = "VerificationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdatedByUserID = "LastUpdatedByUserID";
			public const string IsIncludeInTaxCalc = "IsIncludeInTaxCalc";
			public const string TariffComponentID = "TariffComponentID";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubledgerId = "SubledgerId";
			public const string ChartOfAccountTemplateId = "ChartOfAccountTemplateId";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string Price = "Price";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string TransactionNo = "TransactionNo";
			public const string TransactionDate = "TransactionDate";
			public const string ParamedicID = "ParamedicID";
			public const string SRParamedicFeeAdjustType = "SRParamedicFeeAdjustType";
			public const string Notes = "Notes";
			public const string Amount = "Amount";
			public const string IsApproved = "IsApproved";
			public const string VerificationNo = "VerificationNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdatedByUserID = "LastUpdatedByUserID";
			public const string IsIncludeInTaxCalc = "IsIncludeInTaxCalc";
			public const string TariffComponentID = "TariffComponentID";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubledgerId = "SubledgerId";
			public const string ChartOfAccountTemplateId = "ChartOfAccountTemplateId";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string Price = "Price";
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
			lock (typeof(ParamedicFeeAddDeducMetadata))
			{
				if(ParamedicFeeAddDeducMetadata.mapDelegates == null)
				{
					ParamedicFeeAddDeducMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeAddDeducMetadata.meta == null)
				{
					ParamedicFeeAddDeducMetadata.meta = new ParamedicFeeAddDeducMetadata();
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
				
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRParamedicFeeAdjustType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsIncludeInTaxCalc", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ChartOfAccountTemplateId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("decimal", "System.Decimal"));
		

				meta.Source = "ParamedicFeeAddDeduc";
				meta.Destination = "ParamedicFeeAddDeduc";
				meta.spInsert = "proc_ParamedicFeeAddDeducInsert";				
				meta.spUpdate = "proc_ParamedicFeeAddDeducUpdate";		
				meta.spDelete = "proc_ParamedicFeeAddDeducDelete";
				meta.spLoadAll = "proc_ParamedicFeeAddDeducLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeAddDeducLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeAddDeducMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
