/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/21/2022 3:51:02 PM
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
	abstract public class esParamedicFeePaymentGroupCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeePaymentGroupCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "ParamedicFeePaymentGroupCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esParamedicFeePaymentGroupQuery query)
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
			this.InitQuery(query as esParamedicFeePaymentGroupQuery);
		}
		#endregion
			
		virtual public ParamedicFeePaymentGroup DetachEntity(ParamedicFeePaymentGroup entity)
		{
			return base.DetachEntity(entity) as ParamedicFeePaymentGroup;
		}
		
		virtual public ParamedicFeePaymentGroup AttachEntity(ParamedicFeePaymentGroup entity)
		{
			return base.AttachEntity(entity) as ParamedicFeePaymentGroup;
		}
		
		virtual public void Combine(ParamedicFeePaymentGroupCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeePaymentGroup this[int index]
		{
			get
			{
				return base[index] as ParamedicFeePaymentGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeePaymentGroup);
		}
	}

	[Serializable]
	abstract public class esParamedicFeePaymentGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeePaymentGroupQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esParamedicFeePaymentGroup()
		{
		}
	
		public esParamedicFeePaymentGroup(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String paymentGroupNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentGroupNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentGroupNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String paymentGroupNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentGroupNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentGroupNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String paymentGroupNo)
		{
			esParamedicFeePaymentGroupQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentGroupNo==paymentGroupNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String paymentGroupNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentGroupNo",paymentGroupNo);
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
						case "PaymentGroupNo": this.str.PaymentGroupNo = (string)value; break;
						case "PaymentDate": this.str.PaymentDate = (string)value; break;
						case "PaymentMethodID": this.str.PaymentMethodID = (string)value; break;
						case "BankID": this.str.BankID = (string)value; break;
						case "PaymentAmount": this.str.PaymentAmount = (string)value; break;
						case "IsApprove": this.str.IsApprove = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "IsDetail": this.str.IsDetail = (string)value; break;
						case "FeeAmountBeforeTax": this.str.FeeAmountBeforeTax = (string)value; break;
						case "TaxOnPaymentAmount": this.str.TaxOnPaymentAmount = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "IsDraft": this.str.IsDraft = (string)value; break;
						case "ApproveDateTime": this.str.ApproveDateTime = (string)value; break;
						case "GuaranteeFeeDateFrom": this.str.GuaranteeFeeDateFrom = (string)value; break;
						case "GuaranteeFeeDateTo": this.str.GuaranteeFeeDateTo = (string)value; break;
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
						case "PaymentAmount":
						
							if (value == null || value is System.Decimal)
								this.PaymentAmount = (System.Decimal?)value;
							break;
						case "IsApprove":
						
							if (value == null || value is System.Boolean)
								this.IsApprove = (System.Boolean?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsDetail":
						
							if (value == null || value is System.Int32)
								this.IsDetail = (System.Int32?)value;
							break;
						case "FeeAmountBeforeTax":
						
							if (value == null || value is System.Decimal)
								this.FeeAmountBeforeTax = (System.Decimal?)value;
							break;
						case "TaxOnPaymentAmount":
						
							if (value == null || value is System.Decimal)
								this.TaxOnPaymentAmount = (System.Decimal?)value;
							break;
						case "VoidDateTime":
						
							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "IsDraft":
						
							if (value == null || value is System.Boolean)
								this.IsDraft = (System.Boolean?)value;
							break;
						case "ApproveDateTime":
						
							if (value == null || value is System.DateTime)
								this.ApproveDateTime = (System.DateTime?)value;
							break;
						case "GuaranteeFeeDateFrom":
						
							if (value == null || value is System.DateTime)
								this.GuaranteeFeeDateFrom = (System.DateTime?)value;
							break;
						case "GuaranteeFeeDateTo":
						
							if (value == null || value is System.DateTime)
								this.GuaranteeFeeDateTo = (System.DateTime?)value;
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
		/// Maps to ParamedicFeePaymentGroup.PaymentGroupNo
		/// </summary>
		virtual public System.String PaymentGroupNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentGroupNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentGroupNo, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.PaymentDate
		/// </summary>
		virtual public System.DateTime? PaymentDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentDate);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.PaymentMethodID
		/// </summary>
		virtual public System.String PaymentMethodID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentMethodID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentMethodID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.BankID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.PaymentAmount
		/// </summary>
		virtual public System.Decimal? PaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.IsApprove
		/// </summary>
		virtual public System.Boolean? IsApprove
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeePaymentGroupMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeePaymentGroupMetadata.ColumnNames.IsApprove, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeePaymentGroupMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeePaymentGroupMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.IsDetail
		/// </summary>
		virtual public System.Int32? IsDetail
		{
			get
			{
				return base.GetSystemInt32(ParamedicFeePaymentGroupMetadata.ColumnNames.IsDetail);
			}
			
			set
			{
				base.SetSystemInt32(ParamedicFeePaymentGroupMetadata.ColumnNames.IsDetail, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.FeeAmountBeforeTax
		/// </summary>
		virtual public System.Decimal? FeeAmountBeforeTax
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeePaymentGroupMetadata.ColumnNames.FeeAmountBeforeTax);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeePaymentGroupMetadata.ColumnNames.FeeAmountBeforeTax, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.TaxOnPaymentAmount
		/// </summary>
		virtual public System.Decimal? TaxOnPaymentAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeePaymentGroupMetadata.ColumnNames.TaxOnPaymentAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeePaymentGroupMetadata.ColumnNames.TaxOnPaymentAmount, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeePaymentGroupMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.IsDraft
		/// </summary>
		virtual public System.Boolean? IsDraft
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeePaymentGroupMetadata.ColumnNames.IsDraft);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeePaymentGroupMetadata.ColumnNames.IsDraft, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.ApproveDateTime
		/// </summary>
		virtual public System.DateTime? ApproveDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.ApproveDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.ApproveDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.GuaranteeFeeDateFrom
		/// </summary>
		virtual public System.DateTime? GuaranteeFeeDateFrom
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.GuaranteeFeeDateFrom);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.GuaranteeFeeDateFrom, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicFeePaymentGroup.GuaranteeFeeDateTo
		/// </summary>
		virtual public System.DateTime? GuaranteeFeeDateTo
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.GuaranteeFeeDateTo);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeePaymentGroupMetadata.ColumnNames.GuaranteeFeeDateTo, value);
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
			public esStrings(esParamedicFeePaymentGroup entity)
			{
				this.entity = entity;
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
			public System.String PaymentMethodID
			{
				get
				{
					System.String data = entity.PaymentMethodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentMethodID = null;
					else entity.PaymentMethodID = Convert.ToString(value);
				}
			}
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
			public System.String PaymentAmount
			{
				get
				{
					System.Decimal? data = entity.PaymentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentAmount = null;
					else entity.PaymentAmount = Convert.ToDecimal(value);
				}
			}
			public System.String IsApprove
			{
				get
				{
					System.Boolean? data = entity.IsApprove;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApprove = null;
					else entity.IsApprove = Convert.ToBoolean(value);
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
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
			public System.String IsDetail
			{
				get
				{
					System.Int32? data = entity.IsDetail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDetail = null;
					else entity.IsDetail = Convert.ToInt32(value);
				}
			}
			public System.String FeeAmountBeforeTax
			{
				get
				{
					System.Decimal? data = entity.FeeAmountBeforeTax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FeeAmountBeforeTax = null;
					else entity.FeeAmountBeforeTax = Convert.ToDecimal(value);
				}
			}
			public System.String TaxOnPaymentAmount
			{
				get
				{
					System.Decimal? data = entity.TaxOnPaymentAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxOnPaymentAmount = null;
					else entity.TaxOnPaymentAmount = Convert.ToDecimal(value);
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
			public System.String IsDraft
			{
				get
				{
					System.Boolean? data = entity.IsDraft;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDraft = null;
					else entity.IsDraft = Convert.ToBoolean(value);
				}
			}
			public System.String ApproveDateTime
			{
				get
				{
					System.DateTime? data = entity.ApproveDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApproveDateTime = null;
					else entity.ApproveDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String GuaranteeFeeDateFrom
			{
				get
				{
					System.DateTime? data = entity.GuaranteeFeeDateFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuaranteeFeeDateFrom = null;
					else entity.GuaranteeFeeDateFrom = Convert.ToDateTime(value);
				}
			}
			public System.String GuaranteeFeeDateTo
			{
				get
				{
					System.DateTime? data = entity.GuaranteeFeeDateTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuaranteeFeeDateTo = null;
					else entity.GuaranteeFeeDateTo = Convert.ToDateTime(value);
				}
			}
			private esParamedicFeePaymentGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeePaymentGroupQuery query)
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
				throw new Exception("esParamedicFeePaymentGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicFeePaymentGroup : esParamedicFeePaymentGroup
	{	
	}

	[Serializable]
	abstract public class esParamedicFeePaymentGroupQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeePaymentGroupMetadata.Meta();
			}
		}	
			
		public esQueryItem PaymentGroupNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentGroupNo, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentDate
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentDate, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem PaymentMethodID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentMethodID, esSystemType.String);
			}
		} 
			
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
			
		public esQueryItem PaymentAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem IsApprove
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.IsApprove, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsDetail
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.IsDetail, esSystemType.Int32);
			}
		} 
			
		public esQueryItem FeeAmountBeforeTax
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.FeeAmountBeforeTax, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem TaxOnPaymentAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.TaxOnPaymentAmount, esSystemType.Decimal);
			}
		} 
			
		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem IsDraft
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.IsDraft, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem ApproveDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.ApproveDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem GuaranteeFeeDateFrom
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.GuaranteeFeeDateFrom, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem GuaranteeFeeDateTo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeePaymentGroupMetadata.ColumnNames.GuaranteeFeeDateTo, esSystemType.DateTime);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeePaymentGroupCollection")]
	public partial class ParamedicFeePaymentGroupCollection : esParamedicFeePaymentGroupCollection, IEnumerable< ParamedicFeePaymentGroup>
	{
		public ParamedicFeePaymentGroupCollection()
		{

		}	
		
		public static implicit operator List< ParamedicFeePaymentGroup>(ParamedicFeePaymentGroupCollection coll)
		{
			List< ParamedicFeePaymentGroup> list = new List< ParamedicFeePaymentGroup>();
			
			foreach (ParamedicFeePaymentGroup emp in coll)
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
				return  ParamedicFeePaymentGroupMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeePaymentGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeePaymentGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeePaymentGroup();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public ParamedicFeePaymentGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeePaymentGroupQuery();
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
		public bool Load(ParamedicFeePaymentGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicFeePaymentGroup AddNew()
		{
			ParamedicFeePaymentGroup entity = base.AddNewEntity() as ParamedicFeePaymentGroup;
			
			return entity;		
		}
		public ParamedicFeePaymentGroup FindByPrimaryKey(String paymentGroupNo)
		{
			return base.FindByPrimaryKey(paymentGroupNo) as ParamedicFeePaymentGroup;
		}

		#region IEnumerable< ParamedicFeePaymentGroup> Members

		IEnumerator< ParamedicFeePaymentGroup> IEnumerable< ParamedicFeePaymentGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeePaymentGroup;
			}
		}

		#endregion
		
		private ParamedicFeePaymentGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeePaymentGroup' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicFeePaymentGroup ({PaymentGroupNo})")]
	[Serializable]
	public partial class ParamedicFeePaymentGroup : esParamedicFeePaymentGroup
	{
		public ParamedicFeePaymentGroup()
		{
		}	
	
		public ParamedicFeePaymentGroup(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeePaymentGroupMetadata.Meta();
			}
		}	
	
		override protected esParamedicFeePaymentGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeePaymentGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public ParamedicFeePaymentGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeePaymentGroupQuery();
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
		public bool Load(ParamedicFeePaymentGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private ParamedicFeePaymentGroupQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicFeePaymentGroupQuery : esParamedicFeePaymentGroupQuery
	{
		public ParamedicFeePaymentGroupQuery()
		{

		}		
		
		public ParamedicFeePaymentGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "ParamedicFeePaymentGroupQuery";
        }
	}

	[Serializable]
	public partial class ParamedicFeePaymentGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeePaymentGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentGroupNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.PaymentGroupNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.PaymentDate;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentMethodID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.PaymentMethodID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.BankID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.PaymentAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.PaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.IsApprove, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.IsApprove;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.CreatedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.CreatedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.IsDetail, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.IsDetail;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.FeeAmountBeforeTax, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.FeeAmountBeforeTax;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.TaxOnPaymentAmount, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.TaxOnPaymentAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
 			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.VoidByUserID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.VoidDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.IsDraft, 16, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.IsDraft;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.ApproveDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.ApproveDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.GuaranteeFeeDateFrom, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.GuaranteeFeeDateFrom;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(ParamedicFeePaymentGroupMetadata.ColumnNames.GuaranteeFeeDateTo, 19, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeePaymentGroupMetadata.PropertyNames.GuaranteeFeeDateTo;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public ParamedicFeePaymentGroupMetadata Meta()
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
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string PaymentDate = "PaymentDate";
			public const string PaymentMethodID = "PaymentMethodID";
			public const string BankID = "BankID";
			public const string PaymentAmount = "PaymentAmount";
			public const string IsApprove = "IsApprove";
			public const string IsVoid = "IsVoid";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsDetail = "IsDetail";
			public const string FeeAmountBeforeTax = "FeeAmountBeforeTax";
			public const string TaxOnPaymentAmount = "TaxOnPaymentAmount";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string IsDraft = "IsDraft";
			public const string ApproveDateTime = "ApproveDateTime";
			public const string GuaranteeFeeDateFrom = "GuaranteeFeeDateFrom";
			public const string GuaranteeFeeDateTo = "GuaranteeFeeDateTo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string PaymentGroupNo = "PaymentGroupNo";
			public const string PaymentDate = "PaymentDate";
			public const string PaymentMethodID = "PaymentMethodID";
			public const string BankID = "BankID";
			public const string PaymentAmount = "PaymentAmount";
			public const string IsApprove = "IsApprove";
			public const string IsVoid = "IsVoid";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string IsDetail = "IsDetail";
			public const string FeeAmountBeforeTax = "FeeAmountBeforeTax";
			public const string TaxOnPaymentAmount = "TaxOnPaymentAmount";
			public const string VoidByUserID = "VoidByUserID";
			public const string VoidDateTime = "VoidDateTime";
			public const string IsDraft = "IsDraft";
			public const string ApproveDateTime = "ApproveDateTime";
			public const string GuaranteeFeeDateFrom = "GuaranteeFeeDateFrom";
			public const string GuaranteeFeeDateTo = "GuaranteeFeeDateTo";
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
			lock (typeof(ParamedicFeePaymentGroupMetadata))
			{
				if(ParamedicFeePaymentGroupMetadata.mapDelegates == null)
				{
					ParamedicFeePaymentGroupMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeePaymentGroupMetadata.meta == null)
				{
					ParamedicFeePaymentGroupMetadata.meta = new ParamedicFeePaymentGroupMetadata();
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
				
				meta.AddTypeMap("PaymentGroupNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentDate", new esTypeMap("date", "System.DateTime"));
				meta.AddTypeMap("PaymentMethodID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PaymentAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsApprove", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsDetail", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FeeAmountBeforeTax", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("TaxOnPaymentAmount", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsDraft", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ApproveDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("GuaranteeFeeDateFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("GuaranteeFeeDateTo", new esTypeMap("datetime", "System.DateTime"));
		

				meta.Source = "ParamedicFeePaymentGroup";
				meta.Destination = "ParamedicFeePaymentGroup";
				meta.spInsert = "proc_ParamedicFeePaymentGroupInsert";				
				meta.spUpdate = "proc_ParamedicFeePaymentGroupUpdate";		
				meta.spDelete = "proc_ParamedicFeePaymentGroupDelete";
				meta.spLoadAll = "proc_ParamedicFeePaymentGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeePaymentGroupLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeePaymentGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
