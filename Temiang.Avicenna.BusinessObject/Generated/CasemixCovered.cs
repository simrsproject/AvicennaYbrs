/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/1/2022 10:39:13 PM
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
	abstract public class esCasemixCoveredCollection : esEntityCollectionWAuditLog
	{
		public esCasemixCoveredCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CasemixCoveredCollection";
		}

		#region Query Logic
		protected void InitQuery(esCasemixCoveredQuery query)
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
			this.InitQuery(query as esCasemixCoveredQuery);
		}
		#endregion
		
		virtual public CasemixCovered DetachEntity(CasemixCovered entity)
		{
			return base.DetachEntity(entity) as CasemixCovered;
		}
		
		virtual public CasemixCovered AttachEntity(CasemixCovered entity)
		{
			return base.AttachEntity(entity) as CasemixCovered;
		}
		
		virtual public void Combine(CasemixCoveredCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CasemixCovered this[int index]
		{
			get
			{
				return base[index] as CasemixCovered;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CasemixCovered);
		}
	}



	[Serializable]
	abstract public class esCasemixCovered : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCasemixCoveredQuery GetDynamicQuery()
		{
			return null;
		}

		public esCasemixCovered()
		{

		}

		public esCasemixCovered(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 casemixCoveredID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(casemixCoveredID);
			else
				return LoadByPrimaryKeyStoredProcedure(casemixCoveredID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 casemixCoveredID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(casemixCoveredID);
			else
				return LoadByPrimaryKeyStoredProcedure(casemixCoveredID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 casemixCoveredID)
		{
			esCasemixCoveredQuery query = this.GetDynamicQuery();
			query.Where(query.CasemixCoveredID == casemixCoveredID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 casemixCoveredID)
		{
			esParameters parms = new esParameters();
			parms.Add("CasemixCoveredID",casemixCoveredID);
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
						case "CasemixCoveredID": this.str.CasemixCoveredID = (string)value; break;							
						case "CasemixCoveredName": this.str.CasemixCoveredName = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CasemixCoveredID":
						
							if (value == null || value is System.Int32)
								this.CasemixCoveredID = (System.Int32?)value;
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to CasemixCovered.CasemixCoveredID
		/// </summary>
		virtual public System.Int32? CasemixCoveredID
		{
			get
			{
				return base.GetSystemInt32(CasemixCoveredMetadata.ColumnNames.CasemixCoveredID);
			}
			
			set
			{
				base.SetSystemInt32(CasemixCoveredMetadata.ColumnNames.CasemixCoveredID, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixCovered.CasemixCoveredName
		/// </summary>
		virtual public System.String CasemixCoveredName
		{
			get
			{
				return base.GetSystemString(CasemixCoveredMetadata.ColumnNames.CasemixCoveredName);
			}
			
			set
			{
				base.SetSystemString(CasemixCoveredMetadata.ColumnNames.CasemixCoveredName, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixCovered.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(CasemixCoveredMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(CasemixCoveredMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixCovered.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CasemixCoveredMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(CasemixCoveredMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixCovered.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CasemixCoveredMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CasemixCoveredMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixCovered.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CasemixCoveredMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CasemixCoveredMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCasemixCovered entity)
			{
				this.entity = entity;
			}
			
	
			public System.String CasemixCoveredID
			{
				get
				{
					System.Int32? data = entity.CasemixCoveredID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixCoveredID = null;
					else entity.CasemixCoveredID = Convert.ToInt32(value);
				}
			}
				
			public System.String CasemixCoveredName
			{
				get
				{
					System.String data = entity.CasemixCoveredName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixCoveredName = null;
					else entity.CasemixCoveredName = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esCasemixCovered entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCasemixCoveredQuery query)
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
				throw new Exception("esCasemixCovered can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esCasemixCoveredQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CasemixCoveredMetadata.Meta();
			}
		}	
		

		public esQueryItem CasemixCoveredID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredMetadata.ColumnNames.CasemixCoveredID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CasemixCoveredName
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredMetadata.ColumnNames.CasemixCoveredName, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CasemixCoveredCollection")]
	public partial class CasemixCoveredCollection : esCasemixCoveredCollection, IEnumerable<CasemixCovered>
	{
		public CasemixCoveredCollection()
		{

		}
		
		public static implicit operator List<CasemixCovered>(CasemixCoveredCollection coll)
		{
			List<CasemixCovered> list = new List<CasemixCovered>();
			
			foreach (CasemixCovered emp in coll)
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
				return  CasemixCoveredMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixCoveredQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CasemixCovered(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CasemixCovered();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CasemixCoveredQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixCoveredQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CasemixCoveredQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CasemixCovered AddNew()
		{
			CasemixCovered entity = base.AddNewEntity() as CasemixCovered;
			
			return entity;
		}

		public CasemixCovered FindByPrimaryKey(System.Int32 casemixCoveredID)
		{
			return base.FindByPrimaryKey(casemixCoveredID) as CasemixCovered;
		}


		#region IEnumerable<CasemixCovered> Members

		IEnumerator<CasemixCovered> IEnumerable<CasemixCovered>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CasemixCovered;
			}
		}

		#endregion
		
		private CasemixCoveredQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CasemixCovered' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CasemixCovered ({CasemixCoveredID})")]
	[Serializable]
	public partial class CasemixCovered : esCasemixCovered
	{
		public CasemixCovered()
		{

		}
	
		public CasemixCovered(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CasemixCoveredMetadata.Meta();
			}
		}
		
		
		
		override protected esCasemixCoveredQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixCoveredQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CasemixCoveredQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixCoveredQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CasemixCoveredQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CasemixCoveredQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CasemixCoveredQuery : esCasemixCoveredQuery
	{
		public CasemixCoveredQuery()
		{

		}		
		
		public CasemixCoveredQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CasemixCoveredQuery";
        }
		
			
	}


	[Serializable]
	public partial class CasemixCoveredMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CasemixCoveredMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CasemixCoveredMetadata.ColumnNames.CasemixCoveredID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CasemixCoveredMetadata.PropertyNames.CasemixCoveredID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixCoveredMetadata.ColumnNames.CasemixCoveredName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixCoveredMetadata.PropertyNames.CasemixCoveredName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixCoveredMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixCoveredMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixCoveredMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CasemixCoveredMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixCoveredMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CasemixCoveredMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixCoveredMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixCoveredMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CasemixCoveredMetadata Meta()
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
			 public const string CasemixCoveredID = "CasemixCoveredID";
			 public const string CasemixCoveredName = "CasemixCoveredName";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CasemixCoveredID = "CasemixCoveredID";
			 public const string CasemixCoveredName = "CasemixCoveredName";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
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
			lock (typeof(CasemixCoveredMetadata))
			{
				if(CasemixCoveredMetadata.mapDelegates == null)
				{
					CasemixCoveredMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CasemixCoveredMetadata.meta == null)
				{
					CasemixCoveredMetadata.meta = new CasemixCoveredMetadata();
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
				

				meta.AddTypeMap("CasemixCoveredID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CasemixCoveredName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CasemixCovered";
				meta.Destination = "CasemixCovered";
				
				meta.spInsert = "proc_CasemixCoveredInsert";				
				meta.spUpdate = "proc_CasemixCoveredUpdate";		
				meta.spDelete = "proc_CasemixCoveredDelete";
				meta.spLoadAll = "proc_CasemixCoveredLoadAll";
				meta.spLoadByPrimaryKey = "proc_CasemixCoveredLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CasemixCoveredMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
