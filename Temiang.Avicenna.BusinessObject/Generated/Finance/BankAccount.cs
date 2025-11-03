/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:12 PM
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
	abstract public class esBankAccountCollection : esEntityCollectionWAuditLog
	{
		public esBankAccountCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BankAccountCollection";
		}

		#region Query Logic
		protected void InitQuery(esBankAccountQuery query)
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
			this.InitQuery(query as esBankAccountQuery);
		}
		#endregion
		
		virtual public BankAccount DetachEntity(BankAccount entity)
		{
			return base.DetachEntity(entity) as BankAccount;
		}
		
		virtual public BankAccount AttachEntity(BankAccount entity)
		{
			return base.AttachEntity(entity) as BankAccount;
		}
		
		virtual public void Combine(BankAccountCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BankAccount this[int index]
		{
			get
			{
				return base[index] as BankAccount;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BankAccount);
		}
	}



	[Serializable]
	abstract public class esBankAccount : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBankAccountQuery GetDynamicQuery()
		{
			return null;
		}

		public esBankAccount()
		{

		}

		public esBankAccount(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String bankID, System.String bankAccountNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bankID, bankAccountNo);
			else
				return LoadByPrimaryKeyStoredProcedure(bankID, bankAccountNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String bankID, System.String bankAccountNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(bankID, bankAccountNo);
			else
				return LoadByPrimaryKeyStoredProcedure(bankID, bankAccountNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String bankID, System.String bankAccountNo)
		{
			esBankAccountQuery query = this.GetDynamicQuery();
			query.Where(query.BankID == bankID, query.BankAccountNo == bankAccountNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String bankID, System.String bankAccountNo)
		{
			esParameters parms = new esParameters();
			parms.Add("BankID",bankID);			parms.Add("BankAccountNo",bankAccountNo);
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
						case "BankID": this.str.BankID = (string)value; break;							
						case "BankAccountNo": this.str.BankAccountNo = (string)value; break;							
						case "SRCurrency": this.str.SRCurrency = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to BankAccount.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(BankAccountMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(BankAccountMetadata.ColumnNames.BankID, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccount.BankAccountNo
		/// </summary>
		virtual public System.String BankAccountNo
		{
			get
			{
				return base.GetSystemString(BankAccountMetadata.ColumnNames.BankAccountNo);
			}
			
			set
			{
				base.SetSystemString(BankAccountMetadata.ColumnNames.BankAccountNo, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccount.SRCurrency
		/// </summary>
		virtual public System.String SRCurrency
		{
			get
			{
				return base.GetSystemString(BankAccountMetadata.ColumnNames.SRCurrency);
			}
			
			set
			{
				base.SetSystemString(BankAccountMetadata.ColumnNames.SRCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccount.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(BankAccountMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(BankAccountMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccount.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(BankAccountMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(BankAccountMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccount.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BankAccountMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BankAccountMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BankAccount.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BankAccountMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BankAccountMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esBankAccount entity)
			{
				this.entity = entity;
			}
			
	
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
				
			public System.String BankAccountNo
			{
				get
				{
					System.String data = entity.BankAccountNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankAccountNo = null;
					else entity.BankAccountNo = Convert.ToString(value);
				}
			}
				
			public System.String SRCurrency
			{
				get
				{
					System.String data = entity.SRCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRCurrency = null;
					else entity.SRCurrency = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esBankAccount entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBankAccountQuery query)
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
				throw new Exception("esBankAccount can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class BankAccount : esBankAccount
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
	abstract public class esBankAccountQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BankAccountMetadata.Meta();
			}
		}	
		

		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, BankAccountMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
		
		public esQueryItem BankAccountNo
		{
			get
			{
				return new esQueryItem(this, BankAccountMetadata.ColumnNames.BankAccountNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRCurrency
		{
			get
			{
				return new esQueryItem(this, BankAccountMetadata.ColumnNames.SRCurrency, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, BankAccountMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, BankAccountMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BankAccountMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BankAccountMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BankAccountCollection")]
	public partial class BankAccountCollection : esBankAccountCollection, IEnumerable<BankAccount>
	{
		public BankAccountCollection()
		{

		}
		
		public static implicit operator List<BankAccount>(BankAccountCollection coll)
		{
			List<BankAccount> list = new List<BankAccount>();
			
			foreach (BankAccount emp in coll)
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
				return  BankAccountMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankAccountQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BankAccount(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BankAccount();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BankAccountQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankAccountQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BankAccountQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BankAccount AddNew()
		{
			BankAccount entity = base.AddNewEntity() as BankAccount;
			
			return entity;
		}

		public BankAccount FindByPrimaryKey(System.String bankID, System.String bankAccountNo)
		{
			return base.FindByPrimaryKey(bankID, bankAccountNo) as BankAccount;
		}


		#region IEnumerable<BankAccount> Members

		IEnumerator<BankAccount> IEnumerable<BankAccount>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BankAccount;
			}
		}

		#endregion
		
		private BankAccountQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BankAccount' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BankAccount ({BankID},{BankAccountNo})")]
	[Serializable]
	public partial class BankAccount : esBankAccount
	{
		public BankAccount()
		{

		}
	
		public BankAccount(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BankAccountMetadata.Meta();
			}
		}
		
		
		
		override protected esBankAccountQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BankAccountQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BankAccountQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BankAccountQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BankAccountQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BankAccountQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BankAccountQuery : esBankAccountQuery
	{
		public BankAccountQuery()
		{

		}		
		
		public BankAccountQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BankAccountQuery";
        }
		
			
	}


	[Serializable]
	public partial class BankAccountMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BankAccountMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BankAccountMetadata.ColumnNames.BankID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BankAccountMetadata.PropertyNames.BankID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountMetadata.ColumnNames.BankAccountNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BankAccountMetadata.PropertyNames.BankAccountNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountMetadata.ColumnNames.SRCurrency, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BankAccountMetadata.PropertyNames.SRCurrency;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountMetadata.ColumnNames.Notes, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BankAccountMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 300;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountMetadata.ColumnNames.IsActive, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BankAccountMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BankAccountMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BankAccountMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BankAccountMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BankAccountMetadata Meta()
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
			 public const string BankID = "BankID";
			 public const string BankAccountNo = "BankAccountNo";
			 public const string SRCurrency = "SRCurrency";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string BankID = "BankID";
			 public const string BankAccountNo = "BankAccountNo";
			 public const string SRCurrency = "SRCurrency";
			 public const string Notes = "Notes";
			 public const string IsActive = "IsActive";
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
			lock (typeof(BankAccountMetadata))
			{
				if(BankAccountMetadata.mapDelegates == null)
				{
					BankAccountMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BankAccountMetadata.meta == null)
				{
					BankAccountMetadata.meta = new BankAccountMetadata();
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
				

				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BankAccountNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRCurrency", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "BankAccount";
				meta.Destination = "BankAccount";
				
				meta.spInsert = "proc_BankAccountInsert";				
				meta.spUpdate = "proc_BankAccountUpdate";		
				meta.spDelete = "proc_BankAccountDelete";
				meta.spLoadAll = "proc_BankAccountLoadAll";
				meta.spLoadByPrimaryKey = "proc_BankAccountLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BankAccountMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
