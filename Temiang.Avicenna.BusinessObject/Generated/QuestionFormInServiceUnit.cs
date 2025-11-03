/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:24 PM
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
	abstract public class esQuestionFormInServiceUnitCollection : esEntityCollectionWAuditLog
	{
		public esQuestionFormInServiceUnitCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "QuestionFormInServiceUnitCollection";
		}

		#region Query Logic
		protected void InitQuery(esQuestionFormInServiceUnitQuery query)
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
			this.InitQuery(query as esQuestionFormInServiceUnitQuery);
		}
		#endregion
		
		virtual public QuestionFormInServiceUnit DetachEntity(QuestionFormInServiceUnit entity)
		{
			return base.DetachEntity(entity) as QuestionFormInServiceUnit;
		}
		
		virtual public QuestionFormInServiceUnit AttachEntity(QuestionFormInServiceUnit entity)
		{
			return base.AttachEntity(entity) as QuestionFormInServiceUnit;
		}
		
		virtual public void Combine(QuestionFormInServiceUnitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QuestionFormInServiceUnit this[int index]
		{
			get
			{
				return base[index] as QuestionFormInServiceUnit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QuestionFormInServiceUnit);
		}
	}



	[Serializable]
	abstract public class esQuestionFormInServiceUnit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQuestionFormInServiceUnitQuery GetDynamicQuery()
		{
			return null;
		}

		public esQuestionFormInServiceUnit()
		{

		}

		public esQuestionFormInServiceUnit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String serviceUnitID, System.String questionFormID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, questionFormID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String serviceUnitID, System.String questionFormID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, questionFormID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, questionFormID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String serviceUnitID, System.String questionFormID)
		{
			esQuestionFormInServiceUnitQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.QuestionFormID == questionFormID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String serviceUnitID, System.String questionFormID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID",serviceUnitID);			parms.Add("QuestionFormID",questionFormID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "QuestionFormID": this.str.QuestionFormID = (string)value; break;							
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
		/// Maps to QuestionFormInServiceUnit.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(QuestionFormInServiceUnitMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(QuestionFormInServiceUnitMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionFormInServiceUnit.QuestionFormID
		/// </summary>
		virtual public System.String QuestionFormID
		{
			get
			{
				return base.GetSystemString(QuestionFormInServiceUnitMetadata.ColumnNames.QuestionFormID);
			}
			
			set
			{
				base.SetSystemString(QuestionFormInServiceUnitMetadata.ColumnNames.QuestionFormID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionFormInServiceUnit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QuestionFormInServiceUnitMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QuestionFormInServiceUnitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionFormInServiceUnit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QuestionFormInServiceUnitMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QuestionFormInServiceUnitMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esQuestionFormInServiceUnit entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
				
			public System.String QuestionFormID
			{
				get
				{
					System.String data = entity.QuestionFormID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionFormID = null;
					else entity.QuestionFormID = Convert.ToString(value);
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
			

			private esQuestionFormInServiceUnit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQuestionFormInServiceUnitQuery query)
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
				throw new Exception("esQuestionFormInServiceUnit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class QuestionFormInServiceUnit : esQuestionFormInServiceUnit
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
	abstract public class esQuestionFormInServiceUnitQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return QuestionFormInServiceUnitMetadata.Meta();
			}
		}	
		

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, QuestionFormInServiceUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionFormID
		{
			get
			{
				return new esQueryItem(this, QuestionFormInServiceUnitMetadata.ColumnNames.QuestionFormID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QuestionFormInServiceUnitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QuestionFormInServiceUnitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QuestionFormInServiceUnitCollection")]
	public partial class QuestionFormInServiceUnitCollection : esQuestionFormInServiceUnitCollection, IEnumerable<QuestionFormInServiceUnit>
	{
		public QuestionFormInServiceUnitCollection()
		{

		}
		
		public static implicit operator List<QuestionFormInServiceUnit>(QuestionFormInServiceUnitCollection coll)
		{
			List<QuestionFormInServiceUnit> list = new List<QuestionFormInServiceUnit>();
			
			foreach (QuestionFormInServiceUnit emp in coll)
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
				return  QuestionFormInServiceUnitMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionFormInServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QuestionFormInServiceUnit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QuestionFormInServiceUnit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public QuestionFormInServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionFormInServiceUnitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(QuestionFormInServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public QuestionFormInServiceUnit AddNew()
		{
			QuestionFormInServiceUnit entity = base.AddNewEntity() as QuestionFormInServiceUnit;
			
			return entity;
		}

		public QuestionFormInServiceUnit FindByPrimaryKey(System.String serviceUnitID, System.String questionFormID)
		{
			return base.FindByPrimaryKey(serviceUnitID, questionFormID) as QuestionFormInServiceUnit;
		}


		#region IEnumerable<QuestionFormInServiceUnit> Members

		IEnumerator<QuestionFormInServiceUnit> IEnumerable<QuestionFormInServiceUnit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QuestionFormInServiceUnit;
			}
		}

		#endregion
		
		private QuestionFormInServiceUnitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QuestionFormInServiceUnit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("QuestionFormInServiceUnit ({ServiceUnitID},{QuestionFormID})")]
	[Serializable]
	public partial class QuestionFormInServiceUnit : esQuestionFormInServiceUnit
	{
		public QuestionFormInServiceUnit()
		{

		}
	
		public QuestionFormInServiceUnit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QuestionFormInServiceUnitMetadata.Meta();
			}
		}
		
		
		
		override protected esQuestionFormInServiceUnitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionFormInServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public QuestionFormInServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionFormInServiceUnitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(QuestionFormInServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private QuestionFormInServiceUnitQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class QuestionFormInServiceUnitQuery : esQuestionFormInServiceUnitQuery
	{
		public QuestionFormInServiceUnitQuery()
		{

		}		
		
		public QuestionFormInServiceUnitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "QuestionFormInServiceUnitQuery";
        }
		
			
	}


	[Serializable]
	public partial class QuestionFormInServiceUnitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QuestionFormInServiceUnitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(QuestionFormInServiceUnitMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormInServiceUnitMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionFormInServiceUnitMetadata.ColumnNames.QuestionFormID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormInServiceUnitMetadata.PropertyNames.QuestionFormID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionFormInServiceUnitMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QuestionFormInServiceUnitMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionFormInServiceUnitMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionFormInServiceUnitMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public QuestionFormInServiceUnitMetadata Meta()
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
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string QuestionFormID = "QuestionFormID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string QuestionFormID = "QuestionFormID";
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
			lock (typeof(QuestionFormInServiceUnitMetadata))
			{
				if(QuestionFormInServiceUnitMetadata.mapDelegates == null)
				{
					QuestionFormInServiceUnitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QuestionFormInServiceUnitMetadata.meta == null)
				{
					QuestionFormInServiceUnitMetadata.meta = new QuestionFormInServiceUnitMetadata();
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
				

				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionFormID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "QuestionFormInServiceUnit";
				meta.Destination = "QuestionFormInServiceUnit";
				
				meta.spInsert = "proc_QuestionFormInServiceUnitInsert";				
				meta.spUpdate = "proc_QuestionFormInServiceUnitUpdate";		
				meta.spDelete = "proc_QuestionFormInServiceUnitDelete";
				meta.spLoadAll = "proc_QuestionFormInServiceUnitLoadAll";
				meta.spLoadByPrimaryKey = "proc_QuestionFormInServiceUnitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QuestionFormInServiceUnitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
