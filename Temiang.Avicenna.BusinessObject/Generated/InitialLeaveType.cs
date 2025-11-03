/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:16 PM
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
	abstract public class esInitialLeaveTypeCollection : esEntityCollectionWAuditLog
	{
		public esInitialLeaveTypeCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "InitialLeaveTypeCollection";
		}

		#region Query Logic
		protected void InitQuery(esInitialLeaveTypeQuery query)
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
			this.InitQuery(query as esInitialLeaveTypeQuery);
		}
		#endregion
		
		virtual public InitialLeaveType DetachEntity(InitialLeaveType entity)
		{
			return base.DetachEntity(entity) as InitialLeaveType;
		}
		
		virtual public InitialLeaveType AttachEntity(InitialLeaveType entity)
		{
			return base.AttachEntity(entity) as InitialLeaveType;
		}
		
		virtual public void Combine(InitialLeaveTypeCollection collection)
		{
			base.Combine(collection);
		}
		
		new public InitialLeaveType this[int index]
		{
			get
			{
				return base[index] as InitialLeaveType;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InitialLeaveType);
		}
	}



	[Serializable]
	abstract public class esInitialLeaveType : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInitialLeaveTypeQuery GetDynamicQuery()
		{
			return null;
		}

		public esInitialLeaveType()
		{

		}

		public esInitialLeaveType(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 initialLeaveTypeID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(initialLeaveTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(initialLeaveTypeID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 initialLeaveTypeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(initialLeaveTypeID);
			else
				return LoadByPrimaryKeyStoredProcedure(initialLeaveTypeID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 initialLeaveTypeID)
		{
			esInitialLeaveTypeQuery query = this.GetDynamicQuery();
			query.Where(query.InitialLeaveTypeID == initialLeaveTypeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 initialLeaveTypeID)
		{
			esParameters parms = new esParameters();
			parms.Add("InitialLeaveTypeID",initialLeaveTypeID);
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
						case "InitialLeaveTypeID": this.str.InitialLeaveTypeID = (string)value; break;							
						case "LeaveTypeName": this.str.LeaveTypeName = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "InitialLeaveTypeID":
						
							if (value == null || value is System.Int32)
								this.InitialLeaveTypeID = (System.Int32?)value;
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
		/// Maps to InitialLeaveType.InitialLeaveTypeID
		/// </summary>
		virtual public System.Int32? InitialLeaveTypeID
		{
			get
			{
				return base.GetSystemInt32(InitialLeaveTypeMetadata.ColumnNames.InitialLeaveTypeID);
			}
			
			set
			{
				base.SetSystemInt32(InitialLeaveTypeMetadata.ColumnNames.InitialLeaveTypeID, value);
			}
		}
		
		/// <summary>
		/// Maps to InitialLeaveType.LeaveTypeName
		/// </summary>
		virtual public System.String LeaveTypeName
		{
			get
			{
				return base.GetSystemString(InitialLeaveTypeMetadata.ColumnNames.LeaveTypeName);
			}
			
			set
			{
				base.SetSystemString(InitialLeaveTypeMetadata.ColumnNames.LeaveTypeName, value);
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
			public esStrings(esInitialLeaveType entity)
			{
				this.entity = entity;
			}
			
	
			public System.String InitialLeaveTypeID
			{
				get
				{
					System.Int32? data = entity.InitialLeaveTypeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InitialLeaveTypeID = null;
					else entity.InitialLeaveTypeID = Convert.ToInt32(value);
				}
			}
				
			public System.String LeaveTypeName
			{
				get
				{
					System.String data = entity.LeaveTypeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LeaveTypeName = null;
					else entity.LeaveTypeName = Convert.ToString(value);
				}
			}
			

			private esInitialLeaveType entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInitialLeaveTypeQuery query)
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
				throw new Exception("esInitialLeaveType can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class InitialLeaveType : esInitialLeaveType
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
	abstract public class esInitialLeaveTypeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return InitialLeaveTypeMetadata.Meta();
			}
		}	
		

		public esQueryItem InitialLeaveTypeID
		{
			get
			{
				return new esQueryItem(this, InitialLeaveTypeMetadata.ColumnNames.InitialLeaveTypeID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LeaveTypeName
		{
			get
			{
				return new esQueryItem(this, InitialLeaveTypeMetadata.ColumnNames.LeaveTypeName, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InitialLeaveTypeCollection")]
	public partial class InitialLeaveTypeCollection : esInitialLeaveTypeCollection, IEnumerable<InitialLeaveType>
	{
		public InitialLeaveTypeCollection()
		{

		}
		
		public static implicit operator List<InitialLeaveType>(InitialLeaveTypeCollection coll)
		{
			List<InitialLeaveType> list = new List<InitialLeaveType>();
			
			foreach (InitialLeaveType emp in coll)
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
				return  InitialLeaveTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InitialLeaveTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InitialLeaveType(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InitialLeaveType();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public InitialLeaveTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InitialLeaveTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(InitialLeaveTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public InitialLeaveType AddNew()
		{
			InitialLeaveType entity = base.AddNewEntity() as InitialLeaveType;
			
			return entity;
		}

		public InitialLeaveType FindByPrimaryKey(System.Int32 initialLeaveTypeID)
		{
			return base.FindByPrimaryKey(initialLeaveTypeID) as InitialLeaveType;
		}


		#region IEnumerable<InitialLeaveType> Members

		IEnumerator<InitialLeaveType> IEnumerable<InitialLeaveType>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as InitialLeaveType;
			}
		}

		#endregion
		
		private InitialLeaveTypeQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InitialLeaveType' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("InitialLeaveType ({InitialLeaveTypeID})")]
	[Serializable]
	public partial class InitialLeaveType : esInitialLeaveType
	{
		public InitialLeaveType()
		{

		}
	
		public InitialLeaveType(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InitialLeaveTypeMetadata.Meta();
			}
		}
		
		
		
		override protected esInitialLeaveTypeQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InitialLeaveTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public InitialLeaveTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InitialLeaveTypeQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(InitialLeaveTypeQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private InitialLeaveTypeQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class InitialLeaveTypeQuery : esInitialLeaveTypeQuery
	{
		public InitialLeaveTypeQuery()
		{

		}		
		
		public InitialLeaveTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "InitialLeaveTypeQuery";
        }
		
			
	}


	[Serializable]
	public partial class InitialLeaveTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InitialLeaveTypeMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InitialLeaveTypeMetadata.ColumnNames.InitialLeaveTypeID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InitialLeaveTypeMetadata.PropertyNames.InitialLeaveTypeID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(InitialLeaveTypeMetadata.ColumnNames.LeaveTypeName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InitialLeaveTypeMetadata.PropertyNames.LeaveTypeName;
			c.CharacterMaxLength = 70;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public InitialLeaveTypeMetadata Meta()
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
			 public const string InitialLeaveTypeID = "InitialLeaveTypeID";
			 public const string LeaveTypeName = "LeaveTypeName";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string InitialLeaveTypeID = "InitialLeaveTypeID";
			 public const string LeaveTypeName = "LeaveTypeName";
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
			lock (typeof(InitialLeaveTypeMetadata))
			{
				if(InitialLeaveTypeMetadata.mapDelegates == null)
				{
					InitialLeaveTypeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (InitialLeaveTypeMetadata.meta == null)
				{
					InitialLeaveTypeMetadata.meta = new InitialLeaveTypeMetadata();
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
				

				meta.AddTypeMap("InitialLeaveTypeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LeaveTypeName", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "InitialLeaveType";
				meta.Destination = "InitialLeaveType";
				
				meta.spInsert = "proc_InitialLeaveTypeInsert";				
				meta.spUpdate = "proc_InitialLeaveTypeUpdate";		
				meta.spDelete = "proc_InitialLeaveTypeDelete";
				meta.spLoadAll = "proc_InitialLeaveTypeLoadAll";
				meta.spLoadByPrimaryKey = "proc_InitialLeaveTypeLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InitialLeaveTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
