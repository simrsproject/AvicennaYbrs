/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
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
	abstract public class esMedicalBenefitClaimCollection : esEntityCollectionWAuditLog
	{
		public esMedicalBenefitClaimCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MedicalBenefitClaimCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalBenefitClaimQuery query)
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
			this.InitQuery(query as esMedicalBenefitClaimQuery);
		}
		#endregion
		
		virtual public MedicalBenefitClaim DetachEntity(MedicalBenefitClaim entity)
		{
			return base.DetachEntity(entity) as MedicalBenefitClaim;
		}
		
		virtual public MedicalBenefitClaim AttachEntity(MedicalBenefitClaim entity)
		{
			return base.AttachEntity(entity) as MedicalBenefitClaim;
		}
		
		virtual public void Combine(MedicalBenefitClaimCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalBenefitClaim this[int index]
		{
			get
			{
				return base[index] as MedicalBenefitClaim;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalBenefitClaim);
		}
	}



	[Serializable]
	abstract public class esMedicalBenefitClaim : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalBenefitClaimQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalBenefitClaim()
		{

		}

		public esMedicalBenefitClaim(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 medicalBenefitClaimID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalBenefitClaimID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalBenefitClaimID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 medicalBenefitClaimID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(medicalBenefitClaimID);
			else
				return LoadByPrimaryKeyStoredProcedure(medicalBenefitClaimID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 medicalBenefitClaimID)
		{
			esMedicalBenefitClaimQuery query = this.GetDynamicQuery();
			query.Where(query.MedicalBenefitClaimID == medicalBenefitClaimID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 medicalBenefitClaimID)
		{
			esParameters parms = new esParameters();
			parms.Add("MedicalBenefitClaimID",medicalBenefitClaimID);
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
						case "MedicalBenefitClaimID": this.str.MedicalBenefitClaimID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "ClaimDate": this.str.ClaimDate = (string)value; break;							
						case "MedicalBenefitInfoID": this.str.MedicalBenefitInfoID = (string)value; break;							
						case "YearPeriodID": this.str.YearPeriodID = (string)value; break;							
						case "SettlementDate": this.str.SettlementDate = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "TotalAmountOnBill": this.str.TotalAmountOnBill = (string)value; break;							
						case "TotalReimbursementAmount": this.str.TotalReimbursementAmount = (string)value; break;							
						case "TotalNonReimbursementAmount": this.str.TotalNonReimbursementAmount = (string)value; break;							
						case "TotalApprovedAmount": this.str.TotalApprovedAmount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MedicalBenefitClaimID":
						
							if (value == null || value is System.Int32)
								this.MedicalBenefitClaimID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "ClaimDate":
						
							if (value == null || value is System.DateTime)
								this.ClaimDate = (System.DateTime?)value;
							break;
						
						case "MedicalBenefitInfoID":
						
							if (value == null || value is System.Int32)
								this.MedicalBenefitInfoID = (System.Int32?)value;
							break;
						
						case "YearPeriodID":
						
							if (value == null || value is System.Int32)
								this.YearPeriodID = (System.Int32?)value;
							break;
						
						case "SettlementDate":
						
							if (value == null || value is System.DateTime)
								this.SettlementDate = (System.DateTime?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "TotalAmountOnBill":
						
							if (value == null || value is System.Decimal)
								this.TotalAmountOnBill = (System.Decimal?)value;
							break;
						
						case "TotalReimbursementAmount":
						
							if (value == null || value is System.Decimal)
								this.TotalReimbursementAmount = (System.Decimal?)value;
							break;
						
						case "TotalNonReimbursementAmount":
						
							if (value == null || value is System.Decimal)
								this.TotalNonReimbursementAmount = (System.Decimal?)value;
							break;
						
						case "TotalApprovedAmount":
						
							if (value == null || value is System.Decimal)
								this.TotalApprovedAmount = (System.Decimal?)value;
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
		/// Maps to MedicalBenefitClaim.MedicalBenefitClaimID
		/// </summary>
		virtual public System.Int32? MedicalBenefitClaimID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitClaimMetadata.ColumnNames.MedicalBenefitClaimID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitClaimMetadata.ColumnNames.MedicalBenefitClaimID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitClaimMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitClaimMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.ClaimDate
		/// </summary>
		virtual public System.DateTime? ClaimDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitClaimMetadata.ColumnNames.ClaimDate);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitClaimMetadata.ColumnNames.ClaimDate, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.MedicalBenefitInfoID
		/// </summary>
		virtual public System.Int32? MedicalBenefitInfoID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitClaimMetadata.ColumnNames.MedicalBenefitInfoID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitClaimMetadata.ColumnNames.MedicalBenefitInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.YearPeriodID
		/// </summary>
		virtual public System.Int32? YearPeriodID
		{
			get
			{
				return base.GetSystemInt32(MedicalBenefitClaimMetadata.ColumnNames.YearPeriodID);
			}
			
			set
			{
				base.SetSystemInt32(MedicalBenefitClaimMetadata.ColumnNames.YearPeriodID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.SettlementDate
		/// </summary>
		virtual public System.DateTime? SettlementDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitClaimMetadata.ColumnNames.SettlementDate);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitClaimMetadata.ColumnNames.SettlementDate, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(MedicalBenefitClaimMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalBenefitClaimMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.TotalAmountOnBill
		/// </summary>
		virtual public System.Decimal? TotalAmountOnBill
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitClaimMetadata.ColumnNames.TotalAmountOnBill);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitClaimMetadata.ColumnNames.TotalAmountOnBill, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.TotalReimbursementAmount
		/// </summary>
		virtual public System.Decimal? TotalReimbursementAmount
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitClaimMetadata.ColumnNames.TotalReimbursementAmount);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitClaimMetadata.ColumnNames.TotalReimbursementAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.TotalNonReimbursementAmount
		/// </summary>
		virtual public System.Decimal? TotalNonReimbursementAmount
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitClaimMetadata.ColumnNames.TotalNonReimbursementAmount);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitClaimMetadata.ColumnNames.TotalNonReimbursementAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.TotalApprovedAmount
		/// </summary>
		virtual public System.Decimal? TotalApprovedAmount
		{
			get
			{
				return base.GetSystemDecimal(MedicalBenefitClaimMetadata.ColumnNames.TotalApprovedAmount);
			}
			
			set
			{
				base.SetSystemDecimal(MedicalBenefitClaimMetadata.ColumnNames.TotalApprovedAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalBenefitClaimMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalBenefitClaimMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalBenefitClaim.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalBenefitClaimMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalBenefitClaimMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMedicalBenefitClaim entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MedicalBenefitClaimID
			{
				get
				{
					System.Int32? data = entity.MedicalBenefitClaimID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitClaimID = null;
					else entity.MedicalBenefitClaimID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String ClaimDate
			{
				get
				{
					System.DateTime? data = entity.ClaimDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClaimDate = null;
					else entity.ClaimDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String MedicalBenefitInfoID
			{
				get
				{
					System.Int32? data = entity.MedicalBenefitInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MedicalBenefitInfoID = null;
					else entity.MedicalBenefitInfoID = Convert.ToInt32(value);
				}
			}
				
			public System.String YearPeriodID
			{
				get
				{
					System.Int32? data = entity.YearPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.YearPeriodID = null;
					else entity.YearPeriodID = Convert.ToInt32(value);
				}
			}
				
			public System.String SettlementDate
			{
				get
				{
					System.DateTime? data = entity.SettlementDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SettlementDate = null;
					else entity.SettlementDate = Convert.ToDateTime(value);
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
				
			public System.String TotalAmountOnBill
			{
				get
				{
					System.Decimal? data = entity.TotalAmountOnBill;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalAmountOnBill = null;
					else entity.TotalAmountOnBill = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalReimbursementAmount
			{
				get
				{
					System.Decimal? data = entity.TotalReimbursementAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalReimbursementAmount = null;
					else entity.TotalReimbursementAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalNonReimbursementAmount
			{
				get
				{
					System.Decimal? data = entity.TotalNonReimbursementAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalNonReimbursementAmount = null;
					else entity.TotalNonReimbursementAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String TotalApprovedAmount
			{
				get
				{
					System.Decimal? data = entity.TotalApprovedAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TotalApprovedAmount = null;
					else entity.TotalApprovedAmount = Convert.ToDecimal(value);
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
			

			private esMedicalBenefitClaim entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalBenefitClaimQuery query)
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
				throw new Exception("esMedicalBenefitClaim can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MedicalBenefitClaim : esMedicalBenefitClaim
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
	abstract public class esMedicalBenefitClaimQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MedicalBenefitClaimMetadata.Meta();
			}
		}	
		

		public esQueryItem MedicalBenefitClaimID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.MedicalBenefitClaimID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ClaimDate
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.ClaimDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem MedicalBenefitInfoID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.MedicalBenefitInfoID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem YearPeriodID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.YearPeriodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SettlementDate
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.SettlementDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem TotalAmountOnBill
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.TotalAmountOnBill, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalReimbursementAmount
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.TotalReimbursementAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalNonReimbursementAmount
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.TotalNonReimbursementAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TotalApprovedAmount
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.TotalApprovedAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalBenefitClaimMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalBenefitClaimCollection")]
	public partial class MedicalBenefitClaimCollection : esMedicalBenefitClaimCollection, IEnumerable<MedicalBenefitClaim>
	{
		public MedicalBenefitClaimCollection()
		{

		}
		
		public static implicit operator List<MedicalBenefitClaim>(MedicalBenefitClaimCollection coll)
		{
			List<MedicalBenefitClaim> list = new List<MedicalBenefitClaim>();
			
			foreach (MedicalBenefitClaim emp in coll)
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
				return  MedicalBenefitClaimMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalBenefitClaimQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalBenefitClaim(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalBenefitClaim();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MedicalBenefitClaimQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalBenefitClaimQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MedicalBenefitClaimQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MedicalBenefitClaim AddNew()
		{
			MedicalBenefitClaim entity = base.AddNewEntity() as MedicalBenefitClaim;
			
			return entity;
		}

		public MedicalBenefitClaim FindByPrimaryKey(System.Int32 medicalBenefitClaimID)
		{
			return base.FindByPrimaryKey(medicalBenefitClaimID) as MedicalBenefitClaim;
		}


		#region IEnumerable<MedicalBenefitClaim> Members

		IEnumerator<MedicalBenefitClaim> IEnumerable<MedicalBenefitClaim>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalBenefitClaim;
			}
		}

		#endregion
		
		private MedicalBenefitClaimQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalBenefitClaim' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MedicalBenefitClaim ({MedicalBenefitClaimID})")]
	[Serializable]
	public partial class MedicalBenefitClaim : esMedicalBenefitClaim
	{
		public MedicalBenefitClaim()
		{

		}
	
		public MedicalBenefitClaim(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalBenefitClaimMetadata.Meta();
			}
		}
		
		
		
		override protected esMedicalBenefitClaimQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalBenefitClaimQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MedicalBenefitClaimQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalBenefitClaimQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MedicalBenefitClaimQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MedicalBenefitClaimQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MedicalBenefitClaimQuery : esMedicalBenefitClaimQuery
	{
		public MedicalBenefitClaimQuery()
		{

		}		
		
		public MedicalBenefitClaimQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MedicalBenefitClaimQuery";
        }
		
			
	}


	[Serializable]
	public partial class MedicalBenefitClaimMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalBenefitClaimMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.MedicalBenefitClaimID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.MedicalBenefitClaimID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.ClaimDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.ClaimDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.MedicalBenefitInfoID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.MedicalBenefitInfoID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.YearPeriodID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.YearPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.SettlementDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.SettlementDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.IsApproved;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.TotalAmountOnBill, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.TotalAmountOnBill;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.TotalReimbursementAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.TotalReimbursementAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.TotalNonReimbursementAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.TotalNonReimbursementAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.TotalApprovedAmount, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.TotalApprovedAmount;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalBenefitClaimMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalBenefitClaimMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MedicalBenefitClaimMetadata Meta()
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
			 public const string MedicalBenefitClaimID = "MedicalBenefitClaimID";
			 public const string PersonID = "PersonID";
			 public const string ClaimDate = "ClaimDate";
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string YearPeriodID = "YearPeriodID";
			 public const string SettlementDate = "SettlementDate";
			 public const string IsApproved = "IsApproved";
			 public const string TotalAmountOnBill = "TotalAmountOnBill";
			 public const string TotalReimbursementAmount = "TotalReimbursementAmount";
			 public const string TotalNonReimbursementAmount = "TotalNonReimbursementAmount";
			 public const string TotalApprovedAmount = "TotalApprovedAmount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MedicalBenefitClaimID = "MedicalBenefitClaimID";
			 public const string PersonID = "PersonID";
			 public const string ClaimDate = "ClaimDate";
			 public const string MedicalBenefitInfoID = "MedicalBenefitInfoID";
			 public const string YearPeriodID = "YearPeriodID";
			 public const string SettlementDate = "SettlementDate";
			 public const string IsApproved = "IsApproved";
			 public const string TotalAmountOnBill = "TotalAmountOnBill";
			 public const string TotalReimbursementAmount = "TotalReimbursementAmount";
			 public const string TotalNonReimbursementAmount = "TotalNonReimbursementAmount";
			 public const string TotalApprovedAmount = "TotalApprovedAmount";
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
			lock (typeof(MedicalBenefitClaimMetadata))
			{
				if(MedicalBenefitClaimMetadata.mapDelegates == null)
				{
					MedicalBenefitClaimMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalBenefitClaimMetadata.meta == null)
				{
					MedicalBenefitClaimMetadata.meta = new MedicalBenefitClaimMetadata();
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
				

				meta.AddTypeMap("MedicalBenefitClaimID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ClaimDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("MedicalBenefitInfoID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("YearPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SettlementDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TotalAmountOnBill", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TotalReimbursementAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TotalNonReimbursementAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TotalApprovedAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MedicalBenefitClaim";
				meta.Destination = "MedicalBenefitClaim";
				
				meta.spInsert = "proc_MedicalBenefitClaimInsert";				
				meta.spUpdate = "proc_MedicalBenefitClaimUpdate";		
				meta.spDelete = "proc_MedicalBenefitClaimDelete";
				meta.spLoadAll = "proc_MedicalBenefitClaimLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalBenefitClaimLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalBenefitClaimMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
