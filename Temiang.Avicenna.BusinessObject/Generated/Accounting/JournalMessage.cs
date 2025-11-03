/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/30/2016 1:26:28 PM
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
	abstract public class esJournalMessageCollection : esEntityCollectionWAuditLog
	{
		public esJournalMessageCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "JournalMessageCollection";
		}

		#region Query Logic
		protected void InitQuery(esJournalMessageQuery query)
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
			this.InitQuery(query as esJournalMessageQuery);
		}
		#endregion
		
		virtual public JournalMessage DetachEntity(JournalMessage entity)
		{
			return base.DetachEntity(entity) as JournalMessage;
		}
		
		virtual public JournalMessage AttachEntity(JournalMessage entity)
		{
			return base.AttachEntity(entity) as JournalMessage;
		}
		
		virtual public void Combine(JournalMessageCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JournalMessage this[int index]
		{
			get
			{
				return base[index] as JournalMessage;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JournalMessage);
		}
	}



	[Serializable]
	abstract public class esJournalMessage : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJournalMessageQuery GetDynamicQuery()
		{
			return null;
		}

		public esJournalMessage()
		{

		}

		public esJournalMessage(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 journalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalID);
			else
				return LoadByPrimaryKeyStoredProcedure(journalID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 journalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalID);
			else
				return LoadByPrimaryKeyStoredProcedure(journalID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 journalID)
		{
			esJournalMessageQuery query = this.GetDynamicQuery();
			query.Where(query.JournalID == journalID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 journalID)
		{
			esParameters parms = new esParameters();
			parms.Add("JournalID",journalID);
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
						case "JournalID": this.str.JournalID = (string)value; break;							
						case "Message": this.str.Message = (string)value; break;							
						case "AdditionalData": this.str.AdditionalData = (string)value; break;							
						case "CreatedBy": this.str.CreatedBy = (string)value; break;							
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "JournalID":
						
							if (value == null || value is System.Int32)
								this.JournalID = (System.Int32?)value;
							break;
						
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to JournalMessage.JournalID
		/// </summary>
		virtual public System.Int32? JournalID
		{
			get
			{
				return base.GetSystemInt32(JournalMessageMetadata.ColumnNames.JournalID);
			}
			
			set
			{
				base.SetSystemInt32(JournalMessageMetadata.ColumnNames.JournalID, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalMessage.Message
		/// </summary>
		virtual public System.String Message
		{
			get
			{
				return base.GetSystemString(JournalMessageMetadata.ColumnNames.Message);
			}
			
			set
			{
				base.SetSystemString(JournalMessageMetadata.ColumnNames.Message, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalMessage.AdditionalData
		/// </summary>
		virtual public System.String AdditionalData
		{
			get
			{
				return base.GetSystemString(JournalMessageMetadata.ColumnNames.AdditionalData);
			}
			
			set
			{
				base.SetSystemString(JournalMessageMetadata.ColumnNames.AdditionalData, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalMessage.CreatedBy
		/// </summary>
		virtual public System.String CreatedBy
		{
			get
			{
				return base.GetSystemString(JournalMessageMetadata.ColumnNames.CreatedBy);
			}
			
			set
			{
				base.SetSystemString(JournalMessageMetadata.ColumnNames.CreatedBy, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalMessage.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(JournalMessageMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(JournalMessageMetadata.ColumnNames.CreatedDateTime, value);
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
			public esStrings(esJournalMessage entity)
			{
				this.entity = entity;
			}
			
	
			public System.String JournalID
			{
				get
				{
					System.Int32? data = entity.JournalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalID = null;
					else entity.JournalID = Convert.ToInt32(value);
				}
			}
				
			public System.String Message
			{
				get
				{
					System.String data = entity.Message;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Message = null;
					else entity.Message = Convert.ToString(value);
				}
			}
				
			public System.String AdditionalData
			{
				get
				{
					System.String data = entity.AdditionalData;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdditionalData = null;
					else entity.AdditionalData = Convert.ToString(value);
				}
			}
				
			public System.String CreatedBy
			{
				get
				{
					System.String data = entity.CreatedBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedBy = null;
					else entity.CreatedBy = Convert.ToString(value);
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
			

			private esJournalMessage entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJournalMessageQuery query)
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
				throw new Exception("esJournalMessage can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esJournalMessageQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return JournalMessageMetadata.Meta();
			}
		}	
		

		public esQueryItem JournalID
		{
			get
			{
				return new esQueryItem(this, JournalMessageMetadata.ColumnNames.JournalID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Message
		{
			get
			{
				return new esQueryItem(this, JournalMessageMetadata.ColumnNames.Message, esSystemType.String);
			}
		} 
		
		public esQueryItem AdditionalData
		{
			get
			{
				return new esQueryItem(this, JournalMessageMetadata.ColumnNames.AdditionalData, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedBy
		{
			get
			{
				return new esQueryItem(this, JournalMessageMetadata.ColumnNames.CreatedBy, esSystemType.String);
			}
		} 
		
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, JournalMessageMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JournalMessageCollection")]
	public partial class JournalMessageCollection : esJournalMessageCollection, IEnumerable<JournalMessage>
	{
		public JournalMessageCollection()
		{

		}
		
		public static implicit operator List<JournalMessage>(JournalMessageCollection coll)
		{
			List<JournalMessage> list = new List<JournalMessage>();
			
			foreach (JournalMessage emp in coll)
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
				return  JournalMessageMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalMessageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JournalMessage(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JournalMessage();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public JournalMessageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalMessageQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(JournalMessageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public JournalMessage AddNew()
		{
			JournalMessage entity = base.AddNewEntity() as JournalMessage;
			
			return entity;
		}

		public JournalMessage FindByPrimaryKey(System.Int32 journalID)
		{
			return base.FindByPrimaryKey(journalID) as JournalMessage;
		}


		#region IEnumerable<JournalMessage> Members

		IEnumerator<JournalMessage> IEnumerable<JournalMessage>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JournalMessage;
			}
		}

		#endregion
		
		private JournalMessageQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JournalMessage' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("JournalMessage ({JournalID})")]
	[Serializable]
	public partial class JournalMessage : esJournalMessage
	{
		public JournalMessage()
		{

		}
	
		public JournalMessage(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JournalMessageMetadata.Meta();
			}
		}
		
		
		
		override protected esJournalMessageQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalMessageQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public JournalMessageQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalMessageQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(JournalMessageQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private JournalMessageQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class JournalMessageQuery : esJournalMessageQuery
	{
		public JournalMessageQuery()
		{

		}		
		
		public JournalMessageQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "JournalMessageQuery";
        }
		
			
	}


	[Serializable]
	public partial class JournalMessageMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JournalMessageMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(JournalMessageMetadata.ColumnNames.JournalID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalMessageMetadata.PropertyNames.JournalID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalMessageMetadata.ColumnNames.Message, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalMessageMetadata.PropertyNames.Message;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalMessageMetadata.ColumnNames.AdditionalData, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalMessageMetadata.PropertyNames.AdditionalData;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalMessageMetadata.ColumnNames.CreatedBy, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalMessageMetadata.PropertyNames.CreatedBy;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalMessageMetadata.ColumnNames.CreatedDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = JournalMessageMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public JournalMessageMetadata Meta()
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
			 public const string JournalID = "JournalID";
			 public const string Message = "Message";
			 public const string AdditionalData = "AdditionalData";
			 public const string CreatedBy = "CreatedBy";
			 public const string CreatedDateTime = "CreatedDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string JournalID = "JournalID";
			 public const string Message = "Message";
			 public const string AdditionalData = "AdditionalData";
			 public const string CreatedBy = "CreatedBy";
			 public const string CreatedDateTime = "CreatedDateTime";
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
			lock (typeof(JournalMessageMetadata))
			{
				if(JournalMessageMetadata.mapDelegates == null)
				{
					JournalMessageMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JournalMessageMetadata.meta == null)
				{
					JournalMessageMetadata.meta = new JournalMessageMetadata();
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
				

				meta.AddTypeMap("JournalID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Message", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AdditionalData", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "JournalMessage";
				meta.Destination = "JournalMessage";
				
				meta.spInsert = "proc_JournalMessageInsert";				
				meta.spUpdate = "proc_JournalMessageUpdate";		
				meta.spDelete = "proc_JournalMessageDelete";
				meta.spLoadAll = "proc_JournalMessageLoadAll";
				meta.spLoadByPrimaryKey = "proc_JournalMessageLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JournalMessageMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
