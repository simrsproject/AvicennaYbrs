/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
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
	abstract public class esMergeBillingCollection : esEntityCollectionWAuditLog
	{
		public esMergeBillingCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MergeBillingCollection";
		}

		#region Query Logic
		protected void InitQuery(esMergeBillingQuery query)
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
			this.InitQuery(query as esMergeBillingQuery);
		}
		#endregion
		
		virtual public MergeBilling DetachEntity(MergeBilling entity)
		{
			return base.DetachEntity(entity) as MergeBilling;
		}
		
		virtual public MergeBilling AttachEntity(MergeBilling entity)
		{
			return base.AttachEntity(entity) as MergeBilling;
		}
		
		virtual public void Combine(MergeBillingCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MergeBilling this[int index]
		{
			get
			{
				return base[index] as MergeBilling;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MergeBilling);
		}
	}



	[Serializable]
	abstract public class esMergeBilling : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMergeBillingQuery GetDynamicQuery()
		{
			return null;
		}

		public esMergeBilling()
		{

		}

		public esMergeBilling(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationNo)
		{
			esMergeBillingQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "FromRegistrationNo": this.str.FromRegistrationNo = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
		/// Maps to MergeBilling.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MergeBillingMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(MergeBillingMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to MergeBilling.FromRegistrationNo
		/// </summary>
		virtual public System.String FromRegistrationNo
		{
			get
			{
				return base.GetSystemString(MergeBillingMetadata.ColumnNames.FromRegistrationNo);
			}
			
			set
			{
				base.SetSystemString(MergeBillingMetadata.ColumnNames.FromRegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to MergeBilling.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MergeBillingMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MergeBillingMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MergeBilling.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MergeBillingMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MergeBillingMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esMergeBilling entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String FromRegistrationNo
			{
				get
				{
					System.String data = entity.FromRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromRegistrationNo = null;
					else entity.FromRegistrationNo = Convert.ToString(value);
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
			

			private esMergeBilling entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMergeBillingQuery query)
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
				throw new Exception("esMergeBilling can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MergeBilling : esMergeBilling
	{

		#region UpToRegistration - One To One
		/// <summary>
		/// One to One
		/// Foreign Key Name - RefMergeBillingToRegistration
		/// </summary>

		[XmlIgnore]
		public Registration UpToRegistration
		{
			get
			{
				if(this._UpToRegistration == null
					&& RegistrationNo != null					)
				{
					this._UpToRegistration = new Registration();
					this._UpToRegistration.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToRegistration", this._UpToRegistration);
					this._UpToRegistration.Query.Where(this._UpToRegistration.Query.RegistrationNo == this.RegistrationNo);
					this._UpToRegistration.Query.Load();
				}

				return this._UpToRegistration;
			}
			
			set 
			{ 
				this.RemovePreSave("UpToRegistration");

				if(value == null)
				{
					this._UpToRegistration = null;
				}
				else
				{
					this._UpToRegistration = value;
					this.SetPreSave("UpToRegistration", this._UpToRegistration);
				}
				
				
			} 
		}

		private Registration _UpToRegistration;
		#endregion

		
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
	abstract public class esMergeBillingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MergeBillingMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MergeBillingMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FromRegistrationNo
		{
			get
			{
				return new esQueryItem(this, MergeBillingMetadata.ColumnNames.FromRegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MergeBillingMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MergeBillingMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MergeBillingCollection")]
	public partial class MergeBillingCollection : esMergeBillingCollection, IEnumerable<MergeBilling>
	{
		public MergeBillingCollection()
		{

		}
		
		public static implicit operator List<MergeBilling>(MergeBillingCollection coll)
		{
			List<MergeBilling> list = new List<MergeBilling>();
			
			foreach (MergeBilling emp in coll)
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
				return  MergeBillingMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MergeBillingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MergeBilling(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MergeBilling();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MergeBillingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MergeBillingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MergeBillingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MergeBilling AddNew()
		{
			MergeBilling entity = base.AddNewEntity() as MergeBilling;
			
			return entity;
		}

		public MergeBilling FindByPrimaryKey(System.String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as MergeBilling;
		}


		#region IEnumerable<MergeBilling> Members

		IEnumerator<MergeBilling> IEnumerable<MergeBilling>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MergeBilling;
			}
		}

		#endregion
		
		private MergeBillingQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MergeBilling' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MergeBilling ({RegistrationNo})")]
	[Serializable]
	public partial class MergeBilling : esMergeBilling
	{
		public MergeBilling()
		{

		}
	
		public MergeBilling(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MergeBillingMetadata.Meta();
			}
		}
		
		
		
		override protected esMergeBillingQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MergeBillingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MergeBillingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MergeBillingQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MergeBillingQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MergeBillingQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MergeBillingQuery : esMergeBillingQuery
	{
		public MergeBillingQuery()
		{

		}		
		
		public MergeBillingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MergeBillingQuery";
        }
		
			
	}


	[Serializable]
	public partial class MergeBillingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MergeBillingMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MergeBillingMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MergeBillingMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(MergeBillingMetadata.ColumnNames.FromRegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MergeBillingMetadata.PropertyNames.FromRegistrationNo;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(MergeBillingMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MergeBillingMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MergeBillingMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MergeBillingMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MergeBillingMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string FromRegistrationNo = "FromRegistrationNo";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string FromRegistrationNo = "FromRegistrationNo";
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
			lock (typeof(MergeBillingMetadata))
			{
				if(MergeBillingMetadata.mapDelegates == null)
				{
					MergeBillingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MergeBillingMetadata.meta == null)
				{
					MergeBillingMetadata.meta = new MergeBillingMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MergeBilling";
				meta.Destination = "MergeBilling";
				
				meta.spInsert = "proc_MergeBillingInsert";				
				meta.spUpdate = "proc_MergeBillingUpdate";		
				meta.spDelete = "proc_MergeBillingDelete";
				meta.spLoadAll = "proc_MergeBillingLoadAll";
				meta.spLoadByPrimaryKey = "proc_MergeBillingLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MergeBillingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
