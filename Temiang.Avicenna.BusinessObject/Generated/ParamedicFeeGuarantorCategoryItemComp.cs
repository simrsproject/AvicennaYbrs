/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/19/2015 10:37:48 AM
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
	abstract public class esParamedicFeeGuarantorCategoryItemCompCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeGuarantorCategoryItemCompCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeGuarantorCategoryItemCompCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeGuarantorCategoryItemCompQuery query)
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
			this.InitQuery(query as esParamedicFeeGuarantorCategoryItemCompQuery);
		}
		#endregion
		
		virtual public ParamedicFeeGuarantorCategoryItemComp DetachEntity(ParamedicFeeGuarantorCategoryItemComp entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeGuarantorCategoryItemComp;
		}
		
		virtual public ParamedicFeeGuarantorCategoryItemComp AttachEntity(ParamedicFeeGuarantorCategoryItemComp entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeGuarantorCategoryItemComp;
		}
		
		virtual public void Combine(ParamedicFeeGuarantorCategoryItemCompCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeGuarantorCategoryItemComp this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeGuarantorCategoryItemComp;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeGuarantorCategoryItemComp);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeGuarantorCategoryItemComp : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeGuarantorCategoryItemCompQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeGuarantorCategoryItemComp()
		{

		}

		public esParamedicFeeGuarantorCategoryItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, paramedicID, sRPhysicianFeeType, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, paramedicID, sRPhysicianFeeType, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, paramedicID, sRPhysicianFeeType, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, paramedicID, sRPhysicianFeeType, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType, System.String tariffComponentID)
		{
			esParamedicFeeGuarantorCategoryItemCompQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.ParamedicID == paramedicID, query.SRPhysicianFeeType == sRPhysicianFeeType, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("ParamedicID",paramedicID);			parms.Add("SRPhysicianFeeType",sRPhysicianFeeType);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "SRPhysicianFeeType": this.str.SRPhysicianFeeType = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "IsParamedicFeeUsePercentage": this.str.IsParamedicFeeUsePercentage = (string)value; break;							
						case "ParamedicFeeAmount": this.str.ParamedicFeeAmount = (string)value; break;							
						case "ParamedicFeeAmountReferral": this.str.ParamedicFeeAmountReferral = (string)value; break;							
						case "IsDeductionFeeUsePercentage": this.str.IsDeductionFeeUsePercentage = (string)value; break;							
						case "DeductionFeeAmount": this.str.DeductionFeeAmount = (string)value; break;							
						case "DeductionFeeAmountReferral": this.str.DeductionFeeAmountReferral = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsParamedicFeeUsePercentage":
						
							if (value == null || value is System.Boolean)
								this.IsParamedicFeeUsePercentage = (System.Boolean?)value;
							break;
						
						case "ParamedicFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.ParamedicFeeAmount = (System.Decimal?)value;
							break;
						
						case "ParamedicFeeAmountReferral":
						
							if (value == null || value is System.Decimal)
								this.ParamedicFeeAmountReferral = (System.Decimal?)value;
							break;
						
						case "IsDeductionFeeUsePercentage":
						
							if (value == null || value is System.Boolean)
								this.IsDeductionFeeUsePercentage = (System.Boolean?)value;
							break;
						
						case "DeductionFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.DeductionFeeAmount = (System.Decimal?)value;
							break;
						
						case "DeductionFeeAmountReferral":
						
							if (value == null || value is System.Decimal)
								this.DeductionFeeAmountReferral = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.SRPhysicianFeeType
		/// </summary>
		virtual public System.String SRPhysicianFeeType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.SRPhysicianFeeType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.SRPhysicianFeeType, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.IsParamedicFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsParamedicFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.IsParamedicFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.ParamedicFeeAmount
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.ParamedicFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.IsDeductionFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsDeductionFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.IsDeductionFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.DeductionFeeAmount
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.DeductionFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.DeductionFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.DeductionFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.DeductionFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.DeductionFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItemComp.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeGuarantorCategoryItemComp entity)
			{
				this.entity = entity;
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
				
			public System.String SRPhysicianFeeType
			{
				get
				{
					System.String data = entity.SRPhysicianFeeType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicianFeeType = null;
					else entity.SRPhysicianFeeType = Convert.ToString(value);
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
				
			public System.String IsParamedicFeeUsePercentage
			{
				get
				{
					System.Boolean? data = entity.IsParamedicFeeUsePercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsParamedicFeeUsePercentage = null;
					else entity.IsParamedicFeeUsePercentage = Convert.ToBoolean(value);
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
				
			public System.String ParamedicFeeAmountReferral
			{
				get
				{
					System.Decimal? data = entity.ParamedicFeeAmountReferral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicFeeAmountReferral = null;
					else entity.ParamedicFeeAmountReferral = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsDeductionFeeUsePercentage
			{
				get
				{
					System.Boolean? data = entity.IsDeductionFeeUsePercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeductionFeeUsePercentage = null;
					else entity.IsDeductionFeeUsePercentage = Convert.ToBoolean(value);
				}
			}
				
			public System.String DeductionFeeAmount
			{
				get
				{
					System.Decimal? data = entity.DeductionFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionFeeAmount = null;
					else entity.DeductionFeeAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DeductionFeeAmountReferral
			{
				get
				{
					System.Decimal? data = entity.DeductionFeeAmountReferral;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionFeeAmountReferral = null;
					else entity.DeductionFeeAmountReferral = Convert.ToDecimal(value);
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
			

			private esParamedicFeeGuarantorCategoryItemComp entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeGuarantorCategoryItemCompQuery query)
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
				throw new Exception("esParamedicFeeGuarantorCategoryItemComp can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeGuarantorCategoryItemComp : esParamedicFeeGuarantorCategoryItemComp
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
	abstract public class esParamedicFeeGuarantorCategoryItemCompQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeGuarantorCategoryItemCompMetadata.Meta();
			}
		}	
		

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPhysicianFeeType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.SRPhysicianFeeType, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsParamedicFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ParamedicFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsDeductionFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem DeductionFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.DeductionFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeductionFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.DeductionFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeGuarantorCategoryItemCompCollection")]
	public partial class ParamedicFeeGuarantorCategoryItemCompCollection : esParamedicFeeGuarantorCategoryItemCompCollection, IEnumerable<ParamedicFeeGuarantorCategoryItemComp>
	{
		public ParamedicFeeGuarantorCategoryItemCompCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeGuarantorCategoryItemComp>(ParamedicFeeGuarantorCategoryItemCompCollection coll)
		{
			List<ParamedicFeeGuarantorCategoryItemComp> list = new List<ParamedicFeeGuarantorCategoryItemComp>();
			
			foreach (ParamedicFeeGuarantorCategoryItemComp emp in coll)
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
				return  ParamedicFeeGuarantorCategoryItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeGuarantorCategoryItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeGuarantorCategoryItemComp(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeGuarantorCategoryItemComp();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeGuarantorCategoryItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeGuarantorCategoryItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeGuarantorCategoryItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeGuarantorCategoryItemComp AddNew()
		{
			ParamedicFeeGuarantorCategoryItemComp entity = base.AddNewEntity() as ParamedicFeeGuarantorCategoryItemComp;
			
			return entity;
		}

		public ParamedicFeeGuarantorCategoryItemComp FindByPrimaryKey(System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(itemID, paramedicID, sRPhysicianFeeType, tariffComponentID) as ParamedicFeeGuarantorCategoryItemComp;
		}


		#region IEnumerable<ParamedicFeeGuarantorCategoryItemComp> Members

		IEnumerator<ParamedicFeeGuarantorCategoryItemComp> IEnumerable<ParamedicFeeGuarantorCategoryItemComp>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeGuarantorCategoryItemComp;
			}
		}

		#endregion
		
		private ParamedicFeeGuarantorCategoryItemCompQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeGuarantorCategoryItemComp' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeGuarantorCategoryItemComp ({ParamedicID},{SRPhysicianFeeType},{ItemID},{TariffComponentID})")]
	[Serializable]
	public partial class ParamedicFeeGuarantorCategoryItemComp : esParamedicFeeGuarantorCategoryItemComp
	{
		public ParamedicFeeGuarantorCategoryItemComp()
		{

		}
	
		public ParamedicFeeGuarantorCategoryItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeGuarantorCategoryItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeGuarantorCategoryItemCompQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeGuarantorCategoryItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeGuarantorCategoryItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeGuarantorCategoryItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeGuarantorCategoryItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeGuarantorCategoryItemCompQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeGuarantorCategoryItemCompQuery : esParamedicFeeGuarantorCategoryItemCompQuery
	{
		public ParamedicFeeGuarantorCategoryItemCompQuery()
		{

		}		
		
		public ParamedicFeeGuarantorCategoryItemCompQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeGuarantorCategoryItemCompQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeGuarantorCategoryItemCompMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeGuarantorCategoryItemCompMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.SRPhysicianFeeType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.SRPhysicianFeeType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.IsParamedicFeeUsePercentage, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.IsParamedicFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicFeeAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.ParamedicFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.ParamedicFeeAmountReferral, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.ParamedicFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.IsDeductionFeeUsePercentage, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.IsDeductionFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.DeductionFeeAmount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.DeductionFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.DeductionFeeAmountReferral, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.DeductionFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemCompMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemCompMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeGuarantorCategoryItemCompMetadata Meta()
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
			 public const string ParamedicID = "ParamedicID";
			 public const string SRPhysicianFeeType = "SRPhysicianFeeType";
			 public const string ItemID = "ItemID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
			 public const string ParamedicFeeAmount = "ParamedicFeeAmount";
			 public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
			 public const string IsDeductionFeeUsePercentage = "IsDeductionFeeUsePercentage";
			 public const string DeductionFeeAmount = "DeductionFeeAmount";
			 public const string DeductionFeeAmountReferral = "DeductionFeeAmountReferral";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ParamedicID = "ParamedicID";
			 public const string SRPhysicianFeeType = "SRPhysicianFeeType";
			 public const string ItemID = "ItemID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string IsParamedicFeeUsePercentage = "IsParamedicFeeUsePercentage";
			 public const string ParamedicFeeAmount = "ParamedicFeeAmount";
			 public const string ParamedicFeeAmountReferral = "ParamedicFeeAmountReferral";
			 public const string IsDeductionFeeUsePercentage = "IsDeductionFeeUsePercentage";
			 public const string DeductionFeeAmount = "DeductionFeeAmount";
			 public const string DeductionFeeAmountReferral = "DeductionFeeAmountReferral";
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
			lock (typeof(ParamedicFeeGuarantorCategoryItemCompMetadata))
			{
				if(ParamedicFeeGuarantorCategoryItemCompMetadata.mapDelegates == null)
				{
					ParamedicFeeGuarantorCategoryItemCompMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeGuarantorCategoryItemCompMetadata.meta == null)
				{
					ParamedicFeeGuarantorCategoryItemCompMetadata.meta = new ParamedicFeeGuarantorCategoryItemCompMetadata();
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
				

				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRPhysicianFeeType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsParamedicFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsDeductionFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeductionFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeGuarantorCategoryItemComp";
				meta.Destination = "ParamedicFeeGuarantorCategoryItemComp";
				
				meta.spInsert = "proc_ParamedicFeeGuarantorCategoryItemCompInsert";				
				meta.spUpdate = "proc_ParamedicFeeGuarantorCategoryItemCompUpdate";		
				meta.spDelete = "proc_ParamedicFeeGuarantorCategoryItemCompDelete";
				meta.spLoadAll = "proc_ParamedicFeeGuarantorCategoryItemCompLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeGuarantorCategoryItemCompLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeGuarantorCategoryItemCompMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
