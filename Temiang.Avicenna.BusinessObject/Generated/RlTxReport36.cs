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
	abstract public class esRlTxReport36Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport36Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport36Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport36Query query)
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
			this.InitQuery(query as esRlTxReport36Query);
		}
		#endregion
		
		virtual public RlTxReport36 DetachEntity(RlTxReport36 entity)
		{
			return base.DetachEntity(entity) as RlTxReport36;
		}
		
		virtual public RlTxReport36 AttachEntity(RlTxReport36 entity)
		{
			return base.AttachEntity(entity) as RlTxReport36;
		}
		
		virtual public void Combine(RlTxReport36Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport36 this[int index]
		{
			get
			{
				return base[index] as RlTxReport36;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport36);
		}
	}



	[Serializable]
	abstract public class esRlTxReport36 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport36Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport36()
		{

		}

		public esRlTxReport36(DataRow row)
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
			esRlTxReport36Query query = this.GetDynamicQuery();
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
						case "Total": this.str.Total = (string)value; break;							
						case "Khusus": this.str.Khusus = (string)value; break;							
						case "Besar": this.str.Besar = (string)value; break;							
						case "Sedang": this.str.Sedang = (string)value; break;							
						case "Kecil": this.str.Kecil = (string)value; break;							
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
						
						case "Total":
						
							if (value == null || value is System.Int32)
								this.Total = (System.Int32?)value;
							break;
						
						case "Khusus":
						
							if (value == null || value is System.Int32)
								this.Khusus = (System.Int32?)value;
							break;
						
						case "Besar":
						
							if (value == null || value is System.Int32)
								this.Besar = (System.Int32?)value;
							break;
						
						case "Sedang":
						
							if (value == null || value is System.Int32)
								this.Sedang = (System.Int32?)value;
							break;
						
						case "Kecil":
						
							if (value == null || value is System.Int32)
								this.Kecil = (System.Int32?)value;
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
		/// Maps to RlTxReport3_6.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport36Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport36Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_6.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport36Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport36Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_6.Total
		/// </summary>
		virtual public System.Int32? Total
		{
			get
			{
				return base.GetSystemInt32(RlTxReport36Metadata.ColumnNames.Total);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport36Metadata.ColumnNames.Total, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_6.Khusus
		/// </summary>
		virtual public System.Int32? Khusus
		{
			get
			{
				return base.GetSystemInt32(RlTxReport36Metadata.ColumnNames.Khusus);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport36Metadata.ColumnNames.Khusus, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_6.Besar
		/// </summary>
		virtual public System.Int32? Besar
		{
			get
			{
				return base.GetSystemInt32(RlTxReport36Metadata.ColumnNames.Besar);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport36Metadata.ColumnNames.Besar, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_6.Sedang
		/// </summary>
		virtual public System.Int32? Sedang
		{
			get
			{
				return base.GetSystemInt32(RlTxReport36Metadata.ColumnNames.Sedang);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport36Metadata.ColumnNames.Sedang, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_6.Kecil
		/// </summary>
		virtual public System.Int32? Kecil
		{
			get
			{
				return base.GetSystemInt32(RlTxReport36Metadata.ColumnNames.Kecil);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport36Metadata.ColumnNames.Kecil, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_6.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport36Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport36Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_6.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport36Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport36Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport36 entity)
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
				
			public System.String Total
			{
				get
				{
					System.Int32? data = entity.Total;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Total = null;
					else entity.Total = Convert.ToInt32(value);
				}
			}
				
			public System.String Khusus
			{
				get
				{
					System.Int32? data = entity.Khusus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Khusus = null;
					else entity.Khusus = Convert.ToInt32(value);
				}
			}
				
			public System.String Besar
			{
				get
				{
					System.Int32? data = entity.Besar;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Besar = null;
					else entity.Besar = Convert.ToInt32(value);
				}
			}
				
			public System.String Sedang
			{
				get
				{
					System.Int32? data = entity.Sedang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sedang = null;
					else entity.Sedang = Convert.ToInt32(value);
				}
			}
				
			public System.String Kecil
			{
				get
				{
					System.Int32? data = entity.Kecil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kecil = null;
					else entity.Kecil = Convert.ToInt32(value);
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
			

			private esRlTxReport36 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport36Query query)
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
				throw new Exception("esRlTxReport36 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport36 : esRlTxReport36
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
	abstract public class esRlTxReport36Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport36Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Total
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.Total, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Khusus
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.Khusus, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Besar
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.Besar, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Sedang
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.Sedang, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Kecil
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.Kecil, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport36Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport36Collection")]
	public partial class RlTxReport36Collection : esRlTxReport36Collection, IEnumerable<RlTxReport36>
	{
		public RlTxReport36Collection()
		{

		}
		
		public static implicit operator List<RlTxReport36>(RlTxReport36Collection coll)
		{
			List<RlTxReport36> list = new List<RlTxReport36>();
			
			foreach (RlTxReport36 emp in coll)
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
				return  RlTxReport36Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport36Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport36(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport36();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport36Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport36Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport36Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport36 AddNew()
		{
			RlTxReport36 entity = base.AddNewEntity() as RlTxReport36;
			
			return entity;
		}

		public RlTxReport36 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport36;
		}


		#region IEnumerable<RlTxReport36> Members

		IEnumerator<RlTxReport36> IEnumerable<RlTxReport36>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport36;
			}
		}

		#endregion
		
		private RlTxReport36Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_6' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport36 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport36 : esRlTxReport36
	{
		public RlTxReport36()
		{

		}
	
		public RlTxReport36(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport36Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport36Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport36Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport36Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport36Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport36Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport36Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport36Query : esRlTxReport36Query
	{
		public RlTxReport36Query()
		{

		}		
		
		public RlTxReport36Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport36Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport36Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport36Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.Total, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.Total;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.Khusus, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.Khusus;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.Besar, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.Besar;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.Sedang, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.Sedang;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.Kecil, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.Kecil;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport36Metadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport36Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport36Metadata Meta()
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
			 public const string Total = "Total";
			 public const string Khusus = "Khusus";
			 public const string Besar = "Besar";
			 public const string Sedang = "Sedang";
			 public const string Kecil = "Kecil";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string Total = "Total";
			 public const string Khusus = "Khusus";
			 public const string Besar = "Besar";
			 public const string Sedang = "Sedang";
			 public const string Kecil = "Kecil";
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
			lock (typeof(RlTxReport36Metadata))
			{
				if(RlTxReport36Metadata.mapDelegates == null)
				{
					RlTxReport36Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport36Metadata.meta == null)
				{
					RlTxReport36Metadata.meta = new RlTxReport36Metadata();
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
				meta.AddTypeMap("Total", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Khusus", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Besar", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Sedang", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Kecil", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport3_6";
				meta.Destination = "RlTxReport3_6";
				
				meta.spInsert = "proc_RlTxReport3_6Insert";				
				meta.spUpdate = "proc_RlTxReport3_6Update";		
				meta.spDelete = "proc_RlTxReport3_6Delete";
				meta.spLoadAll = "proc_RlTxReport3_6LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_6LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport36Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
