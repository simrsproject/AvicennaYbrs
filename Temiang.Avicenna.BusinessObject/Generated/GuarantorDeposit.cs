/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/17/2015 10:27:44 AM
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
	abstract public class esGuarantorDepositCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorDepositCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorDepositCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorDepositQuery query)
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
			this.InitQuery(query as esGuarantorDepositQuery);
		}
		#endregion
		
		virtual public GuarantorDeposit DetachEntity(GuarantorDeposit entity)
		{
			return base.DetachEntity(entity) as GuarantorDeposit;
		}
		
		virtual public GuarantorDeposit AttachEntity(GuarantorDeposit entity)
		{
			return base.AttachEntity(entity) as GuarantorDeposit;
		}
		
		virtual public void Combine(GuarantorDepositCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorDeposit this[int index]
		{
			get
			{
				return base[index] as GuarantorDeposit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorDeposit);
		}
	}



	[Serializable]
	abstract public class esGuarantorDeposit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorDepositQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorDeposit()
		{

		}

		public esGuarantorDeposit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo)
		{
			esGuarantorDepositQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo)
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "SRPaymentType": this.str.SRPaymentType = (string)value; break;							
						case "SRPaymentMethod": this.str.SRPaymentMethod = (string)value; break;							
						case "SRCardProvider": this.str.SRCardProvider = (string)value; break;							
						case "SRCardType": this.str.SRCardType = (string)value; break;							
						case "EDCMachineID": this.str.EDCMachineID = (string)value; break;							
						case "CardHolderName": this.str.CardHolderName = (string)value; break;							
						case "BankID": this.str.BankID = (string)value; break;							
						case "BankAccountNo": this.str.BankAccountNo = (string)value; break;							
						case "Amount": this.str.Amount = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to GuarantorDeposit.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(GuarantorDepositMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorDepositMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.SRPaymentType
		/// </summary>
		virtual public System.String SRPaymentType
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.SRPaymentType);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.SRPaymentType, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.SRPaymentMethod
		/// </summary>
		virtual public System.String SRPaymentMethod
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.SRPaymentMethod);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.SRPaymentMethod, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.SRCardProvider
		/// </summary>
		virtual public System.String SRCardProvider
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.SRCardProvider);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.SRCardProvider, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.SRCardType
		/// </summary>
		virtual public System.String SRCardType
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.SRCardType);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.SRCardType, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.EDCMachineID
		/// </summary>
		virtual public System.String EDCMachineID
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.EDCMachineID);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.EDCMachineID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.CardHolderName
		/// </summary>
		virtual public System.String CardHolderName
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.CardHolderName);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.CardHolderName, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.BankID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.BankAccountNo
		/// </summary>
		virtual public System.String BankAccountNo
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.BankAccountNo);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.BankAccountNo, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.Amount
		/// </summary>
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(GuarantorDepositMetadata.ColumnNames.Amount);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorDepositMetadata.ColumnNames.Amount, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(GuarantorDepositMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorDepositMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(GuarantorDepositMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(GuarantorDepositMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorDepositMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorDepositMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDeposit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorDepositMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorDeposit entity)
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
				
			public System.String BankAccountNo
			{
				get
				{
					System.String data = entity.BankAccountNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankAccountNo = null;
					else entity.BankAccountNo = Convert.ToString(value);
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
			

			private esGuarantorDeposit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorDepositQuery query)
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
				throw new Exception("esGuarantorDeposit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorDeposit : esGuarantorDeposit
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
	abstract public class esGuarantorDepositQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorDepositMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPaymentType
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.SRPaymentType, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPaymentMethod
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.SRPaymentMethod, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCardProvider
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.SRCardProvider, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCardType
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.SRCardType, esSystemType.String);
			}
		} 
		
		public esQueryItem EDCMachineID
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.EDCMachineID, esSystemType.String);
			}
		} 
		
		public esQueryItem CardHolderName
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.CardHolderName, esSystemType.String);
			}
		} 
		
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
		
		public esQueryItem BankAccountNo
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.BankAccountNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Amount
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.Amount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorDepositCollection")]
	public partial class GuarantorDepositCollection : esGuarantorDepositCollection, IEnumerable<GuarantorDeposit>
	{
		public GuarantorDepositCollection()
		{

		}
		
		public static implicit operator List<GuarantorDeposit>(GuarantorDepositCollection coll)
		{
			List<GuarantorDeposit> list = new List<GuarantorDeposit>();
			
			foreach (GuarantorDeposit emp in coll)
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
				return  GuarantorDepositMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorDepositQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorDeposit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorDeposit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorDepositQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorDepositQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorDepositQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorDeposit AddNew()
		{
			GuarantorDeposit entity = base.AddNewEntity() as GuarantorDeposit;
			
			return entity;
		}

		public GuarantorDeposit FindByPrimaryKey(System.String transactionNo)
		{
			return base.FindByPrimaryKey(transactionNo) as GuarantorDeposit;
		}


		#region IEnumerable<GuarantorDeposit> Members

		IEnumerator<GuarantorDeposit> IEnumerable<GuarantorDeposit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorDeposit;
			}
		}

		#endregion
		
		private GuarantorDepositQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorDeposit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorDeposit ({TransactionNo})")]
	[Serializable]
	public partial class GuarantorDeposit : esGuarantorDeposit
	{
		public GuarantorDeposit()
		{

		}
	
		public GuarantorDeposit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorDepositMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorDepositQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorDepositQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorDepositQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorDepositQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorDepositQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorDepositQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorDepositQuery : esGuarantorDepositQuery
	{
		public GuarantorDepositQuery()
		{

		}		
		
		public GuarantorDepositQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorDepositQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorDepositMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorDepositMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.GuarantorID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.SRPaymentType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.SRPaymentType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.SRPaymentMethod, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.SRPaymentMethod;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.SRCardProvider, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.SRCardProvider;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.SRCardType, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.SRCardType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.EDCMachineID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.EDCMachineID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.CardHolderName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.CardHolderName;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.BankID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.BankAccountNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.BankAccountNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.Amount, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.Amount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.Notes, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.IsApproved, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.IsVoid, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.LastUpdateDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositMetadata.ColumnNames.LastUpdateByUserID, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorDepositMetadata Meta()
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
			 public const string GuarantorID = "GuarantorID";
			 public const string SRPaymentType = "SRPaymentType";
			 public const string SRPaymentMethod = "SRPaymentMethod";
			 public const string SRCardProvider = "SRCardProvider";
			 public const string SRCardType = "SRCardType";
			 public const string EDCMachineID = "EDCMachineID";
			 public const string CardHolderName = "CardHolderName";
			 public const string BankID = "BankID";
			 public const string BankAccountNo = "BankAccountNo";
			 public const string Amount = "Amount";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string TransactionDate = "TransactionDate";
			 public const string GuarantorID = "GuarantorID";
			 public const string SRPaymentType = "SRPaymentType";
			 public const string SRPaymentMethod = "SRPaymentMethod";
			 public const string SRCardProvider = "SRCardProvider";
			 public const string SRCardType = "SRCardType";
			 public const string EDCMachineID = "EDCMachineID";
			 public const string CardHolderName = "CardHolderName";
			 public const string BankID = "BankID";
			 public const string BankAccountNo = "BankAccountNo";
			 public const string Amount = "Amount";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(GuarantorDepositMetadata))
			{
				if(GuarantorDepositMetadata.mapDelegates == null)
				{
					GuarantorDepositMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorDepositMetadata.meta == null)
				{
					GuarantorDepositMetadata.meta = new GuarantorDepositMetadata();
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
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPaymentMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardProvider", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCardType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EDCMachineID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CardHolderName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "GuarantorDeposit";
				meta.Destination = "GuarantorDeposit";
				
				meta.spInsert = "proc_GuarantorDepositInsert";				
				meta.spUpdate = "proc_GuarantorDepositUpdate";		
				meta.spDelete = "proc_GuarantorDepositDelete";
				meta.spLoadAll = "proc_GuarantorDepositLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorDepositLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorDepositMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
