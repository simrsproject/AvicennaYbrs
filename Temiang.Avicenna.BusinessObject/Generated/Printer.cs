/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 9:19:24 PM
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
	abstract public class esPrinterCollection : esEntityCollectionWAuditLog
	{
		public esPrinterCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PrinterCollection";
		}

		#region Query Logic
		protected void InitQuery(esPrinterQuery query)
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
			this.InitQuery(query as esPrinterQuery);
		}
		#endregion
		
		virtual public Printer DetachEntity(Printer entity)
		{
			return base.DetachEntity(entity) as Printer;
		}
		
		virtual public Printer AttachEntity(Printer entity)
		{
			return base.AttachEntity(entity) as Printer;
		}
		
		virtual public void Combine(PrinterCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Printer this[int index]
		{
			get
			{
				return base[index] as Printer;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Printer);
		}
	}



	[Serializable]
	abstract public class esPrinter : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPrinterQuery GetDynamicQuery()
		{
			return null;
		}

		public esPrinter()
		{

		}

		public esPrinter(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String printerID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(printerID);
			else
				return LoadByPrimaryKeyStoredProcedure(printerID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String printerID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(printerID);
			else
				return LoadByPrimaryKeyStoredProcedure(printerID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String printerID)
		{
			esPrinterQuery query = this.GetDynamicQuery();
			query.Where(query.PrinterID == printerID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String printerID)
		{
			esParameters parms = new esParameters();
			parms.Add("PrinterID",printerID);
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
						case "PrinterID": this.str.PrinterID = (string)value; break;							
						case "PrinterName": this.str.PrinterName = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "PrinterManagerHost": this.str.PrinterManagerHost = (string)value; break;							
						case "PrinterLocationHost": this.str.PrinterLocationHost = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to Printer.PrinterID
		/// </summary>
		virtual public System.String PrinterID
		{
			get
			{
				return base.GetSystemString(PrinterMetadata.ColumnNames.PrinterID);
			}
			
			set
			{
				base.SetSystemString(PrinterMetadata.ColumnNames.PrinterID, value);
			}
		}
		
		/// <summary>
		/// Maps to Printer.PrinterName
		/// </summary>
		virtual public System.String PrinterName
		{
			get
			{
				return base.GetSystemString(PrinterMetadata.ColumnNames.PrinterName);
			}
			
			set
			{
				base.SetSystemString(PrinterMetadata.ColumnNames.PrinterName, value);
			}
		}
		
		/// <summary>
		/// Maps to Printer.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(PrinterMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(PrinterMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to Printer.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PrinterMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PrinterMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Printer.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PrinterMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(PrinterMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to Printer.PrinterManagerHost
		/// </summary>
		virtual public System.String PrinterManagerHost
		{
			get
			{
				return base.GetSystemString(PrinterMetadata.ColumnNames.PrinterManagerHost);
			}
			
			set
			{
				base.SetSystemString(PrinterMetadata.ColumnNames.PrinterManagerHost, value);
			}
		}
		
		/// <summary>
		/// Maps to Printer.PrinterLocationHost
		/// </summary>
		virtual public System.String PrinterLocationHost
		{
			get
			{
				return base.GetSystemString(PrinterMetadata.ColumnNames.PrinterLocationHost);
			}
			
			set
			{
				base.SetSystemString(PrinterMetadata.ColumnNames.PrinterLocationHost, value);
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
			public esStrings(esPrinter entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PrinterID
			{
				get
				{
					System.String data = entity.PrinterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrinterID = null;
					else entity.PrinterID = Convert.ToString(value);
				}
			}
				
			public System.String PrinterName
			{
				get
				{
					System.String data = entity.PrinterName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrinterName = null;
					else entity.PrinterName = Convert.ToString(value);
				}
			}
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
				
			public System.String PrinterManagerHost
			{
				get
				{
					System.String data = entity.PrinterManagerHost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrinterManagerHost = null;
					else entity.PrinterManagerHost = Convert.ToString(value);
				}
			}
				
			public System.String PrinterLocationHost
			{
				get
				{
					System.String data = entity.PrinterLocationHost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PrinterLocationHost = null;
					else entity.PrinterLocationHost = Convert.ToString(value);
				}
			}
			

			private esPrinter entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPrinterQuery query)
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
				throw new Exception("esPrinter can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Printer : esPrinter
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
	abstract public class esPrinterQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PrinterMetadata.Meta();
			}
		}	
		

		public esQueryItem PrinterID
		{
			get
			{
				return new esQueryItem(this, PrinterMetadata.ColumnNames.PrinterID, esSystemType.String);
			}
		} 
		
		public esQueryItem PrinterName
		{
			get
			{
				return new esQueryItem(this, PrinterMetadata.ColumnNames.PrinterName, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, PrinterMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PrinterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PrinterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem PrinterManagerHost
		{
			get
			{
				return new esQueryItem(this, PrinterMetadata.ColumnNames.PrinterManagerHost, esSystemType.String);
			}
		} 
		
		public esQueryItem PrinterLocationHost
		{
			get
			{
				return new esQueryItem(this, PrinterMetadata.ColumnNames.PrinterLocationHost, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PrinterCollection")]
	public partial class PrinterCollection : esPrinterCollection, IEnumerable<Printer>
	{
		public PrinterCollection()
		{

		}
		
		public static implicit operator List<Printer>(PrinterCollection coll)
		{
			List<Printer> list = new List<Printer>();
			
			foreach (Printer emp in coll)
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
				return  PrinterMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrinterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Printer(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Printer();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PrinterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrinterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PrinterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Printer AddNew()
		{
			Printer entity = base.AddNewEntity() as Printer;
			
			return entity;
		}

		public Printer FindByPrimaryKey(System.String printerID)
		{
			return base.FindByPrimaryKey(printerID) as Printer;
		}


		#region IEnumerable<Printer> Members

		IEnumerator<Printer> IEnumerable<Printer>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Printer;
			}
		}

		#endregion
		
		private PrinterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Printer' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Printer ({PrinterID})")]
	[Serializable]
	public partial class Printer : esPrinter
	{
		public Printer()
		{

		}
	
		public Printer(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PrinterMetadata.Meta();
			}
		}
		
		
		
		override protected esPrinterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrinterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PrinterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrinterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PrinterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PrinterQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PrinterQuery : esPrinterQuery
	{
		public PrinterQuery()
		{

		}		
		
		public PrinterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PrinterQuery";
        }
		
			
	}


	[Serializable]
	public partial class PrinterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PrinterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PrinterMetadata.ColumnNames.PrinterID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = PrinterMetadata.PropertyNames.PrinterID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrinterMetadata.ColumnNames.PrinterName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PrinterMetadata.PropertyNames.PrinterName;
			c.CharacterMaxLength = 500;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrinterMetadata.ColumnNames.Notes, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PrinterMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrinterMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PrinterMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrinterMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PrinterMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrinterMetadata.ColumnNames.PrinterManagerHost, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PrinterMetadata.PropertyNames.PrinterManagerHost;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrinterMetadata.ColumnNames.PrinterLocationHost, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PrinterMetadata.PropertyNames.PrinterLocationHost;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PrinterMetadata Meta()
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
			 public const string PrinterID = "PrinterID";
			 public const string PrinterName = "PrinterName";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PrinterManagerHost = "PrinterManagerHost";
			 public const string PrinterLocationHost = "PrinterLocationHost";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PrinterID = "PrinterID";
			 public const string PrinterName = "PrinterName";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PrinterManagerHost = "PrinterManagerHost";
			 public const string PrinterLocationHost = "PrinterLocationHost";
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
			lock (typeof(PrinterMetadata))
			{
				if(PrinterMetadata.mapDelegates == null)
				{
					PrinterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PrinterMetadata.meta == null)
				{
					PrinterMetadata.meta = new PrinterMetadata();
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
				

				meta.AddTypeMap("PrinterID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrinterName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrinterManagerHost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PrinterLocationHost", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Printer";
				meta.Destination = "Printer";
				
				meta.spInsert = "proc_PrinterInsert";				
				meta.spUpdate = "proc_PrinterUpdate";		
				meta.spDelete = "proc_PrinterDelete";
				meta.spLoadAll = "proc_PrinterLoadAll";
				meta.spLoadByPrimaryKey = "proc_PrinterLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PrinterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
