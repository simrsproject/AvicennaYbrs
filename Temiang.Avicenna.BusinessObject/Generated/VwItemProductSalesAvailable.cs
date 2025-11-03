/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/5/2013 1:35:45 AM
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
	abstract public class esVwItemProductSalesAvailableCollection : esEntityCollectionWAuditLog
	{
		public esVwItemProductSalesAvailableCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwItemProductSalesAvailableCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwItemProductSalesAvailableQuery query)
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
			this.InitQuery(query as esVwItemProductSalesAvailableQuery);
		}
		#endregion
		
		virtual public VwItemProductSalesAvailable DetachEntity(VwItemProductSalesAvailable entity)
		{
			return base.DetachEntity(entity) as VwItemProductSalesAvailable;
		}
		
		virtual public VwItemProductSalesAvailable AttachEntity(VwItemProductSalesAvailable entity)
		{
			return base.AttachEntity(entity) as VwItemProductSalesAvailable;
		}
		
		virtual public void Combine(VwItemProductSalesAvailableCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwItemProductSalesAvailable this[int index]
		{
			get
			{
				return base[index] as VwItemProductSalesAvailable;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwItemProductSalesAvailable);
		}
	}



	[Serializable]
	abstract public class esVwItemProductSalesAvailable : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwItemProductSalesAvailableQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwItemProductSalesAvailable()
		{

		}

		public esVwItemProductSalesAvailable(DataRow row)
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
						case "IsSalesAvailable": this.str.IsSalesAvailable = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsSalesAvailable":
						
							if (value == null || value is System.Boolean)
								this.IsSalesAvailable = (System.Boolean?)value;
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
		/// Maps to vw_ItemProductSalesAvailable.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(VwItemProductSalesAvailableMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(VwItemProductSalesAvailableMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ItemProductSalesAvailable.IsSalesAvailable
		/// </summary>
		virtual public System.Boolean? IsSalesAvailable
		{
			get
			{
				return base.GetSystemBoolean(VwItemProductSalesAvailableMetadata.ColumnNames.IsSalesAvailable);
			}
			
			set
			{
				base.SetSystemBoolean(VwItemProductSalesAvailableMetadata.ColumnNames.IsSalesAvailable, value);
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
			public esStrings(esVwItemProductSalesAvailable entity)
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
				
			public System.String IsSalesAvailable
			{
				get
				{
					System.Boolean? data = entity.IsSalesAvailable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSalesAvailable = null;
					else entity.IsSalesAvailable = Convert.ToBoolean(value);
				}
			}
			

			private esVwItemProductSalesAvailable entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwItemProductSalesAvailableQuery query)
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
				throw new Exception("esVwItemProductSalesAvailable can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwItemProductSalesAvailableQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwItemProductSalesAvailableMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, VwItemProductSalesAvailableMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsSalesAvailable
		{
			get
			{
				return new esQueryItem(this, VwItemProductSalesAvailableMetadata.ColumnNames.IsSalesAvailable, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwItemProductSalesAvailableCollection")]
	public partial class VwItemProductSalesAvailableCollection : esVwItemProductSalesAvailableCollection, IEnumerable<VwItemProductSalesAvailable>
	{
		public VwItemProductSalesAvailableCollection()
		{

		}
		
		public static implicit operator List<VwItemProductSalesAvailable>(VwItemProductSalesAvailableCollection coll)
		{
			List<VwItemProductSalesAvailable> list = new List<VwItemProductSalesAvailable>();
			
			foreach (VwItemProductSalesAvailable emp in coll)
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
				return  VwItemProductSalesAvailableMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemProductSalesAvailableQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwItemProductSalesAvailable(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwItemProductSalesAvailable();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwItemProductSalesAvailableQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemProductSalesAvailableQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwItemProductSalesAvailableQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwItemProductSalesAvailable AddNew()
		{
			VwItemProductSalesAvailable entity = base.AddNewEntity() as VwItemProductSalesAvailable;
			
			return entity;
		}


		#region IEnumerable<VwItemProductSalesAvailable> Members

		IEnumerator<VwItemProductSalesAvailable> IEnumerable<VwItemProductSalesAvailable>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwItemProductSalesAvailable;
			}
		}

		#endregion
		
		private VwItemProductSalesAvailableQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_ItemProductSalesAvailable' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwItemProductSalesAvailable ()")]
	[Serializable]
	public partial class VwItemProductSalesAvailable : esVwItemProductSalesAvailable
	{
		public VwItemProductSalesAvailable()
		{

		}
	
		public VwItemProductSalesAvailable(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwItemProductSalesAvailableMetadata.Meta();
			}
		}
		
		
		
		override protected esVwItemProductSalesAvailableQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemProductSalesAvailableQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwItemProductSalesAvailableQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemProductSalesAvailableQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwItemProductSalesAvailableQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwItemProductSalesAvailableQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwItemProductSalesAvailableQuery : esVwItemProductSalesAvailableQuery
	{
		public VwItemProductSalesAvailableQuery()
		{

		}		
		
		public VwItemProductSalesAvailableQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwItemProductSalesAvailableQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwItemProductSalesAvailableMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwItemProductSalesAvailableMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwItemProductSalesAvailableMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemProductSalesAvailableMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwItemProductSalesAvailableMetadata.ColumnNames.IsSalesAvailable, 1, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = VwItemProductSalesAvailableMetadata.PropertyNames.IsSalesAvailable;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwItemProductSalesAvailableMetadata Meta()
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
			 public const string IsSalesAvailable = "IsSalesAvailable";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string IsSalesAvailable = "IsSalesAvailable";
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
			lock (typeof(VwItemProductSalesAvailableMetadata))
			{
				if(VwItemProductSalesAvailableMetadata.mapDelegates == null)
				{
					VwItemProductSalesAvailableMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwItemProductSalesAvailableMetadata.meta == null)
				{
					VwItemProductSalesAvailableMetadata.meta = new VwItemProductSalesAvailableMetadata();
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
				meta.AddTypeMap("IsSalesAvailable", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "vw_ItemProductSalesAvailable";
				meta.Destination = "vw_ItemProductSalesAvailable";
				
				meta.spInsert = "proc_vw_ItemProductSalesAvailableInsert";				
				meta.spUpdate = "proc_vw_ItemProductSalesAvailableUpdate";		
				meta.spDelete = "proc_vw_ItemProductSalesAvailableDelete";
				meta.spLoadAll = "proc_vw_ItemProductSalesAvailableLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ItemProductSalesAvailableLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwItemProductSalesAvailableMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
