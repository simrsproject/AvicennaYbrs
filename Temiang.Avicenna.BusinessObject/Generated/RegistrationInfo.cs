/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 5/8/2014 2:57:11 PM
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
	abstract public class esRegistrationInfoCollection : esEntityCollection
	{
		public esRegistrationInfoCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationInfoCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationInfoQuery query)
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
			this.InitQuery(query as esRegistrationInfoQuery);
		}
		#endregion
		
		virtual public RegistrationInfo DetachEntity(RegistrationInfo entity)
		{
			return base.DetachEntity(entity) as RegistrationInfo;
		}
		
		virtual public RegistrationInfo AttachEntity(RegistrationInfo entity)
		{
			return base.AttachEntity(entity) as RegistrationInfo;
		}
		
		virtual public void Combine(RegistrationInfoCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationInfo this[int index]
		{
			get
			{
				return base[index] as RegistrationInfo;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationInfo);
		}
	}



	[Serializable]
	abstract public class esRegistrationInfo : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationInfoQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationInfo()
		{

		}

		public esRegistrationInfo(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationInfoID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationInfoID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationInfoID)
		{
			esRegistrationInfoQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationInfoID == registrationInfoID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationInfoID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationInfoID",registrationInfoID);
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
						case "RegistrationInfoID": this.str.RegistrationInfoID = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
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
		/// Maps to RegistrationInfo.RegistrationInfoID
		/// </summary>
		virtual public System.String RegistrationInfoID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMetadata.ColumnNames.RegistrationInfoID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMetadata.ColumnNames.RegistrationInfoID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfo.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				if(base.SetSystemString(RegistrationInfoMetadata.ColumnNames.RegistrationNo, value))
				{
					this._UpToRegistrationByRegistrationNo = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfo.Information
		/// </summary>
		virtual public System.String Information
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMetadata.ColumnNames.Information);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMetadata.ColumnNames.Information, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfo.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfo.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfo.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfo.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected Registration _UpToRegistrationByRegistrationNo;
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
			public esStrings(esRegistrationInfo entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationInfoID
			{
				get
				{
					System.String data = entity.RegistrationInfoID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationInfoID = null;
					else entity.RegistrationInfoID = Convert.ToString(value);
				}
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
			

			private esRegistrationInfo entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationInfoQuery query)
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
				throw new Exception("esRegistrationInfo can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RegistrationInfo : esRegistrationInfo
	{

				
		#region UpToRegistrationByRegistrationNo - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - RefRegistrationToRegistrationInfo
		/// </summary>

		[XmlIgnore]
		public Registration UpToRegistrationByRegistrationNo
		{
			get
			{
				if(this._UpToRegistrationByRegistrationNo == null
					&& RegistrationNo != null					)
				{
					this._UpToRegistrationByRegistrationNo = new Registration();
					this._UpToRegistrationByRegistrationNo.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToRegistrationByRegistrationNo", this._UpToRegistrationByRegistrationNo);
					this._UpToRegistrationByRegistrationNo.Query.Where(this._UpToRegistrationByRegistrationNo.Query.RegistrationNo == this.RegistrationNo);
					this._UpToRegistrationByRegistrationNo.Query.Load();
				}

				return this._UpToRegistrationByRegistrationNo;
			}
			
			set
			{
				this.RemovePreSave("UpToRegistrationByRegistrationNo");
				

				if(value == null)
				{
					this.RegistrationNo = null;
					this._UpToRegistrationByRegistrationNo = null;
				}
				else
				{
					this.RegistrationNo = value.RegistrationNo;
					this._UpToRegistrationByRegistrationNo = value;
					this.SetPreSave("UpToRegistrationByRegistrationNo", this._UpToRegistrationByRegistrationNo);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esRegistrationInfoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationInfoID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMetadata.ColumnNames.RegistrationInfoID, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Information
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMetadata.ColumnNames.Information, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationInfoCollection")]
	public partial class RegistrationInfoCollection : esRegistrationInfoCollection, IEnumerable<RegistrationInfo>
	{
		public RegistrationInfoCollection()
		{

		}
		
		public static implicit operator List<RegistrationInfo>(RegistrationInfoCollection coll)
		{
			List<RegistrationInfo> list = new List<RegistrationInfo>();
			
			foreach (RegistrationInfo emp in coll)
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
				return  RegistrationInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationInfo(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationInfo();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationInfo AddNew()
		{
			RegistrationInfo entity = base.AddNewEntity() as RegistrationInfo;
			
			return entity;
		}

		public RegistrationInfo FindByPrimaryKey(System.String registrationInfoID)
		{
			return base.FindByPrimaryKey(registrationInfoID) as RegistrationInfo;
		}


		#region IEnumerable<RegistrationInfo> Members

		IEnumerator<RegistrationInfo> IEnumerable<RegistrationInfo>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationInfo;
			}
		}

		#endregion
		
		private RegistrationInfoQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationInfo' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationInfo ({RegistrationInfoID})")]
	[Serializable]
	public partial class RegistrationInfo : esRegistrationInfo
	{
		public RegistrationInfo()
		{

		}
	
		public RegistrationInfo(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationInfoQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationInfoQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationInfoQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationInfoQuery : esRegistrationInfoQuery
	{
		public RegistrationInfoQuery()
		{

		}		
		
		public RegistrationInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationInfoQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationInfoMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationInfoMetadata.ColumnNames.RegistrationInfoID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMetadata.PropertyNames.RegistrationInfoID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMetadata.ColumnNames.Information, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMetadata.PropertyNames.Information;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMetadata.ColumnNames.CreatedByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationInfoMetadata Meta()
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
			 public const string RegistrationInfoID = "RegistrationInfoID";
			 public const string RegistrationNo = "RegistrationNo";
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
			 public const string RegistrationInfoID = "RegistrationInfoID";
			 public const string RegistrationNo = "RegistrationNo";
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
			lock (typeof(RegistrationInfoMetadata))
			{
				if(RegistrationInfoMetadata.mapDelegates == null)
				{
					RegistrationInfoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationInfoMetadata.meta == null)
				{
					RegistrationInfoMetadata.meta = new RegistrationInfoMetadata();
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
				

				meta.AddTypeMap("RegistrationInfoID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Information", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "RegistrationInfo";
				meta.Destination = "RegistrationInfo";
				
				meta.spInsert = "proc_RegistrationInfoInsert";				
				meta.spUpdate = "proc_RegistrationInfoUpdate";		
				meta.spDelete = "proc_RegistrationInfoDelete";
				meta.spLoadAll = "proc_RegistrationInfoLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationInfoLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
