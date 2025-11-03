/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/31/2017 12:54:47 AM
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
	abstract public class esTransPaymentPatientItemCollection : esEntityCollectionWAuditLog
	{
		public esTransPaymentPatientItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransPaymentPatientItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPaymentPatientItemQuery query)
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
			this.InitQuery(query as esTransPaymentPatientItemQuery);
		}
		#endregion
		
		virtual public TransPaymentPatientItem DetachEntity(TransPaymentPatientItem entity)
		{
			return base.DetachEntity(entity) as TransPaymentPatientItem;
		}
		
		virtual public TransPaymentPatientItem AttachEntity(TransPaymentPatientItem entity)
		{
			return base.AttachEntity(entity) as TransPaymentPatientItem;
		}
		
		virtual public void Combine(TransPaymentPatientItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPaymentPatientItem this[int index]
		{
			get
			{
				return base[index] as TransPaymentPatientItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPaymentPatientItem);
		}
	}



	[Serializable]
	abstract public class esTransPaymentPatientItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPaymentPatientItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPaymentPatientItem()
		{

		}

		public esTransPaymentPatientItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String paymentNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String paymentNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(paymentNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(paymentNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String paymentNo, System.String sequenceNo)
		{
			esTransPaymentPatientItemQuery query = this.GetDynamicQuery();
			query.Where(query.PaymentNo == paymentNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String paymentNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("PaymentNo",paymentNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "PaymentNo": this.str.PaymentNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;							
						case "SRPaymentMethod": this.str.SRPaymentMethod = (string)value; break;							
						case "SRCardProvider": this.str.SRCardProvider = (string)value; break;							
						case "SRCardType": this.str.SRCardType = (string)value; break;							
						case "EDCMachineID": this.str.EDCMachineID = (string)value; break;							
						case "CardHolderName": this.str.CardHolderName = (string)value; break;							
						case "CardFeeAmount": this.str.CardFeeAmount = (string)value; break;							
						case "BankID": this.str.BankID = (string)value; break;							
						case "Amount": this.str.Amount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "CardNo": this.str.CardNo = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CardFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.CardFeeAmount = (System.Decimal?)value;
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to TransPaymentPatientItem.PaymentNo
		/// </summary>
		virtual public System.String PaymentNo
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.PaymentNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.PaymentNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SRPaymentType);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.SRPaymentMethod
		/// </summary>
		virtual public System.String SRPaymentMethod
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SRPaymentMethod);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SRPaymentMethod, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.SRCardProvider
		/// </summary>
		virtual public System.String SRCardProvider
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SRCardProvider);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SRCardProvider, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.SRCardType
		/// </summary>
		virtual public System.String SRCardType
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SRCardType);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.SRCardType, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.EDCMachineID
		/// </summary>
		virtual public System.String EDCMachineID
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.EDCMachineID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.EDCMachineID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.CardHolderName
		/// </summary>
		virtual public System.String CardHolderName
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.CardHolderName);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.CardHolderName, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.CardFeeAmount
		/// </summary>
		virtual public System.Decimal? CardFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentPatientItemMetadata.ColumnNames.CardFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPaymentPatientItemMetadata.ColumnNames.CardFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.BankID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(TransPaymentPatientItemMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(TransPaymentPatientItemMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPaymentPatientItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPaymentPatientItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.CardNo
		/// </summary>
		virtual public System.String CardNo
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.CardNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.CardNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPaymentPatientItem.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(TransPaymentPatientItemMetadata.ColumnNames.ReferenceSequenceNo);
			}
			
			set
			{
				base.SetSystemString(TransPaymentPatientItemMetadata.ColumnNames.ReferenceSequenceNo, value);
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
			public esStrings(esTransPaymentPatientItem entity)
			{
				this.entity = entity;
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
				
			public System.String SRPaymentType
			{
				get
				{
					System.String data = entity.SRPaymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentType = null;
					else entity.SRPaymentType = Convert.ToString(value);
				}
			}
				
			public System.String SRPaymentMethod
			{
				get
				{
					System.String data = entity.SRPaymentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPaymentMethod = null;
					else entity.SRPaymentMethod = Convert.ToString(value);
				}
			}
				
			public System.String SRCardProvider
			{
				get
				{
					System.String data = entity.SRCardProvider;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardProvider = null;
					else entity.SRCardProvider = Convert.ToString(value);
				}
			}
				
			public System.String SRCardType
			{
				get
				{
					System.String data = entity.SRCardType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCardType = null;
					else entity.SRCardType = Convert.ToString(value);
				}
			}
				
			public System.String EDCMachineID
			{
				get
				{
					System.String data = entity.EDCMachineID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EDCMachineID = null;
					else entity.EDCMachineID = Convert.ToString(value);
				}
			}
				
			public System.String CardHolderName
			{
				get
				{
					System.String data = entity.CardHolderName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardHolderName = null;
					else entity.CardHolderName = Convert.ToString(value);
				}
			}
				
			public System.String CardFeeAmount
			{
				get
				{
					System.Decimal? data = entity.CardFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardFeeAmount = null;
					else entity.CardFeeAmount = Convert.ToDecimal(value);
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
				
			public System.String CardNo
			{
				get
				{
					System.String data = entity.CardNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CardNo = null;
					else entity.CardNo = Convert.ToString(value);
				}
			}
				
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
				
			public System.String ReferenceSequenceNo
			{
				get
				{
					System.String data = entity.ReferenceSequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceSequenceNo = null;
					else entity.ReferenceSequenceNo = Convert.ToString(value);
				}
			}
			

			private esTransPaymentPatientItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPaymentPatientItemQuery query)
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
				throw new Exception("esTransPaymentPatientItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class TransPaymentPatientItem : esTransPaymentPatientItem
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
	abstract public class esTransPaymentPatientItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentPatientItemMetadata.Meta();
			}
		}	
		

		public esQueryItem PaymentNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.PaymentNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPaymentMethod
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.SRPaymentMethod, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCardProvider
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.SRCardProvider, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCardType
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.SRCardType, esSystemType.String);
			}
		} 
		
		public esQueryItem EDCMachineID
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.EDCMachineID, esSystemType.String);
			}
		} 
		
		public esQueryItem CardHolderName
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.CardHolderName, esSystemType.String);
			}
		} 
		
		public esQueryItem CardFeeAmount
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.CardFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CardNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.CardNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, TransPaymentPatientItemMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPaymentPatientItemCollection")]
	public partial class TransPaymentPatientItemCollection : esTransPaymentPatientItemCollection, IEnumerable<TransPaymentPatientItem>
	{
		public TransPaymentPatientItemCollection()
		{

		}
		
		public static implicit operator List<TransPaymentPatientItem>(TransPaymentPatientItemCollection coll)
		{
			List<TransPaymentPatientItem> list = new List<TransPaymentPatientItem>();
			
			foreach (TransPaymentPatientItem emp in coll)
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
				return  TransPaymentPatientItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentPatientItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPaymentPatientItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPaymentPatientItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransPaymentPatientItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentPatientItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransPaymentPatientItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransPaymentPatientItem AddNew()
		{
			TransPaymentPatientItem entity = base.AddNewEntity() as TransPaymentPatientItem;
			
			return entity;
		}

		public TransPaymentPatientItem FindByPrimaryKey(System.String paymentNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(paymentNo, sequenceNo) as TransPaymentPatientItem;
		}


		#region IEnumerable<TransPaymentPatientItem> Members

		IEnumerator<TransPaymentPatientItem> IEnumerable<TransPaymentPatientItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPaymentPatientItem;
			}
		}

		#endregion
		
		private TransPaymentPatientItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPaymentPatientItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPaymentPatientItem ({PaymentNo},{SequenceNo})")]
	[Serializable]
	public partial class TransPaymentPatientItem : esTransPaymentPatientItem
	{
		public TransPaymentPatientItem()
		{

		}
	
		public TransPaymentPatientItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPaymentPatientItemMetadata.Meta();
			}
		}
		
		
		
		override protected esTransPaymentPatientItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPaymentPatientItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransPaymentPatientItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPaymentPatientItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransPaymentPatientItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransPaymentPatientItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransPaymentPatientItemQuery : esTransPaymentPatientItemQuery
	{
		public TransPaymentPatientItemQuery()
		{

		}		
		
		public TransPaymentPatientItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransPaymentPatientItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransPaymentPatientItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPaymentPatientItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.PaymentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.PaymentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.SRPaymentType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.SRPaymentMethod, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.SRPaymentMethod;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.SRCardProvider, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.SRCardProvider;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.SRCardType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.SRCardType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.EDCMachineID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.EDCMachineID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.CardHolderName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.CardHolderName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.CardFeeAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.CardFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.BankID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.Amount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.CardNo, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.CardNo;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.ReferenceNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPaymentPatientItemMetadata.ColumnNames.ReferenceSequenceNo, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPaymentPatientItemMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			c.HasDefault = true;
			c.Default = @"('000')";
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransPaymentPatientItemMetadata Meta()
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
			 public const string PaymentNo = "PaymentNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string SRPaymentType = "SRPaymentType";
			 public const string SRPaymentMethod = "SRPaymentMethod";
			 public const string SRCardProvider = "SRCardProvider";
			 public const string SRCardType = "SRCardType";
			 public const string EDCMachineID = "EDCMachineID";
			 public const string CardHolderName = "CardHolderName";
			 public const string CardFeeAmount = "CardFeeAmount";
			 public const string BankID = "BankID";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string CardNo = "CardNo";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string ReferenceSequenceNo = "ReferenceSequenceNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PaymentNo = "PaymentNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string SRPaymentType = "SRPaymentType";
			 public const string SRPaymentMethod = "SRPaymentMethod";
			 public const string SRCardProvider = "SRCardProvider";
			 public const string SRCardType = "SRCardType";
			 public const string EDCMachineID = "EDCMachineID";
			 public const string CardHolderName = "CardHolderName";
			 public const string CardFeeAmount = "CardFeeAmount";
			 public const string BankID = "BankID";
			 public const string Amount = "Amount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string CardNo = "CardNo";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string ReferenceSequenceNo = "ReferenceSequenceNo";
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
			lock (typeof(TransPaymentPatientItemMetadata))
			{
				if(TransPaymentPatientItemMetadata.mapDelegates == null)
				{
					TransPaymentPatientItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPaymentPatientItemMetadata.meta == null)
				{
					TransPaymentPatientItemMetadata.meta = new TransPaymentPatientItemMetadata();
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
				

				meta.AddTypeMap("PaymentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardProvider", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EDCMachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CardHolderName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CardFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CardNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TransPaymentPatientItem";
				meta.Destination = "TransPaymentPatientItem";
				
				meta.spInsert = "proc_TransPaymentPatientItemInsert";				
				meta.spUpdate = "proc_TransPaymentPatientItemUpdate";		
				meta.spDelete = "proc_TransPaymentPatientItemDelete";
				meta.spLoadAll = "proc_TransPaymentPatientItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPaymentPatientItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPaymentPatientItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
