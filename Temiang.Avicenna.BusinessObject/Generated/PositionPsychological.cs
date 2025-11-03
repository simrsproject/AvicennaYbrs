/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:23 PM
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
	abstract public class esPositionPsychologicalCollection : esEntityCollectionWAuditLog
	{
		public esPositionPsychologicalCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PositionPsychologicalCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionPsychologicalQuery query)
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
			this.InitQuery(query as esPositionPsychologicalQuery);
		}
		#endregion
		
		virtual public PositionPsychological DetachEntity(PositionPsychological entity)
		{
			return base.DetachEntity(entity) as PositionPsychological;
		}
		
		virtual public PositionPsychological AttachEntity(PositionPsychological entity)
		{
			return base.AttachEntity(entity) as PositionPsychological;
		}
		
		virtual public void Combine(PositionPsychologicalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PositionPsychological this[int index]
		{
			get
			{
				return base[index] as PositionPsychological;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionPsychological);
		}
	}



	[Serializable]
	abstract public class esPositionPsychological : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionPsychologicalQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionPsychological()
		{

		}

		public esPositionPsychological(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 positionPsychologicalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionPsychologicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionPsychologicalID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 positionPsychologicalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionPsychologicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionPsychologicalID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 positionPsychologicalID)
		{
			esPositionPsychologicalQuery query = this.GetDynamicQuery();
			query.Where(query.PositionPsychologicalID == positionPsychologicalID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 positionPsychologicalID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionPsychologicalID",positionPsychologicalID);
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
						case "PositionPsychologicalID": this.str.PositionPsychologicalID = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "SRPsychological": this.str.SRPsychological = (string)value; break;							
						case "SROperandType": this.str.SROperandType = (string)value; break;							
						case "PsychologicalValue": this.str.PsychologicalValue = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PositionPsychologicalID":
						
							if (value == null || value is System.Int32)
								this.PositionPsychologicalID = (System.Int32?)value;
							break;
						
						case "PositionID":
						
							if (value == null || value is System.Int32)
								this.PositionID = (System.Int32?)value;
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
		/// Maps to PositionPsychological.PositionPsychologicalID
		/// </summary>
		virtual public System.Int32? PositionPsychologicalID
		{
			get
			{
				return base.GetSystemInt32(PositionPsychologicalMetadata.ColumnNames.PositionPsychologicalID);
			}
			
			set
			{
				base.SetSystemInt32(PositionPsychologicalMetadata.ColumnNames.PositionPsychologicalID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPsychological.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionPsychologicalMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(PositionPsychologicalMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPsychological.SRPsychological
		/// </summary>
		virtual public System.String SRPsychological
		{
			get
			{
				return base.GetSystemString(PositionPsychologicalMetadata.ColumnNames.SRPsychological);
			}
			
			set
			{
				base.SetSystemString(PositionPsychologicalMetadata.ColumnNames.SRPsychological, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPsychological.SROperandType
		/// </summary>
		virtual public System.String SROperandType
		{
			get
			{
				return base.GetSystemString(PositionPsychologicalMetadata.ColumnNames.SROperandType);
			}
			
			set
			{
				base.SetSystemString(PositionPsychologicalMetadata.ColumnNames.SROperandType, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPsychological.PsychologicalValue
		/// </summary>
		virtual public System.String PsychologicalValue
		{
			get
			{
				return base.GetSystemString(PositionPsychologicalMetadata.ColumnNames.PsychologicalValue);
			}
			
			set
			{
				base.SetSystemString(PositionPsychologicalMetadata.ColumnNames.PsychologicalValue, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPsychological.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionPsychologicalMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PositionPsychologicalMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPsychological.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionPsychologicalMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PositionPsychologicalMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionPsychological entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PositionPsychologicalID
			{
				get
				{
					System.Int32? data = entity.PositionPsychologicalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionPsychologicalID = null;
					else entity.PositionPsychologicalID = Convert.ToInt32(value);
				}
			}
				
			public System.String PositionID
			{
				get
				{
					System.Int32? data = entity.PositionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionID = null;
					else entity.PositionID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRPsychological
			{
				get
				{
					System.String data = entity.SRPsychological;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPsychological = null;
					else entity.SRPsychological = Convert.ToString(value);
				}
			}
				
			public System.String SROperandType
			{
				get
				{
					System.String data = entity.SROperandType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SROperandType = null;
					else entity.SROperandType = Convert.ToString(value);
				}
			}
				
			public System.String PsychologicalValue
			{
				get
				{
					System.String data = entity.PsychologicalValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PsychologicalValue = null;
					else entity.PsychologicalValue = Convert.ToString(value);
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
			

			private esPositionPsychological entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionPsychologicalQuery query)
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
				throw new Exception("esPositionPsychological can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PositionPsychological : esPositionPsychological
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
	abstract public class esPositionPsychologicalQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PositionPsychologicalMetadata.Meta();
			}
		}	
		

		public esQueryItem PositionPsychologicalID
		{
			get
			{
				return new esQueryItem(this, PositionPsychologicalMetadata.ColumnNames.PositionPsychologicalID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionPsychologicalMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRPsychological
		{
			get
			{
				return new esQueryItem(this, PositionPsychologicalMetadata.ColumnNames.SRPsychological, esSystemType.String);
			}
		} 
		
		public esQueryItem SROperandType
		{
			get
			{
				return new esQueryItem(this, PositionPsychologicalMetadata.ColumnNames.SROperandType, esSystemType.String);
			}
		} 
		
		public esQueryItem PsychologicalValue
		{
			get
			{
				return new esQueryItem(this, PositionPsychologicalMetadata.ColumnNames.PsychologicalValue, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionPsychologicalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionPsychologicalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionPsychologicalCollection")]
	public partial class PositionPsychologicalCollection : esPositionPsychologicalCollection, IEnumerable<PositionPsychological>
	{
		public PositionPsychologicalCollection()
		{

		}
		
		public static implicit operator List<PositionPsychological>(PositionPsychologicalCollection coll)
		{
			List<PositionPsychological> list = new List<PositionPsychological>();
			
			foreach (PositionPsychological emp in coll)
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
				return  PositionPsychologicalMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionPsychologicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionPsychological(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionPsychological();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PositionPsychologicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionPsychologicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PositionPsychologicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PositionPsychological AddNew()
		{
			PositionPsychological entity = base.AddNewEntity() as PositionPsychological;
			
			return entity;
		}

		public PositionPsychological FindByPrimaryKey(System.Int32 positionPsychologicalID)
		{
			return base.FindByPrimaryKey(positionPsychologicalID) as PositionPsychological;
		}


		#region IEnumerable<PositionPsychological> Members

		IEnumerator<PositionPsychological> IEnumerable<PositionPsychological>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PositionPsychological;
			}
		}

		#endregion
		
		private PositionPsychologicalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionPsychological' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PositionPsychological ({PositionPsychologicalID})")]
	[Serializable]
	public partial class PositionPsychological : esPositionPsychological
	{
		public PositionPsychological()
		{

		}
	
		public PositionPsychological(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionPsychologicalMetadata.Meta();
			}
		}
		
		
		
		override protected esPositionPsychologicalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionPsychologicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PositionPsychologicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionPsychologicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PositionPsychologicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PositionPsychologicalQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PositionPsychologicalQuery : esPositionPsychologicalQuery
	{
		public PositionPsychologicalQuery()
		{

		}		
		
		public PositionPsychologicalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PositionPsychologicalQuery";
        }
		
			
	}


	[Serializable]
	public partial class PositionPsychologicalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionPsychologicalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionPsychologicalMetadata.ColumnNames.PositionPsychologicalID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionPsychologicalMetadata.PropertyNames.PositionPsychologicalID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPsychologicalMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionPsychologicalMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPsychologicalMetadata.ColumnNames.SRPsychological, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPsychologicalMetadata.PropertyNames.SRPsychological;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPsychologicalMetadata.ColumnNames.SROperandType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPsychologicalMetadata.PropertyNames.SROperandType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPsychologicalMetadata.ColumnNames.PsychologicalValue, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPsychologicalMetadata.PropertyNames.PsychologicalValue;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPsychologicalMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionPsychologicalMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPsychologicalMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPsychologicalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PositionPsychologicalMetadata Meta()
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
			 public const string PositionPsychologicalID = "PositionPsychologicalID";
			 public const string PositionID = "PositionID";
			 public const string SRPsychological = "SRPsychological";
			 public const string SROperandType = "SROperandType";
			 public const string PsychologicalValue = "PsychologicalValue";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PositionPsychologicalID = "PositionPsychologicalID";
			 public const string PositionID = "PositionID";
			 public const string SRPsychological = "SRPsychological";
			 public const string SROperandType = "SROperandType";
			 public const string PsychologicalValue = "PsychologicalValue";
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
			lock (typeof(PositionPsychologicalMetadata))
			{
				if(PositionPsychologicalMetadata.mapDelegates == null)
				{
					PositionPsychologicalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PositionPsychologicalMetadata.meta == null)
				{
					PositionPsychologicalMetadata.meta = new PositionPsychologicalMetadata();
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
				

				meta.AddTypeMap("PositionPsychologicalID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPsychological", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROperandType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PsychologicalValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PositionPsychological";
				meta.Destination = "PositionPsychological";
				
				meta.spInsert = "proc_PositionPsychologicalInsert";				
				meta.spUpdate = "proc_PositionPsychologicalUpdate";		
				meta.spDelete = "proc_PositionPsychologicalDelete";
				meta.spLoadAll = "proc_PositionPsychologicalLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionPsychologicalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionPsychologicalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
