/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:15 PM
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
	abstract public class esRlTxReport33Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport33Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport33Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport33Query query)
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
			this.InitQuery(query as esRlTxReport33Query);
		}
		#endregion
		
		virtual public RlTxReport33 DetachEntity(RlTxReport33 entity)
		{
			return base.DetachEntity(entity) as RlTxReport33;
		}
		
		virtual public RlTxReport33 AttachEntity(RlTxReport33 entity)
		{
			return base.AttachEntity(entity) as RlTxReport33;
		}
		
		virtual public void Combine(RlTxReport33Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport33 this[int index]
		{
			get
			{
				return base[index] as RlTxReport33;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport33);
		}
	}



	[Serializable]
	abstract public class esRlTxReport33 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport33Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport33()
		{

		}

		public esRlTxReport33(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			esRlTxReport33Query query = this.GetDynamicQuery();
			query.Where(query.RlTxReportNo == rlTxReportNo, query.RlMasterReportItemID == rlMasterReportItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("RlTxReportNo",rlTxReportNo);			parms.Add("RlMasterReportItemID",rlMasterReportItemID);
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
						case "RlTxReportNo": this.str.RlTxReportNo = (string)value; break;							
						case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;							
						case "Jumlah": this.str.Jumlah = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
						
						case "Jumlah":
						
							if (value == null || value is System.Int32)
								this.Jumlah = (System.Int32?)value;
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
		/// Maps to RlTxReport3_3.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport33Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport33Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3.Jumlah
		/// </summary>
		virtual public System.Int32? Jumlah
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33Metadata.ColumnNames.Jumlah);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33Metadata.ColumnNames.Jumlah, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport33Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport33Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport33Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport33Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport33 entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RlTxReportNo
			{
				get
				{
					System.String data = entity.RlTxReportNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlTxReportNo = null;
					else entity.RlTxReportNo = Convert.ToString(value);
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
				
			public System.String Jumlah
			{
				get
				{
					System.Int32? data = entity.Jumlah;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jumlah = null;
					else entity.Jumlah = Convert.ToInt32(value);
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
			

			private esRlTxReport33 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport33Query query)
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
				throw new Exception("esRlTxReport33 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport33 : esRlTxReport33
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esRlTxReport33Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport33Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport33Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport33Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Jumlah
		{
			get
			{
				return new esQueryItem(this, RlTxReport33Metadata.ColumnNames.Jumlah, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport33Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport33Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport33Collection")]
	public partial class RlTxReport33Collection : esRlTxReport33Collection, IEnumerable<RlTxReport33>
	{
		public RlTxReport33Collection()
		{

		}
		
		public static implicit operator List<RlTxReport33>(RlTxReport33Collection coll)
		{
			List<RlTxReport33> list = new List<RlTxReport33>();
			
			foreach (RlTxReport33 emp in coll)
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
				return  RlTxReport33Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport33Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport33(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport33();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport33Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport33Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport33Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport33 AddNew()
		{
			RlTxReport33 entity = base.AddNewEntity() as RlTxReport33;
			
			return entity;
		}

		public RlTxReport33 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport33;
		}


		#region IEnumerable<RlTxReport33> Members

		IEnumerator<RlTxReport33> IEnumerable<RlTxReport33>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport33;
			}
		}

		#endregion
		
		private RlTxReport33Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_3' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport33 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport33 : esRlTxReport33
	{
		public RlTxReport33()
		{

		}
	
		public RlTxReport33(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport33Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport33Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport33Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport33Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport33Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport33Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport33Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport33Query : esRlTxReport33Query
	{
		public RlTxReport33Query()
		{

		}		
		
		public RlTxReport33Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport33Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport33Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport33Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport33Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport33Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33Metadata.ColumnNames.Jumlah, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33Metadata.PropertyNames.Jumlah;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33Metadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport33Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33Metadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport33Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport33Metadata Meta()
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
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string Jumlah = "Jumlah";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string Jumlah = "Jumlah";
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
			lock (typeof(RlTxReport33Metadata))
			{
				if(RlTxReport33Metadata.mapDelegates == null)
				{
					RlTxReport33Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport33Metadata.meta == null)
				{
					RlTxReport33Metadata.meta = new RlTxReport33Metadata();
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
				

				meta.AddTypeMap("RlTxReportNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Jumlah", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport3_3";
				meta.Destination = "RlTxReport3_3";
				
				meta.spInsert = "proc_RlTxReport3_3Insert";				
				meta.spUpdate = "proc_RlTxReport3_3Update";		
				meta.spDelete = "proc_RlTxReport3_3Delete";
				meta.spLoadAll = "proc_RlTxReport3_3LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_3LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport33Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
