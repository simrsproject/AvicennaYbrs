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
	abstract public class esRlTxReport314Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport314Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport314Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport314Query query)
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
			this.InitQuery(query as esRlTxReport314Query);
		}
		#endregion
		
		virtual public RlTxReport314 DetachEntity(RlTxReport314 entity)
		{
			return base.DetachEntity(entity) as RlTxReport314;
		}
		
		virtual public RlTxReport314 AttachEntity(RlTxReport314 entity)
		{
			return base.AttachEntity(entity) as RlTxReport314;
		}
		
		virtual public void Combine(RlTxReport314Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport314 this[int index]
		{
			get
			{
				return base[index] as RlTxReport314;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport314);
		}
	}



	[Serializable]
	abstract public class esRlTxReport314 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport314Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport314()
		{

		}

		public esRlTxReport314(DataRow row)
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
			esRlTxReport314Query query = this.GetDynamicQuery();
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
						case "RujukanPuskesmas": this.str.RujukanPuskesmas = (string)value; break;							
						case "RujukanFasKesLain": this.str.RujukanFasKesLain = (string)value; break;							
						case "RujukanRsLain": this.str.RujukanRsLain = (string)value; break;							
						case "DirujukKePuskesmasAsal": this.str.DirujukKePuskesmasAsal = (string)value; break;							
						case "DirujukKeFasKesAsal": this.str.DirujukKeFasKesAsal = (string)value; break;							
						case "DirujukKeRsAsal": this.str.DirujukKeRsAsal = (string)value; break;							
						case "DirujukPasienRujukan": this.str.DirujukPasienRujukan = (string)value; break;							
						case "DirujukPasienDtgSendiri": this.str.DirujukPasienDtgSendiri = (string)value; break;							
						case "DirujukDiterimaKembali": this.str.DirujukDiterimaKembali = (string)value; break;							
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
						
						case "RujukanPuskesmas":
						
							if (value == null || value is System.Int32)
								this.RujukanPuskesmas = (System.Int32?)value;
							break;
						
						case "RujukanFasKesLain":
						
							if (value == null || value is System.Int32)
								this.RujukanFasKesLain = (System.Int32?)value;
							break;
						
						case "RujukanRsLain":
						
							if (value == null || value is System.Int32)
								this.RujukanRsLain = (System.Int32?)value;
							break;
						
						case "DirujukKePuskesmasAsal":
						
							if (value == null || value is System.Int32)
								this.DirujukKePuskesmasAsal = (System.Int32?)value;
							break;
						
						case "DirujukKeFasKesAsal":
						
							if (value == null || value is System.Int32)
								this.DirujukKeFasKesAsal = (System.Int32?)value;
							break;
						
						case "DirujukKeRsAsal":
						
							if (value == null || value is System.Int32)
								this.DirujukKeRsAsal = (System.Int32?)value;
							break;
						
						case "DirujukPasienRujukan":
						
							if (value == null || value is System.Int32)
								this.DirujukPasienRujukan = (System.Int32?)value;
							break;
						
						case "DirujukPasienDtgSendiri":
						
							if (value == null || value is System.Int32)
								this.DirujukPasienDtgSendiri = (System.Int32?)value;
							break;
						
						case "DirujukDiterimaKembali":
						
							if (value == null || value is System.Int32)
								this.DirujukDiterimaKembali = (System.Int32?)value;
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
		/// Maps to RlTxReport3_14.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport314Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport314Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.RujukanPuskesmas
		/// </summary>
		virtual public System.Int32? RujukanPuskesmas
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.RujukanPuskesmas);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.RujukanPuskesmas, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.RujukanFasKesLain
		/// </summary>
		virtual public System.Int32? RujukanFasKesLain
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.RujukanFasKesLain);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.RujukanFasKesLain, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.RujukanRsLain
		/// </summary>
		virtual public System.Int32? RujukanRsLain
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.RujukanRsLain);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.RujukanRsLain, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.DirujukKePuskesmasAsal
		/// </summary>
		virtual public System.Int32? DirujukKePuskesmasAsal
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukKePuskesmasAsal);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukKePuskesmasAsal, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.DirujukKeFasKesAsal
		/// </summary>
		virtual public System.Int32? DirujukKeFasKesAsal
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukKeFasKesAsal);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukKeFasKesAsal, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.DirujukKeRsAsal
		/// </summary>
		virtual public System.Int32? DirujukKeRsAsal
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukKeRsAsal);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukKeRsAsal, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.DirujukPasienRujukan
		/// </summary>
		virtual public System.Int32? DirujukPasienRujukan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukPasienRujukan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukPasienRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.DirujukPasienDtgSendiri
		/// </summary>
		virtual public System.Int32? DirujukPasienDtgSendiri
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukPasienDtgSendiri);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukPasienDtgSendiri, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.DirujukDiterimaKembali
		/// </summary>
		virtual public System.Int32? DirujukDiterimaKembali
		{
			get
			{
				return base.GetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukDiterimaKembali);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport314Metadata.ColumnNames.DirujukDiterimaKembali, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport314Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport314Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_14.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport314Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport314Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport314 entity)
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
				
			public System.String RujukanPuskesmas
			{
				get
				{
					System.Int32? data = entity.RujukanPuskesmas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RujukanPuskesmas = null;
					else entity.RujukanPuskesmas = Convert.ToInt32(value);
				}
			}
				
			public System.String RujukanFasKesLain
			{
				get
				{
					System.Int32? data = entity.RujukanFasKesLain;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RujukanFasKesLain = null;
					else entity.RujukanFasKesLain = Convert.ToInt32(value);
				}
			}
				
			public System.String RujukanRsLain
			{
				get
				{
					System.Int32? data = entity.RujukanRsLain;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RujukanRsLain = null;
					else entity.RujukanRsLain = Convert.ToInt32(value);
				}
			}
				
			public System.String DirujukKePuskesmasAsal
			{
				get
				{
					System.Int32? data = entity.DirujukKePuskesmasAsal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DirujukKePuskesmasAsal = null;
					else entity.DirujukKePuskesmasAsal = Convert.ToInt32(value);
				}
			}
				
			public System.String DirujukKeFasKesAsal
			{
				get
				{
					System.Int32? data = entity.DirujukKeFasKesAsal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DirujukKeFasKesAsal = null;
					else entity.DirujukKeFasKesAsal = Convert.ToInt32(value);
				}
			}
				
			public System.String DirujukKeRsAsal
			{
				get
				{
					System.Int32? data = entity.DirujukKeRsAsal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DirujukKeRsAsal = null;
					else entity.DirujukKeRsAsal = Convert.ToInt32(value);
				}
			}
				
			public System.String DirujukPasienRujukan
			{
				get
				{
					System.Int32? data = entity.DirujukPasienRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DirujukPasienRujukan = null;
					else entity.DirujukPasienRujukan = Convert.ToInt32(value);
				}
			}
				
			public System.String DirujukPasienDtgSendiri
			{
				get
				{
					System.Int32? data = entity.DirujukPasienDtgSendiri;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DirujukPasienDtgSendiri = null;
					else entity.DirujukPasienDtgSendiri = Convert.ToInt32(value);
				}
			}
				
			public System.String DirujukDiterimaKembali
			{
				get
				{
					System.Int32? data = entity.DirujukDiterimaKembali;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DirujukDiterimaKembali = null;
					else entity.DirujukDiterimaKembali = Convert.ToInt32(value);
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
			

			private esRlTxReport314 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport314Query query)
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
				throw new Exception("esRlTxReport314 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport314 : esRlTxReport314
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
	abstract public class esRlTxReport314Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport314Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RujukanPuskesmas
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.RujukanPuskesmas, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RujukanFasKesLain
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.RujukanFasKesLain, esSystemType.Int32);
			}
		} 
		
		public esQueryItem RujukanRsLain
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.RujukanRsLain, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DirujukKePuskesmasAsal
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.DirujukKePuskesmasAsal, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DirujukKeFasKesAsal
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.DirujukKeFasKesAsal, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DirujukKeRsAsal
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.DirujukKeRsAsal, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DirujukPasienRujukan
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.DirujukPasienRujukan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DirujukPasienDtgSendiri
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.DirujukPasienDtgSendiri, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DirujukDiterimaKembali
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.DirujukDiterimaKembali, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport314Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport314Collection")]
	public partial class RlTxReport314Collection : esRlTxReport314Collection, IEnumerable<RlTxReport314>
	{
		public RlTxReport314Collection()
		{

		}
		
		public static implicit operator List<RlTxReport314>(RlTxReport314Collection coll)
		{
			List<RlTxReport314> list = new List<RlTxReport314>();
			
			foreach (RlTxReport314 emp in coll)
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
				return  RlTxReport314Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport314Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport314(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport314();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport314Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport314Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport314Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport314 AddNew()
		{
			RlTxReport314 entity = base.AddNewEntity() as RlTxReport314;
			
			return entity;
		}

		public RlTxReport314 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport314;
		}


		#region IEnumerable<RlTxReport314> Members

		IEnumerator<RlTxReport314> IEnumerable<RlTxReport314>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport314;
			}
		}

		#endregion
		
		private RlTxReport314Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_14' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport314 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport314 : esRlTxReport314
	{
		public RlTxReport314()
		{

		}
	
		public RlTxReport314(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport314Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport314Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport314Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport314Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport314Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport314Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport314Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport314Query : esRlTxReport314Query
	{
		public RlTxReport314Query()
		{

		}		
		
		public RlTxReport314Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport314Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport314Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport314Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.RujukanPuskesmas, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.RujukanPuskesmas;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.RujukanFasKesLain, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.RujukanFasKesLain;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.RujukanRsLain, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.RujukanRsLain;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.DirujukKePuskesmasAsal, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.DirujukKePuskesmasAsal;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.DirujukKeFasKesAsal, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.DirujukKeFasKesAsal;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.DirujukKeRsAsal, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.DirujukKeRsAsal;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.DirujukPasienRujukan, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.DirujukPasienRujukan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.DirujukPasienDtgSendiri, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.DirujukPasienDtgSendiri;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.DirujukDiterimaKembali, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.DirujukDiterimaKembali;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.LastUpdateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport314Metadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport314Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport314Metadata Meta()
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
			 public const string RujukanPuskesmas = "RujukanPuskesmas";
			 public const string RujukanFasKesLain = "RujukanFasKesLain";
			 public const string RujukanRsLain = "RujukanRsLain";
			 public const string DirujukKePuskesmasAsal = "DirujukKePuskesmasAsal";
			 public const string DirujukKeFasKesAsal = "DirujukKeFasKesAsal";
			 public const string DirujukKeRsAsal = "DirujukKeRsAsal";
			 public const string DirujukPasienRujukan = "DirujukPasienRujukan";
			 public const string DirujukPasienDtgSendiri = "DirujukPasienDtgSendiri";
			 public const string DirujukDiterimaKembali = "DirujukDiterimaKembali";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string RujukanPuskesmas = "RujukanPuskesmas";
			 public const string RujukanFasKesLain = "RujukanFasKesLain";
			 public const string RujukanRsLain = "RujukanRsLain";
			 public const string DirujukKePuskesmasAsal = "DirujukKePuskesmasAsal";
			 public const string DirujukKeFasKesAsal = "DirujukKeFasKesAsal";
			 public const string DirujukKeRsAsal = "DirujukKeRsAsal";
			 public const string DirujukPasienRujukan = "DirujukPasienRujukan";
			 public const string DirujukPasienDtgSendiri = "DirujukPasienDtgSendiri";
			 public const string DirujukDiterimaKembali = "DirujukDiterimaKembali";
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
			lock (typeof(RlTxReport314Metadata))
			{
				if(RlTxReport314Metadata.mapDelegates == null)
				{
					RlTxReport314Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport314Metadata.meta == null)
				{
					RlTxReport314Metadata.meta = new RlTxReport314Metadata();
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
				meta.AddTypeMap("RujukanPuskesmas", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RujukanFasKesLain", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RujukanRsLain", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DirujukKePuskesmasAsal", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DirujukKeFasKesAsal", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DirujukKeRsAsal", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DirujukPasienRujukan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DirujukPasienDtgSendiri", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DirujukDiterimaKembali", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport3_14";
				meta.Destination = "RlTxReport3_14";
				
				meta.spInsert = "proc_RlTxReport3_14Insert";				
				meta.spUpdate = "proc_RlTxReport3_14Update";		
				meta.spDelete = "proc_RlTxReport3_14Delete";
				meta.spLoadAll = "proc_RlTxReport3_14LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_14LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport314Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
