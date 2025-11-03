/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/22/2015 4:30:30 PM
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
	abstract public class esVwItemsAlreadyUsedCollection : esEntityCollectionWAuditLog
	{
		public esVwItemsAlreadyUsedCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwItemsAlreadyUsedCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwItemsAlreadyUsedQuery query)
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
			this.InitQuery(query as esVwItemsAlreadyUsedQuery);
		}
		#endregion
		
		virtual public VwItemsAlreadyUsed DetachEntity(VwItemsAlreadyUsed entity)
		{
			return base.DetachEntity(entity) as VwItemsAlreadyUsed;
		}
		
		virtual public VwItemsAlreadyUsed AttachEntity(VwItemsAlreadyUsed entity)
		{
			return base.AttachEntity(entity) as VwItemsAlreadyUsed;
		}
		
		virtual public void Combine(VwItemsAlreadyUsedCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwItemsAlreadyUsed this[int index]
		{
			get
			{
				return base[index] as VwItemsAlreadyUsed;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwItemsAlreadyUsed);
		}
	}



	[Serializable]
	abstract public class esVwItemsAlreadyUsed : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwItemsAlreadyUsedQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwItemsAlreadyUsed()
		{

		}

		public esVwItemsAlreadyUsed(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		
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
						case "ItemID": this.str.ItemID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{

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
		/// Maps to vw_ItemsAlreadyUsed.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(VwItemsAlreadyUsedMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(VwItemsAlreadyUsedMetadata.ColumnNames.ItemID, value);
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
			public esStrings(esVwItemsAlreadyUsed entity)
			{
				this.entity = entity;
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
			

			private esVwItemsAlreadyUsed entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwItemsAlreadyUsedQuery query)
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
				throw new Exception("esVwItemsAlreadyUsed can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwItemsAlreadyUsedQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwItemsAlreadyUsedMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, VwItemsAlreadyUsedMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwItemsAlreadyUsedCollection")]
	public partial class VwItemsAlreadyUsedCollection : esVwItemsAlreadyUsedCollection, IEnumerable<VwItemsAlreadyUsed>
	{
		public VwItemsAlreadyUsedCollection()
		{

		}
		
		public static implicit operator List<VwItemsAlreadyUsed>(VwItemsAlreadyUsedCollection coll)
		{
			List<VwItemsAlreadyUsed> list = new List<VwItemsAlreadyUsed>();
			
			foreach (VwItemsAlreadyUsed emp in coll)
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
				return  VwItemsAlreadyUsedMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemsAlreadyUsedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwItemsAlreadyUsed(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwItemsAlreadyUsed();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwItemsAlreadyUsedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemsAlreadyUsedQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwItemsAlreadyUsedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwItemsAlreadyUsed AddNew()
		{
			VwItemsAlreadyUsed entity = base.AddNewEntity() as VwItemsAlreadyUsed;
			
			return entity;
		}


		#region IEnumerable<VwItemsAlreadyUsed> Members

		IEnumerator<VwItemsAlreadyUsed> IEnumerable<VwItemsAlreadyUsed>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwItemsAlreadyUsed;
			}
		}

		#endregion
		
		private VwItemsAlreadyUsedQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_ItemsAlreadyUsed' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwItemsAlreadyUsed ()")]
	[Serializable]
	public partial class VwItemsAlreadyUsed : esVwItemsAlreadyUsed
	{
		public VwItemsAlreadyUsed()
		{

		}
	
		public VwItemsAlreadyUsed(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwItemsAlreadyUsedMetadata.Meta();
			}
		}
		
		
		
		override protected esVwItemsAlreadyUsedQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemsAlreadyUsedQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwItemsAlreadyUsedQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemsAlreadyUsedQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwItemsAlreadyUsedQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwItemsAlreadyUsedQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwItemsAlreadyUsedQuery : esVwItemsAlreadyUsedQuery
	{
		public VwItemsAlreadyUsedQuery()
		{

		}		
		
		public VwItemsAlreadyUsedQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwItemsAlreadyUsedQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwItemsAlreadyUsedMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwItemsAlreadyUsedMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwItemsAlreadyUsedMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemsAlreadyUsedMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwItemsAlreadyUsedMetadata Meta()
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
			 public const string ItemID = "ItemID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
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
			lock (typeof(VwItemsAlreadyUsedMetadata))
			{
				if(VwItemsAlreadyUsedMetadata.mapDelegates == null)
				{
					VwItemsAlreadyUsedMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwItemsAlreadyUsedMetadata.meta == null)
				{
					VwItemsAlreadyUsedMetadata.meta = new VwItemsAlreadyUsedMetadata();
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
				

				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "vw_ItemsAlreadyUsed";
				meta.Destination = "vw_ItemsAlreadyUsed";
				
				meta.spInsert = "proc_vw_ItemsAlreadyUsedInsert";				
				meta.spUpdate = "proc_vw_ItemsAlreadyUsedUpdate";		
				meta.spDelete = "proc_vw_ItemsAlreadyUsedDelete";
				meta.spLoadAll = "proc_vw_ItemsAlreadyUsedLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ItemsAlreadyUsedLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwItemsAlreadyUsedMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
