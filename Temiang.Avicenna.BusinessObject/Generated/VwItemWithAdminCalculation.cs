/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/24/2014 9:30:58 AM
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
	abstract public class esVwItemWithAdminCalculationCollection : esEntityCollectionWAuditLog
	{
		public esVwItemWithAdminCalculationCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwItemWithAdminCalculationCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwItemWithAdminCalculationQuery query)
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
			this.InitQuery(query as esVwItemWithAdminCalculationQuery);
		}
		#endregion
		
		virtual public VwItemWithAdminCalculation DetachEntity(VwItemWithAdminCalculation entity)
		{
			return base.DetachEntity(entity) as VwItemWithAdminCalculation;
		}
		
		virtual public VwItemWithAdminCalculation AttachEntity(VwItemWithAdminCalculation entity)
		{
			return base.AttachEntity(entity) as VwItemWithAdminCalculation;
		}
		
		virtual public void Combine(VwItemWithAdminCalculationCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwItemWithAdminCalculation this[int index]
		{
			get
			{
				return base[index] as VwItemWithAdminCalculation;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwItemWithAdminCalculation);
		}
	}



	[Serializable]
	abstract public class esVwItemWithAdminCalculation : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwItemWithAdminCalculationQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwItemWithAdminCalculation()
		{

		}

		public esVwItemWithAdminCalculation(DataRow row)
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
		/// Maps to vw_ItemWithAdminCalculation.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(VwItemWithAdminCalculationMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(VwItemWithAdminCalculationMetadata.ColumnNames.ItemID, value);
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
			public esStrings(esVwItemWithAdminCalculation entity)
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
			

			private esVwItemWithAdminCalculation entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwItemWithAdminCalculationQuery query)
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
				throw new Exception("esVwItemWithAdminCalculation can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwItemWithAdminCalculationQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwItemWithAdminCalculationMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, VwItemWithAdminCalculationMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwItemWithAdminCalculationCollection")]
	public partial class VwItemWithAdminCalculationCollection : esVwItemWithAdminCalculationCollection, IEnumerable<VwItemWithAdminCalculation>
	{
		public VwItemWithAdminCalculationCollection()
		{

		}
		
		public static implicit operator List<VwItemWithAdminCalculation>(VwItemWithAdminCalculationCollection coll)
		{
			List<VwItemWithAdminCalculation> list = new List<VwItemWithAdminCalculation>();
			
			foreach (VwItemWithAdminCalculation emp in coll)
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
				return  VwItemWithAdminCalculationMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemWithAdminCalculationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwItemWithAdminCalculation(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwItemWithAdminCalculation();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwItemWithAdminCalculationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemWithAdminCalculationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwItemWithAdminCalculationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwItemWithAdminCalculation AddNew()
		{
			VwItemWithAdminCalculation entity = base.AddNewEntity() as VwItemWithAdminCalculation;
			
			return entity;
		}


		#region IEnumerable<VwItemWithAdminCalculation> Members

		IEnumerator<VwItemWithAdminCalculation> IEnumerable<VwItemWithAdminCalculation>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwItemWithAdminCalculation;
			}
		}

		#endregion
		
		private VwItemWithAdminCalculationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_ItemWithAdminCalculation' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwItemWithAdminCalculation ()")]
	[Serializable]
	public partial class VwItemWithAdminCalculation : esVwItemWithAdminCalculation
	{
		public VwItemWithAdminCalculation()
		{

		}
	
		public VwItemWithAdminCalculation(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwItemWithAdminCalculationMetadata.Meta();
			}
		}
		
		
		
		override protected esVwItemWithAdminCalculationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemWithAdminCalculationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwItemWithAdminCalculationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemWithAdminCalculationQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwItemWithAdminCalculationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwItemWithAdminCalculationQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwItemWithAdminCalculationQuery : esVwItemWithAdminCalculationQuery
	{
		public VwItemWithAdminCalculationQuery()
		{

		}		
		
		public VwItemWithAdminCalculationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwItemWithAdminCalculationQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwItemWithAdminCalculationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwItemWithAdminCalculationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwItemWithAdminCalculationMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemWithAdminCalculationMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwItemWithAdminCalculationMetadata Meta()
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
			lock (typeof(VwItemWithAdminCalculationMetadata))
			{
				if(VwItemWithAdminCalculationMetadata.mapDelegates == null)
				{
					VwItemWithAdminCalculationMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwItemWithAdminCalculationMetadata.meta == null)
				{
					VwItemWithAdminCalculationMetadata.meta = new VwItemWithAdminCalculationMetadata();
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
				
				
				
				meta.Source = "vw_ItemWithAdminCalculation";
				meta.Destination = "vw_ItemWithAdminCalculation";
				
				meta.spInsert = "proc_vw_ItemWithAdminCalculationInsert";				
				meta.spUpdate = "proc_vw_ItemWithAdminCalculationUpdate";		
				meta.spDelete = "proc_vw_ItemWithAdminCalculationDelete";
				meta.spLoadAll = "proc_vw_ItemWithAdminCalculationLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ItemWithAdminCalculationLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwItemWithAdminCalculationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
