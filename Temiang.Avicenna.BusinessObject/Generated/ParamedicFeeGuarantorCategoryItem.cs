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
	abstract public class esParamedicFeeGuarantorCategoryItemCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeGuarantorCategoryItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeGuarantorCategoryItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeGuarantorCategoryItemQuery query)
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
			this.InitQuery(query as esParamedicFeeGuarantorCategoryItemQuery);
		}
		#endregion
		
		virtual public ParamedicFeeGuarantorCategoryItem DetachEntity(ParamedicFeeGuarantorCategoryItem entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeGuarantorCategoryItem;
		}
		
		virtual public ParamedicFeeGuarantorCategoryItem AttachEntity(ParamedicFeeGuarantorCategoryItem entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeGuarantorCategoryItem;
		}
		
		virtual public void Combine(ParamedicFeeGuarantorCategoryItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeGuarantorCategoryItem this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeGuarantorCategoryItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeGuarantorCategoryItem);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeGuarantorCategoryItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeGuarantorCategoryItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeGuarantorCategoryItem()
		{

		}

		public esParamedicFeeGuarantorCategoryItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, paramedicID, sRPhysicianFeeType);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, paramedicID, sRPhysicianFeeType);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, paramedicID, sRPhysicianFeeType);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, paramedicID, sRPhysicianFeeType);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType)
		{
			esParamedicFeeGuarantorCategoryItemQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.ParamedicID == paramedicID, query.SRPhysicianFeeType == sRPhysicianFeeType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("ParamedicID",paramedicID);			parms.Add("SRPhysicianFeeType",sRPhysicianFeeType);
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
		/// Maps to ParamedicFeeGuarantorCategoryItem.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.SRPhysicianFeeType
		/// </summary>
		virtual public System.String SRPhysicianFeeType
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.SRPhysicianFeeType);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.SRPhysicianFeeType, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.IsParamedicFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsParamedicFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.IsParamedicFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.IsParamedicFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.ParamedicFeeAmount
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.ParamedicFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? ParamedicFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.IsDeductionFeeUsePercentage
		/// </summary>
		virtual public System.Boolean? IsDeductionFeeUsePercentage
		{
			get
			{
				return base.GetSystemBoolean(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.IsDeductionFeeUsePercentage);
			}
			
			set
			{
				base.SetSystemBoolean(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.IsDeductionFeeUsePercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.DeductionFeeAmount
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.DeductionFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.DeductionFeeAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.DeductionFeeAmountReferral
		/// </summary>
		virtual public System.Decimal? DeductionFeeAmountReferral
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.DeductionFeeAmountReferral);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.DeductionFeeAmountReferral, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeGuarantorCategoryItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeGuarantorCategoryItem entity)
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
			

			private esParamedicFeeGuarantorCategoryItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeGuarantorCategoryItemQuery query)
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
				throw new Exception("esParamedicFeeGuarantorCategoryItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeGuarantorCategoryItem : esParamedicFeeGuarantorCategoryItem
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
	abstract public class esParamedicFeeGuarantorCategoryItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeGuarantorCategoryItemMetadata.Meta();
			}
		}	
		

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRPhysicianFeeType
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.SRPhysicianFeeType, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsParamedicFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.IsParamedicFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ParamedicFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ParamedicFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsDeductionFeeUsePercentage
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.IsDeductionFeeUsePercentage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem DeductionFeeAmount
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.DeductionFeeAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeductionFeeAmountReferral
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.DeductionFeeAmountReferral, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeGuarantorCategoryItemCollection")]
	public partial class ParamedicFeeGuarantorCategoryItemCollection : esParamedicFeeGuarantorCategoryItemCollection, IEnumerable<ParamedicFeeGuarantorCategoryItem>
	{
		public ParamedicFeeGuarantorCategoryItemCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeGuarantorCategoryItem>(ParamedicFeeGuarantorCategoryItemCollection coll)
		{
			List<ParamedicFeeGuarantorCategoryItem> list = new List<ParamedicFeeGuarantorCategoryItem>();
			
			foreach (ParamedicFeeGuarantorCategoryItem emp in coll)
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
				return  ParamedicFeeGuarantorCategoryItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeGuarantorCategoryItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeGuarantorCategoryItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeGuarantorCategoryItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeGuarantorCategoryItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeGuarantorCategoryItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeGuarantorCategoryItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeGuarantorCategoryItem AddNew()
		{
			ParamedicFeeGuarantorCategoryItem entity = base.AddNewEntity() as ParamedicFeeGuarantorCategoryItem;
			
			return entity;
		}

		public ParamedicFeeGuarantorCategoryItem FindByPrimaryKey(System.String itemID, System.String paramedicID, System.String sRPhysicianFeeType)
		{
			return base.FindByPrimaryKey(itemID, paramedicID, sRPhysicianFeeType) as ParamedicFeeGuarantorCategoryItem;
		}


		#region IEnumerable<ParamedicFeeGuarantorCategoryItem> Members

		IEnumerator<ParamedicFeeGuarantorCategoryItem> IEnumerable<ParamedicFeeGuarantorCategoryItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeGuarantorCategoryItem;
			}
		}

		#endregion
		
		private ParamedicFeeGuarantorCategoryItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeGuarantorCategoryItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeGuarantorCategoryItem ({ParamedicID},{SRPhysicianFeeType},{ItemID})")]
	[Serializable]
	public partial class ParamedicFeeGuarantorCategoryItem : esParamedicFeeGuarantorCategoryItem
	{
		public ParamedicFeeGuarantorCategoryItem()
		{

		}
	
		public ParamedicFeeGuarantorCategoryItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeGuarantorCategoryItemMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeGuarantorCategoryItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeGuarantorCategoryItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeGuarantorCategoryItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeGuarantorCategoryItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeGuarantorCategoryItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeGuarantorCategoryItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeGuarantorCategoryItemQuery : esParamedicFeeGuarantorCategoryItemQuery
	{
		public ParamedicFeeGuarantorCategoryItemQuery()
		{

		}		
		
		public ParamedicFeeGuarantorCategoryItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeGuarantorCategoryItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeGuarantorCategoryItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeGuarantorCategoryItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.SRPhysicianFeeType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.SRPhysicianFeeType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.IsParamedicFeeUsePercentage, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.IsParamedicFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicFeeAmount, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.ParamedicFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.ParamedicFeeAmountReferral, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.ParamedicFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.IsDeductionFeeUsePercentage, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.IsDeductionFeeUsePercentage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.DeductionFeeAmount, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.DeductionFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.DeductionFeeAmountReferral, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.DeductionFeeAmountReferral;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeGuarantorCategoryItemMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeGuarantorCategoryItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeGuarantorCategoryItemMetadata Meta()
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
			lock (typeof(ParamedicFeeGuarantorCategoryItemMetadata))
			{
				if(ParamedicFeeGuarantorCategoryItemMetadata.mapDelegates == null)
				{
					ParamedicFeeGuarantorCategoryItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeGuarantorCategoryItemMetadata.meta == null)
				{
					ParamedicFeeGuarantorCategoryItemMetadata.meta = new ParamedicFeeGuarantorCategoryItemMetadata();
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
				meta.AddTypeMap("IsParamedicFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ParamedicFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ParamedicFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsDeductionFeeUsePercentage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("DeductionFeeAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionFeeAmountReferral", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeGuarantorCategoryItem";
				meta.Destination = "ParamedicFeeGuarantorCategoryItem";
				
				meta.spInsert = "proc_ParamedicFeeGuarantorCategoryItemInsert";				
				meta.spUpdate = "proc_ParamedicFeeGuarantorCategoryItemUpdate";		
				meta.spDelete = "proc_ParamedicFeeGuarantorCategoryItemDelete";
				meta.spLoadAll = "proc_ParamedicFeeGuarantorCategoryItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeGuarantorCategoryItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeGuarantorCategoryItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
