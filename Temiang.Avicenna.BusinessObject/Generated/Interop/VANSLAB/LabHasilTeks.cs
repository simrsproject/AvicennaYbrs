/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/24/2018 3:45:48 AM
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



namespace Temiang.Avicenna.BusinessObject.Interop.VANSLAB
{

	[Serializable]
	abstract public class esLabHasilTeksCollection : esEntityCollectionWAuditLog
	{
		public esLabHasilTeksCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "LabHasilTeksCollection";
		}

		#region Query Logic
		protected void InitQuery(esLabHasilTeksQuery query)
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
			this.InitQuery(query as esLabHasilTeksQuery);
		}
		#endregion
		
		virtual public LabHasilTeks DetachEntity(LabHasilTeks entity)
		{
			return base.DetachEntity(entity) as LabHasilTeks;
		}
		
		virtual public LabHasilTeks AttachEntity(LabHasilTeks entity)
		{
			return base.AttachEntity(entity) as LabHasilTeks;
		}
		
		virtual public void Combine(LabHasilTeksCollection collection)
		{
			base.Combine(collection);
		}
		
		new public LabHasilTeks this[int index]
		{
			get
			{
				return base[index] as LabHasilTeks;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(LabHasilTeks);
		}
	}



	[Serializable]
	abstract public class esLabHasilTeks : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esLabHasilTeksQuery GetDynamicQuery()
		{
			return null;
		}

		public esLabHasilTeks()
		{

		}

		public esLabHasilTeks(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey()
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic();
			else
				return LoadByPrimaryKeyStoredProcedure();
		}

        //public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, )
        //{
        //    if (sqlAccessType == esSqlAccessType.DynamicSQL)
        //        return LoadByPrimaryKeyDynamic();
        //    else
        //        return LoadByPrimaryKeyStoredProcedure();
        //}

		private bool LoadByPrimaryKeyDynamic()
		{
			esLabHasilTeksQuery query = this.GetDynamicQuery();
			query.Where();
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure()
		{
			esParameters parms = new esParameters();

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
						case "NoLab": this.str.NoLab = (string)value; break;							
						case "KodeTest": this.str.KodeTest = (string)value; break;							
						case "Teks": this.str.Teks = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{

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
		/// Maps to lab_hasil_teks.no_lab
		/// </summary>
		virtual public System.String NoLab
		{
			get
			{
				return base.GetSystemString(LabHasilTeksMetadata.ColumnNames.NoLab);
			}
			
			set
			{
				base.SetSystemString(LabHasilTeksMetadata.ColumnNames.NoLab, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil_teks.kode_test
		/// </summary>
		virtual public System.String KodeTest
		{
			get
			{
				return base.GetSystemString(LabHasilTeksMetadata.ColumnNames.KodeTest);
			}
			
			set
			{
				base.SetSystemString(LabHasilTeksMetadata.ColumnNames.KodeTest, value);
			}
		}
		
		/// <summary>
		/// Maps to lab_hasil_teks.teks
		/// </summary>
		virtual public System.String Teks
		{
			get
			{
				return base.GetSystemString(LabHasilTeksMetadata.ColumnNames.Teks);
			}
			
			set
			{
				base.SetSystemString(LabHasilTeksMetadata.ColumnNames.Teks, value);
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
			public esStrings(esLabHasilTeks entity)
			{
				this.entity = entity;
			}
			
	
			public System.String NoLab
			{
				get
				{
					System.String data = entity.NoLab;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoLab = null;
					else entity.NoLab = Convert.ToString(value);
				}
			}
				
			public System.String KodeTest
			{
				get
				{
					System.String data = entity.KodeTest;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeTest = null;
					else entity.KodeTest = Convert.ToString(value);
				}
			}
				
			public System.String Teks
			{
				get
				{
					System.String data = entity.Teks;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Teks = null;
					else entity.Teks = Convert.ToString(value);
				}
			}
			

			private esLabHasilTeks entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esLabHasilTeksQuery query)
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
				throw new Exception("esLabHasilTeks can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class LabHasilTeks : esLabHasilTeks
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
	abstract public class esLabHasilTeksQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return LabHasilTeksMetadata.Meta();
			}
		}	
		

		public esQueryItem NoLab
		{
			get
			{
				return new esQueryItem(this, LabHasilTeksMetadata.ColumnNames.NoLab, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeTest
		{
			get
			{
				return new esQueryItem(this, LabHasilTeksMetadata.ColumnNames.KodeTest, esSystemType.String);
			}
		} 
		
		public esQueryItem Teks
		{
			get
			{
				return new esQueryItem(this, LabHasilTeksMetadata.ColumnNames.Teks, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("LabHasilTeksCollection")]
	public partial class LabHasilTeksCollection : esLabHasilTeksCollection, IEnumerable<LabHasilTeks>
	{
		public LabHasilTeksCollection()
		{

		}
		
		public static implicit operator List<LabHasilTeks>(LabHasilTeksCollection coll)
		{
			List<LabHasilTeks> list = new List<LabHasilTeks>();
			
			foreach (LabHasilTeks emp in coll)
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
				return  LabHasilTeksMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabHasilTeksQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new LabHasilTeks(row);
		}

		override protected esEntity CreateEntity()
		{
			return new LabHasilTeks();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public LabHasilTeksQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabHasilTeksQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(LabHasilTeksQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public LabHasilTeks AddNew()
		{
			LabHasilTeks entity = base.AddNewEntity() as LabHasilTeks;
			
			return entity;
		}

		public LabHasilTeks FindByPrimaryKey()
		{
			return base.FindByPrimaryKey() as LabHasilTeks;
		}


		#region IEnumerable<LabHasilTeks> Members

		IEnumerator<LabHasilTeks> IEnumerable<LabHasilTeks>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as LabHasilTeks;
			}
		}

		#endregion
		
		private LabHasilTeksQuery query;
	}


	/// <summary>
	/// Encapsulates the 'lab_hasil_teks' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("LabHasilTeks ()")]
	[Serializable]
	public partial class LabHasilTeks : esLabHasilTeks
	{
		public LabHasilTeks()
		{

		}
	
		public LabHasilTeks(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return LabHasilTeksMetadata.Meta();
			}
		}
		
		
		
		override protected esLabHasilTeksQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new LabHasilTeksQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public LabHasilTeksQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new LabHasilTeksQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(LabHasilTeksQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private LabHasilTeksQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class LabHasilTeksQuery : esLabHasilTeksQuery
	{
		public LabHasilTeksQuery()
		{

		}		
		
		public LabHasilTeksQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "LabHasilTeksQuery";
        }
		
			
	}


	[Serializable]
	public partial class LabHasilTeksMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected LabHasilTeksMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(LabHasilTeksMetadata.ColumnNames.NoLab, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilTeksMetadata.PropertyNames.NoLab;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilTeksMetadata.ColumnNames.KodeTest, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilTeksMetadata.PropertyNames.KodeTest;
			c.CharacterMaxLength = 6;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(LabHasilTeksMetadata.ColumnNames.Teks, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = LabHasilTeksMetadata.PropertyNames.Teks;
			c.CharacterMaxLength = 3500;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public LabHasilTeksMetadata Meta()
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
			 public const string NoLab = "no_lab";
			 public const string KodeTest = "kode_test";
			 public const string Teks = "teks";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string NoLab = "NoLab";
			 public const string KodeTest = "KodeTest";
			 public const string Teks = "Teks";
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
			lock (typeof(LabHasilTeksMetadata))
			{
				if(LabHasilTeksMetadata.mapDelegates == null)
				{
					LabHasilTeksMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (LabHasilTeksMetadata.meta == null)
				{
					LabHasilTeksMetadata.meta = new LabHasilTeksMetadata();
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
				

				meta.AddTypeMap("NoLab", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeTest", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Teks", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "lab_hasil_teks";
				meta.Destination = "lab_hasil_teks";
				
				meta.spInsert = "proc_lab_hasil_teksInsert";				
				meta.spUpdate = "proc_lab_hasil_teksUpdate";		
				meta.spDelete = "proc_lab_hasil_teksDelete";
				meta.spLoadAll = "proc_lab_hasil_teksLoadAll";
				meta.spLoadByPrimaryKey = "proc_lab_hasil_teksLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private LabHasilTeksMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
