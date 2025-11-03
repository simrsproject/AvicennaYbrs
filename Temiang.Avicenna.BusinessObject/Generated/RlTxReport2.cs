/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2013 12:20:14 PM
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
	abstract public class esRlTxReport2Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport2Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport2Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport2Query query)
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
			this.InitQuery(query as esRlTxReport2Query);
		}
		#endregion
		
		virtual public RlTxReport2 DetachEntity(RlTxReport2 entity)
		{
			return base.DetachEntity(entity) as RlTxReport2;
		}
		
		virtual public RlTxReport2 AttachEntity(RlTxReport2 entity)
		{
			return base.AttachEntity(entity) as RlTxReport2;
		}
		
		virtual public void Combine(RlTxReport2Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport2 this[int index]
		{
			get
			{
				return base[index] as RlTxReport2;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport2);
		}
	}



	[Serializable]
	abstract public class esRlTxReport2 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport2Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport2()
		{

		}

		public esRlTxReport2(DataRow row)
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
			esRlTxReport2Query query = this.GetDynamicQuery();
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
						case "KeadaanLakiLaki": this.str.KeadaanLakiLaki = (string)value; break;							
						case "KeadaanPerempuan": this.str.KeadaanPerempuan = (string)value; break;							
						case "KebutuhanLakiLaki": this.str.KebutuhanLakiLaki = (string)value; break;							
						case "KebutuhanPerempuan": this.str.KebutuhanPerempuan = (string)value; break;							
						case "KekuranganLakiLaki": this.str.KekuranganLakiLaki = (string)value; break;							
						case "KekuranganPerempuan": this.str.KekuranganPerempuan = (string)value; break;							
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
						
						case "KeadaanLakiLaki":
						
							if (value == null || value is System.Int32)
								this.KeadaanLakiLaki = (System.Int32?)value;
							break;
						
						case "KeadaanPerempuan":
						
							if (value == null || value is System.Int32)
								this.KeadaanPerempuan = (System.Int32?)value;
							break;
						
						case "KebutuhanLakiLaki":
						
							if (value == null || value is System.Int32)
								this.KebutuhanLakiLaki = (System.Int32?)value;
							break;
						
						case "KebutuhanPerempuan":
						
							if (value == null || value is System.Int32)
								this.KebutuhanPerempuan = (System.Int32?)value;
							break;
						
						case "KekuranganLakiLaki":
						
							if (value == null || value is System.Int32)
								this.KekuranganLakiLaki = (System.Int32?)value;
							break;
						
						case "KekuranganPerempuan":
						
							if (value == null || value is System.Int32)
								this.KekuranganPerempuan = (System.Int32?)value;
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
		/// Maps to RlTxReport2.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport2Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport2Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport2Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport2Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.KeadaanLakiLaki
		/// </summary>
		virtual public System.Int32? KeadaanLakiLaki
		{
			get
			{
				return base.GetSystemInt32(RlTxReport2Metadata.ColumnNames.KeadaanLakiLaki);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport2Metadata.ColumnNames.KeadaanLakiLaki, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.KeadaanPerempuan
		/// </summary>
		virtual public System.Int32? KeadaanPerempuan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport2Metadata.ColumnNames.KeadaanPerempuan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport2Metadata.ColumnNames.KeadaanPerempuan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.KebutuhanLakiLaki
		/// </summary>
		virtual public System.Int32? KebutuhanLakiLaki
		{
			get
			{
				return base.GetSystemInt32(RlTxReport2Metadata.ColumnNames.KebutuhanLakiLaki);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport2Metadata.ColumnNames.KebutuhanLakiLaki, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.KebutuhanPerempuan
		/// </summary>
		virtual public System.Int32? KebutuhanPerempuan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport2Metadata.ColumnNames.KebutuhanPerempuan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport2Metadata.ColumnNames.KebutuhanPerempuan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.KekuranganLakiLaki
		/// </summary>
		virtual public System.Int32? KekuranganLakiLaki
		{
			get
			{
				return base.GetSystemInt32(RlTxReport2Metadata.ColumnNames.KekuranganLakiLaki);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport2Metadata.ColumnNames.KekuranganLakiLaki, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.KekuranganPerempuan
		/// </summary>
		virtual public System.Int32? KekuranganPerempuan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport2Metadata.ColumnNames.KekuranganPerempuan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport2Metadata.ColumnNames.KekuranganPerempuan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport2Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport2Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport2.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport2Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport2Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport2 entity)
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
				
			public System.String KeadaanLakiLaki
			{
				get
				{
					System.Int32? data = entity.KeadaanLakiLaki;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeadaanLakiLaki = null;
					else entity.KeadaanLakiLaki = Convert.ToInt32(value);
				}
			}
				
			public System.String KeadaanPerempuan
			{
				get
				{
					System.Int32? data = entity.KeadaanPerempuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeadaanPerempuan = null;
					else entity.KeadaanPerempuan = Convert.ToInt32(value);
				}
			}
				
			public System.String KebutuhanLakiLaki
			{
				get
				{
					System.Int32? data = entity.KebutuhanLakiLaki;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KebutuhanLakiLaki = null;
					else entity.KebutuhanLakiLaki = Convert.ToInt32(value);
				}
			}
				
			public System.String KebutuhanPerempuan
			{
				get
				{
					System.Int32? data = entity.KebutuhanPerempuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KebutuhanPerempuan = null;
					else entity.KebutuhanPerempuan = Convert.ToInt32(value);
				}
			}
				
			public System.String KekuranganLakiLaki
			{
				get
				{
					System.Int32? data = entity.KekuranganLakiLaki;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KekuranganLakiLaki = null;
					else entity.KekuranganLakiLaki = Convert.ToInt32(value);
				}
			}
				
			public System.String KekuranganPerempuan
			{
				get
				{
					System.Int32? data = entity.KekuranganPerempuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KekuranganPerempuan = null;
					else entity.KekuranganPerempuan = Convert.ToInt32(value);
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
			

			private esRlTxReport2 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport2Query query)
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
				throw new Exception("esRlTxReport2 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport2 : esRlTxReport2
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
	abstract public class esRlTxReport2Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport2Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KeadaanLakiLaki
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.KeadaanLakiLaki, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KeadaanPerempuan
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.KeadaanPerempuan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KebutuhanLakiLaki
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.KebutuhanLakiLaki, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KebutuhanPerempuan
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.KebutuhanPerempuan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KekuranganLakiLaki
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.KekuranganLakiLaki, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KekuranganPerempuan
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.KekuranganPerempuan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport2Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport2Collection")]
	public partial class RlTxReport2Collection : esRlTxReport2Collection, IEnumerable<RlTxReport2>
	{
		public RlTxReport2Collection()
		{

		}
		
		public static implicit operator List<RlTxReport2>(RlTxReport2Collection coll)
		{
			List<RlTxReport2> list = new List<RlTxReport2>();
			
			foreach (RlTxReport2 emp in coll)
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
				return  RlTxReport2Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport2Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport2(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport2();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport2Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport2Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport2Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport2 AddNew()
		{
			RlTxReport2 entity = base.AddNewEntity() as RlTxReport2;
			
			return entity;
		}

		public RlTxReport2 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport2;
		}


		#region IEnumerable<RlTxReport2> Members

		IEnumerator<RlTxReport2> IEnumerable<RlTxReport2>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport2;
			}
		}

		#endregion
		
		private RlTxReport2Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport2' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport2 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport2 : esRlTxReport2
	{
		public RlTxReport2()
		{

		}
	
		public RlTxReport2(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport2Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport2Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport2Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport2Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport2Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport2Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport2Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport2Query : esRlTxReport2Query
	{
		public RlTxReport2Query()
		{

		}		
		
		public RlTxReport2Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport2Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport2Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport2Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.KeadaanLakiLaki, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.KeadaanLakiLaki;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.KeadaanPerempuan, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.KeadaanPerempuan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.KebutuhanLakiLaki, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.KebutuhanLakiLaki;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.KebutuhanPerempuan, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.KebutuhanPerempuan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.KekuranganLakiLaki, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.KekuranganLakiLaki;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.KekuranganPerempuan, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.KekuranganPerempuan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport2Metadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport2Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport2Metadata Meta()
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
			 public const string KeadaanLakiLaki = "KeadaanLakiLaki";
			 public const string KeadaanPerempuan = "KeadaanPerempuan";
			 public const string KebutuhanLakiLaki = "KebutuhanLakiLaki";
			 public const string KebutuhanPerempuan = "KebutuhanPerempuan";
			 public const string KekuranganLakiLaki = "KekuranganLakiLaki";
			 public const string KekuranganPerempuan = "KekuranganPerempuan";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string KeadaanLakiLaki = "KeadaanLakiLaki";
			 public const string KeadaanPerempuan = "KeadaanPerempuan";
			 public const string KebutuhanLakiLaki = "KebutuhanLakiLaki";
			 public const string KebutuhanPerempuan = "KebutuhanPerempuan";
			 public const string KekuranganLakiLaki = "KekuranganLakiLaki";
			 public const string KekuranganPerempuan = "KekuranganPerempuan";
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
			lock (typeof(RlTxReport2Metadata))
			{
				if(RlTxReport2Metadata.mapDelegates == null)
				{
					RlTxReport2Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport2Metadata.meta == null)
				{
					RlTxReport2Metadata.meta = new RlTxReport2Metadata();
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
				meta.AddTypeMap("KeadaanLakiLaki", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KeadaanPerempuan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KebutuhanLakiLaki", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KebutuhanPerempuan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KekuranganLakiLaki", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KekuranganPerempuan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport2";
				meta.Destination = "RlTxReport2";
				
				meta.spInsert = "proc_RlTxReport2Insert";				
				meta.spUpdate = "proc_RlTxReport2Update";		
				meta.spDelete = "proc_RlTxReport2Delete";
				meta.spLoadAll = "proc_RlTxReport2LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport2LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport2Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
