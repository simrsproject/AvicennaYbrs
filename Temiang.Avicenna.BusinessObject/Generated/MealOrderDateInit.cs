/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/13/2014 10:00:28 AM
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
	abstract public class esMealOrderDateInitCollection : esEntityCollectionWAuditLog
	{
		public esMealOrderDateInitCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MealOrderDateInitCollection";
		}

		#region Query Logic
		protected void InitQuery(esMealOrderDateInitQuery query)
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
			this.InitQuery(query as esMealOrderDateInitQuery);
		}
		#endregion
		
		virtual public MealOrderDateInit DetachEntity(MealOrderDateInit entity)
		{
			return base.DetachEntity(entity) as MealOrderDateInit;
		}
		
		virtual public MealOrderDateInit AttachEntity(MealOrderDateInit entity)
		{
			return base.AttachEntity(entity) as MealOrderDateInit;
		}
		
		virtual public void Combine(MealOrderDateInitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MealOrderDateInit this[int index]
		{
			get
			{
				return base[index] as MealOrderDateInit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MealOrderDateInit);
		}
	}



	[Serializable]
	abstract public class esMealOrderDateInit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMealOrderDateInitQuery GetDynamicQuery()
		{
			return null;
		}

		public esMealOrderDateInit()
		{

		}

		public esMealOrderDateInit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.DateTime mealOrderDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(mealOrderDate);
			else
				return LoadByPrimaryKeyStoredProcedure(mealOrderDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime mealOrderDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(mealOrderDate);
			else
				return LoadByPrimaryKeyStoredProcedure(mealOrderDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.DateTime mealOrderDate)
		{
			esMealOrderDateInitQuery query = this.GetDynamicQuery();
			query.Where(query.MealOrderDate == mealOrderDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.DateTime mealOrderDate)
		{
			esParameters parms = new esParameters();
			parms.Add("MealOrderDate",mealOrderDate);
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
						case "MealOrderDate": this.str.MealOrderDate = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MealOrderDate":
						
							if (value == null || value is System.DateTime)
								this.MealOrderDate = (System.DateTime?)value;
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
		/// Maps to MealOrderDateInit.MealOrderDate
		/// </summary>
		virtual public System.DateTime? MealOrderDate
		{
			get
			{
				return base.GetSystemDateTime(MealOrderDateInitMetadata.ColumnNames.MealOrderDate);
			}
			
			set
			{
				base.SetSystemDateTime(MealOrderDateInitMetadata.ColumnNames.MealOrderDate, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderDateInit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MealOrderDateInitMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MealOrderDateInitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MealOrderDateInit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MealOrderDateInitMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MealOrderDateInitMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMealOrderDateInit entity)
			{
				this.entity = entity;
			}
			
	
			public System.String MealOrderDate
			{
				get
				{
					System.DateTime? data = entity.MealOrderDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MealOrderDate = null;
					else entity.MealOrderDate = Convert.ToDateTime(value);
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
			

			private esMealOrderDateInit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMealOrderDateInitQuery query)
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
				throw new Exception("esMealOrderDateInit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MealOrderDateInit : esMealOrderDateInit
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
	abstract public class esMealOrderDateInitQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MealOrderDateInitMetadata.Meta();
			}
		}	
		

		public esQueryItem MealOrderDate
		{
			get
			{
				return new esQueryItem(this, MealOrderDateInitMetadata.ColumnNames.MealOrderDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MealOrderDateInitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MealOrderDateInitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MealOrderDateInitCollection")]
	public partial class MealOrderDateInitCollection : esMealOrderDateInitCollection, IEnumerable<MealOrderDateInit>
	{
		public MealOrderDateInitCollection()
		{

		}
		
		public static implicit operator List<MealOrderDateInit>(MealOrderDateInitCollection coll)
		{
			List<MealOrderDateInit> list = new List<MealOrderDateInit>();
			
			foreach (MealOrderDateInit emp in coll)
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
				return  MealOrderDateInitMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealOrderDateInitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MealOrderDateInit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MealOrderDateInit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MealOrderDateInitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealOrderDateInitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MealOrderDateInitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MealOrderDateInit AddNew()
		{
			MealOrderDateInit entity = base.AddNewEntity() as MealOrderDateInit;
			
			return entity;
		}

		public MealOrderDateInit FindByPrimaryKey(System.DateTime mealOrderDate)
		{
			return base.FindByPrimaryKey(mealOrderDate) as MealOrderDateInit;
		}


		#region IEnumerable<MealOrderDateInit> Members

		IEnumerator<MealOrderDateInit> IEnumerable<MealOrderDateInit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MealOrderDateInit;
			}
		}

		#endregion
		
		private MealOrderDateInitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MealOrderDateInit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MealOrderDateInit ({MealOrderDate})")]
	[Serializable]
	public partial class MealOrderDateInit : esMealOrderDateInit
	{
		public MealOrderDateInit()
		{

		}
	
		public MealOrderDateInit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MealOrderDateInitMetadata.Meta();
			}
		}
		
		
		
		override protected esMealOrderDateInitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MealOrderDateInitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MealOrderDateInitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MealOrderDateInitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MealOrderDateInitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MealOrderDateInitQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MealOrderDateInitQuery : esMealOrderDateInitQuery
	{
		public MealOrderDateInitQuery()
		{

		}		
		
		public MealOrderDateInitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MealOrderDateInitQuery";
        }
		
			
	}


	[Serializable]
	public partial class MealOrderDateInitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MealOrderDateInitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MealOrderDateInitMetadata.ColumnNames.MealOrderDate, 0, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderDateInitMetadata.PropertyNames.MealOrderDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderDateInitMetadata.ColumnNames.LastUpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MealOrderDateInitMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MealOrderDateInitMetadata.ColumnNames.LastUpdateByUserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MealOrderDateInitMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MealOrderDateInitMetadata Meta()
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
			 public const string MealOrderDate = "MealOrderDate";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string MealOrderDate = "MealOrderDate";
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
			lock (typeof(MealOrderDateInitMetadata))
			{
				if(MealOrderDateInitMetadata.mapDelegates == null)
				{
					MealOrderDateInitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MealOrderDateInitMetadata.meta == null)
				{
					MealOrderDateInitMetadata.meta = new MealOrderDateInitMetadata();
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
				

				meta.AddTypeMap("MealOrderDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MealOrderDateInit";
				meta.Destination = "MealOrderDateInit";
				
				meta.spInsert = "proc_MealOrderDateInitInsert";				
				meta.spUpdate = "proc_MealOrderDateInitUpdate";		
				meta.spDelete = "proc_MealOrderDateInitDelete";
				meta.spLoadAll = "proc_MealOrderDateInitLoadAll";
				meta.spLoadByPrimaryKey = "proc_MealOrderDateInitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MealOrderDateInitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
