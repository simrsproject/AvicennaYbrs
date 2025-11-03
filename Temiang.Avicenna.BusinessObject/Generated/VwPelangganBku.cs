/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/29/2021 12:08:21 PM
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
	abstract public class esVwPelangganBkuCollection : esEntityCollectionWAuditLog
	{
		public esVwPelangganBkuCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwPelangganBkuCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwPelangganBkuQuery query)
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
			this.InitQuery(query as esVwPelangganBkuQuery);
		}
		#endregion
		
		virtual public VwPelangganBku DetachEntity(VwPelangganBku entity)
		{
			return base.DetachEntity(entity) as VwPelangganBku;
		}
		
		virtual public VwPelangganBku AttachEntity(VwPelangganBku entity)
		{
			return base.AttachEntity(entity) as VwPelangganBku;
		}
		
		virtual public void Combine(VwPelangganBkuCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwPelangganBku this[int index]
		{
			get
			{
				return base[index] as VwPelangganBku;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwPelangganBku);
		}
	}



	[Serializable]
	abstract public class esVwPelangganBku : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwPelangganBkuQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwPelangganBku()
		{

		}

		public esVwPelangganBku(DataRow row)
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
						case "Id": this.str.Id = (string)value; break;							
						case "Name": this.str.Name = (string)value; break;							
						case "Type": this.str.Type = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Type":
						
							if (value == null || value is System.Int32)
								this.Type = (System.Int32?)value;
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
		/// Maps to vw_PelangganBku.Id
		/// </summary>
		virtual public System.String Id
		{
			get
			{
				return base.GetSystemString(VwPelangganBkuMetadata.ColumnNames.Id);
			}
			
			set
			{
				base.SetSystemString(VwPelangganBkuMetadata.ColumnNames.Id, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_PelangganBku.Name
		/// </summary>
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(VwPelangganBkuMetadata.ColumnNames.Name);
			}
			
			set
			{
				base.SetSystemString(VwPelangganBkuMetadata.ColumnNames.Name, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_PelangganBku.Type
		/// </summary>
		virtual public System.Int32? Type
		{
			get
			{
				return base.GetSystemInt32(VwPelangganBkuMetadata.ColumnNames.Type);
			}
			
			set
			{
				base.SetSystemInt32(VwPelangganBkuMetadata.ColumnNames.Type, value);
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
			public esStrings(esVwPelangganBku entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.String data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToString(value);
				}
			}
				
			public System.String Name
			{
				get
				{
					System.String data = entity.Name;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Name = null;
					else entity.Name = Convert.ToString(value);
				}
			}
				
			public System.String Type
			{
				get
				{
					System.Int32? data = entity.Type;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Type = null;
					else entity.Type = Convert.ToInt32(value);
				}
			}
			

			private esVwPelangganBku entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwPelangganBkuQuery query)
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
				throw new Exception("esVwPelangganBku can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwPelangganBkuQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwPelangganBkuMetadata.Meta();
			}
		}	
		

		public esQueryItem Id
		{
			get
			{
				return new esQueryItem(this, VwPelangganBkuMetadata.ColumnNames.Id, esSystemType.String);
			}
		} 
		
		public esQueryItem Name
		{
			get
			{
				return new esQueryItem(this, VwPelangganBkuMetadata.ColumnNames.Name, esSystemType.String);
			}
		} 
		
		public esQueryItem Type
		{
			get
			{
				return new esQueryItem(this, VwPelangganBkuMetadata.ColumnNames.Type, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwPelangganBkuCollection")]
	public partial class VwPelangganBkuCollection : esVwPelangganBkuCollection, IEnumerable<VwPelangganBku>
	{
		public VwPelangganBkuCollection()
		{

		}
		
		public static implicit operator List<VwPelangganBku>(VwPelangganBkuCollection coll)
		{
			List<VwPelangganBku> list = new List<VwPelangganBku>();
			
			foreach (VwPelangganBku emp in coll)
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
				return  VwPelangganBkuMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwPelangganBkuQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwPelangganBku(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwPelangganBku();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwPelangganBkuQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwPelangganBkuQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwPelangganBkuQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwPelangganBku AddNew()
		{
			VwPelangganBku entity = base.AddNewEntity() as VwPelangganBku;
			
			return entity;
		}


		#region IEnumerable<VwPelangganBku> Members

		IEnumerator<VwPelangganBku> IEnumerable<VwPelangganBku>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwPelangganBku;
			}
		}

		#endregion
		
		private VwPelangganBkuQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_PelangganBku' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwPelangganBku ()")]
	[Serializable]
	public partial class VwPelangganBku : esVwPelangganBku
	{
		public VwPelangganBku()
		{

		}
	
		public VwPelangganBku(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwPelangganBkuMetadata.Meta();
			}
		}
		
		
		
		override protected esVwPelangganBkuQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwPelangganBkuQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwPelangganBkuQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwPelangganBkuQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwPelangganBkuQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwPelangganBkuQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwPelangganBkuQuery : esVwPelangganBkuQuery
	{
		public VwPelangganBkuQuery()
		{

		}		
		
		public VwPelangganBkuQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwPelangganBkuQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwPelangganBkuMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwPelangganBkuMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwPelangganBkuMetadata.ColumnNames.Id, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwPelangganBkuMetadata.PropertyNames.Id;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwPelangganBkuMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwPelangganBkuMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwPelangganBkuMetadata.ColumnNames.Type, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwPelangganBkuMetadata.PropertyNames.Type;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwPelangganBkuMetadata Meta()
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
			 public const string Id = "Id";
			 public const string Name = "Name";
			 public const string Type = "Type";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Name = "Name";
			 public const string Type = "Type";
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
			lock (typeof(VwPelangganBkuMetadata))
			{
				if(VwPelangganBkuMetadata.mapDelegates == null)
				{
					VwPelangganBkuMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwPelangganBkuMetadata.meta == null)
				{
					VwPelangganBkuMetadata.meta = new VwPelangganBkuMetadata();
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
				

				meta.AddTypeMap("Id", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Name", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Type", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "vw_PelangganBku";
				meta.Destination = "vw_PelangganBku";
				
				meta.spInsert = "proc_vw_PelangganBkuInsert";				
				meta.spUpdate = "proc_vw_PelangganBkuUpdate";		
				meta.spDelete = "proc_vw_PelangganBkuDelete";
				meta.spLoadAll = "proc_vw_PelangganBkuLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_PelangganBkuLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwPelangganBkuMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
