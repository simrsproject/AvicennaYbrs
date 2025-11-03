/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:17 PM
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
	abstract public class esItemCostCollection : esEntityCollectionWAuditLog
	{
		public esItemCostCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemCostCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemCostQuery query)
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
			this.InitQuery(query as esItemCostQuery);
		}
		#endregion
		
		virtual public ItemCost DetachEntity(ItemCost entity)
		{
			return base.DetachEntity(entity) as ItemCost;
		}
		
		virtual public ItemCost AttachEntity(ItemCost entity)
		{
			return base.AttachEntity(entity) as ItemCost;
		}
		
		virtual public void Combine(ItemCostCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemCost this[int index]
		{
			get
			{
				return base[index] as ItemCost;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemCost);
		}
	}



	[Serializable]
	abstract public class esItemCost : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemCostQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemCost()
		{

		}

		public esItemCost(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRTariffType, itemID, classID, startingDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRTariffType, itemID, classID, startingDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRTariffType, itemID, classID, startingDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRTariffType, itemID, classID, startingDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate)
		{
			esItemCostQuery query = this.GetDynamicQuery();
			query.Where(query.SRTariffType == sRTariffType, query.ItemID == itemID, query.ClassID == classID, query.StartingDate == startingDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate)
		{
			esParameters parms = new esParameters();
			parms.Add("SRTariffType",sRTariffType);			parms.Add("ItemID",itemID);			parms.Add("ClassID",classID);			parms.Add("StartingDate",startingDate);
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
						case "SRTariffType": this.str.SRTariffType = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "StartingDate": this.str.StartingDate = (string)value; break;							
						case "MaterialCost": this.str.MaterialCost = (string)value; break;							
						case "AGCMaterialCost": this.str.AGCMaterialCost = (string)value; break;							
						case "LaborCost": this.str.LaborCost = (string)value; break;							
						case "AGCLaborCost": this.str.AGCLaborCost = (string)value; break;							
						case "OverheadCost": this.str.OverheadCost = (string)value; break;							
						case "AGCOverheadCost": this.str.AGCOverheadCost = (string)value; break;							
						case "SubContractCost": this.str.SubContractCost = (string)value; break;							
						case "AGCSubContractCost": this.str.AGCSubContractCost = (string)value; break;							
						case "OtherCost": this.str.OtherCost = (string)value; break;							
						case "AGCOtherCost": this.str.AGCOtherCost = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StartingDate":
						
							if (value == null || value is System.DateTime)
								this.StartingDate = (System.DateTime?)value;
							break;
						
						case "MaterialCost":
						
							if (value == null || value is System.Decimal)
								this.MaterialCost = (System.Decimal?)value;
							break;
						
						case "LaborCost":
						
							if (value == null || value is System.Decimal)
								this.LaborCost = (System.Decimal?)value;
							break;
						
						case "OverheadCost":
						
							if (value == null || value is System.Decimal)
								this.OverheadCost = (System.Decimal?)value;
							break;
						
						case "SubContractCost":
						
							if (value == null || value is System.Decimal)
								this.SubContractCost = (System.Decimal?)value;
							break;
						
						case "OtherCost":
						
							if (value == null || value is System.Decimal)
								this.OtherCost = (System.Decimal?)value;
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
		/// Maps to ItemCost.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.SRTariffType);
			}
			
			set
			{
				base.SetSystemString(ItemCostMetadata.ColumnNames.SRTariffType, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				if(base.SetSystemString(ItemCostMetadata.ColumnNames.ItemID, value))
				{
					this._UpToItemByItemID = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ItemCostMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(ItemCostMetadata.ColumnNames.StartingDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemCostMetadata.ColumnNames.StartingDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.MaterialCost
		/// </summary>
		virtual public System.Decimal? MaterialCost
		{
			get
			{
				return base.GetSystemDecimal(ItemCostMetadata.ColumnNames.MaterialCost);
			}
			
			set
			{
				base.SetSystemDecimal(ItemCostMetadata.ColumnNames.MaterialCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.AGCMaterialCost
		/// </summary>
		virtual public System.String AGCMaterialCost
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.AGCMaterialCost);
			}
			
			set
			{
				base.SetSystemString(ItemCostMetadata.ColumnNames.AGCMaterialCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.LaborCost
		/// </summary>
		virtual public System.Decimal? LaborCost
		{
			get
			{
				return base.GetSystemDecimal(ItemCostMetadata.ColumnNames.LaborCost);
			}
			
			set
			{
				base.SetSystemDecimal(ItemCostMetadata.ColumnNames.LaborCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.AGCLaborCost
		/// </summary>
		virtual public System.String AGCLaborCost
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.AGCLaborCost);
			}
			
			set
			{
				base.SetSystemString(ItemCostMetadata.ColumnNames.AGCLaborCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.OverheadCost
		/// </summary>
		virtual public System.Decimal? OverheadCost
		{
			get
			{
				return base.GetSystemDecimal(ItemCostMetadata.ColumnNames.OverheadCost);
			}
			
			set
			{
				base.SetSystemDecimal(ItemCostMetadata.ColumnNames.OverheadCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.AGCOverheadCost
		/// </summary>
		virtual public System.String AGCOverheadCost
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.AGCOverheadCost);
			}
			
			set
			{
				base.SetSystemString(ItemCostMetadata.ColumnNames.AGCOverheadCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.SubContractCost
		/// </summary>
		virtual public System.Decimal? SubContractCost
		{
			get
			{
				return base.GetSystemDecimal(ItemCostMetadata.ColumnNames.SubContractCost);
			}
			
			set
			{
				base.SetSystemDecimal(ItemCostMetadata.ColumnNames.SubContractCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.AGCSubContractCost
		/// </summary>
		virtual public System.String AGCSubContractCost
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.AGCSubContractCost);
			}
			
			set
			{
				base.SetSystemString(ItemCostMetadata.ColumnNames.AGCSubContractCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.OtherCost
		/// </summary>
		virtual public System.Decimal? OtherCost
		{
			get
			{
				return base.GetSystemDecimal(ItemCostMetadata.ColumnNames.OtherCost);
			}
			
			set
			{
				base.SetSystemDecimal(ItemCostMetadata.ColumnNames.OtherCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.AGCOtherCost
		/// </summary>
		virtual public System.String AGCOtherCost
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.AGCOtherCost);
			}
			
			set
			{
				base.SetSystemString(ItemCostMetadata.ColumnNames.AGCOtherCost, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemCostMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemCostMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemCost.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemCostMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemCostMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected Item _UpToItemByItemID;
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
			public esStrings(esItemCost entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRTariffType
			{
				get
				{
					System.String data = entity.SRTariffType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTariffType = null;
					else entity.SRTariffType = Convert.ToString(value);
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
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String StartingDate
			{
				get
				{
					System.DateTime? data = entity.StartingDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartingDate = null;
					else entity.StartingDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String MaterialCost
			{
				get
				{
					System.Decimal? data = entity.MaterialCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaterialCost = null;
					else entity.MaterialCost = Convert.ToDecimal(value);
				}
			}
				
			public System.String AGCMaterialCost
			{
				get
				{
					System.String data = entity.AGCMaterialCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AGCMaterialCost = null;
					else entity.AGCMaterialCost = Convert.ToString(value);
				}
			}
				
			public System.String LaborCost
			{
				get
				{
					System.Decimal? data = entity.LaborCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LaborCost = null;
					else entity.LaborCost = Convert.ToDecimal(value);
				}
			}
				
			public System.String AGCLaborCost
			{
				get
				{
					System.String data = entity.AGCLaborCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AGCLaborCost = null;
					else entity.AGCLaborCost = Convert.ToString(value);
				}
			}
				
			public System.String OverheadCost
			{
				get
				{
					System.Decimal? data = entity.OverheadCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OverheadCost = null;
					else entity.OverheadCost = Convert.ToDecimal(value);
				}
			}
				
			public System.String AGCOverheadCost
			{
				get
				{
					System.String data = entity.AGCOverheadCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AGCOverheadCost = null;
					else entity.AGCOverheadCost = Convert.ToString(value);
				}
			}
				
			public System.String SubContractCost
			{
				get
				{
					System.Decimal? data = entity.SubContractCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubContractCost = null;
					else entity.SubContractCost = Convert.ToDecimal(value);
				}
			}
				
			public System.String AGCSubContractCost
			{
				get
				{
					System.String data = entity.AGCSubContractCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AGCSubContractCost = null;
					else entity.AGCSubContractCost = Convert.ToString(value);
				}
			}
				
			public System.String OtherCost
			{
				get
				{
					System.Decimal? data = entity.OtherCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherCost = null;
					else entity.OtherCost = Convert.ToDecimal(value);
				}
			}
				
			public System.String AGCOtherCost
			{
				get
				{
					System.String data = entity.AGCOtherCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AGCOtherCost = null;
					else entity.AGCOtherCost = Convert.ToString(value);
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
			

			private esItemCost entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemCostQuery query)
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
				throw new Exception("esItemCost can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemCost : esItemCost
	{

				
		#region UpToItemByItemID - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - RefItemCostToItem
		/// </summary>

		[XmlIgnore]
		public Item UpToItemByItemID
		{
			get
			{
				if(this._UpToItemByItemID == null
					&& ItemID != null					)
				{
					this._UpToItemByItemID = new Item();
					this._UpToItemByItemID.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToItemByItemID", this._UpToItemByItemID);
					this._UpToItemByItemID.Query.Where(this._UpToItemByItemID.Query.ItemID == this.ItemID);
					this._UpToItemByItemID.Query.Load();
				}

				return this._UpToItemByItemID;
			}
			
			set
			{
				this.RemovePreSave("UpToItemByItemID");
				

				if(value == null)
				{
					this.ItemID = null;
					this._UpToItemByItemID = null;
				}
				else
				{
					this.ItemID = value.ItemID;
					this._UpToItemByItemID = value;
					this.SetPreSave("UpToItemByItemID", this._UpToItemByItemID);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esItemCostQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemCostMetadata.Meta();
			}
		}	
		

		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem MaterialCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.MaterialCost, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AGCMaterialCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.AGCMaterialCost, esSystemType.String);
			}
		} 
		
		public esQueryItem LaborCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.LaborCost, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AGCLaborCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.AGCLaborCost, esSystemType.String);
			}
		} 
		
		public esQueryItem OverheadCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.OverheadCost, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AGCOverheadCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.AGCOverheadCost, esSystemType.String);
			}
		} 
		
		public esQueryItem SubContractCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.SubContractCost, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AGCSubContractCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.AGCSubContractCost, esSystemType.String);
			}
		} 
		
		public esQueryItem OtherCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.OtherCost, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AGCOtherCost
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.AGCOtherCost, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemCostMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemCostCollection")]
	public partial class ItemCostCollection : esItemCostCollection, IEnumerable<ItemCost>
	{
		public ItemCostCollection()
		{

		}
		
		public static implicit operator List<ItemCost>(ItemCostCollection coll)
		{
			List<ItemCost> list = new List<ItemCost>();
			
			foreach (ItemCost emp in coll)
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
				return  ItemCostMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemCostQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemCost(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemCost();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemCostQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemCostQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemCostQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemCost AddNew()
		{
			ItemCost entity = base.AddNewEntity() as ItemCost;
			
			return entity;
		}

		public ItemCost FindByPrimaryKey(System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate)
		{
			return base.FindByPrimaryKey(sRTariffType, itemID, classID, startingDate) as ItemCost;
		}


		#region IEnumerable<ItemCost> Members

		IEnumerator<ItemCost> IEnumerable<ItemCost>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemCost;
			}
		}

		#endregion
		
		private ItemCostQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemCost' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemCost ({SRTariffType},{ItemID},{ClassID},{StartingDate})")]
	[Serializable]
	public partial class ItemCost : esItemCost
	{
		public ItemCost()
		{

		}
	
		public ItemCost(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemCostMetadata.Meta();
			}
		}
		
		
		
		override protected esItemCostQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemCostQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemCostQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemCostQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemCostQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemCostQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemCostQuery : esItemCostQuery
	{
		public ItemCostQuery()
		{

		}		
		
		public ItemCostQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemCostQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemCostMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemCostMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.SRTariffType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.SRTariffType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.StartingDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemCostMetadata.PropertyNames.StartingDate;
			c.IsInPrimaryKey = true;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.MaterialCost, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemCostMetadata.PropertyNames.MaterialCost;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.AGCMaterialCost, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.AGCMaterialCost;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.LaborCost, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemCostMetadata.PropertyNames.LaborCost;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.AGCLaborCost, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.AGCLaborCost;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.OverheadCost, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemCostMetadata.PropertyNames.OverheadCost;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.AGCOverheadCost, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.AGCOverheadCost;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.SubContractCost, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemCostMetadata.PropertyNames.SubContractCost;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.AGCSubContractCost, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.AGCSubContractCost;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.OtherCost, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemCostMetadata.PropertyNames.OtherCost;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.AGCOtherCost, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.AGCOtherCost;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemCostMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemCostMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemCostMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemCostMetadata Meta()
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
			 public const string SRTariffType = "SRTariffType";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string StartingDate = "StartingDate";
			 public const string MaterialCost = "MaterialCost";
			 public const string AGCMaterialCost = "AGCMaterialCost";
			 public const string LaborCost = "LaborCost";
			 public const string AGCLaborCost = "AGCLaborCost";
			 public const string OverheadCost = "OverheadCost";
			 public const string AGCOverheadCost = "AGCOverheadCost";
			 public const string SubContractCost = "SubContractCost";
			 public const string AGCSubContractCost = "AGCSubContractCost";
			 public const string OtherCost = "OtherCost";
			 public const string AGCOtherCost = "AGCOtherCost";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRTariffType = "SRTariffType";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string StartingDate = "StartingDate";
			 public const string MaterialCost = "MaterialCost";
			 public const string AGCMaterialCost = "AGCMaterialCost";
			 public const string LaborCost = "LaborCost";
			 public const string AGCLaborCost = "AGCLaborCost";
			 public const string OverheadCost = "OverheadCost";
			 public const string AGCOverheadCost = "AGCOverheadCost";
			 public const string SubContractCost = "SubContractCost";
			 public const string AGCSubContractCost = "AGCSubContractCost";
			 public const string OtherCost = "OtherCost";
			 public const string AGCOtherCost = "AGCOtherCost";
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
			lock (typeof(ItemCostMetadata))
			{
				if(ItemCostMetadata.mapDelegates == null)
				{
					ItemCostMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemCostMetadata.meta == null)
				{
					ItemCostMetadata.meta = new ItemCostMetadata();
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
				

				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("MaterialCost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AGCMaterialCost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LaborCost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AGCLaborCost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OverheadCost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AGCOverheadCost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SubContractCost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AGCSubContractCost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherCost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AGCOtherCost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemCost";
				meta.Destination = "ItemCost";
				
				meta.spInsert = "proc_ItemCostInsert";				
				meta.spUpdate = "proc_ItemCostUpdate";		
				meta.spDelete = "proc_ItemCostDelete";
				meta.spLoadAll = "proc_ItemCostLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemCostLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemCostMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
