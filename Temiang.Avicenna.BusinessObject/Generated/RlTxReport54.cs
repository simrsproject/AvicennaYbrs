/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:16 PM
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
	abstract public class esRlTxReport54Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport54Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport54Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport54Query query)
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
			this.InitQuery(query as esRlTxReport54Query);
		}
		#endregion
		
		virtual public RlTxReport54 DetachEntity(RlTxReport54 entity)
		{
			return base.DetachEntity(entity) as RlTxReport54;
		}
		
		virtual public RlTxReport54 AttachEntity(RlTxReport54 entity)
		{
			return base.AttachEntity(entity) as RlTxReport54;
		}
		
		virtual public void Combine(RlTxReport54Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport54 this[int index]
		{
			get
			{
				return base[index] as RlTxReport54;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport54);
		}
	}



	[Serializable]
	abstract public class esRlTxReport54 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport54Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport54()
		{

		}

		public esRlTxReport54(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String rlTxReportNo, System.String diagnosaID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, diagnosaID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, diagnosaID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String rlTxReportNo, System.String diagnosaID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(rlTxReportNo, diagnosaID);
			else
				return LoadByPrimaryKeyStoredProcedure(rlTxReportNo, diagnosaID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String rlTxReportNo, System.String diagnosaID)
		{
			esRlTxReport54Query query = this.GetDynamicQuery();
			query.Where(query.RlTxReportNo == rlTxReportNo, query.DiagnosaID == diagnosaID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String rlTxReportNo, System.String diagnosaID)
		{
			esParameters parms = new esParameters();
			parms.Add("RlTxReportNo",rlTxReportNo);			parms.Add("DiagnosaID",diagnosaID);
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
						case "DiagnosaID": this.str.DiagnosaID = (string)value; break;							
						case "KasusBaruL": this.str.KasusBaruL = (string)value; break;							
						case "KasusBaruP": this.str.KasusBaruP = (string)value; break;							
						case "JumlahKasusBaru": this.str.JumlahKasusBaru = (string)value; break;							
						case "JumlahKunjungan": this.str.JumlahKunjungan = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "KasusBaruL":
						
							if (value == null || value is System.Int32)
								this.KasusBaruL = (System.Int32?)value;
							break;
						
						case "KasusBaruP":
						
							if (value == null || value is System.Int32)
								this.KasusBaruP = (System.Int32?)value;
							break;
						
						case "JumlahKasusBaru":
						
							if (value == null || value is System.Int32)
								this.JumlahKasusBaru = (System.Int32?)value;
							break;
						
						case "JumlahKunjungan":
						
							if (value == null || value is System.Int32)
								this.JumlahKunjungan = (System.Int32?)value;
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
		/// Maps to RlTxReport5_4.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport54Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport54Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport5_4.DiagnosaID
		/// </summary>
		virtual public System.String DiagnosaID
		{
			get
			{
				return base.GetSystemString(RlTxReport54Metadata.ColumnNames.DiagnosaID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport54Metadata.ColumnNames.DiagnosaID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport5_4.KasusBaruL
		/// </summary>
		virtual public System.Int32? KasusBaruL
		{
			get
			{
				return base.GetSystemInt32(RlTxReport54Metadata.ColumnNames.KasusBaruL);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport54Metadata.ColumnNames.KasusBaruL, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport5_4.KasusBaruP
		/// </summary>
		virtual public System.Int32? KasusBaruP
		{
			get
			{
				return base.GetSystemInt32(RlTxReport54Metadata.ColumnNames.KasusBaruP);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport54Metadata.ColumnNames.KasusBaruP, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport5_4.JumlahKasusBaru
		/// </summary>
		virtual public System.Int32? JumlahKasusBaru
		{
			get
			{
				return base.GetSystemInt32(RlTxReport54Metadata.ColumnNames.JumlahKasusBaru);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport54Metadata.ColumnNames.JumlahKasusBaru, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport5_4.JumlahKunjungan
		/// </summary>
		virtual public System.Int32? JumlahKunjungan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport54Metadata.ColumnNames.JumlahKunjungan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport54Metadata.ColumnNames.JumlahKunjungan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport5_4.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport54Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport54Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport5_4.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport54Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport54Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport54 entity)
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
				
			public System.String DiagnosaID
			{
				get
				{
					System.String data = entity.DiagnosaID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnosaID = null;
					else entity.DiagnosaID = Convert.ToString(value);
				}
			}
				
			public System.String KasusBaruL
			{
				get
				{
					System.Int32? data = entity.KasusBaruL;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KasusBaruL = null;
					else entity.KasusBaruL = Convert.ToInt32(value);
				}
			}
				
			public System.String KasusBaruP
			{
				get
				{
					System.Int32? data = entity.KasusBaruP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KasusBaruP = null;
					else entity.KasusBaruP = Convert.ToInt32(value);
				}
			}
				
			public System.String JumlahKasusBaru
			{
				get
				{
					System.Int32? data = entity.JumlahKasusBaru;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JumlahKasusBaru = null;
					else entity.JumlahKasusBaru = Convert.ToInt32(value);
				}
			}
				
			public System.String JumlahKunjungan
			{
				get
				{
					System.Int32? data = entity.JumlahKunjungan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JumlahKunjungan = null;
					else entity.JumlahKunjungan = Convert.ToInt32(value);
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
			

			private esRlTxReport54 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport54Query query)
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
				throw new Exception("esRlTxReport54 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport54 : esRlTxReport54
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
	abstract public class esRlTxReport54Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport54Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport54Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem DiagnosaID
		{
			get
			{
				return new esQueryItem(this, RlTxReport54Metadata.ColumnNames.DiagnosaID, esSystemType.String);
			}
		} 
		
		public esQueryItem KasusBaruL
		{
			get
			{
				return new esQueryItem(this, RlTxReport54Metadata.ColumnNames.KasusBaruL, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KasusBaruP
		{
			get
			{
				return new esQueryItem(this, RlTxReport54Metadata.ColumnNames.KasusBaruP, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JumlahKasusBaru
		{
			get
			{
				return new esQueryItem(this, RlTxReport54Metadata.ColumnNames.JumlahKasusBaru, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JumlahKunjungan
		{
			get
			{
				return new esQueryItem(this, RlTxReport54Metadata.ColumnNames.JumlahKunjungan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport54Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport54Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport54Collection")]
	public partial class RlTxReport54Collection : esRlTxReport54Collection, IEnumerable<RlTxReport54>
	{
		public RlTxReport54Collection()
		{

		}
		
		public static implicit operator List<RlTxReport54>(RlTxReport54Collection coll)
		{
			List<RlTxReport54> list = new List<RlTxReport54>();
			
			foreach (RlTxReport54 emp in coll)
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
				return  RlTxReport54Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport54Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport54(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport54();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport54Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport54Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport54Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport54 AddNew()
		{
			RlTxReport54 entity = base.AddNewEntity() as RlTxReport54;
			
			return entity;
		}

		public RlTxReport54 FindByPrimaryKey(System.String rlTxReportNo, System.String diagnosaID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, diagnosaID) as RlTxReport54;
		}


		#region IEnumerable<RlTxReport54> Members

		IEnumerator<RlTxReport54> IEnumerable<RlTxReport54>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport54;
			}
		}

		#endregion
		
		private RlTxReport54Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport5_4' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport54 ({RlTxReportNo},{DiagnosaID})")]
	[Serializable]
	public partial class RlTxReport54 : esRlTxReport54
	{
		public RlTxReport54()
		{

		}
	
		public RlTxReport54(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport54Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport54Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport54Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport54Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport54Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport54Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport54Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport54Query : esRlTxReport54Query
	{
		public RlTxReport54Query()
		{

		}		
		
		public RlTxReport54Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport54Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport54Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport54Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport54Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport54Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport54Metadata.ColumnNames.DiagnosaID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport54Metadata.PropertyNames.DiagnosaID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport54Metadata.ColumnNames.KasusBaruL, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport54Metadata.PropertyNames.KasusBaruL;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport54Metadata.ColumnNames.KasusBaruP, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport54Metadata.PropertyNames.KasusBaruP;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport54Metadata.ColumnNames.JumlahKasusBaru, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport54Metadata.PropertyNames.JumlahKasusBaru;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport54Metadata.ColumnNames.JumlahKunjungan, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport54Metadata.PropertyNames.JumlahKunjungan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport54Metadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport54Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport54Metadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport54Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport54Metadata Meta()
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
			 public const string DiagnosaID = "DiagnosaID";
			 public const string KasusBaruL = "KasusBaruL";
			 public const string KasusBaruP = "KasusBaruP";
			 public const string JumlahKasusBaru = "JumlahKasusBaru";
			 public const string JumlahKunjungan = "JumlahKunjungan";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string DiagnosaID = "DiagnosaID";
			 public const string KasusBaruL = "KasusBaruL";
			 public const string KasusBaruP = "KasusBaruP";
			 public const string JumlahKasusBaru = "JumlahKasusBaru";
			 public const string JumlahKunjungan = "JumlahKunjungan";
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
			lock (typeof(RlTxReport54Metadata))
			{
				if(RlTxReport54Metadata.mapDelegates == null)
				{
					RlTxReport54Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport54Metadata.meta == null)
				{
					RlTxReport54Metadata.meta = new RlTxReport54Metadata();
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
				meta.AddTypeMap("DiagnosaID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KasusBaruL", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KasusBaruP", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JumlahKasusBaru", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JumlahKunjungan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport5_4";
				meta.Destination = "RlTxReport5_4";
				
				meta.spInsert = "proc_RlTxReport5_4Insert";				
				meta.spUpdate = "proc_RlTxReport5_4Update";		
				meta.spDelete = "proc_RlTxReport5_4Delete";
				meta.spLoadAll = "proc_RlTxReport5_4LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport5_4LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport54Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
