/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/27/2015 10:26:44 PM
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
	abstract public class esQuestionInGroupCollection : esEntityCollectionWAuditLog
	{
		public esQuestionInGroupCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "QuestionInGroupCollection";
		}

		#region Query Logic
		protected void InitQuery(esQuestionInGroupQuery query)
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
			this.InitQuery(query as esQuestionInGroupQuery);
		}
		#endregion
		
		virtual public QuestionInGroup DetachEntity(QuestionInGroup entity)
		{
			return base.DetachEntity(entity) as QuestionInGroup;
		}
		
		virtual public QuestionInGroup AttachEntity(QuestionInGroup entity)
		{
			return base.AttachEntity(entity) as QuestionInGroup;
		}
		
		virtual public void Combine(QuestionInGroupCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QuestionInGroup this[int index]
		{
			get
			{
				return base[index] as QuestionInGroup;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QuestionInGroup);
		}
	}



	[Serializable]
	abstract public class esQuestionInGroup : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQuestionInGroupQuery GetDynamicQuery()
		{
			return null;
		}

		public esQuestionInGroup()
		{

		}

		public esQuestionInGroup(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String questionGroupID, System.String questionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionGroupID, questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionGroupID, questionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String questionGroupID, System.String questionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionGroupID, questionID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionGroupID, questionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String questionGroupID, System.String questionID)
		{
			esQuestionInGroupQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionGroupID == questionGroupID, query.QuestionID == questionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String questionGroupID, System.String questionID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionGroupID",questionGroupID);			parms.Add("QuestionID",questionID);
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
						case "QuestionGroupID": this.str.QuestionGroupID = (string)value; break;							
						case "QuestionID": this.str.QuestionID = (string)value; break;							
						case "RowIndex": this.str.RowIndex = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "ParentQuestionID": this.str.ParentQuestionID = (string)value; break;							
						case "QuestionLevel": this.str.QuestionLevel = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RowIndex":
						
							if (value == null || value is System.Int32)
								this.RowIndex = (System.Int32?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "QuestionLevel":
						
							if (value == null || value is System.Int32)
								this.QuestionLevel = (System.Int32?)value;
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
		/// Maps to QuestionInGroup.QuestionGroupID
		/// </summary>
		virtual public System.String QuestionGroupID
		{
			get
			{
				return base.GetSystemString(QuestionInGroupMetadata.ColumnNames.QuestionGroupID);
			}
			
			set
			{
				base.SetSystemString(QuestionInGroupMetadata.ColumnNames.QuestionGroupID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionInGroup.QuestionID
		/// </summary>
		virtual public System.String QuestionID
		{
			get
			{
				return base.GetSystemString(QuestionInGroupMetadata.ColumnNames.QuestionID);
			}
			
			set
			{
				base.SetSystemString(QuestionInGroupMetadata.ColumnNames.QuestionID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionInGroup.RowIndex
		/// </summary>
		virtual public System.Int32? RowIndex
		{
			get
			{
				return base.GetSystemInt32(QuestionInGroupMetadata.ColumnNames.RowIndex);
			}
			
			set
			{
				base.SetSystemInt32(QuestionInGroupMetadata.ColumnNames.RowIndex, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionInGroup.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QuestionInGroupMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QuestionInGroupMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionInGroup.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QuestionInGroupMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QuestionInGroupMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionInGroup.ParentQuestionID
		/// </summary>
		virtual public System.String ParentQuestionID
		{
			get
			{
				return base.GetSystemString(QuestionInGroupMetadata.ColumnNames.ParentQuestionID);
			}
			
			set
			{
				base.SetSystemString(QuestionInGroupMetadata.ColumnNames.ParentQuestionID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionInGroup.QuestionLevel
		/// </summary>
		virtual public System.Int32? QuestionLevel
		{
			get
			{
				return base.GetSystemInt32(QuestionInGroupMetadata.ColumnNames.QuestionLevel);
			}
			
			set
			{
				base.SetSystemInt32(QuestionInGroupMetadata.ColumnNames.QuestionLevel, value);
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
			public esStrings(esQuestionInGroup entity)
			{
				this.entity = entity;
			}
			
	
			public System.String QuestionGroupID
			{
				get
				{
					System.String data = entity.QuestionGroupID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionGroupID = null;
					else entity.QuestionGroupID = Convert.ToString(value);
				}
			}
				
			public System.String QuestionID
			{
				get
				{
					System.String data = entity.QuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionID = null;
					else entity.QuestionID = Convert.ToString(value);
				}
			}
				
			public System.String RowIndex
			{
				get
				{
					System.Int32? data = entity.RowIndex;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RowIndex = null;
					else entity.RowIndex = Convert.ToInt32(value);
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
				
			public System.String ParentQuestionID
			{
				get
				{
					System.String data = entity.ParentQuestionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentQuestionID = null;
					else entity.ParentQuestionID = Convert.ToString(value);
				}
			}
				
			public System.String QuestionLevel
			{
				get
				{
					System.Int32? data = entity.QuestionLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionLevel = null;
					else entity.QuestionLevel = Convert.ToInt32(value);
				}
			}
			

			private esQuestionInGroup entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQuestionInGroupQuery query)
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
				throw new Exception("esQuestionInGroup can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esQuestionInGroupQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return QuestionInGroupMetadata.Meta();
			}
		}	
		

		public esQueryItem QuestionGroupID
		{
			get
			{
				return new esQueryItem(this, QuestionInGroupMetadata.ColumnNames.QuestionGroupID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionID
		{
			get
			{
				return new esQueryItem(this, QuestionInGroupMetadata.ColumnNames.QuestionID, esSystemType.String);
			}
		} 
		
		public esQueryItem RowIndex
		{
			get
			{
				return new esQueryItem(this, QuestionInGroupMetadata.ColumnNames.RowIndex, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QuestionInGroupMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QuestionInGroupMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParentQuestionID
		{
			get
			{
				return new esQueryItem(this, QuestionInGroupMetadata.ColumnNames.ParentQuestionID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionLevel
		{
			get
			{
				return new esQueryItem(this, QuestionInGroupMetadata.ColumnNames.QuestionLevel, esSystemType.Int32);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QuestionInGroupCollection")]
	public partial class QuestionInGroupCollection : esQuestionInGroupCollection, IEnumerable<QuestionInGroup>
	{
		public QuestionInGroupCollection()
		{

		}
		
		public static implicit operator List<QuestionInGroup>(QuestionInGroupCollection coll)
		{
			List<QuestionInGroup> list = new List<QuestionInGroup>();
			
			foreach (QuestionInGroup emp in coll)
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
				return  QuestionInGroupMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionInGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QuestionInGroup(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QuestionInGroup();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public QuestionInGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionInGroupQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(QuestionInGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public QuestionInGroup AddNew()
		{
			QuestionInGroup entity = base.AddNewEntity() as QuestionInGroup;
			
			return entity;
		}

		public QuestionInGroup FindByPrimaryKey(System.String questionGroupID, System.String questionID)
		{
			return base.FindByPrimaryKey(questionGroupID, questionID) as QuestionInGroup;
		}


		#region IEnumerable<QuestionInGroup> Members

		IEnumerator<QuestionInGroup> IEnumerable<QuestionInGroup>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QuestionInGroup;
			}
		}

		#endregion
		
		private QuestionInGroupQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QuestionInGroup' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("QuestionInGroup ({QuestionGroupID},{QuestionID})")]
	[Serializable]
	public partial class QuestionInGroup : esQuestionInGroup
	{
		public QuestionInGroup()
		{

		}
	
		public QuestionInGroup(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QuestionInGroupMetadata.Meta();
			}
		}
		
		
		
		override protected esQuestionInGroupQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionInGroupQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public QuestionInGroupQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionInGroupQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(QuestionInGroupQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private QuestionInGroupQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class QuestionInGroupQuery : esQuestionInGroupQuery
	{
		public QuestionInGroupQuery()
		{

		}		
		
		public QuestionInGroupQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "QuestionInGroupQuery";
        }
		
			
	}


	[Serializable]
	public partial class QuestionInGroupMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QuestionInGroupMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(QuestionInGroupMetadata.ColumnNames.QuestionGroupID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionInGroupMetadata.PropertyNames.QuestionGroupID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionInGroupMetadata.ColumnNames.QuestionID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionInGroupMetadata.PropertyNames.QuestionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionInGroupMetadata.ColumnNames.RowIndex, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionInGroupMetadata.PropertyNames.RowIndex;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionInGroupMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QuestionInGroupMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionInGroupMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionInGroupMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionInGroupMetadata.ColumnNames.ParentQuestionID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionInGroupMetadata.PropertyNames.ParentQuestionID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionInGroupMetadata.ColumnNames.QuestionLevel, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = QuestionInGroupMetadata.PropertyNames.QuestionLevel;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public QuestionInGroupMetadata Meta()
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
			 public const string QuestionGroupID = "QuestionGroupID";
			 public const string QuestionID = "QuestionID";
			 public const string RowIndex = "RowIndex";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ParentQuestionID = "ParentQuestionID";
			 public const string QuestionLevel = "QuestionLevel";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string QuestionGroupID = "QuestionGroupID";
			 public const string QuestionID = "QuestionID";
			 public const string RowIndex = "RowIndex";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ParentQuestionID = "ParentQuestionID";
			 public const string QuestionLevel = "QuestionLevel";
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
			lock (typeof(QuestionInGroupMetadata))
			{
				if(QuestionInGroupMetadata.mapDelegates == null)
				{
					QuestionInGroupMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QuestionInGroupMetadata.meta == null)
				{
					QuestionInGroupMetadata.meta = new QuestionInGroupMetadata();
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
				

				meta.AddTypeMap("QuestionGroupID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RowIndex", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParentQuestionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionLevel", new esTypeMap("int", "System.Int32"));			
				
				
				
				meta.Source = "QuestionInGroup";
				meta.Destination = "QuestionInGroup";
				
				meta.spInsert = "proc_QuestionInGroupInsert";				
				meta.spUpdate = "proc_QuestionInGroupUpdate";		
				meta.spDelete = "proc_QuestionInGroupDelete";
				meta.spLoadAll = "proc_QuestionInGroupLoadAll";
				meta.spLoadByPrimaryKey = "proc_QuestionInGroupLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QuestionInGroupMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
