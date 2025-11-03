/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/8/2020 1:56:39 PM
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
	abstract public class esWebServiceAccessKeyCollection : esEntityCollectionWAuditLog
	{
		public esWebServiceAccessKeyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "WebServiceAccessKeyCollection";
		}

		#region Query Logic
		protected void InitQuery(esWebServiceAccessKeyQuery query)
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
			this.InitQuery(query as esWebServiceAccessKeyQuery);
		}
		#endregion
		
		virtual public WebServiceAccessKey DetachEntity(WebServiceAccessKey entity)
		{
			return base.DetachEntity(entity) as WebServiceAccessKey;
		}
		
		virtual public WebServiceAccessKey AttachEntity(WebServiceAccessKey entity)
		{
			return base.AttachEntity(entity) as WebServiceAccessKey;
		}
		
		virtual public void Combine(WebServiceAccessKeyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public WebServiceAccessKey this[int index]
		{
			get
			{
				return base[index] as WebServiceAccessKey;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(WebServiceAccessKey);
		}
	}



	[Serializable]
	abstract public class esWebServiceAccessKey : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esWebServiceAccessKeyQuery GetDynamicQuery()
		{
			return null;
		}

		public esWebServiceAccessKey()
		{

		}

		public esWebServiceAccessKey(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String clientCode)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(clientCode);
			else
				return LoadByPrimaryKeyStoredProcedure(clientCode);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String clientCode)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(clientCode);
			else
				return LoadByPrimaryKeyStoredProcedure(clientCode);
		}

		private bool LoadByPrimaryKeyDynamic(System.String clientCode)
		{
			esWebServiceAccessKeyQuery query = this.GetDynamicQuery();
			query.Where(query.ClientCode == clientCode);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String clientCode)
		{
			esParameters parms = new esParameters();
			parms.Add("ClientCode",clientCode);
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
						case "ClientCode": this.str.ClientCode = (string)value; break;							
						case "ClientName": this.str.ClientName = (string)value; break;							
						case "StartDate": this.str.StartDate = (string)value; break;							
						case "EndDate": this.str.EndDate = (string)value; break;							
						case "AccessKey": this.str.AccessKey = (string)value; break;							
						case "RequestUrl": this.str.RequestUrl = (string)value; break;							
						case "ResponseUrl": this.str.ResponseUrl = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StartDate":
						
							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						
						case "EndDate":
						
							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
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
		/// Maps to WebServiceAccessKey.ClientCode
		/// </summary>
		virtual public System.String ClientCode
		{
			get
			{
				return base.GetSystemString(WebServiceAccessKeyMetadata.ColumnNames.ClientCode);
			}
			
			set
			{
				base.SetSystemString(WebServiceAccessKeyMetadata.ColumnNames.ClientCode, value);
			}
		}
		
		/// <summary>
		/// Maps to WebServiceAccessKey.ClientName
		/// </summary>
		virtual public System.String ClientName
		{
			get
			{
				return base.GetSystemString(WebServiceAccessKeyMetadata.ColumnNames.ClientName);
			}
			
			set
			{
				base.SetSystemString(WebServiceAccessKeyMetadata.ColumnNames.ClientName, value);
			}
		}
		
		/// <summary>
		/// Maps to WebServiceAccessKey.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(WebServiceAccessKeyMetadata.ColumnNames.StartDate);
			}
			
			set
			{
				base.SetSystemDateTime(WebServiceAccessKeyMetadata.ColumnNames.StartDate, value);
			}
		}
		
		/// <summary>
		/// Maps to WebServiceAccessKey.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(WebServiceAccessKeyMetadata.ColumnNames.EndDate);
			}
			
			set
			{
				base.SetSystemDateTime(WebServiceAccessKeyMetadata.ColumnNames.EndDate, value);
			}
		}
		
		/// <summary>
		/// Maps to WebServiceAccessKey.AccessKey
		/// </summary>
		virtual public System.String AccessKey
		{
			get
			{
				return base.GetSystemString(WebServiceAccessKeyMetadata.ColumnNames.AccessKey);
			}
			
			set
			{
				base.SetSystemString(WebServiceAccessKeyMetadata.ColumnNames.AccessKey, value);
			}
		}
		
		/// <summary>
		/// Maps to WebServiceAccessKey.RequestUrl
		/// </summary>
		virtual public System.String RequestUrl
		{
			get
			{
				return base.GetSystemString(WebServiceAccessKeyMetadata.ColumnNames.RequestUrl);
			}
			
			set
			{
				base.SetSystemString(WebServiceAccessKeyMetadata.ColumnNames.RequestUrl, value);
			}
		}
		
		/// <summary>
		/// Maps to WebServiceAccessKey.ResponseUrl
		/// </summary>
		virtual public System.String ResponseUrl
		{
			get
			{
				return base.GetSystemString(WebServiceAccessKeyMetadata.ColumnNames.ResponseUrl);
			}
			
			set
			{
				base.SetSystemString(WebServiceAccessKeyMetadata.ColumnNames.ResponseUrl, value);
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
			public esStrings(esWebServiceAccessKey entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ClientCode
			{
				get
				{
					System.String data = entity.ClientCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClientCode = null;
					else entity.ClientCode = Convert.ToString(value);
				}
			}
				
			public System.String ClientName
			{
				get
				{
					System.String data = entity.ClientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClientName = null;
					else entity.ClientName = Convert.ToString(value);
				}
			}
				
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String AccessKey
			{
				get
				{
					System.String data = entity.AccessKey;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccessKey = null;
					else entity.AccessKey = Convert.ToString(value);
				}
			}
				
			public System.String RequestUrl
			{
				get
				{
					System.String data = entity.RequestUrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestUrl = null;
					else entity.RequestUrl = Convert.ToString(value);
				}
			}
				
			public System.String ResponseUrl
			{
				get
				{
					System.String data = entity.ResponseUrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResponseUrl = null;
					else entity.ResponseUrl = Convert.ToString(value);
				}
			}
			

			private esWebServiceAccessKey entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esWebServiceAccessKeyQuery query)
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
				throw new Exception("esWebServiceAccessKey can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esWebServiceAccessKeyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return WebServiceAccessKeyMetadata.Meta();
			}
		}	
		

		public esQueryItem ClientCode
		{
			get
			{
				return new esQueryItem(this, WebServiceAccessKeyMetadata.ColumnNames.ClientCode, esSystemType.String);
			}
		} 
		
		public esQueryItem ClientName
		{
			get
			{
				return new esQueryItem(this, WebServiceAccessKeyMetadata.ColumnNames.ClientName, esSystemType.String);
			}
		} 
		
		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, WebServiceAccessKeyMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, WebServiceAccessKeyMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem AccessKey
		{
			get
			{
				return new esQueryItem(this, WebServiceAccessKeyMetadata.ColumnNames.AccessKey, esSystemType.String);
			}
		} 
		
		public esQueryItem RequestUrl
		{
			get
			{
				return new esQueryItem(this, WebServiceAccessKeyMetadata.ColumnNames.RequestUrl, esSystemType.String);
			}
		} 
		
		public esQueryItem ResponseUrl
		{
			get
			{
				return new esQueryItem(this, WebServiceAccessKeyMetadata.ColumnNames.ResponseUrl, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("WebServiceAccessKeyCollection")]
	public partial class WebServiceAccessKeyCollection : esWebServiceAccessKeyCollection, IEnumerable<WebServiceAccessKey>
	{
		public WebServiceAccessKeyCollection()
		{

		}
		
		public static implicit operator List<WebServiceAccessKey>(WebServiceAccessKeyCollection coll)
		{
			List<WebServiceAccessKey> list = new List<WebServiceAccessKey>();
			
			foreach (WebServiceAccessKey emp in coll)
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
				return  WebServiceAccessKeyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WebServiceAccessKeyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new WebServiceAccessKey(row);
		}

		override protected esEntity CreateEntity()
		{
			return new WebServiceAccessKey();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public WebServiceAccessKeyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WebServiceAccessKeyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(WebServiceAccessKeyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public WebServiceAccessKey AddNew()
		{
			WebServiceAccessKey entity = base.AddNewEntity() as WebServiceAccessKey;
			
			return entity;
		}

		public WebServiceAccessKey FindByPrimaryKey(System.String clientCode)
		{
			return base.FindByPrimaryKey(clientCode) as WebServiceAccessKey;
		}


		#region IEnumerable<WebServiceAccessKey> Members

		IEnumerator<WebServiceAccessKey> IEnumerable<WebServiceAccessKey>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as WebServiceAccessKey;
			}
		}

		#endregion
		
		private WebServiceAccessKeyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'WebServiceAccessKey' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("WebServiceAccessKey ({ClientCode})")]
	[Serializable]
	public partial class WebServiceAccessKey : esWebServiceAccessKey
	{
		public WebServiceAccessKey()
		{

		}
	
		public WebServiceAccessKey(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return WebServiceAccessKeyMetadata.Meta();
			}
		}
		
		
		
		override protected esWebServiceAccessKeyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new WebServiceAccessKeyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public WebServiceAccessKeyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new WebServiceAccessKeyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(WebServiceAccessKeyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private WebServiceAccessKeyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class WebServiceAccessKeyQuery : esWebServiceAccessKeyQuery
	{
		public WebServiceAccessKeyQuery()
		{

		}		
		
		public WebServiceAccessKeyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "WebServiceAccessKeyQuery";
        }
		
			
	}


	[Serializable]
	public partial class WebServiceAccessKeyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected WebServiceAccessKeyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(WebServiceAccessKeyMetadata.ColumnNames.ClientCode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAccessKeyMetadata.PropertyNames.ClientCode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(WebServiceAccessKeyMetadata.ColumnNames.ClientName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAccessKeyMetadata.PropertyNames.ClientName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WebServiceAccessKeyMetadata.ColumnNames.StartDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WebServiceAccessKeyMetadata.PropertyNames.StartDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WebServiceAccessKeyMetadata.ColumnNames.EndDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = WebServiceAccessKeyMetadata.PropertyNames.EndDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WebServiceAccessKeyMetadata.ColumnNames.AccessKey, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAccessKeyMetadata.PropertyNames.AccessKey;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WebServiceAccessKeyMetadata.ColumnNames.RequestUrl, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAccessKeyMetadata.PropertyNames.RequestUrl;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(WebServiceAccessKeyMetadata.ColumnNames.ResponseUrl, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = WebServiceAccessKeyMetadata.PropertyNames.ResponseUrl;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public WebServiceAccessKeyMetadata Meta()
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
			 public const string ClientCode = "ClientCode";
			 public const string ClientName = "ClientName";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string AccessKey = "AccessKey";
			 public const string RequestUrl = "RequestUrl";
			 public const string ResponseUrl = "ResponseUrl";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ClientCode = "ClientCode";
			 public const string ClientName = "ClientName";
			 public const string StartDate = "StartDate";
			 public const string EndDate = "EndDate";
			 public const string AccessKey = "AccessKey";
			 public const string RequestUrl = "RequestUrl";
			 public const string ResponseUrl = "ResponseUrl";
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
			lock (typeof(WebServiceAccessKeyMetadata))
			{
				if(WebServiceAccessKeyMetadata.mapDelegates == null)
				{
					WebServiceAccessKeyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (WebServiceAccessKeyMetadata.meta == null)
				{
					WebServiceAccessKeyMetadata.meta = new WebServiceAccessKeyMetadata();
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
				

				meta.AddTypeMap("ClientCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ClientName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AccessKey", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestUrl", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResponseUrl", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "WebServiceAccessKey";
				meta.Destination = "WebServiceAccessKey";
				
				meta.spInsert = "proc_WebServiceAccessKeyInsert";				
				meta.spUpdate = "proc_WebServiceAccessKeyUpdate";		
				meta.spDelete = "proc_WebServiceAccessKeyDelete";
				meta.spLoadAll = "proc_WebServiceAccessKeyLoadAll";
				meta.spLoadByPrimaryKey = "proc_WebServiceAccessKeyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private WebServiceAccessKeyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
