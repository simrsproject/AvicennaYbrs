/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/1/2015 3:56:48 PM
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
	abstract public class esGuarantorInfoCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorInfoCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorInfoQuery query)
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
			this.InitQuery(query as esGuarantorInfoQuery);
		}
		#endregion
		
		virtual public GuarantorInfo DetachEntity(GuarantorInfo entity)
		{
			return base.DetachEntity(entity) as GuarantorInfo;
		}
		
		virtual public GuarantorInfo AttachEntity(GuarantorInfo entity)
		{
			return base.AttachEntity(entity) as GuarantorInfo;
		}
		
		virtual public void Combine(GuarantorInfoCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorInfo this[int index]
		{
			get
			{
				return base[index] as GuarantorInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorInfo);
		}
	}



	[Serializable]
	abstract public class esGuarantorInfo : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorInfo()
		{

		}

		public esGuarantorInfo(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorInfoID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorInfoID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorInfoID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorInfoID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorInfoID)
		{
			esGuarantorInfoQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorInfoID == guarantorInfoID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorInfoID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorInfoID",guarantorInfoID);
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
						case "GuarantorInfoID": this.str.GuarantorInfoID = (string)value; break;							
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "Information": this.str.Information = (string)value; break;							
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;							
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to GuarantorInfo.GuarantorInfoID
		/// </summary>
		virtual public System.String GuarantorInfoID
		{
			get
			{
				return base.GetSystemString(GuarantorInfoMetadata.ColumnNames.GuarantorInfoID);
			}
			
			set
			{
				base.SetSystemString(GuarantorInfoMetadata.ColumnNames.GuarantorInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfo.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorInfoMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorInfoMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfo.Information
		/// </summary>
		virtual public System.String Information
		{
			get
			{
				return base.GetSystemString(GuarantorInfoMetadata.ColumnNames.Information);
			}
			
			set
			{
				base.SetSystemString(GuarantorInfoMetadata.ColumnNames.Information, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfo.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorInfoMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorInfoMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfo.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorInfoMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorInfoMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorInfoMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorInfoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorInfoMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorInfoMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esGuarantorInfo entity)
			{
				this.entity = entity;
			}
			
	
			public System.String GuarantorInfoID
			{
				get
				{
					System.String data = entity.GuarantorInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorInfoID = null;
					else entity.GuarantorInfoID = Convert.ToString(value);
				}
			}
				
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
				
			public System.String Information
			{
				get
				{
					System.String data = entity.Information;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Information = null;
					else entity.Information = Convert.ToString(value);
				}
			}
				
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
				
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
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
			

			private esGuarantorInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorInfoQuery query)
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
				throw new Exception("esGuarantorInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esGuarantorInfoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorInfoMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorInfoID
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoMetadata.ColumnNames.GuarantorInfoID, esSystemType.String);
			}
		} 
		
		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem Information
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoMetadata.ColumnNames.Information, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorInfoCollection")]
	public partial class GuarantorInfoCollection : esGuarantorInfoCollection, IEnumerable<GuarantorInfo>
	{
		public GuarantorInfoCollection()
		{

		}
		
		public static implicit operator List<GuarantorInfo>(GuarantorInfoCollection coll)
		{
			List<GuarantorInfo> list = new List<GuarantorInfo>();
			
			foreach (GuarantorInfo emp in coll)
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
				return  GuarantorInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorInfo();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorInfo AddNew()
		{
			GuarantorInfo entity = base.AddNewEntity() as GuarantorInfo;
			
			return entity;
		}

		public GuarantorInfo FindByPrimaryKey(System.String guarantorInfoID)
		{
			return base.FindByPrimaryKey(guarantorInfoID) as GuarantorInfo;
		}


		#region IEnumerable<GuarantorInfo> Members

		IEnumerator<GuarantorInfo> IEnumerable<GuarantorInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorInfo;
			}
		}

		#endregion
		
		private GuarantorInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorInfo' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorInfo ({GuarantorInfoID})")]
	[Serializable]
	public partial class GuarantorInfo : esGuarantorInfo
	{
		public GuarantorInfo()
		{

		}
	
		public GuarantorInfo(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorInfoQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorInfoQuery : esGuarantorInfoQuery
	{
		public GuarantorInfoQuery()
		{

		}		
		
		public GuarantorInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorInfoQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorInfoMetadata.ColumnNames.GuarantorInfoID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorInfoMetadata.PropertyNames.GuarantorInfoID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoMetadata.ColumnNames.GuarantorID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorInfoMetadata.PropertyNames.GuarantorID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoMetadata.ColumnNames.Information, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorInfoMetadata.PropertyNames.Information;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoMetadata.ColumnNames.CreatedByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorInfoMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorInfoMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorInfoMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorInfoMetadata Meta()
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
			 public const string GuarantorInfoID = "GuarantorInfoID";
			 public const string GuarantorID = "GuarantorID";
			 public const string Information = "Information";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorInfoID = "GuarantorInfoID";
			 public const string GuarantorID = "GuarantorID";
			 public const string Information = "Information";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(GuarantorInfoMetadata))
			{
				if(GuarantorInfoMetadata.mapDelegates == null)
				{
					GuarantorInfoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorInfoMetadata.meta == null)
				{
					GuarantorInfoMetadata.meta = new GuarantorInfoMetadata();
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
				

				meta.AddTypeMap("GuarantorInfoID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Information", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "GuarantorInfo";
				meta.Destination = "GuarantorInfo";
				
				meta.spInsert = "proc_GuarantorInfoInsert";				
				meta.spUpdate = "proc_GuarantorInfoUpdate";		
				meta.spDelete = "proc_GuarantorInfoDelete";
				meta.spLoadAll = "proc_GuarantorInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorInfoLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
