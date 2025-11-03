/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/26/2022 2:05:02 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.SHARELIS
{

	[Serializable]
	abstract public class esHasilLISCollection : esEntityCollectionWAuditLog
	{
		public esHasilLISCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "HasilLISCollection";
		}

		#region Query Logic
		protected void InitQuery(esHasilLISQuery query)
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
			this.InitQuery(query as esHasilLISQuery);
		}
		#endregion
		
		virtual public HasilLIS DetachEntity(HasilLIS entity)
		{
			return base.DetachEntity(entity) as HasilLIS;
		}
		
		virtual public HasilLIS AttachEntity(HasilLIS entity)
		{
			return base.AttachEntity(entity) as HasilLIS;
		}
		
		virtual public void Combine(HasilLISCollection collection)
		{
			base.Combine(collection);
		}
		
		new public HasilLIS this[int index]
		{
			get
			{
				return base[index] as HasilLIS;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(HasilLIS);
		}
	}



	[Serializable]
	abstract public class esHasilLIS : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esHasilLISQuery GetDynamicQuery()
		{
			return null;
		}

		public esHasilLIS()
		{

		}

		public esHasilLIS(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey()
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic();
			else
				return LoadByPrimaryKeyStoredProcedure();
		}

		//public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, )
		//{
		//	if (sqlAccessType == esSqlAccessType.DynamicSQL)
		//		return LoadByPrimaryKeyDynamic();
		//	else
		//		return LoadByPrimaryKeyStoredProcedure();
		//}

		private bool LoadByPrimaryKeyDynamic()
		{
			esHasilLISQuery query = this.GetDynamicQuery();
			query.Where();
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure()
		{
			esParameters parms = new esParameters();

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
						case "Kode": this.str.Kode = (string)value; break;							
						case "NolabLis": this.str.NolabLis = (string)value; break;							
						case "NolabRs": this.str.NolabRs = (string)value; break;							
						case "RegDate": this.str.RegDate = (string)value; break;							
						case "TarifId": this.str.TarifId = (string)value; break;							
						case "TarifName": this.str.TarifName = (string)value; break;							
						case "ParameterId": this.str.ParameterId = (string)value; break;							
						case "ParameterName": this.str.ParameterName = (string)value; break;							
						case "Hasil": this.str.Hasil = (string)value; break;							
						case "Satuan": this.str.Satuan = (string)value; break;							
						case "NilaiRujukan": this.str.NilaiRujukan = (string)value; break;							
						case "Norm": this.str.Norm = (string)value; break;							
						case "UrutBound": this.str.UrutBound = (string)value; break;							
						case "IdHasil": this.str.IdHasil = (string)value; break;							
						case "ModifiedDate": this.str.ModifiedDate = (string)value; break;							
						case "KelPemeriksaan": this.str.KelPemeriksaan = (string)value; break;							
						case "FlagHl": this.str.FlagHl = (string)value; break;							
						case "MetodePeriksa": this.str.MetodePeriksa = (string)value; break;							
						case "Catatan": this.str.Catatan = (string)value; break;							
						case "Rekomendasi": this.str.Rekomendasi = (string)value; break;							
						case "LisId": this.str.LisId = (string)value; break;							
						case "KodeTarif": this.str.KodeTarif = (string)value; break;							
						case "TglVerifikasi": this.str.TglVerifikasi = (string)value; break;							
						case "UserVerifikasi": this.str.UserVerifikasi = (string)value; break;							
						case "TglHasilSelesai": this.str.TglHasilSelesai = (string)value; break;							
						case "UserCetak": this.str.UserCetak = (string)value; break;							
						case "NilaiKritis": this.str.NilaiKritis = (string)value; break;							
						case "DokterPenanggungJawab": this.str.DokterPenanggungJawab = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RegDate":
						
							if (value == null || value is System.DateTime)
								this.RegDate = (System.DateTime?)value;
							break;
						
						case "UrutBound":
						
							if (value == null || value is System.Int32)
								this.UrutBound = (System.Int32?)value;
							break;
						
						case "ModifiedDate":
						
							if (value == null || value is System.DateTime)
								this.ModifiedDate = (System.DateTime?)value;
							break;
						
						case "TglVerifikasi":
						
							if (value == null || value is System.DateTime)
								this.TglVerifikasi = (System.DateTime?)value;
							break;
						
						case "TglHasilSelesai":
						
							if (value == null || value is System.DateTime)
								this.TglHasilSelesai = (System.DateTime?)value;
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
		/// Maps to HasilLIS.kode
		/// </summary>
		virtual public System.String Kode
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.Kode);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.Kode, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.NOLAB_LIS
		/// </summary>
		virtual public System.String NolabLis
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.NolabLis);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.NolabLis, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.NOLAB_RS
		/// </summary>
		virtual public System.String NolabRs
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.NolabRs);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.NolabRs, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.REG_DATE
		/// </summary>
		virtual public System.DateTime? RegDate
		{
			get
			{
				return base.GetSystemDateTime(HasilLISMetadata.ColumnNames.RegDate);
			}
			
			set
			{
				base.SetSystemDateTime(HasilLISMetadata.ColumnNames.RegDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.TARIF_ID
		/// </summary>
		virtual public System.String TarifId
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.TarifId);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.TarifId, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.TARIF_NAME
		/// </summary>
		virtual public System.String TarifName
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.TarifName);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.TarifName, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.PARAMETER_ID
		/// </summary>
		virtual public System.String ParameterId
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.ParameterId);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.ParameterId, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.PARAMETER_NAME
		/// </summary>
		virtual public System.String ParameterName
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.ParameterName);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.ParameterName, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.HASIL
		/// </summary>
		virtual public System.String Hasil
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.Hasil);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.Hasil, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.SATUAN
		/// </summary>
		virtual public System.String Satuan
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.Satuan);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.Satuan, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.NILAI_RUJUKAN
		/// </summary>
		virtual public System.String NilaiRujukan
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.NilaiRujukan);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.NilaiRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.NORM
		/// </summary>
		virtual public System.String Norm
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.Norm);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.Norm, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.URUT_BOUND
		/// </summary>
		virtual public System.Int32? UrutBound
		{
			get
			{
				return base.GetSystemInt32(HasilLISMetadata.ColumnNames.UrutBound);
			}
			
			set
			{
				base.SetSystemInt32(HasilLISMetadata.ColumnNames.UrutBound, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.ID_HASIL
		/// </summary>
		virtual public System.String IdHasil
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.IdHasil);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.IdHasil, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.MODIFIED_DATE
		/// </summary>
		virtual public System.DateTime? ModifiedDate
		{
			get
			{
				return base.GetSystemDateTime(HasilLISMetadata.ColumnNames.ModifiedDate);
			}
			
			set
			{
				base.SetSystemDateTime(HasilLISMetadata.ColumnNames.ModifiedDate, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.KEL_PEMERIKSAAN
		/// </summary>
		virtual public System.String KelPemeriksaan
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.KelPemeriksaan);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.KelPemeriksaan, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.FLAG_HL
		/// </summary>
		virtual public System.String FlagHl
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.FlagHl);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.FlagHl, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.METODE_PERIKSA
		/// </summary>
		virtual public System.String MetodePeriksa
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.MetodePeriksa);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.MetodePeriksa, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.Catatan
		/// </summary>
		virtual public System.String Catatan
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.Catatan);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.Catatan, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.Rekomendasi
		/// </summary>
		virtual public System.String Rekomendasi
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.Rekomendasi);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.Rekomendasi, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.LIS_ID
		/// </summary>
		virtual public System.String LisId
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.LisId);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.LisId, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.KODE_TARIF
		/// </summary>
		virtual public System.String KodeTarif
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.KodeTarif);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.KodeTarif, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.TGL_VERIFIKASI
		/// </summary>
		virtual public System.DateTime? TglVerifikasi
		{
			get
			{
				return base.GetSystemDateTime(HasilLISMetadata.ColumnNames.TglVerifikasi);
			}
			
			set
			{
				base.SetSystemDateTime(HasilLISMetadata.ColumnNames.TglVerifikasi, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.USER_VERIFIKASI
		/// </summary>
		virtual public System.String UserVerifikasi
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.UserVerifikasi);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.UserVerifikasi, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.TGL_HASIL_SELESAI
		/// </summary>
		virtual public System.DateTime? TglHasilSelesai
		{
			get
			{
				return base.GetSystemDateTime(HasilLISMetadata.ColumnNames.TglHasilSelesai);
			}
			
			set
			{
				base.SetSystemDateTime(HasilLISMetadata.ColumnNames.TglHasilSelesai, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.USER_CETAK
		/// </summary>
		virtual public System.String UserCetak
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.UserCetak);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.UserCetak, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.NILAI_KRITIS
		/// </summary>
		virtual public System.String NilaiKritis
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.NilaiKritis);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.NilaiKritis, value);
			}
		}
		
		/// <summary>
		/// Maps to HasilLIS.DOKTER_PENANGGUNG_JAWAB
		/// </summary>
		virtual public System.String DokterPenanggungJawab
		{
			get
			{
				return base.GetSystemString(HasilLISMetadata.ColumnNames.DokterPenanggungJawab);
			}
			
			set
			{
				base.SetSystemString(HasilLISMetadata.ColumnNames.DokterPenanggungJawab, value);
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
			public esStrings(esHasilLIS entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Kode
			{
				get
				{
					System.String data = entity.Kode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kode = null;
					else entity.Kode = Convert.ToString(value);
				}
			}
				
			public System.String NolabLis
			{
				get
				{
					System.String data = entity.NolabLis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NolabLis = null;
					else entity.NolabLis = Convert.ToString(value);
				}
			}
				
			public System.String NolabRs
			{
				get
				{
					System.String data = entity.NolabRs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NolabRs = null;
					else entity.NolabRs = Convert.ToString(value);
				}
			}
				
			public System.String RegDate
			{
				get
				{
					System.DateTime? data = entity.RegDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegDate = null;
					else entity.RegDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String TarifId
			{
				get
				{
					System.String data = entity.TarifId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TarifId = null;
					else entity.TarifId = Convert.ToString(value);
				}
			}
				
			public System.String TarifName
			{
				get
				{
					System.String data = entity.TarifName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TarifName = null;
					else entity.TarifName = Convert.ToString(value);
				}
			}
				
			public System.String ParameterId
			{
				get
				{
					System.String data = entity.ParameterId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParameterId = null;
					else entity.ParameterId = Convert.ToString(value);
				}
			}
				
			public System.String ParameterName
			{
				get
				{
					System.String data = entity.ParameterName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParameterName = null;
					else entity.ParameterName = Convert.ToString(value);
				}
			}
				
			public System.String Hasil
			{
				get
				{
					System.String data = entity.Hasil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hasil = null;
					else entity.Hasil = Convert.ToString(value);
				}
			}
				
			public System.String Satuan
			{
				get
				{
					System.String data = entity.Satuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Satuan = null;
					else entity.Satuan = Convert.ToString(value);
				}
			}
				
			public System.String NilaiRujukan
			{
				get
				{
					System.String data = entity.NilaiRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NilaiRujukan = null;
					else entity.NilaiRujukan = Convert.ToString(value);
				}
			}
				
			public System.String Norm
			{
				get
				{
					System.String data = entity.Norm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Norm = null;
					else entity.Norm = Convert.ToString(value);
				}
			}
				
			public System.String UrutBound
			{
				get
				{
					System.Int32? data = entity.UrutBound;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UrutBound = null;
					else entity.UrutBound = Convert.ToInt32(value);
				}
			}
				
			public System.String IdHasil
			{
				get
				{
					System.String data = entity.IdHasil;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdHasil = null;
					else entity.IdHasil = Convert.ToString(value);
				}
			}
				
			public System.String ModifiedDate
			{
				get
				{
					System.DateTime? data = entity.ModifiedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ModifiedDate = null;
					else entity.ModifiedDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String KelPemeriksaan
			{
				get
				{
					System.String data = entity.KelPemeriksaan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KelPemeriksaan = null;
					else entity.KelPemeriksaan = Convert.ToString(value);
				}
			}
				
			public System.String FlagHl
			{
				get
				{
					System.String data = entity.FlagHl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FlagHl = null;
					else entity.FlagHl = Convert.ToString(value);
				}
			}
				
			public System.String MetodePeriksa
			{
				get
				{
					System.String data = entity.MetodePeriksa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MetodePeriksa = null;
					else entity.MetodePeriksa = Convert.ToString(value);
				}
			}
				
			public System.String Catatan
			{
				get
				{
					System.String data = entity.Catatan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Catatan = null;
					else entity.Catatan = Convert.ToString(value);
				}
			}
				
			public System.String Rekomendasi
			{
				get
				{
					System.String data = entity.Rekomendasi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Rekomendasi = null;
					else entity.Rekomendasi = Convert.ToString(value);
				}
			}
				
			public System.String LisId
			{
				get
				{
					System.String data = entity.LisId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LisId = null;
					else entity.LisId = Convert.ToString(value);
				}
			}
				
			public System.String KodeTarif
			{
				get
				{
					System.String data = entity.KodeTarif;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeTarif = null;
					else entity.KodeTarif = Convert.ToString(value);
				}
			}
				
			public System.String TglVerifikasi
			{
				get
				{
					System.DateTime? data = entity.TglVerifikasi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglVerifikasi = null;
					else entity.TglVerifikasi = Convert.ToDateTime(value);
				}
			}
				
			public System.String UserVerifikasi
			{
				get
				{
					System.String data = entity.UserVerifikasi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserVerifikasi = null;
					else entity.UserVerifikasi = Convert.ToString(value);
				}
			}
				
			public System.String TglHasilSelesai
			{
				get
				{
					System.DateTime? data = entity.TglHasilSelesai;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglHasilSelesai = null;
					else entity.TglHasilSelesai = Convert.ToDateTime(value);
				}
			}
				
			public System.String UserCetak
			{
				get
				{
					System.String data = entity.UserCetak;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserCetak = null;
					else entity.UserCetak = Convert.ToString(value);
				}
			}
				
			public System.String NilaiKritis
			{
				get
				{
					System.String data = entity.NilaiKritis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NilaiKritis = null;
					else entity.NilaiKritis = Convert.ToString(value);
				}
			}
				
			public System.String DokterPenanggungJawab
			{
				get
				{
					System.String data = entity.DokterPenanggungJawab;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DokterPenanggungJawab = null;
					else entity.DokterPenanggungJawab = Convert.ToString(value);
				}
			}
			

			private esHasilLIS entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esHasilLISQuery query)
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
				throw new Exception("esHasilLIS can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esHasilLISQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return HasilLISMetadata.Meta();
			}
		}	
		

		public esQueryItem Kode
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.Kode, esSystemType.String);
			}
		} 
		
		public esQueryItem NolabLis
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.NolabLis, esSystemType.String);
			}
		} 
		
		public esQueryItem NolabRs
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.NolabRs, esSystemType.String);
			}
		} 
		
		public esQueryItem RegDate
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.RegDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TarifId
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.TarifId, esSystemType.String);
			}
		} 
		
		public esQueryItem TarifName
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.TarifName, esSystemType.String);
			}
		} 
		
		public esQueryItem ParameterId
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.ParameterId, esSystemType.String);
			}
		} 
		
		public esQueryItem ParameterName
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.ParameterName, esSystemType.String);
			}
		} 
		
		public esQueryItem Hasil
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.Hasil, esSystemType.String);
			}
		} 
		
		public esQueryItem Satuan
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.Satuan, esSystemType.String);
			}
		} 
		
		public esQueryItem NilaiRujukan
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.NilaiRujukan, esSystemType.String);
			}
		} 
		
		public esQueryItem Norm
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.Norm, esSystemType.String);
			}
		} 
		
		public esQueryItem UrutBound
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.UrutBound, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IdHasil
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.IdHasil, esSystemType.String);
			}
		} 
		
		public esQueryItem ModifiedDate
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.ModifiedDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem KelPemeriksaan
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.KelPemeriksaan, esSystemType.String);
			}
		} 
		
		public esQueryItem FlagHl
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.FlagHl, esSystemType.String);
			}
		} 
		
		public esQueryItem MetodePeriksa
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.MetodePeriksa, esSystemType.String);
			}
		} 
		
		public esQueryItem Catatan
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.Catatan, esSystemType.String);
			}
		} 
		
		public esQueryItem Rekomendasi
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.Rekomendasi, esSystemType.String);
			}
		} 
		
		public esQueryItem LisId
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.LisId, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeTarif
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.KodeTarif, esSystemType.String);
			}
		} 
		
		public esQueryItem TglVerifikasi
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.TglVerifikasi, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem UserVerifikasi
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.UserVerifikasi, esSystemType.String);
			}
		} 
		
		public esQueryItem TglHasilSelesai
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.TglHasilSelesai, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem UserCetak
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.UserCetak, esSystemType.String);
			}
		} 
		
		public esQueryItem NilaiKritis
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.NilaiKritis, esSystemType.String);
			}
		} 
		
		public esQueryItem DokterPenanggungJawab
		{
			get
			{
				return new esQueryItem(this, HasilLISMetadata.ColumnNames.DokterPenanggungJawab, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("HasilLISCollection")]
	public partial class HasilLISCollection : esHasilLISCollection, IEnumerable<HasilLIS>
	{
		public HasilLISCollection()
		{

		}
		
		public static implicit operator List<HasilLIS>(HasilLISCollection coll)
		{
			List<HasilLIS> list = new List<HasilLIS>();
			
			foreach (HasilLIS emp in coll)
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
				return  HasilLISMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HasilLISQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new HasilLIS(row);
		}

		override protected esEntity CreateEntity()
		{
			return new HasilLIS();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public HasilLISQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HasilLISQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(HasilLISQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public HasilLIS AddNew()
		{
			HasilLIS entity = base.AddNewEntity() as HasilLIS;
			
			return entity;
		}

		public HasilLIS FindByPrimaryKey()
		{
			return base.FindByPrimaryKey() as HasilLIS;
		}


		#region IEnumerable<HasilLIS> Members

		IEnumerator<HasilLIS> IEnumerable<HasilLIS>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as HasilLIS;
			}
		}

		#endregion
		
		private HasilLISQuery query;
	}


	/// <summary>
	/// Encapsulates the 'HasilLIS' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("HasilLIS ()")]
	[Serializable]
	public partial class HasilLIS : esHasilLIS
	{
		public HasilLIS()
		{

		}
	
		public HasilLIS(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return HasilLISMetadata.Meta();
			}
		}
		
		
		
		override protected esHasilLISQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new HasilLISQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public HasilLISQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new HasilLISQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(HasilLISQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private HasilLISQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class HasilLISQuery : esHasilLISQuery
	{
		public HasilLISQuery()
		{

		}		
		
		public HasilLISQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "HasilLISQuery";
        }
		
			
	}


	[Serializable]
	public partial class HasilLISMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected HasilLISMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.Kode, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.Kode;
			c.CharacterMaxLength = 32;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.NolabLis, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.NolabLis;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.NolabRs, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.NolabRs;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.RegDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HasilLISMetadata.PropertyNames.RegDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.TarifId, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.TarifId;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.TarifName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.TarifName;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.ParameterId, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.ParameterId;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.ParameterName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.ParameterName;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.Hasil, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.Hasil;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.Satuan, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.Satuan;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.NilaiRujukan, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.NilaiRujukan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.Norm, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.Norm;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.UrutBound, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = HasilLISMetadata.PropertyNames.UrutBound;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.IdHasil, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.IdHasil;
			c.CharacterMaxLength = 35;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.ModifiedDate, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HasilLISMetadata.PropertyNames.ModifiedDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.KelPemeriksaan, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.KelPemeriksaan;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.FlagHl, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.FlagHl;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.MetodePeriksa, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.MetodePeriksa;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.Catatan, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.Catatan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.Rekomendasi, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.Rekomendasi;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.LisId, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.LisId;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.KodeTarif, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.KodeTarif;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.TglVerifikasi, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HasilLISMetadata.PropertyNames.TglVerifikasi;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.UserVerifikasi, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.UserVerifikasi;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.TglHasilSelesai, 24, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = HasilLISMetadata.PropertyNames.TglHasilSelesai;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.UserCetak, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.UserCetak;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.NilaiKritis, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.NilaiKritis;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(HasilLISMetadata.ColumnNames.DokterPenanggungJawab, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = HasilLISMetadata.PropertyNames.DokterPenanggungJawab;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public HasilLISMetadata Meta()
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
			 public const string Kode = "kode";
			 public const string NolabLis = "NOLAB_LIS";
			 public const string NolabRs = "NOLAB_RS";
			 public const string RegDate = "REG_DATE";
			 public const string TarifId = "TARIF_ID";
			 public const string TarifName = "TARIF_NAME";
			 public const string ParameterId = "PARAMETER_ID";
			 public const string ParameterName = "PARAMETER_NAME";
			 public const string Hasil = "HASIL";
			 public const string Satuan = "SATUAN";
			 public const string NilaiRujukan = "NILAI_RUJUKAN";
			 public const string Norm = "NORM";
			 public const string UrutBound = "URUT_BOUND";
			 public const string IdHasil = "ID_HASIL";
			 public const string ModifiedDate = "MODIFIED_DATE";
			 public const string KelPemeriksaan = "KEL_PEMERIKSAAN";
			 public const string FlagHl = "FLAG_HL";
			 public const string MetodePeriksa = "METODE_PERIKSA";
			 public const string Catatan = "Catatan";
			 public const string Rekomendasi = "Rekomendasi";
			 public const string LisId = "LIS_ID";
			 public const string KodeTarif = "KODE_TARIF";
			 public const string TglVerifikasi = "TGL_VERIFIKASI";
			 public const string UserVerifikasi = "USER_VERIFIKASI";
			 public const string TglHasilSelesai = "TGL_HASIL_SELESAI";
			 public const string UserCetak = "USER_CETAK";
			 public const string NilaiKritis = "NILAI_KRITIS";
			 public const string DokterPenanggungJawab = "DOKTER_PENANGGUNG_JAWAB";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Kode = "Kode";
			 public const string NolabLis = "NolabLis";
			 public const string NolabRs = "NolabRs";
			 public const string RegDate = "RegDate";
			 public const string TarifId = "TarifId";
			 public const string TarifName = "TarifName";
			 public const string ParameterId = "ParameterId";
			 public const string ParameterName = "ParameterName";
			 public const string Hasil = "Hasil";
			 public const string Satuan = "Satuan";
			 public const string NilaiRujukan = "NilaiRujukan";
			 public const string Norm = "Norm";
			 public const string UrutBound = "UrutBound";
			 public const string IdHasil = "IdHasil";
			 public const string ModifiedDate = "ModifiedDate";
			 public const string KelPemeriksaan = "KelPemeriksaan";
			 public const string FlagHl = "FlagHl";
			 public const string MetodePeriksa = "MetodePeriksa";
			 public const string Catatan = "Catatan";
			 public const string Rekomendasi = "Rekomendasi";
			 public const string LisId = "LisId";
			 public const string KodeTarif = "KodeTarif";
			 public const string TglVerifikasi = "TglVerifikasi";
			 public const string UserVerifikasi = "UserVerifikasi";
			 public const string TglHasilSelesai = "TglHasilSelesai";
			 public const string UserCetak = "UserCetak";
			 public const string NilaiKritis = "NilaiKritis";
			 public const string DokterPenanggungJawab = "DokterPenanggungJawab";
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
			lock (typeof(HasilLISMetadata))
			{
				if(HasilLISMetadata.mapDelegates == null)
				{
					HasilLISMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (HasilLISMetadata.meta == null)
				{
					HasilLISMetadata.meta = new HasilLISMetadata();
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
				

				meta.AddTypeMap("Kode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NolabLis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NolabRs", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TarifId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TarifName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParameterId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParameterName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Hasil", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Satuan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NilaiRujukan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Norm", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UrutBound", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IdHasil", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ModifiedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("KelPemeriksaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FlagHl", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("MetodePeriksa", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Catatan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Rekomendasi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LisId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeTarif", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglVerifikasi", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("UserVerifikasi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TglHasilSelesai", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("UserCetak", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NilaiKritis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DokterPenanggungJawab", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "HasilLIS";
				meta.Destination = "HasilLIS";
				
				meta.spInsert = "proc_HasilLISInsert";				
				meta.spUpdate = "proc_HasilLISUpdate";		
				meta.spDelete = "proc_HasilLISDelete";
				meta.spLoadAll = "proc_HasilLISLoadAll";
				meta.spLoadByPrimaryKey = "proc_HasilLISLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private HasilLISMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
