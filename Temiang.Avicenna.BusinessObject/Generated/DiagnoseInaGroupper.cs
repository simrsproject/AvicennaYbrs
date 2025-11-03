/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/8/2022 2:33:12 PM
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
	abstract public class esDiagnoseInaGroupperCollection : esEntityCollectionWAuditLog
	{
		public esDiagnoseInaGroupperCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DiagnoseInaGroupperCollection";
		}

		#region Query Logic
		protected void InitQuery(esDiagnoseInaGroupperQuery query)
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
			this.InitQuery(query as esDiagnoseInaGroupperQuery);
		}
		#endregion
		
		virtual public DiagnoseInaGroupper DetachEntity(DiagnoseInaGroupper entity)
		{
			return base.DetachEntity(entity) as DiagnoseInaGroupper;
		}
		
		virtual public DiagnoseInaGroupper AttachEntity(DiagnoseInaGroupper entity)
		{
			return base.AttachEntity(entity) as DiagnoseInaGroupper;
		}
		
		virtual public void Combine(DiagnoseInaGroupperCollection collection)
		{
			base.Combine(collection);
		}
		
		new public DiagnoseInaGroupper this[int index]
		{
			get
			{
				return base[index] as DiagnoseInaGroupper;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(DiagnoseInaGroupper);
		}
	}



	[Serializable]
	abstract public class esDiagnoseInaGroupper : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDiagnoseInaGroupperQuery GetDynamicQuery()
		{
			return null;
		}

		public esDiagnoseInaGroupper()
		{

		}

		public esDiagnoseInaGroupper(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String diagnoseID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(diagnoseID);
			else
				return LoadByPrimaryKeyStoredProcedure(diagnoseID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String diagnoseID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(diagnoseID);
			else
				return LoadByPrimaryKeyStoredProcedure(diagnoseID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String diagnoseID)
		{
			esDiagnoseInaGroupperQuery query = this.GetDynamicQuery();
			query.Where(query.DiagnoseID == diagnoseID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String diagnoseID)
		{
			esParameters parms = new esParameters();
			parms.Add("DiagnoseID",diagnoseID);
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
						case "DiagnoseID": this.str.DiagnoseID = (string)value; break;							
						case "DtdNo": this.str.DtdNo = (string)value; break;							
						case "DiagnoseName": this.str.DiagnoseName = (string)value; break;							
						case "IsChronicDisease": this.str.IsChronicDisease = (string)value; break;							
						case "IsDisease": this.str.IsDisease = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "Synonym": this.str.Synonym = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsChronicDisease":
						
							if (value == null || value is System.Boolean)
								this.IsChronicDisease = (System.Boolean?)value;
							break;
						
						case "IsDisease":
						
							if (value == null || value is System.Boolean)
								this.IsDisease = (System.Boolean?)value;
							break;
						
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
		/// Maps to DiagnoseInaGroupper.DiagnoseID
		/// </summary>
		virtual public System.String DiagnoseID
		{
			get
			{
				return base.GetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.DiagnoseID);
			}
			
			set
			{
				base.SetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.DiagnoseID, value);
			}
		}
		
		/// <summary>
		/// Maps to DiagnoseInaGroupper.DtdNo
		/// </summary>
		virtual public System.String DtdNo
		{
			get
			{
				return base.GetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.DtdNo);
			}
			
			set
			{
				base.SetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.DtdNo, value);
			}
		}
		
		/// <summary>
		/// Maps to DiagnoseInaGroupper.DiagnoseName
		/// </summary>
		virtual public System.String DiagnoseName
		{
			get
			{
				return base.GetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.DiagnoseName);
			}
			
			set
			{
				base.SetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.DiagnoseName, value);
			}
		}
		
		/// <summary>
		/// Maps to DiagnoseInaGroupper.IsChronicDisease
		/// </summary>
		virtual public System.Boolean? IsChronicDisease
		{
			get
			{
				return base.GetSystemBoolean(DiagnoseInaGroupperMetadata.ColumnNames.IsChronicDisease);
			}
			
			set
			{
				base.SetSystemBoolean(DiagnoseInaGroupperMetadata.ColumnNames.IsChronicDisease, value);
			}
		}
		
		/// <summary>
		/// Maps to DiagnoseInaGroupper.IsDisease
		/// </summary>
		virtual public System.Boolean? IsDisease
		{
			get
			{
				return base.GetSystemBoolean(DiagnoseInaGroupperMetadata.ColumnNames.IsDisease);
			}
			
			set
			{
				base.SetSystemBoolean(DiagnoseInaGroupperMetadata.ColumnNames.IsDisease, value);
			}
		}
		
		/// <summary>
		/// Maps to DiagnoseInaGroupper.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(DiagnoseInaGroupperMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(DiagnoseInaGroupperMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to DiagnoseInaGroupper.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DiagnoseInaGroupperMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DiagnoseInaGroupperMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to DiagnoseInaGroupper.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to DiagnoseInaGroupper.Synonym
		/// </summary>
		virtual public System.String Synonym
		{
			get
			{
				return base.GetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.Synonym);
			}
			
			set
			{
				base.SetSystemString(DiagnoseInaGroupperMetadata.ColumnNames.Synonym, value);
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
			public esStrings(esDiagnoseInaGroupper entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DiagnoseID
			{
				get
				{
					System.String data = entity.DiagnoseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseID = null;
					else entity.DiagnoseID = Convert.ToString(value);
				}
			}
				
			public System.String DtdNo
			{
				get
				{
					System.String data = entity.DtdNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DtdNo = null;
					else entity.DtdNo = Convert.ToString(value);
				}
			}
				
			public System.String DiagnoseName
			{
				get
				{
					System.String data = entity.DiagnoseName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseName = null;
					else entity.DiagnoseName = Convert.ToString(value);
				}
			}
				
			public System.String IsChronicDisease
			{
				get
				{
					System.Boolean? data = entity.IsChronicDisease;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsChronicDisease = null;
					else entity.IsChronicDisease = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsDisease
			{
				get
				{
					System.Boolean? data = entity.IsDisease;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDisease = null;
					else entity.IsDisease = Convert.ToBoolean(value);
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
				
			public System.String Synonym
			{
				get
				{
					System.String data = entity.Synonym;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Synonym = null;
					else entity.Synonym = Convert.ToString(value);
				}
			}
			

			private esDiagnoseInaGroupper entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDiagnoseInaGroupperQuery query)
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
				throw new Exception("esDiagnoseInaGroupper can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esDiagnoseInaGroupperQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DiagnoseInaGroupperMetadata.Meta();
			}
		}	
		

		public esQueryItem DiagnoseID
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.DiagnoseID, esSystemType.String);
			}
		} 
		
		public esQueryItem DtdNo
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.DtdNo, esSystemType.String);
			}
		} 
		
		public esQueryItem DiagnoseName
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.DiagnoseName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsChronicDisease
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.IsChronicDisease, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsDisease
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.IsDisease, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem Synonym
		{
			get
			{
				return new esQueryItem(this, DiagnoseInaGroupperMetadata.ColumnNames.Synonym, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DiagnoseInaGroupperCollection")]
	public partial class DiagnoseInaGroupperCollection : esDiagnoseInaGroupperCollection, IEnumerable<DiagnoseInaGroupper>
	{
		public DiagnoseInaGroupperCollection()
		{

		}
		
		public static implicit operator List<DiagnoseInaGroupper>(DiagnoseInaGroupperCollection coll)
		{
			List<DiagnoseInaGroupper> list = new List<DiagnoseInaGroupper>();
			
			foreach (DiagnoseInaGroupper emp in coll)
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
				return  DiagnoseInaGroupperMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DiagnoseInaGroupperQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new DiagnoseInaGroupper(row);
		}

		override protected esEntity CreateEntity()
		{
			return new DiagnoseInaGroupper();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DiagnoseInaGroupperQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DiagnoseInaGroupperQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DiagnoseInaGroupperQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public DiagnoseInaGroupper AddNew()
		{
			DiagnoseInaGroupper entity = base.AddNewEntity() as DiagnoseInaGroupper;
			
			return entity;
		}

		public DiagnoseInaGroupper FindByPrimaryKey(System.String diagnoseID)
		{
			return base.FindByPrimaryKey(diagnoseID) as DiagnoseInaGroupper;
		}


		#region IEnumerable<DiagnoseInaGroupper> Members

		IEnumerator<DiagnoseInaGroupper> IEnumerable<DiagnoseInaGroupper>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as DiagnoseInaGroupper;
			}
		}

		#endregion
		
		private DiagnoseInaGroupperQuery query;
	}


	/// <summary>
	/// Encapsulates the 'DiagnoseInaGroupper' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("DiagnoseInaGroupper ({DiagnoseID})")]
	[Serializable]
	public partial class DiagnoseInaGroupper : esDiagnoseInaGroupper
	{
		public DiagnoseInaGroupper()
		{

		}
	
		public DiagnoseInaGroupper(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DiagnoseInaGroupperMetadata.Meta();
			}
		}
		
		
		
		override protected esDiagnoseInaGroupperQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DiagnoseInaGroupperQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DiagnoseInaGroupperQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DiagnoseInaGroupperQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DiagnoseInaGroupperQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DiagnoseInaGroupperQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DiagnoseInaGroupperQuery : esDiagnoseInaGroupperQuery
	{
		public DiagnoseInaGroupperQuery()
		{

		}		
		
		public DiagnoseInaGroupperQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DiagnoseInaGroupperQuery";
        }
		
			
	}


	[Serializable]
	public partial class DiagnoseInaGroupperMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DiagnoseInaGroupperMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.DiagnoseID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.DiagnoseID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.DtdNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.DtdNo;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.DiagnoseName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.DiagnoseName;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.IsChronicDisease, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.IsChronicDisease;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.IsDisease, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.IsDisease;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.IsActive;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DiagnoseInaGroupperMetadata.ColumnNames.Synonym, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = DiagnoseInaGroupperMetadata.PropertyNames.Synonym;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DiagnoseInaGroupperMetadata Meta()
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
			 public const string DiagnoseID = "DiagnoseID";
			 public const string DtdNo = "DtdNo";
			 public const string DiagnoseName = "DiagnoseName";
			 public const string IsChronicDisease = "IsChronicDisease";
			 public const string IsDisease = "IsDisease";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string Synonym = "Synonym";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DiagnoseID = "DiagnoseID";
			 public const string DtdNo = "DtdNo";
			 public const string DiagnoseName = "DiagnoseName";
			 public const string IsChronicDisease = "IsChronicDisease";
			 public const string IsDisease = "IsDisease";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string Synonym = "Synonym";
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
			lock (typeof(DiagnoseInaGroupperMetadata))
			{
				if(DiagnoseInaGroupperMetadata.mapDelegates == null)
				{
					DiagnoseInaGroupperMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DiagnoseInaGroupperMetadata.meta == null)
				{
					DiagnoseInaGroupperMetadata.meta = new DiagnoseInaGroupperMetadata();
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
				

				meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DtdNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnoseName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsChronicDisease", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsDisease", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Synonym", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "DiagnoseInaGroupper";
				meta.Destination = "DiagnoseInaGroupper";
				
				meta.spInsert = "proc_DiagnoseInaGroupperInsert";				
				meta.spUpdate = "proc_DiagnoseInaGroupperUpdate";		
				meta.spDelete = "proc_DiagnoseInaGroupperDelete";
				meta.spLoadAll = "proc_DiagnoseInaGroupperLoadAll";
				meta.spLoadByPrimaryKey = "proc_DiagnoseInaGroupperLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DiagnoseInaGroupperMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
