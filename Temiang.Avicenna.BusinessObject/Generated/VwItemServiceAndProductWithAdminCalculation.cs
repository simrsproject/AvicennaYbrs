/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/8/2014 2:39:12 PM
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
	abstract public class esVwItemServiceAndProductWithAdminCalculationCollection : esEntityCollectionWAuditLog
	{
		public esVwItemServiceAndProductWithAdminCalculationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwItemServiceAndProductWithAdminCalculationCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwItemServiceAndProductWithAdminCalculationQuery query)
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
			this.InitQuery(query as esVwItemServiceAndProductWithAdminCalculationQuery);
		}
		#endregion
		
		virtual public VwItemServiceAndProductWithAdminCalculation DetachEntity(VwItemServiceAndProductWithAdminCalculation entity)
		{
			return base.DetachEntity(entity) as VwItemServiceAndProductWithAdminCalculation;
		}
		
		virtual public VwItemServiceAndProductWithAdminCalculation AttachEntity(VwItemServiceAndProductWithAdminCalculation entity)
		{
			return base.AttachEntity(entity) as VwItemServiceAndProductWithAdminCalculation;
		}
		
		virtual public void Combine(VwItemServiceAndProductWithAdminCalculationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwItemServiceAndProductWithAdminCalculation this[int index]
		{
			get
			{
				return base[index] as VwItemServiceAndProductWithAdminCalculation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwItemServiceAndProductWithAdminCalculation);
		}
	}



	[Serializable]
	abstract public class esVwItemServiceAndProductWithAdminCalculation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwItemServiceAndProductWithAdminCalculationQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwItemServiceAndProductWithAdminCalculation()
		{

		}

		public esVwItemServiceAndProductWithAdminCalculation(DataRow row)
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
		/// Maps to vw_ItemServiceAndProductWithAdminCalculation.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(VwItemServiceAndProductWithAdminCalculationMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(VwItemServiceAndProductWithAdminCalculationMetadata.ColumnNames.ItemID, value);
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
			public esStrings(esVwItemServiceAndProductWithAdminCalculation entity)
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
			

			private esVwItemServiceAndProductWithAdminCalculation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwItemServiceAndProductWithAdminCalculationQuery query)
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
				throw new Exception("esVwItemServiceAndProductWithAdminCalculation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwItemServiceAndProductWithAdminCalculationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwItemServiceAndProductWithAdminCalculationMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, VwItemServiceAndProductWithAdminCalculationMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwItemServiceAndProductWithAdminCalculationCollection")]
	public partial class VwItemServiceAndProductWithAdminCalculationCollection : esVwItemServiceAndProductWithAdminCalculationCollection, IEnumerable<VwItemServiceAndProductWithAdminCalculation>
	{
		public VwItemServiceAndProductWithAdminCalculationCollection()
		{

		}
		
		public static implicit operator List<VwItemServiceAndProductWithAdminCalculation>(VwItemServiceAndProductWithAdminCalculationCollection coll)
		{
			List<VwItemServiceAndProductWithAdminCalculation> list = new List<VwItemServiceAndProductWithAdminCalculation>();
			
			foreach (VwItemServiceAndProductWithAdminCalculation emp in coll)
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
				return  VwItemServiceAndProductWithAdminCalculationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemServiceAndProductWithAdminCalculationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwItemServiceAndProductWithAdminCalculation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwItemServiceAndProductWithAdminCalculation();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwItemServiceAndProductWithAdminCalculationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemServiceAndProductWithAdminCalculationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwItemServiceAndProductWithAdminCalculationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwItemServiceAndProductWithAdminCalculation AddNew()
		{
			VwItemServiceAndProductWithAdminCalculation entity = base.AddNewEntity() as VwItemServiceAndProductWithAdminCalculation;
			
			return entity;
		}


		#region IEnumerable<VwItemServiceAndProductWithAdminCalculation> Members

		IEnumerator<VwItemServiceAndProductWithAdminCalculation> IEnumerable<VwItemServiceAndProductWithAdminCalculation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwItemServiceAndProductWithAdminCalculation;
			}
		}

		#endregion
		
		private VwItemServiceAndProductWithAdminCalculationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_ItemServiceAndProductWithAdminCalculation' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwItemServiceAndProductWithAdminCalculation ()")]
	[Serializable]
	public partial class VwItemServiceAndProductWithAdminCalculation : esVwItemServiceAndProductWithAdminCalculation
	{
		public VwItemServiceAndProductWithAdminCalculation()
		{

		}
	
		public VwItemServiceAndProductWithAdminCalculation(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwItemServiceAndProductWithAdminCalculationMetadata.Meta();
			}
		}
		
		
		
		override protected esVwItemServiceAndProductWithAdminCalculationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemServiceAndProductWithAdminCalculationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwItemServiceAndProductWithAdminCalculationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemServiceAndProductWithAdminCalculationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwItemServiceAndProductWithAdminCalculationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwItemServiceAndProductWithAdminCalculationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwItemServiceAndProductWithAdminCalculationQuery : esVwItemServiceAndProductWithAdminCalculationQuery
	{
		public VwItemServiceAndProductWithAdminCalculationQuery()
		{

		}		
		
		public VwItemServiceAndProductWithAdminCalculationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwItemServiceAndProductWithAdminCalculationQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwItemServiceAndProductWithAdminCalculationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwItemServiceAndProductWithAdminCalculationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwItemServiceAndProductWithAdminCalculationMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemServiceAndProductWithAdminCalculationMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwItemServiceAndProductWithAdminCalculationMetadata Meta()
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
			lock (typeof(VwItemServiceAndProductWithAdminCalculationMetadata))
			{
				if(VwItemServiceAndProductWithAdminCalculationMetadata.mapDelegates == null)
				{
					VwItemServiceAndProductWithAdminCalculationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwItemServiceAndProductWithAdminCalculationMetadata.meta == null)
				{
					VwItemServiceAndProductWithAdminCalculationMetadata.meta = new VwItemServiceAndProductWithAdminCalculationMetadata();
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
				
				
				
				meta.Source = "vw_ItemServiceAndProductWithAdminCalculation";
				meta.Destination = "vw_ItemServiceAndProductWithAdminCalculation";
				
				meta.spInsert = "proc_vw_ItemServiceAndProductWithAdminCalculationInsert";				
				meta.spUpdate = "proc_vw_ItemServiceAndProductWithAdminCalculationUpdate";		
				meta.spDelete = "proc_vw_ItemServiceAndProductWithAdminCalculationDelete";
				meta.spLoadAll = "proc_vw_ItemServiceAndProductWithAdminCalculationLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ItemServiceAndProductWithAdminCalculationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwItemServiceAndProductWithAdminCalculationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
