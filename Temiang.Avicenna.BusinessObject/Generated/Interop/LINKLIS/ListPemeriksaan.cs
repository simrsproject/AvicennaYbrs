/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/14/2020 4:12:03 PM
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
	abstract public class esListPemeriksaanCollection : esEntityCollectionWAuditLog
	{
		public esListPemeriksaanCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ListPemeriksaanCollection";
		}

		#region Query Logic
		protected void InitQuery(esListPemeriksaanQuery query)
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
			this.InitQuery(query as esListPemeriksaanQuery);
		}
		#endregion

		virtual public ListPemeriksaan DetachEntity(ListPemeriksaan entity)
		{
			return base.DetachEntity(entity) as ListPemeriksaan;
		}

		virtual public ListPemeriksaan AttachEntity(ListPemeriksaan entity)
		{
			return base.AttachEntity(entity) as ListPemeriksaan;
		}

		virtual public void Combine(ListPemeriksaanCollection collection)
		{
			base.Combine(collection);
		}

		new public ListPemeriksaan this[int index]
		{
			get
			{
				return base[index] as ListPemeriksaan;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ListPemeriksaan);
		}
	}



	[Serializable]
	abstract public class esListPemeriksaan : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esListPemeriksaanQuery GetDynamicQuery()
		{
			return null;
		}

		public esListPemeriksaan()
		{

		}

		public esListPemeriksaan(DataRow row)
			: base(row)
		{

		}

		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String kodePemeriksaan)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(kodePemeriksaan);
			else
				return LoadByPrimaryKeyStoredProcedure(kodePemeriksaan);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String kodePemeriksaan)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(kodePemeriksaan);
			else
				return LoadByPrimaryKeyStoredProcedure(kodePemeriksaan);
		}

		private bool LoadByPrimaryKeyDynamic(System.String kodePemeriksaan)
		{
			esListPemeriksaanQuery query = this.GetDynamicQuery();
			query.Where(query.KodePemeriksaan == kodePemeriksaan);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String kodePemeriksaan)
		{
			esParameters parms = new esParameters();
			parms.Add("KodePemeriksaan", kodePemeriksaan);
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
						case "KodePemeriksaan": this.str.KodePemeriksaan = (string)value; break;
						case "NamaPemeriksaan": this.str.NamaPemeriksaan = (string)value; break;
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
		/// Maps to ListPemeriksaan.KodePemeriksaan
		/// </summary>
		virtual public System.String KodePemeriksaan
		{
			get
			{
				return base.GetSystemString(ListPemeriksaanMetadata.ColumnNames.KodePemeriksaan);
			}

			set
			{
				base.SetSystemString(ListPemeriksaanMetadata.ColumnNames.KodePemeriksaan, value);
			}
		}

		/// <summary>
		/// Maps to ListPemeriksaan.NamaPemeriksaan
		/// </summary>
		virtual public System.String NamaPemeriksaan
		{
			get
			{
				return base.GetSystemString(ListPemeriksaanMetadata.ColumnNames.NamaPemeriksaan);
			}

			set
			{
				base.SetSystemString(ListPemeriksaanMetadata.ColumnNames.NamaPemeriksaan, value);
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
			public esStrings(esListPemeriksaan entity)
			{
				this.entity = entity;
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

			public System.String NamaPemeriksaan
			{
				get
				{
					System.String data = entity.NamaPemeriksaan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaPemeriksaan = null;
					else entity.NamaPemeriksaan = Convert.ToString(value);
				}
			}


			private esListPemeriksaan entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esListPemeriksaanQuery query)
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
				throw new Exception("esListPemeriksaan can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esListPemeriksaanQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ListPemeriksaanMetadata.Meta();
			}
		}


		public esQueryItem KodePemeriksaan
		{
			get
			{
				return new esQueryItem(this, ListPemeriksaanMetadata.ColumnNames.KodePemeriksaan, esSystemType.String);
			}
		}

		public esQueryItem NamaPemeriksaan
		{
			get
			{
				return new esQueryItem(this, ListPemeriksaanMetadata.ColumnNames.NamaPemeriksaan, esSystemType.String);
			}
		}

	}



	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ListPemeriksaanCollection")]
	public partial class ListPemeriksaanCollection : esListPemeriksaanCollection, IEnumerable<ListPemeriksaan>
	{
		public ListPemeriksaanCollection()
		{

		}

		public static implicit operator List<ListPemeriksaan>(ListPemeriksaanCollection coll)
		{
			List<ListPemeriksaan> list = new List<ListPemeriksaan>();

			foreach (ListPemeriksaan emp in coll)
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
				return ListPemeriksaanMetadata.Meta();
			}
		}



		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ListPemeriksaanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ListPemeriksaan(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ListPemeriksaan();
		}


		#endregion


		[BrowsableAttribute(false)]
		public ListPemeriksaanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ListPemeriksaanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ListPemeriksaanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		public ListPemeriksaan AddNew()
		{
			ListPemeriksaan entity = base.AddNewEntity() as ListPemeriksaan;

			return entity;
		}

		public ListPemeriksaan FindByPrimaryKey(System.String kodePemeriksaan)
		{
			return base.FindByPrimaryKey(kodePemeriksaan) as ListPemeriksaan;
		}


		#region IEnumerable<ListPemeriksaan> Members

		IEnumerator<ListPemeriksaan> IEnumerable<ListPemeriksaan>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ListPemeriksaan;
			}
		}

		#endregion

		private ListPemeriksaanQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ListPemeriksaan' table
	/// </summary>

	[System.Diagnostics.DebuggerDisplay("ListPemeriksaan ({KodePemeriksaan})")]
	[Serializable]
	public partial class ListPemeriksaan : esListPemeriksaan
	{
		public ListPemeriksaan()
		{

		}

		public ListPemeriksaan(DataRow row)
			: base(row)
		{

		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ListPemeriksaanMetadata.Meta();
			}
		}



		override protected esListPemeriksaanQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ListPemeriksaanQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion




		[BrowsableAttribute(false)]
		public ListPemeriksaanQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ListPemeriksaanQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}


		public bool Load(ListPemeriksaanQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ListPemeriksaanQuery query;
	}



	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]

	public partial class ListPemeriksaanQuery : esListPemeriksaanQuery
	{
		public ListPemeriksaanQuery()
		{

		}

		public ListPemeriksaanQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ListPemeriksaanQuery";
		}


	}


	[Serializable]
	public partial class ListPemeriksaanMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ListPemeriksaanMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ListPemeriksaanMetadata.ColumnNames.KodePemeriksaan, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ListPemeriksaanMetadata.PropertyNames.KodePemeriksaan;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(ListPemeriksaanMetadata.ColumnNames.NamaPemeriksaan, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ListPemeriksaanMetadata.PropertyNames.NamaPemeriksaan;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);

		}
		#endregion

		static public ListPemeriksaanMetadata Meta()
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
			public const string KodePemeriksaan = "KodePemeriksaan";
			public const string NamaPemeriksaan = "NamaPemeriksaan";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string KodePemeriksaan = "KodePemeriksaan";
			public const string NamaPemeriksaan = "NamaPemeriksaan";
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
			lock (typeof(ListPemeriksaanMetadata))
			{
				if (ListPemeriksaanMetadata.mapDelegates == null)
				{
					ListPemeriksaanMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ListPemeriksaanMetadata.meta == null)
				{
					ListPemeriksaanMetadata.meta = new ListPemeriksaanMetadata();
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


				meta.AddTypeMap("KodePemeriksaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaPemeriksaan", new esTypeMap("varchar", "System.String"));



				meta.Source = "ListPemeriksaan";
				meta.Destination = "ListPemeriksaan";

				meta.spInsert = "proc_ListPemeriksaanInsert";
				meta.spUpdate = "proc_ListPemeriksaanUpdate";
				meta.spDelete = "proc_ListPemeriksaanDelete";
				meta.spLoadAll = "proc_ListPemeriksaanLoadAll";
				meta.spLoadByPrimaryKey = "proc_ListPemeriksaanLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ListPemeriksaanMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
