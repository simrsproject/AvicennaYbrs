/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:23 PM
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
	abstract public class esPrintJobParameterLogCollection : esEntityCollectionWAuditLog
	{
		public esPrintJobParameterLogCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PrintJobParameterLogCollection";
		}

		#region Query Logic
		protected void InitQuery(esPrintJobParameterLogQuery query)
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
			this.InitQuery(query as esPrintJobParameterLogQuery);
		}
		#endregion
		
		virtual public PrintJobParameterLog DetachEntity(PrintJobParameterLog entity)
		{
			return base.DetachEntity(entity) as PrintJobParameterLog;
		}
		
		virtual public PrintJobParameterLog AttachEntity(PrintJobParameterLog entity)
		{
			return base.AttachEntity(entity) as PrintJobParameterLog;
		}
		
		virtual public void Combine(PrintJobParameterLogCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PrintJobParameterLog this[int index]
		{
			get
			{
				return base[index] as PrintJobParameterLog;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PrintJobParameterLog);
		}
	}



	[Serializable]
	abstract public class esPrintJobParameterLog : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPrintJobParameterLogQuery GetDynamicQuery()
		{
			return null;
		}

		public esPrintJobParameterLog()
		{

		}

		public esPrintJobParameterLog(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 printNo, System.String name)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(printNo, name);
			else
				return LoadByPrimaryKeyStoredProcedure(printNo, name);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 printNo, System.String name)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(printNo, name);
			else
				return LoadByPrimaryKeyStoredProcedure(printNo, name);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 printNo, System.String name)
		{
			esPrintJobParameterLogQuery query = this.GetDynamicQuery();
			query.Where(query.PrintNo == printNo, query.Name == name);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 printNo, System.String name)
		{
			esParameters parms = new esParameters();
			parms.Add("PrintNo",printNo);			parms.Add("Name",name);
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
						case "PrintNo": this.str.PrintNo = (string)value; break;							
						case "Name": this.str.Name = (string)value; break;							
						case "ValueString": this.str.ValueString = (string)value; break;							
						case "ValueNumeric": this.str.ValueNumeric = (string)value; break;							
						case "ValueDateTime": this.str.ValueDateTime = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PrintNo":
						
							if (value == null || value is System.Int64)
								this.PrintNo = (System.Int64?)value;
							break;
						
						case "ValueNumeric":
						
							if (value == null || value is System.Decimal)
								this.ValueNumeric = (System.Decimal?)value;
							break;
						
						case "ValueDateTime":
						
							if (value == null || value is System.DateTime)
								this.ValueDateTime = (System.DateTime?)value;
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
		/// Maps to PrintJobParameterLog.PrintNo
		/// </summary>
		virtual public System.Int64? PrintNo
		{
			get
			{
				return base.GetSystemInt64(PrintJobParameterLogMetadata.ColumnNames.PrintNo);
			}
			
			set
			{
				base.SetSystemInt64(PrintJobParameterLogMetadata.ColumnNames.PrintNo, value);
			}
		}
		
		/// <summary>
		/// Maps to PrintJobParameterLog.Name
		/// </summary>
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(PrintJobParameterLogMetadata.ColumnNames.Name);
			}
			
			set
			{
				base.SetSystemString(PrintJobParameterLogMetadata.ColumnNames.Name, value);
			}
		}
		
		/// <summary>
		/// Maps to PrintJobParameterLog.ValueString
		/// </summary>
		virtual public System.String ValueString
		{
			get
			{
				return base.GetSystemString(PrintJobParameterLogMetadata.ColumnNames.ValueString);
			}
			
			set
			{
				base.SetSystemString(PrintJobParameterLogMetadata.ColumnNames.ValueString, value);
			}
		}
		
		/// <summary>
		/// Maps to PrintJobParameterLog.ValueNumeric
		/// </summary>
		virtual public System.Decimal? ValueNumeric
		{
			get
			{
				return base.GetSystemDecimal(PrintJobParameterLogMetadata.ColumnNames.ValueNumeric);
			}
			
			set
			{
				base.SetSystemDecimal(PrintJobParameterLogMetadata.ColumnNames.ValueNumeric, value);
			}
		}
		
		/// <summary>
		/// Maps to PrintJobParameterLog.ValueDateTime
		/// </summary>
		virtual public System.DateTime? ValueDateTime
		{
			get
			{
				return base.GetSystemDateTime(PrintJobParameterLogMetadata.ColumnNames.ValueDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PrintJobParameterLogMetadata.ColumnNames.ValueDateTime, value);
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
			public esStrings(esPrintJobParameterLog entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PrintNo
			{
				get
				{
					System.Int64? data = entity.PrintNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrintNo = null;
					else entity.PrintNo = Convert.ToInt64(value);
				}
			}
				
			public System.String Name
			{
				get
				{
					System.String data = entity.Name;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Name = null;
					else entity.Name = Convert.ToString(value);
				}
			}
				
			public System.String ValueString
			{
				get
				{
					System.String data = entity.ValueString;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueString = null;
					else entity.ValueString = Convert.ToString(value);
				}
			}
				
			public System.String ValueNumeric
			{
				get
				{
					System.Decimal? data = entity.ValueNumeric;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueNumeric = null;
					else entity.ValueNumeric = Convert.ToDecimal(value);
				}
			}
				
			public System.String ValueDateTime
			{
				get
				{
					System.DateTime? data = entity.ValueDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValueDateTime = null;
					else entity.ValueDateTime = Convert.ToDateTime(value);
				}
			}
			

			private esPrintJobParameterLog entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPrintJobParameterLogQuery query)
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
				throw new Exception("esPrintJobParameterLog can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PrintJobParameterLog : esPrintJobParameterLog
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
	abstract public class esPrintJobParameterLogQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PrintJobParameterLogMetadata.Meta();
			}
		}	
		

		public esQueryItem PrintNo
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterLogMetadata.ColumnNames.PrintNo, esSystemType.Int64);
			}
		} 
		
		public esQueryItem Name
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterLogMetadata.ColumnNames.Name, esSystemType.String);
			}
		} 
		
		public esQueryItem ValueString
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterLogMetadata.ColumnNames.ValueString, esSystemType.String);
			}
		} 
		
		public esQueryItem ValueNumeric
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterLogMetadata.ColumnNames.ValueNumeric, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ValueDateTime
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterLogMetadata.ColumnNames.ValueDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PrintJobParameterLogCollection")]
	public partial class PrintJobParameterLogCollection : esPrintJobParameterLogCollection, IEnumerable<PrintJobParameterLog>
	{
		public PrintJobParameterLogCollection()
		{

		}
		
		public static implicit operator List<PrintJobParameterLog>(PrintJobParameterLogCollection coll)
		{
			List<PrintJobParameterLog> list = new List<PrintJobParameterLog>();
			
			foreach (PrintJobParameterLog emp in coll)
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
				return  PrintJobParameterLogMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrintJobParameterLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PrintJobParameterLog(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PrintJobParameterLog();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PrintJobParameterLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrintJobParameterLogQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PrintJobParameterLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PrintJobParameterLog AddNew()
		{
			PrintJobParameterLog entity = base.AddNewEntity() as PrintJobParameterLog;
			
			return entity;
		}

		public PrintJobParameterLog FindByPrimaryKey(System.Int64 printNo, System.String name)
		{
			return base.FindByPrimaryKey(printNo, name) as PrintJobParameterLog;
		}


		#region IEnumerable<PrintJobParameterLog> Members

		IEnumerator<PrintJobParameterLog> IEnumerable<PrintJobParameterLog>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PrintJobParameterLog;
			}
		}

		#endregion
		
		private PrintJobParameterLogQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PrintJobParameterLog' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PrintJobParameterLog ({PrintNo},{Name})")]
	[Serializable]
	public partial class PrintJobParameterLog : esPrintJobParameterLog
	{
		public PrintJobParameterLog()
		{

		}
	
		public PrintJobParameterLog(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PrintJobParameterLogMetadata.Meta();
			}
		}
		
		
		
		override protected esPrintJobParameterLogQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrintJobParameterLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PrintJobParameterLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrintJobParameterLogQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PrintJobParameterLogQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PrintJobParameterLogQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PrintJobParameterLogQuery : esPrintJobParameterLogQuery
	{
		public PrintJobParameterLogQuery()
		{

		}		
		
		public PrintJobParameterLogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PrintJobParameterLogQuery";
        }
		
			
	}


	[Serializable]
	public partial class PrintJobParameterLogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PrintJobParameterLogMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PrintJobParameterLogMetadata.ColumnNames.PrintNo, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PrintJobParameterLogMetadata.PropertyNames.PrintNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrintJobParameterLogMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PrintJobParameterLogMetadata.PropertyNames.Name;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrintJobParameterLogMetadata.ColumnNames.ValueString, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PrintJobParameterLogMetadata.PropertyNames.ValueString;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrintJobParameterLogMetadata.ColumnNames.ValueNumeric, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PrintJobParameterLogMetadata.PropertyNames.ValueNumeric;
			c.NumericPrecision = 18;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrintJobParameterLogMetadata.ColumnNames.ValueDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PrintJobParameterLogMetadata.PropertyNames.ValueDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PrintJobParameterLogMetadata Meta()
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
			 public const string PrintNo = "PrintNo";
			 public const string Name = "Name";
			 public const string ValueString = "ValueString";
			 public const string ValueNumeric = "ValueNumeric";
			 public const string ValueDateTime = "ValueDateTime";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PrintNo = "PrintNo";
			 public const string Name = "Name";
			 public const string ValueString = "ValueString";
			 public const string ValueNumeric = "ValueNumeric";
			 public const string ValueDateTime = "ValueDateTime";
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
			lock (typeof(PrintJobParameterLogMetadata))
			{
				if(PrintJobParameterLogMetadata.mapDelegates == null)
				{
					PrintJobParameterLogMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PrintJobParameterLogMetadata.meta == null)
				{
					PrintJobParameterLogMetadata.meta = new PrintJobParameterLogMetadata();
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
				

				meta.AddTypeMap("PrintNo", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("Name", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValueString", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValueNumeric", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ValueDateTime", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				meta.Source = "PrintJobParameterLog";
				meta.Destination = "PrintJobParameterLog";
				
				meta.spInsert = "proc_PrintJobParameterLogInsert";				
				meta.spUpdate = "proc_PrintJobParameterLogUpdate";		
				meta.spDelete = "proc_PrintJobParameterLogDelete";
				meta.spLoadAll = "proc_PrintJobParameterLogLoadAll";
				meta.spLoadByPrimaryKey = "proc_PrintJobParameterLogLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PrintJobParameterLogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
