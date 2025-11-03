/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/17/2015 10:27:48 AM
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
	abstract public class esGuarantorDepositBalanceCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorDepositBalanceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorDepositBalanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorDepositBalanceQuery query)
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
			this.InitQuery(query as esGuarantorDepositBalanceQuery);
		}
		#endregion
		
		virtual public GuarantorDepositBalance DetachEntity(GuarantorDepositBalance entity)
		{
			return base.DetachEntity(entity) as GuarantorDepositBalance;
		}
		
		virtual public GuarantorDepositBalance AttachEntity(GuarantorDepositBalance entity)
		{
			return base.AttachEntity(entity) as GuarantorDepositBalance;
		}
		
		virtual public void Combine(GuarantorDepositBalanceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorDepositBalance this[int index]
		{
			get
			{
				return base[index] as GuarantorDepositBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorDepositBalance);
		}
	}



	[Serializable]
	abstract public class esGuarantorDepositBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorDepositBalanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorDepositBalance()
		{

		}

		public esGuarantorDepositBalance(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String guarantorID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String guarantorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(guarantorID);
			else
				return LoadByPrimaryKeyStoredProcedure(guarantorID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String guarantorID)
		{
			esGuarantorDepositBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.GuarantorID == guarantorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String guarantorID)
		{
			esParameters parms = new esParameters();
			parms.Add("GuarantorID",guarantorID);
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
						case "GuarantorID": this.str.GuarantorID = (string)value; break;							
						case "BalanceAmount": this.str.BalanceAmount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BalanceAmount":
						
							if (value == null || value is System.Decimal)
								this.BalanceAmount = (System.Decimal?)value;
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
		/// Maps to GuarantorDepositBalance.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorDepositBalanceMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositBalanceMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDepositBalance.BalanceAmount
		/// </summary>
		virtual public System.Decimal? BalanceAmount
		{
			get
			{
				return base.GetSystemDecimal(GuarantorDepositBalanceMetadata.ColumnNames.BalanceAmount);
			}
			
			set
			{
				base.SetSystemDecimal(GuarantorDepositBalanceMetadata.ColumnNames.BalanceAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDepositBalance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorDepositBalanceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorDepositBalanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorDepositBalance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorDepositBalanceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorDepositBalanceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esGuarantorDepositBalance entity)
			{
				this.entity = entity;
			}
			
	
			public System.String GuarantorID
			{
				get
				{
					System.String data = entity.GuarantorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GuarantorID = null;
					else entity.GuarantorID = Convert.ToString(value);
				}
			}
				
			public System.String BalanceAmount
			{
				get
				{
					System.Decimal? data = entity.BalanceAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceAmount = null;
					else entity.BalanceAmount = Convert.ToDecimal(value);
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
			

			private esGuarantorDepositBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorDepositBalanceQuery query)
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
				throw new Exception("esGuarantorDepositBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class GuarantorDepositBalance : esGuarantorDepositBalance
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
	abstract public class esGuarantorDepositBalanceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorDepositBalanceMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositBalanceMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem BalanceAmount
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositBalanceMetadata.ColumnNames.BalanceAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositBalanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorDepositBalanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorDepositBalanceCollection")]
	public partial class GuarantorDepositBalanceCollection : esGuarantorDepositBalanceCollection, IEnumerable<GuarantorDepositBalance>
	{
		public GuarantorDepositBalanceCollection()
		{

		}
		
		public static implicit operator List<GuarantorDepositBalance>(GuarantorDepositBalanceCollection coll)
		{
			List<GuarantorDepositBalance> list = new List<GuarantorDepositBalance>();
			
			foreach (GuarantorDepositBalance emp in coll)
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
				return  GuarantorDepositBalanceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorDepositBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorDepositBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorDepositBalance();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorDepositBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorDepositBalanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorDepositBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorDepositBalance AddNew()
		{
			GuarantorDepositBalance entity = base.AddNewEntity() as GuarantorDepositBalance;
			
			return entity;
		}

		public GuarantorDepositBalance FindByPrimaryKey(System.String guarantorID)
		{
			return base.FindByPrimaryKey(guarantorID) as GuarantorDepositBalance;
		}


		#region IEnumerable<GuarantorDepositBalance> Members

		IEnumerator<GuarantorDepositBalance> IEnumerable<GuarantorDepositBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorDepositBalance;
			}
		}

		#endregion
		
		private GuarantorDepositBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorDepositBalance' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorDepositBalance ({GuarantorID})")]
	[Serializable]
	public partial class GuarantorDepositBalance : esGuarantorDepositBalance
	{
		public GuarantorDepositBalance()
		{

		}
	
		public GuarantorDepositBalance(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorDepositBalanceMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorDepositBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorDepositBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorDepositBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorDepositBalanceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorDepositBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorDepositBalanceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorDepositBalanceQuery : esGuarantorDepositBalanceQuery
	{
		public GuarantorDepositBalanceQuery()
		{

		}		
		
		public GuarantorDepositBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorDepositBalanceQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorDepositBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorDepositBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorDepositBalanceMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositBalanceMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositBalanceMetadata.ColumnNames.BalanceAmount, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = GuarantorDepositBalanceMetadata.PropertyNames.BalanceAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositBalanceMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorDepositBalanceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorDepositBalanceMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorDepositBalanceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorDepositBalanceMetadata Meta()
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
			 public const string GuarantorID = "GuarantorID";
			 public const string BalanceAmount = "BalanceAmount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string BalanceAmount = "BalanceAmount";
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
			lock (typeof(GuarantorDepositBalanceMetadata))
			{
				if(GuarantorDepositBalanceMetadata.mapDelegates == null)
				{
					GuarantorDepositBalanceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorDepositBalanceMetadata.meta == null)
				{
					GuarantorDepositBalanceMetadata.meta = new GuarantorDepositBalanceMetadata();
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
				

				meta.AddTypeMap("GuarantorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BalanceAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "GuarantorDepositBalance";
				meta.Destination = "GuarantorDepositBalance";
				
				meta.spInsert = "proc_GuarantorDepositBalanceInsert";				
				meta.spUpdate = "proc_GuarantorDepositBalanceUpdate";		
				meta.spDelete = "proc_GuarantorDepositBalanceDelete";
				meta.spLoadAll = "proc_GuarantorDepositBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorDepositBalanceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorDepositBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
