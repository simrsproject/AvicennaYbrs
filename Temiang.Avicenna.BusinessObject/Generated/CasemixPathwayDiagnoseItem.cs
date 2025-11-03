/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/22/2021 12:19:04 PM
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
	abstract public class esCasemixPathwayDiagnoseItemCollection : esEntityCollectionWAuditLog
	{
		public esCasemixPathwayDiagnoseItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CasemixPathwayDiagnoseItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esCasemixPathwayDiagnoseItemQuery query)
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
			this.InitQuery(query as esCasemixPathwayDiagnoseItemQuery);
		}
		#endregion
		
		virtual public CasemixPathwayDiagnoseItem DetachEntity(CasemixPathwayDiagnoseItem entity)
		{
			return base.DetachEntity(entity) as CasemixPathwayDiagnoseItem;
		}
		
		virtual public CasemixPathwayDiagnoseItem AttachEntity(CasemixPathwayDiagnoseItem entity)
		{
			return base.AttachEntity(entity) as CasemixPathwayDiagnoseItem;
		}
		
		virtual public void Combine(CasemixPathwayDiagnoseItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CasemixPathwayDiagnoseItem this[int index]
		{
			get
			{
				return base[index] as CasemixPathwayDiagnoseItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CasemixPathwayDiagnoseItem);
		}
	}



	[Serializable]
	abstract public class esCasemixPathwayDiagnoseItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCasemixPathwayDiagnoseItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esCasemixPathwayDiagnoseItem()
		{

		}

		public esCasemixPathwayDiagnoseItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String pathwayID, System.String diagnoseID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pathwayID, diagnoseID);
			else
				return LoadByPrimaryKeyStoredProcedure(pathwayID, diagnoseID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String pathwayID, System.String diagnoseID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(pathwayID, diagnoseID);
            else
                return LoadByPrimaryKeyStoredProcedure(pathwayID, diagnoseID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String pathwayID, System.String diagnoseID)
		{
			esCasemixPathwayDiagnoseItemQuery query = this.GetDynamicQuery();
			query.Where(query.PathwayID == pathwayID, query.DiagnoseID == diagnoseID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String pathwayID, System.String diagnoseID)
		{
			esParameters parms = new esParameters();
            parms.Add("PathwayID", pathwayID);
            parms.Add("DiagnoseID",diagnoseID);	
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
						case "PathwayID": this.str.PathwayID = (string)value; break;							
						case "DiagnoseID": this.str.DiagnoseID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to CasemixPathwayDiagnoseItem.PathwayID
		/// </summary>
		virtual public System.String PathwayID
		{
			get
			{
				return base.GetSystemString(CasemixPathwayDiagnoseItemMetadata.ColumnNames.PathwayID);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayDiagnoseItemMetadata.ColumnNames.PathwayID, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathwayDiagnoseItem.DiagnoseID
		/// </summary>
		virtual public System.String DiagnoseID
		{
			get
			{
				return base.GetSystemString(CasemixPathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathwayDiagnoseItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CasemixPathwayDiagnoseItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CasemixPathwayDiagnoseItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixPathwayDiagnoseItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CasemixPathwayDiagnoseItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CasemixPathwayDiagnoseItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCasemixPathwayDiagnoseItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PathwayID
			{
				get
				{
					System.String data = entity.PathwayID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PathwayID = null;
					else entity.PathwayID = Convert.ToString(value);
				}
			}
				
			public System.String DiagnoseID
			{
				get
				{
					System.String data = entity.DiagnoseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseID = null;
					else entity.DiagnoseID = Convert.ToString(value);
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
			

			private esCasemixPathwayDiagnoseItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCasemixPathwayDiagnoseItemQuery query)
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
				throw new Exception("esCasemixPathwayDiagnoseItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esCasemixPathwayDiagnoseItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CasemixPathwayDiagnoseItemMetadata.Meta();
			}
		}	
		

		public esQueryItem PathwayID
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayDiagnoseItemMetadata.ColumnNames.PathwayID, esSystemType.String);
			}
		} 
		
		public esQueryItem DiagnoseID
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayDiagnoseItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CasemixPathwayDiagnoseItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CasemixPathwayDiagnoseItemCollection")]
	public partial class CasemixPathwayDiagnoseItemCollection : esCasemixPathwayDiagnoseItemCollection, IEnumerable<CasemixPathwayDiagnoseItem>
	{
		public CasemixPathwayDiagnoseItemCollection()
		{

		}
		
		public static implicit operator List<CasemixPathwayDiagnoseItem>(CasemixPathwayDiagnoseItemCollection coll)
		{
			List<CasemixPathwayDiagnoseItem> list = new List<CasemixPathwayDiagnoseItem>();
			
			foreach (CasemixPathwayDiagnoseItem emp in coll)
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
				return  CasemixPathwayDiagnoseItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixPathwayDiagnoseItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CasemixPathwayDiagnoseItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CasemixPathwayDiagnoseItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CasemixPathwayDiagnoseItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixPathwayDiagnoseItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CasemixPathwayDiagnoseItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CasemixPathwayDiagnoseItem AddNew()
		{
			CasemixPathwayDiagnoseItem entity = base.AddNewEntity() as CasemixPathwayDiagnoseItem;
			
			return entity;
		}

		public CasemixPathwayDiagnoseItem FindByPrimaryKey(System.String diagnoseID, System.String pathwayID)
		{
			return base.FindByPrimaryKey(diagnoseID, pathwayID) as CasemixPathwayDiagnoseItem;
		}


		#region IEnumerable<CasemixPathwayDiagnoseItem> Members

		IEnumerator<CasemixPathwayDiagnoseItem> IEnumerable<CasemixPathwayDiagnoseItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CasemixPathwayDiagnoseItem;
			}
		}

		#endregion
		
		private CasemixPathwayDiagnoseItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CasemixPathwayDiagnoseItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CasemixPathwayDiagnoseItem ({PathwayID},{DiagnoseID})")]
	[Serializable]
	public partial class CasemixPathwayDiagnoseItem : esCasemixPathwayDiagnoseItem
	{
		public CasemixPathwayDiagnoseItem()
		{

		}
	
		public CasemixPathwayDiagnoseItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CasemixPathwayDiagnoseItemMetadata.Meta();
			}
		}
		
		
		
		override protected esCasemixPathwayDiagnoseItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixPathwayDiagnoseItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CasemixPathwayDiagnoseItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixPathwayDiagnoseItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CasemixPathwayDiagnoseItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CasemixPathwayDiagnoseItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CasemixPathwayDiagnoseItemQuery : esCasemixPathwayDiagnoseItemQuery
	{
		public CasemixPathwayDiagnoseItemQuery()
		{

		}		
		
		public CasemixPathwayDiagnoseItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CasemixPathwayDiagnoseItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class CasemixPathwayDiagnoseItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CasemixPathwayDiagnoseItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CasemixPathwayDiagnoseItemMetadata.ColumnNames.PathwayID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayDiagnoseItemMetadata.PropertyNames.PathwayID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayDiagnoseItemMetadata.PropertyNames.DiagnoseID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayDiagnoseItemMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CasemixPathwayDiagnoseItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixPathwayDiagnoseItemMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixPathwayDiagnoseItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CasemixPathwayDiagnoseItemMetadata Meta()
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
			 public const string PathwayID = "PathwayID";
			 public const string DiagnoseID = "DiagnoseID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PathwayID = "PathwayID";
			 public const string DiagnoseID = "DiagnoseID";
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
			lock (typeof(CasemixPathwayDiagnoseItemMetadata))
			{
				if(CasemixPathwayDiagnoseItemMetadata.mapDelegates == null)
				{
					CasemixPathwayDiagnoseItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CasemixPathwayDiagnoseItemMetadata.meta == null)
				{
					CasemixPathwayDiagnoseItemMetadata.meta = new CasemixPathwayDiagnoseItemMetadata();
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
				

				meta.AddTypeMap("PathwayID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CasemixPathwayDiagnoseItem";
				meta.Destination = "CasemixPathwayDiagnoseItem";
				
				meta.spInsert = "proc_CasemixPathwayDiagnoseItemInsert";				
				meta.spUpdate = "proc_CasemixPathwayDiagnoseItemUpdate";		
				meta.spDelete = "proc_CasemixPathwayDiagnoseItemDelete";
				meta.spLoadAll = "proc_CasemixPathwayDiagnoseItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_CasemixPathwayDiagnoseItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CasemixPathwayDiagnoseItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
