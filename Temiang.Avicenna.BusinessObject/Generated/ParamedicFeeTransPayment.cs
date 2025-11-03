/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/1/2023 11:49:09 AM
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
	abstract public class esParamedicFeeTransPaymentCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeTransPaymentCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeeTransPaymentCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeeTransPaymentQuery query)
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
			this.InitQuery(query as esParamedicFeeTransPaymentQuery);
		}
		#endregion
			
		virtual public ParamedicFeeTransPayment DetachEntity(ParamedicFeeTransPayment entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeTransPayment;
		}
		
		virtual public ParamedicFeeTransPayment AttachEntity(ParamedicFeeTransPayment entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeTransPayment;
		}
		
		virtual public void Combine(ParamedicFeeTransPaymentCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeTransPayment this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeTransPayment;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeTransPayment);
		}
	}

	[Serializable]
	abstract public class esParamedicFeeTransPayment : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeTransPaymentQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeeTransPayment()
		{
		}
	
		public esParamedicFeeTransPayment(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 id)
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}
	
		private bool LoadByPrimaryKeyDynamic(Int32 id)
		{
			esParamedicFeeTransPaymentQuery query = this.GetDynamicQuery();
			query.Where(query.Id==id);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id",id);
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
						case "Id": this.str.Id = (string)value; break;
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;
						case "PaymentRefNo": this.str.PaymentRefNo = (string)value; break;
						case "PaymentRefDate": this.str.PaymentRefDate = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "AmountPercentage": this.str.AmountPercentage = (string)value; break;
						case "Amount": this.str.Amount = (string)value; break;
						case "DiscountAmount": this.str.DiscountAmount = (string)value; break;
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "GuarantorID": this.str.GuarantorID = (string)value; break;
						case "VerificationNo": this.str.VerificationNo = (string)value; break;
						case "IsProporsional": this.str.IsProporsional = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
							break;
						case "PaymentRefDate":
						
							if (value == null || value is System.DateTime)
								this.PaymentRefDate = (System.DateTime?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "AmountPercentage":
						
							if (value == null || value is System.Decimal)
								this.AmountPercentage = (System.Decimal?)value;
							break;
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						case "DiscountAmount":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
							break;
						case "CreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreateDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "VoidDateTime":
						
							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "IsProporsional":
						
							if (value == null || value is System.Boolean)
								this.IsProporsional = (System.Boolean?)value;
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
		/// Maps to ParamedicFeeTransPayment.Id
		/// </summary>
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeeTransPaymentMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeeTransPaymentMetadata.ColumnNames.Id, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.PaymentRefNo
		/// </summary>
		virtual public System.String PaymentRefNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentRefNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentRefNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.PaymentRefDate
		/// </summary>
		virtual public System.DateTime? PaymentRefDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentRefDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentRefDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransPaymentMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransPaymentMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.AmountPercentage
		/// </summary>
		virtual public System.Decimal? AmountPercentage
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransPaymentMetadata.ColumnNames.AmountPercentage);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransPaymentMetadata.ColumnNames.AmountPercentage, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransPaymentMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransPaymentMetadata.ColumnNames.Amount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTransPaymentMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTransPaymentMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentGroupNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransPaymentMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransPaymentMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransPaymentMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransPaymentMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTransPaymentMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTransPaymentMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.GuarantorID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTransPaymentMetadata.ColumnNames.VerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeeTransPayment.IsProporsional
		/// </summary>
		virtual public System.Boolean? IsProporsional
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeTransPaymentMetadata.ColumnNames.IsProporsional);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeTransPaymentMetadata.ColumnNames.IsProporsional, value);
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
			public esStrings(esParamedicFeeTransPayment entity)
			{
				this.entity = entity;
			}
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
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
			public System.String PaymentRefNo
			{
				get
				{
					System.String data = entity.PaymentRefNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentRefNo = null;
					else entity.PaymentRefNo = Convert.ToString(value);
				}
			}
			public System.String PaymentRefDate
			{
				get
				{
					System.DateTime? data = entity.PaymentRefDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentRefDate = null;
					else entity.PaymentRefDate = Convert.ToDateTime(value);
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
			public System.String AmountPercentage
			{
				get
				{
					System.Decimal? data = entity.AmountPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountPercentage = null;
					else entity.AmountPercentage = Convert.ToDecimal(value);
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
			public System.String CreateByUserID
			{
				get
				{
					System.String data = entity.CreateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateByUserID = null;
					else entity.CreateByUserID = Convert.ToString(value);
				}
			}
			public System.String CreateDateTime
			{
				get
				{
					System.DateTime? data = entity.CreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreateDateTime = null;
					else entity.CreateDateTime = Convert.ToDateTime(value);
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
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
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
			public System.String IsProporsional
			{
				get
				{
					System.Boolean? data = entity.IsProporsional;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProporsional = null;
					else entity.IsProporsional = Convert.ToBoolean(value);
				}
			}
			private esParamedicFeeTransPayment entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeTransPaymentQuery query)
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
				throw new Exception("esParamedicFeeTransPayment can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeeTransPayment : esParamedicFeeTransPayment
	{	
	}

	[Serializable]
	abstract public class esParamedicFeeTransPaymentQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransPaymentMetadata.Meta();
			}
		}	
			
		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.Id, esSystemType.Int32);
			}
		} 
			
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentRefNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentRefNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentRefDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentRefDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem AmountPercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.AmountPercentage, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
			
		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem IsProporsional
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTransPaymentMetadata.ColumnNames.IsProporsional, esSystemType.Boolean);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeTransPaymentCollection")]
	public partial class ParamedicFeeTransPaymentCollection : esParamedicFeeTransPaymentCollection, IEnumerable< ParamedicFeeTransPayment>
	{
		public ParamedicFeeTransPaymentCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeeTransPayment>(ParamedicFeeTransPaymentCollection coll)
		{
			List< ParamedicFeeTransPayment> list = new List< ParamedicFeeTransPayment>();
			
			foreach (ParamedicFeeTransPayment emp in coll)
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
				return  ParamedicFeeTransPaymentMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransPaymentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeTransPayment(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeTransPayment();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeeTransPaymentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransPaymentQuery();
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
		public bool Load(ParamedicFeeTransPaymentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeeTransPayment AddNew()
		{
			ParamedicFeeTransPayment entity = base.AddNewEntity() as ParamedicFeeTransPayment;
			
			return entity;		
		}
		public ParamedicFeeTransPayment FindByPrimaryKey(Int32 id)
		{
			return base.FindByPrimaryKey(id) as ParamedicFeeTransPayment;
		}

		#region IEnumerable< ParamedicFeeTransPayment> Members

		IEnumerator< ParamedicFeeTransPayment> IEnumerable< ParamedicFeeTransPayment>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeTransPayment;
			}
		}

		#endregion
		
		private ParamedicFeeTransPaymentQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeTransPayment' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeeTransPayment ({Id})")]
	[Serializable]
	public partial class ParamedicFeeTransPayment : esParamedicFeeTransPayment
	{
		public ParamedicFeeTransPayment()
		{
		}	
	
		public ParamedicFeeTransPayment(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTransPaymentMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeeTransPaymentQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTransPaymentQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeeTransPaymentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTransPaymentQuery();
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
		public bool Load(ParamedicFeeTransPaymentQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeeTransPaymentQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeeTransPaymentQuery : esParamedicFeeTransPaymentQuery
	{
		public ParamedicFeeTransPaymentQuery()
		{

		}		
		
		public ParamedicFeeTransPaymentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeeTransPaymentQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeeTransPaymentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeTransPaymentMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.SequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 7;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.TariffComponentID;
			c.CharacterMaxLength = 2;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentRefNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.PaymentRefNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentRefDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.PaymentRefDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.AmountPercentage, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.AmountPercentage;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.Amount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.DiscountAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.PaymentGroupNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.PaymentGroupNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.CreateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.CreateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.CreateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.VoidByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.VoidDateTime, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.GuarantorID, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.VerificationNo, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.VerificationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeeTransPaymentMetadata.ColumnNames.IsProporsional, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeTransPaymentMetadata.PropertyNames.IsProporsional;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeeTransPaymentMetadata Meta()
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
			public const string Id = "Id";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string PaymentRefNo = "PaymentRefNo";
			public const string PaymentRefDate = "PaymentRefDate";
			public const string IsVoid = "IsVoid";
			public const string AmountPercentage = "AmountPercentage";
			public const string Amount = "Amount";
			public const string DiscountAmount = "DiscountAmount";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string GuarantorID = "GuarantorID";
			public const string VerificationNo = "VerificationNo";
			public const string IsProporsional = "IsProporsional";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string Id = "Id";
			public const string TransactionNo = "TransactionNo";
			public const string SequenceNo = "SequenceNo";
			public const string TariffComponentID = "TariffComponentID";
			public const string PaymentRefNo = "PaymentRefNo";
			public const string PaymentRefDate = "PaymentRefDate";
			public const string IsVoid = "IsVoid";
			public const string AmountPercentage = "AmountPercentage";
			public const string Amount = "Amount";
			public const string DiscountAmount = "DiscountAmount";
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string GuarantorID = "GuarantorID";
			public const string VerificationNo = "VerificationNo";
			public const string IsProporsional = "IsProporsional";
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
			lock (typeof(ParamedicFeeTransPaymentMetadata))
			{
				if(ParamedicFeeTransPaymentMetadata.mapDelegates == null)
				{
					ParamedicFeeTransPaymentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeTransPaymentMetadata.meta == null)
				{
					ParamedicFeeTransPaymentMetadata.meta = new ParamedicFeeTransPaymentMetadata();
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
				
				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentRefNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentRefDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("AmountPercentage", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Amount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsProporsional", new esTypeMap("bit", "System.Boolean"));
		

				meta.Source = "ParamedicFeeTransPayment";
				meta.Destination = "ParamedicFeeTransPayment";
				meta.spInsert = "proc_ParamedicFeeTransPaymentInsert";				
				meta.spUpdate = "proc_ParamedicFeeTransPaymentUpdate";		
				meta.spDelete = "proc_ParamedicFeeTransPaymentDelete";
				meta.spLoadAll = "proc_ParamedicFeeTransPaymentLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeTransPaymentLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeTransPaymentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
