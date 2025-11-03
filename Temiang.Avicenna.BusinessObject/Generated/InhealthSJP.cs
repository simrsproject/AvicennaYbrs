/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 8/30/2017 2:46:44 PM
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
	abstract public class esInhealthSJPCollection : esEntityCollectionWAuditLog
	{
		public esInhealthSJPCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "InhealthSJPCollection";
		}

		#region Query Logic
		protected void InitQuery(esInhealthSJPQuery query)
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
			this.InitQuery(query as esInhealthSJPQuery);
		}
		#endregion
		
		virtual public InhealthSJP DetachEntity(InhealthSJP entity)
		{
			return base.DetachEntity(entity) as InhealthSJP;
		}
		
		virtual public InhealthSJP AttachEntity(InhealthSJP entity)
		{
			return base.AttachEntity(entity) as InhealthSJP;
		}
		
		virtual public void Combine(InhealthSJPCollection collection)
		{
			base.Combine(collection);
		}
		
		new public InhealthSJP this[int index]
		{
			get
			{
				return base[index] as InhealthSJP;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(InhealthSJP);
		}
	}



	[Serializable]
	abstract public class esInhealthSJP : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esInhealthSJPQuery GetDynamicQuery()
		{
			return null;
		}

		public esInhealthSJP()
		{

		}

		public esInhealthSJP(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String nosjp)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nosjp);
			else
				return LoadByPrimaryKeyStoredProcedure(nosjp);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String nosjp)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(nosjp);
			else
				return LoadByPrimaryKeyStoredProcedure(nosjp);
		}

		private bool LoadByPrimaryKeyDynamic(System.String nosjp)
		{
			esInhealthSJPQuery query = this.GetDynamicQuery();
			query.Where(query.Nosjp == nosjp);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String nosjp)
		{
			esParameters parms = new esParameters();
			parms.Add("nosjp",nosjp);
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
						case "Nosjp": this.str.Nosjp = (string)value; break;							
						case "Nokainhealth": this.str.Nokainhealth = (string)value; break;							
						case "Tanggalpelayanan": this.str.Tanggalpelayanan = (string)value; break;							
						case "Tanggalasalrujukan": this.str.Tanggalasalrujukan = (string)value; break;							
						case "Nomorasalrujukan": this.str.Nomorasalrujukan = (string)value; break;							
						case "Kodeproviderasalrujukan": this.str.Kodeproviderasalrujukan = (string)value; break;							
						case "NamaProviderAsalRujukan": this.str.NamaProviderAsalRujukan = (string)value; break;							
						case "Kodeprovider": this.str.Kodeprovider = (string)value; break;							
						case "Jenispelayanan": this.str.Jenispelayanan = (string)value; break;							
						case "Informasitambahan": this.str.Informasitambahan = (string)value; break;							
						case "Kodediagnosautama": this.str.Kodediagnosautama = (string)value; break;							
						case "Kodediagnosatambahan": this.str.Kodediagnosatambahan = (string)value; break;							
						case "Poli": this.str.Poli = (string)value; break;							
						case "Kelasrawat": this.str.Kelasrawat = (string)value; break;							
						case "Kecelakaankerja": this.str.Kecelakaankerja = (string)value; break;							
						case "Username": this.str.Username = (string)value; break;							
						case "Nomormedicalreport": this.str.Nomormedicalreport = (string)value; break;							
						case "TanggalPulang": this.str.TanggalPulang = (string)value; break;							
						case "NoTransaksi": this.str.NoTransaksi = (string)value; break;							
						case "DetailKeanggotaan": this.str.DetailKeanggotaan = (string)value; break;							
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "Kodejenpelruangrawat": this.str.Kodejenpelruangrawat = (string)value; break;							
						case "NamaPasien": this.str.NamaPasien = (string)value; break;							
						case "TanggalLahir": this.str.TanggalLahir = (string)value; break;							
						case "NoKartuBpjs": this.str.NoKartuBpjs = (string)value; break;							
						case "NamaProduk": this.str.NamaProduk = (string)value; break;							
						case "KelasRawatPeserta": this.str.KelasRawatPeserta = (string)value; break;							
						case "BadanUsaha": this.str.BadanUsaha = (string)value; break;							
						case "ProdukCob": this.str.ProdukCob = (string)value; break;							
						case "Idsjp": this.str.Idsjp = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Tanggalpelayanan":
						
							if (value == null || value is System.DateTime)
								this.Tanggalpelayanan = (System.DateTime?)value;
							break;
						
						case "Tanggalasalrujukan":
						
							if (value == null || value is System.DateTime)
								this.Tanggalasalrujukan = (System.DateTime?)value;
							break;
						
						case "Kecelakaankerja":
						
							if (value == null || value is System.Int32)
								this.Kecelakaankerja = (System.Int32?)value;
							break;
						
						case "TanggalPulang":
						
							if (value == null || value is System.DateTime)
								this.TanggalPulang = (System.DateTime?)value;
							break;
						
						case "TanggalLahir":
						
							if (value == null || value is System.DateTime)
								this.TanggalLahir = (System.DateTime?)value;
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
		/// Maps to InhealthSJP.nosjp
		/// </summary>
		virtual public System.String Nosjp
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Nosjp);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Nosjp, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.nokainhealth
		/// </summary>
		virtual public System.String Nokainhealth
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Nokainhealth);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Nokainhealth, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.tanggalpelayanan
		/// </summary>
		virtual public System.DateTime? Tanggalpelayanan
		{
			get
			{
				return base.GetSystemDateTime(InhealthSJPMetadata.ColumnNames.Tanggalpelayanan);
			}
			
			set
			{
				base.SetSystemDateTime(InhealthSJPMetadata.ColumnNames.Tanggalpelayanan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.tanggalasalrujukan
		/// </summary>
		virtual public System.DateTime? Tanggalasalrujukan
		{
			get
			{
				return base.GetSystemDateTime(InhealthSJPMetadata.ColumnNames.Tanggalasalrujukan);
			}
			
			set
			{
				base.SetSystemDateTime(InhealthSJPMetadata.ColumnNames.Tanggalasalrujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.nomorasalrujukan
		/// </summary>
		virtual public System.String Nomorasalrujukan
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Nomorasalrujukan);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Nomorasalrujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.kodeproviderasalrujukan
		/// </summary>
		virtual public System.String Kodeproviderasalrujukan
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Kodeproviderasalrujukan);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Kodeproviderasalrujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.NamaProviderAsalRujukan
		/// </summary>
		virtual public System.String NamaProviderAsalRujukan
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.NamaProviderAsalRujukan);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.NamaProviderAsalRujukan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.kodeprovider
		/// </summary>
		virtual public System.String Kodeprovider
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Kodeprovider);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Kodeprovider, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.jenispelayanan
		/// </summary>
		virtual public System.String Jenispelayanan
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Jenispelayanan);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Jenispelayanan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.informasitambahan
		/// </summary>
		virtual public System.String Informasitambahan
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Informasitambahan);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Informasitambahan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.kodediagnosautama
		/// </summary>
		virtual public System.String Kodediagnosautama
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Kodediagnosautama);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Kodediagnosautama, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.kodediagnosatambahan
		/// </summary>
		virtual public System.String Kodediagnosatambahan
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Kodediagnosatambahan);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Kodediagnosatambahan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.poli
		/// </summary>
		virtual public System.String Poli
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Poli);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Poli, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.kelasrawat
		/// </summary>
		virtual public System.String Kelasrawat
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Kelasrawat);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Kelasrawat, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.kecelakaankerja
		/// </summary>
		virtual public System.Int32? Kecelakaankerja
		{
			get
			{
				return base.GetSystemInt32(InhealthSJPMetadata.ColumnNames.Kecelakaankerja);
			}
			
			set
			{
				base.SetSystemInt32(InhealthSJPMetadata.ColumnNames.Kecelakaankerja, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.username
		/// </summary>
		virtual public System.String Username
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Username);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Username, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.nomormedicalreport
		/// </summary>
		virtual public System.String Nomormedicalreport
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Nomormedicalreport);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Nomormedicalreport, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.TanggalPulang
		/// </summary>
		virtual public System.DateTime? TanggalPulang
		{
			get
			{
				return base.GetSystemDateTime(InhealthSJPMetadata.ColumnNames.TanggalPulang);
			}
			
			set
			{
				base.SetSystemDateTime(InhealthSJPMetadata.ColumnNames.TanggalPulang, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.NoTransaksi
		/// </summary>
		virtual public System.String NoTransaksi
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.NoTransaksi);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.NoTransaksi, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.DetailKeanggotaan
		/// </summary>
		virtual public System.String DetailKeanggotaan
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.DetailKeanggotaan);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.DetailKeanggotaan, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.kodejenpelruangrawat
		/// </summary>
		virtual public System.String Kodejenpelruangrawat
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Kodejenpelruangrawat);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Kodejenpelruangrawat, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.NamaPasien
		/// </summary>
		virtual public System.String NamaPasien
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.NamaPasien);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.NamaPasien, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.TanggalLahir
		/// </summary>
		virtual public System.DateTime? TanggalLahir
		{
			get
			{
				return base.GetSystemDateTime(InhealthSJPMetadata.ColumnNames.TanggalLahir);
			}
			
			set
			{
				base.SetSystemDateTime(InhealthSJPMetadata.ColumnNames.TanggalLahir, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.NoKartuBpjs
		/// </summary>
		virtual public System.String NoKartuBpjs
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.NoKartuBpjs);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.NoKartuBpjs, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.NamaProduk
		/// </summary>
		virtual public System.String NamaProduk
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.NamaProduk);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.NamaProduk, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.KelasRawatPeserta
		/// </summary>
		virtual public System.String KelasRawatPeserta
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.KelasRawatPeserta);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.KelasRawatPeserta, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.BadanUsaha
		/// </summary>
		virtual public System.String BadanUsaha
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.BadanUsaha);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.BadanUsaha, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.ProdukCob
		/// </summary>
		virtual public System.String ProdukCob
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.ProdukCob);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.ProdukCob, value);
			}
		}
		
		/// <summary>
		/// Maps to InhealthSJP.idsjp
		/// </summary>
		virtual public System.String Idsjp
		{
			get
			{
				return base.GetSystemString(InhealthSJPMetadata.ColumnNames.Idsjp);
			}
			
			set
			{
				base.SetSystemString(InhealthSJPMetadata.ColumnNames.Idsjp, value);
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
			public esStrings(esInhealthSJP entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Nosjp
			{
				get
				{
					System.String data = entity.Nosjp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nosjp = null;
					else entity.Nosjp = Convert.ToString(value);
				}
			}
				
			public System.String Nokainhealth
			{
				get
				{
					System.String data = entity.Nokainhealth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nokainhealth = null;
					else entity.Nokainhealth = Convert.ToString(value);
				}
			}
				
			public System.String Tanggalpelayanan
			{
				get
				{
					System.DateTime? data = entity.Tanggalpelayanan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tanggalpelayanan = null;
					else entity.Tanggalpelayanan = Convert.ToDateTime(value);
				}
			}
				
			public System.String Tanggalasalrujukan
			{
				get
				{
					System.DateTime? data = entity.Tanggalasalrujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Tanggalasalrujukan = null;
					else entity.Tanggalasalrujukan = Convert.ToDateTime(value);
				}
			}
				
			public System.String Nomorasalrujukan
			{
				get
				{
					System.String data = entity.Nomorasalrujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nomorasalrujukan = null;
					else entity.Nomorasalrujukan = Convert.ToString(value);
				}
			}
				
			public System.String Kodeproviderasalrujukan
			{
				get
				{
					System.String data = entity.Kodeproviderasalrujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kodeproviderasalrujukan = null;
					else entity.Kodeproviderasalrujukan = Convert.ToString(value);
				}
			}
				
			public System.String NamaProviderAsalRujukan
			{
				get
				{
					System.String data = entity.NamaProviderAsalRujukan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaProviderAsalRujukan = null;
					else entity.NamaProviderAsalRujukan = Convert.ToString(value);
				}
			}
				
			public System.String Kodeprovider
			{
				get
				{
					System.String data = entity.Kodeprovider;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kodeprovider = null;
					else entity.Kodeprovider = Convert.ToString(value);
				}
			}
				
			public System.String Jenispelayanan
			{
				get
				{
					System.String data = entity.Jenispelayanan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Jenispelayanan = null;
					else entity.Jenispelayanan = Convert.ToString(value);
				}
			}
				
			public System.String Informasitambahan
			{
				get
				{
					System.String data = entity.Informasitambahan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Informasitambahan = null;
					else entity.Informasitambahan = Convert.ToString(value);
				}
			}
				
			public System.String Kodediagnosautama
			{
				get
				{
					System.String data = entity.Kodediagnosautama;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kodediagnosautama = null;
					else entity.Kodediagnosautama = Convert.ToString(value);
				}
			}
				
			public System.String Kodediagnosatambahan
			{
				get
				{
					System.String data = entity.Kodediagnosatambahan;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kodediagnosatambahan = null;
					else entity.Kodediagnosatambahan = Convert.ToString(value);
				}
			}
				
			public System.String Poli
			{
				get
				{
					System.String data = entity.Poli;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Poli = null;
					else entity.Poli = Convert.ToString(value);
				}
			}
				
			public System.String Kelasrawat
			{
				get
				{
					System.String data = entity.Kelasrawat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kelasrawat = null;
					else entity.Kelasrawat = Convert.ToString(value);
				}
			}
				
			public System.String Kecelakaankerja
			{
				get
				{
					System.Int32? data = entity.Kecelakaankerja;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kecelakaankerja = null;
					else entity.Kecelakaankerja = Convert.ToInt32(value);
				}
			}
				
			public System.String Username
			{
				get
				{
					System.String data = entity.Username;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Username = null;
					else entity.Username = Convert.ToString(value);
				}
			}
				
			public System.String Nomormedicalreport
			{
				get
				{
					System.String data = entity.Nomormedicalreport;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Nomormedicalreport = null;
					else entity.Nomormedicalreport = Convert.ToString(value);
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
				
			public System.String Kodejenpelruangrawat
			{
				get
				{
					System.String data = entity.Kodejenpelruangrawat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Kodejenpelruangrawat = null;
					else entity.Kodejenpelruangrawat = Convert.ToString(value);
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
				
			public System.String NoKartuBpjs
			{
				get
				{
					System.String data = entity.NoKartuBpjs;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NoKartuBpjs = null;
					else entity.NoKartuBpjs = Convert.ToString(value);
				}
			}
				
			public System.String NamaProduk
			{
				get
				{
					System.String data = entity.NamaProduk;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NamaProduk = null;
					else entity.NamaProduk = Convert.ToString(value);
				}
			}
				
			public System.String KelasRawatPeserta
			{
				get
				{
					System.String data = entity.KelasRawatPeserta;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KelasRawatPeserta = null;
					else entity.KelasRawatPeserta = Convert.ToString(value);
				}
			}
				
			public System.String BadanUsaha
			{
				get
				{
					System.String data = entity.BadanUsaha;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BadanUsaha = null;
					else entity.BadanUsaha = Convert.ToString(value);
				}
			}
				
			public System.String ProdukCob
			{
				get
				{
					System.String data = entity.ProdukCob;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProdukCob = null;
					else entity.ProdukCob = Convert.ToString(value);
				}
			}
				
			public System.String Idsjp
			{
				get
				{
					System.String data = entity.Idsjp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Idsjp = null;
					else entity.Idsjp = Convert.ToString(value);
				}
			}
			

			private esInhealthSJP entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esInhealthSJPQuery query)
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
				throw new Exception("esInhealthSJP can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class InhealthSJP : esInhealthSJP
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
	abstract public class esInhealthSJPQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return InhealthSJPMetadata.Meta();
			}
		}	
		

		public esQueryItem Nosjp
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Nosjp, esSystemType.String);
			}
		} 
		
		public esQueryItem Nokainhealth
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Nokainhealth, esSystemType.String);
			}
		} 
		
		public esQueryItem Tanggalpelayanan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Tanggalpelayanan, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Tanggalasalrujukan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Tanggalasalrujukan, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Nomorasalrujukan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Nomorasalrujukan, esSystemType.String);
			}
		} 
		
		public esQueryItem Kodeproviderasalrujukan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Kodeproviderasalrujukan, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaProviderAsalRujukan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.NamaProviderAsalRujukan, esSystemType.String);
			}
		} 
		
		public esQueryItem Kodeprovider
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Kodeprovider, esSystemType.String);
			}
		} 
		
		public esQueryItem Jenispelayanan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Jenispelayanan, esSystemType.String);
			}
		} 
		
		public esQueryItem Informasitambahan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Informasitambahan, esSystemType.String);
			}
		} 
		
		public esQueryItem Kodediagnosautama
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Kodediagnosautama, esSystemType.String);
			}
		} 
		
		public esQueryItem Kodediagnosatambahan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Kodediagnosatambahan, esSystemType.String);
			}
		} 
		
		public esQueryItem Poli
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Poli, esSystemType.String);
			}
		} 
		
		public esQueryItem Kelasrawat
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Kelasrawat, esSystemType.String);
			}
		} 
		
		public esQueryItem Kecelakaankerja
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Kecelakaankerja, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Username
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Username, esSystemType.String);
			}
		} 
		
		public esQueryItem Nomormedicalreport
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Nomormedicalreport, esSystemType.String);
			}
		} 
		
		public esQueryItem TanggalPulang
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.TanggalPulang, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem NoTransaksi
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.NoTransaksi, esSystemType.String);
			}
		} 
		
		public esQueryItem DetailKeanggotaan
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.DetailKeanggotaan, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem Kodejenpelruangrawat
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Kodejenpelruangrawat, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaPasien
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.NamaPasien, esSystemType.String);
			}
		} 
		
		public esQueryItem TanggalLahir
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.TanggalLahir, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem NoKartuBpjs
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.NoKartuBpjs, esSystemType.String);
			}
		} 
		
		public esQueryItem NamaProduk
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.NamaProduk, esSystemType.String);
			}
		} 
		
		public esQueryItem KelasRawatPeserta
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.KelasRawatPeserta, esSystemType.String);
			}
		} 
		
		public esQueryItem BadanUsaha
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.BadanUsaha, esSystemType.String);
			}
		} 
		
		public esQueryItem ProdukCob
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.ProdukCob, esSystemType.String);
			}
		} 
		
		public esQueryItem Idsjp
		{
			get
			{
				return new esQueryItem(this, InhealthSJPMetadata.ColumnNames.Idsjp, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("InhealthSJPCollection")]
	public partial class InhealthSJPCollection : esInhealthSJPCollection, IEnumerable<InhealthSJP>
	{
		public InhealthSJPCollection()
		{

		}
		
		public static implicit operator List<InhealthSJP>(InhealthSJPCollection coll)
		{
			List<InhealthSJP> list = new List<InhealthSJP>();
			
			foreach (InhealthSJP emp in coll)
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
				return  InhealthSJPMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InhealthSJPQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new InhealthSJP(row);
		}

		override protected esEntity CreateEntity()
		{
			return new InhealthSJP();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public InhealthSJPQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InhealthSJPQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(InhealthSJPQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public InhealthSJP AddNew()
		{
			InhealthSJP entity = base.AddNewEntity() as InhealthSJP;
			
			return entity;
		}

		public InhealthSJP FindByPrimaryKey(System.String nosjp)
		{
			return base.FindByPrimaryKey(nosjp) as InhealthSJP;
		}


		#region IEnumerable<InhealthSJP> Members

		IEnumerator<InhealthSJP> IEnumerable<InhealthSJP>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as InhealthSJP;
			}
		}

		#endregion
		
		private InhealthSJPQuery query;
	}


	/// <summary>
	/// Encapsulates the 'InhealthSJP' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("InhealthSJP ({Nosjp})")]
	[Serializable]
	public partial class InhealthSJP : esInhealthSJP
	{
		public InhealthSJP()
		{

		}
	
		public InhealthSJP(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return InhealthSJPMetadata.Meta();
			}
		}
		
		
		
		override protected esInhealthSJPQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new InhealthSJPQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public InhealthSJPQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new InhealthSJPQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(InhealthSJPQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private InhealthSJPQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class InhealthSJPQuery : esInhealthSJPQuery
	{
		public InhealthSJPQuery()
		{

		}		
		
		public InhealthSJPQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "InhealthSJPQuery";
        }
		
			
	}


	[Serializable]
	public partial class InhealthSJPMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected InhealthSJPMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Nosjp, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Nosjp;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Nokainhealth, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Nokainhealth;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Tanggalpelayanan, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Tanggalpelayanan;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Tanggalasalrujukan, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Tanggalasalrujukan;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Nomorasalrujukan, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Nomorasalrujukan;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Kodeproviderasalrujukan, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Kodeproviderasalrujukan;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.NamaProviderAsalRujukan, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.NamaProviderAsalRujukan;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Kodeprovider, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Kodeprovider;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Jenispelayanan, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Jenispelayanan;
			c.CharacterMaxLength = 1;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Informasitambahan, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Informasitambahan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Kodediagnosautama, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Kodediagnosautama;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Kodediagnosatambahan, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Kodediagnosatambahan;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Poli, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Poli;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Kelasrawat, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Kelasrawat;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Kecelakaankerja, 14, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Kecelakaankerja;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Username, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Username;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Nomormedicalreport, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Nomormedicalreport;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.TanggalPulang, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.TanggalPulang;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.NoTransaksi, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.NoTransaksi;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.DetailKeanggotaan, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.DetailKeanggotaan;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.PatientID, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.PatientID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Kodejenpelruangrawat, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Kodejenpelruangrawat;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.NamaPasien, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.NamaPasien;
			c.CharacterMaxLength = 255;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.TanggalLahir, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.TanggalLahir;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.NoKartuBpjs, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.NoKartuBpjs;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.NamaProduk, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.NamaProduk;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.KelasRawatPeserta, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.KelasRawatPeserta;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.BadanUsaha, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.BadanUsaha;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.ProdukCob, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.ProdukCob;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(InhealthSJPMetadata.ColumnNames.Idsjp, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = InhealthSJPMetadata.PropertyNames.Idsjp;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public InhealthSJPMetadata Meta()
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
			 public const string Nosjp = "nosjp";
			 public const string Nokainhealth = "nokainhealth";
			 public const string Tanggalpelayanan = "tanggalpelayanan";
			 public const string Tanggalasalrujukan = "tanggalasalrujukan";
			 public const string Nomorasalrujukan = "nomorasalrujukan";
			 public const string Kodeproviderasalrujukan = "kodeproviderasalrujukan";
			 public const string NamaProviderAsalRujukan = "NamaProviderAsalRujukan";
			 public const string Kodeprovider = "kodeprovider";
			 public const string Jenispelayanan = "jenispelayanan";
			 public const string Informasitambahan = "informasitambahan";
			 public const string Kodediagnosautama = "kodediagnosautama";
			 public const string Kodediagnosatambahan = "kodediagnosatambahan";
			 public const string Poli = "poli";
			 public const string Kelasrawat = "kelasrawat";
			 public const string Kecelakaankerja = "kecelakaankerja";
			 public const string Username = "username";
			 public const string Nomormedicalreport = "nomormedicalreport";
			 public const string TanggalPulang = "TanggalPulang";
			 public const string NoTransaksi = "NoTransaksi";
			 public const string DetailKeanggotaan = "DetailKeanggotaan";
			 public const string PatientID = "PatientID";
			 public const string Kodejenpelruangrawat = "kodejenpelruangrawat";
			 public const string NamaPasien = "NamaPasien";
			 public const string TanggalLahir = "TanggalLahir";
			 public const string NoKartuBpjs = "NoKartuBpjs";
			 public const string NamaProduk = "NamaProduk";
			 public const string KelasRawatPeserta = "KelasRawatPeserta";
			 public const string BadanUsaha = "BadanUsaha";
			 public const string ProdukCob = "ProdukCob";
			 public const string Idsjp = "idsjp";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Nosjp = "Nosjp";
			 public const string Nokainhealth = "Nokainhealth";
			 public const string Tanggalpelayanan = "Tanggalpelayanan";
			 public const string Tanggalasalrujukan = "Tanggalasalrujukan";
			 public const string Nomorasalrujukan = "Nomorasalrujukan";
			 public const string Kodeproviderasalrujukan = "Kodeproviderasalrujukan";
			 public const string NamaProviderAsalRujukan = "NamaProviderAsalRujukan";
			 public const string Kodeprovider = "Kodeprovider";
			 public const string Jenispelayanan = "Jenispelayanan";
			 public const string Informasitambahan = "Informasitambahan";
			 public const string Kodediagnosautama = "Kodediagnosautama";
			 public const string Kodediagnosatambahan = "Kodediagnosatambahan";
			 public const string Poli = "Poli";
			 public const string Kelasrawat = "Kelasrawat";
			 public const string Kecelakaankerja = "Kecelakaankerja";
			 public const string Username = "Username";
			 public const string Nomormedicalreport = "Nomormedicalreport";
			 public const string TanggalPulang = "TanggalPulang";
			 public const string NoTransaksi = "NoTransaksi";
			 public const string DetailKeanggotaan = "DetailKeanggotaan";
			 public const string PatientID = "PatientID";
			 public const string Kodejenpelruangrawat = "Kodejenpelruangrawat";
			 public const string NamaPasien = "NamaPasien";
			 public const string TanggalLahir = "TanggalLahir";
			 public const string NoKartuBpjs = "NoKartuBpjs";
			 public const string NamaProduk = "NamaProduk";
			 public const string KelasRawatPeserta = "KelasRawatPeserta";
			 public const string BadanUsaha = "BadanUsaha";
			 public const string ProdukCob = "ProdukCob";
			 public const string Idsjp = "Idsjp";
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
			lock (typeof(InhealthSJPMetadata))
			{
				if(InhealthSJPMetadata.mapDelegates == null)
				{
					InhealthSJPMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (InhealthSJPMetadata.meta == null)
				{
					InhealthSJPMetadata.meta = new InhealthSJPMetadata();
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
				

				meta.AddTypeMap("Nosjp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nokainhealth", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Tanggalpelayanan", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("Tanggalasalrujukan", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("Nomorasalrujukan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kodeproviderasalrujukan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaProviderAsalRujukan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kodeprovider", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Jenispelayanan", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Informasitambahan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kodediagnosautama", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kodediagnosatambahan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Poli", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kelasrawat", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Kecelakaankerja", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Username", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Nomormedicalreport", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TanggalPulang", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("NoTransaksi", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DetailKeanggotaan", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Kodejenpelruangrawat", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaPasien", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TanggalLahir", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("NoKartuBpjs", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("NamaProduk", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KelasRawatPeserta", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BadanUsaha", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProdukCob", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Idsjp", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "InhealthSJP";
				meta.Destination = "InhealthSJP";
				
				meta.spInsert = "proc_InhealthSJPInsert";				
				meta.spUpdate = "proc_InhealthSJPUpdate";		
				meta.spDelete = "proc_InhealthSJPDelete";
				meta.spLoadAll = "proc_InhealthSJPLoadAll";
				meta.spLoadByPrimaryKey = "proc_InhealthSJPLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private InhealthSJPMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
