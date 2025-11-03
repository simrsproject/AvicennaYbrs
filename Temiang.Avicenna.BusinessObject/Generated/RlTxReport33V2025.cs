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
	abstract public class esRlTxReport33V2025Collection : esEntityCollectionWAuditLog
	{
		public esRlTxReport33V2025Collection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RlTxReport33V2025Collection";
		}

		#region Query Logic
		protected void InitQuery(esRlTxReport33V2025Query query)
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
			this.InitQuery(query as esRlTxReport33V2025Query);
		}
		#endregion
		
		virtual public RlTxReport33V2025 DetachEntity(RlTxReport33V2025 entity)
		{
			return base.DetachEntity(entity) as RlTxReport33V2025;
		}
		
		virtual public RlTxReport33V2025 AttachEntity(RlTxReport33V2025 entity)
		{
			return base.AttachEntity(entity) as RlTxReport33V2025;
		}
		
		virtual public void Combine(RlTxReport33V2025Collection collection)
		{
			base.Combine(collection);
		}
		
		new public RlTxReport33V2025 this[int index]
		{
			get
			{
				return base[index] as RlTxReport33V2025;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RlTxReport33V2025);
		}
	}



	[Serializable]
	abstract public class esRlTxReport33V2025 : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRlTxReport33V2025Query GetDynamicQuery()
		{
			return null;
		}

		public esRlTxReport33V2025()
		{

		}

		public esRlTxReport33V2025(DataRow row)
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
			esRlTxReport33V2025Query query = this.GetDynamicQuery();
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
						case "MatiDiUgdLaki": this.str.MatiDiUgdLaki = (string)value; break;							
						case "DoaLaki": this.str.DoaLaki = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "MatiDiUgdPerempuan": this.str.MatiDiUgdPerempuan = (string)value; break;
						case "DoaPerempuan": this.str.DoaPerempuan = (string)value; break;
						case "LukaLaki": this.str.LukaLaki = (string)value; break;
						case "LukaPerempuan": this.str.LukaPerempuan = (string)value; break;
						case "FalseEmergency": this.str.FalseEmergency = (string)value; break;
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
						
						case "MatiDiUgdLaki":
						
							if (value == null || value is System.Int32)
								this.MatiDiUgdLaki = (System.Int32?)value;
							break;
						
						case "DoaLaki":
						
							if (value == null || value is System.Int32)
								this.DoaLaki = (System.Int32?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;

						case "MatiDiUgdPerempuan":

							if (value == null || value is System.Int32)
								this.MatiDiUgdPerempuan = (System.Int32?)value;
							break;

						case "DoaPerempuan":

							if (value == null || value is System.Int32)
								this.DoaPerempuan = (System.Int32?)value;
							break;

						case "LukaLaki":

							if (value == null || value is System.Int32)
								this.LukaLaki = (System.Int32?)value;
							break;

						case "LukaPerempuan":

							if (value == null || value is System.Int32)
								this.LukaPerempuan = (System.Int32?)value;
							break;

						case "FalseEmergency":

							if (value == null || value is System.Int32)
								this.FalseEmergency = (System.Int32?)value;
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
		/// Maps to RlTxReport3_3V2025.RlTxReportNo
		/// </summary>
		virtual public System.String RlTxReportNo
		{
			get
			{
				return base.GetSystemString(RlTxReport33V2025Metadata.ColumnNames.RlTxReportNo);
			}
			
			set
			{
				base.SetSystemString(RlTxReport33V2025Metadata.ColumnNames.RlTxReportNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.RlMasterReportItemID
		/// </summary>
		virtual public System.Int32? RlMasterReportItemID
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.RlMasterReportItemID);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.RlMasterReportItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.PasienRujukan
		/// </summary>
		virtual public System.Int32? PasienRujukan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.PasienRujukan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.PasienRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.PasienNonRujukan
		/// </summary>
		virtual public System.Int32? PasienNonRujukan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.PasienNonRujukan);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.PasienNonRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.DiRawat
		/// </summary>
		virtual public System.Int32? DiRawat
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.DiRawat);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.DiRawat, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.DiRujuk
		/// </summary>
		virtual public System.Int32? DiRujuk
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.DiRujuk);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.DiRujuk, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.Pulang
		/// </summary>
		virtual public System.Int32? Pulang
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.Pulang);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.Pulang, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.MatiDiUgdLaki
		/// </summary>
		virtual public System.Int32? MatiDiUgdLaki
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.MatiDiUgdLaki);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.MatiDiUgdLaki, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.DoaLaki
		/// </summary>
		virtual public System.Int32? DoaLaki
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.DoaLaki);
			}
			
			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.DoaLaki, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RlTxReport33V2025Metadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RlTxReport33V2025Metadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RlTxReport3_3V2025.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RlTxReport33V2025Metadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RlTxReport33V2025Metadata.ColumnNames.LastUpdateByUserID, value);
			}
		}

		/// <summary>
		/// Maps to RlTxReport3_3V2025.MatiDiUgdPerempuan
		/// </summary>
		virtual public System.Int32? MatiDiUgdPerempuan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.MatiDiUgdPerempuan);
			}

			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.MatiDiUgdPerempuan, value);
			}
		}

		/// <summary>
		/// Maps to RlTxReport3_3V2025.DoaPerempuan
		/// </summary>
		virtual public System.Int32? DoaPerempuan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.DoaPerempuan);
			}

			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.DoaPerempuan, value);
			}
		}

		/// <summary>
		/// Maps to RlTxReport3_3V2025.LukaLaki
		/// </summary>
		virtual public System.Int32? LukaLaki
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.LukaLaki);
			}

			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.LukaLaki, value);
			}
		}

		/// <summary>
		/// Maps to RlTxReport3_3V2025.LukaPerempuan
		/// </summary>
		virtual public System.Int32? LukaPerempuan
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.LukaPerempuan);
			}

			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.LukaPerempuan, value);
			}
		}

		/// <summary>
		/// Maps to RlTxReport3_3V2025.FalseEmergency
		/// </summary>
		virtual public System.Int32? FalseEmergency
		{
			get
			{
				return base.GetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.FalseEmergency);
			}

			set
			{
				base.SetSystemInt32(RlTxReport33V2025Metadata.ColumnNames.FalseEmergency, value);
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
			public esStrings(esRlTxReport33V2025 entity)
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
				
			public System.String MatiDiUgdLaki
			{
				get
				{
					System.Int32? data = entity.MatiDiUgdLaki;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MatiDiUgdLaki = null;
					else entity.MatiDiUgdLaki = Convert.ToInt32(value);
				}
			}
				
			public System.String DoaLaki
			{
				get
				{
					System.Int32? data = entity.DoaLaki;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DoaLaki = null;
					else entity.DoaLaki = Convert.ToInt32(value);
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

			public System.String MatiDiUgdPerempuan
			{
				get
				{
					System.Int32? data = entity.MatiDiUgdPerempuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MatiDiUgdPerempuan = null;
					else entity.MatiDiUgdPerempuan = Convert.ToInt32(value);
				}
			}

			public System.String DoaPerempuan
			{
				get
				{
					System.Int32? data = entity.DoaPerempuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DoaPerempuan = null;
					else entity.DoaPerempuan = Convert.ToInt32(value);
				}
			}

			public System.String LukaLaki
			{
				get
				{
					System.Int32? data = entity.LukaLaki;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LukaLaki = null;
					else entity.LukaLaki = Convert.ToInt32(value);
				}
			}

			public System.String LukaPerempuan
			{
				get
				{
					System.Int32? data = entity.LukaPerempuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LukaPerempuan = null;
					else entity.LukaPerempuan = Convert.ToInt32(value);
				}
			}

			public System.String FalseEmergency
			{
				get
				{
					System.Int32? data = entity.FalseEmergency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FalseEmergency = null;
					else entity.FalseEmergency = Convert.ToInt32(value);
				}
			}

			private esRlTxReport33V2025 entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRlTxReport33V2025Query query)
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
				throw new Exception("esRlTxReport33V2025 can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class RlTxReport33V2025 : esRlTxReport33V2025
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
	abstract public class esRlTxReport33V2025Query : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport33V2025Metadata.Meta();
			}
		}	
		

		public esQueryItem RlTxReportNo
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.RlTxReportNo, esSystemType.String);
			}
		} 
		
		public esQueryItem RlMasterReportItemID
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.RlMasterReportItemID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PasienRujukan
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.PasienRujukan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PasienNonRujukan
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.PasienNonRujukan, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DiRawat
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.DiRawat, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DiRujuk
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.DiRujuk, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Pulang
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.Pulang, esSystemType.Int32);
			}
		} 
		
		public esQueryItem MatiDiUgdLaki
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.MatiDiUgdLaki, esSystemType.Int32);
			}
		} 
		
		public esQueryItem DoaLaki
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.DoaLaki, esSystemType.Int32);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}
		public esQueryItem MatiDiUgdPerempuan
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.MatiDiUgdPerempuan, esSystemType.Int32);
			}
		}

		public esQueryItem DoaPerempuan
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.DoaPerempuan, esSystemType.Int32);
			}
		}

		public esQueryItem LukaLaki
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.LukaLaki, esSystemType.Int32);
			}
		}

		public esQueryItem LukaPerempuan
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.LukaPerempuan, esSystemType.Int32);
			}
		}

		public esQueryItem FalseEmergency
		{
			get
			{
				return new esQueryItem(this, RlTxReport33V2025Metadata.ColumnNames.FalseEmergency, esSystemType.Int32);
			}
		}

	}



	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RlTxReport33V2025Collection")]
	public partial class RlTxReport33V2025Collection : esRlTxReport33V2025Collection, IEnumerable<RlTxReport33V2025>
	{
		public RlTxReport33V2025Collection()
		{

		}
		
		public static implicit operator List<RlTxReport33V2025>(RlTxReport33V2025Collection coll)
		{
			List<RlTxReport33V2025> list = new List<RlTxReport33V2025>();
			
			foreach (RlTxReport33V2025 emp in coll)
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
				return  RlTxReport33V2025Metadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport33V2025Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RlTxReport33V2025(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RlTxReport33V2025();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RlTxReport33V2025Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport33V2025Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RlTxReport33V2025Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RlTxReport33V2025 AddNew()
		{
			RlTxReport33V2025 entity = base.AddNewEntity() as RlTxReport33V2025;
			
			return entity;
		}

		public RlTxReport33V2025 FindByPrimaryKey(System.String rlTxReportNo, System.Int32 rlMasterReportItemID)
		{
			return base.FindByPrimaryKey(rlTxReportNo, rlMasterReportItemID) as RlTxReport33V2025;
		}


		#region IEnumerable<RlTxReport33V2025> Members

		IEnumerator<RlTxReport33V2025> IEnumerable<RlTxReport33V2025>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RlTxReport33V2025;
			}
		}

		#endregion
		
		private RlTxReport33V2025Query query;
	}


	/// <summary>
	/// Encapsulates the 'RlTxReport3_3V2025' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RlTxReport33V2025 ({RlTxReportNo},{RlMasterReportItemID})")]
	[Serializable]
	public partial class RlTxReport33V2025 : esRlTxReport33V2025
	{
		public RlTxReport33V2025()
		{

		}
	
		public RlTxReport33V2025(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RlTxReport33V2025Metadata.Meta();
			}
		}
		
		
		
		override protected esRlTxReport33V2025Query GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RlTxReport33V2025Query();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RlTxReport33V2025Query Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RlTxReport33V2025Query();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RlTxReport33V2025Query query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RlTxReport33V2025Query query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RlTxReport33V2025Query : esRlTxReport33V2025Query
	{
		public RlTxReport33V2025Query()
		{

		}		
		
		public RlTxReport33V2025Query(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RlTxReport33V2025Query";
        }
		
			
	}


	[Serializable]
	public partial class RlTxReport33V2025Metadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RlTxReport33V2025Metadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.RlTxReportNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.RlTxReportNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.RlMasterReportItemID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.RlMasterReportItemID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.PasienRujukan, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.PasienRujukan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.PasienNonRujukan, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.PasienNonRujukan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.DiRawat, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.DiRawat;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.DiRujuk, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.DiRujuk;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.Pulang, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.Pulang;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.MatiDiUgdLaki, 7, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.MatiDiUgdLaki;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.DoaLaki, 8, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.DoaLaki;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.MatiDiUgdPerempuan, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.MatiDiUgdPerempuan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.DoaPerempuan, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.DoaPerempuan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.LukaLaki, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.LukaLaki;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.LukaPerempuan, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.LukaPerempuan;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RlTxReport33V2025Metadata.ColumnNames.FalseEmergency, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RlTxReport33V2025Metadata.PropertyNames.FalseEmergency;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

		}
		#endregion	
	
		static public RlTxReport33V2025Metadata Meta()
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
			 public const string MatiDiUgdLaki = "MatiDiUgdLaki";
			 public const string DoaLaki = "DoaLaki";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string MatiDiUgdPerempuan = "MatiDiUgdPerempuan";
			public const string DoaPerempuan = "DoaPerempuan";
			public const string LukaLaki = "LukaLaki";
			public const string LukaPerempuan = "LukaPerempuan";
			public const string FalseEmergency = "FalseEmergency";
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
			 public const string MatiDiUgdLaki = "MatiDiUgdLaki";
			 public const string DoaLaki = "DoaLaki";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string MatiDiUgdPerempuan = "MatiDiUgdPerempuan";
			 public const string DoaPerempuan = "DoaPerempuan";
			public const string LukaLaki = "LukaLaki";
			public const string LukaPerempuan = "LukaPerempuan";
			public const string FalseEmergency = "FalseEmergency";
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
			lock (typeof(RlTxReport33V2025Metadata))
			{
				if(RlTxReport33V2025Metadata.mapDelegates == null)
				{
					RlTxReport33V2025Metadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RlTxReport33V2025Metadata.meta == null)
				{
					RlTxReport33V2025Metadata.meta = new RlTxReport33V2025Metadata();
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
				meta.AddTypeMap("MatiDiUgdLaki", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DoaLaki", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));	
				meta.AddTypeMap("MatiDiUgdPerempuan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DoaPerempuan", new esTypeMap("int", "System.Int32"));		
				meta.AddTypeMap("LukaLaki", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LukaPerempuan", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("FalseEmergency", new esTypeMap("int", "System.Int32"));



				meta.Source = "RlTxReport3_3V2025";
				meta.Destination = "RlTxReport3_3V2025";
				
				meta.spInsert = "proc_RlTxReport3_3V2025Insert";				
				meta.spUpdate = "proc_RlTxReport3_3V2025Update";		
				meta.spDelete = "proc_RlTxReport3_3V2025Delete";
				meta.spLoadAll = "proc_RlTxReport3_3V2025LoadAll";
				meta.spLoadByPrimaryKey = "proc_RlTxReport3_3V2025LoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RlTxReport33V2025Metadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
