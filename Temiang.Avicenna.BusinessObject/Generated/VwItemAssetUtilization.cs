/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/20/2013 11:42:05 AM
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
	abstract public class esVwItemAssetUtilizationCollection : esEntityCollectionWAuditLog
	{
		public esVwItemAssetUtilizationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwItemAssetUtilizationCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwItemAssetUtilizationQuery query)
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
			this.InitQuery(query as esVwItemAssetUtilizationQuery);
		}
		#endregion
		
		virtual public VwItemAssetUtilization DetachEntity(VwItemAssetUtilization entity)
		{
			return base.DetachEntity(entity) as VwItemAssetUtilization;
		}
		
		virtual public VwItemAssetUtilization AttachEntity(VwItemAssetUtilization entity)
		{
			return base.AttachEntity(entity) as VwItemAssetUtilization;
		}
		
		virtual public void Combine(VwItemAssetUtilizationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwItemAssetUtilization this[int index]
		{
			get
			{
				return base[index] as VwItemAssetUtilization;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwItemAssetUtilization);
		}
	}



	[Serializable]
	abstract public class esVwItemAssetUtilization : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwItemAssetUtilizationQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwItemAssetUtilization()
		{

		}

		public esVwItemAssetUtilization(DataRow row)
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
		/// Maps to vw_ItemAssetUtilization.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(VwItemAssetUtilizationMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(VwItemAssetUtilizationMetadata.ColumnNames.ItemID, value);
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
			public esStrings(esVwItemAssetUtilization entity)
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
			

			private esVwItemAssetUtilization entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwItemAssetUtilizationQuery query)
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
				throw new Exception("esVwItemAssetUtilization can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwItemAssetUtilizationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwItemAssetUtilizationMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, VwItemAssetUtilizationMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwItemAssetUtilizationCollection")]
	public partial class VwItemAssetUtilizationCollection : esVwItemAssetUtilizationCollection, IEnumerable<VwItemAssetUtilization>
	{
		public VwItemAssetUtilizationCollection()
		{

		}
		
		public static implicit operator List<VwItemAssetUtilization>(VwItemAssetUtilizationCollection coll)
		{
			List<VwItemAssetUtilization> list = new List<VwItemAssetUtilization>();
			
			foreach (VwItemAssetUtilization emp in coll)
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
				return  VwItemAssetUtilizationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemAssetUtilizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwItemAssetUtilization(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwItemAssetUtilization();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwItemAssetUtilizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemAssetUtilizationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwItemAssetUtilizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwItemAssetUtilization AddNew()
		{
			VwItemAssetUtilization entity = base.AddNewEntity() as VwItemAssetUtilization;
			
			return entity;
		}


		#region IEnumerable<VwItemAssetUtilization> Members

		IEnumerator<VwItemAssetUtilization> IEnumerable<VwItemAssetUtilization>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwItemAssetUtilization;
			}
		}

		#endregion
		
		private VwItemAssetUtilizationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_ItemAssetUtilization' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwItemAssetUtilization ()")]
	[Serializable]
	public partial class VwItemAssetUtilization : esVwItemAssetUtilization
	{
		public VwItemAssetUtilization()
		{

		}
	
		public VwItemAssetUtilization(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwItemAssetUtilizationMetadata.Meta();
			}
		}
		
		
		
		override protected esVwItemAssetUtilizationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemAssetUtilizationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwItemAssetUtilizationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemAssetUtilizationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwItemAssetUtilizationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwItemAssetUtilizationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwItemAssetUtilizationQuery : esVwItemAssetUtilizationQuery
	{
		public VwItemAssetUtilizationQuery()
		{

		}		
		
		public VwItemAssetUtilizationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwItemAssetUtilizationQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwItemAssetUtilizationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwItemAssetUtilizationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwItemAssetUtilizationMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemAssetUtilizationMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwItemAssetUtilizationMetadata Meta()
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
			lock (typeof(VwItemAssetUtilizationMetadata))
			{
				if(VwItemAssetUtilizationMetadata.mapDelegates == null)
				{
					VwItemAssetUtilizationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwItemAssetUtilizationMetadata.meta == null)
				{
					VwItemAssetUtilizationMetadata.meta = new VwItemAssetUtilizationMetadata();
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
				
				
				
				meta.Source = "vw_ItemAssetUtilization";
				meta.Destination = "vw_ItemAssetUtilization";
				
				meta.spInsert = "proc_vw_ItemAssetUtilizationInsert";				
				meta.spUpdate = "proc_vw_ItemAssetUtilizationUpdate";		
				meta.spDelete = "proc_vw_ItemAssetUtilizationDelete";
				meta.spLoadAll = "proc_vw_ItemAssetUtilizationLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ItemAssetUtilizationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwItemAssetUtilizationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
