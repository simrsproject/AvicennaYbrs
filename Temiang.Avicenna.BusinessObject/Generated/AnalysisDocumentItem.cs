/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
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
	abstract public class esAnalysisDocumentItemCollection : esEntityCollectionWAuditLog
	{
		public esAnalysisDocumentItemCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AnalysisDocumentItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esAnalysisDocumentItemQuery query)
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
			this.InitQuery(query as esAnalysisDocumentItemQuery);
		}
		#endregion
		
		virtual public AnalysisDocumentItem DetachEntity(AnalysisDocumentItem entity)
		{
			return base.DetachEntity(entity) as AnalysisDocumentItem;
		}
		
		virtual public AnalysisDocumentItem AttachEntity(AnalysisDocumentItem entity)
		{
			return base.AttachEntity(entity) as AnalysisDocumentItem;
		}
		
		virtual public void Combine(AnalysisDocumentItemCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AnalysisDocumentItem this[int index]
		{
			get
			{
				return base[index] as AnalysisDocumentItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AnalysisDocumentItem);
		}
	}



	[Serializable]
	abstract public class esAnalysisDocumentItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAnalysisDocumentItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esAnalysisDocumentItem()
		{

		}

		public esAnalysisDocumentItem(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo, System.Int32 documentFilesID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, documentFilesID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo, System.Int32 documentFilesID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, documentFilesID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, documentFilesID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo, System.Int32 documentFilesID)
		{
			esAnalysisDocumentItemQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.DocumentFilesID == documentFilesID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo, System.Int32 documentFilesID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);			parms.Add("DocumentFilesID",documentFilesID);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "DocumentFilesID": this.str.DocumentFilesID = (string)value; break;							
						case "IsQuantity": this.str.IsQuantity = (string)value; break;							
						case "IsQuality": this.str.IsQuality = (string)value; break;							
						case "IsLegible": this.str.IsLegible = (string)value; break;							
						case "IsSign": this.str.IsSign = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DocumentFilesID":
						
							if (value == null || value is System.Int32)
								this.DocumentFilesID = (System.Int32?)value;
							break;
						
						case "IsQuantity":
						
							if (value == null || value is System.Boolean)
								this.IsQuantity = (System.Boolean?)value;
							break;
						
						case "IsQuality":
						
							if (value == null || value is System.Boolean)
								this.IsQuality = (System.Boolean?)value;
							break;
						
						case "IsLegible":
						
							if (value == null || value is System.Boolean)
								this.IsLegible = (System.Boolean?)value;
							break;
						
						case "IsSign":
						
							if (value == null || value is System.Boolean)
								this.IsSign = (System.Boolean?)value;
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
		/// Maps to AnalysisDocumentItem.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(AnalysisDocumentItemMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(AnalysisDocumentItemMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocumentItem.DocumentFilesID
		/// </summary>
		virtual public System.Int32? DocumentFilesID
		{
			get
			{
				return base.GetSystemInt32(AnalysisDocumentItemMetadata.ColumnNames.DocumentFilesID);
			}
			
			set
			{
				base.SetSystemInt32(AnalysisDocumentItemMetadata.ColumnNames.DocumentFilesID, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocumentItem.IsQuantity
		/// </summary>
		virtual public System.Boolean? IsQuantity
		{
			get
			{
				return base.GetSystemBoolean(AnalysisDocumentItemMetadata.ColumnNames.IsQuantity);
			}
			
			set
			{
				base.SetSystemBoolean(AnalysisDocumentItemMetadata.ColumnNames.IsQuantity, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocumentItem.IsQuality
		/// </summary>
		virtual public System.Boolean? IsQuality
		{
			get
			{
				return base.GetSystemBoolean(AnalysisDocumentItemMetadata.ColumnNames.IsQuality);
			}
			
			set
			{
				base.SetSystemBoolean(AnalysisDocumentItemMetadata.ColumnNames.IsQuality, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocumentItem.IsLegible
		/// </summary>
		virtual public System.Boolean? IsLegible
		{
			get
			{
				return base.GetSystemBoolean(AnalysisDocumentItemMetadata.ColumnNames.IsLegible);
			}
			
			set
			{
				base.SetSystemBoolean(AnalysisDocumentItemMetadata.ColumnNames.IsLegible, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocumentItem.IsSign
		/// </summary>
		virtual public System.Boolean? IsSign
		{
			get
			{
				return base.GetSystemBoolean(AnalysisDocumentItemMetadata.ColumnNames.IsSign);
			}
			
			set
			{
				base.SetSystemBoolean(AnalysisDocumentItemMetadata.ColumnNames.IsSign, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocumentItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AnalysisDocumentItemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AnalysisDocumentItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AnalysisDocumentItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AnalysisDocumentItemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AnalysisDocumentItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAnalysisDocumentItem entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String DocumentFilesID
			{
				get
				{
					System.Int32? data = entity.DocumentFilesID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DocumentFilesID = null;
					else entity.DocumentFilesID = Convert.ToInt32(value);
				}
			}
				
			public System.String IsQuantity
			{
				get
				{
					System.Boolean? data = entity.IsQuantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsQuantity = null;
					else entity.IsQuantity = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsQuality
			{
				get
				{
					System.Boolean? data = entity.IsQuality;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsQuality = null;
					else entity.IsQuality = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsLegible
			{
				get
				{
					System.Boolean? data = entity.IsLegible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLegible = null;
					else entity.IsLegible = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsSign
			{
				get
				{
					System.Boolean? data = entity.IsSign;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSign = null;
					else entity.IsSign = Convert.ToBoolean(value);
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
			

			private esAnalysisDocumentItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAnalysisDocumentItemQuery query)
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
				throw new Exception("esAnalysisDocumentItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AnalysisDocumentItem : esAnalysisDocumentItem
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
	abstract public class esAnalysisDocumentItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AnalysisDocumentItemMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentItemMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem DocumentFilesID
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentItemMetadata.ColumnNames.DocumentFilesID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsQuantity
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentItemMetadata.ColumnNames.IsQuantity, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsQuality
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentItemMetadata.ColumnNames.IsQuality, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsLegible
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentItemMetadata.ColumnNames.IsLegible, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsSign
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentItemMetadata.ColumnNames.IsSign, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AnalysisDocumentItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AnalysisDocumentItemCollection")]
	public partial class AnalysisDocumentItemCollection : esAnalysisDocumentItemCollection, IEnumerable<AnalysisDocumentItem>
	{
		public AnalysisDocumentItemCollection()
		{

		}
		
		public static implicit operator List<AnalysisDocumentItem>(AnalysisDocumentItemCollection coll)
		{
			List<AnalysisDocumentItem> list = new List<AnalysisDocumentItem>();
			
			foreach (AnalysisDocumentItem emp in coll)
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
				return  AnalysisDocumentItemMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AnalysisDocumentItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AnalysisDocumentItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AnalysisDocumentItem();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AnalysisDocumentItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AnalysisDocumentItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AnalysisDocumentItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AnalysisDocumentItem AddNew()
		{
			AnalysisDocumentItem entity = base.AddNewEntity() as AnalysisDocumentItem;
			
			return entity;
		}

		public AnalysisDocumentItem FindByPrimaryKey(System.String registrationNo, System.Int32 documentFilesID)
		{
			return base.FindByPrimaryKey(registrationNo, documentFilesID) as AnalysisDocumentItem;
		}


		#region IEnumerable<AnalysisDocumentItem> Members

		IEnumerator<AnalysisDocumentItem> IEnumerable<AnalysisDocumentItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AnalysisDocumentItem;
			}
		}

		#endregion
		
		private AnalysisDocumentItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AnalysisDocumentItem' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AnalysisDocumentItem ({RegistrationNo},{DocumentFilesID})")]
	[Serializable]
	public partial class AnalysisDocumentItem : esAnalysisDocumentItem
	{
		public AnalysisDocumentItem()
		{

		}
	
		public AnalysisDocumentItem(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AnalysisDocumentItemMetadata.Meta();
			}
		}
		
		
		
		override protected esAnalysisDocumentItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AnalysisDocumentItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AnalysisDocumentItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AnalysisDocumentItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AnalysisDocumentItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AnalysisDocumentItemQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AnalysisDocumentItemQuery : esAnalysisDocumentItemQuery
	{
		public AnalysisDocumentItemQuery()
		{

		}		
		
		public AnalysisDocumentItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AnalysisDocumentItemQuery";
        }
		
			
	}


	[Serializable]
	public partial class AnalysisDocumentItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AnalysisDocumentItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AnalysisDocumentItemMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AnalysisDocumentItemMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentItemMetadata.ColumnNames.DocumentFilesID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AnalysisDocumentItemMetadata.PropertyNames.DocumentFilesID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentItemMetadata.ColumnNames.IsQuantity, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AnalysisDocumentItemMetadata.PropertyNames.IsQuantity;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentItemMetadata.ColumnNames.IsQuality, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AnalysisDocumentItemMetadata.PropertyNames.IsQuality;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentItemMetadata.ColumnNames.IsLegible, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AnalysisDocumentItemMetadata.PropertyNames.IsLegible;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentItemMetadata.ColumnNames.IsSign, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AnalysisDocumentItemMetadata.PropertyNames.IsSign;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentItemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AnalysisDocumentItemMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(AnalysisDocumentItemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AnalysisDocumentItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AnalysisDocumentItemMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string DocumentFilesID = "DocumentFilesID";
			 public const string IsQuantity = "IsQuantity";
			 public const string IsQuality = "IsQuality";
			 public const string IsLegible = "IsLegible";
			 public const string IsSign = "IsSign";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string DocumentFilesID = "DocumentFilesID";
			 public const string IsQuantity = "IsQuantity";
			 public const string IsQuality = "IsQuality";
			 public const string IsLegible = "IsLegible";
			 public const string IsSign = "IsSign";
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
			lock (typeof(AnalysisDocumentItemMetadata))
			{
				if(AnalysisDocumentItemMetadata.mapDelegates == null)
				{
					AnalysisDocumentItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AnalysisDocumentItemMetadata.meta == null)
				{
					AnalysisDocumentItemMetadata.meta = new AnalysisDocumentItemMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DocumentFilesID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsQuantity", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsQuality", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLegible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSign", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AnalysisDocumentItem";
				meta.Destination = "AnalysisDocumentItem";
				
				meta.spInsert = "proc_AnalysisDocumentItemInsert";				
				meta.spUpdate = "proc_AnalysisDocumentItemUpdate";		
				meta.spDelete = "proc_AnalysisDocumentItemDelete";
				meta.spLoadAll = "proc_AnalysisDocumentItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_AnalysisDocumentItemLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AnalysisDocumentItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
