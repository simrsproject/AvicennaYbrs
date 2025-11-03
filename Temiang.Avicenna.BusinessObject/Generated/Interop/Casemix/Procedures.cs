/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/21/2014 11:39:24 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.Casemix
{

	[Serializable]
	abstract public class esProceduresCollection : esEntityCollectionWAuditLog
	{
		public esProceduresCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ProceduresCollection";
		}

		#region Query Logic
		protected void InitQuery(esProceduresQuery query)
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
			this.InitQuery(query as esProceduresQuery);
		}
		#endregion
		
		virtual public Procedures DetachEntity(Procedures entity)
		{
			return base.DetachEntity(entity) as Procedures;
		}
		
		virtual public Procedures AttachEntity(Procedures entity)
		{
			return base.AttachEntity(entity) as Procedures;
		}
		
		virtual public void Combine(ProceduresCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Procedures this[int index]
		{
			get
			{
				return base[index] as Procedures;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Procedures);
		}
	}



	[Serializable]
	abstract public class esProcedures : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esProceduresQuery GetDynamicQuery()
		{
			return null;
		}

		public esProcedures()
		{

		}

		public esProcedures(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String code)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(code);
			else
				return LoadByPrimaryKeyStoredProcedure(code);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String code)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(code);
			else
				return LoadByPrimaryKeyStoredProcedure(code);
		}

		private bool LoadByPrimaryKeyDynamic(System.String code)
		{
			esProceduresQuery query = this.GetDynamicQuery();
			query.Where(query.Code == code);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String code)
		{
			esParameters parms = new esParameters();
			parms.Add("Code",code);
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
						case "Code": this.str.Code = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "Class": this.str.Class = (string)value; break;							
						case "Inpatient": this.str.Inpatient = (string)value; break;							
						case "Outpatient": this.str.Outpatient = (string)value; break;
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
		/// Maps to procedures.Code
		/// </summary>
		virtual public System.String Code
		{
			get
			{
				return base.GetSystemString(ProceduresMetadata.ColumnNames.Code);
			}
			
			set
			{
				base.SetSystemString(ProceduresMetadata.ColumnNames.Code, value);
			}
		}
		
		/// <summary>
		/// Maps to procedures.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(ProceduresMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(ProceduresMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to procedures.Class
		/// </summary>
		virtual public System.String Class
		{
			get
			{
				return base.GetSystemString(ProceduresMetadata.ColumnNames.Class);
			}
			
			set
			{
				base.SetSystemString(ProceduresMetadata.ColumnNames.Class, value);
			}
		}
		
		/// <summary>
		/// Maps to procedures.Inpatient
		/// </summary>
		virtual public System.String Inpatient
		{
			get
			{
				return base.GetSystemString(ProceduresMetadata.ColumnNames.Inpatient);
			}
			
			set
			{
				base.SetSystemString(ProceduresMetadata.ColumnNames.Inpatient, value);
			}
		}
		
		/// <summary>
		/// Maps to procedures.Outpatient
		/// </summary>
		virtual public System.String Outpatient
		{
			get
			{
				return base.GetSystemString(ProceduresMetadata.ColumnNames.Outpatient);
			}
			
			set
			{
				base.SetSystemString(ProceduresMetadata.ColumnNames.Outpatient, value);
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
			public esStrings(esProcedures entity)
			{
				this.entity = entity;
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
				
			public System.String Class
			{
				get
				{
					System.String data = entity.Class;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Class = null;
					else entity.Class = Convert.ToString(value);
				}
			}
				
			public System.String Inpatient
			{
				get
				{
					System.String data = entity.Inpatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Inpatient = null;
					else entity.Inpatient = Convert.ToString(value);
				}
			}
				
			public System.String Outpatient
			{
				get
				{
					System.String data = entity.Outpatient;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Outpatient = null;
					else entity.Outpatient = Convert.ToString(value);
				}
			}
			

			private esProcedures entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esProceduresQuery query)
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
				throw new Exception("esProcedures can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esProceduresQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProceduresMetadata.Meta();
			}
		}	
		

		public esQueryItem Code
		{
			get
			{
				return new esQueryItem(this, ProceduresMetadata.ColumnNames.Code, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, ProceduresMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem Class
		{
			get
			{
				return new esQueryItem(this, ProceduresMetadata.ColumnNames.Class, esSystemType.String);
			}
		} 
		
		public esQueryItem Inpatient
		{
			get
			{
				return new esQueryItem(this, ProceduresMetadata.ColumnNames.Inpatient, esSystemType.String);
			}
		} 
		
		public esQueryItem Outpatient
		{
			get
			{
				return new esQueryItem(this, ProceduresMetadata.ColumnNames.Outpatient, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ProceduresCollection")]
	public partial class ProceduresCollection : esProceduresCollection, IEnumerable<Procedures>
	{
		public ProceduresCollection()
		{

		}
		
		public static implicit operator List<Procedures>(ProceduresCollection coll)
		{
			List<Procedures> list = new List<Procedures>();
			
			foreach (Procedures emp in coll)
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
				return  ProceduresMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProceduresQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Procedures(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Procedures();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ProceduresQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProceduresQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ProceduresQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Procedures AddNew()
		{
			Procedures entity = base.AddNewEntity() as Procedures;
			
			return entity;
		}

		public Procedures FindByPrimaryKey(System.String code)
		{
			return base.FindByPrimaryKey(code) as Procedures;
		}


		#region IEnumerable<Procedures> Members

		IEnumerator<Procedures> IEnumerable<Procedures>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Procedures;
			}
		}

		#endregion
		
		private ProceduresQuery query;
	}


	/// <summary>
	/// Encapsulates the 'procedures' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Procedures ({Code})")]
	[Serializable]
	public partial class Procedures : esProcedures
	{
		public Procedures()
		{

		}
	
		public Procedures(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProceduresMetadata.Meta();
			}
		}
		
		
		
		override protected esProceduresQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProceduresQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ProceduresQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProceduresQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ProceduresQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ProceduresQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ProceduresQuery : esProceduresQuery
	{
		public ProceduresQuery()
		{

		}		
		
		public ProceduresQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ProceduresQuery";
        }
		
			
	}


	[Serializable]
	public partial class ProceduresMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProceduresMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProceduresMetadata.ColumnNames.Code, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ProceduresMetadata.PropertyNames.Code;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ProceduresMetadata.ColumnNames.Description, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ProceduresMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 163;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProceduresMetadata.ColumnNames.Class, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ProceduresMetadata.PropertyNames.Class;
			c.CharacterMaxLength = 11;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProceduresMetadata.ColumnNames.Inpatient, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ProceduresMetadata.PropertyNames.Inpatient;
			c.CharacterMaxLength = 23;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ProceduresMetadata.ColumnNames.Outpatient, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ProceduresMetadata.PropertyNames.Outpatient;
			c.CharacterMaxLength = 103;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ProceduresMetadata Meta()
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
			 public const string Code = "Code";
			 public const string Description = "Description";
			 public const string Class = "Class";
			 public const string Inpatient = "Inpatient";
			 public const string Outpatient = "Outpatient";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Code = "Code";
			 public const string Description = "Description";
			 public const string Class = "Class";
			 public const string Inpatient = "Inpatient";
			 public const string Outpatient = "Outpatient";
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
			lock (typeof(ProceduresMetadata))
			{
				if(ProceduresMetadata.mapDelegates == null)
				{
					ProceduresMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProceduresMetadata.meta == null)
				{
					ProceduresMetadata.meta = new ProceduresMetadata();
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
				

				meta.AddTypeMap("Code", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Class", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Inpatient", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Outpatient", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "procedures";
				meta.Destination = "procedures";
				
				meta.spInsert = "proc_proceduresInsert";				
				meta.spUpdate = "proc_proceduresUpdate";		
				meta.spDelete = "proc_proceduresDelete";
				meta.spLoadAll = "proc_proceduresLoadAll";
				meta.spLoadByPrimaryKey = "proc_proceduresLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProceduresMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
