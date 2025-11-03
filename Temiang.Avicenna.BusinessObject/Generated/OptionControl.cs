/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/5/2014 10:45:04 AM
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
	abstract public class esOptionControlCollection : esEntityCollectionWAuditLog
	{
		public esOptionControlCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "OptionControlCollection";
		}

		#region Query Logic
		protected void InitQuery(esOptionControlQuery query)
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
			this.InitQuery(query as esOptionControlQuery);
		}
		#endregion
		
		virtual public OptionControl DetachEntity(OptionControl entity)
		{
			return base.DetachEntity(entity) as OptionControl;
		}
		
		virtual public OptionControl AttachEntity(OptionControl entity)
		{
			return base.AttachEntity(entity) as OptionControl;
		}
		
		virtual public void Combine(OptionControlCollection collection)
		{
			base.Combine(collection);
		}
		
		new public OptionControl this[int index]
		{
			get
			{
				return base[index] as OptionControl;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(OptionControl);
		}
	}



	[Serializable]
	abstract public class esOptionControl : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esOptionControlQuery GetDynamicQuery()
		{
			return null;
		}

		public esOptionControl()
		{

		}

		public esOptionControl(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String controlName)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(controlName);
			else
				return LoadByPrimaryKeyStoredProcedure(controlName);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String controlName)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(controlName);
			else
				return LoadByPrimaryKeyStoredProcedure(controlName);
		}

		private bool LoadByPrimaryKeyDynamic(System.String controlName)
		{
			esOptionControlQuery query = this.GetDynamicQuery();
			query.Where(query.ControlName == controlName);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String controlName)
		{
			esParameters parms = new esParameters();
			parms.Add("ControlName",controlName);
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
						case "ControlName": this.str.ControlName = (string)value; break;							
						case "Parameters": this.str.Parameters = (string)value; break;							
						case "DefaultCaption": this.str.DefaultCaption = (string)value; break;
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
		/// Maps to OptionControl.ControlName
		/// </summary>
		virtual public System.String ControlName
		{
			get
			{
				return base.GetSystemString(OptionControlMetadata.ColumnNames.ControlName);
			}
			
			set
			{
				base.SetSystemString(OptionControlMetadata.ColumnNames.ControlName, value);
			}
		}
		
		/// <summary>
		/// Maps to OptionControl.Parameters
		/// </summary>
		virtual public System.String Parameters
		{
			get
			{
				return base.GetSystemString(OptionControlMetadata.ColumnNames.Parameters);
			}
			
			set
			{
				base.SetSystemString(OptionControlMetadata.ColumnNames.Parameters, value);
			}
		}
		
		/// <summary>
		/// Maps to OptionControl.DefaultCaption
		/// </summary>
		virtual public System.String DefaultCaption
		{
			get
			{
				return base.GetSystemString(OptionControlMetadata.ColumnNames.DefaultCaption);
			}
			
			set
			{
				base.SetSystemString(OptionControlMetadata.ColumnNames.DefaultCaption, value);
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
			public esStrings(esOptionControl entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ControlName
			{
				get
				{
					System.String data = entity.ControlName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ControlName = null;
					else entity.ControlName = Convert.ToString(value);
				}
			}
				
			public System.String Parameters
			{
				get
				{
					System.String data = entity.Parameters;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Parameters = null;
					else entity.Parameters = Convert.ToString(value);
				}
			}
				
			public System.String DefaultCaption
			{
				get
				{
					System.String data = entity.DefaultCaption;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DefaultCaption = null;
					else entity.DefaultCaption = Convert.ToString(value);
				}
			}
			

			private esOptionControl entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esOptionControlQuery query)
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
				throw new Exception("esOptionControl can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esOptionControlQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OptionControlMetadata.Meta();
			}
		}	
		

		public esQueryItem ControlName
		{
			get
			{
				return new esQueryItem(this, OptionControlMetadata.ColumnNames.ControlName, esSystemType.String);
			}
		} 
		
		public esQueryItem Parameters
		{
			get
			{
				return new esQueryItem(this, OptionControlMetadata.ColumnNames.Parameters, esSystemType.String);
			}
		} 
		
		public esQueryItem DefaultCaption
		{
			get
			{
				return new esQueryItem(this, OptionControlMetadata.ColumnNames.DefaultCaption, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("OptionControlCollection")]
	public partial class OptionControlCollection : esOptionControlCollection, IEnumerable<OptionControl>
	{
		public OptionControlCollection()
		{

		}
		
		public static implicit operator List<OptionControl>(OptionControlCollection coll)
		{
			List<OptionControl> list = new List<OptionControl>();
			
			foreach (OptionControl emp in coll)
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
				return  OptionControlMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OptionControlQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new OptionControl(row);
		}

		override protected esEntity CreateEntity()
		{
			return new OptionControl();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public OptionControlQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OptionControlQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(OptionControlQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public OptionControl AddNew()
		{
			OptionControl entity = base.AddNewEntity() as OptionControl;
			
			return entity;
		}

		public OptionControl FindByPrimaryKey(System.String controlName)
		{
			return base.FindByPrimaryKey(controlName) as OptionControl;
		}


		#region IEnumerable<OptionControl> Members

		IEnumerator<OptionControl> IEnumerable<OptionControl>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as OptionControl;
			}
		}

		#endregion
		
		private OptionControlQuery query;
	}


	/// <summary>
	/// Encapsulates the 'OptionControl' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("OptionControl ({ControlName})")]
	[Serializable]
	public partial class OptionControl : esOptionControl
	{
		public OptionControl()
		{

		}
	
		public OptionControl(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OptionControlMetadata.Meta();
			}
		}
		
		
		
		override protected esOptionControlQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OptionControlQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public OptionControlQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OptionControlQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(OptionControlQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private OptionControlQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class OptionControlQuery : esOptionControlQuery
	{
		public OptionControlQuery()
		{

		}		
		
		public OptionControlQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "OptionControlQuery";
        }
		
			
	}


	[Serializable]
	public partial class OptionControlMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OptionControlMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OptionControlMetadata.ColumnNames.ControlName, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = OptionControlMetadata.PropertyNames.ControlName;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(OptionControlMetadata.ColumnNames.Parameters, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = OptionControlMetadata.PropertyNames.Parameters;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(OptionControlMetadata.ColumnNames.DefaultCaption, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = OptionControlMetadata.PropertyNames.DefaultCaption;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public OptionControlMetadata Meta()
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
			 public const string ControlName = "ControlName";
			 public const string Parameters = "Parameters";
			 public const string DefaultCaption = "DefaultCaption";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ControlName = "ControlName";
			 public const string Parameters = "Parameters";
			 public const string DefaultCaption = "DefaultCaption";
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
			lock (typeof(OptionControlMetadata))
			{
				if(OptionControlMetadata.mapDelegates == null)
				{
					OptionControlMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OptionControlMetadata.meta == null)
				{
					OptionControlMetadata.meta = new OptionControlMetadata();
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
				

				meta.AddTypeMap("ControlName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Parameters", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DefaultCaption", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				meta.Source = "OptionControl";
				meta.Destination = "OptionControl";
				
				meta.spInsert = "proc_OptionControlInsert";				
				meta.spUpdate = "proc_OptionControlUpdate";		
				meta.spDelete = "proc_OptionControlDelete";
				meta.spLoadAll = "proc_OptionControlLoadAll";
				meta.spLoadByPrimaryKey = "proc_OptionControlLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OptionControlMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
