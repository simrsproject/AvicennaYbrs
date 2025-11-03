/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:15 PM
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
	abstract public class esRlTxReport313bCollection : esEntityCollectionWAuditLog
	{
		public esRlTxReport313bCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport313bCollection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport313bQuery query)
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
			this.InitQuery(query as esRlTxReport313bQuery);
		}
		#endregion
		
		virtual public RlTxReport313b DetachEntity(RlTxReport313b entity)
		{
			return base.DetachEntity(entity) as RlTxReport313b;
		}
		
		virtual public RlTxReport313b AttachEntity(RlTxReport313b entity)
		{
			return base.AttachEntity(entity) as RlTxReport313b;
		}
		
		virtual public void Combine(RlTxReport313bCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport313b this[int index]
		{
			get
			{
				return base[index] as RlTxReport313b;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport313b);
		}
	}



	[Serializable]
	abstract public class esRlTxReport313b : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport313bQuery GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport313b()
		{

		}

		public esRlTxReport313b(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, rlMasterReportItemID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, rlMasterReportItemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			esRlTxReport313bQuery query = this.GetDynamicQuery();
			query.Where(query.RlTxReportNo == rlTxReportNo, query.RlMasterReportItemID == rlMasterReportItemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			esParameters parms = new esParameters();
			parms.Add("RlTxReportNo",rlTxReportNo);			parms.Add("RlMasterReportItemID",rlMasterReportItemID);
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
						case "RlTxReportNo": this.str.RlTxReportNo = (string)value; break;							
						case "RlMasterReportItemID": this.str.RlMasterReportItemID = (string)value; break;							
						case "JumlahItemObat": this.str.JumlahItemObat = (string)value; break;							
						case "JumlahItemObatRs": this.str.JumlahItemObatRs = (string)value; break;							
						case "JumlahItemObatFormulariumRs": this.str.JumlahItemObatFormulariumRs = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RlMasterReportItemID":
						
							if (value == null || value is System.Int32)
								this.RlMasterReportItemID = (System.Int32?)value;
							break;
						
						case "JumlahItemObat":
						
							if (value == null || value is System.Int32)
								this.JumlahItemObat = (System.Int32?)value;
							break;
						
						case "JumlahItemObatRs":
						
							if (value == null || value is System.Int32)
								this.JumlahItemObatRs = (System.Int32?)value;
							break;
						
						case "JumlahItemObatFormulariumRs":
						
							if (value == null || value is System.Int32)
								this.JumlahItemObatFormulariumRs = (System.Int32?)value;
							break;
						
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
		/// Maps to RlTxReport3_13b.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport313bMetadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport313bMetadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_13b.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport313bMetadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport313bMetadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_13b.JumlahItemObat
		/// </summary>
		virtual public System.Int32? JumlahItemObat
		{
			get
			{
				return base.GetSystemInt32(RlTxReport313bMetadata.ColumnNames.JumlahItemObat);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport313bMetadata.ColumnNames.JumlahItemObat, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_13b.JumlahItemObatRs
		/// </summary>
		virtual public System.Int32? JumlahItemObatRs
		{
			get
			{
				return base.GetSystemInt32(RlTxReport313bMetadata.ColumnNames.JumlahItemObatRs);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport313bMetadata.ColumnNames.JumlahItemObatRs, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_13b.JumlahItemObatFormulariumRs
		/// </summary>
		virtual public System.Int32? JumlahItemObatFormulariumRs
		{
			get
			{
				return base.GetSystemInt32(RlTxReport313bMetadata.ColumnNames.JumlahItemObatFormulariumRs);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport313bMetadata.ColumnNames.JumlahItemObatFormulariumRs, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_13b.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport313bMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport313bMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_13b.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport313bMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport313bMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport313b entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RlTxReportNo
			{
				get
				{
					System.String data = entity.RlTxReportNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlTxReportNo = null;
					else entity.RlTxReportNo = Convert.ToString(value);
				}
			}
				
			public System.String RlMasterReportItemID
			{
				get
				{
					System.Int32? data = entity.RlMasterReportItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RlMasterReportItemID = null;
					else entity.RlMasterReportItemID = Convert.ToInt32(value);
				}
			}
				
			public System.String JumlahItemObat
			{
				get
				{
					System.Int32? data = entity.JumlahItemObat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JumlahItemObat = null;
					else entity.JumlahItemObat = Convert.ToInt32(value);
				}
			}
				
			public System.String JumlahItemObatRs
			{
				get
				{
					System.Int32? data = entity.JumlahItemObatRs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JumlahItemObatRs = null;
					else entity.JumlahItemObatRs = Convert.ToInt32(value);
				}
			}
				
			public System.String JumlahItemObatFormulariumRs
			{
				get
				{
					System.Int32? data = entity.JumlahItemObatFormulariumRs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JumlahItemObatFormulariumRs = null;
					else entity.JumlahItemObatFormulariumRs = Convert.ToInt32(value);
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
			

			private esRlTxReport313b entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport313bQuery query)
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
				throw new Exception("esRlTxReport313b can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport313b : esRlTxReport313b
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
	abstract public class esRlTxReport313bQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport313bMetadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport313bMetadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport313bMetadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JumlahItemObat
		{
			get
			{
				return new esQueryItem(this, RlTxReport313bMetadata.ColumnNames.JumlahItemObat, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JumlahItemObatRs
		{
			get
			{
				return new esQueryItem(this, RlTxReport313bMetadata.ColumnNames.JumlahItemObatRs, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JumlahItemObatFormulariumRs
		{
			get
			{
				return new esQueryItem(this, RlTxReport313bMetadata.ColumnNames.JumlahItemObatFormulariumRs, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport313bMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport313bMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport313bCollection")]
	public partial class RlTxReport313bCollection : esRlTxReport313bCollection, IEnumerable<RlTxReport313b>
	{
		public RlTxReport313bCollection()
		{

		}
		
		public static implicit operator List<RlTxReport313b>(RlTxReport313bCollection coll)
		{
			List<RlTxReport313b> list = new List<RlTxReport313b>();
			
			foreach (RlTxReport313b emp in coll)
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
				return  RlTxReport313bMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport313bQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport313b(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport313b();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport313bQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport313bQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport313bQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport313b AddNew()
		{
			RlTxReport313b entity = base.AddNewEntity() as RlTxReport313b;
			
			return entity;
		}

		public RlTxReport313b FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport313b;
		}


		#region IEnumerable<RlTxReport313b> Members

		IEnumerator<RlTxReport313b> IEnumerable<RlTxReport313b>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport313b;
			}
		}

		#endregion
		
		private RlTxReport313bQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_13b' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport313b ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport313b : esRlTxReport313b
	{
		public RlTxReport313b()
		{

		}
	
		public RlTxReport313b(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport313bMetadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport313bQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport313bQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport313bQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport313bQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport313bQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport313bQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport313bQuery : esRlTxReport313bQuery
	{
		public RlTxReport313bQuery()
		{

		}		
		
		public RlTxReport313bQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport313bQuery";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport313bMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport313bMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport313bMetadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport313bMetadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport313bMetadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport313bMetadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport313bMetadata.ColumnNames.JumlahItemObat, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport313bMetadata.PropertyNames.JumlahItemObat;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport313bMetadata.ColumnNames.JumlahItemObatRs, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport313bMetadata.PropertyNames.JumlahItemObatRs;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport313bMetadata.ColumnNames.JumlahItemObatFormulariumRs, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport313bMetadata.PropertyNames.JumlahItemObatFormulariumRs;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport313bMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport313bMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport313bMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport313bMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport313bMetadata Meta()
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
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string JumlahItemObat = "JumlahItemObat";
			 public const string JumlahItemObatRs = "JumlahItemObatRs";
			 public const string JumlahItemObatFormulariumRs = "JumlahItemObatFormulariumRs";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string JumlahItemObat = "JumlahItemObat";
			 public const string JumlahItemObatRs = "JumlahItemObatRs";
			 public const string JumlahItemObatFormulariumRs = "JumlahItemObatFormulariumRs";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(RlTxReport313bMetadata))
			{
				if(RlTxReport313bMetadata.mapDelegates == null)
				{
					RlTxReport313bMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport313bMetadata.meta == null)
				{
					RlTxReport313bMetadata.meta = new RlTxReport313bMetadata();
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
				

				meta.AddTypeMap("RlTxReportNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RlMasterReportItemID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JumlahItemObat", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JumlahItemObatRs", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JumlahItemObatFormulariumRs", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport3_13b";
				meta.Destination = "RlTxReport3_13b";
				
				meta.spInsert = "proc_RlTxReport3_13bInsert";				
				meta.spUpdate = "proc_RlTxReport3_13bUpdate";		
				meta.spDelete = "proc_RlTxReport3_13bDelete";
				meta.spLoadAll = "proc_RlTxReport3_13bLoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_13bLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport313bMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
