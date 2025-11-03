/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/27/2013 12:18:40 PM
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
	abstract public class esItemTariffRequest2ItemCompCollection : esEntityCollectionWAuditLog
	{
		public esItemTariffRequest2ItemCompCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemTariffRequest2ItemCompCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffRequest2ItemCompQuery query)
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
			this.InitQuery(query as esItemTariffRequest2ItemCompQuery);
		}
		#endregion
		
		virtual public ItemTariffRequest2ItemComp DetachEntity(ItemTariffRequest2ItemComp entity)
		{
			return base.DetachEntity(entity) as ItemTariffRequest2ItemComp;
		}
		
		virtual public ItemTariffRequest2ItemComp AttachEntity(ItemTariffRequest2ItemComp entity)
		{
			return base.AttachEntity(entity) as ItemTariffRequest2ItemComp;
		}
		
		virtual public void Combine(ItemTariffRequest2ItemCompCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemTariffRequest2ItemComp this[int index]
		{
			get
			{
				return base[index] as ItemTariffRequest2ItemComp;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariffRequest2ItemComp);
		}
	}



	[Serializable]
	abstract public class esItemTariffRequest2ItemComp : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffRequest2ItemCompQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTariffRequest2ItemComp()
		{

		}

		public esItemTariffRequest2ItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String tariffRequestNo, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID, classID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID, classID, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String tariffRequestNo, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID, classID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID, classID, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String tariffRequestNo, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			esItemTariffRequest2ItemCompQuery query = this.GetDynamicQuery();
			query.Where(query.TariffRequestNo == tariffRequestNo, query.ItemID == itemID, query.ClassID == classID, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String tariffRequestNo, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("TariffRequestNo",tariffRequestNo);			parms.Add("ItemID",itemID);			parms.Add("ClassID",classID);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "TariffRequestNo": this.str.TariffRequestNo = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;							
						case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "IsAllowDiscount":
						
							if (value == null || value is System.Boolean)
								this.IsAllowDiscount = (System.Boolean?)value;
							break;
						
						case "IsAllowVariable":
						
							if (value == null || value is System.Boolean)
								this.IsAllowVariable = (System.Boolean?)value;
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
		/// Maps to ItemTariffRequest2ItemComp.TariffRequestNo
		/// </summary>
		virtual public System.String TariffRequestNo
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.TariffRequestNo);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.TariffRequestNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest2ItemComp.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest2ItemComp.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest2ItemComp.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest2ItemComp.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffRequest2ItemCompMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTariffRequest2ItemCompMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Bisa di set True bila di master itemnya IsAllowDiscount=1
		/// </summary>
		virtual public System.Boolean? IsAllowDiscount
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequest2ItemCompMetadata.ColumnNames.IsAllowDiscount);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffRequest2ItemCompMetadata.ColumnNames.IsAllowDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest2ItemComp.IsAllowVariable
		/// </summary>
		virtual public System.Boolean? IsAllowVariable
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequest2ItemCompMetadata.ColumnNames.IsAllowVariable);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffRequest2ItemCompMetadata.ColumnNames.IsAllowVariable, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest2ItemComp.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequest2ItemCompMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffRequest2ItemCompMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequest2ItemComp.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequest2ItemCompMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemTariffRequest2ItemComp entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TariffRequestNo
			{
				get
				{
					System.String data = entity.TariffRequestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffRequestNo = null;
					else entity.TariffRequestNo = Convert.ToString(value);
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
			

			private esItemTariffRequest2ItemComp entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffRequest2ItemCompQuery query)
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
				throw new Exception("esItemTariffRequest2ItemComp can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemTariffRequest2ItemComp : esItemTariffRequest2ItemComp
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
	abstract public class esItemTariffRequest2ItemCompQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequest2ItemCompMetadata.Meta();
			}
		}	
		

		public esQueryItem TariffRequestNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.TariffRequestNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsAllowDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAllowVariable
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequest2ItemCompMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffRequest2ItemCompCollection")]
	public partial class ItemTariffRequest2ItemCompCollection : esItemTariffRequest2ItemCompCollection, IEnumerable<ItemTariffRequest2ItemComp>
	{
		public ItemTariffRequest2ItemCompCollection()
		{

		}
		
		public static implicit operator List<ItemTariffRequest2ItemComp>(ItemTariffRequest2ItemCompCollection coll)
		{
			List<ItemTariffRequest2ItemComp> list = new List<ItemTariffRequest2ItemComp>();
			
			foreach (ItemTariffRequest2ItemComp emp in coll)
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
				return  ItemTariffRequest2ItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequest2ItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariffRequest2ItemComp(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariffRequest2ItemComp();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemTariffRequest2ItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequest2ItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemTariffRequest2ItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemTariffRequest2ItemComp AddNew()
		{
			ItemTariffRequest2ItemComp entity = base.AddNewEntity() as ItemTariffRequest2ItemComp;
			
			return entity;
		}

		public ItemTariffRequest2ItemComp FindByPrimaryKey(System.String tariffRequestNo, System.String itemID, System.String classID, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(tariffRequestNo, itemID, classID, tariffComponentID) as ItemTariffRequest2ItemComp;
		}


		#region IEnumerable<ItemTariffRequest2ItemComp> Members

		IEnumerator<ItemTariffRequest2ItemComp> IEnumerable<ItemTariffRequest2ItemComp>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariffRequest2ItemComp;
			}
		}

		#endregion
		
		private ItemTariffRequest2ItemCompQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariffRequest2ItemComp' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemTariffRequest2ItemComp ({TariffRequestNo},{ItemID},{ClassID},{TariffComponentID})")]
	[Serializable]
	public partial class ItemTariffRequest2ItemComp : esItemTariffRequest2ItemComp
	{
		public ItemTariffRequest2ItemComp()
		{

		}
	
		public ItemTariffRequest2ItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequest2ItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esItemTariffRequest2ItemCompQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequest2ItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemTariffRequest2ItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequest2ItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemTariffRequest2ItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemTariffRequest2ItemCompQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemTariffRequest2ItemCompQuery : esItemTariffRequest2ItemCompQuery
	{
		public ItemTariffRequest2ItemCompQuery()
		{

		}		
		
		public ItemTariffRequest2ItemCompQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemTariffRequest2ItemCompQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemTariffRequest2ItemCompMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffRequest2ItemCompMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.TariffRequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.ClassID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.TariffComponentID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.Price, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.IsAllowDiscount, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.IsAllowDiscount;
			c.Description = "Bisa di set True bila di master itemnya IsAllowDiscount=1";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.IsAllowVariable, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.IsAllowVariable;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequest2ItemCompMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequest2ItemCompMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemTariffRequest2ItemCompMetadata Meta()
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
			 public const string TariffRequestNo = "TariffRequestNo";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string IsAllowDiscount = "IsAllowDiscount";
			 public const string IsAllowVariable = "IsAllowVariable";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TariffRequestNo = "TariffRequestNo";
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string IsAllowDiscount = "IsAllowDiscount";
			 public const string IsAllowVariable = "IsAllowVariable";
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
			lock (typeof(ItemTariffRequest2ItemCompMetadata))
			{
				if(ItemTariffRequest2ItemCompMetadata.mapDelegates == null)
				{
					ItemTariffRequest2ItemCompMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemTariffRequest2ItemCompMetadata.meta == null)
				{
					ItemTariffRequest2ItemCompMetadata.meta = new ItemTariffRequest2ItemCompMetadata();
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
				

				meta.AddTypeMap("TariffRequestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemTariffRequest2ItemComp";
				meta.Destination = "ItemTariffRequest2ItemComp";
				
				meta.spInsert = "proc_ItemTariffRequest2ItemCompInsert";				
				meta.spUpdate = "proc_ItemTariffRequest2ItemCompUpdate";		
				meta.spDelete = "proc_ItemTariffRequest2ItemCompDelete";
				meta.spLoadAll = "proc_ItemTariffRequest2ItemCompLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffRequest2ItemCompLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffRequest2ItemCompMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
