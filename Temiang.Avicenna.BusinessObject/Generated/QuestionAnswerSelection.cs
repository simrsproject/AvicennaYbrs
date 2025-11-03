/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/16/2014 12:40:11 AM
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
	abstract public class esQuestionAnswerSelectionCollection : esEntityCollectionWAuditLog
	{
		public esQuestionAnswerSelectionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "QuestionAnswerSelectionCollection";
		}

		#region Query Logic
		protected void InitQuery(esQuestionAnswerSelectionQuery query)
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
			this.InitQuery(query as esQuestionAnswerSelectionQuery);
		}
		#endregion
		
		virtual public QuestionAnswerSelection DetachEntity(QuestionAnswerSelection entity)
		{
			return base.DetachEntity(entity) as QuestionAnswerSelection;
		}
		
		virtual public QuestionAnswerSelection AttachEntity(QuestionAnswerSelection entity)
		{
			return base.AttachEntity(entity) as QuestionAnswerSelection;
		}
		
		virtual public void Combine(QuestionAnswerSelectionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public QuestionAnswerSelection this[int index]
		{
			get
			{
				return base[index] as QuestionAnswerSelection;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(QuestionAnswerSelection);
		}
	}



	[Serializable]
	abstract public class esQuestionAnswerSelection : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esQuestionAnswerSelectionQuery GetDynamicQuery()
		{
			return null;
		}

		public esQuestionAnswerSelection()
		{

		}

		public esQuestionAnswerSelection(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String questionAnswerSelectionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionAnswerSelectionID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionAnswerSelectionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String questionAnswerSelectionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(questionAnswerSelectionID);
			else
				return LoadByPrimaryKeyStoredProcedure(questionAnswerSelectionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String questionAnswerSelectionID)
		{
			esQuestionAnswerSelectionQuery query = this.GetDynamicQuery();
			query.Where(query.QuestionAnswerSelectionID == questionAnswerSelectionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String questionAnswerSelectionID)
		{
			esParameters parms = new esParameters();
			parms.Add("QuestionAnswerSelectionID",questionAnswerSelectionID);
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
						case "QuestionAnswerSelectionID": this.str.QuestionAnswerSelectionID = (string)value; break;							
						case "QuestionAnswerSelectionText": this.str.QuestionAnswerSelectionText = (string)value; break;							
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
		/// Maps to QuestionAnswerSelection.QuestionAnswerSelectionID
		/// </summary>
		virtual public System.String QuestionAnswerSelectionID
		{
			get
			{
				return base.GetSystemString(QuestionAnswerSelectionMetadata.ColumnNames.QuestionAnswerSelectionID);
			}
			
			set
			{
				base.SetSystemString(QuestionAnswerSelectionMetadata.ColumnNames.QuestionAnswerSelectionID, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionAnswerSelection.QuestionAnswerSelectionText
		/// </summary>
		virtual public System.String QuestionAnswerSelectionText
		{
			get
			{
				return base.GetSystemString(QuestionAnswerSelectionMetadata.ColumnNames.QuestionAnswerSelectionText);
			}
			
			set
			{
				base.SetSystemString(QuestionAnswerSelectionMetadata.ColumnNames.QuestionAnswerSelectionText, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionAnswerSelection.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(QuestionAnswerSelectionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(QuestionAnswerSelectionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to QuestionAnswerSelection.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(QuestionAnswerSelectionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(QuestionAnswerSelectionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esQuestionAnswerSelection entity)
			{
				this.entity = entity;
			}
			
	
			public System.String QuestionAnswerSelectionID
			{
				get
				{
					System.String data = entity.QuestionAnswerSelectionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerSelectionID = null;
					else entity.QuestionAnswerSelectionID = Convert.ToString(value);
				}
			}
				
			public System.String QuestionAnswerSelectionText
			{
				get
				{
					System.String data = entity.QuestionAnswerSelectionText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuestionAnswerSelectionText = null;
					else entity.QuestionAnswerSelectionText = Convert.ToString(value);
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
			

			private esQuestionAnswerSelection entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esQuestionAnswerSelectionQuery query)
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
				throw new Exception("esQuestionAnswerSelection can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esQuestionAnswerSelectionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return QuestionAnswerSelectionMetadata.Meta();
			}
		}	
		

		public esQueryItem QuestionAnswerSelectionID
		{
			get
			{
				return new esQueryItem(this, QuestionAnswerSelectionMetadata.ColumnNames.QuestionAnswerSelectionID, esSystemType.String);
			}
		} 
		
		public esQueryItem QuestionAnswerSelectionText
		{
			get
			{
				return new esQueryItem(this, QuestionAnswerSelectionMetadata.ColumnNames.QuestionAnswerSelectionText, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, QuestionAnswerSelectionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, QuestionAnswerSelectionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("QuestionAnswerSelectionCollection")]
	public partial class QuestionAnswerSelectionCollection : esQuestionAnswerSelectionCollection, IEnumerable<QuestionAnswerSelection>
	{
		public QuestionAnswerSelectionCollection()
		{

		}
		
		public static implicit operator List<QuestionAnswerSelection>(QuestionAnswerSelectionCollection coll)
		{
			List<QuestionAnswerSelection> list = new List<QuestionAnswerSelection>();
			
			foreach (QuestionAnswerSelection emp in coll)
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
				return  QuestionAnswerSelectionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionAnswerSelectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new QuestionAnswerSelection(row);
		}

		override protected esEntity CreateEntity()
		{
			return new QuestionAnswerSelection();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public QuestionAnswerSelectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionAnswerSelectionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(QuestionAnswerSelectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public QuestionAnswerSelection AddNew()
		{
			QuestionAnswerSelection entity = base.AddNewEntity() as QuestionAnswerSelection;
			
			return entity;
		}

		public QuestionAnswerSelection FindByPrimaryKey(System.String questionAnswerSelectionID)
		{
			return base.FindByPrimaryKey(questionAnswerSelectionID) as QuestionAnswerSelection;
		}


		#region IEnumerable<QuestionAnswerSelection> Members

		IEnumerator<QuestionAnswerSelection> IEnumerable<QuestionAnswerSelection>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as QuestionAnswerSelection;
			}
		}

		#endregion
		
		private QuestionAnswerSelectionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'QuestionAnswerSelection' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("QuestionAnswerSelection ({QuestionAnswerSelectionID})")]
	[Serializable]
	public partial class QuestionAnswerSelection : esQuestionAnswerSelection
	{
		public QuestionAnswerSelection()
		{

		}
	
		public QuestionAnswerSelection(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return QuestionAnswerSelectionMetadata.Meta();
			}
		}
		
		
		
		override protected esQuestionAnswerSelectionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new QuestionAnswerSelectionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public QuestionAnswerSelectionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new QuestionAnswerSelectionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(QuestionAnswerSelectionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private QuestionAnswerSelectionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class QuestionAnswerSelectionQuery : esQuestionAnswerSelectionQuery
	{
		public QuestionAnswerSelectionQuery()
		{

		}		
		
		public QuestionAnswerSelectionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "QuestionAnswerSelectionQuery";
        }
		
			
	}


	[Serializable]
	public partial class QuestionAnswerSelectionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected QuestionAnswerSelectionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(QuestionAnswerSelectionMetadata.ColumnNames.QuestionAnswerSelectionID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionAnswerSelectionMetadata.PropertyNames.QuestionAnswerSelectionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionAnswerSelectionMetadata.ColumnNames.QuestionAnswerSelectionText, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionAnswerSelectionMetadata.PropertyNames.QuestionAnswerSelectionText;
			c.CharacterMaxLength = 200;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionAnswerSelectionMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = QuestionAnswerSelectionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(QuestionAnswerSelectionMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = QuestionAnswerSelectionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public QuestionAnswerSelectionMetadata Meta()
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
			 public const string QuestionAnswerSelectionID = "QuestionAnswerSelectionID";
			 public const string QuestionAnswerSelectionText = "QuestionAnswerSelectionText";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string QuestionAnswerSelectionID = "QuestionAnswerSelectionID";
			 public const string QuestionAnswerSelectionText = "QuestionAnswerSelectionText";
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
			lock (typeof(QuestionAnswerSelectionMetadata))
			{
				if(QuestionAnswerSelectionMetadata.mapDelegates == null)
				{
					QuestionAnswerSelectionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (QuestionAnswerSelectionMetadata.meta == null)
				{
					QuestionAnswerSelectionMetadata.meta = new QuestionAnswerSelectionMetadata();
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
				

				meta.AddTypeMap("QuestionAnswerSelectionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuestionAnswerSelectionText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "QuestionAnswerSelection";
				meta.Destination = "QuestionAnswerSelection";
				
				meta.spInsert = "proc_QuestionAnswerSelectionInsert";				
				meta.spUpdate = "proc_QuestionAnswerSelectionUpdate";		
				meta.spDelete = "proc_QuestionAnswerSelectionDelete";
				meta.spLoadAll = "proc_QuestionAnswerSelectionLoadAll";
				meta.spLoadByPrimaryKey = "proc_QuestionAnswerSelectionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private QuestionAnswerSelectionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
