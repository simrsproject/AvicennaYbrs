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
	abstract public class esPersonalPhysicalCollection : esEntityCollectionWAuditLog
	{
		public esPersonalPhysicalCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PersonalPhysicalCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalPhysicalQuery query)
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
			this.InitQuery(query as esPersonalPhysicalQuery);
		}
		#endregion
		
		virtual public PersonalPhysical DetachEntity(PersonalPhysical entity)
		{
			return base.DetachEntity(entity) as PersonalPhysical;
		}
		
		virtual public PersonalPhysical AttachEntity(PersonalPhysical entity)
		{
			return base.AttachEntity(entity) as PersonalPhysical;
		}
		
		virtual public void Combine(PersonalPhysicalCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PersonalPhysical this[int index]
		{
			get
			{
				return base[index] as PersonalPhysical;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalPhysical);
		}
	}



	[Serializable]
	abstract public class esPersonalPhysical : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalPhysicalQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalPhysical()
		{

		}

		public esPersonalPhysical(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 personalPhysicalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalPhysicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalPhysicalID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 personalPhysicalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalPhysicalID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalPhysicalID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 personalPhysicalID)
		{
			esPersonalPhysicalQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalPhysicalID == personalPhysicalID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 personalPhysicalID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalPhysicalID",personalPhysicalID);
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
						case "PersonalPhysicalID": this.str.PersonalPhysicalID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "SRPhysicalCharacteristic": this.str.SRPhysicalCharacteristic = (string)value; break;							
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
						case "PersonalPhysicalID":
						
							if (value == null || value is System.Int32)
								this.PersonalPhysicalID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
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
		/// Maps to PersonalPhysical.PersonalPhysicalID
		/// </summary>
		virtual public System.Int32? PersonalPhysicalID
		{
			get
			{
				return base.GetSystemInt32(PersonalPhysicalMetadata.ColumnNames.PersonalPhysicalID);
			}
			
			set
			{
				base.SetSystemInt32(PersonalPhysicalMetadata.ColumnNames.PersonalPhysicalID, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalPhysical.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalPhysicalMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(PersonalPhysicalMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalPhysical.SRPhysicalCharacteristic
		/// </summary>
		virtual public System.String SRPhysicalCharacteristic
		{
			get
			{
				return base.GetSystemString(PersonalPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic);
			}
			
			set
			{
				base.SetSystemString(PersonalPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalPhysical.PhysicalValue
		/// </summary>
		virtual public System.String PhysicalValue
		{
			get
			{
				return base.GetSystemString(PersonalPhysicalMetadata.ColumnNames.PhysicalValue);
			}
			
			set
			{
				base.SetSystemString(PersonalPhysicalMetadata.ColumnNames.PhysicalValue, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalPhysical.SRMeasurementCode
		/// </summary>
		virtual public System.String SRMeasurementCode
		{
			get
			{
				return base.GetSystemString(PersonalPhysicalMetadata.ColumnNames.SRMeasurementCode);
			}
			
			set
			{
				base.SetSystemString(PersonalPhysicalMetadata.ColumnNames.SRMeasurementCode, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalPhysical.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalPhysicalMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PersonalPhysicalMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalPhysical.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalPhysicalMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PersonalPhysicalMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPersonalPhysical entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PersonalPhysicalID
			{
				get
				{
					System.Int32? data = entity.PersonalPhysicalID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalPhysicalID = null;
					else entity.PersonalPhysicalID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
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
			

			private esPersonalPhysical entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalPhysicalQuery query)
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
				throw new Exception("esPersonalPhysical can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PersonalPhysical : esPersonalPhysical
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
	abstract public class esPersonalPhysicalQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PersonalPhysicalMetadata.Meta();
			}
		}	
		

		public esQueryItem PersonalPhysicalID
		{
			get
			{
				return new esQueryItem(this, PersonalPhysicalMetadata.ColumnNames.PersonalPhysicalID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalPhysicalMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRPhysicalCharacteristic
		{
			get
			{
				return new esQueryItem(this, PersonalPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, esSystemType.String);
			}
		} 
		
		public esQueryItem PhysicalValue
		{
			get
			{
				return new esQueryItem(this, PersonalPhysicalMetadata.ColumnNames.PhysicalValue, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMeasurementCode
		{
			get
			{
				return new esQueryItem(this, PersonalPhysicalMetadata.ColumnNames.SRMeasurementCode, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalPhysicalMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalPhysicalMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalPhysicalCollection")]
	public partial class PersonalPhysicalCollection : esPersonalPhysicalCollection, IEnumerable<PersonalPhysical>
	{
		public PersonalPhysicalCollection()
		{

		}
		
		public static implicit operator List<PersonalPhysical>(PersonalPhysicalCollection coll)
		{
			List<PersonalPhysical> list = new List<PersonalPhysical>();
			
			foreach (PersonalPhysical emp in coll)
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
				return  PersonalPhysicalMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalPhysicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalPhysical(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalPhysical();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PersonalPhysicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalPhysicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PersonalPhysicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PersonalPhysical AddNew()
		{
			PersonalPhysical entity = base.AddNewEntity() as PersonalPhysical;
			
			return entity;
		}

		public PersonalPhysical FindByPrimaryKey(System.Int32 personalPhysicalID)
		{
			return base.FindByPrimaryKey(personalPhysicalID) as PersonalPhysical;
		}


		#region IEnumerable<PersonalPhysical> Members

		IEnumerator<PersonalPhysical> IEnumerable<PersonalPhysical>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PersonalPhysical;
			}
		}

		#endregion
		
		private PersonalPhysicalQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalPhysical' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PersonalPhysical ({PersonalPhysicalID})")]
	[Serializable]
	public partial class PersonalPhysical : esPersonalPhysical
	{
		public PersonalPhysical()
		{

		}
	
		public PersonalPhysical(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalPhysicalMetadata.Meta();
			}
		}
		
		
		
		override protected esPersonalPhysicalQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalPhysicalQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PersonalPhysicalQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalPhysicalQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PersonalPhysicalQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PersonalPhysicalQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PersonalPhysicalQuery : esPersonalPhysicalQuery
	{
		public PersonalPhysicalQuery()
		{

		}		
		
		public PersonalPhysicalQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PersonalPhysicalQuery";
        }
		
			
	}


	[Serializable]
	public partial class PersonalPhysicalMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalPhysicalMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalPhysicalMetadata.ColumnNames.PersonalPhysicalID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalPhysicalMetadata.PropertyNames.PersonalPhysicalID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalPhysicalMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalPhysicalMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalPhysicalMetadata.ColumnNames.SRPhysicalCharacteristic, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalPhysicalMetadata.PropertyNames.SRPhysicalCharacteristic;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalPhysicalMetadata.ColumnNames.PhysicalValue, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalPhysicalMetadata.PropertyNames.PhysicalValue;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalPhysicalMetadata.ColumnNames.SRMeasurementCode, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalPhysicalMetadata.PropertyNames.SRMeasurementCode;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalPhysicalMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalPhysicalMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalPhysicalMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalPhysicalMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PersonalPhysicalMetadata Meta()
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
			 public const string PersonalPhysicalID = "PersonalPhysicalID";
			 public const string PersonID = "PersonID";
			 public const string SRPhysicalCharacteristic = "SRPhysicalCharacteristic";
			 public const string PhysicalValue = "PhysicalValue";
			 public const string SRMeasurementCode = "SRMeasurementCode";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PersonalPhysicalID = "PersonalPhysicalID";
			 public const string PersonID = "PersonID";
			 public const string SRPhysicalCharacteristic = "SRPhysicalCharacteristic";
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
			lock (typeof(PersonalPhysicalMetadata))
			{
				if(PersonalPhysicalMetadata.mapDelegates == null)
				{
					PersonalPhysicalMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PersonalPhysicalMetadata.meta == null)
				{
					PersonalPhysicalMetadata.meta = new PersonalPhysicalMetadata();
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
				

				meta.AddTypeMap("PersonalPhysicalID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRPhysicalCharacteristic", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhysicalValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMeasurementCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PersonalPhysical";
				meta.Destination = "PersonalPhysical";
				
				meta.spInsert = "proc_PersonalPhysicalInsert";				
				meta.spUpdate = "proc_PersonalPhysicalUpdate";		
				meta.spDelete = "proc_PersonalPhysicalDelete";
				meta.spLoadAll = "proc_PersonalPhysicalLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalPhysicalLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalPhysicalMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
