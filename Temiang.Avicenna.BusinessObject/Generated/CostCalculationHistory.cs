/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/27/2012 12:50:43 PM
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
	abstract public class esCostCalculationHistoryCollection : esEntityCollectionWAuditLog
	{
		public esCostCalculationHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CostCalculationHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esCostCalculationHistoryQuery query)
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
			this.InitQuery(query as esCostCalculationHistoryQuery);
		}
		#endregion
		
		virtual public CostCalculationHistory DetachEntity(CostCalculationHistory entity)
		{
			return base.DetachEntity(entity) as CostCalculationHistory;
		}
		
		virtual public CostCalculationHistory AttachEntity(CostCalculationHistory entity)
		{
			return base.AttachEntity(entity) as CostCalculationHistory;
		}
		
		virtual public void Combine(CostCalculationHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CostCalculationHistory this[int index]
		{
			get
			{
				return base[index] as CostCalculationHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CostCalculationHistory);
		}
	}



	[Serializable]
	abstract public class esCostCalculationHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCostCalculationHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esCostCalculationHistory()
		{

		}

		public esCostCalculationHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String recalculationProcessNo, System.String registrationNo, System.String transactionNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, registrationNo, transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, registrationNo, transactionNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String recalculationProcessNo, System.String registrationNo, System.String transactionNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recalculationProcessNo, registrationNo, transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(recalculationProcessNo, registrationNo, transactionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String recalculationProcessNo, System.String registrationNo, System.String transactionNo, System.String sequenceNo)
		{
			esCostCalculationHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RecalculationProcessNo == recalculationProcessNo, query.RegistrationNo == registrationNo, query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String recalculationProcessNo, System.String registrationNo, System.String transactionNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RecalculationProcessNo",recalculationProcessNo);			parms.Add("RegistrationNo",registrationNo);			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "RecalculationProcessNo": this.str.RecalculationProcessNo = (string)value; break;							
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
		/// Maps to CostCalculationHistory.RecalculationProcessNo
		/// </summary>
		virtual public System.String RecalculationProcessNo
		{
			get
			{
				return base.GetSystemString(CostCalculationHistoryMetadata.ColumnNames.RecalculationProcessNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationHistoryMetadata.ColumnNames.RecalculationProcessNo, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(CostCalculationHistoryMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(CostCalculationHistoryMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationHistoryMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(CostCalculationHistoryMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationHistoryMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(CostCalculationHistoryMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(CostCalculationHistoryMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.PatientAmount
		/// </summary>
		virtual public System.Decimal? PatientAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.PatientAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.PatientAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.GuarantorAmount
		/// </summary>
		virtual public System.Decimal? GuarantorAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.GuarantorAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.GuarantorAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.DiscountAmount
		/// </summary>
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.DiscountAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.ParamedicAmount
		/// </summary>
		virtual public System.Decimal? ParamedicAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.ParamedicAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.ParamedicAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CostCalculationHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CostCalculationHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CostCalculationHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CostCalculationHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.ParamedicFeeAmount
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.ParamedicFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(CostCalculationHistoryMetadata.ColumnNames.ParamedicFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.ParamedicFeePaymentNo
		/// </summary>
		virtual public System.String ParamedicFeePaymentNo
		{
			get
			{
				return base.GetSystemString(CostCalculationHistoryMetadata.ColumnNames.ParamedicFeePaymentNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationHistoryMetadata.ColumnNames.ParamedicFeePaymentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(CostCalculationHistoryMetadata.ColumnNames.IsPackage);
			}
			
			set
			{
				base.SetSystemBoolean(CostCalculationHistoryMetadata.ColumnNames.IsPackage, value);
			}
		}
		
		/// <summary>
		/// Maps to CostCalculationHistory.ParentNo
		/// </summary>
		virtual public System.String ParentNo
		{
			get
			{
				return base.GetSystemString(CostCalculationHistoryMetadata.ColumnNames.ParentNo);
			}
			
			set
			{
				base.SetSystemString(CostCalculationHistoryMetadata.ColumnNames.ParentNo, value);
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
			public esStrings(esCostCalculationHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RecalculationProcessNo
			{
				get
				{
					System.String data = entity.RecalculationProcessNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecalculationProcessNo = null;
					else entity.RecalculationProcessNo = Convert.ToString(value);
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
			

			private esCostCalculationHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCostCalculationHistoryQuery query)
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
				throw new Exception("esCostCalculationHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class CostCalculationHistory : esCostCalculationHistory
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
	abstract public class esCostCalculationHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CostCalculationHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RecalculationProcessNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.RecalculationProcessNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.PatientAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem GuarantorAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.GuarantorAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DiscountAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.ParamedicAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicFeeAmount
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicFeePaymentNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.ParamedicFeePaymentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ParentNo
		{
			get
			{
				return new esQueryItem(this, CostCalculationHistoryMetadata.ColumnNames.ParentNo, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CostCalculationHistoryCollection")]
	public partial class CostCalculationHistoryCollection : esCostCalculationHistoryCollection, IEnumerable<CostCalculationHistory>
	{
		public CostCalculationHistoryCollection()
		{

		}
		
		public static implicit operator List<CostCalculationHistory>(CostCalculationHistoryCollection coll)
		{
			List<CostCalculationHistory> list = new List<CostCalculationHistory>();
			
			foreach (CostCalculationHistory emp in coll)
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
				return  CostCalculationHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CostCalculationHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CostCalculationHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CostCalculationHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CostCalculationHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CostCalculationHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CostCalculationHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CostCalculationHistory AddNew()
		{
			CostCalculationHistory entity = base.AddNewEntity() as CostCalculationHistory;
			
			return entity;
		}

		public CostCalculationHistory FindByPrimaryKey(System.String recalculationProcessNo, System.String registrationNo, System.String transactionNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(recalculationProcessNo, registrationNo, transactionNo, sequenceNo) as CostCalculationHistory;
		}


		#region IEnumerable<CostCalculationHistory> Members

		IEnumerator<CostCalculationHistory> IEnumerable<CostCalculationHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CostCalculationHistory;
			}
		}

		#endregion
		
		private CostCalculationHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CostCalculationHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CostCalculationHistory ({RecalculationProcessNo},{RegistrationNo},{TransactionNo},{SequenceNo})")]
	[Serializable]
	public partial class CostCalculationHistory : esCostCalculationHistory
	{
		public CostCalculationHistory()
		{

		}
	
		public CostCalculationHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CostCalculationHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esCostCalculationHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CostCalculationHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CostCalculationHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CostCalculationHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CostCalculationHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CostCalculationHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CostCalculationHistoryQuery : esCostCalculationHistoryQuery
	{
		public CostCalculationHistoryQuery()
		{

		}		
		
		public CostCalculationHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CostCalculationHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class CostCalculationHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CostCalculationHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.RecalculationProcessNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.RecalculationProcessNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.TransactionNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.SequenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 6;
			c.HasDefault = true;
			c.Default = @"('000')";
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.ItemID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.PatientAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.PatientAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.GuarantorAmount, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.GuarantorAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.DiscountAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.ParamedicAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.ParamedicAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.ParamedicFeeAmount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.ParamedicFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.ParamedicFeePaymentNo, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.ParamedicFeePaymentNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.IsPackage, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.IsPackage;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(CostCalculationHistoryMetadata.ColumnNames.ParentNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CostCalculationHistoryMetadata.PropertyNames.ParentNo;
			c.CharacterMaxLength = 7;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CostCalculationHistoryMetadata Meta()
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
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
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
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RecalculationProcessNo = "RecalculationProcessNo";
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
			lock (typeof(CostCalculationHistoryMetadata))
			{
				if(CostCalculationHistoryMetadata.mapDelegates == null)
				{
					CostCalculationHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CostCalculationHistoryMetadata.meta == null)
				{
					CostCalculationHistoryMetadata.meta = new CostCalculationHistoryMetadata();
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
				

				meta.AddTypeMap("RecalculationProcessNo", new esTypeMap("varchar", "System.String"));
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
				
				
				
				meta.Source = "CostCalculationHistory";
				meta.Destination = "CostCalculationHistory";
				
				meta.spInsert = "proc_CostCalculationHistoryInsert";				
				meta.spUpdate = "proc_CostCalculationHistoryUpdate";		
				meta.spDelete = "proc_CostCalculationHistoryDelete";
				meta.spLoadAll = "proc_CostCalculationHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_CostCalculationHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CostCalculationHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
