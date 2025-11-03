/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/21/2014 11:40:59 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.Inacbg
{

	[Serializable]
	abstract public class esTariff2012RsuRnRjCollection : esEntityCollectionWAuditLog
	{
		public esTariff2012RsuRnRjCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "Tariff2012RsuRnRjCollection";
		}

		#region Query Logic
		protected void InitQuery(esTariff2012RsuRnRjQuery query)
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
			this.InitQuery(query as esTariff2012RsuRnRjQuery);
		}
		#endregion
		
		virtual public Tariff2012RsuRnRj DetachEntity(Tariff2012RsuRnRj entity)
		{
			return base.DetachEntity(entity) as Tariff2012RsuRnRj;
		}
		
		virtual public Tariff2012RsuRnRj AttachEntity(Tariff2012RsuRnRj entity)
		{
			return base.AttachEntity(entity) as Tariff2012RsuRnRj;
		}
		
		virtual public void Combine(Tariff2012RsuRnRjCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Tariff2012RsuRnRj this[int index]
		{
			get
			{
				return base[index] as Tariff2012RsuRnRj;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Tariff2012RsuRnRj);
		}
	}



	[Serializable]
	abstract public class esTariff2012RsuRnRj : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esTariff2012RsuRnRjQuery GetDynamicQuery()
		{
			return null;
		}

		public esTariff2012RsuRnRj()
		{

		}

		public esTariff2012RsuRnRj(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 no)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(no);
			else
				return LoadByPrimaryKeyStoredProcedure(no);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 no)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(no);
			else
				return LoadByPrimaryKeyStoredProcedure(no);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 no)
		{
			esTariff2012RsuRnRjQuery query = this.GetDynamicQuery();
			query.Where(query.No == no);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 no)
		{
			esParameters parms = new esParameters();
			parms.Add("no",no);
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
						case "No": this.str.No = (string)value; break;							
						case "IrCode": this.str.IrCode = (string)value; break;							
						case "Code": this.str.Code = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "Alos": this.str.Alos = (string)value; break;							
						case "FinalCostWeight": this.str.FinalCostWeight = (string)value; break;							
						case "BaseRate": this.str.BaseRate = (string)value; break;							
						case "PoliBiasa": this.str.PoliBiasa = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "No":
						
							if (value == null || value is System.Int64)
								this.No = (System.Int64?)value;
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
		/// Maps to tariff_2012_rsu_rn_rj.no
		/// </summary>
		virtual public System.Int64? No
		{
			get
			{
				return base.GetSystemInt64(Tariff2012RsuRnRjMetadata.ColumnNames.No);
			}
			
			set
			{
				base.SetSystemInt64(Tariff2012RsuRnRjMetadata.ColumnNames.No, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_rj.ir_code
		/// </summary>
		virtual public System.String IrCode
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.IrCode);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.IrCode, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_rj.code
		/// </summary>
		virtual public System.String Code
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.Code);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.Code, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_rj.description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_rj.alos
		/// </summary>
		virtual public System.String Alos
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.Alos);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.Alos, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_rj.final_cost_weight
		/// </summary>
		virtual public System.String FinalCostWeight
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.FinalCostWeight);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.FinalCostWeight, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_rj.base_rate
		/// </summary>
		virtual public System.String BaseRate
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.BaseRate);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.BaseRate, value);
			}
		}
		
		/// <summary>
		/// Maps to tariff_2012_rsu_rn_rj.poli_biasa
		/// </summary>
		virtual public System.String PoliBiasa
		{
			get
			{
				return base.GetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.PoliBiasa);
			}
			
			set
			{
				base.SetSystemString(Tariff2012RsuRnRjMetadata.ColumnNames.PoliBiasa, value);
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
			public esStrings(esTariff2012RsuRnRj entity)
			{
				this.entity = entity;
			}
			
	
			public System.String No
			{
				get
				{
					System.Int64? data = entity.No;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.No = null;
					else entity.No = Convert.ToInt64(value);
				}
			}
				
			public System.String IrCode
			{
				get
				{
					System.String data = entity.IrCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IrCode = null;
					else entity.IrCode = Convert.ToString(value);
				}
			}
				
			public System.String Code
			{
				get
				{
					System.String data = entity.Code;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Code = null;
					else entity.Code = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String Alos
			{
				get
				{
					System.String data = entity.Alos;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Alos = null;
					else entity.Alos = Convert.ToString(value);
				}
			}
				
			public System.String FinalCostWeight
			{
				get
				{
					System.String data = entity.FinalCostWeight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FinalCostWeight = null;
					else entity.FinalCostWeight = Convert.ToString(value);
				}
			}
				
			public System.String BaseRate
			{
				get
				{
					System.String data = entity.BaseRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BaseRate = null;
					else entity.BaseRate = Convert.ToString(value);
				}
			}
				
			public System.String PoliBiasa
			{
				get
				{
					System.String data = entity.PoliBiasa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PoliBiasa = null;
					else entity.PoliBiasa = Convert.ToString(value);
				}
			}
			

			private esTariff2012RsuRnRj entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esTariff2012RsuRnRjQuery query)
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
				throw new Exception("esTariff2012RsuRnRj can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esTariff2012RsuRnRjQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return Tariff2012RsuRnRjMetadata.Meta();
			}
		}	
		

		public esQueryItem No
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRjMetadata.ColumnNames.No, esSystemType.Int64);
			}
		} 
		
		public esQueryItem IrCode
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRjMetadata.ColumnNames.IrCode, esSystemType.String);
			}
		} 
		
		public esQueryItem Code
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRjMetadata.ColumnNames.Code, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRjMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem Alos
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRjMetadata.ColumnNames.Alos, esSystemType.String);
			}
		} 
		
		public esQueryItem FinalCostWeight
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRjMetadata.ColumnNames.FinalCostWeight, esSystemType.String);
			}
		} 
		
		public esQueryItem BaseRate
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRjMetadata.ColumnNames.BaseRate, esSystemType.String);
			}
		} 
		
		public esQueryItem PoliBiasa
		{
			get
			{
				return new esQueryItem(this, Tariff2012RsuRnRjMetadata.ColumnNames.PoliBiasa, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("Tariff2012RsuRnRjCollection")]
	public partial class Tariff2012RsuRnRjCollection : esTariff2012RsuRnRjCollection, IEnumerable<Tariff2012RsuRnRj>
	{
		public Tariff2012RsuRnRjCollection()
		{

		}
		
		public static implicit operator List<Tariff2012RsuRnRj>(Tariff2012RsuRnRjCollection coll)
		{
			List<Tariff2012RsuRnRj> list = new List<Tariff2012RsuRnRj>();
			
			foreach (Tariff2012RsuRnRj emp in coll)
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
				return  Tariff2012RsuRnRjMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new Tariff2012RsuRnRjQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Tariff2012RsuRnRj(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Tariff2012RsuRnRj();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public Tariff2012RsuRnRjQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new Tariff2012RsuRnRjQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(Tariff2012RsuRnRjQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Tariff2012RsuRnRj AddNew()
		{
			Tariff2012RsuRnRj entity = base.AddNewEntity() as Tariff2012RsuRnRj;
			
			return entity;
		}

		public Tariff2012RsuRnRj FindByPrimaryKey(System.Int64 no)
		{
			return base.FindByPrimaryKey(no) as Tariff2012RsuRnRj;
		}


		#region IEnumerable<Tariff2012RsuRnRj> Members

		IEnumerator<Tariff2012RsuRnRj> IEnumerable<Tariff2012RsuRnRj>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Tariff2012RsuRnRj;
			}
		}

		#endregion
		
		private Tariff2012RsuRnRjQuery query;
	}


	/// <summary>
	/// Encapsulates the 'tariff_2012_rsu_rn_rj' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Tariff2012RsuRnRj ({No})")]
	[Serializable]
	public partial class Tariff2012RsuRnRj : esTariff2012RsuRnRj
	{
		public Tariff2012RsuRnRj()
		{

		}
	
		public Tariff2012RsuRnRj(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return Tariff2012RsuRnRjMetadata.Meta();
			}
		}
		
		
		
		override protected esTariff2012RsuRnRjQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new Tariff2012RsuRnRjQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public Tariff2012RsuRnRjQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new Tariff2012RsuRnRjQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(Tariff2012RsuRnRjQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private Tariff2012RsuRnRjQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class Tariff2012RsuRnRjQuery : esTariff2012RsuRnRjQuery
	{
		public Tariff2012RsuRnRjQuery()
		{

		}		
		
		public Tariff2012RsuRnRjQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "Tariff2012RsuRnRjQuery";
        }
		
			
	}


	[Serializable]
	public partial class Tariff2012RsuRnRjMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected Tariff2012RsuRnRjMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(Tariff2012RsuRnRjMetadata.ColumnNames.No, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = Tariff2012RsuRnRjMetadata.PropertyNames.No;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRjMetadata.ColumnNames.IrCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRjMetadata.PropertyNames.IrCode;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRjMetadata.ColumnNames.Code, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRjMetadata.PropertyNames.Code;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRjMetadata.ColumnNames.Description, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRjMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 255;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRjMetadata.ColumnNames.Alos, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRjMetadata.PropertyNames.Alos;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRjMetadata.ColumnNames.FinalCostWeight, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRjMetadata.PropertyNames.FinalCostWeight;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRjMetadata.ColumnNames.BaseRate, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRjMetadata.PropertyNames.BaseRate;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(Tariff2012RsuRnRjMetadata.ColumnNames.PoliBiasa, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = Tariff2012RsuRnRjMetadata.PropertyNames.PoliBiasa;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
		}
		#endregion	
	
		static public Tariff2012RsuRnRjMetadata Meta()
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
			 public const string No = "no";
			 public const string IrCode = "ir_code";
			 public const string Code = "code";
			 public const string Description = "description";
			 public const string Alos = "alos";
			 public const string FinalCostWeight = "final_cost_weight";
			 public const string BaseRate = "base_rate";
			 public const string PoliBiasa = "poli_biasa";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string No = "No";
			 public const string IrCode = "IrCode";
			 public const string Code = "Code";
			 public const string Description = "Description";
			 public const string Alos = "Alos";
			 public const string FinalCostWeight = "FinalCostWeight";
			 public const string BaseRate = "BaseRate";
			 public const string PoliBiasa = "PoliBiasa";
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
			lock (typeof(Tariff2012RsuRnRjMetadata))
			{
				if(Tariff2012RsuRnRjMetadata.mapDelegates == null)
				{
					Tariff2012RsuRnRjMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (Tariff2012RsuRnRjMetadata.meta == null)
				{
					Tariff2012RsuRnRjMetadata.meta = new Tariff2012RsuRnRjMetadata();
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
				

				meta.AddTypeMap("No", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("IrCode", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Code", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Alos", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("FinalCostWeight", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("BaseRate", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PoliBiasa", new esTypeMap("char", "System.String"));			
				
				
				
				meta.Source = "tariff_2012_rsu_rn_rj";
				meta.Destination = "tariff_2012_rsu_rn_rj";
				
				meta.spInsert = "proc_tariff_2012_rsu_rn_rjInsert";				
				meta.spUpdate = "proc_tariff_2012_rsu_rn_rjUpdate";		
				meta.spDelete = "proc_tariff_2012_rsu_rn_rjDelete";
				meta.spLoadAll = "proc_tariff_2012_rsu_rn_rjLoadAll";
				meta.spLoadByPrimaryKey = "proc_tariff_2012_rsu_rn_rjLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private Tariff2012RsuRnRjMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
