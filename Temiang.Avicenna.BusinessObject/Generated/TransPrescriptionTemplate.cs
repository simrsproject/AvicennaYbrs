/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/30/2016 7:08:31 AM
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
	abstract public class esTransPrescriptionTemplateCollection : esEntityCollectionWAuditLog
	{
		public esTransPrescriptionTemplateCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "TransPrescriptionTemplateCollection";
		}

		#region Query Logic
		protected void InitQuery(esTransPrescriptionTemplateQuery query)
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
			this.InitQuery(query as esTransPrescriptionTemplateQuery);
		}
		#endregion
		
		virtual public TransPrescriptionTemplate DetachEntity(TransPrescriptionTemplate entity)
		{
			return base.DetachEntity(entity) as TransPrescriptionTemplate;
		}
		
		virtual public TransPrescriptionTemplate AttachEntity(TransPrescriptionTemplate entity)
		{
			return base.AttachEntity(entity) as TransPrescriptionTemplate;
		}
		
		virtual public void Combine(TransPrescriptionTemplateCollection collection)
		{
			base.Combine(collection);
		}
		
		new public TransPrescriptionTemplate this[int index]
		{
			get
			{
				return base[index] as TransPrescriptionTemplate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(TransPrescriptionTemplate);
		}
	}



	[Serializable]
	abstract public class esTransPrescriptionTemplate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTransPrescriptionTemplateQuery GetDynamicQuery()
		{
			return null;
		}

		public esTransPrescriptionTemplate()
		{

		}

		public esTransPrescriptionTemplate(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String templateNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateNo);
			else
				return LoadByPrimaryKeyStoredProcedure(templateNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String templateNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(templateNo);
			else
				return LoadByPrimaryKeyStoredProcedure(templateNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String templateNo)
		{
			esTransPrescriptionTemplateQuery query = this.GetDynamicQuery();
			query.Where(query.TemplateNo == templateNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String templateNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TemplateNo",templateNo);
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
						case "TemplateNo": this.str.TemplateNo = (string)value; break;							
						case "TemplateName": this.str.TemplateName = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "LastCreateDateTime": this.str.LastCreateDateTime = (string)value; break;							
						case "LastCreateUserID": this.str.LastCreateUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "LastCreateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastCreateDateTime = (System.DateTime?)value;
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
		/// Maps to TransPrescriptionTemplate.TemplateNo
		/// </summary>
		virtual public System.String TemplateNo
		{
			get
			{
				return base.GetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.TemplateNo);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.TemplateNo, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionTemplate.TemplateName
		/// </summary>
		virtual public System.String TemplateName
		{
			get
			{
				return base.GetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.TemplateName);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.TemplateName, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionTemplate.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionTemplate.LastCreateDateTime
		/// </summary>
		virtual public System.DateTime? LastCreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionTemplateMetadata.ColumnNames.LastCreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionTemplateMetadata.ColumnNames.LastCreateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionTemplate.LastCreateUserID
		/// </summary>
		virtual public System.String LastCreateUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.LastCreateUserID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.LastCreateUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionTemplate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransPrescriptionTemplateMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(TransPrescriptionTemplateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to TransPrescriptionTemplate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(TransPrescriptionTemplateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esTransPrescriptionTemplate entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TemplateNo
			{
				get
				{
					System.String data = entity.TemplateNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateNo = null;
					else entity.TemplateNo = Convert.ToString(value);
				}
			}
				
			public System.String TemplateName
			{
				get
				{
					System.String data = entity.TemplateName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemplateName = null;
					else entity.TemplateName = Convert.ToString(value);
				}
			}
				
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
				
			public System.String LastCreateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastCreateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateDateTime = null;
					else entity.LastCreateDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LastCreateUserID
			{
				get
				{
					System.String data = entity.LastCreateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastCreateUserID = null;
					else entity.LastCreateUserID = Convert.ToString(value);
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
			

			private esTransPrescriptionTemplate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTransPrescriptionTemplateQuery query)
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
				throw new Exception("esTransPrescriptionTemplate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esTransPrescriptionTemplateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionTemplateMetadata.Meta();
			}
		}	
		

		public esQueryItem TemplateNo
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionTemplateMetadata.ColumnNames.TemplateNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TemplateName
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionTemplateMetadata.ColumnNames.TemplateName, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionTemplateMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastCreateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionTemplateMetadata.ColumnNames.LastCreateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastCreateUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionTemplateMetadata.ColumnNames.LastCreateUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionTemplateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, TransPrescriptionTemplateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("TransPrescriptionTemplateCollection")]
	public partial class TransPrescriptionTemplateCollection : esTransPrescriptionTemplateCollection, IEnumerable<TransPrescriptionTemplate>
	{
		public TransPrescriptionTemplateCollection()
		{

		}
		
		public static implicit operator List<TransPrescriptionTemplate>(TransPrescriptionTemplateCollection coll)
		{
			List<TransPrescriptionTemplate> list = new List<TransPrescriptionTemplate>();
			
			foreach (TransPrescriptionTemplate emp in coll)
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
				return  TransPrescriptionTemplateMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new TransPrescriptionTemplate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new TransPrescriptionTemplate();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public TransPrescriptionTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionTemplateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(TransPrescriptionTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public TransPrescriptionTemplate AddNew()
		{
			TransPrescriptionTemplate entity = base.AddNewEntity() as TransPrescriptionTemplate;
			
			return entity;
		}

		public TransPrescriptionTemplate FindByPrimaryKey(System.String templateNo)
		{
			return base.FindByPrimaryKey(templateNo) as TransPrescriptionTemplate;
		}


		#region IEnumerable<TransPrescriptionTemplate> Members

		IEnumerator<TransPrescriptionTemplate> IEnumerable<TransPrescriptionTemplate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as TransPrescriptionTemplate;
			}
		}

		#endregion
		
		private TransPrescriptionTemplateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'TransPrescriptionTemplate' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("TransPrescriptionTemplate ({TemplateNo})")]
	[Serializable]
	public partial class TransPrescriptionTemplate : esTransPrescriptionTemplate
	{
		public TransPrescriptionTemplate()
		{

		}
	
		public TransPrescriptionTemplate(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransPrescriptionTemplateMetadata.Meta();
			}
		}
		
		
		
		override protected esTransPrescriptionTemplateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TransPrescriptionTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public TransPrescriptionTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TransPrescriptionTemplateQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(TransPrescriptionTemplateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private TransPrescriptionTemplateQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class TransPrescriptionTemplateQuery : esTransPrescriptionTemplateQuery
	{
		public TransPrescriptionTemplateQuery()
		{

		}		
		
		public TransPrescriptionTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "TransPrescriptionTemplateQuery";
        }
		
			
	}


	[Serializable]
	public partial class TransPrescriptionTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransPrescriptionTemplateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TransPrescriptionTemplateMetadata.ColumnNames.TemplateNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionTemplateMetadata.PropertyNames.TemplateNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionTemplateMetadata.ColumnNames.TemplateName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionTemplateMetadata.PropertyNames.TemplateName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionTemplateMetadata.ColumnNames.ParamedicID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionTemplateMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionTemplateMetadata.ColumnNames.LastCreateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionTemplateMetadata.PropertyNames.LastCreateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionTemplateMetadata.ColumnNames.LastCreateUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionTemplateMetadata.PropertyNames.LastCreateUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionTemplateMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = TransPrescriptionTemplateMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(TransPrescriptionTemplateMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = TransPrescriptionTemplateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public TransPrescriptionTemplateMetadata Meta()
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
			 public const string TemplateNo = "TemplateNo";
			 public const string TemplateName = "TemplateName";
			 public const string ParamedicID = "ParamedicID";
			 public const string LastCreateDateTime = "LastCreateDateTime";
			 public const string LastCreateUserID = "LastCreateUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TemplateNo = "TemplateNo";
			 public const string TemplateName = "TemplateName";
			 public const string ParamedicID = "ParamedicID";
			 public const string LastCreateDateTime = "LastCreateDateTime";
			 public const string LastCreateUserID = "LastCreateUserID";
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
			lock (typeof(TransPrescriptionTemplateMetadata))
			{
				if(TransPrescriptionTemplateMetadata.mapDelegates == null)
				{
					TransPrescriptionTemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TransPrescriptionTemplateMetadata.meta == null)
				{
					TransPrescriptionTemplateMetadata.meta = new TransPrescriptionTemplateMetadata();
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
				

				meta.AddTypeMap("TemplateNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TemplateName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastCreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastCreateUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "TransPrescriptionTemplate";
				meta.Destination = "TransPrescriptionTemplate";
				
				meta.spInsert = "proc_TransPrescriptionTemplateInsert";				
				meta.spUpdate = "proc_TransPrescriptionTemplateUpdate";		
				meta.spDelete = "proc_TransPrescriptionTemplateDelete";
				meta.spLoadAll = "proc_TransPrescriptionTemplateLoadAll";
				meta.spLoadByPrimaryKey = "proc_TransPrescriptionTemplateLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransPrescriptionTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
