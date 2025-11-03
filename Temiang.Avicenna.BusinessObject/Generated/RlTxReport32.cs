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
	abstract public class esRlTxReport32Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport32Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport32Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport32Query query)
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
			this.InitQuery(query as esRlTxReport32Query);
		}
		#endregion
		
		virtual public RlTxReport32 DetachEntity(RlTxReport32 entity)
		{
			return base.DetachEntity(entity) as RlTxReport32;
		}
		
		virtual public RlTxReport32 AttachEntity(RlTxReport32 entity)
		{
			return base.AttachEntity(entity) as RlTxReport32;
		}
		
		virtual public void Combine(RlTxReport32Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport32 this[int index]
		{
			get
			{
				return base[index] as RlTxReport32;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport32);
		}
	}



	[Serializable]
	abstract public class esRlTxReport32 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport32Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport32()
		{

		}

		public esRlTxReport32(DataRow row)
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
			esRlTxReport32Query query = this.GetDynamicQuery();
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
						case "PasienRujukan": this.str.PasienRujukan = (string)value; break;							
						case "PasienNonRujukan": this.str.PasienNonRujukan = (string)value; break;							
						case "DiRawat": this.str.DiRawat = (string)value; break;							
						case "DiRujuk": this.str.DiRujuk = (string)value; break;							
						case "Pulang": this.str.Pulang = (string)value; break;							
						case "MatiDiUgd": this.str.MatiDiUgd = (string)value; break;							
						case "Doa": this.str.Doa = (string)value; break;							
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
						
						case "PasienRujukan":
						
							if (value == null || value is System.Int32)
								this.PasienRujukan = (System.Int32?)value;
							break;
						
						case "PasienNonRujukan":
						
							if (value == null || value is System.Int32)
								this.PasienNonRujukan = (System.Int32?)value;
							break;
						
						case "DiRawat":
						
							if (value == null || value is System.Int32)
								this.DiRawat = (System.Int32?)value;
							break;
						
						case "DiRujuk":
						
							if (value == null || value is System.Int32)
								this.DiRujuk = (System.Int32?)value;
							break;
						
						case "Pulang":
						
							if (value == null || value is System.Int32)
								this.Pulang = (System.Int32?)value;
							break;
						
						case "MatiDiUgd":
						
							if (value == null || value is System.Int32)
								this.MatiDiUgd = (System.Int32?)value;
							break;
						
						case "Doa":
						
							if (value == null || value is System.Int32)
								this.Doa = (System.Int32?)value;
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
		/// Maps to RlTxReport3_2.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport32Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport32Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport32Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport32Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.PasienRujukan
		/// </summary>
		virtual public System.Int32? PasienRujukan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport32Metadata.ColumnNames.PasienRujukan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport32Metadata.ColumnNames.PasienRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.PasienNonRujukan
		/// </summary>
		virtual public System.Int32? PasienNonRujukan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport32Metadata.ColumnNames.PasienNonRujukan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport32Metadata.ColumnNames.PasienNonRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.DiRawat
		/// </summary>
		virtual public System.Int32? DiRawat
		{
			get
			{
				return base.GetSystemInt32(RlTxReport32Metadata.ColumnNames.DiRawat);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport32Metadata.ColumnNames.DiRawat, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.DiRujuk
		/// </summary>
		virtual public System.Int32? DiRujuk
		{
			get
			{
				return base.GetSystemInt32(RlTxReport32Metadata.ColumnNames.DiRujuk);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport32Metadata.ColumnNames.DiRujuk, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.Pulang
		/// </summary>
		virtual public System.Int32? Pulang
		{
			get
			{
				return base.GetSystemInt32(RlTxReport32Metadata.ColumnNames.Pulang);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport32Metadata.ColumnNames.Pulang, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.MatiDiUgd
		/// </summary>
		virtual public System.Int32? MatiDiUgd
		{
			get
			{
				return base.GetSystemInt32(RlTxReport32Metadata.ColumnNames.MatiDiUgd);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport32Metadata.ColumnNames.MatiDiUgd, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.Doa
		/// </summary>
		virtual public System.Int32? Doa
		{
			get
			{
				return base.GetSystemInt32(RlTxReport32Metadata.ColumnNames.Doa);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport32Metadata.ColumnNames.Doa, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport32Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport32Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_2.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport32Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport32Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport32 entity)
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
				
			public System.String PasienRujukan
			{
				get
				{
					System.Int32? data = entity.PasienRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PasienRujukan = null;
					else entity.PasienRujukan = Convert.ToInt32(value);
				}
			}
				
			public System.String PasienNonRujukan
			{
				get
				{
					System.Int32? data = entity.PasienNonRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PasienNonRujukan = null;
					else entity.PasienNonRujukan = Convert.ToInt32(value);
				}
			}
				
			public System.String DiRawat
			{
				get
				{
					System.Int32? data = entity.DiRawat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiRawat = null;
					else entity.DiRawat = Convert.ToInt32(value);
				}
			}
				
			public System.String DiRujuk
			{
				get
				{
					System.Int32? data = entity.DiRujuk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiRujuk = null;
					else entity.DiRujuk = Convert.ToInt32(value);
				}
			}
				
			public System.String Pulang
			{
				get
				{
					System.Int32? data = entity.Pulang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulang = null;
					else entity.Pulang = Convert.ToInt32(value);
				}
			}
				
			public System.String MatiDiUgd
			{
				get
				{
					System.Int32? data = entity.MatiDiUgd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MatiDiUgd = null;
					else entity.MatiDiUgd = Convert.ToInt32(value);
				}
			}
				
			public System.String Doa
			{
				get
				{
					System.Int32? data = entity.Doa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Doa = null;
					else entity.Doa = Convert.ToInt32(value);
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
			

			private esRlTxReport32 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport32Query query)
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
				throw new Exception("esRlTxReport32 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport32 : esRlTxReport32
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
	abstract public class esRlTxReport32Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport32Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PasienRujukan
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.PasienRujukan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PasienNonRujukan
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.PasienNonRujukan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DiRawat
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.DiRawat, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DiRujuk
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.DiRujuk, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Pulang
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.Pulang, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MatiDiUgd
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.MatiDiUgd, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Doa
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.Doa, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport32Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport32Collection")]
	public partial class RlTxReport32Collection : esRlTxReport32Collection, IEnumerable<RlTxReport32>
	{
		public RlTxReport32Collection()
		{

		}
		
		public static implicit operator List<RlTxReport32>(RlTxReport32Collection coll)
		{
			List<RlTxReport32> list = new List<RlTxReport32>();
			
			foreach (RlTxReport32 emp in coll)
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
				return  RlTxReport32Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport32Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport32(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport32();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport32Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport32Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport32Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport32 AddNew()
		{
			RlTxReport32 entity = base.AddNewEntity() as RlTxReport32;
			
			return entity;
		}

		public RlTxReport32 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport32;
		}


		#region IEnumerable<RlTxReport32> Members

		IEnumerator<RlTxReport32> IEnumerable<RlTxReport32>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport32;
			}
		}

		#endregion
		
		private RlTxReport32Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_2' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport32 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport32 : esRlTxReport32
	{
		public RlTxReport32()
		{

		}
	
		public RlTxReport32(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport32Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport32Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport32Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport32Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport32Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport32Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport32Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport32Query : esRlTxReport32Query
	{
		public RlTxReport32Query()
		{

		}		
		
		public RlTxReport32Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport32Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport32Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport32Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.PasienRujukan, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.PasienRujukan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.PasienNonRujukan, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.PasienNonRujukan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.DiRawat, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.DiRawat;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.DiRujuk, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.DiRujuk;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.Pulang, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.Pulang;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.MatiDiUgd, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.MatiDiUgd;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.Doa, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.Doa;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport32Metadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport32Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport32Metadata Meta()
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
			 public const string PasienRujukan = "PasienRujukan";
			 public const string PasienNonRujukan = "PasienNonRujukan";
			 public const string DiRawat = "DiRawat";
			 public const string DiRujuk = "DiRujuk";
			 public const string Pulang = "Pulang";
			 public const string MatiDiUgd = "MatiDiUgd";
			 public const string Doa = "Doa";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string PasienRujukan = "PasienRujukan";
			 public const string PasienNonRujukan = "PasienNonRujukan";
			 public const string DiRawat = "DiRawat";
			 public const string DiRujuk = "DiRujuk";
			 public const string Pulang = "Pulang";
			 public const string MatiDiUgd = "MatiDiUgd";
			 public const string Doa = "Doa";
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
			lock (typeof(RlTxReport32Metadata))
			{
				if(RlTxReport32Metadata.mapDelegates == null)
				{
					RlTxReport32Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport32Metadata.meta == null)
				{
					RlTxReport32Metadata.meta = new RlTxReport32Metadata();
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
				meta.AddTypeMap("PasienRujukan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PasienNonRujukan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DiRawat", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DiRujuk", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Pulang", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MatiDiUgd", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Doa", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport3_2";
				meta.Destination = "RlTxReport3_2";
				
				meta.spInsert = "proc_RlTxReport3_2Insert";				
				meta.spUpdate = "proc_RlTxReport3_2Update";		
				meta.spDelete = "proc_RlTxReport3_2Delete";
				meta.spLoadAll = "proc_RlTxReport3_2LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_2LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport32Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
