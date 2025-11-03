/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/25/2016 10:27:23 AM
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
	abstract public class esVwRegistrationForMappingCOACollection : esEntityCollectionWAuditLog
	{
		public esVwRegistrationForMappingCOACollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "VwRegistrationForMappingCOACollection";
		}

		#region Query Logic
		protected void InitQuery(esVwRegistrationForMappingCOAQuery query)
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
			this.InitQuery(query as esVwRegistrationForMappingCOAQuery);
		}
		#endregion
		
		virtual public VwRegistrationForMappingCOA DetachEntity(VwRegistrationForMappingCOA entity)
		{
			return base.DetachEntity(entity) as VwRegistrationForMappingCOA;
		}
		
		virtual public VwRegistrationForMappingCOA AttachEntity(VwRegistrationForMappingCOA entity)
		{
			return base.AttachEntity(entity) as VwRegistrationForMappingCOA;
		}
		
		virtual public void Combine(VwRegistrationForMappingCOACollection collection)
		{
			base.Combine(collection);
		}
		
		new public VwRegistrationForMappingCOA this[int index]
		{
			get
			{
				return base[index] as VwRegistrationForMappingCOA;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(VwRegistrationForMappingCOA);
		}
	}



	[Serializable]
	abstract public class esVwRegistrationForMappingCOA : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esVwRegistrationForMappingCOAQuery GetDynamicQuery()
		{
			return null;
		}

		public esVwRegistrationForMappingCOA()
		{

		}

		public esVwRegistrationForMappingCOA(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "SRRegistrationType": this.str.SRRegistrationType = (string)value; break;
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
		/// Maps to vw_RegistrationForMappingCOA.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(VwRegistrationForMappingCOAMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(VwRegistrationForMappingCOAMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to vw_RegistrationForMappingCOA.SRRegistrationType
		/// </summary>
		virtual public System.String SRRegistrationType
		{
			get
			{
				return base.GetSystemString(VwRegistrationForMappingCOAMetadata.ColumnNames.SRRegistrationType);
			}
			
			set
			{
				base.SetSystemString(VwRegistrationForMappingCOAMetadata.ColumnNames.SRRegistrationType, value);
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
			public esStrings(esVwRegistrationForMappingCOA entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String SRRegistrationType
			{
				get
				{
					System.String data = entity.SRRegistrationType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRRegistrationType = null;
					else entity.SRRegistrationType = Convert.ToString(value);
				}
			}
			

			private esVwRegistrationForMappingCOA entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esVwRegistrationForMappingCOAQuery query)
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
				throw new Exception("esVwRegistrationForMappingCOA can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esVwRegistrationForMappingCOAQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return VwRegistrationForMappingCOAMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, VwRegistrationForMappingCOAMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SRRegistrationType
		{
			get
			{
				return new esQueryItem(this, VwRegistrationForMappingCOAMetadata.ColumnNames.SRRegistrationType, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("VwRegistrationForMappingCOACollection")]
	public partial class VwRegistrationForMappingCOACollection : esVwRegistrationForMappingCOACollection, IEnumerable<VwRegistrationForMappingCOA>
	{
		public VwRegistrationForMappingCOACollection()
		{

		}
		
		public static implicit operator List<VwRegistrationForMappingCOA>(VwRegistrationForMappingCOACollection coll)
		{
			List<VwRegistrationForMappingCOA> list = new List<VwRegistrationForMappingCOA>();
			
			foreach (VwRegistrationForMappingCOA emp in coll)
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
				return  VwRegistrationForMappingCOAMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwRegistrationForMappingCOAQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new VwRegistrationForMappingCOA(row);
		}

		override protected esEntity CreateEntity()
		{
			return new VwRegistrationForMappingCOA();
		}
		
		
		override public bool LoadAll()
		{
			return base.LoadAll(esSqlAccessType.DynamicSQL);
		}	
		
		#endregion


		[BrowsableAttribute( false )]
		public VwRegistrationForMappingCOAQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwRegistrationForMappingCOAQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(VwRegistrationForMappingCOAQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public VwRegistrationForMappingCOA AddNew()
		{
			VwRegistrationForMappingCOA entity = base.AddNewEntity() as VwRegistrationForMappingCOA;
			
			return entity;
		}


		#region IEnumerable<VwRegistrationForMappingCOA> Members

		IEnumerator<VwRegistrationForMappingCOA> IEnumerable<VwRegistrationForMappingCOA>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as VwRegistrationForMappingCOA;
			}
		}

		#endregion
		
		private VwRegistrationForMappingCOAQuery query;
	}


	/// <summary>
	/// Encapsulates the 'vw_RegistrationForMappingCOA' view
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("VwRegistrationForMappingCOA ()")]
	[Serializable]
	public partial class VwRegistrationForMappingCOA : esVwRegistrationForMappingCOA
	{
		public VwRegistrationForMappingCOA()
		{

		}
	
		public VwRegistrationForMappingCOA(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return VwRegistrationForMappingCOAMetadata.Meta();
			}
		}
		
		
		
		override protected esVwRegistrationForMappingCOAQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new VwRegistrationForMappingCOAQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public VwRegistrationForMappingCOAQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new VwRegistrationForMappingCOAQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(VwRegistrationForMappingCOAQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private VwRegistrationForMappingCOAQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class VwRegistrationForMappingCOAQuery : esVwRegistrationForMappingCOAQuery
	{
		public VwRegistrationForMappingCOAQuery()
		{

		}		
		
		public VwRegistrationForMappingCOAQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "VwRegistrationForMappingCOAQuery";
        }
		
			
	}


	[Serializable]
	public partial class VwRegistrationForMappingCOAMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected VwRegistrationForMappingCOAMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(VwRegistrationForMappingCOAMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = VwRegistrationForMappingCOAMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(VwRegistrationForMappingCOAMetadata.ColumnNames.SRRegistrationType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = VwRegistrationForMappingCOAMetadata.PropertyNames.SRRegistrationType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public VwRegistrationForMappingCOAMetadata Meta()
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
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRRegistrationType = "SRRegistrationType";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SRRegistrationType = "SRRegistrationType";
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
			lock (typeof(VwRegistrationForMappingCOAMetadata))
			{
				if(VwRegistrationForMappingCOAMetadata.mapDelegates == null)
				{
					VwRegistrationForMappingCOAMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (VwRegistrationForMappingCOAMetadata.meta == null)
				{
					VwRegistrationForMappingCOAMetadata.meta = new VwRegistrationForMappingCOAMetadata();
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
				

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRRegistrationType", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "vw_RegistrationForMappingCOA";
				meta.Destination = "vw_RegistrationForMappingCOA";
				
				meta.spInsert = "proc_vw_RegistrationForMappingCOAInsert";				
				meta.spUpdate = "proc_vw_RegistrationForMappingCOAUpdate";		
				meta.spDelete = "proc_vw_RegistrationForMappingCOADelete";
				meta.spLoadAll = "proc_vw_RegistrationForMappingCOALoadAll";
				meta.spLoadByPrimaryKey = "proc_vw_RegistrationForMappingCOALoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private VwRegistrationForMappingCOAMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
