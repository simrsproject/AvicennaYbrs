/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/5/2013 2:40:21 AM
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
	abstract public class esEmbalaceCollection : esEntityCollectionWAuditLog
	{
		public esEmbalaceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmbalaceCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmbalaceQuery query)
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
			this.InitQuery(query as esEmbalaceQuery);
		}
		#endregion
		
		virtual public Embalace DetachEntity(Embalace entity)
		{
			return base.DetachEntity(entity) as Embalace;
		}
		
		virtual public Embalace AttachEntity(Embalace entity)
		{
			return base.AttachEntity(entity) as Embalace;
		}
		
		virtual public void Combine(EmbalaceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Embalace this[int index]
		{
			get
			{
				return base[index] as Embalace;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Embalace);
		}
	}



	[Serializable]
	abstract public class esEmbalace : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmbalaceQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmbalace()
		{

		}

		public esEmbalace(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String embalaceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(embalaceID);
			else
				return LoadByPrimaryKeyStoredProcedure(embalaceID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String embalaceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(embalaceID);
			else
				return LoadByPrimaryKeyStoredProcedure(embalaceID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String embalaceID)
		{
			esEmbalaceQuery query = this.GetDynamicQuery();
			query.Where(query.EmbalaceID == embalaceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String embalaceID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmbalaceID",embalaceID);
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
						case "EmbalaceID": this.str.EmbalaceID = (string)value; break;							
						case "EmbalaceName": this.str.EmbalaceName = (string)value; break;							
						case "EmbalaceLabel": this.str.EmbalaceLabel = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "EmbalaceFeeAmount": this.str.EmbalaceFeeAmount = (string)value; break;
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
						
						case "EmbalaceFeeAmount":
						
							if (value == null || value is System.Decimal)
								this.EmbalaceFeeAmount = (System.Decimal?)value;
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
		/// Maps to Embalace.EmbalaceID
		/// </summary>
		virtual public System.String EmbalaceID
		{
			get
			{
				return base.GetSystemString(EmbalaceMetadata.ColumnNames.EmbalaceID);
			}
			
			set
			{
				base.SetSystemString(EmbalaceMetadata.ColumnNames.EmbalaceID, value);
			}
		}
		
		/// <summary>
		/// Maps to Embalace.EmbalaceName
		/// </summary>
		virtual public System.String EmbalaceName
		{
			get
			{
				return base.GetSystemString(EmbalaceMetadata.ColumnNames.EmbalaceName);
			}
			
			set
			{
				base.SetSystemString(EmbalaceMetadata.ColumnNames.EmbalaceName, value);
			}
		}
		
		/// <summary>
		/// Maps to Embalace.EmbalaceLabel
		/// </summary>
		virtual public System.String EmbalaceLabel
		{
			get
			{
				return base.GetSystemString(EmbalaceMetadata.ColumnNames.EmbalaceLabel);
			}
			
			set
			{
				base.SetSystemString(EmbalaceMetadata.ColumnNames.EmbalaceLabel, value);
			}
		}
		
		/// <summary>
		/// Maps to Embalace.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmbalaceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmbalaceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Embalace.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmbalaceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmbalaceMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Embalace.EmbalaceFeeAmount
		/// </summary>
		virtual public System.Decimal? EmbalaceFeeAmount
		{
			get
			{
				return base.GetSystemDecimal(EmbalaceMetadata.ColumnNames.EmbalaceFeeAmount);
			}
			
			set
			{
				base.SetSystemDecimal(EmbalaceMetadata.ColumnNames.EmbalaceFeeAmount, value);
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
			public esStrings(esEmbalace entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmbalaceID
			{
				get
				{
					System.String data = entity.EmbalaceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceID = null;
					else entity.EmbalaceID = Convert.ToString(value);
				}
			}
				
			public System.String EmbalaceName
			{
				get
				{
					System.String data = entity.EmbalaceName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceName = null;
					else entity.EmbalaceName = Convert.ToString(value);
				}
			}
				
			public System.String EmbalaceLabel
			{
				get
				{
					System.String data = entity.EmbalaceLabel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceLabel = null;
					else entity.EmbalaceLabel = Convert.ToString(value);
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
				
			public System.String EmbalaceFeeAmount
			{
				get
				{
					System.Decimal? data = entity.EmbalaceFeeAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmbalaceFeeAmount = null;
					else entity.EmbalaceFeeAmount = Convert.ToDecimal(value);
				}
			}
			

			private esEmbalace entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmbalaceQuery query)
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
				throw new Exception("esEmbalace can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Embalace : esEmbalace
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
	abstract public class esEmbalaceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmbalaceMetadata.Meta();
			}
		}	
		

		public esQueryItem EmbalaceID
		{
			get
			{
				return new esQueryItem(this, EmbalaceMetadata.ColumnNames.EmbalaceID, esSystemType.String);
			}
		} 
		
		public esQueryItem EmbalaceName
		{
			get
			{
				return new esQueryItem(this, EmbalaceMetadata.ColumnNames.EmbalaceName, esSystemType.String);
			}
		} 
		
		public esQueryItem EmbalaceLabel
		{
			get
			{
				return new esQueryItem(this, EmbalaceMetadata.ColumnNames.EmbalaceLabel, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmbalaceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmbalaceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem EmbalaceFeeAmount
		{
			get
			{
				return new esQueryItem(this, EmbalaceMetadata.ColumnNames.EmbalaceFeeAmount, esSystemType.Decimal);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmbalaceCollection")]
	public partial class EmbalaceCollection : esEmbalaceCollection, IEnumerable<Embalace>
	{
		public EmbalaceCollection()
		{

		}
		
		public static implicit operator List<Embalace>(EmbalaceCollection coll)
		{
			List<Embalace> list = new List<Embalace>();
			
			foreach (Embalace emp in coll)
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
				return  EmbalaceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmbalaceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Embalace(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Embalace();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmbalaceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmbalaceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmbalaceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Embalace AddNew()
		{
			Embalace entity = base.AddNewEntity() as Embalace;
			
			return entity;
		}

		public Embalace FindByPrimaryKey(System.String embalaceID)
		{
			return base.FindByPrimaryKey(embalaceID) as Embalace;
		}


		#region IEnumerable<Embalace> Members

		IEnumerator<Embalace> IEnumerable<Embalace>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Embalace;
			}
		}

		#endregion
		
		private EmbalaceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Embalace' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Embalace ({EmbalaceID})")]
	[Serializable]
	public partial class Embalace : esEmbalace
	{
		public Embalace()
		{

		}
	
		public Embalace(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmbalaceMetadata.Meta();
			}
		}
		
		
		
		override protected esEmbalaceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmbalaceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmbalaceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmbalaceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmbalaceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmbalaceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmbalaceQuery : esEmbalaceQuery
	{
		public EmbalaceQuery()
		{

		}		
		
		public EmbalaceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmbalaceQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmbalaceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmbalaceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmbalaceMetadata.ColumnNames.EmbalaceID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = EmbalaceMetadata.PropertyNames.EmbalaceID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(EmbalaceMetadata.ColumnNames.EmbalaceName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmbalaceMetadata.PropertyNames.EmbalaceName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmbalaceMetadata.ColumnNames.EmbalaceLabel, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmbalaceMetadata.PropertyNames.EmbalaceLabel;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmbalaceMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmbalaceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmbalaceMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmbalaceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmbalaceMetadata.ColumnNames.EmbalaceFeeAmount, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = EmbalaceMetadata.PropertyNames.EmbalaceFeeAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmbalaceMetadata Meta()
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
			 public const string EmbalaceID = "EmbalaceID";
			 public const string EmbalaceName = "EmbalaceName";
			 public const string EmbalaceLabel = "EmbalaceLabel";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string EmbalaceFeeAmount = "EmbalaceFeeAmount";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmbalaceID = "EmbalaceID";
			 public const string EmbalaceName = "EmbalaceName";
			 public const string EmbalaceLabel = "EmbalaceLabel";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string EmbalaceFeeAmount = "EmbalaceFeeAmount";
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
			lock (typeof(EmbalaceMetadata))
			{
				if(EmbalaceMetadata.mapDelegates == null)
				{
					EmbalaceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmbalaceMetadata.meta == null)
				{
					EmbalaceMetadata.meta = new EmbalaceMetadata();
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
				

				meta.AddTypeMap("EmbalaceID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmbalaceName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmbalaceLabel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EmbalaceFeeAmount", new esTypeMap("numeric", "System.Decimal"));			
				
				
				
				meta.Source = "Embalace";
				meta.Destination = "Embalace";
				
				meta.spInsert = "proc_EmbalaceInsert";				
				meta.spUpdate = "proc_EmbalaceUpdate";		
				meta.spDelete = "proc_EmbalaceDelete";
				meta.spLoadAll = "proc_EmbalaceLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmbalaceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmbalaceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
