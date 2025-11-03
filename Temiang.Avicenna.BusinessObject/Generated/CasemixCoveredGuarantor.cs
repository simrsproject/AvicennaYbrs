/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/1/2022 10:39:17 PM
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
	abstract public class esCasemixCoveredGuarantorCollection : esEntityCollectionWAuditLog
	{
		public esCasemixCoveredGuarantorCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "CasemixCoveredGuarantorCollection";
		}

		#region Query Logic
		protected void InitQuery(esCasemixCoveredGuarantorQuery query)
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
			this.InitQuery(query as esCasemixCoveredGuarantorQuery);
		}
		#endregion
		
		virtual public CasemixCoveredGuarantor DetachEntity(CasemixCoveredGuarantor entity)
		{
			return base.DetachEntity(entity) as CasemixCoveredGuarantor;
		}
		
		virtual public CasemixCoveredGuarantor AttachEntity(CasemixCoveredGuarantor entity)
		{
			return base.AttachEntity(entity) as CasemixCoveredGuarantor;
		}
		
		virtual public void Combine(CasemixCoveredGuarantorCollection collection)
		{
			base.Combine(collection);
		}
		
		new public CasemixCoveredGuarantor this[int index]
		{
			get
			{
				return base[index] as CasemixCoveredGuarantor;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(CasemixCoveredGuarantor);
		}
	}



	[Serializable]
	abstract public class esCasemixCoveredGuarantor : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esCasemixCoveredGuarantorQuery GetDynamicQuery()
		{
			return null;
		}

		public esCasemixCoveredGuarantor()
		{

		}

		public esCasemixCoveredGuarantor(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 casemixCoveredID, System.String guarantorID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(casemixCoveredID, guarantorID);
			else
				return LoadByPrimaryKeyStoredProcedure(casemixCoveredID, guarantorID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 casemixCoveredID, System.String guarantorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(casemixCoveredID, guarantorID);
			else
				return LoadByPrimaryKeyStoredProcedure(casemixCoveredID, guarantorID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 casemixCoveredID, System.String guarantorID)
		{
			esCasemixCoveredGuarantorQuery query = this.GetDynamicQuery();
			query.Where(query.CasemixCoveredID == casemixCoveredID, query.GuarantorID == guarantorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 casemixCoveredID, System.String guarantorID)
		{
			esParameters parms = new esParameters();
			parms.Add("CasemixCoveredID",casemixCoveredID);			parms.Add("GuarantorID",guarantorID);
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
						case "CasemixCoveredID": this.str.CasemixCoveredID = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "CasemixCoveredID":
						
							if (value == null || value is System.Int32)
								this.CasemixCoveredID = (System.Int32?)value;
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
		/// Maps to CasemixCoveredGuarantor.GuarantorID
		/// </summary>
		virtual public System.String GuarantorID
		{
			get
			{
				return base.GetSystemString(CasemixCoveredGuarantorMetadata.ColumnNames.GuarantorID);
			}
			
			set
			{
				base.SetSystemString(CasemixCoveredGuarantorMetadata.ColumnNames.GuarantorID, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixCoveredGuarantor.CasemixCoveredID
		/// </summary>
		virtual public System.Int32? CasemixCoveredID
		{
			get
			{
				return base.GetSystemInt32(CasemixCoveredGuarantorMetadata.ColumnNames.CasemixCoveredID);
			}
			
			set
			{
				base.SetSystemInt32(CasemixCoveredGuarantorMetadata.ColumnNames.CasemixCoveredID, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixCoveredGuarantor.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(CasemixCoveredGuarantorMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(CasemixCoveredGuarantorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to CasemixCoveredGuarantor.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(CasemixCoveredGuarantorMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(CasemixCoveredGuarantorMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esCasemixCoveredGuarantor entity)
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
				
			public System.String CasemixCoveredID
			{
				get
				{
					System.Int32? data = entity.CasemixCoveredID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CasemixCoveredID = null;
					else entity.CasemixCoveredID = Convert.ToInt32(value);
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
			

			private esCasemixCoveredGuarantor entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esCasemixCoveredGuarantorQuery query)
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
				throw new Exception("esCasemixCoveredGuarantor can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esCasemixCoveredGuarantorQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CasemixCoveredGuarantorMetadata.Meta();
			}
		}	
		

		public esQueryItem GuarantorID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredGuarantorMetadata.ColumnNames.GuarantorID, esSystemType.String);
			}
		} 
		
		public esQueryItem CasemixCoveredID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredGuarantorMetadata.ColumnNames.CasemixCoveredID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredGuarantorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, CasemixCoveredGuarantorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("CasemixCoveredGuarantorCollection")]
	public partial class CasemixCoveredGuarantorCollection : esCasemixCoveredGuarantorCollection, IEnumerable<CasemixCoveredGuarantor>
	{
		public CasemixCoveredGuarantorCollection()
		{

		}
		
		public static implicit operator List<CasemixCoveredGuarantor>(CasemixCoveredGuarantorCollection coll)
		{
			List<CasemixCoveredGuarantor> list = new List<CasemixCoveredGuarantor>();
			
			foreach (CasemixCoveredGuarantor emp in coll)
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
				return  CasemixCoveredGuarantorMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixCoveredGuarantorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new CasemixCoveredGuarantor(row);
		}

		override protected esEntity CreateEntity()
		{
			return new CasemixCoveredGuarantor();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public CasemixCoveredGuarantorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixCoveredGuarantorQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(CasemixCoveredGuarantorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public CasemixCoveredGuarantor AddNew()
		{
			CasemixCoveredGuarantor entity = base.AddNewEntity() as CasemixCoveredGuarantor;
			
			return entity;
		}

		public CasemixCoveredGuarantor FindByPrimaryKey(System.Int32 casemixCoveredID, System.String guarantorID)
		{
			return base.FindByPrimaryKey(casemixCoveredID, guarantorID) as CasemixCoveredGuarantor;
		}


		#region IEnumerable<CasemixCoveredGuarantor> Members

		IEnumerator<CasemixCoveredGuarantor> IEnumerable<CasemixCoveredGuarantor>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as CasemixCoveredGuarantor;
			}
		}

		#endregion
		
		private CasemixCoveredGuarantorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'CasemixCoveredGuarantor' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("CasemixCoveredGuarantor ({GuarantorID},{CasemixCoveredID})")]
	[Serializable]
	public partial class CasemixCoveredGuarantor : esCasemixCoveredGuarantor
	{
		public CasemixCoveredGuarantor()
		{

		}
	
		public CasemixCoveredGuarantor(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CasemixCoveredGuarantorMetadata.Meta();
			}
		}
		
		
		
		override protected esCasemixCoveredGuarantorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CasemixCoveredGuarantorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public CasemixCoveredGuarantorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CasemixCoveredGuarantorQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(CasemixCoveredGuarantorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private CasemixCoveredGuarantorQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class CasemixCoveredGuarantorQuery : esCasemixCoveredGuarantorQuery
	{
		public CasemixCoveredGuarantorQuery()
		{

		}		
		
		public CasemixCoveredGuarantorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "CasemixCoveredGuarantorQuery";
        }
		
			
	}


	[Serializable]
	public partial class CasemixCoveredGuarantorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CasemixCoveredGuarantorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CasemixCoveredGuarantorMetadata.ColumnNames.GuarantorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixCoveredGuarantorMetadata.PropertyNames.GuarantorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixCoveredGuarantorMetadata.ColumnNames.CasemixCoveredID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CasemixCoveredGuarantorMetadata.PropertyNames.CasemixCoveredID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixCoveredGuarantorMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CasemixCoveredGuarantorMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(CasemixCoveredGuarantorMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CasemixCoveredGuarantorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public CasemixCoveredGuarantorMetadata Meta()
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
			 public const string CasemixCoveredID = "CasemixCoveredID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string GuarantorID = "GuarantorID";
			 public const string CasemixCoveredID = "CasemixCoveredID";
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
			lock (typeof(CasemixCoveredGuarantorMetadata))
			{
				if(CasemixCoveredGuarantorMetadata.mapDelegates == null)
				{
					CasemixCoveredGuarantorMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CasemixCoveredGuarantorMetadata.meta == null)
				{
					CasemixCoveredGuarantorMetadata.meta = new CasemixCoveredGuarantorMetadata();
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
				meta.AddTypeMap("CasemixCoveredID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "CasemixCoveredGuarantor";
				meta.Destination = "CasemixCoveredGuarantor";
				
				meta.spInsert = "proc_CasemixCoveredGuarantorInsert";				
				meta.spUpdate = "proc_CasemixCoveredGuarantorUpdate";		
				meta.spDelete = "proc_CasemixCoveredGuarantorDelete";
				meta.spLoadAll = "proc_CasemixCoveredGuarantorLoadAll";
				meta.spLoadByPrimaryKey = "proc_CasemixCoveredGuarantorLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CasemixCoveredGuarantorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
