/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/17/2014 8:32:19 AM
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
	abstract public class esRegistrationPlafondRuleCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationPlafondRuleCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationPlafondRuleCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationPlafondRuleQuery query)
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
			this.InitQuery(query as esRegistrationPlafondRuleQuery);
		}
		#endregion
		
		virtual public RegistrationPlafondRule DetachEntity(RegistrationPlafondRule entity)
		{
			return base.DetachEntity(entity) as RegistrationPlafondRule;
		}
		
		virtual public RegistrationPlafondRule AttachEntity(RegistrationPlafondRule entity)
		{
			return base.AttachEntity(entity) as RegistrationPlafondRule;
		}
		
		virtual public void Combine(RegistrationPlafondRuleCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationPlafondRule this[int index]
		{
			get
			{
				return base[index] as RegistrationPlafondRule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationPlafondRule);
		}
	}



	[Serializable]
	abstract public class esRegistrationPlafondRule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationPlafondRuleQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationPlafondRule()
		{

		}

		public esRegistrationPlafondRule(DataRow row)
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
			esRegistrationPlafondRuleQuery query = this.GetDynamicQuery();
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
						case "PlafondAmount": this.str.PlafondAmount = (string)value; break;							
						case "IsPlafondInPercent": this.str.IsPlafondInPercent = (string)value; break;							
						case "IsToGuarantor": this.str.IsToGuarantor = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PlafondAmount":
						
							if (value == null || value is System.Decimal)
								this.PlafondAmount = (System.Decimal?)value;
							break;
						
						case "IsPlafondInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsPlafondInPercent = (System.Boolean?)value;
							break;
						
						case "IsToGuarantor":
						
							if (value == null || value is System.Boolean)
								this.IsToGuarantor = (System.Boolean?)value;
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
		/// Maps to RegistrationPlafondRule.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationPlafondRuleMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationPlafondRuleMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationPlafondRule.PlafondAmount
		/// </summary>
		virtual public System.Decimal? PlafondAmount
		{
			get
			{
				return base.GetSystemDecimal(RegistrationPlafondRuleMetadata.ColumnNames.PlafondAmount);
			}
			
			set
			{
				base.SetSystemDecimal(RegistrationPlafondRuleMetadata.ColumnNames.PlafondAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationPlafondRule.IsPlafondInPercent
		/// </summary>
		virtual public System.Boolean? IsPlafondInPercent
		{
			get
			{
				return base.GetSystemBoolean(RegistrationPlafondRuleMetadata.ColumnNames.IsPlafondInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationPlafondRuleMetadata.ColumnNames.IsPlafondInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationPlafondRule.IsToGuarantor
		/// </summary>
		virtual public System.Boolean? IsToGuarantor
		{
			get
			{
				return base.GetSystemBoolean(RegistrationPlafondRuleMetadata.ColumnNames.IsToGuarantor);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationPlafondRuleMetadata.ColumnNames.IsToGuarantor, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationPlafondRule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationPlafondRuleMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationPlafondRuleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationPlafondRule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationPlafondRuleMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationPlafondRuleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationPlafondRule entity)
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
				
			public System.String PlafondAmount
			{
				get
				{
					System.Decimal? data = entity.PlafondAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlafondAmount = null;
					else entity.PlafondAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsPlafondInPercent
			{
				get
				{
					System.Boolean? data = entity.IsPlafondInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPlafondInPercent = null;
					else entity.IsPlafondInPercent = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsToGuarantor
			{
				get
				{
					System.Boolean? data = entity.IsToGuarantor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsToGuarantor = null;
					else entity.IsToGuarantor = Convert.ToBoolean(value);
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
			

			private esRegistrationPlafondRule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationPlafondRuleQuery query)
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
				throw new Exception("esRegistrationPlafondRule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RegistrationPlafondRule : esRegistrationPlafondRule
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
	abstract public class esRegistrationPlafondRuleQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationPlafondRuleMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationPlafondRuleMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem PlafondAmount
		{
			get
			{
				return new esQueryItem(this, RegistrationPlafondRuleMetadata.ColumnNames.PlafondAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsPlafondInPercent
		{
			get
			{
				return new esQueryItem(this, RegistrationPlafondRuleMetadata.ColumnNames.IsPlafondInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsToGuarantor
		{
			get
			{
				return new esQueryItem(this, RegistrationPlafondRuleMetadata.ColumnNames.IsToGuarantor, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationPlafondRuleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationPlafondRuleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationPlafondRuleCollection")]
	public partial class RegistrationPlafondRuleCollection : esRegistrationPlafondRuleCollection, IEnumerable<RegistrationPlafondRule>
	{
		public RegistrationPlafondRuleCollection()
		{

		}
		
		public static implicit operator List<RegistrationPlafondRule>(RegistrationPlafondRuleCollection coll)
		{
			List<RegistrationPlafondRule> list = new List<RegistrationPlafondRule>();
			
			foreach (RegistrationPlafondRule emp in coll)
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
				return  RegistrationPlafondRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationPlafondRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationPlafondRule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationPlafondRule();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationPlafondRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationPlafondRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationPlafondRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationPlafondRule AddNew()
		{
			RegistrationPlafondRule entity = base.AddNewEntity() as RegistrationPlafondRule;
			
			return entity;
		}

		public RegistrationPlafondRule FindByPrimaryKey(System.String registrationNo)
		{
			return base.FindByPrimaryKey(registrationNo) as RegistrationPlafondRule;
		}


		#region IEnumerable<RegistrationPlafondRule> Members

		IEnumerator<RegistrationPlafondRule> IEnumerable<RegistrationPlafondRule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationPlafondRule;
			}
		}

		#endregion
		
		private RegistrationPlafondRuleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationPlafondRule' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationPlafondRule ({RegistrationNo})")]
	[Serializable]
	public partial class RegistrationPlafondRule : esRegistrationPlafondRule
	{
		public RegistrationPlafondRule()
		{

		}
	
		public RegistrationPlafondRule(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationPlafondRuleMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationPlafondRuleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationPlafondRuleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationPlafondRuleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationPlafondRuleQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationPlafondRuleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationPlafondRuleQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationPlafondRuleQuery : esRegistrationPlafondRuleQuery
	{
		public RegistrationPlafondRuleQuery()
		{

		}		
		
		public RegistrationPlafondRuleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationPlafondRuleQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationPlafondRuleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationPlafondRuleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationPlafondRuleMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPlafondRuleMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationPlafondRuleMetadata.ColumnNames.PlafondAmount, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationPlafondRuleMetadata.PropertyNames.PlafondAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationPlafondRuleMetadata.ColumnNames.IsPlafondInPercent, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationPlafondRuleMetadata.PropertyNames.IsPlafondInPercent;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationPlafondRuleMetadata.ColumnNames.IsToGuarantor, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationPlafondRuleMetadata.PropertyNames.IsToGuarantor;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationPlafondRuleMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationPlafondRuleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationPlafondRuleMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPlafondRuleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationPlafondRuleMetadata Meta()
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
			 public const string PlafondAmount = "PlafondAmount";
			 public const string IsPlafondInPercent = "IsPlafondInPercent";
			 public const string IsToGuarantor = "IsToGuarantor";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string PlafondAmount = "PlafondAmount";
			 public const string IsPlafondInPercent = "IsPlafondInPercent";
			 public const string IsToGuarantor = "IsToGuarantor";
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
			lock (typeof(RegistrationPlafondRuleMetadata))
			{
				if(RegistrationPlafondRuleMetadata.mapDelegates == null)
				{
					RegistrationPlafondRuleMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationPlafondRuleMetadata.meta == null)
				{
					RegistrationPlafondRuleMetadata.meta = new RegistrationPlafondRuleMetadata();
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
				meta.AddTypeMap("PlafondAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsPlafondInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsToGuarantor", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RegistrationPlafondRule";
				meta.Destination = "RegistrationPlafondRule";
				
				meta.spInsert = "proc_RegistrationPlafondRuleInsert";				
				meta.spUpdate = "proc_RegistrationPlafondRuleUpdate";		
				meta.spDelete = "proc_RegistrationPlafondRuleDelete";
				meta.spLoadAll = "proc_RegistrationPlafondRuleLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationPlafondRuleLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationPlafondRuleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
