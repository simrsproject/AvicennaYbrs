/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/12/2013 10:58:58 AM
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
	abstract public class esItemTariffComponentUpdateHistoryCollection : esEntityCollectionWAuditLog
	{
		public esItemTariffComponentUpdateHistoryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemTariffComponentUpdateHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffComponentUpdateHistoryQuery query)
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
			this.InitQuery(query as esItemTariffComponentUpdateHistoryQuery);
		}
		#endregion
		
		virtual public ItemTariffComponentUpdateHistory DetachEntity(ItemTariffComponentUpdateHistory entity)
		{
			return base.DetachEntity(entity) as ItemTariffComponentUpdateHistory;
		}
		
		virtual public ItemTariffComponentUpdateHistory AttachEntity(ItemTariffComponentUpdateHistory entity)
		{
			return base.AttachEntity(entity) as ItemTariffComponentUpdateHistory;
		}
		
		virtual public void Combine(ItemTariffComponentUpdateHistoryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemTariffComponentUpdateHistory this[int index]
		{
			get
			{
				return base[index] as ItemTariffComponentUpdateHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariffComponentUpdateHistory);
		}
	}



	[Serializable]
	abstract public class esItemTariffComponentUpdateHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffComponentUpdateHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTariffComponentUpdateHistory()
		{

		}

		public esItemTariffComponentUpdateHistory(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String requestNo, System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(requestNo, sRTariffType, itemID, classID, startingDate, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(requestNo, sRTariffType, itemID, classID, startingDate, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String requestNo, System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(requestNo, sRTariffType, itemID, classID, startingDate, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(requestNo, sRTariffType, itemID, classID, startingDate, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String requestNo, System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate, System.String tariffComponentID)
		{
			esItemTariffComponentUpdateHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RequestNo == requestNo, query.SRTariffType == sRTariffType, query.ItemID == itemID, query.ClassID == classID, query.StartingDate == startingDate, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String requestNo, System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("RequestNo",requestNo);			parms.Add("SRTariffType",sRTariffType);			parms.Add("ItemID",itemID);			parms.Add("ClassID",classID);			parms.Add("StartingDate",startingDate);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "RequestNo": this.str.RequestNo = (string)value; break;							
						case "SRTariffType": this.str.SRTariffType = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "StartingDate": this.str.StartingDate = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "ToPrice": this.str.ToPrice = (string)value; break;							
						case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;							
						case "ToIsAllowDiscount": this.str.ToIsAllowDiscount = (string)value; break;							
						case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;							
						case "ToIsAllowVariable": this.str.ToIsAllowVariable = (string)value; break;							
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
						
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "ToPrice":
						
							if (value == null || value is System.Decimal)
								this.ToPrice = (System.Decimal?)value;
							break;
						
						case "IsAllowDiscount":
						
							if (value == null || value is System.Boolean)
								this.IsAllowDiscount = (System.Boolean?)value;
							break;
						
						case "ToIsAllowDiscount":
						
							if (value == null || value is System.Boolean)
								this.ToIsAllowDiscount = (System.Boolean?)value;
							break;
						
						case "IsAllowVariable":
						
							if (value == null || value is System.Boolean)
								this.IsAllowVariable = (System.Boolean?)value;
							break;
						
						case "ToIsAllowVariable":
						
							if (value == null || value is System.Boolean)
								this.ToIsAllowVariable = (System.Boolean?)value;
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
		/// Maps to ItemTariffComponentUpdateHistory.RequestNo
		/// </summary>
		virtual public System.String RequestNo
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.RequestNo);
			}
			
			set
			{
				base.SetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.RequestNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.SRTariffType
		/// </summary>
		virtual public System.String SRTariffType
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.SRTariffType);
			}
			
			set
			{
				base.SetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.SRTariffType, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.StartingDate
		/// </summary>
		virtual public System.DateTime? StartingDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.StartingDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.StartingDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.ToPrice
		/// </summary>
		virtual public System.Decimal? ToPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToPrice);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.IsAllowDiscount
		/// </summary>
		virtual public System.Boolean? IsAllowDiscount
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.IsAllowDiscount);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.IsAllowDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.ToIsAllowDiscount
		/// </summary>
		virtual public System.Boolean? ToIsAllowDiscount
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToIsAllowDiscount);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToIsAllowDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.IsAllowVariable
		/// </summary>
		virtual public System.Boolean? IsAllowVariable
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.IsAllowVariable);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.IsAllowVariable, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.ToIsAllowVariable
		/// </summary>
		virtual public System.Boolean? ToIsAllowVariable
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToIsAllowVariable);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToIsAllowVariable, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffComponentUpdateHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemTariffComponentUpdateHistory entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RequestNo
			{
				get
				{
					System.String data = entity.RequestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestNo = null;
					else entity.RequestNo = Convert.ToString(value);
				}
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
				
			public System.String ToPrice
			{
				get
				{
					System.Decimal? data = entity.ToPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToPrice = null;
					else entity.ToPrice = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsAllowDiscount
			{
				get
				{
					System.Boolean? data = entity.IsAllowDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowDiscount = null;
					else entity.IsAllowDiscount = Convert.ToBoolean(value);
				}
			}
				
			public System.String ToIsAllowDiscount
			{
				get
				{
					System.Boolean? data = entity.ToIsAllowDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToIsAllowDiscount = null;
					else entity.ToIsAllowDiscount = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsAllowVariable
			{
				get
				{
					System.Boolean? data = entity.IsAllowVariable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowVariable = null;
					else entity.IsAllowVariable = Convert.ToBoolean(value);
				}
			}
				
			public System.String ToIsAllowVariable
			{
				get
				{
					System.Boolean? data = entity.ToIsAllowVariable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToIsAllowVariable = null;
					else entity.ToIsAllowVariable = Convert.ToBoolean(value);
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
			

			private esItemTariffComponentUpdateHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffComponentUpdateHistoryQuery query)
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
				throw new Exception("esItemTariffComponentUpdateHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemTariffComponentUpdateHistory : esItemTariffComponentUpdateHistory
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
	abstract public class esItemTariffComponentUpdateHistoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffComponentUpdateHistoryMetadata.Meta();
			}
		}	
		

		public esQueryItem RequestNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.RequestNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRTariffType
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.SRTariffType, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem StartingDate
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.StartingDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ToPrice
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToPrice, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsAllowDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ToIsAllowDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToIsAllowDiscount, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAllowVariable
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem ToIsAllowVariable
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToIsAllowVariable, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffComponentUpdateHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffComponentUpdateHistoryCollection")]
	public partial class ItemTariffComponentUpdateHistoryCollection : esItemTariffComponentUpdateHistoryCollection, IEnumerable<ItemTariffComponentUpdateHistory>
	{
		public ItemTariffComponentUpdateHistoryCollection()
		{

		}
		
		public static implicit operator List<ItemTariffComponentUpdateHistory>(ItemTariffComponentUpdateHistoryCollection coll)
		{
			List<ItemTariffComponentUpdateHistory> list = new List<ItemTariffComponentUpdateHistory>();
			
			foreach (ItemTariffComponentUpdateHistory emp in coll)
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
				return  ItemTariffComponentUpdateHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffComponentUpdateHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariffComponentUpdateHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariffComponentUpdateHistory();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemTariffComponentUpdateHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffComponentUpdateHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemTariffComponentUpdateHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemTariffComponentUpdateHistory AddNew()
		{
			ItemTariffComponentUpdateHistory entity = base.AddNewEntity() as ItemTariffComponentUpdateHistory;
			
			return entity;
		}

		public ItemTariffComponentUpdateHistory FindByPrimaryKey(System.String requestNo, System.String sRTariffType, System.String itemID, System.String classID, System.DateTime startingDate, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(requestNo, sRTariffType, itemID, classID, startingDate, tariffComponentID) as ItemTariffComponentUpdateHistory;
		}


		#region IEnumerable<ItemTariffComponentUpdateHistory> Members

		IEnumerator<ItemTariffComponentUpdateHistory> IEnumerable<ItemTariffComponentUpdateHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariffComponentUpdateHistory;
			}
		}

		#endregion
		
		private ItemTariffComponentUpdateHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariffComponentUpdateHistory' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemTariffComponentUpdateHistory ({RequestNo},{SRTariffType},{ItemID},{ClassID},{StartingDate},{TariffComponentID})")]
	[Serializable]
	public partial class ItemTariffComponentUpdateHistory : esItemTariffComponentUpdateHistory
	{
		public ItemTariffComponentUpdateHistory()
		{

		}
	
		public ItemTariffComponentUpdateHistory(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffComponentUpdateHistoryMetadata.Meta();
			}
		}
		
		
		
		override protected esItemTariffComponentUpdateHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffComponentUpdateHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemTariffComponentUpdateHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffComponentUpdateHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemTariffComponentUpdateHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemTariffComponentUpdateHistoryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemTariffComponentUpdateHistoryQuery : esItemTariffComponentUpdateHistoryQuery
	{
		public ItemTariffComponentUpdateHistoryQuery()
		{

		}		
		
		public ItemTariffComponentUpdateHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemTariffComponentUpdateHistoryQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemTariffComponentUpdateHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffComponentUpdateHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.RequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.RequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.SRTariffType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.SRTariffType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ClassID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.StartingDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.StartingDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.TariffComponentID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.Price, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToPrice, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.ToPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.IsAllowDiscount, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.IsAllowDiscount;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToIsAllowDiscount, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.ToIsAllowDiscount;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.IsAllowVariable, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.IsAllowVariable;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.ToIsAllowVariable, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.ToIsAllowVariable;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffComponentUpdateHistoryMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffComponentUpdateHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemTariffComponentUpdateHistoryMetadata Meta()
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
			 public const string RequestNo = "RequestNo";
			 public const string SRTariffType = "SRTariffType";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string StartingDate = "StartingDate";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string ToPrice = "ToPrice";
			 public const string IsAllowDiscount = "IsAllowDiscount";
			 public const string ToIsAllowDiscount = "ToIsAllowDiscount";
			 public const string IsAllowVariable = "IsAllowVariable";
			 public const string ToIsAllowVariable = "ToIsAllowVariable";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RequestNo = "RequestNo";
			 public const string SRTariffType = "SRTariffType";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string StartingDate = "StartingDate";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string ToPrice = "ToPrice";
			 public const string IsAllowDiscount = "IsAllowDiscount";
			 public const string ToIsAllowDiscount = "ToIsAllowDiscount";
			 public const string IsAllowVariable = "IsAllowVariable";
			 public const string ToIsAllowVariable = "ToIsAllowVariable";
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
			lock (typeof(ItemTariffComponentUpdateHistoryMetadata))
			{
				if(ItemTariffComponentUpdateHistoryMetadata.mapDelegates == null)
				{
					ItemTariffComponentUpdateHistoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemTariffComponentUpdateHistoryMetadata.meta == null)
				{
					ItemTariffComponentUpdateHistoryMetadata.meta = new ItemTariffComponentUpdateHistoryMetadata();
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
				

				meta.AddTypeMap("RequestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRTariffType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartingDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ToPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ToIsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ToIsAllowVariable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemTariffComponentUpdateHistory";
				meta.Destination = "ItemTariffComponentUpdateHistory";
				
				meta.spInsert = "proc_ItemTariffComponentUpdateHistoryInsert";				
				meta.spUpdate = "proc_ItemTariffComponentUpdateHistoryUpdate";		
				meta.spDelete = "proc_ItemTariffComponentUpdateHistoryDelete";
				meta.spLoadAll = "proc_ItemTariffComponentUpdateHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffComponentUpdateHistoryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffComponentUpdateHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
