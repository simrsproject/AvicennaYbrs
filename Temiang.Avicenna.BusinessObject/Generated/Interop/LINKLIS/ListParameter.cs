/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/14/2020 4:12:01 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.LINKLIS
{

	[Serializable]
	abstract public class esListParameterCollection : esEntityCollectionWAuditLog
	{
		public esListParameterCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ListParameterCollection";
		}

		#region Query Logic
		protected void InitQuery(esListParameterQuery query)
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
			this.InitQuery(query as esListParameterQuery);
		}
		#endregion

		virtual public ListParameter DetachEntity(ListParameter entity)
		{
			return base.DetachEntity(entity) as ListParameter;
		}

		virtual public ListParameter AttachEntity(ListParameter entity)
		{
			return base.AttachEntity(entity) as ListParameter;
		}

		virtual public void Combine(ListParameterCollection collection)
		{
			base.Combine(collection);
		}

		new public ListParameter this[int index]
		{
			get
			{
				return base[index] as ListParameter;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ListParameter);
		}
	}



	[Serializable]
	abstract public class esListParameter : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esListParameterQuery GetDynamicQuery()
		{
			return null;
		}

		public esListParameter()
		{

		}

		public esListParameter(DataRow row)
			: base(row)
		{

		}

		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String kodeParameter, System.String kodePemeriksaan)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(kodeParameter, kodePemeriksaan);
			else
				return LoadByPrimaryKeyStoredProcedure(kodeParameter, kodePemeriksaan);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String kodeParameter, System.String kodePemeriksaan)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(kodeParameter, kodePemeriksaan);
			else
				return LoadByPrimaryKeyStoredProcedure(kodeParameter, kodePemeriksaan);
		}

		private bool LoadByPrimaryKeyDynamic(System.String kodeParameter, System.String kodePemeriksaan)
		{
			esListParameterQuery query = this.GetDynamicQuery();
			query.Where(query.KodeParameter == kodeParameter, query.KodePemeriksaan == kodePemeriksaan);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String kodeParameter, System.String kodePemeriksaan)
		{
			esParameters parms = new esParameters();
			parms.Add("KodeParameter", kodeParameter); parms.Add("KodePemeriksaan", kodePemeriksaan);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "KodeParameter": this.str.KodeParameter = (string)value; break;
						case "KodePemeriksaan": this.str.KodePemeriksaan = (string)value; break;
						case "NamaParameter": this.str.NamaParameter = (string)value; break;
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
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}


		/// <summary>
		/// Maps to ListParameter.KodeParameter
		/// </summary>
		virtual public System.String KodeParameter
		{
			get
			{
				return base.GetSystemString(ListParameterMetadata.ColumnNames.KodeParameter);
			}

			set
			{
				base.SetSystemString(ListParameterMetadata.ColumnNames.KodeParameter, value);
			}
		}

		/// <summary>
		/// Maps to ListParameter.KodePemeriksaan
		/// </summary>
		virtual public System.String KodePemeriksaan
		{
			get
			{
				return base.GetSystemString(ListParameterMetadata.ColumnNames.KodePemeriksaan);
			}

			set
			{
				base.SetSystemString(ListParameterMetadata.ColumnNames.KodePemeriksaan, value);
			}
		}

		/// <summary>
		/// Maps to ListParameter.NamaParameter
		/// </summary>
		virtual public System.String NamaParameter
		{
			get
			{
				return base.GetSystemString(ListParameterMetadata.ColumnNames.NamaParameter);
			}

			set
			{
				base.SetSystemString(ListParameterMetadata.ColumnNames.NamaParameter, value);
			}
		}

		#endregion

		#region String Properties


		[BrowsableAttribute(false)]
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
			public esStrings(esListParameter entity)
			{
				this.entity = entity;
			}


			public System.String KodeParameter
			{
				get
				{
					System.String data = entity.KodeParameter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeParameter = null;
					else entity.KodeParameter = Convert.ToString(value);
				}
			}

			public System.String KodePemeriksaan
			{
				get
				{
					System.String data = entity.KodePemeriksaan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodePemeriksaan = null;
					else entity.KodePemeriksaan = Convert.ToString(value);
				}
			}

			public System.String NamaParameter
			{
				get
				{
					System.String data = entity.NamaParameter;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaParameter = null;
					else entity.NamaParameter = Convert.ToString(value);
				}
			}


			private esListParameter entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esListParameterQuery query)
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
				throw new Exception("esListParameter can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esListParameterQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ListParameterMetadata.Meta();
			}
		}


		public esQueryItem KodeParameter
		{
			get
			{
				return new esQueryItem(this, ListParameterMetadata.ColumnNames.KodeParameter, esSystemType.String);
			}
		}

		public esQueryItem KodePemeriksaan
		{
			get
			{
				return new esQueryItem(this, ListParameterMetadata.ColumnNames.KodePemeriksaan, esSystemType.String);
			}
		}

		public esQueryItem NamaParameter
		{
			get
			{
				return new esQueryItem(this, ListParameterMetadata.ColumnNames.NamaParameter, esSystemType.String);
			}
		}

	}



	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ListParameterCollection")]
	public partial class ListParameterCollection : esListParameterCollection, IEnumerable<ListParameter>
	{
		public ListParameterCollection()
		{

		}

		public static implicit operator List<ListParameter>(ListParameterCollection coll)
		{
			List<ListParameter> list = new List<ListParameter>();

			foreach (ListParameter emp in coll)
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
				return ListParameterMetadata.Meta();
			}
		}



		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ListParameterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ListParameter(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ListParameter();
		}


		#endregion


		[BrowsableAttribute(false)]
		public ListParameterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ListParameterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ListParameterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		public ListParameter AddNew()
		{
			ListParameter entity = base.AddNewEntity() as ListParameter;

			return entity;
		}

		public ListParameter FindByPrimaryKey(System.String kodeParameter, System.String kodePemeriksaan)
		{
			return base.FindByPrimaryKey(kodeParameter, kodePemeriksaan) as ListParameter;
		}


		#region IEnumerable<ListParameter> Members

		IEnumerator<ListParameter> IEnumerable<ListParameter>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ListParameter;
			}
		}

		#endregion

		private ListParameterQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ListParameter' table
	/// </summary>

	[System.Diagnostics.DebuggerDisplay("ListParameter ({KodeParameter},{KodePemeriksaan})")]
	[Serializable]
	public partial class ListParameter : esListParameter
	{
		public ListParameter()
		{

		}

		public ListParameter(DataRow row)
			: base(row)
		{

		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ListParameterMetadata.Meta();
			}
		}



		override protected esListParameterQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ListParameterQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion




		[BrowsableAttribute(false)]
		public ListParameterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ListParameterQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}


		public bool Load(ListParameterQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ListParameterQuery query;
	}



	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]

	public partial class ListParameterQuery : esListParameterQuery
	{
		public ListParameterQuery()
		{

		}

		public ListParameterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ListParameterQuery";
		}


	}


	[Serializable]
	public partial class ListParameterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ListParameterMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ListParameterMetadata.ColumnNames.KodeParameter, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ListParameterMetadata.PropertyNames.KodeParameter;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(ListParameterMetadata.ColumnNames.KodePemeriksaan, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ListParameterMetadata.PropertyNames.KodePemeriksaan;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(ListParameterMetadata.ColumnNames.NamaParameter, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ListParameterMetadata.PropertyNames.NamaParameter;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

		}
		#endregion

		static public ListParameterMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string KodeParameter = "KodeParameter";
			public const string KodePemeriksaan = "KodePemeriksaan";
			public const string NamaParameter = "NamaParameter";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string KodeParameter = "KodeParameter";
			public const string KodePemeriksaan = "KodePemeriksaan";
			public const string NamaParameter = "NamaParameter";
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
			lock (typeof(ListParameterMetadata))
			{
				if (ListParameterMetadata.mapDelegates == null)
				{
					ListParameterMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ListParameterMetadata.meta == null)
				{
					ListParameterMetadata.meta = new ListParameterMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();


				meta.AddTypeMap("KodeParameter", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodePemeriksaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaParameter", new esTypeMap("varchar", "System.String"));



				meta.Source = "ListParameter";
				meta.Destination = "ListParameter";

				meta.spInsert = "proc_ListParameterInsert";
				meta.spUpdate = "proc_ListParameterUpdate";
				meta.spDelete = "proc_ListParameterDelete";
				meta.spLoadAll = "proc_ListParameterLoadAll";
				meta.spLoadByPrimaryKey = "proc_ListParameterLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ListParameterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
