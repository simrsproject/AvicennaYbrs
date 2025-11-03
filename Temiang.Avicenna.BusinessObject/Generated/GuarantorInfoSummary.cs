/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/1/2015 4:23:09 PM
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
	abstract public class esGuarantorInfoSummaryCollection : esEntityCollectionWAuditLog
	{
		public esGuarantorInfoSummaryCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "GuarantorInfoSummaryCollection";
		}

		#region Query Logic
		protected void InitQuery(esGuarantorInfoSummaryQuery query)
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
			this.InitQuery(query as esGuarantorInfoSummaryQuery);
		}
		#endregion
		
		virtual public GuarantorInfoSummary DetachEntity(GuarantorInfoSummary entity)
		{
			return base.DetachEntity(entity) as GuarantorInfoSummary;
		}
		
		virtual public GuarantorInfoSummary AttachEntity(GuarantorInfoSummary entity)
		{
			return base.AttachEntity(entity) as GuarantorInfoSummary;
		}
		
		virtual public void Combine(GuarantorInfoSummaryCollection collection)
		{
			base.Combine(collection);
		}
		
		new public GuarantorInfoSummary this[int index]
		{
			get
			{
				return base[index] as GuarantorInfoSummary;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(GuarantorInfoSummary);
		}
	}



	[Serializable]
	abstract public class esGuarantorInfoSummary : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esGuarantorInfoSummaryQuery GetDynamicQuery()
		{
			return null;
		}

		public esGuarantorInfoSummary()
		{

		}

		public esGuarantorInfoSummary(DataRow row)
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
			esGuarantorInfoSummaryQuery query = this.GetDynamicQuery();
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
						case "NoteCount": this.str.NoteCount = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "NoteCount":
						
							if (value == null || value is System.Int32)
								this.NoteCount = (System.Int32?)value;
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
		/// Maps to GuarantorInfoSummary.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(GuarantorInfoSummaryMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(GuarantorInfoSummaryMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfoSummary.NoteCount
		/// </summary>
		virtual public System.Int32? NoteCount
		{
			get
			{
				return base.GetSystemInt32(GuarantorInfoSummaryMetadata.ColumnNames.NoteCount);
			}
			
			set
			{
				base.SetSystemInt32(GuarantorInfoSummaryMetadata.ColumnNames.NoteCount, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfoSummary.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(GuarantorInfoSummaryMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(GuarantorInfoSummaryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to GuarantorInfoSummary.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(GuarantorInfoSummaryMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(GuarantorInfoSummaryMetadata.ColumnNames.LastUpdateDateTime, value);
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
			public esStrings(esGuarantorInfoSummary entity)
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
				
			public System.String NoteCount
			{
				get
				{
					System.Int32? data = entity.NoteCount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoteCount = null;
					else entity.NoteCount = Convert.ToInt32(value);
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
			

			private esGuarantorInfoSummary entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esGuarantorInfoSummaryQuery query)
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
				throw new Exception("esGuarantorInfoSummary can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esGuarantorInfoSummaryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorInfoSummaryMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoSummaryMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem NoteCount
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoSummaryMetadata.ColumnNames.NoteCount, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoSummaryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, GuarantorInfoSummaryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("GuarantorInfoSummaryCollection")]
	public partial class GuarantorInfoSummaryCollection : esGuarantorInfoSummaryCollection, IEnumerable<GuarantorInfoSummary>
	{
		public GuarantorInfoSummaryCollection()
		{

		}
		
		public static implicit operator List<GuarantorInfoSummary>(GuarantorInfoSummaryCollection coll)
		{
			List<GuarantorInfoSummary> list = new List<GuarantorInfoSummary>();
			
			foreach (GuarantorInfoSummary emp in coll)
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
				return  GuarantorInfoSummaryMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorInfoSummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new GuarantorInfoSummary(row);
		}

		override protected esEntity CreateEntity()
		{
			return new GuarantorInfoSummary();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public GuarantorInfoSummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorInfoSummaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(GuarantorInfoSummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public GuarantorInfoSummary AddNew()
		{
			GuarantorInfoSummary entity = base.AddNewEntity() as GuarantorInfoSummary;
			
			return entity;
		}

		public GuarantorInfoSummary FindByPrimaryKey(System.String guarantorID)
		{
			return base.FindByPrimaryKey(guarantorID) as GuarantorInfoSummary;
		}


		#region IEnumerable<GuarantorInfoSummary> Members

		IEnumerator<GuarantorInfoSummary> IEnumerable<GuarantorInfoSummary>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as GuarantorInfoSummary;
			}
		}

		#endregion
		
		private GuarantorInfoSummaryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'GuarantorInfoSummary' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("GuarantorInfoSummary ({GuarantorID})")]
	[Serializable]
	public partial class GuarantorInfoSummary : esGuarantorInfoSummary
	{
		public GuarantorInfoSummary()
		{

		}
	
		public GuarantorInfoSummary(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return GuarantorInfoSummaryMetadata.Meta();
			}
		}
		
		
		
		override protected esGuarantorInfoSummaryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new GuarantorInfoSummaryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public GuarantorInfoSummaryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new GuarantorInfoSummaryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(GuarantorInfoSummaryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private GuarantorInfoSummaryQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class GuarantorInfoSummaryQuery : esGuarantorInfoSummaryQuery
	{
		public GuarantorInfoSummaryQuery()
		{

		}		
		
		public GuarantorInfoSummaryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "GuarantorInfoSummaryQuery";
        }
		
			
	}


	[Serializable]
	public partial class GuarantorInfoSummaryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected GuarantorInfoSummaryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(GuarantorInfoSummaryMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorInfoSummaryMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoSummaryMetadata.ColumnNames.NoteCount, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = GuarantorInfoSummaryMetadata.PropertyNames.NoteCount;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoSummaryMetadata.ColumnNames.LastUpdateByUserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = GuarantorInfoSummaryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(GuarantorInfoSummaryMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = GuarantorInfoSummaryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public GuarantorInfoSummaryMetadata Meta()
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
			 public const string NoteCount = "NoteCount";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string NoteCount = "NoteCount";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
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
			lock (typeof(GuarantorInfoSummaryMetadata))
			{
				if(GuarantorInfoSummaryMetadata.mapDelegates == null)
				{
					GuarantorInfoSummaryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (GuarantorInfoSummaryMetadata.meta == null)
				{
					GuarantorInfoSummaryMetadata.meta = new GuarantorInfoSummaryMetadata();
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
				meta.AddTypeMap("NoteCount", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "GuarantorInfoSummary";
				meta.Destination = "GuarantorInfoSummary";
				
				meta.spInsert = "proc_GuarantorInfoSummaryInsert";				
				meta.spUpdate = "proc_GuarantorInfoSummaryUpdate";		
				meta.spDelete = "proc_GuarantorInfoSummaryDelete";
				meta.spLoadAll = "proc_GuarantorInfoSummaryLoadAll";
				meta.spLoadByPrimaryKey = "proc_GuarantorInfoSummaryLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private GuarantorInfoSummaryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
