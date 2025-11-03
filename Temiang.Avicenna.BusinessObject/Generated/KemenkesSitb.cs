/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/12/2022 6:20:15 PM
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
	abstract public class esKemenkesSitbCollection : esEntityCollectionWAuditLog
	{
		public esKemenkesSitbCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "KemenkesSitbCollection";
		}

		#region Query Logic
		protected void InitQuery(esKemenkesSitbQuery query)
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
			this.InitQuery(query as esKemenkesSitbQuery);
		}
		#endregion
		
		virtual public KemenkesSitb DetachEntity(KemenkesSitb entity)
		{
			return base.DetachEntity(entity) as KemenkesSitb;
		}
		
		virtual public KemenkesSitb AttachEntity(KemenkesSitb entity)
		{
			return base.AttachEntity(entity) as KemenkesSitb;
		}
		
		virtual public void Combine(KemenkesSitbCollection collection)
		{
			base.Combine(collection);
		}
		
		new public KemenkesSitb this[int index]
		{
			get
			{
				return base[index] as KemenkesSitb;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(KemenkesSitb);
		}
	}



	[Serializable]
	abstract public class esKemenkesSitb : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esKemenkesSitbQuery GetDynamicQuery()
		{
			return null;
		}

		public esKemenkesSitb()
		{

		}

		public esKemenkesSitb(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo)
		{
			esKemenkesSitbQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
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
						case "SitbNo": this.str.SitbNo = (string)value; break;							
						case "RequestSitb": this.str.RequestSitb = (string)value; break;							
						case "ResponseSitb": this.str.ResponseSitb = (string)value; break;							
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
		/// Maps to KemenkesSitb.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(KemenkesSitbMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(KemenkesSitbMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to KemenkesSitb.SitbNo
		/// </summary>
		virtual public System.String SitbNo
		{
			get
			{
				return base.GetSystemString(KemenkesSitbMetadata.ColumnNames.SitbNo);
			}
			
			set
			{
				base.SetSystemString(KemenkesSitbMetadata.ColumnNames.SitbNo, value);
			}
		}
		
		/// <summary>
		/// Maps to KemenkesSitb.RequestSitb
		/// </summary>
		virtual public System.String RequestSitb
		{
			get
			{
				return base.GetSystemString(KemenkesSitbMetadata.ColumnNames.RequestSitb);
			}
			
			set
			{
				base.SetSystemString(KemenkesSitbMetadata.ColumnNames.RequestSitb, value);
			}
		}
		
		/// <summary>
		/// Maps to KemenkesSitb.ResponseSitb
		/// </summary>
		virtual public System.String ResponseSitb
		{
			get
			{
				return base.GetSystemString(KemenkesSitbMetadata.ColumnNames.ResponseSitb);
			}
			
			set
			{
				base.SetSystemString(KemenkesSitbMetadata.ColumnNames.ResponseSitb, value);
			}
		}
		
		/// <summary>
		/// Maps to KemenkesSitb.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(KemenkesSitbMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(KemenkesSitbMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to KemenkesSitb.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(KemenkesSitbMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(KemenkesSitbMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esKemenkesSitb entity)
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
				
			public System.String SitbNo
			{
				get
				{
					System.String data = entity.SitbNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SitbNo = null;
					else entity.SitbNo = Convert.ToString(value);
				}
			}
				
			public System.String RequestSitb
			{
				get
				{
					System.String data = entity.RequestSitb;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestSitb = null;
					else entity.RequestSitb = Convert.ToString(value);
				}
			}
				
			public System.String ResponseSitb
			{
				get
				{
					System.String data = entity.ResponseSitb;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResponseSitb = null;
					else entity.ResponseSitb = Convert.ToString(value);
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
			

			private esKemenkesSitb entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esKemenkesSitbQuery query)
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
				throw new Exception("esKemenkesSitb can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esKemenkesSitbQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return KemenkesSitbMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, KemenkesSitbMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SitbNo
		{
			get
			{
				return new esQueryItem(this, KemenkesSitbMetadata.ColumnNames.SitbNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RequestSitb
		{
			get
			{
				return new esQueryItem(this, KemenkesSitbMetadata.ColumnNames.RequestSitb, esSystemType.String);
			}
		} 
		
		public esQueryItem ResponseSitb
		{
			get
			{
				return new esQueryItem(this, KemenkesSitbMetadata.ColumnNames.ResponseSitb, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, KemenkesSitbMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, KemenkesSitbMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("KemenkesSitbCollection")]
	public partial class KemenkesSitbCollection : esKemenkesSitbCollection, IEnumerable<KemenkesSitb>
	{
		public KemenkesSitbCollection()
		{

		}
		
		public static implicit operator List<KemenkesSitb>(KemenkesSitbCollection coll)
		{
			List<KemenkesSitb> list = new List<KemenkesSitb>();
			
			foreach (KemenkesSitb emp in coll)
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
				return  KemenkesSitbMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new KemenkesSitbQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new KemenkesSitb(row);
		}

		override protected esEntity CreateEntity()
		{
			return new KemenkesSitb();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public KemenkesSitbQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new KemenkesSitbQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(KemenkesSitbQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public KemenkesSitb AddNew()
		{
			KemenkesSitb entity = base.AddNewEntity() as KemenkesSitb;
			
			return entity;
		}

		public KemenkesSitb FindByPrimaryKey(System.String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as KemenkesSitb;
		}


		#region IEnumerable<KemenkesSitb> Members

		IEnumerator<KemenkesSitb> IEnumerable<KemenkesSitb>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as KemenkesSitb;
			}
		}

		#endregion
		
		private KemenkesSitbQuery query;
	}


	/// <summary>
	/// Encapsulates the 'KemenkesSitb' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("KemenkesSitb ({RegistrationNo})")]
	[Serializable]
	public partial class KemenkesSitb : esKemenkesSitb
	{
		public KemenkesSitb()
		{

		}
	
		public KemenkesSitb(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return KemenkesSitbMetadata.Meta();
			}
		}
		
		
		
		override protected esKemenkesSitbQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new KemenkesSitbQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public KemenkesSitbQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new KemenkesSitbQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(KemenkesSitbQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private KemenkesSitbQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class KemenkesSitbQuery : esKemenkesSitbQuery
	{
		public KemenkesSitbQuery()
		{

		}		
		
		public KemenkesSitbQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "KemenkesSitbQuery";
        }
		
			
	}


	[Serializable]
	public partial class KemenkesSitbMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected KemenkesSitbMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(KemenkesSitbMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = KemenkesSitbMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(KemenkesSitbMetadata.ColumnNames.SitbNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = KemenkesSitbMetadata.PropertyNames.SitbNo;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(KemenkesSitbMetadata.ColumnNames.RequestSitb, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = KemenkesSitbMetadata.PropertyNames.RequestSitb;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(KemenkesSitbMetadata.ColumnNames.ResponseSitb, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = KemenkesSitbMetadata.PropertyNames.ResponseSitb;
			c.CharacterMaxLength = 2147483647;
			_columns.Add(c);
				
			c = new esColumnMetadata(KemenkesSitbMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = KemenkesSitbMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(KemenkesSitbMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = KemenkesSitbMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public KemenkesSitbMetadata Meta()
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
			 public const string SitbNo = "SitbNo";
			 public const string RequestSitb = "RequestSitb";
			 public const string ResponseSitb = "ResponseSitb";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SitbNo = "SitbNo";
			 public const string RequestSitb = "RequestSitb";
			 public const string ResponseSitb = "ResponseSitb";
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
			lock (typeof(KemenkesSitbMetadata))
			{
				if(KemenkesSitbMetadata.mapDelegates == null)
				{
					KemenkesSitbMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (KemenkesSitbMetadata.meta == null)
				{
					KemenkesSitbMetadata.meta = new KemenkesSitbMetadata();
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
				meta.AddTypeMap("SitbNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestSitb", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResponseSitb", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "KemenkesSitb";
				meta.Destination = "KemenkesSitb";
				
				meta.spInsert = "proc_KemenkesSitbInsert";				
				meta.spUpdate = "proc_KemenkesSitbUpdate";		
				meta.spDelete = "proc_KemenkesSitbDelete";
				meta.spLoadAll = "proc_KemenkesSitbLoadAll";
				meta.spLoadByPrimaryKey = "proc_KemenkesSitbLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private KemenkesSitbMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
