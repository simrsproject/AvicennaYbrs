/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 2/1/2019 7:48:48 PM
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
	abstract public class esInhealthSJPDetailCollection : esEntityCollectionWAuditLog
	{
		public esInhealthSJPDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "InhealthSJPDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esInhealthSJPDetailQuery query)
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
			this.InitQuery(query as esInhealthSJPDetailQuery);
		}
		#endregion
		
		virtual public InhealthSJPDetail DetachEntity(InhealthSJPDetail entity)
		{
			return base.DetachEntity(entity) as InhealthSJPDetail;
		}
		
		virtual public InhealthSJPDetail AttachEntity(InhealthSJPDetail entity)
		{
			return base.AttachEntity(entity) as InhealthSJPDetail;
		}
		
		virtual public void Combine(InhealthSJPDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public InhealthSJPDetail this[int index]
		{
			get
			{
				return base[index] as InhealthSJPDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InhealthSJPDetail);
		}
	}



	[Serializable]
	abstract public class esInhealthSJPDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInhealthSJPDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esInhealthSJPDetail()
		{

		}

		public esInhealthSJPDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String nosjp, System.String idsjp)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nosjp, idsjp);
			else
				return LoadByPrimaryKeyStoredProcedure(nosjp, idsjp);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String nosjp, System.String idsjp)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nosjp, idsjp);
			else
				return LoadByPrimaryKeyStoredProcedure(nosjp, idsjp);
		}

		private bool LoadByPrimaryKeyDynamic(System.String nosjp, System.String idsjp)
		{
			esInhealthSJPDetailQuery query = this.GetDynamicQuery();
			query.Where(query.Nosjp == nosjp, query.Idsjp == idsjp);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String nosjp, System.String idsjp)
		{
			esParameters parms = new esParameters();
			parms.Add("nosjp",nosjp);			parms.Add("idsjp",idsjp);
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
						case "Nosjp": this.str.Nosjp = (string)value; break;							
						case "Idsjp": this.str.Idsjp = (string)value; break;							
						case "Tanggalmasuk": this.str.Tanggalmasuk = (string)value; break;							
						case "Tanggalkeluar": this.str.Tanggalkeluar = (string)value; break;							
						case "Kodejenpelruangrawat": this.str.Kodejenpelruangrawat = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Tanggalmasuk":
						
							if (value == null || value is System.DateTime)
								this.Tanggalmasuk = (System.DateTime?)value;
							break;
						
						case "Tanggalkeluar":
						
							if (value == null || value is System.DateTime)
								this.Tanggalkeluar = (System.DateTime?)value;
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
		/// Maps to InhealthSJPDetail.nosjp
		/// </summary>
		virtual public System.String Nosjp
		{
			get
			{
				return base.GetSystemString(InhealthSJPDetailMetadata.ColumnNames.Nosjp);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPDetailMetadata.ColumnNames.Nosjp, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJPDetail.idsjp
		/// </summary>
		virtual public System.String Idsjp
		{
			get
			{
				return base.GetSystemString(InhealthSJPDetailMetadata.ColumnNames.Idsjp);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPDetailMetadata.ColumnNames.Idsjp, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJPDetail.tanggalmasuk
		/// </summary>
		virtual public System.DateTime? Tanggalmasuk
		{
			get
			{
				return base.GetSystemDateTime(InhealthSJPDetailMetadata.ColumnNames.Tanggalmasuk);
			}
			
			set
			{
				base.SetSystemDateTime(InhealthSJPDetailMetadata.ColumnNames.Tanggalmasuk, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJPDetail.tanggalkeluar
		/// </summary>
		virtual public System.DateTime? Tanggalkeluar
		{
			get
			{
				return base.GetSystemDateTime(InhealthSJPDetailMetadata.ColumnNames.Tanggalkeluar);
			}
			
			set
			{
				base.SetSystemDateTime(InhealthSJPDetailMetadata.ColumnNames.Tanggalkeluar, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJPDetail.kodejenpelruangrawat
		/// </summary>
		virtual public System.String Kodejenpelruangrawat
		{
			get
			{
				return base.GetSystemString(InhealthSJPDetailMetadata.ColumnNames.Kodejenpelruangrawat);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPDetailMetadata.ColumnNames.Kodejenpelruangrawat, value);
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
			public esStrings(esInhealthSJPDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Nosjp
			{
				get
				{
					System.String data = entity.Nosjp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nosjp = null;
					else entity.Nosjp = Convert.ToString(value);
				}
			}
				
			public System.String Idsjp
			{
				get
				{
					System.String data = entity.Idsjp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Idsjp = null;
					else entity.Idsjp = Convert.ToString(value);
				}
			}
				
			public System.String Tanggalmasuk
			{
				get
				{
					System.DateTime? data = entity.Tanggalmasuk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tanggalmasuk = null;
					else entity.Tanggalmasuk = Convert.ToDateTime(value);
				}
			}
				
			public System.String Tanggalkeluar
			{
				get
				{
					System.DateTime? data = entity.Tanggalkeluar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tanggalkeluar = null;
					else entity.Tanggalkeluar = Convert.ToDateTime(value);
				}
			}
				
			public System.String Kodejenpelruangrawat
			{
				get
				{
					System.String data = entity.Kodejenpelruangrawat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kodejenpelruangrawat = null;
					else entity.Kodejenpelruangrawat = Convert.ToString(value);
				}
			}
			

			private esInhealthSJPDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInhealthSJPDetailQuery query)
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
				throw new Exception("esInhealthSJPDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class InhealthSJPDetail : esInhealthSJPDetail
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
	abstract public class esInhealthSJPDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return InhealthSJPDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem Nosjp
		{
			get
			{
				return new esQueryItem(this, InhealthSJPDetailMetadata.ColumnNames.Nosjp, esSystemType.String);
			}
		} 
		
		public esQueryItem Idsjp
		{
			get
			{
				return new esQueryItem(this, InhealthSJPDetailMetadata.ColumnNames.Idsjp, esSystemType.String);
			}
		} 
		
		public esQueryItem Tanggalmasuk
		{
			get
			{
				return new esQueryItem(this, InhealthSJPDetailMetadata.ColumnNames.Tanggalmasuk, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Tanggalkeluar
		{
			get
			{
				return new esQueryItem(this, InhealthSJPDetailMetadata.ColumnNames.Tanggalkeluar, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Kodejenpelruangrawat
		{
			get
			{
				return new esQueryItem(this, InhealthSJPDetailMetadata.ColumnNames.Kodejenpelruangrawat, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InhealthSJPDetailCollection")]
	public partial class InhealthSJPDetailCollection : esInhealthSJPDetailCollection, IEnumerable<InhealthSJPDetail>
	{
		public InhealthSJPDetailCollection()
		{

		}
		
		public static implicit operator List<InhealthSJPDetail>(InhealthSJPDetailCollection coll)
		{
			List<InhealthSJPDetail> list = new List<InhealthSJPDetail>();
			
			foreach (InhealthSJPDetail emp in coll)
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
				return  InhealthSJPDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InhealthSJPDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InhealthSJPDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InhealthSJPDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public InhealthSJPDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InhealthSJPDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(InhealthSJPDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public InhealthSJPDetail AddNew()
		{
			InhealthSJPDetail entity = base.AddNewEntity() as InhealthSJPDetail;
			
			return entity;
		}

		public InhealthSJPDetail FindByPrimaryKey(System.String nosjp, System.String idsjp)
		{
			return base.FindByPrimaryKey(nosjp, idsjp) as InhealthSJPDetail;
		}


		#region IEnumerable<InhealthSJPDetail> Members

		IEnumerator<InhealthSJPDetail> IEnumerable<InhealthSJPDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as InhealthSJPDetail;
			}
		}

		#endregion
		
		private InhealthSJPDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InhealthSJPDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("InhealthSJPDetail ({Nosjp},{Idsjp})")]
	[Serializable]
	public partial class InhealthSJPDetail : esInhealthSJPDetail
	{
		public InhealthSJPDetail()
		{

		}
	
		public InhealthSJPDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InhealthSJPDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esInhealthSJPDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InhealthSJPDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public InhealthSJPDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InhealthSJPDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(InhealthSJPDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private InhealthSJPDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class InhealthSJPDetailQuery : esInhealthSJPDetailQuery
	{
		public InhealthSJPDetailQuery()
		{

		}		
		
		public InhealthSJPDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "InhealthSJPDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class InhealthSJPDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InhealthSJPDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InhealthSJPDetailMetadata.ColumnNames.Nosjp, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPDetailMetadata.PropertyNames.Nosjp;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPDetailMetadata.ColumnNames.Idsjp, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPDetailMetadata.PropertyNames.Idsjp;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPDetailMetadata.ColumnNames.Tanggalmasuk, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InhealthSJPDetailMetadata.PropertyNames.Tanggalmasuk;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPDetailMetadata.ColumnNames.Tanggalkeluar, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InhealthSJPDetailMetadata.PropertyNames.Tanggalkeluar;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPDetailMetadata.ColumnNames.Kodejenpelruangrawat, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPDetailMetadata.PropertyNames.Kodejenpelruangrawat;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public InhealthSJPDetailMetadata Meta()
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
			 public const string Nosjp = "nosjp";
			 public const string Idsjp = "idsjp";
			 public const string Tanggalmasuk = "tanggalmasuk";
			 public const string Tanggalkeluar = "tanggalkeluar";
			 public const string Kodejenpelruangrawat = "kodejenpelruangrawat";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Nosjp = "Nosjp";
			 public const string Idsjp = "Idsjp";
			 public const string Tanggalmasuk = "Tanggalmasuk";
			 public const string Tanggalkeluar = "Tanggalkeluar";
			 public const string Kodejenpelruangrawat = "Kodejenpelruangrawat";
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
			lock (typeof(InhealthSJPDetailMetadata))
			{
				if(InhealthSJPDetailMetadata.mapDelegates == null)
				{
					InhealthSJPDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (InhealthSJPDetailMetadata.meta == null)
				{
					InhealthSJPDetailMetadata.meta = new InhealthSJPDetailMetadata();
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
				

				meta.AddTypeMap("Nosjp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Idsjp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Tanggalmasuk", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("Tanggalkeluar", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("Kodejenpelruangrawat", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "InhealthSJPDetail";
				meta.Destination = "InhealthSJPDetail";
				
				meta.spInsert = "proc_InhealthSJPDetailInsert";				
				meta.spUpdate = "proc_InhealthSJPDetailUpdate";		
				meta.spDelete = "proc_InhealthSJPDetailDelete";
				meta.spLoadAll = "proc_InhealthSJPDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_InhealthSJPDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InhealthSJPDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
