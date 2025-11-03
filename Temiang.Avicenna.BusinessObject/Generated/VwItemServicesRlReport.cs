/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/28/2014 12:12:34 PM
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
	abstract public class esVwItemServicesRlReportCollection : esEntityCollectionWAuditLog
	{
		public esVwItemServicesRlReportCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwItemServicesRlReportCollection";
		}

		#region Query Logic
		protected void InitQuery(esVwItemServicesRlReportQuery query)
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
			this.InitQuery(query as esVwItemServicesRlReportQuery);
		}
		#endregion
		
		virtual public VwItemServicesRlReport DetachEntity(VwItemServicesRlReport entity)
		{
			return base.DetachEntity(entity) as VwItemServicesRlReport;
		}
		
		virtual public VwItemServicesRlReport AttachEntity(VwItemServicesRlReport entity)
		{
			return base.AttachEntity(entity) as VwItemServicesRlReport;
		}
		
		virtual public void Combine(VwItemServicesRlReportCollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwItemServicesRlReport this[int index]
		{
			get
			{
				return base[index] as VwItemServicesRlReport;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwItemServicesRlReport);
		}
	}



	[Serializable]
	abstract public class esVwItemServicesRlReport : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwItemServicesRlReportQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwItemServicesRlReport()
		{

		}

		public esVwItemServicesRlReport(DataRow row)
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
						case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RlMasterReportItemID":
						
							if (value == null || value is System.Int32)
								this.RlMasterReportItemID = (System.Int32?)value;
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
		/// Maps to vw_ItemServicesRlReport.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(VwItemServicesRlReportMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(VwItemServicesRlReportMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_ItemServicesRlReport.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(VwItemServicesRlReportMetadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(VwItemServicesRlReportMetadata.ColumnNames.RlMasterReportItemID, value);
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
			public esStrings(esVwItemServicesRlReport entity)
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
				
			public System.String RlMasterReportItemID
			{
				get
				{
					System.Int32? data = entity.RlMasterReportItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
					else entity.RlMasterReportItemID = Convert.ToInt32(value);
				}
			}
			

			private esVwItemServicesRlReport entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwItemServicesRlReportQuery query)
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
				throw new Exception("esVwItemServicesRlReport can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwItemServicesRlReportQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwItemServicesRlReportMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, VwItemServicesRlReportMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, VwItemServicesRlReportMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwItemServicesRlReportCollection")]
	public partial class VwItemServicesRlReportCollection : esVwItemServicesRlReportCollection, IEnumerable<VwItemServicesRlReport>
	{
		public VwItemServicesRlReportCollection()
		{

		}
		
		public static implicit operator List<VwItemServicesRlReport>(VwItemServicesRlReportCollection coll)
		{
			List<VwItemServicesRlReport> list = new List<VwItemServicesRlReport>();
			
			foreach (VwItemServicesRlReport emp in coll)
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
				return  VwItemServicesRlReportMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemServicesRlReportQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwItemServicesRlReport(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwItemServicesRlReport();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwItemServicesRlReportQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemServicesRlReportQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwItemServicesRlReportQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwItemServicesRlReport AddNew()
		{
			VwItemServicesRlReport entity = base.AddNewEntity() as VwItemServicesRlReport;
			
			return entity;
		}


		#region IEnumerable<VwItemServicesRlReport> Members

		IEnumerator<VwItemServicesRlReport> IEnumerable<VwItemServicesRlReport>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwItemServicesRlReport;
			}
		}

		#endregion
		
		private VwItemServicesRlReportQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_ItemServicesRlReport' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwItemServicesRlReport ()")]
	[Serializable]
	public partial class VwItemServicesRlReport : esVwItemServicesRlReport
	{
		public VwItemServicesRlReport()
		{

		}
	
		public VwItemServicesRlReport(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwItemServicesRlReportMetadata.Meta();
			}
		}
		
		
		
		override protected esVwItemServicesRlReportQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwItemServicesRlReportQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwItemServicesRlReportQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwItemServicesRlReportQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwItemServicesRlReportQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwItemServicesRlReportQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwItemServicesRlReportQuery : esVwItemServicesRlReportQuery
	{
		public VwItemServicesRlReportQuery()
		{

		}		
		
		public VwItemServicesRlReportQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwItemServicesRlReportQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwItemServicesRlReportMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwItemServicesRlReportMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwItemServicesRlReportMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwItemServicesRlReportMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwItemServicesRlReportMetadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = VwItemServicesRlReportMetadata.PropertyNames.RlMasterReportItemID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwItemServicesRlReportMetadata Meta()
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
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
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
			lock (typeof(VwItemServicesRlReportMetadata))
			{
				if(VwItemServicesRlReportMetadata.mapDelegates == null)
				{
					VwItemServicesRlReportMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwItemServicesRlReportMetadata.meta == null)
				{
					VwItemServicesRlReportMetadata.meta = new VwItemServicesRlReportMetadata();
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
				meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "vw_ItemServicesRlReport";
				meta.Destination = "vw_ItemServicesRlReport";
				
				meta.spInsert = "proc_vw_ItemServicesRlReportInsert";				
				meta.spUpdate = "proc_vw_ItemServicesRlReportUpdate";		
				meta.spDelete = "proc_vw_ItemServicesRlReportDelete";
				meta.spLoadAll = "proc_vw_ItemServicesRlReportLoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_ItemServicesRlReportLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwItemServicesRlReportMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
