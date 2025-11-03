/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:21 PM
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
	abstract public class esPersonalContactCollection : esEntityCollectionWAuditLog
	{
		public esPersonalContactCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PersonalContactCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalContactQuery query)
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
			this.InitQuery(query as esPersonalContactQuery);
		}
		#endregion
		
		virtual public PersonalContact DetachEntity(PersonalContact entity)
		{
			return base.DetachEntity(entity) as PersonalContact;
		}
		
		virtual public PersonalContact AttachEntity(PersonalContact entity)
		{
			return base.AttachEntity(entity) as PersonalContact;
		}
		
		virtual public void Combine(PersonalContactCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PersonalContact this[int index]
		{
			get
			{
				return base[index] as PersonalContact;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalContact);
		}
	}



	[Serializable]
	abstract public class esPersonalContact : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalContactQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalContact()
		{

		}

		public esPersonalContact(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 personalContactID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalContactID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalContactID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 personalContactID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalContactID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalContactID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 personalContactID)
		{
			esPersonalContactQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalContactID == personalContactID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 personalContactID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalContactID",personalContactID);
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
						case "PersonalContactID": this.str.PersonalContactID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "SRContactType": this.str.SRContactType = (string)value; break;							
						case "ContactValue": this.str.ContactValue = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PersonalContactID":
						
							if (value == null || value is System.Int32)
								this.PersonalContactID = (System.Int32?)value;
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
		/// Maps to PersonalContact.PersonalContactID
		/// </summary>
		virtual public System.Int32? PersonalContactID
		{
			get
			{
				return base.GetSystemInt32(PersonalContactMetadata.ColumnNames.PersonalContactID);
			}
			
			set
			{
				base.SetSystemInt32(PersonalContactMetadata.ColumnNames.PersonalContactID, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalContact.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalContactMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(PersonalContactMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalContact.SRContactType
		/// </summary>
		virtual public System.String SRContactType
		{
			get
			{
				return base.GetSystemString(PersonalContactMetadata.ColumnNames.SRContactType);
			}
			
			set
			{
				base.SetSystemString(PersonalContactMetadata.ColumnNames.SRContactType, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalContact.ContactValue
		/// </summary>
		virtual public System.String ContactValue
		{
			get
			{
				return base.GetSystemString(PersonalContactMetadata.ColumnNames.ContactValue);
			}
			
			set
			{
				base.SetSystemString(PersonalContactMetadata.ColumnNames.ContactValue, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalContact.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalContactMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PersonalContactMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to PersonalContact.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalContactMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PersonalContactMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPersonalContact entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PersonalContactID
			{
				get
				{
					System.Int32? data = entity.PersonalContactID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalContactID = null;
					else entity.PersonalContactID = Convert.ToInt32(value);
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
				
			public System.String SRContactType
			{
				get
				{
					System.String data = entity.SRContactType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRContactType = null;
					else entity.SRContactType = Convert.ToString(value);
				}
			}
				
			public System.String ContactValue
			{
				get
				{
					System.String data = entity.ContactValue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactValue = null;
					else entity.ContactValue = Convert.ToString(value);
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
			

			private esPersonalContact entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalContactQuery query)
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
				throw new Exception("esPersonalContact can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PersonalContact : esPersonalContact
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
	abstract public class esPersonalContactQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PersonalContactMetadata.Meta();
			}
		}	
		

		public esQueryItem PersonalContactID
		{
			get
			{
				return new esQueryItem(this, PersonalContactMetadata.ColumnNames.PersonalContactID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalContactMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRContactType
		{
			get
			{
				return new esQueryItem(this, PersonalContactMetadata.ColumnNames.SRContactType, esSystemType.String);
			}
		} 
		
		public esQueryItem ContactValue
		{
			get
			{
				return new esQueryItem(this, PersonalContactMetadata.ColumnNames.ContactValue, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalContactMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalContactMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalContactCollection")]
	public partial class PersonalContactCollection : esPersonalContactCollection, IEnumerable<PersonalContact>
	{
		public PersonalContactCollection()
		{

		}
		
		public static implicit operator List<PersonalContact>(PersonalContactCollection coll)
		{
			List<PersonalContact> list = new List<PersonalContact>();
			
			foreach (PersonalContact emp in coll)
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
				return  PersonalContactMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalContactQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalContact(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalContact();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PersonalContactQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalContactQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PersonalContactQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PersonalContact AddNew()
		{
			PersonalContact entity = base.AddNewEntity() as PersonalContact;
			
			return entity;
		}

		public PersonalContact FindByPrimaryKey(System.Int32 personalContactID)
		{
			return base.FindByPrimaryKey(personalContactID) as PersonalContact;
		}


		#region IEnumerable<PersonalContact> Members

		IEnumerator<PersonalContact> IEnumerable<PersonalContact>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PersonalContact;
			}
		}

		#endregion
		
		private PersonalContactQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalContact' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PersonalContact ({PersonalContactID})")]
	[Serializable]
	public partial class PersonalContact : esPersonalContact
	{
		public PersonalContact()
		{

		}
	
		public PersonalContact(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalContactMetadata.Meta();
			}
		}
		
		
		
		override protected esPersonalContactQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalContactQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PersonalContactQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalContactQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PersonalContactQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PersonalContactQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PersonalContactQuery : esPersonalContactQuery
	{
		public PersonalContactQuery()
		{

		}		
		
		public PersonalContactQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PersonalContactQuery";
        }
		
			
	}


	[Serializable]
	public partial class PersonalContactMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalContactMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalContactMetadata.ColumnNames.PersonalContactID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalContactMetadata.PropertyNames.PersonalContactID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalContactMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalContactMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalContactMetadata.ColumnNames.SRContactType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalContactMetadata.PropertyNames.SRContactType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalContactMetadata.ColumnNames.ContactValue, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalContactMetadata.PropertyNames.ContactValue;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalContactMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalContactMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(PersonalContactMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalContactMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PersonalContactMetadata Meta()
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
			 public const string PersonalContactID = "PersonalContactID";
			 public const string PersonID = "PersonID";
			 public const string SRContactType = "SRContactType";
			 public const string ContactValue = "ContactValue";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PersonalContactID = "PersonalContactID";
			 public const string PersonID = "PersonID";
			 public const string SRContactType = "SRContactType";
			 public const string ContactValue = "ContactValue";
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
			lock (typeof(PersonalContactMetadata))
			{
				if(PersonalContactMetadata.mapDelegates == null)
				{
					PersonalContactMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PersonalContactMetadata.meta == null)
				{
					PersonalContactMetadata.meta = new PersonalContactMetadata();
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
				

				meta.AddTypeMap("PersonalContactID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRContactType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContactValue", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "PersonalContact";
				meta.Destination = "PersonalContact";
				
				meta.spInsert = "proc_PersonalContactInsert";				
				meta.spUpdate = "proc_PersonalContactUpdate";		
				meta.spDelete = "proc_PersonalContactDelete";
				meta.spLoadAll = "proc_PersonalContactLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalContactLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalContactMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
