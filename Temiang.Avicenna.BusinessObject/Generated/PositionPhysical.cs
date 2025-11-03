/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:22 PM
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
	abstract public class esPositionPhysicalCollection : esEntityCollectionWAuditLog
	{
		public esPositionPhysicalCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PositionPhysicalCollection";
		}

		#region Query Logic
		protected void InitQuery(esPositionPhysicalQuery query)
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
			this.InitQuery(query as esPositionPhysicalQuery);
		}
		#endregion
		
		virtual public PositionPhysical DetachEntity(PositionPhysical entity)
		{
			return base.DetachEntity(entity) as PositionPhysical;
		}
		
		virtual public PositionPhysical AttachEntity(PositionPhysical entity)
		{
			return base.AttachEntity(entity) as PositionPhysical;
		}
		
		virtual public void Combine(PositionPhysicalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PositionPhysical this[int index]
		{
			get
			{
				return base[index] as PositionPhysical;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PositionPhysical);
		}
	}



	[Serializable]
	abstract public class esPositionPhysical : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPositionPhysicalQuery GetDynamicQuery()
		{
			return null;
		}

		public esPositionPhysical()
		{

		}

		public esPositionPhysical(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 positionPhysicalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionPhysicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionPhysicalID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 positionPhysicalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(positionPhysicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(positionPhysicalID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 positionPhysicalID)
		{
			esPositionPhysicalQuery query = this.GetDynamicQuery();
			query.Where(query.PositionPhysicalID == positionPhysicalID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 positionPhysicalID)
		{
			esParameters parms = new esParameters();
			parms.Add("PositionPhysicalID",positionPhysicalID);
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
						case "PositionPhysicalID": this.str.PositionPhysicalID = (string)value; break;							
						case "PositionID": this.str.PositionID = (string)value; break;							
						case "SRPhysicalCharacteristic": this.str.SRPhysicalCharacteristic = (string)value; break;							
						case "SROperandType": this.str.SROperandType = (string)value; break;							
						case "PhysicalValue": this.str.PhysicalValue = (string)value; break;							
						case "SRMeasurementCode": this.str.SRMeasurementCode = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PositionPhysicalID":
						
							if (value == null || value is System.Int32)
								this.PositionPhysicalID = (System.Int32?)value;
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
		/// Maps to PositionPhysical.PositionPhysicalID
		/// </summary>
		virtual public System.Int32? PositionPhysicalID
		{
			get
			{
				return base.GetSystemInt32(PositionPhysicalMetadata.ColumnNames.PositionPhysicalID);
			}
			
			set
			{
				base.SetSystemInt32(PositionPhysicalMetadata.ColumnNames.PositionPhysicalID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPhysical.PositionID
		/// </summary>
		virtual public System.Int32? PositionID
		{
			get
			{
				return base.GetSystemInt32(PositionPhysicalMetadata.ColumnNames.PositionID);
			}
			
			set
			{
				base.SetSystemInt32(PositionPhysicalMetadata.ColumnNames.PositionID, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPhysical.SRPhysicalCharacteristic
		/// </summary>
		virtual public System.String SRPhysicalCharacteristic
		{
			get
			{
				return base.GetSystemString(PositionPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic);
			}
			
			set
			{
				base.SetSystemString(PositionPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPhysical.SROperandType
		/// </summary>
		virtual public System.String SROperandType
		{
			get
			{
				return base.GetSystemString(PositionPhysicalMetadata.ColumnNames.SROperandType);
			}
			
			set
			{
				base.SetSystemString(PositionPhysicalMetadata.ColumnNames.SROperandType, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPhysical.PhysicalValue
		/// </summary>
		virtual public System.String PhysicalValue
		{
			get
			{
				return base.GetSystemString(PositionPhysicalMetadata.ColumnNames.PhysicalValue);
			}
			
			set
			{
				base.SetSystemString(PositionPhysicalMetadata.ColumnNames.PhysicalValue, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPhysical.SRMeasurementCode
		/// </summary>
		virtual public System.String SRMeasurementCode
		{
			get
			{
				return base.GetSystemString(PositionPhysicalMetadata.ColumnNames.SRMeasurementCode);
			}
			
			set
			{
				base.SetSystemString(PositionPhysicalMetadata.ColumnNames.SRMeasurementCode, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPhysical.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PositionPhysicalMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PositionPhysicalMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PositionPhysical.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PositionPhysicalMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PositionPhysicalMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPositionPhysical entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PositionPhysicalID
			{
				get
				{
					System.Int32? data = entity.PositionPhysicalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionPhysicalID = null;
					else entity.PositionPhysicalID = Convert.ToInt32(value);
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
				
			public System.String SRPhysicalCharacteristic
			{
				get
				{
					System.String data = entity.SRPhysicalCharacteristic;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRPhysicalCharacteristic = null;
					else entity.SRPhysicalCharacteristic = Convert.ToString(value);
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
				
			public System.String PhysicalValue
			{
				get
				{
					System.String data = entity.PhysicalValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhysicalValue = null;
					else entity.PhysicalValue = Convert.ToString(value);
				}
			}
				
			public System.String SRMeasurementCode
			{
				get
				{
					System.String data = entity.SRMeasurementCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMeasurementCode = null;
					else entity.SRMeasurementCode = Convert.ToString(value);
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
			

			private esPositionPhysical entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPositionPhysicalQuery query)
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
				throw new Exception("esPositionPhysical can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PositionPhysical : esPositionPhysical
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
	abstract public class esPositionPhysicalQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PositionPhysicalMetadata.Meta();
			}
		}	
		

		public esQueryItem PositionPhysicalID
		{
			get
			{
				return new esQueryItem(this, PositionPhysicalMetadata.ColumnNames.PositionPhysicalID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PositionID
		{
			get
			{
				return new esQueryItem(this, PositionPhysicalMetadata.ColumnNames.PositionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRPhysicalCharacteristic
		{
			get
			{
				return new esQueryItem(this, PositionPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, esSystemType.String);
			}
		} 
		
		public esQueryItem SROperandType
		{
			get
			{
				return new esQueryItem(this, PositionPhysicalMetadata.ColumnNames.SROperandType, esSystemType.String);
			}
		} 
		
		public esQueryItem PhysicalValue
		{
			get
			{
				return new esQueryItem(this, PositionPhysicalMetadata.ColumnNames.PhysicalValue, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMeasurementCode
		{
			get
			{
				return new esQueryItem(this, PositionPhysicalMetadata.ColumnNames.SRMeasurementCode, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PositionPhysicalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PositionPhysicalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PositionPhysicalCollection")]
	public partial class PositionPhysicalCollection : esPositionPhysicalCollection, IEnumerable<PositionPhysical>
	{
		public PositionPhysicalCollection()
		{

		}
		
		public static implicit operator List<PositionPhysical>(PositionPhysicalCollection coll)
		{
			List<PositionPhysical> list = new List<PositionPhysical>();
			
			foreach (PositionPhysical emp in coll)
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
				return  PositionPhysicalMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionPhysicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PositionPhysical(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PositionPhysical();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PositionPhysicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionPhysicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PositionPhysicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PositionPhysical AddNew()
		{
			PositionPhysical entity = base.AddNewEntity() as PositionPhysical;
			
			return entity;
		}

		public PositionPhysical FindByPrimaryKey(System.Int32 positionPhysicalID)
		{
			return base.FindByPrimaryKey(positionPhysicalID) as PositionPhysical;
		}


		#region IEnumerable<PositionPhysical> Members

		IEnumerator<PositionPhysical> IEnumerable<PositionPhysical>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PositionPhysical;
			}
		}

		#endregion
		
		private PositionPhysicalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PositionPhysical' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PositionPhysical ({PositionPhysicalID})")]
	[Serializable]
	public partial class PositionPhysical : esPositionPhysical
	{
		public PositionPhysical()
		{

		}
	
		public PositionPhysical(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PositionPhysicalMetadata.Meta();
			}
		}
		
		
		
		override protected esPositionPhysicalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PositionPhysicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PositionPhysicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PositionPhysicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PositionPhysicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PositionPhysicalQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PositionPhysicalQuery : esPositionPhysicalQuery
	{
		public PositionPhysicalQuery()
		{

		}		
		
		public PositionPhysicalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PositionPhysicalQuery";
        }
		
			
	}


	[Serializable]
	public partial class PositionPhysicalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PositionPhysicalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PositionPhysicalMetadata.ColumnNames.PositionPhysicalID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionPhysicalMetadata.PropertyNames.PositionPhysicalID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPhysicalMetadata.ColumnNames.PositionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PositionPhysicalMetadata.PropertyNames.PositionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPhysicalMetadata.PropertyNames.SRPhysicalCharacteristic;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPhysicalMetadata.ColumnNames.SROperandType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPhysicalMetadata.PropertyNames.SROperandType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPhysicalMetadata.ColumnNames.PhysicalValue, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPhysicalMetadata.PropertyNames.PhysicalValue;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPhysicalMetadata.ColumnNames.SRMeasurementCode, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPhysicalMetadata.PropertyNames.SRMeasurementCode;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPhysicalMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PositionPhysicalMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PositionPhysicalMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PositionPhysicalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PositionPhysicalMetadata Meta()
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
			 public const string PositionPhysicalID = "PositionPhysicalID";
			 public const string PositionID = "PositionID";
			 public const string SRPhysicalCharacteristic = "SRPhysicalCharacteristic";
			 public const string SROperandType = "SROperandType";
			 public const string PhysicalValue = "PhysicalValue";
			 public const string SRMeasurementCode = "SRMeasurementCode";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PositionPhysicalID = "PositionPhysicalID";
			 public const string PositionID = "PositionID";
			 public const string SRPhysicalCharacteristic = "SRPhysicalCharacteristic";
			 public const string SROperandType = "SROperandType";
			 public const string PhysicalValue = "PhysicalValue";
			 public const string SRMeasurementCode = "SRMeasurementCode";
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
			lock (typeof(PositionPhysicalMetadata))
			{
				if(PositionPhysicalMetadata.mapDelegates == null)
				{
					PositionPhysicalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PositionPhysicalMetadata.meta == null)
				{
					PositionPhysicalMetadata.meta = new PositionPhysicalMetadata();
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
				

				meta.AddTypeMap("PositionPhysicalID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PositionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPhysicalCharacteristic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SROperandType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicalValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMeasurementCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PositionPhysical";
				meta.Destination = "PositionPhysical";
				
				meta.spInsert = "proc_PositionPhysicalInsert";				
				meta.spUpdate = "proc_PositionPhysicalUpdate";		
				meta.spDelete = "proc_PositionPhysicalDelete";
				meta.spLoadAll = "proc_PositionPhysicalLoadAll";
				meta.spLoadByPrimaryKey = "proc_PositionPhysicalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PositionPhysicalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
