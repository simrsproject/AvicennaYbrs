/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/28/2015 8:21:21 PM
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
	abstract public class esNursingDiagnosaServiceUnitCollection : esEntityCollectionWAuditLog
	{
		public esNursingDiagnosaServiceUnitCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "NursingDiagnosaServiceUnitCollection";
		}

		#region Query Logic
		protected void InitQuery(esNursingDiagnosaServiceUnitQuery query)
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
			this.InitQuery(query as esNursingDiagnosaServiceUnitQuery);
		}
		#endregion
		
		virtual public NursingDiagnosaServiceUnit DetachEntity(NursingDiagnosaServiceUnit entity)
		{
			return base.DetachEntity(entity) as NursingDiagnosaServiceUnit;
		}
		
		virtual public NursingDiagnosaServiceUnit AttachEntity(NursingDiagnosaServiceUnit entity)
		{
			return base.AttachEntity(entity) as NursingDiagnosaServiceUnit;
		}
		
		virtual public void Combine(NursingDiagnosaServiceUnitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public NursingDiagnosaServiceUnit this[int index]
		{
			get
			{
				return base[index] as NursingDiagnosaServiceUnit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(NursingDiagnosaServiceUnit);
		}
	}



	[Serializable]
	abstract public class esNursingDiagnosaServiceUnit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esNursingDiagnosaServiceUnitQuery GetDynamicQuery()
		{
			return null;
		}

		public esNursingDiagnosaServiceUnit()
		{

		}

		public esNursingDiagnosaServiceUnit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String nursingDiagnosaID, System.String serviceUnitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nursingDiagnosaID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(nursingDiagnosaID, serviceUnitID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String nursingDiagnosaID, System.String serviceUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nursingDiagnosaID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(nursingDiagnosaID, serviceUnitID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String nursingDiagnosaID, System.String serviceUnitID)
		{
			esNursingDiagnosaServiceUnitQuery query = this.GetDynamicQuery();
			query.Where(query.NursingDiagnosaID == nursingDiagnosaID, query.ServiceUnitID == serviceUnitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String nursingDiagnosaID, System.String serviceUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("NursingDiagnosaID",nursingDiagnosaID);			parms.Add("ServiceUnitID",serviceUnitID);
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
						case "NursingDiagnosaID": this.str.NursingDiagnosaID = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
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
		/// Maps to NursingDiagnosaServiceUnit.NursingDiagnosaID
		/// </summary>
		virtual public System.String NursingDiagnosaID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaServiceUnitMetadata.ColumnNames.NursingDiagnosaID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaServiceUnitMetadata.ColumnNames.NursingDiagnosaID, value);
			}
		}
		
		/// <summary>
		/// Maps to NursingDiagnosaServiceUnit.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(NursingDiagnosaServiceUnitMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(NursingDiagnosaServiceUnitMetadata.ColumnNames.ServiceUnitID, value);
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
			public esStrings(esNursingDiagnosaServiceUnit entity)
			{
				this.entity = entity;
			}
			
	
			public System.String NursingDiagnosaID
			{
				get
				{
					System.String data = entity.NursingDiagnosaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NursingDiagnosaID = null;
					else entity.NursingDiagnosaID = Convert.ToString(value);
				}
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
			

			private esNursingDiagnosaServiceUnit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esNursingDiagnosaServiceUnitQuery query)
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
				throw new Exception("esNursingDiagnosaServiceUnit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esNursingDiagnosaServiceUnitQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaServiceUnitMetadata.Meta();
			}
		}	
		

		public esQueryItem NursingDiagnosaID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaServiceUnitMetadata.ColumnNames.NursingDiagnosaID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, NursingDiagnosaServiceUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("NursingDiagnosaServiceUnitCollection")]
	public partial class NursingDiagnosaServiceUnitCollection : esNursingDiagnosaServiceUnitCollection, IEnumerable<NursingDiagnosaServiceUnit>
	{
		public NursingDiagnosaServiceUnitCollection()
		{

		}
		
		public static implicit operator List<NursingDiagnosaServiceUnit>(NursingDiagnosaServiceUnitCollection coll)
		{
			List<NursingDiagnosaServiceUnit> list = new List<NursingDiagnosaServiceUnit>();
			
			foreach (NursingDiagnosaServiceUnit emp in coll)
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
				return  NursingDiagnosaServiceUnitMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new NursingDiagnosaServiceUnit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new NursingDiagnosaServiceUnit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public NursingDiagnosaServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaServiceUnitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(NursingDiagnosaServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public NursingDiagnosaServiceUnit AddNew()
		{
			NursingDiagnosaServiceUnit entity = base.AddNewEntity() as NursingDiagnosaServiceUnit;
			
			return entity;
		}

		public NursingDiagnosaServiceUnit FindByPrimaryKey(System.String nursingDiagnosaID, System.String serviceUnitID)
		{
			return base.FindByPrimaryKey(nursingDiagnosaID, serviceUnitID) as NursingDiagnosaServiceUnit;
		}


		#region IEnumerable<NursingDiagnosaServiceUnit> Members

		IEnumerator<NursingDiagnosaServiceUnit> IEnumerable<NursingDiagnosaServiceUnit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as NursingDiagnosaServiceUnit;
			}
		}

		#endregion
		
		private NursingDiagnosaServiceUnitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'NursingDiagnosaServiceUnit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("NursingDiagnosaServiceUnit ({NursingDiagnosaID},{ServiceUnitID})")]
	[Serializable]
	public partial class NursingDiagnosaServiceUnit : esNursingDiagnosaServiceUnit
	{
		public NursingDiagnosaServiceUnit()
		{

		}
	
		public NursingDiagnosaServiceUnit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return NursingDiagnosaServiceUnitMetadata.Meta();
			}
		}
		
		
		
		override protected esNursingDiagnosaServiceUnitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new NursingDiagnosaServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public NursingDiagnosaServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new NursingDiagnosaServiceUnitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(NursingDiagnosaServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private NursingDiagnosaServiceUnitQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class NursingDiagnosaServiceUnitQuery : esNursingDiagnosaServiceUnitQuery
	{
		public NursingDiagnosaServiceUnitQuery()
		{

		}		
		
		public NursingDiagnosaServiceUnitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "NursingDiagnosaServiceUnitQuery";
        }
		
			
	}


	[Serializable]
	public partial class NursingDiagnosaServiceUnitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected NursingDiagnosaServiceUnitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(NursingDiagnosaServiceUnitMetadata.ColumnNames.NursingDiagnosaID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaServiceUnitMetadata.PropertyNames.NursingDiagnosaID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(NursingDiagnosaServiceUnitMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = NursingDiagnosaServiceUnitMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public NursingDiagnosaServiceUnitMetadata Meta()
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
			 public const string NursingDiagnosaID = "NursingDiagnosaID";
			 public const string ServiceUnitID = "ServiceUnitID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string NursingDiagnosaID = "NursingDiagnosaID";
			 public const string ServiceUnitID = "ServiceUnitID";
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
			lock (typeof(NursingDiagnosaServiceUnitMetadata))
			{
				if(NursingDiagnosaServiceUnitMetadata.mapDelegates == null)
				{
					NursingDiagnosaServiceUnitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (NursingDiagnosaServiceUnitMetadata.meta == null)
				{
					NursingDiagnosaServiceUnitMetadata.meta = new NursingDiagnosaServiceUnitMetadata();
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
				

				meta.AddTypeMap("NursingDiagnosaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "NursingDiagnosaServiceUnit";
				meta.Destination = "NursingDiagnosaServiceUnit";
				
				meta.spInsert = "proc_NursingDiagnosaServiceUnitInsert";				
				meta.spUpdate = "proc_NursingDiagnosaServiceUnitUpdate";		
				meta.spDelete = "proc_NursingDiagnosaServiceUnitDelete";
				meta.spLoadAll = "proc_NursingDiagnosaServiceUnitLoadAll";
				meta.spLoadByPrimaryKey = "proc_NursingDiagnosaServiceUnitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private NursingDiagnosaServiceUnitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
