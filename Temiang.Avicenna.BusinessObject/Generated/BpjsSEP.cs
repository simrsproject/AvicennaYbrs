/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 6/13/2022 7:51:44 AM
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
	abstract public class esBpjsSEPCollection : esEntityCollectionWAuditLog
	{
		public esBpjsSEPCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "BpjsSEPCollection";
		}

		#region Query Logic
		protected void InitQuery(esBpjsSEPQuery query)
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
			this.InitQuery(query as esBpjsSEPQuery);
		}
		#endregion
		
		virtual public BpjsSEP DetachEntity(BpjsSEP entity)
		{
			return base.DetachEntity(entity) as BpjsSEP;
		}
		
		virtual public BpjsSEP AttachEntity(BpjsSEP entity)
		{
			return base.AttachEntity(entity) as BpjsSEP;
		}
		
		virtual public void Combine(BpjsSEPCollection collection)
		{
			base.Combine(collection);
		}
		
		new public BpjsSEP this[int index]
		{
			get
			{
				return base[index] as BpjsSEP;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BpjsSEP);
		}
	}



	[Serializable]
	abstract public class esBpjsSEP : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBpjsSEPQuery GetDynamicQuery()
		{
			return null;
		}

		public esBpjsSEP()
		{

		}

		public esBpjsSEP(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int64 sepID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sepID);
			else
				return LoadByPrimaryKeyStoredProcedure(sepID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int64 sepID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sepID);
			else
				return LoadByPrimaryKeyStoredProcedure(sepID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int64 sepID)
		{
			esBpjsSEPQuery query = this.GetDynamicQuery();
			query.Where(query.SepID == sepID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int64 sepID)
		{
			esParameters parms = new esParameters();
			parms.Add("SepID",sepID);
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
						case "SepID": this.str.SepID = (string)value; break;							
						case "NoSEP": this.str.NoSEP = (string)value; break;							
						case "NomorKartu": this.str.NomorKartu = (string)value; break;							
						case "TanggalSEP": this.str.TanggalSEP = (string)value; break;							
						case "TanggalRujukan": this.str.TanggalRujukan = (string)value; break;							
						case "NoRujukan": this.str.NoRujukan = (string)value; break;							
						case "PPKRujukan": this.str.PPKRujukan = (string)value; break;							
						case "NamaPPKRujukan": this.str.NamaPPKRujukan = (string)value; break;							
						case "PPKPelayanan": this.str.PPKPelayanan = (string)value; break;							
						case "JenisPelayanan": this.str.JenisPelayanan = (string)value; break;							
						case "Catatan": this.str.Catatan = (string)value; break;							
						case "DiagnosaAwal": this.str.DiagnosaAwal = (string)value; break;							
						case "PoliTujuan": this.str.PoliTujuan = (string)value; break;							
						case "KelasRawat": this.str.KelasRawat = (string)value; break;							
						case "LakaLantas": this.str.LakaLantas = (string)value; break;							
						case "User": this.str.User = (string)value; break;							
						case "NoMR": this.str.NoMR = (string)value; break;							
						case "TanggalPulang": this.str.TanggalPulang = (string)value; break;							
						case "NoTransaksi": this.str.NoTransaksi = (string)value; break;							
						case "NamaPasien": this.str.NamaPasien = (string)value; break;							
						case "Nik": this.str.Nik = (string)value; break;							
						case "JenisKelamin": this.str.JenisKelamin = (string)value; break;							
						case "TanggalLahir": this.str.TanggalLahir = (string)value; break;							
						case "JenisPeserta": this.str.JenisPeserta = (string)value; break;							
						case "DetailKeanggotaan": this.str.DetailKeanggotaan = (string)value; break;							
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "KodeCBG": this.str.KodeCBG = (string)value; break;							
						case "TariffCBG": this.str.TariffCBG = (string)value; break;							
						case "DeskripsiCBG": this.str.DeskripsiCBG = (string)value; break;							
						case "LokasiLaka": this.str.LokasiLaka = (string)value; break;							
						case "NamaKelasRawat": this.str.NamaKelasRawat = (string)value; break;							
						case "IsEksekutif": this.str.IsEksekutif = (string)value; break;							
						case "IsCob": this.str.IsCob = (string)value; break;							
						case "PenjaminLaka": this.str.PenjaminLaka = (string)value; break;							
						case "NamaCob": this.str.NamaCob = (string)value; break;							
						case "StatusPeserta": this.str.StatusPeserta = (string)value; break;							
						case "Umur": this.str.Umur = (string)value; break;							
						case "JenisRujukan": this.str.JenisRujukan = (string)value; break;							
						case "IsKatarak": this.str.IsKatarak = (string)value; break;							
						case "TglKejadian": this.str.TglKejadian = (string)value; break;							
						case "IsSuplesi": this.str.IsSuplesi = (string)value; break;							
						case "NoSepSuplesi": this.str.NoSepSuplesi = (string)value; break;							
						case "KodePropinsi": this.str.KodePropinsi = (string)value; break;							
						case "KodeKabupaten": this.str.KodeKabupaten = (string)value; break;							
						case "KodeKecamatan": this.str.KodeKecamatan = (string)value; break;							
						case "NoSkdp": this.str.NoSkdp = (string)value; break;							
						case "KodeDpjp": this.str.KodeDpjp = (string)value; break;							
						case "FromRegistrationNo": this.str.FromRegistrationNo = (string)value; break;							
						case "ProlanisPRB": this.str.ProlanisPRB = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "KodeDpjpPelayanan": this.str.KodeDpjpPelayanan = (string)value; break;							
						case "KlsRawatNaik": this.str.KlsRawatNaik = (string)value; break;							
						case "Pembiayaan": this.str.Pembiayaan = (string)value; break;							
						case "PenanggungJawab": this.str.PenanggungJawab = (string)value; break;							
						case "TujuanKunj": this.str.TujuanKunj = (string)value; break;							
						case "FlagProcedure": this.str.FlagProcedure = (string)value; break;							
						case "KdPenunjang": this.str.KdPenunjang = (string)value; break;							
						case "AssesmentPel": this.str.AssesmentPel = (string)value; break;							
						case "KodeDpjpKontrol": this.str.KodeDpjpKontrol = (string)value; break;							
						case "NoLP": this.str.NoLP = (string)value; break;							
						case "KlsHak": this.str.KlsHak = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "SepID":
						
							if (value == null || value is System.Int64)
								this.SepID = (System.Int64?)value;
							break;
						
						case "TanggalSEP":
						
							if (value == null || value is System.DateTime)
								this.TanggalSEP = (System.DateTime?)value;
							break;
						
						case "TanggalRujukan":
						
							if (value == null || value is System.DateTime)
								this.TanggalRujukan = (System.DateTime?)value;
							break;
						
						case "TanggalPulang":
						
							if (value == null || value is System.DateTime)
								this.TanggalPulang = (System.DateTime?)value;
							break;
						
						case "TanggalLahir":
						
							if (value == null || value is System.DateTime)
								this.TanggalLahir = (System.DateTime?)value;
							break;
						
						case "TariffCBG":
						
							if (value == null || value is System.Decimal)
								this.TariffCBG = (System.Decimal?)value;
							break;
						
						case "IsEksekutif":
						
							if (value == null || value is System.Boolean)
								this.IsEksekutif = (System.Boolean?)value;
							break;
						
						case "IsCob":
						
							if (value == null || value is System.Boolean)
								this.IsCob = (System.Boolean?)value;
							break;
						
						case "IsKatarak":
						
							if (value == null || value is System.Boolean)
								this.IsKatarak = (System.Boolean?)value;
							break;
						
						case "TglKejadian":
						
							if (value == null || value is System.DateTime)
								this.TglKejadian = (System.DateTime?)value;
							break;
						
						case "IsSuplesi":
						
							if (value == null || value is System.Boolean)
								this.IsSuplesi = (System.Boolean?)value;
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
		/// Maps to BpjsSEP.SepID
		/// </summary>
		virtual public System.Int64? SepID
		{
			get
			{
				return base.GetSystemInt64(BpjsSEPMetadata.ColumnNames.SepID);
			}
			
			set
			{
				base.SetSystemInt64(BpjsSEPMetadata.ColumnNames.SepID, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NoSEP
		/// </summary>
		virtual public System.String NoSEP
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NoSEP);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NoSEP, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NomorKartu
		/// </summary>
		virtual public System.String NomorKartu
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NomorKartu);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NomorKartu, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.TanggalSEP
		/// </summary>
		virtual public System.DateTime? TanggalSEP
		{
			get
			{
				return base.GetSystemDateTime(BpjsSEPMetadata.ColumnNames.TanggalSEP);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsSEPMetadata.ColumnNames.TanggalSEP, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.TanggalRujukan
		/// </summary>
		virtual public System.DateTime? TanggalRujukan
		{
			get
			{
				return base.GetSystemDateTime(BpjsSEPMetadata.ColumnNames.TanggalRujukan);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsSEPMetadata.ColumnNames.TanggalRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NoRujukan
		/// </summary>
		virtual public System.String NoRujukan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NoRujukan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NoRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.PPKRujukan
		/// </summary>
		virtual public System.String PPKRujukan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.PPKRujukan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.PPKRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NamaPPKRujukan
		/// </summary>
		virtual public System.String NamaPPKRujukan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NamaPPKRujukan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NamaPPKRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.PPKPelayanan
		/// </summary>
		virtual public System.String PPKPelayanan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.PPKPelayanan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.PPKPelayanan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.JenisPelayanan
		/// </summary>
		virtual public System.String JenisPelayanan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.JenisPelayanan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.JenisPelayanan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.Catatan
		/// </summary>
		virtual public System.String Catatan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.Catatan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.Catatan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.DiagnosaAwal
		/// </summary>
		virtual public System.String DiagnosaAwal
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.DiagnosaAwal);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.DiagnosaAwal, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.PoliTujuan
		/// </summary>
		virtual public System.String PoliTujuan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.PoliTujuan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.PoliTujuan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KelasRawat
		/// </summary>
		virtual public System.String KelasRawat
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KelasRawat);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KelasRawat, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.LakaLantas
		/// </summary>
		virtual public System.String LakaLantas
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.LakaLantas);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.LakaLantas, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.User
		/// </summary>
		virtual public System.String User
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.User);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.User, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NoMR
		/// </summary>
		virtual public System.String NoMR
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NoMR);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NoMR, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.TanggalPulang
		/// </summary>
		virtual public System.DateTime? TanggalPulang
		{
			get
			{
				return base.GetSystemDateTime(BpjsSEPMetadata.ColumnNames.TanggalPulang);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsSEPMetadata.ColumnNames.TanggalPulang, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NoTransaksi
		/// </summary>
		virtual public System.String NoTransaksi
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NoTransaksi);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NoTransaksi, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NamaPasien
		/// </summary>
		virtual public System.String NamaPasien
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NamaPasien);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NamaPasien, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NIK
		/// </summary>
		virtual public System.String Nik
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.Nik);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.Nik, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.JenisKelamin
		/// </summary>
		virtual public System.String JenisKelamin
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.JenisKelamin);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.JenisKelamin, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.TanggalLahir
		/// </summary>
		virtual public System.DateTime? TanggalLahir
		{
			get
			{
				return base.GetSystemDateTime(BpjsSEPMetadata.ColumnNames.TanggalLahir);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsSEPMetadata.ColumnNames.TanggalLahir, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.JenisPeserta
		/// </summary>
		virtual public System.String JenisPeserta
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.JenisPeserta);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.JenisPeserta, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.DetailKeanggotaan
		/// </summary>
		virtual public System.String DetailKeanggotaan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.DetailKeanggotaan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.DetailKeanggotaan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KodeCBG
		/// </summary>
		virtual public System.String KodeCBG
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KodeCBG);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KodeCBG, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.TariffCBG
		/// </summary>
		virtual public System.Decimal? TariffCBG
		{
			get
			{
				return base.GetSystemDecimal(BpjsSEPMetadata.ColumnNames.TariffCBG);
			}
			
			set
			{
				base.SetSystemDecimal(BpjsSEPMetadata.ColumnNames.TariffCBG, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.DeskripsiCBG
		/// </summary>
		virtual public System.String DeskripsiCBG
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.DeskripsiCBG);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.DeskripsiCBG, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.LokasiLaka
		/// </summary>
		virtual public System.String LokasiLaka
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.LokasiLaka);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.LokasiLaka, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NamaKelasRawat
		/// </summary>
		virtual public System.String NamaKelasRawat
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NamaKelasRawat);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NamaKelasRawat, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.IsEksekutif
		/// </summary>
		virtual public System.Boolean? IsEksekutif
		{
			get
			{
				return base.GetSystemBoolean(BpjsSEPMetadata.ColumnNames.IsEksekutif);
			}
			
			set
			{
				base.SetSystemBoolean(BpjsSEPMetadata.ColumnNames.IsEksekutif, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.IsCob
		/// </summary>
		virtual public System.Boolean? IsCob
		{
			get
			{
				return base.GetSystemBoolean(BpjsSEPMetadata.ColumnNames.IsCob);
			}
			
			set
			{
				base.SetSystemBoolean(BpjsSEPMetadata.ColumnNames.IsCob, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.PenjaminLaka
		/// </summary>
		virtual public System.String PenjaminLaka
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.PenjaminLaka);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.PenjaminLaka, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NamaCob
		/// </summary>
		virtual public System.String NamaCob
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NamaCob);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NamaCob, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.StatusPeserta
		/// </summary>
		virtual public System.String StatusPeserta
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.StatusPeserta);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.StatusPeserta, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.Umur
		/// </summary>
		virtual public System.String Umur
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.Umur);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.Umur, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.JenisRujukan
		/// </summary>
		virtual public System.String JenisRujukan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.JenisRujukan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.JenisRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.IsKatarak
		/// </summary>
		virtual public System.Boolean? IsKatarak
		{
			get
			{
				return base.GetSystemBoolean(BpjsSEPMetadata.ColumnNames.IsKatarak);
			}
			
			set
			{
				base.SetSystemBoolean(BpjsSEPMetadata.ColumnNames.IsKatarak, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.TglKejadian
		/// </summary>
		virtual public System.DateTime? TglKejadian
		{
			get
			{
				return base.GetSystemDateTime(BpjsSEPMetadata.ColumnNames.TglKejadian);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsSEPMetadata.ColumnNames.TglKejadian, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.IsSuplesi
		/// </summary>
		virtual public System.Boolean? IsSuplesi
		{
			get
			{
				return base.GetSystemBoolean(BpjsSEPMetadata.ColumnNames.IsSuplesi);
			}
			
			set
			{
				base.SetSystemBoolean(BpjsSEPMetadata.ColumnNames.IsSuplesi, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NoSepSuplesi
		/// </summary>
		virtual public System.String NoSepSuplesi
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NoSepSuplesi);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NoSepSuplesi, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KodePropinsi
		/// </summary>
		virtual public System.String KodePropinsi
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KodePropinsi);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KodePropinsi, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KodeKabupaten
		/// </summary>
		virtual public System.String KodeKabupaten
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KodeKabupaten);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KodeKabupaten, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KodeKecamatan
		/// </summary>
		virtual public System.String KodeKecamatan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KodeKecamatan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KodeKecamatan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NoSkdp
		/// </summary>
		virtual public System.String NoSkdp
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NoSkdp);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NoSkdp, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KodeDpjp
		/// </summary>
		virtual public System.String KodeDpjp
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KodeDpjp);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KodeDpjp, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.FromRegistrationNo
		/// </summary>
		virtual public System.String FromRegistrationNo
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.FromRegistrationNo);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.FromRegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.ProlanisPRB
		/// </summary>
		virtual public System.String ProlanisPRB
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.ProlanisPRB);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.ProlanisPRB, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BpjsSEPMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(BpjsSEPMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KodeDpjpPelayanan
		/// </summary>
		virtual public System.String KodeDpjpPelayanan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KodeDpjpPelayanan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KodeDpjpPelayanan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KlsRawatNaik
		/// </summary>
		virtual public System.String KlsRawatNaik
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KlsRawatNaik);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KlsRawatNaik, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.Pembiayaan
		/// </summary>
		virtual public System.String Pembiayaan
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.Pembiayaan);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.Pembiayaan, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.PenanggungJawab
		/// </summary>
		virtual public System.String PenanggungJawab
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.PenanggungJawab);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.PenanggungJawab, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.TujuanKunj
		/// </summary>
		virtual public System.String TujuanKunj
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.TujuanKunj);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.TujuanKunj, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.FlagProcedure
		/// </summary>
		virtual public System.String FlagProcedure
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.FlagProcedure);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.FlagProcedure, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KdPenunjang
		/// </summary>
		virtual public System.String KdPenunjang
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KdPenunjang);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KdPenunjang, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.AssesmentPel
		/// </summary>
		virtual public System.String AssesmentPel
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.AssesmentPel);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.AssesmentPel, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KodeDpjpKontrol
		/// </summary>
		virtual public System.String KodeDpjpKontrol
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KodeDpjpKontrol);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KodeDpjpKontrol, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.NoLP
		/// </summary>
		virtual public System.String NoLP
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.NoLP);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.NoLP, value);
			}
		}
		
		/// <summary>
		/// Maps to BpjsSEP.KlsHak
		/// </summary>
		virtual public System.String KlsHak
		{
			get
			{
				return base.GetSystemString(BpjsSEPMetadata.ColumnNames.KlsHak);
			}
			
			set
			{
				base.SetSystemString(BpjsSEPMetadata.ColumnNames.KlsHak, value);
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
			public esStrings(esBpjsSEP entity)
			{
				this.entity = entity;
			}
			
	
			public System.String SepID
			{
				get
				{
					System.Int64? data = entity.SepID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SepID = null;
					else entity.SepID = Convert.ToInt64(value);
				}
			}
				
			public System.String NoSEP
			{
				get
				{
					System.String data = entity.NoSEP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoSEP = null;
					else entity.NoSEP = Convert.ToString(value);
				}
			}
				
			public System.String NomorKartu
			{
				get
				{
					System.String data = entity.NomorKartu;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NomorKartu = null;
					else entity.NomorKartu = Convert.ToString(value);
				}
			}
				
			public System.String TanggalSEP
			{
				get
				{
					System.DateTime? data = entity.TanggalSEP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TanggalSEP = null;
					else entity.TanggalSEP = Convert.ToDateTime(value);
				}
			}
				
			public System.String TanggalRujukan
			{
				get
				{
					System.DateTime? data = entity.TanggalRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TanggalRujukan = null;
					else entity.TanggalRujukan = Convert.ToDateTime(value);
				}
			}
				
			public System.String NoRujukan
			{
				get
				{
					System.String data = entity.NoRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoRujukan = null;
					else entity.NoRujukan = Convert.ToString(value);
				}
			}
				
			public System.String PPKRujukan
			{
				get
				{
					System.String data = entity.PPKRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PPKRujukan = null;
					else entity.PPKRujukan = Convert.ToString(value);
				}
			}
				
			public System.String NamaPPKRujukan
			{
				get
				{
					System.String data = entity.NamaPPKRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaPPKRujukan = null;
					else entity.NamaPPKRujukan = Convert.ToString(value);
				}
			}
				
			public System.String PPKPelayanan
			{
				get
				{
					System.String data = entity.PPKPelayanan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PPKPelayanan = null;
					else entity.PPKPelayanan = Convert.ToString(value);
				}
			}
				
			public System.String JenisPelayanan
			{
				get
				{
					System.String data = entity.JenisPelayanan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JenisPelayanan = null;
					else entity.JenisPelayanan = Convert.ToString(value);
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
				
			public System.String DiagnosaAwal
			{
				get
				{
					System.String data = entity.DiagnosaAwal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnosaAwal = null;
					else entity.DiagnosaAwal = Convert.ToString(value);
				}
			}
				
			public System.String PoliTujuan
			{
				get
				{
					System.String data = entity.PoliTujuan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PoliTujuan = null;
					else entity.PoliTujuan = Convert.ToString(value);
				}
			}
				
			public System.String KelasRawat
			{
				get
				{
					System.String data = entity.KelasRawat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KelasRawat = null;
					else entity.KelasRawat = Convert.ToString(value);
				}
			}
				
			public System.String LakaLantas
			{
				get
				{
					System.String data = entity.LakaLantas;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LakaLantas = null;
					else entity.LakaLantas = Convert.ToString(value);
				}
			}
				
			public System.String User
			{
				get
				{
					System.String data = entity.User;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.User = null;
					else entity.User = Convert.ToString(value);
				}
			}
				
			public System.String NoMR
			{
				get
				{
					System.String data = entity.NoMR;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoMR = null;
					else entity.NoMR = Convert.ToString(value);
				}
			}
				
			public System.String TanggalPulang
			{
				get
				{
					System.DateTime? data = entity.TanggalPulang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TanggalPulang = null;
					else entity.TanggalPulang = Convert.ToDateTime(value);
				}
			}
				
			public System.String NoTransaksi
			{
				get
				{
					System.String data = entity.NoTransaksi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoTransaksi = null;
					else entity.NoTransaksi = Convert.ToString(value);
				}
			}
				
			public System.String NamaPasien
			{
				get
				{
					System.String data = entity.NamaPasien;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaPasien = null;
					else entity.NamaPasien = Convert.ToString(value);
				}
			}
				
			public System.String Nik
			{
				get
				{
					System.String data = entity.Nik;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nik = null;
					else entity.Nik = Convert.ToString(value);
				}
			}
				
			public System.String JenisKelamin
			{
				get
				{
					System.String data = entity.JenisKelamin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JenisKelamin = null;
					else entity.JenisKelamin = Convert.ToString(value);
				}
			}
				
			public System.String TanggalLahir
			{
				get
				{
					System.DateTime? data = entity.TanggalLahir;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TanggalLahir = null;
					else entity.TanggalLahir = Convert.ToDateTime(value);
				}
			}
				
			public System.String JenisPeserta
			{
				get
				{
					System.String data = entity.JenisPeserta;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JenisPeserta = null;
					else entity.JenisPeserta = Convert.ToString(value);
				}
			}
				
			public System.String DetailKeanggotaan
			{
				get
				{
					System.String data = entity.DetailKeanggotaan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DetailKeanggotaan = null;
					else entity.DetailKeanggotaan = Convert.ToString(value);
				}
			}
				
			public System.String PatientID
			{
				get
				{
					System.String data = entity.PatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientID = null;
					else entity.PatientID = Convert.ToString(value);
				}
			}
				
			public System.String KodeCBG
			{
				get
				{
					System.String data = entity.KodeCBG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeCBG = null;
					else entity.KodeCBG = Convert.ToString(value);
				}
			}
				
			public System.String TariffCBG
			{
				get
				{
					System.Decimal? data = entity.TariffCBG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffCBG = null;
					else entity.TariffCBG = Convert.ToDecimal(value);
				}
			}
				
			public System.String DeskripsiCBG
			{
				get
				{
					System.String data = entity.DeskripsiCBG;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeskripsiCBG = null;
					else entity.DeskripsiCBG = Convert.ToString(value);
				}
			}
				
			public System.String LokasiLaka
			{
				get
				{
					System.String data = entity.LokasiLaka;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LokasiLaka = null;
					else entity.LokasiLaka = Convert.ToString(value);
				}
			}
				
			public System.String NamaKelasRawat
			{
				get
				{
					System.String data = entity.NamaKelasRawat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaKelasRawat = null;
					else entity.NamaKelasRawat = Convert.ToString(value);
				}
			}
				
			public System.String IsEksekutif
			{
				get
				{
					System.Boolean? data = entity.IsEksekutif;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEksekutif = null;
					else entity.IsEksekutif = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsCob
			{
				get
				{
					System.Boolean? data = entity.IsCob;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCob = null;
					else entity.IsCob = Convert.ToBoolean(value);
				}
			}
				
			public System.String PenjaminLaka
			{
				get
				{
					System.String data = entity.PenjaminLaka;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PenjaminLaka = null;
					else entity.PenjaminLaka = Convert.ToString(value);
				}
			}
				
			public System.String NamaCob
			{
				get
				{
					System.String data = entity.NamaCob;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaCob = null;
					else entity.NamaCob = Convert.ToString(value);
				}
			}
				
			public System.String StatusPeserta
			{
				get
				{
					System.String data = entity.StatusPeserta;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StatusPeserta = null;
					else entity.StatusPeserta = Convert.ToString(value);
				}
			}
				
			public System.String Umur
			{
				get
				{
					System.String data = entity.Umur;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Umur = null;
					else entity.Umur = Convert.ToString(value);
				}
			}
				
			public System.String JenisRujukan
			{
				get
				{
					System.String data = entity.JenisRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JenisRujukan = null;
					else entity.JenisRujukan = Convert.ToString(value);
				}
			}
				
			public System.String IsKatarak
			{
				get
				{
					System.Boolean? data = entity.IsKatarak;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsKatarak = null;
					else entity.IsKatarak = Convert.ToBoolean(value);
				}
			}
				
			public System.String TglKejadian
			{
				get
				{
					System.DateTime? data = entity.TglKejadian;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TglKejadian = null;
					else entity.TglKejadian = Convert.ToDateTime(value);
				}
			}
				
			public System.String IsSuplesi
			{
				get
				{
					System.Boolean? data = entity.IsSuplesi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSuplesi = null;
					else entity.IsSuplesi = Convert.ToBoolean(value);
				}
			}
				
			public System.String NoSepSuplesi
			{
				get
				{
					System.String data = entity.NoSepSuplesi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoSepSuplesi = null;
					else entity.NoSepSuplesi = Convert.ToString(value);
				}
			}
				
			public System.String KodePropinsi
			{
				get
				{
					System.String data = entity.KodePropinsi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodePropinsi = null;
					else entity.KodePropinsi = Convert.ToString(value);
				}
			}
				
			public System.String KodeKabupaten
			{
				get
				{
					System.String data = entity.KodeKabupaten;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeKabupaten = null;
					else entity.KodeKabupaten = Convert.ToString(value);
				}
			}
				
			public System.String KodeKecamatan
			{
				get
				{
					System.String data = entity.KodeKecamatan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeKecamatan = null;
					else entity.KodeKecamatan = Convert.ToString(value);
				}
			}
				
			public System.String NoSkdp
			{
				get
				{
					System.String data = entity.NoSkdp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoSkdp = null;
					else entity.NoSkdp = Convert.ToString(value);
				}
			}
				
			public System.String KodeDpjp
			{
				get
				{
					System.String data = entity.KodeDpjp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeDpjp = null;
					else entity.KodeDpjp = Convert.ToString(value);
				}
			}
				
			public System.String FromRegistrationNo
			{
				get
				{
					System.String data = entity.FromRegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromRegistrationNo = null;
					else entity.FromRegistrationNo = Convert.ToString(value);
				}
			}
				
			public System.String ProlanisPRB
			{
				get
				{
					System.String data = entity.ProlanisPRB;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProlanisPRB = null;
					else entity.ProlanisPRB = Convert.ToString(value);
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
				
			public System.String KodeDpjpPelayanan
			{
				get
				{
					System.String data = entity.KodeDpjpPelayanan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeDpjpPelayanan = null;
					else entity.KodeDpjpPelayanan = Convert.ToString(value);
				}
			}
				
			public System.String KlsRawatNaik
			{
				get
				{
					System.String data = entity.KlsRawatNaik;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KlsRawatNaik = null;
					else entity.KlsRawatNaik = Convert.ToString(value);
				}
			}
				
			public System.String Pembiayaan
			{
				get
				{
					System.String data = entity.Pembiayaan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pembiayaan = null;
					else entity.Pembiayaan = Convert.ToString(value);
				}
			}
				
			public System.String PenanggungJawab
			{
				get
				{
					System.String data = entity.PenanggungJawab;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PenanggungJawab = null;
					else entity.PenanggungJawab = Convert.ToString(value);
				}
			}
				
			public System.String TujuanKunj
			{
				get
				{
					System.String data = entity.TujuanKunj;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TujuanKunj = null;
					else entity.TujuanKunj = Convert.ToString(value);
				}
			}
				
			public System.String FlagProcedure
			{
				get
				{
					System.String data = entity.FlagProcedure;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FlagProcedure = null;
					else entity.FlagProcedure = Convert.ToString(value);
				}
			}
				
			public System.String KdPenunjang
			{
				get
				{
					System.String data = entity.KdPenunjang;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KdPenunjang = null;
					else entity.KdPenunjang = Convert.ToString(value);
				}
			}
				
			public System.String AssesmentPel
			{
				get
				{
					System.String data = entity.AssesmentPel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssesmentPel = null;
					else entity.AssesmentPel = Convert.ToString(value);
				}
			}
				
			public System.String KodeDpjpKontrol
			{
				get
				{
					System.String data = entity.KodeDpjpKontrol;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KodeDpjpKontrol = null;
					else entity.KodeDpjpKontrol = Convert.ToString(value);
				}
			}
				
			public System.String NoLP
			{
				get
				{
					System.String data = entity.NoLP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoLP = null;
					else entity.NoLP = Convert.ToString(value);
				}
			}
				
			public System.String KlsHak
			{
				get
				{
					System.String data = entity.KlsHak;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KlsHak = null;
					else entity.KlsHak = Convert.ToString(value);
				}
			}
			

			private esBpjsSEP entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBpjsSEPQuery query)
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
				throw new Exception("esBpjsSEP can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esBpjsSEPQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return BpjsSEPMetadata.Meta();
			}
		}	
		

		public esQueryItem SepID
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.SepID, esSystemType.Int64);
			}
		} 
		
		public esQueryItem NoSEP
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NoSEP, esSystemType.String);
			}
		} 
		
		public esQueryItem NomorKartu
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NomorKartu, esSystemType.String);
			}
		} 
		
		public esQueryItem TanggalSEP
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.TanggalSEP, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem TanggalRujukan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.TanggalRujukan, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem NoRujukan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NoRujukan, esSystemType.String);
			}
		} 
		
		public esQueryItem PPKRujukan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.PPKRujukan, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaPPKRujukan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NamaPPKRujukan, esSystemType.String);
			}
		} 
		
		public esQueryItem PPKPelayanan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.PPKPelayanan, esSystemType.String);
			}
		} 
		
		public esQueryItem JenisPelayanan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.JenisPelayanan, esSystemType.String);
			}
		} 
		
		public esQueryItem Catatan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.Catatan, esSystemType.String);
			}
		} 
		
		public esQueryItem DiagnosaAwal
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.DiagnosaAwal, esSystemType.String);
			}
		} 
		
		public esQueryItem PoliTujuan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.PoliTujuan, esSystemType.String);
			}
		} 
		
		public esQueryItem KelasRawat
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KelasRawat, esSystemType.String);
			}
		} 
		
		public esQueryItem LakaLantas
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.LakaLantas, esSystemType.String);
			}
		} 
		
		public esQueryItem User
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.User, esSystemType.String);
			}
		} 
		
		public esQueryItem NoMR
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NoMR, esSystemType.String);
			}
		} 
		
		public esQueryItem TanggalPulang
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.TanggalPulang, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem NoTransaksi
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NoTransaksi, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaPasien
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NamaPasien, esSystemType.String);
			}
		} 
		
		public esQueryItem Nik
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.Nik, esSystemType.String);
			}
		} 
		
		public esQueryItem JenisKelamin
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.JenisKelamin, esSystemType.String);
			}
		} 
		
		public esQueryItem TanggalLahir
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.TanggalLahir, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem JenisPeserta
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.JenisPeserta, esSystemType.String);
			}
		} 
		
		public esQueryItem DetailKeanggotaan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.DetailKeanggotaan, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeCBG
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KodeCBG, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffCBG
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.TariffCBG, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeskripsiCBG
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.DeskripsiCBG, esSystemType.String);
			}
		} 
		
		public esQueryItem LokasiLaka
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.LokasiLaka, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaKelasRawat
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NamaKelasRawat, esSystemType.String);
			}
		} 
		
		public esQueryItem IsEksekutif
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.IsEksekutif, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsCob
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.IsCob, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem PenjaminLaka
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.PenjaminLaka, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaCob
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NamaCob, esSystemType.String);
			}
		} 
		
		public esQueryItem StatusPeserta
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.StatusPeserta, esSystemType.String);
			}
		} 
		
		public esQueryItem Umur
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.Umur, esSystemType.String);
			}
		} 
		
		public esQueryItem JenisRujukan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.JenisRujukan, esSystemType.String);
			}
		} 
		
		public esQueryItem IsKatarak
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.IsKatarak, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem TglKejadian
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.TglKejadian, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsSuplesi
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.IsSuplesi, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem NoSepSuplesi
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NoSepSuplesi, esSystemType.String);
			}
		} 
		
		public esQueryItem KodePropinsi
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KodePropinsi, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeKabupaten
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KodeKabupaten, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeKecamatan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KodeKecamatan, esSystemType.String);
			}
		} 
		
		public esQueryItem NoSkdp
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NoSkdp, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeDpjp
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KodeDpjp, esSystemType.String);
			}
		} 
		
		public esQueryItem FromRegistrationNo
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.FromRegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ProlanisPRB
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.ProlanisPRB, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeDpjpPelayanan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KodeDpjpPelayanan, esSystemType.String);
			}
		} 
		
		public esQueryItem KlsRawatNaik
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KlsRawatNaik, esSystemType.String);
			}
		} 
		
		public esQueryItem Pembiayaan
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.Pembiayaan, esSystemType.String);
			}
		} 
		
		public esQueryItem PenanggungJawab
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.PenanggungJawab, esSystemType.String);
			}
		} 
		
		public esQueryItem TujuanKunj
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.TujuanKunj, esSystemType.String);
			}
		} 
		
		public esQueryItem FlagProcedure
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.FlagProcedure, esSystemType.String);
			}
		} 
		
		public esQueryItem KdPenunjang
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KdPenunjang, esSystemType.String);
			}
		} 
		
		public esQueryItem AssesmentPel
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.AssesmentPel, esSystemType.String);
			}
		} 
		
		public esQueryItem KodeDpjpKontrol
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KodeDpjpKontrol, esSystemType.String);
			}
		} 
		
		public esQueryItem NoLP
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.NoLP, esSystemType.String);
			}
		} 
		
		public esQueryItem KlsHak
		{
			get
			{
				return new esQueryItem(this, BpjsSEPMetadata.ColumnNames.KlsHak, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BpjsSEPCollection")]
	public partial class BpjsSEPCollection : esBpjsSEPCollection, IEnumerable<BpjsSEP>
	{
		public BpjsSEPCollection()
		{

		}
		
		public static implicit operator List<BpjsSEP>(BpjsSEPCollection coll)
		{
			List<BpjsSEP> list = new List<BpjsSEP>();
			
			foreach (BpjsSEP emp in coll)
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
				return  BpjsSEPMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BpjsSEPQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BpjsSEP(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BpjsSEP();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public BpjsSEPQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BpjsSEPQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(BpjsSEPQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public BpjsSEP AddNew()
		{
			BpjsSEP entity = base.AddNewEntity() as BpjsSEP;
			
			return entity;
		}

		public BpjsSEP FindByPrimaryKey(System.Int64 sepID)
		{
			return base.FindByPrimaryKey(sepID) as BpjsSEP;
		}


		#region IEnumerable<BpjsSEP> Members

		IEnumerator<BpjsSEP> IEnumerable<BpjsSEP>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as BpjsSEP;
			}
		}

		#endregion
		
		private BpjsSEPQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BpjsSEP' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("BpjsSEP ({SepID})")]
	[Serializable]
	public partial class BpjsSEP : esBpjsSEP
	{
		public BpjsSEP()
		{

		}
	
		public BpjsSEP(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BpjsSEPMetadata.Meta();
			}
		}
		
		
		
		override protected esBpjsSEPQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BpjsSEPQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public BpjsSEPQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BpjsSEPQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(BpjsSEPQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private BpjsSEPQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class BpjsSEPQuery : esBpjsSEPQuery
	{
		public BpjsSEPQuery()
		{

		}		
		
		public BpjsSEPQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "BpjsSEPQuery";
        }
		
			
	}


	[Serializable]
	public partial class BpjsSEPMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BpjsSEPMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.SepID, 0, typeof(System.Int64), esSystemType.Int64);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.SepID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 19;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NoSEP, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NoSEP;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NomorKartu, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NomorKartu;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.TanggalSEP, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.TanggalSEP;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.TanggalRujukan, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.TanggalRujukan;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NoRujukan, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NoRujukan;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.PPKRujukan, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.PPKRujukan;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NamaPPKRujukan, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NamaPPKRujukan;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.PPKPelayanan, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.PPKPelayanan;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.JenisPelayanan, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.JenisPelayanan;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.Catatan, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.Catatan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.DiagnosaAwal, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.DiagnosaAwal;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.PoliTujuan, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.PoliTujuan;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KelasRawat, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KelasRawat;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.LakaLantas, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.LakaLantas;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.User, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.User;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NoMR, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NoMR;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.TanggalPulang, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.TanggalPulang;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NoTransaksi, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NoTransaksi;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NamaPasien, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NamaPasien;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.Nik, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.Nik;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.JenisKelamin, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.JenisKelamin;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.TanggalLahir, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.TanggalLahir;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.JenisPeserta, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.JenisPeserta;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.DetailKeanggotaan, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.DetailKeanggotaan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.PatientID, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KodeCBG, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KodeCBG;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.TariffCBG, 27, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.TariffCBG;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.DeskripsiCBG, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.DeskripsiCBG;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.LokasiLaka, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.LokasiLaka;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NamaKelasRawat, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NamaKelasRawat;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.IsEksekutif, 31, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.IsEksekutif;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.IsCob, 32, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.IsCob;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.PenjaminLaka, 33, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.PenjaminLaka;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NamaCob, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NamaCob;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.StatusPeserta, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.StatusPeserta;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.Umur, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.Umur;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.JenisRujukan, 37, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.JenisRujukan;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.IsKatarak, 38, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.IsKatarak;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.TglKejadian, 39, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.TglKejadian;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.IsSuplesi, 40, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.IsSuplesi;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NoSepSuplesi, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NoSepSuplesi;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KodePropinsi, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KodePropinsi;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KodeKabupaten, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KodeKabupaten;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KodeKecamatan, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KodeKecamatan;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NoSkdp, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NoSkdp;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KodeDpjp, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KodeDpjp;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.FromRegistrationNo, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.FromRegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.ProlanisPRB, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.ProlanisPRB;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.LastUpdateDateTime, 49, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.LastUpdateByUserID, 50, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KodeDpjpPelayanan, 51, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KodeDpjpPelayanan;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KlsRawatNaik, 52, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KlsRawatNaik;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.Pembiayaan, 53, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.Pembiayaan;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.PenanggungJawab, 54, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.PenanggungJawab;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.TujuanKunj, 55, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.TujuanKunj;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.FlagProcedure, 56, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.FlagProcedure;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KdPenunjang, 57, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KdPenunjang;
			c.CharacterMaxLength = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.AssesmentPel, 58, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.AssesmentPel;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KodeDpjpKontrol, 59, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KodeDpjpKontrol;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.NoLP, 60, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.NoLP;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(BpjsSEPMetadata.ColumnNames.KlsHak, 61, typeof(System.String), esSystemType.String);
			c.PropertyName = BpjsSEPMetadata.PropertyNames.KlsHak;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public BpjsSEPMetadata Meta()
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
			 public const string SepID = "SepID";
			 public const string NoSEP = "NoSEP";
			 public const string NomorKartu = "NomorKartu";
			 public const string TanggalSEP = "TanggalSEP";
			 public const string TanggalRujukan = "TanggalRujukan";
			 public const string NoRujukan = "NoRujukan";
			 public const string PPKRujukan = "PPKRujukan";
			 public const string NamaPPKRujukan = "NamaPPKRujukan";
			 public const string PPKPelayanan = "PPKPelayanan";
			 public const string JenisPelayanan = "JenisPelayanan";
			 public const string Catatan = "Catatan";
			 public const string DiagnosaAwal = "DiagnosaAwal";
			 public const string PoliTujuan = "PoliTujuan";
			 public const string KelasRawat = "KelasRawat";
			 public const string LakaLantas = "LakaLantas";
			 public const string User = "User";
			 public const string NoMR = "NoMR";
			 public const string TanggalPulang = "TanggalPulang";
			 public const string NoTransaksi = "NoTransaksi";
			 public const string NamaPasien = "NamaPasien";
			 public const string Nik = "NIK";
			 public const string JenisKelamin = "JenisKelamin";
			 public const string TanggalLahir = "TanggalLahir";
			 public const string JenisPeserta = "JenisPeserta";
			 public const string DetailKeanggotaan = "DetailKeanggotaan";
			 public const string PatientID = "PatientID";
			 public const string KodeCBG = "KodeCBG";
			 public const string TariffCBG = "TariffCBG";
			 public const string DeskripsiCBG = "DeskripsiCBG";
			 public const string LokasiLaka = "LokasiLaka";
			 public const string NamaKelasRawat = "NamaKelasRawat";
			 public const string IsEksekutif = "IsEksekutif";
			 public const string IsCob = "IsCob";
			 public const string PenjaminLaka = "PenjaminLaka";
			 public const string NamaCob = "NamaCob";
			 public const string StatusPeserta = "StatusPeserta";
			 public const string Umur = "Umur";
			 public const string JenisRujukan = "JenisRujukan";
			 public const string IsKatarak = "IsKatarak";
			 public const string TglKejadian = "TglKejadian";
			 public const string IsSuplesi = "IsSuplesi";
			 public const string NoSepSuplesi = "NoSepSuplesi";
			 public const string KodePropinsi = "KodePropinsi";
			 public const string KodeKabupaten = "KodeKabupaten";
			 public const string KodeKecamatan = "KodeKecamatan";
			 public const string NoSkdp = "NoSkdp";
			 public const string KodeDpjp = "KodeDpjp";
			 public const string FromRegistrationNo = "FromRegistrationNo";
			 public const string ProlanisPRB = "ProlanisPRB";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string KodeDpjpPelayanan = "KodeDpjpPelayanan";
			 public const string KlsRawatNaik = "KlsRawatNaik";
			 public const string Pembiayaan = "Pembiayaan";
			 public const string PenanggungJawab = "PenanggungJawab";
			 public const string TujuanKunj = "TujuanKunj";
			 public const string FlagProcedure = "FlagProcedure";
			 public const string KdPenunjang = "KdPenunjang";
			 public const string AssesmentPel = "AssesmentPel";
			 public const string KodeDpjpKontrol = "KodeDpjpKontrol";
			 public const string NoLP = "NoLP";
			 public const string KlsHak = "KlsHak";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SepID = "SepID";
			 public const string NoSEP = "NoSEP";
			 public const string NomorKartu = "NomorKartu";
			 public const string TanggalSEP = "TanggalSEP";
			 public const string TanggalRujukan = "TanggalRujukan";
			 public const string NoRujukan = "NoRujukan";
			 public const string PPKRujukan = "PPKRujukan";
			 public const string NamaPPKRujukan = "NamaPPKRujukan";
			 public const string PPKPelayanan = "PPKPelayanan";
			 public const string JenisPelayanan = "JenisPelayanan";
			 public const string Catatan = "Catatan";
			 public const string DiagnosaAwal = "DiagnosaAwal";
			 public const string PoliTujuan = "PoliTujuan";
			 public const string KelasRawat = "KelasRawat";
			 public const string LakaLantas = "LakaLantas";
			 public const string User = "User";
			 public const string NoMR = "NoMR";
			 public const string TanggalPulang = "TanggalPulang";
			 public const string NoTransaksi = "NoTransaksi";
			 public const string NamaPasien = "NamaPasien";
			 public const string Nik = "Nik";
			 public const string JenisKelamin = "JenisKelamin";
			 public const string TanggalLahir = "TanggalLahir";
			 public const string JenisPeserta = "JenisPeserta";
			 public const string DetailKeanggotaan = "DetailKeanggotaan";
			 public const string PatientID = "PatientID";
			 public const string KodeCBG = "KodeCBG";
			 public const string TariffCBG = "TariffCBG";
			 public const string DeskripsiCBG = "DeskripsiCBG";
			 public const string LokasiLaka = "LokasiLaka";
			 public const string NamaKelasRawat = "NamaKelasRawat";
			 public const string IsEksekutif = "IsEksekutif";
			 public const string IsCob = "IsCob";
			 public const string PenjaminLaka = "PenjaminLaka";
			 public const string NamaCob = "NamaCob";
			 public const string StatusPeserta = "StatusPeserta";
			 public const string Umur = "Umur";
			 public const string JenisRujukan = "JenisRujukan";
			 public const string IsKatarak = "IsKatarak";
			 public const string TglKejadian = "TglKejadian";
			 public const string IsSuplesi = "IsSuplesi";
			 public const string NoSepSuplesi = "NoSepSuplesi";
			 public const string KodePropinsi = "KodePropinsi";
			 public const string KodeKabupaten = "KodeKabupaten";
			 public const string KodeKecamatan = "KodeKecamatan";
			 public const string NoSkdp = "NoSkdp";
			 public const string KodeDpjp = "KodeDpjp";
			 public const string FromRegistrationNo = "FromRegistrationNo";
			 public const string ProlanisPRB = "ProlanisPRB";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string KodeDpjpPelayanan = "KodeDpjpPelayanan";
			 public const string KlsRawatNaik = "KlsRawatNaik";
			 public const string Pembiayaan = "Pembiayaan";
			 public const string PenanggungJawab = "PenanggungJawab";
			 public const string TujuanKunj = "TujuanKunj";
			 public const string FlagProcedure = "FlagProcedure";
			 public const string KdPenunjang = "KdPenunjang";
			 public const string AssesmentPel = "AssesmentPel";
			 public const string KodeDpjpKontrol = "KodeDpjpKontrol";
			 public const string NoLP = "NoLP";
			 public const string KlsHak = "KlsHak";
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
			lock (typeof(BpjsSEPMetadata))
			{
				if(BpjsSEPMetadata.mapDelegates == null)
				{
					BpjsSEPMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (BpjsSEPMetadata.meta == null)
				{
					BpjsSEPMetadata.meta = new BpjsSEPMetadata();
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
				

				meta.AddTypeMap("SepID", new esTypeMap("bigint", "System.Int64"));
				meta.AddTypeMap("NoSEP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NomorKartu", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TanggalSEP", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("TanggalRujukan", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("NoRujukan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PPKRujukan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaPPKRujukan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PPKPelayanan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JenisPelayanan", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Catatan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnosaAwal", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PoliTujuan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KelasRawat", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("LakaLantas", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("User", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoMR", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TanggalPulang", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("NoTransaksi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaPasien", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nik", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JenisKelamin", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("TanggalLahir", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("JenisPeserta", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DetailKeanggotaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeCBG", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffCBG", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeskripsiCBG", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LokasiLaka", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaKelasRawat", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEksekutif", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCob", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PenjaminLaka", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaCob", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StatusPeserta", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Umur", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JenisRujukan", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("IsKatarak", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("TglKejadian", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsSuplesi", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("NoSepSuplesi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodePropinsi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeKabupaten", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeKecamatan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoSkdp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeDpjp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromRegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProlanisPRB", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KodeDpjpPelayanan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KlsRawatNaik", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Pembiayaan", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("PenanggungJawab", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TujuanKunj", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("FlagProcedure", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("KdPenunjang", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("AssesmentPel", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("KodeDpjpKontrol", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NoLP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KlsHak", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "BpjsSEP";
				meta.Destination = "BpjsSEP";
				
				meta.spInsert = "proc_BpjsSEPInsert";				
				meta.spUpdate = "proc_BpjsSEPUpdate";		
				meta.spDelete = "proc_BpjsSEPDelete";
				meta.spLoadAll = "proc_BpjsSEPLoadAll";
				meta.spLoadByPrimaryKey = "proc_BpjsSEPLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BpjsSEPMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
