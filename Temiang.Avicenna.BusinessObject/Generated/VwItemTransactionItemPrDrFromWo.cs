/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 12/3/2015 2:34:52 PM
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
	abstract public class esVwItemTransactionItemPrDrFromWoCollection : esEntityCollectionWAuditLog
	{
		public esVwItemTransactionItemPrDrFromWoCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwItemTransactionItemPrDrFromWoCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwItemTransactionItemPrDrFromWoQuery query)
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
			this.InitQuery(query as esVwItemTransactionItemPrDrFromWoQuery);
		}
		#endregion
		
		virtual public VwItemTransactionItemPrDrFromWo DetachEntity(VwItemTransactionItemPrDrFromWo entity)
		{
			return base.DetachEntity(entity) as VwItemTransactionItemPrDrFromWo;
		}
		
		virtual public VwItemTransactionItemPrDrFromWo AttachEntity(VwItemTransactionItemPrDrFromWo entity)
		{
			return base.AttachEntity(entity) as VwItemTransactionItemPrDrFromWo;
		}
		
		virtual public void Combine(VwItemTransactionItemPrDrFromWoCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwItemTransactionItemPrDrFromWo this[int index]
		{
			get
			{
				return base[index] as VwItemTransactionItemPrDrFromWo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwItemTransactionItemPrDrFromWo);
		}
	}



	[Serializable]
	abstract public class esVwItemTransactionItemPrDrFromWo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwItemTransactionItemPrDrFromWoQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwItemTransactionItemPrDrFromWo()
		{

		}

		public esVwItemTransactionItemPrDrFromWo(DataRow row)
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;
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
		/// Maps to vw_ItemTransactionItemPrDrFromWo.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ItemTransactionItemPrDrFromWo.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ItemTransactionItemPrDrFromWo.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.ReferenceSequenceNo);
			}
			
			set
			{
				base.SetSystemString(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.ReferenceSequenceNo, value);
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
			public esStrings(esVwItemTransactionItemPrDrFromWo entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
				
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
				
			public System.String ReferenceSequenceNo
			{
				get
				{
					System.String data = entity.ReferenceSequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceSequenceNo = null;
					else entity.ReferenceSequenceNo = Convert.ToString(value);
				}
			}
			

			private esVwItemTransactionItemPrDrFromWo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwItemTransactionItemPrDrFromWoQuery query)
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
				throw new Exception("esVwItemTransactionItemPrDrFromWo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwItemTransactionItemPrDrFromWoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwItemTransactionItemPrDrFromWoMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwItemTransactionItemPrDrFromWoCollection")]
	public partial class VwItemTransactionItemPrDrFromWoCollection : esVwItemTransactionItemPrDrFromWoCollection, IEnumerable<VwItemTransactionItemPrDrFromWo>
	{
		public VwItemTransactionItemPrDrFromWoCollection()
		{

		}
		
		public static implicit operator List<VwItemTransactionItemPrDrFromWo>(VwItemTransactionItemPrDrFromWoCollection coll)
		{
			List<VwItemTransactionItemPrDrFromWo> list = new List<VwItemTransactionItemPrDrFromWo>();
			
			foreach (VwItemTransactionItemPrDrFromWo emp in coll)
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
				return  VwItemTransactionItemPrDrFromWoMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemTransactionItemPrDrFromWoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwItemTransactionItemPrDrFromWo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwItemTransactionItemPrDrFromWo();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwItemTransactionItemPrDrFromWoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemTransactionItemPrDrFromWoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwItemTransactionItemPrDrFromWoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwItemTransactionItemPrDrFromWo AddNew()
		{
			VwItemTransactionItemPrDrFromWo entity = base.AddNewEntity() as VwItemTransactionItemPrDrFromWo;
			
			return entity;
		}


		#region IEnumerable<VwItemTransactionItemPrDrFromWo> Members

		IEnumerator<VwItemTransactionItemPrDrFromWo> IEnumerable<VwItemTransactionItemPrDrFromWo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwItemTransactionItemPrDrFromWo;
			}
		}

		#endregion
		
		private VwItemTransactionItemPrDrFromWoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_ItemTransactionItemPrDrFromWo' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwItemTransactionItemPrDrFromWo ()")]
	[Serializable]
	public partial class VwItemTransactionItemPrDrFromWo : esVwItemTransactionItemPrDrFromWo
	{
		public VwItemTransactionItemPrDrFromWo()
		{

		}
	
		public VwItemTransactionItemPrDrFromWo(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwItemTransactionItemPrDrFromWoMetadata.Meta();
			}
		}
		
		
		
		override protected esVwItemTransactionItemPrDrFromWoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemTransactionItemPrDrFromWoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwItemTransactionItemPrDrFromWoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemTransactionItemPrDrFromWoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwItemTransactionItemPrDrFromWoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwItemTransactionItemPrDrFromWoQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwItemTransactionItemPrDrFromWoQuery : esVwItemTransactionItemPrDrFromWoQuery
	{
		public VwItemTransactionItemPrDrFromWoQuery()
		{

		}		
		
		public VwItemTransactionItemPrDrFromWoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwItemTransactionItemPrDrFromWoQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwItemTransactionItemPrDrFromWoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwItemTransactionItemPrDrFromWoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemTransactionItemPrDrFromWoMetadata.PropertyNames.TransactionNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.ReferenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemTransactionItemPrDrFromWoMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwItemTransactionItemPrDrFromWoMetadata.ColumnNames.ReferenceSequenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemTransactionItemPrDrFromWoMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwItemTransactionItemPrDrFromWoMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string ReferenceSequenceNo = "ReferenceSequenceNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string ReferenceSequenceNo = "ReferenceSequenceNo";
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
			lock (typeof(VwItemTransactionItemPrDrFromWoMetadata))
			{
				if(VwItemTransactionItemPrDrFromWoMetadata.mapDelegates == null)
				{
					VwItemTransactionItemPrDrFromWoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwItemTransactionItemPrDrFromWoMetadata.meta == null)
				{
					VwItemTransactionItemPrDrFromWoMetadata.meta = new VwItemTransactionItemPrDrFromWoMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "vw_ItemTransactionItemPrDrFromWo";
				meta.Destination = "vw_ItemTransactionItemPrDrFromWo";
				
				meta.spInsert = "proc_vw_ItemTransactionItemPrDrFromWoInsert";				
				meta.spUpdate = "proc_vw_ItemTransactionItemPrDrFromWoUpdate";		
				meta.spDelete = "proc_vw_ItemTransactionItemPrDrFromWoDelete";
				meta.spLoadAll = "proc_vw_ItemTransactionItemPrDrFromWoLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ItemTransactionItemPrDrFromWoLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwItemTransactionItemPrDrFromWoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
