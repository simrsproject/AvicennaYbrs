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
	abstract public class esRecruitmentMethodCollection : esEntityCollectionWAuditLog
	{
		public esRecruitmentMethodCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RecruitmentMethodCollection";
		}

		#region Query Logic
		protected void InitQuery(esRecruitmentMethodQuery query)
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
			this.InitQuery(query as esRecruitmentMethodQuery);
		}
		#endregion
		
		virtual public RecruitmentMethod DetachEntity(RecruitmentMethod entity)
		{
			return base.DetachEntity(entity) as RecruitmentMethod;
		}
		
		virtual public RecruitmentMethod AttachEntity(RecruitmentMethod entity)
		{
			return base.AttachEntity(entity) as RecruitmentMethod;
		}
		
		virtual public void Combine(RecruitmentMethodCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RecruitmentMethod this[int index]
		{
			get
			{
				return base[index] as RecruitmentMethod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RecruitmentMethod);
		}
	}



	[Serializable]
	abstract public class esRecruitmentMethod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRecruitmentMethodQuery GetDynamicQuery()
		{
			return null;
		}

		public esRecruitmentMethod()
		{

		}

		public esRecruitmentMethod(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 recruitmentMethodID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recruitmentMethodID);
			else
				return LoadByPrimaryKeyStoredProcedure(recruitmentMethodID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 recruitmentMethodID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(recruitmentMethodID);
			else
				return LoadByPrimaryKeyStoredProcedure(recruitmentMethodID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 recruitmentMethodID)
		{
			esRecruitmentMethodQuery query = this.GetDynamicQuery();
			query.Where(query.RecruitmentMethodID == recruitmentMethodID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 recruitmentMethodID)
		{
			esParameters parms = new esParameters();
			parms.Add("RecruitmentMethodID",recruitmentMethodID);
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
						case "RecruitmentMethodID": this.str.RecruitmentMethodID = (string)value; break;							
						case "PersonnelRequisitionID": this.str.PersonnelRequisitionID = (string)value; break;							
						case "SRRecruitmentMethod": this.str.SRRecruitmentMethod = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RecruitmentMethodID":
						
							if (value == null || value is System.Int32)
								this.RecruitmentMethodID = (System.Int32?)value;
							break;
						
						case "PersonnelRequisitionID":
						
							if (value == null || value is System.Int32)
								this.PersonnelRequisitionID = (System.Int32?)value;
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
		/// Maps to RecruitmentMethod.RecruitmentMethodID
		/// </summary>
		virtual public System.Int32? RecruitmentMethodID
		{
			get
			{
				return base.GetSystemInt32(RecruitmentMethodMetadata.ColumnNames.RecruitmentMethodID);
			}
			
			set
			{
				base.SetSystemInt32(RecruitmentMethodMetadata.ColumnNames.RecruitmentMethodID, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentMethod.PersonnelRequisitionID
		/// </summary>
		virtual public System.Int32? PersonnelRequisitionID
		{
			get
			{
				return base.GetSystemInt32(RecruitmentMethodMetadata.ColumnNames.PersonnelRequisitionID);
			}
			
			set
			{
				base.SetSystemInt32(RecruitmentMethodMetadata.ColumnNames.PersonnelRequisitionID, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentMethod.SRRecruitmentMethod
		/// </summary>
		virtual public System.String SRRecruitmentMethod
		{
			get
			{
				return base.GetSystemString(RecruitmentMethodMetadata.ColumnNames.SRRecruitmentMethod);
			}
			
			set
			{
				base.SetSystemString(RecruitmentMethodMetadata.ColumnNames.SRRecruitmentMethod, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentMethod.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RecruitmentMethodMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RecruitmentMethodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RecruitmentMethod.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RecruitmentMethodMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RecruitmentMethodMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRecruitmentMethod entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RecruitmentMethodID
			{
				get
				{
					System.Int32? data = entity.RecruitmentMethodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RecruitmentMethodID = null;
					else entity.RecruitmentMethodID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonnelRequisitionID
			{
				get
				{
					System.Int32? data = entity.PersonnelRequisitionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonnelRequisitionID = null;
					else entity.PersonnelRequisitionID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRRecruitmentMethod
			{
				get
				{
					System.String data = entity.SRRecruitmentMethod;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRecruitmentMethod = null;
					else entity.SRRecruitmentMethod = Convert.ToString(value);
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
			

			private esRecruitmentMethod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRecruitmentMethodQuery query)
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
				throw new Exception("esRecruitmentMethod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RecruitmentMethod : esRecruitmentMethod
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
	abstract public class esRecruitmentMethodQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RecruitmentMethodMetadata.Meta();
			}
		}	
		

		public esQueryItem RecruitmentMethodID
		{
			get
			{
				return new esQueryItem(this, RecruitmentMethodMetadata.ColumnNames.RecruitmentMethodID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonnelRequisitionID
		{
			get
			{
				return new esQueryItem(this, RecruitmentMethodMetadata.ColumnNames.PersonnelRequisitionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRRecruitmentMethod
		{
			get
			{
				return new esQueryItem(this, RecruitmentMethodMetadata.ColumnNames.SRRecruitmentMethod, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RecruitmentMethodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RecruitmentMethodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RecruitmentMethodCollection")]
	public partial class RecruitmentMethodCollection : esRecruitmentMethodCollection, IEnumerable<RecruitmentMethod>
	{
		public RecruitmentMethodCollection()
		{

		}
		
		public static implicit operator List<RecruitmentMethod>(RecruitmentMethodCollection coll)
		{
			List<RecruitmentMethod> list = new List<RecruitmentMethod>();
			
			foreach (RecruitmentMethod emp in coll)
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
				return  RecruitmentMethodMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RecruitmentMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RecruitmentMethod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RecruitmentMethod();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RecruitmentMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RecruitmentMethodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RecruitmentMethodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RecruitmentMethod AddNew()
		{
			RecruitmentMethod entity = base.AddNewEntity() as RecruitmentMethod;
			
			return entity;
		}

		public RecruitmentMethod FindByPrimaryKey(System.Int32 recruitmentMethodID)
		{
			return base.FindByPrimaryKey(recruitmentMethodID) as RecruitmentMethod;
		}


		#region IEnumerable<RecruitmentMethod> Members

		IEnumerator<RecruitmentMethod> IEnumerable<RecruitmentMethod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RecruitmentMethod;
			}
		}

		#endregion
		
		private RecruitmentMethodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RecruitmentMethod' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RecruitmentMethod ({RecruitmentMethodID})")]
	[Serializable]
	public partial class RecruitmentMethod : esRecruitmentMethod
	{
		public RecruitmentMethod()
		{

		}
	
		public RecruitmentMethod(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RecruitmentMethodMetadata.Meta();
			}
		}
		
		
		
		override protected esRecruitmentMethodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RecruitmentMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RecruitmentMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RecruitmentMethodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RecruitmentMethodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RecruitmentMethodQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RecruitmentMethodQuery : esRecruitmentMethodQuery
	{
		public RecruitmentMethodQuery()
		{

		}		
		
		public RecruitmentMethodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RecruitmentMethodQuery";
        }
		
			
	}


	[Serializable]
	public partial class RecruitmentMethodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RecruitmentMethodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RecruitmentMethodMetadata.ColumnNames.RecruitmentMethodID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RecruitmentMethodMetadata.PropertyNames.RecruitmentMethodID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentMethodMetadata.ColumnNames.PersonnelRequisitionID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RecruitmentMethodMetadata.PropertyNames.PersonnelRequisitionID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentMethodMetadata.ColumnNames.SRRecruitmentMethod, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RecruitmentMethodMetadata.PropertyNames.SRRecruitmentMethod;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentMethodMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RecruitmentMethodMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(RecruitmentMethodMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RecruitmentMethodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RecruitmentMethodMetadata Meta()
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
			 public const string RecruitmentMethodID = "RecruitmentMethodID";
			 public const string PersonnelRequisitionID = "PersonnelRequisitionID";
			 public const string SRRecruitmentMethod = "SRRecruitmentMethod";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RecruitmentMethodID = "RecruitmentMethodID";
			 public const string PersonnelRequisitionID = "PersonnelRequisitionID";
			 public const string SRRecruitmentMethod = "SRRecruitmentMethod";
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
			lock (typeof(RecruitmentMethodMetadata))
			{
				if(RecruitmentMethodMetadata.mapDelegates == null)
				{
					RecruitmentMethodMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RecruitmentMethodMetadata.meta == null)
				{
					RecruitmentMethodMetadata.meta = new RecruitmentMethodMetadata();
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
				

				meta.AddTypeMap("RecruitmentMethodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonnelRequisitionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRRecruitmentMethod", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RecruitmentMethod";
				meta.Destination = "RecruitmentMethod";
				
				meta.spInsert = "proc_RecruitmentMethodInsert";				
				meta.spUpdate = "proc_RecruitmentMethodUpdate";		
				meta.spDelete = "proc_RecruitmentMethodDelete";
				meta.spLoadAll = "proc_RecruitmentMethodLoadAll";
				meta.spLoadByPrimaryKey = "proc_RecruitmentMethodLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RecruitmentMethodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
