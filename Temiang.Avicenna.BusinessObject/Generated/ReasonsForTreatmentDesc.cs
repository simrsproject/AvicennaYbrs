/*
===============================================================================
                    EntitySpaces 2009 by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2009.2.1214.0
EntitySpaces Driver  : SQL
Date Generated       : 6/27/2014 11:15:10 AM
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
	abstract public class esReasonsForTreatmentDescCollection : esEntityCollection
	{
		public esReasonsForTreatmentDescCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ReasonsForTreatmentDescCollection";
		}

		#region Query Logic
		protected void InitQuery(esReasonsForTreatmentDescQuery query)
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
			this.InitQuery(query as esReasonsForTreatmentDescQuery);
		}
		#endregion
		
		virtual public ReasonsForTreatmentDesc DetachEntity(ReasonsForTreatmentDesc entity)
		{
			return base.DetachEntity(entity) as ReasonsForTreatmentDesc;
		}
		
		virtual public ReasonsForTreatmentDesc AttachEntity(ReasonsForTreatmentDesc entity)
		{
			return base.AttachEntity(entity) as ReasonsForTreatmentDesc;
		}
		
		virtual public void Combine(ReasonsForTreatmentDescCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ReasonsForTreatmentDesc this[int index]
		{
			get
			{
				return base[index] as ReasonsForTreatmentDesc;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ReasonsForTreatmentDesc);
		}
	}



	[Serializable]
	abstract public class esReasonsForTreatmentDesc : esEntity
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esReasonsForTreatmentDescQuery GetDynamicQuery()
		{
			return null;
		}

		public esReasonsForTreatmentDesc()
		{

		}

		public esReasonsForTreatmentDesc(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String sRReasonVisit, System.String reasonsForTreatmentID, System.String reasonsForTreatmentDescID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRReasonVisit, reasonsForTreatmentID, reasonsForTreatmentDescID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRReasonVisit, reasonsForTreatmentID, reasonsForTreatmentDescID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String sRReasonVisit, System.String reasonsForTreatmentID, System.String reasonsForTreatmentDescID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRReasonVisit, reasonsForTreatmentID, reasonsForTreatmentDescID);
			else
				return LoadByPrimaryKeyStoredProcedure(sRReasonVisit, reasonsForTreatmentID, reasonsForTreatmentDescID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String sRReasonVisit, System.String reasonsForTreatmentID, System.String reasonsForTreatmentDescID)
		{
			esReasonsForTreatmentDescQuery query = this.GetDynamicQuery();
			query.Where(query.SRReasonVisit == sRReasonVisit, query.ReasonsForTreatmentID == reasonsForTreatmentID, query.ReasonsForTreatmentDescID == reasonsForTreatmentDescID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String sRReasonVisit, System.String reasonsForTreatmentID, System.String reasonsForTreatmentDescID)
		{
			esParameters parms = new esParameters();
			parms.Add("SRReasonVisit",sRReasonVisit);			parms.Add("ReasonsForTreatmentID",reasonsForTreatmentID);			parms.Add("ReasonsForTreatmentDescID",reasonsForTreatmentDescID);
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
						case "SRReasonVisit": this.str.SRReasonVisit = (string)value; break;							
						case "ReasonsForTreatmentID": this.str.ReasonsForTreatmentID = (string)value; break;							
						case "ReasonsForTreatmentDescID": this.str.ReasonsForTreatmentDescID = (string)value; break;							
						case "ReasonsForTreatmentDescName": this.str.ReasonsForTreatmentDescName = (string)value; break;							
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
		/// Maps to ReasonsForTreatmentDesc.SRReasonVisit
		/// </summary>
		virtual public System.String SRReasonVisit
		{
			get
			{
				return base.GetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.SRReasonVisit);
			}
			
			set
			{
				base.SetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.SRReasonVisit, value);
			}
		}
		
		/// <summary>
		/// Maps to ReasonsForTreatmentDesc.ReasonsForTreatmentID
		/// </summary>
		virtual public System.String ReasonsForTreatmentID
		{
			get
			{
				return base.GetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentID);
			}
			
			set
			{
				base.SetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ReasonsForTreatmentDesc.ReasonsForTreatmentDescID
		/// </summary>
		virtual public System.String ReasonsForTreatmentDescID
		{
			get
			{
				return base.GetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescID);
			}
			
			set
			{
				base.SetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescID, value);
			}
		}
		
		/// <summary>
		/// Maps to ReasonsForTreatmentDesc.ReasonsForTreatmentDescName
		/// </summary>
		virtual public System.String ReasonsForTreatmentDescName
		{
			get
			{
				return base.GetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescName);
			}
			
			set
			{
				base.SetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescName, value);
			}
		}
		
		/// <summary>
		/// Maps to ReasonsForTreatmentDesc.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ReasonsForTreatmentDescMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ReasonsForTreatmentDescMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ReasonsForTreatmentDesc.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ReasonsForTreatmentDescMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esReasonsForTreatmentDesc entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SRReasonVisit
			{
				get
				{
					System.String data = entity.SRReasonVisit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRReasonVisit = null;
					else entity.SRReasonVisit = Convert.ToString(value);
				}
			}
				
			public System.String ReasonsForTreatmentID
			{
				get
				{
					System.String data = entity.ReasonsForTreatmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonsForTreatmentID = null;
					else entity.ReasonsForTreatmentID = Convert.ToString(value);
				}
			}
				
			public System.String ReasonsForTreatmentDescID
			{
				get
				{
					System.String data = entity.ReasonsForTreatmentDescID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonsForTreatmentDescID = null;
					else entity.ReasonsForTreatmentDescID = Convert.ToString(value);
				}
			}
				
			public System.String ReasonsForTreatmentDescName
			{
				get
				{
					System.String data = entity.ReasonsForTreatmentDescName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonsForTreatmentDescName = null;
					else entity.ReasonsForTreatmentDescName = Convert.ToString(value);
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
			

			private esReasonsForTreatmentDesc entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esReasonsForTreatmentDescQuery query)
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
				throw new Exception("esReasonsForTreatmentDesc can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ReasonsForTreatmentDesc : esReasonsForTreatmentDesc
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
	abstract public class esReasonsForTreatmentDescQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ReasonsForTreatmentDescMetadata.Meta();
			}
		}	
		

		public esQueryItem SRReasonVisit
		{
			get
			{
				return new esQueryItem(this, ReasonsForTreatmentDescMetadata.ColumnNames.SRReasonVisit, esSystemType.String);
			}
		} 
		
		public esQueryItem ReasonsForTreatmentID
		{
			get
			{
				return new esQueryItem(this, ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentID, esSystemType.String);
			}
		} 
		
		public esQueryItem ReasonsForTreatmentDescID
		{
			get
			{
				return new esQueryItem(this, ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescID, esSystemType.String);
			}
		} 
		
		public esQueryItem ReasonsForTreatmentDescName
		{
			get
			{
				return new esQueryItem(this, ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescName, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ReasonsForTreatmentDescMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ReasonsForTreatmentDescMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ReasonsForTreatmentDescCollection")]
	public partial class ReasonsForTreatmentDescCollection : esReasonsForTreatmentDescCollection, IEnumerable<ReasonsForTreatmentDesc>
	{
		public ReasonsForTreatmentDescCollection()
		{

		}
		
		public static implicit operator List<ReasonsForTreatmentDesc>(ReasonsForTreatmentDescCollection coll)
		{
			List<ReasonsForTreatmentDesc> list = new List<ReasonsForTreatmentDesc>();
			
			foreach (ReasonsForTreatmentDesc emp in coll)
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
				return  ReasonsForTreatmentDescMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ReasonsForTreatmentDescQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ReasonsForTreatmentDesc(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ReasonsForTreatmentDesc();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ReasonsForTreatmentDescQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ReasonsForTreatmentDescQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ReasonsForTreatmentDescQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ReasonsForTreatmentDesc AddNew()
		{
			ReasonsForTreatmentDesc entity = base.AddNewEntity() as ReasonsForTreatmentDesc;
			
			return entity;
		}

		public ReasonsForTreatmentDesc FindByPrimaryKey(System.String sRReasonVisit, System.String reasonsForTreatmentID, System.String reasonsForTreatmentDescID)
		{
			return base.FindByPrimaryKey(sRReasonVisit, reasonsForTreatmentID, reasonsForTreatmentDescID) as ReasonsForTreatmentDesc;
		}


		#region IEnumerable<ReasonsForTreatmentDesc> Members

		IEnumerator<ReasonsForTreatmentDesc> IEnumerable<ReasonsForTreatmentDesc>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ReasonsForTreatmentDesc;
			}
		}

		#endregion
		
		private ReasonsForTreatmentDescQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ReasonsForTreatmentDesc' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ReasonsForTreatmentDesc ({SRReasonVisit},{ReasonsForTreatmentID},{ReasonsForTreatmentDescID})")]
	[Serializable]
	public partial class ReasonsForTreatmentDesc : esReasonsForTreatmentDesc
	{
		public ReasonsForTreatmentDesc()
		{

		}
	
		public ReasonsForTreatmentDesc(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ReasonsForTreatmentDescMetadata.Meta();
			}
		}
		
		
		
		override protected esReasonsForTreatmentDescQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ReasonsForTreatmentDescQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ReasonsForTreatmentDescQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ReasonsForTreatmentDescQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ReasonsForTreatmentDescQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ReasonsForTreatmentDescQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ReasonsForTreatmentDescQuery : esReasonsForTreatmentDescQuery
	{
		public ReasonsForTreatmentDescQuery()
		{

		}		
		
		public ReasonsForTreatmentDescQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ReasonsForTreatmentDescQuery";
        }
		
			
	}


	[Serializable]
	public partial class ReasonsForTreatmentDescMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ReasonsForTreatmentDescMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ReasonsForTreatmentDescMetadata.ColumnNames.SRReasonVisit, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ReasonsForTreatmentDescMetadata.PropertyNames.SRReasonVisit;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ReasonsForTreatmentDescMetadata.PropertyNames.ReasonsForTreatmentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ReasonsForTreatmentDescMetadata.PropertyNames.ReasonsForTreatmentDescID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReasonsForTreatmentDescMetadata.ColumnNames.ReasonsForTreatmentDescName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ReasonsForTreatmentDescMetadata.PropertyNames.ReasonsForTreatmentDescName;
			c.CharacterMaxLength = 150;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReasonsForTreatmentDescMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ReasonsForTreatmentDescMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ReasonsForTreatmentDescMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ReasonsForTreatmentDescMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ReasonsForTreatmentDescMetadata Meta()
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
			 public const string SRReasonVisit = "SRReasonVisit";
			 public const string ReasonsForTreatmentID = "ReasonsForTreatmentID";
			 public const string ReasonsForTreatmentDescID = "ReasonsForTreatmentDescID";
			 public const string ReasonsForTreatmentDescName = "ReasonsForTreatmentDescName";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SRReasonVisit = "SRReasonVisit";
			 public const string ReasonsForTreatmentID = "ReasonsForTreatmentID";
			 public const string ReasonsForTreatmentDescID = "ReasonsForTreatmentDescID";
			 public const string ReasonsForTreatmentDescName = "ReasonsForTreatmentDescName";
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
			lock (typeof(ReasonsForTreatmentDescMetadata))
			{
				if(ReasonsForTreatmentDescMetadata.mapDelegates == null)
				{
					ReasonsForTreatmentDescMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ReasonsForTreatmentDescMetadata.meta == null)
				{
					ReasonsForTreatmentDescMetadata.meta = new ReasonsForTreatmentDescMetadata();
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
				

				meta.AddTypeMap("SRReasonVisit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReasonsForTreatmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReasonsForTreatmentDescID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReasonsForTreatmentDescName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ReasonsForTreatmentDesc";
				meta.Destination = "ReasonsForTreatmentDesc";
				
				meta.spInsert = "proc_ReasonsForTreatmentDescInsert";				
				meta.spUpdate = "proc_ReasonsForTreatmentDescUpdate";		
				meta.spDelete = "proc_ReasonsForTreatmentDescDelete";
				meta.spLoadAll = "proc_ReasonsForTreatmentDescLoadAll";
				meta.spLoadByPrimaryKey = "proc_ReasonsForTreatmentDescLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ReasonsForTreatmentDescMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
