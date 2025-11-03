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
	abstract public class esPrintJobParameterCollection : esEntityCollectionWAuditLog
	{
		public esPrintJobParameterCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "PrintJobParameterCollection";
		}

		#region Query Logic
		protected void InitQuery(esPrintJobParameterQuery query)
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
			this.InitQuery(query as esPrintJobParameterQuery);
		}
		#endregion
		
		virtual public PrintJobParameter DetachEntity(PrintJobParameter entity)
		{
			return base.DetachEntity(entity) as PrintJobParameter;
		}
		
		virtual public PrintJobParameter AttachEntity(PrintJobParameter entity)
		{
			return base.AttachEntity(entity) as PrintJobParameter;
		}
		
		virtual public void Combine(PrintJobParameterCollection collection)
		{
			base.Combine(collection);
		}
		
		new public PrintJobParameter this[int index]
		{
			get
			{
				return base[index] as PrintJobParameter;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PrintJobParameter);
		}
	}



	[Serializable]
	abstract public class esPrintJobParameter : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPrintJobParameterQuery GetDynamicQuery()
		{
			return null;
		}

		public esPrintJobParameter()
		{

		}

		public esPrintJobParameter(DataRow row)
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
			esPrintJobParameterQuery query = this.GetDynamicQuery();
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
		/// Maps to PrintJobParameter.PrintNo
		/// </summary>
		virtual public System.Int64? PrintNo
		{
			get
			{
				return base.GetSystemInt64(PrintJobParameterMetadata.ColumnNames.PrintNo);
			}
			
			set
			{
				base.SetSystemInt64(PrintJobParameterMetadata.ColumnNames.PrintNo, value);
			}
		}
		
		/// <summary>
		/// Maps to PrintJobParameter.Name
		/// </summary>
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(PrintJobParameterMetadata.ColumnNames.Name);
			}
			
			set
			{
				base.SetSystemString(PrintJobParameterMetadata.ColumnNames.Name, value);
			}
		}
		
		/// <summary>
		/// Maps to PrintJobParameter.ValueString
		/// </summary>
		virtual public System.String ValueString
		{
			get
			{
				return base.GetSystemString(PrintJobParameterMetadata.ColumnNames.ValueString);
			}
			
			set
			{
				base.SetSystemString(PrintJobParameterMetadata.ColumnNames.ValueString, value);
			}
		}
		
		/// <summary>
		/// Maps to PrintJobParameter.ValueNumeric
		/// </summary>
		virtual public System.Decimal? ValueNumeric
		{
			get
			{
				return base.GetSystemDecimal(PrintJobParameterMetadata.ColumnNames.ValueNumeric);
			}
			
			set
			{
				base.SetSystemDecimal(PrintJobParameterMetadata.ColumnNames.ValueNumeric, value);
			}
		}
		
		/// <summary>
		/// Maps to PrintJobParameter.ValueDateTime
		/// </summary>
		virtual public System.DateTime? ValueDateTime
		{
			get
			{
				return base.GetSystemDateTime(PrintJobParameterMetadata.ColumnNames.ValueDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(PrintJobParameterMetadata.ColumnNames.ValueDateTime, value);
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
			public esStrings(esPrintJobParameter entity)
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
			

			private esPrintJobParameter entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPrintJobParameterQuery query)
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
				throw new Exception("esPrintJobParameter can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class PrintJobParameter : esPrintJobParameter
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
	abstract public class esPrintJobParameterQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PrintJobParameterMetadata.Meta();
			}
		}	
		

		public esQueryItem PrintNo
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterMetadata.ColumnNames.PrintNo, esSystemType.Int64);
			}
		} 
		
		public esQueryItem Name
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterMetadata.ColumnNames.Name, esSystemType.String);
			}
		} 
		
		public esQueryItem ValueString
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterMetadata.ColumnNames.ValueString, esSystemType.String);
			}
		} 
		
		public esQueryItem ValueNumeric
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterMetadata.ColumnNames.ValueNumeric, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem ValueDateTime
		{
			get
			{
				return new esQueryItem(this, PrintJobParameterMetadata.ColumnNames.ValueDateTime, esSystemType.DateTime);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PrintJobParameterCollection")]
	public partial class PrintJobParameterCollection : esPrintJobParameterCollection, IEnumerable<PrintJobParameter>
	{
		public PrintJobParameterCollection()
		{

		}
		
		public static implicit operator List<PrintJobParameter>(PrintJobParameterCollection coll)
		{
			List<PrintJobParameter> list = new List<PrintJobParameter>();
			
			foreach (PrintJobParameter emp in coll)
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
				return  PrintJobParameterMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrintJobParameterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PrintJobParameter(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PrintJobParameter();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public PrintJobParameterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrintJobParameterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(PrintJobParameterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public PrintJobParameter AddNew()
		{
			PrintJobParameter entity = base.AddNewEntity() as PrintJobParameter;
			
			return entity;
		}

		public PrintJobParameter FindByPrimaryKey(System.Int64 printNo, System.String name)
		{
			return base.FindByPrimaryKey(printNo, name) as PrintJobParameter;
		}


		#region IEnumerable<PrintJobParameter> Members

		IEnumerator<PrintJobParameter> IEnumerable<PrintJobParameter>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as PrintJobParameter;
			}
		}

		#endregion
		
		private PrintJobParameterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PrintJobParameter' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("PrintJobParameter ({PrintNo},{Name})")]
	[Serializable]
	public partial class PrintJobParameter : esPrintJobParameter
	{
		public PrintJobParameter()
		{

		}
	
		public PrintJobParameter(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PrintJobParameterMetadata.Meta();
			}
		}
		
		
		
		override protected esPrintJobParameterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PrintJobParameterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public PrintJobParameterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PrintJobParameterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(PrintJobParameterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private PrintJobParameterQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class PrintJobParameterQuery : esPrintJobParameterQuery
	{
		public PrintJobParameterQuery()
		{

		}		
		
		public PrintJobParameterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "PrintJobParameterQuery";
        }
		
			
	}


	[Serializable]
	public partial class PrintJobParameterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PrintJobParameterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PrintJobParameterMetadata.ColumnNames.PrintNo, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = PrintJobParameterMetadata.PropertyNames.PrintNo;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrintJobParameterMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PrintJobParameterMetadata.PropertyNames.Name;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrintJobParameterMetadata.ColumnNames.ValueString, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PrintJobParameterMetadata.PropertyNames.ValueString;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrintJobParameterMetadata.ColumnNames.ValueNumeric, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PrintJobParameterMetadata.PropertyNames.ValueNumeric;
			c.NumericPrecision = 18;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(PrintJobParameterMetadata.ColumnNames.ValueDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PrintJobParameterMetadata.PropertyNames.ValueDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public PrintJobParameterMetadata Meta()
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
			lock (typeof(PrintJobParameterMetadata))
			{
				if(PrintJobParameterMetadata.mapDelegates == null)
				{
					PrintJobParameterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PrintJobParameterMetadata.meta == null)
				{
					PrintJobParameterMetadata.meta = new PrintJobParameterMetadata();
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
				
				
				
				meta.Source = "PrintJobParameter";
				meta.Destination = "PrintJobParameter";
				
				meta.spInsert = "proc_PrintJobParameterInsert";				
				meta.spUpdate = "proc_PrintJobParameterUpdate";		
				meta.spDelete = "proc_PrintJobParameterDelete";
				meta.spLoadAll = "proc_PrintJobParameterLoadAll";
				meta.spLoadByPrimaryKey = "proc_PrintJobParameterLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PrintJobParameterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
