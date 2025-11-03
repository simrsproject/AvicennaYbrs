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
	abstract public class esRlTxReport312Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport312Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport312Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport312Query query)
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
			this.InitQuery(query as esRlTxReport312Query);
		}
		#endregion
		
		virtual public RlTxReport312 DetachEntity(RlTxReport312 entity)
		{
			return base.DetachEntity(entity) as RlTxReport312;
		}
		
		virtual public RlTxReport312 AttachEntity(RlTxReport312 entity)
		{
			return base.AttachEntity(entity) as RlTxReport312;
		}
		
		virtual public void Combine(RlTxReport312Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport312 this[int index]
		{
			get
			{
				return base[index] as RlTxReport312;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport312);
		}
	}



	[Serializable]
	abstract public class esRlTxReport312 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport312Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport312()
		{

		}

		public esRlTxReport312(DataRow row)
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
			esRlTxReport312Query query = this.GetDynamicQuery();
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
						case "KonselingAnc": this.str.KonselingAnc = (string)value; break;							
						case "KonselingPascaPersalinan": this.str.KonselingPascaPersalinan = (string)value; break;							
						case "KbBaruCmBukanRujukan": this.str.KbBaruCmBukanRujukan = (string)value; break;							
						case "KbBaruCmRujukanRi": this.str.KbBaruCmRujukanRi = (string)value; break;							
						case "KbBaruCmRujukanRj": this.str.KbBaruCmRujukanRj = (string)value; break;							
						case "KbBaruCmTotal": this.str.KbBaruCmTotal = (string)value; break;							
						case "KbBaruDkNifas": this.str.KbBaruDkNifas = (string)value; break;							
						case "KbBaruDkAbortus": this.str.KbBaruDkAbortus = (string)value; break;							
						case "KbBaruDkLain": this.str.KbBaruDkLain = (string)value; break;							
						case "KunjunganUlang": this.str.KunjunganUlang = (string)value; break;							
						case "KeluhanEfekSamping": this.str.KeluhanEfekSamping = (string)value; break;							
						case "KeluhanEfekSampingDiRujuk": this.str.KeluhanEfekSampingDiRujuk = (string)value; break;							
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
						
						case "KonselingAnc":
						
							if (value == null || value is System.Int32)
								this.KonselingAnc = (System.Int32?)value;
							break;
						
						case "KonselingPascaPersalinan":
						
							if (value == null || value is System.Int32)
								this.KonselingPascaPersalinan = (System.Int32?)value;
							break;
						
						case "KbBaruCmBukanRujukan":
						
							if (value == null || value is System.Int32)
								this.KbBaruCmBukanRujukan = (System.Int32?)value;
							break;
						
						case "KbBaruCmRujukanRi":
						
							if (value == null || value is System.Int32)
								this.KbBaruCmRujukanRi = (System.Int32?)value;
							break;
						
						case "KbBaruCmRujukanRj":
						
							if (value == null || value is System.Int32)
								this.KbBaruCmRujukanRj = (System.Int32?)value;
							break;
						
						case "KbBaruCmTotal":
						
							if (value == null || value is System.Int32)
								this.KbBaruCmTotal = (System.Int32?)value;
							break;
						
						case "KbBaruDkNifas":
						
							if (value == null || value is System.Int32)
								this.KbBaruDkNifas = (System.Int32?)value;
							break;
						
						case "KbBaruDkAbortus":
						
							if (value == null || value is System.Int32)
								this.KbBaruDkAbortus = (System.Int32?)value;
							break;
						
						case "KbBaruDkLain":
						
							if (value == null || value is System.Int32)
								this.KbBaruDkLain = (System.Int32?)value;
							break;
						
						case "KunjunganUlang":
						
							if (value == null || value is System.Int32)
								this.KunjunganUlang = (System.Int32?)value;
							break;
						
						case "KeluhanEfekSamping":
						
							if (value == null || value is System.Int32)
								this.KeluhanEfekSamping = (System.Int32?)value;
							break;
						
						case "KeluhanEfekSampingDiRujuk":
						
							if (value == null || value is System.Int32)
								this.KeluhanEfekSampingDiRujuk = (System.Int32?)value;
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
		/// Maps to RlTxReport3_12.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport312Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport312Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KonselingAnc
		/// </summary>
		virtual public System.Int32? KonselingAnc
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KonselingAnc);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KonselingAnc, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KonselingPascaPersalinan
		/// </summary>
		virtual public System.Int32? KonselingPascaPersalinan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KonselingPascaPersalinan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KonselingPascaPersalinan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KbBaruCmBukanRujukan
		/// </summary>
		virtual public System.Int32? KbBaruCmBukanRujukan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruCmBukanRujukan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruCmBukanRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KbBaruCmRujukanRi
		/// </summary>
		virtual public System.Int32? KbBaruCmRujukanRi
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruCmRujukanRi);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruCmRujukanRi, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KbBaruCmRujukanRj
		/// </summary>
		virtual public System.Int32? KbBaruCmRujukanRj
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruCmRujukanRj);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruCmRujukanRj, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KbBaruCmTotal
		/// </summary>
		virtual public System.Int32? KbBaruCmTotal
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruCmTotal);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruCmTotal, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KbBaruDkNifas
		/// </summary>
		virtual public System.Int32? KbBaruDkNifas
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruDkNifas);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruDkNifas, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KbBaruDkAbortus
		/// </summary>
		virtual public System.Int32? KbBaruDkAbortus
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruDkAbortus);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruDkAbortus, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KbBaruDkLain
		/// </summary>
		virtual public System.Int32? KbBaruDkLain
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruDkLain);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KbBaruDkLain, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KunjunganUlang
		/// </summary>
		virtual public System.Int32? KunjunganUlang
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KunjunganUlang);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KunjunganUlang, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KeluhanEfekSamping
		/// </summary>
		virtual public System.Int32? KeluhanEfekSamping
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KeluhanEfekSamping);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KeluhanEfekSamping, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.KeluhanEfekSampingDiRujuk
		/// </summary>
		virtual public System.Int32? KeluhanEfekSampingDiRujuk
		{
			get
			{
				return base.GetSystemInt32(RlTxReport312Metadata.ColumnNames.KeluhanEfekSampingDiRujuk);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport312Metadata.ColumnNames.KeluhanEfekSampingDiRujuk, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport312Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport312Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_12.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport312Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport312Metadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRlTxReport312 entity)
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
				
			public System.String KonselingAnc
			{
				get
				{
					System.Int32? data = entity.KonselingAnc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KonselingAnc = null;
					else entity.KonselingAnc = Convert.ToInt32(value);
				}
			}
				
			public System.String KonselingPascaPersalinan
			{
				get
				{
					System.Int32? data = entity.KonselingPascaPersalinan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KonselingPascaPersalinan = null;
					else entity.KonselingPascaPersalinan = Convert.ToInt32(value);
				}
			}
				
			public System.String KbBaruCmBukanRujukan
			{
				get
				{
					System.Int32? data = entity.KbBaruCmBukanRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KbBaruCmBukanRujukan = null;
					else entity.KbBaruCmBukanRujukan = Convert.ToInt32(value);
				}
			}
				
			public System.String KbBaruCmRujukanRi
			{
				get
				{
					System.Int32? data = entity.KbBaruCmRujukanRi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KbBaruCmRujukanRi = null;
					else entity.KbBaruCmRujukanRi = Convert.ToInt32(value);
				}
			}
				
			public System.String KbBaruCmRujukanRj
			{
				get
				{
					System.Int32? data = entity.KbBaruCmRujukanRj;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KbBaruCmRujukanRj = null;
					else entity.KbBaruCmRujukanRj = Convert.ToInt32(value);
				}
			}
				
			public System.String KbBaruCmTotal
			{
				get
				{
					System.Int32? data = entity.KbBaruCmTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KbBaruCmTotal = null;
					else entity.KbBaruCmTotal = Convert.ToInt32(value);
				}
			}
				
			public System.String KbBaruDkNifas
			{
				get
				{
					System.Int32? data = entity.KbBaruDkNifas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KbBaruDkNifas = null;
					else entity.KbBaruDkNifas = Convert.ToInt32(value);
				}
			}
				
			public System.String KbBaruDkAbortus
			{
				get
				{
					System.Int32? data = entity.KbBaruDkAbortus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KbBaruDkAbortus = null;
					else entity.KbBaruDkAbortus = Convert.ToInt32(value);
				}
			}
				
			public System.String KbBaruDkLain
			{
				get
				{
					System.Int32? data = entity.KbBaruDkLain;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KbBaruDkLain = null;
					else entity.KbBaruDkLain = Convert.ToInt32(value);
				}
			}
				
			public System.String KunjunganUlang
			{
				get
				{
					System.Int32? data = entity.KunjunganUlang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KunjunganUlang = null;
					else entity.KunjunganUlang = Convert.ToInt32(value);
				}
			}
				
			public System.String KeluhanEfekSamping
			{
				get
				{
					System.Int32? data = entity.KeluhanEfekSamping;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeluhanEfekSamping = null;
					else entity.KeluhanEfekSamping = Convert.ToInt32(value);
				}
			}
				
			public System.String KeluhanEfekSampingDiRujuk
			{
				get
				{
					System.Int32? data = entity.KeluhanEfekSampingDiRujuk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KeluhanEfekSampingDiRujuk = null;
					else entity.KeluhanEfekSampingDiRujuk = Convert.ToInt32(value);
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
			

			private esRlTxReport312 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport312Query query)
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
				throw new Exception("esRlTxReport312 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport312 : esRlTxReport312
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
	abstract public class esRlTxReport312Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport312Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KonselingAnc
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KonselingAnc, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KonselingPascaPersalinan
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KonselingPascaPersalinan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KbBaruCmBukanRujukan
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KbBaruCmBukanRujukan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KbBaruCmRujukanRi
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KbBaruCmRujukanRi, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KbBaruCmRujukanRj
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KbBaruCmRujukanRj, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KbBaruCmTotal
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KbBaruCmTotal, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KbBaruDkNifas
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KbBaruDkNifas, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KbBaruDkAbortus
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KbBaruDkAbortus, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KbBaruDkLain
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KbBaruDkLain, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KunjunganUlang
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KunjunganUlang, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KeluhanEfekSamping
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KeluhanEfekSamping, esSystemType.Int32);
			}
		} 
		
		public esQueryItem KeluhanEfekSampingDiRujuk
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.KeluhanEfekSampingDiRujuk, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport312Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport312Collection")]
	public partial class RlTxReport312Collection : esRlTxReport312Collection, IEnumerable<RlTxReport312>
	{
		public RlTxReport312Collection()
		{

		}
		
		public static implicit operator List<RlTxReport312>(RlTxReport312Collection coll)
		{
			List<RlTxReport312> list = new List<RlTxReport312>();
			
			foreach (RlTxReport312 emp in coll)
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
				return  RlTxReport312Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport312Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport312(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport312();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport312Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport312Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport312Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport312 AddNew()
		{
			RlTxReport312 entity = base.AddNewEntity() as RlTxReport312;
			
			return entity;
		}

		public RlTxReport312 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport312;
		}


		#region IEnumerable<RlTxReport312> Members

		IEnumerator<RlTxReport312> IEnumerable<RlTxReport312>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport312;
			}
		}

		#endregion
		
		private RlTxReport312Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_12' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport312 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport312 : esRlTxReport312
	{
		public RlTxReport312()
		{

		}
	
		public RlTxReport312(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport312Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport312Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport312Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport312Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport312Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport312Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport312Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport312Query : esRlTxReport312Query
	{
		public RlTxReport312Query()
		{

		}		
		
		public RlTxReport312Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport312Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport312Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport312Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KonselingAnc, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KonselingAnc;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KonselingPascaPersalinan, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KonselingPascaPersalinan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KbBaruCmBukanRujukan, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KbBaruCmBukanRujukan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KbBaruCmRujukanRi, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KbBaruCmRujukanRi;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KbBaruCmRujukanRj, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KbBaruCmRujukanRj;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KbBaruCmTotal, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KbBaruCmTotal;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KbBaruDkNifas, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KbBaruDkNifas;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KbBaruDkAbortus, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KbBaruDkAbortus;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KbBaruDkLain, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KbBaruDkLain;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KunjunganUlang, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KunjunganUlang;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KeluhanEfekSamping, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KeluhanEfekSamping;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.KeluhanEfekSampingDiRujuk, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.KeluhanEfekSampingDiRujuk;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport312Metadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport312Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RlTxReport312Metadata Meta()
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
			 public const string KonselingAnc = "KonselingAnc";
			 public const string KonselingPascaPersalinan = "KonselingPascaPersalinan";
			 public const string KbBaruCmBukanRujukan = "KbBaruCmBukanRujukan";
			 public const string KbBaruCmRujukanRi = "KbBaruCmRujukanRi";
			 public const string KbBaruCmRujukanRj = "KbBaruCmRujukanRj";
			 public const string KbBaruCmTotal = "KbBaruCmTotal";
			 public const string KbBaruDkNifas = "KbBaruDkNifas";
			 public const string KbBaruDkAbortus = "KbBaruDkAbortus";
			 public const string KbBaruDkLain = "KbBaruDkLain";
			 public const string KunjunganUlang = "KunjunganUlang";
			 public const string KeluhanEfekSamping = "KeluhanEfekSamping";
			 public const string KeluhanEfekSampingDiRujuk = "KeluhanEfekSampingDiRujuk";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RlTxReportNo = "RlTxReportNo";
			 public const string RlMasterReportItemID = "RlMasterReportItemID";
			 public const string KonselingAnc = "KonselingAnc";
			 public const string KonselingPascaPersalinan = "KonselingPascaPersalinan";
			 public const string KbBaruCmBukanRujukan = "KbBaruCmBukanRujukan";
			 public const string KbBaruCmRujukanRi = "KbBaruCmRujukanRi";
			 public const string KbBaruCmRujukanRj = "KbBaruCmRujukanRj";
			 public const string KbBaruCmTotal = "KbBaruCmTotal";
			 public const string KbBaruDkNifas = "KbBaruDkNifas";
			 public const string KbBaruDkAbortus = "KbBaruDkAbortus";
			 public const string KbBaruDkLain = "KbBaruDkLain";
			 public const string KunjunganUlang = "KunjunganUlang";
			 public const string KeluhanEfekSamping = "KeluhanEfekSamping";
			 public const string KeluhanEfekSampingDiRujuk = "KeluhanEfekSampingDiRujuk";
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
			lock (typeof(RlTxReport312Metadata))
			{
				if(RlTxReport312Metadata.mapDelegates == null)
				{
					RlTxReport312Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport312Metadata.meta == null)
				{
					RlTxReport312Metadata.meta = new RlTxReport312Metadata();
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
				meta.AddTypeMap("KonselingAnc", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KonselingPascaPersalinan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KbBaruCmBukanRujukan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KbBaruCmRujukanRi", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KbBaruCmRujukanRj", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KbBaruCmTotal", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KbBaruDkNifas", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KbBaruDkAbortus", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KbBaruDkLain", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KunjunganUlang", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KeluhanEfekSamping", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("KeluhanEfekSampingDiRujuk", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RlTxReport3_12";
				meta.Destination = "RlTxReport3_12";
				
				meta.spInsert = "proc_RlTxReport3_12Insert";				
				meta.spUpdate = "proc_RlTxReport3_12Update";		
				meta.spDelete = "proc_RlTxReport3_12Delete";
				meta.spLoadAll = "proc_RlTxReport3_12LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_12LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport312Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
